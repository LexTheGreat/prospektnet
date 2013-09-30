Imports Winsock_Orcas
Public Class Server
    Shared WithEvents sckListen As Winsock
    Shared Sub Main()
        Dim i As Integer, time1 As Integer, time2 As Integer
        time1 = System.Environment.TickCount
        PlayerHighIndex = 1
        Console.Title = "Loading..."
        Console.WriteLine("Loading options...")
        ServerConfig = New Configuration
        ServerConfig.Load()
        Console.WriteLine("Loading accounts...")
        AccountData.LoadAccounts()
        Console.WriteLine("Loading networking...")
        sckListen = New Winsock
        sckListen.BufferSize = 8192
        sckListen.LegacySupport = False
        sckListen.LocalPort = ServerConfig.Port
        sckListen.MaxPendingConnections = 1
        sckListen.Protocol = WinsockProtocol.Tcp
        sckListen.RemoteHost = "localhost"
        sckListen.RemotePort = ServerConfig.Port
        Console.WriteLine("Initializing script engine...")
        LuaScript = New LuaHandler
        LuaScript.Initialize()
        Console.WriteLine("Initializing player array...")
        For i = 1 To 100
            Networking.Clients(i) = New ClientSocket
            Networking.Clients(i).Socket = New Winsock
        Next
        Console.WriteLine("Starting listener...")
        sckListen.Listen()
        Console.Title = "Prospekt Server <IP " & Networking.GetPublicIP() & " Port " & sckListen.LocalPort & ">"
        time2 = System.Environment.TickCount
        Console.WriteLine("Initialization complete. Server loaded in " & time2 - time1 & "ms.")
        LuaScript.ExecuteScript("OnStartup")
        inServer = True
        ServerLoop()
    End Sub

    Shared Sub sckListen_ConnectionRequest(ByVal sender As Object, ByVal e As WinsockConnectionRequestEventArgs) Handles sckListen.ConnectionRequest
        Dim i As Long
        PlayerHighIndex = PlayerHighIndex + 1
        i = Networking.FindOpenPlayerSlot()

        If i <> 0 Then
            ' we can connect them
            Networking.Clients(i).Socket.Accept(e.Client)
            Networking.Clients(i).index = i
            Networking.Clients(i).IP = e.ClientIP
            Call Networking.SocketConnected(i)
        End If
    End Sub
End Class
