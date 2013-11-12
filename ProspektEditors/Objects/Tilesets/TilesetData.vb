Imports System.IO
Imports IHProspekt.Objects
Imports IHProspekt.Database
Imports IHProspekt.Core
Public Class TilesetData
    Public Sub Save(ByVal SaveTileset As TilesetBase)
        Using File As New Files(pathTilesetData & Trim(SaveTileset.ID) & ".bin", SaveTileset)
            File.WriteBinary()
        End Using
    End Sub

    Public Sub LoadTilesets()
        Dim I As Integer
        Dim fileEntries As String()
        Try
            ReDim Preserve Tileset(0 To 1)
            Tileset(0) = New Tilesets
            If Directory.Exists(pathTilesetData) Then
                fileEntries = Directory.GetFiles(pathTilesetData, "*.bin")
                For Each fileName In fileEntries
                    TilesetCount = I + 1
                    ReDim Preserve Tileset(0 To TilesetCount)
                    Tileset(TilesetCount) = New Tilesets
                    Tileset(TilesetCount).ID = fileName.Replace(pathTilesetData, vbNullString).Replace(".bin", vbNullString)
                    Tileset(TilesetCount).Load()
                    I = TilesetCount
                Next fileName
            End If
        Catch ex As Exception
            ErrHandler.HandleException(ex, ErrorHandler.ErrorLevels.High)
        End Try
    End Sub

    Public Function GetTilesetID(ByVal name As String)
        For Each tile In Tileset
            If tile.Name = name Then Return tile.ID
        Next
        Return 0
    End Function
End Class
