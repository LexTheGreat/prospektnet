Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.ComponentModel

<Serializable()> Public Class MapStructure
    Private mID As Integer
    Private mName As String
    Private mMaxX As Integer
    Private mMaxY As Integer
    Private mColor As OverLayColor
    Private mLayer(MapLayerEnum.COUNT - 1) As LayerData

    Sub New()
        Me.mName = "New Map"
        Me.mMaxX = 35
        Me.mMaxY = 35
        Me.mColor = New OverLayColor()
        For x As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
            Me.mLayer(x) = New LayerData(Me.mMaxX, Me.mMaxY)
            ReSizeTileData(x, New Integer() {Me.mMaxX, Me.mMaxY})
        Next
    End Sub

    Sub Save()
        MapData.Save(Me)
    End Sub

    Sub Load()
        Dim loadMap As New MapStructure
        loadMap = DirectCast(Files.ReadBinary(pathMaps & Trim(Me.mID) & ".bin"), MapStructure)
        Me.mID = loadMap.mID
        Me.mName = loadMap.mName
        Me.mMaxX = loadMap.mMaxX
        Me.mMaxY = loadMap.mMaxY
        Me.mColor = loadMap.mColor
        Me.mLayer = loadMap.mLayer
    End Sub

    ' Saved variables
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
            If Not IsNothing(Me) Then
                Me.mName = value
            End If
        End Set
    End Property

    Public Property MaxX() As Integer
        Get
            Return Me.mMaxX
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mMaxX = value
            End If
        End Set
    End Property

    Public Property MaxY() As Integer
        Get
            Return Me.mMaxY
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mMaxY = value
            End If
        End Set
    End Property

    Public Property Alpha() As Byte
        Get
            Return Me.mColor.Alpha
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.mColor.Alpha = value
            End If
        End Set
    End Property

    Public Property Red() As Byte
        Get
            Return Me.mColor.Red
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.mColor.Red = value
            End If
        End Set
    End Property

    Public Property Green() As Byte
        Get
            Return Me.mColor.Green
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.mColor.Green = value
            End If
        End Set
    End Property

    Public Property Blue() As Byte
        Get
            Return Me.mColor.Blue
        End Get
        Set(value As Byte)
            If Not IsNothing(Me) Then
                Me.mColor.Blue = value
            End If
        End Set
    End Property

    Public Sub SetID(ByVal id As Integer)
        Me.mID = id
    End Sub

    Public Sub SetTileData(ByVal Layer As Byte, ByVal data(,) As TileData)
        Me.mLayer(Layer).ReSizeTileData(Layer, New Integer() {data.GetUpperBound(0), data.GetUpperBound(1)})
        Me.mLayer(Layer).SetTileData(data)
    End Sub

    Public Sub SetTileData(ByVal Layer As Byte, ByVal X As Integer, ByVal Y As Integer, ByVal data As TileData)
        Me.mLayer(Layer).SetTileData(X, Y, data)
    End Sub

    Public Function GetTileData(ByVal Layer As Byte) As TileData(,)
        Return Me.mLayer(Layer).GetTileData
    End Function

    Public Function GetTileData(ByVal Layer As Byte, ByVal X As Integer, ByVal Y As Integer) As TileData
        Return Me.mLayer(Layer).GetTileData(X, Y)
    End Function

    Public Sub ReSizeTileData(ByVal Layer As Byte, ByVal newSize As Integer())
        Me.mLayer(Layer).ReSizeTileData(Layer, newSize)
    End Sub

    Public Function Layer() As LayerData()
        Return Me.mLayer
    End Function
End Class
