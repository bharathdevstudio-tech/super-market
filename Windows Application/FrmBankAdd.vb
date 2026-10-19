Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmBankAdd
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Adapter As New SqlDataAdapter
    Dim Dataset, Dataset1 As New WinDataSet
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Cmd1, command, Cmd2 As New SqlCommand

    Dim Str As String
    Dim Row, RowIndex, iColumnindex, irowindex As Integer
    Dim DateFormat, Execute As Boolean
    Dim FromBool, ToBool, DateBo3, DateBo4, DateBo5 As Boolean
    Dim Opbal As Decimal = 0
    Dim Expenditure1, Income1 As Decimal
    Dim REdit As Boolean = False

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                Me.Close()
            ElseIf msg.WParam.ToInt32 = CInt(Keys.F4) Then
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub FrmBankAddNav_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CbBalType.SelectedIndex = 0
        Me.BankDetails()

    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            If txtLedgerName.Text = String.Empty Then
                MsgBox("Enter Ledger Name")
                txtLedgerName.Focus()
                Exit Sub
            End If
            If txtLedCode.Text = String.Empty Then
                Cmd = New SqlCommand("SELECT ISNULL(MAX(Ba_BankID_i),0) +1 FROM tbl_BankList", VistawinFunctions.ServerConnection, Trans)
                txtLedCode.Text = Cmd.ExecuteScalar
            End If

            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_BankList WHERE Ba_BankName_vc=N'" & txtLedgerName.Text.Trim & "'", VistawinFunctions.ServerConnection, Trans)
            If Cmd.ExecuteScalar > 0 Then
                MsgBox("This Name Already Added")
                txtLedgerName.Focus()
                Exit Sub
            End If

            Cmd = New SqlCommand("SELECT GETDATE()", VistawinFunctions.ServerConnection, Trans)
            VistawinFunctions.TodayDate = Cmd.ExecuteScalar

            Using Cmd1 As New SqlCommand("SP_tbl_BankList", VistawinFunctions.ServerConnection, Trans)
                Cmd1.CommandType = CommandType.StoredProcedure

                Dim Ba_EDate_d As New SqlParameter("@Ba_EDate_d", SqlDbType.DateTime)
                Ba_EDate_d.Value = VistawinFunctions.TodayDate
                Cmd1.Parameters.Add(Ba_EDate_d)

                Dim Ba_RecordNo_i As New SqlParameter("@Ba_RecordNo_i", SqlDbType.Int)
                Ba_RecordNo_i.Value = 0
                Cmd1.Parameters.Add(Ba_RecordNo_i)

                Dim Ba_BankName_vc As New SqlParameter("@Ba_BankName_vc", SqlDbType.NVarChar)
                If txtLedgerName.Text = String.Empty Then
                    Ba_BankName_vc.Value = DBNull.Value
                Else
                    Ba_BankName_vc.Value = txtLedgerName.Text
                End If
                Cmd1.Parameters.Add(Ba_BankName_vc)

                Dim Ba_BankID_i As New SqlParameter("@Ba_BankID_i", SqlDbType.Int)
                Ba_BankID_i.Value = txtLedCode.Text
                Cmd1.Parameters.Add(Ba_BankID_i)

                Dim Ba_OpBal_n As New SqlParameter("@Ba_OpBal_n", SqlDbType.Decimal)
                If txtOpBal.Text = String.Empty Then
                    Ba_OpBal_n.Value = 0
                Else
                    Ba_OpBal_n.Value = txtOpBal.Text
                End If
                Cmd1.Parameters.Add(Ba_OpBal_n)

                Dim Ba_OpBalType_vc As New SqlParameter("@Ba_OpBalType_vc", SqlDbType.VarChar)
                If CbBalType.Text = String.Empty Then
                    Ba_OpBalType_vc.Value = "Debit"
                Else
                    Ba_OpBalType_vc.Value = CbBalType.Text
                End If
                Cmd1.Parameters.Add(Ba_OpBalType_vc)

                Dim Ba_UID_vc As New SqlParameter("@Ba_UID_vc", SqlDbType.VarChar)
                Ba_UID_vc.Value = VistawinFunctions.UserName
                Cmd1.Parameters.Add(Ba_UID_vc)

                Dim Ba_UpDate_d As New SqlParameter("@Ba_UpDate_d", SqlDbType.DateTime)
                Ba_UpDate_d.Value = VistawinFunctions.TodayDate
                Cmd1.Parameters.Add(Ba_UpDate_d)

                Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                InputString.Value = "Add"
                Cmd1.Parameters.Add(InputString)
                Execute = Cmd1.ExecuteNonQuery
            End Using

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Saved")
            End If
            Me.BankDetails()
            Me.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub Clear()
        txtLedCode.Text = String.Empty
        txtLedgerName.Text = String.Empty
        txtOpBal.Text = String.Empty
        txtLedgerName.Focus()
    End Sub
    Private Sub BankDetails()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT Ba_BankID_i AS 'LedCode',Ba_BankName_vc AS 'LedName',ISNULL(Ba_OpBal_n,0)AS 'OpBalL',Ba_OpBalType_vc AS 'OpBalType' FROM tbl_BankList a ORDER BY Ba_BankName_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        If Ds.Tables("FillData").Rows.Count > 0 Then
            RowIndex = 0
            DgvLegderList.Rows.Clear()
            For Each Dr As DataRow In Ds.Tables("FillData").Rows
                DgvLegderList.Rows.Add()
                DgvLegderList.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
                DgvLegderList.Rows(RowIndex).Cells("LedCode").Value = Dr.Item("LedCode")
                DgvLegderList.Rows(RowIndex).Cells("LedName").Value = Dr.Item("LedName")
                DgvLegderList.Rows(RowIndex).Cells("OpBalL").Value = Dr.Item("OpBalL")
                DgvLegderList.Rows(RowIndex).Cells("OpBalType").Value = Dr.Item("OpBalType")
                DgvLegderList.Rows(RowIndex).Cells("Tick").Value = "NO"
                RowIndex += 1
            Next
        Else
            'MsgBox("No Data")
            DgvLegderList.Rows.Clear()
        End If
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Dim SelectBool As Boolean = False
            For Each Dr As DataGridViewRow In DgvLegderList.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    SelectBool = True
                End If
            Next
            If SelectBool = False Then
                MsgBox("Select Any one")
                Exit Sub
            End If

            Dim MastID As String = ""
            For Each Dr As DataGridViewRow In DgvLegderList.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    If MastID = "" Then
                        MastID = "" & Dr.Cells("LedCode").Value & "" + ","
                    Else
                        MsgBox("Select Only One")
                        DgvLegderList.Focus()
                        Exit Sub
                    End If
                End If
            Next

            For Each Dr As DataGridViewRow In DgvLegderList.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_RecDtl WHERE R_CusID_n=" & Dr.Cells("LedCode").Value & "", VistawinFunctions.ServerConnection, Trans)
                    If Cmd.ExecuteScalar > 0 Then
                        MsgBox("Cannot be Deleted !")
                        Exit Sub
                    End If
                End If
            Next
            Dim MsgStr As DialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo)
            If MsgStr = 6 Then
                'If BVistawinSolution1.Permission() = False Then
                '    Exit Sub
                'End If
            Else
                Exit Sub
            End If
            For Each Dr As DataGridViewRow In DgvLegderList.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    Cmd = New SqlCommand("DELETE tbl_BankList WHERE Ba_BankID_i=" & Dr.Cells("LedCode").Value & "", VistawinFunctions.ServerConnection, Trans)
                    Execute = Cmd.ExecuteNonQuery
                End If
            Next
            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Deleted")
                Me.BankDetails()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub

    Private Sub txtLedgerName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLedgerName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtOpBal.Focus()
        End If
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Dim SelectBool As Boolean = False
            For Each Dr As DataGridViewRow In DgvLegderList.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    SelectBool = True
                End If
            Next
            If SelectBool = False Then
                MsgBox("Select any one ")
                Exit Sub
            End If

            For Each Dr As DataGridViewRow In DgvLegderList.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    Cmd = New SqlCommand("UPDATE tbl_BankList SET Ba_BankName_vc=N'" & Dr.Cells("LedName").Value & "',Ba_OpBal_n='" & Dr.Cells("OpBalL").Value & "',Ba_OpBalType_vc='" & Dr.Cells("OpBalType").Value & "' WHERE Ba_BankID_i=" & Dr.Cells("LedCode").Value & "", VistawinFunctions.ServerConnection, Trans)
                    Execute = Cmd.ExecuteNonQuery()
                End If
            Next
            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Saved")
                Me.BankDetails()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
        Me.BankDetails()
    End Sub
    Private Sub DgvLegderList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvLegderList.CellClick

        If e.ColumnIndex = 1 Then
            If e.RowIndex >= 0 Then
                If DgvLegderList.Rows(e.RowIndex).Cells("Tick").Value = "YES" Then
                    For Each Dr As DataGridViewRow In DgvLegderList.Rows
                        Dr.Cells("Tick").Value = "NO"
                    Next
                    DgvLegderList.Rows(e.RowIndex).Cells("Tick").Value = "NO"
                ElseIf DgvLegderList.Rows(e.RowIndex).Cells("Tick").Value = "NO" Then
                    For Each Dr As DataGridViewRow In DgvLegderList.Rows
                        Dr.Cells("Tick").Value = "NO"
                    Next
                    DgvLegderList.Rows(e.RowIndex).Cells("Tick").Value = "YES"
                End If
            End If
        End If


    End Sub
    Private Sub txtOpBal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOpBal.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnSave.PerformClick()
        End If
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
End Class