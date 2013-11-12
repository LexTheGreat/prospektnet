Imports SFML.Audio
Imports IHProspekt.Database
Namespace Audio
    Public Module AudioPlayer
        'Music + Sound Players
        Public soundPlayer As Sound
        Public musicPlayer As Music
        Public soundPlayerBuffer As SoundBuffer

        Public Sub playMusic(ByVal filename As String)
            If ClientConfig.Music = False Then Exit Sub

            If Not System.IO.File.Exists(pathMusic & filename) Then Exit Sub

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
        Public Sub stopMusic()
            If ClientConfig.Music = False Then Exit Sub
            If musicPlayer Is Nothing Then Exit Sub
            musicPlayer.Stop()
            musicPlayer.Dispose()
            musicPlayer = Nothing
        End Sub
        Public Sub playSound(ByVal filename As String)
            If ClientConfig.Sound = False Then Exit Sub
            If Not System.IO.File.Exists(pathSound & filename) Then Exit Sub

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
        Public Sub stopSound()
            If ClientConfig.Sound = False Then Exit Sub
            If soundPlayer Is Nothing Then Exit Sub
            soundPlayer.Stop()
            soundPlayer.Dispose()
            soundPlayerBuffer.Dispose()
            soundPlayerBuffer = Nothing
            soundPlayer = Nothing
        End Sub
    End Module
End Namespace
