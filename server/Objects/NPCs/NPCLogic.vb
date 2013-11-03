Public Class NPCLogic
    Public Function NpcOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 1 To NPCCount
            If Not IsNothing(NPC(i)) Then
                If (NPC(i).X = X And NPC(i).Y = Y) Then Return True
            End If
        Next
        Return False
    End Function

    Public Function CanNPCMove(ByVal Index As Integer, ByVal Dir As Integer) As Boolean
        Dim tempX As Integer, tempY As Integer
        ' Make sure they aren't trying to move when they are already moving
        If NPC(Index).Moving = True Then
            Return False
        End If

        Select Case Dir
            Case DirEnum.Up
                NPC(Index).Dir = DirEnum.Up
                If NPC(Index).Y = 0 Then Return False
                tempY = NPC(Index).Y - 1
                tempX = NPC(Index).X
            Case DirEnum.Down
                NPC(Index).Dir = DirEnum.Down
                tempY = NPC(Index).Y + 1
                tempX = NPC(Index).X
            Case DirEnum.Left
                NPC(Index).Dir = DirEnum.Left
                If NPC(Index).X = 0 Then Return False
                tempY = NPC(Index).Y
                tempX = NPC(Index).X - 1
            Case DirEnum.Right
                NPC(Index).Dir = DirEnum.Right
                tempY = NPC(Index).Y
                tempX = NPC(Index).X + 1
        End Select

        If Players.Logic.PlayerOnTile(tempX, tempY) Then Return False
        If NPCs.Logic.NpcOnTile(tempX, tempY) Then Return False
        If Tilesets.Data.isTileNPCBlocked(0, tempX, tempY) Then Return False
        Return True
    End Function
End Class
