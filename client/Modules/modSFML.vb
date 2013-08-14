Imports SFML.Graphics
Imports SFML.Window

Module modSFML
#Region "General"
    ' DirectX8 window
    Public SfmlWindow As RenderWindow

    ' Global texture
    Public Texture() As TextureRec

    ' Textures
    Public texTileset() As Integer
    Public texSprite() As Integer
    Public texButton() As Integer
    Public texGui() As Integer

    ' Texture counts
    Private countTileset As Integer
    Private countSprite As Integer
    Private countButton As Integer
    Private countGui As Integer

    ' Number of graphic files
    Public numTextures As Integer

    ' Global Texture
    Public Structure TextureRec
        Dim Tex As Sprite
        Dim Width As Integer
        Dim Height As Integer
        Dim FilePath As String
    End Structure

    ' ********************
    ' ** Initialization **
    ' ********************
    Public Sub InitSFML()
        ' Initialize rendering window
        SfmlWindow = New RenderWindow(frmMain.Handle)
        SfmlWindow.SetFramerateLimit(32)

        'Cache and load textures
        InitTextures()
    End Sub

    Private Sub InitTextures()
        ' Buttons
        countButton = 1
        Do While fileExist(pathButtons & countButton & gfxExt)
            ReDim Preserve texButton(0 To countButton)
            texButton(countButton) = cacheTexture(pathButtons & countButton & gfxExt)
            countButton = countButton + 1
        Loop
        countButton = countButton - 1

        ' guis
        countGui = 1
        Do While fileExist(pathGui & countGui & gfxExt)
            ReDim Preserve texGui(0 To countGui)
            texGui(countGui) = cacheTexture(pathGui & countGui & gfxExt)
            countGui = countGui + 1
        Loop
        countGui = countGui - 1

        ' sprites
        countSprite = 1
        Do While fileExist(pathSprites & countSprite & gfxExt)
            ReDim Preserve texSprite(0 To countSprite)
            texSprite(countSprite) = cacheTexture(pathSprites & countSprite & gfxExt)
            countSprite = countSprite + 1
        Loop
        countSprite = countSprite - 1

        ' tilesets
        countTileset = 1
        Do While fileExist(pathTilesets & countTileset & gfxExt)
            ReDim Preserve texTileset(0 To countTileset)
            texTileset(countTileset) = cacheTexture(pathTilesets & countTileset & gfxExt)
            countTileset = countTileset + 1
        Loop
        countTileset = countTileset - 1
    End Sub

    ' Initializing a texture
    Public Function cacheTexture(filePath As String) As Long

        ' Set the max textures
        numTextures = numTextures + 1
        ReDim Preserve Texture(numTextures)

        ' Set the texture path
        Texture(numTextures).FilePath = filePath

        ' Load texture
        LoadTexture(numTextures)

        ' Return function value
        cacheTexture = numTextures
    End Function

    Public Sub LoadTexture(ByVal TextureNum As Long)
        Dim tempTex As Texture
        Dim tempImg As Image
        Dim Tex_Info As Bitmap = New Bitmap(Texture(TextureNum).FilePath)

        tempImg = New Image(Texture(TextureNum).FilePath)
        tempTex = New Texture(tempImg)
        tempTex.Smooth = False

        ' Create texture
        Texture(TextureNum).Tex = New Sprite(tempTex)

        ' Set texture size
        Texture(TextureNum).Height = Tex_Info.Height
        Texture(TextureNum).Width = Tex_Info.Width
    End Sub

    Public Sub DestroySFML()
        ' Unload textures
        UnloadTextures()

        ' Unload Sfml object
        If SfmlWindow.IsOpen Then
            SfmlWindow.Dispose()
            SfmlWindow = Nothing
        End If
    End Sub

    Public Sub UnloadTextures()
        Dim i As Long

        ' Reload the textures
        If numTextures > 0 Then
            For i = 1 To numTextures
                If Not Texture(i).Tex Is Nothing Then
                    Texture(i).Tex.Dispose()
                    Texture(i).Tex = Nothing
                End If
            Next
        End If
    End Sub

    Public Sub renderTexture(ByVal TextureNum As Long, ByVal destX As Long, ByVal destY As Long, ByVal srcX As Long, ByVal srcY As Long, ByVal destWidth As Long, ByVal destHeight As Long, ByVal srcWidth As Long, ByVal srcHeight As Long, Optional ByVal A As Byte = 255, Optional ByVal R As Byte = 255, Optional ByVal G As Byte = 255, Optional ByVal B As Byte = 255)
        Dim TextureWidth As Integer, TextureHeight As Integer

        ' Prevent subscript out range
        If TextureNum <= 0 Then Exit Sub

        ' texture sizes
        TextureWidth = Texture(TextureNum).Width
        TextureHeight = Texture(TextureNum).Height

        ' exit out if we need to
        If TextureWidth <= 0 Or TextureHeight <= 0 Then Exit Sub

        Texture(TextureNum).Tex.Color = New Color(R, G, B, A)
        Texture(TextureNum).Tex.Scale = New Vector2f(destWidth / srcWidth, destHeight / srcHeight)
        Texture(TextureNum).Tex.TextureRect = New IntRect(srcX, srcY, srcWidth, srcHeight)
        Texture(TextureNum).Tex.Position = New Vector2f(destX, destY)
        Texture(TextureNum).Tex.Draw(SfmlWindow, RenderStates.Default)

    End Sub
