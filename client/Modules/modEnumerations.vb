Module modEnumerations
    Public Enum DirEnum
        Up = 0
        Down
        Left
        Right
    End Enum

    Public Enum MenuEnum
        Main = 0
        Login
        Credits
    End Enum

    Public Structure GeomRec
        Dim Left As Integer
        Dim Top As Integer
        Dim Width As Integer
        Dim Height As Integer
    End Structure
End Module
