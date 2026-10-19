<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserRoles
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGVUserName = New System.Windows.Forms.DataGridView()
        Me.Tick = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.EmpName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UserName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GrpUser = New System.Windows.Forms.GroupBox()
        Me.DgvEmpList = New System.Windows.Forms.DataGridView()
        Me.DgvUserList = New System.Windows.Forms.DataGridView()
        Me.SNoU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TickU2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.EmpIDU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmpNameU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UserNameU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PasswordU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PwShow = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CbNewShow = New System.Windows.Forms.CheckBox()
        Me.BtnGrpClose = New System.Windows.Forms.Button()
        Me.BtnUserSave = New System.Windows.Forms.Button()
        Me.BtnUserDel = New System.Windows.Forms.Button()
        Me.BtnUserEdit = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtEmpName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEmpID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.DgvMainMenu = New System.Windows.Forms.DataGridView()
        Me.TickM = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MenuName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DisplayName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecordNoM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnMenuCheck = New System.Windows.Forms.Button()
        Me.BtnUserAdd = New System.Windows.Forms.Button()
        Me.BtnMenuInsert = New System.Windows.Forms.Button()
        Me.BtnMenuSave = New System.Windows.Forms.Button()
        Me.CbMenuSelectAll = New System.Windows.Forms.CheckBox()
        CType(Me.DGVUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GrpUser.SuspendLayout()
        CType(Me.DgvEmpList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvUserList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.DgvMainMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGVUserName
        '
        Me.DGVUserName.AllowUserToAddRows = False
        Me.DGVUserName.AllowUserToResizeRows = False
        Me.DGVUserName.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.HotPink
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVUserName.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVUserName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVUserName.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Tick, Me.EmpName, Me.UserName})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVUserName.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGVUserName.Dock = System.Windows.Forms.DockStyle.Left
        Me.DGVUserName.EnableHeadersVisualStyles = False
        Me.DGVUserName.Location = New System.Drawing.Point(0, 0)
        Me.DGVUserName.Name = "DGVUserName"
        Me.DGVUserName.RowHeadersVisible = False
        Me.DGVUserName.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVUserName.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGVUserName.Size = New System.Drawing.Size(414, 495)
        Me.DGVUserName.TabIndex = 18
        '
        'Tick
        '
        Me.Tick.FalseValue = "NO"
        Me.Tick.HeaderText = "Tick"
        Me.Tick.Name = "Tick"
        Me.Tick.TrueValue = "YES"
        Me.Tick.Width = 35
        '
        'EmpName
        '
        Me.EmpName.HeaderText = "Employee Name"
        Me.EmpName.Name = "EmpName"
        Me.EmpName.ReadOnly = True
        Me.EmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.EmpName.Width = 250
        '
        'UserName
        '
        Me.UserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.UserName.HeaderText = "User Name"
        Me.UserName.Name = "UserName"
        Me.UserName.ReadOnly = True
        Me.UserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BtnClose
        '
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(637, 7)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(80, 33)
        Me.BtnClose.TabIndex = 17
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GrpUser)
        Me.Panel1.Controls.Add(Me.DgvMainMenu)
        Me.Panel1.Controls.Add(Me.DGVUserName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 47)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(782, 495)
        Me.Panel1.TabIndex = 2
        '
        'GrpUser
        '
        Me.GrpUser.BackColor = System.Drawing.Color.LightSteelBlue
        Me.GrpUser.Controls.Add(Me.DgvEmpList)
        Me.GrpUser.Controls.Add(Me.DgvUserList)
        Me.GrpUser.Controls.Add(Me.Panel3)
        Me.GrpUser.Font = New System.Drawing.Font("Tw Cen MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpUser.Location = New System.Drawing.Point(28, 21)
        Me.GrpUser.Name = "GrpUser"
        Me.GrpUser.Size = New System.Drawing.Size(711, 449)
        Me.GrpUser.TabIndex = 20
        Me.GrpUser.TabStop = False
        '
        'DgvEmpList
        '
        Me.DgvEmpList.AllowUserToAddRows = False
        Me.DgvEmpList.AllowUserToDeleteRows = False
        Me.DgvEmpList.BackgroundColor = System.Drawing.Color.White
        Me.DgvEmpList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvEmpList.Location = New System.Drawing.Point(123, 48)
        Me.DgvEmpList.Name = "DgvEmpList"
        Me.DgvEmpList.ReadOnly = True
        Me.DgvEmpList.RowHeadersVisible = False
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvEmpList.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DgvEmpList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvEmpList.Size = New System.Drawing.Size(19, 131)
        Me.DgvEmpList.TabIndex = 851
        Me.DgvEmpList.TabStop = False
        '
        'DgvUserList
        '
        Me.DgvUserList.AllowUserToAddRows = False
        Me.DgvUserList.AllowUserToDeleteRows = False
        Me.DgvUserList.AllowUserToResizeRows = False
        Me.DgvUserList.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Navy
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tw Cen MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvUserList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvUserList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNoU, Me.TickU2, Me.EmpIDU, Me.EmpNameU, Me.UserNameU, Me.PasswordU, Me.PwShow})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tw Cen MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvUserList.DefaultCellStyle = DataGridViewCellStyle8
        Me.DgvUserList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvUserList.EnableHeadersVisualStyles = False
        Me.DgvUserList.Location = New System.Drawing.Point(3, 147)
        Me.DgvUserList.Margin = New System.Windows.Forms.Padding(4)
        Me.DgvUserList.Name = "DgvUserList"
        Me.DgvUserList.RowHeadersVisible = False
        Me.DgvUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvUserList.Size = New System.Drawing.Size(705, 299)
        Me.DgvUserList.TabIndex = 861
        '
        'SNoU
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        Me.SNoU.DefaultCellStyle = DataGridViewCellStyle5
        Me.SNoU.HeaderText = "SNo"
        Me.SNoU.Name = "SNoU"
        Me.SNoU.ReadOnly = True
        Me.SNoU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNoU.Width = 35
        '
        'TickU2
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.NullValue = False
        Me.TickU2.DefaultCellStyle = DataGridViewCellStyle6
        Me.TickU2.FalseValue = "NO"
        Me.TickU2.HeaderText = "T"
        Me.TickU2.Name = "TickU2"
        Me.TickU2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TickU2.TrueValue = "YES"
        Me.TickU2.Visible = False
        Me.TickU2.Width = 35
        '
        'EmpIDU
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        Me.EmpIDU.DefaultCellStyle = DataGridViewCellStyle7
        Me.EmpIDU.HeaderText = "Emp ID"
        Me.EmpIDU.Name = "EmpIDU"
        Me.EmpIDU.ReadOnly = True
        Me.EmpIDU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.EmpIDU.Width = 50
        '
        'EmpNameU
        '
        Me.EmpNameU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.EmpNameU.HeaderText = "Emp Name"
        Me.EmpNameU.Name = "EmpNameU"
        Me.EmpNameU.ReadOnly = True
        Me.EmpNameU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'UserNameU
        '
        Me.UserNameU.HeaderText = "User Name"
        Me.UserNameU.Name = "UserNameU"
        Me.UserNameU.ReadOnly = True
        Me.UserNameU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PasswordU
        '
        Me.PasswordU.HeaderText = "Password"
        Me.PasswordU.Name = "PasswordU"
        Me.PasswordU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PwShow
        '
        Me.PwShow.FalseValue = "NO"
        Me.PwShow.HeaderText = "Show"
        Me.PwShow.Name = "PwShow"
        Me.PwShow.TrueValue = "YES"
        Me.PwShow.Width = 40
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.BtnClear)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.CbNewShow)
        Me.Panel3.Controls.Add(Me.BtnGrpClose)
        Me.Panel3.Controls.Add(Me.BtnUserSave)
        Me.Panel3.Controls.Add(Me.BtnUserDel)
        Me.Panel3.Controls.Add(Me.BtnUserEdit)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.txtEmpName)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.txtEmpID)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.txtUserName)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.txtPassword)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 18)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(705, 129)
        Me.Panel3.TabIndex = 860
        '
        'BtnClear
        '
        Me.BtnClear.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnClear.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClear.ForeColor = System.Drawing.Color.White
        Me.BtnClear.Location = New System.Drawing.Point(415, 88)
        Me.BtnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(76, 31)
        Me.BtnClear.TabIndex = 867
        Me.BtnClear.Text = "Clear"
        Me.BtnClear.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(387, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(202, 19)
        Me.Label7.TabIndex = 866
        Me.Label7.Text = "( Max 10 Letters && Numbers)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(312, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(122, 19)
        Me.Label6.TabIndex = 865
        Me.Label6.Text = "( Max 10 Letters)"
        '
        'CbNewShow
        '
        Me.CbNewShow.AutoSize = True
        Me.CbNewShow.Font = New System.Drawing.Font("Cambria", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbNewShow.ForeColor = System.Drawing.Color.Black
        Me.CbNewShow.Location = New System.Drawing.Point(312, 55)
        Me.CbNewShow.Name = "CbNewShow"
        Me.CbNewShow.Size = New System.Drawing.Size(66, 23)
        Me.CbNewShow.TabIndex = 864
        Me.CbNewShow.Text = "Show"
        Me.CbNewShow.UseVisualStyleBackColor = True
        '
        'BtnGrpClose
        '
        Me.BtnGrpClose.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnGrpClose.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGrpClose.ForeColor = System.Drawing.Color.White
        Me.BtnGrpClose.Location = New System.Drawing.Point(503, 88)
        Me.BtnGrpClose.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnGrpClose.Name = "BtnGrpClose"
        Me.BtnGrpClose.Size = New System.Drawing.Size(76, 31)
        Me.BtnGrpClose.TabIndex = 863
        Me.BtnGrpClose.Text = "Close"
        Me.BtnGrpClose.UseVisualStyleBackColor = False
        '
        'BtnUserSave
        '
        Me.BtnUserSave.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUserSave.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUserSave.ForeColor = System.Drawing.Color.White
        Me.BtnUserSave.Location = New System.Drawing.Point(122, 88)
        Me.BtnUserSave.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnUserSave.Name = "BtnUserSave"
        Me.BtnUserSave.Size = New System.Drawing.Size(76, 31)
        Me.BtnUserSave.TabIndex = 862
        Me.BtnUserSave.Text = "Add"
        Me.BtnUserSave.UseVisualStyleBackColor = False
        '
        'BtnUserDel
        '
        Me.BtnUserDel.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUserDel.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUserDel.ForeColor = System.Drawing.Color.White
        Me.BtnUserDel.Location = New System.Drawing.Point(327, 88)
        Me.BtnUserDel.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnUserDel.Name = "BtnUserDel"
        Me.BtnUserDel.Size = New System.Drawing.Size(76, 31)
        Me.BtnUserDel.TabIndex = 861
        Me.BtnUserDel.Text = "Delete"
        Me.BtnUserDel.UseVisualStyleBackColor = False
        '
        'BtnUserEdit
        '
        Me.BtnUserEdit.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUserEdit.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUserEdit.ForeColor = System.Drawing.Color.White
        Me.BtnUserEdit.Location = New System.Drawing.Point(210, 88)
        Me.BtnUserEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnUserEdit.Name = "BtnUserEdit"
        Me.BtnUserEdit.Size = New System.Drawing.Size(105, 31)
        Me.BtnUserEdit.TabIndex = 860
        Me.BtnUserEdit.Text = "Edit && Save"
        Me.BtnUserEdit.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(96, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 19)
        Me.Label5.TabIndex = 859
        Me.Label5.Text = "*"
        '
        'txtEmpName
        '
        Me.txtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEmpName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpName.ForeColor = System.Drawing.Color.Black
        Me.txtEmpName.Location = New System.Drawing.Point(120, 3)
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.Size = New System.Drawing.Size(377, 26)
        Me.txtEmpName.TabIndex = 849
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(96, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 19)
        Me.Label4.TabIndex = 858
        Me.Label4.Text = "*"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 7)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(77, 19)
        Me.Label19.TabIndex = 850
        Me.Label19.Text = "Employee"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(96, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 19)
        Me.Label3.TabIndex = 857
        Me.Label3.Text = "*"
        '
        'txtEmpID
        '
        Me.txtEmpID.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtEmpID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpID.Location = New System.Drawing.Point(497, 3)
        Me.txtEmpID.Name = "txtEmpID"
        Me.txtEmpID.ReadOnly = True
        Me.txtEmpID.Size = New System.Drawing.Size(68, 26)
        Me.txtEmpID.TabIndex = 852
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 19)
        Me.Label2.TabIndex = 856
        Me.Label2.Text = "Password"
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.txtUserName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.ForeColor = System.Drawing.Color.Black
        Me.txtUserName.Location = New System.Drawing.Point(120, 29)
        Me.txtUserName.MaxLength = 10
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(186, 26)
        Me.txtUserName.TabIndex = 853
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 19)
        Me.Label1.TabIndex = 855
        Me.Label1.Text = "User Name"
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.txtPassword.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.Black
        Me.txtPassword.Location = New System.Drawing.Point(120, 55)
        Me.txtPassword.MaxLength = 10
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(186, 26)
        Me.txtPassword.TabIndex = 854
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'DgvMainMenu
        '
        Me.DgvMainMenu.AllowUserToAddRows = False
        Me.DgvMainMenu.AllowUserToResizeRows = False
        Me.DgvMainMenu.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.HotPink
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvMainMenu.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.DgvMainMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvMainMenu.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TickM, Me.MenuName, Me.DisplayName, Me.RecordNoM})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tw Cen MT", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvMainMenu.DefaultCellStyle = DataGridViewCellStyle10
        Me.DgvMainMenu.Dock = System.Windows.Forms.DockStyle.Left
        Me.DgvMainMenu.EnableHeadersVisualStyles = False
        Me.DgvMainMenu.Location = New System.Drawing.Point(414, 0)
        Me.DgvMainMenu.Name = "DgvMainMenu"
        Me.DgvMainMenu.RowHeadersVisible = False
        Me.DgvMainMenu.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tw Cen MT", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvMainMenu.Size = New System.Drawing.Size(350, 495)
        Me.DgvMainMenu.TabIndex = 19
        '
        'TickM
        '
        Me.TickM.DataPropertyName = "MenuStatus"
        Me.TickM.FalseValue = "NO"
        Me.TickM.HeaderText = "Tick"
        Me.TickM.Name = "TickM"
        Me.TickM.TrueValue = "YES"
        Me.TickM.Width = 40
        '
        'MenuName
        '
        Me.MenuName.HeaderText = "Menu Name"
        Me.MenuName.Name = "MenuName"
        Me.MenuName.ReadOnly = True
        Me.MenuName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MenuName.Visible = False
        '
        'DisplayName
        '
        Me.DisplayName.HeaderText = "Particular"
        Me.DisplayName.Name = "DisplayName"
        Me.DisplayName.ReadOnly = True
        Me.DisplayName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DisplayName.Width = 300
        '
        'RecordNoM
        '
        Me.RecordNoM.HeaderText = "Record No"
        Me.RecordNoM.Name = "RecordNoM"
        Me.RecordNoM.ReadOnly = True
        Me.RecordNoM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RecordNoM.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightPink
        Me.Panel2.Controls.Add(Me.BtnMenuCheck)
        Me.Panel2.Controls.Add(Me.BtnUserAdd)
        Me.Panel2.Controls.Add(Me.BtnMenuInsert)
        Me.Panel2.Controls.Add(Me.BtnMenuSave)
        Me.Panel2.Controls.Add(Me.CbMenuSelectAll)
        Me.Panel2.Controls.Add(Me.BtnClose)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(782, 47)
        Me.Panel2.TabIndex = 3
        '
        'BtnMenuCheck
        '
        Me.BtnMenuCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMenuCheck.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMenuCheck.Location = New System.Drawing.Point(265, 7)
        Me.BtnMenuCheck.Name = "BtnMenuCheck"
        Me.BtnMenuCheck.Size = New System.Drawing.Size(134, 33)
        Me.BtnMenuCheck.TabIndex = 765
        Me.BtnMenuCheck.Text = "Menu Check"
        Me.BtnMenuCheck.UseVisualStyleBackColor = True
        '
        'BtnUserAdd
        '
        Me.BtnUserAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUserAdd.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUserAdd.Location = New System.Drawing.Point(13, 7)
        Me.BtnUserAdd.Name = "BtnUserAdd"
        Me.BtnUserAdd.Size = New System.Drawing.Size(98, 33)
        Me.BtnUserAdd.TabIndex = 761
        Me.BtnUserAdd.Text = "User Add"
        Me.BtnUserAdd.UseVisualStyleBackColor = True
        '
        'BtnMenuInsert
        '
        Me.BtnMenuInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMenuInsert.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMenuInsert.Location = New System.Drawing.Point(127, 7)
        Me.BtnMenuInsert.Name = "BtnMenuInsert"
        Me.BtnMenuInsert.Size = New System.Drawing.Size(122, 33)
        Me.BtnMenuInsert.TabIndex = 760
        Me.BtnMenuInsert.Text = "Menu Insert"
        Me.BtnMenuInsert.UseVisualStyleBackColor = True
        '
        'BtnMenuSave
        '
        Me.BtnMenuSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMenuSave.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMenuSave.Location = New System.Drawing.Point(532, 7)
        Me.BtnMenuSave.Name = "BtnMenuSave"
        Me.BtnMenuSave.Size = New System.Drawing.Size(80, 33)
        Me.BtnMenuSave.TabIndex = 759
        Me.BtnMenuSave.Text = "Save"
        Me.BtnMenuSave.UseVisualStyleBackColor = True
        '
        'CbMenuSelectAll
        '
        Me.CbMenuSelectAll.AutoSize = True
        Me.CbMenuSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbMenuSelectAll.Location = New System.Drawing.Point(421, 12)
        Me.CbMenuSelectAll.Name = "CbMenuSelectAll"
        Me.CbMenuSelectAll.Size = New System.Drawing.Size(97, 22)
        Me.CbMenuSelectAll.TabIndex = 758
        Me.CbMenuSelectAll.Text = "Select All"
        Me.CbMenuSelectAll.UseVisualStyleBackColor = True
        '
        'FrmUserRoles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(782, 542)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmUserRoles"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Roles"
        CType(Me.DGVUserName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GrpUser.ResumeLayout(False)
        CType(Me.DgvEmpList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvUserList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DgvMainMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGVUserName As System.Windows.Forms.DataGridView
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DgvMainMenu As System.Windows.Forms.DataGridView
    Friend WithEvents Tick As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents EmpName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UserName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CbMenuSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents BtnMenuSave As System.Windows.Forms.Button
    Friend WithEvents BtnMenuInsert As System.Windows.Forms.Button
    Friend WithEvents BtnUserAdd As System.Windows.Forms.Button
    Friend WithEvents GrpUser As System.Windows.Forms.GroupBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtEmpName As System.Windows.Forms.TextBox
    Friend WithEvents DgvEmpList As System.Windows.Forms.DataGridView
    Friend WithEvents txtEmpID As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents BtnUserDel As System.Windows.Forms.Button
    Friend WithEvents BtnUserEdit As System.Windows.Forms.Button
    Friend WithEvents BtnGrpClose As System.Windows.Forms.Button
    Friend WithEvents BtnUserSave As System.Windows.Forms.Button
    Friend WithEvents DgvUserList As System.Windows.Forms.DataGridView
    Friend WithEvents CbNewShow As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents SNoU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TickU2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents EmpIDU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmpNameU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UserNameU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PasswordU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PwShow As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents TickM As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents MenuName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DisplayName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordNoM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnMenuCheck As System.Windows.Forms.Button
End Class
