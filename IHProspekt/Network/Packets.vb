Namespace Network
    Public Module Packets
        ' Packets sent by server to client
        Public Enum ServerPackets As Integer
            Alert = 1
            RegisterOk
            LoginOk
            Player
            ClearPlayer
            Position
            Access
            Visible
            Message
            NPC
            NPCPosition
            MapData
            TilesetData
            ' Make sure COUNT is below everything else
            COUNT
        End Enum

        'Packets sent by server to editor
        Public Enum SEditorPackets As Integer
            LoginOk = ServerPackets.COUNT + 1
            MapData
            AccountData
            TilesetData
            NPCData
            ItemData
            DataSent
            ' Make sure COUNT is below everything else
            COUNT
        End Enum

        ' Packets sent by client to server
        Public Enum ClientPackets As Integer
            Register = 1
            NewCharacter
            Login
            Message
            Position
            SetAccess
            SetVisible
            WarpTo
            WarpToMe
            ' Make sure COUNT is below everything else
            COUNT
        End Enum

        'Packets sent by editor to server
        Public Enum CEditorPackets As Integer
            Login = ClientPackets.COUNT + 1
            DataRequest
            MapData
            AccountData
            TilesetData
            NPCData
            ItemData
            ' Make sure COUNT is below everything else
            COUNT
        End Enum
    End Module
End Namespace