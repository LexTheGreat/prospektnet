Imports LuaInterface
Public Class LuaHandler
    Implements IDisposable
    Public LuaObject As New Lua

    Public Sub Initialize()
        LuaObject.RegisterFunction("ServerMessage", Me, Me.GetType().GetMethod("ServerMessage"))
    End Sub

    Public Sub ExecuteScript(ByVal Script As String)
        If Files.Exists(pathScripts & Script & ".txt") Then LuaObject.DoFile(pathScripts & Script & ".txt")
    End Sub

    Public Sub ServerMessage(ByVal Message As String)
        Console.WriteLine(Message)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        LuaObject.Dispose()
        LuaObject = Nothing
    End Sub
End Class
