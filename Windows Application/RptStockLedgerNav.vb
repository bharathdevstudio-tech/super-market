Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports
Imports System.ComponentModel
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class RptStockLedgerNav
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

    Public CusIDRC As String = ""
    Public CusNameRC As String = ""

    Public LedProName As String
    Public LedStockID As String
    Public OpenStLed As Boolean = False


    Private doc As Document
    Private writer As PdfWriter
    Dim pdfPath As String = ""
    Dim pageSizeChk As Rectangle = PageSize.A4
    Dim PdfCancel As Boolean = False

    Dim TblcellFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 8)
    Dim NormalFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 9)
    Dim TblHeaderFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.BLACK)
    Dim TblHeaderColor As BaseColor = BaseColor.LIGHT_GRAY
    Dim TblGrandTotalFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.BLACK)
    Public Class InvoicePageEvent
        Inherits PdfPageEventHelper

        Private bf As BaseFont = Nothing
        Private totalPages As PdfTemplate = Nothing
        Private FooterFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Private TotalPageCount As Integer = 0

        Public Overrides Sub OnOpenDocument(ByVal writer As PdfWriter, ByVal document As Document)
            Try
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
                totalPages = writer.DirectContent.CreateTemplate(50, 50)
            Catch ex As Exception
            End Try
        End Sub

        Public Overrides Sub OnEndPage(ByVal writer As PdfWriter, ByVal document As Document)
            Try
                TotalPageCount = writer.PageNumber
                Dim cb As PdfContentByte = writer.DirectContent

                cb.SetLineWidth(1.5F)
                cb.Rectangle(10, 10, document.PageSize.Width - 20, document.PageSize.Height - 20)
                cb.Stroke()

                If bf IsNot Nothing Then
                    Dim pageN As Integer = writer.PageNumber
                    Dim text As String = "Page " & pageN.ToString() & " of "
                    Dim len As Single = bf.GetWidthPoint(text, 9)
                    Dim x As Single = document.PageSize.Width - 100
                    Dim y As Single = 20

                    cb.BeginText()
                    cb.SetFontAndSize(bf, 9)
                    cb.SetTextMatrix(x, y)
                    cb.ShowText(text)
                    cb.EndText()

                    If totalPages IsNot Nothing Then
                        cb.AddTemplate(totalPages, x + len, y)
                    End If
                End If

                If FooterFont IsNot Nothing Then
                    ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, New Phrase("----", FooterFont), document.PageSize.Width / 2, 15, 0)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Overrides Sub OnCloseDocument(ByVal writer As PdfWriter, ByVal document As Document)
            If totalPages IsNot Nothing AndAlso bf IsNot Nothing Then
                totalPages.BeginText()
                totalPages.SetFontAndSize(bf, 9)
                totalPages.SetTextMatrix(0, 0)
                totalPages.ShowText(TotalPageCount.ToString())
                totalPages.EndText()
            End If
        End Sub
    End Class
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
    Private Sub RptStockLedgerNav_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        MonthCalendar2.Hide()
        txtFromDate2.Text = "01/" + VistawinFunctions.TodayDate.Month.ToString + "/" + VistawinFunctions.TodayDate.Year.ToString
        txtToDate2.Text = Format(CDate(VistawinFunctions.TodayDate), "dd/MM/yyyy")

        DgvProList.Hide()
        DgvProList.Size = New System.Drawing.Size(380, 300)
        DgvProList.BringToFront()

        If OpenStLed Then
            txtStockID.Text = LedStockID
            RemoveHandler txtProName.TextChanged, AddressOf txtProName_TextChanged
            txtProName.Text = LedProName
            AddHandler txtProName.TextChanged, AddressOf txtProName_TextChanged
            CbAll2.Checked = True
            If VistawinFunctions.ProjectTypechk = "INVEN" Then
                Me.InvenStockLedger()
            Else

            End If
        Else
        End If
    End Sub

    Private Sub MonthCalendar2_DateSelected(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar2.DateSelected
        If FromBool = True Then
            If Format(CDate(txtToDate2.Text), "yyyy/MM/dd") < Format(CDate(MonthCalendar2.SelectionRange.Start), "yyyy/MM/dd") Then
                MsgBox("Please,Check the date")
                Exit Sub
            End If
            txtFromDate2.Text = Format(CDate(MonthCalendar2.SelectionRange.Start), "dd/MM/yyyy")
            MonthCalendar2.Hide()
        ElseIf ToBool = True Then
            If Format(CDate(MonthCalendar2.SelectionRange.Start), "yyyy/MM/dd") < Format(CDate(txtFromDate2.Text), "yyyy/MM/dd") Then
                MsgBox("Please,Check the date")
                Exit Sub
            End If
            txtToDate2.Text = Format(CDate(MonthCalendar2.SelectionRange.Start), "dd/MM/yyyy")
            MonthCalendar2.Hide()
        End If
    End Sub
    Private Sub btnFrom2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrom2.Click
        FromBool = True
        ToBool = False
        MonthCalendar2.Show()

        MonthCalendar2.Location = New System.Drawing.Point(txtFromDate2.Location.X, txtFromDate2.Location.Y + 30)

    End Sub

    Private Sub btnTo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTo2.Click
        ToBool = True
        FromBool = False
        MonthCalendar2.Show()
        MonthCalendar2.Location = New System.Drawing.Point(txtToDate2.Location.X, txtToDate2.Location.Y + 30)
    End Sub
    Private Sub btnPreview2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview2.Click

        If VistawinFunctions.ProjectTypechk = "INVEN" Then
            Me.InvenStockLedger()
        ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then
            Me.ClinicStockLedger()
        Else

        End If

    End Sub
    Private Sub ClinicStockLedger()
        If txtProName.Text.Trim = "" Then
            MsgBox("Select Product")
            txtProName.Focus()
            Exit Sub
        End If

        Dim StockID As Integer = Val(txtStockID.Text)
        Dim OpeningQty As Decimal = 0
        Dim Balance As Decimal = 0

        If CbAll2.Checked Then

            command = New SqlCommand("SELECT ISNULL(MIN(BillDate),GETDATE()) FROM StockLedger WHERE MediID=" & StockID, VistawinFunctions.ServerConnection)
            txtFromDate2.Text = Format(CDate(command.ExecuteScalar), "dd/MM/yyyy")

            command = New SqlCommand("SELECT ISNULL(MAX(BillDate),GETDATE()) FROM StockLedger WHERE MediID=" & StockID, VistawinFunctions.ServerConnection)
            txtToDate2.Text = Format(CDate(command.ExecuteScalar), "dd/MM/yyyy")
        End If

        DGVDayDetails.Rows.Clear()

        'Opening Balance
        Dim sql As String = "SELECT ISNULL(SUM(CASE WHEN SaveType IN('PURCHASE','SALES RETURN') THEN TQty  WHEN SaveType IN('INVOICE','SAMPLE','DAMAGE','PURCHASE RETURN') THEN -TQty ELSE 0 END),0) FROM StockLedger WHERE BillDate<@FromDate AND MediID=@StockID"

        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
            cmd.Parameters.AddWithValue("@FromDate", CDate(txtFromDate2.Text))
            cmd.Parameters.AddWithValue("@StockID", StockID)
            OpeningQty = CDec(cmd.ExecuteScalar())
        End Using

        Balance = OpeningQty

        DGVDayDetails.Rows.Add()
        DGVDayDetails.Rows(0).Cells("RecNarrationS").Value = "Opening Balance"
        DGVDayDetails.Rows(0).Cells("BalanceAmtS").Value = Balance

        'Transactions
        sql = "SELECT BillDate,SaveType,InvNo,CusName,('StockNo:'+CONVERT(VARCHAR,StockCode)+' ,MRP:'+CONVERT(VARCHAR,MRPRate)) AS ProDtl,TQty FROM StockLedger WHERE MediID=@StockID AND BillDate BETWEEN @FromDate AND @ToDate ORDER BY BillDate,InvNo"
        Dim ds As New DataSet
        Using da As New SqlDataAdapter(sql, VistawinFunctions.ServerConnection)
            da.SelectCommand.Parameters.AddWithValue("@StockID", StockID)
            da.SelectCommand.Parameters.AddWithValue("@FromDate", CDate(txtFromDate2.Text))
            da.SelectCommand.Parameters.AddWithValue("@ToDate", CDate(txtToDate2.Text))
            da.Fill(ds)
        End Using

        Dim PurchaseTotal As Decimal = 0
        Dim SalesTotal As Decimal = 0
        Dim RowNo As Integer = 1

        For Each dr As DataRow In ds.Tables(0).Rows
            Dim Qty As Decimal = CDec(dr("TQty"))
            Dim Purchase As Decimal = 0
            Dim Sales As Decimal = 0

            Select Case dr("SaveType").ToString().ToUpper()
                Case "PURCHASE", "SALES RETURN"
                    Purchase = Qty
                    Balance += Qty
                    PurchaseTotal += Qty
                Case "INVOICE", "SAMPLE", "DAMAGE", "PURCHASE RETURN"
                    Sales = Qty
                    Balance -= Qty
                    SalesTotal += Qty
            End Select

            DGVDayDetails.Rows.Add()
            With DGVDayDetails.Rows(RowNo)
                .Cells("SNoS").Value = RowNo
                .Cells("RecDateS").Value = Format(CDate(dr("BillDate")), "dd/MM/yyyy")
                .Cells("RecTypeS").Value = dr("SaveType")
                .Cells("RecNumberS").Value = dr("InvNo")
                .Cells("RecNarrationS").Value = dr("CusName")
                .Cells("ProDtlS").Value = dr("ProDtl")
                .Cells("PayableAmtS").Value = Purchase
                .Cells("PaidAmtS").Value = Sales
                .Cells("BalanceAmtS").Value = Balance
                .Cells("TickS").Value = "NO"
            End With
            RowNo += 1
        Next
        txtTotalPurchase.Text = PurchaseTotal.ToString("0.00")
        txtTotalSales.Text = SalesTotal.ToString("0.00")
        txtBalanceAmt.Text = Balance.ToString("0.00")
    End Sub
    Private Sub InvenStockLedger()

        If txtProName.Text.Trim = "" Then
            MsgBox("Select Product")
            txtProName.Focus()
            Exit Sub
        End If

        Dim StockID As Integer = Val(txtStockID.Text)
        Dim OpeningQty As Decimal = 0
        Dim Balance As Decimal = 0

        If CbAll2.Checked Then
            command = New SqlCommand("SELECT ISNULL(MIN(BillDate),GETDATE()) FROM StockLedger WHERE StockCode=" & StockID, VistawinFunctions.ServerConnection)
            txtFromDate2.Text = Format(CDate(command.ExecuteScalar), "dd/MM/yyyy")

            command = New SqlCommand("SELECT ISNULL(MAX(BillDate),GETDATE()) FROM StockLedger WHERE StockCode=" & StockID, VistawinFunctions.ServerConnection)
            txtToDate2.Text = Format(CDate(command.ExecuteScalar), "dd/MM/yyyy")
        End If

        DGVDayDetails.Rows.Clear()

        'Opening Balance
        Dim sql As String = "SELECT ISNULL(SUM(CASE WHEN SaveType IN('PURCHASE','SALES RETURN') THEN TQty  WHEN SaveType IN('INVOICE','SAMPLE','DAMAGE','PURCHASE RETURN') THEN -TQty ELSE 0 END),0) FROM StockLedger WHERE BillDate<@FromDate AND StockCode=@StockID"

        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
            cmd.Parameters.AddWithValue("@FromDate", CDate(txtFromDate2.Text))
            cmd.Parameters.AddWithValue("@StockID", StockID)
            OpeningQty = CDec(cmd.ExecuteScalar())
        End Using

        Balance = OpeningQty

        DGVDayDetails.Rows.Add()
        DGVDayDetails.Rows(0).Cells("RecNarrationS").Value = "Opening Balance"
        DGVDayDetails.Rows(0).Cells("BalanceAmtS").Value = Balance

        'Transactions
        sql = "SELECT BillDate,SaveType,InvNo,CusName,('StockNo:'+CONVERT(VARCHAR,StNumber)+' ,MRP:'+CONVERT(VARCHAR,MRPRate)) AS ProDtl,TQty FROM StockLedger WHERE StockCode=@StockID AND BillDate BETWEEN @FromDate AND @ToDate ORDER BY BillDate,InvNo"
        Dim ds As New DataSet
        Using da As New SqlDataAdapter(sql, VistawinFunctions.ServerConnection)
            da.SelectCommand.Parameters.AddWithValue("@StockID", StockID)
            da.SelectCommand.Parameters.AddWithValue("@FromDate", CDate(txtFromDate2.Text))
            da.SelectCommand.Parameters.AddWithValue("@ToDate", CDate(txtToDate2.Text))
            da.Fill(ds)
        End Using

        Dim PurchaseTotal As Decimal = 0
        Dim SalesTotal As Decimal = 0
        Dim RowNo As Integer = 1

        For Each dr As DataRow In ds.Tables(0).Rows
            Dim Qty As Decimal = CDec(dr("TQty"))
            Dim Purchase As Decimal = 0
            Dim Sales As Decimal = 0

            Select Case dr("SaveType").ToString().ToUpper()
                Case "PURCHASE", "SALES RETURN"
                    Purchase = Qty
                    Balance += Qty
                    PurchaseTotal += Qty
                Case "INVOICE", "SAMPLE", "DAMAGE", "PURCHASE RETURN"
                    Sales = Qty
                    Balance -= Qty
                    SalesTotal += Qty
            End Select

            DGVDayDetails.Rows.Add()
            With DGVDayDetails.Rows(RowNo)
                .Cells("SNoS").Value = RowNo
                .Cells("RecDateS").Value = Format(CDate(dr("BillDate")), "dd/MM/yyyy")
                .Cells("RecTypeS").Value = dr("SaveType")
                .Cells("RecNumberS").Value = dr("InvNo")
                .Cells("RecNarrationS").Value = dr("CusName")
                .Cells("ProDtlS").Value = dr("ProDtl")
                .Cells("PayableAmtS").Value = Purchase
                .Cells("PaidAmtS").Value = Sales
                .Cells("BalanceAmtS").Value = Balance
                .Cells("TickS").Value = "NO"
            End With
            RowNo += 1
        Next
        txtTotalPurchase.Text = PurchaseTotal.ToString("0.00")
        txtTotalSales.Text = SalesTotal.ToString("0.00")
        txtBalanceAmt.Text = Balance.ToString("0.00")

    End Sub

    Private Sub txtProName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProName.KeyDown
        If e.KeyCode = Keys.Down Then
            DgvProList.Focus()
            DgvProList.Show()
        ElseIf e.KeyCode = Keys.Enter Then
            Me.ProDetails()
        End If
    End Sub

    Private Sub txtProName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProName.TextChanged
        txtStockID.Text = String.Empty
        Dim s As String
        Dim ds As DataSet
        If txtProName.Text = Nothing OrElse _
            String.IsNullOrEmpty(txtProName.Text) Then
            DgvProList.Visible = False
        Else
            ds = New DataSet
            If VistawinFunctions.ProjectTypechk = "INVEN" Then
                If CbAny.Checked = True Then
                    Adapter = New SqlDataAdapter("SELECT DISTINCT s.St_ProName_vc AS 'Product Name',s.St_StockID_n AS 'Stock Code' FROM tbl_StockDtl s WHERE ((s.St_StockID_n like '" & txtProName.Text & "' + '%') OR (s.St_ProName_vc LIKE N'" & "%" + txtProName.Text & "' + '%')) ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
                ElseIf CbAny.Checked = False Then
                    Adapter = New SqlDataAdapter("SELECT DISTINCT s.St_ProName_vc AS 'Product Name',s.St_StockID_n AS 'Stock Code' FROM tbl_StockDtl s WHERE ((s.St_StockID_n like '" & txtProName.Text & "' + '%') OR (s.St_ProName_vc LIKE N'" & txtProName.Text & "' + '%')) ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
                End If

            ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then
                If CbAny.Checked = True Then
                    Adapter = New SqlDataAdapter("SELECT DISTINCT s.St_ProName_vc AS 'Product Name',s.St_StockID_n AS 'Stock Code' FROM tbl_Stock s LEFT JOIN tbl_MultiStock ms ON(s.St_StockID_n=ms.St_StockID_n) WHERE ((s.St_StockID_n LIKE '" & txtProName.Text & "' + '%') OR (s.St_ProName_vc LIKE N'" & "%" + txtProName.Text & "' + '%')) ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
                ElseIf CbAny.Checked = False Then
                    Adapter = New SqlDataAdapter("SELECT DISTINCT s.St_ProName_vc AS 'Product Name',s.St_StockID_n AS 'Stock Code' FROM tbl_Stock s LEFT JOIN tbl_MultiStock ms ON(s.St_StockID_n=ms.St_StockID_n) WHERE ((s.St_StockID_n LIKE '" & txtProName.Text & "' + '%') OR (s.St_ProName_vc LIKE N'" & txtProName.Text & "' + '%')) ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
                End If
            Else
            End If

            Adapter.Fill(ds, "ShipToDetail1")
            DgvProList.DataSource = ds.Tables("ShipToDetail1")
            If ds.Tables("ShipToDetail1").Rows.Count > 0 Then
                DgvProList.Show()

                DgvProList.Columns(0).Width = 300
                DgvProList.Columns(1).Width = 60

                Dim searchstring As String
                searchstring = Trim(txtProName.Text)
                Dim startchar As Char = searchstring.Chars(0)
                Dim Strlength As Integer
                Strlength = Trim(txtProName.Text).Length
                If Asc(startchar) >= 65 And Asc(startchar) <= 90 OrElse _
                           Asc(startchar) >= 97 And Asc(startchar) <= 122 Then
                    For Each dr As DataGridViewRow In DgvProList.Rows
                        If dr.Index <> DgvProList.NewRowIndex Then
                            s = dr.Cells(1).Value.ToString()
                            Dim ss As String = Mid(s, 1, Strlength)
                            If ss = UCase(searchstring) OrElse _
                                    ss = searchstring Then
                                DgvProList.MultiSelect = False
                                dr.Cells(1).Selected = True
                                DgvProList.CurrentCell = dr.Cells(1)
                                Exit For
                            End If
                        End If
                    Next
                Else
                    For Each dr As DataGridViewRow In DgvProList.Rows
                        If dr.Index <> DgvProList.NewRowIndex Then
                            s = dr.Cells(0).Value.ToString()
                            Dim ss As String = Mid(s, 1, Strlength)
                            If ss = searchstring Then
                                DgvProList.MultiSelect = False
                                dr.Cells(0).Selected = True
                                DgvProList.CurrentCell = dr.Cells(0)
                                Exit For
                            End If
                        End If
                    Next

                End If
            Else
                DgvProList.Hide()
                txtStockID.Text = String.Empty
                txtProName.Text = String.Empty
            End If
        End If
    End Sub

    Private Sub DgvProList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvProList.CellClick
        Me.ProDetails()
    End Sub


    Private Sub DgvProList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DgvProList.KeyDown
        If e.KeyCode = Keys.Down Then
            DgvProList.Focus()
            DgvProList.Show()
        ElseIf e.KeyCode = Keys.Enter Then
            Me.ProDetails()
        End If
    End Sub
    Private Sub ProDetails()
        Dim dr As DataRow
        Dim dr2 As DataGridViewSelectedRowCollection
        dr2 = DgvProList.SelectedRows
        For Each dr3 As DataGridViewRow In dr2
            dr = TryCast(dr3.DataBoundItem, DataRowView).Row

            RemoveHandler txtProName.TextChanged, AddressOf txtProName_TextChanged
            txtStockID.Text = dr.Item(1)
            txtProName.Text = dr.Item(0)
            AddHandler txtProName.TextChanged, AddressOf txtProName_TextChanged
            DgvProList.Hide()
            If txtProName.Text = String.Empty Then
                DgvProList.Hide()
            Else
                If txtStockID.Text <> String.Empty Then
                    DgvProList.Hide()

                    Ds = New DataSet
                    If VistawinFunctions.ProjectTypechk = "INVEN" Then

                        Adapter = New SqlDataAdapter("SELECT * FROM tbl_StockDtl WHERE St_StockID_n=" & txtStockID.Text.Trim & "", VistawinFunctions.ServerConnection)
                        Adapter.Fill(Ds, "ShipToDetail")
                        If Ds.Tables("ShipToDetail").Rows.Count > 0 Then

                            If IsDBNull(Ds.Tables("ShipToDetail").Rows(0).Item("St_StockID_n")) Then
                                txtStockID.Text = ""
                            Else
                                txtStockID.Text = Ds.Tables("ShipToDetail").Rows(0).Item("St_StockID_n")
                            End If

                            RemoveHandler txtProName.TextChanged, AddressOf txtProName_TextChanged
                            If IsDBNull(Ds.Tables("ShipToDetail").Rows(0).Item("St_ProName_vc")) Then
                                txtProName.Text = ""
                            Else
                                txtProName.Text = Ds.Tables("ShipToDetail").Rows(0).Item("St_ProName_vc")
                            End If
                            AddHandler txtProName.TextChanged, AddressOf txtProName_TextChanged
                        End If

                    ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then
                    Else

                    End If

                  

                End If
            End If
        Next

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnPrint2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPdfReport.Click
        Try
            'If DgvSales.Rows.Count = 0 Then
            '    MsgBox("No Data to Print")
            '    Exit Sub
            'End If

            Me.PdfHeaderDtl()
            If PdfCancel = True Then Exit Sub

            Me.PdfContentDtl()

            doc.Close()
            Process.Start(New ProcessStartInfo(pdfPath) With {.UseShellExecute = True})

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub PdfHeaderDtl()

        'pdfPath = System.IO.Path.Combine(VistawinFunctions.ScanningPath, "SalesProfit Report.pdf")

        'If pdfPath = "" Then
        '    MsgBox("Invalid Path")
        '    PdfCancel = True
        '    Exit Sub
        'End If

        'PdfCancel = False
        'pageSizeChk = PageSize.A4.Rotate()
        'doc = New Document(pageSizeChk, 20, 20, 20, 20)

        'writer = PdfWriter.GetInstance(doc, New FileStream(pdfPath, FileMode.CreateNew))
        'writer.PageEvent = New InvoicePageEvent()
        'doc.Open()

        pageSizeChk = PageSize.A4.Rotate()
        'pageSizeChk = PageSize.A4Dim pdfFile As String = "Report.pdf"
        'pageSizeChk = PageSize.A4

        Dim FileName As String = "StockLedgerReport" & " " & Now.ToString("dd-MM-yyyy hh.mm.ss tt") & ".pdf"
        pdfPath = System.IO.Path.Combine(VistawinFunctions.ScanningPath, FileName)
        doc = New Document(PageSize.A4, 20, 20, 20, 20)

        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(pdfPath, FileMode.CreateNew))
        writer.PageEvent = New InvoicePageEvent()
        doc.Open()

        '============= COMPANY NAME =============
        Dim HeaderTable As New PdfPTable(1)
        HeaderTable.WidthPercentage = 100

        Dim Cmpname1 As String = ""
        Cmd = New SqlCommand("SELECT Cmp_Name_vc FROM tbl_CompanyProfile", VistawinFunctions.ServerConnection)
        If Cmd.ExecuteScalar IsNot Nothing Then
            Cmpname1 = Cmd.ExecuteScalar.ToString()
        End If

        Dim CompCell As New PdfPCell(New Paragraph(Cmpname1, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)))
        CompCell.Border = Rectangle.NO_BORDER
        CompCell.HorizontalAlignment = Element.ALIGN_CENTER
        HeaderTable.AddCell(CompCell)
        doc.Add(HeaderTable)

        '============= REPORT TITLE + DATE =============
        Dim TitleTable As New PdfPTable(2)
        TitleTable.WidthPercentage = 100
        TitleTable.SetWidths({70, 30})

        Dim TitleCell As New PdfPCell(New Phrase("STOCK LEDGER REPORT  :  " &
            Format(CDate(txtFromDate2.Text), "dd/MM/yyyy") & "  To  " &
            Format(CDate(txtToDate2.Text), "dd/MM/yyyy"),
            FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLUE)))
        TitleCell.Border = Rectangle.NO_BORDER
        TitleCell.HorizontalAlignment = Element.ALIGN_LEFT
        TitleCell.Padding = 0

        Dim DateCell As New PdfPCell(New Phrase("Report Date : " & Now.ToString("dd/MM/yyyy hh:mm:ss tt"), NormalFont))
        DateCell.Border = Rectangle.NO_BORDER
        DateCell.HorizontalAlignment = Element.ALIGN_RIGHT
        DateCell.Padding = 0

        TitleTable.AddCell(TitleCell)
        TitleTable.AddCell(DateCell)
        doc.Add(TitleTable)

        Dim line As New iTextSharp.text.pdf.draw.LineSeparator()
        line.LineWidth = 1.5F
        line.LineColor = BaseColor.BLACK
        line.Percentage = 100
        doc.Add(New Chunk(line))
    End Sub
    Private Sub PdfContentDtl()
        Dim table As New PdfPTable(9)
        table.WidthPercentage = 100
        table.SetWidths({5, 10, 10, 10, 15, 10, 9, 9, 9})
        table.HeaderRows = 1

        Dim Headers() As String = {"SNo", "Bill Date", "Type", "Invoice No",
                                    "Particulars", "Product Dtl", "IN Qty", "Out Qty",
                                    "Balance"}

        For Each H As String In Headers
            Dim C As New PdfPCell(New Phrase(H, TblHeaderFont))
            C.BackgroundColor = TblHeaderColor
            C.HorizontalAlignment = Element.ALIGN_CENTER
            C.Padding = 4
            table.AddCell(C)
        Next
        Dim Qty1 As String = ""
        Dim RowNo As Integer = 1
        Dim ProTotal1 As Decimal = 0
        Dim PurTotal1 As Decimal = 0
        Dim ProfitAmt1 As Decimal = 0

        For Each dr As DataGridViewRow In DGVDayDetails.Rows
            If dr.IsNewRow Then Continue For

            If dr.Cells("SNoS").Value Is Nothing OrElse dr.Cells("SNoS").Value.ToString() = "" Then Continue For

            'Dim RecDateS1 As String = dr.Cells("RecDateS").Value.ToString()
            'Dim RecTypeS1 As String = dr.Cells("RecTypeS").Value.ToString()
            'Dim RecNumberS1 As String = dr.Cells("RecNumberS").Value.ToString()
            'Dim RecNarrationS1 As String = dr.Cells("RecNarrationS").Value.ToString()
            'Dim ProDtlS1 As String = dr.Cells("ProDtlS").Value.ToString()
            Dim RecDateS1 As String = Convert.ToString(dr.Cells("RecDateS").Value)
            Dim RecTypeS1 As String = Convert.ToString(dr.Cells("RecTypeS").Value)
            Dim RecNumberS1 As String = Convert.ToString(dr.Cells("RecNumberS").Value)
            Dim RecNarrationS1 As String = Convert.ToString(dr.Cells("RecNarrationS").Value)
            Dim ProDtlS1 As String = Convert.ToString(dr.Cells("ProDtlS").Value)
            Dim PayableAmtS1 As Decimal = dr.Cells("PayableAmtS").Value
            Dim PaidAmtS1 As Decimal = dr.Cells("PaidAmtS").Value
            Dim BalanceAmtS1 As Decimal = dr.Cells("BalanceAmtS").Value

            ProTotal1 += PayableAmtS1
            PurTotal1 += PaidAmtS1
            ProfitAmt1 += BalanceAmtS1
            table.AddCell(New PdfPCell(New Phrase(RowNo.ToString(), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
            table.AddCell(New PdfPCell(New Phrase(RecDateS1, TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
            table.AddCell(New PdfPCell(New Phrase(RecTypeS1, TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_LEFT})
            table.AddCell(New PdfPCell(New Phrase(RecNumberS1, TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_LEFT})
            table.AddCell(New PdfPCell(New Phrase(RecNarrationS1, TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
            table.AddCell(New PdfPCell(New Phrase(ProDtlS1, TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER})
            table.AddCell(New PdfPCell(New Phrase(FormatNumber(BalanceAmtS1, 2), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})
            table.AddCell(New PdfPCell(New Phrase(FormatNumber(PaidAmtS1, 2), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})
            table.AddCell(New PdfPCell(New Phrase(FormatNumber(BalanceAmtS1, 2), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})

            RowNo += 1
        Next

        'Dim TotalLabelCell As New PdfPCell(New Phrase("TOTAL", TblGrandTotalFont))
        'TotalLabelCell.Colspan = 9
        'TotalLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT
        'TotalLabelCell.BackgroundColor = BaseColor.LIGHT_GRAY
        'TotalLabelCell.Padding = 4
        'table.AddCell(TotalLabelCell)

        table.AddCell("")
        table.AddCell("")
        table.AddCell("")
        table.AddCell("")
        table.AddCell("")
        table.AddCell("")

        Dim GrandCell1 As New PdfPCell(New Phrase(FormatNumber(ProTotal1, 2), TblGrandTotalFont))
        GrandCell1.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell1.BackgroundColor = BaseColor.LIGHT_GRAY
        GrandCell1.Padding = 4
        table.AddCell(GrandCell1)

        Dim GrandCell2 As New PdfPCell(New Phrase(FormatNumber(PurTotal1, 2), TblGrandTotalFont))
        GrandCell2.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell2.BackgroundColor = BaseColor.LIGHT_GRAY
        GrandCell2.Padding = 4
        table.AddCell(GrandCell2)

        table.AddCell("")

        doc.Add(table)
    End Sub
End Class