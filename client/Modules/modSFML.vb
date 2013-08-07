Imports SFML.Graphics

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

        Texture(TextureNum).Tex.Color = New SFML.Graphics.Color(R, G, B, A)
        Texture(TextureNum).Tex.TextureRect = New IntRect(srcX, srcY, destWidth, destHeight)
        Texture(TextureNum).Tex.Position = New SFML.Window.Vector2f(destX, destY)
        Texture(TextureNum).Tex.Draw(SfmlWindow, SFML.Graphics.RenderStates.Default)

    End Sub
#End Region
    Public Sub renderMenu()
        On Error GoTo errorhandler
        ' don't render
        If frmMain.WindowState = FormWindowState.Minimized Then Exit Sub

        ' Start rendering
        SfmlWindow.Clear(New SFML.Graphics.Color(255, 255, 255))

        If faderState < 2 Then
            If Not faderAlpha = 255 Then renderTexture(texGui(2), (screenWidth * 0.5) - (Texture(texGui(2)).Width * 0.5), (screenHeight * 0.5) - (Texture(texGui(2)).Height * 0.5), 0, 0, Texture(texGui(2)).Width, Texture(texGui(2)).Height, Texture(texGui(2)).Width, Texture(texGui(2)).Height)
            DrawFader()
            Call verdana.Draw("Press space to skip intro...", 2, 2, SFML.Graphics.Color.Blue)
        Else
            ' Render background
            Call DrawBackGround()

            Call renderTexture(texGui(1), 0, screenHeight - 20, 0, 0, screenWidth, 20, 32, 32, 200, 0, 0, 0)

            Call Verdana.Draw(Application.ProductName & " v" & Application.ProductVersion, 5, screenHeight - 18, Color.White)
            Call Verdana.Draw("eatenbrain.com", screenWidth - 5 - Verdana.GetWidth("eatenbrain.com"), screenHeight - 18, Color.White)
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

        ' Render background
        Call DrawBackGround()
        Call Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

        For i = 1 To PlayerHighindex
            If Not IsNothing(Player(i)) Then
                DrawPlayer(i)
                DrawPlayerName(i)
            End If
        Next

        ' End the rendering
        SfmlWindow.Display()
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Public Sub DrawPlayerName(ByVal Index As Integer)
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

    Public Sub DrawPlayer(ByVal Index As Integer)
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
        Call renderTexture(texGui(1), 0, 0, 0, 0, screenWidth, screenHeight, 32, 32, faderAlpha, 0, 0, 0)
    End Sub

    Private Sub DrawMenu()
        ' Buttons
        Call renderButton((screenWidth * 0.5) - (94 * 0.5), (screenHeight * 0.5) - (22 * 0.5) - 15, 94, 22, 1, 2, 1)
        Call renderButton((screenWidth * 0.5) - (147 * 0.5), (screenHeight * 0.5) - (22 * 0.5) + 15, 147, 22, 3, 4, 2)
    End Sub

    Private Sub DrawCredits()
        Call Silkscreen.Draw("Myself (lol)", (screenWidth * 0.5) - (Silkscreen.GetWidth("Myself (lol)", 20) * 0.5), (screenHeight * 0.5) - 18, Color.White, 20)
        Call Silkscreen.Draw("Aaron Krogh", (screenWidth * 0.5) - (Silkscreen.GetWidth("Aaron Krogh", 20) * 0.5), (screenHeight * 0.5) + 18, Color.White, 20)
    End Sub

    Private Sub DrawLogin()
        Call Silkscreen.Draw("USERNAME:", (screenWidth * 0.5) - 150, (screenHeight * 0.5) - 28, Color.White, 20)
        Call renderTexture(texGui(1), (screenWidth * 0.5) - 145, (screenHeight * 0.5) + 5, 0, 0, 290, 20, 32, 32, 200, 0, 0, 0)
        Call verdana.Draw(sUser & chatShowLine, (screenWidth * 0.5) - 140, (screenHeight * 0.5) + 8, Color.White)
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
    Public Sub DrawBackGround()
        Dim X As Long, Y As Long
        For X = 0 To maxX
            For Y = 0 To maxY
                Call renderTexture(texTileset(1), X * picX, Y * picY, 0, 8 * picY, picX, picY, picX, picY)
            Next Y
        Next X
    End Sub
End Module
