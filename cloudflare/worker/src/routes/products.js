/**
 * Products / items.
 *
 * The legacy schema splits an item across two tables:
 *   tbl_StockDtl   - the master (name, unit, HSN, tax percentages, category)
 *   tbl_MultiStock - the stock line (purchase/sale/MRP rate, quantity, barcode)
 *
 * The API exposes them as one "product" so the desktop client does not have to
 * care about the split. Where an item has several stock lines (batches), the
 * list endpoint uses the lowest St_StNumber_i so the response is deterministic.
 */

import { requireRole } from '../auth.js';
import { badRequest, notFound } from '../http.js';
import { all, one, batch, audit, nowIso, pagination, buildUpdate } from '../db.js';
import { money, percent } from '../money.js';

const PRODUCT_SELECT = `
  SELECT d.St_StockID_n      AS id,
         d.St_ProName_vc     AS name,
         d.St_ProTamil_vc    AS tamilName,
         d.St_Unit_vc        AS unit,
         d.St_HSN_vc         AS hsn,
         d.St_CGSTPer_n      AS cgstPercent,
         d.St_SGSTPer_n      AS sgstPercent,
         d.St_IGSTPer_n      AS igstPercent,
         d.St_TotalSt_i      AS stock,
         d.St_CatID_i        AS categoryId,
         d.St_BrandID_i      AS brandId,
         m.St_StNumber_i     AS stockLine,
         m.St_SaleRate_n     AS saleRate,
         m.St_MRPRate_n      AS mrp,
         m.St_PurRate_n      AS purchaseRate,
         m.St_CurrStock_i    AS lineStock,
         m.St_Barcode_vc     AS barcode
  FROM tbl_StockDtl d
  LEFT JOIN tbl_MultiStock m
         ON m.St_StockID_n = d.St_StockID_n
        AND m.St_StNumber_i = (
              SELECT MIN(x.St_StNumber_i)
              FROM tbl_MultiStock x
              WHERE x.St_StockID_n = d.St_StockID_n
            )
`;

function shape(row) {
  if (!row) return null;
  return {
    id: row.id,
    name: row.name,
    tamilName: row.tamilName,
    unit: row.unit,
    hsn: row.hsn,
    cgstPercent: row.cgstPercent || 0,
    sgstPercent: row.sgstPercent || 0,
    igstPercent: row.igstPercent || 0,
    stock: row.stock || 0,
    categoryId: row.categoryId,
    brandId: row.brandId,
    stockLine: row.stockLine,
    saleRate: row.saleRate || 0,
    mrp: row.mrp || 0,
    purchaseRate: row.purchaseRate || 0,
    lineStock: row.lineStock || 0,
    barcode: row.barcode || null
  };
}

export async function listProducts(request, env) {
  const url = new URL(request.url);
  const { limit, offset } = pagination(url.searchParams);
  const search = (url.searchParams.get('search') || '').trim();
  const barcode = (url.searchParams.get('barcode') || '').trim();

  if (barcode) {
    const row = await one(env.DB,
      `${PRODUCT_SELECT} WHERE m.St_Barcode_vc = ? ORDER BY d.St_ProName_vc LIMIT 1`, barcode);

    return { items: row ? [shape(row)] : [], limit, offset, total: row ? 1 : 0 };
  }

  const where = [];
  const params = [];

  if (search) {
    where.push('(d.St_ProName_vc LIKE ? OR d.St_ProTamil_vc LIKE ? OR d.St_HSN_vc LIKE ? OR m.St_Barcode_vc LIKE ?)');
    const like = `%${search}%`;
    params.push(like, like, like, like);
  }

  const whereSql = where.length ? `WHERE ${where.join(' AND ')}` : '';

  const countRow = await one(env.DB,
    `SELECT COUNT(*) AS c FROM tbl_StockDtl d ${whereSql}`, ...params);

  const rows = await all(env.DB,
    `${PRODUCT_SELECT} ${whereSql} ORDER BY d.St_ProName_vc LIMIT ? OFFSET ?`,
    ...params, limit, offset);

  return { items: rows.map(shape), limit, offset, total: countRow ? countRow.c : 0 };
}

export async function getProduct(request, env, _body, _user, ctx) {
  const row = await one(env.DB, `${PRODUCT_SELECT} WHERE d.St_StockID_n = ?`, ctx.id);
  if (!row) throw notFound('Product not found');

  return shape(row);
}

function validateProduct(body, { partial = false } = {}) {
  const out = {};

  if (!partial || body.name !== undefined) {
    const name = String(body.name || '').trim();
    if (name.length < 1) throw badRequest('name is required');
    if (name.length > 200) throw badRequest('name is too long');
    out.name = name;
  }

  if (body.tamilName !== undefined) out.tamilName = String(body.tamilName || '').slice(0, 200);
  if (body.unit !== undefined) out.unit = String(body.unit || '').slice(0, 20);
  if (body.hsn !== undefined) out.hsn = String(body.hsn || '').slice(0, 20);

  if (body.cgstPercent !== undefined) out.cgstPercent = percent(body.cgstPercent, 'cgstPercent');
  if (body.sgstPercent !== undefined) out.sgstPercent = percent(body.sgstPercent, 'sgstPercent');
  if (body.igstPercent !== undefined) out.igstPercent = percent(body.igstPercent, 'igstPercent');

  if (body.saleRate !== undefined) out.saleRate = money(body.saleRate, 'saleRate');
  if (body.mrp !== undefined) out.mrp = money(body.mrp, 'mrp');
  if (body.purchaseRate !== undefined) out.purchaseRate = money(body.purchaseRate, 'purchaseRate');
  if (body.stock !== undefined) out.stock = money(body.stock, 'stock');

  if (body.barcode !== undefined) out.barcode = String(body.barcode || '').slice(0, 60) || null;

  if (body.categoryId !== undefined) out.categoryId = body.categoryId === null ? null : parseInt(body.categoryId, 10);
  if (body.brandId !== undefined) out.brandId = body.brandId === null ? null : parseInt(body.brandId, 10);

  return out;
}

