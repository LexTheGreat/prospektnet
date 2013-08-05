Public Class clsPlayer
    ' general
    Public Name As String
    Public Sprite As Long
    ' location
    Public X As Long, Y As Long
    Public Dir As Byte
    ' non-saved values
    Public XOffset As Long, YOffset As Long
    Public Moving As Boolean
    Public PlayerStep As Byte

    ' sub routines and functions
    Public Sub New(NewName As String, NewSprite As Long, NewX As Long, NewY As Long)
        Name = NewName
        Sprite = NewSprite
        X = NewX
        Y = NewY
    End Sub

End Class
