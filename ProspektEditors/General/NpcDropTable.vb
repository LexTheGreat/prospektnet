Public Class NpcDropTable

    Private Sub lstGameItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstGameItems.SelectedIndexChanged
        If Not IsNothing(lstGameItems.SelectedItem) Then NpcDropEditor.SelectGameItem(lstGameItems.SelectedIndex)
    End Sub

    Private Sub lstNpcItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNpcItems.SelectedIndexChanged
        If Not IsNothing(lstNpcItems.SelectedItem) Then NpcDropEditor.SelectNpcItem(lstNpcItems.SelectedIndex)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not IsNothing(lstGameItems.SelectedItem) Then NpcDropEditor.AddItem()
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If Not IsNothing(lstNpcItems.SelectedItem) Then NpcDropEditor.RemoveItem()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        NPCs.Data.SaveNpcs()
        ' Reload Editor Data
        NPCs.Data.LoadNpcs()
        NpcEditor.Reload()
        NpcDropEditor.Reload()
    End Sub
End Class