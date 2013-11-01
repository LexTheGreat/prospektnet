Imports LuaInterface
Imports System.IO

Public Class LuaHandler
    Implements IDisposable
    Public LuaObject As Lua
    Public Commands As LuaCommands
    Public autorunfiles As ArrayList

    Public Sub New()
        LuaObject = New Lua
        Commands = New LuaCommands
        addFunction("getPlayers")
        addFunction("cPrint")
        autorun()
    End Sub

    Public Sub addFunction(ByVal luafunction As String, Optional ByVal vbfunction As String = Nothing)
        If String.IsNullOrEmpty(vbfunction) Then
            vbfunction = luafunction
        End If
        LuaObject.RegisterFunction(luafunction, Commands, Commands.GetType().GetMethod(vbfunction))
        Commands.cPrint("Added LuaFunction |" & luafunction + "| to .NET function |" + vbfunction + "|", ConsoleColor.Green)
    End Sub

    Public Sub removeFunction(ByVal luafunction As String)
        If LuaObject.GetFunction(luafunction).Equals(Nothing) Then
            Console.WriteLine(luafunction + " could not be found.")
            Return
        End If
        LuaObject.GetFunction(luafunction).Dispose()
    End Sub

    Public Sub autorun()
        Dim fileEntries As String() = Directory.GetFiles(pathScripts & "autorun", "*.lua", SearchOption.AllDirectories)
        For Each luafile As String In fileEntries
            ExecuteFile(luafile.Replace(pathScripts, Nothing).Replace("\", "/"))
        Next
    End Sub

    REM Public Sub ExecuteScript(ByVal Script As String)
    REM    If Files.Exists(pathScripts & Script & ".lua") Then LuaObject.DoFile(pathScripts & Script & ".lua")
    REM End Sub
    REM Better way to run with functions ~ Created lua error handler
    Public Sub ExecuteFile(ByVal File As String)
        If Files.Exists(pathScripts & File) Then : Try : LuaObject.DoFile(pathScripts & File) : Catch ex As LuaInterface.LuaException : Console.WriteLine(ex) : End Try : Else : Console.WriteLine(File & " Not found") : End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        LuaObject.Dispose()
        LuaObject = Nothing
    End Sub
End Class