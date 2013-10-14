Module Globals
    ' Loop control
    Public inServer As Boolean

    ' Scripting holder
    Public LuaScript As LuaHandler

    ' File paths
    Public Const pathContent As String = "content/"
    Public Const pathScripts As String = pathContent & "scripts/"
    Public Const pathAccounts As String = pathContent & "accounts/"
    Public Const pathNPCs As String = pathContent & "npcs/"
    Public Const pathMaps As String = pathContent & "maps/"

    ' Players
    Public Account As Accounts()
    Public AccountCount As Long = 0
    Public Player() As Players
    Public PlayerHighIndex As Integer

    ' Maps
    Public Map As MapStructure()

    ' NPCs
    Public NPC As NPCs()
    Public NPCCount As Long = 0

    ' Configuration holder
    Public ServerConfig As Configuration
End Module
