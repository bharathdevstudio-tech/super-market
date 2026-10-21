/**
 * Byte-preserving branding replacements.
 *
 * The VB project contains files that are not UTF-8 (they have Windows-1252
 * bytes). Reading and rewriting them as text corrupts those bytes, so this
 * script operates on Buffers and only replaces exact ASCII byte sequences.
 *
 * Run: node tools/apply-branding.js
 */

const fs = require('fs');
const path = require('path');

const ROOT = 'E:/Bharath/SuperMarket App';

const REPLACEMENTS = [
  // Login window title
  ['Windows Application/FrmMiniLogIn.Designer.vb',
    'Me.Text = "Log In"',
    'Me.Text = "Bharath SuperMarket - Log In"'],

  // Status bar website label -> shop brand
  ['Windows Application/FrmMDICommon.Designer.vb',
    'Me.ToolStripStatusLabel3.Text = "Website : vistawin.online"',
    'Me.ToolStripStatusLabel3.Text = "Bharath SuperMarket"'],

  // Message box caption
  ['Windows Application/FrmDownload.vb',
    '"Folder creation completed.", "VISTAWIN"',
    '"Folder creation completed.", "Bharath SuperMarket"'],

  // Vendor file-manager window label (only the ASCII site name is touched)
  ['Windows Application/FrmDownloadVistawin.Designer.vb',
    'vistawin.store',
    'Bharath SuperMarket'],

  // Backup button (designer default)
  ['Windows Application/FrmDBBackup.Designer.vb',
    'Me.btnVistawinBackup.Text = "Send to Vistawin Server"',
    'Me.btnVistawinBackup.Text = "Backup / Upload"'],

  // Backup button (runtime reset after a completed upload)
  ['Windows Application/FrmDBBackup.vb',
    'btnVistawinBackup.Text = "Send to Vistawin Server"',
    'btnVistawinBackup.Text = "Backup / Upload"'],

  // Folder-creation error message
  ['Windows Application/FrmMDILeftMenu.vb',
    '"Unable to create VISTAWIN folders."',
    '"Unable to create the application data folders."']
];

let changed = 0;

for (const [file, from, to] of REPLACEMENTS) {
  const full = path.join(ROOT, file);

  if (!fs.existsSync(full)) {
    console.log(`  MISSING ${file}`);
    continue;
  }

  const before = fs.readFileSync(full);
  const fromBuf = Buffer.from(from, 'latin1');
  const toBuf = Buffer.from(to, 'latin1');

  const index = before.indexOf(fromBuf);

  if (index === -1) {
    console.log(`  no match  ${file}  ::  ${from.slice(0, 60)}`);
    continue;
  }

  // Replace every occurrence, but keep the rest of the bytes exactly as-is.
  let out = before;
  let count = 0;
  let at = out.indexOf(fromBuf);

  while (at !== -1) {
    out = Buffer.concat([out.subarray(0, at), toBuf, out.subarray(at + fromBuf.length)]);
    count++;
    at = out.indexOf(fromBuf, at + toBuf.length);
  }

  fs.writeFileSync(full, out);
  changed += count;
  console.log(`  ok ${count}x  ${file}`);
}

console.log(`\nreplacements applied: ${changed}`);
