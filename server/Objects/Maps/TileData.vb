<Serializable()> Public Class TileData
    Public Tileset As Integer
    Public X As Integer
    Public Y As Integer

    Sub New()
        Me.Tileset = 0
        Me.X = 0
        Me.Y = 0
    End Sub
End Class