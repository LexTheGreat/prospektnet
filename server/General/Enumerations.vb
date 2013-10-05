Module Enumerations
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
        SNPCPosition
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

    ' Directions
    Public Enum DirEnum
        Up = 0
        Down
        Left
        Right
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
