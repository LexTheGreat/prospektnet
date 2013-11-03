Public Class AccountBase
    Public Property Email As String
    Public Property Password As String
    Public Property Player As PlayerBase

    Public Sub New()
        Me.Email = vbNullString
        Me.Password = vbNullString
        Me.Player = New PlayerBase
    End Sub
End Class