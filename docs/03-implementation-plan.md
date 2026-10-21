# 03 — Implementation plan and status

The order below is the order work was done, because each step depends on the
one before it. "Done" means verified by a command whose output is recorded —
not "code written".

## Stage 0 — Inspect before changing anything

| Task | Status | Evidence |
|---|---|---|
| Count source files and SQL usage | Done | 101 files, 94,935 lines, 43 files open SQL |
| Export the live schema | Done | `docs/schema/columns.psv` (2,497 rows), `primary_keys.psv` |
| Check declared integrity | Done | 3 primary-key columns, 0 foreign keys |
| Inventory branding | Done | 1,213 hits / 58 files, classified in `01-source-inventory.md` |

## Stage 1 — D1 schema

| Task | Status |
|---|---|
| Generator script | Done — `tools/generate-d1-schema.js` |
| Core schema (30 tables) | Done — `cloudflare/d1/migrations/0001_core_schema.sql` |
| API tables, triggers, indexes | Done — `0002_api.sql` |
| Schema test | Done — `node cloudflare/tests/schema.test.js` → `RESULT: D1 SCHEMA PASSED` |

## Stage 2 — Worker API

| Endpoint group | Status |
|---|---|
| `POST /api/auth/login`, `GET /api/auth/me`, `POST /api/auth/change-password` | Done |
| `GET/POST /api/users` (admin only) | Done |
| `GET/POST/PUT /api/products` | Done |
| `GET/POST/PUT /api/customers`, `/api/suppliers` | Done |
| `POST/GET /api/sales` (atomic bill + lines + stock + payments) | Done |
| `POST/GET /api/purchases` (atomic receive + stock increase) | Done |
| `GET /api/reports/{sales,gst,top-items,stock-valuation,dashboard}` | Done |
| `POST /api/sync/import`, `/sequences`, `GET /export`, `/reconcile` | Done |

| Cross-cutting requirement | Status |
|---|---|
| Parameterised SQL everywhere | Done — no string-concatenated values |
| No raw-SQL endpoint | Done — the only table-name input is whitelisted and column-validated |
| Password hashing | Done — PBKDF2-SHA256, 100k iterations |
| Short-lived tokens | Done — HMAC-SHA256, 8h default |
| Role authorisation | Done — enforced per handler |
| Idempotency | Done — `X-Idempotency-Key` required on billing |
| Audit logging | Done — `api_audit_log` |
| Rate limiting | Partial — in-memory per isolate. Stops a single hammering client, **not** a distributed attack. Use Cloudflare WAF rate rules for a hard limit. Documented, not overstated. |
| Secrets | No secrets in the repo; `TOKEN_SECRET` is a Wrangler secret |

| Test | Result |
|---|---|
| API suite (78 assertions, real Worker + real SQLite) | `RESULT: API TESTS PASSED` |
| Schema suite | `RESULT: D1 SCHEMA PASSED` |

## Stage 3 — Branding

| Task | Status |
|---|---|
| Assembly name, title, product, company | Done — `BharathSuperMarket.exe`, verified via `FileVersionInfo` |
| Login window title, status bar, message captions, button labels | Done |
| README | Done |
| Internal `VistawinFunctions` class | **Kept on purpose** — reason in `06-branding-and-risks.md` |
| Fixture paths (`E:\VISTAWIN\...`) and vendor URLs | **Kept** — functional, and flagged as a security risk |
| Build after branding | Done — MSBuild succeeded |

Form navigation was decoupled from `Application.ProductName` first, otherwise
renaming the product would have broken every menu click.

## Stage 4 — Migration tooling

| Task | Status |
|---|---|
| SQL Server → D1 importer | Done — `cloudflare/tools/import-from-sqlserver.ps1` (read-only on SQL Server) |
| Row-count + money reconciliation | Done — script fails loudly on drift |

## Stage 5 — NOT done (stated plainly)

| Task | Status |
|---|---|
| **Deploy to Cloudflare** | **Not done.** No Cloudflare account was used; nothing is live. |
| **Run against real D1** | **Not done.** Tests use a local SQLite shim that implements the D1 surface the Worker uses. |
| **Migrate the VB forms onto the API** | **Not done.** The desktop app still talks to SQL Server directly. |
| Offline queue / sync for the API path | Not started. Do not describe cloud mode as working offline. |
| Replace vendor endpoints | Not done — flagged as P1 in `06-branding-and-risks.md` |
| Reports beyond the five API aggregates | Not done (Crystal Reports still use SQL Server) |

## Stage 6 — Next, in dependency order

1. Deploy to a staging Worker + D1 (`docs/04-deployment-and-testing.md`).
2. Run the importer against a copy of the shop database; reconcile.
3. Add `BharathApiClient.vb` (HTTP wrapper) and a repository layer to the VB
   project — **new code, no form changes yet**.
4. Move one vertical slice at a time, starting with **login**, then **item
   master**, then **POS billing**. Keep the SQL Server path working until the
   sliced feature has been used for a full day without discrepancy.
5. Only after every slice is migrated: remove SQL Server connection strings.

Do not skip step 4. A half-migrated form that writes to two databases is worse
than an unmigrated one.
