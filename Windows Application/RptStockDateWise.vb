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
Public Class RptStockDateWise
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat, AddOrEdit As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet

    Private doc As Document
    Private writer As PdfWriter
    Dim pdfPath As String = ""
    Dim pageSizeChk As Rectangle = PageSize.A4
    Dim PdfCancel As Boolean = False

    'Dim TamilFontPath As String = "C:\Windows\Fonts\Latha.ttf"
    'Dim bfTamil As BaseFont = BaseFont.CreateFont(TamilFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
    'Dim TblcellFont As New iTextSharp.text.Font(bfTamil, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)

    Dim TblcellFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 9)
    Dim GstcellFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 9)

    Dim NormalFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 9)
    Dim TblHeaderFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK)
    Dim TblHeaderColor As BaseColor = BaseColor.LIGHT_GRAY
    Dim TblGrandTotalFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK)

    Public Class InvoicePageEvent
        Inherits PdfPageEventHelper

        Private bf As BaseFont
        Private totalPages As PdfTemplate
        Public EveryPageFooterText As String = ""
        Private FooterFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)

        Public Overrides Sub OnOpenDocument(ByVal writer As PdfWriter, ByVal document As Document)
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            totalPages = writer.DirectContent.CreateTemplate(50, 50)
        End Sub

        Public Overrides Sub OnEndPage(ByVal writer As PdfWriter, ByVal document As Document)

            Dim cb As PdfContentByte = writer.DirectContent
            Dim pageN As Integer = writer.PageNumber
            Dim text As String = "Page " & pageN & " of "
            Dim len As Single = bf.GetWidthPoint(text, 9)
            Dim x As Single = document.PageSize.Width - 100
            Dim y As Single = 15

            cb.BeginText()
            cb.SetFontAndSize(bf, 9)
            cb.SetTextMatrix(x, y)
            cb.ShowText(text)
            cb.EndText()
            cb.AddTemplate(totalPages, x + len, y)

            '================ Footer Text =================
            ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, New Phrase(EveryPageFooterText, FooterFont), (document.Left + document.Right) / 2, 10, 0)
        End Sub

        Public Overrides Sub OnCloseDocument(ByVal writer As PdfWriter, ByVal document As Document)
            totalPages.BeginText()
            totalPages.SetFontAndSize(bf, 9)
            totalPages.SetTextMatrix(0, 0)
            totalPages.ShowText(writer.PageNumber.ToString())
            totalPages.EndText()
        End Sub

    End Class
    Private Sub RptStockDateWise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        MonthCalendar3.Hide()
        txtFromDate.Text = Format(CDate(VistawinFunctions.TodayDate), "dd/MM/yyyy")
        txtToDate.Text = Format(CDate(VistawinFunctions.TodayDate), "dd/MM/yyyy")

        CbPapersize.SelectedIndex = 0
    End Sub
    Private Sub MonthCalendar3_DateSelected(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar3.DateSelected
        If FromBool = True Then
            If Format(CDate(txtToDate.Text), "yyyy/MM/dd") < Format(CDate(MonthCalendar3.SelectionRange.Start), "yyyy/MM/dd") Then
                MsgBox("Please,Check the date")
                Exit Sub
            End If
            txtFromDate.Text = Format(CDate(MonthCalendar3.SelectionRange.Start), "dd/MM/yyyy")
            MonthCalendar3.Hide()
        ElseIf ToBool = True Then
            If Format(CDate(MonthCalendar3.SelectionRange.Start), "yyyy/MM/dd") < Format(CDate(txtFromDate.Text), "yyyy/MM/dd") Then
                MsgBox("Please,Check the date")
                Exit Sub
            End If
            txtToDate.Text = Format(CDate(MonthCalendar3.SelectionRange.Start), "dd/MM/yyyy")
            MonthCalendar3.Hide()
        End If
    End Sub
    Private Sub btnFrom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrom.Click
        FromBool = True
        ToBool = False
        MonthCalendar3.Show()
        MonthCalendar3.BringToFront()
        MonthCalendar3.Location = New System.Drawing.Point(txtFromDate.Location.X, txtFromDate.Location.Y + 30)
    End Sub
    Private Sub btnTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTo.Click
        ToBool = True
        FromBool = False
        MonthCalendar3.Show()
        MonthCalendar3.BringToFront()
        MonthCalendar3.Location = New System.Drawing.Point(txtToDate.Location.X, txtToDate.Location.Y + 30)
    End Sub
    Private Sub BtnPreview3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview3.Click

        DgvStock.Show()
        DgvStock.BringToFront()
        MonthCalendar3.BringToFront()

        If VistawinFunctions.ProjectTypechk = "INVEN" Then
            Me.InvenStockRpt()
        ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then
            Me.ClinicStockRpt()
        Else
        End If

    End Sub
 
    Private Sub InvenStockRpt()

        DgvStock.Rows.Clear()
        RowIndex = 0

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT s.St_StockID_n AS 'StockCode',s.St_ProName_vc AS 'ProName',(ISNULL(St_TotalSt_i,0)+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_SalParticular WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_DamageParticular WHERE Sa_SaveTy_vc='DAMAGE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "'))-((SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_SalParticular WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')) AS 'OPStock',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'PurQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_SalParticular WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SaleQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'PRTQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_SalParticular WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SRTQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SamQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_DamageParticular WHERE Sa_SaveTy_vc='DAMAGE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'WasQty',ISNULL(St_TotalSt_i,0)AS 'CurrStock' FROM tbl_StockDtl s LEFT JOIN tbl_MultiStock m ON(s.St_StockID_n=m.St_StockID_n) GROUP BY s.St_StockID_n,s.St_ProName_vc,St_TotalSt_i ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        If Ds.Tables("FillData").Rows.Count > 0 Then
            For Each Dr As DataRow In Ds.Tables("FillData").Rows
                DgvStock.Rows.Add()
                DgvStock.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
                DgvStock.Rows(RowIndex).Cells("StockCode").Value = Dr.Item("StockCode")
                DgvStock.Rows(RowIndex).Cells("ProName").Value = Dr.Item("ProName")
                DgvStock.Rows(RowIndex).Cells("OPStock").Value = Dr.Item("OPStock")

                DgvStock.Rows(RowIndex).Cells("PurQty").Value = Dr.Item("PurQty")
                DgvStock.Rows(RowIndex).Cells("SaleQty").Value = Dr.Item("SaleQty")
                DgvStock.Rows(RowIndex).Cells("PRTQty").Value = Dr.Item("PRTQty")
                DgvStock.Rows(RowIndex).Cells("SRTQty").Value = Dr.Item("SRTQty")
                DgvStock.Rows(RowIndex).Cells("SamQty").Value = Dr.Item("SamQty")
                DgvStock.Rows(RowIndex).Cells("WasQty").Value = Dr.Item("WasQty")

                DgvStock.Rows(RowIndex).Cells("CurrStock").Value = Dr.Item("CurrStock")
                RowIndex += 1
            Next
        End If

    End Sub
    
    Private Sub txtProductName1A_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProductName1A.TextChanged

        If VistawinFunctions.ProjectTypechk = "INVEN" Then
            InvenSearch()
        ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then
            ClinicSearch()
        Else
        End If

    End Sub
    Private Sub InvenSearch()
        Dim s As String
        Dim ds As DataSet

        DgvStock.Show()
        If txtProductName1A.Text = Nothing OrElse _
            String.IsNullOrEmpty(txtProductName1A.Text) Then
            DgvStock.Visible = True
            DgvStock.Rows.Clear()
            Me.InvenStockRpt()
        Else
            ds = New DataSet
            Adapter = New SqlDataAdapter("SELECT s.St_StockID_n AS 'StockCode',s.St_ProName_vc AS 'ProName',(ISNULL(St_TotalSt_i,0)+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_SalParticular WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_DamageParticular WHERE Sa_SaveTy_vc='DAMAGE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "'))-((SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_SalParticular WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')) AS 'OPStock',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'PurQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_SalParticular WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SaleQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'PRTQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_SalParticular WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SRTQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SamQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_DamageParticular WHERE Sa_SaveTy_vc='DAMAGE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'WasQty',ISNULL(St_TotalSt_i,0)AS 'CurrStock' FROM tbl_StockDtl s LEFT JOIN tbl_MultiStock m ON(s.St_StockID_n=m.St_StockID_n) WHERE (s.St_ProName_vc LIKE N'" & txtProductName1A.Text + "%" & "' OR s.St_ProName_vc LIKE '" & txtProductName1A.Text + "%" & "') GROUP BY s.St_StockID_n,s.St_ProName_vc,St_TotalSt_i ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
            Adapter.Fill(ds, "FillDataa")

            If ds.Tables("FillDataa").Rows.Count > 0 Then
                DgvStock.Rows.Clear()
                Dim StockValue As Decimal = 0
                RowIndex = 0
                For Each dr As DataRow In ds.Tables("FillDataa").Rows
                    DgvStock.Rows.Add()
                    DgvStock.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
                    DgvStock.Rows(RowIndex).Cells("StockCode").Value = dr.Item("StockCode")
                    DgvStock.Rows(RowIndex).Cells("ProName").Value = dr.Item("ProName")
                    DgvStock.Rows(RowIndex).Cells("OPStock").Value = dr.Item("OPStock")

                    DgvStock.Rows(RowIndex).Cells("PurQty").Value = dr.Item("PurQty")
                    DgvStock.Rows(RowIndex).Cells("SaleQty").Value = dr.Item("SaleQty")
                    DgvStock.Rows(RowIndex).Cells("PRTQty").Value = dr.Item("PRTQty")
                    DgvStock.Rows(RowIndex).Cells("SRTQty").Value = dr.Item("SRTQty")
                    DgvStock.Rows(RowIndex).Cells("SamQty").Value = dr.Item("SamQty")
                    DgvStock.Rows(RowIndex).Cells("WasQty").Value = dr.Item("WasQty")

                    DgvStock.Rows(RowIndex).Cells("CurrStock").Value = dr.Item("CurrStock")
                    RowIndex += 1
                Next
            Else
                DgvStock.Rows.Clear()
            End If

            Dim searchstring As String
            searchstring = Trim(txtProductName1A.Text)
            Dim startchar As Char = searchstring.Chars(0)
            Dim Strlength As Integer
            Strlength = Trim(txtProductName1A.Text).Length
            If Asc(startchar) >= 65 And Asc(startchar) <= 90 OrElse _
                       Asc(startchar) >= 97 And Asc(startchar) <= 122 Then
                For Each dr As DataGridViewRow In DgvStock.Rows
                    If dr.Index <> DgvStock.NewRowIndex Then
                        s = dr.Cells(2).Value.ToString()
                        Dim ss As String = Mid(s, 1, Strlength)
                        If ss = UCase(searchstring) OrElse _
                                ss = searchstring Then
                            DgvStock.MultiSelect = False
                            dr.Cells(2).Selected = True
                            DgvStock.CurrentCell = dr.Cells(2)
                            Exit For
                        End If
                    End If
                Next
            Else
                For Each dr As DataGridViewRow In DgvStock.Rows
                    If dr.Index <> DgvStock.NewRowIndex Then
                        s = dr.Cells(6).Value.ToString()
                        Dim ss As String = Mid(s, 1, Strlength)
                        If ss = searchstring Then
                            DgvStock.MultiSelect = False
                            dr.Cells(1).Selected = True
                            DgvStock.CurrentCell = dr.Cells(1)
                            Exit For
                        End If
                    End If
                Next

            End If

            '    End If
            'Next

        End If
    End Sub
    Private Sub InvenStockRptNew()

        DgvStock.Rows.Clear()
        RowIndex = 0
        Ds = New DataSet

        Dim Sql As String = _
        "SELECT " & _
        "S.St_StockID_n AS StockCode," & _
        "S.St_ProName_vc AS ProName," & _
        "(ISNULL(S.St_TotalSt_i,0)+ISNULL(Sales.InvoiceQty,0)+ISNULL(PReturn.PurRetQty,0)+ISNULL(Sample.SampleQty,0)-ISNULL(Purchase.PurQty,0)-ISNULL(SReturn.SalesRetQty,0)) AS OPStock," & _
        "ISNULL(Purchase.PurQty,0) AS PurQty," & _
        "ISNULL(Sales.InvoiceQty,0) AS SaleQty," & _
        "ISNULL(PReturn.PurRetQty,0) AS PRTQty," & _
        "ISNULL(SReturn.SalesRetQty,0) AS SRTQty," & _
        "ISNULL(Sample.SampleQty,0) AS SamQty," & _
        "ISNULL(Damage.DamageQty,0) AS WasQty," & _
        "ISNULL(S.St_TotalSt_i,0) AS CurrStock " & _
        "FROM tbl_StockDtl S " & _
                "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) PurQty FROM tbl_PurchParticular " & _
        "WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) Purchase " & _
        "ON Purchase.Sa_StCode_n=S.St_StockID_n " & _
                "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) InvoiceQty FROM tbl_SalParticular " & _
        "WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) Sales " & _
        "ON Sales.Sa_StCode_n=S.St_StockID_n " & _
                "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) PurRetQty FROM tbl_PurchParticular " & _
        "WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) PReturn " & _
        "ON PReturn.Sa_StCode_n=S.St_StockID_n " & _
                "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) SalesRetQty FROM tbl_SalParticular " & _
        "WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) SReturn " & _
        "ON SReturn.Sa_StCode_n=S.St_StockID_n " & _
                "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) SampleQty FROM tbl_WasteParticular " & _
        "WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) Sample " & _
        "ON Sample.Sa_StCode_n=S.St_StockID_n " & _
        "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) DamageQty FROM tbl_DamageParticular " & _
        "WHERE Sa_SaveTy_vc='DAMAGE' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) Damage " & _
        "ON Damage.Sa_StCode_n=S.St_StockID_n " & _
                "ORDER BY S.St_ProName_vc"

        Adapter = New SqlDataAdapter(Sql, VistawinFunctions.ServerConnection)
        Adapter.SelectCommand.Parameters.AddWithValue("@FDate", CDate(txtFromDate.Text))
        Adapter.SelectCommand.Parameters.AddWithValue("@TDate", CDate(txtToDate.Text))

        Adapter.Fill(Ds, "FillData")

        If Ds.Tables("FillData").Rows.Count > 0 Then
            For Each Dr As DataRow In Ds.Tables("FillData").Rows
                DgvStock.Rows.Add()

                With DgvStock.Rows(RowIndex)
                    .Cells("SNo").Value = RowIndex + 1
                    .Cells("StockCode").Value = Dr("StockCode")
                    .Cells("ProName").Value = Dr("ProName")
                    .Cells("OPStock").Value = Dr("OPStock")
                    .Cells("PurQty").Value = Dr("PurQty")
                    .Cells("SaleQty").Value = Dr("SaleQty")
                    .Cells("PRTQty").Value = Dr("PRTQty")
                    .Cells("SRTQty").Value = Dr("SRTQty")
                    .Cells("SamQty").Value = Dr("SamQty")
                    .Cells("WasQty").Value = Dr("WasQty")
                    .Cells("CurrStock").Value = Dr("CurrStock")
                End With
                RowIndex += 1
            Next
        End If

    End Sub
    ' Private Sub txtProductName1A_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProductName1A.TextChanged

    '     If txtProductName1A.Text.Trim = "" Then
    '         InvenStockRpt()
    '         Exit Sub
    '     End If

    '     Dim ds As New DataSet

    '     Dim Sql As String = _
    '"SELECT " & _
    '"S.St_StockID_n AS StockCode," & _
    '"S.St_ProName_vc AS ProName," & _
    '"(ISNULL(S.St_TotalSt_i,0)+ISNULL(Sales.InvoiceQty,0)+ISNULL(PReturn.PurRetQty,0)+ISNULL(Sample.SampleQty,0)-ISNULL(Purchase.PurQty,0)-ISNULL(SReturn.SalesRetQty,0)) AS OPStock," & _
    '"ISNULL(Purchase.PurQty,0) AS PurQty," & _
    '"ISNULL(Sales.InvoiceQty,0) AS SaleQty," & _
    '"ISNULL(PReturn.PurRetQty,0) AS PRTQty," & _
    '"ISNULL(SReturn.SalesRetQty,0) AS SRTQty," & _
    '"ISNULL(Sample.SampleQty,0) AS SamQty," & _
    '"ISNULL(Damage.DamageQty,0) AS WasQty," & _
    '"ISNULL(S.St_TotalSt_i,0) AS CurrStock " & _
    '"FROM tbl_StockDtl S " & _
    '        "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) PurQty FROM tbl_PurchParticular " & _
    '"WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) Purchase " & _
    '"ON Purchase.Sa_StCode_n=S.St_StockID_n " & _
    '        "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) InvoiceQty FROM tbl_SalParticular " & _
    '"WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) Sales " & _
    '"ON Sales.Sa_StCode_n=S.St_StockID_n " & _
    '        "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) PurRetQty FROM tbl_PurchParticular " & _
    '"WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) PReturn " & _
    '"ON PReturn.Sa_StCode_n=S.St_StockID_n " & _
    '        "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) SalesRetQty FROM tbl_SalParticular " & _
    '"WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) SReturn " & _
    '"ON SReturn.Sa_StCode_n=S.St_StockID_n " & _
    '        "LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) SampleQty FROM tbl_WasteParticular " & _
    '"WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) Sample " & _
    '"ON Sample.Sa_StCode_n=S.St_StockID_n " & _
    '"LEFT JOIN (SELECT Sa_StCode_n,SUM(Sa_TQty_i) DamageQty FROM tbl_DamageParticular " & _
    '"WHERE Sa_SaveTy_vc='DAMAGE' AND Sa_BillDate_d BETWEEN @FDate AND @TDate GROUP BY Sa_StCode_n) Damage " & _
    '"ON Damage.Sa_StCode_n=S.St_StockID_n " & _
    '     "WHERE S.St_ProName_vc LIKE '%' + @ProName + '%'" & _
    '        "ORDER BY S.St_ProName_vc"

    '     Adapter = New SqlDataAdapter(Sql, VistawinFunctions.ServerConnection)

    '     Adapter.SelectCommand.Parameters.AddWithValue("@FDate", CDate(txtFromDate.Text))
    '     Adapter.SelectCommand.Parameters.AddWithValue("@TDate", CDate(txtToDate.Text))
    '     Adapter.SelectCommand.Parameters.AddWithValue("@ProName", txtProductName1A.Text.Trim())

    '     Adapter.Fill(ds, "FillDataa")
    '     MessageBox.Show(ds.Tables("FillDataa").Rows.Count.ToString())

    '     'FillStockGrid(ds.Tables("FillDataa"))

    '     DgvStock.Rows.Clear()
    '     RowIndex = 0

    '     If ds.Tables("FillDataa").Rows.Count > 0 Then
    '         For Each Dr As DataRow In ds.Tables("FillDataa").Rows
    '             DgvStock.Rows.Add()

    '             With DgvStock.Rows(RowIndex)
    '                 .Cells("SNo").Value = RowIndex + 1
    '                 .Cells("StockCode").Value = Dr("StockCode")
    '                 .Cells("ProName").Value = Dr("ProName")
    '                 .Cells("OPStock").Value = Dr("OPStock")
    '                 .Cells("PurQty").Value = Dr("PurQty")
    '                 .Cells("SaleQty").Value = Dr("SaleQty")
    '                 .Cells("PRTQty").Value = Dr("PRTQty")
    '                 .Cells("SRTQty").Value = Dr("SRTQty")
    '                 .Cells("SamQty").Value = Dr("SamQty")
    '                 .Cells("WasQty").Value = Dr("WasQty")
    '                 .Cells("CurrStock").Value = Dr("CurrStock")
    '             End With
    '             RowIndex += 1
    '         Next
    '     End If

    ' End Sub

    ' Private Sub FillStockGrid(ByVal dt As DataTable)

    '     DgvStock.Rows.Clear()
    '     RowIndex = 0

    '     For Each Dr As DataRow In dt.Rows
    '         DgvStock.Rows.Add()
    '         With DgvStock.Rows(RowIndex)
    '             .Cells("SNo").Value = RowIndex + 1
    '             .Cells("StockCode").Value = Dr("StockCode")
    '             .Cells("ProName").Value = Dr("ProName")
    '             .Cells("OPStock").Value = Dr("OPStock")
    '             .Cells("PurQty").Value = Dr("PurQty")
    '             .Cells("SaleQty").Value = Dr("SaleQty")
    '             .Cells("PRTQty").Value = Dr("PRTQty")
    '             .Cells("SRTQty").Value = Dr("SRTQty")
    '             .Cells("SamQty").Value = Dr("SamQty")
    '             .Cells("WasQty").Value = Dr("WasQty")
    '             .Cells("CurrStock").Value = Dr("CurrStock")
    '         End With
    '         RowIndex += 1
    '     Next

    ' End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
    Private Sub BtnPdfReport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPdfReport.Click
        Try
            Me.HeaderDtl()
            If PdfCancel = True Then Exit Sub

            Me.ContentDtl()

            doc.Close()
            Process.Start(New ProcessStartInfo(pdfPath) With {.UseShellExecute = True})

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub HeaderDtl()

        pdfPath = System.IO.Path.Combine(VistawinFunctions.ScanningPath, "Closing StockReport.pdf")

        If pdfPath = "" Then
            MsgBox("Invalid Path")
            Exit Sub
        End If

        Select Case CbPaperSize.Text
            Case "A5P"
                pageSizeChk = PageSize.A5
            Case "A5L"
                pageSizeChk = PageSize.A5.Rotate()
            Case "A4P"
                pageSizeChk = PageSize.A4
            Case "A4L"
                pageSizeChk = PageSize.A4.Rotate()
            Case "2Inch"
                pageSizeChk = New Rectangle(144, 1000)
            Case "3Inch"
                pageSizeChk = New Rectangle(216, 1200)
            Case Else
                pageSizeChk = PageSize.A4
        End Select

        If CbPaperSize.Text = "2Inch" OrElse CbPaperSize.Text = "3Inch" Then
            doc = New Document(pageSizeChk, 5, 5, 5, 5)
        Else
            doc = New Document(pageSizeChk, 20, 20, 20, 20)
        End If

        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(pdfPath, FileMode.Create))
        writer.PageEvent = New InvoicePageEvent()
        doc.Open()

        '================ BORDER =================
        If CbPaperSize.Text <> "2Inch" AndAlso CbPaperSize.Text <> "3Inch" Then
            Dim cb As PdfContentByte = writer.DirectContent
            cb.SetLineWidth(1.5)
            cb.Rectangle(10, 10, doc.PageSize.Width - 20, doc.PageSize.Height - 20)
            cb.Stroke()
        End If
        '=============HEADING=====================

        Dim HeaderTable As New PdfPTable(1)
        HeaderTable.WidthPercentage = 100

        Dim Cmpname1 As String = ""
        Cmd = New SqlCommand("SELECT Cmp_Name_vc FROM tbl_CompanyProfile", VistawinFunctions.ServerConnection)
        If Cmd.ExecuteScalar IsNot Nothing Then
            Cmpname1 = Cmd.ExecuteScalar.ToString()
        End If
        Dim CompPara As New Paragraph(Cmpname1, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14))

        Dim TextCell As New PdfPCell(CompPara)
        TextCell.Border = Rectangle.NO_BORDER
        TextCell.HorizontalAlignment = Element.ALIGN_CENTER
        HeaderTable.AddCell(TextCell)
        doc.Add(HeaderTable)

        '===============REPORT TITLE==================

        Dim TitleTable As New PdfPTable(2)
        TitleTable.WidthPercentage = 100
        TitleTable.SetWidths({70, 30})

        Dim ReportTitle1 As String = "STOCK REPORT : " & Format(CDate(txtFromDate.Text), "dd/MM/yyyy") & " TO " & Format(CDate(txtToDate.Text), "dd/MM/yyyy")
        Dim TitleCell As New PdfPCell(New Phrase(ReportTitle1, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLUE)))
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

        '=================LINE ADD=================
        Dim line As New iTextSharp.text.pdf.draw.LineSeparator()
        line.LineWidth = 1.5F
        line.LineColor = BaseColor.BLACK
        line.Percentage = 100
        doc.Add(New Chunk(line))
    End Sub
    Private Sub ContentDtl()

        Dim table As New PdfPTable(9)
        table.WidthPercentage = 100
        table.SetWidths({5, 20, 10, 10, 10, 10, 10, 10, 10})
        table.DefaultCell.BorderColor = BaseColor.GRAY

        table.HeaderRows = 1

        Dim headers() As String = {"S.No", "Product", "OP Stock", "Pur Qty", "PRT Qty", "Sale Qty", "SRT Qty", "Damage Qty", "Curr Stock"}
        For Each h As String In headers
            Dim cell As New PdfPCell(New Phrase(h, TblHeaderFont))
            cell.BackgroundColor = TblHeaderColor
            cell.HorizontalAlignment = Element.ALIGN_CENTER
            cell.Padding = 6
            table.AddCell(cell)
        Next

        Dim RowNo As Integer = 1
        Dim CurrentSaveType As String = ""

        Dim OPStockTotal As Decimal = 0
        Dim PRTQtyTotal As Decimal = 0
        Dim PurQtyTotal As Decimal = 0
        Dim SaleQtyTotal As Decimal = 0
        Dim SRTQtyTotal As Decimal = 0
        Dim WasQtyTotal As Decimal = 0
        Dim CurrStockTotal As Decimal = 0

        For Each row As DataGridViewRow In DgvStock.Rows
            If row.IsNewRow Then Continue For

            Dim ProName1 As String = row.Cells("ProName").Value.ToString()
            Dim OPStock1 As Decimal = Convert.ToDecimal(row.Cells("OPStock").Value)
            Dim PurQty1 As Decimal = Convert.ToDecimal(row.Cells("PurQty").Value)
            Dim PRTQty1 As Decimal = Convert.ToDecimal(row.Cells("PRTQty").Value)
            Dim SaleQty1 As Decimal = Convert.ToDecimal(row.Cells("SaleQty").Value)
            Dim SRTQty1 As Decimal = Convert.ToDecimal(row.Cells("SRTQty").Value)
            Dim WasQty1 As Decimal = Convert.ToDecimal(row.Cells("WasQty").Value)
            Dim CurrStock1 As Decimal = Convert.ToDecimal(row.Cells("CurrStock").Value)

            OPStockTotal += OPStock1
            PurQtyTotal += PurQty1
            PRTQtyTotal += PRTQty1
            SaleQtyTotal += SaleQty1
            SRTQtyTotal += SRTQty1
            WasQtyTotal += WasQty1
            CurrStockTotal += CurrStock1

            table.AddCell(New PdfPCell(New Phrase(RowNo.ToString(), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_LEFT})

            table.AddCell(New PdfPCell(New Phrase(ProName1, TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_LEFT})

            table.AddCell(New PdfPCell(New Phrase(FormatQty(OPStock1), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})

            table.AddCell(New PdfPCell(New Phrase(FormatQty(PurQty1), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})

            table.AddCell(New PdfPCell(New Phrase(FormatQty(PRTQty1), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})

            table.AddCell(New PdfPCell(New Phrase(FormatQty(SaleQty1), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})

            table.AddCell(New PdfPCell(New Phrase(FormatQty(SRTQty1), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})

            table.AddCell(New PdfPCell(New Phrase(FormatQty(WasQty1), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})

            table.AddCell(New PdfPCell(New Phrase(FormatQty(CurrStock1), TblcellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT})

            RowNo += 1
        Next

        table.AddCell("")
        table.AddCell("")

        Dim GrandCell As New PdfPCell(New Phrase(FormatNumber(OPStockTotal, 2), TblGrandTotalFont))
        GrandCell.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell.BackgroundColor = BaseColor.LIGHT_GRAY
        table.AddCell(GrandCell)

        Dim GrandCell2 As New PdfPCell(New Phrase(FormatNumber(PurQtyTotal, 2), TblGrandTotalFont))
        GrandCell2.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell2.BackgroundColor = BaseColor.LIGHT_GRAY
        table.AddCell(GrandCell2)

        Dim GrandCell3 As New PdfPCell(New Phrase(FormatNumber(PRTQtyTotal, 2), TblGrandTotalFont))
        GrandCell3.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell3.BackgroundColor = BaseColor.LIGHT_GRAY
        table.AddCell(GrandCell3)

        Dim GrandCell4 As New PdfPCell(New Phrase(FormatNumber(SaleQtyTotal, 2), TblGrandTotalFont))
        GrandCell4.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell4.BackgroundColor = BaseColor.LIGHT_GRAY
        table.AddCell(GrandCell4)

        Dim GrandCell5 As New PdfPCell(New Phrase(FormatNumber(SRTQtyTotal, 2), TblGrandTotalFont))
        GrandCell5.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell5.BackgroundColor = BaseColor.LIGHT_GRAY
        table.AddCell(GrandCell5)

        Dim GrandCell6 As New PdfPCell(New Phrase(FormatNumber(WasQtyTotal, 2), TblGrandTotalFont))
        GrandCell6.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell6.BackgroundColor = BaseColor.LIGHT_GRAY
        table.AddCell(GrandCell6)

        Dim GrandCell7 As New PdfPCell(New Phrase(FormatNumber(CurrStockTotal, 2), TblGrandTotalFont))
        GrandCell7.HorizontalAlignment = Element.ALIGN_RIGHT
        GrandCell7.BackgroundColor = BaseColor.LIGHT_GRAY
        table.AddCell(GrandCell7)

        doc.Add(table)

    End Sub
    Private Function FormatQty(ByVal Qty As Decimal) As String
        If Qty = Math.Floor(Qty) Then
            Return Qty.ToString("N0")
        Else
            Return Qty.ToString("N3")
        End If
    End Function
    Private Sub ClinicStockRpt()
        DgvStock.Rows.Clear()
        RowIndex = 0

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT s.St_StockID_n AS 'StockCode',s.St_ProName_vc AS 'ProName',(ISNULL(St_TotalSt_i,0)+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_BillParticular WHERE Sa_SaveTy_vc='INVOICE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='WASTAGE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "'))-((SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_BillParticular WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')) AS 'OPStock',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'PurQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_BillParticular WHERE Sa_SaveTy_vc='INVOICE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SaleQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'PRTQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_BillParticular WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SRTQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SamQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='WASTAGE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'WasQty',ISNULL(St_TotalSt_i,0)AS 'CurrStock' FROM tbl_Stock s LEFT JOIN tbl_MultiStock m ON(s.St_StockID_n=m.St_StockID_n) GROUP BY s.St_StockID_n,s.St_ProName_vc,St_TotalSt_i ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        If Ds.Tables("FillData").Rows.Count > 0 Then
            For Each Dr As DataRow In Ds.Tables("FillData").Rows
                DgvStock.Rows.Add()
                DgvStock.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
                DgvStock.Rows(RowIndex).Cells("StockCode").Value = Dr.Item("StockCode")
                DgvStock.Rows(RowIndex).Cells("ProName").Value = Dr.Item("ProName")
                DgvStock.Rows(RowIndex).Cells("OPStock").Value = Dr.Item("OPStock")

                DgvStock.Rows(RowIndex).Cells("PurQty").Value = Dr.Item("PurQty")
                DgvStock.Rows(RowIndex).Cells("SaleQty").Value = Dr.Item("SaleQty")
                DgvStock.Rows(RowIndex).Cells("PRTQty").Value = Dr.Item("PRTQty")
                DgvStock.Rows(RowIndex).Cells("SRTQty").Value = Dr.Item("SRTQty")
                DgvStock.Rows(RowIndex).Cells("SamQty").Value = Dr.Item("SamQty")
                DgvStock.Rows(RowIndex).Cells("WasQty").Value = Dr.Item("WasQty")

                DgvStock.Rows(RowIndex).Cells("CurrStock").Value = Dr.Item("CurrStock")

                DgvStock.Rows(RowIndex).Cells("StockCode").Value = Dr.Item("StockCode")
                DgvStock.Rows(RowIndex).Cells("ProName").Value = Dr.Item("ProName")
                DgvStock.Rows(RowIndex).Cells("OPStock").Value = Dr.Item("OPStock")

                RowIndex += 1
            Next
        End If
    End Sub
    Private Sub ClinicSearch()
        Dim s As String
        Dim ds As DataSet

        DgvStock.Show()
        If txtProductName1A.Text = Nothing OrElse _
            String.IsNullOrEmpty(txtProductName1A.Text) Then
            DgvStock.Visible = True
            DgvStock.Rows.Clear()
            Me.ClinicStockRpt()
        Else
            ds = New DataSet
            Adapter = New SqlDataAdapter("SELECT s.St_StockID_n AS 'StockCode',s.St_ProName_vc AS 'ProName',(ISNULL(St_TotalSt_i,0)+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_BillParticular WHERE Sa_SaveTy_vc='INVOICE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='WASTAGE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "'))-((SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')+(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_BillParticular WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d>='" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "')) AS 'OPStock',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'PurQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_BillParticular WHERE Sa_SaveTy_vc='INVOICE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SaleQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_PurchParticular WHERE Sa_SaveTy_vc='PURCHASE RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'PRTQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_BillParticular WHERE Sa_SaveTy_vc='SALES RETURN' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SRTQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='SAMPLE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'SamQty',(SELECT ISNULL(SUM(Sa_TQty_i),0)FROM tbl_WasteParticular WHERE Sa_SaveTy_vc='WASTAGE' AND Sa_StCode_n=s.St_StockID_n AND Sa_BillDate_d BETWEEN '" & Format(CDate(txtFromDate.Text.Trim), "yyyy/MM/dd") & "' AND '" & Format(CDate(txtToDate.Text.Trim), "yyyy/MM/dd") & "')AS 'WasQty',ISNULL(St_TotalSt_i,0)AS 'CurrStock' FROM tbl_Stock s LEFT JOIN tbl_MultiStock m ON(s.St_StockID_n=m.St_StockID_n) WHERE (s.St_ProName_vc LIKE N'" & txtProductName1A.Text + "%" & "') GROUP BY s.St_StockID_n,s.St_ProName_vc,St_TotalSt_i ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
            Adapter.Fill(ds, "FillDataa")

            If ds.Tables("FillDataa").Rows.Count > 0 Then
                DgvStock.Rows.Clear()
                Dim StockValue As Decimal = 0
                RowIndex = 0
                For Each dr As DataRow In ds.Tables("FillDataa").Rows
                    DgvStock.Rows.Add()
                    DgvStock.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
                    DgvStock.Rows(RowIndex).Cells("StockCode").Value = dr.Item("StockCode")
                    DgvStock.Rows(RowIndex).Cells("ProName").Value = dr.Item("ProName")
                    DgvStock.Rows(RowIndex).Cells("OPStock").Value = dr.Item("OPStock")

                    DgvStock.Rows(RowIndex).Cells("PurQty").Value = dr.Item("PurQty")
                    DgvStock.Rows(RowIndex).Cells("SaleQty").Value = dr.Item("SaleQty")
                    DgvStock.Rows(RowIndex).Cells("PRTQty").Value = dr.Item("PRTQty")
                    DgvStock.Rows(RowIndex).Cells("SRTQty").Value = dr.Item("SRTQty")
                    DgvStock.Rows(RowIndex).Cells("SamQty").Value = dr.Item("SamQty")
                    DgvStock.Rows(RowIndex).Cells("WasQty").Value = dr.Item("WasQty")

                    DgvStock.Rows(RowIndex).Cells("CurrStock").Value = dr.Item("CurrStock")
                    RowIndex += 1
                Next
            Else
                DgvStock.Rows.Clear()
            End If

            Dim searchstring As String
            searchstring = Trim(txtProductName1A.Text)
            Dim startchar As Char = searchstring.Chars(0)
            Dim Strlength As Integer
            Strlength = Trim(txtProductName1A.Text).Length
            If Asc(startchar) >= 65 And Asc(startchar) <= 90 OrElse _
                       Asc(startchar) >= 97 And Asc(startchar) <= 122 Then
                For Each dr As DataGridViewRow In DgvStock.Rows
                    If dr.Index <> DgvStock.NewRowIndex Then
                        s = dr.Cells(2).Value.ToString()
                        Dim ss As String = Mid(s, 1, Strlength)
                        If ss = UCase(searchstring) OrElse _
                                ss = searchstring Then
                            DgvStock.MultiSelect = False
                            dr.Cells(2).Selected = True
                            DgvStock.CurrentCell = dr.Cells(2)
                            Exit For
                        End If
                    End If
                Next
            Else
                For Each dr As DataGridViewRow In DgvStock.Rows
                    If dr.Index <> DgvStock.NewRowIndex Then
                        s = dr.Cells(6).Value.ToString()
                        Dim ss As String = Mid(s, 1, Strlength)
                        If ss = searchstring Then
                            DgvStock.MultiSelect = False
                            dr.Cells(1).Selected = True
                            DgvStock.CurrentCell = dr.Cells(1)
                            Exit For
                        End If
                    End If
                Next

            End If

            '    End If
            'Next

        End If
    End Sub
End Class