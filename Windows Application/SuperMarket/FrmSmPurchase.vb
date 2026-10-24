Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

''' <summary>
''' SuperMarket - Purchase Entry (PURCHASE / PURCHASE RETURN).
'''   Header  -> tbl_PurchDetail
'''   Lines   -> tbl_PurchParticular
'''   Bill no -> tbl_BillSeries + MAX(Sa_BillNo_n)+1 per save type + FY
'''   Stock   -> tbl_MultiStock.St_CurrStock_i / tbl_StockDtl.St_TotalSt_i
''' Purchase GST is EXCLUSIVE : taxable + CGST + SGST = line amount.
''' All SQL is parameterized and the whole save runs in one transaction.
''' </summary>
Public Class FrmSmPurchase

    Dim dtItems As New DataTable
    Dim dtSupp As New DataTable
    Dim dictDisplay As New Dictionary(Of String, DataRow)
    Dim dictBarcode As New Dictionary(Of String, DataRow)
    Dim dictCode As New Dictionary(Of String, DataRow)
    Dim dictSupp As New Dictionary(Of String, DataRow)
    Dim Trans As SqlTransaction
    Dim SavedBill As Boolean = False
    Dim LastInvoice As String = ""

    ' printing
    Dim printLines As New List(Of String)
    Dim printIndex As Integer = 0

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                If DgvPurchase.Rows.Count > 0 Then
                    If MessageBox.Show("Close without saving the current purchase?", "Purchase Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return True
                End If
                Me.Close()
                Return True
            ElseIf msg.WParam.ToInt32 = CInt(Keys.F2) Then
                BtnSave.PerformClick()
                Return True
            ElseIf msg.WParam.ToInt32 = CInt(Keys.F4) Then
                cboItem.Focus()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FrmSmPurchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VSuperMarket.EnsureConnection()
            cboBillType.Items.Clear()
            cboBillType.Items.AddRange(New Object() {"PURCHASE", "PURCHASE RETURN"})
            cboBillType.SelectedIndex = 0
            LoadSuppliers()
            LoadItems()
            ResetBill()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==================================================================================
    '  data loading
    ' ==================================================================================
    Private Sub LoadSuppliers()
        dtSupp = New DataTable
        Using cmd As New SqlCommand("SELECT C_ID_n, C_Name_vc, ISNULL(C_Mobile_vc,'') AS Mobile FROM tbl_CusProfile " & _
                                    "WHERE C_Type_vc='SUPPLIER' AND ISNULL(C_Show_vc,'YES')='YES' ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dtSupp)
            End Using
        End Using
        cboSupplier.Items.Clear()
        dictSupp.Clear()
        For Each r As DataRow In dtSupp.Rows
            Dim disp As String = Convert.ToString(r("C_ID_n")) & " - " & Convert.ToString(r("C_Name_vc"))
            cboSupplier.Items.Add(disp)
            dictSupp(disp.ToUpper()) = r
        Next
        If cboSupplier.Items.Count > 0 Then cboSupplier.SelectedIndex = 0
    End Sub

    Private Sub LoadItems()
        dtItems = New DataTable
        Using cmd As New SqlCommand("SELECT m.St_StNumber_i, m.St_StockID_n, s.St_ProName_vc, ISNULL(s.St_Unit_vc,'') AS Unit, " & _
                                    "ISNULL(s.St_HSN_vc,'') AS HSN, ISNULL(m.St_MRPRate_n,0) AS MRP, ISNULL(m.St_SaleRate_n,0) AS SaleRate, " & _
                                    "ISNULL(m.St_PurRate_n,0) AS PurRate, ISNULL(m.St_CurrStock_i,0) AS Stock, ISNULL(m.St_Barcode_vc,'') AS Barcode, " & _
                                    "ISNULL(s.St_CGSTPer_n,0) AS CGST, ISNULL(s.St_SGSTPer_n,0) AS SGST " & _
                                    "FROM tbl_StockDtl s INNER JOIN tbl_MultiStock m ON m.St_StockID_n = s.St_StockID_n " & _
                                    "WHERE ISNULL(m.St_Show_vc,'YES')='YES' ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dtItems)
            End Using
        End Using
        cboItem.Items.Clear()
        dictDisplay.Clear() : dictBarcode.Clear() : dictCode.Clear()
        For Each r As DataRow In dtItems.Rows
            Dim disp As String = Convert.ToString(r("St_StockID_n")) & " - " & Convert.ToString(r("St_ProName_vc"))
            cboItem.Items.Add(disp)
            dictDisplay(disp.ToUpper()) = r
            Dim bc As String = Convert.ToString(r("Barcode")).Trim()
            If bc <> "" AndAlso Not dictBarcode.ContainsKey(bc.ToUpper()) Then dictBarcode(bc.ToUpper()) = r
            Dim cd As String = Convert.ToString(r("St_StockID_n")).Trim()
            If cd <> "" AndAlso Not dictCode.ContainsKey(cd) Then dictCode(cd) = r
        Next
    End Sub

    ' ==================================================================================
    '  item entry
    ' ==================================================================================
    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        TryAddFromItemBox()
    End Sub

    Private Sub cboItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItem.KeyDown
        If e.KeyCode = Keys.Enter Then
            TryAddFromItemBox()
            e.SuppressKeyPress = True
        End If
    End Sub

    ' fills rate / MRP / GST / stock as soon as an item is picked from the list
    Private Sub cboItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboItem.SelectedIndexChanged
        Try
            If cboItem.SelectedIndex < 0 Then Exit Sub
            Dim r As DataRow = ResolveItem(Convert.ToString(cboItem.SelectedItem))
            If r IsNot Nothing Then PrefillFromItem(r)
        Catch
        End Try
    End Sub

    Private Sub qty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyDown, txtPurRate.KeyDown, txtDisc.KeyDown
        If e.KeyCode = Keys.Enter Then
            TryAddFromItemBox()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub PrefillFromItem(ByVal r As DataRow)
        txtPurRate.Text = DecOf(r("PurRate")).ToString("0.00")
        txtMRP.Text = DecOf(r("MRP")).ToString("0.00")
        txtSaleRate.Text = DecOf(r("SaleRate")).ToString("0.00")
        lblGST.Text = (DecOf(r("CGST")) + DecOf(r("SGST"))).ToString("0.##")
        lblStock.Text = "Stock: " & DecOf(r("Stock")).ToString("0.###")
    End Sub

    Private Sub TryAddFromItemBox()
        If cboItem.Text.Trim = "" Then Exit Sub
        Dim r As DataRow = ResolveItem(cboItem.Text.Trim())
        If r Is Nothing Then
            MessageBox.Show("Item not found.", "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboItem.SelectAll() : cboItem.Focus()
            Exit Sub
        End If
        If txtPurRate.Text.Trim = "" Then txtPurRate.Text = DecOf(r("PurRate")).ToString("0.00")
        If txtMRP.Text.Trim = "" Then txtMRP.Text = DecOf(r("MRP")).ToString("0.00")
        If txtSaleRate.Text.Trim = "" Then txtSaleRate.Text = DecOf(r("SaleRate")).ToString("0.00")
        lblGST.Text = (DecOf(r("CGST")) + DecOf(r("SGST"))).ToString("0.##")
        lblStock.Text = "Stock: " & DecOf(r("Stock")).ToString("0.###")
        AddItemToCart(r)
    End Sub

    Private Function ResolveItem(ByVal txt As String) As DataRow
        Dim key As String = txt.ToUpper()
        ' a scanned barcode is exact, so check it first - the combo's
        ' auto-complete must never be able to turn a scan into another item
        If dictBarcode.ContainsKey(key) Then Return dictBarcode(key)
        If dictDisplay.ContainsKey(key) Then Return dictDisplay(key)
        If dictCode.ContainsKey(key) Then Return dictCode(key)
        Dim sp As Integer = txt.IndexOf(" "c)
        If sp > 0 Then
            Dim head As String = txt.Substring(0, sp).Trim().ToUpper()
            If dictBarcode.ContainsKey(head) Then Return dictBarcode(head)
            If dictCode.ContainsKey(head) Then Return dictCode(head)
        End If
        Dim found As DataRow = Nothing
        Dim count As Integer = 0
        For Each r As DataRow In dtItems.Rows
            If Convert.ToString(r("St_ProName_vc")).ToUpper().StartsWith(key) Then
                found = r : count += 1
                If count > 1 Then Exit For
            End If
        Next
        If count = 1 Then Return found
        Return Nothing
    End Function

    Private Sub AddItemToCart(ByVal r As DataRow)
        Dim stNo As Decimal = DecOf(r("St_StNumber_i"))
        Dim code As String = Convert.ToString(r("St_StockID_n"))
        Dim qty As Decimal = DecOf(txtQty.Text)
        If qty <= 0 Then qty = 1
        Dim purRate As Decimal = DecOf(txtPurRate.Text)

        ' same item + same purchase rate already on the bill -> just add qty
        For Each gr As DataGridViewRow In DgvPurchase.Rows
            If Convert.ToString(gr.Cells("StNo").Value) = Convert.ToString(stNo) AndAlso DecOf(gr.Cells("PurRate").Value) = purRate Then
                gr.Cells("Qty").Value = DecOf(gr.Cells("Qty").Value) + qty
                RecalcRow(gr)
                RecalcTotals()
                ResetEntry()
                Exit Sub
            End If
        Next

        Dim idx As Integer = DgvPurchase.Rows.Add()
        Dim row As DataGridViewRow = DgvPurchase.Rows(idx)
        row.Cells("SNoB").Value = idx + 1
        row.Cells("Code").Value = code
        row.Cells("StNo").Value = stNo.ToString()
        row.Cells("ItemName").Value = Convert.ToString(r("St_ProName_vc"))
        row.Cells("Unit").Value = Convert.ToString(r("Unit"))
        row.Cells("HSN").Value = Convert.ToString(r("HSN"))
        row.Cells("PurRate").Value = purRate.ToString("0.00")
        row.Cells("MRP").Value = DecOf(txtMRP.Text).ToString("0.00")
        row.Cells("SaleRate").Value = DecOf(txtSaleRate.Text).ToString("0.00")
        row.Cells("Qty").Value = qty.ToString("0.###")
        row.Cells("DisPer").Value = DecOf(txtDisc.Text).ToString("0.##")
        row.Cells("GSTPer").Value = (DecOf(r("CGST")) + DecOf(r("SGST"))).ToString("0.##")
        row.Cells("Remark").Value = ""
        RecalcRow(row)
        RecalcTotals()
        ResetEntry()
    End Sub

    Private Function DecOf(ByVal o As Object) As Decimal
        If o Is Nothing OrElse Convert.IsDBNull(o) Then Return 0
        Dim d As Decimal = 0
        Decimal.TryParse(Convert.ToString(o), d)
        Return d
    End Function

    Private Sub ResetEntry()
        cboItem.Text = ""
        txtQty.Text = "1"
        txtPurRate.Text = ""
        txtMRP.Text = ""
        txtSaleRate.Text = ""
        txtDisc.Text = "0"
        lblGST.Text = "0.00"
        lblStock.Text = ""
        cboItem.Focus()
    End Sub

    ' ==================================================================================
    '  cart maths (GST exclusive)
    ' ==================================================================================
    Private Sub RecalcRow(ByVal r As DataGridViewRow)
        If r Is Nothing Then Exit Sub
        Dim rate As Decimal = DecOf(r.Cells("PurRate").Value)
        Dim qty As Decimal = DecOf(r.Cells("Qty").Value)
        Dim disPer As Decimal = DecOf(r.Cells("DisPer").Value)
        Dim gst As Decimal = DecOf(r.Cells("GSTPer").Value)

        Dim gross As Decimal = rate * qty
        Dim dis As Decimal = Math.Round(gross * disPer / 100, 2)
        Dim taxable As Decimal = gross - dis
        Dim cgst As Decimal = Math.Round(taxable * (gst / 2) / 100, 2)
        Dim sgst As Decimal = Math.Round(taxable * (gst / 2) / 100, 2)

        r.Cells("DisAmt").Value = dis.ToString("0.00")
        r.Cells("Taxable").Value = taxable.ToString("0.00")
        r.Cells("CGSTAmt").Value = cgst.ToString("0.00")
        r.Cells("SGSTAmt").Value = sgst.ToString("0.00")
        r.Cells("Amount").Value = (taxable + cgst + sgst).ToString("0.00")
    End Sub

    Private Sub RecalcTotals()
        Dim subTotal As Decimal = 0
        Dim discount As Decimal = 0
        Dim taxable As Decimal = 0
        Dim cgst As Decimal = 0
        Dim sgst As Decimal = 0
        Dim billAmt As Decimal = 0
        Dim totQty As Decimal = 0

        For Each r As DataGridViewRow In DgvPurchase.Rows
            Dim rate As Decimal = DecOf(r.Cells("PurRate").Value)
            Dim qty As Decimal = DecOf(r.Cells("Qty").Value)
            Dim disPer As Decimal = DecOf(r.Cells("DisPer").Value)
            Dim gst As Decimal = DecOf(r.Cells("GSTPer").Value)
            Dim gross As Decimal = rate * qty
            Dim dis As Decimal = Math.Round(gross * disPer / 100, 2)
            Dim tx As Decimal = gross - dis

            subTotal += gross
            discount += dis
            taxable += tx
            cgst += Math.Round(tx * (gst / 2) / 100, 2)
            sgst += Math.Round(tx * (gst / 2) / 100, 2)
            totQty += qty
        Next

        billAmt = taxable + cgst + sgst
        Dim finalAmt As Decimal = VSuperMarket.RoundOff(billAmt)
        Dim roundOff As Decimal = finalAmt - billAmt

        txtSubTotal.Text = subTotal.ToString("0.00")
        txtDiscount.Text = discount.ToString("0.00")
        txtTaxable.Text = taxable.ToString("0.00")
        txtCGST.Text = cgst.ToString("0.00")
        txtSGST.Text = sgst.ToString("0.00")
        txtRoundOff.Text = roundOff.ToString("0.00")
        txtBillAmount.Text = finalAmt.ToString("0.00")
        lblItemCount.Text = DgvPurchase.Rows.Count.ToString()
        lblTotalQty.Text = totQty.ToString("0.###")
    End Sub

    Private Sub DgvPurchase_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvPurchase.CellEndEdit
        If e.RowIndex < 0 Then Exit Sub
        Try
            Dim r As DataGridViewRow = DgvPurchase.Rows(e.RowIndex)
            Dim name As String = DgvPurchase.Columns(e.ColumnIndex).Name
            If name = "Qty" Then
                If DecOf(r.Cells("Qty").Value) <= 0 Then r.Cells("Qty").Value = "1"
            ElseIf name = "DisPer" Then
                Dim d As Decimal = DecOf(r.Cells("DisPer").Value)
                If d < 0 Then d = 0
                If d > 100 Then d = 100
                r.Cells("DisPer").Value = d.ToString("0.##")
            End If
            RecalcRow(r)
            RecalcTotals()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvPurchase_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DgvPurchase.KeyDown
        If e.KeyCode = Keys.Delete AndAlso Not DgvPurchase.IsCurrentCellInEditMode Then
            DeleteCurrentRow()
            e.Handled = True
        End If
    End Sub

    ' ==================================================================================
    '  header / bill number
    ' ==================================================================================
    Private Function BillType() As String
        Return Convert.ToString(cboBillType.SelectedItem)
    End Function

    Private Sub cboBillType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBillType.SelectedIndexChanged
        If cboBillType.SelectedIndex < 0 Then Exit Sub
        txtBillNo.Text = PreviewBillNo()
    End Sub

    Private Function PreviewBillNo() As String
        Try
            Dim t As String = BillType()
            Dim fy As String = VSuperMarket.CurrentFY
            Dim series As String = ""
            Using cmd As New SqlCommand("SELECT TOP 1 ISNULL(B_BillSeries_vc,'') FROM tbl_BillSeries WHERE B_BillType_vc=@T AND B_AcYear_vc=@FY", VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@T", t)
                cmd.Parameters.AddWithValue("@FY", fy)
                series = Convert.ToString(cmd.ExecuteScalar())
            End Using
            Dim n As Decimal = 1D
            Using cmd As New SqlCommand("SELECT ISNULL(MAX(CONVERT(NUMERIC(18,0),Sa_BillNo_n)),0)+1 FROM tbl_PurchDetail WHERE Sa_SaveType_vc=@T AND Sa_AcYear_vc=@FY", VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@T", t)
                cmd.Parameters.AddWithValue("@FY", fy)
                n = Convert.ToDecimal(cmd.ExecuteScalar())
            End Using
            Return series & n.ToString("0") & " (next)"
        Catch
            Return ""
        End Try
    End Function

    Private Sub cboSupplier_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSupplier.SelectedIndexChanged
        Dim r As DataRow = ResolveSupplier()
        If r IsNot Nothing Then
            txtSuppName.Text = Convert.ToString(r("C_Name_vc"))
            txtSuppMobile.Text = Convert.ToString(r("Mobile"))
        End If
    End Sub

    Private Function ResolveSupplier() As DataRow
        Dim txt As String = cboSupplier.Text.Trim()
        If txt = "" Then Return Nothing
        If dictSupp.ContainsKey(txt.ToUpper()) Then Return dictSupp(txt.ToUpper())
        If cboSupplier.SelectedItem IsNot Nothing Then
            Dim sel As String = Convert.ToString(cboSupplier.SelectedItem)
            If dictSupp.ContainsKey(sel.ToUpper()) Then Return dictSupp(sel.ToUpper())
        End If
        Return Nothing
    End Function

    ' ==================================================================================
    '  buttons
    ' ==================================================================================
    Private Sub BtnDeleteRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeleteRow.Click
        DeleteCurrentRow()
    End Sub

    Private Sub DeleteCurrentRow()
        If DgvPurchase.CurrentRow Is Nothing Then
            MessageBox.Show("Select a row to delete.", "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        DgvPurchase.Rows.Remove(DgvPurchase.CurrentRow)
        RenumberRows()
        RecalcTotals()
    End Sub

    Private Sub RenumberRows()
        For i As Integer = 0 To DgvPurchase.Rows.Count - 1
            DgvPurchase.Rows(i).Cells("SNoB").Value = i + 1
        Next
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If DgvPurchase.Rows.Count > 0 Then
            If MessageBox.Show("Clear the current purchase?", "Purchase Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub
        End If
        ResetBill()
    End Sub

    Private Sub ResetBill()
        DgvPurchase.Rows.Clear()
        RecalcTotals()
        ResetEntry()
        dtpBillDate.Value = Date.Today
        SavedBill = False
        LastInvoice = ""
        txtBillNo.Text = PreviewBillNo()
        If cboSupplier.Items.Count > 0 AndAlso cboSupplier.SelectedIndex < 0 Then cboSupplier.SelectedIndex = 0
        cboItem.Focus()
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    ' ==================================================================================
    '  save
    ' ==================================================================================
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If DgvPurchase.Rows.Count = 0 Then
            MessageBox.Show("No items in the purchase.", "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Not VSuperMarket.HasPermission("FrmSmPurchase", "ADD") Then
            MessageBox.Show("You do not have permission to save purchases.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim supRow As DataRow = ResolveSupplier()
        If supRow Is Nothing Then
            MessageBox.Show("Please select a supplier.", "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboSupplier.Focus()
            Exit Sub
        End If

        Dim bt As String = BillType()
        Dim isReturn As Boolean = String.Equals(bt, "PURCHASE RETURN", StringComparison.OrdinalIgnoreCase)
        Dim supId As Integer = Convert.ToInt32(supRow("C_ID_n"))
        Dim supName As String = Convert.ToString(supRow("C_Name_vc"))

        Dim mrpTotal As Decimal = 0
        For Each r As DataGridViewRow In DgvPurchase.Rows
            mrpTotal += DecOf(r.Cells("MRP").Value) * DecOf(r.Cells("Qty").Value)
        Next

        SmSyncQueue.EnsurePrepared()   ' probe once, outside the transaction
        Trans = VistawinFunctions.ServerConnection.BeginTransaction()
        Try
            Dim bn As VSuperMarket.BillNumber = VSuperMarket.NextBillNumber(Trans, bt, "tbl_PurchDetail")

            Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_PurchDetail WHERE Sa_SaveType_vc=@T AND Sa_AcYear_vc=@FY AND Sa_BillNo_n=@N", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@T", bt)
                cmd.Parameters.AddWithValue("@FY", bn.AcYear)
                cmd.Parameters.AddWithValue("@N", bn.BillNo)
                If Convert.ToInt32(cmd.ExecuteScalar()) > 0 Then
                    Throw New Exception("Purchase number " & bn.InvoiceNo & " already exists. Please retry.")
                End If
            End Using

            SaveHeader(Trans, bn, bt, supId, supName, mrpTotal)

            For Each r As DataGridViewRow In DgvPurchase.Rows
                SaveLine(Trans, bt, bn.InvoiceNo, supId, r, isReturn)
            Next

            ' queue the cloud replication event in the SAME transaction
            SmSyncQueue.EnqueuePurchase(Trans, bt, bn.InvoiceNo, "INSERT")

            Trans.Commit()
            SavedBill = True
            LastInvoice = bn.InvoiceNo
            VSuperMarket.LogActivity("FrmSmPurchase", "ADD", bt & " " & bn.InvoiceNo & " Rs." & DecOf(txtBillAmount.Text).ToString("0.00"))
            MessageBox.Show("Purchase " & bn.InvoiceNo & " saved successfully.", "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBillNo.Text = bn.InvoiceNo
            RecalcTotals()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    Private Sub SaveHeader(ByVal trans As SqlTransaction, ByVal bn As VSuperMarket.BillNumber, ByVal bt As String, ByVal supId As Integer, ByVal supName As String, ByVal mrpTotal As Decimal)
        Dim gstTotal As Decimal = DecOf(txtCGST.Text) + DecOf(txtSGST.Text)
        Dim words As String = AmountInWords(DecOf(txtBillAmount.Text))

        Using cmd As New SqlCommand("INSERT INTO tbl_PurchDetail (Sa_EntDate_d,Sa_SaveType_vc,Sa_BillDate_d,Sa_BillTime_vc,Sa_Shift_vc,Sa_InvNo_vc,Sa_BillNo_n,Sa_BType_vc," & _
                                    "Sa_CusID_n,Sa_CusName_vc,Sa_GSTTotal_n,Sa_TotalAmt_n,Sa_RoType_vc,Sa_RoAmt_n,Sa_FinalTotal_n,Sa_Words_vc," & _
                                    "Sa_GSTWords_vc,Sa_AcYear_vc,Sa_BillPoints_n,Sa_TodaySave_n,Sa_MRPTotal_n,Sa_UserID_vc,Sa_UpDate_d," & _
                                    "Sa_EmpID_i,Sa_EmpPer_n,Sa_EmpAmt_n,Sa_COutAmt_n,Sa_OldBal_n,Sa_ComType_vc,Sa_GstCal_vc) VALUES " & _
                                    "(@Ent,@SaveTy,@BillDate,@BillTime,@Shift,@InvNo,@BillNo,@BType,@CusID,@CusName,@GSTTot,@TotAmt,@RoType," & _
                                    "@RoAmt,@FinalTot,@Words,@GSTWords,@AcYear,@BillPoints,@TodaySave,@MRPTotal,@UserID,@UpDate,@EmpID,@EmpPer," & _
                                    "@EmpAmt,@COutAmt,@OldBal,@ComType,@GstCal)", VistawinFunctions.ServerConnection, Trans)
            cmd.Parameters.AddWithValue("@Ent", Date.Now)
            cmd.Parameters.AddWithValue("@SaveTy", bt)
            cmd.Parameters.AddWithValue("@BillDate", dtpBillDate.Value.Date)
            cmd.Parameters.AddWithValue("@BillTime", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@Shift", "")
            cmd.Parameters.AddWithValue("@InvNo", bn.InvoiceNo)
            cmd.Parameters.AddWithValue("@BillNo", bn.BillNo)
            ' Sa_BType_vc is the legacy *payment* type (CASH / CREDIT, varchar(10)).
            ' The document type (PURCHASE / PURCHASE RETURN) lives in Sa_SaveType_vc.
            cmd.Parameters.AddWithValue("@BType", "CASH")
            cmd.Parameters.AddWithValue("@CusID", supId)
            cmd.Parameters.AddWithValue("@CusName", supName)
            cmd.Parameters.AddWithValue("@GSTTot", DecOf(txtCGST.Text) + DecOf(txtSGST.Text))
            cmd.Parameters.AddWithValue("@TotAmt", DecOf(txtTaxable.Text))
            cmd.Parameters.AddWithValue("@RoType", "ROUND OFF")
            cmd.Parameters.AddWithValue("@RoAmt", VSuperMarket.RoundOffDiff(DecOf(txtBillAmount.Text)))
            cmd.Parameters.AddWithValue("@FinalTot", DecOf(txtBillAmount.Text))
            cmd.Parameters.AddWithValue("@Words", AmountInWords(DecOf(txtBillAmount.Text)))
            cmd.Parameters.AddWithValue("@GSTWords", "")
            cmd.Parameters.AddWithValue("@AcYear", bn.AcYear)
            cmd.Parameters.AddWithValue("@BillPoints", 0)
            cmd.Parameters.AddWithValue("@TodaySave", 0)
            cmd.Parameters.AddWithValue("@MRPTotal", mrpTotal)
            cmd.Parameters.AddWithValue("@UserID", VSuperMarket.CurrentUserName)
            cmd.Parameters.AddWithValue("@UpDate", Date.Now)
            cmd.Parameters.AddWithValue("@EmpID", 0)
            cmd.Parameters.AddWithValue("@EmpPer", 0)
            cmd.Parameters.AddWithValue("@EmpAmt", 0)
            cmd.Parameters.AddWithValue("@COutAmt", 0)
            cmd.Parameters.AddWithValue("@OldBal", 0)
            cmd.Parameters.AddWithValue("@ComType", "")
            cmd.Parameters.AddWithValue("@GstCal", "EXCLUSIVE")
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub SaveLine(ByVal trans As SqlTransaction, ByVal bt As String, ByVal invNo As String, ByVal supId As Integer, ByVal r As DataGridViewRow, ByVal isReturn As Boolean)
        Dim stNo As Decimal = DecOf(r.Cells("StNo").Value)
        Dim code As Integer = CInt(DecOf(r.Cells("Code").Value))
        Dim purRate As Decimal = DecOf(r.Cells("PurRate").Value)
        Dim mrp As Decimal = DecOf(r.Cells("MRP").Value)
        Dim saleRate As Decimal = DecOf(r.Cells("SaleRate").Value)
        Dim qty As Decimal = DecOf(r.Cells("Qty").Value)
        Dim disPer As Decimal = DecOf(r.Cells("DisPer").Value)
        Dim disAmt As Decimal = DecOf(r.Cells("DisAmt").Value)
        Dim gstPer As Decimal = DecOf(r.Cells("GSTPer").Value)
        Dim cgstAmt As Decimal = DecOf(r.Cells("CGSTAmt").Value)
        Dim sgstAmt As Decimal = DecOf(r.Cells("SGSTAmt").Value)
        Dim taxable As Decimal = DecOf(r.Cells("Taxable").Value)
        Dim netRate As Decimal = purRate - Math.Round(purRate * disPer / 100, 2)
        Dim proTotal As Decimal = taxable + cgstAmt + sgstAmt
        Dim remark As String = Convert.ToString(r.Cells("Remark").Value)

        Using cmd As New SqlCommand("INSERT INTO tbl_PurchParticular (Sa_SaveTy_vc,Sa_BillDate_d,Sa_InvNo_vc,Sa_CusID_n,Sa_StNumber_n,Sa_StCode_n," & _
                                    "Sa_PurRate_n,Sa_MRP_n,Sa_SaleRate_n,Sa_ProDPer_n,Sa_ProDAmt_n,Sa_NetRate_n,Sa_Qty_i,Sa_FQty_i,Sa_TQty_i," & _
                                    "Sa_QType_vc,Sa_Pack_vc,Sa_Taxable_n,Sa_CGSTP_n,Sa_SGSTP_n,Sa_IGSTP_n,Sa_CGSTAmt_n,Sa_SGSTAmt_n,Sa_IGSTAmt_n," & _
                                    "Sa_ProTotal_n,Sa_PRemark_vc,Sa_SRTInv_vc,Sa_SchemeAmt_n,Sa_WhSaleRate_n,Sa_Cal1_n,Sa_Cal2_n) VALUES " & _
                                    "(@SaveTy,@BillDate,@InvNo,@CusID,@StNumber,@StCode,@PurRate,@MRP,@SaleRate,@ProDPer,@ProDAmt,@NetRate," & _
                                    "@Qty,@FQty,@TQty,@QType,@Pack,@Taxable,@CGSTP,@SGSTP,@IGSTP,@CGSTAmt,@SGSTAmt,@IGSTAmt,@ProTotal," & _
                                    "@PRemark,@SRTInv,@SchemeAmt,@WhSaleRate,@Cal1,@Cal2)", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@SaveTy", bt)
            cmd.Parameters.AddWithValue("@BillDate", dtpBillDate.Value.Date)
            cmd.Parameters.AddWithValue("@InvNo", invNo)
            cmd.Parameters.AddWithValue("@CusID", supId)
            cmd.Parameters.AddWithValue("@StNumber", stNo)
            cmd.Parameters.AddWithValue("@StCode", code)
            cmd.Parameters.AddWithValue("@PurRate", purRate)
            cmd.Parameters.AddWithValue("@MRP", mrp)
            cmd.Parameters.AddWithValue("@SaleRate", saleRate)
            cmd.Parameters.AddWithValue("@ProDPer", disPer)
            cmd.Parameters.AddWithValue("@ProDAmt", disAmt)
            cmd.Parameters.AddWithValue("@NetRate", netRate)
            cmd.Parameters.AddWithValue("@Qty", qty)
            cmd.Parameters.AddWithValue("@FQty", 0)
            cmd.Parameters.AddWithValue("@TQty", qty)
            cmd.Parameters.AddWithValue("@QType", "")
            cmd.Parameters.AddWithValue("@Pack", "")
            cmd.Parameters.AddWithValue("@Taxable", taxable)
            cmd.Parameters.AddWithValue("@CGSTP", Math.Round(gstPer / 2, 2))
            cmd.Parameters.AddWithValue("@SGSTP", Math.Round(gstPer / 2, 2))
            cmd.Parameters.AddWithValue("@IGSTP", 0)
            cmd.Parameters.AddWithValue("@CGSTAmt", cgstAmt)
            cmd.Parameters.AddWithValue("@SGSTAmt", sgstAmt)
            cmd.Parameters.AddWithValue("@IGSTAmt", 0)
            cmd.Parameters.AddWithValue("@ProTotal", proTotal)
            cmd.Parameters.AddWithValue("@PRemark", If(remark, ""))
            cmd.Parameters.AddWithValue("@SRTInv", "")
            cmd.Parameters.AddWithValue("@SchemeAmt", 0)
            cmd.Parameters.AddWithValue("@WhSaleRate", saleRate)
            cmd.Parameters.AddWithValue("@Cal1", 0)
            cmd.Parameters.AddWithValue("@Cal2", 0)
            cmd.ExecuteNonQuery()
        End Using

        ' stock movement (purchase increases, purchase-return decreases)
        Dim delta As Decimal = If(isReturn, -qty, qty)
        Using cmd As New SqlCommand("UPDATE tbl_MultiStock SET St_CurrStock_i = ISNULL(St_CurrStock_i,0) + @D, St_UpDate_d=GETDATE(), St_UID_vc=@U WHERE St_StNumber_i = @SN", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@D", delta)
            cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
            cmd.Parameters.AddWithValue("@SN", stNo)
            cmd.ExecuteNonQuery()
        End Using
        Using cmd As New SqlCommand("UPDATE tbl_StockDtl SET St_TotalSt_i = ISNULL(St_TotalSt_i,0) + @D WHERE St_StockID_n = @C AND (St_Decrease_vc='YES' OR St_Decrease_vc IS NULL)", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@D", delta)
            cmd.Parameters.AddWithValue("@C", code)
            cmd.ExecuteNonQuery()
        End Using
        ' audit - legacy convention (SP_tbl_PurchDetail): St_Form_vc = document type,
        '   St_Modify_vc = ADD/EDIT, quantity columns carry the line qty
        Using cmd As New SqlCommand("INSERT INTO tbl_StockModify (St_EDate_d,St_Form_vc,St_Modify_vc,St_StockID_n,St_MediName_vc,St_GodownSt_i,St_ShopSt_i,St_TotalSt_i,St_InvNo_vc,St_Remark_vc) " & _
                                    "VALUES (GETDATE(),@F,@M,@C,@N,@G,@S,@Q,@I,@R)", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@F", If(isReturn, "PURCHASE RETURN", "PURCHASE"))
            cmd.Parameters.AddWithValue("@M", "ADD")
            cmd.Parameters.AddWithValue("@C", code)
            cmd.Parameters.AddWithValue("@N", Convert.ToString(r.Cells("ItemName").Value))
            cmd.Parameters.AddWithValue("@G", qty)
            cmd.Parameters.AddWithValue("@S", 0)
            cmd.Parameters.AddWithValue("@Q", qty)
            cmd.Parameters.AddWithValue("@I", invNo)
            cmd.Parameters.AddWithValue("@R", If(remark, ""))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' ==================================================================================
    '  printing
    ' ==================================================================================
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            If DgvPurchase.Rows.Count = 0 Then
                MessageBox.Show("Nothing to print.", "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            BuildPrintLines()
            printIndex = 0
            Dim pd As New Printing.PrintDocument
            pd.DefaultPageSettings.Margins = New Printing.Margins(60, 60, 40, 40)
            AddHandler pd.PrintPage, AddressOf OnPrintPage
            Dim prev As New PrintPreviewDialog
            prev.Document = pd
            prev.WindowState = FormWindowState.Maximized
            prev.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Purchase Entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BuildPrintLines()
        printLines = New List(Of String)
        Dim invNo As String = If(LastInvoice <> "", LastInvoice, txtBillNo.Text.Replace(" (next)", ""))
        printLines.Add("PURCHASE : " & BillType())
        printLines.Add(Company("Cmp_Name_vc"))
        printLines.Add(Company("Cmp_Address_vc"))
        Dim ph As String = Company("Cmp_PhoneNo_vc")
        If ph <> "" Then printLines.Add("Ph: " & ph)
        printLines.Add(New String("-"c, 58))
        printLines.Add("No      : " & invNo)
        printLines.Add("Date    : " & dtpBillDate.Value.ToString("dd-MM-yyyy HH:mm"))
        printLines.Add("Supplier: " & txtSuppName.Text)
        printLines.Add(New String("-"c, 58))
        printLines.Add(String.Format("{0,-22}{1,6}{2,8}{3,8}{4,12}", "Item", "Qty", "Rate", "GST%", "Amount"))
        printLines.Add(New String("-"c, 58))
        For Each r As DataGridViewRow In DgvPurchase.Rows
            Dim nm As String = Convert.ToString(r.Cells("ItemName").Value)
            If nm.Length > 22 Then nm = nm.Substring(0, 22)
            printLines.Add(String.Format("{0,-22}{1,6}{2,8}{3,8}{4,12}", nm, DecOf(r.Cells("Qty").Value).ToString("0.##"), DecOf(r.Cells("PurRate").Value).ToString("0.00"), DecOf(r.Cells("GSTPer").Value).ToString("0.##"), DecOf(r.Cells("Amount").Value).ToString("0.00")))
        Next
        printLines.Add(New String("-"c, 58))
        printLines.Add(String.Format("{0,44}{1,12}", "Taxable", txtTaxable.Text))
        printLines.Add(String.Format("{0,44}{1,12}", "Discount", txtDiscount.Text))
        printLines.Add(String.Format("{0,44}{1,12}", "CGST", txtCGST.Text))
        printLines.Add(String.Format("{0,44}{1,12}", "SGST", txtSGST.Text))
        printLines.Add(String.Format("{0,44}{1,12}", "Round Off", txtRoundOff.Text))
        printLines.Add(String.Format("{0,44}{1,12}", "PURCHASE AMOUNT", txtBillAmount.Text))
        printLines.Add(New String("-"c, 58))
        printLines.Add(AmountInWords(DecOf(txtBillAmount.Text)))
    End Sub

    Private Function Company(ByVal col As String) As String
        Try
            Using cmd As New SqlCommand("SELECT TOP 1 ISNULL(" & col & ",'') FROM tbl_Companyprofile", VistawinFunctions.ServerConnection)
                Return Convert.ToString(cmd.ExecuteScalar())
            End Using
        Catch
            Return ""
        End Try
    End Function

    Private Sub OnPrintPage(ByVal sender As Object, ByVal e As Printing.PrintPageEventArgs)
        Dim font As New Font("Consolas", 9)
        Dim y As Single = e.MarginBounds.Top
        While printIndex < printLines.Count
            Dim line As String = printLines(printIndex)
            e.Graphics.DrawString(line, font, Brushes.Black, e.MarginBounds.Left, y)
            y += font.GetHeight(e.Graphics) + 1
            printIndex += 1
            If y + font.GetHeight(e.Graphics) > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                Return
            End If
        End While
        e.HasMorePages = False
    End Sub

    ' ==================================================================================
    '  amount in words (Indian numbering)
    ' ==================================================================================
    Private Function AmountInWords(ByVal amt As Decimal) As String
        Dim neg As Boolean = amt < 0
        amt = Math.Abs(amt)
        Dim rupees As Long = CLng(Math.Floor(amt))
        Dim paise As Integer = CInt(Math.Round((amt - rupees) * 100))
        Dim s As String = Words(rupees) & " Rupees"
        If paise > 0 Then s &= " and " & Words(paise) & " Paise"
        s &= " Only"
        If neg Then s = "Minus " & s
        Return s
    End Function

    Private Function TwoDigits(ByVal n As Integer) As String
        Dim a() As String = {"", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
        Dim b() As String = {"", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
        If n < 20 Then Return a(n)
        Return b(n \ 10) & If(n Mod 10 > 0, " " & a(n Mod 10), "")
    End Function

    Private Function ThreeDigits(ByVal n As Integer) As String
        Dim h As Integer = n \ 100
        Dim r As Integer = n Mod 100
        Dim s As String = ""
        If h > 0 Then s = TwoDigits(h) & " Hundred"
        If r > 0 Then
            If s <> "" Then s &= " and " & TwoDigits(r) Else s = TwoDigits(r)
        End If
        Return s
    End Function

    Private Function Words(ByVal n As Long) As String
        If n = 0 Then Return "Zero"
        Dim crore As Long = n \ 10000000
        Dim rem1 As Long = n Mod 10000000
        Dim lakh As Long = rem1 \ 100000
        Dim rem2 As Long = rem1 Mod 100000
        Dim thousand As Long = rem2 \ 1000
        Dim rest As Long = rem2 Mod 1000
        Dim parts As New List(Of String)
        If crore > 0 Then parts.Add(Words(crore) & " Crore")
        If lakh > 0 Then parts.Add(TwoDigits(CInt(lakh)) & " Lakh")
        If thousand > 0 Then parts.Add(ThreeDigits(CInt(thousand)) & " Thousand")
        If rest > 0 Then parts.Add(ThreeDigits(CInt(rest)))
        Return String.Join(" ", parts.ToArray())
    End Function

End Class
