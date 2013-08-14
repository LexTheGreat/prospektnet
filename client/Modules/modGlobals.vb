Module modGlobals
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
    Public maxX As Integer
    Public maxY As Integer

    ' Loop control
    Public inMenu As Boolean
    Public inGame As Boolean
    Public elapsedTime As Integer
    Public gameFPS As Integer

    ' Main menu
    Public curMenu As Byte
    Public curTextbox As Byte
    Public sUser As String
    Public sPass As String
    Public chatShowLine As String

    ' Fonts
    Public Verdana As TextWriter

    ' Players
    Public MyIndex As Integer
    Public Player(100) As clsPlayer
    Public PlayerHighindex As Integer

    ' fader
    Public canFade As Boolean
    Public faderAlpha As Byte
    Public faderState As Byte
    Public faderSpeed As Byte

    ' chat
    Public Const maxChatLines As Byte = 15
    Public chatbuffer(maxChatLines) As String
    Public sChat As String
    Public inChat As Boolean

    Public ClientConfig As ConfigStruct
    Public Structure ConfigStruct
        Dim ScreenWidth As Integer
        Dim ScreenHeight As Integer
        Dim MenuMusic As String
        Dim GameMusic As String
        Dim Music As Boolean
        Dim Sound As Boolean
        Dim IP As String
        Dim Port As Integer
    End Structure

    ' Packets sent by server to client
    Public Enum ServerPackets
        SLoginOk = 1
        SPlayer
        SClearPlayer
        SPosition
        SMessage
        ' Make sure SMSG_COUNT is below everything else
        SMSG_COUNT
    End Enum

    ' Packets sent by client to server
    Public Enum ClientPackets
        CLogin = 1
        CPosition
        CMessage
        ' Make sure CMSG_COUNT is below everything else
        CMSG_COUNT
    End Enum
    Public Enum DirEnum
        Up = 0
        Down
        Left
        Right
    End Enum

    Public Enum MenuEnum
        Main = 0
        Login
        Credits
    End Enum

    Public Structure GeomRec
        Dim Left As Integer
        Dim Top As Integer
        Dim Width As Integer
        Dim Height As Integer
    End Structure
End Module
