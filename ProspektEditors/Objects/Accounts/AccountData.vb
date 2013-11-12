Imports System.IO
Imports IHProspekt.Objects
Imports IHProspekt.Database
Public Class AccountData
    Public Sub Save(ByVal SaveAccount As AccountBase)
        Using File As New Files(pathAccounts & SaveAccount.Email & ".xml", SaveAccount)
            File.WriteXML()
        End Using
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
                    Using File As New Files(fileName, loadAcc)
                        loadAcc = DirectCast(File.ReadXML, AccountBase)
                    End Using
                    AccountCount = i + 1
                    ReDim Preserve Account(0 To AccountCount)
                    Account(AccountCount) = New Accounts
                    Account(AccountCount).Base = loadAcc
                    i = AccountCount
                Next fileName
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Accounts.Data.LoadAccounts")
        End Try
    End Sub

    Public Sub SaveAccounts()
        If Directory.Exists(pathAccounts) Then
            For I As Integer = 1 To AccountCount
                Account(I).Save()
            Next
        End If
    End Sub

    Public Sub NewAccount()
        Dim newAccount As New Accounts, i As Integer = GetNextAccountIndex()
        newAccount.Email = i & "@email.com"
        ReDim Preserve Account(0 To i)
        Account(i) = newAccount
        SaveAccounts()
    End Sub

    Public Function GetAccountIndex(ByVal Email As String) As Integer
        For index As Integer = 1 To AccountCount
            If Account(index).Email = Email Then Return index
        Next
        Return 0
    End Function

    Public Function GetAccount(ByVal Email As String) As Accounts
        For Each plyr In Account
            If plyr.Email = Email Then Return plyr
        Next
        Return New Accounts
    End Function

    Public Function GetNextAccountIndex() As Integer
        If Not IsNothing(Account) Then Return Account.Length
        Return 0
    End Function

    Public Function AccountExists(ByVal LoginEmail As String) As Boolean
        Dim Filename As String
        Filename = pathAccounts & Trim(LoginEmail) & ".xml"

        If System.IO.File.Exists(Filename) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
