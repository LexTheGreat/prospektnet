Imports Lidgren.Network
Namespace Network
    Public Module Networking
        Public Sub InitializeNetwork()
            Dim pConfig As NetPeerConfiguration
            pConfig = New NetPeerConfiguration("Prospekt")
            pConfig.AutoFlushSendQueue = True
            pClient = New NetClient(pConfig)
            pClient.Start()
            pClient.Connect(EditorConfig.IP, EditorConfig.Port)
        End Sub
        Public Sub HandleMessage()
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

        Public Sub DestroyNetwork()
            pClient.Disconnect("Disconneted by request of user")
        End Sub

        Public Function ConnectToServer() As Boolean
            Dim Wait As Integer

            ' Check to see if we are already connected, if so just exit
            If IsConnected() Then
                ConnectToServer = True
                Exit Function
            End If
            pClient.Disconnect("Disconneted by request of user")
            pClient.Connect(EditorConfig.IP, EditorConfig.Port)
            ' Try connect with the server
            Wait = MainTimer.GetTotalTimeElapsed

            ' Wait until connected or 3 seconds have passed and report the server being down
            Do While (Not IsConnected()) And (MainTimer.GetTotalTimeElapsed <= Wait + 1000)
                Application.DoEvents()
            Loop

            ' Return function value
            ConnectToServer = IsConnected()
        End Function

        Public Function IsConnected() As Boolean
            If IsNothing(pClient.ServerConnection) Then Return False
            If pClient.ServerConnection.Status = NetConnectionStatus.Connected Then Return True
            Return False
        End Function

        Public Sub SendData(ByRef Data As NetOutgoingMessage)
            If Networking.IsConnected Then
                pClient.SendMessage(Data, NetDeliveryMethod.ReliableOrdered)
            End If
        End Sub
    End Module
End Namespace