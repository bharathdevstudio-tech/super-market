# 07 — Offline-First Sync: Phase 1 Inspection

Read-only inspection of the **existing** application. Nothing here is a proposal
to change billing, tax, rounding, discount, stock or printing logic. Everything
below was read from the actual source and the actual schema export
(`docs/schema/columns.psv`), not assumed.

Date: 2026-10-23
Scope inspected: `Windows Application/SuperMarket/*.vb`,
`Windows Application/SuperMarket/VSuperMarket.vb`,
`docs/schema/columns.psv`, `cloudflare/worker/src/routes/sales.js`.

---

## 1. Existing architecture (as it is today)

```
FrmSmPosBilling (VB.NET WinForms)
    -> SqlTransaction on VistawinFunctions.ServerConnection
        -> dbo.SP_tbl_SalDetail_Insert        (bill header)
        -> dbo.SP_tbl_SalParticular_Insert    (one per bill line)
        -> UPDATE tbl_MultiStock              (live stock)
        -> UPDATE tbl_StockDtl                (godown stock)
        -> INSERT tbl_StockModify             (stock audit trail)
    -> Trans.Commit()
```

There is **no** queue, no sync table and no offline buffer anywhere in the
project today. Confirmed by searching all `.vb`/`.sql`/`.md`/`.gs` files for
`sync_queue`, `SyncQueue`, `SM_SYNC` — zero hits.

---

## 2. Bill tables (verified column by column)

### 2.1 `tbl_SalDetail` — bill **header**

Identity: `Sa_RecordNo_i` (`int`, NOT NULL, identity) — the only key.

Columns actually written by the POS (`InsertBillHeader`, line 1094):

| Column | Type | Source |
|---|---|---|
| `Sa_EntDate_d` | datetime | `Date.Now` |
| `Sa_Shift_vc` | varchar(30) | `""` |
| `Sa_SaveType_vc` | varchar(30) | bill type (INVOICE / ESTIMATE / SALES RETURN) |
| `Sa_BillDate_d` | datetime | `dtpBillDate.Value.Date` |
| `Sa_BillTime_vc` | varchar(50) | `Date.Now.ToString("HH:mm")` |
| `Sa_InvNo_vc` | varchar(100) | `series & billNo` |
| `Sa_BillNo_n` | numeric(18,0) | running number per save type + FY |
| `Sa_BType_vc` | varchar(10) | same as save type |
| `Sa_CusID_n` | int | customer id, 0 for CASH |
| `Sa_CusName_vc` | varchar(100) | customer name, `"CASH"` fallback |
| `Sa_GSTTotal_n` | numeric(18,2) | `txtTax.Text` |
| `Sa_TotalAmt_n` | numeric(18,2) | `txtSubTotal.Text` |
| `Sa_RoType_vc` | varchar(20) | `"ROUND OFF"` |
| `Sa_RoAmt_n` | numeric(18,2) | `RoundOff(bill) - bill` |
| `Sa_FinalTotal_n` | numeric(18,2) | `txtBillAmount.Text` |
| `Sa_Words_vc` | varchar(900) | amount in words |
| `Sa_AcYear_vc` | varchar(20) | e.g. `2026-2027` |
| `Sa_MRPTotal_n` | numeric(18,2) | `SUM(MRP * Qty)` |
| `Sa_UserID_vc` | varchar(10) | `VSuperMarket.CurrentUserName` |
| `Sa_UpDate_d` | datetime | `Date.Now` |
| `Sa_DueDate_d` | datetime | `dtpBillDate.Value.Date` |
| `Sa_BefDisAmt_n` | numeric(18,2) | `txtSubTotal.Text` |
| `Sa_DiscountAmt_n` | numeric(18,2) | `txtDiscount.Text` |

Also written as literal zeros/empties: `Sa_BillPoints_n`, `Sa_TodaySave_n`,
`Sa_EmpID_i`, `Sa_EmpPer_n`, `Sa_EmpAmt_n`, `Sa_COutAmt_n`, `Sa_OldBal_n`,
`Sa_MarPer_n`, `Sa_MarAmt_n`, `Sa_ComType_vc`, `Sa_Remark_vc`,
`Sa_DeliAdd_vc`, `Sa_VehicleNo_vc`, `Sa_RecNo_i`, `Sa_OldInvNo_vc`,
`Sa_GSTWords_vc`.

