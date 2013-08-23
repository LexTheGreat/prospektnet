Public Class ClientSocket
    Public index As Long
    Public IP As String
    Public WithEvents Socket As Winsock_Orcas.Winsock

    Private Sub DataArrival(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockDataArrivalEventArgs) Handles Socket.DataArrival
        If Networking.IsConnected(index) Then
            Call Networking.IncomingData(index, Socket.Get)
        End If
    End Sub

    Private Sub Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles Socket.Disconnected
        Networking.CloseSocket(index)
    End Sub
End Class
