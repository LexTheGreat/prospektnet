Imports Lidgren.Network
Namespace Network.SendData
    Public Module SendData
        Public Sub Register(ByVal Name As String, ByVal Password As String)
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage

            Buffer.Write(ClientPackets.Register)
            Buffer.Write(Name)
            Buffer.Write(Password)
            Networking.SendData(Buffer)

            loginSent = True
        End Sub

        Public Sub NewCharacter(ByVal Login As String, ByVal Name As String)
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage

            Buffer.Write(ClientPackets.NewCharacter)
            Buffer.Write(Login)
            Buffer.Write(Name)
            Networking.SendData(Buffer)

            loginSent = True
        End Sub

        Public Sub Login(ByVal Login As String, ByVal Password As String)
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage

            Buffer.Write(ClientPackets.Login)
            Buffer.Write(Login)
            Buffer.Write(Password)
            Networking.SendData(Buffer)

            loginSent = True
        End Sub

        Public Sub Position()
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage

            Buffer.Write(ClientPackets.Position)
            Buffer.Write(Player(MyIndex).Moving)
            Buffer.Write(Player(MyIndex).X)
            Buffer.Write(Player(MyIndex).Y)
            Buffer.Write(Player(MyIndex).Dir)
            Networking.SendData(Buffer)

        End Sub
        Public Sub Message(ByVal Message As String)
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage

            Buffer.Write(ClientPackets.Message)
            Buffer.Write("[" & [Enum].GetName(GetType(ChatModes), chatMode) & "] ")
            Buffer.Write(Message)
            Networking.SendData(Buffer)

        End Sub

        Public Sub WarpTo(ByVal index As Integer)
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage

            Buffer.Write(ClientPackets.WarpTo)
            Buffer.Write(index)
            Networking.SendData(Buffer)

        End Sub

        Public Sub WarpToMe(ByVal index As Integer)
            Dim Buffer As NetOutgoingMessage = pClient.CreateMessage

            Buffer.Write(ClientPackets.WarpToMe)
            Buffer.Write(index)
            Networking.SendData(Buffer)

        End Sub
    End Module
End Namespace