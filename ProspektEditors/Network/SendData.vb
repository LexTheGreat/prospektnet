Imports Lidgren.Network
Imports IHProspekt.Objects
Imports IHProspekt.Network
Imports IHProspekt.Core
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
            Buffer.Write(SyncBox.chkMaps.Checked)
            Buffer.Write(SyncBox.chkAccounts.Checked)
            Buffer.Write(SyncBox.chkTilesets.Checked)
            Buffer.Write(SyncBox.chkNpcs.Checked)
            Buffer.Write(SyncBox.chkItems.Checked)
            Networking.SendData(Buffer)
        End Sub

        Public Sub MapData()
            Dim sTileData As New TileData
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.MapData)

            'Maps data
            Buffer.Write(MapCount)
            For Each mp In Map
                Buffer.WriteAllProperties(mp.Base)
            Next
            Networking.SendData(Buffer)
        End Sub

        Public Sub PlayerData()
            Dim sTileData As New TileData
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.AccountData)

            'Accounts data
            Buffer.Write(AccountCount)
            For Each acc In Account
                Buffer.WriteAllProperties(acc.Base)
            Next
            Networking.SendData(Buffer)
        End Sub

        Public Sub TilesetData()
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.TilesetData)

            'Tileset data
            Buffer.Write(TilesetCount)
            For Each tile In Tileset
                Buffer.WriteAllProperties(tile.Base)
            Next
            Networking.SendData(Buffer)
        End Sub

        Public Sub NpcData()
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.NPCData)

            'Npc data
            Buffer.Write(NPCCount)
            For Each nc In NPC
                Buffer.WriteAllProperties(nc.Base)
            Next
            Networking.SendData(Buffer)
        End Sub

        Public Sub ItemData()
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage
            Buffer.Write(CEditorPackets.ItemData)

            'Item data
            Buffer.Write(ItemCount)
            For Each itm In Item
                Buffer.WriteAllProperties(itm.Base)
            Next
            Networking.SendData(Buffer)
        End Sub
    End Module
End Namespace
