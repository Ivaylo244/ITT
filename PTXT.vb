Imports System.Data.Common
Imports System.Globalization
Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports System.Reflection
Imports System.Windows.Forms
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime
Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class PTXT

    <CommandMethod("PTXT")>
    Public Sub ExportPointsAndText()

        Dim doc As Document = AcApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor

        ' ---------------------------------------------------------
        ' 1. Избор на TXT файл
        ' ---------------------------------------------------------
        Dim filePath As String = ""

        Using sfd As New SaveFileDialog()

            sfd.Title = "Избери файл за запис"
            sfd.Filter = "Текстов файл (*.txt)|*.txt"
            sfd.DefaultExt = "txt"
            sfd.AddExtension = True
            sfd.OverwritePrompt = True
            sfd.FileName = "Points.txt"

            If sfd.ShowDialog() <> DialogResult.OK Then
                ed.WriteMessage(vbLf & "PTXT е прекратена.")
                Return
            End If

            filePath = sfd.FileName

        End Using

        Try

            ' Създаваме/изчистваме файла още в началото.
            Using sw As New StreamWriter(filePath, False)
            End Using

            Dim pointNumber As Integer = 1

            ed.WriteMessage(vbLf)
            ed.WriteMessage(vbLf & "PTXT - избор на точки и коти.")
            ed.WriteMessage(vbLf & "Enter или Escape прекратява командата.")
            ed.WriteMessage(vbLf)

            Do

                ' -------------------------------------------------
                ' 2. Избор на точка
                ' -------------------------------------------------
                Dim ppo As New PromptPointOptions(
                    vbLf & "Посочи точка <Enter за край>: "
                )

                ppo.AllowNone = True

                Dim ppr As PromptPointResult = ed.GetPoint(ppo)

                If ppr.Status = PromptStatus.None OrElse
                   ppr.Status = PromptStatus.Cancel Then

                    Exit Do

                End If

                If ppr.Status <> PromptStatus.OK Then
                    Exit Do
                End If

                Dim selectedPoint As Point3d = ppr.Value

                ' -------------------------------------------------
                ' 3. Избор на TEXT / MTEXT
                ' -------------------------------------------------
                Dim textValue As String = ""

                Do

                    Dim peo As New PromptEntityOptions(
                        vbLf & "Посочи текст за кота <Enter за край>: "
                    )

                    peo.AllowNone = True

                    peo.SetRejectMessage(
                        vbLf & "Трябва да избереш TEXT или MTEXT."
                    )

                    peo.AddAllowedClass(GetType(DBText), False)
                    peo.AddAllowedClass(GetType(MText), False)

                    Dim per As PromptEntityResult = ed.GetEntity(peo)

                    If per.Status = PromptStatus.None OrElse
                       per.Status = PromptStatus.Cancel Then

                        ed.WriteMessage(
                            vbLf & "PTXT приключи. Записани точки: " &
                            (pointNumber - 1).ToString()
                        )

                        Return

                    End If

                    If per.Status <> PromptStatus.OK Then
                        Continue Do
                    End If

                    Using tr As Transaction =
                        db.TransactionManager.StartTransaction()

                        Dim ent As Entity =
                            TryCast(
                                tr.GetObject(
                                    per.ObjectId,
                                    OpenMode.ForRead
                                ),
                                Entity
                            )

                        If TypeOf ent Is DBText Then

                            Dim txt As DBText =
                                DirectCast(ent, DBText)

                            textValue = txt.TextString

                        ElseIf TypeOf ent Is MText Then

                            Dim mtxt As MText =
                                DirectCast(ent, MText)

                            textValue = mtxt.Contents

                        Else

                            Continue Do

                        End If

                        tr.Commit()

                    End Using

                    Exit Do

                Loop

                ' -------------------------------------------------
                ' 4. Подготовка на реда
                '
                ' № X Y КОТА
                ' -------------------------------------------------
                Dim xText As String =
    selectedPoint.Y.ToString(
        "F3",
        CultureInfo.InvariantCulture
    )

                Dim yText As String =
    selectedPoint.X.ToString(
        "F3",
        CultureInfo.InvariantCulture
    )

                ' Текстът се записва точно както е в чертежа.
                Dim line As String =
                    pointNumber.ToString() & " " &
                    xText & " " &
                    yText & " " &
                    textValue

                ' -------------------------------------------------
                ' 5. Веднага записваме реда във файла
                ' -------------------------------------------------
                Using sw As New StreamWriter(filePath, True)
                    sw.WriteLine(line)
                End Using

                ed.WriteMessage(
                    vbLf &
                    "Записано: " &
                    line
                )

                pointNumber += 1

            Loop

            ed.WriteMessage(
                vbLf &
                "PTXT приключи. Записани точки: " &
                (pointNumber - 1).ToString()
            )

            ed.WriteMessage(
                vbLf &
                "Файл: " &
                filePath
            )

        Catch ex As System.Exception

            ed.WriteMessage(
                vbLf &
                "Грешка в PTXT: " &
                ex.Message
            )

        End Try

    End Sub

End Class