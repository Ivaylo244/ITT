Imports Autodesk.AutoCAD.Runtime
Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class CmdIvoTools

    <CommandMethod("IVOTOOLS")>
    Public Sub RunIvoTools()
        Try
            MenuCreator.CreateMenu()

            AcApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage(
                vbLf & "IVO TOOLS менюто е създадено успешно."
            )

        Catch ex As Exception

            AcApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage(
                vbLf & "Грешка при създаване на менюто: " & ex.Message
            )

        End Try
    End Sub

End Class

Public Class CmdBINX

    <CommandMethod("BINX")>
    Public Sub RunBINX()

        Dim frm As New Form1()
        frm.ShowDialog()

    End Sub

End Class

Public Class cmdBTRANS
    <CommandMethod("BTRANS")>
    Public Sub RunBTRANS()
        Dim frm As New fBTRANSvbNet()
        frm.ShowDialog()
    End Sub

End Class

Public Class cmdfCVE
    <CommandMethod("fCVE")>
    Public Sub RunCVE()
        Dim frm As New fCVE()
        frm.ShowDialog()
    End Sub

End Class

Public Class cmdSHARK
    <CommandMethod("SHARK")>
    Public Sub RunSHARK()
        Dim frm As New fSHARK()
        frm.ShowDialog()
    End Sub
End Class
Public Class cmdLENDIM
    <CommandMethod("LENDIM")>
    Public Sub RunSHARK()
        Dim frm As New frmLENDIM()
        frm.ShowDialog()
    End Sub
End Class

Public Class cmdRATT
    <CommandMethod("RATT")>
    Public Sub RunRATT()
        Dim frm As New frmRATT()
        frm.ShowDialog()
    End Sub
End Class

Public Class cmdBREG
    <CommandMethod("BREG")>
    Public Sub RunBREG()
        Dim frm As New fBREG()
        frm.ShowDialog()
    End Sub
End Class

Public Class cmdP2B

    <CommandMethod("P2B")>
    Public Sub RunP2B()

        Dim frm As New fP2B()
        frm.ShowDialog()

    End Sub

End Class