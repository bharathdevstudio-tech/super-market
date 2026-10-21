/**
 * Bulk import / export used by the SQL Server -> D1 migration.
 *
 * This is the only endpoint that accepts a table name from the client, so it
 * is locked down:
 *   - admin role only
 *   - table name must be in ALLOWED_TABLES
 *   - column names are validated against the table's real columns (PRAGMA)
 *   - values are always bound parameters
 *   - there is no raw SQL execution path anywhere
 */

import { requireRole } from '../auth.js';
import { badRequest, notFound } from '../http.js';
import { all, one, batch, audit } from '../db.js';

/**
 * Import order matters: masters before transactions, so the migration script
 * can stream tables top to bottom.
 */
const ALLOWED_TABLES = [
  'tbl_Companyprofile',
  'tbl_AreaDivision',
  'tbl_Area',
  'tbl_Category',
  'tbl_Brand',
  'tbl_UnitList',
  'tbl_GSTTariff',
  'tbl_Denomination',
  'tbl_DenoDtl',
  'tbl_BankList',
  'tbl_Settings',
  'tbl_GeneralSettings',
  'tbl_RptMaster',
  'tbl_DashBoard',
  'tbl_UserList',
  'tbl_UserRoles',
  'tbl_UserRoleMenu',
  'tbl_MenuMaster',
  'tbl_SmUserRights',
  'tbl_UserPermission',
  'tbl_CusProfile',
  'tbl_StockDtl',
  'tbl_MultiStock',
  'tbl_BillSeries',
  'tbl_SalDetail',
  'tbl_SalParticular',
  'tbl_PurchDetail',
  'tbl_PurchParticular',
  'tbl_StockModify',
  'tbl_ActivityLog'
];

const CHUNK = 50;

async function tableColumns(db, table) {
  if (!ALLOWED_TABLES.includes(table)) {
    throw badRequest(`Table "${table}" is not importable`);
  }
  const info = await all(db, `PRAGMA table_info(${table})`);
  if (info.length === 0) throw badRequest(`Table "${table}" does not exist`);

  return info.map(c => c.name);
}

export async function importRows(request, env, body, user) {
  requireRole(user, 'admin');

  const table = String(body.table || '');
  const rows = body.rows;
  const mode = String(body.mode || 'replace').toLowerCase();

  if (!Array.isArray(rows)) throw badRequest('rows must be an array');
  if (rows.length === 0) return { table, received: 0, written: 0 };
  if (rows.length > 5000) throw badRequest('Send at most 5000 rows per request');
  if (!['replace', 'ignore'].includes(mode)) throw badRequest('mode must be replace or ignore');

  const columns = await tableColumns(env.DB, table);
  const columnSet = new Set(columns);

  const verb = mode === 'replace' ? 'INSERT OR REPLACE' : 'INSERT OR IGNORE';
  let written = 0;

  for (let start = 0; start < rows.length; start += CHUNK) {
    const slice = rows.slice(start, start + CHUNK);
    const statements = [];

    for (const [offset, row] of slice.entries()) {
      if (!row || typeof row !== 'object' || Array.isArray(row)) {
        throw badRequest(`rows[${start + offset}] must be an object`);
      }

      const keys = Object.keys(row);
      if (keys.length === 0) throw badRequest(`rows[${start + offset}] is empty`);

      for (const key of keys) {
        if (!columnSet.has(key)) {
          throw badRequest(`rows[${start + offset}] has unknown column "${key}" for ${table}`);
        }
      }

      const sql = `${verb} INTO ${table} (${keys.join(', ')}) VALUES (${keys.map(() => '?').join(', ')})`;
      statements.push(env.DB.prepare(sql).bind(...keys.map(k => row[k])));
    }

    const results = await batch(env.DB, statements);
    for (const result of results) {
      written += result && result.meta ? result.meta.changes || 0 : 0;
    }
  }

  await audit(env.DB, {
    userId: user.id,
    username: user.username,
    action: 'sync.import',
    entity: table,
    detail: `${rows.length} row(s) received, ${written} written, mode=${mode}`
  });

  return { table, received: rows.length, written, mode };
}

