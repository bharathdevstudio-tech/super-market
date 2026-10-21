/**
 * Updates the index (staged) copy of a skip-worktree file without touching the
 * working-tree copy, which holds real local credentials.
 *
 * Why this exists: four files are marked skip-worktree because the committed
 * version has placeholder secrets while the working copy has real ones. A
 * branding change in one of those files would otherwise exist only locally and
 * a fresh clone would be inconsistent.
 *
 * This script:
 *   1. reads the exact HEAD blob as binary (no PowerShell re-encoding)
 *   2. applies a byte replacement declared below
 *   3. writes the new blob with git hash-object -w
 *   4. points the index at it and restores the skip-worktree bit
 *
 * It never writes the working-tree copy, so credentials cannot leak.
 *
 * Run: node tools/patch-skipworktree-index.js "Windows Application/FrmDBBackup.vb"
 */

const { execSync } = require('child_process');
const path = require('path');
const os = require('os');
const fs = require('fs');

const ROOT = 'E:/Bharath/SuperMarket App';

// Declared here rather than passed on the command line: PowerShell drops the
// embedded double quotes when handing arguments to a native executable.
const PATCHES = {
  'Windows Application/FrmDBBackup.vb': [
    [
      'btnVistawinBackup.Text = "Send to Vistawin Server"',
      'btnVistawinBackup.Text = "Backup / Upload"'
    ]
  ]
};

const rel = process.argv[2];

if (!rel || !PATCHES[rel]) {
  console.error(`usage: node tools/patch-skipworktree-index.js <one of: ${Object.keys(PATCHES).join(', ')}>`);
  process.exit(1);
}

const git = (args, opts = {}) =>
  execSync(`git ${args}`, { cwd: ROOT, maxBuffer: 64 * 1024 * 1024, ...opts });

let blob = git(`cat-file -p ${JSON.stringify(`HEAD:${rel}`)}`, { encoding: 'buffer' });

for (const [fromText, toText] of PATCHES[rel]) {
  const from = Buffer.from(fromText, 'latin1');
  const to = Buffer.from(toText, 'latin1');

  const at = blob.indexOf(from);
  if (at === -1) {
    console.error(`committed blob does not contain: ${fromText}`);
    process.exit(2);
  }

  blob = Buffer.concat([blob.subarray(0, at), to, blob.subarray(at + from.length)]);
  console.log(`patched: ${fromText}`);
}

const tmp = path.join(os.tmpdir(), 'skipworktree-patch.bin');
fs.writeFileSync(tmp, blob);

const hash = git(`hash-object -w ${JSON.stringify(tmp)}`).toString().trim();

git(`update-index --cacheinfo 100644,${hash},${JSON.stringify(rel)}`);
git(`update-index --skip-worktree ${JSON.stringify(rel)}`);

const staged = git(`rev-parse ${JSON.stringify(`:${rel}`)}`).toString().trim();

console.log(`new index blob: ${hash}`);
console.log(`index points at: ${staged}`);
console.log('working tree untouched (real credentials preserved)');
