<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpices
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
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpices))
		Me.scView = New System.Windows.Forms.SplitContainer
		Me.gbCombinations = New System.Windows.Forms.GroupBox
		Me.lvCombinations = New System.Windows.Forms.ListView
		Me.lvCombinations_chName = New System.Windows.Forms.ColumnHeader
		Me.tsCombinations = New System.Windows.Forms.ToolStrip
		Me.gbFoods = New System.Windows.Forms.GroupBox
		Me.lvFoods = New System.Windows.Forms.ListView
		Me.lvFoods_chName = New System.Windows.Forms.ColumnHeader
		Me.tsFoods = New System.Windows.Forms.ToolStrip
		Me.gbSpices = New System.Windows.Forms.GroupBox
		Me.tvSpices = New System.Windows.Forms.TreeView
		Me.lvSpices = New System.Windows.Forms.ListView
		Me.lvSpices_chName = New System.Windows.Forms.ColumnHeader
		Me.tsSpices = New System.Windows.Forms.ToolStrip
		Me.tsSpices_tsbIWC = New System.Windows.Forms.ToolStripButton
		Me.tsSpices_tssSep1 = New System.Windows.Forms.ToolStripSeparator
		Me.tsSpices_tsbNew = New System.Windows.Forms.ToolStripButton
		Me.tsSpices_tsbEdit = New System.Windows.Forms.ToolStripButton
		Me.tsSpices_tsbDelete = New System.Windows.Forms.ToolStripButton
		Me.tsSpices_tssSep2 = New System.Windows.Forms.ToolStripSeparator
		Me.tsSpices_tssbViews = New System.Windows.Forms.ToolStripSplitButton
		Me.tsSpices_tssbViews_tsmiTree = New System.Windows.Forms.ToolStripMenuItem
		Me.tsSpices_tssbViews_tsmiList = New System.Windows.Forms.ToolStripMenuItem
		Me.tsBrowser = New System.Windows.Forms.ToolStrip
		Me.tsBrowser_tssbRefresh = New System.Windows.Forms.ToolStripButton
		Me.tsBrowser_tssSep1 = New System.Windows.Forms.ToolStripSeparator
		Me.tsBrowser_tssbBack = New System.Windows.Forms.ToolStripSplitButton
		Me.tsBrowser_tssbForward = New System.Windows.Forms.ToolStripSplitButton
		Me.tsBrowser_tssSep2 = New System.Windows.Forms.ToolStripSeparator
		Me.tsBrowser_tstLAddress = New System.Windows.Forms.ToolStripLabel
		Me.tsBrowser_tstbName = New System.Windows.Forms.ToolStripTextBox
		Me.tsBrowser_tsbGoTo = New System.Windows.Forms.ToolStripButton
		Me.scMain = New System.Windows.Forms.SplitContainer
		Me.scManage = New System.Windows.Forms.SplitContainer
		Me.gbSpicesComments = New System.Windows.Forms.GroupBox
		Me.lvSpicesComments = New System.Windows.Forms.ListView
		Me.tsSpicesComments = New System.Windows.Forms.ToolStrip
		Me.tscMain = New System.Windows.Forms.ToolStripContainer
		Me.ttAddCombination = New System.Windows.Forms.ToolTip(Me.components)
		Me.ttBrowser = New System.Windows.Forms.ToolTip(Me.components)
		Me.scView.Panel1.SuspendLayout()
		Me.scView.Panel2.SuspendLayout()
		Me.scView.SuspendLayout()
		Me.gbCombinations.SuspendLayout()
		Me.gbFoods.SuspendLayout()
		Me.gbSpices.SuspendLayout()
		Me.tsSpices.SuspendLayout()
		Me.tsBrowser.SuspendLayout()
		Me.scMain.Panel1.SuspendLayout()
		Me.scMain.Panel2.SuspendLayout()
		Me.scMain.SuspendLayout()
		Me.scManage.Panel1.SuspendLayout()
		Me.scManage.Panel2.SuspendLayout()
		Me.scManage.SuspendLayout()
		Me.gbSpicesComments.SuspendLayout()
		Me.tscMain.ContentPanel.SuspendLayout()
		Me.tscMain.TopToolStripPanel.SuspendLayout()
		Me.tscMain.SuspendLayout()
		Me.SuspendLayout()
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
		Me.scView.Panel1.Controls.Add(Me.gbCombinations)
		'
		'scView.Panel2
		'
		Me.scView.Panel2.Controls.Add(Me.gbFoods)
		Me.scView.Size = New System.Drawing.Size(383, 453)
		Me.scView.SplitterDistance = 234
		Me.scView.TabIndex = 10
		'
		'gbCombinations
		'
		Me.gbCombinations.Controls.Add(Me.lvCombinations)
		Me.gbCombinations.Controls.Add(Me.tsCombinations)
		Me.gbCombinations.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbCombinations.Location = New System.Drawing.Point(0, 0)
		Me.gbCombinations.Name = "gbCombinations"
		Me.gbCombinations.Size = New System.Drawing.Size(383, 234)
		Me.gbCombinations.TabIndex = 14
		Me.gbCombinations.TabStop = False
		Me.gbCombinations.Text = "Known Combinations:"
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
		Me.lvCombinations.Size = New System.Drawing.Size(377, 190)
		Me.lvCombinations.TabIndex = 3
		Me.lvCombinations.UseCompatibleStateImageBehavior = False
		Me.lvCombinations.View = System.Windows.Forms.View.Details
		'
		'lvCombinations_chName
		'
		Me.lvCombinations_chName.Text = "Name"
		Me.lvCombinations_chName.Width = 334
		'
		'tsCombinations
		'
		Me.tsCombinations.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.tsCombinations.Location = New System.Drawing.Point(3, 16)
		Me.tsCombinations.Name = "tsCombinations"
		Me.tsCombinations.Size = New System.Drawing.Size(377, 25)
		Me.tsCombinations.TabIndex = 7
		Me.tsCombinations.Text = "Combinations"
		'
		'gbFoods
		'
		Me.gbFoods.Controls.Add(Me.lvFoods)
		Me.gbFoods.Controls.Add(Me.tsFoods)
		Me.gbFoods.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbFoods.Location = New System.Drawing.Point(0, 0)
		Me.gbFoods.Name = "gbFoods"
		Me.gbFoods.Size = New System.Drawing.Size(383, 215)
		Me.gbFoods.TabIndex = 15
		Me.gbFoods.TabStop = False
		Me.gbFoods.Text = "Foods:"
		'
		'lvFoods
		'
		Me.lvFoods.AllowDrop = True
		Me.lvFoods.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvFoods_chName})
		Me.lvFoods.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lvFoods.FullRowSelect = True
		Me.lvFoods.GridLines = True
		Me.lvFoods.HideSelection = False
		Me.lvFoods.Location = New System.Drawing.Point(3, 41)
		Me.lvFoods.Name = "lvFoods"
		Me.lvFoods.Size = New System.Drawing.Size(377, 171)
		Me.lvFoods.TabIndex = 4
		Me.lvFoods.UseCompatibleStateImageBehavior = False
		Me.lvFoods.View = System.Windows.Forms.View.Details
		'
		'lvFoods_chName
		'
		Me.lvFoods_chName.Text = "Name"
		Me.lvFoods_chName.Width = 334
		'
		'tsFoods
		'
		Me.tsFoods.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.tsFoods.Location = New System.Drawing.Point(3, 16)
		Me.tsFoods.Name = "tsFoods"
		Me.tsFoods.Size = New System.Drawing.Size(377, 25)
		Me.tsFoods.TabIndex = 8
		Me.tsFoods.Text = "Combinations"
		'
		'gbSpices
		'
		Me.gbSpices.Controls.Add(Me.tvSpices)
		Me.gbSpices.Controls.Add(Me.lvSpices)
		Me.gbSpices.Controls.Add(Me.tsSpices)
		Me.gbSpices.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbSpices.Location = New System.Drawing.Point(0, 0)
		Me.gbSpices.Name = "gbSpices"
		Me.gbSpices.Size = New System.Drawing.Size(258, 384)
		Me.gbSpices.TabIndex = 12
		Me.gbSpices.TabStop = False
		Me.gbSpices.Text = "Spice List"
		'
		'tvSpices
		'
		Me.tvSpices.AllowDrop = True
		Me.tvSpices.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tvSpices.FullRowSelect = True
		Me.tvSpices.HideSelection = False
		Me.tvSpices.LabelEdit = True
		Me.tvSpices.Location = New System.Drawing.Point(3, 41)
		Me.tvSpices.Name = "tvSpices"
		Me.tvSpices.ShowNodeToolTips = True
		Me.tvSpices.Size = New System.Drawing.Size(252, 340)
		Me.tvSpices.TabIndex = 0
		'
		'lvSpices
		'
		Me.lvSpices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvSpices_chName})
		Me.lvSpices.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lvSpices.FullRowSelect = True
		Me.lvSpices.GridLines = True
		Me.lvSpices.HideSelection = False
		Me.lvSpices.Location = New System.Drawing.Point(3, 41)
		Me.lvSpices.Name = "lvSpices"
		Me.lvSpices.Size = New System.Drawing.Size(252, 340)
		Me.lvSpices.TabIndex = 1
		Me.lvSpices.UseCompatibleStateImageBehavior = False
		Me.lvSpices.View = System.Windows.Forms.View.Details
		Me.lvSpices.Visible = False
		'
		'lvSpices_chName
		'
		Me.lvSpices_chName.Text = "Name"
		'
		'tsSpices
		'
		Me.tsSpices.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsSpices_tsbIWC, Me.tsSpices_tssSep1, Me.tsSpices_tsbNew, Me.tsSpices_tsbEdit, Me.tsSpices_tsbDelete, Me.tsSpices_tssSep2, Me.tsSpices_tssbViews})
		Me.tsSpices.Location = New System.Drawing.Point(3, 16)
		Me.tsSpices.Name = "tsSpices"
		Me.tsSpices.Size = New System.Drawing.Size(252, 25)
		Me.tsSpices.TabIndex = 5
		Me.tsSpices.Text = "Items"
		'
		'tsSpices_tsbIWC
		'
		Me.tsSpices_tsbIWC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsSpices_tsbIWC.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsSpices_tsbIWC.Name = "tsSpices_tsbIWC"
		Me.tsSpices_tsbIWC.Size = New System.Drawing.Size(52, 22)
		Me.tsSpices_tsbIWC.Text = "Cook It!"
		'
		'tsSpices_tssSep1
		'
		Me.tsSpices_tssSep1.Name = "tsSpices_tssSep1"
		Me.tsSpices_tssSep1.Size = New System.Drawing.Size(6, 25)
		'
		'tsSpices_tsbNew
		'
		Me.tsSpices_tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsSpices_tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsSpices_tsbNew.Name = "tsSpices_tsbNew"
		Me.tsSpices_tsbNew.Size = New System.Drawing.Size(35, 22)
		Me.tsSpices_tsbNew.Text = "New"
		'
		'tsSpices_tsbEdit
		'
		Me.tsSpices_tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsSpices_tsbEdit.Enabled = False
		Me.tsSpices_tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsSpices_tsbEdit.Name = "tsSpices_tsbEdit"
		Me.tsSpices_tsbEdit.Size = New System.Drawing.Size(31, 22)
		Me.tsSpices_tsbEdit.Text = "Edit"
		'
		'tsSpices_tsbDelete
		'
		Me.tsSpices_tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsSpices_tsbDelete.Enabled = False
		Me.tsSpices_tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsSpices_tsbDelete.Name = "tsSpices_tsbDelete"
		Me.tsSpices_tsbDelete.Size = New System.Drawing.Size(44, 22)
		Me.tsSpices_tsbDelete.Text = "Delete"
		'
		'tsSpices_tssSep2
		'
		Me.tsSpices_tssSep2.Name = "tsSpices_tssSep2"
		Me.tsSpices_tssSep2.Size = New System.Drawing.Size(6, 25)
		'
		'tsSpices_tssbViews
		'
		Me.tsSpices_tssbViews.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsSpices_tssbViews.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsSpices_tssbViews_tsmiTree, Me.tsSpices_tssbViews_tsmiList})
		Me.tsSpices_tssbViews.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsSpices_tssbViews.Name = "tsSpices_tssbViews"
		Me.tsSpices_tssbViews.Size = New System.Drawing.Size(46, 22)
		Me.tsSpices_tssbViews.Text = "Tree"
		'
		'tsSpices_tssbViews_tsmiTree
		'
		Me.tsSpices_tssbViews_tsmiTree.Checked = True
		Me.tsSpices_tssbViews_tsmiTree.CheckState = System.Windows.Forms.CheckState.Checked
		Me.tsSpices_tssbViews_tsmiTree.Name = "tsSpices_tssbViews_tsmiTree"
		Me.tsSpices_tssbViews_tsmiTree.Size = New System.Drawing.Size(97, 22)
		Me.tsSpices_tssbViews_tsmiTree.Text = "Tree"
		'
		'tsSpices_tssbViews_tsmiList
		'
		Me.tsSpices_tssbViews_tsmiList.Name = "tsSpices_tssbViews_tsmiList"
		Me.tsSpices_tssbViews_tsmiList.Size = New System.Drawing.Size(97, 22)
		Me.tsSpices_tssbViews_tsmiList.Text = "List"
		'
		'tsBrowser
		'
		Me.tsBrowser.Dock = System.Windows.Forms.DockStyle.None
		Me.tsBrowser.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBrowser_tssbRefresh, Me.tsBrowser_tssSep1, Me.tsBrowser_tssbBack, Me.tsBrowser_tssbForward, Me.tsBrowser_tssSep2, Me.tsBrowser_tstLAddress, Me.tsBrowser_tstbName, Me.tsBrowser_tsbGoTo})
		Me.tsBrowser.Location = New System.Drawing.Point(3, 0)
		Me.tsBrowser.Name = "tsBrowser"
		Me.tsBrowser.Size = New System.Drawing.Size(574, 25)
		Me.tsBrowser.TabIndex = 2
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
		'tsBrowser_tstLAddress
		'
		Me.tsBrowser_tstLAddress.Name = "tsBrowser_tstLAddress"
		Me.tsBrowser_tstLAddress.Size = New System.Drawing.Size(38, 22)
		Me.tsBrowser_tstLAddress.Text = "Spice:"
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
		Me.scMain.Size = New System.Drawing.Size(645, 453)
		Me.scMain.SplitterDistance = 258
		Me.scMain.TabIndex = 11
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
		Me.scManage.Panel1.Controls.Add(Me.gbSpices)
		'
		'scManage.Panel2
		'
		Me.scManage.Panel2.Controls.Add(Me.gbSpicesComments)
		Me.scManage.Size = New System.Drawing.Size(258, 453)
		Me.scManage.SplitterDistance = 384
		Me.scManage.TabIndex = 9
		'
		'gbSpicesComments
		'
		Me.gbSpicesComments.Controls.Add(Me.lvSpicesComments)
		Me.gbSpicesComments.Controls.Add(Me.tsSpicesComments)
		Me.gbSpicesComments.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbSpicesComments.Location = New System.Drawing.Point(0, 0)
		Me.gbSpicesComments.Name = "gbSpicesComments"
		Me.gbSpicesComments.Size = New System.Drawing.Size(258, 65)
		Me.gbSpicesComments.TabIndex = 13
		Me.gbSpicesComments.TabStop = False
		Me.gbSpicesComments.Text = "Comments:"
		'
		'lvSpicesComments
		'
		Me.lvSpicesComments.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lvSpicesComments.FullRowSelect = True
		Me.lvSpicesComments.GridLines = True
		Me.lvSpicesComments.HideSelection = False
		Me.lvSpicesComments.Location = New System.Drawing.Point(29, 16)
		Me.lvSpicesComments.Name = "lvSpicesComments"
		Me.lvSpicesComments.Size = New System.Drawing.Size(226, 46)
		Me.lvSpicesComments.TabIndex = 1
		Me.lvSpicesComments.UseCompatibleStateImageBehavior = False
		Me.lvSpicesComments.View = System.Windows.Forms.View.Details
		'
		'tsSpicesComments
		'
		Me.tsSpicesComments.Dock = System.Windows.Forms.DockStyle.Left
		Me.tsSpicesComments.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.tsSpicesComments.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
		Me.tsSpicesComments.Location = New System.Drawing.Point(3, 16)
		Me.tsSpicesComments.Name = "tsSpicesComments"
		Me.tsSpicesComments.Size = New System.Drawing.Size(26, 46)
		Me.tsSpicesComments.TabIndex = 6
		Me.tsSpicesComments.Text = "Combinations"
		'
		'tscMain
		'
		'
		'tscMain.ContentPanel
		'
		Me.tscMain.ContentPanel.Controls.Add(Me.scMain)
		Me.tscMain.ContentPanel.Size = New System.Drawing.Size(645, 453)
		Me.tscMain.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tscMain.Location = New System.Drawing.Point(0, 0)
		Me.tscMain.Name = "tscMain"
		Me.tscMain.Size = New System.Drawing.Size(645, 478)
		Me.tscMain.TabIndex = 4
		Me.tscMain.Text = "ToolStripContainer1"
		'
		'tscMain.TopToolStripPanel
		'
		Me.tscMain.TopToolStripPanel.Controls.Add(Me.tsBrowser)
		'
		'ttAddCombination
		'
		Me.ttAddCombination.IsBalloon = True
		Me.ttAddCombination.ToolTipTitle = "To add items:"
		'
		'ttBrowser
		'
		Me.ttBrowser.AutomaticDelay = 0
		'
		'frmSpices
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(645, 478)
		Me.Controls.Add(Me.tscMain)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.KeyPreview = True
		Me.Name = "frmSpices"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "Spice List"
		Me.scView.Panel1.ResumeLayout(False)
		Me.scView.Panel2.ResumeLayout(False)
		Me.scView.ResumeLayout(False)
		Me.gbCombinations.ResumeLayout(False)
		Me.gbCombinations.PerformLayout()
		Me.gbFoods.ResumeLayout(False)
		Me.gbFoods.PerformLayout()
		Me.gbSpices.ResumeLayout(False)
		Me.gbSpices.PerformLayout()
		Me.tsSpices.ResumeLayout(False)
		Me.tsSpices.PerformLayout()
		Me.tsBrowser.ResumeLayout(False)
		Me.tsBrowser.PerformLayout()
		Me.scMain.Panel1.ResumeLayout(False)
		Me.scMain.Panel2.ResumeLayout(False)
		Me.scMain.ResumeLayout(False)
		Me.scManage.Panel1.ResumeLayout(False)
		Me.scManage.Panel2.ResumeLayout(False)
		Me.scManage.ResumeLayout(False)
		Me.gbSpicesComments.ResumeLayout(False)
		Me.gbSpicesComments.PerformLayout()
		Me.tscMain.ContentPanel.ResumeLayout(False)
		Me.tscMain.TopToolStripPanel.ResumeLayout(False)
		Me.tscMain.TopToolStripPanel.PerformLayout()
		Me.tscMain.ResumeLayout(False)
		Me.tscMain.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents scView As System.Windows.Forms.SplitContainer
	Friend WithEvents gbSpices As System.Windows.Forms.GroupBox
	Friend WithEvents tsSpices As System.Windows.Forms.ToolStrip
	Friend WithEvents tsSpices_tsbNew As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsSpices_tsbEdit As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsSpices_tsbDelete As System.Windows.Forms.ToolStripButton
	Friend WithEvents lvSpices As System.Windows.Forms.ListView
	Friend WithEvents tsBrowser As System.Windows.Forms.ToolStrip
	Friend WithEvents tsBrowser_tssbBack As System.Windows.Forms.ToolStripSplitButton
	Friend WithEvents tsBrowser_tssbForward As System.Windows.Forms.ToolStripSplitButton
	Friend WithEvents tsBrowser_tstbName As System.Windows.Forms.ToolStripTextBox
	Friend WithEvents tsBrowser_tsbGoTo As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsBrowser_tssSep1 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents scMain As System.Windows.Forms.SplitContainer
	Friend WithEvents tscMain As System.Windows.Forms.ToolStripContainer
	Friend WithEvents ttAddCombination As System.Windows.Forms.ToolTip
	Friend WithEvents gbFoods As System.Windows.Forms.GroupBox
	Friend WithEvents lvFoods As System.Windows.Forms.ListView
	Friend WithEvents lvFoods_chName As System.Windows.Forms.ColumnHeader
	Friend WithEvents gbCombinations As System.Windows.Forms.GroupBox
	Friend WithEvents lvCombinations As System.Windows.Forms.ListView
	Friend WithEvents lvCombinations_chName As System.Windows.Forms.ColumnHeader
	Friend WithEvents tsCombinations As System.Windows.Forms.ToolStrip
	Friend WithEvents tsFoods As System.Windows.Forms.ToolStrip
	Friend WithEvents ttBrowser As System.Windows.Forms.ToolTip
	Friend WithEvents tvSpices As System.Windows.Forms.TreeView
	Friend WithEvents tsSpices_tssSep1 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents tsSpices_tssbViews As System.Windows.Forms.ToolStripSplitButton
	Friend WithEvents tsSpices_tssbViews_tsmiTree As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents tsSpices_tssbViews_tsmiList As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents lvSpices_chName As System.Windows.Forms.ColumnHeader
	Friend WithEvents tsSpices_tsbIWC As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsSpices_tssSep2 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents tsBrowser_tssbRefresh As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsBrowser_tssSep2 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents tsBrowser_tstLAddress As System.Windows.Forms.ToolStripLabel
	Friend WithEvents scManage As System.Windows.Forms.SplitContainer
	Friend WithEvents gbSpicesComments As System.Windows.Forms.GroupBox
	Friend WithEvents lvSpicesComments As System.Windows.Forms.ListView
	Friend WithEvents tsSpicesComments As System.Windows.Forms.ToolStrip
End Class
