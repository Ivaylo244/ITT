Imports Autodesk.AutoCAD.Runtime
Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class IvoTools
    Implements IExtensionApplication

    Public Sub Initialize() Implements IExtensionApplication.Initialize
        Try
            MenuCreator.CreateMenu()

            AcApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage(
                vbLf & "ITT менюто е заредено успешно."
            )

        Catch ex As Exception

            AcApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage(
                vbLf & "Грешка при зареждане на ITT менюто: " & ex.Message
            )

        End Try
    End Sub

    Public Sub Terminate() Implements IExtensionApplication.Terminate
    End Sub

End Class