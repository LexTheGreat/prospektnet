Imports System
Imports System.IO
Imports System.Collections
Imports System.Xml.Serialization

Class AccountData
    Public Shared Function CreateAccount(ByVal newAccount As Accounts) As Boolean
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

    Public Shared Sub CreateCharacter(ByVal curAccount As Accounts)
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

    Public Shared Sub LoadAccounts()
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

    Public Shared Function VerifyAccount(ByVal Email As String, ByVal Password As String) As Boolean
        Try
            For Each curAccount In Account
                If curAccount.Email = Email And curAccount.Password = Password Then Return True
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Shared Function GetAccountIndex(ByVal Email As String) As Integer
        For index As Integer = 0 To Account.Length
            If Account(index).Email = Email Then Return index
        Next
        Return 0
    End Function

    Public Shared Function GetAccount(ByVal Email As String) As Accounts
        For Each curAccount In Account
            If curAccount.Email = Email Then Return curAccount
        Next
        Return New Accounts
    End Function

    Public Shared Function AccountExists(ByVal Email As String) As Boolean
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
