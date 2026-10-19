<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettings
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.DgvSettings = New System.Windows.Forms.DataGridView()
        Me.SNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecordNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TblName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Particular = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Condi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DgvPaperSize = New System.Windows.Forms.DataGridView()
        Me.SNoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecordNoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FormNameP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaperSizeP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrderNoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowP = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnClose2 = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.DgvBillSeries = New System.Windows.Forms.DataGridView()
        Me.SNoB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecordNoB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AcYearB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillTypeB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillSeriesB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartNoB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillCountB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BtnClose1 = New System.Windows.Forms.Button()
        Me.BtnAcSave = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DgvSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DgvPaperSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.DgvBillSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.LightBlue
        Me.GroupBox1.Controls.Add(Me.BtnClose)
        Me.GroupBox1.Controls.Add(Me.BtnUpdate)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(749, 50)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.LightBlue
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Brown
        Me.BtnClose.Location = New System.Drawing.Point(391, 14)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(82, 31)
        Me.BtnClose.TabIndex = 71
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.BackColor = System.Drawing.Color.LightBlue
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.Brown
        Me.BtnUpdate.Location = New System.Drawing.Point(277, 14)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(82, 31)
        Me.BtnUpdate.TabIndex = 69
        Me.BtnUpdate.Text = "Update"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'DgvSettings
        '
        Me.DgvSettings.AllowUserToAddRows = False
        Me.DgvSettings.AllowUserToDeleteRows = False
        Me.DgvSettings.AllowUserToResizeRows = False
        Me.DgvSettings.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        Me.DgvSettings.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSettings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo, Me.RecordNo, Me.TblName, Me.Particular, Me.Condi})
        Me.DgvSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvSettings.EnableHeadersVisualStyles = False
        Me.DgvSettings.Location = New System.Drawing.Point(3, 53)
        Me.DgvSettings.Name = "DgvSettings"
        Me.DgvSettings.RowHeadersVisible = False
        Me.DgvSettings.RowHeadersWidth = 44
        Me.DgvSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvSettings.Size = New System.Drawing.Size(749, 481)
        Me.DgvSettings.TabIndex = 19
        '
        'SNo
        '
        Me.SNo.HeaderText = "SNo"
        Me.SNo.Name = "SNo"
        Me.SNo.ReadOnly = True
        Me.SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo.Width = 50
        '
        'RecordNo
        '
        Me.RecordNo.HeaderText = "Record No"
        Me.RecordNo.Name = "RecordNo"
        Me.RecordNo.ReadOnly = True
        Me.RecordNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RecordNo.Visible = False
        '
        'TblName
        '
        Me.TblName.HeaderText = "Form Name"
        Me.TblName.Name = "TblName"
        Me.TblName.ReadOnly = True
        Me.TblName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TblName.Width = 150
        '
        'Particular
        '
        Me.Particular.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Particular.HeaderText = "Particular"
        Me.Particular.Name = "Particular"
        Me.Particular.ReadOnly = True
        Me.Particular.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Condi
        '
        Me.Condi.HeaderText = "Value"
        Me.Condi.Name = "Condi"
        Me.Condi.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Condi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Condi.Width = 150
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(763, 567)
        Me.TabControl1.TabIndex = 20
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.DgvSettings)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(755, 537)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.DgvPaperSize)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(755, 537)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Paper Size"
        '
        'DgvPaperSize
        '
        Me.DgvPaperSize.AllowUserToAddRows = False
        Me.DgvPaperSize.AllowUserToDeleteRows = False
        Me.DgvPaperSize.AllowUserToResizeRows = False
        Me.DgvPaperSize.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        Me.DgvPaperSize.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvPaperSize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPaperSize.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNoP, Me.RecordNoP, Me.FormNameP, Me.PaperSizeP, Me.OrderNoP, Me.ShowP})
        Me.DgvPaperSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvPaperSize.EnableHeadersVisualStyles = False
        Me.DgvPaperSize.Location = New System.Drawing.Point(3, 53)
        Me.DgvPaperSize.Name = "DgvPaperSize"
        Me.DgvPaperSize.RowHeadersVisible = False
        Me.DgvPaperSize.RowHeadersWidth = 44
        Me.DgvPaperSize.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvPaperSize.Size = New System.Drawing.Size(749, 481)
        Me.DgvPaperSize.TabIndex = 20
        '
        'SNoP
        '
        Me.SNoP.HeaderText = "SNo"
        Me.SNoP.Name = "SNoP"
        Me.SNoP.ReadOnly = True
        Me.SNoP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNoP.Width = 50
        '
        'RecordNoP
        '
        Me.RecordNoP.HeaderText = "Record No"
        Me.RecordNoP.Name = "RecordNoP"
        Me.RecordNoP.ReadOnly = True
        Me.RecordNoP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RecordNoP.Visible = False
        '
        'FormNameP
        '
        Me.FormNameP.HeaderText = "Form Name"
        Me.FormNameP.Name = "FormNameP"
        Me.FormNameP.ReadOnly = True
        Me.FormNameP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.FormNameP.Width = 150
        '
        'PaperSizeP
        '
        Me.PaperSizeP.HeaderText = "Paper Size"
        Me.PaperSizeP.Name = "PaperSizeP"
        Me.PaperSizeP.ReadOnly = True
        Me.PaperSizeP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PaperSizeP.Width = 150
        '
        'OrderNoP
        '
        Me.OrderNoP.HeaderText = "Order By"
        Me.OrderNoP.Name = "OrderNoP"
        Me.OrderNoP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.OrderNoP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ShowP
        '
        Me.ShowP.HeaderText = "Show"
        Me.ShowP.Items.AddRange(New Object() {"YES", "NO"})
        Me.ShowP.Name = "ShowP"
        Me.ShowP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ShowP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.LightBlue
        Me.GroupBox2.Controls.Add(Me.BtnClose2)
        Me.GroupBox2.Controls.Add(Me.BtnSave)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(749, 50)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'BtnClose2
        '
        Me.BtnClose2.BackColor = System.Drawing.Color.LightBlue
        Me.BtnClose2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose2.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose2.ForeColor = System.Drawing.Color.Brown
        Me.BtnClose2.Location = New System.Drawing.Point(391, 14)
        Me.BtnClose2.Name = "BtnClose2"
        Me.BtnClose2.Size = New System.Drawing.Size(82, 31)
        Me.BtnClose2.TabIndex = 71
        Me.BtnClose2.Text = "Close"
        Me.BtnClose2.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.LightBlue
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.Brown
        Me.BtnSave.Location = New System.Drawing.Point(277, 14)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(82, 31)
        Me.BtnSave.TabIndex = 69
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage3.Controls.Add(Me.DgvBillSeries)
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 26)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(755, 537)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Bill Series"
        '
        'DgvBillSeries
        '
        Me.DgvBillSeries.AllowUserToAddRows = False
        Me.DgvBillSeries.AllowUserToDeleteRows = False
        Me.DgvBillSeries.AllowUserToResizeRows = False
        Me.DgvBillSeries.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        Me.DgvBillSeries.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DgvBillSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBillSeries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNoB, Me.RecordNoB, Me.AcYearB, Me.BillTypeB, Me.BillSeriesB, Me.StartNoB, Me.BillCountB})
        Me.DgvBillSeries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvBillSeries.EnableHeadersVisualStyles = False
        Me.DgvBillSeries.Location = New System.Drawing.Point(0, 50)
        Me.DgvBillSeries.Name = "DgvBillSeries"
        Me.DgvBillSeries.RowHeadersVisible = False
        Me.DgvBillSeries.RowHeadersWidth = 44
        Me.DgvBillSeries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvBillSeries.Size = New System.Drawing.Size(755, 487)
        Me.DgvBillSeries.TabIndex = 21
        '
        'SNoB
        '
        Me.SNoB.HeaderText = "SNo"
        Me.SNoB.Name = "SNoB"
        Me.SNoB.ReadOnly = True
        Me.SNoB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNoB.Width = 50
        '
        'RecordNoB
        '
        Me.RecordNoB.HeaderText = "Record No"
        Me.RecordNoB.Name = "RecordNoB"
        Me.RecordNoB.ReadOnly = True
        Me.RecordNoB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RecordNoB.Visible = False
        '
        'AcYearB
        '
        Me.AcYearB.HeaderText = "Ac Year"
        Me.AcYearB.Name = "AcYearB"
        Me.AcYearB.ReadOnly = True
        Me.AcYearB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.AcYearB.Width = 120
        '
        'BillTypeB
        '
        Me.BillTypeB.HeaderText = "BillType"
        Me.BillTypeB.Name = "BillTypeB"
        Me.BillTypeB.ReadOnly = True
        Me.BillTypeB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.BillTypeB.Width = 150
        '
        'BillSeriesB
        '
        Me.BillSeriesB.HeaderText = "Bill Series"
        Me.BillSeriesB.Name = "BillSeriesB"
        Me.BillSeriesB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.BillSeriesB.Width = 130
        '
        'StartNoB
        '
        Me.StartNoB.HeaderText = "Bill Start From"
        Me.StartNoB.Name = "StartNoB"
        Me.StartNoB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.StartNoB.Width = 120
        '
        'BillCountB
        '
        Me.BillCountB.HeaderText = "BillCount"
        Me.BillCountB.Name = "BillCountB"
        Me.BillCountB.ReadOnly = True
        Me.BillCountB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.BillCountB.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.LightBlue
        Me.GroupBox3.Controls.Add(Me.BtnClose1)
        Me.GroupBox3.Controls.Add(Me.BtnAcSave)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(755, 50)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        '
        'BtnClose1
        '
        Me.BtnClose1.BackColor = System.Drawing.Color.LightBlue
        Me.BtnClose1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose1.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose1.ForeColor = System.Drawing.Color.Brown
        Me.BtnClose1.Location = New System.Drawing.Point(391, 14)
        Me.BtnClose1.Name = "BtnClose1"
        Me.BtnClose1.Size = New System.Drawing.Size(82, 31)
        Me.BtnClose1.TabIndex = 71
        Me.BtnClose1.Text = "Close"
        Me.BtnClose1.UseVisualStyleBackColor = False
        '
        'BtnAcSave
        '
        Me.BtnAcSave.BackColor = System.Drawing.Color.LightBlue
        Me.BtnAcSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAcSave.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAcSave.ForeColor = System.Drawing.Color.Brown
        Me.BtnAcSave.Location = New System.Drawing.Point(277, 14)
        Me.BtnAcSave.Name = "BtnAcSave"
        Me.BtnAcSave.Size = New System.Drawing.Size(82, 31)
        Me.BtnAcSave.TabIndex = 69
        Me.BtnAcSave.Text = "Save"
        Me.BtnAcSave.UseVisualStyleBackColor = False
        '
        'FrmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(763, 567)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "FrmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DgvSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DgvPaperSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DgvBillSeries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DgvSettings As System.Windows.Forms.DataGridView
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TblName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Particular As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Condi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnClose2 As System.Windows.Forms.Button
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents DgvPaperSize As System.Windows.Forms.DataGridView
    Friend WithEvents SNoP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordNoP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FormNameP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaperSizeP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderNoP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ShowP As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents DgvBillSeries As System.Windows.Forms.DataGridView
    Friend WithEvents SNoB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordNoB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AcYearB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillTypeB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillSeriesB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StartNoB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillCountB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnClose1 As System.Windows.Forms.Button
    Friend WithEvents BtnAcSave As System.Windows.Forms.Button
End Class
