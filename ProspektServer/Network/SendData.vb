Imports Lidgren.Network
Imports IHProspekt.Objects
Imports IHProspekt.Network
Imports IHProspekt.Core
Namespace Network.SendData
    Public Module SendData
        Public Sub Alert(ByVal index As Integer, ByVal Message As String)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.Alert)
            Buffer.Write(Message)
            SendDataTo(index, Buffer)
        End Sub

        Public Sub RegisterOk(ByVal index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.RegisterOk)
            SendDataTo(index, Buffer)
        End Sub

        Public Sub LoginOk(ByVal index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.LoginOk)
            Buffer.Write(index)
            SendDataTo(index, Buffer)
        End Sub

        Public Sub PlayerData(ByVal index As Integer)
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
            SendDataToAll(Buffer)
        End Sub

        Public Sub ClearPlayer(ByVal index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.ClearPlayer)
            Buffer.Write(index)
            Buffer.Write(PlayerCount)
            SendDataToAllBut(index, Buffer)
        End Sub

        Public Sub Position(ByVal index As Integer, Optional ByVal UpdateOwn As Boolean = False)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.Position)
            Buffer.Write(index)
            Buffer.Write(Player(index).Moving)
            Buffer.Write(Player(index).X)
            Buffer.Write(Player(index).Y)
            Buffer.Write(Player(index).Dir)
            If Not UpdateOwn Then
                SendDataToAllBut(index, Buffer)
            Else
                SendDataToAll(Buffer)
            End If
        End Sub

        Public Sub Access(ByVal index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.Access)
            Buffer.Write(index)
            Buffer.Write(Player(index).AccessMode)
            SendDataTo(index, Buffer)
        End Sub

        Public Sub Visible(ByVal index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.Visible)
            Buffer.Write(index)
            Buffer.Write(Player(index).Visible)
            SendDataToAll(Buffer)
        End Sub

        Public Sub Message(ByVal Message As String, Optional ByVal ChatMode As String = vbNullString)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.Message)
            Buffer.Write(Message)

            If ChatMode = vbNullString Then
                SendDataToAll(Buffer)
            ElseIf (ChatMode = "[" & ChatModes.GM & "] ") Then
                SendDataToAdmins(Buffer)
            End If
        End Sub

        Public Sub MessageTo(ByVal Index As Integer, ByVal Message As String)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.Message)
            Buffer.Write(Message)
            SendDataTo(Index, Buffer)
        End Sub

        Public Sub MessageToAdmins(ByVal Message As String)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.Message)
            Buffer.Write(Message)
            SendDataToAdmins(Buffer)
        End Sub

        Public Sub NPCData(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.NPC)
            Buffer.Write(Index)
            Buffer.Write(NPCCount)
            Buffer.Write(NPC(Index).Base.Name)
            Buffer.Write(NPC(Index).Base.Sprite)
            Buffer.Write(NPC(Index).Base.ID)
            Buffer.Write(NPC(Index).Base.Level)
            Buffer.Write(NPC(Index).Base.Health)
            Buffer.Write(NPC(Index).Base.X)
            Buffer.Write(NPC(Index).Base.Y)
            Buffer.Write(NPC(Index).Base.Dir)
            Buffer.Write(NPC(Index).Base.Inventory.Length)
            For l As Integer = 0 To NPC(Index).Base.Inventory.Length
                Buffer.Write(NPC(Index).Base.Inventory(Index))
            Next
            SendDataToAll(Buffer)
        End Sub

        Public Sub NPCPosition(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(ServerPackets.NPCPosition)
            Buffer.Write(Index)
            Buffer.Write(NPC(Index).Moving)
            Buffer.Write(NPC(Index).X)
            Buffer.Write(NPC(Index).Y)
            Buffer.Write(NPC(Index).Dir)
            SendDataToAll(Buffer)
        End Sub

        Public Sub MapData(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Dim sMap As Maps = Map(Player(Index).Map)
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
                For x As Integer = 0 To sMap.MaxX
                    For y As Integer = 0 To sMap.MaxY
                        sTileData = sMap.Layer(I).Tiles(x, y)
                        Buffer.Write(sTileData.Tileset)
                        Buffer.Write(sTileData.X)
                        Buffer.Write(sTileData.Y)
                    Next
                Next
            Next
            SendDataTo(Index, Buffer)
        End Sub

        Public Sub TilesetData(ByVal Index As Integer)
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
            SendDataToAll(Buffer)
        End Sub

        ' Editor Packets
        Public Sub EditorLoginOk(ByVal index As Integer, ByVal Mode As Byte)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(SEditorPackets.LoginOk)
            Buffer.Write(Mode)
            SendDataTo(index, Buffer)
        End Sub

        Public Sub EditorMapData(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(SEditorPackets.MapData)

            'Maps data
            Buffer.Write(MapCount)
            For Each mp In Map
                Buffer.WriteAllProperties(mp.Base)
            Next
            SendDataTo(Index, Buffer)
            EditorDataSent(Index, 1, "Map Data Synchronized!")
        End Sub

        Public Sub EditorPlayerData(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(SEditorPackets.AccountData)

            'Accounts data
            Buffer.Write(AccountCount)
            For Each acc In Account
                Buffer.WriteAllProperties(acc.Base)
            Next
            SendDataTo(Index, Buffer)
            EditorDataSent(Index, 1, "Player Data Synchronized!")
        End Sub

        Public Sub EditorTilesetData(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(SEditorPackets.TilesetData)

            'Tileset data
            Buffer.Write(TilesetCount)
            For Each tile In Tileset
                Buffer.WriteAllProperties(tile.Base)
            Next
            SendDataTo(Index, Buffer)
            EditorDataSent(Index, 1, "Tileset Data Synchronized!")
        End Sub

        Public Sub EditorNPCData(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(SEditorPackets.NPCData)

            'Npc data
            Buffer.Write(NPCCount)
            For Each nc In NPC
                Buffer.WriteAllProperties(nc.Base)
            Next
            SendDataTo(Index, Buffer)
            EditorDataSent(Index, 1, "Npc Data Synchronized!")
        End Sub

        Public Sub EditorItemData(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(CEditorPackets.NPCData)

            'Item data
            Buffer.Write(ItemCount)
            For Each itm In Item
                Buffer.WriteAllProperties(itm.Base)
            Next
            SendDataTo(Index, Buffer)
        End Sub

        Public Sub EditorDataSent(ByVal index As Integer, ByVal Mode As Byte, ByVal Text As String)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(SEditorPackets.DataSent)
            Buffer.Write(Mode)
            Buffer.Write(Text)
            SendDataTo(index, Buffer)
        End Sub
    End Module
End Namespace