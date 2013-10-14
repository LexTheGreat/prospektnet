Public Class HandleData
    Public Shared Sub HandleDataPackets(ByVal PacketNum As Long, ByRef Data() As Byte)
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ServerPackets.Alert Then HandleData.Alert(Data)
        If PacketNum = SEditorPackets.LoginOk Then HandleData.LoginOk(Data)
        If PacketNum = SEditorPackets.Data Then HandleData.EditorData(Data)
        If PacketNum = SEditorPackets.DataSent Then HandleData.DataSent(Data)
    End Sub

    Public Shared Sub Alert(ByVal data() As Byte)
        Dim Message As String
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(data)
        Message = Buffer.ReadString
        MsgBox(Message)
    End Sub

    Public Shared Sub LoginOk(ByRef Data() As Byte)
        Dim mode As Byte = 0
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(Data)
        mode = Buffer.ReadInteger()
        If mode = 0 Then SendData.DataRequest() Else SendData.Data()
    End Sub

    Public Shared Sub EditorData(ByRef Data() As Byte)
        Dim num As Integer = 0, sTileData As New TileData
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(Data)
        num = Buffer.ReadInteger
        ReDim Account(0 To num)
        For i As Integer = 0 To num
            Account(i) = New Accounts
            Account(i).Email = Buffer.ReadString
            Account(i).Password = Buffer.ReadString
            Account(i).Name = Buffer.ReadString
            Account(i).Sprite = Buffer.ReadInteger
            Account(i).Map = Buffer.ReadInteger
            Account(i).X = Buffer.ReadInteger
            Account(i).Y = Buffer.ReadInteger
            Account(i).SetPlayerDir(Buffer.ReadInteger)
            Account(i).SetPlayerAccess(Buffer.ReadInteger)
            Account(i).Visible = Buffer.ReadInteger
        Next
        AccountData.SaveAccounts()
        AccountEditor.ReloadList()

        num = Buffer.ReadInteger
        ReDim Map(0 To num)
        For i As Integer = 0 To num
            Map(i) = New MapStructure
            Map(i).Name = Buffer.ReadString
            Map(i).MaxX = Buffer.ReadInteger
            Map(i).MaxY = Buffer.ReadInteger
            Map(i).Alpha = Buffer.ReadInteger
            Map(i).Red = Buffer.ReadInteger
            Map(i).Green = Buffer.ReadInteger
            Map(i).Blue = Buffer.ReadInteger
            For l As Integer = MapLayerEnum.Ground To MapLayerEnum.FringeMask
                Map(i).Layer(l).ReSizeTileData(i, New Integer() {Map(i).MaxX, Map(i).MaxY})
                For x As Integer = 0 To Map(i).MaxX - 1
                    For y As Integer = 0 To Map(i).MaxY - 1
                        sTileData = New TileData
                        sTileData.Tileset = Buffer.ReadInteger
                        sTileData.X = Buffer.ReadInteger
                        sTileData.Y = Buffer.ReadInteger
                        Map(i).Layer(l).SetTileData(x, y, sTileData)
                    Next
                Next
            Next
        Next
        MapData.SaveMaps()
        MapEditor.ReloadList()
    End Sub

    Public Shared Sub DataSent(ByRef Data() As Byte)
        Dim mode As Byte = 0
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(Data)
        mode = Buffer.ReadInteger()
        If mode = 0 Then
            CommitData.Hide()
            MsgBox("Commit sucesfull")
        Else
            SyncData.Hide()
            MsgBox("Editors synchronized")
        End If
    End Sub
End Class
