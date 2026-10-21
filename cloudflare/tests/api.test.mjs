/**
 * End-to-end API tests.
 *
 * Runs the real Worker (cloudflare/worker/src/index.js) against a real SQLite
 * database with the real migrations applied. Nothing is mocked except D1 itself.
 *
 *   node cloudflare/tests/api.test.mjs
 */

import worker from '../worker/src/index.js';
import { hashPassword } from '../worker/src/auth.js';
import { currentFinancialYear } from '../worker/src/db.js';
import { createTestDb } from './d1-shim.mjs';

let pass = 0;
let fail = 0;

function check(name, condition, extra) {
  if (condition) {
    pass++;
    console.log('  ok   ' + name);
  } else {
    fail++;
    console.log('  FAIL ' + name + (extra === undefined ? '' : '  -> ' + JSON.stringify(extra)));
  }
}

function section(name) {
  console.log('');
  console.log('=== ' + name + ' ===');
}

async function call(env, method, path, { body, token, headers } = {}) {
  const init = { method, headers: {} };

  if (body !== undefined) {
    init.headers['Content-Type'] = 'application/json';
    init.body = JSON.stringify(body);
  }
  if (token) init.headers.Authorization = `Bearer ${token}`;
  Object.assign(init.headers, headers || {});

  const response = await worker.fetch(new Request('https://api.test' + path, init), env, {});
  const text = await response.text();

  let parsed = null;
  if (text) {
    try { parsed = JSON.parse(text); } catch { parsed = text; }
  }

  return { status: response.status, body: parsed };
}

// ---------------------------------------------------------------------------
// Setup
// ---------------------------------------------------------------------------

const db = createTestDb();
const env = { DB: db, TOKEN_SECRET: 'test-only-secret-not-real', TOKEN_TTL_SECONDS: '3600' };

const acYear = currentFinancialYear();

db.prepare(
  'INSERT INTO tbl_BillSeries (B_BillType_vc, B_AcYear_vc, B_BillSeries_vc, B_StartNo_i) VALUES (?,?,?,?)'
).run('INVOICE', acYear, 'SM', 1);

db.prepare(
  'INSERT INTO tbl_BillSeries (B_BillType_vc, B_AcYear_vc, B_BillSeries_vc, B_StartNo_i) VALUES (?,?,?,?)'
).run('PURCHASE', acYear, 'PM', 1);

const adminHash = await hashPassword('Admin@12345');
const cashierHash = await hashPassword('Cashier@12345');

db.prepare(
  `INSERT INTO api_users (emp_id, username, display_name, password_hash, role, is_active, created_at, updated_at)
   VALUES (?,?,?,?,?,1,?,?)`
).run(1, 'admin', 'Administrator', adminHash, 'admin', new Date().toISOString(), new Date().toISOString());

db.prepare(
  `INSERT INTO api_users (emp_id, username, display_name, password_hash, role, is_active, created_at, updated_at)
   VALUES (?,?,?,?,?,1,?,?)`
).run(2, 'cashier', 'Cashier', cashierHash, 'cashier', new Date().toISOString(), new Date().toISOString());

// One item: id 1, purchase 80, sale 100, MRP 120, GST 2.5 + 2.5, stock 10.
db.prepare(
  `INSERT INTO tbl_StockDtl (St_StockID_n, St_ProName_vc, St_Unit_vc, St_HSN_vc,
                             St_CGSTPer_n, St_SGSTPer_n, St_IGSTPer_n, St_TotalSt_i, St_EntDate_d)
   VALUES (?,?,?,?,?,?,?,?,?)`
).run(1, 'Test Rice 1kg', 'NOS', '1006', 2.5, 2.5, 0, 10, new Date().toISOString());

db.prepare(
  `INSERT INTO tbl_MultiStock (St_StockID_n, St_StNumber_i, St_ProName_vc, St_CurrStock_i,
                               St_PurRate_n, St_SaleRate_n, St_MRPRate_n, St_Barcode_vc, St_EDate_d)
   VALUES (?,?,?,?,?,?,?,?,?)`
).run(1, 1, 'Test Rice 1kg', 10, 80, 100, 120, '8901234567890', new Date().toISOString());

