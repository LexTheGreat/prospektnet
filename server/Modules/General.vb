Module General
    Sub ServerLoop()
        Dim Tick As Integer
        Dim tmrPlayerSave As Integer
        Dim tmrNpcMove As Integer
        Do While inServer
            Tick = System.Environment.TickCount()
            'Saves players every 5 minutes
            If tmrPlayerSave < Tick Then
                Console.WriteLine("Saving Players...")
                AccountData.SaveOnlineAccounts()
                tmrPlayerSave = System.Environment.TickCount + 300000
            End If
            'Generate Npc movement every second
            'If tmrNpcMove < Tick Then
            '    Dim i As Integer
            '    For i = 0 To NPCCount ' Loop through Npc's
            '        If Not IsNothing(NPC(i)) Then ' Make sure Npc exists
            '            NPC(i).GenerateMovement()
            '        End If
            '    Next i
            '    tmrNpcMove = System.Environment.TickCount + 1000
            'End If
        Loop
    End Sub

    Public Function RandomNumber(ByVal MaxNumber As Integer, _
    Optional ByVal MinNumber As Integer = 0) As Integer
        Dim r As New Random()

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)
    End Function
End Module
