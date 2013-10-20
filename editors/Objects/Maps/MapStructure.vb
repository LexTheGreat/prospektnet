Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
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
        Next
        ReSizeTileData(New Integer() {Me.mMaxX, Me.mMaxY})
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
        ReSizeTileData(New Integer() {loadMap.mMaxX, loadMap.mMaxY})
    End Sub

    Public Sub SetID(ByVal id As Integer)
        Me.mID = id
    End Sub

    Public Sub SetTileData(ByVal Layer As Byte, ByVal data(,) As TileData)
        Me.mLayer(Layer).ReSizeTileData(New Integer() {Me.MaxX, Me.MaxY})
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

    Public Sub ReSizeTileData(ByVal newSize As Integer())
        For x As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
            Me.mLayer(x).ReSizeTileData(newSize)
        Next
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

    <CategoryAttribute("Properties"), _
       DisplayName("MaxY")> _
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
