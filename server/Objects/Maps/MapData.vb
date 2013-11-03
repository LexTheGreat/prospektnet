Imports System.IO
Public Class MapData
    Public Sub LoadAll()
        Try
            If Directory.Exists(pathMaps) Then
                Dim fileEntries As String() = Directory.GetFiles(pathMaps, "*.bin")
                ReDim Preserve Map(0 To 1)
                Map(0) = New Maps
                MapCount = 0
                For Each fileName In fileEntries
                    ReDim Preserve Map(0 To MapCount)
                    Map(MapCount) = New Maps
                    Map(MapCount).ID = fileName.Replace(pathMaps, vbNullString).Replace(".bin", vbNullString)
                    Map(MapCount).Load()
                    MapCount = MapCount + 1
                Next fileName
                MapCount = MapCount - 1
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Data.LoadMaps")
        End Try
    End Sub

    Public Sub Save(ByVal SaveMap As MapBase)
        Files.WriteBinary(pathMaps & Trim(SaveMap.ID) & ".bin", SaveMap)
    End Sub

    Public Sub SaveAll()
        Try
            If Directory.Exists(pathMaps) Then
                For Each mp In Map
                    mp.Save()
                Next
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Data.SaveMaps")
        End Try
    End Sub

    Public Sub NewMap()
        Dim newMap As New Maps, i As Integer = GetNextMapIndex()
        newMap.ID = i
        ReDim Preserve Map(0 To i)
        Map(i) = newMap
        SaveAll()
    End Sub

    Public Function GetMapIndex(ByVal ID As Integer) As Integer
        For index As Integer = 0 To MapCount
            If Map(index).ID = ID Then Return index
        Next
        Return 0
    End Function

    Public Function GetMap(ByVal ID As String) As Maps
        For Each mp In Map
            If mp.ID = ID Then Return mp
        Next
        Return New Maps
    End Function

    Public Function GetNextMapIndex() As Integer
        If Not IsNothing(Map) Then Return MapCount + 1
        Return 0
    End Function

    Public Function MapExists(ByVal ID As Integer) As Boolean
        Dim Filename As String
        Filename = pathMaps & Trim(ID) & ".bin"

        If Files.Exists(Filename) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