### 2.2 `tbl_SalParticular` — bill **lines**

The naming is inverted relative to what you would expect: "Detail" holds the
header, "Particular" holds the lines.

Identity: `Sa_RecordNo2_i` (`int`, NOT NULL, identity).
`Sa_StCode_n` (`int`) is NOT NULL.

Columns written per line (`SaveLine`, line 1145): `Sa_SaveTy_vc`,
`Sa_BillDate_d`, `Sa_InvNo_vc`, `Sa_CusID_n`, `Sa_StNumber_n`,
`Sa_StCode_n`, `Sa_PurRate_n`, `Sa_MRP_n`, `Sa_SaleRate_n`, `Sa_ProDPer_n`,
`Sa_ProDAmt_n`, `Sa_NetRate_n`, `Sa_Qty_i`, `Sa_FQty_i`, `Sa_TQty_i`,
`Sa_QType_vc`, `Sa_Pack_vc`, `Sa_Taxable_n`, `Sa_CGSTP_n`, `Sa_SGSTP_n`,
`Sa_IGSTP_n`, `Sa_CGSTAmt_n`, `Sa_SGSTAmt_n`, `Sa_IGSTAmt_n`,
`Sa_ProTotal_n`, `Sa_PRemark_vc`, `Sa_SRTInv_vc`, `Sa_DumRate_n`,
`Sa_SaleMar_n`, `Sa_SaleMarAmt_n`, `Sa_Delivery_vc`, `Sa_DisCalType_vc`.

Line type note: `Sa_StNumber_n` is `numeric(18,0)` but `Sa_StCode_n` is `int`.
`ReadBillLines` (line 1082) has a comment about this exact trap.

### 2.3 Purchase tables — same shape

`tbl_PurchDetail` (header, identity `Sa_RecordNo_i`) and
`tbl_PurchParticular` (lines, identity `Sa_RecordNo2_i`) mirror the sales
tables. Purchase save is at `FrmSmPurchase.BtnSave_Click`, line 470:
`BeginTransaction` -> `NextBillNumber(Trans, bt, "tbl_PurchDetail")` ->
`SaveHeader` -> `SaveLine` per row -> `Commit`.

Purchase form is ADD-only in this screen (no edit/delete path).

---

## 3. Stock tables

| Table | Role | Key |
|---|---|---|
| `tbl_StockDtl` | item master + godown stock | `St_StockID_n` (numeric(18,0)) |
| `tbl_MultiStock` | per-batch/live stock + rates | `St_StNumber_i` (numeric(18,0)), identity `St_RecordNo2_i` |
| `tbl_StockModify` | stock audit trail | identity `St_RecordNo_i` |

`ApplyStockDelta` (line 1224) updates **both**:

```sql
UPDATE tbl_MultiStock SET St_CurrStock_i = ISNULL(St_CurrStock_i,0) + @D WHERE St_StNumber_i = @SN
UPDATE tbl_StockDtl   SET St_TotalSt_i   = ISNULL(St_TotalSt_i,0)   + @D
 WHERE St_StockID_n = @C AND (St_Decrease_vc='YES' OR St_Decrease_vc IS NULL)
```

`@D` is `-qty` for a sale and `+qty` for a sales return. Note the
`St_Decrease_vc` guard on the second update — items flagged `NO` do not move
godown stock. Any sync implementation must reproduce this, not simplify it.

`WriteStockAudit` (line 1238) inserts into `tbl_StockModify` with
`St_Form_vc` = save type, `St_Modify_vc` = ADD / EDIT / BILL DELETE / ROW DELETE.

---

## 4. Bill numbering (verified)

`VSuperMarket.NextBillNumber` (line 65) — must be called inside an open
transaction:

1. `CurrentFY` = `GetFY(Date.Today)`, e.g. `2026-2027`.
2. Read `tbl_BillSeries` for `B_BillType_vc = saveType AND B_AcYear_vc = FY`
   with `WITH (UPDLOCK,HOLDLOCK)`, taking `B_BillSeries_vc` (prefix) and
   `B_StartNo_i` (start number).
3. Count existing rows for type+FY.
   - 0 rows -> `billNo = startNo`
   - else -> `billNo = MAX(CONVERT(NUMERIC(18,0), Sa_BillNo_n)) + 1`
