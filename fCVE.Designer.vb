<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fCVE
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
        Me.txtBoxDirectory = New System.Windows.Forms.TextBox()
        Me.lblDirectory = New System.Windows.Forms.Label()
        Me.ComBoxBlock = New System.Windows.Forms.ComboBox()
        Me.lblBlock = New System.Windows.Forms.Label()
        Me.CombBoxAttrib = New System.Windows.Forms.ComboBox()
        Me.lblAttrib = New System.Windows.Forms.Label()
        Me.lblDecimal = New System.Windows.Forms.Label()
        Me.ComBoxDecimal = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxScale = New System.Windows.Forms.TextBox()
        Me.lblRotation = New System.Windows.Forms.Label()
        Me.txtBoxRotation = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnSelecLines = New System.Windows.Forms.Button()
        Me.txtBoxStartNumber = New System.Windows.Forms.TextBox()
        Me.txtStartNumber = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtBoxDirectory
        '
        Me.txtBoxDirectory.Location = New System.Drawing.Point(18, 48)
        Me.txtBoxDirectory.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtBoxDirectory.Name = "txtBoxDirectory"
        Me.txtBoxDirectory.Size = New System.Drawing.Size(268, 20)
        Me.txtBoxDirectory.TabIndex = 0
        '
        'lblDirectory
        '
        Me.lblDirectory.AutoSize = True
        Me.lblDirectory.Location = New System.Drawing.Point(16, 24)
        Me.lblDirectory.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDirectory.Name = "lblDirectory"
        Me.lblDirectory.Size = New System.Drawing.Size(171, 13)
        Me.lblDirectory.TabIndex = 1
        Me.lblDirectory.Text = "Въведете директория на файла:"
        '
        'ComBoxBlock
        '
        Me.ComBoxBlock.FormattingEnabled = True
        Me.ComBoxBlock.Location = New System.Drawing.Point(18, 108)
        Me.ComBoxBlock.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ComBoxBlock.Name = "ComBoxBlock"
        Me.ComBoxBlock.Size = New System.Drawing.Size(111, 21)
        Me.ComBoxBlock.TabIndex = 2
        '
        'lblBlock
        '
        Me.lblBlock.AutoSize = True
        Me.lblBlock.Location = New System.Drawing.Point(16, 84)
        Me.lblBlock.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblBlock.Name = "lblBlock"
        Me.lblBlock.Size = New System.Drawing.Size(35, 13)
        Me.lblBlock.TabIndex = 3
        Me.lblBlock.Text = "Блок:"
        '
        'CombBoxAttrib
        '
        Me.CombBoxAttrib.FormattingEnabled = True
        Me.CombBoxAttrib.Location = New System.Drawing.Point(175, 108)
        Me.CombBoxAttrib.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CombBoxAttrib.Name = "CombBoxAttrib"
        Me.CombBoxAttrib.Size = New System.Drawing.Size(111, 21)
        Me.CombBoxAttrib.TabIndex = 4
        '
        'lblAttrib
        '
        Me.lblAttrib.AutoSize = True
        Me.lblAttrib.Location = New System.Drawing.Point(172, 84)
        Me.lblAttrib.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblAttrib.Name = "lblAttrib"
        Me.lblAttrib.Size = New System.Drawing.Size(50, 13)
        Me.lblAttrib.TabIndex = 5
        Me.lblAttrib.Text = "Атрибут:"
        '
        'lblDecimal
        '
        Me.lblDecimal.AutoSize = True
        Me.lblDecimal.Location = New System.Drawing.Point(16, 156)
        Me.lblDecimal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDecimal.Name = "lblDecimal"
        Me.lblDecimal.Size = New System.Drawing.Size(88, 13)
        Me.lblDecimal.TabIndex = 6
        Me.lblDecimal.Text = "брой дес. знаци"
        '
        'ComBoxDecimal
        '
        Me.ComBoxDecimal.FormattingEnabled = True
        Me.ComBoxDecimal.Location = New System.Drawing.Point(18, 181)
        Me.ComBoxDecimal.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ComBoxDecimal.Name = "ComBoxDecimal"
        Me.ComBoxDecimal.Size = New System.Drawing.Size(110, 21)
        Me.ComBoxDecimal.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(172, 156)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Мащаб"
        '
        'txtBoxScale
        '
        Me.txtBoxScale.Location = New System.Drawing.Point(175, 181)
        Me.txtBoxScale.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtBoxScale.Name = "txtBoxScale"
        Me.txtBoxScale.Size = New System.Drawing.Size(111, 20)
        Me.txtBoxScale.TabIndex = 9
        '
        'lblRotation
        '
        Me.lblRotation.AutoSize = True
        Me.lblRotation.Location = New System.Drawing.Point(16, 227)
        Me.lblRotation.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblRotation.Name = "lblRotation"
        Me.lblRotation.Size = New System.Drawing.Size(49, 13)
        Me.lblRotation.TabIndex = 10
        Me.lblRotation.Text = "Ротация"
        '
        'txtBoxRotation
        '
        Me.txtBoxRotation.Location = New System.Drawing.Point(17, 250)
        Me.txtBoxRotation.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtBoxRotation.Name = "txtBoxRotation"
        Me.txtBoxRotation.Size = New System.Drawing.Size(111, 20)
        Me.txtBoxRotation.TabIndex = 11
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(298, 46)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(77, 24)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "Търси"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnSelecLines
        '
        Me.btnSelecLines.Location = New System.Drawing.Point(136, 292)
        Me.btnSelecLines.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSelecLines.Name = "btnSelecLines"
        Me.btnSelecLines.Size = New System.Drawing.Size(124, 26)
        Me.btnSelecLines.TabIndex = 13
        Me.btnSelecLines.Text = "Избор на линии"
        Me.btnSelecLines.UseVisualStyleBackColor = True
        '
        'txtBoxStartNumber
        '
        Me.txtBoxStartNumber.Location = New System.Drawing.Point(175, 250)
        Me.txtBoxStartNumber.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtBoxStartNumber.Name = "txtBoxStartNumber"
        Me.txtBoxStartNumber.Size = New System.Drawing.Size(111, 20)
        Me.txtBoxStartNumber.TabIndex = 14
        '
        'txtStartNumber
        '
        Me.txtStartNumber.AutoSize = True
        Me.txtStartNumber.Location = New System.Drawing.Point(172, 227)
        Me.txtStartNumber.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtStartNumber.Name = "txtStartNumber"
        Me.txtStartNumber.Size = New System.Drawing.Size(85, 13)
        Me.txtStartNumber.TabIndex = 15
        Me.txtStartNumber.Text = "Начален номер"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(59, 79)
        Me.btnSelect.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(18, 19)
        Me.btnSelect.TabIndex = 16
        Me.btnSelect.Text = "B"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'fCVE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 341)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.txtStartNumber)
        Me.Controls.Add(Me.txtBoxStartNumber)
        Me.Controls.Add(Me.btnSelecLines)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtBoxRotation)
        Me.Controls.Add(Me.lblRotation)
        Me.Controls.Add(Me.txtBoxScale)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComBoxDecimal)
        Me.Controls.Add(Me.lblDecimal)
        Me.Controls.Add(Me.lblAttrib)
        Me.Controls.Add(Me.CombBoxAttrib)
        Me.Controls.Add(Me.lblBlock)
        Me.Controls.Add(Me.ComBoxBlock)
        Me.Controls.Add(Me.lblDirectory)
        Me.Controls.Add(Me.txtBoxDirectory)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "fCVE"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Извеждане на координати на линии"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtBoxDirectory As Windows.Forms.TextBox
    Friend WithEvents lblDirectory As Windows.Forms.Label
    Friend WithEvents ComBoxBlock As Windows.Forms.ComboBox
    Friend WithEvents lblBlock As Windows.Forms.Label
    Friend WithEvents CombBoxAttrib As Windows.Forms.ComboBox
    Friend WithEvents lblAttrib As Windows.Forms.Label
    Friend WithEvents lblDecimal As Windows.Forms.Label
    Friend WithEvents ComBoxDecimal As Windows.Forms.ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtBoxScale As Windows.Forms.TextBox
    Friend WithEvents lblRotation As Windows.Forms.Label
    Friend WithEvents txtBoxRotation As Windows.Forms.TextBox
    Friend WithEvents btnSearch As Windows.Forms.Button
    Friend WithEvents btnSelecLines As Windows.Forms.Button
    Friend WithEvents txtBoxStartNumber As Windows.Forms.TextBox
    Friend WithEvents txtStartNumber As Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents btnSelect As Windows.Forms.Button
End Class
