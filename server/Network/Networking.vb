Imports Winsock_Orcas
Public Class Networking
    Public Shared Clients(100) As ClientSocket

    Public Shared Sub SendDataTo(ByVal index As Long, ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Dim TempData() As Byte
        If PlayerLogic.IsConnected(index) Then
            Buffer = New ByteBuffer
            TempData = Data

            Buffer.WriteLong(UBound(TempData) - LBound(TempData) + 1)
            Buffer.WriteBytes(TempData)

            Clients(index).Socket.Send(Buffer.ToArray)
            Buffer = Nothing
        End If
    End Sub

    Public Shared Sub SendDataToAll(ByRef Data() As Byte)
        Dim i As Long

        For i = 1 To PlayerHighIndex
            If PlayerLogic.IsPlaying(i) Then
                Call SendDataTo(i, Data)
            End If
        Next
    End Sub

    Public Shared Sub SendDataToAllBut(ByVal index As Long, ByRef Data() As Byte)
        Dim i As Long
        For i = 1 To PlayerHighIndex
            If PlayerLogic.IsPlaying(i) Then
                If i <> index Then
                    Call SendDataTo(i, Data)
                End If
            End If
        Next
    End Sub

    Public Shared Sub SendDataToAdmins(ByRef Data() As Byte)
        Dim i As Long

        For i = 1 To PlayerHighIndex
            If PlayerLogic.IsPlaying(i) And Not Player(i).AccessMode = ACCESS.NONE Then
                Call SendDataTo(i, Data)
            End If
        Next
    End Sub

    Public Shared Sub SendDataToParty(ByVal id As Long, ByRef Data() As Byte)
        Dim i As Long

        For i = 1 To PlayerHighIndex
            If PlayerLogic.IsPlaying(i) And Player(i).GetParty = id > 0 Then
                Call SendDataTo(i, Data)
            End If
        Next
    End Sub

    Public Shared Sub SendDataToGuild(ByVal id As Long, ByRef Data() As Byte)
        Dim i As Long

        For i = 1 To PlayerHighIndex
            If PlayerLogic.IsPlaying(i) And Player(i).GuildID = id > 0 Then
                Call SendDataTo(i, Data)
            End If
        Next
    End Sub

    Public Shared Sub Handle(ByVal index As Long, ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        ' Start the command
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        HandleData.HandleDataPackets(Buffer.ReadLong, index, Buffer.ReadBytes(Buffer.Length))
        Buffer = Nothing
    End Sub

    Public Shared Sub SocketConnected(ByVal index As Long)
        If index = 0 Then Exit Sub
        Console.WriteLine("Received connection from " & PlayerLogic.GetPlayerIP(index) & ".")
    End Sub

    Public Shared Sub IncomingData(ByVal index As Long, ByVal Data() As Byte)
        Dim pLength As Long
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer

        Buffer.WriteBytes(Data)

        If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)

        Do While pLength > 0 And pLength <= Buffer.Length - 8
            If pLength <= Buffer.Length - 8 Then
                Buffer.ReadLong()
                Networking.Handle(index, Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)
        Loop

        ' Clear buffer
        Buffer = Nothing
    End Sub

    Public Shared Sub CloseSocket(ByVal index As Long)
        If index > 0 Then
            Console.WriteLine("Connection from " & PlayerLogic.GetPlayerIP(index) & " has been terminated.")
            If Not IsNothing(Player(index)) Then
                Select Case Player(index).AccessMode
                    Case ACCESS.NONE : SendData.Message(Trim$(Player(index).Name) & " has left the game.")
                    Case ACCESS.GMIT : SendData.Message(Trim$("(GMIT) " & Player(index).Name) & " has left the game.")
                    Case ACCESS.GM : SendData.Message(Trim$("(GM) " & Player(index).Name) & " has left the game.")
                    Case ACCESS.LEAD_GM : SendData.Message(Trim$("(Lead GM) " & Player(index).Name) & " has left the game.")
                    Case ACCESS.DEV : SendData.Message(Trim$("(DEV) " & Player(index).Name) & " has left the game.")
                    Case ACCESS.ADMIN : SendData.Message(Trim$("(Admin) " & Player(index).Name) & " has left the game.")
                End Select
                Console.WriteLine(Player(index).Name & " has left the game.")
                Player(index).SetIsPlaying(False)
                Player(index).Save()
                Player(index) = Nothing
                PlayerLogic.UpdateHighIndex()
                SendData.ClearPlayer(index)
            End If
            Clients(index).Socket.Close()
        End If
    End Sub

    Public Shared Function GetPublicIP() As String
        Dim direction As String = ""
        Dim request As System.Net.WebRequest
        On Error GoTo errorhandler
        request = System.Net.WebRequest.Create("http://checkip.dyndns.org/")
        Using response As System.Net.WebResponse = request.GetResponse()
            Using stream As New System.IO.StreamReader(response.GetResponseStream())
                direction = stream.ReadToEnd()
            End Using
        End Using

        'Search for the ip in the html
        Dim first As Integer = direction.IndexOf("Address: ") + 9
        Dim last As Integer = direction.LastIndexOf("</body>")
        direction = direction.Substring(first, last - first)

        Return direction
errorhandler:
        Return "localhost"
    End Function
End Class
