﻿Imports SFML.Graphics
Imports SFML.Window
Imports Prospekt.Audio
Namespace Graphics
    Public Class Render
        ' Rendering window
        Public Shared Window As RenderWindow
        Delegate Function Pressed(ByVal index As Integer) As Boolean

        Public Shared Sub Initialize()
            ' Initialize rendering window
            Window = New RenderWindow(GameWindow.Handle)
            Window.SetFramerateLimit(32)

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
        Public Shared Function cacheTexture(filePath As String) As Integer

            ' Set the max textures
            numTextures = numTextures + 1
            ReDim Preserve gTexture(numTextures)

            ' Set the texture path
            gTexture(numTextures).FilePath = filePath

            ' Load texture
            LoadTexture(numTextures)

            ' Return function value
            cacheTexture = numTextures
        End Function

        Public Shared Sub LoadTexture(ByVal TextureNum As Integer)
            Dim tempTex As Texture
            Dim tempImg As Image
            Dim Tex_Info As Bitmap = New Bitmap(gTexture(TextureNum).FilePath)

            tempImg = New Image(gTexture(TextureNum).FilePath)
            tempTex = New Texture(tempImg)
            tempTex.Smooth = False

            ' Create texture
            gTexture(TextureNum).Tex = New Sprite(tempTex)

            ' Set texture size
            gTexture(TextureNum).Height = Tex_Info.Height
            gTexture(TextureNum).Width = Tex_Info.Width
        End Sub

        Public Shared Sub UnloadTextures()
            Dim i As Integer

            ' Reload the textures
            If numTextures > 0 Then
                For i = 1 To numTextures
                    If Not gTexture(i).Tex Is Nothing Then
                        gTexture(i).Tex.Dispose()
                        gTexture(i).Tex = Nothing
                    End If
                Next
            End If
        End Sub

        Public Shared Sub DrawFader()
            RenderTexture(texGui(1), 0, 0, 0, 0, ClientConfig.ScreenWidth, ClientConfig.ScreenHeight, 32, 32, faderAlpha, 0, 0, 0)
        End Sub

        Public Shared Sub RenderTexture(ByVal textureNum As Integer, ByVal destX As Integer, ByVal destY As Integer, ByVal srcX As Integer, ByVal srcY As Integer, ByVal destWidth As Integer, ByVal destHeight As Integer, ByVal srcWidth As Integer, ByVal srcHeight As Integer, Optional ByVal A As Byte = 255, Optional ByVal R As Byte = 255, Optional ByVal G As Byte = 255, Optional ByVal B As Byte = 255)
            Dim textureWidth As Integer, textureHeight As Integer

            ' Prevent subscript out range
            If textureNum <= 0 Or textureNum > numTextures Then Exit Sub

            ' texture sizes
            textureWidth = gTexture(textureNum).Width
            textureHeight = gTexture(textureNum).Height

            ' exit out if we need to
            If textureWidth <= 0 Or textureHeight <= 0 Then Exit Sub

            gTexture(textureNum).Tex.Color = New Color(R, G, B, A)
            If destWidth <> srcWidth Or destHeight <> srcHeight Then gTexture(textureNum).Tex.Scale = New Vector2f(destWidth / srcWidth, destHeight / srcHeight)
            gTexture(textureNum).Tex.TextureRect = New IntRect(srcX, srcY, srcWidth, srcHeight)
            gTexture(textureNum).Tex.Position = New Vector2f(destX, destY)
            gTexture(textureNum).Tex.Draw(Window, RenderStates.Default)

        End Sub

        Public Shared Sub RenderRectangle(ByVal destX As Integer, ByVal destY As Integer, ByVal destWidth As Integer, ByVal destHeight As Integer, ByVal destThickness As Integer, Optional ByVal A As Byte = 255, Optional ByVal R As Byte = 255, Optional ByVal G As Byte = 255, Optional ByVal B As Byte = 255, Optional ByVal Fill As Boolean = False)
            Dim TextRect As New RectangleShape()

            If Fill Then TextRect.FillColor = New Color(R, G, B, A) Else TextRect.FillColor = New Color(Color.Transparent)
            TextRect.OutlineColor = New Color(R, G, B, A)
            TextRect.OutlineThickness = destThickness
            TextRect.Size = New Vector2f(destWidth, destHeight)
            TextRect.Position = New Vector2f(destX, destY)
            TextRect.Draw(Window, RenderStates.Default)
        End Sub

        Public Shared Sub RenderButton(ByVal X As Integer, ByVal Y As Integer, ByVal W As Integer, ByVal H As Integer, ByVal Norm As Integer, ByVal Hov As Integer, ByVal btn As Pressed, ByVal Index As Integer)
            ' Change the button state
            If mouseX > X And mouseX < X + W And mouseY > Y And mouseY < Y + H Then
                ' Hover state
                Render.RenderTexture(texButton(Hov), X, Y, 0, 0, W, H, W, H)

                ' When the button is clicked
                If mouseLeftDown > 0 Then
                    ' Run button if it works
                    If (btn.Invoke(Index)) Then
                        ' Button sound
                        playSound("button.ogg")
                    End If
                End If
            Else
                ' Normal state
                Render.RenderTexture(texButton(Norm), X, Y, 0, 0, W, H, W, H)
            End If
        End Sub
    End Class
End Namespace
