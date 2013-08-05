Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Module modDirectX
    ' DirectX8 device
    Public DirectDevice As Device

    ' DirectX8 window
    Public DirectWindow As PresentParameters

    ' Global texture
    Public Texture() As TextureRec

    ' Textures
    Public texTileset() As Integer
    Public texSprite() As Integer
    Public texButton() As Integer
    Public texGui() As Integer

    ' Texture counts
    Private countTileset As Integer
    Private countSprite As Integer
    Private countButton As Integer
    Private countGui As Integer

    ' Number of graphic files
    Public numTextures As Integer

    ' Last texture loaded
    Public CurrentTexture As Integer

    ' Global Texture
    Public Structure TextureRec
        Dim Tex As Texture
        Dim Width As Integer
        Dim Height As Integer
        Dim FilePath As String
    End Structure

    ' ********************
    ' ** Initialization **
    ' ********************
    Public Sub InitDirectDraw()
        ' Defines the window settings
        Call InitDirectWindow()

        ' Test for set the DirectX8 device
        Call TryCreateDevice()

        ' Initialise texture effe
        InitD3DEffects()

        ' Initialise the textures
        InitTextures()

        ' Create gamefont
        Verdana8 = New TextWriter(DirectDevice, New Drawing.Font("Verdana", 8))
        Verdana20 = New TextWriter(DirectDevice, New Drawing.Font("Verdana", 20))
    End Sub

    Public Sub InitD3DEffects()
        ' Now to tell directx which effects
        With DirectDevice
            .VertexFormat = VertexFormats.Transformed Or VertexFormats.Diffuse Or VertexFormats.Texture1

            ' Disable lighting
            .RenderState.Lighting = False

            ' Alpha blender effects
            .RenderState.SourceBlend = Blend.SourceAlpha
            .RenderState.DestinationBlend = Blend.InvSourceAlpha
            .RenderState.AlphaBlendEnable = True
            .RenderState.ZBufferEnable = False
            .RenderState.ZBufferWriteEnable = False

            ' Drawing effects
            .RenderState.FillMode = FillMode.Solid
            .RenderState.CullMode = Cull.None

            ' Texture effects
            .SetTextureStageState(0, TextureStageStates.AlphaOperation, TextureOperation.Modulate)
        End With
    End Sub

    Public Sub InitDirectWindow()
        DirectWindow = New PresentParameters

        With DirectWindow
            ' Back buffer
            .BackBufferCount = 1
            .BackBufferFormat = Format.X8R8G8B8
            .BackBufferWidth = ScreenWidth
            .BackBufferHeight = ScreenHeight

            ' Efects
            .SwapEffect = SwapEffect.Copy

            ' The window
            .DeviceWindow = frmMain
            .Windowed = True
        End With
    End Sub

    Public Sub TryCreateDevice()
        ' Test for set the DirectX8 device
        If Not CreateDirectDevice(CreateFlags.MixedVertexProcessing) Then
            If Not CreateDirectDevice(CreateFlags.HardwareVertexProcessing) Then
                If Not CreateDirectDevice(CreateFlags.SoftwareVertexProcessing) Then
                    If Not CreateDirectDevice(CreateFlags.PureDevice) Then
                        If Not CreateDirectDevice(CreateFlags.FpuPreserve) Then
                            MsgBox("Error initializing DirectX8.")
                            DestroyDirectDraw()
                            End
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Function CreateDirectDevice(ByVal Flag As CreateFlags) As Boolean
        ' If have error exit function
        On Error GoTo errorhandler

        ' Create DirectX8 device
        DirectDevice = New Device(0, DeviceType.Hardware, DirectWindow.DeviceWindow, Flag, DirectWindow)

        ' Return function value
        CreateDirectDevice = True

