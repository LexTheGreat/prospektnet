Public Class CommitData
    Private Sub btnPublish_Click(sender As Object, e As EventArgs) Handles btnPublish.Click
        If IsNothing(txtEmail.Text) Or IsNothing(txtPassword.Text) Then MessageBox.Show("You must enter a Email and Password!")
        ' Setup Networking
        If Networking.ConnectToServer Then
            SendData.Login(txtEmail.Text, txtPassword.Text, 1)
        Else
            MsgBox("No connection to server")
            Me.Focus()
        End If
    End Sub
End Class