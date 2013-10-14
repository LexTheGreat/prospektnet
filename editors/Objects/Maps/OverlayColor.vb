Imports System.ComponentModel
<Serializable()> Public Class OverLayColor
    <DisplayName("Alpha")> _
    Public Property Alpha() As Byte
    <DisplayName("Red")> _
    Public Property Red() As Byte
    <DisplayName("Green")> _
    Public Property Green() As Byte
    <DisplayName("Blue")> _
    Public Property Blue() As Byte
    Sub New()
        Me.Alpha = 0
        Me.Red = 255
        Me.Green = 255
        Me.Blue = 255
    End Sub
End Class