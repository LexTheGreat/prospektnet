Imports Lidgren.Network
Public Class Networking
    Public Shared Sub Initialize()
        Dim pConfig As NetPeerConfiguration
        pConfig = New NetPeerConfiguration("Prospekt")
        pConfig.AutoFlushSendQueue = True
        pClient = New NetClient(pConfig)
        pClient.Start()
        pClient.Connect(ClientConfig.IP, ClientConfig.Port)
    End Sub
    Public Shared Sub HandleMessage()
        Dim im As NetIncomingMessage
        im = pClient.ReadMessage
        While Not IsNothing(im)
            Select Case im.MessageType
                Case NetIncomingMessageType.DebugMessage, NetIncomingMessageType.ErrorMessage, NetIncomingMessageType.VerboseDebugMessage, NetIncomingMessageType.WarningMessage
                    Exit Sub
                Case NetIncomingMessageType.Data
                    HandleData.HandleDataPackets(im.ReadInt32, im)
                    Exit Sub
                Case NetIncomingMessageType.StatusChanged
                    Exit Sub
            End Select
        End While
    End Sub

    Public Shared Sub Dispose()
        pClient.Disconnect("Disconneted by request of user")
    End Sub

    Public Shared Function ConnectToServer() As Boolean
        Dim Wait As Integer

        ' Check to see if we are already connected, if so just exit
        If IsConnected() Then
            ConnectToServer = True
            Exit Function
        End If
        pClient.Connect(ClientConfig.IP, ClientConfig.Port)
        ' Try connect with the server
        Wait = System.Environment.TickCount

        ' Wait until connected or 3 seconds have passed and report the server being down
        Do While (Not IsConnected()) And (System.Environment.TickCount <= Wait + 1000)
            Application.DoEvents()
        Loop

        ' Return function value
        ConnectToServer = IsConnected()
    End Function

    Public Shared Function IsConnected() As Boolean
        If pClient.ServerConnection.Status = NetConnectionStatus.Connected Then Return True
        Return False
    End Function

    Public Shared Sub SendData(ByRef Data As NetOutgoingMessage)
        If Networking.IsConnected Then
            Dim outGoingPacket As NetOutgoingMessage = pClient.CreateMessage(Data.LengthBytes)
            outGoingPacket.Write(Data)
            pClient.SendMessage(outGoingPacket, NetDeliveryMethod.ReliableOrdered)
        End If
    End Sub
End Class
