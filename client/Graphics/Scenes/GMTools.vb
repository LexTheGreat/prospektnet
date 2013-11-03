Imports SFML.Graphics
Imports SFML.Window
Imports Prospekt.Graphics
Imports Prospekt.Network

Public Class GMTools
    Public Shared Visible As Boolean
    Public Shared GMBase As GMWindow

    Public Class GMWindow
        Public Shared Mode As Integer ' 0 = None, 1 = Player, 2 = Items, 3 = Npc, 4 = Terrain
        Public X As Integer
        Public Y As Integer
        Public Width As Integer
        Public Height As Integer
        Public Texture As Integer
        Public Title As String
        Public Shared Scroll As Integer = 0

        ' Player Mode
        Private Shared sPlayerIndex As Integer = -1
        Private Shared pMode As Integer = 0

        Public Shared Sub Toggle()
            Scroll = 0
            Mode = 0
            pMode = 0
            sPlayerIndex = -1
        End Sub

        Public Sub Draw()
            Render.RenderTexture(Texture, X, Y, 0, 0, Width, Height, Width, Height, 120, 0, 0, 0)
            ' Draw Title
            Verdana.Draw(Title, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(Title), Y + 5, Color.White, 22)
            Dim line As String = "-", i As Integer
            For i = 0 To Title.Length
                line = line + "-"
            Next
            ' Draw Title Underline
            Verdana.Draw(line, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(Title), Y + 23, Color.White, 22)
            ' Exit Button
            Render.RenderButton(X + 215, Y, 15, 15, 9, 9, AddressOf ButtonPress, -1)

            If Not (Mode = 0) Then
                ' Back Button
                Render.RenderButton(X, Y, 15, 15, 7, 7, AddressOf ButtonPress, 0)
            End If

            ' Draw Mode
            Select Case Mode
                Case 0 : DrawSelectMode()
                Case 1 : DrawPlayerMode()
            End Select
        End Sub

        Public Sub DrawSelectMode()
            Dim text As String = vbNullString

            text = "- Player List -"
            Verdana.Draw(text, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(Title) - (Verdana.GetWidth(text) * 0.2), Y + 58, Color.White, AddressOf ButtonPress, 1, 18)

            text = "- Item List -"
            Verdana.Draw(text, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(Title) - (Verdana.GetWidth(text) * 0.2), Y + 88, Color.White, AddressOf ButtonPress, 2, 18)

            text = "- Npc List -"
            Verdana.Draw(text, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(Title) - (Verdana.GetWidth(text) * 0.2), Y + 118, Color.White, AddressOf ButtonPress, 3, 18)

            text = "- Terrain List -"
            Verdana.Draw(text, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(Title) - (Verdana.GetWidth(text) * 0.2), Y + 148, Color.White, AddressOf ButtonPress, 4, 18)
        End Sub

        Public Sub DrawPlayerMode()
            ' Draw Mode
            Select Case pMode
                Case 0 : DrawPlayerList()
                Case 1, 2 : DrawPlayerOptions()
            End Select
        End Sub

        Public Sub DrawPlayerList()
            Dim i As Integer, text As String = "Online Players"
            Verdana.Draw(text, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(Title), Y + 45, Color.White, 12)

            For i = Scroll + 1 To Scroll + 10
                If Not IsNothing(Player(i)) Then
                    ' Trim name if to long
                    If (Trim$(Player(i).Name).Length >= 25) Then
                        text = Trim$(Player(i).Name).Substring(0, 23) & "..."
                    Else
                        text = Trim$(Player(i).Name)
                    End If
                    If (i = sPlayerIndex) Then
                        Verdana.Draw(text, X + 12, Y + 55 + (i * 12), Color.Green, AddressOf PlayerSelected, i, 12)
                    Else
                        Verdana.Draw(text, X + 12, Y + 55 + (i * 12), Color.White, AddressOf PlayerSelected, i, 12)
                    End If
                End If
            Next
            ' Check if we need an up scroll
            If (Scroll > 9) Then
                ' Scroll Up
                Render.RenderButton(X + 205, Y + 65, 15, 15, 6, 6, AddressOf PlayerModeButtonPress, 1)
            End If
            ' Check if we need a down scroll
            If (PlayerCount > 9 And (Scroll + 9) <= PlayerCount) Then
                ' Scroll Down
                Render.RenderButton(X + 205, Y + 175, 15, 15, 5, 5, AddressOf PlayerModeButtonPress, 2)
            End If
            If Not sPlayerIndex = -1 Then
                ' Player Options
                text = "Player Options"
                Verdana.Draw(text, X + 12, Y + 195, Color.White, AddressOf PlayerModeButtonPress, 3, 18)
            End If
        End Sub

        Public Sub DrawPlayerOptions()
            Dim text As String = "Player Options"
            If (pMode = 1) Then
                'Draw title
                Verdana.Draw(text, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(Title), Y + 45, Color.White, 12)

                text = "- Edit Player -"
                Verdana.Draw(text, X + 12, Y + 65, Color.White, AddressOf PlayerModeButtonPress, 4, 12)

                text = "- Warp To -"
                Verdana.Draw(text, X + 12, Y + 85, Color.White, AddressOf PlayerModeButtonPress, 5, 12)

                text = "- Warp To Me -"
                Verdana.Draw(text, X + 12, Y + 105, Color.White, AddressOf PlayerModeButtonPress, 6, 12)

                text = "- View Inventory -"
                Verdana.Draw(text, X + 12, Y + 125, Color.White, AddressOf PlayerModeButtonPress, 7, 12)

                text = "- View Bank -"
                Verdana.Draw(text, X + 12, Y + 145, Color.White, AddressOf PlayerModeButtonPress, 8, 12)
            ElseIf (pMode = 2) Then
                If (Trim$(Player(sPlayerIndex).Name).Length >= 10) Then
                    text = "Edit: " & Trim$(Player(sPlayerIndex).Name).Substring(0, 7) & "..."
                Else
                    text = "Edit: " & Trim$(Player(sPlayerIndex).Name)
                End If
                'Draw title
                Verdana.Draw(text, ((X + (Title.Length * 2)) + (X + Width) / (Title.Length)), Y + 45, Color.White, 12)

            End If
        End Sub

        Public Sub DrawPlayerEdit()
            Dim text As String = "Edit: " & Trim$(Player(sPlayerIndex).Name)
            If (Trim$(Player(sPlayerIndex).Name).Length >= 10) Then
                text = "Edit: " & Trim$(Player(sPlayerIndex).Name).Substring(0, 7) & "..."
            End If

            Verdana.Draw(text, X + (gTexture(Me.Texture).Width * 0.5) + Verdana.GetWidth(text), Y + 45, Color.White, 12)

        End Sub

        Public Shared Function ButtonPress(ByVal index As Integer) As Boolean
            ' Handle what the button does
            Select Case index
                Case -1 : GMTools.Visible = False
                Case 0 : Toggle()
                Case 1 : Mode = 1
                Case 2 : Mode = 2
                Case 3 : Mode = 3
                Case 4 : Mode = 4
                Case Else
                    MsgBox("Button not assigned. Report this immediately!")
                    Return False
            End Select
            Return True
        End Function

        Public Shared Function PlayerModeButtonPress(ByVal Index As Integer) As Boolean
            Select Case Index
                Case 0 ' Nothing
                Case 1 ' Scroll Up
                    Scroll = Scroll - 9
                    If (Scroll < 0) Then
                        Scroll = 0
                        Return False
                    End If
                    Return True
                Case 2 ' Scroll Down
                    Scroll = Scroll + 9
                    If (PlayerCount > 10) Then
                        If (Scroll > PlayerCount - 10) Then
                            Scroll = PlayerCount - 10
                            Return True
                        End If
                    Else
                        Scroll = 0
                        Return False
                    End If
                    Return True
                Case 3 ' Player Features
                    pMode = 1
                    Return True
                Case 4 ' Edit Playr
                    pMode = 2
                    Return True
                Case 5 ' Warp To
                    SendData.WarpTo(sPlayerIndex)
                    pMode = 0
                    Return True
                Case 6 ' Warp To Me
                    SendData.WarpToMe(sPlayerIndex)
                    pMode = 0
                    Return True
                Case 7 ' View Inventory
                    pMode = 0
                    Return True
                Case 8 ' View Bank
                    pMode = 0
                    Return True
                Case Else
                    MsgBox("Button not assigned. Report this immediately!")
                    Return False
            End Select
            Return False
        End Function

        Public Shared Function PlayerSelected(ByVal index As Integer) As Boolean
            ' Index not valid
            If Not (index > 0 And index <= PlayerCount) Then
                MessageBox.Show("Error: Invalid Player Index!")
                Return False
            End If
            ' Valid index, set it and return true
            sPlayerIndex = index
            Return True
        End Function
    End Class

    Public Shared Sub Init()
        Visible = False
        GMBase = New GMWindow
        GMBase.Width = 25
        GMBase.Height = 350
        GMBase.X = ClientConfig.ScreenWidth - 235
        GMBase.Y = 0
        GMBase.Texture = texGui(1)

        GMBase.Title = "GM Tools"
    End Sub

    Public Shared Sub Draw()
        ' Draw the background
        GMBase.Draw()
    End Sub

    Public Shared Sub Show()
        Visible = True
        ' Show the background
        GMWindow.Toggle()
    End Sub

    Public Shared Sub Close()
        Visible = False
        ' Hide the background
        GMWindow.Toggle()
    End Sub
End Class
