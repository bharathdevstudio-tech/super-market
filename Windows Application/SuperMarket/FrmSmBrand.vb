Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' SuperMarket - Brand Master.  Add / Edit / Save / Delete / Search / Refresh / Export.
''' All new SQL is parameterized. Uses VistawinFunctions.ServerConnection.
''' </summary>
Public Class FrmSmBrand

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

    Private Sub FrmSmBrand_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VSuperMarket.EnsureConnection()
            LoadGrid()
            txtBrandName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGrid()
        Try
            VSuperMarket.EnsureConnection()
            Ds = New DataSet
            Dim sql As String = "SELECT Brand_Code_n, Brand_Name_vc, ISNULL(Brand_Show_vc,'YES') AS Brand_Show_vc FROM tbl_Brand "
            If txtSearch.Text.Trim <> "" Then
                Adapter = New SqlDataAdapter(sql & "WHERE Brand_Name_vc LIKE @S ORDER BY Brand_Name_vc", VistawinFunctions.ServerConnection)
                Adapter.SelectCommand.Parameters.AddWithValue("@S", "%" & txtSearch.Text.Trim & "%")
            Else
                Adapter = New SqlDataAdapter(sql & "ORDER BY Brand_Name_vc", VistawinFunctions.ServerConnection)
            End If
            Adapter.Fill(Ds, "Filldata")

            DgvBrand.Rows.Clear()
            Dim i As Integer = 0
            For Each Dr As DataRow In Ds.Tables("Filldata").Rows
                DgvBrand.Rows.Add()
                DgvBrand.Rows(i).Cells("SNo3").Value = i + 1
                DgvBrand.Rows(i).Cells("Brand_Code_n").Value = Dr("Brand_Code_n")
                DgvBrand.Rows(i).Cells("Brand_Name_vc").Value = Dr("Brand_Name_vc")
                DgvBrand.Rows(i).Cells("Brand_Show_vc").Value = Dr("Brand_Show_vc")
                DgvBrand.Rows(i).Cells("Tick3").Value = "NO"
                i += 1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearFields()
        txtBrandName.Text = ""
        chkShow.Checked = True
        EditMode = False
        EditCode = 0
        txtBrandName.Focus()
    End Sub

    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearFields()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If txtBrandName.Text.Trim = "" Then
            MessageBox.Show("Enter Brand Name.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtBrandName.Focus()
            Exit Sub
        End If
        If Not VSuperMarket.HasPermission("FrmSmBrand", If(EditMode, "EDIT", "ADD")) Then
            MessageBox.Show("You do not have permission for this action.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            Dim dup As Integer
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_Brand WHERE Brand_Name_vc=@N AND Brand_Code_n<>@C", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@N", txtBrandName.Text.Trim)
                cmd.Parameters.AddWithValue("@C", EditCode)
                dup = CInt(cmd.ExecuteScalar())
            End Using
            If dup > 0 Then
                MessageBox.Show("This Brand Name already exists.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Trans.Rollback() : Trans.Dispose()
                txtBrandName.Focus()
                Exit Sub
            End If

            If EditMode Then
                Using cmd As New SqlCommand("UPDATE tbl_Brand SET Brand_Name_vc=@N, Brand_Show_vc=@S, User_id_vc=@U, Ent_date_d=GETDATE() WHERE Brand_Code_n=@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@N", txtBrandName.Text.Trim)
                    cmd.Parameters.AddWithValue("@S", If(chkShow.Checked, "YES", "NO"))
                    cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
                    cmd.Parameters.AddWithValue("@C", EditCode)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmBrand", "EDIT", "Brand " & txtBrandName.Text.Trim)
            Else
                ' Brand_Code_n is not an identity column in the legacy schema.
                Dim newCode As Integer
                Using cmd As New SqlCommand("SELECT ISNULL(MAX(Brand_Code_n),0)+1 FROM tbl_Brand WITH (UPDLOCK,HOLDLOCK)", VistawinFunctions.ServerConnection, Trans)
                    newCode = CInt(cmd.ExecuteScalar())
                End Using
                Using cmd As New SqlCommand("INSERT INTO tbl_Brand (Brand_Code_n,Brand_Name_vc,Brand_Show_vc,User_id_vc,Ent_date_d) VALUES (@C,@N,@S,@U,GETDATE())", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", newCode)
                    cmd.Parameters.AddWithValue("@N", txtBrandName.Text.Trim)
                    cmd.Parameters.AddWithValue("@S", If(chkShow.Checked, "YES", "NO"))
                    cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmBrand", "ADD", "Brand " & txtBrandName.Text.Trim)
            End If

            Trans.Commit()
            MessageBox.Show("Saved Successfully.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    Private Sub DgvBrand_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvBrand.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If DgvBrand.Columns(e.ColumnIndex).Name = "Tick3" Then
            For Each Dr As DataGridViewRow In DgvBrand.Rows
                Dr.Cells("Tick3").Value = "NO"
            Next
            DgvBrand.Rows(e.RowIndex).Cells("Tick3").Value = "YES"
        Else
            LoadRowIntoFields(e.RowIndex)
        End If
    End Sub

    Private Sub LoadRowIntoFields(ByVal rowIndex As Integer)
        Dim r As DataGridViewRow = DgvBrand.Rows(rowIndex)
        EditCode = CInt(r.Cells("Brand_Code_n").Value)
        txtBrandName.Text = Convert.ToString(r.Cells("Brand_Name_vc").Value)
        chkShow.Checked = (Convert.ToString(r.Cells("Brand_Show_vc").Value).ToUpper = "YES")
        EditMode = True
        txtBrandName.Focus()
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        For Each Dr As DataGridViewRow In DgvBrand.Rows
            If Convert.ToString(Dr.Cells("Tick3").Value) = "YES" Then
                LoadRowIntoFields(Dr.Index)
                Exit Sub
            End If
        Next
        MessageBox.Show("Select a row first.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not VSuperMarket.HasPermission("FrmSmBrand", "DELETE") Then
            MessageBox.Show("You do not have permission to delete.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim ticked As New System.Collections.Generic.List(Of Integer)
        For Each Dr As DataGridViewRow In DgvBrand.Rows
            If Convert.ToString(Dr.Cells("Tick3").Value) = "YES" Then ticked.Add(CInt(Dr.Cells("Brand_Code_n").Value))
        Next
        If ticked.Count = 0 Then
            MessageBox.Show("Select at least one row to delete.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If MessageBox.Show("Are you sure to delete the selected record(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            For Each code As Integer In ticked
                Dim used As Integer
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_StockDtl WHERE St_BrandID_i=@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", code)
                    used = CInt(cmd.ExecuteScalar())
                End Using
                If used > 0 Then
                    MessageBox.Show("Brand is used by a product and cannot be deleted.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Trans.Rollback() : Trans.Dispose()
                    Exit Sub
                End If
                Using cmd As New SqlCommand("DELETE FROM tbl_Brand WHERE Brand_Code_n=@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", code)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmBrand", "DELETE", "Brand code " & code.ToString())
            Next
            Trans.Commit()
            MessageBox.Show("Deleted Successfully.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            If DgvBrand.Rows.Count = 0 Then
                MessageBox.Show("Nothing to export.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Using sfd As New SaveFileDialog
                sfd.Filter = "CSV file (*.csv)|*.csv"
                sfd.FileName = "BrandMaster_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"
                If sfd.ShowDialog() = DialogResult.OK Then
                    Using sw As New StreamWriter(sfd.FileName, False)
                        Dim heads As New System.Text.StringBuilder
                        For Each col As DataGridViewColumn In DgvBrand.Columns
                            If heads.Length > 0 Then heads.Append(",")
                            heads.Append("""" & col.HeaderText & """")
                        Next
                        sw.WriteLine(heads.ToString())
                        For Each r As DataGridViewRow In DgvBrand.Rows
                            Dim sb As New System.Text.StringBuilder
                            For Each c As DataGridViewCell In r.Cells
                                If sb.Length > 0 Then sb.Append(",")
                                sb.Append("""" & Convert.ToString(c.Value).Replace("""", """""") & """")
                            Next
                            sw.WriteLine(sb.ToString())
                        Next
                    End Using
                    MessageBox.Show("Exported successfully.", "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Brand Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

End Class