4. `InvoiceNo = Series & BillNo.ToString("0")`.

All three `tbl_BillSeries` columns are NOT NULL (`B_AcYear_vc`,
`B_BillType_vc`, `B_BillSeries_vc`), plus `B_StartNo_i`.

**Consequence for sync:** bill numbers are allocated by the *local* SQL Server
and must be preserved verbatim in the cloud. The current Worker
`createSale` calls its own `nextBillNumber()` and ignores any client number,
so it **cannot** be used for replication. A separate ingest route is required.

---

## 5. Keys and constraints (important)

Across the whole database there are:

- **0 foreign keys**
- only **3 declared primary keys**: `tbl_ActivityLog.AL_RecordNo_i`,
  `tbl_SmUserRights.UR_RecordNo_i`, `tbl_Treeview.MenuID`
- **134 identity columns**

So `tbl_SalDetail`, `tbl_SalParticular`, `tbl_PurchDetail`,
`tbl_PurchParticular`, `tbl_MultiStock` and `tbl_StockModify` have **no
declared PK** apart from their identity column, and no FK relationships.
Referential integrity is enforced only by application code.

Practical implications:

- `Sa_InvNo_vc` is the de-facto business key but is **not unique-constrained**.
  The duplicate guard is a `SELECT COUNT(*)` inside the transaction
  (`BtnSave_Click` line 1036), not a DB constraint.
- Therefore the sync layer must treat `(SaveType, AcYear, InvNo)` as the
  identity of a bill and carry its own uniqueness guarantee.
- Deleting rows by `Sa_InvNo_vc` (as `DeleteBill` does) can in principle affect
  more than one bill if the same invoice number were ever duplicated. Sync must
  not make this worse — it must key on the same values the app uses.

---

## 6. Exact save / delete / update transaction boundaries

| Operation | Location | Transaction |
|---|---|---|
| Save new bill | `BtnSave_Click` line 992 -> `Commit` line 1053 | yes |
| Update reopened bill | same block, `editing = True` branch line 998 | yes |
| Delete bill | `DeleteBill` line 919 -> `Commit` line 937 | yes |
| Purchase save | `FrmSmPurchase.BtnSave_Click` line 470 -> `Commit` line 489 | yes |

Update path detail (line 998-1028): reverse old stock via `ReadBillLines`,
`DELETE FROM tbl_SalParticular`, `DELETE FROM tbl_SalDetail`, then re-insert
under the **same** `invNo` / `billNo` / `acYear`.

Note that `DeleteBill` and the update path both use
`VistawinFunctions.ServerConnection` (a single shared connection) and mutate the
module-level `Trans` field. Any sync hook must run **inside** that same
transaction, before `Commit`, and must use the same connection object.

---

## 7. Where a sync hook can safely attach

The three safe insertion points — inside the existing transaction, after all
writes, before `Commit`:

1. `FrmSmPosBilling.BtnSave_Click` — after the `SaveLine` loop (line 1051),
   before `Trans.Commit()` (line 1053).
2. `FrmSmPosBilling.DeleteBill` — after the two `DELETE` statements
   (line 936), before `Trans.Commit()` (line 937).
3. `FrmSmPurchase.BtnSave_Click` — after the `SaveLine` loop (line 487),
   before `Trans.Commit()` (line 489).

Plus the existing edit branch is covered by point 1 (same code path, `editing`
flag distinguishes INSERT from UPDATE).

No calculation, printing, report or stored procedure needs to change.

---

## 8. Consequences for the design (Phase 2+)

1. **Queue must be written inside the same transaction.** If it were written
   after `Commit` in a separate connection, a crash between the two would lose
   the bill from the cloud permanently, with no record that anything was missed.
2. **The queue must not become a new point of failure for billing.** If the
   queue tables are absent (fresh install, restore from an old backup), the
   app must keep billing. This needs an explicit capability check, not a blind
   `INSERT`.
3. **Payload must carry the local bill number**, and the cloud ingest route
   must accept it rather than allocate its own.
4. **Idempotency key must be generated in .NET** (a `Guid`), stored in the
   outbox row and sent as `X-Idempotency-Key`, so a retried upload cannot
   create a duplicate bill.
