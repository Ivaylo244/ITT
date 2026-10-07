<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fP2B
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtBoxFoundPoints = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSelectPoints = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnManualSelectBlock = New System.Windows.Forms.Button()
        Me.cmbBoxSelectBlock = New System.Windows.Forms.ComboBox()
        Me.txtBoxFoundBlocks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbbxselectattrib = New System.Windows.Forms.ComboBox()
        Me.rd2d3d = New System.Windows.Forms.RadioButton()
        Me.rd2d = New System.Windows.Forms.RadioButton()
        Me.rd3d = New System.Windows.Forms.RadioButton()
        Me.cmbboxBLOCKS2 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkboxDeletePoints = New System.Windows.Forms.CheckBox()
        Me.btnConvert2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtBoxFoundPoints)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnSelectPoints)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 26)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(222, 94)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Избор на точки"
        '
        'txtBoxFoundPoints
        '
        Me.txtBoxFoundPoints.Location = New System.Drawing.Point(74, 64)
        Me.txtBoxFoundPoints.Name = "txtBoxFoundPoints"
        Me.txtBoxFoundPoints.Size = New System.Drawing.Size(73, 20)
        Me.txtBoxFoundPoints.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Намерени:"
        '
        'btnSelectPoints
        '
        Me.btnSelectPoints.Location = New System.Drawing.Point(6, 19)
        Me.btnSelectPoints.Name = "btnSelectPoints"
        Me.btnSelectPoints.Size = New System.Drawing.Size(111, 29)
        Me.btnSelectPoints.TabIndex = 0
        Me.btnSelectPoints.Text = "Избор"
        Me.btnSelectPoints.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnManualSelectBlock)
        Me.GroupBox2.Controls.Add(Me.cmbBoxSelectBlock)
        Me.GroupBox2.Controls.Add(Me.txtBoxFoundBlocks)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(267, 26)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(222, 94)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Избор на блокове:"
        '
        'btnManualSelectBlock
        '
        Me.btnManualSelectBlock.Location = New System.Drawing.Point(152, 21)
        Me.btnManualSelectBlock.Name = "btnManualSelectBlock"
        Me.btnManualSelectBlock.Size = New System.Drawing.Size(54, 25)
        Me.btnManualSelectBlock.TabIndex = 4
        Me.btnManualSelectBlock.Text = "M"
        Me.btnManualSelectBlock.UseVisualStyleBackColor = True
        '
        'cmbBoxSelectBlock
        '
        Me.cmbBoxSelectBlock.FormattingEnabled = True
        Me.cmbBoxSelectBlock.Location = New System.Drawing.Point(12, 24)
        Me.cmbBoxSelectBlock.Name = "cmbBoxSelectBlock"
        Me.cmbBoxSelectBlock.Size = New System.Drawing.Size(120, 21)
        Me.cmbBoxSelectBlock.TabIndex = 3
        '
        'txtBoxFoundBlocks
        '
        Me.txtBoxFoundBlocks.Location = New System.Drawing.Point(74, 64)
        Me.txtBoxFoundBlocks.Name = "txtBoxFoundBlocks"
        Me.txtBoxFoundBlocks.Size = New System.Drawing.Size(73, 20)
        Me.txtBoxFoundBlocks.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Намерени:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbbxselectattrib)
        Me.GroupBox3.Controls.Add(Me.rd2d3d)
        Me.GroupBox3.Controls.Add(Me.rd2d)
        Me.GroupBox3.Controls.Add(Me.rd3d)
        Me.GroupBox3.Controls.Add(Me.cmbboxBLOCKS2)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(24, 143)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(465, 188)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Съответствие на атрибути по височина"
        '
        'cmbbxselectattrib
        '
        Me.cmbbxselectattrib.FormattingEnabled = True
        Me.cmbbxselectattrib.Location = New System.Drawing.Point(272, 53)
        Me.cmbbxselectattrib.Name = "cmbbxselectattrib"
        Me.cmbbxselectattrib.Size = New System.Drawing.Size(103, 21)
        Me.cmbbxselectattrib.TabIndex = 12
        '
        'rd2d3d
        '
        Me.rd2d3d.AutoSize = True
        Me.rd2d3d.Location = New System.Drawing.Point(283, 135)
        Me.rd2d3d.Name = "rd2d3d"
        Me.rd2d3d.Size = New System.Drawing.Size(90, 17)
        Me.rd2d3d.TabIndex = 11
        Me.rd2d3d.TabStop = True
        Me.rd2d3d.Text = "3D + атрибут"
        Me.rd2d3d.UseVisualStyleBackColor = True
        '
        'rd2d
        '
        Me.rd2d.AutoSize = True
        Me.rd2d.Location = New System.Drawing.Point(283, 112)
        Me.rd2d.Name = "rd2d"
        Me.rd2d.Size = New System.Drawing.Size(39, 17)
        Me.rd2d.TabIndex = 10
        Me.rd2d.TabStop = True
        Me.rd2d.Text = "2D"
        Me.rd2d.UseVisualStyleBackColor = True
        '
        'rd3d
        '
        Me.rd3d.AutoSize = True
        Me.rd3d.Location = New System.Drawing.Point(283, 89)
        Me.rd3d.Name = "rd3d"
        Me.rd3d.Size = New System.Drawing.Size(39, 17)
        Me.rd3d.TabIndex = 9
        Me.rd3d.TabStop = True
        Me.rd3d.Text = "3D"
        Me.rd3d.UseVisualStyleBackColor = True
        '
        'cmbboxBLOCKS2
        '
        Me.cmbboxBLOCKS2.FormattingEnabled = True
        Me.cmbboxBLOCKS2.Location = New System.Drawing.Point(38, 53)
        Me.cmbboxBLOCKS2.Name = "cmbboxBLOCKS2"
        Me.cmbboxBLOCKS2.Size = New System.Drawing.Size(103, 21)
        Me.cmbboxBLOCKS2.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(280, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Атрибут на блок"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Атрибути на точка"
        '
        'chkboxDeletePoints
        '
        Me.chkboxDeletePoints.AutoSize = True
        Me.chkboxDeletePoints.Location = New System.Drawing.Point(24, 337)
        Me.chkboxDeletePoints.Name = "chkboxDeletePoints"
        Me.chkboxDeletePoints.Size = New System.Drawing.Size(206, 17)
        Me.chkboxDeletePoints.TabIndex = 6
        Me.chkboxDeletePoints.Text = "Изтрий точките след конвертиране"
        Me.chkboxDeletePoints.UseVisualStyleBackColor = True
        '
        'btnConvert2
        '
        Me.btnConvert2.Location = New System.Drawing.Point(215, 399)
        Me.btnConvert2.Name = "btnConvert2"
        Me.btnConvert2.Size = New System.Drawing.Size(97, 38)
        Me.btnConvert2.TabIndex = 7
        Me.btnConvert2.Text = "Конвертирай"
        Me.btnConvert2.UseVisualStyleBackColor = True
        '
        'fP2B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(519, 449)
        Me.Controls.Add(Me.btnConvert2)
        Me.Controls.Add(Me.chkboxDeletePoints)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "fP2B"
        Me.Text = "Конвертиране на точки в блокове"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnSelectPoints As Windows.Forms.Button
    Friend WithEvents txtBoxFoundPoints As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents cmbBoxSelectBlock As Windows.Forms.ComboBox
    Friend WithEvents txtBoxFoundBlocks As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents btnManualSelectBlock As Windows.Forms.Button
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents chkboxDeletePoints As Windows.Forms.CheckBox
    Friend WithEvents cmbbxselectattrib As Windows.Forms.ComboBox
    Friend WithEvents rd2d3d As Windows.Forms.RadioButton
    Friend WithEvents rd2d As Windows.Forms.RadioButton
    Friend WithEvents rd3d As Windows.Forms.RadioButton
    Friend WithEvents cmbboxBLOCKS2 As Windows.Forms.ComboBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents btnConvert2 As Windows.Forms.Button
End Class
