Imports SFML.Graphics
Imports Prospekt.Input
Imports Prospekt.Audio
Imports Prospekt.Graphics
Imports Prospekt.Network

Public Class MenuScene
    Public Enum MenuEnum
        Main = 0
        Login
        Register
        Creation
        Credits
        StatusMessage
        ' Make sure COUNT is below everything else
        COUNT
    End Enum
    Public Sub Draw()
        On Error GoTo errorhandler
        ' don't render
        If GameWindow.WindowState = FormWindowState.Minimized Then Exit Sub

        ' Start rendering
        Render.Window.Clear(New Color(255, 255, 255))

        If faderState < 2 Then
            If Not faderAlpha = 255 Then Render.RenderTexture(texGui(2), (ClientConfig.ScreenWidth * 0.5) - (gTexture(texGui(2)).Width * 0.5), (ClientConfig.ScreenHeight * 0.5) - (gTexture(texGui(2)).Height * 0.5), 0, 0, gTexture(texGui(2)).Width, gTexture(texGui(2)).Height, gTexture(texGui(2)).Width, gTexture(texGui(2)).Height)
            Render.DrawFader()
            Verdana.Draw("Press 'SPACE' to skip intro", 2, 2, New Color(100, 100, 100, 255))
        Else
            ' Render background
            Render.RenderTexture(texGui(3), 0, 0, 0, 0, ClientConfig.ScreenWidth, ClientConfig.ScreenHeight, gTexture(texGui(3)).Width, gTexture(texGui(3)).Height)

            Render.RenderTexture(texGui(1), 0, ClientConfig.ScreenHeight - 20, 0, 0, ClientConfig.ScreenWidth, 20, 32, 32, 200, 0, 0, 0)
            Render.RenderTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 200, (ClientConfig.ScreenHeight * 0.5) - 100, 0, 0, 400, 200, 32, 32, 120, 0, 0, 0)


            Verdana.Draw(Application.ProductName & " v" & Application.ProductVersion, 5, ClientConfig.ScreenHeight - 18, Color.White)
            Verdana.Draw("indiearmory.com", ClientConfig.ScreenWidth - 5 - Verdana.GetWidth("indiearmory.com"), ClientConfig.ScreenHeight - 18, Color.Cyan, AddressOf ButtonPress, 4)
            Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

            Select Case curMenu
                Case MenuEnum.Main : DrawMenu()
                Case MenuEnum.Login : DrawLogin()
                Case MenuEnum.Register : DrawLogin()
                Case MenuEnum.Creation : DrawCreation()
                Case MenuEnum.Credits : DrawCredits()
                Case MenuEnum.StatusMessage : DrawStatusMessage()
            End Select

            Render.DrawFader()
        End If

        ' End the rendering
        Render.Window.Display()
        Exit Sub
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Public Function KeyDown(ByVal Key As Keys) As Boolean
        ' Logging in
        If loginSent Then Return False

        Select Case Key
            Case Keys.Escape
                If curMenu <> MenuEnum.Main Then
                    ' Button sound
                    playSound("button.ogg")
                    curMenu = MenuEnum.Main
                Else
                    GameWindow.Close()
                End If
            Case Keys.Tab
                If curTextbox = 0 Then
                    curTextbox = 1
                Else
                    curTextbox = 0
                End If
            Case Keys.Back
                If curMenu = MenuEnum.Login Or curMenu = MenuEnum.Register Then
                    If curTextbox = 0 Then
                        If Len(sEmail) > 0 Then sEmail = Mid(sEmail, 1, Len(sEmail) - 1)
                    Else
                        If Len(sPass) > 0 Then sPass = Mid(sPass, 1, Len(sPass) - 1)
                        If Len(sHidden) > 0 Then sHidden = Mid(sHidden, 1, Len(sHidden) - 1)
                    End If
                ElseIf curMenu = MenuEnum.Creation Then
                    If Len(sCharacter) > 0 Then sCharacter = Mid(sCharacter, 1, Len(sCharacter) - 1)
                End If
            Case Keys.Space
                If faderState < 2 Then
                    faderState = 2
                    faderAlpha = 0
                End If
            Case Keys.Return
                If curMenu = MenuEnum.Login Then
                    If Networking.ConnectToServer() Then
                        If Len(Trim(sEmail)) > 0 And Len(Trim(sPass)) > 0 And EmailAddressChecker(Trim(sEmail)) = True Then
                            curMenu = MenuEnum.StatusMessage
                            sMessage = "Sending login..."
                            SendData.Login(sEmail, sPass)
                        Else
                            curMenu = MenuEnum.Login
                            sMessage = vbNullString
                            MsgBox(Trim(sEmail))
                            MsgBox("Username and password fields can not be empty or you entered wrong email.")
                        End If
                    Else
                        curMenu = MenuEnum.Main
                        sMessage = vbNullString
                        MsgBox("Server is offline")
                    End If
                ElseIf curMenu = MenuEnum.Register Then
                    If Networking.ConnectToServer() Then
                        If Len(Trim(sEmail)) > 0 And Len(Trim(sPass)) > 0 And EmailAddressChecker(Trim(sEmail)) = True Then
                            curMenu = MenuEnum.StatusMessage
                            sMessage = "Sending register..."
                            SendData.Register(sEmail, sPass)
                        Else
                            curMenu = MenuEnum.Register
                            sMessage = vbNullString
                            MsgBox(Trim(sEmail))
                            MsgBox("Username and password fields can not be empty or you entered wrong email.")
                        End If
                    Else
                        curMenu = MenuEnum.Main
                        sMessage = vbNullString
                        MsgBox("Server is offline")
                    End If
                ElseIf curMenu = MenuEnum.Creation Then
                    If Networking.ConnectToServer() Then
                        If Len(Trim(sEmail)) > 0 And Len(Trim(sCharacter)) > 0 Then
                            curMenu = MenuEnum.StatusMessage
                            sMessage = "Sending new character..."
                            SendData.NewCharacter(sEmail, sCharacter)
                        Else
                            curMenu = MenuEnum.Creation
                            sMessage = vbNullString
                            MsgBox("Name field can not be empty")
                        End If
                    Else
                        curMenu = MenuEnum.Main
                        sMessage = vbNullString
                        MsgBox("Server is offline")
                    End If
                End If
        End Select
    End Function

    Public Function KeyPress(ByVal Key As Char) As Boolean
        ' Logging in
        If loginSent Then Return False

        If Not Keyboard.GetKeyState(Keys.Back) And Not Keyboard.GetKeyState(Keys.Return) And Not Keyboard.GetKeyState(Keys.Tab) And Not Keyboard.GetKeyState(Keys.Escape) Then
            If curMenu = MenuEnum.Login Or curMenu = MenuEnum.Register Then
                If curTextbox = 0 Then
                    sEmail = sEmail & Key.ToString
                Else
                    sPass = sPass & Key.ToString
                    sHidden = sHidden & "*"
                End If
            ElseIf curMenu = MenuEnum.Creation Then
                sCharacter = sCharacter & Key.ToString
            End If
        End If
    End Function

    Public Sub DrawMenu()
        ' Buttons
        Render.RenderButton((ClientConfig.ScreenWidth * 0.5) - (128 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (32 * 0.5) - 30, 128, 32, 1, 2, AddressOf ButtonPress, 1)
        Render.RenderButton((ClientConfig.ScreenWidth * 0.5) - (128 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (32 * 0.5), 128, 32, 10, 11, AddressOf ButtonPress, 2)
        Render.RenderButton((ClientConfig.ScreenWidth * 0.5) - (128 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (32 * 0.5) + 30, 128, 32, 3, 4, AddressOf ButtonPress, 3)
    End Sub

    Public Sub DrawLogin()
        ' Back Button
        Render.RenderButton((ClientConfig.ScreenWidth * 0.5) - 185, (ClientConfig.ScreenHeight * 0.5) - 85, 15, 15, 7, 7, AddressOf ButtonPress, 0)

        Verdana.Draw("EMAIL:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("USERNAME:"), (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Render.RenderTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5) - 30, 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 0 Then
            Verdana.Draw(sEmail & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Else
            Verdana.Draw(sEmail, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        End If
        Verdana.Draw("PASSWORD:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("PASSWORD:"), (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        Render.RenderTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5), 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 1 Then
            Verdana.Draw(sHidden & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        Else
            Verdana.Draw(sHidden, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        End If
    End Sub

    Public Sub DrawCreation()
        Verdana.Draw("Player Name:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("Player Name:"), (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Render.RenderTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5) - 30, 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        Verdana.Draw(sCharacter & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
    End Sub

    Public Sub DrawCredits()
        ' Back Button
        Render.RenderButton((ClientConfig.ScreenWidth * 0.5) - 185, (ClientConfig.ScreenHeight * 0.5) - 85, 15, 15, 7, 7, AddressOf ButtonPress, 0)
        Verdana.Draw("IndieArmory", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("IndieArmory") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 42, Color.White)
        Verdana.Draw("Thomas 'Deathbeam' Slusny", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Thomas 'Deathbeam' Slusny") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 28, Color.White)
        Verdana.Draw("Aaron Krogh", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Aaron Krogh") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 14, Color.White)
        Verdana.Draw("Enterbrain", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Enterbrain") * 0.5), (ClientConfig.ScreenHeight * 0.5), Color.White)
        Verdana.Draw("First Seed Material", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("First Seed Material") * 0.5), (ClientConfig.ScreenHeight * 0.5) + 14, Color.White)
        Verdana.Draw("James 'Ertzel' Wilson", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("James 'Ertzel' Wilson") * 0.5), (ClientConfig.ScreenHeight * 0.5) + 28, Color.White)
    End Sub

    Public Sub DrawStatusMessage()
        Render.RenderTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 10 - (Verdana.GetWidth(sMessage) * 0.5), (ClientConfig.ScreenHeight * 0.5) - 20, 0, 0, Verdana.GetWidth(sMessage) + 20, 34, 32, 32, 200, 0, 0, 0)
        Verdana.Draw(sMessage, (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth(sMessage) * 0.5), (ClientConfig.ScreenHeight * 0.5) - 14, Color.White)
    End Sub

    Public Function ButtonPress(ByVal index As Integer) As Boolean
        ' Logging in
        If loginSent Then Return False

        ' Handle what the button does
        Select Case index
            Case 0
                curMenu = MenuEnum.Main
                curTextbox = 0
                sEmail = vbNullString
                sPass = vbNullString
                sHidden = vbNullString
            Case 1 : curMenu = MenuEnum.Login
            Case 2 : curMenu = MenuEnum.Register
            Case 3 : curMenu = MenuEnum.Credits
            Case 4 : Process.Start("http://indiearmory.com/")
            Case Else
                MsgBox("Button not assigned. Report this immediately!")
                Return False
        End Select
        Return True
    End Function
End Class
