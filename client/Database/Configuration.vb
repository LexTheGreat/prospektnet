Imports System.IO
Imports System.Xml.Serialization
Public Class Configuration
    Public ScreenWidth As Integer
    Public ScreenHeight As Integer
    Public MenuMusic As String
    Public GameMusic As String
    Public Music As Boolean
    Public Sound As Boolean
    Public IP As String
    Public Port As Integer

    Public Sub New()
        Me.ScreenWidth = 800
        Me.ScreenHeight = 600
        Me.MenuMusic = "tranquility.ogg"
        Me.GameMusic = "touchthesky.ogg"
        Me.Music = True
        Me.Sound = True
        Me.IP = "localhost"
        Me.Port = 8080
    End Sub

    Public Sub Load()
        Dim newConfig As New Configuration

        ' Get object from file
        newConfig = DirectCast(Files.ReadXML(pathContent & "config.xml", Me), Configuration)
        Me.ScreenWidth = newConfig.ScreenWidth
        Me.ScreenHeight = newConfig.ScreenHeight
        Me.MenuMusic = newConfig.MenuMusic
        Me.GameMusic = newConfig.GameMusic
        Me.Music = newConfig.Music
        Me.Sound = newConfig.Sound
        Me.IP = newConfig.IP
        Me.Port = newConfig.Port
    End Sub

    Public Sub Save()
        Files.WriteXML(pathContent & "config.xml", Me)
    End Sub
End Class