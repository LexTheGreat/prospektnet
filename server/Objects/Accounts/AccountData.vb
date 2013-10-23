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
            Files.WriteXML(pathAccounts & newAccount.Email & ".xml", newAccount)
            AccountCount = AccountCount + 1
            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Accounts.Create)")
            Return False
        End Try
    End Function

    Public Shared Sub CreateCharacter(ByVal curAccount As Accounts)
        Try
            'Serialize object to a file.
            Files.WriteXML(pathAccounts & curAccount.Email & ".xml", curAccount)
            Account(GetAccountIndex(curAccount.Email)) = curAccount
            Exit Sub
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.CreateCharacter)")
            Exit Sub
        End Try
    End Sub

    Public Shared Sub LoadAccounts()
        Dim objAcc As Object, loadAcc As New Accounts
        Dim fileEntries As String()
        Dim fileName As String, i As Integer = 0
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

    Public Shared Sub SaveAccounts()
        Try
            If Directory.Exists(pathAccounts) Then
                For Each acc In Account
                    acc.Save()
                Next
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Data.SaveMaps")
        End Try
    End Sub

    Public Shared Sub SaveAccount(ByRef saccount As Accounts)
        Dim Writer As StreamWriter
        Dim Ser As XmlSerializer
        Try
            'Serialize object to a file.
            Writer = New StreamWriter(pathAccounts & saccount.Email & ".xml")
            Ser = New XmlSerializer(saccount.GetType)
            Ser.Serialize(Writer, saccount)
            Writer.Close()
            Account(GetAccountIndex(saccount.Email)) = saccount
            Exit Sub
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.SaveAccount)")
            Exit Sub
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
