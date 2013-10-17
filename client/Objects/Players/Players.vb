Public Class Players
    ' general
    Public Name As String
    Public Password As String
    Public Sprite As Integer
    ' location
    Public X As Integer, Y As Integer
    Public Dir As Byte
    ' Guild
    Public GuildID As Integer
    ' Admin values
    Public AccessMode As Byte
    Public Visible As Boolean
    ' non-saved values
    Public XOffset As Integer, YOffset As Integer
    Public Moving As Boolean
    Public PlayerStep As Byte
    Public PartyID As Integer

    ' sub routines and functions
    Public Sub Load(NewName As String, NewSprite As Integer, NewX As Integer, NewY As Integer, NewDir As Byte, ByVal NewGuild As Integer, NewParty As Integer, NewAccess As Byte, NewVisible As Boolean)
        Name = NewName
        Sprite = NewSprite
        X = NewX
        Y = NewY
        Dir = NewDir
        GuildID = NewGuild
        PartyID = NewParty
        AccessMode = NewAccess
        Visible = NewVisible
    End Sub

    Public Sub SetAccess(ByVal value As Byte)
        Me.AccessMode = value
    End Sub

    Public Function GetAccess() As Byte
        Return Me.AccessMode
    End Function

    Sub ProcessMovement()
        Dim MovementSpeed As Integer

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
