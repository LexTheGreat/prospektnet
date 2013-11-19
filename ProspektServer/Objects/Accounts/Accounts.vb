Imports IHProspekt.Objects
Imports IHProspekt.Utilities
Public Class Accounts
    ' general
    Public Base As AccountBase
    Public Shared Data As New AccountData

    Public Sub New()
        Me.Base = New AccountBase
    End Sub

    Public Function Create() As Boolean
        If Me.Email = vbNullString Then Return False
        Data.CreateAccount(Me.Base)
        Return True
    End Function

    Public Function Save() As Boolean
        If Me.Email = vbNullString Then Return False
        Data.Save(Me.Base)
        Return True
    End Function

    Public Function NewCharacter() As Boolean
        If Me.Email = vbNullString Or Me.Player.Name = vbNullString Then Return False
        Data.CreateCharacter(Me.Base)
        Return True
    End Function

    ' Saved variables
    Public Property Email() As String
        Get
            Return Me.Base.Email
        End Get
        Set(ByVal value As String)
            If Not IsNothing(Me) Then
                Me.Base.Email = value
            End If
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Me.Base.Password
        End Get
        Set(ByVal value As String)
            If Not IsNothing(Me) Then
                Me.Base.Password = value
            End If
        End Set
    End Property

    Public Property Player() As PlayerBase
        Get
            Return Me.Base.Player
        End Get
        Set(ByVal value As PlayerBase)
            If Not IsNothing(Me) Then
                Me.Base.Player = value
            End If
        End Set
    End Property

    Public Function GetOpenInventory() As Integer
        Dim count As Integer = 0
        For Each inv In Me.Base.Player.Inventory
            If inv < 0 Then Return count Else count = count + 1
        Next
        Return Me.Base.Player.Inventory.Length
    End Function

    Public Function GetInventoryItem(ByVal slot As Integer) As Items
        If Me.Base.Player.Inventory.Length <= slot Then Return Nothing
        If Not IsNothing(Me.Base.Player.Inventory(slot)) And Not (Me.Base.Player.Inventory(slot) < 0) Then
            If Not IsNothing(Item(Me.Base.Player.Inventory(slot))) Then Return Item(Me.Base.Player.Inventory(slot))
        End If
        Return Nothing
    End Function

    Public Sub RemoveInventoryItem(ByVal slot As Integer)
        RemoveAt(Me.Base.Player.Inventory, slot)
    End Sub
End Class
