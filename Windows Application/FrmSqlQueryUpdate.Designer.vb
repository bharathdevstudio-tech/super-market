<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSqlQueryUpdate
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblServerName = New System.Windows.Forms.Label()
        Me.lblDatabase = New System.Windows.Forms.Label()
        Me.lblApiUrl = New System.Windows.Forms.Label()
        Me.txtApiUrl = New System.Windows.Forms.TextBox()
        Me.lblApiKey = New System.Windows.Forms.Label()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.btnLoadRemoteFiles = New System.Windows.Forms.Button()
        Me.lblRemoteFile = New System.Windows.Forms.Label()
        Me.cboRemoteFiles = New System.Windows.Forms.ComboBox()
        Me.btnExecuteRemote = New System.Windows.Forms.Button()
        Me.grpOptions = New System.Windows.Forms.GroupBox()
        Me.chkUseTransaction = New System.Windows.Forms.CheckBox()
        Me.grpProgress = New System.Windows.Forms.GroupBox()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblLogHeader = New System.Windows.Forms.Label()
        Me.rtbLog = New System.Windows.Forms.RichTextBox()
        Me.txtSqlServerName = New System.Windows.Forms.TextBox()
        Me.txtDatabaseName = New System.Windows.Forms.TextBox()
        Me.grpOptions.SuspendLayout()
        Me.grpProgress.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblServerName
        '
        Me.lblServerName.AutoSize = True
        Me.lblServerName.Location = New System.Drawing.Point(12, 15)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(72, 13)
        Me.lblServerName.TabIndex = 0
        Me.lblServerName.Text = "Server Name:"
        '
        'lblDatabase
        '
        Me.lblDatabase.AutoSize = True
        Me.lblDatabase.Location = New System.Drawing.Point(12, 47)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(56, 13)
        Me.lblDatabase.TabIndex = 0
        Me.lblDatabase.Text = "Database:"
        '
        'lblApiUrl
        '
        Me.lblApiUrl.AutoSize = True
        Me.lblApiUrl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblApiUrl.Location = New System.Drawing.Point(12, 85)
        Me.lblApiUrl.Name = "lblApiUrl"
        Me.lblApiUrl.Size = New System.Drawing.Size(52, 13)
        Me.lblApiUrl.TabIndex = 0
        Me.lblApiUrl.Text = "API URL:"
        '
        'txtApiUrl
        '
        Me.txtApiUrl.Location = New System.Drawing.Point(15, 111)
        Me.txtApiUrl.Name = "txtApiUrl"
        Me.txtApiUrl.Size = New System.Drawing.Size(390, 20)
        Me.txtApiUrl.TabIndex = 6
        Me.txtApiUrl.Text = "https://vistawin.store/sqlexcute/sql_api.php"
        '
        'lblApiKey
        '
        Me.lblApiKey.AutoSize = True
        Me.lblApiKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblApiKey.Location = New System.Drawing.Point(11, 58)
        Me.lblApiKey.Name = "lblApiKey"
        Me.lblApiKey.Size = New System.Drawing.Size(48, 13)
        Me.lblApiKey.TabIndex = 0
        Me.lblApiKey.Text = "API Key:"
        '
        'txtApiKey
        '
        Me.txtApiKey.Location = New System.Drawing.Point(65, 58)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.Size = New System.Drawing.Size(130, 20)
        Me.txtApiKey.TabIndex = 7
        Me.txtApiKey.Text = "Hello123"
        Me.txtApiKey.UseSystemPasswordChar = True
        '
        'btnLoadRemoteFiles
        '
        Me.btnLoadRemoteFiles.Location = New System.Drawing.Point(582, 67)
        Me.btnLoadRemoteFiles.Name = "btnLoadRemoteFiles"
        Me.btnLoadRemoteFiles.Size = New System.Drawing.Size(75, 25)
        Me.btnLoadRemoteFiles.TabIndex = 8
        Me.btnLoadRemoteFiles.Text = "Load Files"
        Me.btnLoadRemoteFiles.UseVisualStyleBackColor = True
        '
        'lblRemoteFile
        '
        Me.lblRemoteFile.AutoSize = True
        Me.lblRemoteFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRemoteFile.Location = New System.Drawing.Point(12, 79)
        Me.lblRemoteFile.Name = "lblRemoteFile"
        Me.lblRemoteFile.Size = New System.Drawing.Size(101, 13)
        Me.lblRemoteFile.TabIndex = 0
        Me.lblRemoteFile.Text = "Available SQL Files:"
        '
        'cboRemoteFiles
        '
        Me.cboRemoteFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRemoteFiles.FormattingEnabled = True
        Me.cboRemoteFiles.Location = New System.Drawing.Point(132, 73)
        Me.cboRemoteFiles.MaxDropDownItems = 25
        Me.cboRemoteFiles.Name = "cboRemoteFiles"
        Me.cboRemoteFiles.Size = New System.Drawing.Size(390, 21)
        Me.cboRemoteFiles.TabIndex = 9
        '
        'btnExecuteRemote
        '
        Me.btnExecuteRemote.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnExecuteRemote.ForeColor = System.Drawing.Color.DarkGreen
        Me.btnExecuteRemote.Location = New System.Drawing.Point(165, 107)
        Me.btnExecuteRemote.Name = "btnExecuteRemote"
        Me.btnExecuteRemote.Size = New System.Drawing.Size(246, 28)
        Me.btnExecuteRemote.TabIndex = 10
        Me.btnExecuteRemote.Text = "Execute Selected SQL File"
        Me.btnExecuteRemote.UseVisualStyleBackColor = True
        '
        'grpOptions
        '
        Me.grpOptions.Controls.Add(Me.chkUseTransaction)
        Me.grpOptions.Controls.Add(Me.lblApiKey)
        Me.grpOptions.Controls.Add(Me.lblApiUrl)
        Me.grpOptions.Controls.Add(Me.txtApiKey)
        Me.grpOptions.Controls.Add(Me.txtApiUrl)
        Me.grpOptions.Location = New System.Drawing.Point(551, 107)
        Me.grpOptions.Name = "grpOptions"
        Me.grpOptions.Size = New System.Drawing.Size(247, 184)
        Me.grpOptions.TabIndex = 3
        Me.grpOptions.TabStop = False
        Me.grpOptions.Text = "Execution Options"
        '
        'chkUseTransaction
        '
        Me.chkUseTransaction.AutoSize = True
        Me.chkUseTransaction.Checked = True
        Me.chkUseTransaction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseTransaction.Location = New System.Drawing.Point(15, 22)
        Me.chkUseTransaction.Name = "chkUseTransaction"
        Me.chkUseTransaction.Size = New System.Drawing.Size(196, 17)
        Me.chkUseTransaction.TabIndex = 11
        Me.chkUseTransaction.Text = "Use Transaction (rollback on failure)"
        Me.chkUseTransaction.UseVisualStyleBackColor = True
        '
        'grpProgress
        '
        Me.grpProgress.Controls.Add(Me.progressBar)
        Me.grpProgress.Controls.Add(Me.lblStatus)
        Me.grpProgress.Controls.Add(Me.lblLogHeader)
        Me.grpProgress.Controls.Add(Me.rtbLog)
        Me.grpProgress.Location = New System.Drawing.Point(12, 181)
        Me.grpProgress.Name = "grpProgress"
        Me.grpProgress.Size = New System.Drawing.Size(510, 315)
        Me.grpProgress.TabIndex = 4
        Me.grpProgress.TabStop = False
        Me.grpProgress.Text = "Progress && Execution Log"
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(10, 22)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(494, 22)
        Me.progressBar.TabIndex = 0
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.Location = New System.Drawing.Point(10, 50)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(91, 13)
        Me.lblStatus.TabIndex = 1
        Me.lblStatus.Text = "Status: Ready."
        '
        'lblLogHeader
        '
        Me.lblLogHeader.AutoSize = True
        Me.lblLogHeader.Location = New System.Drawing.Point(10, 72)
        Me.lblLogHeader.Name = "lblLogHeader"
        Me.lblLogHeader.Size = New System.Drawing.Size(78, 13)
        Me.lblLogHeader.TabIndex = 2
        Me.lblLogHeader.Text = "Execution Log:"
        '
        'rtbLog
        '
        Me.rtbLog.BackColor = System.Drawing.Color.Black
        Me.rtbLog.Font = New System.Drawing.Font("Courier New", 8.25!)
        Me.rtbLog.ForeColor = System.Drawing.Color.Lime
        Me.rtbLog.Location = New System.Drawing.Point(10, 90)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.ReadOnly = True
        Me.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbLog.Size = New System.Drawing.Size(494, 215)
        Me.rtbLog.TabIndex = 3
        Me.rtbLog.Text = ""
        '
        'txtSqlServerName
        '
        Me.txtSqlServerName.Location = New System.Drawing.Point(132, 12)
        Me.txtSqlServerName.Name = "txtSqlServerName"
        Me.txtSqlServerName.Size = New System.Drawing.Size(390, 20)
        Me.txtSqlServerName.TabIndex = 11
        Me.txtSqlServerName.Text = "V1SYSTEM\SQLEXPRESS"
        '
        'txtDatabaseName
        '
        Me.txtDatabaseName.Location = New System.Drawing.Point(132, 41)
        Me.txtDatabaseName.Name = "txtDatabaseName"
        Me.txtDatabaseName.Size = New System.Drawing.Size(390, 20)
        Me.txtDatabaseName.TabIndex = 12
        '
        'FrmSqlQueryUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 508)
        Me.Controls.Add(Me.lblRemoteFile)
        Me.Controls.Add(Me.cboRemoteFiles)
        Me.Controls.Add(Me.btnExecuteRemote)
        Me.Controls.Add(Me.lblDatabase)
        Me.Controls.Add(Me.lblServerName)
        Me.Controls.Add(Me.txtDatabaseName)
        Me.Controls.Add(Me.txtSqlServerName)
        Me.Controls.Add(Me.btnLoadRemoteFiles)
        Me.Controls.Add(Me.grpOptions)
        Me.Controls.Add(Me.grpProgress)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmSqlQueryUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SQL Executor - Remote Hostinger Execution"
        Me.grpOptions.ResumeLayout(False)
        Me.grpOptions.PerformLayout()
        Me.grpProgress.ResumeLayout(False)
        Me.grpProgress.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    ' Control declarations
    Friend WithEvents lblServerName As System.Windows.Forms.Label

    Friend WithEvents lblDatabase As System.Windows.Forms.Label

    Friend WithEvents lblApiUrl As System.Windows.Forms.Label
    Friend WithEvents txtApiUrl As System.Windows.Forms.TextBox
    Friend WithEvents lblApiKey As System.Windows.Forms.Label
    Friend WithEvents txtApiKey As System.Windows.Forms.TextBox
    Friend WithEvents btnLoadRemoteFiles As System.Windows.Forms.Button
    Friend WithEvents lblRemoteFile As System.Windows.Forms.Label
    Friend WithEvents cboRemoteFiles As System.Windows.Forms.ComboBox
    Friend WithEvents btnExecuteRemote As System.Windows.Forms.Button

    Friend WithEvents grpOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkUseTransaction As System.Windows.Forms.CheckBox

    Friend WithEvents grpProgress As System.Windows.Forms.GroupBox
    Friend WithEvents progressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblLogHeader As System.Windows.Forms.Label
    Friend WithEvents rtbLog As System.Windows.Forms.RichTextBox
    Friend WithEvents txtSqlServerName As System.Windows.Forms.TextBox
    Friend WithEvents txtDatabaseName As System.Windows.Forms.TextBox

End Class
