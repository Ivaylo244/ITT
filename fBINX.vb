Imports System.IO
Imports System.Text
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.ApplicationServices
Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class Form1

    Private Enum ColumnRole
        NoneRole = 0
        NOMER = 1
        X = 2
        Y = 3
        Z = 4
        KOTA = 5
        DESK = 6
    End Enum

    Private Class ImportRow
        Public Property SourceLineNumber As Integer
        Public Property RawLine As String
        Public Property RawValues As List(Of String)

        Public Property Nomer As String
        Public Property X As Double?
        Public Property Y As Double?
        Public Property Z As Double?
        Public Property Kota As Double?
        Public Property Desk As String
    End Class

    Private _parsedRows As New List(Of ImportRow)
    Private _previewLines As New List(Of String)
    Private _columnCombos As New List(Of ComboBox)
    Private _isUpdatingChecks As Boolean = False
    Private _settingsLoaded As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            My.Settings.Reload()

            PrepareUi()
            LoadBlocks()
            LoadLayers()

            txtboxRead1.Multiline = True
            txtboxRead1.ScrollBars = ScrollBars.Both
            txtboxRead1.WordWrap = False

            txtBoxRead2.Multiline = True
            txtBoxRead2.ScrollBars = ScrollBars.Both
            txtBoxRead2.WordWrap = False

            If String.IsNullOrWhiteSpace(txtBoxScale.Text) Then txtBoxScale.Text = "1"
            If String.IsNullOrWhiteSpace(txtBoxRotation.Text) Then txtBoxRotation.Text = "0"

            chkBox2D.Checked = True
            chkBox3D.Checked = False

            LoadUserSettings()

            _settingsLoaded = True
            WriteLog("Програмата е готова.")
        Catch ex As Exception
            MessageBox.Show("Грешка при зареждане на формата:" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            SaveUserSettings()
        Catch ex As Exception
            MessageBox.Show("Грешка при запис на настройките:" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub PrepareUi()
        _columnCombos.Clear()
        _columnCombos.Add(cmBoxk1)
        _columnCombos.Add(ComboBoxk2)
        _columnCombos.Add(ComboBoxk3)
        _columnCombos.Add(ComboBoxk4)
        _columnCombos.Add(ComboBoxk5)
        _columnCombos.Add(ComboBoxk6)
        _columnCombos.Add(ComboBoxk7)

        LoadColumnCombos()
        LoadStartLineOptions()
        LoadSeparatorOptions()
        LoadDecimalOptions()
    End Sub

    Private Sub LoadColumnCombos()
        For Each cbo As ComboBox In _columnCombos
            cbo.Items.Clear()
            cbo.Items.Add("")
            cbo.Items.Add("NOMER")
            cbo.Items.Add("X")
            cbo.Items.Add("Y")
            cbo.Items.Add("Z")
            cbo.Items.Add("KOTA")
            cbo.Items.Add("DESK")
            cbo.SelectedIndex = 0
            cbo.DropDownStyle = ComboBoxStyle.DropDownList
        Next
    End Sub

    Private Sub LoadStartLineOptions()
        cmbBoxLine.Items.Clear()
        cmbBoxLine.Items.Add("1")
        cmbBoxLine.Items.Add("2")
        cmbBoxLine.Items.Add("3")
        cmbBoxLine.SelectedIndex = 0
        cmbBoxLine.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub LoadSeparatorOptions()
        cmBoxRazdelitel.Items.Clear()
        cmBoxRazdelitel.Items.Add("SPACE")
        cmBoxRazdelitel.Items.Add("TAB")
        cmBoxRazdelitel.Items.Add(";")
        cmBoxRazdelitel.Items.Add(",")
        cmBoxRazdelitel.Items.Add("|")
        cmBoxRazdelitel.SelectedIndex = 0
        cmBoxRazdelitel.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub LoadDecimalOptions()
        cmBoxDesZnaci.Items.Clear()
        For i As Integer = 0 To 6
            cmBoxDesZnaci.Items.Add(i.ToString())
        Next
        cmBoxDesZnaci.SelectedIndex = 2
        cmBoxDesZnaci.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub LoadBlocks()
        cmbBoxBlock.Items.Clear()

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database

        Using tr As Transaction = db.TransactionManager.StartTransaction()
            Dim bt As BlockTable = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

            For Each btrId As ObjectId In bt
                Dim btr As BlockTableRecord = CType(tr.GetObject(btrId, OpenMode.ForRead), BlockTableRecord)

                If Not btr.IsAnonymous AndAlso Not btr.IsLayout Then
                    cmbBoxBlock.Items.Add(btr.Name)
                End If
            Next

            tr.Commit()
        End Using

        If cmbBoxBlock.Items.Count > 0 Then
            cmbBoxBlock.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadLayers()
        cmbBoxLay.Items.Clear()

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database

        Using tr As Transaction = db.TransactionManager.StartTransaction()
            Dim lt As LayerTable = CType(tr.GetObject(db.LayerTableId, OpenMode.ForRead), LayerTable)

            For Each layerId As ObjectId In lt
                Dim ltr As LayerTableRecord = CType(tr.GetObject(layerId, OpenMode.ForRead), LayerTableRecord)
                cmbBoxLay.Items.Add(ltr.Name)
            Next

            tr.Commit()
        End Using

        If cmbBoxLay.Items.Count > 0 Then
            cmbBoxLay.SelectedItem = "0"
            If cmbBoxLay.SelectedIndex < 0 Then cmbBoxLay.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadBlockAttributes(blockName As String)
        comboboxKota.Items.Clear()
        comboboxKota.Items.Add("")

        If String.IsNullOrWhiteSpace(blockName) Then
            comboboxKota.SelectedIndex = 0
            Exit Sub
        End If

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database

        Using tr As Transaction = db.TransactionManager.StartTransaction()
            Dim bt As BlockTable = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

            If Not bt.Has(blockName) Then
                tr.Commit()
                comboboxKota.SelectedIndex = 0
                Exit Sub
            End If

            Dim btr As BlockTableRecord = CType(tr.GetObject(bt(blockName), OpenMode.ForRead), BlockTableRecord)

            For Each entId As ObjectId In btr
                Dim obj As DBObject = tr.GetObject(entId, OpenMode.ForRead)
                If TypeOf obj Is AttributeDefinition Then
                    Dim ad As AttributeDefinition = CType(obj, AttributeDefinition)
                    If Not ad.Constant Then
                        comboboxKota.Items.Add(ad.Tag.ToUpperInvariant())
                    End If
                End If
            Next

            tr.Commit()
        End Using

        Dim kotaIndex As Integer = comboboxKota.Items.IndexOf("KOTA")
        If kotaIndex >= 0 Then
            comboboxKota.SelectedIndex = kotaIndex
        Else
            comboboxKota.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmbBoxBlock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBoxBlock.SelectedIndexChanged
        Try
            Dim blockName As String = GetSelectedText(cmbBoxBlock)
            LoadBlockAttributes(blockName)

            If _settingsLoaded Then
                SetComboIfExists(comboboxKota, My.Settings.LastKotaAttrib)
            End If
        Catch ex As Exception
            MessageBox.Show("Грешка при четене на атрибутите на блока:" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub chkBox2D_CheckedChanged(sender As Object, e As EventArgs) Handles chkBox2D.CheckedChanged
        If _isUpdatingChecks Then Exit Sub
        _isUpdatingChecks = True
        If chkBox2D.Checked Then chkBox3D.Checked = False
        If Not chkBox2D.Checked AndAlso Not chkBox3D.Checked Then chkBox2D.Checked = True
        _isUpdatingChecks = False
    End Sub

    Private Sub chkBox3D_CheckedChanged(sender As Object, e As EventArgs) Handles chkBox3D.CheckedChanged
        If _isUpdatingChecks Then Exit Sub
        _isUpdatingChecks = True
        If chkBox3D.Checked Then chkBox2D.Checked = False
        If Not chkBox2D.Checked AndAlso Not chkBox3D.Checked Then chkBox2D.Checked = True
        _isUpdatingChecks = False
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Using ofd As New OpenFileDialog()
            ofd.Title = "Избор на входен файл"
            ofd.Filter = "Text/CSV/KPT|*.txt;*.csv;*.kpt|Text files|*.txt|CSV files|*.csv|KPT files|*.kpt|All files|*.*"

            If File.Exists(txtBoxSearch.Text) Then
                Try
                    ofd.InitialDirectory = Path.GetDirectoryName(txtBoxSearch.Text)
                    ofd.FileName = Path.GetFileName(txtBoxSearch.Text)
                Catch
                End Try
            End If

            If ofd.ShowDialog() = DialogResult.OK Then
                txtBoxSearch.Text = ofd.FileName
            End If
        End Using
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            txtboxRead1.Clear()
            txtBoxRead2.Clear()
            _parsedRows.Clear()

            Dim lines As List(Of String) = ReadSourceLines(txtBoxSearch.Text)
            _previewLines = lines

            Dim sb As New StringBuilder()
            For i As Integer = 0 To Math.Min(lines.Count - 1, 200)
                sb.AppendLine(lines(i))
            Next

            txtboxRead1.Text = sb.ToString()

            SaveUserSettings()
            WriteLog("Прегледани редове: " & lines.Count.ToString())
        Catch ex As Exception
            MessageBox.Show("Грешка при показване на файла:" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub btnReadMe_Click(sender As Object, e As EventArgs) Handles btnReadMe.Click
        Try
            txtBoxRead2.Clear()
            _parsedRows.Clear()

            Dim lines As List(Of String) = ReadSourceLines(txtBoxSearch.Text)
            _previewLines = lines

            Dim startLine As Integer = GetStartLine()
            Dim separatorMode As String = GetSeparatorMode()

            If startLine > lines.Count Then
                Throw New Exception("Началният ред е по-голям от броя редове във файла.")
            End If

            Dim workLines As List(Of String) = lines.Skip(startLine - 1).ToList()
            Dim rows As List(Of ImportRow) = ParseRows(workLines, startLine, separatorMode)
            _parsedRows = rows

            ShowParsedPreview(rows)
            SaveUserSettings()
            WriteLog("Прочетени редове след началния ред: " & workLines.Count.ToString())
            WriteLog("Успешно разпознати редове: " & rows.Count.ToString())
        Catch ex As Exception
            MessageBox.Show("Грешка при прочитане на файла:" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Function ParseRows(lines As List(Of String), sourceStartLine As Integer, separatorMode As String) As List(Of ImportRow)
        Dim result As New List(Of ImportRow)

        For i As Integer = 0 To lines.Count - 1
            Dim rawLine As String = lines(i)

            If String.IsNullOrWhiteSpace(rawLine) Then Continue For

            Dim values As List(Of String) = SplitLine(rawLine, separatorMode)
            If values.Count = 0 Then Continue For

            Dim row As New ImportRow()
            row.SourceLineNumber = sourceStartLine + i
            row.RawLine = rawLine
            row.RawValues = values

            For colIndex As Integer = 0 To _columnCombos.Count - 1
                If colIndex >= values.Count Then Exit For

                Dim role As ColumnRole = GetComboRole(_columnCombos(colIndex))
                Dim textValue As String = values(colIndex).Trim()

                Select Case role
                    Case ColumnRole.NOMER
                        row.Nomer = textValue

                    Case ColumnRole.X
                        Dim d As Double
                        If TryParseDoubleFlexible(textValue, d) Then row.X = d

                    Case ColumnRole.Y
                        Dim d As Double
                        If TryParseDoubleFlexible(textValue, d) Then row.Y = d

                    Case ColumnRole.Z
                        Dim d As Double
                        If TryParseDoubleFlexible(textValue, d) Then row.Z = d

                    Case ColumnRole.KOTA
                        Dim d As Double
                        If TryParseDoubleFlexible(textValue, d) Then row.Kota = d

                    Case ColumnRole.DESK
                        row.Desk = textValue
                End Select
            Next

            If row.X.HasValue AndAlso row.Y.HasValue Then
                result.Add(row)
            End If
        Next

        Return result
    End Function

    Private Sub ShowParsedPreview(rows As List(Of ImportRow))

        dgvPreview.Columns.Clear()
        dgvPreview.Rows.Clear()

        dgvPreview.AllowUserToAddRows = False
        dgvPreview.ReadOnly = True
        dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgvPreview.AllowUserToResizeColumns = True
        dgvPreview.ScrollBars = ScrollBars.Both
        dgvPreview.RowHeadersWidth = 25

        For Each cbo As ComboBox In _columnCombos

            Dim header As String = GetSelectedText(cbo)

            If Not String.IsNullOrWhiteSpace(header) Then

                dgvPreview.Columns.Add(header, header)

                Dim col = dgvPreview.Columns(dgvPreview.Columns.Count - 1)

                Select Case header.ToUpper()

                    Case "NOMER"
                        col.Width = 70

                    Case "X", "Y"
                        col.Width = 130

                    Case "Z", "KOTA"
                        col.Width = 90

                    Case "DESK"
                        col.Width = 110

                    Case Else
                        col.Width = 90

                End Select

            End If

        Next

        Dim maxRows As Integer = Math.Min(rows.Count, 200)

        For i As Integer = 0 To maxRows - 1

            Dim r As ImportRow = rows(i)
            Dim values As New List(Of Object)

            For Each cbo As ComboBox In _columnCombos

                Select Case GetComboRole(cbo)

                    Case ColumnRole.NOMER
                        values.Add(NullToEmpty(r.Nomer))

                    Case ColumnRole.X
                        values.Add(If(r.X.HasValue,
                                      r.X.Value.ToString(CultureInfo.CurrentCulture),
                                      ""))

                    Case ColumnRole.Y
                        values.Add(If(r.Y.HasValue,
                                      r.Y.Value.ToString(CultureInfo.CurrentCulture),
                                      ""))

                    Case ColumnRole.Z
                        values.Add(If(r.Z.HasValue,
                                      r.Z.Value.ToString(CultureInfo.CurrentCulture),
                                      ""))

                    Case ColumnRole.KOTA
                        values.Add(If(r.Kota.HasValue,
                                      r.Kota.Value.ToString("F" & GetDecimalCount(),
                                      CultureInfo.CurrentCulture),
                                      ""))

                    Case ColumnRole.DESK
                        values.Add(NullToEmpty(r.Desk))

                End Select

            Next

            dgvPreview.Rows.Add(values.ToArray())

        Next

    End Sub

    Private Function SplitLine(line As String, separatorMode As String) As List(Of String)
        Dim values As New List(Of String)

        Select Case separatorMode.ToUpperInvariant()
            Case "SPACE"
                values = line.Split(New Char() {" "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries).ToList()

            Case "TAB"
                values = line.Split(New String() {ControlChars.Tab}, StringSplitOptions.None).Select(Function(x) x.Trim()).ToList()

            Case ";"
                values = line.Split(";"c).Select(Function(x) x.Trim()).ToList()

            Case ","
                values = line.Split(","c).Select(Function(x) x.Trim()).ToList()

            Case "|"
                values = line.Split("|"c).Select(Function(x) x.Trim()).ToList()

            Case Else
                values = line.Split(New Char() {" "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries).ToList()
        End Select

        Return values
    End Function

    Private Function ReadSourceLines(filePath As String) As List(Of String)
        If String.IsNullOrWhiteSpace(filePath) Then
            Throw New Exception("Не е избран файл.")
        End If

        If Not File.Exists(filePath) Then
            Throw New Exception("Файлът не съществува:" & Environment.NewLine & filePath)
        End If

        Try
            Return File.ReadAllLines(filePath, Encoding.UTF8).ToList()
        Catch
            Return File.ReadAllLines(filePath, Encoding.Default).ToList()
        End Try
    End Function

    Private Function GetStartLine() As Integer
        Dim n As Integer = 1
        Integer.TryParse(GetSelectedText(cmbBoxLine), n)
        If n < 1 Then n = 1
        If n > 3 Then n = 3
        Return n
    End Function

    Private Function GetSeparatorMode() As String
        Return GetSelectedText(cmBoxRazdelitel)
    End Function

    Private Function GetDecimalCount() As Integer
        Dim n As Integer = 2
        Integer.TryParse(GetSelectedText(cmBoxDesZnaci), n)
        If n < 0 Then n = 0
        If n > 10 Then n = 10
        Return n
    End Function

    Private Function GetComboRole(cbo As ComboBox) As ColumnRole
        Dim textValue As String = GetSelectedText(cbo).Trim().ToUpperInvariant()

        Select Case textValue
            Case "NOMER" : Return ColumnRole.NOMER
            Case "X" : Return ColumnRole.X
            Case "Y" : Return ColumnRole.Y
            Case "Z" : Return ColumnRole.Z
            Case "KOTA" : Return ColumnRole.KOTA
            Case "DESK" : Return ColumnRole.DESK
            Case Else : Return ColumnRole.NoneRole
        End Select
    End Function

    Private Function TryParseDoubleFlexible(text As String, ByRef value As Double) As Boolean
        value = 0
        If String.IsNullOrWhiteSpace(text) Then Return False

        Dim s As String = text.Trim()

        If Double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, value) Then Return True
        If Double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, value) Then Return True

        Dim sDot As String = s.Replace(","c, "."c)
        If Double.TryParse(sDot, NumberStyles.Any, CultureInfo.InvariantCulture, value) Then Return True

        Dim sComma As String = s.Replace("."c, ","c)
        If Double.TryParse(sComma, NumberStyles.Any, CultureInfo.CurrentCulture, value) Then Return True

        Return False
    End Function

    Private Function GetSelectedText(cbo As ComboBox) As String
        If cbo Is Nothing Then Return ""
        If cbo.SelectedItem Is Nothing Then Return ""
        Return cbo.SelectedItem.ToString()
    End Function

    Private Function NullToEmpty(text As String) As String
        If text Is Nothing Then Return ""
        Return text
    End Function

    Private Function CleanDeskBlockName(value As String) As String
        If value Is Nothing Then Return ""

        Return value.Trim() _
                    .Replace("""", "") _
                    .Replace("'", "") _
                    .Trim()
    End Function

    Private Sub WriteLog(message As String)
        If txtBoxRead2.TextLength > 0 Then
            txtBoxRead2.AppendText(Environment.NewLine)
        End If
        txtBoxRead2.AppendText(message)
    End Sub

    Private Sub SetComboIfExists(cbo As ComboBox, value As String)
        If cbo Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(value) Then Exit Sub

        For i As Integer = 0 To cbo.Items.Count - 1
            If String.Compare(cbo.Items(i).ToString(), value, True) = 0 Then
                cbo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub LoadUserSettings()
        txtBoxSearch.Text = My.Settings.LastFilePath
        txtBoxScale.Text = If(String.IsNullOrWhiteSpace(My.Settings.LastScale), "1", My.Settings.LastScale)
        txtBoxRotation.Text = If(String.IsNullOrWhiteSpace(My.Settings.LastRotation), "0", My.Settings.LastRotation)

        SetComboIfExists(cmbBoxBlock, My.Settings.LastBLock)

        If cmbBoxBlock.SelectedIndex >= 0 Then
            LoadBlockAttributes(GetSelectedText(cmbBoxBlock))
        End If

        SetComboIfExists(cmbBoxLay, My.Settings.LastLayer)
        SetComboIfExists(cmbBoxLine, My.Settings.LastStartLine)
        SetComboIfExists(cmBoxRazdelitel, My.Settings.LastSeparator)
        SetComboIfExists(cmBoxDesZnaci, My.Settings.LastDecimals)

        SetComboIfExists(cmBoxk1, My.Settings.LastK1)
        SetComboIfExists(ComboBoxk2, My.Settings.LastK2)
        SetComboIfExists(ComboBoxk3, My.Settings.LastK3)
        SetComboIfExists(ComboBoxk4, My.Settings.LastK4)
        SetComboIfExists(ComboBoxk5, My.Settings.LastK5)
        SetComboIfExists(ComboBoxk6, My.Settings.LastK6)
        SetComboIfExists(ComboBoxk7, My.Settings.LastK7)

        chkBox2D.Checked = My.Settings.LastMode2D
        chkBox3D.Checked = My.Settings.LastMode3D

        If Not chkBox2D.Checked AndAlso Not chkBox3D.Checked Then
            chkBox2D.Checked = True
            chkBox3D.Checked = False
        End If

        SetComboIfExists(comboboxKota, My.Settings.LastKotaAttrib)
    End Sub

    Private Sub SaveUserSettings()
        My.Settings.LastBLock = GetSelectedText(cmbBoxBlock)
        My.Settings.LastLayer = GetSelectedText(cmbBoxLay)
        My.Settings.LastScale = txtBoxScale.Text
        My.Settings.LastRotation = txtBoxRotation.Text
        My.Settings.LastMode2D = chkBox2D.Checked
        My.Settings.LastMode3D = chkBox3D.Checked
        My.Settings.LastFilePath = txtBoxSearch.Text
        My.Settings.LastStartLine = GetSelectedText(cmbBoxLine)
        My.Settings.LastSeparator = GetSelectedText(cmBoxRazdelitel)
        My.Settings.LastKotaAttrib = GetSelectedText(comboboxKota)
        My.Settings.LastDecimals = GetSelectedText(cmBoxDesZnaci)

        My.Settings.LastK1 = GetSelectedText(cmBoxk1)
        My.Settings.LastK2 = GetSelectedText(ComboBoxk2)
        My.Settings.LastK3 = GetSelectedText(ComboBoxk3)
        My.Settings.LastK4 = GetSelectedText(ComboBoxk4)
        My.Settings.LastK5 = GetSelectedText(ComboBoxk5)
        My.Settings.LastK6 = GetSelectedText(ComboBoxk6)
        My.Settings.LastK7 = GetSelectedText(ComboBoxk7)

        My.Settings.Save()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            ValidateBeforeImport()

            If _parsedRows Is Nothing OrElse _parsedRows.Count = 0 Then
                Throw New Exception("Няма прочетени данни. Натисни първо бутона за прочитане.")
            End If

            InsertBlocks(_parsedRows)
            SaveUserSettings()
        Catch ex As Exception
            MessageBox.Show("Грешка при импорт:" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub ValidateBeforeImport()

        If String.IsNullOrWhiteSpace(GetSelectedText(cmbBoxBlock)) Then
            Throw New Exception("Не е избран основен блок за точките.")
        End If

        If chkKrokirane.Checked Then
            Dim hasDesk As Boolean = _columnCombos.Any(Function(c) GetComboRole(c) = ColumnRole.DESK)

            If Not hasDesk Then
                Throw New Exception("При включено Крокиране трябва да има избрана колона DESK.")
            End If
        End If

        If String.IsNullOrWhiteSpace(GetSelectedText(cmbBoxLay)) Then
            Throw New Exception("Не е избран слой.")
        End If

        If Not chkBox2D.Checked AndAlso Not chkBox3D.Checked Then
            Throw New Exception("Избери 2D или 3D режим.")
        End If

        Dim scaleValue As Double
        If Not TryParseDoubleFlexible(txtBoxScale.Text, scaleValue) Then
            Throw New Exception("Невалиден мащаб.")
        End If

        Dim rotationValue As Double
        If Not TryParseDoubleFlexible(txtBoxRotation.Text, rotationValue) Then
            Throw New Exception("Невалидна ротация.")
        End If

        Dim hasX As Boolean = _columnCombos.Any(Function(c) GetComboRole(c) = ColumnRole.X)
        Dim hasY As Boolean = _columnCombos.Any(Function(c) GetComboRole(c) = ColumnRole.Y)

        If Not hasX OrElse Not hasY Then
            Throw New Exception("Трябва да има избрани колони X и Y.")
        End If

    End Sub

    Private Sub InsertBlocks(rows As List(Of ImportRow))

        Dim minX As Double = Double.MaxValue
        Dim minY As Double = Double.MaxValue
        Dim maxX As Double = Double.MinValue
        Dim maxY As Double = Double.MinValue
        Dim hasInsertedPoint As Boolean = False

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor

        Dim defaultBlockName As String = GetSelectedText(cmbBoxBlock)
        Dim targetLayer As String = GetSelectedText(cmbBoxLay)
        Dim kotaAttributeTag As String = GetSelectedText(comboboxKota).Trim().ToUpperInvariant()
        Dim kotaDecimals As Integer = GetDecimalCount()

        Dim defaultScale As Double
        Dim defaultRotation As Double

        If Not TryParseDoubleFlexible(txtBoxScale.Text, defaultScale) Then defaultScale = 1.0
        If Not TryParseDoubleFlexible(txtBoxRotation.Text, defaultRotation) Then defaultRotation = 0.0

        Dim pointBlockCount As Integer = 0
        Dim symbolBlockCount As Integer = 0
        Dim skippedCount As Integer = 0

        Dim sbLog As New StringBuilder()

        Using docLock As DocumentLock = doc.LockDocument()
            Using tr As Transaction = db.TransactionManager.StartTransaction()

                Dim bt As BlockTable = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)
                Dim ms As BlockTableRecord = CType(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite), BlockTableRecord)

                If Not bt.Has(defaultBlockName) Then
                    Throw New Exception("Основният блок за точките не е намерен в чертежа: " & defaultBlockName)
                End If

                For Each row As ImportRow In rows

                    Try

                        If Not row.X.HasValue OrElse Not row.Y.HasValue Then
                            skippedCount += 1
                            sbLog.AppendLine("Ред " & row.SourceLineNumber.ToString() & ": липсва X или Y.")
                            Continue For
                        End If

                        Dim zValue As Double = 0.0

                        If chkBox3D.Checked Then
                            If row.Z.HasValue Then
                                zValue = row.Z.Value
                            ElseIf row.Kota.HasValue Then
                                zValue = row.Kota.Value
                            Else
                                zValue = 0.0
                            End If
                        Else
                            zValue = 0.0
                        End If

                        Dim insPt As New Point3d(row.Y.Value, row.X.Value, zValue)

                        minX = Math.Min(minX, insPt.X)
                        minY = Math.Min(minY, insPt.Y)
                        maxX = Math.Max(maxX, insPt.X)
                        maxY = Math.Max(maxY, insPt.Y)
                        hasInsertedPoint = True

                        ' 1. Винаги вмъква основния блок за точката
                        InsertOneBlockWithAttributes(
                        tr,
                        ms,
                        bt,
                        defaultBlockName,
                        insPt,
                        targetLayer,
                        defaultScale,
                        defaultRotation,
                        row,
                        kotaAttributeTag,
                        kotaDecimals,
                        True)

                        pointBlockCount += 1

                        ' 2. Ако е включено Крокиране, вмъква и условния знак от DESK
                        If chkKrokirane.Checked Then

                            Dim deskBlockName As String = CleanDeskBlockName(row.Desk)

                            If String.IsNullOrWhiteSpace(deskBlockName) Then

                                sbLog.AppendLine("Ред " & row.SourceLineNumber.ToString() & ": липсва DESK за условен знак.")

                            ElseIf Not bt.Has(deskBlockName) Then

                                sbLog.AppendLine("Ред " & row.SourceLineNumber.ToString() & ": липсва блок за условен знак """ & deskBlockName & """ в чертежа.")

                            Else

                                InsertOneBlockWithAttributes(
                                tr,
                                ms,
                                bt,
                                deskBlockName,
                                insPt,
                                targetLayer,
                                defaultScale,
                                defaultRotation,
                                row,
                                kotaAttributeTag,
                                kotaDecimals,
                                False)

                                symbolBlockCount += 1

                            End If

                        End If

                    Catch exRow As Exception

                        skippedCount += 1
                        sbLog.AppendLine("Ред " & row.SourceLineNumber.ToString() & ": " & exRow.Message)

                    End Try

                Next

                tr.Commit()

            End Using
        End Using

        txtBoxRead2.Clear()

        txtBoxRead2.Text =
        "Импортът приключи." & Environment.NewLine &
        "Режим Крокиране: " & If(chkKrokirane.Checked, "ДА", "НЕ") & Environment.NewLine &
        "Вмъкнати основни блокове точки: " & pointBlockCount.ToString() & Environment.NewLine &
        "Вмъкнати условни знаци: " & symbolBlockCount.ToString() & Environment.NewLine &
        "Пропуснати редове: " & skippedCount.ToString()

        If sbLog.Length > 0 Then
            txtBoxRead2.AppendText(Environment.NewLine & Environment.NewLine & "Подробности:" & Environment.NewLine)
            txtBoxRead2.AppendText(sbLog.ToString())
        End If

        ed.WriteMessage(Environment.NewLine & "BINX: точки = " & pointBlockCount.ToString() & ", условни знаци = " & symbolBlockCount.ToString())

        If hasInsertedPoint Then
            ZoomToInsertedPoints(minX, minY, maxX, maxY)
        End If

    End Sub

    Private Sub InsertOneBlockWithAttributes(
    tr As Transaction,
    ms As BlockTableRecord,
    bt As BlockTable,
    blockName As String,
    insPt As Point3d,
    targetLayer As String,
    scaleValue As Double,
    rotationValue As Double,
    row As ImportRow,
    kotaAttributeTag As String,
    kotaDecimals As Integer,
    fillAttributes As Boolean)

        Dim blockDef As BlockTableRecord =
        CType(tr.GetObject(bt(blockName), OpenMode.ForRead), BlockTableRecord)

        Dim br As New BlockReference(insPt, blockDef.ObjectId)

        br.Layer = targetLayer
        br.ScaleFactors = New Scale3d(scaleValue)
        br.Rotation = rotationValue

        ms.AppendEntity(br)
        tr.AddNewlyCreatedDBObject(br, True)

        If blockDef.HasAttributeDefinitions Then

            For Each id As ObjectId In blockDef

                Dim obj As DBObject = tr.GetObject(id, OpenMode.ForRead)

                If TypeOf obj Is AttributeDefinition Then

                    Dim ad As AttributeDefinition = CType(obj, AttributeDefinition)

                    If ad.Constant Then Continue For

                    Dim ar As New AttributeReference()
                    ar.SetAttributeFromBlock(ad, br.BlockTransform)

                    If fillAttributes Then

                        Dim tag As String = ad.Tag.Trim().ToUpperInvariant()

                        If tag = "NOMER" Then

                            ar.TextString = NullToEmpty(row.Nomer)

                        ElseIf tag = "DESK" OrElse tag = "DESC" Then

                            ar.TextString = NullToEmpty(row.Desk)

                        ElseIf row.Kota.HasValue Then

                            If Not String.IsNullOrWhiteSpace(kotaAttributeTag) Then

                                If tag = kotaAttributeTag Then
                                    ar.TextString = row.Kota.Value.ToString("F" & kotaDecimals, CultureInfo.CurrentCulture)
                                End If

                            ElseIf tag = "KOTA" Then

                                ar.TextString = row.Kota.Value.ToString("F" & kotaDecimals, CultureInfo.CurrentCulture)

                            End If

                        End If

                    End If

                    br.AttributeCollection.AppendAttribute(ar)
                    tr.AddNewlyCreatedDBObject(ar, True)

                End If

            Next

        End If

    End Sub

    Private Sub ZoomToInsertedPoints(minX As Double, minY As Double, maxX As Double, maxY As Double)

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument

        Dim pad As Double = Math.Max(maxX - minX, maxY - minY) * 0.2
        If pad <= 0 Then pad = 10

        Dim p1 As String = (minX - pad).ToString(CultureInfo.InvariantCulture) & "," &
                           (minY - pad).ToString(CultureInfo.InvariantCulture)

        Dim p2 As String = (maxX + pad).ToString(CultureInfo.InvariantCulture) & "," &
                           (maxY + pad).ToString(CultureInfo.InvariantCulture)

        doc.SendStringToExecute("_.ZOOM _W " & p1 & " " & p2 & " ", True, False, False)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class