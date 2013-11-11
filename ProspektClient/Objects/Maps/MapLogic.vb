Public Class MapLogic
    Public Function ConvertX(ByVal X As Integer) As Integer
        Return X - (TileView.Left * picX) - Camera.X
    End Function

    Public Function ConvertY(ByVal Y As Integer) As Integer
        Return Y - (TileView.Top * picY) - Camera.Y
    End Function

    Public Function IsValidPoint(ByVal X As Integer, ByVal Y As Integer) As Boolean
        If X < 0 Then Return False
        If Y < 0 Then Return False
        If X > Map.MaxX Then Return False
        If Y > Map.MaxY Then Return False
        Return True
    End Function

    Public Sub UpdateCamera()
        Dim offsetX As Integer, offsetY As Integer, StartX As Integer, StartY As Integer, EndX As Integer, EndY As Integer

        ' Center of screen
        offsetX = Player(MyIndex).XOffset + picX
        offsetY = Player(MyIndex).YOffset + picY

        ' Start screen for rendering
        StartX = Player(MyIndex).X - ((maxX + 1) \ 2) - 1
        StartY = Player(MyIndex).Y - ((maxY + 1) \ 2) - 1

        If StartX < 0 Then
            offsetX = 0

            If StartX = -1 Then
                If Player(MyIndex).XOffset > 0 Then
                    offsetX = Player(MyIndex).XOffset
                End If
            End If

            StartX = 0
        End If

        If StartY < 0 Then
            offsetY = 0

            If StartY = -1 Then
                If Player(MyIndex).YOffset > 0 Then
                    offsetY = Player(MyIndex).YOffset
                End If
            End If

            StartY = 0
        End If

        EndX = StartX + (maxX + 1) + 1
        EndY = StartY + (maxY + 1) + 1

        If EndX > Map.MaxX Then
            offsetX = 32

            If EndX = Map.MaxX Then
                If Player(MyIndex).XOffset < 0 Then
                    offsetX = Player(MyIndex).XOffset + picX
                End If
            End If

            EndX = Map.MaxX
            StartX = EndX - maxX - 1
        End If

        If EndY > Map.MaxY Then
            offsetY = 32

            If EndY = Map.MaxY Then
                If Player(MyIndex).YOffset < 0 Then
                    offsetY = Player(MyIndex).YOffset + picY
                End If
            End If

            EndY = Map.MaxY
            StartY = EndY - maxY - 1
        End If

        With TileView
            .Y = StartY
            .Height = EndY
            .X = StartX
            .Width = EndX
        End With

        With Camera
            .Y = offsetY
            .Height = .Y + screenY
            .X = offsetX
            .Width = .X + screenX
        End With
    End Sub
End Class
