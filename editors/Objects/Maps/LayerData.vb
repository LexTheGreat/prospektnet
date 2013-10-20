<Serializable()> Public Class LayerData
    Private mTiles(,) As TileData

    Sub New(ByVal maxX As Integer, ByVal maxY As Integer)
        ReDim Preserve Me.mTiles(maxX, maxY)
        For X As Integer = 0 To maxX
            For Y As Integer = 0 To maxY
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

    Public Sub ReSizeTileData(ByVal newSize As Integer())
        Dim newTiles(newSize(0), newSize(1)) As TileData
        For x As Integer = 0 To newSize(0)
            For y As Integer = 0 To newSize(1)
                If Me.mTiles.GetUpperBound(0) <= x Or Me.mTiles.GetUpperBound(1) <= y Then
                    newTiles(x, y) = New TileData
                Else
                    newTiles(x, y) = Me.mTiles(x, y)
                End If
            Next
        Next
        Me.mTiles = newTiles
    End Sub

End Class