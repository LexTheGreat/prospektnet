Imports LuaInterface
Imports System.IO
Namespace Scripting
    Public Class LuaHandler
        Implements IDisposable
        Public LuaObject As Lua
        Public Commands As LuaCommands
        Public autorunfiles As ArrayList

        Public Sub New()
            LuaObject = New Lua
            Commands = New LuaCommands
            REM server
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
            Try : LuaObject.RegisterFunction(luafunction, Commands, Commands.GetType().GetMethod(vbfunction)) : Catch ex As LuaException : ServerLogic.WriteLine(ex, ConsoleColor.Red) : End Try
            ServerLogic.WriteLine("Added LuaFunction |" & luafunction + "| to .NET function |" + vbfunction + "|", ConsoleColor.Green)
        End Sub

        Public Sub removeFunction(ByVal luafunction As String)
            If LuaObject.GetFunction(luafunction).Equals(Nothing) Then
                ServerLogic.WriteLine(luafunction + " could not be found.", ConsoleColor.Yellow)
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

        Public Sub ExecuteFile(ByVal File As String)
            If Files.Exists(pathScripts & File) Then : Try : LuaObject.DoFile(pathScripts & File) : Catch ex As LuaInterface.LuaException : ServerLogic.WriteLine(ex, ConsoleColor.Red) : End Try : Else : Console.WriteLine(File & " Not found") : End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            LuaObject.Dispose()
            LuaObject = Nothing
        End Sub
    End Class
End Namespace