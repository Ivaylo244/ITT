<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fBTRANSvbNet
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
        Me.btnChoose = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbboxCSkum = New System.Windows.Forms.ComboBox()
        Me.btnTransform = New System.Windows.Forms.Button()
        Me.cmbboxVSkum = New System.Windows.Forms.ComboBox()
        Me.cmbboxCSot = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbboxVSot = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnChoose
        '
        Me.btnChoose.Location = New System.Drawing.Point(43, 176)
        Me.btnChoose.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnChoose.Name = "btnChoose"
        Me.btnChoose.Size = New System.Drawing.Size(92, 33)
        Me.btnChoose.TabIndex = 25
        Me.btnChoose.Text = "Избери"
        Me.btnChoose.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 24)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 17)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Координатна система:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 58)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Oт:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 98)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Към:"
        '
        'cmbboxCSkum
        '
        Me.cmbboxCSkum.FormattingEnabled = True
        Me.cmbboxCSkum.Items.AddRange(New Object() {"БГС2005 Кадастрална", "Софийска", "КС 1970", "КС 1950"})
        Me.cmbboxCSkum.Location = New System.Drawing.Point(56, 95)
        Me.cmbboxCSkum.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmbboxCSkum.Name = "cmbboxCSkum"
        Me.cmbboxCSkum.Size = New System.Drawing.Size(122, 21)
        Me.cmbboxCSkum.TabIndex = 17
        '
        'btnTransform
        '
        Me.btnTransform.Location = New System.Drawing.Point(248, 176)
        Me.btnTransform.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnTransform.Name = "btnTransform"
        Me.btnTransform.Size = New System.Drawing.Size(92, 33)
        Me.btnTransform.TabIndex = 23
        Me.btnTransform.Text = "Трансформирай"
        Me.btnTransform.UseVisualStyleBackColor = True
        '
        'cmbboxVSkum
        '
        Me.cmbboxVSkum.FormattingEnabled = True
        Me.cmbboxVSkum.Items.AddRange(New Object() {"Балтийска", "EVRS 2007"})
        Me.cmbboxVSkum.Location = New System.Drawing.Point(248, 98)
        Me.cmbboxVSkum.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmbboxVSkum.Name = "cmbboxVSkum"
        Me.cmbboxVSkum.Size = New System.Drawing.Size(122, 21)
        Me.cmbboxVSkum.TabIndex = 22
        '
        'cmbboxCSot
        '
        Me.cmbboxCSot.FormattingEnabled = True
        Me.cmbboxCSot.Items.AddRange(New Object() {"БГС2005 Кадастрална", "Софийска", "КС 1970", "КС 1950"})
        Me.cmbboxCSot.Location = New System.Drawing.Point(56, 55)
        Me.cmbboxCSot.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmbboxCSot.Name = "cmbboxCSot"
        Me.cmbboxCSot.Size = New System.Drawing.Size(122, 21)
        Me.cmbboxCSot.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label5.Location = New System.Drawing.Point(212, 98)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Към:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label6.Location = New System.Drawing.Point(212, 58)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Oт:"
        '
        'cmbboxVSot
        '
        Me.cmbboxVSot.FormattingEnabled = True
        Me.cmbboxVSot.Items.AddRange(New Object() {"Балтийска", "EVRS 2007"})
        Me.cmbboxVSot.Location = New System.Drawing.Point(246, 58)
        Me.cmbboxVSot.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmbboxVSot.Name = "cmbboxVSot"
        Me.cmbboxVSot.Size = New System.Drawing.Size(122, 21)
        Me.cmbboxVSot.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.Location = New System.Drawing.Point(214, 24)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 17)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Височинна система:"
        '
        'fBTRANSvbNet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 242)
        Me.Controls.Add(Me.btnChoose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbboxCSkum)
        Me.Controls.Add(Me.btnTransform)
        Me.Controls.Add(Me.cmbboxVSkum)
        Me.Controls.Add(Me.cmbboxCSot)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbboxVSot)
        Me.Controls.Add(Me.Label4)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "fBTRANSvbNet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Трансформиране на точки"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnChoose As Windows.Forms.Button
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents cmbboxCSkum As Windows.Forms.ComboBox
    Friend WithEvents btnTransform As Windows.Forms.Button
    Friend WithEvents cmbboxVSkum As Windows.Forms.ComboBox
    Friend WithEvents cmbboxCSot As Windows.Forms.ComboBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents cmbboxVSot As Windows.Forms.ComboBox
    Friend WithEvents Label4 As Windows.Forms.Label
End Class
