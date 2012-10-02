Imports System.Windows.Forms

Imports SpiceMaster.Core

Public Class frmDefineCombination

#Region "Variables"
	Dim m_Loaded As Boolean = False

	Friend MyWP As New My.SpiceMaster.Settings.FormOptions.WindowPos, MyWP_XML As Xml.XmlDocument = mdiMain.MyWP_XML

	Dim WithEvents MyDocument As Document = mdiMain.MyDocument
	Dim WithEvents MyDefinitions As DefinitionList = MyDocument.Definitions
	Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

	Dim WithEvents m_Item As CombinationReference

    Dim m_CombinationCommentsManager As New CommentListManager
    Dim m_ComList As CommentList

    Dim m_Result As Boolean = False, m_IsDifferent As Boolean = False
#End Region

#Region "MyDefinitions"
    Private Sub MyDefinitions_ItemAdded(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs) Handles MyDefinitions.ItemAdded
        Dim spc As Definition, nspct As Element.RefTag
        Dim lvi As ListViewItem

        If e.Source = ElementList.EventArgs.SourceEnum.ListItem Then
            spc = e.SourceObject

            If spc.Index = MyDefinitions.Count Then
                lvi = lvDefinitions.Items.Add(spc.FullText)
            Else
                nspct = MyDefinitions(spc.Index + 1).Tag
                lvi = lvDefinitions.Items.Insert(nspct.ListViewItem.Index, spc.FullText)
            End If

            lvi.SubItems.Add(spc.DisplayText)

            lvi.Tag = spc
            spc.Tag.ListViewItem = lvi

            lvi.Checked = True
        End If

        gbDefinitions.Text = "Definitions (" & MyDefinitions.Count & " items):"
    End Sub
#End Region

#Region "UI"
	Private Sub AlignMe()
		Left = (mdiMain.Width - Width) / 2
		Top = (mdiMain.Height - Height) / 2
	End Sub

    Private Sub CreateUI()
        If Not m_Loaded Then ReCreateUI()
    End Sub
    Private Sub ReCreateUI()
        Dim i As Integer
        Dim def As Definition, defr As DefinitionReference
        Dim lvi As ListViewItem, cbs As ComboBox.ObjectCollection

        m_Loaded = False

        If m_Item IsNot Nothing Then
            m_ComList = m_Item.Comments
        Else
            m_ComList = New CommentList
        End If
        m_CombinationCommentsManager.SetControls(m_ComList, lvCombinationComments, tsCombinationComments, gbCombinationComments)

        AlignMe()

        tbCombination.Text = ""
        With MyDocument.Spices
            cbs = cbSpice.Items
            cbs.Clear()
            For i = 1 To .Count
                With .Item(i)
                    cbs.Add(.Name)
                End With
            Next
        End With
        With MyDefinitions
            lvDefinitions.Items.Clear()
            For i = 1 To .Count
                def = .Item(i)
                lvi = lvDefinitions.Items.Add(def.FullText)
                lvi.SubItems.Add(def.DisplayText)
                lvi.Checked = False
                def.Tag.ListViewItem = lvi
                lvi.Tag = def
            Next
        End With

        If (m_Item IsNot Nothing) AndAlso (m_Item.Item IsNot Nothing) Then
            tbCombination.Text = m_Item.DisplayText
            cbSpice.SelectedIndex = m_Item.Item.Spice.Index - 1
            With m_Item.Item.Definitions
                For i = 1 To .Count
                    defr = .Item(i)
                    defr.Item.Tag.ListViewItem.Checked = True
                Next
            End With
        End If

        Localize(MyMUISources.Current)
        LoadSUS()

        m_Loaded = True
    End Sub

	Private Sub RefreshName()
		Dim i As Integer
		Dim s As String
		Dim def As Definition
		Dim lvi As ListViewItem

		If Not m_Loaded Then Return

        s = ""

		With lvDefinitions.Items
			For i = 1 To .Count
				lvi = .Item(i - 1)
				If lvi Is Nothing Then Exit For
				If lvi.Index >= 0 Then
					If lvi.Checked Then
						def = lvi.Tag
						s = s & " " & def.Name
					End If
				End If
			Next
		End With

        s = cbSpice.Text & s
        'If (m_Item Is Nothing) OrElse (m_Item.Item Is Nothing) Then
        '    s = cbSpice.Text & s
        'Else
        '    s = m_Item.Item.Spice.Name & s
        '    If m_Item.Item.Parent IsNot Nothing Then
        '        cbSpice.SelectedIndex = m_Item.Item.Spice.Index - 1
        '    End If
        'End If

		tbCombination.Text = s
    End Sub
#End Region

