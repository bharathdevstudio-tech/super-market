Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Web.Script.Serialization

Public Class FrmDownloadVistawin

    ' ── API endpoint (no trailing slash) ──────────────────────────────────
    Private Const API_BASE As String = "https://vistawin.online/api"

    ' Current folder relative to uploads/ ("" = root)
    Private currentPath As String = ""

    ' Tag payload for each ListView row
    Private Class RowInfo
        Public Name As String
        Public IsDir As Boolean
        Public RelPath As String
        Public DownloadUrl As String
    End Class

    ' ── FORM LOAD ─────────────────────────────────────────────────────────
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MyBase.Load

        ' TLS 1.2 required for HTTPS on .NET 3.5
        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)

        ' ListView columns
        lvFiles.View = View.Details
        lvFiles.FullRowSelect = True
        lvFiles.GridLines = True
        lvFiles.MultiSelect = False
        lvFiles.Columns.Add("Name", 240)
        lvFiles.Columns.Add("Type", 70)
        lvFiles.Columns.Add("Size", 90)
        lvFiles.Columns.Add("Modified", 150)
        lvFiles.Columns.Add("Download URL", 240)

        UpdatePathLabel()
        lblStatus.Text = "Click 'Load Files' to fetch the file list."
        btnUp.Enabled = False
    End Sub

    ' ── PATH HELPERS ──────────────────────────────────────────────────────
    Private Sub UpdatePathLabel()
        If currentPath = "" Then
            lblPath.Text = "📂  /  (root)"
        Else
            lblPath.Text = "📂  /" & currentPath.Replace("\", "/")
        End If
        btnUp.Enabled = (currentPath <> "")
    End Sub

    Private Function ParentPath(ByVal path As String) As String
        If String.IsNullOrEmpty(path) Then Return ""
        Dim i As Integer = path.LastIndexOf("/"c)
        If i < 0 Then Return ""
        Return path.Substring(0, i)
    End Function

    Private Function JoinPath(ByVal base As String, ByVal name As String) As String
        If String.IsNullOrEmpty(base) Then Return name
        Return base.TrimEnd("/"c) & "/" & name
    End Function

    ' ── LOAD FILES ────────────────────────────────────────────────────────
    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles btnLoad.Click
        LoadCurrentFolder()
    End Sub

    Private Sub LoadCurrentFolder()
        lvFiles.Items.Clear()
        lblStatus.Text = "Connecting to server..."
        UpdatePathLabel()
        SetButtons(False)
        Application.DoEvents()

        Try
            Dim url As String = API_BASE & "/list_files.php"
            If currentPath <> "" Then
                url &= "?path=" & Uri.EscapeDataString(currentPath)
            End If

            Dim json As String = FetchString(url)

            If String.IsNullOrEmpty(json) Then
                ShowError("Server returned an empty response.")
                Return
            End If

            Dim js As New JavaScriptSerializer()
            Dim root As Dictionary(Of String, Object) = _
                js.Deserialize(Of Dictionary(Of String, Object))(json)

            If root.ContainsKey("error") Then
                ShowError("Server error: " & root("error").ToString())
                Return
            End If

            ' Sync path from server if provided
            If root.ContainsKey("path") AndAlso root("path") IsNot Nothing Then
                currentPath = root("path").ToString()
                UpdatePathLabel()
            End If

            Dim list As ArrayList = CType(root("files"), ArrayList)

            If list.Count = 0 Then
                lblStatus.Text = "Folder is empty."
                Return
            End If

            For Each item As Object In list
                Dim f As Dictionary(Of String, Object) = _
                    CType(item, Dictionary(Of String, Object))

                Dim name As String = f("name").ToString()
                Dim typ As String = "file"
                If f.ContainsKey("type") AndAlso f("type") IsNot Nothing Then
                    typ = f("type").ToString().ToLower()
                End If
                Dim isDir As Boolean = (typ = "dir" OrElse typ = "folder")

                Dim rel As String = ""
                If f.ContainsKey("path") AndAlso f("path") IsNot Nothing Then
                    rel = f("path").ToString()
                Else
                    rel = JoinPath(currentPath, name)
                End If

                Dim dlUrl As String = ""
                If f.ContainsKey("download_url") AndAlso f("download_url") IsNot Nothing Then
                    dlUrl = f("download_url").ToString()
                End If

                Dim displayName As String
                If isDir Then
                    displayName = "📁  " & name
                Else
                    displayName = "📄  " & name
                End If

                Dim lvi As New ListViewItem(displayName)
                lvi.SubItems.Add(If(isDir, "Folder", "File"))
                If isDir Then
                    lvi.SubItems.Add("—")
                Else
                    lvi.SubItems.Add(HumanSize(Convert.ToInt64(f("size"))))
                End If
                lvi.SubItems.Add(f("modified").ToString())
                lvi.SubItems.Add(dlUrl)

                Dim info As New RowInfo()
                info.Name = name
                info.IsDir = isDir
                info.RelPath = rel
                info.DownloadUrl = dlUrl
                lvi.Tag = info

                If isDir Then
                    lvi.ForeColor = Color.FromArgb(0, 90, 180)
                End If

                lvFiles.Items.Add(lvi)
            Next

            lblStatus.Text = "Loaded " & lvFiles.Items.Count & " item(s)."

        Catch ex As WebException
            ShowError("Network error: " & ex.Message)
        Catch ex As Exception
            ShowError("Error: " & ex.Message)
        Finally
            SetButtons(True)
        End Try
    End Sub

    ' ── OPEN FOLDER (double-click) ────────────────────────────────────────
    Private Sub lvFiles_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) _
        Handles lvFiles.DoubleClick
        OpenSelectedFolder()
    End Sub

    Private Sub lvFiles_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) _
        Handles lvFiles.KeyDown
        If e.KeyCode = Keys.Enter Then
            OpenSelectedFolder()
            e.Handled = True
        ElseIf e.KeyCode = Keys.Back Then
            GoUp()
            e.Handled = True
        End If
    End Sub

    Private Sub OpenSelectedFolder()
        If lvFiles.SelectedItems.Count = 0 Then Return
        Dim info As RowInfo = TryCast(lvFiles.SelectedItems(0).Tag, RowInfo)
        If info Is Nothing OrElse Not info.IsDir Then Return

        currentPath = info.RelPath
        LoadCurrentFolder()
    End Sub

    ' ── UP / PARENT FOLDER ────────────────────────────────────────────────
    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles btnUp.Click
        GoUp()
    End Sub

    Private Sub GoUp()
        If currentPath = "" Then Return
        currentPath = ParentPath(currentPath)
        LoadCurrentFolder()
    End Sub

    ' ── NEW FOLDER ────────────────────────────────────────────────────────
    Private Sub btnNewFolder_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles btnNewFolder.Click

        Dim folderName As String = InputBox(
            "Enter a name for the new folder:" & vbCrLf & vbCrLf &
            "Location: " & If(currentPath = "", "/ (root)", "/" & currentPath),
            "New Folder",
            "New Folder")

        If folderName Is Nothing Then Return
        folderName = folderName.Trim()
        If folderName = "" Then Return

        ' Basic client-side name check
        If folderName.IndexOf("/"c) >= 0 OrElse folderName.IndexOf("\"c) >= 0 OrElse _
           folderName = "." OrElse folderName = ".." Then
            ShowError("Invalid folder name.")
            Return
        End If

        SetButtons(False)
        lblStatus.Text = "Creating folder  " & folderName & "  ..."
        Application.DoEvents()

        Try
            Dim postData As String =
                "name=" & Uri.EscapeDataString(folderName) &
                "&path=" & Uri.EscapeDataString(currentPath)

            Dim responseJson As String
            Using wc As New WebClient()
                wc.Headers.Add("User-Agent", "HostingerFileManager/1.0")
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                responseJson = wc.UploadString(API_BASE & "/mkdir.php", "POST", postData)
            End Using

            Dim js As New JavaScriptSerializer()
            Dim resp As Dictionary(Of String, Object) = _
                js.Deserialize(Of Dictionary(Of String, Object))(responseJson)

            If resp.ContainsKey("error") Then
                ShowError("Could not create folder: " & resp("error").ToString())
                lblStatus.Text = "Create folder failed."
                Return
            End If

            Dim created As String = folderName
            If resp.ContainsKey("name") AndAlso resp("name") IsNot Nothing Then
                created = resp("name").ToString()
            End If

            lblStatus.Text = "Created folder: " & created
            MessageBox.Show("Folder created!" & vbCrLf & created,
                            "New Folder", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadCurrentFolder()

        Catch ex As WebException
            Dim code As String = HttpCode(ex)
            Dim body As String = ""
            If ex.Response IsNot Nothing Then
                Try
                    Using sr As New StreamReader(ex.Response.GetResponseStream())
                        body = sr.ReadToEnd()
                    End Using
                    Dim js2 As New JavaScriptSerializer()
                    Dim errR As Dictionary(Of String, Object) = _
                        js2.Deserialize(Of Dictionary(Of String, Object))(body)
                    If errR.ContainsKey("error") Then body = errR("error").ToString()
                Catch
                End Try
            End If
            ShowError("Create folder error" & code & vbCrLf &
                      If(body <> "", body, ex.Message))
            lblStatus.Text = "Create folder failed."
        Catch ex As Exception
            ShowError("Unexpected error: " & ex.Message)
            lblStatus.Text = "Create folder error."
        Finally
            SetButtons(True)
        End Try
    End Sub

    ' ── DOWNLOAD ──────────────────────────────────────────────────────────
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles btnDownload.Click

        If lvFiles.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a file or folder from the list first.",
                            "No Item Selected",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim info As RowInfo = TryCast(lvFiles.SelectedItems(0).Tag, RowInfo)
        If info Is Nothing Then Return

        Dim dlUrl As String = info.DownloadUrl
        If String.IsNullOrEmpty(dlUrl) Then
            ' Build URL from path if list didn't include it
            dlUrl = API_BASE & "/download.php?file=" & Uri.EscapeDataString(info.RelPath)
        End If

        Dim defaultName As String
        If info.IsDir Then
            defaultName = info.Name & ".zip"
        Else
            defaultName = info.Name
        End If

        saveDialog.FileName = defaultName
        If info.IsDir Then
            saveDialog.Filter = "ZIP Archive (*.zip)|*.zip|All Files (*.*)|*.*"
        Else
            saveDialog.Filter = "All Files (*.*)|*.*"
        End If
        saveDialog.Title = If(info.IsDir, "Save Folder As ZIP", "Save As")
        If saveDialog.ShowDialog() <> DialogResult.OK Then Return

        Dim savePath As String = saveDialog.FileName
        SetButtons(False)
        If info.IsDir Then
            lblStatus.Text = "Zipping & downloading folder  " & info.Name & "  ..."
        Else
            lblStatus.Text = "Downloading  " & info.Name & "  ..."
        End If
        progressBar.Visible = True
        progressBar.Style = ProgressBarStyle.Marquee
        Application.DoEvents()

        Try
            Using wc As New WebClient()
                wc.Headers.Add("User-Agent", "HostingerFileManager/1.0")
                wc.DownloadFile(dlUrl, savePath)
            End Using
            lblStatus.Text = "Downloaded: " & Path.GetFileName(savePath)
            MessageBox.Show("Downloaded successfully!" & vbCrLf & "Saved to: " & savePath,
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As WebException
            Dim code As String = HttpCode(ex)
            Dim body As String = ""
            If ex.Response IsNot Nothing Then
                Try
                    Using sr As New StreamReader(ex.Response.GetResponseStream())
                        body = sr.ReadToEnd()
                    End Using
                Catch
                End Try
            End If
            ShowError("Download failed" & code & vbCrLf &
                      If(body <> "", body, ex.Message))
            lblStatus.Text = "Download failed."
            TryDelete(savePath)
        Catch ex As Exception
            ShowError("Error during download: " & ex.Message)
            TryDelete(savePath)
        Finally
            progressBar.Visible = False
            SetButtons(True)
        End Try
    End Sub

    ' ── RENAME ────────────────────────────────────────────────────────────
    Private Sub btnRename_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles btnRename.Click

        If lvFiles.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a file or folder to rename.",
                            "No Item Selected",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim info As RowInfo = TryCast(lvFiles.SelectedItems(0).Tag, RowInfo)
        If info Is Nothing Then Return

        Dim kind As String = If(info.IsDir, "folder", "file")
        Dim newName As String = InputBox(
            "Enter a new name for this " & kind & ":" & vbCrLf & vbCrLf &
            "Current: " & info.Name,
            "Rename " & kind,
            info.Name)

        If newName Is Nothing Then Return
        newName = newName.Trim()
        If newName = "" Then Return

        If newName = info.Name Then
            lblStatus.Text = "Name unchanged."
            Return
        End If

        If newName.IndexOf("/"c) >= 0 OrElse newName.IndexOf("\"c) >= 0 OrElse _
           newName = "." OrElse newName = ".." Then
            ShowError("Invalid name.")
            Return
        End If

        SetButtons(False)
        lblStatus.Text = "Renaming  " & info.Name & "  ..."
        Application.DoEvents()

        Try
            Dim postData As String =
                "file=" & Uri.EscapeDataString(info.RelPath) &
                "&new_name=" & Uri.EscapeDataString(newName)

            Dim responseJson As String
            Using wc As New WebClient()
                wc.Headers.Add("User-Agent", "HostingerFileManager/1.0")
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                responseJson = wc.UploadString(API_BASE & "/rename.php", "POST", postData)
            End Using

            Dim js As New JavaScriptSerializer()
            Dim resp As Dictionary(Of String, Object) = _
                js.Deserialize(Of Dictionary(Of String, Object))(responseJson)

            If resp.ContainsKey("error") Then
                ShowError("Rename failed: " & resp("error").ToString())
                lblStatus.Text = "Rename failed."
                Return
            End If

            Dim finalName As String = newName
            If resp.ContainsKey("name") AndAlso resp("name") IsNot Nothing Then
                finalName = resp("name").ToString()
            End If

            lblStatus.Text = "Renamed to: " & finalName
            MessageBox.Show("Renamed successfully!" & vbCrLf &
                            info.Name & "  →  " & finalName,
                            "Rename Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadCurrentFolder()

        Catch ex As WebException
            Dim code As String = HttpCode(ex)
            Dim body As String = ""
            If ex.Response IsNot Nothing Then
                Try
                    Using sr As New StreamReader(ex.Response.GetResponseStream())
                        body = sr.ReadToEnd()
                    End Using
                    Dim js2 As New JavaScriptSerializer()
                    Dim errR As Dictionary(Of String, Object) = _
                        js2.Deserialize(Of Dictionary(Of String, Object))(body)
                    If errR.ContainsKey("error") Then body = errR("error").ToString()
                Catch
                End Try
            End If
            ShowError("Rename error" & code & vbCrLf &
                      If(body <> "", body, ex.Message))
            lblStatus.Text = "Rename failed."
        Catch ex As Exception
            ShowError("Unexpected error: " & ex.Message)
            lblStatus.Text = "Rename error."
        Finally
            SetButtons(True)
        End Try
    End Sub

    ' ── DELETE ────────────────────────────────────────────────────────────
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles btnDelete.Click

        If lvFiles.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a file or folder from the list first.",
                            "No Item Selected",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim info As RowInfo = TryCast(lvFiles.SelectedItems(0).Tag, RowInfo)
        If info Is Nothing Then Return

        Dim kind As String = If(info.IsDir, "folder (and everything inside)", "file")
        Dim ans As DialogResult = MessageBox.Show(
            "Permanently delete this " & kind & " from the server?" & vbCrLf & vbCrLf &
            info.RelPath,
            "Confirm Delete",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If ans <> DialogResult.Yes Then Return

        SetButtons(False)
        lblStatus.Text = "Deleting  " & info.Name & "  ..."
        Application.DoEvents()

        Try
            Dim postData As String = "file=" & Uri.EscapeDataString(info.RelPath)
            Dim responseJson As String

            Using wc As New WebClient()
                wc.Headers.Add("User-Agent", "HostingerFileManager/1.0")
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                responseJson = wc.UploadString(API_BASE & "/delete.php", "POST", postData)
            End Using

            Dim js As New JavaScriptSerializer()
            Dim resp As Dictionary(Of String, Object) = _
                js.Deserialize(Of Dictionary(Of String, Object))(responseJson)

            If resp.ContainsKey("error") Then
                ShowError("Delete failed: " & resp("error").ToString())
                lblStatus.Text = "Delete failed."
                Return
            End If

            lblStatus.Text = "Deleted: " & info.Name
            MessageBox.Show("Deleted successfully!" & vbCrLf & info.RelPath,
                            "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadCurrentFolder()

        Catch ex As WebException
            Dim code As String = HttpCode(ex)
            Dim body As String = ""
            If ex.Response IsNot Nothing Then
                Try
                    Using sr As New StreamReader(ex.Response.GetResponseStream())
                        body = sr.ReadToEnd()
                    End Using
                    Dim js2 As New JavaScriptSerializer()
                    Dim errR As Dictionary(Of String, Object) = _
                        js2.Deserialize(Of Dictionary(Of String, Object))(body)
                    If errR.ContainsKey("error") Then body = errR("error").ToString()
                Catch
                End Try
            End If
            ShowError("Delete error" & code & vbCrLf &
                      If(body <> "", body, ex.Message))
            lblStatus.Text = "Delete failed."
        Catch ex As Exception
            ShowError("Unexpected error: " & ex.Message)
            lblStatus.Text = "Delete error."
        Finally
            SetButtons(True)
        End Try
    End Sub

    ' ── UPLOAD ────────────────────────────────────────────────────────────
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles btnUpload.Click

        openDialog.Title = "Select File to Upload"
        openDialog.Filter = "All Files (*.*)|*.*"
        openDialog.FileName = ""
        If openDialog.ShowDialog() <> DialogResult.OK Then Return

        Dim filePath As String = openDialog.FileName
        Dim fileName As String = Path.GetFileName(filePath)
        Dim fileSize As Long = New FileInfo(filePath).Length

        If fileSize > 10 * 1024 * 1024 Then
            Dim ans As DialogResult = MessageBox.Show(
                "File size is " & HumanSize(fileSize) & ". Upload anyway?",
                "Large File",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ans <> DialogResult.Yes Then Return
        End If

        SetButtons(False)
        Dim destLabel As String = If(currentPath = "", "/ (root)", "/" & currentPath)
        lblStatus.Text = "Uploading  " & fileName & "  →  " & destLabel
        progressBar.Visible = True
        progressBar.Style = ProgressBarStyle.Marquee
        Application.DoEvents()

        Try
            Dim uploadUrl As String = API_BASE & "/upload.php"
            If currentPath <> "" Then
                uploadUrl &= "?path=" & Uri.EscapeDataString(currentPath)
            End If
            Dim responseJson As String = UploadFile(filePath, uploadUrl, currentPath)

            Dim js As New JavaScriptSerializer()
            Dim resp As Dictionary(Of String, Object) = _
                js.Deserialize(Of Dictionary(Of String, Object))(responseJson)

            If resp.ContainsKey("error") Then
                ShowError("Upload failed: " & resp("error").ToString())
                lblStatus.Text = "Upload failed."
                Return
            End If

            Dim uploadedName As String = resp("name").ToString()
            Dim uploadedSize As Long = Convert.ToInt64(resp("size"))

            lblStatus.Text = "Uploaded: " & uploadedName & " (" & HumanSize(uploadedSize) & ")"
            MessageBox.Show("Upload successful!" & vbCrLf &
                            "File: " & uploadedName & vbCrLf &
                            "Folder: " & destLabel & vbCrLf &
                            "Size: " & HumanSize(uploadedSize),
                            "Upload Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadCurrentFolder()

        Catch ex As WebException
            Dim code As String = HttpCode(ex)
            Dim body As String = ""
            If ex.Response IsNot Nothing Then
                Try
                    Using sr As New StreamReader(ex.Response.GetResponseStream())
                        body = sr.ReadToEnd()
                    End Using
                    Dim js2 As New JavaScriptSerializer()
                    Dim errR As Dictionary(Of String, Object) = _
                        js2.Deserialize(Of Dictionary(Of String, Object))(body)
                    If errR.ContainsKey("error") Then body = errR("error").ToString()
                Catch
                End Try
            End If
            ShowError("Upload error" & code & vbCrLf &
                      If(body <> "", body, ex.Message))
            lblStatus.Text = "Upload failed."
        Catch ex As Exception
            ShowError("Unexpected error: " & ex.Message)
            lblStatus.Text = "Upload error."
        Finally
            SetButtons(True)
            progressBar.Visible = False
        End Try
    End Sub

    ' ── MULTIPART UPLOAD HELPER ───────────────────────────────────────────
    Private Function UploadFile(ByVal localPath As String,
                                ByVal uploadUrl As String,
                                ByVal destFolder As String) As String

        Dim boundary As String = "----FormBoundary" & DateTime.Now.Ticks.ToString("x")
        Dim newline As String = vbCrLf
        Dim ms As New MemoryStream()

        ' Optional path field so server places file in current folder
        If Not String.IsNullOrEmpty(destFolder) Then
            Dim pathPart As String =
                "--" & boundary & newline &
                "Content-Disposition: form-data; name=""path""" & newline & newline &
                destFolder & newline
            Dim pathBytes As Byte() = Encoding.UTF8.GetBytes(pathPart)
            ms.Write(pathBytes, 0, pathBytes.Length)
        End If

        Dim fileName As String = Path.GetFileName(localPath)
        Dim header As String =
            "--" & boundary & newline &
            "Content-Disposition: form-data; name=""file""; filename=""" & fileName & """" & newline &
            "Content-Type: application/octet-stream" & newline & newline

        Dim headerBytes As Byte() = Encoding.UTF8.GetBytes(header)
        ms.Write(headerBytes, 0, headerBytes.Length)

        Dim fileBytes As Byte() = File.ReadAllBytes(localPath)
        ms.Write(fileBytes, 0, fileBytes.Length)

        Dim footer As String = newline & "--" & boundary & "--" & newline
        Dim footerBytes As Byte() = Encoding.UTF8.GetBytes(footer)
        ms.Write(footerBytes, 0, footerBytes.Length)

        Dim body As Byte() = ms.ToArray()
        ms.Dispose()

        Using wc As New WebClient()
            wc.Headers.Add("User-Agent", "HostingerFileManager/1.0")
            wc.Headers.Add("Content-Type", "multipart/form-data; boundary=" & boundary)
            Dim responseBytes As Byte() = wc.UploadData(uploadUrl, "POST", body)
            Return Encoding.UTF8.GetString(responseBytes)
        End Using
    End Function

    ' ── HELPERS ───────────────────────────────────────────────────────────

    Private Function FetchString(ByVal url As String) As String
        Using wc As New WebClient()
            wc.Headers.Add("User-Agent", "HostingerFileManager/1.0")
            Return wc.DownloadString(url)
        End Using
    End Function

    Private Function HumanSize(ByVal b As Long) As String
        If b >= 1073741824L Then Return String.Format("{0:0.00} GB", b / 1073741824.0)
        If b >= 1048576L Then Return String.Format("{0:0.00} MB", b / 1048576.0)
        If b >= 1024L Then Return String.Format("{0:0.00} KB", b / 1024.0)
        Return b & " B"
    End Function

    Private Function HttpCode(ByVal ex As WebException) As String
        If TypeOf ex.Response Is HttpWebResponse Then
            Return " [HTTP " & CType(ex.Response, HttpWebResponse).StatusCode & "]"
        End If
        Return ""
    End Function

    Private Sub ShowError(ByVal msg As String)
        MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        lblStatus.Text = "Error."
    End Sub

    Private Sub TryDelete(ByVal path As String)
        Try
            If File.Exists(path) Then File.Delete(path)
        Catch
        End Try
    End Sub

    Private Sub SetButtons(ByVal enabled As Boolean)
        btnLoad.Enabled = enabled
        btnDownload.Enabled = enabled
        btnUpload.Enabled = enabled
        btnRename.Enabled = enabled
        btnDelete.Enabled = enabled
        btnNewFolder.Enabled = enabled
        btnUp.Enabled = enabled AndAlso (currentPath <> "")
    End Sub

End Class
