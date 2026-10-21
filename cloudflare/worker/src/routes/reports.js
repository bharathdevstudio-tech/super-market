/**
 * Reporting endpoints.
 *
 * These are read-only aggregates over the legacy billing tables, so the shop
 * gets the same numbers from the cloud as it did from SQL Server.
 */

import { badRequest } from '../http.js';
import { all, one } from '../db.js';
import { round2 } from '../money.js';

function dateRange(request) {
  const url = new URL(request.url);
  const from = url.searchParams.get('from');
  const to = url.searchParams.get('to');

  if (!from || !to) {
    throw badRequest('from and to are required (YYYY-MM-DD)');
  }
  if (!/^\d{4}-\d{2}-\d{2}$/.test(from) || !/^\d{4}-\d{2}-\d{2}$/.test(to)) {
    throw badRequest('from and to must be YYYY-MM-DD');
  }
  if (from > to) {
    throw badRequest('from cannot be after to');
  }

  return { from, to };
}

/** Daily sales totals. */
export async function salesSummary(request, env) {
  const { from, to } = dateRange(request);

  const rows = await all(env.DB,
    `SELECT Sa_BillDate_d AS date,
            COUNT(*)                AS bills,
            ROUND(SUM(Sa_TotalAmt_n), 2)  AS taxable,
            ROUND(SUM(Sa_GSTTotal_n), 2)  AS gst,
            ROUND(SUM(Sa_DiscountAmt_n), 2) AS discount,
            ROUND(SUM(Sa_FinalTotal_n), 2) AS total
     FROM tbl_SalDetail
     WHERE Sa_BillDate_d >= ? AND Sa_BillDate_d <= ?
     GROUP BY Sa_BillDate_d
     ORDER BY Sa_BillDate_d`, from, to);

  const totals = rows.reduce((acc, r) => ({
    bills: acc.bills + r.bills,
    taxable: round2(acc.taxable + r.taxable),
    gst: round2(acc.gst + r.gst),
    discount: round2(acc.discount + r.discount),
    total: round2(acc.total + r.total)
  }), { bills: 0, taxable: 0, gst: 0, discount: 0, total: 0 });

  return { from, to, days: rows, totals };
}

/** GST breakdown for filing. */
export async function gstSummary(request, env) {
  const { from, to } = dateRange(request);

  const rows = await all(env.DB,
    `SELECT Sa_CGSTP_n AS cgstPercent,
            Sa_SGSTP_n AS sgstPercent,
            Sa_IGSTP_n AS igstPercent,
            ROUND(SUM(Sa_Taxable_n), 2) AS taxable,
            ROUND(SUM(Sa_CGSTAmt_n), 2) AS cgst,
            ROUND(SUM(Sa_SGSTAmt_n), 2) AS sgst,
            ROUND(SUM(Sa_IGSTAmt_n), 2) AS igst,
            ROUND(SUM(Sa_ProTotal_n), 2) AS total
     FROM tbl_SalParticular
     WHERE Sa_BillDate_d >= ? AND Sa_BillDate_d <= ?
     GROUP BY Sa_CGSTP_n, Sa_SGSTP_n, Sa_IGSTP_n
     ORDER BY cgstPercent, sgstPercent, igstPercent`, from, to);

  return { from, to, slabs: rows };
}

/** Best-selling items for a period. */
export async function topItems(request, env) {
  const url = new URL(request.url);
  const { from, to } = dateRange(request);

  let limit = parseInt(url.searchParams.get('limit') || '20', 10);
  if (!Number.isFinite(limit) || limit < 1) limit = 20;
  limit = Math.min(limit, 100);

  const rows = await all(env.DB,
    `SELECT p.Sa_StCode_n AS stockCode,
            COALESCE(d.St_ProName_vc, '') AS name,
            ROUND(SUM(p.Sa_Qty_i), 3)      AS qty,
            ROUND(SUM(p.Sa_ProTotal_n), 2) AS amount
     FROM tbl_SalParticular p
     LEFT JOIN tbl_StockDtl d ON d.St_StockID_n = p.Sa_StCode_n
     WHERE p.Sa_BillDate_d >= ? AND p.Sa_BillDate_d <= ?
     GROUP BY p.Sa_StCode_n
     ORDER BY qty DESC
     LIMIT ?`, from, to, limit);

  return { from, to, items: rows };
}

/** Stock valuation (quantity x sale rate) for everything currently in stock. */
export async function stockValuation(_request, env) {
  const rows = await all(env.DB,
    `SELECT d.St_StockID_n AS stockCode,
            d.St_ProName_vc AS name,
            d.St_Unit_vc AS unit,
            d.St_TotalSt_i AS stock,
            m.St_SaleRate_n AS saleRate,
            m.St_PurRate_n AS purchaseRate,
            ROUND(d.St_TotalSt_i * COALESCE(m.St_SaleRate_n, 0), 2) AS saleValue,
            ROUND(d.St_TotalSt_i * COALESCE(m.St_PurRate_n, 0), 2) AS costValue
     FROM tbl_StockDtl d
     LEFT JOIN tbl_MultiStock m
            ON m.St_StockID_n = d.St_StockID_n
           AND m.St_StNumber_i = (
                 SELECT MIN(x.St_StNumber_i) FROM tbl_MultiStock x WHERE x.St_StockID_n = d.St_StockID_n
               )
     WHERE d.St_TotalSt_i <> 0
     ORDER BY d.St_ProName_vc`);

  const totals = rows.reduce((acc, r) => ({
    saleValue: round2(acc.saleValue + (r.saleValue || 0)),
    costValue: round2(acc.costValue + (r.costValue || 0))
  }), { saleValue: 0, costValue: 0 });

  return { items: rows, totals };
}

/** Dashboard counters for today. */
export async function dashboard(request, env) {
  const url = new URL(request.url);
  const date = url.searchParams.get('date') || new Date().toISOString().slice(0, 10);

  if (!/^\d{4}-\d{2}-\d{2}$/.test(date)) throw badRequest('date must be YYYY-MM-DD');

  const today = await one(env.DB,
    `SELECT COUNT(*) AS bills,
            ROUND(COALESCE(SUM(Sa_FinalTotal_n), 0), 2) AS total
     FROM tbl_SalDetail WHERE Sa_BillDate_d = ?`, date);

  const lowStock = await one(env.DB,
    `SELECT COUNT(*) AS c FROM tbl_StockDtl WHERE St_TotalSt_i <= 5`);

  const items = await one(env.DB,
    'SELECT COUNT(*) AS c FROM tbl_StockDtl');

  const customers = await one(env.DB,
    "SELECT COUNT(*) AS c FROM tbl_CusProfile WHERE C_Type_vc = 'CUSTOMER'");

  return {
    date,
    bills: today ? today.bills : 0,
    salesTotal: today ? today.total : 0,
    lowStockItems: lowStock ? lowStock.c : 0,
    itemCount: items ? items.c : 0,
    customerCount: customers ? customers.c : 0
  };
}
