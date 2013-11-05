Imports Lidgren.Network
Public Class Server
    Public Shared Sub Main()
        Dim time1 As Integer, time2 As Integer
        time1 = System.Environment.TickCount
        Console.Title = "Loading..."
        Console.WriteLine("Loading configuration...")
        ServerConfig = New Configuration
        ServerConfig.Load()
        Console.WriteLine("Loading accounts...")
        Accounts.Data.LoadAccounts()
        Console.WriteLine("Loading players...")
        ReDim Player(ServerConfig.MaxPlayers)
        ReDim ConnectedClients(ServerConfig.MaxPlayers)
        Console.WriteLine("Loading npcs...")
        NPCs.Data.LoadNPCs()
        Console.WriteLine("Loading maps...")
        Maps.Data.LoadAll()
        Console.WriteLine("Loading tilesets...")
        Tilesets.Data.LoadAll()
        Console.WriteLine("Loading networking...")
        Networking.Initialize()
        Console.WriteLine("Initializing script engine...")
        LuaScript = New Scripting.LuaHandler
        LuaScript.ExecuteFile("server.lua")
        Console.Title = "Prospekt Server <IP " & Networking.GetPublicIP() & " Port " & ServerConfig.Port & ">"
        time2 = System.Environment.TickCount
        ServerLogic.WriteLine("Initialization complete. Server loaded in " & time2 - time1 & "ms.", ConsoleColor.Green)
        inServer = True
        ServerLoop()
    End Sub

    Public Shared Sub ServerLoop()
        Dim Tick As Integer
        Dim tmrPlayerSave As Integer = System.Environment.TickCount + 300000
        Dim tmrNpcMove As Integer
        Dim i As Integer
        Do While inServer
            Tick = System.Environment.TickCount()
            Networking.HandleMessage()
            'Saves players every 5 minutes
            If tmrPlayerSave < Tick Then
                ServerLogic.WriteLine("Saving Players...", ConsoleColor.Green)
                Players.Data.SaveOnlinePlayers()
                tmrPlayerSave = System.Environment.TickCount + 300000
            End If
            'Generate Npc movement every second
            If tmrNpcMove < Tick Then
                For i = 1 To NPCCount ' Loop through Npc's
                    If Not IsNothing(NPC(i)) Then ' Make sure Npc exists
                        NPC(i).GenerateMovement()
                    End If
                Next i
                LuaScript.executeFunction("onTick")
                tmrNpcMove = System.Environment.TickCount + 1000
            End If
        Loop
    End Sub
End Class
