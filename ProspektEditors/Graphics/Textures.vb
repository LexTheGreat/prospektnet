Imports SFML.Graphics
Imports IHProspekt.Database
Namespace Graphics
    Module Textures
        ' Textures
        Public texTileset() As Integer
        Public texSprite() As Integer
        Public texItem() As Integer
        Public texButton() As Integer
        Public texGui() As Integer

        ' Texture counts
        Public countTileset As Integer
        Public countSprite As Integer
        Public countItem As Integer
        Public countButton As Integer
        Public countGui As Integer

        ' Global texture
        Public gTexture() As TextureRec

        ' Number of all textures
        Public numTextures As Integer

        Public Structure TextureRec
            Dim Tex As Sprite
            Dim Width As Integer
            Dim Height As Integer
            Dim FilePath As String
        End Structure

        Public Structure GeomRec
            Dim Left As Integer
            Dim Top As Integer
            Dim Width As Integer
            Dim Height As Integer
        End Structure

        Public Sub InitTextures()
            ' Buttons
            countButton = 1
            Do While Exists(pathButtons & countButton & gfxExt)
                ReDim Preserve texButton(0 To countButton)
                texButton(countButton) = Render.cacheTexture(pathButtons & countButton & gfxExt)
                countButton = countButton + 1
            Loop
            countButton = countButton - 1

            ' guis
            countGui = 1
            Do While Exists(pathGui & countGui & gfxExt)
                ReDim Preserve texGui(0 To countGui)
                texGui(countGui) = Render.cacheTexture(pathGui & countGui & gfxExt)
                countGui = countGui + 1
            Loop
            countGui = countGui - 1

            ' sprites
            countSprite = 1
            Do While Exists(pathSprites & countSprite & gfxExt)
                ReDim Preserve texSprite(0 To countSprite)
                texSprite(countSprite) = Render.cacheTexture(pathSprites & countSprite & gfxExt)
                countSprite = countSprite + 1
            Loop
            countSprite = countSprite - 1

            ' tilesets
            countTileset = 1
            Do While Exists(pathTilesets & countTileset & gfxExt)
                ReDim Preserve texTileset(0 To countTileset)
                texTileset(countTileset) = Render.cacheTexture(pathTilesets & countTileset & gfxExt)
                countTileset = countTileset + 1
            Loop
            countTileset = countTileset - 1

            ' items
            countItem = 1
            Do While Exists(pathItems & countItem & gfxExt)
                ReDim Preserve texItem(0 To countItem)
                texItem(countItem) = Render.cacheTexture(pathItems & countItem & gfxExt)
                countItem = countItem + 1
            Loop
            countItem = countItem - 1
        End Sub
    End Module
End Namespace