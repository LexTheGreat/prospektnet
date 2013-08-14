Imports Winsock_Orcas
Module modNetworking
#Region "General"
    Public WithEvents PlayerSocket As Winsock
    Private Sub DataArrival(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockDataArrivalEventArgs) Handles PlayerSocket.DataArrival
        If IsConnected() Then Call IncomingData(PlayerSocket.Get)
    End Sub
    Private Sub Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlayerSocket.Disconnected
        If inGame Then frmMain.Close()
    End Sub
    Private Sub HandleData(ByRef Data() As Byte)
        Dim Buffer As clsBuffer
        ' Start the command
        Buffer = New clsBuffer
        buffer.WriteBytes(Data)
        HandleDataPackets(Buffer.ReadLong, Buffer.ReadBytes(Buffer.Length))
        Buffer = Nothing
    End Sub
    Public Sub IncomingData(ByVal Data() As Byte)
        Dim Buffer As clsBuffer
        Dim pLength As Long
        Buffer = New clsBuffer

        Buffer.WriteBytes(Data)

        If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)

        Do While pLength > 0 And pLength <= Buffer.Length - 8
            If pLength <= Buffer.Length - 8 Then
                Buffer.ReadLong()
                HandleData(Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)
        Loop

        ' Clear buffer
        Buffer = Nothing
    End Sub

    Sub TcpInit()
        ' Create data to connect with the server
        PlayerSocket = New Winsock

        ' connect
        PlayerSocket.RemoteHost = ClientConfig.IP
        PlayerSocket.RemotePort = ClientConfig.Port
        PlayerSocket.Connect()
    End Sub

    Sub DestroyTCP()
        PlayerSocket.Close()
        PlayerSocket = Nothing
    End Sub

    Public Function ConnectToServer() As Boolean
        Dim Wait As Long

        ' Check to see if we are already connected, if so just exit
        If IsConnected() Then
            ConnectToServer = True
            Exit Function
        End If

        ' Try connect with the server
        Wait = System.Environment.TickCount
        PlayerSocket.Close()
        PlayerSocket.Connect()

        ' Wait until connected or 3 seconds have passed and report the server being down
        Do While (Not IsConnected()) And (System.Environment.TickCount <= Wait + 1000)
            Application.DoEvents()
        Loop

        ' Return function value
        ConnectToServer = IsConnected()
    End Function

    Function IsConnected() As Boolean
        If PlayerSocket.State = WinsockStates.Connected Then
            IsConnected = True
        End If
    End Function

    Sub SendData(ByRef Data() As Byte)
        Dim Buffer As clsBuffer

        If IsConnected() Then
            Buffer = New clsBuffer

            Buffer.WriteLong((UBound(Data) - LBound(Data)) + 1)
            Buffer.WriteBytes(Data)
            PlayerSocket.Send(Buffer.ToArray())
            Buffer = Nothing
        End If
    End Sub
#End Region
    Private Sub HandleDataPackets(ByVal PacketNum As Long, ByRef Data() As Byte)
        ' Checks which is the command to run
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ServerPackets.SLoginOk Then HandleLoginOk(Data)
        If PacketNum = ServerPackets.SPlayer Then HandlePlayer(Data)
        If PacketNum = ServerPackets.SClearPlayer Then HandleClearPlayer(Data)
        If PacketNum = ServerPackets.SPosition Then HandlePosition(Data)
        If PacketNum = ServerPackets.SMessage Then HandleMessage(Data)
    End Sub
    Public Sub SendLogin(ByVal Name As String, ByVal Password As String)
        Dim Buffer As clsBuffer

        Buffer = New clsBuffer
        Buffer.WriteLong(ClientPackets.CLogin)
        Buffer.WriteString(Name)
        Buffer.WriteString(Password)
        SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub SendPosition()
        Dim Buffer As clsBuffer

        Buffer = New clsBuffer
        Buffer.WriteLong(ClientPackets.CPosition)
        Buffer.WriteLong(Player(MyIndex).Moving)
        Buffer.WriteLong(Player(MyIndex).X)
        Buffer.WriteLong(Player(MyIndex).Y)
        Buffer.WriteLong(Player(MyIndex).Dir)
        SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub
    Public Sub SendMessage(ByVal Message As String)
        Dim Buffer As clsBuffer

        Buffer = New clsBuffer
        Buffer.WriteLong(ClientPackets.CMessage)
        Buffer.WriteString(Message)
        SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Sub HandleLoginOk(ByRef Data() As Byte)
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer
        Buffer.WriteBytes(Data)
        MyIndex = Buffer.ReadLong
        Buffer = Nothing
        faderState = 3
        faderAlpha = 0
    End Sub

    Sub HandlePlayer(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        PlayerHighindex = Buffer.ReadLong
        If IsNothing(Player(tempIndex)) Then Player(tempIndex) = New clsPlayer
        Player(tempIndex).Load(Buffer.ReadString, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong)
        Buffer = Nothing
    End Sub

    Sub HandleClearPlayer(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        PlayerHighindex = Buffer.ReadLong
        Player(tempIndex) = Nothing
        Buffer = Nothing
    End Sub

    Sub HandlePosition(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        Player(tempIndex).Moving = Buffer.ReadLong
        Player(tempIndex).X = Buffer.ReadLong
        Player(tempIndex).Y = Buffer.ReadLong
        Player(tempIndex).Dir = Buffer.ReadLong
        Select Case Player(tempIndex).Dir
            Case DirEnum.Up
                Player(tempIndex).YOffset = picY
            Case DirEnum.Down
                Player(tempIndex).YOffset = picY * -1
            Case DirEnum.Left
                Player(tempIndex).XOffset = picX
            Case DirEnum.Right
                Player(tempIndex).XOffset = picX * -1
        End Select
        Buffer = Nothing
    End Sub

    Sub HandleMessage(ByRef Data() As Byte)
        Dim Buffer As clsBuffer, Message As String, I As Integer
        Buffer = New clsBuffer
        Buffer.WriteBytes(Data)
        Message = Buffer.ReadString
        Buffer = Nothing
        For I = 1 To maxChatLines
            If Len(Trim(chatbuffer(I))) = 0 Then
                chatbuffer(I) = Message
                Exit Sub
            End If
        Next
        For I = 1 To maxChatLines
            If I < maxChatLines Then
                chatbuffer(I) = chatbuffer(I + 1)
            Else
                chatbuffer(I) = Message
            End If
        Next
    End Sub
End Module
