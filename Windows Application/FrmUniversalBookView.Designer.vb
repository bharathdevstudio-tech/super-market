<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUniversalBookView
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
        Me.PnlRoomHost = New System.Windows.Forms.Panel()
        Me.DgvAdvBookDtl = New System.Windows.Forms.DataGridView()
        Me.SNo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArrDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusPlace = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mobile1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RoomNoA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AdvAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Purpose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReservID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvAdvBookDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PnlRoomHost
        '
        Me.PnlRoomHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlRoomHost.Location = New System.Drawing.Point(0, 0)
        Me.PnlRoomHost.Name = "PnlRoomHost"
        Me.PnlRoomHost.Size = New System.Drawing.Size(1004, 291)
        Me.PnlRoomHost.TabIndex = 0
        '
        'DgvAdvBookDtl
        '
        Me.DgvAdvBookDtl.AllowUserToAddRows = False
        Me.DgvAdvBookDtl.AllowUserToResizeRows = False
        Me.DgvAdvBookDtl.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvAdvBookDtl.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvAdvBookDtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvAdvBookDtl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo1, Me.ArrDate, Me.CusID, Me.CusName, Me.CusPlace, Me.Mobile1, Me.RoomNoA, Me.AdvAmt, Me.Purpose, Me.ReservID})
        Me.DgvAdvBookDtl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DgvAdvBookDtl.EnableHeadersVisualStyles = False
        Me.DgvAdvBookDtl.Location = New System.Drawing.Point(0, 291)
        Me.DgvAdvBookDtl.Name = "DgvAdvBookDtl"
        Me.DgvAdvBookDtl.RowHeadersVisible = False
        Me.DgvAdvBookDtl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvAdvBookDtl.Size = New System.Drawing.Size(1004, 236)
        Me.DgvAdvBookDtl.TabIndex = 692
        '
        'SNo1
        '
        Me.SNo1.HeaderText = "SNo"
        Me.SNo1.Name = "SNo1"
        Me.SNo1.ReadOnly = True
        Me.SNo1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo1.Width = 50
        '
        'ArrDate
        '
        Me.ArrDate.HeaderText = "Arrival Date"
        Me.ArrDate.Name = "ArrDate"
        Me.ArrDate.ReadOnly = True
        Me.ArrDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ArrDate.Width = 200
        '
        'CusID
        '
        Me.CusID.HeaderText = "ID"
        Me.CusID.Name = "CusID"
        Me.CusID.ReadOnly = True
        Me.CusID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CusName
        '
        Me.CusName.HeaderText = "Name"
        Me.CusName.Name = "CusName"
        Me.CusName.ReadOnly = True
        Me.CusName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CusName.Width = 300
        '
        'CusPlace
        '
        Me.CusPlace.HeaderText = "Place"
        Me.CusPlace.Name = "CusPlace"
        Me.CusPlace.ReadOnly = True
        Me.CusPlace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CusPlace.Width = 170
        '
        'Mobile1
        '
        Me.Mobile1.HeaderText = "Mobile"
        Me.Mobile1.Name = "Mobile1"
        Me.Mobile1.ReadOnly = True
        Me.Mobile1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Mobile1.Width = 150
        '
        'RoomNoA
        '
        Me.RoomNoA.HeaderText = "Room"
        Me.RoomNoA.Name = "RoomNoA"
        Me.RoomNoA.ReadOnly = True
        Me.RoomNoA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'AdvAmt
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        Me.AdvAmt.DefaultCellStyle = DataGridViewCellStyle2
        Me.AdvAmt.HeaderText = "Advance"
        Me.AdvAmt.Name = "AdvAmt"
        Me.AdvAmt.ReadOnly = True
        Me.AdvAmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Purpose
        '
        Me.Purpose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Purpose.HeaderText = "Purpose"
        Me.Purpose.Name = "Purpose"
        Me.Purpose.ReadOnly = True
        Me.Purpose.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ReservID
        '
        Me.ReservID.HeaderText = "ReservID"
        Me.ReservID.Name = "ReservID"
        Me.ReservID.ReadOnly = True
        Me.ReservID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ReservID.Visible = False
        '
        'FrmUniversalBookView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 527)
        Me.Controls.Add(Me.PnlRoomHost)
        Me.Controls.Add(Me.DgvAdvBookDtl)
        Me.Name = "FrmUniversalBookView"
        Me.Text = "FrmUniversalBookView"
        CType(Me.DgvAdvBookDtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PnlRoomHost As System.Windows.Forms.Panel
    Friend WithEvents DgvAdvBookDtl As System.Windows.Forms.DataGridView
    Friend WithEvents SNo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArrDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusPlace As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mobile1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RoomNoA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AdvAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Purpose As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReservID As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
