Imports System.IO
Imports System.Xml.Serialization
Public Class Configuration
    Public Port As Integer

    Public Sub LoadOptions()
        'Serialize object to a text file.
        Dim objStreamReader As New StreamReader(pathContent & "config.xml")
        Dim x As New XmlSerializer(ServerConfig.GetType)
        ServerConfig = x.Deserialize(objStreamReader)
        objStreamReader.Close()
    End Sub

    Public Sub SaveOptions()
        'Serialize object to a text file.
        Dim objStreamWriter As New StreamWriter(pathContent & "config.xml")
        Dim x As New XmlSerializer(ServerConfig.GetType)
        x.Serialize(objStreamWriter, ServerConfig)
        objStreamWriter.Close()
    End Sub
End Class
