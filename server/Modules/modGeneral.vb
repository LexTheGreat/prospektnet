Module modGeneral
    Sub ServerLoop()
        Dim Tick As Integer
        Dim tmrPlayerSave As Integer
        Do While inServer
            Tick = System.Environment.TickCount()
            'Saves players every 5 minutes
            If tmrPlayerSave < Tick Then
                For I = 1 To PlayerHighIndex
                    If IsPlaying(I) Then Player(I).Save()
                Next
                tmrPlayerSave = System.Environment.TickCount + 300000
            End If
        Loop
    End Sub

    Public Function AccountExists(ByVal Name As String) As Boolean
        Dim db As SQLiteDatabase
        Dim data As DataTable
        Dim query As String
        Dim r As DataRow
        db = New SQLiteDatabase("content/accounts.s3db")
        query = "select Login from Accounts"
        data = db.GetDataTable(query)
        For Each r In data.Rows
            If Trim(r("Login").ToString) = Trim(Name) Then
                Return True
            End If
        Next
        Return False
    End Function
End Module
