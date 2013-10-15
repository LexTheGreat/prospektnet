<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditorWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuMain_File = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMain_Publish = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMain_Sync = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabEditors = New System.Windows.Forms.TabControl()
        Me.tabMapEditor = New System.Windows.Forms.TabPage()
        Me.groupMapData = New System.Windows.Forms.GroupBox()
        Me.tabMaps = New System.Windows.Forms.TabControl()
        Me.tabMap = New System.Windows.Forms.TabPage()
        Me.mapScrlY = New System.Windows.Forms.VScrollBar()
        Me.mapScrlX = New System.Windows.Forms.HScrollBar()
        Me.mapPreview = New System.Windows.Forms.PictureBox()
        Me.panMapRight = New System.Windows.Forms.Panel()
        Me.panMapTiles = New System.Windows.Forms.Panel()
        Me.panMapTileSetBack = New System.Windows.Forms.Panel()
        Me.mapPanTileSet = New System.Windows.Forms.Panel()
        Me.tileSetScrlY = New System.Windows.Forms.VScrollBar()
        Me.tileSetScrlX = New System.Windows.Forms.HScrollBar()
        Me.TileSetPreview = New System.Windows.Forms.PictureBox()
        Me.groupMapTileset = New System.Windows.Forms.GroupBox()
        Me.mapCmbTileSet = New System.Windows.Forms.ComboBox()
        Me.groupMapTileBtns = New System.Windows.Forms.GroupBox()
        Me.mapBtnClearLayer = New System.Windows.Forms.Button()
        Me.mapBtnFillLayer = New System.Windows.Forms.Button()
        Me.panMapLeft = New System.Windows.Forms.Panel()
        Me.tabProperties = New System.Windows.Forms.TabControl()
        Me.tabMapSettings = New System.Windows.Forms.TabPage()
        Me.proptMapData = New System.Windows.Forms.PropertyGrid()
        Me.tabEditor = New System.Windows.Forms.TabPage()
        Me.proptMapEditorData = New System.Windows.Forms.PropertyGrid()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lstMaps = New System.Windows.Forms.ListBox()
        Me.mnuMapList = New System.Windows.Forms.MenuStrip()
        Me.mnuMapSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMapNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMapUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabAccountEditor = New System.Windows.Forms.TabPage()
        Me.groupPlayerData = New System.Windows.Forms.GroupBox()
        Me.tabAccounts = New System.Windows.Forms.TabControl()
        Me.tabAccount = New System.Windows.Forms.TabPage()
        Me.proptAccountData = New System.Windows.Forms.PropertyGrid()
        Me.groupAccountList = New System.Windows.Forms.GroupBox()
        Me.lstAccounts = New System.Windows.Forms.ListBox()
        Me.mnuAccountList = New System.Windows.Forms.MenuStrip()
        Me.mnuAccountSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAccountNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAccountUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.imgSprites = New System.Windows.Forms.ImageList(Me.components)
        Me.mnuMain.SuspendLayout()
        Me.tabEditors.SuspendLayout()
        Me.tabMapEditor.SuspendLayout()
        Me.groupMapData.SuspendLayout()
        Me.tabMaps.SuspendLayout()
        Me.tabMap.SuspendLayout()
        CType(Me.mapPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panMapRight.SuspendLayout()
        Me.panMapTiles.SuspendLayout()
        Me.panMapTileSetBack.SuspendLayout()
        Me.mapPanTileSet.SuspendLayout()
        CType(Me.TileSetPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupMapTileset.SuspendLayout()
        Me.groupMapTileBtns.SuspendLayout()
        Me.panMapLeft.SuspendLayout()
        Me.tabProperties.SuspendLayout()
        Me.tabMapSettings.SuspendLayout()
        Me.tabEditor.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.mnuMapList.SuspendLayout()
        Me.tabAccountEditor.SuspendLayout()
        Me.groupPlayerData.SuspendLayout()
        Me.tabAccounts.SuspendLayout()
        Me.tabAccount.SuspendLayout()
        Me.groupAccountList.SuspendLayout()
        Me.mnuAccountList.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMain_File})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(819, 24)
        Me.mnuMain.TabIndex = 1
        Me.mnuMain.Text = "mnuMain"
        '
        'mnuMain_File
        '
        Me.mnuMain_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMain_Publish, Me.mnuMain_Sync})
        Me.mnuMain_File.Name = "mnuMain_File"
        Me.mnuMain_File.Size = New System.Drawing.Size(37, 20)
        Me.mnuMain_File.Text = "File"
        '
        'mnuMain_Publish
        '
        Me.mnuMain_Publish.Name = "mnuMain_Publish"
        Me.mnuMain_Publish.Size = New System.Drawing.Size(162, 22)
        Me.mnuMain_Publish.Text = "Publish Changes"
        '
        'mnuMain_Sync
        '
        Me.mnuMain_Sync.Name = "mnuMain_Sync"
        Me.mnuMain_Sync.Size = New System.Drawing.Size(162, 22)
        Me.mnuMain_Sync.Text = "Sync Local Data"
        '
        'tabEditors
        '
        Me.tabEditors.Controls.Add(Me.tabMapEditor)
        Me.tabEditors.Controls.Add(Me.tabAccountEditor)
        Me.tabEditors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabEditors.HotTrack = True
        Me.tabEditors.Location = New System.Drawing.Point(0, 24)
        Me.tabEditors.Name = "tabEditors"
        Me.tabEditors.SelectedIndex = 0
        Me.tabEditors.Size = New System.Drawing.Size(819, 452)
        Me.tabEditors.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabEditors.TabIndex = 2
        '
        'tabMapEditor
        '
        Me.tabMapEditor.Controls.Add(Me.groupMapData)
        Me.tabMapEditor.Controls.Add(Me.panMapRight)
        Me.tabMapEditor.Controls.Add(Me.panMapLeft)
        Me.tabMapEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabMapEditor.Name = "tabMapEditor"
        Me.tabMapEditor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMapEditor.Size = New System.Drawing.Size(811, 426)
        Me.tabMapEditor.TabIndex = 0
        Me.tabMapEditor.Text = "Map Editor"
        Me.tabMapEditor.UseVisualStyleBackColor = True
        '
        'groupMapData
        '
        Me.groupMapData.Controls.Add(Me.tabMaps)
        Me.groupMapData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupMapData.Location = New System.Drawing.Point(218, 3)
        Me.groupMapData.Name = "groupMapData"
        Me.groupMapData.Size = New System.Drawing.Size(390, 420)
        Me.groupMapData.TabIndex = 55
        Me.groupMapData.TabStop = False
        Me.groupMapData.Text = "Map"
        '
        'tabMaps
        '
        Me.tabMaps.Controls.Add(Me.tabMap)
        Me.tabMaps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMaps.Location = New System.Drawing.Point(3, 16)
        Me.tabMaps.Name = "tabMaps"
        Me.tabMaps.SelectedIndex = 0
        Me.tabMaps.Size = New System.Drawing.Size(384, 401)
        Me.tabMaps.TabIndex = 0
        '
        'tabMap
        '
        Me.tabMap.Controls.Add(Me.mapScrlY)
        Me.tabMap.Controls.Add(Me.mapScrlX)
        Me.tabMap.Controls.Add(Me.mapPreview)
        Me.tabMap.ForeColor = System.Drawing.Color.Transparent
        Me.tabMap.Location = New System.Drawing.Point(4, 22)
        Me.tabMap.Name = "tabMap"
        Me.tabMap.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMap.Size = New System.Drawing.Size(376, 375)
        Me.tabMap.TabIndex = 0
        Me.tabMap.Text = "Map"
        Me.tabMap.UseVisualStyleBackColor = True
        '
        'mapScrlY
        '
        Me.mapScrlY.Dock = System.Windows.Forms.DockStyle.Right
        Me.mapScrlY.LargeChange = 1
        Me.mapScrlY.Location = New System.Drawing.Point(356, 3)
        Me.mapScrlY.Maximum = 32
        Me.mapScrlY.Name = "mapScrlY"
        Me.mapScrlY.Size = New System.Drawing.Size(17, 352)
        Me.mapScrlY.TabIndex = 5
        '
        'mapScrlX
        '
        Me.mapScrlX.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.mapScrlX.LargeChange = 1
        Me.mapScrlX.Location = New System.Drawing.Point(3, 355)
        Me.mapScrlX.Maximum = 32
        Me.mapScrlX.Name = "mapScrlX"
        Me.mapScrlX.Size = New System.Drawing.Size(370, 17)
        Me.mapScrlX.TabIndex = 4
        '
        'mapPreview
        '
        Me.mapPreview.BackColor = System.Drawing.Color.DarkGray
        Me.mapPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mapPreview.Location = New System.Drawing.Point(3, 3)
        Me.mapPreview.Name = "mapPreview"
        Me.mapPreview.Size = New System.Drawing.Size(370, 369)
        Me.mapPreview.TabIndex = 3
        Me.mapPreview.TabStop = False
        '
        'panMapRight
        '
        Me.panMapRight.Controls.Add(Me.panMapTiles)
        Me.panMapRight.Controls.Add(Me.groupMapTileBtns)
        Me.panMapRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.panMapRight.Location = New System.Drawing.Point(608, 3)
        Me.panMapRight.Name = "panMapRight"
        Me.panMapRight.Size = New System.Drawing.Size(200, 420)
        Me.panMapRight.TabIndex = 54
        '
        'panMapTiles
        '
        Me.panMapTiles.Controls.Add(Me.panMapTileSetBack)
        Me.panMapTiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panMapTiles.Location = New System.Drawing.Point(0, 0)
        Me.panMapTiles.Name = "panMapTiles"
        Me.panMapTiles.Size = New System.Drawing.Size(200, 384)
        Me.panMapTiles.TabIndex = 48
        '
        'panMapTileSetBack
        '
        Me.panMapTileSetBack.Controls.Add(Me.mapPanTileSet)
        Me.panMapTileSetBack.Controls.Add(Me.groupMapTileset)
        Me.panMapTileSetBack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panMapTileSetBack.Location = New System.Drawing.Point(0, 0)
        Me.panMapTileSetBack.Name = "panMapTileSetBack"
        Me.panMapTileSetBack.Size = New System.Drawing.Size(200, 384)
        Me.panMapTileSetBack.TabIndex = 53
        '
        'mapPanTileSet
        '
        Me.mapPanTileSet.AutoScroll = True
        Me.mapPanTileSet.AutoSize = True
        Me.mapPanTileSet.Controls.Add(Me.tileSetScrlY)
        Me.mapPanTileSet.Controls.Add(Me.tileSetScrlX)
        Me.mapPanTileSet.Controls.Add(Me.TileSetPreview)
        Me.mapPanTileSet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mapPanTileSet.Location = New System.Drawing.Point(0, 40)
        Me.mapPanTileSet.Name = "mapPanTileSet"
        Me.mapPanTileSet.Size = New System.Drawing.Size(200, 344)
        Me.mapPanTileSet.TabIndex = 54
        '
        'tileSetScrlY
        '
        Me.tileSetScrlY.Dock = System.Windows.Forms.DockStyle.Right
        Me.tileSetScrlY.LargeChange = 1
        Me.tileSetScrlY.Location = New System.Drawing.Point(183, 0)
        Me.tileSetScrlY.Maximum = 1
        Me.tileSetScrlY.Name = "tileSetScrlY"
        Me.tileSetScrlY.Size = New System.Drawing.Size(17, 327)
        Me.tileSetScrlY.TabIndex = 8
        '
        'tileSetScrlX
        '
        Me.tileSetScrlX.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tileSetScrlX.LargeChange = 1
        Me.tileSetScrlX.Location = New System.Drawing.Point(0, 327)
        Me.tileSetScrlX.Maximum = 1
        Me.tileSetScrlX.Name = "tileSetScrlX"
        Me.tileSetScrlX.Size = New System.Drawing.Size(200, 17)
        Me.tileSetScrlX.TabIndex = 7
        '
        'TileSetPreview
        '
        Me.TileSetPreview.BackColor = System.Drawing.Color.DarkGray
        Me.TileSetPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TileSetPreview.Location = New System.Drawing.Point(0, 0)
        Me.TileSetPreview.Name = "TileSetPreview"
        Me.TileSetPreview.Size = New System.Drawing.Size(200, 344)
        Me.TileSetPreview.TabIndex = 6
        Me.TileSetPreview.TabStop = False
        '
        'groupMapTileset
        '
        Me.groupMapTileset.Controls.Add(Me.mapCmbTileSet)
        Me.groupMapTileset.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupMapTileset.Location = New System.Drawing.Point(0, 0)
        Me.groupMapTileset.Name = "groupMapTileset"
        Me.groupMapTileset.Size = New System.Drawing.Size(200, 40)
        Me.groupMapTileset.TabIndex = 55
        Me.groupMapTileset.TabStop = False
        Me.groupMapTileset.Text = "TileSet"
        '
        'mapCmbTileSet
        '
        Me.mapCmbTileSet.Dock = System.Windows.Forms.DockStyle.Top
        Me.mapCmbTileSet.FormattingEnabled = True
        Me.mapCmbTileSet.Location = New System.Drawing.Point(3, 16)
        Me.mapCmbTileSet.Name = "mapCmbTileSet"
        Me.mapCmbTileSet.Size = New System.Drawing.Size(194, 21)
        Me.mapCmbTileSet.TabIndex = 0
        '
        'groupMapTileBtns
        '
        Me.groupMapTileBtns.Controls.Add(Me.mapBtnClearLayer)
        Me.groupMapTileBtns.Controls.Add(Me.mapBtnFillLayer)
        Me.groupMapTileBtns.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.groupMapTileBtns.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.groupMapTileBtns.Location = New System.Drawing.Point(0, 384)
        Me.groupMapTileBtns.Name = "groupMapTileBtns"
        Me.groupMapTileBtns.Size = New System.Drawing.Size(200, 36)
        Me.groupMapTileBtns.TabIndex = 49
        Me.groupMapTileBtns.TabStop = False
        '
        'mapBtnClearLayer
        '
        Me.mapBtnClearLayer.Location = New System.Drawing.Point(135, 9)
        Me.mapBtnClearLayer.Name = "mapBtnClearLayer"
        Me.mapBtnClearLayer.Size = New System.Drawing.Size(59, 23)
        Me.mapBtnClearLayer.TabIndex = 12
        Me.mapBtnClearLayer.Tag = "Clear Layer"
        Me.mapBtnClearLayer.Text = "Clear"
        Me.mapBtnClearLayer.UseVisualStyleBackColor = True
        '
        'mapBtnFillLayer
        '
        Me.mapBtnFillLayer.Location = New System.Drawing.Point(6, 11)
        Me.mapBtnFillLayer.Name = "mapBtnFillLayer"
        Me.mapBtnFillLayer.Size = New System.Drawing.Size(59, 23)
        Me.mapBtnFillLayer.TabIndex = 11
        Me.mapBtnFillLayer.Tag = "Fill Layer"
        Me.mapBtnFillLayer.Text = "Fill"
        Me.mapBtnFillLayer.UseVisualStyleBackColor = True
        '
        'panMapLeft
        '
        Me.panMapLeft.Controls.Add(Me.tabProperties)
        Me.panMapLeft.Controls.Add(Me.GroupBox3)
        Me.panMapLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.panMapLeft.Location = New System.Drawing.Point(3, 3)
        Me.panMapLeft.Name = "panMapLeft"
        Me.panMapLeft.Size = New System.Drawing.Size(215, 420)
        Me.panMapLeft.TabIndex = 51
        '
        'tabProperties
        '
        Me.tabProperties.Controls.Add(Me.tabMapSettings)
        Me.tabProperties.Controls.Add(Me.tabEditor)
        Me.tabProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabProperties.Location = New System.Drawing.Point(0, 150)
        Me.tabProperties.Name = "tabProperties"
        Me.tabProperties.SelectedIndex = 0
        Me.tabProperties.Size = New System.Drawing.Size(215, 270)
        Me.tabProperties.TabIndex = 52
        '
        'tabMapSettings
        '
        Me.tabMapSettings.Controls.Add(Me.proptMapData)
        Me.tabMapSettings.Location = New System.Drawing.Point(4, 22)
        Me.tabMapSettings.Name = "tabMapSettings"
        Me.tabMapSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMapSettings.Size = New System.Drawing.Size(207, 244)
        Me.tabMapSettings.TabIndex = 0
        Me.tabMapSettings.Text = "Map Settings"
        Me.tabMapSettings.UseVisualStyleBackColor = True
        '
        'proptMapData
        '
        Me.proptMapData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.proptMapData.Location = New System.Drawing.Point(3, 3)
        Me.proptMapData.Name = "proptMapData"
        Me.proptMapData.PropertySort = System.Windows.Forms.PropertySort.Categorized
        Me.proptMapData.Size = New System.Drawing.Size(201, 238)
        Me.proptMapData.TabIndex = 49
        '
        'tabEditor
        '
        Me.tabEditor.Controls.Add(Me.proptMapEditorData)
        Me.tabEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabEditor.Name = "tabEditor"
        Me.tabEditor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEditor.Size = New System.Drawing.Size(207, 244)
        Me.tabEditor.TabIndex = 1
        Me.tabEditor.Text = "Editor Settings"
        Me.tabEditor.UseVisualStyleBackColor = True
        '
        'proptMapEditorData
        '
        Me.proptMapEditorData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.proptMapEditorData.Location = New System.Drawing.Point(3, 3)
        Me.proptMapEditorData.Name = "proptMapEditorData"
        Me.proptMapEditorData.Size = New System.Drawing.Size(201, 238)
        Me.proptMapEditorData.TabIndex = 49
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lstMaps)
        Me.GroupBox3.Controls.Add(Me.mnuMapList)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(215, 150)
        Me.GroupBox3.TabIndex = 51
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Map List"
        '
        'lstMaps
        '
        Me.lstMaps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMaps.FormattingEnabled = True
        Me.lstMaps.Location = New System.Drawing.Point(3, 40)
        Me.lstMaps.Name = "lstMaps"
        Me.lstMaps.Size = New System.Drawing.Size(209, 107)
        Me.lstMaps.TabIndex = 41
        '
        'mnuMapList
        '
        Me.mnuMapList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMapSave, Me.mnuMapNew, Me.mnuMapUndo})
        Me.mnuMapList.Location = New System.Drawing.Point(3, 16)
        Me.mnuMapList.Name = "mnuMapList"
        Me.mnuMapList.Size = New System.Drawing.Size(209, 24)
        Me.mnuMapList.TabIndex = 42
        '
        'mnuMapSave
        '
        Me.mnuMapSave.Name = "mnuMapSave"
        Me.mnuMapSave.Size = New System.Drawing.Size(43, 20)
        Me.mnuMapSave.Text = "Save"
        '
        'mnuMapNew
        '
        Me.mnuMapNew.Name = "mnuMapNew"
        Me.mnuMapNew.Size = New System.Drawing.Size(43, 20)
        Me.mnuMapNew.Text = "New"
        '
        'mnuMapUndo
        '
        Me.mnuMapUndo.Name = "mnuMapUndo"
        Me.mnuMapUndo.Size = New System.Drawing.Size(48, 20)
        Me.mnuMapUndo.Text = "Undo"
        '
        'tabAccountEditor
        '
        Me.tabAccountEditor.Controls.Add(Me.groupPlayerData)
        Me.tabAccountEditor.Controls.Add(Me.groupAccountList)
        Me.tabAccountEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabAccountEditor.Name = "tabAccountEditor"
        Me.tabAccountEditor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAccountEditor.Size = New System.Drawing.Size(811, 426)
        Me.tabAccountEditor.TabIndex = 3
        Me.tabAccountEditor.Text = "Account Editor"
        Me.tabAccountEditor.UseVisualStyleBackColor = True
        '
        'groupPlayerData
        '
        Me.groupPlayerData.Controls.Add(Me.tabAccounts)
        Me.groupPlayerData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupPlayerData.Location = New System.Drawing.Point(158, 3)
        Me.groupPlayerData.Name = "groupPlayerData"
        Me.groupPlayerData.Size = New System.Drawing.Size(650, 420)
        Me.groupPlayerData.TabIndex = 2
        Me.groupPlayerData.TabStop = False
        Me.groupPlayerData.Text = "Account Data"
        '
        'tabAccounts
        '
        Me.tabAccounts.Controls.Add(Me.tabAccount)
        Me.tabAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabAccounts.Location = New System.Drawing.Point(3, 16)
        Me.tabAccounts.Name = "tabAccounts"
        Me.tabAccounts.SelectedIndex = 0
        Me.tabAccounts.Size = New System.Drawing.Size(644, 401)
        Me.tabAccounts.TabIndex = 0
        '
        'tabAccount
        '
        Me.tabAccount.Controls.Add(Me.proptAccountData)
        Me.tabAccount.Location = New System.Drawing.Point(4, 22)
        Me.tabAccount.Name = "tabAccount"
        Me.tabAccount.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAccount.Size = New System.Drawing.Size(636, 375)
        Me.tabAccount.TabIndex = 2
        Me.tabAccount.Text = "Account"
        Me.tabAccount.UseVisualStyleBackColor = True
        '
        'proptAccountData
        '
        Me.proptAccountData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.proptAccountData.Location = New System.Drawing.Point(3, 3)
        Me.proptAccountData.Name = "proptAccountData"
        Me.proptAccountData.Size = New System.Drawing.Size(630, 369)
        Me.proptAccountData.TabIndex = 1
        '
        'groupAccountList
        '
        Me.groupAccountList.Controls.Add(Me.lstAccounts)
        Me.groupAccountList.Controls.Add(Me.mnuAccountList)
        Me.groupAccountList.Dock = System.Windows.Forms.DockStyle.Left
        Me.groupAccountList.Location = New System.Drawing.Point(3, 3)
        Me.groupAccountList.Name = "groupAccountList"
        Me.groupAccountList.Size = New System.Drawing.Size(155, 420)
        Me.groupAccountList.TabIndex = 0
        Me.groupAccountList.TabStop = False
        Me.groupAccountList.Text = "Account List"
        '
        'lstAccounts
        '
        Me.lstAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstAccounts.FormattingEnabled = True
        Me.lstAccounts.Location = New System.Drawing.Point(3, 40)
        Me.lstAccounts.Name = "lstAccounts"
        Me.lstAccounts.Size = New System.Drawing.Size(149, 377)
        Me.lstAccounts.TabIndex = 0
        '
        'mnuAccountList
        '
        Me.mnuAccountList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAccountSave, Me.mnuAccountNew, Me.mnuAccountUndo})
        Me.mnuAccountList.Location = New System.Drawing.Point(3, 16)
        Me.mnuAccountList.Name = "mnuAccountList"
        Me.mnuAccountList.Size = New System.Drawing.Size(149, 24)
        Me.mnuAccountList.TabIndex = 1
        '
        'mnuAccountSave
        '
        Me.mnuAccountSave.Name = "mnuAccountSave"
        Me.mnuAccountSave.Size = New System.Drawing.Size(43, 20)
        Me.mnuAccountSave.Text = "Save"
        '
        'mnuAccountNew
        '
        Me.mnuAccountNew.Name = "mnuAccountNew"
        Me.mnuAccountNew.Size = New System.Drawing.Size(43, 20)
        Me.mnuAccountNew.Text = "New"
        '
        'mnuAccountUndo
        '
        Me.mnuAccountUndo.Name = "mnuAccountUndo"
        Me.mnuAccountUndo.Size = New System.Drawing.Size(48, 20)
        Me.mnuAccountUndo.Text = "Undo"
        '
        'imgSprites
        '
        Me.imgSprites.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imgSprites.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgSprites.TransparentColor = System.Drawing.Color.Transparent
        '
        'EditorWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 476)
        Me.Controls.Add(Me.tabEditors)
        Me.Controls.Add(Me.mnuMain)
        Me.MainMenuStrip = Me.mnuMain
        Me.Name = "EditorWindow"
        Me.Text = "Prospekt.NET Editor"
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.tabEditors.ResumeLayout(False)
        Me.tabMapEditor.ResumeLayout(False)
        Me.groupMapData.ResumeLayout(False)
        Me.tabMaps.ResumeLayout(False)
        Me.tabMap.ResumeLayout(False)
        CType(Me.mapPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panMapRight.ResumeLayout(False)
        Me.panMapTiles.ResumeLayout(False)
        Me.panMapTileSetBack.ResumeLayout(False)
        Me.panMapTileSetBack.PerformLayout()
        Me.mapPanTileSet.ResumeLayout(False)
        CType(Me.TileSetPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupMapTileset.ResumeLayout(False)
        Me.groupMapTileBtns.ResumeLayout(False)
        Me.panMapLeft.ResumeLayout(False)
        Me.tabProperties.ResumeLayout(False)
        Me.tabMapSettings.ResumeLayout(False)
        Me.tabEditor.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.mnuMapList.ResumeLayout(False)
        Me.mnuMapList.PerformLayout()
        Me.tabAccountEditor.ResumeLayout(False)
        Me.groupPlayerData.ResumeLayout(False)
        Me.tabAccounts.ResumeLayout(False)
        Me.tabAccount.ResumeLayout(False)
        Me.groupAccountList.ResumeLayout(False)
        Me.groupAccountList.PerformLayout()
        Me.mnuAccountList.ResumeLayout(False)
        Me.mnuAccountList.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuMain_File As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tabEditors As System.Windows.Forms.TabControl
    Friend WithEvents tabMapEditor As System.Windows.Forms.TabPage
    Friend WithEvents tabAccountEditor As System.Windows.Forms.TabPage
    Friend WithEvents groupAccountList As System.Windows.Forms.GroupBox
    Friend WithEvents imgSprites As System.Windows.Forms.ImageList
    Friend WithEvents lstAccounts As System.Windows.Forms.ListBox
    Friend WithEvents groupPlayerData As System.Windows.Forms.GroupBox
    Friend WithEvents tabAccounts As System.Windows.Forms.TabControl
    Friend WithEvents tabAccount As System.Windows.Forms.TabPage
    Friend WithEvents proptAccountData As System.Windows.Forms.PropertyGrid
    Friend WithEvents mnuAccountList As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuAccountSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccountUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents panMapLeft As System.Windows.Forms.Panel
    Private WithEvents tabProperties As System.Windows.Forms.TabControl
    Private WithEvents tabMapSettings As System.Windows.Forms.TabPage
    Private WithEvents tabEditor As System.Windows.Forms.TabPage
    Private WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents lstMaps As System.Windows.Forms.ListBox
    Friend WithEvents panMapRight As System.Windows.Forms.Panel
    Friend WithEvents panMapTiles As System.Windows.Forms.Panel
    Friend WithEvents mnuMain_Publish As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccountNew As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents proptMapData As System.Windows.Forms.PropertyGrid
    Public WithEvents proptMapEditorData As System.Windows.Forms.PropertyGrid
    Friend WithEvents groupMapData As System.Windows.Forms.GroupBox
    Friend WithEvents tabMaps As System.Windows.Forms.TabControl
    Friend WithEvents tabMap As System.Windows.Forms.TabPage
    Friend WithEvents mapPreview As System.Windows.Forms.PictureBox
    Friend WithEvents mapScrlY As System.Windows.Forms.VScrollBar
    Friend WithEvents mapScrlX As System.Windows.Forms.HScrollBar
    Friend WithEvents mnuMapList As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuMapSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMapNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMapUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMain_Sync As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents panMapTileSetBack As System.Windows.Forms.Panel
    Friend WithEvents mapPanTileSet As System.Windows.Forms.Panel
    Friend WithEvents groupMapTileset As System.Windows.Forms.GroupBox
    Public WithEvents mapCmbTileSet As System.Windows.Forms.ComboBox
    Private WithEvents groupMapTileBtns As System.Windows.Forms.GroupBox
    Private WithEvents mapBtnClearLayer As System.Windows.Forms.Button
    Private WithEvents mapBtnFillLayer As System.Windows.Forms.Button
    Friend WithEvents tileSetScrlY As System.Windows.Forms.VScrollBar
    Friend WithEvents tileSetScrlX As System.Windows.Forms.HScrollBar
    Friend WithEvents TileSetPreview As System.Windows.Forms.PictureBox

End Class
