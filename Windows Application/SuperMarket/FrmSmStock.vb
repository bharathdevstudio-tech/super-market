Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

''' <summary>
''' SuperMarket - Stock / Inventory.
'''   Reads   : tbl_StockDtl (item master) + tbl_MultiStock (POS stock + rates)
'''   Adjust  : tbl_MultiStock.St_CurrStock_i and tbl_StockDtl.St_TotalSt_i
'''             plus one audit row in tbl_StockModify   - one transaction.
'''   Ledger  : movement history from tbl_StockModify.
''' All SQL is parameterized.
''' </summary>
Public Class FrmSmStock

    Dim dtStock As New DataTable
    Dim dictCat As New Dictionary(Of String, Integer)
    Dim SelStNo As Decimal = 0
    Dim SelCode As Decimal = 0
    Dim SelStock As Decimal = 0
    Dim SelReorder As Decimal = 0
    Dim SelName As String = ""
    Dim Barcode As String = ""
    Dim suppressEvents As Boolean = False
    Dim Loading As Boolean = True
    Dim printLines As New List(Of String)
    Dim printIndex As Integer = 0

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                Me.Close()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FrmSmStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VSuperMarket.EnsureConnection()
            If Not VSuperMarket.HasPermission("FrmSmStock", "VIEW") Then
                MessageBox.Show("You do not have permission to view stock.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
                Exit Sub
            End If
            cboAdjustType.Items.Clear()
            cboAdjustType.Items.AddRange(New Object() {"INCREASE", "DECREASE"})
            cboAdjustType.SelectedIndex = 0
            LoadCategories()
            LoadGrid()
            Loading = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Stock", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==================================================================================
    '  data loading
    ' ==================================================================================
    Private Sub LoadCategories()
        cboCategory.Items.Clear()
        dictCat.Clear()
        cboCategory.Items.Add("ALL")
        Using cmd As New SqlCommand("SELECT Category_Code_n, Category_Name_vc FROM tbl_Category ORDER BY Category_Name_vc", VistawinFunctions.ServerConnection)
            Using dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    Dim disp As String = Convert.ToString(dr("Category_Code_n")) & " - " & Convert.ToString(dr("Category_Name_vc"))
                    cboCategory.Items.Add(disp)
                    dictCat(disp.ToUpper()) = Convert.ToInt32(dr("Category_Code_n"))
                End While
            End Using
        End Using
        cboCategory.SelectedIndex = 0
    End Sub

    Private Sub LoadGrid()
        Try
            suppressEvents = True
            Dim sql As String = "SELECT ISNULL(s.St_StockID_n,0) AS Code, ISNULL(s.St_ProName_vc,'') AS ItemName, " & _
                                "ISNULL(c.Category_Name_vc,'') AS Category, ISNULL(b.Brand_Name_vc,'') AS Brand, " & _
                                "ISNULL(s.St_Unit_vc,'') AS Unit, ISNULL(s.St_HSN_vc,'') AS HSN, " & _
                                "ISNULL(m.St_MRPRate_n,0) AS MRP, ISNULL(m.St_PurRate_n,0) AS PurRate, ISNULL(m.St_SaleRate_n,0) AS SaleRate, " & _
                                "ISNULL(m.St_CurrStock_i,0) AS Stock, ISNULL(s.St_TotalSt_i,0) AS Godown, ISNULL(s.St_RemiStock_i,0) AS Reorder, " & _
                                "ISNULL(m.St_Show_vc,'YES') AS ShowIt, ISNULL(m.St_StNumber_i,0) AS StNo, ISNULL(m.St_Barcode_vc,'') AS Barcode " & _
                                "FROM tbl_StockDtl s " & _
                                "OUTER APPLY (SELECT TOP 1 * FROM tbl_MultiStock mm WHERE mm.St_StockID_n = s.St_StockID_n ORDER BY mm.St_StNumber_i) m " & _
                                "LEFT JOIN tbl_Category c ON c.Category_Code_n = s.St_CatID_i " & _
                                "LEFT JOIN tbl_Brand b ON b.Brand_Code_n = s.St_BrandID_i WHERE 1=1"

            Using cmd As New SqlCommand()
                cmd.Connection = VistawinFunctions.ServerConnection

                Dim q As String = txtSearch.Text.Trim()
                If q <> "" Then
                    sql &= " AND (s.St_ProName_vc LIKE @Q OR CONVERT(varchar(20),s.St_StockID_n) LIKE @Q OR ISNULL(m.St_Barcode_vc,'') LIKE @Q)"
                    cmd.Parameters.AddWithValue("@Q", "%" & q & "%")
                End If

                Dim catId As Integer = 0
                If cboCategory.SelectedIndex > 0 Then
                    dictCat.TryGetValue(Convert.ToString(cboCategory.SelectedItem).ToUpper(), catId)
                End If
                If catId > 0 Then
                    sql &= " AND ISNULL(s.St_CatID_i,0) = @CAT"
                    cmd.Parameters.AddWithValue("@CAT", catId)
                End If

                If chkLowStock.Checked Then
                    sql &= " AND ISNULL(s.St_RemiStock_i,0) > 0 AND ISNULL(m.St_CurrStock_i,0) <= ISNULL(s.St_RemiStock_i,0)"
                End If

                sql &= " ORDER BY s.St_ProName_vc"
                cmd.CommandText = sql

                dtStock = New DataTable
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtStock)
                End Using
            End Using

            FillGrid()
        Finally
            suppressEvents = False
        End Try
    End Sub

    Private Sub FillGrid()
        DgvStock.Rows.Clear()
        For Each r As DataRow In dtStock.Rows
            Dim idx As Integer = DgvStock.Rows.Add()
            Dim g As DataGridViewRow = DgvStock.Rows(idx)
            g.Cells("ColCode").Value = DecOf(r("Code"))
            g.Cells("ColItemName").Value = Convert.ToString(r("ItemName"))
            g.Cells("ColCategory").Value = Convert.ToString(r("Category"))
            g.Cells("ColBrand").Value = Convert.ToString(r("Brand"))
            g.Cells("ColUnit").Value = Convert.ToString(r("Unit"))
            g.Cells("ColHSN").Value = Convert.ToString(r("HSN"))
            g.Cells("ColMRP").Value = DecOf(r("MRP"))
            g.Cells("ColPurRate").Value = DecOf(r("PurRate"))
            g.Cells("ColSaleRate").Value = DecOf(r("SaleRate"))
            g.Cells("ColStock").Value = DecOf(r("Stock"))
            g.Cells("ColTotal").Value = DecOf(r("Godown"))
            g.Cells("ColReorder").Value = DecOf(r("Reorder"))
            g.Cells("ColValue").Value = DecOf(r("Stock")) * DecOf(r("PurRate"))
            g.Cells("ColShow").Value = Convert.ToString(r("ShowIt"))
            g.Cells("ColStNo").Value = DecOf(r("StNo")).ToString("0")
            g.Tag = Convert.ToString(r("Barcode"))
        Next
        If DgvStock.Rows.Count > 0 Then DgvStock.CurrentCell = DgvStock.Rows(0).Cells("ColItemName")
    End Sub

    Private Function DecOf(ByVal o As Object) As Decimal
        If o Is Nothing OrElse Convert.IsDBNull(o) Then Return 0
        Dim d As Decimal = 0
        Decimal.TryParse(Convert.ToString(o), d)
        Return d
    End Function

    ' ==================================================================================
    '  low stock highlight + selection
    ' ==================================================================================
    Private Sub DgvStock_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DgvStock.CellFormatting
        If e.RowIndex < 0 Then Exit Sub
        Try
            Dim g As DataGridViewRow = DgvStock.Rows(e.RowIndex)
            Dim stock As Decimal = DecOf(g.Cells("ColStock").Value)
            Dim reorder As Decimal = DecOf(g.Cells("ColReorder").Value)
            If stock <= 0 Then
                e.CellStyle.BackColor = Color.MistyRose
                e.CellStyle.ForeColor = Color.DarkRed
            ElseIf reorder > 0 AndAlso stock <= reorder Then
                e.CellStyle.BackColor = Color.LightYellow
            End If
        Catch
        End Try
    End Sub

    Private Sub DgvStock_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgvStock.SelectionChanged
        If suppressEvents Then Exit Sub
        If DgvStock.CurrentRow Is Nothing Then Exit Sub
        Dim g As DataGridViewRow = DgvStock.CurrentRow
        SelCode = DecOf(g.Cells("ColCode").Value)
        SelStNo = DecOf(g.Cells("ColStNo").Value)
        SelStock = DecOf(g.Cells("ColStock").Value)
        SelReorder = DecOf(g.Cells("ColReorder").Value)
        Barcode = Convert.ToString(g.Tag)
        SelName = Convert.ToString(g.Cells("ColItemName").Value)
        lblSelItem.Text = SelName & "  (Stock: " & SelStock.ToString("0.###") & ")"
        txtAdjustQty.Text = "0"
        txtReason.Text = ""
        lblAdjustMsg.Text = ""
        LoadLedger()
    End Sub

    Private Sub LoadLedger()
        DgvLedger.Rows.Clear()
        If SelCode <= 0 Then Exit Sub
        Using cmd As New SqlCommand("SELECT TOP 100 St_EDate_d, St_Form_vc, St_Modify_vc, St_InvNo_vc, St_TotalSt_i, St_Remark_vc " & _
                                    "FROM tbl_StockModify WHERE St_StockID_n = @C ORDER BY St_RecordNo_i DESC", VistawinFunctions.ServerConnection)
            cmd.Parameters.AddWithValue("@C", SelCode)
            Using dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    Dim idx As Integer = DgvLedger.Rows.Add()
                    Dim lr As DataGridViewRow = DgvLedger.Rows(idx)
                    Dim dt As DateTime
                    If DateTime.TryParse(Convert.ToString(dr("St_EDate_d")), dt) Then
                        lr.Cells("LDate").Value = dt.ToString("dd-MM-yyyy HH:mm")
                    Else
                        lr.Cells("LDate").Value = ""
                    End If
                    lr.Cells("LType").Value = Convert.ToString(dr("St_Form_vc")) & " / " & Convert.ToString(dr("St_Modify_vc"))
                    lr.Cells("LRef").Value = Convert.ToString(dr("St_InvNo_vc"))
                    Dim q As Decimal = Convert.ToDecimal(If(Convert.IsDBNull(dr("St_TotalSt_i")), 0D, dr("St_TotalSt_i")))
                    Dim mv As Decimal = q * MovementSign(Convert.ToString(dr("St_Form_vc")), Convert.ToString(dr("St_Modify_vc")))
                    lr.Cells("LQty").Value = If(mv > 0, "+", "") & mv.ToString("0.###")
                    lr.Cells("LRemark").Value = Convert.ToString(dr("St_Remark_vc"))
                End While
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' tbl_StockModify stores the movement magnitude (legacy TQty). The direction comes
    ''' from the document type (St_Form_vc) plus the action (St_Modify_vc): ADD/EDIT post
    ''' the document's own effect, ROW DELETE / BILL DELETE reverse it.
    ''' </summary>
    Private Function MovementSign(ByVal formType As String, ByVal modifyType As String) As Integer
        Dim doc As String = UCase(Trim(formType))
        Dim act As String = UCase(Trim(modifyType))
        If act = "ADJUST IN" Then Return 1
        If act = "ADJUST OUT" Then Return -1
        Dim reversing As Boolean = (act = "BILL DELETE" OrElse act = "ROW DELETE")
        Dim docSign As Integer = 1
        Select Case doc
            Case "INVOICE", "ESTIMATE", "SALES", "PURCHASE RETURN", "DAMAGE", "WASTAGE"
                docSign = -1
            Case "SALES RETURN", "PURCHASE", "STOCK IN"
                docSign = 1
        End Select
        If reversing Then Return -docSign
        Return docSign
    End Function

    ' ==================================================================================
    '  filters
    ' ==================================================================================
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Try
            LoadGrid()
            lblAdjustMsg.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Stock", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' The Search box doubles as a barcode box - the same idea as the POS
    ''' "Item / Barcode" field.  A hardware scanner types the digits and then
    ''' sends Enter, so Enter is where a scan is recognised:
    '''   * exact barcode / item code -> drop the other filters, reload and put
    '''     the cursor on that item (its stock can then be adjusted)
    '''   * anything else             -> the usual name / code filter
    ''' </summary>
    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            ScanOrSearch()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub ScanOrSearch()
        Try
            lblAdjustMsg.Text = ""
            Dim t As String = txtSearch.Text.Trim()
            If t = "" Then
                LoadGrid()
                Exit Sub
            End If

            Dim code As Decimal = ScanLookupCode(t)
            If code <= 0 Then
                ' not a known barcode / item code -> keep the plain search behaviour
                LoadGrid()
                Exit Sub
            End If

            ' a scan must always find its item - drop the filters that could hide it
            Loading = True
            cboCategory.SelectedIndex = 0
            chkLowStock.Checked = False
            Loading = False
            txtSearch.Text = t
            LoadGrid()
            SelectByCode(code)
            ' LoadGrid suppresses the selection event while it refills the grid, so
            ' re-sync the selection fields + ledger with the scanned row (same fix
            ' BtnApply_Click uses after an adjustment).
            DgvStock_SelectionChanged(Me, EventArgs.Empty)
            If SelCode = code Then
                lblAdjustMsg.Text = "Scanned: " & SelName & "   (Stock: " & SelStock.ToString("0.###") & ")"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Stock", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Exact match on the item barcode first, then on the item code.  Uses the
    ''' same tbl_MultiStock row the grid shows (TOP 1 by St_StNumber_i) so the
    ''' code returned is the one LoadGrid will display.
    ''' Returns St_StockID_n, or 0 when the text is neither a barcode nor a code.
    ''' </summary>
    Private Function ScanLookupCode(ByVal text As String) As Decimal
        Try
            Using cmd As New SqlCommand("SELECT TOP 1 ISNULL(s.St_StockID_n,0) FROM tbl_StockDtl s " & _
                                        "OUTER APPLY (SELECT TOP 1 * FROM tbl_MultiStock mm WHERE mm.St_StockID_n = s.St_StockID_n ORDER BY mm.St_StNumber_i) m " & _
                                        "WHERE ISNULL(m.St_Barcode_vc,'') = @B OR CONVERT(varchar(20),s.St_StockID_n) = @B", VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@B", text)
                Dim o As Object = cmd.ExecuteScalar()
                If o Is Nothing OrElse Convert.IsDBNull(o) Then Return 0
                Return Convert.ToDecimal(o)
            End Using
        Catch
            Return 0
        End Try
    End Function

    Private Sub chkLowStock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLowStock.CheckedChanged
        If Loading Then Exit Sub
        Try
            LoadGrid()
        Catch
        End Try
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        If Loading Then Exit Sub
        If suppressEvents Then Exit Sub
        If cboCategory.SelectedIndex < 0 Then Exit Sub
        Try
            LoadGrid()
        Catch
        End Try
    End Sub

    ' ==================================================================================
    '  stock adjustment
    ' ==================================================================================
    Private Sub BtnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnApply.Click
        lblAdjustMsg.Text = ""
        If SelStNo <= 0 Then
            MessageBox.Show("Select an item first.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim qty As Decimal = DecOf(txtAdjustQty.Text)
        If qty <= 0 Then
            MessageBox.Show("Enter a quantity greater than zero.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAdjustQty.Focus()
            Exit Sub
        End If
        If Not VSuperMarket.HasPermission("FrmSmStock", "EDIT") Then
            MessageBox.Show("You do not have permission to adjust stock.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim increasing As Boolean = (cboAdjustType.SelectedIndex = 0)
        Dim delta As Decimal = If(increasing, qty, -qty)
        If Not increasing AndAlso (SelStock - qty) < 0 Then
            If MessageBox.Show("This will leave a negative stock of " & (SelStock - qty).ToString("0.###") & ". Continue?", _
                               "Stock", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Exit Sub
            End If
        End If

        Dim reason As String = txtReason.Text.Trim()
        If reason = "" Then reason = If(increasing, "STOCK IN", "STOCK OUT")
        Dim itemName As String = If(SelName, "")
        Dim Trans As SqlTransaction = Nothing
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction()

            Using cmd As New SqlCommand("UPDATE tbl_MultiStock SET St_CurrStock_i = ISNULL(St_CurrStock_i,0) + @D, St_UpDate_d=GETDATE(), St_UID_vc=@U WHERE St_StNumber_i = @SN", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@D", delta)
                cmd.Parameters.AddWithValue("@U", VSuperMarket.CurrentUserName)
                cmd.Parameters.AddWithValue("@SN", SelStNo)
                cmd.ExecuteNonQuery()
            End Using

            Using cmd As New SqlCommand("UPDATE tbl_StockDtl SET St_TotalSt_i = ISNULL(St_TotalSt_i,0) + @D WHERE St_StockID_n = @C AND (St_Decrease_vc='YES' OR St_Decrease_vc IS NULL)", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@D", delta)
                cmd.Parameters.AddWithValue("@C", SelCode)
                cmd.ExecuteNonQuery()
            End Using

            Using cmd As New SqlCommand("INSERT INTO tbl_StockModify (St_EDate_d,St_Form_vc,St_Modify_vc,St_StockID_n,St_MediName_vc,St_GodownSt_i,St_ShopSt_i,St_TotalSt_i,St_InvNo_vc,St_Remark_vc) " & _
                                        "VALUES (GETDATE(),@F,@M,@C,@N,@G,@S,@Q,@I,@R)", VistawinFunctions.ServerConnection, Trans)
                cmd.Parameters.AddWithValue("@F", "STOCK ADJUST")
                cmd.Parameters.AddWithValue("@M", If(increasing, "ADJUST IN", "ADJUST OUT"))
                cmd.Parameters.AddWithValue("@C", SelCode)
                cmd.Parameters.AddWithValue("@N", itemName)
                cmd.Parameters.AddWithValue("@G", qty)
                cmd.Parameters.AddWithValue("@S", 0)
                cmd.Parameters.AddWithValue("@Q", qty)
                cmd.Parameters.AddWithValue("@I", "")
                cmd.Parameters.AddWithValue("@R", reason)
                cmd.ExecuteNonQuery()
            End Using

            Trans.Commit()
            VSuperMarket.LogActivity("FrmSmStock", "EDIT", If(increasing, "ADJUST IN ", "ADJUST OUT ") & qty.ToString("0.###") & " - " & itemName)
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message, "Stock", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try

        txtAdjustQty.Text = "0"
        txtReason.Text = ""
        LoadGrid()
        SelectByCode(SelCode)
        ' re-sync the selection fields and the ledger with the new stock level
        DgvStock_SelectionChanged(Me, EventArgs.Empty)
        lblAdjustMsg.Text = "Stock adjusted successfully."
    End Sub

    Private Sub SelectByCode(ByVal code As Decimal)
        Try
            For Each g As DataGridViewRow In DgvStock.Rows
                If DecOf(g.Cells("ColCode").Value) = code Then
                    DgvStock.CurrentCell = g.Cells("ColItemName")
                    Exit For
                End If
            Next
        Catch
        End Try
    End Sub

    ' ==================================================================================
    '  ledger toggle + print
    ' ==================================================================================
    Private Sub BtnLedger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLedger.Click
        GroupLedger.Visible = Not GroupLedger.Visible
        BtnLedger.Text = If(GroupLedger.Visible, "Hide Ledger", "Ledger")
        If GroupLedger.Visible Then LoadLedger()
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            If DgvStock.Rows.Count = 0 Then
                MessageBox.Show("Nothing to print.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            BuildPrintLines()
            printIndex = 0
            Dim pd As New Printing.PrintDocument
            pd.DefaultPageSettings.Margins = New Printing.Margins(50, 60, 40, 40)
            AddHandler pd.PrintPage, AddressOf OnPrintPage
            Dim prev As New PrintPreviewDialog
            prev.Document = pd
            prev.WindowState = FormWindowState.Maximized
            prev.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Stock", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BuildPrintLines()
        printLines = New List(Of String)
        printLines.Add("STOCK REPORT")
        printLines.Add(Company("Cmp_Name_vc"))
        printLines.Add(Company("Cmp_Address_vc"))
        printLines.Add("As on : " & Date.Now.ToString("dd-MM-yyyy HH:mm"))

        Dim flt As String = ""
        If txtSearch.Text.Trim <> "" Then flt &= "Name~" & txtSearch.Text.Trim() & " "
        If cboCategory.SelectedIndex > 0 Then flt &= "Category~" & Convert.ToString(cboCategory.SelectedItem) & " "
        If chkLowStock.Checked Then flt &= "LowStock "
        printLines.Add("Filter: " & If(flt.Trim = "", "ALL", flt.Trim()))

        printLines.Add(New String("-"c, 62))
        printLines.Add(String.Format("{0,-24}{1,10}{2,10}{3,12}", "Item", "Stock", "Reorder", "Value"))
        printLines.Add(New String("-"c, 62))
        Dim totalValue As Decimal = 0
        For Each g As DataGridViewRow In DgvStock.Rows
            Dim nm As String = Convert.ToString(g.Cells("ColItemName").Value)
            If nm.Length > 24 Then nm = nm.Substring(0, 24)
            Dim val As Decimal = DecOf(g.Cells("ColValue").Value)
            totalValue += val
            printLines.Add(String.Format("{0,-24}{1,10}{2,10}{3,12}", nm, DecOf(g.Cells("ColStock").Value).ToString("0.###"), DecOf(g.Cells("ColReorder").Value).ToString("0.###"), val.ToString("0.00")))
        Next
        printLines.Add(New String("-"c, 62))
        printLines.Add(String.Format("{0,-24}{1,32}", "Total Stock Value", totalValue.ToString("0.00")))
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
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

    Private Sub OnPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim f As New Font("Consolas", 9)
        Dim y As Single = e.MarginBounds.Top
        While printIndex < printLines.Count
            e.Graphics.DrawString(printLines(printIndex), f, Brushes.Black, e.MarginBounds.Left, y)
            y += f.GetHeight(e.Graphics)
            printIndex += 1
        End While
    End Sub

End Class
