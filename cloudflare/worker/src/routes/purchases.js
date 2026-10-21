/**
 * Purchases (goods received from a supplier).
 *
 * Mirrors the sales flow, but stock goes up instead of down and the header
 * table is tbl_PurchDetail / tbl_PurchParticular.
 *
 * Purchase rates can be edited by the supplier's bill, so the client may send
 * a rate. It is validated and rounded, and the line totals are recomputed
 * server-side exactly like a sale.
 */

import { requireRole } from '../auth.js';
import { badRequest, notFound } from '../http.js';
import { all, one, batch, audit, nowIso, pagination, currentFinancialYear, nextBillNumber } from '../db.js';
import { computeSaleLine, computeBillTotals, round2 } from '../money.js';

const MAX_LINES = 500;

function billDateParts(value) {
  const date = value ? new Date(value) : new Date();
  if (Number.isNaN(date.getTime())) throw badRequest('billDate is not a valid date');

  const parts = new Intl.DateTimeFormat('en-CA', {
    timeZone: 'Asia/Kolkata',
    year: 'numeric', month: '2-digit', day: '2-digit',
    hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: false
  }).formatToParts(date);

  const get = (t) => parts.find(p => p.type === t).value;

  return {
    date: `${get('year')}-${get('month')}-${get('day')}`,
    time: `${get('hour')}:${get('minute')}:${get('second')}`
  };
}