5. **Update and delete must be replicated as events**, not as snapshots, because
   the cloud copy has to reverse the previous stock movement exactly the way
   `DeleteBill` / the `editing` branch do locally.
6. **`.NET Framework 3.5` constraints.** No `System.Net.Http`, no `async/await`,
   no `Task`, no `DateTimeOffset` serialisation helpers, no LINQ-to-JSON. JSON
   must be produced by hand (as the existing `FrmDBBackup` code already does)
   and HTTP must use `HttpWebRequest`.

---

## 9. Writer scan — who can create a sales bill?

Searched the whole project for every possible writer. Result:

| Statement | Files found |
|---|---|
| `SP_tbl_SalDetail_Insert` | **only** `FrmSmPosBilling.vb:1096` |
| `INSERT INTO tbl_SalDetail` | none |
| `SP_tbl_SalParticular_Insert` | **only** `FrmSmPosBilling.vb:1161` |
| `INSERT INTO tbl_SalParticular` | none |
| `INSERT INTO tbl_PurchDetail` | **only** `FrmSmPurchase.vb:508` |
| `INSERT INTO tbl_PurchParticular` | **only** `FrmSmPurchase.vb:568` |

`FrmDgvSalesRetail` — repeatedly named in code comments as the "legacy POS" —
**does not exist in this project**. The referenced `SP_tbl_SalDelete` /
`SP_tbl_SalDetail` procedures live in the database, not in this source tree.

Conclusion: in this codebase there is exactly **one** writer per document
family, so a single hook per form covers every bill that can be created.

## 10. Payment split — confirmed NOT persisted by POS

`tbl_SalDetail` has **no** payment/tender/mode column (searched the full column
export for `pay`, `cash`, `card`, `upi`, `tender`, `mode` — zero matches on that
table). The only payment-related tables in the database are the receipt
family: `tbl_ReceiptDtl`, `tbl_ReceiptSummary`, `tbl_ReceiptInvWise`,
`tbl_RecDtl`, `tbl_ChqRecDtl`, `tbl_ChqRecInvWise`, `CashLedger`.

So:

- The POS bill screen records **what is owed**, not **how it was paid**.
- Payment collection is a separate receipts module.
- `api_sale_payment` in the Worker is a **new, cloud-only capability**, not a
  replication of anything that exists locally.

This must be stated honestly in any deployment note: syncing a bill does not
sync a payment split, because there is no payment split to sync. If payment
replication is wanted, `tbl_ReceiptDtl` is the table to replicate — as its own
entity, in a later phase.

## 11. Remaining unknowns (must be resolved before Phase 3 is wired)

- **Stability of `St_StNumber_i`.** It is `numeric(18,0)` NOT NULL with no
  declared unique constraint, and it is the key the POS uses to move stock. If a
  restore or a stock-merge ever renumbers it, a queued event captured before the
  restore would point at the wrong row. The queue stores `StNumber` **and**
  `StCode` so the cloud can detect a mismatch instead of silently corrupting
  stock.
- **No declared uniqueness on `(Sa_SaveType_vc, Sa_AcYear_vc, Sa_BillNo_n)`.**
  The duplicate guard is an application-level `SELECT COUNT(*)`. The queue's
  cloud ingest therefore has to enforce uniqueness itself.
- **`tbl_BillSeries` seeding for a new financial year.** `NextBillNumber`
  silently falls back to `startNo = 1` when no row exists for the FY. Not a sync
  problem, but it means the cloud cannot derive a bill number and must always
  accept the one the client sends.

---

## 12. Phase 2 — local sync queue (implemented)

Files: `db/sqlserver/01_create_sync_queue.sql`,
`02_rollback_sync_queue.sql`, `03_verify_sync_queue.sql`, `README.md`.

Purely additive objects, all prefixed `SM_Sync`:

