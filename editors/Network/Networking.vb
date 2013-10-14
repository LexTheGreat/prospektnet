Imports Winsock_Orcas
Public Class Networking
    Public Shared WithEvents PlayerSocket As Winsock
    Public Shared Sub DataArrival(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockDataArrivalEventArgs) Handles PlayerSocket.DataArrival
        If IsConnected() Then IncomingData(PlayerSocket.Get)
    End Sub
    Public Shared Sub Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlayerSocket.Disconnected
    End Sub
    Public Shared Sub Handle(ByRef Data() As Byte)
        Dim Buffer as New ByteBuffer
        ' Start the command
        
        Buffer.WriteBytes(Data)
        HandleData.HandleDataPackets(Buffer.ReadInteger, Buffer.ReadBytes(Buffer.Length))
        
    End Sub
    Public Shared Sub IncomingData(ByVal Data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim pLength As Long


        Buffer.WriteBytes(Data)

        If Buffer.Length >= 4 Then pLength = Buffer.ReadInteger(False)

        Do While pLength > 0 And pLength <= Buffer.Length - 4
            If pLength <= Buffer.Length - 4 Then
                Buffer.ReadInteger()
                Handle(Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If Buffer.Length >= 4 Then pLength = Buffer.ReadInteger(False)
        Loop

        ' Clear buffer

    End Sub

    Public Shared Sub Initialize()
        ' Create data to connect with the server
        PlayerSocket = New Winsock

        ' connect
        PlayerSocket.RemoteHost = EditorConfig.IP
        PlayerSocket.RemotePort = EditorConfig.Port
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
        If PlayerSocket.State = WinsockStates.Connected Then Return True
        Return False
    End Function

    Public Shared Sub SendData(ByRef Data() As Byte)
        Dim Buffer as New ByteBuffer

        If IsConnected() Then
            

            Buffer.WriteInteger((UBound(Data) - LBound(Data)) + 1)
            Buffer.WriteBytes(Data)
            PlayerSocket.Send(Buffer.ToArray())
            
        End If
    End Sub
End Class
