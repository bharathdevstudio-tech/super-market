Imports System.IO
Imports System.Data.SqlClient

Public Class SubRptStockReport
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat, AddOrEdit As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet
    Dim OrderChk As String = ""
    Dim DelProList As Boolean = False

    Dim SelectedStNumber As String = ""
    Dim SelectedStockID As String = ""
    Dim SelectedProName As String = ""
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                GrpMore.Hide()
                Return True
                Exit Function
            ElseIf msg.WParam.ToInt32 = CInt(Keys.F12) Then
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub SubRptStockReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GrpChange.Hide()
        GrpChange.Location = New System.Drawing.Point(353, 168)
        GrpChange.Size = New System.Drawing.Size(700, 400)
        GrpChange.BringToFront()

        DgvOldValue.Hide()
        DgvOldValue.Size = New System.Drawing.Size(600, 350)
        DgvOldValue.BringToFront()

        GrpLeft.Hide()
        BtnShowHide.Text = If(GrpLeft.Visible, "<<", ">>")

        GrpMore.Hide()

        txtProductName1A.Focus()

        Select Case VistawinFunctions.CmpDatabaseName
            Case "KIRUTHICKCLINICTCRDB", "SLMARKDB", "BHAVANIELECSURDB"
                LblSendtoServer.Show()
            Case Else
                LblSendtoServer.Hide()
        End Select

        LoadReportCombo()

    End Sub
    Private Sub LoadReportCombo()

        CbRptType.Items.Clear()
        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                CbRptType.Items.Add("Category")
                CbRptType.Items.Add("Brand")
                CbRptType.Items.Add("MRP")
                CbRptType.Items.Add("HSNCode")
                CbRptType.Items.Add("Purchase Rate")
                CbRptType.Items.Add("Sale Rate")
                CbRptType.Items.Add("Wholesale Rate")
                CbRptType.Items.Add("Stock Qty")
                CbRptType.Items.Add("Unit")
                CbRptType.Items.Add("CGST")
                CbRptType.Items.Add("SGST")
                CbRptType.Items.Add("Shelf")
                CbRptType.Items.Add("Row")
                CbRptType.Items.Add("Reminder Stock")
                CbRptType.Items.Add("Count>1 Product")
                CbRptType.Items.Add("Disc Per")
                CbRptType.Items.Add("Warranty")
            Case "CLINIC"
                CbRptType.Items.Add("Category")
                CbRptType.Items.Add("Brand")
                'CbRptType.Items.Add("Medicine")
                CbRptType.Items.Add("Pharma")
                CbRptType.Items.Add("Expiry Report")
                CbRptType.Items.Add("Expired")
                CbRptType.Items.Add("BatchNo")
                CbRptType.Items.Add("MRP")
                CbRptType.Items.Add("HSNCode")
                CbRptType.Items.Add("Purchase Rate")
                CbRptType.Items.Add("Sale Rate")
                CbRptType.Items.Add("Stock Qty")
                CbRptType.Items.Add("Pack")
                CbRptType.Items.Add("CGST")
                CbRptType.Items.Add("SGST")
                CbRptType.Items.Add("Shelf")
                CbRptType.Items.Add("Row")
                CbRptType.Items.Add("Reminder Stock")
                CbRptType.Items.Add("Count>1 Product")
                CbRptType.Items.Add("Disc Per")
        End Select

        If CbRptType.Items.Count > 0 Then
            CbRptType.SelectedIndex = 0
        End If

    End Sub
    Private Sub CbRptType_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CbRptType.TextChanged

        txtMasterSearch.Clear()

        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                InvenLoadMasterList("")
            Case "CLINIC"
                ClinicLoadMasterList("")
        End Select

    End Sub

    Private Sub txtMasterSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtMasterSearch.TextChanged
        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                InvenLoadMasterList(Trim(txtMasterSearch.Text))
            Case "CLINIC"
                ClinicLoadMasterList(Trim(txtMasterSearch.Text))
        End Select

    End Sub
    Private Sub MasterList()
        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                InvenLoadMasterList("")
            Case "CLINIC"
                ClinicLoadMasterList("")
        End Select

    End Sub
    Private Sub InvenLoadMasterList(ByVal SearchText As String)
        Try
            Dim Sql As String = ""
            Dim SearchField As String = ""
            Dim HasWhere As Boolean = False
            Select Case Trim(CbRptType.Text)
                Case "Category"
                    Sql = "SELECT DISTINCT Category_Code_n AS EmpIDM, Category_Name_vc AS EmpNameM, '' AS ParticularsM FROM tbl_Category"
                    SearchField = "Category_Name_vc"
                Case "Brand"
                    Sql = "SELECT DISTINCT Brand_Code_n AS EmpIDM, Brand_Name_vc AS EmpNameM, '' AS ParticularsM FROM tbl_Brand"
                    SearchField = "Brand_Name_vc"
                Case "MRP"
                    Sql = "SELECT DISTINCT ISNULL(St_MRPRate_n,0) AS EmpIDM, ISNULL(St_MRPRate_n,0) AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_MRPRate_n,0))"
                Case "Purchase Rate"
                    Sql = "SELECT DISTINCT ISNULL(St_PurRate_n,0) AS EmpIDM, ISNULL(St_PurRate_n,0) AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_PurRate_n,0))"
                Case "Sale Rate"
                    Sql = "SELECT DISTINCT ISNULL(St_SaleRate_n,0) AS EmpIDM, ISNULL(St_SaleRate_n,0) AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_SaleRate_n,0))"
                Case "Wholesale Rate"
                    Sql = "SELECT DISTINCT ISNULL(St_SaleRate2_n,0) AS EmpIDM, ISNULL(St_SaleRate2_n,0) AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_SaleRate2_n,0))"
                Case "Stock Qty"
                    Sql = "SELECT DISTINCT ISNULL(St_TotalSt_i,0) AS EmpIDM, ISNULL(St_TotalSt_i,0) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_TotalSt_i,0))"
                Case "HSNCode"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, LTRIM(RTRIM(ISNULL(St_HSN_vc,''))) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl WHERE LTRIM(RTRIM(ISNULL(St_HSN_vc,'')))<>''"
                    SearchField = "St_HSN_vc"
                    HasWhere = True
                Case "Unit"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, LTRIM(RTRIM(ISNULL(St_Unit_vc,''))) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl WHERE LTRIM(RTRIM(ISNULL(St_Unit_vc,'')))<>''"
                    SearchField = "St_Unit_vc"
                    HasWhere = True
                Case "CGST"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM,CONVERT(VARCHAR(50), St_CGSTPer_n) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl WHERE St_CGSTPer_n IS NOT NULL"
                    SearchField = "St_CGSTPer_n"
                    HasWhere = True
                Case "SGST"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM,CONVERT(VARCHAR(50), St_SGSTPer_n) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl WHERE St_SGSTPer_n IS NOT NULL"
                    SearchField = "St_SGSTPer_n"
                    HasWhere = True
                Case "Disc Per"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM,CONVERT(VARCHAR(50), St_DisPer_n) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl WHERE St_DisPer_n IS NOT NULL"
                    SearchField = "St_DisPer_n"
                    HasWhere = True
                Case "Shelf"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, LTRIM(RTRIM(ISNULL(St_Shelf_vc,''))) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl WHERE LTRIM(RTRIM(ISNULL(St_Shelf_vc,'')))<>''"
                    SearchField = "St_Shelf_vc"
                    HasWhere = True
                Case "Row"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, LTRIM(RTRIM(ISNULL(St_Row_vc,''))) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl WHERE LTRIM(RTRIM(ISNULL(St_Row_vc,'')))<>''"
                    SearchField = "St_Row_vc"
                    HasWhere = True
                Case "Reminder Stock"
                    Sql = "SELECT DISTINCT ISNULL(St_RemiStock_i,0) AS EmpIDM, ISNULL(St_RemiStock_i,0) AS EmpNameM, '' AS ParticularsM FROM tbl_StockDtl"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_RemiStock_i,0))"
                Case "Count>1 Product"
                    Sql = "SELECT St_StockID_n AS EmpIDM, COUNT(*) AS EmpNameM, St_StockID_n AS ParticularsM FROM tbl_MultiStock"
                    If Trim(SearchText) <> "" Then
                        Sql &= " WHERE CONVERT(NVARCHAR(50),St_StockID_n) LIKE @Search "
                    End If
                    Sql &= " GROUP BY St_StockID_n HAVING COUNT(*) > 1 ORDER BY COUNT(*) DESC"
                    FillMasterGrid(Sql, SearchText)
                    Exit Sub
                Case Else
                    DgvMasterList.Rows.Clear()
                    Exit Sub
            End Select
            If Trim(SearchText) <> "" Then
                If HasWhere Then
                    Sql &= " AND " & SearchField & " LIKE @Search "
                Else
                    Sql &= " WHERE " & SearchField & " LIKE @Search "
                End If
            End If
            Sql &= " ORDER BY EmpNameM"
            FillMasterGrid(Sql, SearchText)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Master List", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ClinicLoadMasterList(ByVal SearchText As String)
        Try
            Dim Sql As String = ""
            Dim SearchField As String = ""
            Dim HasWhere As Boolean = False
            Select Case Trim(CbRptType.Text)
                Case "Supplier"
                    Sql = "SELECT C_ID_n AS EmpIDM, C_Name_vc AS EmpNameM, '' AS ParticularsM FROM tbl_CusProfile WHERE C_Type_vc='SUPPLIER'"
                    SearchField = "C_Name_vc"
                    HasWhere = True
                Case "Category"
                    Sql = "SELECT DISTINCT Category_Code_n AS EmpIDM,Category_Name_vc AS EmpNameM, '' AS ParticularsM FROM tbl_Category"
                    SearchField = "Category_Name_vc"
                Case "Pharma"
                    Sql = "SELECT DISTINCT Pharma_Code_n AS EmpIDM, Pharma_Name_vc AS EmpNameM, '' AS ParticularsM FROM tbl_Pharma"
                    SearchField = "Pharma_Name_vc"
                Case "Expiry Report"
                    Sql = "SELECT DISTINCT St_EMonth_i AS EmpIDM, St_EYear_vc AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "ISNULL(St_EYear_vc,'')"
                Case "Expired"
                    Sql = "SELECT DISTINCT St_EMonth_i AS EmpIDM, St_EYear_vc AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock WHERE St_EYear_vc < YEAR(GETDATE()) OR (St_EYear_vc = YEAR(GETDATE()) AND St_EMonth_i < MONTH(GETDATE()))"
                    SearchField = "CONVERT(NVARCHAR(50),St_EYear_vc)"
                    HasWhere = True
                Case "Medicine"
                    Sql = "SELECT St_StockID_n AS EmpIDM,St_ProName_vc AS EmpNameM, '' AS ParticularsM FROM tbl_Stock"
                    SearchField = "St_ProName_vc"
                Case "BatchNo"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, St_BatchNo_vc AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "St_BatchNo_vc"
                Case "MRP"
                    Sql = "SELECT DISTINCT ISNULL(St_MRPRate_n,0) AS EmpIDM, ISNULL(St_MRPRate_n,0) AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_MRPRate_n,0))"
                Case "HSNCode"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, LTRIM(RTRIM(ISNULL(St_HSN_vc,''))) AS EmpNameM, '' AS ParticularsM FROM tbl_Stock WHERE LTRIM(RTRIM(ISNULL(St_HSN_vc,'')))<>''"
                    SearchField = "St_HSN_vc"
                Case "Purchase Rate"
                    Sql = "SELECT DISTINCT ISNULL(St_PurRate_n,0) AS EmpIDM, ISNULL(St_PurRate_n,0) AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_PurRate_n,0))"
                Case "Sale Rate"
                    Sql = "SELECT DISTINCT ISNULL(St_SaleRate_n,0) AS EmpIDM, ISNULL(St_SaleRate_n,0) AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_SaleRate_n,0))"
                Case "MFR"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, St_MFR_vc AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "St_MFR_vc"
                Case "Stock Qty"
                    Sql = "SELECT DISTINCT ISNULL(St_TotalSt_i,0) AS EmpIDM, ISNULL(St_TotalSt_i,0) AS EmpNameM, '' AS ParticularsM FROM tbl_Stock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_TotalSt_i,0))"
                Case "Pack"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, St_Packint_i AS EmpNameM, '' AS ParticularsM FROM tbl_MultiStock"
                    SearchField = "St_Unit_vc"
                Case "CGST"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM,CONVERT(VARCHAR(50), St_CGSTPer_n) AS EmpNameM, '' AS ParticularsM FROM tbl_Stock WHERE St_CGSTPer_n IS NOT NULL"
                    SearchField = "St_CGSTPer_n"
                    HasWhere = True
                Case "SGST"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM,CONVERT(VARCHAR(50), St_SGSTPer_n) AS EmpNameM, '' AS ParticularsM FROM tbl_Stock WHERE St_SGSTPer_n IS NOT NULL"
                    SearchField = "St_SGSTPer_n"
                    HasWhere = True
                Case "Shelf"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, LTRIM(RTRIM(ISNULL(St_Shelf_vc,''))) AS EmpNameM, '' AS ParticularsM FROM tbl_Stock WHERE LTRIM(RTRIM(ISNULL(St_Shelf_vc,'')))<>''"
                    SearchField = "St_Shelf_vc"
                    HasWhere = True
                Case "Row"
                    Sql = "SELECT DISTINCT 1 AS EmpIDM, LTRIM(RTRIM(ISNULL(St_Row_vc,''))) AS EmpNameM, '' AS ParticularsM FROM tbl_Stock WHERE LTRIM(RTRIM(ISNULL(St_Row_vc,'')))<>''"
                    SearchField = "St_Row_vc"
                    HasWhere = True
                Case "Reminder Stock"
                    Sql = "SELECT DISTINCT ISNULL(St_RemiStock_i,0) AS EmpIDM, ISNULL(St_RemiStock_i,0) AS EmpNameM, '' AS ParticularsM FROM tbl_Stock"
                    SearchField = "CONVERT(NVARCHAR(50),ISNULL(St_RemiStock_i,0))"
                Case "Count>1 Product"
                    Sql = "SELECT St_StockID_n AS EmpIDM, COUNT(*) AS EmpNameM, St_StockID_n AS ParticularsM FROM tbl_MultiStock"
                    If Trim(SearchText) <> "" Then
                        Sql &= " WHERE CONVERT(NVARCHAR(50),St_StockID_n) LIKE @Search "
                    End If
                    Sql &= " GROUP BY St_StockID_n HAVING COUNT(*) > 1 ORDER BY COUNT(*) DESC"
                    FillMasterGrid(Sql, SearchText)
                    Exit Sub
                Case Else
                    DgvMasterList.Rows.Clear()
                    Exit Sub
            End Select
            If Trim(SearchText) <> "" Then
                If HasWhere Then
                    Sql &= " AND " & _
                           SearchField & _
                           " LIKE @Search "
                Else
                    Sql &= " WHERE " & _
                           SearchField & _
                           " LIKE @Search "
                End If
            End If
            Sql &= " ORDER BY EmpNameM"
            FillMasterGrid(Sql, SearchText)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Master List", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillMasterGrid(ByVal Sql As String, ByVal SearchText As String)

        Dim Dt As New DataTable
        Using Cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
            If SearchText <> "" Then
                Cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200)
                Cmd.Parameters("@Search").Value = SearchText & "%"
            End If
            Using Da As New SqlDataAdapter(Cmd)
                Da.Fill(Dt)
            End Using
        End Using
        DgvMasterList.Rows.Clear()
        Dim RowNo As Integer = 1
        For Each Dr As DataRow In Dt.Rows
            Dim Index As Integer = DgvMasterList.Rows.Add()
            With DgvMasterList.Rows(Index)
                .Cells("SNoM").Value = RowNo
                .Cells("EmpIDM").Value = Dr("EmpIDM")
                .Cells("EmpNameM").Value = Dr("EmpNameM")
                .Cells("ParticularsM").Value = Dr("ParticularsM")
                If SearchText = "" Then
                    .Cells("TickM").Value = "YES"
                Else
                    .Cells("TickM").Value = "NO"
                End If
            End With
            RowNo += 1
        Next
        If SearchText = "" Then
            CBSelectM.Checked = True
        Else
            CBSelectM.Checked = False
        End If
        If DgvMasterList.Rows.Count > 0 Then
            If Not DgvMasterList.Rows(0).IsNewRow Then
                DgvMasterList.ClearSelection()
                If DgvMasterList.Columns.Contains("EmpNameM") Then
                    DgvMasterList.CurrentCell = DgvMasterList.Rows(0).Cells("EmpNameM")
                End If
            End If
        End If
    End Sub
    Private Sub BtnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChange.Click
        GrpChange.Show()
        GrpChange.BringToFront()
        txtOldValue.Focus()
    End Sub
    Private Sub BtnBulkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBulkSave.Click

        If txtNewValue.Text = String.Empty Then
            MsgBox("Enter New Value")
            txtNewValue.Focus()
            Exit Sub
            'ElseIf txtOldValue.Text = String.Empty Then
            '    MsgBox("Enter Old Value")
            '    txtOldValue.Focus()
            '    Exit Sub
        ElseIf CbChangeType.Text = String.Empty Then
            MsgBox("Select Type")
            CbChangeType.Focus()
            Exit Sub
        End If
        Me.BulkSave()
    End Sub
    Private Sub BtnChangeClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChangeClose.Click
        GrpChange.Hide()
    End Sub
    Private Sub BtnClose2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose2.Click
        Me.Close()
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                Me.InvenStDelete()
            Case "CLINIC"
                ClinicStDelete()
        End Select
        Me.InvenStDelete()
    End Sub
    Private Sub InvenStDelete()
        Try
            Dim DeleteCount As Integer = 0

            Dim dtDesign As New DataTable
            Dim SqlDgvTablechk As String = ""

            SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_ColAlign_vc,BD_Datatype_vc,BD_ColOrderby_i FROM tbl_DgvSubRpt_Stock WHERE BD_StockDtl_vc='YES' ORDER BY BD_ColOrderby_i"
            Using daDesign As New SqlDataAdapter(SqlDgvTablechk, VistawinFunctions.ServerConnection)
                daDesign.Fill(dtDesign)
            End Using
            If dtDesign.Rows.Count = 0 Then
                MsgBox("Bill Design Not Found")
                Exit Sub
            End If
            Dim StockIDColumn As String = ""
            For Each dr As DataRow In dtDesign.Rows
                If dr("BD_SQLColumn_vc").ToString().Contains("St_StockID_n") Then
                    StockIDColumn = dr("BD_ColumnName_vc").ToString()
                    Exit For
                End If
            Next
            If StockIDColumn = "" Then
                MsgBox("Stock ID column not found.")
                Exit Sub
            End If
            For Each Dr As DataGridViewRow In DgvStockReport.Rows
                Select Case VistawinFunctions.ProjectTypechk
                    Case "INVEN"
                        If Dr.IsNewRow Then Continue For
                        If Dr.Cells("SNo").Value.ToString() = "TOTAL" Then Continue For
                        If Dr.Cells("Tick").Value IsNot Nothing AndAlso CBool(Dr.Cells("Tick").Value) Then
                            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_PurchParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                            Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                            If CInt(Cmd.ExecuteScalar()) > 0 Then
                                MsgBox("Cannot be Deleted!")
                                Exit Sub
                            End If
                            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_SalParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                            Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                            If CInt(Cmd.ExecuteScalar()) > 0 Then
                                MsgBox("Cannot be Deleted!")
                                Exit Sub
                            End If
                            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_DamageParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                            Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                            If CInt(Cmd.ExecuteScalar()) > 0 Then
                                MsgBox("Cannot be Deleted!")
                                Exit Sub
                            End If
                            DeleteCount += 1
                        End If
                    Case "CLINIC"
                        If Dr.IsNewRow Then Continue For
                        If Dr.Cells("SNo").Value.ToString() = "TOTAL" Then Continue For
                        If Dr.Cells("Tick").Value IsNot Nothing AndAlso CBool(Dr.Cells("Tick").Value) Then
                            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_PurchParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                            Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                            If CInt(Cmd.ExecuteScalar()) > 0 Then
                                MsgBox("Cannot be Deleted!")
                                Exit Sub
                            End If
                            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_BillParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                            Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                            If CInt(Cmd.ExecuteScalar()) > 0 Then
                                MsgBox("Cannot be Deleted!")
                                Exit Sub
                            End If
                            'Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_DamageParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                            'Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                            'If CInt(Cmd.ExecuteScalar()) > 0 Then
                            '    MsgBox("Cannot be Deleted!")
                            '    Exit Sub
                            'End If
                            DeleteCount += 1
                        End If
                End Select
            Next

            If DeleteCount = 0 Then
                MsgBox("Select Any One")
                Exit Sub
            End If
            If MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
            If BVistawinSolution1.Permission() = False Then Exit Sub
            Trans = VistawinFunctions.ServerConnection.BeginTransaction()
            For Each Dr As DataGridViewRow In DgvStockReport.Rows
                If Dr.IsNewRow Then Continue For
                If Dr.Cells("SNo").Value.ToString() = "TOTAL" Then Continue For

                If Dr.Cells("Tick").Value IsNot Nothing AndAlso CBool(Dr.Cells("Tick").Value) Then
                    Using Cmd As New SqlCommand("SP_tbl_StockDelete", VistawinFunctions.ServerConnection, Trans)
                        Cmd.CommandType = CommandType.StoredProcedure
                        Cmd.Parameters.AddWithValue("@St_StockID_n", Dr.Cells("St_StockID_n").Value)
                        Cmd.Parameters.AddWithValue("@InputString", "Delete")
                        Cmd.ExecuteNonQuery()
                    End Using
                End If
            Next
            Trans.Commit()
            MsgBox(DeleteCount & " Record(s) Deleted Successfully")
            InvenProReport()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub
    Private Sub ClinicStDelete()
        Try
            Dim DeleteCount As Integer = 0

            Dim dtDesign As New DataTable
            Dim SqlDgvTablechk As String = ""

            SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_ColAlign_vc,BD_Datatype_vc,BD_ColOrderby_i FROM tbl_DgvSubRpt_Stock WHERE BD_StockDtl_vc='YES' ORDER BY BD_ColOrderby_i"
            Using daDesign As New SqlDataAdapter(SqlDgvTablechk, VistawinFunctions.ServerConnection)
                daDesign.Fill(dtDesign)
            End Using
            If dtDesign.Rows.Count = 0 Then
                MsgBox("Bill Design Not Found")
                Exit Sub
            End If
            Dim StockIDColumn As String = ""
            For Each dr As DataRow In dtDesign.Rows
                If dr("BD_SQLColumn_vc").ToString().Contains("St_StockID_n") Then
                    StockIDColumn = dr("BD_ColumnName_vc").ToString()
                    Exit For
                End If
            Next
            If StockIDColumn = "" Then
                MsgBox("Stock ID column not found.")
                Exit Sub
            End If
            For Each Dr As DataGridViewRow In DgvStockReport.Rows
                If Dr.IsNewRow Then Continue For
                If Dr.Cells("SNo").Value.ToString() = "TOTAL" Then Continue For
                If Dr.Cells("Tick").Value IsNot Nothing AndAlso CBool(Dr.Cells("Tick").Value) Then
                    Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_PurchParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                    Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                    If CInt(Cmd.ExecuteScalar()) > 0 Then
                        MsgBox("Cannot be Deleted!")
                        Exit Sub
                    End If
                    Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_BillParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                    Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                    If CInt(Cmd.ExecuteScalar()) > 0 Then
                        MsgBox("Cannot be Deleted!")
                        Exit Sub
                    End If
                    'Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_DamageParticular WHERE Sa_StCode_n=@Code", VistawinFunctions.ServerConnection)
                    'Cmd.Parameters.AddWithValue("@Code", Dr.Cells(StockIDColumn).Value)
                    'If CInt(Cmd.ExecuteScalar()) > 0 Then
                    '    MsgBox("Cannot be Deleted!")
                    '    Exit Sub
                    'End If
                    DeleteCount += 1
                End If
            Next
            If DeleteCount = 0 Then
                MsgBox("Select Any One")
                Exit Sub
            End If
            If MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
            If BVistawinSolution1.Permission() = False Then Exit Sub
            Trans = VistawinFunctions.ServerConnection.BeginTransaction()
            For Each Dr As DataGridViewRow In DgvStockReport.Rows
                If Dr.IsNewRow Then Continue For
                If Dr.Cells("SNo").Value.ToString() = "TOTAL" Then Continue For

                If Dr.Cells("Tick").Value IsNot Nothing AndAlso CBool(Dr.Cells("Tick").Value) Then
                    Using Cmd As New SqlCommand("SP_tbl_StockDelete", VistawinFunctions.ServerConnection, Trans)
                        Cmd.CommandType = CommandType.StoredProcedure
                        Cmd.Parameters.AddWithValue("@St_StockID_n", Dr.Cells("St_StockID_n").Value)
                        Cmd.Parameters.AddWithValue("@InputString", "Delete")
                        Cmd.ExecuteNonQuery()
                    End Using
                End If
            Next
            Trans.Commit()
            MsgBox(DeleteCount & " Record(s) Deleted Successfully")
            InvenProReport()
        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MessageBox.Show(ex.Message)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try
    End Sub
    Private Sub DgvStockReport_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DgvStockReport.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SelectCurrentRow()
        End If
    End Sub
    Private Sub DgvStockReport_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvStockReport.CellDoubleClick
        If e.RowIndex >= 0 Then
            SelectCurrentRow()
        End If
    End Sub

    Private Sub BtnPdfReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPdfReport.Click

        MessageBox.Show("PDF report module will be enabled with the SuperMarket Reports.",
                        "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
    Private Sub BtnClosingSt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClosingSt.Click
        Dim frm As New RptStockDateWise
        frm.ShowDialog()
    End Sub
    Private Sub DgvMasterList_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvMasterList.CurrentCellDirtyStateChanged
        If DgvMasterList.IsCurrentCellDirty Then
            DgvMasterList.CommitEdit( _
                DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub BtnDeletablePro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeletablePro.Click
        DelProList = True
        Me.InvenProReport()
    End Sub
    Private Sub BtnPreview2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview2.Click
        If CbRptType.Text = String.Empty Then
            MsgBox("Select Anyone")
            CbRptType.Focus()
            Exit Sub
        End If
        txtProductName1A.Text = String.Empty
        txtProductName1A.Focus()
        Me.InvenProReport()
    End Sub
    Private Sub InvenProReport(Optional ByVal SearchText As String = "")
        Try
            DgvStockReport.Rows.Clear()
            DgvStockReport.Columns.Clear()
            Dim dtDesign As New DataTable
            Dim SqlDgvTablechk As String = ""
            SqlDgvTablechk = "SELECT BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc,BD_SQLColumn_vc,BD_ColAlign_vc,BD_Datatype_vc,BD_ColOrderby_i FROM tbl_DgvSubRpt_Stock WHERE BD_StockDtl_vc='YES' ORDER BY BD_ColOrderby_i"
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
            Dim MastID As String = ""
            Dim MastName As String = ""
            Dim ParticulName As String = ""
            For Each Dr As DataGridViewRow In DgvMasterList.Rows
                If Dr.IsNewRow Then Continue For
                Dim IsSelected As Boolean = False
                If Dr.Cells("TickM").Value IsNot Nothing Then
                    Dim TickValue As String = _
                        Trim(Convert.ToString(Dr.Cells("TickM").Value)).ToUpper()
                    If TickValue = "YES" OrElse _
                       TickValue = "TRUE" OrElse _
                       TickValue = "1" Then
                        IsSelected = True
                    End If
                End If
                If IsSelected Then
                    If CbRptType.Text = "Expiry Report" OrElse _
                       CbRptType.Text = "Expired" Then
                        MastName &= "'" & _
                                    Dr.Cells("EmpIDM").Value.ToString() & "/" & _
                                    Dr.Cells("EmpNameM").Value.ToString() & "',"
                    Else
                        MastID &= Dr.Cells("EmpIDM").Value.ToString() & ","
                        MastName &= "'" & Dr.Cells("EmpNameM").Value.ToString().Replace("'", "''") & "',"
                        ParticulName &= "'" & Dr.Cells("ParticularsM").Value.ToString().Replace("'", "''") & "',"
                    End If
                End If
            Next
            Dim WhereCond As String = ""
            Dim FilterCond As String = ""
            If CBSelectM.Checked = False Then
                If CbRptType.Text = "Category" Then
                    If MastID <> "" Then
                        FilterCond = "s.St_CatID_i IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Brand" Then
                    If MastID <> "" Then
                        FilterCond = "s.St_BrandID_i IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                    'ElseIf CbRptType.Text = "Product" Then

                    '    If MastID <> "" Then
                    '        FilterCond = "s.St_StockID_n IN (" & MastID.TrimEnd(","c) & ")"
                    '    End If
                ElseIf CbRptType.Text = "MRP" Then
                    If MastID <> "" Then
                        FilterCond = "ms.St_MRPRate_n IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Purchase Rate" Then
                    If MastID <> "" Then
                        FilterCond = "ms.St_PurRate_n IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Sale Rate" Then
                    If MastID <> "" Then
                        FilterCond = "ms.St_SaleRate_n IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Wholesale Rate" Then
                    If MastID <> "" Then
                        FilterCond = "ms.St_SaleRate2_n IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "HSNCode" Then
                    If MastName <> "" Then
                        FilterCond = "s.St_HSN_vc IN (" & MastName.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Stock Qty" Then
                    If MastID <> "" Then
                        FilterCond = "s.St_TotalSt_i IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Unit" Then
                    If MastName <> "" Then
                        FilterCond = "s.St_Unit_vc IN (" & MastName.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Shelf" Then
                    If MastName <> "" Then
                        FilterCond = "s.St_Shelf_vc IN (" & MastName.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Row" Then
                    If MastName <> "" Then
                        FilterCond = "s.St_Row_vc IN (" & MastName.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Reminder Stock" Then
                    If MastID <> "" Then
                        FilterCond = "s.St_RemiStock_i IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Count>1 Product" Then
                    If MastID <> "" Then
                        FilterCond = "s.St_StockID_n IN (" & MastID.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "CGST" Then
                    If MastName <> "" Then
                        FilterCond = "LTRIM(RTRIM(ISNULL(s.St_CGSTPer_n,''))) IN (" & MastName.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "SGST" Then
                    If MastName <> "" Then
                        FilterCond = "LTRIM(RTRIM(ISNULL(s.St_SGSTPer_n,''))) IN (" & MastName.TrimEnd(","c) & ")"
                    End If
                ElseIf CbRptType.Text = "Disc Per" Then
                    If MastName <> "" Then
                        FilterCond = "LTRIM(RTRIM(ISNULL(s.St_DisPer_n,''))) IN (" & MastName.TrimEnd(","c) & ")"
                    End If
                End If
            End If
            If FilterCond <> "" Then
                WhereCond = " WHERE " & FilterCond
            End If
            If SearchText.Trim <> "" Then
                Dim SearchCond As String = "(s.St_ProName_vc LIKE @Search " & " OR CAST(s.St_StockID_n AS VARCHAR(20)) LIKE @Search)"
                If WhereCond = "" Then
                    WhereCond = " WHERE " & SearchCond
                Else
                    WhereCond &= " AND " & SearchCond
                End If
            End If

            Select Case VistawinFunctions.ProjectTypechk
                Case "INVEN"
                    If DelProList = False Then
                        Sql = "SELECT 'NO' AS Tick," & ColList & " FROM tbl_StockDtl s LEFT JOIN tbl_MultiStock ms ON s.St_StockID_n=ms.St_StockID_n LEFT JOIN tbl_Category c ON s.St_CatID_i=c.Category_Code_n LEFT JOIN tbl_Brand br ON s.St_BrandID_i=br.Brand_Code_n " & WhereCond & " ORDER BY s.St_ProName_vc"
                    Else
                        Sql = "SELECT 'NO' AS Tick," & ColList & " FROM tbl_StockDtl s LEFT JOIN tbl_MultiStock ms ON s.St_StockID_n=ms.St_StockID_n LEFT JOIN tbl_Category c ON s.St_CatID_i=c.Category_Code_n LEFT JOIN tbl_Brand br ON s.St_BrandID_i=br.Brand_Code_n WHERE NOT EXISTS (SELECT 1 FROM tbl_SalParticular sp WHERE sp.Sa_StCode_n=s.St_StockID_n) AND NOT EXISTS (SELECT 1 FROM tbl_PurchParticular pp WHERE pp.Sa_StCode_n=s.St_StockID_n) AND NOT EXISTS (SELECT 1 FROM tbl_DamageParticular dp WHERE dp.Sa_StCode_n=s.St_StockID_n)"
                        If SearchText.Trim <> "" Then
                            Sql &= " AND (s.St_ProName_vc LIKE @Search OR CAST(s.St_StockID_n AS VARCHAR(20)) LIKE @Search)"
                        End If
                        Sql &= " ORDER BY s.St_ProName_vc"
                    End If
                Case "CLINIC"
                    If DelProList = False Then
                        Sql = "SELECT 'NO' AS Tick," & ColList & " FROM tbl_Stock s LEFT JOIN tbl_MultiStock ms ON s.St_StockID_n=ms.St_StockID_n LEFT JOIN tbl_Category c ON s.St_CatID_i=c.Category_Code_n LEFT JOIN tbl_CusProfile sp ON (C_Type_vc='SUPPLIER' AND ms.St_SupID_i=sp.C_ID_n) LEFT JOIN tbl_Pharma p ON (ms.St_PharID_i=p.Pharma_Code_n) " & WhereCond & " ORDER BY s.St_ProName_vc"
                    Else
                        Sql = "SELECT 'NO' AS Tick," & ColList & " FROM tbl_Stock s LEFT JOIN tbl_MultiStock ms ON s.St_StockID_n=ms.St_StockID_n LEFT JOIN tbl_Category c ON s.St_CatID_i=c.Category_Code_n LEFT JOIN tbl_CusProfile sp ON (C_Type_vc='SUPPLIER' AND ms.St_SupID_i=sp.C_ID_n) LEFT JOIN tbl_Pharma p ON (ms.St_PharID_i=p.Pharma_Code_n) WHERE NOT EXISTS (SELECT 1 FROM tbl_BillParticular sp WHERE sp.Sa_StCode_n=s.St_StockID_n) AND NOT EXISTS (SELECT 1 FROM tbl_PurchParticular pp WHERE pp.Sa_StCode_n=s.St_StockID_n)"
                        If SearchText.Trim <> "" Then
                            Sql &= " AND (s.St_ProName_vc LIKE @Search OR CAST(s.St_StockID_n AS VARCHAR(20)) LIKE @Search)"
                        End If
                        Sql &= " ORDER BY s.St_ProName_vc"
                    End If
            End Select

            Using cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                If SearchText.Trim <> "" Then
                    If CbAny.Checked Then
                        cmd.Parameters.AddWithValue("@Search", "%" & SearchText.Trim & "%")
                    Else
                        cmd.Parameters.AddWithValue("@Search", SearchText.Trim & "%")
                    End If
                End If
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtSale)
                End Using
            End Using
            If dtSale.Rows.Count = 0 Then
                'MsgBox("No Data Found")
                Exit Sub
            End If
            Dim ColTotals As New Dictionary(Of Integer, Decimal)
            Dim ChkCol As New DataGridViewCheckBoxColumn
            With ChkCol
                .Name = "Tick"
                .HeaderText = "Tick"
                .Width = 40
                .ReadOnly = False
            End With
            DgvStockReport.Columns.Add(ChkCol)
            Dim SNoCol As New DataGridViewTextBoxColumn
            With SNoCol
                .Name = "SNo"
                .HeaderText = "S.No"
                .Width = 60
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            DgvStockReport.Columns.Add(SNoCol)
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
                DgvStockReport.Columns.Add(dgCol)
            Next
            For Each drSale As DataRow In dtSale.Rows
                Dim Row As Integer = DgvStockReport.Rows.Add()
                DgvStockReport.Rows(Row).Cells("Tick").Value = False
                DgvStockReport.Rows(Row).Cells("SNo").Value = Row + 1
                If drSale("Tick").ToString().ToUpper() = "YES" Then
                    DgvStockReport.Rows(Row).Cells("Tick").Value = True
                Else
                    DgvStockReport.Rows(Row).Cells("Tick").Value = False
                End If
                For i As Integer = 0 To dtDesign.Rows.Count - 1
                    Dim ColName As String = dtDesign.Rows(i)("BD_ColumnName_vc").ToString()
                    Dim Dt As String = dtDesign.Rows(i)("BD_Datatype_vc").ToString().ToUpper()
                    If IsDBNull(drSale(ColName)) Then
                        DgvStockReport.Rows(Row).Cells(i + 2).Value = ""
                    Else
                        Select Case Dt
                            Case "DATE"
                                DgvStockReport.Rows(Row).Cells(i + 2).Value = Format(CDate(drSale(ColName)), "dd/MM/yyyy")
                            Case "QTY", "INT"
                                Dim Qty As Decimal = Val(drSale(ColName))
                                If Qty = Math.Truncate(Qty) Then
                                    DgvStockReport.Rows(Row).Cells(i + 2).Value = Qty.ToString("N0")
                                Else
                                    DgvStockReport.Rows(Row).Cells(i + 2).Value = Qty.ToString("N3")
                                End If
                            Case "DECIMAL", "NUMBER", "NUMERIC", "FINALTOTAL"
                                DgvStockReport.Rows(Row).Cells(i + 2).Value = FormatNumber(Val(drSale(ColName)), 2)
                            Case Else
                                DgvStockReport.Rows(Row).Cells(i + 2).Value = drSale(ColName)
                        End Select
                    End If
                    If Dt = "FINALTOTAL" Then
                        If Not ColTotals.ContainsKey(i) Then
                            ColTotals.Add(i, 0D)
                        End If

                        If Not IsDBNull(drSale(ColName)) Then
                            ColTotals(i) += Convert.ToDecimal(drSale(ColName))
                        End If
                    End If
                Next
            Next
            Dim TotalRow As Integer = DgvStockReport.Rows.Add()
            With DgvStockReport.Rows(TotalRow)
                .Cells("Tick").Value = False
                '.Cells("SNo").Value = "TOTAL"
                .DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .DefaultCellStyle.ForeColor = Color.Red
            End With
            For Each kv In ColTotals
                DgvStockReport.Rows(TotalRow).Cells(kv.Key + 2).Value = FormatNumber(kv.Value, 2)
            Next
            For Each col As DataGridViewColumn In DgvStockReport.Columns
                If col.Name = "Tick" OrElse col.Name = "SNo" Then
                    col.SortMode = DataGridViewColumnSortMode.NotSortable
                Else
                    col.SortMode = DataGridViewColumnSortMode.Automatic
                End If
                If col.Name = "Tick" Then
                    col.ReadOnly = False
                Else
                    col.ReadOnly = True
                End If
            Next
            ReNumberStockReport()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub DgvStockReport_Sorted(ByVal sender As Object, ByVal e As EventArgs) Handles DgvStockReport.Sorted
        ReNumberStockReport()
    End Sub
    Private Sub ReNumberStockReport()
        Try
            Dim SNo As Integer = 1
            For Each Row As DataGridViewRow In DgvStockReport.Rows
                If Row.IsNewRow Then Continue For
                If Row.Tag IsNot Nothing AndAlso _
                   Row.Tag.ToString() = "TOTAL" Then
                    Row.Cells("SNo").Value = ""
                    Continue For
                End If
                Row.Cells("SNo").Value = SNo
                SNo += 1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtProductName1A_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtProductName1A.TextChanged
        InvenProReport(txtProductName1A.Text.Trim)
    End Sub
    Private Sub DgvStockReport_CellMouseClick(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles DgvStockReport.CellMouseClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            If e.Button = MouseButtons.Right Then
                DgvStockReport.ClearSelection()
                DgvStockReport.Rows(e.RowIndex).Selected = True
                SelectedStNumber = DgvStockReport.Rows(e.RowIndex).Cells("St_StNumber_i").Value.ToString()
                SelectedStockID = DgvStockReport.Rows(e.RowIndex).Cells("St_StockID_n").Value.ToString()
                SelectedProName = DgvStockReport.Rows(e.RowIndex).Cells("St_ProName_vc").Value.ToString()
                CmsCustomer.Show(Cursor.Position)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub MnuSalesRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuSalesRpt.Click
        Dim Frm As New SubFrmProBillDtlNew
        Frm.LProName = SelectedProName
        Frm.LStockID = SelectedStockID
        Frm.LStNumber = SelectedStNumber
        Frm.InvRptType = "INVOICE"
        Frm.ShowDialog()
    End Sub
    Private Sub MnuSRTRpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuSRTRpt.Click
        Dim Frm As New SubFrmProBillDtlNew
        Frm.LProName = SelectedProName
        Frm.LStockID = SelectedStockID
        Frm.LStNumber = SelectedStNumber
        Frm.InvRptType = "SALES RETURN"
        Frm.ShowDialog()
    End Sub
    Private Sub MnuPurRpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuPurRpt.Click
        Dim Frm As New SubFrmProBillDtlNew
        Frm.LProName = SelectedProName
        Frm.LStockID = SelectedStockID
        Frm.LStNumber = SelectedStNumber
        Frm.InvRptType = "PURCHASE"
        Frm.ShowDialog()
    End Sub
    Private Sub MnuPRTRpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuPRTRpt.Click
        Dim Frm As New SubFrmProBillDtlNew
        Frm.LProName = SelectedProName
        Frm.LStockID = SelectedStockID
        Frm.LStNumber = SelectedStNumber
        Frm.InvRptType = "PURCHASE RETURN"
        Frm.ShowDialog()
    End Sub
    Private Sub LblMore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblMore.Click
        GrpMore.Show()
    End Sub
    Private Sub BtnShowHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShowHide.Click
        GrpLeft.Visible = Not GrpLeft.Visible
        If GrpLeft.Visible Then
            BtnShowHide.Text = "<<"
        Else
            BtnShowHide.Text = ">>"
        End If
    End Sub
    Private Sub MnuStLedger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuStLedger.Click
        Dim Frm As New RptStockLedgerNav
        Frm.LedProName = SelectedProName
        Frm.LedStockID = SelectedStockID
        Frm.OpenStLed = True
        Frm.ShowDialog()
    End Sub
    Private Sub CBSelectM_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBSelectM.CheckedChanged
        If CBSelectM.Checked = True Then
            For Each Dr As DataGridViewRow In DgvMasterList.Rows
                Dr.Cells("TickM").Value = "YES"
            Next
        ElseIf CBSelectM.Checked = False Then
            For Each Dr As DataGridViewRow In DgvMasterList.Rows
                Dr.Cells("TickM").Value = "NO"
            Next
        End If
    End Sub
    Private Sub BtnColOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnColOrder.Click
        Dim Frm As New SubFrmColOrderCommon
        Frm.ColOrderFormChk = "STOCK REPORT"
        Frm.ShowDialog()
    End Sub
    Private Sub BulkSave()
        If Trim(CbChangeType.Text) = "" Then
            MsgBox("Select Change Type")
            Exit Sub
        End If
        If Trim(txtOldValue.Text) = "" Then
            MsgBox("Enter Old Value")
            txtOldValue.Focus()
            Exit Sub
        End If
        If Trim(txtNewValue.Text) = "" Then
            MsgBox("Enter New Value")
            txtNewValue.Focus()
            Exit Sub
        End If
        Dim TableName As String = ""
        Dim FieldName As String = ""
        Dim ValueType As String = ""
        Select Case Trim(CbChangeType.Text)
            Case "MRP"
                TableName = "tbl_MultiStock"
                FieldName = "St_MRPRate_n"
                ValueType = "DECIMAL"
            Case "Purchase Rate"
                TableName = "tbl_MultiStock"
                FieldName = "St_PurRate_n"
                ValueType = "DECIMAL"
            Case "Sale Rate"
                TableName = "tbl_MultiStock"
                FieldName = "St_SaleRate_n"
                ValueType = "DECIMAL"
            Case "Wholesale Rate"
                TableName = "tbl_MultiStock"
                FieldName = "St_SaleRate2_n"
                ValueType = "DECIMAL"
            Case "HSNCode"
                TableName = "tbl_StockDtl"
                FieldName = "St_HSN_vc"
                ValueType = "TEXT"
            Case "Unit"
                TableName = "tbl_StockDtl"
                FieldName = "St_Unit_vc"
                ValueType = "TEXT"
            Case "CGST"
                TableName = "tbl_StockDtl"
                FieldName = "St_CGSTPer_n"
                ValueType = "TEXT"
            Case "SGST"
                TableName = "tbl_StockDtl"
                FieldName = "St_SGSTPer_n"
                ValueType = "TEXT"
            Case "Shelf"
                TableName = "tbl_StockDtl"
                FieldName = "St_Shelf_vc"
                ValueType = "TEXT"
            Case "Row"
                TableName = "tbl_StockDtl"
                FieldName = "St_Row_vc"
                ValueType = "TEXT"
            Case "Reminder Stock"
                TableName = "tbl_StockDtl"
                FieldName = "St_RemiStock_i"
                ValueType = "INTEGER"
            Case "Disc Per"
                TableName = "tbl_StockDtl"
                FieldName = "St_DisPer_n"
                ValueType = "TEXT"
            Case Else
                MsgBox("Invalid Change Type")
                Exit Sub
        End Select
        Dim OldDecimal As Decimal
        Dim NewDecimal As Decimal
        Dim OldInteger As Integer
        Dim NewInteger As Integer

        If ValueType = "DECIMAL" Then
            If Not Decimal.TryParse(Trim(txtOldValue.Text), OldDecimal) Then
                MsgBox("Invalid Old Value")
                txtOldValue.Focus()
                Exit Sub
            End If
            If Not Decimal.TryParse(Trim(txtNewValue.Text), NewDecimal) Then
                MsgBox("Invalid New Value")
                txtNewValue.Focus()
                Exit Sub
            End If
        ElseIf ValueType = "INTEGER" Then
            If Not Integer.TryParse(Trim(txtOldValue.Text), OldInteger) Then
                MsgBox("Invalid Old Value")
                txtOldValue.Focus()
                Exit Sub
            End If
            If Not Integer.TryParse(Trim(txtNewValue.Text), NewInteger) Then
                MsgBox("Invalid New Value")
                txtNewValue.Focus()
                Exit Sub
            End If
        End If
        Dim Tran As SqlTransaction = Nothing
        Try
            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If
            Tran = VistawinFunctions.ServerConnection.BeginTransaction()

            Dim Sql As String = "UPDATE TableName  SET " & FieldName & "=@NewValue WHERE " & FieldName & "=@OldValue"
            Dim RowsAffected As Integer = 0
            Using Cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection, Tran)
                If ValueType = "DECIMAL" Then
                    Dim PNew As SqlParameter = Cmd.Parameters.Add("@NewValue", SqlDbType.Decimal)
                    PNew.Precision = 18
                    PNew.Scale = 2
                    PNew.Value = NewDecimal
                    Dim POld As SqlParameter = Cmd.Parameters.Add("@OldValue", SqlDbType.Decimal)
                    POld.Precision = 18
                    POld.Scale = 2
                    POld.Value = OldDecimal
                ElseIf ValueType = "INTEGER" Then
                    Cmd.Parameters.Add("@NewValue", SqlDbType.Int).Value = NewInteger
                    Cmd.Parameters.Add("@OldValue", SqlDbType.Int).Value = OldInteger
                Else
                    Cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar, 500).Value = Trim(txtNewValue.Text)
                    Cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar, 500).Value = Trim(txtOldValue.Text)
                End If
                RowsAffected = Cmd.ExecuteNonQuery()
            End Using
            If RowsAffected > 0 Then
                Tran.Commit()
                MsgBox(RowsAffected.ToString() & " Record(s) Successfully Changed")
                txtOldValue.Clear()
                txtOldID.Clear()
                txtNewValue.Clear()
                txtOldValue.Focus()
            Else
                Tran.Rollback()
                MsgBox("Old Value Not Found")
                txtOldValue.Focus()
            End If
        Catch ex As Exception
            If Tran IsNot Nothing Then
                Try
                    Tran.Rollback()
                Catch
                End Try
            End If
            MessageBox.Show(ex.Message, "Bulk Change", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Tran IsNot Nothing Then
                Tran.Dispose()
            End If
        End Try
    End Sub
    Private Sub SelectCurrentRow()
        Try
            Dim InvNo As String = ""
            If DgvStockReport.CurrentRow Is Nothing Then Exit Sub

            If Not DgvStockReport.Columns.Contains("St_StockID_n") Then
                MsgBox("Stock ID Column Not Found", MsgBoxStyle.Exclamation, "Information")
                Exit Sub
            End If

            InvNo = Trim(Convert.ToString(DgvStockReport.CurrentRow.Cells("St_StockID_n").Value))
            If InvNo = "" Then
                MsgBox("Stock ID Not Found", MsgBoxStyle.Exclamation, "Information")
                Exit Sub
            End If

            If TypeOf Me.Owner Is FrmDgvStockCommon Then
                Dim frm As FrmDgvStockCommon = DirectCast(Me.Owner, FrmDgvStockCommon)
                frm.LoadStock(InvNo)

            Else
                MsgBox("Stock Parent Form Not Found", MsgBoxStyle.Exclamation, "Information")
                Exit Sub
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Stock Edit", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub LblSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblSendtoServer.Click
        Dim frm As New FrmHostingSync
        frm.ShowDialog()
    End Sub
End Class