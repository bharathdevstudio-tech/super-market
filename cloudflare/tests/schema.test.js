/**
 * Validates the generated D1 migration against a real SQLite engine
 * (node:sqlite, built into Node 22+).
 *
 * Run: node cloudflare/tests/schema.test.js
 */
const fs = require('fs');
const path = require('path');
const { DatabaseSync } = require('node:sqlite');

const ROOT = path.resolve(__dirname, '../..');
const MIGRATION = path.join(ROOT, 'cloudflare/d1/migrations/0001_core_schema.sql');

let fail = 0;
const ok = (m) => console.log('  ok   ' + m);
const bad = (m) => { fail++; console.log('  FAIL ' + m); };

console.log('=== schema: executes on SQLite ===');
const db = new DatabaseSync(':memory:');

try {
  db.exec(fs.readFileSync(MIGRATION, 'utf8'));
  ok('migration applied without error');
} catch (err) {
  bad('migration failed: ' + err.message);
  process.exit(1);
}

const tables = db.prepare("SELECT name FROM sqlite_master WHERE type='table' AND name GLOB 'tbl_*' ORDER BY name").all().map(r => r.name);
console.log('  tables: ' + tables.length);
if (tables.length !== 30) bad('expected 30 core tables, found ' + tables.length);
else ok('30 core tables created');

const indexes = db.prepare("SELECT name FROM sqlite_master WHERE type='index' AND name GLOB 'ix_*'").all().map(r => r.name);
if (indexes.length < 8) bad('expected at least 8 indexes, found ' + indexes.length);
else ok(indexes.length + ' indexes created');

console.log('');
console.log('=== schema: every core table has a primary key ===');
for (const t of tables) {
  const pk = db.prepare(`PRAGMA table_info(${t})`).all().filter(c => c.pk > 0);
  if (pk.length === 0) bad(`${t} has no primary key`);
}
if (!fail) ok('all ' + tables.length + ' tables have a primary key');

console.log('');
console.log('=== schema: expected columns survived the mapping ===');
const checks = [
  ['tbl_SalDetail', 'Sa_InvNo_vc'],
  ['tbl_SalDetail', 'Sa_BillDate_d'],
  ['tbl_SalParticular', 'Sa_InvNo_vc'],
  ['tbl_StockDtl', 'St_StockID_n'],
  ['tbl_MultiStock', 'St_StockID_n'],
  ['tbl_CusProfile', 'C_Name_vc'],
  ['tbl_UserList', 'U_UserName_vc'],
  ['tbl_Companyprofile', 'Cmp_Name_vc']
];
for (const [table, col] of checks) {
  const cols = db.prepare(`PRAGMA table_info(${table})`).all().map(c => c.name);
  if (!cols.includes(col)) bad(`${table}.${col} is missing`);
}
if (!fail) ok('spot-checked columns present');

console.log('');
console.log('=== schema: types mapped as documented ===');
const t1 = db.prepare("PRAGMA table_info(tbl_SalDetail)").all();
const billDate = t1.find(c => c.name === 'Sa_BillDate_d');
if (billDate.type !== 'TEXT') bad('Sa_BillDate_d should be TEXT, got ' + billDate.type);
else ok('datetime -> TEXT');

const t2 = db.prepare("PRAGMA table_info(tbl_Category)").all();
const gst = t2.find(c => c.name === 'Cat_GSTPer_n');
if (gst.type !== 'REAL') bad('Cat_GSTPer_n should be REAL, got ' + gst.type);
else ok('numeric -> REAL');

console.log('');
console.log('=== schema: primary key actually enforces uniqueness ===');
try {
  db.prepare('INSERT INTO tbl_Category (Category_Code_n, Category_Name_vc) VALUES (?,?)').run(1, 'Grocery');
  db.prepare('INSERT INTO tbl_Category (Category_Code_n, Category_Name_vc) VALUES (?,?)').run(1, 'Duplicate');
  bad('duplicate primary key was accepted');
} catch (err) {
  ok('duplicate primary key rejected (' + err.message.split('\n')[0] + ')');
}

const integrity = db.prepare('PRAGMA integrity_check').get();
const integrityValue = Object.values(integrity)[0];
if (integrityValue !== 'ok') bad('integrity_check: ' + JSON.stringify(integrity));
else ok('integrity_check ok');

db.close();

console.log('');
console.log(fail === 0 ? 'RESULT: D1 SCHEMA PASSED' : `RESULT: ${fail} FAILURE(S)`);
process.exit(fail === 0 ? 0 : 1);