// ---------------------------------------------------------------------------
// Health
// ---------------------------------------------------------------------------

section('health');

{
  const r = await call(env, 'GET', '/api/health');
  check('GET /api/health returns 200', r.status === 200, r);
  check('health reports ok', r.body && r.body.ok === true, r.body);
  check('health reports a version', !!(r.body && r.body.version), r.body);
}

// ---------------------------------------------------------------------------
// Authentication
// ---------------------------------------------------------------------------

section('authentication');

let adminToken = null;
let cashierToken = null;
let cashierId = null;

{
  const r = await call(env, 'POST', '/api/auth/login', { body: { username: 'admin', password: 'Admin@12345' } });
  check('login with correct password returns 200', r.status === 200, r);
  check('login returns a token', !!(r.body && r.body.token), r.body);
  check('login returns the admin role', r.body && r.body.user && r.body.user.role === 'admin', r.body);
  adminToken = r.body && r.body.token;
}

{
  const r = await call(env, 'POST', '/api/auth/login', { body: { username: 'admin', password: 'wrong-password' } });
  check('wrong password returns 401', r.status === 401, r);
  check('wrong password does not leak whether the user exists',
    r.body && /invalid username or password/i.test(r.body.error.message), r.body);
}

{
  const r = await call(env, 'POST', '/api/auth/login', { body: { username: 'nobody', password: 'whatever' } });
  check('unknown user returns 401', r.status === 401, r);
}

{
  const r = await call(env, 'GET', '/api/products');
  check('protected endpoint without a token returns 401', r.status === 401, r);
}

{
  const r = await call(env, 'GET', '/api/products', { token: 'not-a-real-token' });
  check('forged token returns 401', r.status === 401, r);
}

{
  const r = await call(env, 'POST', '/api/auth/login', { body: { username: 'cashier', password: 'Cashier@12345' } });
  check('cashier can log in', r.status === 200, r);
  cashierToken = r.body && r.body.token;
  cashierId = r.body && r.body.user && r.body.user.id;
}

{
  const r = await call(env, 'GET', '/api/auth/me', { token: adminToken });
  check('GET /api/auth/me returns the current user', r.status === 200 && r.body.username === 'admin', r);
}

// ---------------------------------------------------------------------------
// Products
// ---------------------------------------------------------------------------

section('products');

{
  const r = await call(env, 'GET', '/api/products', { token: adminToken });
  check('product list returns 200', r.status === 200, r);
  check('product list contains the seeded item', r.body.items.length === 1 && r.body.items[0].name === 'Test Rice 1kg', r.body);
  check('product list exposes rate and stock', r.body.items[0].saleRate === 100 && r.body.items[0].stock === 10, r.body.items[0]);
}

{
  const r = await call(env, 'GET', '/api/products?barcode=8901234567890', { token: adminToken });
  check('barcode lookup finds the item', r.body.items.length === 1 && r.body.items[0].id === 1, r.body);
}

{
  const r = await call(env, 'POST', '/api/products', {
    token: cashierToken,
    body: { name: 'Cashier Item' }
  });
  check('cashier cannot create a product (403)', r.status === 403, r);
}

{
  const r = await call(env, 'POST', '/api/products', {
    token: adminToken,
    body: { name: 'Sugar 1kg', unit: 'KG', saleRate: 45, mrp: 50, purchaseRate: 38, stock: 25, cgstPercent: 2.5, sgstPercent: 2.5, barcode: '111222333' }
  });
  check('admin can create a product', r.status === 200 || r.status === 201, r);
  check('created product has the given stock', r.body && r.body.stock === 25, r.body);
  check('created product keeps its barcode', r.body && r.body.barcode === '111222333', r.body);
}

{
  const r = await call(env, 'POST', '/api/products', { token: adminToken, body: { name: '' } });
  check('product without a name is rejected', r.status === 400, r);
}

{
  const r = await call(env, 'POST', '/api/products', { token: adminToken, body: { name: 'Bad', cgstPercent: 500 } });
  check('GST percent above 100 is rejected', r.status === 400, r);
}

// ---------------------------------------------------------------------------
// Customers / suppliers
// ---------------------------------------------------------------------------

