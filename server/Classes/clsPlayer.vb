Public Class clsPlayer
    ' general
    Public Name As String
    Public Sprite As Integer
    ' location
    Public X As Integer
    Public Y As Integer
    Public Dir As Byte
    ' non-saved values
    Public XOffset As Long, YOffset As Long
    Public Moving As Boolean
    Public PlayerStep As Byte
    Public isPlaying As Boolean

    ' sub routines and functions
    Public Sub New(NewName As String, NewSprite As Integer, NewX As Integer, NewY As Integer, NewDir As Byte)
        Name = NewName
        Sprite = NewSprite
        X = NewX
        Y = NewY
        Dir = NewDir
    End Sub
End Class
