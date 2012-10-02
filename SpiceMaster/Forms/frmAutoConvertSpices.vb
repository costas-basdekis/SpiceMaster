Imports System.Windows.Forms

Imports SpiceMaster.Core

Public Class frmAutoConvertSpices

#Region "Variables & Declarations"
	Structure ConvertTo
		Friend Name As String
		Friend Definitions As DefinitionReferenceList
		Friend Conjunction As CombinationReference.ConnectWithEnum
	End Structure
	Structure Conversion
		Friend ItemFrom As Spice
		Friend ConvertTo As CombinationReferenceList
		Friend WillConvert As Boolean
		Friend ListViewItem As ListViewItem
	End Structure

	Friend MyWP As New My.SpiceMaster.Settings.FormOptions.WindowPos, MyWP_XML As Xml.XmlDocument = mdiMain.MyWP_XML

	Private MyDocument As Document = mdiMain.MyDocument
	Private MySpices As SpiceList = MyDocument.Spices
	Private MyDefinitions As DefinitionList = mdiMain.MyDocument.Definitions
	Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources
	Private m_Items() As Conversion, m_Count As Integer, m_ConvertCount As Integer, m_SelectedCount As Integer
	Private mThisItem As Integer = -1

	Private WithEvents m_CombinationsManager As New CombinationReferenceListManager

	Private WithEvents m_ProccessHook As frmProgress.ProccessHook
	Private WithEvents m_ProccessHookBegin As New frmProgress.ProccessHookBegin
	Dim m_ConvertOnlySelected As Boolean = False

	Private bUpdatingDefsLV As Boolean = False
#End Region

