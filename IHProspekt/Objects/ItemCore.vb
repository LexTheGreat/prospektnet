Imports IHProspekt.Core
Namespace Objects
    <Serializable()> Public Class ItemBase
        Public Property ID As Integer
        Public Property Name As String
        Public Property Sprite As Integer
        Public Property Health As Integer
        Public Property Type As Byte
        Public Property Stats As ItemStats
        Public Property Reqs As ItemRequirements

        Sub New()
            Me.ID = 0
            Me.Name = "New Item"
            Me.Sprite = 1
            Me.Health = 1
            Me.Type = ItemType.Armor
            Me.Stats = New ItemStats
            Me.Reqs = New ItemRequirements
        End Sub
    End Class

    <Serializable()> Public Class ItemStats
        Public Property Str As Integer
        Public Property Dex As Integer
        Public Property Int As Integer

        Sub New()
            Me.Str = 0
            Me.Dex = 0
            Me.Int = 0
        End Sub
    End Class

    <Serializable()> Public Class ItemRequirements
        Public Property Lvl As Integer
        Public Property Str As Integer
        Public Property Dex As Integer
        Public Property Int As Integer

        Sub New()
            Me.Lvl = 0
            Me.Str = 0
            Me.Dex = 0
            Me.Int = 0
        End Sub
    End Class
End Namespace