section('customers and suppliers');

{
  const first = await call(env, 'POST', '/api/customers', { token: adminToken, body: { name: 'Ravi', mobile: '9876543210' } });
  check('customer can be created', first.status === 200 || first.status === 201, first);
  check('first customer gets code 1', first.body && first.body.code === 1, first.body);

  const second = await call(env, 'POST', '/api/customers', { token: adminToken, body: { name: 'Meena', mobile: '9876500000' } });
  check('second customer gets code 2', second.body && second.body.code === 2, second.body);
}

{
  const supplier = await call(env, 'POST', '/api/suppliers', { token: adminToken, body: { name: 'ABC Traders', mobile: '9000000000' } });
  check('supplier can be created', supplier.status === 200 || supplier.status === 201, supplier);
  check('supplier numbering is independent of customers (code 1)', supplier.body && supplier.body.code === 1, supplier.body);
  check('supplier is stored with type SUPPLIER', supplier.body && supplier.body.type === 'SUPPLIER', supplier.body);
}

{
  const dup = await call(env, 'POST', '/api/customers', { token: adminToken, body: { name: 'Ravi', mobile: '9876543210' } });
  check('duplicate customer name+mobile is rejected', dup.status === 400, dup);
}

{
  const list = await call(env, 'GET', '/api/customers', { token: adminToken });
  check('customer list only contains customers', list.body.items.every(i => i.type === 'CUSTOMER'), list.body);
  const suppliers = await call(env, 'GET', '/api/suppliers', { token: adminToken });
  check('supplier list only contains suppliers', suppliers.body.items.every(i => i.type === 'SUPPLIER'), suppliers.body);
}

// ---------------------------------------------------------------------------
// Sales
// ---------------------------------------------------------------------------

section('sales and billing');

let firstInvoice = null;

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-1' },
    body: { items: [{ stockCode: 1, qty: 3 }] }
  });

  check('sale is created', r.status === 201, r);
  check('invoice number follows the series (SM1)', r.body && r.body.invoiceNo === 'SM1', r.body);
  check('taxable is rate x qty', r.body && r.body.totals.taxable === 300, r.body && r.body.totals);
  check('CGST is computed server-side (3.75)', r.body && r.body.lines[0].cgstAmount === 3.75, r.body && r.body.lines[0]);
  check('bill total is rounded (307.50 -> 308)', r.body && r.body.totals.finalTotal === 308, r.body && r.body.totals);
  check('round-off is recorded', r.body && r.body.totals.roundOff === 0.5, r.body && r.body.totals);

  firstInvoice = r.body && r.body.invoiceNo;
}

{
  const stock = db.prepare('SELECT St_CurrStock_i AS q, St_TotalSt_i AS total FROM tbl_MultiStock m JOIN tbl_StockDtl d ON d.St_StockID_n = m.St_StockID_n WHERE m.St_StockID_n = 1').get();
  check('stock line decreased 10 -> 7', stock.q === 7, stock);
  check('stock master decreased 10 -> 7', stock.total === 7, stock);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-1' },
    body: { items: [{ stockCode: 1, qty: 3 }] }
  });

  check('replaying the same idempotency key returns the same bill', r.body && r.body.invoiceNo === firstInvoice, r.body);

  const stock = db.prepare('SELECT St_CurrStock_i AS q FROM tbl_MultiStock WHERE St_StockID_n = 1').get();
  check('replay did not move stock again (still 7)', stock.q === 7, stock);

  const count = db.prepare('SELECT COUNT(*) AS c FROM tbl_SalDetail').get();
  check('replay did not create a second bill', count.c === 1, count);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-2' },
    body: { items: [{ stockCode: 1, qty: 1 }] }
  });

  check('next bill number increments (SM2)', r.body && r.body.invoiceNo === 'SM2', r.body);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-oversell' },
    body: { items: [{ stockCode: 1, qty: 9999 }] }
  });

  check('overselling is rejected with 409', r.status === 409, r);
  check('oversell response names the shortage', r.body && Array.isArray(r.body.error.details) && r.body.error.details.length > 0, r.body);

  const stock = db.prepare('SELECT St_CurrStock_i AS q FROM tbl_MultiStock WHERE St_StockID_n = 1').get();
  check('rejected sale did not move stock (still 6)', stock.q === 6, stock);

  const count = db.prepare('SELECT COUNT(*) AS c FROM tbl_SalDetail').get();
  check('rejected sale did not create a bill', count.c === 2, count);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-bad-qty' },
    body: { items: [{ stockCode: 1, qty: -5 }] }
  });
  check('negative quantity is rejected', r.status === 400, r);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-no-key' },
    body: { items: [] }
  });
  check('empty item list is rejected', r.status === 400, r);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    body: { items: [{ stockCode: 1, qty: 1 }] }
  });
  check('sale without an idempotency key is rejected', r.status === 400, r);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-payments' },
    body: {
      items: [{ stockCode: 1, qty: 1 }],
      payments: [{ mode: 'CASH', amount: 100 }, { mode: 'UPI', amount: 3 }]
    }
  });

  check('split payment that matches the total is accepted', r.status === 201, r);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-bad-payment' },
    body: { items: [{ stockCode: 1, qty: 1 }], payments: [{ mode: 'CASH', amount: 1 }] }
  });

  check('payment total that does not match is rejected', r.status === 400, r);
}

