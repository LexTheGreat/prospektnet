Imports Prospekt.Network
Namespace Scripting
    Public Class LuaCommands
        Public Function getPlayerIndex(ByVal name As String) As Integer
            For index As Integer = 1 To PlayerCount
                If Player(index).Name = name Then Return index
            Next
            Return 0 ' <- Server idk about this...
        End Function
        Public Function getPlayer(ByVal index As Integer) As Players
            If isOnline(index) Then
                Return Player(index)
            End If
            Return Player(0)
        End Function
        Public Function getPlayers() As Integer
            Return PlayerCount - 1
        End Function
        Public Function isOnline(ByVal index As Integer) As Boolean
            Return IsConnected(index)
        End Function
        ' Functions
        Public Sub cPrint(ByVal Message As Object, Optional ByVal color As Object = ConsoleColor.Gray)
            ServerLogic.WriteLine(Message, color)
        End Sub
    End Class
End Namespace