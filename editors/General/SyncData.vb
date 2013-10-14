Public Class frmSyncData

    Private Sub btnSync_Click(sender As Object, e As EventArgs) Handles btnSync.Click
        If IsNothing(txtEmail.Text) Or IsNothing(txtPassword.Text) Then MessageBox.Show("You must enter a Email and Password!")
        ' Setup Networking
        If Networking.ConnectToServer Then SendData.Login(txtEmail.Text, txtPassword.Text, 0)
    End Sub

    Private Sub frmSyncData_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Networking.Dispose()
    End Sub

End Class