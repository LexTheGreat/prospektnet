Imports Lidgren.Network
Imports IHProspekt.Objects
Imports IHProspekt.Network
Imports IHProspekt.Core
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
            If PacketNum = CEditorPackets.AccountData Then HandleData.EditorPlayerData(index, Data)
            If PacketNum = CEditorPackets.TilesetData Then HandleData.EditorTilesetData(index, Data)
            If PacketNum = CEditorPackets.NPCData Then HandleData.EditorNpcData(index, Data)
            If PacketNum = CEditorPackets.ItemData Then HandleData.EditorItemData(index, Data)
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
                    Server.Writeline("Account " & Email & " Failed To Register! [Error: email already taken]")
                    SendData.Alert(index, "Account " & Email & " Failed To Register!" & vbNewLine & "[Error: email already taken]")
                    Exit Sub
                End If
                SendData.RegisterOk(index)
                Server.Writeline("Account " & Email & " has been registered.")
            Else
                Server.Writeline("Account " & Email & " Failed To Register! [Error: email already taken]")
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
                    Server.Writeline("Character " & Name & " Failed To Create! [Error: Account does not exist]")
                    SendData.Alert(index, "Character " & Name & " Failed To Create!" & vbNewLine & "[Error: Account does not exist]")
                    Exit Sub
                End If
                SendData.Alert(index, "Your character has been created. You may now login!")
                Server.Writeline("Character " & Name & " has been created.")
            Else
                Server.Writeline("Character " & Name & " Failed To Create! [Error: Name already taken]")
                SendData.Alert(index, "Character " & Name & " Failed To Create!" & vbNewLine & "[Error: Name already taken]")
            End If

        End Sub

        Public Sub Login(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim Name As String, Password As String

            Name = data.ReadString
            Password = data.ReadString

            Player(index) = New Players
            If Not Accounts.Data.AccountExists(Name) Then
                Server.Writeline("Account " & Name & " Failed To Load! [Error: account does not exist]")
                SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: account does not exist]")
                Exit Sub
            Else
                If Not Accounts.Data.VerifyAccount(Name, Password) Then
                    Server.Writeline("Account " & Name & " Failed To Load! [Error: username or password are incorrect]")
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
                Server.Writeline(Name & " has logged in.")
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
                    Server.Writeline("Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetAdmin (Error: invalid access level)")
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
                    Server.Writeline("Hacking Attempt - Player: " & Player(index).Name & " - In HandleSetVisible (Error: invalid access level)")
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
                    Server.Writeline("Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpTo (Error: invalid access level)")
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
                    Server.Writeline("Hacking Attempt - Player: " & Player(index).Name & " - In HandleWarpToMe (Error: invalid access level)")
                End If
            End If

        End Sub

        Public Sub EditorLogin(ByVal index As Integer, ByRef data As NetIncomingMessage)
            Dim Name As String, Password As String, Mode As Integer

            Name = data.ReadString
            Password = data.ReadString
            Mode = data.ReadInt32

            If Not Accounts.Data.AccountExists(Name) Then
                Server.Writeline("Account " & Name & " Failed To Load! [Error: account does not exist]")
                SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: account does not exist]")
                Exit Sub
            Else
                If Not Accounts.Data.VerifyAccount(Name, Password) Then
                    Server.Writeline("Account " & Name & " Failed To Load! [Error: username or password are incorrect]")
                    SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: username or password are incorrect]")
                    Exit Sub
                End If
                If Not Players.Data.HasPlayer(Accounts.Data.GetAccountIndex(Name)) Then
                    Server.Writeline("Account " & Name & " Failed To Load! [Error: character missing]")
                    SendData.Alert(index, "Account " & Name & " Failed To Load!" & vbNewLine & "[Error: character missing]")
                    Exit Sub
                End If
                'If Not Accounts.Data.GetAccount(Name).Player.AccessMode >= ACCESS.DEV Then
                '    Server.Writeline("Account " & Name & " Failed To Access Editor! [Error: invalid account access]")
                '    SendData.Alert(index, "Account " & Name & " Failed To Access Editor!" & vbNewLine & "[Error: invalid account access]")
                '    Exit Sub
                'End If
                SendData.EditorLoginOk(index, Mode)
                Server.Writeline("Account: " & Name & " has logged into the editor!")
            End If
        End Sub

        Public Sub EditorDataRequest(ByVal index As Integer, ByRef data As NetIncomingMessage)
            If data.ReadBoolean = True Then SendData.EditorMapData(index)
            If data.ReadBoolean = True Then SendData.EditorTilesetData(index)
            If data.ReadBoolean = True Then SendData.EditorPlayerData(index)
            If data.ReadBoolean = True Then SendData.EditorNPCData(index)
            If data.ReadBoolean = True Then SendData.EditorItemData(index)
        End Sub

        Public Sub EditorMapData(ByVal Index As Integer, ByRef data As NetIncomingMessage)
            MapCount = data.ReadInt32
            ReDim Map(0 To MapCount)
            For i As Integer = 0 To MapCount - 1
                Map(i) = New Maps
                data.ReadAllProperties(Map(i).Base)
            Next i
            Maps.Data.SaveAll()
            SendData.EditorDataSent(Index, 0, "Map Data Commit Sucesfull!")
        End Sub

        Public Sub EditorPlayerData(ByVal Index As Integer, ByRef data As NetIncomingMessage)
            AccountCount = data.ReadInt32
            ReDim Account(0 To AccountCount)
            For i As Integer = 0 To AccountCount - 1
                Account(i) = New Accounts
                data.ReadAllProperties(Account(i).Base)
            Next i
            Accounts.Data.SaveAll()
            SendData.EditorDataSent(Index, 0, "Player Data Commit Sucesfull!")
        End Sub

        Public Sub EditorTilesetData(ByVal Index As Integer, ByRef data As NetIncomingMessage)
            TilesetCount = data.ReadInt32
            ReDim Tileset(0 To TilesetCount)
            For i As Integer = 0 To TilesetCount - 1
                Tileset(i) = New Tilesets
                data.ReadAllProperties(Tileset(i).Base)
                Tileset(i).Save()
            Next i
            Tilesets.Data.SaveAll()
            SendData.EditorDataSent(Index, 0, "Tileset Data Commit Sucesfull!")
        End Sub

        Public Sub EditorNpcData(ByVal Index As Integer, ByRef data As NetIncomingMessage)
            NPCCount = data.ReadInt32
            ReDim NPC(0 To NPCCount)
            For i As Integer = 0 To NPCCount - 1
                NPC(i) = New NPCs
                data.ReadAllProperties(NPC(i).Base)
            Next i
            NPCs.Data.SaveAll()
            SendData.EditorDataSent(Index, 0, "Npc Data Commit Sucesfull!")
        End Sub

        Public Sub EditorItemData(ByVal Index As Integer, ByRef data As NetIncomingMessage)
            ItemCount = data.ReadInt32
            ReDim Item(0 To ItemCount)
            For i As Integer = 0 To ItemCount - 1
                Item(i) = New Items
                data.ReadAllProperties(Item(i).Base)
            Next i
            Items.Data.SaveAll()
            SendData.EditorDataSent(Index, 0, "Item Data Commit Sucesfull!")
        End Sub
    End Module
End Namespace