| Object | Kind | Purpose |
|---|---|---|
| `SM_SyncOutbox` | table | One row per document change event. Written in the same transaction as the bill. |
| `SM_SyncOutboxLog` | table | Append-only attempt history (`CLAIMED`/`SENT`/`FAILED`/`DEAD`, HTTP status, message). |
| `SM_SyncMeta` | table | Key/value bookkeeping (`StoreID`, `SyncEnabled`, `LastSuccessfulSyncUtc`). |
| `SM_SyncOutbox_Claim` | proc | Atomically claims a batch (`READPAST` + `UPDLOCK` + `ROWLOCK`, `TOP (@n)`), reclaims stale `SENDING` rows. |
| `SM_SyncOutbox_MarkSent` | proc | Marks `SENT`, guarded on `Status='SENDING'`. |
| `SM_SyncOutbox_MarkFailed` | proc | Retry with caller-supplied backoff; moves to `DEAD` at the attempt ceiling. |
| `SM_SyncOutbox_Stats` | proc | Counts per status for the UI. |

Design points that matter:

- `EventID` (`UNIQUEIDENTIFIER`, `UQ_SM_SyncOutbox_EventID`) is generated in .NET
  and is the idempotency key from the desktop all the way to the Worker
  (`X-Idempotency-Key`).
- `SaveType` / `AcYear` / `BillNo` are duplicated out of the JSON payload as real
  columns so the queue can be queried on 2008 R2 without a JSON parser.
- `ContentHash` is `CHAR(64)` SHA-256 hex computed with
  `System.Security.Cryptography.SHA256` in .NET, because 2008 R2 `HASHBYTES`
  only offers MD5/SHA1.
- `SyncEnabled` is seeded to `'0'`, so the schema can be deployed without
  changing any runtime behaviour until Phase 3 is wired and configured.

### Verification actually performed

Against a real **SQL Server 2008 R2 SP2 Express (10.50.4000.0)** instance:

1. `01_create_sync_queue.sql` — executed clean; executed a second time clean
   (idempotent).
2. `03_verify_sync_queue.sql` — all 7 objects `PASS`, unique constraint and both
   indexes `PASS`, `LeakCheck` `PASS` (no `SM_Sync` column on a business table),
   smoke test enqueue -> claim -> `SENT` -> log row -> stats all correct, and the
   final `RollbackCheck` `PASS` (nothing left behind).
3. `02_rollback_sync_queue.sql` — executed clean, only `SM_Sync*` removed, stub
   business tables still present; executed a second time clean.
4. Scratch database dropped afterwards. The real application database was **not**
   modified.

Known behavioural note: `SM_SyncOutbox_Stats` uses a sentinel date instead of
`NULL` inside `MIN(...)` to avoid the 2008 R2 `Null value is eliminated by an
aggregate` warning; `NULLIF` converts the sentinel back to `NULL`.

---

## 13. Phase 3 — queue writer (implemented)

New file: `Windows Application/SuperMarket/SmSyncQueue.vb`
(`Friend NotInheritable Class SmSyncQueue`), added to the `.vbproj`.

Attach points — all inside the **existing** transaction, after the bill rows are
written, before `Commit`. No calculation, tax, rounding, discount, stock,
printing, report or stored procedure was changed.

| Location | Call |
|---|---|
| `FrmSmPosBilling.BtnSave_Click`, before `BeginTransaction` | `SmSyncQueue.EnsurePrepared()` |
| `FrmSmPosBilling.BtnSave_Click`, after the `SaveLine` loop | `SmSyncQueue.EnqueueSale(Trans, bt, invNo, If(editing, "UPDATE", "INSERT"))` |
| `FrmSmPosBilling.DeleteBill`, before `BeginTransaction` | `SmSyncQueue.EnsurePrepared()` |
| `FrmSmPosBilling.DeleteBill`, before the first `DELETE` | `SmSyncQueue.EnqueueSaleDeleted(Trans, bt, invNo)` |
| `FrmSmPurchase.BtnSave_Click`, before `BeginTransaction` | `SmSyncQueue.EnsurePrepared()` |
| `FrmSmPurchase.BtnSave_Click`, after the `SaveLine` loop | `SmSyncQueue.EnqueuePurchase(Trans, bt, bn.InvoiceNo, "INSERT")` |

Design choices:

- **Read-back, not UI copy.** The payload is built by re-reading
  `tbl_SalDetail` / `tbl_SalParticular` (or the purchase pair) inside the
  transaction. Whatever the stored procedures actually wrote is what gets
  synced — no second, drifting copy of the billing rules.
- **Two distinct guards.** `EnsurePrepared()` probes once for the three
  `SM_Sync*` tables (cached per process). If they are missing, every enqueue is
  a silent no-op and billing behaves exactly as before. `SM_SyncMeta.SyncEnabled`
  is separate and only gates the *uploader* (Phase 4), so events are never lost
  just because uploading is not switched on yet.
