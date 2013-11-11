Imports System.IO
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Namespace Database
    Public Module Files
        Public Function Exists(ByVal filepath As String) As Boolean
            Return File.Exists(filepath)
        End Function

        Public Function WriteXML(ByVal Path As String, ByVal obj As Object) As Boolean
            Try
                'Serialize object to a file.
                Dim Writer As New StreamWriter(Path)
                Dim ser As New XmlSerializer(obj.GetType)
                ser.Serialize(Writer, obj)
                Writer.Close()
                Return True
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.ToString & " (In: WriteXML")
                Return False
            End Try
        End Function

        Public Function ReadXML(ByVal Path As String, ByVal obj As Object) As Object
            Try
                'Deserialize file to object.
                Dim Reader As New StreamReader(Path)
                Dim ser As New XmlSerializer(obj.GetType)
                obj = ser.Deserialize(Reader)
                Reader.Close()
                Return obj
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.ToString & " (In: ReadXML")
                Return Nothing
            End Try
        End Function

        Public Function WriteBinary(ByVal Path As String, ByVal obj As Object) As Boolean
            Try
                Dim fs As New FileStream(Path, FileMode.Create)
                Dim formatter As New BinaryFormatter

                formatter.Serialize(fs, obj)
                fs.Close()
                Return True
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.ToString & " (In: WriteBinary")
                Return False
            End Try
        End Function

        Public Function ReadBinary(ByVal Path As String) As Object
            Try
                Dim fs As New FileStream(Path, FileMode.Open)
                Dim formatter As New BinaryFormatter, obj As Object
                obj = formatter.Deserialize(fs)
                fs.Close()
                Return obj
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.ToString & " (In: ReadBinary")
                Return Nothing
            End Try
        End Function
    End Module
End Namespace