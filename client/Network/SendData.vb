Class SendData
    Public Shared Sub Register(ByVal Name As String, ByVal Password As String)
        Dim Buffer as New ByteBuffer
        
        Buffer.WriteInteger(ClientPackets.Register)
        Buffer.WriteString(Name)
        Buffer.WriteString(Password)
        Networking.SendData(Buffer.ToArray())

        loginSent = True
    End Sub

    Public Shared Sub NewCharacter(ByVal Login As String, ByVal Name As String)
        Dim Buffer As New ByteBuffer

        Buffer.WriteInteger(ClientPackets.NewCharacter)
        Buffer.WriteString(Login)
        Buffer.WriteString(Name)
        Networking.SendData(Buffer.ToArray())

        loginSent = True
    End Sub

    Public Shared Sub Login(ByVal Login As String, ByVal Password As String)
        Dim Buffer As New ByteBuffer

        Buffer.WriteInteger(ClientPackets.Login)
        Buffer.WriteString(Login)
        Buffer.WriteString(Password)
        Networking.SendData(Buffer.ToArray())

        loginSent = True
    End Sub

    Public Shared Sub Position()
        Dim Buffer As New ByteBuffer

        Buffer.WriteInteger(ClientPackets.Position)
        Buffer.WriteInteger(Player(MyIndex).Moving)
        Buffer.WriteInteger(Player(MyIndex).X)
        Buffer.WriteInteger(Player(MyIndex).Y)
        Buffer.WriteInteger(Player(MyIndex).Dir)
        Networking.SendData(Buffer.ToArray())

    End Sub
    Public Shared Sub Message(ByVal Message As String)
        Dim Buffer As New ByteBuffer

        Buffer.WriteInteger(ClientPackets.Message)
        Buffer.WriteString("[" & [Enum].GetName(GetType(ChatModes), chatMode) & "] ")
        Buffer.WriteString(Message)
        Networking.SendData(Buffer.ToArray())

    End Sub

    Public Shared Sub WarpTo(ByVal index As Long)
        Dim Buffer As New ByteBuffer

        Buffer.WriteInteger(ClientPackets.WarpTo)
        Buffer.WriteInteger(index)
        Networking.SendData(Buffer.ToArray())

    End Sub

    Public Shared Sub WarpToMe(ByVal index As Long)
        Dim Buffer As New ByteBuffer

        Buffer.WriteInteger(ClientPackets.WarpToMe)
        Buffer.WriteInteger(index)
        Networking.SendData(Buffer.ToArray())

    End Sub
End Class
