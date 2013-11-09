Imports System.ComponentModel
Imports Prospekt.Graphics
Imports IHProspekt.Core

Class TilesetClass
    Private index As Integer = -1
    Private curTileSet As Integer = 1
    Private mapMouseRect As Rectangle, mapSrcRect As Rectangle
    Private selectMouseRect As Rectangle, selectSrcRect As Rectangle

    Public Sub Init()
        If countTileset > 0 Then
            EditorWindow.cmbTilesetEditor.Items.Clear()
            ReDim Preserve Tileset(0 To countTileset)
            For I As Integer = 1 To countTileset
                EditorWindow.cmbTilesetEditor.Items.Add(pathTilesets & I & ".png")
            Next
        End If
        EditorWindow.cmbTilesetEditor.SelectedIndex = 0
    End Sub

    Public Sub Reload()
        SelectTileset()
    End Sub

    Public Sub SelectTileset()
        selectSrcRect = New Rectangle(0, 0, 0, 0)
        curTileSet = CInt(EditorWindow.cmbTilesetEditor.SelectedItem.ToString.Replace(pathTilesets, vbNullString).Replace(".png", vbNullString))
        index = curTileSet
        If Not curTileSet >= 0 Then Exit Sub
        EditorWindow.scrlTilesetEditorX.Minimum = 0
        EditorWindow.scrlTilesetEditorY.Minimum = 0
        If gTexture(texTileset(curTileSet)).Width < EditorWindow.picTilesetEditor.Width Then
            EditorWindow.scrlTilesetEditorX.Maximum = 0
        Else
            EditorWindow.scrlTilesetEditorX.Maximum = (gTexture(texTileset(curTileSet)).Width - EditorWindow.picTilesetEditor.Width) / picX
            EditorWindow.scrlTilesetEditorX.Value = 0
        End If
        If gTexture(texTileset(curTileSet)).Height < EditorWindow.picTilesetEditor.Height Then
            EditorWindow.scrlTilesetEditorY.Maximum = 0
        Else
            EditorWindow.scrlTilesetEditorY.Maximum = (gTexture(texTileset(curTileSet)).Height - EditorWindow.picTilesetEditor.Height) / picY
            EditorWindow.scrlTilesetEditorY.Value = 0
        End If
        If IsNothing(Tileset(index)) Then Tileset(index) = New Tilesets
        Tileset(index).MaxX = gTexture(texTileset(curTileSet)).Width / picX
        Tileset(index).MaxY = gTexture(texTileset(curTileSet)).Height / picY
        Tileset(index).ResizeTileData(New Integer() {Tileset(index).MaxX, Tileset(index).MaxY})
        EditorWindow.txtTilesetName.Text = Tileset(index).Name
    End Sub

    Public Sub picTilesetEditor_MouseMove(e As MouseEventArgs)
        If e.X + (EditorWindow.tileSetScrlX.Value * picX) >= gTexture(texTileset(curTileSet)).Width Or
            e.Y + (EditorWindow.tileSetScrlY.Value * picY) >= gTexture(texTileset(curTileSet)).Height Then Exit Sub
        selectMouseRect = New Rectangle(SnapTo(e.X, picX, EditorWindow.picTilesetEditor.Width), SnapTo(e.Y, picY, EditorWindow.picTilesetEditor.Height), picX, picY)
    End Sub

    Public Sub picTilesetEditor_MouseLeave(e As EventArgs)
        selectMouseRect = New Rectangle(0, 0, 0, 0)
    End Sub

    Public Sub picTilesetEditor_MouseDown(e As MouseEventArgs)
        If e.X + (EditorWindow.scrlTilesetEditorX.Value * picX) >= gTexture(texTileset(curTileSet)).Width Or
            e.Y + (EditorWindow.scrlTilesetEditorY.Value * picY) >= gTexture(texTileset(curTileSet)).Height Then Exit Sub
        Dim selectX As Integer = EditorWindow.scrlTilesetEditorX.Value * picX, selectY As Integer = EditorWindow.scrlTilesetEditorY.Value * picY
        If e.Button = MouseButtons.Left Then
            If Tileset(index).Tile((selectMouseRect.X + selectX) / picX, (selectMouseRect.Y + selectY) / picY) < TileType.COUNT - 1 Then
                Tileset(index).Tile((selectMouseRect.X + selectX) / picX, (selectMouseRect.Y + selectY) / picY) += 1
            Else
                Tileset(index).Tile((selectMouseRect.X + selectX) / picX, (selectMouseRect.Y + selectY) / picY) = 0
            End If
        ElseIf e.Button = MouseButtons.Right Then
            Tileset(index).Tile((selectMouseRect.X + selectX) / picX, (selectMouseRect.Y + selectY) / picY) = TileType.Walkable
        End If
    End Sub

    Public Sub btnSaveTileset_Click(e As EventArgs)
        If curTileSet >= 0 Then
            If Not IsNothing(Tileset(index)) Then
                Tileset(index).ID = curTileSet
                Tileset(index).Save()
            End If
        End If
    End Sub

    Public Sub ClearTileset()
        If Not curTileSet >= 0 Then Exit Sub
        For X As Integer = 0 To Tileset(index).Tile.GetUpperBound(0)
            For Y As Integer = 0 To Tileset(index).Tile.GetUpperBound(1)
                Tileset(index).Tile(X, Y) = TileType.Walkable
            Next
        Next
    End Sub

    Public Sub txtTilesetName_TextChanged()
        If Not curTileSet >= 0 Then Exit Sub
        Tileset(index).Name = EditorWindow.txtTilesetName.Text
    End Sub

    Public Sub DrawTileset()
        Dim ScrlX As Integer = EditorWindow.scrlTilesetEditorX.Value, ScrlY As Integer = EditorWindow.scrlTilesetEditorY.Value
        If curTileSet >= 0 Then Render.RenderTexture(Render.TileEditWindow, texTileset(curTileSet), 0 - ScrlX * picX, 0 - ScrlY * picY, 0, 0, gTexture(texTileset(curTileSet)).Width, gTexture(texTileset(curTileSet)).Height, gTexture(texTileset(curTileSet)).Width, gTexture(texTileset(curTileSet)).Height)
    End Sub

    Public Sub DrawTilesetSelection()
        If Not curTileSet >= 0 Then Exit Sub
        If selectMouseRect.Width > 0 Then Render.RenderRectangle(Render.TileEditWindow, selectMouseRect.X, selectMouseRect.Y, picX, picY, 2, 255, 0, 255, 255)
    End Sub

    Public Sub DrawTileTypes()
        If Not curTileSet > 0 Then Exit Sub
        Dim selectX As Integer = EditorWindow.scrlTilesetEditorX.Value, selectY As Integer = EditorWindow.scrlTilesetEditorY.Value
        For X As Integer = 0 To Tileset(index).Tile.GetUpperBound(0)
            For Y As Integer = 0 To Tileset(index).Tile.GetUpperBound(1)
                Select Case Tileset(index).Tile(X, Y)
                    Case TileType.Blocked
                        Render.RenderRectangle(Render.TileEditWindow, (X - selectX) * picX, (Y - selectY) * picY, picX, picY, 0, 150, 255, 0, 0, True)
                        Verdana.Draw(Render.TileEditWindow, "B", (X - selectX) * picX, (Y - selectY) * picY, SFML.Graphics.Color.White, 22)
                    Case TileType.NPCAvoid
                        Render.RenderRectangle(Render.TileEditWindow, (X - selectX) * picX, (Y - selectY) * picY, picX, picY, 0, 150, 0, 0, 255, True)
                        Verdana.Draw(Render.TileEditWindow, "NA", (X - selectX) * picX, (Y - selectY) * picY, SFML.Graphics.Color.White, 22)
                End Select
            Next Y
        Next X
    End Sub

    Public Function SnapTo(ByVal loc As Integer, ByVal size As Integer, ByVal max As Integer) As Integer
        ' if its less then 0, return 0
        If loc <= 0 Then Return 0
        Do
            ' Remove one until we get to a divisible number of size
            loc = loc - 1
        Loop Until isDivisible(loc, size)

        ' If its over the max return max minus size
        If loc + size >= max Then Return max - size

        ' Return final location
        Return loc
    End Function

    Function isDivisible(x As Integer, d As Integer) As Boolean
        Return (x Mod d) = 0
    End Function
End Class
