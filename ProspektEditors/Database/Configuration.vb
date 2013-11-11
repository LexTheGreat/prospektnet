Imports IHProspekt.Database
Public Class Configuration
    Public IP As String
    Public Port As Integer

    Public Sub New()
        Me.IP = "localhost"
        Me.Port = 8080
    End Sub

    Public Sub Load()
        Dim newConfig As New Configuration
        newConfig = DirectCast(ReadXML(pathContent & "config.xml", Me), Configuration)
        Me.IP = newConfig.IP
        Me.Port = newConfig.Port
    End Sub

    Public Sub Save()
        WriteXML(pathContent & "config.xml", Me)
    End Sub
End Class