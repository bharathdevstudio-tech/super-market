<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDBClear
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.DgvTableList = New System.Windows.Forms.DataGridView()
        Me.SNo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TableName1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RowCount1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Delete1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CbAll = New System.Windows.Forms.CheckBox()
        Me.CBSelectM = New System.Windows.Forms.CheckBox()
        CType(Me.DgvTableList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnDelete
        '
        Me.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDelete.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.Color.Crimson
        Me.BtnDelete.Location = New System.Drawing.Point(320, 8)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(97, 34)
        Me.BtnDelete.TabIndex = 2
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Crimson
        Me.BtnClose.Location = New System.Drawing.Point(457, 8)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(97, 34)
        Me.BtnClose.TabIndex = 3
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'DgvTableList
        '
        Me.DgvTableList.AllowUserToAddRows = False
        Me.DgvTableList.AllowUserToDeleteRows = False
        Me.DgvTableList.AllowUserToResizeRows = False
        Me.DgvTableList.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Navy
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tw Cen MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvTableList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvTableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvTableList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo1, Me.Tick1, Me.TableName1, Me.RowCount1, Me.Delete1})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tw Cen MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvTableList.DefaultCellStyle = DataGridViewCellStyle3
        Me.DgvTableList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvTableList.EnableHeadersVisualStyles = False
        Me.DgvTableList.Location = New System.Drawing.Point(0, 50)
        Me.DgvTableList.Margin = New System.Windows.Forms.Padding(4)
        Me.DgvTableList.Name = "DgvTableList"
        Me.DgvTableList.RowHeadersVisible = False
        Me.DgvTableList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvTableList.Size = New System.Drawing.Size(586, 459)
        Me.DgvTableList.TabIndex = 863
        '
        'SNo1
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        Me.SNo1.DefaultCellStyle = DataGridViewCellStyle2
        Me.SNo1.HeaderText = "SNo"
        Me.SNo1.Name = "SNo1"
        Me.SNo1.ReadOnly = True
        Me.SNo1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo1.Width = 35
        '
        'Tick1
        '
        Me.Tick1.FalseValue = "NO"
        Me.Tick1.HeaderText = "Tick"
        Me.Tick1.Name = "Tick1"
        Me.Tick1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tick1.TrueValue = "YES"
        Me.Tick1.Width = 40
        '
        'TableName1
        '
        Me.TableName1.HeaderText = "Table"
        Me.TableName1.Name = "TableName1"
        Me.TableName1.ReadOnly = True
        Me.TableName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TableName1.Width = 300
        '
        'RowCount1
        '
        Me.RowCount1.HeaderText = "Count"
        Me.RowCount1.Name = "RowCount1"
        Me.RowCount1.ReadOnly = True
        Me.RowCount1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Delete1
        '
        Me.Delete1.HeaderText = "Delete"
        Me.Delete1.Name = "Delete1"
        Me.Delete1.ReadOnly = True
        Me.Delete1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Delete1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CbAll)
        Me.Panel1.Controls.Add(Me.CBSelectM)
        Me.Panel1.Controls.Add(Me.BtnClose)
        Me.Panel1.Controls.Add(Me.BtnDelete)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(586, 50)
        Me.Panel1.TabIndex = 864
        '
        'CbAll
        '
        Me.CbAll.AutoSize = True
        Me.CbAll.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbAll.Location = New System.Drawing.Point(12, 12)
        Me.CbAll.Name = "CbAll"
        Me.CbAll.Size = New System.Drawing.Size(89, 21)
        Me.CbAll.TabIndex = 765
        Me.CbAll.Text = "All Table"
        Me.CbAll.UseVisualStyleBackColor = True
        '
        'CBSelectM
        '
        Me.CBSelectM.AutoSize = True
        Me.CBSelectM.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBSelectM.Location = New System.Drawing.Point(183, 12)
        Me.CBSelectM.Name = "CBSelectM"
        Me.CBSelectM.Size = New System.Drawing.Size(91, 21)
        Me.CBSelectM.TabIndex = 764
        Me.CBSelectM.Text = "Select All"
        Me.CBSelectM.UseVisualStyleBackColor = True
        '
        'FrmDBClear
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Wheat
        Me.ClientSize = New System.Drawing.Size(586, 509)
        Me.Controls.Add(Me.DgvTableList)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmDBClear"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DB Clear"
        CType(Me.DgvTableList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents DgvTableList As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CBSelectM As System.Windows.Forms.CheckBox
    Friend WithEvents CbAll As System.Windows.Forms.CheckBox
    Friend WithEvents SNo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents TableName1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RowCount1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Delete1 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
