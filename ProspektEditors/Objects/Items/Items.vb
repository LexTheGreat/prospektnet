Imports System.ComponentModel
Imports Prospekt.Network
Imports IHProspekt.Objects
Imports IHProspekt.Core
Imports IHProspekt.Database
Public Class Items
    ' general
    Public Base As ItemBase
    Public Shared Data As New ItemData

    Public Sub New()
        Me.Base = New ItemBase
    End Sub

    Sub Save()
        Items.Data.Save(Me.Base)
    End Sub

    Sub Load()
        Dim loadItem As New ItemBase
        loadItem = DirectCast(ReadXML(pathItemData & Trim(Me.Base.ID) & ".xml", loadItem), ItemBase)
        Me.Base = loadItem
    End Sub

    Public Sub SetID(ByVal id As Integer)
        Me.Base.ID = id
    End Sub

    Public Sub SetItemType(ByVal value As Byte)
        Me.Base.Type = value
    End Sub

    Public Function GetItemType() As Byte
        Return Me.Base.Type
    End Function

    ' ProptyGrid Functions
    Public Class SpriteConverter
        Inherits Int32Converter

        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim itms() As Integer, i As Integer = 0
            ReDim Preserve itms(0 To i)
            For i = 0 To Graphics.countItem - 1
                ReDim Preserve itms(0 To i)
                itms(i) = i + 1
            Next

            Return New StandardValuesCollection(itms)
        End Function
    End Class

    Public Class TypeConverter
        Inherits StringConverter

        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim dirString() As String, i As Integer = 0
            ReDim Preserve dirString(0 To i)
            For i = 0 To ItemType.COUNT - 1
                ReDim Preserve dirString(0 To i)
                dirString(i) = [Enum].GetName(GetType(ItemType), i)
            Next

            Return New StandardValuesCollection(dirString)
        End Function
    End Class

    <CategoryAttribute("General"), _
       DisplayName("ID")> _
    ReadOnly Property ID() As Integer
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
    Public Property Sprite() As Integer
        Get
            Return Me.Base.Sprite
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Sprite = value
            End If
        End Set
    End Property

    <CategoryAttribute("Data"), _
       DisplayName("Health")> _
    Public Property Health() As Integer
        Get
            Return Me.Base.Health
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Health = value
            End If
        End Set
    End Property

    <TypeConverter(GetType(TypeConverter)), _
       CategoryAttribute("Data"), _
       DisplayName("Type")> _
    Public Property Type() As String
        Get
            Return [Enum].GetName(GetType(ItemType), Me.Base.Type)
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Type = DirectCast([Enum].Parse(GetType(ItemType), value), ItemType)
            End If
        End Set
    End Property

    <CategoryAttribute("Stats"), _
       DisplayName("Strength")> _
    Public Property sStrengh() As Integer
        Get
            Return Me.Base.Stats.Str
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Stats.Str = value
            End If
        End Set
    End Property

    <CategoryAttribute("Stats"), _
       DisplayName("Dexterity")> _
    Public Property sDexterity() As Integer
        Get
            Return Me.Base.Stats.Dex
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Stats.Dex = value
            End If
        End Set
    End Property

    <CategoryAttribute("Stats"), _
       DisplayName("Intelligence")> _
    Public Property sIntelligence() As Integer
        Get
            Return Me.Base.Stats.Int
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Stats.Int = value
            End If
        End Set
    End Property

    <CategoryAttribute("Requirements"), _
       DisplayName("Level")> _
    Public Property rLevel() As Integer
        Get
            Return Me.Base.Reqs.Lvl
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Reqs.Lvl = value
            End If
        End Set
    End Property

    <CategoryAttribute("Requirements"), _
       DisplayName("Strength")> _
    Public Property rStrengh() As Integer
        Get
            Return Me.Base.Reqs.Str
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Reqs.Str = value
            End If
        End Set
    End Property

    <CategoryAttribute("Requirements"), _
       DisplayName("Dexterity")> _
    Public Property rDexterity() As Integer
        Get
            Return Me.Base.Reqs.Dex
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Reqs.Dex = value
            End If
        End Set
    End Property

    <CategoryAttribute("Requirements"), _
       DisplayName("Intelligence")> _
    Public Property rIntelligence() As Integer
        Get
            Return Me.Base.Reqs.Int
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Reqs.Int = value
            End If
        End Set
    End Property
End Class
