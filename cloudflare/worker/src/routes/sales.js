/**
 * Sales / POS billing.
 *
 * This is the part that must not go wrong: a bill, its lines, the stock
 * movement and the payment split all belong to one atomic unit.
 *
 * Rules preserved from the desktop POS:
 *   - bill number comes from tbl_BillSeries (series prefix + per FY counter)
 *   - invoice number is <series><billNo>
 *   - tax percentages come from the item master, never from the client
 *   - the server recomputes every line and the bill total
 *   - round-off is applied to the payable amount
 *   - stock is decremented on tbl_MultiStock and tbl_StockDtl
 *
 * Safety:
 *   - idempotency key prevents a retried POST from creating a second bill
 *   - stock triggers abort the whole batch if stock would go negative
 */

import { requireRole } from '../auth.js';
import { badRequest, conflict, notFound } from '../http.js';
import { all, one, batch, audit, nowIso, pagination, currentFinancialYear, nextBillNumber } from '../db.js';
import { computeSaleLine, computeBillTotals, round2 } from '../money.js';

const MAX_LINES = 200;
const PAYMENT_MODES = ['CASH', 'CARD', 'UPI', 'CREDIT'];

/** Format a JS date as YYYY-MM-DD and HH:MM:SS in Asia/Kolkata. */
function billDateParts(value) {
  const date = value ? new Date(value) : new Date();
  if (Number.isNaN(date.getTime())) throw badRequest('billDate is not a valid date');

  const parts = new Intl.DateTimeFormat('en-CA', {
    timeZone: 'Asia/Kolkata',
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
    hour12: false
  }).formatToParts(date);

  const get = (t) => parts.find(p => p.type === t).value;

  return {
    date: `${get('year')}-${get('month')}-${get('day')}`,
    time: `${get('hour')}:${get('minute')}:${get('second')}`
  };
}

function validateItems(raw) {
  if (!Array.isArray(raw) || raw.length === 0) {
    throw badRequest('items must be a non-empty array');
  }
  if (raw.length > MAX_LINES) {
    throw badRequest(`A bill cannot have more than ${MAX_LINES} lines`);
  }

  return raw.map((item, index) => {
    const stockCode = parseInt(item.stockCode, 10);
    if (!Number.isFinite(stockCode)) {
      throw badRequest(`items[${index}].stockCode is required`);
    }

    return {
      index,
      stockCode,
      stNumber: item.stNumber === undefined || item.stNumber === null ? null : Number(item.stNumber),
      qty: item.qty,
      rate: item.rate,
      discount: item.discount || 0,
      mrp: item.mrp
    };
  });
}

async function loadStockRows(db, codes) {
  const placeholders = codes.map(() => '?').join(',');
  const rows = await all(db,
    `SELECT d.St_StockID_n   AS stockCode,
            d.St_ProName_vc  AS name,
            d.St_Unit_vc     AS unit,
            d.St_CGSTPer_n   AS cgstPercent,
            d.St_SGSTPer_n   AS sgstPercent,
            d.St_IGSTPer_n   AS igstPercent,
            m.St_StNumber_i  AS stNumber,
            m.St_CurrStock_i AS stock,
            m.St_SaleRate_n  AS saleRate,
            m.St_MRPRate_n   AS mrp
     FROM tbl_StockDtl d
     JOIN tbl_MultiStock m ON m.St_StockID_n = d.St_StockID_n
     WHERE d.St_StockID_n IN (${placeholders})
     ORDER BY m.St_StNumber_i`,
    ...codes);

  const byCode = new Map();
  for (const row of rows) {
    if (!byCode.has(row.stockCode)) byCode.set(row.stockCode, []);
    byCode.get(row.stockCode).push(row);
  }

  return byCode;
}

/** Picks the stock line to sell from: explicit stNumber, else the first line. */
function pickStockLine(lines, requested) {
  if (!lines || lines.length === 0) return null;
  if (requested === null) return lines[0];

  return lines.find(l => Number(l.stNumber) === Number(requested)) || null;
}