export async function createPurchase(request, env, body, user) {
  requireRole(user, 'admin', 'manager');

  const idempotencyKey = (request.headers.get('X-Idempotency-Key') || body.idempotencyKey || '').trim();
  if (!idempotencyKey) throw badRequest('X-Idempotency-Key header is required');
  if (idempotencyKey.length > 200) throw badRequest('Idempotency key is too long');

  const prior = await one(env.DB,
    'SELECT status_code, response FROM api_idempotency WHERE key = ?', idempotencyKey);

  if (prior) {
    return { __replay: true, status: prior.status_code, body: JSON.parse(prior.response) };
  }

  if (!Array.isArray(body.items) || body.items.length === 0) {
    throw badRequest('items must be a non-empty array');
  }
  if (body.items.length > MAX_LINES) {
    throw badRequest(`A purchase cannot have more than ${MAX_LINES} lines`);
  }

  const supplierId = parseInt(body.supplierId, 10);
  if (!Number.isFinite(supplierId)) throw badRequest('supplierId is required');

  const supplier = await one(env.DB,
    "SELECT C_ID_n, C_Name_vc FROM tbl_CusProfile WHERE C_Type_vc = 'SUPPLIER' AND C_ID_n = ?", supplierId);

  if (!supplier) throw badRequest('supplierId does not exist');

  const parsed = billDateParts(body.billDate);
  const acYear = currentFinancialYear(new Date(`${parsed.date}T00:00:00Z`));

  const codes = [...new Set(body.items.map(i => parseInt(i.stockCode, 10)))];
  if (codes.some(c => !Number.isFinite(c))) throw badRequest('Every item needs a numeric stockCode');

  const placeholders = codes.map(() => '?').join(',');
  const masters = await all(env.DB,
    `SELECT St_StockID_n AS stockCode, St_ProName_vc AS name, St_Unit_vc AS unit,
            St_CGSTPer_n AS cgstPercent, St_SGSTPer_n AS sgstPercent, St_IGSTPer_n AS igstPercent
     FROM tbl_StockDtl WHERE St_StockID_n IN (${placeholders})`, ...codes);

  const masterMap = new Map(masters.map(m => [Number(m.stockCode), m]));

  const lines = body.items.map((item, index) => {
    const stockCode = parseInt(item.stockCode, 10);
    const master = masterMap.get(stockCode);

    if (!master) throw badRequest(`Item ${stockCode} does not exist`, [{ index }]);

    const computed = computeSaleLine({
      qty: item.qty,
      rate: item.rate,
      discount: item.discount || 0,
      cgstPercent: Number(master.cgstPercent) || 0,
      sgstPercent: Number(master.sgstPercent) || 0,
      igstPercent: Number(master.igstPercent) || 0
    });

    return {
      stockCode,
      name: master.name,
      unit: master.unit,
      mrp: round2(item.mrp || 0),
      saleRate: round2(item.saleRate || 0),
      ...computed
    };
  });

  const totals = computeBillTotals(lines, { billDiscount: body.billDiscount || 0 });

  const bill = await nextBillNumber(env.DB, String(body.saveType || 'PURCHASE').toUpperCase(), acYear);
  const now = nowIso();

  const statements = [
    env.DB.prepare(
      `INSERT INTO tbl_PurchDetail
         (Sa_EntDate_d, Sa_SaveType_vc, Sa_BillDate_d, Sa_BillTime_vc, Sa_InvNo_vc, Sa_BillNo_n,
          Sa_BType_vc, Sa_CusID_n, Sa_CusName_vc, Sa_GSTTotal_n, Sa_TotalAmt_n, Sa_RoType_vc,
          Sa_RoAmt_n, Sa_FinalTotal_n, Sa_AcYear_vc, Sa_UserID_vc)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`
    ).bind(
      now, body.saveType || 'PURCHASE', parsed.date, parsed.time, bill.invoiceNo, bill.billNo,
      'PURCHASE', supplierId, supplier.C_Name_vc, totals.gst, totals.beforeRound, 'ROUND',
      totals.roundOff, totals.finalTotal, acYear, user.username
    )
  ];

  for (const line of lines) {
    statements.push(
      env.DB.prepare(
        `INSERT INTO tbl_PurchParticular
           (Sa_SaveTy_vc, Sa_BillDate_d, Sa_InvNo_vc, Sa_CusID_n, Sa_StCode_n,
            Sa_PurRate_n, Sa_MRP_n, Sa_SaleRate_n, Sa_Qty_i, Sa_FQty_i, Sa_TQty_i,
            Sa_Taxable_n, Sa_CGSTP_n, Sa_SGSTP_n, Sa_IGSTP_n,
            Sa_CGSTAmt_n, Sa_SGSTAmt_n, Sa_IGSTAmt_n, Sa_ProTotal_n,
            Sa_ProDPer_n, Sa_ProDAmt_n, Sa_NetRate_n)
         VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`
      ).bind(
        body.saveType || 'PURCHASE', parsed.date, bill.invoiceNo, supplierId, line.stockCode,
        line.rate, line.mrp, line.saleRate, line.qty, line.qty, line.qty,
        line.taxable, line.cgstPercent, line.sgstPercent, line.igstPercent,
        line.cgstAmount, line.sgstAmount, line.igstAmount, line.total,
        0, line.discount, line.rate
      )
    );

    statements.push(
      env.DB.prepare(
        'UPDATE tbl_StockDtl SET St_TotalSt_i = St_TotalSt_i + ? WHERE St_StockID_n = ?'
      ).bind(line.qty, line.stockCode)
    );

    statements.push(
      env.DB.prepare(
        `UPDATE tbl_MultiStock
            SET St_CurrStock_i = St_CurrStock_i + ?,
                St_PurRate_n   = ?,
                St_MRPRate_n   = CASE WHEN ? > 0 THEN ? ELSE St_MRPRate_n END,
                St_SaleRate_n  = CASE WHEN ? > 0 THEN ? ELSE St_SaleRate_n END
          WHERE St_StockID_n = ?
            AND St_StNumber_i = (SELECT MIN(x.St_StNumber_i) FROM tbl_MultiStock x WHERE x.St_StockID_n = ?)`
      ).bind(
        line.qty, line.rate,
        line.mrp, line.mrp,
        line.saleRate, line.saleRate,
        line.stockCode, line.stockCode
      )
    );
  }

  const responseBody = {
    invoiceNo: bill.invoiceNo,
    billNo: bill.billNo,
    acYear,
    saveType: body.saveType || 'PURCHASE',
    billDate: parsed.date,
    supplier: { id: supplierId, name: supplier.C_Name_vc },
    lines: lines.map(l => ({
      stockCode: l.stockCode, name: l.name, unit: l.unit, qty: l.qty, rate: l.rate,
      discount: l.discount, taxable: l.taxable,
      cgstAmount: l.cgstAmount, sgstAmount: l.sgstAmount, igstAmount: l.igstAmount,
      total: l.total
    })),
    totals: {
      taxable: totals.taxable, gst: totals.gst, billDiscount: totals.billDiscount,
      roundOff: totals.roundOff, finalTotal: totals.finalTotal
    }
  };

  statements.push(
    env.DB.prepare(
      'INSERT INTO api_idempotency (key, endpoint, user_id, status_code, response, created_at) VALUES (?,?,?,?,?,?)'
    ).bind(idempotencyKey, 'POST /api/purchases', user.id, 201, JSON.stringify(responseBody), now)
  );

  try {
    await batch(env.DB, statements);
  } catch (err) {
    const message = String(err && err.message ? err.message : err);

    if (message.includes('UNIQUE constraint failed: api_idempotency.key')) {
      const stored = await one(env.DB, 'SELECT status_code, response FROM api_idempotency WHERE key = ?', idempotencyKey);
      if (stored) return { __replay: true, status: stored.status_code, body: JSON.parse(stored.response) };
    }

    throw err;
  }

  await audit(env.DB, {
    userId: user.id, username: user.username, action: 'purchase.create',
    entity: 'tbl_PurchDetail', entityId: bill.invoiceNo,
    detail: `${lines.length} line(s), total ${totals.finalTotal}`
  });

  return { __replay: false, status: 201, body: responseBody };
}

