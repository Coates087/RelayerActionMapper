Imports System.Text.Json.Serialization

#Region "For Previewing the JSON file"
Public Class GenericKeyPlus
    Inherits GenericKey
    <JsonInclude>
    Public Property Description As String
End Class
Public Class GenericKeyCodePlus
    Inherits GenericKeyCode
    <JsonInclude>
    Public Property Description As String

End Class
Public Class GenericKeyButtonNamePlus
    Inherits GenericKeyButtonName
    <JsonInclude>
    Public Property Description As String

End Class

Public Class GameControlsPlus
    Public Function GetDescriptonsForControls() As GameControlsPlus
        Me.W.Description = $"Move cursor up"
        Me.A.Description = $"Move cursor left"
        Me.D.Description = $"Move cursor right"
        Me.S.Description = $"Move cursor down"

        Me.CtrlW.Description = $"Move cursor up"
        Me.CtrlA.Description = $"Move cursor left"
        Me.CtrlD.Description = $"Move cursor right"
        Me.CtrlS.Description = $"Move cursor down"

        Me.Q.Description = $"Switch page/unit left"
        Me.E.Description = $"Switch page/unit left{vbCrLf}VN Event/Cutscene: Fast Forword"
        Me.WheelDown.Description = $"Zoom In"
        Me.WheelUp.Description = $"Zoom Out"

        Me.V.Description = $"Battle Screen: Display Stage information"
        Me.Escape.Description = $"Options Menu: revert to default settings{vbCrLf}Battle Screen: Battle menu{vbCrLf}VN Event/Cutscene: Skip event"

        Me.F.Description = $"Battle Screen: View Aggro List"
        Me.R.Description = $"Battle Screen: Toggle Auto Battle"

        Me.Enter.Description = $"Confirm"
        Me.Backspace.Description = $"Cancel/Back"
        Me.Tab.Description = $"Battle Screen:Display Detailed Info{vbCrLf}VN Event/Cutscene: Backlog{vbCrLf}Shop Screen: Overview{vbCrLf}Star Cube Screen: Overview"
        Me.Shift.Description = $"Equipment Screen: Remove skill/equipment{vbCrLf}Battle Screen: Display threat area{vbCrLf}VN Event/Cutscene: Auto-Advance{vbCrLf}Shop Screen: Confirm Purchase"

        Me.UpArrow.Description = $"Battle Screen: Move camera up{vbCrLf}Star Cube Screen: freely move cursor up"
        Me.LeftArrow.Description = $"Battle Screen: Move camera left{vbCrLf}Star Cube Screen: freely move cursor left"
        Me.DownArrow.Description = $"Battle Screen: Move camera down{vbCrLf}Star Cube Screen: freely move cursor down"
        Me.RightArrow.Description = $"Battle Screen: Move camera right{vbCrLf}Star Cube Screen: freely move cursor right"

        Return Me
    End Function

    Public Function GetDescriptonsForControlsJSON() As GameControlsPlus
        Me.W.Description = $"Move cursor up"
        Me.A.Description = $"Move cursor left"
        Me.D.Description = $"Move cursor right"
        Me.S.Description = $"Move cursor down"

        Me.CtrlW.Description = $"Move cursor up"
        Me.CtrlA.Description = $"Move cursor left"
        Me.CtrlD.Description = $"Move cursor right"
        Me.CtrlS.Description = $"Move cursor down"

        Me.Q.Description = $"Switch page/unit left"
        Me.E.Description = $"Switch page/unit left  --  VN Event/Cutscene: Fast Forword"
        Me.WheelDown.Description = $"Zoom In"
        Me.WheelUp.Description = $"Zoom Out"

        Me.V.Description = $"Battle Screen: Display Stage information"
        Me.Escape.Description = $"Options Menu: revert to default settings  --  Battle Screen: Battle menu  --  VN Event/Cutscene: Skip event"

        Me.F.Description = $"Battle Screen: View Aggro List"
        Me.R.Description = $"Battle Screen: Toggle Auto Battle"

        Me.Enter.Description = $"Confirm"
        Me.Backspace.Description = $"Cancel/Back"
        Me.Tab.Description = $"Battle Screen:Display Detailed Info  --  VN Event/Cutscene: Backlog  --  Shop Screen: Overview  --  Star Cube Screen: Overview"
        Me.Shift.Description = $"Equipment Screen: Remove skill/equipment  --  Battle Screen: Display threat area  --  VN Event/Cutscene: Auto-Advance  --  Shop Screen: Confirm Purchase"

        Me.UpArrow.Description = $"Battle Screen: Move camera up  --  Star Cube Screen: freely move cursor up"
        Me.LeftArrow.Description = $"Battle Screen: Move camera left  --  Star Cube Screen: freely move cursor left"
        Me.DownArrow.Description = $"Battle Screen: Move camera down  --  Star Cube Screen: freely move cursor down"
        Me.RightArrow.Description = $"Battle Screen: Move camera right  --  Star Cube Screen: freely move cursor right"

        Return Me
    End Function


    <JsonInclude>
    Public Property Enter As GenericKeyPlus
    <JsonInclude>
    Public Property Backspace As GenericKeyPlus
    <JsonInclude>
    Public Property Shift As GenericKeyPlus
    <JsonInclude>
    Public Property Tab As GenericKeyPlus
    <JsonInclude>
    Public Property W As GenericKeyPlus
    <JsonInclude>
    Public Property S As GenericKeyPlus
    <JsonInclude>
    Public Property A As GenericKeyPlus
    <JsonInclude>
    Public Property D As GenericKeyPlus
    <JsonInclude>
    Public Property Q As GenericKeyPlus
    <JsonInclude>
    Public Property E As GenericKeyPlus
    <JsonInclude>
    Public Property F As GenericKeyPlus
    <JsonInclude>
    Public Property R As GenericKeyPlus
    <JsonInclude>
    Public Property V As GenericKeyPlus
    <JsonInclude>
    Public Property Escape As GenericKeyPlus
    <JsonInclude>
    Public Property UpArrow As GenericKeyPlus
    <JsonInclude>
    Public Property DownArrow As GenericKeyPlus
    <JsonInclude>
    Public Property LeftArrow As GenericKeyPlus
    <JsonInclude>
    Public Property RightArrow As GenericKeyPlus
    <JsonInclude>
    Public Property WheelUp As GenericKeyPlus
    <JsonInclude>
    Public Property WheelDown As GenericKeyPlus
    Public Property Ctrl As GenericKeyCodePlus
    <JsonInclude>
    <JsonPropertyName("Ctrl+W")>
    Public Property CtrlW As GenericKeyButtonNamePlus
    <JsonInclude>
    <JsonPropertyName("Ctrl+S")>
    Public Property CtrlS As GenericKeyButtonNamePlus
    <JsonInclude>
    <JsonPropertyName("Ctrl+A")>
    Public Property CtrlA As GenericKeyButtonNamePlus
    <JsonInclude>
    <JsonPropertyName("Ctrl+D")>
    Public Property CtrlD As GenericKeyButtonNamePlus