export async function createSale(request, env, body, user) {
  requireRole(user, 'admin', 'manager', 'cashier');

  const idempotencyKey = (request.headers.get('X-Idempotency-Key') || body.idempotencyKey || '').trim();

  if (!idempotencyKey) {
    throw badRequest('X-Idempotency-Key header is required');
  }
  if (idempotencyKey.length > 200) {
    throw badRequest('Idempotency key is too long');
  }

  // Replay: return exactly what the first request produced.
  const prior = await one(env.DB,
    'SELECT status_code, response FROM api_idempotency WHERE key = ?', idempotencyKey);

  if (prior) {
    return { __replay: true, status: prior.status_code, body: JSON.parse(prior.response) };
  }

  const items = validateItems(body.items);
  const parsed = billDateParts(body.billDate);
  const acYear = currentFinancialYear(new Date(`${parsed.date}T00:00:00Z`));

  const stockCodes = [...new Set(items.map(i => i.stockCode))];
  const stockMap = await loadStockRows(env.DB, stockCodes);

  // Build the lines using server-side rates and tax.
  const lines = [];
  const shortages = [];

  for (const item of items) {
    const stockLines = stockMap.get(item.stockCode);
    const stock = pickStockLine(stockLines, item.stNumber);

    if (!stock) {
      throw badRequest(`Item ${item.stockCode} does not exist`, [{ index: item.index, stockCode: item.stockCode }]);
    }

    const rate = item.rate === undefined || item.rate === null ? Number(stock.saleRate) || 0 : item.rate;

    const computed = computeSaleLine({
      qty: item.qty,
      rate,
      discount: item.discount,
      cgstPercent: Number(stock.cgstPercent) || 0,
      sgstPercent: Number(stock.sgstPercent) || 0,
      igstPercent: Number(stock.igstPercent) || 0
    });

    if (Number(stock.stock) < computed.qty) {
      shortages.push({
        index: item.index,
        stockCode: item.stockCode,
        name: stock.name,
        requested: computed.qty,
        available: Number(stock.stock)
      });
    }

    lines.push({
      stockCode: item.stockCode,
      stNumber: Number(stock.stNumber),
      name: stock.name,
      unit: stock.unit,
      mrp: item.mrp === undefined ? Number(stock.mrp) || 0 : item.mrp,
      ...computed
    });
  }

  if (shortages.length) {
    throw conflict('Insufficient stock for one or more items', shortages);
  }

  const totals = computeBillTotals(lines, { billDiscount: body.billDiscount || 0 });

  // Payments are optional (walk-in cash sale). If supplied they must add up.
  const payments = Array.isArray(body.payments) ? body.payments : [];
  let paymentTotal = 0;

  for (const [index, payment] of payments.entries()) {
    const mode = String(payment.mode || '').toUpperCase();
    if (!PAYMENT_MODES.includes(mode)) {
      throw badRequest(`payments[${index}].mode must be one of ${PAYMENT_MODES.join(', ')}`);
    }
    const amount = round2(payment.amount);
    if (!Number.isFinite(amount) || amount <= 0) {
      throw badRequest(`payments[${index}].amount must be greater than 0`);
    }
    paymentTotal = round2(paymentTotal + amount);
  }

  if (payments.length && paymentTotal !== totals.finalTotal) {
    throw badRequest(
      `Payments total ${paymentTotal} does not match the bill total ${totals.finalTotal}`
    );
  }

  // Customer: optional. CASH walk-in is stored as id 0 / name "CASH" like the POS.
  let customerId = 0;
  let customerName = 'CASH';

  if (body.customerId !== undefined && body.customerId !== null && body.customerId !== '') {
    customerId = parseInt(body.customerId, 10);
    const customer = await one(env.DB,
      "SELECT C_ID_n, C_Name_vc FROM tbl_CusProfile WHERE C_Type_vc = 'CUSTOMER' AND C_ID_n = ?", customerId);

    if (!customer) throw badRequest('customerId does not exist');
    customerName = customer.C_Name_vc;
  }

  const bill = await nextBillNumber(env.DB, String(body.saveType || 'INVOICE').toUpperCase(), acYear);
  const now = nowIso();

  const statements = [];

  // Stock movement first: the triggers abort the batch if this would go negative.
  for (const line of lines) {
    statements.push(
      env.DB.prepare(
        `UPDATE tbl_MultiStock
            SET St_CurrStock_i = St_CurrStock_i - ?
          WHERE St_StockID_n = ? AND St_StNumber_i = ?`
      ).bind(line.qty, line.stockCode, line.stNumber)
    );

    statements.push(
      env.DB.prepare(
        'UPDATE tbl_StockDtl SET St_TotalSt_i = St_TotalSt_i - ? WHERE St_StockID_n = ?'
      ).bind(line.qty, line.stockCode)
    );
  }

  statements.push(
    env.DB.prepare(
      `INSERT INTO tbl_SalDetail
         (Sa_EntDate_d, Sa_SaveType_vc, Sa_BillDate_d, Sa_BillTime_vc, Sa_InvNo_vc, Sa_BillNo_n,
          Sa_BType_vc, Sa_CusID_n, Sa_CusName_vc, Sa_GSTTotal_n, Sa_TotalAmt_n, Sa_RoType_vc,
          Sa_RoAmt_n, Sa_FinalTotal_n, Sa_AcYear_vc, Sa_UserID_vc, Sa_DiscountAmt_n,
          Sa_BefDisAmt_n, Sa_MRPTotal_n)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`
    ).bind(
      now, body.saveType || 'INVOICE', parsed.date, parsed.time, bill.invoiceNo, bill.billNo,
      'RETAIL', customerId, customerName, totals.gst, totals.beforeRound, 'ROUND',
      totals.roundOff, totals.finalTotal, acYear, user.username, totals.billDiscount,
      totals.beforeRound, round2(lines.reduce((sum, l) => sum + l.mrp * l.qty, 0))
    )
  );

  for (const line of lines) {
    statements.push(
      env.DB.prepare(
        `INSERT INTO tbl_SalParticular
           (Sa_SaveTy_vc, Sa_BillDate_d, Sa_InvNo_vc, Sa_CusID_n, Sa_StNumber_n, Sa_StCode_n,
            Sa_MRP_n, Sa_SaleRate_n, Sa_Qty_i, Sa_FQty_i, Sa_TQty_i,
            Sa_Taxable_n, Sa_CGSTP_n, Sa_SGSTP_n, Sa_IGSTP_n,
            Sa_CGSTAmt_n, Sa_SGSTAmt_n, Sa_IGSTAmt_n, Sa_ProTotal_n,
            Sa_ProDPer_n, Sa_ProDAmt_n, Sa_NetRate_n)
         VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`
      ).bind(
        body.saveType || 'INVOICE', parsed.date, bill.invoiceNo, customerId, line.stNumber, line.stockCode,
        line.mrp, line.rate, line.qty, line.qty, line.qty,
        line.taxable, line.cgstPercent, line.sgstPercent, line.igstPercent,
        line.cgstAmount, line.sgstAmount, line.igstAmount, line.total,
        0, line.discount, line.rate
      )
    );
  }

  for (const payment of payments) {
    statements.push(
      env.DB.prepare(
        'INSERT INTO api_sale_payment (invoice_no, mode, amount, reference, created_at) VALUES (?,?,?,?,?)'
      ).bind(bill.invoiceNo, String(payment.mode).toUpperCase(), round2(payment.amount), payment.reference || null, now)
    );
  }

  const responseBody = {
    invoiceNo: bill.invoiceNo,
    billNo: bill.billNo,
    acYear,
    saveType: body.saveType || 'INVOICE',
    billDate: parsed.date,
    billTime: parsed.time,
    customer: { id: customerId, name: customerName },
    lines: lines.map(l => ({
      stockCode: l.stockCode,
      stNumber: l.stNumber,
      name: l.name,
      unit: l.unit,
      qty: l.qty,
      rate: l.rate,
      mrp: l.mrp,
      discount: l.discount,
      taxable: l.taxable,
      cgstPercent: l.cgstPercent,
      sgstPercent: l.sgstPercent,
      igstPercent: l.igstPercent,
      cgstAmount: l.cgstAmount,
      sgstAmount: l.sgstAmount,
      igstAmount: l.igstAmount,
      total: l.total
    })),
    totals: {
      gross: totals.gross,
      taxable: totals.taxable,
      gst: totals.gst,
      billDiscount: totals.billDiscount,
      beforeRound: totals.beforeRound,
      roundOff: totals.roundOff,
      finalTotal: totals.finalTotal
    },
    payments: payments.map(p => ({ mode: String(p.mode).toUpperCase(), amount: round2(p.amount) }))
  };

  statements.push(
    env.DB.prepare(
      'INSERT INTO api_idempotency (key, endpoint, user_id, status_code, response, created_at) VALUES (?,?,?,?,?,?)'
    ).bind(idempotencyKey, 'POST /api/sales', user.id, 201, JSON.stringify(responseBody), now)
  );

  try {
    await batch(env.DB, statements);
  } catch (err) {
    const message = String(err && err.message ? err.message : err);

    // A concurrent request with the same key won the insert.
    if (message.includes('UNIQUE constraint failed: api_idempotency.key')) {
      const stored = await one(env.DB, 'SELECT status_code, response FROM api_idempotency WHERE key = ?', idempotencyKey);
      if (stored) {
        return { __replay: true, status: stored.status_code, body: JSON.parse(stored.response) };
      }
    }

    if (message.includes('insufficient stock')) {
      throw conflict('Insufficient stock; the bill was not saved', [{ message }]);
    }

    throw err;
  }

  await audit(env.DB, {
    userId: user.id,
    username: user.username,
    action: 'sale.create',
    entity: 'tbl_SalDetail',
    entityId: bill.invoiceNo,
    detail: `${lines.length} line(s), total ${totals.finalTotal}`
  });

  return { __replay: false, status: 201, body: responseBody };
}

