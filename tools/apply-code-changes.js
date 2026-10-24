/**
 * Applies only the code changes (vendor upload removal + local backup) to
 * any copy of FrmDBBackup.vb without touching credentials.
 *
 * Idempotent and safe to run on the sanitized HEAD blob or on the working copy.
 * Uses only structural/code markers, never secret values.
 *
 * Run: node tools/apply-code-changes.js input.vb output.vb
 */

const fs = require('fs');

const inp = process.argv[2];
const out = process.argv[3];

if (!inp || !out) {
  console.error('usage: node tools/apply-code-changes.js input.vb output.vb');
  process.exit(1);
}

const text = fs.readFileSync(inp, 'latin1');
const nl = text.includes('\r\n') ? '\r\n' : '\n';
let lines = text.split(/\r?\n/);

const rm = (cond, label) => {
  const before = lines.length;
  lines = lines.filter((l, i) => {
    if (!cond(l, i)) return true;
    return false;
  });
  if (lines.length !== before) console.log(`  removed: ${label}`);
};

const has = (s) => (l) => l.includes(s);

// 1. Vendor constants + CHUNK_SIZE
rm(has('Private Const PHP_API_URL'), 'PHP_API_URL');
rm(has('Private Const API_KEY As String'), 'API_KEY');
rm(has('Private Const CHUNK_SIZE'), 'CHUNK_SIZE');

// 2. Internet check
const avail = lines.findIndex((l) => l.includes('My.Computer.Network.IsAvailable'));
if (avail !== -1) {
  let end = avail;
  while (end < lines.length && !lines[end].includes('Exit Sub')) end++;
  if (end < lines.length) {
    // Remove from avail up to and including Exit Sub and following blank lines
    let delEnd = end + 1;
    while (delEnd < lines.length && lines[delEnd].trim() === '') delEnd++;
    lines.splice(avail, delEnd - avail);
    console.log('  removed: internet check');
  }
}

// 3. Normalize worker_DoWork
const w = lines.findIndex((l) => l.includes('Private Sub worker_DoWork'));
if (w !== -1) {
  // Remove preceding worker comments
  let start = w;
  while (start > 0) {
    const p = lines[start - 1];
    if (p.includes('Background worker') || p.includes('compress then upload') || p.includes('build the backup') || p.includes('STEP')) {
      start--;
      continue;
    }
    if (p.trim().startsWith("'") && p.trim().length < 80 && start < w) { start--; continue; }
    break;
  }
  // Find end of method
  let end = w;
  let depth = 0;
  for (let i = w; i < lines.length; i++) {
    if (/^\s*(Public|Private|Protected|Friend).*\b(Sub|Function)\b/.test(lines[i]) && i > w) break;
    if (lines[i].trim() === 'End Sub') { end = i; break; }
  }
  const repl = [
    "    ' Builds the backup archive locally. No data leaves this machine.",
    "    Private Sub worker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles worker.DoWork",
    "",
    "        Dim bw As BackgroundWorker = CType(sender, BackgroundWorker)",
    "        Dim zipPath As String = _filePath & \".zip\"",
    "",
    "        Try",
    "            ' STEP 1: compress the file",
    "            bw.ReportProgress(0, \"Compressing \" & _fileName & \"...\")",
    "",
    "            CompressToZip(_filePath, _fileName, zipPath)",
    "",
    "            If bw.CancellationPending Then",
    "                e.Cancel = True",
    "                CleanupZip(zipPath)",
    "                Return",
    "            End If",
    "",
    "            ' STEP 2: verify the archive exists",
    "            Dim fi As New FileInfo(zipPath)",
    "",
    "            If Not fi.Exists OrElse fi.Length = 0 Then",
    "                Throw New Exception(\"The compressed archive was not created.\")",
    "            End If",
    "",
    "            bw.ReportProgress(100, \"Archive created: \" & zipPath)",
    "            e.Result = \"LOCAL:\" & zipPath",
    "",
    "        Catch ex As Exception",
    "            CleanupZip(zipPath)",
    "            e.Result = \"ERROR:\" & ex.Message",
    "        End Try",
    "",
    "    End Sub",
  ];
  lines.splice(start, end - start + 1, ...repl);
  console.log('  replaced: worker_DoWork');
}

