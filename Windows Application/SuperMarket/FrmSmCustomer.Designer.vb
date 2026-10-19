<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSmCustomer
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
        Me.DgvCust = New System.Windows.Forms.DataGridView()
        Me.SNo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C_RecordNumber_i = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_ID_n = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_Name_vc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_Mobile_vc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_GSTIN_vc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_OpBal_n = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Active = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBoxSearch = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCustCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtAdd1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtAdd2 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtAdd3 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPincode = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtStateCode = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtGSTIN = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtPAN = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtOpBal = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboOpType = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtCreditLimit = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDueDays = New System.Windows.Forms.TextBox()
        Me.chkShow = New System.Windows.Forms.CheckBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DgvCust, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxSearch.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DgvCust)
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
        'DgvCust
        '
        Me.DgvCust.AllowUserToAddRows = False
        Me.DgvCust.AllowUserToDeleteRows = False
        Me.DgvCust.AllowUserToResizeRows = False
        Me.DgvCust.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Olive
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvCust.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvCust.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCust.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo2, Me.Tick2, Me.C_RecordNumber_i, Me.C_ID_n, Me.C_Name_vc, Me.C_Mobile_vc, Me.C_GSTIN_vc, Me.C_OpBal_n, Me.Active})
        Me.DgvCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvCust.EnableHeadersVisualStyles = False
        Me.DgvCust.Location = New System.Drawing.Point(3, 272)
        Me.DgvCust.Name = "DgvCust"
        Me.DgvCust.RowHeadersVisible = False
        Me.DgvCust.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvCust.Size = New System.Drawing.Size(1094, 425)
        Me.DgvCust.TabIndex = 1
        '
        'SNo2
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkKhaki
        Me.SNo2.DefaultCellStyle = DataGridViewCellStyle2
        Me.SNo2.HeaderText = "SNo"
        Me.SNo2.Name = "SNo2"
        Me.SNo2.ReadOnly = True
        Me.SNo2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo2.Width = 55
        '
        'Tick2
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkKhaki
        DataGridViewCellStyle3.NullValue = False
        Me.Tick2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Tick2.FalseValue = "NO"
        Me.Tick2.HeaderText = "T"
        Me.Tick2.Name = "Tick2"
        Me.Tick2.TrueValue = "YES"
        Me.Tick2.Width = 45
        '
        'C_RecordNumber_i
        '
        Me.C_RecordNumber_i.HeaderText = "Rec"
        Me.C_RecordNumber_i.Name = "C_RecordNumber_i"
        Me.C_RecordNumber_i.ReadOnly = True
        Me.C_RecordNumber_i.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_RecordNumber_i.Visible = False
        '
        'C_ID_n
        '
        Me.C_ID_n.HeaderText = "Code"
        Me.C_ID_n.Name = "C_ID_n"
        Me.C_ID_n.ReadOnly = True
        Me.C_ID_n.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_ID_n.Width = 70
        '
        'C_Name_vc
        '
        Me.C_Name_vc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.C_Name_vc.HeaderText = "Name"
        Me.C_Name_vc.Name = "C_Name_vc"
        Me.C_Name_vc.ReadOnly = True
        Me.C_Name_vc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'C_Mobile_vc
        '
        Me.C_Mobile_vc.HeaderText = "Mobile"
        Me.C_Mobile_vc.Name = "C_Mobile_vc"
        Me.C_Mobile_vc.ReadOnly = True
        Me.C_Mobile_vc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_Mobile_vc.Width = 120
        '
        'C_GSTIN_vc
        '
        Me.C_GSTIN_vc.HeaderText = "GSTIN"
        Me.C_GSTIN_vc.Name = "C_GSTIN_vc"
        Me.C_GSTIN_vc.ReadOnly = True
        Me.C_GSTIN_vc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_GSTIN_vc.Width = 170
        '
        'C_OpBal_n
        '
        Me.C_OpBal_n.HeaderText = "Op. Balance"
        Me.C_OpBal_n.Name = "C_OpBal_n"
        Me.C_OpBal_n.ReadOnly = True
        Me.C_OpBal_n.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_OpBal_n.Width = 110
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
        Me.GroupBoxSearch.TabIndex = 2
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
        Me.GroupBox4.Controls.Add(Me.txtCustCode)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtCustName)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txtMobile)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.txtEmail)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtAdd1)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.txtAdd2)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.txtAdd3)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtPincode)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.txtStateCode)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.txtGSTIN)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.txtPAN)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.txtOpBal)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.cboOpType)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.txtCreditLimit)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtDueDays)
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
        Me.Label3.Size = New System.Drawing.Size(46, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Code"
        '
        'txtCustCode
        '
        Me.txtCustCode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustCode.Location = New System.Drawing.Point(100, 18)
        Me.txtCustCode.Name = "txtCustCode"
        Me.txtCustCode.ReadOnly = True
        Me.txtCustCode.Size = New System.Drawing.Size(140, 25)
        Me.txtCustCode.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(250, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Name"
        '
        'txtCustName
        '
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.Location = New System.Drawing.Point(342, 18)
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(250, 25)
        Me.txtCustName.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(612, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 17)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Mobile"
        '
        'txtMobile
        '
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.Location = New System.Drawing.Point(704, 18)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(140, 25)
        Me.txtMobile.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(866, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 17)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(900, 18)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(180, 25)
        Me.txtEmail.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 54)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 17)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Address1"
        '
        'txtAdd1
        '
        Me.txtAdd1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdd1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd1.Location = New System.Drawing.Point(100, 50)
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(492, 25)
        Me.txtAdd1.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(612, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 17)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Address2"
        '
        'txtAdd2
        '
        Me.txtAdd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdd2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd2.Location = New System.Drawing.Point(700, 50)
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(380, 25)
        Me.txtAdd2.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 86)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 17)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Address3"
        '
        'txtAdd3
        '
        Me.txtAdd3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdd3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd3.Location = New System.Drawing.Point(100, 82)
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.Size = New System.Drawing.Size(492, 25)
        Me.txtAdd3.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(612, 86)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 17)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Pincode"
        '
        'txtPincode
        '
        Me.txtPincode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPincode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPincode.Location = New System.Drawing.Point(700, 82)
        Me.txtPincode.Name = "txtPincode"
        Me.txtPincode.Size = New System.Drawing.Size(150, 25)
        Me.txtPincode.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 118)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 17)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "State Code"
        '
        'txtStateCode
        '
        Me.txtStateCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStateCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStateCode.Location = New System.Drawing.Point(100, 114)
        Me.txtStateCode.Name = "txtStateCode"
        Me.txtStateCode.Size = New System.Drawing.Size(140, 25)
        Me.txtStateCode.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(250, 118)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 17)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "GSTIN"
        '
        'txtGSTIN
        '
        Me.txtGSTIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTIN.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTIN.Location = New System.Drawing.Point(342, 114)
        Me.txtGSTIN.Name = "txtGSTIN"
        Me.txtGSTIN.Size = New System.Drawing.Size(250, 25)
        Me.txtGSTIN.TabIndex = 17
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(612, 118)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(34, 17)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "PAN"
        '
        'txtPAN
        '
        Me.txtPAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPAN.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPAN.Location = New System.Drawing.Point(700, 114)
        Me.txtPAN.Name = "txtPAN"
        Me.txtPAN.Size = New System.Drawing.Size(150, 25)
        Me.txtPAN.TabIndex = 19
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 150)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(118, 17)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Opening Balance"
        '
        'txtOpBal
        '
        Me.txtOpBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOpBal.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpBal.Location = New System.Drawing.Point(130, 146)
        Me.txtOpBal.Name = "txtOpBal"
        Me.txtOpBal.Size = New System.Drawing.Size(110, 25)
        Me.txtOpBal.TabIndex = 21
        Me.txtOpBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(250, 150)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 17)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "OP Type"
        '
        'cboOpType
        '
        Me.cboOpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOpType.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOpType.FormattingEnabled = True
        Me.cboOpType.Items.AddRange(New Object() {"Debit", "Credit"})
        Me.cboOpType.Location = New System.Drawing.Point(342, 146)
        Me.cboOpType.Name = "cboOpType"
        Me.cboOpType.Size = New System.Drawing.Size(120, 26)
        Me.cboOpType.TabIndex = 23
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(480, 150)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 17)
        Me.Label17.TabIndex = 24
        Me.Label17.Text = "Credit Limit"
        '
        'txtCreditLimit
        '
        Me.txtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditLimit.Location = New System.Drawing.Point(570, 146)
        Me.txtCreditLimit.Name = "txtCreditLimit"
        Me.txtCreditLimit.Size = New System.Drawing.Size(120, 25)
        Me.txtCreditLimit.TabIndex = 25
        Me.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(700, 150)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(64, 17)
        Me.Label18.TabIndex = 26
        Me.Label18.Text = "Due Days"
        '
        'txtDueDays
        '
        Me.txtDueDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDueDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDueDays.Location = New System.Drawing.Point(782, 146)
        Me.txtDueDays.Name = "txtDueDays"
        Me.txtDueDays.Size = New System.Drawing.Size(90, 25)
        Me.txtDueDays.TabIndex = 27
        Me.txtDueDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkShow
        '
        Me.chkShow.AutoSize = True
        Me.chkShow.Checked = True
        Me.chkShow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShow.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShow.Location = New System.Drawing.Point(920, 148)
        Me.chkShow.Name = "chkShow"
        Me.chkShow.Size = New System.Drawing.Size(68, 21)
        Me.chkShow.TabIndex = 28
        Me.chkShow.Text = "Active"
        Me.chkShow.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.Olive
        Me.BtnSave.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(130, 170)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(90, 28)
        Me.BtnSave.TabIndex = 29
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'BtnEdit
        '
        Me.BtnEdit.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnEdit.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(226, 170)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(90, 28)
        Me.BtnEdit.TabIndex = 30
        Me.BtnEdit.Text = "Edit"
        Me.BtnEdit.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnDelete.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.Location = New System.Drawing.Point(322, 170)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(90, 28)
        Me.BtnDelete.TabIndex = 31
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = False
        '
        'BtnClear
        '
        Me.BtnClear.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnClear.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClear.Location = New System.Drawing.Point(418, 170)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(90, 28)
        Me.BtnClear.TabIndex = 32
        Me.BtnClear.Text = "New"
        Me.BtnClear.UseVisualStyleBackColor = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.DarkKhaki
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(514, 170)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(90, 28)
        Me.BtnClose.TabIndex = 33
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'FrmSmCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 700)
        Me.Controls.Add(Me.GroupBox3)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "FrmSmCustomer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer Master"
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.DgvCust, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxSearch.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DgvCust As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxSearch As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents BtnExport As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCustCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAdd3 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPincode As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtStateCode As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtGSTIN As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtPAN As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtOpBal As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboOpType As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtCreditLimit As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDueDays As System.Windows.Forms.TextBox
    Friend WithEvents chkShow As System.Windows.Forms.CheckBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnEdit As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents SNo2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents C_RecordNumber_i As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_ID_n As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Name_vc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Mobile_vc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_GSTIN_vc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_OpBal_n As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Active As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
