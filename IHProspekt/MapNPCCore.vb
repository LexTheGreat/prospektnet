Namespace Core
    Public Class MapNPCBase
        ' NPC number of this map npc
        Private mNum As Integer
        ' Map NPC spawn location on map
        Private mX As Integer
        Private mY As Integer
        Private mDir As Byte

        Public Sub New()
            Me.mNum = 0
            Me.mX = 10
            Me.mY = 10
            Me.mDir = DirEnum.Down
        End Sub

        Public Property Num() As Integer
            Get
                Return Me.mNum
            End Get
            Set(value As Integer)
                If Not IsNothing(Me) Then
                    Me.mNum = value
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

        Public Property Dir() As Byte
            Get
                Return Me.mDir
            End Get
            Set(value As Byte)
                If Not IsNothing(Me) Then
                    Me.mDir = value
                End If
            End Set
        End Property
    End Class
End Namespace