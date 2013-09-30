Imports Winsock_Orcas
Public Class Networking
    Public Shared WithEvents PlayerSocket As Winsock
    Public Shared Sub DataArrival(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockDataArrivalEventArgs) Handles PlayerSocket.DataArrival
        If IsConnected() Then IncomingData(PlayerSocket.Get)
    End Sub
    Public Shared Sub Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlayerSocket.Disconnected
        If inGame Then GameWindow.Close()
    End Sub
    Public Shared Sub HandleData(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        ' Start the command
        Buffer = New ByteBuffer
        buffer.WriteBytes(Data)
        HandleDataPackets(Buffer.ReadLong, Buffer.ReadBytes(Buffer.Length))
        Buffer = Nothing
    End Sub
    Public Shared Sub IncomingData(ByVal Data() As Byte)
        Dim Buffer As ByteBuffer
        Dim pLength As Long
        Buffer = New ByteBuffer

        Buffer.WriteBytes(Data)

        If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)

        Do While pLength > 0 And pLength <= Buffer.Length - 8
            If pLength <= Buffer.Length - 8 Then
                Buffer.ReadLong()
                HandleData(Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)
        Loop

        ' Clear buffer
        Buffer = Nothing
    End Sub

    Public Shared Sub Initialize()
        ' Create data to connect with the server
        PlayerSocket = New Winsock

        ' connect
        PlayerSocket.RemoteHost = ClientConfig.IP
        PlayerSocket.RemotePort = ClientConfig.Port
        PlayerSocket.Connect()
    End Sub

    Public Shared Sub Dispose()
        PlayerSocket.Close()
        PlayerSocket = Nothing
    End Sub

    Public Shared Function ConnectToServer() As Boolean
        Dim Wait As Long

        ' Check to see if we are already connected, if so just exit
        If IsConnected() Then
            ConnectToServer = True
            Exit Function
        End If

        ' Try connect with the server
        Wait = System.Environment.TickCount
        PlayerSocket.Close()
        PlayerSocket.Connect()

        ' Wait until connected or 3 seconds have passed and report the server being down
        Do While (Not IsConnected()) And (System.Environment.TickCount <= Wait + 1000)
            Application.DoEvents()
        Loop

        ' Return function value
        ConnectToServer = IsConnected()
    End Function

    Public Shared Function IsConnected() As Boolean
        If PlayerSocket.State = WinsockStates.Connected Then
            IsConnected = True
        End If
    End Function

    Public Shared Sub SendData(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer

        If IsConnected() Then
            Buffer = New ByteBuffer

            Buffer.WriteLong((UBound(Data) - LBound(Data)) + 1)
            Buffer.WriteBytes(Data)
            PlayerSocket.Send(Buffer.ToArray())
            Buffer = Nothing
        End If
    End Sub
End Class
