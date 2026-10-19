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
Imports System.Drawing.Drawing2D
Imports System.Byte
Public Class FrmDgvStockCommon
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

    Dim OpenImg As New OpenFileDialog
    Private RightClickCell As DataGridViewCell = Nothing
    Private WithEvents dgvDatePicker As New DateTimePicker
    Private CurrentDateRow As Integer = -1

    Private MultiStockCalculating As Boolean = False

    Dim dtArea As New DataTable
    Dim dgvSearch As New DataGridView
    Dim ActiveRow As Integer = -1
    Dim AddChk As Boolean = False

    Private ImgMenu As ContextMenuStrip
    Dim DgvStockDtlH As New DataGridView
    Dim valueCell As DataGridViewCell

    Dim SelectedStockID As String = ""
    Dim SelectedProName As String = ""

    Dim ImgStockID As Integer = 0

    Dim CatCode As Integer = 0
    Dim BranCode As Integer = 0
    Dim ProceCode As Integer = 0
    Dim MolCode As Integer = 0
    Dim CmpCode As Integer = 0
    Dim SupCode As Integer = 0
    Dim PharCode As Integer = 0

    Public StockNoToLoad As String = ""
    Public ChangedCategoryName As String = ""
    Public Treatfrm As String = "NO"

    Private Sub FrmDgvStockCommon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddHandler DgvStockDtl.DataError, AddressOf DgvStockDtl_DataError

        Dim ImgMenu As New ContextMenuStrip
        Dim mnuUpload As New ToolStripMenuItem("Upload Image")
        Dim mnuCamera As New ToolStripMenuItem("Capture From Camera")
        Dim mnuView As New ToolStripMenuItem("View Image")
        Dim mnuDelete As New ToolStripMenuItem("Delete Image")

        ImgMenu.Items.AddRange(New ToolStripItem() {
            mnuUpload, mnuCamera, mnuView, mnuDelete
        })

        DgvStockDtl.ContextMenuStrip = ImgMenu

        AddHandler mnuUpload.Click, AddressOf mnuUpload_Click
        AddHandler mnuView.Click, AddressOf mnuView_Click
        AddHandler mnuDelete.Click, AddressOf mnuDelete_Click
        AddHandler mnuCamera.Click, AddressOf mnuCamera_Click

        dgvDatePicker.Format = DateTimePickerFormat.Custom
        dgvDatePicker.CustomFormat = "dd/MM/yyyy"
        dgvDatePicker.Visible = False
        DgvStockDtl.Controls.Add(dgvDatePicker)

        CreateImageMenu()

        For Each col As DataGridViewColumn In DgvStockDtl.Columns
            If TypeOf col Is DataGridViewImageColumn Then
                CType(col, DataGridViewImageColumn).ImageLayout =
                    DataGridViewImageCellLayout.Zoom
            End If
        Next
        DgvLength.Hide()
        DgvLength.Location = New System.Drawing.Point(499, 11)
        DgvLength.Size = New System.Drawing.Size(733, 300)
        DgvLength.BringToFront()

        GrpSettings.Hide()
        GrpSettings.BringToFront()
        GrpSettings.Location = New System.Drawing.Point(521, 92)
        GrpSettings.Size = New System.Drawing.Size(663, 384)

        Me.LoadEmptyMsDgv()
        Me.LoadEmptyDgv()

        Me.LoadMultiStockAdd()

        If Trim(StockNoToLoad) <> "" Then
            Me.LoadStock(StockNoToLoad)
        End If

    End Sub
    Private Sub LoadEmptyMsDgv()
        DgvMultiSt.Columns.Clear()
        DgvMultiSt.Columns.Add("Field", "Field")
        DgvMultiSt.Columns.Add("Value", "Value")

        ' Field column fixed
        DgvMultiSt.Columns(0).ReadOnly = True
        DgvMultiSt.Columns(0).Width = 180
        DgvMultiSt.Columns(0).DefaultCellStyle.BackColor = Color.MediumAquamarine
        Panel1.BackColor = Color.MediumAquamarine
        DgvMultiSt.Columns(1).Width = 300
        DgvMultiSt.Columns(1).Width = 300

        For Each col As DataGridViewColumn In DgvMultiSt.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        Me.LoadMultiStockAdd()
    End Sub
    Private Sub LoadEmptyDgv()
        DgvStockDtl.Columns.Clear()
        DgvStockDtl.Columns.Add("Field", "Field")
        DgvStockDtl.Columns.Add("Value", "Value")

        DgvStockDtl.Columns(0).ReadOnly = True
        DgvStockDtl.Columns(0).Width = 180
        DgvStockDtl.Columns(0).DefaultCellStyle.BackColor = Color.MediumAquamarine
        DgvStockDtl.Columns(0).DefaultCellStyle.Font = New Font("Times New Roman", 10, FontStyle.Bold)

        DgvStockDtl.Columns(1).Width = 300
        DgvStockDtl.Columns(1).Width = 300

        'DgvStockDtl.ColumnHeadersDefaultCellStyle.BackColor = Color.MediumAquamarine
        'DgvStockDtl.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        Panel5.BackColor = Color.MediumAquamarine
        Panel1.BackColor = Color.MediumAquamarine
        BtnQtyFocus.BackColor = Color.MediumAquamarine
        BtnDgvFocus.BackColor = Color.MediumAquamarine
        BtnStReport.BackColor = Color.MediumAquamarine
        btnSave.BackColor = Color.MediumAquamarine
        BtnClear.BackColor = Color.MediumAquamarine
        BtnDashboard.BackColor = Color.MediumAquamarine
        BtnPharChange.BackColor = Color.MediumAquamarine
        BtnSettings.BackColor = Color.MediumAquamarine
        BtnDel.BackColor = Color.MediumAquamarine
        BtnClose1.BackColor = Color.MediumAquamarine
        BtnColOrd.BackColor = Color.MediumAquamarine

        For Each col As DataGridViewColumn In DgvStockDtl.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        Me.LoadStockAdd()
    End Sub
    Private Sub CreateImageMenu()

        ImgMenu = New ContextMenuStrip

        Dim mnuUpload As New ToolStripMenuItem("Upload Image")
        Dim mnuCamera As New ToolStripMenuItem("Capture From Camera")
        Dim mnuView As New ToolStripMenuItem("View Image")
        Dim mnuDelete As New ToolStripMenuItem("Delete Image")

        ImgMenu.Items.AddRange(New ToolStripItem() {
            mnuUpload, mnuCamera, mnuView, mnuDelete
        })

        'Attach events
        AddHandler mnuUpload.Click, AddressOf mnuUpload_Click
        AddHandler mnuView.Click, AddressOf mnuView_Click
        AddHandler mnuDelete.Click, AddressOf mnuDelete_Click
        AddHandler mnuCamera.Click, AddressOf mnuCamera_Click

    End Sub
    Private Sub DgvStockDtl_CellMouseDown(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles DgvStockDtl.CellMouseDown

        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        If e.Button = MouseButtons.Right Then
            Dim clickedCell As DataGridViewCell = DgvStockDtl(e.ColumnIndex, e.RowIndex)

            If TypeOf clickedCell Is DataGridViewImageCell Then
                DgvStockDtl.CurrentCell = clickedCell
                RightClickCell = CType(clickedCell, DataGridViewImageCell)
                ImgMenu.Show(Cursor.Position)
            Else
                RightClickCell = Nothing
            End If
        End If

    End Sub
    Private Sub mnuUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        If RightClickCell Is Nothing Then Exit Sub

        Using ofd As New OpenFileDialog()
            ofd.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp"

            If ofd.ShowDialog() = DialogResult.OK Then

                Dim tmp As Image = Image.FromFile(ofd.FileName)
                Dim img As New Bitmap(tmp)
                tmp.Dispose()

                RightClickCell.Value = img
                DgvStockDtl.Rows(RightClickCell.RowIndex).Height = 80

            End If
        End Using

    End Sub
    Private Sub mnuView_Click(ByVal sender As Object, ByVal e As EventArgs)

        If RightClickCell Is Nothing Then Exit Sub
        If RightClickCell.Value Is Nothing Then Exit Sub

        Dim f As New Form
        f.Size = New Size(600, 500)
        f.Text = "Image Preview"

        Dim pic As New PictureBox
        pic.Dock = DockStyle.Fill
        pic.SizeMode = PictureBoxSizeMode.Zoom
        pic.Image = CType(RightClickCell.Value, Image)

        f.Controls.Add(pic)
        f.ShowDialog()

    End Sub
    Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As EventArgs)

        If RightClickCell Is Nothing Then Exit Sub

        If MessageBox.Show("Delete Image?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            RightClickCell.Value = Nothing
        End If

    End Sub
    Private Sub mnuCamera_Click(ByVal sender As Object, ByVal e As EventArgs)

        If RightClickCell Is Nothing Then Exit Sub

        MessageBox.Show("Camera capture is not available in the SuperMarket edition.",
                        "Stock Image", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
    Private Sub DgvStockDtl_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs)
        e.ThrowException = False
    End Sub
    Private Sub AddButtonRow(ByVal ButtonText As String, ByVal BtnColor As Color)

        Dim Row As Integer = DgvStockDtl.Rows.Add()

        Dim Btn As New DataGridViewButtonCell

        Btn.Value = ButtonText

        Btn.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Btn.Style.BackColor = BtnColor
        Btn.Style.ForeColor = Color.White
        Btn.Style.SelectionBackColor = BtnColor
        Btn.Style.SelectionForeColor = Color.White

        DgvStockDtl.Rows(Row).Cells(0).Value = ""
        DgvStockDtl.Rows(Row).Cells(1) = Btn
        DgvStockDtl.Rows(Row).Height = 32

    End Sub
    Private Sub LoadStockAdd()
        AddChk = True
        DgvStockDtl.Rows.Clear()

        Dim dtDesign As New DataTable

        Dim Sql As String = "SELECT * FROM tbl_DgvEntry_Stock WHERE BD_ColShow_vc='YES' ORDER BY BD_ColOrderby_i"

        Dim DaDesign As New SqlDataAdapter(Sql, VistawinFunctions.ServerConnection)
        DaDesign.Fill(dtDesign)

        Dim ColList As String = ""

        For Each dr As DataRow In dtDesign.Rows
            ColList &= dr("BD_SQLColumn_vc").ToString() & ","
        Next

        ColList = ColList.TrimEnd(","c)

        Dim Ds As New DataSet
        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                Adapter = New SqlDataAdapter("SELECT " & ColList & " FROM tbl_StockDtl", VistawinFunctions.ServerConnection)

            Case "CLINIC"
                Adapter = New SqlDataAdapter("SELECT " & ColList & " FROM tbl_Stock", VistawinFunctions.ServerConnection)
        End Select

        Adapter.Fill(Ds, "FillData")

        Dim dt As DataTable = Ds.Tables("FillData")

        For Each drDesign As DataRow In dtDesign.Rows

            Dim ColumnName As String = drDesign("BD_ColumnName_vc").ToString()
            Dim DisplayName As String = drDesign("BD_AliasName_vc").ToString()
            Dim ControlType As String = drDesign("BD_ControlType_vc").ToString().ToUpper()
            Dim IsReadOnly As Boolean = (drDesign("BD_ReadOnly_vc").ToString().ToUpper() = "YES")
            Dim IsMandatory As Boolean = (drDesign("BD_Mandatory_vc").ToString().ToUpper() = "YES")

            If Not dt.Columns.Contains(ColumnName) Then Continue For
            Dim Cell As DataGridViewCell

            Select Case ControlType
                Case "DATE"
                    Cell = New DataGridViewTextBoxCell()
                    Cell.Value = ""
                Case "TEXTBOX"
                    Cell = New DataGridViewTextBoxCell()
                    Cell.Value = ""
                Case "MULTILINE"
                    Cell = New DataGridViewTextBoxCell()
                    Cell.Value = ""
                Case "COMBO"
                    Dim cmb As New DataGridViewComboBoxCell()

                    Dim dtCombo As New DataTable
                    Dim da As New SqlDataAdapter(drDesign("BD_ComboSQL_vc").ToString(), VistawinFunctions.ServerConnection)
                    da.Fill(dtCombo)

                    For Each r As DataRow In dtCombo.Rows
                        cmb.Items.Add(r(0).ToString())
                    Next

                    If cmb.Items.Count > 0 Then
                        cmb.Value = cmb.Items(0)
                    End If
                    Cell = cmb
                Case "IMAGE"
                    Dim img As New DataGridViewImageCell
                    img.ImageLayout = DataGridViewImageCellLayout.Zoom
                    Cell = img
                Case "CHECKBOX"
                    Cell = New DataGridViewCheckBoxCell()
                Case Else
                    Cell = New DataGridViewTextBoxCell()
            End Select

            Dim RowIndex As Integer = DgvStockDtl.Rows.Add(DisplayName, "")
            DgvStockDtl.Rows(RowIndex).Cells(1) = Cell
            DgvStockDtl.Rows(RowIndex).Tag = ColumnName

            If ControlType = "DATE" Then
                Dim dtp As New DateTimePicker()
                dtp.Name = "dtp_" & ColumnName
                dtp.Tag = ColumnName
                dtp.AccessibleDescription = RowIndex.ToString()

                dtp.Format = DateTimePickerFormat.Custom

                Dim DateFmt As String = drDesign("BD_Format_vc").ToString().Trim()
                If DateFmt = "" Then
                    DateFmt = "dd/MM/yyyy"
                End If
                dtp.CustomFormat = DateFmt
                dtp.Value = Date.Today
                DgvStockDtl.Controls.Add(dtp)
                SetDatePickerPosition(dtp)
                dtp.BringToFront()
            End If
            Cell.ReadOnly = IsReadOnly
            If drDesign("BD_BackColor_vc").ToString() <> "" Then
                Cell.Style.BackColor = Color.FromName(drDesign("BD_BackColor_vc").ToString())
            End If
            If drDesign("BD_ForeColor_vc").ToString() <> "" Then
                Cell.Style.ForeColor = Color.FromName(drDesign("BD_ForeColor_vc").ToString())
            End If
            If IsNumeric(drDesign("BD_Height_i")) Then
                DgvStockDtl.Rows(RowIndex).Height = CInt(drDesign("BD_Height_i"))
            End If
        Next

        'AddButtonRow("SAVE", Color.Green)
        'AddButtonRow("CLOSE", Color.Red)

        DgvStockDtl.ClearSelection()
        DgvStockDtl.ResumeLayout()
    End Sub

    Private Sub LoadMultiStockAdd()
        AddChk = True
       
        DgvMultiSt.Rows.Clear()

        Dim dtDesign As New DataTable
        Dim Sql As String = "SELECT * FROM tbl_DgvEntry_MultiStock WHERE BD_ColShow_vc='YES' ORDER BY BD_ColOrderby_i"
        Dim DaDesign As New SqlDataAdapter(Sql, VistawinFunctions.ServerConnection)
        DaDesign.Fill(dtDesign)

        Dim ColList As String = ""
        For Each dr As DataRow In dtDesign.Rows
            If dr("BD_SQLColumn_vc").ToString().Trim() <> "" Then
                ColList &= dr("BD_SQLColumn_vc").ToString() & ","
            End If
        Next

        ColList = ColList.TrimEnd(","c)
        Dim Ds As New DataSet

        Adapter = New SqlDataAdapter("SELECT " & ColList & " FROM tbl_MultiStock", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        Dim dt As DataTable = Ds.Tables("FillData")

        DgvMultiSt.Rows.Clear()
        DgvMultiSt.Columns.Clear()

        Dim SnoCol As New DataGridViewTextBoxColumn
        SnoCol.Name = "SNo"
        SnoCol.HeaderText = "S.No"
        SnoCol.Width = 50
        SnoCol.ReadOnly = True
        SnoCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvMultiSt.Columns.Add(SnoCol)

        Dim DeleteCol As New DataGridViewButtonColumn
        DeleteCol.Name = "Delete"
        DeleteCol.HeaderText = "Delete"
        DeleteCol.Text = "Delete"
        DeleteCol.UseColumnTextForButtonValue = True
        DeleteCol.Width = 60
        DeleteCol.SortMode = DataGridViewColumnSortMode.NotSortable
        DgvMultiSt.Columns.Add(DeleteCol)

        For Each dr As DataRow In dtDesign.Rows

            Dim ColName As String = dr("BD_ColumnName_vc").ToString()
            Dim AliasName As String = dr("BD_AliasName_vc").ToString()
            Dim ControlType As String = dr("BD_ControlType_vc").ToString().Trim().ToUpper()

            Dim Col As DataGridViewColumn = Nothing
            Select Case ControlType
                Case "CHECKBOX"
                    Dim chkCol As New DataGridViewCheckBoxColumn
                    Col = chkCol
                Case "COMBO"
                    Dim cmbCol As New DataGridViewComboBoxColumn
                    Dim dtCombo As New DataTable

                    Dim ComboSql As String = dr("BD_ComboSQL_vc").ToString().Trim()
                    If ComboSql <> "" Then

                        Using da As New SqlDataAdapter(ComboSql, VistawinFunctions.ServerConnection)
                            da.Fill(dtCombo)
                        End Using
                        For Each r As DataRow In dtCombo.Rows
                            If Not IsDBNull(r(0)) Then
                                cmbCol.Items.Add(r(0).ToString())
                            End If
                        Next
                    End If
                    Col = cmbCol
                Case Else
                    Col = New DataGridViewTextBoxColumn()
            End Select

            Col.Name = AliasName
            Col.HeaderText = AliasName

            If IsNumeric(dr("BD_Width_i")) Then
                Dim ColWidth As Integer = CInt(dr("BD_Width_i"))
                If ColWidth > 0 Then
                    Col.Width = ColWidth
                End If
            End If
            If dr("BD_BackColor_vc").ToString().Trim() <> "" Then
                Col.DefaultCellStyle.BackColor = Color.FromName(dr("BD_BackColor_vc").ToString().Trim())
            End If
            If dr("BD_ForeColor_vc").ToString().Trim() <> "" Then
                Col.DefaultCellStyle.ForeColor = Color.FromName(dr("BD_ForeColor_vc").ToString().Trim())
            End If
            Dim Alignment As String = dr("BD_Alignment_vc").ToString().Trim().ToUpper()
            Select Case Alignment
                Case "LEFT"
                    Col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Case "CENTER"
                    Col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Case "RIGHT"
                    Col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End Select

            Select Case AliasName.ToUpper()

                Case "PURRATE", "SALERATE", "SALERATE2", "MRP", "MARGIN1", "MARGIN2", "OLD MRP RATE", "OLD SALE RATE", "MRP LESS(%)", "MRP LESS AMOUNT", "DISC(%)"

                    Col.DefaultCellStyle.Format = "N2"


                Case "GODOWN STOCK", "MEDICAL STOCK", "SHOP STOCK", "CURRSTOCK", "TOTAL STOCK"

                    Col.DefaultCellStyle.Format = "N3"


                Case Else

                    'STRING / StNumber / BatchNo etc.
                    'No Format

            End Select

            DgvMultiSt.Columns.Add(Col)
        Next
        Dim Row As Integer = DgvMultiSt.Rows.Add()

        DgvMultiSt.Rows(Row).Cells("SNo").Value = Row + 1
        For Each dr As DataRow In dtDesign.Rows
            If IsNumeric(dr("BD_Height_i")) Then
                Dim RowHeight As Integer = CInt(dr("BD_Height_i"))
                If RowHeight > 0 Then
                    DgvMultiSt.Rows(Row).Height = RowHeight
                End If
            End If
        Next
        With DgvMultiSt.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Maroon
            .ForeColor = Color.White
            .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        For Each col As DataGridViewColumn In DgvMultiSt.Columns
            'col.ReadOnly = True
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        DgvMultiSt.EnableHeadersVisualStyles = False
    End Sub
    Private Sub SaveStock()
        Try

            If Not ValidateStockGrid() Then Exit Sub
            If Not ValidateMultiGrid() Then Exit Sub

            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If

            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Try
                Dim TABLENAMECHK As String = ""
                If VistawinFunctions.ProjectTypechk = "INVEN" Then
                    TABLENAMECHK = "tbl_StockDtl"
                ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then
                    TABLENAMECHK = "tbl_Stock"
                Else
                End If

                Dim SPName As String
                If AddChk = True Then
                    SPName = "SP_Dgv_tbl_Stock_Insert"
                Else
                    SPName = "SP_Dgv_tbl_Stock_Edit"
                End If

                Dim StockID As Integer = Convert.ToInt32(Val(Convert.ToString(GetGridDecimal("Stock ID"))))
                If AddChk = True AndAlso StockID = 0 Then
                    Using cmdID As New SqlCommand("SELECT ISNULL(MAX(CONVERT(NUMERIC(18,0), St_StockID_n)),0)+1 FROM " & TABLENAMECHK, VistawinFunctions.ServerConnection, Trans)
                        StockID = Convert.ToInt32(cmdID.ExecuteScalar())
                    End Using
                ElseIf AddChk = False AndAlso StockID = 0 Then
                    MsgBox("Stock ID Not Found", MsgBoxStyle.Exclamation, "Information")
                    Trans.Rollback()
                    Exit Sub
                End If

                Dim CatID As Integer = 0
                Dim CatValue As Object = GetGridTag("Category")

                If CatValue IsNot Nothing AndAlso _
                   Not IsDBNull(CatValue) AndAlso _
                   Trim(CatValue.ToString()) <> "" Then
                    CatID = Convert.ToInt32(CatValue)
                End If

                Dim MolID As Integer = 0
                Dim MolValue As Object = GetGridTag("Molecule")

                If MolValue IsNot Nothing AndAlso _
                   Not IsDBNull(MolValue) AndAlso _
                   Trim(MolValue.ToString()) <> "" Then
                    MolID = Convert.ToInt32(MolValue)
                End If
                Dim ProcID As Integer = 0
                Dim ProcValue As Object = GetGridTag("How to Use")

                If ProcValue IsNot Nothing AndAlso _
                   Not IsDBNull(ProcValue) AndAlso _
                   Trim(ProcValue.ToString()) <> "" Then
                    ProcID = Convert.ToInt32(ProcValue)
                End If
                Dim CmpID As Integer = 0
                Dim CmpValue As Object = GetGridTag("Company Id")

                If CmpValue IsNot Nothing AndAlso _
                   Not IsDBNull(CmpValue) AndAlso _
                   Trim(CmpValue.ToString()) <> "" Then
                    CmpID = Convert.ToInt32(CmpValue)
                End If
                Dim BrandID As Integer = 0
                Dim BrandValue As Object = GetGridTag("Brand")
                If BrandValue IsNot Nothing AndAlso _
                   Not IsDBNull(BrandValue) AndAlso _
                   Trim(BrandValue.ToString()) <> "" Then
                    BrandID = Convert.ToInt32(BrandValue)
                End If

                Using cmd As New SqlCommand(SPName, VistawinFunctions.ServerConnection, Trans)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@St_StockID_n", StockID)
                    cmd.Parameters.AddWithValue("@St_ProName_vc", GetGridString("Product Name"))
                    cmd.Parameters.AddWithValue("@St_ProTamil_vc", GetGridString("Alias Name"))
                    cmd.Parameters.AddWithValue("@St_Unit_vc", GetGridString("Unit"))
                    cmd.Parameters.AddWithValue("@St_HSN_vc", GetGridString("HSN Code"))
                    cmd.Parameters.AddWithValue("@St_CGSTPer_n", GetGridDecimal("CGST (%)"))
                    cmd.Parameters.AddWithValue("@St_SGSTPer_n", GetGridDecimal("SGST (%)"))
                    cmd.Parameters.AddWithValue("@St_IGSTPer_n", GetGridDecimal("IGSTPer"))
                    cmd.Parameters.AddWithValue("@St_Shelf_vc", GetGridString("Shelf"))
                    cmd.Parameters.AddWithValue("@St_Row_vc", GetGridString("Row"))
                    cmd.Parameters.AddWithValue("@St_RemiStock_i", GetGridInteger("Reminter Stock"))
                    cmd.Parameters.AddWithValue("@St_Remark_vc", GetGridString("Note"))
                    cmd.Parameters.AddWithValue("@St_CatID_i", CatCode)
                    cmd.Parameters.AddWithValue("@St_BrandID_i", BranCode)
                    cmd.Parameters.AddWithValue("@St_ElCom_n", GetGridDecimal("Emp Comm (%)"))
                    cmd.Parameters.AddWithValue("@St_DisPer_n", GetGridDecimal("Disc (%)"))
                    cmd.Parameters.AddWithValue("@St_GoTotalSt_i", GetGridDecimal("Godown Stock"))
                    cmd.Parameters.AddWithValue("@St_MediTotalSt_i", GetGridDecimal("Medical Stock"))
                    cmd.Parameters.AddWithValue("@St_TotalSt_i", GetGridDecimal("Total Stock"))
                    cmd.Parameters.AddWithValue("@St_ProcedureID_i", ProceCode)
                    cmd.Parameters.AddWithValue("@St_MolID_i", MolCode)
                    cmd.Parameters.AddWithValue("@Old_CGSTPer_n", GetGridDecimal("Old CGST(%)"))
                    cmd.Parameters.AddWithValue("@Old_SGSTPer_n", GetGridDecimal("Old SGST(%)"))
                    cmd.Parameters.AddWithValue("@St_MediType_vc", GetGridString("Medicine Type"))
                    cmd.Parameters.AddWithValue("@St_ValType_vc", GetGridString("Val Type"))
                    cmd.Parameters.AddWithValue("@Old_MRPRate_n", GetGridDecimal("old MRP Rate"))
                    cmd.Parameters.AddWithValue("@MRP_LessPer_n", GetGridDecimal("MRP Less(%)"))
                    cmd.Parameters.AddWithValue("@MRP_LessAmt_n", GetGridDecimal("MRP Less Amount"))
                    cmd.Parameters.AddWithValue("@st_Guarantee_n", GetGridDecimal("Guarantee"))
                    cmd.Parameters.AddWithValue("@St_Warranty_n", GetGridDecimal("Warranty"))
                    cmd.Parameters.AddWithValue("@St_Margin1Cal_vc", GetGridString("Margin1 Cal"))
                    cmd.Parameters.AddWithValue("@St_Margin2Cal_vc", GetGridString("Margin2 Cal"))
                    cmd.Parameters.AddWithValue("@St_CmpID_i", CmpCode)
                    cmd.Parameters.AddWithValue("@St_EntDate_d", DateTime.Now)
                    Dim NillDate As Date = GetDateValue("Nill Date")
                    Dim OpStockDate As Date = GetDateValue("Op Stock Date")
                    cmd.Parameters.AddWithValue("@St_NillDate_d", If(NillDate = Date.MinValue, DBNull.Value, CType(NillDate, Object)))
                    cmd.Parameters.AddWithValue("@St_OpStockDate_d", If(OpStockDate = Date.MinValue, DBNull.Value, CType(OpStockDate, Object)))
                    cmd.Parameters.AddWithValue("@St_PackCal_vc", GetGridString("Pack Cal"))
                    cmd.Parameters.AddWithValue("@St_Liquor_vc", GetGridString("Liquor"))
                    cmd.ExecuteNonQuery()
                End Using

                For Each Dr As DataGridViewRow In DgvMultiSt.Rows

                    If Dr.IsNewRow Then Continue For

                    If VistawinFunctions.ProjectTypechk = "INVEN" Then

                        If Trim(If(Dr.Cells("SaleRate").Value, "").ToString()) = "" Then
                            Continue For
                        End If

                    ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then

                        If Trim(If(Dr.Cells("BatchNo").Value, "").ToString()) = "" Then
                            Continue For
                        End If

                    End If

                    If Dr.Cells("CurrStock").Value Is Nothing Then Continue For
                    If Trim(Dr.Cells("CurrStock").Value.ToString()) = "" Then
                        Continue For
                    End If

                    Dim StNumber As Integer = 0

                    If Dr.Cells("StNumber").Value IsNot Nothing Then
                        If Trim(Dr.Cells("StNumber").Value.ToString()) <> "" Then
                            StNumber = Convert.ToInt32(Val(Dr.Cells("StNumber").Value.ToString()))
                        End If
                    End If

                    Dim IsNewMultiStock As Boolean = False

                    If StNumber = 0 Then

                        IsNewMultiStock = True

                        Using cmdID As New SqlCommand( _
                            "SELECT ISNULL(MAX(St_StNumber_i),0)+1 FROM tbl_MultiStock",
                            VistawinFunctions.ServerConnection,
                            Trans)

                            StNumber = Convert.ToInt32(cmdID.ExecuteScalar())

                        End Using

                        Dr.Cells("StNumber").Value = StNumber

                    End If


                    If IsNewMultiStock Then
                        SPName = "SP_Dgv_tbl_MultiStock_Insert"
                    Else
                        SPName = "SP_Dgv_tbl_MultiStock_Edit"
                    End If

                    Using cmd As New SqlCommand( _
                        SPName, _
                        VistawinFunctions.ServerConnection, _
                        Trans)

                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@St_StNumber_i", StNumber)

                        cmd.Parameters.AddWithValue("@St_StockID_n", StockID)

                        cmd.Parameters.AddWithValue("@St_ProName_vc", _
                                                     GetGridString("Product Name"))

                        cmd.Parameters.AddWithValue("@St_CurrStock_i", _
                                                     GetDgvDecimalValue(Dr, "CurrStock"))

                        cmd.Parameters.AddWithValue("@St_BatchNo_vc", _
                                                     GetDgvStringValue(Dr, "BatchNo"))

                        cmd.Parameters.AddWithValue("@St_EMonth_i", _
                                                     GetDgvIntegerValue(Dr, "Month"))

                        cmd.Parameters.AddWithValue("@St_EYear_vc", _
                                                     GetDgvStringValue(Dr, "Year"))

                        cmd.Parameters.AddWithValue("@St_PurRate_n", _
                                                     GetDgvDecimalValue(Dr, "PurRate"))

                        cmd.Parameters.AddWithValue("@St_SaleRate_n", _
                                                     GetDgvDecimalValue(Dr, "SaleRate"))

                        cmd.Parameters.AddWithValue("@St_SaleRate2_n", _
                                                     GetDgvDecimalValue(Dr, "WholeSaleRate"))

                        cmd.Parameters.AddWithValue("@St_MRPRate_n", _
                                                     GetDgvDecimalValue(Dr, "MRP"))

                        cmd.Parameters.AddWithValue("@St_MarPer1_n", _
                                                     GetDgvDecimalValue(Dr, "Margin1"))

                        cmd.Parameters.AddWithValue("@St_MarPer2_n", _
                                                     GetDgvDecimalValue(Dr, "Margin2"))

                        cmd.Parameters.AddWithValue("@St_EDate_d", DateTime.Now)

                        cmd.Parameters.AddWithValue("@St_UpDate_d", DateTime.Now)

                        cmd.Parameters.AddWithValue("@St_UID_vc", _
                                                     VistawinFunctions.UserName)

                        cmd.Parameters.AddWithValue("@St_Show_vc", _
                                                     GetDgvStringValue(Dr, "Show"))

                        cmd.Parameters.AddWithValue("@St_Barcode_vc", _
                                                     GetDgvStringValue(Dr, "Barcode"))

                        cmd.Parameters.AddWithValue("@St_GoCurSt_i", _
                                                     GetDgvDecimalValue(Dr, "Godown Stock"))

                        cmd.Parameters.AddWithValue("@St_MediCurSt_i", _
                                                     GetDgvDecimalValue(Dr, "Medical Stock"))


                        Dim SupID As Integer = 0
                        Dim PharID As Integer = 0

                        Dim SupCell As DataGridViewCell =
                            Dr.Cells("Supplier Name")

                        If SupCell.Tag IsNot Nothing AndAlso
                           Trim(SupCell.Tag.ToString()) <> "" Then

                            SupID = Convert.ToInt32(SupCell.Tag)

                        End If


                        Dim PharCell As DataGridViewCell =
                            Dr.Cells("Pharma Name")

                        If PharCell.Tag IsNot Nothing AndAlso
                           Trim(PharCell.Tag.ToString()) <> "" Then

                            PharID = Convert.ToInt32(PharCell.Tag)

                        End If


                        cmd.Parameters.AddWithValue("@St_SupID_i", SupID)

                        cmd.Parameters.AddWithValue("@St_PharID_i", PharID)



                        cmd.Parameters.AddWithValue("@St_MFR_vc", _
                                                     GetDgvStringValue(Dr, "MFR"))

                        cmd.Parameters.AddWithValue("@St_Pack_i", _
                                                     GetDgvStringValue(Dr, "Pack"))

                        cmd.Parameters.AddWithValue("@St_Packchar_vc", _
                                                     GetDgvStringValue(Dr, "Packs"))

                        cmd.Parameters.AddWithValue("@Old_MRPRate_n", _
                                                     GetDgvDecimalValue(Dr, "Old MRP Rate"))

                        cmd.Parameters.AddWithValue("@Old_SaleRate_n", _
                                                     GetDgvDecimalValue(Dr, "Old Sale Rate"))

                        cmd.Parameters.AddWithValue("@MRP_LessPer_n", _
                                                     GetDgvDecimalValue(Dr, "MRP Less(%)"))

                        cmd.Parameters.AddWithValue("@MRP_LessAmt_n", _
                                                     GetDgvDecimalValue(Dr, "MRP Less Amount"))

                        cmd.Parameters.AddWithValue("@St_ExpStatus_vc", _
                                                     GetDgvStringValue(Dr, "Exp Status"))

                        cmd.Parameters.AddWithValue("@St_MulDisPer_n", _
                                                     GetDgvDecimalValue(Dr, "Disc(%)"))

                        cmd.Parameters.AddWithValue("@St_ProSize_vc", _
                                                     GetDgvStringValue(Dr, "Product Size"))

                        cmd.Parameters.AddWithValue("@St_ProColor_vc", _
                                                     GetDgvStringValue(Dr, "Product Colour"))

                        cmd.Parameters.AddWithValue("@St_ModelName_vc", GetDgvStringValue(Dr, "Model Name"))
                        cmd.Parameters.AddWithValue("@St_NoteChar_vc", GetDgvStringValue(Dr, "NoteChar"))
                        cmd.Parameters.AddWithValue("@St_NoteNum_n", GetDgvDecimalValue(Dr, "NoteNum"))
                        cmd.Parameters.AddWithValue("@St_NoteInt_i", GetDgvStringValue(Dr, "NoteInt"))

                        cmd.ExecuteNonQuery()

                    End Using

                Next

                ImgStockID = StockID
                For Each row As DataGridViewRow In DgvStockDtl.Rows
                    Dim ProName As String = Trim(GetGridValue("Product Name") & "")
                    If row.IsNewRow Then Continue For
                    If row.Cells(0).Value Is Nothing Then Continue For
                    Dim FieldName As String = row.Cells(0).Value.ToString()
                    If FieldName = "Product Image" Then
                        If row.Cells(1).Value IsNot Nothing AndAlso TypeOf row.Cells(1).Value Is System.Drawing.Image Then
                            SaveImageToFile(CType(row.Cells(1).Value, System.Drawing.Image), ProName & "_" & ImgStockID & ".jpg")
                        End If
                    End If
                Next
                Trans.Commit()

                If Treatfrm = "NO" Then
                    MessageBox.Show("Successfully Saved", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    ChangedCategoryName = GetCategoryName(CatCode)
                    MessageBox.Show("Successfully Saved", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                End If

            Catch ex As Exception
                Trans.Rollback()
                MessageBox.Show(ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Clear()
    End Sub
    Private Function ValidateStockGrid() As Boolean
        For Each row As DataGridViewRow In DgvStockDtl.Rows
            If row.IsNewRow Then Continue For

            Dim fieldName As String = Trim(If(row.Cells("Field").Value, "").ToString())
            Dim fieldValue As String = Trim(If(row.Cells("Value").Value, "").ToString())

            Select Case fieldName
                Case "Product Name"
                    If fieldValue = "" Then
                        MessageBox.Show("Enter Product Name")
                        DgvStockDtl.CurrentCell = row.Cells("Value")
                        DgvStockDtl.BeginEdit(True)
                        Return False
                    End If
            End Select
        Next
        Return True
    End Function
    Private Function ValidateMultiGrid() As Boolean

        Select Case VistawinFunctions.ProjectTypechk

            '========================================================
            ' INVEN
            ' BatchNo is hidden, so DON'T check BatchNo.
            ' SaleRate is used to identify the entered row.
            '========================================================
          Case "INVEN"

                For Each row As DataGridViewRow In DgvMultiSt.Rows

                    If row.IsNewRow Then Continue For

                    'Check Sale Rate
                    If Trim(If(row.Cells("SaleRate").Value, "").ToString()) = "" Then
                        MessageBox.Show("Enter Sale Rate")
                        DgvMultiSt.CurrentCell = row.Cells("SaleRate")
                        DgvMultiSt.BeginEdit(True)
                        Return False
                    End If

                    ''Check MRP
                    'If Trim(If(row.Cells("MRP").Value, "").ToString()) = "" Then
                    '    MessageBox.Show("Enter MRP Rate")
                    '    DgvMultiSt.CurrentCell = row.Cells("MRP")
                    '    DgvMultiSt.BeginEdit(True)
                    '    Return False
                    'End If

                    ''Check Purchase Rate
                    'If Trim(If(row.Cells("PurRate").Value, "").ToString()) = "" Then
                    '    MessageBox.Show("Enter Purchase Rate")
                    '    DgvMultiSt.CurrentCell = row.Cells("PurRate")
                    '    DgvMultiSt.BeginEdit(True)
                    '    Return False
                    'End If

                Next


                '========================================================
                ' CLINIC
                ' BatchNo is used to identify the entered row.
                ' Only rows having BatchNo will be validated/saved.
                '========================================================
            Case "CLINIC"

                For Each row As DataGridViewRow In DgvMultiSt.Rows

                    If row.IsNewRow Then Continue For

                    '--------------------------------
                    ' Get values
                    '--------------------------------
                    Dim BatchNo As String =
                        Trim(If(row.Cells("BatchNo").Value, "").ToString())

                    Dim MonthValue As String =
                        Trim(If(row.Cells("Month").Value, "").ToString())

                    Dim YearValue As String =
                        Trim(If(row.Cells("Year").Value, "").ToString())

                    Dim PackValue As String =
                        Trim(If(row.Cells("Pack").Value, "").ToString())

                    Dim MRP As String =
                        Trim(If(row.Cells("MRP").Value, "").ToString())

                    Dim PurRate As String =
                        Trim(If(row.Cells("PurRate").Value, "").ToString())


                    '--------------------------------
                    ' Completely empty row
                    '
                    ' SaleRate / CurrStock are NOT checked
                    ' because they may contain default 0.00
                    '--------------------------------
                    If BatchNo = "" AndAlso
                       MonthValue = "" AndAlso
                       YearValue = "" AndAlso
                       PackValue = "" AndAlso
                       MRP = "" AndAlso
                       PurRate = "" Then

                        Continue For

                    End If


                    '--------------------------------
                    ' Batch is required
                    '--------------------------------
                    If BatchNo = "" Then

                        MessageBox.Show("Enter Batch")

                        DgvMultiSt.CurrentCell = row.Cells("BatchNo")
                        DgvMultiSt.BeginEdit(True)

                        Return False

                    End If


                    '--------------------------------
                    ' Expiry Month
                    '--------------------------------
                    Dim expMonth As Integer

                    If Not Integer.TryParse(MonthValue, expMonth) _
                        OrElse expMonth < 1 _
                        OrElse expMonth > 12 Then

                        MessageBox.Show("Enter Valid Exp Month (1 to 12)")

                        DgvMultiSt.CurrentCell = row.Cells("Month")
                        DgvMultiSt.BeginEdit(True)

                        Return False

                    End If


                    '--------------------------------
                    ' Expiry Year
                    '--------------------------------
                    Dim expYear As Integer

                    If Not Integer.TryParse(YearValue, expYear) _
                        OrElse expYear < 1000 _
                        OrElse expYear > 9999 Then

                        MessageBox.Show("Enter Valid Exp Year (4 Digits)")

                        DgvMultiSt.CurrentCell = row.Cells("Year")
                        DgvMultiSt.BeginEdit(True)

                        Return False

                    End If


                    '--------------------------------
                    ' MRP
                    '--------------------------------
                    If MRP = "" Then

                        MessageBox.Show("Enter MRP Rate")

                        DgvMultiSt.CurrentCell = row.Cells("MRP")
                        DgvMultiSt.BeginEdit(True)

                        Return False

                    End If


                    '--------------------------------
                    ' Purchase Rate
                    '--------------------------------
                    If PurRate = "" Then

                        MessageBox.Show("Enter Purchase Rate")

                        DgvMultiSt.CurrentCell = row.Cells("PurRate")
                        DgvMultiSt.BeginEdit(True)

                        Return False

                    End If

                Next

        End Select

        Return True

    End Function

    Private Function GetCategoryName(ByVal CategoryID As Integer) As String

        If CategoryID = 0 Then Return ""

        Using Cmd As New SqlCommand( _
            "SELECT ISNULL(Category_Name_vc,'') " & _
            "FROM tbl_Category " & _
            "WHERE Category_Code_n=@CatID", _
            VistawinFunctions.ServerConnection)

            Cmd.Parameters.AddWithValue("@CatID", CategoryID)

            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If

            Return Convert.ToString(Cmd.ExecuteScalar()).Trim()

        End Using

    End Function
    Private Function GetDgvIntegerValue(ByVal Dr As DataGridViewRow,
                                    ByVal ColumnName As String) As Integer

        If Dr.Cells(ColumnName).Value Is Nothing Then
            Return 0
        End If

        If IsDBNull(Dr.Cells(ColumnName).Value) Then
            Return 0
        End If

        If Trim(Dr.Cells(ColumnName).Value.ToString()) = "" Then
            Return 0
        End If

        Return Convert.ToInt32(Val(Dr.Cells(ColumnName).Value.ToString()))

    End Function
    Private Sub SaveImageToFile(ByVal Img As System.Drawing.Image, ByVal FileName As String)
        If Img Is Nothing Then Exit Sub
        Dim FolderPath As String = VistawinFunctions.ScanningPath & "\Pro_Image\"
        If Not System.IO.Directory.Exists(FolderPath) Then
            System.IO.Directory.CreateDirectory(FolderPath)
        End If
        Img.Save(System.IO.Path.Combine(FolderPath, FileName), System.Drawing.Imaging.ImageFormat.Jpeg)
    End Sub
    Private Function LoadImageFromFile(ByVal FileName As String) As System.Drawing.Image
        Try
            Dim FolderPath As String = System.IO.Path.Combine(VistawinFunctions.ScanningPath, "Pro_Image")
            Dim FullPath As String = System.IO.Path.Combine(FolderPath, FileName)

            If Not System.IO.File.Exists(FullPath) Then
                Return Nothing
            End If
            Dim ImgBytes() As Byte = System.IO.File.ReadAllBytes(FullPath)
            Using ms As New MemoryStream(ImgBytes)
                Using tempImg As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                    Return New Bitmap(tempImg)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Load Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Sub DeleteCustomerImage(ByVal FieldName As String)
        Try
            Dim ProName As String = Trim(GetGridValue("Product Name") & "")
            Dim StockID As String = Trim(GetGridValue("Stock ID") & "")

            If StockID = "" Then Exit Sub
            Dim FilePath As String = ""


            FilePath = System.IO.Path.Combine(VistawinFunctions.ScanningPath & "\Pro_Image\", ProName & "_R" & StockID & ".jpg")
            If System.IO.File.Exists(FilePath) Then
                System.IO.File.Delete(FilePath)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function GetDateValue(ByVal FieldName As String) As Date
        For Each ctl As Control In DgvStockDtl.Controls
            If TypeOf ctl Is DateTimePicker Then
                If ctl.Tag IsNot Nothing AndAlso ctl.Tag.ToString = FieldName Then
                    Return DirectCast(ctl, DateTimePicker).Value
                End If
            End If
        Next
        Return Date.Now
    End Function
    Private Function GetGridString(ByVal FieldName As String) As String
        Dim Obj As Object = GetGridValue(FieldName)
        If Obj Is Nothing OrElse IsDBNull(Obj) Then
            Return ""
        End If
        Return Trim(Obj.ToString())
    End Function
    Private Function GetGridDecimal(ByVal FieldName As String) As Decimal
        Try
            Dim S As String = GetGridString(FieldName)
            If S = "" Then Return 0D
            Return Convert.ToDecimal(S)
        Catch
            Return 0D
        End Try
    End Function
    Private Function GetGridInteger(ByVal FieldName As String) As Integer
        Try
            Dim S As String = GetGridString(FieldName)
            If S = "" Then Return 0
            Return Convert.ToInt32(S)
        Catch
            Return 0
        End Try
    End Function
    Private Function GetDgvDecimalValue(ByVal Dr As DataGridViewRow, ByVal ColumnName As String) As Decimal
        Try
            If Dr Is Nothing Then Return 0D
            If Dr.Cells(ColumnName).Value Is Nothing Then
                Return 0D
            End If
            If IsDBNull(Dr.Cells(ColumnName).Value) Then
                Return 0D
            End If
            Dim s As String = Trim(Dr.Cells(ColumnName).Value.ToString())
            If s = "" Then
                Return 0D
            End If
            Return Convert.ToDecimal(s)
        Catch
            Return 0D
        End Try
    End Function
    Private Function GetDgvStringValue(ByVal Dr As DataGridViewRow, ByVal ColumnName As String) As String
        Try
            If Dr Is Nothing Then Return ""
            If Dr.Cells(ColumnName).Value Is Nothing Then
                Return ""
            End If
            If IsDBNull(Dr.Cells(ColumnName).Value) Then
                Return ""
            End If
            Return Trim(Dr.Cells(ColumnName).Value.ToString())
        Catch
            Return ""
        End Try
    End Function
    Private Function GetGridTag(ByVal DisplayName As String) As Object
        For Each rw As DataGridViewRow In DgvStockDtl.Rows
            If rw.Cells(0).Value IsNot Nothing Then
                If rw.Cells(0).Value.ToString() = DisplayName Then
                    Return rw.Cells(1).Tag
                End If
            End If
        Next
        Return DBNull.Value
    End Function
    Private Sub DgvStockDtl_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvStockDtl.CellDoubleClick
        Try
            Dim displayName As String = DgvStockDtl.Rows(e.RowIndex).Cells(0).Value.ToString()
            If displayName = "Category" Then
                Dim frm As New SubFrmCommonSearch
                frm.CatChk = "Category1"
                frm.Query = "SELECT Category_Code_n, Category_Name_vc FROM tbl_Category WHERE Category_Name_vc LIKE @Search OR CAST(Category_Code_n AS VARCHAR) LIKE @Search ORDER BY Category_Name_vc,Category_Code_n"
                frm.ReturnColumn = "Category_Name_vc"
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Value = frm.SelectedValue
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Tag = frm.SelectedCode
                    CatCode = frm.SelectedCode
                End If
            End If
            If displayName = "Brand" Then
                Dim CatChk As String = "Brand1"
                Dim frm As New SubFrmCommonSearch
                frm.CatChk = "Brand1"
                frm.BranQuery = "SELECT Brand_Code_n, Brand_Name_vc FROM tbl_Brand WHERE Brand_Name_vc LIKE @Search OR CAST(Brand_Code_n AS VARCHAR) LIKE @Search ORDER BY Brand_Name_vc"
                frm.ReturnColumn = "Brand_Name_vc"
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Value = frm.SelectedValue
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Tag = frm.SelectedCode
                    BranCode = frm.SelectedCode
                End If
            End If
            If displayName = "How to Use" Then
                Dim CatChk As String = "Procedure1"
                Dim frm As New SubFrmCommonSearch
                frm.CatChk = "Procedure1"
                frm.ProcedureQuery = "SELECT Pr_ProcedureID_i, Pr_ProcedureName_vc FROM tbl_ProcedureList WHERE Pr_ProcedureName_vc LIKE @Search OR CAST(Pr_ProcedureID_i AS VARCHAR) LIKE @Search ORDER BY Pr_ProcedureName_vc"
                frm.ReturnColumn = "Pr_ProcedureName_vc"
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Value = frm.SelectedValue
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Tag = frm.SelectedCode
                    ProceCode = frm.SelectedCode
                End If
            End If
            If displayName = "Molecule" Then
                Dim CatChk As String = "MolID1"
                Dim frm As New SubFrmCommonSearch
                frm.CatChk = "MolID1"
                frm.MolIDQuery = "SELECT Mo_Code_i, Mo_Name_vc FROM tbl_Molecule WHERE Mo_Name_vc LIKE @Search OR CAST(Mo_Code_i AS VARCHAR) LIKE @Search ORDER BY Mo_Name_vc"
                frm.ReturnColumn = "Mo_Name_vc"
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Value = frm.SelectedValue
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Tag = frm.SelectedCode
                    MolCode = frm.SelectedCode
                End If
            End If
            If displayName = "Company Id" Then
                Dim CatChk As String = "CompanyId1"
                Dim frm As New SubFrmCommonSearch
                frm.CatChk = "CompanyId1"
                frm.CompanyIdQuery = "SELECT SC_CompID_i, SC_CompName_vc FROM tbl_SupCompany WHERE SC_CompName_vc LIKE @Search OR CAST(SC_CompID_i AS VARCHAR) LIKE @Search ORDER BY SC_CompName_vc"
                frm.ReturnColumn = "SC_CompName_vc"
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Value = frm.SelectedValue
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Tag = frm.SelectedCode
                    CmpCode = frm.SelectedCode
                End If
            End If
            If displayName = "Unit" Then
                Dim CatChk As String = "Unit1"
                Dim frm As New SubFrmCommonSearch
                frm.CatChk = "Unit1"
                frm.UnitQuery = "SELECT U_RecordNo_i, U_EName_vc FROM tbl_UnitList WHERE U_EName_vc LIKE @Search OR CAST(U_RecordNo_i AS VARCHAR) LIKE @Search ORDER BY U_EName_vc"
                frm.ReturnColumn = "U_EName_vc"
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Value = frm.SelectedValue
                    DgvStockDtl.Rows(e.RowIndex).Cells(1).Tag = frm.SelectedCode
                    CmpCode = frm.SelectedCode
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub DgvMultiSt_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvMultiSt.CellDoubleClick
        Try
            If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
            If DgvMultiSt.Rows(e.RowIndex).IsNewRow Then Exit Sub
            Dim DisplayName As String = DgvMultiSt.Columns(e.ColumnIndex).HeaderText.Trim()

            If DisplayName.Equals("Supplier Name", StringComparison.OrdinalIgnoreCase) Then
                Dim Frm As New SubFrmCommonSearch
                Frm.CatChk = "SupplierName1"
                Frm.SupplierNameQuery = "SELECT C_ID_n, C_Name_vc FROM tbl_CusProfile WHERE C_Type_vc = 'SUPPLIER' AND (C_Name_vc LIKE @Search OR CAST(C_ID_n AS VARCHAR(20)) LIKE @Search) ORDER BY C_Name_vc, C_ID_n"
                Frm.ReturnColumn = "C_Name_vc"
                If Frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DgvMultiSt.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Frm.SelectedValue
                    DgvMultiSt.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = Frm.SelectedCode
                    DgvMultiSt.CurrentCell = DgvMultiSt.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    SupCode = Frm.SelectedCode
                End If
                Frm.Dispose()
            ElseIf DisplayName.Equals("Pharma Name", StringComparison.OrdinalIgnoreCase) Then
                Dim Frm As New SubFrmCommonSearch
                Frm.CatChk = "PharmaName1"
                Frm.PharmaNameQuery = "SELECT Pharma_Code_n, Pharma_Name_vc FROM tbl_Pharma WHERE Pharma_Name_vc LIKE @Search OR CAST(Pharma_Code_n AS VARCHAR(20)) LIKE @Search ORDER BY Pharma_Name_vc, Pharma_Code_n"
                Frm.ReturnColumn = "Pharma_Name_vc"
                If Frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DgvMultiSt.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Frm.SelectedValue
                    DgvMultiSt.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = Frm.SelectedCode
                    DgvMultiSt.CurrentCell = DgvMultiSt.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    PharCode = Frm.SelectedCode
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Multi Stock", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GetGridValue(ByVal fieldName As String) As Object
        For Each row As DataGridViewRow In DgvStockDtl.Rows
            If Not row.IsNewRow Then
                If row.Cells(0).Value.ToString() = fieldName Then
                    Return row.Cells(1).Value
                End If
            End If
        Next
        Return DBNull.Value
    End Function
    Private Function GetGridMultiValue(ByVal fieldName As String) As Object
        For Each row As DataGridViewRow In DgvMultiSt.Rows
            If Not row.IsNewRow Then
                If row.Cells(0).Value.ToString() = fieldName Then
                    Return row.Cells(1).Value
                End If
            End If
        Next

        Return DBNull.Value
    End Function
    Private Sub DgvStockDtl_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvStockDtl.CellClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            Dim displayName As String = ""
            Dim CellValue As String = ""
            If DgvStockDtl.Rows(e.RowIndex).Cells(0).Value IsNot Nothing Then
                displayName = DgvStockDtl.Rows(e.RowIndex).Cells(0).Value.ToString()
            End If
            If DgvStockDtl.Rows(e.RowIndex).Cells(1).Value IsNot Nothing Then
                CellValue = DgvStockDtl.Rows(e.RowIndex).Cells(1).Value.ToString()
            End If
            If displayName = "" AndAlso CellValue = "SAVE" Then
                Me.SaveStock()
            ElseIf displayName = "" AndAlso CellValue = "DELETE" Then
                'Me.SaveForeigner()
            ElseIf displayName = "" AndAlso CellValue = "CLOSE" Then
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub DgvStockDtl_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvStockDtl.SelectionChanged
        If DgvStockDtl.CurrentCell Is Nothing Then Exit Sub
        If DgvStockDtl.CurrentCell.ColumnIndex = 0 Then
            RemoveHandler DgvStockDtl.SelectionChanged, AddressOf DgvStockDtl_SelectionChanged
            DgvStockDtl.CurrentCell = DgvStockDtl.Rows(DgvStockDtl.CurrentCell.RowIndex).Cells(1)
            AddHandler DgvStockDtl.SelectionChanged, AddressOf DgvStockDtl_SelectionChanged
        End If
    End Sub
    Private Sub DgvStockDtl_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DgvStockDtl.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True

            Dim r As Integer = DgvStockDtl.CurrentCell.RowIndex
            If r < DgvStockDtl.Rows.Count - 1 Then
                DgvStockDtl.CurrentCell = DgvStockDtl.Rows(r + 1).Cells(1)
            End If
        End If
    End Sub
    Private Sub DgvStockDtl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles DgvStockDtl.Enter
        If DgvStockDtl.CurrentCell Is Nothing Then
            DgvStockDtl.CurrentCell = DgvStockDtl.Rows(0).Cells(1)
        ElseIf DgvStockDtl.CurrentCell.ColumnIndex = 0 Then
            DgvStockDtl.CurrentCell = DgvStockDtl.Rows(DgvStockDtl.CurrentCell.RowIndex).Cells(1)
        End If
    End Sub
    Private Sub SetDatePickerPosition(ByVal dtp As DateTimePicker)
        If dtp Is Nothing Then Exit Sub
        Dim RowIndex As Integer = -1
        If Not Integer.TryParse(dtp.AccessibleDescription, RowIndex) Then
            dtp.Visible = False
            Exit Sub
        End If
        If RowIndex < 0 OrElse
           RowIndex >= DgvStockDtl.Rows.Count Then
            dtp.Visible = False
            Exit Sub
        End If
        If Not DgvStockDtl.Rows(RowIndex).Displayed Then
            dtp.Visible = False
            Exit Sub
        End If
        Dim rect As Rectangle = DgvStockDtl.GetCellDisplayRectangle(1, RowIndex, False)
        Dim TopLimit As Integer = 0
        If DgvStockDtl.ColumnHeadersVisible Then
            TopLimit = DgvStockDtl.ColumnHeadersHeight
        End If
        If rect.Top < TopLimit OrElse
           rect.Bottom > DgvStockDtl.ClientSize.Height OrElse
           rect.Left < 0 OrElse
           rect.Right > DgvStockDtl.ClientSize.Width Then
            dtp.Visible = False
            Exit Sub
        End If
        dtp.Location = New Point(rect.X + 1, rect.Y + 1)
        dtp.Size = New Size(rect.Width - 2, rect.Height - 2)
        dtp.Visible = True
        dtp.BringToFront()
    End Sub
    Private Sub UpdateAllDatePickerPositions()
        For Each ctrl As Control In DgvStockDtl.Controls
            If TypeOf ctrl Is DateTimePicker Then
                Dim dtp As DateTimePicker =
                   DirectCast(ctrl, DateTimePicker)
                If dtp.Name.StartsWith("dtp_") Then
                    SetDatePickerPosition(dtp)
                End If
            End If
        Next
    End Sub
    Private Sub DgvStockDtl_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs) Handles DgvStockDtl.Scroll
        UpdateAllDatePickerPositions()
    End Sub
    Private Sub DgvStockDtl_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvStockDtl.SizeChanged
        UpdateAllDatePickerPositions()
    End Sub
    Private Sub DgvStockDtl_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs) Handles DgvStockDtl.ColumnWidthChanged
        UpdateAllDatePickerPositions()
    End Sub
    Private Sub DgvStockDtl_RowHeightChanged(ByVal sender As Object, ByVal e As DataGridViewRowEventArgs) Handles DgvStockDtl.RowHeightChanged
        UpdateAllDatePickerPositions()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.SaveStock()
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose1.Click
        Me.Close()
    End Sub
    'Private Sub Btn8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn8.Click
    '    Dim frm As New SubFrmColOrderCommon
    '    frm.ShowDialog()
    'End Sub
    Private Sub BtnStReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStReport.Click
        DgvLength.Hide()

        Dim frm As New SubRptStockReport
        frm.Owner = Me
        frm.ShowDialog()
    End Sub
    Public Sub LoadStock(ByVal StockNo As String)
        Try
            If Trim(StockNo) = "" Then Exit Sub
            SelectedStockID = Trim(StockNo)
            AddChk = False

            LoadStockEdit()
            LoadMultiStockEdit()
            UpdateLeftStockTotal()
            LoadImagesToGrid()

            DgvStockDtl.Columns(0).DefaultCellStyle.BackColor = Color.Khaki
            Panel1.BackColor = Color.Khaki
            Panel5.BackColor = Color.Khaki
            BtnQtyFocus.BackColor = Color.Khaki
            BtnDgvFocus.BackColor = Color.Khaki
            BtnStReport.BackColor = Color.Khaki
            btnSave.BackColor = Color.Khaki
            BtnClear.BackColor = Color.Khaki
            BtnDashboard.BackColor = Color.Khaki
            BtnPharChange.BackColor = Color.Khaki
            BtnSettings.BackColor = Color.Khaki
            BtnDel.BackColor = Color.Khaki
            BtnClose1.BackColor = Color.Khaki
            BtnColOrd.BackColor = Color.Khaki

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub LoadStockEdit()
        AddChk = False
        DgvStockDtl.SuspendLayout()
        Try
            If Trim(SelectedStockID) = "" Then Exit Sub
            DgvStockDtl.Rows.Clear()
            For i As Integer = DgvStockDtl.Controls.Count - 1 To 0 Step -1
                Dim ctl As Control = DgvStockDtl.Controls(i)
                If TypeOf ctl Is DateTimePicker Then
                    If ctl.Name.StartsWith("dtp_") Then
                        DgvStockDtl.Controls.RemoveAt(i)
                        ctl.Dispose()
                    End If
                End If
            Next

            Dim dtDesign As New DataTable
            Using da As New SqlDataAdapter("SELECT * FROM tbl_DgvEntry_Stock WHERE BD_ColShow_vc='YES' ORDER BY BD_ColOrderby_i", VistawinFunctions.ServerConnection)
                da.Fill(dtDesign)
            End Using
            Dim TableName As String = ""
            If VistawinFunctions.ProjectTypechk = "INVEN" Then
                TableName = "tbl_StockDtl"
            ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then
                TableName = "tbl_Stock"
            Else
                MsgBox("Invalid Project Type")
                Exit Sub
            End If

            Dim dtStock As New DataTable
            Using da As New SqlDataAdapter("SELECT * FROM " & TableName & " WHERE St_StockID_n=@StockID", VistawinFunctions.ServerConnection)
                da.SelectCommand.Parameters.AddWithValue("@StockID", SelectedStockID)
                da.Fill(dtStock)
            End Using

            If dtStock.Rows.Count = 0 Then
                MsgBox("Stock Record Not Found")
                Exit Sub
            End If
            Dim StockRow As DataRow = dtStock.Rows(0)
            For Each drDesign As DataRow In dtDesign.Rows
                Dim ColumnName As String = Trim(Convert.ToString(drDesign("BD_ColumnName_vc")))
                Dim DisplayName As String = Trim(Convert.ToString(drDesign("BD_AliasName_vc")))
                Dim ControlType As String = Trim(Convert.ToString(drDesign("BD_ControlType_vc"))).ToUpper()
                Dim FormatType As String = Trim(Convert.ToString(drDesign("BD_Format_vc")))
                If ColumnName = "" Then Continue For
                If DisplayName = "" Then Continue For

                If Not dtStock.Columns.Contains(ColumnName) Then
                    Continue For
                End If
                Dim DbValue As Object = StockRow(ColumnName)

                Dim Cell As DataGridViewCell = Nothing
                Select Case ControlType
                    Case "COMBO"
                        Dim cmb As New DataGridViewComboBoxCell()
                        Dim ComboSql As String = _
                            Trim(Convert.ToString( _
                            drDesign("BD_ComboSQL_vc")))
                        If ComboSql <> "" Then
                            Dim dtCombo As New DataTable
                            Using daCombo As New SqlDataAdapter(ComboSql, VistawinFunctions.ServerConnection)
                                daCombo.Fill(dtCombo)
                            End Using
                            For Each cr As DataRow In dtCombo.Rows
                                If Not IsDBNull(cr(0)) Then
                                    Dim ItemValue As String = Convert.ToString(cr(0))
                                    If Not cmb.Items.Contains(ItemValue) Then
                                        cmb.Items.Add(ItemValue)
                                    End If
                                End If
                            Next
                        End If
                        Cell = cmb

                    Case "CHECKBOX"
                        Cell = New DataGridViewCheckBoxCell()
                    Case "IMAGE"
                        Dim img As New DataGridViewImageCell()
                        img.ImageLayout = DataGridViewImageCellLayout.Zoom
                        Cell = img

                    Case Else
                        Cell = New DataGridViewTextBoxCell()
                End Select
                Dim RowIndex As Integer = DgvStockDtl.Rows.Add()
                DgvStockDtl.Rows(RowIndex).Cells(0).Value = DisplayName
                DgvStockDtl.Rows(RowIndex).Cells(1) = Cell
                DgvStockDtl.Rows(RowIndex).Tag = ColumnName

                If ControlType = "TEXTBOX" OrElse _
                   ControlType = "MULTILINE" OrElse _
                   ControlType = "" Then
                    If IsDBNull(DbValue) Then
                        Cell.Value = ""
                    Else
                        If DisplayName.ToUpper() = "CATEGORY" Then
                            Cell.Tag = DbValue
                            Dim NameValue As String = ""
                            Using cmd As New SqlCommand("SELECT ISNULL(Category_Name_vc,'') FROM tbl_Category WHERE Category_Code_n=@ID", _
                                VistawinFunctions.ServerConnection)
                                cmd.Parameters.AddWithValue("@ID", DbValue)
                                NameValue = Convert.ToString(cmd.ExecuteScalar())
                            End Using
                            Cell.Value = NameValue
                        ElseIf DisplayName.ToUpper() = "BRAND" Then
                            Cell.Tag = DbValue
                            Dim NameValue As String = ""
                            Using cmd As New SqlCommand("SELECT ISNULL(Brand_Name_vc,'') FROM tbl_Brand WHERE Brand_Code_n=@ID", VistawinFunctions.ServerConnection)
                                cmd.Parameters.AddWithValue("@ID", DbValue)
                                NameValue = Convert.ToString(cmd.ExecuteScalar())
                            End Using
                            Cell.Value = NameValue
                        ElseIf DisplayName.ToUpper() = "MOLECULE" Then
                            Cell.Tag = DbValue
                            Dim NameValue As String = ""
                            Using cmd As New SqlCommand("SELECT ISNULL(Mo_Name_vc,'') FROM tbl_Molecule WHERE Mo_Code_i=@ID", VistawinFunctions.ServerConnection)
                                cmd.Parameters.AddWithValue("@ID", DbValue)
                                NameValue = Convert.ToString(cmd.ExecuteScalar())
                            End Using
                            Cell.Value = NameValue
                        ElseIf DisplayName.ToUpper() = "HOW TO USE" Then
                            Cell.Tag = DbValue
                            Dim NameValue As String = ""
                            Using cmd As New SqlCommand("SELECT ISNULL(Pr_ProcedureName_vc,'') FROM tbl_ProcedureList WHERE Pr_ProcedureID_i=@ID", VistawinFunctions.ServerConnection)
                                cmd.Parameters.AddWithValue("@ID", DbValue)
                                NameValue = Convert.ToString(cmd.ExecuteScalar())
                            End Using
                            Cell.Value = NameValue
                        ElseIf DisplayName.ToUpper() = "COMPANY ID" Then
                            Cell.Tag = DbValue
                            Dim NameValue As String = ""
                            Using cmd As New SqlCommand("SELECT ISNULL(SC_CompName_vc,'') FROM tbl_SupCompany WHERE SC_CompID_i=@ID", VistawinFunctions.ServerConnection)
                                cmd.Parameters.AddWithValue("@ID", DbValue)
                                NameValue = Convert.ToString(cmd.ExecuteScalar())
                            End Using
                            Cell.Value = NameValue
                        Else
                            Cell.Value = DbValue
                        End If
                    End If
                ElseIf ControlType = "COMBO" Then
                    If Not IsDBNull(DbValue) Then
                        Dim cmb As DataGridViewComboBoxCell = DirectCast(Cell, DataGridViewComboBoxCell)
                        Dim ValueText As String = Convert.ToString(DbValue)
                        If Not cmb.Items.Contains(ValueText) Then
                            cmb.Items.Add(ValueText)
                        End If
                        cmb.Value = ValueText
                    End If
                ElseIf ControlType = "CHECKBOX" Then
                    Dim ValueText As String = Trim(Convert.ToString(DbValue)).ToUpper()
                    Cell.Value = (ValueText = "TRUE" OrElse ValueText = "YES" OrElse ValueText = "1")
                ElseIf ControlType = "IMAGE" Then
                    Cell.Value = Nothing
                End If
                If ControlType = "DATE" Then
                    Dim dtp As New DateTimePicker()
                    dtp.Name = "dtp_" & ColumnName

                    dtp.Tag = DisplayName
                    dtp.AccessibleDescription = RowIndex.ToString()
                    dtp.Format = DateTimePickerFormat.Custom

                    If FormatType = "" Then
                        FormatType = "dd/MM/yyyy"
                    End If
                    dtp.CustomFormat = FormatType
                    If Not IsDBNull(DbValue) AndAlso IsDate(DbValue) Then
                        dtp.Value = CDate(DbValue)
                        Cell.Value = dtp.Value.ToString(FormatType)
                    Else
                        dtp.Value = Date.Today
                        Cell.Value = ""
                    End If
                    DgvStockDtl.Controls.Add(dtp)
                    SetDatePickerPosition(dtp)
                    dtp.BringToFront()

                    AddHandler dtp.ValueChanged, AddressOf DatePicker_ValueChanged
                End If

                Dim IsReadOnly As Boolean = _
                    (Trim(Convert.ToString( _
                    drDesign("BD_ReadOnly_vc"))).ToUpper() = "YES")

                Cell.ReadOnly = IsReadOnly

                If ColumnName.ToUpper() = "ST_STOCKID_N" OrElse DisplayName.ToUpper() = "STOCK ID" Then
                    Cell.ReadOnly = True
                    Cell.Style.BackColor = Color.LightGray
                End If
                Dim BackColorName As String = Trim(Convert.ToString(drDesign("BD_BackColor_vc")))
                If BackColorName <> "" AndAlso DisplayName.ToUpper() <> "STOCK ID" Then
                    Cell.Style.BackColor = Color.FromName(BackColorName)
                End If
                Dim ForeColorName As String = Trim(Convert.ToString(drDesign("BD_ForeColor_vc")))
                If ForeColorName <> "" Then
                    Cell.Style.ForeColor = Color.FromName(ForeColorName)
                End If
                If IsNumeric(drDesign("BD_Height_i")) Then
                    Dim H As Integer = CInt(drDesign("BD_Height_i"))
                    If H > 0 Then
                        DgvStockDtl.Rows(RowIndex).Height = H
                    End If
                End If
            Next
            DgvStockDtl.ClearSelection()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Stock Edit", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DgvStockDtl.ResumeLayout()
        End Try
    End Sub
    Private Sub CbShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbShowAll.CheckedChanged

        LoadMultiStockEdit()

    End Sub
    Private Sub LoadMultiStockEdit()
        AddChk = False
        DgvMultiSt.SuspendLayout()
        Try
            If Trim(SelectedStockID) = "" Then Exit Sub
            DgvMultiSt.Rows.Clear()
            DgvMultiSt.Columns.Clear()
            DgvMultiSt.AllowUserToAddRows = False
            DgvMultiSt.AutoGenerateColumns = False

            Dim dtDesign As New DataTable
            Using da As New SqlDataAdapter("SELECT * FROM tbl_DgvEntry_MultiStock WHERE BD_ColShow_vc='YES' ORDER BY BD_ColOrderby_i", VistawinFunctions.ServerConnection)
                da.Fill(dtDesign)
            End Using

            Dim dtStock As New DataTable
            Dim Sql As String = ""

            Sql = "SELECT * FROM tbl_MultiStock " &
                  "WHERE St_StockID_n=@StockID "

            If CbShowAll.Checked = True Then
                Sql = Sql & "AND St_Show_vc='YES' "
            End If

            Sql = Sql & "ORDER BY St_StNumber_i"

            Using da As New SqlDataAdapter(Sql, VistawinFunctions.ServerConnection)

                da.SelectCommand.Parameters.AddWithValue("@StockID",
                                                         Convert.ToDecimal(SelectedStockID))

                da.Fill(dtStock)

            End Using

            Dim SnoCol As New DataGridViewTextBoxColumn()
            SnoCol.Name = "SNo"
            SnoCol.HeaderText = "S.No"
            SnoCol.Width = 50
            SnoCol.ReadOnly = True
            SnoCol.SortMode = DataGridViewColumnSortMode.NotSortable
            SnoCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DgvMultiSt.Columns.Add(SnoCol)

            Dim DeleteCol As New DataGridViewButtonColumn
            DeleteCol.Name = "Delete"
            DeleteCol.HeaderText = "Delete"
            DeleteCol.Text = "Delete"
            DeleteCol.UseColumnTextForButtonValue = True
            DeleteCol.Width = 60
            DeleteCol.SortMode = DataGridViewColumnSortMode.NotSortable
            DgvMultiSt.Columns.Add(DeleteCol)

            For Each dr As DataRow In dtDesign.Rows
                Dim AliasName As String = Trim(Convert.ToString(dr("BD_AliasName_vc")))
                Dim ControlType As String = Trim(Convert.ToString(dr("BD_ControlType_vc"))).ToUpper()
                If AliasName = "" Then Continue For
                Dim DbColumn As String = GetMultiStockDbColumn(AliasName)
                If DbColumn = "" Then Continue For
                If Not dtStock.Columns.Contains(DbColumn) Then
                    Continue For
                End If

                Dim Col As DataGridViewColumn = Nothing

                Select Case ControlType
                    Case "CHECKBOX"
                        Dim chk As New DataGridViewCheckBoxColumn()
                        chk.TrueValue = True
                        chk.FalseValue = False
                        Col = chk
                    Case "COMBO"

                        Dim cmb As New DataGridViewComboBoxColumn()

                        cmb.DisplayStyle =
                            DataGridViewComboBoxDisplayStyle.DropDownButton

                        cmb.FlatStyle = FlatStyle.Flat

                        Dim ComboSql As String =
                            Trim(Convert.ToString(dr("BD_ComboSQL_vc")))

                        If ComboSql <> "" Then

                            Dim dtCombo As New DataTable

                            Using daCombo As New SqlDataAdapter(
                                ComboSql,
                                VistawinFunctions.ServerConnection)

                                daCombo.Fill(dtCombo)

                            End Using

                            If dtCombo.Columns.Count >= 2 Then

                                cmb.DataSource = dtCombo

                                cmb.ValueMember =
                                    dtCombo.Columns(0).ColumnName

                                cmb.DisplayMember =
                                    dtCombo.Columns(1).ColumnName

                            ElseIf dtCombo.Columns.Count = 1 Then

                                For Each cr As DataRow In dtCombo.Rows

                                    If Not IsDBNull(cr(0)) Then

                                        Dim ItemValue As String =
                                            Convert.ToString(cr(0))

                                        If Not cmb.Items.Contains(ItemValue) Then
                                            cmb.Items.Add(ItemValue)
                                        End If

                                    End If

                                Next

                            End If

                        End If

                        Col = cmb
                    Case Else
                        Col = New DataGridViewTextBoxColumn()
                End Select

                Col.Name = AliasName
                Col.HeaderText = AliasName
                Col.Tag = DbColumn

                If IsNumeric(dr("BD_Width_i")) Then
                    Dim W As Integer = CInt(dr("BD_Width_i"))
                    If W > 0 Then
                        Col.Width = W
                    End If
                End If
                Dim BackColorName As String = Trim(Convert.ToString(dr("BD_BackColor_vc")))
                If BackColorName <> "" Then
                    Col.DefaultCellStyle.BackColor = Color.FromName(BackColorName)
                End If
                Dim ForeColorName As String = Trim(Convert.ToString(dr("BD_ForeColor_vc")))
                If ForeColorName <> "" Then
                    Col.DefaultCellStyle.ForeColor = Color.FromName(ForeColorName)
                End If
                Dim AlignValue As String = Trim(Convert.ToString(dr("BD_Alignment_vc"))).ToUpper()

                Select Case AlignValue
                    Case "LEFT"
                        Col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Case "CENTER"
                        Col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    Case "RIGHT"
                        Col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select

                Select Case AliasName.ToUpper()
                    Case "PURRATE", "SALERATE", "SALERATE2", "MRP", "MARGIN1", "MARGIN2", "OLD MRP RATE", "OLD SALE RATE", "MRP LESS(%)", "MRP LESS AMOUNT", "DISC(%)"
                        Col.DefaultCellStyle.Format = "N2"
                    Case "GODOWN STOCK", "MEDICAL STOCK", "SHOP STOCK", "CURRSTOCK", "TOTAL STOCK"
                        Col.DefaultCellStyle.Format = "N3"
                    Case Else
                        Col.DefaultCellStyle.Format = ""
                End Select

                Dim IsReadOnly As Boolean = False
                If dtDesign.Columns.Contains("BD_ReadOnly_vc") Then
                    IsReadOnly = (Trim(Convert.ToString(dr("BD_ReadOnly_vc"))).ToUpper() = "YES")
                End If
                Col.ReadOnly = IsReadOnly
                If AliasName.ToUpper() = "STNUMBER" Then
                    Col.ReadOnly = True
                    Col.DefaultCellStyle.BackColor = Color.LightGray
                End If
                Col.SortMode = DataGridViewColumnSortMode.NotSortable
                DgvMultiSt.Columns.Add(Col)
            Next


            '=========================================================
            ' LOAD PHARMA MASTER
            ' Code = Pharma_Code_n
            ' Name = Pharma_Name_vc
            '=========================================================
            Dim dtPharma As New DataTable

            Using daPharma As New SqlDataAdapter( _
                "SELECT Pharma_Code_n, Pharma_Name_vc " & _
                "FROM tbl_Pharma", _
                VistawinFunctions.ServerConnection)

                daPharma.Fill(dtPharma)

            End Using

            '=========================================================
            ' LOAD SUPPLIER MASTER
            ' Code = C_ID_n
            ' Name = C_Name_vc
            '=========================================================
            Dim dtSupplier As New DataTable

            Using daSupplier As New SqlDataAdapter( _
                "SELECT C_ID_n, C_Name_vc " & _
                "FROM tbl_CusProfile " & _
                "WHERE C_Type_vc='SUPPLIER'", _
                VistawinFunctions.ServerConnection)

                daSupplier.Fill(dtSupplier)

            End Using

            Dim SNo As Integer = 1
            For Each StockRow As DataRow In dtStock.Rows
                Dim RowIndex As Integer = DgvMultiSt.Rows.Add()
                Dim GridRow As DataGridViewRow = DgvMultiSt.Rows(RowIndex)
                GridRow.Cells("SNo").Value = SNo
                For Each Col As DataGridViewColumn In DgvMultiSt.Columns
                    If Col.Name = "SNo" Then Continue For
                    If Col.Tag Is Nothing Then Continue For
                    Dim DbColumn As String = Trim(Convert.ToString(Col.Tag))
                    If DbColumn = "" Then Continue For
                    If Not dtStock.Columns.Contains(DbColumn) Then
                        Continue For
                    End If
                    Dim DbValue As Object = StockRow(DbColumn)
                    If IsDBNull(DbValue) Then
                        GridRow.Cells(Col.Name).Value = Nothing
                        Continue For
                    End If

                    If TypeOf GridRow.Cells(Col.Name) Is DataGridViewComboBoxCell Then

                        Dim ValueText As String =
                            Convert.ToString(DbValue).Trim()

                        Dim ComboCell As DataGridViewComboBoxCell =
                            DirectCast(GridRow.Cells(Col.Name), 
                                       DataGridViewComboBoxCell)

                        Try
                            ComboCell.Value = DbValue
                        Catch
                            ComboCell.Value = ValueText
                        End Try


                    ElseIf TypeOf GridRow.Cells(Col.Name) Is DataGridViewCheckBoxCell Then

                        Dim ValueText As String =
                            Trim(Convert.ToString(DbValue)).ToUpper()

                        GridRow.Cells(Col.Name).Value =
                            (ValueText = "YES" OrElse
                             ValueText = "TRUE" OrElse
                             ValueText = "1")


                    Else

                        If Col.Name.ToUpper() = "PHARMA NAME" Then

                            Dim PharmaCode As String = Convert.ToString(DbValue).Trim()
                            Dim PharmaName As String = ""

                            For Each PharmaRow As DataRow In dtPharma.Rows
                                If Not IsDBNull(PharmaRow("Pharma_Code_n")) Then
                                    If Convert.ToString(PharmaRow("Pharma_Code_n")).Trim() = PharmaCode Then
                                        If Not IsDBNull(PharmaRow("Pharma_Name_vc")) Then
                                            PharmaName = Convert.ToString(PharmaRow("Pharma_Name_vc")).Trim()
                                        End If
                                        Exit For
                                    End If
                                End If
                            Next
                            GridRow.Cells(Col.Name).Tag = DbValue
                            PharCode = PharmaCode

                            If PharmaName <> "" Then
                                GridRow.Cells(Col.Name).Value = PharmaName
                            Else
                                GridRow.Cells(Col.Name).Value = DbValue
                            End If


                        ElseIf Col.Name.ToUpper() = "SUPPLIER NAME" Then

                            '=====================================================
                            ' SUPPLIER NAME
                            ' Database : St_SupID_i
                            ' Display  : C_Name_vc
                            '=====================================================

                            Dim SupplierID As String =
                                Convert.ToString(DbValue).Trim()

                            Dim SupplierName As String = ""

                            For Each SupplierRow As DataRow In dtSupplier.Rows

                                If Not IsDBNull(SupplierRow("C_ID_n")) Then

                                    If Convert.ToString(
                                        SupplierRow("C_ID_n")).Trim() =
                                        SupplierID Then

                                        If Not IsDBNull(
                                            SupplierRow("C_Name_vc")) Then

                                            SupplierName =
                                                Convert.ToString(
                                                    SupplierRow("C_Name_vc")).Trim()

                                        End If

                                        Exit For

                                    End If

                                End If

                            Next

                            'Keep original Supplier ID
                            GridRow.Cells(Col.Name).Tag = DbValue
                            SupCode = SupplierID

                            'Display Supplier Name
                            If SupplierName <> "" Then
                                GridRow.Cells(Col.Name).Value = SupplierName
                            Else
                                GridRow.Cells(Col.Name).Value = DbValue
                            End If


                        Else

                            GridRow.Cells(Col.Name).Value = DbValue

                        End If

                    End If

                Next
                SNo += 1
            Next

            Dim NewRow As Integer = DgvMultiSt.Rows.Add()
            DgvMultiSt.Rows(NewRow).Cells("SNo").Value = DgvMultiSt.Rows.Count

            If DgvMultiSt.Rows.Count = 0 Then
                Dim R As Integer = DgvMultiSt.Rows.Add()
                DgvMultiSt.Rows(R).Cells("SNo").Value = 1
            End If

            With DgvMultiSt.ColumnHeadersDefaultCellStyle
                .BackColor = Color.Maroon
                .ForeColor = Color.White
                .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            DgvMultiSt.EnableHeadersVisualStyles = False
            DgvMultiSt.ClearSelection()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Multi Stock Edit", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DgvMultiSt.ResumeLayout()
        End Try
    End Sub
    Private Sub LoadImagesToGrid()
        Try
            Dim ProName As String = Trim(Convert.ToString(GetGridValue("Product Name")))
            Dim StockID As String = Trim(SelectedStockID)
            If StockID = "" Then
                StockID = Trim(Convert.ToString(GetGridValue("Stock ID")))
            End If
            If StockID = "" Then Exit Sub
            If ProName = "" Then Exit Sub
            Dim FileName As String = ProName & "_" & StockID & ".jpg"
            Dim Img As System.Drawing.Image = LoadImageFromFile(FileName)
            For Each row As DataGridViewRow In DgvStockDtl.Rows
                If row.IsNewRow Then Continue For
                If row.Cells.Count < 2 Then Continue For
                If row.Cells(0).Value Is Nothing Then Continue For
                If IsDBNull(row.Cells(0).Value) Then Continue For
                Dim FieldName As String = Trim(Convert.ToString(row.Cells(0).Value))
                If FieldName.ToUpper() = "PRODUCT IMAGE" Then
                    If Not TypeOf row.Cells(1) Is DataGridViewImageCell Then
                        Dim ImgCell As New DataGridViewImageCell()
                        ImgCell.ImageLayout = DataGridViewImageCellLayout.Zoom
                        row.Cells(1) = ImgCell
                    End If
                    If Img IsNot Nothing Then
                        row.Cells(1).Value = New Bitmap(Img)
                        DirectCast(row.Cells(1), DataGridViewImageCell).ImageLayout = DataGridViewImageCellLayout.Zoom
                        row.Height = 100
                    Else
                        row.Cells(1).Value = Nothing
                        row.Height = 25
                    End If
                    Exit For
                End If
            Next
            If Img IsNot Nothing Then
                Img.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show( _
                ex.Message, _
                "Load Product Image", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DatePicker_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dtp As DateTimePicker = DirectCast(sender, DateTimePicker)
            Dim RowIndex As Integer = -1
            If Not Integer.TryParse(dtp.AccessibleDescription, RowIndex) Then Exit Sub
            If RowIndex < 0 OrElse RowIndex >= DgvStockDtl.Rows.Count Then Exit Sub
            DgvStockDtl.Rows(RowIndex).Cells(1).Value = dtp.Text
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function GetDesignDbColumn(ByVal dt As DataTable, ByVal drDesign As DataRow) As String
        Dim SqlColumn As String = Trim(Convert.ToString(drDesign("BD_SQLColumn_vc")))
        Dim ColumnName As String = Trim(Convert.ToString(drDesign("BD_ColumnName_vc")))
        If SqlColumn <> "" AndAlso dt.Columns.Contains(SqlColumn) Then
            Return SqlColumn
        End If
        If ColumnName <> "" AndAlso dt.Columns.Contains(ColumnName) Then
            Return ColumnName
        End If
        Return ""
    End Function
    Private Function GetMultiStockDbColumn( ByVal AliasName As String) As String
        Select Case Trim(AliasName).ToUpper()
            Case "STNUMBER"
                Return "St_StNumber_i"
            Case "PURRATE"
                Return "St_PurRate_n"
            Case "MARGIN1"
                Return "St_MarPer1_n"
            Case "SALERATE"
                Return "St_SaleRate_n"
            Case "WHOLESALERATE"
                Return "St_SaleRate2_n"
            Case "MARGIN2"
                Return "St_MarPer2_n"
            Case "MRP"
                Return "St_MRPRate_n"
            Case "CURRSTOCK"
                'Case "CURRSTOCK" Or "TOTALSTOCK"
                Return "St_CurrStock_i"
            Case "SHOW"
                Return "St_Show_vc"
            Case "BATCHNO"
                Return "St_BatchNo_vc"
            Case "MONTH"
                'Case "EMONTH" Or "MONTH"
                Return "St_EMonth_i"
            Case "YEAR"
                'Case "EYEAR" Or "YEAR"
                Return "St_EYear_vc"
            Case "GODOWN STOCK"
                Return "St_GoCurSt_i"
            Case "MEDICAL STOCK"
                'Case "MEDICAL STOCK" Or "SHOP STOCK"
                Return "St_MediCurSt_i"
            Case "SUPPLIER NAME"
                'Case "SUPPLIER NAME" Or "SUPPLIER"
                Return "St_SupID_i"
            Case "PHARMA NAME"
                'Case "PHARMA NAME" Or "PHARMA"
                Return "St_PharID_i"
            Case "MFR"
                Return "St_MFR_vc"
            Case "BARCODE"
                Return "St_Barcode_vc"
            Case "PACK"
                'Case "PACKINT" Or "PACK"
                Return "St_Pack_i"
            Case "PACKS"
                Return "St_Packchar_vc"
                'Case "PACKCHAR" Or "PACKS"
                '    Return "St_Packchar_vc"
            Case "OLD MRP RATE"
                Return "Old_MRPRate_n"
            Case "OLD SALE RATE"
                Return "Old_SaleRate_n"
            Case "MRP LESS(%)"
                Return "MRP_LessPer_n"
            Case "MRP LESS AMOUNT"
                Return "MRP_LessAmt_n"
            Case "EXP STATUS"
                Return "St_ExpStatus_vc"
            Case "DISC(%)"
                Return "St_MulDisPer_n"
            Case "PRODUCT SIZE"
                Return "St_ProSize_vc"
            Case "PRODUCT COLOUR"
                Return "St_ProColor_vc"
            Case "MODEL NAME"
                Return "St_ModelName_vc"
            Case "NOTEINT"
                Return "St_NoteInt_i"
            Case "NOTENUM"
                Return "St_NoteNum_n"
            Case "NOTECHAR"
                Return "St_NoteChar_vc"
            Case Else
                Return ""
        End Select
    End Function
    Private Function GetDecimalCellValue(ByVal Dr As DataGridViewRow,ByVal ColumnName As String) As Decimal
        Dim Result As Decimal = 0D
        If Not DgvMultiSt.Columns.Contains(ColumnName) Then
            Return 0D
        End If
        Dim CellValue As Object = Dr.Cells(ColumnName).Value
        If CellValue Is Nothing OrElse
           CellValue Is DBNull.Value OrElse
           CellValue.ToString().Trim() = "" Then
            Return 0D
        End If
        Decimal.TryParse(CellValue.ToString(), Result)
        Return Result
    End Function
    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDel.Click
        StDelete()
    End Sub
    Private Sub StDelete()
        Try
            Execute = True
            Trans = VistawinFunctions.ServerConnection.BeginTransaction
            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_PurchParticular WHERE Sa_StCode_n=" & SelectedStockID & "", VistawinFunctions.ServerConnection, Trans)
            If Cmd.ExecuteScalar > 0 Then
                MsgBox("Cannot be Deleted !")
                Trans.Rollback()   'Added
                Exit Sub
            End If
            Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_BillParticular WHERE Sa_StCode_n=" & SelectedStockID & "", VistawinFunctions.ServerConnection, Trans)
            If Cmd.ExecuteScalar > 0 Then
                MsgBox("Cannot be Deleted !")
                Trans.Rollback()
                Exit Sub
            End If
            'Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_DamageParticular WHERE Sa_StCode_n=" & SelectedStockID & "", VistawinFunctions.ServerConnection, Trans)
            'If Cmd.ExecuteScalar > 0 Then
            '    MsgBox("Cannot be Deleted !")
            '    Trans.Rollback()
            '    Exit Sub
            'End If
            If MessageBox.Show("Are you sure to Delete ? ---> " & SelectedProName, "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then
                Trans.Rollback()
                Exit Sub
            End If
            If BVistawinSolution1.Permission() = False Then
                Trans.Rollback()
                Exit Sub
            End If
            Using command As New SqlCommand("SP_tbl_StockDelete", VistawinFunctions.ServerConnection, Trans)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@St_StockID_n", SelectedStockID)
                command.Parameters.AddWithValue("@Inputstring", "Delete")
                command.ExecuteNonQuery()
                Execute = True
            End Using
            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Deleted")
            End If
            Me.LoadEmptyDgv()
            Me.LoadStockAdd()
            Me.LoadEmptyMsDgv()
            Me.LoadMultiStockAdd()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            If Trans IsNot Nothing Then
                Trans.Rollback()
            End If
        Finally
            If Trans IsNot Nothing Then
                Trans.Dispose()
            End If
        End Try
    End Sub
    Private Sub DgvMultiStCellEndEdit(ByVal sender As Object,
                                     ByVal e As DataGridViewCellEventArgs) Handles DgvMultiSt.CellEndEdit

        If e.RowIndex < 0 Then Exit Sub

        Select Case VistawinFunctions.ProjectTypechk

            Case "INVEN"
                InvenDgvEdit(e.RowIndex)

            Case "CLINIC"
                ClinicDgvEdit(e.RowIndex)

        End Select

        EnsureEmptyMultiStockRow()
    End Sub
    Private Sub InvenDgvEdit(ByVal RowIndex As Integer)

        Try

            If RowIndex < 0 Then Exit Sub

            Dim Row As DataGridViewRow = DgvMultiSt.Rows(RowIndex)

            Dim GodownStock As Decimal = 0
            Dim MedicalStock As Decimal = 0
            Dim CurrentStock As Decimal = 0

            Dim PurValue As Decimal = 0
            Dim Mar1Rate As Decimal = 0
            Dim Mar2Rate As Decimal = 0

            Decimal.TryParse(Convert.ToString(Row.Cells("Godown Stock").Value),
                             GodownStock)

            Decimal.TryParse(Convert.ToString(Row.Cells("Medical Stock").Value),
                             MedicalStock)

            Decimal.TryParse(Convert.ToString(Row.Cells("PurRate").Value),
                             PurValue)

            Decimal.TryParse(Convert.ToString(Row.Cells("Margin1").Value),
                             Mar1Rate)

            Decimal.TryParse(Convert.ToString(Row.Cells("Margin2").Value),
                             Mar2Rate)


            '-----------------------------------------
            ' Current Stock
            '-----------------------------------------
            CurrentStock = GodownStock + MedicalStock

            Row.Cells("CurrStock").Value =
                CurrentStock.ToString("0.00")


            '-----------------------------------------
            ' Sale Rate 1
            '-----------------------------------------
            Dim SaleRate As Decimal =
                PurValue + (PurValue * Mar1Rate / 100)

            Row.Cells("SaleRate").Value =
                SaleRate.ToString("0.00")


            '-----------------------------------------
            ' Sale Rate 2
            '-----------------------------------------
            Dim SaleRate2 As Decimal =
                PurValue + (PurValue * Mar2Rate / 100)

            Row.Cells("WholeSaleRate").Value =
                SaleRate2.ToString("0.00")


            '-----------------------------------------
            ' Total Stock
            '-----------------------------------------
            UpdateLeftStockTotal()


        Catch ex As Exception

            MessageBox.Show(ex.Message,
                            "Stock Calculation",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub ClinicDgvEdit(ByVal RowIndex As Integer)

        Try

            If RowIndex < 0 Then Exit Sub

            Dim Row As DataGridViewRow =
                DgvMultiSt.Rows(RowIndex)

            Dim GodownStock As Decimal = 0
            Dim MedicalStock As Decimal = 0
            Dim CurrentStock As Decimal = 0

            Dim PackValue As Decimal = 0
            Dim MRPValue As Decimal = 0

            Dim PackCal As String = ""


            '-----------------------------------------
            ' Godown Stock
            '-----------------------------------------
            Decimal.TryParse(
                Convert.ToString(Row.Cells("Godown Stock").Value),
                GodownStock)


            '-----------------------------------------
            ' Medical Stock
            '-----------------------------------------
            Decimal.TryParse(
                Convert.ToString(Row.Cells("Medical Stock").Value),
                MedicalStock)


            '-----------------------------------------
            ' Pack
            '-----------------------------------------
            Decimal.TryParse(
                Convert.ToString(Row.Cells("Pack").Value),
                PackValue)


            '-----------------------------------------
            ' MRP
            '-----------------------------------------
            Decimal.TryParse(
                Convert.ToString(Row.Cells("MRP").Value),
                MRPValue)


            '-----------------------------------------
            ' Get Pack Cal = YES / NO
            ' DgvStockDtl:
            ' Field       Value
            ' Pack Cal    YES
            '-----------------------------------------
            For Each Dr As DataGridViewRow In DgvStockDtl.Rows

                If Dr.IsNewRow Then Continue For

                If Convert.ToString(Dr.Cells(0).Value).Trim().Equals(
                    "Pack Cal",
                    StringComparison.OrdinalIgnoreCase) Then

                    PackCal =
                        Convert.ToString(Dr.Cells(1).Value).Trim().ToUpper()

                    Exit For

                End If

            Next


            '-----------------------------------------
            ' Current Stock
            '-----------------------------------------
            CurrentStock =
                GodownStock + MedicalStock

            Row.Cells("CurrStock").Value =
                CurrentStock.ToString("0.00")


            '-----------------------------------------
            ' Sale Rate
            '-----------------------------------------
            If PackCal = "YES" AndAlso PackValue > 0 Then

                Dim SaleRate As Decimal =
                    MRPValue / PackValue

                Row.Cells("SaleRate").Value =
                    SaleRate.ToString("0.00")

            Else

                'Row.Cells("SaleRate").Value = "0.00"

            End If


            '-----------------------------------------
            ' Total Stock
            '-----------------------------------------
            UpdateLeftStockTotal()


        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "Stock Calculation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        End Try

    End Sub
  
    Private Sub SetGridValue(ByVal columnName As String, ByVal value As String)
        Try
            If DgvMultiSt.CurrentRow IsNot Nothing Then
                DgvMultiSt.CurrentRow.Cells(columnName).Value = value
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub UpdateLeftStockTotal()
        Try
            Dim TotalGodown As Decimal = 0
            Dim TotalMedical As Decimal = 0
            Dim TotalStock As Decimal = 0
            For Each Dr As DataGridViewRow In DgvMultiSt.Rows
                If Dr.IsNewRow Then Continue For
                Dim Godown As Decimal = 0
                Dim Medical As Decimal = 0
                Dim TotalSt As Decimal = 0
                Decimal.TryParse( Convert.ToString(Dr.Cells("Godown Stock").Value), Godown)
                Decimal.TryParse(Convert.ToString(Dr.Cells("Medical Stock").Value), Medical)
                Decimal.TryParse(Convert.ToString(Dr.Cells("CurrStock").Value), TotalSt)
                TotalGodown += Godown
                TotalMedical += Medical
                TotalStock += TotalSt

            Next

            SetLeftGridValue("Godown Stock", TotalGodown.ToString("0.00"))
            SetLeftGridValue("Medical Stock", TotalMedical.ToString("0.00"))
            SetLeftGridValue("Total Stock", TotalStock.ToString("0.00"))

            txtGodownStock.Text = TotalGodown.ToString("0.00")
            txtMediStock.Text = TotalMedical.ToString("0.00")
            txtTotalStock.Text = TotalStock.ToString("0.00")

            'SetLeftGridValue("Medical Stock", TotalMedical.ToString("0.00"))
            'SetLeftGridValue("Total Stock", TotalStock.ToString("0.00"))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub SetLeftGridValue(ByVal FieldName As String, ByVal Value As String)
        Try
            For Each Dr As DataGridViewRow In DgvStockDtl.Rows
                If Dr.IsNewRow Then Continue For
                If Trim(Convert.ToString(Dr.Cells("Field").Value)) = FieldName Then
                    Dr.Cells("Value").Value = Value
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Me.Clear()
    End Sub
    Private Sub Clear()
        Me.LoadEmptyDgv()
        Me.LoadEmptyMsDgv()
        DgvLength.Hide()
    End Sub
    Private Sub BtnDashboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDashboard.Click
        DgvLength.Show()
        DgvLength.BringToFront()
        DgvLength.Dock = DockStyle.Left
        Me.LengthDtl()
    End Sub
    Private Sub LengthDtl()
        DgvLength.Rows.Clear()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT TOP 1 St_ProName_vc AS 'ParticularMi',MIN(LEN(St_ProName_vc))AS 'LengthMi','Product.Min' AS 'ParHeadMi' FROM tbl_Stock GROUP BY St_ProName_vc ORDER BY LEN(St_ProName_vc) ASC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        Adapter = New SqlDataAdapter("SELECT TOP 1 St_ProName_vc AS 'ParticularMi',MAX(LEN(St_ProName_vc))AS 'LengthMi','Product.Max' AS 'ParHeadMi' FROM tbl_Stock GROUP BY St_ProName_vc ORDER BY LEN(St_ProName_vc) DESC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        Adapter = New SqlDataAdapter("SELECT TOP 1 St_HSN_vc AS 'ParticularMi',MIN(LEN(St_HSN_vc))AS 'LengthMi','HSNCode.Min' AS 'ParHeadMi' FROM tbl_Stock GROUP BY St_HSN_vc ORDER BY LEN(St_HSN_vc) ASC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        Adapter = New SqlDataAdapter("SELECT TOP 1 St_HSN_vc AS 'ParticularMi',MAX(LEN(St_HSN_vc))AS 'LengthMi','HSNCode.Max' AS 'ParHeadMi' FROM tbl_Stock GROUP BY St_HSN_vc ORDER BY LEN(St_HSN_vc) DESC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        '-----------------
        Adapter = New SqlDataAdapter("SELECT TOP 1 St_ProName_vc AS 'ParticularMi',MIN((St_MRPRate_n))AS 'LengthMi','MRP.Min' AS 'ParHeadMi' FROM tbl_MultiStock GROUP BY St_ProName_vc,St_MRPRate_n ORDER BY St_MRPRate_n ASC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        Adapter = New SqlDataAdapter("SELECT TOP 1 St_ProName_vc AS 'ParticularMi',MAX((St_MRPRate_n))AS 'LengthMi','MRP.Max' AS 'ParHeadMi' FROM tbl_MultiStock GROUP BY St_ProName_vc,St_MRPRate_n ORDER BY St_MRPRate_n DESC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        '-----------------
        Adapter = New SqlDataAdapter("SELECT TOP 1 St_ProName_vc AS 'ParticularMi',MIN((St_SaleRate_n))AS 'LengthMi','SaleRate.Min' AS 'ParHeadMi' FROM tbl_MultiStock GROUP BY St_ProName_vc,St_SaleRate_n ORDER BY St_SaleRate_n ASC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        Adapter = New SqlDataAdapter("SELECT TOP 1 St_ProName_vc AS 'ParticularMi',MAX((St_SaleRate_n))AS 'LengthMi','SaleRate.Max' AS 'ParHeadMi' FROM tbl_MultiStock GROUP BY St_ProName_vc,St_SaleRate_n ORDER BY St_SaleRate_n DESC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        '-----------------
        Adapter = New SqlDataAdapter("SELECT TOP 1 St_ProName_vc AS 'ParticularMi',MIN((St_PurRate_n))AS 'LengthMi','PurRate.Min' AS 'ParHeadMi' FROM tbl_MultiStock GROUP BY St_ProName_vc,St_PurRate_n ORDER BY St_PurRate_n ASC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        Adapter = New SqlDataAdapter("SELECT TOP 1 St_ProName_vc AS 'ParticularMi',MAX((St_PurRate_n))AS 'LengthMi','PurRate.Max' AS 'ParHeadMi' FROM tbl_MultiStock GROUP BY St_ProName_vc,St_PurRate_n ORDER BY St_PurRate_n DESC", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")

        If Ds.Tables("FillData").Rows.Count > 0 Then
            Row = 0
            For Each Dr As DataRow In Ds.Tables("FillData").Rows
                DgvLength.Rows.Add()
                DgvLength.Rows(Row).Cells("SNoMi").Value = Row + 1
                DgvLength.Rows(Row).Cells("ParHeadMi").Value = Dr.Item("ParHeadMi")
                DgvLength.Rows(Row).Cells("ParticularMi").Value = Dr.Item("ParticularMi")
                DgvLength.Rows(Row).Cells("LengthMi").Value = Dr.Item("LengthMi")
                Row += 1
            Next
        End If
    End Sub
    Private Sub BtnQtyFocus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQtyFocus.Click
       
        If DgvMultiSt.CurrentRow Is Nothing Then Exit Sub

        DgvMultiSt.CurrentCell = DgvMultiSt.Rows(DgvMultiSt.CurrentRow.Index).Cells("Medical Stock")
        DgvMultiSt.Focus()
        DgvMultiSt.BeginEdit(True)
    End Sub
    Private Sub BtnDgvFocus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDgvFocus.Click
        Me.DgvMultiStFocus()
    End Sub
    Private Sub DgvMultiStFocus()
        If DgvMultiSt.Rows.Count = 1 Then
            RowIndex = 0
            DgvMultiSt.CurrentCell = DgvMultiSt.Item("SNo", DgvMultiSt.Rows.Count - 1)
            DgvMultiSt.Focus()
        Else
            DgvMultiSt.CurrentCell = DgvMultiSt.Item("SNo", DgvMultiSt.Rows.Count - 1)
            DgvMultiSt.Focus()
        End If
    End Sub
    Private Sub BtnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSettings.Click

        GrpSettings.Show()
        GrpSettings.BringToFront()

        Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='GodownShow'", VistawinFunctions.ServerConnection, Trans)
        CbGodownShow.Text = Cmd.ExecuteScalar()

        Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='GodownReadOnly'", VistawinFunctions.ServerConnection, Trans)
        CbGodownEdit.Text = Cmd.ExecuteScalar()

        Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='ProName'", VistawinFunctions.ServerConnection, Trans)
        CbProCase.Text = Cmd.ExecuteScalar()

        Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='MedicalReadOnly'", VistawinFunctions.ServerConnection, Trans)
        CbMediQtyEdit.Text = Cmd.ExecuteScalar()

        Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='SaleAutoCal'", VistawinFunctions.ServerConnection, Trans)
        CBSaleRateCal.Text = Cmd.ExecuteScalar()

        Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='Product Search'", VistawinFunctions.ServerConnection, Trans)
        CbProSearch.Text = Cmd.ExecuteScalar()

        Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='TransPWAsk'", VistawinFunctions.ServerConnection, Trans)
        CbTransPWAsk.Text = Cmd.ExecuteScalar()

        'Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='TransferPW'", VistawinFunctions.ServerConnection, Trans)
        'txtTransPw.Text = Cmd.ExecuteScalar()

        Cmd = New SqlCommand("SELECT Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='ExpiryDays'", VistawinFunctions.ServerConnection, Trans)
        txtExpDays.Text = Cmd.ExecuteScalar()

    End Sub
    Private Sub BtnSettSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSettSave.Click
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & CbGodownShow.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='GodownShow'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & CbGodownEdit.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='GodownReadOnly'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & CbProCase.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='ProName'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & CbMediQtyEdit.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='MedicalReadOnly'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & CBSaleRateCal.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='SaleAutoCal'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & CbProSearch.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='Product Search'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & CbTransPWAsk.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='TransPWAsk'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            'Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & txtTransPw.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='TransferPW'", VistawinFunctions.ServerConnection, Trans)
            'Execute = Cmd.ExecuteNonQuery()

            If txtExpDays.Text = String.Empty Then
                txtExpDays.Text = CDec(90)
            End If

            Cmd = New SqlCommand("UPDATE tbl_Settings SET Set_Condition_vc='" & txtExpDays.Text & "' WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='ExpiryDays'", VistawinFunctions.ServerConnection, Trans)
            Execute = Cmd.ExecuteNonQuery()

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Saved")
                Me.ProNameCase()
                'Me.GoStockYesNo()

                GrpSettings.Hide()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub ProNameCase()

        'Cmd = New SqlCommand("SELECT Set_Condition_vc FROM tbl_Settings WHERE Set_TblName_vc='STOCK' AND Set_Particular_vc='ProName'", VistawinFunctions.ServerConnection)
        'Cmd.ExecuteScalar()
        'If Cmd.ExecuteScalar() = "Normal" Then
        '    txtProductName.CharacterCasing = CharacterCasing.Normal
        'ElseIf Cmd.ExecuteScalar() = "Upper" Then
        '    txtProductName.CharacterCasing = CharacterCasing.Upper
        'End If
        'bṇ()
    End Sub
    Private Sub BtnColOrd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnColOrd.Click
        'Dim frm As New SubFrmColOrderCommon
        'frm.ShowDialog()
    End Sub
    Private Sub DgvMultiSt_CellContentClick(
    ByVal sender As Object,
    ByVal e As DataGridViewCellEventArgs) _
    Handles DgvMultiSt.CellContentClick

        Try

            If e.RowIndex < 0 Then Exit Sub
            If e.ColumnIndex < 0 Then Exit Sub

            If DgvMultiSt.Rows(e.RowIndex).IsNewRow Then
                Exit Sub
            End If


            '================================================
            ' CHECK DELETE COLUMN
            '================================================
            If DgvMultiSt.Columns(e.ColumnIndex).Name <> "Delete" Then
                Exit Sub
            End If


            '================================================
            ' GET ST NUMBER
            '================================================
            Dim StNumber As Integer = 0

            If DgvMultiSt.Columns.Contains("StNumber") Then

                If DgvMultiSt.Rows(e.RowIndex).Cells("StNumber").Value IsNot Nothing Then

                    If Trim(DgvMultiSt.Rows(e.RowIndex).
                            Cells("StNumber").Value.ToString()) <> "" Then

                        StNumber = Convert.ToInt32(
                            Val(DgvMultiSt.Rows(e.RowIndex).
                                Cells("StNumber").Value.ToString()))

                    End If

                End If

            End If


            If StNumber = 0 Then
                DgvMultiSt.Rows.RemoveAt(e.RowIndex)
                'MsgBox("Stock Number Not Found")
                'Exit Sub

            End If


            '================================================
            ' CHECK PURCHASE / BILL
            '================================================
            Dim Used As Boolean = False

            Dim Sql As String =
                "SELECT COUNT(*) " &
                "FROM (" &
                "SELECT sa_stnumber_n AS StNumber " &
                "FROM tbl_PurchParticular " &
                "WHERE sa_stnumber_n = @StNumber " &
                "UNION ALL " &
                "SELECT sa_stnumber_n AS StNumber " &
                "FROM tbl_BillParticular " &
                "WHERE sa_stnumber_n = @StNumber" &
                ") X"


            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If


            Using Cmd As New SqlCommand(
                Sql,
                VistawinFunctions.ServerConnection)

                Cmd.Parameters.AddWithValue("@StNumber", StNumber)

                Dim Cnt As Integer =
                    Convert.ToInt32(Cmd.ExecuteScalar())

                If Cnt > 0 Then
                    Used = True
                End If

            End Using


            '================================================
            ' USED STOCK - DO NOT DELETE
            '================================================
            If Used Then

                MsgBox(
                    "This Stock Number is already used in Purchase or Bill." &
                    vbCrLf & vbCrLf &
                    "Stock Number : " & StNumber,
                    MsgBoxStyle.Exclamation,
                    "Cannot Delete")

                Exit Sub

            End If


            '================================================
            ' CONFIRM DELETE
            '================================================
            If MsgBox(
                "Do you want to remove this row?" &
                vbCrLf & vbCrLf &
                "Stock Number : " & StNumber,
                MsgBoxStyle.YesNo Or MsgBoxStyle.Question,
                "Delete Stock") <> MsgBoxResult.Yes Then

                Exit Sub

            End If


            '================================================
            ' DELETE FROM tbl_MultiStock
            '================================================
            Dim DeleteSql As String =
                "DELETE FROM tbl_MultiStock " &
                "WHERE St_StNumber_i = @StNumber"


            Using CmdDelete As New SqlCommand(
                DeleteSql,
                VistawinFunctions.ServerConnection)

                CmdDelete.Parameters.AddWithValue(
                    "@StNumber",
                    StNumber)

                CmdDelete.ExecuteNonQuery()

            End Using


            '================================================
            ' REMOVE ROW FROM GRID
            '================================================
            DgvMultiSt.Rows.RemoveAt(e.RowIndex)


            '================================================
            ' RESET S.NO
            '================================================
            Dim Sno As Integer = 1

            For i As Integer = 0 To DgvMultiSt.Rows.Count - 1

                If Not DgvMultiSt.Rows(i).IsNewRow Then

                    DgvMultiSt.Rows(i).Cells("SNo").Value = Sno
                    Sno += 1

                End If

            Next


        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "Delete Stock",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub EnsureEmptyMultiStockRow()

        Try

            If DgvMultiSt.Rows.Count = 0 Then

                Dim RowNo As Integer =
                    DgvMultiSt.Rows.Add()

                DgvMultiSt.Rows(RowNo).Cells("SNo").Value =
                    RowNo + 1

                Exit Sub

            End If


            '-----------------------------------------
            ' Last row check
            '-----------------------------------------

            Dim LastRow As DataGridViewRow =
                DgvMultiSt.Rows(DgvMultiSt.Rows.Count - 1)


            'NewRow / empty row already exists
            If LastRow.IsNewRow Then Exit Sub


            Dim HasValue As Boolean = False


            'Check all columns except SNo and Delete
            For Each Cell As DataGridViewCell In LastRow.Cells

                If Cell.OwningColumn.Name = "SNo" OrElse
                   Cell.OwningColumn.Name = "Delete" Then

                    Continue For

                End If


                If Cell.Value IsNot Nothing AndAlso
                   Cell.Value.ToString().Trim() <> "" Then

                    HasValue = True
                    Exit For

                End If

            Next


            '-----------------------------------------
            ' Last row has data
            ' Add new empty row
            '-----------------------------------------

            If HasValue Then

                Dim NewRowNo As Integer =
                    DgvMultiSt.Rows.Add()

                DgvMultiSt.Rows(NewRowNo).Cells("SNo").Value =
                    NewRowNo + 1

            End If


        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "Multi Stock",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        End Try

    End Sub
End Class