Imports System.IO
Imports IHProspekt.Objects
Imports IHProspekt.Database
Imports IHProspekt.Core
Public Class AccountData
    Public Sub Save(ByVal SaveAccount As AccountBase)
        Using File As New Files(pathAccounts & SaveAccount.Email & ".xml", SaveAccount)
            File.WriteXML()
        End Using
    End Sub

    Public Sub LoadAll()
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
            ErrHandler.HandleException(ex, ErrorHandler.ErrorLevels.High)
        End Try
    End Sub

    Public Sub SaveAll()
        If Directory.Exists(pathAccounts) Then
            For Each acc In Account
                acc.Save()
            Next
        End If
    End Sub

    Public Sub NewAccount()
        Dim newAccount As New Accounts, i As Integer = GetNextAccountIndex()
        newAccount.Email = i & "@email.com"
        ReDim Preserve Account(0 To i)
        Account(i) = newAccount
        SaveAll()
    End Sub

    Public Function GetAccountIndex(ByVal Email As String) As Integer
        Dim i As Integer = 0
        For Each acc In Account
            If acc.Email = Email Then Return i
            i = i + 1
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
