﻿Imports System
Imports System.IO
Imports System.Collections
Imports System.Xml.Serialization

Module AccountData
    Public Function CreateAccount(ByVal newAccount As Accounts) As Boolean
        Try
            ' Add player to accounts array
            ReDim Preserve Account(0 To AccountCount + 1)
            Account(AccountCount) = New Accounts
            Account(AccountCount) = newAccount
            'Serialize object to a file.
            Dim Writer As New StreamWriter(pathAccounts & newAccount.Email & ".xml")
            Dim ser As New XmlSerializer(newAccount.GetType)
            ser.Serialize(Writer, newAccount)
            Writer.Close()
            AccountCount = AccountCount + 1
            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Accounts.Create)")
            Return False
        End Try
    End Function

    Public Sub CreateCharacter(ByVal curAccount As Accounts)
        Dim Writer As StreamWriter
        Dim Ser As XmlSerializer
        Try
            'Serialize object to a file.
            Writer = New StreamWriter(pathAccounts & curAccount.Email & ".xml")
            Ser = New XmlSerializer(curAccount.GetType)
            Ser.Serialize(Writer, curAccount)
            Writer.Close()
            Account(GetAccountIndex(curAccount.Email)) = curAccount
            Exit Sub
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.CreateCharacter)")
            Exit Sub
        End Try
    End Sub

    Public Sub LoadAccounts()
        Dim Reader As StreamReader
        Dim Ser As XmlSerializer
        Try
            ReDim Preserve Account(0 To 1)
            Account(0) = New Accounts
            If Directory.Exists(pathAccounts) Then
                Dim fileEntries As String() = Directory.GetFiles(pathAccounts)
                Dim fileName As String, i As Integer = 0, loadAcc As New Accounts
                For Each fileName In fileEntries
                    'Deserialize file to object.
                    Reader = New StreamReader(fileName)
                    Ser = New XmlSerializer(loadAcc.GetType)
                    loadAcc = Ser.Deserialize(Reader)
                    Reader.Close()
                    AccountCount = i + 1
                    ReDim Preserve Account(0 To AccountCount)
                    Account(i) = loadAcc
                    i = AccountCount
                Next fileName
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.LoadAccounts")
        End Try
    End Sub

    Public Sub SaveOnlineAccounts()
        Dim index As Integer
        Dim acc As New Accounts
        Dim Writer As StreamWriter
        Dim Ser As XmlSerializer

        For index = 1 To PlayerHighIndex
            If Networking.IsPlaying(index) Then
                Try
                    acc = Account(GetAccountPlayerIndex(Player(index).Name))
                    'Serialize object to a file.
                    Writer = New StreamWriter(pathAccounts & acc.Email & ".xml")
                    Ser = New XmlSerializer(acc.GetType)
                    Ser.Serialize(Writer, acc)
                    Writer.Close()
                    Account(GetAccountPlayerIndex(Player(index).Name)).Player = Player(index)
                Catch ex As Exception
                    Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.SaveOnlineAccounts)")
                    Continue For
                End Try
            End If
        Next index
    End Sub

    Public Sub SavePlayer(ByVal player As Players)
        Dim acc As New Accounts
        Dim Writer As StreamWriter
        Dim Ser As XmlSerializer
        Try
            acc = Account(GetAccountPlayerIndex(player.Name))
            acc.Player = player
            'Serialize object to a file.
            Writer = New StreamWriter(pathAccounts & acc.Email & ".xml")
            Ser = New XmlSerializer(acc.GetType)
            Ser.Serialize(Writer, acc)
            Writer.Close()
            Account(GetAccountPlayerIndex(player.Name)) = acc
            Exit Sub
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.SavePlayer)")
            Exit Sub
        End Try
    End Sub

    Public Function VerifyAccount(ByVal Email As String, ByVal Password As String) As Boolean
        Try
            For Each curAccount In Account
                If curAccount.Email = Email And curAccount.Password = Password Then Return True
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Function GetAccountIndex(ByVal Email As String) As Integer
        For index As Integer = 0 To Account.Length
            If Account(index).Email = Email Then Return index
        Next
        Return 0
    End Function

    Public Function GetAccount(ByVal Email As String) As Accounts
        For Each curAccount In Account
            If curAccount.Email = Email Then Return curAccount
        Next
        Return New Accounts
    End Function

    Public Function GetAccountPlayerIndex(ByVal Name As String) As Integer
        For index As Integer = 0 To Account.Length
            If Account(index).Player.Name = Name Then Return index
        Next
        Return 0
    End Function

    Public Function GetAccountPlayer(ByVal Name As String) As Players
        Dim player As New Players
        For Each curAccount In Account
            If curAccount.Player.Name = Name Then Return curAccount.Player
        Next
        Return player
    End Function

    Public Function AccountExists(ByVal Email As String) As Boolean
        Try
            For Each curAccount In Account
                If curAccount.Email = Email Then Return True
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Function CharacterExists(ByVal Name As String) As Boolean
        Try
            For Each curAccount In Account
                If curAccount.Player.Name = Name Then Return True
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Function HasCharacter(ByVal index As Integer) As Boolean
        Try
            If Not Account(index).Player.Name = vbNullString Then Return True
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Function PlayerOnTile(ByVal X As Integer, ByVal Y As Integer) As Boolean
        For i = 1 To PlayerHighIndex
            If Not IsNothing(Player(i)) Then
                If (Player(i).X = X And Player(i).Y = Y) Then Return True
            End If
        Next
        Return False
    End Function
End Module