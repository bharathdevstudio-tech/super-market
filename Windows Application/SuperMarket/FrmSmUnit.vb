Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' SuperMarket - Unit Master.  Add / Edit / Save / Delete / Search / Refresh / Export.
''' All new SQL is parameterized. Uses VistawinFunctions.ServerConnection.
''' </summary>
Public Class FrmSmUnit

    Dim Ds As New DataSet
    Dim Adapter As New SqlDataAdapter
    Dim Trans As SqlTransaction
    Dim EditMode As Boolean = False
    Dim EditCode As Integer = 0

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                Me.Close()
            ElseIf msg.WParam.ToInt32 = CInt(Keys.F2) Then
                BtnSave.PerformClick()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FrmSmUnit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VSuperMarket.EnsureConnection()
            LoadGrid()
            txtUnitName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGrid()
        Try
            VSuperMarket.EnsureConnection()
            Ds = New DataSet
            Dim sql As String = "SELECT U_RecordNo_i, U_EName_vc, ISNULL(U_OrderNo_i,0) AS U_OrderNo_i, ISNULL(U_Show_vc,'YES') AS U_Show_vc " & _
                                "FROM tbl_UnitList "
            If txtSearch.Text.Trim <> "" Then
                Adapter = New SqlDataAdapter(sql & "WHERE U_EName_vc LIKE @S ORDER BY U_OrderNo_i", VistawinFunctions.ServerConnection)
                Adapter.SelectCommand.Parameters.AddWithValue("@S", "%" & txtSearch.Text.Trim & "%")
            Else
                Adapter = New SqlDataAdapter(sql & "ORDER BY U_OrderNo_i", VistawinFunctions.ServerConnection)
            End If
            Adapter.Fill(Ds, "Filldata")

            DgvUnit.Rows.Clear()
            Dim i As Integer = 0
            For Each Dr As DataRow In Ds.Tables("Filldata").Rows
                DgvUnit.Rows.Add()
                DgvUnit.Rows(i).Cells("SNo3").Value = i + 1
                DgvUnit.Rows(i).Cells("U_RecordNo_i").Value = Dr("U_RecordNo_i")
                DgvUnit.Rows(i).Cells("U_EName_vc").Value = Dr("U_EName_vc")
                DgvUnit.Rows(i).Cells("U_OrderNo_i").Value = Dr("U_OrderNo_i")
                DgvUnit.Rows(i).Cells("U_Show_vc").Value = Dr("U_Show_vc")
                DgvUnit.Rows(i).Cells("Tick3").Value = "NO"
                i += 1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearFields()
        txtUnitName.Text = ""
        txtOrderNo.Text = ""
        chkShow.Checked = True
        EditMode = False
        EditCode = 0
        txtUnitName.Focus()
    End Sub

    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearFields()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If txtUnitName.Text.Trim = "" Then
            MessageBox.Show("Enter Unit Name.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUnitName.Focus()
            Exit Sub
        End If
        If Not VSuperMarket.HasPermission("FrmSmUnit", If(EditMode, "EDIT", "ADD")) Then
            MessageBox.Show("You do not have permission for this action.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim orderNo As Integer = 0
        If txtOrderNo.Text.Trim <> "" Then Integer.TryParse(txtOrderNo.Text.Trim, orderNo)

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            Dim dup As Integer
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_UnitList WHERE U_EName_vc=@N AND U_RecordNo_i<>@C", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@N", txtUnitName.Text.Trim)
                cmd.Parameters.AddWithValue("@C", EditCode)
                dup = CInt(cmd.ExecuteScalar())
            End Using
            If dup > 0 Then
                MessageBox.Show("This Unit Name already exists.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Trans.Rollback() : Trans.Dispose()
                txtUnitName.Focus()
                Exit Sub
            End If

            If EditMode Then
                Using cmd As New SqlCommand("UPDATE tbl_UnitList SET U_EName_vc=@N, U_OrderNo_i=@O, U_Show_vc=@S WHERE U_RecordNo_i=@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@N", txtUnitName.Text.Trim)
                    cmd.Parameters.AddWithValue("@O", orderNo)
                    cmd.Parameters.AddWithValue("@S", If(chkShow.Checked, "YES", "NO"))
                    cmd.Parameters.AddWithValue("@C", EditCode)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmUnit", "EDIT", "Unit " & txtUnitName.Text.Trim)
            Else
                Using cmd As New SqlCommand("INSERT INTO tbl_UnitList (U_EName_vc,U_OrderNo_i,U_Show_vc) VALUES (@N,@O,@S)", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@N", txtUnitName.Text.Trim)
                    cmd.Parameters.AddWithValue("@O", orderNo)
                    cmd.Parameters.AddWithValue("@S", If(chkShow.Checked, "YES", "NO"))
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmUnit", "ADD", "Unit " & txtUnitName.Text.Trim)
            End If

            Trans.Commit()
            MessageBox.Show("Saved Successfully.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    Private Sub DgvUnit_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvUnit.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If DgvUnit.Columns(e.ColumnIndex).Name = "Tick3" Then
            For Each Dr As DataGridViewRow In DgvUnit.Rows
                Dr.Cells("Tick3").Value = "NO"
            Next
            DgvUnit.Rows(e.RowIndex).Cells("Tick3").Value = "YES"
        Else
            LoadRowIntoFields(e.RowIndex)
        End If
    End Sub

    Private Sub LoadRowIntoFields(ByVal rowIndex As Integer)
        Dim r As DataGridViewRow = DgvUnit.Rows(rowIndex)
        EditCode = CInt(r.Cells("U_RecordNo_i").Value)
        txtUnitName.Text = Convert.ToString(r.Cells("U_EName_vc").Value)
        txtOrderNo.Text = Convert.ToString(r.Cells("U_OrderNo_i").Value)
        chkShow.Checked = (Convert.ToString(r.Cells("U_Show_vc").Value).ToUpper = "YES")
        EditMode = True
        txtUnitName.Focus()
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        For Each Dr As DataGridViewRow In DgvUnit.Rows
            If Convert.ToString(Dr.Cells("Tick3").Value) = "YES" Then
                LoadRowIntoFields(Dr.Index)
                Exit Sub
            End If
        Next
        MessageBox.Show("Select a row first.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not VSuperMarket.HasPermission("FrmSmUnit", "DELETE") Then
            MessageBox.Show("You do not have permission to delete.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim ticked As New System.Collections.Generic.List(Of Integer)
        For Each Dr As DataGridViewRow In DgvUnit.Rows
            If Convert.ToString(Dr.Cells("Tick3").Value) = "YES" Then ticked.Add(CInt(Dr.Cells("U_RecordNo_i").Value))
        Next
        If ticked.Count = 0 Then
            MessageBox.Show("Select at least one row to delete.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If MessageBox.Show("Are you sure to delete the selected record(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            For Each code As Integer In ticked
                Dim used As Integer
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_StockDtl WHERE St_Unit_vc=(SELECT U_EName_vc FROM tbl_UnitList WHERE U_RecordNo_i=@C)", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", code)
                    used = CInt(cmd.ExecuteScalar())
                End Using
                If used > 0 Then
                    MessageBox.Show("Unit is used by a product and cannot be deleted.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Trans.Rollback() : Trans.Dispose()
                    Exit Sub
                End If
                Using cmd As New SqlCommand("DELETE FROM tbl_UnitList WHERE U_RecordNo_i=@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", code)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmUnit", "DELETE", "Unit id " & code.ToString())
            Next
            Trans.Commit()
            MessageBox.Show("Deleted Successfully.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        LoadGrid()
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then LoadGrid()
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        txtSearch.Text = ""
        ClearFields()
        LoadGrid()
    End Sub

    Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        Try
            If DgvUnit.Rows.Count = 0 Then
                MessageBox.Show("Nothing to export.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Using sfd As New SaveFileDialog
                sfd.Filter = "CSV file (*.csv)|*.csv"
                sfd.FileName = "UnitMaster_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"
                If sfd.ShowDialog() = DialogResult.OK Then
                    Using sw As New StreamWriter(sfd.FileName, False)
                        Dim heads As New System.Text.StringBuilder
                        For Each col As DataGridViewColumn In DgvUnit.Columns
                            If heads.Length > 0 Then heads.Append(",")
                            heads.Append("""" & col.HeaderText & """")
                        Next
                        sw.WriteLine(heads.ToString())
                        For Each r As DataGridViewRow In DgvUnit.Rows
                            Dim sb As New System.Text.StringBuilder
                            For Each c As DataGridViewCell In r.Cells
                                If sb.Length > 0 Then sb.Append(",")
                                sb.Append("""" & Convert.ToString(c.Value).Replace("""", """""") & """")
                            Next
                            sw.WriteLine(sb.ToString())
                        Next
                    End Using
                    MessageBox.Show("Exported successfully.", "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Unit Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

End Class
