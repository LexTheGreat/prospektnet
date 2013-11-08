Imports IHProspekt.Core
<Serializable()> Public Class Tilesets
    Public Base As TilesetBase
    Public Shared Logic As New TilesetLogic

    Sub New()
        Me.Base = New TilesetBase
    End Sub

    Public Sub ResizeTileData(ByVal newSize As Integer())
        Me.Base.ResizeTileData(newSize)
    End Sub

    Public Property ID() As Integer
        Get
            Return Me.Base.ID
        End Get

        Set(value As Integer)
            Me.Base.ID = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return Me.Base.Name
        End Get

        Set(value As String)
            Me.Base.Name = value
        End Set
    End Property

    Public Property MaxX() As Integer
        Get
            Return Me.Base.MaxX
        End Get

        Set(value As Integer)
            Me.Base.MaxX = value
        End Set
    End Property

    Public Property MaxY() As Integer
        Get
            Return Me.Base.MaxY
        End Get

        Set(value As Integer)
            Me.Base.MaxY = value
        End Set
    End Property

    Public Overloads Property Tile(ByVal X As Integer, ByVal Y As Integer) As Byte
        Get
            Return Me.Base.Tile(X, Y)
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.Base.Tile(X, Y) = value
            End If
        End Set
    End Property

    Public Overloads Property Tile As Byte(,)
        Get
            Return Me.Base.Tile
        End Get
        Set(value As Byte(,))
            If Not IsNothing(Me) Then
                Me.Base.Tile = value
            End If
        End Set
    End Property
End Class
