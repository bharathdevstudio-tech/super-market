<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDBChange
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
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GrpAdd = New System.Windows.Forms.GroupBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnGrpClose = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDBRemark = New System.Windows.Forms.TextBox()
        Me.txtSqlName = New System.Windows.Forms.TextBox()
        Me.txtDBName = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSysName = New System.Windows.Forms.TextBox()
        Me.DgvDBList = New System.Windows.Forms.DataGridView()
        Me.SNo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SystemName1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SQLName1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DBName1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DBRemark1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColorName1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DBRecord1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DBShow1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnChangeDB = New System.Windows.Forms.Button()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnDBDel = New System.Windows.Forms.Button()
        Me.btnbrowse = New System.Windows.Forms.Button()
        Me.BtnView2 = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GrpAdd.SuspendLayout()
        CType(Me.DgvDBList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1268, 465)
        Me.TabControl1.TabIndex = 709
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1260, 435)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "DataBase Restore"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GrpAdd)
        Me.GroupBox3.Controls.Add(Me.DgvDBList)
        Me.GroupBox3.Controls.Add(Me.GroupBox5)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1254, 429)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'GrpAdd
        '
        Me.GrpAdd.BackColor = System.Drawing.Color.SandyBrown
        Me.GrpAdd.Controls.Add(Me.BtnSave)
        Me.GrpAdd.Controls.Add(Me.BtnGrpClose)
        Me.GrpAdd.Controls.Add(Me.Label13)
        Me.GrpAdd.Controls.Add(Me.Label12)
        Me.GrpAdd.Controls.Add(Me.Label11)
        Me.GrpAdd.Controls.Add(Me.Label10)
        Me.GrpAdd.Controls.Add(Me.txtDBRemark)
        Me.GrpAdd.Controls.Add(Me.txtSqlName)
        Me.GrpAdd.Controls.Add(Me.txtDBName)
        Me.GrpAdd.Controls.Add(Me.Label9)
        Me.GrpAdd.Controls.Add(Me.Label8)
        Me.GrpAdd.Controls.Add(Me.Label7)
        Me.GrpAdd.Controls.Add(Me.Label3)
        Me.GrpAdd.Controls.Add(Me.txtSysName)
        Me.GrpAdd.Location = New System.Drawing.Point(11, 56)
        Me.GrpAdd.Name = "GrpAdd"
        Me.GrpAdd.Size = New System.Drawing.Size(605, 174)
        Me.GrpAdd.TabIndex = 715
        Me.GrpAdd.TabStop = False
        '
        'BtnSave
        '
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(276, 132)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(81, 29)
        Me.BtnSave.TabIndex = 727
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnGrpClose
        '
        Me.BtnGrpClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnGrpClose.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGrpClose.Location = New System.Drawing.Point(377, 132)
        Me.BtnGrpClose.Name = "BtnGrpClose"
        Me.BtnGrpClose.Size = New System.Drawing.Size(81, 29)
        Me.BtnGrpClose.TabIndex = 717
        Me.BtnGrpClose.Text = "Close"
        Me.BtnGrpClose.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Red
        Me.Label13.Location = New System.Drawing.Point(188, 102)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(16, 20)
        Me.Label13.TabIndex = 726
        Me.Label13.Text = "*"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Red
        Me.Label12.Location = New System.Drawing.Point(188, 80)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(16, 20)
        Me.Label12.TabIndex = 725
        Me.Label12.Text = "*"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(188, 50)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(16, 20)
        Me.Label11.TabIndex = 724
        Me.Label11.Text = "*"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(188, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(16, 20)
        Me.Label10.TabIndex = 723
        Me.Label10.Text = "*"
        '
        'txtDBRemark
        '
        Me.txtDBRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDBRemark.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDBRemark.Location = New System.Drawing.Point(218, 100)
        Me.txtDBRemark.Name = "txtDBRemark"
        Me.txtDBRemark.Size = New System.Drawing.Size(364, 26)
        Me.txtDBRemark.TabIndex = 722
        '
        'txtSqlName
        '
        Me.txtSqlName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSqlName.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSqlName.Location = New System.Drawing.Point(218, 48)
        Me.txtSqlName.Name = "txtSqlName"
        Me.txtSqlName.Size = New System.Drawing.Size(364, 26)
        Me.txtSqlName.TabIndex = 721
        '
        'txtDBName
        '
        Me.txtDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDBName.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDBName.Location = New System.Drawing.Point(218, 74)
        Me.txtDBName.Name = "txtDBName"
        Me.txtDBName.Size = New System.Drawing.Size(364, 26)
        Me.txtDBName.TabIndex = 721
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(10, 102)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 19)
        Me.Label9.TabIndex = 720
        Me.Label9.Text = "Remark"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(10, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(154, 19)
        Me.Label8.TabIndex = 719
        Me.Label8.Text = "SQL Server Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(141, 19)
        Me.Label7.TabIndex = 718
        Me.Label7.Text = "Database Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 19)
        Me.Label3.TabIndex = 717
        Me.Label3.Text = "System Name"
        '
        'txtSysName
        '
        Me.txtSysName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSysName.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSysName.Location = New System.Drawing.Point(218, 22)
        Me.txtSysName.Name = "txtSysName"
        Me.txtSysName.Size = New System.Drawing.Size(364, 26)
        Me.txtSysName.TabIndex = 701
        '
        'DgvDBList
        '
        Me.DgvDBList.AllowUserToAddRows = False
        Me.DgvDBList.AllowUserToResizeRows = False
        Me.DgvDBList.BackgroundColor = System.Drawing.Color.White
        Me.DgvDBList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDBList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo1, Me.Tick1, Me.SystemName1, Me.SQLName1, Me.DBName1, Me.DBRemark1, Me.ColorName1, Me.DBRecord1, Me.DBShow1})
        Me.DgvDBList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvDBList.EnableHeadersVisualStyles = False
        Me.DgvDBList.Location = New System.Drawing.Point(3, 19)
        Me.DgvDBList.Name = "DgvDBList"
        Me.DgvDBList.RowHeadersVisible = False
        Me.DgvDBList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvDBList.Size = New System.Drawing.Size(1248, 275)
        Me.DgvDBList.TabIndex = 712
        '
        'SNo1
        '
        DataGridViewCellStyle25.BackColor = System.Drawing.Color.LightGreen
        Me.SNo1.DefaultCellStyle = DataGridViewCellStyle25
        Me.SNo1.HeaderText = "SNo"
        Me.SNo1.Name = "SNo1"
        Me.SNo1.ReadOnly = True
        Me.SNo1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo1.Width = 45
        '
        'Tick1
        '
        Me.Tick1.FalseValue = "NO"
        Me.Tick1.HeaderText = "Tick"
        Me.Tick1.Name = "Tick1"
        Me.Tick1.TrueValue = "YES"
        Me.Tick1.Width = 40
        '
        'SystemName1
        '
        DataGridViewCellStyle26.BackColor = System.Drawing.Color.LightGreen
        Me.SystemName1.DefaultCellStyle = DataGridViewCellStyle26
        Me.SystemName1.HeaderText = "System Name"
        Me.SystemName1.Name = "SystemName1"
        Me.SystemName1.ReadOnly = True
        Me.SystemName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SystemName1.Width = 220
        '
        'SQLName1
        '
        DataGridViewCellStyle27.BackColor = System.Drawing.Color.LightGreen
        Me.SQLName1.DefaultCellStyle = DataGridViewCellStyle27
        Me.SQLName1.HeaderText = "Server Name"
        Me.SQLName1.Name = "SQLName1"
        Me.SQLName1.ReadOnly = True
        Me.SQLName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SQLName1.Width = 320
        '
        'DBName1
        '
        DataGridViewCellStyle28.BackColor = System.Drawing.Color.LightGreen
        Me.DBName1.DefaultCellStyle = DataGridViewCellStyle28
        Me.DBName1.HeaderText = "DataBase Name"
        Me.DBName1.Name = "DBName1"
        Me.DBName1.ReadOnly = True
        Me.DBName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DBName1.Width = 180
        '
        'DBRemark1
        '
        DataGridViewCellStyle29.BackColor = System.Drawing.Color.LightGreen
        Me.DBRemark1.DefaultCellStyle = DataGridViewCellStyle29
        Me.DBRemark1.HeaderText = "Remark"
        Me.DBRemark1.Name = "DBRemark1"
        Me.DBRemark1.ReadOnly = True
        Me.DBRemark1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DBRemark1.Width = 320
        '
        'ColorName1
        '
        DataGridViewCellStyle30.BackColor = System.Drawing.Color.LightGreen
        Me.ColorName1.DefaultCellStyle = DataGridViewCellStyle30
        Me.ColorName1.HeaderText = "Color Name"
        Me.ColorName1.Name = "ColorName1"
        Me.ColorName1.ReadOnly = True
        Me.ColorName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColorName1.Width = 120
        '
        'DBRecord1
        '
        DataGridViewCellStyle31.BackColor = System.Drawing.Color.LightGreen
        Me.DBRecord1.DefaultCellStyle = DataGridViewCellStyle31
        Me.DBRecord1.HeaderText = "ID"
        Me.DBRecord1.Name = "DBRecord1"
        Me.DBRecord1.ReadOnly = True
        Me.DBRecord1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DBRecord1.Width = 50
        '
        'DBShow1
        '
        DataGridViewCellStyle32.BackColor = System.Drawing.Color.LightGreen
        Me.DBShow1.DefaultCellStyle = DataGridViewCellStyle32
        Me.DBShow1.HeaderText = "Show"
        Me.DBShow1.Name = "DBShow1"
        Me.DBShow1.ReadOnly = True
        Me.DBShow1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DBShow1.Visible = False
        Me.DBShow1.Width = 70
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.LightGreen
        Me.GroupBox5.Controls.Add(Me.BtnClose)
        Me.GroupBox5.Controls.Add(Me.BtnChangeDB)
        Me.GroupBox5.Controls.Add(Me.BtnAdd)
        Me.GroupBox5.Controls.Add(Me.BtnDBDel)
        Me.GroupBox5.Controls.Add(Me.btnbrowse)
        Me.GroupBox5.Controls.Add(Me.BtnView2)
        Me.GroupBox5.Controls.Add(Me.txtPath)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.btnRestore)
        Me.GroupBox5.Controls.Add(Me.ProgressBar1)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox5.Location = New System.Drawing.Point(3, 294)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(1248, 132)
        Me.GroupBox5.TabIndex = 714
        Me.GroupBox5.TabStop = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.LightGreen
        Me.BtnClose.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(610, 87)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(81, 37)
        Me.BtnClose.TabIndex = 718
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'BtnChangeDB
        '
        Me.BtnChangeDB.BackColor = System.Drawing.Color.LightGreen
        Me.BtnChangeDB.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnChangeDB.Location = New System.Drawing.Point(414, 87)
        Me.BtnChangeDB.Name = "BtnChangeDB"
        Me.BtnChangeDB.Size = New System.Drawing.Size(183, 37)
        Me.BtnChangeDB.TabIndex = 717
        Me.BtnChangeDB.Text = "Change Connection"
        Me.BtnChangeDB.UseVisualStyleBackColor = False
        '
        'BtnAdd
        '
        Me.BtnAdd.BackColor = System.Drawing.Color.LightGreen
        Me.BtnAdd.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(102, 87)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(81, 37)
        Me.BtnAdd.TabIndex = 716
        Me.BtnAdd.Text = "Add"
        Me.BtnAdd.UseVisualStyleBackColor = False
        '
        'BtnDBDel
        '
        Me.BtnDBDel.BackColor = System.Drawing.Color.LightGreen
        Me.BtnDBDel.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDBDel.Location = New System.Drawing.Point(196, 87)
        Me.BtnDBDel.Name = "BtnDBDel"
        Me.BtnDBDel.Size = New System.Drawing.Size(81, 37)
        Me.BtnDBDel.TabIndex = 714
        Me.BtnDBDel.Text = "Delete"
        Me.BtnDBDel.UseVisualStyleBackColor = False
        '
        'btnbrowse
        '
        Me.btnbrowse.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrowse.Location = New System.Drawing.Point(595, 20)
        Me.btnbrowse.Name = "btnbrowse"
        Me.btnbrowse.Size = New System.Drawing.Size(36, 26)
        Me.btnbrowse.TabIndex = 6
        Me.btnbrowse.Text = ". . ."
        Me.btnbrowse.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnbrowse.UseVisualStyleBackColor = True
        '
        'BtnView2
        '
        Me.BtnView2.BackColor = System.Drawing.Color.LightGreen
        Me.BtnView2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnView2.Location = New System.Drawing.Point(8, 87)
        Me.BtnView2.Name = "BtnView2"
        Me.BtnView2.Size = New System.Drawing.Size(81, 37)
        Me.BtnView2.TabIndex = 713
        Me.BtnView2.Text = "Preview"
        Me.BtnView2.UseVisualStyleBackColor = False
        '
        'txtPath
        '
        Me.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPath.Location = New System.Drawing.Point(145, 22)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(444, 26)
        Me.txtPath.TabIndex = 5
        Me.txtPath.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 20)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Database Path"
        '
        'btnRestore
        '
        Me.btnRestore.BackColor = System.Drawing.Color.LightGreen
        Me.btnRestore.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestore.Location = New System.Drawing.Point(290, 87)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(111, 37)
        Me.btnRestore.TabIndex = 1
        Me.btnRestore.Text = "Restore"
        Me.btnRestore.UseVisualStyleBackColor = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.ForeColor = System.Drawing.Color.LimeGreen
        Me.ProgressBar1.Location = New System.Drawing.Point(145, 54)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(444, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'FrmDBChange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1268, 465)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDBChange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GrpAdd.ResumeLayout(False)
        Me.GrpAdd.PerformLayout()
        CType(Me.DgvDBList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents btnRestore As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnbrowse As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents DgvDBList As System.Windows.Forms.DataGridView
    Friend WithEvents BtnView2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnDBDel As System.Windows.Forms.Button
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents GrpAdd As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSysName As System.Windows.Forms.TextBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnGrpClose As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDBRemark As System.Windows.Forms.TextBox
    Friend WithEvents txtSqlName As System.Windows.Forms.TextBox
    Friend WithEvents txtDBName As System.Windows.Forms.TextBox
    Friend WithEvents BtnChangeDB As System.Windows.Forms.Button
    Friend WithEvents SNo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents SystemName1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SQLName1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DBName1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DBRemark1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColorName1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DBRecord1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DBShow1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnClose As System.Windows.Forms.Button
End Class
