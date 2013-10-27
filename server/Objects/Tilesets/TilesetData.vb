Imports System.IO
Public Class TilesetData
    Public Shared Sub Save(ByVal SaveTileset As Tilesets)
        Files.WriteBinary(pathTilesets & Trim(SaveTileset.ID) & ".bin", SaveTileset)
    End Sub

    Public Shared Sub LoadTilesets()
        Try
            If Directory.Exists(pathTilesets) Then
                Dim fileEntries As String() = Directory.GetFiles(pathTilesets, "*.bin")
                ReDim Preserve Tileset(0 To 1)
                Tileset(0) = New Tilesets
                For Each fileName In fileEntries
                    ReDim Preserve Tileset(0 To TilesetCount)
                    Tileset(TilesetCount) = New Tilesets
                    Tileset(TilesetCount).SetID(fileName.Replace(pathTilesets, vbNullString).Replace(".bin", vbNullString))
                    Tileset(TilesetCount).Load()
                    TilesetCount = TilesetCount + 1
                Next fileName
                TilesetCount = TilesetCount - 1
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Data.LoadTilesets")
        End Try
    End Sub

    Public Shared Function isTileBlocked(ByVal MapNum As Integer, ByVal X As Integer, ByVal Y As Integer) As Boolean
        Dim tempTile As New TileData
        For I As Integer = 0 To MapLayerEnum.COUNT - 1
            tempTile = Map(MapNum).Layer(I).GetTileData(X, Y)
            If tempTile.Tileset < 0 Then Return False
            If tempTile.Tileset > TilesetCount Then Return False
            If Tileset(tempTile.Tileset).Tile(tempTile.X / 32, tempTile.Y / 32) = TileType.Blocked Then Return True
        Next I
        Return False
    End Function

    Public Shared Function isTileNPCBlocked(ByVal MapNum As Integer, ByVal X As Integer, ByVal Y As Integer) As Boolean
        Dim tempTile As New TileData
        For I As Integer = 0 To MapLayerEnum.COUNT - 1
            tempTile = Map(MapNum).Layer(I).GetTileData(X, Y)
            If tempTile.Tileset < 0 Then Return False
            If tempTile.Tileset > TilesetCount Then Return False
            If Tileset(tempTile.Tileset).Tile(tempTile.X / 32, tempTile.Y / 32) > TileType.Walkable Then Return True
        Next I
        Return False
    End Function

    Public Shared Sub SendTilesets()
        Dim i As Integer
        For i = 1 To TilesetCount
            If Not IsNothing(Tileset(i)) Then
                SendData.TilesetData(i)
            End If
        Next
    End Sub
End Class
