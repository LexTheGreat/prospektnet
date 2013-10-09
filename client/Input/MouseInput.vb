Public Class MouseInput
    Public Shared Sub MouseDown(e As MouseEventArgs)
        mouseLeftDown = Windows.Forms.MouseButtons.Left
        mouseRightDown = Windows.Forms.MouseButtons.Right
    End Sub

    Public Shared Sub MouseMove(e As MouseEventArgs)
        mouseX = e.X
        mouseY = e.Y
    End Sub

    Public Shared Sub MouseUp(e As MouseEventArgs)
        mouseLeftDown = 0
        mouseRightDown = 0
    End Sub
End Class
