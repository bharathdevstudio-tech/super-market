/**
 * Repairs the damage done by running remove-vendor-upload.js twice.
 *
 * The span helper kept the original end marker, so the second run duplicated
 * `End Sub` / `End If`, and the first replacement consumed the
 * `Dim result As String = CStr(e.Result)` declaration.
 *
 * This script is idempotent: running it twice leaves the same result.
 *
 * Run: node tools/repair-backup-form.js "<path to FrmDBBackup.vb>"
 */

const fs = require('fs');

const file = process.argv[2];
if (!file) {
  console.error('usage: node tools/repair-backup-form.js "<path to FrmDBBackup.vb>"');
  process.exit(1);
}

const text = fs.readFileSync(file, 'latin1');
const nl = text.includes('\r\n') ? '\r\n' : '\n';
let lines = text.split(nl);
let changes = 0;

const log = (m) => { changes++; console.log(`  ${m}`); };

// ---------------------------------------------------------------------------
// 1. Remove the duplicated / stale "Background worker" comment lines and keep
//    exactly one plain-ASCII comment above worker_DoWork.
// ---------------------------------------------------------------------------

const workerAt = lines.findIndex((l) => l.includes('Private Sub worker_DoWork'));

if (workerAt > 0) {
  let firstComment = workerAt;
  while (firstComment > 0) {
    const prev = lines[firstComment - 1];
    const isWorkerComment =
      prev.includes('Background worker') ||
      prev.includes('compress then upload') ||
      prev.includes('STEP 1: Compress') ||
      prev.trim() === "'";
    if (!isWorkerComment) break;
    firstComment--;
  }

  if (firstComment < workerAt) {
    lines.splice(firstComment, workerAt - firstComment, "    ' Builds the backup archive locally. No data leaves this machine.");
    log('normalised worker_DoWork comment to one ASCII line');
  }
}

// ---------------------------------------------------------------------------
// 2. Collapse duplicated consecutive "    End Sub" lines.
// ---------------------------------------------------------------------------

for (let i = lines.length - 1; i > 0; i--) {
  if (lines[i].trim() === 'End Sub' && lines[i - 1].trim() === 'End Sub') {
    lines.splice(i, 1);
    log('removed duplicated End Sub');
  }
}

// ---------------------------------------------------------------------------
// 3. Collapse duplicated "End If" on one line: "End If        End If".
// ---------------------------------------------------------------------------

for (let i = 0; i < lines.length; i++) {
  if (/^\s*End If\s+End If\s*$/.test(lines[i])) {
    lines[i] = lines[i].replace(/End If\s+End If/, 'End If');
    log('merged duplicated End If');
  }
}

// ---------------------------------------------------------------------------
// 4. Restore the missing "Dim result As String = CStr(e.Result)" declaration.
// ---------------------------------------------------------------------------

const usesResult = lines.findIndex((l) => l.includes('If result.StartsWith("ERROR:")'));
const declaresResult = lines.some((l) => l.includes('Dim result As String = CStr(e.Result)'));

if (usesResult !== -1 && !declaresResult) {
  // Walk back over blank lines so the declaration sits right before the first
  // use with one blank line separating it from the progress-bar block above.
  let insertAt = usesResult;
  lines.splice(insertAt, 0, '', '        Dim result As String = CStr(e.Result)');
  log('restored missing Dim result declaration');
}

// ---------------------------------------------------------------------------
// 5. Drop comments that still describe the old upload behaviour.
// ---------------------------------------------------------------------------

const staleMarkers = ['Upload complete', 'Send one chunk to PHP', 'compress then upload'];
lines = lines.filter((l) => {
  const isComment = l.trim().startsWith("'");
  if (isComment && staleMarkers.some((m) => l.includes(m))) {
    changes++;
    return false;
  }
  return true;
});

// ---------------------------------------------------------------------------
// 6. CHUNK_SIZE was only used by the chunked upload.
// ---------------------------------------------------------------------------

const before = lines.length;
lines = lines.filter((l) => !l.includes('Private Const CHUNK_SIZE As Integer'));
if (lines.length !== before) log('removed unused CHUNK_SIZE constant');

// ---------------------------------------------------------------------------
// 7. Tidy the two mangled STEP comments inside worker_DoWork.
// ---------------------------------------------------------------------------

for (let i = 0; i < lines.length; i++) {
  if (lines[i].includes('STEP 1: Compress to ZIP')) {
    lines[i] = "            ' STEP 1: compress the file";
  } else if (lines[i].includes('STEP 2: Verify the archive exists')) {
    lines[i] = "            ' STEP 2: verify the archive exists";
  }
}

fs.writeFileSync(file, lines.join(nl), 'latin1');
console.log(`\n${changes} repair(s) written to ${file}`);
