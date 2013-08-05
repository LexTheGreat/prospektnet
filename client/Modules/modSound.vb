Option Explicit On
Imports Microsoft.DirectX.AudioVideoPlayback

Module modSound
    'Music + Sound Players
    Public soundPlayer As Audio
    Public musicPlayer As Audio

    ' Hardcoded sound effects
    Public Const buttonClick As String = "button.ogg"

    Sub playMusic(ByVal filename As String)

        If Not FileExist(Application.StartupPath & pathMusic & filename) Then Exit Sub

        If MusicPlayer Is Nothing Then
            MusicPlayer = New Audio(Application.StartupPath & pathMusic & filename, True)
        Else
            MusicPlayer.Dispose()
            MusicPlayer = Nothing
            MusicPlayer = New Audio(Application.StartupPath & pathMusic & filename, True)
        End If
    End Sub
    Sub stopMusic()
        If MusicPlayer Is Nothing Then Exit Sub
        MusicPlayer.Dispose()
        MusicPlayer = Nothing
    End Sub
    Sub playSound(ByVal filename As String)

        If Not FileExist(Application.StartupPath & pathSound & filename) Then Exit Sub

        If SoundPlayer Is Nothing Then
            SoundPlayer = New Audio(Application.StartupPath & pathSound & filename, True)
        Else
            SoundPlayer.Dispose()
            SoundPlayer = Nothing
            SoundPlayer = New Audio(Application.StartupPath & pathSound & filename, True)
        End If
    End Sub
    Sub stopSound()
        If SoundPlayer Is Nothing Then Exit Sub
        SoundPlayer.Dispose()
        SoundPlayer = Nothing
    End Sub
End Module
