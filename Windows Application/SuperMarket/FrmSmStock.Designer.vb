<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSmStock
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DgvStock = New System.Windows.Forms.DataGridView()
        Me.ColCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBrand = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColHSN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColMRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPurRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColSaleRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColReorder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColShow = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColStNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupAdjust = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSelItem = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAdjustQty = New System.Windows.Forms.TextBox()
        Me.cboAdjustType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.BtnApply = New System.Windows.Forms.Button()
        Me.lblAdjustMsg = New System.Windows.Forms.Label()
        Me.GroupLedger = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DgvLedger = New System.Windows.Forms.DataGridView()
        Me.LDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LRemark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupFilter = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkLowStock = New System.Windows.Forms.CheckBox()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.BtnLedger = New System.Windows.Forms.Button()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DgvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupAdjust.SuspendLayout()
        Me.GroupLedger.SuspendLayout()
        CType(Me.DgvLedger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupFilter.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DgvStock)
        Me.GroupBox3.Controls.Add(Me.GroupAdjust)
        Me.GroupBox3.Controls.Add(Me.GroupLedger)
        Me.GroupBox3.Controls.Add(Me.GroupFilter)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1220, 740)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'DgvStock
        '
        Me.DgvStock.AllowUserToAddRows = False
        Me.DgvStock.AllowUserToDeleteRows = False
        Me.DgvStock.AllowUserToResizeRows = False
        Me.DgvStock.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Olive
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvStock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvStock.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCode, Me.ColItemName, Me.ColCategory, Me.ColBrand, Me.ColUnit, Me.ColHSN, Me.ColMRP, Me.ColPurRate, Me.ColSaleRate, Me.ColStock, Me.ColTotal, Me.ColReorder, Me.ColValue, Me.ColShow, Me.ColStNo})
        Me.DgvStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvStock.Location = New System.Drawing.Point(3, 73)
        Me.DgvStock.MultiSelect = False
        Me.DgvStock.Name = "DgvStock"
        Me.DgvStock.ReadOnly = True
        Me.DgvStock.RowHeadersVisible = False
        Me.DgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvStock.Size = New System.Drawing.Size(1214, 414)
        Me.DgvStock.TabIndex = 0
        '
        'ColCode
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkKhaki
        Me.ColCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColCode.HeaderText = "Code"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.ReadOnly = True
        Me.ColCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColCode.Width = 60
        '
        'ColItemName
        '
        Me.ColItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColItemName.HeaderText = "Item Name"
        Me.ColItemName.Name = "ColItemName"
        Me.ColItemName.ReadOnly = True
        Me.ColItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColCategory
        '
        Me.ColCategory.HeaderText = "Category"
        Me.ColCategory.Name = "ColCategory"
        Me.ColCategory.ReadOnly = True
        Me.ColCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColCategory.Width = 110
        '
        'ColBrand
        '
        Me.ColBrand.HeaderText = "Brand"
        Me.ColBrand.Name = "ColBrand"
        Me.ColBrand.ReadOnly = True
        Me.ColBrand.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColBrand.Width = 105
        '
        'ColUnit
        '
        Me.ColUnit.HeaderText = "Unit"
        Me.ColUnit.Name = "ColUnit"
        Me.ColUnit.ReadOnly = True
        Me.ColUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColUnit.Width = 55
        '
        'ColHSN
        '
        Me.ColHSN.HeaderText = "HSN"
        Me.ColHSN.Name = "ColHSN"
        Me.ColHSN.ReadOnly = True
        Me.ColHSN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColHSN.Width = 75
        '
        'ColMRP
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        Me.ColMRP.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColMRP.HeaderText = "MRP"
        Me.ColMRP.Name = "ColMRP"
        Me.ColMRP.ReadOnly = True
        Me.ColMRP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColMRP.Width = 70
        '
        'ColPurRate
        '
        Me.ColPurRate.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColPurRate.HeaderText = "Pur Rate"
        Me.ColPurRate.Name = "ColPurRate"
        Me.ColPurRate.ReadOnly = True
        Me.ColPurRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColPurRate.Width = 75
        '
        'ColSaleRate
        '
        Me.ColSaleRate.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColSaleRate.HeaderText = "Sale Rate"
        Me.ColSaleRate.Name = "ColSaleRate"
        Me.ColSaleRate.ReadOnly = True
        Me.ColSaleRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColSaleRate.Width = 80
        '
        'ColStock
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ColStock.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColStock.HeaderText = "Stock"
        Me.ColStock.Name = "ColStock"
        Me.ColStock.ReadOnly = True
        Me.ColStock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColStock.Width = 70
        '
        'ColTotal
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColTotal.HeaderText = "Godown"
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColTotal.Width = 70
        '
        'ColReorder
        '
        Me.ColReorder.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColReorder.HeaderText = "Reorder"
        Me.ColReorder.Name = "ColReorder"
        Me.ColReorder.ReadOnly = True
        Me.ColReorder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColReorder.Width = 70
        '
        'ColValue
        '
        Me.ColValue.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColValue.HeaderText = "Stock Value"
        Me.ColValue.Name = "ColValue"
        Me.ColValue.ReadOnly = True
        Me.ColValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColValue.Width = 90
        '
        'ColShow
        '
        Me.ColShow.HeaderText = "POS"
        Me.ColShow.Name = "ColShow"
        Me.ColShow.ReadOnly = True
        Me.ColShow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColShow.Width = 50
        '
        'ColStNo
        '
        Me.ColStNo.HeaderText = "StNo"
        Me.ColStNo.Name = "ColStNo"
        Me.ColStNo.ReadOnly = True
        Me.ColStNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColStNo.Visible = False
        '
        'GroupAdjust
        '
        Me.GroupAdjust.Controls.Add(Me.Label1)
        Me.GroupAdjust.Controls.Add(Me.lblSelItem)
        Me.GroupAdjust.Controls.Add(Me.Label2)
        Me.GroupAdjust.Controls.Add(Me.txtAdjustQty)
        Me.GroupAdjust.Controls.Add(Me.cboAdjustType)
        Me.GroupAdjust.Controls.Add(Me.Label3)
        Me.GroupAdjust.Controls.Add(Me.txtReason)
        Me.GroupAdjust.Controls.Add(Me.BtnApply)
        Me.GroupAdjust.Controls.Add(Me.lblAdjustMsg)
        Me.GroupAdjust.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupAdjust.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupAdjust.Location = New System.Drawing.Point(3, 487)
        Me.GroupAdjust.Name = "GroupAdjust"
        Me.GroupAdjust.Size = New System.Drawing.Size(1214, 68)
        Me.GroupAdjust.TabIndex = 1
        Me.GroupAdjust.TabStop = False
        Me.GroupAdjust.Text = "Stock Adjustment"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Item"
        '
        'lblSelItem
        '
        Me.lblSelItem.AutoSize = True
        Me.lblSelItem.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelItem.ForeColor = System.Drawing.Color.DarkRed
        Me.lblSelItem.Location = New System.Drawing.Point(56, 24)
        Me.lblSelItem.Name = "lblSelItem"
        Me.lblSelItem.Size = New System.Drawing.Size(66, 17)
        Me.lblSelItem.TabIndex = 1
        Me.lblSelItem.Text = "(no item)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Qty"
        '
        'txtAdjustQty
        '
        Me.txtAdjustQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdjustQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdjustQty.Location = New System.Drawing.Point(58, 38)
        Me.txtAdjustQty.Name = "txtAdjustQty"
        Me.txtAdjustQty.Size = New System.Drawing.Size(80, 25)
        Me.txtAdjustQty.TabIndex = 1
        Me.txtAdjustQty.Text = "0"
        Me.txtAdjustQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboAdjustType
        '
        Me.cboAdjustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAdjustType.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAdjustType.FormattingEnabled = True
        Me.cboAdjustType.Location = New System.Drawing.Point(148, 37)
        Me.cboAdjustType.Name = "cboAdjustType"
        Me.cboAdjustType.Size = New System.Drawing.Size(130, 26)
        Me.cboAdjustType.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Size(292, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Reason"
        '
        'txtReason
        '
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.Location = New System.Drawing.Point(352, 37)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(290, 25)
        Me.txtReason.TabIndex = 3
        '
        'BtnApply
        '
        Me.BtnApply.BackColor = System.Drawing.Color.Olive
        Me.BtnApply.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnApply.ForeColor = System.Drawing.Color.White
        Me.BtnApply.Location = New System.Drawing.Point(655, 33)
        Me.BtnApply.Name = "BtnApply"
        Me.BtnApply.Size = New System.Drawing.Size(140, 32)
        Me.BtnApply.TabIndex = 3
        Me.BtnApply.Text = "Apply Adjust"
        Me.BtnApply.UseVisualStyleBackColor = False
        '
        'lblAdjustMsg
        '
        Me.lblAdjustMsg.AutoSize = True
        Me.lblAdjustMsg.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdjustMsg.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblAdjustMsg.Location = New System.Drawing.Point(810, 42)
        Me.lblAdjustMsg.Name = "lblAdjustMsg"
        Me.lblAdjustMsg.Size = New System.Drawing.Size(0, 17)
        Me.lblAdjustMsg.TabIndex = 4
        '
        'GroupLedger
        '
        Me.GroupLedger.Controls.Add(Me.Label4)
        Me.GroupLedger.Controls.Add(Me.DgvLedger)
        Me.GroupLedger.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupLedger.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupLedger.Location = New System.Drawing.Point(3, 555)
        Me.GroupLedger.Name = "GroupLedger"
        Me.GroupLedger.Size = New System.Drawing.Size(1214, 182)
        Me.GroupLedger.TabIndex = 2
        Me.GroupLedger.TabStop = False
        Me.GroupLedger.Text = "Stock Ledger"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Gray
        Me.Label4.Location = New System.Drawing.Point(180, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(340, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "(last 100 stock movements of the selected item)"
        '
        'DgvLedger
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DgvLedger.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DgvLedger.AllowUserToAddRows = False
        Me.DgvLedger.AllowUserToDeleteRows = False
        Me.DgvLedger.AllowUserToResizeRows = False
        Me.DgvLedger.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgvLedger.BackgroundColor = System.Drawing.Color.White
        Me.DgvLedger.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvLedger.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LDate, Me.LType, Me.LRef, Me.LQty, Me.LRemark})
        Me.DgvLedger.Location = New System.Drawing.Point(8, 24)
        Me.DgvLedger.Name = "DgvLedger"
        Me.DgvLedger.ReadOnly = True
        Me.DgvLedger.RowHeadersVisible = False
        Me.DgvLedger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvLedger.Size = New System.Drawing.Size(1198, 150)
        Me.DgvLedger.TabIndex = 0
        '
        'LDate
        '
        Me.LDate.HeaderText = "Date"
        Me.LDate.Name = "LDate"
        Me.LDate.ReadOnly = True
        Me.LDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.LDate.Width = 150
        '
        'LType
        '
        Me.LType.HeaderText = "Entry"
        Me.LType.Name = "LType"
        Me.LType.ReadOnly = True
        Me.LType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.LType.Width = 110
        '
        'LRef
        '
        Me.LRef.HeaderText = "Reference"
        Me.LRef.Name = "LRef"
        Me.LRef.ReadOnly = True
        Me.LRef.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.LRef.Width = 140
        '
        'LQty
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.LQty.HeaderText = "Qty"
        Me.LQty.Name = "LQty"
        Me.LQty.ReadOnly = True
        Me.LQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.LQty.Width = 90
        '
        'LRemark
        '
        Me.LRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LRemark.HeaderText = "Remark"
        Me.LRemark.Name = "LRemark"
        Me.LRemark.ReadOnly = True
        Me.LRemark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GroupFilter
        '
        Me.GroupFilter.Controls.Add(Me.Label5)
        Me.GroupFilter.Controls.Add(Me.txtSearch)
        Me.GroupFilter.Controls.Add(Me.Label6)
        Me.GroupFilter.Controls.Add(Me.cboCategory)
        Me.GroupFilter.Controls.Add(Me.chkLowStock)
        Me.GroupFilter.Controls.Add(Me.BtnRefresh)
        Me.GroupFilter.Controls.Add(Me.BtnLedger)
        Me.GroupFilter.Controls.Add(Me.BtnPrint)
        Me.GroupFilter.Controls.Add(Me.BtnClose)
        Me.GroupFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupFilter.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupFilter.Location = New System.Drawing.Point(3, 21)
        Me.GroupFilter.Name = "GroupFilter"
        Me.GroupFilter.Size = New System.Drawing.Size(1214, 52)
        Me.GroupFilter.TabIndex = 3
        Me.GroupFilter.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 17)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Search / Barcode"
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(142, 16)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(165, 25)
        Me.txtSearch.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(325, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 17)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Category"
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(400, 16)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(180, 26)
        Me.cboCategory.TabIndex = 2
        '
        'chkLowStock
        '
        Me.chkLowStock.AutoSize = True
        Me.chkLowStock.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLowStock.Location = New System.Drawing.Point(592, 20)
        Me.chkLowStock.Name = "chkLowStock"
        Me.chkLowStock.Size = New System.Drawing.Size(130, 21)
        Me.chkLowStock.TabIndex = 3
        Me.chkLowStock.Text = "Low stock only"
        Me.chkLowStock.UseVisualStyleBackColor = True
        '
        'BtnRefresh
        '
        Me.BtnRefresh.BackColor = System.Drawing.Color.Olive
        Me.BtnRefresh.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRefresh.ForeColor = System.Drawing.Color.White
        Me.BtnRefresh.Location = New System.Drawing.Point(720, 13)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(95, 32)
        Me.BtnRefresh.TabIndex = 4
        Me.BtnRefresh.Text = "Refresh"
        Me.BtnRefresh.UseVisualStyleBackColor = False
        '
        'BtnLedger
        '
        Me.BtnLedger.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnLedger.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLedger.Location = New System.Drawing.Point(825, 13)
        Me.BtnLedger.Name = "BtnLedger"
        Me.BtnLedger.Size = New System.Drawing.Size(105, 32)
        Me.BtnLedger.TabIndex = 5
        Me.BtnLedger.Text = "Ledger"
        Me.BtnLedger.UseVisualStyleBackColor = False
        '
        'BtnPrint
        '
        Me.BtnPrint.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnPrint.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(940, 13)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(90, 32)
        Me.BtnPrint.TabIndex = 6
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(1045, 13)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(95, 32)
        Me.BtnClose.TabIndex = 6
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'FrmSmStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1220, 740)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "FrmSmStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock / Inventory"
        '
        'FrmSmStock
        '
        Me.GroupFilter.ResumeLayout(False)
        Me.GroupFilter.PerformLayout()
        Me.GroupAdjust.ResumeLayout(False)
        Me.GroupAdjust.PerformLayout()
        Me.GroupLedger.ResumeLayout(False)
        Me.GroupLedger.PerformLayout()
        CType(Me.DgvLedger, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DgvStock As System.Windows.Forms.DataGridView
    Friend WithEvents ColCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColUnit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColHSN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMRP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPurRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSaleRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColStock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColReorder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColShow As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColStNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupAdjust As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSelItem As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAdjustQty As System.Windows.Forms.TextBox
    Friend WithEvents cboAdjustType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents BtnApply As System.Windows.Forms.Button
    Friend WithEvents lblAdjustMsg As System.Windows.Forms.Label
    Friend WithEvents GroupLedger As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DgvLedger As System.Windows.Forms.DataGridView
    Friend WithEvents LDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LRef As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LRemark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupFilter As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents chkLowStock As System.Windows.Forms.CheckBox
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents BtnLedger As System.Windows.Forms.Button
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
End Class
