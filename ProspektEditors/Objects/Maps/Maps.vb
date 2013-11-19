Imports System.ComponentModel
Imports IHProspekt.Objects
Imports IHProspekt.Core
Imports IHProspekt.Database
Imports IHProspekt.Utilities
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
        Using File As New Files(pathMaps & Trim(Me.Base.ID) & ".bin", loadMap)
            loadMap = DirectCast(File.ReadBinary, MapBase)
        End Using
        Me.Base = loadMap
    End Sub

    Public Sub AddNPC(ByVal NPC As MapNPCBase)
        Base.NPCCount += 1
        ReDim Preserve Base.NPC(0 To Base.NPCCount)
        Base.NPC(Base.NPCCount) = NPC
    End Sub

    Public Sub RemoveNpc(ByVal X As Integer, ByVal Y As Integer)
        Dim slot As Integer = 0
        For slot = 0 To Base.NPC.Length - 1
            If Base.NPC(slot).X = X And Base.NPC(slot).Y = Y Then Exit For
        Next
        If slot >= Base.NPC.Length Then Exit Sub
        Base.NPCCount = Base.NPCCount - 1
        RemoveAt(Base.NPC, slot)
    End Sub

    Public Sub SetID(ByVal id As Integer)
        Me.Base.ID = id
    End Sub

    Public Sub SetTileData(ByVal Layer As Byte, ByVal data(,) As TileData)
        Me.Base.Layer(Layer).ReSizeTileData(New Integer() {Me.Base.MaxX, Me.Base.MaxY})
        Me.Base.Layer(Layer).Tiles = data
    End Sub

    Public Sub SetTileData(ByVal Layer As Byte, ByVal X As Integer, ByVal Y As Integer, ByVal data As TileData)
        Me.Base.Layer(Layer).Tiles(X, Y) = data
    End Sub

    Public Function GetTileData(ByVal Layer As Byte) As TileData(,)
        Return Me.Base.Layer(Layer).Tiles
    End Function

    Public Function GetTileData(ByVal Layer As Byte, ByVal X As Integer, ByVal Y As Integer) As TileData
        Return Me.Base.Layer(Layer).Tiles(X, Y)
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

    <CategoryAttribute("Tile Overlay"), _
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

    <CategoryAttribute("Tile Overlay"), _
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

    <CategoryAttribute("Tile Overlay"), _
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

    <CategoryAttribute("Tile Overlay"), _
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
