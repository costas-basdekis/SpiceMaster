Imports System.Windows.Forms

Imports SpiceMaster.Core

Public Class frmDefineCombination

#Region "Variables"
	Dim m_Loaded As Boolean = False

	Dim WithEvents MyDocument As Document = mdiMain.MyDocument
	Dim WithEvents MyDefinitions As DefinitionList = MyDocument.Definitions

	Dim WithEvents m_Item As CombinationReference

	Dim m_CombinationCommentsManager As New CommentListManager

	Dim m_Result As Boolean = False
#End Region

#Region "MyDefinitions"
	Private Sub MyDefinitions_ListItemAdded(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs) Handles MyDefinitions.ListItemAdded, MyDefinitions.BatchListItemAdded
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
		Dim i As Integer
		Dim def As Definition, defr As DefinitionReference, coml As CommentList = Nothing
		Dim lvi As ListViewItem, cbs As ComboBox.ObjectCollection

		If m_Item IsNot Nothing Then
			coml = m_Item.Comments
		End If
		m_CombinationCommentsManager.SetControls(coml, lvCombinationComments, tsCombinationComments, gbCombinationComments, "Comments")

		AlignMe()

		If Not m_Loaded Then
			'tbSpice.Text = ""
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

			m_Loaded = True
		Else
			With lvDefinitions.Items
				For i = 1 To .Count
					lvi = .Item(i - 1)
					lvi.Checked = False
				Next
			End With
		End If

		If m_Item IsNot Nothing Then
			'tbSpice.Text = m_Item.DisplayText
			cbSpice.SelectedIndex = m_Item.Item.Spice.Index - 1
			With m_Item.Item.Definitions
				For i = 1 To .Count
					defr = .Item(i)
					defr.Item.Tag.ListViewItem.Checked = True
				Next
			End With
		End If
	End Sub

	Private Sub RefreshName()
		Dim i As Integer
		Dim s As String
		Dim def As Definition
		Dim lvi As ListViewItem

		If m_Item Is Nothing Then
			Return
		End If

		s = m_Item.Item.Spice.Name

		With lvDefinitions.Items
			For i = 1 To .Count
				lvi = .Item(i - 1)
				If lvi.Index >= 0 Then
					If lvi.Checked Then
						def = lvi.Tag
						s = s & " " & def.Name
					End If
				End If
			Next
		End With

		'tbSpice.Text = s
		If m_Item.Item.Parent IsNot Nothing Then
			cbSpice.SelectedIndex = m_Item.Item.Spice.Index - 1
		End If
	End Sub
#End Region

#Region "Form"
	Friend Sub ShowMe()
		m_Result = False
		m_Item = Nothing

		'gbSpice.Dock = DockStyle.None
		'gbSpice.Visible = False
		'gbDefinitions.Dock = DockStyle.Fill
		gbDefinitions.Parent = Me
		tlpMain.Visible = False
		gbDefinitions.Dock = DockStyle.Fill
		Text = "Definitions"
		Me.ShowDialog()
	End Sub

	Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		CreateUI()
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
		Dim ncmb As New Combination(m_Item.Item.Spice)
		Dim el As Definition
		Dim lvi As ListViewItem

		If m_Item.Item.Spice.Parent Is Nothing Then
			If cbSpice.Text = "" Then
				MsgBox("Enter a name for the spice")
				Exit Sub
			End If
			m_Item.Item.Spice._Name = cbSpice.Text
		End If

		With MyDefinitions
			For i = 1 To .Count
				el = .Item(i)
				lvi = el.Tag.ListViewItem
				If lvi.Checked Then
					ncmb.Definitions.Add(New DefinitionReference(el))
				End If
			Next
		End With

		If Not ncmb.Equals(m_Item.Item) Then
			ncmb = m_Item.Item.Spice.DerivedCombinations.Add(ncmb, False)
			m_Item.Item = ncmb
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

		If (sAbbr <> "") And (sAbbr <> def.Name) Then
			sText = InputBox("Enter new item full text:", "Rename item", def.FullText)

			If (sText <> "") Then
				def.Name = sAbbr
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
#End Region

#Region "Edit item"
	Friend Function EditCombination(ByVal Item As CombinationReference) As Boolean
		m_Result = False
		m_Item = Item


		'gbSpice.Visible = True
		'gbSpice.Dock = DockStyle.Top
		'gbDefinitions.Dock = DockStyle.Fill
		tlpMain.Visible = True
		tlpMain.Controls.Add(gbDefinitions, 0, 1)
		gbDefinitions.Dock = DockStyle.Fill
		tlpMain.Dock = DockStyle.Fill
		Text = "Define Spice"
		If Item.Item.Spice.Parent Is Nothing Then
			cbSpice.DropDownStyle = ComboBoxStyle.DropDown
			cbSpice.Text = Item.Item.Spice.DisplayText
		Else
			cbSpice.DropDownStyle = ComboBoxStyle.DropDownList
		End If
		Me.ShowDialog()

		Return m_Result
	End Function
#End Region
End Class