export async function listPurchases(request, env) {
  const url = new URL(request.url);
  const { limit, offset } = pagination(url.searchParams);

  const where = [];
  const params = [];

  const from = url.searchParams.get('from');
  const to = url.searchParams.get('to');

  if (from) { where.push('Sa_BillDate_d >= ?'); params.push(from); }
  if (to) { where.push('Sa_BillDate_d <= ?'); params.push(to); }

  const whereSql = where.length ? `WHERE ${where.join(' AND ')}` : '';

  const countRow = await one(env.DB, `SELECT COUNT(*) AS c FROM tbl_PurchDetail ${whereSql}`, ...params);
  const rows = await all(env.DB,
    `SELECT Sa_InvNo_vc AS invoiceNo, Sa_BillNo_n AS billNo, Sa_BillDate_d AS billDate,
            Sa_CusID_n AS supplierId, Sa_CusName_vc AS supplierName,
            Sa_GSTTotal_n AS gst, Sa_FinalTotal_n AS finalTotal, Sa_AcYear_vc AS acYear
     FROM tbl_PurchDetail ${whereSql}
     ORDER BY Sa_BillDate_d DESC, Sa_RecordNo_i DESC
     LIMIT ? OFFSET ?`, ...params, limit, offset);

  return { items: rows, limit, offset, total: countRow ? countRow.c : 0 };
}

export async function getPurchase(_request, env, _body, _user, ctx) {
  const invoiceNo = ctx.id;

  const header = await one(env.DB,
    `SELECT Sa_InvNo_vc AS invoiceNo, Sa_BillNo_n AS billNo, Sa_BillDate_d AS billDate,
            Sa_CusID_n AS supplierId, Sa_CusName_vc AS supplierName,
            Sa_GSTTotal_n AS gst, Sa_TotalAmt_n AS beforeRound,
            Sa_RoAmt_n AS roundOff, Sa_FinalTotal_n AS finalTotal, Sa_AcYear_vc AS acYear
     FROM tbl_PurchDetail WHERE Sa_InvNo_vc = ?`, invoiceNo);

  if (!header) throw notFound('Purchase not found');

  const lines = await all(env.DB,
    `SELECT Sa_StCode_n AS stockCode, Sa_PurRate_n AS rate, Sa_MRP_n AS mrp, Sa_Qty_i AS qty,
            Sa_Taxable_n AS taxable, Sa_CGSTP_n AS cgstPercent, Sa_SGSTP_n AS sgstPercent,
            Sa_IGSTP_n AS igstPercent, Sa_CGSTAmt_n AS cgstAmount, Sa_SGSTAmt_n AS sgstAmount,
            Sa_IGSTAmt_n AS igstAmount, Sa_ProTotal_n AS total
     FROM tbl_PurchParticular WHERE Sa_InvNo_vc = ? ORDER BY Sa_RecordNo2_i`, invoiceNo);

  return { ...header, lines };
}
