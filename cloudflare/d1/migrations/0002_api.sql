-- =============================================================================
-- Bharath SuperMarket - API support objects
-- =============================================================================
-- Adds the tables the Worker needs that do not exist in the legacy schema:
-- credentials for the API, audit log, idempotency keys, bill-number sequences.
--
-- The legacy tbl_UserList.U_Password_vc is nvarchar(20), which cannot hold a
-- modern password hash, so credentials live in api_users instead. Each row is
-- linked to the legacy employee id (U_EmpID_i) so permissions still map.
-- =============================================================================

-- -----------------------------------------------------------------------------
-- 1. API credentials
-- -----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS api_users (
  id            INTEGER PRIMARY KEY AUTOINCREMENT,
  emp_id        INTEGER NOT NULL,           -- tbl_UserList.U_EmpID_i
  username      TEXT    NOT NULL UNIQUE,
  display_name  TEXT,
  password_hash TEXT    NOT NULL,           -- PBKDF2-SHA256, salt embedded
  role          TEXT    NOT NULL DEFAULT 'cashier',  -- admin | manager | cashier
  is_active     INTEGER NOT NULL DEFAULT 1,
  must_change   INTEGER NOT NULL DEFAULT 0, -- set after importing a legacy password
  created_at    TEXT    NOT NULL,
  updated_at    TEXT    NOT NULL,
  last_login_at TEXT
);

CREATE INDEX IF NOT EXISTS ix_api_users_emp ON api_users (emp_id);

-- -----------------------------------------------------------------------------
-- 2. Audit log
-- -----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS api_audit_log (
  id         INTEGER PRIMARY KEY AUTOINCREMENT,
  at         TEXT NOT NULL,
  user_id    INTEGER,
  username   TEXT,
  action     TEXT NOT NULL,          -- login, sale.create, product.update ...
  entity     TEXT,                   -- tbl_SalDetail, tbl_StockDtl ...
  entity_id  TEXT,
  detail     TEXT,
  ip         TEXT,
  request_id TEXT
);

CREATE INDEX IF NOT EXISTS ix_api_audit_at     ON api_audit_log (at);
CREATE INDEX IF NOT EXISTS ix_api_audit_action ON api_audit_log (action);

-- -----------------------------------------------------------------------------
-- 3. Idempotency (prevents the same bill being posted twice on a retry)
-- -----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS api_idempotency (
  key          TEXT PRIMARY KEY,
  endpoint     TEXT NOT NULL,
  user_id      INTEGER,
  status_code  INTEGER NOT NULL,
  response     TEXT NOT NULL,
  created_at   TEXT NOT NULL
);

CREATE INDEX IF NOT EXISTS ix_api_idem_created ON api_idempotency (created_at);

-- -----------------------------------------------------------------------------
-- 4. Bill number sequences
-- -----------------------------------------------------------------------------
-- Mirrors tbl_BillSeries (series prefix + start number) with a race-safe
-- counter. next_no is the number that will be handed out next.
CREATE TABLE IF NOT EXISTS api_bill_sequence (
  bill_type TEXT    NOT NULL,          -- INVOICE, SALES RETURN, PURCHASE ...
  ac_year   TEXT    NOT NULL,
  series    TEXT    NOT NULL,          -- SM, SR, PM ...
  next_no   INTEGER NOT NULL,
  PRIMARY KEY (bill_type, ac_year)
);

-- -----------------------------------------------------------------------------
-- 4b. Sale payments
-- -----------------------------------------------------------------------------
-- The legacy POS did not persist the cash/card/UPI split anywhere (Sa_ComType_vc
-- was written as an empty string), so there is nothing to migrate. This table
-- adds the capability without touching the legacy billing tables.
CREATE TABLE IF NOT EXISTS api_sale_payment (
  id         INTEGER PRIMARY KEY AUTOINCREMENT,
  invoice_no TEXT    NOT NULL,
  mode       TEXT    NOT NULL,          -- CASH | CARD | UPI | CREDIT
  amount     REAL    NOT NULL,
  reference  TEXT,
  created_at TEXT    NOT NULL
);

CREATE INDEX IF NOT EXISTS ix_api_sale_payment_inv ON api_sale_payment (invoice_no);

-- -----------------------------------------------------------------------------
-- 5. Stock guards
-- -----------------------------------------------------------------------------
-- D1 batches are atomic, so making an over-sell abort the statement rolls the
-- whole bill back. This is what enforces stock server-side.
CREATE TRIGGER IF NOT EXISTS trg_multistock_non_negative
BEFORE UPDATE OF St_CurrStock_i ON tbl_MultiStock
WHEN NEW.St_CurrStock_i < 0
BEGIN
  SELECT RAISE(ABORT, 'insufficient stock: tbl_MultiStock.St_CurrStock_i would go negative');
END;

CREATE TRIGGER IF NOT EXISTS trg_stockdtl_non_negative
BEFORE UPDATE OF St_TotalSt_i ON tbl_StockDtl
WHEN NEW.St_TotalSt_i < 0
BEGIN
  SELECT RAISE(ABORT, 'insufficient stock: tbl_StockDtl.St_TotalSt_i would go negative');
END;

-- -----------------------------------------------------------------------------
-- 6. Lookup indexes used by the API hot paths
-- -----------------------------------------------------------------------------
CREATE INDEX IF NOT EXISTS ix_salpart_stock  ON tbl_SalParticular (Sa_StCode_n);
CREATE INDEX IF NOT EXISTS ix_multistock_bar ON tbl_MultiStock (St_Barcode_vc);
CREATE INDEX IF NOT EXISTS ix_cusprofile_mob ON tbl_CusProfile (C_Mobile_vc);
CREATE INDEX IF NOT EXISTS ix_api_users_name ON api_users (username);
