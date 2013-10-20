Imports Lidgren.Network
Public Class SendData
    Public Shared Sub Login(ByVal Login As String, ByVal Password As String, ByVal Mode As Integer)
        Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
        Buffer.Write(CEditorPackets.Login)
        Buffer.Write(Login)
        Buffer.Write(Password)
        Buffer.Write(Mode)
        Networking.SendData(Buffer)
    End Sub

    Public Shared Sub DataRequest()
        Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
        Buffer.Write(CEditorPackets.DataRequest)
        Networking.SendData(Buffer)
    End Sub

    Public Shared Sub MapData()
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
    Public Shared Sub PlayerData()
        Dim sTileData As New TileData
        Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
        Buffer.Write(CEditorPackets.PlayerData)

        'Accounts data
        Buffer.Write(AccountCount)
        For I As Integer = 0 To AccountCount
            Buffer.Write(Account(I).Email)
            Buffer.Write(Account(I).Password)
            Buffer.Write(Account(I).Name)
            Buffer.Write(Account(I).Sprite)
            Buffer.Write(Account(I).Map)
            Buffer.Write(Account(I).X)
            Buffer.Write(Account(I).Y)
            Buffer.Write(Account(I).GetPlayerDir)
            Buffer.Write(Account(I).GetPlayerAccess)
            Buffer.Write(Account(I).Visible)
        Next
        Networking.SendData(Buffer)
    End Sub
End Class
