Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports
Imports System.ComponentModel
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime

Imports System.Drawing
Imports System.Drawing.Imaging.ImageFormat
Imports System.Drawing.Drawing2D
Imports System.Drawing.Graphics

Imports System.IO.Ports
Imports System.Management
Imports System.Threading
Imports System.Windows.Forms
Public Class FrmDBChange
    Dim Ds As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat As Boolean
    Dim FromBool, ToBool, AddOrEdit As Boolean
    Dim SendSMS As Boolean = False
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Ds1, Dataset As New WinDataSet
    Dim fpath As String
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                Me.Close()
            ElseIf msg.WParam.ToInt32 = CInt(Keys.F11) Then
                'Me.StockAdd()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub FrmDBChange_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.DBSysList()

        GrpAdd.Hide()

        DgvDBList.ColumnHeadersDefaultCellStyle.BackColor = Color.Green
        DgvDBList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DgvDBList.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))

    End Sub
    Private Sub btnRestore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRestore.Click

        Try
            Dim SelectBool As Boolean = False
            Dim SerName1 As String = ""
            Dim DBName1 As String = ""
            For Each Dr As DataGridViewRow In DgvDBList.Rows
                If Dr.Cells("Tick1").Value = "YES" Then
                    SelectBool = True
                    SerName1 = Dr.Cells("SQLName1").Value
                    DBName1 = Dr.Cells("DBName1").Value
                End If
            Next
            If SelectBool = False Then
                MsgBox("Select any one ")
                Exit Sub
            End If

            If txtPath.Text = String.Empty Then
                MsgBox("Select Path Name")
                txtPath.Focus()
                Exit Sub
            End If

            Dim MsgStr As DialogResult = MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo)
            If MsgStr = 6 Then
                If BVistawinSolution1.Permission() = False Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            btnRestore.Enabled = False

            ProgressBar1.Visible = True
            ProgressBar1.Value = 20
            VistawinFunctions.ServerConnection.Close()

            VistawinFunctions.ServerConnection = New SqlConnection("Data Source='" & SerName1 & "';DataBase=master;User ID=sa;Password=YOUR_SA_PASSWORD")
            VistawinFunctions.ServerConnection.Open()

            Command = New SqlCommand("alter database  " + DBName1 + " set single_user with rollback immediate", VistawinFunctions.ServerConnection)
            Command.ExecuteNonQuery()
            ProgressBar1.Value = 50
            Using cmd As New SqlCommand("dbrestore", VistawinFunctions.ServerConnection)
                cmd.CommandType = CommandType.StoredProcedure

                Dim dbnm As New SqlParameter("@dbnm", SqlDbType.VarChar)
                dbnm.Value = DBName1
                cmd.Parameters.Add(dbnm)

                Dim dbnm1 As New SqlParameter("@pathname", SqlDbType.VarChar)
                dbnm1.Value = txtPath.Text
                cmd.Parameters.Add(dbnm1)

                cmd.CommandTimeout = 900
                cmd.ExecuteNonQuery()

            End Using
            ProgressBar1.Value = 70
            Command = New SqlCommand("alter database  " + DBName1 + " set  multi_user", VistawinFunctions.ServerConnection)
            Command.ExecuteNonQuery()

            VistawinFunctions.ServerConnection = New SqlConnection("Data Source='" & SerName1 & "';DataBase='" & DBName1 & "';User ID=sa;Password=YOUR_SA_PASSWORD")
            ProgressBar1.Value = 100

            If ProgressBar1.Value = 100 Then
                MsgBox("Successfully Restored!")
                'ProgressBar1.Visible = False
            End If
            MsgBox("Once Close the Application and Again Open!")

            Select Case VistawinFunctions.CmpDatabaseName
                Case "HUSSAINLODGETVLDB", "MAMPLAYSCHOOLDB", "VSEFSHOPTSIDB", "BALAJICOMPLEXDBNEW", "JMTRAVELDB", "JMMAHALDATA", "FINANCETSIDB"
                    FrmMDICommon.Close()

                Case Else
                    'FrmMDIMain.Close()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub btnbrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbrowse.Click
        Try
            Dim obj As New OpenFileDialog
            obj.ShowDialog()
            fpath = obj.FileName
            txtPath.Text = fpath
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnView2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnView2.Click
        Me.DBSysList()
    End Sub
    Private Sub DBSysList()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT DB_RecordNumber_i AS 'DBRecord1',DB_SystemName_vc AS 'SystemName1', DB_SQLName_vc AS 'SQLName1', DB_DatabaseName_vc AS 'DBName1',DB_Remark_vc AS 'DBRemark1', DB_Show_vc AS 'DBShow1',DB_ColorName_vc AS 'ColorName1'  FROM tbl_DBRestoreSetting WHERE DB_Show_vc='YES' ORDER BY DB_RecordNumber_i", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        RowIndex = 0
        DgvDBList.Rows.Clear()
        For Each Dr As DataRow In Ds.Tables("FillData").Rows
            DgvDBList.Rows.Add()
            DgvDBList.Rows(RowIndex).Cells("SNo1").Value = RowIndex + 1
            DgvDBList.Rows(RowIndex).Cells("DBRecord1").Value = Dr.Item("DBRecord1")
            DgvDBList.Rows(RowIndex).Cells("SystemName1").Value = Dr.Item("SystemName1")
            DgvDBList.Rows(RowIndex).Cells("SQLName1").Value = Dr.Item("SQLName1")
            DgvDBList.Rows(RowIndex).Cells("DBName1").Value = Dr.Item("DBName1")
            DgvDBList.Rows(RowIndex).Cells("DBRemark1").Value = Dr.Item("DBRemark1")
            DgvDBList.Rows(RowIndex).Cells("DBShow1").Value = Dr.Item("DBShow1")
            DgvDBList.Rows(RowIndex).Cells("ColorName1").Value = Dr.Item("ColorName1")
            DgvDBList.Rows(RowIndex).Cells("Tick1").Value = "NO"
            RowIndex += 1
        Next
    End Sub
    Private Sub DgvDBList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvDBList.CellClick
        If e.ColumnIndex = 1 Then
            If e.RowIndex >= 0 Then
                If DgvDBList.Rows(e.RowIndex).Cells("Tick1").Value = "YES" Then
                    For Each Dr As DataGridViewRow In DgvDBList.Rows
                        Dr.Cells("Tick1").Value = "NO"
                    Next
                    DgvDBList.Rows(e.RowIndex).Cells("Tick1").Value = "NO"
                ElseIf DgvDBList.Rows(e.RowIndex).Cells("Tick1").Value = "NO" Then
                    For Each Dr As DataGridViewRow In DgvDBList.Rows
                        Dr.Cells("Tick1").Value = "NO"
                    Next
                    DgvDBList.Rows(e.RowIndex).Cells("Tick1").Value = "YES"
                End If
            End If
        End If
    End Sub

    Private Sub BtnDBDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDBDel.Click
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Dim SelectBool As Boolean = False
            Dim RecdNo1 As Integer = 0
            For Each Dr As DataGridViewRow In DgvDBList.Rows
                If Dr.Cells("Tick1").Value = "YES" Then
                    SelectBool = True
                    RecdNo1 = Dr.Cells("DBRecord1").Value
                End If
            Next
            If SelectBool = False Then
                MsgBox("Select any one ")
                Exit Sub
            End If

            Dim MsgStr As DialogResult = MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo)
            If MsgStr = 6 Then
                If BVistawinSolution1.Permission() = False Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            Cmd = New SqlCommand("DELETE FROM tbl_DBRestoreSetting  WHERE DB_RecordNumber_i=" & RecdNo1 & "", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Deleted")
                Me.DBSysList()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try

    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        GrpAdd.Show()
        GrpAdd.BringToFront()
        txtSysName.Focus()
    End Sub

    Private Sub BtnGrpClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGrpClose.Click
        GrpAdd.Hide()
        txtSysName.Text = String.Empty
        txtSqlName.Text = String.Empty
        txtDBName.Text = String.Empty
        txtDBRemark.Text = String.Empty
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If txtSysName.Text = String.Empty Then
                MsgBox("Enter System Name")
                txtSysName.Focus()
                Exit Sub
            ElseIf txtSqlName.Text = String.Empty Then
                MsgBox("Enter SQLServer Name")
                txtSqlName.Focus()
                Exit Sub
            ElseIf txtDBName.Text = String.Empty Then
                MsgBox("Enter Database Name")
                txtDBName.Focus()
                Exit Sub
            ElseIf txtDBRemark.Text = String.Empty Then
                MsgBox("Enter Remark")
                txtDBRemark.Focus()
                Exit Sub
            End If

            Command = New SqlCommand("INSERT INTO tbl_DBRestoreSetting(DB_SystemName_vc, DB_SQLName_vc, DB_DatabaseName_vc, DB_Remark_vc, DB_Show_vc) VALUES ('" & txtSysName.Text & "','" & txtSqlName.Text & "','" & txtDBName.Text & "','" & txtDBRemark.Text & "','YES')", VistawinFunctions.ServerConnection, Trans)
            Execute = Command.ExecuteNonQuery()

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Saved")
                txtSysName.Text = String.Empty
                txtSqlName.Text = String.Empty
                txtDBName.Text = String.Empty
                txtDBRemark.Text = String.Empty
                Me.DBSysList()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
        GrpAdd.Hide()
    End Sub

    Private Sub txtSysName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSysName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtSqlName.Focus()
        End If
    End Sub

    Private Sub txtSqlName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSqlName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtDBName.Focus()
        End If
    End Sub

    Private Sub txtDBName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDBName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtDBRemark.Focus()
        End If
    End Sub

    Private Sub txtDBRemark_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDBRemark.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnSave.PerformClick()
        End If
    End Sub
    Private Sub BtnChangeDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChangeDB.Click
        Dim SelectBool As Boolean = False
        Dim SerName1 As String = ""
        Dim DBName1 As String = ""
        Dim Color1 As String = ""
        For Each Dr As DataGridViewRow In DgvDBList.Rows
            If Dr.Cells("Tick1").Value = "YES" Then
                SelectBool = True
                SerName1 = Dr.Cells("SQLName1").Value
                DBName1 = Dr.Cells("DBName1").Value
                Color1 = Dr.Cells("ColorName1").Value
            End If
        Next
        If SelectBool = False Then
            MsgBox("Select any one ")
            Exit Sub
        End If

        Dim MsgStr As DialogResult = MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo)
        If MsgStr = 6 Then
            If BVistawinSolution1.Permission() = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        Me.DatabaseBackup()

        VistawinFunctions.ServerConnection = New SqlConnection("Data Source='" & SerName1 & "';DataBase='" & DBName1 & "';User ID=sa;Password=YOUR_SA_PASSWORD")
        VistawinFunctions.ServerConnection.Open()
        ''FrmMDIMain.ForeColor = Color.FromName(Color1)
        Me.DBChangeForm2()
        Me.Close()
    End Sub
    Private Sub DatabaseBackup()
        Dim dbname As String = VistawinFunctions.ServerConnection.Database
        Dim dtt As Object = Format(Date.Today, "ddMMyyyy")
        Dim pathname As String = VistawinFunctions.BackupPath + "\" + dbname + dtt

        Try
            Using cmd As New SqlCommand("[dbo].[dbbackup]", VistawinFunctions.ServerConnection)
                cmd.CommandType = CommandType.StoredProcedure

                Dim dbnm As New SqlParameter("@dbnm", SqlDbType.VarChar)
                dbnm.Value = dbname
                cmd.Parameters.Add(dbnm)

                Dim dbnm1 As New SqlParameter("@pathname", SqlDbType.VarChar)
                dbnm1.Value = pathname
                cmd.Parameters.Add(dbnm1)

                Dim dbse As New SqlParameter("@dbsname", SqlDbType.VarChar)
                dbse.Value = dbname + dtt + "-Full Backup"
                cmd.Parameters.Add(dbse)
                cmd.CommandTimeout = 900
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub DBChangeForm2()

        Me.LoginCmpDetail()

        Dim SqlDBNameChk1 As String = VistawinFunctions.CmpDatabaseName


        Dim FrmTitle As String = "Version : " & VistawinFunctions.VWVerDate & "    User : " & VistawinFunctions.UserName & "    Company : " & VistawinFunctions.CmpName
        Select Case SqlDBNameChk1
            Case "HUSSAINLODGETVLDB", "MAMPLAYSCHOOLDB", "VSEFSHOPTSIDB", "JMTRAVELDB",
                "JMMAHALDATA", "MAMSITEDB", "FINANCETSIDB", "BALAJICOMPLEXDBNEW"
                FrmMDICommon.Text = FrmTitle
                FrmMDICommon.UserNameChk = VistawinFunctions.UserName
                FrmMDICommon.Show()

            Case Else
                'FrmMDIMain.Text = FrmTitle
                'FrmMDIMain.Show()
        End Select

    End Sub
    Private Sub LoginCmpDetail()

        Dim dt As New DataTable
        Using da As New SqlDataAdapter("SELECT TOP 1 * FROM tbl_Companyprofile", VistawinFunctions.ServerConnection)
            da.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then Exit Sub

        Dim dr As DataRow = dt.Rows(0)

        VistawinFunctions.TodayDate = Date.Now
        VistawinFunctions.CmpName = If(IsDBNull(dr("Cmp_Name_vc")), "", dr("Cmp_Name_vc").ToString())
        VistawinFunctions.CmpAddress = If(IsDBNull(dr("Cmp_Address_vc")), "", dr("Cmp_Address_vc").ToString())
        VistawinFunctions.CmpCaption = If(IsDBNull(dr("Cmp_Caption_vc")), "", dr("Cmp_Caption_vc").ToString())
        VistawinFunctions.CmpDistrict = If(IsDBNull(dr("Cmp_District_vc")), "", dr("Cmp_District_vc").ToString())
        VistawinFunctions.CmpState = If(IsDBNull(dr("Cmp_State_vc")), "", dr("Cmp_State_vc").ToString())
        VistawinFunctions.CmpMobileNo = If(IsDBNull(dr("Cmp_MobileNo_n")), "", dr("Cmp_MobileNo_n").ToString())
        VistawinFunctions.CmpEmailID = If(IsDBNull(dr("Cmp_Email_vc")), "", dr("Cmp_Email_vc").ToString())
        VistawinFunctions.CmpPhoneNo = If(IsDBNull(dr("Cmp_PhoneNo_vc")), "", dr("Cmp_PhoneNo_vc").ToString())
        VistawinFunctions.CmpProprietor = If(IsDBNull(dr("Cmp_Proprietor_vc")), "", dr("Cmp_Proprietor_vc").ToString())
        VistawinFunctions.CmpLicenseNo = If(IsDBNull(dr("Cmp_LicenseNo_vc")), "", dr("Cmp_LicenseNo_vc").ToString())
        VistawinFunctions.MastPassword = If(IsDBNull(dr("Mast_Password_vc")), "", dr("Mast_Password_vc").ToString())
        VistawinFunctions.BackupPath = If(IsDBNull(dr("BackupPath_vc")), "", dr("BackupPath_vc").ToString())
        VistawinFunctions.ScanningPath = If(IsDBNull(dr("ScanningPath_vc")), "", dr("ScanningPath_vc").ToString())
        VistawinFunctions.CmpDatabaseName = If(IsDBNull(dr("Cmp_DataBaseName_vc")), "", dr("Cmp_DataBaseName_vc").ToString())
        VistawinFunctions.CmpBankDetails = If(IsDBNull(dr("Cmp_BankDetails_vc")), "", dr("Cmp_BankDetails_vc").ToString())
        VistawinFunctions.CmpMovingWord = If(IsDBNull(dr("Moving_Word_vc")), "", dr("Moving_Word_vc").ToString())
        VistawinFunctions.CmpReportHead = If(IsDBNull(dr("Cmp_ReportHead_vc")), "", dr("Cmp_ReportHead_vc").ToString())
        VistawinFunctions.GDrivePath = If(IsDBNull(dr("Cmp_GDrivePath_vc")), "", dr("Cmp_GDrivePath_vc").ToString())
        VistawinFunctions.UpiName = If(IsDBNull(dr("Cmp_UpiName_vc")), "", dr("Cmp_UpiName_vc").ToString())
        VistawinFunctions.UpiID = If(IsDBNull(dr("Cmp_UpiID_vc")), "", dr("Cmp_UpiID_vc").ToString())
        VistawinFunctions.CmpToEmail = If(IsDBNull(dr("Cmp_ToMail_vc")), "", dr("Cmp_ToMail_vc").ToString())
        VistawinFunctions.PrintingType = If(IsDBNull(dr("BillingType_vc")), "DEFAULT", dr("BillingType_vc").ToString())
        VistawinFunctions.SaveImage = If(IsDBNull(dr("Cmp_SaveImage_vc")), "NO", dr("Cmp_SaveImage_vc").ToString())
        VistawinFunctions.CmpBZS = If(IsDBNull(dr("Bill_ZeroStock_vc")), "NO", dr("Bill_ZeroStock_vc").ToString())
        VistawinFunctions.CmpBAfP = If(IsDBNull(dr("Bill_AftPrint_vc")), "NO", dr("Bill_AftPrint_vc").ToString())
        VistawinFunctions.GStCal = If(IsDBNull(dr("Cmp_GSTCal_vc")), "INCLUDING", dr("Cmp_GSTCal_vc").ToString())
        VistawinFunctions.CmpExpDays = If(IsDBNull(dr("Cmp_ExpDays_i")), 0, Convert.ToInt32(dr("Cmp_ExpDays_i")))

        If Not IsDBNull(dr("Cmp_Logo_im")) Then
            VistawinFunctions.CmpLogo = DirectCast(dr("Cmp_Logo_im"), Byte())
        End If

        If Not IsDBNull(dr("Cmp_BackImage_im")) Then
            Dim imgBytes() As Byte = DirectCast(dr("Cmp_BackImage_im"), Byte())
            Using ms As New MemoryStream(imgBytes)
                Me.BackgroundImage = Image.FromStream(ms)
            End Using
            VistawinFunctions.CmpBackImage = imgBytes
        Else
            Me.BackgroundImage = Nothing
        End If

    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
End Class