<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubFrmInstantQty
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
        Me.GrpStock = New System.Windows.Forms.GroupBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtTotQty = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCurQty = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtStCodeU = New System.Windows.Forms.TextBox()
        Me.txtProNameU = New System.Windows.Forms.TextBox()
        Me.txtStNumberU = New System.Windows.Forms.TextBox()
        Me.txtNewQty = New System.Windows.Forms.TextBox()
        Me.BtnStUpdate = New System.Windows.Forms.Button()
        Me.BtnGrpStClose = New System.Windows.Forms.Button()
        Me.GrpStock.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrpStock
        '
        Me.GrpStock.BackColor = System.Drawing.Color.LightBlue
        Me.GrpStock.Controls.Add(Me.Label24)
        Me.GrpStock.Controls.Add(Me.txtTotQty)
        Me.GrpStock.Controls.Add(Me.Label7)
        Me.GrpStock.Controls.Add(Me.txtCurQty)
        Me.GrpStock.Controls.Add(Me.Label10)
        Me.GrpStock.Controls.Add(Me.Label20)
        Me.GrpStock.Controls.Add(Me.txtStCodeU)
        Me.GrpStock.Controls.Add(Me.txtProNameU)
        Me.GrpStock.Controls.Add(Me.txtStNumberU)
        Me.GrpStock.Controls.Add(Me.txtNewQty)
        Me.GrpStock.Controls.Add(Me.BtnStUpdate)
        Me.GrpStock.Controls.Add(Me.BtnGrpStClose)
        Me.GrpStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpStock.Location = New System.Drawing.Point(0, 0)
        Me.GrpStock.Name = "GrpStock"
        Me.GrpStock.Size = New System.Drawing.Size(700, 207)
        Me.GrpStock.TabIndex = 898
        Me.GrpStock.TabStop = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(11, 94)
        Me.Label24.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(70, 17)
        Me.Label24.TabIndex = 912
        Me.Label24.Text = "Total Qty"
        '
        'txtTotQty
        '
        Me.txtTotQty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtTotQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotQty.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotQty.ForeColor = System.Drawing.Color.Black
        Me.txtTotQty.Location = New System.Drawing.Point(134, 87)
        Me.txtTotQty.Name = "txtTotQty"
        Me.txtTotQty.ReadOnly = True
        Me.txtTotQty.Size = New System.Drawing.Size(112, 25)
        Me.txtTotQty.TabIndex = 911
        Me.txtTotQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(11, 42)
        Me.Label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 17)
        Me.Label7.TabIndex = 910
        Me.Label7.Text = "Curr.Qty"
        '
        'txtCurQty
        '
        Me.txtCurQty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCurQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurQty.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurQty.ForeColor = System.Drawing.Color.Black
        Me.txtCurQty.Location = New System.Drawing.Point(134, 37)
        Me.txtCurQty.Name = "txtCurQty"
        Me.txtCurQty.ReadOnly = True
        Me.txtCurQty.Size = New System.Drawing.Size(112, 25)
        Me.txtCurQty.TabIndex = 909
        Me.txtCurQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(11, 68)
        Me.Label10.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 17)
        Me.Label10.TabIndex = 906
        Me.Label10.Text = "New Qty"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(11, 16)
        Me.Label20.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(105, 17)
        Me.Label20.TabIndex = 905
        Me.Label20.Text = "Product Name"
        '
        'txtStCodeU
        '
        Me.txtStCodeU.BackColor = System.Drawing.Color.White
        Me.txtStCodeU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStCodeU.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStCodeU.ForeColor = System.Drawing.Color.Black
        Me.txtStCodeU.Location = New System.Drawing.Point(564, 37)
        Me.txtStCodeU.Name = "txtStCodeU"
        Me.txtStCodeU.ReadOnly = True
        Me.txtStCodeU.Size = New System.Drawing.Size(117, 25)
        Me.txtStCodeU.TabIndex = 895
        Me.txtStCodeU.Visible = False
        '
        'txtProNameU
        '
        Me.txtProNameU.BackColor = System.Drawing.Color.White
        Me.txtProNameU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProNameU.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProNameU.ForeColor = System.Drawing.Color.Black
        Me.txtProNameU.Location = New System.Drawing.Point(134, 12)
        Me.txtProNameU.Name = "txtProNameU"
        Me.txtProNameU.ReadOnly = True
        Me.txtProNameU.Size = New System.Drawing.Size(547, 25)
        Me.txtProNameU.TabIndex = 894
        '
        'txtStNumberU
        '
        Me.txtStNumberU.BackColor = System.Drawing.Color.White
        Me.txtStNumberU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStNumberU.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStNumberU.ForeColor = System.Drawing.Color.Black
        Me.txtStNumberU.Location = New System.Drawing.Point(564, 62)
        Me.txtStNumberU.Name = "txtStNumberU"
        Me.txtStNumberU.ReadOnly = True
        Me.txtStNumberU.Size = New System.Drawing.Size(117, 25)
        Me.txtStNumberU.TabIndex = 893
        Me.txtStNumberU.Visible = False
        '
        'txtNewQty
        '
        Me.txtNewQty.BackColor = System.Drawing.Color.White
        Me.txtNewQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewQty.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewQty.ForeColor = System.Drawing.Color.Black
        Me.txtNewQty.Location = New System.Drawing.Point(134, 62)
        Me.txtNewQty.Name = "txtNewQty"
        Me.txtNewQty.Size = New System.Drawing.Size(112, 25)
        Me.txtNewQty.TabIndex = 892
        Me.txtNewQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnStUpdate
        '
        Me.BtnStUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnStUpdate.Font = New System.Drawing.Font("Segoe UI", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStUpdate.Location = New System.Drawing.Point(169, 143)
        Me.BtnStUpdate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnStUpdate.Name = "BtnStUpdate"
        Me.BtnStUpdate.Size = New System.Drawing.Size(84, 32)
        Me.BtnStUpdate.TabIndex = 891
        Me.BtnStUpdate.Text = "Update"
        Me.BtnStUpdate.UseVisualStyleBackColor = True
        '
        'BtnGrpStClose
        '
        Me.BtnGrpStClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnGrpStClose.Font = New System.Drawing.Font("Segoe UI", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGrpStClose.Location = New System.Drawing.Point(286, 143)
        Me.BtnGrpStClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnGrpStClose.Name = "BtnGrpStClose"
        Me.BtnGrpStClose.Size = New System.Drawing.Size(84, 32)
        Me.BtnGrpStClose.TabIndex = 890
        Me.BtnGrpStClose.Text = "Close"
        Me.BtnGrpStClose.UseVisualStyleBackColor = True
        '
        'SubFrmInstantQty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 207)
        Me.Controls.Add(Me.GrpStock)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SubFrmInstantQty"
        Me.Text = "Instant Qty"
        Me.GrpStock.ResumeLayout(False)
        Me.GrpStock.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GrpStock As System.Windows.Forms.GroupBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtTotQty As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCurQty As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtStCodeU As System.Windows.Forms.TextBox
    Friend WithEvents txtProNameU As System.Windows.Forms.TextBox
    Friend WithEvents txtStNumberU As System.Windows.Forms.TextBox
    Friend WithEvents txtNewQty As System.Windows.Forms.TextBox
    Friend WithEvents BtnStUpdate As System.Windows.Forms.Button
    Friend WithEvents BtnGrpStClose As System.Windows.Forms.Button
End Class
