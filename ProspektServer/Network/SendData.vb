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
            Buffer.WriteAllFields(NPC(Index).Base)
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
                        sTileData = sMap.Layer(I).GetTileData(x, y)
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

        Public Sub EditorPlayerData(ByVal Index As Integer)
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
            SendDataTo(Index, Buffer)
        End Sub

        Public Sub EditorMapData(ByVal Index As Integer)
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
                    For x As Integer = 0 To Map(i).MaxX
                        For y As Integer = 0 To Map(i).MaxY
                            sTileData = Map(i).Layer(j).GetTileData(x, y)
                            Buffer.Write(sTileData.Tileset)
                            Buffer.Write(sTileData.X)
                            Buffer.Write(sTileData.Y)
                        Next
                    Next
                Next
            Next
            SendDataTo(Index, Buffer)
        End Sub

        Public Sub EditorTilesetData(ByVal Index As Integer)
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
            SendDataTo(Index, Buffer)
        End Sub

        Public Sub EditorNPCData(ByVal Index As Integer)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(SEditorPackets.NPCData)
            Buffer.Write(NPCCount)
            For I As Integer = 1 To NPCCount
                Buffer.WriteAllFields(NPC(I).Base)
            Next
            SendDataTo(Index, Buffer)
        End Sub

        Public Sub EditorDataSent(ByVal index As Integer, ByVal Mode As Byte)
            Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
            Buffer.Write(SEditorPackets.DataSent)
            Buffer.Write(Mode)
            SendDataTo(index, Buffer)
        End Sub
    End Module
End Namespace