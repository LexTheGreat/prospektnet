Public Class NPCs
    ' general
    Private mName As String
    Private mSprite As Integer
    ' location
    Private mX As Integer
    Private mY As Integer
    Private mDir As Byte
    ' non-saved values
    Private mIndex As Integer
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

    ' sub routines and functions
    Public Function Create() As Boolean
        If Me.Name = vbNullString Then Return False
        NPCData.CreateNPC(Me)
        Return True
    End Function

    Public Function Save() As Boolean
        If Me.Name = vbNullString Then Return False
        NPCData.SaveNPC(Me)
        Return True
    End Function

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

    Public Sub SetIndex(ByVal index As Integer)
        Me.mIndex = index
    End Sub

    Public Sub GenerateMovement()
        Dim newX As Integer, newY As Integer
        If RandomNumber(50) <= 30 Then ' Move Npc
            If RandomNumber(1) = 1 Then ' See if moving vertical or horizontal
                If Me.mX - 1 < 0 Then newX = RandomNumber(1) Else newX = RandomNumber(Me.mX + 1, Me.mX - 1)
                If newX > Me.mX Then Me.mDir = DirEnum.Right Else Me.mDir = DirEnum.Left
                If newX < 0 Then newX = 1 And Me.mDir = DirEnum.Right
                Me.mX = newX
            Else
                If Me.mY - 1 < 0 Then newY = RandomNumber(1) Else newY = RandomNumber(Me.mY + 1, Me.mY - 1)
                If newY > Me.mY Then Me.mDir = DirEnum.Down Else Me.mDir = DirEnum.Up
                If newY < 0 Then newY = 1 And Me.mDir = DirEnum.Down
                Me.mY = newY
            End If
            SendNPCPosition(Me.mIndex)
        End If
    End Sub
End Class
