Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class MenuCreator

    Public Shared Sub CreateMenu()

        Try
            Dim acadApp As Object = AcApp.AcadApplication
            Dim menuBar As Object = acadApp.MenuBar
            Dim menuGroup As Object = acadApp.MenuGroups.Item(0)

            ' Първо махаме IVO TOOLS от MenuBar, ако вече е добавено
            For i As Integer = menuBar.Count - 1 To 0 Step -1
                Try
                    Dim mbItem As Object = menuBar.Item(i)
                    If UCase(CStr(mbItem.Name)) = "IVO TOOLS" Then
                        mbItem.RemoveFromMenuBar()
                    End If
                Catch
                End Try
            Next

            ' После трием старото popup меню IVO TOOLS
            For i As Integer = menuGroup.Menus.Count - 1 To 0 Step -1
                Try
                    Dim m As Object = menuGroup.Menus.Item(i)
                    If UCase(CStr(m.Name)) = "IVO TOOLS" Then
                        Try : m.RemoveFromMenuBar() : Catch : End Try
                        Try : m.Delete() : Catch : End Try
                    End If
                Catch
                End Try
            Next

            ' Създаваме ново меню
            Dim mainMenu As Object = menuGroup.Menus.Add("IVO TOOLS")

            mainMenu.AddMenuItem(mainMenu.Count + 1, "Вмъкване на блокове - BINX", "BINX ")
            mainMenu.AddMenuItem(mainMenu.Count + 1, "Извеждане на координати на линия - CVE", "fCVE ")
            mainMenu.AddMenuItem(mainMenu.Count + 1, "Привързване на текст с блок - SHARK", "SHARK ")
            mainMenu.AddMenuItem(mainMenu.Count + 1, "Трансформации - BTRANS", "BTRANS ")
            mainMenu.addmenuitem(mainMenu.count + 1, "Оразмеряване на линии - LENDIM", "LENDIM" & vbCr)
            mainMenu.addmenuitem(mainMenu.count + 1, "Запис на блокове в отделен файл - BREG", "BREG" & vbCr)
            mainMenu.addmenuitem(mainMenu.count + 1, "Площ по контур - AREATXT", "AREATXT" & vbCr)
            mainMenu.addmenuitem(mainMenu.count + 1, "Закръгляне на атрубити - RATT", "RATT" & vbCr)
            mainMenu.addmenuitem(mainMenu.count + 1, "Вдигане на фасада - FAS3", "FAS3" & vbCr)
            mainMenu.addmenuitem(mainMenu.count + 1, "Конвертиране на точки към блокове - P2B" & vbCr)
            mainMenu.addmenuitem(mainMenu.count + 1, "Запис на точки във файл - PTXT" & vbCr)
            mainMenu.addmenuitem(mainMenu.count + 1, "Котиране на бордюр горе - CURB" & vbCr)

            mainMenu.InsertInMenuBar(menuBar.Count + 1)

            acadApp.Update()

            Dim doc = AcApp.DocumentManager.MdiActiveDocument
            If doc IsNot Nothing Then
                doc.Editor.WriteMessage(vbLf & "IVO TOOLS менюто е създадено успешно.")
            End If

        Catch ex As Exception

            Dim doc = AcApp.DocumentManager.MdiActiveDocument
            If doc IsNot Nothing Then
                doc.Editor.WriteMessage(vbLf & "Грешка при създаване на менюто: " & ex.Message)
            End If

        End Try

    End Sub

    <Autodesk.AutoCAD.Runtime.CommandMethod("IVOTOOLSUNLOAD")>
    Public Shared Sub RemoveMenu()

        Try
            Dim acadApp As Object = AcApp.AcadApplication
            Dim menuBar As Object = acadApp.MenuBar

            For i As Integer = menuBar.Count - 1 To 0 Step -1

                Try
                    Dim mbItem As Object = menuBar.Item(i)

                    If UCase(CStr(mbItem.Name)) = "IVO TOOLS" Then
                        mbItem.RemoveFromMenuBar()
                    End If

                Catch
                End Try

            Next

            acadApp.Update()

            Dim doc = AcApp.DocumentManager.MdiActiveDocument

            If doc IsNot Nothing Then
                doc.Editor.WriteMessage(
                    vbLf & "IVO TOOLS е премахнато от MenuBar-а.")
            End If

        Catch ex As Exception

            Dim doc = AcApp.DocumentManager.MdiActiveDocument

            If doc IsNot Nothing Then
                doc.Editor.WriteMessage(
                    vbLf & "Грешка: " & ex.Message)
            End If

        End Try

    End Sub
End Class