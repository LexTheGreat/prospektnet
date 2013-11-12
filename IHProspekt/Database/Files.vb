Imports System
Imports System.IO
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Namespace Database
    Public Class Files
        Implements IDisposable
        Private mPath As String
        Private mObject As Object

        Public Sub New(path As String)
            Me.mPath = path
            Me.mObject = Nothing
        End Sub

        Public Sub New(path As String, obj As Object)
            Me.mPath = path
            Me.mObject = obj
        End Sub

        Public Function WriteXML() As Boolean
            Try
                'Serialize object to a file.
                Dim Writer As New StreamWriter(Me.mPath)
                Dim ser As New XmlSerializer(Me.mObject.GetType)
                ser.Serialize(Writer, Me.mObject)
                Writer.Close()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function WriteXML(obj As Object) As Boolean
            Try
                'Serialize object to a file.
                Dim Writer As New StreamWriter(Me.mPath)
                Dim ser As New XmlSerializer(obj.GetType)
                ser.Serialize(Writer, obj)
                Writer.Close()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function ReadXML() As Object
            Try
                'Deserialize file to object.
                Dim Reader As New StreamReader(Me.mPath)
                Dim ser As New XmlSerializer(Me.mObject.GetType)
                Me.mObject = ser.Deserialize(Reader)
                Reader.Close()
                Return Me.mObject
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function ReadXML(obj As Object) As Object
            Try
                'Deserialize file to object.
                Dim Reader As New StreamReader(Me.mPath)
                Dim ser As New XmlSerializer(obj.GetType)
                obj = ser.Deserialize(Reader)
                Reader.Close()
                Return obj
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function WriteBinary() As Boolean
            Try
                Dim fs As New FileStream(Me.mPath, FileMode.Create)
                Dim formatter As New BinaryFormatter

                formatter.Serialize(fs, Me.mObject)
                fs.Close()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function WriteBinary(obj As Object) As Boolean
            Try
                Dim fs As New FileStream(Me.mPath, FileMode.Create)
                Dim formatter As New BinaryFormatter

                formatter.Serialize(fs, obj)
                fs.Close()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function ReadBinary() As Object
            Try
                Dim fs As New FileStream(Me.mPath, FileMode.Open)
                Dim formatter As New BinaryFormatter
                Me.mObject = formatter.Deserialize(fs)
                fs.Close()
                Return Me.mObject
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.mPath = vbNullString
            Me.mObject = Nothing
        End Sub
    End Class
End Namespace