Public Class Players
    ' general
    Public Name As String
    Public Password As String
    Public Sprite As Integer
    ' location
    Public X As Integer, Y As Integer
    Public Dir As Byte
    ' non-saved values
    Public XOffset As Long, YOffset As Long
    Public Moving As Boolean
    Public PlayerStep As Byte

    ' sub routines and functions
    Public Sub Load(NewName As String, NewSprite As Integer, NewX As Integer, NewY As Integer, NewDir As Byte)
        Name = NewName
        Sprite = NewSprite
        X = NewX
        Y = NewY
        Dir = NewDir
    End Sub

    Sub ProcessMovement()
        Dim MovementSpeed As Long

        If Moving = True Then
            MovementSpeed = 2
        Else
            Exit Sub
        End If

        Select Case Dir
            Case DirEnum.Up
                YOffset = YOffset - MovementSpeed
                If YOffset < 0 Then YOffset = 0
            Case DirEnum.Down
                YOffset = YOffset + MovementSpeed
                If YOffset > 0 Then YOffset = 0
            Case DirEnum.Left
                XOffset = XOffset - MovementSpeed
                If XOffset < 0 Then XOffset = 0
            Case DirEnum.Right
                XOffset = XOffset + MovementSpeed
                If XOffset > 0 Then XOffset = 0
        End Select

        ' Check if completed walking over to the next tile
        If Moving = True Then
            If Dir = DirEnum.Right Or Dir = DirEnum.Down Then
                If (XOffset >= 0) And (YOffset >= 0) Then
                    Moving = False
                    If PlayerStep = 0 Then
                        PlayerStep = 2
                    Else
                        PlayerStep = 0
                    End If
                End If
            Else
                If (XOffset <= 0) And (YOffset <= 0) Then
                    Moving = False
                    If PlayerStep = 0 Then
                        PlayerStep = 2
                    Else
                        PlayerStep = 0
                    End If
                End If
            End If
        End If
    End Sub

End Class
