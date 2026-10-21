# 02 — Database migration map

How the live SQL Server schema (144 tables, 20 views, 77 procedures) maps onto
D1 (SQLite). Generated, not hand-written: `tools/generate-d1-schema.js` reads
`docs/schema/columns.psv` (2,497 catalogued columns) and emits
`cloudflare/d1/migrations/0001_core_schema.sql`.

## Type mapping

| SQL Server | SQLite (D1) | Note |
|---|---|---|
| `int`, `bigint`, `smallint`, `tinyint`, `bit` | `INTEGER` | |
| `numeric`, `decimal`, `money`, `float` | `REAL` | IEEE-754; rounded to 2dp at the API boundary |
| `datetime`, `date` | `TEXT` | ISO-8601 |
| `varchar`, `nvarchar`, `char` | `TEXT` | |
| `image`, `varbinary` | `BLOB` | |

## Money policy

SQL Server `NUMERIC(18,2)` is exact. SQLite has no decimal type, so:

* every money value is rounded to 2 decimal places **at the API boundary**
* rounding is **half away from zero**, matching the desktop `RoundOff` helper
* the **server** recomputes all line and bill totals; client totals are ignored

This is implemented in `cloudflare/worker/src/money.js` and asserted by the API
tests (`bill total is rounded (307.50 -> 308)`).

## Keys

The source schema declares only three primary keys, so keys had to be
established rather than discovered.

**1. Identity columns → `INTEGER PRIMARY KEY`.** 134 columns are `IDENTITY`.
Each became the SQLite rowid alias. Consequence: inserts must **omit** these
columns (SQLite assigns them).

**2. Declared primary keys preserved as-is:**

* `tbl_ActivityLog.AL_RecordNo_i`
* `tbl_SmUserRights.UR_RecordNo_i`
* `tbl_Treeview.MenuID` (table not migrated yet)

**3. Business keys inferred** where a table has neither a declared key nor an
identity column. These are **inferences**, recorded here so nobody mistakes them
for original constraints:

| Table | Inferred key | Evidence |
|---|---|---|
| `tbl_Area` | `Area_Code_n` | sequential code used by the form |
| `tbl_Brand` | `Brand_Code_n` | sequential code used by the form |
| `tbl_Category` | `Category_Code_n` | sequential code used by the form |
| `tbl_Companyprofile` | `Cmp_ID_i` | single-row config table |
| `tbl_Denomination` | `Deno_Code_n` | sequential code used by the form |

If any of these is wrong, imports will collide loudly (`UNIQUE constraint
failed`), which is the desired failure mode.

## The 30 migrated tables

Masters:

`tbl_Companyprofile`, `tbl_AreaDivision`, `tbl_Area`, `tbl_Category`,
`tbl_Brand`, `tbl_UnitList`, `tbl_GSTTariff`, `tbl_Denomination`,
`tbl_DenoDtl`, `tbl_BankList`, `tbl_Settings`, `tbl_GeneralSettings`,
`tbl_RptMaster`, `tbl_DashBoard`

Users and permissions:

`tbl_UserList`, `tbl_UserRoles`, `tbl_UserRoleMenu`, `tbl_MenuMaster`,
`tbl_SmUserRights`, `tbl_UserPermission`

Parties:

`tbl_CusProfile`

Stock:

`tbl_StockDtl`, `tbl_MultiStock`, `tbl_StockModify`

Billing:

`tbl_BillSeries`, `tbl_SalDetail`, `tbl_SalParticular`, `tbl_PurchDetail`,
`tbl_PurchParticular`

Audit:

`tbl_ActivityLog`

These 30 tables are what POS billing, item master, parties, stock and the core
reports actually need. Everything else is not migrated yet.

## Not migrated (114 tables + 20 views)

Deliberately out of scope for the first cut. Grouped:

