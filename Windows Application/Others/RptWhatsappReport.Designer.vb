<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptWhatsappReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptWhatsappReport))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnPreview = New System.Windows.Forms.Button()
        Me.txtFromDate = New System.Windows.Forms.TextBox()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.btnTo4 = New System.Windows.Forms.Button()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.btnFrom4 = New System.Windows.Forms.Button()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.DgvOutstanding = New System.Windows.Forms.DataGridView()
        Me.SNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SendDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MobileNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SendStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MsgType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FileType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WhMessage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FilePath = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvOutstanding, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Khaki
        Me.Panel1.Controls.Add(Me.BtnPreview)
        Me.Panel1.Controls.Add(Me.txtFromDate)
        Me.Panel1.Controls.Add(Me.BtnClose)
        Me.Panel1.Controls.Add(Me.btnTo4)
        Me.Panel1.Controls.Add(Me.txtToDate)
        Me.Panel1.Controls.Add(Me.btnFrom4)
        Me.Panel1.Controls.Add(Me.Label48)
        Me.Panel1.Controls.Add(Me.Label47)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1205, 49)
        Me.Panel1.TabIndex = 1
        '
        'BtnPreview
        '
        Me.BtnPreview.BackColor = System.Drawing.Color.Khaki
        Me.BtnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPreview.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPreview.Location = New System.Drawing.Point(382, 9)
        Me.BtnPreview.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Size = New System.Drawing.Size(76, 30)
        Me.BtnPreview.TabIndex = 874
        Me.BtnPreview.Text = "Preview"
        Me.BtnPreview.UseVisualStyleBackColor = False
        '
        'txtFromDate
        '
        Me.txtFromDate.BackColor = System.Drawing.Color.White
        Me.txtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Location = New System.Drawing.Point(52, 11)
        Me.txtFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.ReadOnly = True
        Me.txtFromDate.Size = New System.Drawing.Size(102, 26)
        Me.txtFromDate.TabIndex = 869
        Me.txtFromDate.TabStop = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.Khaki
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Black
        Me.BtnClose.Location = New System.Drawing.Point(490, 9)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(76, 30)
        Me.BtnClose.TabIndex = 852
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'btnTo4
        '
        Me.btnTo4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnTo4.BackgroundImage = CType(resources.GetObject("btnTo4.BackgroundImage"), System.Drawing.Image)
        Me.btnTo4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTo4.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTo4.ImageKey = "(none)"
        Me.btnTo4.Location = New System.Drawing.Point(324, 11)
        Me.btnTo4.Name = "btnTo4"
        Me.btnTo4.Size = New System.Drawing.Size(25, 26)
        Me.btnTo4.TabIndex = 872
        Me.btnTo4.UseVisualStyleBackColor = False
        '
        'txtToDate
        '
        Me.txtToDate.BackColor = System.Drawing.Color.White
        Me.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Location = New System.Drawing.Point(222, 11)
        Me.txtToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.ReadOnly = True
        Me.txtToDate.Size = New System.Drawing.Size(102, 26)
        Me.txtToDate.TabIndex = 871
        Me.txtToDate.TabStop = False
        '
        'btnFrom4
        '
        Me.btnFrom4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnFrom4.BackgroundImage = CType(resources.GetObject("btnFrom4.BackgroundImage"), System.Drawing.Image)
        Me.btnFrom4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFrom4.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrom4.ImageKey = "(none)"
        Me.btnFrom4.Location = New System.Drawing.Point(154, 11)
        Me.btnFrom4.Name = "btnFrom4"
        Me.btnFrom4.Size = New System.Drawing.Size(25, 26)
        Me.btnFrom4.TabIndex = 870
        Me.btnFrom4.UseVisualStyleBackColor = False
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.Color.Khaki
        Me.Label48.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(186, 15)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(26, 19)
        Me.Label48.TabIndex = 873
        Me.Label48.Text = "To"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.Color.Khaki
        Me.Label47.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(7, 14)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(42, 19)
        Me.Label47.TabIndex = 868
        Me.Label47.Text = "Date"
        '
        'DgvOutstanding
        '
        Me.DgvOutstanding.AllowUserToAddRows = False
        Me.DgvOutstanding.AllowUserToResizeRows = False
        Me.DgvOutstanding.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvOutstanding.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvOutstanding.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvOutstanding.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo, Me.SendDate, Me.MobileNo, Me.SendStatus, Me.MsgType, Me.FileType, Me.WhMessage, Me.FilePath})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 11.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvOutstanding.DefaultCellStyle = DataGridViewCellStyle2
        Me.DgvOutstanding.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvOutstanding.EnableHeadersVisualStyles = False
        Me.DgvOutstanding.Location = New System.Drawing.Point(0, 49)
        Me.DgvOutstanding.Name = "DgvOutstanding"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 11.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvOutstanding.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DgvOutstanding.RowHeadersVisible = False
        Me.DgvOutstanding.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvOutstanding.Size = New System.Drawing.Size(1205, 542)
        Me.DgvOutstanding.TabIndex = 703
        '
        'SNo
        '
        Me.SNo.HeaderText = "SNo"
        Me.SNo.Name = "SNo"
        Me.SNo.ReadOnly = True
        Me.SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo.Width = 50
        '
        'SendDate
        '
        Me.SendDate.HeaderText = "Send Date"
        Me.SendDate.Name = "SendDate"
        Me.SendDate.ReadOnly = True
        Me.SendDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SendDate.Width = 150
        '
        'MobileNo
        '
        Me.MobileNo.HeaderText = "Mobile No"
        Me.MobileNo.Name = "MobileNo"
        Me.MobileNo.ReadOnly = True
        Me.MobileNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MobileNo.Width = 150
        '
        'SendStatus
        '
        Me.SendStatus.HeaderText = "Status"
        Me.SendStatus.Name = "SendStatus"
        Me.SendStatus.ReadOnly = True
        Me.SendStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MsgType
        '
        Me.MsgType.HeaderText = "Msg Type"
        Me.MsgType.Name = "MsgType"
        Me.MsgType.ReadOnly = True
        Me.MsgType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MsgType.Width = 150
        '
        'FileType
        '
        Me.FileType.HeaderText = "FileType"
        Me.FileType.Name = "FileType"
        Me.FileType.ReadOnly = True
        Me.FileType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'WhMessage
        '
        Me.WhMessage.HeaderText = "Message"
        Me.WhMessage.Name = "WhMessage"
        Me.WhMessage.ReadOnly = True
        Me.WhMessage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.WhMessage.Width = 350
        '
        'FilePath
        '
        Me.FilePath.HeaderText = "FilePath"
        Me.FilePath.Name = "FilePath"
        Me.FilePath.ReadOnly = True
        Me.FilePath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.FilePath.Width = 400
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(172, 80)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 875
        '
        'RptWhatsappReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1205, 591)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Controls.Add(Me.DgvOutstanding)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "RptWhatsappReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Whatsapp Report"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DgvOutstanding, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtFromDate As System.Windows.Forms.TextBox
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents btnTo4 As System.Windows.Forms.Button
    Friend WithEvents txtToDate As System.Windows.Forms.TextBox
    Friend WithEvents btnFrom4 As System.Windows.Forms.Button
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents DgvOutstanding As System.Windows.Forms.DataGridView
    Friend WithEvents BtnPreview As System.Windows.Forms.Button
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SendDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MobileNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SendStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MsgType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FileType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WhMessage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FilePath As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
