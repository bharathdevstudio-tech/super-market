Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' SuperMarket - Supplier Master.
'''   tbl_CusProfile : C_Type_vc='SUPPLIER', C_RecordNumber_i identity,
'''   C_AllID_n = MAX+1 (global), C_ID_n = MAX+1 per type.
''' All new SQL is parameterized.
''' </summary>
Public Class FrmSmSupplier

    Private Const PartyType As String = "SUPPLIER"
    Private Const Party3 As String = "Supplier"

    Dim Ds As New DataSet
    Dim Adapter As New SqlDataAdapter
    Dim Trans As SqlTransaction
    Dim EditMode As Boolean = False
    Dim EditRec As Integer = 0        ' C_RecordNumber_i
    Dim EditID As Integer = 0         ' C_ID_n

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

    Private Sub FrmSmSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VSuperMarket.EnsureConnection()
            cboOpType.SelectedIndex = 0
            LoadGrid()
            txtSupName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGrid()
        Try
            VSuperMarket.EnsureConnection()
            Ds = New DataSet
            Dim sql As String = "SELECT C_RecordNumber_i, C_ID_n, C_Name_vc, ISNULL(C_Mobile_vc,'') AS C_Mobile_vc, " & _
                                "ISNULL(C_GSTIN_vc,'') AS C_GSTIN_vc, ISNULL(C_OpBal_n,0) AS C_OpBal_n, " & _
                                "CASE WHEN ISNULL(C_Show_vc,'YES')='YES' THEN 'YES' ELSE 'NO' END AS Active " & _
                                "FROM tbl_CusProfile WHERE C_Type_vc=@T "

            If txtSearch.Text.Trim <> "" Then
                sql = sql & "AND (C_Name_vc LIKE @S OR ISNULL(C_Mobile_vc,'') LIKE @S OR ISNULL(C_GSTIN_vc,'') LIKE @S OR CONVERT(varchar(20),C_ID_n) LIKE @S) "
            End If
            sql = sql & "ORDER BY C_Name_vc"

            Adapter = New SqlDataAdapter(sql, VistawinFunctions.ServerConnection)
            Adapter.SelectCommand.Parameters.AddWithValue("@T", PartyType)
            If txtSearch.Text.Trim <> "" Then
                Adapter.SelectCommand.Parameters.AddWithValue("@S", "%" & txtSearch.Text.Trim & "%")
            End If
            Adapter.Fill(Ds, "Filldata")

            DgvSup.Rows.Clear()
            Dim i As Integer = 0
            For Each Dr As DataRow In Ds.Tables("Filldata").Rows
                DgvSup.Rows.Add()
                DgvSup.Rows(i).Cells("SNo3").Value = i + 1
                DgvSup.Rows(i).Cells("Tick3").Value = "NO"
                DgvSup.Rows(i).Cells("C_RecordNumber_i").Value = Dr("C_RecordNumber_i")
                DgvSup.Rows(i).Cells("C_ID_n").Value = Dr("C_ID_n")
                DgvSup.Rows(i).Cells("C_Name_vc").Value = Dr("C_Name_vc")
                DgvSup.Rows(i).Cells("C_Mobile_vc").Value = Dr("C_Mobile_vc")
                DgvSup.Rows(i).Cells("C_GSTIN_vc").Value = Dr("C_GSTIN_vc")
                DgvSup.Rows(i).Cells("C_OpBal_n").Value = Format(Convert.ToDecimal(Dr("C_OpBal_n")), "0.00")
                DgvSup.Rows(i).Cells("Active").Value = Dr("Active")
                i += 1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearFields()
        txtSupCode.Text = ""
        txtSupName.Text = ""
        txtMobile.Text = ""
        txtEmail.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtAdd3.Text = ""
        txtPincode.Text = ""
        txtStateCode.Text = ""
        txtGSTIN.Text = ""
        txtPAN.Text = ""
        txtOpBal.Text = ""
        txtCreditLimit.Text = ""
        txtDueDays.Text = ""
        cboOpType.SelectedIndex = 0
        chkShow.Checked = True
        EditMode = False
        EditRec = 0
        EditID = 0
        txtSupName.Focus()
    End Sub

    Private Function GetDec(ByVal t As TextBox) As Decimal
        If t.Text.Trim = "" Then Return 0
        Dim v As Decimal = 0
        Decimal.TryParse(t.Text.Trim, v)
        Return v
    End Function

    Private Function GetInt(ByVal t As TextBox) As Integer
        If t.Text.Trim = "" Then Return 0
        Dim v As Integer = 0
        Integer.TryParse(t.Text.Trim, v)
        Return v
    End Function

    Private Function DBN(ByVal o As Object) As String
        If o Is Nothing OrElse Convert.IsDBNull(o) Then Return ""
        Return Convert.ToString(o)
    End Function

    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearFields()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If txtSupName.Text.Trim = "" Then
            MessageBox.Show("Enter Supplier Name.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSupName.Focus()
            Exit Sub
        End If
        If Not VSuperMarket.HasPermission("FrmSmSupplier", If(EditMode, "EDIT", "ADD")) Then
            MessageBox.Show("You do not have permission for this action.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim opBal As Decimal = GetDec(txtOpBal)
        Dim opType As String = Convert.ToString(cboOpType.SelectedItem)
        If opType = "" Then opType = "Debit"
        Dim cLimit As Decimal = GetDec(txtCreditLimit)
        Dim dueDays As Integer = GetInt(txtDueDays)
        Dim show As String = If(chkShow.Checked, "YES", "NO")

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            Dim dup As Integer
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_CusProfile WHERE C_Type_vc=@T AND C_Name_vc=@N AND ISNULL(C_Mobile_vc,'')=@M AND C_RecordNumber_i<>@R", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@T", PartyType)
                cmd.Parameters.AddWithValue("@N", txtSupName.Text.Trim)
                cmd.Parameters.AddWithValue("@M", txtMobile.Text.Trim)
                cmd.Parameters.AddWithValue("@R", EditRec)
                dup = CInt(cmd.ExecuteScalar())
            End Using
            If dup > 0 Then
                MessageBox.Show("This record already exists.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Trans.Rollback() : Trans.Dispose()
                txtSupName.Focus()
                Exit Sub
            End If

            If EditMode Then
                Using cmd As New SqlCommand("UPDATE tbl_CusProfile SET C_Name_vc=@N, C_CmpName_vc=@CN, C_Mobile_vc=@Mob, C_Email_vc=@Email, C_Add1_vc=@A1, " & _
                                            "C_Add2_vc=@A2, C_Add3_vc=@A3, C_Pincode_vc=@Pin, C_StateCode_vc=@State, C_GSTIN_vc=@GSTIN, C_PANNo_vc=@PAN, " & _
                                            "C_OpBal_n=@OpBal, C_OpType_vc=@OpType, C_Show_vc=@Show, C_CLimit_n=@CLimit, C_DueDays_i=@DueDays, " & _
                                            "C_UpDate_d=GETDATE() WHERE C_RecordNumber_i=@R", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@N", txtSupName.Text.Trim)
                    cmd.Parameters.AddWithValue("@CN", txtSupName.Text.Trim)
                    cmd.Parameters.AddWithValue("@Mob", txtMobile.Text.Trim)
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim)
                    cmd.Parameters.AddWithValue("@A1", txtAdd1.Text.Trim)
                    cmd.Parameters.AddWithValue("@A2", txtAdd2.Text.Trim)
                    cmd.Parameters.AddWithValue("@A3", txtAdd3.Text.Trim)
                    cmd.Parameters.AddWithValue("@Pin", txtPincode.Text.Trim)
                    cmd.Parameters.AddWithValue("@State", txtStateCode.Text.Trim)
                    cmd.Parameters.AddWithValue("@GSTIN", txtGSTIN.Text.Trim)
                    cmd.Parameters.AddWithValue("@PAN", txtPAN.Text.Trim)
                    cmd.Parameters.AddWithValue("@OpBal", opBal)
                    cmd.Parameters.AddWithValue("@OpType", opType)
                    cmd.Parameters.AddWithValue("@Show", show)
                    cmd.Parameters.AddWithValue("@CLimit", cLimit)
                    cmd.Parameters.AddWithValue("@DueDays", dueDays)
                    cmd.Parameters.AddWithValue("@R", EditRec)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmSupplier", "EDIT", "Supplier " & txtSupName.Text.Trim)
            Else
                Dim newAll As Integer
                Using cmd As New SqlCommand("SELECT ISNULL(MAX(C_AllID_n),0)+1 FROM tbl_CusProfile WITH (UPDLOCK,HOLDLOCK)", VistawinFunctions.ServerConnection, Trans)
                    newAll = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
                Dim newID As Integer
                Using cmd As New SqlCommand("SELECT ISNULL(MAX(C_ID_n),0)+1 FROM tbl_CusProfile WITH (UPDLOCK,HOLDLOCK) WHERE C_Type_vc=@T", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@T", PartyType)
                    newID = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
                Using cmd As New SqlCommand("INSERT INTO tbl_CusProfile (C_EDate_d, C_AllID_n, C_Type_vc, C_Type3_vc, C_ID_n, C_GrpID_i, C_Name_vc, C_CmpName_vc, C_Mobile_vc, C_Email_vc, " & _
                                            "C_Add1_vc, C_Add2_vc, C_Add3_vc, C_Pincode_vc, C_StateCode_vc, C_GSTIN_vc, C_PANNo_vc, C_OpBal_n, C_OpType_vc, " & _
                                            "C_Show_vc, C_OrderBy_i, C_CLimit_n, C_DueDays_i, C_UserID_vc, C_UpDate_d) " & _
                                            "VALUES (GETDATE(), @AllID, @T, @T3, @ID, 0, @N, @CN, @Mob, @Email, @A1, @A2, @A3, @Pin, @State, @GSTIN, @PAN, " & _
                                            "@OpBal, @OpType, @Show, 1, @CLimit, @DueDays, @User, GETDATE())", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@AllID", newAll)
                    cmd.Parameters.AddWithValue("@T", PartyType)
                    cmd.Parameters.AddWithValue("@T3", Party3)
                    cmd.Parameters.AddWithValue("@ID", newID)
                    cmd.Parameters.AddWithValue("@N", txtSupName.Text.Trim)
                    cmd.Parameters.AddWithValue("@CN", txtSupName.Text.Trim)
                    cmd.Parameters.AddWithValue("@Mob", txtMobile.Text.Trim)
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim)
                    cmd.Parameters.AddWithValue("@A1", txtAdd1.Text.Trim)
                    cmd.Parameters.AddWithValue("@A2", txtAdd2.Text.Trim)
                    cmd.Parameters.AddWithValue("@A3", txtAdd3.Text.Trim)
                    cmd.Parameters.AddWithValue("@Pin", txtPincode.Text.Trim)
                    cmd.Parameters.AddWithValue("@State", txtStateCode.Text.Trim)
                    cmd.Parameters.AddWithValue("@GSTIN", txtGSTIN.Text.Trim)
                    cmd.Parameters.AddWithValue("@PAN", txtPAN.Text.Trim)
                    cmd.Parameters.AddWithValue("@OpBal", opBal)
                    cmd.Parameters.AddWithValue("@OpType", opType)
                    cmd.Parameters.AddWithValue("@Show", show)
                    cmd.Parameters.AddWithValue("@CLimit", cLimit)
                    cmd.Parameters.AddWithValue("@DueDays", dueDays)
                    cmd.Parameters.AddWithValue("@User", VSuperMarket.CurrentUserName)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmSupplier", "ADD", "Supplier " & txtSupName.Text.Trim)
            End If

            Trans.Commit()
            MessageBox.Show("Saved Successfully.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    Private Sub DgvSup_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvSup.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If DgvSup.Columns(e.ColumnIndex).Name = "Tick3" Then
            For Each Dr As DataGridViewRow In DgvSup.Rows
                Dr.Cells("Tick3").Value = "NO"
            Next
            DgvSup.Rows(e.RowIndex).Cells("Tick3").Value = "YES"
        Else
            LoadRowIntoFields(e.RowIndex)
        End If
    End Sub

    Private Sub LoadRowIntoFields(ByVal rowIndex As Integer)
        Dim r As DataGridViewRow = DgvSup.Rows(rowIndex)
        Using cmd As New SqlCommand("SELECT * FROM tbl_CusProfile WHERE C_RecordNumber_i=@R", VistawinFunctions.ServerConnection)
            cmd.Parameters.AddWithValue("@R", CInt(r.Cells("C_RecordNumber_i").Value))
            Using dr As SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    EditRec = Convert.ToInt32(dr("C_RecordNumber_i"))
                    EditID = Convert.ToInt32(dr("C_ID_n"))
                    txtSupCode.Text = dr("C_ID_n").ToString()
                    txtSupName.Text = DBN(dr("C_Name_vc"))
                    txtMobile.Text = DBN(dr("C_Mobile_vc"))
                    txtEmail.Text = DBN(dr("C_Email_vc"))
                    txtAdd1.Text = DBN(dr("C_Add1_vc"))
                    txtAdd2.Text = DBN(dr("C_Add2_vc"))
                    txtAdd3.Text = DBN(dr("C_Add3_vc"))
                    txtPincode.Text = DBN(dr("C_Pincode_vc"))
                    txtStateCode.Text = DBN(dr("C_StateCode_vc"))
                    txtGSTIN.Text = DBN(dr("C_GSTIN_vc"))
                    txtPAN.Text = DBN(dr("C_PANNo_vc"))
                    txtOpBal.Text = Convert.ToDecimal(dr("C_OpBal_n")).ToString("0.##")
                    txtCreditLimit.Text = Convert.ToDecimal(dr("C_CLimit_n")).ToString("0.##")
                    txtDueDays.Text = Convert.ToInt32(dr("C_DueDays_i")).ToString()
                    If String.Equals(DBN(dr("C_OpType_vc")), "Credit", StringComparison.OrdinalIgnoreCase) Then
                        cboOpType.SelectedItem = "Credit"
                    Else
                        cboOpType.SelectedItem = "Debit"
                    End If
                    chkShow.Checked = String.Equals(DBN(dr("C_Show_vc")), "YES", StringComparison.OrdinalIgnoreCase)
                End If
            End Using
        End Using
        EditMode = True
        txtSupName.Focus()
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        For Each r As DataGridViewRow In DgvSup.Rows
            If Convert.ToString(r.Cells("Tick3").Value) = "YES" Then
                LoadRowIntoFields(r.Index)
                Exit Sub
            End If
        Next
        If DgvSup.CurrentRow IsNot Nothing Then
            LoadRowIntoFields(DgvSup.CurrentRow.Index)
        Else
            MessageBox.Show("Select a row to edit.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not VSuperMarket.HasPermission("FrmSmSupplier", "DELETE") Then
            MessageBox.Show("You do not have permission for this action.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim ids As New List(Of Integer)
        For Each r As DataGridViewRow In DgvSup.Rows
            If Convert.ToString(r.Cells("Tick3").Value) = "YES" Then ids.Add(CInt(r.Cells("C_ID_n").Value))
        Next
        If ids.Count = 0 AndAlso DgvSup.CurrentRow IsNot Nothing Then ids.Add(CInt(DgvSup.CurrentRow.Cells("C_ID_n").Value))
        If ids.Count = 0 Then
            MessageBox.Show("Select a row to delete.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If MessageBox.Show("Delete selected record(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            For Each id As Integer In ids
                Dim used As Integer
                Using cmd As New SqlCommand("SELECT (SELECT COUNT(*) FROM tbl_PurchDetail WHERE Sa_CusID_n=@I) + (SELECT COUNT(*) FROM tbl_PODetail WHERE Sa_CusID_n=@I)", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@I", id)
                    used = CInt(cmd.ExecuteScalar())
                End Using
                If used > 0 Then
                    MessageBox.Show("Cannot delete: transactions exist for this supplier.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Trans.Rollback() : Trans.Dispose()
                    Exit Sub
                End If
                Using cmd As New SqlCommand("DELETE FROM tbl_CusProfile WHERE C_Type_vc=@T AND C_ID_n=@I", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@T", PartyType)
                    cmd.Parameters.AddWithValue("@I", id)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmSupplier", "DELETE", "Supplier id " & id.ToString())
            Next
            Trans.Commit()
            MessageBox.Show("Deleted Successfully.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        LoadGrid()
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then LoadGrid()
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        txtSearch.Text = ""
        ClearFields()
        LoadGrid()
    End Sub

    Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        Try
            If DgvSup.Rows.Count = 0 Then
                MessageBox.Show("Nothing to export.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Using sfd As New SaveFileDialog
                sfd.Filter = "CSV file (*.csv)|*.csv"
                sfd.FileName = "SupplierMaster_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"
                If sfd.ShowDialog() = DialogResult.OK Then
                    Using sw As New StreamWriter(sfd.FileName, False)
                        Dim heads As New System.Text.StringBuilder
                        For Each col As DataGridViewColumn In DgvSup.Columns
                            If heads.Length > 0 Then heads.Append(",")
                            heads.Append("""" & col.HeaderText & """")
                        Next
                        sw.WriteLine(heads.ToString())
                        For Each r As DataGridViewRow In DgvSup.Rows
                            Dim sb As New System.Text.StringBuilder
                            For Each c As DataGridViewCell In r.Cells
                                If sb.Length > 0 Then sb.Append(",")
                                sb.Append("""" & Convert.ToString(c.Value).Replace("""", """""""") & """")
                            Next
                            sw.WriteLine(sb.ToString())
                        Next
                    End Using
                    MessageBox.Show("Exported successfully.", "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Supplier Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

End Class
