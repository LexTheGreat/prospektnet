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

    Public Function GetMoving() As Boolean
        If Not IsNothing(Me) Then
            Return Me.mMoving
        End If
        Return False
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
        Dim i As Integer
        i = Int(Rnd() * 2)

        If i = 1 Then
            i = Int(Rnd() * 4)
            If NPCLogic.CanNPCMove(Me.mIndex, i) Then
                Select Case i
                    Case DirEnum.Up
                        Me.mY = Me.mY - 1
                    Case DirEnum.Down
                        Me.mY = Me.mY + 1
                    Case DirEnum.Left
                        Me.mX = Me.mX - 1
                    Case DirEnum.Right
                        Me.mX = Me.mX + 1
                End Select
                Me.mDir = i
                Me.mMoving = True
                SendData.NPCPosition(Me.mIndex)
            Else
                Me.mMoving = False
            End If
        Else
            Me.mMoving = False
        End If
    End Sub
End Class
