Imports IHProspekt.Objects
Imports IHProspekt.Core
Public Class NPCs
    ' general
    Public Base As NPCBase
    Public Shared Logic As New NPCLogic
    ' non-saved values
    Private mXOffset As Integer, mYOffset As Integer
    Private mMoving As Boolean = True
    Private mNpcStep As Byte
    Private mSpawned As Boolean = False

    Public Sub New()
        Me.Base = New NpcBase
    End Sub

    Public Sub Load(NewName As String, NewSprite As Integer, NewX As Integer, NewY As Integer, NewDir As Byte)
        Me.Base.Name = NewName
        Me.Base.Sprite = NewSprite
        Me.Base.X = NewX
        Me.Base.Y = NewY
        Me.Base.Dir = NewDir
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

    Public Property NpcStep() As Byte
        Get
            Return Me.mNpcStep
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.mNpcStep = value
            End If
        End Set
    End Property

    Public Property Spawned() As Boolean
        Get
            Return Me.mSpawned
        End Get
        Set(value As Boolean)
            If Not IsNothing(Me) Then
                Me.mSpawned = value
            End If
        End Set
    End Property

    Sub ProcessMovement()
        Dim MovementSpeed As Integer

        If mMoving = True Then
            MovementSpeed = 2
        Else
            Exit Sub
        End If

        Select Case Dir
            Case DirEnum.Up
                mYOffset = mYOffset - MovementSpeed
                If mYOffset < 0 Then mYOffset = 0
            Case DirEnum.Down
                mYOffset = mYOffset + MovementSpeed
                If mYOffset > 0 Then mYOffset = 0
            Case DirEnum.Left
                mXOffset = mXOffset - MovementSpeed
                If mXOffset < 0 Then mXOffset = 0
            Case DirEnum.Right
                mXOffset = mXOffset + MovementSpeed
                If mXOffset > 0 Then mXOffset = 0
        End Select

        ' Check if completed walking over to the next tile
        If mMoving = True Then
            If Dir = DirEnum.Right Or Dir = DirEnum.Down Then
                If (mXOffset >= 0) And (mYOffset >= 0) Then
                    mMoving = False
                    If mNpcStep = 0 Then
                        mNpcStep = 2
                    Else
                        mNpcStep = 0
                    End If
                End If
            Else
                If (mXOffset <= 0) And (mYOffset <= 0) Then
                    mMoving = False
                    If mNpcStep = 0 Then
                        mNpcStep = 2
                    Else
                        mNpcStep = 0
                    End If
                End If
            End If
        End If
    End Sub
End Class

