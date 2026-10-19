Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' SuperMarket - Item (Product) Master.
'''   header  : tbl_StockDtl        (St_RecordNo_i identity, St_StockID_n = item code)
'''   rates   : tbl_MultiStock      (St_StNumber_i = stock line, one line per item)
''' All new SQL is parameterized. Uses VistawinFunctions.ServerConnection.
''' </summary>
Public Class FrmSmItem

    Dim Ds As New DataSet
    Dim Adapter As New SqlDataAdapter
    Dim Trans As SqlTransaction
    Dim EditMode As Boolean = False
    Dim EditCode As Integer = 0          ' St_RecordNo_i
    Dim EditStockID As Decimal = 0       ' St_StockID_n
    Dim EditStNumber As Decimal = 0      ' St_StNumber_i (tbl_MultiStock line)
    Dim dtCat As DataTable
    Dim suppressCat As Boolean = False

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

    Private Sub FrmSmItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VSuperMarket.EnsureConnection()
            LoadCombos()
            LoadGrid()
            txtItemName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCombos()
        ' Categories
        dtCat = New DataTable
        Using da As New SqlDataAdapter("SELECT Category_Code_n, Category_Name_vc, ISNULL(Cat_GSTPer_n,0) AS Cat_GSTPer_n FROM tbl_Category ORDER BY Category_Name_vc", VistawinFunctions.ServerConnection)
            da.Fill(dtCat)
        End Using
        Dim r0 As DataRow = dtCat.NewRow()
        r0("Category_Code_n") = 0
        r0("Category_Name_vc") = "-- Select --"
        r0("Cat_GSTPer_n") = 0
        dtCat.Rows.InsertAt(r0, 0)
        suppressCat = True
        cboCategory.DataSource = dtCat
        cboCategory.DisplayMember = "Category_Name_vc"
        cboCategory.ValueMember = "Category_Code_n"
        cboCategory.SelectedIndex = 0
        suppressCat = False

        ' Brands
        Dim dtB As New DataTable
        Using da As New SqlDataAdapter("SELECT Brand_Code_n, Brand_Name_vc FROM tbl_Brand ORDER BY Brand_Name_vc", VistawinFunctions.ServerConnection)
            da.Fill(dtB)
        End Using
        Dim rb As DataRow = dtB.NewRow()
        rb("Brand_Code_n") = 0
        rb("Brand_Name_vc") = "-- None --"
        dtB.Rows.InsertAt(rb, 0)
        cboBrand.DataSource = dtB
        cboBrand.DisplayMember = "Brand_Name_vc"
        cboBrand.ValueMember = "Brand_Code_n"
        cboBrand.SelectedIndex = 0

        ' Units
        Dim dtU As New DataTable
        Using da As New SqlDataAdapter("SELECT U_EName_vc FROM tbl_UnitList ORDER BY U_OrderNo_i", VistawinFunctions.ServerConnection)
            da.Fill(dtU)
        End Using
        cboUnit.DataSource = dtU
        cboUnit.DisplayMember = "U_EName_vc"
        cboUnit.SelectedIndex = -1
    End Sub

    Private Sub LoadGrid()
        Try
            VSuperMarket.EnsureConnection()
            Ds = New DataSet
            Dim sql As String = "SELECT s.St_RecordNo_i, s.St_StockID_n, s.St_ProName_vc, s.St_ProTamil_vc, " & _
                                "ISNULL(s.St_CatID_i,0) AS St_CatID_i, ISNULL(c.Category_Name_vc,'') AS CatName, " & _
                                "ISNULL(s.St_BrandID_i,0) AS St_BrandID_i, ISNULL(b.Brand_Name_vc,'') AS BrandName, " & _
                                "ISNULL(s.St_Unit_vc,'') AS St_Unit_vc, ISNULL(s.St_HSN_vc,'') AS St_HSN_vc, " & _
                                "ISNULL(s.St_CGSTPer_n,0) AS St_CGSTPer_n, ISNULL(s.St_SGSTPer_n,0) AS St_SGSTPer_n, " & _
                                "ISNULL(s.St_TotalSt_i,0) AS St_TotalSt_i, ISNULL(s.St_RemiStock_i,0) AS St_RemiStock_i, " & _
                                "ISNULL(s.St_Shelf_vc,'') AS St_Shelf_vc, " & _
                                "ISNULL(m.St_MRPRate_n,0) AS St_MRPRate_n, ISNULL(m.St_PurRate_n,0) AS St_PurRate_n, " & _
                                "ISNULL(m.St_SaleRate_n,0) AS St_SaleRate_n, ISNULL(m.St_Barcode_vc,'') AS St_Barcode_vc, " & _
                                "ISNULL(m.St_StNumber_i,0) AS St_StNumber_i, " & _
                                "CASE WHEN ISNULL(m.St_Show_vc,'YES')='YES' THEN 'YES' ELSE 'NO' END AS Active " & _
                                "FROM tbl_StockDtl s " & _
                                "LEFT JOIN tbl_Category c ON s.St_CatID_i = c.Category_Code_n " & _
                                "LEFT JOIN tbl_Brand b ON s.St_BrandID_i = b.Brand_Code_n " & _
                                "OUTER APPLY (SELECT TOP 1 * FROM tbl_MultiStock mm WHERE mm.St_StockID_n = s.St_StockID_n ORDER BY mm.St_StNumber_i) m "

            If txtSearch.Text.Trim <> "" Then
                Adapter = New SqlDataAdapter(sql & "WHERE s.St_ProName_vc LIKE @S OR s.St_ProTamil_vc LIKE @S OR CONVERT(varchar(30),s.St_StockID_n) LIKE @S OR ISNULL(m.St_Barcode_vc,'') LIKE @S ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
                Adapter.SelectCommand.Parameters.AddWithValue("@S", "%" & txtSearch.Text.Trim & "%")
            Else
                Adapter = New SqlDataAdapter(sql & "ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
            End If
            Adapter.Fill(Ds, "Filldata")

            DgvItem.Rows.Clear()
            Dim i As Integer = 0
            For Each Dr As DataRow In Ds.Tables("Filldata").Rows
                DgvItem.Rows.Add()
                DgvItem.Rows(i).Cells("SNo1").Value = i + 1
                DgvItem.Rows(i).Cells("Tick1").Value = "NO"
                DgvItem.Rows(i).Cells("St_RecordNo_i").Value = Dr("St_RecordNo_i")
                DgvItem.Rows(i).Cells("St_StockID_n").Value = Dr("St_StockID_n")
                DgvItem.Rows(i).Cells("St_ProName_vc").Value = Dr("St_ProName_vc")
                DgvItem.Rows(i).Cells("CatName").Value = Dr("CatName")
                DgvItem.Rows(i).Cells("BrandName").Value = Dr("BrandName")
                DgvItem.Rows(i).Cells("St_Unit_vc").Value = Dr("St_Unit_vc")
                DgvItem.Rows(i).Cells("St_MRPRate_n").Value = Format(Convert.ToDecimal(Dr("St_MRPRate_n")), "0.00")
                DgvItem.Rows(i).Cells("St_SaleRate_n").Value = Format(Convert.ToDecimal(Dr("St_SaleRate_n")), "0.00")
                DgvItem.Rows(i).Cells("St_TotalSt_i").Value = Format(Convert.ToDecimal(Dr("St_TotalSt_i")), "0.###")
                DgvItem.Rows(i).Cells("Active").Value = Dr("Active")
                i += 1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearFields()
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtTamilName.Text = ""
        txtHSN.Text = ""
        txtBarcode.Text = ""
        txtCGST.Text = ""
        txtSGST.Text = ""
        txtMRP.Text = ""
        txtPurRate.Text = ""
        txtSaleRate.Text = ""
        txtOpStock.Text = ""
        txtReorder.Text = ""
        txtRack.Text = ""
        chkShow.Checked = True
        suppressCat = True
        cboCategory.SelectedIndex = 0
        cboBrand.SelectedIndex = 0
        cboUnit.SelectedIndex = -1
        suppressCat = False
        EditMode = False
        EditCode = 0
        EditStockID = 0
        EditStNumber = 0
        txtItemName.Focus()
    End Sub

    Private Function GetDec(ByVal t As TextBox) As Decimal
        If t.Text.Trim = "" Then Return 0
        Dim v As Decimal = 0
        Decimal.TryParse(t.Text.Trim, v)
        Return v
    End Function

    Private Function SelInt(ByVal cbo As ComboBox) As Integer
        If cbo.SelectedValue Is Nothing Then Return 0
        Dim v As Integer = 0
        Integer.TryParse(cbo.SelectedValue.ToString(), v)
        Return v
    End Function

    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        If suppressCat Then Exit Sub
        Try
            Dim code As Integer = SelInt(cboCategory)
            Dim gst As Decimal = 0
            If dtCat IsNot Nothing AndAlso code > 0 Then
                For Each dr As DataRow In dtCat.Rows
                    If Convert.ToInt32(dr("Category_Code_n")) = code Then
                        gst = Convert.ToDecimal(dr("Cat_GSTPer_n"))
                        Exit For
                    End If
                Next
            End If
            txtCGST.Text = (gst / 2).ToString("0.##")
            txtSGST.Text = (gst / 2).ToString("0.##")
        Catch
        End Try
    End Sub

    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearFields()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If txtItemName.Text.Trim = "" Then
            MessageBox.Show("Enter Item Name.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemName.Focus()
            Exit Sub
        End If
        If SelInt(cboCategory) = 0 Then
            MessageBox.Show("Select a Category.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboCategory.Focus()
            Exit Sub
        End If
        If cboUnit.SelectedIndex < 0 Then
            MessageBox.Show("Select a Unit.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboUnit.Focus()
            Exit Sub
        End If
        If Not VSuperMarket.HasPermission("FrmSmItem", If(EditMode, "EDIT", "ADD")) Then
            MessageBox.Show("You do not have permission for this action.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim catId As Integer = SelInt(cboCategory)
        Dim brandId As Integer = SelInt(cboBrand)
        Dim unit As String = Convert.ToString(cboUnit.SelectedValue)
        Dim cgst As Decimal = GetDec(txtCGST)
        Dim sgst As Decimal = GetDec(txtSGST)
        Dim mrp As Decimal = GetDec(txtMRP)
        Dim pur As Decimal = GetDec(txtPurRate)
        Dim sale As Decimal = GetDec(txtSaleRate)
        Dim opstk As Decimal = GetDec(txtOpStock)
        Dim reorder As Decimal = GetDec(txtReorder)
        Dim show As String = If(chkShow.Checked, "YES", "NO")

        If sale > 0 AndAlso mrp > 0 AndAlso sale > mrp Then
            If MessageBox.Show("Sale Rate is greater than MRP. Continue?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub
        End If

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            ' duplicate check : same name + brand + category
            Dim dup As Integer
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_StockDtl WHERE St_ProName_vc=@N AND ISNULL(St_BrandID_i,0)=@B AND ISNULL(St_CatID_i,0)=@K AND St_RecordNo_i<>@R", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@N", txtItemName.Text.Trim)
                cmd.Parameters.AddWithValue("@B", brandId)
                cmd.Parameters.AddWithValue("@K", catId)
                cmd.Parameters.AddWithValue("@R", EditCode)
                dup = CInt(cmd.ExecuteScalar())
            End Using
            If dup > 0 Then
                MessageBox.Show("This Item already exists for the selected Brand / Category.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Trans.Rollback() : Trans.Dispose()
                txtItemName.Focus()
                Exit Sub
            End If

            ' barcode uniqueness (only when supplied)
            If txtBarcode.Text.Trim <> "" Then
                Dim bdup As Integer
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_MultiStock WHERE St_Barcode_vc=@B AND St_StockID_n<>@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@B", txtBarcode.Text.Trim)
                    cmd.Parameters.AddWithValue("@C", EditStockID)
                    bdup = CInt(cmd.ExecuteScalar())
                End Using
                If bdup > 0 Then
                    MessageBox.Show("This Barcode is already used by another item.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Trans.Rollback() : Trans.Dispose()
                    txtBarcode.Focus()
                    Exit Sub
                End If
            End If

            If EditMode Then
                Using cmd As New SqlCommand("UPDATE tbl_StockDtl SET St_ProName_vc=@N, St_ProTamil_vc=@TN, St_CatID_i=@K, St_BrandID_i=@B, " & _
                                            "St_Unit_vc=@U, St_HSN_vc=@H, St_CGSTPer_n=@CG, St_SGSTPer_n=@SG, St_IGSTPer_n=@IG, " & _
                                            "St_RemiStock_i=@RE, St_Shelf_vc=@RK, St_TotalSt_i=@QTY, St_GoTotalSt_i=@QTY2, St_Decrease_vc='YES' " & _
                                            "WHERE St_RecordNo_i=@R", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@N", txtItemName.Text.Trim)
                    cmd.Parameters.AddWithValue("@TN", txtTamilName.Text.Trim)
                    cmd.Parameters.AddWithValue("@K", catId)
                    cmd.Parameters.AddWithValue("@B", brandId)
                    cmd.Parameters.AddWithValue("@U", unit)
                    cmd.Parameters.AddWithValue("@H", txtHSN.Text.Trim)
                    cmd.Parameters.AddWithValue("@CG", cgst)
                    cmd.Parameters.AddWithValue("@SG", sgst)
                    cmd.Parameters.AddWithValue("@IG", cgst + sgst)
                    cmd.Parameters.AddWithValue("@RE", reorder)
                    cmd.Parameters.AddWithValue("@RK", txtRack.Text.Trim)
                    cmd.Parameters.AddWithValue("@QTY", opstk)
                    cmd.Parameters.AddWithValue("@QTY2", opstk)
                    cmd.Parameters.AddWithValue("@R", EditCode)
                    cmd.ExecuteNonQuery()
                End Using

                If EditStNumber > 0 Then
                    Using cmd As New SqlCommand("UPDATE tbl_MultiStock SET St_ProName_vc=@N, St_MRPRate_n=@MRP, St_PurRate_n=@PR, St_SaleRate_n=@SR, " & _
                                                "St_CurrStock_i=@QTY, St_Barcode_vc=@BAR, St_Show_vc=@SH, St_UpDate_d=GETDATE(), St_UID_vc=@U " & _
                                                "WHERE St_StNumber_i=@SN", VistawinFunctions.ServerConnection, Trans)
                        cmd.Parameters.AddWithValue("@N", txtItemName.Text.Trim)
                        cmd.Parameters.AddWithValue("@MRP", mrp)
                        cmd.Parameters.AddWithValue("@PR", pur)
                        cmd.Parameters.AddWithValue("@SR", sale)
                        cmd.Parameters.AddWithValue("@QTY", opstk)
                        cmd.Parameters.AddWithValue("@BAR", txtBarcode.Text.Trim)
                        cmd.Parameters.AddWithValue("@SH", show)
                        cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
                        cmd.Parameters.AddWithValue("@SN", EditStNumber)
                        cmd.ExecuteNonQuery()
                    End Using
                Else
                    Dim newSN As Decimal
                    Using cmd As New SqlCommand("SELECT ISNULL(MAX(CONVERT(NUMERIC(18,0),St_StNumber_i)),0)+1 FROM tbl_MultiStock WITH (UPDLOCK,HOLDLOCK)", VistawinFunctions.ServerConnection, Trans)
                        newSN = Convert.ToDecimal(cmd.ExecuteScalar())
                    End Using
                    Using cmd As New SqlCommand("INSERT INTO tbl_MultiStock (St_StNumber_i,St_StockID_n,St_ProName_vc,St_MRPRate_n,St_PurRate_n,St_SaleRate_n,St_CurrStock_i,St_Barcode_vc,St_Show_vc,St_UpDate_d,St_UID_vc) " & _
                                                "VALUES (@SN,@C,@N,@MRP,@PR,@SR,@QTY,@BAR,@SH,GETDATE(),@U)", VistawinFunctions.ServerConnection, Trans)
                        cmd.Parameters.AddWithValue("@SN", newSN)
                        cmd.Parameters.AddWithValue("@C", EditStockID)
                        cmd.Parameters.AddWithValue("@N", txtItemName.Text.Trim)
                        cmd.Parameters.AddWithValue("@MRP", mrp)
                        cmd.Parameters.AddWithValue("@PR", pur)
                        cmd.Parameters.AddWithValue("@SR", sale)
                        cmd.Parameters.AddWithValue("@QTY", opstk)
                        cmd.Parameters.AddWithValue("@BAR", txtBarcode.Text.Trim)
                        cmd.Parameters.AddWithValue("@SH", show)
                        cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
                        cmd.ExecuteNonQuery()
                    End Using
                End If
                VSuperMarket.LogActivity("FrmSmItem", "EDIT", "Item " & txtItemName.Text.Trim)
            Else
                Dim newCode As Decimal
                Using cmd As New SqlCommand("SELECT ISNULL(MAX(CONVERT(NUMERIC(18,0),St_StockID_n)),0)+1 FROM tbl_StockDtl WITH (UPDLOCK,HOLDLOCK)", VistawinFunctions.ServerConnection, Trans)
                    newCode = Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
                Using cmd As New SqlCommand("INSERT INTO tbl_StockDtl (St_StockID_n,St_ProName_vc,St_ProTamil_vc,St_CatID_i,St_BrandID_i,St_Unit_vc,St_HSN_vc,St_CGSTPer_n,St_SGSTPer_n,St_IGSTPer_n,St_RemiStock_i,St_Shelf_vc,St_TotalSt_i,St_GoTotalSt_i,St_Decrease_vc,St_EntDate_d) " & _
                                            "VALUES (@C,@N,@TN,@K,@B,@U,@H,@CG,@SG,@IG,@RE,@RK,@QTY,@QTY2,'YES',GETDATE())", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", newCode)
                    cmd.Parameters.AddWithValue("@N", txtItemName.Text.Trim)
                    cmd.Parameters.AddWithValue("@TN", txtTamilName.Text.Trim)
                    cmd.Parameters.AddWithValue("@K", catId)
                    cmd.Parameters.AddWithValue("@B", brandId)
                    cmd.Parameters.AddWithValue("@U", unit)
                    cmd.Parameters.AddWithValue("@H", txtHSN.Text.Trim)
                    cmd.Parameters.AddWithValue("@CG", cgst)
                    cmd.Parameters.AddWithValue("@SG", sgst)
                    cmd.Parameters.AddWithValue("@IG", cgst + sgst)
                    cmd.Parameters.AddWithValue("@RE", reorder)
                    cmd.Parameters.AddWithValue("@RK", txtRack.Text.Trim)
                    cmd.Parameters.AddWithValue("@QTY", opstk)
                    cmd.Parameters.AddWithValue("@QTY2", opstk)
                    cmd.ExecuteNonQuery()
                End Using

                Dim newSN As Decimal
                Using cmd As New SqlCommand("SELECT ISNULL(MAX(CONVERT(NUMERIC(18,0),St_StNumber_i)),0)+1 FROM tbl_MultiStock WITH (UPDLOCK,HOLDLOCK)", VistawinFunctions.ServerConnection, Trans)
                    newSN = Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
                Using cmd As New SqlCommand("INSERT INTO tbl_MultiStock (St_StNumber_i,St_StockID_n,St_ProName_vc,St_MRPRate_n,St_PurRate_n,St_SaleRate_n,St_CurrStock_i,St_Barcode_vc,St_Show_vc,St_UpDate_d,St_UID_vc) " & _
                                            "VALUES (@SN,@C,@N,@MRP,@PR,@SR,@QTY,@BAR,@SH,GETDATE(),@U)", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@SN", newSN)
                    cmd.Parameters.AddWithValue("@C", newCode)
                    cmd.Parameters.AddWithValue("@N", txtItemName.Text.Trim)
                    cmd.Parameters.AddWithValue("@MRP", mrp)
                    cmd.Parameters.AddWithValue("@PR", pur)
                    cmd.Parameters.AddWithValue("@SR", sale)
                    cmd.Parameters.AddWithValue("@QTY", opstk)
                    cmd.Parameters.AddWithValue("@BAR", txtBarcode.Text.Trim)
                    cmd.Parameters.AddWithValue("@SH", show)
                    cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmItem", "ADD", "Item " & txtItemName.Text.Trim)
            End If

            Trans.Commit()
            MessageBox.Show("Saved Successfully.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    Private Sub DgvItem_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvItem.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If DgvItem.Columns(e.ColumnIndex).Name = "Tick1" Then
            For Each Dr As DataGridViewRow In DgvItem.Rows
                Dr.Cells("Tick1").Value = "NO"
            Next
            DgvItem.Rows(e.RowIndex).Cells("Tick1").Value = "YES"
        Else
            LoadRowIntoFields(e.RowIndex)
        End If
    End Sub

    Private Sub LoadRowIntoFields(ByVal rowIndex As Integer)
        Dim r As DataGridViewRow = DgvItem.Rows(rowIndex)
        Dim sql As String = "SELECT s.St_RecordNo_i, s.St_StockID_n, s.St_ProName_vc, s.St_ProTamil_vc, ISNULL(s.St_CatID_i,0) AS St_CatID_i, " & _
                            "ISNULL(s.St_BrandID_i,0) AS St_BrandID_i, ISNULL(s.St_Unit_vc,'') AS St_Unit_vc, ISNULL(s.St_HSN_vc,'') AS St_HSN_vc, " & _
                            "ISNULL(s.St_CGSTPer_n,0) AS St_CGSTPer_n, ISNULL(s.St_SGSTPer_n,0) AS St_SGSTPer_n, ISNULL(s.St_RemiStock_i,0) AS St_RemiStock_i, " & _
                            "ISNULL(s.St_Shelf_vc,'') AS St_Shelf_vc, ISNULL(s.St_TotalSt_i,0) AS St_TotalSt_i, " & _
                            "ISNULL(m.St_MRPRate_n,0) AS St_MRPRate_n, ISNULL(m.St_PurRate_n,0) AS St_PurRate_n, ISNULL(m.St_SaleRate_n,0) AS St_SaleRate_n, " & _
                            "ISNULL(m.St_Barcode_vc,'') AS St_Barcode_vc, ISNULL(m.St_StNumber_i,0) AS St_StNumber_i, ISNULL(m.St_Show_vc,'YES') AS St_Show_vc " & _
                            "FROM tbl_StockDtl s OUTER APPLY (SELECT TOP 1 * FROM tbl_MultiStock mm WHERE mm.St_StockID_n = s.St_StockID_n ORDER BY mm.St_StNumber_i) m " & _
                            "WHERE s.St_RecordNo_i=@R"
        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
            cmd.Parameters.AddWithValue("@R", CInt(r.Cells("St_RecordNo_i").Value))
            Using dr As SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    EditCode = Convert.ToInt32(dr("St_RecordNo_i"))
                    EditStockID = Convert.ToDecimal(dr("St_StockID_n"))
                    txtItemCode.Text = dr("St_StockID_n").ToString()
                    txtItemName.Text = Convert.ToString(dr("St_ProName_vc"))
                    txtTamilName.Text = Convert.ToString(dr("St_ProTamil_vc"))
                    txtHSN.Text = Convert.ToString(dr("St_HSN_vc"))
                    txtBarcode.Text = Convert.ToString(dr("St_Barcode_vc"))
                    txtCGST.Text = Convert.ToDecimal(dr("St_CGSTPer_n")).ToString("0.##")
                    txtSGST.Text = Convert.ToDecimal(dr("St_SGSTPer_n")).ToString("0.##")
                    txtMRP.Text = Convert.ToDecimal(dr("St_MRPRate_n")).ToString("0.##")
                    txtPurRate.Text = Convert.ToDecimal(dr("St_PurRate_n")).ToString("0.##")
                    txtSaleRate.Text = Convert.ToDecimal(dr("St_SaleRate_n")).ToString("0.##")
                    txtOpStock.Text = Convert.ToDecimal(dr("St_TotalSt_i")).ToString("0.###")
                    txtReorder.Text = Convert.ToDecimal(dr("St_RemiStock_i")).ToString("0.###")
                    txtRack.Text = Convert.ToString(dr("St_Shelf_vc"))
                    EditStNumber = Convert.ToDecimal(dr("St_StNumber_i"))
                    chkShow.Checked = (Convert.ToString(dr("St_Show_vc")).ToUpper = "YES")

                    suppressCat = True
                    cboCategory.SelectedValue = Convert.ToInt32(dr("St_CatID_i"))
                    cboBrand.SelectedValue = Convert.ToInt32(dr("St_BrandID_i"))
                    cboUnit.SelectedValue = Convert.ToString(dr("St_Unit_vc"))
                    suppressCat = False
                End If
            End Using
        End Using
        EditMode = True
        txtItemName.Focus()
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        For Each Dr As DataGridViewRow In DgvItem.Rows
            If Convert.ToString(Dr.Cells("Tick1").Value) = "YES" Then
                LoadRowIntoFields(Dr.Index)
                Exit Sub
            End If
        Next
        MessageBox.Show("Select a row first.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not VSuperMarket.HasPermission("FrmSmItem", "DELETE") Then
            MessageBox.Show("You do not have permission to delete.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim ticked As New System.Collections.Generic.List(Of Integer)
        For Each Dr As DataGridViewRow In DgvItem.Rows
            If Convert.ToString(Dr.Cells("Tick1").Value) = "YES" Then ticked.Add(CInt(Dr.Cells("St_RecordNo_i").Value))
        Next
        If ticked.Count = 0 Then
            MessageBox.Show("Select at least one row to delete.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If MessageBox.Show("Are you sure to delete the selected item(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            For Each rec As Integer In ticked
                Dim code As Decimal
                Using cmd As New SqlCommand("SELECT ISNULL(St_StockID_n,0) FROM tbl_StockDtl WHERE St_RecordNo_i=@R", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@R", rec)
                    code = Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
                Dim used As Integer
                Using cmd As New SqlCommand("SELECT (SELECT COUNT(*) FROM tbl_SalParticular WHERE Sa_StCode_n=@C) + (SELECT COUNT(*) FROM tbl_PurchParticular WHERE Sa_StCode_n=@C)", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", Convert.ToInt32(code))
                    used = CInt(cmd.ExecuteScalar())
                End Using
                If used > 0 Then
                    MessageBox.Show("Item code " & code.ToString() & " is used in transactions and cannot be deleted.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Trans.Rollback() : Trans.Dispose()
                    Exit Sub
                End If
                Using cmd As New SqlCommand("DELETE FROM tbl_MultiStock WHERE St_StockID_n=@C", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@C", code)
                    cmd.ExecuteNonQuery()
                End Using
                Using cmd As New SqlCommand("DELETE FROM tbl_StockDtl WHERE St_RecordNo_i=@R", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@R", rec)
                    cmd.ExecuteNonQuery()
                End Using
                VSuperMarket.LogActivity("FrmSmItem", "DELETE", "Item id " & rec.ToString())
            Next
            Trans.Commit()
            MessageBox.Show("Deleted Successfully.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadGrid()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        LoadGrid()
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            ScanOrSearch()
            e.SuppressKeyPress = True
        End If
    End Sub

    ''' <summary>
    ''' The Search box doubles as a barcode box (same idea as the POS / Stock
    ''' "Barcode" fields).  A scanner types the digits then sends Enter, so on an
    ''' exact barcode / item code match the item is loaded straight into the
    ''' fields, ready to edit.  Anything else is the usual name / code search.
    ''' </summary>
    Private Sub ScanOrSearch()
        Try
            Dim t As String = txtSearch.Text.Trim()
            LoadGrid()
            If t = "" Then Exit Sub

            Dim recNo As Integer = 0
            Using cmd As New SqlCommand("SELECT TOP 1 ISNULL(s.St_RecordNo_i,0) FROM tbl_StockDtl s " & _
                                        "OUTER APPLY (SELECT TOP 1 * FROM tbl_MultiStock mm WHERE mm.St_StockID_n = s.St_StockID_n ORDER BY mm.St_StNumber_i) m " & _
                                        "WHERE ISNULL(m.St_Barcode_vc,'') = @B OR CONVERT(varchar(30),s.St_StockID_n) = @B", VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@B", t)
                Dim o As Object = cmd.ExecuteScalar()
                If o IsNot Nothing AndAlso Not Convert.IsDBNull(o) Then recNo = Convert.ToInt32(o)
            End Using
            If recNo <= 0 Then Exit Sub

            For i As Integer = 0 To DgvItem.Rows.Count - 1
                If Convert.ToInt32(DgvItem.Rows(i).Cells("St_RecordNo_i").Value) = recNo Then
                    For Each Dr As DataGridViewRow In DgvItem.Rows
                        Dr.Cells("Tick1").Value = "NO"
                    Next
                    DgvItem.Rows(i).Cells("Tick1").Value = "YES"
                    DgvItem.CurrentCell = DgvItem.Rows(i).Cells("St_ProName_vc")
                    LoadRowIntoFields(i)
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        txtSearch.Text = ""
        ClearFields()
        LoadGrid()
    End Sub

    Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        Try
            If DgvItem.Rows.Count = 0 Then
                MessageBox.Show("Nothing to export.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Using sfd As New SaveFileDialog
                sfd.Filter = "CSV file (*.csv)|*.csv"
                sfd.FileName = "ItemMaster_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"
                If sfd.ShowDialog() = DialogResult.OK Then
                    Using sw As New StreamWriter(sfd.FileName, False)
                        Dim heads As New System.Text.StringBuilder
                        For Each col As DataGridViewColumn In DgvItem.Columns
                            If heads.Length > 0 Then heads.Append(",")
                            heads.Append("""" & col.HeaderText & """")
                        Next
                        sw.WriteLine(heads.ToString())
                        For Each r As DataGridViewRow In DgvItem.Rows
                            Dim sb As New System.Text.StringBuilder
                            For Each c As DataGridViewCell In r.Cells
                                If sb.Length > 0 Then sb.Append(",")
                                sb.Append("""" & Convert.ToString(c.Value).Replace("""", """""") & """")
                            Next
                            sw.WriteLine(sb.ToString())
                        Next
                    End Using
                    MessageBox.Show("Exported successfully.", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

End Class