errorhandler:
        Exit Function
    End Function

    Private Sub InitTextures()
        ' Buttons
        countButton = 1
        Do While fileExist(Application.StartupPath & pathButtons & countButton & gfxExt)
            ReDim Preserve texButton(0 To countButton)
            texButton(countButton) = CacheTexture(Application.StartupPath & pathButtons & countButton & gfxExt)
            countButton = countButton + 1
        Loop
        countButton = countButton - 1

        ' guis
        countGui = 1
        Do While fileExist(Application.StartupPath & pathGui & countGui & gfxExt)
            ReDim Preserve texGui(0 To countGui)
            texGui(countGui) = CacheTexture(Application.StartupPath & pathGui & countGui & gfxExt)
            countGui = countGui + 1
        Loop
        countGui = countGui - 1

        ' sprites
        countSprite = 1
        Do While fileExist(Application.StartupPath & pathSprites & countSprite & gfxExt)
            ReDim Preserve texSprite(0 To countSprite)
            texSprite(countSprite) = CacheTexture(Application.StartupPath & pathSprites & countSprite & gfxExt)
            countSprite = countSprite + 1
        Loop
        countSprite = countSprite - 1

        ' tilesets
        countTileset = 1
        Do While fileExist(Application.StartupPath & pathTilesets & countTileset & gfxExt)
            ReDim Preserve texTileset(0 To countTileset)
            texTileset(countTileset) = CacheTexture(Application.StartupPath & pathTilesets & countTileset & gfxExt)
            countTileset = countTileset + 1
        Loop
        countTileset = countTileset - 1
    End Sub

    ' Initializing a texture
    Public Function cacheTexture(filePath As String) As Long

        ' Set the max textures
        numTextures = numTextures + 1
        ReDim Preserve Texture(numTextures)

        ' Set the texture path
        Texture(numTextures).FilePath = filePath

        ' Load texture
        LoadTexture(numTextures)

        ' Return function value
        cacheTexture = numTextures
    End Function

    Public Sub SetTexture(ByVal TextureNum As Long)
        ' Prevent subscript out of range
        If TextureNum > UBound(Texture) Or TextureNum < 0 Then Exit Sub

        ' Set texture
        If TextureNum <> CurrentTexture Then
            Call DirectDevice.SetTexture(0, Texture(TextureNum).Tex)
            CurrentTexture = TextureNum
        End If
    End Sub

    Public Sub LoadTexture(ByVal TextureNum As Long)
        Dim Tex_Info As Bitmap = New Bitmap(Texture(TextureNum).FilePath)

        ' Create texture
        Texture(TextureNum).Tex = TextureLoader.FromFile(DirectDevice, Texture(TextureNum).FilePath, Tex_Info.Width, Tex_Info.Height, -1, Usage.None, Format.Unknown, Pool.Managed, Filter.Point, Filter.None, -65281)

        ' Set texture size
        Texture(TextureNum).Height = Tex_Info.Height
        Texture(TextureNum).Width = Tex_Info.Width
    End Sub

    Public Sub DestroyDirectDraw()
        ' Unload textures
        UnloadTextures()

        ' Unload DirectX8 object
        If Not DirectDevice Is Nothing Then DirectDevice = Nothing
    End Sub

    Public Sub UnloadTextures()
        Dim i As Long

        ' Reload the textures
        If NumTextures > 0 Then
            For i = 1 To NumTextures
                If Not Texture(i).Tex Is Nothing Then Texture(i).Tex = Nothing
            Next
        End If
    End Sub

    ' **************
    ' ** Blitting **
    ' **************

    Public Sub renderTexture(ByVal TextureNum As Long, ByVal destX As Long, ByVal destY As Long, ByVal srcX As Long, ByVal srcY As Long, ByVal destWidth As Long, ByVal destHeight As Long, ByVal srcWidth As Long, ByVal srcHeight As Long, Optional ByVal Color As Integer = -1)
        Dim Vertices() As CustomVertex.TransformedColoredTextured
        Dim TextureWidth As Integer, TextureHeight As Integer

        ' Prevent subscript out range
        If TextureNum <= 0 Then Exit Sub

        ' load the texture
        Call SetTexture(TextureNum)

        ' texture sizes
        TextureWidth = Texture(TextureNum).Width
        TextureHeight = Texture(TextureNum).Height

        ' exit out if we need to
        If TextureWidth <= 0 Or TextureHeight <= 0 Then Exit Sub

        ' Create vertex
        ReDim Vertices(3)
        Vertices(0) = New CustomVertex.TransformedColoredTextured(destX, destY, 0, 1, Color, (srcX / TextureWidth), (srcY / TextureHeight))
        Vertices(1) = New CustomVertex.TransformedColoredTextured(destX + destWidth, Vertices(0).Y, Vertices(0).Z, Vertices(0).Rhw, Vertices(0).Color, (srcX + srcWidth) / TextureWidth, Vertices(0).Tv)
        Vertices(2) = New CustomVertex.TransformedColoredTextured(Vertices(0).X, destY + destHeight, Vertices(0).Z, Vertices(0).Rhw, Vertices(0).Color, Vertices(0).Tu, (srcY + srcHeight) / TextureHeight)
        Vertices(3) = New CustomVertex.TransformedColoredTextured(Vertices(1).X, Vertices(2).Y, Vertices(0).Z, Vertices(0).Rhw, Vertices(0).Color, Vertices(1).Tu, Vertices(2).Tv)


        ' Render the texture
        Call DirectDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, Vertices)
    End Sub

    Public Sub DeviceLost()
        'Do a loop while device is lost
        Do While DeviceLostException.IsExceptionIgnored
            Exit Sub
        Loop

        UnloadTextures()

        'Reset the device
        DirectDevice.Reset(DirectWindow)

        InitDirectDraw()
    End Sub
End Module
