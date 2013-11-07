Imports Prospekt.Network
Public Class NPCs
    ' general
    Public Base As NPCBase
    Public Shared Data As New NPCData
    ' non-saved values
    Private mIndex As Integer

    Public Sub New()
        Me.Base = New NPCBase
    End Sub

    Public Property Name() As String
        Get
            Return Me.Base.Name
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Name = value
            End If
        End Set
    End Property

    Public Property Sprite() As Integer
        Get
            Return Me.Base.Sprite
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Sprite = value
            End If
        End Set
    End Property

    Public Property X() As Integer
        Get
            Return Me.Base.X
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.X = value
            End If
        End Set
    End Property

    Public Property Y() As Integer
        Get
            Return Me.Base.Y
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Y = value
            End If
        End Set
    End Property

    Public Property Dir() As Byte
        Get
            Return Me.Base.Dir
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.Base.Dir = value
            End If
        End Set
    End Property

    Public WriteOnly Property Index As Integer
        Set(value As Integer)
            Me.mIndex = value
        End Set
    End Property
End Class
