Module SendData
    Public Sub SendAlert(ByVal index As Long, ByVal Message As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SAlert)
        Buffer.WriteString(Message)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub SendRegisterOk(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SRegisterOk)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

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
        Buffer.WriteLong(Player(index).GuildID)
        Buffer.WriteLong(Player(index).GetParty)
        Buffer.WriteLong(Player(index).AccessMode)
        Buffer.WriteLong(Player(index).Visible)
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

    Public Sub SendPosition(ByVal index As Long, Optional ByVal UpdateOwn As Boolean = False)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SPosition)
        Buffer.WriteLong(index)
        Buffer.WriteLong(Player(index).GetMoving)
        Buffer.WriteLong(Player(index).X)
        Buffer.WriteLong(Player(index).Y)
        Buffer.WriteLong(Player(index).Dir)
        If Not UpdateOwn Then
            Networking.SendDataToAllBut(index, Buffer.ToArray())
        Else
            Networking.SendDataToAll(Buffer.ToArray())
        End If
        Buffer = Nothing
    End Sub

    Public Sub SendAccess(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SAccess)
        Buffer.WriteLong(Player(index).AccessMode)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub SendVisible(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SVisible)
        Buffer.WriteLong(index)
        Buffer.WriteBool(Player(index).Visible)
        Networking.SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub SendMessage(ByVal Message As String, Optional ByVal ChatMode As String = vbNullString)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SMessage)
        Buffer.WriteString(Message)

        If ChatMode = vbNullString Then
            Networking.SendDataToAll(Buffer.ToArray())
        ElseIf (ChatMode = "[" & ChatModes.GM & "] ") Then
            Networking.SendDataToAdmins(Buffer.ToArray())
        End If
        Buffer = Nothing
    End Sub
End Module
