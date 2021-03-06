﻿Imports System.IO
Imports Prospekt.Network
Imports IHProspekt.Objects
Imports IHProspekt.Core
Imports IHProspekt.Database
Public Class TilesetData
    Public Sub Save(ByVal SaveTileset As TilesetBase)
        Using File As New Files(pathTilesets & Trim(SaveTileset.ID) & ".bin", SaveTileset)
            File.WriteBinary()
        End Using
    End Sub

    Public Sub LoadAll()
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
                    Tileset(TilesetCount).ID = CInt(fileName.Replace(pathTilesets, vbNullString).Replace(".bin", vbNullString))
                    Tileset(TilesetCount).Load()
                    I = TilesetCount
                Next fileName
            End If
        Catch ex As Exception
            Server.WriteLine("Error: " & ex.ToString & " (In: Tilesets.Data.LoadTilesets")
        End Try
    End Sub

    Public Sub SaveAll()
        Try
            If Directory.Exists(pathTilesets) Then
                For Each tile In Tileset
                    tile.Save()
                Next
            End If
        Catch ex As Exception
            Server.WriteLine("Error: " & ex.ToString & " (In: Tilesets.Data.SaveMaps")
        End Try
    End Sub

    Public Function GetTilesetID(ByVal name As String)
        For Each tile In Tileset
            If tile.Name = name Then Return tile.ID
        Next
        Return 0
    End Function

    Public Function isTileBlocked(ByVal MapNum As Integer, ByVal X As Integer, ByVal Y As Integer) As Boolean
        Dim tempTile As New TileData
        For I As Integer = 0 To MapLayerEnum.COUNT - 1
            tempTile = Map(MapNum).Layer(I).Tiles(X, Y)
            If tempTile.Tileset < 0 Then Return False
            If tempTile.Tileset > TilesetCount Then Return False
            If Tileset(tempTile.Tileset).Tile(tempTile.X / 32, tempTile.Y / 32) = TileType.Blocked Then Return True
        Next I
        Return False
    End Function

    Public Function isTileNPCBlocked(ByVal MapNum As Integer, ByVal X As Integer, ByVal Y As Integer) As Boolean
        Dim tempTile As New TileData
        For I As Integer = 0 To MapLayerEnum.COUNT - 1
            tempTile = Map(MapNum).Layer(I).Tiles(X, Y)
            If tempTile.Tileset < 0 Then Return False
            If tempTile.Tileset > TilesetCount Then Return False
            If Tileset(tempTile.Tileset).Tile(tempTile.X / 32, tempTile.Y / 32) > TileType.Walkable Then Return True
        Next I
        Return False
    End Function

    Public Sub SendTilesets()
        Dim i As Integer
        For i = 1 To TilesetCount
            If Not IsNothing(Tileset(i)) Then
                SendData.TilesetData(i)
            End If
        Next
    End Sub
End Class
