Public Class EditorWindow

    Private Sub EditorWindow_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        inEditor = False
        Verdana.Dispose()
        Networking.Dispose()
        Render.Dispose()
        End
    End Sub

    Private Sub mnuMain_Publish_Click(sender As Object, e As EventArgs) Handles mnuMain_Publish.Click
        CommitData.Show()
    End Sub

    Private Sub mnuMain_Sync_Click(sender As Object, e As EventArgs) Handles mnuMain_Sync.Click
        SyncData.Show()
    End Sub

    ' Map Editor
    Private Sub mnuMapSave_Click(sender As Object, e As EventArgs) Handles mnuMapSave.Click
        If Not IsNothing(lstMaps.Items) Then
            MapData.SaveMaps()
            ' Reload Editor Data
            MapData.LoadMaps()
            MapEditor.ReloadList()
        End If
    End Sub

    Private Sub mnuMapNew_Click(sender As Object, e As EventArgs) Handles mnuMapNew.Click
        MapData.NewMap()
        ' Reload Editor Data
        MapData.LoadMaps()
        MapEditor.ReloadList()
    End Sub

    Private Sub mnuMapUndo_Click(sender As Object, e As EventArgs) Handles mnuMapUndo.Click
        If Not IsNothing(lstMaps.Items) Then MapEditor.Undo()
    End Sub

    Private Sub lstMaps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMaps.SelectedIndexChanged
        If IsNothing(lstMaps.SelectedItem) Then Exit Sub
        If Not IsNothing(Map(MapData.GetMapIndex(lstMaps.SelectedIndex))) Then MapEditor.Load(MapData.GetMapIndex(lstMaps.SelectedIndex))
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

    Private Sub mapPicTileSet_MouseEnter(sender As Object, e As EventArgs) Handles TileSetPreview.MouseEnter
        Cursor.Hide()
        tileSetScrlY.Focus()
    End Sub

    Private Sub mapPicTileSet_MouseLeave(sender As Object, e As EventArgs) Handles TileSetPreview.MouseLeave
        Cursor.Show()
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPicTileSet_MouseLeave(e)
    End Sub

    Private Sub mapPicTileSet_MouseDown(sender As Object, e As MouseEventArgs) Handles TileSetPreview.MouseDown
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPicTileSet_MouseDown(e)
    End Sub

    Private Sub mapPicTileSet_MouseMove(sender As Object, e As MouseEventArgs) Handles TileSetPreview.MouseMove
        If Not IsNothing(lstMaps.Items) Then MapEditor.mapPicTileSet_MouseMove(e)
    End Sub

    Private Sub mapCmbTileSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles mapCmbTileSet.SelectedIndexChanged
        If Not IsNothing(mapCmbTileSet.Items) Then MapEditor.SelectTileset()
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

    ' Account Editor
    Private Sub mnuAccountSave_Click(sender As Object, e As EventArgs) Handles mnuAccountSave.Click
        If Not IsNothing(lstAccounts.Items) Then
            AccountData.SaveAccounts()
            ' Reload Editor Data
            AccountData.LoadAccounts()
            AccountEditor.Reload()
        End If
    End Sub

    Private Sub mnuAccountNew_Click(sender As Object, e As EventArgs) Handles mnuAccountNew.Click
        AccountData.NewAccount()
        ' Reload Editor Data
        AccountData.LoadAccounts()
        AccountEditor.ReloadList()
    End Sub

    Private Sub mnuAccountUndo_Click(sender As Object, e As EventArgs) Handles mnuAccountUndo.Click
        If Not IsNothing(lstAccounts.Items) Then AccountEditor.Undo()
    End Sub

    Private Sub lstAccounts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAccounts.SelectedIndexChanged
        If IsNothing(lstAccounts.SelectedItem) Then Exit Sub
        If Not IsNothing(Account(AccountData.GetAccountIndex(lstAccounts.SelectedItem.ToString))) Then AccountEditor.Load(AccountData.GetAccountIndex(lstAccounts.SelectedItem.ToString))
    End Sub

    Private Sub proptPlayerData_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles proptAccountData.PropertyValueChanged
        If Not IsNothing(lstAccounts.Items) Then AccountEditor.Verify()
    End Sub

    Private Sub EditorWindow_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Not inEditor Then Exit Sub
        Render.ReInitialize()
        If IsNothing(lstMaps.SelectedItem) Then Exit Sub
        If Not IsNothing(Map(MapData.GetMapIndex(lstMaps.SelectedIndex))) Then MapEditor.Load(MapData.GetMapIndex(lstMaps.SelectedIndex))
    End Sub

    Private Sub EditorWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        General.Main()
    End Sub

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
End Class
