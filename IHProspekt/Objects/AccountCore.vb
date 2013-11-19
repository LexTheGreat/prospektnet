Namespace Objects
    Public Class AccountBase
        Public Property Email As String
        Public Property Password As String
        Public Property Player As PlayerBase

        Public Sub New()
            Me.Email = "account@email.com"
            Me.Password = "Password"
            Me.Player = New PlayerBase
        End Sub
    End Class
End Namespace