export async function listSales(request, env) {
  const url = new URL(request.url);
  const { limit, offset } = pagination(url.searchParams);

  const from = url.searchParams.get('from');
  const to = url.searchParams.get('to');

  const where = [];
  const params = [];

  if (from) { where.push('Sa_BillDate_d >= ?'); params.push(from); }
  if (to) { where.push('Sa_BillDate_d <= ?'); params.push(to); }

  const whereSql = where.length ? `WHERE ${where.join(' AND ')}` : '';

  const countRow = await one(env.DB, `SELECT COUNT(*) AS c FROM tbl_SalDetail ${whereSql}`, ...params);
  const rows = await all(env.DB,
    `SELECT Sa_InvNo_vc AS invoiceNo, Sa_BillNo_n AS billNo, Sa_BillDate_d AS billDate,
            Sa_BillTime_vc AS billTime, Sa_CusID_n AS customerId, Sa_CusName_vc AS customerName,
            Sa_GSTTotal_n AS gst, Sa_DiscountAmt_n AS discount, Sa_FinalTotal_n AS finalTotal,
            Sa_AcYear_vc AS acYear
     FROM tbl_SalDetail ${whereSql}
     ORDER BY Sa_BillDate_d DESC, Sa_RecordNo_i DESC
     LIMIT ? OFFSET ?`, ...params, limit, offset);

  return { items: rows, limit, offset, total: countRow ? countRow.c : 0 };
}

