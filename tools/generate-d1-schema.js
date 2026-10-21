/**
 * Generates Cloudflare D1 (SQLite) DDL from the exported SQL Server catalog.
 *
 * Input : docs/schema/columns.psv  (TABLE|ORD|COLUMN|TYPE|LEN|PREC|SCALE|NULLABLE|IDENTITY)
 *         docs/schema/primary_keys.psv (TABLE|COLUMN|ORD)
 * Output: cloudflare/d1/migrations/0001_core_schema.sql
 *
 * Only tables in CORE_TABLES are emitted in this first migration. The full
 * catalog is kept in docs/schema/ so later migrations can add the rest
 * without re-reading SQL Server.
 */
const fs = require('fs');
const path = require('path');

const ROOT = 'E:/Bharath/SuperMarket App';

const CORE_TABLES = [
  'tbl_Area',
  'tbl_AreaDivision',
  'tbl_BankList',
  'tbl_BillSeries',
  'tbl_Brand',
  'tbl_Category',
  'tbl_Companyprofile',
  'tbl_CusProfile',
  'tbl_DashBoard',
  'tbl_DenoDtl',
  'tbl_Denomination',
  'tbl_GSTTariff',
  'tbl_GeneralSettings',
  'tbl_MenuMaster',
  'tbl_MultiStock',
  'tbl_PurchDetail',
  'tbl_PurchParticular',
  'tbl_RptMaster',
  'tbl_SalDetail',
  'tbl_SalParticular',
  'tbl_Settings',
  'tbl_SmUserRights',
  'tbl_StockDtl',
  'tbl_StockModify',
  'tbl_UnitList',
  'tbl_UserPermission',
  'tbl_UserRoleMenu',
  'tbl_UserRoles',
  'tbl_UserList',
  'tbl_ActivityLog'
];

// Columns the application already treats as the natural key. These are used
// when a table has neither a declared PRIMARY KEY nor an identity column.
// Derived from actual usage (VHostingSync.GetKeyColumn and the forms), not guessed.
const BUSINESS_KEYS = {
  tbl_Area: 'Area_Code_n',
  tbl_Brand: 'Brand_Code_n',
  tbl_Category: 'Category_Code_n',
  tbl_Companyprofile: 'Cmp_ID_i',
  tbl_Denomination: 'Deno_Code_n'
};

function readPsv(file) {
  const text = fs.readFileSync(path.join(ROOT, file), 'utf8').replace(/^\uFEFF/, '');
  return text.split(/\r?\n/).filter(l => l.trim() !== '').map(l => l.split('|'));
}

function sqliteType(sqlType) {
  switch (sqlType.toLowerCase()) {
    case 'int': case 'bigint': case 'smallint': case 'tinyint': case 'bit':
      return 'INTEGER';
    case 'numeric': case 'decimal': case 'money': case 'smallmoney':
    case 'float': case 'real':
      return 'REAL';
    case 'datetime': case 'datetime2': case 'smalldatetime': case 'date':
      return 'TEXT';
    case 'image': case 'varbinary': case 'binary': case 'timestamp':
      return 'BLOB';
    case 'uniqueidentifier':
      return 'TEXT';
    default:
      return 'TEXT';
  }
}

// SQL Server default expressions that also make sense in SQLite.
function sqliteDefault(col) {
  const name = col.name.toLowerCase();
  if (col.type === 'bit') return '0';
  if (name.endsWith('_i') || name.endsWith('_n') || name.endsWith('_d')) {
    // leave money/date defaults alone unless they are obvious counters
  }
  return null;
}

const columns = readPsv('docs/schema/columns.psv');
const pks = readPsv('docs/schema/primary_keys.psv');

const pkMap = new Map();
for (const r of pks) {
  if (!pkMap.has(r[0])) pkMap.set(r[0], []);
  pkMap.get(r[0]).push(r[1]);
}

