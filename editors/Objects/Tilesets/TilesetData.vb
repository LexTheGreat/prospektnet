Imports System.IO
Public Class TilesetData
    Public Shared Sub Save(ByVal SaveTileset As Tilesets)
        Files.WriteBinary(pathTilesets & Trim(SaveTileset.ID) & ".bin", SaveTileset)
    End Sub

    Public Shared Sub LoadTilesets()
        Dim I As Integer
        Dim fileEntries As String()
        Try
            ReDim Preserve Tileset(0 To 1)
            Tileset(0) = New Tilesets
            If Directory.Exists(pathTilesets) Then
                fileEntries = Directory.GetFiles(pathTilesets, "*.bin")
                For Each fileName In fileEntries
                    TilesetCount = I + 1
                    ReDim Preserve Tileset(0 To TilesetCount)
                    Tileset(TilesetCount) = New Tilesets
                    Tileset(TilesetCount).SetID(fileName.Replace(pathTilesets, vbNullString).Replace(".bin", vbNullString))
                    Tileset(TilesetCount).Load()
                    I = TilesetCount
                Next fileName
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
