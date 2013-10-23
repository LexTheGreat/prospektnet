﻿Imports System
Imports System.IO
Imports System.Collections
Imports System.Xml.Serialization

Class AccountData

    Public Shared Sub LoadAccounts()
        Dim objAcc As Object, loadAcc As New Accounts
        Dim fileEntries As String()
        Dim fileName As String
        Try
            ReDim Preserve Account(0 To 1)
            Account(0) = New Accounts
            If Directory.Exists(pathAccounts) Then
                fileEntries = Directory.GetFiles(pathAccounts)
                For Each fileName In fileEntries
                    ' Get object from file
                    objAcc = Files.ReadXML(fileName, loadAcc)
                    If IsNothing(objAcc) Then objAcc = New Accounts
                    ' Convert object to loadAcc
                    loadAcc = CType(objAcc, Accounts)
                    ReDim Preserve Account(0 To AccountCount)
                    Account(AccountCount) = loadAcc
                    AccountCount = AccountCount + 1
                Next fileName
                AccountCount = AccountCount - 1
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.LoadAccounts")
        End Try
    End Sub

    Public Shared Sub SaveAccounts()
        If Directory.Exists(pathAccounts) Then
            For Each plyr In Account
                plyr.Save()
            Next
        End If
    End Sub

    Public Shared Sub NewAccount()
        Dim newAccount As New Accounts, i As Integer = GetNextAccountIndex()
        newAccount.Email = i & "@email.com"
        ReDim Preserve Account(0 To i)
        Account(i) = newAccount
        SaveAccounts()
    End Sub

    Public Shared Function GetAccountIndex(ByVal Email As String) As Integer
        For index As Integer = 0 To AccountCount
            If Account(index).Email = Email Then Return index
        Next
        Return 0
    End Function

    Public Shared Function GetAccount(ByVal Email As String) As Accounts
        For Each plyr In Account
            If plyr.Email = Email Then Return plyr
        Next
        Return New Accounts
    End Function

    Public Shared Function GetNextAccountIndex() As Integer
        If Not IsNothing(Account) Then Return Account.Length
        Return 0
    End Function

    Public Shared Function AccountExists(ByVal LoginEmail As String) As Boolean
        Dim Filename As String
        Filename = pathAccounts & Trim(LoginEmail) & ".xml"

        If Files.Exists(Filename) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
