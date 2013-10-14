Imports Winsock_Orcas
Public Class Networking
    Public Shared Clients() As ClientSocket

    Public Shared Sub Initialize()
        Server.sckListen = New Winsock
        Server.sckListen.BufferSize = 8192
        Server.sckListen.LegacySupport = False
        Server.sckListen.LocalPort = ServerConfig.Port
        Server.sckListen.MaxPendingConnections = 1
        Server.sckListen.Protocol = WinsockProtocol.Tcp
        Server.sckListen.RemoteHost = "localhost"
        Server.sckListen.RemotePort = ServerConfig.Port
    End Sub

    Public Shared Sub SendDataTo(ByVal index As Long, ByRef Data() As Byte)
        Dim Buffer as New ByteBuffer
        Dim TempData() As Byte
        If PlayerLogic.IsConnected(index) Then
            
            TempData = Data

            Buffer.WriteInteger(UBound(TempData) - LBound(TempData) + 1)
            Buffer.WriteBytes(TempData)

            Clients(index).Socket.Send(Buffer.ToArray)
            
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

    Public Shared Sub Handle(ByVal index As Long, ByRef Data() As Byte)
        Dim Buffer as New ByteBuffer
        ' Start the command
        
        Buffer.WriteBytes(Data)
        HandleData.HandleDataPackets(Buffer.ReadInteger, index, Buffer.ReadBytes(Buffer.Length))
        
    End Sub

    Public Shared Sub SocketConnected(ByVal index As Long)
        If index = 0 Then Exit Sub
        Console.WriteLine("Received connection from " & PlayerLogic.GetPlayerIP(index) & ".")
    End Sub

    Public Shared Sub IncomingData(ByVal index As Long, ByVal Data() As Byte)
        Dim pLength As Long
        Dim Buffer as New ByteBuffer
        

        Buffer.WriteBytes(Data)

        If Buffer.Length >= 4 Then pLength = Buffer.ReadInteger(False)

        Do While pLength > 0 And pLength <= Buffer.Length - 4
            If pLength <= Buffer.Length - 4 Then
                Buffer.ReadInteger()
                Networking.Handle(index, Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If Buffer.Length >= 4 Then pLength = Buffer.ReadInteger(False)
        Loop

        ' Clear buffer
        
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
