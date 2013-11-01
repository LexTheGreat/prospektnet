Public Class LuaCommands
    REM !!! Defualt lua command apply use print("message here") !!!
    REM Public Sub ServerMessage(ByVal Message As String)
    REM    Console.WriteLine(Message)
    REM End Sub
    Public Function getPlayers()
        REM Server counts as a player? - 1 him ;/
        Return PlayerCount - 1
    End Function
    Public Sub cPrint(ByVal Message As String, Optional ByVal color As Integer = 15)
        If color <= 15 Then
            Console.ForegroundColor = color
            Console.WriteLine(Message)
            Console.ResetColor()
            Return
        End If
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("cPrint usage error (color must be <= 15)")
        Console.ResetColor()
    End Sub
End Class
