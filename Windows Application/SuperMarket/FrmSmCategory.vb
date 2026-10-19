Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' SuperMarket - Product Category Master.
''' Add / Edit / Save / Delete / Search / Refresh / Export.
''' All new SQL is parameterized. Uses VistawinFunctions.ServerConnection.
''' </summary>
Public Class FrmSmCategory

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

    Private Sub FrmSmCategory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VSuperMarket.EnsureConnection()
            LoadGrid()
            txtCategoryName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ---- grid -------------------------------------------------------------------------
    Private Sub LoadGrid()
        Try
            VSuperMarket.EnsureConnection()
            Ds = New DataSet
            Dim sql As String = "SELECT Category_Code_n, Category_Name_vc, Cat_AliasName_vc, ISNULL(Cat_GSTPer_n,0) AS Cat_GSTPer_n, " & _
                                "ISNULL(Cat_Show_vc,'YES') AS Cat_Show_vc FROM tbl_Category ORDER BY Category_Name_vc"
            If txtSearch.Text.Trim <> "" Then
                sql = "SELECT Category_Code_n, Category_Name_vc, Cat_AliasName_vc, ISNULL(Cat_GSTPer_n,0) AS Cat_GSTPer_n, " & _
                      "ISNULL(Cat_Show_vc,'YES') AS Cat_Show_vc FROM tbl_Category " & _
                      "WHERE Category_Name_vc LIKE @S OR Cat_AliasName_vc LIKE @S ORDER BY Category_Name_vc"
                Adapter = New SqlDataAdapter(sql, VistawinFunctions.ServerConnection)
                Adapter.SelectCommand.Parameters.AddWithValue("@S", "%" & txtSearch.Text.Trim & "%")
            Else
                Adapter = New SqlDataAdapter(sql, VistawinFunctions.ServerConnection)
            End If
            Adapter.Fill(Ds, "Filldata")

            DgvCategory.Rows.Clear()
            Dim i As Integer = 0
            For Each Dr As DataRow In Ds.Tables("Filldata").Rows
                DgvCategory.Rows.Add()
                DgvCategory.Rows(i).Cells("SNo3").Value = i + 1
                DgvCategory.Rows(i).Cells("Category_Code_n").Value = Dr("Category_Code_n")
                DgvCategory.Rows(i).Cells("Category_Name_vc").Value = Dr("Category_Name_vc")
                DgvCategory.Rows(i).Cells("Cat_AliasName_vc").Value = Dr("Cat_AliasName_vc")
                DgvCategory.Rows(i).Cells("Cat_GSTPer_n").Value = Dr("Cat_GSTPer_n")
                DgvCategory.Rows(i).Cells("Cat_Show_vc").Value = Dr("Cat_Show_vc")
                DgvCategory.Rows(i).Cells("Tick3").Value = "NO"
                i += 1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ---- clear / new ------------------------------------------------------------------
    Private Sub ClearFields()
        txtCategoryName.Text = ""
        txtAliasName.Text = ""
        txtGstPer.Text = ""
        chkShow.Checked = True
        EditMode = False
        EditCode = 0
        txtCategoryName.Focus()
    End Sub

    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearFields()
    End Sub

    ' ---- save (insert / update) -------------------------------------------------------
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If txtCategoryName.Text.Trim = "" Then
            MessageBox.Show("Enter Category Name.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCategoryName.Focus()
            Exit Sub
        End If
        If Not VSuperMarket.HasPermission("FrmSmCategory", If(EditMode, "EDIT", "ADD")) Then
            MessageBox.Show("You do not have permission for this action.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim gst As Decimal = 0
        If txtGstPer.Text.Trim <> "" AndAlso Not Decimal.TryParse(txtGstPer.Text.Trim, gst) Then
            MessageBox.Show("GST % must be numeric.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtGstPer.Focus()
            Exit Sub
        End If

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            ' duplicate check (parameterized)
            Dim dupSql As String = "SELECT COUNT(*) FROM tbl_Category WHERE Category_Name_vc=@N AND Category_Code_n<>@C"
            Dim dup As Integer
            Using cmd As New SqlCommand(dupSql, VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@N", txtCategoryName.Text.Trim)
                cmd.Parameters.AddWithValue("@C", EditCode)
                dup = CInt(cmd.ExecuteScalar())
            End Using
            If dup > 0 Then
                MessageBox.Show("This Category Name already exists.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Trans.Rollback() : Trans.Dispose()
                txtCategoryName.Focus()
                Exit Sub
            End If

            If EditMode Then
                Dim up As String = "UPDATE tbl_Category SET Category_Name_vc=@N, Cat_AliasName_vc=@A, Cat_GSTPer_n=@G, " & _
                                   "Cat_Show_vc=@S, User_id_vc=@U, Ent_date_d=GETDATE() WHERE Category_Code_n=@C"
                Using cmd As New SqlCommand(up, VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@N", txtCategoryName.Text.Trim)
                    cmd.Parameters.AddWithValue("@A", txtAliasName.Text.Trim)
                    cmd.Parameters.AddWithValue("@G", gst)
                    cmd.Parameters.AddWithValue("@S", If(chkShow.Checked, "YES", "NO"))
                    cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
                    cmd.Parameters.AddWithValue("@C", EditCode)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmCategory", "EDIT", "Category " & txtCategoryName.Text.Trim)
            Else
                ' Category_Code_n is not an identity column in the legacy schema,
                ' so the next code is taken inside the transaction (UPDLOCK).
                Dim newCode As Integer
                Using cmd As New SqlCommand("SELECT ISNULL(MAX(Category_Code_n),0)+1 FROM tbl_Category WITH (UPDLOCK,HOLDLOCK)", VistawinFunctions.ServerConnection, Trans)
                    newCode = CInt(cmd.ExecuteScalar())
                End Using
                Dim ins As String = "INSERT INTO tbl_Category (Category_Code_n,Category_Name_vc,Cat_AliasName_vc,Cat_GSTPer_n,Cat_Show_vc,User_id_vc,Ent_date_d) " & _
                                    "VALUES (@C,@N,@A,@G,@S,@U,GETDATE())"
                Using cmd As New SqlCommand(ins, VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", newCode)
                    cmd.Parameters.AddWithValue("@N", txtCategoryName.Text.Trim)
                    cmd.Parameters.AddWithValue("@A", txtAliasName.Text.Trim)
                    cmd.Parameters.AddWithValue("@G", gst)
                    cmd.Parameters.AddWithValue("@S", If(chkShow.Checked, "YES", "NO"))
                    cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmCategory", "ADD", "Category " & txtCategoryName.Text.Trim)
            End If

            Trans.Commit()
            MessageBox.Show("Saved Successfully.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    ' ---- grid selection : tick + load into fields -------------------------------------
    Private Sub DgvCategory_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvCategory.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If DgvCategory.Columns(e.ColumnIndex).Name = "Tick3" Then
            For Each Dr As DataGridViewRow In DgvCategory.Rows
                Dr.Cells("Tick3").Value = "NO"
            Next
            DgvCategory.Rows(e.RowIndex).Cells("Tick3").Value = "YES"
        Else
            LoadRowIntoFields(e.RowIndex)
        End If
    End Sub

    Private Sub LoadRowIntoFields(ByVal rowIndex As Integer)
        Try
            Dim r As DataGridViewRow = DgvCategory.Rows(rowIndex)
            EditCode = CInt(r.Cells("Category_Code_n").Value)
            txtCategoryName.Text = Convert.ToString(r.Cells("Category_Name_vc").Value)
            txtAliasName.Text = Convert.ToString(r.Cells("Cat_AliasName_vc").Value)
            txtGstPer.Text = Convert.ToString(r.Cells("Cat_GSTPer_n").Value)
            chkShow.Checked = (Convert.ToString(r.Cells("Cat_Show_vc").Value).ToUpper = "YES")
            EditMode = True
            txtCategoryName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        For Each Dr As DataGridViewRow In DgvCategory.Rows
            If Convert.ToString(Dr.Cells("Tick3").Value) = "YES" Then
                LoadRowIntoFields(Dr.Index)
                Exit Sub
            End If
        Next
        MessageBox.Show("Select a row first.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' ---- delete -----------------------------------------------------------------------
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not VSuperMarket.HasPermission("FrmSmCategory", "DELETE") Then
            MessageBox.Show("You do not have permission to delete.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim ticked As New System.Collections.Generic.List(Of Integer)
        For Each Dr As DataGridViewRow In DgvCategory.Rows
            If Convert.ToString(Dr.Cells("Tick3").Value) = "YES" Then ticked.Add(CInt(Dr.Cells("Category_Code_n").Value))
        Next
        If ticked.Count = 0 Then
            MessageBox.Show("Select at least one row to delete.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If MessageBox.Show("Are you sure to delete the selected record(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            For Each code As Integer In ticked
                ' block delete when the category is used by a product
                Dim used As Integer
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_StockDtl WHERE St_CatID_i=@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", code)
                    used = CInt(cmd.ExecuteScalar())
                End Using
                If used > 0 Then
                    MessageBox.Show("Category is used by a product and cannot be deleted.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Trans.Rollback() : Trans.Dispose()
                    Exit Sub
                End If
                Using cmd As New SqlCommand("DELETE FROM tbl_Category WHERE Category_Code_n=@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", code)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmCategory", "DELETE", "Category code " & code.ToString())
            Next
            Trans.Commit()
            MessageBox.Show("Deleted Successfully.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    ' ---- search / refresh -------------------------------------------------------------
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

    ' ---- export to CSV (opens in Excel) ----------------------------------------------
    Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        Try
            If DgvCategory.Rows.Count = 0 Then
                MessageBox.Show("Nothing to export.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Using sfd As New SaveFileDialog
                sfd.Filter = "CSV file (*.csv)|*.csv"
                sfd.FileName = "CategoryMaster_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"
                If sfd.ShowDialog() = DialogResult.OK Then
                    Using sw As New StreamWriter(sfd.FileName, False)
                        Dim heads As New System.Text.StringBuilder
                        For Each col As DataGridViewColumn In DgvCategory.Columns
                            If heads.Length > 0 Then heads.Append(",")
                            heads.Append("""" & col.HeaderText & """")
                        Next
                        sw.WriteLine(heads.ToString())
                        For Each r As DataGridViewRow In DgvCategory.Rows
                            Dim sb As New System.Text.StringBuilder
                            For Each c As DataGridViewCell In r.Cells
                                If sb.Length > 0 Then sb.Append(",")
                                sb.Append("""" & Convert.ToString(c.Value).Replace("""", """""") & """")
                            Next
                            sw.WriteLine(sb.ToString())
                        Next
                    End Using
                    MessageBox.Show("Exported successfully.", "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

End Class
