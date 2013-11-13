Imports System.ComponentModel
Imports Prospekt.Graphics
Imports IHProspekt.Objects
Imports IHProspekt.Core
Class MapClass
    Private index As Integer = -1
    Private curTileSet As Integer = 1
    Private curLayer As Byte
    Private curNpc As Integer = -1
    Private editorProperty As New EditorProperties()
    Private mapMouseRect As Rectangle, mapSrcRect As Rectangle
    Private selectMouseRect As Rectangle, selectSrcRect As Rectangle
    Private npcImgs As ImageList

    Class EditorProperties
        ' ProptyGrid Functions
        Public Class LayerConverter
            Inherits StringConverter

            Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
                Return True
            End Function

            Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
                Dim stringLayers() As String, i As Integer = 0
                ReDim stringLayers(0)
                For i = 0 To MapLayerEnum.COUNT - 1
                    ReDim Preserve stringLayers(0 To i)
                    stringLayers(i) = [Enum].GetName(GetType(MapLayerEnum), i)
                Next

                Return New StandardValuesCollection(stringLayers)
            End Function
        End Class

        Public Class ModeConverter
            Inherits StringConverter

            Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
                Return True
            End Function

            Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
                Dim stringModes() As String, i As Integer = 0
                ReDim stringModes(0)
                For i = 0 To MapModeEnum.COUNT - 1
                    ReDim Preserve stringModes(0 To i)
                    stringModes(i) = [Enum].GetName(GetType(MapModeEnum), i)
                Next

                Return New StandardValuesCollection(stringModes)
            End Function
        End Class

        <TypeConverter(GetType(ModeConverter)), _
           CategoryAttribute("Editor Mode"), _
           DisplayName("Object Type")> _
        Public Property oMode As String

        <CategoryAttribute("Visible Layers"), _
           DisplayName("Ground")> _
        Public Property l1 As Boolean
        <CategoryAttribute("Visible Layers"), _
           DisplayName("Ground Mask")> _
        Public Property l2 As Boolean
        <CategoryAttribute("Visible Layers"), _
           DisplayName("Fringe")> _
        Public Property l3 As Boolean
        <CategoryAttribute("Visible Layers"), _
           DisplayName("Fringe Mask")> _
        Public Property l4 As Boolean
        <CategoryAttribute("Visible Layers"), _
           DisplayName("Npc's")> _
        Public Property lNpc As Boolean

        <TypeConverter(GetType(LayerConverter)), _
           CategoryAttribute("Selected Layer"), _
           DisplayName("Layer")> _
        Public Property lyr As String

        <CategoryAttribute("Cursor"), _
           DisplayName("Draw Position")> _
        Public Property curPos As Boolean

        Sub New()
            Me.l1 = True
            Me.l2 = True
            Me.l3 = True
            Me.l4 = True
            Me.lNpc = True
            Me.lyr = [Enum].GetName(GetType(MapLayerEnum), 0)
            Me.curPos = True
            Me.oMode = [Enum].GetName(GetType(MapModeEnum), 0)
        End Sub

        Public Function GetLayer() As Byte
            Select Case Me.lyr
                Case "Ground"
                    Return 0
                Case "Ground Mask"
                    Return 1
                Case "Fringe"
                    Return 2
                Case "Fringe Mask"
                    Return 3
                Case "Npc"
                    Return 4
                Case Else
                    Return 0
            End Select
        End Function
    End Class

    Public Sub Init()
        If Not IsNothing(Map) Then
            EditorWindow.lstMaps.Items.Clear()
            For Each mp In Map
                If Not IsNothing(mp) Then
                    EditorWindow.lstMaps.Items.Add(mp.Name)
                End If
            Next
        End If
        EditorWindow.proptMapData.SelectedObject = vbNull
        EditorWindow.proptMapEditorData.SelectedObject = editorProperty
        curLayer = editorProperty.GetLayer
        If TilesetCount > 0 Then
            EditorWindow.mapCmbTileSet.Items.Clear()
            For I As Integer = 1 To TilesetCount
                EditorWindow.mapCmbTileSet.Items.Add(Tileset(I).Name)
            Next
            EditorWindow.mapCmbTileSet.SelectedIndex = 0
        End If

        If NPCCount > 0 Then
            If Not IsNothing(NPC) Then
                EditorWindow.lstMapNpcs.Items.Clear()
                npcImgs = New ImageList
                npcImgs.ImageSize = New Size(picX, picY)
                For I As Integer = 0 To NPCCount
                    If Not IsNothing(NPC(I)) Then
                        npcImgs.Images.Add(GetNpcImage(I))
                        EditorWindow.lstMapNpcs.Items.Add(NPC(I).Name, I)
                    End If
                Next
            End If
        End If
        EditorWindow.lstMapNpcs.LargeImageList = npcImgs
        EditorWindow.lstMapNpcs.View = View.LargeIcon
    End Sub

    Public Sub Load(ByVal i As Integer)
        EditorWindow.mapScrlX.Value = 0
        EditorWindow.mapScrlY.Value = 0
        If i < 0 Then Exit Sub
        If Map.Length > i Then
            index = i
            EditorWindow.tabMap.Text = Map(i).Name
            EditorWindow.proptMapData.SelectedObject = Map(i)
            If Math.Round(Map(i).MaxX - (EditorWindow.mapPreview.Width / picX), 0) < 0 Then
                EditorWindow.mapScrlX.Maximum = 0
            Else
                EditorWindow.mapScrlX.Maximum = Math.Round(Map(i).MaxX - (EditorWindow.mapPreview.Width / picX), 0)
            End If
            If Math.Round(Map(i).MaxY - (EditorWindow.mapPreview.Height / picX), 0) < 0 Then
                EditorWindow.mapScrlY.Maximum = 0
            Else
                EditorWindow.mapScrlY.Maximum = Math.Round(Map(i).MaxY - (EditorWindow.mapPreview.Height / picX), 0)
            End If
            EditorWindow.mapScrlX.Value = 0
            EditorWindow.mapScrlY.Value = 0
        End If
    End Sub

    Public Sub ReloadList()
        EditorWindow.lstMaps.Items.Clear()
        If Not IsNothing(Map) Then
            For Each mp In Map
                If Not IsNothing(mp) Then
                    EditorWindow.lstMaps.Items.Add(mp.Name)
                End If
            Next
        End If
        EditorWindow.tabMap.Text = "Map"
        EditorWindow.proptMapData.SelectedObject = vbNull
        Load(index)
    End Sub

    Public Sub Reload()
        ReloadList()
        Load(index)
        SelectTileset()
    End Sub

    Public Sub ReloadTilesets()
        If TilesetCount > 0 Then
            EditorWindow.mapCmbTileSet.Items.Clear()
            For I As Integer = 1 To TilesetCount
                EditorWindow.mapCmbTileSet.Items.Add(Tileset(I).Name)
            Next
            EditorWindow.mapCmbTileSet.SelectedIndex = 0
        End If
    End Sub

    Public Sub Undo()
        Maps.Data.LoadMaps()
        EditorWindow.tabMap.Text = "Map"
        EditorWindow.proptMapData.SelectedObject = vbNull
        index = -1
    End Sub

    Public Sub Verify()
        Dim MapVisible As Integer() = GetMapVisible()
        If Map(index).Name = vbNullString Then Map(index).Name = "New Map"
        If Map(index).MaxX < 35 Then Map(index).MaxX = 35
        If Map(index).MaxY < 35 Then Map(index).MaxY = 35
        If Map(index).Alpha > 255 Then Map(index).Alpha = 255
        If Map(index).Alpha < 0 Then Map(index).Alpha = 0
        If Map(index).Red > 255 Then Map(index).Red = 255
        If Map(index).Red < 0 Then Map(index).Red = 0
        If Map(index).Green > 255 Then Map(index).Green = 255
        If Map(index).Green < 0 Then Map(index).Green = 0
        If Map(index).Blue > 255 Then Map(index).Blue = 255
        If Map(index).Blue < 0 Then Map(index).Blue = 0

        ' Refresh data
        EditorWindow.proptMapData.Refresh()
    End Sub

    Public Sub VerifyEditor()
        curLayer = editorProperty.GetLayer

        ' Refresh data
        EditorWindow.proptMapEditorData.Refresh()
    End Sub

    Public Sub SelectTileset()
        selectSrcRect = New Rectangle(0, 0, 0, 0)
        curTileSet = Tilesets.Data.GetTilesetID(EditorWindow.mapCmbTileSet.SelectedItem.ToString)
        EditorWindow.tileSetScrlX.Minimum = 0
        EditorWindow.tileSetScrlY.Minimum = 0
        If gTexture(texTileset(curTileSet)).Width < EditorWindow.mapPicTileset.Width Then
            EditorWindow.tileSetScrlX.Maximum = 0
        Else
            EditorWindow.tileSetScrlX.Maximum = (gTexture(texTileset(curTileSet)).Width - EditorWindow.mapPicTileset.Width) / picX
            EditorWindow.tileSetScrlX.Value = 0
        End If
        If gTexture(texTileset(curTileSet)).Height < EditorWindow.mapPicTileset.Height Then
            EditorWindow.tileSetScrlY.Maximum = 0
        Else
            EditorWindow.tileSetScrlY.Maximum = (gTexture(texTileset(curTileSet)).Height - EditorWindow.mapPicTileset.Height) / picY
            EditorWindow.tileSetScrlY.Value = 0
        End If
    End Sub

    Public Sub SelectNpc(ByVal index As Integer)
        If IsNothing(NPC(index)) Then Exit Sub
        curNpc = index
    End Sub

    Public Sub mapPreview_MouseMove(e As MouseEventArgs)
        If index < 0 Then Exit Sub
        If e.X >= Map(index).MaxX * picX Or e.Y >= Map(index).MaxY * picY Then Exit Sub
        mapMouseRect = New Rectangle(SnapTo(e.X, picX, EditorWindow.mapPreview.Width), SnapTo(e.Y, picY, EditorWindow.mapPreview.Height), picX, picY)
    End Sub

    Public Sub mapPreview_MouseLeave(e As EventArgs)
        mapMouseRect = New Rectangle(0, 0, 0, 0)
    End Sub

    Public Sub mapPreview_MouseDown(e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            mapSrcRect = mapMouseRect
            Place()
        ElseIf e.Button = MouseButtons.Right Then
            mapSrcRect = New Rectangle(0, 0, 0, 0)
            Remove()
        End If
    End Sub

    Public Sub mapPicTileSet_MouseMove(e As MouseEventArgs)
        If e.X + (EditorWindow.tileSetScrlX.Value * picX) >= gTexture(texTileset(curTileSet)).Width Or
            e.Y + (EditorWindow.tileSetScrlY.Value * picY) >= gTexture(texTileset(curTileSet)).Height Then Exit Sub
        selectMouseRect = New Rectangle(SnapTo(e.X, picX, EditorWindow.mapPicTileset.Width), SnapTo(e.Y, picY, EditorWindow.mapPicTileset.Height), picX, picY)
    End Sub

    Public Sub mapPicTileSet_MouseLeave(e As EventArgs)
        selectMouseRect = New Rectangle(0, 0, 0, 0)
    End Sub

    Public Sub mapPicTileSet_MouseDown(e As MouseEventArgs)
        If e.X + (EditorWindow.tileSetScrlX.Value * picX) >= gTexture(texTileset(curTileSet)).Width Or
            e.Y + (EditorWindow.tileSetScrlY.Value * picY) >= gTexture(texTileset(curTileSet)).Height Then Exit Sub
        If e.Button = MouseButtons.Left Then
            Dim selectX As Integer = EditorWindow.tileSetScrlX.Value * picX, selectY As Integer = EditorWindow.tileSetScrlY.Value * picY
            selectSrcRect = New Rectangle(selectMouseRect.X + selectX, selectMouseRect.Y + selectY, selectMouseRect.Width, selectMouseRect.Height)
        ElseIf e.Button = MouseButtons.Right Then
            selectSrcRect = New Rectangle(0, 0, 0, 0)
        End If
    End Sub

    Public Sub ModeChange(ByVal mode As Integer)
        If mode >= MapModeEnum.COUNT Then Exit Sub
        curNpc = -1
        selectSrcRect = New Rectangle(0, 0, 0, 0)
        editorProperty.oMode = [Enum].GetName(GetType(MapModeEnum), mode)
        EditorWindow.proptMapEditorData.Refresh()
    End Sub

    Public Sub FillLayer()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Then Exit Sub
        Select Case (editorProperty.oMode)
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Tile)
                If selectSrcRect.Height = 0 Then Exit Sub
                Dim newData(Map(index).MaxX, Map(index).MaxY) As TileData
                For x As Integer = 0 To Map(index).MaxX
                    For y As Integer = 0 To Map(index).MaxY
                        newData(x, y) = New TileData
                        newData(x, y).Tileset = curTileSet
                        newData(x, y).X = selectSrcRect.X
                        newData(x, y).Y = selectSrcRect.Y
                        Map(index).SetTileData(curLayer, newData)
                    Next
                Next
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Npc)
                If curNpc < 0 Or curNpc > NPCCount Then Exit Select
                Dim tempMapNPC As New MapNPCBase
                For x As Integer = 0 To Map(index).MaxX
                    For y As Integer = 0 To Map(index).MaxY
                        tempMapNPC.Num = curNpc
                        tempMapNPC.X = x
                        tempMapNPC.Y = x
                        tempMapNPC.Dir = DirEnum.Down
                        Map(index).AddNPC(tempMapNPC)
                    Next
                Next
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Attribute)
            Case Else
        End Select
    End Sub

    Public Sub ClearLayer()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Then Exit Sub
        Select Case (editorProperty.oMode)
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Tile)
                Dim newData(Map(index).MaxX, Map(index).MaxY) As TileData
                For x As Integer = 0 To Map(index).MaxX
                    For y As Integer = 0 To Map(index).MaxY
                        newData(x, y) = New TileData
                        Map(index).SetTileData(curLayer, newData)
                    Next
                Next
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Npc)
                ReDim Map(index).Base.NPC(0)
                Map(index).Base.NPCCount = 0
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Attribute)
            Case Else
        End Select
    End Sub

    Public Sub Place()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Then Exit Sub
        Dim X As Integer = (mapMouseRect.X + (EditorWindow.mapScrlX.Value * picX)) / picX, Y As Integer = (mapMouseRect.Y + (EditorWindow.mapScrlY.Value * picY)) / picY
        Select Case (editorProperty.oMode)
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Tile)
                If selectSrcRect.Height = 0 Then Exit Sub
                Dim newData As New TileData
                newData.Tileset = curTileSet
                newData.X = selectSrcRect.X
                newData.Y = selectSrcRect.Y
                Map(index).SetTileData(curLayer, X, Y, newData)
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Npc)
                If curNpc < 0 Or curNpc > NPCCount Then Exit Select
                Dim tempMapNPC As New MapNPCBase
                tempMapNPC.Num = curNpc
                tempMapNPC.X = X
                tempMapNPC.Y = Y
                tempMapNPC.Dir = DirEnum.Down
                Map(index).AddNPC(tempMapNPC)
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Attribute)
            Case Else
        End Select
    End Sub

    Public Sub Remove()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Then Exit Sub
        Dim X As Integer = (mapMouseRect.X + (EditorWindow.mapScrlX.Value * picX)) / picX, Y As Integer = (mapMouseRect.Y + (EditorWindow.mapScrlY.Value * picY)) / picY
        Select Case (editorProperty.oMode)
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Tile)
                Dim newData As New TileData
                Map(index).SetTileData(curLayer, X, Y, newData)
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Npc)
                Map(index).RemoveNpc(X, Y)
            Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Attribute)
            Case Else
        End Select
    End Sub

    Public Sub DrawTileset()
        Dim ScrlX As Integer = EditorWindow.tileSetScrlX.Value, ScrlY As Integer = EditorWindow.tileSetScrlY.Value
        If curTileSet > 0 Then Render.RenderTexture(Render.TileWindow, texTileset(curTileSet), 0 - ScrlX * picX, 0 - ScrlY * picY, 0, 0, gTexture(texTileset(curTileSet)).Width, gTexture(texTileset(curTileSet)).Height, gTexture(texTileset(curTileSet)).Width, gTexture(texTileset(curTileSet)).Height)
    End Sub

    Public Sub DrawTilesetSelection()
        If Not curTileSet > 0 Then Exit Sub
        Dim selectX As Integer = EditorWindow.tileSetScrlX.Value * picX, selectY As Integer = EditorWindow.tileSetScrlY.Value * picY
        If selectMouseRect.Width > 0 Then Render.RenderRectangle(Render.TileWindow, selectMouseRect.X, selectMouseRect.Y, picX, picY, 2, 255, 0, 255, 255)
        If selectSrcRect.Width > 0 Then
            If selectSrcRect.Width > 0 Then Render.RenderRectangle(Render.TileWindow, selectSrcRect.X - selectX, selectSrcRect.Y - selectY, picX, picY, 2, 255, 255, 215, 0)
        End If
    End Sub

    Public Sub DrawMapTiles()
        If index < 0 Then Exit Sub
        Dim MapVisible As Integer() = GetMapVisible()
        Dim ScrlX As Integer = EditorWindow.mapScrlX.Value, ScrlY As Integer = EditorWindow.mapScrlY.Value

        For x As Integer = 0 + ScrlX To MapVisible(0) + ScrlX
            For y As Integer = 0 + ScrlY To MapVisible(1) + ScrlY
                For lyr As Integer = 0 To 3
                    If x >= Map(index).MaxX Or y >= Map(index).MaxY Then Continue For
                    If Map(index).GetTileData(lyr, x, y).Tileset < 0 Then Continue For
                    Select Case lyr
                        Case MapLayerEnum.Ground
                            If Not editorProperty.l1 Then Continue For ' Dont Draw Ground Layer
                        Case MapLayerEnum.GroundMask
                            If Not editorProperty.l2 Then Continue For ' Dont Draw Ground Mask Layer
                        Case MapLayerEnum.Fringe
                            If Not editorProperty.l3 Then Continue For ' Dont Draw Fringe Layer
                        Case MapLayerEnum.FringeMask
                            If Not editorProperty.l4 Then Continue For ' Dont Draw Fringe Mask Layer
                        Case Else
                    End Select
                    Render.RenderTexture(Render.Window, texTileset(Map(index).GetTileData(lyr, x, y).Tileset), ConvertX(x) * picX, ConvertY(y) * picY, Map(index).GetTileData(lyr, x, y).X, Map(index).GetTileData(lyr, x, y).Y, picX, picY, picX, picY, Map(index).Alpha, Map(index).Red, Map(index).Green, Map(index).Blue)
                Next
            Next
        Next
    End Sub

    Public Sub DrawMapNPCs()
        Dim rec As GeomRec, spritetop As Integer
        Dim sprite As Integer
        Dim X As Integer, Y As Integer, dir As Integer
        If index < 0 Then Exit Sub
        If Not editorProperty.lNpc Then Exit Sub ' Dont Draw Npc's

        For I As Integer = 0 To Map(index).Base.NPCCount
            If IsNothing(Map(index).Base.NPC(I)) Then Continue For
            If Map(index).Base.NPC(I).Num < 0 Or Map(index).Base.NPC(I).Num > NPCCount Then Continue For
            sprite = NPC(Map(index).Base.NPC(I).Num).Sprite
            X = Map(index).Base.NPC(I).X
            Y = Map(index).Base.NPC(I).Y - 1
            dir = Map(index).Base.NPC(I).Dir
            Select Case dir
                Case DirEnum.Up : spritetop = 0
                Case DirEnum.Down : spritetop = 2
                Case DirEnum.Left : spritetop = 3
                Case DirEnum.Right : spritetop = 1
            End Select
            rec.Top = spritetop * (gTexture(texSprite(sprite)).Height / 4)
            rec.Height = gTexture(texSprite(sprite)).Height / 4
            rec.Left = 0
            rec.Width = gTexture(texSprite(sprite)).Width / 3
            Render.RenderTexture(Render.Window, texSprite(sprite), ConvertX(X) * picX - (rec.Width / 6), ConvertY(Y) * picY, rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
        Next
    End Sub

    Public Sub DrawMapSelection()
        If mapMouseRect.Width > 0 Then
            Select Case (editorProperty.oMode)
                Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Tile)
                    If selectSrcRect.Width <= 0 Then Exit Select
                    Render.RenderTexture(Render.Window, texTileset(curTileSet), mapMouseRect.X, mapMouseRect.Y, selectSrcRect.X, selectSrcRect.Y, picX, picY, picX, picY)
                Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Npc)
                    If curNpc < 0 Or curNpc > NPCCount Then Exit Select
                    Dim rec As GeomRec
                    Dim sprite As Integer = NPC(curNpc).Sprite
                    rec.Top = 2 * (gTexture(texSprite(sprite)).Height / 4)
                    rec.Height = gTexture(texSprite(sprite)).Height / 4
                    rec.Left = 0
                    rec.Width = gTexture(texSprite(sprite)).Width / 3
                    Render.RenderTexture(Render.Window, texSprite(sprite), mapMouseRect.X - (rec.Width / 6), mapMouseRect.Y - (rec.Height / 2), rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
                Case [Enum].GetName(GetType(MapModeEnum), MapModeEnum.Attribute)
                Case Else
            End Select
        End If
        If mapMouseRect.Width > 0 Then Render.RenderRectangle(Render.Window, mapMouseRect.X, mapMouseRect.Y, picX, picY, 2, 255, 255, 215, 0)

        ' Needed to fix some weird bug that only draws the second text
        Verdana.Draw(Render.Window, "Blank", 0, 0, SFML.Graphics.Color.Transparent)
        If editorProperty.curPos And mapMouseRect.Width > 0 Then
            Dim DrawX As Integer = mapMouseRect.X / picX + EditorWindow.mapScrlX.Value, DrawY As Integer = mapMouseRect.Y / picY + EditorWindow.mapScrlY.Value
            Dim mapX As Integer = mapMouseRect.X, mapY As Integer = mapMouseRect.Y
            If mapY / picY >= (EditorWindow.mapPreview.Height / 2) / picY Then mapY = mapY - (picY / 2) Else mapY = mapY + picY
            Verdana.Draw(Render.Window, "X: " & DrawX & " - Y: " & DrawY, mapX - (picX / 2.5), mapY, SFML.Graphics.Color.White, 9)
        End If
    End Sub

    Public Function GetMapVisible() As Integer()
        If index < 0 Then Return New Integer() {10, 10}
        Dim maxX As Integer, maxY As Integer

        If EditorWindow.mapPreview.Width / picX > Map(index).MaxX Then
            maxX = Map(index).MaxX
        Else
            maxX = Math.Round(EditorWindow.mapPreview.Width / picX, 0)
        End If

        If EditorWindow.mapPreview.Height / picY > Map(index).MaxY Then
            maxY = Map(index).MaxY
        Else
            maxY = Math.Round(EditorWindow.mapPreview.Height / picY, 0)
        End If

        Return New Integer() {maxX, maxY}
    End Function

    Public Function GetNpcImage(ByVal NpcNumber As Integer) As Bitmap
        Dim rec As GeomRec
        Dim sprite As Integer = NPC(NpcNumber).Sprite
        rec.Top = 2 * (gTexture(texSprite(sprite)).Height / 4)
        rec.Height = gTexture(texSprite(sprite)).Height / 4
        rec.Left = 0
        rec.Width = gTexture(texSprite(sprite)).Width / 3

        Dim ImagePart As Bitmap = New Bitmap(rec.Width, rec.Height)
        Using G As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(ImagePart)
            Dim TargetRect As Rectangle = New Rectangle(0, 0, rec.Width, rec.Height)
            Dim SourceRect As Rectangle = New Rectangle(rec.Left, rec.Top, rec.Width, rec.Height)
            G.DrawImage(Bitmap.FromFile(gTexture(texSprite(sprite)).FilePath), TargetRect, SourceRect, GraphicsUnit.Pixel)
        End Using
        Return ImagePart
    End Function

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

    Function ConvertX(ByVal X As Integer) As Integer
        Dim ScrlX As Integer = EditorWindow.mapScrlX.Value
        Return X - ScrlX
    End Function

    Function ConvertY(ByVal Y As Integer) As Integer
        Dim ScrlY As Integer = EditorWindow.mapScrlY.Value
        Return Y - ScrlY
    End Function
End Class
