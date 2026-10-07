''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''Програмката е за създаване на координатен регистър от полилинии в AutoCAD.
''Потребителят избира блок, атрибут, брой десетични знаци, мащаб и ротация. След това избира полилинии в
''чертежа и програмата поставя блокове на върховете им, като попълва атрибута с пореден номер. 
''Необходимо е блокът да има атрибут с текстов тип "номер"
''Създател Ивайло Великов, 2026
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.IO
Imports System.Globalization
Imports System.Windows.Forms
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry

Public Class fCVE

    Private Sub fCVE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadDecimalOptions()
            LoadBlockNames()
            txtBoxRotation.Text = "0"
            txtBoxStartNumber.Text = "1"
            txtBoxScale.Text = "0.2"
        Catch ex As Exception
            MessageBox.Show("Грешка при инициализация: " & ex.Message)
        End Try
    End Sub
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim doc As Autodesk.AutoCAD.ApplicationServices.Document
        Dim ed As Editor
        Dim peo As PromptEntityOptions
        Dim per As PromptEntityResult
        Dim br As BlockReference
        Dim btr As BlockTableRecord
        Dim blockName As String
        Dim foundIndex As Integer
        Dim i As Integer

        doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        If doc Is Nothing Then
            MessageBox.Show("Няма активен чертеж.")
            Exit Sub
        End If

        ed = doc.Editor

        Try
            Me.Hide()

            peo = New PromptEntityOptions(vbLf & "Изберете блок: ")
            peo.SetRejectMessage(vbLf & "Моля изберете само блок.")
            peo.AddAllowedClass(GetType(BlockReference), False)

            per = ed.GetEntity(peo)

            Me.Show()

            If per.Status <> PromptStatus.OK Then Exit Sub

            Using tr As Transaction = doc.Database.TransactionManager.StartTransaction()

                br = CType(tr.GetObject(per.ObjectId, OpenMode.ForRead), BlockReference)
                btr = CType(tr.GetObject(br.BlockTableRecord, OpenMode.ForRead), BlockTableRecord)
                blockName = btr.Name

                foundIndex = -1

                For i = 0 To ComBoxBlock.Items.Count - 1
                    If String.Compare(ComBoxBlock.Items(i).ToString(), blockName, True) = 0 Then
                        foundIndex = i
                        Exit For
                    End If
                Next

                If foundIndex >= 0 Then
                    ComBoxBlock.SelectedIndex = foundIndex
                Else
                    ComBoxBlock.Items.Add(blockName)
                    ComBoxBlock.SelectedIndex = ComBoxBlock.Items.Count - 1
                End If

                tr.Commit()
            End Using

            LoadAttributeTagsForSelectedBlock()

        Catch ex As Exception
            Me.Show()
            MessageBox.Show("Грешка при избор на блок: " & ex.Message)
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim sfd As SaveFileDialog

        sfd = New SaveFileDialog()

        Try
            sfd.Title = "Избор на текстов файл"
            sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            sfd.DefaultExt = "txt"
            sfd.AddExtension = True
            sfd.OverwritePrompt = True
            sfd.FileName = "points.txt"

            If sfd.ShowDialog() = DialogResult.OK Then
                txtBoxDirectory.Text = sfd.FileName
            End If
        Catch ex As Exception
            MessageBox.Show("Грешка при избор на файл: " & ex.Message)
        Finally
            sfd.Dispose()
        End Try
    End Sub

    Private Sub ComBoxBlock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComBoxBlock.SelectedIndexChanged
        Try
            LoadAttributeTagsForSelectedBlock()
        Catch ex As Exception
            MessageBox.Show("Грешка при зареждане на атрибутите: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSelecLines_Click(sender As Object, e As EventArgs) Handles btnSelecLines.Click
        Dim data As SettingsData
        Dim doc As Autodesk.AutoCAD.ApplicationServices.Document
        Dim ed As Editor
        Dim psr As PromptSelectionResult
        Dim pso As PromptSelectionOptions
        Dim filter As SelectionFilter
        Dim tvs() As TypedValue
        Dim selSet As SelectionSet
        Dim ids() As ObjectId
        Dim currentNumber As Integer
        Dim totalInserted As Integer

        data = GetSettings()
        If data Is Nothing Then Exit Sub

        doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        If doc Is Nothing Then
            MessageBox.Show("Няма активен чертеж.")
            Exit Sub
        End If

        ed = doc.Editor

        Try
            Me.Hide()

            tvs = New TypedValue() {
                New TypedValue(CInt(DxfCode.Start), "LWPOLYLINE,POLYLINE")
            }

            filter = New SelectionFilter(tvs)

            pso = New PromptSelectionOptions()
            pso.MessageForAdding = vbLf & "Изберете полилинии:"

            psr = ed.GetSelection(pso, filter)

            If psr.Status <> PromptStatus.OK Then
                Me.Show()
                Exit Sub
            End If

            selSet = psr.Value
            ids = selSet.GetObjectIds()

            currentNumber = data.StartNumber
            totalInserted = ProcessSelectedPolylines(ids, data, currentNumber)

            Me.Show()
            MessageBox.Show("Готово. Добавени точки: " & totalInserted.ToString())

        Catch ex As Exception
            Me.Show()
            MessageBox.Show("Грешка: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadDecimalOptions()
        ComBoxDecimal.Items.Clear()
        ComBoxDecimal.Items.Add("0")
        ComBoxDecimal.Items.Add("1")
        ComBoxDecimal.Items.Add("2")
        ComBoxDecimal.Items.Add("3")
        ComBoxDecimal.Items.Add("4")
        ComBoxDecimal.Items.Add("5")
        ComBoxDecimal.SelectedIndex = 2
    End Sub

    Private Sub LoadBlockNames()
        Dim doc As Autodesk.AutoCAD.ApplicationServices.Document
        Dim db As Database
        Dim tr As Transaction
        Dim bt As BlockTable
        Dim btrId As ObjectId
        Dim btr As BlockTableRecord
        Dim blockName As String

        ComBoxBlock.Items.Clear()
        CombBoxAttrib.Items.Clear()

        doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        If doc Is Nothing Then Exit Sub

        db = doc.Database
        tr = db.TransactionManager.StartTransaction()

        Try
            bt = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

            For Each btrId In bt
                btr = CType(tr.GetObject(btrId, OpenMode.ForRead), BlockTableRecord)
                blockName = btr.Name

                If Not btr.IsLayout AndAlso Not blockName.StartsWith("*") Then
                    ComBoxBlock.Items.Add(blockName)
                End If
            Next

            If ComBoxBlock.Items.Count > 0 Then
                ComBoxBlock.SelectedIndex = 0
            End If

            tr.Commit()
        Finally
            tr.Dispose()
        End Try
    End Sub

    Private Sub LoadAttributeTagsForSelectedBlock()
        Dim doc As Autodesk.AutoCAD.ApplicationServices.Document
        Dim db As Database
        Dim tr As Transaction
        Dim bt As BlockTable
        Dim btr As BlockTableRecord
        Dim entId As ObjectId
        Dim ent As Entity
        Dim attDef As AttributeDefinition
        Dim blockName As String

        CombBoxAttrib.Items.Clear()

        If ComBoxBlock.SelectedItem Is Nothing Then Exit Sub

        blockName = ComBoxBlock.SelectedItem.ToString()
        doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        If doc Is Nothing Then Exit Sub

        db = doc.Database
        tr = db.TransactionManager.StartTransaction()

        Try
            bt = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

            If Not bt.Has(blockName) Then
                tr.Commit()
                Exit Sub
            End If

            btr = CType(tr.GetObject(bt(blockName), OpenMode.ForRead), BlockTableRecord)

            For Each entId In btr
                ent = TryCast(tr.GetObject(entId, OpenMode.ForRead), Entity)

                If TypeOf ent Is AttributeDefinition Then
                    attDef = CType(ent, AttributeDefinition)
                    If Not attDef.Constant Then
                        CombBoxAttrib.Items.Add(attDef.Tag)
                    End If
                End If
            Next

            If CombBoxAttrib.Items.Count > 0 Then
                CombBoxAttrib.SelectedIndex = 0
            End If

            tr.Commit()
        Finally
            tr.Dispose()
        End Try
    End Sub

    Private Function GetSettings() As SettingsData
        Dim data As SettingsData
        Dim decimalCount As Integer
        Dim scaleValue As Double
        Dim rotationGon As Double
        Dim startNumber As Integer
        Dim outputPath As String

        data = New SettingsData()
        outputPath = txtBoxDirectory.Text.Trim()

        If String.IsNullOrWhiteSpace(outputPath) Then
            MessageBox.Show("Моля избери текстов файл.")
            Return Nothing
        End If

        If ComBoxBlock.SelectedItem Is Nothing Then
            MessageBox.Show("Моля избери блок.")
            Return Nothing
        End If

        If CombBoxAttrib.SelectedItem Is Nothing Then
            MessageBox.Show("Моля избери атрибут.")
            Return Nothing
        End If

        If ComBoxDecimal.SelectedItem Is Nothing Then
            MessageBox.Show("Моля избери брой десетични знаци.")
            Return Nothing
        End If

        If Not Integer.TryParse(ComBoxDecimal.SelectedItem.ToString(), decimalCount) Then
            MessageBox.Show("Невалиден брой десетични знаци.")
            Return Nothing
        End If

        If Not Double.TryParse(txtBoxScale.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, scaleValue) Then
            MessageBox.Show("Невалиден мащаб.")
            Return Nothing
        End If

        If scaleValue <= 0 Then
            MessageBox.Show("Мащабът трябва да е по-голям от 0.")
            Return Nothing
        End If

        If Not Double.TryParse(txtBoxRotation.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, rotationGon) Then
            MessageBox.Show("Невалидна ротация.")
            Return Nothing
        End If

        If Not Integer.TryParse(txtBoxStartNumber.Text.Trim(), startNumber) Then
            MessageBox.Show("Невалиден начален номер.")
            Return Nothing
        End If

        data.OutputPath = outputPath
        data.BlockName = ComBoxBlock.SelectedItem.ToString()
        data.AttributeTag = CombBoxAttrib.SelectedItem.ToString()
        data.DecimalCount = decimalCount
        data.ScaleValue = scaleValue
        data.RotationGon = rotationGon
        data.RotationRad = rotationGon * Math.PI / 200.0
        data.StartNumber = startNumber

        Return data
    End Function

    Private Function ProcessSelectedPolylines(ids() As ObjectId, data As SettingsData, ByRef currentNumber As Integer) As Integer
        Dim doc As Autodesk.AutoCAD.ApplicationServices.Document
        Dim db As Database
        Dim countInserted As Integer

        doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        db = doc.Database
        countInserted = 0

        Using docLock As Autodesk.AutoCAD.ApplicationServices.DocumentLock = doc.LockDocument()

            Using tr As Transaction = db.TransactionManager.StartTransaction()

                Dim bt As BlockTable = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

                If Not bt.Has(data.BlockName) Then
                    Throw New Exception("Блокът """ & data.BlockName & """ не е намерен.")
                End If

                Dim blockDef As BlockTableRecord = CType(tr.GetObject(bt(data.BlockName), OpenMode.ForRead), BlockTableRecord)
                Dim ms As BlockTableRecord = CType(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite), BlockTableRecord)

                Using sw As New StreamWriter(data.OutputPath, False, System.Text.Encoding.Default)

                    For Each id In ids
                        Dim obj As DBObject = tr.GetObject(id, OpenMode.ForRead)

                        If TypeOf obj Is Polyline Then
                            Dim pl As Polyline = CType(obj, Polyline)

                            For i As Integer = 0 To pl.NumberOfVertices - 1
                                Dim pt As Point3d = pl.GetPoint3dAt(i)
                                InsertBlockAtPoint(tr, ms, blockDef, pt, currentNumber, data)
                                WritePointToFile(sw, currentNumber, pt, data.DecimalCount)
                                currentNumber += 1
                                countInserted += 1
                            Next

                        ElseIf TypeOf obj Is Polyline2d Then
                            Dim pl2d As Polyline2d = CType(obj, Polyline2d)
                            Dim pts2d As List(Of Point3d) = GetPolyline2dVertices(tr, pl2d)

                            For Each pt In pts2d
                                InsertBlockAtPoint(tr, ms, blockDef, pt, currentNumber, data)
                                WritePointToFile(sw, currentNumber, pt, data.DecimalCount)
                                currentNumber += 1
                                countInserted += 1
                            Next
                        End If
                    Next

                End Using

                tr.Commit()
            End Using

        End Using

        Return countInserted
    End Function

    Private Function GetPolyline2dVertices(tr As Transaction, pl2d As Polyline2d) As List(Of Point3d)
        Dim pts As List(Of Point3d)
        Dim vId As ObjectId
        Dim v2d As Vertex2d

        pts = New List(Of Point3d)()

        For Each vId In pl2d
            v2d = CType(tr.GetObject(vId, OpenMode.ForRead), Vertex2d)
            pts.Add(v2d.Position)
        Next

        Return pts
    End Function

    Private Sub InsertBlockAtPoint(tr As Transaction, ownerSpace As BlockTableRecord, blockDef As BlockTableRecord, insPt As Point3d, pointNumber As Integer, data As SettingsData)
        Dim br As BlockReference
        Dim attId As ObjectId
        Dim attObj As DBObject
        Dim attDef As AttributeDefinition
        Dim attRef As AttributeReference

        br = New BlockReference(insPt, blockDef.ObjectId)
        br.ScaleFactors = New Scale3d(data.ScaleValue, data.ScaleValue, data.ScaleValue)
        br.Rotation = data.RotationRad

        ownerSpace.AppendEntity(br)
        tr.AddNewlyCreatedDBObject(br, True)

        If blockDef.HasAttributeDefinitions Then
            For Each attId In blockDef
                attObj = tr.GetObject(attId, OpenMode.ForRead)

                If TypeOf attObj Is AttributeDefinition Then
                    attDef = CType(attObj, AttributeDefinition)

                    If Not attDef.Constant Then
                        attRef = New AttributeReference()
                        attRef.SetAttributeFromBlock(attDef, br.BlockTransform)

                        If String.Compare(attDef.Tag, data.AttributeTag, True) = 0 Then
                            attRef.TextString = pointNumber.ToString()
                        Else
                            attRef.TextString = attDef.TextString
                        End If

                        br.AttributeCollection.AppendAttribute(attRef)
                        tr.AddNewlyCreatedDBObject(attRef, True)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub WritePointToFile(sw As StreamWriter, pointNumber As Integer, pt As Point3d, decimalCount As Integer)
        Dim fmt As String = "F" & decimalCount.ToString()

        ' Геодезично: първо Y (Northing), после X (Easting)
        sw.WriteLine(pointNumber.ToString() & " " &
                 pt.Y.ToString(fmt, CultureInfo.InvariantCulture) & " " &
                 pt.X.ToString(fmt, CultureInfo.InvariantCulture))
    End Sub


End Class

Public Class SettingsData
    Public OutputPath As String
    Public BlockName As String
    Public AttributeTag As String
    Public DecimalCount As Integer
    Public ScaleValue As Double
    Public RotationGon As Double
    Public RotationRad As Double
    Public StartNumber As Integer
End Class