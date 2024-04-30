Public Class ButtonItems

    Public Class XboxButton
        Public Property XboxButtonName As String
        Public Property XboxButtonValue As String
    End Class


    Public Shared Function GetXboxButtons() As List(Of XboxButton)
        Dim result As New List(Of XboxButton)

        '' Face buttons
        result.Add(New XboxButton With {.XboxButtonName = "Xbox A", .XboxButtonValue = "joystick_button_0"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox B", .XboxButtonValue = "joystick_button_1"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox X", .XboxButtonValue = "joystick_button_2"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox Y", .XboxButtonValue = "joystick_button_3"})

        '' Shoulder buttons
        result.Add(New XboxButton With {.XboxButtonName = "Xbox LB", .XboxButtonValue = "joystick_button_4"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox RB", .XboxButtonValue = "joystick_button_5"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox LT", .XboxButtonValue = "Axis_9_P"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox RT", .XboxButtonValue = "Axis_10_P"})

        '' Analog buttons
        result.Add(New XboxButton With {.XboxButtonName = "Xbox LStick Button", .XboxButtonValue = "joystick_button_8"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox RStick Button", .XboxButtonValue = "joystick_button_9"})

        '' Left Stick
        result.Add(New XboxButton With {.XboxButtonName = "Xbox LStick Up", .XboxButtonValue = "Axis_2_N"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox LStick Down", .XboxButtonValue = "Axis_2_P"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox LStick Left", .XboxButtonValue = "Axis_1_N"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox LStick Right", .XboxButtonValue = "Axis_1_P"})

        '' Right Stick
        result.Add(New XboxButton With {.XboxButtonName = "Xbox RStick Up", .XboxButtonValue = "Axis_5_N"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox RStick Down", .XboxButtonValue = "Axis_5_P"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox RStick Left", .XboxButtonValue = "Axis_4_N"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox RStick Right", .XboxButtonValue = "Axis_4_P"})

        '' D pad
        result.Add(New XboxButton With {.XboxButtonName = "Xbox D Pad Up", .XboxButtonValue = "Axis_7_P"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox D Pad Down", .XboxButtonValue = "Axis_7_N"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox D Pad Left", .XboxButtonValue = "Axis_6_N"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox D Pad Right", .XboxButtonValue = "Axis_6_P"})

        ' Start and Back
        result.Add(New XboxButton With {.XboxButtonName = "Xbox Back/View", .XboxButtonValue = "joystick_button_6"})
        result.Add(New XboxButton With {.XboxButtonName = "Xbox Start/Menu", .XboxButtonValue = "joystick_button_7"})

        Return result
    End Function



    Public Shared Function GetPS_Buttons() As List(Of XboxButton)
        Dim result As New List(Of XboxButton)

        '' Face buttons
        result.Add(New XboxButton With {.XboxButtonName = "PS Cross", .XboxButtonValue = "joystick_button_1"})
        result.Add(New XboxButton With {.XboxButtonName = "PS Circle", .XboxButtonValue = "joystick_button_2"})
        result.Add(New XboxButton With {.XboxButtonName = "PS Triangle", .XboxButtonValue = "joystick_button_3"})
        result.Add(New XboxButton With {.XboxButtonName = "PS Square", .XboxButtonValue = "joystick_button_0"})

        '' Shoulder buttons
        result.Add(New XboxButton With {.XboxButtonName = "PS L1", .XboxButtonValue = "joystick_button_4"})
        result.Add(New XboxButton With {.XboxButtonName = "PS R1", .XboxButtonValue = "joystick_button_5"})
        result.Add(New XboxButton With {.XboxButtonName = "PS L2", .XboxButtonValue = "joystick_button_6"})
        result.Add(New XboxButton With {.XboxButtonName = "PS R2", .XboxButtonValue = "joystick_button_7"})

        '' Analog buttons
        result.Add(New XboxButton With {.XboxButtonName = "PS L3 (Left Stick Btn)", .XboxButtonValue = "joystick_button_10"})
        result.Add(New XboxButton With {.XboxButtonName = "PS R3 (Right Stick Btn)", .XboxButtonValue = "joystick_button_11"})

        '' Left Stick
        result.Add(New XboxButton With {.XboxButtonName = "PS LStick Up", .XboxButtonValue = "Axis_2_N"})
        result.Add(New XboxButton With {.XboxButtonName = "PS LStick Down", .XboxButtonValue = "Axis_2_P"})
        result.Add(New XboxButton With {.XboxButtonName = "PS LStick Left", .XboxButtonValue = "Axis_1_N"})
        result.Add(New XboxButton With {.XboxButtonName = "PS LStick Right", .XboxButtonValue = "Axis_1_P"})

        '' Right Stick
        result.Add(New XboxButton With {.XboxButtonName = "PS RStick Up", .XboxButtonValue = "Axis_6_N"})
        result.Add(New XboxButton With {.XboxButtonName = "PS RStick Down", .XboxButtonValue = "Axis_6_P"})
        result.Add(New XboxButton With {.XboxButtonName = "PS RStick Left", .XboxButtonValue = "Axis_3_N"})
        result.Add(New XboxButton With {.XboxButtonName = "PS RStick Right", .XboxButtonValue = "Axis_3_P"})

        '' D pad
        result.Add(New XboxButton With {.XboxButtonName = "PS D Pad Up", .XboxButtonValue = "Axis_8_P"})
        result.Add(New XboxButton With {.XboxButtonName = "PS D Pad Down", .XboxButtonValue = "Axis_8_N"})
        result.Add(New XboxButton With {.XboxButtonName = "PS D Pad Left", .XboxButtonValue = "Axis_7_N"})
        result.Add(New XboxButton With {.XboxButtonName = "PS D Pad Right", .XboxButtonValue = "Axis_7_P"})

        ' Start and Back
        result.Add(New XboxButton With {.XboxButtonName = "PS Select/Share", .XboxButtonValue = "joystick_button_8"})
        result.Add(New XboxButton With {.XboxButtonName = "PS Start/Options", .XboxButtonValue = "joystick_button_9"})

        Return result
    End Function
End Class
