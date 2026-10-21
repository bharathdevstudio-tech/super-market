/**
 * Authentication routes.
 */

import { hashPassword, verifyPassword, signToken, issuePayload, requireUser, requireRole } from '../auth.js';
import { badRequest, unauthorized, conflict } from '../http.js';
import { one, run, audit, nowIso } from '../db.js';

/**
 * Best-effort rate limiting.
 *
 * Workers isolates are short-lived and per-colo, so this stops a single
 * hammering client but is not a global guarantee. For a hard limit use a
 * Durable Object or Cloudflare's WAF rate limiting rules. This is documented
 * rather than pretended to be stronger than it is.
 */
const attempts = new Map();
const WINDOW_MS = 5 * 60 * 1000;
const MAX_ATTEMPTS = 10;

function rateLimit(key) {
  const now = Date.now();
  const entry = attempts.get(key);

  if (!entry || now - entry.start > WINDOW_MS) {
    attempts.set(key, { start: now, count: 1 });
    return;
  }

  entry.count += 1;

  if (entry.count > MAX_ATTEMPTS) {
    const retryAfter = Math.ceil((entry.start + WINDOW_MS - now) / 1000);
    const err = unauthorized('Too many login attempts. Try again later.');
    err.status = 429;
    err.code = 'RATE_LIMITED';
    err.details = [{ retryAfterSeconds: retryAfter }];
    throw err;
  }

  // Keep the map from growing without bound.
  if (attempts.size > 5000) attempts.clear();
}

export async function login(request, env, body) {
  const username = String(body.username || '').trim();
  const password = String(body.password || '');

  if (!username || !password) throw badRequest('username and password are required');

  const ip = request.headers.get('CF-Connecting-IP') || 'unknown';
  rateLimit(`${ip}|${username.toLowerCase()}`);

  const user = await one(env.DB,
    `SELECT id, emp_id, username, display_name, password_hash, role, is_active
     FROM api_users WHERE username = ? COLLATE NOCASE`, username);

  // Same error for unknown user and wrong password: do not leak which usernames exist.
  const invalid = unauthorized('Invalid username or password');

  if (!user) {
    audit(env.DB, { action: 'login.failed', username, ip, detail: 'unknown user' });
    throw invalid;
  }
  if (!user.is_active) throw unauthorized('This account is disabled');

  const ok = await verifyPassword(password, user.password_hash);

  if (!ok) {
    await audit(env.DB, { userId: user.id, username: user.username, action: 'login.failed', ip, detail: 'bad password' });
    throw invalid;
  }

  const ttl = parseInt(env.TOKEN_TTL_SECONDS || '28800', 10);
  const token = await signToken(issuePayload(user, ttl), env.TOKEN_SECRET);

  await run(env.DB, 'UPDATE api_users SET last_login_at = ? WHERE id = ?', nowIso(), user.id);
  await audit(env.DB, { userId: user.id, username: user.username, action: 'login.success', ip });

  return {
    token,
    expiresIn: ttl,
    user: {
      id: user.id,
      empId: user.emp_id,
      username: user.username,
      displayName: user.display_name,
      role: user.role
    }
  };
}

export async function me(_request, env, _body, user) {
  return {
    id: user.id,
    empId: user.emp_id,
    username: user.username,
    displayName: user.display_name,
    role: user.role
  };
}

export async function changePassword(_request, env, body, user) {
  const current = String(body.currentPassword || '');
  const next = String(body.newPassword || '');

  if (next.length < 8) throw badRequest('New password must be at least 8 characters');
  if (next.length > 256) throw badRequest('New password is too long');

  const row = await one(env.DB, 'SELECT password_hash FROM api_users WHERE id = ?', user.id);

  if (!row || !(await verifyPassword(current, row.password_hash))) {
    throw unauthorized('Current password is incorrect');
  }

  const hash = await hashPassword(next);

  await run(env.DB,
    'UPDATE api_users SET password_hash = ?, must_change = 0, updated_at = ? WHERE id = ?',
    hash, nowIso(), user.id);

  await audit(env.DB, { userId: user.id, username: user.username, action: 'password.change' });

  return { changed: true };
}

/** Admin-only user provisioning. */
export async function createUser(_request, env, body, user) {
  requireRole(user, 'admin');

  const username = String(body.username || '').trim();
  const password = String(body.password || '');
  const role = String(body.role || 'cashier');
  const empId = parseInt(body.empId, 10);

  if (!/^[A-Za-z0-9._-]{3,50}$/.test(username)) {
    throw badRequest('username must be 3-50 characters (letters, digits, . _ -)');
  }
  if (!['admin', 'manager', 'cashier'].includes(role)) {
    throw badRequest('role must be admin, manager or cashier');
  }
  if (!Number.isFinite(empId)) throw badRequest('empId is required (tbl_UserList.U_EmpID_i)');

  const existing = await one(env.DB, 'SELECT id FROM api_users WHERE username = ? COLLATE NOCASE', username);
  if (existing) throw conflict('That username already exists');

  const hash = await hashPassword(password);
  const now = nowIso();

  const result = await run(env.DB,
    `INSERT INTO api_users (emp_id, username, display_name, password_hash, role, is_active, created_at, updated_at)
     VALUES (?,?,?,?,?,1,?,?)`,
    empId, username, body.displayName || username, hash, role, now, now);

  await audit(env.DB, { userId: user.id, username: user.username, action: 'user.create', entity: 'api_users', entityId: String(result.lastRowId) });

  return {
    id: result.lastRowId,
    empId,
    username,
    role,
    displayName: body.displayName || username
  };
}

export async function listUsers(_request, env, _body, user) {
  requireRole(user, 'admin');

  const rows = await env.DB
    .prepare('SELECT id, emp_id, username, display_name, role, is_active, last_login_at FROM api_users ORDER BY username')
    .all();

  return { items: rows.results || [] };
}

/** Re-export so the router does not need to import auth.js directly. */
export { requireUser };
