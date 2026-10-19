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

Imports System.IO.Ports
Imports System.Management
Imports System.Threading
Imports System.Windows.Forms
Public Class FrmUserRoles

    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet

    Dim UserName1 As String = ""
    Dim c, c1 As ToolStripItem
    Dim t As ToolStripMenuItem
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                GrpUser.Hide()
                Return True
                Exit Function
            ElseIf msg.WParam.ToInt32 = CInt(Keys.F1) Then
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub FrmUserRoles_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DgvEmpList.Hide()
        DgvEmpList.Size = New System.Drawing.Size(400, 300)
        DgvEmpList.BringToFront()

        GrpUser.Hide()
        GrpUser.BringToFront()
        GrpUser.Location = New System.Drawing.Point(28, 21)
        GrpUser.Size = New System.Drawing.Size(700, 500)

        Me.UserList()

        Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_UserPermission", VistawinFunctions.ServerConnection)
        Cmd.ExecuteScalar()

        If Cmd.ExecuteScalar() = 0 Then
            BtnMenuInsert.Enabled = True
        Else
            BtnMenuInsert.Enabled = False
        End If
    End Sub
    
    Private Sub UserList()

        DGVUserName.Rows.Clear()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT U_EmpName_vc AS 'EmpName',U_UserName_vc AS 'UserName' FROM tbl_UserList ORDER BY U_UserName_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "UserName")
        Row = 0
        If Ds.Tables("UserName").Rows.Count > 0 Then
            For Each dr As DataRow In Ds.Tables("UserName").Rows
                DGVUserName.Rows.Add()
                DGVUserName.Rows(Row).Cells("EmpName").Value = dr.Item("EmpName")
                DGVUserName.Rows(Row).Cells("UserName").Value = dr.Item("UserName")
                DGVUserName.Rows(Row).Cells("Tick").Value = "NO"
                Row += 1
            Next
        End If
    End Sub
    Private Sub BtnMenuInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMenuInsert.Click
        Me.MenuList()
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction
            For Each dr As DataGridViewRow In DgvMainMenu.Rows
                Cmd = New SqlCommand("INSERT INTO tbl_UserPermission (Ent_Date_d, Menu_Name_vc, Display_Name_vc) VALUES(GETDATE(),'" & dr.Cells("MenuName").Value & "','" & dr.Cells("DisplayName").Value & "')", VistawinFunctions.ServerConnection, Trans)
                Execute = Cmd.ExecuteNonQuery()
            Next
            If Execute = True Then
                Trans.Commit()
                MsgBox("Insert Successfully")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try

    End Sub

    Private Sub BtnMenuCheck_Click(sender As System.Object, e As System.EventArgs) Handles BtnMenuCheck.Click

        DgvMainMenu.Rows.Clear()
        Me.MenuList()

        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            For Each dr As DataGridViewRow In DgvMainMenu.Rows
                Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_UserPermission WHERE Menu_Name_vc='" & dr.Cells("MenuName").Value.ToString & "' AND Display_Name_vc='" & dr.Cells("DisplayName").Value & "'", VistawinFunctions.ServerConnection, Trans)
                If Cmd.ExecuteScalar = 0 Then
                    Cmd = New SqlCommand("INSERT INTO tbl_UserPermission (Ent_Date_d, Menu_Name_vc, Display_Name_vc) VALUES(GETDATE(),'" & dr.Cells("MenuName").Value & "','" & dr.Cells("DisplayName").Value & "')", VistawinFunctions.ServerConnection, Trans)
                    Execute = Cmd.ExecuteNonQuery
                Else
                End If
            Next

            If Execute = True Then
                Trans.Commit()
                MsgBox("Updated Successfully")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub DGVUserName_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVUserName.CellClick
        If e.ColumnIndex = 0 Then
            If e.RowIndex >= 0 Then
                If DGVUserName.Rows(e.RowIndex).Cells("Tick").Value = "YES" Then
                    For Each Dr As DataGridViewRow In DGVUserName.Rows
                        Dr.Cells("Tick").Value = "NO"
                    Next
                    DGVUserName.Rows(e.RowIndex).Cells("Tick").Value = "NO"
                    DgvMainMenu.Rows.Clear()
                ElseIf DGVUserName.Rows(e.RowIndex).Cells("Tick").Value = "NO" Then
                    For Each Dr As DataGridViewRow In DGVUserName.Rows
                        Dr.Cells("Tick").Value = "NO"
                    Next
                    DGVUserName.Rows(e.RowIndex).Cells("Tick").Value = "YES"
                    Me.UserMainMenuRoles()
                End If
            End If
        End If
    End Sub
 
    Private Sub UserMainMenuRoles()

        For Each Dr As DataGridViewRow In DGVUserName.Rows
            If Dr.Cells("Tick").Value = "YES" Then
                UserName1 = Dr.Cells("UserName").Value
            End If
        Next

        Cmd = New SqlCommand("SELECT COUNT(*) FROM sys.tables a LEFt JOIN sys.columns b ON(a.object_id=b.object_id) WHERE a.name='tbl_UserPermission' AND b.name= '" & UserName1 & "_vc" & "'", VistawinFunctions.ServerConnection, Trans)
        Cmd.ExecuteScalar()

        If Cmd.ExecuteScalar() = 0 Then
            Me.MenuList()
        Else
            Ds = New DataSet
            Adapter = New SqlDataAdapter("SELECT Menu_Name_vc AS 'MenuName',Display_Name_vc AS 'DisplayName',Menu_RecordNo_i AS 'RecordNoM',ISNULL(" & UserName1 & "_vc,'NO') AS 'TickM' FROM tbl_UserPermission WHERE " & UserName1 & "_vc IN ('NO','YES') OR " & UserName1 & "_vc IS NULL", VistawinFunctions.ServerConnection)
            Adapter.Fill(Ds, "FillData")
            Row = 0

            DgvMainMenu.Rows.Clear()
            If Ds.Tables("FillData").Rows.Count > 0 Then
                For Each dr As DataRow In Ds.Tables("FillData").Rows
                    DgvMainMenu.Rows.Add()
                    DgvMainMenu.Rows(Row).Cells("MenuName").Value = dr.Item("MenuName")
                    DgvMainMenu.Rows(Row).Cells("DisplayName").Value = dr.Item("DisplayName")
                    DgvMainMenu.Rows(Row).Cells("RecordNoM").Value = dr.Item("RecordNoM")
                    DgvMainMenu.Rows(Row).Cells("TickM").Value = dr.Item("TickM")
                    Row += 1
                Next
            End If
        End If
    End Sub
    Private Sub MenuList()

        Row = 0
        
    End Sub
    Private Sub SetAllMenuItems(ByRef menus As ToolStripItemCollection)
        Dim c As ToolStripItem
        Dim t As ToolStripMenuItem
        For Each c In menus
            If c.GetType Is GetType(ToolStripMenuItem) Then
                t = c
                If t.Name.ToString <> String.Empty Then
                    DgvMainMenu.Rows.Add()
                    DgvMainMenu.Rows(Row).Cells("MenuName").Value = t.Name
                    DgvMainMenu.Rows(Row).Cells("DisplayName").Value = t.Text
                    DgvMainMenu.Rows(Row).Cells("TickM").Value = "NO"
                    Row += 1
                End If
                SetAllMenuItems(t.DropDownItems)
            End If
        Next
    End Sub
    Private Sub CbMenuSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbMenuSelectAll.CheckedChanged

        If CbMenuSelectAll.Checked = True Then
            For Each Dr As DataGridViewRow In DgvMainMenu.Rows
                Dr.Cells("TickM").Value = "YES"
            Next
        ElseIf CbMenuSelectAll.Checked = False Then
            For Each Dr As DataGridViewRow In DgvMainMenu.Rows
                Dr.Cells("TickM").Value = "NO"
            Next
        End If
    End Sub


    Private Sub BtnMenuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMenuSave.Click
        Me.UserPermission()
    End Sub
    Private Sub UserPermission()
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Dim RowCheck As Boolean = False
            For Each Dr As DataGridViewRow In DGVUserName.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    RowCheck = True
                    UserName1 = Dr.Cells("UserName").Value
                End If
            Next
            If RowCheck = False Then
                MsgBox("Select Any User")
                DGVUserName.Focus()
                Exit Sub
            End If

            For Each dr As DataGridViewRow In DgvMainMenu.Rows
                Cmd = New SqlCommand("UPDATE tbl_UserPermission SET " & UserName1 & "_vc ='" & dr.Cells("TickM").Value & "' WHERE Menu_RecordNo_i= '" & dr.Cells("RecordNoM").Value & "'", VistawinFunctions.ServerConnection, Trans)
                Execute = Cmd.ExecuteNonQuery()
            Next
            If Execute = True Then
                Trans.Commit()
                MsgBox("Updated Successfully")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
        DgvMainMenu.Rows.Clear()
    End Sub

    Private Sub BtnUserAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserAdd.Click
        GrpUser.Show()
        GrpUser.BringToFront()
        txtEmpName.Focus()
        Me.UserLoginList()
    End Sub
    Private Sub UserLoginList()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT U_EmpName_vc AS 'EmpNameU',U_EmpID_i AS 'EmpIDU',U_UserName_vc AS 'UserNameU',U_Password_vc AS 'PasswordU' FROM tbl_UserList ORDER BY U_EmpName_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        RowIndex = 0
        DgvUserList.Rows.Clear()
        For Each Dr As DataRow In Ds.Tables("FillData").Rows
            DgvUserList.Rows.Add()
            DgvUserList.Rows(RowIndex).Cells("SNoU").Value = RowIndex + 1
            DgvUserList.Rows(RowIndex).Cells("EmpNameU").Value = Dr.Item("EmpNameU")
            DgvUserList.Rows(RowIndex).Cells("EmpIDU").Value = Dr.Item("EmpIDU")
            DgvUserList.Rows(RowIndex).Cells("UserNameU").Value = Dr.Item("UserNameU")
            'DgvUserList.Rows(RowIndex).Cells("PasswordU").Value = Dr.Item("PasswordU")
            DgvUserList.Rows(RowIndex).Cells("PasswordU").Value = "****"
            DgvUserList.Rows(RowIndex).Cells("TickU2").Value = "NO"
            DgvUserList.Rows(RowIndex).Cells("PwShow").Value = "NO"
            RowIndex += 1
        Next
    End Sub
    Private Sub txtEmpName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyDown
        If e.KeyCode = Keys.Down Then
            DgvEmpList.Focus()
            DgvEmpList.Show()
            DgvEmpList.BringToFront()
        ElseIf e.KeyCode = Keys.Enter Then
            Me.EmpDetail()
        End If
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpName.TextChanged

        txtEmpID.Text = String.Empty
        Dim s As String
        Dim ds As DataSet
        If txtEmpName.Text = Nothing OrElse _
            String.IsNullOrEmpty(txtEmpName.Text) Then
            DgvEmpList.Visible = False
        Else
            ds = New DataSet
            Adapter = New SqlDataAdapter("SELECT  C_Name_vc AS 'Name',C_ID_n AS 'ID' FROM tbl_CusProfile WHERE C_Type_vc='EMPLOYEE' AND C_Show_vc='YES' AND NOT EXISTS(SELECT U_EmpID_i FROM tbl_UserList WHERE C_ID_n=U_EmpID_i) AND (C_Mobile_vc like '" & txtEmpName.Text + "%" & "' OR C_Name_vc LIKE N'" & txtEmpName.Text + "%" & "' OR C_ID_n LIKE N'" & txtEmpName.Text + "%" & "') ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
            Adapter.Fill(ds, "ShipToDetail1")
            DgvEmpList.DataSource = ds.Tables("ShipToDetail1")
            If ds.Tables("ShipToDetail1").Rows.Count > 0 Then
                DgvEmpList.Show()
                DgvEmpList.BringToFront()

                DgvEmpList.Columns(0).Width = 300
                DgvEmpList.Columns(1).Width = 60

                Dim searchstring As String
                searchstring = Trim(txtEmpName.Text)
                Dim startchar As Char = searchstring.Chars(0)
                Dim Strlength As Integer
                Strlength = Trim(txtEmpName.Text).Length
                If Asc(startchar) >= 65 And Asc(startchar) <= 90 OrElse _
                           Asc(startchar) >= 97 And Asc(startchar) <= 122 Then
                    For Each dr As DataGridViewRow In DgvEmpList.Rows
                        If dr.Index <> DgvEmpList.NewRowIndex Then
                            s = dr.Cells(1).Value.ToString()
                            Dim ss As String = Mid(s, 1, Strlength)
                            If ss = UCase(searchstring) OrElse _
                                    ss = searchstring Then
                                DgvEmpList.MultiSelect = False
                                dr.Cells(1).Selected = True
                                DgvEmpList.CurrentCell = dr.Cells(1)
                                Exit For
                            End If
                        End If
                    Next

                Else
                    For Each dr As DataGridViewRow In DgvEmpList.Rows
                        If dr.Index <> DgvEmpList.NewRowIndex Then
                            s = dr.Cells(0).Value.ToString()
                            Dim ss As String = Mid(s, 1, Strlength)
                            If ss = searchstring Then
                                DgvEmpList.MultiSelect = False
                                dr.Cells(0).Selected = True
                                DgvEmpList.CurrentCell = dr.Cells(0)
                                Exit For
                            End If
                        End If
                    Next

                End If
            Else
                DgvEmpList.Hide()
                txtEmpID.Text = String.Empty
                txtEmpName.Text = String.Empty
            End If
        End If

    End Sub

    Private Sub DgvEmpList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvEmpList.CellClick
        Me.EmpDetail()
    End Sub
    Private Sub DgvEmpList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DgvEmpList.KeyDown
        If e.KeyCode = Keys.Down Then
            DgvEmpList.Focus()
            DgvEmpList.Show()
            DgvEmpList.BringToFront()
        ElseIf e.KeyCode = Keys.Enter Then
            Me.EmpDetail()
        End If
    End Sub
    Private Sub EmpDetail()
        Dim dr As DataRow
        Dim dr2 As DataGridViewSelectedRowCollection
        dr2 = DgvEmpList.SelectedRows
        For Each dr3 As DataGridViewRow In dr2
            dr = TryCast(dr3.DataBoundItem, DataRowView).Row

            RemoveHandler txtEmpName.TextChanged, AddressOf txtEmpName_TextChanged
            txtEmpID.Text = dr.Item(1)
            txtEmpName.Text = dr.Item(0)
            AddHandler txtEmpName.TextChanged, AddressOf txtEmpName_TextChanged
            DgvEmpList.Hide()
            If txtEmpName.Text = String.Empty Then
                DgvEmpList.Hide()
            Else
                If txtEmpID.Text <> String.Empty Then
                    DgvEmpList.Hide()

                    Ds = New DataSet
                    Adapter = New SqlDataAdapter("SELECT * FROM tbl_CusProfile WHERE C_Type_vc='EMPLOYEE' AND C_Show_vc='YES' AND C_ID_n=" & txtEmpID.Text.Trim & "", VistawinFunctions.ServerConnection)
                    Adapter.Fill(Ds, "FillData")
                    If Ds.Tables("FillData").Rows.Count > 0 Then

                        If IsDBNull(Ds.Tables("FillData").Rows(0).Item("C_ID_n")) Then
                            txtEmpID.Text = ""
                        Else
                            txtEmpID.Text = Ds.Tables("FillData").Rows(0).Item("C_ID_n")
                        End If

                        If IsDBNull(Ds.Tables("FillData").Rows(0).Item("C_Name_vc")) Then
                            txtEmpName.Text = ""
                        Else
                            txtEmpName.Text = Ds.Tables("FillData").Rows(0).Item("C_Name_vc")
                        End If
                    End If
                End If
            End If
        Next
        txtUserName.Focus()
    End Sub

    Private Sub BtnGrpClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGrpClose.Click
        Me.Clear()
        GrpUser.Hide()
        Me.UserList()
    End Sub
    Private Sub BtnUserSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserSave.Click
        Me.UserAdd()
    End Sub
    Private Sub UserAdd()

        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If txtEmpName.Text = String.Empty Then
                MsgBox("Select Employee")
                txtEmpName.Focus()
                Exit Sub
            ElseIf txtUserName.Text = String.Empty Then
                MsgBox("Enter User Name")
                txtUserName.Focus()
                Exit Sub
            ElseIf txtPassword.Text = String.Empty Then
                MsgBox("Enter Password")
                txtPassword.Focus()
                Exit Sub
            ElseIf txtUserName.Text.Trim().Length() > 10 Then
                MsgBox("Enter Maximum 10 Digit UserName")
                txtUserName.Text = String.Empty
                txtUserName.Focus()
                Exit Sub
            ElseIf txtPassword.Text.Trim().Length() > 10 Then
                MsgBox("Enter Maximum 10 Digit Password")
                txtPassword.Text = String.Empty
                txtPassword.Focus()
                Exit Sub
            End If

            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_UserList WHERE U_EmpID_i='" & txtEmpID.Text & "'", VistawinFunctions.ServerConnection, Trans)
            If Cmd.ExecuteScalar > 0 Then
                MsgBox("Employee is Already Added")
                txtEmpName.Focus()
                Exit Sub
            End If

            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_UserList WHERE U_UserName_vc='" & txtUserName.Text & "'", VistawinFunctions.ServerConnection, Trans)
            If Cmd.ExecuteScalar > 0 Then
                MsgBox("This User Name is Already Added")
                txtEmpName.Focus()
                Exit Sub
            End If

            Using cmd As New SqlCommand("[dbo].[SP_tbl_UserAdd]", VistawinFunctions.ServerConnection, Trans)
                cmd.CommandType = CommandType.StoredProcedure

                Dim U_EmpName_vc As New SqlParameter("@U_EmpName_vc", SqlDbType.NVarChar)
                U_EmpName_vc.Value = txtEmpName.Text.Trim
                cmd.Parameters.Add(U_EmpName_vc)

                Dim U_EmpID_i As New SqlParameter("@U_EmpID_i", SqlDbType.Int)
                U_EmpID_i.Value = txtEmpID.Text
                cmd.Parameters.Add(U_EmpID_i)

                Dim U_UserName_vc As New SqlParameter("@U_UserName_vc", SqlDbType.NVarChar)
                'U_UserName_vc.Value = txtUserName.Text.Trim + "_vc"
                U_UserName_vc.Value = txtUserName.Text.Trim
                cmd.Parameters.Add(U_UserName_vc)

                Dim User_vc As New SqlParameter("@User_vc", SqlDbType.VarChar)
                User_vc.Value = txtUserName.Text.Trim + "_vc"
                cmd.Parameters.Add(User_vc)

                Dim U_Password_vc As New SqlParameter("@U_Password_vc", SqlDbType.VarChar)
                U_Password_vc.Value = txtPassword.Text.Trim
                cmd.Parameters.Add(U_Password_vc)

                Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                InputString.Value = "Add"
                cmd.Parameters.Add(InputString)
                Execute = cmd.ExecuteNonQuery
            End Using

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Added")
                Me.UpdatePermission()
                Me.Clear()
                Me.UserLoginList()
                Me.UserList()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub UpdatePermission()
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Cmd = New SqlCommand("UPDATE tbl_UserPermission SET " & txtUserName.Text.Trim & "_vc='YES'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            If Execute = True Then
                Trans.Commit()
                'MsgBox("Successfully Added")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Me.Clear()
    End Sub
    Private Sub Clear()

        txtEmpName.Text = String.Empty
        txtEmpID.Text = String.Empty
        txtUserName.Text = String.Empty
        txtPassword.Text = String.Empty

        txtEmpName.ReadOnly = False
        txtUserName.ReadOnly = False
        CbNewShow.Checked = False

        txtEmpName.Focus()
        'GrpUser.Hide()
    End Sub

    Private Sub txtUserName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUserName.KeyDown

        ' ------This is Also Work-------------

        'Select Case e.KeyValue
        '    Case Keys.A To Keys.Z
        '        ''Case Keys.Space
        '    Case Keys.Left, Keys.Right, Keys.Back
        '    Case Keys.NumPad0 To Keys.NumPad9
        'Case Keys.D0 To Keys.D9
        '    Case Else : e.SuppressKeyPress = True
        'End Select

        If e.KeyCode = Keys.Enter Then
            txtPassword.Focus()
        End If

    End Sub
    Private Sub txtUserName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserName.KeyPress

        ' ------A to Z ,Backsapace,Delete Only-------------
        If Not Char.IsLetter(e.KeyChar) And Not e.KeyChar = ChrW(Keys.Back) And Not e.KeyChar = ChrW(Keys.Delete) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnUserSave.PerformClick()
        End If
    End Sub
    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress

        ' ------A to Z ,Numbers,Backsapace,Delete Only-------------
        If Not Char.IsLetter(e.KeyChar) And Not IsNumeric(e.KeyChar) And Not e.KeyChar = ChrW(Keys.Back) And Not e.KeyChar = ChrW(Keys.Delete) Then
            e.Handled = True
        End If
    End Sub
    Private Sub BtnUserEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserEdit.Click

        Me.UserEdit()

    End Sub
    Private Sub UserEdit()
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If txtEmpName.Text = String.Empty Then
                MsgBox("Select Employee")
                txtEmpName.Focus()
                Exit Sub
            ElseIf txtEmpID.Text = String.Empty Then
                txtEmpID.Focus()
                Exit Sub
            ElseIf txtUserName.Text = String.Empty Then
                MsgBox("Enter User Name")
                txtUserName.Focus()
                Exit Sub
            ElseIf txtPassword.Text = String.Empty Then
                MsgBox("Enter Password")
                txtPassword.Focus()
                Exit Sub
            ElseIf txtUserName.Text.Trim().Length() > 10 Then
                MsgBox("Enter Maximum 10 Digit UserName")
                txtUserName.Text = String.Empty
                txtUserName.Focus()
                Exit Sub
            ElseIf txtPassword.Text.Trim().Length() > 10 Then
                MsgBox("Enter Maximum 10 Digit Password")
                'txtPassword.Text = String.Empty
                txtPassword.Focus()
                Exit Sub
            End If

            Cmd = New SqlCommand("UPDATE tbl_UserList SET U_Password_vc='" & txtPassword.Text.Trim & "' WHERE U_EmpID_i='" & txtEmpID.Text & "'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Edited")
                Me.UserLoginList()
                Me.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub BtnUserDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserDel.Click
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If txtEmpName.Text = String.Empty Then
                MsgBox("Select Employee")
                txtEmpName.Focus()
                Exit Sub
            ElseIf txtUserName.Text = String.Empty Then
                MsgBox("Enter User Name")
                txtUserName.Focus()
                Exit Sub
            ElseIf txtPassword.Text = String.Empty Then
                MsgBox("Enter Password")
                txtPassword.Focus()
                Exit Sub
            End If

            Dim MsgStr As DialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo)
            If MsgStr = 6 Then
                If BVistawinSolution1.Permission() = False Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            Using cmd As New SqlCommand("[dbo].[SP_tbl_UserDelete]", VistawinFunctions.ServerConnection, Trans)
                cmd.CommandType = CommandType.StoredProcedure

                Dim U_EmpID_i As New SqlParameter("@U_EmpID_i", SqlDbType.Int)
                U_EmpID_i.Value = txtEmpID.Text
                cmd.Parameters.Add(U_EmpID_i)

                Dim U_UserName_vc As New SqlParameter("@U_UserName_vc", SqlDbType.NVarChar)
                'U_UserName_vc.Value = txtUserName.Text.Trim + "_vc"
                U_UserName_vc.Value = txtUserName.Text.Trim
                cmd.Parameters.Add(U_UserName_vc)

                Dim User_vc As New SqlParameter("@User_vc", SqlDbType.VarChar)
                User_vc.Value = txtUserName.Text.Trim + "_vc"
                cmd.Parameters.Add(User_vc)

                Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                InputString.Value = "Delete"
                cmd.Parameters.Add(InputString)
                Execute = cmd.ExecuteNonQuery
            End Using

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Deleted")
                Me.UserLoginList()
                Me.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    
    Private Sub DgvUserList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DgvUserList.CellMouseDoubleClick

        Dim iColumn As Integer = DgvUserList.CurrentCell.ColumnIndex
        Dim iRow As Integer = DgvUserList.CurrentCell.RowIndex

        If iColumn = 3 Then
            txtEmpID.Text = DgvUserList.Rows(e.RowIndex).Cells("EmpIDU").Value
            Me.UserEditDtl()
        Else
            Exit Sub
        End If

    End Sub
    Private Sub UserEditDtl()

        txtEmpName.ReadOnly = True
        txtUserName.ReadOnly = True

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT U_EmpID_i AS 'EmpID', U_EmpName_vc AS 'EmpName', U_UserName_vc AS 'UserName', U_Password_vc AS 'PWord' FROM tbl_UserList WHERE U_EmpID_i='" & txtEmpID.Text & "'", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        RowIndex = 0
        If Ds.Tables("FillData").Rows.Count > 0 Then
            For Each Dr As DataRow In Ds.Tables("FillData").Rows

                If IsDBNull(Ds.Tables("FillData").Rows(0).Item("EmpID")) Then
                    txtEmpID.Text = ""
                Else
                    txtEmpID.Text = Ds.Tables("FillData").Rows(0).Item("EmpID")
                End If

                RemoveHandler txtEmpName.TextChanged, AddressOf txtEmpName_TextChanged
                If IsDBNull(Ds.Tables("FillData").Rows(0).Item("EmpName")) Then
                    txtEmpName.Text = ""
                Else
                    txtEmpName.Text = Ds.Tables("FillData").Rows(0).Item("EmpName")
                End If
                AddHandler txtEmpName.TextChanged, AddressOf txtEmpName_TextChanged

                If IsDBNull(Ds.Tables("FillData").Rows(0).Item("UserName")) Then
                    txtUserName.Text = ""
                Else
                    txtUserName.Text = Ds.Tables("FillData").Rows(0).Item("UserName")
                End If

                If IsDBNull(Ds.Tables("FillData").Rows(0).Item("Pword")) Then
                    txtPassword.Text = ""
                Else
                    txtPassword.Text = Ds.Tables("FillData").Rows(0).Item("Pword")
                End If
            Next
        End If
    End Sub

    Private Sub DgvUserList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvUserList.CellClick
        If e.ColumnIndex = 6 Then
            If e.RowIndex >= 0 Then
                If DgvUserList.Rows(e.RowIndex).Cells("PwShow").Value = "YES" Then
                    For Each Dr As DataGridViewRow In DgvUserList.Rows
                        Dr.Cells("PwShow").Value = "NO"
                    Next
                    DgvUserList.Rows(e.RowIndex).Cells("PwShow").Value = "NO"
                ElseIf DgvUserList.Rows(e.RowIndex).Cells("PwShow").Value = "NO" Then
                    For Each Dr As DataGridViewRow In DgvUserList.Rows
                        Dr.Cells("PwShow").Value = "NO"
                    Next
                    DgvUserList.Rows(e.RowIndex).Cells("PwShow").Value = "YES"
                End If
                Me.ShowPassword()
            End If
        End If
    End Sub
    Private Sub ShowPassword()

        Dim EmpID1 As Integer
        For Each Dr As DataGridViewRow In DgvUserList.Rows
            If Dr.Cells("PwShow").Value = "YES" Then
                EmpID1 = Dr.Cells("EmpIDU").Value

                Cmd = New SqlCommand("SELECT U_Password_vc FROM tbl_UserList WHERE U_EmpID_i='" & EmpID1 & "'", VistawinFunctions.ServerConnection, Trans)
                Cmd.ExecuteScalar()
                Dr.Cells("PasswordU").Value = Cmd.ExecuteScalar()
            Else
                Dr.Cells("PasswordU").Value = "****"
            End If
        Next
    End Sub


    Private Sub CbNewShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbNewShow.CheckedChanged
        If CbNewShow.Checked = True Then
            txtPassword.UseSystemPasswordChar = False
        ElseIf CbNewShow.Checked = False Then
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

End Class