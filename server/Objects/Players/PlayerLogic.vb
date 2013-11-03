Public Class PlayerLogic
    Public Function PlayerOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 1 To PlayerCount
            If Not IsNothing(Player(i)) Then
                If (Player(i).X = X And Player(i).Y = Y) Then Return True
            End If
        Next
        Return False
    End Function

    Public Function IsPlaying(ByVal index As Integer) As Boolean
        ' Checks if the player is online
        If Networking.IsConnected(index) Then
            If Not IsNothing(Player(index)) Then
                If Player(index).IsPlaying Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Public Function FindOpenPlayerSlot() As Integer
        Dim i As Integer
        For i = 1 To PlayerCount
            If Not Networking.IsConnected(i) Then
                Return i
                Exit Function
            End If
        Next i
        Return 0
    End Function
End Class
