Imports System
Imports System.IO
Imports System.Collections
Imports System.Xml.Serialization

Module NPCData
    Public Function CreateNPC(ByVal newNPC As NPCs) As Boolean
        Try
            ' Add player to accounts array
            ReDim Preserve NPC(0 To NPCCount + 1)
            NPC(NPCCount) = New NPCs
            NPC(NPCCount) = newNPC
            'Serialize object to a file.
            Dim Writer As New StreamWriter(pathNPCs & newNPC.Name & ".xml")
            Dim ser As New XmlSerializer(newNPC.GetType)
            ser.Serialize(Writer, newNPC)
            Writer.Close()
            NPCCount = NPCCount + 1
            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: NPCData.Create)")
            Return False
        End Try
    End Function

    Public Sub LoadNPCs()
        Dim Reader As StreamReader
        Dim Ser As XmlSerializer
        Try
            ReDim Preserve NPC(0 To 1)
            NPC(0) = New NPCs
            If Directory.Exists(pathNPCs) Then
                Dim fileEntries As String() = Directory.GetFiles(pathNPCs)
                Dim fileName As String, i As Integer = 0, loadNPC As New NPCs
                For Each fileName In fileEntries
                    'Deserialize file to object.
                    Reader = New StreamReader(fileName)
                    Ser = New XmlSerializer(loadNPC.GetType)
                    loadNPC = Ser.Deserialize(Reader)
                    Reader.Close()
                    NPCCount = i + 1
                    ReDim Preserve NPC(0 To NPCCount)
                    NPC(i) = loadNPC
                    i = NPCCount
                Next fileName
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: NPCData.LoadAccounts")
        End Try
    End Sub

    Public Sub SaveNPC(ByVal snpc As NPCs)
        Dim Writer As StreamWriter
        Dim Ser As XmlSerializer
        Try
            'Serialize object to a file.
            Writer = New StreamWriter(pathNPCs & snpc.Name & ".xml")
            Ser = New XmlSerializer(snpc.GetType)
            Ser.Serialize(Writer, snpc)
            Writer.Close()
            NPC(GetNPCIndex(snpc.Name)) = snpc
            Exit Sub
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString & " (In: NPCData.SaveNPC)")
            Exit Sub
        End Try
    End Sub
    Public Sub SendNPCs()
        For i = 1 To NPCCount
            If Not IsNothing(NPC(i)) Then
                SendNPC(i)
            End If
        Next
    End Sub

    Public Function GetNPCIndex(ByVal Name As String) As Integer
        For index As Integer = 0 To NPC.Length
            If NPC(index).Name = Name Then Return index
        Next
        Return 0
    End Function
End Module
