Imports System.Text
Imports System.Security.Cryptography

Public Module ServerLogic
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
