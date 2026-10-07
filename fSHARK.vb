''променена е логиката на извеждане на csv при линейни обекти, че взимаше само start X, ENd X, а не всички вертркси.

Imports System.IO
Imports System.Globalization
Imports System.Text
Imports System.Windows.Forms
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry

Public Class fSHARK

    Private Class TextItem
        Public Property Value As String
        Public Property Position As Point3d
        Public Property HandleText As String
        Public Property Layer As String
        Public Property TextType As String
        Public Property ObjectId As ObjectId
    End Class

    Private Class ManualPickResult
        Public Property SelectedText As TextItem
        Public Property SkipCurrent As Boolean
        Public Property CancelAll As Boolean
    End Class

    Private Class CurveInfo
        Public Property CurveId As ObjectId
        Public Property HandleText As String
        Public Property EntityType As String
        Public Property Layer As String
        Public Property StartPoint As Point3d
        Public Property EndPoint As Point3d
        Public Property Length As Double
        Public Property Extents As Extents3d
        Public Property VerticesText As String
        Public Property WktText As String
    End Class

    Private _cancelAll As Boolean = False

    Public Sub New()
        InitializeComponent()

        AddHandler Me.Load, AddressOf fSHARK_Load
        AddHandler btnBrowseOutputCsv.Click, AddressOf btnBrowseOutputCsv_Click
        AddHandler btnRun.Click, AddressOf btnRun_Click
        AddHandler btnClose.Click, AddressOf btnClose_Click
        AddHandler btnSelectM.Click, AddressOf btnSelectM_Click
        AddHandler btnMSelectLine.Click, AddressOf btnMSelectLine_Click
    End Sub

    Private Sub fSHARK_Load(sender As Object, e As EventArgs)
        Try
            txtBlockNames.Text = "SHAHTA"
            txtTextLayers.Text = "0"
            txtSearchRadius.Text = "2.00"

            chkUseTextLayerFilter.Checked = True
            chkText.Checked = True
            chkMText.Checked = True

            txtLog.Multiline = True
            txtLog.ScrollBars = ScrollBars.Vertical
            txtLog.ReadOnly = True

            txtBoxLine.Text = ""
            txtTextLayersLines.Text = "0"
            txtSearchRadiusLines.Text = "2.00"

            checkLayLines.Checked = True
            checkTextLine.Checked = True
            checkMtextLine.Checked = True

            txtLogLines.Multiline = True
            txtLogLines.ScrollBars = ScrollBars.Vertical
            txtLogLines.ReadOnly = True

            SuggestDefaultCsvPaths()

            LogMessage("Формата е заредена успешно.")
            LogMessage("Избери блок с бутона M.")
            LogMessageLines("Формата е заредена успешно.")
            LogMessageLines("Избери линия/полилиния с бутона M.")
        Catch ex As Exception
            MessageBox.Show("Грешка при зареждане на формата: " & ex.Message,
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SuggestDefaultCsvPaths()
        Try
            Dim doc As Autodesk.AutoCAD.ApplicationServices.Document =
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

            If doc Is Nothing Then Exit Sub
            If String.IsNullOrWhiteSpace(doc.Name) Then Exit Sub

            Dim dwgPath As String = doc.Name
            Dim folderPath As String = Path.GetDirectoryName(dwgPath)
            Dim fileNameNoExt As String = Path.GetFileNameWithoutExtension(dwgPath)

            If String.IsNullOrWhiteSpace(folderPath) Then Exit Sub

            txtOutputCsv.Text = Path.Combine(folderPath, fileNameNoExt & "_shahti.csv")
            txtOutputCsvLines.Text = Path.Combine(folderPath, fileNameNoExt & "_linii.csv")
        Catch
        End Try
    End Sub

    Private Sub btnBrowseOutputCsv_Click(sender As Object, e As EventArgs)
        Try
            Dim targetTextBox As TextBox = ResolveCsvTargetTextBox()

            Using sfd As New SaveFileDialog()
                sfd.Title = "Избор на CSV файл"
                sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
                sfd.DefaultExt = "csv"
                sfd.AddExtension = True

                If targetTextBox IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(targetTextBox.Text) Then
                    Try
                        Dim currentFolder As String = Path.GetDirectoryName(targetTextBox.Text)
                        Dim currentFile As String = Path.GetFileName(targetTextBox.Text)

                        If Directory.Exists(currentFolder) Then
                            sfd.InitialDirectory = currentFolder
                            sfd.FileName = currentFile
                        End If
                    Catch
                    End Try
                End If

                If sfd.ShowDialog() = DialogResult.OK Then
                    If targetTextBox Is txtOutputCsvLines Then
                        txtOutputCsvLines.Text = sfd.FileName
                        LogMessageLines("Избран CSV: " & sfd.FileName)
                    Else
                        txtOutputCsv.Text = sfd.FileName
                        LogMessage("Избран CSV: " & sfd.FileName)
                    End If
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Грешка при избор на CSV файл: " & ex.Message,
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ResolveCsvTargetTextBox() As TextBox
        If txtOutputCsvLines.Focused OrElse txtBoxLine.Focused OrElse txtTextLayersLines.Focused OrElse
           txtSearchRadiusLines.Focused OrElse txtLogLines.Focused Then
            Return txtOutputCsvLines
        End If

        If txtOutputCsv.Focused OrElse txtBlockNames.Focused OrElse txtTextLayers.Focused OrElse
           txtSearchRadius.Focused OrElse txtLog.Focused Then
            Return txtOutputCsv
        End If

        If Not String.IsNullOrWhiteSpace(txtOutputCsv.Text) AndAlso String.IsNullOrWhiteSpace(txtOutputCsvLines.Text) Then
            Return txtOutputCsvLines
        End If

        Return txtOutputCsv
    End Function

    Private Sub btnSelectM_Click(sender As Object, e As EventArgs)
        Try
            Dim doc As Autodesk.AutoCAD.ApplicationServices.Document =
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

            If doc Is Nothing Then
                MessageBox.Show("Няма активен чертеж.",
                                "Грешка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim ed As Autodesk.AutoCAD.EditorInput.Editor = doc.Editor

            Me.Hide()

            Dim peo As New PromptEntityOptions(vbLf & "Избери блок: ")
            peo.SetRejectMessage(vbLf & "Трябва да избереш блок.")
            peo.AddAllowedClass(GetType(BlockReference), False)

            Dim per As PromptEntityResult = ed.GetEntity(peo)

            Me.Show()
            Me.Activate()

            If per.Status <> PromptStatus.OK Then
                LogMessage("Няма избран блок.")
                Exit Sub
            End If

            Using tr As Transaction = doc.Database.TransactionManager.StartTransaction()

                Dim br As BlockReference =
                    TryCast(tr.GetObject(per.ObjectId, OpenMode.ForRead), BlockReference)

                If br Is Nothing Then
                    MessageBox.Show("Избраният обект не е блок.",
                                    "Грешка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim blockName As String = GetEffectiveBlockName(tr, br)
                Dim blockLayer As String = If(br.Layer, "")

                txtBlockNames.Text = blockName
                txtTextLayers.Text = blockLayer

                LogMessage("Избран блок: " & blockName)
                LogMessage("Слой на блока: " & blockLayer)

                tr.Commit()
            End Using

        Catch ex As Exception
            Me.Show()
            Me.Activate()

            MessageBox.Show("Грешка при избор на блок: " & ex.Message,
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMSelectLine_Click(sender As Object, e As EventArgs)
        Try
            Dim doc As Autodesk.AutoCAD.ApplicationServices.Document =
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

            If doc Is Nothing Then
                MessageBox.Show("Няма активен чертеж.",
                                "Грешка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim ed As Autodesk.AutoCAD.EditorInput.Editor = doc.Editor

            Me.Hide()

            Dim peo As New PromptEntityOptions(vbLf & "Избери LINE / POLYLINE: ")
            peo.SetRejectMessage(vbLf & "Трябва да избереш LINE или POLYLINE.")
            peo.AddAllowedClass(GetType(Line), False)
            peo.AddAllowedClass(GetType(Polyline), False)
            peo.AddAllowedClass(GetType(Polyline2d), False)
            peo.AddAllowedClass(GetType(Polyline3d), False)

            Dim per As PromptEntityResult = ed.GetEntity(peo)

            Me.Show()
            Me.Activate()

            If per.Status <> PromptStatus.OK Then
                LogMessageLines("Няма избрана линия/полилиния.")
                Exit Sub
            End If

            Using tr As Transaction = doc.Database.TransactionManager.StartTransaction()
                Dim ent As Entity = TryCast(tr.GetObject(per.ObjectId, OpenMode.ForRead), Entity)

                If ent Is Nothing Then
                    MessageBox.Show("Избраният обект не е валиден.",
                                    "Грешка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim lineType As String = ent.GetType().Name.ToUpperInvariant()
                Dim layerName As String = If(ent.Layer, "")

                txtBoxLine.Text = lineType
                txtTextLayersLines.Text = layerName

                LogMessageLines("Избран обект: " & lineType)
                LogMessageLines("Слой на обекта: " & layerName)

                tr.Commit()
            End Using

        Catch ex As Exception
            Me.Show()
            Me.Activate()

            MessageBox.Show("Грешка при избор на линия/полилиния: " & ex.Message,
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRun_Click(sender As Object, e As EventArgs)
        Try
            txtLog.Clear()
            txtLogLines.Clear()

            _cancelAll = False

            Dim runPoints As Boolean = Not String.IsNullOrWhiteSpace(txtBlockNames.Text)
            Dim runLines As Boolean = Not String.IsNullOrWhiteSpace(txtBoxLine.Text)

            If Not runPoints AndAlso Not runLines Then
                MessageBox.Show("Няма зададени настройки нито за точки, нито за линии.",
                                "Грешка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
                Exit Sub
            End If

            If runPoints Then
                ProcessPoints()
            End If

            If Not _cancelAll AndAlso runLines Then
                ProcessLines()
            End If

        Catch ex As Exception
            MessageBox.Show("Грешка при обработката: " & ex.Message,
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ProcessPoints()
        LogMessage("=== Старт точки ===")
        _cancelAll = False

        Dim blockNames As List(Of String) = ParseSemicolonList(txtBlockNames.Text)
        Dim blockLayers As List(Of String) = ParseSemicolonList(txtTextLayers.Text)

        Dim searchRadius As Double
        If Not Double.TryParse(txtSearchRadius.Text.Replace(",", "."),
                               NumberStyles.Any,
                               CultureInfo.InvariantCulture,
                               searchRadius) Then

            MessageBox.Show("Невалиден радиус за търсене при точковите обекти.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtSearchRadius.Focus()
            Exit Sub
        End If

        If searchRadius <= 0 Then
            MessageBox.Show("Радиусът при точковите обекти трябва да е по-голям от 0.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtSearchRadius.Focus()
            Exit Sub
        End If

        If blockNames.Count = 0 Then
            MessageBox.Show("Няма зададено име на блок.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtBlockNames.Focus()
            Exit Sub
        End If

        If chkUseTextLayerFilter.Checked AndAlso blockLayers.Count = 0 Then
            MessageBox.Show("Маркирана е проверка на слоеве, но няма зададен слой за точките.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtTextLayers.Focus()
            Exit Sub
        End If

        If Not chkText.Checked AndAlso Not chkMText.Checked Then
            MessageBox.Show("Трябва да е избран поне един тип текст за точковите обекти.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtOutputCsv.Text) Then
            MessageBox.Show("Избери изходен CSV файл за точковите обекти.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtOutputCsv.Focus()
            Exit Sub
        End If

        Dim doc As Autodesk.AutoCAD.ApplicationServices.Document =
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

        If doc Is Nothing Then
            MessageBox.Show("Няма активен чертеж.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim ed As Editor = doc.Editor

        LogMessage("Чертеж: " & doc.Name)
        LogMessage("Блокове: " & String.Join(", ", blockNames))
        LogMessage("Филтър по слой: " & If(chkUseTextLayerFilter.Checked, "Да", "Не"))

        If chkUseTextLayerFilter.Checked Then
            LogMessage("Слоеве: " & String.Join(", ", blockLayers))
        End If

        LogMessage("TEXT: " & If(chkText.Checked, "Да", "Не"))
        LogMessage("MTEXT: " & If(chkMText.Checked, "Да", "Не"))
        LogMessage("Радиус: " & searchRadius.ToString("0.###", CultureInfo.InvariantCulture))

        EnsureOutputFolder(txtOutputCsv.Text)

        Dim writtenCount As Integer = 0
        Dim blockCount As Integer = 0
        Dim textCount As Integer = 0

        Using docLock As DocumentLock = doc.LockDocument()
            Using tr As Transaction = doc.Database.TransactionManager.StartTransaction()

                Dim bt As BlockTable =
                    CType(tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead), BlockTable)

                Dim ms As BlockTableRecord =
                    CType(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForRead), BlockTableRecord)

                Dim allTexts As List(Of TextItem) = CollectTexts(ms, tr, chkText.Checked, chkMText.Checked)

                textCount = allTexts.Count
                LogMessage("Намерени текстове: " & textCount.ToString())

                Using sw As New StreamWriter(txtOutputCsv.Text, False, Encoding.UTF8)
                    sw.WriteLine("DWG_NAME,BLOCK_HANDLE,BLOCK_NAME,X,Y,Z,TEXT_VALUE,TEXT_HANDLE,TEXT_X,TEXT_Y,TEXT_TYPE,LAYER_BLOCK,LAYER_TEXT,DISTANCE,STATUS")

                    For Each entId As ObjectId In ms
                        Try
                            Dim ent As Entity = TryCast(tr.GetObject(entId, OpenMode.ForRead), Entity)
                            If ent Is Nothing Then Continue For
                            If Not TypeOf ent Is BlockReference Then Continue For

                            Dim br As BlockReference = CType(ent, BlockReference)
                            Dim effName As String = GetEffectiveBlockName(tr, br)

                            If String.IsNullOrWhiteSpace(effName) Then Continue For
                            If Not NameMatches(effName, blockNames) Then Continue For

                            Dim brLayer As String = If(br.Layer, "")
                            If chkUseTextLayerFilter.Checked AndAlso Not LayerMatches(brLayer, blockLayers) Then
                                Continue For
                            End If

                            blockCount += 1

                            Dim candidates As New List(Of TextItem)
                            For Each t As TextItem In allTexts
                                If t Is Nothing Then Continue For
                                Dim d As Double = Distance2D(br.Position, t.Position)
                                If d <= searchRadius Then
                                    candidates.Add(t)
                                End If
                            Next

                            Dim dwgName As String = If(Path.GetFileName(doc.Name), "")
                            Dim blockHandle As String = br.Handle.ToString()
                            Dim xVal As String = br.Position.X.ToString("0.###", CultureInfo.InvariantCulture)
                            Dim yVal As String = br.Position.Y.ToString("0.###", CultureInfo.InvariantCulture)
                            Dim zVal As String = br.Position.Z.ToString("0.###", CultureInfo.InvariantCulture)
                            Dim layerBlock As String = brLayer

                            If candidates.Count = 1 Then
                                Dim t As TextItem = candidates(0)
                                Dim d As Double = Distance2D(br.Position, t.Position)

                                sw.WriteLine(String.Join(",",
                                                         Csv(dwgName),
                                                         Csv(blockHandle),
                                                         Csv(effName),
                                                         Csv(xVal),
                                                         Csv(yVal),
                                                         Csv(zVal),
                                                         Csv(t.Value),
                                                         Csv(t.HandleText),
                                                         Csv(t.Position.X.ToString("0.###", CultureInfo.InvariantCulture)),
                                                         Csv(t.Position.Y.ToString("0.###", CultureInfo.InvariantCulture)),
                                                         Csv(t.TextType),
                                                         Csv(layerBlock),
                                                         Csv(t.Layer),
                                                         Csv(d.ToString("0.###", CultureInfo.InvariantCulture)),
                                                         Csv("OK_AUTO")))
                                writtenCount += 1
                            Else
                                Dim pickResult As ManualPickResult =
                                    ManualPickTextForBlock(doc, ed, tr, br, candidates, searchRadius)

                                If pickResult IsNot Nothing AndAlso pickResult.CancelAll Then
                                    LogMessage("Обработката е прекратена от потребителя.")
                                    _cancelAll = True
                                    Exit For
                                End If

                                If pickResult IsNot Nothing AndAlso pickResult.SelectedText IsNot Nothing Then
                                    Dim manualText As TextItem = pickResult.SelectedText
                                    Dim d As Double = Distance2D(br.Position, manualText.Position)

                                    sw.WriteLine(String.Join(",",
                                                             Csv(dwgName),
                                                             Csv(blockHandle),
                                                             Csv(effName),
                                                             Csv(xVal),
                                                             Csv(yVal),
                                                             Csv(zVal),
                                                             Csv(manualText.Value),
                                                             Csv(manualText.HandleText),
                                                             Csv(manualText.Position.X.ToString("0.###", CultureInfo.InvariantCulture)),
                                                             Csv(manualText.Position.Y.ToString("0.###", CultureInfo.InvariantCulture)),
                                                             Csv(manualText.TextType),
                                                             Csv(layerBlock),
                                                             Csv(manualText.Layer),
                                                             Csv(d.ToString("0.###", CultureInfo.InvariantCulture)),
                                                             Csv("OK_MANUAL")))
                                Else
                                    sw.WriteLine(String.Join(",",
                                                             Csv(dwgName),
                                                             Csv(blockHandle),
                                                             Csv(effName),
                                                             Csv(xVal),
                                                             Csv(yVal),
                                                             Csv(zVal),
                                                             Csv(""),
                                                             Csv(""),
                                                             Csv(""),
                                                             Csv(""),
                                                             Csv(""),
                                                             Csv(layerBlock),
                                                             Csv(""),
                                                             Csv(""),
                                                             Csv("MANUAL_SKIP")))
                                End If

                                writtenCount += 1
                            End If

                        Catch exBlock As Exception
                            LogMessage("Грешка при блок: " & exBlock.Message)
                        End Try

                        If _cancelAll Then Exit For
                    Next
                End Using

                tr.Commit()
            End Using
        End Using

        LogMessage("Намерени блокове: " & blockCount.ToString())
        LogMessage("Записани редове в CSV: " & writtenCount.ToString())

        If _cancelAll Then
            LogMessage("Процесът е прекратен.")
            MessageBox.Show("Обработката на точките беше прекратена. Записани редове: " & writtenCount.ToString(),
                            "Прекратено",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        Else
            LogMessage("Готово.")
            MessageBox.Show("Готово за точките. Записани редове: " & writtenCount.ToString(),
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ProcessLines()
        LogMessageLines("=== Старт линии ===")
        _cancelAll = False

        Dim lineTypes As List(Of String) = ParseSemicolonList(txtBoxLine.Text)
        Dim lineLayers As List(Of String) = ParseSemicolonList(txtTextLayersLines.Text)

        Dim searchRadius As Double
        If Not Double.TryParse(txtSearchRadiusLines.Text.Replace(",", "."),
                               NumberStyles.Any,
                               CultureInfo.InvariantCulture,
                               searchRadius) Then

            MessageBox.Show("Невалиден радиус за търсене при линейните обекти.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtSearchRadiusLines.Focus()
            Exit Sub
        End If

        If searchRadius <= 0 Then
            MessageBox.Show("Радиусът при линейните обекти трябва да е по-голям от 0.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtSearchRadiusLines.Focus()
            Exit Sub
        End If

        If lineTypes.Count = 0 Then
            MessageBox.Show("Няма зададен тип линия/полилиния.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtBoxLine.Focus()
            Exit Sub
        End If

        If checkLayLines.Checked AndAlso lineLayers.Count = 0 Then
            MessageBox.Show("Маркирана е проверка на слоеве, но няма зададен слой за линиите.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtTextLayersLines.Focus()
            Exit Sub
        End If

        If Not checkTextLine.Checked AndAlso Not checkMtextLine.Checked Then
            MessageBox.Show("Трябва да е избран поне един тип текст за линейните обекти.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtOutputCsvLines.Text) Then
            MessageBox.Show("Избери изходен CSV файл за линейните обекти.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            txtOutputCsvLines.Focus()
            Exit Sub
        End If

        Dim doc As Autodesk.AutoCAD.ApplicationServices.Document =
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

        If doc Is Nothing Then
            MessageBox.Show("Няма активен чертеж.",
                            "Грешка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim ed As Editor = doc.Editor

        LogMessageLines("Чертеж: " & doc.Name)
        LogMessageLines("Типове: " & String.Join(", ", lineTypes))
        LogMessageLines("Филтър по слой: " & If(checkLayLines.Checked, "Да", "Не"))

        If checkLayLines.Checked Then
            LogMessageLines("Слоеве: " & String.Join(", ", lineLayers))
        End If

        LogMessageLines("TEXT: " & If(checkTextLine.Checked, "Да", "Не"))
        LogMessageLines("MTEXT: " & If(checkMtextLine.Checked, "Да", "Не"))
        LogMessageLines("Радиус: " & searchRadius.ToString("0.###", CultureInfo.InvariantCulture))

        EnsureOutputFolder(txtOutputCsvLines.Text)

        Dim writtenCount As Integer = 0
        Dim lineCount As Integer = 0
        Dim textCount As Integer = 0

        Using docLock As DocumentLock = doc.LockDocument()
            Using tr As Transaction = doc.Database.TransactionManager.StartTransaction()

                Dim bt As BlockTable =
                    CType(tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead), BlockTable)

                Dim ms As BlockTableRecord =
                    CType(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForRead), BlockTableRecord)

                Dim allTexts As List(Of TextItem) = CollectTexts(ms, tr, checkTextLine.Checked, checkMtextLine.Checked)

                textCount = allTexts.Count
                LogMessageLines("Намерени текстове: " & textCount.ToString())

                Using sw As New StreamWriter(txtOutputCsvLines.Text, False, Encoding.UTF8)
                    sw.WriteLine("DWG_NAME,LINE_HANDLE,LINE_TYPE,START_X,START_Y,END_X,END_Y,LENGTH,VERTICES,WKT,TEXT_VALUE,TEXT_HANDLE,TEXT_X,TEXT_Y,TEXT_TYPE,LAYER_LINE,LAYER_TEXT,DISTANCE,STATUS")

                    For Each entId As ObjectId In ms
                        Try
                            Dim ent As Entity = TryCast(tr.GetObject(entId, OpenMode.ForRead), Entity)
                            If ent Is Nothing Then Continue For

                            If Not IsSupportedLineEntity(ent) Then Continue For

                            Dim typeName As String = ent.GetType().Name.ToUpperInvariant()
                            If Not NameMatches(typeName, lineTypes) Then Continue For

                            Dim entLayer As String = If(ent.Layer, "")
                            If checkLayLines.Checked AndAlso Not LayerMatches(entLayer, lineLayers) Then
                                Continue For
                            End If

                            Dim cInfo As CurveInfo = GetCurveInfo(ent, tr)
                            If cInfo Is Nothing Then Continue For

                            lineCount += 1

                            Dim candidates As New List(Of TextItem)

                            For Each t As TextItem In allTexts
                                If t Is Nothing Then Continue For

                                Dim d As Double = DistancePointToEntity2D(ent, t.Position)
                                If d <= searchRadius Then
                                    candidates.Add(t)
                                End If
                            Next

                            Dim dwgName As String = If(Path.GetFileName(doc.Name), "")
                            Dim lineHandle As String = cInfo.HandleText
                            Dim lineType As String = cInfo.EntityType
                            Dim sx As String = cInfo.StartPoint.X.ToString("0.###", CultureInfo.InvariantCulture)
                            Dim sy As String = cInfo.StartPoint.Y.ToString("0.###", CultureInfo.InvariantCulture)
                            Dim exP As String = cInfo.EndPoint.X.ToString("0.###", CultureInfo.InvariantCulture)
                            Dim eyP As String = cInfo.EndPoint.Y.ToString("0.###", CultureInfo.InvariantCulture)
                            Dim lenVal As String = cInfo.Length.ToString("0.###", CultureInfo.InvariantCulture)
                            Dim layerLine As String = cInfo.Layer

                            If candidates.Count = 1 Then
                                Dim t As TextItem = candidates(0)
                                Dim d As Double = DistancePointToEntity2D(ent, t.Position)

                                sw.WriteLine(String.Join(",",
                         Csv(dwgName),
                         Csv(lineHandle),
                         Csv(lineType),
                         Csv(sx),
                         Csv(sy),
                         Csv(exP),
                         Csv(eyP),
                         Csv(lenVal),
                         Csv(cInfo.VerticesText),
                         Csv(cInfo.WktText),
                         Csv(t.Value),
                         Csv(t.HandleText),
                         Csv(t.Position.X.ToString("0.###", CultureInfo.InvariantCulture)),
                         Csv(t.Position.Y.ToString("0.###", CultureInfo.InvariantCulture)),
                         Csv(t.TextType),
                         Csv(layerLine),
                         Csv(t.Layer),
                         Csv(d.ToString("0.###", CultureInfo.InvariantCulture)),
                         Csv("OK_AUTO")))
                                writtenCount += 1
                            Else
                                Dim pickResult As ManualPickResult =
                                    ManualPickTextForCurve(doc, ed, tr, ent, candidates, searchRadius)

                                If pickResult IsNot Nothing AndAlso pickResult.CancelAll Then
                                    LogMessageLines("Обработката е прекратена от потребителя.")
                                    _cancelAll = True
                                    Exit For
                                End If

                                If pickResult IsNot Nothing AndAlso pickResult.SelectedText IsNot Nothing Then
                                    Dim manualText As TextItem = pickResult.SelectedText
                                    Dim d As Double = DistancePointToEntity2D(ent, manualText.Position)

                                    sw.WriteLine(String.Join(",",
                         Csv(dwgName),
                         Csv(lineHandle),
                         Csv(lineType),
                         Csv(sx),
                         Csv(sy),
                         Csv(exP),
                         Csv(eyP),
                         Csv(lenVal),
                         Csv(cInfo.VerticesText),
                         Csv(cInfo.WktText),
                         Csv(manualText.Value),
                         Csv(manualText.HandleText),
                         Csv(manualText.Position.X.ToString("0.###", CultureInfo.InvariantCulture)),
                         Csv(manualText.Position.Y.ToString("0.###", CultureInfo.InvariantCulture)),
                         Csv(manualText.TextType),
                         Csv(layerLine),
                         Csv(manualText.Layer),
                         Csv(d.ToString("0.###", CultureInfo.InvariantCulture)),
                         Csv("OK_MANUAL")))
                                Else
                                    sw.WriteLine(String.Join(",",
                         Csv(dwgName),
                         Csv(lineHandle),
                         Csv(lineType),
                         Csv(sx),
                         Csv(sy),
                         Csv(exP),
                         Csv(eyP),
                         Csv(lenVal),
                         Csv(cInfo.VerticesText),
                         Csv(cInfo.WktText),
                         Csv(""),
                         Csv(""),
                         Csv(""),
                         Csv(""),
                         Csv(""),
                         Csv(layerLine),
                         Csv(""),
                         Csv(""),
                         Csv("MANUAL_SKIP")))
                                End If

                                writtenCount += 1
                            End If

                        Catch exLine As Exception
                            LogMessageLines("Грешка при линия/полилиния: " & exLine.Message)
                        End Try

                        If _cancelAll Then Exit For
                    Next
                End Using

                tr.Commit()
            End Using
        End Using

        LogMessageLines("Намерени линии/полилинии: " & lineCount.ToString())
        LogMessageLines("Записани редове в CSV: " & writtenCount.ToString())

        If _cancelAll Then
            LogMessageLines("Процесът е прекратен.")
            MessageBox.Show("Обработката на линиите беше прекратена. Записани редове: " & writtenCount.ToString(),
                            "Прекратено",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        Else
            LogMessageLines("Готово.")
            MessageBox.Show("Готово за линиите. Записани редове: " & writtenCount.ToString(),
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        End If
    End Sub

    Private Function CollectTexts(ms As BlockTableRecord,
                                  tr As Transaction,
                                  useText As Boolean,
                                  useMText As Boolean) As List(Of TextItem)

        Dim allTexts As New List(Of TextItem)

        For Each entId As ObjectId In ms
            Dim ent As Entity = TryCast(tr.GetObject(entId, OpenMode.ForRead), Entity)
            If ent Is Nothing Then Continue For

            If TypeOf ent Is DBText AndAlso useText Then
                Dim dbt As DBText = CType(ent, DBText)
                Dim val As String = If(dbt.TextString, "").Trim()

                If val <> "" Then
                    allTexts.Add(New TextItem With {
                        .Value = val,
                        .Position = dbt.Position,
                        .HandleText = dbt.Handle.ToString(),
                        .Layer = dbt.Layer,
                        .TextType = "TEXT",
                        .ObjectId = dbt.ObjectId
                    })
                End If

            ElseIf TypeOf ent Is MText AndAlso useMText Then
                Dim mt As MText = CType(ent, MText)
                Dim val As String = ""

                If mt.Contents IsNot Nothing Then
                    val = mt.Contents.Replace(vbCr, " ").Replace(vbLf, " ").Trim()
                End If

                If val <> "" Then
                    allTexts.Add(New TextItem With {
                        .Value = val,
                        .Position = mt.Location,
                        .HandleText = mt.Handle.ToString(),
                        .Layer = mt.Layer,
                        .TextType = "MTEXT",
                        .ObjectId = mt.ObjectId
                    })
                End If
            End If
        Next

        Return allTexts
    End Function

    Private Function ManualPickTextForBlock(doc As Autodesk.AutoCAD.ApplicationServices.Document,
                                            ed As Editor,
                                            tr As Transaction,
                                            br As BlockReference,
                                            candidates As List(Of TextItem),
                                            searchRadius As Double) As ManualPickResult
        Dim result As New ManualPickResult With {
            .SelectedText = Nothing,
            .SkipCurrent = False,
            .CancelAll = False
        }

        Try
            If candidates Is Nothing Then
                candidates = New List(Of TextItem)
            End If

            ZoomToBlockAndCandidates(doc, br.Position, candidates, searchRadius)
            System.Windows.Forms.Application.DoEvents()
            Threading.Thread.Sleep(200)

            LogMessage("--------------------------------------------------")
            LogMessage("Блок handle: " & br.Handle.ToString())

            If candidates.Count = 0 Then
                LogMessage("Няма намерен текст в радиуса.")
            Else
                LogMessage("Намерени кандидати: " & candidates.Count.ToString())
                For Each t As TextItem In candidates
                    If t Is Nothing Then Continue For
                    LogMessage("  - " & t.Value & " | " & t.TextType & " | слой: " & t.Layer)
                Next
            End If

            LogMessage("Избери ръчно TEXT/MTEXT.")
            LogMessage("Enter = пропуск на текущия обект, Esc = край на обработката.")

            Me.Hide()

            Dim peo As New PromptEntityOptions(vbLf & "Избери текст. Enter = пропуск, Esc = край: ")
            peo.SetRejectMessage(vbLf & "Трябва да избереш TEXT или MTEXT.")
            peo.AddAllowedClass(GetType(DBText), False)
            peo.AddAllowedClass(GetType(MText), False)
            peo.AllowNone = True

            Dim per As PromptEntityResult = ed.GetEntity(peo)

            Me.Show()
            Me.Activate()

            If per.Status = PromptStatus.None Then
                result.SkipCurrent = True
                LogMessage("Пропуснат текущ обект.")
                Return result
            End If

            If per.Status = PromptStatus.Cancel Then
                result.CancelAll = True
                LogMessage("Заявен край на обработката.")
                Return result
            End If

            If per.Status <> PromptStatus.OK Then
                result.SkipCurrent = True
                LogMessage("Пропуснат текущ обект.")
                Return result
            End If

            Dim ent As Entity = TryCast(tr.GetObject(per.ObjectId, OpenMode.ForRead), Entity)
            result.SelectedText = TextItemFromEntity(ent)
            If result.SelectedText Is Nothing Then
                result.SkipCurrent = True
            End If

            Return result

        Catch ex As Exception
            Me.Show()
            Me.Activate()
            LogMessage("Грешка при ръчен избор: " & ex.Message)
            result.SkipCurrent = True
            Return result
        End Try
    End Function

    Private Function ManualPickTextForCurve(doc As Autodesk.AutoCAD.ApplicationServices.Document,
                                            ed As Editor,
                                            tr As Transaction,
                                            curveEnt As Entity,
                                            candidates As List(Of TextItem),
                                            searchRadius As Double) As ManualPickResult
        Dim result As New ManualPickResult With {
            .SelectedText = Nothing,
            .SkipCurrent = False,
            .CancelAll = False
        }

        Try
            If candidates Is Nothing Then
                candidates = New List(Of TextItem)
            End If

            ZoomToCurveAndCandidates(doc, curveEnt, candidates, searchRadius)
            System.Windows.Forms.Application.DoEvents()
            Threading.Thread.Sleep(200)

            LogMessageLines("--------------------------------------------------")
            LogMessageLines("Линия handle: " & curveEnt.Handle.ToString())

            If candidates.Count = 0 Then
                LogMessageLines("Няма намерен текст в радиуса.")
            Else
                LogMessageLines("Намерени кандидати: " & candidates.Count.ToString())
                For Each t As TextItem In candidates
                    If t Is Nothing Then Continue For
                    LogMessageLines("  - " & t.Value & " | " & t.TextType & " | слой: " & t.Layer)
                Next
            End If

            LogMessageLines("Избери ръчно TEXT/MTEXT.")
            LogMessageLines("Enter = пропуск на текущия обект, Esc = край на обработката.")

            Me.Hide()

            Dim peo As New PromptEntityOptions(vbLf & "Избери текст. Enter = пропуск, Esc = край: ")
            peo.SetRejectMessage(vbLf & "Трябва да избереш TEXT или MTEXT.")
            peo.AddAllowedClass(GetType(DBText), False)
            peo.AddAllowedClass(GetType(MText), False)
            peo.AllowNone = True

            Dim per As PromptEntityResult = ed.GetEntity(peo)

            Me.Show()
            Me.Activate()

            If per.Status = PromptStatus.None Then
                result.SkipCurrent = True
                LogMessageLines("Пропуснат текущ обект.")
                Return result
            End If

            If per.Status = PromptStatus.Cancel Then
                result.CancelAll = True
                LogMessageLines("Заявен край на обработката.")
                Return result
            End If

            If per.Status <> PromptStatus.OK Then
                result.SkipCurrent = True
                LogMessageLines("Пропуснат текущ обект.")
                Return result
            End If

            Dim ent As Entity = TryCast(tr.GetObject(per.ObjectId, OpenMode.ForRead), Entity)
            result.SelectedText = TextItemFromEntity(ent)
            If result.SelectedText Is Nothing Then
                result.SkipCurrent = True
            End If

            Return result

        Catch ex As Exception
            Me.Show()
            Me.Activate()
            LogMessageLines("Грешка при ръчен избор: " & ex.Message)
            result.SkipCurrent = True
            Return result
        End Try
    End Function

    Private Function TextItemFromEntity(ent As Entity) As TextItem
        If ent Is Nothing Then Return Nothing

        If TypeOf ent Is DBText Then
            Dim dbt As DBText = CType(ent, DBText)
            Return New TextItem With {
                .Value = If(dbt.TextString, "").Trim(),
                .Position = dbt.Position,
                .HandleText = dbt.Handle.ToString(),
                .Layer = dbt.Layer,
                .TextType = "TEXT",
                .ObjectId = dbt.ObjectId
            }
        End If

        If TypeOf ent Is MText Then
            Dim mt As MText = CType(ent, MText)
            Dim val As String = ""
            If mt.Contents IsNot Nothing Then
                val = mt.Contents.Replace(vbCr, " ").Replace(vbLf, " ").Trim()
            End If

            Return New TextItem With {
                .Value = val,
                .Position = mt.Location,
                .HandleText = mt.Handle.ToString(),
                .Layer = mt.Layer,
                .TextType = "MTEXT",
                .ObjectId = mt.ObjectId
            }
        End If

        Return Nothing
    End Function

    Private Sub ZoomToBlockAndCandidates(doc As Autodesk.AutoCAD.ApplicationServices.Document,
                                         blockPos As Point3d,
                                         candidates As List(Of TextItem),
                                         searchRadius As Double)
        Try
            Dim minX As Double = blockPos.X
            Dim minY As Double = blockPos.Y
            Dim maxX As Double = blockPos.X
            Dim maxY As Double = blockPos.Y

            If candidates IsNot Nothing AndAlso candidates.Count > 0 Then
                For Each t As TextItem In candidates
                    If t Is Nothing Then Continue For

                    If t.Position.X < minX Then minX = t.Position.X
                    If t.Position.Y < minY Then minY = t.Position.Y
                    If t.Position.X > maxX Then maxX = t.Position.X
                    If t.Position.Y > maxY Then maxY = t.Position.Y
                Next
            Else
                minX = blockPos.X - searchRadius
                minY = blockPos.Y - searchRadius
                maxX = blockPos.X + searchRadius
                maxY = blockPos.Y + searchRadius
            End If

            ZoomWindow(doc, minX, minY, maxX, maxY, searchRadius, False)
        Catch ex As Exception
            LogMessage("Грешка при zoom: " & ex.Message)
        End Try
    End Sub

    Private Sub ZoomToCurveAndCandidates(doc As Autodesk.AutoCAD.ApplicationServices.Document,
                                         curveEnt As Entity,
                                         candidates As List(Of TextItem),
                                         searchRadius As Double)
        Try
            Dim minX As Double = 0.0
            Dim minY As Double = 0.0
            Dim maxX As Double = 0.0
            Dim maxY As Double = 0.0
            Dim hasExt As Boolean = False

            Try
                Dim ext As Extents3d = curveEnt.GeometricExtents
                minX = ext.MinPoint.X
                minY = ext.MinPoint.Y
                maxX = ext.MaxPoint.X
                maxY = ext.MaxPoint.Y
                hasExt = True
            Catch
            End Try

            If Not hasExt Then
                minX = -searchRadius
                minY = -searchRadius
                maxX = searchRadius
                maxY = searchRadius
            End If

            If candidates IsNot Nothing Then
                For Each t As TextItem In candidates
                    If t Is Nothing Then Continue For

                    If t.Position.X < minX Then minX = t.Position.X
                    If t.Position.Y < minY Then minY = t.Position.Y
                    If t.Position.X > maxX Then maxX = t.Position.X
                    If t.Position.Y > maxY Then maxY = t.Position.Y
                Next
            End If

            ZoomWindow(doc, minX, minY, maxX, maxY, searchRadius, True)
        Catch ex As Exception
            LogMessageLines("Грешка при zoom: " & ex.Message)
        End Try
    End Sub

    Private Sub ZoomWindow(doc As Autodesk.AutoCAD.ApplicationServices.Document,
                           minX As Double,
                           minY As Double,
                           maxX As Double,
                           maxY As Double,
                           searchRadius As Double,
                           isLineLog As Boolean)
        Dim dx As Double = maxX - minX
        Dim dy As Double = maxY - minY

        If dx < searchRadius Then dx = searchRadius
        If dy < searchRadius Then dy = searchRadius

        Dim marginX As Double = Math.Max(dx * 0.5, 5.0)
        Dim marginY As Double = Math.Max(dy * 0.5, 5.0)

        minX -= marginX
        minY -= marginY
        maxX += marginX
        maxY += marginY

        Dim p1(0 To 2) As Double
        Dim p2(0 To 2) As Double

        p1(0) = minX : p1(1) = minY : p1(2) = 0.0
        p2(0) = maxX : p2(1) = maxY : p2(2) = 0.0

        Dim acadApp As Object = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication
        acadApp.ZoomWindow(p1, p2)

        Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen()
        doc.Editor.Regen()
    End Sub

    Private Function GetEffectiveBlockName(tr As Transaction, br As BlockReference) As String
        Try
            If br Is Nothing Then Return ""

            If br.IsDynamicBlock AndAlso Not br.DynamicBlockTableRecord.IsNull Then
                Dim dynBtr As BlockTableRecord =
                    TryCast(tr.GetObject(br.DynamicBlockTableRecord, OpenMode.ForRead), BlockTableRecord)

                If dynBtr IsNot Nothing AndAlso dynBtr.Name IsNot Nothing Then
                    Return dynBtr.Name
                End If
            End If

            If Not br.BlockTableRecord.IsNull Then
                Dim btr As BlockTableRecord =
                    TryCast(tr.GetObject(br.BlockTableRecord, OpenMode.ForRead), BlockTableRecord)

                If btr IsNot Nothing AndAlso btr.Name IsNot Nothing Then
                    Return btr.Name
                End If
            End If

            Return ""
        Catch
            Return ""
        End Try
    End Function

    Private Function IsSupportedLineEntity(ent As Entity) As Boolean
        If ent Is Nothing Then Return False

        Return TypeOf ent Is Line OrElse
               TypeOf ent Is Polyline OrElse
               TypeOf ent Is Polyline2d OrElse
               TypeOf ent Is Polyline3d
    End Function

    Private Function GetCurveInfo(ent As Entity, tr As Transaction) As CurveInfo
        Try
            If ent Is Nothing Then Return Nothing

            Dim result As New CurveInfo()
            result.CurveId = ent.ObjectId
            result.HandleText = ent.Handle.ToString()
            result.EntityType = ent.GetType().Name.ToUpperInvariant()
            result.Layer = ent.Layer

            Dim pts As List(Of Point3d) = GetCurveVertices(ent, tr)

            If pts Is Nothing OrElse pts.Count < 2 Then
                Return Nothing
            End If

            result.StartPoint = pts(0)
            result.EndPoint = pts(pts.Count - 1)

            Dim totalLen As Double = 0.0
            For i As Integer = 0 To pts.Count - 2
                totalLen += pts(i).DistanceTo(pts(i + 1))
            Next
            result.Length = totalLen

            result.VerticesText = BuildVerticesText(pts)
            result.WktText = BuildLineStringWkt(pts)

            Try
                result.Extents = ent.GeometricExtents
            Catch
            End Try

            Return result
        Catch
            Return Nothing
        End Try
    End Function
    Private Function GetCurveVertices(ent As Entity, tr As Transaction) As List(Of Point3d)
        Dim pts As New List(Of Point3d)

        If TypeOf ent Is Line Then
            Dim ln As Line = CType(ent, Line)
            pts.Add(ln.StartPoint)
            pts.Add(ln.EndPoint)
            Return pts
        End If

        If TypeOf ent Is Polyline Then
            Dim pl As Polyline = CType(ent, Polyline)
            For i As Integer = 0 To pl.NumberOfVertices - 1
                pts.Add(pl.GetPoint3dAt(i))
            Next
            Return pts
        End If

        If TypeOf ent Is Polyline2d Then
            Dim pl2 As Polyline2d = CType(ent, Polyline2d)
            For Each vId As ObjectId In pl2
                Dim v2d As Vertex2d = TryCast(tr.GetObject(vId, OpenMode.ForRead), Vertex2d)
                If v2d IsNot Nothing Then
                    pts.Add(v2d.Position)
                End If
            Next
            Return pts
        End If

        If TypeOf ent Is Polyline3d Then
            Dim pl3 As Polyline3d = CType(ent, Polyline3d)
            For Each vId As ObjectId In pl3
                Dim v3d As PolylineVertex3d = TryCast(tr.GetObject(vId, OpenMode.ForRead), PolylineVertex3d)
                If v3d IsNot Nothing Then
                    pts.Add(v3d.Position)
                End If
            Next
            Return pts
        End If

        Return pts
    End Function

    Private Function BuildVerticesText(pts As List(Of Point3d)) As String
        Dim parts As New List(Of String)

        For Each pt As Point3d In pts
            parts.Add(pt.X.ToString("0.###", CultureInfo.InvariantCulture) &
                  " " &
                  pt.Y.ToString("0.###", CultureInfo.InvariantCulture))
        Next

        Return String.Join(" | ", parts)
    End Function

    Private Function BuildLineStringWkt(pts As List(Of Point3d)) As String
        Dim parts As New List(Of String)

        For Each pt As Point3d In pts
            parts.Add(pt.X.ToString("0.###", CultureInfo.InvariantCulture) &
                  " " &
                  pt.Y.ToString("0.###", CultureInfo.InvariantCulture))
        Next

        Return "LINESTRING (" & String.Join(", ", parts) & ")"
    End Function
    Private Function DistancePointToEntity2D(ent As Entity, pt As Point3d) As Double
        Try
            If ent Is Nothing Then Return Double.MaxValue
            If Not TypeOf ent Is Curve Then Return Double.MaxValue

            Dim c As Curve = CType(ent, Curve)
            Dim ptOnCurve As Point3d = c.GetClosestPointTo(pt, False)
            Return Distance2D(pt, ptOnCurve)
        Catch
            Return Double.MaxValue
        End Try
    End Function

    Private Function NameMatches(blockName As String, allowedNames As List(Of String)) As Boolean
        If String.IsNullOrWhiteSpace(blockName) Then Return False
        If allowedNames Is Nothing OrElse allowedNames.Count = 0 Then Return False

        For Each s As String In allowedNames
            If Not String.IsNullOrWhiteSpace(s) Then
                If String.Equals(blockName.Trim(), s.Trim(), StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function

    Private Function LayerMatches(layerName As String, allowedLayers As List(Of String)) As Boolean
        If String.IsNullOrWhiteSpace(layerName) Then Return False
        If allowedLayers Is Nothing OrElse allowedLayers.Count = 0 Then Return False

        For Each s As String In allowedLayers
            If Not String.IsNullOrWhiteSpace(s) Then
                If String.Equals(layerName.Trim(), s.Trim(), StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function

    Private Function Distance2D(p1 As Point3d, p2 As Point3d) As Double
        Dim dx As Double = p1.X - p2.X
        Dim dy As Double = p1.Y - p2.Y
        Return Math.Sqrt(dx * dx + dy * dy)
    End Function

    Private Function Csv(value As String) As String
        If value Is Nothing Then value = ""
        value = value.Replace("""", """""")
        Return """" & value & """"
    End Function

    Private Sub EnsureOutputFolder(csvPath As String)
        Dim folderPath As String = Path.GetDirectoryName(csvPath)

        If String.IsNullOrWhiteSpace(folderPath) Then Exit Sub

        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If
    End Sub

    Private Function ParseSemicolonList(input As String) As List(Of String)
        Dim result As New List(Of String)

        If String.IsNullOrWhiteSpace(input) Then
            Return result
        End If

        Dim parts() As String = input.Split(";"c)

        For Each part As String In parts
            Dim value As String = part.Trim()
            If value <> "" Then
                result.Add(value)
            End If
        Next

        Return result
    End Function

    Private Sub LogMessage(message As String)
        txtLog.AppendText(message & Environment.NewLine)
    End Sub

    Private Sub LogMessageLines(message As String)
        txtLogLines.AppendText(message & Environment.NewLine)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

End Class