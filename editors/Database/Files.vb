Imports System.IO
Imports System.Xml.Serialization

Class Files
    Public Shared Function Exists(ByVal filepath As String) As Boolean
        Return File.Exists(filepath)
    End Function

    Public Shared Function Write(ByVal Path As String, ByVal obj As Object) As Boolean
        'Serialize object to a file.
        Dim Writer As New StreamWriter(Path)
        Dim ser As New XmlSerializer(obj.GetType)
        ser.Serialize(Writer, obj)
        Writer.Close()
        Return True
    End Function

    Public Shared Function Read(ByVal Path As String, ByVal obj As Object) As Object
        'Deserialize file to object.
        Dim Reader As New StreamReader(Path)
        Dim ser As New XmlSerializer(obj.GetType)
        obj = ser.Deserialize(Reader)
        Reader.Close()
        Return obj
    End Function
End Class

