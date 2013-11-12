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

        <System.Runtime.CompilerServices.Extension()> _
        Public Sub RemoveAt(Of T)(ByRef arr As T(), ByVal index As Integer)
            Dim uBound = arr.GetUpperBound(0)
            Dim lBound = arr.GetLowerBound(0)
            Dim arrLen = uBound - lBound

            If index < lBound OrElse index > uBound Then
                Throw New ArgumentOutOfRangeException( _
                String.Format("Index must be from {0} to {1}.", lBound, uBound))
            Else
                'create an array 1 element less than the input array
                Dim outArr(arrLen - 1) As T
                'copy the first part of the input array
                Array.Copy(arr, 0, outArr, 0, index)
                'then copy the second part of the input array
                Array.Copy(arr, index + 1, outArr, index, uBound - index)

                arr = outArr
            End If
        End Sub
    End Module
End Namespace