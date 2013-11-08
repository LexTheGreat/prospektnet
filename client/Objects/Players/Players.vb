Imports IHProspekt.Core
Public Class Players
    ' general
    Public Base As PlayerBase
    Public Shared Logic As New PlayerLogic
    ' non-saved values
    Private mXOffset As Integer, mYOffset As Integer
    Private mMoving As Boolean
    Private mPlayerStep As Byte

    Public Sub New()
        Me.Base = New PlayerBase
    End Sub

    ' sub routines and functions
    Public Sub Load(NewName As String, NewSprite As Integer, NewX As Integer, NewY As Integer, NewDir As Byte, NewAccess As Byte, NewVisible As Boolean)
        Me.Base.Name = NewName
        Me.Base.Sprite = NewSprite
        Me.Base.X = NewX
        Me.Base.Y = NewY
        Me.Base.Dir = NewDir
        Me.Base.AccessMode = NewAccess
        Me.Base.Visible = NewVisible
    End Sub

    Sub ProcessMovement()
        Dim MovementSpeed As Integer

        If Moving = True Then
            MovementSpeed = 2
        Else
            Exit Sub
        End If

        Select Case Dir
            Case DirEnum.Up
                Me.mYOffset = Me.mYOffset - MovementSpeed
                If Me.mYOffset < 0 Then Me.mYOffset = 0
            Case DirEnum.Down
                Me.mYOffset = Me.mYOffset + MovementSpeed
                If Me.mYOffset > 0 Then Me.mYOffset = 0
            Case DirEnum.Left
                Me.mXOffset = Me.mXOffset - MovementSpeed
                If Me.mXOffset < 0 Then Me.mXOffset = 0
            Case DirEnum.Right
                XOffset = XOffset + MovementSpeed
                If Me.mXOffset > 0 Then Me.mXOffset = 0
        End Select

        ' Check if completed walking over to the next tile
        If Me.mMoving = True Then
            If Me.Base.Dir = DirEnum.Right Or Me.Base.Dir = DirEnum.Down Then
                If (Me.mXOffset >= 0) And (Me.mYOffset >= 0) Then
                    Me.mMoving = False
                    If Me.mPlayerStep = 0 Then
                        Me.mPlayerStep = 2
                    Else
                        Me.mPlayerStep = 0
                    End If
                End If
            Else
                If (Me.mXOffset <= 0) And (Me.mYOffset <= 0) Then
                    Me.mMoving = False
                    If Me.mPlayerStep = 0 Then
                        Me.mPlayerStep = 2
                    Else
                        Me.mPlayerStep = 0
                    End If
                End If
            End If
        End If
    End Sub
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
    Public Property XOffset() As Integer
        Get
            Return Me.mXOffset
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mXOffset = value
            End If
        End Set
    End Property

    Public Property YOffset() As Integer
        Get
            Return Me.mYOffset
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mYOffset = value
            End If
        End Set
    End Property

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

    Public Property PlayerStep() As Byte
        Get
            Return Me.mPlayerStep
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.mPlayerStep = value
            End If
        End Set
    End Property
End Class
