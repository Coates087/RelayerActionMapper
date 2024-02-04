Public Class frmControlWindow


    Public gImageData As Bitmap = Nothing
    Public gControls As GameControls = Nothing
    Public gButton As GenericKey = Nothing
    Public gKeyList As GenericKeyCode = Nothing

    Public gStrKeys As New List(Of String)
    Public gStrAllKeys As String = String.Empty
    Private Const conDlbQuote As String = Chr(34)
    Private Const conRemoveTag As String = "btn-remove-"
    Private Const conComboTag As String = "key-"
    Private gHighestNo As Integer = 1
    Private gLastTabStop As Integer = 1
    Public toolTipText As String = String.Empty

    '' On removing a control, figure out how to move the other controls dynamically

    Private Sub frmControlWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadControlData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim myControls As List(Of Control) = gBoxKeys.Controls.Cast(Of Control)().Where(Function(t) t.GetType = GetType(ComboBox) AndAlso t.Tag?.ToString.StartsWith("key-")).ToList

        gStrKeys = New List(Of String)
        For Each aComboControl In myControls
            gStrKeys.Add(DirectCast(aComboControl, ComboBox).SelectedValue)

        Next
        gStrAllKeys = String.Join(", ", gStrKeys)
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddKey_Click(sender As Object, e As EventArgs) Handles btnAddKey.Click

        Dim keyList As List(Of KeyboardClass) = GetKeyboardKeys()
        Dim lastControl As Control = gBoxKeys.Controls.Cast(Of Control)().Where(Function(t) t.GetType = GetType(ComboBox)).LastOrDefault
        Dim newY As Integer = (lastControl.Location.Y + lastControl.Size.Height) + 5 ' + (lastControl.Location.Y / 2)

        'gHighestNo = +1
        Dim controlResult = CopyDropDownItem(cboDefaultKey, btnRemove, gHighestNo.ToString, lastControl.TabStop + 1, lastControl.TabStop + 2, cboDefaultKey.Location.X, newY)

        controlResult.DisplayMember = "KeyName"
        controlResult.ValueMember = "KeyCode"
        controlResult.DataSource = New List(Of KeyboardClass)(keyList)

        controlResult.Parent = gBoxKeys
    End Sub

    Private Sub btnDynamicRemove(sender As Object, e As EventArgs)

        If Not IsNothing(sender.Tag) Then
            Dim tagNo As String = sender.Tag.ToString.Replace(conRemoveTag, "")

            Dim myDropDown = gBoxKeys.Controls.Cast(Of Control)().Where(Function(t) {conComboTag + tagNo}.Contains(t.Tag?.ToString)).FirstOrDefault

            Dim myButton = gBoxKeys.Controls.Cast(Of Control)().Where(Function(t) {conRemoveTag + tagNo}.Contains(t.Tag?.ToString)).FirstOrDefault

            gBoxKeys.Controls.Remove(myDropDown)
            gBoxKeys.Controls.Remove(myButton)

            OrganizeControls()
        End If

    End Sub

    Private Sub OrganizeControls()

        Dim allControls = gBoxKeys.Controls.Cast(Of Control)().Where(Function(t) Not IsNothing(t.Tag)).ToList

        Dim myDropDowns1 = allControls.Cast(Of Control)().Where(Function(t) conComboTag.StartsWith(t.Tag?.ToString)).ToList

        Dim myDropDowns = allControls.Cast(Of Control)().Where(Function(t) t.Tag?.ToString.StartsWith(conComboTag) AndAlso Not cboDefaultKey.Tag.ToString.Equals(t.Tag?.ToString)).ToList

        Dim myButtons = allControls.Cast(Of Control)().Where(Function(t) t.Tag?.ToString.StartsWith(conRemoveTag) AndAlso Not btnRemove.Tag.ToString.Equals(t.Tag?.ToString)).ToList

        Dim startingPoint As Integer = cboDefaultKey.Location.Y
        ''+= cboDefaultKey.Size.Height + 5
        Dim newY As Integer = cboDefaultKey.Size.Height + 5

        For i As Integer = 0 To myDropDowns.Count - 1 Step 1
            newY += cboDefaultKey.Size.Height + 5
            myDropDowns(i).Location = New Point(myDropDowns(i).Location.X, newY) '

            myButtons(i).Location = New Point(myButtons(i).Location.X, newY) '
        Next
    End Sub

    Private Sub LoadControlData()
        picBoxButton.Image = gImageData
        Dim keyList As List(Of KeyboardClass) = GetKeyboardKeys()
        cboDefaultKey.DisplayMember = "KeyName"
        cboDefaultKey.ValueMember = "KeyCode"
        cboDefaultKey.DataSource = New List(Of KeyboardClass)(keyList)

        Dim obj = IIf(IsNothing(gButton), gKeyList, gButton)

        Dim strKeys As List(Of String) = obj.KeyCode

        If Not String.IsNullOrWhiteSpace(toolTipText) Then
            tip1.SetToolTip(picBoxButton, toolTipText)
        End If

        If strKeys.Count > 0 Then
            cboDefaultKey.SelectedValue = strKeys(0)
            Dim newY As Integer = cboDefaultKey.Location.Y
            Dim tabStop As Integer = 1
            Dim tabStop2 As Integer = 2

            For i As Integer = 1 To strKeys.Count - 1 Step 1
                newY += cboDefaultKey.Size.Height + 5

                Dim controlResult = CopyDropDownItem(cboDefaultKey, btnRemove, i.ToString, tabStop, tabStop2, cboDefaultKey.Location.X, newY)

                controlResult.DisplayMember = "KeyName"
                controlResult.ValueMember = "KeyCode"
                controlResult.DataSource = New List(Of KeyboardClass)(keyList)

                controlResult.Parent = gBoxKeys

                controlResult.SelectedValue = strKeys(i)

                tabStop += 2
                tabStop2 += 2
            Next

        End If



    End Sub

    Private Function CopyDropDownItem(originalControl As ComboBox, originalRemoveButton As Button, strTag As String, tabStop As Integer, tabStop2 As Integer, x As Integer, y As Integer) As ComboBox
        Dim finalControl = New ComboBox()

        finalControl.Font = originalControl.Font

        finalControl.Size = originalControl.Size
        finalControl.Tag = conComboTag + strTag
        finalControl.TabStop = tabStop
        finalControl.DropDownStyle = originalControl.DropDownStyle

        finalControl.Location = New Point(x, y)
        finalControl.Parent = originalControl.Parent

        finalControl.Name = "cboKey_" + strTag

        Dim finalRemoveButton = New Button()
        finalRemoveButton.Visible = True
        finalRemoveButton.Text = originalRemoveButton.Text
        finalRemoveButton.Size = originalRemoveButton.Size
        finalRemoveButton.Tag = conRemoveTag + strTag
        finalRemoveButton.TabStop = tabStop2
        finalRemoveButton.Location = New Point(x + 5 + finalControl.Size.Width, y) '
        finalRemoveButton.Parent = originalRemoveButton.Parent

        finalRemoveButton.Name = "btnRemove_" + strTag

        gLastTabStop = tabStop2
        gHighestNo += 1

        AddHandler finalRemoveButton.Click, AddressOf btnDynamicRemove
        Return finalControl
    End Function

    Private Function CopyControlItem(objControl As Object, x As Integer, y As Integer) As Object
        Dim finalControl As Object = Nothing

        Return finalControl
    End Function


    Public Function GetKeyboardKeys() As List(Of KeyboardClass)
        Dim result As New List(Of KeyboardClass)

        '' Face buttons
        result.Add(New KeyboardClass With {.KeyCode = "None", .KeyName = "None"})

        result.Add(New KeyboardClass With {.KeyCode = "Backspace", .KeyName = "Backspace"})
        result.Add(New KeyboardClass With {.KeyCode = "Tab", .KeyName = "Tab"})
        result.Add(New KeyboardClass With {.KeyCode = "Clear", .KeyName = "Clear"})
        result.Add(New KeyboardClass With {.KeyCode = "Return", .KeyName = "Enter"})
        result.Add(New KeyboardClass With {.KeyCode = "Escape", .KeyName = "Escape"})
        result.Add(New KeyboardClass With {.KeyCode = "Pause", .KeyName = "Pause"})
        result.Add(New KeyboardClass With {.KeyCode = "Space", .KeyName = "Space"})
        result.Add(New KeyboardClass With {.KeyCode = "Exclaim", .KeyName = "Exclaimation"})
        result.Add(New KeyboardClass With {.KeyCode = "DoubleQuote", .KeyName = "DoubleQuote"})
        result.Add(New KeyboardClass With {.KeyCode = "Quote", .KeyName = "Quote"})
        result.Add(New KeyboardClass With {.KeyCode = "Hash", .KeyName = "# (Hash)"})
        result.Add(New KeyboardClass With {.KeyCode = "Dollar", .KeyName = "$ (Dollar)"})
        result.Add(New KeyboardClass With {.KeyCode = "Percent", .KeyName = "% (Percent)"})
        result.Add(New KeyboardClass With {.KeyCode = "Ampersand", .KeyName = "& (Ampersand)"})
        result.Add(New KeyboardClass With {.KeyCode = "LeftParen", .KeyName = "( (LeftParen)"})
        result.Add(New KeyboardClass With {.KeyCode = "RightParen", .KeyName = ") (RightParen)"})
        result.Add(New KeyboardClass With {.KeyCode = "Asterisk", .KeyName = "* (Asterisk)"})
        result.Add(New KeyboardClass With {.KeyCode = "Plus", .KeyName = "+ (Plus)"})
        result.Add(New KeyboardClass With {.KeyCode = "Comma", .KeyName = ", (Comma)"})
        result.Add(New KeyboardClass With {.KeyCode = "Minus", .KeyName = "- (Minus)"})
        result.Add(New KeyboardClass With {.KeyCode = "Period", .KeyName = ". (Period)"})
        result.Add(New KeyboardClass With {.KeyCode = "Slash", .KeyName = "/ (Slash)"})
        result.Add(New KeyboardClass With {.KeyCode = "Backslash", .KeyName = "\ (Back Slash)"})
        result.Add(New KeyboardClass With {.KeyCode = "Period", .KeyName = ". (Period)"})
        result.Add(New KeyboardClass With {.KeyCode = "Colon", .KeyName = ": (:)"})
        result.Add(New KeyboardClass With {.KeyCode = "Semicolon", .KeyName = "; (;)"})
        result.Add(New KeyboardClass With {.KeyCode = "Less", .KeyName = "< (Less)"})
        result.Add(New KeyboardClass With {.KeyCode = "Equals", .KeyName = "= (Equals)"})
        result.Add(New KeyboardClass With {.KeyCode = "Greater", .KeyName = "> (Greater)"})
        result.Add(New KeyboardClass With {.KeyCode = "Question", .KeyName = "? (Question)"})
        result.Add(New KeyboardClass With {.KeyCode = "At", .KeyName = "@ (At Symbol)"})
        result.Add(New KeyboardClass With {.KeyCode = "LeftBracket", .KeyName = "[ (LeftBracket)"})
        result.Add(New KeyboardClass With {.KeyCode = "RightBracket", .KeyName = "] (RighttBracket)"})
        result.Add(New KeyboardClass With {.KeyCode = "LeftCurlyBracket", .KeyName = "{ (LeftCurlyBracket)"})
        result.Add(New KeyboardClass With {.KeyCode = "RightCurlyBracket", .KeyName = "} (RightCurlyBracket)"})

        result.Add(New KeyboardClass With {.KeyCode = "Caret", .KeyName = "^ (Caret)"})
        result.Add(New KeyboardClass With {.KeyCode = "Underscore", .KeyName = "_ (Underscore)"})
        result.Add(New KeyboardClass With {.KeyCode = "BackQuote", .KeyName = "` (BackQuote)"})
        result.Add(New KeyboardClass With {.KeyCode = "A", .KeyName = "A"})
        result.Add(New KeyboardClass With {.KeyCode = "B", .KeyName = "B"})
        result.Add(New KeyboardClass With {.KeyCode = "C", .KeyName = "C"})
        result.Add(New KeyboardClass With {.KeyCode = "D", .KeyName = "D"})
        result.Add(New KeyboardClass With {.KeyCode = "E", .KeyName = "E"})
        result.Add(New KeyboardClass With {.KeyCode = "F", .KeyName = "F"})
        result.Add(New KeyboardClass With {.KeyCode = "G", .KeyName = "G"})
        result.Add(New KeyboardClass With {.KeyCode = "H", .KeyName = "H"})
        result.Add(New KeyboardClass With {.KeyCode = "I", .KeyName = "I"})
        result.Add(New KeyboardClass With {.KeyCode = "J", .KeyName = "J"})
        result.Add(New KeyboardClass With {.KeyCode = "K", .KeyName = "K"})
        result.Add(New KeyboardClass With {.KeyCode = "L", .KeyName = "L"})
        result.Add(New KeyboardClass With {.KeyCode = "M", .KeyName = "M"})
        result.Add(New KeyboardClass With {.KeyCode = "N", .KeyName = "N"})
        result.Add(New KeyboardClass With {.KeyCode = "O", .KeyName = "O"})

        result.Add(New KeyboardClass With {.KeyCode = "P", .KeyName = "P"})
        result.Add(New KeyboardClass With {.KeyCode = "Q", .KeyName = "Q"})
        result.Add(New KeyboardClass With {.KeyCode = "R", .KeyName = "R"})
        result.Add(New KeyboardClass With {.KeyCode = "S", .KeyName = "S"})
        result.Add(New KeyboardClass With {.KeyCode = "T", .KeyName = "T"})
        result.Add(New KeyboardClass With {.KeyCode = "U", .KeyName = "U"})
        result.Add(New KeyboardClass With {.KeyCode = "V", .KeyName = "V"})
        result.Add(New KeyboardClass With {.KeyCode = "W", .KeyName = "W"})
        result.Add(New KeyboardClass With {.KeyCode = "X", .KeyName = "X"})
        result.Add(New KeyboardClass With {.KeyCode = "Y", .KeyName = "Y"})
        result.Add(New KeyboardClass With {.KeyCode = "Z", .KeyName = "Z"})

        result.Add(New KeyboardClass With {.KeyCode = "Pipe", .KeyName = "| (Pipe)"})
        result.Add(New KeyboardClass With {.KeyCode = "Tilde", .KeyName = "~ (Tilde)"})
        result.Add(New KeyboardClass With {.KeyCode = "Delete", .KeyName = "Delete"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad0", .KeyName = "Keypad 0"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad1", .KeyName = "Keypad 1"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad2", .KeyName = "Keypad 2"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad3", .KeyName = "Keypad 3"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad4", .KeyName = "Keypad 4"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad5", .KeyName = "Keypad 5"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad6", .KeyName = "Keypad 6"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad7", .KeyName = "Keypad 7"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad8", .KeyName = "Keypad 8"})
        result.Add(New KeyboardClass With {.KeyCode = "Keypad9", .KeyName = "Keypad 9"})

        result.Add(New KeyboardClass With {.KeyCode = "KeypadPeriod", .KeyName = "Keypad ."})
        result.Add(New KeyboardClass With {.KeyCode = "KeypadDivide", .KeyName = "Keypad /"})
        result.Add(New KeyboardClass With {.KeyCode = "KeypadMultiply", .KeyName = "Keypad *"})
        result.Add(New KeyboardClass With {.KeyCode = "KeypadMinus", .KeyName = "Keypad -"})
        result.Add(New KeyboardClass With {.KeyCode = "KeypadPlus", .KeyName = "Keypad +"})
        result.Add(New KeyboardClass With {.KeyCode = "KeypadEnter", .KeyName = "Keypad Enter"})
        result.Add(New KeyboardClass With {.KeyCode = "UpArrow", .KeyName = "Up Arrow"})
        result.Add(New KeyboardClass With {.KeyCode = "DownArrow", .KeyName = "Down Arrow"})
        result.Add(New KeyboardClass With {.KeyCode = "LeftArrow", .KeyName = "Left Arrow"})
        result.Add(New KeyboardClass With {.KeyCode = "RightArrow", .KeyName = "Right Arrow"})

        result.Add(New KeyboardClass With {.KeyCode = "Insert", .KeyName = "Insert"})
        result.Add(New KeyboardClass With {.KeyCode = "Home", .KeyName = "Home"})
        result.Add(New KeyboardClass With {.KeyCode = "End", .KeyName = "End"})
        result.Add(New KeyboardClass With {.KeyCode = "PageUp", .KeyName = "Page Up"})
        result.Add(New KeyboardClass With {.KeyCode = "PageDown", .KeyName = "Page Down"})

        result.Add(New KeyboardClass With {.KeyCode = "F1", .KeyName = "F1"})
        result.Add(New KeyboardClass With {.KeyCode = "F2", .KeyName = "F2"})
        result.Add(New KeyboardClass With {.KeyCode = "F3", .KeyName = "F3"})
        result.Add(New KeyboardClass With {.KeyCode = "F4", .KeyName = "F4"})
        result.Add(New KeyboardClass With {.KeyCode = "F5", .KeyName = "F5"})
        result.Add(New KeyboardClass With {.KeyCode = "F6", .KeyName = "F6"})
        result.Add(New KeyboardClass With {.KeyCode = "F7", .KeyName = "F7"})
        result.Add(New KeyboardClass With {.KeyCode = "F8", .KeyName = "F8"})
        result.Add(New KeyboardClass With {.KeyCode = "F9", .KeyName = "F9"})
        result.Add(New KeyboardClass With {.KeyCode = "F10", .KeyName = "F10"})
        result.Add(New KeyboardClass With {.KeyCode = "F11", .KeyName = "F11"})
        result.Add(New KeyboardClass With {.KeyCode = "F12", .KeyName = "F12"})
        result.Add(New KeyboardClass With {.KeyCode = "F13", .KeyName = "F13"})
        result.Add(New KeyboardClass With {.KeyCode = "F14", .KeyName = "F14"})
        result.Add(New KeyboardClass With {.KeyCode = "F15", .KeyName = "F15"})
        result.Add(New KeyboardClass With {.KeyCode = "Numlock", .KeyName = "Numlock"})
        result.Add(New KeyboardClass With {.KeyCode = "CapsLock", .KeyName = "CapsLock"})
        result.Add(New KeyboardClass With {.KeyCode = "ScrollLock", .KeyName = "ScrollLock"})
        result.Add(New KeyboardClass With {.KeyCode = "LeftShift", .KeyName = "LeftShift"})
        result.Add(New KeyboardClass With {.KeyCode = "RightShift", .KeyName = "RightShift"})
        result.Add(New KeyboardClass With {.KeyCode = "LeftControl", .KeyName = "LeftControl"})
        result.Add(New KeyboardClass With {.KeyCode = "RightControl", .KeyName = "RightControl"})
        result.Add(New KeyboardClass With {.KeyCode = "LeftAlt", .KeyName = "LeftAlt"})
        result.Add(New KeyboardClass With {.KeyCode = "RightAlt", .KeyName = "RightAlt"})

        result.Add(New KeyboardClass With {.KeyCode = "LeftCommand", .KeyName = "LeftCommand"})
        result.Add(New KeyboardClass With {.KeyCode = "RightCommand", .KeyName = "RightCommand"})
        result.Add(New KeyboardClass With {.KeyCode = "LeftApple", .KeyName = "LeftApple"})
        result.Add(New KeyboardClass With {.KeyCode = "RightApple", .KeyName = "RightApple"})
        result.Add(New KeyboardClass With {.KeyCode = "Help", .KeyName = "Help"})
        result.Add(New KeyboardClass With {.KeyCode = "Print", .KeyName = "Print"})

        result.Add(New KeyboardClass With {.KeyCode = "SysReq", .KeyName = "SysReq"})
        result.Add(New KeyboardClass With {.KeyCode = "Break", .KeyName = "Break"})
        result.Add(New KeyboardClass With {.KeyCode = "Menu", .KeyName = "Menu"})

        result.Add(New KeyboardClass With {.KeyCode = "Mouse0", .KeyName = "Mouse 0"})
        result.Add(New KeyboardClass With {.KeyCode = "Mouse1", .KeyName = "Mouse 1"})
        result.Add(New KeyboardClass With {.KeyCode = "Mouse2", .KeyName = "Mouse 2"})
        result.Add(New KeyboardClass With {.KeyCode = "Mouse3", .KeyName = "Mouse 3"})
        result.Add(New KeyboardClass With {.KeyCode = "Mouse4", .KeyName = "Mouse 4"})
        result.Add(New KeyboardClass With {.KeyCode = "Mouse5", .KeyName = "Mouse 5"})
        result.Add(New KeyboardClass With {.KeyCode = "Mouse6", .KeyName = "Mouse 6"})

        'result.Add(New KeyboardClass With {.KeyCode = "None", .KeyName = "None"})
        'result.Add(New KeyboardClass With {.KeyCode = "None", .KeyName = "None"})
        'result.Add(New KeyboardClass With {.KeyCode = "None", .KeyName = "None"})

        Return result
    End Function




    'LeftWindows
    'RightWindows
    'AltGr

    'SysReq
    'Break
    'Menu
    'Mouse0
    'Mouse1
    'Mouse2
    'Mouse3
    'Mouse4
    'Mouse5
    'Mouse6

    'Alpha0
    'Alpha1
    'Alpha2
    'Alpha3
    'Alpha4
    'Alpha5
    'Alpha6
    'Alpha7
    'Alpha8
    'Alpha9


End Class