#Region "UI"
	Private Sub PopulateItems()
		Dim i As Integer, s As String
		Dim spc As Spice
		Dim lvi As ListViewItem, lvs As ListView.ListViewItemCollection

		m_Count = MySpices.Count
		m_ConvertCount = 0
		m_SelectedCount = 0
		ReDim m_Items(m_Count)

		If m_Count = 0 Then
			Exit Sub
		End If

		lvs = lvMain.Items

		lvs.Clear()

		With MySpices
			For i = 1 To .Count
				spc = .Item(i)
				With m_Items(i)
					.ItemFrom = spc
					LookForConjuctions(m_Items(i))
					lvi = lvs.Add(spc.DisplayText)
					lvi.Tag = m_Items(i)
					If .WillConvert Then
						m_ConvertCount += 1
						'm_SelectedCount += 1
						If .ConvertTo.Count = 1 Then
							s = Trim(.ConvertTo.Item(1).Item.Definitions.DisplayText)
							lvi.SubItems.Add("to " & .ConvertTo.Item(1).Item.Spice.Name & IIf(s <> "", " as '" & s & "'", ""))
						Else
							lvi.SubItems.Add("to " & .ConvertTo.Count & " items")
						End If
						If Not m_ConvertOnlySelected Then
							lvi.Checked = True
						End If
					Else
						lvi.SubItems.Add("")
						lvi.Checked = False
					End If
					lvi.Group = lvMain.Groups(IIf(.WillConvert, 0, 1))
					.ListViewItem = lvi
				End With
			Next
		End With

        m_CombinationsManager.SetControls(Nothing, lvCombinations, tsCombinations, gbCombinations, ManageFlags.DoNotAddIWC Or ManageFlags.ShowComments)

		UpdateText()
	End Sub
	Private Sub LookForConjuctions(ByRef Item As Conversion)
		Dim i As Integer, j As Integer, cj As CombinationReference.ConnectWithEnum = CombinationReference.ConnectWithEnum.None, ncj As CombinationReference.ConnectWithEnum
		Dim s As String, s1 As String, s2 As String = ""
		Dim el As ConvertTo
		Dim col As New Collection
		Dim spc As Spice, cmbr As CombinationReference

		s = Item.ItemFrom.DisplayText
		col.Clear()
		Do While s <> ""
			ncj = CombinationReference.ConnectWithEnum.None
			i = InStr(s, " κ' ", CompareMethod.Text) : If i > 3 Then ncj = CombinationReference.ConnectWithEnum.And : s2 = Mid(s, i + 4)
			j = InStr(s, "+", CompareMethod.Text) : If (j > 3) And ((i < 1) Or (j < i)) Then i = j : ncj = CombinationReference.ConnectWithEnum.And : s2 = Mid(s, i + 1)
			j = InStr(s, " ή ", CompareMethod.Text) : If (j > 3) And ((i < 1) Or (j < i)) Then i = j : ncj = CombinationReference.ConnectWithEnum.Or : s2 = Mid(s, i + 3)
			If ncj <> CombinationReference.ConnectWithEnum.None Then
				s1 = Mid(s, 1, i - 1)
				s = s2
			Else
				s1 = s
				s = ""
			End If
			el = New ConvertTo
			With el
				.Conjunction = cj
				.Definitions = ParseItem(s1)
				.Name = s1
				col.Add(el)
			End With
			cj = ncj
		Loop

		With Item
			.ConvertTo = New CombinationReferenceList
			For i = 1 To col.Count
				With CType(col(i), ConvertTo)
					spc = New Spice
					spc._Name = Trim(.Name)
					cmbr = New CombinationReference(New Combination(spc))
					cmbr.Item.Definitions.CopyFrom(.Definitions)
					cmbr.Comments.CopyFrom(Item.ItemFrom.Comments)
					cmbr.ConnectWith = .Conjunction
				End With
				cmbr = .ConvertTo.Add(cmbr)
				If cmbr IsNot Nothing Then
					cmbr.ConnectWith = CType(col(i), ConvertTo).Conjunction
				End If
			Next
			With .ConvertTo
				If .Count = 1 Then
					With .Item(1)
						If .Item.Definitions.Count = 0 Then
							.Remove()
						End If
					End With
				End If
			End With
			.WillConvert = .ConvertTo.Count > 0
		End With
	End Sub
	Private Function ParseItem(ByRef Item As String) As DefinitionReferenceList
		Dim i As Integer, j As Integer, s As String = Item
		Dim def As Definition
		Dim c As New DefinitionReferenceList(New Combination(New Spice))

		With MyDefinitions
			Do
				For i = 1 To .Count
					def = .Item(i)
					If s.EndsWith(" " & def.DisplayText) Then
						j = s.LastIndexOf(" " & def.DisplayText)
						If j > 3 Then
							c.Add(New DefinitionReference(def))
							s = s.Substring(0, j)
							Continue Do
						Else
							Exit Do
						End If
					End If
				Next
				Exit Do
			Loop
		End With

		Item = s

		Return c
	End Function

	Private Sub SelectItem(ByVal Index As Integer)
		If (Index >= 1) And (Index <= m_Count) Then
			m_CombinationsManager.List = m_Items(Index).ConvertTo
		Else
			m_CombinationsManager.List = Nothing
		End If
	End Sub
	Private Sub UpdateText()
		Dim s As String, bOWC As Boolean
		Dim lvi1 As ListViewItem

		If lvMain.SelectedItems.Count > 0 Then
			lvi1 = lvMain.SelectedItems(0)
			If lvi1.Index >= 0 Then
				With m_Items(lvi1.Index + 1)
					bOWC = .WillConvert
					If .WillConvert Then
						m_ConvertCount -= 1
						If lvi1.Checked Then
							m_SelectedCount -= 1
						End If
					End If
					While .ConvertTo.Count >= 1
						If .ConvertTo.Item(1).DisplayText = .ItemFrom.DisplayText Then
							.ConvertTo.Item(1).Remove()
						Else
							Exit While
						End If
					End While
					.WillConvert = (.ConvertTo.Count >= 1)
					If .WillConvert Then
						m_ConvertCount += 1
						If .ConvertTo.Count = 1 Then
							s = Trim(.ConvertTo.Item(1).Item.Definitions.DisplayText)
							.ListViewItem.SubItems(1).Text = "to " & .ConvertTo.Item(1).Item.Spice.Name & IIf(s <> "", " as '" & s, "") & "'"
						Else
							.ListViewItem.SubItems(1).Text = "to " & .ConvertTo.Count & " items"
						End If
						.ListViewItem.Group = lvMain.Groups(0)
						If Not (bOWC OrElse m_ConvertOnlySelected OrElse .ListViewItem.Checked) Then
							.ListViewItem.Checked = True
						ElseIf .ListViewItem.Checked Then
							m_SelectedCount += 1
						End If
					Else
						.ListViewItem.Checked = False
						.ListViewItem.SubItems(1).Text = ""
						.ListViewItem.Group = lvMain.Groups(1)
					End If
					.ListViewItem.EnsureVisible()
					lvMain.Sort()
				End With
			End If
		End If

		UpdateButtons()
	End Sub
	Private Sub UpdateButtons()
		gbMain.Text = "Found " & m_Count & " items (" & m_SelectedCount & "/" & m_ConvertCount & " to convert):"
		tsMain_tsbConvertAll.Enabled = (m_ConvertCount > 0)
		tsMain_tsbConvertSelected.Enabled = (m_SelectedCount > 0)
		tsMain_tsbCheckAll.Enabled = tsMain_tsbConvertAll.Enabled
		tsMain_tsbCheckNone.Enabled = tsMain_tsbConvertAll.Enabled
	End Sub
