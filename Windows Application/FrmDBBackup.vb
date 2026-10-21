Imports System
Imports System.IO
Imports System.IO.Packaging
Imports System.Net
Imports System.Text
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class FrmDBBackup
    Dim Command As New SqlCommand
    ' ─────────────────────────────────────────────
    '  DEFAULT VALUES
    ' ─────────────────────────────────────────────
    'Private Const DEFAULT_FOLDER As String = "S:\Old\New folder"
    Private Const DEFAULT_FOLDER As String = ""
    Private Const DEFAULT_PREFIX As String = ""
    Private Const PHP_API_URL As String = "https://vistawin.store/ClientSQLDB/upload_api.php"
    Private Const API_KEY As String = "YOUR_API_KEY"
    Private Const CHUNK_SIZE As Integer = 2 * 1024 * 1024   ' 2 MB per chunk
    ' ─────────────────────────────────────────────

    Private WithEvents worker As BackgroundWorker
    Private _filePath As String
    Private _fileName As String
    Private _cancelled As Boolean = False

    Private googleAppsScriptApiUrl As String = "YOUR_GOOGLE_APPS_SCRIPT_URL"
    Private apiKey As String = "YOUR_API_KEY"

    Public Sub New()
        InitializeComponent()
        UpdatePreview()
        worker = New BackgroundWorker()
        worker.WorkerReportsProgress = True
        worker.WorkerSupportsCancellation = True
    End Sub

    Private Sub FrmDBAutoUpload_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFolderPath.Text = VistawinFunctions.BackupPath
        txtFilePrefix.Text = VistawinFunctions.CmpDatabaseName
        txtApiKey.Text = VistawinFunctions.ServerApiChk

        googleAppsScriptApiUrl = txtApiKey.Text

        If txtApiKey.Text = String.Empty Then
            txtApiKey.Text = "YOUR_GOOGLE_APPS_SCRIPT_URL"
            googleAppsScriptApiUrl = txtApiKey.Text
        End If

        'txtApiKey.Hide()
    End Sub
    ' ── Preview label ─────────────────────────────
    Private Sub txtFolder_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFolderPath.TextChanged
        UpdatePreview()
    End Sub
    Private Sub txtPrefix_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilePrefix.TextChanged
        UpdatePreview()
    End Sub
    Private Sub UpdatePreview()
        Dim prefix As String = txtFilePrefix.Text.Trim()
        If String.IsNullOrEmpty(prefix) Then prefix = DEFAULT_PREFIX
        Dim folder As String = txtFolderPath.Text.Trim()
        If String.IsNullOrEmpty(folder) Then folder = DEFAULT_FOLDER
        lblPreview.Text = "Will search: " & Path.Combine(folder, prefix & DateTime.Now.ToString("ddMMyyyy"))
    End Sub
    Private Function GetTodayFileName() As String
        Dim prefix As String = txtFilePrefix.Text.Trim()
        If String.IsNullOrEmpty(prefix) Then prefix = DEFAULT_PREFIX
        Return prefix & DateTime.Now.ToString("ddMMyyyy")
    End Function
    Private Function GetFolderPath() As String
        Dim folder As String = txtFolderPath.Text.Trim()
        If String.IsNullOrEmpty(folder) Then folder = DEFAULT_FOLDER
        Return folder
    End Function

    ' ── Upload button ─────────────────────────────
    Private Sub btnVistawinBackup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVistawinBackup.Click

        If My.Computer.Network.IsAvailable Then

        Else
            MsgBox("Please,check your internet connection")
            Exit Sub
        End If

        ' Cancel if already running
        If worker.IsBusy Then
            _cancelled = True
            worker.CancelAsync()
            btnVistawinBackup.Text = "Backup / Upload"
            lblStatus.Text = "Cancelling..."
            btnVistawinBackup.Enabled = False
            Return
        End If

        _fileName = GetTodayFileName()
        _filePath = Path.Combine(GetFolderPath(), _fileName)
        _cancelled = False

        If Not File.Exists(_filePath) Then
            MessageBox.Show( _
                "File not found: " & _filePath, _
                "File Missing", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Warning)
            lblStatus.Text = "File not found: " & _fileName
            Return
        End If

        Dim fi As New FileInfo(_filePath)
        lblStatus.Text = "Found: " & _fileName & "  (" & FormatFileSize(fi.Length) & ")  Compressing..."

        ' Lock UI
        txtFolderPath.Enabled = False
        txtFilePrefix.Enabled = False
        progressBar.Value = 0
        progressBar.Visible = True
        lblPct.Text = "0%"
        lblPct.Visible = True
        btnVistawinBackup.Text = "Cancel"
        Me.Cursor = Cursors.WaitCursor

        worker.RunWorkerAsync()
    End Sub

    ' ── Background worker: compress then upload ───
    Private Sub worker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles worker.DoWork

        Dim bw As BackgroundWorker = CType(sender, BackgroundWorker)
        Dim zipPath As String = _filePath & ".zip"

        Try
            ' ── STEP 1: Compress to ZIP ───────────
            bw.ReportProgress(0, "Compressing " & _fileName & "...")

            CompressToZip(_filePath, _fileName, zipPath)

            If bw.CancellationPending Then
                e.Cancel = True
                CleanupZip(zipPath)
                Return
            End If

            bw.ReportProgress(5, "Compression done. Starting upload...")

            ' ── STEP 2: Chunked upload of the ZIP ─
            Dim zipName As String = _fileName & ".zip"
            Dim fi As New FileInfo(zipPath)
            Dim totalBytes As Long = fi.Length
            Dim totalChunks As Integer = CInt(Math.Ceiling(CDbl(totalBytes) / CHUNK_SIZE))
            Dim bytesSent As Long = 0

            Using fs As New FileStream(zipPath, FileMode.Open, FileAccess.Read, FileShare.Read)

                For chunkIndex As Integer = 0 To totalChunks - 1

                    If bw.CancellationPending Then
                        e.Cancel = True
                        PostCancel(zipName)
                        CleanupZip(zipPath)
                        Return
                    End If

                    Dim bytesRemaining As Long = totalBytes - bytesSent
                    Dim thisChunkSize As Integer = CInt(Math.Min(CHUNK_SIZE, bytesRemaining))
                    Dim buffer(thisChunkSize - 1) As Byte
                    Dim bytesRead As Integer = fs.Read(buffer, 0, thisChunkSize)

                    If bytesRead < thisChunkSize Then
                        ReDim Preserve buffer(bytesRead - 1)
                    End If

                    Dim chunkResponse As String = PostChunk(zipName, buffer, chunkIndex, totalChunks)

                    If Not chunkResponse.Contains("""status"":""success""") AndAlso _
                       Not chunkResponse.Contains("""status"": ""success""") Then
                        Throw New Exception("Chunk " & chunkIndex & " failed: " & chunkResponse)
                    End If

                    bytesSent += bytesRead

                    ' Progress: 5% (compress done) + up to 93% for upload = max 98%
                    Dim uploadPct As Integer = CInt((CDbl(bytesSent) / totalBytes) * 93)
                    bw.ReportProgress(5 + uploadPct, _
                        "Uploading " & FormatFileSize(bytesSent) & " / " & FormatFileSize(totalBytes) & _
                        "  (" & zipName & ")")

                Next chunkIndex

            End Using

            ' ── STEP 3: Finalize ──────────────────
            bw.ReportProgress(98, "Finalizing on server...")
            Dim finalResponse As String = PostFinalize(zipName, totalBytes)

            ' ── STEP 4: Delete local temp ZIP ─────
            CleanupZip(zipPath)

            e.Result = finalResponse

        Catch ex As Exception
            CleanupZip(zipPath)
            e.Result = "ERROR:" & ex.Message
        End Try

    End Sub

    ' ── Compress source file into a ZIP ───────────
    ' Uses System.IO.Packaging (WindowsBase.dll) – available in .NET 3.5
    Private Sub CompressToZip(ByVal sourcePath As String, ByVal entryName As String, ByVal zipPath As String)

        ' Delete any leftover zip from a previous run
        If File.Exists(zipPath) Then File.Delete(zipPath)

        ' ZipPackage wraps the OPC/ZIP container
        Using pkg As Package = Package.Open(zipPath, FileMode.Create)
            ' URI inside the zip – must start with /
            Dim partUri As Uri = PackUriHelper.CreatePartUri(New Uri("/" & entryName, UriKind.Relative))
            Dim part As PackagePart = pkg.CreatePart(partUri, "application/octet-stream", CompressionOption.Maximum)

            Using src As New FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read)
                Using dest As Stream = part.GetStream()
                    Dim buf(65535) As Byte
                    Dim read As Integer = src.Read(buf, 0, buf.Length)
                    Do While read > 0
                        dest.Write(buf, 0, read)
                        read = src.Read(buf, 0, buf.Length)
                    Loop
                End Using
            End Using
        End Using

    End Sub

    ' ── Delete temp zip silently ──────────────────
    Private Sub CleanupZip(ByVal zipPath As String)
        Try
            If File.Exists(zipPath) Then File.Delete(zipPath)
        Catch
            ' Ignore cleanup errors
        End Try
    End Sub

    ' ── Report progress back to UI ────────────────
    Private Sub worker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles worker.ProgressChanged
        progressBar.Value = Math.Min(e.ProgressPercentage, 100)
        lblPct.Text = e.ProgressPercentage & "%"
        If e.UserState IsNot Nothing Then
            lblStatus.Text = CStr(e.UserState)
        End If
    End Sub

    ' ── Upload complete ───────────────────────────
    Private Sub worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted

        txtFolderPath.Enabled = True
        txtFilePrefix.Enabled = True
        btnVistawinBackup.Text = "Send to Server"
        btnVistawinBackup.Enabled = True
        Me.Cursor = Cursors.Default

        If e.Cancelled OrElse _cancelled Then
            progressBar.Visible = False
            lblPct.Visible = False
            lblStatus.Text = "Upload cancelled."
            Return
        End If

        progressBar.Value = 100
        lblPct.Text = "100%"
        System.Threading.Thread.Sleep(400)
        progressBar.Visible = False
        lblPct.Visible = False

        Dim result As String = CStr(e.Result)

        If result.StartsWith("ERROR:") Then
            MessageBox.Show( _
                "Upload error:" & Environment.NewLine & result.Substring(6), _
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblStatus.Text = "Upload failed."
        Else
            HandleApiResponse(result, _fileName & ".zip")
        End If

    End Sub

    ' ── Send one chunk to PHP ─────────────────────
    Private Function PostChunk( _
            ByVal fileName As String, _
            ByVal data() As Byte, _
            ByVal chunkIndex As Integer, _
            ByVal totalChunks As Integer) As String

        Dim boundary As String = "----Chunk" & DateTime.Now.Ticks.ToString("x")
        Dim nl As String = Environment.NewLine
        Dim enc As New UTF8Encoding(False)
        Dim sb As New StringBuilder()

        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""api_key""" & nl & nl)
        sb.Append(API_KEY & nl)
        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""action""" & nl & nl)
        sb.Append("upload_chunk" & nl)
        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""file_name""" & nl & nl)
        sb.Append(fileName & nl)
        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""chunk_index""" & nl & nl)
        sb.Append(chunkIndex.ToString() & nl)
        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""total_chunks""" & nl & nl)
        sb.Append(totalChunks.ToString() & nl)
        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""chunk_data""; filename=""chunk""" & nl)
        sb.Append("Content-Type: application/octet-stream" & nl & nl)

        Dim footer As String = nl & "--" & boundary & "--" & nl
        Dim hBytes() As Byte = enc.GetBytes(sb.ToString())
        Dim fBytes() As Byte = enc.GetBytes(footer)

        Dim total As Integer = hBytes.Length + data.Length + fBytes.Length
        Dim body(total - 1) As Byte
        Buffer.BlockCopy(hBytes, 0, body, 0, hBytes.Length)
        Buffer.BlockCopy(data, 0, body, hBytes.Length, data.Length)
        Buffer.BlockCopy(fBytes, 0, body, hBytes.Length + data.Length, fBytes.Length)

        Return SendPost(boundary, body)
    End Function

    ' ── Finalize: rename .tmp to final file ───────
    Private Function PostFinalize(ByVal fileName As String, ByVal totalSize As Long) As String
        Dim boundary As String = "----Final" & DateTime.Now.Ticks.ToString("x")
        Dim nl As String = Environment.NewLine
        Dim enc As New UTF8Encoding(False)
        Dim sb As New StringBuilder()

        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""api_key""" & nl & nl)
        sb.Append(API_KEY & nl)
        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""action""" & nl & nl)
        sb.Append("finalize" & nl)
        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""file_name""" & nl & nl)
        sb.Append(fileName & nl)
        sb.Append("--" & boundary & nl)
        sb.Append("Content-Disposition: form-data; name=""total_size""" & nl & nl)
        sb.Append(totalSize.ToString() & nl)
        sb.Append("--" & boundary & "--" & nl)

        Return SendPost(boundary, enc.GetBytes(sb.ToString()))
    End Function

    ' ── Cancel: clean up server temp file ─────────
    Private Sub PostCancel(ByVal fileName As String)
        Try
            Dim boundary As String = "----Cancel" & DateTime.Now.Ticks.ToString("x")
            Dim nl As String = Environment.NewLine
            Dim enc As New UTF8Encoding(False)
            Dim sb As New StringBuilder()

            sb.Append("--" & boundary & nl)
            sb.Append("Content-Disposition: form-data; name=""api_key""" & nl & nl)
            sb.Append(API_KEY & nl)
            sb.Append("--" & boundary & nl)
            sb.Append("Content-Disposition: form-data; name=""action""" & nl & nl)
            sb.Append("cancel" & nl)
            sb.Append("--" & boundary & nl)
            sb.Append("Content-Disposition: form-data; name=""file_name""" & nl & nl)
            sb.Append(fileName & nl)
            sb.Append("--" & boundary & "--" & nl)

            SendPost(boundary, enc.GetBytes(sb.ToString()))
        Catch
            ' Best-effort
        End Try
    End Sub

    ' ── Core HTTP POST ────────────────────────────
    Private Function SendPost(ByVal boundary As String, ByVal body() As Byte) As String
        System.Net.ServicePointManager.SecurityProtocol = CType(3072, System.Net.SecurityProtocolType)

        Dim request As HttpWebRequest = CType(WebRequest.Create(PHP_API_URL), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "multipart/form-data; boundary=" & boundary
        request.ContentLength = body.Length
        request.Timeout = 120000

        Using reqStream As Stream = request.GetRequestStream()
            reqStream.Write(body, 0, body.Length)
        End Using

        Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                Return reader.ReadToEnd()
            End Using
        End Using
    End Function

    ' ── JSON response handler ─────────────────────
    Private Sub HandleApiResponse(ByVal jsonResponse As String, ByVal fileName As String)
        Dim json As String = jsonResponse.Trim()

        If json.Contains("""status"":""success""") OrElse _
           json.Contains("""status"": ""success""") Then
            MessageBox.Show( _
                "Upload successful!" & Environment.NewLine & fileName & " uploaded to server.", _
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            lblStatus.Text = "Upload successful: " & fileName

        ElseIf json.Contains("""status"":""error""") OrElse _
               json.Contains("""status"": ""error""") Then
            Dim msg As String = ExtractJsonValue(json, "message")
            If String.IsNullOrEmpty(msg) Then msg = "Unknown error from server."
            MessageBox.Show( _
                "Upload failed:" & Environment.NewLine & msg, _
                "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblStatus.Text = "Upload failed: " & msg
        Else
            MessageBox.Show( _
                "Unexpected server response:" & Environment.NewLine & json, _
                "Unknown Response", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            lblStatus.Text = "Unexpected response."
        End If
    End Sub

    ' ── JSON value extractor ──────────────────────
    Private Function ExtractJsonValue(ByVal json As String, ByVal key As String) As String
        Dim searchKey As String = """" & key & """:"
        Dim idx As Integer = json.IndexOf(searchKey)
        If idx < 0 Then Return String.Empty
        idx = idx + searchKey.Length
        Do While idx < json.Length AndAlso (json(idx) = " "c OrElse json(idx) = Chr(9))
            idx = idx + 1
        Loop
        If idx >= json.Length Then Return String.Empty
        If json(idx) = """"c Then
            idx = idx + 1
            Dim endIdx As Integer = json.IndexOf(""""c, idx)
            If endIdx < 0 Then Return String.Empty
            Return json.Substring(idx, endIdx - idx)
        Else
            Dim endIdx As Integer = json.IndexOfAny(New Char() {","c, "}"c}, idx)
            If endIdx < 0 Then Return json.Substring(idx)
            Return json.Substring(idx, endIdx - idx).Trim()
        End If
    End Function

    ' ── Human-readable file size ──────────────────
    Private Function FormatFileSize(ByVal bytes As Long) As String
        If bytes >= 1073741824L Then
            Return String.Format("{0:F2} GB", CDbl(bytes) / 1073741824.0)
        ElseIf bytes >= 1048576L Then
            Return String.Format("{0:F1} MB", CDbl(bytes) / 1048576.0)
        ElseIf bytes >= 1024L Then
            Return String.Format("{0:F1} KB", CDbl(bytes) / 1024.0)
        Else
            Return bytes.ToString() & " B"
        End If
    End Function

    ' ── Timer tick (spare) ────────────────────────
    Private Sub tmrProgress_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrProgress.Tick
    End Sub

    Private Sub BtnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBackup.Click

        If txtFolderPath.Text = String.Empty Then
            MsgBox("Enter Backup Path")
            txtFolderPath.Focus()
            Exit Sub
        End If

        Me.DatabaseBackup()
    End Sub
    Private Sub DatabaseBackup()

        Try
            Dim dbName As String = VistawinFunctions.ServerConnection.Database
            Dim fileName As String = dbName & Format(Date.Today, "ddMMyyyy")
            Dim pathName As String = System.IO.Path.Combine(txtFolderPath.Text, fileName)

            Using cmd As New SqlCommand("dbo.dbbackup", VistawinFunctions.ServerConnection)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 900

                cmd.Parameters.AddWithValue("@dbnm", dbName)
                cmd.Parameters.AddWithValue("@pathname", pathName)
                cmd.Parameters.AddWithValue("@dbsname", fileName & "-Full Backup")
                cmd.ExecuteNonQuery()
            End Using
            MessageBox.Show("Database Backup Completed Successfully.", "Backup")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Backup Error")
        End Try

    End Sub

    Private Sub BtnGDriveBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGDriveBackup.Click


        ' ---------- validate inputs ----------
        Dim folderPath As String = txtFolderPath.Text.Trim()
        Dim filePrefix As String = txtFilePrefix.Text.Trim()

        If folderPath = "" Then
            MessageBox.Show("Please enter or browse to the upload folder path.", _
                "Missing Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFolderPath.Focus()
            Return
        End If

        If filePrefix = "" Then
            MessageBox.Show("Please enter the file name prefix (e.g. mani).", _
                "Missing Prefix", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFilePrefix.Focus()
            Return
        End If

        If Not Directory.Exists(folderPath) Then
            MessageBox.Show("Folder not found:" & vbCrLf & folderPath & vbCrLf & vbCrLf & _
                "Please check the folder path and try again.", _
                "Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' Step 1: build search pattern  e.g. mani11072026*
            '         matches any extension OR no extension at all
            Dim datePart As String = DateTime.Now.ToString("ddMMyyyy")
            Dim searchPattern As String = filePrefix & datePart & "*"

            lblStatus.Text = "Searching: " & searchPattern
            lblStatus.ForeColor = System.Drawing.Color.Blue
            Me.Refresh()

            ' Step 2: find all matching files
            Dim matchedFiles As String() = Directory.GetFiles(folderPath, searchPattern)

            ' Step 3: nothing found?
            If matchedFiles.Length = 0 Then
                lblStatus.Text = "File not found: " & searchPattern
                lblStatus.ForeColor = System.Drawing.Color.Red
                MessageBox.Show( _
                    "No file found matching:" & vbCrLf & _
                    "  " & searchPattern & vbCrLf & vbCrLf & _
                    "Folder: " & folderPath & vbCrLf & vbCrLf & _
                    "Any file format is accepted." & vbCrLf & _
                    "Examples: " & filePrefix & datePart & "  or  " & filePrefix & datePart & ".bak", _
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Step 4: use first matched file (any extension accepted)
            Dim fullFilePath As String = matchedFiles(0)
            Dim fileName As String = Path.GetFileName(fullFilePath)

            lblStatus.Text = "Reading: " & fileName
            lblStatus.ForeColor = System.Drawing.Color.Blue
            Me.Refresh()

            ' Step 5: read the original file bytes
            Dim fileBytes As Byte() = File.ReadAllBytes(fullFilePath)

            ' Step 6: compress OR send as-is depending on checkbox
            Dim uploadFileName As String
            Dim uploadBytes As Byte()
            Dim uploadMime As String

            If chkCompress.Checked Then
                ' --- compress into a .zip in memory using ZipPackage (works in .NET 4.0) ---
                Dim zipFileName As String = Path.GetFileNameWithoutExtension(fileName) & ".zip"

                lblStatus.Text = "Compressing: " & fileName & " → " & zipFileName
                Me.Refresh()

                Using zipStream As New MemoryStream()
                    Using pkg As Package = ZipPackage.Open(zipStream, FileMode.Create, FileAccess.ReadWrite)
                        Dim partUri As Uri = PackUriHelper.CreatePartUri(New Uri("/" & fileName.Replace(" ", "_"), UriKind.Relative))
                        Dim part As PackagePart = pkg.CreatePart(partUri, "application/octet-stream", CompressionOption.Maximum)
                        Using partStream As Stream = part.GetStream()
                            partStream.Write(fileBytes, 0, fileBytes.Length)
                        End Using
                    End Using
                    uploadBytes = zipStream.ToArray()
                End Using

                uploadFileName = zipFileName
                uploadMime = "application/zip"
            Else
                ' --- send original file directly ---
                uploadBytes = fileBytes
                uploadFileName = fileName
                uploadMime = "application/octet-stream"
            End If

            ' Step 7: convert to Base64
            Dim fileBase64 As String = Convert.ToBase64String(uploadBytes)

            ' Step 8: build JSON
            lblStatus.Text = "Uploading to Google Drive..."
            Me.Refresh()

            Dim jsonPayload As String = "{" & _
                """api_key"":""" & EscapeJson(apiKey) & """," & _
                """file_name"":""" & EscapeJson(uploadFileName) & """," & _
                """file_base64"":""" & fileBase64 & """," & _
                """mime_type"":""" & uploadMime & """" & _
                "}"

            ' Step 8: POST to Google Apps Script
            Dim jsonResponse As String = SendPostRequest(googleAppsScriptApiUrl, jsonPayload)

            ' Step 9: handle response
            If jsonResponse.Contains("""status"":""success""") Then
                lblStatus.Text = "Uploaded: " & uploadFileName
                lblStatus.ForeColor = System.Drawing.Color.Green

                If chkCompress.Checked Then
                    MessageBox.Show( _
                        "File compressed and uploaded successfully to Google Drive!" & vbCrLf & vbCrLf & _
                        "Original : " & fileName & vbCrLf & _
                        "Uploaded : " & uploadFileName, _
                        "Upload Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show( _
                        "File uploaded successfully to Google Drive!" & vbCrLf & vbCrLf & _
                        "File: " & uploadFileName, _
                        "Upload Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            ElseIf jsonResponse.Contains("""status"":""error""") Then
                Dim errMsg As String = ExtractJsonValue(jsonResponse, "message")
                lblStatus.Text = "Upload failed: " & errMsg
                lblStatus.ForeColor = System.Drawing.Color.Red

                If errMsg.ToLower().Contains("invalid api key") Then
                    MessageBox.Show("Invalid API Key." & vbCrLf & vbCrLf & _
                        "Ensure apiKey in Form1.vb matches API_KEY in Google Apps Script.", _
                        "Auth Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf errMsg.ToLower().Contains("folder") Or errMsg.ToLower().Contains("permission") Then
                    MessageBox.Show("Google Drive folder error." & vbCrLf & vbCrLf & _
                        "Check the folder ID in Google Apps Script.", _
                        "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("API Error: " & errMsg, _
                        "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                lblStatus.Text = "Unexpected response"
                lblStatus.ForeColor = System.Drawing.Color.Red
                MessageBox.Show("Unexpected API response:" & vbCrLf & jsonResponse, _
                    "Unexpected Response", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As WebException
            lblStatus.Text = "Network error"
            lblStatus.ForeColor = System.Drawing.Color.Red
            Dim webDetail As String = ex.Message
            If ex.Response IsNot Nothing Then
                Using rdr As New StreamReader(ex.Response.GetResponseStream())
                    webDetail = rdr.ReadToEnd()
                End Using
            End If
            MessageBox.Show("Network error:" & vbCrLf & vbCrLf & webDetail, _
                "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            lblStatus.Text = "Error: " & ex.Message
            lblStatus.ForeColor = System.Drawing.Color.Red
            MessageBox.Show("Error:" & vbCrLf & ex.Message, _
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        '' ---------- validate inputs ----------
        'Dim folderPath As String = txtFolderPath.Text.Trim()
        'Dim filePrefix As String = txtFilePrefix.Text.Trim()

        'If folderPath = "" Then
        '    MessageBox.Show("Please enter or browse to the upload folder path.", _
        '        "Missing Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    txtFolderPath.Focus()
        '    Return
        'End If

        'If filePrefix = "" Then
        '    MessageBox.Show("Please enter the file name prefix (e.g. mani).", _
        '        "Missing Prefix", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    txtFilePrefix.Focus()
        '    Return
        'End If

        'If Not Directory.Exists(folderPath) Then
        '    MessageBox.Show("Folder not found:" & vbCrLf & folderPath & vbCrLf & vbCrLf & _
        '        "Please check the folder path and try again.", _
        '        "Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return
        'End If

        'Try
        '    ' Step 1: build search pattern  e.g. mani11072026*
        '    '         matches any extension OR no extension at all
        '    Dim datePart As String = DateTime.Now.ToString("ddMMyyyy")
        '    Dim searchPattern As String = filePrefix & datePart & "*"

        '    lblStatus.Text = "Searching: " & searchPattern
        '    lblStatus.ForeColor = System.Drawing.Color.Blue
        '    Me.Refresh()

        '    ' Step 2: find all matching files
        '    Dim matchedFiles As String() = Directory.GetFiles(folderPath, searchPattern)

        '    ' Step 3: nothing found?
        '    If matchedFiles.Length = 0 Then
        '        lblStatus.Text = "File not found: " & searchPattern
        '        lblStatus.ForeColor = System.Drawing.Color.Red
        '        MessageBox.Show( _
        '            "No file found matching:" & vbCrLf & _
        '            "  " & searchPattern & vbCrLf & vbCrLf & _
        '            "Folder: " & folderPath & vbCrLf & vbCrLf & _
        '            "Any file format is accepted." & vbCrLf & _
        '            "Examples: " & filePrefix & datePart & "  or  " & filePrefix & datePart & ".bak", _
        '            "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Return
        '    End If

        '    ' Step 4: use first matched file (any extension accepted)
        '    Dim fullFilePath As String = matchedFiles(0)
        '    Dim fileName As String = Path.GetFileName(fullFilePath)

        '    lblStatus.Text = "Reading: " & fileName
        '    lblStatus.ForeColor = System.Drawing.Color.Blue
        '    Me.Refresh()

        '    ' Step 5: read bytes
        '    Dim fileBytes As Byte() = File.ReadAllBytes(fullFilePath)

        '    ' Step 6: convert to Base64
        '    Dim fileBase64 As String = Convert.ToBase64String(fileBytes)

        '    ' Step 7: build JSON
        '    lblStatus.Text = "Uploading to Google Drive..."
        '    Me.Refresh()

        '    Dim jsonPayload As String = "{" & _
        '        """api_key"":""" & EscapeJson(apiKey) & """," & _
        '        """file_name"":""" & EscapeJson(fileName) & """," & _
        '        """file_base64"":""" & fileBase64 & """," & _
        '        """mime_type"":""" & "application/octet-stream" & """" & _
        '        "}"

        '    ' Step 8: POST to Google Apps Script
        '    Dim jsonResponse As String = SendPostRequest(googleAppsScriptApiUrl, jsonPayload)

        '    ' Step 9: handle response
        '    If jsonResponse.Contains("""status"":""success""") Then
        '        lblStatus.Text = "Uploaded: " & fileName
        '        lblStatus.ForeColor = System.Drawing.Color.Green
        '        MessageBox.Show( _
        '            "File uploaded successfully to Google Drive!" & vbCrLf & vbCrLf & _
        '            "File: " & fileName, _
        '            "Upload Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '    ElseIf jsonResponse.Contains("""status"":""error""") Then
        '        Dim errMsg As String = ExtractJsonValueGDrive(jsonResponse, "message")
        '        lblStatus.Text = "Upload failed: " & errMsg
        '        lblStatus.ForeColor = System.Drawing.Color.Red

        '        If errMsg.ToLower().Contains("invalid api key") Then
        '            MessageBox.Show("Invalid API Key." & vbCrLf & vbCrLf & _
        '                "Ensure apiKey in Form1.vb matches API_KEY in Google Apps Script.", _
        '                "Auth Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ElseIf errMsg.ToLower().Contains("folder") Or errMsg.ToLower().Contains("permission") Then
        '            MessageBox.Show("Google Drive folder error." & vbCrLf & vbCrLf & _
        '                "Check the folder ID in Google Apps Script.", _
        '                "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Else
        '            MessageBox.Show("API Error: " & errMsg, _
        '                "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        End If

        '    Else
        '        lblStatus.Text = "Unexpected response"
        '        lblStatus.ForeColor = System.Drawing.Color.Red
        '        MessageBox.Show("Unexpected API response:" & vbCrLf & jsonResponse, _
        '            "Unexpected Response", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End If

        'Catch ex As WebException
        '    lblStatus.Text = "Network error"
        '    lblStatus.ForeColor = System.Drawing.Color.Red
        '    Dim webDetail As String = ex.Message
        '    If ex.Response IsNot Nothing Then
        '        Using rdr As New StreamReader(ex.Response.GetResponseStream())
        '            webDetail = rdr.ReadToEnd()
        '        End Using
        '    End If
        '    MessageBox.Show("Network error:" & vbCrLf & vbCrLf & webDetail, _
        '        "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        'Catch ex As Exception
        '    lblStatus.Text = "Error: " & ex.Message
        '    lblStatus.ForeColor = System.Drawing.Color.Red
        '    MessageBox.Show("Error:" & vbCrLf & ex.Message, _
        '        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

    End Sub

    Private Function SendPostRequest(ByVal url As String, ByVal jsonBody As String) As String
        Dim postData As Byte() = Encoding.UTF8.GetBytes(jsonBody)

        Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        req.Method = "POST"
        req.ContentType = "application/json"
        req.ContentLength = postData.Length
        req.Timeout = 120000

        Using s As Stream = req.GetRequestStream()
            s.Write(postData, 0, postData.Length)
        End Using

        Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
            Using rdr As New StreamReader(resp.GetResponseStream(), Encoding.UTF8)
                Return rdr.ReadToEnd()
            End Using
        End Using
    End Function

    ' ============================================================
    ' Extract a string value from a flat JSON response
    ' ============================================================
    Private Function ExtractJsonValueGDrive(ByVal json As String, ByVal key As String) As String
        Try
            Dim token As String = """" & key & """:"""
            Dim si As Integer = json.IndexOf(token)
            If si = -1 Then Return ""
            si += token.Length
            Dim ei As Integer = json.IndexOf("""", si)
            If ei = -1 Then Return ""
            Return json.Substring(si, ei - si)
        Catch
            Return ""
        End Try
    End Function

    ' ============================================================
    ' Escape special characters for a JSON string value
    ' ============================================================
    Private Function EscapeJson(ByVal value As String) As String
        If String.IsNullOrEmpty(value) Then Return ""
        value = value.Replace("\", "\\")
        value = value.Replace("""", "\""")
        value = value.Replace("/", "\/")
        value = value.Replace(Chr(8), "\b")
        value = value.Replace(Chr(12), "\f")
        value = value.Replace(Chr(10), "\n")
        value = value.Replace(Chr(13), "\r")
        value = value.Replace(Chr(9), "\t")
        Return value
    End Function

End Class
