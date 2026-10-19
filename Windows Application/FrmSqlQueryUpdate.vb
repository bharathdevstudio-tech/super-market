Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.Sql
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class FrmSqlQueryUpdate

#Region "Form Load - TLS Fix"

    Private Sub FrmSqlQueryUpdate_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Force TLS 1.2 — required for modern hosting (Hostinger, etc.)
        ' .NET 3.5 defaults to TLS 1.0 which is rejected by most servers
        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType) ' TLS 1.2
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateCert

        txtDatabaseName.Text = VistawinFunctions.CmpDatabaseName

        Me.LoadRemoteFiles()
    End Sub

    Private Shared Function ValidateCert(ByVal sender As Object,
                                         ByVal cert As X509Certificate,
                                         ByVal chain As X509Chain,
                                         ByVal errors As SslPolicyErrors) As Boolean
        Return True ' Accept all certs (needed for some hosting providers)
    End Function

#End Region

#Region "Connection String Builder"

    Private Function GetConnectionString(ByVal databaseName As String) As String
        Dim builder As New SqlConnectionStringBuilder()
        builder.DataSource = txtSqlServerName.Text.Trim()
        builder.InitialCatalog = databaseName
        builder.UserID = "sa"
        builder.Password = "YOUR_SA_PASSWORD"
        builder.IntegratedSecurity = False
        builder.ConnectTimeout = 15
        Return builder.ConnectionString
    End Function

#End Region




