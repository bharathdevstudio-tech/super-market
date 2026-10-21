/**
 * Database helpers for D1.
 *
 * Everything is parameterised - no SQL string interpolation with user input.
 * Table/column names that must be dynamic are validated against a whitelist
 * before they get anywhere near a query.
 */

import { badRequest } from './http.js';

export const nowIso = () => new Date().toISOString();

/** Run a query returning many rows. */
export async function all(db, sql, ...params) {
  const result = await db.prepare(sql).bind(...params).all();
  return result.results || [];
}

/** Run a query returning the first row or null. */
export async function one(db, sql, ...params) {
  return await db.prepare(sql).bind(...params).first();
}

/** Run a write statement, returning { changes, lastRowId }. */
export async function run(db, sql, ...params) {
  const result = await db.prepare(sql).bind(...params).run();
  return {
    changes: result.meta ? result.meta.changes : 0,
    lastRowId: result.meta ? result.meta.last_row_id : null
  };
}

/** Execute several statements atomically. D1 batches are all-or-nothing. */
export async function batch(db, statements) {
  if (!Array.isArray(statements) || statements.length === 0) return [];
  return await db.batch(statements);
}

/**
 * Builds a "SET a = ?, b = ?, ..." fragment from a whitelist.
 * Returns the fragment and the ordered values.
 */
export function buildUpdate(fields, allowed, prefix = '') {
  const sets = [];
  const values = [];

  for (const [key, value] of Object.entries(fields)) {
    if (!allowed.includes(key)) {
      throw badRequest(`Field "${key}" cannot be updated`);
    }
    if (value === undefined) continue;

    sets.push(`${key} = ?`);
    values.push(value);
  }

  if (sets.length === 0) throw badRequest('No updatable fields supplied');

  return { fragment: prefix + sets.join(', '), values };
}

/**
 * Pagination with hard caps so a client cannot ask for the whole table.
 */
export function pagination(query, { defaultLimit = 50, maxLimit = 200 } = {}) {
  let limit = parseInt(query.get('limit') || String(defaultLimit), 10);
  let offset = parseInt(query.get('offset') || '0', 10);

  if (!Number.isFinite(limit) || limit < 1) limit = defaultLimit;
  if (!Number.isFinite(offset) || offset < 0) offset = 0;

  return { limit: Math.min(limit, maxLimit), offset };
}

/**
 * Financial year string used by the legacy bill numbering, e.g. "2026-2027".
 * The desktop uses April-March; keep that, not the calendar year.
 */
export function currentFinancialYear(date = new Date()) {
  const year = date.getUTCFullYear();
  const month = date.getUTCMonth() + 1; // 1-12
  const startYear = month >= 4 ? year : year - 1;

  return `${startYear}-${startYear + 1}`;
}

/**
 * Allocates the next bill number, race-safe.
 *
 * Mirrors VSuperMarket.NextBillNumber: series prefix from tbl_BillSeries plus
 * a per (bill type, financial year) counter. The UPDATE...RETURNING makes two
 * concurrent terminals get different numbers.
 */
export async function nextBillNumber(db, billType, acYear) {
  const sequence = await db
    .prepare('SELECT series, next_no FROM api_bill_sequence WHERE bill_type = ? AND ac_year = ?')
    .bind(billType, acYear)
    .first();

  if (!sequence) {
    // Fall back to the legacy series table so a migration that did not seed
    // api_bill_sequence still works.
    const legacy = await db
      .prepare('SELECT B_BillSeries_vc AS series, B_StartNo_i AS startNo FROM tbl_BillSeries WHERE B_BillType_vc = ? AND B_AcYear_vc = ? LIMIT 1')
      .bind(billType, acYear)
      .first();

    if (!legacy) {
      throw badRequest(`No bill series configured for ${billType} in ${acYear}`);
    }

    await db
      .prepare('INSERT OR IGNORE INTO api_bill_sequence (bill_type, ac_year, series, next_no) VALUES (?,?,?,?)')
      .bind(billType, acYear, legacy.series, legacy.startNo || 1)
      .run();

    return await nextBillNumber(db, billType, acYear);
  }

  const updated = await db
    .prepare('UPDATE api_bill_sequence SET next_no = next_no + 1 WHERE bill_type = ? AND ac_year = ? RETURNING next_no - 1 AS allocated')
    .bind(billType, acYear)
    .first();

  const number = updated.allocated;

  return {
    series: sequence.series,
    billNo: number,
    invoiceNo: `${sequence.series}${number}`,
    acYear
  };
}

/** Writes an audit row. Never throw from here - auditing must not break a sale. */
export async function audit(db, { userId = null, username = null, action, entity = null, entityId = null, detail = null, ip = null, requestId = null }) {
  try {
    await db
      .prepare('INSERT INTO api_audit_log (at, user_id, username, action, entity, entity_id, detail, ip, request_id) VALUES (?,?,?,?,?,?,?,?,?)')
      .bind(nowIso(), userId, username, action, entity, entityId, detail, ip, requestId)
      .run();
  } catch (err) {
    console.error(JSON.stringify({ level: 'warn', message: 'audit write failed', detail: String(err) }));
  }
}
