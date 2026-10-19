<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptSalRptCommon
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptSalRptCommon))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblHeader = New System.Windows.Forms.Label()
        Me.BtnGSTPdfRpt = New System.Windows.Forms.Button()
        Me.BtnPdfReport = New System.Windows.Forms.Button()
        Me.BtnShowHide = New System.Windows.Forms.Button()
        Me.txtFromDate = New System.Windows.Forms.TextBox()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.btnTo4 = New System.Windows.Forms.Button()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.BtnPreview = New System.Windows.Forms.Button()
        Me.btnFrom4 = New System.Windows.Forms.Button()
        Me.txtCusID = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.DgvProSales = New System.Windows.Forms.DataGridView()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.DGVCustomer = New System.Windows.Forms.DataGridView()
        Me.DgvRptMaster = New System.Windows.Forms.DataGridView()
        Me.SNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Particulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RptType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtNameSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GrpLeft = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvProSales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvRptMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GrpLeft.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Khaki
        Me.Panel1.Controls.Add(Me.LblHeader)
        Me.Panel1.Controls.Add(Me.BtnGSTPdfRpt)
        Me.Panel1.Controls.Add(Me.BtnPdfReport)
        Me.Panel1.Controls.Add(Me.BtnShowHide)
        Me.Panel1.Controls.Add(Me.txtFromDate)
        Me.Panel1.Controls.Add(Me.BtnClose)
        Me.Panel1.Controls.Add(Me.btnTo4)
        Me.Panel1.Controls.Add(Me.txtCustName)
        Me.Panel1.Controls.Add(Me.txtToDate)
        Me.Panel1.Controls.Add(Me.BtnPreview)
        Me.Panel1.Controls.Add(Me.btnFrom4)
        Me.Panel1.Controls.Add(Me.txtCusID)
        Me.Panel1.Controls.Add(Me.Label48)
        Me.Panel1.Controls.Add(Me.Label47)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(823, 82)
        Me.Panel1.TabIndex = 0
        '
        'LblHeader
        '
        Me.LblHeader.AutoSize = True
        Me.LblHeader.BackColor = System.Drawing.Color.Khaki
        Me.LblHeader.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHeader.Location = New System.Drawing.Point(165, 48)
        Me.LblHeader.Name = "LblHeader"
        Me.LblHeader.Size = New System.Drawing.Size(50, 19)
        Me.LblHeader.TabIndex = 877
        Me.LblHeader.Text = "Select"
        '
        'BtnGSTPdfRpt
        '
        Me.BtnGSTPdfRpt.BackColor = System.Drawing.Color.Khaki
        Me.BtnGSTPdfRpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnGSTPdfRpt.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGSTPdfRpt.ForeColor = System.Drawing.Color.Black
        Me.BtnGSTPdfRpt.Location = New System.Drawing.Point(577, 8)
        Me.BtnGSTPdfRpt.Name = "BtnGSTPdfRpt"
        Me.BtnGSTPdfRpt.Size = New System.Drawing.Size(143, 29)
        Me.BtnGSTPdfRpt.TabIndex = 876
        Me.BtnGSTPdfRpt.Text = "GST Pdf Report"
        Me.BtnGSTPdfRpt.UseVisualStyleBackColor = False
        '
        'BtnPdfReport
        '
        Me.BtnPdfReport.BackColor = System.Drawing.Color.Khaki
        Me.BtnPdfReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPdfReport.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPdfReport.ForeColor = System.Drawing.Color.Black
        Me.BtnPdfReport.Location = New System.Drawing.Point(455, 8)
        Me.BtnPdfReport.Name = "BtnPdfReport"
        Me.BtnPdfReport.Size = New System.Drawing.Size(106, 29)
        Me.BtnPdfReport.TabIndex = 875
        Me.BtnPdfReport.Text = "Pdf Report"
        Me.BtnPdfReport.UseVisualStyleBackColor = False
        '
        'BtnShowHide
        '
        Me.BtnShowHide.BackColor = System.Drawing.Color.Khaki
        Me.BtnShowHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnShowHide.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShowHide.ForeColor = System.Drawing.Color.Black
        Me.BtnShowHide.Location = New System.Drawing.Point(10, 43)
        Me.BtnShowHide.Name = "BtnShowHide"
        Me.BtnShowHide.Size = New System.Drawing.Size(78, 29)
        Me.BtnShowHide.TabIndex = 857
        Me.BtnShowHide.Text = "<<  Hide"
        Me.BtnShowHide.UseVisualStyleBackColor = False
        '
        'txtFromDate
        '
        Me.txtFromDate.BackColor = System.Drawing.Color.White
        Me.txtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Location = New System.Drawing.Point(52, 11)
        Me.txtFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.ReadOnly = True
        Me.txtFromDate.Size = New System.Drawing.Size(102, 26)
        Me.txtFromDate.TabIndex = 869
        Me.txtFromDate.TabStop = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.Khaki
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Black
        Me.BtnClose.Location = New System.Drawing.Point(736, 8)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(68, 29)
        Me.BtnClose.TabIndex = 852
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'btnTo4
        '
        Me.btnTo4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnTo4.BackgroundImage = CType(resources.GetObject("btnTo4.BackgroundImage"), System.Drawing.Image)
        Me.btnTo4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTo4.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTo4.ImageKey = "(none)"
        Me.btnTo4.Location = New System.Drawing.Point(324, 11)
        Me.btnTo4.Name = "btnTo4"
        Me.btnTo4.Size = New System.Drawing.Size(25, 26)
        Me.btnTo4.TabIndex = 872
        Me.btnTo4.UseVisualStyleBackColor = False
        '
        'txtCustName
        '
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustName.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!)
        Me.txtCustName.Location = New System.Drawing.Point(222, 45)
        Me.txtCustName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(399, 27)
        Me.txtCustName.TabIndex = 857
        '
        'txtToDate
        '
        Me.txtToDate.BackColor = System.Drawing.Color.White
        Me.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Location = New System.Drawing.Point(222, 11)
        Me.txtToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.ReadOnly = True
        Me.txtToDate.Size = New System.Drawing.Size(102, 26)
        Me.txtToDate.TabIndex = 871
        Me.txtToDate.TabStop = False
        '
        'BtnPreview
        '
        Me.BtnPreview.BackColor = System.Drawing.Color.Khaki
        Me.BtnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPreview.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPreview.ForeColor = System.Drawing.Color.Black
        Me.BtnPreview.Location = New System.Drawing.Point(363, 8)
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Size = New System.Drawing.Size(76, 29)
        Me.BtnPreview.TabIndex = 851
        Me.BtnPreview.Text = "Preview"
        Me.BtnPreview.UseVisualStyleBackColor = False
        '
        'btnFrom4
        '
        Me.btnFrom4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnFrom4.BackgroundImage = CType(resources.GetObject("btnFrom4.BackgroundImage"), System.Drawing.Image)
        Me.btnFrom4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFrom4.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrom4.ImageKey = "(none)"
        Me.btnFrom4.Location = New System.Drawing.Point(154, 11)
        Me.btnFrom4.Name = "btnFrom4"
        Me.btnFrom4.Size = New System.Drawing.Size(25, 26)
        Me.btnFrom4.TabIndex = 870
        Me.btnFrom4.UseVisualStyleBackColor = False
        '
        'txtCusID
        '
        Me.txtCusID.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtCusID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCusID.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!)
        Me.txtCusID.Location = New System.Drawing.Point(621, 45)
        Me.txtCusID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCusID.Name = "txtCusID"
        Me.txtCusID.ReadOnly = True
        Me.txtCusID.Size = New System.Drawing.Size(90, 27)
        Me.txtCusID.TabIndex = 859
        Me.txtCusID.TabStop = False
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.Color.Khaki
        Me.Label48.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(186, 15)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(26, 19)
        Me.Label48.TabIndex = 873
        Me.Label48.Text = "To"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.Color.Khaki
        Me.Label47.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(7, 14)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(42, 19)
        Me.Label47.TabIndex = 868
        Me.Label47.Text = "Date"
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(68, 242)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 874
        '
        'DgvProSales
        '
        Me.DgvProSales.AllowUserToAddRows = False
        Me.DgvProSales.AllowUserToOrderColumns = True
        Me.DgvProSales.AllowUserToResizeRows = False
        Me.DgvProSales.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvProSales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvProSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvProSales.DefaultCellStyle = DataGridViewCellStyle2
        Me.DgvProSales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvProSales.EnableHeadersVisualStyles = False
        Me.DgvProSales.Location = New System.Drawing.Point(3, 98)
        Me.DgvProSales.Name = "DgvProSales"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvProSales.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DgvProSales.RowHeadersVisible = False
        Me.DgvProSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvProSales.Size = New System.Drawing.Size(823, 458)
        Me.DgvProSales.TabIndex = 866
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(3, 98)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(823, 458)
        Me.CrystalReportViewer1.TabIndex = 875
        '
        'DGVCustomer
        '
        Me.DGVCustomer.AllowUserToAddRows = False
        Me.DGVCustomer.AllowUserToDeleteRows = False
        Me.DGVCustomer.AllowUserToResizeRows = False
        Me.DGVCustomer.BackgroundColor = System.Drawing.Color.White
        Me.DGVCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVCustomer.Location = New System.Drawing.Point(225, 88)
        Me.DGVCustomer.Name = "DGVCustomer"
        Me.DGVCustomer.ReadOnly = True
        Me.DGVCustomer.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVCustomer.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVCustomer.Size = New System.Drawing.Size(85, 120)
        Me.DGVCustomer.TabIndex = 858
        '
        'DgvRptMaster
        '
        Me.DgvRptMaster.AllowUserToAddRows = False
        Me.DgvRptMaster.AllowUserToDeleteRows = False
        Me.DgvRptMaster.AllowUserToResizeRows = False
        Me.DgvRptMaster.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        Me.DgvRptMaster.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DgvRptMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvRptMaster.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo, Me.Tick, Me.Particulars, Me.Category, Me.RptType})
        Me.DgvRptMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvRptMaster.EnableHeadersVisualStyles = False
        Me.DgvRptMaster.Location = New System.Drawing.Point(3, 55)
        Me.DgvRptMaster.Name = "DgvRptMaster"
        Me.DgvRptMaster.RowHeadersVisible = False
        Me.DgvRptMaster.RowHeadersWidth = 44
        Me.DgvRptMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvRptMaster.Size = New System.Drawing.Size(435, 501)
        Me.DgvRptMaster.TabIndex = 876
        '
        'SNo
        '
        Me.SNo.HeaderText = "SNo"
        Me.SNo.Name = "SNo"
        Me.SNo.ReadOnly = True
        Me.SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo.Width = 50
        '
        'Tick
        '
        Me.Tick.FalseValue = "NO"
        Me.Tick.HeaderText = "Tick"
        Me.Tick.Name = "Tick"
        Me.Tick.TrueValue = "YES"
        Me.Tick.Width = 50
        '
        'Particulars
        '
        Me.Particulars.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Particulars.HeaderText = "Particulars"
        Me.Particulars.Name = "Particulars"
        Me.Particulars.ReadOnly = True
        Me.Particulars.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Category
        '
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        Me.Category.ReadOnly = True
        Me.Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Category.Visible = False
        '
        'RptType
        '
        Me.RptType.HeaderText = "RptType"
        Me.RptType.Name = "RptType"
        Me.RptType.ReadOnly = True
        Me.RptType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RptType.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MonthCalendar1)
        Me.GroupBox1.Controls.Add(Me.DGVCustomer)
        Me.GroupBox1.Controls.Add(Me.DgvProSales)
        Me.GroupBox1.Controls.Add(Me.CrystalReportViewer1)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(441, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(829, 559)
        Me.GroupBox1.TabIndex = 877
        Me.GroupBox1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Thistle
        Me.Panel3.Controls.Add(Me.txtNameSearch)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 16)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(435, 39)
        Me.Panel3.TabIndex = 877
        '
        'txtNameSearch
        '
        Me.txtNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNameSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!)
        Me.txtNameSearch.Location = New System.Drawing.Point(68, 6)
        Me.txtNameSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNameSearch.Name = "txtNameSearch"
        Me.txtNameSearch.Size = New System.Drawing.Size(264, 27)
        Me.txtNameSearch.TabIndex = 874
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Thistle
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 17)
        Me.Label1.TabIndex = 874
        Me.Label1.Text = "Search"
        '
        'GrpLeft
        '
        Me.GrpLeft.Controls.Add(Me.DgvRptMaster)
        Me.GrpLeft.Controls.Add(Me.Panel3)
        Me.GrpLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.GrpLeft.Location = New System.Drawing.Point(0, 0)
        Me.GrpLeft.Name = "GrpLeft"
        Me.GrpLeft.Size = New System.Drawing.Size(441, 559)
        Me.GrpLeft.TabIndex = 878
        Me.GrpLeft.TabStop = False
        '
        'RptSalRptCommon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1270, 559)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GrpLeft)
        Me.Name = "RptSalRptCommon"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DgvProSales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvRptMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GrpLeft.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnTo4 As System.Windows.Forms.Button
    Friend WithEvents txtToDate As System.Windows.Forms.TextBox
    Friend WithEvents btnFrom4 As System.Windows.Forms.Button
    Friend WithEvents txtFromDate As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents BtnPreview As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents DgvProSales As System.Windows.Forms.DataGridView
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents txtCusID As System.Windows.Forms.TextBox
    Friend WithEvents DGVCustomer As System.Windows.Forms.DataGridView
    Friend WithEvents DgvRptMaster As System.Windows.Forms.DataGridView
    Friend WithEvents BtnShowHide As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GrpLeft As System.Windows.Forms.GroupBox
    Friend WithEvents txtNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnPdfReport As System.Windows.Forms.Button
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Particulars As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RptType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnGSTPdfRpt As System.Windows.Forms.Button
    Friend WithEvents LblHeader As System.Windows.Forms.Label
End Class
