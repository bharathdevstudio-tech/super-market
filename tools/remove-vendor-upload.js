/**
 * Removes the legacy vendor upload from FrmDBBackup.
 *
 * The form used to compress a file and POST it in chunks to
 * https://vistawin.store/ClientSQLDB/upload_api.php - the previous vendor's
 * server. That sends the shop's data to a third party nobody here controls.
 *
 * After this change the button only creates the ZIP archive locally. The
 * separate "Google Drive Backup" button is untouched.
 *
 * The same transformation has to be applied to two copies of the file:
 *   - the working copy (real local credentials)
 *   - the committed copy (placeholder credentials, marked skip-worktree)
 * so this lives in a script with no secret-dependent logic, and both copies get
 * byte-identical code changes.
 *
 * Run: node tools/remove-vendor-upload.js "<path to FrmDBBackup.vb>"
 */

const fs = require('fs');

const file = process.argv[2];

if (!file) {
  console.error('usage: node tools/remove-vendor-upload.js "<path to FrmDBBackup.vb>"');
  process.exit(1);
}

let text = fs.readFileSync(file, 'latin1');
const nl = text.includes('\r\n') ? '\r\n' : '\n';

let applied = 0;

function replaceOnce(name, from, to) {
  const at = text.indexOf(from);
  if (at === -1) {
    console.log(`  skip (not found): ${name}`);
    return false;
  }
  text = text.slice(0, at) + to + text.slice(at + from.length);
  applied++;
  console.log(`  applied: ${name}`);
  return true;
}

/** Replaces a span from a single-line start marker up to a single-line end marker. */
function replaceSpan(name, startMarker, endMarker, replacement) {
  const start = text.indexOf(startMarker);
  if (start === -1) {
    console.log(`  skip (start not found): ${name}`);
    return false;
  }
  const end = text.indexOf(endMarker, start);
  if (end === -1) {
    console.log(`  skip (end not found): ${name}`);
    return false;
  }
  text = text.slice(0, start) + replacement + text.slice(end);
  applied++;
  console.log(`  applied: ${name}`);
  return true;
}

// ---------------------------------------------------------------------------
// 1. Drop the vendor endpoint constants
// ---------------------------------------------------------------------------

replaceOnce(
  'vendor constants',
  `    Private Const PHP_API_URL As String = "https://vistawin.store/ClientSQLDB/upload_api.php"${nl}` +
  `    Private Const API_KEY As String = "MY_SECRET_KEY_123"${nl}`,
  `    ' Remote upload to the previous vendor (vistawin.store) was removed.${nl}` +
  `    ' This form now creates the backup archive locally, on this machine only.${nl}`
);

// ---------------------------------------------------------------------------
// 2. No internet requirement: the operation is local now
// ---------------------------------------------------------------------------

replaceOnce(
  'internet check',
  `        If My.Computer.Network.IsAvailable Then${nl}` +
  `${nl}` +
  `        Else${nl}` +
  `            MsgBox("Please,check your internet connection")${nl}` +
  `            Exit Sub${nl}` +
  `        End If${nl}` +
  `${nl}`,
  ``
);

// ---------------------------------------------------------------------------
// 3. Background worker: compress only, no upload
// ---------------------------------------------------------------------------

const newWorker = [
  `    ' ── Background worker: build the backup archive ─`,
  `    Private Sub worker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles worker.DoWork`,
  ``,
  `        Dim bw As BackgroundWorker = CType(sender, BackgroundWorker)`,
  `        Dim zipPath As String = _filePath & ".zip"`,
  ``,
  `        Try`,
  `            ' ── STEP 1: Compress to ZIP ───────────`,
  `            bw.ReportProgress(0, "Compressing " & _fileName & "...")`,
  ``,
  `            CompressToZip(_filePath, _fileName, zipPath)`,
  ``,
  `            If bw.CancellationPending Then`,
  `                e.Cancel = True`,
  `                CleanupZip(zipPath)`,
  `                Return`,
  `            End If`,
  ``,
  `            ' ── STEP 2: Verify the archive exists ─`,
  `            Dim fi As New FileInfo(zipPath)`,
  ``,
  `            If Not fi.Exists OrElse fi.Length = 0 Then`,
  `                Throw New Exception("The compressed archive was not created.")`,
  `            End If`,
  ``,
  `            bw.ReportProgress(100, "Archive created: " & zipPath)`,
  `            e.Result = "LOCAL:" & zipPath`,
  ``,
  `        Catch ex As Exception`,
  `            CleanupZip(zipPath)`,
  `            e.Result = "ERROR:" & ex.Message`,
  `        End Try`,
  ``,
  `    End Sub`,
  ``
].join(nl);

replaceSpan(
  'worker_DoWork body',
  '    Private Sub worker_DoWork',
  '    End Sub',
  newWorker
);

// ---------------------------------------------------------------------------
// 4. Completion handler wording and result handling
// ---------------------------------------------------------------------------

replaceOnce(
  'cancel message',
  `            lblStatus.Text = "Upload cancelled."`,
  `            lblStatus.Text = "Cancelled."`
);

const newResultHandling = [
  `        If result.StartsWith("ERROR:") Then`,
  `            MessageBox.Show( _`,
  `                "Backup error:" & Environment.NewLine & result.Substring(6), _`,
  `                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)`,
  `            lblStatus.Text = "Backup failed."`,
  `        ElseIf result.StartsWith("LOCAL:") Then`,
  `            Dim savedPath As String = result.Substring(6)`,
  `            MessageBox.Show( _`,
  `                "Backup archive created:" & Environment.NewLine & savedPath, _`,
  `                "Backup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)`,
  `            lblStatus.Text = "Archive created: " & Path.GetFileName(savedPath)`,
  `        End If`
].join(nl);

replaceSpan(
  'result handling',
  '        Dim result As String = CStr(e.Result)',
  '        End If',
  newResultHandling
);

// ---------------------------------------------------------------------------
// 5. Delete the chunked-upload and HTTP helpers
//
//    PostChunk / PostFinalize / PostCancel / SendPost / HandleApiResponse were
//    all only used by the vendor upload.
// ---------------------------------------------------------------------------

replaceSpan(
  'chunked upload helpers',
  '    Private Function PostChunk( _',
  '    Private Function ExtractJsonValue(',
  ``
);

// ---------------------------------------------------------------------------
// 6. Button labels
// ---------------------------------------------------------------------------

replaceOnce(
  'button label (running reset)',
  `            btnVistawinBackup.Text = "Backup / Upload"`,
  `            btnVistawinBackup.Text = "Create Backup"`
);

replaceOnce(
  'button label (completed)',
  `        btnVistawinBackup.Text = "Send to Server"`,
  `        btnVistawinBackup.Text = "Create Backup"`
);

fs.writeFileSync(file, text, 'latin1');

console.log(`\n${applied} change(s) written to ${file}`);
