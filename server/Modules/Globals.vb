Module Globals
    ' Loop control
    Public inServer As Boolean

    Public LuaScript As LuaHandler

    ' File paths
    Public Const pathContent As String = "content/"
    Public Const pathScripts As String = pathContent & "scripts/"

    ' Players
    Public Player(100) As Players
    Public PlayerHighIndex As Integer

    ' Configuration
    Public ServerConfig As Configuration

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
