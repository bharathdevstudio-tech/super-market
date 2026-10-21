Imports System.IO
Imports System.Data.SqlClient
Imports System.Diagnostics

Imports System.Net
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Public Class FrmDownload

    Private Const API_URL As String = "https://vistawin.store/update-application/api/download.php"
    Private Const API_TOKEN As String = "Hello123"
    Private Const MAX_CUSTOMER_NAME_LENGTH As Integer = 50

    Private tempFilePath As String = ""
    Private downloadedFileSize As Long = 0

    Private AppDownloadOnly As Boolean = False
    Private Sub FrmDownload_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ServicePointManager.SecurityProtocol = CType(48 Or 192 Or 3072, SecurityProtocolType)
        ServicePointManager.ServerCertificateValidationCallback = Function(sender2, cert, chain, errors) True
        ServicePointManager.DefaultConnectionLimit = 10
        ServicePointManager.Expect100Continue = False

        lblStatus.Text = "Ready"
        ProgressBar1.Visible = False
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0

        txtDatabaseName.Text = VistawinFunctions.CmpDatabaseName
        txtDatabaseName.Focus()
    End Sub

    Private Sub LblAnyDesk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblAnyDesk.Click
        Dim url As String = "https://anydesk.com/en/downloads/windows"
        Process.Start(url)
    End Sub
    Private Sub LblUltraViewer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblUltraViewer.Click

        Dim url As String = "https://www.ultraviewer.net/en/download.html"
        'Dim url As String = "https://vistawin.online/software/UltraViewer.exe"
        Process.Start(url)
    End Sub

    Private Sub LblWinRar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblWinRar.Click

        Dim url As String = "https://www.win-rar.com/download.html?&L=0"
        Process.Start(url)
    End Sub

    Private Sub LblDownloadFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblDownloadFolder.Click
        'Process.Start("C:\Users\USER\Downloads")

        Dim DownloadPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        DownloadPath = Replace(DownloadPath, "\Documents", "\Downloads")
        Process.Start(DownloadPath)
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub LblAdobe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblAdobe.Click
        Dim url As String = "https://get.adobe.com/uk/reader/"
        Process.Start(url)
    End Sub

    Private Sub BtnMorefiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMorefiles.Click
        Dim frm As New FrmDownloadVistawin
        frm.ShowDialog()
    End Sub

    Private Sub BtnFolderCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFolderCreate.Click

        Directory.CreateDirectory("" & VistawinFunctions.ScanningPath & "\Cus_Bill")
        Directory.CreateDirectory("" & VistawinFunctions.ScanningPath & "\Cus_Image")
        Directory.CreateDirectory("" & VistawinFunctions.ScanningPath & "\Exp_Image")
        MessageBox.Show("Folder creation completed.", "Bharath SuperMarket", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub LblConPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblConPanel.Click
        Try
            Process.Start("control.exe")
        Catch
            Try
                Process.Start("explorer.exe", "shell:::{21EC2020-3AEA-1069-A2DD-08002B30309D}")
            Catch ex As Exception
                MessageBox.Show("Unable to open Control Panel." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Try

    End Sub

    Private Sub LblOpenPrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblOpenPrinter.Click
        Try
            Process.Start("control.exe", "/name Microsoft.DevicesAndPrinters")
        Catch
            Try
                Process.Start("explorer.exe", "shell:::{A8A91A66-3A7D-4424-8D24-04E180695C7A}")
            Catch ex As Exception
                MessageBox.Show("Unable to open Devices and Printers." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Try

    End Sub
    Private Sub btnDownloadInstall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadInstall.Click

        AppDownloadOnly = False

        Dim customerName As String = txtDatabaseName.Text.Trim()

        ' Local validation
        Dim validationError As String = ValidateCustomerName(customerName)
        If validationError IsNot Nothing Then
            MessageBox.Show(validationError, "Invalid Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDatabaseName.Focus()
            Return
        End If

        If BackgroundWorker1.IsBusy Then
            MessageBox.Show("Download is already in progress. Please wait.", "Download in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Disable UI
        txtDatabaseName.Enabled = False
        btnDownloadInstall.Enabled = False
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        lblStatus.Text = "Connecting to server..."

        BackgroundWorker1.RunWorkerAsync(customerName)
    End Sub

    Private Function ValidateCustomerName(ByVal customerName As String) As String
        If String.IsNullOrEmpty(customerName) Then
            Return "Please enter a customer name."
        End If
        If customerName.Length > MAX_CUSTOMER_NAME_LENGTH Then
            Return "Customer name is too long (maximum " & MAX_CUSTOMER_NAME_LENGTH & " characters)."
        End If
        Dim validPattern As New Regex("^[a-zA-Z0-9 _-]+$")
        If Not validPattern.IsMatch(customerName) Then
            Return "Customer name contains invalid characters." & vbCrLf & "Only letters, numbers, spaces, hyphens and underscores are allowed."
        End If
        Dim dangerous() As String = {"..", "/", "\", ":", "*", "?", """", "<", ">", "|"}
        For Each p As String In dangerous
            If customerName.Contains(p) Then
                Return "Invalid characters detected in customer name."
            End If
        Next
        Return Nothing
    End Function

    ' ================================================================
    '  BACKGROUND WORKER - DO WORK
    ' ================================================================
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim customerName As String = CStr(e.Argument)
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        Try

            ' Unique temp file path
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            'tempFilePath = Path.Combine(Path.GetTempPath(), "VistawinSetup_" & timestamp & ".exe")
            'tempFilePath = Path.Combine("D:\All Images", "VistawinSetup_" & timestamp & ".exe")
            tempFilePath = Path.Combine(VistawinFunctions.ScanningPath, "VistawinSetup_" & timestamp & ".exe")

            worker.ReportProgress(5, "Connecting to server...")

            ' ── Build POST request ──────────────────────────────────
            Dim request As HttpWebRequest = CType(WebRequest.Create(API_URL), HttpWebRequest)

            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.UserAgent = "VistawinDownloader/1.0"
            request.Timeout = 30000   ' 30 sec connect
            request.ReadWriteTimeout = 300000  ' 5 min read
            request.AllowAutoRedirect = True  ' Follow redirects
            request.KeepAlive = False
            request.ServicePoint.Expect100Continue = False

            ' POST body
            'Dim postData As String = "customer_name=" & Uri.EscapeDataString(customerName) & "&api_token=" & Uri.EscapeDataString(API_TOKEN)

            Dim postData As String

            If AppDownloadOnly Then

                postData = _
                    "customer_name=" & _
                    Uri.EscapeDataString(customerName) & _
                    "&api_token=" & _
                    Uri.EscapeDataString(API_TOKEN) & _
                    "&download_type=ANYFILE"

            Else

                postData = _
                    "customer_name=" & _
                    Uri.EscapeDataString(customerName) & _
                    "&api_token=" & _
                    Uri.EscapeDataString(API_TOKEN)

            End If

            'Dim postData As String
            'If AppDownloadOnly Then
            '    postData = "customer_name=" & Uri.EscapeDataString("OFFLINE VB APP") & "&api_token=" & Uri.EscapeDataString(API_TOKEN) & "&download_type=ANYFILE"
            'Else
            '    postData = "customer_name=" & Uri.EscapeDataString(customerName) & "&api_token=" & Uri.EscapeDataString(API_TOKEN)
            'End If

            Dim postBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(postData)

            request.ContentLength = postBytes.Length

            ' Write POST body
            Using reqStream As Stream = request.GetRequestStream()
                reqStream.Write(postBytes, 0, postBytes.Length)
            End Using

            worker.ReportProgress(10, "Sending request...")

            ' ── Get response ────────────────────────────────────────
            Dim response As HttpWebResponse

            Try
                response = CType(request.GetResponse(), HttpWebResponse)
            Catch webEx As WebException
                ' Check if we got an HTTP error response with a body
                If webEx.Response IsNot Nothing Then
                    Dim errResp As HttpWebResponse = CType(webEx.Response, HttpWebResponse)
                    Dim errBody As String = ""
                    Try
                        Using errStream As Stream = errResp.GetResponseStream()
                            Using rdr As New StreamReader(errStream)
                                errBody = rdr.ReadToEnd()
                            End Using
                        End Using
                    Catch
                    End Try

                    ' Try to extract JSON message
                    Dim msg As String = ExtractJsonMessage(errBody)
                    If String.IsNullOrEmpty(msg) Then
                        msg = "Server error " & CInt(errResp.StatusCode).ToString() & ": " & errResp.StatusDescription
                        If Not String.IsNullOrEmpty(errBody) Then
                            msg &= vbCrLf & errBody.Substring(0, Math.Min(200, errBody.Length))
                        End If
                    End If
                    e.Result = New DownloadResult With {.Success = False, .ErrorMessage = msg}
                    Return
                End If

                ' Network-level error
                Dim netMsg As String
                Select Case webEx.Status
                    Case WebExceptionStatus.Timeout
                        netMsg = "Connection timeout. Please check your internet connection."
                    Case WebExceptionStatus.NameResolutionFailure
                        netMsg = "Cannot find server. Please check your internet connection."
                    Case WebExceptionStatus.ConnectFailure
                        netMsg = "Cannot connect to server. Server may be offline."
                    Case WebExceptionStatus.TrustFailure
                        netMsg = "SSL certificate error. Server certificate is not trusted."
                    Case Else
                        netMsg = "Network error: " & webEx.Message
                End Select
                e.Result = New DownloadResult With {.Success = False, .ErrorMessage = netMsg}
                Return
            End Try

            ' ── Process response ────────────────────────────────────
            Using response
                Dim contentType As String = If(response.ContentType, "")
                Dim installerHdr As String = response.Headers("X-Installer-File")

                If AppDownloadOnly Then

                    Dim FolderPath As String = "D:\VISTAWIN"

                    If Not Directory.Exists(FolderPath) Then
                        Directory.CreateDirectory(FolderPath)
                    End If

                    Dim FileName As String = ""

                    'Server header-ல் வரும் original filename
                    If Not String.IsNullOrEmpty(installerHdr) Then
                        FileName = Path.GetFileName(installerHdr)
                    End If

                    If String.IsNullOrEmpty(FileName) Then
                        Throw New Exception("Server file name not received.")
                    End If

                    tempFilePath = Path.Combine(FolderPath, FileName)

                End If

                worker.ReportProgress(15, "Response received. Checking...")

                ' If content-type is JSON or HTML → it is an error response
                If String.IsNullOrEmpty(installerHdr) AndAlso
                   (contentType.Contains("application/json") OrElse
                    contentType.Contains("text/html") OrElse
                    contentType.Contains("text/plain")) Then

                    Using rdrStream As Stream = response.GetResponseStream()
                        Using rdr As New StreamReader(rdrStream)
                            Dim body As String = rdr.ReadToEnd()
                            Dim msg As String = ExtractJsonMessage(body)
                            If String.IsNullOrEmpty(msg) Then
                                msg = "Server returned: " & body.Substring(0, Math.Min(200, body.Length))
                            End If
                            e.Result = New DownloadResult With {.Success = False, .ErrorMessage = msg}
                            Return
                        End Using
                    End Using
                End If

                ' ── Stream file download ─────────────────────────────
                Dim totalBytes As Long = response.ContentLength
                Dim totalRead As Long = 0

                worker.ReportProgress(20, "Downloading setup...")

                Using respStream As Stream = response.GetResponseStream()
                    Using fileStream As New FileStream(tempFilePath, FileMode.CreateNew, FileAccess.Write)

                        ''Using fileStream As New FileStream(tempFilePath, FileMode.Create, FileAccess.Write)
                        Dim buffer(8191) As Byte
                        Dim bytesRead As Integer

                        Do
                            bytesRead = respStream.Read(buffer, 0, buffer.Length)
                            If bytesRead = 0 Then Exit Do
                            fileStream.Write(buffer, 0, bytesRead)
                            totalRead += bytesRead

                            If totalBytes > 0 Then
                                Dim pct As Integer = CInt((totalRead * 70) / totalBytes) + 20
                                worker.ReportProgress(pct, "Downloading: " & FormatBytes(totalRead) & " / " & FormatBytes(totalBytes))
                            Else
                                worker.ReportProgress(50, "Downloading: " & FormatBytes(totalRead))
                            End If
                        Loop
                    End Using
                End Using

                downloadedFileSize = totalRead
            End Using

            worker.ReportProgress(90, "Validating downloaded file...")

            ' Validate file
            'Dim validErr As String = ValidateDownloadedFile(tempFilePath)
            'If validErr IsNot Nothing Then
            '    e.Result = New DownloadResult With {.Success = False, .ErrorMessage = validErr}
            '    Return
            'End If

            If Not AppDownloadOnly Then
                Dim validErr As String = ValidateDownloadedFile(tempFilePath)
                If validErr IsNot Nothing Then
                    e.Result = New DownloadResult With {.Success = False, .ErrorMessage = validErr}
                    Return
                End If
            End If

            worker.ReportProgress(100, "Download complete!")
            e.Result = New DownloadResult With {.Success = True, .FilePath = tempFilePath}

        Catch ex As Exception
            e.Result = New DownloadResult With {.Success = False, .ErrorMessage = "Unexpected error: " & ex.Message}
        End Try
    End Sub

    ' ================================================================
    '  BACKGROUND WORKER - PROGRESS CHANGED
    ' ================================================================
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ProgressBar1.Value = Math.Min(e.ProgressPercentage, 100)
        If e.UserState IsNot Nothing Then
            lblStatus.Text = e.UserState.ToString()
        End If
    End Sub

    ' ================================================================
    '  BACKGROUND WORKER - COMPLETED
    ' ================================================================
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ' Re-enable controls
        txtDatabaseName.Enabled = True
        btnDownloadInstall.Enabled = True
        ProgressBar1.Visible = False

        ' Unhandled exception
        If e.Error IsNot Nothing Then
            lblStatus.Text = "Error occurred"
            MessageBox.Show("Unexpected error: " & e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CleanupTempFile()
            Return
        End If

        Dim result As DownloadResult = CType(e.Result, DownloadResult)

        If result.Success Then

            If AppDownloadOnly Then

                lblStatus.Text = "App Download Complete"

                MessageBox.Show("Download completed successfully." & vbCrLf & _
                                result.FilePath, _
                                "App Download", _
                                MessageBoxButtons.OK, _
                                MessageBoxIcon.Information)

                AppDownloadOnly = False
                Return

            End If

            lblStatus.Text = "Download completed successfully"

            Dim answer As DialogResult = MessageBox.Show(
                "Setup downloaded successfully." & vbCrLf & vbCrLf &
                "Do you want to start installation?",
                "Download Complete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If answer = DialogResult.Yes Then
                Try
                    Dim startInfo As New ProcessStartInfo()
                    startInfo.FileName = result.FilePath
                    startInfo.UseShellExecute = True
                    startInfo.Verb = "open"
                    Process.Start(startInfo)
                    lblStatus.Text = "Installer started"
                Catch ex As Exception
                    MessageBox.Show("Failed to start installer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    lblStatus.Text = "Failed to start installer"
                End Try
            Else
                lblStatus.Text = "Setup saved: " & result.FilePath
            End If
        Else
            lblStatus.Text = "Download failed"
            MessageBox.Show(result.ErrorMessage, "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CleanupTempFile()
        End If
    End Sub

    ' ================================================================
    '  HELPER: Extract "message" from JSON  (no JSON library needed)
    ' ================================================================
    Private Function ExtractJsonMessage(ByVal jsonText As String) As String
        Try
            If String.IsNullOrEmpty(jsonText) Then Return ""
            Dim m As Match = Regex.Match(jsonText, """message""\s*:\s*""([^""]+)""", RegexOptions.IgnoreCase)
            If m.Success Then Return m.Groups(1).Value
        Catch
        End Try
        Return ""
    End Function

    ' ================================================================
    '  HELPER: Validate downloaded EXE file
    ' ================================================================
    Private Function ValidateDownloadedFile(ByVal filePath As String) As String
        Try
            If Not File.Exists(filePath) Then Return "Downloaded file does not exist."

            Dim info As New FileInfo(filePath)
            If info.Length = 0 Then Return "Downloaded file is empty."
            If info.Length < 64 Then Return "Downloaded file is too small to be a valid installer."

            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                Dim hdr(19) As Byte
                Dim n As Integer = fs.Read(hdr, 0, 20)
                If n >= 2 Then
                    ' Check for JSON/HTML error page
                    Dim txt As String = System.Text.Encoding.ASCII.GetString(hdr, 0, n)
                    If txt.StartsWith("{") OrElse txt.ToLower().StartsWith("<") Then
                        Return "The server returned an error page instead of a file. Please check server configuration."
                    End If
                    ' Check MZ header
                    If hdr(0) <> 77 OrElse hdr(1) <> 90 Then ' M=77 Z=90
                        Return "Downloaded file is not a valid Windows executable (missing MZ header)."
                    End If
                End If
            End Using

            Return Nothing ' Valid
        Catch ex As Exception
            Return "Error validating file: " & ex.Message
        End Try
    End Function

    ' ================================================================
    '  HELPER: Format bytes for display
    ' ================================================================
    Private Function FormatBytes(ByVal bytes As Long) As String
        If bytes < 1024 Then Return bytes & " B"
        If bytes < 1048576 Then Return (bytes / 1024).ToString("F1") & " KB"
        Return (bytes / 1048576).ToString("F1") & " MB"
    End Function

    ' ================================================================
    '  HELPER: Cleanup temp file on failure
    ' ================================================================
    Private Sub CleanupTempFile()
        Try
            If Not String.IsNullOrEmpty(tempFilePath) AndAlso File.Exists(tempFilePath) Then
                File.Delete(tempFilePath)
            End If
        Catch
        End Try
    End Sub

    ' ================================================================
    '  FORM CLOSING
    ' ================================================================
    Private Sub FrmDownload_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If BackgroundWorker1.IsBusy Then
            If MessageBox.Show("Download is in progress. Do you want to cancel and exit?",
                               "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                e.Cancel = True
                Return
            End If
        End If
        CleanupTempFile()
    End Sub

    Private Sub BtnShowSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShowSQL.Click
        Dim frm As New FrmSqlQueryUpdate
        frm.ShowDialog()
    End Sub

    Private Sub BtnCommonInstall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCommonInstall.Click
        AppDownloadOnly = False

        If BackgroundWorker1.IsBusy Then Exit Sub

        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        lblStatus.Text = "Downloading COMMON OFFILINE EXE..."

        BackgroundWorker1.RunWorkerAsync("COMMON OFFILINE EXE")

    End Sub
    Private Sub BtnAppDownload_Click(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) _
                                   Handles BtnAppDownload.Click

        If BackgroundWorker1.IsBusy Then Exit Sub

        AppDownloadOnly = True

        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        lblStatus.Text = "Downloading App File..."

        BackgroundWorker1.RunWorkerAsync("OFFLINE VB APP")

        'AppDownloadOnly = True
        'BackgroundWorker1.RunWorkerAsync("OFFLINE VB APP")

    End Sub

End Class
Public Class DownloadResult
    Public Success As Boolean
    Public ErrorMessage As String
    Public FilePath As String
End Class