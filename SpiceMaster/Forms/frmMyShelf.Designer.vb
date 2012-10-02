<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMyShelf
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.gbCombinations = New System.Windows.Forms.GroupBox()
        Me.lvCombinations = New System.Windows.Forms.ListView()
        Me.lvCombinations_chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tsCombinations = New System.Windows.Forms.ToolStrip()
        Me.gbCombinations.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbCombinations
        '
        Me.gbCombinations.Controls.Add(Me.lvCombinations)
        Me.gbCombinations.Controls.Add(Me.tsCombinations)
        Me.gbCombinations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCombinations.Location = New System.Drawing.Point(0, 0)
        Me.gbCombinations.Name = "gbCombinations"
        Me.gbCombinations.Size = New System.Drawing.Size(458, 353)
        Me.gbCombinations.TabIndex = 3
        Me.gbCombinations.TabStop = False
        Me.gbCombinations.Text = "Spices:"
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
        Me.lvCombinations.Size = New System.Drawing.Size(452, 309)
        Me.lvCombinations.TabIndex = 2
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
        Me.tsCombinations.Size = New System.Drawing.Size(452, 25)
        Me.tsCombinations.TabIndex = 0
        Me.tsCombinations.Text = "Combinations"
        '
        'frmMyShelf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 353)
        Me.Controls.Add(Me.gbCombinations)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMyShelf"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "My Shelf"
        Me.gbCombinations.ResumeLayout(False)
        Me.gbCombinations.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbCombinations As System.Windows.Forms.GroupBox
    Friend WithEvents lvCombinations As System.Windows.Forms.ListView
    Friend WithEvents lvCombinations_chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents tsCombinations As System.Windows.Forms.ToolStrip

End Class
