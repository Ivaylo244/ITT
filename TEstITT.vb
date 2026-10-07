Imports Autodesk.AutoCAD.Runtime
Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class TEstITT

    <CommandMethod("ITTTEST")>
    Public Sub ITTTEST()

        AcApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage(
            vbLf & "ITTTEST работи."
        )

    End Sub

End Class