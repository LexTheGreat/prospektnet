Imports System.ComponentModel

Module MapEditor
    Private index As Integer = -1
    Private curTileSet As Integer
    Private curLayer As Byte
    Private editorProperty As New EditorProperties()
    Private mapMouseRect As Rectangle, mapSrcRect As Rectangle
    Private selectMouseRect As Rectangle, selectSrcRect As Rectangle

    Class EditorProperties
        ' ProptyGrid Functions
        Public Class TypeConverter
            Inherits StringConverter

            Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
                Return True
            End Function

            Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
                Return New StandardValuesCollection(New String() {"Ground", "Ground Mask", "Fringe", "Fringe Mask"})
            End Function
        End Class

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

        <TypeConverter(GetType(TypeConverter)), _
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
            Me.lyr = "Ground"
            Me.curPos = True
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
                Case Else
                    Return 0
            End Select
        End Function
    End Class

    Public Sub Init()
        If Not IsNothing(Map) Then
            For Each mp In Map
                If Not IsNothing(mp) Then
                    EditorWindow.lstMaps.Items.Add(mp.Name)
                End If
            Next
        End If
        EditorWindow.proptMapData.SelectedObject = vbNull
        EditorWindow.proptMapEditorData.SelectedObject = editorProperty
        curLayer = editorProperty.GetLayer
        If countTileset > 0 Then
            For I As Integer = 1 To countTileset
                EditorWindow.mapCmbTileSet.Items.Add(pathTilesets & I & ".png")
            Next
        End If
        EditorWindow.mapCmbTileSet.SelectedIndex = 0
    End Sub

    Public Sub Load(ByVal i As Integer)
        Dim MapMax As Integer() = GetMapMax(), MapVisible As Integer() = GetMapVisible()
        index = i
        If i < 0 Then Exit Sub
        EditorWindow.tabMap.Text = Map(i).Name
        EditorWindow.proptMapData.SelectedObject = Map(i)
        EditorWindow.mapScrlH.Maximum = Map(i).MaxX
        EditorWindow.mapScrlV.Maximum = Map(i).MaxY
        EditorWindow.mapPreview.Invalidate()

        If MapMax(0) > MapVisible(0) Then
            EditorWindow.mapScrlH.Maximum = MapMax(0) - MapVisible(0)
        Else
            EditorWindow.mapScrlH.Maximum = 1
        End If

        If MapMax(1) > MapVisible(1) Then
            EditorWindow.mapScrlV.Maximum = MapMax(1) - MapVisible(1)
        Else
            EditorWindow.mapScrlV.Maximum = 1
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
        Load(index)
    End Sub

    Public Sub Undo()
        MapData.LoadMaps()
        EditorWindow.tabMap.Text = "Map"
        EditorWindow.proptMapData.SelectedObject = vbNull
        index = -1
        EditorWindow.mapPreview.Invalidate()
    End Sub

    Public Sub Verify()
        Dim MapMax As Integer() = GetMapMax(), MapVisible As Integer() = GetMapVisible()
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

        If MapMax(0) > MapVisible(0) Then
            EditorWindow.mapScrlH.Maximum = MapMax(0) - MapVisible(0)
        Else
            EditorWindow.mapScrlH.Maximum = 1
        End If

        If MapMax(1) > MapVisible(1) Then
            EditorWindow.mapScrlV.Maximum = MapMax(1) - MapVisible(1)
        Else
            EditorWindow.mapScrlV.Maximum = 1
        End If

        Map(index).ReSizeTileData(curLayer, New Integer() {Map(index).MaxX, Map(index).MaxY})

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
        curTileSet = CInt(EditorWindow.mapCmbTileSet.SelectedItem.ToString.Replace(pathTilesets, vbNullString).Replace(".png", vbNullString))
    End Sub

    Public Sub mapPreview_MouseMove(e As MouseEventArgs)
        mapMouseRect = New Rectangle(SnapTo(e.X, picX, EditorWindow.mapPreview.Width), SnapTo(e.Y, picY, EditorWindow.mapPreview.Height), picX, picY)
    End Sub

    Public Sub mapPreview_MouseLeave(e As EventArgs)
        selectSrcRect = New Rectangle(0, 0, 0, 0)
        mapMouseRect = New Rectangle(0, 0, 0, 0)
    End Sub

    Public Sub mapPreview_MouseDown(e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            mapSrcRect = mapMouseRect
            PlaceTile()
        ElseIf e.Button = MouseButtons.Right Then
            mapSrcRect = New Rectangle(0, 0, 0, 0)
            RemoveTile()
        End If
    End Sub

    Public Sub mapPicTileSet_MouseMove(e As MouseEventArgs)
        selectMouseRect = New Rectangle(SnapTo(e.X, picX, EditorWindow.mapPicTileSet.Width), SnapTo(e.Y, picY, EditorWindow.mapPicTileSet.Height), picX, picY)
    End Sub

    Public Sub mapPicTileSet_MouseLeave(e As EventArgs)
        selectMouseRect = New Rectangle(0, 0, 0, 0)
    End Sub

    Public Sub mapPicTileSet_MouseDown(e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            selectSrcRect = selectMouseRect
        ElseIf e.Button = MouseButtons.Right Then
            selectSrcRect = New Rectangle(0, 0, 0, 0)
        End If
    End Sub

    Public Sub FillLayer()
        Dim newData As TileData
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Or selectSrcRect.Height = 0 Then Exit Sub
        For x As Integer = 0 To Map(index).MaxX - 1
            For y As Integer = 0 To Map(index).MaxY - 1
                newData = New TileData
                newData.Tileset = curTileSet
                newData.X = selectSrcRect.X
                newData.Y = selectSrcRect.Y
                Map(index).SetTileData(curLayer, x, y, newData)
            Next
        Next
    End Sub

    Public Sub ClearLayer()
        Dim newData As TileData
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Then Exit Sub
        For x As Integer = 0 To Map(index).MaxX - 1
            For y As Integer = 0 To Map(index).MaxY - 1
                newData = New TileData
                Map(index).SetTileData(curLayer, x, y, newData)
            Next
        Next
    End Sub

    Public Sub PlaceTile()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Or selectSrcRect.Height = 0 Then Exit Sub
        Dim X As Integer = (mapMouseRect.X + (EditorWindow.mapScrlH.Value * picX)) / picX, Y As Integer = (mapMouseRect.Y + (EditorWindow.mapScrlV.Value * picY)) / picY
        Dim newData As New TileData
        newData.Tileset = curTileSet
        newData.X = selectSrcRect.X
        newData.Y = selectSrcRect.Y

        Map(index).SetTileData(curLayer, X, Y, newData)
    End Sub

    Public Sub RemoveTile()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Then Exit Sub
        Dim X As Integer = (mapMouseRect.X + (EditorWindow.mapScrlH.Value * picX)) / picX, Y As Integer = (mapMouseRect.Y + (EditorWindow.mapScrlV.Value * picY)) / picY
        Dim newData As New TileData
        Map(index).SetTileData(curLayer, X, Y, newData)
    End Sub

    Public Sub DrawMap()
        ' Draw Map Tiles
        DrawMapTiles()
    End Sub

    Public Sub DrawTileset()
        If curTileSet > 0 Then Render.RenderTextureTo(texTileset(curTileSet), 0, 0, 0, 0, Texture(texTileset(curTileSet)).Width, Texture(texTileset(curTileSet)).Height, Texture(texTileset(curTileSet)).Width, Texture(texTileset(curTileSet)).Height)
    End Sub

    Public Sub DrawSelection()
        If curTileSet > 0 Then Render.RenderTextureTo(texGui(1), selectMouseRect.X, selectMouseRect.Y, 0, 0, picX, picY, picX, picY, 200, 0, 255, 0)
    End Sub

    Public Sub DrawMapSelection()
        Render.RenderTextureTo(texGui(1), mapMouseRect.X, mapMouseRect.Y, 0, 0, picX, picY, picX, picY, 200, 0, 255, 0)
    End Sub

    Public Sub DrawMapTiles()
        If index < 0 Then Exit Sub
        Dim MapVisible As Integer() = GetMapVisible()
        Dim ScrlH As Integer = EditorWindow.mapScrlH.Value, ScrlV As Integer = EditorWindow.mapScrlV.Value

        For x As Integer = 0 + ScrlH To MapVisible(0) + ScrlH
            For y As Integer = 0 + ScrlV To MapVisible(1) + ScrlV
                For lyr As Integer = 0 To 3
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
                    Render.RenderTexture(texTileset(Map(index).GetTileData(lyr, x, y).Tileset), (x - ScrlH) * picX, (y - ScrlV) * picY, Map(index).GetTileData(lyr, x, y).X, Map(index).GetTileData(lyr, x, y).Y, picX, picY, picX, picY)
                Next
            Next
        Next
    End Sub

    Public Function GetMapVisible() As Integer()
        If index < 0 Then Return New Integer() {15, 15}
        Dim maxX As Integer, maxY As Integer

        If EditorWindow.mapPreview.Width / picX > Map(index).GetTileData(0).GetUpperBound(0) Then
            maxX = Map(index).GetTileData(0).GetUpperBound(0) - 1
        Else
            maxX = EditorWindow.mapPreview.Width / picX
        End If

        If EditorWindow.mapPreview.Height / picY > Map(index).GetTileData(0).GetUpperBound(1) Then
            maxY = Map(index).GetTileData(0).GetUpperBound(1) - 1
        Else
            maxY = EditorWindow.mapPreview.Height / picY
        End If

        Return New Integer() {maxX - 1, maxY - 1}
    End Function

    Public Function GetMapMax() As Integer()
        If index < 0 Then Return New Integer() {35, 35}
        Dim maxX As Integer, maxY As Integer

        If Map(index).GetTileData(0).GetUpperBound(0) > EditorWindow.mapPreview.Width / picX Then
            maxX = SnapTo(Map(index).GetTileData(0).GetUpperBound(0), picX, EditorWindow.mapPreview.Width)
        Else
            maxX = Map(index).GetTileData(0).GetUpperBound(0) - 1
        End If

        If Map(index).GetTileData(0).GetUpperBound(1) > EditorWindow.mapPreview.Height / picY Then
            maxY = SnapTo(Map(index).GetTileData(0).GetUpperBound(1), picY, EditorWindow.mapPreview.Height)
        Else
            maxY = Map(index).GetTileData(0).GetUpperBound(1) - 1
        End If

        Return New Integer() {maxX - 1, maxY - 1}
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

    Public Function GetImagePart(ByVal SourceImage As Image, ByVal Region As Rectangle) As Image
        If IsNothing(New Bitmap(Region.Width, Region.Height)) Then Return Nothing
        Dim ImagePart As Bitmap = New Bitmap(Region.Width, Region.Height)

        Using G As Graphics = Graphics.FromImage(ImagePart)
            Dim TargetRect As Rectangle = New Rectangle(0, 0, Region.Width, Region.Height)
            Dim SourceRect As Rectangle = Region
            G.DrawImage(SourceImage, TargetRect, SourceRect, GraphicsUnit.Pixel)
        End Using
        Return ImagePart
    End Function

End Module