End Class

#End Region


Public Class GenericKey
    Inherits GenericKeyButtonName
    <JsonInclude>
    Public Property KeyCode As New List(Of String)
End Class

Public Class GenericKeyButtonName
    <JsonInclude>
    Public Property ButtonName As String
    <JsonIgnore>
    Public Property XBoxButton As String

End Class
Public Class GenericKeyCode
    <JsonInclude>
    Public Property KeyCode As New List(Of String)

End Class

Public Class GameControls
    <JsonInclude>
    Public Property Enter As GenericKey
    <JsonInclude>
    Public Property Backspace As GenericKey
    <JsonInclude>
    Public Property Shift As GenericKey
    <JsonInclude>
    Public Property Tab As GenericKey
    <JsonInclude>
    Public Property W As GenericKey
    <JsonInclude>
    Public Property S As GenericKey
    <JsonInclude>
    Public Property A As GenericKey
    <JsonInclude>
    Public Property D As GenericKey
    <JsonInclude>
    Public Property Q As GenericKey
    <JsonInclude>
    Public Property E As GenericKey
    <JsonInclude>
    Public Property F As GenericKey
    <JsonInclude>
    Public Property R As GenericKey
    <JsonInclude>
    Public Property V As GenericKey
    <JsonInclude>
    Public Property Escape As GenericKey
    <JsonInclude>
    Public Property UpArrow As GenericKey
    <JsonInclude>
    Public Property DownArrow As GenericKey
    <JsonInclude>
    Public Property LeftArrow As GenericKey
    <JsonInclude>
    Public Property RightArrow As GenericKey
    <JsonInclude>
    Public Property WheelUp As GenericKey
    <JsonInclude>
    Public Property WheelDown As GenericKey
    Public Property Ctrl As GenericKeyCode
    <JsonInclude>
    <JsonPropertyName("Ctrl+W")>
    Public Property CtrlW As GenericKeyButtonName
    <JsonInclude>
    <JsonPropertyName("Ctrl+S")>
    Public Property CtrlS As GenericKeyButtonName
    <JsonInclude>
    <JsonPropertyName("Ctrl+A")>
    Public Property CtrlA As GenericKeyButtonName
    <JsonInclude>
    <JsonPropertyName("Ctrl+D")>
    Public Property CtrlD As GenericKeyButtonName
End Class

Public Class KeyboardClass
    Public Property KeyCode As String
    Public Property KeyName As String
End Class