#End Region
    Public Sub renderMenu()
        On Error GoTo errorhandler
        ' don't render
        If frmMain.WindowState = FormWindowState.Minimized Then Exit Sub

        ' Start rendering
        SfmlWindow.Clear(New Color(255, 255, 255))

        If faderState < 2 Then
            If Not faderAlpha = 255 Then renderTexture(texGui(2), (ClientConfig.screenWidth * 0.5) - (Texture(texGui(2)).Width * 0.5), (ClientConfig.screenHeight * 0.5) - (Texture(texGui(2)).Height * 0.5), 0, 0, Texture(texGui(2)).Width, Texture(texGui(2)).Height, Texture(texGui(2)).Width, Texture(texGui(2)).Height)
            DrawFader()
            Call Verdana.Draw("Press 'SPACE' to skip intro", 2, 2, New Color(100, 100, 100, 255))
        Else
            ' Render background
            Call DrawBackGround()

            Call renderTexture(texGui(1), 0, ClientConfig.ScreenHeight - 20, 0, 0, ClientConfig.ScreenWidth, 20, 32, 32, 200, 0, 0, 0)
            Call renderTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 200, (ClientConfig.ScreenHeight * 0.5) - 100, 0, 0, 400, 200, 32, 32, 120, 0, 0, 0)
            Call renderTexture(texGui(3), (ClientConfig.ScreenWidth * 0.5) - (Texture(texGui(3)).Width * 0.5), 0, 0, 0, Texture(texGui(3)).Width, Texture(texGui(3)).Height, Texture(texGui(3)).Width, Texture(texGui(3)).Height)

            Call Verdana.Draw(Application.ProductName & " v" & Application.ProductVersion, 5, ClientConfig.screenHeight - 18, Color.White)
            Call Verdana.Draw("eatenbrain.com", ClientConfig.screenWidth - 5 - Verdana.GetWidth("eatenbrain.com"), ClientConfig.screenHeight - 18, Color.White)
            Call Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

            Select Case curMenu
                Case MenuEnum.Main : DrawMenu()
                Case MenuEnum.Login : DrawLogin()
                Case MenuEnum.Credits : DrawCredits()
            End Select

            DrawFader()
        End If

        ' End the rendering
        SfmlWindow.Display()
        Exit Sub
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Public Sub renderGame()
        Dim i As Integer
        On Error GoTo errorhandler
        ' don't render
        If frmMain.WindowState = FormWindowState.Minimized Then Exit Sub

        ' Start rendering
        SfmlWindow.Clear(New Color(255, 255, 255))

        ' Render map tiles
        Call DrawMapTiles()
        Call Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

        For i = 1 To PlayerHighindex
            If Not IsNothing(Player(i)) Then
                DrawPlayer(i)
                DrawPlayerName(i)
            End If
        Next

        DrawChat()

        ' End the rendering
        SfmlWindow.Display()
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Private Sub DrawPlayerName(ByVal Index As Integer)
        Dim textX As Integer, textY As Integer, Text As String, textSize As Long

        Text = Trim$(Player(Index).Name)
        textSize = Verdana.GetWidth(Trim$(Player(Index).Name))

        textX = Player(Index).X * picX + Player(Index).XOffset + (picX * 0.5) - (textSize * 0.5)
        textY = Player(Index).Y * picY + Player(Index).YOffset - picY

        textY = Player(Index).Y * picY + Player(Index).YOffset - picY

        If Player(Index).Sprite >= 1 Then
            textY = Player(Index).Y * picY + Player(Index).YOffset - (Texture(texSprite(Player(Index).Sprite)).Height / 4) + 12
        End If

        Call Verdana.Draw(Text, textX, textY, Color.White)
    End Sub

    Private Sub DrawPlayer(ByVal Index As Integer)
        Dim Anim As Byte
        Dim X As Integer
        Dim Y As Integer
        Dim Sprite As Integer, spritetop As Integer
        Dim rec As GeomRec

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

        renderTexture(texSprite(Sprite), X, Y, rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Private Sub DrawFader()
        Call renderTexture(texGui(1), 0, 0, 0, 0, ClientConfig.screenWidth, ClientConfig.screenHeight, 32, 32, faderAlpha, 0, 0, 0)
    End Sub

    Private Sub DrawMenu()
        ' Buttons
        Call renderButton((ClientConfig.ScreenWidth * 0.5) - (128 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (32 * 0.5) - 15, 128, 32, 1, 2, 1)
        Call renderButton((ClientConfig.ScreenWidth * 0.5) - (128 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (32 * 0.5) + 15, 128, 32, 3, 4, 2)
    End Sub

    Private Sub DrawCredits()
        Call Verdana.Draw("Eatenbrain", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Eatenbrain") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 42, Color.White)
        Call Verdana.Draw("Thomas 'Deathbeam' Slusny", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Thomas 'Deathbeam' Slusny") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 28, Color.White)
        Call Verdana.Draw("Aaron Krogh", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Aaron Krogh") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 14, Color.White)
        Call Verdana.Draw("Enterbrain", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Enterbrain") * 0.5), (ClientConfig.ScreenHeight * 0.5), Color.White)
        Call Verdana.Draw("First Seed Material", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("First Seed Material") * 0.5), (ClientConfig.ScreenHeight * 0.5) + 14, Color.White)
    End Sub

    Private Sub DrawLogin()
        Call Verdana.Draw("USERNAME:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("USERNAME:"), (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Call renderTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5) - 30, 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 0 Then
            Call Verdana.Draw(sUser & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Else
            Call Verdana.Draw(sUser, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        End If
        Call Verdana.Draw("PASSWORD:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("PASSWORD:"), (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        Call renderTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5), 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 1 Then
            Call Verdana.Draw(sPass & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        Else
            Call Verdana.Draw(sPass, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        End If
    End Sub
    Private Sub renderButton(ByVal X As Integer, ByVal Y As Integer, ByVal W As Integer, ByVal H As Integer, ByVal Norm As Integer, ByVal Hov As Integer, ByVal ButtonIndex As Integer)

        ' Change the button state
        If mouseX > X And mouseX < X + W And mouseY > Y And mouseY < Y + H Then
            ' Hover state
            Call renderTexture(texButton(Hov), X, Y, 0, 0, W, H, W, H)

            ' When the button is clicked
            If mouseLeftDown > 0 Then

                ' Button sound
                playSound("button.ogg")

                ' Handle what the button does
                Select Case ButtonIndex
                    Case 0 ' Nothing
                    Case 1 : curMenu = MenuEnum.Login
                    Case 2 : curMenu = MenuEnum.Credits
                    Case Else : MsgBox("Button not assigned. Report this immediately!")
                End Select
            End If
        Else
            ' Normal state
            Call renderTexture(texButton(Norm), X, Y, 0, 0, W, H, W, H)
        End If
    End Sub
    Private Sub DrawBackGround()
        Dim X As Long, Y As Long
        For X = 0 To maxX
            For Y = 0 To maxY
                Call renderTexture(texTileset(4), X * picX, Y * picY, 0, 8 * picY, picX, picY, picX, picY)
            Next Y
        Next X
    End Sub

    Private Sub DrawMapTiles()
        Dim X As Long, Y As Long
        For X = 0 To maxX
            For Y = 0 To maxY
                Call renderTexture(texTileset(1), X * picX, Y * picY, 0, 8 * picY, picX, picY, picX, picY)
            Next Y
        Next X
    End Sub

    Private Sub DrawChat()
        Dim i As Long
        Call renderTexture(texGui(1), 5, ClientConfig.ScreenHeight - 255, 0, 0, 300, 250, 32, 32, 120, 0, 0, 0)
        Call renderTexture(texGui(1), 5, ClientConfig.ScreenHeight - 25, 0, 0, 300, 20, 32, 32, 200, 0, 0, 0)
        If inChat Then
            Verdana.Draw(sChat & chatShowLine, 8, ClientConfig.ScreenHeight - 22, Color.White)
        Else
            Verdana.Draw("Press 'ENTER' to start chatting", 8, ClientConfig.ScreenHeight - 22, Color.White)
        End If
        For i = 1 To maxChatLines
            Verdana.Draw(chatbuffer(i), 8, ClientConfig.ScreenHeight - 252 + (15 * (i - 1)), Color.White)
        Next
    End Sub
End Module
