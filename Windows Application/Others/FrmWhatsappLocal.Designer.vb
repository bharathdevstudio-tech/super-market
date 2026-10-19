<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmWhatsappLocal
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWhatsappLocal))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblConnectedNumber = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.lblServerStatusValue = New System.Windows.Forms.Label()
        Me.lblServerStatus = New System.Windows.Forms.Label()
        Me.btnCheckConnection = New System.Windows.Forms.Button()
        Me.lblWhatsAppStatusValue = New System.Windows.Forms.Label()
        Me.lblWhatsAppStatus = New System.Windows.Forms.Label()
        Me.btnRefreshQR = New System.Windows.Forms.Button()
        Me.btnServerStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.picQR = New System.Windows.Forms.PictureBox()
        Me.lblQRPlaceholder = New System.Windows.Forms.Label()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.lblSelectedFile = New System.Windows.Forms.Label()
        Me.grpResults = New System.Windows.Forms.GroupBox()
        Me.lstResults = New System.Windows.Forms.ListBox()
        Me.lblTotalContacts = New System.Windows.Forms.Label()
        Me.txtMobileNo = New System.Windows.Forms.TextBox()
        Me.grpMore = New System.Windows.Forms.GroupBox()
        Me.GrpClose = New System.Windows.Forms.Button()
        Me.grpProgress = New System.Windows.Forms.GroupBox()
        Me.lblPendingValue = New System.Windows.Forms.Label()
        Me.lblFailedValue = New System.Windows.Forms.Label()
        Me.lblSentValue = New System.Windows.Forms.Label()
        Me.lblTotalValue = New System.Windows.Forms.Label()
        Me.lblPending = New System.Windows.Forms.Label()
        Me.lblFailed = New System.Windows.Forms.Label()
        Me.lblSent = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpDelay = New System.Windows.Forms.GroupBox()
        Me.lblDelayHint = New System.Windows.Forms.Label()
        Me.txtDelay = New System.Windows.Forms.TextBox()
        Me.lblDelayMs = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.timerStatus = New System.Windows.Forms.Timer(Me.components)
        Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.backgroundWorkerSend = New System.ComponentModel.BackgroundWorker()
        Me.BtnNodeStart = New System.Windows.Forms.Button()
        Me.BtnNodeStop = New System.Windows.Forms.Button()
        Me.BtnMore = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CbServerOnline = New System.Windows.Forms.CheckBox()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.LblSelect = New System.Windows.Forms.Label()
        Me.PnlFileType = New System.Windows.Forms.Panel()
        Me.RbImage = New System.Windows.Forms.RadioButton()
        Me.RbPdf = New System.Windows.Forms.RadioButton()
        Me.LblFileType = New System.Windows.Forms.Label()
        Me.CbPlanImage = New System.Windows.Forms.ComboBox()
        Me.PicPlanImage = New System.Windows.Forms.PictureBox()
        Me.DgvTreatList1 = New System.Windows.Forms.DataGridView()
        Me.SNo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tick1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.NextTreatdate1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MessageEng1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusName1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusMobile1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustID1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnPreview1 = New System.Windows.Forms.Button()
        Me.RbSIngleCus = New System.Windows.Forms.RadioButton()
        Me.PnlPerson = New System.Windows.Forms.Panel()
        Me.RbMultiCus = New System.Windows.Forms.RadioButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CBSelect = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpResults.SuspendLayout()
        Me.grpMore.SuspendLayout()
        Me.grpProgress.SuspendLayout()
        Me.grpDelay.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.PnlFileType.SuspendLayout()
        CType(Me.PicPlanImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvTreatList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlPerson.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(174, 60)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(313, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "WhatsApp Document Sender"
        '
        'lblConnectedNumber
        '
        Me.lblConnectedNumber.AutoSize = True
        Me.lblConnectedNumber.BackColor = System.Drawing.Color.AliceBlue
        Me.lblConnectedNumber.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConnectedNumber.ForeColor = System.Drawing.Color.Red
        Me.lblConnectedNumber.Location = New System.Drawing.Point(369, 73)
        Me.lblConnectedNumber.Name = "lblConnectedNumber"
        Me.lblConnectedNumber.Size = New System.Drawing.Size(172, 20)
        Me.lblConnectedNumber.TabIndex = 11
        Me.lblConnectedNumber.Text = "Connected Number: ---"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbCustomer.FormattingEnabled = True
        Me.cmbCustomer.Location = New System.Drawing.Point(124, 22)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(402, 23)
        Me.cmbCustomer.TabIndex = 10
        '
        'lblServerStatusValue
        '
        Me.lblServerStatusValue.AutoSize = True
        Me.lblServerStatusValue.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerStatusValue.Location = New System.Drawing.Point(171, 73)
        Me.lblServerStatusValue.Name = "lblServerStatusValue"
        Me.lblServerStatusValue.Size = New System.Drawing.Size(94, 20)
        Me.lblServerStatusValue.TabIndex = 4
        Me.lblServerStatusValue.Text = "Not Checked"
        '
        'lblServerStatus
        '
        Me.lblServerStatus.AutoSize = True
        Me.lblServerStatus.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerStatus.Location = New System.Drawing.Point(116, 73)
        Me.lblServerStatus.Name = "lblServerStatus"
        Me.lblServerStatus.Size = New System.Drawing.Size(52, 20)
        Me.lblServerStatus.TabIndex = 3
        Me.lblServerStatus.Text = "Status:"
        '
        'btnCheckConnection
        '
        Me.btnCheckConnection.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.btnCheckConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckConnection.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCheckConnection.ForeColor = System.Drawing.Color.White
        Me.btnCheckConnection.Location = New System.Drawing.Point(14, 23)
        Me.btnCheckConnection.Name = "btnCheckConnection"
        Me.btnCheckConnection.Size = New System.Drawing.Size(135, 30)
        Me.btnCheckConnection.TabIndex = 2
        Me.btnCheckConnection.Text = "Check Connection"
        Me.btnCheckConnection.UseVisualStyleBackColor = False
        '
        'lblWhatsAppStatusValue
        '
        Me.lblWhatsAppStatusValue.AutoSize = True
        Me.lblWhatsAppStatusValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblWhatsAppStatusValue.Location = New System.Drawing.Point(430, 284)
        Me.lblWhatsAppStatusValue.Name = "lblWhatsAppStatusValue"
        Me.lblWhatsAppStatusValue.Size = New System.Drawing.Size(88, 15)
        Me.lblWhatsAppStatusValue.TabIndex = 5
        Me.lblWhatsAppStatusValue.Text = "Not Connected"
        '
        'lblWhatsAppStatus
        '
        Me.lblWhatsAppStatus.AutoSize = True
        Me.lblWhatsAppStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblWhatsAppStatus.Location = New System.Drawing.Point(176, 315)
        Me.lblWhatsAppStatus.Name = "lblWhatsAppStatus"
        Me.lblWhatsAppStatus.Size = New System.Drawing.Size(42, 15)
        Me.lblWhatsAppStatus.TabIndex = 4
        Me.lblWhatsAppStatus.Text = "Status:"
        '
        'btnRefreshQR
        '
        Me.btnRefreshQR.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.btnRefreshQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshQR.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshQR.ForeColor = System.Drawing.Color.White
        Me.btnRefreshQR.Location = New System.Drawing.Point(162, 23)
        Me.btnRefreshQR.Name = "btnRefreshQR"
        Me.btnRefreshQR.Size = New System.Drawing.Size(98, 30)
        Me.btnRefreshQR.TabIndex = 3
        Me.btnRefreshQR.Text = "Refresh QR"
        Me.btnRefreshQR.UseVisualStyleBackColor = False
        '
        'btnServerStart
        '
        Me.btnServerStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnServerStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnServerStart.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnServerStart.ForeColor = System.Drawing.Color.White
        Me.btnServerStart.Location = New System.Drawing.Point(654, 25)
        Me.btnServerStart.Name = "btnServerStart"
        Me.btnServerStart.Size = New System.Drawing.Size(107, 30)
        Me.btnServerStart.TabIndex = 12
        Me.btnServerStart.Text = "SERVER 2"
        Me.btnServerStart.UseVisualStyleBackColor = False
        '
        'btnStop
        '
        Me.btnStop.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnStop.Enabled = False
        Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStop.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnStop.ForeColor = System.Drawing.Color.White
        Me.btnStop.Location = New System.Drawing.Point(654, 63)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(107, 30)
        Me.btnStop.TabIndex = 8
        Me.btnStop.Text = "STOP 2"
        Me.btnStop.UseVisualStyleBackColor = False
        '
        'picQR
        '
        Me.picQR.BackColor = System.Drawing.Color.White
        Me.picQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picQR.Location = New System.Drawing.Point(110, 116)
        Me.picQR.Name = "picQR"
        Me.picQR.Size = New System.Drawing.Size(190, 160)
        Me.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picQR.TabIndex = 2
        Me.picQR.TabStop = False
        '
        'lblQRPlaceholder
        '
        Me.lblQRPlaceholder.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblQRPlaceholder.ForeColor = System.Drawing.Color.Gray
        Me.lblQRPlaceholder.Location = New System.Drawing.Point(185, 275)
        Me.lblQRPlaceholder.Name = "lblQRPlaceholder"
        Me.lblQRPlaceholder.Size = New System.Drawing.Size(190, 30)
        Me.lblQRPlaceholder.TabIndex = 1
        Me.lblQRPlaceholder.Text = "Click CHECK CONNECTION to load QR"
        Me.lblQRPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(393, 23)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(88, 30)
        Me.btnLogout.TabIndex = 9
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnBrowse.ForeColor = System.Drawing.Color.White
        Me.btnBrowse.Location = New System.Drawing.Point(649, 80)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 25)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'txtFile
        '
        Me.txtFile.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFile.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtFile.Location = New System.Drawing.Point(111, 81)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(527, 23)
        Me.txtFile.TabIndex = 1
        '
        'lblSelectedFile
        '
        Me.lblSelectedFile.AutoSize = True
        Me.lblSelectedFile.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedFile.Location = New System.Drawing.Point(7, 79)
        Me.lblSelectedFile.Name = "lblSelectedFile"
        Me.lblSelectedFile.Size = New System.Drawing.Size(32, 20)
        Me.lblSelectedFile.TabIndex = 0
        Me.lblSelectedFile.Text = "File"
        '
        'grpResults
        '
        Me.grpResults.Controls.Add(Me.lstResults)
        Me.grpResults.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpResults.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.grpResults.Location = New System.Drawing.Point(306, 110)
        Me.grpResults.Name = "grpResults"
        Me.grpResults.Size = New System.Drawing.Size(486, 166)
        Me.grpResults.TabIndex = 10
        Me.grpResults.TabStop = False
        Me.grpResults.Text = "Results"
        '
        'lstResults
        '
        Me.lstResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstResults.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.lstResults.FormattingEnabled = True
        Me.lstResults.ItemHeight = 14
        Me.lstResults.Location = New System.Drawing.Point(3, 19)
        Me.lstResults.Name = "lstResults"
        Me.lstResults.Size = New System.Drawing.Size(480, 144)
        Me.lstResults.TabIndex = 0
        '
        'lblTotalContacts
        '
        Me.lblTotalContacts.AutoSize = True
        Me.lblTotalContacts.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTotalContacts.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTotalContacts.Location = New System.Drawing.Point(26, 315)
        Me.lblTotalContacts.Name = "lblTotalContacts"
        Me.lblTotalContacts.Size = New System.Drawing.Size(94, 15)
        Me.lblTotalContacts.TabIndex = 2
        Me.lblTotalContacts.Text = "Total Contacts: 0"
        '
        'txtMobileNo
        '
        Me.txtMobileNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtMobileNo.Location = New System.Drawing.Point(111, 110)
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMobileNo.Size = New System.Drawing.Size(527, 23)
        Me.txtMobileNo.TabIndex = 1
        '
        'grpMore
        '
        Me.grpMore.BackColor = System.Drawing.Color.LightPink
        Me.grpMore.Controls.Add(Me.GrpClose)
        Me.grpMore.Controls.Add(Me.lblWhatsAppStatus)
        Me.grpMore.Controls.Add(Me.lblWhatsAppStatusValue)
        Me.grpMore.Controls.Add(Me.lblTotalContacts)
        Me.grpMore.Controls.Add(Me.lblTitle)
        Me.grpMore.Controls.Add(Me.grpProgress)
        Me.grpMore.Controls.Add(Me.cmbCustomer)
        Me.grpMore.Controls.Add(Me.Label1)
        Me.grpMore.Controls.Add(Me.lblQRPlaceholder)
        Me.grpMore.Controls.Add(Me.btnStop)
        Me.grpMore.Controls.Add(Me.btnServerStart)
        Me.grpMore.Controls.Add(Me.grpDelay)
        Me.grpMore.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpMore.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.grpMore.Location = New System.Drawing.Point(6, 209)
        Me.grpMore.Name = "grpMore"
        Me.grpMore.Size = New System.Drawing.Size(80, 37)
        Me.grpMore.TabIndex = 5
        Me.grpMore.TabStop = False
        Me.grpMore.Text = "Msg"
        '
        'GrpClose
        '
        Me.GrpClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GrpClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GrpClose.ForeColor = System.Drawing.Color.White
        Me.GrpClose.Location = New System.Drawing.Point(288, 351)
        Me.GrpClose.Name = "GrpClose"
        Me.GrpClose.Size = New System.Drawing.Size(163, 30)
        Me.GrpClose.TabIndex = 20
        Me.GrpClose.Text = "Close"
        Me.GrpClose.UseVisualStyleBackColor = False
        '
        'grpProgress
        '
        Me.grpProgress.Controls.Add(Me.lblPendingValue)
        Me.grpProgress.Controls.Add(Me.lblFailedValue)
        Me.grpProgress.Controls.Add(Me.lblSentValue)
        Me.grpProgress.Controls.Add(Me.lblTotalValue)
        Me.grpProgress.Controls.Add(Me.lblPending)
        Me.grpProgress.Controls.Add(Me.lblFailed)
        Me.grpProgress.Controls.Add(Me.lblSent)
        Me.grpProgress.Controls.Add(Me.lblTotal)
        Me.grpProgress.Controls.Add(Me.progressBar)
        Me.grpProgress.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpProgress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.grpProgress.Location = New System.Drawing.Point(29, 100)
        Me.grpProgress.Name = "grpProgress"
        Me.grpProgress.Size = New System.Drawing.Size(560, 95)
        Me.grpProgress.TabIndex = 9
        Me.grpProgress.TabStop = False
        Me.grpProgress.Text = "Sending Progress"
        '
        'lblPendingValue
        '
        Me.lblPendingValue.AutoSize = True
        Me.lblPendingValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPendingValue.Location = New System.Drawing.Point(501, 40)
        Me.lblPendingValue.Name = "lblPendingValue"
        Me.lblPendingValue.Size = New System.Drawing.Size(17, 19)
        Me.lblPendingValue.TabIndex = 7
        Me.lblPendingValue.Text = "0"
        '
        'lblFailedValue
        '
        Me.lblFailedValue.AutoSize = True
        Me.lblFailedValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFailedValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblFailedValue.Location = New System.Drawing.Point(349, 40)
        Me.lblFailedValue.Name = "lblFailedValue"
        Me.lblFailedValue.Size = New System.Drawing.Size(17, 19)
        Me.lblFailedValue.TabIndex = 6
        Me.lblFailedValue.Text = "0"
        '
        'lblSentValue
        '
        Me.lblSentValue.AutoSize = True
        Me.lblSentValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSentValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSentValue.Location = New System.Drawing.Point(227, 40)
        Me.lblSentValue.Name = "lblSentValue"
        Me.lblSentValue.Size = New System.Drawing.Size(17, 19)
        Me.lblSentValue.TabIndex = 5
        Me.lblSentValue.Text = "0"
        '
        'lblTotalValue
        '
        Me.lblTotalValue.AutoSize = True
        Me.lblTotalValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalValue.Location = New System.Drawing.Point(91, 40)
        Me.lblTotalValue.Name = "lblTotalValue"
        Me.lblTotalValue.Size = New System.Drawing.Size(17, 19)
        Me.lblTotalValue.TabIndex = 4
        Me.lblTotalValue.Text = "0"
        '
        'lblPending
        '
        Me.lblPending.AutoSize = True
        Me.lblPending.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPending.Location = New System.Drawing.Point(435, 19)
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(54, 15)
        Me.lblPending.TabIndex = 3
        Me.lblPending.Text = "Pending:"
        '
        'lblFailed
        '
        Me.lblFailed.AutoSize = True
        Me.lblFailed.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblFailed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblFailed.Location = New System.Drawing.Point(305, 19)
        Me.lblFailed.Name = "lblFailed"
        Me.lblFailed.Size = New System.Drawing.Size(41, 15)
        Me.lblFailed.TabIndex = 2
        Me.lblFailed.Text = "Failed:"
        '
        'lblSent
        '
        Me.lblSent.AutoSize = True
        Me.lblSent.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSent.Location = New System.Drawing.Point(180, 19)
        Me.lblSent.Name = "lblSent"
        Me.lblSent.Size = New System.Drawing.Size(33, 15)
        Me.lblSent.TabIndex = 1
        Me.lblSent.Text = "Sent:"
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTotal.Location = New System.Drawing.Point(46, 19)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(35, 15)
        Me.lblTotal.TabIndex = 0
        Me.lblTotal.Text = "Total:"
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(18, 62)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(530, 15)
        Me.progressBar.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 20)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Server"
        '
        'grpDelay
        '
        Me.grpDelay.Controls.Add(Me.lblDelayHint)
        Me.grpDelay.Controls.Add(Me.txtDelay)
        Me.grpDelay.Controls.Add(Me.lblDelayMs)
        Me.grpDelay.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpDelay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.grpDelay.Location = New System.Drawing.Point(29, 211)
        Me.grpDelay.Name = "grpDelay"
        Me.grpDelay.Size = New System.Drawing.Size(560, 55)
        Me.grpDelay.TabIndex = 6
        Me.grpDelay.TabStop = False
        Me.grpDelay.Text = "Delay Between Messages"
        '
        'lblDelayHint
        '
        Me.lblDelayHint.AutoSize = True
        Me.lblDelayHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblDelayHint.ForeColor = System.Drawing.Color.Gray
        Me.lblDelayHint.Location = New System.Drawing.Point(180, 28)
        Me.lblDelayHint.Name = "lblDelayHint"
        Me.lblDelayHint.Size = New System.Drawing.Size(218, 13)
        Me.lblDelayHint.TabIndex = 2
        Me.lblDelayHint.Text = "Wait time before sending to next contact"
        '
        'txtDelay
        '
        Me.txtDelay.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDelay.Location = New System.Drawing.Point(95, 24)
        Me.txtDelay.Name = "txtDelay"
        Me.txtDelay.Size = New System.Drawing.Size(75, 25)
        Me.txtDelay.TabIndex = 1
        Me.txtDelay.Text = "2000"
        Me.txtDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDelayMs
        '
        Me.lblDelayMs.AutoSize = True
        Me.lblDelayMs.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDelayMs.Location = New System.Drawing.Point(15, 28)
        Me.lblDelayMs.Name = "lblDelayMs"
        Me.lblDelayMs.Size = New System.Drawing.Size(66, 15)
        Me.lblDelayMs.TabIndex = 0
        Me.lblDelayMs.Text = "Delay (ms):"
        '
        'txtMessage
        '
        Me.txtMessage.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtMessage.Location = New System.Drawing.Point(111, 139)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(527, 23)
        Me.txtMessage.TabIndex = 1
        '
        'btnSend
        '
        Me.btnSend.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSend.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnSend.ForeColor = System.Drawing.Color.White
        Me.btnSend.Location = New System.Drawing.Point(283, 5)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(145, 33)
        Me.btnSend.TabIndex = 7
        Me.btnSend.Text = "Send"
        Me.btnSend.UseVisualStyleBackColor = False
        '
        'timerStatus
        '
        Me.timerStatus.Interval = 3000
        '
        'openFileDialog
        '
        Me.openFileDialog.Filter = resources.GetString("openFileDialog.Filter")
        Me.openFileDialog.Title = "Select Document to Send"
        '
        'backgroundWorkerSend
        '
        Me.backgroundWorkerSend.WorkerReportsProgress = True
        Me.backgroundWorkerSend.WorkerSupportsCancellation = True
        '
        'BtnNodeStart
        '
        Me.BtnNodeStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.BtnNodeStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnNodeStart.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnNodeStart.ForeColor = System.Drawing.Color.White
        Me.BtnNodeStart.Location = New System.Drawing.Point(273, 23)
        Me.BtnNodeStart.Name = "BtnNodeStart"
        Me.BtnNodeStart.Size = New System.Drawing.Size(107, 30)
        Me.BtnNodeStart.TabIndex = 13
        Me.BtnNodeStart.Text = "Server Start"
        Me.BtnNodeStart.UseVisualStyleBackColor = False
        '
        'BtnNodeStop
        '
        Me.BtnNodeStop.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.BtnNodeStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnNodeStop.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnNodeStop.ForeColor = System.Drawing.Color.White
        Me.BtnNodeStop.Location = New System.Drawing.Point(494, 23)
        Me.BtnNodeStop.Name = "BtnNodeStop"
        Me.BtnNodeStop.Size = New System.Drawing.Size(104, 30)
        Me.BtnNodeStop.TabIndex = 14
        Me.BtnNodeStop.Text = "Server Stop"
        Me.BtnNodeStop.UseVisualStyleBackColor = False
        '
        'BtnMore
        '
        Me.BtnMore.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.BtnMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMore.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnMore.ForeColor = System.Drawing.Color.White
        Me.BtnMore.Location = New System.Drawing.Point(611, 23)
        Me.BtnMore.Name = "BtnMore"
        Me.BtnMore.Size = New System.Drawing.Size(75, 30)
        Me.BtnMore.TabIndex = 15
        Me.BtnMore.Text = "More"
        Me.BtnMore.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 159)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 20)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Scan QR"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 139)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Message"
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnClose.ForeColor = System.Drawing.Color.White
        Me.BtnClose.Location = New System.Drawing.Point(463, 5)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(168, 33)
        Me.BtnClose.TabIndex = 19
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 20)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Mobile No"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.grpMore)
        Me.GroupBox1.Controls.Add(Me.CbServerOnline)
        Me.GroupBox1.Controls.Add(Me.BtnReport)
        Me.GroupBox1.Controls.Add(Me.btnLogout)
        Me.GroupBox1.Controls.Add(Me.picQR)
        Me.GroupBox1.Controls.Add(Me.BtnNodeStop)
        Me.GroupBox1.Controls.Add(Me.lblConnectedNumber)
        Me.GroupBox1.Controls.Add(Me.BtnNodeStart)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnRefreshQR)
        Me.GroupBox1.Controls.Add(Me.lblServerStatusValue)
        Me.GroupBox1.Controls.Add(Me.BtnMore)
        Me.GroupBox1.Controls.Add(Me.btnCheckConnection)
        Me.GroupBox1.Controls.Add(Me.grpResults)
        Me.GroupBox1.Controls.Add(Me.lblServerStatus)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(803, 285)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Settings"
        '
        'CbServerOnline
        '
        Me.CbServerOnline.AutoSize = True
        Me.CbServerOnline.Checked = True
        Me.CbServerOnline.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CbServerOnline.Location = New System.Drawing.Point(651, 74)
        Me.CbServerOnline.Name = "CbServerOnline"
        Me.CbServerOnline.Size = New System.Drawing.Size(74, 19)
        Me.CbServerOnline.TabIndex = 19
        Me.CbServerOnline.Text = "Server 2"
        Me.CbServerOnline.UseVisualStyleBackColor = True
        Me.CbServerOnline.Visible = False
        '
        'BtnReport
        '
        Me.BtnReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.BtnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnReport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnReport.ForeColor = System.Drawing.Color.White
        Me.BtnReport.Location = New System.Drawing.Point(699, 23)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(75, 30)
        Me.BtnReport.TabIndex = 18
        Me.BtnReport.Text = "Report"
        Me.BtnReport.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LblSelect)
        Me.GroupBox2.Controls.Add(Me.PnlFileType)
        Me.GroupBox2.Controls.Add(Me.LblFileType)
        Me.GroupBox2.Controls.Add(Me.txtMessage)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtFile)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.CbPlanImage)
        Me.GroupBox2.Controls.Add(Me.txtMobileNo)
        Me.GroupBox2.Controls.Add(Me.lblSelectedFile)
        Me.GroupBox2.Controls.Add(Me.btnBrowse)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(8, 297)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(801, 172)
        Me.GroupBox2.TabIndex = 21
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Messages"
        '
        'LblSelect
        '
        Me.LblSelect.AutoSize = True
        Me.LblSelect.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSelect.Location = New System.Drawing.Point(267, 46)
        Me.LblSelect.Name = "LblSelect"
        Me.LblSelect.Size = New System.Drawing.Size(49, 20)
        Me.LblSelect.TabIndex = 700
        Me.LblSelect.Text = "Select"
        '
        'PnlFileType
        '
        Me.PnlFileType.Controls.Add(Me.RbImage)
        Me.PnlFileType.Controls.Add(Me.RbPdf)
        Me.PnlFileType.Location = New System.Drawing.Point(111, 37)
        Me.PnlFileType.Name = "PnlFileType"
        Me.PnlFileType.Size = New System.Drawing.Size(147, 38)
        Me.PnlFileType.TabIndex = 699
        '
        'RbImage
        '
        Me.RbImage.AutoSize = True
        Me.RbImage.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbImage.Location = New System.Drawing.Point(69, 8)
        Me.RbImage.Name = "RbImage"
        Me.RbImage.Size = New System.Drawing.Size(62, 21)
        Me.RbImage.TabIndex = 698
        Me.RbImage.Text = "Image"
        Me.RbImage.UseVisualStyleBackColor = True
        '
        'RbPdf
        '
        Me.RbPdf.AutoSize = True
        Me.RbPdf.Checked = True
        Me.RbPdf.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbPdf.Location = New System.Drawing.Point(8, 8)
        Me.RbPdf.Name = "RbPdf"
        Me.RbPdf.Size = New System.Drawing.Size(45, 21)
        Me.RbPdf.TabIndex = 697
        Me.RbPdf.TabStop = True
        Me.RbPdf.Text = "Pdf"
        Me.RbPdf.UseVisualStyleBackColor = True
        '
        'LblFileType
        '
        Me.LblFileType.AutoSize = True
        Me.LblFileType.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFileType.Location = New System.Drawing.Point(7, 49)
        Me.LblFileType.Name = "LblFileType"
        Me.LblFileType.Size = New System.Drawing.Size(67, 20)
        Me.LblFileType.TabIndex = 23
        Me.LblFileType.Text = "File Type"
        '
        'CbPlanImage
        '
        Me.CbPlanImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbPlanImage.Font = New System.Drawing.Font("Times New Roman", 11.25!)
        Me.CbPlanImage.Location = New System.Drawing.Point(322, 44)
        Me.CbPlanImage.Name = "CbPlanImage"
        Me.CbPlanImage.Size = New System.Drawing.Size(316, 25)
        Me.CbPlanImage.TabIndex = 22
        '
        'PicPlanImage
        '
        Me.PicPlanImage.BackColor = System.Drawing.Color.White
        Me.PicPlanImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PicPlanImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicPlanImage.Location = New System.Drawing.Point(815, 15)
        Me.PicPlanImage.Name = "PicPlanImage"
        Me.PicPlanImage.Size = New System.Drawing.Size(363, 433)
        Me.PicPlanImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicPlanImage.TabIndex = 18
        Me.PicPlanImage.TabStop = False
        '
        'DgvTreatList1
        '
        Me.DgvTreatList1.AllowUserToAddRows = False
        Me.DgvTreatList1.AllowUserToResizeRows = False
        Me.DgvTreatList1.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvTreatList1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvTreatList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvTreatList1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo1, Me.Tick1, Me.NextTreatdate1, Me.MessageEng1, Me.CusName1, Me.CusMobile1, Me.CustID1})
        Me.DgvTreatList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvTreatList1.EnableHeadersVisualStyles = False
        Me.DgvTreatList1.Location = New System.Drawing.Point(3, 60)
        Me.DgvTreatList1.Name = "DgvTreatList1"
        Me.DgvTreatList1.RowHeadersVisible = False
        Me.DgvTreatList1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvTreatList1.Size = New System.Drawing.Size(1181, 461)
        Me.DgvTreatList1.TabIndex = 695
        '
        'SNo1
        '
        Me.SNo1.HeaderText = "SNo"
        Me.SNo1.Name = "SNo1"
        Me.SNo1.ReadOnly = True
        Me.SNo1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SNo1.Width = 50
        '
        'Tick1
        '
        Me.Tick1.FalseValue = "NO"
        Me.Tick1.HeaderText = "Tick"
        Me.Tick1.Name = "Tick1"
        Me.Tick1.TrueValue = "YES"
        Me.Tick1.Width = 40
        '
        'NextTreatdate1
        '
        Me.NextTreatdate1.HeaderText = "Next Treatment"
        Me.NextTreatdate1.Name = "NextTreatdate1"
        Me.NextTreatdate1.ReadOnly = True
        Me.NextTreatdate1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MessageEng1
        '
        Me.MessageEng1.HeaderText = "Message"
        Me.MessageEng1.Name = "MessageEng1"
        Me.MessageEng1.ReadOnly = True
        Me.MessageEng1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MessageEng1.Width = 300
        '
        'CusName1
        '
        Me.CusName1.HeaderText = "Name"
        Me.CusName1.Name = "CusName1"
        Me.CusName1.ReadOnly = True
        Me.CusName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CusName1.Width = 300
        '
        'CusMobile1
        '
        Me.CusMobile1.HeaderText = "Mobile"
        Me.CusMobile1.Name = "CusMobile1"
        Me.CusMobile1.ReadOnly = True
        Me.CusMobile1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CusMobile1.Width = 150
        '
        'CustID1
        '
        Me.CustID1.HeaderText = "ID"
        Me.CustID1.Name = "CustID1"
        Me.CustID1.ReadOnly = True
        Me.CustID1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BtnPreview1
        '
        Me.BtnPreview1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BtnPreview1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPreview1.Font = New System.Drawing.Font("Bookman Old Style", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPreview1.ForeColor = System.Drawing.Color.Black
        Me.BtnPreview1.Location = New System.Drawing.Point(386, 12)
        Me.BtnPreview1.Name = "BtnPreview1"
        Me.BtnPreview1.Size = New System.Drawing.Size(83, 31)
        Me.BtnPreview1.TabIndex = 696
        Me.BtnPreview1.Text = "Preview"
        Me.BtnPreview1.UseVisualStyleBackColor = False
        '
        'RbSIngleCus
        '
        Me.RbSIngleCus.AutoSize = True
        Me.RbSIngleCus.Checked = True
        Me.RbSIngleCus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbSIngleCus.Location = New System.Drawing.Point(7, 8)
        Me.RbSIngleCus.Name = "RbSIngleCus"
        Me.RbSIngleCus.Size = New System.Drawing.Size(105, 21)
        Me.RbSIngleCus.TabIndex = 697
        Me.RbSIngleCus.TabStop = True
        Me.RbSIngleCus.Text = "Single Person"
        Me.RbSIngleCus.UseVisualStyleBackColor = True
        '
        'PnlPerson
        '
        Me.PnlPerson.Controls.Add(Me.RbMultiCus)
        Me.PnlPerson.Controls.Add(Me.RbSIngleCus)
        Me.PnlPerson.Location = New System.Drawing.Point(12, 472)
        Me.PnlPerson.Name = "PnlPerson"
        Me.PnlPerson.Size = New System.Drawing.Size(249, 38)
        Me.PnlPerson.TabIndex = 698
        '
        'RbMultiCus
        '
        Me.RbMultiCus.AutoSize = True
        Me.RbMultiCus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbMultiCus.Location = New System.Drawing.Point(124, 8)
        Me.RbMultiCus.Name = "RbMultiCus"
        Me.RbMultiCus.Size = New System.Drawing.Size(117, 21)
        Me.RbMultiCus.TabIndex = 698
        Me.RbMultiCus.Text = "Multiple Person"
        Me.RbMultiCus.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1195, 552)
        Me.TabControl1.TabIndex = 699
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.AliceBlue
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.PnlPerson)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.PicPlanImage)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1187, 524)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Whatsapp"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.DgvTreatList1)
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1187, 524)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Send Multiple"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.CBSelect)
        Me.Panel2.Controls.Add(Me.BtnPreview1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1181, 57)
        Me.Panel2.TabIndex = 697
        '
        'CBSelect
        '
        Me.CBSelect.AutoSize = True
        Me.CBSelect.Location = New System.Drawing.Point(170, 22)
        Me.CBSelect.Name = "CBSelect"
        Me.CBSelect.Size = New System.Drawing.Size(74, 19)
        Me.CBSelect.TabIndex = 697
        Me.CBSelect.Text = "Select All"
        Me.CBSelect.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.BtnClose)
        Me.Panel3.Controls.Add(Me.btnSend)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 552)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1195, 43)
        Me.Panel3.TabIndex = 700
        '
        'FrmWhatsappLocal
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(1195, 595)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmWhatsappLocal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WhatsApp"
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpResults.ResumeLayout(False)
        Me.grpMore.ResumeLayout(False)
        Me.grpMore.PerformLayout()
        Me.grpProgress.ResumeLayout(False)
        Me.grpProgress.PerformLayout()
        Me.grpDelay.ResumeLayout(False)
        Me.grpDelay.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.PnlFileType.ResumeLayout(False)
        Me.PnlFileType.PerformLayout()
        CType(Me.PicPlanImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvTreatList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlPerson.ResumeLayout(False)
        Me.PnlPerson.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblConnectedNumber As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents btnCheckConnection As System.Windows.Forms.Button
    Friend WithEvents lblServerStatus As System.Windows.Forms.Label
    Friend WithEvents lblServerStatusValue As System.Windows.Forms.Label
    Friend WithEvents lblQRPlaceholder As System.Windows.Forms.Label
    Friend WithEvents lblWhatsAppStatus As System.Windows.Forms.Label
    Friend WithEvents lblWhatsAppStatusValue As System.Windows.Forms.Label
    Friend WithEvents picQR As System.Windows.Forms.PictureBox
    Friend WithEvents btnRefreshQR As System.Windows.Forms.Button
    Friend WithEvents lblSelectedFile As System.Windows.Forms.Label
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalContacts As System.Windows.Forms.Label
    Friend WithEvents grpMore As System.Windows.Forms.GroupBox
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents grpDelay As System.Windows.Forms.GroupBox
    Friend WithEvents lblDelayMs As System.Windows.Forms.Label
    Friend WithEvents txtDelay As System.Windows.Forms.TextBox
    Friend WithEvents lblDelayHint As System.Windows.Forms.Label
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnServerStart As System.Windows.Forms.Button
    Friend WithEvents btnLogout As System.Windows.Forms.Button
    Friend WithEvents grpProgress As System.Windows.Forms.GroupBox
    Friend WithEvents progressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblSent As System.Windows.Forms.Label
    Friend WithEvents lblFailed As System.Windows.Forms.Label
    Friend WithEvents lblPending As System.Windows.Forms.Label
    Friend WithEvents lblTotalValue As System.Windows.Forms.Label
    Friend WithEvents lblSentValue As System.Windows.Forms.Label
    Friend WithEvents lblFailedValue As System.Windows.Forms.Label
    Friend WithEvents lblPendingValue As System.Windows.Forms.Label
    Friend WithEvents grpResults As System.Windows.Forms.GroupBox
    Friend WithEvents lstResults As System.Windows.Forms.ListBox
    Friend WithEvents timerStatus As System.Windows.Forms.Timer
    Friend WithEvents openFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents backgroundWorkerSend As System.ComponentModel.BackgroundWorker
    Friend WithEvents BtnNodeStart As System.Windows.Forms.Button
    Friend WithEvents BtnNodeStop As System.Windows.Forms.Button
    Friend WithEvents BtnMore As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GrpClose As System.Windows.Forms.Button
    Friend WithEvents CbPlanImage As System.Windows.Forms.ComboBox
    Friend WithEvents PicPlanImage As System.Windows.Forms.PictureBox
    Friend WithEvents BtnReport As System.Windows.Forms.Button
    Friend WithEvents CbServerOnline As System.Windows.Forms.CheckBox
    Friend WithEvents DgvTreatList1 As System.Windows.Forms.DataGridView
    Friend WithEvents BtnPreview1 As System.Windows.Forms.Button
    Friend WithEvents RbSIngleCus As System.Windows.Forms.RadioButton
    Friend WithEvents PnlPerson As System.Windows.Forms.Panel
    Friend WithEvents RbMultiCus As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents LblSelect As System.Windows.Forms.Label
    Friend WithEvents PnlFileType As System.Windows.Forms.Panel
    Friend WithEvents RbImage As System.Windows.Forms.RadioButton
    Friend WithEvents RbPdf As System.Windows.Forms.RadioButton
    Friend WithEvents LblFileType As System.Windows.Forms.Label
    Friend WithEvents SNo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tick1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NextTreatdate1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MessageEng1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusName1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusMobile1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustID1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CBSelect As System.Windows.Forms.CheckBox

End Class
