/**
 * Shows how the working copy of a skip-worktree file differs from the committed
 * blob, WITHOUT printing secret values.
 *
 * Prints, for every changed line: the line number, the change sign, and either
 * "SECRET" (masked) or a short sanitized preview.
 *
 * Run: node tools/diff-skipworktree.js "Windows Application/FrmDBBackup.vb"
 */

const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');
const os = require('os');

const rel = process.argv[2];
if (!rel) {
  console.error('usage: node tools/diff-skipworktree.js "<path>"');
  process.exit(1);
}

const tmp = path.join(os.tmpdir(), 'opencode', 'head-blob.txt');
fs.mkdirSync(path.dirname(tmp), { recursive: true });

// Binary-safe extraction: no PowerShell redirection, no re-encoding.
const blob = execSync(`git show HEAD:${JSON.stringify(rel)}`, { encoding: 'buffer', maxBuffer: 64 * 1024 * 1024 });
fs.writeFileSync(tmp, blob);

let diff = '';
try {
  diff = execSync(
    `git diff --no-index --text --unified=0 -- ${JSON.stringify(tmp)} ${JSON.stringify(rel)}`,
    { encoding: 'utf8', maxBuffer: 64 * 1024 * 1024 }
  );
} catch (e) {
  // git diff exits 1 when files differ; that is the normal case.
  diff = String(e.stdout || '');
}

const SECRET = /script\.google\.com|apiKey|ApiKey|ServerApiChk|Password|Pwd|txtApiKey|"siva"|ConnectionString|Initial Catalog|Data Source|User ID|UID=|PWD=|SECRET|TOKEN/i;

let currentHeader = '';
const stats = { secret: 0, code: 0 };

for (const line of diff.split(/\r?\n/)) {
  if (line.startsWith('@@')) {
    currentHeader = line;
    console.log(`\n${line}`);
    continue;
  }
  if (!/^[+-]/.test(line) || line.startsWith('+++') || line.startsWith('---')) continue;

  const sign = line[0];
  const body = line.slice(1);
  const masked = SECRET.test(body);

  if (masked) {
    stats.secret++;
    console.log(`  ${sign} [SECRET masked] (${body.length} chars)`);
  } else {
    stats.code++;
    const preview = body.length > 70 ? body.slice(0, 70) + '...' : body;
    console.log(`  ${sign} ${preview}`);
  }
}

console.log(`\nsecret-ish changed lines: ${stats.secret}`);
console.log(`code changed lines:       ${stats.code}`);
