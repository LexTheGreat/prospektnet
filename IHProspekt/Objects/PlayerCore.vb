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
        Private mInventory() As Integer

        Public Sub New()
            Me.Name = "New Player"
            Me.Sprite = 1
            Me.Y = 15
            Me.X = 10
            Me.Dir = 1
            Me.AccessMode = ACCESS.NONE
            Me.Visible = True
            Me.Map = 0
            ReDim mInventory(0 To 0)
            Me.mInventory(0) = -1
        End Sub

        Public Property Inventory As Integer()
            Get
                Return Me.mInventory
            End Get
            Set(value As Integer())
                If Not IsNothing(Me) Then
                    Me.mInventory = value
                End If
            End Set
        End Property
    End Class
End Namespace