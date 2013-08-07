﻿Module modEnumerations
    ' Packets sent by server to client
    Public Enum ServerPackets
        SLoginOk = 1
        SPlayer
        SClearPlayer
        SPosition
        ' Make sure SMSG_COUNT is below everything else
        SMSG_COUNT
    End Enum

    ' Packets sent by client to server
    Public Enum ClientPackets
        CLogin = 1
        CPosition
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
