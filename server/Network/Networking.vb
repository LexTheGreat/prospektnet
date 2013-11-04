Imports Lidgren.Network
Imports Prospekt.Network
Public Class Networking
    Public Shared Sub Initialize()
        Dim pConfig As NetPeerConfiguration
        pConfig = New NetPeerConfiguration("Prospekt")
        pConfig.Port = ServerConfig.Port
        pConfig.AutoFlushSendQueue = True
        pConfig.MaximumConnections = ServerConfig.MaxPlayers
        pServer = New NetServer(pConfig)
        pServer.Start()
    End Sub

    Public Shared Function getIndex(ByRef connection As NetConnection)
        For I As Integer = 1 To PlayerCount
            If Not IsNothing(ConnectedClients(I)) And Not IsNothing(connection) Then
                If ConnectedClients(I).RemoteUniqueIdentifier = connection.RemoteUniqueIdentifier Then
                    Return I
                End If
            End If
        Next
        Return 0
    End Function

    Public Shared Function IsConnected(ByVal index As Integer) As Boolean
        Dim tempClient As NetConnection
        tempClient = ConnectedClients(index)
        If IsNothing(tempClient) Then
            Return False
        ElseIf tempClient.Status = NetConnectionStatus.Connected Then
            Return True
        End If
        Return False
    End Function

    Public Shared Sub SendDataTo(ByVal index As Integer, ByRef Data As NetOutgoingMessage)
        If Networking.IsConnected(index) Then
            pServer.SendMessage(Data, ConnectedClients(index), NetDeliveryMethod.ReliableOrdered)
        End If
    End Sub

    Public Shared Sub SendDataToAll(ByRef Data As NetOutgoingMessage)
        Dim i As Integer

        For i = 1 To PlayerCount
            If Players.Logic.IsPlaying(i) Then
                Call SendDataTo(i, Data)
            End If
        Next
    End Sub

    Public Shared Sub SendDataToAllBut(ByVal index As Integer, ByRef Data As NetOutgoingMessage)
        Dim i As Integer
        For i = 1 To PlayerCount
            If Players.Logic.IsPlaying(i) Then
                If i <> index Then
                    Call SendDataTo(i, Data)
                End If
            End If
        Next
    End Sub

    Public Shared Sub SendDataToAdmins(ByRef Data As NetOutgoingMessage)
        Dim i As Integer

        For i = 1 To PlayerCount
            If Players.Logic.IsPlaying(i) And Not Player(i).AccessMode = ACCESS.NONE Then
                Call SendDataTo(i, Data)
            End If
        Next
    End Sub
    Public Shared Sub UpdateHighIndex()
        Dim i As Long
        PlayerCount = 0
        For i = ServerConfig.MaxPlayers To 1 Step -1
            If IsConnected(i) Then
                PlayerCount = i
                Exit Sub
            End If
        Next i
    End Sub

    Public Shared Sub HandleMessage()
        Dim im As NetIncomingMessage, index As Integer, I As Integer
        im = pServer.ReadMessage
        While Not IsNothing(im)

            index = Networking.getIndex(im.SenderConnection)
            Select Case im.MessageType
                Case NetIncomingMessageType.DebugMessage, NetIncomingMessageType.ErrorMessage, NetIncomingMessageType.VerboseDebugMessage, NetIncomingMessageType.WarningMessage
                    Exit Sub
                Case NetIncomingMessageType.Data
                    HandleData.HandleDataPackets(im.ReadInt32, index, im)
                    Exit Sub
                Case NetIncomingMessageType.StatusChanged
                    Dim status As NetConnectionStatus = im.ReadByte
                    If status = NetConnectionStatus.Connected Then
                        PlayerCount = PlayerCount + 1
                        I = Players.Logic.FindOpenPlayerSlot()

                        If I <> 0 Then
                            ' we can connect them
                            ConnectedClients(I) = im.SenderConnection
                            Console.WriteLine("Received connection from " & im.SenderConnection.RemoteUniqueIdentifier & ".")
                        End If
                    ElseIf status = NetConnectionStatus.Disconnected Then
                        CloseSocket(index)
                    End If
                    Exit Sub
            End Select
        End While
    End Sub

    Public Shared Sub CloseSocket(ByVal index As Integer)
        If index > 0 Then
            Console.WriteLine("Closed connection from " & ConnectedClients(index).RemoteUniqueIdentifier & ".")
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
                LuaScript.executeFunction("onLogout", index)
                Player(index).IsPlaying = False
                Player(index).Save()
                Player(index) = Nothing
                UpdateHighIndex()
                SendData.ClearPlayer(index)
                ConnectedClients(index) = Nothing
            End If
        End If
    End Sub

    Public Shared Function GetPublicIP() As String
        Dim direction As String = ""
        Dim request As System.Net.WebRequest
        Try
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
        Catch ex As Exception
            Return "localhost"
        End Try
    End Function
End Class
