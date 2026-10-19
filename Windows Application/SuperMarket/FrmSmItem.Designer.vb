<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSmItem
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DgvItem = New System.Windows.Forms.DataGridView()
        Me.SNo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.St_RecordNo_i = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_StockID_n = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_ProName_vc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CatName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BrandName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_Unit_vc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_MRPRate_n = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_SaleRate_n = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_TotalSt_i = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Active = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBoxSearch = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTamilName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboBrand = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboUnit = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtHSN = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCGST = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtSGST = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtMRP = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtPurRate = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtSaleRate = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtOpStock = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtReorder = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtRack = New System.Windows.Forms.TextBox()
        Me.chkShow = New System.Windows.Forms.CheckBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DgvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxSearch.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DgvItem)
        Me.GroupBox3.Controls.Add(Me.GroupBoxSearch)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1100, 700)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'DgvItem
        '
        Me.DgvItem.AllowUserToAddRows = False
        Me.DgvItem.AllowUserToDeleteRows = False
        Me.DgvItem.AllowUserToResizeRows = False
        Me.DgvItem.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Olive
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvItem.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo1, Me.Tick1, Me.St_RecordNo_i, Me.St_StockID_n, Me.St_ProName_vc, Me.CatName, Me.BrandName, Me.St_Unit_vc, Me.St_MRPRate_n, Me.St_SaleRate_n, Me.St_TotalSt_i, Me.Active})
        Me.DgvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvItem.EnableHeadersVisualStyles = False
        Me.DgvItem.Location = New System.Drawing.Point(3, 273)
        Me.DgvItem.Name = "DgvItem"
        Me.DgvItem.RowHeadersVisible = False
        Me.DgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvItem.Size = New System.Drawing.Size(1094, 424)
        Me.DgvItem.TabIndex = 2
        '
        'SNo1
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkKhaki
        Me.SNo1.DefaultCellStyle = DataGridViewCellStyle2
        Me.SNo1.HeaderText = "SNo"
        Me.SNo1.Name = "SNo1"
        Me.SNo1.ReadOnly = True
        Me.SNo1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo1.Width = 55
        '
        'Tick1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkKhaki
        DataGridViewCellStyle3.NullValue = False
        Me.Tick1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Tick1.FalseValue = "NO"
        Me.Tick1.HeaderText = "T"
        Me.Tick1.Name = "Tick1"
        Me.Tick1.TrueValue = "YES"
        Me.Tick1.Width = 45
        '
        'St_RecordNo_i
        '
        Me.St_RecordNo_i.HeaderText = "ID"
        Me.St_RecordNo_i.Name = "St_RecordNo_i"
        Me.St_RecordNo_i.ReadOnly = True
        Me.St_RecordNo_i.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.St_RecordNo_i.Visible = False
        '
        'St_StockID_n
        '
        Me.St_StockID_n.HeaderText = "Code"
        Me.St_StockID_n.Name = "St_StockID_n"
        Me.St_StockID_n.ReadOnly = True
        Me.St_StockID_n.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.St_StockID_n.Width = 70
        '
        'St_ProName_vc
        '
        Me.St_ProName_vc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.St_ProName_vc.HeaderText = "Item Name"
        Me.St_ProName_vc.Name = "St_ProName_vc"
        Me.St_ProName_vc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CatName
        '
        Me.CatName.HeaderText = "Category"
        Me.CatName.Name = "CatName"
        Me.CatName.ReadOnly = True
        Me.CatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CatName.Width = 130
        '
        'BrandName
        '
        Me.BrandName.HeaderText = "Brand"
        Me.BrandName.Name = "BrandName"
        Me.BrandName.ReadOnly = True
        Me.BrandName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.BrandName.Width = 120
        '
        'St_Unit_vc
        '
        Me.St_Unit_vc.HeaderText = "Unit"
        Me.St_Unit_vc.Name = "St_Unit_vc"
        Me.St_Unit_vc.ReadOnly = True
        Me.St_Unit_vc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.St_Unit_vc.Width = 70
        '
        'St_MRPRate_n
        '
        Me.St_MRPRate_n.HeaderText = "MRP"
        Me.St_MRPRate_n.Name = "St_MRPRate_n"
        Me.St_MRPRate_n.ReadOnly = True
        Me.St_MRPRate_n.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.St_MRPRate_n.Width = 80
        '
        'St_SaleRate_n
        '
        Me.St_SaleRate_n.HeaderText = "Sale Rate"
        Me.St_SaleRate_n.Name = "St_SaleRate_n"
        Me.St_SaleRate_n.ReadOnly = True
        Me.St_SaleRate_n.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.St_SaleRate_n.Width = 90
        '
        'St_TotalSt_i
        '
        Me.St_TotalSt_i.HeaderText = "Stock"
        Me.St_TotalSt_i.Name = "St_TotalSt_i"
        Me.St_TotalSt_i.ReadOnly = True
        Me.St_TotalSt_i.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.St_TotalSt_i.Width = 80
        '
        'Active
        '
        Me.Active.HeaderText = "Active"
        Me.Active.Name = "Active"
        Me.Active.ReadOnly = True
        Me.Active.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Active.Width = 70
        '
        'GroupBoxSearch
        '
        Me.GroupBoxSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBoxSearch.Controls.Add(Me.Label5)
        Me.GroupBoxSearch.Controls.Add(Me.txtSearch)
        Me.GroupBoxSearch.Controls.Add(Me.BtnSearch)
        Me.GroupBoxSearch.Controls.Add(Me.BtnRefresh)
        Me.GroupBoxSearch.Controls.Add(Me.BtnExport)
        Me.GroupBoxSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBoxSearch.Location = New System.Drawing.Point(3, 224)
        Me.GroupBoxSearch.Name = "GroupBoxSearch"
        Me.GroupBoxSearch.Size = New System.Drawing.Size(1094, 48)
        Me.GroupBoxSearch.TabIndex = 1
        Me.GroupBoxSearch.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 17)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Search / Barcode"
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(134, 14)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(236, 25)
        Me.txtSearch.TabIndex = 1
        '
        'BtnSearch
        '
        Me.BtnSearch.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnSearch.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(380, 12)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(80, 29)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "Search"
        Me.BtnSearch.UseVisualStyleBackColor = False
        '
        'BtnRefresh
        '
        Me.BtnRefresh.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnRefresh.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRefresh.Location = New System.Drawing.Point(466, 12)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(80, 29)
        Me.BtnRefresh.TabIndex = 3
        Me.BtnRefresh.Text = "Refresh"
        Me.BtnRefresh.UseVisualStyleBackColor = False
        '
        'BtnExport
        '
        Me.BtnExport.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnExport.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExport.Location = New System.Drawing.Point(552, 12)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(80, 29)
        Me.BtnExport.TabIndex = 4
        Me.BtnExport.Text = "Export"
        Me.BtnExport.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.DarkKhaki
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtItemCode)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtItemName)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txtTamilName)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.cboCategory)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.cboBrand)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.cboUnit)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.txtHSN)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtBarcode)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.txtCGST)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.txtSGST)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.txtMRP)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.txtPurRate)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.txtSaleRate)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.txtOpStock)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtReorder)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.txtRack)
        Me.GroupBox4.Controls.Add(Me.chkShow)
        Me.GroupBox4.Controls.Add(Me.BtnSave)
        Me.GroupBox4.Controls.Add(Me.BtnEdit)
        Me.GroupBox4.Controls.Add(Me.BtnDelete)
        Me.GroupBox4.Controls.Add(Me.BtnClear)
        Me.GroupBox4.Controls.Add(Me.BtnClose)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(3, 21)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1094, 203)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Item Code"
        '
        'txtItemCode
        '
        Me.txtItemCode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.Location = New System.Drawing.Point(100, 18)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReadOnly = True
        Me.txtItemCode.Size = New System.Drawing.Size(140, 25)
        Me.txtItemCode.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(250, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Item Name"
        '
        'txtItemName
        '
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.Location = New System.Drawing.Point(342, 18)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(140, 25)
        Me.txtItemName.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(492, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 17)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Tamil Name"
        '
        'txtTamilName
        '
        Me.txtTamilName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTamilName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTamilName.Location = New System.Drawing.Point(584, 18)
        Me.txtTamilName.Name = "txtTamilName"
        Me.txtTamilName.Size = New System.Drawing.Size(140, 25)
        Me.txtTamilName.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(734, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 17)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Category"
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(826, 18)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(250, 26)
        Me.cboCategory.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 54)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 17)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Brand"
        '
        'cboBrand
        '
        Me.cboBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBrand.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBrand.FormattingEnabled = True
        Me.cboBrand.Location = New System.Drawing.Point(100, 50)
        Me.cboBrand.Name = "cboBrand"
        Me.cboBrand.Size = New System.Drawing.Size(140, 26)
        Me.cboBrand.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(250, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 17)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Unit"
        '
        'cboUnit
        '
        Me.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUnit.FormattingEnabled = True
        Me.cboUnit.Location = New System.Drawing.Point(342, 50)
        Me.cboUnit.Name = "cboUnit"
        Me.cboUnit.Size = New System.Drawing.Size(140, 26)
        Me.cboUnit.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(492, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 17)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "HSN Code"
        '
        'txtHSN
        '
        Me.txtHSN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHSN.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHSN.Location = New System.Drawing.Point(584, 50)
        Me.txtHSN.Name = "txtHSN"
        Me.txtHSN.Size = New System.Drawing.Size(140, 25)
        Me.txtHSN.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(734, 54)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 17)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Barcode"
        '
        'txtBarcode
        '
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.Location = New System.Drawing.Point(826, 50)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(250, 25)
        Me.txtBarcode.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 86)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 17)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "CGST %"
        '
        'txtCGST
        '
        Me.txtCGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCGST.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCGST.Location = New System.Drawing.Point(100, 82)
        Me.txtCGST.Name = "txtCGST"
        Me.txtCGST.Size = New System.Drawing.Size(140, 25)
        Me.txtCGST.TabIndex = 15
        Me.txtCGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(250, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(58, 17)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "SGST %"
        '
        'txtSGST
        '
        Me.txtSGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSGST.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSGST.Location = New System.Drawing.Point(342, 82)
        Me.txtSGST.Name = "txtSGST"
        Me.txtSGST.Size = New System.Drawing.Size(140, 25)
        Me.txtSGST.TabIndex = 17
        Me.txtSGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(492, 86)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(40, 17)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "MRP"
        '
        'txtMRP
        '
        Me.txtMRP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRP.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRP.Location = New System.Drawing.Point(584, 82)
        Me.txtMRP.Name = "txtMRP"
        Me.txtMRP.Size = New System.Drawing.Size(140, 25)
        Me.txtMRP.TabIndex = 19
        Me.txtMRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(734, 86)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(104, 17)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Purchase Rate"
        '
        'txtPurRate
        '
        Me.txtPurRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurRate.Location = New System.Drawing.Point(826, 82)
        Me.txtPurRate.Name = "txtPurRate"
        Me.txtPurRate.Size = New System.Drawing.Size(250, 25)
        Me.txtPurRate.TabIndex = 21
        Me.txtPurRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 118)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 17)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "Sale Rate"
        '
        'txtSaleRate
        '
        Me.txtSaleRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleRate.Location = New System.Drawing.Point(100, 114)
        Me.txtSaleRate.Name = "txtSaleRate"
        Me.txtSaleRate.Size = New System.Drawing.Size(140, 25)
        Me.txtSaleRate.TabIndex = 23
        Me.txtSaleRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(250, 118)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(104, 17)
        Me.Label17.TabIndex = 24
        Me.Label17.Text = "Opening Stock"
        '
        'txtOpStock
        '
        Me.txtOpStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOpStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpStock.Location = New System.Drawing.Point(342, 114)
        Me.txtOpStock.Name = "txtOpStock"
        Me.txtOpStock.Size = New System.Drawing.Size(140, 25)
        Me.txtOpStock.TabIndex = 25
        Me.txtOpStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(492, 118)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(94, 17)
        Me.Label18.TabIndex = 26
        Me.Label18.Text = "Reorder Level"
        '
        'txtReorder
        '
        Me.txtReorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReorder.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReorder.Location = New System.Drawing.Point(584, 114)
        Me.txtReorder.Name = "txtReorder"
        Me.txtReorder.Size = New System.Drawing.Size(140, 25)
        Me.txtReorder.TabIndex = 27
        Me.txtReorder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(734, 118)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(41, 17)
        Me.Label19.TabIndex = 28
        Me.Label19.Text = "Rack"
        '
        'txtRack
        '
        Me.txtRack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRack.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRack.Location = New System.Drawing.Point(826, 114)
        Me.txtRack.Name = "txtRack"
        Me.txtRack.Size = New System.Drawing.Size(250, 25)
        Me.txtRack.TabIndex = 29
        '
        'chkShow
        '
        Me.chkShow.AutoSize = True
        Me.chkShow.Checked = True
        Me.chkShow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShow.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShow.Location = New System.Drawing.Point(826, 160)
        Me.chkShow.Name = "chkShow"
        Me.chkShow.Size = New System.Drawing.Size(68, 21)
        Me.chkShow.TabIndex = 30
        Me.chkShow.Text = "Active"
        Me.chkShow.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.Olive
        Me.BtnSave.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(100, 160)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(90, 30)
        Me.BtnSave.TabIndex = 31
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'BtnEdit
        '
        Me.BtnEdit.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnEdit.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(196, 160)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(90, 30)
        Me.BtnEdit.TabIndex = 32
        Me.BtnEdit.Text = "Edit"
        Me.BtnEdit.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnDelete.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.Location = New System.Drawing.Point(292, 160)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(90, 30)
        Me.BtnDelete.TabIndex = 33
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = False
        '
        'BtnClear
        '
        Me.BtnClear.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnClear.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClear.Location = New System.Drawing.Point(388, 160)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(90, 30)
        Me.BtnClear.TabIndex = 34
        Me.BtnClear.Text = "New"
        Me.BtnClear.UseVisualStyleBackColor = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(484, 160)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(90, 30)
        Me.BtnClose.TabIndex = 35
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'FrmSmItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 700)
        Me.Controls.Add(Me.GroupBox3)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "FrmSmItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item Master"
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.DgvItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxSearch.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DgvItem As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxSearch As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents BtnExport As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTamilName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboBrand As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtHSN As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCGST As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSGST As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMRP As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtPurRate As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtSaleRate As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtOpStock As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtReorder As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtRack As System.Windows.Forms.TextBox
    Friend WithEvents chkShow As System.Windows.Forms.CheckBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnEdit As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents SNo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents St_RecordNo_i As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_StockID_n As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_ProName_vc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CatName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BrandName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_Unit_vc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_MRPRate_n As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_SaleRate_n As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_TotalSt_i As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Active As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
