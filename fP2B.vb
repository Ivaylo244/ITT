''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Програмка за конвертиране на POINT / COGO POINT в блок
' v.1.1
' Разработил: Ивайло Великов
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System
Imports System.Collections.Generic
Imports System.Globalization

Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry

Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application
Imports CivilCogoPoint = Autodesk.Civil.DatabaseServices.CogoPoint


Public Class fP2B

    Private SelectedPointIds As New List(Of ObjectId)


    ' ============================================================
    ' LOAD
    ' ============================================================

    Private Sub fP2B_Load(
        sender As Object,
        e As EventArgs) Handles MyBase.Load

        txtBoxFoundPoints.ReadOnly = True
        txtBoxFoundBlocks.ReadOnly = True

        txtBoxFoundPoints.Text = "0"
        txtBoxFoundBlocks.Text = "0"

        rd3d.Checked = True

        LoadBlocks()
        UpdateAttributeControlState()

    End Sub


    ' ============================================================
    ' RADIO BUTTONS
    ' ============================================================

    Private Sub rd3d_CheckedChanged(
        sender As Object,
        e As EventArgs) Handles rd3d.CheckedChanged

        UpdateAttributeControlState()

    End Sub


    Private Sub rd2d_CheckedChanged(
        sender As Object,
        e As EventArgs) Handles rd2d.CheckedChanged

        UpdateAttributeControlState()

    End Sub


    Private Sub rd2d3d_CheckedChanged(
        sender As Object,
        e As EventArgs) Handles rd2d3d.CheckedChanged

        UpdateAttributeControlState()

    End Sub


    Private Sub UpdateAttributeControlState()

        If rd3d.Checked Then
            cmbbxselectattrib.Enabled = False
        Else
            cmbbxselectattrib.Enabled = True
        End If

    End Sub


    ' ============================================================
    ' ЗАРЕЖДАНЕ НА БЛОКОВЕТЕ
    ' ============================================================

    Private Sub LoadBlocks()

        cmbBoxSelectBlock.Items.Clear()

        Dim doc As Document =
            AcApp.DocumentManager.MdiActiveDocument

        If doc Is Nothing Then Return

        Dim db As Database = doc.Database


        Using tr As Transaction =
            db.TransactionManager.StartTransaction()

            Dim bt As BlockTable =
                CType(
                    tr.GetObject(
                        db.BlockTableId,
                        OpenMode.ForRead),
                    BlockTable)


            For Each id As ObjectId In bt

                Dim btr As BlockTableRecord =
                    TryCast(
                        tr.GetObject(
                            id,
                            OpenMode.ForRead),
                        BlockTableRecord)

                If btr Is Nothing Then Continue For
                If btr.IsLayout Then Continue For
                If btr.IsAnonymous Then Continue For

                cmbBoxSelectBlock.Items.Add(
                    btr.Name)

            Next

            tr.Commit()

        End Using


        txtBoxFoundBlocks.Text =
            cmbBoxSelectBlock.Items.Count.ToString()

    End Sub


    ' ============================================================
    ' ИЗБОР НА POINT / COGO POINT
    ' ============================================================

    Private Sub btnSelectPoints_Click(
        sender As Object,
        e As EventArgs) Handles btnSelectPoints.Click

        Dim doc As Document =
            AcApp.DocumentManager.MdiActiveDocument

        If doc Is Nothing Then Return

        Dim ed As Editor =
            doc.Editor


        Try

            Me.Hide()


            Dim pso As New PromptSelectionOptions()

            pso.MessageForAdding =
                vbLf & "Изберете POINT и/или COGO POINT обекти: "


            Dim psr As PromptSelectionResult =
                ed.GetSelection(pso)


            If psr.Status <> PromptStatus.OK Then

                SelectedPointIds.Clear()
                txtBoxFoundPoints.Text = "0"

                Return

            End If


            SelectedPointIds.Clear()


            Using tr As Transaction =
                doc.Database.TransactionManager.StartTransaction()


                For Each so As SelectedObject In psr.Value

                    If so Is Nothing Then Continue For


                    Dim obj As Autodesk.AutoCAD.DatabaseServices.DBObject =
                        tr.GetObject(
                            so.ObjectId,
                            OpenMode.ForRead)


                    If TypeOf obj Is DBPoint Then

                        SelectedPointIds.Add(
                            so.ObjectId)

                    ElseIf TypeOf obj Is CivilCogoPoint Then

                        SelectedPointIds.Add(
                            so.ObjectId)

                    End If

                Next


                tr.Commit()

            End Using


            txtBoxFoundPoints.Text =
                SelectedPointIds.Count.ToString()


            ed.WriteMessage(
                vbLf &
                "Избрани точки: " &
                SelectedPointIds.Count.ToString())


        Catch ex As Exception

            System.Windows.Forms.MessageBox.Show(
                ex.Message,
                "Грешка при избор на точки",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error)

        Finally

            Me.Show()
            Me.Activate()

        End Try

    End Sub


    ' ============================================================
    ' ИЗБОР НА БЛОК
    ' ============================================================

    Private Sub cmbBoxSelectBlock_SelectedIndexChanged(
        sender As Object,
        e As EventArgs) Handles cmbBoxSelectBlock.SelectedIndexChanged

        If cmbBoxSelectBlock.SelectedItem Is Nothing Then Return

        LoadBlockAttributes(
            cmbBoxSelectBlock.SelectedItem.ToString())

    End Sub


    ' ============================================================
    ' РЪЧЕН ИЗБОР НА БЛОК
    ' ============================================================

    Private Sub btnManualSelectBlock_Click(
        sender As Object,
        e As EventArgs) Handles btnManualSelectBlock.Click

        Dim doc As Document =
            AcApp.DocumentManager.MdiActiveDocument

        If doc Is Nothing Then Return

        Dim ed As Editor =
            doc.Editor


        Try

            Me.Hide()


            Dim peo As New PromptEntityOptions(
                vbLf & "Изберете блок: ")

            peo.SetRejectMessage(
                vbLf & "Трябва да изберете блок.")

            peo.AddAllowedClass(
                GetType(BlockReference),
                True)


            Dim per As PromptEntityResult =
                ed.GetEntity(peo)


            If per.Status <> PromptStatus.OK Then Return


            Dim blockName As String = ""


            Using tr As Transaction =
                doc.Database.TransactionManager.StartTransaction()


                Dim br As BlockReference =
                    TryCast(
                        tr.GetObject(
                            per.ObjectId,
                            OpenMode.ForRead),
                        BlockReference)

                If br Is Nothing Then Return


                Dim btrId As ObjectId


                If br.IsDynamicBlock Then

                    btrId =
                        br.DynamicBlockTableRecord

                Else

                    btrId =
                        br.BlockTableRecord

                End If


                Dim btr As BlockTableRecord =
                    CType(
                        tr.GetObject(
                            btrId,
                            OpenMode.ForRead),
                        BlockTableRecord)


                blockName =
                    btr.Name


                tr.Commit()

            End Using


            If cmbBoxSelectBlock.Items.Contains(
                blockName) Then

                cmbBoxSelectBlock.SelectedItem =
                    blockName

            Else

                cmbBoxSelectBlock.Items.Add(
                    blockName)

                cmbBoxSelectBlock.SelectedItem =
                    blockName

            End If


            LoadBlockAttributes(
                blockName)


        Catch ex As Exception

            System.Windows.Forms.MessageBox.Show(
                ex.Message,
                "Грешка при избор на блок",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error)

        Finally

            Me.Show()
            Me.Activate()

        End Try

    End Sub


    ' ============================================================
    ' АТРИБУТИ НА БЛОКА
    ' ============================================================

    Private Sub LoadBlockAttributes(
        blockName As String)

        cmbbxselectattrib.Items.Clear()

        cmbbxselectattrib.Items.Add(
            "(Не използвай)")

        cmbbxselectattrib.SelectedIndex = 0


        Dim doc As Document =
            AcApp.DocumentManager.MdiActiveDocument

        If doc Is Nothing Then Return


        Dim db As Database =
            doc.Database


        Using tr As Transaction =
            db.TransactionManager.StartTransaction()


            Dim bt As BlockTable =
                CType(
                    tr.GetObject(
                        db.BlockTableId,
                        OpenMode.ForRead),
                    BlockTable)


            If Not bt.Has(blockName) Then Return


            Dim btr As BlockTableRecord =
                CType(
                    tr.GetObject(
                        bt(blockName),
                        OpenMode.ForRead),
                    BlockTableRecord)


            For Each id As ObjectId In btr

                Dim ad As AttributeDefinition =
                    TryCast(
                        tr.GetObject(
                            id,
                            OpenMode.ForRead),
                        AttributeDefinition)

                If ad Is Nothing Then Continue For
                If ad.Constant Then Continue For

                cmbbxselectattrib.Items.Add(
                    ad.Tag)

            Next


            tr.Commit()

        End Using

    End Sub


    ' ============================================================
    ' ВЗИМА КООРДИНАТИТЕ
    ' ============================================================

    Private Function GetPointPosition(
        obj As Autodesk.AutoCAD.DatabaseServices.DBObject,
        ByRef position As Point3d) As Boolean


        ' --------------------------------------------------------
        ' ОБИКНОВЕН POINT
        ' --------------------------------------------------------

        Dim dbPt As DBPoint =
            TryCast(
                obj,
                DBPoint)

        If dbPt IsNot Nothing Then

            position =
                dbPt.Position

            Return True

        End If


        ' --------------------------------------------------------
        ' CIVIL COGO POINT
        ' --------------------------------------------------------

        Dim cogoPt As CivilCogoPoint =
            TryCast(
                obj,
                CivilCogoPoint)

        If cogoPt IsNot Nothing Then

            position =
                New Point3d(
                    cogoPt.Easting,
                    cogoPt.Northing,
                    cogoPt.Elevation)

            Return True

        End If


        Return False

    End Function


    ' ============================================================
    ' КОНВЕРТИРАНЕ
    ' ============================================================

    Private Sub btnConvert2_Click(
        sender As Object,
        e As EventArgs) Handles btnConvert2.Click


        If SelectedPointIds.Count = 0 Then

            System.Windows.Forms.MessageBox.Show(
                "Няма избрани POINT или COGO POINT обекти.",
                "P2B",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information)

            Return

        End If


        If String.IsNullOrWhiteSpace(
            cmbBoxSelectBlock.Text) Then

            System.Windows.Forms.MessageBox.Show(
                "Не е избран блок.",
                "P2B",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information)

            Return

        End If


        If rd2d.Checked OrElse rd2d3d.Checked Then

            If cmbbxselectattrib.SelectedItem Is Nothing OrElse
               cmbbxselectattrib.SelectedItem.ToString() =
               "(Не използвай)" Then

                System.Windows.Forms.MessageBox.Show(
                    "Изберете атрибут за котата.",
                    "P2B",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information)

                Return

            End If

        End If


        Dim doc As Document =
            AcApp.DocumentManager.MdiActiveDocument

        If doc Is Nothing Then Return


        Dim db As Database =
            doc.Database

        Dim ed As Editor =
            doc.Editor


        Dim converted As Integer = 0


        Try

            Using docLock As DocumentLock =
                doc.LockDocument()


                Using tr As Transaction =
                    db.TransactionManager.StartTransaction()


                    Dim bt As BlockTable =
                        CType(
                            tr.GetObject(
                                db.BlockTableId,
                                OpenMode.ForRead),
                            BlockTable)


                    Dim blockName As String =
                        cmbBoxSelectBlock.Text.Trim()


                    If Not bt.Has(blockName) Then

                        System.Windows.Forms.MessageBox.Show(
                            "Блокът не е намерен.",
                            "P2B",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Error)

                        Return

                    End If


                    Dim blockDef As BlockTableRecord =
                        CType(
                            tr.GetObject(
                                bt(blockName),
                                OpenMode.ForRead),
                            BlockTableRecord)


                    Dim currentSpace As BlockTableRecord =
                        CType(
                            tr.GetObject(
                                db.CurrentSpaceId,
                                OpenMode.ForWrite),
                            BlockTableRecord)


                    For Each pointId As ObjectId In SelectedPointIds


                        If pointId.IsNull Then Continue For
                        If pointId.IsErased Then Continue For


                        Dim obj As Autodesk.AutoCAD.DatabaseServices.DBObject =
                            tr.GetObject(
                                pointId,
                                OpenMode.ForRead)


                        Dim originalPosition As Point3d


                        If Not GetPointPosition(
                            obj,
                            originalPosition) Then

                            Continue For

                        End If


                        Dim insertPosition As Point3d


                        ' =================================================
                        ' 3D
                        ' =================================================

                        If rd3d.Checked Then

                            insertPosition =
                                New Point3d(
                                    originalPosition.X,
                                    originalPosition.Y,
                                    originalPosition.Z)


                            ' =================================================
                            ' 2D
                            ' =================================================

                        ElseIf rd2d.Checked Then

                            insertPosition =
                                New Point3d(
                                    originalPosition.X,
                                    originalPosition.Y,
                                    0.0)


                            ' =================================================
                            ' 2D + 3D
                            ' =================================================

                        Else

                            insertPosition =
                                New Point3d(
                                    originalPosition.X,
                                    originalPosition.Y,
                                    originalPosition.Z)

                        End If


                        ' =================================================
                        ' СЪЗДАВАНЕ НА БЛОК
                        ' =================================================

                        Dim br As New BlockReference(
                            insertPosition,
                            blockDef.ObjectId)


                        currentSpace.AppendEntity(
                            br)


                        tr.AddNewlyCreatedDBObject(
                            br,
                            True)


                        ' =================================================
                        ' АТРИБУТИ
                        ' =================================================

                        For Each entityId As ObjectId In blockDef


                            Dim ad As AttributeDefinition =
                                TryCast(
                                    tr.GetObject(
                                        entityId,
                                        OpenMode.ForRead),
                                    AttributeDefinition)


                            If ad Is Nothing Then Continue For
                            If ad.Constant Then Continue For


                            Dim ar As New AttributeReference()


                            ar.SetAttributeFromBlock(
                                ad,
                                br.BlockTransform)


                            ar.TextString =
                                ad.TextString


                            If rd2d.Checked OrElse
                               rd2d3d.Checked Then


                                If String.Equals(
                                    ad.Tag,
                                    cmbbxselectattrib.SelectedItem.ToString(),
                                    StringComparison.OrdinalIgnoreCase) Then


                                    ar.TextString =
                                        originalPosition.Z.ToString(
                                            "0.###",
                                            CultureInfo.InvariantCulture)

                                End If

                            End If


                            br.AttributeCollection.AppendAttribute(
                                ar)


                            tr.AddNewlyCreatedDBObject(
                                ar,
                                True)

                        Next


                        ' =================================================
                        ' ИЗТРИВАНЕ НА ОРИГИНАЛНИЯ ОБЕКТ
                        ' =================================================

                        If chkboxDeletePoints.Checked Then

                            obj.UpgradeOpen()

                            obj.Erase()

                        End If


                        converted += 1

                    Next


                    tr.Commit()

                End Using

            End Using


            ed.Regen()


            System.Windows.Forms.MessageBox.Show(
                "Конвертирани точки: " &
                converted.ToString(),
                "P2B",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information)


            If chkboxDeletePoints.Checked Then

                SelectedPointIds.Clear()

                txtBoxFoundPoints.Text = "0"

            End If


        Catch ex As Exception

            System.Windows.Forms.MessageBox.Show(
                ex.Message,
                "Грешка при конвертиране",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error)

        End Try

    End Sub

End Class