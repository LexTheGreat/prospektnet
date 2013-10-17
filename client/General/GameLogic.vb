Module GameLogic
    Public Sub showMenu()
        inGame = False
        AudioPlayer.stopMusic()

        AudioPlayer.playMusic(ClientConfig.MenuMusic)
        ' fader
        faderAlpha = 255
        faderState = 0
        faderSpeed = 4
        canFade = True
        inMenu = True
        menuLoop()
    End Sub

    Public Sub showGame()
        inMenu = False
        AudioPlayer.stopMusic()
        AudioPlayer.playMusic(ClientConfig.GameMusic)
        inGame = True
        chatMode = ChatModes.SAY
        GMTools.Init()
        gameLoop()
    End Sub

    Public Function EmailAddressChecker(ByVal emailAddress As String) As Boolean
        Dim regExPattern As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
        Dim emailAddressMatch As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(emailAddress, regExPattern)
        If emailAddressMatch.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function WordWarp(ByVal str As String, ByVal numOfChar As Integer) As String()
        Dim sArr() As String
        Dim nCount As Integer
        Dim separators() As String = {",", ".", "!", "?", ";", ":", " "}
        Dim nSpace() As String = str.Split(separators, StringSplitOptions.RemoveEmptyEntries)
        ReDim sArr(Len(str) \ numOfChar)
        Do While Len(str)
            sArr(nCount) = Left$(str, numOfChar)
            str = Mid$(str, numOfChar + 1)
            nCount = nCount + 1
        Loop
        WordWarp = sArr
    End Function

    Public Function ResizeArray(arr As Array, newSizes() As Integer) As Array
        If newSizes.Length <> arr.Rank Then
            Throw New ArgumentException()
        End If

        Dim temp As Array = Array.CreateInstance(arr.GetType().GetElementType(), newSizes)
        Dim length As Integer = If(arr.Length <= temp.Length, arr.Length, temp.Length)
        Array.ConstrainedCopy(arr, 0, temp, 0, length)
        Return temp
    End Function
End Module
