<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIWannaCook
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("With all criteria", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Not with 1", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("With all criteria", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Not with 1", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIWannaCook))
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.gbCriteria = New System.Windows.Forms.GroupBox()
        Me.scManage = New System.Windows.Forms.SplitContainer()
        Me.gbFoods = New System.Windows.Forms.GroupBox()
        Me.lvFoods = New System.Windows.Forms.ListView()
        Me.lvFoods_chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tsFoods = New System.Windows.Forms.ToolStrip()
        Me.gbSpices = New System.Windows.Forms.GroupBox()
        Me.lvSpices = New System.Windows.Forms.ListView()
        Me.lvCombinations_chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tsSpices = New System.Windows.Forms.ToolStrip()
        Me.gbResults = New System.Windows.Forms.GroupBox()
        Me.tabResults = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.gbMainResults = New System.Windows.Forms.GroupBox()
        Me.lvResults = New System.Windows.Forms.ListView()
        Me.tsResults = New System.Windows.Forms.ToolStrip()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.gbSepResults = New System.Windows.Forms.GroupBox()
        Me.lvSepResults = New System.Windows.Forms.ListView()
        Me.tsSepResults = New System.Windows.Forms.ToolStrip()
        Me.tsShelf = New System.Windows.Forms.ToolStrip()
        Me.tsShelf_IFPC = New System.Windows.Forms.ToolStripButton()
        Me.tsShelf_Sep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsShelf_SOMS = New System.Windows.Forms.ToolStripButton()
        Me.tsShelf_Sep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsShelf_MS = New System.Windows.Forms.ToolStripButton()
        Me.tsShelf_ICPC = New System.Windows.Forms.ToolStripButton()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.gbCriteria.SuspendLayout()
        Me.scManage.Panel1.SuspendLayout()
        Me.scManage.Panel2.SuspendLayout()
        Me.scManage.SuspendLayout()
        Me.gbFoods.SuspendLayout()
        Me.gbSpices.SuspendLayout()
        Me.gbResults.SuspendLayout()
        Me.tabResults.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.gbMainResults.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.gbSepResults.SuspendLayout()
        Me.tsShelf.SuspendLayout()
        Me.SuspendLayout()
        '
        'scMain
        '
        Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMain.Location = New System.Drawing.Point(0, 25)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.gbCriteria)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.gbResults)
        Me.scMain.Size = New System.Drawing.Size(641, 437)
        Me.scMain.SplitterDistance = 277
        Me.scMain.TabIndex = 0
        '
        'gbCriteria
        '
        Me.gbCriteria.Controls.Add(Me.scManage)
        Me.gbCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCriteria.Location = New System.Drawing.Point(0, 0)
        Me.gbCriteria.Name = "gbCriteria"
        Me.gbCriteria.Size = New System.Drawing.Size(277, 437)
        Me.gbCriteria.TabIndex = 0
        Me.gbCriteria.TabStop = False
        Me.gbCriteria.Text = "Search criteria:"
        '
        'scManage
        '
        Me.scManage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scManage.Location = New System.Drawing.Point(3, 16)
        Me.scManage.Name = "scManage"
        Me.scManage.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scManage.Panel1
        '
        Me.scManage.Panel1.Controls.Add(Me.gbFoods)
        '
        'scManage.Panel2
        '
        Me.scManage.Panel2.Controls.Add(Me.gbSpices)
        Me.scManage.Size = New System.Drawing.Size(271, 418)
        Me.scManage.SplitterDistance = 206
        Me.scManage.TabIndex = 2
        '
        'gbFoods
        '
        Me.gbFoods.Controls.Add(Me.lvFoods)
        Me.gbFoods.Controls.Add(Me.tsFoods)
        Me.gbFoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFoods.Location = New System.Drawing.Point(0, 0)
        Me.gbFoods.Name = "gbFoods"
        Me.gbFoods.Size = New System.Drawing.Size(271, 206)
        Me.gbFoods.TabIndex = 3
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
        Me.lvFoods.Size = New System.Drawing.Size(265, 162)
        Me.lvFoods.TabIndex = 1
        Me.lvFoods.UseCompatibleStateImageBehavior = False
        Me.lvFoods.View = System.Windows.Forms.View.Details
        '
        'lvFoods_chName
        '
        Me.lvFoods_chName.Text = "Name"
        Me.lvFoods_chName.Width = 173
        '
        'tsFoods
        '
        Me.tsFoods.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsFoods.Location = New System.Drawing.Point(3, 16)
        Me.tsFoods.Name = "tsFoods"
        Me.tsFoods.Size = New System.Drawing.Size(265, 25)
        Me.tsFoods.TabIndex = 2
        Me.tsFoods.Text = "Foods"
        '
        'gbSpices
        '
        Me.gbSpices.Controls.Add(Me.lvSpices)
        Me.gbSpices.Controls.Add(Me.tsSpices)
        Me.gbSpices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSpices.Location = New System.Drawing.Point(0, 0)
        Me.gbSpices.Name = "gbSpices"
        Me.gbSpices.Size = New System.Drawing.Size(271, 208)
        Me.gbSpices.TabIndex = 2
        Me.gbSpices.TabStop = False
        Me.gbSpices.Text = "Spices:"
        '
        'lvSpices
        '
        Me.lvSpices.AllowDrop = True
        Me.lvSpices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvCombinations_chName})
        Me.lvSpices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSpices.FullRowSelect = True
        Me.lvSpices.GridLines = True
        Me.lvSpices.HideSelection = False
        Me.lvSpices.Location = New System.Drawing.Point(3, 41)
        Me.lvSpices.Name = "lvSpices"
        Me.lvSpices.Size = New System.Drawing.Size(265, 164)
        Me.lvSpices.TabIndex = 2
        Me.lvSpices.UseCompatibleStateImageBehavior = False
        Me.lvSpices.View = System.Windows.Forms.View.Details
        '
        'lvCombinations_chName
        '
        Me.lvCombinations_chName.Text = "Name"
        Me.lvCombinations_chName.Width = 181
        '
        'tsSpices
        '
        Me.tsSpices.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsSpices.Location = New System.Drawing.Point(3, 16)
        Me.tsSpices.Name = "tsSpices"
        Me.tsSpices.Size = New System.Drawing.Size(265, 25)
        Me.tsSpices.TabIndex = 0
        Me.tsSpices.Text = "Combinations"
        '
        'gbResults
        '
        Me.gbResults.Controls.Add(Me.tabResults)
        Me.gbResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbResults.Location = New System.Drawing.Point(0, 0)
        Me.gbResults.Name = "gbResults"
        Me.gbResults.Size = New System.Drawing.Size(360, 437)
        Me.gbResults.TabIndex = 3
        Me.gbResults.TabStop = False
        Me.gbResults.Text = "Results:"
        '
        'tabResults
        '
        Me.tabResults.Controls.Add(Me.TabPage1)
        Me.tabResults.Controls.Add(Me.TabPage2)
        Me.tabResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabResults.Location = New System.Drawing.Point(3, 16)
        Me.tabResults.Name = "tabResults"
        Me.tabResults.SelectedIndex = 0
        Me.tabResults.Size = New System.Drawing.Size(354, 418)
        Me.tabResults.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.gbMainResults)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(346, 392)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Perfect matches"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'gbMainResults
        '
        Me.gbMainResults.Controls.Add(Me.lvResults)
        Me.gbMainResults.Controls.Add(Me.tsResults)
        Me.gbMainResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMainResults.Location = New System.Drawing.Point(3, 3)
        Me.gbMainResults.Name = "gbMainResults"
        Me.gbMainResults.Size = New System.Drawing.Size(340, 386)
        Me.gbMainResults.TabIndex = 7
        Me.gbMainResults.TabStop = False
        Me.gbMainResults.Text = "Perfect matches:"
        '
        'lvResults
        '
        Me.lvResults.AllowDrop = True
        Me.lvResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvResults.FullRowSelect = True
        Me.lvResults.GridLines = True
        ListViewGroup1.Header = "With all criteria"
        ListViewGroup1.Name = "lvResults_lvgRank0"
        ListViewGroup2.Header = "Not with 1"
        ListViewGroup2.Name = "lvResults_lvgRank1"
        Me.lvResults.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvResults.HideSelection = False
        Me.lvResults.Location = New System.Drawing.Point(3, 41)
        Me.lvResults.Name = "lvResults"
        Me.lvResults.Size = New System.Drawing.Size(334, 342)
        Me.lvResults.TabIndex = 6
        Me.lvResults.TileSize = New System.Drawing.Size(100, 30)
        Me.lvResults.UseCompatibleStateImageBehavior = False
        Me.lvResults.View = System.Windows.Forms.View.Tile
        '
        'tsResults
        '
        Me.tsResults.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsResults.Location = New System.Drawing.Point(3, 16)
        Me.tsResults.Name = "tsResults"
        Me.tsResults.Size = New System.Drawing.Size(334, 25)
        Me.tsResults.TabIndex = 7
        Me.tsResults.Text = "Combinations"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.gbSepResults)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(346, 392)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Also"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'gbSepResults
        '
        Me.gbSepResults.Controls.Add(Me.lvSepResults)
        Me.gbSepResults.Controls.Add(Me.tsSepResults)
        Me.gbSepResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSepResults.Location = New System.Drawing.Point(3, 3)
        Me.gbSepResults.Name = "gbSepResults"
        Me.gbSepResults.Size = New System.Drawing.Size(340, 386)
        Me.gbSepResults.TabIndex = 8
        Me.gbSepResults.TabStop = False
        Me.gbSepResults.Text = "Also:"
        '
        'lvSepResults
        '
        Me.lvSepResults.AllowDrop = True
        Me.lvSepResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSepResults.FullRowSelect = True
        Me.lvSepResults.GridLines = True
        ListViewGroup3.Header = "With all criteria"
        ListViewGroup3.Name = "lvResults_lvgRank0"
        ListViewGroup4.Header = "Not with 1"
        ListViewGroup4.Name = "lvResults_lvgRank1"
        Me.lvSepResults.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.lvSepResults.HideSelection = False
        Me.lvSepResults.Location = New System.Drawing.Point(3, 41)
        Me.lvSepResults.Name = "lvSepResults"
        Me.lvSepResults.Size = New System.Drawing.Size(334, 342)
        Me.lvSepResults.TabIndex = 8
        Me.lvSepResults.TileSize = New System.Drawing.Size(80, 15)
        Me.lvSepResults.UseCompatibleStateImageBehavior = False
        Me.lvSepResults.View = System.Windows.Forms.View.Tile
        '
        'tsSepResults
        '
        Me.tsSepResults.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsSepResults.Location = New System.Drawing.Point(3, 16)
        Me.tsSepResults.Name = "tsSepResults"
        Me.tsSepResults.Size = New System.Drawing.Size(334, 25)
        Me.tsSepResults.TabIndex = 9
        Me.tsSepResults.Text = "Combinations"
        '
        'tsShelf
        '
        Me.tsShelf.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsShelf.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsShelf_IFPC, Me.tsShelf_ICPC, Me.tsShelf_Sep1, Me.tsShelf_SOMS, Me.tsShelf_Sep2, Me.tsShelf_MS})
        Me.tsShelf.Location = New System.Drawing.Point(0, 0)
        Me.tsShelf.Name = "tsShelf"
        Me.tsShelf.Size = New System.Drawing.Size(641, 25)
        Me.tsShelf.TabIndex = 5
        Me.tsShelf.Text = "Shelf"
        '
        'tsShelf_IFPC
        '
        Me.tsShelf_IFPC.Checked = True
        Me.tsShelf_IFPC.CheckOnClick = True
        Me.tsShelf_IFPC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsShelf_IFPC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsShelf_IFPC.Image = CType(resources.GetObject("tsShelf_IFPC.Image"), System.Drawing.Image)
        Me.tsShelf_IFPC.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsShelf_IFPC.Name = "tsShelf_IFPC"
        Me.tsShelf_IFPC.Size = New System.Drawing.Size(166, 22)
        Me.tsShelf_IFPC.Text = "Include Food's Parent Criteria"
        '
        'tsShelf_Sep1
        '
        Me.tsShelf_Sep1.Name = "tsShelf_Sep1"
        Me.tsShelf_Sep1.Size = New System.Drawing.Size(6, 25)
        '
        'tsShelf_SOMS
        '
        Me.tsShelf_SOMS.CheckOnClick = True
        Me.tsShelf_SOMS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsShelf_SOMS.Image = CType(resources.GetObject("tsShelf_SOMS.Image"), System.Drawing.Image)
        Me.tsShelf_SOMS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsShelf_SOMS.Name = "tsShelf_SOMS"
        Me.tsShelf_SOMS.Size = New System.Drawing.Size(123, 22)
        Me.tsShelf_SOMS.Text = "Show Only 'My Shelf'"
        '
        'tsShelf_Sep2
        '
        Me.tsShelf_Sep2.Name = "tsShelf_Sep2"
        Me.tsShelf_Sep2.Size = New System.Drawing.Size(6, 25)
        '
        'tsShelf_MS
        '
        Me.tsShelf_MS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsShelf_MS.Image = CType(resources.GetObject("tsShelf_MS.Image"), System.Drawing.Image)
        Me.tsShelf_MS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsShelf_MS.Name = "tsShelf_MS"
        Me.tsShelf_MS.Size = New System.Drawing.Size(95, 22)
        Me.tsShelf_MS.Text = "Open 'My Shelf'"
        '
        'tsShelf_ICPC
        '
        Me.tsShelf_ICPC.Checked = True
        Me.tsShelf_ICPC.CheckOnClick = True
        Me.tsShelf_ICPC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsShelf_ICPC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsShelf_ICPC.Image = CType(resources.GetObject("tsShelf_ICPC.Image"), System.Drawing.Image)
        Me.tsShelf_ICPC.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsShelf_ICPC.Name = "tsShelf_ICPC"
        Me.tsShelf_ICPC.Size = New System.Drawing.Size(209, 22)
        Me.tsShelf_ICPC.Text = "Include Combinations' Parent Criteria"
        '
        'frmIWannaCook
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 462)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.tsShelf)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmIWannaCook"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "I Wanna Cook!"
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.ResumeLayout(False)
        Me.gbCriteria.ResumeLayout(False)
        Me.scManage.Panel1.ResumeLayout(False)
        Me.scManage.Panel2.ResumeLayout(False)
        Me.scManage.ResumeLayout(False)
        Me.gbFoods.ResumeLayout(False)
        Me.gbFoods.PerformLayout()
        Me.gbSpices.ResumeLayout(False)
        Me.gbSpices.PerformLayout()
        Me.gbResults.ResumeLayout(False)
        Me.tabResults.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.gbMainResults.ResumeLayout(False)
        Me.gbMainResults.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.gbSepResults.ResumeLayout(False)
        Me.gbSepResults.PerformLayout()
        Me.tsShelf.ResumeLayout(False)
        Me.tsShelf.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents gbResults As System.Windows.Forms.GroupBox
    Friend WithEvents tsShelf As System.Windows.Forms.ToolStrip
    Friend WithEvents tsShelf_SOMS As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsShelf_MS As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsShelf_Sep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsShelf_IFPC As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsShelf_Sep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tabResults As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents gbMainResults As System.Windows.Forms.GroupBox
    Friend WithEvents lvResults As System.Windows.Forms.ListView
    Friend WithEvents tsResults As System.Windows.Forms.ToolStrip
    Friend WithEvents gbSepResults As System.Windows.Forms.GroupBox
    Friend WithEvents lvSepResults As System.Windows.Forms.ListView
    Friend WithEvents tsSepResults As System.Windows.Forms.ToolStrip
    Friend WithEvents gbCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents scManage As System.Windows.Forms.SplitContainer
    Friend WithEvents gbFoods As System.Windows.Forms.GroupBox
    Friend WithEvents lvFoods As System.Windows.Forms.ListView
    Friend WithEvents lvFoods_chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents tsFoods As System.Windows.Forms.ToolStrip
    Friend WithEvents gbSpices As System.Windows.Forms.GroupBox
    Friend WithEvents lvSpices As System.Windows.Forms.ListView
    Friend WithEvents lvCombinations_chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents tsSpices As System.Windows.Forms.ToolStrip
    Friend WithEvents tsShelf_ICPC As System.Windows.Forms.ToolStripButton
End Class
