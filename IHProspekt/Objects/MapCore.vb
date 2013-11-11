Imports IHProspekt.Core
Namespace Objects
    <Serializable()> Public Class MapBase
        Public Property ID As Integer
        Public Property Name As String
        Public Property MaxX As Integer
        Public Property MaxY As Integer
        Public Property Color As OverLayColor
        Private mLayer(MapLayerEnum.COUNT - 1) As LayerData
        Public NPC(0) As MapNPCBase
        Public Property NPCCount As Integer

        Sub New()
            Me.Name = "New Map"
            Me.MaxX = 35
            Me.MaxY = 35
            Me.NPCCount = -1
            Me.Color = New OverLayColor()
            For x As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
                Me.mLayer(x) = New LayerData(Me.MaxX, Me.MaxY)
                Me.mLayer(x).ResizeTileData(New Integer() {Me.MaxX, Me.MaxY})
            Next
        End Sub

        Public Overloads Property Layer(ByVal Index As Byte) As LayerData
            Get
                Return Me.mLayer(Index)
            End Get
            Set(value As LayerData)
                If Not IsNothing(Me) Then
                    Me.mLayer(Index) = value
                End If
            End Set
        End Property

        Public Overloads Property Layer As LayerData()
            Get
                Return Me.mLayer
            End Get
            Set(value As LayerData())
                If Not IsNothing(Me) Then
                    Me.mLayer = value
                End If
            End Set
        End Property
    End Class

    <Serializable()> Public Class OverLayColor
        Public Property Alpha As Byte
        Public Property Red As Byte
        Public Property Green As Byte
        Public Property Blue As Byte

        Sub New()
            Me.Alpha = 0
            Me.Red = 255
            Me.Green = 255
            Me.Blue = 255
        End Sub
    End Class

    <Serializable()> Public Class LayerData
        Private mTiles(,) As TileData

        Sub New(ByVal maxX As Integer, ByVal maxY As Integer)
            ReDim Me.mTiles(maxX, maxY)
            For X As Integer = 0 To Me.mTiles.GetUpperBound(0)
                For Y As Integer = 0 To Me.mTiles.GetUpperBound(1)
                    Me.mTiles(X, Y) = New TileData
                Next
            Next
        End Sub

        Public Sub SetTileData(ByVal Data As TileData(,))
            Me.mTiles = Data
        End Sub

        Public Sub SetTileData(ByVal X As Integer, ByVal Y As Integer, ByVal data As TileData)
            Me.mTiles(X, Y) = data
        End Sub

        Public Function GetTileData() As TileData(,)
            Return Me.mTiles
        End Function

        Public Function GetTileData(ByVal X As Integer, ByVal Y As Integer) As TileData
            Return Me.mTiles(X, Y)
        End Function

        Public Sub ResizeTileData(ByVal newSize As Integer())
            Dim newTiles(newSize(0), newSize(1)) As TileData
            For x As Integer = 0 To newSize(0)
                For y As Integer = 0 To newSize(1)
                    If Me.mTiles.GetUpperBound(0) <= x Or Me.mTiles.GetUpperBound(1) <= y Then
                        newTiles(x, y) = New TileData
                    Else
                        newTiles(x, y) = Me.mTiles(x, y)
                    End If
                Next
            Next
            Me.mTiles = newTiles
        End Sub
    End Class

    <Serializable()> Public Class TileData
        Public Property Tileset As Integer
        Public Property X As Integer
        Public Property Y As Integer

        Sub New()
            Me.Tileset = 0
            Me.X = 0
            Me.Y = 0
        End Sub
    End Class
End Namespace
