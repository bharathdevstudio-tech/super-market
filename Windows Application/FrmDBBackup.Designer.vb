Partial Class FrmDBBackup
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtFolderPath = New System.Windows.Forms.TextBox()
        Me.txtFilePrefix = New System.Windows.Forms.TextBox()
        Me.lblPreview = New System.Windows.Forms.Label()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.lblPct = New System.Windows.Forms.Label()
        Me.btnVistawinBackup = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.tmrProgress = New System.Windows.Forms.Timer(Me.components)
        Me.BtnBackup = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnGDriveBackup = New System.Windows.Forms.Button()
        Me.chkCompress = New System.Windows.Forms.CheckBox()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtFolderPath
        '
        Me.txtFolderPath.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtFolderPath.Location = New System.Drawing.Point(134, 17)
        Me.txtFolderPath.Name = "txtFolderPath"
        Me.txtFolderPath.Size = New System.Drawing.Size(654, 23)
        Me.txtFolderPath.TabIndex = 0
        '
        'txtFilePrefix
        '
        Me.txtFilePrefix.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePrefix.Location = New System.Drawing.Point(134, 49)
        Me.txtFilePrefix.Name = "txtFilePrefix"
        Me.txtFilePrefix.Size = New System.Drawing.Size(654, 23)
        Me.txtFilePrefix.TabIndex = 1
        '
        'lblPreview
        '
        Me.lblPreview.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreview.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.lblPreview.Location = New System.Drawing.Point(12, 252)
        Me.lblPreview.Name = "lblPreview"
        Me.lblPreview.Size = New System.Drawing.Size(460, 18)
        Me.lblPreview.TabIndex = 3
        Me.lblPreview.Text = "Status"
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(134, 89)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(654, 22)
        Me.progressBar.TabIndex = 5
        Me.progressBar.Visible = False
        '
        'lblPct
        '
        Me.lblPct.BackColor = System.Drawing.Color.Black
        Me.lblPct.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPct.ForeColor = System.Drawing.Color.White
        Me.lblPct.Location = New System.Drawing.Point(12, 173)
        Me.lblPct.Name = "lblPct"
        Me.lblPct.Size = New System.Drawing.Size(56, 22)
        Me.lblPct.TabIndex = 6
        Me.lblPct.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPct.Visible = False
        '
        'btnVistawinBackup
        '
        Me.btnVistawinBackup.BackColor = System.Drawing.Color.SteelBlue
        Me.btnVistawinBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVistawinBackup.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnVistawinBackup.ForeColor = System.Drawing.Color.White
        Me.btnVistawinBackup.Location = New System.Drawing.Point(563, 152)
        Me.btnVistawinBackup.Name = "btnVistawinBackup"
        Me.btnVistawinBackup.Size = New System.Drawing.Size(225, 31)
        Me.btnVistawinBackup.TabIndex = 2
        Me.btnVistawinBackup.Text = "Create Backup"
        Me.btnVistawinBackup.UseVisualStyleBackColor = False
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.lblStatus.Location = New System.Drawing.Point(12, 216)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(460, 20)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Text = "Ready."
        '
        'tmrProgress
        '
        Me.tmrProgress.Interval = 80
        '
        'BtnBackup
        '
        Me.BtnBackup.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBackup.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BtnBackup.ForeColor = System.Drawing.Color.White
        Me.BtnBackup.Location = New System.Drawing.Point(134, 152)
        Me.BtnBackup.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnBackup.Name = "BtnBackup"
        Me.BtnBackup.Size = New System.Drawing.Size(194, 31)
        Me.BtnBackup.TabIndex = 699
        Me.BtnBackup.Text = "Backup"
        Me.BtnBackup.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(8, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(97, 18)
        Me.Label20.TabIndex = 703
        Me.Label20.Text = "Backup Path"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 18)
        Me.Label1.TabIndex = 704
        Me.Label1.Text = "File Name"
        '
        'BtnGDriveBackup
        '
        Me.BtnGDriveBackup.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnGDriveBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnGDriveBackup.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BtnGDriveBackup.ForeColor = System.Drawing.Color.White
        Me.BtnGDriveBackup.Location = New System.Drawing.Point(345, 152)
        Me.BtnGDriveBackup.Name = "BtnGDriveBackup"
        Me.BtnGDriveBackup.Size = New System.Drawing.Size(201, 31)
        Me.BtnGDriveBackup.TabIndex = 705
        Me.BtnGDriveBackup.Text = "Send to Google Drive"
        Me.BtnGDriveBackup.UseVisualStyleBackColor = False
        '
        'chkCompress
        '
        Me.chkCompress.AutoSize = True
        Me.chkCompress.Checked = True
        Me.chkCompress.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompress.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompress.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.chkCompress.Location = New System.Drawing.Point(376, 124)
        Me.chkCompress.Name = "chkCompress"
        Me.chkCompress.Size = New System.Drawing.Size(126, 20)
        Me.chkCompress.TabIndex = 706
        Me.chkCompress.Text = "Compress to .zip"
        Me.chkCompress.UseVisualStyleBackColor = True
        '
        'txtApiKey
        '
        Me.txtApiKey.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtApiKey.Location = New System.Drawing.Point(11, 323)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.ReadOnly = True
        Me.txtApiKey.Size = New System.Drawing.Size(786, 23)
        Me.txtApiKey.TabIndex = 707
        '
        'FrmDBBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(809, 389)
        Me.Controls.Add(Me.txtApiKey)
        Me.Controls.Add(Me.chkCompress)
        Me.Controls.Add(Me.BtnGDriveBackup)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.BtnBackup)
        Me.Controls.Add(Me.txtFolderPath)
        Me.Controls.Add(Me.txtFilePrefix)
        Me.Controls.Add(Me.lblPreview)
        Me.Controls.Add(Me.progressBar)
        Me.Controls.Add(Me.lblPct)
        Me.Controls.Add(Me.btnVistawinBackup)
        Me.Controls.Add(Me.lblStatus)
        Me.ForeColor = System.Drawing.Color.Black
        Me.MaximizeBox = False
        Me.Name = "FrmDBBackup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents txtFilePrefix As System.Windows.Forms.TextBox
    Friend WithEvents lblPreview As System.Windows.Forms.Label
    Friend WithEvents progressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblPct As System.Windows.Forms.Label
    Friend WithEvents btnVistawinBackup As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents tmrProgress As System.Windows.Forms.Timer
    Friend WithEvents BtnBackup As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnGDriveBackup As System.Windows.Forms.Button
    Friend WithEvents chkCompress As System.Windows.Forms.CheckBox
    Friend WithEvents txtApiKey As System.Windows.Forms.TextBox

End Class
