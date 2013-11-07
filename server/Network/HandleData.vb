Imports Lidgren.Network
Namespace Network.HandleData
    Public Module HandleData
        Public Sub HandleDataPackets(ByVal PacketNum As Integer, ByVal index As Integer, ByRef Data As NetIncomingMessage)
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
            If PacketNum = CEditorPackets.MapData Then HandleData.EditorMapData(index, Data)
            If PacketNum = CEditorPackets.PlayerData Then HandleData.EditorPlayerData(index, Data)
            If PacketNum = CEditorPackets.TilesetData Then HandleData.EditorTilesetData(index, Data)
        End Sub

        Public Sub Register(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim Email As String, Password As String

            Email = data.ReadString
            Password = data.ReadString

            If Not Accounts.Data.AccountExists(Email) Then
                Dim newAccount = New Accounts
                newAccount.Email = Email
                newAccount.Password = Password
                newAccount.Player = New PlayerBase
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

        Public Sub NewCharacter(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim Email As String, Name As String

            Email = data.ReadString
            Name = data.ReadString

            If Not Players.Data.PlayerExists(Name) Then
                Dim newAccount As New Accounts
                newAccount = Accounts.Data.GetAccount(Email)
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

        Public Sub Login(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim Name As String, Password As String

            Name = data.ReadString
            Password = data.ReadString

            Player(index) = New Players
            If Not Accounts.Data.AccountExists(Name) Then
                Console.WriteLine("Account " & Name & " Failed To Load! [Error: account does not exist]")
                SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: account does not exist]")
                Exit Sub
            Else
                If Not Accounts.Data.VerifyAccount(Name, Password) Then
                    Console.WriteLine("Account " & Name & " Failed To Load! [Error: username or password are incorrect]")
                    SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: username or password are incorrect]")
                    Exit Sub
                End If
                If Not Players.Data.HasPlayer(Accounts.Data.GetAccountIndex(Name)) Then
                    SendData.RegisterOk(index)
                    Exit Sub
                End If
                UpdateHighIndex()
                Player(index).Load(Accounts.Data.GetAccount(Name).Player.Name)
                Player(index).IsPlaying = True
                Players.Data.SendPlayers()
                NPCs.Data.SendNPCs()
                Tilesets.Data.SendTilesets()
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
                LuaScript.executeFunction("onLogin", index)
            End If
        End Sub

        Public Sub Message(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim ChatMode As String, Message As String

            ChatMode = data.ReadString
            Message = data.ReadString

            Select Case Player(index).AccessMode
                Case ACCESS.NONE : SendData.Message(ChatMode & Trim$(Player(index).Name) & ": " & Message)
                Case ACCESS.GMIT : SendData.Message(ChatMode & Trim$("(GMIT) " & Player(index).Name) & ": " & Message)
                Case ACCESS.GM : SendData.Message(ChatMode & Trim$("(GM) " & Player(index).Name) & ": " & Message)
                Case ACCESS.LEAD_GM : SendData.Message(ChatMode & Trim$("(Lead GM) " & Player(index).Name) & ": " & Message)
                Case ACCESS.DEV : SendData.Message(ChatMode & Trim$("(DEV) " & Player(index).Name) & ": " & Message)
                Case ACCESS.ADMIN : SendData.Message(ChatMode & Trim$("(Admin) " & Player(index).Name) & ": " & Message)
            End Select
        End Sub

        Public Sub Position(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim Moving As Boolean, X As Integer, Y As Integer, Dir As Integer

            Moving = data.ReadBoolean
            X = data.ReadInt32
            Y = data.ReadInt32
            Dir = data.ReadByte

            If Not Players.Logic.PlayerOnTile(X, Y) And Not NPCs.Logic.NpcOnTile(X, Y) And Not Tilesets.Data.isTileBlocked(Player(index).Map, X, Y) Then ' Send new position to others
                Player(index).Moving = Moving
                Player(index).X = X
                Player(index).Y = Y
                Player(index).Dir = Dir
                SendData.Position(index)
            Else ' Resend old position to self
                SendData.Position(index, True)
            End If
        End Sub

        Public Sub SetAdmin(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim i As Integer

            i = data.ReadInt32
            If Players.Logic.IsPlaying(index) Then
                If Player(index).AccessMode > ACCESS.NONE Then
                    If Players.Logic.IsPlaying(i) Then
                        Player(i).AccessMode = data.ReadInt32
                        SendData.Access(i)
                    End If
                Else
                    Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetAdmin (Error: invalid access level)")
                End If
            End If
        End Sub

        Public Sub SetVisible(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim i As Integer

            i = data.ReadInt32
            If Players.Logic.IsPlaying(index) Then
                If Player(index).AccessMode > ACCESS.NONE Then
                    If Players.Logic.IsPlaying(i) Then
                        Player(i).Visible = data.ReadBoolean
                        SendData.Position(i)
                        SendData.Visible(i)
                    End If
                Else
                    Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetVisible (Error: invalid access level)")
                End If
            End If
        End Sub

        Public Sub WarpTo(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim i As Integer

            i = data.ReadInt32
            If Players.Logic.IsPlaying(index) Then
                If Player(index).AccessMode > ACCESS.NONE Then
                    If Players.Logic.IsPlaying(i) Then
                        Player(index).Dir = Player(i).Dir
                        Player(index).X = Player(i).X
                        Player(index).Y = Player(i).Y
                        SendData.Position(index, True)
                    End If
                Else
                    Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpTo (Error: invalid access level)")
                End If
            End If
        End Sub

        Public Sub WarpToMe(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim i As Integer

            i = data.ReadInt32
            If Players.Logic.IsPlaying(index) Then
                If Player(index).AccessMode > ACCESS.NONE Then
                    If Players.Logic.IsPlaying(i) Then
                        Player(i).Dir = Player(index).Dir
                        Player(i).X = Player(index).X
                        Player(i).Y = Player(index).Y
                        SendData.Position(i, True)
                        SendData.MessageTo(index, "[System] " & Player(i).Name & " has been summoned!")
                    Else
                        SendData.MessageTo(index, "[System] " & "Player " & Player(i).Name & " not found!")
                    End If
                Else
                    Console.WriteLine("Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpToMe (Error: invalid access level)")
                End If
            End If

        End Sub

        Public Sub EditorLogin(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim Name As String, Password As String, Mode As Integer

            Name = data.ReadString
            Password = data.ReadString
            Mode = data.ReadInt32

            Player(index) = New Players()
            If Not Accounts.Data.AccountExists(Name) Then
                Console.WriteLine("Account " & Name & " Failed To Load! [Error: account does not exist]")
                SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: account does not exist]")
                Exit Sub
            Else
                If Not Accounts.Data.VerifyAccount(Name, Password) Then
                    Console.WriteLine("Account " & Name & " Failed To Load! [Error: username or password are incorrect]")
                    SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: username or password are incorrect]")
                    Exit Sub
                End If
                If Not Players.Data.HasPlayer(Accounts.Data.GetAccountIndex(Name)) Then
                    Console.WriteLine("Account " & Name & " Failed To Load! [Error: character missing]")
                    SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: character missing]")
                    Exit Sub
                End If
                UpdateHighIndex()
                Player(index).Load(Accounts.Data.GetAccount(Name).Player.Name)
                Player(index).IsPlaying = False
                SendData.EditorLoginOk(index, Mode)
                Console.WriteLine("Account: " & Name & " has logged into the editor!")
            End If
        End Sub

        Public Sub EditorDataRequest(ByVal index As Integer, ByRef data As NetIncomingMessage)
            SendData.EditorMapData(index)
            SendData.EditorTilesetData(index)
            SendData.EditorPlayerData(index)
            SendData.EditorNPCData(index)
            SendData.EditorDataSent(index, 1)
        End Sub

        Public Sub EditorMapData(ByVal Index As Integer, ByRef data As NetIncomingMessage)
            Dim num As Integer = 0
            Dim saveMap() As Maps
            Dim sTileData As TileData

            num = data.ReadInt32
            ReDim saveMap(0 To num)
            For i As Integer = 0 To num
                saveMap(i) = New Maps
                saveMap(i).Name = data.ReadString
                saveMap(i).MaxX = data.ReadInt32
                saveMap(i).MaxY = data.ReadInt32
                saveMap(i).Alpha = data.ReadByte
                saveMap(i).Red = data.ReadByte
                saveMap(i).Green = data.ReadByte
                saveMap(i).Blue = data.ReadByte
                For l As Integer = MapLayerEnum.Ground To MapLayerEnum.COUNT - 1
                    saveMap(i).Layer(l) = New LayerData(saveMap(i).MaxX, saveMap(i).MaxY)
                    For x As Integer = 0 To saveMap(i).MaxX - 1
                        For y As Integer = 0 To saveMap(i).MaxY - 1
                            sTileData = New TileData
                            sTileData.Tileset = data.ReadInt32
                            sTileData.X = data.ReadInt32
                            sTileData.Y = data.ReadInt32
                            saveMap(i).Layer(l).SetTileData(x, y, sTileData)
                        Next y
                    Next x
                Next l
                saveMap(i).Save()
            Next i
            Maps.Data.LoadAll()
            ' Send new maps to online players
            For i As Integer = 1 To PlayerCount
                SendData.MapData(i)
            Next
            SendData.EditorMapData(Index)
            SendData.EditorDataSent(Index, 0)
        End Sub

        Public Sub EditorPlayerData(ByVal Index As Integer, ByRef data As NetIncomingMessage)
            Dim num As Integer = 0
            Dim savePlayer() As Accounts

            num = data.ReadInt32
            ReDim savePlayer(0 To num)
            For i As Integer = 1 To num
                savePlayer(i) = New Accounts
                savePlayer(i).Email = data.ReadString
                savePlayer(i).Password = data.ReadString
                savePlayer(i).Player = New PlayerBase
                savePlayer(i).Player.Name = data.ReadString
                savePlayer(i).Player.Sprite = data.ReadInt32
                savePlayer(i).Player.Map = data.ReadInt32
                savePlayer(i).Player.X = data.ReadInt32
                savePlayer(i).Player.Y = data.ReadInt32
                savePlayer(i).Player.Dir = data.ReadByte
                savePlayer(i).Player.AccessMode = data.ReadByte
                savePlayer(i).Player.Visible = data.ReadBoolean
                savePlayer(i).Save()
            Next i
            Accounts.Data.LoadAccounts()
            SendData.EditorPlayerData(Index)
            SendData.EditorDataSent(Index, 0)
        End Sub

        Public Sub EditorTilesetData(ByVal Index As Integer, ByRef data As NetIncomingMessage)
            Dim saveTileset() As Tilesets, num As Integer

            num = data.ReadInt32
            ReDim saveTileset(0 To num)
            For i As Integer = 1 To num
                saveTileset(i) = New Tilesets
                saveTileset(i).ID = data.ReadInt32
                saveTileset(i).Name = data.ReadString
                saveTileset(i).MaxX = data.ReadInt32
                saveTileset(i).MaxY = data.ReadInt32
                saveTileset(i).ResizeTileData(New Integer() {saveTileset(i).MaxX, saveTileset(i).MaxY})
                For x As Integer = 0 To saveTileset(i).MaxX
                    For y As Integer = 0 To saveTileset(i).MaxY
                        saveTileset(i).Tile(x, y) = data.ReadByte
                    Next y
                Next x
                saveTileset(i).Save()
            Next i
            Tilesets.Data.LoadAll()
            SendData.EditorTilesetData(Index)
            SendData.EditorDataSent(Index, 0)
        End Sub
    End Module
End Namespace