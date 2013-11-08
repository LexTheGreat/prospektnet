Imports Prospekt.Input
Imports Prospekt.Network
Imports IHProspekt.Core
Public Class PlayerLogic
    Public Function IsTryingToMove() As Boolean
        'If DirUp Or DirDown Or DirLeft Or DirRight Then
        If dirUp Or dirDown Or dirLeft Or dirRight Then
            IsTryingToMove = True
        End If
    End Function

    Public Function CanMove() As Boolean
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
            If Player(MyIndex).Y = Map.MaxY Then Return False
            tempY = Player(MyIndex).Y + 1
            tempX = Player(MyIndex).X
        ElseIf dirLeft Then
            Player(MyIndex).Dir = DirEnum.Left
            If Player(MyIndex).X = 0 Then Return False
            tempY = Player(MyIndex).Y
            tempX = Player(MyIndex).X - 1
        ElseIf dirRight Then
            Player(MyIndex).Dir = DirEnum.Right
            If Player(MyIndex).X = Map.MaxX Then Return False
            tempY = Player(MyIndex).Y
            tempX = Player(MyIndex).X + 1
        End If

        If Players.Logic.PlayerOnTile(tempX, tempY) Then Return False
        If NPCs.Logic.NpcOnTile(tempX, tempY) Then Return False
        If Tilesets.Logic.isTileBlocked(tempX, tempY) Then Return False
    End Function

    Public Sub CheckMovement()
        If IsTryingToMove() Then
            If CanMove() Then
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
                SendData.Position()
            End If
        End If
    End Sub

    Public Function PlayerOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 1 To PlayerCount
            If Not IsNothing(Player(i)) Then
                If (Player(i).X = X And Player(i).Y = Y) Then Return True
            End If
        Next
        Return False
    End Function

    Public Sub CheckInputKeys()

        'Move Up
        If Keyboard.GetKeyState(Keys.W) < 0 Or Keyboard.GetKeyState(Keys.Up) < 0 Then
            dirUp = True
            dirDown = False
            dirLeft = False
            dirRight = False
            Exit Sub
        Else
            dirUp = False
        End If

        'Move Down
        If Keyboard.GetKeyState(Keys.S) < 0 Or Keyboard.GetKeyState(Keys.Down) < 0 Then
            dirUp = False
            dirDown = True
            dirLeft = False
            dirRight = False
            Exit Sub
        Else
            dirDown = False
        End If

        'Move left
        If Keyboard.GetKeyState(Keys.A) < 0 Or Keyboard.GetKeyState(Keys.Left) < 0 Then
            dirUp = False
            dirDown = False
            dirLeft = True
            dirRight = False
            Exit Sub
        Else
            dirLeft = False
        End If

        'Move Right
        If Keyboard.GetKeyState(Keys.D) < 0 Or Keyboard.GetKeyState(Keys.Right) < 0 Then
            dirUp = False
            dirDown = False
            dirLeft = False
            dirRight = True
            Exit Sub
        Else
            dirRight = False
        End If
    End Sub
End Class
