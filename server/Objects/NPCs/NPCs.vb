Imports Prospekt.Network
Imports IHProspekt.Core
Public Class NPCs
    ' general
    Public Base As NPCBase
    Public Shared Data As New NPCData
    Public Shared Logic As New NPCLogic
    ' non-saved values
    Private mIndex As Integer
    Private mXOffset As Integer, mYOffset As Integer
    Private mMoving As Boolean
    Private mStep As Byte
    Private mSpawned As Boolean

    Public Sub New()
        Me.Base = New NPCBase
    End Sub

    ' sub routines and functions
    Public Function Save() As Boolean
        If Me.Name = vbNullString Then Return False
        NPCs.Data.SaveNPC(Me.Base)
        Return True
    End Function

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

    Public ReadOnly Property Moving() As Boolean
        Get
            Return Me.mMoving
        End Get
    End Property

    Public WriteOnly Property Index As Integer
        Set(value As Integer)
            Me.mIndex = value
        End Set
    End Property

    Public Sub GenerateMovement()
        Dim i As Integer
        i = Int(Rnd() * 2)

        If i = 1 Then
            i = Int(Rnd() * 4)
            If NPCs.Logic.CanNPCMove(Me.mIndex, i) Then
                Select Case i
                    Case DirEnum.Up
                        Me.Base.Y = Me.Base.Y - 1
                    Case DirEnum.Down
                        Me.Base.Y = Me.Base.Y + 1
                    Case DirEnum.Left
                        Me.Base.X = Me.Base.X - 1
                    Case DirEnum.Right
                        Me.Base.X = Me.Base.X + 1
                End Select
                Me.Base.Dir = i
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
