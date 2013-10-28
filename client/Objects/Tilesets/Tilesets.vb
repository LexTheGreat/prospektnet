<Serializable()> Public Class Tilesets
    Private mID As Integer
    Private mName As String
    Private mMaxX As Integer
    Private mMaxY As Integer
    Public Tile(0, 0) As Byte

    Public Sub ResizeArray(ByVal newSize As Integer())
        Dim newTiles(newSize(0), newSize(1)) As Byte
        For x As Integer = 0 To newSize(0)
            For y As Integer = 0 To newSize(1)
                If Me.Tile.GetUpperBound(0) <= x Or Me.Tile.GetUpperBound(1) <= y Then
                    newTiles(x, y) = 0
                Else
                    newTiles(x, y) = Me.Tile(x, y)
                End If
            Next
        Next
        Me.Tile = newTiles
    End Sub

    Public Sub SetID(ByVal id As Integer)
        Me.mID = id
    End Sub

    ReadOnly Property ID() As String
        Get
            Return Me.mID
        End Get
    End Property

    Public Property Name() As String
        Get
            Return Me.mName
        End Get

        Set(value As String)
            Me.mName = value
        End Set
    End Property

    Public Property MaxX() As Integer
        Get
            Return Me.mMaxX
        End Get

        Set(value As Integer)
            Me.mMaxX = value
        End Set
    End Property

    Public Property MaxY() As Integer
        Get
            Return Me.mMaxY
        End Get

        Set(value As Integer)
            Me.mMaxY = value
        End Set
    End Property
End Class
