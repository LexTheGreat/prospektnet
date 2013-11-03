Public Class Maps
    Public Base As MapBase
    Public Shared Logic As New MapLogic

    Sub New()
        Me.Base = New MapBase
    End Sub

    Public Sub Load(Name As String, MaxX As Integer, MaxY As Integer, Color As OverLayColor, Layer() As LayerData)
        Me.Base.Name = Name
        Me.Base.MaxX = MaxX
        Me.Base.MaxY = MaxY
        Me.Base.Color = Color
        Me.Base.Layer = Layer
    End Sub

    ' Saved variables
    ReadOnly Property ID() As String
        Get
            Return Me.Base.ID
        End Get
    End Property

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

    Public Property MaxX() As Integer
        Get
            Return Me.Base.MaxX
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.MaxX = value
            End If
        End Set
    End Property

    Public Property MaxY() As Integer
        Get
            Return Me.Base.MaxY
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.MaxY = value
            End If
        End Set
    End Property

    Public Property Alpha() As Byte
        Get
            Return Me.Base.Color.Alpha
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.Base.Color.Alpha = value
            End If
        End Set
    End Property

    Public Property Red() As Byte
        Get
            Return Me.Base.Color.Red
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.Base.Color.Red = value
            End If
        End Set
    End Property

    Public Property Green() As Byte
        Get
            Return Me.Base.Color.Green
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.Base.Color.Green = value
            End If
        End Set
    End Property

    Public Property Blue() As Byte
        Get
            Return Me.Base.Color.Blue
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.Base.Color.Blue = value
            End If
        End Set
    End Property

    Public Sub SetID(ByVal id As Integer)
        Me.Base.ID = id
    End Sub

    Public Sub SetTileData(ByVal Layer As Byte, ByVal data(,) As TileData)
        Me.Base.Layer(Layer).ReSizeTileData(New Integer() {data.GetUpperBound(0), data.GetUpperBound(1)})
        Me.Base.Layer(Layer).SetTileData(data)
    End Sub

    Public Sub SetTileData(ByVal Layer As Byte, ByVal X As Integer, ByVal Y As Integer, ByVal data As TileData)
        Me.Base.Layer(Layer).SetTileData(X, Y, data)
    End Sub

    Public Function GetTileData(ByVal Layer As Byte) As TileData(,)
        Return Me.Base.Layer(Layer).GetTileData
    End Function

    Public Function GetTileData(ByVal Layer As Byte, ByVal X As Integer, ByVal Y As Integer) As TileData
        Return Me.Base.Layer(Layer).GetTileData(X, Y)
    End Function

    Public Sub ReSizeTileData(ByVal Layer As Byte, ByVal newSize As Integer())
        Me.Base.Layer(Layer).ReSizeTileData(newSize)
    End Sub

    Public Function Layer() As LayerData()
        Return Me.Base.Layer
    End Function
End Class
