# VISTAWIN — SuperMarket App

A legacy **VB.NET / Windows Forms** supermarket point-of-sale and inventory
application (Visual Studio 2010, .NET Framework 3.5) backed by **SQL Server**.

## Repository layout

| Path | Contents |
|---|---|
| `Windows Application.sln` | Visual Studio solution |
| `Windows Application/` | Project: forms, modules, resources, `SuperMarket/` POS module |
| `Database/` | SQL Server schema, seed and upgrade scripts |
| `Database/_superseded/` | Earlier schema/seed scripts, kept for reference |

## Requirements

* Visual Studio 2010 (or the MSBuild v4.0 tool-chain)
* .NET Framework 3.5 targeting pack
* SQL Server (Express is sufficient)
* **SAP Crystal Reports runtime for .NET 13.0.2000.0** — the
  `CrystalDecisions.*` assemblies are GAC-installed and are *not* part of
  this repository.

## Third-party assemblies

`bin/` and `obj/` are excluded from version control, but the project
references the following third-party DLLs **from `bin\Debug\`**.  Copy them
into `Windows Application\bin\Debug\` before building.

| Assembly | Purpose | Licence (verify before redistributing) |
|---|---|---|
| `AForge.Video.dll`, `AForge.Video.DirectShow.dll` | camera capture | LGPL |
| `zxing.dll`, `MessagingToolkit.Barcode.dll`, `MessagingToolkit.QRCode.dll` | barcode / QR | Apache-2.0 / MIT |
| `Newtonsoft.Json.dll` | JSON | MIT |
| `EPPlus.dll` | Excel export | LGPL |
| `itextsharp.dll` | PDF | AGPL |
| `TwainLib.dll`, `AxInterop.TwainControlXTrial.dll`, `Interop.TwainControlXTrial.dll` | TWAIN scanning | check vendor terms |

## Build

```
MSBuild.exe "Windows Application\Windows Application.vbproj" /p:Configuration=Debug /t:Build
```

Output: `Windows Application\bin\Debug\VISTAWIN.exe`

## Database setup

Run the scripts in `Database\` in numeric order, for example:

```
sqlcmd -S .\SQLEXPRESS -E -i Database\03_SuperMarketDB_Extensions.sql
...
sqlcmd -S .\SQLEXPRESS -E -f 65001 -i Database\11_SuperMarketDB_Sample_Items.sql
sqlcmd -S .\SQLEXPRESS -E -i Database\12_SuperMarketDB_Menu_OnlineSync.sql
```

Script 12 adds the **ONLINE → Online Sync** menu entry. It is idempotent, so it
is safe to re-run after upgrading.

> `-f 65001` is required for script 11 so that Tamil product names are stored
> correctly instead of as `?`.

The seed scripts create demo logins — application user `admin` / `admin`.
Change them before any real use.

## Configuration

Database credentials and cloud-backup keys are **not** committed.  The
source contains placeholders that you set after cloning:

| Placeholder | Meaning | Files |
|---|---|---|
| `YOUR_SA_PASSWORD` | SQL Server `sa` password | `FrmDBChange.vb`, `FrmMiniLogIn.vb`, `FrmSqlQueryUpdate.vb` |
| `YOUR_API_KEY` | optional cloud-backup key | `FrmDBBackup.vb` |
| `YOUR_GOOGLE_APPS_SCRIPT_URL` | optional backup webhook | `FrmDBBackup.vb` |

Some optional maintenance features also contact the vendor's own
`vistawin.store` endpoints (application update, SQL helper, backup upload).
Remove or replace those URLs if you do not want the application to use them.

## Semi-online sync

The app keeps working offline on SQL Server, and can push its data to a hosted
backend for owner reports. Open **ONLINE → Online Sync**.

| Direction | Purpose |
|---|---|
| SQL Server → Hostinger | push local data to the server (default) |
| Hostinger → SQL Server | pull server data back into SQL Server |

The backend is **free**: a Google Apps Script web app storing the data in a
Google Sheet. No server, no database fee, no credit card.

### One-time setup (about 5 minutes)

1. Create a blank spreadsheet at <https://sheets.google.com>
   (for example `SuperMarket Sync`).
2. In that sheet: **Extensions → Apps Script**.
3. Delete the sample code and paste `Server\GoogleAppsScript\Code.gs`.
4. **Deploy → New deployment → Web app**
   - Execute as: `Me`
   - Who has access: `Anyone` (the desktop app has no Google login)
5. Copy the `/exec` URL and paste it into `SM_SYNC_API` in
   `Windows Application\Others\VHostingSync.vb`, then rebuild.

One URL serves both directions — the `action` parameter decides what happens:

```
GET  ?action=tables               -> list of synced tables
GET  ?action=data&table=tbl_X     -> rows of one table
POST ?action=clear&table=tbl_X    -> empty the remote copy
POST ?action=insert&table=tbl_X   -> insert the pushed rows
```

Because the URL is public, anybody who has it could overwrite the sheets. The
sheets are only a copy — the shop's SQL Server stays the source of truth — but
`SHARED_TOKEN` in `Code.gs` lets you lock it down later.

If you would rather run your own PHP/MySQL server, keep `SM_SYNC_API` pointing
at it: any backend that implements the four actions above works unchanged.

**Tables pushed:** `tbl_BillSeries`, `tbl_Brand`, `tbl_Category`,
`tbl_CusProfile`, `tbl_MultiStock`, `tbl_PurchDetail`, `tbl_PurchParticular`,
`tbl_SalDetail`, `tbl_SalParticular`, `tbl_StockDtl`, `tbl_StockModify`,
`tbl_UnitList`.

`tbl_Companyprofile` and `tbl_UserList` are deliberately **excluded** because
they hold passwords and SMS credentials.

The sync is push-first: line-item tables (`tbl_SalParticular`,
`tbl_PurchParticular`) are designed to be uploaded, not downloaded — the
Server → SQL Server direction updates one row per key, which suits the master
tables (`tbl_Category`, `tbl_Brand`, `tbl_StockDtl`, …).
