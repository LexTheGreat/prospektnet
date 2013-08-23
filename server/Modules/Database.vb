Imports System.Xml
Imports System.IO
Module Database
    Public Function fileExist(ByVal filepath As String, Optional ByVal Raw As Boolean = False) As Boolean
        If Raw = True Then
            fileExist = System.IO.File.Exists(filepath)
        Else
            fileExist = System.IO.File.Exists(filepath)
        End If
    End Function

    Public Sub LoadOptions()
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        m_xmld = New XmlDocument
        m_xmld.Load(pathContent & "config.xml")

        m_nodelist = m_xmld.SelectNodes("configuration/network")
        For Each m_node In m_nodelist
            ServerConfig.Port = m_node.Item("port").InnerText
        Next
    End Sub
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
                Exit Function
            Else
                Return False
                Exit Function
            End If
        Next
    End Function
End Module

