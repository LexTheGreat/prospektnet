
Public Class frmMain
    Private Sub frmMain_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        inMenu = False
        inGame = False
        Verdana = Nothing
        Silkscreen = Nothing
        Player = Nothing
        DestroyTCP()
        DestroySFML()
        End
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                If inMenu Then
                    If curMenu <> MenuEnum.Main Then
                        ' Button sound
                        playSound("button.ogg")
                        curMenu = MenuEnum.Main
                    Else
                        Me.Close()
                    End If
                End If
            Case Keys.Back
                If inMenu And curMenu = MenuEnum.Login And Len(sUser) > 0 Then sUser = Mid(sUser, 1, Len(sUser) - 1)
            Case Keys.Space
                If inMenu Then
                    If faderState < 2 Then
                        faderState = 2
                        faderAlpha = 0
                    End If
                End If
            Case Keys.Return
                If inMenu Then
                    If curMenu = MenuEnum.Login Then
                        If ConnectToServer() Then
                            SendLogin(sUser)
                        Else
                            MsgBox("Server is offline")
                        End If
                    End If
                End If
        End Select
    End Sub

    Private Sub frmMain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If inMenu And curMenu = MenuEnum.Login Then
            If Not GetKeyState(Keys.Back) And Not GetKeyState(Keys.Return) And Not GetKeyState(Keys.Tab) And Not GetKeyState(Keys.Escape) Then
                sUser = sUser & e.KeyChar
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
