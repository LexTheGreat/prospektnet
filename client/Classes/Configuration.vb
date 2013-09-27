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

    Public Sub LoadOptions()
        'Serialize object to a text file.
        Dim objStreamReader As New StreamReader(pathContent & "config.xml")
        Dim x As New XmlSerializer(ClientConfig.GetType)
        ClientConfig = x.Deserialize(objStreamReader)
        objStreamReader.Close()
    End Sub

    Public Sub SaveOptions()
        'Serialize object to a text file.
        Dim objStreamWriter As New StreamWriter(pathContent & "config.xml")
        Dim x As New XmlSerializer(ClientConfig.GetType)
        x.Serialize(objStreamWriter, ClientConfig)
        objStreamWriter.Close()
    End Sub
End Class
