Public Class GameWindow
    Private Sub frmMain_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        inMenu = False
        inGame = False
        Verdana = Nothing
        Networking.Dispose()
        Render.Dispose()
        End
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If inGame Then
            GameMain.KeyDown(e.KeyCode)
        End If
        If inMenu Then
            MenuMain.KeyDown(e.KeyCode)
        End If
    End Sub

    Private Sub frmMain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If inMenu And curMenu = MenuEnum.Login Or curMenu = MenuEnum.Register Or curMenu = MenuEnum.Creation Then
            MenuMain.KeyPress(e.KeyChar)
        End If
        If inGame And inChat Then
            GameMain.KeyPress(e.KeyChar)
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
