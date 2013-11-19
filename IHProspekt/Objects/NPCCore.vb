Imports IHProspekt.Core
Namespace Objects
    Public Class NPCBase
        ' general
        Public Property Name As String
        Public Property Sprite As Integer
        Public Property ID As Integer
        Public Property Level As Integer
        Public Property Health As Integer
        ' location
        Public Property X As Integer
        Public Property Y As Integer
        Public Property Dir As Byte
        ' Drop Table
        Public Property Inventory As Integer()

        Public Sub New()
            Me.Name = "New NPC"
            Me.ID = 0
            Me.Sprite = 1
            Me.Level = 1
            Me.Health = 1
            Me.Y = 15
            Me.X = 10
            Me.Dir = DirEnum.Down
            ReDim Inventory(0 To 0)
            Me.Inventory(0) = -1
        End Sub
    End Class
End Namespace