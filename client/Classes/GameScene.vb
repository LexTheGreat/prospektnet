Imports SFML.Graphics
Imports SFML.Window

Public Class GameScene
    Public Shared Sub Draw()
        Dim i As Integer
        On Error GoTo errorhandler
        ' don't render
        If GameWindow.WindowState = FormWindowState.Minimized Then Exit Sub

        ' Start rendering
        Render.Window.Clear(New Color(255, 255, 255))

        ' Render map tiles
        DrawMapTiles()
        Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

        For i = 1 To PlayerHighindex
            If Not IsNothing(Player(i)) Then
                DrawPlayer(i)
                DrawPlayerName(i)
            End If
        Next

        For i = 1 To NPCCount
            If Not IsNothing(NPC(i)) Then
                DrawNPC(i)
                DrawNPCName(i)
            End If
        Next

        DrawChat()
        If GMTools.Visible Then GMTools.Draw()

        ' End the rendering
        Render.Window.Display()
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Public Shared Function KeyDown(ByVal Key As Keys) As Boolean
        Select Case Key
            Case Keys.Return
                If inChat Then
                    If Len(Trim(sChat)) > 0 Then SendMessage(sChat)
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
                        If (chatMode = ChatModes.Guild And Player(MyIndex).GuildID < 0) Then
                            If Player(MyIndex).PartyID < 0 Then
                                If Player(MyIndex).GetAccess() = ACCESS.NONE Then chatMode = ChatModes.Say Else chatMode = ChatModes.GM
                            Else
                                chatMode = ChatModes.Party
                            End If
                        End If
                        If (chatMode = ChatModes.Party And Player(MyIndex).PartyID < 0) Then
                            If Player(MyIndex).GetAccess() = ACCESS.NONE Then chatMode = ChatModes.Say Else chatMode = ChatModes.GM
                        End If
                        If (chatMode = ChatModes.GM And Player(MyIndex).GetAccess() = ACCESS.NONE) Then chatMode = ChatModes.Say
                    Else : chatMode = ChatModes.Say
                    End If
                End If
            Case Keys.G
                If Not inChat Then
                    If Not (Player(MyIndex).GetAccess() > ACCESS.NONE) Then Return False
                    If GMTools.Visible Then GMTools.Close() Else GMTools.Show()
                End If
        End Select
    End Function

    Public Shared Function KeyPress(ByVal Key As Char) As Boolean
        If inChat And Not GetKeyState(Keys.Back) And Not GetKeyState(Keys.Return) And Not GetKeyState(Keys.Tab) And Not GetKeyState(Keys.Escape) Then
            sChat = sChat & Key.ToString
            UpdateVisibleChat()
        End If
    End Function

    Private Shared Sub UpdateVisibleChat()
        Dim mode As String = "[" & [Enum].GetName(GetType(ChatModes), chatMode) & "] "
        Dim max As Integer = maxChatChars - mode.Length
        If (sChat.Length > max) Then vChat = sChat.Substring(sChat.Length - max) Else vChat = sChat
    End Sub

    Public Shared Sub DrawPlayerName(ByVal Index As Integer)
        Dim textX As Integer, textY As Integer, Text As String, Access As String, textSize As Integer

        If Not (Index = MyIndex) Then
            If Not (Player(Index).Visible = True) Then Exit Sub
        End If

        Access = DrawPlayerAccess(Index)

        Text = Trim$(Player(Index).Name)
        textSize = Verdana.GetWidth(Text)

        textX = Player(Index).X * picX + Player(Index).XOffset + (picX * 0.5) - (textSize * 0.6) + (Verdana.GetWidth(Access) * 0.5)
        textY = Player(Index).Y * picY + Player(Index).YOffset - picY

        If Player(Index).Sprite >= 1 Then
            textY = Player(Index).Y * picY + Player(Index).YOffset - (Texture(texSprite(Player(Index).Sprite)).Height / 4) + 12
        End If

        Verdana.Draw(Text, textX, textY, Color.White)
    End Sub

    Private Shared Function DrawPlayerAccess(ByVal Index As Integer) As String
        Dim textX As Integer, textY As Integer, Text As String = vbNullString, textSize As Integer, TextColor As Color = Color.White

        Select Case Player(Index).GetAccess()
            Case ACCESS.NONE
                DrawPlayerAccess = vbNullString
                Exit Function
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

        textX = Player(Index).X * picX + Player(Index).XOffset + (picX * 0.5) - (textSize * 0.6) - (Verdana.GetWidth(Trim$(Player(Index).Name)) * 0.5)
        textY = Player(Index).Y * picY + Player(Index).YOffset - picY

        If Player(Index).Sprite >= 1 Then
            textY = Player(Index).Y * picY + Player(Index).YOffset - (Texture(texSprite(Player(Index).Sprite)).Height / 4) + 12
        End If

        Verdana.Draw(Text, textX, textY, TextColor)

        DrawPlayerAccess = Text
    End Function

    Public Shared Sub DrawPlayer(ByVal Index As Integer)
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

        rec.Top = spritetop * (Texture(texSprite(Sprite)).Height / 4)
        rec.Height = Texture(texSprite(Sprite)).Height / 4
        rec.Left = Anim * (Texture(texSprite(Sprite)).Width / 3)
        rec.Width = Texture(texSprite(Sprite)).Width / 3

        ' Calculate the X and Y
        X = Player(Index).X * picX + Player(Index).XOffset - ((Texture(texSprite(Sprite)).Width / 3 - 32) / 2)
        Y = Player(Index).Y * picY + Player(Index).YOffset - ((Texture(texSprite(Sprite)).Height / 4) - 32) - 4

        Render.RenderTexture(texSprite(Sprite), X, Y, rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Public Shared Sub DrawNPC(ByVal Index As Integer)
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
                spritetop = 0
            Case DirEnum.Down
                spritetop = 2
            Case DirEnum.Left
                spritetop = 3
            Case DirEnum.Right
                spritetop = 1
        End Select

        rec.Top = spritetop * (Texture(texSprite(Sprite)).Height / 4)
        rec.Height = Texture(texSprite(Sprite)).Height / 4
        rec.Left = Anim * (Texture(texSprite(Sprite)).Width / 3)
        rec.Width = Texture(texSprite(Sprite)).Width / 3

        ' Calculate the X and Y
        X = NPC(Index).X * picX - ((Texture(texSprite(Sprite)).Width / 3 - 32) / 2)
        Y = NPC(Index).Y * picY - ((Texture(texSprite(Sprite)).Height / 4) - 32) - 4

        Render.RenderTexture(texSprite(Sprite), X, Y, rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Public Shared Sub DrawNPCName(ByVal Index As Integer)
        Dim textX As Integer, textY As Integer, Text As String, textSize As Integer

        Text = Trim$(NPC(Index).Name)
        textSize = Verdana.GetWidth(Text)

        textX = NPC(Index).X * picX + (picX * 0.5) - (textSize * 0.6) + (Verdana.GetWidth(Text) * 0.5)
        textY = NPC(Index).Y * picY - picY

        If NPC(Index).Sprite >= 1 Then
            textY = NPC(Index).Y * picY - (Texture(texSprite(NPC(Index).Sprite)).Height / 4) + 12
        End If

        Verdana.Draw(Text, textX, textY, Color.White)
    End Sub

    Public Shared Sub DrawMapTiles()
        Dim X As Long, Y As Long
        For X = 0 To maxX
            For Y = 0 To maxY
                Render.RenderTexture(texTileset(1), X * picX, Y * picY, 0, 8 * picY, picX, picY, picX, picY)
            Next Y
        Next X
    End Sub

    Public Shared Sub DrawChat()
        Dim i As Long
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
End Class
