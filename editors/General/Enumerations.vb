﻿Module Enumerations
    ' Packets sent by server to client
    Public Enum ServerPackets
        Alert = 1
        RegisterOk
        LoginOk
        Player
        ClearPlayer
        Position
        Access
        Visible
        Message
        NPC
        NPCPosition
        MapData
        ' Make sure SMSG_COUNT is below everything else
        COUNT
    End Enum

    ' Packets sent by client to server
    Public Enum ClientPackets
        Register = 1
        NewCharacter
        Login
        Message
        Position
        SetAccess
        SetVisible
        WarpTo
        WarpToMe
        ' Make sure CMSG_COUNT is below everything else
        COUNT
    End Enum

    'Packets sent by editor to server
    Public Enum CEditorPackets
        Login = ClientPackets.COUNT + 1
        DataRequest
        Data
        ' Make sure MSG_COUNT is below everything else
        COUNT
    End Enum

    'Packets sent by server to editor
    Public Enum SEditorPackets
        LoginOk = ServerPackets.COUNT + 1
        Data
        DataSent
        ' Make sure MSG_COUNT is below everything else
        COUNT
    End Enum

    Public Enum DirEnum
        Up = 0
        Down
        Left
        Right
        ' Make sure COUNT is below everything else
        COUNT
    End Enum

    Public Enum MenuEnum
        Main = 0
        Login
        Register
        Creation
        Credits
        StatusMessage
        ' Make sure COUNT is below everything else
        COUNT
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
        Guild
        Party
        GM
        ' Make sure COUNT is below everything else
        COUNT
    End Enum

    ' Layers
    Public Enum MapLayerEnum
        Ground = 0
        GroundMask
        Fringe
        FringeMask
        ' Make sure COUNT is below everything else
        COUNT
    End Enum
End Module