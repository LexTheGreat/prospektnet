Module General
    Sub ServerLoop()
        Dim Tick As Integer
        Dim tmrPlayerSave As Integer
        Do While inServer
            Tick = System.Environment.TickCount()
            'Saves players every 5 minutes
            If tmrPlayerSave < Tick Then
                Console.WriteLine("Saving Players...")
                AccountData.SaveOnlineAccounts()
                tmrPlayerSave = System.Environment.TickCount + 300000
            End If
        Loop
    End Sub
End Module
