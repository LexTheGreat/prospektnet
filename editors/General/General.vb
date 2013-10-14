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
        MapEditor.Init(EditorWindow)
        ' Setup Account Editor
        AccountData.LoadAccounts()
        AccountEditor.Init(EditorWindow)
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

            ' Start rendering
            Render.TileWindow.Clear(New Color(255, 255, 255))
            MapEditor.DrawTileset()
            MapEditor.DrawSelection()
            ' End the rendering
            Render.TileWindow.Display()

            ' Start rendering
            Render.Window.Clear(New Color(255, 255, 255))
            MapEditor.DrawMap()
            MapEditor.DrawMapSelection()
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
