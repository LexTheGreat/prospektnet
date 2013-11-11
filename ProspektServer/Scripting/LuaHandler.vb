Imports LuaInterface
Imports System.IO
Imports IHProspekt.Database
Namespace Scripting
    Public Class LuaHandler
        Implements IDisposable
        Private LuaObject As Lua
        Private Commands As LuaCommands
        Private autorunfiles As ArrayList

        Public Sub New()
            LuaObject = New Lua
            Commands = New LuaCommands
            REM server
            addFunction("getPlayer", "getPlayer")
            addFunction("getPlayers", "getPlayers")
            addFunction("getPlayerIndex", "getPlayerIndex")
            REM player
            addFunction("isOnline", "isOnline")
            REM Util's
            addFunction("cPrint", "cPrint")
            autorun()
        End Sub

        Public Sub addFunction(ByVal luafunction As String, Optional ByVal vbfunction As String = Nothing)
            If String.IsNullOrEmpty(vbfunction) Then
                vbfunction = luafunction
            End If
            Try : LuaObject.RegisterFunction(luafunction, Commands, Commands.GetType().GetMethod(vbfunction)) : Catch ex As LuaException : Server.Writeline(ex, ConsoleColor.Red) : End Try
            Server.Writeline("Added LuaFunction |" & luafunction + "| to .NET function |" + vbfunction + "|", ConsoleColor.Green)
        End Sub

        Public Sub executeFunction(ByVal luafunction As String, ByVal ParamArray arg() As Object)
            If arg.Length >= 1 Then
                Try : LuaObject.GetFunction(luafunction).Call(arg) : Catch ex As LuaException : Server.Writeline(ex, ConsoleColor.Red) : Catch ex As Exception : End Try
            Else
                Try : LuaObject.GetFunction(luafunction).Call() : Catch ex As LuaException : Server.Writeline(ex, ConsoleColor.Red) : Catch ex As Exception : End Try
            End If
        End Sub

        Public Sub autorun()
            Dim fileEntries As String() = Directory.GetFiles(pathScripts & "autorun", "*.lua", SearchOption.AllDirectories)
            For Each luafile As String In fileEntries
                ExecuteFile(luafile.Replace(pathScripts, Nothing).Replace("\", "/"))
            Next
        End Sub

        Public Sub ExecuteFile(ByVal File As String)
            If Exists(pathScripts & File) Then : Try : LuaObject.DoFile(pathScripts & File) : Catch ex As LuaInterface.LuaException : Server.Writeline(ex, ConsoleColor.Red) : End Try : Else : Server.Writeline(File & " Not found") : End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            LuaObject.Dispose()
            LuaObject = Nothing
        End Sub
    End Class
End Namespace