const byTable = new Map();
for (const r of columns) {
  const [table, ord, name, type, len, prec, scale, nullable, identity] = r;
  if (!CORE_TABLES.includes(table)) continue;
  if (!byTable.has(table)) byTable.set(table, []);
  byTable.get(table).push({ ord: +ord, name, type, len, prec, scale, nullable, identity: identity === '1' });
}

const missing = CORE_TABLES.filter(t => !byTable.has(t));
if (missing.length) {
  console.error('ERROR: these CORE_TABLES do not exist in the exported catalog: ' + missing.join(', '));
  process.exit(1);
}

const out = [];
out.push('-- Generated from the live SQL Server catalog by tools/generate-d1-schema.js');
out.push('-- Source: docs/schema/columns.psv (do not edit by hand; re-run the generator)');
out.push('--');
out.push('-- Type mapping:');
out.push('--   int/bigint/smallint/tinyint/bit  -> INTEGER');
out.push('--   numeric/decimal/money/float      -> REAL   (values rounded to 2dp at the API boundary)');
out.push('--   datetime/date                    -> TEXT   (ISO-8601 UTC)');
out.push('--   image/varbinary                  -> BLOB');
out.push('--   everything else                  -> TEXT');
out.push('');
out.push('PRAGMA foreign_keys = ON;');
out.push('');

for (const table of CORE_TABLES) {
  const cols = byTable.get(table).sort((a, b) => a.ord - b.ord);
  const declared = pkMap.get(table) || [];
  const identity = cols.filter(c => c.identity);
  const business = BUSINESS_KEYS[table] ? [BUSINESS_KEYS[table]] : [];
  const keyCols = declared.length ? declared : (identity.length ? [identity[0].name] : business);

  out.push(`-- ${table}: ${cols.length} columns, ${identity.length} identity, effective key: ${keyCols.length ? keyCols.join(',') : 'NONE'}`);
  out.push(`CREATE TABLE IF NOT EXISTS ${table} (`);

  const lines = [];
  for (const c of cols) {
    let line = `  ${c.name} ${sqliteType(c.type)}`;
    if (keyCols.includes(c.name)) line += ' PRIMARY KEY';
    if (c.nullable === 'NO' && !keyCols.includes(c.name)) line += ' NOT NULL';
    lines.push(line);
  }
  out.push(lines.join(',\n'));
  out.push(');');
  out.push('');
}

out.push('-- Indexes for the access paths the application actually uses.');
out.push('CREATE INDEX IF NOT EXISTS ix_saldetail_date   ON tbl_SalDetail (Sa_BillDate_d);');
out.push('CREATE INDEX IF NOT EXISTS ix_saldetail_no     ON tbl_SalDetail (Sa_InvNo_vc);');
out.push('CREATE INDEX IF NOT EXISTS ix_salpart_inv      ON tbl_SalParticular (Sa_InvNo_vc);');
out.push('CREATE INDEX IF NOT EXISTS ix_purchdetail_date ON tbl_PurchDetail (Sa_BillDate_d);');
out.push('CREATE INDEX IF NOT EXISTS ix_purchpart_inv    ON tbl_PurchParticular (Sa_InvNo_vc);');
out.push('CREATE INDEX IF NOT EXISTS ix_stockdtl_code    ON tbl_StockDtl (St_StockID_n);');
out.push('CREATE INDEX IF NOT EXISTS ix_multistock_code  ON tbl_MultiStock (St_StockID_n);');
out.push('CREATE INDEX IF NOT EXISTS ix_cusprofile_name  ON tbl_CusProfile (C_Name_vc);');

const target = path.join(ROOT, 'cloudflare/d1/migrations/0001_core_schema.sql');
fs.mkdirSync(path.dirname(target), { recursive: true });
fs.writeFileSync(target, out.join('\n') + '\n', 'utf8');

console.log(`wrote ${target}`);
console.log(`tables: ${CORE_TABLES.length}`);
console.log(`columns: ${[...byTable.values()].reduce((n, c) => n + c.length, 0)}`);
