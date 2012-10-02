<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoConvertSpices
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
		Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("To be converted:", System.Windows.Forms.HorizontalAlignment.Left)
		Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Not to be converted:", System.Windows.Forms.HorizontalAlignment.Left)
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutoConvertSpices))
		Me.gbMain = New System.Windows.Forms.GroupBox
		Me.lvMain = New System.Windows.Forms.ListView
		Me.lvMain_chItem = New System.Windows.Forms.ColumnHeader
		Me.lvMain_chComments = New System.Windows.Forms.ColumnHeader
		Me.tsMain = New System.Windows.Forms.ToolStrip
		Me.tsMain_tsbConvertAll = New System.Windows.Forms.ToolStripButton
		Me.tsMain_tsbConvertSelected = New System.Windows.Forms.ToolStripButton
		Me.tsMain_tssSep1 = New System.Windows.Forms.ToolStripSeparator
		Me.tsMain_tsbCheckAll = New System.Windows.Forms.ToolStripButton
		Me.tsMain_tsbCheckNone = New System.Windows.Forms.ToolStripButton
		Me.scMain = New System.Windows.Forms.SplitContainer
		Me.gbCombinations = New System.Windows.Forms.GroupBox
		Me.lvCombinations = New System.Windows.Forms.ListView
		Me.tsCombinations = New System.Windows.Forms.ToolStrip
		Me.gbMain.SuspendLayout()
		Me.tsMain.SuspendLayout()
		Me.scMain.Panel1.SuspendLayout()
		Me.scMain.Panel2.SuspendLayout()
		Me.scMain.SuspendLayout()
		Me.gbCombinations.SuspendLayout()
		Me.SuspendLayout()
		'
		'gbMain
		'
		Me.gbMain.Controls.Add(Me.lvMain)
		Me.gbMain.Controls.Add(Me.tsMain)
		Me.gbMain.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbMain.Location = New System.Drawing.Point(0, 0)
		Me.gbMain.Name = "gbMain"
		Me.gbMain.Size = New System.Drawing.Size(362, 403)
		Me.gbMain.TabIndex = 0
		Me.gbMain.TabStop = False
		Me.gbMain.Text = "Found 0 items (0 to convert):"
		'
		'lvMain
		'
		Me.lvMain.CheckBoxes = True
		Me.lvMain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvMain_chItem, Me.lvMain_chComments})
		Me.lvMain.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lvMain.FullRowSelect = True
		Me.lvMain.GridLines = True
		ListViewGroup1.Header = "To be converted:"
		ListViewGroup1.Name = "lvMain_lvgTBC"
		ListViewGroup2.Header = "Not to be converted:"
		ListViewGroup2.Name = "lvMain_lvgNTBC"
		Me.lvMain.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
		Me.lvMain.HideSelection = False
		Me.lvMain.Location = New System.Drawing.Point(3, 41)
		Me.lvMain.Name = "lvMain"
		Me.lvMain.Size = New System.Drawing.Size(356, 359)
		Me.lvMain.Sorting = System.Windows.Forms.SortOrder.Ascending
		Me.lvMain.TabIndex = 1
		Me.lvMain.UseCompatibleStateImageBehavior = False
		Me.lvMain.View = System.Windows.Forms.View.Details
		'
		'lvMain_chItem
		'
		Me.lvMain_chItem.Text = "Item"
		Me.lvMain_chItem.Width = 140
		'
		'lvMain_chComments
		'
		Me.lvMain_chComments.Text = "Conversion"
		Me.lvMain_chComments.Width = 207
		'
		'tsMain
		'
		Me.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsMain_tsbConvertAll, Me.tsMain_tsbConvertSelected, Me.tsMain_tssSep1, Me.tsMain_tsbCheckAll, Me.tsMain_tsbCheckNone})
		Me.tsMain.Location = New System.Drawing.Point(3, 16)
		Me.tsMain.Name = "tsMain"
		Me.tsMain.Size = New System.Drawing.Size(356, 25)
		Me.tsMain.TabIndex = 0
		Me.tsMain.Text = "ToolStrip1"
		'
		'tsMain_tsbConvertAll
		'
		Me.tsMain_tsbConvertAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsMain_tsbConvertAll.Image = CType(resources.GetObject("tsMain_tsbConvertAll.Image"), System.Drawing.Image)
		Me.tsMain_tsbConvertAll.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsMain_tsbConvertAll.Name = "tsMain_tsbConvertAll"
		Me.tsMain_tsbConvertAll.Size = New System.Drawing.Size(70, 22)
		Me.tsMain_tsbConvertAll.Text = "Convert All"
		'
		'tsMain_tsbConvertSelected
		'
		Me.tsMain_tsbConvertSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsMain_tsbConvertSelected.Image = CType(resources.GetObject("tsMain_tsbConvertSelected.Image"), System.Drawing.Image)
		Me.tsMain_tsbConvertSelected.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsMain_tsbConvertSelected.Name = "tsMain_tsbConvertSelected"
		Me.tsMain_tsbConvertSelected.Size = New System.Drawing.Size(100, 22)
		Me.tsMain_tsbConvertSelected.Text = "Convert Selected"
		'
		'tsMain_tssSep1
		'
		Me.tsMain_tssSep1.Name = "tsMain_tssSep1"
		Me.tsMain_tssSep1.Size = New System.Drawing.Size(6, 25)
		'
		'tsMain_tsbCheckAll
		'
		Me.tsMain_tsbCheckAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsMain_tsbCheckAll.Image = CType(resources.GetObject("tsMain_tsbCheckAll.Image"), System.Drawing.Image)
		Me.tsMain_tsbCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsMain_tsbCheckAll.Name = "tsMain_tsbCheckAll"
		Me.tsMain_tsbCheckAll.Size = New System.Drawing.Size(61, 22)
		Me.tsMain_tsbCheckAll.Text = "Check All"
		'
		'tsMain_tsbCheckNone
		'
		Me.tsMain_tsbCheckNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsMain_tsbCheckNone.Image = CType(resources.GetObject("tsMain_tsbCheckNone.Image"), System.Drawing.Image)
		Me.tsMain_tsbCheckNone.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsMain_tsbCheckNone.Name = "tsMain_tsbCheckNone"
		Me.tsMain_tsbCheckNone.Size = New System.Drawing.Size(76, 22)
		Me.tsMain_tsbCheckNone.Text = "Check None"
		'
		'scMain
		'
		Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
		Me.scMain.Location = New System.Drawing.Point(0, 0)
		Me.scMain.Name = "scMain"
		'
		'scMain.Panel1
		'
		Me.scMain.Panel1.Controls.Add(Me.gbMain)
		'
		'scMain.Panel2
		'
		Me.scMain.Panel2.Controls.Add(Me.gbCombinations)
		Me.scMain.Size = New System.Drawing.Size(673, 403)
		Me.scMain.SplitterDistance = 362
		Me.scMain.TabIndex = 2
		'
		'gbCombinations
		'
		Me.gbCombinations.Controls.Add(Me.lvCombinations)
		Me.gbCombinations.Controls.Add(Me.tsCombinations)
		Me.gbCombinations.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbCombinations.Location = New System.Drawing.Point(0, 0)
		Me.gbCombinations.Name = "gbCombinations"
		Me.gbCombinations.Size = New System.Drawing.Size(307, 403)
		Me.gbCombinations.TabIndex = 2
		Me.gbCombinations.TabStop = False
		Me.gbCombinations.Text = "Convert to:"
		'
		'lvCombinations
		'
		Me.lvCombinations.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lvCombinations.FullRowSelect = True
		Me.lvCombinations.GridLines = True
		Me.lvCombinations.HideSelection = False
		Me.lvCombinations.Location = New System.Drawing.Point(3, 41)
		Me.lvCombinations.Name = "lvCombinations"
		Me.lvCombinations.Size = New System.Drawing.Size(301, 359)
		Me.lvCombinations.TabIndex = 1
		Me.lvCombinations.UseCompatibleStateImageBehavior = False
		Me.lvCombinations.View = System.Windows.Forms.View.Details
		'
		'tsCombinations
		'
		Me.tsCombinations.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.tsCombinations.Location = New System.Drawing.Point(3, 16)
		Me.tsCombinations.Name = "tsCombinations"
		Me.tsCombinations.Size = New System.Drawing.Size(301, 25)
		Me.tsCombinations.TabIndex = 0
		Me.tsCombinations.Text = "ToolStrip1"
		'
		'frmAutoConvertSpices
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(673, 403)
		Me.Controls.Add(Me.scMain)
		Me.KeyPreview = True
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAutoConvertSpices"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Auto Convert Spices"
		Me.gbMain.ResumeLayout(False)
		Me.gbMain.PerformLayout()
		Me.tsMain.ResumeLayout(False)
		Me.tsMain.PerformLayout()
		Me.scMain.Panel1.ResumeLayout(False)
		Me.scMain.Panel2.ResumeLayout(False)
		Me.scMain.ResumeLayout(False)
		Me.gbCombinations.ResumeLayout(False)
		Me.gbCombinations.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents gbMain As System.Windows.Forms.GroupBox
	Friend WithEvents tsMain As System.Windows.Forms.ToolStrip
	Friend WithEvents lvMain As System.Windows.Forms.ListView
	Friend WithEvents lvMain_chItem As System.Windows.Forms.ColumnHeader
	Friend WithEvents tsMain_tsbConvertAll As System.Windows.Forms.ToolStripButton
	Friend WithEvents lvMain_chComments As System.Windows.Forms.ColumnHeader
	Friend WithEvents scMain As System.Windows.Forms.SplitContainer
	Friend WithEvents gbCombinations As System.Windows.Forms.GroupBox
	Friend WithEvents lvCombinations As System.Windows.Forms.ListView
	Friend WithEvents tsCombinations As System.Windows.Forms.ToolStrip
	Friend WithEvents tsMain_tsbConvertSelected As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsMain_tssSep1 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents tsMain_tsbCheckAll As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsMain_tsbCheckNone As System.Windows.Forms.ToolStripButton
End Class
