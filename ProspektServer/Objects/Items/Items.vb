Imports Prospekt.Network
Imports IHProspekt.Objects
Imports IHProspekt.Core
Imports IHProspekt.Database
Public Class Items
    ' general
    Public Base As ItemBase
    Public Shared Data As New ItemData

    Public Sub New()
        Me.Base = New ItemBase
    End Sub

    Sub Save()
        Items.Data.Save(Me.Base)
    End Sub

    Sub Load()
        Dim loadItem As New ItemBase
        loadItem = DirectCast(ReadXML(pathItems & Trim(Me.Base.ID) & ".xml", loadItem), ItemBase)
        Me.Base = loadItem
    End Sub

    Public Property ID() As Integer
        Get
            Return Me.Base.ID
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.ID = value
            End If
        End Set
    End Property

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

    Public Property Health() As Integer
        Get
            Return Me.Base.Health
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Health = value
            End If
        End Set
    End Property

    Public Property Type() As Integer
        Get
            Return Me.Base.Type
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Type = value
            End If
        End Set
    End Property

    Public Property sStrengh() As Integer
        Get
            Return Me.Base.Stats.Str
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Stats.Str = value
            End If
        End Set
    End Property

    Public Property sDexterity() As Integer
        Get
            Return Me.Base.Stats.Dex
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Stats.Dex = value
            End If
        End Set
    End Property

    Public Property sIntelligence() As Integer
        Get
            Return Me.Base.Stats.Int
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Stats.Int = value
            End If
        End Set
    End Property

    Public Property rLevel() As Integer
        Get
            Return Me.Base.Reqs.Lvl
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Reqs.Lvl = value
            End If
        End Set
    End Property

    Public Property rStrengh() As Integer
        Get
            Return Me.Base.Reqs.Str
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Reqs.Str = value
            End If
        End Set
    End Property

    Public Property rDexterity() As Integer
        Get
            Return Me.Base.Reqs.Dex
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Reqs.Dex = value
            End If
        End Set
    End Property

    Public Property rIntelligence() As Integer
        Get
            Return Me.Base.Reqs.Int
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Reqs.Int = value
            End If
        End Set
    End Property
End Class
