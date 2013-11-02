Imports System.Text
Imports System.Security.Cryptography

Public Module ServerLogic
    Public Function ResizeArray(ByVal arr As Array, ByVal newSizes() As Integer) As Array
        If newSizes.Length <> arr.Rank Then
            Throw New ArgumentException()
        End If

        Dim temp As Array = Array.CreateInstance(arr.GetType().GetElementType(), newSizes)
        Dim length As Integer = If(arr.Length <= temp.Length, arr.Length, temp.Length)
        Array.ConstrainedCopy(arr, 0, temp, 0, length)
        Return temp
    End Function
    Function Md5FromString(ByVal Source As String) As String
        Dim Bytes() As Byte
        Dim sb As New StringBuilder()
        If String.IsNullOrEmpty(Source) Then
            Throw New ArgumentNullException
        End If
        Bytes = Encoding.Default.GetBytes(Source)
        Bytes = MD5.Create().ComputeHash(Bytes)
        For x As Integer = 0 To Bytes.Length - 1
            sb.Append(Bytes(x).ToString("x2"))
        Next
        Return sb.ToString()
    End Function
    Sub WriteLine(ByVal obj As Object, Optional ByVal color As Object = "Gray")
        Dim colorNames() As String = ConsoleColor.GetNames(GetType(ConsoleColor))
        For Each colorName As String In colorNames
            colorName = CType(System.Enum.Parse(GetType(ConsoleColor), colorName), ConsoleColor)
            Dim colorType
            Try : colorType = CType(System.Enum.Parse(GetType(ConsoleColor), color), ConsoleColor) : Catch ex As Exception : colorType = ConsoleColor.Gray : End Try
            If colorName = colorType Then
                Console.ForegroundColor = colorType
                Console.WriteLine(obj)
                Console.ResetColor()
                Return
            End If
        Next
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("cPrint usage error (Incorrect color)")
        Console.ResetColor()
    End Sub
End Module
