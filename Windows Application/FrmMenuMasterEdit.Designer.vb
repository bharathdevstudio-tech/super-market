<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMenuMasterEdit
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.Btnadd = New System.Windows.Forms.Button()
        Me.BtnPreview = New System.Windows.Forms.Button()
        Me.BtnClose2 = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.DgvMenuMaster = New System.Windows.Forms.DataGridView()
        Me.GrpAdd = New System.Windows.Forms.GroupBox()
        Me.txtParentID = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMenuOrder = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFromOrder = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.CbFormState = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFormName = New System.Windows.Forms.TextBox()
        Me.BtnAddSave = New System.Windows.Forms.Button()
        Me.txtLedCode = New System.Windows.Forms.TextBox()
        Me.txtMenuName = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MenuID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FormName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FormState = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ParentID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuNameOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FormNameOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DgvMenuMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpAdd.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.LightBlue
        Me.GroupBox2.Controls.Add(Me.BtnDelete)
        Me.GroupBox2.Controls.Add(Me.Btnadd)
        Me.GroupBox2.Controls.Add(Me.BtnPreview)
        Me.GroupBox2.Controls.Add(Me.BtnClose2)
        Me.GroupBox2.Controls.Add(Me.BtnSave)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(744, 50)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        '
        'BtnDelete
        '
        Me.BtnDelete.BackColor = System.Drawing.Color.LightBlue
        Me.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDelete.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.Color.Brown
        Me.BtnDelete.Location = New System.Drawing.Point(417, 12)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(82, 31)
        Me.BtnDelete.TabIndex = 75
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = False
        '
        'Btnadd
        '
        Me.Btnadd.BackColor = System.Drawing.Color.LightBlue
        Me.Btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btnadd.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnadd.ForeColor = System.Drawing.Color.Brown
        Me.Btnadd.Location = New System.Drawing.Point(38, 12)
        Me.Btnadd.Name = "Btnadd"
        Me.Btnadd.Size = New System.Drawing.Size(82, 31)
        Me.Btnadd.TabIndex = 74
        Me.Btnadd.Text = "Add"
        Me.Btnadd.UseVisualStyleBackColor = False
        '
        'BtnPreview
        '
        Me.BtnPreview.BackColor = System.Drawing.Color.LightBlue
        Me.BtnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPreview.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPreview.ForeColor = System.Drawing.Color.Brown
        Me.BtnPreview.Location = New System.Drawing.Point(191, 13)
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Size = New System.Drawing.Size(82, 31)
        Me.BtnPreview.TabIndex = 73
        Me.BtnPreview.Text = "Preview"
        Me.BtnPreview.UseVisualStyleBackColor = False
        '
        'BtnClose2
        '
        Me.BtnClose2.BackColor = System.Drawing.Color.LightBlue
        Me.BtnClose2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose2.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose2.ForeColor = System.Drawing.Color.Brown
        Me.BtnClose2.Location = New System.Drawing.Point(521, 13)
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
        Me.BtnSave.Location = New System.Drawing.Point(329, 13)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(82, 31)
        Me.BtnSave.TabIndex = 69
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'DgvMenuMaster
        '
        Me.DgvMenuMaster.AllowUserToAddRows = False
        Me.DgvMenuMaster.AllowUserToDeleteRows = False
        Me.DgvMenuMaster.AllowUserToResizeRows = False
        Me.DgvMenuMaster.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        Me.DgvMenuMaster.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvMenuMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvMenuMaster.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo, Me.Tick, Me.MenuID, Me.MenuName, Me.FormName, Me.FormState, Me.ParentID, Me.MenuNameOrder, Me.FormNameOrder})
        Me.DgvMenuMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvMenuMaster.EnableHeadersVisualStyles = False
        Me.DgvMenuMaster.Location = New System.Drawing.Point(0, 50)
        Me.DgvMenuMaster.Name = "DgvMenuMaster"
        Me.DgvMenuMaster.RowHeadersVisible = False
        Me.DgvMenuMaster.RowHeadersWidth = 44
        Me.DgvMenuMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvMenuMaster.Size = New System.Drawing.Size(744, 442)
        Me.DgvMenuMaster.TabIndex = 23
        '
        'GrpAdd
        '
        Me.GrpAdd.BackColor = System.Drawing.Color.Tan
        Me.GrpAdd.Controls.Add(Me.txtParentID)
        Me.GrpAdd.Controls.Add(Me.Label5)
        Me.GrpAdd.Controls.Add(Me.txtMenuOrder)
        Me.GrpAdd.Controls.Add(Me.Label4)
        Me.GrpAdd.Controls.Add(Me.txtFromOrder)
        Me.GrpAdd.Controls.Add(Me.Label1)
        Me.GrpAdd.Controls.Add(Me.BtnClose)
        Me.GrpAdd.Controls.Add(Me.CbFormState)
        Me.GrpAdd.Controls.Add(Me.Label3)
        Me.GrpAdd.Controls.Add(Me.Label2)
        Me.GrpAdd.Controls.Add(Me.txtFormName)
        Me.GrpAdd.Controls.Add(Me.BtnAddSave)
        Me.GrpAdd.Controls.Add(Me.txtLedCode)
        Me.GrpAdd.Controls.Add(Me.txtMenuName)
        Me.GrpAdd.Controls.Add(Me.Label10)
        Me.GrpAdd.Location = New System.Drawing.Point(133, 105)
        Me.GrpAdd.Name = "GrpAdd"
        Me.GrpAdd.Size = New System.Drawing.Size(426, 264)
        Me.GrpAdd.TabIndex = 24
        Me.GrpAdd.TabStop = False
        '
        'txtParentID
        '
        Me.txtParentID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtParentID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtParentID.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParentID.Location = New System.Drawing.Point(107, 89)
        Me.txtParentID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtParentID.Name = "txtParentID"
        Me.txtParentID.Size = New System.Drawing.Size(213, 25)
        Me.txtParentID.TabIndex = 840
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(10, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 17)
        Me.Label5.TabIndex = 839
        Me.Label5.Text = "Parent ID"
        '
        'txtMenuOrder
        '
        Me.txtMenuOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMenuOrder.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMenuOrder.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMenuOrder.Location = New System.Drawing.Point(107, 114)
        Me.txtMenuOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMenuOrder.Name = "txtMenuOrder"
        Me.txtMenuOrder.Size = New System.Drawing.Size(213, 25)
        Me.txtMenuOrder.TabIndex = 838
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(10, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 17)
        Me.Label4.TabIndex = 837
        Me.Label4.Text = "Menu Order"
        '
        'txtFromOrder
        '
        Me.txtFromOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromOrder.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFromOrder.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromOrder.Location = New System.Drawing.Point(107, 139)
        Me.txtFromOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFromOrder.Name = "txtFromOrder"
        Me.txtFromOrder.Size = New System.Drawing.Size(213, 25)
        Me.txtFromOrder.TabIndex = 836
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 141)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 17)
        Me.Label1.TabIndex = 835
        Me.Label1.Text = "Form Order"
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.Tan
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnClose.Location = New System.Drawing.Point(189, 215)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(69, 29)
        Me.BtnClose.TabIndex = 834
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'CbFormState
        '
        Me.CbFormState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbFormState.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbFormState.FormattingEnabled = True
        Me.CbFormState.Items.AddRange(New Object() {"Maximized", "Normal"})
        Me.CbFormState.Location = New System.Drawing.Point(107, 64)
        Me.CbFormState.Name = "CbFormState"
        Me.CbFormState.Size = New System.Drawing.Size(213, 25)
        Me.CbFormState.TabIndex = 833
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(10, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 17)
        Me.Label3.TabIndex = 832
        Me.Label3.Text = "Form Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 17)
        Me.Label2.TabIndex = 831
        Me.Label2.Text = "Form State"
        '
        'txtFormName
        '
        Me.txtFormName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFormName.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFormName.Location = New System.Drawing.Point(107, 39)
        Me.txtFormName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFormName.Name = "txtFormName"
        Me.txtFormName.Size = New System.Drawing.Size(213, 25)
        Me.txtFormName.TabIndex = 830
        '
        'BtnAddSave
        '
        Me.BtnAddSave.BackColor = System.Drawing.Color.Tan
        Me.BtnAddSave.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddSave.Location = New System.Drawing.Point(27, 215)
        Me.BtnAddSave.Name = "BtnAddSave"
        Me.BtnAddSave.Size = New System.Drawing.Size(69, 29)
        Me.BtnAddSave.TabIndex = 5
        Me.BtnAddSave.Text = "Save"
        Me.BtnAddSave.UseVisualStyleBackColor = False
        '
        'txtLedCode
        '
        Me.txtLedCode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtLedCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLedCode.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLedCode.Location = New System.Drawing.Point(338, 21)
        Me.txtLedCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLedCode.Name = "txtLedCode"
        Me.txtLedCode.ReadOnly = True
        Me.txtLedCode.Size = New System.Drawing.Size(52, 25)
        Me.txtLedCode.TabIndex = 1
        Me.txtLedCode.TabStop = False
        Me.txtLedCode.Visible = False
        '
        'txtMenuName
        '
        Me.txtMenuName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMenuName.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMenuName.Location = New System.Drawing.Point(107, 14)
        Me.txtMenuName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMenuName.Name = "txtMenuName"
        Me.txtMenuName.Size = New System.Drawing.Size(213, 25)
        Me.txtMenuName.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(10, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 17)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Menu Name"
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
        'MenuID
        '
        Me.MenuID.HeaderText = "Menu ID"
        Me.MenuID.Name = "MenuID"
        Me.MenuID.ReadOnly = True
        Me.MenuID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MenuID.Visible = False
        Me.MenuID.Width = 70
        '
        'MenuName
        '
        Me.MenuName.HeaderText = "Menu Name"
        Me.MenuName.Name = "MenuName"
        Me.MenuName.ReadOnly = True
        Me.MenuName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MenuName.Width = 170
        '
        'FormName
        '
        Me.FormName.HeaderText = "Form Name"
        Me.FormName.Name = "FormName"
        Me.FormName.ReadOnly = True
        Me.FormName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.FormName.Width = 200
        '
        'FormState
        '
        Me.FormState.HeaderText = "Form State"
        Me.FormState.Items.AddRange(New Object() {"Maximized", "Normal"})
        Me.FormState.Name = "FormState"
        Me.FormState.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FormState.Width = 130
        '
        'ParentID
        '
        Me.ParentID.HeaderText = "Parent ID"
        Me.ParentID.Name = "ParentID"
        Me.ParentID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ParentID.Width = 80
        '
        'MenuNameOrder
        '
        Me.MenuNameOrder.HeaderText = "MenuName Order"
        Me.MenuNameOrder.Name = "MenuNameOrder"
        Me.MenuNameOrder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MenuNameOrder.Width = 80
        '
        'FormNameOrder
        '
        Me.FormNameOrder.HeaderText = "FormName Order"
        Me.FormNameOrder.Name = "FormNameOrder"
        Me.FormNameOrder.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FormNameOrder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.FormNameOrder.Width = 80
        '
        'FrmMenuMasterEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 492)
        Me.Controls.Add(Me.GrpAdd)
        Me.Controls.Add(Me.DgvMenuMaster)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "FrmMenuMasterEdit"
        Me.Text = "MenuMasterEdit"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DgvMenuMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpAdd.ResumeLayout(False)
        Me.GrpAdd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnPreview As System.Windows.Forms.Button
    Friend WithEvents BtnClose2 As System.Windows.Forms.Button
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents DgvMenuMaster As System.Windows.Forms.DataGridView
    Friend WithEvents GrpAdd As System.Windows.Forms.GroupBox
    Friend WithEvents txtParentID As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMenuOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFromOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents CbFormState As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFormName As System.Windows.Forms.TextBox
    Friend WithEvents BtnAddSave As System.Windows.Forms.Button
    Friend WithEvents txtLedCode As System.Windows.Forms.TextBox
    Friend WithEvents txtMenuName As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Btnadd As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents MenuID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenuName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FormName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FormState As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ParentID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenuNameOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FormNameOrder As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
