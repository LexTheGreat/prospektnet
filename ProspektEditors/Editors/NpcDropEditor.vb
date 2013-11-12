Imports Prospekt.Graphics
Imports IHProspekt.Objects
Imports IHProspekt.Core
Public Class NpcDropClass
    Private Index As Integer
    Private GameItemIndex As Integer = -1
    Private NpcItemIndex As Integer = -1

    Public Sub Init(ByVal NpcNum As Integer)
        If IsNothing(NPC(NpcNum)) Then
            NpcDropTable.Dispose()
            Exit Sub
        End If
        Index = NpcNum
        NpcDropTable.Text = NPC(Index).Name & "'s Drop Table"
        NpcDropTable.groupNpcItems.Text = NPC(Index).Name & "'s Items"

        NpcDropTable.lstGameItems.Items.Clear()
        For I As Integer = 0 To ItemCount
            If Not IsNothing(Item(I)) Then
                NpcDropTable.lstGameItems.Items.Add(Item(I).Name)
            End If
        Next

        NpcDropTable.lstNpcItems.Items.Clear()
        For I As Integer = 0 To NPC(Index).Base.Inventory.Length
            Dim itm As Items = NPC(Index).GetInventoryItem(I)
            If Not IsNothing(itm) Then
                NpcDropTable.lstNpcItems.Items.Add(itm.Name)
            End If
        Next
    End Sub

    Public Sub Reload()
        NpcDropTable.lstNpcItems.Items.Clear()
        For I As Integer = 0 To NPC(Index).Base.Inventory.Length
            Dim itm As Items = NPC(Index).GetInventoryItem(I)
            If Not IsNothing(itm) Then
                NpcDropTable.lstNpcItems.Items.Add(itm.Name)
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
        If GameItemIndex = -1 Then Exit Sub
        Dim num As Integer = NPC(Index).GetOpenInventory()
        ReDim Preserve NPC(Index).Base.Inventory(0 To num)
        NPC(Index).Base.Inventory(num) = GameItemIndex
        Reload()
    End Sub

    Public Sub RemoveItem()
        If NpcItemIndex = -1 Then Exit Sub
        NPC(Index).RemoveInventoryItem(NpcItemIndex)
        Reload()
    End Sub
End Class
