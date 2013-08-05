Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Drawing
Public Class TextWriter
    Private fontGraphics As Graphics
    Private xFont As Direct3D.Font
    Private wFont As System.Drawing.Font
    Private MyText As String
    Private theColor As System.Drawing.Color
    'Private spr As Sprite
    Private pos As Point
    Private IsVisible As Boolean

    Public Sub New(ByRef gDevice As Direct3D.Device, ByVal Font As System.Drawing.Font, Optional ByVal text As String = "")
        xFont = New Direct3D.Font(gDevice, Font)
        wFont = Font
        fontGraphics = frmMain.CreateGraphics
        'spr = New Sprite(gDevice)
        If text <> "" Then xFont.PreloadText(text)
        pos = New Point(10, 10)
        theColor = Drawing.Color.Black
        IsVisible = True
    End Sub

    Public Sub Dispose()
        xFont.Dispose()
        xFont = Nothing
        pos = Nothing
    End Sub

    Public Property Color() As Drawing.Color
        Get
            Return theColor
        End Get
        Set(ByVal value As Drawing.Color)
            theColor = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return MyText
        End Get
        Set(ByVal value As String)
            MyText = value
            If Not xFont.Disposed Then xFont.PreloadText(value)
        End Set
    End Property

    Public Function Draw() As Integer
        If IsVisible Then
            Return xFont.DrawText(Nothing, MyText, pos, theColor)
        End If
        Return 1
    End Function

    Public Function Draw(ByVal DrawText As String, ByVal X As Integer, ByVal Y As Integer, ByVal textColor As Drawing.Color) As Integer
        If IsVisible Then
            Return xFont.DrawText(Nothing, DrawText, New Point(X, Y), textColor)
        End If
        Return 1
    End Function

    Public Function GetWidth() As Integer
        Dim fontsize As New SizeF
        If IsVisible Then
            fontsize = fontGraphics.MeasureString(MyText, wFont)
            Return fontsize.Width
        End If
        Return 1
    End Function

    Public Function GetWidth(ByVal DrawText As String) As Integer
        Dim fontsize As New SizeF
        If IsVisible Then
            fontsize = fontGraphics.MeasureString(DrawText, wFont)
            Return fontsize.Width
        End If
        Return 1
    End Function


    Public Property Position() As Point
        Get
            Return pos
        End Get
        Set(ByVal value As Point)
            pos = value
        End Set
    End Property

    Public Property Visible() As Boolean
        Get
            Return IsVisible
        End Get
        Set(ByVal value As Boolean)
            IsVisible = value
        End Set
    End Property
End Class
