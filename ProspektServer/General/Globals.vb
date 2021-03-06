﻿Imports IHProspekt.Core
Module Globals
    ' Loop control
    Public inServer As Boolean
    Public MainTimer As GameTimer

    ' Scripting holder
    Public LuaScript As Scripting.LuaHandler

    ' Networking
    Public pServer As Lidgren.Network.NetServer
    Public ConnectedClients() As Lidgren.Network.NetConnection

    ' File paths
    Public Const pathContent As String = "content/"
    Public Const pathScripts As String = pathContent & "scripts/"
    Public Const pathAccounts As String = pathContent & "accounts/"
    Public Const pathNPCs As String = pathContent & "npcs/"
    Public Const pathMaps As String = pathContent & "maps/"
    Public Const pathItems As String = pathContent & "items/"
    Public Const pathTilesets As String = pathContent & "tilesets/"

    ' Players
    Public Account As Accounts()
    Public AccountCount As Integer = 0
    Public Player() As Players
    Public PlayerCount As Integer = 1

    ' Maps
    Public Map As Maps()
    Public MapCount As Integer = 0

    ' Tilesets
    Public Tileset() As Tilesets
    Public TilesetCount As Integer = 0

    ' NPCs
    Public NPC() As NPCs
    Public NPCCount As Integer = 0

    ' Items
    Public Item() As Items
    Public ItemCount As Integer

    ' Configuration holder
    Public ServerConfig As Configuration
End Module
