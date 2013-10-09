﻿Public Class GameWindow
    Private Sub frmMain_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        inMenu = False
        inGame = False
        Verdana = Nothing
        Networking.Dispose()
        Render.Dispose()
        End
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        KeyboardInput.KeyDown(e)
    End Sub

    Private Sub frmMain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        KeyboardInput.KeyPress(e)
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True) 'Do not draw forms background
        Me.Refresh()
    End Sub

    Private Sub frmMain_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        MouseInput.MouseDown(e)
    End Sub

    Private Sub frmMain_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        MouseInput.MouseMove(e)
    End Sub

    Private Sub frmMain_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        MouseInput.MouseUp(e)
    End Sub
End Class