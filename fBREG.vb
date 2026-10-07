Imports System.Data.Common
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Runtime
Imports AcApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class fBREG

    Private SelectedBlocks As New List(Of BlockData)

    Private Class BlockData
        Public Property BlockName As String
        Public Property X As Double
        Public Property Y As Double
        Public Property Z As Double
        Public Property NOMER As String
        Public Property KOTA As String
        Public Property DESCRIPTION As String
    End Class

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        InitCombos()
        LoadBlocks()
    End Sub

    Private Sub InitCombos()

        Dim cols() As String = {"НОМЕР", "X", "Y", "КОТА", "ОПИСАНИЕ", "ПРАЗНО"}

        For Each cb As ComboBox In New ComboBox() {cmBoxk1, ComboBoxk2, ComboBoxk3, ComboBoxk4, ComboBoxk5}
            cb.Items.Clear()
            cb.Items.AddRange(cols)
            cb.DropDownStyle = ComboBoxStyle.DropDownList
        Next

        cmBoxk1.Text = "НОМЕР"
        ComboBoxk2.Text = "X"
        ComboBoxk3.Text = "Y"
        ComboBoxk4.Text = "КОТА"
        ComboBoxk5.Text = "ОПИСАНИЕ"

        cmBoxRazdelitel.Items.Clear()
        cmBoxRazdelitel.Items.AddRange(New String() {"SPACE", "TAB", ";", ",", "|"})
        cmBoxRazdelitel.DropDownStyle = ComboBoxStyle.DropDownList
        cmBoxRazdelitel.Text = "SPACE"

        For Each cb As ComboBox In New ComboBox() {cmBoxDesZnaciXY, cmBoxDesZnaciKota}
            cb.Items.Clear()
            For i As Integer = 0 To 6
                cb.Items.Add(i.ToString())
            Next
            cb.DropDownStyle = ComboBoxStyle.DropDownList
            cb.Text = "3"
        Next

    End Sub

    Private Sub LoadBlocks()

        cmbboxBlock.Items.Clear()
        cmbboxBlock.Items.Add("ВСИЧКИ БЛОКОВЕ")

        Dim doc = AcApp.DocumentManager.MdiActiveDocument
        Dim db = doc.Database

        Using tr = db.TransactionManager.StartTransaction()

            Dim bt = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

            For Each id As ObjectId In bt

                Dim btr = CType(tr.GetObject(id, OpenMode.ForRead), BlockTableRecord)

                If Not btr.IsLayout AndAlso Not btr.IsAnonymous Then
                    cmbboxBlock.Items.Add(btr.Name)
                End If

            Next

            tr.Commit()
        End Using

        cmbboxBlock.DropDownStyle = ComboBoxStyle.DropDownList

        If cmbboxBlock.Items.Count > 0 Then
            cmbboxBlock.SelectedIndex = 0
        End If

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Using sfd As New SaveFileDialog()
            sfd.Title = "Избери файл за запис"
            sfd.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*"
            sfd.FileName = "BREG.txt"

            If sfd.ShowDialog() = DialogResult.OK Then
                txtBoxpath.Text = sfd.FileName
            End If
        End Using

    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        SelectedBlocks.Clear()

        Dim doc = AcApp.DocumentManager.MdiActiveDocument
        Dim ed = doc.Editor
        Dim db = doc.Database

        Me.Hide()

        Dim pso As New PromptSelectionOptions()
        pso.MessageForAdding = vbLf & "Избери блокове: "

        Dim filterValues() As TypedValue = {
            New TypedValue(DxfCode.Start, "INSERT")
        }

        Dim filter As New SelectionFilter(filterValues)
        Dim psr = ed.GetSelection(pso, filter)

        If psr.Status <> PromptStatus.OK Then
            Me.Show()
            txtBoxlog.Text = "Няма избрани блокове."
            Return
        End If

        Dim wantedBlock As String = ""

        If cmbboxBlock.Text <> "ВСИЧКИ БЛОКОВЕ" Then
            wantedBlock = cmbboxBlock.Text
        End If

        Using tr = db.TransactionManager.StartTransaction()

            For Each id As ObjectId In psr.Value.GetObjectIds()

                Dim br = TryCast(tr.GetObject(id, OpenMode.ForRead), BlockReference)
                If br Is Nothing Then Continue For

                Dim realName As String = GetBlockName(br, tr)

                If wantedBlock <> "" AndAlso Not realName.Equals(wantedBlock, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                Dim data As New BlockData()
                data.BlockName = realName
                data.X = br.Position.X
                data.Y = br.Position.Y
                data.Z = br.Position.Z

                ReadAttributes(br, tr, data)

                SelectedBlocks.Add(data)

            Next

            tr.Commit()
        End Using

        SelectedBlocks.Reverse() ''обръща зпаисването на номерата в текстовия файл.

        Me.Show()
        txtBoxlog.Text = "Прочетени блокове: " & SelectedBlocks.Count.ToString()

    End Sub

    Private Function GetBlockName(br As BlockReference, tr As Transaction) As String

        If br.IsDynamicBlock Then
            Dim dynBtr = CType(tr.GetObject(br.DynamicBlockTableRecord, OpenMode.ForRead), BlockTableRecord)
            Return dynBtr.Name
        End If

        Dim btr = CType(tr.GetObject(br.BlockTableRecord, OpenMode.ForRead), BlockTableRecord)
        Return btr.Name

    End Function

    Private Sub ReadAttributes(br As BlockReference, tr As Transaction, data As BlockData)

        For Each attId As ObjectId In br.AttributeCollection

            Dim att = TryCast(tr.GetObject(attId, OpenMode.ForRead), AttributeReference)
            If att Is Nothing Then Continue For

            Dim tag As String = att.Tag.ToUpperInvariant()
            Dim value As String = att.TextString.Trim()

            Select Case tag
                Case "NOMER"
                    data.NOMER = value
                Case "KOTA"
                    data.KOTA = value
                Case "DESCRIPTION"
                    data.DESCRIPTION = value
            End Select

        Next

    End Sub

    Private Sub btnRec_Click(sender As Object, e As EventArgs) Handles btnRec.Click

        If SelectedBlocks.Count = 0 Then
            txtBoxlog.Text = "Първо избери блокове."
            Return
        End If

        If String.IsNullOrWhiteSpace(txtBoxpath.Text) Then
            txtBoxlog.Text = "Не е избран файл за запис."
            Return
        End If

        Dim sep As String = GetSeparator()
        Dim decXY As Integer = CInt(cmBoxDesZnaciXY.Text)
        Dim decKota As Integer = CInt(cmBoxDesZnaciKota.Text)

        Dim sb As New StringBuilder()

        For Each b In SelectedBlocks

            Dim values As New List(Of String)

            For Each cb As ComboBox In New ComboBox() {cmBoxk1, ComboBoxk2, ComboBoxk3, ComboBoxk4, ComboBoxk5}

                Select Case cb.Text
                    Case "НОМЕР"
                        values.Add(If(b.NOMER, ""))
                    Case "Y"
                        values.Add(RoundText(b.X, decXY))
                    Case "X"
                        values.Add(RoundText(b.Y, decXY))
                    Case "КОТА"
                        values.Add(GetKotaText(b, decKota))
                    Case "ОПИСАНИЕ"
                        values.Add(If(b.DESCRIPTION, ""))
                    Case "ПРАЗНО"
                End Select

            Next

            sb.AppendLine(String.Join(sep, values))

        Next

        File.WriteAllText(txtBoxpath.Text, sb.ToString(), Encoding.UTF8)

        txtBoxlog.Text =
            "Прочетени блокове: " & SelectedBlocks.Count.ToString() & Environment.NewLine &
            "Файлът е записан успешно:" & Environment.NewLine &
            txtBoxpath.Text

    End Sub

    Private Function GetSeparator() As String

        Select Case cmBoxRazdelitel.Text
            Case "SPACE"
                Return " "
            Case "TAB"
                Return vbTab
            Case Else
                Return cmBoxRazdelitel.Text
        End Select

    End Function

    Private Function RoundText(value As Double, decimals As Integer) As String
        Return Math.Round(value, decimals).ToString("F" & decimals, CultureInfo.InvariantCulture)
    End Function

    Private Function GetKotaText(b As BlockData, decimals As Integer) As String

        Dim d As Double

        If Not String.IsNullOrWhiteSpace(b.KOTA) Then
            If Double.TryParse(b.KOTA.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, d) Then
                Return RoundText(d, decimals)
            Else
                Return b.KOTA
            End If
        End If

        Return RoundText(b.Z, decimals)

    End Function

    Private Sub fBREG_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class