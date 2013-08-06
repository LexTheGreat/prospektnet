Imports SFML.Graphics

Module modSFML
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
        If NumTextures > 0 Then
            For i = 1 To NumTextures
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
End Module
