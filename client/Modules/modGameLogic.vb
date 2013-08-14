Module modGameLogic
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

            renderMenu()

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
                If frmMain.Focused Then CheckInputKeys() ' Check which keys were pressed

                CheckMovement() ' Check if player is trying to move
                tmr25 = Tick + 25
            End If

            ' Process input before rendering, otherwise input will be behind by 1 frame
            If walkTimer < Tick Then
                For I = 1 To PlayerHighindex
                    If Not IsNothing(Player(I)) Then Player(I).ProcessMovement()
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

            renderGame()

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

    Function IsTryingToMove() As Boolean
        'If DirUp Or DirDown Or DirLeft Or DirRight Then
        If dirUp Or dirDown Or dirLeft Or dirRight Then
            IsTryingToMove = True
        End If
    End Function

    Function CanMove() As Boolean
        CanMove = True
        ' Make sure they aren't trying to move when they are already moving
        If Player(MyIndex).Moving = True Then
            Return False
        End If

        If inChat Then
            Return False
        End If

        If dirUp Then
            Player(MyIndex).Dir = DirEnum.Up
            If Player(MyIndex).Y = 0 Then Return False
        ElseIf dirDown Then
            Player(MyIndex).Dir = DirEnum.Down
            If Player(MyIndex).Y = maxY Then Return False
        ElseIf dirLeft Then
            Player(MyIndex).Dir = DirEnum.Left
            If Player(MyIndex).X = 0 Then Return False
        ElseIf dirRight Then
            Player(MyIndex).Dir = DirEnum.Right
            If Player(MyIndex).X = maxX Then Return False
        End If

    End Function

    Sub CheckMovement()
        If IsTryingToMove Then
            If CanMove Then
                Player(MyIndex).Moving = True

                Select Case Player(MyIndex).Dir
                    Case DirEnum.Up
                        Player(MyIndex).YOffset = picY
                        Player(MyIndex).Y = Player(MyIndex).Y - 1
                    Case DirEnum.Down
                        Player(MyIndex).YOffset = picY * -1
                        Player(MyIndex).Y = Player(MyIndex).Y + 1
                    Case DirEnum.Left
                        Player(MyIndex).XOffset = picX
                        Player(MyIndex).X = Player(MyIndex).X - 1
                    Case DirEnum.Right
                        Player(MyIndex).XOffset = picX * -1
                        Player(MyIndex).X = Player(MyIndex).X + 1
                End Select
                SendPosition()
            End If
        End If
    End Sub

    Public Sub CheckInputKeys()

        ' move up
        If GetKeyState(Keys.W) < 0 Then
            dirUp = True
            dirDown = False
            dirLeft = False
            dirRight = False
            Exit Sub
        Else
            dirUp = False
        End If

        'Move Right
        If GetKeyState(Keys.D) < 0 Then
            dirUp = False
            dirDown = False
            dirLeft = False
            dirRight = True
            Exit Sub
        Else
            dirRight = False
        End If

        'Move down
        If GetKeyState(Keys.S) < 0 Then
            dirUp = False
            dirDown = True
            dirLeft = False
            dirRight = False
            Exit Sub
        Else
            dirDown = False
        End If

        'Move left
        If GetKeyState(Keys.A) < 0 Then
            dirUp = False
            dirDown = False
            dirLeft = True
            dirRight = False
            Exit Sub
        Else
            dirLeft = False
        End If
    End Sub
End Module
