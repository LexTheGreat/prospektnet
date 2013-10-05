
Module GuildStructure
    ' Guilds
    Public Guild() As Guilds

    Public Class Guilds
        Private mId As Integer
        Private mName As String
        Private mTag As String
        Private mTagColor(3) As Byte
        Private mLevel As Integer
        Private mExp As Integer
        Private mLeader As String
        Private mMembers As GuildMembers()

        Public Class GuildMembers
            Private mName As String
            Private mRank As Integer

            Public Enum Ranks
                Recruit = 1
                Member
                SeniorMember
                Council
                Leader
                ' Make sure COUNT is below everything else
                COUNT
            End Enum

            Public Property Name() As String
                Get
                    Return mName
                End Get
                Set(ByVal value As String)
                    mName = value
                End Set
            End Property

            Public Property Rank() As Integer
                Get
                    Return mRank
                End Get
                Set(ByVal value As Integer)
                    mRank = value
                End Set
            End Property
        End Class

        ReadOnly Property ID() As Integer
            Get
                Return mId
            End Get
        End Property

        Public Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property

        Public Property Tag() As String
            Get
                Return mTag
            End Get
            Set(ByVal value As String)
                mTag = value
            End Set
        End Property

        Public Property Color() As Byte()
            Get
                Return mTagColor
            End Get
            Set(ByVal value As Byte())
                mTagColor = value
            End Set
        End Property

        Public Property Level() As Integer
            Get
                Return mLevel
            End Get
            Set(ByVal value As Integer)
                mLevel = value
            End Set
        End Property

        Public Property Exp() As Integer
            Get
                Return mExp
            End Get
            Set(ByVal value As Integer)
                mExp = value
            End Set
        End Property

        Public Property Members() As GuildMembers()
            Get
                Return mMembers
            End Get
            Set(ByVal value As GuildMembers())
                mMembers = value
            End Set
        End Property


        Public Function Create(ByVal Index As Integer, ByVal ID As Integer, ByVal Name As String, ByVal Tag As String, ByVal Color As Byte()) As Boolean
            ' Make sure player isn't already in a guild
            If Not (Guild(Player(Index).GuildID).Name = vbNullString) Then Return False

            Me.mId = ID
            Me.mName = Name
            Me.mTag = Tag
            Me.mTagColor = Color
            Me.AddMember(Index, GuildMembers.Ranks.Leader)

            ' guild created
            Return True
        End Function

        Public Function AddMember(ByVal Index As Integer, ByVal Rank As GuildMembers.Ranks) As Boolean
            Dim i As Integer = 0
            ' Check if member exists
            For Each member As GuildMembers In Me.Members()
                ' Player already is a member
                If (member.Name = Player(Index).Name) Then Return False
                ' Get next avaiable slot in members
                If Not (member.Name = vbNullString) Then i = i + 1
            Next
            ReDim Preserve Me.Members(0 To i)
            ' Add new member
            Me.Members(i).Name = Player(Index).Name
            Me.Members(i).Rank = Rank

            ' Member added
            Return True
        End Function

        Public Function RemoveMember(ByVal Index As Integer, ByVal guild As Guilds) As Boolean
            Dim NewList As GuildMembers()
            Dim j As Integer = 0

            ReDim NewList(0 To UBound(Me.Members))
            ' Create a new member list
            For i = LBound(Me.Members) To UBound(Me.Members)
                ' Add all members besides the one being removed to new list
                If Me.Members(i).Name <> Player(Index).Name Then
                    j = j + 1
                    NewList(j) = Me.Members(i)
                End If
            Next i
            ReDim Preserve NewList(0 To j)
            'Assign new lst to members
            Me.Members = NewList

            ' Member Removed
            Return True
        End Function

        Public Function EditMemberRank(ByVal Index As Integer, ByVal Rank As GuildMembers.Ranks, ByVal guild As Guilds) As Boolean
            Dim i As Integer = 0
            ' Check if member exists
            For Each member As GuildMembers In Me.Members()
                ' Found member
                If (member.Name = Player(Index).Name) Then
                    member.Rank = Rank
                    Return True
                End If
            Next

            ' Member not found
            Return False
        End Function

        Public Function EditName(ByVal Name As String) As Boolean
            ' Check if name exists
            For Each g As Guilds In Guild
                If (g.Name = Name) Then Return False
            Next
            Me.Name = Name

            ' Name changed
            Return True
        End Function

        Public Function EditTag(ByVal Tag As String, ByVal Color As Byte()) As Boolean
            ' Make sure color is set correctly
            If Not Color.Length = 3 Then Return False

            Me.Tag = Name
            Me.mTagColor = Color

            ' Tag changed
            Return True
        End Function
    End Class

    Public Function Save(ByVal ID As Integer) As Boolean

        Return True
    End Function

    Public Function Load(ByVal ID As Integer) As Boolean

        Return True
    End Function

End Module
