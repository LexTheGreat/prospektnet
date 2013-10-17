Imports Lidgren.Network
Public Class Server

    Public Shared Sub Main()
        Dim time1 As Integer, time2 As Integer
        time1 = System.Environment.TickCount
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
        ReDim Player(ServerConfig.MaxPlayers)
        ReDim ConnectedClients(ServerConfig.MaxPlayers)
        LuaScript.ExecuteScript("OnStartup")
        Console.Title = "Prospekt Server <IP " & Networking.GetPublicIP() & " Port " & ServerConfig.Port & ">"
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
            Networking.HandleMessage()
            'Saves players every 5 minutes
            If tmrPlayerSave < Tick Then
                Console.WriteLine("Saving Players...")
                PlayerData.SaveOnlinePlayers()
                tmrPlayerSave = System.Environment.TickCount + 300000
            End If
            'Generate Npc movement every second
            If tmrNpcMove < Tick Then
                For i = 1 To NPCCount ' Loop through Npc's
                    If Not IsNothing(NPC(i)) Then ' Make sure Npc exists
                        NPC(i).GenerateMovement()
                    End If
                Next i
                tmrNpcMove = System.Environment.TickCount + 1000
            End If
        Loop
    End Sub
End Class
