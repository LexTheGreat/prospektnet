Imports System.IO
Imports Prospekt.Network
Imports IHProspekt.Objects
Imports IHProspekt.Database
Public Class NPCData

    Public Sub LoadNPCs()
        Try
            ReDim Preserve NPC(0 To 1)
            NPC(0) = New NPCs
            If Directory.Exists(pathNPCs) Then
                Dim fileEntries As String() = Directory.GetFiles(pathNPCs)
                Dim fileName As String, i As Integer, loadNPC As New NPCBase
                For Each fileName In fileEntries
                    loadNPC = DirectCast(ReadXML(fileName, loadNPC), NPCBase)
                    NPCCount = i + 1
                    ReDim Preserve NPC(0 To NPCCount)
                    NPC(NPCCount) = New NPCs
                    NPC(NPCCount).Base = loadNPC
                    NPC(NPCCount).Index = NPCCount
                    i = NPCCount
                Next fileName
            End If
        Catch ex As Exception
            Server.Writeline("Error: " & ex.ToString & " (In: NPCs.Data.LoadNPCs")
        End Try
    End Sub

    Public Sub SaveNPC(ByVal snpc As NPCBase)
        Try
            WriteXML(pathNPCs & snpc.Name & ".xml", snpc)
            NPC(GetNPCIndex(snpc.Name)).Base = snpc
            Exit Sub
        Catch ex As Exception
            Server.Writeline("Error: " & ex.ToString & " (In: NPCs.Data.SaveNPC)")
            Exit Sub
        End Try
    End Sub

    Public Function GetNPCIndex(ByVal Name As String) As Integer
        For index As Integer = 1 To NPCCount
            If NPC(index).Name = Name Then Return index
        Next
        Return 0
    End Function

    Public Sub SendNPCs()
        Dim i As Integer
        For i = 1 To NPCCount
            If Not IsNothing(NPC(i)) Then
                SendData.NPCData(i)
            End If
        Next
    End Sub
End Class
