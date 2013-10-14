<Serializable()> Public Class LayerData
    Private mTiles(,) As TileData

    Sub New(ByVal maxX As Integer, ByVal maxY As Integer)
        ReDim Me.mTiles(maxX, maxY)
        For X As Integer = 0 To Me.mTiles.GetUpperBound(0)
            For Y As Integer = 0 To Me.mTiles.GetUpperBound(1)
                Me.mTiles(X, Y) = New TileData
            Next
        Next
    End Sub

    Public Sub SetTileData(ByVal Data As TileData(,))
        Me.mTiles = Data
    End Sub

    Public Sub SetTileData(ByVal X As Integer, ByVal Y As Integer, ByVal data As TileData)
        Me.mTiles(X, Y) = data
    End Sub

    Public Function GetTileData() As TileData(,)
        Return Me.mTiles
    End Function

    Public Function GetTileData(ByVal X As Integer, ByVal Y As Integer) As TileData
        Return Me.mTiles(X, Y)
    End Function

    Public Sub ReSizeTileData(ByVal Layer As Byte, ByVal newSize As Integer())
        Me.mTiles = CType(ResizeArray(Me.mTiles, newSize), TileData(,))
    End Sub

End Class