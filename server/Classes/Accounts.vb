Public Class Accounts
    Private mLogin As String
    Private mPassword As String
    Private mPlayer As Players

    Public Sub New()
        Me.mLogin = vbNullString
        Me.mPassword = vbNullString
        Me.mPlayer = New Players
    End Sub

    Public Function Create() As Boolean
        If Me.Login = vbNullString Then Return False
        AccountData.CreateAccount(Me)
        Return True
    End Function

    Public Function NewCharacter() As Boolean
        If Me.Login = vbNullString Or Me.Player.Name = vbNullString Then Return False
        AccountData.CreateCharacter(Me)
        Return True
    End Function

    ' Saved variables
    Public Property Login() As String
        Get
            Return Me.mLogin
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.mLogin = value
            End If
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Me.mPassword
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.mPassword = value
            End If
        End Set
    End Property

    Public Property Player() As Players
        Get
            Return Me.mPlayer
        End Get
        Set(value As Players)
            If Not IsNothing(Me) Then
                Me.mPlayer = value
            End If
        End Set
    End Property
End Class
