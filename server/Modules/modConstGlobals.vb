Module modConstGlobals
    ' File paths
    Public Const pathContent As String = "content/"

    ' Players
    Public Player(100) As clsPlayer
    Public PlayerHighIndex As Integer

    ' Configuration
    Public ServerConfig As ConfigStruct
    Public Structure ConfigStruct
        Dim Port As Integer
    End Structure
End Module
