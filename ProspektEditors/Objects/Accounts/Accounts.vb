Imports System.ComponentModel
Imports IHProspekt.Objects
Imports IHProspekt.Core
Imports IHProspekt.Database
Public Class Accounts
    Public Base As AccountBase
    Public Shared Data As New AccountData

    Public Sub New()
        Me.Base = New AccountBase
    End Sub

    Public Sub Save()
        Data.Save(Me.Base)
    End Sub

    Public Sub Load()
        Dim newAccount As New AccountBase

        ' Get object from file
        newAccount = DirectCast(ReadXML(pathAccounts & Me.Base.Email & ".xml", Me.Base), AccountBase)
        Me.Base.Email = newAccount.Email
        Me.Base.Password = newAccount.Password
        Me.Base.Player.Name = newAccount.Player.Name
        Me.Base.Player.Sprite = newAccount.Player.Sprite
        Me.Base.Player.Map = newAccount.Player.Map
        Me.Base.Player.X = newAccount.Player.X
        Me.Base.Player.Y = newAccount.Player.Y
        Me.Base.Player.Dir = newAccount.Player.Dir
    End Sub

    Public Sub SetPlayerDir(ByVal value As Byte)
        Me.Base.Player.Dir = value
    End Sub

    Public Function GetPlayerDir() As Byte
        Return Me.Base.Player.Dir
    End Function

    Public Sub SetPlayerAccess(ByVal value As Byte)
        Me.Base.Player.AccessMode = value
    End Sub

    Public Function GetPlayerAccess() As Byte
        Return Me.Base.Player.AccessMode
    End Function

    ' ProptyGrid Functions
    Public Class DirConverter
        Inherits StringConverter

        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim dirString() As String, i As Integer = 0
            ReDim Preserve dirString(0 To i)
            For i = 0 To DirEnum.COUNT - 1
                ReDim Preserve dirString(0 To i)
                dirString(i) = [Enum].GetName(GetType(DirEnum), i)
            Next

            Return New StandardValuesCollection(dirString)
        End Function
    End Class

    Public Class AccessConverter
        Inherits StringConverter

        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim accessString() As String, i As Integer = 0
            ReDim Preserve accessString(0 To i)
            For i = 0 To ACCESS.COUNT - 1
                ReDim Preserve accessString(0 To i)
                accessString(i) = [Enum].GetName(GetType(ACCESS), i)
            Next

            Return New StandardValuesCollection(accessString)
        End Function
    End Class

    Public Class MapConverter
        Inherits StringConverter

        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim mapString() As String, i As Integer = 0
            ReDim Preserve mapString(0 To i)
            For i = 0 To MapCount
                ReDim Preserve mapString(0 To i)
                mapString(i) = Map(i).Name
            Next

            Return New StandardValuesCollection(mapString)
        End Function
    End Class

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

    <CategoryAttribute("Account"), _
       DisplayName("Email")> _
    Public Property Email() As String
        Get
            Return Me.Base.Email
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Email = value
            End If
        End Set
    End Property

    <CategoryAttribute("Account"), _
       DisplayName("Password")> _
    Public Property Password() As String
        Get
            Return Me.Base.Password
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Password = value
            End If
        End Set
    End Property

    <CategoryAttribute("General"), _
       DisplayName("Name")> _
    Public Property Name() As String
        Get
            Return Me.Base.Player.Name
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Player.Name = value
            End If
        End Set
    End Property

    <TypeConverter(GetType(SpriteConverter)), _
       CategoryAttribute("General"), _
       DisplayName("Sprite")> _
    Public Property Sprite() As Integer
        Get
            Return Me.Base.Player.Sprite
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Player.Sprite = value
            End If
        End Set
    End Property

    <TypeConverter(GetType(MapConverter)), _
       CategoryAttribute("Position"), _
       DisplayName("Map")> _
    Public Property PlayerMap() As String
        Get
            Return Map(Me.Base.Player.Map).Name
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                For i As Integer = 0 To MapCount
                    If Map(i).Name = value Then Me.Base.Player.Map = i
                Next
            End If
        End Set
    End Property

    <CategoryAttribute("Position"), _
       DisplayName("X")> _
    Public Property X() As Integer
        Get
            Return Me.Base.Player.X
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Player.X = value
            End If
        End Set
    End Property

    <CategoryAttribute("Position"), _
       DisplayName("Y")> _
    Public Property Y() As Integer
        Get
            Return Me.Base.Player.Y
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.Base.Player.Y = value
            End If
        End Set
    End Property

    <TypeConverter(GetType(DirConverter)), _
       CategoryAttribute("Position"), _
       DisplayName("Dir")> _
    Public Property PlayerDir() As String
        Get
            Return [Enum].GetName(GetType(DirEnum), Me.Base.Player.Dir)
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Player.Dir = DirectCast([Enum].Parse(GetType(DirEnum), value), DirEnum)
            End If
        End Set
    End Property

    <TypeConverter(GetType(AccessConverter)), _
       CategoryAttribute("Admin"), _
       DisplayName("Access")> _
    Public Property AccessMode() As String
        Get
            Return [Enum].GetName(GetType(ACCESS), Me.Base.Player.AccessMode)
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.Base.Player.AccessMode = DirectCast([Enum].Parse(GetType(ACCESS), value), ACCESS)
            End If
        End Set
    End Property

    <CategoryAttribute("Admin"), _
       DisplayName("Visible")> _
    Public Property Visible() As Boolean
        Get
            Return Me.Base.Player.Visible
        End Get
        Set(value As Boolean)
            If Not IsNothing(Me) Then
                Me.Base.Player.Visible = value
            End If
        End Set
    End Property
End Class