#End Region

#Region "Form"
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Localize(MyMUISources.Current)
        PopulateItems()
        SelectItem(0)
        LoadSUS()
    End Sub
	Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		SaveSUS()
	End Sub
	Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
		If e.KeyCode = Keys.Escape Then
			Me.Close()
		End If
	End Sub
#End Region

#Region "Items"
	Private Sub lvMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMain.SelectedIndexChanged
		With lvMain.SelectedItems
			If .Count = 0 Then
				SelectItem(0)
				Exit Sub
			End If

			SelectItem(.Item(0).Index + 1)
		End With
	End Sub
	Private Sub lvMain_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lvMain.ItemCheck
		Dim c As Conversion = m_Items(e.Index + 1) 'lvMain.Items(e.Index).Tag

		If e.CurrentValue = CheckState.Checked Then
			m_SelectedCount -= 1
		End If

		e.NewValue = IIf(c.WillConvert, e.NewValue, CheckState.Unchecked)

		If e.NewValue = CheckState.Checked Then
			m_SelectedCount += 1
		End If

		UpdateButtons()
	End Sub

	Private Sub m_CombinationsManager_ListEvent(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs) Handles m_CombinationsManager.ListEvent
		UpdateText()
	End Sub

	Private Sub tsMain_tsbCheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsMain_tsbCheckAll.Click
		Dim i As Integer

		For i = 1 To m_Count
			With m_Items(i)
				.ListViewItem.Checked = .WillConvert
			End With
		Next
	End Sub
	Private Sub tsMain_tsbCheckNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsMain_tsbCheckNone.Click
		Dim i As Integer

		For i = 1 To m_Count
			With m_Items(i)
				.ListViewItem.Checked = False
			End With
		Next
	End Sub
#End Region

