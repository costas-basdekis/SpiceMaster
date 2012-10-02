Imports System.IO
Imports SpiceMaster.Core

Public Class frmLoadFileSelect
    Dim m_Path As String

    Public Function SelectFile() As String
        m_Path = ""
        ShowDialog(mdiMain)

        Return m_Path
    End Function

    Private Sub LoadFile_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Localize(mdiMain.MyMUISources.Current)

        lvMain.Items.Clear()

        RDL_Changed()

        For Each s As String In My.Computer.FileSystem.GetFiles(Directory.GetCurrentDirectory())
            If s.EndsWith(".smd") Then
                With lvMain.Items.Add(Path.GetFileName(s))
                    .SubItems.Add(Path.GetDirectoryName(s))
                    .Group = lvMain.Groups.Item("ghCD")
                End With
            End If
        Next

        With lvMain.Items.Add("")
            .SubItems.Add("")
            .Group = lvMain.Groups.Item("ghLoad")
            .Text = .Group.Header
        End With

        ofdMain.InitialDirectory = Directory.GetCurrentDirectory()
    End Sub

    Friend Sub Localize(ByVal Source As MUISource)
        Text = Source("Forms", "LoadFileSelect", "FormCaption")
        ofdMain.Title = Text

        lvMain.Columns.Item(0).Text = Source("Forms", "LoadFileSelect", "ListHeaders", "Name")
        lvMain.Columns.Item(1).Text = Source("Forms", "LoadFileSelect", "ListHeaders", "Path")

        lvMain.Groups.Item("ghRDL").Header = Source("Forms", "LoadFileSelect", "GroupHeaders", "RDL")
        lvMain.Groups.Item("ghCD").Header = Source("Forms", "LoadFileSelect", "GroupHeaders", "CD")
        lvMain.Groups.Item("ghLoad").Header = Source("Forms", "LoadFileSelect", "GroupHeaders", "Load")
    End Sub
    Private Sub RDL_Changed()
        Dim i As Integer, s As String

        For i = 1 To mdiMain.MyRDL.Count
            s = mdiMain.MyRDL.Items(i)
            Dim lvi As ListViewItem = lvMain.Items.Add(Path.GetFileName(s))
            lvi.SubItems.Add(Path.GetDirectoryName(s))
            lvi.Group = lvMain.Groups.Item("ghRDL")
        Next
    End Sub

    Private Sub lvMain_ItemActivate(sender As Object, e As EventArgs) Handles lvMain.ItemActivate
        If lvMain.SelectedItems.Count = 0 Then Return

        With lvMain.SelectedItems.Item(0)
            If .SubItems(1).Text = "" Then
                If ofdMain.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    m_Path = ofdMain.FileName
                    Close()
                End If
            Else
                m_Path = String.Format("{0}\{1}", .SubItems(1).Text, .Text)
                Close()
            End If
        End With
    End Sub

    Private Sub frmLoadFileSelect_KeyDown(sender As System.Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class