{
  const r = await call(env, 'POST', '/api/sales', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-sale-bad-mode' },
    body: { items: [{ stockCode: 1, qty: 1 }], payments: [{ mode: 'BITCOIN', amount: 103 }] }
  });

  check('unknown payment mode is rejected', r.status === 400, r);
}

{
  const r = await call(env, 'GET', `/api/sales/${firstInvoice}`, { token: adminToken });
  check('GET /api/sales/:id returns the bill', r.status === 200 && r.body.invoiceNo === firstInvoice, r);
  check('bill has its lines', Array.isArray(r.body.lines) && r.body.lines.length === 1, r.body);
}

// ---------------------------------------------------------------------------
// Stock guard
// ---------------------------------------------------------------------------

section('stock integrity');

{
  const before = db.prepare('SELECT St_CurrStock_i AS q FROM tbl_MultiStock WHERE St_StockID_n = 1').get().q;

  let threw = false;
  try {
    db.prepare('UPDATE tbl_MultiStock SET St_CurrStock_i = -1 WHERE St_StockID_n = 1').run();
  } catch (err) {
    threw = /insufficient stock/.test(String(err.message));
  }
  check('database trigger blocks negative stock', threw);

  const after = db.prepare('SELECT St_CurrStock_i AS q FROM tbl_MultiStock WHERE St_StockID_n = 1').get().q;
  check('stock is unchanged after the blocked update', after === before, { before, after });
}

// ---------------------------------------------------------------------------
// Purchases
// ---------------------------------------------------------------------------

section('purchases');

{
  const before = db.prepare('SELECT St_CurrStock_i AS q FROM tbl_MultiStock WHERE St_StockID_n = 1').get().q;

  const r = await call(env, 'POST', '/api/purchases', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-purchase-1' },
    body: { supplierId: 1, items: [{ stockCode: 1, qty: 20, rate: 82, mrp: 120, saleRate: 100 }] }
  });

  check('purchase is created', r.status === 201, r);
  check('purchase uses the PM series', r.body && r.body.invoiceNo === 'PM1', r.body);

  const after = db.prepare('SELECT St_CurrStock_i AS q FROM tbl_MultiStock WHERE St_StockID_n = 1').get().q;
  check('purchase increased stock by 20', after === before + 20, { before, after });
}

{
  const r = await call(env, 'POST', '/api/purchases', {
    token: adminToken,
    headers: { 'X-Idempotency-Key': 'test-purchase-2' },
    body: { supplierId: 999, items: [{ stockCode: 1, qty: 1, rate: 82 }] }
  });

  check('purchase with an unknown supplier is rejected', r.status === 400, r);
}

// ---------------------------------------------------------------------------
// Reports
// ---------------------------------------------------------------------------

section('reports');

