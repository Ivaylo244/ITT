<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fSHARK
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
        Me.txtBlockNames = New System.Windows.Forms.TextBox()
        Me.lblBlockNames = New System.Windows.Forms.Label()
        Me.txtTextLayers = New System.Windows.Forms.TextBox()
        Me.lblTextLayers = New System.Windows.Forms.Label()
        Me.txtSearchRadius = New System.Windows.Forms.TextBox()
        Me.lblSearchRadius = New System.Windows.Forms.Label()
        Me.txtOutputCsv = New System.Windows.Forms.TextBox()
        Me.lblOutputCsv = New System.Windows.Forms.Label()
        Me.chkUseTextLayerFilter = New System.Windows.Forms.CheckBox()
        Me.chkText = New System.Windows.Forms.CheckBox()
        Me.chkMText = New System.Windows.Forms.CheckBox()
        Me.btnBrowseOutputCsv = New System.Windows.Forms.Button()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.lblLog = New System.Windows.Forms.Label()
        Me.btnSelectM = New System.Windows.Forms.Button()
        Me.grpPoints = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.checkMtextLine = New System.Windows.Forms.CheckBox()
        Me.checkTextLine = New System.Windows.Forms.CheckBox()
        Me.checkLayLines = New System.Windows.Forms.CheckBox()
        Me.txtLogLines = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOutputCsvLines = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSearchRadiusLines = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTextLayersLines = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxLine = New System.Windows.Forms.TextBox()
        Me.btnMSelectLine = New System.Windows.Forms.Button()
        Me.lblLinePLine = New System.Windows.Forms.Label()
        Me.grpPoints.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtBlockNames
        '
        Me.txtBlockNames.Location = New System.Drawing.Point(5, 64)
        Me.txtBlockNames.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtBlockNames.Name = "txtBlockNames"
        Me.txtBlockNames.Size = New System.Drawing.Size(114, 20)
        Me.txtBlockNames.TabIndex = 0
        '
        'lblBlockNames
        '
        Me.lblBlockNames.AutoSize = True
        Me.lblBlockNames.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBlockNames.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblBlockNames.Location = New System.Drawing.Point(5, 28)
        Me.lblBlockNames.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblBlockNames.Name = "lblBlockNames"
        Me.lblBlockNames.Size = New System.Drawing.Size(40, 17)
        Me.lblBlockNames.TabIndex = 1
        Me.lblBlockNames.Text = "Блок:"
        '
        'txtTextLayers
        '
        Me.txtTextLayers.Location = New System.Drawing.Point(5, 131)
        Me.txtTextLayers.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtTextLayers.Name = "txtTextLayers"
        Me.txtTextLayers.Size = New System.Drawing.Size(114, 20)
        Me.txtTextLayers.TabIndex = 2
        '
        'lblTextLayers
        '
        Me.lblTextLayers.AutoSize = True
        Me.lblTextLayers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTextLayers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblTextLayers.Location = New System.Drawing.Point(5, 103)
        Me.lblTextLayers.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTextLayers.Name = "lblTextLayers"
        Me.lblTextLayers.Size = New System.Drawing.Size(41, 17)
        Me.lblTextLayers.TabIndex = 3
        Me.lblTextLayers.Text = "Слой:"
        '
        'txtSearchRadius
        '
        Me.txtSearchRadius.Location = New System.Drawing.Point(5, 198)
        Me.txtSearchRadius.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtSearchRadius.Name = "txtSearchRadius"
        Me.txtSearchRadius.Size = New System.Drawing.Size(114, 20)
        Me.txtSearchRadius.TabIndex = 4
        '
        'lblSearchRadius
        '
        Me.lblSearchRadius.AutoSize = True
        Me.lblSearchRadius.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSearchRadius.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblSearchRadius.Location = New System.Drawing.Point(5, 167)
        Me.lblSearchRadius.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSearchRadius.Name = "lblSearchRadius"
        Me.lblSearchRadius.Size = New System.Drawing.Size(55, 17)
        Me.lblSearchRadius.TabIndex = 5
        Me.lblSearchRadius.Text = "Обхват:"
        '
        'txtOutputCsv
        '
        Me.txtOutputCsv.Location = New System.Drawing.Point(5, 259)
        Me.txtOutputCsv.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtOutputCsv.Name = "txtOutputCsv"
        Me.txtOutputCsv.Size = New System.Drawing.Size(114, 20)
        Me.txtOutputCsv.TabIndex = 6
        '
        'lblOutputCsv
        '
        Me.lblOutputCsv.AutoSize = True
        Me.lblOutputCsv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOutputCsv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblOutputCsv.Location = New System.Drawing.Point(5, 230)
        Me.lblOutputCsv.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblOutputCsv.Name = "lblOutputCsv"
        Me.lblOutputCsv.Size = New System.Drawing.Size(35, 17)
        Me.lblOutputCsv.TabIndex = 7
        Me.lblOutputCsv.Text = "CSV:"
        '
        'chkUseTextLayerFilter
        '
        Me.chkUseTextLayerFilter.AutoSize = True
        Me.chkUseTextLayerFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.chkUseTextLayerFilter.Location = New System.Drawing.Point(155, 28)
        Me.chkUseTextLayerFilter.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkUseTextLayerFilter.Name = "chkUseTextLayerFilter"
        Me.chkUseTextLayerFilter.Size = New System.Drawing.Size(127, 19)
        Me.chkUseTextLayerFilter.TabIndex = 8
        Me.chkUseTextLayerFilter.Text = "Проверка слоеве"
        Me.chkUseTextLayerFilter.UseVisualStyleBackColor = True
        '
        'chkText
        '
        Me.chkText.AutoSize = True
        Me.chkText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.chkText.Location = New System.Drawing.Point(155, 64)
        Me.chkText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkText.Name = "chkText"
        Me.chkText.Size = New System.Drawing.Size(119, 19)
        Me.chkText.TabIndex = 9
        Me.chkText.Text = "Проверка текст"
        Me.chkText.UseVisualStyleBackColor = True
        '
        'chkMText
        '
        Me.chkMText.AutoSize = True
        Me.chkMText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.chkMText.Location = New System.Drawing.Point(155, 101)
        Me.chkMText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkMText.Name = "chkMText"
        Me.chkMText.Size = New System.Drawing.Size(157, 19)
        Me.chkMText.TabIndex = 10
        Me.chkMText.Text = "Проверка мулти текст"
        Me.chkMText.UseVisualStyleBackColor = True
        '
        'btnBrowseOutputCsv
        '
        Me.btnBrowseOutputCsv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnBrowseOutputCsv.Location = New System.Drawing.Point(744, 159)
        Me.btnBrowseOutputCsv.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnBrowseOutputCsv.Name = "btnBrowseOutputCsv"
        Me.btnBrowseOutputCsv.Size = New System.Drawing.Size(90, 37)
        Me.btnBrowseOutputCsv.TabIndex = 11
        Me.btnBrowseOutputCsv.Text = "Запиши като CSV"
        Me.btnBrowseOutputCsv.UseVisualStyleBackColor = True
        '
        'btnRun
        '
        Me.btnRun.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnRun.Location = New System.Drawing.Point(744, 28)
        Me.btnRun.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(90, 37)
        Me.btnRun.TabIndex = 12
        Me.btnRun.Text = "Старт"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnClose.Location = New System.Drawing.Point(744, 93)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(90, 37)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Затвори"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(5, 326)
        Me.txtLog.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.Size = New System.Drawing.Size(330, 128)
        Me.txtLog.TabIndex = 14
        '
        'lblLog
        '
        Me.lblLog.AutoSize = True
        Me.lblLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblLog.Location = New System.Drawing.Point(5, 294)
        Me.lblLog.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblLog.Name = "lblLog"
        Me.lblLog.Size = New System.Drawing.Size(37, 17)
        Me.lblLog.TabIndex = 15
        Me.lblLog.Text = "LOG:"
        '
        'btnSelectM
        '
        Me.btnSelectM.Location = New System.Drawing.Point(71, 26)
        Me.btnSelectM.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSelectM.Name = "btnSelectM"
        Me.btnSelectM.Size = New System.Drawing.Size(29, 19)
        Me.btnSelectM.TabIndex = 16
        Me.btnSelectM.Text = "M"
        Me.btnSelectM.UseVisualStyleBackColor = True
        '
        'grpPoints
        '
        Me.grpPoints.Controls.Add(Me.btnSelectM)
        Me.grpPoints.Controls.Add(Me.lblLog)
        Me.grpPoints.Controls.Add(Me.txtLog)
        Me.grpPoints.Controls.Add(Me.chkMText)
        Me.grpPoints.Controls.Add(Me.chkText)
        Me.grpPoints.Controls.Add(Me.chkUseTextLayerFilter)
        Me.grpPoints.Controls.Add(Me.lblOutputCsv)
        Me.grpPoints.Controls.Add(Me.txtOutputCsv)
        Me.grpPoints.Controls.Add(Me.lblSearchRadius)
        Me.grpPoints.Controls.Add(Me.txtSearchRadius)
        Me.grpPoints.Controls.Add(Me.lblTextLayers)
        Me.grpPoints.Controls.Add(Me.txtTextLayers)
        Me.grpPoints.Controls.Add(Me.lblBlockNames)
        Me.grpPoints.Controls.Add(Me.txtBlockNames)
        Me.grpPoints.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpPoints.Location = New System.Drawing.Point(24, 12)
        Me.grpPoints.Name = "grpPoints"
        Me.grpPoints.Size = New System.Drawing.Size(349, 459)
        Me.grpPoints.TabIndex = 17
        Me.grpPoints.TabStop = False
        Me.grpPoints.Text = "Точкови обекти"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.checkMtextLine)
        Me.GroupBox1.Controls.Add(Me.checkTextLine)
        Me.GroupBox1.Controls.Add(Me.checkLayLines)
        Me.GroupBox1.Controls.Add(Me.txtLogLines)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtOutputCsvLines)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtSearchRadiusLines)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtTextLayersLines)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtBoxLine)
        Me.GroupBox1.Controls.Add(Me.btnMSelectLine)
        Me.GroupBox1.Controls.Add(Me.lblLinePLine)
        Me.GroupBox1.Location = New System.Drawing.Point(390, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(349, 459)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Линейни обекти"
        '
        'checkMtextLine
        '
        Me.checkMtextLine.AutoSize = True
        Me.checkMtextLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.checkMtextLine.Location = New System.Drawing.Point(189, 99)
        Me.checkMtextLine.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.checkMtextLine.Name = "checkMtextLine"
        Me.checkMtextLine.Size = New System.Drawing.Size(157, 19)
        Me.checkMtextLine.TabIndex = 29
        Me.checkMtextLine.Text = "Проверка мулти текст"
        Me.checkMtextLine.UseVisualStyleBackColor = True
        '
        'checkTextLine
        '
        Me.checkTextLine.AutoSize = True
        Me.checkTextLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.checkTextLine.Location = New System.Drawing.Point(189, 64)
        Me.checkTextLine.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.checkTextLine.Name = "checkTextLine"
        Me.checkTextLine.Size = New System.Drawing.Size(119, 19)
        Me.checkTextLine.TabIndex = 28
        Me.checkTextLine.Text = "Проверка текст"
        Me.checkTextLine.UseVisualStyleBackColor = True
        '
        'checkLayLines
        '
        Me.checkLayLines.AutoSize = True
        Me.checkLayLines.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.checkLayLines.Location = New System.Drawing.Point(189, 27)
        Me.checkLayLines.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.checkLayLines.Name = "checkLayLines"
        Me.checkLayLines.Size = New System.Drawing.Size(127, 19)
        Me.checkLayLines.TabIndex = 27
        Me.checkLayLines.Text = "Проверка слоеве"
        Me.checkLayLines.UseVisualStyleBackColor = True
        '
        'txtLogLines
        '
        Me.txtLogLines.Location = New System.Drawing.Point(5, 326)
        Me.txtLogLines.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtLogLines.Multiline = True
        Me.txtLogLines.Name = "txtLogLines"
        Me.txtLogLines.Size = New System.Drawing.Size(280, 127)
        Me.txtLogLines.TabIndex = 26
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 294)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 17)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "LOG:"
        '
        'txtOutputCsvLines
        '
        Me.txtOutputCsvLines.Location = New System.Drawing.Point(5, 259)
        Me.txtOutputCsvLines.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtOutputCsvLines.Name = "txtOutputCsvLines"
        Me.txtOutputCsvLines.Size = New System.Drawing.Size(114, 20)
        Me.txtOutputCsvLines.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 230)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 17)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "CSV:"
        '
        'txtSearchRadiusLines
        '
        Me.txtSearchRadiusLines.Location = New System.Drawing.Point(5, 198)
        Me.txtSearchRadiusLines.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtSearchRadiusLines.Name = "txtSearchRadiusLines"
        Me.txtSearchRadiusLines.Size = New System.Drawing.Size(114, 20)
        Me.txtSearchRadiusLines.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 167)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 17)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Обхват:"
        '
        'txtTextLayersLines
        '
        Me.txtTextLayersLines.Location = New System.Drawing.Point(5, 131)
        Me.txtTextLayersLines.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtTextLayersLines.Name = "txtTextLayersLines"
        Me.txtTextLayersLines.Size = New System.Drawing.Size(114, 20)
        Me.txtTextLayersLines.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 101)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 17)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Слой:"
        '
        'txtBoxLine
        '
        Me.txtBoxLine.Location = New System.Drawing.Point(5, 63)
        Me.txtBoxLine.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtBoxLine.Name = "txtBoxLine"
        Me.txtBoxLine.Size = New System.Drawing.Size(114, 20)
        Me.txtBoxLine.TabIndex = 18
        '
        'btnMSelectLine
        '
        Me.btnMSelectLine.Location = New System.Drawing.Point(130, 26)
        Me.btnMSelectLine.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnMSelectLine.Name = "btnMSelectLine"
        Me.btnMSelectLine.Size = New System.Drawing.Size(29, 19)
        Me.btnMSelectLine.TabIndex = 17
        Me.btnMSelectLine.Text = "M"
        Me.btnMSelectLine.UseVisualStyleBackColor = True
        '
        'lblLinePLine
        '
        Me.lblLinePLine.AutoSize = True
        Me.lblLinePLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLinePLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblLinePLine.Location = New System.Drawing.Point(5, 29)
        Me.lblLinePLine.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblLinePLine.Name = "lblLinePLine"
        Me.lblLinePLine.Size = New System.Drawing.Size(102, 15)
        Me.lblLinePLine.TabIndex = 2
        Me.lblLinePLine.Text = "Линия/Полилиния"
        '
        'fSHARK
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(849, 491)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpPoints)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.btnBrowseOutputCsv)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "fSHARK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Извеждане на елементи в SHAPE"
        Me.grpPoints.ResumeLayout(False)
        Me.grpPoints.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents txtBlockNames As Windows.Forms.TextBox
    Friend WithEvents lblBlockNames As Windows.Forms.Label
    Friend WithEvents txtTextLayers As Windows.Forms.TextBox
    Friend WithEvents lblTextLayers As Windows.Forms.Label
    Friend WithEvents txtSearchRadius As Windows.Forms.TextBox
    Friend WithEvents lblSearchRadius As Windows.Forms.Label
    Friend WithEvents txtOutputCsv As Windows.Forms.TextBox
    Friend WithEvents lblOutputCsv As Windows.Forms.Label
    Friend WithEvents chkUseTextLayerFilter As Windows.Forms.CheckBox
    Friend WithEvents chkText As Windows.Forms.CheckBox
    Friend WithEvents chkMText As Windows.Forms.CheckBox
    Friend WithEvents btnBrowseOutputCsv As Windows.Forms.Button
    Friend WithEvents btnRun As Windows.Forms.Button
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents txtLog As Windows.Forms.TextBox
    Friend WithEvents lblLog As Windows.Forms.Label
    Friend WithEvents btnSelectM As Windows.Forms.Button
    Friend WithEvents grpPoints As Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents lblLinePLine As Windows.Forms.Label
    Friend WithEvents txtLogLines As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents txtOutputCsvLines As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents txtSearchRadiusLines As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtTextLayersLines As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtBoxLine As Windows.Forms.TextBox
    Friend WithEvents btnMSelectLine As Windows.Forms.Button
    Friend WithEvents checkMtextLine As Windows.Forms.CheckBox
    Friend WithEvents checkTextLine As Windows.Forms.CheckBox
    Friend WithEvents checkLayLines As Windows.Forms.CheckBox
End Class
