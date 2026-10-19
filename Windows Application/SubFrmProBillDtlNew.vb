Imports System.IO
Imports System.Data.SqlClient

Public Class SubFrmProBillDtlNew
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet

    Public LStNumber As String
    Public LProName As String
    Public LStockID As String
    Public InvRptType As String
    Public LCusID As String

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
    Private Sub SubFrmProBillDtl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                Me.InvenBillDetail()
            Case "CLINIC"
                Me.ClinicBillDetail()
        End Select

    End Sub

    Private Sub CbAllCust_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbAllCust.CheckedChanged
        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                Me.InvenBillDetail()
            Case "CLINIC"
                Me.ClinicBillDetail()
        End Select
    End Sub
    Private Sub DGVBill_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DgvBill.CellFormatting

        Select Case DgvBill.Columns(e.ColumnIndex).Name
            Case "Qty", "FQty", "TQty"
                If e.Value Is Nothing OrElse IsDBNull(e.Value) Then Exit Sub
                Dim Qty As Decimal

                If Decimal.TryParse(e.Value.ToString(), Qty) Then
                    If Qty = Decimal.Truncate(Qty) Then
                        e.Value = Qty.ToString("N0")    '10
                    Else
                        e.Value = Qty.ToString("N3")    '10.250
                    End If
                    e.FormattingApplied = True
                End If
        End Select

    End Sub
    Private Sub InvenBillDetail()

        DgvBill.Rows.Clear()
        Ds = New DataSet
        Dim DetailTable As String = ""
        Dim PartTable As String = ""
        Dim SaveType As String = ""
        Dim ExtraField As String = ""
        Dim CusWhere As String = ""

        If Not CbAllCust.Checked Then
            CusWhere = " AND sa.Sa_CusID_n='" & LCusID & "'"
        End If

        Select Case InvRptType

            Case "PURCHASE", "PURCHASE RETURN"

                DetailTable = "tbl_PurchDetail"
                PartTable = "tbl_PurchParticular"
                SaveType = "= '" & InvRptType & "'"
                ExtraField = "0 AS DumRate,0 AS SaleMar,0 AS SaleMarAmt"

            Case "INVOICE", "ESTIMATE"

                DetailTable = "tbl_SalDetail"
                PartTable = "tbl_SalParticular"
                SaveType = "IN('INVOICE','ESTIMATE')"
                ExtraField = "Sa_DumRate_n AS DumRate,Sa_SaleMar_n AS SaleMar,Sa_SaleMarAmt_n AS SaleMarAmt"

            Case "SALES RETURN"

                DetailTable = "tbl_SalDetail"
                PartTable = "tbl_SalParticular"
                SaveType = "= 'SALES RETURN'"
                ExtraField = "Sa_DumRate_n AS DumRate,Sa_SaleMar_n AS SaleMar,Sa_SaleMarAmt_n AS SaleMarAmt"
        End Select

        Dim Sql As String = ""
        Sql = "SELECT Sa_SaveType_vc AS SaveType,sa.Sa_BillDate_d AS BillDate,Sa_BType_vc AS BType,sa.Sa_InvNo_vc AS InvNo,sa.Sa_BillNo_n AS BillNo,sa.Sa_CusID_n AS CusID,sa.Sa_CusName_vc AS CusName,sp.Sa_ProTotal_n AS ProTotal,Sa_StCode_n AS StockCode,St_StNumber_i AS StNumber,St_HSN_vc AS HSNCode,Sa_PurRate_n AS PurRate,Sa_MRP_n AS MRPRate,Sa_SaleRate_n AS SaleRate,Sa_ProDPer_n AS PDPer,Sa_ProDAmt_n AS PDAmt,Sa_NetRate_n AS NetRate,Sa_Qty_i AS Qty,Sa_FQty_i AS FQty,Sa_TQty_i AS TQty,Sa_QType_vc AS QType,Sa_Taxable_n AS Taxable,Sa_CGSTP_n AS CGSTPer,Sa_SGSTP_n AS SGSTPer,Sa_IGSTP_n AS IGSTPer,Sa_CGSTAmt_n AS CGSTAmt,Sa_SGSTAmt_n AS SGSTAmt,Sa_IGSTAmt_n AS IGSTAmt,sp.Sa_SRTInv_vc AS SRTInv," & ExtraField & " FROM " & DetailTable & " sa LEFT JOIN " & PartTable & " sp ON sa.Sa_InvNo_vc=sp.Sa_InvNo_vc LEFT JOIN tbl_MultiStock m ON sp.Sa_StNumber_n=m.St_StNumber_i LEFT JOIN tbl_StockDtl s ON s.St_StockID_n=m.St_StockID_n WHERE Sa_SaveType_vc " & SaveType & " AND Sa_StCode_n='" & LStockID & "'" & CusWhere & " ORDER BY sp.Sa_RecordNo2_i"
        Adapter = New SqlDataAdapter(Sql, VistawinFunctions.ServerConnection)

        Adapter.Fill(Ds, "FillData")

        If Ds.Tables("FillData").Rows.Count = 0 Then Exit Sub

        Dim QtyTotal As Decimal = 0
        Dim FQtyTotal As Decimal = 0
        Dim TQtyTotal As Decimal = 0

        Dim Columns() As String = {
         "SaveType", "BillDate", "InvNo", "StockCode", "StNumber", "CusName", "PurRate", "MRPRate",
         "DumRate", "SaleMar", "SaleRate", "PDPer", "PDAmt", "NetRate", "Qty", "FQty", "TQty", "QType",
         "Taxable", "CGSTPer", "SGSTPer", "IGSTPer", "CGSTAmt", "SGSTAmt", "IGSTAmt", "ProTotal",
         "SRTInv", "HSNCode", "SaleMarAmt", "BType"
           }

        For Each dr As DataRow In Ds.Tables("FillData").Rows
            Dim r As Integer = DgvBill.Rows.Add()

            DgvBill.Rows(r).Cells("SNo").Value = r + 1
            DgvBill.Rows(r).Cells("BillDate").Value = CDate(dr("BillDate")).ToString("dd/MM/yyyy")

            For Each Col As String In Columns
                DgvBill.Rows(r).Cells(Col).Value = dr(Col)
            Next

            QtyTotal += Convert.ToDecimal(dr("Qty"))
            FQtyTotal += Convert.ToDecimal(dr("FQty"))
            TQtyTotal += Convert.ToDecimal(dr("TQty"))
        Next
        Dim TotalRow As Integer = DgvBill.Rows.Add()

        DgvBill.Rows(TotalRow).Cells("Qty").Value = QtyTotal
        DgvBill.Rows(TotalRow).Cells("FQty").Value = FQtyTotal
        DgvBill.Rows(TotalRow).Cells("TQty").Value = TQtyTotal

        For Each Col As String In {"Qty", "FQty", "TQty"}
            With DgvBill.Rows(TotalRow).Cells(Col)
                .Style.ForeColor = Color.Red
                .Style.Font = New Font("Times New Roman", 12, FontStyle.Bold)
            End With
        Next

    End Sub
    Private Sub ClinicBillDetail()

        DgvBill.Rows.Clear()
        Ds = New DataSet
        Dim DetailTable As String = ""
        Dim PartTable As String = ""
        Dim SaveType As String = ""
        Dim ExtraField As String = ""
        Dim CusWhere As String = ""

        If Not CbAllCust.Checked Then
            'CusWhere = " AND sa.Sa_PatID_n='" & LCusID & "'"
            CusWhere = " AND sa.Sa_CusID_n='" & LCusID & "'"
        End If

        Select Case InvRptType

            Case "PURCHASE", "PURCHASE RETURN"

                DetailTable = "tbl_PurchDetail"
                PartTable = "tbl_PurchParticular"
                SaveType = "= '" & InvRptType & "'"
                ExtraField = "0 AS DumRate,0 AS SaleMar,0 AS SaleMarAmt"

            Case "INVOICE"

                DetailTable = "tbl_BillDetail"
                PartTable = "tbl_BillParticular"
                SaveType = "IN('INVOICE','ESTIMATE')"
                ExtraField = "0 AS DumRate,0 AS SaleMar,0 AS SaleMarAmt"

            Case "SALES RETURN"

                DetailTable = "tbl_BillDetail"
                PartTable = "tbl_BillParticular"
                SaveType = "= 'SALES RETURN'"
                ExtraField = "0 AS DumRate,0 AS SaleMar,0 AS SaleMarAmt"
        End Select

        Dim Sql As String = ""

        Select Case InvRptType
            Case "PURCHASE", "PURCHASE RETURN"

                Sql = "SELECT Sa_SaveType_vc AS SaveType,sa.Sa_BillDate_d AS BillDate,Sa_BType_vc AS BType,sa.Sa_InvNo_vc AS InvNo,sa.Sa_BillNo_n AS BillNo,sa.Sa_CusID_n AS CusID,sa.Sa_CusName_vc AS CusName,sp.Sa_ProTotal_n AS ProTotal,Sa_StCode_n AS StockCode,St_StNumber_i AS StNumber,St_HSN_vc AS HSNCode,Sa_PurRate_n AS PurRate,Sa_MRP_n AS MRPRate,Sa_SaleRate_n AS SaleRate,Sa_ProDPer_n AS PDPer,Sa_ProDAmt_n AS PDAmt,Sa_NetRate_n AS NetRate,Sa_Qty_i AS Qty,Sa_FQty_i AS FQty,Sa_TQty_i AS TQty,Sa_QType_vc AS QType,Sa_Taxable_n AS Taxable,Sa_CGSTP_n AS CGSTPer,Sa_SGSTP_n AS SGSTPer,Sa_IGSTP_n AS IGSTPer,Sa_CGSTAmt_n AS CGSTAmt,Sa_SGSTAmt_n AS SGSTAmt,Sa_IGSTAmt_n AS IGSTAmt,sp.Sa_SRTInv_vc AS SRTInv," & ExtraField & " FROM " & DetailTable & " sa LEFT JOIN " & PartTable & " sp ON sa.Sa_InvNo_vc=sp.Sa_InvNo_vc LEFT JOIN tbl_MultiStock m ON sp.Sa_StNumber_n=m.St_StNumber_i LEFT JOIN tbl_Stock s ON s.St_StockID_n=m.St_StockID_n WHERE Sa_SaveType_vc " & SaveType & " AND Sa_StCode_n='" & LStockID & "'" & CusWhere & " ORDER BY sp.Sa_RecordNo2_i"
            Case "INVOICE", "SALES RETURN"

                Sql = "SELECT Sa_SaveType_vc AS SaveType,sa.Sa_BillDate_d AS BillDate,Sa_BType_vc AS BType,sa.Sa_InvNo_vc AS InvNo,sa.Sa_BillNo_n AS BillNo,sa.Sa_PatID_n AS CusID,sa.Sa_PatName_vc AS CusName,sp.Sa_ProTotal_n AS ProTotal,Sa_StCode_n AS StockCode,St_StNumber_i AS StNumber,St_HSN_vc AS HSNCode,Sa_PurRate_n AS PurRate,Sa_MRP_n AS MRPRate,Sa_SaleRate_n AS SaleRate,Sa_ProDPer_n AS PDPer,Sa_ProDAmt_n AS PDAmt,Sa_NetRate_n AS NetRate,Sa_Qty_i AS Qty,Sa_FQty_i AS FQty,Sa_TQty_i AS TQty,Sa_QType_vc AS QType,Sa_Taxable_n AS Taxable,Sa_CGSTP_n AS CGSTPer,Sa_SGSTP_n AS SGSTPer,Sa_IGSTP_n AS IGSTPer,Sa_CGSTAmt_n AS CGSTAmt,Sa_SGSTAmt_n AS SGSTAmt,Sa_IGSTAmt_n AS IGSTAmt,sp.Sa_SRTInv_vc AS SRTInv," & ExtraField & " FROM " & DetailTable & " sa LEFT JOIN " & PartTable & " sp ON sa.Sa_InvNo_vc=sp.Sa_InvNo_vc LEFT JOIN tbl_MultiStock m ON sp.Sa_StNumber_n=m.St_StNumber_i LEFT JOIN tbl_Stock s ON s.St_StockID_n=m.St_StockID_n WHERE Sa_SaveType_vc " & SaveType & " AND Sa_StCode_n='" & LStockID & "'" & CusWhere & " ORDER BY sp.Sa_RecordNo2_i"
        End Select

        Adapter = New SqlDataAdapter(Sql, VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        If Ds.Tables("FillData").Rows.Count = 0 Then Exit Sub

        Dim QtyTotal As Decimal = 0
        Dim FQtyTotal As Decimal = 0
        Dim TQtyTotal As Decimal = 0

        Dim Columns() As String = {
         "SaveType", "BillDate", "InvNo", "StockCode", "StNumber", "CusName", "PurRate", "MRPRate",
         "DumRate", "SaleMar", "SaleRate", "PDPer", "PDAmt", "NetRate", "Qty", "FQty", "TQty", "QType",
         "Taxable", "CGSTPer", "SGSTPer", "IGSTPer", "CGSTAmt", "SGSTAmt", "IGSTAmt", "ProTotal",
         "SRTInv", "HSNCode", "SaleMarAmt", "BType"
           }

        For Each dr As DataRow In Ds.Tables("FillData").Rows
            Dim r As Integer = DgvBill.Rows.Add()

            DgvBill.Rows(r).Cells("SNo").Value = r + 1
            DgvBill.Rows(r).Cells("BillDate").Value = CDate(dr("BillDate")).ToString("dd/MM/yyyy")

            For Each Col As String In Columns
                DgvBill.Rows(r).Cells(Col).Value = dr(Col)
            Next

            QtyTotal += Convert.ToDecimal(dr("Qty"))
            FQtyTotal += Convert.ToDecimal(dr("FQty"))
            TQtyTotal += Convert.ToDecimal(dr("TQty"))
        Next
        Dim TotalRow As Integer = DgvBill.Rows.Add()

        DgvBill.Rows(TotalRow).Cells("Qty").Value = QtyTotal
        DgvBill.Rows(TotalRow).Cells("FQty").Value = FQtyTotal
        DgvBill.Rows(TotalRow).Cells("TQty").Value = TQtyTotal

        For Each Col As String In {"Qty", "FQty", "TQty"}
            With DgvBill.Rows(TotalRow).Cells(Col)
                .Style.ForeColor = Color.Red
                .Style.Font = New Font("Times New Roman", 12, FontStyle.Bold)
            End With
        Next

    End Sub

End Class