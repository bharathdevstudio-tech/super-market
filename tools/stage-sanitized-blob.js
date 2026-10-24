/**
 * Rebuilds the staged blob for a skip-worktree file.
 *
 * Why: FrmDBBackup.vb is marked skip-worktree, so the committed copy holds
 * placeholder credentials while the working copy holds the real ones. A normal
 * `git add` would either stage the real secrets or leave the committed copy
 * with the old vendor-upload code.
 *
 * This script takes the repaired working copy and restores the committed
 * placeholder values for the credential lines only, all other (code) changes
 * coming from the working copy.
 *
 * It never prints credential values. It fails loudly if a credential line is
 * not where it is expected, so silent mis-staging is not possible.
 *
 * Run: node tools/stage-sanitized-blob.js "Windows Application/FrmDBBackup.vb"
 */

const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');
const os = require('os');

const rel = process.argv[2];
if (!rel) {
  console.error('usage: node tools/stage-sanitized-blob.js "<path>"');
  process.exit(1);
}

const quoted = JSON.stringify(rel);

// ---------------------------------------------------------------------------
// Read both versions as raw bytes (latin1 round-trips every byte 1:1).
// ---------------------------------------------------------------------------

const headBlob = execSync(`git show HEAD:${quoted}`, { encoding: 'buffer', maxBuffer: 64 * 1024 * 1024 });
const headText = headBlob.toString('latin1');
const workText = fs.readFileSync(rel, 'latin1');

const headNl = headText.includes('\r\n') ? '\r\n' : '\n';

const headLines = headText.split(/\r?\n/);
const workLines = workText.split(/\r?\n/);

// Keep the repository's line-ending convention (HEAD's), so the staged blob is
// byte-comparable with the rest of the tree.
const work = workLines.slice();

// ---------------------------------------------------------------------------
// Locate the credential lines in both versions by their declaration prefix.
// ---------------------------------------------------------------------------

function findLine(lines, needle) {
  const hits = [];
  lines.forEach((l, i) => { if (l.includes(needle)) hits.push(i); });
  return hits;
}

/** Copies HEAD's line onto the working array at the given offsets. */
function restore(targetNeedle, label) {
  const headAt = findLine(headLines, targetNeedle);
  const workAt = findLine(work, targetNeedle);

  if (headAt.length !== 1 || workAt.length !== 1) {
    throw new Error(
      `${label}: expected exactly one match, found head=${headAt.length} work=${workAt.length}`
    );
  }

  work[workAt[0]] = headLines[headAt[0]];
  console.log(`  restored committed placeholder: ${label} (line ${workAt[0] + 1})`);
}

restore('Private googleAppsScriptApiUrl As String =', 'googleAppsScriptApiUrl');
restore('Private apiKey As String =', 'apiKey');
restore('txtApiKey.Text = "https://script.google.com', 'googleAppsScriptApiUrl default in Load');

// ---------------------------------------------------------------------------
// Safety net: the staged blob must not contain the vendor endpoint or any
// obviously real credential.
// ---------------------------------------------------------------------------

const staged = work.join(headNl);

const forbidden = [
  ['vendor upload endpoint', /https:\/\/vistawin\.store\/ClientSQLDB\/upload_api\.php/],
  ['real apps script deployment URL', /script\.google\.com\/macros\/s\/AKfycb/],
];

let failure = false;
for (const [label, re] of forbidden) {
  if (re.test(staged)) {
    console.error(`  FAIL: staged blob still contains ${label}`);
    failure = true;
  }
}

// The plain-text placeholder must still be present, proving the swap happened.
if (!/YOUR_GOOGLE_APPS_SCRIPT/i.test(staged) && !/YOUR_API_KEY/i.test(staged)) {
  console.error('  FAIL: staged blob has no recognisable placeholder credential');
  failure = true;
}

if (failure) {
  console.error('\nAborted. Index was not modified.');
  process.exit(1);
}

// ---------------------------------------------------------------------------
// Write the blob, register it, and re-apply skip-worktree.
// ---------------------------------------------------------------------------

const tmp = path.join(os.tmpdir(), 'opencode', 'staged-blob.bin');
fs.mkdirSync(path.dirname(tmp), { recursive: true });
fs.writeFileSync(tmp, Buffer.from(staged, 'latin1'));

const hash = execSync(`git hash-object -w ${JSON.stringify(tmp)}`, { encoding: 'utf8' }).trim();
console.log(`\n  blob ${hash}`);

execSync(`git update-index --cacheinfo 100644,${hash},${quoted}`, { stdio: 'inherit' });
execSync(`git update-index --skip-worktree ${quoted}`, { stdio: 'inherit' });

console.log('  index updated, skip-worktree re-applied');
