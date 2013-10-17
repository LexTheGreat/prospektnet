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
        Buffer.Write(Map.Length)
        For Each sMap In Map
            If IsNothing(sMap) Then Continue For
            Buffer.Write(sMap.Name)
            Buffer.Write(sMap.MaxX)
            Buffer.Write(sMap.MaxY)
            Buffer.Write(sMap.Alpha)
            Buffer.Write(sMap.Red)
            Buffer.Write(sMap.Green)
            Buffer.Write(sMap.Blue)
            For l As Integer = MapLayerEnum.Ground To MapLayerEnum.FringeMask
                For x As Integer = 0 To sMap.MaxX - 1
                    For y As Integer = 0 To sMap.MaxY - 1
                        sTileData = sMap.Layer(l).GetTileData(x, y)
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
        Buffer.Write(Account.Length)
        For Each sPlayer In Account
            If IsNothing(sPlayer) Then Continue For
            Buffer.Write(sPlayer.Email)
            Buffer.Write(sPlayer.Password)
            Buffer.Write(sPlayer.Name)
            Buffer.Write(sPlayer.Sprite)
            Buffer.Write(sPlayer.Map)
            Buffer.Write(sPlayer.X)
            Buffer.Write(sPlayer.Y)
            Buffer.Write(sPlayer.GetPlayerDir)
            Buffer.Write(sPlayer.GetPlayerAccess)
            Buffer.Write(sPlayer.Visible)
        Next
        Networking.SendData(Buffer)
    End Sub
End Class
