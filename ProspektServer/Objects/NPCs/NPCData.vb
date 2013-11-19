Imports System.IO
Imports Prospekt.Network
Imports IHProspekt.Objects
Imports IHProspekt.Database
Public Class NPCData
    Public Sub LoadAll()
        Try
            ReDim Preserve NPC(0 To 1)
            NPC(0) = New NPCs
            If Directory.Exists(pathNPCs) Then
                Dim fileEntries As String() = Directory.GetFiles(pathNPCs)
                Dim fileName As String, i As Integer, loadNPC As New NPCBase
                For Each fileName In fileEntries
                    Using File As New Files(fileName, loadNPC)
                        loadNPC = DirectCast(File.ReadXML, NPCBase)
                    End Using
                    NPCCount = i + 1
                    ReDim Preserve NPC(0 To NPCCount)
                    NPC(NPCCount) = New NPCs
                    NPC(NPCCount).Base = loadNPC
                    NPC(NPCCount).Index = NPCCount
                    i = NPCCount
                Next fileName
            End If
        Catch ex As Exception
            Server.WriteLine("Error: " & ex.ToString & " (In: NPCs.Data.LoadNPCs")
        End Try
    End Sub

    Public Sub Save(ByVal snpc As NPCBase)
        Try
            Using File As New Files(pathNPCs & snpc.Name & ".xml", snpc)
                File.WriteXML()
            End Using
            NPC(GetNPCIndex(snpc.Name)).Base = snpc
            Exit Sub
        Catch ex As Exception
            Server.WriteLine("Error: " & ex.ToString & " (In: NPCs.Data.SaveNPC)")
            Exit Sub
        End Try
    End Sub

    Public Sub SaveAll()
        Try
            If Directory.Exists(pathNPCs) Then
                For Each nc In NPC
                    nc.Save()
                Next
            End If
        Catch ex As Exception
            Server.WriteLine("Error: " & ex.ToString & " (In: NPCs.Data.SaveAll")
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
