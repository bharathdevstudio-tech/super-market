<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDgvStockCommon
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DgvStockDtl = New System.Windows.Forms.DataGridView()
        Me.DgvMultiSt = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnColOrd = New System.Windows.Forms.Button()
        Me.BtnDashboard = New System.Windows.Forms.Button()
        Me.BtnPharChange = New System.Windows.Forms.Button()
        Me.BtnSettings = New System.Windows.Forms.Button()
        Me.BtnDgvFocus = New System.Windows.Forms.Button()
        Me.BtnQtyFocus = New System.Windows.Forms.Button()
        Me.BtnStReport = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.BtnClose1 = New System.Windows.Forms.Button()
        Me.BtnDel = New System.Windows.Forms.Button()
        Me.DgvLength = New System.Windows.Forms.DataGridView()
        Me.SNoMi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParHeadMi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParticularMi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LengthMi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrpSettings = New System.Windows.Forms.GroupBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.CbProSearch = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.CBSaleRateCal = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtExpDays = New System.Windows.Forms.TextBox()
        Me.CbGodownEdit = New System.Windows.Forms.ComboBox()
        Me.CbGodownShow = New System.Windows.Forms.ComboBox()
        Me.BtnSettClose = New System.Windows.Forms.Button()
        Me.BtnSettSave = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CbTransPWAsk = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.CbMediQtyEdit = New System.Windows.Forms.ComboBox()
        Me.CbProCase = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtGodownStock = New System.Windows.Forms.TextBox()
        Me.LblGoStock = New System.Windows.Forms.Label()
        Me.txtMediStock = New System.Windows.Forms.TextBox()
        Me.LblMediStock = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CbShowAll = New System.Windows.Forms.CheckBox()
        Me.txtTotalStock = New System.Windows.Forms.TextBox()
        Me.LblTotStock = New System.Windows.Forms.Label()
        CType(Me.DgvStockDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvMultiSt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvLength, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpSettings.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'DgvStockDtl
        '
        Me.DgvStockDtl.AllowUserToAddRows = False
        Me.DgvStockDtl.AllowUserToResizeRows = False
        Me.DgvStockDtl.BackgroundColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvStockDtl.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvStockDtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvStockDtl.Dock = System.Windows.Forms.DockStyle.Left
        Me.DgvStockDtl.EnableHeadersVisualStyles = False
        Me.DgvStockDtl.Location = New System.Drawing.Point(0, 0)
        Me.DgvStockDtl.Name = "DgvStockDtl"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvStockDtl.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvStockDtl.RowHeadersVisible = False
        Me.DgvStockDtl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvStockDtl.Size = New System.Drawing.Size(483, 536)
        Me.DgvStockDtl.TabIndex = 1
        '
        'DgvMultiSt
        '
        Me.DgvMultiSt.AllowUserToDeleteRows = False
        Me.DgvMultiSt.AllowUserToResizeRows = False
        Me.DgvMultiSt.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Brown
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvMultiSt.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DgvMultiSt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvMultiSt.DefaultCellStyle = DataGridViewCellStyle4
        Me.DgvMultiSt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvMultiSt.EnableHeadersVisualStyles = False
        Me.DgvMultiSt.Location = New System.Drawing.Point(3, 16)
        Me.DgvMultiSt.Name = "DgvMultiSt"
        Me.DgvMultiSt.RowHeadersVisible = False
        Me.DgvMultiSt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvMultiSt.Size = New System.Drawing.Size(925, 456)
        Me.DgvMultiSt.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.MediumAquamarine
        Me.Panel1.Controls.Add(Me.BtnColOrd)
        Me.Panel1.Controls.Add(Me.BtnDashboard)
        Me.Panel1.Controls.Add(Me.BtnPharChange)
        Me.Panel1.Controls.Add(Me.BtnSettings)
        Me.Panel1.Controls.Add(Me.BtnDgvFocus)
        Me.Panel1.Controls.Add(Me.BtnQtyFocus)
        Me.Panel1.Controls.Add(Me.BtnStReport)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.BtnClear)
        Me.Panel1.Controls.Add(Me.BtnClose1)
        Me.Panel1.Controls.Add(Me.BtnDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 536)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1414, 67)
        Me.Panel1.TabIndex = 3
        '
        'BtnColOrd
        '
        Me.BtnColOrd.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnColOrd.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnColOrd.ForeColor = System.Drawing.Color.Black
        Me.BtnColOrd.Location = New System.Drawing.Point(1057, 9)
        Me.BtnColOrd.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnColOrd.Name = "BtnColOrd"
        Me.BtnColOrd.Size = New System.Drawing.Size(87, 51)
        Me.BtnColOrd.TabIndex = 890
        Me.BtnColOrd.UseVisualStyleBackColor = False
        '
        'BtnDashboard
        '
        Me.BtnDashboard.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnDashboard.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDashboard.ForeColor = System.Drawing.Color.Black
        Me.BtnDashboard.Location = New System.Drawing.Point(511, 9)
        Me.BtnDashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnDashboard.Name = "BtnDashboard"
        Me.BtnDashboard.Size = New System.Drawing.Size(117, 51)
        Me.BtnDashboard.TabIndex = 888
        Me.BtnDashboard.Text = "F8" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Dashboard"
        Me.BtnDashboard.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDashboard.UseVisualStyleBackColor = False
        '
        'BtnPharChange
        '
        Me.BtnPharChange.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnPharChange.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPharChange.ForeColor = System.Drawing.Color.Black
        Me.BtnPharChange.Location = New System.Drawing.Point(631, 9)
        Me.BtnPharChange.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnPharChange.Name = "BtnPharChange"
        Me.BtnPharChange.Size = New System.Drawing.Size(148, 51)
        Me.BtnPharChange.TabIndex = 889
        Me.BtnPharChange.Text = "F9" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.BtnPharChange.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPharChange.UseVisualStyleBackColor = False
        '
        'BtnSettings
        '
        Me.BtnSettings.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnSettings.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSettings.ForeColor = System.Drawing.Color.Black
        Me.BtnSettings.Location = New System.Drawing.Point(782, 9)
        Me.BtnSettings.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnSettings.Name = "BtnSettings"
        Me.BtnSettings.Size = New System.Drawing.Size(132, 51)
        Me.BtnSettings.TabIndex = 887
        Me.BtnSettings.Text = "F10" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Settings"
        Me.BtnSettings.UseVisualStyleBackColor = False
        '
        'BtnDgvFocus
        '
        Me.BtnDgvFocus.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnDgvFocus.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDgvFocus.ForeColor = System.Drawing.Color.Black
        Me.BtnDgvFocus.Location = New System.Drawing.Point(102, 9)
        Me.BtnDgvFocus.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnDgvFocus.Name = "BtnDgvFocus"
        Me.BtnDgvFocus.Size = New System.Drawing.Size(115, 51)
        Me.BtnDgvFocus.TabIndex = 886
        Me.BtnDgvFocus.Text = "F4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Table Focus"
        Me.BtnDgvFocus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDgvFocus.UseVisualStyleBackColor = False
        '
        'BtnQtyFocus
        '
        Me.BtnQtyFocus.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnQtyFocus.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnQtyFocus.ForeColor = System.Drawing.Color.Black
        Me.BtnQtyFocus.Location = New System.Drawing.Point(8, 9)
        Me.BtnQtyFocus.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnQtyFocus.Name = "BtnQtyFocus"
        Me.BtnQtyFocus.Size = New System.Drawing.Size(91, 51)
        Me.BtnQtyFocus.TabIndex = 756
        Me.BtnQtyFocus.Text = "F3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Qty Focus"
        Me.BtnQtyFocus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnQtyFocus.UseVisualStyleBackColor = False
        '
        'BtnStReport
        '
        Me.BtnStReport.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnStReport.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStReport.ForeColor = System.Drawing.Color.Black
        Me.BtnStReport.Location = New System.Drawing.Point(220, 9)
        Me.BtnStReport.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnStReport.Name = "BtnStReport"
        Me.BtnStReport.Size = New System.Drawing.Size(121, 51)
        Me.BtnStReport.TabIndex = 755
        Me.BtnStReport.Text = "F5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Stock Report"
        Me.BtnStReport.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.MediumAquamarine
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(344, 9)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 51)
        Me.btnSave.TabIndex = 19
        Me.btnSave.Text = "F6" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'BtnClear
        '
        Me.BtnClear.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnClear.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClear.ForeColor = System.Drawing.Color.Black
        Me.BtnClear.Location = New System.Drawing.Point(427, 9)
        Me.BtnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(81, 51)
        Me.BtnClear.TabIndex = 748
        Me.BtnClear.Text = "F7" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Clear"
        Me.BtnClear.UseVisualStyleBackColor = False
        '
        'BtnClose1
        '
        Me.BtnClose1.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnClose1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose1.ForeColor = System.Drawing.Color.Black
        Me.BtnClose1.Location = New System.Drawing.Point(1152, 9)
        Me.BtnClose1.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClose1.Name = "BtnClose1"
        Me.BtnClose1.Size = New System.Drawing.Size(87, 51)
        Me.BtnClose1.TabIndex = 754
        Me.BtnClose1.Text = "F12" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Close"
        Me.BtnClose1.UseVisualStyleBackColor = False
        '
        'BtnDel
        '
        Me.BtnDel.BackColor = System.Drawing.Color.MediumAquamarine
        Me.BtnDel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDel.ForeColor = System.Drawing.Color.Black
        Me.BtnDel.Location = New System.Drawing.Point(917, 9)
        Me.BtnDel.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnDel.Name = "BtnDel"
        Me.BtnDel.Size = New System.Drawing.Size(132, 51)
        Me.BtnDel.TabIndex = 749
        Me.BtnDel.Text = "F11" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Stock Delete"
        Me.BtnDel.UseVisualStyleBackColor = False
        '
        'DgvLength
        '
        Me.DgvLength.AllowUserToAddRows = False
        Me.DgvLength.AllowUserToDeleteRows = False
        Me.DgvLength.AllowUserToResizeRows = False
        Me.DgvLength.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.DeepPink
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvLength.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DgvLength.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvLength.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNoMi, Me.ParHeadMi, Me.ParticularMi, Me.LengthMi})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.LightPink
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvLength.DefaultCellStyle = DataGridViewCellStyle7
        Me.DgvLength.EnableHeadersVisualStyles = False
        Me.DgvLength.Location = New System.Drawing.Point(33, 209)
        Me.DgvLength.Name = "DgvLength"
        Me.DgvLength.ReadOnly = True
        Me.DgvLength.RowHeadersVisible = False
        Me.DgvLength.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvLength.Size = New System.Drawing.Size(112, 57)
        Me.DgvLength.TabIndex = 899
        '
        'SNoMi
        '
        Me.SNoMi.HeaderText = "SNo"
        Me.SNoMi.Name = "SNoMi"
        Me.SNoMi.ReadOnly = True
        Me.SNoMi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNoMi.Width = 40
        '
        'ParHeadMi
        '
        Me.ParHeadMi.HeaderText = "Particular"
        Me.ParHeadMi.Name = "ParHeadMi"
        Me.ParHeadMi.ReadOnly = True
        Me.ParHeadMi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ParHeadMi.Width = 200
        '
        'ParticularMi
        '
        Me.ParticularMi.HeaderText = "Name"
        Me.ParticularMi.Name = "ParticularMi"
        Me.ParticularMi.ReadOnly = True
        Me.ParticularMi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ParticularMi.Width = 350
        '
        'LengthMi
        '
        Me.LengthMi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.LengthMi.DefaultCellStyle = DataGridViewCellStyle6
        Me.LengthMi.HeaderText = "Length \ Value"
        Me.LengthMi.Name = "LengthMi"
        Me.LengthMi.ReadOnly = True
        Me.LengthMi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GrpSettings
        '
        Me.GrpSettings.BackColor = System.Drawing.Color.LightPink
        Me.GrpSettings.Controls.Add(Me.Label23)
        Me.GrpSettings.Controls.Add(Me.CbProSearch)
        Me.GrpSettings.Controls.Add(Me.Label20)
        Me.GrpSettings.Controls.Add(Me.CBSaleRateCal)
        Me.GrpSettings.Controls.Add(Me.Label19)
        Me.GrpSettings.Controls.Add(Me.txtExpDays)
        Me.GrpSettings.Controls.Add(Me.CbGodownEdit)
        Me.GrpSettings.Controls.Add(Me.CbGodownShow)
        Me.GrpSettings.Controls.Add(Me.BtnSettClose)
        Me.GrpSettings.Controls.Add(Me.BtnSettSave)
        Me.GrpSettings.Controls.Add(Me.Label18)
        Me.GrpSettings.Controls.Add(Me.CbTransPWAsk)
        Me.GrpSettings.Controls.Add(Me.Label17)
        Me.GrpSettings.Controls.Add(Me.CbMediQtyEdit)
        Me.GrpSettings.Controls.Add(Me.CbProCase)
        Me.GrpSettings.Controls.Add(Me.Label15)
        Me.GrpSettings.Controls.Add(Me.Label14)
        Me.GrpSettings.Controls.Add(Me.Label12)
        Me.GrpSettings.Location = New System.Drawing.Point(229, 251)
        Me.GrpSettings.Name = "GrpSettings"
        Me.GrpSettings.Size = New System.Drawing.Size(360, 202)
        Me.GrpSettings.TabIndex = 900
        Me.GrpSettings.TabStop = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(12, 186)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(123, 19)
        Me.Label23.TabIndex = 921
        Me.Label23.Text = "Product Search"
        '
        'CbProSearch
        '
        Me.CbProSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbProSearch.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbProSearch.FormattingEnabled = True
        Me.CbProSearch.Items.AddRange(New Object() {"First", "Any"})
        Me.CbProSearch.Location = New System.Drawing.Point(207, 183)
        Me.CbProSearch.Name = "CbProSearch"
        Me.CbProSearch.Size = New System.Drawing.Size(136, 25)
        Me.CbProSearch.TabIndex = 920
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(12, 152)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(166, 19)
        Me.Label20.TabIndex = 919
        Me.Label20.Text = "Sale Rate Calculation"
        '
        'CBSaleRateCal
        '
        Me.CBSaleRateCal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBSaleRateCal.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBSaleRateCal.FormattingEnabled = True
        Me.CBSaleRateCal.Items.AddRange(New Object() {"YES", "NO"})
        Me.CBSaleRateCal.Location = New System.Drawing.Point(207, 149)
        Me.CBSaleRateCal.Name = "CBSaleRateCal"
        Me.CBSaleRateCal.Size = New System.Drawing.Size(136, 25)
        Me.CBSaleRateCal.TabIndex = 918
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(12, 254)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(94, 19)
        Me.Label19.TabIndex = 917
        Me.Label19.Text = "Expiry Days"
        '
        'txtExpDays
        '
        Me.txtExpDays.BackColor = System.Drawing.Color.White
        Me.txtExpDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpDays.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpDays.Location = New System.Drawing.Point(207, 251)
        Me.txtExpDays.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.txtExpDays.Name = "txtExpDays"
        Me.txtExpDays.Size = New System.Drawing.Size(136, 26)
        Me.txtExpDays.TabIndex = 916
        '
        'CbGodownEdit
        '
        Me.CbGodownEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbGodownEdit.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbGodownEdit.FormattingEnabled = True
        Me.CbGodownEdit.Items.AddRange(New Object() {"YES", "NO"})
        Me.CbGodownEdit.Location = New System.Drawing.Point(207, 47)
        Me.CbGodownEdit.Name = "CbGodownEdit"
        Me.CbGodownEdit.Size = New System.Drawing.Size(136, 25)
        Me.CbGodownEdit.TabIndex = 913
        '
        'CbGodownShow
        '
        Me.CbGodownShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbGodownShow.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbGodownShow.FormattingEnabled = True
        Me.CbGodownShow.Items.AddRange(New Object() {"YES", "NO"})
        Me.CbGodownShow.Location = New System.Drawing.Point(207, 13)
        Me.CbGodownShow.Name = "CbGodownShow"
        Me.CbGodownShow.Size = New System.Drawing.Size(136, 25)
        Me.CbGodownShow.TabIndex = 912
        '
        'BtnSettClose
        '
        Me.BtnSettClose.BackColor = System.Drawing.Color.LightPink
        Me.BtnSettClose.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSettClose.ForeColor = System.Drawing.Color.Black
        Me.BtnSettClose.Location = New System.Drawing.Point(326, 330)
        Me.BtnSettClose.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnSettClose.Name = "BtnSettClose"
        Me.BtnSettClose.Size = New System.Drawing.Size(82, 34)
        Me.BtnSettClose.TabIndex = 911
        Me.BtnSettClose.Text = "Close"
        Me.BtnSettClose.UseVisualStyleBackColor = False
        '
        'BtnSettSave
        '
        Me.BtnSettSave.BackColor = System.Drawing.Color.LightPink
        Me.BtnSettSave.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSettSave.ForeColor = System.Drawing.Color.Black
        Me.BtnSettSave.Location = New System.Drawing.Point(218, 330)
        Me.BtnSettSave.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnSettSave.Name = "BtnSettSave"
        Me.BtnSettSave.Size = New System.Drawing.Size(82, 34)
        Me.BtnSettSave.TabIndex = 910
        Me.BtnSettSave.Text = "Save"
        Me.BtnSettSave.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(12, 220)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(177, 19)
        Me.Label18.TabIndex = 909
        Me.Label18.Text = "Ask Transfer Password"
        '
        'CbTransPWAsk
        '
        Me.CbTransPWAsk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbTransPWAsk.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbTransPWAsk.FormattingEnabled = True
        Me.CbTransPWAsk.Items.AddRange(New Object() {"YES", "NO"})
        Me.CbTransPWAsk.Location = New System.Drawing.Point(207, 217)
        Me.CbTransPWAsk.Name = "CbTransPWAsk"
        Me.CbTransPWAsk.Size = New System.Drawing.Size(136, 25)
        Me.CbTransPWAsk.TabIndex = 908
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(12, 118)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(173, 19)
        Me.Label17.TabIndex = 907
        Me.Label17.Text = "Medical Qty ReadOnly"
        '
        'CbMediQtyEdit
        '
        Me.CbMediQtyEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbMediQtyEdit.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbMediQtyEdit.FormattingEnabled = True
        Me.CbMediQtyEdit.Items.AddRange(New Object() {"YES", "NO"})
        Me.CbMediQtyEdit.Location = New System.Drawing.Point(207, 115)
        Me.CbMediQtyEdit.Name = "CbMediQtyEdit"
        Me.CbMediQtyEdit.Size = New System.Drawing.Size(136, 25)
        Me.CbMediQtyEdit.TabIndex = 906
        '
        'CbProCase
        '
        Me.CbProCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbProCase.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbProCase.FormattingEnabled = True
        Me.CbProCase.Items.AddRange(New Object() {"Normal", "Upper", "Lower"})
        Me.CbProCase.Location = New System.Drawing.Point(207, 81)
        Me.CbProCase.Name = "CbProCase"
        Me.CbProCase.Size = New System.Drawing.Size(136, 25)
        Me.CbProCase.TabIndex = 905
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(12, 84)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(115, 19)
        Me.Label15.TabIndex = 904
        Me.Label15.Text = "Product Name"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(12, 50)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(175, 19)
        Me.Label14.TabIndex = 903
        Me.Label14.Text = "Godown Qty ReadOnly"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(12, 16)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(114, 19)
        Me.Label12.TabIndex = 901
        Me.Label12.Text = "Godown Show"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.MediumAquamarine
        Me.GroupBox1.Controls.Add(Me.GrpSettings)
        Me.GroupBox1.Controls.Add(Me.DgvLength)
        Me.GroupBox1.Controls.Add(Me.DgvMultiSt)
        Me.GroupBox1.Controls.Add(Me.Panel5)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(483, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(931, 536)
        Me.GroupBox1.TabIndex = 901
        Me.GroupBox1.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.txtGodownStock)
        Me.Panel5.Controls.Add(Me.LblGoStock)
        Me.Panel5.Controls.Add(Me.txtMediStock)
        Me.Panel5.Controls.Add(Me.LblMediStock)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.CbShowAll)
        Me.Panel5.Controls.Add(Me.txtTotalStock)
        Me.Panel5.Controls.Add(Me.LblTotStock)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(3, 472)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(925, 61)
        Me.Panel5.TabIndex = 889
        '
        'txtGodownStock
        '
        Me.txtGodownStock.BackColor = System.Drawing.Color.White
        Me.txtGodownStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGodownStock.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGodownStock.ForeColor = System.Drawing.Color.Red
        Me.txtGodownStock.Location = New System.Drawing.Point(471, 27)
        Me.txtGodownStock.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.txtGodownStock.Name = "txtGodownStock"
        Me.txtGodownStock.ReadOnly = True
        Me.txtGodownStock.Size = New System.Drawing.Size(130, 30)
        Me.txtGodownStock.TabIndex = 764
        Me.txtGodownStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblGoStock
        '
        Me.LblGoStock.AutoSize = True
        Me.LblGoStock.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGoStock.Location = New System.Drawing.Point(465, 3)
        Me.LblGoStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblGoStock.Name = "LblGoStock"
        Me.LblGoStock.Size = New System.Drawing.Size(133, 22)
        Me.LblGoStock.TabIndex = 765
        Me.LblGoStock.Text = "Godown Stock"
        '
        'txtMediStock
        '
        Me.txtMediStock.BackColor = System.Drawing.Color.White
        Me.txtMediStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMediStock.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMediStock.ForeColor = System.Drawing.Color.Red
        Me.txtMediStock.Location = New System.Drawing.Point(627, 27)
        Me.txtMediStock.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.txtMediStock.Name = "txtMediStock"
        Me.txtMediStock.ReadOnly = True
        Me.txtMediStock.Size = New System.Drawing.Size(130, 30)
        Me.txtMediStock.TabIndex = 762
        Me.txtMediStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblMediStock
        '
        Me.LblMediStock.AutoSize = True
        Me.LblMediStock.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMediStock.Location = New System.Drawing.Point(625, 3)
        Me.LblMediStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblMediStock.Name = "LblMediStock"
        Me.LblMediStock.Size = New System.Drawing.Size(130, 22)
        Me.LblMediStock.TabIndex = 763
        Me.LblMediStock.Text = "Medical Stock"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(9, 11)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(301, 22)
        Me.Label3.TabIndex = 761
        Me.Label3.Text = "Double Click SNo--> Row Delete"
        '
        'CbShowAll
        '
        Me.CbShowAll.AutoSize = True
        Me.CbShowAll.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbShowAll.Location = New System.Drawing.Point(321, 10)
        Me.CbShowAll.Name = "CbShowAll"
        Me.CbShowAll.Size = New System.Drawing.Size(106, 23)
        Me.CbShowAll.TabIndex = 760
        Me.CbShowAll.Text = "Stock Only"
        Me.CbShowAll.UseVisualStyleBackColor = True
        '
        'txtTotalStock
        '
        Me.txtTotalStock.BackColor = System.Drawing.Color.White
        Me.txtTotalStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalStock.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalStock.ForeColor = System.Drawing.Color.Red
        Me.txtTotalStock.Location = New System.Drawing.Point(779, 27)
        Me.txtTotalStock.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.txtTotalStock.Name = "txtTotalStock"
        Me.txtTotalStock.ReadOnly = True
        Me.txtTotalStock.Size = New System.Drawing.Size(130, 30)
        Me.txtTotalStock.TabIndex = 8
        Me.txtTotalStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblTotStock
        '
        Me.LblTotStock.AutoSize = True
        Me.LblTotStock.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotStock.Location = New System.Drawing.Point(780, 3)
        Me.LblTotStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblTotStock.Name = "LblTotStock"
        Me.LblTotStock.Size = New System.Drawing.Size(106, 22)
        Me.LblTotStock.TabIndex = 759
        Me.LblTotStock.Text = "Total Stock"
        '
        'FrmDgvStockCommon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1414, 603)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DgvStockDtl)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmDgvStockCommon"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock"
        CType(Me.DgvStockDtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvMultiSt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.DgvLength, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpSettings.ResumeLayout(False)
        Me.GrpSettings.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DgvStockDtl As System.Windows.Forms.DataGridView
    Friend WithEvents DgvMultiSt As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BtnDashboard As System.Windows.Forms.Button
    Friend WithEvents BtnPharChange As System.Windows.Forms.Button
    Friend WithEvents BtnSettings As System.Windows.Forms.Button
    Friend WithEvents BtnDgvFocus As System.Windows.Forms.Button
    Friend WithEvents BtnQtyFocus As System.Windows.Forms.Button
    Friend WithEvents BtnStReport As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents BtnClose1 As System.Windows.Forms.Button
    Friend WithEvents BtnDel As System.Windows.Forms.Button
    Friend WithEvents DgvLength As System.Windows.Forms.DataGridView
    Friend WithEvents SNoMi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParHeadMi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParticularMi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LengthMi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrpSettings As System.Windows.Forms.GroupBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents CbProSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents CBSaleRateCal As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtExpDays As System.Windows.Forms.TextBox
    Friend WithEvents CbGodownEdit As System.Windows.Forms.ComboBox
    Friend WithEvents CbGodownShow As System.Windows.Forms.ComboBox
    Friend WithEvents BtnSettClose As System.Windows.Forms.Button
    Friend WithEvents BtnSettSave As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CbTransPWAsk As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents CbMediQtyEdit As System.Windows.Forms.ComboBox
    Friend WithEvents CbProCase As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents BtnColOrd As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtGodownStock As System.Windows.Forms.TextBox
    Friend WithEvents LblGoStock As System.Windows.Forms.Label
    Friend WithEvents txtMediStock As System.Windows.Forms.TextBox
    Friend WithEvents LblMediStock As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CbShowAll As System.Windows.Forms.CheckBox
    Friend WithEvents txtTotalStock As System.Windows.Forms.TextBox
    Friend WithEvents LblTotStock As System.Windows.Forms.Label
End Class
