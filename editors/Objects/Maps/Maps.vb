Imports System.ComponentModel
Imports IHProspekt.Core
<Serializable()> Public Class Maps
    Public Base As MapBase
    Public Shared Data As New MapData

    Sub New()
        Me.Base = New MapBase
    End Sub

    Sub Save()
        Maps.Data.Save(Me.Base)
    End Sub

    Sub Load()
        Dim loadMap As New MapBase
        loadMap = DirectCast(Files.ReadBinary(pathMaps & Trim(Me.Base.ID) & ".bin"), MapBase)
        Me.Base.ID = loadMap.ID
        Me.Base.Name = loadMap.Name
        Me.Base.MaxX = loadMap.MaxX
        Me.Base.MaxY = loadMap.MaxY
        Me.Base.Color = loadMap.Color
        Me.Base.Layer = loadMap.Layer
    End Sub

    Public Sub AddNPC(ByVal NPC As MapNPCBase)
        Base.NPCCount += 1
        ReDim Preserve Base.NPC(0 To Base.NPCCount)
        Base.NPC(Base.NPCCount) = NPC
    End Sub

    Public Sub SetID(ByVal id As Integer)
        Me.Base.ID = id
    End Sub

    Public Sub SetTileData(ByVal Layer As Byte, ByVal data(,) As TileData)
        Me.Base.Layer(Layer).ReSizeTileData(New Integer() {Me.Base.MaxX, Me.Base.MaxY})
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

    Public Sub ReSizeTileData(ByVal newSize As Integer())
        For x As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
            Me.Base.Layer(x).ReSizeTileData(newSize)
        Next
    End Sub

    Public Function Layer() As LayerData()
        Return Me.Base.Layer
    End Function

    ' Property Grid
    <CategoryAttribute("Properties"), _
       DisplayName("ID")> _
    ReadOnly Property ID() As Integer
        Get
            Return Me.Base.ID
        End Get
    End Property

    <CategoryAttribute("Properties"), _
       DisplayName("Name")> _
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

    <CategoryAttribute("Properties"), _
       DisplayName("MaxX")> _
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

    <CategoryAttribute("Properties"), _
       DisplayName("MaxY")> _
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

    <CategoryAttribute("Overlay"), _
       DisplayName("Alpha")> _
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

    <CategoryAttribute("Overlay"), _
       DisplayName("Red")> _
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

    <CategoryAttribute("Overlay"), _
       DisplayName("Green")> _
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

    <CategoryAttribute("Overlay"), _
       DisplayName("Blue")> _
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
End Class
