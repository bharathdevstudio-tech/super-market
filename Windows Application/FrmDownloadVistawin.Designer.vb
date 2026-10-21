<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDownloadVistawin
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
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlPath = New System.Windows.Forms.Panel()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnNewFolder = New System.Windows.Forms.Button()
        Me.lblPath = New System.Windows.Forms.Label()
        Me.lvFiles = New System.Windows.Forms.ListView()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnUpload = New System.Windows.Forms.Button()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.btnRename = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.saveDialog = New System.Windows.Forms.SaveFileDialog()
        Me.openDialog = New System.Windows.Forms.OpenFileDialog()
        Me.pnlTop.SuspendLayout()
        Me.pnlPath.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()

        ' ── pnlTop (blue header) ──────────────────────────────────────────
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(0, 102, 204)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Height = 48
        Me.pnlTop.Controls.Add(Me.lblTitle)

        ' ── lblTitle ──────────────────────────────────────────────────────
        Me.lblTitle.AutoSize = False
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Text = "  Hostinger File Manager  —  Bharath SuperMarket"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        ' ── pnlPath (breadcrumb bar) ──────────────────────────────────────
        Me.pnlPath.Anchor = _
            System.Windows.Forms.AnchorStyles.Top Or _
            System.Windows.Forms.AnchorStyles.Left Or _
            System.Windows.Forms.AnchorStyles.Right
        Me.pnlPath.Location = New System.Drawing.Point(0, 48)
        Me.pnlPath.Size = New System.Drawing.Size(884, 36)
        Me.pnlPath.BackColor = System.Drawing.Color.FromArgb(235, 242, 250)
        Me.pnlPath.Controls.Add(Me.btnUp)
        Me.pnlPath.Controls.Add(Me.btnNewFolder)
        Me.pnlPath.Controls.Add(Me.lblPath)

        ' ── btnUp ─────────────────────────────────────────────────────────
        Me.btnUp.Location = New System.Drawing.Point(10, 5)
        Me.btnUp.Size = New System.Drawing.Size(80, 26)
        Me.btnUp.Text = "↑  Up"
        Me.btnUp.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnUp.BackColor = System.Drawing.Color.FromArgb(70, 70, 70)
        Me.btnUp.ForeColor = System.Drawing.Color.White
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.Enabled = False

        ' ── btnNewFolder ──────────────────────────────────────────────────
        Me.btnNewFolder.Location = New System.Drawing.Point(98, 5)
        Me.btnNewFolder.Size = New System.Drawing.Size(120, 26)
        Me.btnNewFolder.Text = "+  New Folder"
        Me.btnNewFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNewFolder.BackColor = System.Drawing.Color.FromArgb(90, 60, 160)
        Me.btnNewFolder.ForeColor = System.Drawing.Color.White
        Me.btnNewFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewFolder.FlatAppearance.BorderSize = 0

        ' ── lblPath ───────────────────────────────────────────────────────
        Me.lblPath.Anchor = _
            System.Windows.Forms.AnchorStyles.Top Or _
            System.Windows.Forms.AnchorStyles.Left Or _
            System.Windows.Forms.AnchorStyles.Right
        Me.lblPath.Location = New System.Drawing.Point(230, 8)
        Me.lblPath.Size = New System.Drawing.Size(640, 20)
        Me.lblPath.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblPath.ForeColor = System.Drawing.Color.FromArgb(30, 60, 100)
        Me.lblPath.Text = "📂  /  (root)"

        ' ── lvFiles ───────────────────────────────────────────────────────
        Me.lvFiles.Anchor = _
            System.Windows.Forms.AnchorStyles.Top Or _
            System.Windows.Forms.AnchorStyles.Bottom Or _
            System.Windows.Forms.AnchorStyles.Left Or _
            System.Windows.Forms.AnchorStyles.Right
        Me.lvFiles.Location = New System.Drawing.Point(12, 92)
        Me.lvFiles.Size = New System.Drawing.Size(860, 358)
        Me.lvFiles.Font = New System.Drawing.Font("Segoe UI", 9.0!)

        ' ── pnlBottom ─────────────────────────────────────────────────────
        Me.pnlBottom.Anchor = _
            System.Windows.Forms.AnchorStyles.Bottom Or _
            System.Windows.Forms.AnchorStyles.Left Or _
            System.Windows.Forms.AnchorStyles.Right
        Me.pnlBottom.Location = New System.Drawing.Point(0, 460)
        Me.pnlBottom.Size = New System.Drawing.Size(884, 80)
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(240, 240, 240)
        Me.pnlBottom.Controls.Add(Me.btnLoad)
        Me.pnlBottom.Controls.Add(Me.btnUpload)
        Me.pnlBottom.Controls.Add(Me.btnDownload)
        Me.pnlBottom.Controls.Add(Me.btnRename)
        Me.pnlBottom.Controls.Add(Me.btnDelete)
        Me.pnlBottom.Controls.Add(Me.progressBar)
        Me.pnlBottom.Controls.Add(Me.lblStatus)

        ' ── btnLoad (blue) ────────────────────────────────────────────────
        Me.btnLoad.Location = New System.Drawing.Point(10, 12)
        Me.btnLoad.Size = New System.Drawing.Size(110, 32)
        Me.btnLoad.Text = "⟳  Load"
        Me.btnLoad.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnLoad.BackColor = System.Drawing.Color.FromArgb(0, 122, 204)
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.FlatAppearance.BorderSize = 0

        ' ── btnUpload (orange) ────────────────────────────────────────────
        Me.btnUpload.Location = New System.Drawing.Point(128, 12)
        Me.btnUpload.Size = New System.Drawing.Size(120, 32)
        Me.btnUpload.Text = "↑  Upload"
        Me.btnUpload.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnUpload.BackColor = System.Drawing.Color.FromArgb(230, 120, 0)
        Me.btnUpload.ForeColor = System.Drawing.Color.White
        Me.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpload.FlatAppearance.BorderSize = 0

        ' ── btnDownload (green) ───────────────────────────────────────────
        Me.btnDownload.Location = New System.Drawing.Point(256, 12)
        Me.btnDownload.Size = New System.Drawing.Size(130, 32)
        Me.btnDownload.Text = "↓  Download"
        Me.btnDownload.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnDownload.BackColor = System.Drawing.Color.FromArgb(0, 153, 76)
        Me.btnDownload.ForeColor = System.Drawing.Color.White
        Me.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDownload.FlatAppearance.BorderSize = 0

        ' ── btnRename (teal) ──────────────────────────────────────────────
        Me.btnRename.Location = New System.Drawing.Point(394, 12)
        Me.btnRename.Size = New System.Drawing.Size(120, 32)
        Me.btnRename.Text = "✎  Rename"
        Me.btnRename.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnRename.BackColor = System.Drawing.Color.FromArgb(0, 140, 140)
        Me.btnRename.ForeColor = System.Drawing.Color.White
        Me.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRename.FlatAppearance.BorderSize = 0

        ' ── btnDelete (red) ───────────────────────────────────────────────
        Me.btnDelete.Location = New System.Drawing.Point(522, 12)
        Me.btnDelete.Size = New System.Drawing.Size(120, 32)
        Me.btnDelete.Text = "✕  Delete"
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(200, 40, 40)
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.FlatAppearance.BorderSize = 0

        ' ── progressBar ───────────────────────────────────────────────────
        Me.progressBar.Anchor = _
            System.Windows.Forms.AnchorStyles.Left Or _
            System.Windows.Forms.AnchorStyles.Right
        Me.progressBar.Location = New System.Drawing.Point(10, 52)
        Me.progressBar.Size = New System.Drawing.Size(860, 8)
        Me.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.progressBar.Visible = False

        ' ── lblStatus ─────────────────────────────────────────────────────
        Me.lblStatus.Anchor = _
            System.Windows.Forms.AnchorStyles.Top Or _
            System.Windows.Forms.AnchorStyles.Left Or _
            System.Windows.Forms.AnchorStyles.Right
        Me.lblStatus.Location = New System.Drawing.Point(655, 20)
        Me.lblStatus.Size = New System.Drawing.Size(215, 20)
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60)
        Me.lblStatus.Text = "Ready"

        ' ── Form1 ─────────────────────────────────────────────────────────
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 542)
        Me.MinimumSize = New System.Drawing.Size(700, 500)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlPath)
        Me.Controls.Add(Me.lvFiles)
        Me.Controls.Add(Me.pnlBottom)
        Me.Name = "Form1"
        Me.Text = "Hostinger File Manager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.BackColor = System.Drawing.Color.White

        Me.pnlTop.ResumeLayout(False)
        Me.pnlPath.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlPath As System.Windows.Forms.Panel
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnNewFolder As System.Windows.Forms.Button
    Friend WithEvents lblPath As System.Windows.Forms.Label
    Friend WithEvents lvFiles As System.Windows.Forms.ListView
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnUpload As System.Windows.Forms.Button
    Friend WithEvents btnDownload As System.Windows.Forms.Button
    Friend WithEvents btnRename As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents progressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents saveDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents openDialog As System.Windows.Forms.OpenFileDialog

End Class