- **Failure policy.** Once the outbox exists, an enqueue failure rolls the bill
  back. A silently unsynced bill is worse than a visible error. All values are
  defensively truncated/hashed so the insert cannot fail on data shape.
- **Delete ordering.** `EnqueueSaleDeleted` is called while the header row still
  exists, so the event carries `saveType`/`invoiceNo`/`billNo`/`acYear`; the
  delete itself then runs in the same transaction.
- **Payload is self-describing** (`eventId`, `storeId`, `entity`, `operation`,
  `schemaVersion`, `bill`, `lines`) with a SHA-256 `ContentHash` over the exact
  UTF-8 bytes, so the Worker can reject a mangled body.
- **.NET 3.5 only:** hand-built JSON (`StringBuilder`), `SHA256.Create()`,
  `SqlDataReader`, no `System.Net.Http`, no `async`, no LINQ-to-JSON.

### Verification actually performed

1. **Build** — `MSBuild` Debug build succeeded
   (`bin\Debug\BharathSuperMarket.exe`).
2. **Column names** — the four SELECT statements were extracted from the VB
   source by `tools/gen-sync-sql-test.js` (not transcribed) and executed as
   `SELECT TOP 0` against the **real `SuperMarketDB`** on `.\SQLEXPRESS`. All
   four executed with zero errors; nothing was written.
3. **Insert shape** — the `SM_SyncOutbox` INSERT was extracted the same way and
   executed against the outbox schema: 1 row inserted.
4. **JSON/hash unit checks** (`tools/verify-payload.ps1`, 32-bit PowerShell
   reflecting into the built exe): `Sha256Hex("abc")` matches the known vector,
   `JsonText` round-trips quotes/backslashes/newlines/Tamil text, `null`
   handling, full sale payload parses with the right values, DELETE payload has
   zero lines. All `PASS`.
5. **End-to-end enqueue** (`tools/verify-enqueue.ps1`): a scratch database was
   built with the **real column shapes** (`SELECT TOP 0 * INTO ... FROM
   SuperMarketDB.dbo.<table>`), the outbox script applied, sample sale/purchase
   bills inserted, then the **real `SmSyncQueue` code from the built assembly**
   was invoked with `VistawinFunctions.ServerConnection` pointed at the scratch
   DB. Result: 4 events (`INV42` INSERT, `INV42` UPDATE, `INV43` DELETE,
   `PUR7` INSERT), all 4 hashes matched their payload, all 4 payloads parsed and
   matched their columns, line details preserved, unique `EventID`s, all rows
   seeded `PENDING`/0 attempts. All `PASS`.
6. **Negative path** (`tools/verify-missing.ps1`): with the connection pointed at
   a database **without** the outbox, `IsAvailable` is `False` and all three
   enqueue calls are no-ops that do not throw. This is the "billing must keep
   working" guarantee, measured rather than assumed.
7. **Runtime** (`tools/verify-outbox-runtime.sql`): claim batch, immediate
   re-claim = 0 rows, `MarkSent` → `SENT` + log, `MarkFailed` → `FAILED` with a
   future `NextAttemptAtUtc` + log, stale-lock reclaim picked up only the stale
   row with attempt 2, attempt ceiling → `DEAD` + log, `DEAD` never claimed
   again, stats correct. All as designed.
8. Scratch database dropped. `SuperMarketDB` was only ever read.

### How to switch sync on (operator steps)

1. Back up the database.
2. Run `db/sqlserver/01_create_sync_queue.sql` on the live database, then
   `03_verify_sync_queue.sql`.
3. Confirm billing still saves normally — the outbox now receives a row per bill
   with `Status='PENDING'`.
4. Leave `SM_SyncMeta.SyncEnabled='0'` until Phase 4 (uploader + Worker ingest
   route) is deployed. Setting it to `'1'` is what starts uploading.

### Known gap (by design, to be closed in a later phase)

Bills saved while `SM_Sync*` did not exist have no outbox row. A one-off
backfill (scan `tbl_SalDetail` for invoices with no `SALE` event) is required
when the queue is first deployed on an existing shop database. That backfill is
not part of Phase 3.


