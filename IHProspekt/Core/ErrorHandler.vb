Imports System
Imports System.IO
Namespace Core
    Public Class ErrorHandler
        Implements IDisposable
        Private mPath As String
        Private mSupressionLevel As ErrorLevels
        Private mCrashType As CrashType
        Private mDestroy As Object
        Public Enum ErrorLevels As Byte
            REM Signifies an error that will not jeopardize system stability.
            Low
            REM Signifies an error that should not jeopardize system stability.
            Medium
            REM Signifies an error that will (most likely) jeopardize system stability.
            High
        End Enum
        Public Enum CrashType As Byte
            REM Outputs crash message to MsgBox
            MsgBox
            REM Outputs crash message to console
            Console
            REM Signifies an error that will (most likely) jeopardize system stability.
        End Enum
        Public WriteOnly Property DestroyTarget() As Object
            Set(value As Object)
                Me.mDestroy = value
            End Set
        End Property
        Public WriteOnly Property LogPath() As String
            Set(value As String)
                Me.mPath = value
            End Set
        End Property
        Public WriteOnly Property SuppresionLevel() As ErrorLevels
            Set(value As ErrorLevels)
                Me.mSupressionLevel = value
            End Set
        End Property
        Public WriteOnly Property ErrCrashType() As CrashType
            Set(value As CrashType)
                Me.mCrashType = value
            End Set
        End Property
        Public Sub HandleException(ex As Exception, errorLevel As ErrorLevels)
            REM Invoke the method that will log our error in a log-file.
            LogError(ex, errorLevel)
            REM If the error that has occured has an error level higher than what we're supressing,
            REM Notify the end user that the error is unrecoverable and shut everything down.
            If errorLevel >= Me.mSupressionLevel Then
                Select Case Me.mCrashType
                    Case CrashType.Console
                        Console.WriteLine("An unrecoverable error has occured; please check the error log files for additional details.")
                    Case CrashType.MsgBox
                        MsgBox("An unrecoverable error has occured; please check the error log files for additional details.")
                End Select
                'mDestroy().Dispose()
            End If
        End Sub
        Private Sub LogError(ex As Exception, errorLevel As ErrorLevels)
            REM Append to the error-log file using StreamWriter.
            If Not Directory.Exists(mPath) Then Directory.CreateDirectory(mPath)
            File.AppendAllText(mPath & "Errors.log", "[" & DateTime.Now.ToString("M/d/yyyy") & "] - Error Level: " & errorLevel.ToString() & ", Error Message: " & ex.Message & ex.StackTrace & vbNewLine)
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.mPath = vbNullString
            Me.mSupressionLevel = vbNull
            Me.mCrashType = vbNull
            Me.mDestroy = Nothing
        End Sub
    End Class
End Namespace