#Region "Convert"
	Private Sub tsMain_tsbConvertAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsMain_tsbConvertAll.Click
		m_ConvertOnlySelected = False
		If Not frmProgress.ShowMe(Me, "Converting...", m_ProccessHookBegin, True) Then
			MsgBox("Cannot open Progress Window! It is not important but contact the programmer for that", MsgBoxStyle.Exclamation)
            Convert()
            frmProgress.Close()
		End If
	End Sub
	Private Sub tsMain_tsbConvertSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsMain_tsbConvertSelected.Click
		m_ConvertOnlySelected = True
		If Not frmProgress.ShowMe(Me, "Converting...", m_ProccessHookBegin, True) Then
			MsgBox("Cannot open Progress Window! It is not important but contact the programmer for that", MsgBoxStyle.Exclamation)
			Convert()
            frmProgress.Close()
        End If
	End Sub

	Private Sub m_ProccessHook_BeginOperation(ByVal ProccessHook As frmProgress.ProccessHook) Handles m_ProccessHookBegin.BeginOperation
		m_ProccessHook = ProccessHook
		Convert()
	End Sub

	Private Sub Convert()
		Dim i As Integer, j As Integer, k As Integer
		Dim spc As Spice
        Dim cmb As Combination, ncmb As Combination, cmbs As CombinationList
		Dim m_Total As Integer = IIf(m_ConvertOnlySelected, m_SelectedCount, m_ConvertCount)

		MyDocument.BatchStart()
		MySpices.BatchStart()

		m_ProccessHook.ProgressCurrent(m_Total) = 0
		m_ProccessHook.Category = "Combinations"

        For i = 1 To m_Count
            Debug.Print(i)
            With m_Items(i)
                If m_ProccessHookBegin.CancelInvoked Then Exit For
                If .WillConvert AndAlso IIf(m_ConvertOnlySelected, .ListViewItem.Checked, True) Then
                    m_ProccessHook.Item = .ItemFrom.Name
                    cmbs = .ItemFrom.DerivedCombinations
                    cmbs.BatchStart()

                    'k=Count to 2
                    For k = .ConvertTo.Count To 2 Step -1
                        ncmb = .ConvertTo(k).Item
                        spc = MySpices.Add(ncmb.Spice.Name, False)
                        If spc Is Nothing Then
                            Continue For
                        End If
                        ncmb._Spice = spc
                        j = 1
                        While j <= cmbs.Count
                            cmb = cmbs.Item(j)
                            If (cmb.Foods.Count > 0) And (cmb.Combinations.Count > 0) Then
                                ConvertCombination(cmb, .ConvertTo(k))
                            Else
                                j += 1
                            End If
                        End While
                    Next
                    'k=1
                    k = 1
                    ncmb = .ConvertTo(k).Item
                    spc = MySpices.Add(ncmb.Spice.Name, False)
                    If spc Is Nothing Then
                        Continue For
                    End If
                    ncmb._Spice = spc
                    j = 1
                    While j <= cmbs.Count
                        cmb = cmbs.Item(j)
                        If (cmb.Foods.Count > 0) And (cmb.Combinations.Count > 0) Then
                            ConvertCombination(cmb, .ConvertTo(k))
                        Else
                            j += 1
                        End If
                    End While

                    cmbs.BatchEnd()
                    .ItemFrom.Remove()
                    If m_ConvertOnlySelected Then
                        m_SelectedCount -= 1
                    Else
                        m_ConvertCount -= 1
                    End If

                    m_ProccessHook.ProgressCurrent(m_Total) = m_Total - IIf(m_ConvertOnlySelected, m_SelectedCount, m_ConvertCount)
                End If
            End With
        Next

        MySpices.BatchEnd()
        MyDocument.BatchEnd()

        PopulateItems()
	End Sub
    Private Sub ConvertCombination(ByVal OldCombination As Combination, ByVal NewCombination As CombinationReference)
        Dim i As Integer
        Dim combr As CombinationReference, combr2 As CombinationReference
        Dim ncmb As Combination = New Combination(NewCombination.Item.Spice)
        Dim defrs As DefinitionReferenceList

        'Create new combination
        defrs = ncmb.Definitions
        'With old combination combinations
        With OldCombination.Definitions
            For i = 1 To .Count
                defrs.Add(.Item(i))
            Next
        End With
        'And with new definition list
        With NewCombination.Item.Definitions
            For i = 1 To .Count
                defrs.Add(.Item(i))
            Next
        End With
        'Add it
        ncmb = NewCombination.Item.Spice.DerivedCombinations.Add(ncmb, False)
        'Convert old's combinations
        With OldCombination.Combinations
            For i = .Count To 1 Step -1
                With .Item(i).Item.Combinations
                    combr = .Item(OldCombination)
                    If combr IsNot Nothing Then
                        combr2 = .Item(ncmb)
                        If combr2 IsNot Nothing Then
                            combr2.Remove()
                            combr.Comments.CopyFrom(combr2.Comments, False)
                        End If
                        combr.Item = ncmb
                        combr.Comments.CopyFrom(NewCombination.Comments, False)
                    End If
                End With
            Next
        End With
        'Convert old's foods
        With OldCombination.Foods
            For i = .Count To 1 Step -1
                With .Item(1).Item.Combinations
                    combr = .Item(OldCombination)
                    If combr IsNot Nothing Then
                        combr.Item = ncmb
                        combr.Comments.CopyFrom(NewCombination.Comments, False)
                    End If
                End With
            Next
        End With
    End Sub
	Private Sub ConvertInsertCombination(ByVal OldCombination As Combination, ByVal NewCombination As CombinationReference)
		Dim i As Integer
		Dim combr As CombinationReference, combr2 As CombinationReference
		Dim ncmb As Combination = New Combination(NewCombination.Item.Spice)
		Dim defrs As DefinitionReferenceList

		'Create new combination
		defrs = ncmb.Definitions
		'With old combination combinations
		With OldCombination.Definitions
			For i = 1 To .Count
				defrs.Add(.Item(i))
			Next
		End With
		'And with new definition list
		With NewCombination.Item.Definitions
			For i = 1 To .Count
				defrs.Add(.Item(i))
			Next
		End With
		'Add it
		ncmb = NewCombination.Item.Spice.DerivedCombinations.Add(ncmb, False)
		combr2 = New CombinationReference(ncmb)
		combr2.Comments.CopyFrom(NewCombination.Comments)
		combr2.ConnectWith = NewCombination.ConnectWith
		'Convert old's combinations
		With OldCombination.Combinations
			For i = .Count To 1 Step -1
				With .Item(i).Item.Combinations
					combr = .Item(OldCombination)
					If combr IsNot Nothing Then
						combr = .Insert(combr.Index, True, combr2)
					End If
				End With
			Next
		End With
		'Convert old's foods
		With OldCombination.Foods
			For i = .Count To 1 Step -1
				With .Item(i).Item.Combinations
					combr = .Item(OldCombination)
					If combr IsNot Nothing Then
						combr = .Insert(combr.Index, True, combr2)
					End If
				End With
			Next
		End With
		'With OldCombination.Combinations
		'	For i = .Count To 1 Step -1
		'		With .Item(i).Item.Combinations
		'			combr = .Item(OldCombination)
		'			If combr IsNot Nothing Then
		'				combr2 = New CombinationReference(ncmb)
		'				combr2.Comments.CopyFrom(NewCombination.Comments)
		'				combr2.ConnectWith = NewCombination.ConnectWith
		'				combr = .Insert(combr.Index, True, combr2)
		'			End If
		'		End With
		'	Next
		'End With
		'With OldCombination.Foods
		'	For i = .Count To 1 Step -1
		'		With .Item(1).Item.Combinations
		'			combr = .Item(OldCombination)
		'			If combr IsNot Nothing Then
		'				combr2 = New CombinationReference(ncmb)
		'				combr2.Comments.CopyFrom(NewCombination.Comments)
		'				combr2.ConnectWith = NewCombination.ConnectWith
		'				combr = .Insert(combr.Index, True, combr2)
		'			End If
		'		End With
		'	Next
		'End With
	End Sub
