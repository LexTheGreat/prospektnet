Imports IHProspekt.Core
Public Module Globals
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    ' Graphics extension
    Public Const gfxExt As String = ".png"

    ' Networking
    Public pClient As Lidgren.Network.NetClient

    ' Scenes
    Public MenuMain As MenuScene
    Public GameMain As GameScene

    ' File paths
    Public Const pathContent As String = "content/"
    Public Const pathSprites As String = pathContent & "graphics/sprites/"
    Public Const pathTilesets As String = pathContent & "graphics/tilesets/"
    Public Const pathGui As String = pathContent & "graphics/gui/"
    Public Const pathButtons As String = pathGui & "buttons/"
    Public Const pathMusic As String = pathContent & "music/"
    Public Const pathSound As String = pathContent & "sounds/"

    ' Hardcoded sound effects
    Public Const buttonClick As String = "button.ogg"

    ' Tile engine
    Public Const picX As Byte = 32
    Public Const picY As Byte = 32
    Public Map As Maps
    Public Camera As Rectangle
    Public TileView As Rectangle

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
    Public maxX As Integer
    Public maxY As Integer
    Public screenX As Integer
    Public screenY As Integer

    ' Loop control
    Public inMenu As Boolean
    Public inGame As Boolean
    Public loginSent As Boolean
    Public elapsedTime As Integer
    Public gameFPS As Integer

    ' Main menu
    Public curMenu As Byte
    Public curTextbox As Byte
    Public sEmail As String
    Public sPass As String
    Public sCharacter As String
    Public sMessage As String
    Public sHidden As String
    Public chatShowLine As String

    ' Fonts
    Public Verdana As Prospekt.Graphics.TextWriter

    ' Players
    Public MyIndex As Integer
    Public Player(100) As Players
    Public PlayerCount As Integer

    ' NPCs
    Public NPC As NPCs()
    Public NPCCount As Integer

    ' Tilesets
    Public Tileset() As Tilesets
    Public TilesetCount As Integer

    ' fader
    Public canFade As Boolean
    Public faderAlpha As Byte
    Public faderState As Byte
    Public faderSpeed As Byte

    ' chat
    Public Const maxChatLines As Byte = 15
    Public Const maxChatChars As Byte = 42
    Public chatbuffer(maxChatLines) As String
    Public chatMode As ChatModes
    Public sChat As String
    Public vChat As String
    Public inChat As Boolean

    ' Configuration
    Public ClientConfig As Configuration
End Module
