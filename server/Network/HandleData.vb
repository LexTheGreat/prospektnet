Class HandleData
    Public Shared Sub HandleDataPackets(ByVal PacketNum As Long, ByVal index As Long, ByRef Data() As Byte)
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ClientPackets.Register Then HandleData.Register(index, Data)
        If PacketNum = ClientPackets.NewCharacter Then HandleData.NewCharacter(index, Data)
        If PacketNum = ClientPackets.Login Then HandleData.Login(index, Data)
        If PacketNum = ClientPackets.Message Then HandleData.Message(index, Data)
        If PacketNum = ClientPackets.Position Then HandleData.Position(index, Data)
        If PacketNum = ClientPackets.SetAccess Then HandleData.SetAdmin(index, Data)
        If PacketNum = ClientPackets.SetVisible Then HandleData.SetVisible(index, Data)
        If PacketNum = ClientPackets.WarpTo Then HandleData.WarpTo(index, Data)
        If PacketNum = ClientPackets.WarpToMe Then HandleData.WarpToMe(index, Data)

        ' Editor packets
        If PacketNum = CEditorPackets.Login Then HandleData.EditorLogin(index, Data)
        If PacketNum = CEditorPackets.DataRequest Then HandleData.EditorDataRequest(index, Data)
        If PacketNum = CEditorPackets.Data Then HandleData.EditorData(index, Data)
    End Sub

    Public Shared Sub Register(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim Email As String, Password As String

        Buffer.WriteBytes(data)
        Email = Buffer.ReadString
        Password = Buffer.ReadString

        If Not AccountData.AccountExists(Email) Then
            Dim newAccount = New Accounts
            newAccount.Email = Email
            newAccount.Password = Password
            newAccount.Player = New Players
            If newAccount.Create() = False Then
                Console.WriteLine("Account " & Email & " Failed To Register! [Error: email already taken]")
                SendData.Alert(index, "Account " & Email & " Failed To Register!" & vbNewLine & "[Error: email already taken]")
                Exit Sub
            End If
            SendData.RegisterOk(index)
            Console.WriteLine("Account " & Email & " has been registered.")
        Else
            Console.WriteLine("Account " & Email & " Failed To Register! [Error: email already taken]")
            SendData.Alert(index, "Account " & Email & " Failed To Register!" & vbNewLine & "[Error: email already taken]")
        End If
    End Sub

    Public Shared Sub NewCharacter(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim Email As String, Name As String

        Buffer.WriteBytes(data)
        Email = Buffer.ReadString
        Name = Buffer.ReadString

        If Not PlayerData.PlayerExists(Name) Then
            Dim newAccount As New Accounts
            newAccount = AccountData.GetAccount(Email)
            newAccount.Player.Name = Name
            If newAccount.Email = vbNullString Or newAccount.NewCharacter() = False Then
                Console.WriteLine("Character " & Name & " Failed To Create! [Error: Account does not exist]")
                SendData.Alert(index, "Character " & Name & " Failed To Create!" & vbNewLine & "[Error: Account does not exist]")
                Exit Sub
            End If
            SendData.Alert(index, "Your character has been created. You may now login!")
            Console.WriteLine("Character " & Name & " has been created.")
        Else
            Console.WriteLine("Character " & Name & " Failed To Create! [Error: Name already taken]")
            SendData.Alert(index, "Character " & Name & " Failed To Create!" & vbNewLine & "[Error: Name already taken]")
        End If

    End Sub

    Public Shared Sub Login(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim Name As String, Password As String

        Buffer.WriteBytes(data)
        Name = Buffer.ReadString
        Password = Buffer.ReadString

        Player(index) = New Players()
        If Not AccountData.AccountExists(Name) Then
            Console.WriteLine("Account " & Name & " Failed To Load! [Error: account does not exist]")
            SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: account does not exist]")
            Exit Sub
        Else
            If Not AccountData.VerifyAccount(Name, Password) Then
                Console.WriteLine("Account " & Name & " Failed To Load! [Error: username or password are incorrect]")
                SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: username or password are incorrect]")
                Exit Sub
            End If
            If Not PlayerData.HasPlayer(AccountData.GetAccountIndex(Name)) Then
                SendData.RegisterOk(index)
                Exit Sub
            End If
            PlayerLogic.UpdateHighIndex()
            Player(index).Load(AccountData.GetAccount(Name).Player.Name)
            Player(index).SetIsPlaying(True)
            PlayerData.SendPlayers()
            NPCData.SendNPCs()
            SendData.MapData(index)
            SendData.LoginOk(index)

            Select Case Player(index).AccessMode
                Case ACCESS.NONE : SendData.Message(Trim$(Player(index).Name) & " has logged in.")
                Case ACCESS.GMIT : SendData.Message(Trim$("(GMIT) " & Player(index).Name) & " has logged in.")
                Case ACCESS.GM : SendData.Message(Trim$("(GM) " & Player(index).Name) & " has logged in.")
                Case ACCESS.LEAD_GM : SendData.Message(Trim$("(Lead GM) " & Player(index).Name) & " has logged in.")
                Case ACCESS.DEV : SendData.Message(Trim$("(DEV) " & Player(index).Name) & " has logged in.")
                Case ACCESS.ADMIN : SendData.Message(Trim$("(Admin) " & Player(index).Name) & " has logged in.")
            End Select
            Console.WriteLine(Name & " has logged in.")
        End If
    End Sub

    Public Shared Sub Message(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer, ChatMode As String, Message As String

        Buffer.WriteBytes(data)
        ChatMode = Buffer.ReadString
        Message = Buffer.ReadString

        Select Case Player(index).AccessMode
            Case ACCESS.NONE : SendData.Message(ChatMode & Trim$(Player(index).Name) & ": " & Message)
            Case ACCESS.GMIT : SendData.Message(ChatMode & Trim$("(GMIT) " & Player(index).Name) & ": " & Message)
            Case ACCESS.GM : SendData.Message(ChatMode & Trim$("(GM) " & Player(index).Name) & ": " & Message)
            Case ACCESS.LEAD_GM : SendData.Message(ChatMode & Trim$("(Lead GM) " & Player(index).Name) & ": " & Message)
            Case ACCESS.DEV : SendData.Message(ChatMode & Trim$("(DEV) " & Player(index).Name) & ": " & Message)
            Case ACCESS.ADMIN : SendData.Message(ChatMode & Trim$("(Admin) " & Player(index).Name) & ": " & Message)
        End Select
    End Sub

    Public Shared Sub Position(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim Moving As Boolean, X As Integer, Y As Integer, Dir As Integer

        Buffer.WriteBytes(data)
        Moving = Buffer.ReadInteger
        X = Buffer.ReadInteger
        Y = Buffer.ReadInteger
        Dir = Buffer.ReadInteger

        If Not PlayerLogic.PlayerOnTile(X, Y) And Not NPCLogic.NpcOnTile(X, Y) Then ' Send new position to others
            Player(index).SetMoving(Moving)
            Player(index).SetPosition(New Integer() {X, Y})
            Player(index).SetDir(Dir)
            SendData.Position(index)
        Else ' Resend old position to self
            SendData.Position(index, True)
        End If
    End Sub

    Public Shared Sub SetAdmin(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim i As Integer

        Buffer.WriteBytes(data)
        i = Buffer.ReadInteger
        If PlayerLogic.IsPlaying(index) Then
            If Player(index).AccessMode > ACCESS.NONE Then
                If PlayerLogic.IsPlaying(i) Then
                    Player(i).AccessMode = Buffer.ReadInteger
                    SendData.Access(i)
                Else

                    Buffer.WriteInteger(ServerPackets.Message)
                    Buffer.WriteString("[System] " & "Player " & Player(i).Name & " not found!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                End If
            Else
                Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetAdmin (Error: invalid access level)")

                Buffer.WriteInteger(ServerPackets.Message)
                Buffer.WriteString("[System] " & "Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetAdmin (Error: invalid access level)")
                Networking.SendDataToAdmins(Buffer.ToArray())
            End If
        End If
    End Sub

    Public Shared Sub SetVisible(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim i As Integer

        Buffer.WriteBytes(data)
        i = Buffer.ReadInteger
        If PlayerLogic.IsPlaying(index) Then
            If Player(index).AccessMode > ACCESS.NONE Then
                If PlayerLogic.IsPlaying(i) Then
                    Player(i).Visible = Buffer.ReadBool
                    SendData.Position(i)
                    SendData.Visible(i)
                Else

                    Buffer.WriteInteger(ServerPackets.Message)
                    Buffer.WriteString("[System] " & "Player " & Player(i).Name & " not found!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                End If
            Else
                Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetVisible (Error: invalid access level)")

                Buffer.WriteInteger(ServerPackets.Message)
                Buffer.WriteString("[System] " & "Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetVisible (Error: invalid access level)")
                Networking.SendDataToAdmins(Buffer.ToArray())
            End If
        End If
    End Sub

    Public Shared Sub WarpTo(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim i As Integer

        Buffer.WriteBytes(data)
        i = Buffer.ReadInteger
        If PlayerLogic.IsPlaying(index) Then
            If Player(index).AccessMode > ACCESS.NONE Then
                If PlayerLogic.IsPlaying(i) Then
                    Player(index).SetDir(Player(i).Dir)
                    Player(index).SetPosition(New Integer() {Player(i).X, Player(i).Y})
                    SendData.Position(index, True)

                    Buffer.WriteInteger(ServerPackets.Message)
                    Buffer.WriteString("[System] You have been summoned to " & Player(i).Name & "!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                Else

                    Buffer.WriteInteger(ServerPackets.Message)
                    Buffer.WriteString("[System] " & "Player " & Player(i).Name & " not found!")
                    Networking.SendDataTo(index, Buffer.ToArray())
                End If
            Else
                Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpTo (Error: invalid access level)")

                Buffer.WriteInteger(ServerPackets.Message)
                Buffer.WriteString("[System] " & "Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpTo (Error: invalid access level)")
                Networking.SendDataToAdmins(Buffer.ToArray())
            End If
        End If
    End Sub

    Public Shared Sub WarpToMe(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim i As Integer

        Buffer.WriteBytes(data)
        i = Buffer.ReadInteger
        If PlayerLogic.IsPlaying(index) Then
            If Player(index).AccessMode > ACCESS.NONE Then
                If PlayerLogic.IsPlaying(i) Then
                    Player(i).SetDir(Player(index).Dir())
                    Player(i).SetPosition(New Integer() {Player(index).X, Player(index).Y})
                    SendData.Position(i, True)
                    SendData.MessageTo(index, "[System] " & Player(i).Name & " has been summoned!")
                Else
                    SendData.MessageTo(index, "[System] " & "Player " & Player(i).Name & " not found!")
                End If
            Else
                Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpToMe (Error: invalid access level)")

                Buffer.WriteInteger(ServerPackets.Message)
                Buffer.WriteString("[System] " & "Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpToMe (Error: invalid access level)")
                Networking.SendDataToAdmins(Buffer.ToArray())
            End If
        End If

    End Sub

    Public Shared Sub EditorLogin(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim Name As String, Password As String, Mode As Integer
        Buffer.WriteBytes(data)
        Name = Buffer.ReadString
        Password = Buffer.ReadString
        Mode = Buffer.ReadInteger

        Player(index) = New Players()
        If Not AccountData.AccountExists(Name) Then
            Console.WriteLine("Account " & Name & " Failed To Load! [Error: account does not exist]")
            SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: account does not exist]")
            Exit Sub
        Else
            If Not AccountData.VerifyAccount(Name, Password) Then
                Console.WriteLine("Account " & Name & " Failed To Load! [Error: username or password are incorrect]")
                SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: username or password are incorrect]")
                Exit Sub
            End If
            If Not PlayerData.HasPlayer(AccountData.GetAccountIndex(Name)) Then
                Console.WriteLine("Account " & Name & " Failed To Load! [Error: character missing]")
                SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: character missing]")
                Exit Sub
            End If
            PlayerLogic.UpdateHighIndex()
            Player(index).Load(AccountData.GetAccount(Name).Player.Name)
            Player(index).SetIsPlaying(False)
            SendData.EditorLoginOk(index, Mode)
            SendData.EditorData(index)
            Console.WriteLine("Account: " & Name & " has logged into the editor!")
        End If
    End Sub

    Public Shared Sub EditorDataRequest(ByVal index As Long, ByVal data() As Byte)
        SendData.EditorData(index)
        SendData.EditorDataSent(index, 1)
    End Sub

    Public Shared Sub EditorData(ByVal Index As Long, ByRef Data() As Byte)
        Dim num As Integer = 0
        Dim savePlayer() As Accounts
        Dim saveMap() As MapStructure
        Dim sTileData As TileData
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(Data)
        num = Buffer.ReadInteger
        ReDim savePlayer(0 To num)
        For i As Integer = 0 To num
            savePlayer(i) = New Accounts
            savePlayer(i).Email = Buffer.ReadString
            savePlayer(i).Password = Buffer.ReadString
            savePlayer(i).Player = New Players
            savePlayer(i).Player.Name = Buffer.ReadString
            savePlayer(i).Player.Sprite = Buffer.ReadInteger
            savePlayer(i).Player.Map = Buffer.ReadInteger
            savePlayer(i).Player.X = Buffer.ReadInteger
            savePlayer(i).Player.Y = Buffer.ReadInteger
            savePlayer(i).Player.Dir = Buffer.ReadInteger
            savePlayer(i).Player.AccessMode = Buffer.ReadInteger
            savePlayer(i).Player.Visible = Buffer.ReadInteger
            savePlayer(i).Player.Save()
        Next i
        AccountData.LoadAccounts()
        num = Buffer.ReadInteger
        ReDim saveMap(0 To num)
        For i As Integer = 0 To num
            saveMap(i) = New MapStructure
            saveMap(i).Name = Buffer.ReadString
            saveMap(i).MaxX = Buffer.ReadInteger
            saveMap(i).MaxY = Buffer.ReadInteger
            saveMap(i).Alpha = Buffer.ReadInteger
            saveMap(i).Red = Buffer.ReadInteger
            saveMap(i).Green = Buffer.ReadInteger
            saveMap(i).Blue = Buffer.ReadInteger
            For l As Integer = MapLayerEnum.Ground To MapLayerEnum.FringeMask
                saveMap(i).Layer(l) = New LayerData(saveMap(i).MaxX, saveMap(i).MaxY)
                For x As Integer = 0 To saveMap(i).MaxX - 1
                    For y As Integer = 0 To saveMap(i).MaxY - 1
                        sTileData = New TileData
                        sTileData.Tileset = Buffer.ReadLong
                        sTileData.X = Buffer.ReadLong
                        sTileData.Y = Buffer.ReadLong
                        saveMap(i).Layer(l).SetTileData(x, y, sTileData)
                    Next y
                Next x
            Next l
            saveMap(i).Save()
        Next i
        MapData.LoadMaps()
        ' Send new maps to online players
        For i As Integer = 1 To PlayerHighIndex
            SendData.MapData(i)
        Next
        SendData.EditorData(Index)
        SendData.EditorDataSent(Index, 0)
    End Sub
End Class
