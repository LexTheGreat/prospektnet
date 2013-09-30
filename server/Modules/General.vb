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

    Public Function RandomNumber(ByVal MaxNumber As Integer, _
    Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        Dim r As New Random(System.DateTime.Now.Millisecond)

        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)

    End Function
End Module
