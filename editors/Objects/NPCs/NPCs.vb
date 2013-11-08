Imports System.ComponentModel
Imports Prospekt.Network
Imports IHProspekt.Core
Public Class NPCs
    ' general
    Public Base As NPCBase
    Public Shared Data As New NPCData
    ' non-saved values
    Private mIndex As Integer

    Public Sub New()
        Me.Base = New NPCBase
    End Sub

    Sub Save()
        NPCs.Data.Save(Me.Base)
    End Sub

    Sub Load()
        Dim loadNpc As New NPCBase
        loadNpc = DirectCast(Files.ReadXML(pathNPCs & Trim(Me.Base.ID) & ".xml", loadNpc), NPCBase)
        Me.Base.Name = loadNpc.Name
        Me.Base.Sprite = loadNpc.Sprite
    End Sub

    Public Sub SetID(ByVal id As Integer)
        Me.Base.ID = id
    End Sub

    Public Class SpriteConverter
        Inherits StringConverter

        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim spriteString() As String, i As Integer = 0
            ReDim Preserve spriteString(0 To i)
            For i = 0 To Graphics.countSprite - 1
                ReDim Preserve spriteString(0 To i)
                spriteString(i) = i + 1
            Next

            Return New StandardValuesCollection(spriteString)
        End Function
    End Class

    <CategoryAttribute("General"), _
       DisplayName("ID")> _
    ReadOnly Property ID() As String
        Get
            Return Me.Base.ID
        End Get
    End Property

    <CategoryAttribute("General"), _
       DisplayName("Name")> _
    Public Property Name() As String
        Get
            Return Me.Base.Name
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Name = value
            End If
        End Set
    End Property

    <TypeConverter(GetType(SpriteConverter)), _
       CategoryAttribute("General"), _
       DisplayName("Sprite")> _
    Public Property Sprite() As String
        Get
            Return Me.Base.Sprite
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Sprite = value
            End If
        End Set
    End Property

    Public WriteOnly Property Index As Integer
        Set(value As Integer)
            Me.mIndex = value
        End Set
    End Property
End Class
