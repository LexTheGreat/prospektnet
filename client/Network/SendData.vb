Class SendData
    Public Shared Sub Register(ByVal Name As String, ByVal Password As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CRegister)
        Buffer.WriteString(Name)
        Buffer.WriteString(Password)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
        loginSent = True
    End Sub

    Public Shared Sub NewCharacter(ByVal Login As String, ByVal Name As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CNewCharacter)
        Buffer.WriteString(Login)
        Buffer.WriteString(Name)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
        loginSent = True
    End Sub

    Public Shared Sub Login(ByVal Login As String, ByVal Password As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CLogin)
        Buffer.WriteString(Login)
        Buffer.WriteString(Password)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
        loginSent = True
    End Sub

    Public Shared Sub Position()
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
    Public Shared Sub Message(ByVal Message As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CMessage)
        Buffer.WriteString("[" & [Enum].GetName(GetType(ChatModes), chatMode) & "] ")
        Buffer.WriteString(Message)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub WarpTo(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CWarpTo)
        Buffer.WriteLong(index)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub WarpToMe(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ClientPackets.CWarpToMe)
        Buffer.WriteLong(index)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub
End Class