#Region "Remote SQL Files - Load From Hostinger"
    Private Sub btnLoadRemoteFiles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadRemoteFiles.Click
        Me.LoadRemoteFiles()
    End Sub
    Private Sub LoadRemoteFiles()
        Dim apiUrl As String = txtApiUrl.Text.Trim()
        Dim apiKey As String = txtApiKey.Text.Trim()

        If String.IsNullOrEmpty(apiUrl) Then
            MessageBox.Show("Please enter the API URL.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrEmpty(apiKey) Then
            MessageBox.Show("Please enter the API Key.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        cboRemoteFiles.Items.Clear()
        lblStatus.Text = "Status: Loading remote files..."
        AppendLog("Fetching SQL files from: " & apiUrl)
        Application.DoEvents()

        Try
            ' Build URL with action=list
            Dim listUrl As String = apiUrl
            If Not listUrl.Contains("?") Then
                listUrl &= "?action=list"
            Else
                listUrl &= "&action=list"
            End If

            ' Create web request with API Key header
            Dim request As HttpWebRequest = CType(WebRequest.Create(listUrl), HttpWebRequest)
            request.Method = "GET"
            request.Headers.Add("X-Api-Key", apiKey)
            request.UserAgent = "SqlExecutor/1.1"
            request.Timeout = 30000
            request.KeepAlive = False

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim stream As New StreamReader(response.GetResponseStream())
            Dim responseText As String = stream.ReadToEnd()
            stream.Close()
            response.Close()

            ' Parse JSON - extract file names from the "files" array only
            Dim fileCount As Integer = 0

            ' First locate the "files": [ array
            Dim filesArrayStart As Integer = responseText.IndexOf("""files"":[")
            If filesArrayStart = -1 Then
                AppendLog("Error: No files array in response.")
                Return
            End If

            ' Work only within the files array to avoid picking up "folder" field
            Dim searchFrom As Integer = filesArrayStart + 9 ' skip past "files":[

            Do While True
                ' Find next "name": inside the files array
                Dim nameKeyPos As Integer = responseText.IndexOf("""name"":", searchFrom)
                If nameKeyPos = -1 Then Exit Do

                ' Find the opening quote of the value
                Dim valStart As Integer = responseText.IndexOf("""", nameKeyPos + 7)
                If valStart = -1 Then Exit Do

                ' Find the closing quote (handle escaped quotes)
                Dim valEnd As Integer = valStart + 1
                Do While valEnd < responseText.Length
                    If responseText(valEnd) = """"c AndAlso responseText(valEnd - 1) <> "\"c Then
                        Exit Do
                    End If
                    valEnd += 1
                Loop

                Dim fileName As String = responseText.Substring(valStart + 1, valEnd - valStart - 1)
                If Not String.IsNullOrEmpty(fileName) Then
                    cboRemoteFiles.Items.Add(fileName)
                    fileCount += 1
                End If

                searchFrom = valEnd + 1
            Loop

            If fileCount = 0 Then
                AppendLog("No SQL files found in remote folder.")
                lblStatus.Text = "Status: No remote files found."
            Else
                AppendLog(fileCount.ToString() & " remote SQL file(s) loaded.")
                lblStatus.Text = "Status: " & fileCount.ToString() & " file(s) loaded."
                If cboRemoteFiles.Items.Count > 0 Then
                    cboRemoteFiles.SelectedIndex = 0
                End If
            End If

        Catch ex As WebException
            Dim msg As String = "Web Error: " & ex.Message
            AppendLog(msg)
            lblStatus.Text = "Status: Connection error."
            MessageBox.Show("Connection error: " & msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            AppendLog("Error loading remote files: " & ex.Message)
            lblStatus.Text = "Status: Error loading files."
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Remote SQL Files - Execute"

    Private Sub btnExecuteRemote_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecuteRemote.Click
        Dim serverName As String = txtSqlServerName.Text.Trim()
        If String.IsNullOrEmpty(serverName) Then
            MessageBox.Show("Please enter or select a SQL Server name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txtDatabaseName.Text Is Nothing Then
            MessageBox.Show("Please select a database.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim apiUrl As String = txtApiUrl.Text.Trim()
        Dim apiKey As String = txtApiKey.Text.Trim()

        If String.IsNullOrEmpty(apiUrl) Then
            MessageBox.Show("Please enter the API URL.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrEmpty(apiKey) Then
            MessageBox.Show("Please enter the API Key.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If cboRemoteFiles.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a SQL file.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedFile As String = cboRemoteFiles.SelectedItem.ToString()
        Dim selectedDatabase As String = txtDatabaseName.Text.ToString()

        Dim confirmMsg As String =
            "Execute SQL file?" & Environment.NewLine & Environment.NewLine &
            "Server   : " & serverName & Environment.NewLine &
            "Database : " & selectedDatabase & Environment.NewLine &
            "File     : " & selectedFile

        If MessageBox.Show(confirmMsg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            AppendLog("Execution cancelled.")
            Return
        End If

        AppendLog("Downloading: " & selectedFile)
        lblStatus.Text = "Status: Downloading..."
        Application.DoEvents()

        Dim sqlContent As String = String.Empty
        Try
            ' Build get URL with proper encoding
            Dim getUrl As String = apiUrl
            If Not getUrl.Contains("?") Then
                getUrl &= "?action=get&file=" & UrlEncode(selectedFile)
            Else
                getUrl &= "&action=get&file=" & UrlEncode(selectedFile)
            End If

            Dim request As HttpWebRequest = CType(WebRequest.Create(getUrl), HttpWebRequest)
            request.Method = "GET"
            request.Headers.Add("X-Api-Key", apiKey)
            request.UserAgent = "SqlExecutor/1.1"
            request.Timeout = 30000
            request.KeepAlive = False

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim stream As New StreamReader(response.GetResponseStream())
            Dim responseText As String = stream.ReadToEnd()
            stream.Close()
            response.Close()

            ' Extract base64 content from JSON — find "content": and read value
            Dim contentKey As Integer = responseText.IndexOf("""content"":")
            If contentKey = -1 Then
                AppendLog("Error: No content field in response.")
                MessageBox.Show("Server response missing content field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim cValStart As Integer = responseText.IndexOf("""", contentKey + 10)
            If cValStart = -1 Then
                AppendLog("Error: Malformed content field.")
                Return
            End If

            ' Read until closing quote (handling escaped quotes)
            Dim cValEnd As Integer = cValStart + 1
            Do While cValEnd < responseText.Length
                If responseText(cValEnd) = """"c AndAlso responseText(cValEnd - 1) <> "\"c Then
                    Exit Do
                End If
                cValEnd += 1
            Loop

            Dim encodedContent As String = responseText.Substring(cValStart + 1, cValEnd - cValStart - 1)
            Dim decodedBytes As Byte() = Convert.FromBase64String(encodedContent)
            sqlContent = Encoding.UTF8.GetString(decodedBytes)
            AppendLog("Downloaded. Size: " & decodedBytes.Length & " bytes")

            If String.IsNullOrEmpty(sqlContent.Trim()) Then
                MessageBox.Show("The SQL file is empty.", "Empty File", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ExecuteSqlContent(sqlContent, serverName, selectedDatabase, selectedFile)

        Catch ex As WebException
            AppendLog("Download failed: " & ex.Message)
            lblStatus.Text = "Status: Download failed."
            MessageBox.Show("Download failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            AppendLog("Error: " & ex.Message)
            lblStatus.Text = "Status: Error."
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Simple URL encoding
    Private Function UrlEncode(ByVal text As String) As String
        Dim result As New StringBuilder()
        For Each c As Char In text
            If Char.IsLetterOrDigit(c) OrElse "-_.~".Contains(c) Then
                result.Append(c)
            Else
                For Each b As Byte In Encoding.UTF8.GetBytes(New Char() {c})
                    result.Append("%" & b.ToString("X2"))
                Next
            End If
        Next
        Return result.ToString()
    End Function

#End Region

#Region "SQL Execution"

    Private Sub ExecuteSqlContent(ByVal sqlContent As String, ByVal serverName As String, ByVal databaseName As String, ByVal sourceFileName As String)
        Dim batches As String() = VSqlBatchSplitter.Split(sqlContent)

        If batches Is Nothing OrElse batches.Length = 0 Then
            MessageBox.Show("No SQL batches found.", "No Batches", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        rtbLog.Clear()
        progressBar.Minimum = 0
        progressBar.Maximum = batches.Length
        progressBar.Value = 0
        btnLoadRemoteFiles.Enabled = False
        btnExecuteRemote.Enabled = False

        AppendLog("=== SQL Execution Started ===")
        AppendLog("Server      : " & serverName)
        AppendLog("Database    : " & databaseName)
        AppendLog("SQL File    : " & sourceFileName)
        AppendLog("Batches     : " & batches.Length.ToString())
        AppendLog("Transaction : " & If(chkUseTransaction.Checked, "Yes", "No"))
        AppendLog("Start Time  : " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        AppendLog("-----")

        Dim startTime As DateTime = DateTime.Now
        Dim useTransaction As Boolean = chkUseTransaction.Checked

        Dim conn As SqlConnection = Nothing
        Dim tran As SqlTransaction = Nothing

        Try
            Dim connStr As String = GetConnectionString(databaseName)
            conn = New SqlConnection(connStr)
            conn.Open()
            AppendLog("Connected to database: " & databaseName)

            If useTransaction Then
                tran = conn.BeginTransaction()
                AppendLog("Transaction started.")
            End If

            Dim successCount As Integer = 0

            For batchIndex As Integer = 0 To batches.Length - 1
                Dim batchSQL As String = batches(batchIndex).Trim()

                If String.IsNullOrEmpty(batchSQL) Then
                    progressBar.Value = batchIndex + 1
                    Continue For
                End If

                lblStatus.Text = "Batch " & (batchIndex + 1).ToString() & " of " & batches.Length.ToString()
                Application.DoEvents()

                Dim cmd As SqlCommand = Nothing
                Try
                    cmd = New SqlCommand(batchSQL, conn)
                    If tran IsNot Nothing Then
                        cmd.Transaction = tran
                    End If
                    cmd.CommandTimeout = 300
                    cmd.ExecuteNonQuery()
                    successCount += 1
                    AppendLog("✓ Batch " & (batchIndex + 1).ToString() & " OK")
                Catch ex As SqlException
                    AppendLog("✗ Batch " & (batchIndex + 1).ToString() & " ERROR")
                    For Each sqlError As SqlError In ex.Errors
                        AppendLog("  Error: " & sqlError.Message)
                    Next

                    If useTransaction AndAlso tran IsNot Nothing Then
                        Try
                            tran.Rollback()
                            AppendLog("  Transaction rolled back.")
                        Catch
                        End Try
                        tran = Nothing
                    End If

                    MessageBox.Show("Batch " & (batchIndex + 1).ToString() & " failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    progressBar.Value = batchIndex + 1
                    GoTo ExecutionFinished
                Finally
                    If cmd IsNot Nothing Then
                        cmd.Dispose()
                    End If
                End Try

                progressBar.Value = batchIndex + 1
                Application.DoEvents()
            Next

            If useTransaction AndAlso tran IsNot Nothing Then
                Try
                    tran.Commit()
                    AppendLog("Transaction committed.")
                Catch ex As Exception
                    AppendLog("Commit failed: " & ex.Message)
                    Try
                        tran.Rollback()
                    Catch
                    End Try
                    tran = Nothing
                    GoTo ExecutionFinished
                End Try
                tran = Nothing
            End If

            Dim elapsed As TimeSpan = DateTime.Now - startTime
            AppendLog("-----")
            AppendLog("✓ Success: " & successCount.ToString() & "/" & batches.Length.ToString() & " batches")
            AppendLog("Elapsed: " & String.Format("{0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds))
            AppendLog("=== Execution Completed ===")
            lblStatus.Text = "Status: Completed successfully."
            MessageBox.Show("Success! " & successCount.ToString() & " batch(es) executed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

ExecutionFinished:

        Catch ex As Exception
            AppendLog("Error: " & ex.Message)
            lblStatus.Text = "Status: Error."
            MessageBox.Show("Execution failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If tran IsNot Nothing Then
                Try
                    tran.Rollback()
                Catch
                End Try
                tran.Dispose()
            End If
            If conn IsNot Nothing Then
                conn.Dispose()
            End If
            btnLoadRemoteFiles.Enabled = True
            btnExecuteRemote.Enabled = True
        End Try
    End Sub

#End Region

#Region "Helper Methods"

    Private Sub AppendLog(ByVal message As String)
        rtbLog.AppendText(message & Environment.NewLine)
        rtbLog.ScrollToCaret()
        Application.DoEvents()
    End Sub

    Private Function BuildSqlErrorMessage(ByVal ex As SqlException) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("SQL Error:")
        For Each sqlError As SqlError In ex.Errors
            sb.AppendLine("  " & sqlError.Number.ToString() & ": " & sqlError.Message)
        Next
        Return sb.ToString()
    End Function

#End Region

End Class
