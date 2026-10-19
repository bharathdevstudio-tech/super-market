<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptStockDateWise
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptStockDateWise))
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnFrom = New System.Windows.Forms.Button()
        Me.txtFromDate = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnPdfReport = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CbPapersize = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnTo = New System.Windows.Forms.Button()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtProductName1A = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnPreview3 = New System.Windows.Forms.Button()
        Me.MonthCalendar3 = New System.Windows.Forms.MonthCalendar()
        Me.DgvStock = New System.Windows.Forms.DataGridView()
        Me.SNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StockCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PurQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRTQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SaleQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SRTQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SamQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WasQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CurrStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnFrom
        '
        Me.btnFrom.BackgroundImage = CType(resources.GetObject("btnFrom.BackgroundImage"), System.Drawing.Image)
        Me.btnFrom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFrom.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrom.ImageKey = "(none)"
        Me.btnFrom.Location = New System.Drawing.Point(181, 10)
        Me.btnFrom.Name = "btnFrom"
        Me.btnFrom.Size = New System.Drawing.Size(30, 27)
        Me.btnFrom.TabIndex = 682
        Me.btnFrom.UseVisualStyleBackColor = True
        '
        'txtFromDate
        '
        Me.txtFromDate.BackColor = System.Drawing.Color.White
        Me.txtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDate.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Location = New System.Drawing.Point(59, 10)
        Me.txtFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.ReadOnly = True
        Me.txtFromDate.Size = New System.Drawing.Size(119, 27)
        Me.txtFromDate.TabIndex = 681
        Me.txtFromDate.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightPink
        Me.Panel1.Controls.Add(Me.BtnPdfReport)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.CbPapersize)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.BtnTo)
        Me.Panel1.Controls.Add(Me.txtToDate)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtProductName1A)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.BtnClose)
        Me.Panel1.Controls.Add(Me.BtnPreview3)
        Me.Panel1.Controls.Add(Me.txtFromDate)
        Me.Panel1.Controls.Add(Me.btnFrom)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1196, 82)
        Me.Panel1.TabIndex = 683
        '
        'BtnPdfReport
        '
        Me.BtnPdfReport.BackColor = System.Drawing.Color.LightPink
        Me.BtnPdfReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPdfReport.Font = New System.Drawing.Font("Bookman Old Style", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPdfReport.Location = New System.Drawing.Point(820, 13)
        Me.BtnPdfReport.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnPdfReport.Name = "BtnPdfReport"
        Me.BtnPdfReport.Size = New System.Drawing.Size(119, 31)
        Me.BtnPdfReport.TabIndex = 854
        Me.BtnPdfReport.Text = "Pdf Report"
        Me.BtnPdfReport.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(412, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 17)
        Me.Label7.TabIndex = 830
        Me.Label7.Text = "Paper Size"
        '
        'CbPapersize
        '
        Me.CbPapersize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbPapersize.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbPapersize.FormattingEnabled = True
        Me.CbPapersize.Items.AddRange(New Object() {"A4L", "A4P"})
        Me.CbPapersize.Location = New System.Drawing.Point(502, 10)
        Me.CbPapersize.Name = "CbPapersize"
        Me.CbPapersize.Size = New System.Drawing.Size(209, 25)
        Me.CbPapersize.TabIndex = 829
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Bookman Old Style", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(218, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 19)
        Me.Label1.TabIndex = 827
        Me.Label1.Text = "To"
        '
        'BtnTo
        '
        Me.BtnTo.BackgroundImage = CType(resources.GetObject("BtnTo.BackgroundImage"), System.Drawing.Image)
        Me.BtnTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnTo.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnTo.ImageKey = "(none)"
        Me.BtnTo.Location = New System.Drawing.Point(367, 10)
        Me.BtnTo.Name = "BtnTo"
        Me.BtnTo.Size = New System.Drawing.Size(30, 27)
        Me.BtnTo.TabIndex = 826
        Me.BtnTo.UseVisualStyleBackColor = True
        '
        'txtToDate
        '
        Me.txtToDate.BackColor = System.Drawing.Color.White
        Me.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDate.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Location = New System.Drawing.Point(247, 10)
        Me.txtToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.ReadOnly = True
        Me.txtToDate.Size = New System.Drawing.Size(119, 27)
        Me.txtToDate.TabIndex = 825
        Me.txtToDate.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(379, 47)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(113, 17)
        Me.Label9.TabIndex = 766
        Me.Label9.Text = "Search Product"
        '
        'txtProductName1A
        '
        Me.txtProductName1A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductName1A.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductName1A.Location = New System.Drawing.Point(502, 43)
        Me.txtProductName1A.Name = "txtProductName1A"
        Me.txtProductName1A.Size = New System.Drawing.Size(209, 26)
        Me.txtProductName1A.TabIndex = 765
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Bookman Old Style", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 19)
        Me.Label6.TabIndex = 685
        Me.Label6.Text = "Date"
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.LightPink
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Bookman Old Style", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(955, 13)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(72, 31)
        Me.BtnClose.TabIndex = 684
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'BtnPreview3
        '
        Me.BtnPreview3.BackColor = System.Drawing.Color.LightPink
        Me.BtnPreview3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPreview3.Font = New System.Drawing.Font("Bookman Old Style", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPreview3.Location = New System.Drawing.Point(726, 13)
        Me.BtnPreview3.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnPreview3.Name = "BtnPreview3"
        Me.BtnPreview3.Size = New System.Drawing.Size(78, 31)
        Me.BtnPreview3.TabIndex = 683
        Me.BtnPreview3.Text = "Preview"
        Me.BtnPreview3.UseVisualStyleBackColor = False
        '
        'MonthCalendar3
        '
        Me.MonthCalendar3.Location = New System.Drawing.Point(191, 41)
        Me.MonthCalendar3.Name = "MonthCalendar3"
        Me.MonthCalendar3.TabIndex = 755
        '
        'DgvStock
        '
        Me.DgvStock.AllowUserToAddRows = False
        Me.DgvStock.AllowUserToDeleteRows = False
        Me.DgvStock.AllowUserToResizeRows = False
        Me.DgvStock.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvStock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvStock.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo, Me.StockCode, Me.ProName, Me.OPStock, Me.PurQty, Me.PRTQty, Me.SaleQty, Me.SRTQty, Me.SamQty, Me.WasQty, Me.CurrStock})
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvStock.DefaultCellStyle = DataGridViewCellStyle20
        Me.DgvStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvStock.EnableHeadersVisualStyles = False
        Me.DgvStock.Location = New System.Drawing.Point(0, 82)
        Me.DgvStock.Margin = New System.Windows.Forms.Padding(4)
        Me.DgvStock.Name = "DgvStock"
        Me.DgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvStock.Size = New System.Drawing.Size(1196, 442)
        Me.DgvStock.TabIndex = 756
        '
        'SNo
        '
        Me.SNo.HeaderText = "SNo"
        Me.SNo.Name = "SNo"
        Me.SNo.ReadOnly = True
        Me.SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo.Width = 60
        '
        'StockCode
        '
        Me.StockCode.HeaderText = "Stock Code"
        Me.StockCode.Name = "StockCode"
        Me.StockCode.ReadOnly = True
        Me.StockCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.StockCode.Width = 90
        '
        'ProName
        '
        Me.ProName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ProName.HeaderText = "Product Name"
        Me.ProName.Name = "ProName"
        Me.ProName.ReadOnly = True
        Me.ProName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'OPStock
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.NullValue = Nothing
        Me.OPStock.DefaultCellStyle = DataGridViewCellStyle12
        Me.OPStock.HeaderText = "Opening Stock"
        Me.OPStock.Name = "OPStock"
        Me.OPStock.ReadOnly = True
        Me.OPStock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PurQty
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PurQty.DefaultCellStyle = DataGridViewCellStyle13
        Me.PurQty.HeaderText = "Pur.Qty"
        Me.PurQty.Name = "PurQty"
        Me.PurQty.ReadOnly = True
        Me.PurQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PRTQty
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PRTQty.DefaultCellStyle = DataGridViewCellStyle14
        Me.PRTQty.HeaderText = "Pur.Return Qty"
        Me.PRTQty.Name = "PRTQty"
        Me.PRTQty.ReadOnly = True
        Me.PRTQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SaleQty
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.SaleQty.DefaultCellStyle = DataGridViewCellStyle15
        Me.SaleQty.HeaderText = "Sale.Qty"
        Me.SaleQty.Name = "SaleQty"
        Me.SaleQty.ReadOnly = True
        Me.SaleQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SRTQty
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.SRTQty.DefaultCellStyle = DataGridViewCellStyle16
        Me.SRTQty.HeaderText = "Sale Return Qty"
        Me.SRTQty.Name = "SRTQty"
        Me.SRTQty.ReadOnly = True
        Me.SRTQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SamQty
        '
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.SamQty.DefaultCellStyle = DataGridViewCellStyle17
        Me.SamQty.HeaderText = "Sample Qty"
        Me.SamQty.Name = "SamQty"
        Me.SamQty.ReadOnly = True
        Me.SamQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SamQty.Visible = False
        '
        'WasQty
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.WasQty.DefaultCellStyle = DataGridViewCellStyle18
        Me.WasQty.HeaderText = "Damage Qty"
        Me.WasQty.Name = "WasQty"
        Me.WasQty.ReadOnly = True
        Me.WasQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CurrStock
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.CurrStock.DefaultCellStyle = DataGridViewCellStyle19
        Me.CurrStock.HeaderText = "Current Stock"
        Me.CurrStock.Name = "CurrStock"
        Me.CurrStock.ReadOnly = True
        Me.CurrStock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1196, 524)
        Me.CrystalReportViewer1.TabIndex = 757
        Me.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'RptStockDateWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1196, 524)
        Me.Controls.Add(Me.MonthCalendar3)
        Me.Controls.Add(Me.DgvStock)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Name = "RptStockDateWise"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock Report DateWise"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DgvStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnFrom As System.Windows.Forms.Button
    Friend WithEvents txtFromDate As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MonthCalendar3 As System.Windows.Forms.MonthCalendar
    Friend WithEvents DgvStock As System.Windows.Forms.DataGridView
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnPreview3 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtProductName1A As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnTo As System.Windows.Forms.Button
    Friend WithEvents txtToDate As System.Windows.Forms.TextBox
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StockCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPStock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PurQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PRTQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SaleQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SRTQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SamQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WasQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CurrStock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CbPapersize As System.Windows.Forms.ComboBox
    Friend WithEvents BtnPdfReport As System.Windows.Forms.Button
End Class
