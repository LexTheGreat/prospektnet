Imports System.IO
Imports System.Xml.Serialization
Imports IHProspekt.Database
Public Class Configuration
    Public Port As Integer
    Public MaxPlayers As Integer

    Public Sub New()
        Me.Port = 8080
        Me.MaxPlayers = 100
    End Sub

    Public Sub Load()
        Dim newConfig As New Configuration
        newConfig = DirectCast(ReadXML(pathContent & "config.xml", Me), Configuration)
        Me.Port = newConfig.Port
        Me.MaxPlayers = newConfig.MaxPlayers
    End Sub

    Public Sub Save()
        WriteXML(pathContent & "config.xml", Me)
    End Sub
End Class
