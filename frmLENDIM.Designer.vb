<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLENDIM
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbboxbrznaci = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxWidth = New System.Windows.Forms.TextBox()
        Me.txtBoxHeight = New System.Windows.Forms.TextBox()
        Me.cmbboxstyle = New System.Windows.Forms.ComboBox()
        Me.lblStyle = New System.Windows.Forms.Label()
        Me.lblWidth = New System.Windows.Forms.Label()
        Me.lblHeight = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtboxoffsetY = New System.Windows.Forms.TextBox()
        Me.txtboxoffsetX = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.PictureBoxpreview = New System.Windows.Forms.PictureBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtboxsufix = New System.Windows.Forms.TextBox()
        Me.txtboxprefix = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBoxpreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbboxbrznaci)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtBoxWidth)
        Me.GroupBox1.Controls.Add(Me.txtBoxHeight)
        Me.GroupBox1.Controls.Add(Me.cmbboxstyle)
        Me.GroupBox1.Controls.Add(Me.lblStyle)
        Me.GroupBox1.Controls.Add(Me.lblWidth)
        Me.GroupBox1.Controls.Add(Me.lblHeight)
        Me.GroupBox1.Location = New System.Drawing.Point(31, 33)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(416, 188)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Настройки на текста"
        '
        'cmbboxbrznaci
        '
        Me.cmbboxbrznaci.FormattingEnabled = True
        Me.cmbboxbrznaci.Location = New System.Drawing.Point(153, 148)
        Me.cmbboxbrznaci.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbboxbrznaci.Name = "cmbboxbrznaci"
        Me.cmbboxbrznaci.Size = New System.Drawing.Size(167, 24)
        Me.cmbboxbrznaci.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 148)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Брой знаци:"
        '
        'txtBoxWidth
        '
        Me.txtBoxWidth.Location = New System.Drawing.Point(153, 111)
        Me.txtBoxWidth.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtBoxWidth.Name = "txtBoxWidth"
        Me.txtBoxWidth.Size = New System.Drawing.Size(167, 22)
        Me.txtBoxWidth.TabIndex = 5
        '
        'txtBoxHeight
        '
        Me.txtBoxHeight.Location = New System.Drawing.Point(153, 73)
        Me.txtBoxHeight.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtBoxHeight.Name = "txtBoxHeight"
        Me.txtBoxHeight.Size = New System.Drawing.Size(167, 22)
        Me.txtBoxHeight.TabIndex = 4
        '
        'cmbboxstyle
        '
        Me.cmbboxstyle.FormattingEnabled = True
        Me.cmbboxstyle.Location = New System.Drawing.Point(153, 32)
        Me.cmbboxstyle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbboxstyle.Name = "cmbboxstyle"
        Me.cmbboxstyle.Size = New System.Drawing.Size(167, 24)
        Me.cmbboxstyle.TabIndex = 3
        '
        'lblStyle
        '
        Me.lblStyle.AutoSize = True
        Me.lblStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStyle.Location = New System.Drawing.Point(8, 36)
        Me.lblStyle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStyle.Name = "lblStyle"
        Me.lblStyle.Size = New System.Drawing.Size(44, 17)
        Me.lblStyle.TabIndex = 2
        Me.lblStyle.Text = "Стил:"
        '
        'lblWidth
        '
        Me.lblWidth.AutoSize = True
        Me.lblWidth.Location = New System.Drawing.Point(8, 111)
        Me.lblWidth.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblWidth.Name = "lblWidth"
        Me.lblWidth.Size = New System.Drawing.Size(61, 16)
        Me.lblWidth.TabIndex = 1
        Me.lblWidth.Text = "Ширина:"
        '
        'lblHeight
        '
        Me.lblHeight.AutoSize = True
        Me.lblHeight.Location = New System.Drawing.Point(8, 73)
        Me.lblHeight.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeight.Name = "lblHeight"
        Me.lblHeight.Size = New System.Drawing.Size(74, 16)
        Me.lblHeight.TabIndex = 0
        Me.lblHeight.Text = "Височина:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtboxoffsetY)
        Me.GroupBox2.Controls.Add(Me.txtboxoffsetX)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(31, 247)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(416, 150)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Настройки на отместването"
        '
        'txtboxoffsetY
        '
        Me.txtboxoffsetY.Location = New System.Drawing.Point(153, 80)
        Me.txtboxoffsetY.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtboxoffsetY.Name = "txtboxoffsetY"
        Me.txtboxoffsetY.Size = New System.Drawing.Size(167, 22)
        Me.txtboxoffsetY.TabIndex = 5
        '
        'txtboxoffsetX
        '
        Me.txtboxoffsetX.Location = New System.Drawing.Point(153, 42)
        Me.txtboxoffsetX.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtboxoffsetX.Name = "txtboxoffsetX"
        Me.txtboxoffsetX.Size = New System.Drawing.Size(167, 22)
        Me.txtboxoffsetX.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 80)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Offset Y:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 42)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Offset X:"
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(184, 726)
        Me.btnSelect.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(143, 46)
        Me.btnSelect.TabIndex = 8
        Me.btnSelect.Text = "Избор на линии"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'PictureBoxpreview
        '
        Me.PictureBoxpreview.Location = New System.Drawing.Point(31, 529)
        Me.PictureBoxpreview.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBoxpreview.Name = "PictureBoxpreview"
        Me.PictureBoxpreview.Size = New System.Drawing.Size(416, 190)
        Me.PictureBoxpreview.TabIndex = 7
        Me.PictureBoxpreview.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtboxsufix)
        Me.GroupBox3.Controls.Add(Me.txtboxprefix)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Location = New System.Drawing.Point(31, 405)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(416, 117)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Префикс/Суфикс"
        '
        'txtboxsufix
        '
        Me.txtboxsufix.Location = New System.Drawing.Point(153, 73)
        Me.txtboxsufix.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtboxsufix.Name = "txtboxsufix"
        Me.txtboxsufix.Size = New System.Drawing.Size(167, 22)
        Me.txtboxsufix.TabIndex = 5
        '
        'txtboxprefix
        '
        Me.txtboxprefix.Location = New System.Drawing.Point(153, 34)
        Me.txtboxprefix.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtboxprefix.Name = "txtboxprefix"
        Me.txtboxprefix.Size = New System.Drawing.Size(167, 22)
        Me.txtboxprefix.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 73)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 16)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Суфикс:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 34)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Префикс:"
        '
        'frmLENDIM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 786)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.PictureBoxpreview)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmLENDIM"
        Me.Text = "Оразмеряване на линии"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBoxpreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents cmbboxstyle As Windows.Forms.ComboBox
    Friend WithEvents lblStyle As Windows.Forms.Label
    Friend WithEvents lblWidth As Windows.Forms.Label
    Friend WithEvents lblHeight As Windows.Forms.Label
    Friend WithEvents txtBoxWidth As Windows.Forms.TextBox
    Friend WithEvents txtBoxHeight As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents txtboxoffsetY As Windows.Forms.TextBox
    Friend WithEvents txtboxoffsetX As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents cmbboxbrznaci As Windows.Forms.ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents btnSelect As Windows.Forms.Button
    Friend WithEvents PictureBoxpreview As Windows.Forms.PictureBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents txtboxsufix As Windows.Forms.TextBox
    Friend WithEvents txtboxprefix As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
End Class
