/**
 * Customers and suppliers.
 *
 * Business rule preserved from the desktop app (FrmSmCustomer / FrmSmSupplier):
 * both live in tbl_CusProfile and are separated by C_Type_vc. C_ID_n is
 * MAX+1 *per type*, and C_AllID_n is the global id. Getting this wrong would
 * break the numbering the shop already uses, so it is reproduced exactly.
 */

import { requireRole } from '../auth.js';
import { badRequest, notFound } from '../http.js';
import { all, one, batch, audit, nowIso, pagination, buildUpdate } from '../db.js';

const TYPES = { customer: 'CUSTOMER', supplier: 'SUPPLIER' };

function typeOf(kind) {
  const t = TYPES[String(kind || '').toLowerCase()];
  if (!t) throw badRequest('Unknown party type');
  return t;
}

const SELECT = `
  SELECT C_RecordNumber_i AS recordId,
         C_AllID_n        AS allId,
         C_Type_vc        AS type,
         C_ID_n           AS code,
         C_Name_vc        AS name,
         C_CmpName_vc     AS companyName,
         C_Mobile_vc      AS mobile,
         C_Email_vc       AS email,
         C_Add1_vc        AS address1,
         C_Add2_vc        AS address2,
         C_Add3_vc        AS address3,
         C_Pincode_vc     AS pincode,
         C_GSTIN_vc       AS gstin,
         C_PANNo_vc       AS panNo,
         C_AreaCode_i     AS areaCode,
         C_Type3_vc       AS group3
  FROM tbl_CusProfile
`;

function shape(r) {
  return {
    recordId: r.recordId,
    allId: r.allId,
    type: r.type,
    code: r.code,
    name: r.name,
    companyName: r.companyName,
    mobile: r.mobile,
    email: r.email,
    address1: r.address1,
    address2: r.address2,
    address3: r.address3,
    pincode: r.pincode,
    gstin: r.gstin,
    panNo: r.panNo,
    areaCode: r.areaCode,
    group3: r.group3
  };
}

export function makePartyRoutes(kind) {
  const partyType = typeOf(kind);

  async function list(request, env) {
    const url = new URL(request.url);
    const { limit, offset } = pagination(url.searchParams);
    const search = (url.searchParams.get('search') || '').trim();

    const where = ['C_Type_vc = ?'];
    const params = [partyType];

    if (search) {
      where.push('(C_Name_vc LIKE ? OR C_Mobile_vc LIKE ? OR C_GSTIN_vc LIKE ?)');
      const like = `%${search}%`;
      params.push(like, like, like);
    }

    const whereSql = `WHERE ${where.join(' AND ')}`;

    const countRow = await one(env.DB, `SELECT COUNT(*) AS c FROM tbl_CusProfile ${whereSql}`, ...params);
    const rows = await all(env.DB,
      `${SELECT} ${whereSql} ORDER BY C_Name_vc LIMIT ? OFFSET ?`, ...params, limit, offset);

    return { items: rows.map(shape), limit, offset, total: countRow ? countRow.c : 0 };
  }

  async function get(_request, env, _body, _user, ctx) {
    const row = await one(env.DB, `${SELECT} WHERE C_Type_vc = ? AND C_ID_n = ?`, partyType, ctx.id);
    if (!row) throw notFound(`${kind} not found`);

    return shape(row);
  }

  async function create(request, env, body, user) {
    requireRole(user, 'admin', 'manager', 'cashier');

    const name = String(body.name || '').trim();
    if (!name) throw badRequest('name is required');
    if (name.length > 200) throw badRequest('name is too long');

    const mobile = String(body.mobile || '').trim();
    if (mobile && !/^[0-9+\-\s]{6,20}$/.test(mobile)) throw badRequest('mobile is not valid');

    // Duplicate guard, same rule as the desktop: same name + mobile within a type.
    const dup = await one(env.DB,
      `SELECT COUNT(*) AS c FROM tbl_CusProfile
       WHERE C_Type_vc = ? AND C_Name_vc = ? AND COALESCE(C_Mobile_vc,'') = ?`,
      partyType, name, mobile);

    if (dup && dup.c > 0) throw badRequest(`${kind} already exists with the same name and mobile`);

    const codeRow = await one(env.DB,
      'SELECT COALESCE(MAX(C_ID_n), 0) + 1 AS code FROM tbl_CusProfile WHERE C_Type_vc = ?', partyType);
    const allRow = await one(env.DB, 'SELECT COALESCE(MAX(C_AllID_n), 0) + 1 AS allId FROM tbl_CusProfile');

    const code = codeRow.code;
    const allId = allRow.allId;

    await env.DB.prepare(
      `INSERT INTO tbl_CusProfile
         (C_EDate_d, C_AllID_n, C_Type_vc, C_Type3_vc, C_ID_n, C_GrpID_i,
          C_Name_vc, C_CmpName_vc, C_Mobile_vc, C_Email_vc,
          C_Add1_vc, C_Add2_vc, C_Add3_vc, C_Pincode_vc, C_GSTIN_vc, C_PANNo_vc, C_AreaCode_i)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`
    ).bind(
      nowIso(), allId, partyType, body.group3 || null, code, body.groupId || null,
      name, body.companyName || null, mobile, body.email || null,
      body.address1 || null, body.address2 || null, body.address3 || null,
      body.pincode || null, body.gstin || null, body.panNo || null,
      body.areaCode || null
    ).run();

    await audit(env.DB, {
      userId: user.id, username: user.username, action: `${kind}.create`,
      entity: 'tbl_CusProfile', entityId: String(code), detail: name
    });

    return get(request, env, null, null, { id: code });
  }

  async function update(request, env, body, user, ctx) {
    requireRole(user, 'admin', 'manager');

    const existing = await one(env.DB,
      'SELECT C_ID_n FROM tbl_CusProfile WHERE C_Type_vc = ? AND C_ID_n = ?', partyType, ctx.id);

    if (!existing) throw notFound(`${kind} not found`);

    const fields = {
      C_Name_vc: body.name === undefined ? undefined : String(body.name).trim(),
      C_CmpName_vc: body.companyName,
      C_Mobile_vc: body.mobile,
      C_Email_vc: body.email,
      C_Add1_vc: body.address1,
      C_Add2_vc: body.address2,
      C_Add3_vc: body.address3,
      C_Pincode_vc: body.pincode,
      C_GSTIN_vc: body.gstin,
      C_PANNo_vc: body.panNo,
      C_AreaCode_i: body.areaCode
    };

    if (fields.C_Name_vc !== undefined && !fields.C_Name_vc) throw badRequest('name cannot be empty');

    const { fragment, values } = buildUpdate(fields, Object.keys(fields));

    await env.DB.prepare(
      `UPDATE tbl_CusProfile SET ${fragment} WHERE C_Type_vc = ? AND C_ID_n = ?`
    ).bind(...values, partyType, ctx.id).run();

    await audit(env.DB, {
      userId: user.id, username: user.username, action: `${kind}.update`,
      entity: 'tbl_CusProfile', entityId: String(ctx.id)
    });

    return get(request, env, null, null, ctx);
  }

  return { list, get, create, update };
}

