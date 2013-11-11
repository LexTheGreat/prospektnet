Namespace Objects
    <Serializable()> Public Class TilesetBase
        Public Property ID As Integer
        Public Property Name As String
        Public Property MaxX As Integer
        Public Property MaxY As Integer
        Private mTile(0, 0) As Byte

        Sub New()
            Me.Name = "New Tileset"
        End Sub

        Public Overloads Property Tile(ByVal X As Integer, ByVal Y As Integer) As Byte
            Get
                Return Me.mTile(X, Y)
            End Get
            Set(value As Byte)
                If Not IsNothing(Me) Then
                    Me.mTile(X, Y) = value
                End If
            End Set
        End Property

        Public Overloads Property Tile As Byte(,)
            Get
                Return Me.mTile
            End Get
            Set(value As Byte(,))
                If Not IsNothing(Me) Then
                    Me.mTile = value
                End If
            End Set
        End Property

        Public Sub ResizeTileData(ByVal newSize As Integer())
            Dim newTiles(newSize(0), newSize(1)) As Byte
            For x As Integer = 0 To newSize(0)
                For y As Integer = 0 To newSize(1)
                    If Me.mTile.GetUpperBound(0) <= x Or Me.mTile.GetUpperBound(1) <= y Then
                        newTiles(x, y) = 0
                    Else
                        newTiles(x, y) = Me.mTile(x, y)
                    End If
                Next
            Next
            Me.mTile = newTiles
        End Sub
    End Class
End Namespace