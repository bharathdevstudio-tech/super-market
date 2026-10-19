Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmMiniLogIn
    Dim Ds As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Row As Integer
    Dim BVistawinSolution1 As New BVistawinSolution

    Dim ImageData As Byte()
    Dim MS1 As New MemoryStream()

    Dim SysNameChk1 As String = My.Computer.Name.ToUpper()
    Dim SqlServerNameChk1 As String = ""
    Dim SqlDBNameChk1 As String = ""
    Private FoundSqlServer As String = ""
    Private Class DBItem
        Public Name As String
        Public Server As String
        Public Database As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private DBList As New List(Of DBItem)
    Private Sub FrmMiniLogIn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.LoginImgCheck()

        txtPassword.Clear()
        txtUserName.Clear()

        CbSqlServerName.Items.Clear()

        Select Case SysNameChk1

            Case "GANESH-PC"
                CbSqlServerName.Items.Add("GANESH-PC")

            Case "V1SYSTEM", "V2SYSTEM", "V3SYSTEM", "V4OFFICE", "OFFICE-LAP"
                CbSqlServerName.Items.Add(SysNameChk1 & "\SQLEXPRESS")

            Case "DESKTOP-HS99BEG"
                CbSqlServerName.Items.Add("DESKTOP-HS99BEG\VISTAWIN")

            Case Else
                CbSqlServerName.Hide()
                CbDBSel.Hide()
                CbDatabase.Show()

                CreateDBConfigIfNotExists()
                LoadDatabaseList()

                txtUserName.Focus()
                Exit Sub
        End Select

        CbDatabase.Hide()
        CbSqlServerName.Show()
        CbDBSel.Show()

        If CbSqlServerName.Items.Count > 0 Then
            CbSqlServerName.SelectedIndex = 0
        End If

        lblStatus.Hide()
        txtUserName.Focus()

    End Sub
    Private Sub CbSqlServerName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CbSqlServerName.TextChanged

        Select Case SysNameChk1
            Case "GANESH-PC", "OFFICE-LAP", "V1SYSTEM", "V2SYSTEM", "V3SYSTEM", "V4OFFICE", "DESKTOP-HS99BEG"
                Me.GetSystemDBlist()
                'Case "V3SYSTEM"
                '    CbDBSel.Items.Add("BHAVANIELECSURDB")
                '    CbDBSel.SelectedIndex = 0
            Case Else
        End Select

    End Sub
   
    Private Sub btnLogIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
        Try
            Select Case SysNameChk1
                Case "GANESH-PC", "OFFICE-LAP", "V1SYSTEM", "V2SYSTEM", "V3SYSTEM", "V4OFFICE", "DESKTOP-HS99BEG"

                    If CbSqlServerName.SelectedItem Is Nothing OrElse CbDBSel.SelectedItem Is Nothing Then
                        MessageBox.Show("Select Server and Database.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    SqlServerNameChk1 = CbSqlServerName.Text.Trim()
                    SqlDBNameChk1 = CbDBSel.Text.Trim()

                Case Else
                    If CbDatabase.SelectedItem Is Nothing Then
                        MessageBox.Show("Select Database.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        CbDatabase.Focus()
                        Exit Sub
                    End If

                    Dim DB As DBItem = TryCast(CbDatabase.SelectedItem, DBItem)
                    If DB Is Nothing Then
                        MessageBox.Show("Invalid Database Configuration.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    SqlServerNameChk1 = DB.Server.Trim()
                    SqlDBNameChk1 = DB.Database.Trim()
            End Select

            If VistawinFunctions.ServerConnection IsNot Nothing Then
                If VistawinFunctions.ServerConnection.State <> ConnectionState.Closed Then
                    VistawinFunctions.ServerConnection.Close()
                End If
                VistawinFunctions.ServerConnection.Dispose()
            End If

            Dim ConString As String = "Data Source=" & SqlServerNameChk1 & ";Initial Catalog=" & SqlDBNameChk1 & ";User ID=sa" & ";Password=YOUR_SA_PASSWORD" & ";Connect Timeout=5"
            VistawinFunctions.ServerConnection = New SqlConnection(ConString)
            VistawinFunctions.ServerConnection.Open()

            VistawinFunctions.VWVerDate = "06.10.2026"
            VistawinFunctions.SqlServerNameShow = SqlServerNameChk1
            VistawinFunctions.CmpDatabaseName = SqlDBNameChk1

            If CInt(GetValue("SELECT COUNT(*) FROM tbl_UserList")) = 0 Then
                Using cmd As New SqlCommand("INSERT INTO tbl_UserList(U_EmpID_i,U_EmpName_vc,U_UserName_vc,U_Password_vc) VALUES(1,'SOFTWARE','admin','admin')", VistawinFunctions.ServerConnection)
                    cmd.ExecuteNonQuery()
                End Using
            End If

            If {"GANESH-PC", "OFFICE-LAP", "V1SYSTEM", "V2SYSTEM", "V3SYSTEM", "V4OFFICE", "DESKTOP-HS99BEG"}.Contains(SysNameChk1) Then
                Using cmd As New SqlCommand("SELECT U_UserName_vc,U_Password_vc FROM tbl_UserList WHERE U_EmpID_i=1", VistawinFunctions.ServerConnection)
                    Using dr As SqlDataReader = cmd.ExecuteReader
                        If dr.Read Then
                            txtUserName.Text = dr("U_UserName_vc").ToString
                            txtPassword.Text = dr("U_Password_vc").ToString
                        End If
                    End Using
                End Using
            End If

            Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_UserList WHERE U_UserName_vc=@User AND U_Password_vc=@Pwd", VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@User", txtUserName.Text.Trim)
                cmd.Parameters.AddWithValue("@Pwd", txtPassword.Text.Trim)
                If CInt(cmd.ExecuteScalar()) = 0 Then
                    MsgBox("Invalid User Name or Password")
                    txtPassword.Clear()
                    txtPassword.Focus()
                    Exit Sub
                End If
            End Using

            'If CDate(GetValue("SELECT GETDATE()")) > CDate(GetValue("SELECT Updated_Date_d FROM tbl_CompanyProfile")) Then
            '    MsgBox("License Expired.")
            '    Exit Sub
            'Else
            '    VRenewalchk.RenewalUpdate()
            'End If

            'If My.Computer.Network.IsAvailable Then
            'Else
            '    MsgBox("check your internet connection")
            '    Exit Sub
            'End If

            'lblStatus.Show()
            'lblStatus.Text = "Checking licence..."
            'Me.Cursor = Cursors.WaitCursor
            'Dim licenceValid As Boolean = VLicenseChecker.CheckLicense()
            'Me.Cursor = Cursors.Default

            'If Not licenceValid Then
            '    lblStatus.Text = "Login blocked. Please contact your Admin."
            '    Return
            'End If

            CheckCompanyProfileColumns()

            VistawinFunctions.CmpName = GetValue("SELECT Cmp_Name_vc FROM tbl_CompanyProfile").ToString()
            VistawinFunctions.ProjectTypechk = GetValue("SELECT cmp_ProjectType_vc FROM tbl_CompanyProfile").ToString()

            VistawinFunctions.UserName = txtUserName.Text

            Dim FrmTitle As String = "Version Date : " & VistawinFunctions.VWVerDate & "    User : " & VistawinFunctions.UserName & " Cmp Name:  " & VistawinFunctions.CmpName

            FrmMDICommon.Text = FrmTitle
            FrmMDICommon.UserNameChk = VistawinFunctions.UserName
            FrmMDICommon.Show()

            txtPassword.Text = String.Empty
            txtUserName.Text = String.Empty
            txtUserName.Focus()
            Me.Hide()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtUserName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUserName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLogIn.PerformClick()
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub LblWebsite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblWebsite.Click
        Dim url As String = "https://vistawin.online/"
        Process.Start(url)
    End Sub
   
    Private Sub GetSystemDBlist()
        Try
            Dim con As New SqlConnection("Server=" & CbSqlServerName.Text & ";Database=master;Integrated Security=True")
            con.Open()

            Dim cmd As New SqlCommand("SELECT name FROM sys.databases " & "WHERE name NOT IN " & "('master','model','msdb','tempdb','CLASSICMARKDB') " & "ORDER BY name", con)
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            CbDBSel.Items.Clear()
            While dr.Read()
                CbDBSel.Items.Add(dr("name").ToString())
            End While

            dr.Close()
            con.Close()

            If CbDBSel.Items.Count > 0 Then
                CbDBSel.SelectedIndex = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub CheckCompanyProfileColumns()
        Try
            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If

            Dim sql As String = ""

            sql = "IF COL_LENGTH('dbo.tbl_Companyprofile', 'cmp_ServerApi_vc') IS NULL ALTER TABLE dbo.tbl_Companyprofile ADD cmp_ServerApi_vc VARCHAR(999) NULL;"
            Using Cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
                Cmd.ExecuteNonQuery()
            End Using

            sql = "IF COL_LENGTH('dbo.tbl_Companyprofile', 'cmp_ProjectType_vc') IS NULL ALTER TABLE dbo.tbl_Companyprofile ADD cmp_ProjectType_vc VARCHAR(20) NULL;"
            Using Cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
                Cmd.ExecuteNonQuery()
            End Using

            sql = "IF COL_LENGTH('dbo.tbl_MenuMaster', 'Menu_Show_vc') IS NULL ALTER TABLE dbo.tbl_MenuMaster ADD Menu_Show_vc VARCHAR(20) NULL;"
            Using Cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
                Cmd.ExecuteNonQuery()
            End Using

            sql = "UPDATE dbo.tbl_MenuMaster SET Menu_Show_vc = 'YES' WHERE Menu_Show_vc IS NULL;"
            Using Cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
                Cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MessageBox.Show("Company Profile column update error." & vbCrLf & vbCrLf & ex.Message, "Database Update", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Function GetDBConfigPath() As String

        Dim Drives() As String = {"C:\", "D:\", "E:\", "F:\", "G:\"}
        For Each Drive As String In Drives
            Try
                Dim FileName As String = IO.Path.Combine(Drive, "VISTAWIN\DBConfig.txt")
                If IO.File.Exists(FileName) Then
                    Return FileName
                End If
            Catch
            End Try
        Next
        Return ""

    End Function
    Private Sub LoadDatabaseList()
        Try
            Dim FileName As String = GetDBConfigPath()

            If FileName = "" Then
                MsgBox("DBConfig.txt not found." & vbCrLf & "Checked C:\ to G:\ VISTAWIN folder.", MsgBoxStyle.Exclamation, "Database")
                Exit Sub
            End If

            DBList.Clear()
            CbDatabase.Items.Clear()

            Dim Lines() As String = IO.File.ReadAllLines(FileName)
            Dim Item As DBItem = Nothing

            For Each RawLine As String In Lines
                Dim Line As String = RawLine.Trim()

                If Line = "" Then
                    If Item IsNot Nothing Then
                        AddDBItem(Item)
                        Item = Nothing
                    End If
                    Continue For
                End If

                Dim P As Integer = Line.IndexOf("="c)
                If P <= 0 Then Continue For

                Dim Key As String = Line.Substring(0, P).Trim().ToUpper()
                Dim Value As String = Line.Substring(P + 1).Trim()

                Select Case Key
                    Case "NAME"
                        If Item IsNot Nothing Then
                            AddDBItem(Item)
                        End If
                        Item = New DBItem
                        Item.Name = Value

                    Case "SERVER"
                        If Item Is Nothing Then
                            Item = New DBItem
                        End If
                        Item.Server = Value

                    Case "DATABASE"
                        If Item Is Nothing Then
                            Item = New DBItem
                        End If
                        Item.Database = Value
                End Select
            Next

            If Item IsNot Nothing Then
                AddDBItem(Item)
            End If

            If CbDatabase.Items.Count > 0 Then
                CbDatabase.SelectedIndex = 0
            Else
                MsgBox("No valid database found in DBConfig.txt.", MsgBoxStyle.Exclamation, "Database")
            End If

        Catch ex As Exception
            MsgBox("DBConfig Load Error" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Database")
        End Try

    End Sub
    Private Sub AddDBItem(ByVal Item As DBItem)

        If Item Is Nothing Then Exit Sub

        If String.IsNullOrEmpty(Item.Name) Then Exit Sub
        If String.IsNullOrEmpty(Item.Server) Then Exit Sub
        If String.IsNullOrEmpty(Item.Database) Then Exit Sub

        DBList.Add(Item)
        CbDatabase.Items.Add(Item)

    End Sub
    Private Sub LoginImgCheck()
        Try
            If PictureBox1.BackgroundImage IsNot Nothing Then
                PictureBox1.BackgroundImage.Dispose()
                PictureBox1.BackgroundImage = Nothing
            End If

            Dim Paths() As String = {
                "C:\VISTAWIN\loginimg.jpg",
                "D:\VISTAWIN\loginimg.jpg",
                "E:\VISTAWIN\loginimg.jpg",
                "F:\VISTAWIN\loginimg.jpg",
                "G:\VISTAWIN\loginimg.jpg"
            }
            For Each filePath As String In Paths
                If IO.File.Exists(filePath) Then
                    Using fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)
                        Using img As Image = Image.FromStream(fs)
                            PictureBox1.BackgroundImage = New Bitmap(img)
                        End Using
                    End Using
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub
    Private Sub CreateDBConfigIfNotExists()
        Try
            Dim FileName As String = ConfigFileName()
            If Not IO.File.Exists(FileName) Then

                Dim Lines() As String = {
                    "NAME=",
                     "SERVER=",
                    "DATABASE="
                }
                IO.File.WriteAllLines(FileName, Lines)
            End If
        Catch ex As Exception
            MessageBox.Show("DBConfig create error." & vbCrLf & ex.Message)
        End Try

    End Sub
    Private Function ConfigFileName() As String

        Dim Drives() As String = {"C:\", "D:\", "E:\", "F:\", "G:\"}

        For Each Drive As String In Drives
            Try
                If IO.Directory.Exists(Drive) Then
                    Dim Folder As String = IO.Path.Combine(Drive, "VISTAWIN")
                    If IO.Directory.Exists(Folder) Then
                        Return IO.Path.Combine(Folder, "DBConfig.txt")
                    End If
                End If
            Catch
            End Try
        Next

        For Each Drive As String In Drives
            Try
                If IO.Directory.Exists(Drive) Then
                    Dim Folder As String = IO.Path.Combine(Drive, "VISTAWIN")
                    If Not IO.Directory.Exists(Folder) Then
                        IO.Directory.CreateDirectory(Folder)
                    End If
                    Return IO.Path.Combine(Folder, "DBConfig.txt")
                End If
            Catch
            End Try
        Next
        Throw New Exception("C:, D:, E:, F:,G: drive.")

    End Function

    Private Function GetValue(ByVal Sql As String) As Object
        Using cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
            Return cmd.ExecuteScalar()
        End Using
    End Function
End Class
