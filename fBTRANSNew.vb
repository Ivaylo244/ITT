Imports System.Windows.Forms
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry

Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class fBTRANSvbNet

    Private selectedIds As New List(Of ObjectId)

    Private Sub fBTRANSvbNet_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cmbboxCSot.Items.Clear()
        cmbboxCSkum.Items.Clear()

        Dim csItems As String() = {
            "1950 зона 3 - меридиан 24",
            "1950 зона 3 - меридиан 27",
            "1950 зона 6 - меридиан 21",
            "1950 зона 6 - меридиан 27",
            "1970 зона 3",
            "1970 зона 5",
            "1970 зона 7",
            "1970 зона 9",
            "Софийска",
            "БГС2005 Кадастрална"
        }

        cmbboxCSot.Items.AddRange(csItems)
        cmbboxCSkum.Items.AddRange(csItems)

        cmbboxVSot.Items.Clear()
        cmbboxVSkum.Items.Clear()

        Dim vsItems As String() = {
            "EVRS2007",
            "Балтийска"
        }

        cmbboxVSot.Items.AddRange(vsItems)
        cmbboxVSkum.Items.AddRange(vsItems)

        cmbboxCSot.Text = "1950 зона 3 - меридиан 27"
        cmbboxCSkum.Text = "БГС2005 Кадастрална"

        cmbboxVSot.Text = "EVRS2007"
        cmbboxVSkum.Text = "EVRS2007"

    End Sub

    Private Sub btnChoose_Click(sender As Object, e As EventArgs) Handles btnChoose.Click

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor

        Me.Hide()

        Dim pso As New PromptSelectionOptions()
        pso.MessageForAdding = vbLf & "Избери блокове за трансформация: "
        pso.AllowDuplicates = False

        Dim filterValues As TypedValue() = {
            New TypedValue(DxfCode.Start, "INSERT")
        }

        Dim filter As New SelectionFilter(filterValues)
        Dim psr As PromptSelectionResult = ed.GetSelection(pso, filter)

        Me.Show()
        Me.Activate()

        If psr.Status <> PromptStatus.OK Then
            MessageBox.Show("Няма избрани блокове.", "Избор")
            Exit Sub
        End If

        selectedIds.Clear()

        For Each id As ObjectId In psr.Value.GetObjectIds()
            selectedIds.Add(id)
        Next

        MessageBox.Show("Избрани блокове: " & selectedIds.Count.ToString(), "Готово")

    End Sub

    Private Sub btnTransform_Click(sender As Object, e As EventArgs) Handles btnTransform.Click

        If selectedIds.Count = 0 Then
            MessageBox.Show("Първо избери блокове.", "Няма избор")
            Exit Sub
        End If

        Try
            Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
            Dim db As Database = doc.Database

            Dim moved As Integer = 0
            Dim skipped As Integer = 0

            Dim csFrom As String = cmbboxCSot.Text.Trim()
            Dim csTo As String = cmbboxCSkum.Text.Trim()

            Dim fromP As TransParams = GetTransParams(csFrom)
            Dim toP As TransParams = GetTransParams(csTo)

            VTransITT.EnsureInitialized()

            Using docLock As DocumentLock = doc.LockDocument()
                Using tr As Transaction = db.TransactionManager.StartTransaction()

                    For Each id As ObjectId In selectedIds

                        Dim br As BlockReference =
                            TryCast(tr.GetObject(id, OpenMode.ForWrite), BlockReference)

                        If br Is Nothing Then
                            skipped += 1
                            Continue For
                        End If

                        Dim oldPt As Point3d = br.Position

                        Dim newX As Double = 0
                        Dim newY As Double = 0
                        Dim newZ As Double = oldPt.Z

                        Dim res As Integer = VTransITT.TransformPoint(
                            fromP.Sys,
                            toP.Sys,
                            fromP.Type,
                            toP.Type,
                            fromP.Width,
                            toP.Width,
                            fromP.Zone,
                            toP.Zone,
                            oldPt.X,
                            oldPt.Y,
                            oldPt.Z,
                            newX,
                            newY,
                            newZ
                        )

                        If res <> 0 Then
                            skipped += 1
                            doc.Editor.WriteMessage(vbLf & "BTRANS error: " & res.ToString())
                            Continue For
                        End If

                        Dim newPt As New Point3d(newX, newY, oldPt.Z)
                        Dim displacement As Vector3d = newPt - oldPt

                        br.Position = newPt

                        For Each attId As ObjectId In br.AttributeCollection

                            Dim attRef As AttributeReference =
                                TryCast(tr.GetObject(attId, OpenMode.ForWrite), AttributeReference)

                            If attRef Is Nothing Then Continue For

                            attRef.Position = attRef.Position.TransformBy(
                                Matrix3d.Displacement(displacement)
                            )

                            If attRef.Justify <> AttachmentPoint.BaseLeft Then
                                attRef.AlignmentPoint = attRef.AlignmentPoint.TransformBy(
                                    Matrix3d.Displacement(displacement)
                                )
                            End If

                        Next

                        moved += 1

                    Next

                    tr.Commit()
                End Using
            End Using

            ''VTransITT.FreeTrans() вероятно крашва от това

            doc.Editor.Regen()

            MessageBox.Show(
                "Готово." & vbCrLf &
                "Трансформирани блокове: " & moved & vbCrLf &
                "Пропуснати: " & skipped,
                "BTRANS"
            )

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Грешка")
        End Try

    End Sub

    Private Structure TransParams
        Public Sys As Integer
        Public Type As Integer
        Public Width As Integer
        Public Zone As Integer
    End Structure

    Private Function GetTransParams(systemName As String) As TransParams

        Dim p As New TransParams()

        Select Case systemName

            Case "1950 зона 3 - меридиан 24"
                p.Sys = 1
                p.Type = 0
                p.Width = 0
                p.Zone = 24

            Case "1950 зона 3 - меридиан 27"
                p.Sys = 1
                p.Type = 0
                p.Width = 0
                p.Zone = 27

            Case "1950 зона 6 - меридиан 21"
                p.Sys = 1
                p.Type = 0
                p.Width = 1
                p.Zone = 21

            Case "1950 зона 6 - меридиан 27"
                p.Sys = 1
                p.Type = 0
                p.Width = 1
                p.Zone = 27

            Case "1970 зона 3"
                p.Sys = 2
                p.Type = 0
                p.Width = 0
                p.Zone = 3

            Case "1970 зона 5"
                p.Sys = 2
                p.Type = 0
                p.Width = 0
                p.Zone = 5

            Case "1970 зона 7"
                p.Sys = 2
                p.Type = 0
                p.Width = 0
                p.Zone = 7

            Case "1970 зона 9"
                p.Sys = 2
                p.Type = 0
                p.Width = 0
                p.Zone = 9

            Case "Софийска"
                p.Sys = 7
                p.Type = 0
                p.Width = 0
                p.Zone = -999

            Case "БГС2005 Кадастрална"
                p.Sys = 8
                p.Type = 4
                p.Width = -999
                p.Zone = -999

            Case Else
                Throw New Exception("Непозната координатна система: " & systemName)

        End Select

        Return p

    End Function

End Class