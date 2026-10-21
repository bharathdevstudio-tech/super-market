/**
 * Bharath SuperMarket API - Cloudflare Worker entry point.
 *
 *   Windows app  ->  HTTPS  ->  this Worker  ->  D1
 *
 * Cloudflare does not run the VB executable and does not host SQL Server; it
 * only serves this API. The desktop app keeps its UI and business logic and
 * talks to these endpoints instead of connecting to SQL Server directly.
 */

import { ApiError, errorResponse, json, readJson, badRequest, notFound } from './http.js';
import { requireUser } from './auth.js';

import * as authRoutes from './routes/auth.js';
import * as productRoutes from './routes/products.js';
import { makePartyRoutes } from './routes/parties.js';
import * as salesRoutes from './routes/sales.js';
import * as purchaseRoutes from './routes/purchases.js';
import * as reportRoutes from './routes/reports.js';
import * as syncRoutes from './routes/sync.js';

const API_VERSION = '1.0.0';

const customers = makePartyRoutes('customer');
const suppliers = makePartyRoutes('supplier');

/**
 * Route table. `auth` means a valid Bearer token is required. Authorization by
 * role is enforced inside the handlers (requireRole) so the rule lives next to
 * the business logic it protects.
 */
const routes = [
  { method: 'GET',    pattern: /^\/api\/health$/,                    handler: health },

  { method: 'POST',   pattern: /^\/api\/auth\/login$/,               handler: authRoutes.login },
  { method: 'GET',    pattern: /^\/api\/auth\/me$/,                  handler: authRoutes.me, auth: true },
  { method: 'POST',   pattern: /^\/api\/auth\/change-password$/,     handler: authRoutes.changePassword, auth: true },

  { method: 'GET',    pattern: /^\/api\/users$/,                     handler: authRoutes.listUsers, auth: true },
  { method: 'POST',   pattern: /^\/api\/users$/,                     handler: authRoutes.createUser, auth: true },

  { method: 'GET',    pattern: /^\/api\/products$/,                  handler: productRoutes.listProducts, auth: true },
  { method: 'GET',    pattern: /^\/api\/products\/([^/]+)$/,         handler: productRoutes.getProduct, auth: true },
  { method: 'POST',   pattern: /^\/api\/products$/,                  handler: productRoutes.createProduct, auth: true },
  { method: 'PUT',    pattern: /^\/api\/products\/([^/]+)$/,         handler: productRoutes.updateProduct, auth: true },

  { method: 'GET',    pattern: /^\/api\/customers$/,                 handler: customers.list, auth: true },
  { method: 'GET',    pattern: /^\/api\/customers\/([^/]+)$/,        handler: customers.get, auth: true },
  { method: 'POST',   pattern: /^\/api\/customers$/,                 handler: customers.create, auth: true },
  { method: 'PUT',    pattern: /^\/api\/customers\/([^/]+)$/,        handler: customers.update, auth: true },

  { method: 'GET',    pattern: /^\/api\/suppliers$/,                 handler: suppliers.list, auth: true },
  { method: 'GET',    pattern: /^\/api\/suppliers\/([^/]+)$/,        handler: suppliers.get, auth: true },
  { method: 'POST',   pattern: /^\/api\/suppliers$/,                 handler: suppliers.create, auth: true },
  { method: 'PUT',    pattern: /^\/api\/suppliers\/([^/]+)$/,        handler: suppliers.update, auth: true },

  { method: 'GET',    pattern: /^\/api\/sales$/,                     handler: salesRoutes.listSales, auth: true },
  { method: 'POST',   pattern: /^\/api\/sales$/,                     handler: salesRoutes.createSale, auth: true },
  { method: 'GET',    pattern: /^\/api\/sales\/([^/]+)$/,            handler: salesRoutes.getSale, auth: true },

  { method: 'GET',    pattern: /^\/api\/purchases$/,                 handler: purchaseRoutes.listPurchases, auth: true },
  { method: 'POST',   pattern: /^\/api\/purchases$/,                 handler: purchaseRoutes.createPurchase, auth: true },
  { method: 'GET',    pattern: /^\/api\/purchases\/([^/]+)$/,        handler: purchaseRoutes.getPurchase, auth: true },

  { method: 'GET',    pattern: /^\/api\/reports\/sales$/,            handler: reportRoutes.salesSummary, auth: true },
  { method: 'GET',    pattern: /^\/api\/reports\/gst$/,              handler: reportRoutes.gstSummary, auth: true },
  { method: 'GET',    pattern: /^\/api\/reports\/top-items$/,        handler: reportRoutes.topItems, auth: true },
  { method: 'GET',    pattern: /^\/api\/reports\/stock-valuation$/,  handler: reportRoutes.stockValuation, auth: true },
  { method: 'GET',    pattern: /^\/api\/reports\/dashboard$/,        handler: reportRoutes.dashboard, auth: true },

  { method: 'POST',   pattern: /^\/api\/sync\/import$/,              handler: syncRoutes.importRows, auth: true },
  { method: 'GET',    pattern: /^\/api\/sync\/export$/,              handler: syncRoutes.exportRows, auth: true },
  { method: 'POST',   pattern: /^\/api\/sync\/sequences$/,           handler: syncRoutes.seedSequences, auth: true },
  { method: 'GET',    pattern: /^\/api\/sync\/reconcile$/,           handler: syncRoutes.reconcile, auth: true }
];

async function health(_request, env) {
  const started = Date.now();

  const row = await env.DB.prepare('SELECT 1 AS ok').first();

  return {
    ok: row && row.ok === 1,
    service: 'bharath-supermarket-api',
    version: API_VERSION,
    time: new Date().toISOString(),
    dbLatencyMs: Date.now() - started
  };
}

function matchRoute(method, pathname) {
  for (const route of routes) {
    if (route.method !== method) continue;

    const match = route.pattern.exec(pathname);
    if (match) {
      return { route, params: match.slice(1) };
    }
  }
  return null;
}

export default {
  async fetch(request, env, ctx) {
    const url = new URL(request.url);
    const requestId = crypto.randomUUID();

    // CORS preflight - needed only if a browser dashboard is added later.
    if (request.method === 'OPTIONS') {
      return new Response(null, {
        status: 204,
        headers: {
          'Access-Control-Allow-Origin': request.headers.get('Origin') || '*',
          'Access-Control-Allow-Methods': 'GET,POST,PUT,DELETE,OPTIONS',
          'Access-Control-Allow-Headers': 'Content-Type,Authorization,X-Idempotency-Key',
          'Access-Control-Max-Age': '86400'
        }
      });
    }

    try {
      if (!url.pathname.startsWith('/api/')) {
        throw notFound('Unknown endpoint');
      }

      const matched = matchRoute(request.method, url.pathname);
      if (!matched) {
        throw notFound(`No route for ${request.method} ${url.pathname}`);
      }

      const { route, params } = matched;
      const user = route.auth ? await requireUser(request, env) : null;

      let body = {};
      if (request.method === 'POST' || request.method === 'PUT') {
        body = await readJson(request);
      }

      const result = await route.handler(request, env, body, user, {
        params,
        id: params[0],
        requestId
      });

      // Sales/purchases return the stored idempotent response.
      if (result && typeof result === 'object' && '__replay' in result) {
        return json(result.body, result.status, request, env);
      }

      return json(result === undefined ? { ok: true } : result, 200, request, env);
    } catch (err) {
      return errorResponse(err, request, env);
    }
  }
};
