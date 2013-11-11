Imports System.IO
Imports IHProspekt.Objects
Imports IHProspekt.Core
Public Class TilesetLogic
    Public Function isTileBlocked(ByVal X As Integer, ByVal Y As Integer) As Boolean
        Dim tempTile As New TileData
        For I As Integer = 0 To MapLayerEnum.COUNT - 1
            tempTile = Map.Layer(I).GetTileData(X, Y)
            If tempTile.Tileset < 0 Then Return False
            If tempTile.Tileset > TilesetCount Then Return False
            If IsNothing(Tileset(tempTile.Tileset)) Then Return False
            If Tileset(tempTile.Tileset).Tile(tempTile.X / 32, tempTile.Y / 32) = TileType.Blocked Then Return True
        Next I
        Return False
    End Function
End Class
