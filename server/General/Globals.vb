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

    ' Players
    Public Account As Accounts()
    Public AccountCount As Long = 0
    Public Player(100) As Players
    Public PlayerHighIndex As Integer

    ' NPCs
    Public NPC As NPCs()
    Public NPCCount As Long = 0

    ' Configuration holder
    Public ServerConfig As Configuration
End Module
