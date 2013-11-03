Public Class Tilesets
    ' general
    Public Base As TilesetBase
    Public Shared Data As New TilesetData

    Sub New()
        Me.Base = New TilesetBase
    End Sub

    Public Sub Save()
        Tilesets.Data.Save(Me.Base)
    End Sub

    Public Sub Load()
        Dim loadTileset As New TilesetBase
        loadTileset = DirectCast(Files.ReadBinary(pathTilesets & Trim(Me.Base.ID) & ".bin"), TilesetBase)
        Me.Base.ID = loadTileset.ID
        Me.Base.Name = loadTileset.Name
        Me.Base.MaxX = loadTileset.MaxX
        Me.Base.MaxY = loadTileset.MaxY
        Me.Base.ResizeTileData(New Integer() {loadTileset.MaxX, loadTileset.MaxY})
        Me.Base.Tile = loadTileset.Tile
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
