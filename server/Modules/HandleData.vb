Module HandleData
    Public Sub HandleDataPackets(ByVal PacketNum As Long, ByVal index As Long, ByRef Data() As Byte)
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ClientPackets.CRegister Then HandleRegister(index, Data)
        If PacketNum = ClientPackets.CNewCharacter Then HandleNewCharacter(index, Data)
        If PacketNum = ClientPackets.CLogin Then HandleLogin(index, Data)
        If PacketNum = ClientPackets.CMessage Then HandleMessage(index, Data)
        If PacketNum = ClientPackets.CPosition Then HandlePosition(index, Data)
        If PacketNum = ClientPackets.CSetAccess Then HandleSetAdmin(index, Data)
        If PacketNum = ClientPackets.CSetVisible Then HandleSetVisible(index, Data)
        If PacketNum = ClientPackets.CWarpTo Then HandleWarpTo(index, Data)
        If PacketNum = ClientPackets.CWarpToMe Then HandleWarpToMe(index, Data)
    End Sub

    Private Sub HandleRegister(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim Email As String, Password As String

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        Email = Buffer.ReadString
        Password = Buffer.ReadString
        Buffer = Nothing
        If Not AccountData.AccountExists(Email) Then
            Dim newAccount = New Accounts
            newAccount.Email = Email
            newAccount.Password = Password
            newAccount.Player = New Players
            If newAccount.Create() = False Then
                Console.WriteLine("Account " & Email & " Failed To Register! [Error: email already taken]")
                SendAlert(index, "Account " & Email & " Failed To Register!" & vbNewLine & "[Error: email already taken]")
                Exit Sub
            End If
            SendRegisterOk(index)
            Console.WriteLine("Account " & Email & " has been registered.")
        Else
            Console.WriteLine("Account " & Email & " Failed To Register! [Error: email already taken]")
            SendAlert(index, "Account " & Email & " Failed To Register!" & vbNewLine & "[Error: email already taken]")
        End If
    End Sub

    Private Sub HandleNewCharacter(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim Email As String, Name As String

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        Email = Buffer.ReadString
        Name = Buffer.ReadString
        Buffer = Nothing
        If Not AccountData.CharacterExists(Name) Then
            Dim newAccount As New Accounts
            newAccount = AccountData.GetAccount(Email)
            newAccount.Player.Name = Name
            If newAccount.Email = vbNullString Or newAccount.NewCharacter() = False Then
                Console.WriteLine("Character " & Name & " Failed To Create! [Error: Account does not exist]")
                SendAlert(index, "Character " & Name & " Failed To Create!" & vbNewLine & "[Error: Account does not exist]")
                Exit Sub
            End If
            SendAlert(index, "Your character has been created. You may now login!")
            Console.WriteLine("Character " & Name & " has been created.")
        Else
            Console.WriteLine("Character " & Name & " Failed To Create! [Error: Name already taken]")
            SendAlert(index, "Character " & Name & " Failed To Create!" & vbNewLine & "[Error: Name already taken]")
        End If

    End Sub

    Private Sub HandleLogin(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim Name As String, Password As String

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        Name = Buffer.ReadString
        Password = Buffer.ReadString
        Buffer = Nothing
        Player(index) = New Players()
        If Not AccountData.AccountExists(Name) Then
            Console.WriteLine("Account " & Name & " Failed To Load! [Error: account does not exist]")
            SendAlert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: account does not exist]")
            Exit Sub
        Else
            If Not AccountData.VerifyAccount(Name, Password) Then
                Console.WriteLine("Account " & Name & " Failed To Load! [Error: username or password are incorrect]")
                SendAlert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: username or password are incorrect]")
                Exit Sub
            End If
            If Not AccountData.HasCharacter(GetAccountIndex(Name)) Then
                SendRegisterOk(index)
                Exit Sub
            End If
            Networking.UpdateHighIndex()
            Player(index).Load(AccountData.GetAccount(Name).Player.Name)
            Player(index).SetIsPlaying(True)
            SendPlayers()
            SendNPCs()
            SendLoginOk(index)

            Select Case Player(index).AccessMode
                Case ACCESS.NONE : SendMessage(Trim$(Player(index).Name) & " has logged in.")
                Case ACCESS.GMIT : SendMessage(Trim$("(GMIT) " & Player(index).Name) & " has logged in.")
                Case ACCESS.GM : SendMessage(Trim$("(GM) " & Player(index).Name) & " has logged in.")
                Case ACCESS.LEAD_GM : SendMessage(Trim$("(Lead GM) " & Player(index).Name) & " has logged in.")
                Case ACCESS.DEV : SendMessage(Trim$("(DEV) " & Player(index).Name) & " has logged in.")
                Case ACCESS.ADMIN : SendMessage(Trim$("(Admin) " & Player(index).Name) & " has logged in.")
            End Select
            Console.WriteLine(Name & " has logged in.")
        End If
    End Sub

    Private Sub HandleMessage(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer, ChatMode As String, Message As String

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        ChatMode = Buffer.ReadString
        Message = Buffer.ReadString
        Buffer = Nothing

        Select Case Player(index).AccessMode
            Case ACCESS.NONE : SendMessage(ChatMode & Trim$(Player(index).Name) & ": " & Message)
            Case ACCESS.GMIT : SendMessage(ChatMode & Trim$("(GMIT) " & Player(index).Name) & ": " & Message)
            Case ACCESS.GM : SendMessage(ChatMode & Trim$("(GM) " & Player(index).Name) & ": " & Message)
            Case ACCESS.LEAD_GM : SendMessage(ChatMode & Trim$("(Lead GM) " & Player(index).Name) & ": " & Message)
            Case ACCESS.DEV : SendMessage(ChatMode & Trim$("(DEV) " & Player(index).Name) & ": " & Message)
            Case ACCESS.ADMIN : SendMessage(ChatMode & Trim$("(Admin) " & Player(index).Name) & ": " & Message)
        End Select
    End Sub

    Private Sub HandlePosition(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim Moving As Boolean, X As Integer, Y As Integer, Dir As Integer

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        Moving = Buffer.ReadLong
        X = Buffer.ReadLong
        Y = Buffer.ReadLong
        Dir = Buffer.ReadLong
        Buffer = Nothing
        If Not PlayerOnTile(X, Y) And Not NpcOnTile(X, Y) Then ' Send new position to others
            Player(index).SetMoving(Moving)
            Player(index).SetPosition(New Integer() {X, Y})
            Player(index).SetDir(Dir)
            SendPosition(index)
        Else ' Resend old position to self
            SendPosition(index, True)
        End If
    End Sub

    Private Sub HandleSetAdmin(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim i As Integer

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        i = Buffer.ReadLong
        If Networking.IsPlaying(index) Then
            If Player(index).AccessMode > ACCESS.NONE Then
                If Networking.IsPlaying(i) Then
                    Player(i).AccessMode = Buffer.ReadLong
                    SendAccess(i)
                Else
                    Buffer = New ByteBuffer
                    Buffer.WriteLong(ServerPackets.SMessage)
                    Buffer.WriteString("[System] " & "Player " & Player(i).Name & " not found!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                End If
            Else
                Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetAdmin (Error: invalid access level)")
                Buffer = New ByteBuffer
                Buffer.WriteLong(ServerPackets.SMessage)
                Buffer.WriteString("[System] " & "Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetAdmin (Error: invalid access level)")
                Networking.SendDataToAdmins(Buffer.ToArray())
            End If
        End If
        Buffer = Nothing
    End Sub

    Private Sub HandleSetVisible(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim i As Integer

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        i = Buffer.ReadLong
        If Networking.IsPlaying(index) Then
            If Player(index).AccessMode > ACCESS.NONE Then
                If Networking.IsPlaying(i) Then
                    Player(i).Visible = Buffer.ReadBool
                    SendPosition(i)
                    SendVisible(i)
                Else
                    Buffer = New ByteBuffer
                    Buffer.WriteLong(ServerPackets.SMessage)
                    Buffer.WriteString("[System] " & "Player " & Player(i).Name & " not found!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                End If
            Else
                Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetVisible (Error: invalid access level)")
                Buffer = New ByteBuffer
                Buffer.WriteLong(ServerPackets.SMessage)
                Buffer.WriteString("[System] " & "Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetVisible (Error: invalid access level)")
                Networking.SendDataToAdmins(Buffer.ToArray())
            End If
        End If
        Buffer = Nothing
    End Sub

    Private Sub HandleWarpTo(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim i As Integer

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        i = Buffer.ReadLong
        If Networking.IsPlaying(index) Then
            If Player(index).AccessMode > ACCESS.NONE Then
                If Networking.IsPlaying(i) Then
                    Player(index).SetDir(Player(i).Dir)
                    Player(index).SetPosition(New Integer() {Player(i).X, Player(i).Y})
                    SendPosition(index, True)
                    Buffer = New ByteBuffer
                    Buffer.WriteLong(ServerPackets.SMessage)
                    Buffer.WriteString("[System] You have been summoned to " & Player(i).Name & "!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                Else
                    Buffer = New ByteBuffer
                    Buffer.WriteLong(ServerPackets.SMessage)
                    Buffer.WriteString("[System] " & "Player " & Player(i).Name & " not found!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                End If
            Else
                Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpTo (Error: invalid access level)")
                Buffer = New ByteBuffer
                Buffer.WriteLong(ServerPackets.SMessage)
                Buffer.WriteString("[System] " & "Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpTo (Error: invalid access level)")
                Networking.SendDataToAdmins(Buffer.ToArray())
            End If
        End If
        Buffer = Nothing
    End Sub

    Private Sub HandleWarpToMe(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As ByteBuffer
        Dim i As Integer

        Buffer = New ByteBuffer
        Buffer.WriteBytes(data)
        i = Buffer.ReadLong
        If Networking.IsPlaying(index) Then
            If Player(index).AccessMode > ACCESS.NONE Then
                If Networking.IsPlaying(i) Then
                    Player(i).SetDir(Player(index).Dir())
                    Player(i).SetPosition(New Integer() {Player(index).X, Player(index).Y})
                    SendPosition(i, True)
                    Buffer = New ByteBuffer
                    Buffer.WriteLong(ServerPackets.SMessage)
                    Buffer.WriteString("[System] " & Player(i).Name & " has been summoned!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                Else
                    Buffer = New ByteBuffer
                    Buffer.WriteLong(ServerPackets.SMessage)
                    Buffer.WriteString("[System] " & "Player " & Player(i).Name & " not found!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                End If
            Else
                Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpToMe (Error: invalid access level)")
                Buffer = New ByteBuffer
                Buffer.WriteLong(ServerPackets.SMessage)
                Buffer.WriteString("[System] " & "Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpToMe (Error: invalid access level)")
                Networking.SendDataToAdmins(Buffer.ToArray())
            End If
        End If
        Buffer = Nothing
    End Sub

End Module
