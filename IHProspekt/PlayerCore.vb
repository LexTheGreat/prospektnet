﻿Namespace Core
    Public Class PlayerBase
        Public Property Name As String
        Public Property Sprite As Integer
        ' location
        Public Property X As Integer
        Public Property Y As Integer
        Public Property Dir As Integer
        Public Property Map As Integer
        ' Admin values
        Public Property AccessMode As Byte
        Public Property Visible As Boolean

        Public Sub New()
            Me.Name = vbNullString
            Me.Sprite = 1
            Me.Y = 15
            Me.X = 10
            Me.Dir = 1
            Me.AccessMode = ACCESS.NONE
            Me.Visible = True
            Me.Map = 0
        End Sub
    End Class
End Namespace