Public Class HelpSectionClass
    Public Function GetHelpSectionText() As String

        Dim helpText As String = $"The Relayer Action Mapper supports command line arguments. You can run these arguments by adding them in a batch (.bat) file. See below for available command line arguments.

**Commands**

-ps
Description: Sets Relayer Action Mapper to ""PlayStation"" mode. This is the mode that changes the button icons and game pad options on the ""Edit Controls screen"".
Usage: Relayer Action Mapper.exe -ps

-overridesave
Description: Checks the ""Always Override File"" checkbox. When checking this checkbox, the ""Save File"" button won't ask you if you want to override your config file.
Usage: Relayer Action Mapper.exe -overridesave

-load
Description: Immediately loads your config file, if the file exist. If the file path of your file contains a space, you must surround it with double quotes.
Usage: Relayer Action Mapper.exe -load ""C:\My Folder\KeyConfig.json""

-save
Description: Sets the default directory for saving your config file. Like the ""-load"" command, you will need to surround the directory path with double quotes if it contains spaces.
Usage: Relayer Action Mapper.exe -save ""C:\My Folder\Keys\""

**Additional Information**
You can use none, one, or a combonation of these command line arguments together.
Example: 
Relayer Action Mapper.exe -ps -load ""C:\My Folder\KeyConfig.json"" -save ""C:\My Folder\Keys\"" -overridesave
"
        Return helpText

    End Function
End Class