// Re-find after splice
const w2 = lines.findIndex((l) => l.includes('Private Sub worker_DoWork'));
if (w2 !== -1) {
  // ensure single End Sub after
  // (repl ends with End Sub)
}

// 4. Button labels
for (let i = 0; i < lines.length; i++) {
  if (lines[i].includes('btnVistawinBackup.Text = "Backup / Upload"')) {
    lines[i] = lines[i].replace('"Backup / Upload"', '"Create Backup"');
    console.log('  updated: button label');
  }
  if (lines[i].includes('btnVistawinBackup.Text = "Send to Server"')) {
    lines[i] = lines[i].replace('"Send to Server"', '"Create Backup"');
    console.log('  updated: button label');
  }
}

// 5. Completion handler
const rc = lines.findIndex((l) => l.includes('Private Sub worker_RunWorkerCompleted'));
if (rc !== -1) {
  let end = rc;
  for (let i = rc; i < lines.length; i++) {
    if (lines[i].trim() === 'End Sub') { end = i; break; }
  }
  const repl = [
    "    Private Sub worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted",
    "",
    "        txtFolderPath.Enabled = True",
    "        txtFilePrefix.Enabled = True",
    "        btnVistawinBackup.Text = \"Create Backup\"",
    "        btnVistawinBackup.Enabled = True",
    "        Me.Cursor = Cursors.Default",
    "",
    "        If e.Cancelled OrElse _cancelled Then",
    "            progressBar.Visible = False",
    "            lblPct.Visible = False",
    "            lblStatus.Text = \"Cancelled.\"",
    "            Return",
    "        End If",
    "",
    "        progressBar.Value = 100",
    "        lblPct.Text = \"100%\"",
    "        System.Threading.Thread.Sleep(400)",
    "        progressBar.Visible = False",
    "        lblPct.Visible = False",
    "",
    "        Dim result As String = CStr(e.Result)",
    "        If result.StartsWith(\"ERROR:\") Then",
    "            MessageBox.Show( _",
    "                \"Backup error:\" & Environment.NewLine & result.Substring(6), _",
    "                \"Error\", MessageBoxButtons.OK, MessageBoxIcon.Error)",
    "            lblStatus.Text = \"Backup failed.\"",
    "        ElseIf result.StartsWith(\"LOCAL:\") Then",
    "            Dim savedPath As String = result.Substring(6)",
    "            MessageBox.Show( _",
    "                \"Backup archive created:\" & Environment.NewLine & savedPath, _",
    "                \"Backup Complete\", MessageBoxButtons.OK, MessageBoxIcon.Information)",
    "            lblStatus.Text = \"Archive created: \" & Path.GetFileName(savedPath)",
    "        End If",
    "",
    "    End Sub",
  ];
  lines.splice(rc, end - rc + 1, ...repl);
  console.log('  replaced: worker_RunWorkerCompleted');
}

// 6. Remove chunked upload helpers
const pc = lines.findIndex((l) => l.includes('Private Function PostChunk('));
const ej = lines.findIndex((l) => l.includes('Private Function ExtractJsonValue('));
if (pc !== -1 && ej !== -1 && ej > pc) {
  lines.splice(pc, ej - pc);
  console.log('  removed: chunked upload helpers');
}

// 7. Collapse duplicate End Sub/End If
for (let i = lines.length - 1; i > 0; i--) {
  if (lines[i].trim() === 'End Sub' && lines[i - 1].trim() === 'End Sub') lines.splice(i, 1);
}
for (let i = 0; i < lines.length; i++) {
  if (/^\s*End If\s+End If\s*$/.test(lines[i])) lines[i] = lines[i].replace(/\s+End If\s*$/, '');
}

fs.writeFileSync(out, lines.join(nl), 'latin1');
console.log('wrote ' + out);
