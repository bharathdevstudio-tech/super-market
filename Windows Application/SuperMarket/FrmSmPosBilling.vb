Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

''' <summary>
''' SuperMarket - POS Billing.
'''   Header  -> tbl_SalDetail        (via SP_tbl_SalDetail_Insert)
'''   Lines   -> tbl_SalParticular    (via SP_tbl_SalParticular_Insert)
'''   Bill no -> tbl_BillSeries + MAX(Sa_BillNo_n)+1 per save type + FY
'''   Stock   -> tbl_MultiStock.St_CurrStock_i / tbl_StockDtl.St_TotalSt_i
''' All SQL is parameterized and the whole save runs in one transaction.
''' </summary>
Public Class FrmSmPosBilling

    Dim dtItems As New DataTable
    Dim dtCust As New DataTable
    Dim dictDisplay As New Dictionary(Of String, DataRow)
    Dim dictBarcode As New Dictionary(Of String, DataRow)
    Dim dictCode As New Dictionary(Of String, DataRow)
    Dim dictCust As New Dictionary(Of String, DataRow)
    Dim Trans As SqlTransaction
    Dim SavedBill As Boolean = False
    Dim LastInvoice As String = ""

    ' a saved bill re-opened from the database (empty = a brand new bill)
    Dim LoadedInvoice As String = ""
    Dim LoadedSaveType As String = ""
    Dim LoadedBillNo As Decimal = 0
    Dim LoadedAcYear As String = ""
    Dim ViewDirty As Boolean = False

    ' printing
    Dim printLines As New List(Of String)
    Dim printIndex As Integer = 0

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                If DgvBilling.Rows.Count > 0 Then
                    If MessageBox.Show("Close without saving the current bill?", "POS Billing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return True
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

    Private Sub FrmSmPosBilling_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VSuperMarket.EnsureConnection()
            LoadDgvBilling()
            LoadPayableAdd()                ' also fills the Bill Type combo when the design sets one
            If cboBillType.Items.Count = 0 Then
                cboBillType.Items.AddRange(New Object() {"INVOICE", "ESTIMATE", "SALES RETURN"})
            End If
            If cboBillType.Items.Count > 0 Then cboBillType.SelectedIndex = 0
            LoadCustomers()
            LoadItems()
            ResetBill()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==================================================================================
    '  data loading
    ' ==================================================================================
    Private Sub LoadCustomers()
        dtCust = New DataTable
        Using cmd As New SqlCommand("SELECT C_ID_n, C_Name_vc, ISNULL(C_Mobile_vc,'') AS Mobile FROM tbl_CusProfile " & _
                                    "WHERE C_Type_vc='CUSTOMER' AND ISNULL(C_Show_vc,'YES')='YES' ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dtCust)
            End Using
        End Using
        cboCustomer.Items.Clear()
        dictCust.Clear()
        For Each r As DataRow In dtCust.Rows
            Dim disp As String = Convert.ToString(r("C_ID_n")) & " - " & Convert.ToString(r("C_Name_vc"))
            cboCustomer.Items.Add(disp)
            dictCust(disp.ToUpper()) = r
        Next
        If cboCustomer.Items.Count > 0 Then cboCustomer.SelectedIndex = 0
    End Sub

    Private Sub LoadItems()
        dtItems = New DataTable
        Using cmd As New SqlCommand("SELECT m.St_StNumber_i, m.St_StockID_n, s.St_ProName_vc, ISNULL(s.St_Unit_vc,'') AS Unit, " & _
                                    "ISNULL(m.St_MRPRate_n,0) AS MRP, ISNULL(m.St_SaleRate_n,0) AS SaleRate, ISNULL(m.St_PurRate_n,0) AS PurRate, " & _
                                    "ISNULL(m.St_CurrStock_i,0) AS Stock, ISNULL(m.St_Barcode_vc,'') AS Barcode, " & _
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

    Private Sub ResolveStockInfo(ByVal r As DataRow)
        txtRate.Text = DecOf(r("SaleRate")).ToString("0.00")
        lblStock.Text = "Stock: " & Convert.ToDecimal(r("Stock")).ToString("0.###")
    End Sub

    ' ==================================================================================
    '  design tables - tbl_DgvEntry_Sal (grid) / tbl_DgvEntry_SalPayDtl (header labels)
    ' ==================================================================================
    ' Same design tables the legacy FrmDgvSalesRetail reads.  When a table has no rows
    ' the form simply keeps the columns / captions already defined in the designer, so
    ' a missing design row can never leave the billing screen unusable.
    Private Sub LoadDgvBilling()
        Try
            Dim dtDesign As New DataTable
            Using da As New SqlDataAdapter("SELECT * FROM tbl_DgvEntry_Sal ORDER BY BD_ColOrderby_i", VistawinFunctions.ServerConnection)
                da.Fill(dtDesign)
            End Using
            If dtDesign.Rows.Count = 0 Then Exit Sub                    ' keep the designer columns

            DgvBilling.Rows.Clear()
            DgvBilling.Columns.Clear()

            ' the row delete button comes first, exactly like FrmDgvSalesRetail
            Dim DeleteCol As New DataGridViewButtonColumn
            With DeleteCol
                .Name = "Delete"
                .HeaderText = ""
                .Text = "X"
                .UseColumnTextForButtonValue = True
                .FlatStyle = FlatStyle.Flat
                .Width = 32
            End With
            DgvBilling.Columns.Add(DeleteCol)

            For Each dr As DataRow In dtDesign.Rows
                Dim ColName As String = Trim(Convert.ToString(dr("BD_ColumnName_vc")))
                If ColName = "" Then Continue For
                Dim Caption As String = Trim(Convert.ToString(dr("BD_AliasName_vc")))

                Dim ControlType As String = Trim(Convert.ToString(dr("BD_ControlType_vc"))).ToUpper()

                Dim dgCol As DataGridViewColumn = Nothing
                If ControlType = "COMBO" Then
                    Dim ComboCol As New DataGridViewComboBoxColumn
                    ComboCol.FlatStyle = FlatStyle.Flat
                    Dim ComboSql As String = Trim(Convert.ToString(dr("BD_ComboSQL_vc")))
                    If ComboSql <> "" Then
                        Using cmd As New SqlCommand(ComboSql, VistawinFunctions.ServerConnection)
                            Using rd As SqlDataReader = cmd.ExecuteReader()
                                While rd.Read()
                                    If Not rd.IsDBNull(0) Then ComboCol.Items.Add(Convert.ToString(rd(0)))
                                End While
                            End Using
                        End Using
                    End If
                    dgCol = ComboCol
                Else
                    dgCol = New DataGridViewTextBoxColumn
                End If

                dgCol.Name = ColName                            ' BD_ColumnName_vc - what the row maths uses
                dgCol.HeaderText = If(Caption = "", ColName, Caption)

                Dim WidthVal As String = Trim(Convert.ToString(dr("BD_Width_i"))).ToUpper()
                If WidthVal = "FILL" OrElse WidthVal = "*" Then
                    dgCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Else
                    Dim w As Integer = 0
                    Integer.TryParse(WidthVal, w)
                    If w <= 0 Then w = 100
                    dgCol.Width = w
                End If

                dgCol.ReadOnly = (Trim(Convert.ToString(dr("BD_ReadOnly_vc"))).ToUpper() = "YES")
                dgCol.Visible = (Trim(Convert.ToString(dr("BD_ColShow_vc"))).ToUpper() = "YES")

                dgCol.DefaultCellStyle.BackColor = DgvColor(Convert.ToString(dr("BD_BackColor_vc")), Color.White)
                dgCol.DefaultCellStyle.ForeColor = DgvColor(Convert.ToString(dr("BD_ForeColor_vc")), Color.Black)

                Select Case Trim(Convert.ToString(dr("BD_Format_vc"))).ToUpper()
                    Case "N0", "N1", "N2", "N3", "N4"
                        dgCol.DefaultCellStyle.Format = Trim(Convert.ToString(dr("BD_Format_vc"))).ToUpper()
                    Case "DATE"
                        dgCol.DefaultCellStyle.Format = "dd/MM/yyyy"
                    Case "DATETIME"
                        dgCol.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss tt"
                End Select

                Select Case Trim(Convert.ToString(dr("BD_Alignment_vc"))).ToUpper()
                    Case "LEFT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Case "CENTER"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    Case "RIGHT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select

                DgvBilling.Columns.Add(dgCol)
            Next

            DgvBilling.AllowUserToAddRows = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' a COMBO design row may carry its own list in BD_ComboSQL_vc
    Private Function DesignComboItems(ByVal sql As String) As List(Of String)
        Dim items As New List(Of String)
        sql = Trim(Convert.ToString(sql))
        If sql = "" Then Return items
        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
            Using rd As SqlDataReader = cmd.ExecuteReader()
                While rd.Read()
                    If Not rd.IsDBNull(0) Then
                        Dim v As String = Convert.ToString(rd(0)).Trim()
                        If v <> "" AndAlso Not items.Contains(v) Then items.Add(v)
                    End If
                End While
            End Using
        End Using
        Return items
    End Function

    Private Function DgvColor(ByVal name As String, ByVal fallback As Color) As Color
        Dim s As String = Convert.ToString(name).Trim()
        If s = "" Then Return fallback
        Dim c As Color = Color.FromName(s)
        If c.ToArgb() = 0 Then Return fallback              ' unknown colour name
        Return c
    End Function

    ' header / totals captions and visibility come from tbl_DgvEntry_SalPayDtl
    Private Sub LoadPayableAdd()
        Try
            Dim dictLbl As New Dictionary(Of String, Control)(StringComparer.OrdinalIgnoreCase)
            Dim dictCtl As New Dictionary(Of String, Control)(StringComparer.OrdinalIgnoreCase)

            dictLbl("Sa_SaveType_vc") = Label1
            dictCtl("Sa_SaveType_vc") = cboBillType
            dictLbl("Sa_BillDate_d") = Label2
            dictCtl("Sa_BillDate_d") = dtpBillDate
            dictLbl("Sa_BillNo_n") = Label3
            dictCtl("Sa_BillNo_n") = txtBillNo
            dictLbl("Sa_CusID_n") = Label4
            dictCtl("Sa_CusID_n") = cboCustomer
            dictLbl("Sa_CusName_vc") = Label5
            dictCtl("Sa_CusName_vc") = txtCustName
            dictLbl("Sa_CusMobile_vc") = Label6
            dictCtl("Sa_CusMobile_vc") = txtCustMobile
            dictLbl("TotalItem") = Label11
            dictCtl("TotalItem") = lblItemCount
            dictLbl("TotalQty") = Label12
            dictCtl("TotalQty") = lblTotalQty
            dictLbl("Sa_BefDisAmt_n") = Label13
            dictCtl("Sa_BefDisAmt_n") = txtSubTotal
            dictLbl("Sa_DiscountAmt_n") = Label14
            dictCtl("Sa_DiscountAmt_n") = txtDiscount
            dictLbl("Sa_Taxable_n") = Label15
            dictCtl("Sa_Taxable_n") = txtTaxable
            dictLbl("Sa_GSTTotal_n") = Label16
            dictCtl("Sa_GSTTotal_n") = txtTax
            dictLbl("Sa_CGSTAmt_n") = Label17
            dictCtl("Sa_CGSTAmt_n") = txtCGST
            dictLbl("Sa_SGSTAmt_n") = Label18
            dictCtl("Sa_SGSTAmt_n") = txtSGST
            dictLbl("Sa_RoAmt_n") = Label19
            dictCtl("Sa_RoAmt_n") = txtRoundOff
            dictLbl("Sa_FinalTotal_n") = Label20
            dictCtl("Sa_FinalTotal_n") = txtBillAmount

            Dim dtDesign As New DataTable
            Using da As New SqlDataAdapter("SELECT * FROM tbl_DgvEntry_SalPayDtl ORDER BY BD_ColOrderby_i", VistawinFunctions.ServerConnection)
                da.Fill(dtDesign)
            End Using
            If dtDesign.Rows.Count = 0 Then Exit Sub            ' keep the designer captions

            For Each dr As DataRow In dtDesign.Rows
                Dim key As String = Trim(Convert.ToString(dr("BD_ColumnName_vc")))
                If key = "" OrElse Not dictCtl.ContainsKey(key) Then Continue For

                Dim shown As Boolean = (Trim(Convert.ToString(dr("BD_ColShow_vc"))).ToUpper() = "YES")
                Dim caption As String = Trim(Convert.ToString(dr("BD_AliasName_vc")))

                Dim lbl As Control = dictLbl(key)
                Dim ctl As Control = dictCtl(key)
                If caption <> "" Then lbl.Text = caption
                lbl.Visible = shown
                ctl.Visible = shown

                ' a visible COMBO takes its items from BD_ComboSQL_vc when one is set
                Dim cmb As ComboBox = TryCast(ctl, ComboBox)
                If cmb IsNot Nothing AndAlso shown Then
                    Dim designItems As List(Of String) = DesignComboItems(Convert.ToString(dr("BD_ComboSQL_vc")))
                    If designItems.Count > 0 Then
                        cmb.Items.Clear()
                        cmb.Items.AddRange(designItems.ToArray())
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    ' shows the rate / live stock as soon as an item is picked from the list
    Private Sub cboItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboItem.SelectedIndexChanged
        Try
            If cboItem.SelectedIndex < 0 Then Exit Sub
            Dim r As DataRow = ResolveItem(Convert.ToString(cboItem.SelectedItem))
            If r IsNot Nothing Then
                txtRate.Text = Convert.ToDecimal(r("SaleRate")).ToString("0.00")
                ShowStock(r)
            End If
        Catch
        End Try
    End Sub

    Private Sub qty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyDown, txtRate.KeyDown, txtDisc.KeyDown
        If e.KeyCode = Keys.Enter Then
            TryAddFromItemBox()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub TryAddFromItemBox()
        If cboItem.Text.Trim = "" Then Exit Sub
        Dim r As DataRow = ResolveItem(cboItem.Text.Trim())
        If r Is Nothing Then
            MessageBox.Show("Item not found.", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboItem.SelectAll() : cboItem.Focus()
            Exit Sub
        End If
        If txtRate.Text.Trim = "" Then txtRate.Text = Convert.ToDecimal(r("SaleRate")).ToString("0.##")
        ShowStock(r)
        AddItemToCart(r)
    End Sub

    Private Function ResolveItem(ByVal txt As String) As DataRow
        Dim key As String = txt.ToUpper()
        ' a scanned barcode is exact, so check it first - the combo's
        ' auto-complete must never be able to turn a scan into another item
        If dictBarcode.ContainsKey(key) Then Return dictBarcode(key)
        If dictDisplay.ContainsKey(key) Then Return dictDisplay(key)
        If dictCode.ContainsKey(key) Then Return dictCode(key)
        ' barcode / code may be the leading token of the display text
        Dim sp As Integer = txt.IndexOf(" "c)
        If sp > 0 Then
            Dim head As String = txt.Substring(0, sp).Trim().ToUpper()
            If dictBarcode.ContainsKey(head) Then Return dictBarcode(head)
            If dictCode.ContainsKey(head) Then Return dictCode(head)
        End If
        ' name prefix search
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

    Private Sub ShowStock(ByVal r As DataRow)
        lblStock.Text = "Stock: " & Convert.ToDecimal(r("Stock")).ToString("0.##")
    End Sub

    Private Sub AddItemToCart(ByVal r As DataRow)
        Dim stNo As Decimal = Convert.ToDecimal(r("St_StNumber_i"))
        Dim code As String = Convert.ToString(r("St_StockID_n"))
        Dim qty As Decimal = 1
        Decimal.TryParse(txtQty.Text.Trim(), qty)
        If qty <= 0 Then qty = 1

        ' same rate line already on the bill -> just add qty
        For Each gr As DataGridViewRow In DgvBilling.Rows
            If Convert.ToString(gr.Cells("StNo").Value) = Convert.ToString(stNo) Then
                gr.Cells("Qty").Value = DecOf(gr.Cells("Qty").Value) + qty
                RecalcRow(gr)
                RecalcTotals()
                ViewDirty = True
                ResetEntry()
                Exit Sub
            End If
        Next

        Dim idx As Integer = DgvBilling.Rows.Add()
        Dim row As DataGridViewRow = DgvBilling.Rows(idx)
        row.Cells("SNoB").Value = idx + 1
        row.Cells("Code").Value = code
        row.Cells("StNo").Value = stNo.ToString()
        row.Cells("ItemName").Value = Convert.ToString(r("St_ProName_vc"))
        row.Cells("Unit").Value = Convert.ToString(r("Unit"))
        row.Cells("MRP").Value = Convert.ToDecimal(r("MRP")).ToString("0.00")
        row.Cells("SaleRate").Value = DecOf(txtRate.Text).ToString("0.00")
        row.Cells("Qty").Value = DecOf(txtQty.Text).ToString("0.###")
        row.Cells("DisPer").Value = DecOf(txtDisc.Text).ToString("0.##")
        row.Cells("GSTPer").Value = (Convert.ToDecimal(r("CGST")) + Convert.ToDecimal(r("SGST"))).ToString("0.##")
        row.Cells("Remark").Value = ""
        RecalcRow(row)
        RecalcTotals()
        ViewDirty = True
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
        txtRate.Text = ""
        txtDisc.Text = "0"
        lblStock.Text = ""
        cboItem.Focus()
    End Sub

    ' ==================================================================================
    '  cart maths
    ' ==================================================================================
    Private Sub RecalcRow(ByVal r As DataGridViewRow)
        If r Is Nothing Then Exit Sub
        Dim rate As Decimal = DecOf(r.Cells("SaleRate").Value)
        Dim qty As Decimal = DecOf(r.Cells("Qty").Value)
        Dim disPer As Decimal = DecOf(r.Cells("DisPer").Value)
        Dim gst As Decimal = DecOf(r.Cells("GSTPer").Value)

        Dim gross As Decimal = rate * qty
        Dim dis As Decimal = Math.Round(gross * disPer / 100, 2)
        Dim net As Decimal = gross - dis
        Dim taxable As Decimal = If(gst > 0, Math.Round(net * 100 / (100 + gst), 2), net)
        Dim taxAmt As Decimal = net - taxable

        r.Cells("DisAmt").Value = dis.ToString("0.00")
        r.Cells("NetRate").Value = (rate - Math.Round(rate * disPer / 100, 2)).ToString("0.00")
        r.Cells("CGSTAmt").Value = Math.Round(taxAmt / 2, 2).ToString("0.00")
        r.Cells("SGSTAmt").Value = Math.Round(taxAmt / 2, 2).ToString("0.00")
        r.Cells("Amount").Value = net.ToString("0.00")
    End Sub

    Private Sub RecalcTotals()
        Dim subTotal As Decimal = 0
        Dim discount As Decimal = 0
        Dim taxable As Decimal = 0
        Dim tax As Decimal = 0
        Dim billAmt As Decimal = 0
        Dim totQty As Decimal = 0

        For Each r As DataGridViewRow In DgvBilling.Rows
            Dim rate As Decimal = DecOf(r.Cells("SaleRate").Value)
            Dim qty As Decimal = DecOf(r.Cells("Qty").Value)
            Dim disPer As Decimal = DecOf(r.Cells("DisPer").Value)
            Dim gst As Decimal = DecOf(r.Cells("GSTPer").Value)
            Dim gross As Decimal = rate * qty
            Dim dis As Decimal = Math.Round(gross * disPer / 100, 2)
            Dim net As Decimal = gross - dis
            Dim tx As Decimal = If(gst > 0, Math.Round(net * 100 / (100 + gst), 2), net)

            subTotal += gross
            discount += dis
            taxable += tx
            tax += (net - tx)
            billAmt += net
            totQty += qty
        Next

        Dim finalAmt As Decimal = VSuperMarket.RoundOff(billAmt)
        Dim roundOff As Decimal = finalAmt - billAmt

        txtSubTotal.Text = subTotal.ToString("0.00")
        txtDiscount.Text = discount.ToString("0.00")
        txtTaxable.Text = taxable.ToString("0.00")
        txtTax.Text = tax.ToString("0.00")
        txtCGST.Text = Math.Round(tax / 2, 2).ToString("0.00")
        txtSGST.Text = Math.Round(tax / 2, 2).ToString("0.00")
        txtRoundOff.Text = roundOff.ToString("0.00")
        txtBillAmount.Text = finalAmt.ToString("0.00")
        lblItemCount.Text = DgvBilling.Rows.Count.ToString()
        lblTotalQty.Text = totQty.ToString("0.###")
    End Sub

    Private Sub DgvBilling_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvBilling.CellEndEdit
        If e.RowIndex < 0 Then Exit Sub
        Try
            Dim r As DataGridViewRow = DgvBilling.Rows(e.RowIndex)
            Dim name As String = DgvBilling.Columns(e.ColumnIndex).Name
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
            ViewDirty = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvBilling_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DgvBilling.KeyDown
        If e.KeyCode = Keys.Delete AndAlso Not DgvBilling.IsCurrentCellInEditMode Then
            DeleteCurrentRow()
            e.Handled = True
        End If
    End Sub

    ' the "X" button column the design table puts in front of the grid
    Private Sub DgvBilling_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvBilling.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If Convert.ToString(DgvBilling.Columns(e.ColumnIndex).Name) <> "Delete" Then Exit Sub
        Try
            DgvBilling.Rows.RemoveAt(e.RowIndex)
            RenumberRows()
            RecalcTotals()
            ViewDirty = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
            Using cmd As New SqlCommand("SELECT ISNULL(MAX(CONVERT(NUMERIC(18,0),Sa_BillNo_n)),0)+1 FROM tbl_SalDetail WHERE Sa_SaveType_vc=@T AND Sa_AcYear_vc=@FY", VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@T", t)
                cmd.Parameters.AddWithValue("@FY", fy)
                n = Convert.ToDecimal(cmd.ExecuteScalar())
            End Using
            Return series & n.ToString("0") & " (next)"
        Catch
            Return ""
        End Try
    End Function

    Private Sub cboCustomer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCustomer.SelectedIndexChanged
        Dim r As DataRow = ResolveCustomer()
        If r IsNot Nothing Then
            txtCustName.Text = Convert.ToString(r("C_Name_vc"))
            txtCustMobile.Text = Convert.ToString(r("Mobile"))
        End If
    End Sub

    Private Function ResolveCustomer() As DataRow
        Dim txt As String = cboCustomer.Text.Trim()
        If txt = "" Then Return Nothing
        If dictCust.ContainsKey(txt.ToUpper()) Then Return dictCust(txt.ToUpper())
        If cboCustomer.SelectedItem IsNot Nothing Then
            Dim sel As String = Convert.ToString(cboCustomer.SelectedItem)
            If dictCust.ContainsKey(sel.ToUpper()) Then Return dictCust(sel.ToUpper())
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
        If DgvBilling.CurrentRow Is Nothing Then
            MessageBox.Show("Select a row to delete.", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        DgvBilling.Rows.Remove(DgvBilling.CurrentRow)
        RenumberRows()
        RecalcTotals()
        ViewDirty = True
    End Sub

    Private Sub RenumberRows()
        For i As Integer = 0 To DgvBilling.Rows.Count - 1
            DgvBilling.Rows(i).Cells("SNoB").Value = i + 1
        Next
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If DgvBilling.Rows.Count > 0 Then
            If MessageBox.Show("Clear the current bill?", "POS Billing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub
        End If
        ResetBill()
    End Sub

    Private Sub ResetBill()
        DgvBilling.Rows.Clear()
        LoadedInvoice = ""
        LoadedSaveType = ""
        LoadedBillNo = 0
        LoadedAcYear = ""
        ViewDirty = False
        RecalcTotals()
        ResetEntry()
        dtpBillDate.Value = Date.Today
        SavedBill = False
        LastInvoice = ""
        txtBillNo.Text = PreviewBillNo()
        If cboCustomer.Items.Count > 0 AndAlso cboCustomer.SelectedIndex < 0 Then cboCustomer.SelectedIndex = 0
        UpdateViewState()
        lblStatus.Text = ""
        cboItem.Focus()
    End Sub

    ' a brand new bill and a re-opened one behave differently: the bill type and number
    ' of a saved bill are fixed, and only a saved bill can be deleted
    Private Sub UpdateViewState()
        Dim viewing As Boolean = (LoadedInvoice <> "")
        cboBillType.Enabled = Not viewing
        BtnDeleteBill.Enabled = viewing
        lblViewMode.Text = If(viewing, "VIEWING  " & LoadedInvoice, "New Bill")
        txtOpenBill.Text = ""
    End Sub

    ' editing a re-opened bill must not be thrown away by accident
    Private Function ConfirmDiscard() As Boolean
        If LoadedInvoice = "" OrElse Not ViewDirty Then Return True
        Return MessageBox.Show("Discard the changes to bill " & LoadedInvoice & "?", "POS Billing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes
    End Function

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    ' ==================================================================================
    '  open / prev / next / delete   (re-open a saved bill, walk the bill book)
    ' ==================================================================================
    Private Sub BtnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOpen.Click
        OpenBillFromBox()
    End Sub

    Private Sub txtOpenBill_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOpenBill.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            OpenBillFromBox()
        End If
    End Sub

    Private Sub OpenBillFromBox()
        Dim typed As String = txtOpenBill.Text.Trim()
        If typed = "" Then
            MessageBox.Show("Enter the bill number to open (for example " & BillType() & " 1).", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtOpenBill.Focus()
            Exit Sub
        End If
        Dim invNo As String = ResolveInvoiceNo(typed)
        If invNo = "" Then
            MessageBox.Show("No saved bill matches '" & typed & "'.", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If invNo = LoadedInvoice Then Exit Sub
        If Not ConfirmDiscard() Then Exit Sub
        LoadSavedBill(invNo)
    End Sub

    ' accepts the full invoice number ("SM12") or just the running number ("12")
    Private Function ResolveInvoiceNo(ByVal typed As String) As String
        typed = typed.Trim()
        If typed = "" Then Return ""
        Using cmd As New SqlCommand("SELECT TOP 1 Sa_InvNo_vc FROM tbl_SalDetail WHERE Sa_InvNo_vc = @I", VistawinFunctions.ServerConnection)
            cmd.Parameters.AddWithValue("@I", typed)
            Dim v As Object = cmd.ExecuteScalar()
            If v IsNot Nothing AndAlso Not Convert.IsDBNull(v) Then Return Convert.ToString(v)
        End Using
        Dim n As Decimal
        If Decimal.TryParse(typed, n) Then
            Using cmd As New SqlCommand("SELECT TOP 1 Sa_InvNo_vc FROM tbl_SalDetail WHERE Sa_BillNo_n = @N ORDER BY Sa_EntDate_d DESC", VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@N", n)
                Dim v As Object = cmd.ExecuteScalar()
                If v IsNot Nothing AndAlso Not Convert.IsDBNull(v) Then Return Convert.ToString(v)
            End Using
        End If
        Return ""
    End Function

    Private Sub BtnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrev.Click
        OpenAdjacentBill(False)
    End Sub

    Private Sub BtnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNext.Click
        OpenAdjacentBill(True)
    End Sub

    ' legacy PreviousBill / NextBill: walk the same save type and financial year by Sa_BillNo_n.
    ' From a brand new bill both directions open the latest saved bill of the selected type.
    Private Sub OpenAdjacentBill(ByVal forward As Boolean)
        Try
            Dim viewing As Boolean = (LoadedInvoice <> "")
            Dim bt As String = If(viewing, LoadedSaveType, BillType())
            Dim acYear As String = If(viewing, LoadedAcYear, VSuperMarket.CurrentFY)
            Dim billNo As Decimal = If(viewing, LoadedBillNo, Decimal.MaxValue)

            Dim sql As String
            If Not forward Then
                sql = "SELECT TOP 1 Sa_InvNo_vc FROM tbl_SalDetail WHERE Sa_SaveType_vc = @T AND Sa_AcYear_vc = @FY AND Sa_BillNo_n < @N ORDER BY Sa_BillNo_n DESC"
            ElseIf Not viewing Then
                sql = "SELECT TOP 1 Sa_InvNo_vc FROM tbl_SalDetail WHERE Sa_SaveType_vc = @T AND Sa_AcYear_vc = @FY ORDER BY Sa_BillNo_n DESC"
            Else
                sql = "SELECT TOP 1 Sa_InvNo_vc FROM tbl_SalDetail WHERE Sa_SaveType_vc = @T AND Sa_AcYear_vc = @FY AND Sa_BillNo_n > @N ORDER BY Sa_BillNo_n ASC"
            End If

            Dim invNo As String = ""
            Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@T", bt)
                cmd.Parameters.AddWithValue("@FY", acYear)
                cmd.Parameters.AddWithValue("@N", billNo)
                Dim v As Object = cmd.ExecuteScalar()
                If v IsNot Nothing AndAlso Not Convert.IsDBNull(v) Then invNo = Convert.ToString(v)
            End Using

            If invNo = "" Then
                MessageBox.Show("No " & bt & " bill " & If(forward, "after", "before") & " this one in " & acYear & ".", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Not ConfirmDiscard() Then Exit Sub
            LoadSavedBill(invNo)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ----------------------------------------------------------------------------------
    '  re-open a saved bill: header from tbl_SalDetail, lines through the grid design
    '  table's BD_SQLColumn_vc expressions (order = BD_ColOrderby_i)
    ' ----------------------------------------------------------------------------------
    Private Sub LoadSavedBill(ByVal invNo As String)
        invNo = invNo.Trim()
        If invNo = "" Then Exit Sub
        Try
            ' ---- header ----
            Dim dtH As New DataTable
            Using da As New SqlDataAdapter("SELECT TOP 1 sa.Sa_SaveType_vc, sa.Sa_BillNo_n, sa.Sa_AcYear_vc, sa.Sa_BillDate_d, " & _
                                           "sa.Sa_CusID_n, ISNULL(sa.Sa_CusName_vc,'') AS Sa_CusName_vc, " & _
                                           "ISNULL(c.C_Name_vc,'') AS C_Name_vc, ISNULL(c.C_Mobile_vc,'') AS C_Mobile_vc " & _
                                           "FROM tbl_SalDetail sa LEFT JOIN tbl_CusProfile c ON c.C_ID_n = sa.Sa_CusID_n " & _
                                           "WHERE sa.Sa_InvNo_vc = @I", VistawinFunctions.ServerConnection)
                da.SelectCommand.Parameters.AddWithValue("@I", invNo)
                da.Fill(dtH)
            End Using
            If dtH.Rows.Count = 0 Then
                MessageBox.Show("Bill " & invNo & " was not found.", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Dim hdr As DataRow = dtH.Rows(0)

            ' ---- the SELECT comes from the design table ----
            Dim selectList As New List(Of String)
            Dim colNames As New List(Of String)
            Using cmd As New SqlCommand("SELECT BD_ColumnName_vc, BD_SQLColumn_vc FROM tbl_DgvEntry_Sal ORDER BY BD_ColOrderby_i", VistawinFunctions.ServerConnection)
                Using rd As SqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        Dim colName As String = Trim(Convert.ToString(rd("BD_ColumnName_vc")))
                        Dim expr As String = Trim(Convert.ToString(rd("BD_SQLColumn_vc")))
                        If colName = "" OrElse expr = "" Then Continue While
                        ' no ISNULL here: the design may point at a numeric column while the
                        ' grid stores text, so a NULL is simply handled when filling the cell
                        selectList.Add(expr & " AS " & colName)
                        colNames.Add(colName)
                    End While
                End Using
            End Using

            If selectList.Count = 0 Then
                MessageBox.Show("The billing design (tbl_DgvEntry_Sal) has no readable columns, so a saved bill cannot be re-opened.", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim sql As String = "SELECT " & String.Join(", ", selectList.ToArray()) & _
                " FROM tbl_SalDetail sa" & _
                " LEFT JOIN tbl_SalParticular sp ON sp.Sa_InvNo_vc = sa.Sa_InvNo_vc" & _
                " LEFT JOIN tbl_MultiStock m ON m.St_StNumber_i = sp.Sa_StNumber_n" & _
                " LEFT JOIN tbl_StockDtl s ON s.St_StockID_n = sp.Sa_StCode_n" & _
                " WHERE sa.Sa_InvNo_vc = @I ORDER BY sp.Sa_RecordNo2_i"

            Dim dtL As New DataTable
            Using da As New SqlDataAdapter(sql, VistawinFunctions.ServerConnection)
                da.SelectCommand.Parameters.AddWithValue("@I", invNo)
                da.Fill(dtL)
            End Using

            DgvBilling.Rows.Clear()
            For Each dr As DataRow In dtL.Rows
                Dim idx As Integer = DgvBilling.Rows.Add()
                Dim row As DataGridViewRow = DgvBilling.Rows(idx)
                For Each colName As String In colNames
                    If DgvBilling.Columns.Contains(colName) Then
                        Dim v As Object = dr(colName)
                        If Convert.IsDBNull(v) Then v = Nothing
                        row.Cells(colName).Value = v
                    End If
                Next
            Next
            RenumberRows()
            RecalcTotals()

            ' ---- header controls ----
            LoadedInvoice = invNo
            LoadedSaveType = Convert.ToString(hdr("Sa_SaveType_vc"))
            LoadedBillNo = If(Convert.IsDBNull(hdr("Sa_BillNo_n")), 0D, Convert.ToDecimal(hdr("Sa_BillNo_n")))
            LoadedAcYear = Convert.ToString(hdr("Sa_AcYear_vc"))
            ViewDirty = False

            If LoadedSaveType <> "" AndAlso Not cboBillType.Items.Contains(LoadedSaveType) Then cboBillType.Items.Add(LoadedSaveType)
            cboBillType.SelectedItem = LoadedSaveType

            Dim billDate As Date
            If DateTime.TryParse(Convert.ToString(hdr("Sa_BillDate_d")), billDate) Then dtpBillDate.Value = billDate
            txtBillNo.Text = invNo

            Dim custName As String = Convert.ToString(hdr("C_Name_vc"))
            If custName = "" Then custName = Convert.ToString(hdr("Sa_CusName_vc"))
            txtCustName.Text = custName
            txtCustMobile.Text = Convert.ToString(hdr("C_Mobile_vc"))

            Dim custId As Integer = If(Convert.IsDBNull(hdr("Sa_CusID_n")), 0, Convert.ToInt32(hdr("Sa_CusID_n")))
            Dim disp As String = ""
            For Each s As String In cboCustomer.Items
                If s.StartsWith(custId.ToString() & " - ", StringComparison.Ordinal) Then disp = s : Exit For
            Next
            If disp <> "" Then cboCustomer.SelectedItem = disp Else cboCustomer.Text = custName

            SavedBill = True
            LastInvoice = invNo
            UpdateViewState()
            lblStatus.Text = "Bill " & invNo & " opened."
        Catch ex As Exception
            MessageBox.Show("Could not open bill " & invNo & ":" & vbCrLf & ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ----------------------------------------------------------------------------------
    '  delete the bill currently on screen - stock is put back and logged
    '  ('BILL DELETE', the same audit shape FrmDgvSalesRetail / SP_tbl_SalDelete use)
    ' ----------------------------------------------------------------------------------
    Private Sub BtnDeleteBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeleteBill.Click
        If LoadedInvoice = "" Then
            MessageBox.Show("Open a saved bill first.", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If Not VSuperMarket.HasPermission("FrmSmPosBilling", "DELETE") Then
            MessageBox.Show("You do not have permission to delete bills.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If MessageBox.Show("Delete bill " & LoadedInvoice & " ?" & vbCrLf & "Its stock movement will be put back.", "POS Billing", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Exit Sub
        DeleteBill(LoadedInvoice, LoadedSaveType)
    End Sub

    ' ----------------------------------------------------------------------------------
    '  delete a bill - stock is put back and logged
    '  ('BILL DELETE', the same audit shape FrmDgvSalesRetail / SP_tbl_SalDelete use)
    ' ----------------------------------------------------------------------------------
    Private Sub DeleteBill(ByVal invNo As String, ByVal bt As String)
        If invNo = "" Then Exit Sub
        Dim isReturn As Boolean = String.Equals(bt, "SALES RETURN", StringComparison.OrdinalIgnoreCase)
        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            Dim lines As List(Of Object()) = ReadBillLines(Trans, invNo)
            For Each l As Object() In lines
                Dim stNo As Decimal = Convert.ToDecimal(l(0))
                Dim code As Integer = CInt(Convert.ToDecimal(l(1)))
                Dim tqty As Decimal = Convert.ToDecimal(l(2))
                ApplyStockDelta(Trans, stNo, code, If(isReturn, -tqty, tqty))
                WriteStockAudit(Trans, invNo, bt, "BILL DELETE", code, Convert.ToString(l(3)), 0, 0, tqty)
            Next
            Using cmd As New SqlCommand("DELETE FROM tbl_SalParticular WHERE Sa_InvNo_vc = @I", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@I", invNo)
                cmd.ExecuteNonQuery()
            End Using
            Using cmd As New SqlCommand("DELETE FROM tbl_SalDetail WHERE Sa_InvNo_vc = @I", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@I", invNo)
                cmd.ExecuteNonQuery()
            End Using
            Trans.Commit()
            VSuperMarket.LogActivity("FrmSmPosBilling", "DELETE", bt & " " & invNo & " (" & lines.Count & " line(s))")
            If String.Equals(invNo, LoadedInvoice, StringComparison.OrdinalIgnoreCase) Then
                ResetBill()
                lblStatus.Text = "Bill " & invNo & " deleted."
            End If
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    ' ==================================================================================
    '  save   (new bill, or update of a bill re-opened with Open / Prev / Next)
    ' ==================================================================================
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If DgvBilling.Rows.Count = 0 Then
            MessageBox.Show("No items in the bill.", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim editing As Boolean = (LoadedInvoice <> "")
        If Not VSuperMarket.HasPermission("FrmSmPosBilling", If(editing, "EDIT", "ADD")) Then
            MessageBox.Show("You do not have permission to " & If(editing, "edit", "save") & " bills.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim bt As String = If(editing, LoadedSaveType, BillType())
        Dim isReturn As Boolean = String.Equals(bt, "SALES RETURN", StringComparison.OrdinalIgnoreCase)

        Dim custRow As DataRow = ResolveCustomer()
        Dim custId As Integer = 0
        Dim custName As String = cboCustomer.Text.Trim()
        If custRow IsNot Nothing Then
            custId = Convert.ToInt32(custRow("C_ID_n"))
            custName = Convert.ToString(custRow("C_Name_vc"))
        Else
            ' fall back to the walk-in CASH customer if present
            For Each r As DataRow In dtCust.Rows
                If String.Equals(Convert.ToString(r("C_Name_vc")), "CASH", StringComparison.OrdinalIgnoreCase) Then
                    custRow = r : custId = Convert.ToInt32(r("C_ID_n")) : custName = "CASH"
                    Exit For
                End If
            Next
            If custName = "" Then custName = "CASH"
        End If

        ' MRP total for the header
        Dim mrpTotal As Decimal = 0
        For Each r As DataGridViewRow In DgvBilling.Rows
            mrpTotal += DecOf(r.Cells("MRP").Value) * DecOf(r.Cells("Qty").Value)
        Next

        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            Dim invNo As String
            Dim billNo As Decimal
            Dim acYear As String

            If editing Then
                ' an update keeps the invoice / bill number: reverse the old stock movement,
                ' drop the old rows, then write the edited bill back under the same number
                invNo = LoadedInvoice
                billNo = LoadedBillNo
                acYear = LoadedAcYear

                Dim oldLines As List(Of Object()) = ReadBillLines(Trans, invNo)
                For Each l As Object() In oldLines
                    Dim oQty As Decimal = Convert.ToDecimal(l(2))
                    ApplyStockDelta(Trans, Convert.ToDecimal(l(0)), CInt(Convert.ToDecimal(l(1))), If(isReturn, -oQty, oQty))
                Next
                ' lines the user removed from the bill are logged exactly like a row delete
                Dim keptStNos As New List(Of Decimal)
                For Each r As DataGridViewRow In DgvBilling.Rows
                    keptStNos.Add(DecOf(r.Cells("StNo").Value))
                Next
                For Each l As Object() In oldLines
                    If Not keptStNos.Contains(Convert.ToDecimal(l(0))) Then
                        WriteStockAudit(Trans, invNo, bt, "ROW DELETE", CInt(Convert.ToDecimal(l(1))), Convert.ToString(l(3)), 0, 0, Convert.ToDecimal(l(2)))
                    End If
                Next

                Using cmd As New SqlCommand("DELETE FROM tbl_SalParticular WHERE Sa_InvNo_vc=@I", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@I", invNo)
                    cmd.ExecuteNonQuery()
                End Using
                Using cmd As New SqlCommand("DELETE FROM tbl_SalDetail WHERE Sa_InvNo_vc=@I", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@I", invNo)
                    cmd.ExecuteNonQuery()
                End Using
            Else
                Dim bn As VSuperMarket.BillNumber = VSuperMarket.NextBillNumber(Trans, bt)
                invNo = bn.InvoiceNo
                billNo = bn.BillNo
                acYear = bn.AcYear

                ' duplicate guard for the reserved number
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM tbl_SalDetail WHERE Sa_SaveType_vc=@T AND Sa_AcYear_vc=@FY AND Sa_BillNo_n=@N", VistawinFunctions.ServerConnection, Trans)
                    cmd.Parameters.AddWithValue("@T", bt)
                    cmd.Parameters.AddWithValue("@FY", acYear)
                    cmd.Parameters.AddWithValue("@N", billNo)
                    If Convert.ToInt32(cmd.ExecuteScalar()) > 0 Then
                        Throw New Exception("Bill number " & invNo & " already exists. Please retry.")
                    End If
                End Using
            End If

            InsertBillHeader(Trans, bt, invNo, billNo, acYear, custId, custName, mrpTotal)

            Dim modifyType As String = If(editing, "EDIT", "ADD")
            For Each r As DataGridViewRow In DgvBilling.Rows
                SaveLine(Trans, bt, invNo, custId, r, isReturn, modifyType)
            Next

            Trans.Commit()
            SavedBill = True
            ViewDirty = False
            LastInvoice = invNo
            VSuperMarket.LogActivity("FrmSmPosBilling", If(editing, "EDIT", "ADD"), bt & " " & invNo & " Rs." & DecOf(txtBillAmount.Text).ToString("0.00"))
            lblStatus.Text = "Bill " & invNo & " " & If(editing, "updated", "saved") & " successfully."
            If editing Then
                LoadedBillNo = billNo
                LoadedAcYear = acYear
            End If
            txtBillNo.Text = invNo
            RecalcTotals()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub

    ' the lines of a saved bill: (0) stock number, (1) item code, (2) qty, (3) item name
    Private Function ReadBillLines(ByVal trans As SqlTransaction, ByVal invNo As String) As List(Of Object())
        Dim lines As New List(Of Object())
        Using cmd As New SqlCommand("SELECT sp.Sa_StNumber_n, sp.Sa_StCode_n, sp.Sa_TQty_i, ISNULL(s.St_ProName_vc,'') " & _
                                    "FROM tbl_SalParticular sp LEFT JOIN tbl_StockDtl s ON s.St_StockID_n = sp.Sa_StCode_n " & _
                                    "WHERE sp.Sa_InvNo_vc = @I", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@I", invNo)
            Using rd As SqlDataReader = cmd.ExecuteReader()
                While rd.Read()
                    ' Sa_StCode_n is INT while the others are NUMERIC - GetDecimal would throw on it
                    lines.Add(New Object() {Convert.ToDecimal(rd.GetValue(0)), _
                                            Convert.ToDecimal(rd.GetValue(1)), _
                                            Convert.ToDecimal(rd.GetValue(2)), _
                                            Convert.ToString(rd.GetValue(3))})
                End While
            End Using
        End Using
        Return lines
    End Function

    ' header row - the existing SP_tbl_SalDetail_Insert keeps the legacy column set in one place
    Private Sub InsertBillHeader(ByVal trans As SqlTransaction, ByVal bt As String, ByVal invNo As String, ByVal billNo As Decimal, ByVal acYear As String, ByVal custId As Integer, ByVal custName As String, ByVal mrpTotal As Decimal)
        Dim words As String = AmountInWords(DecOf(txtBillAmount.Text))
        Using cmd As New SqlCommand("dbo.SP_tbl_SalDetail_Insert", VistawinFunctions.ServerConnection, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Sa_EntDate_d", Date.Now)
            cmd.Parameters.AddWithValue("@Sa_Shift_vc", "")
            cmd.Parameters.AddWithValue("@Sa_SaveType_vc", bt)
            cmd.Parameters.AddWithValue("@Sa_BillDate_d", dtpBillDate.Value.Date)
            cmd.Parameters.AddWithValue("@Sa_BillTime_vc", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@Sa_InvNo_vc", invNo)
            cmd.Parameters.AddWithValue("@Sa_BillNo_n", billNo)
            cmd.Parameters.AddWithValue("@Sa_BType_vc", bt)
            cmd.Parameters.AddWithValue("@Sa_CusID_n", custId)
            cmd.Parameters.AddWithValue("@Sa_CusName_vc", custName)
            cmd.Parameters.AddWithValue("@Sa_GSTTotal_n", DecOf(txtTax.Text))
            cmd.Parameters.AddWithValue("@Sa_TotalAmt_n", DecOf(txtSubTotal.Text))
            cmd.Parameters.AddWithValue("@Sa_RoType_vc", "ROUND OFF")
            cmd.Parameters.AddWithValue("@Sa_RoAmt_n", RoundOffAmt(DecOf(txtBillAmount.Text)))
            cmd.Parameters.AddWithValue("@Sa_FinalTotal_n", DecOf(txtBillAmount.Text))
            cmd.Parameters.AddWithValue("@Sa_Words_vc", words)
            cmd.Parameters.AddWithValue("@Sa_GSTWords_vc", "")
            cmd.Parameters.AddWithValue("@Sa_AcYear_vc", acYear)
            cmd.Parameters.AddWithValue("@Sa_BillPoints_n", 0)
            cmd.Parameters.AddWithValue("@Sa_TodaySave_n", 0)
            cmd.Parameters.AddWithValue("@Sa_MRPTotal_n", mrpTotal)
            cmd.Parameters.AddWithValue("@Sa_UserID_vc", VSuperMarket.CurrentUserName)
            cmd.Parameters.AddWithValue("@Sa_UpDate_d", Date.Now)
            cmd.Parameters.AddWithValue("@Sa_EmpID_i", 0)
            cmd.Parameters.AddWithValue("@Sa_EmpPer_n", 0)
            cmd.Parameters.AddWithValue("@Sa_EmpAmt_n", 0)
            cmd.Parameters.AddWithValue("@Sa_COutAmt_n", 0)
            cmd.Parameters.AddWithValue("@Sa_OldBal_n", 0)
            cmd.Parameters.AddWithValue("@Sa_ComType_vc", "")
            cmd.Parameters.AddWithValue("@Sa_MarPer_n", 0)
            cmd.Parameters.AddWithValue("@Sa_MarAmt_n", 0)
            cmd.Parameters.AddWithValue("@Sa_DueDate_d", dtpBillDate.Value.Date)
            cmd.Parameters.AddWithValue("@Sa_Remark_vc", "")
            cmd.Parameters.AddWithValue("@Sa_DeliAdd_vc", "")
            cmd.Parameters.AddWithValue("@Sa_VehicleNo_vc", "")
            cmd.Parameters.AddWithValue("@Sa_RecNo_i", 0)
            cmd.Parameters.AddWithValue("@Sa_OldInvNo_vc", "")
            cmd.Parameters.AddWithValue("@Sa_BefDisAmt_n", DecOf(txtSubTotal.Text))
            cmd.Parameters.AddWithValue("@Sa_DiscountAmt_n", DecOf(txtDiscount.Text))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function RoundOffAmt(ByVal billAmt As Decimal) As Decimal
        Return VSuperMarket.RoundOff(billAmt) - billAmt
    End Function

    Private Sub SaveLine(ByVal trans As SqlTransaction, ByVal bt As String, ByVal invNo As String, ByVal custId As Integer, ByVal r As DataGridViewRow, ByVal isReturn As Boolean, Optional ByVal modifyType As String = "ADD")
        Dim stNo As Decimal = DecOf(r.Cells("StNo").Value)
        Dim code As Integer = CInt(DecOf(r.Cells("Code").Value))
        Dim mrp As Decimal = DecOf(r.Cells("MRP").Value)
        Dim rate As Decimal = DecOf(r.Cells("SaleRate").Value)
        Dim qty As Decimal = DecOf(r.Cells("Qty").Value)
        Dim disPer As Decimal = DecOf(r.Cells("DisPer").Value)
        Dim disAmt As Decimal = DecOf(r.Cells("DisAmt").Value)
        Dim gstPer As Decimal = DecOf(r.Cells("GSTPer").Value)
        Dim cgstAmt As Decimal = DecOf(r.Cells("CGSTAmt").Value)
        Dim sgstAmt As Decimal = DecOf(r.Cells("SGSTAmt").Value)
        Dim netRate As Decimal = DecOf(r.Cells("NetRate").Value)
        Dim netAmt As Decimal = DecOf(r.Cells("Amount").Value)
        Dim taxable As Decimal = If(gstPer > 0, Math.Round(netAmt * 100 / (100 + gstPer), 2), netAmt)
        Dim remark As String = Convert.ToString(r.Cells("Remark").Value)

        Using cmd As New SqlCommand("dbo.SP_tbl_SalParticular_Insert", VistawinFunctions.ServerConnection, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Sa_SaveTy_vc", bt)
            cmd.Parameters.AddWithValue("@Sa_BillDate_d", dtpBillDate.Value.Date)
            cmd.Parameters.AddWithValue("@Sa_InvNo_vc", invNo)
            cmd.Parameters.AddWithValue("@Sa_CusID_n", custId)
            cmd.Parameters.AddWithValue("@Sa_StNumber_n", stNo)
            cmd.Parameters.AddWithValue("@Sa_StCode_n", code)
            cmd.Parameters.AddWithValue("@Sa_PurRate_n", 0)
            cmd.Parameters.AddWithValue("@Sa_MRP_n", mrp)
            cmd.Parameters.AddWithValue("@Sa_SaleRate_n", rate)
            cmd.Parameters.AddWithValue("@Sa_ProDPer_n", disPer)
            cmd.Parameters.AddWithValue("@Sa_ProDAmt_n", disAmt)
            cmd.Parameters.AddWithValue("@Sa_NetRate_n", netRate)
            cmd.Parameters.AddWithValue("@Sa_Qty_i", qty)
            cmd.Parameters.AddWithValue("@Sa_FQty_i", 0)
            cmd.Parameters.AddWithValue("@Sa_TQty_i", qty)
            cmd.Parameters.AddWithValue("@Sa_QType_vc", "")
            cmd.Parameters.AddWithValue("@Sa_Pack_vc", "")
            cmd.Parameters.AddWithValue("@Sa_Taxable_n", taxable)
            cmd.Parameters.AddWithValue("@Sa_CGSTP_n", Math.Round(gstPer / 2, 2))
            cmd.Parameters.AddWithValue("@Sa_SGSTP_n", Math.Round(gstPer / 2, 2))
            cmd.Parameters.AddWithValue("@Sa_IGSTP_n", 0)
            cmd.Parameters.AddWithValue("@Sa_CGSTAmt_n", cgstAmt)
            cmd.Parameters.AddWithValue("@Sa_SGSTAmt_n", sgstAmt)
            cmd.Parameters.AddWithValue("@Sa_IGSTAmt_n", 0)
            cmd.Parameters.AddWithValue("@Sa_ProTotal_n", netAmt)
            cmd.Parameters.AddWithValue("@Sa_PRemark_vc", If(remark, ""))
            cmd.Parameters.AddWithValue("@Sa_SRTInv_vc", "")
            cmd.Parameters.AddWithValue("@Sa_DumRate_n", 0)
            cmd.Parameters.AddWithValue("@Sa_SaleMar_n", 0)
            cmd.Parameters.AddWithValue("@Sa_SaleMarAmt_n", 0)
            cmd.Parameters.AddWithValue("@Sa_Delivery_vc", "")
            cmd.Parameters.AddWithValue("@Sa_DisCalType_vc", "")
            cmd.ExecuteNonQuery()
        End Using

        ' stock movement (sale decreases, sales-return increases)
        Dim delta As Decimal = If(isReturn, qty, -qty)
        ApplyStockDelta(trans, stNo, code, delta)

        ' audit - legacy convention (see SP_tbl_SalDetail / SP_tbl_PurchDetail):
        '   St_Form_vc     = document/save type   (INVOICE / ESTIMATE / SALES RETURN)
        '   St_Modify_vc   = action               (ADD / EDIT)
        '   St_GodownSt_i  = line qty, St_ShopSt_i = free qty (none on this screen)
        '   St_TotalSt_i   = line total qty (TQty) - magnitude, direction implied by form+action
        Using cmd As New SqlCommand("INSERT INTO tbl_StockModify (St_InvNo_vc,St_Form_vc,St_Modify_vc,St_EDate_d,St_StockID_n,St_MediName_vc,St_GodownSt_i,St_ShopSt_i,St_TotalSt_i) " & _
                                    "VALUES (@I,@F,@M,GETDATE(),@C,@N,@G,@S,@Q)", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@I", invNo)
            cmd.Parameters.AddWithValue("@F", bt)
            cmd.Parameters.AddWithValue("@M", modifyType)
            cmd.Parameters.AddWithValue("@C", code)
            cmd.Parameters.AddWithValue("@N", Convert.ToString(r.Cells("ItemName").Value))
            cmd.Parameters.AddWithValue("@G", qty)
            cmd.Parameters.AddWithValue("@S", 0)
            cmd.Parameters.AddWithValue("@Q", qty)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' signed stock movement applied to both stock tables
    '   tbl_MultiStock.St_CurrStock_i = live stock shown on screen
    '   tbl_StockDtl.St_TotalSt_i     = godown stock (only for St_Decrease_vc='YES' items)
    Private Sub ApplyStockDelta(ByVal trans As SqlTransaction, ByVal stNo As Decimal, ByVal code As Integer, ByVal delta As Decimal)
        Using cmd As New SqlCommand("UPDATE tbl_MultiStock SET St_CurrStock_i = ISNULL(St_CurrStock_i,0) + @D WHERE St_StNumber_i = @SN", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@D", delta)
            cmd.Parameters.AddWithValue("@SN", stNo)
            cmd.ExecuteNonQuery()
        End Using
        Using cmd As New SqlCommand("UPDATE tbl_StockDtl SET St_TotalSt_i = ISNULL(St_TotalSt_i,0) + @D WHERE St_StockID_n = @C AND (St_Decrease_vc='YES' OR St_Decrease_vc IS NULL)", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@D", delta)
            cmd.Parameters.AddWithValue("@C", code)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' a Row Delete / Bill Delete restores (or re-takes) the line's stock and logs it
    Private Sub WriteStockAudit(ByVal trans As SqlTransaction, ByVal invNo As String, ByVal formType As String, ByVal modifyType As String, ByVal code As Integer, ByVal itemName As String, ByVal godown As Decimal, ByVal shop As Decimal, ByVal total As Decimal)
        Using cmd As New SqlCommand("INSERT INTO tbl_StockModify (St_InvNo_vc,St_Form_vc,St_Modify_vc,St_EDate_d,St_StockID_n,St_MediName_vc,St_GodownSt_i,St_ShopSt_i,St_TotalSt_i) " & _
                                    "VALUES (@I,@F,@M,GETDATE(),@C,@N,@G,@S,@Q)", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@I", invNo)
            cmd.Parameters.AddWithValue("@F", formType)
            cmd.Parameters.AddWithValue("@M", modifyType)
            cmd.Parameters.AddWithValue("@C", code)
            cmd.Parameters.AddWithValue("@N", If(itemName, ""))
            cmd.Parameters.AddWithValue("@G", godown)
            cmd.Parameters.AddWithValue("@S", shop)
            cmd.Parameters.AddWithValue("@Q", total)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' ==================================================================================
    '  printing
    ' ==================================================================================
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            If DgvBilling.Rows.Count = 0 Then
                MessageBox.Show("Nothing to print.", "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            MessageBox.Show(ex.Message, "POS Billing", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BuildPrintLines()
        printLines = New List(Of String)
        Dim invNo As String = If(LastInvoice <> "", LastInvoice, If(txtBillNo.Text.Replace(" (next)", ""), ""))
        printLines.Add(Company("Cmp_Name_vc"))
        printLines.Add(Company("Cmp_Address_vc"))
        Dim ph As String = Company("Cmp_PhoneNo_vc")
        If ph <> "" Then printLines.Add("Ph: " & ph)
        printLines.Add(New String("-"c, 44))
        printLines.Add("Bill No : " & invNo)
        printLines.Add("Date    : " & dtpBillDate.Value.ToString("dd-MM-yyyy HH:mm"))
        printLines.Add("Customer: " & txtCustName.Text)
        printLines.Add(New String("-"c, 44))
        printLines.Add(String.Format("{0,-17}{1,4}{2,7}{3,10}", "Item", "Qty", "Rate", "Amount"))
        printLines.Add(New String("-"c, 44))
        For Each r As DataGridViewRow In DgvBilling.Rows
            Dim nm As String = Convert.ToString(r.Cells("ItemName").Value)
            If nm.Length > 17 Then nm = nm.Substring(0, 17)
            printLines.Add(String.Format("{0,-17}{1,4}{2,7}{3,10}", nm, DecOf(r.Cells("Qty").Value).ToString("0.##"), DecOf(r.Cells("SaleRate").Value).ToString("0.00"), DecOf(r.Cells("Amount").Value).ToString("0.00")))
        Next
        printLines.Add(New String("-"c, 44))
        printLines.Add(String.Format("{0,29}{1,10}", "Sub Total", txtSubTotal.Text))
        printLines.Add(String.Format("{0,29}{1,10}", "Discount", txtDiscount.Text))
        printLines.Add(String.Format("{0,29}{1,10}", "Taxable", txtTaxable.Text))
        printLines.Add(String.Format("{0,29}{1,10}", "CGST", txtCGST.Text))
        printLines.Add(String.Format("{0,29}{1,10}", "SGST", txtSGST.Text))
        printLines.Add(String.Format("{0,29}{1,10}", "Round Off", txtRoundOff.Text))
        printLines.Add(String.Format("{0,29}{1,10}", "BILL AMOUNT", txtBillAmount.Text))
        printLines.Add(New String("-"c, 44))
        printLines.Add(AmountInWords(DecOf(txtBillAmount.Text)))
        printLines.Add("")
        printLines.Add("        Thank you, visit again!")
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
