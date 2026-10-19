<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSmBrand
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
        Me.DgvBrand = New System.Windows.Forms.DataGridView()
        Me.SNo3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Brand_Code_n = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Brand_Name_vc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Brand_Show_vc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBoxSearch = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBrandName = New System.Windows.Forms.TextBox()
        Me.chkShow = New System.Windows.Forms.CheckBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DgvBrand, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxSearch.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DgvBrand)
        Me.GroupBox3.Controls.Add(Me.GroupBoxSearch)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(984, 561)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'DgvBrand
        '
        Me.DgvBrand.AllowUserToAddRows = False
        Me.DgvBrand.AllowUserToDeleteRows = False
        Me.DgvBrand.AllowUserToResizeRows = False
        Me.DgvBrand.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Olive
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvBrand.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvBrand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBrand.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo3, Me.Tick3, Me.Brand_Code_n, Me.Brand_Name_vc, Me.Brand_Show_vc})
        Me.DgvBrand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvBrand.EnableHeadersVisualStyles = False
        Me.DgvBrand.Location = New System.Drawing.Point(3, 148)
        Me.DgvBrand.Name = "DgvBrand"
        Me.DgvBrand.RowHeadersVisible = False
        Me.DgvBrand.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvBrand.Size = New System.Drawing.Size(978, 410)
        Me.DgvBrand.TabIndex = 2
        '
        'SNo3
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkKhaki
        Me.SNo3.DefaultCellStyle = DataGridViewCellStyle2
        Me.SNo3.HeaderText = "SNo"
        Me.SNo3.Name = "SNo3"
        Me.SNo3.ReadOnly = True
        Me.SNo3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo3.Width = 55
        '
        'Tick3
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkKhaki
        DataGridViewCellStyle3.NullValue = False
        Me.Tick3.DefaultCellStyle = DataGridViewCellStyle3
        Me.Tick3.FalseValue = "NO"
        Me.Tick3.HeaderText = "T"
        Me.Tick3.Name = "Tick3"
        Me.Tick3.TrueValue = "YES"
        Me.Tick3.Width = 45
        '
        'Brand_Code_n
        '
        Me.Brand_Code_n.HeaderText = "Code"
        Me.Brand_Code_n.Name = "Brand_Code_n"
        Me.Brand_Code_n.ReadOnly = True
        Me.Brand_Code_n.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Brand_Code_n.Width = 80
        '
        'Brand_Name_vc
        '
        Me.Brand_Name_vc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Brand_Name_vc.HeaderText = "Brand Name"
        Me.Brand_Name_vc.Name = "Brand_Name_vc"
        Me.Brand_Name_vc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Brand_Show_vc
        '
        Me.Brand_Show_vc.HeaderText = "Active"
        Me.Brand_Show_vc.Name = "Brand_Show_vc"
        Me.Brand_Show_vc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Brand_Show_vc.Width = 80
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
        Me.GroupBoxSearch.Location = New System.Drawing.Point(3, 100)
        Me.GroupBoxSearch.Name = "GroupBoxSearch"
        Me.GroupBoxSearch.Size = New System.Drawing.Size(978, 48)
        Me.GroupBoxSearch.TabIndex = 1
        Me.GroupBoxSearch.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 17)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Search"
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(70, 14)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(300, 25)
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
        Me.GroupBox4.Controls.Add(Me.txtBrandName)
        Me.GroupBox4.Controls.Add(Me.chkShow)
        Me.GroupBox4.Controls.Add(Me.BtnSave)
        Me.GroupBox4.Controls.Add(Me.BtnEdit)
        Me.GroupBox4.Controls.Add(Me.BtnDelete)
        Me.GroupBox4.Controls.Add(Me.BtnClear)
        Me.GroupBox4.Controls.Add(Me.BtnClose)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(3, 21)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(978, 79)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Brand Name"
        '
        'txtBrandName
        '
        Me.txtBrandName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBrandName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBrandName.Location = New System.Drawing.Point(110, 16)
        Me.txtBrandName.Name = "txtBrandName"
        Me.txtBrandName.Size = New System.Drawing.Size(280, 25)
        Me.txtBrandName.TabIndex = 1
        '
        'chkShow
        '
        Me.chkShow.AutoSize = True
        Me.chkShow.Checked = True
        Me.chkShow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShow.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShow.Location = New System.Drawing.Point(410, 19)
        Me.chkShow.Name = "chkShow"
        Me.chkShow.Size = New System.Drawing.Size(68, 21)
        Me.chkShow.TabIndex = 2
        Me.chkShow.Text = "Active"
        Me.chkShow.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.Olive
        Me.BtnSave.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(110, 48)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(80, 28)
        Me.BtnSave.TabIndex = 3
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'BtnEdit
        '
        Me.BtnEdit.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnEdit.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(196, 48)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(80, 28)
        Me.BtnEdit.TabIndex = 4
        Me.BtnEdit.Text = "Edit"
        Me.BtnEdit.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnDelete.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.Location = New System.Drawing.Point(282, 48)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(80, 28)
        Me.BtnDelete.TabIndex = 5
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = False
        '
        'BtnClear
        '
        Me.BtnClear.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnClear.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClear.Location = New System.Drawing.Point(368, 48)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(80, 28)
        Me.BtnClear.TabIndex = 6
        Me.BtnClear.Text = "New"
        Me.BtnClear.UseVisualStyleBackColor = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(454, 48)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(80, 28)
        Me.BtnClose.TabIndex = 7
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'FrmSmBrand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "FrmSmBrand"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Brand Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.DgvBrand, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxSearch.ResumeLayout(False)
        Me.GroupBoxSearch.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DgvBrand As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxSearch As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents BtnExport As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBrandName As System.Windows.Forms.TextBox
    Friend WithEvents chkShow As System.Windows.Forms.CheckBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnEdit As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents SNo3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Brand_Code_n As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Brand_Name_vc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Brand_Show_vc As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
