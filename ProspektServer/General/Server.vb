Imports Lidgren.Network
Imports Prospekt.Network
Imports IHProspekt.Core
Public Class Server
    Public Shared Sub Main()
        Dim time1 As Integer, time2 As Integer
        MainTimer = New GameTimer
        time1 = MainTimer.GetTotalTimeElapsed
        Console.Title = "Loading..."
        Server.Writeline("Loading configuration...")
        ServerConfig = New Configuration
        ServerConfig.Load()
        Server.Writeline("Loading accounts...")
        Accounts.Data.LoadAll()
        Server.Writeline("Loading players...")
        ReDim Player(ServerConfig.MaxPlayers)
        ReDim ConnectedClients(ServerConfig.MaxPlayers)
        Server.WriteLine("Loading items...")
        Items.Data.LoadAll()
        Server.Writeline("Loading npcs...")
        NPCs.Data.LoadAll()
        Server.Writeline("Loading maps...")
        Maps.Data.LoadAll()
        Server.Writeline("Loading tilesets...")
        Tilesets.Data.LoadAll()
        Server.Writeline("Initializing networking..")
        InitializeNetwork()
        Server.Writeline("Initializing script engine...")
        LuaScript = New Scripting.LuaHandler
        LuaScript.ExecuteFile("server.lua")
        Console.Title = "Prospekt Server <IP " & GetPublicIP() & " Port " & ServerConfig.Port & ">"
        time2 = MainTimer.GetTotalTimeElapsed
        Server.Writeline("Initialization complete. Server loaded in " & time2 - time1 & "ms.", ConsoleColor.Green)
        inServer = True
        ServerLoop()
    End Sub

    Public Shared Sub WriteLine(ByVal obj As Object, Optional ByVal color As Object = "Gray")
        Dim colorNames() As String = ConsoleColor.GetNames(GetType(ConsoleColor))
        For Each colorName As String In colorNames
            colorName = CType(System.Enum.Parse(GetType(ConsoleColor), colorName), ConsoleColor)
            Dim colorType
            Try : colorType = CType(System.Enum.Parse(GetType(ConsoleColor), color), ConsoleColor) : Catch ex As Exception : colorType = ConsoleColor.Gray : End Try
            If colorName = colorType Then
                Console.ForegroundColor = colorType
                Console.WriteLine(obj)
                Console.ResetColor()
                Return
            End If
        Next
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("cPrint usage error (Incorrect color)")
        Console.ResetColor()
    End Sub

    Public Shared Sub ServerLoop()
        Dim Tick As Integer
        Dim tmrPlayerSave As Integer = MainTimer.GetTotalTimeElapsed + 300000
        Dim tmr1000 As Integer
        Dim i As Integer
        Do While inServer
            Tick = MainTimer.GetTotalTimeElapsed()
            HandleMessage()
            'Saves players every 5 minutes
            If tmrPlayerSave < Tick Then
                Server.Writeline("Saving Players...", ConsoleColor.Green)
                Players.Data.SaveOnlinePlayers()
                tmrPlayerSave = MainTimer.GetTotalTimeElapsed + 300000
            End If
            If tmr1000 < Tick Then
                'Generate Npc movement every second
                For i = 1 To NPCCount ' Loop through Npc's
                    If Not IsNothing(NPC(i)) Then ' Make sure Npc exists
                        NPC(i).GenerateMovement()
                    End If
                Next i
                'Execute lua script every second
                LuaScript.executeFunction("onTick")
                tmr1000 = MainTimer.GetTotalTimeElapsed + 1000
            End If
        Loop
    End Sub
End Class
