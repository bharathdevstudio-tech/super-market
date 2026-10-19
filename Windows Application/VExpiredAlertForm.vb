Imports System.Windows.Forms
Imports System.Drawing
Imports System.Net
Imports System.IO

Public Class VExpiredAlertForm
    Inherits System.Windows.Forms.Form

    ' ── Runtime data ─────────────────────────────────────────────────────────
    Private _company As String = ""
    Private _renewalDate As String = ""
    Private _overDays As String = "0"
    Private _remark As String = ""
    Private _bankAcName As String = ""
    Private _bankAccNo As String = ""
    Private _bankName As String = ""
    Private _bankBranch As String = ""
    Private _bankIfsc As String = ""
    Private _qrUrl As String = ""

    ' Keep MemoryStream alive — Image.FromStream needs stream open while image shown
    Private _qrStream As MemoryStream

    ' ── Controls declared at class level ─────────────────────────────────────
    Private WithEvents btnOK As Button
    Private picQR As PictureBox
    Private lblQRStatus As Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblIcon As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents lblQRHead As System.Windows.Forms.Label
    Private pnlLeft As Panel

    ' ── Designer constructor (no args) ────────────────────────────────────────
    Public Sub New()
        InitializeComponent()
    End Sub

    ' ── Runtime constructor ───────────────────────────────────────────────────
    Public Sub New(ByVal company As String,
                   ByVal renewalDate As String,
                   ByVal overDays As String,
                   ByVal remark As String,
                   ByVal bankAcName As String,
                   ByVal bankAccNo As String,
                   ByVal bankName As String,
                   ByVal bankBranch As String,
                   ByVal bankIfsc As String,
                   ByVal qrUrl As String)
        _company = company
        _renewalDate = renewalDate
        _overDays = overDays
        _remark = remark
        _bankAcName = bankAcName
        _bankAccNo = bankAccNo
        _bankName = bankName
        _bankBranch = bankBranch
        _bankIfsc = bankIfsc
        _qrUrl = qrUrl
        InitializeComponent()
    End Sub

    ' ═════════════════════════════════════════════════════════════════════════
    ' InitializeComponent — STATIC layout only, no runtime data references
    ' ═════════════════════════════════════════════════════════════════════════
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblIcon = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.lblQRHead = New System.Windows.Forms.Label()
        Me.picQR = New System.Windows.Forms.PictureBox()
        Me.lblQRStatus = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblIcon)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(644, 55)
        Me.pnlHeader.TabIndex = 0
        '
        'lblIcon
        '
        Me.lblIcon.AutoSize = True
        Me.lblIcon.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblIcon.ForeColor = System.Drawing.Color.White
        Me.lblIcon.Location = New System.Drawing.Point(14, 12)
        Me.lblIcon.Name = "lblIcon"
        Me.lblIcon.Size = New System.Drawing.Size(55, 37)
        Me.lblIcon.TabIndex = 0
        Me.lblIcon.Text = "✖"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(63, 12)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(327, 25)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "  ACCESS DENIED  —  Licence Expired"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.White
        Me.pnlLeft.Location = New System.Drawing.Point(10, 65)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(333, 309)
        Me.pnlLeft.TabIndex = 1
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRight.Controls.Add(Me.lblQRHead)
        Me.pnlRight.Controls.Add(Me.picQR)
        Me.pnlRight.Controls.Add(Me.lblQRStatus)
        Me.pnlRight.Location = New System.Drawing.Point(359, 65)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(279, 309)
        Me.pnlRight.TabIndex = 2
        '
        'lblQRHead
        '
        Me.lblQRHead.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblQRHead.ForeColor = System.Drawing.Color.DimGray
        Me.lblQRHead.Location = New System.Drawing.Point(0, 8)
        Me.lblQRHead.Name = "lblQRHead"
        Me.lblQRHead.Size = New System.Drawing.Size(278, 20)
        Me.lblQRHead.TabIndex = 0
        Me.lblQRHead.Text = "Scan to Pay / Contact"
        Me.lblQRHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picQR
        '
        Me.picQR.BackColor = System.Drawing.Color.White
        Me.picQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picQR.Location = New System.Drawing.Point(15, 35)
        Me.picQR.Name = "picQR"
        Me.picQR.Size = New System.Drawing.Size(248, 248)
        Me.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picQR.TabIndex = 1
        Me.picQR.TabStop = False
        '
        'lblQRStatus
        '
        Me.lblQRStatus.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Italic)
        Me.lblQRStatus.ForeColor = System.Drawing.Color.Gray
        Me.lblQRStatus.Location = New System.Drawing.Point(0, 288)
        Me.lblQRStatus.Name = "lblQRStatus"
        Me.lblQRStatus.Size = New System.Drawing.Size(278, 20)
        Me.lblQRStatus.TabIndex = 2
        Me.lblQRStatus.Text = "Loading QR..."
        Me.lblQRStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(260, 380)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(130, 36)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK — Close"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'ExpiredAlertForm
        '
        Me.AcceptButton = Me.btnOK
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(644, 421)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.btnOK)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExpiredAlertForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Licence Expired — Access Denied"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlRight.ResumeLayout(False)
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    ' ═════════════════════════════════════════════════════════════════════════
    ' Form_Load — populate data rows and load QR image
    ' ═════════════════════════════════════════════════════════════════════════
    Private Sub ExpiredAlertForm_Load(ByVal sender As Object,
                                      ByVal e As EventArgs) Handles MyBase.Load
        PopulateLeftPanel()

        If _qrUrl <> "" Then
            lblQRStatus.Text = "Loading QR..."
            LoadQRImage(_qrUrl)
        Else
            lblQRStatus.Text = "No QR image available."
        End If
    End Sub

    ''' <summary>Adds all data rows to pnlLeft at runtime.</summary>
    Private Sub PopulateLeftPanel()
        Dim y As Integer = 5

        ' Licence heading
        AddInfoRow(pnlLeft, "LICENCE INFORMATION", "", y, True)
        y += 22

        AddInfoRow(pnlLeft, "Company", _company, y, False) : y += 24
        AddInfoRow(pnlLeft, "Expired On", _renewalDate, y, False) : y += 24
        AddInfoRow(pnlLeft, "Overdue By", _overDays & " day(s)", y, False) : y += 24

        If _remark <> "" Then
            AddInfoRow(pnlLeft, "Remark", _remark, y, False)
            y += 24
        End If

        y += 8

        ' Red divider
        Dim div1 As New Label()
        div1.BackColor = Color.FromArgb(192, 0, 0)
        div1.Location = New Point(0, y)
        div1.Size = New Size(330, 2)
        pnlLeft.Controls.Add(div1)
        y += 10

        ' Bank details
        If _bankAcName <> "" OrElse _bankAccNo <> "" OrElse _bankName <> "" Then
            AddInfoRow(pnlLeft, "PAYMENT / RENEWAL DETAILS", "", y, True)
            y += 22
            AddInfoRow(pnlLeft, "A/C Name", _bankAcName, y, False) : y += 24
            AddInfoRow(pnlLeft, "A/C Number", _bankAccNo, y, False) : y += 24
            AddInfoRow(pnlLeft, "Bank", _bankName, y, False) : y += 24
            AddInfoRow(pnlLeft, "Branch", _bankBranch, y, False) : y += 24
            AddInfoRow(pnlLeft, "IFSC Code", _bankIfsc, y, False) : y += 24
        End If

        y += 8

        ' Contact message
        Dim div2 As New Label()
        div2.BackColor = Color.LightGray
        div2.Location = New Point(0, y)
        div2.Size = New Size(330, 1)
        pnlLeft.Controls.Add(div2)
        y += 8

        Dim lblContact As New Label()
        lblContact.Text = "Please Contact Your Admin to Renew."
        lblContact.Font = New Font("Segoe UI", 9, FontStyle.Bold Or FontStyle.Italic)
        lblContact.ForeColor = Color.FromArgb(192, 0, 0)
        lblContact.AutoSize = True
        lblContact.Location = New Point(0, y)
        pnlLeft.Controls.Add(lblContact)
    End Sub

    ''' <summary>Adds a label/value row. If isHeading=True renders as section heading.</summary>
    Private Sub AddInfoRow(ByVal parent As Control,
                           ByVal labelText As String,
                           ByVal valueText As String,
                           ByVal y As Integer,
                           ByVal isHeading As Boolean)
        If isHeading Then
            Dim lbl As New Label()
            lbl.Text = labelText
            lbl.Font = New Font("Segoe UI", 8, FontStyle.Bold)
            lbl.ForeColor = Color.Gray
            lbl.AutoSize = True
            lbl.Location = New Point(0, y)
            parent.Controls.Add(lbl)
        Else
            Dim lbl As New Label()
            lbl.Text = labelText & " :"
            lbl.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            lbl.ForeColor = Color.DimGray
            lbl.Location = New Point(0, y)
            lbl.Size = New Size(100, 20)
            parent.Controls.Add(lbl)

            Dim val As New Label()
            val.Text = valueText
            val.Font = New Font("Segoe UI", 9)
            val.ForeColor = Color.Black
            val.Location = New Point(105, y)
            val.Size = New Size(225, 20)
            parent.Controls.Add(val)
        End If
    End Sub

    ''' <summary>Downloads QR image and shows it in picQR.</summary>
    Private Sub LoadQRImage(ByVal url As String)
        Try
            System.Net.ServicePointManager.SecurityProtocol = CType(3072, System.Net.SecurityProtocolType)
            Dim wc As New WebClient()
            wc.Headers.Add("User-Agent", "LoginSystem/1.0")
            Dim imgBytes() As Byte = wc.DownloadData(url)
            _qrStream = New MemoryStream(imgBytes)
            picQR.Image = Image.FromStream(_qrStream)
            lblQRStatus.Text = ""
        Catch ex As Exception
            lblQRStatus.Text = "QR load failed: " & ex.Message
            lblQRStatus.ForeColor = Color.Red
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object,
                             ByVal e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If picQR IsNot Nothing AndAlso picQR.Image IsNot Nothing Then
                    picQR.Image.Dispose()
                    picQR.Image = Nothing
                End If
                If _qrStream IsNot Nothing Then
                    _qrStream.Dispose()
                    _qrStream = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub lblTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTitle.Click

    End Sub
End Class
