Module AccountEditor
    Private base As EditorWindow
    Private index As Integer

    Public Sub Init(ByVal frm As EditorWindow)
        base = frm
        If Not IsNothing(Account) Then
            For Each plyr In Account
                If Not IsNothing(plyr) Then
                    base.lstAccounts.Items.Add(plyr.Email)
                End If
            Next
        End If
        base.tabAccount.Text = "Account"
        base.proptAccountData.SelectedObject = vbNull
    End Sub

    Public Sub Load(ByVal i As Integer)
        index = i
        base.tabAccount.Text = Account(i).Email
        base.proptAccountData.SelectedObject = Account(i)
    End Sub

    Public Sub ReloadList()
        base.lstAccounts.Items.Clear()
        If Not IsNothing(Account) Then
            For Each plyr In Account
                If Not IsNothing(plyr) Then
                    base.lstAccounts.Items.Add(plyr.Email)
                End If
            Next
        End If
        base.tabAccount.Text = "Account"
        base.proptAccountData.SelectedObject = vbNull
        Load(index)
    End Sub

    Public Sub Reload()
        Load(index)
    End Sub

    Public Sub Undo()
        AccountData.LoadAccounts()
        base.tabAccount.Text = "Account"
        base.proptAccountData.SelectedObject = vbNull
    End Sub

    Public Sub Verify()
        If Account(index).Email = vbNullString Then Account(index).Email = index & "@email.com"
        If Account(index).Password = vbNullString Then Account(index).Password = "password"
        If Account(index).Name = vbNullString Then Account(index).Name = "New Player"
        If Account(index).Sprite < 0 Then Account(index).Sprite = 0
        If Account(index).X < 0 Then Account(index).X = 0
        If Account(index).Y < 0 Then Account(index).Y = 0

        ' Refresh data
        base.proptAccountData.Refresh()
    End Sub
End Module
