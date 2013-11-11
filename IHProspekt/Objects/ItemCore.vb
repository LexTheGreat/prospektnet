Imports IHProspekt.Core
Namespace Objects
    <Serializable()> Public Class ItemBase
        Private mID As Integer
        Private mName As String
        Private mSprite As Integer
        Private mHealth As Integer
        Private mType As Byte
        Private mStats As ItemStats
        Private mReqs As ItemRequirements

        Sub New()
            Me.mName = "New Item"
            Me.mID = 0
            Me.mSprite = 1
            Me.mHealth = 1
            Me.mType = ItemType.Armor
            Me.mStats = New ItemStats
            Me.mReqs = New ItemRequirements
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

        Public Property Health() As Integer
            Get
                Return Me.mHealth
            End Get
            Set(value As Integer)
                If Not IsNothing(Me) Then
                    Me.mHealth = value
                End If
            End Set
        End Property

        Public Property Type() As Integer
            Get
                Return Me.mType
            End Get
            Set(value As Integer)
                If Not IsNothing(Me) Then
                    Me.mType = value
                End If
            End Set
        End Property

        Public Property Stats() As ItemStats
            Get
                Return Me.mStats
            End Get
            Set(value As ItemStats)
                If Not IsNothing(Me) Then
                    Me.mStats = value
                End If
            End Set
        End Property

        Public Property Reqs() As ItemRequirements
            Get
                Return Me.mReqs
            End Get
            Set(value As ItemRequirements)
                If Not IsNothing(Me) Then
                    Me.mReqs = value
                End If
            End Set
        End Property
    End Class

    <Serializable()> Public Class ItemStats
        Public Str As Integer
        Public Dex As Integer
        Public Int As Integer

        Sub New()
            Me.Str = 0
            Me.Dex = 0
            Me.Int = 0
        End Sub
    End Class

    <Serializable()> Public Class ItemRequirements
        Public Lvl As Integer
        Public Str As Integer
        Public Dex As Integer
        Public Int As Integer

        Sub New()
            Me.Lvl = 0
            Me.Str = 0
            Me.Dex = 0
            Me.Int = 0
        End Sub
    End Class
End Namespace