Class AccountClass
    Private index As Integer

    Public Sub Init()
        If Not IsNothing(Account) Then
            For Each plyr In Account
                If Not IsNothing(plyr) Then
                    EditorWindow.lstAccounts.Items.Add(plyr.Email)
                End If
            Next
        End If
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
        If Not IsNothing(Account) Then
            For Each plyr In Account
                If Not IsNothing(plyr) Then
                    EditorWindow.lstAccounts.Items.Add(plyr.Email)
                End If
            Next
        End If
        EditorWindow.tabAccount.Text = "Account"
        EditorWindow.proptAccountData.SelectedObject = vbNull
        Load(index)
    End Sub

    Public Sub Reload()
        Load(index)
    End Sub

    Public Sub Undo()
        AccountData.LoadAccounts()
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
End Class
