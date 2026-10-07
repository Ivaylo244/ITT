Imports System.Globalization

Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry

Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class CURB

    <CommandMethod("CURB")>
    Public Sub CURB_Command()

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor

        Try

            ' ---------------------------------------------------------
            ' 1. Избор на TEXT, MTEXT или BLOCK
            ' ---------------------------------------------------------
            Dim peo As New PromptEntityOptions(
                vbLf & "Избери текст или блок с кота: "
            )

            peo.SetRejectMessage(
                vbLf & "Трябва да избереш TEXT, MTEXT или BLOCK."
            )

            peo.AddAllowedClass(GetType(DBText), False)
            peo.AddAllowedClass(GetType(MText), False)
            peo.AddAllowedClass(GetType(BlockReference), False)

            Dim per As PromptEntityResult = ed.GetEntity(peo)

            If per.Status <> PromptStatus.OK Then
                Return
            End If

            ' ---------------------------------------------------------
            ' 2. Пита за височина на бордюра
            ' ---------------------------------------------------------
            Dim pdo As New PromptDoubleOptions(
                vbLf & "Колко е височината на бордюра? "
            )

            pdo.AllowNegative = True
            pdo.AllowZero = False

            Dim pdr As PromptDoubleResult = ed.GetDouble(pdo)

            If pdr.Status <> PromptStatus.OK Then
                Return
            End If

            Dim curbHeight As Double = pdr.Value

            Using tr As Transaction =
                db.TransactionManager.StartTransaction()

                Dim obj As Entity =
                    TryCast(
                        tr.GetObject(
                            per.ObjectId,
                            OpenMode.ForRead
                        ),
                        Entity
                    )

                If obj Is Nothing Then
                    Return
                End If

                ' Определяме колко нагоре да преместим новия обект.
                Dim moveDistance As Double =
                    GetMoveDistance(obj)

                Dim moveVector As New Vector3d(
                    0,
                    moveDistance,
                    0
                )

                ' -----------------------------------------------------
                ' TEXT
                ' -----------------------------------------------------
                If TypeOf obj Is DBText Then

                    Dim oldText As DBText =
                        DirectCast(obj, DBText)

                    Dim oldValue As Double

                    If Not TryReadNumber(
                        oldText.TextString,
                        oldValue
                    ) Then

                        ed.WriteMessage(
                            vbLf &
                            "Текстът не съдържа валидна кота."
                        )

                        Return

                    End If

                    Dim newValue As Double =
                        oldValue + curbHeight

                    Dim newText As DBText =
                        DirectCast(
                            oldText.Clone(),
                            DBText
                        )

                    newText.TextString =
                        FormatLikeOriginal(
                            newValue,
                            oldText.TextString
                        )

                    newText.TransformBy(
                        Matrix3d.Displacement(moveVector)
                    )

                    Dim btr As BlockTableRecord =
                        DirectCast(
                            tr.GetObject(
                                db.CurrentSpaceId,
                                OpenMode.ForWrite
                            ),
                            BlockTableRecord
                        )

                    btr.AppendEntity(newText)

                    tr.AddNewlyCreatedDBObject(
                        newText,
                        True
                    )

                    ' -----------------------------------------------------
                    ' MTEXT
                    ' -----------------------------------------------------
                ElseIf TypeOf obj Is MText Then

                    Dim oldMText As MText =
                        DirectCast(obj, MText)

                    Dim oldValue As Double

                    If Not TryReadNumber(
                        oldMText.Contents,
                        oldValue
                    ) Then

                        ed.WriteMessage(
                            vbLf &
                            "Текстът не съдържа валидна кота."
                        )

                        Return

                    End If

                    Dim newValue As Double =
                        oldValue + curbHeight

                    Dim newMText As MText =
                        DirectCast(
                            oldMText.Clone(),
                            MText
                        )

                    newMText.Contents =
                        FormatLikeOriginal(
                            newValue,
                            oldMText.Contents
                        )

                    newMText.TransformBy(
                        Matrix3d.Displacement(moveVector)
                    )

                    Dim btr As BlockTableRecord =
                        DirectCast(
                            tr.GetObject(
                                db.CurrentSpaceId,
                                OpenMode.ForWrite
                            ),
                            BlockTableRecord
                        )

                    btr.AppendEntity(newMText)

                    tr.AddNewlyCreatedDBObject(
                        newMText,
                        True
                    )

                    ' -----------------------------------------------------
                    ' BLOCK
                    ' -----------------------------------------------------
                ElseIf TypeOf obj Is BlockReference Then

                    Dim oldBlock As BlockReference =
                        DirectCast(obj, BlockReference)

                    CopyBlockWithNewKota(
                        oldBlock,
                        curbHeight,
                        moveVector,
                        db,
                        tr,
                        ed
                    )

                End If

                tr.Commit()

            End Using

        Catch ex As System.Exception

            ed.WriteMessage(
                vbLf &
                "Грешка в KOTUP: " &
                ex.Message
            )

        End Try

    End Sub


    ' =============================================================
    ' КОПИРАНЕ НА БЛОК
    ' =============================================================
    Private Sub CopyBlockWithNewKota(
        oldBlock As BlockReference,
        curbHeight As Double,
        moveVector As Vector3d,
        db As Database,
        tr As Transaction,
        ed As Editor
    )

        Dim oldKotaAttribute As AttributeReference = Nothing

        ' ---------------------------------------------------------
        ' Търсим атрибут KOTA
        ' ---------------------------------------------------------
        For Each attId As ObjectId In oldBlock.AttributeCollection

            Dim att As AttributeReference =
                TryCast(
                    tr.GetObject(
                        attId,
                        OpenMode.ForRead
                    ),
                    AttributeReference
                )

            If att IsNot Nothing Then

                If att.Tag.Equals(
                    "KOTA",
                    StringComparison.OrdinalIgnoreCase
                ) Then

                    oldKotaAttribute = att
                    Exit For

                End If

            End If

        Next

        If oldKotaAttribute Is Nothing Then

            ed.WriteMessage(
                vbLf &
                "Избраният блок няма атрибут KOTA."
            )

            Return

        End If


        ' ---------------------------------------------------------
        ' Четем старата кота
        ' ---------------------------------------------------------
        Dim oldValue As Double

        If Not TryReadNumber(
            oldKotaAttribute.TextString,
            oldValue
        ) Then

            ed.WriteMessage(
                vbLf &
                "Атрибутът KOTA не съдържа валидно число."
            )

            Return

        End If

        Dim newValue As Double =
            oldValue + curbHeight


        ' ---------------------------------------------------------
        ' Създаваме нов BlockReference
        ' ---------------------------------------------------------
        Dim newPosition As Point3d =
            oldBlock.Position + moveVector

        Dim newBlock As New BlockReference(
            newPosition,
            oldBlock.BlockTableRecord
        )

        newBlock.SetPropertiesFrom(oldBlock)

        newBlock.Rotation = oldBlock.Rotation
        newBlock.ScaleFactors = oldBlock.ScaleFactors
        newBlock.Normal = oldBlock.Normal


        Dim currentSpace As BlockTableRecord =
            DirectCast(
                tr.GetObject(
                    db.CurrentSpaceId,
                    OpenMode.ForWrite
                ),
                BlockTableRecord
            )

        currentSpace.AppendEntity(newBlock)

        tr.AddNewlyCreatedDBObject(
            newBlock,
            True
        )


        ' ---------------------------------------------------------
        ' Копираме атрибутите
        ' ---------------------------------------------------------
        For Each attId As ObjectId In oldBlock.AttributeCollection

            Dim oldAtt As AttributeReference =
                TryCast(
                    tr.GetObject(
                        attId,
                        OpenMode.ForRead
                    ),
                    AttributeReference
                )

            If oldAtt Is Nothing Then
                Continue For
            End If

            Dim newAtt As AttributeReference =
                DirectCast(
                    oldAtt.Clone(),
                    AttributeReference
                )

            ' Преместваме атрибута заедно с блока.
            newAtt.TransformBy(
                Matrix3d.Displacement(moveVector)
            )

            ' Ако е KOTA -> сменяме стойността.
            If newAtt.Tag.Equals(
                "KOTA",
                StringComparison.OrdinalIgnoreCase
            ) Then

                newAtt.TextString =
                    FormatLikeOriginal(
                        newValue,
                        oldAtt.TextString
                    )

            End If

            newBlock.AttributeCollection.AppendAttribute(
                newAtt
            )

            tr.AddNewlyCreatedDBObject(
                newAtt,
                True
            )

        Next

    End Sub


    ' =============================================================
    ' ПРОЧИТАНЕ НА ЧИСЛО
    '
    ' Приема:
    ' 0.15
    ' 0,15
    ' 523.18
    ' 523,18
    ' =============================================================
    Private Function TryReadNumber(
        text As String,
        ByRef value As Double
    ) As Boolean

        If String.IsNullOrWhiteSpace(text) Then
            Return False
        End If

        Dim s As String = text.Trim()

        s = s.Replace(",", ".")

        Return Double.TryParse(
            s,
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            value
        )

    End Function


    ' =============================================================
    ' ЗАПАЗВА БРОЯ ДЕСЕТИЧНИ ЗНАЦИ
    '
    ' Пример:
    ' стара кота: 523.18
    ' + 0.15
    ' нова:       523.33
    '
    ' стара кота: 523.180
    ' нова:       523.330
    ' =============================================================
    Private Function FormatLikeOriginal(
        value As Double,
        originalText As String
    ) As String

        Dim s As String =
            originalText.Trim()

        Dim separatorIndex As Integer =
            Math.Max(
                s.LastIndexOf("."c),
                s.LastIndexOf(","c)
            )

        Dim decimals As Integer = 0

        If separatorIndex >= 0 Then

            decimals =
                s.Length -
                separatorIndex -
                1

        End If

        If decimals < 0 Then
            decimals = 0
        End If

        Dim result As String =
            value.ToString(
                "F" & decimals.ToString(),
                CultureInfo.InvariantCulture
            )

        ' Ако оригиналът е бил с "," запазваме ","
        If s.Contains(",") AndAlso
           Not s.Contains(".") Then

            result =
                result.Replace(".", ",")

        End If

        Return result

    End Function


    ' =============================================================
    ' ОПРЕДЕЛЯ КОЛКО ДА СЕ ПРЕМЕСТИ НОВИЯТ ОБЕКТ НАГОРЕ
    ' =============================================================
    Private Function GetMoveDistance(
        ent As Entity
    ) As Double

        Try

            Dim ext As Extents3d =
                ent.GeometricExtents

            Dim height As Double =
                Math.Abs(
                    ext.MaxPoint.Y -
                    ext.MinPoint.Y
                )

            If height > 0.000001 Then

                ' Височината на обекта + 20% разстояние.
                Return height * 1.1

            End If

        Catch
        End Try

        ' Ако не можем да определим размер.
        Return 1.0

    End Function

End Class