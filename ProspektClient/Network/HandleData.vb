﻿Imports Lidgren.Network
Imports IHProspekt.Objects
Imports IHProspekt.Network
Imports IHProspekt.Core
Namespace Network.HandleData
    Public Module HandleData
        Public Sub HandleDataPackets(ByVal PacketNum As Integer, ByRef Data As NetIncomingMessage)
            ' Checks which is the command to run
            If PacketNum = 0 Then Exit Sub
            If PacketNum = ServerPackets.Alert Then HandleData.Alert(Data)
            If PacketNum = ServerPackets.LoginOk Then HandleData.LoginOk(Data)
            If PacketNum = ServerPackets.RegisterOk Then HandleData.RegisterOk(Data)
            If PacketNum = ServerPackets.Player Then HandleData.PlayerData(Data)
            If PacketNum = ServerPackets.ClearPlayer Then HandleData.ClearPlayer(Data)
            If PacketNum = ServerPackets.Position Then HandleData.Position(Data)
            If PacketNum = ServerPackets.Message Then HandleData.Message(Data)
            If PacketNum = ServerPackets.Access Then HandleData.Access(Data)
            If PacketNum = ServerPackets.Visible Then HandleData.Visible(Data)
            If PacketNum = ServerPackets.NPC Then HandleData.NPCData(Data)
            If PacketNum = ServerPackets.NPCPosition Then HandleData.NPCPosition(Data)
            If PacketNum = ServerPackets.MapData Then HandleData.MapData(Data)
            If PacketNum = ServerPackets.TilesetData Then HandleData.TilesetData(Data)
        End Sub

        Public Sub Alert(ByRef Data As NetIncomingMessage)
            MessageBox.Show(Data.ReadString)

            faderState = 2
            faderAlpha = 0
            sEmail = vbNullString
            sPass = vbNullString
            sHidden = vbNullString
            sCharacter = vbNullString
            sMessage = vbNullString
            curTextbox = 0
            loginSent = False
            curMenu = MenuScene.MenuEnum.Main
        End Sub

        Public Sub RegisterOk(ByRef Data As NetIncomingMessage)
            loginSent = False
            curMenu = MenuScene.MenuEnum.Creation
        End Sub

        Public Sub LoginOk(ByRef Data As NetIncomingMessage)
            MyIndex = Data.ReadInt32
            faderState = 3
            faderAlpha = 0
        End Sub

        Public Sub PlayerData(ByRef Data As NetIncomingMessage)
            Dim tempIndex As Integer

            tempIndex = Data.ReadInt32
            PlayerCount = Data.ReadInt32
            ReDim Preserve Player(0 To PlayerCount)
            If IsNothing(Player(tempIndex)) Then Player(tempIndex) = New Players
            Player(tempIndex).Load(Data.ReadString, Data.ReadInt32, Data.ReadInt32, Data.ReadInt32, Data.ReadByte, Data.ReadByte, Data.ReadBoolean)
        End Sub

        Public Sub ClearPlayer(ByRef Data As NetIncomingMessage)
            Dim tempIndex As Integer
            tempIndex = Data.ReadInt32
            PlayerCount = Data.ReadInt32
            Player(tempIndex) = Nothing
        End Sub

        Public Sub Position(ByRef Data As NetIncomingMessage)
            Dim tempIndex As Integer
            tempIndex = Data.ReadInt32
            Player(tempIndex).Moving = Data.ReadBoolean
            Player(tempIndex).X = Data.ReadInt32
            Player(tempIndex).Y = Data.ReadInt32
            Player(tempIndex).Dir = Data.ReadByte
            Select Case Player(tempIndex).Dir
                Case DirEnum.Up
                    Player(tempIndex).YOffset = picY
                Case DirEnum.Down
                    Player(tempIndex).YOffset = picY * -1
                Case DirEnum.Left
                    Player(tempIndex).XOffset = picX
                Case DirEnum.Right
                    Player(tempIndex).XOffset = picX * -1
            End Select
        End Sub

        Public Sub Message(ByRef Data As NetIncomingMessage)
            Dim Message As String, Messages As String(), I As Integer
            ReDim Preserve Messages(0 To maxChatLines)
            Message = Data.ReadString
            ' Check if the message is larger then maxChatChars
            If (Message.Length > maxChatChars) Then
                'Resize the Messages array
                ReDim Preserve Messages(0 To Convert.ToInt32(Message.Length / maxChatChars))
                'Split the message into maxChatChars character chunks
                Messages = WordWarp(Message, maxChatChars)
            Else
                'Resize the Messages array
                ReDim Preserve Messages(0 To 0)
                Messages(0) = Message
            End If

            'Loop through all chunks of the Message and display
            For X As Integer = 0 To (Messages.Length - 1)
                For I = 1 To maxChatLines
                    If Len(Trim(chatbuffer(I))) = 0 Then
                        chatbuffer(I) = Messages(X)
                        Exit For
                    End If
                Next I

                For I = 1 To maxChatLines
                    If I < maxChatLines Then
                        chatbuffer(I) = chatbuffer(I + 1)
                    Else
                        chatbuffer(I) = Messages(X)
                    End If
                Next I
            Next X
        End Sub

        Public Sub Access(ByRef Data As NetIncomingMessage)
            Dim tempIndex As Integer
            tempIndex = Data.ReadInt32
            Player(tempIndex).AccessMode = Data.ReadByte
        End Sub

        Public Sub Visible(ByRef Data As NetIncomingMessage)
            Dim tempIndex As Integer
            tempIndex = Data.ReadInt32
            Player(tempIndex).Visible = Data.ReadBoolean
        End Sub

        Public Sub NPCData(ByRef Data As NetIncomingMessage)
            Dim tempIndex As Integer, invsize As Integer, num As Integer
            tempIndex = Data.ReadInt32
            num = Data.ReadInt32
            ReDim Preserve NPC(0 To num)
            If IsNothing(NPC(tempIndex)) Then NPC(tempIndex) = New NPCs
            NPC(tempIndex).Base.Name = Data.ReadString
            NPC(tempIndex).Base.Sprite = Data.ReadInt32
            NPC(tempIndex).Base.ID = Data.ReadInt32
            NPC(tempIndex).Base.Level = Data.ReadInt32
            NPC(tempIndex).Base.Health = Data.ReadInt32
            NPC(tempIndex).Base.X = Data.ReadInt32
            NPC(tempIndex).Base.Y = Data.ReadInt32
            NPC(tempIndex).Base.Dir = Data.ReadInt32
            invsize = Data.ReadInt32
            ReDim NPC(tempIndex).Base.Inventory(0 To invsize)
            For l As Integer = 0 To invsize
                NPC(tempIndex).Base.Inventory(l) = Data.ReadInt32
            Next
            NPCCount = num
        End Sub

        Public Sub NPCPosition(ByRef Data As NetIncomingMessage)
            Dim tempIndex As Integer
            tempIndex = Data.ReadInt32
            NPC(tempIndex).Moving = Data.ReadBoolean
            NPC(tempIndex).X = Data.ReadInt32
            NPC(tempIndex).Y = Data.ReadInt32
            NPC(tempIndex).Dir = Data.ReadByte
            Select Case NPC(tempIndex).Dir
                Case DirEnum.Up
                    NPC(tempIndex).YOffset = picY
                Case DirEnum.Down
                    NPC(tempIndex).YOffset = picY * -1
                Case DirEnum.Left
                    NPC(tempIndex).XOffset = picX
                Case DirEnum.Right
                    NPC(tempIndex).XOffset = picX * -1
            End Select

        End Sub

        Public Sub MapData(ByRef Data As NetIncomingMessage)
            Dim sTileData As TileData
            Map = New Maps
            Map.Name = Data.ReadString
            Map.MaxX = Data.ReadInt32
            Map.MaxY = Data.ReadInt32
            Map.Alpha = Data.ReadByte
            Map.Red = Data.ReadByte
            Map.Green = Data.ReadByte
            Map.Blue = Data.ReadByte
            For i As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
                Map.ReSizeTileData(i, New Integer() {Map.MaxX, Map.MaxY})
                Map.Layer(i) = New LayerData(Map.MaxX, Map.MaxY)
                For x As Integer = 0 To Map.MaxX
                    For y As Integer = 0 To Map.MaxY
                        sTileData = New TileData
                        sTileData.Tileset = Data.ReadInt32
                        sTileData.X = Data.ReadInt32
                        sTileData.Y = Data.ReadInt32
                        Map.Layer(i).Tiles(x, y) = sTileData
                    Next y
                Next x
            Next i
        End Sub

        Public Sub TilesetData(ByRef data As NetIncomingMessage)
            Dim tempIndex As Integer
            tempIndex = data.ReadInt32
            TilesetCount = data.ReadInt32
            ReDim Preserve Tileset(0 To TilesetCount)
            Tileset(tempIndex) = New Tilesets
            Tileset(tempIndex).ID = data.ReadInt32
            Tileset(tempIndex).Name = data.ReadString
            Tileset(tempIndex).MaxX = data.ReadInt32
            Tileset(tempIndex).MaxY = data.ReadInt32
            Tileset(tempIndex).ResizeTileData(New Integer() {Tileset(tempIndex).MaxX, Tileset(tempIndex).MaxY})
            For x As Integer = 0 To Tileset(tempIndex).MaxX
                For y As Integer = 0 To Tileset(tempIndex).MaxY
                    Tileset(tempIndex).Tile(x, y) = data.ReadByte
                Next y
            Next x
        End Sub
    End Module
End Namespace