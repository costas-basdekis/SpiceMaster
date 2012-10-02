<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLanguagesManager
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLanguagesManager))
		Me.scMain = New System.Windows.Forms.SplitContainer
		Me.gbLanguages = New System.Windows.Forms.GroupBox
		Me.lvLanguages = New System.Windows.Forms.ListView
		Me.tsLanguages = New System.Windows.Forms.ToolStrip
		Me.tsLanguages_tsbCreateCopy = New System.Windows.Forms.ToolStripButton
		Me.tvItems = New System.Windows.Forms.TreeView
		Me.pnlText = New System.Windows.Forms.Panel
		Me.tbValue = New System.Windows.Forms.TextBox
		Me.butUpdate = New System.Windows.Forms.Button
		Me.scMain.Panel1.SuspendLayout()
		Me.scMain.Panel2.SuspendLayout()
		Me.scMain.SuspendLayout()
		Me.gbLanguages.SuspendLayout()
		Me.tsLanguages.SuspendLayout()
		Me.pnlText.SuspendLayout()
		Me.SuspendLayout()
		'
		'scMain
		'
		Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
		Me.scMain.Location = New System.Drawing.Point(0, 0)
		Me.scMain.Name = "scMain"
		'
		'scMain.Panel1
		'
		Me.scMain.Panel1.Controls.Add(Me.gbLanguages)
		'
		'scMain.Panel2
		'
		Me.scMain.Panel2.Controls.Add(Me.tvItems)
		Me.scMain.Panel2.Controls.Add(Me.pnlText)
		Me.scMain.Size = New System.Drawing.Size(435, 315)
		Me.scMain.SplitterDistance = 167
		Me.scMain.TabIndex = 1
		'
		'gbLanguages
		'
		Me.gbLanguages.Controls.Add(Me.lvLanguages)
		Me.gbLanguages.Controls.Add(Me.tsLanguages)
		Me.gbLanguages.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbLanguages.Location = New System.Drawing.Point(0, 0)
		Me.gbLanguages.Name = "gbLanguages"
		Me.gbLanguages.Size = New System.Drawing.Size(167, 315)
		Me.gbLanguages.TabIndex = 0
		Me.gbLanguages.TabStop = False
		Me.gbLanguages.Text = "Languages"
		'
		'lvLanguages
		'
		Me.lvLanguages.CheckBoxes = True
		Me.lvLanguages.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lvLanguages.Location = New System.Drawing.Point(3, 41)
		Me.lvLanguages.Name = "lvLanguages"
		Me.lvLanguages.Size = New System.Drawing.Size(161, 271)
		Me.lvLanguages.TabIndex = 1
		Me.lvLanguages.UseCompatibleStateImageBehavior = False
		Me.lvLanguages.View = System.Windows.Forms.View.List
		'
		'tsLanguages
		'
		Me.tsLanguages.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.tsLanguages.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsLanguages_tsbCreateCopy})
		Me.tsLanguages.Location = New System.Drawing.Point(3, 16)
		Me.tsLanguages.Name = "tsLanguages"
		Me.tsLanguages.Size = New System.Drawing.Size(161, 25)
		Me.tsLanguages.TabIndex = 0
		Me.tsLanguages.Text = "ToolStrip1"
		'
		'tsLanguages_tsbCreateCopy
		'
		Me.tsLanguages_tsbCreateCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsLanguages_tsbCreateCopy.Image = CType(resources.GetObject("tsLanguages_tsbCreateCopy.Image"), System.Drawing.Image)
		Me.tsLanguages_tsbCreateCopy.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsLanguages_tsbCreateCopy.Name = "tsLanguages_tsbCreateCopy"
		Me.tsLanguages_tsbCreateCopy.Size = New System.Drawing.Size(76, 22)
		Me.tsLanguages_tsbCreateCopy.Text = "Create Copy"
		'
		'tvItems
		'
		Me.tvItems.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tvItems.Location = New System.Drawing.Point(0, 22)
		Me.tvItems.Name = "tvItems"
		Me.tvItems.Size = New System.Drawing.Size(264, 293)
		Me.tvItems.TabIndex = 1
		'
		'pnlText
		'
		Me.pnlText.Controls.Add(Me.tbValue)
		Me.pnlText.Controls.Add(Me.butUpdate)
		Me.pnlText.Dock = System.Windows.Forms.DockStyle.Top
		Me.pnlText.Location = New System.Drawing.Point(0, 0)
		Me.pnlText.Name = "pnlText"
		Me.pnlText.Size = New System.Drawing.Size(264, 22)
		Me.pnlText.TabIndex = 2
		'
		'tbValue
		'
		Me.tbValue.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tbValue.Location = New System.Drawing.Point(0, 0)
		Me.tbValue.Name = "tbValue"
		Me.tbValue.Size = New System.Drawing.Size(196, 20)
		Me.tbValue.TabIndex = 0
		'
		'butUpdate
		'
		Me.butUpdate.Dock = System.Windows.Forms.DockStyle.Right
		Me.butUpdate.Location = New System.Drawing.Point(196, 0)
		Me.butUpdate.Name = "butUpdate"
		Me.butUpdate.Size = New System.Drawing.Size(68, 22)
		Me.butUpdate.TabIndex = 1
		Me.butUpdate.Text = "Update"
		Me.butUpdate.UseVisualStyleBackColor = True
		'
		'frmLanguagesManager
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(435, 315)
		Me.Controls.Add(Me.scMain)
		Me.KeyPreview = True
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmLanguagesManager"
		Me.ShowIcon = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Languages Manager"
		Me.scMain.Panel1.ResumeLayout(False)
		Me.scMain.Panel2.ResumeLayout(False)
		Me.scMain.ResumeLayout(False)
		Me.gbLanguages.ResumeLayout(False)
		Me.gbLanguages.PerformLayout()
		Me.tsLanguages.ResumeLayout(False)
		Me.tsLanguages.PerformLayout()
		Me.pnlText.ResumeLayout(False)
		Me.pnlText.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents scMain As System.Windows.Forms.SplitContainer
	Friend WithEvents gbLanguages As System.Windows.Forms.GroupBox
	Friend WithEvents tsLanguages As System.Windows.Forms.ToolStrip
	Friend WithEvents tsLanguages_tsbCreateCopy As System.Windows.Forms.ToolStripButton
	Friend WithEvents lvLanguages As System.Windows.Forms.ListView
	Friend WithEvents tvItems As System.Windows.Forms.TreeView
	Friend WithEvents tbValue As System.Windows.Forms.TextBox
	Friend WithEvents pnlText As System.Windows.Forms.Panel
	Friend WithEvents butUpdate As System.Windows.Forms.Button

End Class
