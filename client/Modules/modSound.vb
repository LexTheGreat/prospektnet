Option Explicit On
Imports SFML.Audio

Module modSound
    'Music + Sound Players
    Public soundPlayer As SFML.Audio.Sound
    Public musicPlayer As SFML.Audio.Music
    Public soundPlayerBuffer As SFML.Audio.SoundBuffer

    ' Hardcoded sound effects
    Public Const buttonClick As String = "button.ogg"

    Sub playMusic(ByVal filename As String)
        If ClientConfig.Music = False Then Exit Sub

        If Not fileExist(pathMusic & filename) Then Exit Sub

        If musicPlayer Is Nothing Then
            musicPlayer = New Music(pathMusic & filename)
            musicPlayer.Play()
        Else
            musicPlayer.Stop()
            musicPlayer.Dispose()
            musicPlayer = Nothing
            musicPlayer = New Music(pathMusic & filename)
            musicPlayer.Play()
        End If
    End Sub
    Sub stopMusic()
        If ClientConfig.Music = False Then Exit Sub
        If musicPlayer Is Nothing Then Exit Sub
        musicPlayer.Stop()
        musicPlayer.Dispose()
        musicPlayer = Nothing
    End Sub
    Sub playSound(ByVal filename As String)
        If ClientConfig.Sound = False Then Exit Sub
        If Not fileExist(pathSound & filename) Then Exit Sub

        If soundPlayer Is Nothing Then
            soundPlayerBuffer = New SoundBuffer(pathSound & filename)
            soundPlayer = New Sound(soundPlayerBuffer)
            soundPlayer.Play()
        Else
            soundPlayer.Stop()
            soundPlayer.Dispose()
            soundPlayerBuffer.Dispose()
            soundPlayerBuffer = Nothing
            soundPlayer = Nothing
            soundPlayerBuffer = New SoundBuffer(pathSound & filename)
            soundPlayer = New Sound(soundPlayerBuffer)
            soundPlayer.Play()
        End If
    End Sub
    Sub stopSound()
        If ClientConfig.Sound = False Then Exit Sub
        If soundPlayer Is Nothing Then Exit Sub
        soundPlayer.Stop()
        soundPlayer.Dispose()
        soundPlayerBuffer.Dispose()
        soundPlayerBuffer = Nothing
        soundPlayer = Nothing
    End Sub
End Module
