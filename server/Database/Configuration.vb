Imports System.IO
Imports System.Xml.Serialization
Public Class Configuration
    Public Port As Integer
    Public MaxPlayers As Integer

    Public Sub New()
        Me.Port = 8080
        Me.MaxPlayers = 100
    End Sub

    Public Sub Load()
        Dim objConfig As Object, newConfig As New Configuration

        ' Get object from file
        objConfig = Files.ReadXML(pathContent & "config.xml", Me)
        If IsNothing(objConfig) Then objConfig = New Configuration()
        ' Convert object to newConfig
        newConfig = CType(objConfig, Configuration)
        Me.Port = newConfig.Port
        Me.MaxPlayers = newConfig.MaxPlayers
    End Sub

    Public Sub Save()
        Files.WriteXML(pathContent & "config.xml", Me)
    End Sub
End Class
