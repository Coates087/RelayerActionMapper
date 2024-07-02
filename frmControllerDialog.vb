

Public Class frmControllerDialog
    Public IsQuestion As Boolean = False
    Public UseCustomText As Boolean = False
    Public CustomText As String = ""
    Public gControls As GameControls = Nothing
    Private Const nexusLink As String = "https://www.nexusmods.com/relayeradvanced/mods/1"
    Private Const gameBananaLink As String = "https://gamebanana.com/mods/490768"

    Private Const gamepadOnlyWarningText As String = "The "“Edit for Controller Only”" option is intended for the Controller Button Prompts mod on Nexus Mods and Game Banana." +
        vbCrLf + vbCrLf +
    "Nexus Mods Link:" + vbCrLf + nexusLink + vbCrLf + vbCrLf + "Game Banana Link:" + vbCrLf +
    gameBananaLink + vbCrLf + vbCrLf +
    "This mode will override all changes made for keyboard and mouse controls."



    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPreviewConfigFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strQuestion As String = ""
        If IsQuestion = True Then
            btnOkay.Visible = False
            strQuestion = " Do you wish to use this mode?"
        Else
            btnYes.Visible = False
            btnClose.Visible = False
        End If

        Dim finalText As String = gamepadOnlyWarningText + strQuestion

        If UseCustomText = True Then
            finalText = CustomText
        End If

        rTxtMessage.ReadOnly = True
        'rTxtMessage.BackColor = Color.Blue
        rTxtMessage.Text = finalText
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Me.Close()
    End Sub

    Private Sub btnOkay_Click(sender As Object, e As EventArgs) Handles btnOkay.Click

        Me.Close()
    End Sub
End Class