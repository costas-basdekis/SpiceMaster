<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadFileProccess
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
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.lblDocument = New System.Windows.Forms.Label()
        Me.pgMain = New System.Windows.Forms.ProgressBar()
        Me.butClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblLoading
        '
        Me.lblLoading.AutoSize = True
        Me.lblLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblLoading.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLoading.Location = New System.Drawing.Point(60, 39)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(389, 46)
        Me.lblLoading.TabIndex = 0
        Me.lblLoading.Text = "Loading Document..."
        '
        'lblDocument
        '
        Me.lblDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDocument.AutoEllipsis = True
        Me.lblDocument.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblDocument.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDocument.Location = New System.Drawing.Point(22, 137)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(462, 85)
        Me.lblDocument.TabIndex = 1
        Me.lblDocument.Text = "C:\Program Files\Microsoft Visual Studio\Visual Basic 6\My Projects\SpiceMaster\M" & _
    "y Documents\Presentation\Draft Number 1.smd"
        Me.lblDocument.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pgMain
        '
        Me.pgMain.Location = New System.Drawing.Point(30, 260)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(437, 27)
        Me.pgMain.TabIndex = 2
        '
        'butClose
        '
        Me.butClose.Location = New System.Drawing.Point(388, 10)
        Me.butClose.Name = "butClose"
        Me.butClose.Size = New System.Drawing.Size(96, 26)
        Me.butClose.TabIndex = 3
        Me.butClose.Text = "Close"
        Me.butClose.UseVisualStyleBackColor = True
        Me.butClose.Visible = False
        '
        'frmLoadFileProcessScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(496, 321)
        Me.ControlBox = False
        Me.Controls.Add(Me.butClose)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.lblDocument)
        Me.Controls.Add(Me.lblLoading)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoadFileProcessScreen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLoading As System.Windows.Forms.Label
    Friend WithEvents lblDocument As System.Windows.Forms.Label
    Friend WithEvents pgMain As System.Windows.Forms.ProgressBar
    Friend WithEvents butClose As System.Windows.Forms.Button

End Class
