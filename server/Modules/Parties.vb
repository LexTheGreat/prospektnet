Module PartyStructure
    ' Parties
    Public Party() As Parties
    Class Parties
        Private mId As Integer
        Private mMembers As PartyMembers()

        Public Class PartyMembers
            Private mIndex As Integer
            Private isLeader As Boolean = False

            Public Property Index() As Integer
                Get
                    Return mIndex
                End Get
                Set(ByVal value As Integer)
                    mIndex = value
                End Set
            End Property

            Public Property Leader() As Boolean
                Get
                    Return isLeader
                End Get
                Set(ByVal value As Boolean)
                    isLeader = value
                End Set
            End Property
        End Class

        ReadOnly Property ID() As Integer
            Get
                Return mId
            End Get
        End Property

        Public Property Members() As PartyMembers()
            Get
                Return mMembers
            End Get
            Set(ByVal value As PartyMembers())
                mMembers = value
            End Set
        End Property

        Public Function Create(ByVal Index As Integer) As Boolean
            ' Make sure player isn't already in a party
            If Not (Party(Player(Index).GetParty).ID = vbNullString) Then Return False
            Dim nextID As Integer = Party.Length

            Me.mId = nextID
            Me.AddMember(Index, True)
            ' party created
            Return True
        End Function

        Public Function AddMember(ByVal Index As Integer, Optional ByVal isLeader As Boolean = False) As Boolean
            Dim i As Integer = 0
            ' Check if member exists
            For Each member As PartyMembers In Me.Members()
                ' Player already is a party
                If (member.Index = Index) Then Return False
                ' Get next avaiable slot in members
                If Not (member.Index > 0) Then i = i + 1
            Next
            ReDim Preserve Me.Members(0 To i)
            ' Add new member
            Me.Members(i).Index = Index
            ' If will be party leader, set it
            If isLeader Then Me.Members(i).Leader = True

            ' Member added
            Return True
        End Function

        Public Function RemoveMember(ByVal Index As Integer) As Boolean
            Dim NewList As PartyMembers()
            Dim j As Integer = 0

            ReDim NewList(0 To UBound(Me.Members))
            ' Create a new member list
            For i = LBound(Me.Members) To UBound(Me.Members)
                ' Add all members besides the one being removed to new list
                If Me.Members(i).Index <> Index Then
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

        Public Function GetLeader() As Integer
            For Each member As PartyMembers In Me.Members()
                If (member.Leader) Then Return member.Index
            Next
            Return -1
        End Function

        Public Function GetMemberIndexes() As Integer()
            ReDim Preserve GetMemberIndexes(0 To 25)
            Dim i As Integer = 0
            For Each member As PartyMembers In Me.Members()
                If Not (member.Index > 0) Then
                    GetMemberIndexes(i) = member.Index
                    i = i + 1
                End If
            Next
            ReDim Preserve GetMemberIndexes(0 To i - 1)

            Return GetMemberIndexes
        End Function

        Public Function GetMemberNames() As String()
            ReDim Preserve GetMemberNames(0 To 25)
            Dim i As Integer = 0
            For Each member As PartyMembers In Me.Members()
                If Not (member.Index > 0) Then
                    GetMemberIndexes(i) = Player(member.Index).Name
                    i = i + 1
                End If
            Next
            ReDim Preserve GetMemberNames(0 To i - 1)

            Return GetMemberNames()
        End Function

    End Class

End Module
