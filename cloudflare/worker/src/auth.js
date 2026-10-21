/**
 * Authentication.
 *
 * Passwords: PBKDF2-SHA256 via WebCrypto (available in Workers).
 *   stored as  pbkdf2$sha256$<iterations>$<saltBase64>$<hashBase64>
 *
 * Tokens: stateless HMAC-SHA256 tokens, short-lived (default 8h).
 *   <base64url(payload)>.<base64url(signature)>
 *
 * The signing key comes from the TOKEN_SECRET secret binding. It is never in
 * source and never shipped inside the Windows executable.
 */

import { unauthorized, forbidden } from './http.js';

const PBKDF2_ITERATIONS = 100000;
const SALT_BYTES = 16;
const KEY_BYTES = 32;

const encoder = new TextEncoder();

function base64FromBytes(bytes) {
  let binary = '';
  for (const b of new Uint8Array(bytes)) binary += String.fromCharCode(b);
  return btoa(binary);
}

function bytesFromBase64(text) {
  const binary = atob(text);
  const out = new Uint8Array(binary.length);
  for (let i = 0; i < binary.length; i++) out[i] = binary.charCodeAt(i);
  return out;
}

function base64UrlFromBytes(bytes) {
  return base64FromBytes(bytes).replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/, '');
}

function bytesFromBase64Url(text) {
  const padded = text.replace(/-/g, '+').replace(/_/g, '/');
  return bytesFromBase64(padded + '='.repeat((4 - (padded.length % 4)) % 4));
}

/** Constant-time string comparison. */
function safeEqual(a, b) {
  if (typeof a !== 'string' || typeof b !== 'string') return false;
  if (a.length !== b.length) return false;

  let diff = 0;
  for (let i = 0; i < a.length; i++) diff |= a.charCodeAt(i) ^ b.charCodeAt(i);
  return diff === 0;
}

async function derive(password, salt, iterations) {
  const key = await crypto.subtle.importKey(
    'raw', encoder.encode(password), 'PBKDF2', false, ['deriveBits']
  );

  const bits = await crypto.subtle.deriveBits(
    { name: 'PBKDF2', salt, iterations, hash: 'SHA-256' },
    key,
    KEY_BYTES * 8
  );

  return new Uint8Array(bits);
}

export async function hashPassword(password) {
  if (typeof password !== 'string' || password.length < 4) {
    throw new Error('Password must be at least 4 characters');
  }
  if (password.length > 256) {
    throw new Error('Password is too long');
  }

  const salt = crypto.getRandomValues(new Uint8Array(SALT_BYTES));
  const hash = await derive(password, salt, PBKDF2_ITERATIONS);

  return `pbkdf2$sha256$${PBKDF2_ITERATIONS}$${base64FromBytes(salt)}$${base64FromBytes(hash)}`;
}

export async function verifyPassword(password, stored) {
  if (typeof stored !== 'string') return false;

  const parts = stored.split('$');
  if (parts.length !== 5 || parts[0] !== 'pbkdf2' || parts[1] !== 'sha256') return false;

  const iterations = parseInt(parts[2], 10);
  if (!Number.isFinite(iterations) || iterations < 1000 || iterations > 1000000) return false;

  const salt = bytesFromBase64(parts[3]);
  const expected = parts[4];
  const actual = base64FromBytes(await derive(password, salt, iterations));

  return safeEqual(actual, expected);
}

// ---------------------------------------------------------------------------
// Tokens
// ---------------------------------------------------------------------------

async function hmacKey(secret) {
  return crypto.subtle.importKey(
    'raw', encoder.encode(secret), { name: 'HMAC', hash: 'SHA-256' }, false, ['sign', 'verify']
  );
}

export async function signToken(payload, secret) {
  const key = await hmacKey(secret);
  const body = base64UrlFromBytes(encoder.encode(JSON.stringify(payload)));
  const signature = await crypto.subtle.sign('HMAC', key, encoder.encode(body));

  return `${body}.${base64UrlFromBytes(signature)}`;
}

export async function verifyToken(token, secret) {
  if (typeof token !== 'string' || !token.includes('.')) return null;

  const [body, signature] = token.split('.');
  if (!body || !signature) return null;

  try {
    const key = await hmacKey(secret);
    const ok = await crypto.subtle.verify(
      'HMAC', key, bytesFromBase64Url(signature), encoder.encode(body)
    );

    if (!ok) return null;

    const payload = JSON.parse(new TextDecoder().decode(bytesFromBase64Url(body)));
    const now = Math.floor(Date.now() / 1000);

    if (!payload || typeof payload !== 'object') return null;
    if (typeof payload.exp !== 'number' || payload.exp <= now) return null;
    if (typeof payload.sub !== 'number') return null;

    return payload;
  } catch {
    return null;
  }
}

export function issuePayload(user, ttlSeconds) {
  const now = Math.floor(Date.now() / 1000);

  return {
    sub: user.id,
    empId: user.emp_id,
    username: user.username,
    role: user.role,
    iat: now,
    exp: now + ttlSeconds,
    jti: crypto.randomUUID()
  };
}

// ---------------------------------------------------------------------------
// Request authentication / authorisation
// ---------------------------------------------------------------------------

/**
 * Verifies the Bearer token and loads the user from api_users.
 * Throws 401 when the token is missing, invalid, expired or the user is
 * disabled/deleted - a token never outlives the account.
 */
export async function requireUser(request, env) {
  const header = request.headers.get('Authorization') || '';
  const match = /^Bearer\s+(.+)$/i.exec(header.trim());

  if (!match) throw unauthorized('Missing Bearer token');
  if (!env.TOKEN_SECRET) throw unauthorized('Server is not configured (TOKEN_SECRET missing)');

  const payload = await verifyToken(match[1], env.TOKEN_SECRET);
  if (!payload) throw unauthorized('Invalid or expired token');

  const user = await env.DB
    .prepare('SELECT id, emp_id, username, display_name, role, is_active FROM api_users WHERE id = ?')
    .bind(payload.sub)
    .first();

  if (!user) throw unauthorized('User no longer exists');
  if (!user.is_active) throw unauthorized('User is disabled');

  return user;
}

/** Role gate. `roles` is a list of allowed roles. */
export function requireRole(user, ...roles) {
  if (!roles.includes(user.role)) {
    throw forbidden(`Requires role: ${roles.join(' or ')}`);
  }
}
