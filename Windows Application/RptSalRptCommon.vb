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
Public Class RptSalRptCommon
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet

    Dim RptCategoryChk As String = ""
    Dim RptTypeChk As String = ""
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                Me.Close()
            Else
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub RptSalRptCommon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        MonthCalendar1.Hide()
        txtFromDate.Text = "01/" + VistawinFunctions.TodayDate.Month.ToString + "/" + VistawinFunctions.TodayDate.Year.ToString
        txtToDate.Text = Format(CDate(VistawinFunctions.TodayDate), "dd/MM/yyyy")

        'CbRptType.SelectedIndex = 0

        txtCustName.Hide()
        txtCusID.Hide()
        LblHeader.Hide()

        DGVCustomer.Hide()
        DGVCustomer.Size = New System.Drawing.Size(900, 250)
        DGVCustomer.BringToFront()

        BtnShowHide.Text = If(GrpLeft.Visible, "<< Hide", ">> Show")

        Me.RptMasterList()
    End Sub
    Private Sub RptMasterList()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("Select RM_Particulars_vc AS 'Particulars',RM_Category_vc AS 'Category',RM_RptType_vc AS 'RptType' FROM tbl_RptMaster WHERE RM_Show_vc='YES' ORDER BY RM_Particulars_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        RowIndex = 0
        DgvRptMaster.Rows.Clear()
        For Each Dr As DataRow In Ds.Tables("FillData").Rows
            DgvRptMaster.Rows.Add()
            DgvRptMaster.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
            DgvRptMaster.Rows(RowIndex).Cells("Particulars").Value = Dr.Item("Particulars")
            DgvRptMaster.Rows(RowIndex).Cells("Category").Value = Dr.Item("Category")
            DgvRptMaster.Rows(RowIndex).Cells("RptType").Value = Dr.Item("RptType")
            DgvRptMaster.Rows(RowIndex).Cells("Tick").Value = "NO"
            RowIndex += 1
        Next
    End Sub
    Private Sub DgvRptMaster_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvRptMaster.CellClick

        If e.RowIndex < 0 Then Exit Sub
        If e.ColumnIndex <> 1 Then Exit Sub

        For Each Dr As DataGridViewRow In DgvRptMaster.Rows
            Dr.Cells("Tick").Value = "NO"
        Next

        DgvRptMaster.Rows(e.RowIndex).Cells("Tick").Value = "YES"

        SingleCus()

    End Sub
  
    Private Sub SingleCus()

        For Each Dr As DataGridViewRow In DgvRptMaster.Rows
            If Dr.Cells("Tick").Value = "YES" Then
                RptTypeChk = Dr.Cells("RptType").Value
            End If
        Next

        Select Case RptTypeChk
            Case "SINGLE CUSTOMER SUMMARY", "SINGLE CUSTOMER DETAIL", "SINGLE AREA SUMMARY", "SINGLE PRODUCT DETAIL", "SINGLE CATEGORY",
                "SINGLE EMPLOYEE", "SINGLE PO DETAIL", "SINGLE QUOTATION DETAIL"

                txtCustName.Text = String.Empty
                txtCusID.Text = String.Empty
                txtCustName.Show()
                txtCusID.Show()
                LblHeader.Show()
                txtCustName.Focus()
            Case Else
                txtCustName.Hide()
                txtCusID.Hide()
                LblHeader.Hide()
                DGVCustomer.Hide()
        End Select

    End Sub
    Private Sub MonthCalendar1_DateSelected(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        If FromBool = True Then
            If Format(CDate(txtToDate.Text), "yyyy/MM/dd") < Format(CDate(MonthCalendar1.SelectionRange.Start), "yyyy/MM/dd") Then
                MsgBox("Please,Check the date")
                Exit Sub
            End If
            txtFromDate.Text = Format(CDate(MonthCalendar1.SelectionRange.Start), "dd/MM/yyyy")
            MonthCalendar1.Hide()
        ElseIf ToBool = True Then
            If Format(CDate(MonthCalendar1.SelectionRange.Start), "yyyy/MM/dd") < Format(CDate(txtFromDate.Text), "yyyy/MM/dd") Then
                MsgBox("Please,Check the date")
                Exit Sub
            End If
            txtToDate.Text = Format(CDate(MonthCalendar1.SelectionRange.Start), "dd/MM/yyyy")
            MonthCalendar1.Hide()
        End If
    End Sub
    Private Sub btnFrom4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrom4.Click
        FromBool = True
        ToBool = False
        MonthCalendar1.Show()
        MonthCalendar1.BringToFront()
        MonthCalendar1.Location = New System.Drawing.Point(txtFromDate.Location.X, txtFromDate.Location.Y + 30)
    End Sub

    Private Sub btnTo4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTo4.Click
        ToBool = True
        FromBool = False
        MonthCalendar1.Show()
        MonthCalendar1.BringToFront()
        MonthCalendar1.Location = New System.Drawing.Point(txtToDate.Location.X, txtToDate.Location.Y + 30)
    End Sub
    Private Sub BtnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview.Click

        Dim SelectBool As Boolean = False
        For Each Dr As DataGridViewRow In DgvRptMaster.Rows
            If Dr.IsNewRow Then Continue For

            If Convert.ToString(Dr.Cells("Tick").Value) = "YES" Then
                SelectBool = True
                RptCategoryChk = Convert.ToString(Dr.Cells("Category").Value)
                RptTypeChk = Convert.ToString(Dr.Cells("RptType").Value)
                Exit For
            End If
        Next

        If Not SelectBool Then
            MessageBox.Show("Select any one.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Select Case RptCategoryChk.ToUpper()
            Case "DETAIL"

                MessageBox.Show("PDF report module will be enabled with the SuperMarket Reports.",
                        "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Case "SUMMARY"

                MessageBox.Show("PDF report module will be enabled with the SuperMarket Reports.",
                        "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select

    End Sub
 
    Private Sub InvenReportDetail()
        Try
            DgvProSales.Rows.Clear()
            DgvProSales.Columns.Clear()

            Dim dtDesign As New DataTable
            Dim SqlDgvTablechk As String = ""

            Select Case RptTypeChk
                Case "PURCHASE", "PURCHASE RETURN"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Pur WHERE BD_PurDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "INVOICE", "ESTIMATE", "SALES RETURN", "SINGLE CUSTOMER DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Sal WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "RECEIPT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_St WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PROFIT DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProfitDtl WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SINGLE PRODUCT DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProSingle WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "QUOTATION DETAIL", "SINGLE QUOTATION DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Quo WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PO DETAIL", "SINGLE PO DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_PO WHERE BD_PurDtl_vc='YES' ORDER BY BD_ColOrderby_i"
            End Select

            Using daDesign As New SqlDataAdapter(SqlDgvTablechk, VistawinFunctions.ServerConnection)
                daDesign.Fill(dtDesign)
            End Using

            If dtDesign.Rows.Count = 0 Then
                MsgBox("Bill Design Not Found")
                Exit Sub
            End If

            Dim ColList As String = ""
            For Each dr As DataRow In dtDesign.Rows
                ColList &= dr("BD_SQLColumn_vc").ToString() & " AS [" & dr("BD_ColumnName_vc").ToString() & "],"
            Next
            ColList = ColList.TrimEnd(","c)

            Dim dtSale As New DataTable
            Dim Sql As String = ""

            Select Case RptTypeChk
                Case "PURCHASE"
                    Sql = "SELECT " & ColList & " FROM tbl_PurchDetail sa LEFT JOIN tbl_PurchParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "PURCHASE RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_PurchDetail sa LEFT JOIN tbl_PurchParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE RETURN') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "INVOICE", "ESTIMATE"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "SALES RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('SALES RETURN') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "PROFIT DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sp.Sa_RecordNo2_i"
                Case "SINGLE CUSTOMER DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND sa.Sa_CusID_n=@CusID ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "SINGLE PRODUCT DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND Sa_StNumber_n=@CusID ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "QUOTATION DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_QuoDetail sa LEFT JOIN tbl_QuoParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('QUOTATION') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "SINGLE QUOTATION DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_QuoDetail sa LEFT JOIN tbl_QuoParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('QUOTATION') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND sa.Sa_CusID_n=@CusID ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "PO DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_PODetail sa LEFT JOIN tbl_POParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE ORDER') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "SINGLE PO DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_PODetail sa LEFT JOIN tbl_POParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE ORDER') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND sa.Sa_CusID_n=@CusID ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
            End Select

            Using cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@FromDate", CDate(txtFromDate.Text))
                cmd.Parameters.AddWithValue("@ToDate", CDate(txtToDate.Text))
                cmd.Parameters.AddWithValue("@CusID", txtCusID.Text)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtSale)
                End Using
            End Using

            If dtSale.Rows.Count = 0 Then
                MsgBox("No Data")
                Exit Sub
            End If

            Dim ColTotals As New Dictionary(Of Integer, Decimal)
            Dim SNoCol As New DataGridViewTextBoxColumn
            With SNoCol
                .Name = "SNo"
                .HeaderText = "S.No"
                .Width = 60
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            DgvProSales.Columns.Add(SNoCol)

            For Each dr As DataRow In dtDesign.Rows
                Dim dgCol As New DataGridViewTextBoxColumn
                dgCol.Name = dr("BD_ColumnName_vc").ToString()
                dgCol.HeaderText = dr("BD_AliasName_vc").ToString()
                dgCol.Width = Val(dr("BD_ColWidth_vc"))

                Select Case dr("BD_ColAlign_vc").ToString().ToUpper()
                    Case "LEFT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Case "CENTER"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    Case "RIGHT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select
                DgvProSales.Columns.Add(dgCol)
            Next

            Dim PrevInvNo As String = ""
            Dim SNo As Integer = 1
            Dim GrandTotal As Decimal = 0

            Dim HideCols As New List(Of Integer)
            Dim InvNoField As String = ""
            Dim FinalTotalCol As Integer = -1
            Dim FinalTotalField As String = ""

            For i As Integer = 0 To dtDesign.Rows.Count - 1
                Dim DataType As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()
                Select Case DataType
                    Case "TITLE"
                        InvNoField = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                        HideCols.Add(i)
                    Case "DATE"
                        HideCols.Add(i)
                    Case "FINALTOTAL"
                        FinalTotalField = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                        FinalTotalCol = i + 1
                        HideCols.Add(i)
                End Select
            Next

            For Each dr As DataRow In dtDesign.Rows
                If dr("BD_SQLColumn_vc").ToString().ToUpper().Contains("SA_INVNO_VC") Then
                    InvNoField = dr("BD_ColumnName_vc").ToString()
                    Exit For
                End If
            Next

            If InvNoField = "" Then
                MsgBox("Invoice No field not found in tbl_DgvReport")
                Exit Sub
            End If

            For Each drSale As DataRow In dtSale.Rows
                Dim Row As Integer = DgvProSales.Rows.Add()
                Dim CurInvNo As String = drSale(InvNoField).ToString()

                If PrevInvNo <> CurInvNo Then

                    DgvProSales.Rows(Row).Cells("SNo").Value = SNo
                    SNo += 1
                    If FinalTotalField <> "" AndAlso Not IsDBNull(drSale(FinalTotalField)) Then
                        GrandTotal += CDec(drSale(FinalTotalField))
                    End If

                    For i As Integer = 0 To dtDesign.Rows.Count - 1
                        Dim ColName As String = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                        Dim DataType As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()

                        If IsDBNull(drSale(ColName)) Then
                            DgvProSales.Rows(Row).Cells(i + 1).Value = ""
                        Else
                            Select Case DataType

                                Case "DATE"
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy")

                                Case "QTY"
                                    Dim Qty As Decimal = Val(drSale(ColName))
                                    If Qty = Math.Truncate(Qty) Then
                                        DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N0")
                                    Else
                                        DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N3")
                                    End If

                                Case "DECIMAL", "NUMBER", "NUMERIC"
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = FormatNumber(Val(drSale(ColName)), 2)
                                Case Else
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = drSale(ColName)
                            End Select
                        End If
                    Next

                Else
                    For i As Integer = 0 To dtDesign.Rows.Count - 1
                        Dim ColName As String = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                        Dim DataType As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()

                        If HideCols.Contains(i) Then
                            DgvProSales.Rows(Row).Cells(i + 1).Value = ""

                        ElseIf IsDBNull(drSale(ColName)) Then
                            DgvProSales.Rows(Row).Cells(i + 1).Value = ""
                        Else
                            Select Case DataType
                                Case "DATE"
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy")
                                Case "QTY"
                                    Dim Qty As Decimal = Val(drSale(ColName))
                                    If Qty = Math.Truncate(Qty) Then
                                        DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N0")
                                    Else
                                        DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N3")
                                    End If
                                Case "DECIMAL", "NUMBER", "NUMERIC"
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = FormatNumber(Val(drSale(ColName)), 2)

                                Case Else
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = drSale(ColName)
                            End Select
                        End If
                    Next
                End If
                PrevInvNo = CurInvNo
            Next

            Dim TotalRow As Integer = DgvProSales.Rows.Add()
            With DgvProSales.Rows(TotalRow)
                .DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .DefaultCellStyle.ForeColor = Color.Red
                .Cells("SNo").Value = "TOTAL"
                If FinalTotalCol >= 0 Then
                    .Cells(FinalTotalCol).Value = FormatNumber(GrandTotal, 2)
                End If
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub InvenReportSummary()
        Try
            DgvProSales.Rows.Clear()
            DgvProSales.Columns.Clear()

            Dim dtDesign As New DataTable
            Dim SqlDgvTablechk As String = ""

            Select Case RptTypeChk
                Case "PURCHASE", "PURCHASE RETURN"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Pur WHERE BD_PurSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "INVOICE", "ESTIMATE", "SALES RETURN", "SINGLE CUSTOMER SUMMARY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Sal WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "STOCK"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_St WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CATEGORY", "SINGLE CATEGORY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Cat WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "LAST BILL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_LastBill WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CASHCREDIT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_CashCredit WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PROSUMMARY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProSum WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PROFIT SUMMARY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProfitSum WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PRODUCT LASTBILL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProLast WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PRODUCT DISCOUNT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleProDisc WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "BILL DISCOUNT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_BillDisc WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "AREA WISE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleArea WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "DIVISION WISE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleDivision WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "AREA DATEWISE", "SINGLE AREA SUMMARY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleAreaDW WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "NOOFITEMS"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_NoofItems WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "BILL VALUE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_CusBillValue WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "EMP WISE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleEmp WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "RECEIPT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_Rec_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "DISCOUNT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_Dis_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SALE RETURN"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_SRT_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PAYMENT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_Pay_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SUP DISCOUNT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_SupDisc_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SINGLE EMPLOYEE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleEmpSingle WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CUS OUTSTANDING"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Outs WHERE BD_CusOut_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SUP OUTSTANDING"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Outs WHERE BD_SupOut_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "EMP OUTSTANDING"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Outs WHERE BD_EmpOut_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CUSTOMER LIST", "SUPPLIER LIST", "EMPLOYEE LIST", "CONTACT LIST"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvSubRpt_Cus WHERE BD_ColShow_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "STOCK LIST"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvSubRpt_Stock WHERE BD_StockDtl_vc='YES' ORDER BY BD_ColOrderby_i"
            End Select

            Using daDesign As New SqlDataAdapter(SqlDgvTablechk, VistawinFunctions.ServerConnection)
                daDesign.Fill(dtDesign)
            End Using

            If dtDesign.Rows.Count = 0 Then
                MsgBox("Bill Design Not Found")
                Exit Sub
            End If

            Dim ColList As String = ""
            For Each dr As DataRow In dtDesign.Rows
                ColList &= dr("BD_SQLColumn_vc").ToString() & " AS [" & dr("BD_ColumnName_vc").ToString() & "],"
            Next
            ColList = ColList.TrimEnd(","c)

            Dim dtSale As New DataTable
            Dim Sql As String = ""

            Select Case RptTypeChk
                Case "PURCHASE"
                    Sql = "SELECT " & ColList & " FROM tbl_PurchDetail sa LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "PURCHASE RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_PurchDetail sa LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE RETURN') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "INVOICE", "ESTIMATE"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "SALES RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) WHERE sa.Sa_SaveType_vc IN('SALES RETURN') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "RECEIPT test"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_ReceiptInvWise re ON (sa.Sa_InvNo_vc=re.Rec_Inv_No_vc) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "CATEGORY"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock m ON(sp.Sa_StNumber_n=m.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=m.St_StockID_n) LEFT JOIN tbl_Category ca ON(s.St_CatID_i=ca.Category_Code_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY Sa_SaveType_vc,CONVERT(DATE,sa.Sa_BillDate_d),Category_Name_vc ORDER BY CONVERT(DATE,sa.Sa_BillDate_d),Category_Name_vc"
                Case "LAST BILL"
                    Sql = "WITH BALAS AS(SELECT " & ColList & " ,ROW_NUMBER ()OVER(PARTITION BY Sa_CusID_n ORDER BY Sa_BillDate_d DESC)AS rn FROM tbl_SalDetail AS a )SELECT * FROM BALAS WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND rn=1 ORDER BY Sa_BillDate_d DESC"
                Case "CASHCREDIT"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail a WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND Sa_BillDate_d BETWEEN @FromDate AND @ToDate"
                Case "PROSUMMARY"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock m ON(sp.Sa_StNumber_n=m.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=m.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY s.St_StockID_n,s.St_ProName_vc ORDER BY SUM(Sa_TQty_i) DESC,s.St_ProName_vc"
                Case "PROFIT SUMMARY"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY sp.Sa_BillDate_d,Sa_SaveType_vc,sp.Sa_InvNo_vc,sa.Sa_CusName_vc,Sa_FinalTotal_n,Sa_Acyear_vc,Sa_BillNo_n ORDER BY sp.Sa_BillDate_d,Sa_Acyear_vc,sa.Sa_BillNo_n"
                Case "PRODUCT LASTBILL"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock m ON(sp.Sa_StNumber_n=m.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=m.St_StockID_n) WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') GROUP BY s.St_StockID_n,St_StNumber_i,s.St_ProName_vc,St_MRPRate_n ORDER BY DiffDays DESC"
                Case "PRODUCT DISCOUNT"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON(sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND Sa_ProDPer_n>0 GROUP BY sa.Sa_BillDate_d,Sa_SaveType_vc,sa.Sa_InvNo_vc,Sa_FinalTotal_n,Sa_CusName_vc,Sa_ProDPer_n,Sa_BillNo_n ORDER BY sa.Sa_BillDate_d,Sa_BillNo_n"
                Case "BILL DISCOUNT"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND ISNULL(Sa_DiscountAmt_n,0.00)>0 ORDER BY sa.Sa_BillDate_d,Sa_BillNo_n"
                Case "AREA WISE"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_CusProfile cu ON(Sa.Sa_CusID_n=cu.C_ID_n AND C_Type_vc='CUSTOMER') LEFT JOIN tbl_Area ar ON(cu.C_AreaCode_i=ar.Area_Code_n) GROUP BY Area_Name_vc ORDER BY ISNULL(SUM(Sa_FinalTotal_n),0.00) DESC"
                Case "DIVISION WISE"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_CusProfile cu ON(Sa.Sa_CusID_n=cu.C_ID_n AND C_Type_vc='CUSTOMER') LEFT JOIN tbl_Area ar ON(cu.C_AreaCode_i=ar.Area_Code_n) LEFT JOIN tbl_AreaDivision ad ON(ad.Di_DiviID_i=ar.Di_DiviID_i) GROUP BY Di_DivisionName_vc ORDER BY ISNULL(SUM(Sa_FinalTotal_n),0.00) DESC"
                Case "AREA DATEWISE"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON(sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock m ON(sp.Sa_Stnumber_n=m.St_StNumber_i)LEFT JOIN tbl_StockDtl s ON(m.St_StockID_n=s.St_StockID_n) LEFT JOIN tbl_CusProfile cu ON(sa.Sa_CusID_n=cu.C_ID_n AND C_Type_vc='CUSTOMER')LEFT JOIN tbl_Area ar ON(C_AreaCode_i=ar.Area_Code_n) WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY sa.Sa_BillDate_d,Sa_SaveType_vc,Sa_FinalTotal_n,Area_Name_vc,Area_Code_n ORDER BY sa.Sa_BillDate_d,Area_Name_vc"
                Case "NOOFITEMS"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON(sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY sp.Sa_BillDate_d,sp.Sa_InvNo_vc,sa.Sa_CusName_vc,Sa_BillNo_n ORDER BY sp.Sa_BillDate_d,Sa_BillNo_n"
                Case "BILL VALUE"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') GROUP BY sa.Sa_CusID_n,sa.Sa_CusName_vc ORDER BY SUM(Sa_FinalTotal_n) DESC"
                Case "EMP WISE"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_CusProfile em ON(sa.Sa_EmpID_i=em.C_ID_n AND em.C_Type_vc='EMPLOYEE') WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') GROUP BY em.C_ID_n,em.C_Name_vc ORDER BY ISNULL(SUM(Sa_FinalTotal_n),0.00) DESC"
                Case "RECEIPT"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise a LEFT JOIN tbl_CusProfile em ON(a.Rec_CollEmpID_i=em.C_ID_n AND C_Type_vc='EMPLOYEE') WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='CUSTOMER' AND Rec_Type_vc='RECEIPT' ORDER BY Rec_Date_d"
                Case "DISCOUNT"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise a LEFT JOIN tbl_CusProfile em ON(a.Rec_CollEmpID_i=em.C_ID_n AND C_Type_vc='EMPLOYEE') WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='CUSTOMER' AND Rec_Type_vc='DISCOUNT' ORDER BY Rec_Date_d"
                Case "SALE RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise a LEFT JOIN tbl_CusProfile em ON(a.Rec_CollEmpID_i=em.C_ID_n AND C_Type_vc='EMPLOYEE') WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='CUSTOMER' AND Rec_Type_vc='SALE RETURN' ORDER BY Rec_Date_d"
                Case "PAYMENT"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise a LEFT JOIN tbl_CusProfile em ON(a.Rec_CollEmpID_i=em.C_ID_n AND C_Type_vc='EMPLOYEE') WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='SUPPLIER' AND Rec_Type_vc='PAYMENT' ORDER BY Rec_Date_d"
                Case "SUP DISCOUNT"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise a LEFT JOIN tbl_CusProfile em ON(a.Rec_CollEmpID_i=em.C_ID_n AND C_Type_vc='EMPLOYEE') WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='SUPPLIER' AND Rec_Type_vc='SUP DISCOUNT' ORDER BY Rec_Date_d"
                Case "CUS OUTSTANDING"
                    Sql = "SELECT " & ColList & " FROM CustOutstanding ORDER BY CustName"
                Case "SUP OUTSTANDING"
                    Sql = "SELECT " & ColList & " FROM SupOutstanding ORDER BY CustName"
                Case "EMP OUTSTANDING"
                    Sql = "SELECT " & ColList & " FROM EmpOutstanding ORDER BY CustName"
                Case "CUSTOMER LIST"
                    Sql = "SELECT " & ColList & " FROM tbl_CusProfile WHERE C_Type_vc='CUSTOMER'"
                Case "SUPPLIER LIST"
                    Sql = "SELECT " & ColList & " FROM tbl_CusProfile WHERE C_Type_vc='SUPPLIER'"
                Case "EMPLOYEE LIST"
                    Sql = "SELECT " & ColList & " FROM tbl_CusProfile WHERE C_Type_vc='EMPLOYEE'"
                Case "CONTACT LIST"
                    Sql = "SELECT " & ColList & " FROM tbl_CusProfile WHERE C_Type_vc='CONTACT'"
                Case "STOCK LIST"
                    Sql = "SELECT 'NO' AS Tick," & ColList & _
                   " FROM tbl_StockDtl s " & _
                   " LEFT JOIN tbl_MultiStock ms ON s.St_StockID_n=ms.St_StockID_n " & _
                   " LEFT JOIN tbl_Category c ON s.St_CatID_i=c.Category_Code_n " & _
                   " LEFT JOIN tbl_Brand br ON s.St_BrandID_i=br.Brand_Code_n " & _
                   " ORDER BY s.St_ProName_vc"


                Case "SINGLE CUSTOMER SUMMARY"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER' AND sa.Sa_CusID_n=cu.C_ID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND sa.Sa_CusID_n=@CusID ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "SINGLE AREA SUMMARY"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON(sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock m ON(sp.Sa_Stnumber_n=m.St_StNumber_i)LEFT JOIN tbl_StockDtl s ON(m.St_StockID_n=s.St_StockID_n) LEFT JOIN tbl_CusProfile cu ON(sa.Sa_CusID_n=cu.C_ID_n AND C_Type_vc='CUSTOMER')LEFT JOIN tbl_Area ar ON(C_AreaCode_i=ar.Area_Code_n) WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND Area_Code_n=@CusID GROUP BY sa.Sa_BillDate_d,Sa_SaveType_vc,Sa_FinalTotal_n,Area_Name_vc,Area_Code_n ORDER BY sa.Sa_BillDate_d,Area_Name_vc"
                Case "SINGLE CATEGORY"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock m ON(sp.Sa_StNumber_n=m.St_StNumber_i) LEFT JOIN tbl_StockDtl s ON(s.St_StockID_n=m.St_StockID_n) LEFT JOIN tbl_Category ca ON(s.St_CatID_i=ca.Category_Code_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND ca.Category_Code_n=@CusID GROUP BY Sa_SaveType_vc,CONVERT(DATE,sa.Sa_BillDate_d),Category_Name_vc ORDER BY CONVERT(DATE,sa.Sa_BillDate_d),Category_Name_vc"
                Case "SINGLE EMPLOYEE"
                    Sql = "SELECT " & ColList & " FROM tbl_SalDetail sa LEFT JOIN tbl_SalParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile em ON (C_Type_vc='EMPLOYEE' AND sa.Sa_EmpID_i=em.C_ID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND Sa_EmpID_i=@CusID GROUP BY sa.Sa_BillDate_d,sa.Sa_SaveType_vc, sa.Sa_InvNo_vc ,sa.Sa_CusID_n,Sa_CusName_vc,Sa_FinalTotal_n,Sa_EmpID_i,em.C_Name_vc,Sa_EmpPer_n,Sa_EmpAmt_n,Sa_BillNo_n ORDER BY sa.Sa_BillDate_d,Sa_BillNo_n"
            End Select

            Using cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@FromDate", CDate(txtFromDate.Text))
                cmd.Parameters.AddWithValue("@ToDate", CDate(txtToDate.Text))
                cmd.Parameters.AddWithValue("@CusID", txtCusID.Text)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtSale)
                End Using
            End Using

            If dtSale.Rows.Count = 0 Then
                MsgBox("No Data")
                Exit Sub
            End If

            Dim ColTotals As New Dictionary(Of Integer, Decimal)
            Dim SNoCol As New DataGridViewTextBoxColumn
            With SNoCol
                .Name = "SNo"
                .HeaderText = "S.No"
                .Width = 60
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            DgvProSales.Columns.Add(SNoCol)

            For Each dr As DataRow In dtDesign.Rows
                Dim dgCol As New DataGridViewTextBoxColumn
                dgCol.Name = dr("BD_ColumnName_vc").ToString()
                dgCol.HeaderText = dr("BD_AliasName_vc").ToString()
                dgCol.Width = Val(dr("BD_ColWidth_vc"))

                Select Case dr("BD_ColAlign_vc").ToString().ToUpper()
                    Case "LEFT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Case "CENTER"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    Case "RIGHT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select
                DgvProSales.Columns.Add(dgCol)
            Next


            For Each drSale As DataRow In dtSale.Rows
                Dim Row As Integer = DgvProSales.Rows.Add()
                DgvProSales.Rows(Row).Cells("SNo").Value = Row + 1

                For i As Integer = 0 To dtDesign.Rows.Count - 1
                    Dim ColName As String = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                    Dim Dt As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()

                    If Not dtSale.Columns.Contains(ColName) Then
                        Continue For
                    End If

                    If drSale.IsNull(ColName) Then
                        DgvProSales.Rows(Row).Cells(i + 1).Value = ""
                    Else
                        Select Case Dt
                            Case "DATE"
                                DgvProSales.Rows(Row).Cells(i + 1).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy")
                            Case "QTY"
                                Dim Qty As Decimal = Val(drSale(ColName))
                                If Qty = Math.Truncate(Qty) Then
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N0")
                                Else
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N3")
                                End If
                            Case "DECIMAL", "NUMBER", "NUMERIC", "FINALTOTAL"
                                DgvProSales.Rows(Row).Cells(i + 1).Value = FormatNumber(Val(drSale(ColName)), 2)
                            Case Else
                                DgvProSales.Rows(Row).Cells(i + 1).Value = drSale(ColName)
                        End Select
                    End If

                    If Dt = "DECIMAL" OrElse Dt = "NUMBER" OrElse Dt = "NUMERIC" OrElse Dt = "FINALTOTAL" OrElse Dt = "QTY" Then
                        If Not ColTotals.ContainsKey(i) Then
                            ColTotals.Add(i, 0D)
                        End If
                        If Not drSale.IsNull(ColName) Then
                            ColTotals(i) += Convert.ToDecimal(drSale(ColName))
                        End If
                    End If
                Next
            Next

            Dim TotalRow As Integer = DgvProSales.Rows.Add()
            With DgvProSales.Rows(TotalRow)
                .DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .DefaultCellStyle.ForeColor = Color.Red
                .Cells("SNo").Value = "TOTAL"
            End With
            For Each kv In ColTotals
                DgvProSales.Rows(TotalRow).Cells(kv.Key + 1).Value = FormatNumber(kv.Value, 2)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCustName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustName.TextChanged

        If txtCustName.Text.Trim = "" Then
            DGVCustomer.Hide()
            Exit Sub
        End If

        Dim ds As New DataSet
        Select Case RptTypeChk
            Case "SINGLE CUSTOMER SUMMARY", "SINGLE CUSTOMER DETAIL", "SINGLE QUOTATION DETAIL"
                Adapter = New SqlDataAdapter("SELECT C_Name_vc AS 'Name',C_ID_n AS 'ID',C_Mobile_vc AS 'MobileNo',C_Add1_vc+','+C_Add2_vc+','+C_Add3_vc AS 'Address' FROM tbl_CusProfile WHERE C_Type_vc='CUSTOMER' AND (C_Mobile_vc like '" & txtCustName.Text + "%" & "' OR C_Name_vc LIKE '" & txtCustName.Text + "%" & "' OR C_ID_n LIKE '" & txtCustName.Text + "%" & "')  ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
            Case "SINGLE AREA SUMMARY"
                Adapter = New SqlDataAdapter("SELECT Area_Name_vc AS 'AreaName',Area_Code_n AS 'AreaCode','' AS 'MobileNo','' AS 'Address' FROM tbl_Area WHERE (Area_Name_vc LIKE '" & txtCustName.Text + "%" & "')  ORDER BY Area_Name_vc", VistawinFunctions.ServerConnection)
            Case "SINGLE PRODUCT DETAIL"
                Adapter = New SqlDataAdapter("SELECT DISTINCT s.St_ProName_vc AS 'Product Name',St_StNumber_i AS 'StNumber','' AS 'Category','' AS 'Brand' FROM tbl_StockDtl s LEFT JOIN tbl_MultiStock ms ON(s.St_StockID_n=ms.St_StockID_n) WHERE (s.St_ProName_vc LIKE '" & txtCustName.Text + "%" & "')  ORDER BY s.St_ProName_vc", VistawinFunctions.ServerConnection)
            Case "SINGLE CATEGORY"
                Adapter = New SqlDataAdapter("SELECT Category_Name_vc AS 'Category Name',Category_Code_n as 'Category Code','' AS 'A','' AS 'B' FROM tbl_Category WHERE (Category_Name_vc LIKE '" & txtCustName.Text + "%" & "')  ORDER BY Category_Name_vc", VistawinFunctions.ServerConnection)
            Case "SINGLE EMPLOYEE"
                Adapter = New SqlDataAdapter("SELECT C_Name_vc AS 'Name',C_ID_n AS 'ID',C_Mobile_vc AS 'MobileNo',C_Add1_vc+','+C_Add2_vc+','+C_Add3_vc AS 'Address' FROM tbl_CusProfile WHERE C_Type_vc='EMPLOYEE' AND (C_Mobile_vc like '" & txtCustName.Text + "%" & "' OR C_Name_vc LIKE '" & txtCustName.Text + "%" & "' OR C_ID_n LIKE '" & txtCustName.Text + "%" & "')  ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
            Case "SINGLE PO DETAIL"
                Adapter = New SqlDataAdapter("SELECT C_Name_vc AS 'Name',C_ID_n AS 'ID',C_Mobile_vc AS 'MobileNo',C_Add1_vc+','+C_Add2_vc+','+C_Add3_vc AS 'Address' FROM tbl_CusProfile WHERE C_Type_vc='SUPPLIER' AND (C_Mobile_vc like '" & txtCustName.Text + "%" & "' OR C_Name_vc LIKE '" & txtCustName.Text + "%" & "' OR C_ID_n LIKE '" & txtCustName.Text + "%" & "')  ORDER BY C_Name_vc", VistawinFunctions.ServerConnection)
        End Select
        Adapter.Fill(ds, "Customer")

        DGVCustomer.DataSource = ds.Tables("Customer")
        If ds.Tables("Customer").Rows.Count > 0 Then
            DGVCustomer.Show()
            DGVCustomer.Columns(0).Width = 300
            DGVCustomer.Columns(1).Width = 50
            DGVCustomer.Columns(1).Visible = False
            DGVCustomer.Columns(2).Width = 100
            DGVCustomer.Columns(3).Width = 300
        Else
            DGVCustomer.Hide()
        End If

    End Sub

    Private Sub txtCustName_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtCustName.KeyDown

        If e.KeyCode = Keys.Down Then
            DGVCustomer.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            Customer()
        End If

    End Sub

    Private Sub DGVCustomer_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGVCustomer.CellClick
        Customer()
    End Sub

    Private Sub DGVCustomer_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DGVCustomer.KeyDown

        If e.KeyCode = Keys.Enter Then
            Customer()
        End If
    End Sub

    Private Sub Customer()
        If DGVCustomer.CurrentRow Is Nothing Then Exit Sub
        txtCustName.Text = DGVCustomer.CurrentRow.Cells(0).Value.ToString()
        txtCusID.Text = DGVCustomer.CurrentRow.Cells(1).Value.ToString()
        DGVCustomer.Hide()
    End Sub

    Private Sub BtnShowHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShowHide.Click

        GrpLeft.Visible = Not GrpLeft.Visible

        If GrpLeft.Visible Then
            BtnShowHide.Text = "<< Hide"
        Else
            BtnShowHide.Text = ">> Show"
        End If

    End Sub
    Private Sub txtNameSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameSearch.TextChanged

        Dim SearchText As String = txtNameSearch.Text.Trim()
        DgvRptMaster.Rows.Clear()

        If SearchText = "" Then
            RptMasterList()
            Exit Sub
        End If

        Dim dt As New DataTable
        Using da As New SqlDataAdapter("SELECT RM_Particulars_vc AS Particulars, RM_Category_vc AS Category, RM_RptType_vc AS RptType FROM tbl_RptMaster WHERE RM_Show_vc='YES' AND RM_Particulars_vc LIKE @Search + '%' ORDER BY RM_Particulars_vc", VistawinFunctions.ServerConnection)
            da.SelectCommand.Parameters.AddWithValue("@Search", SearchText)
            da.Fill(dt)
        End Using

        For i As Integer = 0 To dt.Rows.Count - 1
            DgvRptMaster.Rows.Add()
            DgvRptMaster.Rows(i).Cells("SNo").Value = i + 1
            DgvRptMaster.Rows(i).Cells("Tick").Value = "NO"
            DgvRptMaster.Rows(i).Cells("Particulars").Value = dt.Rows(i)("Particulars")
            DgvRptMaster.Rows(i).Cells("Category").Value = dt.Rows(i)("Category")
            DgvRptMaster.Rows(i).Cells("RptType").Value = dt.Rows(i)("RptType")
        Next

        If DgvRptMaster.Rows.Count > 0 Then
            DgvRptMaster.CurrentCell = DgvRptMaster.Rows(0).Cells("Particulars")
        End If

    End Sub

    Private Sub BtnPdfReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPdfReport.Click

        MessageBox.Show("PDF report module will be enabled with the SuperMarket Reports.",
                        "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BtnGSTPdfRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGSTPdfRpt.Click

        MessageBox.Show("PDF report module will be enabled with the SuperMarket Reports.",
                        "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
    Private Sub ClinicReportDetail()
        Try
            DgvProSales.Rows.Clear()
            DgvProSales.Columns.Clear()

            Dim dtDesign As New DataTable
            Dim SqlDgvTablechk As String = ""

            Select Case RptTypeChk
                Case "PURCHASE", "PURCHASE RETURN"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Pur WHERE BD_PurDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "INVOICE", "ESTIMATE", "SALES RETURN", "SINGLE CUSTOMER DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Sal WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "RECEIPT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_St WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PROFIT DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProfitDtl WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SINGLE PRODUCT DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProSingle WHERE BD_SalDtl_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PO DETAIL", "SINGLE PO DETAIL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_PO WHERE BD_PurDtl_vc='YES' ORDER BY BD_ColOrderby_i"
            End Select

            Using daDesign As New SqlDataAdapter(SqlDgvTablechk, VistawinFunctions.ServerConnection)
                daDesign.Fill(dtDesign)
            End Using

            If dtDesign.Rows.Count = 0 Then
                MsgBox("Bill Design Not Found")
                Exit Sub
            End If

            Dim ColList As String = ""
            For Each dr As DataRow In dtDesign.Rows
                ColList &= dr("BD_SQLColumn_vc").ToString() & " AS [" & dr("BD_ColumnName_vc").ToString() & "],"
            Next
            ColList = ColList.TrimEnd(","c)

            Dim dtSale As New DataTable
            Dim Sql As String = ""

            Select Case RptTypeChk
                Case "PURCHASE"
                    Sql = "SELECT " & ColList & " FROM tbl_PurchDetail sa LEFT JOIN tbl_PurchParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "PURCHASE RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_PurchDetail sa LEFT JOIN tbl_PurchParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE RETURN') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "INVOICE", "ESTIMATE"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "SALES RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('SALES RETURN') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "PROFIT DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sp.Sa_RecordNo2_i"
                Case "SINGLE PRODUCT DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND Sa_StNumber_n=@CusID ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "PO DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_PODetail sa LEFT JOIN tbl_POParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE ORDER') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
                Case "SINGLE PO DETAIL"
                    Sql = "SELECT " & ColList & " FROM tbl_PODetail sa LEFT JOIN tbl_POParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) LEFT JOIN tbl_MultiStock ms ON(sp.Sa_StNumber_n=ms.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=ms.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE ORDER') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND sa.Sa_CusID_n=@CusID ORDER BY sa.Sa_BillDate_d,sa.Sa_InvNo_vc"
            End Select

            Using cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@FromDate", CDate(txtFromDate.Text))
                cmd.Parameters.AddWithValue("@ToDate", CDate(txtToDate.Text))
                cmd.Parameters.AddWithValue("@CusID", txtCusID.Text)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtSale)
                End Using
            End Using

            If dtSale.Rows.Count = 0 Then
                MsgBox("No Data")
                Exit Sub
            End If

            Dim ColTotals As New Dictionary(Of Integer, Decimal)
            Dim SNoCol As New DataGridViewTextBoxColumn
            With SNoCol
                .Name = "SNo"
                .HeaderText = "S.No"
                .Width = 60
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            DgvProSales.Columns.Add(SNoCol)

            For Each dr As DataRow In dtDesign.Rows
                Dim dgCol As New DataGridViewTextBoxColumn
                dgCol.Name = dr("BD_ColumnName_vc").ToString()
                dgCol.HeaderText = dr("BD_AliasName_vc").ToString()
                dgCol.Width = Val(dr("BD_ColWidth_vc"))

                Select Case dr("BD_ColAlign_vc").ToString().ToUpper()
                    Case "LEFT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Case "CENTER"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    Case "RIGHT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select
                DgvProSales.Columns.Add(dgCol)
            Next

            Dim PrevInvNo As String = ""
            Dim SNo As Integer = 1
            Dim GrandTotal As Decimal = 0

            Dim HideCols As New List(Of Integer)
            Dim InvNoField As String = ""
            Dim FinalTotalCol As Integer = -1
            Dim FinalTotalField As String = ""

            For i As Integer = 0 To dtDesign.Rows.Count - 1
                Dim DataType As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()
                Select Case DataType
                    Case "TITLE"
                        InvNoField = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                        HideCols.Add(i)
                    Case "DATE"
                        HideCols.Add(i)
                    Case "FINALTOTAL"
                        FinalTotalField = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                        FinalTotalCol = i + 1
                        HideCols.Add(i)
                End Select
            Next

            For Each dr As DataRow In dtDesign.Rows
                If dr("BD_SQLColumn_vc").ToString().ToUpper().Contains("SA_INVNO_VC") Then
                    InvNoField = dr("BD_ColumnName_vc").ToString()
                    Exit For
                End If
            Next

            If InvNoField = "" Then
                MsgBox("Invoice No field not found in tbl_DgvReport")
                Exit Sub
            End If

            For Each drSale As DataRow In dtSale.Rows
                Dim Row As Integer = DgvProSales.Rows.Add()
                Dim CurInvNo As String = drSale(InvNoField).ToString()

                If PrevInvNo <> CurInvNo Then

                    DgvProSales.Rows(Row).Cells("SNo").Value = SNo
                    SNo += 1
                    If FinalTotalField <> "" AndAlso Not IsDBNull(drSale(FinalTotalField)) Then
                        GrandTotal += CDec(drSale(FinalTotalField))
                    End If

                    For i As Integer = 0 To dtDesign.Rows.Count - 1
                        Dim ColName As String = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                        Dim DataType As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()

                        If IsDBNull(drSale(ColName)) Then
                            DgvProSales.Rows(Row).Cells(i + 1).Value = ""
                        Else
                            Select Case DataType

                                Case "DATE"
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy")

                                Case "QTY"
                                    Dim Qty As Decimal = Val(drSale(ColName))
                                    If Qty = Math.Truncate(Qty) Then
                                        DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N0")
                                    Else
                                        DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N3")
                                    End If

                                Case "DECIMAL", "NUMBER", "NUMERIC"
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = FormatNumber(Val(drSale(ColName)), 2)
                                Case Else
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = drSale(ColName)
                            End Select
                        End If
                    Next

                Else
                    For i As Integer = 0 To dtDesign.Rows.Count - 1
                        Dim ColName As String = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                        Dim DataType As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()

                        If HideCols.Contains(i) Then
                            DgvProSales.Rows(Row).Cells(i + 1).Value = ""

                        ElseIf IsDBNull(drSale(ColName)) Then
                            DgvProSales.Rows(Row).Cells(i + 1).Value = ""
                        Else
                            Select Case DataType
                                Case "DATE"
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy")
                                Case "QTY"
                                    Dim Qty As Decimal = Val(drSale(ColName))
                                    If Qty = Math.Truncate(Qty) Then
                                        DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N0")
                                    Else
                                        DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N3")
                                    End If
                                Case "DECIMAL", "NUMBER", "NUMERIC"
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = FormatNumber(Val(drSale(ColName)), 2)

                                Case Else
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = drSale(ColName)
                            End Select
                        End If
                    Next
                End If
                PrevInvNo = CurInvNo
            Next

            Dim TotalRow As Integer = DgvProSales.Rows.Add()
            With DgvProSales.Rows(TotalRow)
                .DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .DefaultCellStyle.ForeColor = Color.Red
                .Cells("SNo").Value = "TOTAL"
                If FinalTotalCol >= 0 Then
                    .Cells(FinalTotalCol).Value = FormatNumber(GrandTotal, 2)
                End If
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub ClinicReportSummary()
        Try
            DgvProSales.Rows.Clear()
            DgvProSales.Columns.Clear()

            Dim dtDesign As New DataTable
            Dim SqlDgvTablechk As String = ""

            Select Case RptTypeChk
                Case "PURCHASE", "PURCHASE RETURN"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Pur WHERE BD_PurSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "INVOICE", "ESTIMATE", "SALES RETURN", "SINGLE CUSTOMER SUMMARY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Sal WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "STOCK"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_St WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CATEGORY", "SINGLE CATEGORY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Cat WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "LAST BILL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_LastBill WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CASHCREDIT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_CashCredit WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PROSUMMARY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProSum WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PROFIT SUMMARY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProfitSum WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PRODUCT LASTBILL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_ProLast WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PRODUCT DISCOUNT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleProDisc WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "BILL DISCOUNT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_BillDisc WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "AREA WISE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleArea WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "DIVISION WISE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleDivision WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "AREA DATEWISE", "SINGLE AREA SUMMARY"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleAreaDW WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "NOOFITEMS"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_NoofItems WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "BILL VALUE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_CusBillValue WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "EMP WISE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleEmp WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "RECEIPT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_Rec_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "DISCOUNT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_Dis_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SALE RETURN"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_SRT_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "PAYMENT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_Pay_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SUP DISCOUNT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleRec WHERE BD_SupDisc_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "SINGLE EMPLOYEE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_SaleEmpSingle WHERE BD_SalSum_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "Outstanding"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_DgvReport_Outs WHERE BD_CusOut_vc='YES' ORDER BY BD_ColOrderby_i"

            End Select

            Using daDesign As New SqlDataAdapter(SqlDgvTablechk, VistawinFunctions.ServerConnection)
                daDesign.Fill(dtDesign)
            End Using

            If dtDesign.Rows.Count = 0 Then
                MsgBox("Bill Design Not Found")
                Exit Sub
            End If

            Dim ColList As String = ""
            For Each dr As DataRow In dtDesign.Rows
                ColList &= dr("BD_SQLColumn_vc").ToString() & " AS [" & dr("BD_ColumnName_vc").ToString() & "],"
            Next
            ColList = ColList.TrimEnd(","c)

            Dim dtSale As New DataTable
            Dim Sql As String = ""

            Select Case RptTypeChk
                Case "PURCHASE"
                    Sql = "SELECT " & ColList & " FROM tbl_PurchDetail sa LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "PURCHASE RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_PurchDetail sa LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='SUPPLIER' AND sa.Sa_CusID_n=cu.C_ID_n) WHERE sa.Sa_SaveType_vc IN('PURCHASE RETURN') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "INVOICE", "ESTIMATE"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "SALES RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_CusProfile cu ON (C_Type_vc='CUSTOMER') WHERE sa.Sa_SaveType_vc IN('SALES RETURN') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "RECEIPT test"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_ReceiptInvWise re ON (sa.Sa_InvNo_vc=re.Rec_Inv_No_vc) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate ORDER BY CONVERT(DATE,sa.Sa_BillDate_d)"
                Case "CATEGORY"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock m ON(sp.Sa_StNumber_n=m.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=m.St_StockID_n) LEFT JOIN tbl_Category ca ON(s.St_CatID_i=ca.Category_Code_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY Sa_SaveType_vc,CONVERT(DATE,sa.Sa_BillDate_d),Category_Name_vc ORDER BY CONVERT(DATE,sa.Sa_BillDate_d),Category_Name_vc"
                Case "LAST BILL"
                    Sql = "WITH BALAS AS(SELECT " & ColList & " ,ROW_NUMBER ()OVER(ORDER BY Sa_BillDate_d DESC)AS rn FROM tbl_BillDetail AS a )SELECT * FROM BALAS WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND rn=1 ORDER BY Sa_BillDate_d DESC"
                Case "CASHCREDIT"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail a WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND Sa_BillDate_d BETWEEN @FromDate AND @ToDate"
                Case "PROSUMMARY"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc)  LEFT JOIN tbl_MultiStock m ON(sp.Sa_StNumber_n=m.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=m.St_StockID_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY s.St_StockID_n,s.St_ProName_vc ORDER BY SUM(Sa_TQty_i) DESC,s.St_ProName_vc"
                Case "PROFIT SUMMARY"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY sp.Sa_BillDate_d,Sa_SaveType_vc,sp.Sa_InvNo_vc,Sa_FinalTotal_n,Sa_Acyear_vc,Sa_BillNo_n ORDER BY sp.Sa_BillDate_d,Sa_Acyear_vc,sa.Sa_BillNo_n"
                Case "PRODUCT LASTBILL"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock m ON(sp.Sa_StNumber_n=m.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=m.St_StockID_n) WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') GROUP BY s.St_StockID_n,St_StNumber_i,s.St_ProName_vc,St_MRPRate_n ORDER BY DiffDays DESC"
                Case "PRODUCT DISCOUNT"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON(sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND Sa_ProDPer_n>0 GROUP BY sa.Sa_BillDate_d,Sa_SaveType_vc,sa.Sa_InvNo_vc,Sa_FinalTotal_n,Sa_ProDPer_n,Sa_BillNo_n ORDER BY sa.Sa_BillDate_d,Sa_BillNo_n"
                Case "BILL DISCOUNT"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa WHERE Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND ISNULL(Sa_DiscountAmt_n,0.00)>0 ORDER BY sa.Sa_BillDate_d,Sa_BillNo_n"
                Case "NOOFITEMS"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON(sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) WHERE Sa_SaveTy_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate GROUP BY sp.Sa_BillDate_d,sp.Sa_InvNo_vc,Sa_BillNo_n ORDER BY sp.Sa_BillDate_d,Sa_BillNo_n"
                Case "RECEIPT"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='CUSTOMER' AND Rec_Type_vc='RECEIPT' ORDER BY Rec_Date_d"
                Case "DISCOUNT"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='CUSTOMER' AND Rec_Type_vc='DISCOUNT' ORDER BY Rec_Date_d"
                Case "SALE RETURN"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='CUSTOMER' AND Rec_Type_vc='SALE RETURN' ORDER BY Rec_Date_d"
                Case "PAYMENT"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='SUPPLIER' AND Rec_Type_vc='PAYMENT' ORDER BY Rec_Date_d"
                Case "SUP DISCOUNT"
                    Sql = "SELECT " & ColList & " FROM tbl_ReceiptInvWise WHERE CONVERT(DATE,Rec_Date_d) BETWEEN @FromDate AND @ToDate AND Rec_Number_n<>0 AND Rec_TotalAmt_n<>0 AND Rec_ParType_vc='SUPPLIER' AND Rec_Type_vc='SUP DISCOUNT' ORDER BY Rec_Date_d"


                Case "SINGLE CATEGORY"
                    Sql = "SELECT " & ColList & " FROM tbl_BillDetail sa LEFT JOIN tbl_BillParticular sp ON (sa.Sa_InvNo_vc=sp.Sa_InvNo_vc) LEFT JOIN tbl_MultiStock m ON(sp.Sa_StNumber_n=m.St_StNumber_i) LEFT JOIN tbl_Stock s ON(s.St_StockID_n=m.St_StockID_n) LEFT JOIN tbl_Category ca ON(s.St_CatID_i=ca.Category_Code_n) WHERE sa.Sa_SaveType_vc IN('INVOICE','ESTIMATE') AND CONVERT(DATE,sa.Sa_BillDate_d) BETWEEN @FromDate AND @ToDate AND ca.Category_Code_n=@CusID GROUP BY Sa_SaveType_vc,CONVERT(DATE,sa.Sa_BillDate_d),Category_Name_vc ORDER BY CONVERT(DATE,sa.Sa_BillDate_d),Category_Name_vc"
                Case "SUP OUTSTANDING"
                    Sql = "SELECT " & ColList & " FROM SupOutstanding ORDER BY CustName"
            End Select

            Using cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@FromDate", CDate(txtFromDate.Text))
                cmd.Parameters.AddWithValue("@ToDate", CDate(txtToDate.Text))
                cmd.Parameters.AddWithValue("@CusID", txtCusID.Text)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtSale)
                End Using
            End Using

            If dtSale.Rows.Count = 0 Then
                MsgBox("No Data")
                Exit Sub
            End If

            Dim ColTotals As New Dictionary(Of Integer, Decimal)
            Dim SNoCol As New DataGridViewTextBoxColumn
            With SNoCol
                .Name = "SNo"
                .HeaderText = "S.No"
                .Width = 60
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            DgvProSales.Columns.Add(SNoCol)

            For Each dr As DataRow In dtDesign.Rows
                Dim dgCol As New DataGridViewTextBoxColumn
                dgCol.Name = dr("BD_ColumnName_vc").ToString()
                dgCol.HeaderText = dr("BD_AliasName_vc").ToString()
                dgCol.Width = Val(dr("BD_ColWidth_vc"))

                Select Case dr("BD_ColAlign_vc").ToString().ToUpper()
                    Case "LEFT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Case "CENTER"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    Case "RIGHT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select
                DgvProSales.Columns.Add(dgCol)
            Next


            For Each drSale As DataRow In dtSale.Rows
                Dim Row As Integer = DgvProSales.Rows.Add()
                DgvProSales.Rows(Row).Cells("SNo").Value = Row + 1

                For i As Integer = 0 To dtDesign.Rows.Count - 1
                    Dim ColName As String = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                    Dim Dt As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()

                    If Not dtSale.Columns.Contains(ColName) Then
                        Continue For
                    End If

                    If drSale.IsNull(ColName) Then
                        DgvProSales.Rows(Row).Cells(i + 1).Value = ""
                    Else
                        Select Case Dt
                            Case "DATE"
                                DgvProSales.Rows(Row).Cells(i + 1).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy")
                            Case "QTY"
                                Dim Qty As Decimal = Val(drSale(ColName))
                                If Qty = Math.Truncate(Qty) Then
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N0")
                                Else
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N3")
                                End If
                            Case "DECIMAL", "NUMBER", "NUMERIC", "FINALTOTAL"
                                DgvProSales.Rows(Row).Cells(i + 1).Value = FormatNumber(Val(drSale(ColName)), 2)
                            Case Else
                                DgvProSales.Rows(Row).Cells(i + 1).Value = drSale(ColName)
                        End Select
                    End If

                    If Dt = "DECIMAL" OrElse Dt = "NUMBER" OrElse Dt = "NUMERIC" OrElse Dt = "FINALTOTAL" OrElse Dt = "QTY" Then
                        If Not ColTotals.ContainsKey(i) Then
                            ColTotals.Add(i, 0D)
                        End If
                        If Not drSale.IsNull(ColName) Then
                            ColTotals(i) += Convert.ToDecimal(drSale(ColName))
                        End If
                    End If
                Next
            Next

            Dim TotalRow As Integer = DgvProSales.Rows.Add()
            With DgvProSales.Rows(TotalRow)
                .DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .DefaultCellStyle.ForeColor = Color.Red
                .Cells("SNo").Value = "TOTAL"
            End With
            For Each kv In ColTotals
                DgvProSales.Rows(TotalRow).Cells(kv.Key + 1).Value = FormatNumber(kv.Value, 2)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub LodgeReportSummary()
        Try
            DgvProSales.Rows.Clear()
            DgvProSales.Columns.Clear()

            Dim dtDesign As New DataTable
            Dim SqlDgvTablechk As String = ""

            Select Case RptTypeChk
                Case "BOOKING REPORT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_Lod_DgvBookRpt WHERE BD_Show_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CHECKOUT REPORT"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_Lod_DgvChkRpt WHERE BD_Show_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "HOUSE KEEPING"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_Lod_DgvHKRpt WHERE BD_Show_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "TICK REMOVE"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_Lod_DgvTickRmRpt WHERE BD_Show_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CASH BILL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_Lod_DgvCashRpt WHERE BD_Show_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CARD BILL"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_Lod_DgvCardRpt WHERE BD_Show_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "OUTSTANDING"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_Lod_DgvOuts WHERE BD_Show_vc='YES' ORDER BY BD_ColOrderby_i"
                Case "CURR BOOKING"
                    SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_Datatype_vc,BD_ColAlign_vc,BD_ColOrderby_i FROM tbl_Lod_DgvCurrBook WHERE BD_Show_vc='YES' ORDER BY BD_ColOrderby_i"
            End Select

            Using daDesign As New SqlDataAdapter(SqlDgvTablechk, VistawinFunctions.ServerConnection)
                daDesign.Fill(dtDesign)
            End Using

            If dtDesign.Rows.Count = 0 Then
                MsgBox("Bill Design Not Found")
                Exit Sub
            End If

            Dim ColList As String = ""
            For Each dr As DataRow In dtDesign.Rows
                ColList &= dr("BD_SQLColumn_vc").ToString() & " AS [" & dr("BD_ColumnName_vc").ToString() & "],"
            Next
            ColList = ColList.TrimEnd(","c)

            Dim dtSale As New DataTable
            Dim Sql As String = ""

            Select Case RptTypeChk
                Case "BOOKING REPORT"
                    Sql = "SELECT " & ColList & " from tbl_BookingDetails bo left join tbl_Lod_CusProfile cu ON(bo.Book_CustID_n=cu.C_ID_n And C_Type_vc='CUSTOMER') WHERE CONVERT(DATE,Book_EntDate_d) BETWEEN @FromDate AND @ToDate AND Book_CustName_vc <>'Again'"
                Case "CHECKOUT REPORT"
                    Sql = "SELECT " & ColList & " FROM tbl_Lod_CheckOutDetail WHERE CONVERT(DATE,Chkt_EntDate_d) BETWEEN @FromDate AND @ToDate"
                Case "HOUSE KEEPING"
                    Sql = "SELECT " & ColList & " FROM tbl_BookingDetails a WHERE Book_RoomCheckOut_vc = 'YES' AND Book_HKComplete_vc = 'YES' AND CONVERT(DATE,Book_CheckOutDate_d) BETWEEN @FromDate AND @ToDate AND Book_CustName_vc <>'Again' ORDER BY Book_CheckOutDate_d"
                Case "TICK REMOVE"
                    Sql = "SELECT " & ColList & " FROM tbl_Lod_TickRemove WHERE CONVERT(DATE,Ent_Date_d) BETWEEN @FromDate AND @ToDate ORDER BY Ent_Date_d,Booking_ID_n"
                Case "CASH BILL"
                    Sql = "SELECT " & ColList & " from tbl_Lod_ReceiptDetail where Rec_BillMode_vc  ='Cash' AND  Rec_AccShow_vc='YES' and convert(date,Rec_BillDate_d) BETWEEN @FromDate AND @ToDate order by convert(date,Rec_BillDate_d)"
                Case "CARD BILL"
                    Sql = "SELECT " & ColList & " from tbl_Lod_ReceiptDetail where Rec_BillMode_vc  ='BANK' AND  Rec_AccShow_vc='YES' and convert(date,Rec_BillDate_d) BETWEEN @FromDate AND @ToDate order by convert(date,Rec_BillDate_d)"
            End Select

            Using cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@FromDate", CDate(txtFromDate.Text))
                cmd.Parameters.AddWithValue("@ToDate", CDate(txtToDate.Text))
                cmd.Parameters.AddWithValue("@CusID", txtCusID.Text)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtSale)
                End Using
            End Using

            If dtSale.Rows.Count = 0 Then
                MsgBox("No Data")
                Exit Sub
            End If

            Dim ColTotals As New Dictionary(Of Integer, Decimal)
            Dim SNoCol As New DataGridViewTextBoxColumn
            With SNoCol
                .Name = "SNo"
                .HeaderText = "S.No"
                .Width = 60
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            DgvProSales.Columns.Add(SNoCol)

            For Each dr As DataRow In dtDesign.Rows
                Dim dgCol As New DataGridViewTextBoxColumn
                dgCol.Name = dr("BD_ColumnName_vc").ToString()
                dgCol.HeaderText = dr("BD_AliasName_vc").ToString()
                dgCol.Width = Val(dr("BD_ColWidth_vc"))

                Select Case dr("BD_ColAlign_vc").ToString().ToUpper()
                    Case "LEFT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Case "CENTER"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    Case "RIGHT"
                        dgCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select
                DgvProSales.Columns.Add(dgCol)
            Next


            For Each drSale As DataRow In dtSale.Rows
                Dim Row As Integer = DgvProSales.Rows.Add()
                DgvProSales.Rows(Row).Cells("SNo").Value = Row + 1

                For i As Integer = 0 To dtDesign.Rows.Count - 1
                    Dim ColName As String = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                    Dim Dt As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()

                    If Not dtSale.Columns.Contains(ColName) Then
                        Continue For
                    End If

                    If drSale.IsNull(ColName) Then
                        DgvProSales.Rows(Row).Cells(i + 1).Value = ""
                    Else
                        Select Case Dt
                            Case "DATE"
                                DgvProSales.Rows(Row).Cells(i + 1).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy")
                            Case "DATETIME"
                                DgvProSales.Rows(Row).Cells(i + 1).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy hh:mm:ss tt")
                            Case "QTY"
                                Dim Qty As Decimal = Val(drSale(ColName))
                                If Qty = Math.Truncate(Qty) Then
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N0")
                                Else
                                    DgvProSales.Rows(Row).Cells(i + 1).Value = Qty.ToString("N3")
                                End If
                            Case "DECIMAL", "NUMBER", "NUMERIC", "FINALTOTAL"
                                DgvProSales.Rows(Row).Cells(i + 1).Value = FormatNumber(Val(drSale(ColName)), 2)
                            Case Else
                                DgvProSales.Rows(Row).Cells(i + 1).Value = drSale(ColName)
                        End Select
                    End If

                    If Dt = "DECIMAL" OrElse Dt = "NUMBER" OrElse Dt = "NUMERIC" OrElse Dt = "FINALTOTAL" OrElse Dt = "QTY" Then
                        If Not ColTotals.ContainsKey(i) Then
                            ColTotals.Add(i, 0D)
                        End If
                        If Not drSale.IsNull(ColName) Then
                            ColTotals(i) += Convert.ToDecimal(drSale(ColName))
                        End If
                    End If
                Next
            Next

            Dim TotalRow As Integer = DgvProSales.Rows.Add()
            With DgvProSales.Rows(TotalRow)
                .DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .DefaultCellStyle.ForeColor = Color.Red
                .Cells("SNo").Value = "TOTAL"
            End With
            For Each kv In ColTotals
                DgvProSales.Rows(TotalRow).Cells(kv.Key + 1).Value = FormatNumber(kv.Value, 2)
            Next


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
 
End Class