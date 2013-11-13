Imports SFML.Graphics
Imports System.Security.Cryptography
Imports System.Text
Imports Prospekt.Graphics
Imports Prospekt.Network
Imports IHProspekt.Core
Module General
    Public Sub Main()
        ' Initialize main timer
        MainTimer = New GameTimer
        ' Load configuration
        EditorConfig = New Configuration
        EditorConfig.Load()

        ErrHandler = New ErrorHandler
        ErrHandler.LogPath = "content/errors/"
        ErrHandler.SuppresionLevel = ErrorHandler.ErrorLevels.High
        ErrHandler.ErrCrashType = ErrorHandler.CrashType.MsgBox

        ' Initialize network
        InitializeNetwork()
        Render.Initialize()
        Verdana = New TextWriter("content/fonts/Verdana.ttf")
        ' Setup Tileset Editor
        Tilesets.Data.LoadTilesets()
        TilesetEditor = New TilesetClass
        TilesetEditor.Init()
        ' Setup Item Editor
        Items.Data.LoadItems()
        ItemEditor = New ItemClass
        ItemEditor.Init()
        ' Setup Npc Editor
        NPCs.Data.LoadNpcs()
        NpcEditor = New NpcClass
        NpcDropEditor = New NpcDropClass
        NpcEditor.Init()
        'Setup Map Editor
        Maps.Data.LoadMaps()
        MapEditor = New MapClass
        MapEditor.Init()
        ' Setup Account Editor
        Accounts.Data.LoadAccounts()
        AccountEditor = New AccountClass
        AccountEditor.Init()
        InventoryEditor = New PlayerInventoryClass
        EditorWindow.Visible = True
        inEditor = True
        EditorLoop()
    End Sub

    Public Sub EditorLoop()

        Dim FrameTime As Integer
        Dim Tick As Integer
        Dim TickFPS As Integer
        Dim FPS As Integer

        Do While inEditor = True
            Tick = MainTimer.GetTotalTimeElapsed() ' Set the inital tick
            ElapsedTime = Tick - FrameTime ' Set the time difference for time-based movement
            FrameTime = Tick
            Networking.HandleMessage()
            ' Clear Rendering
            Render.Window.Clear(New Color(Color.Magenta))
            Select Case SelectedEditor
                Case 0 ' Map Editor
                    ' Start rendering
                    Render.TileWindow.Clear(New Color(Color.Magenta))
                    MapEditor.DrawTileset()
                    MapEditor.DrawTilesetSelection()
                    MapEditor.DrawMapTiles()
                    MapEditor.DrawMapNPCs()
                    MapEditor.DrawMapSelection()
                    ' End the rendering
                    Render.TileWindow.Display()
                Case 1 ' Tile Editor
                    ' Start rendering
                    Render.TileEditWindow.Clear(New Color(Color.Magenta))
                    TilesetEditor.DrawTileset()
                    TilesetEditor.DrawTileTypes()
                    TilesetEditor.DrawTilesetSelection()
                    ' End the rendering
                    Render.TileEditWindow.Display()
            End Select

            ' End the rendering
            Render.Window.Display()

            ' Calculate fps
            If TickFPS < Tick Then
                EditorFPS = FPS
                TickFPS = Tick + 1000
                FPS = 0
            Else
                FPS = FPS + 1
            End If
            Application.DoEvents()
        Loop
    End Sub
End Module
