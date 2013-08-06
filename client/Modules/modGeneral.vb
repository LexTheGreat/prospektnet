Module modGeneral
    Public Sub Main()
        screenWidth = 1024
        screenHeight = 768
        maxX = (screenWidth / 32) - 1
        maxY = (screenHeight / 32) - 1
        frmMain.Width = screenWidth + (SystemInformation.FrameBorderSize.Width * 2)
        frmMain.Height = screenHeight + SystemInformation.CaptionHeight + (SystemInformation.FrameBorderSize.Height * 2)
        frmMain.Show()
        InitSFML()
        Verdana = New TextWriter("verdana.ttf")
        Silkscreen = New TextWriter("silkscreen.ttf")
        inMenu = True
        playMusic("tranquility.ogg")
        ' fader
        faderAlpha = 255
        faderState = 0
        faderSpeed = 4
        canFade = True
        menuLoop()
    End Sub

    Public Sub showGame()
        inMenu = False
        Player = New clsPlayer(sUser, 1, maxX * 0.5, maxY * 0.5)
        inGame = True
        stopMusic()
        playMusic("touchthesky.ogg")
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

    Public Function GetTickCount()
        Return System.Environment.TickCount
    End Function
End Module
