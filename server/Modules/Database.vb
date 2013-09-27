Module Database
    Public Function fileExist(ByVal filepath As String, Optional ByVal Raw As Boolean = False) As Boolean
        If Raw = True Then
            fileExist = System.IO.File.Exists(filepath)
        Else
            fileExist = System.IO.File.Exists(filepath)
        End If
    End Function
    Public Function AccountExists(ByVal Name As String) As Boolean
        Dim db As SQLiteDatabase
        Dim data As DataTable
        Dim query As String
        Dim r As DataRow
        db = New SQLiteDatabase(pathContent & "accounts.s3db")
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