export async function getSale(_request, env, _body, _user, ctx) {
  const invoiceNo = ctx.id;

  const header = await one(env.DB,
    `SELECT Sa_InvNo_vc AS invoiceNo, Sa_BillNo_n AS billNo, Sa_BillDate_d AS billDate,
            Sa_BillTime_vc AS billTime, Sa_CusID_n AS customerId, Sa_CusName_vc AS customerName,
            Sa_GSTTotal_n AS gst, Sa_DiscountAmt_n AS discount, Sa_BefDisAmt_n AS beforeRound,
            Sa_RoAmt_n AS roundOff, Sa_FinalTotal_n AS finalTotal, Sa_AcYear_vc AS acYear,
            Sa_SaveType_vc AS saveType
     FROM tbl_SalDetail WHERE Sa_InvNo_vc = ?`, invoiceNo);

  if (!header) throw notFound('Bill not found');

  const lines = await all(env.DB,
    `SELECT Sa_StCode_n AS stockCode, Sa_StNumber_n AS stNumber, Sa_MRP_n AS mrp,
            Sa_SaleRate_n AS rate, Sa_Qty_i AS qty, Sa_Taxable_n AS taxable,
            Sa_CGSTP_n AS cgstPercent, Sa_SGSTP_n AS sgstPercent, Sa_IGSTP_n AS igstPercent,
            Sa_CGSTAmt_n AS cgstAmount, Sa_SGSTAmt_n AS sgstAmount, Sa_IGSTAmt_n AS igstAmount,
            Sa_ProTotal_n AS total, Sa_ProDAmt_n AS discount
     FROM tbl_SalParticular WHERE Sa_InvNo_vc = ? ORDER BY Sa_RecordNo2_i`, invoiceNo);

  const payments = await all(env.DB,
    'SELECT mode, amount, reference FROM api_sale_payment WHERE invoice_no = ? ORDER BY id', invoiceNo);

  return { ...header, lines, payments };
}
