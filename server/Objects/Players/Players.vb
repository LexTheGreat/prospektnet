Imports System.IO
Imports System.Xml.Serialization

Public Class Players
    ' general
    Private mName As String
    Private mSprite As Integer
    ' location
    Private mX As Integer
    Private mY As Integer
    Private mDir As Byte
    ' Guild
    Private mGuildID As Integer
    ' Admin values
    Private mAccessMode As Byte
    Private mVisible As Boolean
    ' non-saved values
    Private mXOffset As Integer, mYOffset As Integer
    Private mMoving As Boolean = True
    Private mPlayerStep As Byte
    Private mIsPlaying As Boolean = False
    Private mPartyID As Integer = -1

    Public Sub New()
        Me.mName = vbNullString
        Me.mSprite = 1
        Me.mY = 15
        Me.mX = 10
        Me.mDir = 1
        Me.mGuildID = -1
        Me.mAccessMode = ACCESS.NONE
        Me.mVisible = True
    End Sub

    ' sub routines and functions
    Public Function Create(ByVal PName As String) As Boolean
        Try
            Dim newPlayer As New Players()
            newPlayer.Name = PName
            ' Update accounts array
            Account(PlayerData.GetPlayerIndex(PName)).Player = newPlayer
            'Serialize object to a file.
            Dim Writer As New StreamWriter(pathAccounts & PName & ".xml")
            Dim ser As New XmlSerializer(newPlayer.GetType)
            ser.Serialize(Writer, newPlayer)
            Writer.Close()
            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Players.Create)")
            Return False
        End Try
    End Function

    Public Function Load(ByVal PName As String) As Boolean
        Try
            Dim newPlayer As New Players()
            newPlayer = PlayerData.GetPlayer(PName)
            'Check if account is valid
            If newPlayer.Name = vbNullString Then Return False
            ' Load player
            Me.mName = newPlayer.Name
            Me.mSprite = newPlayer.Sprite
            Me.mY = newPlayer.Y
            Me.mX = newPlayer.X
            Me.mDir = newPlayer.Dir
            Me.mGuildID = newPlayer.GuildID
            Me.mAccessMode = newPlayer.AccessMode
            Me.mVisible = newPlayer.Visible
            ' Add player to accounts array
            ReDim Preserve Account(0 To AccountCount + 1)
            Account(AccountCount) = New Accounts
            Account(AccountCount).Email = Account(PlayerData.GetPlayerIndex(PName)).Email
            Account(AccountCount).Password = Account(PlayerData.GetPlayerIndex(PName)).Password
            Account(AccountCount).Player = newPlayer
            AccountCount = AccountCount + 1
            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Players.Load)")
            Return False
        End Try
    End Function

    Public Function Save() As Boolean
        If Me.Name = vbNullString Then Return False
        PlayerData.SavePlayer(Me)
        Return True
    End Function

    ' Saved variables
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

    Public Property Dir() As Integer
        Get
            Return Me.mDir
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mDir = value
            End If
        End Set
    End Property

    Public Property GuildID() As Integer
        Get
            Return Me.mGuildID
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mGuildID = value
            End If
        End Set
    End Property

    Public Property AccessMode() As Integer
        Get
            Return Me.mAccessMode
        End Get
        Set(value As Integer)
            If Not IsNothing(Me) Then
                Me.mAccessMode = value
            End If
        End Set
    End Property

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

    ' Non Saved
    Public Sub SetIsPlaying(value As Boolean)
        If Not IsNothing(Me) Then
            Me.mIsPlaying = value
        End If
    End Sub

    Public Function GetIsPlaying() As Boolean
        If Not IsNothing(Me) Then
            Return Me.mIsPlaying
        End If
        Return False
    End Function

    Public Sub SetMoving(value As Boolean)
        If Not IsNothing(Me) Then
            If Me.mIsPlaying Then
                Me.mMoving = value
            End If
        End If
    End Sub

    Public Function GetMoving() As Boolean
        If Not IsNothing(Me) Then
            If Me.mIsPlaying Then
                Return Me.mMoving
            End If
        End If
        Return False
    End Function

    Public Sub SetPosition(value As Integer())
        If Not IsNothing(Me) Then
            If Me.mIsPlaying Then
                Me.mX = value(0)
                Me.mY = value(1)
            End If
        End If
    End Sub

    Public Sub SetDir(value As Integer)
        If Not IsNothing(Me) Then
            If Me.mIsPlaying Then
                Me.mDir = value
            End If
        End If
    End Sub

    Public Sub SetParty(value As Integer)
        If Not IsNothing(Me) Then
            If Me.mIsPlaying Then
                Me.mPartyID = value
            End If
        End If
    End Sub

    Public Function GetParty() As Integer
        If Not IsNothing(Me) Then
            If Me.mIsPlaying Then
                Return Me.mPartyID
            End If
        End If
        Return -1
    End Function
End Class