#End Region

#Region "StartUp Settings"
	Sub LoadSUS()
		Dim Splits() As Integer = {40}, Headers() As Integer = {80, 80, 80, 80}, ListsViews() As Integer = {View.Details, View.Details}

		With MyWP
            .XMLDocument = MyWP_XML : .FormName = "AutoConv" : .Form = Me

            .Load(FormWindowState.Normal, mdiMain.DisplayRectangle.Width / 4, mdiMain.ClientRectangle.Height / 4, mdiMain.DisplayRectangle.Width / 2, mdiMain.ClientRectangle.Height / 1.5, Splits, Headers, ListsViews)

			.GetSplitter(scMain, 0)
			.GetList(lvMain, 2, 0)
			.GetList(lvCombinations, 2, 2, 0)
		End With
	End Sub
	Sub SaveSUS()
        With MyWP
            .SetSplitter(scMain, 0)
            .SetList(lvMain, 2, 0)
            .SetList(lvCombinations, 2, 2, 0)

            .Save()
        End With
	End Sub
#End Region

#Region "MUI"
	Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
		Text = Source("Forms", "AutoConvertSpices", "FormCaption")
		tsMain_tsbConvertAll.Text = Source("Forms", "AutoConvertSpices", "General", "ConvertAll")
		tsMain_tsbConvertSelected.Text = Source("Forms", "AutoConvertSpices", "General", "ConvertSelected")
		tsMain_tsbCheckAll.Text = Source("Forms", "AutoConvertSpices", "General", "SelectAll")
		tsMain_tsbCheckNone.Text = Source("Forms", "AutoConvertSpices", "General", "SelectNone")
		lvMain_chItem.Text = Source("Forms", "AutoConvertSpices", "General", "ItemLabel")
		lvMain_chComments.Text = Source("Forms", "AutoConvertSpices", "General", "ConversionLabel")
	End Sub
#End Region
End Class