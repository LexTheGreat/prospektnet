Module General
    Public Sub Main()
        ClientConfig = New Configuration
        ClientConfig.Load()
        ClientConfig.Load()
        maxX = (ClientConfig.ScreenWidth / 32) - 1
        maxY = (ClientConfig.ScreenHeight / 32) - 1
        GameWindow.Width = ClientConfig.ScreenWidth + (16)
        GameWindow.Height = ClientConfig.ScreenHeight + SystemInformation.CaptionHeight + (16)
        GameWindow.Show()
        Networking.Initialize()
        Render.Initialize()
        Verdana = New TextWriter("content/fonts/Verdana.ttf")
        showMenu()
    End Sub

    Public Sub menuLoop()
        Dim FrameTime As Integer
        Dim Tick As Integer
        Dim TickFPS As Integer
        Dim FPS As Integer

        Dim tmr500 As Integer, faderTimer As Integer, tmr15 As Integer

        Do While inMenu = True
            Tick = System.Environment.TickCount() ' Set the inital tick
            ElapsedTime = Tick - FrameTime ' Set the time difference for time-based movement
            FrameTime = Tick

            If tmr500 < Tick Then
                ' animate textbox
                If chatShowLine = "|" Then
                    chatShowLine = vbNullString
                Else
                    chatShowLine = "|"
                End If
                tmr500 = System.Environment.TickCount() + 500
            End If

            If tmr15 < Tick Then
                If canFade Then
                    If faderTimer = 0 Then
                        Select Case faderState
                            Case 0, 2 ' fading in
                                If faderAlpha <= 0 Then
                                    faderTimer = Tick + 1000
                                Else
                                    ' fade out a bit
                                    If faderSpeed <= faderAlpha Then
                                        faderAlpha = faderAlpha - faderSpeed
                                    Else
                                        faderAlpha = 0
                                    End If
                                End If
                            Case 1, 3, 4 ' fading out
                                If faderAlpha >= 254 Then
                                    If faderState = 4 Then
                                        ' fading out to game - make game load during fade
                                        showGame()
                                        Exit Sub
                                    ElseIf faderState < 2 Then
                                        faderState = faderState + 1
                                    ElseIf faderState = 3 Then
                                        faderState = faderState + 1
                                    End If
                                Else
                                    ' fade in a bit
                                    If faderAlpha <= 255 - faderSpeed Then
                                        faderAlpha = faderAlpha + faderSpeed
                                    Else
                                        faderAlpha = 255
                                    End If
                                End If
                        End Select
                    Else
                        If faderTimer < Tick Then
                            If faderState < 2 Then
                                faderState = faderState + 1
                                faderTimer = 0
                            Else
                                faderTimer = 0
                            End If
                            If faderState = 3 Then
                                faderState = faderState + 1
                                faderTimer = 0
                            Else
                                faderTimer = 0
                            End If
                        End If
                    End If
                End If
                tmr15 = System.Environment.TickCount() + 15
            End If


            MenuScene.Draw()

            ' Calculate fps
            If TickFPS < Tick Then
                gameFPS = FPS
                TickFPS = Tick + 1000
                FPS = 0
            Else
                FPS = FPS + 1
            End If
            Application.DoEvents()
        Loop
    End Sub

    Public Sub gameLoop()
        Dim FrameTime As Integer
        Dim Tick As Integer
        Dim TickFPS As Integer
        Dim FPS As Integer
        Dim tmr25 As Integer, walkTimer As Integer, tmr500 As Integer
        Dim I As Long

        Do While inGame = True
            Tick = System.Environment.TickCount() ' Set the inital tick
            elapsedTime = Tick - FrameTime ' Set the time difference for time-based movement
            FrameTime = Tick

            If tmr25 < Tick Then
                If GameWindow.Focused Then CheckInputKeys() ' Check which keys were pressed

                CheckMovement() ' Check if player is trying to move
                tmr25 = Tick + 25
            End If

            ' Process input before rendering, otherwise input will be behind by 1 frame
            If walkTimer < Tick Then
                For I = 1 To PlayerHighindex
                    If Not IsNothing(Player(I)) Then Player(I).ProcessMovement()
                Next I
                For I = 0 To NPCCount
                    If Not IsNothing(NPC(I)) Then NPC(I).ProcessMovement()
                Next I
                walkTimer = Tick + 30 ' edit this value to change WalkTimer
            End If

            If tmr500 < Tick Then
                ' animate textbox
                If chatShowLine = "|" Then
                    chatShowLine = vbNullString
                Else
                    chatShowLine = "|"
                End If
                tmr500 = System.Environment.TickCount() + 500
            End If

            GameScene.Draw()

            ' Calculate fps
            If TickFPS < Tick Then
                gameFPS = FPS
                TickFPS = Tick + 1000
                FPS = 0
            Else
                FPS = FPS + 1
            End If
            Application.DoEvents()
        Loop
    End Sub

End Module