{
  const r = await call(env, 'GET', '/api/reports/sales?from=2020-01-01&to=2099-12-31', { token: adminToken });
  check('sales report returns 200', r.status === 200, r);
  check('sales report totals the bills', r.body.totals && r.body.totals.bills >= 3, r.body.totals);
}

{
  const r = await call(env, 'GET', '/api/reports/sales', { token: adminToken });
  check('sales report requires a date range', r.status === 400, r);
}

{
  const r = await call(env, 'GET', '/api/reports/gst?from=2020-01-01&to=2099-12-31', { token: adminToken });
  check('GST report returns slabs', r.status === 200 && Array.isArray(r.body.slabs) && r.body.slabs.length > 0, r.body);
}

{
  const r = await call(env, 'GET', '/api/reports/dashboard', { token: adminToken });
  check('dashboard returns counters', r.status === 200 && typeof r.body.billCount !== 'undefined' || typeof r.body.bills !== 'undefined', r.body);
}

// ---------------------------------------------------------------------------
// Sync (import)
// ---------------------------------------------------------------------------

section('sync');

{
  const r = await call(env, 'POST', '/api/sync/import', {
    token: adminToken,
    body: { table: 'tbl_Category', rows: [{ Category_Code_n: 1, Category_Name_vc: 'Grocery' }] }
  });

  check('admin can import rows', r.status === 200 && r.body.written === 1, r);
}

{
  const r = await call(env, 'POST', '/api/sync/import', {
    token: adminToken,
    body: { table: 'tbl_Category', rows: [{ Category_Code_n: 2, Category_Name_vc: 'Snacks' }] }
  });
  check('second import works', r.status === 200 && r.body.written === 1, r);

  const again = await call(env, 'POST', '/api/sync/import', {
    token: adminToken,
    body: { table: 'tbl_Category', rows: [{ Category_Code_n: 2, Category_Name_vc: 'Snacks' }] }
  });
  check('re-import is idempotent (no duplicate)', again.status === 200, again);

  const count = db.prepare('SELECT COUNT(*) AS c FROM tbl_Category').get();
  check('category count is still 2', count.c === 2, count);
}

{
  const r = await call(env, 'POST', '/api/sync/import', {
    token: adminToken,
    body: { table: 'sqlite_master', rows: [{ name: 'x' }] }
  });
  check('unknown table is rejected', r.status === 400, r);
}

{
  const r = await call(env, 'POST', '/api/sync/import', {
    token: cashierToken,
    body: { table: 'tbl_Category', rows: [{ Category_Code_n: 9, Category_Name_vc: 'Nope' }] }
  });
  check('non-admin cannot import', r.status === 403, r);
}

{
  const r = await call(env, 'POST', '/api/sync/import', {
    token: adminToken,
    body: { table: 'tbl_Category', rows: [{ Category_Code_n: 3, NotAColumn: 'x' }] }
  });
  check('unknown column is rejected', r.status === 400, r);
}

{
  const r = await call(env, 'POST', '/api/sync/sequences', { token: adminToken });
  check('bill sequences can be seeded from legacy data', r.status === 200 && r.body.seeded >= 2, r);

  const seq = db.prepare("SELECT next_no FROM api_bill_sequence WHERE bill_type = 'INVOICE' AND ac_year = ?").get(acYear);
  const maxBill = db.prepare('SELECT MAX(Sa_BillNo_n) AS n FROM tbl_SalDetail').get().n;
  check('invoice sequence continues after the highest bill', seq && seq.next_no === maxBill + 1, { seq, maxBill });
}

{
  const r = await call(env, 'GET', '/api/sync/reconcile', { token: adminToken });
  check('reconcile returns row counts', r.status === 200 && r.body.counts && r.body.counts.tbl_SalDetail >= 3, r.body);
  check('reconcile returns sales totals', r.body.salesTotals && typeof r.body.salesTotals.finalTotal === 'number', r.body);
}

// ---------------------------------------------------------------------------
// Summary
// ---------------------------------------------------------------------------

console.log('');
console.log(JSON.stringify({ passed: pass, failed: fail }));
console.log(fail === 0 ? 'RESULT: API TESTS PASSED' : `RESULT: ${fail} FAILURE(S)`);

db.close();
process.exit(fail === 0 ? 0 : 1);
