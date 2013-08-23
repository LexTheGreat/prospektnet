Module SendData
    Public Sub SendLoginOk(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SLoginOk)
        Buffer.WriteLong(index)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub
    Public Sub SendPlayers()
        Dim i As Integer
        For i = 1 To PlayerHighIndex
            SendPlayer(i)
        Next
    End Sub
    Public Sub SendPlayer(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SPlayer)
        Buffer.WriteLong(index)
        Buffer.WriteLong(PlayerHighIndex)
        Buffer.WriteString(Player(index).Name)
        Buffer.WriteLong(Player(index).Sprite)
        Buffer.WriteLong(Player(index).X)
        Buffer.WriteLong(Player(index).Y)
        Buffer.WriteLong(Player(index).Dir)
        Networking.SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub
    Public Sub SendClearPlayer(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SClearPlayer)
        Buffer.WriteLong(index)
        Buffer.WriteLong(PlayerHighIndex)
        Networking.SendDataToAllBut(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub SendPosition(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SPosition)
        Buffer.WriteLong(index)
        Buffer.WriteLong(Player(index).Moving)
        Buffer.WriteLong(Player(index).X)
        Buffer.WriteLong(Player(index).Y)
        Buffer.WriteLong(Player(index).Dir)
        Networking.SendDataToAllBut(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub SendMessage(ByVal Message As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SMessage)
        Buffer.WriteString(Message)
        Networking.SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub
End Module
