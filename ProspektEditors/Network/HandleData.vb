Imports Lidgren.Network
Imports IHProspekt.Objects
Imports IHProspekt.Network
Imports IHProspekt.Core
Namespace Network.HandleData
    Public Module HandleData
        Public Sub HandleDataPackets(ByVal PacketNum As Integer, ByRef Data As NetIncomingMessage)
            If PacketNum = 0 Then Exit Sub
            If PacketNum = ServerPackets.Alert Then HandleData.Alert(Data)
            If PacketNum = SEditorPackets.LoginOk Then HandleData.LoginOk(Data)
            If PacketNum = SEditorPackets.MapData Then HandleData.EditorMapData(Data)
            If PacketNum = SEditorPackets.PlayerData Then HandleData.EditorPlayerData(Data)
            If PacketNum = SEditorPackets.TilesetData Then HandleData.EditorTilesetData(Data)
            If PacketNum = SEditorPackets.NPCData Then HandleData.EditorNPCData(Data)
            If PacketNum = SEditorPackets.DataSent Then HandleData.DataSent(Data)
        End Sub

        Public Sub Alert(ByRef Data As NetIncomingMessage)
            Dim Message As String

            Message = Data.ReadString
            MsgBox(Message)
        End Sub

        Public Sub LoginOk(ByRef Data As NetIncomingMessage)
            Dim mode As Byte = 0

            mode = Data.ReadByte
            If mode = 0 Then
                SendData.DataRequest()
            Else
                SendData.MapData()
                SendData.PlayerData()
                SendData.TilesetData()
            End If
        End Sub

        Public Sub EditorMapData(ByRef Data As NetIncomingMessage)
            Dim num As Integer, sTileData As New TileData

            num = Data.ReadInt32
            ReDim Map(0 To num)
            For i As Integer = 0 To num
                Map(i) = New Maps
                Map(i).Name = Data.ReadString
                Map(i).MaxX = Data.ReadInt32
                Map(i).MaxY = Data.ReadInt32
                Map(i).Alpha = Data.ReadByte
                Map(i).Red = Data.ReadByte
                Map(i).Green = Data.ReadByte
                Map(i).Blue = Data.ReadByte
                Map(i).ReSizeTileData(New Integer() {Map(i).MaxX, Map(i).MaxY})
                For l As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
                    For x As Integer = 0 To Map(i).MaxX
                        For y As Integer = 0 To Map(i).MaxY
                            sTileData = New TileData
                            sTileData.Tileset = Data.ReadInt32
                            sTileData.X = Data.ReadInt32
                            sTileData.Y = Data.ReadInt32
                            Map(i).Layer(l).SetTileData(x, y, sTileData)
                        Next
                    Next
                Next
            Next
            Maps.Data.SaveMaps()
            MapEditor.ReloadList()
        End Sub

        Public Sub EditorPlayerData(ByRef Data As NetIncomingMessage)
            Dim num As Integer = 0, sTileData As New TileData

            num = Data.ReadInt32
            ReDim Account(0 To num)
            For i As Integer = 1 To num
                Account(i) = New Accounts
                Account(i).Email = Data.ReadString
                Account(i).Password = Data.ReadString
                Account(i).Name = Data.ReadString
                Account(i).Sprite = Data.ReadInt32
                Account(i).PlayerMap = Data.ReadInt32
                Account(i).X = Data.ReadInt32
                Account(i).Y = Data.ReadInt32
                Account(i).SetPlayerDir(Data.ReadByte)
                Account(i).SetPlayerAccess(Data.ReadByte)
                Account(i).Visible = Data.ReadBoolean
            Next
            Accounts.Data.SaveAccounts()
            AccountEditor.ReloadList()
        End Sub

        Public Sub EditorTilesetData(ByRef data As NetIncomingMessage)
            Dim num As Integer

            num = data.ReadInt32
            ReDim Tileset(0 To num)
            For i As Integer = 1 To num
                Tileset(i) = New Tilesets
                Tileset(i).ID = data.ReadInt32
                Tileset(i).Name = data.ReadString
                Tileset(i).MaxX = data.ReadInt32
                Tileset(i).MaxY = data.ReadInt32
                Tileset(i).ResizeTileData(New Integer() {Tileset(i).MaxX, Tileset(i).MaxY})
                For x As Integer = 0 To Tileset(i).MaxX
                    For y As Integer = 0 To Tileset(i).MaxY
                        Tileset(i).Tile(x, y) = data.ReadByte
                    Next y
                Next x
                Tileset(i).Save()
            Next i
            TilesetEditor.Init()
        End Sub

        Public Sub EditorNPCData(ByRef data As NetIncomingMessage)
            Dim num As Integer, invsize As Integer

            num = data.ReadInt32
            ReDim NPC(0 To num)
            For i As Integer = 1 To num
                NPC(i) = New NPCs
                NPC(i).Base.Name = data.ReadString
                NPC(i).Base.Sprite = data.ReadInt32
                NPC(i).Base.ID = data.ReadInt32
                NPC(i).Base.Level = data.ReadInt32
                NPC(i).Base.Health = data.ReadInt32
                NPC(i).Base.X = data.ReadInt32
                NPC(i).Base.Y = data.ReadInt32
                NPC(i).Base.Dir = data.ReadInt32
                invsize = data.ReadInt32
                ReDim NPC(i).Base.Inventory(0 To invsize)
                For l As Integer = 0 To invsize
                    NPC(i).Base.Inventory(l) = data.ReadInt32
                Next
            Next i
            NPCCount = num
        End Sub

        Public Sub DataSent(ByRef Data As NetIncomingMessage)
            Dim mode As Byte = 0

            mode = Data.ReadByte
            If mode = 0 Then
                CommitData.Hide()
                MsgBox("Commit sucesfull")
            Else
                SyncData.Hide()
                MsgBox("Editors synchronized")
            End If
        End Sub
    End Module
End Namespace