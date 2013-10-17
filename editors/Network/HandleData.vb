Imports Lidgren.Network
Public Class HandleData
    Public Shared Sub HandleDataPackets(ByVal PacketNum As Integer, ByRef Data As NetIncomingMessage)
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ServerPackets.Alert Then HandleData.Alert(Data)
        If PacketNum = SEditorPackets.LoginOk Then HandleData.LoginOk(Data)
        If PacketNum = SEditorPackets.MapData Then HandleData.EditorMapData(Data)
        If PacketNum = SEditorPackets.PlayerData Then HandleData.EditorPlayerData(Data)
        If PacketNum = SEditorPackets.DataSent Then HandleData.DataSent(Data)
    End Sub

    Public Shared Sub Alert(ByRef Data As NetIncomingMessage)
        Dim Message As String


        Message = Data.ReadString
        MsgBox(Message)
    End Sub

    Public Shared Sub LoginOk(ByRef Data As NetIncomingMessage)
        Dim mode As Byte = 0


        mode = Data.ReadInt32()
        If mode = 0 Then
            SendData.DataRequest()
        Else
            SendData.MapData()
            SendData.PlayerData()
        End If
    End Sub

    Public Shared Sub EditorMapData(ByRef Data As NetIncomingMessage)
        Dim num As Integer = 0, sTileData As New TileData


        num = Data.ReadInt32
        ReDim Map(0 To num)
        For i As Integer = 0 To num
            Map(i) = New MapStructure
            Map(i).Name = Data.ReadString
            Map(i).MaxX = Data.ReadInt32
            Map(i).MaxY = Data.ReadInt32
            Map(i).Alpha = Data.ReadInt32
            Map(i).Red = Data.ReadInt32
            Map(i).Green = Data.ReadInt32
            Map(i).Blue = Data.ReadInt32
            Map(i).ReSizeTileData(New Integer() {Map(i).MaxX, Map(i).MaxY})
            For l As Integer = MapLayerEnum.Ground To MapLayerEnum.FringeMask
                For x As Integer = 0 To Map(i).MaxX - 1
                    For y As Integer = 0 To Map(i).MaxY - 1
                        sTileData = New TileData
                        sTileData.Tileset = Data.ReadInt32
                        sTileData.X = Data.ReadInt32
                        sTileData.Y = Data.ReadInt32
                        Map(i).Layer(l).SetTileData(x, y, sTileData)
                    Next
                Next
            Next
        Next
        MapData.SaveMaps()
        MapEditor.ReloadList()
    End Sub

    Public Shared Sub EditorPlayerData(ByRef Data As NetIncomingMessage)
        Dim num As Integer = 0, sTileData As New TileData


        num = Data.ReadInt32
        ReDim Account(0 To num)
        For i As Integer = 0 To num
            Account(i) = New Accounts
            Account(i).Email = Data.ReadString
            Account(i).Password = Data.ReadString
            Account(i).Name = Data.ReadString
            Account(i).Sprite = Data.ReadInt32
            Account(i).Map = Data.ReadInt32
            Account(i).X = Data.ReadInt32
            Account(i).Y = Data.ReadInt32
            Account(i).SetPlayerDir(Data.ReadInt32)
            Account(i).SetPlayerAccess(Data.ReadInt32)
            Account(i).Visible = Data.ReadInt32
        Next
        AccountData.SaveAccounts()
        AccountEditor.ReloadList()
    End Sub

    Public Shared Sub DataSent(ByRef Data As NetIncomingMessage)
        Dim mode As Byte = 0


        mode = Data.ReadInt32()
        If mode = 0 Then
            CommitData.Hide()
            MsgBox("Commit sucesfull")
        Else
            SyncData.Hide()
            MsgBox("Editors synchronized")
        End If
    End Sub
End Class
