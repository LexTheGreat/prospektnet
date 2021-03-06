﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.mnuMain_View = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMain_Textures = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabEditors = New System.Windows.Forms.TabControl()
        Me.tabMapEditor = New System.Windows.Forms.TabPage()
        Me.groupMapData = New System.Windows.Forms.GroupBox()
        Me.tabMaps = New System.Windows.Forms.TabControl()
        Me.tabMap = New System.Windows.Forms.TabPage()
        Me.mapPreview = New System.Windows.Forms.PictureBox()
        Me.mapScrlY = New System.Windows.Forms.VScrollBar()
        Me.mapScrlX = New System.Windows.Forms.HScrollBar()
        Me.panMapRight = New System.Windows.Forms.Panel()
        Me.tabMapObjects = New System.Windows.Forms.TabControl()
        Me.mapObjectsTiles = New System.Windows.Forms.TabPage()
        Me.mapPicTileset = New System.Windows.Forms.PictureBox()
        Me.tileSetScrlY = New System.Windows.Forms.VScrollBar()
        Me.tileSetScrlX = New System.Windows.Forms.HScrollBar()
        Me.groupMapTileset = New System.Windows.Forms.GroupBox()
        Me.mapCmbTileSet = New System.Windows.Forms.ComboBox()
        Me.mapObjectsNpcs = New System.Windows.Forms.TabPage()
        Me.lstMapNpcs = New System.Windows.Forms.ListView()
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
        Me.tabTilesetEditor = New System.Windows.Forms.TabPage()
        Me.picTilesetEditor = New System.Windows.Forms.PictureBox()
        Me.groupTilesetEditor = New System.Windows.Forms.GroupBox()
        Me.lblTilesetName = New System.Windows.Forms.Label()
        Me.txtTilesetName = New System.Windows.Forms.TextBox()
        Me.btnClearTileset = New System.Windows.Forms.Button()
        Me.btnSaveTileset = New System.Windows.Forms.Button()
        Me.cmbTilesetEditor = New System.Windows.Forms.ComboBox()
        Me.scrlTilesetEditorY = New System.Windows.Forms.VScrollBar()
        Me.scrlTilesetEditorX = New System.Windows.Forms.HScrollBar()
        Me.tabAccountEditor = New System.Windows.Forms.TabPage()
        Me.groupPlayerData = New System.Windows.Forms.GroupBox()
        Me.tabAccounts = New System.Windows.Forms.TabControl()
        Me.tabAccount = New System.Windows.Forms.TabPage()
        Me.proptAccountData = New System.Windows.Forms.PropertyGrid()
        Me.btnEditPlayerInventory = New System.Windows.Forms.Button()
        Me.groupAccountList = New System.Windows.Forms.GroupBox()
        Me.lstAccounts = New System.Windows.Forms.ListBox()
        Me.mnuAccountList = New System.Windows.Forms.MenuStrip()
        Me.mnuAccountSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAccountNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAccountUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabNpcEditor = New System.Windows.Forms.TabPage()
        Me.groupNpcData = New System.Windows.Forms.GroupBox()
        Me.tabNpcs = New System.Windows.Forms.TabControl()
        Me.tabNpc = New System.Windows.Forms.TabPage()
        Me.proptNpcData = New System.Windows.Forms.PropertyGrid()
        Me.btnNpcDropTable = New System.Windows.Forms.Button()
        Me.groupNpcList = New System.Windows.Forms.GroupBox()
        Me.lstNpcs = New System.Windows.Forms.ListBox()
        Me.mnuNpcList = New System.Windows.Forms.MenuStrip()
        Me.mnuNpcSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNpcNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNpcUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabItemEditor = New System.Windows.Forms.TabPage()
        Me.groupItemData = New System.Windows.Forms.GroupBox()
        Me.tabItems = New System.Windows.Forms.TabControl()
        Me.tabItem = New System.Windows.Forms.TabPage()
        Me.proptItemData = New System.Windows.Forms.PropertyGrid()
        Me.groupItemList = New System.Windows.Forms.GroupBox()
        Me.lstitems = New System.Windows.Forms.ListBox()
        Me.mnuItemList = New System.Windows.Forms.MenuStrip()
        Me.mnuItemSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItemNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItemUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.imgSprites = New System.Windows.Forms.ImageList(Me.components)
        Me.mnuMain.SuspendLayout()
        Me.tabEditors.SuspendLayout()
        Me.tabMapEditor.SuspendLayout()
        Me.groupMapData.SuspendLayout()
        Me.tabMaps.SuspendLayout()
        Me.tabMap.SuspendLayout()
        CType(Me.mapPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panMapRight.SuspendLayout()
        Me.tabMapObjects.SuspendLayout()
        Me.mapObjectsTiles.SuspendLayout()
        CType(Me.mapPicTileset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupMapTileset.SuspendLayout()
        Me.mapObjectsNpcs.SuspendLayout()
        Me.groupMapTileBtns.SuspendLayout()
        Me.panMapLeft.SuspendLayout()
        Me.tabProperties.SuspendLayout()
        Me.tabMapSettings.SuspendLayout()
        Me.tabEditor.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.mnuMapList.SuspendLayout()
        Me.tabTilesetEditor.SuspendLayout()
        CType(Me.picTilesetEditor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupTilesetEditor.SuspendLayout()
        Me.tabAccountEditor.SuspendLayout()
        Me.groupPlayerData.SuspendLayout()
        Me.tabAccounts.SuspendLayout()
        Me.tabAccount.SuspendLayout()
        Me.groupAccountList.SuspendLayout()
        Me.mnuAccountList.SuspendLayout()
        Me.tabNpcEditor.SuspendLayout()
        Me.groupNpcData.SuspendLayout()
        Me.tabNpcs.SuspendLayout()
        Me.tabNpc.SuspendLayout()
        Me.groupNpcList.SuspendLayout()
        Me.mnuNpcList.SuspendLayout()
        Me.tabItemEditor.SuspendLayout()
        Me.groupItemData.SuspendLayout()
        Me.tabItems.SuspendLayout()
        Me.tabItem.SuspendLayout()
        Me.groupItemList.SuspendLayout()
        Me.mnuItemList.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMain_File, Me.mnuMain_View})
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
        'mnuMain_View
        '
        Me.mnuMain_View.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMain_Textures})
        Me.mnuMain_View.Name = "mnuMain_View"
        Me.mnuMain_View.Size = New System.Drawing.Size(44, 20)
        Me.mnuMain_View.Text = "View"
        '
        'mnuMain_Textures
        '
        Me.mnuMain_Textures.Name = "mnuMain_Textures"
        Me.mnuMain_Textures.Size = New System.Drawing.Size(118, 22)
        Me.mnuMain_Textures.Text = "Textures"
        '
        'tabEditors
        '
        Me.tabEditors.Controls.Add(Me.tabMapEditor)
        Me.tabEditors.Controls.Add(Me.tabTilesetEditor)
        Me.tabEditors.Controls.Add(Me.tabAccountEditor)
        Me.tabEditors.Controls.Add(Me.tabNpcEditor)
        Me.tabEditors.Controls.Add(Me.tabItemEditor)
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
        Me.tabMap.Controls.Add(Me.mapPreview)
        Me.tabMap.Controls.Add(Me.mapScrlY)
        Me.tabMap.Controls.Add(Me.mapScrlX)
        Me.tabMap.ForeColor = System.Drawing.Color.Transparent
        Me.tabMap.Location = New System.Drawing.Point(4, 22)
        Me.tabMap.Name = "tabMap"
        Me.tabMap.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMap.Size = New System.Drawing.Size(376, 375)
        Me.tabMap.TabIndex = 0
        Me.tabMap.Text = "Map"
        Me.tabMap.UseVisualStyleBackColor = True
        '
        'mapPreview
        '
        Me.mapPreview.BackColor = System.Drawing.Color.DarkGray
        Me.mapPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mapPreview.Location = New System.Drawing.Point(3, 3)
        Me.mapPreview.Name = "mapPreview"
        Me.mapPreview.Size = New System.Drawing.Size(353, 352)
        Me.mapPreview.TabIndex = 8
        Me.mapPreview.TabStop = False
        '
        'mapScrlY
        '
        Me.mapScrlY.Dock = System.Windows.Forms.DockStyle.Right
        Me.mapScrlY.LargeChange = 1
        Me.mapScrlY.Location = New System.Drawing.Point(356, 3)
        Me.mapScrlY.Maximum = 32
        Me.mapScrlY.Name = "mapScrlY"
        Me.mapScrlY.Size = New System.Drawing.Size(17, 352)
        Me.mapScrlY.TabIndex = 7
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
        'panMapRight
        '
        Me.panMapRight.Controls.Add(Me.tabMapObjects)
        Me.panMapRight.Controls.Add(Me.groupMapTileBtns)
        Me.panMapRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.panMapRight.Location = New System.Drawing.Point(608, 3)
        Me.panMapRight.Name = "panMapRight"
        Me.panMapRight.Size = New System.Drawing.Size(200, 420)
        Me.panMapRight.TabIndex = 54
        '
        'tabMapObjects
        '
        Me.tabMapObjects.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.tabMapObjects.Controls.Add(Me.mapObjectsTiles)
        Me.tabMapObjects.Controls.Add(Me.mapObjectsNpcs)
        Me.tabMapObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMapObjects.Location = New System.Drawing.Point(0, 0)
        Me.tabMapObjects.Name = "tabMapObjects"
        Me.tabMapObjects.SelectedIndex = 0
        Me.tabMapObjects.Size = New System.Drawing.Size(200, 384)
        Me.tabMapObjects.TabIndex = 50
        '
        'mapObjectsTiles
        '
        Me.mapObjectsTiles.Controls.Add(Me.mapPicTileset)
        Me.mapObjectsTiles.Controls.Add(Me.tileSetScrlY)
        Me.mapObjectsTiles.Controls.Add(Me.tileSetScrlX)
        Me.mapObjectsTiles.Controls.Add(Me.groupMapTileset)
        Me.mapObjectsTiles.Location = New System.Drawing.Point(4, 4)
        Me.mapObjectsTiles.Name = "mapObjectsTiles"
        Me.mapObjectsTiles.Padding = New System.Windows.Forms.Padding(3)
        Me.mapObjectsTiles.Size = New System.Drawing.Size(192, 358)
        Me.mapObjectsTiles.TabIndex = 0
        Me.mapObjectsTiles.Text = "Tiles"
        Me.mapObjectsTiles.UseVisualStyleBackColor = True
        '
        'mapPicTileset
        '
        Me.mapPicTileset.BackColor = System.Drawing.Color.DarkGray
        Me.mapPicTileset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mapPicTileset.Location = New System.Drawing.Point(3, 43)
        Me.mapPicTileset.Name = "mapPicTileset"
        Me.mapPicTileset.Size = New System.Drawing.Size(169, 295)
        Me.mapPicTileset.TabIndex = 58
        Me.mapPicTileset.TabStop = False
        '
        'tileSetScrlY
        '
        Me.tileSetScrlY.Dock = System.Windows.Forms.DockStyle.Right
        Me.tileSetScrlY.LargeChange = 1
        Me.tileSetScrlY.Location = New System.Drawing.Point(172, 43)
        Me.tileSetScrlY.Maximum = 1
        Me.tileSetScrlY.Name = "tileSetScrlY"
        Me.tileSetScrlY.Size = New System.Drawing.Size(17, 295)
        Me.tileSetScrlY.TabIndex = 57
        '
        'tileSetScrlX
        '
        Me.tileSetScrlX.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tileSetScrlX.LargeChange = 1
        Me.tileSetScrlX.Location = New System.Drawing.Point(3, 338)
        Me.tileSetScrlX.Maximum = 1
        Me.tileSetScrlX.Name = "tileSetScrlX"
        Me.tileSetScrlX.Size = New System.Drawing.Size(186, 17)
        Me.tileSetScrlX.TabIndex = 56
        '
        'groupMapTileset
        '
        Me.groupMapTileset.Controls.Add(Me.mapCmbTileSet)
        Me.groupMapTileset.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupMapTileset.Location = New System.Drawing.Point(3, 3)
        Me.groupMapTileset.Name = "groupMapTileset"
        Me.groupMapTileset.Size = New System.Drawing.Size(186, 40)
        Me.groupMapTileset.TabIndex = 59
        Me.groupMapTileset.TabStop = False
        Me.groupMapTileset.Text = "TileSet"
        '
        'mapCmbTileSet
        '
        Me.mapCmbTileSet.Dock = System.Windows.Forms.DockStyle.Top
        Me.mapCmbTileSet.FormattingEnabled = True
        Me.mapCmbTileSet.Location = New System.Drawing.Point(3, 16)
        Me.mapCmbTileSet.Name = "mapCmbTileSet"
        Me.mapCmbTileSet.Size = New System.Drawing.Size(180, 21)
        Me.mapCmbTileSet.TabIndex = 0
        '
        'mapObjectsNpcs
        '
        Me.mapObjectsNpcs.Controls.Add(Me.lstMapNpcs)
        Me.mapObjectsNpcs.Location = New System.Drawing.Point(4, 4)
        Me.mapObjectsNpcs.Name = "mapObjectsNpcs"
        Me.mapObjectsNpcs.Padding = New System.Windows.Forms.Padding(3)
        Me.mapObjectsNpcs.Size = New System.Drawing.Size(192, 358)
        Me.mapObjectsNpcs.TabIndex = 1
        Me.mapObjectsNpcs.Text = "Npcs"
        Me.mapObjectsNpcs.UseVisualStyleBackColor = True
        '
        'lstMapNpcs
        '
        Me.lstMapNpcs.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lstMapNpcs.AutoArrange = False
        Me.lstMapNpcs.BackColor = System.Drawing.Color.DarkGray
        Me.lstMapNpcs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstMapNpcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMapNpcs.ForeColor = System.Drawing.Color.Black
        Me.lstMapNpcs.HideSelection = False
        Me.lstMapNpcs.Location = New System.Drawing.Point(3, 3)
        Me.lstMapNpcs.MultiSelect = False
        Me.lstMapNpcs.Name = "lstMapNpcs"
        Me.lstMapNpcs.Size = New System.Drawing.Size(186, 352)
        Me.lstMapNpcs.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstMapNpcs.TabIndex = 0
        Me.lstMapNpcs.UseCompatibleStateImageBehavior = False
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
        Me.mapBtnClearLayer.Location = New System.Drawing.Point(117, 9)
        Me.mapBtnClearLayer.Name = "mapBtnClearLayer"
        Me.mapBtnClearLayer.Size = New System.Drawing.Size(80, 23)
        Me.mapBtnClearLayer.TabIndex = 12
        Me.mapBtnClearLayer.Tag = "Clear Layer"
        Me.mapBtnClearLayer.Text = "Clear Layer"
        Me.mapBtnClearLayer.UseVisualStyleBackColor = True
        '
        'mapBtnFillLayer
        '
        Me.mapBtnFillLayer.Location = New System.Drawing.Point(3, 9)
        Me.mapBtnFillLayer.Name = "mapBtnFillLayer"
        Me.mapBtnFillLayer.Size = New System.Drawing.Size(80, 23)
        Me.mapBtnFillLayer.TabIndex = 11
        Me.mapBtnFillLayer.Tag = "Fill Layer"
        Me.mapBtnFillLayer.Text = "Fill Layer"
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
        'tabTilesetEditor
        '
        Me.tabTilesetEditor.Controls.Add(Me.picTilesetEditor)
        Me.tabTilesetEditor.Controls.Add(Me.groupTilesetEditor)
        Me.tabTilesetEditor.Controls.Add(Me.scrlTilesetEditorY)
        Me.tabTilesetEditor.Controls.Add(Me.scrlTilesetEditorX)
        Me.tabTilesetEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabTilesetEditor.Name = "tabTilesetEditor"
        Me.tabTilesetEditor.Size = New System.Drawing.Size(811, 426)
        Me.tabTilesetEditor.TabIndex = 4
        Me.tabTilesetEditor.Text = "Tileset Editor"
        Me.tabTilesetEditor.UseVisualStyleBackColor = True
        '
        'picTilesetEditor
        '
        Me.picTilesetEditor.BackColor = System.Drawing.Color.DarkGray
        Me.picTilesetEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picTilesetEditor.Location = New System.Drawing.Point(0, 66)
        Me.picTilesetEditor.Name = "picTilesetEditor"
        Me.picTilesetEditor.Size = New System.Drawing.Size(794, 343)
        Me.picTilesetEditor.TabIndex = 56
        Me.picTilesetEditor.TabStop = False
        '
        'groupTilesetEditor
        '
        Me.groupTilesetEditor.Controls.Add(Me.lblTilesetName)
        Me.groupTilesetEditor.Controls.Add(Me.txtTilesetName)
        Me.groupTilesetEditor.Controls.Add(Me.btnClearTileset)
        Me.groupTilesetEditor.Controls.Add(Me.btnSaveTileset)
        Me.groupTilesetEditor.Controls.Add(Me.cmbTilesetEditor)
        Me.groupTilesetEditor.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupTilesetEditor.Location = New System.Drawing.Point(0, 0)
        Me.groupTilesetEditor.Name = "groupTilesetEditor"
        Me.groupTilesetEditor.Size = New System.Drawing.Size(794, 66)
        Me.groupTilesetEditor.TabIndex = 59
        Me.groupTilesetEditor.TabStop = False
        Me.groupTilesetEditor.Text = "TileSet"
        '
        'lblTilesetName
        '
        Me.lblTilesetName.AutoSize = True
        Me.lblTilesetName.Location = New System.Drawing.Point(9, 40)
        Me.lblTilesetName.Name = "lblTilesetName"
        Me.lblTilesetName.Size = New System.Drawing.Size(38, 13)
        Me.lblTilesetName.TabIndex = 15
        Me.lblTilesetName.Text = "Name:"
        '
        'txtTilesetName
        '
        Me.txtTilesetName.Location = New System.Drawing.Point(53, 39)
        Me.txtTilesetName.Name = "txtTilesetName"
        Me.txtTilesetName.Size = New System.Drawing.Size(568, 20)
        Me.txtTilesetName.TabIndex = 14
        '
        'btnClearTileset
        '
        Me.btnClearTileset.Location = New System.Drawing.Point(708, 37)
        Me.btnClearTileset.Name = "btnClearTileset"
        Me.btnClearTileset.Size = New System.Drawing.Size(80, 23)
        Me.btnClearTileset.TabIndex = 13
        Me.btnClearTileset.Tag = "Clear Tileset"
        Me.btnClearTileset.Text = "Clear Tileset"
        Me.btnClearTileset.UseVisualStyleBackColor = True
        '
        'btnSaveTileset
        '
        Me.btnSaveTileset.Location = New System.Drawing.Point(627, 37)
        Me.btnSaveTileset.Name = "btnSaveTileset"
        Me.btnSaveTileset.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveTileset.TabIndex = 1
        Me.btnSaveTileset.Text = "Save"
        Me.btnSaveTileset.UseVisualStyleBackColor = True
        '
        'cmbTilesetEditor
        '
        Me.cmbTilesetEditor.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmbTilesetEditor.FormattingEnabled = True
        Me.cmbTilesetEditor.Location = New System.Drawing.Point(8, 12)
        Me.cmbTilesetEditor.Name = "cmbTilesetEditor"
        Me.cmbTilesetEditor.Size = New System.Drawing.Size(780, 21)
        Me.cmbTilesetEditor.TabIndex = 0
        '
        'scrlTilesetEditorY
        '
        Me.scrlTilesetEditorY.Dock = System.Windows.Forms.DockStyle.Right
        Me.scrlTilesetEditorY.LargeChange = 1
        Me.scrlTilesetEditorY.Location = New System.Drawing.Point(794, 0)
        Me.scrlTilesetEditorY.Maximum = 1
        Me.scrlTilesetEditorY.Name = "scrlTilesetEditorY"
        Me.scrlTilesetEditorY.Size = New System.Drawing.Size(17, 409)
        Me.scrlTilesetEditorY.TabIndex = 58
        '
        'scrlTilesetEditorX
        '
        Me.scrlTilesetEditorX.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.scrlTilesetEditorX.LargeChange = 1
        Me.scrlTilesetEditorX.Location = New System.Drawing.Point(0, 409)
        Me.scrlTilesetEditorX.Maximum = 1
        Me.scrlTilesetEditorX.Name = "scrlTilesetEditorX"
        Me.scrlTilesetEditorX.Size = New System.Drawing.Size(811, 17)
        Me.scrlTilesetEditorX.TabIndex = 57
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
        Me.tabAccount.Controls.Add(Me.btnEditPlayerInventory)
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
        Me.proptAccountData.Location = New System.Drawing.Point(3, 26)
        Me.proptAccountData.Name = "proptAccountData"
        Me.proptAccountData.Size = New System.Drawing.Size(630, 346)
        Me.proptAccountData.TabIndex = 1
        '
        'btnEditPlayerInventory
        '
        Me.btnEditPlayerInventory.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnEditPlayerInventory.Location = New System.Drawing.Point(3, 3)
        Me.btnEditPlayerInventory.Name = "btnEditPlayerInventory"
        Me.btnEditPlayerInventory.Size = New System.Drawing.Size(630, 23)
        Me.btnEditPlayerInventory.TabIndex = 3
        Me.btnEditPlayerInventory.Text = "Edit Inventory"
        Me.btnEditPlayerInventory.UseVisualStyleBackColor = True
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
        'tabNpcEditor
        '
        Me.tabNpcEditor.Controls.Add(Me.groupNpcData)
        Me.tabNpcEditor.Controls.Add(Me.groupNpcList)
        Me.tabNpcEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabNpcEditor.Name = "tabNpcEditor"
        Me.tabNpcEditor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNpcEditor.Size = New System.Drawing.Size(811, 426)
        Me.tabNpcEditor.TabIndex = 5
        Me.tabNpcEditor.Text = "Npc Editor"
        Me.tabNpcEditor.UseVisualStyleBackColor = True
        '
        'groupNpcData
        '
        Me.groupNpcData.Controls.Add(Me.tabNpcs)
        Me.groupNpcData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupNpcData.Location = New System.Drawing.Point(158, 3)
        Me.groupNpcData.Name = "groupNpcData"
        Me.groupNpcData.Size = New System.Drawing.Size(650, 420)
        Me.groupNpcData.TabIndex = 4
        Me.groupNpcData.TabStop = False
        Me.groupNpcData.Text = "Npc Data"
        '
        'tabNpcs
        '
        Me.tabNpcs.Controls.Add(Me.tabNpc)
        Me.tabNpcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabNpcs.Location = New System.Drawing.Point(3, 16)
        Me.tabNpcs.Name = "tabNpcs"
        Me.tabNpcs.SelectedIndex = 0
        Me.tabNpcs.Size = New System.Drawing.Size(644, 401)
        Me.tabNpcs.TabIndex = 0
        '
        'tabNpc
        '
        Me.tabNpc.Controls.Add(Me.proptNpcData)
        Me.tabNpc.Controls.Add(Me.btnNpcDropTable)
        Me.tabNpc.Location = New System.Drawing.Point(4, 22)
        Me.tabNpc.Name = "tabNpc"
        Me.tabNpc.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNpc.Size = New System.Drawing.Size(636, 375)
        Me.tabNpc.TabIndex = 2
        Me.tabNpc.Text = "Npc"
        Me.tabNpc.UseVisualStyleBackColor = True
        '
        'proptNpcData
        '
        Me.proptNpcData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.proptNpcData.Location = New System.Drawing.Point(3, 26)
        Me.proptNpcData.Name = "proptNpcData"
        Me.proptNpcData.Size = New System.Drawing.Size(630, 346)
        Me.proptNpcData.TabIndex = 1
        '
        'btnNpcDropTable
        '
        Me.btnNpcDropTable.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnNpcDropTable.Location = New System.Drawing.Point(3, 3)
        Me.btnNpcDropTable.Name = "btnNpcDropTable"
        Me.btnNpcDropTable.Size = New System.Drawing.Size(630, 23)
        Me.btnNpcDropTable.TabIndex = 2
        Me.btnNpcDropTable.Text = "Edit Drop Table"
        Me.btnNpcDropTable.UseVisualStyleBackColor = True
        '
        'groupNpcList
        '
        Me.groupNpcList.Controls.Add(Me.lstNpcs)
        Me.groupNpcList.Controls.Add(Me.mnuNpcList)
        Me.groupNpcList.Dock = System.Windows.Forms.DockStyle.Left
        Me.groupNpcList.Location = New System.Drawing.Point(3, 3)
        Me.groupNpcList.Name = "groupNpcList"
        Me.groupNpcList.Size = New System.Drawing.Size(155, 420)
        Me.groupNpcList.TabIndex = 3
        Me.groupNpcList.TabStop = False
        Me.groupNpcList.Text = "Npc List"
        '
        'lstNpcs
        '
        Me.lstNpcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstNpcs.FormattingEnabled = True
        Me.lstNpcs.Location = New System.Drawing.Point(3, 40)
        Me.lstNpcs.Name = "lstNpcs"
        Me.lstNpcs.Size = New System.Drawing.Size(149, 377)
        Me.lstNpcs.Sorted = True
        Me.lstNpcs.TabIndex = 0
        '
        'mnuNpcList
        '
        Me.mnuNpcList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNpcSave, Me.mnuNpcNew, Me.mnuNpcUndo})
        Me.mnuNpcList.Location = New System.Drawing.Point(3, 16)
        Me.mnuNpcList.Name = "mnuNpcList"
        Me.mnuNpcList.Size = New System.Drawing.Size(149, 24)
        Me.mnuNpcList.TabIndex = 1
        '
        'mnuNpcSave
        '
        Me.mnuNpcSave.Name = "mnuNpcSave"
        Me.mnuNpcSave.Size = New System.Drawing.Size(43, 20)
        Me.mnuNpcSave.Text = "Save"
        '
        'mnuNpcNew
        '
        Me.mnuNpcNew.Name = "mnuNpcNew"
        Me.mnuNpcNew.Size = New System.Drawing.Size(43, 20)
        Me.mnuNpcNew.Text = "New"
        '
        'mnuNpcUndo
        '
        Me.mnuNpcUndo.Name = "mnuNpcUndo"
        Me.mnuNpcUndo.Size = New System.Drawing.Size(48, 20)
        Me.mnuNpcUndo.Text = "Undo"
        '
        'tabItemEditor
        '
        Me.tabItemEditor.Controls.Add(Me.groupItemData)
        Me.tabItemEditor.Controls.Add(Me.groupItemList)
        Me.tabItemEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabItemEditor.Name = "tabItemEditor"
        Me.tabItemEditor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabItemEditor.Size = New System.Drawing.Size(811, 426)
        Me.tabItemEditor.TabIndex = 6
        Me.tabItemEditor.Text = "Item Editor"
        Me.tabItemEditor.UseVisualStyleBackColor = True
        '
        'groupItemData
        '
        Me.groupItemData.Controls.Add(Me.tabItems)
        Me.groupItemData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupItemData.Location = New System.Drawing.Point(158, 3)
        Me.groupItemData.Name = "groupItemData"
        Me.groupItemData.Size = New System.Drawing.Size(650, 420)
        Me.groupItemData.TabIndex = 6
        Me.groupItemData.TabStop = False
        Me.groupItemData.Text = "Item Data"
        '
        'tabItems
        '
        Me.tabItems.Controls.Add(Me.tabItem)
        Me.tabItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabItems.Location = New System.Drawing.Point(3, 16)
        Me.tabItems.Name = "tabItems"
        Me.tabItems.SelectedIndex = 0
        Me.tabItems.Size = New System.Drawing.Size(644, 401)
        Me.tabItems.TabIndex = 0
        '
        'tabItem
        '
        Me.tabItem.Controls.Add(Me.proptItemData)
        Me.tabItem.Location = New System.Drawing.Point(4, 22)
        Me.tabItem.Name = "tabItem"
        Me.tabItem.Padding = New System.Windows.Forms.Padding(3)
        Me.tabItem.Size = New System.Drawing.Size(636, 375)
        Me.tabItem.TabIndex = 2
        Me.tabItem.Text = "Item"
        Me.tabItem.UseVisualStyleBackColor = True
        '
        'proptItemData
        '
        Me.proptItemData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.proptItemData.Location = New System.Drawing.Point(3, 3)
        Me.proptItemData.Name = "proptItemData"
        Me.proptItemData.Size = New System.Drawing.Size(630, 369)
        Me.proptItemData.TabIndex = 1
        '
        'groupItemList
        '
        Me.groupItemList.Controls.Add(Me.lstitems)
        Me.groupItemList.Controls.Add(Me.mnuItemList)
        Me.groupItemList.Dock = System.Windows.Forms.DockStyle.Left
        Me.groupItemList.Location = New System.Drawing.Point(3, 3)
        Me.groupItemList.Name = "groupItemList"
        Me.groupItemList.Size = New System.Drawing.Size(155, 420)
        Me.groupItemList.TabIndex = 5
        Me.groupItemList.TabStop = False
        Me.groupItemList.Text = "Item List"
        '
        'lstitems
        '
        Me.lstitems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstitems.FormattingEnabled = True
        Me.lstitems.Location = New System.Drawing.Point(3, 40)
        Me.lstitems.Name = "lstitems"
        Me.lstitems.Size = New System.Drawing.Size(149, 377)
        Me.lstitems.TabIndex = 0
        '
        'mnuItemList
        '
        Me.mnuItemList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItemSave, Me.mnuItemNew, Me.mnuItemUndo})
        Me.mnuItemList.Location = New System.Drawing.Point(3, 16)
        Me.mnuItemList.Name = "mnuItemList"
        Me.mnuItemList.Size = New System.Drawing.Size(149, 24)
        Me.mnuItemList.TabIndex = 1
        '
        'mnuItemSave
        '
        Me.mnuItemSave.Name = "mnuItemSave"
        Me.mnuItemSave.Size = New System.Drawing.Size(43, 20)
        Me.mnuItemSave.Text = "Save"
        '
        'mnuItemNew
        '
        Me.mnuItemNew.Name = "mnuItemNew"
        Me.mnuItemNew.Size = New System.Drawing.Size(43, 20)
        Me.mnuItemNew.Text = "New"
        '
        'mnuItemUndo
        '
        Me.mnuItemUndo.Name = "mnuItemUndo"
        Me.mnuItemUndo.Size = New System.Drawing.Size(48, 20)
        Me.mnuItemUndo.Text = "Undo"
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
        Me.tabMapObjects.ResumeLayout(False)
        Me.mapObjectsTiles.ResumeLayout(False)
        CType(Me.mapPicTileset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupMapTileset.ResumeLayout(False)
        Me.mapObjectsNpcs.ResumeLayout(False)
        Me.groupMapTileBtns.ResumeLayout(False)
        Me.panMapLeft.ResumeLayout(False)
        Me.tabProperties.ResumeLayout(False)
        Me.tabMapSettings.ResumeLayout(False)
        Me.tabEditor.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.mnuMapList.ResumeLayout(False)
        Me.mnuMapList.PerformLayout()
        Me.tabTilesetEditor.ResumeLayout(False)
        CType(Me.picTilesetEditor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupTilesetEditor.ResumeLayout(False)
        Me.groupTilesetEditor.PerformLayout()
        Me.tabAccountEditor.ResumeLayout(False)
        Me.groupPlayerData.ResumeLayout(False)
        Me.tabAccounts.ResumeLayout(False)
        Me.tabAccount.ResumeLayout(False)
        Me.groupAccountList.ResumeLayout(False)
        Me.groupAccountList.PerformLayout()
        Me.mnuAccountList.ResumeLayout(False)
        Me.mnuAccountList.PerformLayout()
        Me.tabNpcEditor.ResumeLayout(False)
        Me.groupNpcData.ResumeLayout(False)
        Me.tabNpcs.ResumeLayout(False)
        Me.tabNpc.ResumeLayout(False)
        Me.groupNpcList.ResumeLayout(False)
        Me.groupNpcList.PerformLayout()
        Me.mnuNpcList.ResumeLayout(False)
        Me.mnuNpcList.PerformLayout()
        Me.tabItemEditor.ResumeLayout(False)
        Me.groupItemData.ResumeLayout(False)
        Me.tabItems.ResumeLayout(False)
        Me.tabItem.ResumeLayout(False)
        Me.groupItemList.ResumeLayout(False)
        Me.groupItemList.PerformLayout()
        Me.mnuItemList.ResumeLayout(False)
        Me.mnuItemList.PerformLayout()
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
    Friend WithEvents mnuMain_Publish As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccountNew As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents proptMapData As System.Windows.Forms.PropertyGrid
    Public WithEvents proptMapEditorData As System.Windows.Forms.PropertyGrid
    Friend WithEvents groupMapData As System.Windows.Forms.GroupBox
    Friend WithEvents tabMaps As System.Windows.Forms.TabControl
    Friend WithEvents tabMap As System.Windows.Forms.TabPage
    Friend WithEvents mapScrlX As System.Windows.Forms.HScrollBar
    Friend WithEvents mnuMapList As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuMapSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMapNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMapUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMain_Sync As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents groupMapTileBtns As System.Windows.Forms.GroupBox
    Private WithEvents mapBtnClearLayer As System.Windows.Forms.Button
    Private WithEvents mapBtnFillLayer As System.Windows.Forms.Button
    Friend WithEvents tabTilesetEditor As System.Windows.Forms.TabPage
    Friend WithEvents groupTilesetEditor As System.Windows.Forms.GroupBox
    Public WithEvents cmbTilesetEditor As System.Windows.Forms.ComboBox
    Friend WithEvents scrlTilesetEditorY As System.Windows.Forms.VScrollBar
    Friend WithEvents scrlTilesetEditorX As System.Windows.Forms.HScrollBar
    Friend WithEvents picTilesetEditor As System.Windows.Forms.PictureBox
    Friend WithEvents btnSaveTileset As System.Windows.Forms.Button
    Private WithEvents btnClearTileset As System.Windows.Forms.Button
    Friend WithEvents lblTilesetName As System.Windows.Forms.Label
    Friend WithEvents txtTilesetName As System.Windows.Forms.TextBox
    Friend WithEvents mapPreview As System.Windows.Forms.PictureBox
    Friend WithEvents mapScrlY As System.Windows.Forms.VScrollBar
    Friend WithEvents tabNpcEditor As System.Windows.Forms.TabPage
    Friend WithEvents groupNpcData As System.Windows.Forms.GroupBox
    Friend WithEvents tabNpcs As System.Windows.Forms.TabControl
    Friend WithEvents tabNpc As System.Windows.Forms.TabPage
    Friend WithEvents proptNpcData As System.Windows.Forms.PropertyGrid
    Friend WithEvents groupNpcList As System.Windows.Forms.GroupBox
    Friend WithEvents lstNpcs As System.Windows.Forms.ListBox
    Friend WithEvents mnuNpcList As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuNpcSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNpcNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNpcUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMain_View As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMain_Textures As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tabItemEditor As System.Windows.Forms.TabPage
    Friend WithEvents groupItemData As System.Windows.Forms.GroupBox
    Friend WithEvents tabItems As System.Windows.Forms.TabControl
    Friend WithEvents tabItem As System.Windows.Forms.TabPage
    Friend WithEvents proptItemData As System.Windows.Forms.PropertyGrid
    Friend WithEvents groupItemList As System.Windows.Forms.GroupBox
    Friend WithEvents lstitems As System.Windows.Forms.ListBox
    Friend WithEvents mnuItemList As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuItemSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItemNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItemUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnNpcDropTable As System.Windows.Forms.Button
    Friend WithEvents btnEditPlayerInventory As System.Windows.Forms.Button
    Friend WithEvents tabMapObjects As System.Windows.Forms.TabControl
    Friend WithEvents mapObjectsTiles As System.Windows.Forms.TabPage
    Friend WithEvents mapPicTileset As System.Windows.Forms.PictureBox
    Friend WithEvents tileSetScrlY As System.Windows.Forms.VScrollBar
    Friend WithEvents tileSetScrlX As System.Windows.Forms.HScrollBar
    Friend WithEvents groupMapTileset As System.Windows.Forms.GroupBox
    Public WithEvents mapCmbTileSet As System.Windows.Forms.ComboBox
    Friend WithEvents mapObjectsNpcs As System.Windows.Forms.TabPage
    Friend WithEvents lstMapNpcs As System.Windows.Forms.ListView

End Class
