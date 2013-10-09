Class HandleData
    Public Shared Sub HandleDataPackets(ByVal PacketNum As Long, ByRef Data() As Byte)
        ' Checks which is the command to run
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ServerPackets.SAlert Then HandleData.Alert(Data)
        If PacketNum = ServerPackets.SLoginOk Then HandleData.LoginOk(Data)
        If PacketNum = ServerPackets.SRegisterOk Then HandleData.RegisterOk(Data)
        If PacketNum = ServerPackets.SPlayer Then HandleData.PlayerData(Data)
        If PacketNum = ServerPackets.SClearPlayer Then HandleData.ClearPlayer(Data)
        If PacketNum = ServerPackets.SPosition Then HandleData.Position(Data)
        If PacketNum = ServerPackets.SMessage Then HandleData.Message(Data)
        If PacketNum = ServerPackets.SAccess Then HandleData.Access(Data)
        If PacketNum = ServerPackets.SVisible Then HandleData.Visible(Data)
        If PacketNum = ServerPackets.SNPC Then HandleData.NPCData(Data)
        If PacketNum = ServerPackets.SNPCPosition Then HandleData.NPCPosition(Data)
    End Sub

    Public Shared Sub Alert(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        MessageBox.Show(Buffer.ReadString)
        Buffer = Nothing
        faderState = 2
        faderAlpha = 0
        If curMenu = MenuEnum.Login Or curMenu = MenuEnum.Register Or curMenu = MenuEnum.Creation Then
            sEmail = vbNullString
            sPass = vbNullString
            sHidden = vbNullString
            sCharacter = vbNullString
            curTextbox = 0
            loginSent = False
            curMenu = MenuEnum.Main
        End If
    End Sub

    Public Shared Sub RegisterOk(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        Buffer = Nothing
        loginSent = False
        curMenu = MenuEnum.Creation
    End Sub

    Public Shared Sub LoginOk(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        MyIndex = Buffer.ReadLong
        Buffer = Nothing
        faderState = 3
        faderAlpha = 0
    End Sub

    Public Shared Sub PlayerData(ByRef Data() As Byte)
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

    Public Shared Sub ClearPlayer(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        PlayerHighindex = Buffer.ReadLong
        Player(tempIndex) = Nothing
        Buffer = Nothing
    End Sub

    Public Shared Sub Position(ByRef Data() As Byte)
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

    Public Shared Sub Message(ByRef Data() As Byte)
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

    Public Shared Sub Access(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        Player(MyIndex).SetAccess(Buffer.ReadLong)
        Buffer = Nothing
    End Sub

    Public Shared Sub Visible(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        Player(tempIndex).Visible = Buffer.ReadBool
        Buffer = Nothing
    End Sub

    Public Shared Sub NPCData(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        NPCCount = Buffer.ReadLong
        ReDim Preserve NPC(0 To NPCCount)
        If IsNothing(NPC(tempIndex)) Then NPC(tempIndex) = New NPCs
        NPC(tempIndex).Load(Buffer.ReadString, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong)
        Buffer = Nothing
    End Sub

    Public Shared Sub NPCPosition(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        If IsNothing(NPC(tempIndex)) Then Exit Sub
        If Not IsNothing(NPC(tempIndex)) Then
            NPC(tempIndex).Moving = Buffer.ReadLong
            NPC(tempIndex).X = Buffer.ReadLong
            NPC(tempIndex).Y = Buffer.ReadLong
            NPC(tempIndex).Dir = Buffer.ReadLong
        End If
        Select Case NPC(tempIndex).Dir
            Case DirEnum.Up
                NPC(tempIndex).YOffset = picY
            Case DirEnum.Down
                NPC(tempIndex).YOffset = picY * -1
            Case DirEnum.Left
                NPC(tempIndex).XOffset = picX
            Case DirEnum.Right
                NPC(tempIndex).XOffset = picX * -1
        End Select
        Buffer = Nothing
    End Sub
End Class
