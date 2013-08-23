Module SendData
    Public Sub SendLogin(ByVal Name As String, ByVal Password As String)
        Dim Buffer As ByteBuffer

        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CLogin)
        Buffer.WriteString(Name)
        Buffer.WriteString(Password)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub SendPosition()
        Dim Buffer As ByteBuffer

        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CPosition)
        Buffer.WriteLong(Player(MyIndex).Moving)
        Buffer.WriteLong(Player(MyIndex).X)
        Buffer.WriteLong(Player(MyIndex).Y)
        Buffer.WriteLong(Player(MyIndex).Dir)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub
    Public Sub SendMessage(ByVal Message As String)
        Dim Buffer As ByteBuffer

        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CMessage)
        Buffer.WriteString(Message)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub
End Module
