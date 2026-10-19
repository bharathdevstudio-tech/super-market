Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmAccLedger
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

    Private Sub FrmAccLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CbBalType.SelectedIndex = 0
        Me.LedgerDetails()

    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN", "CLINIC", "COMPLEX"
                Me.InvenSave()
            Case "LODGE"
                Me.LodgeSave()
        End Select
    End Sub
    Private Sub InvenSave()
        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            If txtLedgerName.Text = String.Empty Then
                MsgBox("Enter Ledger Name")
                txtLedgerName.Focus()
                Exit Sub
            End If
            If txtLedCode.Text = String.Empty Then
                Cmd = New SqlCommand("SELECT ISNULL(MAX(L_LedCode_i),0) +1 FROM tbl_AccLedger", VistawinFunctions.ServerConnection, Trans)
                txtLedCode.Text = Cmd.ExecuteScalar
            End If

            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_AccLedger WHERE L_LedName_vc=N'" & txtLedgerName.Text.Trim & "'", VistawinFunctions.ServerConnection, Trans)
            If Cmd.ExecuteScalar > 0 Then
                MsgBox("This Name Already Added")
                txtLedgerName.Focus()
                Exit Sub
            End If

            Cmd = New SqlCommand("SELECT GETDATE()", VistawinFunctions.ServerConnection, Trans)
            VistawinFunctions.TodayDate = Cmd.ExecuteScalar

            Using Cmd1 As New SqlCommand("SP_tbl_AccLedger", VistawinFunctions.ServerConnection, Trans)
                Cmd1.CommandType = CommandType.StoredProcedure

                Dim L_EDate_d As New SqlParameter("@L_EDate_d", SqlDbType.DateTime)
                L_EDate_d.Value = VistawinFunctions.TodayDate
                Cmd1.Parameters.Add(L_EDate_d)

                Dim L_RecordNo_i As New SqlParameter("@L_RecordNo_i", SqlDbType.Int)
                L_RecordNo_i.Value = 0
                Cmd1.Parameters.Add(L_RecordNo_i)

                Dim L_LedName_vc As New SqlParameter("@L_LedName_vc", SqlDbType.NVarChar)
                If txtLedgerName.Text = String.Empty Then
                    L_LedName_vc.Value = DBNull.Value
                Else
                    L_LedName_vc.Value = txtLedgerName.Text
                End If
                Cmd1.Parameters.Add(L_LedName_vc)

                Dim L_LedCode_i As New SqlParameter("@L_LedCode_i", SqlDbType.Int)
                L_LedCode_i.Value = txtLedCode.Text
                Cmd1.Parameters.Add(L_LedCode_i)

                Dim L_OpBal_n As New SqlParameter("@L_OpBal_n", SqlDbType.Decimal)
                If txtOpBal.Text = String.Empty Then
                    L_OpBal_n.Value = 0
                Else
                    L_OpBal_n.Value = txtOpBal.Text
                End If
                Cmd1.Parameters.Add(L_OpBal_n)

                Dim L_OpBalType_vc As New SqlParameter("@L_OpBalType_vc", SqlDbType.VarChar)
                If CbBalType.Text = String.Empty Then
                    L_OpBalType_vc.Value = "Debit"
                Else
                    L_OpBalType_vc.Value = CbBalType.Text
                End If
                Cmd1.Parameters.Add(L_OpBalType_vc)

                Dim Ac_GrpCode_i As New SqlParameter("@Ac_GrpCode_i", SqlDbType.Int)
                Ac_GrpCode_i.Value = 0
                Cmd1.Parameters.Add(Ac_GrpCode_i)

                Dim L_UID_vc As New SqlParameter("@L_UID_vc", SqlDbType.VarChar)
                L_UID_vc.Value = VistawinFunctions.UserName
                Cmd1.Parameters.Add(L_UID_vc)

                Dim L_UpDate_d As New SqlParameter("@L_UpDate_d", SqlDbType.DateTime)
                L_UpDate_d.Value = VistawinFunctions.TodayDate
                Cmd1.Parameters.Add(L_UpDate_d)

                Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                InputString.Value = "Add"
                Cmd1.Parameters.Add(InputString)
                Execute = Cmd1.ExecuteNonQuery
            End Using

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Saved")
            End If
            Me.LedgerDetails()
            Me.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub LodgeSave()

        If Trim(txtLedgerName.Text) = "" Then
            MsgBox("Enter Ledger Name")
            txtLedgerName.Focus()
            Exit Sub
        End If

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try

            If Trim(txtLedCode.Text) = "" Then
                Cmd = New SqlCommand("SELECT ISNULL(MAX(L_LedCode_i),0)+1 FROM tbl_AccLedger", VistawinFunctions.ServerConnection, Trans)
                txtLedCode.Text = Cmd.ExecuteScalar.ToString
            End If

            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_AccLedger WHERE L_LedName_vc=@Name", VistawinFunctions.ServerConnection, Trans)
            Cmd.Parameters.AddWithValue("@Name", txtLedgerName.Text.Trim)

            If Val(Cmd.ExecuteScalar) > 0 Then
                MsgBox("This Name Already Added")
                txtLedgerName.Focus()
                Trans.Rollback()
                Exit Sub
            End If

            Using Cmd1 As New SqlCommand("SP_tbl_Lod_AccLedger", VistawinFunctions.ServerConnection, Trans)
                Cmd1.CommandType = CommandType.StoredProcedure

                Cmd1.Parameters.AddWithValue("@L_EDate_d", VistawinFunctions.TodayDate)
                Cmd1.Parameters.AddWithValue("@L_RecordNo_i", 0)
                Cmd1.Parameters.AddWithValue("@L_LedName_vc", txtLedgerName.Text.Trim)
                Cmd1.Parameters.AddWithValue("@L_LedCode_i", Val(txtLedCode.Text))
                Cmd1.Parameters.AddWithValue("@L_OpBal_n", Val(txtOpBal.Text))
                Cmd1.Parameters.AddWithValue("@L_OpBalType_vc", If(CbBalType.Text = "", "Debit", CbBalType.Text))
                Cmd1.Parameters.AddWithValue("@Ac_GrpCode_i", 0)
                Cmd1.Parameters.AddWithValue("@L_UID_vc", VistawinFunctions.UserName)
                Cmd1.Parameters.AddWithValue("@L_UpDate_d", VistawinFunctions.TodayDate)
                Cmd1.Parameters.AddWithValue("@InputString", "Add")

                If Cmd1.ExecuteNonQuery() > 0 Then
                    Trans.Commit()
                    MsgBox("Successfully Saved")
                Else
                    Trans.Rollback()
                End If
            End Using

            Me.LedgerDetails()
            Me.Clear()

        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MsgBox(ex.Message)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try

    End Sub
    Private Sub Clear()
        txtLedCode.Text = String.Empty
        txtLedgerName.Text = String.Empty
        txtOpBal.Text = String.Empty
        txtLedgerName.Focus()
    End Sub
    Private Sub LedgerDetails()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT L_LedCode_i AS 'LedCode',L_LedName_vc AS 'LedName',ISNULL(L_OpBal_n,0)AS 'OpBalL',L_OpBalType_vc AS 'OpBalType' FROM tbl_AccLedger a ORDER BY L_LedName_vc", VistawinFunctions.ServerConnection)
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

        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN", "CLINIC"
                Me.InvenDelete()
            Case "LODGE"
                Me.LodgeDelete()
            Case "COMPLEX"
                Me.ComplexDelete()
        End Select
     
    End Sub
   
    Private Sub InvenDelete()

        Dim Trans As SqlTransaction = Nothing
        Try
            Dim SelectedRow As DataGridViewRow = Nothing
            Dim SelectCount As Integer = 0

            For Each Dr As DataGridViewRow In DgvLegderList.Rows
                If Dr.IsNewRow Then Continue For
                If Convert.ToString(Dr.Cells("Tick").Value).Trim().ToUpper() = "YES" Then
                    SelectCount += 1
                    SelectedRow = Dr
                End If
            Next

            If SelectCount = 0 Then
                MsgBox("Select Any One")
                DgvLegderList.Focus()
                Exit Sub
            End If

            If SelectCount > 1 Then
                MsgBox("Select Only One")
                DgvLegderList.Focus()
                Exit Sub
            End If

            Dim LedCode As Integer = 0
            If SelectedRow.Cells("LedCode").Value Is Nothing OrElse IsDBNull(SelectedRow.Cells("LedCode").Value) Then
                MsgBox("Invalid Ledger Code")
                Exit Sub
            End If

            If Not Integer.TryParse(Convert.ToString(SelectedRow.Cells("LedCode").Value), LedCode) Then
                MsgBox("Invalid Ledger Code")
                Exit Sub
            End If

            If LedCode = 1 Then
                MsgBox("Cannot be Deleted !", MsgBoxStyle.Exclamation, "Information")
                Exit Sub
            End If

            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If
            Trans = VistawinFunctions.ServerConnection.BeginTransaction()

            Dim UsedIn As String = ""
            Dim SqlCheck As String = "SELECT TOP 1 'Receipt' FROM tbl_RecDtl WHERE R_CusID_n=@LedCode"

            Using Cmd As New SqlCommand(SqlCheck, VistawinFunctions.ServerConnection, Trans)
                Cmd.Parameters.Add("@LedCode", SqlDbType.Int).Value = LedCode
                Dim Obj As Object = Cmd.ExecuteScalar()
                If Obj IsNot Nothing AndAlso Not IsDBNull(Obj) Then
                    UsedIn = Convert.ToString(Obj)
                End If
            End Using

            If UsedIn <> "" Then
                Trans.Rollback()
                Trans.Dispose()
                Trans = Nothing
                MsgBox("Cannot be Deleted !" & vbCrLf & "This Ledger is already used in " & UsedIn & ".", MsgBoxStyle.Exclamation, "Ledger Delete")
                Exit Sub
            End If

            If MessageBox.Show("Are you sure you want to delete this ledger?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Trans.Rollback()
                Trans.Dispose()
                Trans = Nothing
                Exit Sub
            End If

            Dim RowsAffected As Integer = 0

            Using Cmd As New SqlCommand("DELETE FROM tbl_AccLedger WHERE L_LedCode_i=@LedCode", VistawinFunctions.ServerConnection, Trans)
                Cmd.Parameters.Add("@LedCode", SqlDbType.Int).Value = LedCode
                RowsAffected = Cmd.ExecuteNonQuery()
            End Using

            If RowsAffected > 0 Then
                Trans.Commit()
                Trans.Dispose()
                Trans = Nothing
                MsgBox("Successfully Deleted")
                Me.LedgerDetails()
            Else
                Trans.Rollback()
                Trans.Dispose()
                Trans = Nothing
                MsgBox("Ledger not found.")
            End If

        Catch ex As Exception
            If Trans IsNot Nothing Then
                Try
                    Trans.Rollback()
                Catch
                End Try

                Try
                    Trans.Dispose()
                Catch
                End Try
                Trans = Nothing
            End If
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then
                Trans.Dispose()
            End If
        End Try

    End Sub
    Private Sub LodgeDelete()
        Dim LedCode As Integer = 0

        For Each dr As DataGridViewRow In DgvLegderList.Rows
            If dr.Cells("Tick").Value = "YES" Then
                If LedCode <> 0 Then
                    MsgBox("Select Only One")
                    Exit Sub
                End If
                LedCode = Val(dr.Cells("LedCode").Value)
            End If
        Next

        If LedCode = 0 Then
            MsgBox("Select Any One")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Cmd = New SqlCommand("SELECT L_LedCode_i FROM tbl_AccLedger WHERE L_LedCode_i=@Code", VistawinFunctions.ServerConnection, Trans)
            Cmd.Parameters.AddWithValue("@Code", LedCode)
            If Val(Cmd.ExecuteScalar & "") = 1 Then
                MsgBox("Cannot be Deleted !")
                Trans.Rollback()
                Exit Sub
            End If

            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_Lod_Voucher WHERE R_LedCode_n=@Code", VistawinFunctions.ServerConnection, Trans)
            Cmd.Parameters.AddWithValue("@Code", LedCode)
            If Val(Cmd.ExecuteScalar) > 0 Then
                MsgBox("Cannot be Deleted !")
                Trans.Rollback()
                Exit Sub
            End If

            Cmd = New SqlCommand("DELETE FROM tbl_AccLedger WHERE L_LedCode_i=@Code", VistawinFunctions.ServerConnection, Trans)
            Cmd.Parameters.AddWithValue("@Code", LedCode)
            If Cmd.ExecuteNonQuery() > 0 Then
                Trans.Commit()
                MsgBox("Successfully Deleted")
                LedgerDetails()
            Else
                Trans.Rollback()
            End If

        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try

    End Sub
    Private Sub ComplexDelete()
        Dim LedCode As Integer = 0

        For Each dr As DataGridViewRow In DgvLegderList.Rows
            If dr.Cells("Tick").Value = "YES" Then
                If LedCode <> 0 Then
                    MsgBox("Select Only One")
                    Exit Sub
                End If
                LedCode = Val(dr.Cells("LedCode").Value)
            End If
        Next

        If LedCode = 0 Then
            MsgBox("Select Any One")
            Exit Sub
        End If

        If MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Cmd = New SqlCommand("SELECT L_LedCode_i FROM tbl_AccLedger WHERE L_LedCode_i=@Code", VistawinFunctions.ServerConnection, Trans)
            Cmd.Parameters.AddWithValue("@Code", LedCode)
            If Val(Cmd.ExecuteScalar & "") = 1 Then
                MsgBox("Cannot be Deleted !")
                Trans.Rollback()
                Exit Sub
            End If

            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_ReceiptDtl WHERE Rec_LedCode_n=@Code", VistawinFunctions.ServerConnection, Trans)
            Cmd.Parameters.AddWithValue("@Code", LedCode)
            If Val(Cmd.ExecuteScalar) > 0 Then
                MsgBox("Cannot be Deleted !")
                Trans.Rollback()
                Exit Sub
            End If

            Cmd = New SqlCommand("DELETE FROM tbl_AccLedger WHERE L_LedCode_i=@Code", VistawinFunctions.ServerConnection, Trans)
            Cmd.Parameters.AddWithValue("@Code", LedCode)
            If Cmd.ExecuteNonQuery() > 0 Then
                Trans.Commit()
                MsgBox("Successfully Deleted")
                LedgerDetails()
            Else
                Trans.Rollback()
            End If

        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try

    End Sub
    Private Sub txtLedgerName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLedgerName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtOpBal.Focus()
        End If
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.LedgerEdit()
    End Sub
    Private Sub LedgerEdit()

        Dim Trans As SqlTransaction = Nothing
        Try

            Dim SelectedRow As DataGridViewRow = Nothing
            Dim SelectCount As Integer = 0

            For Each Dr As DataGridViewRow In DgvLegderList.Rows
                If Dr.IsNewRow Then Continue For
                If Convert.ToString(Dr.Cells("Tick").Value).Trim().ToUpper() = "YES" Then
                    SelectCount += 1
                    SelectedRow = Dr
                End If
            Next

            If SelectCount = 0 Then
                MsgBox("Select Any One")
                DgvLegderList.Focus()
                Exit Sub
            End If

            If SelectCount > 1 Then
                MsgBox("Select Only One")
                DgvLegderList.Focus()
                Exit Sub
            End If

            Dim LedCode As Integer = 0
            If SelectedRow.Cells("LedCode").Value Is Nothing OrElse IsDBNull(SelectedRow.Cells("LedCode").Value) Then
                MsgBox("Invalid Ledger Code")
                Exit Sub
            End If

            If Not Integer.TryParse(Convert.ToString(SelectedRow.Cells("LedCode").Value), LedCode) Then
                MsgBox("Invalid Ledger Code")
                Exit Sub
            End If

            If LedCode = 1 Then
                MsgBox("Cannot be Edited !", MsgBoxStyle.Exclamation, "Information")
                Exit Sub
            End If

            Dim LedName As String = Convert.ToString(SelectedRow.Cells("LedName").Value).Trim()
            Dim OpBalType As String = Convert.ToString(SelectedRow.Cells("OpBalType").Value).Trim()

            Dim OpBal As Decimal = 0
            Decimal.TryParse(Convert.ToString(SelectedRow.Cells("OpBalL").Value), OpBal)

            If LedName = "" Then
                MsgBox("Enter Ledger Name")
                DgvLegderList.Focus()
                Exit Sub
            End If

            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If
            Trans = VistawinFunctions.ServerConnection.BeginTransaction()

            Dim RowsAffected As Integer = 0

            Dim Sql As String = "UPDATE tbl_AccLedger SET L_LedName_vc=@LedName,L_OpBal_n=@OpBal,L_OpBalType_vc=@OpBalType WHERE L_LedCode_i=@LedCode"
            Using Cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection, Trans)
                Cmd.Parameters.Add("@LedName", SqlDbType.NVarChar, 200).Value = LedName
                Cmd.Parameters.Add("@OpBal", SqlDbType.Decimal).Value = OpBal
                Cmd.Parameters.Add("@OpBalType", SqlDbType.VarChar, 20).Value = OpBalType
                Cmd.Parameters.Add("@LedCode", SqlDbType.Int).Value = LedCode
                RowsAffected = Cmd.ExecuteNonQuery()
            End Using

            If RowsAffected > 0 Then
                Trans.Commit()
                Trans.Dispose()
                Trans = Nothing
                MsgBox("Successfully Saved")
                Me.LedgerDetails()
            Else
                Trans.Rollback()
                Trans.Dispose()
                Trans = Nothing
                MsgBox("Ledger not found.")
            End If

        Catch ex As Exception
            If Trans IsNot Nothing Then
                Try
                    Trans.Rollback()
                Catch
                End Try

                Try
                    Trans.Dispose()
                Catch
                End Try
                Trans = Nothing
            End If

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then
                Trans.Dispose()
            End If
        End Try

    End Sub
    Private Sub DgvLegderList_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvLegderList.CellClick

        If e.RowIndex < 0 OrElse e.ColumnIndex <> 1 Then Exit Sub

        For Each Dr As DataGridViewRow In DgvLegderList.Rows
            Dr.Cells("Tick").Value = "NO"
        Next

        DgvLegderList.Rows(e.RowIndex).Cells("Tick").Value = "YES"

    End Sub
    Private Sub txtNameOrMobileNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNameOrMobileNo.TextChanged

        DgvLegderList.Rows.Clear()

        If Trim(txtNameOrMobileNo.Text) = "" Then
            LedgerDetails()
            Exit Sub
        End If

        Dim Ds As New DataSet
        Adapter = New SqlDataAdapter("SELECT L_LedCode_i AS 'LedCode',L_LedName_vc AS 'LedName',ISNULL(L_OpBal_n,0)AS 'OpBalL',L_OpBalType_vc AS 'OpBalType' FROM tbl_AccLedger a WHERE (L_LedName_vc like N'" & txtNameOrMobileNo.Text.Trim + "%" & "' OR L_LedCode_i LIKE '" & txtNameOrMobileNo.Text.Trim + "%" & "') ORDER BY L_LedName_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds)

        Dim RowIndex As Integer = 0
        For Each Dr As DataRow In Ds.Tables(0).Rows
            DgvLegderList.Rows.Add()
            DgvLegderList.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
            DgvLegderList.Rows(RowIndex).Cells("LedCode").Value = Dr("LedCode")
            DgvLegderList.Rows(RowIndex).Cells("LedName").Value = Dr("LedName")
            DgvLegderList.Rows(RowIndex).Cells("OpBalL").Value = Dr("OpBalL")
            DgvLegderList.Rows(RowIndex).Cells("OpBalType").Value = Dr("OpBalType")
            RowIndex += 1
        Next

        If DgvLegderList.Rows.Count > 0 Then
            DgvLegderList.CurrentCell = DgvLegderList.Rows(0).Cells("LedName")
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