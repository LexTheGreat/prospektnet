Imports LuaInterface
Public Class LuaHandler
    Implements IDisposable
    Public LuaObject As Lua
    Public Commands As LuaCommands

    Public Sub New()
        LuaObject = New Lua
        Commands = New LuaCommands
        LuaObject.RegisterFunction("ServerMessage", Commands, Commands.GetType().GetMethod("ServerMessage"))
    End Sub

    Public Sub ExecuteScript(ByVal Script As String)
        If Files.Exists(pathScripts & Script & ".txt") Then LuaObject.DoFile(pathScripts & Script & ".txt")
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        LuaObject.Dispose()
        LuaObject = Nothing
    End Sub
End Class
