Imports Prospekt.Network
Public Class EditorWindow

    Private Sub EditorWindow_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        inEditor = False
        Verdana.Dispose()
        DestroyNetwork()
        Graphics.Render.Dispose()
        End
    End Sub

    Private Sub mnuMain_Publish_Click(sender As Object, e As EventArgs) Handles mnuMain_Publish.Click
        CommitData.Show()
    End Sub

    Private Sub mnuMain_Sync_Click(sender As Object, e As EventArgs) Handles mnuMain_Sync.Click
        SyncData.Show()
    End Sub

    Private Sub EditorWindow_Load(sender As Object, e As EventArgs) Handles Me.Load
        General.Main()
    End Sub

    Private Sub EditorWindow_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Not inEditor Then Exit Sub
        Select Case SelectedEditor
            Case 0 : MapEditor.Reload()
            Case 1 : TilesetEditor.Reload()
        End Select
        Graphics.Render.ReInitialize()
    End Sub

    Private Sub tabEditors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabEditors.SelectedIndexChanged
        SelectedEditor = tabEditors.SelectedIndex
    End Sub

    Private Sub mnuMain_Textures_Click(sender As Object, e As EventArgs) Handles mnuMain_Textures.Click
        Dim TV As TextureViewer = New TextureViewer
        TV.init()
    End Sub

    ' Map Editor
    Private Sub mnuMapSave_Click(sender As Object, e As EventArgs) Handles mnuMapSave.Click
        If Not IsNothing(lstMaps.Items) Then
            Maps.Data.SaveMaps()
            ' Reload Editor Data
            Maps.Data.LoadMaps()
            MapEditor.ReloadList()
        End If
    End Sub

    Private Sub mnuMapNew_Click(sender As Object, e As EventArgs) Handles mnuMapNew.Click
        Maps.Data.NewMap()
        ' Reload Editor Data
        Maps.Data.LoadMaps()
        MapEditor.ReloadList()
    End Sub

    Private Sub mnuMapUndo_Click(sender As Object, e As EventArgs) Handles mnuMapUndo.Click
        If Not IsNothing(lstMaps.Items) Then MapEditor.Undo()
    End Sub

    Private Sub lstMaps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMaps.SelectedIndexChanged
        If IsNothing(lstMaps.SelectedItem) Then Exit Sub
        If Not IsNothing(Map(Maps.Data.GetMapIndex(lstMaps.SelectedIndex))) Then MapEditor.Load(Maps.Data.GetMapIndex(lstMaps.SelectedIndex))
    End Sub

    Private Sub proptMapData_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles proptMapData.PropertyValueChanged
        If Not IsNothing(lstMaps.Items) Then MapEditor.Verify()
    End Sub

    Private Sub proptMapEditorData_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles proptMapEditorData.PropertyValueChanged
        If Not IsNothing(lstMaps.Items) Then MapEditor.VerifyEditor()
    End Sub

    Private Sub mapPreview_MouseEnter(sender As Object, e As EventArgs) Handles mapPreview.MouseEnter
        Cursor.Hide()
        mapScrlY.Focus()
    End Sub

    Private Sub mapPreview_MouseLeave(sender As Object, e As EventArgs) Handles mapPreview.MouseLeave
        Cursor.Show()
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPreview_MouseLeave(e)
    End Sub

    Private Sub mapPreview_MouseDown(sender As Object, e As MouseEventArgs) Handles mapPreview.MouseDown
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPreview_MouseDown(e)
    End Sub

    Private Sub mapPreview_MouseMove(sender As Object, e As MouseEventArgs) Handles mapPreview.MouseMove
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPreview_MouseMove(e)
    End Sub

    Private Sub mapPicTileSet_MouseEnter(sender As Object, e As EventArgs) Handles mapPicTileset.MouseEnter
        Cursor.Hide()
        tileSetScrlY.Focus()
    End Sub

    Private Sub mapPicTileSet_MouseLeave(sender As Object, e As EventArgs) Handles mapPicTileset.MouseLeave
        Cursor.Show()
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPicTileSet_MouseLeave(e)
    End Sub

    Private Sub mapPicTileSet_MouseDown(sender As Object, e As MouseEventArgs) Handles mapPicTileset.MouseDown
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPicTileSet_MouseDown(e)
    End Sub

    Private Sub mapPicTileSet_MouseMove(sender As Object, e As MouseEventArgs) Handles mapPicTileset.MouseMove
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPicTileSet_MouseMove(e)
    End Sub

    Private Sub mapCmbTileSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles mapCmbTileSet.SelectedIndexChanged
        If Not IsNothing(mapCmbTileSet.Items) Then MapEditor.SelectTileset()
    End Sub

    Private Sub lstMapNpcs_MouseEnter(sender As Object, e As EventArgs) Handles lstMapNpcs.MouseEnter
        lstMapNpcs.Focus()
    End Sub

    Private Sub lstMapNpcs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMapNpcs.SelectedIndexChanged
        If Not IsNothing(lstMapNpcs.FocusedItem) Then MapEditor.SelectNpc(lstMapNpcs.FocusedItem.Index)
    End Sub

    Private Sub mapBtnFillLayer_Click(sender As Object, e As EventArgs) Handles mapBtnFillLayer.Click
        If Not IsNothing(mapCmbTileSet.Items) Then
            Dim reply As DialogResult = MessageBox.Show("Are you sure you wish to full this layer?", "Wait!", _
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If reply = DialogResult.Yes Then
                MapEditor.FillLayer()
            End If
        End If
    End Sub

    Private Sub mapBtnClearLayer_Click(sender As Object, e As EventArgs) Handles mapBtnClearLayer.Click
        If Not IsNothing(mapCmbTileSet.Items) Then
            Dim reply As DialogResult = MessageBox.Show("Are you sure you wish to clear this layer?", "Wait!", _
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If reply = DialogResult.Yes Then
                MapEditor.ClearLayer()
            End If
        End If
    End Sub

    Private Sub tabMapObjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMapObjects.SelectedIndexChanged
        If Not IsNothing(lstMaps.Items) Then MapEditor.ModeChange(tabMapObjects.SelectedIndex)
    End Sub

    ' Tileset Editor
    Private Sub cmbTilesetEditor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTilesetEditor.SelectedIndexChanged
        If Not IsNothing(cmbTilesetEditor.Items) Then TilesetEditor.SelectTileset()
    End Sub

    Private Sub picTilesetEditor_MouseEnter(sender As Object, e As EventArgs) Handles picTilesetEditor.MouseEnter
        Cursor.Hide()
        scrlTilesetEditorY.Focus()
    End Sub

    Private Sub picTilesetEditor_MouseLeave(sender As Object, e As EventArgs) Handles picTilesetEditor.MouseLeave
        Cursor.Show()
        TilesetEditor.picTilesetEditor_MouseLeave(e)
    End Sub

    Private Sub picTilesetEditor_MouseDown(sender As Object, e As MouseEventArgs) Handles picTilesetEditor.MouseDown
        TilesetEditor.picTilesetEditor_MouseDown(e)
    End Sub

    Private Sub picTilesetEditor_MouseMove(sender As Object, e As MouseEventArgs) Handles picTilesetEditor.MouseMove
        TilesetEditor.picTilesetEditor_MouseMove(e)
    End Sub

    Private Sub btnSaveTileset_Click(sender As Object, e As EventArgs) Handles btnSaveTileset.Click
        TilesetEditor.btnSaveTileset_Click(e)
    End Sub

    Private Sub btnClearTileset_Click(sender As Object, e As EventArgs) Handles btnClearTileset.Click
        If Not IsNothing(mapCmbTileSet.Items) Then
            Dim reply As DialogResult = MessageBox.Show("Are you sure you wish to clear this tileset?", "Wait!", _
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If reply = DialogResult.Yes Then
                TilesetEditor.ClearTileset()
            End If
        End If
    End Sub

    Private Sub txtTilesetName_TextChanged(sender As Object, e As EventArgs) Handles txtTilesetName.TextChanged
        If Not IsNothing(cmbTilesetEditor.Items) Then TilesetEditor.txtTilesetName_TextChanged()
    End Sub

    ' Account Editor
    Private Sub mnuAccountSave_Click(sender As Object, e As EventArgs) Handles mnuAccountSave.Click
        If Not IsNothing(lstAccounts.Items) Then
            Accounts.Data.SaveAccounts()
            ' Reload Editor Data
            Accounts.Data.LoadAccounts()
            AccountEditor.Reload()
        End If
    End Sub

    Private Sub mnuAccountNew_Click(sender As Object, e As EventArgs) Handles mnuAccountNew.Click
        Accounts.Data.NewAccount()
        ' Reload Editor Data
        Accounts.Data.LoadAccounts()
        AccountEditor.ReloadList()
    End Sub

    Private Sub mnuAccountUndo_Click(sender As Object, e As EventArgs) Handles mnuAccountUndo.Click
        If Not IsNothing(lstAccounts.Items) Then AccountEditor.Undo()
    End Sub

    Private Sub lstAccounts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAccounts.SelectedIndexChanged
        If IsNothing(lstAccounts.SelectedItem) Then Exit Sub
        If Not IsNothing(Account(Accounts.Data.GetAccountIndex(lstAccounts.SelectedItem.ToString))) Then AccountEditor.Load(Accounts.Data.GetAccountIndex(lstAccounts.SelectedItem.ToString))
    End Sub

    Private Sub proptPlayerData_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles proptAccountData.PropertyValueChanged
        If Not IsNothing(lstAccounts.Items) Then AccountEditor.Verify()
    End Sub

    Private Sub btnEditPlayerInventory_Click(sender As Object, e As EventArgs) Handles btnEditPlayerInventory.Click
        If Not IsNothing(lstAccounts.Items) Then AccountEditor.EditInventory()
    End Sub

    ' Npc Editor
    Private Sub mnuNpcSave_Click(sender As Object, e As EventArgs) Handles mnuNpcSave.Click
        If Not IsNothing(lstNpcs.Items) Then
            NPCs.Data.SaveNpcs()
            ' Reload Editor Data
            NPCs.Data.LoadNpcs()
            NpcEditor.Reload()
        End If
    End Sub

    Private Sub mnuNpcNew_Click(sender As Object, e As EventArgs) Handles mnuNpcNew.Click
        NPCs.Data.NewNpc()
        ' Reload Editor Data
        NPCs.Data.LoadNpcs()
        NpcEditor.ReloadList()
    End Sub

    Private Sub mnuNpctUndo_Click(sender As Object, e As EventArgs) Handles mnuNpcUndo.Click
        If Not IsNothing(lstNpcs.Items) Then NpcEditor.Undo()
    End Sub

    Private Sub lstNpcs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNpcs.SelectedIndexChanged
        If IsNothing(lstNpcs.SelectedItem) Then Exit Sub
        If Not IsNothing(NPC(NPCs.Data.GetNpcIndex(lstNpcs.SelectedItem.ToString))) Then NpcEditor.Load(NPCs.Data.GetNpcIndex(lstNpcs.SelectedItem.ToString))
    End Sub

    Private Sub proptNpcrData_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles proptNpcData.PropertyValueChanged
        If Not IsNothing(lstNpcs.Items) Then NpcEditor.Verify()
    End Sub

    Private Sub btnNpcDropTable_Click(sender As Object, e As EventArgs) Handles btnNpcDropTable.Click
        If Not IsNothing(lstNpcs.Items) Then NpcEditor.EditDropTable()
    End Sub

    ' Item Editor
    Private Sub mnuItemSave_Click(sender As Object, e As EventArgs) Handles mnuItemSave.Click
        If Not IsNothing(lstitems.Items) Then
            Items.Data.SaveItems()
            ' Reload Editor Data
            Items.Data.LoadItems()
            ItemEditor.Reload()
        End If
    End Sub

    Private Sub mnuItemNew_Click(sender As Object, e As EventArgs) Handles mnuItemNew.Click
        Items.Data.NewItem()
        ' Reload Editor Data
        Items.Data.LoadItems()
        ItemEditor.ReloadList()
    End Sub

    Private Sub mnuItemUndo_Click(sender As Object, e As EventArgs) Handles mnuItemUndo.Click
        If Not IsNothing(lstitems.Items) Then ItemEditor.Undo()
    End Sub

    Private Sub lstitems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstitems.SelectedIndexChanged
        If IsNothing(lstitems.SelectedItem) Then Exit Sub
        If Not IsNothing(Item(Items.Data.GetItemIndex(lstitems.SelectedItem.ToString))) Then ItemEditor.Load(Items.Data.GetItemIndex(lstitems.SelectedItem.ToString))
    End Sub

    Private Sub proptItemData_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles proptItemData.PropertyValueChanged
        If Not IsNothing(lstitems.Items) Then ItemEditor.Verify()
    End Sub
End Class
