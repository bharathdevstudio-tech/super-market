<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDownload
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
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.LblUltraViewer = New System.Windows.Forms.Label()
        Me.LblAnyDesk = New System.Windows.Forms.Label()
        Me.LblWinRar = New System.Windows.Forms.Label()
        Me.LblAdobe = New System.Windows.Forms.Label()
        Me.BtnMorefiles = New System.Windows.Forms.Button()
        Me.BtnFolderCreate = New System.Windows.Forms.Button()
        Me.LblConPanel = New System.Windows.Forms.Label()
        Me.LblOpenPrinter = New System.Windows.Forms.Label()
        Me.txtDatabaseName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnAppDownload = New System.Windows.Forms.Button()
        Me.BtnCommonInstall = New System.Windows.Forms.Button()
        Me.BtnShowSQL = New System.Windows.Forms.Button()
        Me.btnDownloadInstall = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.LblDownloadFolder = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnClose
        '
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Crimson
        Me.BtnClose.Location = New System.Drawing.Point(941, 217)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(90, 34)
        Me.BtnClose.TabIndex = 4
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'LblUltraViewer
        '
        Me.LblUltraViewer.AutoSize = True
        Me.LblUltraViewer.Font = New System.Drawing.Font("Cambria", 14.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUltraViewer.ForeColor = System.Drawing.Color.Blue
        Me.LblUltraViewer.Location = New System.Drawing.Point(12, 50)
        Me.LblUltraViewer.Name = "LblUltraViewer"
        Me.LblUltraViewer.Size = New System.Drawing.Size(114, 22)
        Me.LblUltraViewer.TabIndex = 820
        Me.LblUltraViewer.Text = "UltraViewer"
        Me.LblUltraViewer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblAnyDesk
        '
        Me.LblAnyDesk.AutoSize = True
        Me.LblAnyDesk.Font = New System.Drawing.Font("Cambria", 14.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAnyDesk.ForeColor = System.Drawing.Color.Blue
        Me.LblAnyDesk.Location = New System.Drawing.Point(12, 9)
        Me.LblAnyDesk.Name = "LblAnyDesk"
        Me.LblAnyDesk.Size = New System.Drawing.Size(83, 22)
        Me.LblAnyDesk.TabIndex = 821
        Me.LblAnyDesk.Text = "AnyDesk"
        Me.LblAnyDesk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblWinRar
        '
        Me.LblWinRar.AutoSize = True
        Me.LblWinRar.Font = New System.Drawing.Font("Cambria", 14.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWinRar.ForeColor = System.Drawing.Color.Blue
        Me.LblWinRar.Location = New System.Drawing.Point(12, 88)
        Me.LblWinRar.Name = "LblWinRar"
        Me.LblWinRar.Size = New System.Drawing.Size(87, 22)
        Me.LblWinRar.TabIndex = 822
        Me.LblWinRar.Text = "Win-RAR"
        Me.LblWinRar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblAdobe
        '
        Me.LblAdobe.AutoSize = True
        Me.LblAdobe.Font = New System.Drawing.Font("Cambria", 14.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAdobe.ForeColor = System.Drawing.Color.Blue
        Me.LblAdobe.Location = New System.Drawing.Point(12, 128)
        Me.LblAdobe.Name = "LblAdobe"
        Me.LblAdobe.Size = New System.Drawing.Size(201, 22)
        Me.LblAdobe.TabIndex = 824
        Me.LblAdobe.Text = "Adobe Acrobat Reader"
        Me.LblAdobe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnMorefiles
        '
        Me.BtnMorefiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMorefiles.Font = New System.Drawing.Font("Cambria", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMorefiles.ForeColor = System.Drawing.Color.Crimson
        Me.BtnMorefiles.Location = New System.Drawing.Point(10, 217)
        Me.BtnMorefiles.Name = "BtnMorefiles"
        Me.BtnMorefiles.Size = New System.Drawing.Size(130, 34)
        Me.BtnMorefiles.TabIndex = 825
        Me.BtnMorefiles.Text = "More Files"
        Me.BtnMorefiles.UseVisualStyleBackColor = True
        '
        'BtnFolderCreate
        '
        Me.BtnFolderCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFolderCreate.Font = New System.Drawing.Font("Cambria", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.BtnFolderCreate.ForeColor = System.Drawing.Color.Crimson
        Me.BtnFolderCreate.Location = New System.Drawing.Point(867, 121)
        Me.BtnFolderCreate.Name = "BtnFolderCreate"
        Me.BtnFolderCreate.Size = New System.Drawing.Size(146, 34)
        Me.BtnFolderCreate.TabIndex = 826
        Me.BtnFolderCreate.Text = "Create Folder"
        Me.BtnFolderCreate.UseVisualStyleBackColor = True
        Me.BtnFolderCreate.Visible = False
        '
        'LblConPanel
        '
        Me.LblConPanel.AutoSize = True
        Me.LblConPanel.Font = New System.Drawing.Font("Cambria", 14.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblConPanel.ForeColor = System.Drawing.Color.Blue
        Me.LblConPanel.Location = New System.Drawing.Point(249, 9)
        Me.LblConPanel.Name = "LblConPanel"
        Me.LblConPanel.Size = New System.Drawing.Size(173, 22)
        Me.LblConPanel.TabIndex = 828
        Me.LblConPanel.Text = "Open Control Panel"
        Me.LblConPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblOpenPrinter
        '
        Me.LblOpenPrinter.AutoSize = True
        Me.LblOpenPrinter.Font = New System.Drawing.Font("Cambria", 14.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOpenPrinter.ForeColor = System.Drawing.Color.Blue
        Me.LblOpenPrinter.Location = New System.Drawing.Point(249, 50)
        Me.LblOpenPrinter.Name = "LblOpenPrinter"
        Me.LblOpenPrinter.Size = New System.Drawing.Size(120, 22)
        Me.LblOpenPrinter.TabIndex = 827
        Me.LblOpenPrinter.Text = "Open Printer"
        Me.LblOpenPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDatabaseName
        '
        Me.txtDatabaseName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatabaseName.Location = New System.Drawing.Point(575, 32)
        Me.txtDatabaseName.Name = "txtDatabaseName"
        Me.txtDatabaseName.ReadOnly = True
        Me.txtDatabaseName.Size = New System.Drawing.Size(328, 24)
        Me.txtDatabaseName.TabIndex = 830
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(572, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 16)
        Me.Label1.TabIndex = 829
        Me.Label1.Text = "Database Name:"
        '
        'BtnAppDownload
        '
        Me.BtnAppDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAppDownload.Font = New System.Drawing.Font("Cambria", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAppDownload.ForeColor = System.Drawing.Color.Crimson
        Me.BtnAppDownload.Location = New System.Drawing.Point(564, 217)
        Me.BtnAppDownload.Name = "BtnAppDownload"
        Me.BtnAppDownload.Size = New System.Drawing.Size(172, 34)
        Me.BtnAppDownload.TabIndex = 834
        Me.BtnAppDownload.Text = "App Download"
        Me.BtnAppDownload.UseVisualStyleBackColor = True
        '
        'BtnCommonInstall
        '
        Me.BtnCommonInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCommonInstall.Font = New System.Drawing.Font("Cambria", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCommonInstall.ForeColor = System.Drawing.Color.Crimson
        Me.BtnCommonInstall.Location = New System.Drawing.Point(418, 217)
        Me.BtnCommonInstall.Name = "BtnCommonInstall"
        Me.BtnCommonInstall.Size = New System.Drawing.Size(130, 34)
        Me.BtnCommonInstall.TabIndex = 833
        Me.BtnCommonInstall.Text = "Exe Install"
        Me.BtnCommonInstall.UseVisualStyleBackColor = True
        '
        'BtnShowSQL
        '
        Me.BtnShowSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnShowSQL.Font = New System.Drawing.Font("Cambria", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShowSQL.ForeColor = System.Drawing.Color.Crimson
        Me.BtnShowSQL.Location = New System.Drawing.Point(752, 217)
        Me.BtnShowSQL.Name = "BtnShowSQL"
        Me.BtnShowSQL.Size = New System.Drawing.Size(173, 34)
        Me.BtnShowSQL.TabIndex = 832
        Me.BtnShowSQL.Text = "Show SQL Form"
        Me.BtnShowSQL.UseVisualStyleBackColor = True
        '
        'btnDownloadInstall
        '
        Me.btnDownloadInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDownloadInstall.Font = New System.Drawing.Font("Cambria", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownloadInstall.ForeColor = System.Drawing.Color.Crimson
        Me.btnDownloadInstall.Location = New System.Drawing.Point(156, 217)
        Me.btnDownloadInstall.Name = "btnDownloadInstall"
        Me.btnDownloadInstall.Size = New System.Drawing.Size(246, 34)
        Me.btnDownloadInstall.TabIndex = 831
        Me.btnDownloadInstall.Text = "Exe Download && Install"
        Me.btnDownloadInstall.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(576, 101)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(42, 15)
        Me.lblStatus.TabIndex = 836
        Me.lblStatus.Text = "Ready"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(575, 69)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(328, 23)
        Me.ProgressBar1.TabIndex = 835
        Me.ProgressBar1.Visible = False
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'LblDownloadFolder
        '
        Me.LblDownloadFolder.AutoSize = True
        Me.LblDownloadFolder.Font = New System.Drawing.Font("Cambria", 14.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDownloadFolder.ForeColor = System.Drawing.Color.Blue
        Me.LblDownloadFolder.Location = New System.Drawing.Point(249, 88)
        Me.LblDownloadFolder.Name = "LblDownloadFolder"
        Me.LblDownloadFolder.Size = New System.Drawing.Size(203, 22)
        Me.LblDownloadFolder.TabIndex = 837
        Me.LblDownloadFolder.Text = "Open Download Folder"
        Me.LblDownloadFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmDownload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Wheat
        Me.ClientSize = New System.Drawing.Size(1046, 275)
        Me.Controls.Add(Me.LblDownloadFolder)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.BtnAppDownload)
        Me.Controls.Add(Me.BtnCommonInstall)
        Me.Controls.Add(Me.BtnShowSQL)
        Me.Controls.Add(Me.btnDownloadInstall)
        Me.Controls.Add(Me.txtDatabaseName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblConPanel)
        Me.Controls.Add(Me.LblOpenPrinter)
        Me.Controls.Add(Me.BtnFolderCreate)
        Me.Controls.Add(Me.BtnMorefiles)
        Me.Controls.Add(Me.LblAdobe)
        Me.Controls.Add(Me.LblWinRar)
        Me.Controls.Add(Me.LblAnyDesk)
        Me.Controls.Add(Me.LblUltraViewer)
        Me.Controls.Add(Me.BtnClose)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDownload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Download"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents LblUltraViewer As System.Windows.Forms.Label
    Friend WithEvents LblAnyDesk As System.Windows.Forms.Label
    Friend WithEvents LblWinRar As System.Windows.Forms.Label
    Friend WithEvents LblAdobe As System.Windows.Forms.Label
    Friend WithEvents BtnMorefiles As System.Windows.Forms.Button
    Friend WithEvents BtnFolderCreate As System.Windows.Forms.Button
    Friend WithEvents LblConPanel As System.Windows.Forms.Label
    Friend WithEvents LblOpenPrinter As System.Windows.Forms.Label
    Friend WithEvents txtDatabaseName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnAppDownload As System.Windows.Forms.Button
    Friend WithEvents BtnCommonInstall As System.Windows.Forms.Button
    Friend WithEvents BtnShowSQL As System.Windows.Forms.Button
    Friend WithEvents btnDownloadInstall As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents LblDownloadFolder As System.Windows.Forms.Label
End Class