#Region "Form"
    Friend Sub ShowMe()
        m_Result = False
        m_Item = Nothing

        gbDefinitions.Parent = Me
        tlpMain.Visible = False
        gbDefinitions.Dock = DockStyle.Fill
        Text = "Definitions"
        Me.ShowDialog()
    End Sub

    Private Sub Form_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        CreateUI()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        cbSpice.Text = ""
        m_Item = Nothing
        m_Loaded = False
        SaveSUS()
    End Sub

	Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
		Select Case e.KeyCode
			'Case Keys.Enter
			'	butOK_Click(Nothing, Nothing)
			Case Keys.Escape
				Close()
		End Select
	End Sub

	Private Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click
		Dim i As Integer
        Dim ncmb As Combination = Nothing, ncmbr As CombinationReference = Nothing
		Dim el As Definition
        Dim lvi As ListViewItem

        'Make the new item
        If cbSpice.Text = "" Then
            MsgBox("Enter a name for the spice")
            Return
        End If

        'Add spice
        ncmb = New Combination(MyDocument.Spices.Add(cbSpice.Text, False))
        'Add definition references
        ncmb.Definitions.Clear()
        With MyDefinitions
            For i = 1 To .Count
                el = .Item(i)
                lvi = el.Tag.ListViewItem
                If lvi.Checked Then
                    ncmb.Definitions.Add(New DefinitionReference(el))
                End If
            Next
        End With
        'Add combination
        ncmb = ncmb.Spice.DerivedCombinations.Add(ncmb, False)

        'Chang if different
        m_IsDifferent = (m_Item Is Nothing) OrElse (m_Item.Item Is Nothing) OrElse (Not m_Item.Item.Equals(ncmb))
        If m_IsDifferent Then
            If m_Item Is Nothing Then m_Item = New CombinationReference
            m_Item.Item = ncmb
            m_Item.Comments.CopyFrom(m_ComList)
        End If

        m_Result = True

        Close()
    End Sub
#End Region

#Region "Definitions"
	Private Sub tsDefinitions_tsbNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsDefinitions_tsbNew.Click
		Dim sText As String = "", sAbbr As String = ""

		sAbbr = InputBox("Enter new item abbrevation:", "New item")

		If sAbbr <> "" Then
			sText = InputBox("Enter new item full text:", "New item")

			If (sText <> "") Then
				MyDefinitions.Add(sAbbr, sText)
			End If
		End If
	End Sub
	Private Sub tsDefinitions_tsbRename_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsDefinitions_tsbRename.Click
		Dim sText As String = "", sAbbr As String = ""
		Dim def As Definition

		If lvDefinitions.SelectedItems.Count = 0 Then
			Return
		End If

		def = lvDefinitions.SelectedItems(0).Tag

		sAbbr = InputBox("Enter new item abbrevation:", "Rename item", def.Name)

		If (sAbbr <> "") Then
			sText = InputBox("Enter new item full text:", "Rename item", def.FullText)

			def.Name = sAbbr
			If (sText <> "") Then
				If def.Name = sAbbr Then
					def.FullText = sText
				End If
			End If
		End If
	End Sub

	Private Sub lvDefinitions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvDefinitions.SelectedIndexChanged
		Dim b As Boolean = (lvDefinitions.SelectedItems.Count > 0)

		tsDefinitions_tsbRename.Enabled = b
		tsDefinitions_tsbDelete.Enabled = b
	End Sub
	Private Sub lvDefinitions_ItemChecked(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles lvDefinitions.ItemChecked
		RefreshName()
	End Sub

    Private Sub cbSpice_TextChanged(sender As Object, e As System.EventArgs) Handles cbSpice.TextChanged
        RefreshName()
    End Sub
#End Region
    'A dialog to select a spice with combinations
    Friend Function EditCombination(ByVal Item As CombinationReference, Optional ByRef WasDifferent As Boolean = False) As Boolean
        m_Result = False
        m_Item = Item

        tlpMain.Visible = True
        tlpMain.Controls.Add(gbDefinitions, 0, 1)
        gbDefinitions.Dock = DockStyle.Fill
        tlpMain.Dock = DockStyle.Fill
        Text = "Define Spice"
        cbSpice.DropDownStyle = ComboBoxStyle.DropDown
        If (Item Is Nothing) OrElse (Item.Item Is Nothing) OrElse (Item.Item.Spice Is Nothing) Then
            cbSpice.Text = ""
        Else
            cbSpice.Text = Item.Item.Spice.DisplayText
        End If
        Me.ShowDialog()
        cbSpice.SelectedIndex = -1
        cbSpice.Text = ""
        m_Item = Nothing

        WasDifferent = m_IsDifferent

        Return m_Result
    End Function

#Region "MUI"
	Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
		Text = Source("Forms", "DefineCombination", "FormCaption")
		gbSpice.Text = Source("General", "ItemsCaptions", "Spice") '
		butOK.Text = Source("General", "Buttons", "OK")
		gbDefinitions.Text = Source("General", "ListCaptions", "Definitions")
		tsDefinitions_tsbNew.Text = Source("General", "ItemsCommands", "New")
		tsDefinitions_tsbRename.Text = Source("General", "ItemsCommands", "Edit")
		tsDefinitions_tsbDelete.Text = Source("General", "ItemsCommands", "Remove")
		lvDefinitions_chName.Text = Source("General", "ListHeaders", "FullName")
		lvDefinitions_chAbbrevation.Text = Source("General", "ListHeaders", "Abbrevation")
	End Sub
#End Region

#Region "StartUp Settings"
	Sub LoadSUS()
		Dim Headers() As Integer = {80, 80, 80, 80}, ListsViews() As Integer = {View.Details, View.Details}

		With MyWP
			.XMLDocument = MyWP_XML : .FormName = "DefineCombination" : .Form = Nothing

			.Load(FormWindowState.Normal, Me.Left, Me.Top, Me.Width, Me.Height, , Headers, ListsViews)

			.GetList(lvDefinitions, 2, 0, 0)
			.GetList(lvCombinationComments, 2, 2, 0)
		End With
	End Sub
	Sub SaveSUS()
		With MyWP
			.SetList(lvDefinitions, 2, 0, 0)
			.SetList(lvCombinationComments, 2, 2, 0)

			.Save()
		End With
	End Sub
#End Region
End Class