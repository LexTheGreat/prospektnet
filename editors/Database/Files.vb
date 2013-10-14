Imports System.IO
Imports System.Xml.Serialization

Class Files
    Public Shared Function Exists(ByVal filepath As String) As Boolean
        Return File.Exists(filepath)
    End Function

    Public Shared Function Write(ByVal Path As String, ByVal obj As Object) As Boolean
        Try
            'Serialize object to a file.
            Dim Writer As New StreamWriter(Path)
            Dim ser As New XmlSerializer(obj.GetType)
            ser.Serialize(Writer, obj)
            Writer.Close()
            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Files.Write")
            Return False
        End Try
    End Function

    Public Shared Function Read(ByVal Path As String, ByVal obj As Object) As Object
        Try
            'Deserialize file to object.
            Dim Reader As New StreamReader(Path)
            Dim ser As New XmlSerializer(obj.GetType)
            obj = ser.Deserialize(Reader)
            Reader.Close()
            Return obj
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Files.Read")
            Return Nothing
        End Try
    End Function
End Class

