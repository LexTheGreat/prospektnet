Module modConstGlobals
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    ' Graphics extension
    Public Const gfxExt As String = ".png"

    ' File paths
    Public Const pathContent As String = "content/"
    Public Const pathSprites As String = pathContent & "graphics/sprites/"
    Public Const pathTilesets As String = pathContent & "graphics/tilesets/"
    Public Const pathGui As String = pathContent & "graphics/gui/"
    Public Const pathButtons As String = pathGui & "buttons/"
    Public Const pathMusic As String = pathContent & "music/"
    Public Const pathSound As String = pathContent & "sounds/"

    ' Tile engine
    Public Const picX As Byte = 32
    Public Const picY As Byte = 32

    ' Input
    Public mouseX As Integer
    Public mouseY As Integer
    Public mouseLeftDown As Integer
    Public mouseRightDown As Integer
    Public dirUp As Boolean
    Public dirDown As Boolean
    Public dirLeft As Boolean
    Public dirRight As Boolean

    ' Screen dimensions
    Public screenWidth As Integer
    Public screenHeight As Integer
    Public maxX As Integer
    Public maxY As Integer

    ' Loop control
    Public inMenu As Boolean
    Public inGame As Boolean
    Public elapsedTime As Integer
    Public gameFPS As Integer

    ' Main menu
    Public curMenu As Byte
    Public sUser As String
    Public chatShowLine As String

    ' Fonts
    Public Verdana As TextWriter
    Public Silkscreen As TextWriter

    ' Players
    Public MyIndex As Integer
    Public Player(100) As clsPlayer
    Public PlayerHighindex As Integer

    ' fader
    Public canFade As Boolean
    Public faderAlpha As Byte
    Public faderState As Byte
    Public faderSpeed As Byte
End Module
