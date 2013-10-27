Imports SFML.Graphics
Module General
    Public Sub Main()
        EditorConfig = New Configuration
        EditorConfig.Load()
        Networking.Initialize()
        Render.Initialize()
        Verdana = New TextWriter("content/fonts/Verdana.ttf")
        'Setup Map Editor
        MapData.LoadMaps()
        MapEditor = New MapClass
        MapEditor.Init()
        ' Setup Account Editor
        AccountData.LoadAccounts()
        AccountEditor = New AccountClass
        AccountEditor.Init()
        ' Setup Tileset Editor
        TilesetData.LoadTilesets()
        TilesetEditor = New TilesetClass
        TilesetEditor.Init()
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
            ' Start rendering
            Render.TileWindow.Clear(New Color(255, 255, 255))
            MapEditor.DrawTileset()
            MapEditor.DrawTilesetSelection()
            ' End the rendering
            Render.TileWindow.Display()

            ' Start rendering
            Render.TileEditWindow.Clear(New Color(255, 255, 255))
            TilesetEditor.DrawTileset()
            TilesetEditor.DrawTileTypes()
            TilesetEditor.DrawTilesetSelection()
            ' End the rendering
            Render.TileEditWindow.Display()

            ' Start rendering
            Render.Window.Clear(New Color(255, 255, 255))
            MapEditor.DrawMapTiles()
            MapEditor.DrawMapSelection()
            MapEditor.DraMapOverlay()
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

    Public Function ResizeArray(arr As Array, newSizes() As Integer) As Array
        If newSizes.Length <> arr.Rank Then
            Throw New ArgumentException()
        End If

        Dim temp As Array = Array.CreateInstance(arr.GetType().GetElementType(), newSizes)
        Dim length As Integer = If(arr.Length <= temp.Length, arr.Length, temp.Length)
        Array.ConstrainedCopy(arr, 0, temp, 0, length)
        Return temp
    End Function
End Module
