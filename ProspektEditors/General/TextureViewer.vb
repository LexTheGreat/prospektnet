Public Class TextureViewer
    Public Type As Integer = 1

    Public Sub init()
        cmbType.Items.Add("Tiles")
        cmbType.Items.Add("Sprites")
        cmbType.Items.Add("Items")
        cmbType.SelectedIndex = 0
        Me.Show()
    End Sub

    Public Sub LoadTextures(ByVal index As Integer)
        Me.Type = index
        lstTextures.Items.Clear()
        If index = 1 Then ' Tiles
            For I As Integer = 1 To Graphics.countTileset
                lstTextures.Items.Add(I)
            Next
        ElseIf index = 2 Then ' Sprites
            For I As Integer = 1 To Graphics.countSprite
                lstTextures.Items.Add(I)
            Next
        ElseIf index = 3 Then ' Items
            For I As Integer = 1 To Graphics.countItem
                lstTextures.Items.Add(I)
            Next
        End If
        lstTextures.SelectedIndex = 0
    End Sub

    Private Sub lstTextures_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTextures.SelectedIndexChanged
        If Me.Type = 1 Then ' Tiles
            Me.picTexture.Image = Bitmap.FromFile(Graphics.gTexture(Graphics.texTileset(lstTextures.SelectedIndex + 1)).FilePath)
        ElseIf Me.Type = 2 Then ' Sprites
            Me.picTexture.Image = Bitmap.FromFile(Graphics.gTexture(Graphics.texSprite(lstTextures.SelectedIndex + 1)).FilePath)
        ElseIf Me.Type = 3 Then ' Items
            Me.picTexture.Image = Bitmap.FromFile(Graphics.gTexture(Graphics.texItem(lstTextures.SelectedIndex + 1)).FilePath)
        End If
    End Sub

    Private Sub cmbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbType.SelectedIndexChanged
        LoadTextures(cmbType.SelectedIndex + 1)
    End Sub
End Class