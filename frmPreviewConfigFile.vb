Imports System.IO
Imports System.Text.Json
Public Class frmPreviewConfigFile

    Public gControls As GameControlsPlus = Nothing

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPreviewConfigFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rTxtJson.ReadOnly = True

        If IsNothing(gControls) Then
            '' Display error in rich text box
            LoadErrorMessage()
        Else
            '' Stringify gControls object as a formatted JSON string
            LoadJSONString()
        End If
    End Sub

    Private Sub LoadErrorMessage()

        ''Microsoft Sans Serif, 8.25pt
        Dim aFontFamily As FontFamily = rTxtJson.Font.FontFamily
        rTxtJson.Font = New Font(aFontFamily, 28)
        rTxtJson.Text = "<No Controls are set>"
    End Sub

    Private Sub LoadJSONString()
        Dim myOptions As New JsonSerializerOptions
        myOptions.WriteIndented = True
        myOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        myOptions.WriteIndented = True

        Dim json As String = JsonSerializer.Serialize(gControls, myOptions)

        rTxtJson.Text = json
    End Sub
End Class