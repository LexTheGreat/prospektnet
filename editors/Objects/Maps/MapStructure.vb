Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports System.ComponentModel
<Serializable()> Public Class MapStructure
    Private mID As Integer
    Private mName As String
    Private mMaxX As Long
    Private mMaxY As Long
    Private mColor As OverLayColor
    Private mLayer(MapLayerEnum.FringeMask) As LayerData

    Sub New()
        Me.mName = "New Map"
        Me.mMaxX = 35
        Me.mMaxY = 35
        Me.mColor = New OverLayColor()
        For x As Integer = MapLayerEnum.Ground To MapLayerEnum.FringeMask
            Me.mLayer(x) = New LayerData(Me.mMaxX, Me.mMaxY)
            ReSizeTileData(x, New Integer() {Me.mMaxX, Me.mMaxY})
        Next
    End Sub

    Sub Save()
        Dim fs As New FileStream(pathMaps & Trim$(Me.mID) & ".bin", FileMode.Create)
        Dim formatter As New BinaryFormatter

        formatter.Serialize(fs, Me)
        fs.Close()
    End Sub

    Sub Load()
        Dim loadMap As New MapStructure
        Dim fs As New FileStream(pathMaps & Trim(Me.mID) & ".bin", FileMode.Open)

        Dim formatter As New BinaryFormatter
        loadMap = DirectCast(formatter.Deserialize(fs), MapStructure)
        fs.Close()
        Me.mID = loadMap.mID
        Me.mName = loadMap.mName
        Me.mMaxX = loadMap.mMaxX
        Me.mMaxY = loadMap.mMaxY
        Me.mColor = loadMap.mColor
        Me.mLayer = loadMap.mLayer
    End Sub

    Public Sub ReSizeTileData(ByVal Layer As Byte, ByVal newSize As Integer())
        Me.mLayer(Layer).ReSizeTileData(Layer, newSize)
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

    Public Sub SetID(ByVal id As Integer)
        Me.mID = id
    End Sub

    Public Function Layer() As LayerData()
        Return Me.mLayer
    End Function

    ' Property Grid
    <CategoryAttribute("Properties"), _
       DisplayName("ID")> _
    ReadOnly Property ID() As String
        Get
            Return Me.mID
        End Get
    End Property

    <CategoryAttribute("Properties"), _
       DisplayName("Name")> _
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

    <CategoryAttribute("Properties"), _
       DisplayName("MaxX")> _
    Public Property MaxX() As Long
        Get
            Return Me.mMaxX
        End Get
        Set(value As Long)
            If Not IsNothing(Me) Then
                Me.mMaxX = value
            End If
        End Set
    End Property

    <CategoryAttribute("Properties"), _
       DisplayName("MaxY")> _
    Public Property MaxY() As Long
        Get
            Return Me.mMaxY
        End Get
        Set(value As Long)
            If Not IsNothing(Me) Then
                Me.mMaxY = value
            End If
        End Set
    End Property

    <CategoryAttribute("Overlay"), _
       DisplayName("Alpha")> _
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

    <CategoryAttribute("Overlay"), _
       DisplayName("Red")> _
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

    <CategoryAttribute("Overlay"), _
       DisplayName("Green")> _
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

    <CategoryAttribute("Overlay"), _
       DisplayName("Blue")> _
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
End Class