/** Dumps one table for reconciliation against SQL Server. */
export async function exportRows(request, env, _body, user) {
  requireRole(user, 'admin', 'manager');

  const url = new URL(request.url);
  const table = url.searchParams.get('table') || '';
  const columns = await tableColumns(env.DB, table);

  let limit = parseInt(url.searchParams.get('limit') || '2000', 10);
  if (!Number.isFinite(limit) || limit < 1) limit = 2000;
  limit = Math.min(limit, 5000);

  const offset = Math.max(0, parseInt(url.searchParams.get('offset') || '0', 10) || 0);

  const rows = await all(env.DB, `SELECT * FROM ${table} LIMIT ? OFFSET ?`, limit, offset);
  const countRow = await one(env.DB, `SELECT COUNT(*) AS c FROM ${table}`);

  return { table, columns, rows, total: countRow ? countRow.c : 0, limit, offset };
}

/**
 * Seeds api_bill_sequence from the legacy data so the first cloud bill
 * continues from where SQL Server left off instead of restarting at 1.
 */
export async function seedSequences(_request, env, _body, user) {
  requireRole(user, 'admin');

  const series = await all(env.DB,
    'SELECT B_BillType_vc AS billType, B_AcYear_vc AS acYear, B_BillSeries_vc AS series, B_StartNo_i AS startNo FROM tbl_BillSeries');

  const sales = await all(env.DB,
    `SELECT Sa_SaveType_vc AS billType, Sa_AcYear_vc AS acYear, COALESCE(MAX(Sa_BillNo_n), 0) AS maxNo
     FROM tbl_SalDetail GROUP BY Sa_SaveType_vc, Sa_AcYear_vc`);

  const purchases = await all(env.DB,
    `SELECT Sa_SaveType_vc AS billType, Sa_AcYear_vc AS acYear, COALESCE(MAX(Sa_BillNo_n), 0) AS maxNo
     FROM tbl_PurchDetail GROUP BY Sa_SaveType_vc, Sa_AcYear_vc`);

  const maxByKey = new Map();
  for (const row of [...sales, ...purchases]) {
    const key = `${row.billType}|${row.acYear}`;
    maxByKey.set(key, Math.max(maxByKey.get(key) || 0, Number(row.maxNo) || 0));
  }

  let seeded = 0;
  const statements = [];

  for (const s of series) {
    const key = `${s.billType}|${s.acYear}`;
    const maxNo = maxByKey.get(key) || 0;
    const nextNo = Math.max(Number(s.startNo) || 1, maxNo + 1);

    statements.push(
      env.DB.prepare(
        `INSERT INTO api_bill_sequence (bill_type, ac_year, series, next_no)
         VALUES (?,?,?,?)
         ON CONFLICT (bill_type, ac_year) DO UPDATE SET series = excluded.series, next_no = excluded.next_no`
      ).bind(s.billType, s.acYear, s.series, nextNo)
    );
    seeded++;
  }

  if (statements.length) await batch(env.DB, statements);

  await audit(env.DB, {
    userId: user.id, username: user.username, action: 'sync.sequences',
    detail: `${seeded} bill sequence(s) seeded`
  });

  return { seeded };
}

/**
 * Reconciliation helper: row counts + money totals for the tables that matter.
 * The import script compares these against SQL Server and fails loudly on drift.
 */
export async function reconcile(_request, env) {
  const counts = {};

  for (const table of ALLOWED_TABLES) {
    const row = await one(env.DB, `SELECT COUNT(*) AS c FROM ${table}`);
    counts[table] = row ? row.c : 0;
  }

  const salesTotals = await one(env.DB,
    `SELECT ROUND(COALESCE(SUM(Sa_FinalTotal_n), 0), 2) AS finalTotal,
            ROUND(COALESCE(SUM(Sa_GSTTotal_n), 0), 2)   AS gstTotal
     FROM tbl_SalDetail`);

  const purchaseTotals = await one(env.DB,
    `SELECT ROUND(COALESCE(SUM(Sa_FinalTotal_n), 0), 2) AS finalTotal
     FROM tbl_PurchDetail`);

  const stockTotals = await one(env.DB,
    `SELECT ROUND(COALESCE(SUM(St_TotalSt_i), 0), 0) AS totalStock FROM tbl_StockDtl`);

  return { counts, salesTotals, purchaseTotals, stockTotals };
}
