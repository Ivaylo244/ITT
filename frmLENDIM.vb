Imports System.Globalization
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports AcColors = Autodesk.AutoCAD.Colors
Imports Drawing = System.Drawing

Public Class frmLENDIM

    Private Sub frmLENDIM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTextStyles()

        cmbboxbrznaci.Items.Clear()
        cmbboxbrznaci.Items.AddRange({"0", "1", "2", "3"})
        cmbboxbrznaci.SelectedItem = "2"

        txtBoxHeight.Text = "2.00"
        txtBoxWidth.Text = "1.00"
        txtboxoffsetX.Text = "0.00"
        txtboxoffsetY.Text = "2.00"

        txtboxprefix.Text = ""
        txtboxsufix.Text = ""

        DrawPreview()
    End Sub

    Private Sub PreviewChanged(sender As Object, e As EventArgs) _
        Handles txtBoxHeight.TextChanged,
                txtBoxWidth.TextChanged,
                txtboxoffsetX.TextChanged,
                txtboxoffsetY.TextChanged,
                txtboxprefix.TextChanged,
                txtboxsufix.TextChanged,
                cmbboxbrznaci.SelectedIndexChanged,
                cmbboxstyle.SelectedIndexChanged

        DrawPreview()
    End Sub

    Private Sub LoadTextStyles()
        cmbboxstyle.Items.Clear()

        Dim doc = Application.DocumentManager.MdiActiveDocument
        Dim db = doc.Database

        Using tr = db.TransactionManager.StartTransaction()
            Dim tst = CType(tr.GetObject(db.TextStyleTableId, OpenMode.ForRead), TextStyleTable)

            For Each id As ObjectId In tst
                Dim rec = CType(tr.GetObject(id, OpenMode.ForRead), TextStyleTableRecord)
                cmbboxstyle.Items.Add(rec.Name)
            Next

            tr.Commit()
        End Using

        If cmbboxstyle.Items.Count > 0 Then cmbboxstyle.SelectedIndex = 0
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Me.Hide()

        Try
            CreateLengthTexts()
        Catch ex As Exception
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(vbLf & "LENDIM грешка: " & ex.Message)
        End Try

        Me.Show()
    End Sub

    Private Sub CreateLengthTexts()
        Dim doc = Application.DocumentManager.MdiActiveDocument
        Dim db = doc.Database
        Dim ed = doc.Editor

        Dim textHeight As Double = ParseDouble(txtBoxHeight.Text, 2.0)
        Dim widthFactor As Double = ParseDouble(txtBoxWidth.Text, 1.0)
        Dim offsetX As Double = ParseDouble(txtboxoffsetX.Text, 0.0)
        Dim offsetY As Double = ParseDouble(txtboxoffsetY.Text, 2.0)
        Dim decimals As Integer = CInt(cmbboxbrznaci.SelectedItem.ToString())
        Dim styleName As String = cmbboxstyle.Text

        Dim prefix As String = txtboxprefix.Text
        Dim sufix As String = txtboxsufix.Text

        Dim pso As New PromptSelectionOptions()
        pso.MessageForAdding = vbLf & "Избери линии и полилинии: "

        Dim filterValues() As TypedValue = {
            New TypedValue(DxfCode.Start, "LINE,LWPOLYLINE")
        }

        Dim filter As New SelectionFilter(filterValues)
        Dim psr = ed.GetSelection(pso, filter)

        If psr.Status <> PromptStatus.OK Then Exit Sub

        Using docLock = doc.LockDocument()
            Using tr = db.TransactionManager.StartTransaction()

                Dim layerId = EnsureLayer(db, tr, "LENDIM")
                Dim textStyleId = GetTextStyleId(db, tr, styleName)

                Dim bt = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)
                Dim ms = CType(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite), BlockTableRecord)

                For Each selObj As SelectedObject In psr.Value
                    If selObj Is Nothing Then Continue For

                    Dim ent = TryCast(tr.GetObject(selObj.ObjectId, OpenMode.ForRead), Entity)

                    If TypeOf ent Is Line Then
                        Dim ln = CType(ent, Line)

                        AddTextOnSegment(db, ms, tr,
                                         ln.StartPoint,
                                         ln.EndPoint,
                                         ln.Length,
                                         textHeight,
                                         widthFactor,
                                         offsetX,
                                         offsetY,
                                         decimals,
                                         layerId,
                                         textStyleId,
                                         prefix,
                                         sufix)

                    ElseIf TypeOf ent Is Polyline Then
                        Dim pl = CType(ent, Polyline)

                        For i As Integer = 0 To pl.NumberOfVertices - 2
                            AddPolylineSegmentText(db, ms, tr, pl, i,
                                                   textHeight,
                                                   widthFactor,
                                                   offsetX,
                                                   offsetY,
                                                   decimals,
                                                   layerId,
                                                   textStyleId,
                                                   prefix,
                                                   sufix)
                        Next

                        If pl.Closed Then
                            AddPolylineSegmentText(db, ms, tr, pl, pl.NumberOfVertices - 1,
                                                   textHeight,
                                                   widthFactor,
                                                   offsetX,
                                                   offsetY,
                                                   decimals,
                                                   layerId,
                                                   textStyleId,
                                                   prefix,
                                                   sufix)
                        End If
                    End If
                Next

                tr.Commit()
            End Using
        End Using

        ed.WriteMessage(vbLf & "LENDIM: Готово.")
    End Sub

    Private Sub AddPolylineSegmentText(db As Database,
                                       ms As BlockTableRecord,
                                       tr As Transaction,
                                       pl As Polyline,
                                       index As Integer,
                                       textHeight As Double,
                                       widthFactor As Double,
                                       offsetX As Double,
                                       offsetY As Double,
                                       decimals As Integer,
                                       layerId As ObjectId,
                                       textStyleId As ObjectId,
                                       prefix As String,
                                       sufix As String)

        Dim nextIndex As Integer = If(index = pl.NumberOfVertices - 1, 0, index + 1)

        Dim p1 = pl.GetPoint3dAt(index)
        Dim p2 = pl.GetPoint3dAt(nextIndex)
        Dim bulge = pl.GetBulgeAt(index)

        Dim valueToWrite As Double
        Dim finalPrefix As String = prefix

        If Math.Abs(bulge) < 0.000000001 Then
            valueToWrite = p1.DistanceTo(p2)
        Else
            valueToWrite = GetArcSegmentRadius(p1, p2, bulge)
            finalPrefix = "R = "
        End If

        AddTextOnSegment(db, ms, tr,
                         p1,
                         p2,
                         valueToWrite,
                         textHeight,
                         widthFactor,
                         offsetX,
                         offsetY,
                         decimals,
                         layerId,
                         textStyleId,
                         finalPrefix,
                         sufix)
    End Sub

    Private Sub AddTextOnSegment(db As Database,
                                 ms As BlockTableRecord,
                                 tr As Transaction,
                                 p1 As Point3d,
                                 p2 As Point3d,
                                 lengthValue As Double,
                                 textHeight As Double,
                                 widthFactor As Double,
                                 offsetX As Double,
                                 offsetY As Double,
                                 decimals As Integer,
                                 layerId As ObjectId,
                                 textStyleId As ObjectId,
                                 prefix As String,
                                 sufix As String)

        Dim dx = p2.X - p1.X
        Dim dy = p2.Y - p1.Y

        If Math.Abs(dx) < 0.000000001 AndAlso Math.Abs(dy) < 0.000000001 Then Exit Sub

        Dim angle = Math.Atan2(dy, dx)

        Dim mid As New Point3d(
            (p1.X + p2.X) / 2.0,
            (p1.Y + p2.Y) / 2.0,
            (p1.Z + p2.Z) / 2.0
        )

        Dim ux = Math.Cos(angle)
        Dim uy = Math.Sin(angle)

        Dim px = -Math.Sin(angle)
        Dim py = Math.Cos(angle)

        Dim insertPoint As New Point3d(
            mid.X + ux * offsetX + px * offsetY,
            mid.Y + uy * offsetX + py * offsetY,
            mid.Z
        )

        If angle > Math.PI / 2.0 OrElse angle < -Math.PI / 2.0 Then
            angle += Math.PI
        End If

        Dim numberText As String = lengthValue.ToString("F" & decimals.ToString(), CultureInfo.InvariantCulture)
        Dim txtValue As String = prefix & numberText & sufix

        Dim txt As New DBText()
        txt.SetDatabaseDefaults(db)
        txt.TextString = txtValue
        txt.Height = textHeight
        txt.WidthFactor = widthFactor
        txt.Rotation = angle
        txt.LayerId = layerId
        txt.TextStyleId = textStyleId
        txt.HorizontalMode = TextHorizontalMode.TextCenter
        txt.VerticalMode = TextVerticalMode.TextVerticalMid
        txt.Position = insertPoint
        txt.AlignmentPoint = insertPoint

        ms.AppendEntity(txt)
        tr.AddNewlyCreatedDBObject(txt, True)

        txt.AdjustAlignment(db)
    End Sub

    Private Function EnsureLayer(db As Database, tr As Transaction, layerName As String) As ObjectId
        Dim lt = CType(tr.GetObject(db.LayerTableId, OpenMode.ForRead), LayerTable)

        If lt.Has(layerName) Then Return lt(layerName)

        lt.UpgradeOpen()

        Dim ltr As New LayerTableRecord()
        ltr.Name = layerName
        ltr.Color = AcColors.Color.FromColorIndex(AcColors.ColorMethod.ByAci, 1)

        Dim id = lt.Add(ltr)
        tr.AddNewlyCreatedDBObject(ltr, True)

        Return id
    End Function

    Private Function GetTextStyleId(db As Database, tr As Transaction, styleName As String) As ObjectId
        Dim tst = CType(tr.GetObject(db.TextStyleTableId, OpenMode.ForRead), TextStyleTable)

        If tst.Has(styleName) Then Return tst(styleName)

        Return db.Textstyle
    End Function

    Private Function GetArcSegmentLength(p1 As Point3d, p2 As Point3d, bulge As Double) As Double
        Dim chord = p1.DistanceTo(p2)
        Dim theta = 4.0 * Math.Atan(Math.Abs(bulge))
        Dim radius = chord / (2.0 * Math.Sin(theta / 2.0))

        Return radius * theta
    End Function

    Private Function GetArcSegmentRadius(p1 As Point3d, p2 As Point3d, bulge As Double) As Double
        Dim chord = p1.DistanceTo(p2)
        Dim theta = 4.0 * Math.Atan(Math.Abs(bulge))
        Dim radius = chord / (2.0 * Math.Sin(theta / 2.0))

        Return radius
    End Function

    Private Function ParseDouble(value As String, defaultValue As Double) As Double
        Dim result As Double

        If Double.TryParse(value.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, result) Then
            Return result
        End If

        Return defaultValue
    End Function

    Private Sub DrawPreview()
        If PictureBoxpreview Is Nothing Then Exit Sub
        If PictureBoxpreview.Width <= 0 OrElse PictureBoxpreview.Height <= 0 Then Exit Sub

        Dim bmp As New Drawing.Bitmap(PictureBoxpreview.Width, PictureBoxpreview.Height)

        Using g As Drawing.Graphics = Drawing.Graphics.FromImage(bmp)
            g.Clear(Drawing.Color.White)
            g.SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias

            Dim offsetY As Double = ParseDouble(txtboxoffsetY.Text, 2.0)

            Dim decimals As Integer = 2
            If cmbboxbrznaci.SelectedItem IsNot Nothing Then
                Integer.TryParse(cmbboxbrznaci.SelectedItem.ToString(), decimals)
            End If

            Dim yLine As Single = CSng(PictureBoxpreview.Height / 2 + 30)
            Dim x1 As Single = 30
            Dim x2 As Single = PictureBoxpreview.Width - 30

            g.DrawLine(Drawing.Pens.Black, x1, yLine, x2, yLine)

            Dim numberText As String = 20.0.ToString("F" & decimals.ToString(), CultureInfo.InvariantCulture)
            Dim valueText As String = txtboxprefix.Text & numberText & txtboxsufix.Text

            Using f As New Drawing.Font("Arial", 14, Drawing.FontStyle.Regular)
                Dim size = g.MeasureString(valueText, f)
                Dim scalePreview As Double = 8.0
                Dim textY As Single = CSng(yLine - offsetY * scalePreview - size.Height / 2)

                g.DrawString(valueText,
                             f,
                             Drawing.Brushes.Black,
                             CSng((PictureBoxpreview.Width - size.Width) / 2),
                             textY)
            End Using
        End Using

        If PictureBoxpreview.Image IsNot Nothing Then
            PictureBoxpreview.Image.Dispose()
        End If

        PictureBoxpreview.Image = bmp
    End Sub

End Class