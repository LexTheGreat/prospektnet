﻿Imports Winsock_Orcas
Public Class Server
    Public Shared WithEvents sckListen As Winsock

    Public Shared Sub sckListen_ConnectionRequest(ByVal sender As Object, ByVal e As WinsockConnectionRequestEventArgs) Handles sckListen.ConnectionRequest
        Dim i As Long
        PlayerHighIndex = PlayerHighIndex + 1
        i = PlayerLogic.FindOpenPlayerSlot()

        If i <> 0 Then
            ' we can connect them
            Networking.Clients(i).Socket.Accept(e.Client)
            Networking.Clients(i).index = i
            Networking.Clients(i).IP = e.ClientIP
            Call Networking.SocketConnected(i)
        End If
    End Sub

    Public Shared Sub Main()
        Dim i As Integer, time1 As Integer, time2 As Integer
        time1 = System.Environment.TickCount
        PlayerHighIndex = 1
        Console.Title = "Loading..."
        Console.WriteLine("Loading options...")
        ServerConfig = New Configuration
        ServerConfig.Load()
        Console.WriteLine("Loading accounts...")
        AccountData.LoadAccounts()
        Console.WriteLine("Loading npcs...")
        NPCData.LoadNPCs()
        Console.WriteLine("Loading maps...")
        MapData.LoadMaps()
        Console.WriteLine("Loading networking...")
        Networking.Initialize()
        Console.WriteLine("Initializing script engine...")
        LuaScript = New LuaHandler
        Console.WriteLine("Initializing player array...")
        ReDim Player(100)
        ReDim Networking.Clients(100)
        For i = 1 To ServerConfig.MaxPlayers
            Networking.Clients(i) = New ClientSocket
            Networking.Clients(i).Socket = New Winsock
        Next
        LuaScript.ExecuteScript("OnStartup")
        Console.WriteLine("Starting listener...")
        sckListen.Listen()
        Console.Title = "Prospekt Server <IP " & Networking.GetPublicIP() & " Port " & sckListen.LocalPort & ">"
        time2 = System.Environment.TickCount
        Console.WriteLine("Initialization complete. Server loaded in " & time2 - time1 & "ms.")
        inServer = True
        ServerLoop()
    End Sub

    Public Shared Sub ServerLoop()
        Dim Tick As Integer
        Dim tmrPlayerSave As Integer
        Dim tmrNpcMove As Integer
        Dim i As Integer
        Do While inServer
            Tick = System.Environment.TickCount()
            'Saves players every 5 minutes
            If tmrPlayerSave < Tick Then
                Console.WriteLine("Saving Players...")
                PlayerData.SaveOnlinePlayers()
                tmrPlayerSave = System.Environment.TickCount + 300000
            End If
            'Generate Npc movement every second
            If tmrNpcMove < Tick Then
                For i = 0 To NPCCount ' Loop through Npc's
                    If Not IsNothing(NPC(i)) Then ' Make sure Npc exists
                        NPC(i).GenerateMovement()
                    End If
                Next i
                tmrNpcMove = System.Environment.TickCount + 1000
            End If
        Loop
    End Sub
End Class
