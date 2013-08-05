Module modGameLogic
    Public Sub menuLoop()
        Dim FrameTime As Integer
        Dim Tick As Integer
        Dim TickFPS As Integer
        Dim FPS As Integer

        Dim tmr500 As Integer, faderTimer As Integer, tmr15 As Integer

        Do While inMenu = True
            Tick = GetTickCount() ' Set the inital tick
            ElapsedTime = Tick - FrameTime ' Set the time difference for time-based movement
            FrameTime = Tick

            If tmr500 < Tick Then
                ' animate textbox
                If chatShowLine = "|" Then
                    chatShowLine = vbNullString
                Else
                    chatShowLine = "|"
                End If
                tmr500 = GetTickCount() + 500
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
                            Case 1 ' fading out
                                If faderAlpha >= 254 Then
                                    faderState = faderState + 1
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
                            If faderState < 3 Then
                                faderState = faderState + 1
                                faderTimer = 0
                            Else
                                faderTimer = 0
                            End If
                        End If
                    End If
                End If
                tmr15 = GetTickCount() + 15
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

        Dim tmr25 As Integer, walkTimer As Integer

        Do While inGame = True
            Tick = GetTickCount() ' Set the inital tick
            elapsedTime = Tick - FrameTime ' Set the time difference for time-based movement
            FrameTime = Tick

            If tmr25 < Tick Then
                CheckInputKeys() ' Check which keys were pressed

                CheckMovement() ' Check if player is trying to move
                tmr25 = Tick + 25
            End If

            ' Process input before rendering, otherwise input will be behind by 1 frame
            If walkTimer < Tick Then
                ProcessMovement()
                walkTimer = Tick + 30 ' edit this value to change WalkTimer
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

    Sub ProcessMovement()
        Dim MovementSpeed As Long

        If Player.Moving = True Then
            MovementSpeed = 2
        Else
            Exit Sub
        End If

        Select Case Player.Dir
            Case DirEnum.Up
                Player.YOffset = Player.YOffset - MovementSpeed
                If Player.YOffset < 0 Then Player.YOffset = 0
            Case DirEnum.Down
                Player.YOffset = Player.YOffset + MovementSpeed
                If Player.YOffset > 0 Then Player.YOffset = 0
            Case DirEnum.Left
                Player.XOffset = Player.XOffset - MovementSpeed
                If Player.XOffset < 0 Then Player.XOffset = 0
            Case DirEnum.Right
                Player.XOffset = Player.XOffset + MovementSpeed
                If Player.XOffset > 0 Then Player.XOffset = 0
        End Select

        ' Check if completed walking over to the next tile
        If Player.Moving = True Then
            If Player.Dir = DirEnum.Right Or Player.Dir = DirEnum.Down Then
                If (Player.XOffset >= 0) And (Player.YOffset >= 0) Then
                    Player.Moving = False
                    If Player.PlayerStep = 0 Then
                        Player.PlayerStep = 2
                    Else
                        Player.PlayerStep = 0
                    End If
                End If
            Else
                If (Player.XOffset <= 0) And (Player.YOffset <= 0) Then
                    Player.Moving = False
                    If Player.PlayerStep = 0 Then
                        Player.PlayerStep = 2
                    Else
                        Player.PlayerStep = 0
                    End If
                End If
            End If
        End If
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
        If Player.Moving = True Then
            CanMove = False
            Exit Function
        End If

        If dirUp Then
            Player.Dir = DirEnum.Up
            If Player.Y = 0 Then CanMove = False
        ElseIf dirDown Then
            Player.Dir = DirEnum.Down
            If Player.Y = maxY Then CanMove = False
        ElseIf dirLeft Then
            Player.Dir = DirEnum.Left
            If Player.X = 0 Then CanMove = False
        ElseIf dirRight Then
            Player.Dir = DirEnum.Right
            If Player.X = maxX Then CanMove = False
        End If

    End Function

    Sub CheckMovement()
        If IsTryingToMove Then
            If CanMove Then
                Player.Moving = True

                Select Case Player.Dir
                    Case DirEnum.Up
                        Player.YOffset = picY
                        Player.Y = Player.Y - 1
                    Case DirEnum.Down
                        Player.YOffset = picY * -1
                        Player.Y = Player.Y + 1
                    Case DirEnum.Left
                        Player.XOffset = picX
                        Player.X = Player.X - 1
                    Case DirEnum.Right
                        Player.XOffset = picX * -1
                        Player.X = Player.X + 1
                End Select
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
