Namespace Objects
    <Serializable()> Public Class TilesetBase
        Public Property ID As Integer
        Public Property Name As String
        Public Property MaxX As Integer
        Public Property MaxY As Integer
        Public Property Tile As Byte(,)

        Sub New()
            Me.ID = 0
            Me.Name = "New Tileset"
            Me.MaxX = 34
            Me.MaxY = 34
            ReDim Me.Tile(0, 0)
            Me.Tile(0, 0) = 0
        End Sub

        Public Sub ResizeTileData(ByVal newSize As Integer())
            Dim newTiles(newSize(0), newSize(1)) As Byte
            For x As Integer = 0 To newSize(0)
                For y As Integer = 0 To newSize(1)
                    If Me.Tile.GetUpperBound(0) <= x Or Me.Tile.GetUpperBound(1) <= y Then
                        newTiles(x, y) = 0
                    Else
                        newTiles(x, y) = Me.Tile(x, y)
                    End If
                Next
            Next
            Me.Tile = newTiles
        End Sub
    End Class
End Namespace