<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDocumentChanges
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
        Me.lbEvents = New System.Windows.Forms.ListBox()
        Me.lbChanges = New System.Windows.Forms.Label()
        Me.tbEvent = New System.Windows.Forms.TextBox()
        Me.cbShowEvents = New System.Windows.Forms.CheckBox()
        Me.butClear = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lbEvents
        '
        Me.lbEvents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbEvents.FormattingEnabled = True
        Me.lbEvents.Location = New System.Drawing.Point(11, 24)
        Me.lbEvents.Name = "lbEvents"
        Me.lbEvents.Size = New System.Drawing.Size(644, 264)
        Me.lbEvents.TabIndex = 0
        '
        'lbChanges
        '
        Me.lbChanges.AutoSize = True
        Me.lbChanges.Location = New System.Drawing.Point(12, 8)
        Me.lbChanges.Name = "lbChanges"
        Me.lbChanges.Size = New System.Drawing.Size(134, 13)
        Me.lbChanges.TabIndex = 1
        Me.lbChanges.Text = "Document Changes: 0 (+0)"
        '
        'tbEvent
        '
        Me.tbEvent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbEvent.Location = New System.Drawing.Point(12, 294)
        Me.tbEvent.Multiline = True
        Me.tbEvent.Name = "tbEvent"
        Me.tbEvent.Size = New System.Drawing.Size(643, 165)
        Me.tbEvent.TabIndex = 2
        '
        'cbShowEvents
        '
        Me.cbShowEvents.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbShowEvents.AutoSize = True
        Me.cbShowEvents.Location = New System.Drawing.Point(546, 24)
        Me.cbShowEvents.Name = "cbShowEvents"
        Me.cbShowEvents.Size = New System.Drawing.Size(78, 17)
        Me.cbShowEvents.TabIndex = 3
        Me.cbShowEvents.Text = "List Events"
        Me.cbShowEvents.UseVisualStyleBackColor = True
        '
        'butClear
        '
        Me.butClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butClear.Location = New System.Drawing.Point(546, 47)
        Me.butClear.Name = "butClear"
        Me.butClear.Size = New System.Drawing.Size(78, 23)
        Me.butClear.TabIndex = 4
        Me.butClear.Text = "Clear"
        Me.butClear.UseVisualStyleBackColor = True
        '
        'frmDocumentChanges
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 471)
        Me.Controls.Add(Me.butClear)
        Me.Controls.Add(Me.cbShowEvents)
        Me.Controls.Add(Me.tbEvent)
        Me.Controls.Add(Me.lbChanges)
        Me.Controls.Add(Me.lbEvents)
        Me.Name = "frmDocumentChanges"
        Me.Text = "Document Changes"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbEvents As System.Windows.Forms.ListBox
    Friend WithEvents lbChanges As System.Windows.Forms.Label
    Friend WithEvents tbEvent As System.Windows.Forms.TextBox
    Friend WithEvents cbShowEvents As System.Windows.Forms.CheckBox
    Friend WithEvents butClear As System.Windows.Forms.Button
End Class
