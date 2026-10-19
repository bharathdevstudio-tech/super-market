<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHostingSync
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmHostingSync))
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.cboMode = New System.Windows.Forms.ComboBox()
        Me.lblTable = New System.Windows.Forms.Label()
        Me.cboTables = New System.Windows.Forms.ComboBox()
        Me.btnRefreshTables = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnSync = New System.Windows.Forms.Button()
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtFromDate = New System.Windows.Forms.TextBox()
        Me.btnTo4 = New System.Windows.Forms.Button()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.btnFrom4 = New System.Windows.Forms.Button()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(176, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Hosting Data Sync"
        '
        'lblMode
        '
        Me.lblMode.AutoSize = True
        Me.lblMode.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblMode.Location = New System.Drawing.Point(12, 53)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(42, 15)
        Me.lblMode.TabIndex = 1
        Me.lblMode.Text = "Mode:"
        '
        'cboMode
        '
        Me.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMode.FormattingEnabled = True
        Me.cboMode.Location = New System.Drawing.Point(60, 50)
        Me.cboMode.Name = "cboMode"
        Me.cboMode.Size = New System.Drawing.Size(190, 21)
        Me.cboMode.TabIndex = 0
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTable.Location = New System.Drawing.Point(267, 53)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(39, 15)
        Me.lblTable.TabIndex = 3
        Me.lblTable.Text = "Table:"
        '
        'cboTables
        '
        Me.cboTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTables.FormattingEnabled = True
        Me.cboTables.Location = New System.Drawing.Point(313, 50)
        Me.cboTables.Name = "cboTables"
        Me.cboTables.Size = New System.Drawing.Size(240, 21)
        Me.cboTables.TabIndex = 1
        '
        'btnRefreshTables
        '
        Me.btnRefreshTables.Location = New System.Drawing.Point(562, 48)
        Me.btnRefreshTables.Name = "btnRefreshTables"
        Me.btnRefreshTables.Size = New System.Drawing.Size(105, 25)
        Me.btnRefreshTables.TabIndex = 2
        Me.btnRefreshTables.Text = "Refresh Tables"
        Me.btnRefreshTables.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(674, 48)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(95, 25)
        Me.btnLoad.TabIndex = 3
        Me.btnLoad.Text = "Load Data"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnSync
        '
        Me.btnSync.Location = New System.Drawing.Point(776, 48)
        Me.btnSync.Name = "btnSync"
        Me.btnSync.Size = New System.Drawing.Size(145, 25)
        Me.btnSync.TabIndex = 4
        Me.btnSync.Text = "Sync"
        Me.btnSync.UseVisualStyleBackColor = True
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToDeleteRows = False
        Me.dgvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvData.BackgroundColor = System.Drawing.Color.White
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Location = New System.Drawing.Point(12, 187)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.ReadOnly = True
        Me.dgvData.RowHeadersVisible = False
        Me.dgvData.Size = New System.Drawing.Size(909, 329)
        Me.dgvData.TabIndex = 5
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblStatus.ForeColor = System.Drawing.Color.DimGray
        Me.lblStatus.Location = New System.Drawing.Point(12, 83)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 15)
        Me.lblStatus.TabIndex = 9
        '
        'txtFromDate
        '
        Me.txtFromDate.BackColor = System.Drawing.Color.White
        Me.txtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Location = New System.Drawing.Point(336, 83)
        Me.txtFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.ReadOnly = True
        Me.txtFromDate.Size = New System.Drawing.Size(102, 26)
        Me.txtFromDate.TabIndex = 875
        Me.txtFromDate.TabStop = False
        '
        'btnTo4
        '
        Me.btnTo4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnTo4.BackgroundImage = CType(resources.GetObject("btnTo4.BackgroundImage"), System.Drawing.Image)
        Me.btnTo4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTo4.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTo4.ImageKey = "(none)"
        Me.btnTo4.Location = New System.Drawing.Point(608, 83)
        Me.btnTo4.Name = "btnTo4"
        Me.btnTo4.Size = New System.Drawing.Size(25, 26)
        Me.btnTo4.TabIndex = 878
        Me.btnTo4.UseVisualStyleBackColor = False
        '
        'txtToDate
        '
        Me.txtToDate.BackColor = System.Drawing.Color.White
        Me.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Location = New System.Drawing.Point(506, 83)
        Me.txtToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.ReadOnly = True
        Me.txtToDate.Size = New System.Drawing.Size(102, 26)
        Me.txtToDate.TabIndex = 877
        Me.txtToDate.TabStop = False
        '
        'btnFrom4
        '
        Me.btnFrom4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnFrom4.BackgroundImage = CType(resources.GetObject("btnFrom4.BackgroundImage"), System.Drawing.Image)
        Me.btnFrom4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFrom4.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrom4.ImageKey = "(none)"
        Me.btnFrom4.Location = New System.Drawing.Point(438, 83)
        Me.btnFrom4.Name = "btnFrom4"
        Me.btnFrom4.Size = New System.Drawing.Size(25, 26)
        Me.btnFrom4.TabIndex = 876
        Me.btnFrom4.UseVisualStyleBackColor = False
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.Color.Khaki
        Me.Label48.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(470, 87)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(26, 19)
        Me.Label48.TabIndex = 879
        Me.Label48.Text = "To"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.Color.Khaki
        Me.Label47.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(291, 86)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(42, 19)
        Me.Label47.TabIndex = 874
        Me.Label47.Text = "Date"
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(452, 144)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 880
        '
        'FrmHostingSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 528)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Controls.Add(Me.txtFromDate)
        Me.Controls.Add(Me.btnTo4)
        Me.Controls.Add(Me.txtToDate)
        Me.Controls.Add(Me.btnFrom4)
        Me.Controls.Add(Me.Label48)
        Me.Controls.Add(Me.Label47)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.dgvData)
        Me.Controls.Add(Me.btnSync)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnRefreshTables)
        Me.Controls.Add(Me.cboTables)
        Me.Controls.Add(Me.lblTable)
        Me.Controls.Add(Me.cboMode)
        Me.Controls.Add(Me.lblMode)
        Me.Controls.Add(Me.lblTitle)
        Me.MinimumSize = New System.Drawing.Size(800, 450)
        Me.Name = "FrmHostingSync"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hosting Data Sync"
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblMode As System.Windows.Forms.Label
    Friend WithEvents cboMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblTable As System.Windows.Forms.Label
    Friend WithEvents cboTables As System.Windows.Forms.ComboBox
    Friend WithEvents btnRefreshTables As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnSync As System.Windows.Forms.Button
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtFromDate As System.Windows.Forms.TextBox
    Friend WithEvents btnTo4 As System.Windows.Forms.Button
    Friend WithEvents txtToDate As System.Windows.Forms.TextBox
    Friend WithEvents btnFrom4 As System.Windows.Forms.Button
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
End Class
