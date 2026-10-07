Imports System.Globalization
Imports System.Windows.Forms

Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput

Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class frmRATT

    Private Sub frmRATT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FillRoundCombo()
        FillBlockCombo()

    End Sub

    Private Sub FillRoundCombo()

        cmbbxround.Items.Clear()

        For i As Integer = 0 To 6
            cmbbxround.Items.Add(i.ToString())
        Next

        cmbbxround.SelectedIndex = 2

    End Sub

    Private Sub FillBlockCombo()

        cmbbxBL.Items.Clear()

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database

        Using tr As Transaction = db.TransactionManager.StartTransaction()

            Dim bt As BlockTable =
                CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

            For Each id As ObjectId In bt

                Dim btr As BlockTableRecord =
                    CType(tr.GetObject(id, OpenMode.ForRead), BlockTableRecord)

                If Not btr.IsLayout AndAlso Not btr.IsAnonymous Then
                    cmbbxBL.Items.Add(btr.Name)
                End If

            Next

            tr.Commit()

        End Using

        If cmbbxBL.Items.Count > 0 Then
            cmbbxBL.SelectedIndex = 0
        End If

    End Sub

    Private Sub cmbbxBL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbbxBL.SelectedIndexChanged

        FillAttrCombo()

    End Sub

    Private Sub FillAttrCombo()

        cmbbxattr.Items.Clear()

        If cmbbxBL.Text.Trim() = "" Then Exit Sub

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database

        Using tr As Transaction = db.TransactionManager.StartTransaction()

            Dim bt As BlockTable =
                CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

            If Not bt.Has(cmbbxBL.Text) Then Exit Sub

            Dim btr As BlockTableRecord =
                CType(tr.GetObject(bt(cmbbxBL.Text), OpenMode.ForRead), BlockTableRecord)

            For Each id As ObjectId In btr

                Dim obj As DBObject = tr.GetObject(id, OpenMode.ForRead)

                If TypeOf obj Is AttributeDefinition Then

                    Dim attDef As AttributeDefinition =
                        CType(obj, AttributeDefinition)

                    If Not attDef.Constant Then
                        cmbbxattr.Items.Add(attDef.Tag)
                    End If

                End If

            Next

            tr.Commit()

        End Using

        If cmbbxattr.Items.Count > 0 Then
            cmbbxattr.SelectedIndex = 0
        End If

    End Sub

    Private Sub btnMBl_Click(sender As Object, e As EventArgs) Handles btnMBl.Click

        Me.Hide()

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor
        Dim db As Database = doc.Database

        Dim peo As New PromptEntityOptions(vbLf & "Избери блок: ")
        peo.SetRejectMessage(vbLf & "Избраният обект не е блок.")
        peo.AddAllowedClass(GetType(BlockReference), True)

        Dim per As PromptEntityResult = ed.GetEntity(peo)

        If per.Status = PromptStatus.OK Then

            Using tr As Transaction = db.TransactionManager.StartTransaction()

                Dim br As BlockReference =
                    CType(tr.GetObject(per.ObjectId, OpenMode.ForRead), BlockReference)

                Dim btr As BlockTableRecord =
                    CType(tr.GetObject(br.DynamicBlockTableRecord, OpenMode.ForRead), BlockTableRecord)

                cmbbxBL.Text = btr.Name

                tr.Commit()

            End Using

            FillAttrCombo()

        End If

        Me.Show()
        Me.Activate()

    End Sub

    Private Sub btnRound_Click(sender As Object, e As EventArgs) Handles btnRound.Click

        If cmbbxBL.Text.Trim() = "" Then
            MessageBox.Show("Избери блок.")
            Exit Sub
        End If

        If cmbbxattr.Text.Trim() = "" Then
            MessageBox.Show("Избери атрибут.")
            Exit Sub
        End If

        If cmbbxround.Text.Trim() = "" Then
            MessageBox.Show("Избери точност.")
            Exit Sub
        End If

        Dim decimals As Integer

        If Not Integer.TryParse(cmbbxround.Text.Trim(), decimals) Then
            MessageBox.Show("Невалидна точност.")
            Exit Sub
        End If

        RoundAllAttributes(
            cmbbxBL.Text.Trim(),
            cmbbxattr.Text.Trim(),
            decimals
        )

    End Sub

    Private Sub RoundAllAttributes(blockName As String, attrTag As String, decimals As Integer)

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor

        Dim changedCount As Integer = 0
        Dim skippedCount As Integer = 0

        Using doc.LockDocument()

            Using tr As Transaction = db.TransactionManager.StartTransaction()

                Dim bt As BlockTable =
                    CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

                Dim ms As BlockTableRecord =
                    CType(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForRead), BlockTableRecord)

                For Each id As ObjectId In ms

                    Dim obj As DBObject = tr.GetObject(id, OpenMode.ForRead)

                    If TypeOf obj Is BlockReference Then

                        Dim br As BlockReference = CType(obj, BlockReference)

                        Dim brBtr As BlockTableRecord =
                            CType(tr.GetObject(br.DynamicBlockTableRecord, OpenMode.ForRead), BlockTableRecord)

                        If String.Equals(brBtr.Name, blockName, StringComparison.OrdinalIgnoreCase) Then

                            For Each attId As ObjectId In br.AttributeCollection

                                Dim attRef As AttributeReference =
                                    CType(tr.GetObject(attId, OpenMode.ForWrite), AttributeReference)

                                If String.Equals(attRef.Tag, attrTag, StringComparison.OrdinalIgnoreCase) Then

                                    Dim oldText As String = attRef.TextString.Trim()
                                    Dim value As Double

                                    If Double.TryParse(
                                        oldText,
                                        NumberStyles.Any,
                                        CultureInfo.InvariantCulture,
                                        value
                                    ) Then

                                        Dim newValue As Double = Math.Round(value, decimals)

                                        attRef.TextString =
                                            newValue.ToString(
                                                "F" & decimals,
                                                CultureInfo.InvariantCulture
                                            )

                                        changedCount += 1

                                    Else

                                        skippedCount += 1

                                    End If

                                End If

                            Next

                        End If

                    End If

                Next

                tr.Commit()

            End Using

        End Using

        ed.Regen()

        MessageBox.Show(
            "Готово!" & vbCrLf &
            "Закръглени атрибути: " & changedCount & vbCrLf &
            "Пропуснати нечислови стойности: " & skippedCount,
            "RATT"
        )

    End Sub

End Class