<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefineCombination
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
		Me.gbSpice = New System.Windows.Forms.GroupBox
		Me.butOK = New System.Windows.Forms.Button
		Me.cbSpice = New System.Windows.Forms.ComboBox
		Me.gbDefinitions = New System.Windows.Forms.GroupBox
		Me.lvDefinitions = New System.Windows.Forms.ListView
		Me.lvDefinitions_chName = New System.Windows.Forms.ColumnHeader
		Me.lvDefinitions_chAbbrevation = New System.Windows.Forms.ColumnHeader
		Me.tsDefinitions = New System.Windows.Forms.ToolStrip
		Me.tsDefinitions_tsbNew = New System.Windows.Forms.ToolStripButton
		Me.tsDefinitions_tsbRename = New System.Windows.Forms.ToolStripButton
		Me.tsDefinitions_tsbDelete = New System.Windows.Forms.ToolStripButton
		Me.gbCombinationComments = New System.Windows.Forms.GroupBox
		Me.lvCombinationComments = New System.Windows.Forms.ListView
		Me.tsCombinationComments = New System.Windows.Forms.ToolStrip
		Me.tbCombination = New System.Windows.Forms.TextBox
		Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
		Me.gbSpice.SuspendLayout()
		Me.gbDefinitions.SuspendLayout()
		Me.tsDefinitions.SuspendLayout()
		Me.gbCombinationComments.SuspendLayout()
		Me.tlpMain.SuspendLayout()
		Me.SuspendLayout()
		'
		'gbSpice
		'
		Me.gbSpice.Controls.Add(Me.tbCombination)
		Me.gbSpice.Controls.Add(Me.butOK)
		Me.gbSpice.Controls.Add(Me.cbSpice)
		Me.gbSpice.Location = New System.Drawing.Point(3, 3)
		Me.gbSpice.Name = "gbSpice"
		Me.gbSpice.Size = New System.Drawing.Size(315, 72)
		Me.gbSpice.TabIndex = 1
		Me.gbSpice.TabStop = False
		Me.gbSpice.Text = "Spice:"
		'
		'butOK
		'
		Me.butOK.Location = New System.Drawing.Point(245, 19)
		Me.butOK.Name = "butOK"
		Me.butOK.Size = New System.Drawing.Size(64, 23)
		Me.butOK.TabIndex = 0
		Me.butOK.Text = "OK"
		Me.butOK.UseVisualStyleBackColor = True
		'
		'cbSpice
		'
		Me.cbSpice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbSpice.FormattingEnabled = True
		Me.cbSpice.Location = New System.Drawing.Point(6, 19)
		Me.cbSpice.Name = "cbSpice"
		Me.cbSpice.Size = New System.Drawing.Size(233, 21)
		Me.cbSpice.TabIndex = 1
		'
		'gbDefinitions
		'
		Me.gbDefinitions.Controls.Add(Me.lvDefinitions)
		Me.gbDefinitions.Controls.Add(Me.tsDefinitions)
		Me.gbDefinitions.Location = New System.Drawing.Point(3, 81)
		Me.gbDefinitions.Name = "gbDefinitions"
		Me.gbDefinitions.Size = New System.Drawing.Size(315, 273)
		Me.gbDefinitions.TabIndex = 2
		Me.gbDefinitions.TabStop = False
		Me.gbDefinitions.Text = "Definitions:"
		'
		'lvDefinitions
		'
		Me.lvDefinitions.CheckBoxes = True
		Me.lvDefinitions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvDefinitions_chName, Me.lvDefinitions_chAbbrevation})
		Me.lvDefinitions.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lvDefinitions.FullRowSelect = True
		Me.lvDefinitions.GridLines = True
		Me.lvDefinitions.HideSelection = False
		Me.lvDefinitions.Location = New System.Drawing.Point(3, 41)
		Me.lvDefinitions.Name = "lvDefinitions"
		Me.lvDefinitions.Size = New System.Drawing.Size(309, 229)
		Me.lvDefinitions.TabIndex = 3
		Me.lvDefinitions.UseCompatibleStateImageBehavior = False
		Me.lvDefinitions.View = System.Windows.Forms.View.Details
		'
		'lvDefinitions_chName
		'
		Me.lvDefinitions_chName.Text = "Full Name"
		Me.lvDefinitions_chName.Width = 179
		'
		'lvDefinitions_chAbbrevation
		'
		Me.lvDefinitions_chAbbrevation.Text = "Abbrevation"
		Me.lvDefinitions_chAbbrevation.Width = 102
		'
		'tsDefinitions
		'
		Me.tsDefinitions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsDefinitions_tsbNew, Me.tsDefinitions_tsbRename, Me.tsDefinitions_tsbDelete})
		Me.tsDefinitions.Location = New System.Drawing.Point(3, 16)
		Me.tsDefinitions.Name = "tsDefinitions"
		Me.tsDefinitions.Size = New System.Drawing.Size(309, 25)
		Me.tsDefinitions.TabIndex = 2
		Me.tsDefinitions.Text = "Items"
		'
		'tsDefinitions_tsbNew
		'
		Me.tsDefinitions_tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsDefinitions_tsbNew.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_LIST_ADD
		Me.tsDefinitions_tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsDefinitions_tsbNew.Name = "tsDefinitions_tsbNew"
		Me.tsDefinitions_tsbNew.Size = New System.Drawing.Size(23, 22)
		Me.tsDefinitions_tsbNew.Text = "New"
		'
		'tsDefinitions_tsbRename
		'
		Me.tsDefinitions_tsbRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsDefinitions_tsbRename.Enabled = False
		Me.tsDefinitions_tsbRename.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_LIST_EDIT
		Me.tsDefinitions_tsbRename.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsDefinitions_tsbRename.Name = "tsDefinitions_tsbRename"
		Me.tsDefinitions_tsbRename.Size = New System.Drawing.Size(23, 22)
		Me.tsDefinitions_tsbRename.Text = "Rename"
		'
		'tsDefinitions_tsbDelete
		'
		Me.tsDefinitions_tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsDefinitions_tsbDelete.Enabled = False
		Me.tsDefinitions_tsbDelete.Image = Global.SpiceMaster.My.Resources.Resources.IMAGE_LIST_REMOVE
		Me.tsDefinitions_tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsDefinitions_tsbDelete.Name = "tsDefinitions_tsbDelete"
		Me.tsDefinitions_tsbDelete.Size = New System.Drawing.Size(23, 22)
		Me.tsDefinitions_tsbDelete.Text = "Delete"
		'
		'gbCombinationComments
		'
		Me.gbCombinationComments.Controls.Add(Me.lvCombinationComments)
		Me.gbCombinationComments.Controls.Add(Me.tsCombinationComments)
		Me.gbCombinationComments.Location = New System.Drawing.Point(3, 360)
		Me.gbCombinationComments.Name = "gbCombinationComments"
		Me.gbCombinationComments.Size = New System.Drawing.Size(315, 113)
		Me.gbCombinationComments.TabIndex = 3
		Me.gbCombinationComments.TabStop = False
		Me.gbCombinationComments.Text = "Comments:"
		'
		'lvCombinationComments
		'
		Me.lvCombinationComments.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lvCombinationComments.FullRowSelect = True
		Me.lvCombinationComments.GridLines = True
		Me.lvCombinationComments.HideSelection = False
		Me.lvCombinationComments.Location = New System.Drawing.Point(29, 16)
		Me.lvCombinationComments.Name = "lvCombinationComments"
		Me.lvCombinationComments.Size = New System.Drawing.Size(283, 94)
		Me.lvCombinationComments.TabIndex = 4
		Me.lvCombinationComments.UseCompatibleStateImageBehavior = False
		Me.lvCombinationComments.View = System.Windows.Forms.View.Details
		'
		'tsCombinationComments
		'
		Me.tsCombinationComments.Dock = System.Windows.Forms.DockStyle.Left
		Me.tsCombinationComments.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.tsCombinationComments.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
		Me.tsCombinationComments.Location = New System.Drawing.Point(3, 16)
		Me.tsCombinationComments.Name = "tsCombinationComments"
		Me.tsCombinationComments.Size = New System.Drawing.Size(26, 94)
		Me.tsCombinationComments.TabIndex = 3
		Me.tsCombinationComments.Text = "Combinations"
		'
		'tbCombination
		'
		Me.tbCombination.Location = New System.Drawing.Point(6, 46)
		Me.tbCombination.Name = "tbCombination"
		Me.tbCombination.ReadOnly = True
		Me.tbCombination.Size = New System.Drawing.Size(303, 20)
		Me.tbCombination.TabIndex = 3
		'
		'tlpMain
		'
		Me.tlpMain.ColumnCount = 1
		Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tlpMain.Controls.Add(Me.gbSpice, 0, 0)
		Me.tlpMain.Controls.Add(Me.gbCombinationComments, 0, 2)
		Me.tlpMain.Controls.Add(Me.gbDefinitions, 0, 1)
		Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tlpMain.Location = New System.Drawing.Point(0, 0)
		Me.tlpMain.Name = "tlpMain"
		Me.tlpMain.RowCount = 3
		Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tlpMain.Size = New System.Drawing.Size(321, 476)
		Me.tlpMain.TabIndex = 5
		'
		'frmDefineCombination
		'
		Me.AcceptButton = Me.butOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(321, 476)
		Me.Controls.Add(Me.tlpMain)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.KeyPreview = True
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmDefineCombination"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Define combination"
		Me.gbSpice.ResumeLayout(False)
		Me.gbSpice.PerformLayout()
		Me.gbDefinitions.ResumeLayout(False)
		Me.gbDefinitions.PerformLayout()
		Me.tsDefinitions.ResumeLayout(False)
		Me.tsDefinitions.PerformLayout()
		Me.gbCombinationComments.ResumeLayout(False)
		Me.gbCombinationComments.PerformLayout()
		Me.tlpMain.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents gbSpice As System.Windows.Forms.GroupBox
	Friend WithEvents gbDefinitions As System.Windows.Forms.GroupBox
	Friend WithEvents lvDefinitions As System.Windows.Forms.ListView
	Friend WithEvents lvDefinitions_chName As System.Windows.Forms.ColumnHeader
	Friend WithEvents tsDefinitions As System.Windows.Forms.ToolStrip
	Friend WithEvents tsDefinitions_tsbNew As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsDefinitions_tsbRename As System.Windows.Forms.ToolStripButton
	Friend WithEvents tsDefinitions_tsbDelete As System.Windows.Forms.ToolStripButton
	Friend WithEvents lvDefinitions_chAbbrevation As System.Windows.Forms.ColumnHeader
	Friend WithEvents butOK As System.Windows.Forms.Button
	Friend WithEvents cbSpice As System.Windows.Forms.ComboBox
	Friend WithEvents gbCombinationComments As System.Windows.Forms.GroupBox
	Friend WithEvents lvCombinationComments As System.Windows.Forms.ListView
	Friend WithEvents tsCombinationComments As System.Windows.Forms.ToolStrip
	Friend WithEvents tbCombination As System.Windows.Forms.TextBox
	Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel

End Class
