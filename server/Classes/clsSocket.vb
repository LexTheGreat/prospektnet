Public Class clsSocket
    Public index As Long
    Public IP As String
    Public WithEvents Socket As Winsock_Orcas.Winsock

    Private Sub DataArrival(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockDataArrivalEventArgs) Handles Socket.DataArrival
        If IsConnected(index) Then
            Call IncomingData(index, Socket.Get)
        End If
    End Sub

    Private Sub Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles Socket.Disconnected
        CloseSocket(index)
    End Sub
End Class
