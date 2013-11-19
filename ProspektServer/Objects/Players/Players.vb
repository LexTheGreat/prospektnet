Imports IHProspekt.Objects

Public Class Players
    ' general
    Public Base As PlayerBase
    Public Shared Data As New PlayerData
    Public Shared Logic As New PlayerLogic
    ' non-saved values
    Private mXOffset As Integer, mYOffset As Integer
    Private mMoving As Boolean
    Private mPlayerStep As Byte
    Private mIsPlaying As Boolean

    Public Sub New()
        Me.Base = New PlayerBase
    End Sub

    ' sub routines and functions
    Public Function Create(ByVal PName As String) As Boolean
        If PName = vbNullString Then Return False
        Players.Data.Create(PName)
        Return True
    End Function

    Public Function Load(ByVal PName As String) As Boolean
        Try
            Dim newPlayer As New PlayerBase()
            newPlayer = Players.Data.GetPlayer(PName)
            'Check if account is valid
            If newPlayer.Name = vbNullString Then Return False
            ' Load player
            Me.Name = newPlayer.Name
            Me.Sprite = newPlayer.Sprite
            Me.Y = newPlayer.Y
            Me.X = newPlayer.X
            Me.Dir = newPlayer.Dir
            Me.AccessMode = newPlayer.AccessMode
            Me.Visible = newPlayer.Visible
            Return True
        Catch ex As Exception
            Server.Writeline("Error: " & ex.ToString & " (In: Players.Load)")
            Return False
        End Try
    End Function

    Public Function Save() As Boolean
        If Me.Name = vbNullString Then Return False
        Players.Data.Save(Me.Base)
        Return True
    End Function

    ' Saved variables
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

    Public Property Map() As Integer
        Get
            Return Me.Base.Map
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Map = value
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

    Public Property AccessMode() As Byte
        Get
            Return Me.Base.AccessMode
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.Base.AccessMode = value
            End If
        End Set
    End Property

    Public Property Visible() As Boolean
        Get
            Return Me.Base.Visible
        End Get
        Set(value As Boolean)
            If Not IsNothing(Me) Then
                Me.Base.Visible = value
            End If
        End Set
    End Property

    ' Non Saved
    Public Property Moving() As Boolean
        Get
            Return Me.mMoving
        End Get
        Set(value As Boolean)
            If Not IsNothing(Me) Then
                Me.mMoving = value
            End If
        End Set
    End Property

    Public Property IsPlaying() As Boolean
        Get
            Return Me.mIsPlaying
        End Get
        Set(value As Boolean)
            If Not IsNothing(Me) Then
                Me.mIsPlaying = value
            End If
        End Set
    End Property
End Class
