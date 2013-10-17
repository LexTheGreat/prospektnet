Imports System
Imports System.IO
Imports System.Collections
Imports System.Xml.Serialization

Public Class PlayerData
    Public Shared Sub SaveOnlinePlayers()
        Dim index As Integer
        Dim acc As New Accounts
        Dim Writer As StreamWriter
        Dim Ser As XmlSerializer

        For index = 1 To PlayerCount
            If PlayerLogic.IsPlaying(index) Then
                Try
                    acc = Account(PlayerData.GetPlayerIndex(Player(index).Name))
                    'Serialize object to a file.
                    Writer = New StreamWriter(pathAccounts & acc.Email & ".xml")
                    Ser = New XmlSerializer(acc.GetType)
                    Ser.Serialize(Writer, acc)
                    Writer.Close()
                    Account(PlayerData.GetPlayerIndex(Player(index).Name)).Player = Player(index)
                Catch ex As Exception
                    Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.SaveOnlineAccounts)")
                    Continue For
                End Try
            End If
        Next index
    End Sub

    Public Shared Sub SavePlayer(ByVal player As Players)
        Dim acc As New Accounts
        Try
            acc = Account(PlayerData.GetPlayerIndex(player.Name))
            acc.Player = player
            'Serialize object to a file.
            Files.Write(pathAccounts & acc.Email & ".xml", acc)
            Account(PlayerData.GetPlayerIndex(player.Name)) = acc
            Exit Sub
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: AccountData.SavePlayer)")
            Exit Sub
        End Try
    End Sub

    Public Shared Function PlayerExists(ByVal Name As String) As Boolean
        Try
            For Each curAccount In Account
                If curAccount.Player.Name = Name Then Return True
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Shared Function HasPlayer(ByVal index As Integer) As Boolean
        Try
            If Not Account(index).Player.Name = vbNullString Then Return True
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Shared Function GetPlayer(ByVal Name As String) As Players
        Dim player As New Players
        For Each curAccount In Account
            If curAccount.Player.Name = Name Then Return curAccount.Player
        Next
        Return player
    End Function

    Public Shared Function GetPlayerIndex(ByVal Name As String) As Integer
        For index As Integer = 0 To Account.Length
            If Account(index).Player.Name = Name Then Return index
        Next
        Return 0
    End Function

    Public Shared Sub SendPlayers()
        Dim i As Integer
        For i = 1 to PlayerCount
            If Not IsNothing(Player(i)) Then SendData.PlayerData(i)
        Next
    End Sub
End Class
