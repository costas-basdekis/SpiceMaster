<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.OK_Button = New System.Windows.Forms.Button
		Me.Cancel_Button = New System.Windows.Forms.Button
		Me.tcMain = New System.Windows.Forms.TabControl
		Me.tcMain_tpOptions = New System.Windows.Forms.TabPage
		Me.cbAutoLoadDB = New System.Windows.Forms.CheckBox
		Me.gbAutoLoadDB = New System.Windows.Forms.GroupBox
		Me.butSelectFile = New System.Windows.Forms.Button
		Me.tbSpecificFile = New System.Windows.Forms.TextBox
		Me.rbLoadSpecific = New System.Windows.Forms.RadioButton
		Me.rbLoadLast = New System.Windows.Forms.RadioButton
		Me.ofdMain = New System.Windows.Forms.OpenFileDialog
		Me.TableLayoutPanel1.SuspendLayout()
		Me.tcMain.SuspendLayout()
		Me.tcMain_tpOptions.SuspendLayout()
		Me.gbAutoLoadDB.SuspendLayout()
		Me.SuspendLayout()
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TableLayoutPanel1.ColumnCount = 2
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(66, 123)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 1
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
		Me.TableLayoutPanel1.TabIndex = 0
		'
		'OK_Button
		'
		Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.OK_Button.Location = New System.Drawing.Point(3, 3)
		Me.OK_Button.Name = "OK_Button"
		Me.OK_Button.Size = New System.Drawing.Size(67, 23)
		Me.OK_Button.TabIndex = 0
		Me.OK_Button.Text = "OK"
		'
		'Cancel_Button
		'
		Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
		Me.Cancel_Button.Name = "Cancel_Button"
		Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
		Me.Cancel_Button.TabIndex = 1
		Me.Cancel_Button.Text = "Cancel"
		'
		'tcMain
		'
		Me.tcMain.Controls.Add(Me.tcMain_tpOptions)
		Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tcMain.Location = New System.Drawing.Point(0, 0)
		Me.tcMain.Name = "tcMain"
		Me.tcMain.SelectedIndex = 0
		Me.tcMain.Size = New System.Drawing.Size(224, 164)
		Me.tcMain.TabIndex = 0
		'
		'tcMain_tpOptions
		'
		Me.tcMain_tpOptions.Controls.Add(Me.cbAutoLoadDB)
		Me.tcMain_tpOptions.Controls.Add(Me.gbAutoLoadDB)
		Me.tcMain_tpOptions.Location = New System.Drawing.Point(4, 22)
		Me.tcMain_tpOptions.Name = "tcMain_tpOptions"
		Me.tcMain_tpOptions.Padding = New System.Windows.Forms.Padding(3)
		Me.tcMain_tpOptions.Size = New System.Drawing.Size(216, 138)
		Me.tcMain_tpOptions.TabIndex = 0
		Me.tcMain_tpOptions.Text = "Startup"
		Me.tcMain_tpOptions.UseVisualStyleBackColor = True
		'
		'cbAutoLoadDB
		'
		Me.cbAutoLoadDB.AutoSize = True
		Me.cbAutoLoadDB.BackColor = System.Drawing.Color.Transparent
		Me.cbAutoLoadDB.Checked = True
		Me.cbAutoLoadDB.CheckState = System.Windows.Forms.CheckState.Checked
		Me.cbAutoLoadDB.Location = New System.Drawing.Point(8, 6)
		Me.cbAutoLoadDB.Name = "cbAutoLoadDB"
		Me.cbAutoLoadDB.Size = New System.Drawing.Size(118, 17)
		Me.cbAutoLoadDB.TabIndex = 1
		Me.cbAutoLoadDB.Text = "Auto load database"
		Me.cbAutoLoadDB.UseVisualStyleBackColor = False
		'
		'gbAutoLoadDB
		'
		Me.gbAutoLoadDB.Controls.Add(Me.butSelectFile)
		Me.gbAutoLoadDB.Controls.Add(Me.tbSpecificFile)
		Me.gbAutoLoadDB.Controls.Add(Me.rbLoadSpecific)
		Me.gbAutoLoadDB.Controls.Add(Me.rbLoadLast)
		Me.gbAutoLoadDB.ForeColor = System.Drawing.SystemColors.Control
		Me.gbAutoLoadDB.Location = New System.Drawing.Point(8, 6)
		Me.gbAutoLoadDB.Name = "gbAutoLoadDB"
		Me.gbAutoLoadDB.Size = New System.Drawing.Size(200, 91)
		Me.gbAutoLoadDB.TabIndex = 0
		Me.gbAutoLoadDB.TabStop = False
		Me.gbAutoLoadDB.Text = "Auto load database"
		'
		'butSelectFile
		'
		Me.butSelectFile.Enabled = False
		Me.butSelectFile.ForeColor = System.Drawing.SystemColors.ControlText
		Me.butSelectFile.Location = New System.Drawing.Point(119, 36)
		Me.butSelectFile.Name = "butSelectFile"
		Me.butSelectFile.Size = New System.Drawing.Size(75, 23)
		Me.butSelectFile.TabIndex = 3
		Me.butSelectFile.Text = "Select"
		Me.butSelectFile.UseVisualStyleBackColor = True
		'
		'tbSpecificFile
		'
		Me.tbSpecificFile.Enabled = False
		Me.tbSpecificFile.Location = New System.Drawing.Point(18, 65)
		Me.tbSpecificFile.Name = "tbSpecificFile"
		Me.tbSpecificFile.Size = New System.Drawing.Size(176, 20)
		Me.tbSpecificFile.TabIndex = 2
		'
		'rbLoadSpecific
		'
		Me.rbLoadSpecific.AutoSize = True
		Me.rbLoadSpecific.ForeColor = System.Drawing.SystemColors.ControlText
		Me.rbLoadSpecific.Location = New System.Drawing.Point(6, 42)
		Me.rbLoadSpecific.Name = "rbLoadSpecific"
		Me.rbLoadSpecific.Size = New System.Drawing.Size(79, 17)
		Me.rbLoadSpecific.TabIndex = 1
		Me.rbLoadSpecific.Text = "Specific file"
		Me.rbLoadSpecific.UseVisualStyleBackColor = True
		'
		'rbLoadLast
		'
		Me.rbLoadLast.AutoSize = True
		Me.rbLoadLast.Checked = True
		Me.rbLoadLast.ForeColor = System.Drawing.SystemColors.ControlText
		Me.rbLoadLast.Location = New System.Drawing.Point(6, 19)
		Me.rbLoadLast.Name = "rbLoadLast"
		Me.rbLoadLast.Size = New System.Drawing.Size(96, 17)
		Me.rbLoadLast.TabIndex = 0
		Me.rbLoadLast.TabStop = True
		Me.rbLoadLast.Text = "Last loaded file"
		Me.rbLoadLast.UseVisualStyleBackColor = True
		'
		'ofdMain
		'
		Me.ofdMain.DefaultExt = "*.xml"
		Me.ofdMain.FileName = "OpenFileDialog1"
		Me.ofdMain.Filter = "Spice Documents (*.xml)|*.xml|All files (*.*)|*.*"
		Me.ofdMain.Title = "Select default file to load"
		'
		'frmOptions
		'
		Me.AcceptButton = Me.OK_Button
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.Cancel_Button
		Me.ClientSize = New System.Drawing.Size(224, 164)
		Me.Controls.Add(Me.TableLayoutPanel1)
		Me.Controls.Add(Me.tcMain)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmOptions"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Options"
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.tcMain.ResumeLayout(False)
		Me.tcMain_tpOptions.ResumeLayout(False)
		Me.tcMain_tpOptions.PerformLayout()
		Me.gbAutoLoadDB.ResumeLayout(False)
		Me.gbAutoLoadDB.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents OK_Button As System.Windows.Forms.Button
	Friend WithEvents Cancel_Button As System.Windows.Forms.Button
	Friend WithEvents tcMain As System.Windows.Forms.TabControl
	Friend WithEvents tcMain_tpOptions As System.Windows.Forms.TabPage
	Friend WithEvents cbAutoLoadDB As System.Windows.Forms.CheckBox
	Friend WithEvents gbAutoLoadDB As System.Windows.Forms.GroupBox
	Friend WithEvents rbLoadLast As System.Windows.Forms.RadioButton
	Friend WithEvents butSelectFile As System.Windows.Forms.Button
	Friend WithEvents tbSpecificFile As System.Windows.Forms.TextBox
	Friend WithEvents rbLoadSpecific As System.Windows.Forms.RadioButton
	Friend WithEvents ofdMain As System.Windows.Forms.OpenFileDialog

End Class
