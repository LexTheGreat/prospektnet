Class SendData
    Public Shared Sub Alert(ByVal index As Long, ByVal Message As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SAlert)
        Buffer.WriteString(Message)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub RegisterOk(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SRegisterOk)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub LoginOk(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SLoginOk)
        Buffer.WriteLong(index)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub Players()
        Dim i As Integer
        For i = 1 To PlayerHighIndex
            SendData.PlayerData(i)
        Next
    End Sub

    Public Shared Sub PlayerData(ByVal index As Long)
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

    Public Shared Sub ClearPlayer(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SClearPlayer)
        Buffer.WriteLong(index)
        Buffer.WriteLong(PlayerHighIndex)
        Networking.SendDataToAllBut(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub Position(ByVal index As Long, Optional ByVal UpdateOwn As Boolean = False)
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

    Public Shared Sub Access(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SAccess)
        Buffer.WriteLong(Player(index).AccessMode)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub Visible(ByVal index As Long)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SVisible)
        Buffer.WriteLong(index)
        Buffer.WriteBool(Player(index).Visible)
        Networking.SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub Message(ByVal Message As String, Optional ByVal ChatMode As String = vbNullString)
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

    Public Shared Sub MessageTo(ByVal Index As Integer, ByVal Message As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SMessage)
        Buffer.WriteString(Message)
        Networking.SendDataTo(Index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub MessageToAdmins(ByVal Message As String)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SMessage)
        Buffer.WriteString(Message)
        Networking.SendDataToAdmins(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub NPCs()
        Dim i As Integer
        For i = 0 To NPCCount
            If Not IsNothing(NPC(i)) Then
                SendData.NPCData(i)
            End If
        Next
    End Sub

    Public Shared Sub NPCData(ByVal Index As Integer)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SNPC)
        Buffer.WriteLong(Index)
        Buffer.WriteLong(NPCCount)
        Buffer.WriteString(NPC(Index).Name)
        Buffer.WriteLong(NPC(Index).Sprite)
        Buffer.WriteLong(NPC(Index).X)
        Buffer.WriteLong(NPC(Index).Y)
        Buffer.WriteLong(NPC(Index).Dir)
        Networking.SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Shared Sub NPCPosition(ByVal Index As Integer)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteLong(ServerPackets.SNPCPosition)
        Buffer.WriteLong(Index)
        Buffer.WriteLong(NPC(Index).GetMoving)
        Buffer.WriteLong(NPC(Index).X)
        Buffer.WriteLong(NPC(Index).Y)
        Buffer.WriteLong(NPC(Index).Dir)
        Networking.SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub
End Class
