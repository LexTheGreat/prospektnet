Module HandleData
    Public Sub HandleDataPackets(ByVal PacketNum As Long, ByRef Data() As Byte)
        ' Checks which is the command to run
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ServerPackets.SAlert Then HandleAlert(Data)
        If PacketNum = ServerPackets.SLoginOk Then HandleLoginOk(Data)
        If PacketNum = ServerPackets.SRegisterOk Then HandleRegisterOk(Data)
        If PacketNum = ServerPackets.SPlayer Then HandlePlayer(Data)
        If PacketNum = ServerPackets.SClearPlayer Then HandleClearPlayer(Data)
        If PacketNum = ServerPackets.SPosition Then HandlePosition(Data)
        If PacketNum = ServerPackets.SMessage Then HandleMessage(Data)
        If PacketNum = ServerPackets.SAccess Then HandleAccess(Data)
        If PacketNum = ServerPackets.SVisible Then HandleVisible(Data)
    End Sub

    Private Sub HandleAlert(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        MessageBox.Show(Buffer.ReadString)
        Buffer = Nothing
        faderState = 2
        faderAlpha = 0
        If curMenu = MenuEnum.Login Or curMenu = MenuEnum.Register Or curMenu = MenuEnum.Creation Then
            sUser = vbNullString
            sPass = vbNullString
            sHidden = vbNullString
            sCharacter = vbNullString
            curTextbox = 0
            loginSent = False
            curMenu = MenuEnum.Main
        End If
    End Sub

    Private Sub HandleRegisterOk(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        Buffer = Nothing
        loginSent = False
        curMenu = MenuEnum.Creation
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
        Player(tempIndex).Load(Buffer.ReadString, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadBool)
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
        Dim Buffer As ByteBuffer, Message As String, Messages As String(), I As Integer
        ReDim Preserve Messages(0 To maxChatLines)
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        Message = Buffer.ReadString
        Buffer = Nothing

        ' Check if the message is larger then maxChatChars
        If (Message.Length > maxChatChars) Then
            'Resize the Messages array
            ReDim Preserve Messages(0 To Convert.ToInt32(Message.Length / maxChatChars))
            'Split the message into maxChatChars character chunks
            Messages = WordWarp(Message, maxChatChars)
        Else
            'Resize the Messages array
            ReDim Preserve Messages(0 To 0)
            Messages(0) = Message
        End If

        'Loop through all chunks of the Message and display
        For X As Integer = 0 To (Messages.Length - 1)
            For I = 1 To maxChatLines
                If Len(Trim(chatbuffer(I))) = 0 Then
                    chatbuffer(I) = Messages(X)
                    Exit For
                End If
            Next I

            For I = 1 To maxChatLines
                If I < maxChatLines Then
                    chatbuffer(I) = chatbuffer(I + 1)
                Else
                    chatbuffer(I) = Messages(X)
                End If
            Next I
        Next X
    End Sub

    Private Sub HandleAccess(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        Player(MyIndex).SetAccess(Buffer.ReadLong)
        Buffer = Nothing
    End Sub

    Private Sub HandleVisible(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        Player(tempIndex).Visible = Buffer.ReadBool
        Buffer = Nothing
    End Sub
End Module
