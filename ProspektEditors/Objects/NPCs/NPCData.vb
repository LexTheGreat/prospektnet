﻿Imports System.IO
Imports IHProspekt.Objects
Imports IHProspekt.Database
Imports IHProspekt.Core
Public Class NPCData
    Public Sub LoadAll()
        Try
            If Directory.Exists(pathNPCs) Then
                Dim fileEntries As String() = Directory.GetFiles(pathNPCs, "*.xml")
                ReDim Preserve NPC(0 To 1)
                NPC(0) = New NPCs
                NPCCount = 0
                For Each fileName In fileEntries
                    ReDim Preserve NPC(0 To NPCCount)
                    NPC(NPCCount) = New NPCs
                    NPC(NPCCount).SetID(NPCCount)
                    NPC(NPCCount).Load()
                    NPCCount = NPCCount + 1
                Next fileName
                NPCCount = NPCCount - 1
            End If
        Catch ex As Exception
            ErrHandler.HandleException(ex, ErrorHandler.ErrorLevels.High)
        End Try
    End Sub

    Public Sub SaveAll()
        If Directory.Exists(pathNPCs) Then
            For Each nc In NPC
                nc.Save()
            Next
        End If
    End Sub

    Public Sub Save(ByVal SaveNpc As NPCBase)
        Using File As New Files(pathNPCs & Trim(SaveNpc.ID) & ".xml", SaveNpc)
            File.WriteXML()
        End Using
    End Sub

    Public Sub NewNpc()
        Dim newNpc As New NPCs, i As Integer = GetNextNpcIndex()
        ReDim Preserve NPC(0 To i)
        newNpc.SetID(i)
        NPC(i) = newNpc
        SaveAll()
    End Sub

    Public Function GetNpcIndex(ByVal ID As Integer) As Integer
        For index As Integer = 0 To NPCCount
            If NPC(index).ID = ID Then Return index
        Next
        Return 0
    End Function

    Public Function GetNpcIndexByName(ByVal Name As String) As Integer
        For index As Integer = 0 To NPCCount
            If NPC(index).Name = Name Then Return index
        Next
        Return 0
    End Function

    Public Function GetNpc(ByVal ID As Integer) As NPCs
        For Each nc In NPC
            If nc.ID = ID Then Return nc
        Next
        Return New NPCs
    End Function

    Public Function GetNextNpcIndex() As Integer
        If Not IsNothing(NPC) Then Return NPCCount + 1
        Return 0
    End Function

    Public Function NpcExists(ByVal ID As Integer) As Boolean
        Dim Filename As String
        Filename = pathNPCs & Trim(ID) & ".xml"

        If System.IO.File.Exists(Filename) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetNPCIndex(ByVal Name As String) As Integer
        For index As Integer = 1 To NPCCount
            If NPC(index).Name = Name Then Return index
        Next
        Return 0
    End Function
End Class