| Group | Examples | Why deferred |
|---|---|---|
| Grid layout tables (≈35) | `tbl_DgvEntry_*`, `tbl_DgvReport_*`, `tbl_DgvSubRpt_*` | These store column layouts for WinForms grids, not business data. They describe the UI, so they stay with the UI. |
| Other sales documents | `tbl_Quo*` (quotation), `tbl_Rec*` (receipt), `tbl_Trans*`, `tbl_SalOR*` | Not used by supermarket billing |
| Clinic / pharmacy leftovers | `tbl_Service*`, `tbl_Pat*`, `tbl_Fav*`, `tbl_Parking*` | Not grocery retail |
| Fuel / transport / HR | `tbl_T_*` (vehicle, trip, tyre), `tbl_EAttendance`, `tbl_Emp*` | Different product lines |
| Wastage / damage / production | `tbl_Wastage*`, `tbl_Damage*`, `tbl_Production*`, `tbl_Dum*` | Not in the current workflows |
| Bill designer | `tbl_BillDesign_*`, `tbl_PaperSize` | Printing templates, Crystal/local printer specific |
| WhatsApp / QR | `tbl_WhatsappRpt`, `tbl_QRCodeImage` | Tied to a local helper service |
| Views (20) | `CustLedger`, `SupOutstanding`, `StockLedger`, `CusDueDisplay`, … | Recreated as API report queries instead of views |

Adding any of these later is mechanical: append to `CORE_TABLES` in the
generator, re-run it, and add the name to `ALLOWED_TABLES` in
`cloudflare/worker/src/routes/sync.js`.

## Additive tables (not in the legacy schema)

These exist only in D1 and are clearly prefixed so nobody mistakes them for
legacy data:

| Table | Purpose |
|---|---|
| `api_users` | Cloud credentials (PBKDF2). Legacy `tbl_UserList.U_Password_vc` is `nvarchar(20)` and cannot hold a real hash, so it is not reused. |
| `api_audit_log` | Login and write auditing |
| `api_idempotency` | Stops a retried bill POST from creating a second bill |
| `api_bill_sequence` | Per-financial-year bill counter, seeded from legacy maxima |
| `api_sale_payment` | Cash/card/UPI split. **New capability** — the legacy POS never persisted the split (`Sa_ComType_vc` was written as `''`). |

## Schema quirks that must not be "fixed"

These were discovered by inspecting the live database and are reproduced exactly:

1. **`tbl_PurchDetail` has no `Sa_DiscountAmt_n` or `Sa_BefDisAmt_n`.**
   Only `tbl_SalDetail` has them. A purchase-header insert uses 16 columns.
   (This broke the first version of `purchases.js` — the tests caught it.)
2. **`tbl_BillSeries` has three NOT NULL columns**: `B_AcYear_vc`,
   `B_BillType_vc`, `B_BillSeries_vc`. The series cannot be created with only a
   prefix.
3. **Stock is split across two tables.** `tbl_StockDtl` is the master
   (`St_TotalSt_i`, tax rates, name). `tbl_MultiStock` holds the sellable rows
   (`St_CurrStock_i`, `St_SaleRate_n`, `St_MRPRate_n`, barcode).
   `tbl_StockDtl` has **no** `St_CurrStock_i`, `St_SaleRate_n` or
   `St_Barcode_vc` — those live only in `tbl_MultiStock`.
4. **Customers and suppliers share `tbl_CusProfile`.** They are separated by
   `C_Type_vc` (`CUSTOMER` / `SUPPLIER`). `C_ID_n` restarts at 1 per type;
   `C_AllID_n` is global. The API reproduces exactly that.
5. **Products can have several stock rows.** The API picks the lowest
   `St_StNumber_i` unless the caller names one, so results are deterministic.

## Integrity now enforced

The legacy database has no foreign keys and only three primary keys. Two
guards were added because SQLite would otherwise silently accept bad data:

* **Non-negative stock triggers** on `tbl_MultiStock.St_CurrStock_i` and
  `tbl_StockDtl.St_TotalSt_i`. A sale that would drive stock negative aborts the
  whole batch. The API also pre-checks and returns `409` with the shortage
  detail, so the normal path gives a useful message instead of a raw trigger
  error.
* **`UNIQUE` indexes** on the inferred business keys and on
  `api_idempotency.key`.

No constraints were added to legacy tables beyond these; that keeps imported
data faithful.
