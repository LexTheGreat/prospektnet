Module modGeneral
    Public Sub Main()
        loadOptions()
        maxX = (ClientConfig.screenWidth / 32) - 1
        maxY = (ClientConfig.screenHeight / 32) - 1
        frmMain.Width = ClientConfig.screenWidth + (16)
        frmMain.Height = ClientConfig.screenHeight + SystemInformation.CaptionHeight + (16)
        frmMain.Show()
        TcpInit()
        InitSFML()
        Verdana = New TextWriter("content/fonts/verdana.ttf")
        Silkscreen = New TextWriter("content/fonts/silkscreen.ttf")
        showMenu()
    End Sub

    Public Sub showMenu()
        inGame = False
        stopMusic()
        playMusic(ClientConfig.MenuMusic)
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
        stopMusic()
        playMusic(ClientConfig.GameMusic)
        inGame = True
        gameLoop()
    End Sub

    Public Function GetKeyState(ByVal key As Integer) As Boolean
        Dim s As Short
        s = GetAsyncKeyState(key)
        If s = 0 Then Return False
        Return True
    End Function

    Public Function fileExist(ByVal filepath As String, Optional ByVal Raw As Boolean = False) As Boolean
        If Raw = True Then
            fileExist = System.IO.File.Exists(filepath)
        Else
            fileExist = System.IO.File.Exists(Application.StartupPath & "/" & filepath)
        End If
    End Function
End Module
