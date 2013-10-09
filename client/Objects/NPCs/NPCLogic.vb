Public Class NPCLogic
    Public Shared Function NpcOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 0 To NPCCount
            If Not IsNothing(NPC(i)) Then
                If (NPC(i).X = X And NPC(i).Y = Y) Then Return True
            End If
        Next
        Return False
    End Function
End Class
