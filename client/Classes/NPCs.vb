Public Class NPCs
    ' general
    Private mName As String
    Private mSprite As Integer
    ' location
    Private mX As Integer
    Private mY As Integer
    Private mDir As Integer
    ' non-saved values
    Private mXOffset As Integer, mYOffset As Integer
    Private mMoving As Boolean = True
    Private mNpcStep As Byte
    Private mSpawned As Boolean = False

    Public Sub New()
        Me.mName = vbNullString
        Me.mSprite = 1
        Me.mY = 15
        Me.mX = 10
        Me.mDir = 1
    End Sub

    ' Saved variables
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

    Public Property Dir() As Integer
        Get
            Return Me.mDir
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mDir = value
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
        Dim MovementSpeed As Long

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

