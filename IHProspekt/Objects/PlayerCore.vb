Imports IHProspekt.Core
Namespace Objects
    Public Class PlayerBase
        Public Property Name As String
        Public Property Sprite As Integer
        ' location
        Public Property X As Integer
        Public Property Y As Integer
        Public Property Dir As Integer
        Public Property Map As Integer
        ' Admin values
        Public Property AccessMode As Byte
        Public Property Visible As Boolean
        ' Inventories
        Public Property Inventory As Integer()

        Public Sub New()
            Me.Name = "New Player"
            Me.Sprite = 1
            Me.Y = 15
            Me.X = 10
            Me.Dir = 1
            Me.AccessMode = ACCESS.NONE
            Me.Visible = True
            Me.Map = 0
            ReDim Inventory(0 To 0)
            Me.Inventory(0) = -1
        End Sub
    End Class
End Namespace