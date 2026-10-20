/**
 * VISTAWIN SuperMarket - free semi-online sync backend.
 * =====================================================
 *
 * Google Apps Script + Google Sheets.  No server, no database fee,
 * no credit card.  This script implements exactly the four actions that
 * Windows Application\Others\VHostingSync.vb already calls:
 *
 *   GET  ?action=tables                  -> {"data":[{"name":"tbl_X"}, ...]}
 *   GET  ?action=data&table=tbl_X        -> {"data":[{col:val, ...}, ...]}
 *   POST ?action=clear&table=tbl_X       -> {"status":"success"}
 *   POST ?action=insert&table=tbl_X      -> {"status":"success","inserted":N}
 *        body: {"data":[{col:val, ...}, ...]}
 *
 * Each SQL table becomes one sheet; row 1 holds the column names.
 * The desktop app clears the sheet and re-inserts the whole table on every
 * push, so the sheets always mirror the shop's SQL Server.
 *
 * ---------------------------------------------------------------------------
 * SETUP (about 5 minutes, free)
 * ---------------------------------------------------------------------------
 *  1. Open https://sheets.google.com and create a blank spreadsheet,
 *     for example "SuperMarket Sync".
 *  2. In that sheet: Extensions -> Apps Script.
 *  3. Delete the sample code and paste this whole file.
 *  4. Deploy -> New deployment -> type "Web app".
 *       Execute as:      Me
 *       Who has access:  Anyone
 *     (access must be "Anyone" - the desktop app has no Google login)
 *  5. Copy the /exec URL, e.g.
 *       https://script.google.com/macros/s/XXXXXXXXXXXX/exec
 *  6. Paste it into SM_SYNC_API in
 *     Windows Application\Others\VHostingSync.vb and rebuild.
 *
 * The URL is the same for push and pull; the action parameter decides.
 *
 * NOTE: "Anyone" means anybody who knows the URL can overwrite the sheets.
 * The sheets are only a copy - the shop's SQL Server stays the source of
 * truth - but if you need protection, set SHARED_TOKEN below and have the
 * app send it (see the comment there).
 */

/** Leave blank when the script is bound to the spreadsheet (created from the sheet). */
var SPREADSHEET_ID = '';

/**
 * Optional shared secret.  Leave blank for now: the current VB client does
 * not send a token.  If you set it here you must also send
 * "&token=..." from the client, otherwise every call is rejected.
 */
var SHARED_TOKEN = '';

/** Never expose these as syncable tables. */
var BLOCKED_SHEETS = ['Sheet1', 'SyncLog'];

// ---------------------------------------------------------------------------

function doGet(e) {
  return handleRequest(e, null);
}

function doPost(e) {
  var body = (e && e.postData && e.postData.contents) ? e.postData.contents : '{}';
  return handleRequest(e, body);
}

function handleRequest(e, body) {
  try {
    var params = (e && e.parameter) ? e.parameter : {};

    if (SHARED_TOKEN) {
      var token = String(params.token || '');
      if (token !== SHARED_TOKEN) {
        return output({ status: 'error', message: 'Invalid token', data: [] });
      }
    }

    var action = String(params.action || '').toLowerCase();
    var table = String(params.table || '').trim();

    switch (action) {
      case 'tables':
        return output({ status: 'success', data: listTables() });

      case 'data':
        if (!table) return output({ status: 'error', message: 'table is required', data: [] });
        return output({ status: 'success', data: readTable(table) });

      case 'clear':
        if (!table) return output({ status: 'error', message: 'table is required', data: [] });
        return output(clearTable(table));

      case 'insert':
        if (!table) return output({ status: 'error', message: 'table is required', data: [] });
        return output(insertRows(table, body));

      default:
        return output({ status: 'error', message: 'Unknown action: ' + action, data: [] });
    }
  } catch (err) {
    return output({ status: 'error', message: String(err), data: [] });
  }
}

// ---------------------------------------------------------------------------
// Spreadsheet helpers
// ---------------------------------------------------------------------------

function getSpreadsheet() {
  if (SPREADSHEET_ID) return SpreadsheetApp.openById(SPREADSHEET_ID);

  var active = SpreadsheetApp.getActiveSpreadsheet();
  if (!active) {
    throw new Error('Bind this script to a spreadsheet, or set SPREADSHEET_ID.');
  }
  return active;
}

function getSheet(name, createIfMissing) {
  var ss = getSpreadsheet();
  var sheet = ss.getSheetByName(name);

  if (!sheet && createIfMissing) {
    sheet = ss.insertSheet(name);
  }

  return sheet;
}

