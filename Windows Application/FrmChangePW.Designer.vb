<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChangePW
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
        Me.txtOldPW = New System.Windows.Forms.TextBox()
        Me.BtnChange = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.txtNewPW = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CbOldShow = New System.Windows.Forms.CheckBox()
        Me.CbNewShow = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblUser = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtOldPW
        '
        Me.txtOldPW.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldPW.Location = New System.Drawing.Point(160, 52)
        Me.txtOldPW.MaxLength = 10
        Me.txtOldPW.Name = "txtOldPW"
        Me.txtOldPW.Size = New System.Drawing.Size(164, 25)
        Me.txtOldPW.TabIndex = 0
        Me.txtOldPW.UseSystemPasswordChar = True
        '
        'BtnChange
        '
        Me.BtnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnChange.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnChange.ForeColor = System.Drawing.Color.Crimson
        Me.BtnChange.Location = New System.Drawing.Point(111, 164)
        Me.BtnChange.Name = "BtnChange"
        Me.BtnChange.Size = New System.Drawing.Size(105, 34)
        Me.BtnChange.TabIndex = 2
        Me.BtnChange.Text = "Change"
        Me.BtnChange.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Cambria", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Crimson
        Me.BtnClose.Location = New System.Drawing.Point(258, 164)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(105, 34)
        Me.BtnClose.TabIndex = 3
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'txtNewPW
        '
        Me.txtNewPW.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPW.Location = New System.Drawing.Point(160, 92)
        Me.txtNewPW.MaxLength = 10
        Me.txtNewPW.Name = "txtNewPW"
        Me.txtNewPW.Size = New System.Drawing.Size(164, 25)
        Me.txtNewPW.TabIndex = 1
        Me.txtNewPW.UseSystemPasswordChar = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Crimson
        Me.Label5.Location = New System.Drawing.Point(11, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 19)
        Me.Label5.TabIndex = 815
        Me.Label5.Text = "Old Password"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CbOldShow
        '
        Me.CbOldShow.AutoSize = True
        Me.CbOldShow.Font = New System.Drawing.Font("Cambria", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbOldShow.ForeColor = System.Drawing.Color.Crimson
        Me.CbOldShow.Location = New System.Drawing.Point(330, 53)
        Me.CbOldShow.Name = "CbOldShow"
        Me.CbOldShow.Size = New System.Drawing.Size(66, 23)
        Me.CbOldShow.TabIndex = 816
        Me.CbOldShow.Text = "Show"
        Me.CbOldShow.UseVisualStyleBackColor = True
        '
        'CbNewShow
        '
        Me.CbNewShow.AutoSize = True
        Me.CbNewShow.Font = New System.Drawing.Font("Cambria", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbNewShow.ForeColor = System.Drawing.Color.Crimson
        Me.CbNewShow.Location = New System.Drawing.Point(330, 93)
        Me.CbNewShow.Name = "CbNewShow"
        Me.CbNewShow.Size = New System.Drawing.Size(66, 23)
        Me.CbNewShow.TabIndex = 817
        Me.CbNewShow.Text = "Show"
        Me.CbNewShow.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Crimson
        Me.Label1.Location = New System.Drawing.Point(11, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 19)
        Me.Label1.TabIndex = 818
        Me.Label1.Text = "New Password"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblUser
        '
        Me.LblUser.AutoSize = True
        Me.LblUser.Font = New System.Drawing.Font("Cambria", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUser.ForeColor = System.Drawing.Color.Crimson
        Me.LblUser.Location = New System.Drawing.Point(11, 13)
        Me.LblUser.Name = "LblUser"
        Me.LblUser.Size = New System.Drawing.Size(87, 19)
        Me.LblUser.TabIndex = 819
        Me.LblUser.Text = "User Name"
        Me.LblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUserName
        '
        Me.txtUserName.BackColor = System.Drawing.Color.Gainsboro
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(160, 12)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReadOnly = True
        Me.txtUserName.Size = New System.Drawing.Size(164, 25)
        Me.txtUserName.TabIndex = 4
        Me.txtUserName.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(411, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(202, 19)
        Me.Label7.TabIndex = 868
        Me.Label7.Text = "( Max 10 Letters && Numbers)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tw Cen MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(411, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(122, 19)
        Me.Label6.TabIndex = 867
        Me.Label6.Text = "( Max 10 Letters)"
        '
        'FrmChangePW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Wheat
        Me.ClientSize = New System.Drawing.Size(642, 225)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.LblUser)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CbNewShow)
        Me.Controls.Add(Me.CbOldShow)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtNewPW)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.txtOldPW)
        Me.Controls.Add(Me.BtnChange)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmChangePW"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtOldPW As System.Windows.Forms.TextBox
    Friend WithEvents BtnChange As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents txtNewPW As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CbOldShow As System.Windows.Forms.CheckBox
    Friend WithEvents CbNewShow As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblUser As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
