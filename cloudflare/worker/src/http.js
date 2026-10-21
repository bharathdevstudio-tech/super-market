/**
 * HTTP helpers: JSON responses, errors, CORS.
 *
 * Every error the API returns has the same shape so the VB client can show a
 * useful message instead of a stack trace:
 *   { "error": { "code": "VALIDATION_FAILED", "message": "...", "details": [...] } }
 */

export class ApiError extends Error {
  constructor(status, code, message, details) {
    super(message);
    this.status = status;
    this.code = code;
    this.details = details || null;
  }
}

export const badRequest = (message, details) => new ApiError(400, 'BAD_REQUEST', message, details);
export const unauthorized = (message = 'Authentication required') => new ApiError(401, 'UNAUTHORIZED', message);
export const forbidden = (message = 'Not allowed') => new ApiError(403, 'FORBIDDEN', message);
export const notFound = (message = 'Not found') => new ApiError(404, 'NOT_FOUND', message);
export const conflict = (message, details) => new ApiError(409, 'CONFLICT', message, details);

export function corsHeaders(request, env) {
  const allowed = (env && env.CORS_ORIGIN) || '*';
  const origin = request.headers.get('Origin') || allowed;

  return {
    'Access-Control-Allow-Origin': origin,
    'Access-Control-Allow-Methods': 'GET,POST,PUT,DELETE,OPTIONS',
    'Access-Control-Allow-Headers': 'Content-Type,Authorization,X-Idempotency-Key',
    'Access-Control-Max-Age': '86400'
  };
}

export function json(body, status = 200, request, env) {
  return new Response(JSON.stringify(body), {
    status,
    headers: {
      'Content-Type': 'application/json; charset=utf-8',
      'Cache-Control': 'no-store',
      ...(request ? corsHeaders(request, env) : {})
    }
  });
}

export function errorResponse(err, request, env) {
  const isApi = err instanceof ApiError;
  const status = isApi ? err.status : 500;
  const code = isApi ? err.code : 'INTERNAL';
  const message = isApi ? err.message : 'Internal server error';

  if (!isApi) {
    console.error(JSON.stringify({
      level: 'error',
      message: err && err.message,
      stack: err && err.stack
    }));
  }

  return json({
    error: {
      code,
      message,
      ...(isApi && err.details ? { details: err.details } : {})
    }
  }, status, request, env);
}

/**
 * Reads and validates a JSON body.
 * `maxBytes` stops a client from posting a giant payload.
 */
export async function readJson(request, maxBytes = 512 * 1024) {
  const text = await request.text();

  if (text.length > maxBytes) {
    throw badRequest('Request body is too large');
  }

  if (!text) return {};

  try {
    return JSON.parse(text);
  } catch {
    throw badRequest('Request body is not valid JSON');
  }
}
