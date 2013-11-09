﻿Imports System.ComponentModel
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
        Me.Base = loadNpc
    End Sub

    Public Sub SetID(ByVal id As Integer)
        Me.Base.ID = id
    End Sub

    Public Class SpriteConverter
        Inherits Int32Converter

        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim sprites() As Integer, i As Integer = 0
            ReDim Preserve sprites(0 To i)
            For i = 0 To Graphics.countSprite - 1
                ReDim Preserve sprites(0 To i)
                sprites(i) = i + 1
            Next

            Return New StandardValuesCollection(sprites)
        End Function
    End Class

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

    <CategoryAttribute("General"), _
       DisplayName("ID")> _
    ReadOnly Property ID() As Integer
        Get
            Return Me.Base.ID
        End Get
    End Property

    <CategoryAttribute("Data"), _
       DisplayName("Level")> _
    Public Property Level() As Integer
        Get
            Return Me.Base.Level
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Level = value
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

    Public WriteOnly Property Index As Integer
        Set(value As Integer)
            Me.mIndex = value
        End Set
    End Property
End Class
