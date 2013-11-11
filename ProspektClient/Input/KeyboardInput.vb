Namespace Input.Keyboard
    Public Module KeyboardInput
        Public Sub KeyDown(e As KeyEventArgs)
            If inGame Then
                GameMain.KeyDown(e.KeyCode)
            End If
            If inMenu Then
                MenuMain.KeyDown(e.KeyCode)
            End If
        End Sub

        Public Sub KeyPress(e As KeyPressEventArgs)
            If inMenu And curMenu = MenuScene.MenuEnum.Login Or curMenu = MenuScene.MenuEnum.Register Or curMenu = MenuScene.MenuEnum.Creation Then
                MenuMain.KeyPress(e.KeyChar)
            End If
            If inGame And inChat Then
                GameMain.KeyPress(e.KeyChar)
            End If
        End Sub

        Public Function GetKeyState(ByVal key As Integer) As Boolean
            Dim s As Short
            s = GetAsyncKeyState(key)
            If s = 0 Then Return False
            Return True
        End Function
    End Module
End Namespace
