Imports Lidgren.Network
Namespace Network.SendData
    Public Module SendData
        Public Sub Login(ByVal Login As String, ByVal Password As String, ByVal Mode As Integer)
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.Login)
            Buffer.Write(Login)
            Buffer.Write(Password)
            Buffer.Write(Mode)
            Networking.SendData(Buffer)
        End Sub

        Public Sub DataRequest()
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.DataRequest)
            Networking.SendData(Buffer)
        End Sub

        Public Sub MapData()
            Dim sTileData As New TileData
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.MapData)
            'Maps data
            Buffer.Write(MapCount)
            For I As Integer = 0 To MapCount
                Buffer.Write(Map(I).Name)
                Buffer.Write(Map(I).MaxX)
                Buffer.Write(Map(I).MaxY)
                Buffer.Write(Map(I).Alpha)
                Buffer.Write(Map(I).Red)
                Buffer.Write(Map(I).Green)
                Buffer.Write(Map(I).Blue)
                For j As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
                    For x As Integer = 0 To Map(I).MaxX - 1
                        For y As Integer = 0 To Map(I).MaxY - 1
                            sTileData = Map(I).Layer(j).GetTileData(x, y)
                            Buffer.Write(sTileData.Tileset)
                            Buffer.Write(sTileData.X)
                            Buffer.Write(sTileData.Y)
                        Next
                    Next
                Next
            Next
            Networking.SendData(Buffer)
        End Sub
        Public Sub PlayerData()
            Dim sTileData As New TileData
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.PlayerData)

            'Accounts data
            Buffer.Write(AccountCount)
            For I As Integer = 1 To AccountCount
                Buffer.Write(Account(I).Email)
                Buffer.Write(Account(I).Password)
                Buffer.Write(Account(I).Name)
                Buffer.Write(Account(I).Sprite)
                Buffer.Write(Account(I).PlayerMap)
                Buffer.Write(Account(I).X)
                Buffer.Write(Account(I).Y)
                Buffer.Write(Account(I).GetPlayerDir)
                Buffer.Write(Account(I).GetPlayerAccess)
                Buffer.Write(Account(I).Visible)
            Next
            Networking.SendData(Buffer)
        End Sub

        Public Sub TilesetData()
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.TilesetData)

            'Tileset data
            Buffer.Write(TilesetCount)
            For I As Integer = 1 To TilesetCount
                Buffer.Write(Tileset(I).ID)
                Buffer.Write(Tileset(I).Name)
                Buffer.Write(Tileset(I).MaxX)
                Buffer.Write(Tileset(I).MaxY)
                For x As Integer = 0 To Tileset(I).MaxX
                    For y As Integer = 0 To Tileset(I).MaxY
                        Buffer.Write(Tileset(I).Tile(x, y))
                    Next
                Next
            Next
            Networking.SendData(Buffer)
        End Sub
    End Module
End Namespace
