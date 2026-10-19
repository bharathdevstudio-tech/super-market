<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubFrmColOrderCommon
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnPreview = New System.Windows.Forms.Button()
        Me.CbType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnUp = New System.Windows.Forms.Button()
        Me.BtnDown = New System.Windows.Forms.Button()
        Me.DGVOrder = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        CType(Me.DGVOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Wheat
        Me.Panel1.Controls.Add(Me.BtnPreview)
        Me.Panel1.Controls.Add(Me.CbType)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.BtnSave)
        Me.Panel1.Controls.Add(Me.BtnClose)
        Me.Panel1.Controls.Add(Me.BtnUp)
        Me.Panel1.Controls.Add(Me.BtnDown)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(908, 53)
        Me.Panel1.TabIndex = 11
        '
        'BtnPreview
        '
        Me.BtnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPreview.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPreview.ForeColor = System.Drawing.Color.Crimson
        Me.BtnPreview.Location = New System.Drawing.Point(309, 7)
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Size = New System.Drawing.Size(102, 34)
        Me.BtnPreview.TabIndex = 10
        Me.BtnPreview.Text = "Preview"
        Me.BtnPreview.UseVisualStyleBackColor = True
        '
        'CbType
        '
        Me.CbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbType.Font = New System.Drawing.Font("Microsoft Tai Le", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbType.FormattingEnabled = True
        Me.CbType.Location = New System.Drawing.Point(62, 10)
        Me.CbType.Name = "CbType"
        Me.CbType.Size = New System.Drawing.Size(211, 24)
        Me.CbType.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Type"
        '
        'BtnSave
        '
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.Crimson
        Me.BtnSave.Location = New System.Drawing.Point(660, 7)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(90, 34)
        Me.BtnSave.TabIndex = 6
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Crimson
        Me.BtnClose.Location = New System.Drawing.Point(773, 7)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(90, 34)
        Me.BtnClose.TabIndex = 7
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnUp
        '
        Me.BtnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUp.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUp.ForeColor = System.Drawing.Color.Crimson
        Me.BtnUp.Location = New System.Drawing.Point(434, 7)
        Me.BtnUp.Name = "BtnUp"
        Me.BtnUp.Size = New System.Drawing.Size(90, 34)
        Me.BtnUp.TabIndex = 4
        Me.BtnUp.Text = "Up"
        Me.BtnUp.UseVisualStyleBackColor = True
        '
        'BtnDown
        '
        Me.BtnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDown.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDown.ForeColor = System.Drawing.Color.Crimson
        Me.BtnDown.Location = New System.Drawing.Point(547, 7)
        Me.BtnDown.Name = "BtnDown"
        Me.BtnDown.Size = New System.Drawing.Size(90, 34)
        Me.BtnDown.TabIndex = 5
        Me.BtnDown.Text = "Down"
        Me.BtnDown.UseVisualStyleBackColor = True
        '
        'DGVOrder
        '
        Me.DGVOrder.AllowUserToAddRows = False
        Me.DGVOrder.AllowUserToDeleteRows = False
        Me.DGVOrder.AllowUserToOrderColumns = True
        Me.DGVOrder.AllowUserToResizeColumns = False
        Me.DGVOrder.AllowUserToResizeRows = False
        Me.DGVOrder.BackgroundColor = System.Drawing.Color.White
        Me.DGVOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVOrder.Location = New System.Drawing.Point(0, 53)
        Me.DGVOrder.Name = "DGVOrder"
        Me.DGVOrder.RowHeadersVisible = False
        Me.DGVOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVOrder.Size = New System.Drawing.Size(908, 489)
        Me.DGVOrder.TabIndex = 12
        '
        'SubFrmColOrderCommon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(908, 542)
        Me.Controls.Add(Me.DGVOrder)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "SubFrmColOrderCommon"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Column Order"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DGVOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BtnPreview As System.Windows.Forms.Button
    Friend WithEvents CbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnUp As System.Windows.Forms.Button
    Friend WithEvents BtnDown As System.Windows.Forms.Button
    Friend WithEvents DGVOrder As System.Windows.Forms.DataGridView
End Class
