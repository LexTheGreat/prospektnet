Module modGeneral
    Public Sub Main()
        screenWidth = 800
        screenHeight = 600
        maxX = (screenWidth / 32) - 1
        maxY = (screenHeight / 32) - 1
        frmMain.Width = screenWidth + (SystemInformation.FrameBorderSize.Width * 2)
        frmMain.Height = screenHeight + SystemInformation.CaptionHeight + (SystemInformation.FrameBorderSize.Height * 2)
        frmMain.Show()
        InitDirectDraw()
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
        Verdana8.Dispose()
        Verdana20.Dispose()
        Player = New clsPlayer(sUser, 1, maxX * 0.5, maxY * 0.5)
        inGame = True
        Verdana8 = New TextWriter(DirectDevice, New Drawing.Font("Verdana", 8))
        Verdana20 = New TextWriter(DirectDevice, New Drawing.Font("Verdana", 20))
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

    Public Function fileExist(ByVal filepath) As Boolean
        fileExist = System.IO.File.Exists(filepath)
    End Function

    Public Function GetTickCount()
        Return System.Environment.TickCount
    End Function
End Module
