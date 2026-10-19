# Superseded database scripts

These two scripts belong to the **first** attempt, when `SuperMarketDB` had its own
small custom schema (about 51 tables). That database was never compatible with the
shared VISTAWIN core forms, so it was **replaced**.

The live database is now a **rebuild of the legacy retail schema**
(`tbl_*`, ~142 tables / 77 procedures / 20 views, restored from the reference
database `BHAVANIELECSURDB`). The core forms (`FrmMDICommon`, `FrmMiniLogIn`,
`FrmDgvStockCommon`, …) expect exactly that schema.

Therefore:

* `01_SuperMarketDB_Schema.sql` — **do not run.** It would add the old custom
  tables back on top of the legacy schema.
* `02_SuperMarketDB_Seed.sql` — **do not run.** It seeds the old custom shape.

Use these instead (see `../`):

* `03_SuperMarketDB_Extensions.sql` — the small additive changes the SuperMarket
  module needs on the legacy schema
  (`tbl_Category.Cat_GSTPer_n`, `tbl_Category.Cat_Show_vc`,
  `tbl_Brand.Brand_Show_vc`, `tbl_ActivityLog`, `tbl_SmUserRights`).
* `04_SuperMarketDB_Seed.sql` — the configuration rows
  (company profile, admin user, menu + role grants, units, categories, brands,
  GST slabs, bill series).

Kept only for history. Safe to delete once you are happy with the rebase.
