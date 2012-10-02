<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadFileSelect
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Recently opened files", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Files in current directory", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Load another file", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Load another file")
        Me.lvMain = New System.Windows.Forms.ListView()
        Me.chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ofdMain = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'lvMain
        '
        Me.lvMain.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvMain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chName, Me.chPath})
        Me.lvMain.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lvMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMain.FullRowSelect = True
        ListViewGroup1.Header = "Recently opened files"
        ListViewGroup1.Name = "ghRDL"
        ListViewGroup2.Header = "Files in current directory"
        ListViewGroup2.Name = "ghCD"
        ListViewGroup3.Header = "Load another file"
        ListViewGroup3.Name = "ghLoad"
        Me.lvMain.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3})
        Me.lvMain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvMain.HideSelection = False
        Me.lvMain.HotTracking = True
        Me.lvMain.HoverSelection = True
        ListViewItem1.Group = ListViewGroup3
        Me.lvMain.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.lvMain.LabelWrap = False
        Me.lvMain.Location = New System.Drawing.Point(0, 0)
        Me.lvMain.MultiSelect = False
        Me.lvMain.Name = "lvMain"
        Me.lvMain.Size = New System.Drawing.Size(779, 395)
        Me.lvMain.TabIndex = 0
        Me.lvMain.UseCompatibleStateImageBehavior = False
        Me.lvMain.View = System.Windows.Forms.View.Details
        '
        'chName
        '
        Me.chName.Text = "Name"
        Me.chName.Width = 207
        '
        'chPath
        '
        Me.chPath.Text = "Path"
        Me.chPath.Width = 542
        '
        'ofdMain
        '
        Me.ofdMain.DefaultExt = "*.xml"
        Me.ofdMain.Filter = "All Spice Master Documents (*.xml, *.smd)|*.xml; *.smd|Spice Documents (*.xml)|*." & _
    "xml|SpiceMaster Documents (*.smd)|*.smd|All files (*.*)|*.*"
        Me.ofdMain.Title = "Select file to load"
        '
        'frmLoadFileSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 395)
        Me.Controls.Add(Me.lvMain)
        Me.KeyPreview = True
        Me.Name = "frmLoadFileSelect"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Load a file"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvMain As System.Windows.Forms.ListView
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents ofdMain As System.Windows.Forms.OpenFileDialog
End Class
