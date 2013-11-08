Imports SFML.Graphics
Imports System.Security.Cryptography
Imports System.Text
Imports Prospekt.Graphics
Imports Prospekt.Network
Module General
    Public Sub Main()
        EditorConfig = New Configuration
        EditorConfig.Load()
        InitializeNetwork()
        Render.Initialize()
        Verdana = New TextWriter("content/fonts/Verdana.ttf")
        ' Setup Tileset Editor
        Tilesets.Data.LoadTilesets()
        TilesetEditor = New TilesetClass
        TilesetEditor.Init()
        'Setup Map NPC Editor
        NPCs.Data.LoadNpcs()
        MapNPCEditor = New MapNPCClass
        MapNPCEditor.Init()
        'Setup Map Editor
        Maps.Data.LoadMaps()
        MapEditor = New MapClass
        MapEditor.Init()
        ' Setup Account Editor
        Accounts.Data.LoadAccounts()
        AccountEditor = New AccountClass
        AccountEditor.Init()
        ' Setup Npc Editor
        NPCs.Data.LoadNpcs()
        NpcEditor = New NpcClass
        NpcEditor.Init()
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
            Tick = System.Environment.TickCount() ' Set the inital tick
            ElapsedTime = Tick - FrameTime ' Set the time difference for time-based movement
            FrameTime = Tick
            Networking.HandleMessage()
            If MapNPCs.Visible Then
                Render.MapNPCWindow.Clear(New Color(255, 255, 255))
                MapNPCEditor.DrawNPC()
                Render.MapNPCWindow.Display()
            End If
            Select Case SelectedEditor
                Case 0 ' Map Editor
                    ' Start rendering
                    Render.TileWindow.Clear(New Color(255, 255, 255))
                    MapEditor.DrawTileset()
                    MapEditor.DrawTilesetSelection()
                    MapEditor.DrawMapTiles()
                    MapEditor.DrawMapSelection()
                    MapEditor.DraMapOverlay()
                    ' End the rendering
                    Render.TileWindow.Display()
                Case 1 ' Tile Editor
                    ' Start rendering
                    Render.TileEditWindow.Clear(New Color(255, 255, 255))
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
