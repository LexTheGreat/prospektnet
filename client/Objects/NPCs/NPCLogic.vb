Public Class NPCLogic
    Public Function NpcOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 1 To NPCCount
            If Not IsNothing(NPC(i)) Then
                If (NPC(i).X = X And NPC(i).Y = Y) Then Return True
            End If
        Next
        Return False
    End Function
End Class
