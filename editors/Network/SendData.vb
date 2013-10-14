Public Class SendData
    Public Shared Sub Login(ByVal Login As String, ByVal Password As String, ByVal Mode As Integer)
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(CEditorPackets.Login)
        Buffer.WriteString(Login)
        Buffer.WriteString(Password)
        Buffer.WriteInteger(Mode)
        Networking.SendData(Buffer.ToArray())
    End Sub

    Public Shared Sub DataRequest()
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(CEditorPackets.DataRequest)
        Networking.SendData(Buffer.ToArray())
    End Sub

    Public Shared Sub Data()
        Dim sTileData As New TileData
        Dim Buffer As New ByteBuffer
        Buffer.WriteInteger(CEditorPackets.Data)

        'Accounts data
        Buffer.WriteInteger(Account.Length - 1)
        For Each sPlayer In Account
            If IsNothing(sPlayer) Then Continue For
            Buffer.WriteString(sPlayer.Email)
            Buffer.WriteString(sPlayer.Password)
            Buffer.WriteString(sPlayer.Name)
            Buffer.WriteInteger(sPlayer.Sprite)
            Buffer.WriteInteger(sPlayer.Map)
            Buffer.WriteInteger(sPlayer.X)
            Buffer.WriteInteger(sPlayer.Y)
            Buffer.WriteInteger(sPlayer.PlayerDir)
            Buffer.WriteInteger(sPlayer.AccessMode)
            Buffer.WriteInteger(sPlayer.Visible)
        Next

        'Maps data
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
        Networking.SendData(Buffer.ToArray())
    End Sub
End Class
