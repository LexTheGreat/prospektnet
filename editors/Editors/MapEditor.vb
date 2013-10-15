﻿Imports System.ComponentModel

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
        Dim MapMax As Integer(), MapVisible As Integer()
        EditorWindow.mapScrlX.Value = 0
        EditorWindow.mapScrlY.Value = 0
        If Map.Length > i Then
            index = i
            EditorWindow.tabMap.Text = Map(i).Name
            EditorWindow.proptMapData.SelectedObject = Map(i)
            EditorWindow.mapScrlX.Value = 0
            EditorWindow.mapScrlY.Value = 0
            MapMax = GetMapMax()
            MapVisible = GetMapVisible()
            If MapMax(0) > MapVisible(0) Then
                EditorWindow.mapScrlX.Maximum = MapMax(0)
            Else
                EditorWindow.mapScrlX.Maximum = 1
            End If

            If MapMax(1) > MapVisible(1) Then
                EditorWindow.mapScrlY.Maximum = MapMax(1)
            Else
                EditorWindow.mapScrlY.Maximum = 1
            End If
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
        EditorWindow.tileSetScrlX.Value = 0
        EditorWindow.tileSetScrlY.Value = 0
        EditorWindow.tileSetScrlX.Maximum = Texture(texTileset(curTileSet)).Width / picX
        EditorWindow.tileSetScrlY.Maximum = Texture(texTileset(curTileSet)).Height / picY
        ' Width: 1024, Height: 512
    End Sub

    Public Sub mapPreview_MouseMove(e As MouseEventArgs)
        mapMouseRect = New Rectangle(SnapTo(e.X, picX, EditorWindow.mapPreview.Width), SnapTo(e.Y, picY, EditorWindow.mapPreview.Height), picX, picY)
    End Sub

    Public Sub mapPreview_MouseLeave(e As EventArgs)
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
        selectMouseRect = New Rectangle(SnapTo(e.X, picX, EditorWindow.TileSetPreview.Width), SnapTo(e.Y, picY, EditorWindow.TileSetPreview.Height), picX, picY)
    End Sub

    Public Sub mapPicTileSet_MouseLeave(e As EventArgs)
        selectMouseRect = New Rectangle(0, 0, 0, 0)
    End Sub

    Public Sub mapPicTileSet_MouseDown(e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim selectX As Integer = EditorWindow.tileSetScrlX.Value * picX, selectY As Integer = EditorWindow.tileSetScrlY.Value * picY
            selectSrcRect = New Rectangle(selectMouseRect.X + selectX, selectMouseRect.Y + selectY, selectMouseRect.Width, selectMouseRect.Height)
        ElseIf e.Button = MouseButtons.Right Then
            selectSrcRect = New Rectangle(0, 0, 0, 0)
        End If
    End Sub

    Public Sub FillLayer()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Or selectSrcRect.Height = 0 Then Exit Sub
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
    End Sub

    Public Sub ClearLayer()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Or selectSrcRect.Height = 0 Then Exit Sub
        Dim newData(Map(index).MaxX, Map(index).MaxY) As TileData
        For x As Integer = 0 To Map(index).MaxX
            For y As Integer = 0 To Map(index).MaxY
                newData(x, y) = New TileData
                Map(index).SetTileData(curLayer, newData)
            Next
        Next
    End Sub

    Public Sub PlaceTile()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Or selectSrcRect.Height = 0 Then Exit Sub
        Dim X As Integer = (mapMouseRect.X + (EditorWindow.mapScrlX.Value * picX)) / picX, Y As Integer = (mapMouseRect.Y + (EditorWindow.mapScrlY.Value * picY)) / picY
        Dim newData As New TileData
        newData.Tileset = curTileSet
        newData.X = selectSrcRect.X
        newData.Y = selectSrcRect.Y

        Map(index).SetTileData(curLayer, X, Y, newData)
    End Sub

    Public Sub RemoveTile()
        If index < 0 Then Exit Sub
        If IsNothing(Map(index)) Then Exit Sub
        Dim X As Integer = (mapMouseRect.X + (EditorWindow.mapScrlX.Value * picX)) / picX, Y As Integer = (mapMouseRect.Y + (EditorWindow.mapScrlY.Value * picY)) / picY
        Dim newData As New TileData
        Map(index).SetTileData(curLayer, X, Y, newData)
    End Sub

    Public Sub DrawTileset()
        Dim ScrlX As Integer = EditorWindow.tileSetScrlX.Value, ScrlY As Integer = EditorWindow.tileSetScrlY.Value
        If curTileSet > 0 Then Render.RenderTileTexture(texTileset(curTileSet), 0, 0, ScrlX * picX, ScrlY * picY, Texture(texTileset(curTileSet)).Width, Texture(texTileset(curTileSet)).Height, Texture(texTileset(curTileSet)).Width, Texture(texTileset(curTileSet)).Height)
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
                    Render.RenderMapTexture(texTileset(Map(index).GetTileData(lyr, x, y).Tileset), (x - ScrlX) * picX, (y - ScrlY) * picY, Map(index).GetTileData(lyr, x, y).X, Map(index).GetTileData(lyr, x, y).Y, picX, picY, picX, picY)
                Next
            Next
        Next
    End Sub

    Public Sub DrawMapSelection()
        If selectSrcRect.Width > 0 And mapMouseRect.Width > 0 Then
            Render.RenderMapTexture(texTileset(curTileSet), mapMouseRect.X, mapMouseRect.Y, selectSrcRect.X, selectSrcRect.Y, picX, picY, picX, picY)
        End If
        If mapMouseRect.Width > 0 Then Render.RenderRectangle(Render.Window, mapMouseRect.X, mapMouseRect.Y, picX, picY, 2, 255, 255, 215, 0)

        ' Needed to fix some weird bug that only draws the second text
        Verdana.Draw("Blank", 0, 0, SFML.Graphics.Color.Transparent)
        If editorProperty.curPos And mapMouseRect.Width > 0 Then
            Dim ScrlX As Integer = EditorWindow.mapScrlX.Value, ScrlY As Integer = EditorWindow.mapScrlY.Value
            Dim DrawX As Integer = mapMouseRect.X / picX + ScrlX, DrawY As Integer = mapMouseRect.Y / picY + ScrlY
            Verdana.Draw("X: " & DrawX & " - Y: " & DrawY, mapMouseRect.X, mapMouseRect.Y, SFML.Graphics.Color.White, 9)
        End If
    End Sub

    Public Sub DraMapOverlay()
        If index < 0 Then Exit Sub
        Dim DrawRect As New Rectangle(0, 0, EditorWindow.mapPreview.Width * 2, EditorWindow.mapPreview.Height)
        Render.RenderRectangle(Render.Window, 0, 0, EditorWindow.mapPreview.Width * 2, EditorWindow.mapPreview.Height, 2, Map(index).Alpha, Map(index).Red, Map(index).Green, Map(index).Blue, True)
    End Sub

    Public Function GetMapVisible() As Integer()
        If index < 0 Then Return New Integer() {10, 10}
        Dim maxX As Integer, maxY As Integer

        If EditorWindow.mapPreview.Width / picX > Map(index).MaxX Then
            maxX = Map(index).MaxX
        Else
            maxX = EditorWindow.mapPreview.Width / picX
        End If

        If EditorWindow.mapPreview.Height / picY > Map(index).MaxY Then
            maxY = Map(index).MaxY
        Else
            maxY = EditorWindow.mapPreview.Height / picY
        End If

        Return New Integer() {maxX - 1, maxY - 1}
    End Function

    Public Function GetMapMax() As Integer()
        If index < 0 Then Return New Integer() {34, 34}
        Dim maxX As Integer, maxY As Integer, visible As Integer() = GetMapVisible()

        maxX = Map(index).MaxX - visible(0) - 1
        maxY = Map(index).MaxY - visible(1) - 1

        Return New Integer() {maxX, maxY}
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
End Module