Module HandleData
    Public Sub HandleDataPackets(ByVal PacketNum As Long, ByRef Data() As Byte)
        ' Checks which is the command to run
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ServerPackets.SLoginOk Then HandleLoginOk(Data)
        If PacketNum = ServerPackets.SPlayer Then HandlePlayer(Data)
        If PacketNum = ServerPackets.SClearPlayer Then HandleClearPlayer(Data)
        If PacketNum = ServerPackets.SPosition Then HandlePosition(Data)
        If PacketNum = ServerPackets.SMessage Then HandleMessage(Data)
    End Sub

    Private Sub HandleLoginOk(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        MyIndex = Buffer.ReadLong
        Buffer = Nothing
        faderState = 3
        faderAlpha = 0
    End Sub

    Private Sub HandlePlayer(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        PlayerHighindex = Buffer.ReadLong
        If IsNothing(Player(tempIndex)) Then Player(tempIndex) = New Players
        Player(tempIndex).Load(Buffer.ReadString, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong)
        Buffer = Nothing
    End Sub

    Private Sub HandleClearPlayer(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        PlayerHighindex = Buffer.ReadLong
        Player(tempIndex) = Nothing
        Buffer = Nothing
    End Sub

    Private Sub HandlePosition(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
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

    Private Sub HandleMessage(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer, Message As String, I As Integer
        Buffer = New ByteBuffer
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
