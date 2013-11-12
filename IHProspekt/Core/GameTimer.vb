Imports System
Namespace Core
    Public Class GameTimer
        Implements IDisposable
        Private StopWatch As Stopwatch
        Public Sub New()
            StopWatch = New Stopwatch
            StopWatch.Start()
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            StopWatch.Stop()
            StopWatch = Nothing
        End Sub
        Public ReadOnly Property GetTotalTimeElapsed As Long
            Get
                Return StopWatch.ElapsedMilliseconds
            End Get
        End Property
    End Class
End Namespace