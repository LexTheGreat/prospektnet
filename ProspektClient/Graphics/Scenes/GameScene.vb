Imports SFML.Graphics
Imports Prospekt.Input
Imports Prospekt.Graphics
Imports Prospekt.Network
Imports IHProspekt.Objects
Imports IHProspekt.Core

Public Class GameScene
    Public Sub Draw()
        Dim i As Integer
        On Error GoTo errorhandler
        ' don't render
        If GameWindow.WindowState = FormWindowState.Minimized Then Exit Sub

        Maps.Logic.UpdateCamera()

        ' Start rendering
        Render.Window.Clear(New Color(255, 255, 255))

        For i = MapLayerEnum.Ground To MapLayerEnum.GroundMask
            DrawMapTile(i)
        Next

        For i = 1 To NPCCount
            If Not IsNothing(NPC(i)) Then
                DrawNPC(i)
            End If
        Next

        For i = 1 To PlayerCount
            If Not IsNothing(Player(i)) Then
                DrawPlayer(i)
            End If
        Next


        For i = 1 To NPCCount
            If Not IsNothing(NPC(i)) Then
                DrawNPCName(i)
            End If
        Next

        For i = 1 To PlayerCount
            If Not IsNothing(Player(i)) Then
                DrawPlayerName(i)
            End If
        Next

        For i = MapLayerEnum.Fringe To MapLayerEnum.COUNT - 1
            DrawMapTile(i)
        Next

        DrawMapOverlay()

        DrawChat()
        Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

        If GMTools.Visible Then GMTools.Draw()

        ' End the rendering
        Render.Window.Display()
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Public Function KeyDown(ByVal Key As Keys) As Boolean
        Select Case Key
            Case Keys.Return
                If inChat Then
                    If Len(Trim(sChat)) > 0 Then SendData.Message(sChat)
                    sChat = vbNullString
                    vChat = vbNullString
                    inChat = False
                Else
                    inChat = True
                End If
            Case Keys.Back
                If inChat And Len(sChat) > 0 Then
                    sChat = Mid(sChat, 1, Len(sChat) - 1)
                    UpdateVisibleChat()
                End If
            Case Keys.Tab
                If inChat Then
                    If chatMode + 1 < ChatModes.COUNT Then
                        chatMode = chatMode + 1
                        If (chatMode = ChatModes.GM And Player(MyIndex).AccessMode = ACCESS.NONE) Then chatMode = ChatModes.Say
                    Else : chatMode = ChatModes.Say
                    End If
                End If
            Case Keys.G
                If Not inChat Then
                    If Not (Player(MyIndex).AccessMode > ACCESS.NONE) Then Return False
                    If GMTools.Visible Then GMTools.Close() Else GMTools.Show()
                End If
        End Select
    End Function

    Public Function KeyPress(ByVal Key As Char) As Boolean
        If inChat And Not Keyboard.GetKeyState(Keys.Back) And Not Keyboard.GetKeyState(Keys.Return) And Not Keyboard.GetKeyState(Keys.Tab) And Not Keyboard.GetKeyState(Keys.Escape) Then
            sChat = sChat & Key.ToString
            UpdateVisibleChat()
        End If
    End Function

    Private Sub UpdateVisibleChat()
        Dim mode As String = "[" & [Enum].GetName(GetType(ChatModes), chatMode) & "] "
        Dim max As Integer = maxChatChars - mode.Length
        If (sChat.Length > max) Then vChat = sChat.Substring(sChat.Length - max) Else vChat = sChat
    End Sub

    Public Sub DrawPlayerName(ByVal Index As Integer)
        Dim textX As Integer, textY As Integer, Text As String, Access As String, textSize As Integer

        If Not (Index = MyIndex) Then
            If Not (Player(Index).Visible = True) Then Exit Sub
        End If

        Access = DrawPlayerAccess(Index)

        Text = Trim$(Player(Index).Name)
        textSize = Verdana.GetWidth(Text)

        textX = Player(Index).X * picX + Player(Index).XOffset + (picX * 0.5) - (textSize * 0.5) + (Verdana.GetWidth(Access) * 0.5)
        textY = Player(Index).Y * picY + Player(Index).YOffset - picY

        If Player(Index).Sprite >= 1 Then
            textY = Player(Index).Y * picY + Player(Index).YOffset - (gTexture(texSprite(Player(Index).Sprite)).Height / 4) + 12
        End If

        Verdana.Draw(Text, Maps.Logic.ConvertX(textX), Maps.Logic.ConvertY(textY), Color.White)
    End Sub

    Private Function DrawPlayerAccess(ByVal Index As Integer) As String
        Dim textX As Integer, textY As Integer, Text As String = vbNullString, textSize As Integer, TextColor As Color = Color.White

        Select Case Player(Index).AccessMode
            Case ACCESS.NONE
                Return vbNullString
            Case ACCESS.GMIT
                Text = Trim$("(GMIT) ")
                TextColor = Color.Cyan
            Case ACCESS.GM
                Text = Trim$("(GM) ")
                TextColor = Color.Yellow
            Case ACCESS.LEAD_GM
                Text = Trim$("(Lead GM) ")
                TextColor = Color.Green
            Case ACCESS.DEV
                Text = Trim$("(DEV) ")
                TextColor = Color.Magenta
            Case ACCESS.ADMIN
                Text = Trim$("(Admin) ")
                TextColor = Color.Red
        End Select

        textSize = Verdana.GetWidth(Text)

        textX = Player(Index).X * picX + Player(Index).XOffset + (picX * 0.5) - (textSize * 0.5) - (Verdana.GetWidth(Trim$(Player(Index).Name)) * 0.5)
        textY = Player(Index).Y * picY + Player(Index).YOffset - picY

        If Player(Index).Sprite >= 1 Then
            textY = Player(Index).Y * picY + Player(Index).YOffset - (gTexture(texSprite(Player(Index).Sprite)).Height / 4) + 12
        End If

        Verdana.Draw(Text, Maps.Logic.ConvertX(textX), Maps.Logic.ConvertY(textY), TextColor)

        Return Text
    End Function

    Public Sub DrawPlayer(ByVal Index As Integer)
        Dim Anim As Byte
        Dim X As Integer
        Dim Y As Integer
        Dim Sprite As Integer, spritetop As Integer
        Dim rec As GeomRec

        If Not (Index = MyIndex) Then
            If Not (Player(Index).Visible = True) Then Exit Sub
        End If

        ' pre-load sprite for calculations
        Sprite = Player(Index).Sprite
        'SetTexture Tex_Char(Sprite)

        If Sprite < 1 Then Exit Sub

        ' Reset frame
        Anim = 1

        ' walk normally
        Select Case Player(Index).Dir
            Case DirEnum.Up
                If (Player(Index).YOffset > 8) Then Anim = Player(Index).PlayerStep
                spritetop = 0
            Case DirEnum.Down
                If (Player(Index).YOffset < -8) Then Anim = Player(Index).PlayerStep
                spritetop = 2
            Case DirEnum.Left
                If (Player(Index).XOffset > 8) Then Anim = Player(Index).PlayerStep
                spritetop = 3
            Case DirEnum.Right
                If (Player(Index).XOffset < -8) Then Anim = Player(Index).PlayerStep
                spritetop = 1
        End Select

        rec.Top = spritetop * (gTexture(texSprite(Sprite)).Height / 4)
        rec.Height = gTexture(texSprite(Sprite)).Height / 4
        rec.Left = Anim * (gTexture(texSprite(Sprite)).Width / 3)
        rec.Width = gTexture(texSprite(Sprite)).Width / 3

        ' Calculate the X and Y
        X = Player(Index).X * picX + Player(Index).XOffset - ((gTexture(texSprite(Sprite)).Width / 3 - 32) / 2)
        Y = Player(Index).Y * picY + Player(Index).YOffset - ((gTexture(texSprite(Sprite)).Height / 4) - 32) - 4

        Render.RenderTexture(texSprite(Sprite), Maps.Logic.ConvertX(X), Maps.Logic.ConvertY(Y), rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Public Sub DrawNPCName(ByVal Index As Integer)
        Dim textX As Integer, textY As Integer, Text As String, textSize As Integer


        Text = Trim$(NPC(Index).Name)
        textSize = Verdana.GetWidth(Text)

        textX = NPC(Index).X * picX + NPC(Index).XOffset + (picX * 0.5) - (textSize * 0.5)
        textY = NPC(Index).Y * picY + NPC(Index).YOffset - picY

        If NPC(Index).Sprite >= 1 Then
            textY = NPC(Index).Y * picY + NPC(Index).YOffset - (gTexture(texSprite(NPC(Index).Sprite)).Height / 4) + 12
        End If

        Verdana.Draw(Text, Maps.Logic.ConvertX(textX), Maps.Logic.ConvertY(textY), Color.White)
    End Sub

    Public Sub DrawNPC(ByVal Index As Integer)
        Dim Anim As Byte
        Dim X As Integer
        Dim Y As Integer
        Dim Sprite As Integer, spritetop As Integer
        Dim rec As GeomRec

        ' pre-load sprite for calculations
        Sprite = NPC(Index).Sprite
        'SetTexture Tex_Char(Sprite)

        If Sprite < 1 Then Exit Sub

        ' Reset frame
        Anim = 1

        ' walk normally
        Select Case NPC(Index).Dir
            Case DirEnum.Up
                If (NPC(Index).YOffset > 8) Then Anim = NPC(Index).NpcStep
                spritetop = 0
            Case DirEnum.Down
                If (NPC(Index).YOffset < -8) Then Anim = NPC(Index).NpcStep
                spritetop = 2
            Case DirEnum.Left
                If (NPC(Index).XOffset > 8) Then Anim = NPC(Index).NpcStep
                spritetop = 3
            Case DirEnum.Right
                If (NPC(Index).XOffset < -8) Then Anim = NPC(Index).NpcStep
                spritetop = 1
        End Select

        rec.Top = spritetop * (gTexture(texSprite(Sprite)).Height / 4)
        rec.Height = gTexture(texSprite(Sprite)).Height / 4
        rec.Left = Anim * (gTexture(texSprite(Sprite)).Width / 3)
        rec.Width = gTexture(texSprite(Sprite)).Width / 3

        ' Calculate the X and Y
        X = NPC(Index).X * picX + NPC(Index).XOffset - ((gTexture(texSprite(Sprite)).Width / 3 - 32) / 2)
        Y = NPC(Index).Y * picY + NPC(Index).YOffset - ((gTexture(texSprite(Sprite)).Height / 4) - 32) - 4

        Render.RenderTexture(texSprite(Sprite), Maps.Logic.ConvertX(X), Maps.Logic.ConvertY(Y), rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Public Sub DrawChat()
        Dim i As Integer
        Render.RenderTexture(texGui(1), 5, ClientConfig.ScreenHeight - 255, 0, 0, 300, 250, 32, 32, 120, 0, 0, 0)
        Render.RenderTexture(texGui(1), 5, ClientConfig.ScreenHeight - 25, 0, 0, 300, 20, 32, 32, 200, 0, 0, 0)
        If inChat Then
            Dim mode As String = "[" & [Enum].GetName(GetType(ChatModes), chatMode) & "] "
            Verdana.Draw(mode & vChat & chatShowLine, 8, ClientConfig.ScreenHeight - 22, Color.White)
        Else
            Verdana.Draw("Press 'ENTER' to start chatting", 8, ClientConfig.ScreenHeight - 22, Color.White)
        End If
        For i = 1 To maxChatLines
            Verdana.Draw(chatbuffer(i), 8, ClientConfig.ScreenHeight - 252 + (15 * (i - 1)), Color.White)
        Next
    End Sub

    Private Sub DrawMapTile(ByVal i As MapLayerEnum)
        If IsNothing(Map) Then
            Verdana.Draw("Mapdata missing", 10, 10, Color.Black)
            Exit Sub
        End If

        Dim data As TileData
        For X As Integer = TileView.Left To TileView.Right
            For Y As Integer = TileView.Top To TileView.Bottom
                If Maps.Logic.IsValidPoint(X, Y) Then
                    data = Map.Layer(i).Tiles(X, Y)
                    If data.Tileset < 0 Then Continue For
                    Call Render.RenderTexture(texTileset(data.Tileset), Maps.Logic.ConvertX(X * picX), Maps.Logic.ConvertY(Y * picY), data.X, data.Y, picX, picY, picX, picY)
                End If
            Next Y
        Next X
    End Sub

    Private Sub DrawMapOverlay()
        If IsNothing(Map) Then Exit Sub
        Render.RenderRectangle(0, 0, ClientConfig.ScreenWidth, ClientConfig.ScreenHeight, 1, Map.Alpha, Map.Red, Map.Green, Map.Blue, True)
    End Sub
End Class
