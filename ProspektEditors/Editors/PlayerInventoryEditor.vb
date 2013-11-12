Imports Prospekt.Graphics
Imports IHProspekt.Objects
Imports IHProspekt.Core
Public Class PlayerInventoryClass
    Private Index As Integer
    Private GameItemIndex As Integer = -1
    Private PlayerItemIndex As Integer = -1

    Public Sub Init(ByVal PlayerNum As Integer)
        If IsNothing(Account(PlayerNum)) Then
            PlayerInventory.Dispose()
            Exit Sub
        End If
        Index = PlayerNum
        PlayerInventory.Text = Account(Index).Name & "'s Drop Table"
        PlayerInventory.groupPlayerItems.Text = Account(Index).Name & "'s Items"

        PlayerInventory.lstGameItems.Items.Clear()
        For I As Integer = 0 To ItemCount
            If Not IsNothing(Item(I)) Then
                PlayerInventory.lstGameItems.Items.Add(Item(I).Name)
            End If
        Next

        PlayerInventory.lstPlayerItems.Items.Clear()
        For I As Integer = 0 To Account(Index).Base.Player.Inventory.Length
            Dim itm As Items = Account(Index).GetInventoryItem(I)
            If Not IsNothing(itm) Then
                PlayerInventory.lstPlayerItems.Items.Add(itm.Name)
            End If
        Next
    End Sub

    Public Sub Reload()
        PlayerInventory.lstPlayerItems.Items.Clear()
        For I As Integer = 0 To Account(Index).Base.Player.Inventory.Length
            Dim itm As Items = Account(Index).GetInventoryItem(I)
            If Not IsNothing(itm) Then
                PlayerInventory.lstPlayerItems.Items.Add(itm.Name)
            End If
        Next
    End Sub

    Public Sub SelectGameItem(ByVal Index As Integer)
        GameItemIndex = Index
    End Sub

    Public Sub SelectPlayerItem(ByVal Index As Integer)
        PlayerItemIndex = Index
    End Sub

    Public Sub AddItem()
        If GameItemIndex = -1 Then Exit Sub
        Dim num As Integer = Account(Index).GetOpenInventory()
        ReDim Preserve Account(Index).Base.Player.Inventory(0 To num)
        Account(Index).Base.Player.Inventory(num) = GameItemIndex
        Reload()
    End Sub

    Public Sub RemoveItem()
        If PlayerItemIndex = -1 Then Exit Sub
        Account(Index).RemoveInventoryItem(PlayerItemIndex)
        Reload()
    End Sub
End Class
