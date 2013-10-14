Imports System
Imports System.IO
Imports System.Collections
Imports System.Xml.Serialization

Class AccountData

    Public Shared Sub LoadAccounts()
        If Directory.Exists(pathAccounts) Then
            Dim fileEntries As String() = Directory.GetFiles(pathAccounts, "*.xml")
            Dim i As Integer = 0
            ReDim Account(0 To 0)
            Account(0) = New Accounts
            For Each fileName In fileEntries
                ReDim Preserve Account(0 To i)
                Account(i) = New Accounts
                Account(i).Email = fileName.Replace(pathAccounts, vbNullString).Replace(".xml", vbNullString)
                Account(i).Load()
                i = i + 1
            Next fileName
        End If
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
        For index As Integer = 0 To Account.Length
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
        Filename = pathAccounts & Trim(LoginEmail) & ".bin"

        If Files.Exists(Filename) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
