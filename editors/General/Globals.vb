Module Globals
    ' Graphics extension
    Public Const gfxExt As String = ".png"

    ' Configuration
    Public EditorConfig As Configuration

    ' Networking
    Public pClient As Lidgren.Network.NetClient

    ' Editors

    Public AccountEditor As AccountClass
    Public MapEditor As MapClass
    Public TilesetEditor As TilesetClass

    ' Loop Control
    Public ElapsedTime As Integer
    Public EditorFPS As Integer
    Public inEditor As Boolean

    ' Graphics & sound paths
    Public Const pathContent As String = "content/"
    Public Const pathSprites As String = pathContent & "graphics/sprites/"
    Public Const pathTilesets As String = pathContent & "graphics/tilesets/"
    Public Const pathGui As String = pathContent & "graphics/gui/"
    Public Const pathButtons As String = pathGui & "buttons/"
    Public Const pathMusic As String = pathContent & "music/"
    Public Const pathSound As String = pathContent & "sounds/"

    ' Data paths
    Public Const pathScripts As String = pathContent & "scripts/"
    Public Const pathAccounts As String = pathContent & "accounts/"
    Public Const pathNPCs As String = pathContent & "npcs/"
    Public Const pathMaps As String = pathContent & "maps/"

    ' Fonts
    Public Verdana As TextWriter

    ' Tile engine
    Public Const picX As Byte = 32
    Public Const picY As Byte = 32
    Public Map As MapStructure()
    Public MapCount As Integer
    Public Camera As Rectangle
    Public TileView As Rectangle
    Public Tileset() As Tilesets
    Public TilesetCount As Integer

    ' Accounts
    Public Account As Accounts()
    Public AccountCount As Integer
End Module
