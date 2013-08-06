Imports SFML.Graphics

Module modRendering
    Public Sub renderMenu()
        On Error GoTo errorhandler
        ' don't render
        If frmMain.WindowState = FormWindowState.Minimized Then Exit Sub

        ' Start rendering
        SfmlWindow.Clear(New SFML.Graphics.Color(0, 0, 0))

        If faderState < 2 Then
            If Not faderAlpha = 255 Then renderTexture(texGui(2), (screenWidth * 0.5) - (Texture(texGui(2)).Width * 0.5), (screenHeight * 0.5) - (Texture(texGui(2)).Height * 0.5), 0, 0, Texture(texGui(2)).Width, Texture(texGui(2)).Height, Texture(texGui(2)).Width, Texture(texGui(2)).Height)
            DrawFader()
            Call verdana.Draw("Press space to skip intro...", 2, 2, SFML.Graphics.Color.Blue)
        Else
            ' Render background
            Call DrawBackGround()

            Call renderTexture(texGui(1), 0, screenHeight - 20, 0, 0, screenWidth, 20, 32, 32, 200, 0, 0, 0)

            Call Verdana.Draw(Application.ProductName & " v" & Application.ProductVersion, 5, screenHeight - 18, Color.White)
            Call Verdana.Draw("eatenbrain.com", screenWidth - 5 - Verdana.GetWidth("eatenbrain.com"), screenHeight - 18, Color.White)
            Call Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

            Select Case curMenu
                Case MenuEnum.Main : DrawMenu()
                Case MenuEnum.Login : DrawLogin()
                Case MenuEnum.Credits : DrawCredits()
            End Select
        End If

        ' End the rendering
        SfmlWindow.Display()
        Exit Sub
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Public Sub renderGame()
        On Error GoTo errorhandler
        ' don't render
        If frmMain.WindowState = FormWindowState.Minimized Then Exit Sub

        ' Start rendering
        SfmlWindow.Clear(New Color(0, 0, 0))

        ' Render background
        Call DrawBackGround()
        Call verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

        DrawPlayer()
        DrawPlayerName()

        ' End the rendering
        SfmlWindow.Display()
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Public Sub DrawPlayerName()
        Dim textX As Integer, textY As Integer, Text As String, textSize As Long

        Text = Trim$(Player.Name)
        textSize = verdana.GetWidth(Trim$(Player.Name))

        textX = Player.X * picX + Player.XOffset + (picX * 0.5) - (textSize * 0.5)
        textY = Player.Y * picY + Player.YOffset - picY

        textY = Player.Y * picY + Player.YOffset - picY

        If Player.Sprite >= 1 Then
            textY = Player.Y * picY + Player.YOffset - (Texture(texSprite(Player.Sprite)).Height / 4) + 12
        End If

        Call verdana.Draw(Text, textX, textY, Color.White)
    End Sub

    Public Sub DrawPlayer()
        Dim Anim As Byte
        Dim X As Integer
        Dim Y As Integer
        Dim Sprite As Integer, spritetop As Integer
        Dim rec As GeomRec

        ' pre-load sprite for calculations
        Sprite = Player.Sprite
        'SetTexture Tex_Char(Sprite)

        If Sprite < 1 Then Exit Sub

        ' Reset frame
        Anim = 1

        ' walk normally
        Select Case Player.Dir
            Case DirEnum.Up
                If (Player.YOffset > 8) Then Anim = Player.PlayerStep
                spritetop = 0
            Case DirEnum.Down
                If (Player.YOffset < -8) Then Anim = Player.PlayerStep
                spritetop = 2
            Case DirEnum.Left
                If (Player.XOffset > 8) Then Anim = Player.PlayerStep
                spritetop = 3
            Case DirEnum.Right
                If (Player.XOffset < -8) Then Anim = Player.PlayerStep
                spritetop = 1
        End Select

        rec.Top = spritetop * (Texture(texSprite(Sprite)).Height / 4)
        rec.Height = Texture(texSprite(Sprite)).Height / 4
        rec.Left = Anim * (Texture(texSprite(Sprite)).Width / 3)
        rec.Width = Texture(texSprite(Sprite)).Width / 3

        ' Calculate the X and Y
        X = Player.X * picX + Player.XOffset - ((Texture(texSprite(Sprite)).Width / 3 - 32) / 2)
        Y = Player.Y * picY + Player.YOffset - ((Texture(texSprite(Sprite)).Height / 4) - 32) - 4

        renderTexture(texSprite(Sprite), X, Y, rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Private Sub DrawFader()
        Call renderTexture(texGui(1), 0, 0, 0, 0, screenWidth, screenHeight, 32, 32, faderAlpha, 0, 0, 0)
    End Sub

    Private Sub DrawMenu()
        ' Buttons
        Call renderButton((screenWidth * 0.5) - (94 * 0.5), (screenHeight * 0.5) - (22 * 0.5) - 15, 94, 22, 1, 2, 1)
        Call renderButton((screenWidth * 0.5) - (147 * 0.5), (screenHeight * 0.5) - (22 * 0.5) + 15, 147, 22, 3, 4, 2)
    End Sub

    Private Sub DrawCredits()
        Call Silkscreen.Draw("Myself (lol)", (screenWidth * 0.5) - (Silkscreen.GetWidth("Myself (lol)", 20) * 0.5), (screenHeight * 0.5) - 18, Color.White, 20)
        Call Silkscreen.Draw("Aaron Krogh", (screenWidth * 0.5) - (Silkscreen.GetWidth("Aaron Krogh", 20) * 0.5), (screenHeight * 0.5) + 18, Color.White, 20)
    End Sub

    Private Sub DrawLogin()
        Call Silkscreen.Draw("USERNAME:", (screenWidth * 0.5) - 150, (screenHeight * 0.5) - 28, Color.White, 20)
        Call renderTexture(texGui(1), (screenWidth * 0.5) - 145, (screenHeight * 0.5) + 5, 0, 0, 290, 20, 32, 32, 200, 0, 0, 0)
        Call verdana.Draw(sUser & chatShowLine, (screenWidth * 0.5) - 140, (screenHeight * 0.5) + 8, Color.White)
    End Sub
    Private Sub renderButton(ByVal X As Integer, ByVal Y As Integer, ByVal W As Integer, ByVal H As Integer, ByVal Norm As Integer, ByVal Hov As Integer, ByVal ButtonIndex As Integer)

        ' Change the button state
        If mouseX > X And mouseX < X + W And mouseY > Y And mouseY < Y + H Then
            ' Hover state
            Call renderTexture(texButton(Hov), X, Y, 0, 0, W, H, W, H)

            ' When the button is clicked
            If mouseLeftDown > 0 Then

                ' Button sound
                playSound("button.ogg")

                ' Handle what the button does
                Select Case ButtonIndex
                    Case 0 ' Nothing
                    Case 1 : curMenu = MenuEnum.Login
                    Case 2 : curMenu = MenuEnum.Credits
                    Case Else : MsgBox("Button not assigned. Report this immediately!")
                End Select
                mouseLeftDown = 0
            End If
        Else
            ' Normal state
            Call renderTexture(texButton(Norm), X, Y, 0, 0, W, H, W, H)
        End If
    End Sub
    Public Sub DrawBackGround()
        Dim X As Long, Y As Long
        For X = 0 To maxX
            For Y = 0 To maxY
                Call renderTexture(texTileset(1), X * picX, Y * picY, 0, 8 * picY, picX, picY, picX, picY)
            Next Y
        Next X
    End Sub
End Module
