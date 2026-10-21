# 01 — Source inventory

What the application is, measured from the repository rather than assumed.

## Size

| Metric | Value |
|---|---|
| VB source files | 101 |
| VB source lines | 94,935 |
| Files that open SQL Server connections | 43 |
| `SqlConnection` references | 37 |
| `SqlCommand` references | 337 |
| `SqlDataAdapter` references | 189 |
| `VistawinFunctions.ServerConnection` references | 548 |
| Solution / project | 1 solution, 1 project (`Windows Application.vbproj`) |
| Target framework | .NET Framework 3.5 |
| Output assembly | `BharathSuperMarket.exe` (was `VISTAWIN.exe`) |

## Where the code lives

| Folder | What is in it |
|---|---|
| `Windows Application/` | Main project: MDI shell, login, masters, reports |
| `Windows Application/SuperMarket/` | POS billing and supermarket screens |
| `Database/` | SQL Server schema / seed / upgrade scripts |
| `Database/_superseded/` | Earlier scripts, kept for reference |
| `cloudflare/` | New Worker API, D1 migrations, tests, import tool |
| `docs/` | This documentation set and the schema catalog |

## Two facts that shape the whole migration

**1. This was not originally a supermarket product.**
The code carries clinic/pharmacy leftovers: `Pat_PlanImg`, `HTHLoginLogo`,
`Kiruthick Clinic`, `HTH` prefixes, patient-image folders. The `SuperMarket/`
folder is a later addition on top of that base. Consequence: some screens and
tables have nothing to do with grocery retail. They are not migrated just
because they exist — the migration map marks them.

**2. The database has almost no declared integrity.**
144 tables, 77 stored procedures, 20 views, 1 scalar function, and:

| Integrity feature | Count |
|---|---|
| Declared primary keys | 3 columns in 3 tables |
| Declared foreign keys | 0 |
| Identity columns | 134 |

The only declared primary keys are:

* `tbl_ActivityLog.AL_RecordNo_i`
* `tbl_SmUserRights.UR_RecordNo_i`
* `tbl_Treeview.MenuID`

Everything else relies on the application to keep it consistent. That is the
single biggest risk in moving to a different database engine, because SQLite
(D1) will happily accept duplicates that the app never creates.

**Mitigation:** the D1 schema generator invents a business key for every table
that has no declared key (`tools/generate-d1-schema.js`, `BUSINESS_KEYS`). The
mapping is listed in `02-database-migration-map.md`. It is documented as an
inference, not as something the original schema declared.

## Forms and their data access

| Area | Example forms | Access pattern |
|---|---|---|
| Login / database setup | `FrmMiniLogIn`, `FrmDBChange`, `FrmSqlQueryUpdate` | Direct connection string building, `master` queries |
| MDI shell / menus | `FrmMDICommon`, `FrmMDILeftMenu` | Menu table, form resolution |
| Item master | `FrmDgvStockCommon`, `FrmStock*` | Heavy `SqlDataAdapter` use, grid binding |
| POS billing | `SuperMarket/FrmSmPosBilling` | Header + detail inserts, stock updates |
| Purchases | `FrmPurchase*` | Header + detail inserts, stock updates |
| Reports | `Rpt*` forms | Crystal Reports against SQL Server |
| Maintenance | `FrmDBBackup`, `FrmDownload` | Vendor endpoints (see risks) |

Every one of these builds its own `SqlConnection`/`SqlCommand`. There is no
repository layer today. That is why the API client is being introduced as a new
layer rather than by rewriting each form from scratch.

## Branding surfaces found

1,213 case-insensitive occurrences of "vistawin" in 58 files, but they are not
all branding:

| Category | Count | Action |
|---|---|---|
| `VistawinFunctions` (internal helper class) | 1,079 | **Kept** — see `06-branding-and-risks.md` |
| `VistawinSolution1` (My.Settings key) | ~30 | Kept (settings persistence key) |
| Visible text / captions / titles | 8 | Replaced with Bharath SuperMarket |
| Filesystem paths (`E:\VISTAWIN\...`) | ~20 | **Kept** — they point at existing data folders |
| Vendor endpoints (`vistawin.store`, `vistawin.online`) | ~15 | **Kept but flagged as a security issue** |
| Assembly name / metadata | 3 | Replaced |

## Tools added for this migration

| Tool | Purpose |
|---|---|
| `tools/generate-d1-schema.js` | Reads `docs/schema/columns.psv`, emits D1 DDL |
| `tools/apply-branding.js` | Byte-preserving text replacement (project has non-UTF8 files) |
| `tools/patch-skipworktree-index.js` | Patches staged copy of a `skip-worktree` file without touching local secrets |
| `cloudflare/tools/import-from-sqlserver.ps1` | Read-only SQL Server → D1 import with reconciliation |
