/**
 * Money and quantity rules.
 *
 * The legacy SQL Server schema stores money as NUMERIC(18,2) and the app
 * rounds with MidpointRounding.AwayFromZero (VSuperMarket.RoundOff). D1/JS
 * numbers are IEEE-754 doubles, so every money value that enters or leaves
 * the API is rounded to 2 decimals here, with the same half-away-from-zero
 * rule, so totals match the desktop application.
 *
 * The server is the authority: client-sent totals are never trusted.
 */

import { badRequest } from './http.js';

/** Largest amount we accept. Guards against overflow and fat-finger input. */
const MAX_AMOUNT = 99999999.99;

export function round2(value) {
  const n = Number(value);
  if (!Number.isFinite(n)) return 0;
  const sign = n < 0 ? -1 : 1;
  return sign * Math.round(Math.abs(n) * 100 + 1e-9) / 100;
}

/** Round to whole rupees (the desktop's RoundOff for the final payable amount). */
export function roundOff(value) {
  const n = Number(value);
  if (!Number.isFinite(n)) return 0;
  const sign = n < 0 ? -1 : 1;
  return sign * Math.round(Math.abs(n) + 1e-9);
}

export function toNumber(value, fieldName) {
  if (value === null || value === undefined || value === '') return 0;
  const n = Number(value);
  if (!Number.isFinite(n)) {
    throw badRequest(`Field "${fieldName}" must be a number`);
  }
  return n;
}

/** Parse and range-check a money value. */
export function money(value, fieldName, { allowNegative = false, max = MAX_AMOUNT } = {}) {
  const n = round2(toNumber(value, fieldName));

  if (!allowNegative && n < 0) {
    throw badRequest(`Field "${fieldName}" cannot be negative`);
  }
  if (Math.abs(n) > max) {
    throw badRequest(`Field "${fieldName}" is out of range`);
  }
  return n;
}

/** Parse a quantity (> 0 by default). */
export function quantity(value, fieldName, { allowZero = false } = {}) {
  const n = toNumber(value, fieldName);

  if (!Number.isFinite(n)) throw badRequest(`Field "${fieldName}" must be a number`);
  if (allowZero ? n < 0 : n <= 0) {
    throw badRequest(`Field "${fieldName}" must be greater than ${allowZero ? 'or equal to ' : ''}0`);
  }
  if (n > 1000000) throw badRequest(`Field "${fieldName}" is out of range`);

  return n;
}

/** Percentage 0..100. */
export function percent(value, fieldName) {
  const n = toNumber(value, fieldName);

  if (n < 0 || n > 100) throw badRequest(`Field "${fieldName}" must be between 0 and 100`);
  return n;
}

/**
 * Recomputes one sale line the same way the POS does:
 *   taxable   = rate * qty - discount
 *   CGST/SGST = taxable * percent / 2 / 100  (or IGST)
 *   total     = taxable + tax
 */
export function computeSaleLine(line) {
  const qty = quantity(line.qty, 'qty');
  const rate = money(line.rate, 'rate', { allowNegative: true });
  const discount = money(line.discount || 0, 'discount');
  const cgstPercent = percent(line.cgstPercent || 0, 'cgstPercent');
  const sgstPercent = percent(line.sgstPercent || 0, 'sgstPercent');
  const igstPercent = percent(line.igstPercent || 0, 'igstPercent');

  const gross = round2(rate * qty);
  const taxable = round2(gross - discount);

  if (taxable < 0) {
    throw badRequest('Discount cannot be greater than the line amount');
  }

  const cgstAmount = round2(taxable * (cgstPercent / 2) / 100);
  const sgstAmount = round2(taxable * (sgstPercent / 2) / 100);
  const igstAmount = round2(taxable * (igstPercent / 100));

  return {
    qty,
    rate,
    discount,
    gross,
    taxable,
    cgstPercent,
    sgstPercent,
    igstPercent,
    cgstAmount,
    sgstAmount,
    igstAmount,
    total: round2(taxable + cgstAmount + sgstAmount + igstAmount)
  };
}

/**
 * Totals for a bill. Round-off is applied to the final payable amount, which
 * is what the desktop prints on the bill.
 */
export function computeBillTotals(lines, { billDiscount = 0 } = {}) {
  let taxable = 0;
  let gst = 0;
  let gross = 0;

  for (const line of lines) {
    taxable = round2(taxable + line.taxable);
    gst = round2(gst + line.cgstAmount + line.sgstAmount + line.igstAmount);
    gross = round2(gross + line.gross);
  }

  const discount = money(billDiscount, 'billDiscount');
  const beforeRound = round2(taxable + gst - discount);

  if (beforeRound < 0) {
    throw badRequest('Bill discount cannot exceed the bill total');
  }

  const finalTotal = roundOff(beforeRound);
  const roundOffAmount = round2(finalTotal - beforeRound);

  return { gross, taxable, gst, billDiscount: discount, beforeRound, roundOff: roundOffAmount, finalTotal };
}

