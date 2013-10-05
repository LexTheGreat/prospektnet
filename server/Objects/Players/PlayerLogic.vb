Public Class PlayerLogic
    Public Shared Function PlayerOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 1 To PlayerHighIndex
            If Not IsNothing(Player(i)) Then
                If (Player(i).X = X And Player(i).Y = Y) Then Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function IsConnected(ByVal index As Long) As Boolean
        If Networking.Clients(index).Socket.State = Winsock_Orcas.WinsockStates.Connected Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function IsPlaying(ByVal index As Long) As Boolean
        ' Checks if the player is online
        If IsConnected(index) Then
            If Not IsNothing(Player(index)) Then
                If Player(index).GetIsPlaying Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Public Shared Function GetPlayerIP(ByVal index As Long) As String
        Return Networking.Clients(index).IP
    End Function

    Public Shared Function FindOpenPlayerSlot() As Long
        Dim i As Long
        For i = 1 To PlayerHighIndex
            If Not IsConnected(i) Then
                Return i
                Exit Function
            End If
        Next i
        Return 0
    End Function

    Public Shared Sub UpdateHighIndex()
        Dim i As Long
        PlayerHighIndex = 0
        For i = 100 To 1 Step -1
            If PlayerLogic.IsConnected(i) Then
                PlayerHighIndex = i
                Exit Sub
            End If
        Next i
    End Sub
End Class
