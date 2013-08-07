Imports System.Text

Public Class clsBuffer
    ' Our byte array to hold all data
    Public Buff As List(Of Byte)

    ' This is used for holding our position in the byte array when reading variables
    Public ReadHead As Integer
    Public WriteHead As Integer

    Public Sub New()
        ' Create a new buffer
        Buff = New List(Of Byte)

        ' Clear buffer values
        ReadHead = 0
        WriteHead = 0
    End Sub

    Public Sub Clear()
        Buff.Clear()
        ReadHead = 0
    End Sub

    Public Function ReadString(Optional ByVal Peek As Boolean = True) As String
        Dim Len As Integer = ReadInteger(True)
        Dim ret As String = Encoding.ASCII.GetString(Buff.ToArray, ReadHead, Len)

        ' Check to see if this passes the byte count
        If Peek And Buff.Count > ReadHead Then
            If ret.Length > 0 Then
                ReadHead += Len
            End If
        End If

        ' Return function value
        Return ret
    End Function

    Public Function ReadBytes(ByVal Length As Integer, Optional ByRef Peek As Boolean = True) As Byte()
        Dim ret() As Byte = Buff.GetRange(ReadHead, Length).ToArray

        If Peek Then
            ReadHead += Length
        End If

        ' Return function value
        Return ret
    End Function

    Public Function ReadInteger(Optional ByVal peek As Boolean = True) As Integer
        Dim Ret As Integer = BitConverter.ToInt32(Buff.ToArray, ReadHead)

        ' Check to see if this passes the byte count
        If Buff.Count <= ReadHead Then Return 0

        If peek And Buff.Count > ReadHead Then
            ReadHead += 4
        End If

        ' Return function value
        Return Ret
    End Function

    Public Function ReadLong(Optional ByVal peek As Boolean = True) As Long
        Dim ret As Long = BitConverter.ToInt64(Buff.ToArray, ReadHead)

        ' Check to see if this passes the byte count
        If Buff.Count <= ReadHead Then Return 0

        If peek And Buff.Count > ReadHead Then
            ReadHead += 8
        End If

        ' Return function value
        Return ret
    End Function

    Public Sub WriteBytes(ByVal Input() As Byte)
        Buff.AddRange(Input)
    End Sub

    Public Sub WriteInteger(ByVal Input As Integer)
        Buff.AddRange(BitConverter.GetBytes(Input))
    End Sub

    Public Sub WriteLong(ByVal Input As Long)
        Buff.AddRange(BitConverter.GetBytes(Input))
    End Sub

    Public Sub WriteString(ByVal Input As String)
        Buff.AddRange(BitConverter.GetBytes(Input.Length))
        Buff.AddRange(Encoding.ASCII.GetBytes(Input))
    End Sub

    Public Function ToArray() As Byte()
        Return Buff.ToArray
    End Function

    Public Function Count() As Integer
        Return Buff.Count
    End Function

    Public Function Length() As Integer
        Length = Count() - ReadHead
    End Function
End Class
