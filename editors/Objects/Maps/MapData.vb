Imports System.IO
Public Class MapData
    Public Shared Sub LoadMaps()
        Try
            If Directory.Exists(pathMaps) Then
                Dim fileEntries As String() = Directory.GetFiles(pathMaps, "*.bin")
                Dim i As Integer = 0
                ReDim Map(0 To 0)
                Map(0) = New MapStructure
                For Each fileName In fileEntries
                    ReDim Preserve Map(0 To i)
                    Map(i) = New MapStructure
                    Map(i).SetID(fileName.Replace(pathMaps, vbNullString).Replace(".bin", vbNullString))
                    Map(i).Load()
                    i = i + 1
                Next fileName
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Data.LoadMaps")
        End Try
    End Sub

    Public Shared Sub SaveMaps()
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

    Public Shared Sub NewMap()
        Dim newMap As New MapStructure, i As Integer = GetNextMapIndex()
        newMap.SetID(i)
        ReDim Preserve Map(0 To i)
        Map(i) = newMap
        SaveMaps()
    End Sub

    Public Shared Function GetMapIndex(ByVal ID As Integer) As Integer
        For index As Integer = 0 To Map.Length
            If Map(index).ID = ID Then Return index
        Next
        Return 0
    End Function

    Public Shared Function GetMap(ByVal ID As String) As MapStructure
        For Each mp In Map
            If mp.ID = ID Then Return mp
        Next
        Return New MapStructure
    End Function

    Public Shared Function GetNextMapIndex() As Integer
        If Not IsNothing(Map) Then Return Map.Length
        Return 0
    End Function

    Public Shared Function MapExists(ByVal ID As Integer) As Boolean
        Dim Filename As String
        Filename = pathMaps & Trim(ID) & ".bin"

        If Files.Exists(Filename) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
