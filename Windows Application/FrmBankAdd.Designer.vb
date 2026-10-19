<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankAdd
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.CbBalType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtOpBal = New System.Windows.Forms.TextBox()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.txtLedCode = New System.Windows.Forms.TextBox()
        Me.txtLedgerName = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DgvLegderList = New System.Windows.Forms.DataGridView()
        Me.SNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.LedName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OpBalL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OpBalType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.LedCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DgvLegderList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Tan
        Me.GroupBox1.Controls.Add(Me.BtnClose)
        Me.GroupBox1.Controls.Add(Me.CbBalType)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtOpBal)
        Me.GroupBox1.Controls.Add(Me.BtnEdit)
        Me.GroupBox1.Controls.Add(Me.BtnSave)
        Me.GroupBox1.Controls.Add(Me.BtnDelete)
        Me.GroupBox1.Controls.Add(Me.txtLedCode)
        Me.GroupBox1.Controls.Add(Me.txtLedgerName)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(605, 109)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.Tan
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnClose.Location = New System.Drawing.Point(359, 72)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(69, 29)
        Me.BtnClose.TabIndex = 834
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'CbBalType
        '
        Me.CbBalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbBalType.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbBalType.FormattingEnabled = True
        Me.CbBalType.Items.AddRange(New Object() {"Debit", "Credit"})
        Me.CbBalType.Location = New System.Drawing.Point(342, 41)
        Me.CbBalType.Name = "CbBalType"
        Me.CbBalType.Size = New System.Drawing.Size(102, 23)
        Me.CbBalType.TabIndex = 833
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(10, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 17)
        Me.Label3.TabIndex = 832
        Me.Label3.Text = "OP.Balance"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(250, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 17)
        Me.Label2.TabIndex = 831
        Me.Label2.Text = "OP.Bal Type"
        '
        'txtOpBal
        '
        Me.txtOpBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOpBal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOpBal.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpBal.Location = New System.Drawing.Point(107, 39)
        Me.txtOpBal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtOpBal.Name = "txtOpBal"
        Me.txtOpBal.Size = New System.Drawing.Size(136, 25)
        Me.txtOpBal.TabIndex = 830
        '
        'BtnEdit
        '
        Me.BtnEdit.BackColor = System.Drawing.Color.Tan
        Me.BtnEdit.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnEdit.Location = New System.Drawing.Point(191, 72)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(69, 29)
        Me.BtnEdit.TabIndex = 739
        Me.BtnEdit.Text = "Edit"
        Me.BtnEdit.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.Tan
        Me.BtnSave.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(107, 72)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(69, 29)
        Me.BtnSave.TabIndex = 5
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.BackColor = System.Drawing.Color.Tan
        Me.BtnDelete.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnDelete.Location = New System.Drawing.Point(275, 72)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(69, 29)
        Me.BtnDelete.TabIndex = 65
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = False
        '
        'txtLedCode
        '
        Me.txtLedCode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtLedCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLedCode.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLedCode.Location = New System.Drawing.Point(451, 16)
        Me.txtLedCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLedCode.Name = "txtLedCode"
        Me.txtLedCode.ReadOnly = True
        Me.txtLedCode.Size = New System.Drawing.Size(52, 25)
        Me.txtLedCode.TabIndex = 1
        Me.txtLedCode.TabStop = False
        Me.txtLedCode.Visible = False
        '
        'txtLedgerName
        '
        Me.txtLedgerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLedgerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLedgerName.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLedgerName.Location = New System.Drawing.Point(107, 14)
        Me.txtLedgerName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLedgerName.Name = "txtLedgerName"
        Me.txtLedgerName.Size = New System.Drawing.Size(338, 25)
        Me.txtLedgerName.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(10, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(86, 17)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Bank Name"
        '
        'DgvLegderList
        '
        Me.DgvLegderList.AllowUserToAddRows = False
        Me.DgvLegderList.AllowUserToDeleteRows = False
        Me.DgvLegderList.AllowUserToResizeRows = False
        Me.DgvLegderList.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Chocolate
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvLegderList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DgvLegderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvLegderList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo, Me.Tick, Me.LedName, Me.OpBalL, Me.OpBalType, Me.LedCode})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvLegderList.DefaultCellStyle = DataGridViewCellStyle12
        Me.DgvLegderList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvLegderList.EnableHeadersVisualStyles = False
        Me.DgvLegderList.Location = New System.Drawing.Point(0, 109)
        Me.DgvLegderList.Name = "DgvLegderList"
        Me.DgvLegderList.RowHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.DgvLegderList.RowHeadersVisible = False
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvLegderList.RowsDefaultCellStyle = DataGridViewCellStyle14
        Me.DgvLegderList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvLegderList.Size = New System.Drawing.Size(605, 376)
        Me.DgvLegderList.TabIndex = 17
        '
        'SNo
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.Tan
        Me.SNo.DefaultCellStyle = DataGridViewCellStyle9
        Me.SNo.HeaderText = "SNo"
        Me.SNo.Name = "SNo"
        Me.SNo.ReadOnly = True
        Me.SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo.Width = 50
        '
        'Tick
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.Tan
        DataGridViewCellStyle10.NullValue = False
        Me.Tick.DefaultCellStyle = DataGridViewCellStyle10
        Me.Tick.FalseValue = "NO"
        Me.Tick.HeaderText = "T"
        Me.Tick.Name = "Tick"
        Me.Tick.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tick.TrueValue = "YES"
        Me.Tick.Width = 45
        '
        'LedName
        '
        Me.LedName.HeaderText = "Bank Name"
        Me.LedName.Name = "LedName"
        Me.LedName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.LedName.Width = 300
        '
        'OpBalL
        '
        Me.OpBalL.HeaderText = "OpBal"
        Me.OpBalL.Name = "OpBalL"
        Me.OpBalL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'OpBalType
        '
        Me.OpBalType.HeaderText = "Type"
        Me.OpBalType.Items.AddRange(New Object() {"Debit", "Credit"})
        Me.OpBalType.Name = "OpBalType"
        Me.OpBalType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'LedCode
        '
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.Tan
        Me.LedCode.DefaultCellStyle = DataGridViewCellStyle11
        Me.LedCode.HeaderText = "Ledger Code"
        Me.LedCode.Name = "LedCode"
        Me.LedCode.ReadOnly = True
        Me.LedCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.LedCode.Visible = False
        '
        'FrmBankAddNav
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 485)
        Me.Controls.Add(Me.DgvLegderList)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBankAddNav"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bank"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DgvLegderList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DgvLegderList As System.Windows.Forms.DataGridView
    Friend WithEvents txtLedCode As System.Windows.Forms.TextBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents txtLedgerName As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnEdit As System.Windows.Forms.Button
    Friend WithEvents txtOpBal As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CbBalType As System.Windows.Forms.ComboBox
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents LedName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OpBalL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OpBalType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents LedCode As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
