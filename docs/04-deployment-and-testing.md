# 04 — Deployment and local testing

Two separate things:

* **Local testing** — runs today on this machine, no account needed.
* **Deployment** — needs a Cloudflare account. **This has not been done yet.**
  Nothing in this repository is live.

---

## Part 1 — Local testing (works now)

Requires Node 18+. This project was tested on Node 24.

### API + schema tests

```powershell
cd "E:\Bharath\SuperMarket App"

node cloudflare/tests/schema.test.js   # -> RESULT: D1 SCHEMA PASSED
node cloudflare/tests/api.test.mjs     # -> RESULT: API TESTS PASSED
```

What the API suite actually does: it imports the **real Worker**
(`cloudflare/worker/src/index.js`), builds a real SQLite database, applies the
**real migrations**, and drives it through `fetch` requests. Only D1 itself is
substituted (`cloudflare/tests/d1-shim.mjs`). Auth, billing, stock, idempotency,
RBAC, reports and import are all exercised.

### Rebuild the D1 schema after changing the catalog

```powershell
node tools/generate-d1-schema.js
node cloudflare/tests/schema.test.js
```

The generator overwrites `cloudflare/d1/migrations/0001_core_schema.sql`. Never
hand-edit that file; edit `CORE_TABLES` / `BUSINESS_KEYS` in the generator.

### Build the desktop app

```powershell
& "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" `
  "E:\Bharath\SuperMarket App\Windows Application\Windows Application.vbproj" `
  /p:Configuration=Debug /t:Build /v:minimal /nologo
```

Output: `Windows Application\bin\Debug\BharathSuperMarket.exe`

---

## Part 2 — Deploy to Cloudflare (not done yet)

Free-plan Worker + D1. Steps are standard Wrangler 3.

### 1. Install and log in

```powershell
cd "E:\Bharath\SuperMarket App\cloudflare\worker"
npm install
npx wrangler login
```

### 2. Create the database

```powershell
npx wrangler d1 create bharath-supermarket
```

Copy the returned `database_id` into `wrangler.toml`, replacing
`REPLACE_WITH_D1_DATABASE_ID`. It is not a secret, but it is account-specific,
so it is not committed for you.

### 3. Apply the migrations

```powershell
npx wrangler d1 migrations apply bharath-supermarket --remote
```

### 4. Set the token secret

```powershell
npx wrangler secret put TOKEN_SECRET
```

Paste a long random value **at the prompt** (do not put it in a file):

```powershell
# generate one locally if you need to
[Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Max 256 }))
```

`TOKEN_SECRET` is never in the repo. Changing it invalidates every issued token,
which forcibly logs everyone out — that is the intended revocation mechanism.

### 5. Create the first admin user

There is no self-registration. Insert the first user with a hash generated
locally (never send a password through chat):

```powershell
cd "E:\Bharath\SuperMarket App"
node -e "import('./cloudflare/worker/src/auth.js').then(async m => console.log(await m.hashPassword(process.argv[1])))" "YOUR-PASSWORD-HERE"
```

Then, using the returned hash:

```powershell
npx wrangler d1 execute bharath-supermarket --remote --command `
  "INSERT INTO api_users (emp_id, username, display_name, password_hash, role, is_active, created_at, updated_at) VALUES (1,'admin','Administrator','PASTE_HASH_HERE','admin',1,datetime('now'),datetime('now'))"
```

Every later user can be created through `POST /api/users` as admin.

### 6. Deploy

```powershell
npx wrangler deploy
```

### 7. Smoke test

```powershell
curl https://<your-worker>.workers.dev/api/health
# {"ok":true,"service":"bharath-supermarket-api","version":"1.0.0",...}
```

If `ok` is false, the D1 binding or the migrations are wrong. Do not proceed.

### 8. Import the shop data

```powershell
cd "E:\Bharath\SuperMarket App"
.\cloudflare\tools\import-from-sqlserver.ps1 -ApiBase https://<your-worker>.workers.dev
```

Dry run first — it reads SQL Server and writes nothing:

```powershell
.\cloudflare\tools\import-from-sqlserver.ps1 -ApiBase https://<your-worker>.workers.dev -DryRun
```

The script is **read-only on SQL Server**. It never modifies the source.
It ends with `RECONCILED` or a list of mismatches and a non-zero exit code.

---

## Operating notes

| Topic | Reality |
|---|---|
| Internet required | Yes. The Worker path cannot bill offline. There is no offline queue. |
| Worker cold start | Requests may occasionally take ~200–500 ms |
| D1 limits (free) | 5 GB storage, 5M rows read/day, 100k rows written/day — plenty for one shop |
| Rate limiting | In-memory per isolate only; add a Cloudflare WAF rate rule for a hard limit |
| Backups | `npx wrangler d1 export bharath-supermarket --remote --output backup.sql` |
| Logs | `npx wrangler tail` |

**Do not** describe this as "Cloudflare runs the application". Cloudflare runs
the API. The WinForms executable runs on the shop's PC, exactly as before.
