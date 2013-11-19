Imports System.IO
Imports IHProspekt.Objects
Imports IHProspekt.Database
Imports IHProspekt.Core
Public Class TilesetData
    Public Sub LoadAll()
        Try
            If Directory.Exists(pathNPCs) Then
                Dim fileEntries As String() = Directory.GetFiles(pathTilesetData, "*.bin")
                ReDim Preserve Tileset(0 To 1)
                Tileset(0) = New Tilesets
                TilesetCount = 0
                For Each fileName In fileEntries
                    ReDim Preserve Tileset(0 To TilesetCount)
                    Tileset(TilesetCount) = New Tilesets
                    Tileset(TilesetCount).ID = TilesetCount
                    Tileset(TilesetCount).Load()
                    TilesetCount = TilesetCount + 1
                Next fileName
                TilesetCount = TilesetCount - 1
            End If
        Catch ex As Exception
            ErrHandler.HandleException(ex, ErrorHandler.ErrorLevels.High)
        End Try
    End Sub

    Public Sub SaveAll()
        If Directory.Exists(pathTilesetData) Then
            For Each tile In Tileset
                tile.Save()
            Next
        End If
    End Sub

    Public Sub Save(ByVal SaveTileset As TilesetBase)
        Using File As New Files(pathTilesetData & Trim(SaveTileset.ID) & ".bin", SaveTileset)
            File.WriteBinary()
        End Using
    End Sub

    Public Function GetTilesetID(ByVal name As String)
        For Each tile In Tileset
            If tile.Name = name Then Return tile.ID
        Next
        Return 0
    End Function
End Class
