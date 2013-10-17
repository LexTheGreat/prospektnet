Imports Lidgren.Network
Class SendData
    Public Shared Sub Alert(ByVal index As Integer, ByVal Message As String)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Alert)
        Buffer.Write(Message)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub RegisterOk(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.RegisterOk)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub LoginOk(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.LoginOk)
        Buffer.Write(index)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub PlayerData(ByVal index As Integer)
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
        Networking.SendDataToAll(Buffer)
    End Sub

    Public Shared Sub ClearPlayer(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.ClearPlayer)
        Buffer.Write(index)
        Buffer.Write(PlayerCount)
        Networking.SendDataToAllBut(index, Buffer)
    End Sub

    Public Shared Sub Position(ByVal index As Integer, Optional ByVal UpdateOwn As Boolean = False)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Position)
        Buffer.Write(index)
        Buffer.Write(Player(index).GetMoving)
        Buffer.Write(Player(index).X)
        Buffer.Write(Player(index).Y)
        Buffer.Write(Player(index).Dir)
        If Not UpdateOwn Then
            Networking.SendDataToAllBut(index, Buffer)
        Else
            Networking.SendDataToAll(Buffer)
        End If
    End Sub

    Public Shared Sub Access(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Access)
        Buffer.Write(Player(index).AccessMode)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub Visible(ByVal index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Visible)
        Buffer.Write(index)
        Buffer.Write(Player(index).Visible)
        Networking.SendDataToAll(Buffer)
    End Sub

    Public Shared Sub Message(ByVal Message As String, Optional ByVal ChatMode As String = vbNullString)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Message)
        Buffer.Write(Message)

        If ChatMode = vbNullString Then
            Networking.SendDataToAll(Buffer)
        ElseIf (ChatMode = "[" & ChatModes.GM & "] ") Then
            Networking.SendDataToAdmins(Buffer)
        End If
    End Sub

    Public Shared Sub MessageTo(ByVal Index As Integer, ByVal Message As String)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Message)
        Buffer.Write(Message)
        Networking.SendDataTo(Index, Buffer)
    End Sub

    Public Shared Sub MessageToAdmins(ByVal Message As String)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.Message)
        Buffer.Write(Message)
        Networking.SendDataToAdmins(Buffer)
    End Sub

    Public Shared Sub NPCData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.NPC)
        Buffer.Write(Index)
        Buffer.Write(NPC.Length)
        Buffer.Write(NPC(Index).Name)
        Buffer.Write(NPC(Index).Sprite)
        Buffer.Write(NPC(Index).X)
        Buffer.Write(NPC(Index).Y)
        Buffer.Write(NPC(Index).Dir)
        Networking.SendDataToAll(Buffer)
    End Sub

    Public Shared Sub NPCPosition(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(ServerPackets.NPCPosition)
        Buffer.Write(Index)
        Buffer.Write(NPC(Index).GetMoving)
        Buffer.Write(NPC(Index).X)
        Buffer.Write(NPC(Index).Y)
        Buffer.Write(NPC(Index).Dir)
        Networking.SendDataToAll(Buffer)
    End Sub

    Public Shared Sub MapData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Dim sMap As MapStructure = Map(Player(Index).Map)
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
            For x As Integer = 0 To sMap.MaxX - 1
                For y As Integer = 0 To sMap.MaxY - 1
                    sTileData = sMap.Layer(I).GetTileData(x, y)
                    Buffer.Write(sTileData.Tileset)
                    Buffer.Write(sTileData.X)
                    Buffer.Write(sTileData.Y)
                Next
            Next
        Next
        Networking.SendDataTo(Index, Buffer)
    End Sub

    ' Editor Packets
    Public Shared Sub EditorLoginOk(ByVal index As Integer, ByVal Mode As Byte)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(SEditorPackets.LoginOk)
        Buffer.Write(Mode)
        Networking.SendDataTo(index, Buffer)
    End Sub

    Public Shared Sub EditorPlayerData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Dim sTileData As New TileData

        Buffer.Write(SEditorPackets.PlayerData)
        Buffer.Write(Account.Length)
        For Each sPlayer In Account
            If IsNothing(sPlayer) Then Continue For
            Buffer.Write(sPlayer.Email)
            Buffer.Write(sPlayer.Password)
            Buffer.Write(sPlayer.Player.Name)
            Buffer.Write(sPlayer.Player.Sprite)
            Buffer.Write(sPlayer.Player.Map)
            Buffer.Write(sPlayer.Player.X)
            Buffer.Write(sPlayer.Player.Y)
            Buffer.Write(sPlayer.Player.Dir)
            Buffer.Write(sPlayer.Player.AccessMode)
            Buffer.Write(sPlayer.Player.Visible)
        Next
        Networking.SendDataTo(Index, Buffer)
    End Sub

    Public Shared Sub EditorMapData(ByVal Index As Integer)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Dim sTileData As New TileData

        Buffer.Write(SEditorPackets.MapData)
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
            For i As Integer = MapLayerEnum.Ground To MapLayerEnum.FringeMask
                For x As Integer = 0 To sMap.MaxX - 1
                    For y As Integer = 0 To sMap.MaxY - 1
                        sTileData = sMap.Layer(i).GetTileData(x, y)
                        Buffer.Write(sTileData.Tileset)
                        Buffer.Write(sTileData.X)
                        Buffer.Write(sTileData.Y)
                    Next
                Next
            Next
        Next
        Networking.SendDataTo(Index, Buffer)
    End Sub

    Public Shared Sub EditorDataSent(ByVal index As Integer, ByVal Mode As Byte)
        Dim Buffer As NetOutgoingMessage = pServer.CreateMessage
        Buffer.Write(SEditorPackets.DataSent)
        Buffer.Write(Mode)
        Networking.SendDataTo(index, Buffer)
    End Sub
End Class
