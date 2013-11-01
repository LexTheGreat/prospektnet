Imports Lidgren.Network
Class SendData
    Public Shared Sub Alert(ByVal index As Integer, ByVal Message As String)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Alert)
        Buffer.Write(Message)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub RegisterOk(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.RegisterOk)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub LoginOk(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.LoginOk)
        Buffer.Write(index)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub PlayerData(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Player)
        Buffer.Write(index)
        Buffer.Write(PlayerCount)
        Buffer.Write(Player(index).Name)
        Buffer.Write(Player(index).Sprite)
        Buffer.Write(Player(index).X)
        Buffer.Write(Player(index).Y)
        Buffer.Write(Player(index).Dir)
        Buffer.Write(Player(index).AccessMode)
        Buffer.Write(Player(index).Visible)
        Networking.SendDataToAll(Buffer)
    End Sub

    Public Shared Sub ClearPlayer(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.ClearPlayer)
        Buffer.Write(index)
        Buffer.Write(PlayerCount)
        Networking.SendDataToAllBut(index, Buffer)
    End Sub

    Public Shared Sub Position(ByVal index As Integer, Optional ByVal UpdateOwn As Boolean = False)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Position)
        Buffer.Write(index)
        Buffer.Write(Player(index).GetMoving)
        Buffer.Write(Player(index).X)
        Buffer.Write(Player(index).Y)
        Buffer.Write(Player(index).Dir)
        If Not UpdateOwn Then
            Networking.SendDataToAllBut(index, Buffer)
        Else
            Networking.SendDataToAll(Buffer)
        End If
    End Sub

    Public Shared Sub Access(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Access)
        Buffer.Write(index)
        Buffer.Write(Player(index).AccessMode)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub Visible(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Visible)
        Buffer.Write(index)
        Buffer.Write(Player(index).Visible)
        Networking.SendDataToAll(Buffer)
    End Sub

    Public Shared Sub Message(ByVal Message As String, Optional ByVal ChatMode As String = vbNullString)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Message)
        Buffer.Write(Message)

        If ChatMode = vbNullString Then
            Networking.SendDataToAll(Buffer)
        ElseIf (ChatMode = "[" & ChatModes.GM & "] ") Then
            Networking.SendDataToAdmins(Buffer)
        End If
    End Sub

    Public Shared Sub MessageTo(ByVal Index As Integer, ByVal Message As String)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Message)
        Buffer.Write(Message)
        Networking.SendDataTo(Index, Buffer)
    End Sub

    Public Shared Sub MessageToAdmins(ByVal Message As String)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Message)
        Buffer.Write(Message)
        Networking.SendDataToAdmins(Buffer)
    End Sub

    Public Shared Sub NPCData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.NPC)
        Buffer.Write(Index)
        Buffer.Write(NPCCount)
        Buffer.WriteAllFields(NPC(Index).Base)
        Networking.SendDataToAll(Buffer)
    End Sub

    Public Shared Sub NPCPosition(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.NPCPosition)
        Buffer.Write(Index)
        Buffer.Write(NPC(Index).Moving)
        Buffer.Write(NPC(Index).X)
        Buffer.Write(NPC(Index).Y)
        Buffer.Write(NPC(Index).Dir)
        Networking.SendDataToAll(Buffer)
    End Sub

    Public Shared Sub MapData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Dim sMap As MapStructure = Map(Player(Index).Map)
        Dim sTileData As New TileData

        Buffer.Write(ServerPackets.MapData)
        Buffer.Write(sMap.Name)
        Buffer.Write(sMap.MaxX)
        Buffer.Write(sMap.MaxY)
        Buffer.Write(sMap.Alpha)
        Buffer.Write(sMap.Red)
        Buffer.Write(sMap.Green)
        Buffer.Write(sMap.Blue)
        For I As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
            For x As Integer = 0 To sMap.MaxX - 1
                For y As Integer = 0 To sMap.MaxY - 1
                    sTileData = sMap.Layer(I).GetTileData(x, y)
                    Buffer.Write(sTileData.Tileset)
                    Buffer.Write(sTileData.X)
                    Buffer.Write(sTileData.Y)
                Next
            Next
        Next
        Networking.SendDataTo(Index, Buffer)
    End Sub

    Public Shared Sub TilesetData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage

        Buffer.Write(ServerPackets.TilesetData)
        Buffer.Write(Index)
        Buffer.Write(TilesetCount)
        Buffer.Write(Tileset(Index).ID)
        Buffer.Write(Tileset(Index).Name)
        Buffer.Write(Tileset(Index).MaxX)
        Buffer.Write(Tileset(Index).MaxY)
        For x As Integer = 0 To Tileset(Index).MaxX
            For y As Integer = 0 To Tileset(Index).MaxY
                Buffer.Write(Tileset(Index).Tile(x, y))
            Next
        Next
        Networking.SendDataToAll(Buffer)
    End Sub

    ' Editor Packets
    Public Shared Sub EditorLoginOk(ByVal index As Integer, ByVal Mode As Byte)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(SEditorPackets.LoginOk)
        Buffer.Write(Mode)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub EditorPlayerData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Dim sTileData As New TileData

        Buffer.Write(SEditorPackets.PlayerData)
        Buffer.Write(AccountCount)
        For I As Integer = 1 To AccountCount
            Buffer.Write(Account(I).Email)
            Buffer.Write(Account(I).Password)
            Buffer.Write(Account(I).Player.Name)
            Buffer.Write(Account(I).Player.Sprite)
            Buffer.Write(Account(I).Player.Map)
            Buffer.Write(Account(I).Player.X)
            Buffer.Write(Account(I).Player.Y)
            Buffer.Write(Account(I).Player.Dir)
            Buffer.Write(Account(I).Player.AccessMode)
            Buffer.Write(Account(I).Player.Visible)
        Next
        Networking.SendDataTo(Index, Buffer)
    End Sub

    Public Shared Sub EditorMapData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Dim sTileData As New TileData

        Buffer.Write(SEditorPackets.MapData)
        Buffer.Write(MapCount)
        For i As Integer = 0 To MapCount
            Buffer.Write(Map(i).Name)
            Buffer.Write(Map(i).MaxX)
            Buffer.Write(Map(i).MaxY)
            Buffer.Write(Map(i).Alpha)
            Buffer.Write(Map(i).Red)
            Buffer.Write(Map(i).Green)
            Buffer.Write(Map(i).Blue)
            For j As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
                For x As Integer = 0 To Map(i).MaxX - 1
                    For y As Integer = 0 To Map(i).MaxY - 1
                        sTileData = Map(i).Layer(j).GetTileData(x, y)
                        Buffer.Write(sTileData.Tileset)
                        Buffer.Write(sTileData.X)
                        Buffer.Write(sTileData.Y)
                    Next
                Next
            Next
        Next
        Networking.SendDataTo(Index, Buffer)
    End Sub

    Public Shared Sub EditorTilesetData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage

        Buffer.Write(SEditorPackets.TilesetData)
        Buffer.Write(TilesetCount)
        For i As Integer = 1 To TilesetCount
            Buffer.Write(Tileset(i).ID)
            Buffer.Write(Tileset(i).Name)
            Buffer.Write(Tileset(i).MaxX)
            Buffer.Write(Tileset(i).MaxY)
            For x As Integer = 0 To Tileset(i).MaxX
                For y As Integer = 0 To Tileset(i).MaxY
                    Buffer.Write(Tileset(i).Tile(x, y))
                Next
            Next
        Next i
        Networking.SendDataTo(Index, Buffer)
    End Sub

    Public Shared Sub EditorDataSent(ByVal index As Integer, ByVal Mode As Byte)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(SEditorPackets.DataSent)
        Buffer.Write(Mode)
        Networking.SendDataTo(index, Buffer)
    End Sub
End Class
