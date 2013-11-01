Public Class Accounts
    Private mEmail As String
    Private mPassword As String
    Private mPlayer As Players

    Public Sub New()
        Me.mEmail = vbNullString
        Me.mPassword = vbNullString
        Me.mPlayer = New Players
    End Sub

    Public Function Create() As Boolean
        If Me.Email = vbNullString Then Return False
        AccountData.CreateAccount(Me)
        Return True
    End Function

    Public Function Save() As Boolean
        If Me.Email = vbNullString Then Return False
        AccountData.SaveAccount(Me)
        Return True
    End Function

    Public Function NewCharacter() As Boolean
        If Me.Email = vbNullString Or Me.Player.Name = vbNullString Then Return False
        AccountData.CreateCharacter(Me)
        Return True
    End Function

    ' Saved variables
    Public Property Email() As String
        Get
            Return Me.mEmail
        End Get
        Set(ByVal value As String)
            If Not IsNothing(Me) Then
                Me.mEmail = value
            End If
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Me.mPassword
        End Get
        Set(ByVal value As String)
            If Not IsNothing(Me) Then
                Me.mPassword = Md5FromString(value)
            End If
        End Set
    End Property

    Public Property Player() As Players
        Get
            Return Me.mPlayer
        End Get
        Set(ByVal value As Players)
            If Not IsNothing(Me) Then
                Me.mPlayer = value
            End If
        End Set
    End Property
End Class
