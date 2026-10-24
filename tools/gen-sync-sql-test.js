/*
 * Extracts the SQL statements from Windows Application/SuperMarket/SmSyncQueue.vb
 * so they can be executed against a real database instead of being transcribed
 * by hand (transcription is exactly how column-name typos slip in).
 *
 * Output: a single batch of read-only SELECT TOP 0 statements.
 */
const fs = require('fs');
const path = require('path');

const file = path.join(__dirname, '..', 'Windows Application', 'SuperMarket', 'SmSyncQueue.vb');
const src = fs.readFileSync(file, 'utf8').split(/\r?\n/);

function isStringLine(line) {
  return /"[^"]*"/.test(line);
}

const statements = [];
for (let i = 0; i < src.length; i++) {
  if (!/Dim sql As String = _/.test(src[i])) continue;
  let j = i + 1;
  let pieces = [];
  while (j < src.length) {
    const line = src[j];
    if (!isStringLine(line)) break;
    const matches = line.match(/"[^"]*"/g) || [];
    for (const m of matches) pieces.push(m.slice(1, -1));
    const continues = /_\s*$/.test(line);
    j++;
    if (!continues) break;
  }
  statements.push(pieces.join(''));
  i = j - 1;
}

const problems = [];
const out = [];
out.push('SET NOCOUNT ON;');
out.push("DECLARE @I varchar(100), @T varchar(30);");
out.push("SET @I = '__no_such_bill__';");
out.push("SET @T = '__no_such_type__';");

statements.forEach((sql, idx) => {
  let probe = sql.replace(/^\s*SELECT TOP 1\s+/i, 'SELECT TOP 0 ');
  if (probe === sql && /^\s*SELECT\s+/i.test(sql)) {
    probe = sql.replace(/^(\s*)SELECT\s+/i, '$1SELECT TOP 0 ');
  }
  if (!/^\s*SELECT TOP 0\s/i.test(probe)) {
    problems.push('statement ' + idx + ' is not a SELECT: ' + sql.slice(0, 60));
    return;
  }
  if (!/\bFROM\s+dbo\.|FROM\s+tbl_/i.test(probe)) {
    problems.push('statement ' + idx + ' has no table: ' + sql.slice(0, 60));
    return;
  }
  out.push('PRINT ' + "'stmt " + idx + "';");
  out.push(probe + ';');
});

const target = path.join(process.env.TEMP || '.', 'sm_sync_sql_probe.sql');
fs.writeFileSync(target, out.join('\n'), 'utf8');

/* -------------------------------------------------------------------------
   The INSERT is validated separately, in a scratch database that only has the
   SM_Sync tables. Declare every parameter the VB code binds, then execute the
   exact statement text.
   ------------------------------------------------------------------------- */
const insert = statements.find(s => /^\s*INSERT INTO dbo\.SM_SyncOutbox/i.test(s));
if (insert) {
  const params = Array.from(new Set((insert.match(/@[A-Za-z_][A-Za-z0-9_]*/g) || [])));
  const decls = params.map(p => {
    switch (p) {
      case '@EventID': return 'DECLARE @EventID uniqueidentifier = NEWID();';
      case '@Payload': return "DECLARE @Payload nvarchar(max) = N'{\"test\":true}';";
      case '@ContentHash': return "DECLARE @ContentHash char(64) = REPLICATE('a',64);";
      case '@BillNo': return 'DECLARE @BillNo numeric(18,0) = 7;';
      case '@OccurredAtUtc': return 'DECLARE @OccurredAtUtc datetime = GETUTCDATE();';
      case '@SaveType': return "DECLARE @SaveType varchar(30) = 'INVOICE';";
      case '@AcYear': return "DECLARE @AcYear varchar(20) = '2026-2027';";
      case '@EntityType': return "DECLARE @EntityType varchar(30) = 'SALE';";
      case '@EntityKey': return "DECLARE @EntityKey varchar(120) = 'TEST/7';";
      case '@Operation': return "DECLARE @Operation varchar(10) = 'INSERT';";
      case '@StoreID': return "DECLARE @StoreID varchar(50) = 'STORE-1';";
      default: return 'DECLARE ' + p + ' sql_variant; -- unknown, review generated SQL';
    }
  });
  const insertSql = [
    'SET NOCOUNT ON;',
    ...decls,
    insert + ';',
    "SELECT 'inserted' AS Step, COUNT(*) AS Rows FROM dbo.SM_SyncOutbox WHERE EventID = @EventID;",
    'DELETE FROM dbo.SM_SyncOutbox WHERE EventID = @EventID;'
  ].join('\n');
  const insertTarget = path.join(process.env.TEMP || '.', 'sm_sync_insert_probe.sql');
  fs.writeFileSync(insertTarget, insertSql, 'utf8');
  console.log('insert probe written: ' + insertTarget);
}

console.log('statements extracted: ' + statements.length);
if (problems.length) {
  console.log('PROBLEMS:');
  problems.forEach(p => console.log(' - ' + p));
}
console.log('probe written: ' + target);
