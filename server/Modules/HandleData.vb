Module HandleData
    Public Sub HandleDataPackets(ByVal PacketNum As Long, ByVal index As Long, ByRef Data() As Byte)
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ClientPackets.CLogin Then HandleLogin(index, Data)
        If PacketNum = ClientPackets.CPosition Then HandlePosition(index, Data)
        If PacketNum = ClientPackets.CMessage Then HandleMessage(index, Data)
    End Sub
    Private Sub HandleLogin(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim Name As String, Password As String

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        Name = Buffer.ReadString
        Password = Buffer.ReadString
        Buffer = Nothing
        Networking.UpdateHighIndex()
        Player(index) = New Players
        If Not AccountExists(Name) Then
            Player(index).Create(Name, Password)
            Player(index).Name = Name
            Player(index).Password = Password
            Player(index).X = 1
            Player(index).Y = 5
            Player(index).Dir = 1
            Player(index).Sprite = 1
            Player(index).Save()
            Console.WriteLine("Account " & Name & " has been created.")
        Else
            Player(index).Load(Name)
            Console.WriteLine("Account " & Name & " has been loaded.")
        End If
        Player(index).isPlaying = True
        SendPlayers()
        SendLoginOk(index)
        SendMessage(Name & " has logged in.")
        Console.WriteLine(Name & " has logged in.")
    End Sub

    Private Sub HandlePosition(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        Player(index).Moving = Buffer.ReadLong
        Player(index).X = Buffer.ReadLong
        Player(index).Y = Buffer.ReadLong
        Player(index).Dir = Buffer.ReadLong
        Buffer = Nothing
        SendPosition(index)
    End Sub

    Private Sub HandleMessage(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer, Message As String

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        Message = Buffer.ReadString
        Buffer = Nothing
        SendMessage(Trim(Player(index).Name) & ": " & Message)
    End Sub

End Module
