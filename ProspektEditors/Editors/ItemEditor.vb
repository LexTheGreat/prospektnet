Class ItemClass
    Private index As Integer

    Public Sub Init()
        For I As Integer = 0 To ItemCount
            If Not IsNothing(Item(I)) Then
                EditorWindow.lstitems.Items.Add(Item(I).Name)
            End If
        Next
        EditorWindow.tabItem.Text = "Item"
        EditorWindow.proptItemData.SelectedObject = vbNull
    End Sub

    Public Sub Load(ByVal i As Integer)
        If i < 0 Then Exit Sub
        index = i
        EditorWindow.tabItem.Text = Item(i).Name
        EditorWindow.proptItemData.SelectedObject = Item(i)
    End Sub

    Public Sub ReloadList()
        EditorWindow.lstitems.Items.Clear()
        If Not IsNothing(Item) Then
            For Each nc In Item
                If Not IsNothing(nc) Then
                    EditorWindow.lstitems.Items.Add(nc.Name)
                End If
            Next
        End If
        EditorWindow.tabItem.Text = "Item"
        EditorWindow.proptItemData.SelectedObject = vbNull
        Load(index)
    End Sub

    Public Sub Reload()
        ReloadList()
        Load(index)
    End Sub

    Public Sub Undo()
        Items.Data.LoadItems()
        EditorWindow.tabItem.Text = "Item"
        EditorWindow.proptItemData.SelectedObject = vbNull
    End Sub

    Public Sub Verify()
        If Item(index).Name = vbNullString Then Item(index).Name = "New Item"
        If Item(index).Sprite < 0 Then Item(index).Sprite = 0
        If Item(index).Health < 0 Then Item(index).Health = 0

        ' Refresh data
        EditorWindow.proptItemData.Refresh()
    End Sub
End Class
