Module modGlobals
    ' Loop control
    Public inServer As Boolean

    ' File paths
    Public Const pathContent As String = "content/"

    ' Players
    Public Player(100) As clsPlayer
    Public PlayerHighIndex As Integer

    ' Configuration
    Public ServerConfig As ConfigStruct
    Public Structure ConfigStruct
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
End Module
