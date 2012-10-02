<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mdiMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mdiMain))
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.msMain_tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiFile_tsmiNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiFile_tsmiSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiFile_tsmiSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiFile_tsmiOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiFile_tssSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.msMain_tsmiFile_tsmiExportHTML = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiFile_tssSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.msMain_tsmiFile_tsmiNoRD = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiFile_tssSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.msMain_tsmiFile_tsmiExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiView = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiView_tsmiFoods = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiView_tsmiSpices = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiView_tsmiDefinitions = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiView_tsmiAutoConvertSpices = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiView_tssSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.msMain_tsmiView_tsmiIWannaCook = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiView_tsmiMyShelf = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiTools_tsmiOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.msMain_tsmiTools_tsmiLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsMain_tsmiTools_tsmiLanguage_tssSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.msMain_tsmiWindows = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiWindows_tsmiCascade = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiWindows_tsmiTileVer = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiWindows_tsmiTileHor = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiWindows_tsmiArrIcons = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiHelp_tsmiContents = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiHelp_tsmiIndex = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiHelp_tsmiSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain_tsmiHelp_tsmiSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.msMain_tsmiHelp_tsmiAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.tsMain_tsbFoods = New System.Windows.Forms.ToolStripButton()
        Me.tsMain_tsbSpices = New System.Windows.Forms.ToolStripButton()
        Me.tsMain_tsbDefinitions = New System.Windows.Forms.ToolStripButton()
        Me.tsMain_tsbAutoConvert = New System.Windows.Forms.ToolStripButton()
        Me.tsMain_tssSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsMain_tsbIWannaCook = New System.Windows.Forms.ToolStripButton()
        Me.tsMain_tsbMyShelf = New System.Windows.Forms.ToolStripButton()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.ssMain_tsslMain = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ilMain = New System.Windows.Forms.ImageList(Me.components)
        Me.sfdMain = New System.Windows.Forms.SaveFileDialog()
        Me.sfdExport = New System.Windows.Forms.SaveFileDialog()
        Me.msMain.SuspendLayout()
        Me.tsMain.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msMain_tsmiFile, Me.msMain_tsmiView, Me.msMain_tsmiTools, Me.msMain_tsmiWindows, Me.msMain_tsmiHelp})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.MdiWindowListItem = Me.msMain_tsmiWindows
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(632, 24)
        Me.msMain.TabIndex = 5
        Me.msMain.Text = "Main Menu"
        '
        'msMain_tsmiFile
        '
        Me.msMain_tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msMain_tsmiFile_tsmiNew, Me.msMain_tsmiFile_tsmiSave, Me.msMain_tsmiFile_tsmiSaveAs, Me.msMain_tsmiFile_tsmiOpen, Me.msMain_tsmiFile_tssSep1, Me.msMain_tsmiFile_tsmiExportHTML, Me.msMain_tsmiFile_tssSep2, Me.msMain_tsmiFile_tsmiNoRD, Me.msMain_tsmiFile_tssSep3, Me.msMain_tsmiFile_tsmiExit})
        Me.msMain_tsmiFile.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder
        Me.msMain_tsmiFile.Name = "msMain_tsmiFile"
        Me.msMain_tsmiFile.Size = New System.Drawing.Size(37, 20)
        Me.msMain_tsmiFile.Text = "&File"
        '
        'msMain_tsmiFile_tsmiNew
        '
        Me.msMain_tsmiFile_tsmiNew.Image = Global.SpiceMaster.My.Resources.Resources.NewDocumentHS
        Me.msMain_tsmiFile_tsmiNew.ImageTransparentColor = System.Drawing.Color.Black
        Me.msMain_tsmiFile_tsmiNew.Name = "msMain_tsmiFile_tsmiNew"
        Me.msMain_tsmiFile_tsmiNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.msMain_tsmiFile_tsmiNew.Size = New System.Drawing.Size(206, 22)
        Me.msMain_tsmiFile_tsmiNew.Text = "&New"
        '
        'msMain_tsmiFile_tsmiSave
        '
        Me.msMain_tsmiFile_tsmiSave.Image = Global.SpiceMaster.My.Resources.Resources.saveHS
        Me.msMain_tsmiFile_tsmiSave.ImageTransparentColor = System.Drawing.Color.Black
        Me.msMain_tsmiFile_tsmiSave.Name = "msMain_tsmiFile_tsmiSave"
        Me.msMain_tsmiFile_tsmiSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.msMain_tsmiFile_tsmiSave.Size = New System.Drawing.Size(206, 22)
        Me.msMain_tsmiFile_tsmiSave.Text = "&Save"
        '
        'msMain_tsmiFile_tsmiSaveAs
        '
        Me.msMain_tsmiFile_tsmiSaveAs.Image = Global.SpiceMaster.My.Resources.Resources.saveHS
        Me.msMain_tsmiFile_tsmiSaveAs.ImageTransparentColor = System.Drawing.Color.Black
        Me.msMain_tsmiFile_tsmiSaveAs.Name = "msMain_tsmiFile_tsmiSaveAs"
        Me.msMain_tsmiFile_tsmiSaveAs.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.msMain_tsmiFile_tsmiSaveAs.Size = New System.Drawing.Size(206, 22)
        Me.msMain_tsmiFile_tsmiSaveAs.Text = "Save &as..."
        '
        'msMain_tsmiFile_tsmiOpen
        '
        Me.msMain_tsmiFile_tsmiOpen.Image = Global.SpiceMaster.My.Resources.Resources.OpenFileHS
        Me.msMain_tsmiFile_tsmiOpen.ImageTransparentColor = System.Drawing.Color.Black
        Me.msMain_tsmiFile_tsmiOpen.Name = "msMain_tsmiFile_tsmiOpen"
        Me.msMain_tsmiFile_tsmiOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.msMain_tsmiFile_tsmiOpen.Size = New System.Drawing.Size(206, 22)
        Me.msMain_tsmiFile_tsmiOpen.Text = "&Open..."
        '
        'msMain_tsmiFile_tssSep1
        '
        Me.msMain_tsmiFile_tssSep1.Name = "msMain_tsmiFile_tssSep1"
        Me.msMain_tsmiFile_tssSep1.Size = New System.Drawing.Size(203, 6)
        '
        'msMain_tsmiFile_tsmiExportHTML
        '
        Me.msMain_tsmiFile_tsmiExportHTML.Name = "msMain_tsmiFile_tsmiExportHTML"
        Me.msMain_tsmiFile_tsmiExportHTML.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.msMain_tsmiFile_tsmiExportHTML.Size = New System.Drawing.Size(206, 22)
        Me.msMain_tsmiFile_tsmiExportHTML.Text = "&Export to HTML..."
        '
        'msMain_tsmiFile_tssSep2
        '
        Me.msMain_tsmiFile_tssSep2.Name = "msMain_tsmiFile_tssSep2"
        Me.msMain_tsmiFile_tssSep2.Size = New System.Drawing.Size(203, 6)
        '
        'msMain_tsmiFile_tsmiNoRD
        '
        Me.msMain_tsmiFile_tsmiNoRD.Enabled = False
        Me.msMain_tsmiFile_tsmiNoRD.Name = "msMain_tsmiFile_tsmiNoRD"
        Me.msMain_tsmiFile_tsmiNoRD.Size = New System.Drawing.Size(206, 22)
        Me.msMain_tsmiFile_tsmiNoRD.Text = "<No recent documents>"
        '
        'msMain_tsmiFile_tssSep3
        '
        Me.msMain_tsmiFile_tssSep3.Name = "msMain_tsmiFile_tssSep3"
        Me.msMain_tsmiFile_tssSep3.Size = New System.Drawing.Size(203, 6)
        '
        'msMain_tsmiFile_tsmiExit
        '
        Me.msMain_tsmiFile_tsmiExit.Name = "msMain_tsmiFile_tsmiExit"
        Me.msMain_tsmiFile_tsmiExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.msMain_tsmiFile_tsmiExit.Size = New System.Drawing.Size(206, 22)
        Me.msMain_tsmiFile_tsmiExit.Text = "E&xit"
        '
        'msMain_tsmiView
        '
        Me.msMain_tsmiView.Checked = True
        Me.msMain_tsmiView.CheckState = System.Windows.Forms.CheckState.Checked
        Me.msMain_tsmiView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msMain_tsmiView_tsmiFoods, Me.msMain_tsmiView_tsmiSpices, Me.msMain_tsmiView_tsmiDefinitions, Me.msMain_tsmiView_tsmiAutoConvertSpices, Me.msMain_tsmiView_tssSep1, Me.msMain_tsmiView_tsmiIWannaCook, Me.msMain_tsmiView_tsmiMyShelf})
        Me.msMain_tsmiView.Name = "msMain_tsmiView"
        Me.msMain_tsmiView.Size = New System.Drawing.Size(44, 20)
        Me.msMain_tsmiView.Text = "&View"
        '
        'msMain_tsmiView_tsmiFoods
        '
        Me.msMain_tsmiView_tsmiFoods.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_FOODS
        Me.msMain_tsmiView_tsmiFoods.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.msMain_tsmiView_tsmiFoods.Name = "msMain_tsmiView_tsmiFoods"
        Me.msMain_tsmiView_tsmiFoods.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.msMain_tsmiView_tsmiFoods.Size = New System.Drawing.Size(228, 22)
        Me.msMain_tsmiView_tsmiFoods.Text = "Foods"
        '
        'msMain_tsmiView_tsmiSpices
        '
        Me.msMain_tsmiView_tsmiSpices.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_SPICES
        Me.msMain_tsmiView_tsmiSpices.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.msMain_tsmiView_tsmiSpices.Name = "msMain_tsmiView_tsmiSpices"
        Me.msMain_tsmiView_tsmiSpices.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.msMain_tsmiView_tsmiSpices.Size = New System.Drawing.Size(228, 22)
        Me.msMain_tsmiView_tsmiSpices.Text = "Spices"
        '
        'msMain_tsmiView_tsmiDefinitions
        '
        Me.msMain_tsmiView_tsmiDefinitions.Name = "msMain_tsmiView_tsmiDefinitions"
        Me.msMain_tsmiView_tsmiDefinitions.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.msMain_tsmiView_tsmiDefinitions.Size = New System.Drawing.Size(228, 22)
        Me.msMain_tsmiView_tsmiDefinitions.Text = "Definitions"
        '
        'msMain_tsmiView_tsmiAutoConvertSpices
        '
        Me.msMain_tsmiView_tsmiAutoConvertSpices.Name = "msMain_tsmiView_tsmiAutoConvertSpices"
        Me.msMain_tsmiView_tsmiAutoConvertSpices.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.msMain_tsmiView_tsmiAutoConvertSpices.Size = New System.Drawing.Size(228, 22)
        Me.msMain_tsmiView_tsmiAutoConvertSpices.Text = "Auto Convert Spices..."
        '
        'msMain_tsmiView_tssSep1
        '
        Me.msMain_tsmiView_tssSep1.Name = "msMain_tsmiView_tssSep1"
        Me.msMain_tsmiView_tssSep1.Size = New System.Drawing.Size(225, 6)
        '
        'msMain_tsmiView_tsmiIWannaCook
        '
        Me.msMain_tsmiView_tsmiIWannaCook.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_APPLICATION
        Me.msMain_tsmiView_tsmiIWannaCook.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.msMain_tsmiView_tsmiIWannaCook.Name = "msMain_tsmiView_tsmiIWannaCook"
        Me.msMain_tsmiView_tsmiIWannaCook.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.msMain_tsmiView_tsmiIWannaCook.Size = New System.Drawing.Size(228, 22)
        Me.msMain_tsmiView_tsmiIWannaCook.Text = "I Wanna Cook!"
        '
        'msMain_tsmiView_tsmiMyShelf
        '
        Me.msMain_tsmiView_tsmiMyShelf.Name = "msMain_tsmiView_tsmiMyShelf"
        Me.msMain_tsmiView_tsmiMyShelf.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.msMain_tsmiView_tsmiMyShelf.Size = New System.Drawing.Size(228, 22)
        Me.msMain_tsmiView_tsmiMyShelf.Text = "My Shelf"
        '
        'msMain_tsmiTools
        '
        Me.msMain_tsmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msMain_tsmiTools_tsmiOptions, Me.ToolStripMenuItem1, Me.msMain_tsmiTools_tsmiLanguage})
        Me.msMain_tsmiTools.Name = "msMain_tsmiTools"
        Me.msMain_tsmiTools.Size = New System.Drawing.Size(48, 20)
        Me.msMain_tsmiTools.Text = "&Tools"
        '
        'msMain_tsmiTools_tsmiOptions
        '
        Me.msMain_tsmiTools_tsmiOptions.Image = Global.SpiceMaster.My.Resources.Resources.OptionsHS
        Me.msMain_tsmiTools_tsmiOptions.ImageTransparentColor = System.Drawing.Color.Black
        Me.msMain_tsmiTools_tsmiOptions.Name = "msMain_tsmiTools_tsmiOptions"
        Me.msMain_tsmiTools_tsmiOptions.Size = New System.Drawing.Size(126, 22)
        Me.msMain_tsmiTools_tsmiOptions.Text = "&Options..."
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(123, 6)
        '
        'msMain_tsmiTools_tsmiLanguage
        '
        Me.msMain_tsmiTools_tsmiLanguage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager, Me.tsMain_tsmiTools_tsmiLanguage_tssSep1})
        Me.msMain_tsmiTools_tsmiLanguage.Name = "msMain_tsmiTools_tsmiLanguage"
        Me.msMain_tsmiTools_tsmiLanguage.Size = New System.Drawing.Size(126, 22)
        Me.msMain_tsmiTools_tsmiLanguage.Text = "&Language"
        '
        'msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager
        '
        Me.msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager.Name = "msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager"
        Me.msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager.Size = New System.Drawing.Size(226, 22)
        Me.msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager.Text = "Languages Manager..."
        '
        'tsMain_tsmiTools_tsmiLanguage_tssSep1
        '
        Me.tsMain_tsmiTools_tsmiLanguage_tssSep1.Name = "tsMain_tsmiTools_tsmiLanguage_tssSep1"
        Me.tsMain_tsmiTools_tsmiLanguage_tssSep1.Size = New System.Drawing.Size(223, 6)
        '
        'msMain_tsmiWindows
        '
        Me.msMain_tsmiWindows.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msMain_tsmiWindows_tsmiCascade, Me.msMain_tsmiWindows_tsmiTileVer, Me.msMain_tsmiWindows_tsmiTileHor, Me.msMain_tsmiWindows_tsmiArrIcons})
        Me.msMain_tsmiWindows.Name = "msMain_tsmiWindows"
        Me.msMain_tsmiWindows.Size = New System.Drawing.Size(68, 20)
        Me.msMain_tsmiWindows.Text = "&Windows"
        '
        'msMain_tsmiWindows_tsmiCascade
        '
        Me.msMain_tsmiWindows_tsmiCascade.Name = "msMain_tsmiWindows_tsmiCascade"
        Me.msMain_tsmiWindows_tsmiCascade.Size = New System.Drawing.Size(151, 22)
        Me.msMain_tsmiWindows_tsmiCascade.Text = "&Cascade"
        '
        'msMain_tsmiWindows_tsmiTileVer
        '
        Me.msMain_tsmiWindows_tsmiTileVer.Name = "msMain_tsmiWindows_tsmiTileVer"
        Me.msMain_tsmiWindows_tsmiTileVer.Size = New System.Drawing.Size(151, 22)
        Me.msMain_tsmiWindows_tsmiTileVer.Text = "Tile &Vertical"
        '
        'msMain_tsmiWindows_tsmiTileHor
        '
        Me.msMain_tsmiWindows_tsmiTileHor.Name = "msMain_tsmiWindows_tsmiTileHor"
        Me.msMain_tsmiWindows_tsmiTileHor.Size = New System.Drawing.Size(151, 22)
        Me.msMain_tsmiWindows_tsmiTileHor.Text = "Tile &Horizontal"
        '
        'msMain_tsmiWindows_tsmiArrIcons
        '
        Me.msMain_tsmiWindows_tsmiArrIcons.Name = "msMain_tsmiWindows_tsmiArrIcons"
        Me.msMain_tsmiWindows_tsmiArrIcons.Size = New System.Drawing.Size(151, 22)
        Me.msMain_tsmiWindows_tsmiArrIcons.Text = "&Arrange Icons"
        '
        'msMain_tsmiHelp
        '
        Me.msMain_tsmiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msMain_tsmiHelp_tsmiContents, Me.msMain_tsmiHelp_tsmiIndex, Me.msMain_tsmiHelp_tsmiSearch, Me.msMain_tsmiHelp_tsmiSep1, Me.msMain_tsmiHelp_tsmiAbout})
        Me.msMain_tsmiHelp.Name = "msMain_tsmiHelp"
        Me.msMain_tsmiHelp.Size = New System.Drawing.Size(44, 20)
        Me.msMain_tsmiHelp.Text = "&Help"
        '
        'msMain_tsmiHelp_tsmiContents
        '
        Me.msMain_tsmiHelp_tsmiContents.Enabled = False
        Me.msMain_tsmiHelp_tsmiContents.Name = "msMain_tsmiHelp_tsmiContents"
        Me.msMain_tsmiHelp_tsmiContents.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
        Me.msMain_tsmiHelp_tsmiContents.Size = New System.Drawing.Size(168, 22)
        Me.msMain_tsmiHelp_tsmiContents.Text = "&Contents"
        '
        'msMain_tsmiHelp_tsmiIndex
        '
        Me.msMain_tsmiHelp_tsmiIndex.Enabled = False
        Me.msMain_tsmiHelp_tsmiIndex.Image = CType(resources.GetObject("msMain_tsmiHelp_tsmiIndex.Image"), System.Drawing.Image)
        Me.msMain_tsmiHelp_tsmiIndex.ImageTransparentColor = System.Drawing.Color.Black
        Me.msMain_tsmiHelp_tsmiIndex.Name = "msMain_tsmiHelp_tsmiIndex"
        Me.msMain_tsmiHelp_tsmiIndex.Size = New System.Drawing.Size(168, 22)
        Me.msMain_tsmiHelp_tsmiIndex.Text = "&Index"
        '
        'msMain_tsmiHelp_tsmiSearch
        '
        Me.msMain_tsmiHelp_tsmiSearch.Enabled = False
        Me.msMain_tsmiHelp_tsmiSearch.Image = CType(resources.GetObject("msMain_tsmiHelp_tsmiSearch.Image"), System.Drawing.Image)
        Me.msMain_tsmiHelp_tsmiSearch.ImageTransparentColor = System.Drawing.Color.Black
        Me.msMain_tsmiHelp_tsmiSearch.Name = "msMain_tsmiHelp_tsmiSearch"
        Me.msMain_tsmiHelp_tsmiSearch.Size = New System.Drawing.Size(168, 22)
        Me.msMain_tsmiHelp_tsmiSearch.Text = "&Search"
        '
        'msMain_tsmiHelp_tsmiSep1
        '
        Me.msMain_tsmiHelp_tsmiSep1.Name = "msMain_tsmiHelp_tsmiSep1"
        Me.msMain_tsmiHelp_tsmiSep1.Size = New System.Drawing.Size(165, 6)
        '
        'msMain_tsmiHelp_tsmiAbout
        '
        Me.msMain_tsmiHelp_tsmiAbout.Name = "msMain_tsmiHelp_tsmiAbout"
        Me.msMain_tsmiHelp_tsmiAbout.Size = New System.Drawing.Size(168, 22)
        Me.msMain_tsmiHelp_tsmiAbout.Text = "&About ..."
        '
        'tsMain
        '
        Me.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsMain_tsbFoods, Me.tsMain_tsbSpices, Me.tsMain_tsbDefinitions, Me.tsMain_tsbAutoConvert, Me.tsMain_tssSep1, Me.tsMain_tsbIWannaCook, Me.tsMain_tsbMyShelf})
        Me.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tsMain.Location = New System.Drawing.Point(0, 24)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Size = New System.Drawing.Size(632, 25)
        Me.tsMain.TabIndex = 6
        Me.tsMain.Text = "View"
        '
        'tsMain_tsbFoods
        '
        Me.tsMain_tsbFoods.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_FOODS
        Me.tsMain_tsbFoods.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsMain_tsbFoods.Name = "tsMain_tsbFoods"
        Me.tsMain_tsbFoods.Size = New System.Drawing.Size(59, 22)
        Me.tsMain_tsbFoods.Text = "Foods"
        '
        'tsMain_tsbSpices
        '
        Me.tsMain_tsbSpices.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_SPICES
        Me.tsMain_tsbSpices.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsMain_tsbSpices.Name = "tsMain_tsbSpices"
        Me.tsMain_tsbSpices.Size = New System.Drawing.Size(60, 22)
        Me.tsMain_tsbSpices.Text = "Spices"
        '
        'tsMain_tsbDefinitions
        '
        Me.tsMain_tsbDefinitions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsMain_tsbDefinitions.Image = CType(resources.GetObject("tsMain_tsbDefinitions.Image"), System.Drawing.Image)
        Me.tsMain_tsbDefinitions.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsMain_tsbDefinitions.Name = "tsMain_tsbDefinitions"
        Me.tsMain_tsbDefinitions.Size = New System.Drawing.Size(68, 22)
        Me.tsMain_tsbDefinitions.Text = "Definitions"
        '
        'tsMain_tsbAutoConvert
        '
        Me.tsMain_tsbAutoConvert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsMain_tsbAutoConvert.Image = CType(resources.GetObject("tsMain_tsbAutoConvert.Image"), System.Drawing.Image)
        Me.tsMain_tsbAutoConvert.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsMain_tsbAutoConvert.Name = "tsMain_tsbAutoConvert"
        Me.tsMain_tsbAutoConvert.Size = New System.Drawing.Size(127, 22)
        Me.tsMain_tsbAutoConvert.Text = "Auto Convert Spices..."
        '
        'tsMain_tssSep1
        '
        Me.tsMain_tssSep1.Name = "tsMain_tssSep1"
        Me.tsMain_tssSep1.Size = New System.Drawing.Size(6, 25)
        '
        'tsMain_tsbIWannaCook
        '
        Me.tsMain_tsbIWannaCook.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_APPLICATION
        Me.tsMain_tsbIWannaCook.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsMain_tsbIWannaCook.Name = "tsMain_tsbIWannaCook"
        Me.tsMain_tsbIWannaCook.Size = New System.Drawing.Size(104, 22)
        Me.tsMain_tsbIWannaCook.Text = "I Wanna Cook!"
        '
        'tsMain_tsbMyShelf
        '
        Me.tsMain_tsbMyShelf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsMain_tsbMyShelf.Image = CType(resources.GetObject("tsMain_tsbMyShelf.Image"), System.Drawing.Image)
        Me.tsMain_tsbMyShelf.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsMain_tsbMyShelf.Name = "tsMain_tsbMyShelf"
        Me.tsMain_tsbMyShelf.Size = New System.Drawing.Size(57, 22)
        Me.tsMain_tsbMyShelf.Text = "My Shelf"
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ssMain_tsslMain})
        Me.ssMain.Location = New System.Drawing.Point(0, 431)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(632, 22)
        Me.ssMain.TabIndex = 7
        '
        'ssMain_tsslMain
        '
        Me.ssMain_tsslMain.Name = "ssMain_tsslMain"
        Me.ssMain_tsslMain.Size = New System.Drawing.Size(39, 17)
        Me.ssMain_tsslMain.Text = "Ready"
        '
        'ilMain
        '
        Me.ilMain.ImageStream = CType(resources.GetObject("ilMain.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilMain.TransparentColor = System.Drawing.Color.Transparent
        Me.ilMain.Images.SetKeyName(0, "smiley1.jpg")
        '
        'sfdMain
        '
        Me.sfdMain.DefaultExt = "*.smd"
        Me.sfdMain.Filter = "SpiceMaster Documents (*.smd)|*.smd|All files (*.*)|*.*"
        Me.sfdMain.Title = "Select file to save"
        '
        'sfdExport
        '
        Me.sfdExport.DefaultExt = "*.htm"
        Me.sfdExport.FileName = "Spice Master Document by G.Kanellopoulos (e.f. by C.Basdekis).htm"
        Me.sfdExport.Filter = "HTML files (*.htm)|*.htm|HTML files (*.html)|*.html|All files (*.*)|*.*"
        Me.sfdExport.Title = "Select file to export"
        '
        'mdiMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.Controls.Add(Me.tsMain)
        Me.Controls.Add(Me.msMain)
        Me.Controls.Add(Me.ssMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.msMain
        Me.Name = "mdiMain"
        Me.Text = "SpiceMaster"
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents msMain_tsmiHelp_tsmiContents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiHelp_tsmiIndex As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiHelp_tsmiSearch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiHelp_tsmiSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents msMain_tsmiHelp_tsmiAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiWindows_tsmiArrIcons As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiWindows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiWindows_tsmiCascade As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiWindows_tsmiTileVer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiWindows_tsmiTileHor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiTools_tsmiOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ssMain_tsslMain As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ssMain As System.Windows.Forms.StatusStrip
    Friend WithEvents tsMain As System.Windows.Forms.ToolStrip
    Friend WithEvents msMain_tsmiFile_tsmiExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain As System.Windows.Forms.MenuStrip
    Friend WithEvents msMain_tsmiView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ilMain As System.Windows.Forms.ImageList
    Friend WithEvents tsMain_tsbSpices As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsMain_tsbFoods As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsMain_tssSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsMain_tsbIWannaCook As System.Windows.Forms.ToolStripButton
    Friend WithEvents msMain_tsmiFile_tsmiNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiFile_tsmiSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiFile_tsmiOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiFile_tssSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents msMain_tsmiView_tsmiFoods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiView_tsmiSpices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiView_tssSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents msMain_tsmiView_tsmiIWannaCook As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sfdMain As System.Windows.Forms.SaveFileDialog
    Friend WithEvents tsMain_tsbDefinitions As System.Windows.Forms.ToolStripButton
    Friend WithEvents msMain_tsmiView_tsmiDefinitions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsMain_tsbMyShelf As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsMain_tsbAutoConvert As System.Windows.Forms.ToolStripButton
    Friend WithEvents msMain_tsmiFile_tsmiExportHTML As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiFile_tssSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents msMain_tsmiView_tsmiAutoConvertSpices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiView_tsmiMyShelf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sfdExport As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents msMain_tsmiTools_tsmiLanguage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsMain_tsmiTools_tsmiLanguage_tssSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents msMain_tsmiFile_tsmiNoRD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain_tsmiFile_tssSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents msMain_tsmiFile_tsmiSave As System.Windows.Forms.ToolStripMenuItem

End Class