function listTables() {
  var ss = getSpreadsheet();
  var sheets = ss.getSheets();
  var out = [];

  for (var i = 0; i < sheets.length; i++) {
    var name = sheets[i].getName();

    if (BLOCKED_SHEETS.indexOf(name) >= 0) continue;
    if (name.charAt(0) === '_') continue;          // internal sheets
    if (sheets[i].getLastRow() < 1) continue;      // empty sheet

    out.push({ name: name });
  }

  return out;
}

function readTable(name) {
  var sheet = getSheet(name, false);
  if (!sheet) return [];

  var lastRow = sheet.getLastRow();
  var lastCol = sheet.getLastColumn();
  if (lastRow < 2 || lastCol < 1) return [];

  var values = sheet.getRange(1, 1, lastRow, lastCol).getValues();
  var headers = values[0];
  var rows = [];
  var timeZone = Session.getScriptTimeZone();

  for (var r = 1; r < values.length; r++) {
    var row = {};
    var empty = true;

    for (var c = 0; c < headers.length; c++) {
      var key = String(headers[c] || '').trim();
      if (!key) continue;

      var value = values[r][c];

      if (value instanceof Date) {
        value = Utilities.formatDate(value, timeZone, 'yyyy-MM-dd HH:mm:ss');
      } else if (value === null || value === undefined) {
        value = '';
      } else {
        value = String(value);
      }

      row[key] = value;
      if (value !== '') empty = false;
    }

    if (!empty) rows.push(row);
  }

  return rows;
}

function clearTable(name) {
  var lock = LockService.getScriptLock();
  lock.waitLock(30000);

  try {
    var sheet = getSheet(name, false);
    if (!sheet) return { status: 'success', message: 'table did not exist', data: [] };

    var lastRow = sheet.getLastRow();
    var lastCol = sheet.getLastColumn();

    if (lastRow > 1 && lastCol > 0) {
      sheet.getRange(2, 1, lastRow - 1, lastCol).clearContent();
    }

    return { status: 'success', message: 'cleared', data: [] };
  } finally {
    lock.releaseLock();
  }
}

function insertRows(name, body) {
  var payload = {};

  try {
    payload = JSON.parse(body || '{}');
  } catch (err) {
    return { status: 'error', message: 'Invalid JSON body: ' + err, data: [] };
  }

  var rows = payload.data || [];
  if (!Array.isArray(rows) || rows.length === 0) {
    return { status: 'success', inserted: 0, message: 'no rows', data: [] };
  }

  var lock = LockService.getScriptLock();
  lock.waitLock(30000);

  try {
    var sheet = getSheet(name, true);

    // Header = existing columns + any new ones, in first-seen order.
    var headers = [];

    var lastCol = sheet.getLastColumn();
    if (sheet.getLastRow() >= 1 && lastCol >= 1) {
      var existing = sheet.getRange(1, 1, 1, lastCol).getValues()[0];
      for (var i = 0; i < existing.length; i++) {
        var h = String(existing[i] || '').trim();
        if (h) headers.push(h);
      }
    }

    for (var r = 0; r < rows.length; r++) {
      for (var key in rows[r]) {
        if (Object.prototype.hasOwnProperty.call(rows[r], key) && headers.indexOf(key) < 0) {
          headers.push(key);
        }
      }
    }

    if (headers.length === 0) {
      return { status: 'error', message: 'No columns in payload', data: [] };
    }

    sheet.getRange(1, 1, 1, headers.length).setValues([headers]);

    var matrix = [];
    for (var m = 0; m < rows.length; m++) {
      var line = [];
      for (var c = 0; c < headers.length; c++) {
        var v = rows[m][headers[c]];
        line.push((v === undefined || v === null) ? '' : String(v));
      }
      matrix.push(line);
    }

    var startRow = sheet.getLastRow() + 1;
    if (startRow < 2) startRow = 2;

    sheet.getRange(startRow, 1, matrix.length, headers.length).setValues(matrix);

    // Keep the newest rows readable without scrolling forever.
    sheet.setFrozenRows(1);

    return { status: 'success', inserted: matrix.length, message: 'inserted', data: [] };
  } finally {
    lock.releaseLock();
  }
}

// ---------------------------------------------------------------------------

function output(obj) {
  return ContentService
    .createTextOutput(JSON.stringify(obj))
    .setMimeType(ContentService.MimeType.JSON);
}
