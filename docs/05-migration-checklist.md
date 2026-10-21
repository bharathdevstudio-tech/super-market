# 05 — Migration checklist and rollback

The desktop app is not migrated yet. This checklist is for the day the switch
happens, and it assumes the current state: **SQL Server is the system of record,
the cloud API exists but no form uses it.**

## Before anything

- [ ] A verified backup of the SQL Server database exists
      (`BACKUP DATABASE SuperMarketDB TO DISK = ...`), and the restore has been
      tested once. An untested backup is not a backup.
- [ ] The Worker is deployed and `GET /api/health` returns `ok: true`.
- [ ] Migrations `0001` and `0002` are applied to the **remote** D1.
- [ ] `TOKEN_SECRET` is set, and the admin user can log in.
- [ ] `cloudflare/tests/api.test.mjs` passes on the machine doing the switch.
- [ ] The importer has run and finished with `RECONCILED`.
- [ ] Bill series in `api_bill_sequence` continue *after* the highest legacy
      bill number (the importer seeds this; verify, don't assume).
- [ ] Shop staff know a cut-over is happening and no billing runs during it.

## Verify the import

Row counts alone are not enough — a chunk-level replace can preserve counts while
losing values. Check all of:

- [ ] row counts per table (`/api/sync/reconcile`)
- [ ] `SUM(Sa_FinalTotal_n)` on `tbl_SalDetail` equals SQL Server (importer does this)
- [ ] `SUM(Sa_GSTTotal_n)` matches
- [ ] last 20 invoices exist with matching totals and dates
- [ ] stock quantities: `SUM(St_TotalSt_i)` matches SQL Server
- [ ] a known customer's ledger totals match
- [ ] `tbl_CusProfile` codes did not shift (`C_ID_n` per type, `C_AllID_n` global)

Spot-check three real bills by opening them in the app and comparing line by
line. If any line differs, stop.

## Cut-over

1. Stop billing.
2. Take a final incremental import (the importer is idempotent — `INSERT OR
   REPLACE` — so re-running is safe).
3. Roll the API URL out to the desktop app (once the client is wired).
4. Create the first cloud bill. Verify:
   - [ ] invoice number is the expected next number, not 1
   - [ ] GST amounts match what the desktop would have produced
   - [ ] stock decreased by exactly the billed quantity
   - [ ] re-submitting the same bill (same idempotency key) does not duplicate it
5. Let it run for one full day before removing the SQL Server path.

## Rollback

The reason this is safe: **SQL Server was never modified.** The importer is
read-only, so the old system is intact and current as of step 1.

### If the API is wrong but the desktop still uses SQL Server

Nothing to do. Keep using SQL Server. Fix the API, re-run the importer, try
again another day.

### If forms have already been switched to the API

1. **Bills created in the cloud after the cut-over exist only in D1.**
   Before pointing the app back at SQL Server, export them:

   ```powershell
   npx wrangler d1 execute bharath-supermarket --remote --command `
     "SELECT * FROM tbl_SalDetail WHERE Sa_BillDate_d >= 'CUTOVER-DATE'"
   ```

   and the matching `tbl_SalParticular` rows. Re-enter them in the desktop app
   manually. **Do not** insert them directly into SQL Server unless you also
   adjust stock by hand — the desktop's stock table will otherwise be wrong.

2. Revert the desktop app to the previous build (keep the previous
   `BharathSuperMarket.exe` before deploying a new one).

3. Re-verify stock against physical count if any cloud bills were posted.

4. Rotate `TOKEN_SECRET` so any stale tokens stop working.

### If D1 data is corrupted

```powershell
npx wrangler d1 export bharath-supermarket --remote --output d1-before-restore.sql
# delete the database only after exporting
npx wrangler d1 delete bharath-supermarket
# recreate, migrate, re-import from SQL Server
```

Re-importing is always possible because SQL Server remains the source of truth
until the migration is declared finished.

## Things that must never be done

- Never point the desktop app's SQL connection string at D1. D1 is not SQL
  Server and the existing `SqlConnection` code cannot talk to it.
- Never delete the SQL Server database on the day of cut-over.
- Never claim offline billing works — there is no offline queue.
- Never commit `.env`, real connection strings, `TOKEN_SECRET`, or API keys.
