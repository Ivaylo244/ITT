<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fBREG
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
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtBoxpath = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmBoxk1 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ComboBoxk2 = New System.Windows.Forms.ComboBox()
        Me.ComboBoxk5 = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ComboBoxk3 = New System.Windows.Forms.ComboBox()
        Me.ComboBoxk4 = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmBoxDesZnaciKota = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmBoxDesZnaciXY = New System.Windows.Forms.ComboBox()
        Me.cmBoxRazdelitel = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBoxlog = New System.Windows.Forms.TextBox()
        Me.btnRec = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cmbboxBlock = New System.Windows.Forms.ComboBox()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.txtBoxpath)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(23, 22)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(347, 49)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Файл"
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(273, 13)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(60, 27)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Търси"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtBoxpath
        '
        Me.txtBoxpath.Location = New System.Drawing.Point(4, 17)
        Me.txtBoxpath.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtBoxpath.Name = "txtBoxpath"
        Me.txtBoxpath.Size = New System.Drawing.Size(242, 21)
        Me.txtBoxpath.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.cmBoxk1)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.ComboBoxk2)
        Me.GroupBox2.Controls.Add(Me.ComboBoxk5)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.ComboBoxk3)
        Me.GroupBox2.Controls.Add(Me.ComboBoxk4)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(23, 85)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(493, 40)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Колони"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 19)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(14, 15)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "1"
        '
        'cmBoxk1
        '
        Me.cmBoxk1.FormattingEnabled = True
        Me.cmBoxk1.Items.AddRange(New Object() {"NOMER", "X", "Y", "Z", "KOTA", "DESK", "", ""})
        Me.cmBoxk1.Location = New System.Drawing.Point(20, 15)
        Me.cmBoxk1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmBoxk1.Name = "cmBoxk1"
        Me.cmBoxk1.Size = New System.Drawing.Size(59, 23)
        Me.cmBoxk1.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label9.Location = New System.Drawing.Point(91, 18)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(14, 15)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "2"
        '
        'ComboBoxk2
        '
        Me.ComboBoxk2.FormattingEnabled = True
        Me.ComboBoxk2.Items.AddRange(New Object() {"NOMER", "X", "Y", "Z", "KOTA", "DESK", "", ""})
        Me.ComboBoxk2.Location = New System.Drawing.Point(106, 16)
        Me.ComboBoxk2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ComboBoxk2.Name = "ComboBoxk2"
        Me.ComboBoxk2.Size = New System.Drawing.Size(56, 23)
        Me.ComboBoxk2.TabIndex = 19
        '
        'ComboBoxk5
        '
        Me.ComboBoxk5.FormattingEnabled = True
        Me.ComboBoxk5.Items.AddRange(New Object() {"NOMER", "X", "Y", "Z", "KOTA", "DESK", "", ""})
        Me.ComboBoxk5.Location = New System.Drawing.Point(373, 15)
        Me.ComboBoxk5.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ComboBoxk5.Name = "ComboBoxk5"
        Me.ComboBoxk5.Size = New System.Drawing.Size(76, 23)
        Me.ComboBoxk5.TabIndex = 25
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label10.Location = New System.Drawing.Point(180, 19)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(14, 15)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "3"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label12.Location = New System.Drawing.Point(355, 19)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(14, 15)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "5"
        '
        'ComboBoxk3
        '
        Me.ComboBoxk3.FormattingEnabled = True
        Me.ComboBoxk3.Items.AddRange(New Object() {"NOMER", "X", "Y", "Z", "KOTA", "DESK", "", ""})
        Me.ComboBoxk3.Location = New System.Drawing.Point(195, 15)
        Me.ComboBoxk3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ComboBoxk3.Name = "ComboBoxk3"
        Me.ComboBoxk3.Size = New System.Drawing.Size(59, 23)
        Me.ComboBoxk3.TabIndex = 21
        '
        'ComboBoxk4
        '
        Me.ComboBoxk4.FormattingEnabled = True
        Me.ComboBoxk4.Items.AddRange(New Object() {"NOMER", "X", "Y", "Z", "KOTA", "DESK", "", ""})
        Me.ComboBoxk4.Location = New System.Drawing.Point(286, 15)
        Me.ComboBoxk4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ComboBoxk4.Name = "ComboBoxk4"
        Me.ComboBoxk4.Size = New System.Drawing.Size(59, 23)
        Me.ComboBoxk4.TabIndex = 23
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label11.Location = New System.Drawing.Point(271, 18)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(14, 15)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "4"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmBoxDesZnaciKota)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.cmBoxDesZnaciXY)
        Me.GroupBox3.Controls.Add(Me.cmBoxRazdelitel)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(23, 143)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(285, 119)
        Me.GroupBox3.TabIndex = 30
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Настройки"
        '
        'cmBoxDesZnaciKota
        '
        Me.cmBoxDesZnaciKota.FormattingEnabled = True
        Me.cmBoxDesZnaciKota.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cmBoxDesZnaciKota.Location = New System.Drawing.Point(172, 90)
        Me.cmBoxDesZnaciKota.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmBoxDesZnaciKota.Name = "cmBoxDesZnaciKota"
        Me.cmBoxDesZnaciKota.Size = New System.Drawing.Size(78, 23)
        Me.cmBoxDesZnaciKota.TabIndex = 34
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 90)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 15)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Десетични знаци кота"
        '
        'cmBoxDesZnaciXY
        '
        Me.cmBoxDesZnaciXY.FormattingEnabled = True
        Me.cmBoxDesZnaciXY.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cmBoxDesZnaciXY.Location = New System.Drawing.Point(172, 54)
        Me.cmBoxDesZnaciXY.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmBoxDesZnaciXY.Name = "cmBoxDesZnaciXY"
        Me.cmBoxDesZnaciXY.Size = New System.Drawing.Size(78, 23)
        Me.cmBoxDesZnaciXY.TabIndex = 32
        '
        'cmBoxRazdelitel
        '
        Me.cmBoxRazdelitel.FormattingEnabled = True
        Me.cmBoxRazdelitel.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.cmBoxRazdelitel.Location = New System.Drawing.Point(172, 18)
        Me.cmBoxRazdelitel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmBoxRazdelitel.Name = "cmBoxRazdelitel"
        Me.cmBoxRazdelitel.Size = New System.Drawing.Size(74, 23)
        Me.cmBoxRazdelitel.TabIndex = 23
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 54)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 15)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Десетични знаци X, Y"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 23)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 15)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Разделител:"
        '
        'txtBoxlog
        '
        Me.txtBoxlog.Location = New System.Drawing.Point(342, 147)
        Me.txtBoxlog.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtBoxlog.Multiline = True
        Me.txtBoxlog.Name = "txtBoxlog"
        Me.txtBoxlog.Size = New System.Drawing.Size(174, 110)
        Me.txtBoxlog.TabIndex = 31
        '
        'btnRec
        '
        Me.btnRec.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnRec.Location = New System.Drawing.Point(307, 266)
        Me.btnRec.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnRec.Name = "btnRec"
        Me.btnRec.Size = New System.Drawing.Size(60, 27)
        Me.btnRec.TabIndex = 32
        Me.btnRec.Text = "Запиши"
        Me.btnRec.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cmbboxBlock)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(387, 22)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(132, 49)
        Me.GroupBox4.TabIndex = 33
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Блок"
        '
        'cmbboxBlock
        '
        Me.cmbboxBlock.FormattingEnabled = True
        Me.cmbboxBlock.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.cmbboxBlock.Location = New System.Drawing.Point(23, 18)
        Me.cmbboxBlock.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmbboxBlock.Name = "cmbboxBlock"
        Me.cmbboxBlock.Size = New System.Drawing.Size(97, 23)
        Me.cmbboxBlock.TabIndex = 35
        '
        'btnSelect
        '
        Me.btnSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(178, 266)
        Me.btnSelect.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(60, 27)
        Me.btnSelect.TabIndex = 34
        Me.btnSelect.Text = "Избери"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'fBREG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 309)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.btnRec)
        Me.Controls.Add(Me.txtBoxlog)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "fBREG"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Запис във файл"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnSearch As Windows.Forms.Button
    Friend WithEvents txtBoxpath As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents cmBoxk1 As Windows.Forms.ComboBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents ComboBoxk2 As Windows.Forms.ComboBox
    Friend WithEvents ComboBoxk5 As Windows.Forms.ComboBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents ComboBoxk3 As Windows.Forms.ComboBox
    Friend WithEvents ComboBoxk4 As Windows.Forms.ComboBox
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents cmBoxDesZnaciKota As Windows.Forms.ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents cmBoxDesZnaciXY As Windows.Forms.ComboBox
    Friend WithEvents cmBoxRazdelitel As Windows.Forms.ComboBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtBoxlog As Windows.Forms.TextBox
    Friend WithEvents btnRec As Windows.Forms.Button
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents cmbboxBlock As Windows.Forms.ComboBox
    Friend WithEvents btnSelect As Windows.Forms.Button
End Class
