Imports IHProspekt.Core
Namespace Objects
    <Serializable()> Public Class MapNPCBase
        ' NPC number of this map npc
        Public Property Num As Integer
        ' Map NPC spawn location on map
        Public Property X As Integer
        Public Property Y As Integer
        Public Property Dir As Byte

        Public Sub New()
            Me.Num = 0
            Me.X = 10
            Me.Y = 10
            Me.Dir = DirEnum.Down
        End Sub
    End Class
End Namespace