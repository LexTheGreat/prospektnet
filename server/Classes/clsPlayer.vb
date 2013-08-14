Public Class clsPlayer
    ' general
    Public Name As String
    Public Password As String
    Public Sprite As Integer
    ' location
    Public X As Integer
    Public Y As Integer
    Public Dir As Byte
    ' non-saved values
    Public XOffset As Integer, YOffset As Integer
    Public Moving As Boolean
    Public PlayerStep As Byte
    Public isPlaying As Boolean

    ' sub routines and functions
    Public Sub Save()
        Dim db As SQLiteDatabase
        Dim data As Dictionary(Of String, String)
        db = New SQLiteDatabase("content/accounts.s3db")
        data = New Dictionary(Of String, String)
        data.Add("X", X)
        data.Add("Y", Y)
        data.Add("Dir", Dir)
        db.Update("Accounts", data, "Login= '" & Name & "'")
    End Sub

    Public Function Create(ByVal PName As String, ByVal PPass As String) As Boolean
        Dim db As SQLiteDatabase
        Dim data As Dictionary(Of String, String)
        db = New SQLiteDatabase("content/accounts.s3db")
        data = New Dictionary(Of String, String)
        data.Add("Login", PName)
        data.Add("Password", PPass)
        Try
            db.Insert("Accounts", data)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Load(ByVal PName As String)
        Dim db As SQLiteDatabase
        Dim data As DataTable
        Dim query As String
        Dim r As DataRow
        db = New SQLiteDatabase("content/accounts.s3db")
        query = "select Login, Password, X, Y, Dir from Accounts"
        data = db.GetDataTable(query)
        For Each r In data.Rows
            If Trim(r("Login").ToString) = Trim(PName) Then
                Name = Trim(r("Login").ToString)
                Password = Trim(r("Password").ToString)
                X = Val(r("X").ToString)
                Y = Val(r("Y").ToString)
                Dir = Val(r("Dir").ToString)
            End If
        Next
    End Sub
End Class
