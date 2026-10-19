Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmChangePW
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet

    Private Sub FrmChangePW_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtUserName.Text = VistawinFunctions.UserName
        txtOldPW.Focus()
    End Sub
    
    Private Sub CbOldShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbOldShow.CheckedChanged

        If CbOldShow.Checked = True Then
            txtOldPW.UseSystemPasswordChar = False
        ElseIf CbOldShow.Checked = False Then
            txtOldPW.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub CbNewShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbNewShow.CheckedChanged
        If CbNewShow.Checked = True Then
            txtNewPW.UseSystemPasswordChar = False
        ElseIf CbNewShow.Checked = False Then
            txtNewPW.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub BtnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChange.Click

        Me.UserPwChange()
    End Sub
    
    Private Sub UserPwChange() '
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If txtOldPW.Text = String.Empty Then
                MsgBox("Enter Old Password")
                txtOldPW.Focus()
                Exit Sub
            ElseIf txtNewPW.Text = String.Empty Then
                MsgBox("Enter New Password")
                txtNewPW.Focus()
                Exit Sub
            ElseIf txtNewPW.Text.Trim().Length() > 10 Then
                MsgBox("Enter Maximum 10 Digit Password")
                txtNewPW.Text = String.Empty
                txtNewPW.Focus()
                Exit Sub
            End If

            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_UserList WHERE U_Password_vc='" & txtOldPW.Text.Trim & "' AND U_UserName_vc='" & txtUserName.Text.Trim & "'", VistawinFunctions.ServerConnection, Trans)
            If Cmd.ExecuteScalar = 0 Then
                MsgBox("Incorrect the Password")
                txtOldPW.Text = String.Empty
                txtOldPW.Focus()
                Exit Sub
            Else
            End If

            Cmd = New SqlCommand("UPDATE tbl_UserList SET U_Password_vc='" & txtNewPW.Text.Trim & "' WHERE U_UserName_vc='" & txtUserName.Text.Trim & "'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Changed")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try

    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
    Private Sub txtOldPW_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOldPW.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNewPW.Focus()
        End If
    End Sub
    Private Sub txtOldPW_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOldPW.KeyPress

        ' ------A to Z ,Numbers,Backsapace,Delete Only-------------
        If Not Char.IsLetter(e.KeyChar) And Not IsNumeric(e.KeyChar) And Not e.KeyChar = ChrW(Keys.Back) And Not e.KeyChar = ChrW(Keys.Delete) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtNewPW_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNewPW.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnChange.PerformClick()
        End If
    End Sub
    Private Sub txtNewPW_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNewPW.KeyPress

        ' ------A to Z ,Numbers,Backsapace,Delete Only-------------
        If Not Char.IsLetter(e.KeyChar) And Not IsNumeric(e.KeyChar) And Not e.KeyChar = ChrW(Keys.Back) And Not e.KeyChar = ChrW(Keys.Delete) Then
            e.Handled = True
        End If
    End Sub
  
End Class