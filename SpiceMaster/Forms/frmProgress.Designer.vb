<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProgress
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
		Me.lblCategorylbl = New System.Windows.Forms.Label
		Me.lblCategory = New System.Windows.Forms.Label
		Me.lblItem = New System.Windows.Forms.Label
		Me.lblItemlbl = New System.Windows.Forms.Label
		Me.pgMain = New System.Windows.Forms.ProgressBar
		Me.butCancel = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'lblCategorylbl
		'
		Me.lblCategorylbl.AutoSize = True
		Me.lblCategorylbl.Location = New System.Drawing.Point(12, 9)
		Me.lblCategorylbl.Name = "lblCategorylbl"
		Me.lblCategorylbl.Size = New System.Drawing.Size(52, 13)
		Me.lblCategorylbl.TabIndex = 0
		Me.lblCategorylbl.Text = "Category:"
		'
		'lblCategory
		'
		Me.lblCategory.AutoSize = True
		Me.lblCategory.Location = New System.Drawing.Point(107, 9)
		Me.lblCategory.Name = "lblCategory"
		Me.lblCategory.Size = New System.Drawing.Size(0, 13)
		Me.lblCategory.TabIndex = 1
		'
		'lblItem
		'
		Me.lblItem.AutoSize = True
		Me.lblItem.Location = New System.Drawing.Point(107, 35)
		Me.lblItem.Name = "lblItem"
		Me.lblItem.Size = New System.Drawing.Size(0, 13)
		Me.lblItem.TabIndex = 3
		'
		'lblItemlbl
		'
		Me.lblItemlbl.AutoSize = True
		Me.lblItemlbl.Location = New System.Drawing.Point(12, 35)
		Me.lblItemlbl.Name = "lblItemlbl"
		Me.lblItemlbl.Size = New System.Drawing.Size(30, 13)
		Me.lblItemlbl.TabIndex = 2
		Me.lblItemlbl.Text = "Item:"
		'
		'pgMain
		'
		Me.pgMain.Location = New System.Drawing.Point(15, 62)
		Me.pgMain.Name = "pgMain"
		Me.pgMain.Size = New System.Drawing.Size(347, 23)
		Me.pgMain.Step = 1
		Me.pgMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous
		Me.pgMain.TabIndex = 4
		'
		'butCancel
		'
		Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.butCancel.Location = New System.Drawing.Point(272, 91)
		Me.butCancel.Name = "butCancel"
		Me.butCancel.Size = New System.Drawing.Size(90, 23)
		Me.butCancel.TabIndex = 5
		Me.butCancel.Text = "Cancel"
		Me.butCancel.UseVisualStyleBackColor = True
		'
		'frmProgress
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.butCancel
		Me.ClientSize = New System.Drawing.Size(376, 120)
		Me.Controls.Add(Me.butCancel)
		Me.Controls.Add(Me.pgMain)
		Me.Controls.Add(Me.lblItem)
		Me.Controls.Add(Me.lblItemlbl)
		Me.Controls.Add(Me.lblCategory)
		Me.Controls.Add(Me.lblCategorylbl)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmProgress"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Proccessing..."
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lblCategorylbl As System.Windows.Forms.Label
	Friend WithEvents lblCategory As System.Windows.Forms.Label
	Friend WithEvents lblItem As System.Windows.Forms.Label
	Friend WithEvents lblItemlbl As System.Windows.Forms.Label
	Friend WithEvents pgMain As System.Windows.Forms.ProgressBar
	Friend WithEvents butCancel As System.Windows.Forms.Button

End Class
