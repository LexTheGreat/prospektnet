Imports SFML.Graphics
Public Class TextWriter
    Private xFont As Font
    Private xText As Text
    Private fontName As String
    Private IsVisible As Boolean

    Public Sub New(ByVal Filename As String)
        xFont = New SFML.Graphics.Font(Filename)
        IsVisible = True
    End Sub

    Public Sub Dispose()
        xFont.Dispose()
        xFont = Nothing
        xText.Dispose()
        xText = Nothing
    End Sub

    Public Sub Draw(ByVal DrawText As String, ByVal X As Integer, ByVal Y As Integer, ByVal textColor As Color, Optional ByVal Size As Integer = 12)
        If IsVisible Then
            xText = New Text(DrawText, xFont, Size)
            xText.Color = New Color(textColor)
            xText.Position = New SFML.Window.Vector2f(X, Y)
            xText.Draw(SfmlWindow, RenderStates.Default)
        End If
    End Sub

    Public Function GetWidth(ByVal DrawText As String, Optional ByVal Size As Integer = 12) As Integer
        Dim tempText As Text
        If IsVisible Then
            tempText = New Text(DrawText, xFont, Size)
            Return tempText.GetLocalBounds.Width
        End If
        Return 1
    End Function

    Public Property Visible() As Boolean
        Get
            Return IsVisible
        End Get
        Set(ByVal value As Boolean)
            IsVisible = value
        End Set
    End Property
End Class
