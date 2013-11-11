Imports Prospekt.Graphics
Imports IHProspekt.Objects
Imports IHProspekt.Core
Public Class NpcDropClass
    Private Index As Integer
    Private GameItemIndex As Integer
    Private NpcItemIndex As Integer

    Public Sub Init(ByVal NpcNum As Integer)
        Index = NpcNum
        For I As Integer = 0 To ItemCount
            If Not IsNothing(Item(I)) Then
                NpcDropTable.lstGameItems.Items.Add(Item(I).Name)
            End If
        Next
    End Sub

    Public Sub SelectGameItem(ByVal Index As Integer)
        GameItemIndex = Index
    End Sub

    Public Sub SelectNpcItem(ByVal Index As Integer)
        NpcItemIndex = Index
    End Sub

    Public Sub AddItem()

    End Sub

    Public Sub RemoveItem()

    End Sub
End Class
