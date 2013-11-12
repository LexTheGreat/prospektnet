Public Class PlayerInventory

    Private Sub lstGameItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstGameItems.SelectedIndexChanged
        If Not IsNothing(lstGameItems.SelectedItem) Then InventoryEditor.SelectGameItem(lstGameItems.SelectedIndex)
    End Sub

    Private Sub lstPlayerItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstPlayerItems.SelectedIndexChanged
        If Not IsNothing(lstPlayerItems.SelectedItem) Then InventoryEditor.SelectPlayerItem(lstPlayerItems.SelectedIndex)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not IsNothing(lstGameItems.SelectedItem) Then InventoryEditor.AddItem()
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If Not IsNothing(lstPlayerItems.SelectedItem) Then InventoryEditor.RemoveItem()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Accounts.Data.SaveAccounts()
        ' Reload Editor Data
        Accounts.Data.LoadAccounts()
        AccountEditor.Reload()
        InventoryEditor.Reload()
    End Sub
End Class