
Public Class frmMain
    Private Sub frmMain_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        inMenu = False
        inGame = False
        Verdana = Nothing
        DestroyTCP()
        DestroySFML()
        End
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If inGame Then
            Select e.KeyCode
                Case Keys.Return
                    If inChat Then
                        If Len(Trim(sChat)) > 0 Then SendMessage(sChat)
                        sChat = vbNullString
                        inChat = False
                    Else
                        inChat = True
                    End If
                Case Keys.Back
                    If inChat And Len(sChat) > 0 Then sChat = Mid(sChat, 1, Len(sChat) - 1)
            End Select
        End If
        If inMenu Then
            Select Case e.KeyCode
                Case Keys.Escape
                    If curMenu <> MenuEnum.Main Then
                        ' Button sound
                        playSound("button.ogg")
                        curMenu = MenuEnum.Main
                    Else
                        Me.Close()
                    End If
                Case Keys.Tab
                    If curTextbox = 0 Then
                        curTextbox = 1
                    Else
                        curTextbox = 0
                    End If
                Case Keys.Back
                    If curMenu = MenuEnum.Login Then
                        If curTextbox = 0 Then
                            If Len(sUser) > 0 Then sUser = Mid(sUser, 1, Len(sUser) - 1)
                        Else
                            If Len(sPass) > 0 Then sPass = Mid(sPass, 1, Len(sPass) - 1)
                        End If
                    End If
                Case Keys.Space
                    If faderState < 2 Then
                        faderState = 2
                        faderAlpha = 0
                    End If
                Case Keys.Return
                    If curMenu = MenuEnum.Login Then
                        If ConnectToServer() Then
                            If Len(Trim(sUser)) > 0 And Len(Trim(sPass)) > 0 Then
                                SendLogin(sUser, sPass)
                            Else
                                MsgBox("Username and password fields can not be empty")
                            End If
                        Else
                            MsgBox("Server is offline")
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub frmMain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If inMenu And curMenu = MenuEnum.Login Then
            If Not GetKeyState(Keys.Back) And Not GetKeyState(Keys.Return) And Not GetKeyState(Keys.Tab) And Not GetKeyState(Keys.Escape) Then
                If curTextbox = 0 Then
                    sUser = sUser & e.KeyChar
                Else
                    sPass = sPass & e.KeyChar
                End If
            End If
        End If
        If inGame And inChat Then
            If Not GetKeyState(Keys.Back) And Not GetKeyState(Keys.Return) And Not GetKeyState(Keys.Tab) And Not GetKeyState(Keys.Escape) Then
                sChat = sChat & e.KeyChar
            End If
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True) 'Do not draw forms background
        Me.Refresh()
    End Sub

    Private Sub frmMain_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        mouseLeftDown = Windows.Forms.MouseButtons.Left
        mouseRightDown = Windows.Forms.MouseButtons.Right
    End Sub

    Private Sub frmMain_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        mouseX = e.X
        mouseY = e.Y
    End Sub

    Private Sub frmMain_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        mouseLeftDown = 0
        mouseRightDown = 0
    End Sub
End Class
