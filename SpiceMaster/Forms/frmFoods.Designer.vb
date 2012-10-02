<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFoods
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFoods))
        Me.tscMain = New System.Windows.Forms.ToolStripContainer()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.scManage = New System.Windows.Forms.SplitContainer()
        Me.gbFoods = New System.Windows.Forms.GroupBox()
        Me.tvFoods = New System.Windows.Forms.TreeView()
        Me.tsFoods = New System.Windows.Forms.ToolStrip()
        Me.gbFoodsComments = New System.Windows.Forms.GroupBox()
        Me.lvFoodsComments = New System.Windows.Forms.ListView()
        Me.tsFoodsComments = New System.Windows.Forms.ToolStrip()
        Me.scView = New System.Windows.Forms.SplitContainer()
        Me.wbFoods = New System.Windows.Forms.WebBrowser()
        Me.gbCombinations = New System.Windows.Forms.GroupBox()
        Me.lvCombinations = New System.Windows.Forms.ListView()
        Me.lvCombinations_chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tsCombinations = New System.Windows.Forms.ToolStrip()
        Me.tsBrowser = New System.Windows.Forms.ToolStrip()
        Me.tsBrowser_tssbRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tsBrowser_tssSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBrowser_tssbBack = New System.Windows.Forms.ToolStripSplitButton()
        Me.tsBrowser_tssbForward = New System.Windows.Forms.ToolStripSplitButton()
        Me.tsBrowser_tssSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBrowser_tsbUp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBrowser_tstLAddress = New System.Windows.Forms.ToolStripLabel()
        Me.tsBrowser_tstbName = New System.Windows.Forms.ToolStripTextBox()
        Me.tsBrowser_tsbGoTo = New System.Windows.Forms.ToolStripButton()
        Me.ttBrowser = New System.Windows.Forms.ToolTip(Me.components)
        Me.tscMain.ContentPanel.SuspendLayout()
        Me.tscMain.TopToolStripPanel.SuspendLayout()
        Me.tscMain.SuspendLayout()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.scManage.Panel1.SuspendLayout()
        Me.scManage.Panel2.SuspendLayout()
        Me.scManage.SuspendLayout()
        Me.gbFoods.SuspendLayout()
        Me.gbFoodsComments.SuspendLayout()
        Me.scView.Panel1.SuspendLayout()
        Me.scView.Panel2.SuspendLayout()
        Me.scView.SuspendLayout()
        Me.gbCombinations.SuspendLayout()
        Me.tsBrowser.SuspendLayout()
        Me.SuspendLayout()
        '
        'tscMain
        '
        '
        'tscMain.ContentPanel
        '
        Me.tscMain.ContentPanel.Controls.Add(Me.scMain)
        Me.tscMain.ContentPanel.Size = New System.Drawing.Size(882, 478)
        Me.tscMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tscMain.Location = New System.Drawing.Point(0, 0)
        Me.tscMain.Name = "tscMain"
        Me.tscMain.Size = New System.Drawing.Size(882, 503)
        Me.tscMain.TabIndex = 0
        Me.tscMain.Text = "ToolStripContainer1"
        '
        'tscMain.TopToolStripPanel
        '
        Me.tscMain.TopToolStripPanel.Controls.Add(Me.tsBrowser)
        '
        'scMain
        '
        Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMain.Location = New System.Drawing.Point(0, 0)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.scManage)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.scView)
        Me.scMain.Size = New System.Drawing.Size(882, 478)
        Me.scMain.SplitterDistance = 288
        Me.scMain.TabIndex = 4
        '
        'scManage
        '
        Me.scManage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scManage.Location = New System.Drawing.Point(0, 0)
        Me.scManage.Name = "scManage"
        Me.scManage.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scManage.Panel1
        '
        Me.scManage.Panel1.Controls.Add(Me.gbFoods)
        '
        'scManage.Panel2
        '
        Me.scManage.Panel2.Controls.Add(Me.gbFoodsComments)
        Me.scManage.Size = New System.Drawing.Size(288, 478)
        Me.scManage.SplitterDistance = 404
        Me.scManage.TabIndex = 1
        '
        'gbFoods
        '
        Me.gbFoods.Controls.Add(Me.tvFoods)
        Me.gbFoods.Controls.Add(Me.tsFoods)
        Me.gbFoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFoods.Location = New System.Drawing.Point(0, 0)
        Me.gbFoods.Name = "gbFoods"
        Me.gbFoods.Size = New System.Drawing.Size(288, 404)
        Me.gbFoods.TabIndex = 2
        Me.gbFoods.TabStop = False
        Me.gbFoods.Text = "Foods:"
        '
        'tvFoods
        '
        Me.tvFoods.AllowDrop = True
        Me.tvFoods.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tvFoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvFoods.FullRowSelect = True
        Me.tvFoods.HideSelection = False
        Me.tvFoods.HotTracking = True
        Me.tvFoods.LabelEdit = True
        Me.tvFoods.Location = New System.Drawing.Point(3, 41)
        Me.tvFoods.Name = "tvFoods"
        Me.tvFoods.Size = New System.Drawing.Size(282, 360)
        Me.tvFoods.TabIndex = 0
        '
        'tsFoods
        '
        Me.tsFoods.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsFoods.Location = New System.Drawing.Point(3, 16)
        Me.tsFoods.Name = "tsFoods"
        Me.tsFoods.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.tsFoods.Size = New System.Drawing.Size(282, 25)
        Me.tsFoods.TabIndex = 0
        Me.tsFoods.Text = "Foods"
        '
        'gbFoodsComments
        '
        Me.gbFoodsComments.Controls.Add(Me.lvFoodsComments)
        Me.gbFoodsComments.Controls.Add(Me.tsFoodsComments)
        Me.gbFoodsComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFoodsComments.Location = New System.Drawing.Point(0, 0)
        Me.gbFoodsComments.Name = "gbFoodsComments"
        Me.gbFoodsComments.Size = New System.Drawing.Size(288, 70)
        Me.gbFoodsComments.TabIndex = 1
        Me.gbFoodsComments.TabStop = False
        Me.gbFoodsComments.Text = "Comments:"
        '
        'lvFoodsComments
        '
        Me.lvFoodsComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFoodsComments.FullRowSelect = True
        Me.lvFoodsComments.GridLines = True
        Me.lvFoodsComments.HideSelection = False
        Me.lvFoodsComments.Location = New System.Drawing.Point(29, 16)
        Me.lvFoodsComments.Name = "lvFoodsComments"
        Me.lvFoodsComments.Size = New System.Drawing.Size(256, 51)
        Me.lvFoodsComments.TabIndex = 1
        Me.lvFoodsComments.UseCompatibleStateImageBehavior = False
        Me.lvFoodsComments.View = System.Windows.Forms.View.Details
        '
        'tsFoodsComments
        '
        Me.tsFoodsComments.Dock = System.Windows.Forms.DockStyle.Left
        Me.tsFoodsComments.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsFoodsComments.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.tsFoodsComments.Location = New System.Drawing.Point(3, 16)
        Me.tsFoodsComments.Name = "tsFoodsComments"
        Me.tsFoodsComments.Size = New System.Drawing.Size(26, 51)
        Me.tsFoodsComments.TabIndex = 3
        Me.tsFoodsComments.Text = "Combinations"
        '
        'scView
        '
        Me.scView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scView.Location = New System.Drawing.Point(0, 0)
        Me.scView.Name = "scView"
        Me.scView.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scView.Panel1
        '
        Me.scView.Panel1.Controls.Add(Me.wbFoods)
        '
        'scView.Panel2
        '
        Me.scView.Panel2.Controls.Add(Me.gbCombinations)
        Me.scView.Size = New System.Drawing.Size(590, 478)
        Me.scView.SplitterDistance = 356
        Me.scView.TabIndex = 0
        '
        'wbFoods
        '
        Me.wbFoods.AllowWebBrowserDrop = False
        Me.wbFoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbFoods.Location = New System.Drawing.Point(0, 0)
        Me.wbFoods.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbFoods.Name = "wbFoods"
        Me.wbFoods.Size = New System.Drawing.Size(590, 356)
        Me.wbFoods.TabIndex = 3
        Me.wbFoods.Url = New System.Uri("http://2", System.UriKind.Absolute)
        '
        'gbCombinations
        '
        Me.gbCombinations.Controls.Add(Me.lvCombinations)
        Me.gbCombinations.Controls.Add(Me.tsCombinations)
        Me.gbCombinations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCombinations.Location = New System.Drawing.Point(0, 0)
        Me.gbCombinations.Name = "gbCombinations"
        Me.gbCombinations.Size = New System.Drawing.Size(590, 118)
        Me.gbCombinations.TabIndex = 2
        Me.gbCombinations.TabStop = False
        Me.gbCombinations.Text = "Known Combinations (0):"
        '
        'lvCombinations
        '
        Me.lvCombinations.AllowDrop = True
        Me.lvCombinations.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvCombinations_chName})
        Me.lvCombinations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvCombinations.FullRowSelect = True
        Me.lvCombinations.GridLines = True
        Me.lvCombinations.HideSelection = False
        Me.lvCombinations.Location = New System.Drawing.Point(3, 41)
        Me.lvCombinations.Name = "lvCombinations"
        Me.lvCombinations.Size = New System.Drawing.Size(584, 74)
        Me.lvCombinations.TabIndex = 4
        Me.lvCombinations.TileSize = New System.Drawing.Size(100, 30)
        Me.lvCombinations.UseCompatibleStateImageBehavior = False
        Me.lvCombinations.View = System.Windows.Forms.View.Tile
        '
        'lvCombinations_chName
        '
        Me.lvCombinations_chName.Text = "Name"
        '
        'tsCombinations
        '
        Me.tsCombinations.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsCombinations.Location = New System.Drawing.Point(3, 16)
        Me.tsCombinations.Name = "tsCombinations"
        Me.tsCombinations.Size = New System.Drawing.Size(584, 25)
        Me.tsCombinations.TabIndex = 0
        Me.tsCombinations.Text = "Combinations"
        '
        'tsBrowser
        '
        Me.tsBrowser.Dock = System.Windows.Forms.DockStyle.None
        Me.tsBrowser.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBrowser_tssbRefresh, Me.tsBrowser_tssSep1, Me.tsBrowser_tssbBack, Me.tsBrowser_tssbForward, Me.tsBrowser_tssSep2, Me.tsBrowser_tsbUp, Me.ToolStripSeparator1, Me.tsBrowser_tstLAddress, Me.tsBrowser_tstbName, Me.tsBrowser_tsbGoTo})
        Me.tsBrowser.Location = New System.Drawing.Point(3, 0)
        Me.tsBrowser.Name = "tsBrowser"
        Me.tsBrowser.Size = New System.Drawing.Size(602, 25)
        Me.tsBrowser.TabIndex = 6
        Me.tsBrowser.Text = "Navigation"
        '
        'tsBrowser_tssbRefresh
        '
        Me.tsBrowser_tssbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBrowser_tssbRefresh.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_BROWSE_REFRESH
        Me.tsBrowser_tssbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBrowser_tssbRefresh.Name = "tsBrowser_tssbRefresh"
        Me.tsBrowser_tssbRefresh.Size = New System.Drawing.Size(23, 22)
        Me.tsBrowser_tssbRefresh.Text = "Refresh"
        '
        'tsBrowser_tssSep1
        '
        Me.tsBrowser_tssSep1.Name = "tsBrowser_tssSep1"
        Me.tsBrowser_tssSep1.Size = New System.Drawing.Size(6, 25)
        '
        'tsBrowser_tssbBack
        '
        Me.tsBrowser_tssbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBrowser_tssbBack.Enabled = False
        Me.tsBrowser_tssbBack.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_BROWSE_BACK
        Me.tsBrowser_tssbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBrowser_tssbBack.Name = "tsBrowser_tssbBack"
        Me.tsBrowser_tssbBack.Size = New System.Drawing.Size(32, 22)
        Me.tsBrowser_tssbBack.Tag = "-1"
        Me.tsBrowser_tssbBack.Text = "Back"
        '
        'tsBrowser_tssbForward
        '
        Me.tsBrowser_tssbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBrowser_tssbForward.Enabled = False
        Me.tsBrowser_tssbForward.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_BROWSE_FORWARD
        Me.tsBrowser_tssbForward.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBrowser_tssbForward.Name = "tsBrowser_tssbForward"
        Me.tsBrowser_tssbForward.Size = New System.Drawing.Size(32, 22)
        Me.tsBrowser_tssbForward.Tag = "1"
        Me.tsBrowser_tssbForward.Text = "Forward"
        '
        'tsBrowser_tssSep2
        '
        Me.tsBrowser_tssSep2.Name = "tsBrowser_tssSep2"
        Me.tsBrowser_tssSep2.Size = New System.Drawing.Size(6, 25)
        '
        'tsBrowser_tsbUp
        '
        Me.tsBrowser_tsbUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBrowser_tsbUp.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_BROWSE_UP
        Me.tsBrowser_tsbUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBrowser_tsbUp.Name = "tsBrowser_tsbUp"
        Me.tsBrowser_tsbUp.Size = New System.Drawing.Size(23, 22)
        Me.tsBrowser_tsbUp.Text = "Up"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tsBrowser_tstLAddress
        '
        Me.tsBrowser_tstLAddress.Name = "tsBrowser_tstLAddress"
        Me.tsBrowser_tstLAddress.Size = New System.Drawing.Size(37, 22)
        Me.tsBrowser_tstLAddress.Text = "Food:"
        '
        'tsBrowser_tstbName
        '
        Me.tsBrowser_tstbName.AcceptsTab = True
        Me.tsBrowser_tstbName.Name = "tsBrowser_tstbName"
        Me.tsBrowser_tstbName.Size = New System.Drawing.Size(400, 25)
        '
        'tsBrowser_tsbGoTo
        '
        Me.tsBrowser_tsbGoTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBrowser_tsbGoTo.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_BROWSE_GO
        Me.tsBrowser_tsbGoTo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBrowser_tsbGoTo.Name = "tsBrowser_tsbGoTo"
        Me.tsBrowser_tsbGoTo.Size = New System.Drawing.Size(23, 22)
        Me.tsBrowser_tsbGoTo.Text = "Go"
        '
        'ttBrowser
        '
        Me.ttBrowser.AutomaticDelay = 0
        '
        'frmFoods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(882, 503)
        Me.Controls.Add(Me.tscMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmFoods"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Foods"
        Me.tscMain.ContentPanel.ResumeLayout(False)
        Me.tscMain.TopToolStripPanel.ResumeLayout(False)
        Me.tscMain.TopToolStripPanel.PerformLayout()
        Me.tscMain.ResumeLayout(False)
        Me.tscMain.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.ResumeLayout(False)
        Me.scManage.Panel1.ResumeLayout(False)
        Me.scManage.Panel2.ResumeLayout(False)
        Me.scManage.ResumeLayout(False)
        Me.gbFoods.ResumeLayout(False)
        Me.gbFoods.PerformLayout()
        Me.gbFoodsComments.ResumeLayout(False)
        Me.gbFoodsComments.PerformLayout()
        Me.scView.Panel1.ResumeLayout(False)
        Me.scView.Panel2.ResumeLayout(False)
        Me.scView.ResumeLayout(False)
        Me.gbCombinations.ResumeLayout(False)
        Me.gbCombinations.PerformLayout()
        Me.tsBrowser.ResumeLayout(False)
        Me.tsBrowser.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
	Friend WithEvents tscMain As System.Windows.Forms.ToolStripContainer
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
	Friend WithEvents gbFoods As System.Windows.Forms.GroupBox
	Friend WithEvents tvFoods As System.Windows.Forms.TreeView
	Friend WithEvents tsFoods As System.Windows.Forms.ToolStrip
	Friend WithEvents scView As System.Windows.Forms.SplitContainer
	Friend WithEvents wbFoods As System.Windows.Forms.WebBrowser
	Friend WithEvents gbCombinations As System.Windows.Forms.GroupBox
	Friend WithEvents lvCombinations As System.Windows.Forms.ListView
	Friend WithEvents lvCombinations_chName As System.Windows.Forms.ColumnHeader
	Friend WithEvents tsCombinations As System.Windows.Forms.ToolStrip
	Friend WithEvents ttBrowser As System.Windows.Forms.ToolTip
    Friend WithEvents scManage As System.Windows.Forms.SplitContainer
	Friend WithEvents gbFoodsComments As System.Windows.Forms.GroupBox
	Friend WithEvents lvFoodsComments As System.Windows.Forms.ListView
    Friend WithEvents tsFoodsComments As System.Windows.Forms.ToolStrip
    Friend WithEvents tsBrowser As System.Windows.Forms.ToolStrip
    Friend WithEvents tsBrowser_tssbRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBrowser_tssSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsBrowser_tssbBack As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tsBrowser_tssbForward As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tsBrowser_tssSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsBrowser_tstLAddress As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsBrowser_tstbName As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tsBrowser_tsbGoTo As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBrowser_tsbUp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
