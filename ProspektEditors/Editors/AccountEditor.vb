Class AccountClass
    Private index As Integer

    Public Sub Init()
        For I As Integer = 1 To AccountCount
            If Not IsNothing(Account(I)) Then
                EditorWindow.lstAccounts.Items.Add(Account(I).Email)
            End If
        Next
        EditorWindow.tabAccount.Text = "Account"
        EditorWindow.proptAccountData.SelectedObject = vbNull
    End Sub

    Public Sub Load(ByVal i As Integer)
        If i <= 0 Then Exit Sub
        index = i
        EditorWindow.tabAccount.Text = Account(i).Email
        EditorWindow.proptAccountData.SelectedObject = Account(i)
    End Sub

    Public Sub ReloadList()
        EditorWindow.lstAccounts.Items.Clear()
        For I As Integer = 1 To AccountCount
            If Not IsNothing(Account(I)) Then
                EditorWindow.lstAccounts.Items.Add(Account(I).Email)
            End If
        Next
        EditorWindow.tabAccount.Text = "Account"
        EditorWindow.proptAccountData.SelectedObject = vbNull
        Load(index)
    End Sub

    Public Sub Reload()
        ReloadList()
        Load(index)
    End Sub

    Public Sub Undo()
        Accounts.Data.LoadAccounts()
        EditorWindow.tabAccount.Text = "Account"
        EditorWindow.proptAccountData.SelectedObject = vbNull
    End Sub

    Public Sub Verify()
        If Account(index).Email = vbNullString Then Account(index).Email = index & "@email.com"
        If Account(index).Password = vbNullString Then Account(index).Password = "password"
        If Account(index).Name = vbNullString Then Account(index).Name = "New Player"
        If Account(index).Sprite < 0 Then Account(index).Sprite = 0
        If Account(index).X < 0 Then Account(index).X = 0
        If Account(index).Y < 0 Then Account(index).Y = 0

        ' Refresh data
        EditorWindow.proptAccountData.Refresh()
    End Sub

    Public Sub EditInventory()
        If index <= 0 Then Exit Sub
        InventoryEditor.Init(index)
        PlayerInventory.Show()
    End Sub
End Class
