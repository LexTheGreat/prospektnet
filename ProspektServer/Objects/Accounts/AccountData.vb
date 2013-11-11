Imports System.IO
Imports IHProspekt.Objects
Imports IHProspekt.Database
Public Class AccountData
    Public Function CreateAccount(ByVal newAccount As AccountBase) As Boolean
        Try
            ' Add player to accounts array
            ReDim Preserve Account(0 To AccountCount + 1)
            Account(AccountCount) = New Accounts
            Account(AccountCount).Base = newAccount
            'Serialize object to a file.
            WriteXML(pathAccounts & newAccount.Email & ".xml", newAccount)
            AccountCount = AccountCount + 1
            Return True
        Catch ex As Exception
            Server.Writeline("Error: " & ex.ToString & " (In: Accounts.Create)")
            Return False
        End Try
    End Function

    Public Sub CreateCharacter(ByVal curAccount As AccountBase)
        Try
            'Serialize object to a file.
            WriteXML(pathAccounts & curAccount.Email & ".xml", curAccount)
            Account(GetAccountIndex(curAccount.Email)).Base = curAccount
            Exit Sub
        Catch ex As Exception
            Server.Writeline("Error: " & ex.ToString & " (In: Accounts.Data.CreateCharacter)")
            Exit Sub
        End Try
    End Sub

    Public Sub LoadAccounts()
        Dim loadAcc As New AccountBase
        Dim fileEntries As String()
        Dim fileName As String, i As Integer = 0
        Try
            ReDim Preserve Account(0 To 1)
            Account(0) = New Accounts
            If Directory.Exists(pathAccounts) Then
                fileEntries = Directory.GetFiles(pathAccounts)
                For Each fileName In fileEntries
                    loadAcc = DirectCast(ReadXML(fileName, loadAcc), AccountBase)
                    AccountCount = i + 1
                    ReDim Preserve Account(0 To AccountCount)
                    Account(AccountCount) = New Accounts
                    Account(AccountCount).Base = loadAcc
                    i = AccountCount
                Next fileName
            End If
        Catch ex As Exception
            Server.Writeline("Error: " & ex.ToString & " (In: Accounts.Data.LoadAccounts")
        End Try
    End Sub

    Public Sub SaveAccounts()
        Try
            If Directory.Exists(pathAccounts) Then
                For Each acc In Account
                    acc.Save()
                Next
            End If
        Catch ex As Exception
            Server.Writeline("Error: " & ex.ToString & " (In: Accounts.Data.SaveAccounts")
        End Try
    End Sub

    Public Sub SaveAccount(ByRef saccount As AccountBase)
        Try
            WriteXML(pathAccounts & saccount.Email & ".xml", saccount)
            Account(GetAccountIndex(saccount.Email)).Base = saccount
            Exit Sub
        Catch ex As Exception
            Server.Writeline("Error: " & ex.ToString & " (In: Accounts.Data.SaveAccount)")
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
End Class
