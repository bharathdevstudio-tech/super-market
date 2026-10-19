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
```

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
