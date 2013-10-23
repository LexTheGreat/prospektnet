Imports System.ComponentModel
Public Class Accounts
    ' account
    Private mEmail As String
    Private mPassword As String
    ' general
    Private mName As String
    Private mSprite As Integer
    ' location
    Private mX As Integer
    Private mY As Integer
    Private mDir As Byte
    Private mMapIndex As Integer
    ' Admin values
    Private mAccessMode As Byte
    Private mVisible As Boolean

    Public Sub New()
        Me.mEmail = "Email@email.com"
        Me.mPassword = "password"
        Me.mName = "New Character"
        Me.mSprite = 1
        Me.mMapIndex = 0
        Me.mX = 15
        Me.mY = 15
        Me.mDir = DirEnum.Down
        Me.mAccessMode = ACCESS.NONE
        Me.mVisible = True
    End Sub

    Public Sub Save()
        Files.WriteXML(pathAccounts & Me.mEmail & ".xml", Me)
    End Sub

    Public Sub Load()
        Dim objAccount As Object, newAccount As New Accounts

        ' Get object from file
        objAccount = Files.ReadXML(pathAccounts & Me.mEmail & ".xml", Me)
        If IsNothing(objAccount) Then objAccount = New Accounts()
        ' Convert object to newConfig
        newAccount = CType(objAccount, Accounts)
        Me.mEmail = newAccount.mEmail
        Me.mPassword = newAccount.mPassword
        Me.mName = newAccount.mName
        Me.mSprite = newAccount.mSprite
        Me.mMapIndex = newAccount.mMapIndex
        Me.mX = newAccount.mX
        Me.mY = newAccount.mY
        Me.mDir = newAccount.mDir
    End Sub

    Public Sub SetPlayerDir(ByVal value As Byte)
        Me.mDir = value
    End Sub

    Public Function GetPlayerDir() As Byte
        Return Me.mDir
    End Function

    Public Sub SetPlayerAccess(ByVal value As Byte)
        Me.mAccessMode = value
    End Sub

    Public Function GetPlayerAccess() As Byte
        Return Me.mAccessMode
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

    <CategoryAttribute("Account"), _
       DisplayName("Email")> _
    Public Property Email() As String
        Get
            Return Me.mEmail
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.mEmail = value
            End If
        End Set
    End Property

    <CategoryAttribute("Account"), _
       DisplayName("Password")> _
    Public Property Password() As String
        Get
            Return Me.mPassword
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.mPassword = value
            End If
        End Set
    End Property

    <CategoryAttribute("General"), _
       DisplayName("Name")> _
    Public Property Name() As String
        Get
            Return Me.mName
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.mName = value
            End If
        End Set
    End Property

    <CategoryAttribute("General"), _
       DisplayName("Sprite")> _
    Public Property Sprite() As Integer
        Get
            Return Me.mSprite
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mSprite = value
            End If
        End Set
    End Property

    <CategoryAttribute("Position"), _
       DisplayName("Map")> _
    Public Property Map() As Integer
        Get
            Return Me.mMapIndex
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mMapIndex = value
            End If
        End Set
    End Property

    <CategoryAttribute("Position"), _
       DisplayName("X")> _
    Public Property X() As Integer
        Get
            Return Me.mX
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mX = value
            End If
        End Set
    End Property

    <CategoryAttribute("Position"), _
       DisplayName("Y")> _
    Public Property Y() As Integer
        Get
            Return Me.mY
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mY = value
            End If
        End Set
    End Property

    <TypeConverter(GetType(DirConverter)), _
       CategoryAttribute("Position"), _
       DisplayName("Dir")> _
    Public Property PlayerDir() As String
        Get
            Return [Enum].GetName(GetType(DirEnum), Me.mDir)
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.mDir = DirectCast([Enum].Parse(GetType(DirEnum), value), DirEnum)
            End If
        End Set
    End Property
    <TypeConverter(GetType(AccessConverter)), _
       CategoryAttribute("Admin"), _
       DisplayName("Access")> _
    Public Property AccessMode() As String
        Get
            Return [Enum].GetName(GetType(ACCESS), Me.mAccessMode)
        End Get
        Set(value As String)
            If Not IsNothing(Me) Then
                Me.mAccessMode = DirectCast([Enum].Parse(GetType(ACCESS), value), ACCESS)
            End If
        End Set
    End Property

    <CategoryAttribute("Admin"), _
       DisplayName("Visible")> _
    Public Property Visible() As Boolean
        Get
            Return Me.mVisible
        End Get
        Set(value As Boolean)
            If Not IsNothing(Me) Then
                Me.mVisible = value
            End If
        End Set
    End Property
End Class