export async function createProduct(request, env, body, user) {
  requireRole(user, 'admin', 'manager');

  const p = validateProduct(body);
  const now = nowIso();

  if (p.saleRate !== undefined && p.mrp !== undefined && p.mrp > 0 && p.saleRate > p.mrp) {
    throw badRequest('saleRate cannot be greater than mrp');
  }

  const idRow = await one(env.DB, 'SELECT COALESCE(MAX(St_StockID_n), 0) + 1 AS id FROM tbl_StockDtl');
  const lineRow = await one(env.DB, 'SELECT COALESCE(MAX(St_StNumber_i), 0) + 1 AS n FROM tbl_MultiStock');

  const id = idRow.id;
  const stNumber = lineRow.n;

  const statements = [
    env.DB.prepare(
      `INSERT INTO tbl_StockDtl (St_StockID_n, St_ProName_vc, St_ProTamil_vc, St_Unit_vc, St_HSN_vc,
                                 St_CGSTPer_n, St_SGSTPer_n, St_IGSTPer_n, St_TotalSt_i,
                                 St_CatID_i, St_BrandID_i, St_EntDate_d)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?)`
    ).bind(id, p.name, p.tamilName || '', p.unit || 'NOS', p.hsn || '',
      p.cgstPercent || 0, p.sgstPercent || 0, p.igstPercent || 0, p.stock || 0,
      p.categoryId || null, p.brandId || null, now),

    env.DB.prepare(
      `INSERT INTO tbl_MultiStock (St_StockID_n, St_StNumber_i, St_ProName_vc, St_CurrStock_i,
                                   St_PurRate_n, St_SaleRate_n, St_MRPRate_n, St_Barcode_vc, St_EDate_d)
       VALUES (?,?,?,?,?,?,?,?,?)`
    ).bind(id, stNumber, p.name, p.stock || 0,
      p.purchaseRate || 0, p.saleRate || 0, p.mrp || 0, p.barcode || null, now)
  ];

  await batch(env.DB, statements);
  await audit(env.DB, {
    userId: user.id, username: user.username, action: 'product.create',
    entity: 'tbl_StockDtl', entityId: String(id), detail: p.name
  });

  return getProduct(request, env, null, null, { id });
}

export async function updateProduct(request, env, body, user, ctx) {
  requireRole(user, 'admin', 'manager');

  const existing = await one(env.DB, 'SELECT St_StockID_n, St_TotalSt_i FROM tbl_StockDtl WHERE St_StockID_n = ?', ctx.id);
  if (!existing) throw notFound('Product not found');

  const p = validateProduct(body, { partial: true });
  const now = nowIso();

  const masterFields = {
    St_ProName_vc: p.name,
    St_ProTamil_vc: p.tamilName,
    St_Unit_vc: p.unit,
    St_HSN_vc: p.hsn,
    St_CGSTPer_n: p.cgstPercent,
    St_SGSTPer_n: p.sgstPercent,
    St_IGSTPer_n: p.igstPercent,
    St_TotalSt_i: p.stock,
    St_CatID_i: p.categoryId,
    St_BrandID_i: p.brandId
  };

  const lineFields = {
    St_SaleRate_n: p.saleRate,
    St_MRPRate_n: p.mrp,
    St_PurRate_n: p.purchaseRate,
    St_CurrStock_i: p.stock,
    St_Barcode_vc: p.barcode
  };

  const hasMaster = Object.values(masterFields).some(v => v !== undefined);
  const hasLine = Object.values(lineFields).some(v => v !== undefined);

  if (!hasMaster && !hasLine) throw badRequest('No fields to update');

  const statements = [];

  if (hasMaster) {
    const { fragment, values } = buildUpdate(masterFields, Object.keys(masterFields));
    statements.push(
      env.DB.prepare(`UPDATE tbl_StockDtl SET ${fragment} WHERE St_StockID_n = ?`).bind(...values, ctx.id)
    );
  }

  if (hasLine) {
    const { fragment, values } = buildUpdate(lineFields, Object.keys(lineFields));
    statements.push(
      env.DB.prepare(
        `UPDATE tbl_MultiStock SET ${fragment}
         WHERE St_StockID_n = ?
           AND St_StNumber_i = (SELECT MIN(x.St_StNumber_i) FROM tbl_MultiStock x WHERE x.St_StockID_n = ?)`
      ).bind(...values, ctx.id, ctx.id)
    );
  }

  await batch(env.DB, statements);
  await audit(env.DB, {
    userId: user.id, username: user.username, action: 'product.update',
    entity: 'tbl_StockDtl', entityId: String(ctx.id)
  });

  return getProduct(request, env, null, null, ctx);
}

