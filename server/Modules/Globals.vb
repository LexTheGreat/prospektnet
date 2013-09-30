Module Globals
    ' Loop control
    Public inServer As Boolean

    Public LuaScript As LuaHandler

    ' File paths
    Public Const pathContent As String = "content/"
    Public Const pathScripts As String = pathContent & "scripts/"
    Public Const pathAccounts As String = pathContent & "accounts/"
    Public Const pathNPCs As String = pathContent & "npcs/"

    ' Players
    Public Account As Accounts()
    Public AccountCount As Long = 0
    Public Player(100) As Players
    Public PlayerHighIndex As Integer

    ' NPCs
    Public NPC As NPCs()
    Public NPCCount As Long = 0

    ' Configuration
    Public ServerConfig As Configuration

    ' Packets sent by server to client
    Public Enum ServerPackets
        SAlert = 1
        SRegisterOk
        SLoginOk
        SPlayer
        SClearPlayer
        SPosition
        SAccess
        SVisible
        SMessage
        SNPC
        ' Make sure SMSG_COUNT is below everything else
        SMSG_COUNT
    End Enum

    ' Packets sent by client to server
    Public Enum ClientPackets
        CRegister = 1
        CNewCharacter
        CLogin
        CMessage
        CPosition
        CSetPosition
        CSetAccess
        CSetVisible
        CWarpTo
        CWarpToMe
        ' Make sure CMSG_COUNT is below everything else
        CMSG_COUNT
    End Enum

    ' Admin Ranks
    Public Enum ACCESS
        NONE = 0
        GMIT
        GM
        LEAD_GM
        DEV
        ADMIN
        ' Make sure COUNT is below everything else
        COUNT
    End Enum

    ' Chat Modes
    Public Enum ChatModes
        Say = 0
        Whisper
        Shout
        Party
        Guild
        GM
        ' Make sure COUNT is below everything else
        COUNT
    End Enum
End Module
