# 06 — Branding decisions and risks

## What was rebranded

| Surface | Before | After | How verified |
|---|---|---|---|
| Assembly / executable | `VISTAWIN.exe` | `BharathSuperMarket.exe` | MSBuild output path |
| File description | `WindowsApplication` | `Bharath SuperMarket` | `FileVersionInfo` |
| Product name | `WindowsApplication` | `Bharath SuperMarket` | `FileVersionInfo` |
| Company | *(empty)* | `Bharath SuperMarket` | `FileVersionInfo` |
| Login window title | `Log In` | `Bharath SuperMarket - Log In` | build + designer |
| Status bar | `Website : vistawin.online` | `Bharath SuperMarket` | build |
| Download message caption | `VISTAWIN` | `Bharath SuperMarket` | build |
| File-manager window label | `... vistawin.store` | `Bharath SuperMarket` | build |
| Backup button | `Send to Vistawin Server` | `Backup / Upload` | designer + runtime |
| Folder error message | `Unable to create VISTAWIN folders.` | `Unable to create the application data folders.` | build |
| README | `VISTAWIN — SuperMarket App` | `Bharath SuperMarket` | read |

The rebuild succeeded and the executable metadata was read back from the
produced binary, not assumed.

## Why the internal class name `VistawinFunctions` was kept

It appears 1,079 times across 58 files. Renaming it is mechanical, but it was
deliberately not done, for one reason:

**Four files are marked `skip-worktree`** — `FrmDBBackup.vb`, `FrmDBChange.vb`,
`FrmMiniLogIn.vb`, `FrmSqlQueryUpdate.vb`. Their committed copies contain
**placeholder** credentials; the working copies on this machine contain the
shop's real ones. That is why they are excluded from normal staging.

If the class were renamed, those four files would need edits too. The edits
would land in the working copies only. The committed copies would still call
`VistawinFunctions`, so **a fresh clone would not compile**.

The alternatives were worse: committing the real credential files (forbidden),
or hand-editing the staged blobs of four sensitive files (risk of leaking
secrets for a cosmetic gain).

`VistawinFunctions` is an internal type name. It is not shown to any user. The
name that matters — the executable, window titles, captions, metadata — is
branded.

**If it should still be renamed later**, the safe order is:

1. Replace the real credentials in the four files with placeholders.
2. `git update-index --no-skip-worktree` on each.
3. Rename `VistawinFunctions` → `BharathFunctions` everywhere.
4. Commit the placeholder versions.
5. Re-apply the real credentials locally and re-enable `skip-worktree`.

## Kept because they are functional, not branding

| Item | Count | Why kept |
|---|---|---|
| `E:\VISTAWIN\...` folder paths | ~20 | These point at where the app already stores icons, backups and scanned images. Changing the string would make the app look in a folder that does not exist and break image loading on existing installs. |
| `vistawin1` local service URL | 2 | Address of a **local** WhatsApp helper on port 8080, not a brand |
| `VistawinSolution1` | ~30 | A `My.Settings` key; renaming it silently discards saved settings |

If the folders are ever moved, the change must be a **migration** (copy data,
then update the setting), not a string replacement.

## Risks

### P1 — The backup form uploads the database to the previous vendor

`FrmDBBackup.vb` uploads the database to
`https://vistawin.store/ClientSQLDB/upload_api.php`.

That is a third party this shop does not control. If pressed, it would send
customer names, phone numbers, bills and stock data to someone else's server.

**Action required before production:** either remove the upload path, or point it
at an endpoint the shop controls. The button label was changed to
`Backup / Upload`, but **the endpoint was not changed**, because replacing it
with a placeholder would silently break the feature instead of failing visibly.
This is the highest-priority open item.

### P1 — Other vendor endpoints

Also present and still pointing at `vistawin.store` / `vistawin.online`:
application update checks, a SQL helper, and a WhatsApp relay. Same
consideration: they are the old vendor's infrastructure.

### P2 — No foreign keys anywhere

0 foreign keys, 3 primary-key columns in the entire 144-table schema. The
application has always been responsible for consistency. The API enforces it at
the boundary (validation, transactions, stock triggers), but **imported legacy
data may already contain orphans.** Reconciliation checks row counts and money
totals; it does not prove referential consistency. Add orphan checks before
trusting reports.

### P2 — Secrets live in the working tree

The four `skip-worktree` files hold real credentials locally. This is contained
(they are excluded from commits) but fragile: `git update-index --no-skip-worktree`
or a forced checkout would expose them. Prefer moving credentials to a config
file outside the repo.

### P3 — This was a clinic application

Patient-image paths, `HTH` prefixes and clinic-named resources remain. They are
harmless if unused, but they are confusing and some screens may still be
reachable. Not a security issue by itself; worth pruning when the unused tables
are dropped.

### P3 — `Application.ProductName` was used for form lookup

Form navigation previously resolved types as
`Application.ProductName & "." & FormName`. Renaming the product would have
broken **every menu click**. Both call sites now use
`GetType(...).Namespace`, which is the actual root namespace. Verified by a
successful build; menu navigation should be manually clicked through once
before shipping.
