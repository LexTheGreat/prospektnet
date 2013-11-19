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
            If PacketNum = SEditorPackets.AccountData Then HandleData.EditorPlayerData(Data)
            If PacketNum = SEditorPackets.TilesetData Then HandleData.EditorTilesetData(Data)
            If PacketNum = SEditorPackets.NPCData Then HandleData.EditorNPCData(Data)
            If PacketNum = SEditorPackets.ItemData Then HandleData.EditorItemData(Data)
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
                If CommitBox.chkMaps.Checked Then SendData.MapData()
                If CommitBox.chkAccounts.Checked Then SendData.PlayerData()
                If CommitBox.chkTilesets.Checked Then SendData.TilesetData()
                If CommitBox.chkNpcs.Checked Then SendData.NpcData()
                If CommitBox.chkItems.Checked Then SendData.ItemData()
            End If
        End Sub

        Public Sub EditorMapData(ByRef Data As NetIncomingMessage)
            MapCount = Data.ReadInt32
            ReDim Map(0 To MapCount)
            For i As Integer = 0 To MapCount
                Map(i) = New Maps
                Data.ReadAllProperties(Map(i).Base)
            Next
            Maps.Data.SaveAll()
            MapEditor.ReloadList()
        End Sub

        Public Sub EditorPlayerData(ByRef Data As NetIncomingMessage)
            AccountCount = Data.ReadInt32
            ReDim Account(0 To AccountCount)
            For i As Integer = 0 To AccountCount
                Account(i) = New Accounts
                Data.ReadAllProperties(Account(i).Base)
            Next i
            Accounts.Data.SaveAll()
            AccountEditor.ReloadList()
        End Sub

        Public Sub EditorTilesetData(ByRef data As NetIncomingMessage)
            TilesetCount = data.ReadInt32
            ReDim Tileset(0 To TilesetCount)
            For i As Integer = 0 To TilesetCount
                Tileset(i) = New Tilesets
                data.ReadAllProperties(Tileset(i).Base)
                Tileset(i).Save()
            Next i
            Tilesets.Data.SaveAll()
            TilesetEditor.Init()
        End Sub

        Public Sub EditorNPCData(ByRef data As NetIncomingMessage)
            NPCCount = data.ReadInt32
            ReDim NPC(0 To NPCCount)
            For i As Integer = 0 To NPCCount
                NPC(i) = New NPCs
                data.ReadAllProperties(NPC(i).Base)
            Next i
            NPCs.Data.SaveAll()
            NpcEditor.Init()
        End Sub

        Public Sub EditorItemData(ByRef data As NetIncomingMessage)
            ItemCount = data.ReadInt32
            ReDim Item(0 To ItemCount)
            For i As Integer = 0 To ItemCount
                Item(i) = New Items
                Data.ReadAllProperties(Item(i).Base)
            Next i
            Items.Data.SaveAll()
            ItemEditor.Init()
        End Sub

        Public Sub DataSent(ByRef Data As NetIncomingMessage)
            Dim mode As Byte = 0

            mode = Data.ReadByte
            If mode = 0 Then
                CommitData.Hide()
                MsgBox(Data.ReadString)
            Else
                SyncData.Hide()
                MsgBox(Data.ReadString)
            End If
        End Sub
    End Module
End Namespace