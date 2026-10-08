
Imports System
Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class MenuCreator

    Private Const MENU_NAME As String = "IVO TOOLS"

    Private Shared Sub WriteLog(message As String)
        Dim doc = AcApp.DocumentManager.MdiActiveDocument

        If doc IsNot Nothing Then
            doc.Editor.WriteMessage(vbLf & message)
        End If
    End Sub

    Private Shared Function FindMenu(menuGroup As Object,
                                     menuName As String) As Object

        For i As Integer = 0 To CInt(menuGroup.Menus.Count) - 1

            Dim m As Object = menuGroup.Menus.Item(i)

            If String.Equals(CStr(m.Name),
                             menuName,
                             StringComparison.OrdinalIgnoreCase) Then
                Return m
            End If

        Next

        Return Nothing
    End Function

    Private Shared Function CreateSubMenu(parentMenu As Object,
                                          title As String) As Object

        Dim subMenu As Object =
            parentMenu.AddSubMenu(CInt(parentMenu.Count) + 1, title)

        Return subMenu
    End Function

    Private Shared Sub AddCommand(targetMenu As Object,
                                  caption As String,
                                  commandName As String)

        Dim macro As String =
            ChrW(3) & ChrW(3) & "_." & commandName & " "

        targetMenu.AddMenuItem(
            CInt(targetMenu.Count) + 1,
            caption,
            macro
        )
    End Sub

    Public Shared Sub CreateMenu()

        Try
            Dim acadApp As Object = AcApp.AcadApplication
            Dim menuBar As Object = acadApp.MenuBar
            Dim menuGroup As Object = acadApp.MenuGroups.Item(0)

            Dim mainMenu As Object = FindMenu(menuGroup, MENU_NAME)

            If mainMenu IsNot Nothing Then

                Try
                    mainMenu.RemoveFromMenuBar()
                Catch
                End Try

                ' Изчистваме старите елементи.
                For i As Integer = CInt(mainMenu.Count) - 1 To 0 Step -1
                    mainMenu.Item(i).Delete()
                Next

            Else
                mainMenu = menuGroup.Menus.Add(MENU_NAME)
            End If

            ' ======================================
            ' БЛОКОВЕ
            ' ======================================

            Dim blocksMenu As Object =
                CreateSubMenu(mainMenu, "Блокове")

            AddCommand(blocksMenu, "Вмъкване на блокове - BINX", "BINX")
            AddCommand(blocksMenu, "Запис на блокове във файл - BREG", "BREG")
            AddCommand(blocksMenu, "Конвертиране на точки към блокове - P2B", "P2B")
            AddCommand(blocksMenu, "Котиране на бордюр - CURB", "CURB")
            AddCommand(blocksMenu, "Извеждане на координати на линия - CVE", "fCVE")
            AddCommand(blocksMenu, "Привързване на текст с блок - SHARK", "SHARK")
            AddCommand(blocksMenu, "Закръгляне на атрибути - RATT", "RATT")

            ' ======================================
            ' СЛОЕВЕ
            ' ======================================

            Dim layersMenu As Object =
                CreateSubMenu(mainMenu, "Слоеве")

            ' Бъдещи команди за слоеве.
            ' Не добавяме фиктивни команди.

            ' ======================================
            ' ОРАЗМЕРЯВАНЕ
            ' ======================================

            Dim dimMenu As Object =
                CreateSubMenu(mainMenu, "Оразмеряване")

            AddCommand(dimMenu, "Оразмеряване на линии - LENDIM", "LENDIM")
            AddCommand(dimMenu, "Площ по контур - AREATXT", "AREATXT")

            ' ======================================
            ' ТОЧКИ
            ' ======================================

            Dim pointsMenu As Object =
                CreateSubMenu(mainMenu, "Точки")

            AddCommand(pointsMenu, "Запис на точки във файл - PTXT", "PTXT")

            ' ======================================
            ' ТРАНСФОРМАЦИИ
            ' ======================================

            Dim transMenu As Object =
                CreateSubMenu(mainMenu, "Трансформации")

            AddCommand(transMenu, "Трансформации - BTRANS", "BTRANS")

            ' ======================================
            ' ДРУГИ
            ' ======================================

            Dim otherMenu As Object =
                CreateSubMenu(mainMenu, "Други")

            AddCommand(otherMenu, "Вдигане на фасада - FAS3", "FAS3")

            ' ======================================
            ' ПОКАЗВАНЕ В MENUBAR
            ' ======================================

            mainMenu.InsertInMenuBar(CInt(menuBar.Count) + 1)
            acadApp.Update()

            WriteLog("IVO TOOLS менюто е заредено успешно.")

        Catch ex As System.Exception
            WriteLog("Грешка при създаване на менюто: " & ex.ToString())
        End Try
    End Sub

End Class
