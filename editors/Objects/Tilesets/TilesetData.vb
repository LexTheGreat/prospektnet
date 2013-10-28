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

    Public Shared Function GetTilesetID(ByVal name As String)
        For Each tile In Tileset
            If tile.Name = name Then Return tile.ID
        Next
        Return 0
    End Function
End Class
