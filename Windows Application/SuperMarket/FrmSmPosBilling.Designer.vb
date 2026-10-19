<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSmPosBilling
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
        Me.DgvBilling = New System.Windows.Forms.DataGridView()
        Me.SNoB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SaleRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DisPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DisAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSTPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CGSTAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SGSTAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupTotals = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblItemCount = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTotalQty = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtTaxable = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtTax = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtCGST = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtSGST = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtRoundOff = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtBillAmount = New System.Windows.Forms.TextBox()
        Me.GroupEntry = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboItem = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDisc = New System.Windows.Forms.TextBox()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.GroupHeader = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboBillType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpBillDate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCustMobile = New System.Windows.Forms.TextBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnDeleteRow = New System.Windows.Forms.Button()
        Me.BtnNew = New System.Windows.Forms.Button()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.lblViewMode = New System.Windows.Forms.Label()
        Me.BtnPrev = New System.Windows.Forms.Button()
        Me.BtnNext = New System.Windows.Forms.Button()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtOpenBill = New System.Windows.Forms.TextBox()
        Me.BtnOpen = New System.Windows.Forms.Button()
        Me.BtnDeleteBill = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DgvBilling, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupTotals.SuspendLayout()
        Me.GroupEntry.SuspendLayout()
        Me.GroupHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DgvBilling)
        Me.GroupBox3.Controls.Add(Me.GroupTotals)
        Me.GroupBox3.Controls.Add(Me.GroupEntry)
        Me.GroupBox3.Controls.Add(Me.GroupHeader)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1220, 740)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'DgvBilling
        '
        Me.DgvBilling.AllowUserToAddRows = False
        Me.DgvBilling.AllowUserToDeleteRows = False
        Me.DgvBilling.AllowUserToResizeRows = False
        Me.DgvBilling.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Olive
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvBilling.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBilling.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNoB, Me.Code, Me.StNo, Me.ItemName, Me.Unit, Me.MRP, Me.SaleRate, Me.Qty, Me.DisPer, Me.DisAmt, Me.GSTPer, Me.CGSTAmt, Me.SGSTAmt, Me.NetRate, Me.Amount, Me.Remark})
        Me.DgvBilling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvBilling.EnableHeadersVisualStyles = False
        Me.DgvBilling.Location = New System.Drawing.Point(3, 161)
        Me.DgvBilling.Name = "DgvBilling"
        Me.DgvBilling.RowHeadersVisible = False
        Me.DgvBilling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvBilling.Size = New System.Drawing.Size(1214, 479)
        Me.DgvBilling.TabIndex = 3
        '
        'SNoB
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkKhaki
        Me.SNoB.DefaultCellStyle = DataGridViewCellStyle2
        Me.SNoB.HeaderText = "SNo"
        Me.SNoB.Name = "SNoB"
        Me.SNoB.ReadOnly = True
        Me.SNoB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNoB.Width = 50
        '
        'Code
        '
        Me.Code.HeaderText = "Code"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        Me.Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Code.Width = 65
        '
        'StNo
        '
        Me.StNo.HeaderText = "StNo"
        Me.StNo.Name = "StNo"
        Me.StNo.ReadOnly = True
        Me.StNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.StNo.Visible = False
        '
        'ItemName
        '
        Me.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ItemName.HeaderText = "Item Name"
        Me.ItemName.Name = "ItemName"
        Me.ItemName.ReadOnly = True
        Me.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Unit
        '
        Me.Unit.HeaderText = "Unit"
        Me.Unit.Name = "Unit"
        Me.Unit.ReadOnly = True
        Me.Unit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Unit.Width = 60
        '
        'MRP
        '
        Me.MRP.HeaderText = "MRP"
        Me.MRP.Name = "MRP"
        Me.MRP.ReadOnly = True
        Me.MRP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MRP.Width = 75
        '
        'SaleRate
        '
        Me.SaleRate.HeaderText = "Rate"
        Me.SaleRate.Name = "SaleRate"
        Me.SaleRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SaleRate.Width = 80
        '
        'Qty
        '
        Me.Qty.HeaderText = "Qty"
        Me.Qty.Name = "Qty"
        Me.Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Qty.Width = 70
        '
        'DisPer
        '
        Me.DisPer.HeaderText = "Dis %"
        Me.DisPer.Name = "DisPer"
        Me.DisPer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DisPer.Width = 65
        '
        'DisAmt
        '
        Me.DisAmt.HeaderText = "Disc"
        Me.DisAmt.Name = "DisAmt"
        Me.DisAmt.ReadOnly = True
        Me.DisAmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DisAmt.Width = 80
        '
        'GSTPer
        '
        Me.GSTPer.HeaderText = "GST %"
        Me.GSTPer.Name = "GSTPer"
        Me.GSTPer.ReadOnly = True
        Me.GSTPer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSTPer.Width = 65
        '
        'CGSTAmt
        '
        Me.CGSTAmt.HeaderText = "CGST"
        Me.CGSTAmt.Name = "CGSTAmt"
        Me.CGSTAmt.ReadOnly = True
        Me.CGSTAmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CGSTAmt.Width = 80
        '
        'SGSTAmt
        '
        Me.SGSTAmt.HeaderText = "SGST"
        Me.SGSTAmt.Name = "SGSTAmt"
        Me.SGSTAmt.ReadOnly = True
        Me.SGSTAmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SGSTAmt.Width = 80
        '
        'NetRate
        '
        Me.NetRate.HeaderText = "Net Rate"
        Me.NetRate.Name = "NetRate"
        Me.NetRate.ReadOnly = True
        Me.NetRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NetRate.Width = 85
        '
        'Amount
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Amount.DefaultCellStyle = DataGridViewCellStyle3
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.ReadOnly = True
        Me.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Amount.Width = 95
        '
        'Remark
        '
        Me.Remark.HeaderText = "Remark"
        Me.Remark.Name = "Remark"
        Me.Remark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Remark.Width = 120
        '
        'GroupTotals
        '
        Me.GroupTotals.Controls.Add(Me.Label11)
        Me.GroupTotals.Controls.Add(Me.lblItemCount)
        Me.GroupTotals.Controls.Add(Me.Label12)
        Me.GroupTotals.Controls.Add(Me.lblTotalQty)
        Me.GroupTotals.Controls.Add(Me.Label13)
        Me.GroupTotals.Controls.Add(Me.txtSubTotal)
        Me.GroupTotals.Controls.Add(Me.Label14)
        Me.GroupTotals.Controls.Add(Me.txtDiscount)
        Me.GroupTotals.Controls.Add(Me.Label15)
        Me.GroupTotals.Controls.Add(Me.txtTaxable)
        Me.GroupTotals.Controls.Add(Me.Label16)
        Me.GroupTotals.Controls.Add(Me.txtTax)
        Me.GroupTotals.Controls.Add(Me.Label17)
        Me.GroupTotals.Controls.Add(Me.txtCGST)
        Me.GroupTotals.Controls.Add(Me.Label18)
        Me.GroupTotals.Controls.Add(Me.txtSGST)
        Me.GroupTotals.Controls.Add(Me.Label19)
        Me.GroupTotals.Controls.Add(Me.txtRoundOff)
        Me.GroupTotals.Controls.Add(Me.Label20)
        Me.GroupTotals.Controls.Add(Me.txtBillAmount)
        Me.GroupTotals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupTotals.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupTotals.Location = New System.Drawing.Point(3, 640)
        Me.GroupTotals.Name = "GroupTotals"
        Me.GroupTotals.Size = New System.Drawing.Size(1214, 100)
        Me.GroupTotals.TabIndex = 2
        Me.GroupTotals.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(44, 17)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Items"
        '
        'lblItemCount
        '
        Me.lblItemCount.AutoSize = True
        Me.lblItemCount.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCount.Location = New System.Drawing.Point(58, 20)
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Size = New System.Drawing.Size(16, 17)
        Me.lblItemCount.TabIndex = 1
        Me.lblItemCount.Text = "0"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(120, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 17)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Total Qty"
        '
        'lblTotalQty
        '
        Me.lblTotalQty.AutoSize = True
        Me.lblTotalQty.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalQty.Location = New System.Drawing.Point(196, 20)
        Me.lblTotalQty.Name = "lblTotalQty"
        Me.lblTotalQty.Size = New System.Drawing.Size(16, 17)
        Me.lblTotalQty.TabIndex = 3
        Me.lblTotalQty.Text = "0"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(280, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 17)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Sub Total"
        '
        'txtSubTotal
        '
        Me.txtSubTotal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.Location = New System.Drawing.Point(350, 16)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(110, 25)
        Me.txtSubTotal.TabIndex = 5
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(480, 20)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 17)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Discount"
        '
        'txtDiscount
        '
        Me.txtDiscount.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.Location = New System.Drawing.Point(548, 16)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.ReadOnly = True
        Me.txtDiscount.Size = New System.Drawing.Size(110, 25)
        Me.txtDiscount.TabIndex = 7
        Me.txtDiscount.TabStop = False
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(680, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 17)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "Taxable"
        '
        'txtTaxable
        '
        Me.txtTaxable.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtTaxable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTaxable.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxable.Location = New System.Drawing.Point(745, 16)
        Me.txtTaxable.Name = "txtTaxable"
        Me.txtTaxable.ReadOnly = True
        Me.txtTaxable.Size = New System.Drawing.Size(110, 25)
        Me.txtTaxable.TabIndex = 9
        Me.txtTaxable.TabStop = False
        Me.txtTaxable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(875, 20)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(66, 17)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "Total Tax"
        '
        'txtTax
        '
        Me.txtTax.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTax.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTax.Location = New System.Drawing.Point(945, 16)
        Me.txtTax.Name = "txtTax"
        Me.txtTax.ReadOnly = True
        Me.txtTax.Size = New System.Drawing.Size(110, 25)
        Me.txtTax.TabIndex = 11
        Me.txtTax.TabStop = False
        Me.txtTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(8, 60)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(46, 17)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "CGST"
        '
        'txtCGST
        '
        Me.txtCGST.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCGST.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCGST.Location = New System.Drawing.Point(60, 56)
        Me.txtCGST.Name = "txtCGST"
        Me.txtCGST.ReadOnly = True
        Me.txtCGST.Size = New System.Drawing.Size(100, 25)
        Me.txtCGST.TabIndex = 13
        Me.txtCGST.TabStop = False
        Me.txtCGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(175, 60)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(46, 17)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "SGST"
        '
        'txtSGST
        '
        Me.txtSGST.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSGST.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSGST.Location = New System.Drawing.Point(230, 56)
        Me.txtSGST.Name = "txtSGST"
        Me.txtSGST.ReadOnly = True
        Me.txtSGST.Size = New System.Drawing.Size(100, 25)
        Me.txtSGST.TabIndex = 13
        Me.txtSGST.TabStop = False
        Me.txtSGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(350, 60)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(76, 17)
        Me.Label19.TabIndex = 14
        Me.Label19.Text = "Round Off"
        '
        'txtRoundOff
        '
        Me.txtRoundOff.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtRoundOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRoundOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoundOff.Location = New System.Drawing.Point(432, 56)
        Me.txtRoundOff.Name = "txtRoundOff"
        Me.txtRoundOff.ReadOnly = True
        Me.txtRoundOff.Size = New System.Drawing.Size(100, 25)
        Me.txtRoundOff.TabIndex = 15
        Me.txtRoundOff.TabStop = False
        Me.txtRoundOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(575, 58)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(100, 19)
        Me.Label20.TabIndex = 16
        Me.Label20.Text = "Bill Amount"
        '
        'txtBillAmount
        '
        Me.txtBillAmount.BackColor = System.Drawing.Color.White
        Me.txtBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillAmount.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillAmount.ForeColor = System.Drawing.Color.DarkRed
        Me.txtBillAmount.Location = New System.Drawing.Point(690, 52)
        Me.txtBillAmount.Name = "txtBillAmount"
        Me.txtBillAmount.ReadOnly = True
        Me.txtBillAmount.Size = New System.Drawing.Size(180, 32)
        Me.txtBillAmount.TabIndex = 17
        Me.txtBillAmount.TabStop = False
        Me.txtBillAmount.Text = "0.00"
        Me.txtBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupEntry
        '
        Me.GroupEntry.Controls.Add(Me.Label7)
        Me.GroupEntry.Controls.Add(Me.cboItem)
        Me.GroupEntry.Controls.Add(Me.Label8)
        Me.GroupEntry.Controls.Add(Me.txtQty)
        Me.GroupEntry.Controls.Add(Me.Label9)
        Me.GroupEntry.Controls.Add(Me.txtRate)
        Me.GroupEntry.Controls.Add(Me.Label10)
        Me.GroupEntry.Controls.Add(Me.txtDisc)
        Me.GroupEntry.Controls.Add(Me.BtnAdd)
        Me.GroupEntry.Controls.Add(Me.lblStock)
        Me.GroupEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupEntry.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupEntry.Location = New System.Drawing.Point(3, 99)
        Me.GroupEntry.Name = "GroupEntry"
        Me.GroupEntry.Size = New System.Drawing.Size(1214, 62)
        Me.GroupEntry.TabIndex = 1
        Me.GroupEntry.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(108, 17)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Item / Barcode"
        '
        'cboItem
        '
        Me.cboItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItem.FormattingEnabled = True
        Me.cboItem.Location = New System.Drawing.Point(130, 22)
        Me.cboItem.Name = "cboItem"
        Me.cboItem.Size = New System.Drawing.Size(430, 28)
        Me.cboItem.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(575, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 17)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Qty"
        '
        'txtQty
        '
        Me.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.Location = New System.Drawing.Point(610, 22)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(70, 26)
        Me.txtQty.TabIndex = 2
        Me.txtQty.Text = "1"
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(690, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 17)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Rate"
        '
        'txtRate
        '
        Me.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(730, 22)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(90, 26)
        Me.txtRate.TabIndex = 3
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(830, 26)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 17)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Disc %"
        '
        'txtDisc
        '
        Me.txtDisc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDisc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisc.Location = New System.Drawing.Point(890, 22)
        Me.txtDisc.Name = "txtDisc"
        Me.txtDisc.Size = New System.Drawing.Size(70, 26)
        Me.txtDisc.TabIndex = 4
        Me.txtDisc.Text = "0"
        Me.txtDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnAdd
        '
        Me.BtnAdd.BackColor = System.Drawing.Color.Olive
        Me.BtnAdd.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAdd.ForeColor = System.Drawing.Color.White
        Me.BtnAdd.Location = New System.Drawing.Point(975, 20)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(95, 30)
        Me.BtnAdd.TabIndex = 5
        Me.BtnAdd.Text = "Add (F4)"
        Me.BtnAdd.UseVisualStyleBackColor = False
        '
        'lblStock
        '
        Me.lblStock.AutoSize = True
        Me.lblStock.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStock.ForeColor = System.Drawing.Color.DarkRed
        Me.lblStock.Location = New System.Drawing.Point(1085, 26)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(74, 17)
        Me.lblStock.TabIndex = 6
        Me.lblStock.Text = "Stock: -"
        '
        'GroupHeader
        '
        Me.GroupHeader.Controls.Add(Me.Label1)
        Me.GroupHeader.Controls.Add(Me.cboBillType)
        Me.GroupHeader.Controls.Add(Me.Label2)
        Me.GroupHeader.Controls.Add(Me.dtpBillDate)
        Me.GroupHeader.Controls.Add(Me.Label3)
        Me.GroupHeader.Controls.Add(Me.txtBillNo)
        Me.GroupHeader.Controls.Add(Me.Label4)
        Me.GroupHeader.Controls.Add(Me.cboCustomer)
        Me.GroupHeader.Controls.Add(Me.Label5)
        Me.GroupHeader.Controls.Add(Me.txtCustName)
        Me.GroupHeader.Controls.Add(Me.Label6)
        Me.GroupHeader.Controls.Add(Me.txtCustMobile)
        Me.GroupHeader.Controls.Add(Me.BtnSave)
        Me.GroupHeader.Controls.Add(Me.BtnDeleteRow)
        Me.GroupHeader.Controls.Add(Me.BtnNew)
        Me.GroupHeader.Controls.Add(Me.BtnPrint)
        Me.GroupHeader.Controls.Add(Me.BtnClose)
        Me.GroupHeader.Controls.Add(Me.lblViewMode)
        Me.GroupHeader.Controls.Add(Me.BtnPrev)
        Me.GroupHeader.Controls.Add(Me.BtnNext)
        Me.GroupHeader.Controls.Add(Me.BtnOpen)
        Me.GroupHeader.Controls.Add(Me.txtOpenBill)
        Me.GroupHeader.Controls.Add(Me.Label21)
        Me.GroupHeader.Controls.Add(Me.BtnDeleteBill)
        Me.GroupHeader.Controls.Add(Me.lblStatus)
        Me.GroupHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupHeader.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupHeader.Location = New System.Drawing.Point(3, 21)
        Me.GroupHeader.Name = "GroupHeader"
        Me.GroupHeader.Size = New System.Drawing.Size(1214, 110)
        Me.GroupHeader.TabIndex = 0
        Me.GroupHeader.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bill Type"
        '
        'cboBillType
        '
        Me.cboBillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBillType.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBillType.FormattingEnabled = True
        Me.cboBillType.Location = New System.Drawing.Point(82, 20)
        Me.cboBillType.Name = "cboBillType"
        Me.cboBillType.Size = New System.Drawing.Size(120, 26)
        Me.cboBillType.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(215, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Bill Date"
        '
        'dtpBillDate
        '
        Me.dtpBillDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpBillDate.Location = New System.Drawing.Point(288, 20)
        Me.dtpBillDate.Name = "dtpBillDate"
        Me.dtpBillDate.Size = New System.Drawing.Size(115, 25)
        Me.dtpBillDate.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(418, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Bill No"
        '
        'txtBillNo
        '
        Me.txtBillNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.Location = New System.Drawing.Point(480, 20)
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.ReadOnly = True
        Me.txtBillNo.Size = New System.Drawing.Size(120, 25)
        Me.txtBillNo.TabIndex = 5
        Me.txtBillNo.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(615, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Customer"
        '
        'cboCustomer
        '
        Me.cboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(690, 20)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(310, 26)
        Me.cboCustomer.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 17)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Name"
        '
        'txtCustName
        '
        Me.txtCustName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.Location = New System.Drawing.Point(82, 46)
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.ReadOnly = True
        Me.txtCustName.Size = New System.Drawing.Size(300, 25)
        Me.txtCustName.TabIndex = 9
        Me.txtCustName.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(400, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 17)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Mobile"
        '
        'txtCustMobile
        '
        Me.txtCustMobile.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustMobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustMobile.Location = New System.Drawing.Point(455, 46)
        Me.txtCustMobile.Name = "txtCustMobile"
        Me.txtCustMobile.ReadOnly = True
        Me.txtCustMobile.Size = New System.Drawing.Size(150, 25)
        Me.txtCustMobile.TabIndex = 11
        Me.txtCustMobile.TabStop = False
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.Olive
        Me.BtnSave.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(620, 42)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(95, 30)
        Me.BtnSave.TabIndex = 12
        Me.BtnSave.Text = "Save (F2)"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'BtnDeleteRow
        '
        Me.BtnDeleteRow.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnDeleteRow.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteRow.Location = New System.Drawing.Point(720, 42)
        Me.BtnDeleteRow.Name = "BtnDeleteRow"
        Me.BtnDeleteRow.Size = New System.Drawing.Size(95, 30)
        Me.BtnDeleteRow.TabIndex = 13
        Me.BtnDeleteRow.Text = "Del Row"
        Me.BtnDeleteRow.UseVisualStyleBackColor = False
        '
        'BtnNew
        '
        Me.BtnNew.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnNew.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNew.Location = New System.Drawing.Point(820, 42)
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(95, 30)
        Me.BtnNew.TabIndex = 14
        Me.BtnNew.Text = "New"
        Me.BtnNew.UseVisualStyleBackColor = False
        '
        'BtnPrint
        '
        Me.BtnPrint.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnPrint.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(920, 42)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(95, 30)
        Me.BtnPrint.TabIndex = 15
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(1020, 42)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(95, 30)
        Me.BtnClose.TabIndex = 16
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'lblViewMode
        '
        Me.lblViewMode.AutoSize = True
        Me.lblViewMode.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblViewMode.ForeColor = System.Drawing.Color.DarkRed
        Me.lblViewMode.Location = New System.Drawing.Point(8, 80)
        Me.lblViewMode.Name = "lblViewMode"
        Me.lblViewMode.Size = New System.Drawing.Size(72, 17)
        Me.lblViewMode.TabIndex = 17
        Me.lblViewMode.Text = "New Bill"
        '
        'BtnPrev
        '
        Me.BtnPrev.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnPrev.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrev.Location = New System.Drawing.Point(200, 76)
        Me.BtnPrev.Name = "BtnPrev"
        Me.BtnPrev.Size = New System.Drawing.Size(80, 28)
        Me.BtnPrev.TabIndex = 18
        Me.BtnPrev.Text = "< Prev"
        Me.BtnPrev.UseVisualStyleBackColor = False
        '
        'BtnNext
        '
        Me.BtnNext.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnNext.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNext.Location = New System.Drawing.Point(284, 76)
        Me.BtnNext.Name = "BtnNext"
        Me.BtnNext.Size = New System.Drawing.Size(80, 28)
        Me.BtnNext.TabIndex = 19
        Me.BtnNext.Text = "Next >"
        Me.BtnNext.UseVisualStyleBackColor = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(376, 82)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(90, 17)
        Me.Label21.TabIndex = 20
        Me.Label21.Text = "Open Bill No"
        '
        'txtOpenBill
        '
        Me.txtOpenBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOpenBill.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpenBill.Location = New System.Drawing.Point(474, 78)
        Me.txtOpenBill.Name = "txtOpenBill"
        Me.txtOpenBill.Size = New System.Drawing.Size(120, 25)
        Me.txtOpenBill.TabIndex = 20
        '
        'BtnOpen
        '
        Me.BtnOpen.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnOpen.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOpen.Location = New System.Drawing.Point(602, 76)
        Me.BtnOpen.Name = "BtnOpen"
        Me.BtnOpen.Size = New System.Drawing.Size(70, 28)
        Me.BtnOpen.TabIndex = 21
        Me.BtnOpen.Text = "Open"
        Me.BtnOpen.UseVisualStyleBackColor = False
        '
        'BtnDeleteBill
        '
        Me.BtnDeleteBill.BackColor = System.Drawing.Color.DarkRed
        Me.BtnDeleteBill.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteBill.ForeColor = System.Drawing.Color.White
        Me.BtnDeleteBill.Location = New System.Drawing.Point(680, 76)
        Me.BtnDeleteBill.Name = "BtnDeleteBill"
        Me.BtnDeleteBill.Size = New System.Drawing.Size(110, 28)
        Me.BtnDeleteBill.TabIndex = 22
        Me.BtnDeleteBill.Text = "Delete Bill"
        Me.BtnDeleteBill.UseVisualStyleBackColor = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = False
        Me.lblStatus.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblStatus.Location = New System.Drawing.Point(798, 80)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(408, 22)
        Me.lblStatus.TabIndex = 23
        Me.lblStatus.Text = ""
        '
        'FrmSmPosBilling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1220, 740)
        Me.Controls.Add(Me.GroupBox3)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "FrmSmPosBilling"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS Billing"
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.DgvBilling, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupTotals.ResumeLayout(False)
        Me.GroupEntry.ResumeLayout(False)
        Me.GroupEntry.PerformLayout()
        Me.GroupHeader.ResumeLayout(False)
        Me.GroupHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DgvBilling As System.Windows.Forms.DataGridView
    Friend WithEvents SNoB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MRP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SaleRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DisPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DisAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GSTPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CGSTAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SGSTAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupTotals As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblItemCount As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblTotalQty As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtTaxable As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtTax As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtCGST As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtSGST As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtRoundOff As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtBillAmount As System.Windows.Forms.TextBox
    Friend WithEvents GroupEntry As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboItem As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDisc As System.Windows.Forms.TextBox
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents GroupHeader As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboBillType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpBillDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCustMobile As System.Windows.Forms.TextBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnDeleteRow As System.Windows.Forms.Button
    Friend WithEvents BtnNew As System.Windows.Forms.Button
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents lblViewMode As System.Windows.Forms.Label
    Friend WithEvents BtnPrev As System.Windows.Forms.Button
    Friend WithEvents BtnNext As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtOpenBill As System.Windows.Forms.TextBox
    Friend WithEvents BtnOpen As System.Windows.Forms.Button
    Friend WithEvents BtnDeleteBill As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
End Class
