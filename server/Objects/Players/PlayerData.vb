Imports System
Imports System.IO
Imports System.Collections
Imports System.Xml.Serialization
Imports Prospekt.Network
Imports IHProspekt.Core

Public Class PlayerData
    Public Sub SaveOnlinePlayers()
        Dim index As Integer

        For index = 1 To PlayerCount
            If Players.Logic.IsPlaying(index) Then
                Try
                    Account(Players.Data.GetPlayerIndex(Player(index).Name)).Player = Player(index).Base
                    Account(Players.Data.GetPlayerIndex(Player(index).Name)).Save()
                Catch ex As Exception
                    Console.WriteLine("Error: " & ex.ToString & " (In: Players.Data.SaveOnlinePlayers)")
                    Continue For
                End Try
            End If
        Next index
    End Sub

    Public Sub SavePlayer(ByVal player As PlayerBase)
        Dim acc As New Accounts
        Try
            acc = Account(Players.Data.GetPlayerIndex(player.Name))
            acc.Player = player
            'Serialize object to a file.
            Files.WriteXML(pathAccounts & acc.Email & ".xml", acc.Base)
            Account(Players.Data.GetPlayerIndex(player.Name)) = acc
            Exit Sub
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Accounts.Data.SavePlayer)")
            Exit Sub
        End Try
    End Sub

    Public Function Create(ByVal Name As String) As Boolean
        Try
            Dim newPlayer As New PlayerBase()
            newPlayer.Name = Name
            ' Update accounts array
            Account(Players.Data.GetPlayerIndex(Name)).Player = newPlayer
            Files.WriteXML(pathAccounts & Name & ".xml", newPlayer)
            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: Players.Create)")
            Return False
        End Try
    End Function

    Public Function PlayerExists(ByVal Name As String) As Boolean
        Try
            For Each curAccount In Account
                If curAccount.Player.Name = Name Then Return True
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Function HasPlayer(ByVal index As Integer) As Boolean
        Try
            If Not Account(index).Player.Name = vbNullString Then Return True
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Function GetPlayer(ByVal Name As String) As PlayerBase
        Dim player As New PlayerBase
        For Each curAccount In Account
            If curAccount.Player.Name = Name Then Return curAccount.Player
        Next
        Return player
    End Function

    Public Function GetPlayerIndex(ByVal Name As String) As Integer
        For index As Integer = 0 To Account.Length
            If Account(index).Player.Name = Name Then Return index
        Next
        Return 0
    End Function

    Public Sub SendPlayers()
        Dim i As Integer
        For i = 1 To PlayerCount
            If Not IsNothing(Player(i)) Then SendData.PlayerData(i)
        Next
    End Sub
End Class
