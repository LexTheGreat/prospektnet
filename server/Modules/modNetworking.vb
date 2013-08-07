Imports Winsock_Orcas
Module modNetworking
#Region "General"
    Public Clients(100) As clsSocket
    Public Sub UpdateHighIndex()
        Dim i As Long
        PlayerHighIndex = 0
        For i = 100 To 1 Step -1
            If IsConnected(i) Then
                PlayerHighIndex = i
                Exit Sub
            End If
        Next i
    End Sub
    Public Function IsConnected(ByVal index As Long) As Boolean
        If Clients(index).Socket.State = WinsockStates.Connected Then
            Return True
        End If
        Return False
    End Function

    Function IsPlaying(ByVal index As Long) As Boolean
        ' Checks if the player is online
        If IsConnected(index) Then
            If Player(index).isPlaying Then
                Return True
            End If
        End If
        Return False
    End Function
    Function GetPlayerIP(ByVal index As Long) As String
        Return Clients(index).IP
    End Function

    Public Function FindOpenPlayerSlot() As Long
        Dim i As Long
        For i = 1 To PlayerHighIndex
            If Not IsConnected(i) Then
                Return i
                Exit Function
            End If
        Next i
        Return 0
    End Function

    Sub SendDataTo(ByVal index As Long, ByRef Data() As Byte)
        Dim Buffer As clsBuffer
        Dim TempData() As Byte
        If IsConnected(index) Then
            Buffer = New clsBuffer
            TempData = Data

            Buffer.WriteLong(UBound(TempData) - LBound(TempData) + 1)
            Buffer.WriteBytes(TempData)

            Clients(index).Socket.Send(Buffer.ToArray)
            Buffer = Nothing
        End If
    End Sub

    Sub SendDataToAll(ByRef Data() As Byte)
        Dim i As Long

        For i = 1 To PlayerHighIndex
            If IsPlaying(i) Then
                Call SendDataTo(i, Data)
            End If
        Next
    End Sub

    Sub SendDataToAllBut(ByVal index As Long, ByRef Data() As Byte)
        Dim i As Long
        For i = 1 To PlayerHighIndex
            If IsPlaying(i) Then
                If i <> index Then
                    Call SendDataTo(i, Data)
                End If
            End If
        Next
    End Sub
    Sub HandleData(ByVal index As Long, ByRef Data() As Byte)
        Dim Buffer As clsBuffer
        ' Start the command
        Buffer = New clsBuffer
        Buffer.WriteBytes(Data)
        HandleDataPackets(Buffer.ReadLong, index, Buffer.ReadBytes(Buffer.Length))
        Buffer = Nothing
    End Sub

    Sub SocketConnected(ByVal index As Long)
        If index = 0 Then Exit Sub
        Console.WriteLine("Received connection from " & GetPlayerIP(index) & ".")
    End Sub

    Public Sub IncomingData(ByVal index As Long, ByVal Data() As Byte)
        Dim pLength As Long
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer

        Buffer.WriteBytes(Data)

        If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)

        Do While pLength > 0 And pLength <= Buffer.Length - 8
            If pLength <= Buffer.Length - 8 Then
                Buffer.ReadLong()
                HandleData(index, Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)
        Loop

        ' Clear buffer
        Buffer = Nothing
    End Sub

    Sub CloseSocket(ByVal index As Long)
        If index > 0 Then
            Console.WriteLine("Connection from " & GetPlayerIP(index) & " has been terminated.")

            Clients(index).Socket.Close()
            Player(index) = Nothing
            UpdateHighIndex()
            SendClearPlayer(index)
        End If
    End Sub
#End Region
    Private Sub HandleDataPackets(ByVal PacketNum As Long, ByVal index As Long, ByRef Data() As Byte)
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ClientPackets.CLogin Then HandleLogin(index, Data)
        If PacketNum = ClientPackets.CPosition Then HandlePosition(index, Data)
    End Sub
    Private Sub HandleLogin(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As clsBuffer
        Dim Name As String

        Buffer = New clsBuffer
        Buffer.WriteBytes(data)
        Name = Buffer.ReadString
        Buffer = Nothing
        UpdateHighIndex()
        Player(index) = New clsPlayer(Name, 1, 1, 1, 0)
        Player(index).isPlaying = True
        SendPlayers()
        SendLoginOk(index)
        Console.WriteLine(Name & " has logged in.")
    End Sub

    Private Sub HandlePosition(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As clsBuffer

        Buffer = New clsBuffer
        Buffer.WriteBytes(data)
        Player(index).Moving = Buffer.ReadLong
        Player(index).X = Buffer.ReadLong
        Player(index).Y = Buffer.ReadLong
        Player(index).Dir = Buffer.ReadLong
        Buffer = Nothing
        SendPosition(index)
    End Sub

    Sub SendLoginOk(ByVal index As Long)
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer
        Buffer.WriteLong(ServerPackets.SLoginOk)
        Buffer.WriteLong(index)
        SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub
    Sub SendPlayers()
        Dim i As Integer
        For i = 1 To PlayerHighIndex
            SendPlayer(i)
        Next
    End Sub
    Sub SendPlayer(ByVal index As Long)
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer
        Buffer.WriteLong(ServerPackets.SPlayer)
        Buffer.WriteLong(index)
        Buffer.WriteLong(PlayerHighIndex)
        Buffer.WriteString(Player(index).Name)
        Buffer.WriteLong(Player(index).Sprite)
        Buffer.WriteLong(Player(index).X)
        Buffer.WriteLong(Player(index).Y)
        Buffer.WriteLong(Player(index).Dir)
        SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub
    Sub SendClearPlayer(ByVal index As Long)
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer
        Buffer.WriteLong(ServerPackets.SClearPlayer)
        Buffer.WriteLong(index)
        Buffer.WriteLong(PlayerHighIndex)
        SendDataToAllBut(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Sub SendPosition(ByVal index As Long)
        Dim Buffer As clsBuffer
        Buffer = New clsBuffer
        Buffer.WriteLong(ServerPackets.SPosition)
        Buffer.WriteLong(index)
        Buffer.WriteLong(Player(index).Moving)
        Buffer.WriteLong(Player(index).X)
        Buffer.WriteLong(Player(index).Y)
        Buffer.WriteLong(Player(index).Dir)
        SendDataToAllBut(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub
End Module
