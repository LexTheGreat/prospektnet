Imports SFML.Graphics
Imports SFML.Window

Public Class Render
    ' Rendering window
    Public Shared Window As RenderWindow
    Public Shared TileWindow As RenderWindow
    Delegate Function Pressed(ByVal index As Integer) As Boolean

    Public Shared Sub Initialize()
        ' Initialize rendering window
        Window = New RenderWindow(EditorWindow.mapPreview.Handle)
        TileWindow = New RenderWindow(EditorWindow.mapPicTileSet.Handle)
        Window.SetFramerateLimit(32)
        TileWindow.SetFramerateLimit(32)
        'Cache and load textures
        InitTextures()
    End Sub

    Public Shared Sub Dispose()
        ' Unload textures
        UnloadTextures()

        ' Unload Sfml object
        If Window.IsOpen Then
            Window.Dispose()
            Window = Nothing
        End If
    End Sub

    ' Initializing a texture
    Public Shared Function cacheTexture(filePath As String) As Long

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

    Public Shared Sub LoadTexture(ByVal TextureNum As Long)
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

    Public Shared Sub UnloadTextures()
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

    Public Shared Sub RenderTexture(ByVal textureNum As Long, ByVal destX As Long, ByVal destY As Long, ByVal srcX As Long, ByVal srcY As Long, ByVal destWidth As Long, ByVal destHeight As Long, ByVal srcWidth As Long, ByVal srcHeight As Long, Optional ByVal A As Byte = 255, Optional ByVal R As Byte = 255, Optional ByVal G As Byte = 255, Optional ByVal B As Byte = 255)
        Dim textureWidth As Integer, textureHeight As Integer

        ' Prevent subscript out range
        If textureNum <= 0 Or textureNum > numTextures Then Exit Sub

        ' texture sizes
        textureWidth = Texture(textureNum).Width
        textureHeight = Texture(textureNum).Height

        ' exit out if we need to
        If textureWidth <= 0 Or textureHeight <= 0 Then Exit Sub

        Texture(textureNum).Tex.Color = New Color(R, G, B, A)
        If destWidth <> srcWidth Or destHeight <> srcHeight Then Texture(textureNum).Tex.Scale = New Vector2f(destWidth / srcWidth, destHeight / srcHeight)
        Texture(textureNum).Tex.TextureRect = New IntRect(srcX, srcY, srcWidth, srcHeight)
        Texture(textureNum).Tex.Position = New Vector2f(destX, destY)
        Texture(textureNum).Tex.Draw(Window, RenderStates.Default)
    End Sub

    Public Shared Sub RenderTextureTo(ByVal textureNum As Long, ByVal destX As Long, ByVal destY As Long, ByVal srcX As Long, ByVal srcY As Long, ByVal destWidth As Long, ByVal destHeight As Long, ByVal srcWidth As Long, ByVal srcHeight As Long, Optional ByVal A As Byte = 255, Optional ByVal R As Byte = 255, Optional ByVal G As Byte = 255, Optional ByVal B As Byte = 255)
        Dim textureWidth As Integer, textureHeight As Integer

        ' Prevent subscript out range
        If textureNum <= 0 Or textureNum > numTextures Then Exit Sub

        ' texture sizes
        textureWidth = Texture(textureNum).Width
        textureHeight = Texture(textureNum).Height

        ' exit out if we need to
        If textureWidth <= 0 Or textureHeight <= 0 Then Exit Sub

        Texture(textureNum).Tex.Color = New Color(R, G, B, A)
        If destWidth <> srcWidth Or destHeight <> srcHeight Then Texture(textureNum).Tex.Scale = New Vector2f(destWidth / srcWidth, destHeight / srcHeight)
        Texture(textureNum).Tex.TextureRect = New IntRect(srcX, srcY, srcWidth, srcHeight)
        Texture(textureNum).Tex.Position = New Vector2f(destX, destY)
        Texture(textureNum).Tex.Draw(TileWindow, RenderStates.Default)
    End Sub
End Class
