Imports System.Windows.Forms
Imports SpiceMaster.Core

Public Class frmLanguagesManager

#Region "Variables"
    Dim m_Loaded As Boolean = False

    Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

    Dim m_SettingChecks As Boolean = False
    Private m_FileIsReadOnly As String
#End Region

#Region "Form"
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim msCurrent As MUISource = MyMUISources.Current, mSource As MUISource

        m_Loaded = False
        With MyMUISources
            For i As Integer = 1 To .Count
                mSource = .Item(i)
                With lvLanguages.Items.Add(mSource.LanguageName)
                    .Tag = mSource
                    If msCurrent IsNot Nothing Then
                        If ReferenceEquals(msCurrent, mSource) Then
                            m_SettingChecks = True
                            .Checked = True
                            m_SettingChecks = False
                            msCurrent = Nothing
                        End If
                    End If
                End With
            Next
        End With
        DisplaySource(MyMUISources.Current)
        m_Loaded = True

        Localize(MyMUISources.Current)
    End Sub
    Private Sub DisplaySource(ByVal Source As MUISource)
        Dim tn As TreeNode

        tvItems.Tag = Source
        With tvItems.Nodes
            .Clear()
            tn = .Add("MUIXML Language = '" & Source.Language & "'")
            tn.Tag = Nothing
            EnumNode(tn, Source.Document("MUIXML"))
            'tn.ExpandAll()
        End With
        SelectNode(Nothing)
    End Sub
    Private Sub EnumNode(ByVal Node As TreeNode, ByVal Source As Xml.XmlElement)
        Dim i As Integer
        Dim xel As Xml.XmlElement, xatt As Xml.XmlAttribute
        Dim tn As TreeNode

        With Source.ChildNodes
            For i = 1 To .Count
                xel = .Item(i - 1)
                If xel.Attributes.Count > 0 Then
                    tn = Node.Nodes.Add(xel.Name & " = '" & xel.Attributes(0).Value & "'")
                    xatt = xel.Attributes(0)
                    tn.Tag = xatt
                Else
                    tn = Node.Nodes.Add(xel.Name)
                    tn.Tag = Nothing
                End If
                EnumNode(tn, xel)
                'tn.Expand()
            Next
        End With
    End Sub
    Private Sub SelectNode(ByVal Node As TreeNode)
        Dim xatt As Xml.XmlAttribute
        tbValue.Text = ""

        If (Node Is Nothing) OrElse (Node.Tag Is Nothing) Then
            tbValue.Text = ""
            tbValue.Enabled = False
        Else
            xatt = Node.Tag
            tbValue.Text = xatt.Value
            tbValue.Enabled = True
        End If
        butUpdate.Enabled = tbValue.Enabled
    End Sub
    Sub SelectSource(ByVal Item As ListViewItem)
        Dim i As Integer, lvi As ListViewItem

        If Not m_Loaded Then Return
        If m_SettingChecks Then Return

        m_SettingChecks = True
        With lvLanguages.Items
            For i = 1 To .Count
                lvi = .Item(i - 1)
                lvi.Checked = False
            Next
            Item.Checked = True
            MyMUISources.Current = Item.Tag
        End With
        m_SettingChecks = False
        DisplaySource(MyMUISources.Current)
    End Sub

    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
#End Region

#Region "Items"
    Private Sub lvLanguages_ItemChecked(ByVal sender As Object, ByVal e As Windows.Forms.ItemCheckedEventArgs) Handles lvLanguages.ItemChecked
        SelectSource(e.Item)
    End Sub

    Private Sub tvItems_AfterSelect(ByVal sender As Object, ByVal e As Windows.Forms.TreeViewEventArgs) Handles tvItems.AfterSelect
        SelectNode(e.Node)
    End Sub

    Private Sub tbValue_KeyDown(ByVal sender As Object, ByVal e As Windows.Forms.KeyEventArgs) Handles tbValue.KeyDown
        If e.KeyCode = Keys.Enter Then
            butUpdate_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub tbValue_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles tbValue.LostFocus
        butUpdate_Click(Nothing, Nothing)
    End Sub
    Private Sub butUpdate_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles butUpdate.Click
        Dim ms As MUISource
        Dim xatt As Xml.XmlAttribute

        If Not m_Loaded Then Return
        If m_SettingChecks Then Return

        If tbValue.Enabled AndAlso (tvItems.SelectedNode.Tag IsNot Nothing) Then
            xatt = tvItems.SelectedNode.Tag
            If tbValue.Text = "" Then
                tbValue.Text = xatt.Value
            Else
                If StrComp(xatt.Value, tbValue.Text, CompareMethod.Text) <> 0 Then
                    If MyMUISources.Current.ReadOnly Then
                        MessageBox.Show(m_FileIsReadOnly)
                    End If
                    xatt.Value = tbValue.Text
                    If StrComp(xatt.Value, tbValue.Text, CompareMethod.Text) = 0 Then
                        tvItems.SelectedNode.Text = String.Format("{0} = '{1}'", xatt.OwnerElement.Name, xatt.Value)
                        tbValue.Text = xatt.Value
                        MyMUISources.Refresh()
                        ms = tvItems.Tag
                        ms.Save()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub tsLanguages_tsbCreateCopy_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsLanguages_tsbCreateCopy.Click
        Dim sLangNameEng As String, sPostFix As String, sLangNameLoc As String
        Dim mSource As MUISource
        Dim lvi As ListViewItem

        sLangNameEng = InputBox("Enter the language name (in English):", "New language", "New language") : If sLangNameEng = "" Then Return
EnterFileName:
        sPostFix = InputBox("Enter the file's postfix (as xxx in MUI-xxx.xml):", "New language", "lng") : If sPostFix = "" Then Return
        mSource = MyMUISources.ByFile("MUI-" & sLangNameEng & ".xml") : If mSource IsNot Nothing Then MsgBox("This file already exists. Specify another.", MsgBoxStyle.OkOnly) : GoTo EnterFileName

        Try
            sPostFix = My.Computer.FileSystem.CombinePath(My.Computer.FileSystem.CurrentDirectory, "MUI-" & sPostFix & ".xml")
        Catch
            MsgBox("Invalid filename. Specify another.", MsgBoxStyle.OkOnly)
            GoTo EnterFileName
        End Try

        sLangNameLoc = InputBox("Enter the language name (localized):", "New language", "New language") : If sLangNameLoc = "" Then Return

        mSource = tvItems.Tag
        mSource = mSource.CreateCopy(sLangNameEng, sPostFix, sLangNameLoc)
        mSource.Save()
        MyMUISources.Add(mSource)
        lvi = lvLanguages.Items.Add(sLangNameEng)
        lvi.Tag = mSource
        SelectSource(lvi)
    End Sub
#End Region

#Region "MUI"
    Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
        Text = Source("Forms", "LanguagesManager", "FormCaption")
        gbLanguages.Text = Source("General", "ListCaptions", "Languages")
        tsLanguages_tsbCreateCopy.Text = Source("Forms", "LanguagesManager", "General", "CreateCopyCaption")
        butUpdate.Text = Source("Forms", "LanguagesManager", "General", "UpdateCaption")

        m_FileIsReadOnly =String.Format(Source("Forms", "LanguagesManager", "Messages", "FileIsReadOnly"), tsLanguages_tsbCreateCopy.Text)
    End Sub
#End Region
End Class