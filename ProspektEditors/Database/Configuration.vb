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
        Using File As New Files(pathContent & "config.xml", Me)
            newConfig = DirectCast(File.ReadXML, Configuration)
        End Using
        Me.IP = newConfig.IP
        Me.Port = newConfig.Port
    End Sub

    Public Sub Save()
        Using File As New Files(pathContent & "config.xml", Me)
            File.WriteXML()
        End Using
    End Sub
End Class