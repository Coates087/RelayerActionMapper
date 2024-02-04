Imports System.IO
Imports System.Text.Json
Public Class frmControllerDialog

    Public gControls As GameControls = Nothing
    Private Const nexusLink As String = "https://www.nexusmods.com/relayeradvanced/mods/1"
    Private Const gameBananaLink As String = "https://gamebanana.com/mods/490768"

    Private Const gamepadOnlyWarningText As String = "You have selected "“Edit for Controller Only”". This mode is intended for the Controller Button Prompts mod on Nexus Mods and Game Banana." +
        vbCrLf + vbCrLf +
    "Nexus Mods Link:" + vbCrLf + nexusLink + vbCrLf + vbCrLf + "Game Banana Link:" + vbCrLf +
    gameBananaLink + vbCrLf + vbCrLf +
    "This mode will override all changes made for keyboard and mouse controls. Do you wish to use this mode?"


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPreviewConfigFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rTxtMessage.ReadOnly = True

        rTxtMessage.Text = gamepadOnlyWarningText
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Me.Close()
    End Sub
End Class