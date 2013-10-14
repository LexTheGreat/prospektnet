Class SendData
    Public Shared Sub Alert(ByVal index As Long, ByVal Message As String)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.Alert)
        Buffer.WriteString(Message)
        Networking.SendDataTo(index, Buffer.ToArray())
    End Sub

    Public Shared Sub RegisterOk(ByVal index As Long)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.RegisterOk)
        Networking.SendDataTo(index, Buffer.ToArray())
    End Sub

    Public Shared Sub LoginOk(ByVal index As Long)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.LoginOk)
        Buffer.WriteInteger(index)
        Networking.SendDataTo(index, Buffer.ToArray())
    End Sub

    Public Shared Sub PlayerData(ByVal index As Long)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.Player)
        Buffer.WriteInteger(index)
        Buffer.WriteInteger(PlayerHighIndex)
        Buffer.WriteString(Player(index).Name)
        Buffer.WriteInteger(Player(index).Sprite)
        Buffer.WriteInteger(Player(index).X)
        Buffer.WriteInteger(Player(index).Y)
        Buffer.WriteInteger(Player(index).Dir)
        Buffer.WriteInteger(Player(index).AccessMode)
        Buffer.WriteInteger(Player(index).Visible)
        Networking.SendDataToAll(Buffer.ToArray())
    End Sub

    Public Shared Sub ClearPlayer(ByVal index As Long)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.ClearPlayer)
        Buffer.WriteInteger(index)
        Buffer.WriteInteger(PlayerHighIndex)
        Networking.SendDataToAllBut(index, Buffer.ToArray())
    End Sub

    Public Shared Sub Position(ByVal index As Long, Optional ByVal UpdateOwn As Boolean = False)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.Position)
        Buffer.WriteInteger(index)
        Buffer.WriteInteger(Player(index).GetMoving)
        Buffer.WriteInteger(Player(index).X)
        Buffer.WriteInteger(Player(index).Y)
        Buffer.WriteInteger(Player(index).Dir)
        If Not UpdateOwn Then
            Networking.SendDataToAllBut(index, Buffer.ToArray())
        Else
            Networking.SendDataToAll(Buffer.ToArray())
        End If
    End Sub

    Public Shared Sub Access(ByVal index As Long)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.Access)
        Buffer.WriteInteger(Player(index).AccessMode)
        Networking.SendDataTo(index, Buffer.ToArray())
    End Sub

    Public Shared Sub Visible(ByVal index As Long)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.Visible)
        Buffer.WriteInteger(index)
        Buffer.WriteBool(Player(index).Visible)
        Networking.SendDataToAll(Buffer.ToArray())
    End Sub

    Public Shared Sub Message(ByVal Message As String, Optional ByVal ChatMode As String = vbNullString)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.Message)
        Buffer.WriteString(Message)

        If ChatMode = vbNullString Then
            Networking.SendDataToAll(Buffer.ToArray())
        ElseIf (ChatMode = "[" & ChatModes.GM & "] ") Then
            Networking.SendDataToAdmins(Buffer.ToArray())
        End If
    End Sub

    Public Shared Sub MessageTo(ByVal Index As Integer, ByVal Message As String)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.Message)
        Buffer.WriteString(Message)
        Networking.SendDataTo(Index, Buffer.ToArray())
    End Sub

    Public Shared Sub MessageToAdmins(ByVal Message As String)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.Message)
        Buffer.WriteString(Message)
        Networking.SendDataToAdmins(Buffer.ToArray())
    End Sub

    Public Shared Sub NPCData(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.NPC)
        Buffer.WriteInteger(Index)
        Buffer.WriteInteger(NPCCount)
        Buffer.WriteString(NPC(Index).Name)
        Buffer.WriteInteger(NPC(Index).Sprite)
        Buffer.WriteInteger(NPC(Index).X)
        Buffer.WriteInteger(NPC(Index).Y)
        Buffer.WriteInteger(NPC(Index).Dir)
        Networking.SendDataToAll(Buffer.ToArray())
    End Sub

    Public Shared Sub NPCPosition(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(ServerPackets.NPCPosition)
        Buffer.WriteInteger(Index)
        Buffer.WriteInteger(NPC(Index).GetMoving)
        Buffer.WriteInteger(NPC(Index).X)
        Buffer.WriteInteger(NPC(Index).Y)
        Buffer.WriteInteger(NPC(Index).Dir)
        Networking.SendDataToAll(Buffer.ToArray())
    End Sub

    Public Shared Sub MapData(ByVal Index As Long)
        Dim Buffer As New ByteBuffer
        Dim sMap As MapStructure = Map(Player(Index).Map)
        Dim sTileData As New TileData

        Buffer.WriteInteger(ServerPackets.MapData)
        Buffer.WriteString(sMap.Name)
        Buffer.WriteInteger(sMap.MaxX)
        Buffer.WriteInteger(sMap.MaxY)
        Buffer.WriteInteger(sMap.Alpha)
        Buffer.WriteInteger(sMap.Red)
        Buffer.WriteInteger(sMap.Green)
        Buffer.WriteInteger(sMap.Blue)
        For I As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
            For x As Integer = 0 To sMap.MaxX - 1
                For y As Integer = 0 To sMap.MaxY - 1
                    sTileData = sMap.Layer(I).GetTileData(x, y)
                    Buffer.WriteInteger(sTileData.Tileset)
                    Buffer.WriteInteger(sTileData.X)
                    Buffer.WriteInteger(sTileData.Y)
                Next
            Next
        Next
        Networking.SendDataTo(Index, Buffer.ToArray())
    End Sub

    ' Editor Packets
    Public Shared Sub EditorLoginOk(ByVal index As Long, ByVal Mode As Byte)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(SEditorPackets.LoginOk)
        Buffer.WriteInteger(Mode)
        Networking.SendDataTo(index, Buffer.ToArray())
    End Sub

    Public Shared Sub EditorData(ByVal Index As Long)
        Dim Buffer As New ByteBuffer
        Dim sTileData As New TileData

        Buffer.WriteInteger(SEditorPackets.Data)
        Buffer.WriteInteger(Account.Length)
        For Each sPlayer In Account
            If IsNothing(sPlayer) Then Continue For
            Buffer.WriteString(sPlayer.Email)
            Buffer.WriteString(sPlayer.Password)
            Buffer.WriteString(sPlayer.Player.Name)
            Buffer.WriteInteger(sPlayer.Player.Sprite)
            Buffer.WriteInteger(sPlayer.Player.Map)
            Buffer.WriteInteger(sPlayer.Player.X)
            Buffer.WriteInteger(sPlayer.Player.Y)
            Buffer.WriteInteger(sPlayer.Player.Dir)
            Buffer.WriteInteger(sPlayer.Player.AccessMode)
        Next

        Buffer.WriteInteger(Map.Length - 1)
        For Each sMap In Map
            If IsNothing(sMap) Then Continue For
            Buffer.WriteString(sMap.Name)
            Buffer.WriteInteger(sMap.MaxX)
            Buffer.WriteInteger(sMap.MaxY)
            Buffer.WriteInteger(sMap.Alpha)
            Buffer.WriteInteger(sMap.Red)
            Buffer.WriteInteger(sMap.Green)
            Buffer.WriteInteger(sMap.Blue)
            For l As Integer = MapLayerEnum.Ground To MapLayerEnum.FringeMask
                For x As Integer = 0 To sMap.MaxX - 1
                    For y As Integer = 0 To sMap.MaxY - 1
                        sTileData = sMap.Layer(l).GetTileData(x, y)
                        Buffer.WriteInteger(sTileData.Tileset)
                        Buffer.WriteInteger(sTileData.X)
                        Buffer.WriteInteger(sTileData.Y)
                    Next
                Next
            Next
        Next
        Networking.SendDataTo(Index, Buffer.ToArray())
    End Sub
End Class
