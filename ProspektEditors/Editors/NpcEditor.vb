Class NpcClass
    Private index As Integer

    Public Sub Init()
        For I As Integer = 0 To NPCCount
            If Not IsNothing(NPC(I)) Then
                EditorWindow.lstNpcs.Items.Add(NPC(I).Name)
            End If
        Next

        EditorWindow.tabNpc.Text = "Npc"
        EditorWindow.proptNpcData.SelectedObject = vbNull
    End Sub

    Public Sub Load(ByVal i As Integer)
        If i < 0 Then Exit Sub
        index = i
        EditorWindow.tabNpc.Text = NPC(i).Name
        EditorWindow.proptNpcData.SelectedObject = NPC(i)
    End Sub

    Public Sub ReloadList()
        EditorWindow.lstNpcs.Items.Clear()
        If Not IsNothing(NPC) Then
            For Each nc In NPC
                If Not IsNothing(nc) Then
                    EditorWindow.lstNpcs.Items.Add(nc.Name)
                End If
            Next
        End If
        EditorWindow.tabNpc.Text = "Npc"
        EditorWindow.proptNpcData.SelectedObject = vbNull
        Load(index)
    End Sub

    Public Sub Reload()
        ReloadList()
        Load(index)
    End Sub

    Public Sub Undo()
        NPCs.Data.LoadAll()
        EditorWindow.tabNpc.Text = "Npc"
        EditorWindow.proptNpcData.SelectedObject = vbNull
    End Sub

    Public Sub Verify()
        If NPC(index).Name = vbNullString Then NPC(index).Name = "New Npc"
        If NPC(index).Sprite < 0 Then NPC(index).Sprite = 0
        If NPC(index).Level < 0 Then NPC(index).Level = 0
        If NPC(index).Level > NpcMaxLevel Then NPC(index).Level = NpcMaxLevel
        If NPC(index).Health < 0 Then NPC(index).Health = 0

        ' Refresh data
        EditorWindow.proptNpcData.Refresh()
    End Sub

    Public Sub EditDropTable()
        If index < 0 Then Exit Sub
        NpcDropEditor.Init(index)
        NpcDropTable.Show()
    End Sub
End Class
