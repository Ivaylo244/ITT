<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRATT
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbbxBL = New System.Windows.Forms.ComboBox()
        Me.cmbbxattr = New System.Windows.Forms.ComboBox()
        Me.btnMBl = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbbxround = New System.Windows.Forms.ComboBox()
        Me.btnRound = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Избери блок:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Избери атрибут:"
        '
        'cmbbxBL
        '
        Me.cmbbxBL.FormattingEnabled = True
        Me.cmbbxBL.Location = New System.Drawing.Point(123, 39)
        Me.cmbbxBL.Name = "cmbbxBL"
        Me.cmbbxBL.Size = New System.Drawing.Size(121, 21)
        Me.cmbbxBL.TabIndex = 2
        '
        'cmbbxattr
        '
        Me.cmbbxattr.FormattingEnabled = True
        Me.cmbbxattr.Location = New System.Drawing.Point(123, 75)
        Me.cmbbxattr.Name = "cmbbxattr"
        Me.cmbbxattr.Size = New System.Drawing.Size(121, 21)
        Me.cmbbxattr.TabIndex = 3
        '
        'btnMBl
        '
        Me.btnMBl.Location = New System.Drawing.Point(269, 34)
        Me.btnMBl.Name = "btnMBl"
        Me.btnMBl.Size = New System.Drawing.Size(37, 28)
        Me.btnMBl.TabIndex = 4
        Me.btnMBl.Text = "М"
        Me.btnMBl.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Прецизност:"
        '
        'cmbbxround
        '
        Me.cmbbxround.FormattingEnabled = True
        Me.cmbbxround.Location = New System.Drawing.Point(123, 108)
        Me.cmbbxround.Name = "cmbbxround"
        Me.cmbbxround.Size = New System.Drawing.Size(121, 21)
        Me.cmbbxround.TabIndex = 7
        '
        'btnRound
        '
        Me.btnRound.Location = New System.Drawing.Point(123, 163)
        Me.btnRound.Name = "btnRound"
        Me.btnRound.Size = New System.Drawing.Size(77, 31)
        Me.btnRound.TabIndex = 8
        Me.btnRound.Text = "Закръгли"
        Me.btnRound.UseVisualStyleBackColor = True
        '
        'frmRATT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 206)
        Me.Controls.Add(Me.btnRound)
        Me.Controls.Add(Me.cmbbxround)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnMBl)
        Me.Controls.Add(Me.cmbbxattr)
        Me.Controls.Add(Me.cmbbxBL)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmRATT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Закръгляне на атрибути"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents cmbbxBL As Windows.Forms.ComboBox
    Friend WithEvents cmbbxattr As Windows.Forms.ComboBox
    Friend WithEvents btnMBl As Windows.Forms.Button
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents cmbbxround As Windows.Forms.ComboBox
    Friend WithEvents btnRound As Windows.Forms.Button
End Class
