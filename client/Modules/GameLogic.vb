Module GameLogic

    Function IsTryingToMove() As Boolean
        'If DirUp Or DirDown Or DirLeft Or DirRight Then
        If dirUp Or dirDown Or dirLeft Or dirRight Then
            IsTryingToMove = True
        End If
    End Function

    Function CanMove() As Boolean
        Dim tempX As Integer, tempY As Integer
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
            tempY = Player(MyIndex).Y - 1
            tempX = Player(MyIndex).X
        ElseIf dirDown Then
            Player(MyIndex).Dir = DirEnum.Down
            If Player(MyIndex).Y = maxY Then Return False
            tempY = Player(MyIndex).Y + 1
            tempX = Player(MyIndex).X
        ElseIf dirLeft Then
            Player(MyIndex).Dir = DirEnum.Left
            If Player(MyIndex).X = 0 Then Return False
            tempY = Player(MyIndex).Y
            tempX = Player(MyIndex).X - 1
        ElseIf dirRight Then
            Player(MyIndex).Dir = DirEnum.Right
            If Player(MyIndex).X = maxX Then Return False
            tempY = Player(MyIndex).Y
            tempX = Player(MyIndex).X + 1
        End If

        If PlayerOnTile(tempX, tempY) Then Return False
        If NpcOnTile(tempX, tempY) Then Return False
    End Function

    Public Function PlayerOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 1 To PlayerHighIndex
            If Not IsNothing(Player(i)) Then
                If (Player(i).X = X And Player(i).Y = Y) Then Return True
            End If
        Next
        Return False
    End Function

    Public Function NpcOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 0 To NPCCount
            If Not IsNothing(NPC(i)) Then
                If (NPC(i).X = X And NPC(i).Y = Y) Then Return True
            End If
        Next
        Return False
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

        'Move Up
        If GetKeyState(Keys.W) < 0 Or GetKeyState(Keys.Up) < 0 Then
            dirUp = True
            dirDown = False
            dirLeft = False
            dirRight = False
            Exit Sub
        Else
            dirUp = False
        End If

        'Move Down
        If GetKeyState(Keys.S) < 0 Or GetKeyState(Keys.Down) < 0 Then
            dirUp = False
            dirDown = True
            dirLeft = False
            dirRight = False
            Exit Sub
        Else
            dirDown = False
        End If

        'Move left
        If GetKeyState(Keys.A) < 0 Or GetKeyState(Keys.Left) < 0 Then
            dirUp = False
            dirDown = False
            dirLeft = True
            dirRight = False
            Exit Sub
        Else
            dirLeft = False
        End If

        'Move Right
        If GetKeyState(Keys.D) < 0 Or GetKeyState(Keys.Right) < 0 Then
            dirUp = False
            dirDown = False
            dirLeft = False
            dirRight = True
            Exit Sub
        Else
            dirRight = False
        End If
    End Sub

    Public Sub showMenu()
        inGame = False
        AudioPlayer.stopMusic()

        'AudioPlayer.playMusic(ClientConfig.MenuMusic)
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
        'AudioPlayer.playMusic(ClientConfig.GameMusic)
        inGame = True
        chatMode = ChatModes.SAY
        GMTools.Init()
        gameLoop()
    End Sub

    Public Function GetKeyState(ByVal key As Integer) As Boolean
        Dim s As Short
        s = GetAsyncKeyState(key)
        If s = 0 Then Return False
        Return True
    End Function

    Public Function EmailAddressChecker(ByVal emailAddress As String) As Boolean
        Dim regExPattern As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
        Dim emailAddressMatch As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(emailAddress, regExPattern)
        If emailAddressMatch.Success Then
            Return True
        Else
            Return False
        End If
    End Function
End Module
