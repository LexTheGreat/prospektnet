Namespace Input.Mouse
    Public Module MouseInput
        Public Sub MouseDown(e As MouseEventArgs)
            mouseLeftDown = Windows.Forms.MouseButtons.Left
            mouseRightDown = Windows.Forms.MouseButtons.Right
        End Sub

        Public Sub MouseMove(e As MouseEventArgs)
            mouseX = e.X
            mouseY = e.Y
        End Sub

        Public Sub MouseUp(e As MouseEventArgs)
            mouseLeftDown = 0
            mouseRightDown = 0
        End Sub
    End Module
End Namespace