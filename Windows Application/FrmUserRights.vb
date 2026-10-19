Imports System.IO
Imports System.Data.SqlClient

Public Class FrmUserRights
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet
    Private Sub FrmUserRights_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    
        DgvEmpList.Hide()
        DgvEmpList.Size = New System.Drawing.Size(400, 300)
        DgvEmpList.BringToFront()

        GrpUser.Hide()
        GrpUser.BringToFront()
        GrpUser.Location = New System.Drawing.Point(28, 21)
        GrpUser.Size = New System.Drawing.Size(700, 500)
    End Sub
    Private Sub BtnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview.Click
      
        LoadRoleRightsMatrix()
    End Sub
   
    Private Sub LoadRoleRightsMatrix()

        Dim dtMenu As New DataTable
        Dim dtEmp As New DataTable
        Dim dtRights As New DataTable
        Dim dt As New DataTable

        Using da As New SqlDataAdapter("SELECT MenuID,MenuName FROM tbl_MenuMaster ORDER BY MenuID", VistawinFunctions.ServerConnection)
            da.Fill(dtMenu)
        End Using

        Using da As New SqlDataAdapter("SELECT U_EmpID_i,C_Name_vc FROM tbl_UserList INNER JOIN tbl_CusProfile ON U_EmpID_i=C_ID_n WHERE C_Type_vc='EMPLOYEE' ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
            da.Fill(dtEmp)
        End Using

        Using da As New SqlDataAdapter("SELECT URM_EmpID_i,URM_MenuID_i FROM tbl_UserRoleMenu", VistawinFunctions.ServerConnection)
            da.Fill(dtRights)
        End Using

        'First column
        dt.Columns.Add("Menu Name")

        For Each emp As DataRow In dtEmp.Rows
            dt.Columns.Add(emp("C_Name_vc").ToString, GetType(Boolean))
        Next

        'Rows = Menus
        For Each m As DataRow In dtMenu.Rows
            Dim r As DataRow = dt.NewRow()
            r("Menu Name") = m("MenuName").ToString

            For Each emp As DataRow In dtEmp.Rows

                Dim found() As DataRow = dtRights.Select("URM_EmpID_i=" & emp("U_EmpID_i") & _
                    " AND URM_MenuID_i=" & m("MenuID"))

                If found.Length > 0 Then
                    r(emp("C_Name_vc").ToString) = True
                Else
                    r(emp("C_Name_vc").ToString) = False
                End If
            Next
            dt.Rows.Add(r)
        Next

        DgvMenu.DataSource = dt

        DgvMenu.Columns(0).Width = 200
        DgvMenu.Columns("Menu Name").ReadOnly = True

        For i As Integer = 1 To DgvMenu.Columns.Count - 1
            DgvMenu.Columns(i).Width = 100
        Next

    End Sub
    Private Sub DgvMenu_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DgvMenu.CellFormatting

        If e.RowIndex < 0 OrElse e.ColumnIndex = 0 Then Exit Sub

        If TypeOf DgvMenu.Columns(e.ColumnIndex) Is DataGridViewCheckBoxColumn Then

            If e.Value IsNot Nothing AndAlso CBool(e.Value) Then
                DgvMenu.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Green
            Else
                DgvMenu.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.White
            End If

        End If

    End Sub
    Private Sub DgvMenu_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvMenu.CellValueChanged

        If e.RowIndex < 0 OrElse e.ColumnIndex = 0 Then Exit Sub

        If CBool(DgvMenu.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
            DgvMenu.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Green
        Else
            DgvMenu.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.White
        End If

    End Sub

    Private Sub DgvMenu_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvMenu.CurrentCellDirtyStateChanged

        If DgvMenu.IsCurrentCellDirty Then
            DgvMenu.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        Me.SaveRoleRights()

    End Sub
    Private Sub SaveRoleRights()

        Try
            Dim dtEmp As New DataTable
            Using da As New SqlDataAdapter("SELECT U_EmpID_i,C_Name_vc FROM tbl_UserList INNER JOIN tbl_CusProfile ON U_EmpID_i=C_ID_n WHERE C_Type_vc='EMPLOYEE' ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
                da.Fill(dtEmp)
            End Using

            For Each dgvr As DataGridViewRow In DgvMenu.Rows

                If dgvr.IsNewRow Then Continue For

                Dim cmdMenu As New SqlCommand("SELECT MenuID FROM tbl_MenuMaster WHERE MenuName=@MenuName", VistawinFunctions.ServerConnection)
                cmdMenu.Parameters.AddWithValue("@MenuName", dgvr.Cells("Menu Name").Value.ToString())
                Dim MenuID As Integer = CInt(cmdMenu.ExecuteScalar())

                For i As Integer = 1 To DgvMenu.Columns.Count - 1

                    Dim EmpID As Integer = CInt(dtEmp.Rows(i - 1)("U_EmpID_i"))
                    Dim Allow As Boolean = CBool(dgvr.Cells(i).Value)

                    If Allow Then
                        Dim cmdSave As New SqlCommand("IF NOT EXISTS(SELECT * FROM tbl_UserRoleMenu WHERE URM_EmpID_i=@EmpID AND URM_MenuID_i=@MenuID) " & _
                            "INSERT INTO tbl_UserRoleMenu(URM_EmpID_i,URM_MenuID_i) VALUES(@EmpID,@MenuID)", VistawinFunctions.ServerConnection)

                        cmdSave.Parameters.AddWithValue("@EmpID", EmpID)
                        cmdSave.Parameters.AddWithValue("@MenuID", MenuID)
                        cmdSave.ExecuteNonQuery()
                    Else
                        Dim cmdDelete As New SqlCommand("DELETE FROM tbl_UserRoleMenu WHERE URM_EmpID_i=@EmpID AND URM_MenuID_i=@MenuID", VistawinFunctions.ServerConnection)
                        cmdDelete.Parameters.AddWithValue("@EmpID", EmpID)
                        cmdDelete.Parameters.AddWithValue("@MenuID", MenuID)
                        cmdDelete.ExecuteNonQuery()
                    End If
                Next

            Next
            MessageBox.Show("Saved Successfully")
            LoadRoleRightsMatrix()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
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
                'Me.UserList()
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

    Private Sub BtnGrpClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGrpClose.Click
        Me.Clear()
        GrpUser.Hide()
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
    Private Sub DgvEmpList_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvEmpList.CellClick
        If e.RowIndex >= 0 Then EmpDetail()
    End Sub

    Private Sub DgvEmpList_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DgvEmpList.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            EmpDetail()
        End If

    End Sub

    Private Sub txtEmpName_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtEmpName.KeyDown

        If e.KeyCode = Keys.Down AndAlso DgvEmpList.Rows.Count > 0 Then
            DgvEmpList.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            EmpDetail()
        End If

    End Sub
    Private Sub EmpDetail()

        If DgvEmpList.CurrentRow Is Nothing Then Exit Sub

        txtEmpID.Text = DgvEmpList.CurrentRow.Cells("ID").Value.ToString()
        txtEmpName.Text = DgvEmpList.CurrentRow.Cells("Name").Value.ToString()

        DgvEmpList.Hide()

        txtUserName.Focus()

    End Sub
    Private Sub txtEmpName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmpName.TextChanged

        txtEmpID.Clear()

        If txtEmpName.Text.Trim = "" Then
            DgvEmpList.Hide()
            Exit Sub
        End If

        Dim dt As New DataTable
        Adapter = New SqlDataAdapter("SELECT C_Name_vc AS Name,C_ID_n AS ID  FROM tbl_CusProfile WHERE C_Type_vc='EMPLOYEE' AND C_Show_vc='YES' AND NOT EXISTS (SELECT U_EmpID_i FROM tbl_UserList WHERE C_ID_n=U_EmpID_i) AND (C_Name_vc LIKE @Search OR C_Mobile_vc LIKE @Search OR CONVERT(VARCHAR,C_ID_n) LIKE @Search) ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
        Adapter.SelectCommand.Parameters.AddWithValue("@Search", txtEmpName.Text.Trim & "%")
        Adapter.Fill(dt)

        DgvEmpList.DataSource = dt

        If dt.Rows.Count > 0 Then
            DgvEmpList.Columns(0).Width = 300
            DgvEmpList.Columns(1).Width = 60
            DgvEmpList.Show()
            DgvEmpList.CurrentCell = DgvEmpList.Rows(0).Cells(0)
        Else
            DgvEmpList.Hide()
        End If

    End Sub
End Class