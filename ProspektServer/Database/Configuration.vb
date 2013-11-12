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
        Using File As New Files(pathContent & "config.xml", Me)
            newConfig = DirectCast(File.ReadXML, Configuration)
        End Using
        Me.Port = newConfig.Port
        Me.MaxPlayers = newConfig.MaxPlayers
    End Sub

    Public Sub Save()
        Using File As New Files(pathContent & "config.xml", Me)
            File.WriteXML()
        End Using
    End Sub
End Class
