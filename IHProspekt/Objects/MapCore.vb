Imports IHProspekt.Core
Namespace Objects
    <Serializable()> Public Class MapBase
        Public Property ID As Integer
        Public Property Name As String
        Public Property MaxX As Integer
        Public Property MaxY As Integer
        Public Property Color As OverLayColor
        Public Property Layer As LayerData()
        Public Property NPC As MapNPCBase()
        Public Property NPCCount As Integer

        Sub New()
            Me.Name = "New Map"
            Me.MaxX = 35
            Me.MaxY = 35
            Me.NPCCount = -1
            Me.Color = New OverLayColor()
            ReDim Me.Layer(0 To MapLayerEnum.COUNT - 1)
            For x As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
                Me.Layer(x) = New LayerData(Me.MaxX, Me.MaxY)
            Next
            ReDim Me.NPC(0)
        End Sub
    End Class

    <Serializable()> Public Class OverLayColor
        Public Property Alpha As Byte
        Public Property Red As Byte
        Public Property Green As Byte
        Public Property Blue As Byte

        Sub New()
            Me.Alpha = 255
            Me.Red = 255
            Me.Green = 255
            Me.Blue = 255
        End Sub
    End Class

    <Serializable()> Public Class LayerData
        Public Property Tiles As TileData(,)

        Sub New(ByVal maxX As Integer, ByVal maxY As Integer)
            ReDim Me.Tiles(maxX, maxY)
            For X As Integer = 0 To Me.Tiles.GetUpperBound(0)
                For Y As Integer = 0 To Me.Tiles.GetUpperBound(1)
                    Me.Tiles(X, Y) = New TileData
                Next
            Next
        End Sub

        Public Sub ResizeTileData(ByVal newSize As Integer())
            Dim newTiles(newSize(0), newSize(1)) As TileData
            For x As Integer = 0 To newSize(0)
                For y As Integer = 0 To newSize(1)
                    If Me.Tiles.GetUpperBound(0) <= x Or Me.Tiles.GetUpperBound(1) <= y Then
                        newTiles(x, y) = New TileData
                    Else
                        newTiles(x, y) = Me.Tiles(x, y)
                    End If
                Next
            Next
            Me.Tiles = newTiles
        End Sub
    End Class

    <Serializable()> Public Class TileData
        Public Property Tileset As Integer
        Public Property X As Integer
        Public Property Y As Integer

        Sub New()
            Me.Tileset = 0
            Me.X = 0
            Me.Y = 0
        End Sub
    End Class
End Namespace
