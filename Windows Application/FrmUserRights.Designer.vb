<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserRights
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnUserAdd = New System.Windows.Forms.Button()
        Me.BtnPreview = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
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
        Me.Label8 = New System.Windows.Forms.Label()
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
        Me.DgvMenu = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.GrpUser.SuspendLayout()
        CType(Me.DgvEmpList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvUserList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.DgvMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Bookman Old Style", 12.0!)
        Me.BtnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnSave.Location = New System.Drawing.Point(444, 10)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(98, 30)
        Me.BtnSave.TabIndex = 2
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.BtnUserAdd)
        Me.Panel1.Controls.Add(Me.BtnPreview)
        Me.Panel1.Controls.Add(Me.BtnClose)
        Me.Panel1.Controls.Add(Me.BtnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1038, 46)
        Me.Panel1.TabIndex = 0
        '
        'BtnUserAdd
        '
        Me.BtnUserAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUserAdd.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUserAdd.Location = New System.Drawing.Point(200, 10)
        Me.BtnUserAdd.Name = "BtnUserAdd"
        Me.BtnUserAdd.Size = New System.Drawing.Size(98, 30)
        Me.BtnUserAdd.TabIndex = 766
        Me.BtnUserAdd.Text = "User Add"
        Me.BtnUserAdd.UseVisualStyleBackColor = True
        '
        'BtnPreview
        '
        Me.BtnPreview.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPreview.Font = New System.Drawing.Font("Bookman Old Style", 12.0!)
        Me.BtnPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnPreview.Location = New System.Drawing.Point(322, 10)
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Size = New System.Drawing.Size(98, 30)
        Me.BtnPreview.TabIndex = 3
        Me.BtnPreview.Text = "Preview"
        Me.BtnPreview.UseVisualStyleBackColor = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Bookman Old Style", 12.0!)
        Me.BtnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnClose.Location = New System.Drawing.Point(566, 10)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(98, 30)
        Me.BtnClose.TabIndex = 1
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'GrpUser
        '
        Me.GrpUser.BackColor = System.Drawing.Color.LightSteelBlue
        Me.GrpUser.Controls.Add(Me.DgvEmpList)
        Me.GrpUser.Controls.Add(Me.DgvUserList)
        Me.GrpUser.Controls.Add(Me.Panel3)
        Me.GrpUser.Font = New System.Drawing.Font("Tw Cen MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpUser.Location = New System.Drawing.Point(65, 76)
        Me.GrpUser.Name = "GrpUser"
        Me.GrpUser.Size = New System.Drawing.Size(604, 417)
        Me.GrpUser.TabIndex = 21
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
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvEmpList.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvEmpList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvEmpList.Size = New System.Drawing.Size(19, 52)
        Me.DgvEmpList.TabIndex = 851
        Me.DgvEmpList.TabStop = False
        '
        'DgvUserList
        '
        Me.DgvUserList.AllowUserToAddRows = False
        Me.DgvUserList.AllowUserToDeleteRows = False
        Me.DgvUserList.AllowUserToResizeRows = False
        Me.DgvUserList.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tw Cen MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvUserList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvUserList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNoU, Me.TickU2, Me.EmpIDU, Me.EmpNameU, Me.UserNameU, Me.PasswordU, Me.PwShow})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tw Cen MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvUserList.DefaultCellStyle = DataGridViewCellStyle6
        Me.DgvUserList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvUserList.EnableHeadersVisualStyles = False
        Me.DgvUserList.Location = New System.Drawing.Point(3, 171)
        Me.DgvUserList.Margin = New System.Windows.Forms.Padding(4)
        Me.DgvUserList.Name = "DgvUserList"
        Me.DgvUserList.RowHeadersVisible = False
        Me.DgvUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvUserList.Size = New System.Drawing.Size(598, 243)
        Me.DgvUserList.TabIndex = 861
        '
        'SNoU
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        Me.SNoU.DefaultCellStyle = DataGridViewCellStyle3
        Me.SNoU.HeaderText = "SNo"
        Me.SNoU.Name = "SNoU"
        Me.SNoU.ReadOnly = True
        Me.SNoU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNoU.Width = 35
        '
        'TickU2
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.NullValue = False
        Me.TickU2.DefaultCellStyle = DataGridViewCellStyle4
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
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        Me.EmpIDU.DefaultCellStyle = DataGridViewCellStyle5
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
        Me.Panel3.Controls.Add(Me.Label8)
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
        Me.Panel3.Size = New System.Drawing.Size(598, 153)
        Me.Panel3.TabIndex = 860
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 125)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(327, 19)
        Me.Label8.TabIndex = 868
        Me.Label8.Text = "Password Change ---> Emp Name Double Click"
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
        Me.Label7.Location = New System.Drawing.Point(311, 61)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 19)
        Me.Label7.TabIndex = 866
        Me.Label7.Text = "( Max 10 Letters)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(311, 32)
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
        Me.CbNewShow.Location = New System.Drawing.Point(440, 59)
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
        'DgvMenu
        '
        Me.DgvMenu.AllowUserToAddRows = False
        Me.DgvMenu.AllowUserToResizeRows = False
        Me.DgvMenu.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.Green
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvMenu.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DgvMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvMenu.EnableHeadersVisualStyles = False
        Me.DgvMenu.GridColor = System.Drawing.SystemColors.AppWorkspace
        Me.DgvMenu.Location = New System.Drawing.Point(0, 46)
        Me.DgvMenu.Name = "DgvMenu"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvMenu.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DgvMenu.RowHeadersVisible = False
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvMenu.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DgvMenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvMenu.Size = New System.Drawing.Size(1038, 564)
        Me.DgvMenu.TabIndex = 22
        '
        'FrmUserRights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1038, 610)
        Me.Controls.Add(Me.GrpUser)
        Me.Controls.Add(Me.DgvMenu)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmUserRights"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Rights"
        Me.Panel1.ResumeLayout(False)
        Me.GrpUser.ResumeLayout(False)
        CType(Me.DgvEmpList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvUserList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DgvMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnPreview As System.Windows.Forms.Button
    Friend WithEvents GrpUser As System.Windows.Forms.GroupBox
    Friend WithEvents DgvEmpList As System.Windows.Forms.DataGridView
    Friend WithEvents DgvUserList As System.Windows.Forms.DataGridView
    Friend WithEvents SNoU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TickU2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents EmpIDU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmpNameU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UserNameU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PasswordU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PwShow As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CbNewShow As System.Windows.Forms.CheckBox
    Friend WithEvents BtnGrpClose As System.Windows.Forms.Button
    Friend WithEvents BtnUserSave As System.Windows.Forms.Button
    Friend WithEvents BtnUserDel As System.Windows.Forms.Button
    Friend WithEvents BtnUserEdit As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEmpName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEmpID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents BtnUserAdd As System.Windows.Forms.Button
    Friend WithEvents DgvMenu As System.Windows.Forms.DataGridView
End Class
