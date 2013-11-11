Namespace Core
    Public Module Enumerations
        ' Admin Ranks
        Public Enum ACCESS As Byte
            NONE = 0
            GMIT
            GM
            LEAD_GM
            DEV
            ADMIN
            ' Make sure COUNT is below everything else
            COUNT
        End Enum

        ' Directions
        Public Enum DirEnum As Byte
            Up = 0
            Down
            Left
            Right
            ' Make sure COUNT is below everything else
            COUNT
        End Enum

        ' Chat Modes
        Public Enum ChatModes As Byte
            Say = 0
            Whisper
            Shout
            Party
            Guild
            GM
            ' Make sure COUNT is below everything else
            COUNT
        End Enum

        ' Layers
        Public Enum MapLayerEnum As Byte
            Ground = 0
            GroundMask
            Fringe
            FringeMask
            ' Make sure COUNT is below everything else
            COUNT
        End Enum

        ' Tile type
        Public Enum TileType As Byte
            Walkable = 0
            Blocked
            NPCAvoid
            ' Make sure COUNT is below everything else
            COUNT
        End Enum
    End Module
End Namespace
