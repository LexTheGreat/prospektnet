Public Class NPCBase
    ' general
    Private mName As String
    Private mSprite As Integer
    ' location
    Private mX As Integer
    Private mY As Integer
    Private mDir As Byte

    Public Sub New()
        Me.mName = vbNullString
        Me.mSprite = 1
        Me.mY = 15
        Me.mX = 10
        Me.mDir = DirEnum.Down
    End Sub

    Public Property Name() As String
        Get
            Return Me.mName
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.mName = value
            End If
        End Set
    End Property

    Public Property Sprite() As Integer
        Get
            Return Me.mSprite
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mSprite = value
            End If
        End Set
    End Property

    Public Property X() As Integer
        Get
            Return Me.mX
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mX = value
            End If
        End Set
    End Property

    Public Property Y() As Integer
        Get
            Return Me.mY
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mY = value
            End If
        End Set
    End Property

    Public Property Dir() As Byte
        Get
            Return Me.mDir
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.mDir = value
            End If
        End Set
    End Property
End Class
