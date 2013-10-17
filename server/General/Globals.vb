Module Globals
    ' Loop control
    Public inServer As Boolean

    ' Scripting holder
    Public LuaScript As LuaHandler

    ' Networking
    Public pServer As Lidgren.Network.NetServer
    Public ConnectedClients() As Lidgren.Network.NetConnection

    ' File paths
    Public Const pathContent As String = "content/"
    Public Const pathScripts As String = pathContent & "scripts/"
    Public Const pathAccounts As String = pathContent & "accounts/"
    Public Const pathNPCs As String = pathContent & "npcs/"
    Public Const pathMaps As String = pathContent & "maps/"

    ' Players
    Public Account As Accounts()
    Public AccountCount As Integer = 0
    Public Player() As Players
    Public PlayerCount As Integer = 1

    ' Maps
    Public Map As MapStructure()

    ' NPCs
    Public NPC() As NPCs
    Public NPCCount As Integer = 0

    ' Configuration holder
    Public ServerConfig As Configuration
End Module
