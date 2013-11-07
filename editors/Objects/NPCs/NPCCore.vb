Public Class NPCBase
    ' general
    Private mID As Integer
    Private mName As String
    Private mSprite As Integer

    Public Sub New()
        Me.mID = 0
        Me.mName = "New Npc"
        Me.mSprite = 1
    End Sub

    Public Property ID() As Integer
        Get
            Return Me.mID
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mID = value
            End If
        End Set
    End Property

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
End Class
