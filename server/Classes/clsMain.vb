Imports Winsock_Orcas
Public Class clsMain
    Shared WithEvents sckListen As Winsock_Orcas.Winsock
    Shared Sub Main()
        Dim i As Integer, time1 As Integer, time2 As Integer
        time1 = System.Environment.TickCount
        PlayerHighIndex = 1
        Console.Title = "Prospekt Server"
        Console.WriteLine("Loading networking...")
        sckListen = New Winsock
        sckListen.BufferSize = 8192
        sckListen.LegacySupport = False
        sckListen.LocalPort = 8080
        sckListen.MaxPendingConnections = 1
        sckListen.Protocol = Winsock_Orcas.WinsockProtocol.Tcp
        sckListen.RemoteHost = "localhost"
        sckListen.RemotePort = 8080
        Console.WriteLine("Initializing player array...")
        For i = 1 To 100
            Clients(i) = New clsSocket
            Clients(i).Socket = New Winsock
        Next
        Console.WriteLine("Starting listener...")
        sckListen.Listen()
        time2 = System.Environment.TickCount
        Console.WriteLine("Initialization complete. Server loaded in " & time2 - time1 & "ms.")
        Console.ReadLine()
    End Sub

    Shared Sub sckListen_ConnectionRequest(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockConnectionRequestEventArgs) Handles sckListen.ConnectionRequest
        Dim i As Long
        PlayerHighIndex = PlayerHighIndex + 1
        i = FindOpenPlayerSlot()

        If i <> 0 Then
            ' we can connect them
            Clients(i).Socket.Accept(e.Client)
            Clients(i).index = i
            Clients(i).IP = e.ClientIP
            Call SocketConnected(i)
        End If
    End Sub
End Class
