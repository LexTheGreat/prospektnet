Public Class MapNPCs

    Private Sub lstNPCs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNPCs.SelectedIndexChanged
        If Not IsNothing(lstNPCs.SelectedItem) Then MapNPCEditor.SelectNPC(lstNPCs.SelectedIndex + 1)
    End Sub

    Private Sub AddNPCToMapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNPCToMapToolStripMenuItem.Click
        MapNPCEditor.AddNPCToMap()
        Me.Dispose()
    End Sub
End Class