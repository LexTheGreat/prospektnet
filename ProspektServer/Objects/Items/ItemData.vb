Imports System.IO
Imports Prospekt.Network
Imports IHProspekt.Objects
Imports IHProspekt.Database
Public Class ItemData
    Public Sub LoadItems()
        Try
            If Directory.Exists(pathItems) Then
                Dim fileEntries As String() = Directory.GetFiles(pathItems, "*.xml")
                ReDim Preserve Item(0 To 1)
                Item(0) = New Items
                ItemCount = 0
                For Each fileName In fileEntries
                    ReDim Preserve Item(0 To ItemCount)
                    Item(ItemCount) = New Items
                    Item(ItemCount).ID = fileName.Replace(pathItems, vbNullString).Replace(".xml", vbNullString)
                    Item(ItemCount).Load()
                    ItemCount = ItemCount + 1
                Next fileName
                ItemCount = ItemCount - 1
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Data.LoadItems")
        End Try
    End Sub

    Public Sub SaveItems()
        If Directory.Exists(pathItems) Then
            For Each itm In Item
                itm.Save()
            Next
        End If
    End Sub

    Public Sub Save(ByVal SaveItem As ItemBase)
        WriteXML(pathItems & Trim(SaveItem.ID) & ".xml", SaveItem)
    End Sub

    Public Function GetItemIndex(ByVal ID As Integer) As Integer
        For index As Integer = 0 To ItemCount
            If Item(index).ID = ID Then Return index
        Next
        Return 0
    End Function

    Public Function GetItem(ByVal ID As String) As Items
        For Each itm In Item
            If itm.ID = ID Then Return itm
        Next
        Return New Items
    End Function

    Public Function ItemExists(ByVal ID As Integer) As Boolean
        Dim Filename As String
        Filename = pathItems & Trim(ID) & ".xml"

        If Exists(Filename) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetItemIndex(ByVal Name As String) As Integer
        For index As Integer = 1 To ItemCount
            If Item(index).Name = Name Then Return index
        Next
        Return 0
    End Function
End Class
