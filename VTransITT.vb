Imports System.Runtime.InteropServices

Public Class VTransITT

    Public Const AUTO_ZONE As Integer = -999

    <DllImport("trans_dll_64.dll",
               EntryPoint:="InitializeTrans",
               CharSet:=CharSet.Unicode,
               CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function InitializeTrans() As Integer
    End Function

    <DllImport("trans_dll_64.dll",
               EntryPoint:="FreeTrans",
               CharSet:=CharSet.Unicode,
               CallingConvention:=CallingConvention.StdCall)>
    Public Shared Sub FreeTrans()
    End Sub

    <DllImport("trans_dll_64.dll",
               EntryPoint:="TRANS",
               CharSet:=CharSet.Unicode,
               CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function TRANS(
        sys1 As Integer,
        sys2 As Integer,
        type1 As Integer,
        type2 As Integer,
        width1 As Integer,
        width2 As Integer,
        zone1 As Integer,
        ByRef zone2 As Integer,
        ByRef y As Double,
        ByRef x As Double,
        ByRef h As Double
    ) As Integer
    End Function

    Public Shared Function TransformPoint(
        fromSys As Integer,
        toSys As Integer,
        fromType As Integer,
        toType As Integer,
        fromWidth As Integer,
        toWidth As Integer,
        fromZone As Integer,
        toZone As Integer,
        xInput As Double,
        yInput As Double,
        hInput As Double,
        ByRef xOutput As Double,
        ByRef yOutput As Double,
        ByRef hOutput As Double
    ) As Integer

        Dim x As Double = xInput
        Dim y As Double = yInput
        Dim h As Double = hInput
        Dim zoneOut As Integer = toZone

        Dim result As Integer = TRANS(
            fromSys,
            toSys,
            fromType,
            toType,
            fromWidth,
            toWidth,
            fromZone,
            zoneOut,
            y,
            x,
            h
        )

        xOutput = x
        yOutput = y
        hOutput = h

        Return result

    End Function
    Private Shared _isInitialized As Boolean = False

    Public Shared Sub EnsureInitialized()
        If Not _isInitialized Then
            Dim r As Integer = InitializeTrans()
            If r <> 0 Then
                Throw New Exception("Грешка при InitializeTrans: " & r.ToString())
            End If
            _isInitialized = True
        End If
    End Sub
End Class