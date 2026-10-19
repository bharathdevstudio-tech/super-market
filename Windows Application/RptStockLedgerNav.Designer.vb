<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptStockLedgerNav
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptStockLedgerNav))
        Me.DgvProList = New System.Windows.Forms.DataGridView()
        Me.MonthCalendar2 = New System.Windows.Forms.MonthCalendar()
        Me.DGVDayDetails = New System.Windows.Forms.DataGridView()
        Me.SNoS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TickS = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RecDateS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecTypeS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecNumberS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecNarrationS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProDtlS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PayableAmtS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaidAmtS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalanceAmtS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtBalanceAmt = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtTotalSales = New System.Windows.Forms.TextBox()
        Me.txtTotalPurchase = New System.Windows.Forms.TextBox()
        Me.BtnPdfReport = New System.Windows.Forms.Button()
        Me.txtStockID = New System.Windows.Forms.TextBox()
        Me.txtProName = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.LblNameLed = New System.Windows.Forms.Label()
        Me.CbAll2 = New System.Windows.Forms.CheckBox()
        Me.btnPreview2 = New System.Windows.Forms.Button()
        Me.btnTo2 = New System.Windows.Forms.Button()
        Me.txtToDate2 = New System.Windows.Forms.TextBox()
        Me.btnFrom2 = New System.Windows.Forms.Button()
        Me.txtFromDate2 = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.CrystalReportViewer2 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CbAny = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.DgvProList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVDayDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DgvProList
        '
        Me.DgvProList.AllowUserToAddRows = False
        Me.DgvProList.AllowUserToDeleteRows = False
        Me.DgvProList.BackgroundColor = System.Drawing.Color.White
        Me.DgvProList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvProList.Location = New System.Drawing.Point(507, 31)
        Me.DgvProList.Name = "DgvProList"
        Me.DgvProList.ReadOnly = True
        Me.DgvProList.RowHeadersVisible = False
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvProList.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.DgvProList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvProList.Size = New System.Drawing.Size(49, 142)
        Me.DgvProList.TabIndex = 767
        '
        'MonthCalendar2
        '
        Me.MonthCalendar2.Location = New System.Drawing.Point(61, 105)
        Me.MonthCalendar2.Name = "MonthCalendar2"
        Me.MonthCalendar2.TabIndex = 765
        '
        'DGVDayDetails
        '
        Me.DGVDayDetails.AllowUserToAddRows = False
        Me.DGVDayDetails.AllowUserToDeleteRows = False
        Me.DGVDayDetails.AllowUserToResizeRows = False
        Me.DGVDayDetails.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Chocolate
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVDayDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DGVDayDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDayDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNoS, Me.TickS, Me.RecDateS, Me.RecTypeS, Me.RecNumberS, Me.RecNarrationS, Me.ProDtlS, Me.PayableAmtS, Me.PaidAmtS, Me.BalanceAmtS})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVDayDetails.DefaultCellStyle = DataGridViewCellStyle12
        Me.DGVDayDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVDayDetails.EnableHeadersVisualStyles = False
        Me.DGVDayDetails.Location = New System.Drawing.Point(0, 44)
        Me.DGVDayDetails.Name = "DGVDayDetails"
        Me.DGVDayDetails.RowHeadersVisible = False
        Me.DGVDayDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVDayDetails.Size = New System.Drawing.Size(1278, 462)
        Me.DGVDayDetails.TabIndex = 681
        '
        'SNoS
        '
        Me.SNoS.HeaderText = "SNo"
        Me.SNoS.Name = "SNoS"
        Me.SNoS.ReadOnly = True
        Me.SNoS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNoS.Width = 40
        '
        'TickS
        '
        Me.TickS.FalseValue = "NO"
        Me.TickS.HeaderText = "Tick"
        Me.TickS.Name = "TickS"
        Me.TickS.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TickS.TrueValue = "YES"
        Me.TickS.Visible = False
        Me.TickS.Width = 40
        '
        'RecDateS
        '
        Me.RecDateS.HeaderText = "Date"
        Me.RecDateS.Name = "RecDateS"
        Me.RecDateS.ReadOnly = True
        Me.RecDateS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'RecTypeS
        '
        Me.RecTypeS.HeaderText = "Type"
        Me.RecTypeS.Name = "RecTypeS"
        Me.RecTypeS.ReadOnly = True
        Me.RecTypeS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RecTypeS.Width = 170
        '
        'RecNumberS
        '
        Me.RecNumberS.HeaderText = "Invoice No"
        Me.RecNumberS.Name = "RecNumberS"
        Me.RecNumberS.ReadOnly = True
        Me.RecNumberS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RecNumberS.Width = 180
        '
        'RecNarrationS
        '
        Me.RecNarrationS.HeaderText = "Particulars"
        Me.RecNarrationS.Name = "RecNarrationS"
        Me.RecNarrationS.ReadOnly = True
        Me.RecNarrationS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RecNarrationS.Width = 300
        '
        'ProDtlS
        '
        Me.ProDtlS.HeaderText = "Product Dtl"
        Me.ProDtlS.Name = "ProDtlS"
        Me.ProDtlS.ReadOnly = True
        Me.ProDtlS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ProDtlS.Width = 300
        '
        'PayableAmtS
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        Me.PayableAmtS.DefaultCellStyle = DataGridViewCellStyle9
        Me.PayableAmtS.HeaderText = "IN Qty"
        Me.PayableAmtS.Name = "PayableAmtS"
        Me.PayableAmtS.ReadOnly = True
        Me.PayableAmtS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PayableAmtS.Width = 80
        '
        'PaidAmtS
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        Me.PaidAmtS.DefaultCellStyle = DataGridViewCellStyle10
        Me.PaidAmtS.HeaderText = "Out Qty"
        Me.PaidAmtS.Name = "PaidAmtS"
        Me.PaidAmtS.ReadOnly = True
        Me.PaidAmtS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PaidAmtS.Width = 80
        '
        'BalanceAmtS
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        Me.BalanceAmtS.DefaultCellStyle = DataGridViewCellStyle11
        Me.BalanceAmtS.HeaderText = "Balance"
        Me.BalanceAmtS.Name = "BalanceAmtS"
        Me.BalanceAmtS.ReadOnly = True
        Me.BalanceAmtS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(1064, 9)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(63, 17)
        Me.Label37.TabIndex = 690
        Me.Label37.Text = "Balance"
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmt.ForeColor = System.Drawing.Color.Red
        Me.txtBalanceAmt.Location = New System.Drawing.Point(1145, 6)
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.Size = New System.Drawing.Size(121, 29)
        Me.txtBalanceAmt.TabIndex = 689
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(869, 9)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(58, 17)
        Me.Label60.TabIndex = 682
        Me.Label60.Text = "Out Qty"
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(666, 9)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(49, 17)
        Me.Label61.TabIndex = 681
        Me.Label61.Text = "IN Qty"
        '
        'txtTotalSales
        '
        Me.txtTotalSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalSales.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalSales.ForeColor = System.Drawing.Color.Red
        Me.txtTotalSales.Location = New System.Drawing.Point(937, 6)
        Me.txtTotalSales.Name = "txtTotalSales"
        Me.txtTotalSales.Size = New System.Drawing.Size(121, 29)
        Me.txtTotalSales.TabIndex = 680
        Me.txtTotalSales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalPurchase
        '
        Me.txtTotalPurchase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalPurchase.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPurchase.ForeColor = System.Drawing.Color.Red
        Me.txtTotalPurchase.Location = New System.Drawing.Point(734, 6)
        Me.txtTotalPurchase.Name = "txtTotalPurchase"
        Me.txtTotalPurchase.Size = New System.Drawing.Size(121, 29)
        Me.txtTotalPurchase.TabIndex = 679
        Me.txtTotalPurchase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnPdfReport
        '
        Me.BtnPdfReport.BackColor = System.Drawing.Color.BurlyWood
        Me.BtnPdfReport.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPdfReport.Location = New System.Drawing.Point(1051, 5)
        Me.BtnPdfReport.Name = "BtnPdfReport"
        Me.BtnPdfReport.Size = New System.Drawing.Size(127, 35)
        Me.BtnPdfReport.TabIndex = 767
        Me.BtnPdfReport.Text = "Pdf Report"
        Me.BtnPdfReport.UseVisualStyleBackColor = False
        '
        'txtStockID
        '
        Me.txtStockID.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStockID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStockID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStockID.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockID.Location = New System.Drawing.Point(347, 10)
        Me.txtStockID.Name = "txtStockID"
        Me.txtStockID.ReadOnly = True
        Me.txtStockID.Size = New System.Drawing.Size(66, 25)
        Me.txtStockID.TabIndex = 771
        Me.txtStockID.Visible = False
        '
        'txtProName
        '
        Me.txtProName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtProName.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProName.Location = New System.Drawing.Point(507, 6)
        Me.txtProName.Name = "txtProName"
        Me.txtProName.Size = New System.Drawing.Size(344, 25)
        Me.txtProName.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.BurlyWood
        Me.btnClose.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(1194, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 35)
        Me.btnClose.TabIndex = 62
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'LblNameLed
        '
        Me.LblNameLed.AutoSize = True
        Me.LblNameLed.BackColor = System.Drawing.Color.Transparent
        Me.LblNameLed.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNameLed.ForeColor = System.Drawing.Color.Black
        Me.LblNameLed.Location = New System.Drawing.Point(439, 9)
        Me.LblNameLed.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblNameLed.Name = "LblNameLed"
        Me.LblNameLed.Size = New System.Drawing.Size(63, 17)
        Me.LblNameLed.TabIndex = 769
        Me.LblNameLed.Text = "Product"
        '
        'CbAll2
        '
        Me.CbAll2.AutoSize = True
        Me.CbAll2.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbAll2.Location = New System.Drawing.Point(347, 8)
        Me.CbAll2.Name = "CbAll2"
        Me.CbAll2.Size = New System.Drawing.Size(82, 21)
        Me.CbAll2.TabIndex = 768
        Me.CbAll2.Text = "All Date"
        Me.CbAll2.UseVisualStyleBackColor = True
        '
        'btnPreview2
        '
        Me.btnPreview2.BackColor = System.Drawing.Color.BurlyWood
        Me.btnPreview2.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview2.Location = New System.Drawing.Point(950, 5)
        Me.btnPreview2.Name = "btnPreview2"
        Me.btnPreview2.Size = New System.Drawing.Size(85, 35)
        Me.btnPreview2.TabIndex = 766
        Me.btnPreview2.Text = "Preview"
        Me.btnPreview2.UseVisualStyleBackColor = False
        '
        'btnTo2
        '
        Me.btnTo2.BackgroundImage = CType(resources.GetObject("btnTo2.BackgroundImage"), System.Drawing.Image)
        Me.btnTo2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTo2.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTo2.ImageKey = "(none)"
        Me.btnTo2.Location = New System.Drawing.Point(312, 5)
        Me.btnTo2.Name = "btnTo2"
        Me.btnTo2.Size = New System.Drawing.Size(27, 26)
        Me.btnTo2.TabIndex = 763
        Me.btnTo2.UseVisualStyleBackColor = True
        '
        'txtToDate2
        '
        Me.txtToDate2.BackColor = System.Drawing.Color.White
        Me.txtToDate2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDate2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtToDate2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate2.Location = New System.Drawing.Point(215, 5)
        Me.txtToDate2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtToDate2.Name = "txtToDate2"
        Me.txtToDate2.ReadOnly = True
        Me.txtToDate2.Size = New System.Drawing.Size(94, 26)
        Me.txtToDate2.TabIndex = 759
        Me.txtToDate2.TabStop = False
        '
        'btnFrom2
        '
        Me.btnFrom2.BackgroundImage = CType(resources.GetObject("btnFrom2.BackgroundImage"), System.Drawing.Image)
        Me.btnFrom2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFrom2.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrom2.ImageKey = "(none)"
        Me.btnFrom2.Location = New System.Drawing.Point(149, 5)
        Me.btnFrom2.Name = "btnFrom2"
        Me.btnFrom2.Size = New System.Drawing.Size(26, 26)
        Me.btnFrom2.TabIndex = 762
        Me.btnFrom2.UseVisualStyleBackColor = True
        '
        'txtFromDate2
        '
        Me.txtFromDate2.BackColor = System.Drawing.Color.White
        Me.txtFromDate2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDate2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate2.Location = New System.Drawing.Point(52, 5)
        Me.txtFromDate2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFromDate2.Name = "txtFromDate2"
        Me.txtFromDate2.ReadOnly = True
        Me.txtFromDate2.Size = New System.Drawing.Size(94, 26)
        Me.txtFromDate2.TabIndex = 760
        Me.txtFromDate2.TabStop = False
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(3, 9)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(40, 17)
        Me.Label58.TabIndex = 761
        Me.Label58.Text = "Date"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(182, 9)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(26, 17)
        Me.Label59.TabIndex = 764
        Me.Label59.Text = "To"
        '
        'CrystalReportViewer2
        '
        Me.CrystalReportViewer2.ActiveViewIndex = -1
        Me.CrystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer2.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer2.Name = "CrystalReportViewer2"
        Me.CrystalReportViewer2.Size = New System.Drawing.Size(1278, 551)
        Me.CrystalReportViewer2.TabIndex = 766
        Me.CrystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.BurlyWood
        Me.Panel1.Controls.Add(Me.CbAny)
        Me.Panel1.Controls.Add(Me.CbAll2)
        Me.Panel1.Controls.Add(Me.BtnPdfReport)
        Me.Panel1.Controls.Add(Me.txtProName)
        Me.Panel1.Controls.Add(Me.Label59)
        Me.Panel1.Controls.Add(Me.Label58)
        Me.Panel1.Controls.Add(Me.txtFromDate2)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnFrom2)
        Me.Panel1.Controls.Add(Me.LblNameLed)
        Me.Panel1.Controls.Add(Me.txtToDate2)
        Me.Panel1.Controls.Add(Me.btnTo2)
        Me.Panel1.Controls.Add(Me.btnPreview2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1278, 44)
        Me.Panel1.TabIndex = 0
        '
        'CbAny
        '
        Me.CbAny.AutoSize = True
        Me.CbAny.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbAny.Location = New System.Drawing.Point(857, 9)
        Me.CbAny.Name = "CbAny"
        Me.CbAny.Size = New System.Drawing.Size(54, 21)
        Me.CbAny.TabIndex = 780
        Me.CbAny.Text = "Any"
        Me.CbAny.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.BurlyWood
        Me.Panel2.Controls.Add(Me.Label37)
        Me.Panel2.Controls.Add(Me.txtTotalSales)
        Me.Panel2.Controls.Add(Me.txtBalanceAmt)
        Me.Panel2.Controls.Add(Me.txtTotalPurchase)
        Me.Panel2.Controls.Add(Me.txtStockID)
        Me.Panel2.Controls.Add(Me.Label60)
        Me.Panel2.Controls.Add(Me.Label61)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 506)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1278, 45)
        Me.Panel2.TabIndex = 2
        '
        'RptStockLedgerNav
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1278, 551)
        Me.Controls.Add(Me.DgvProList)
        Me.Controls.Add(Me.MonthCalendar2)
        Me.Controls.Add(Me.DGVDayDetails)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CrystalReportViewer2)
        Me.Name = "RptStockLedgerNav"
        Me.Text = "Ledger"
        CType(Me.DgvProList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVDayDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnTo2 As System.Windows.Forms.Button
    Friend WithEvents txtToDate2 As System.Windows.Forms.TextBox
    Friend WithEvents btnFrom2 As System.Windows.Forms.Button
    Friend WithEvents txtFromDate2 As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents DGVDayDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtTotalSales As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPurchase As System.Windows.Forms.TextBox
    Friend WithEvents MonthCalendar2 As System.Windows.Forms.MonthCalendar
    Friend WithEvents BtnPdfReport As System.Windows.Forms.Button
    Friend WithEvents btnPreview2 As System.Windows.Forms.Button
    Friend WithEvents CrystalReportViewer2 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents CbAll2 As System.Windows.Forms.CheckBox
    Friend WithEvents txtStockID As System.Windows.Forms.TextBox
    Friend WithEvents txtProName As System.Windows.Forms.TextBox
    Friend WithEvents LblNameLed As System.Windows.Forms.Label
    Friend WithEvents DgvProList As System.Windows.Forms.DataGridView
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmt As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SNoS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TickS As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents RecDateS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecTypeS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecNumberS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecNarrationS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProDtlS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PayableAmtS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaidAmtS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BalanceAmtS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CbAny As System.Windows.Forms.CheckBox
End Class
