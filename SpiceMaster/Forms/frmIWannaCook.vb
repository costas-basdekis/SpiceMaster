Imports System.Windows.Forms

Imports SpiceMaster.Core

Public Class frmIWannaCook

#Region "Variables"
	Dim m_Loaded As Boolean = False

	Friend MyWP As New My.SpiceMaster.Settings.FormOptions.WindowPos, MyWP_XML As Xml.XmlDocument = mdiMain.MyWP_XML

	Dim WithEvents MyDocument As Document = mdiMain.MyDocument
    Dim WithEvents MyShelf As CombinationReferenceList = MyDocument.MyShelf
    Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources
    Dim m_ResultsText As String, m_ItemsText As String

    Dim WithEvents m_Foods As New FoodReferenceList, m_Combinations As New CombinationReferenceList
    Dim WithEvents m_Results As New CombinationReferenceList, m_SepResults As New CombinationReferenceList

    Dim m_SpicesManager As New CombinationReferenceListManager
    Dim m_FoodsManager As New FoodReferenceListManager
    Dim m_ResultsManager As New CombinationReferenceListManager, m_SepResultsManager As New CombinationReferenceListManager


#End Region

#Region "Document"
    Private Sub MyDocument_Loaded() Handles MyDocument.Loaded
        m_Foods = New FoodReferenceList
        m_Combinations = New CombinationReferenceList
        m_Results = New CombinationReferenceList

        CreateUI()
    End Sub
#End Region

#Region "UI"
    Private Sub CreateUI()
        If Not m_Loaded Then ReCreateUI()
    End Sub
    Private Sub ReCreateUI()
        Static sd As Long = 0
        'Apparently there is a API bug that randomly cannot add a new ToolStrip
        'No known solution found
        Try
            m_FoodsManager.SetControls(m_Foods, lvFoods, tsFoods, gbFoods, ManageFlags.DoNotAddIWC Or ManageFlags.ShowComments)
            m_SpicesManager.SetControls(m_Combinations, lvSpices, tsSpices, gbSpices, ManageFlags.DoNotAddIWC)
            m_ResultsManager.SetControls(Nothing, lvResults, tsResults, gbMainResults, ManageFlags.IsReadOnly Or ManageFlags.ShowComments)
            lvResults.Columns.Add("ot_fo", "Other forms found")
            m_SepResultsManager.SetControls(Nothing, lvSepResults, tsSepResults, gbSepResults, ManageFlags.IsReadOnly Or ManageFlags.ShowComments)
        Catch ex As Exception
            Hide()
            Return
        End Try

        m_FoodsManager.Clipboard = MyDocument.ClipBoard
        m_SpicesManager.Clipboard = MyDocument.ClipBoard
        m_ResultsManager.Clipboard = MyDocument.ClipBoard
        m_SepResultsManager.Clipboard = MyDocument.ClipBoard

        LoadSUS()
        Localize(MyMUISources.Current)

        m_Loaded = True
    End Sub
#End Region

#Region "Form"
    Friend Sub ShowMe()
        'Apparently there is a API bug that randomly cannot add a new ToolStrip
        'No known solution found
        Try
            PrivateShowMe()
        Catch ex As Exception
            PrivateShowMe()
        End Try
    End Sub
    Private Sub PrivateShowMe()
        If m_Loaded Then
            Visible = True
        Else
            If Not Visible Then Me.Show(mdiMain)
            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
            End If
            Me.Activate()
            Me.BringToFront()
            CreateUI()
        End If
    End Sub
    Private Sub frmIWannaCook_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        LoadSUS()
        Localize(MyMUISources.Current)
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSUS()
        m_Loaded = False
        m_Foods = New FoodReferenceList
        m_Combinations = New CombinationReferenceList
    End Sub

    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub tsShelf_MS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsShelf_MS.Click
        frmMyShelf.ShowMe()
    End Sub
    Private Sub tsShelf_SOMS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsShelf_SOMS.CheckedChanged
        Search()
    End Sub

    Private Sub tsShelf_IFPC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsShelf_IFPC.CheckedChanged
        Search()
    End Sub
    Private Sub tsShelf_ICPC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsShelf_ICPC.CheckedChanged
        Search()
    End Sub
#End Region

#Region "Search"
    Friend Sub Search()
        Dim i As Integer
        Dim fd As Food, fdl As FoodList
        Dim cmbrl As CombinationReferenceList
        Dim cmbr As CombinationReference
        Dim cmbrls As New Collection

        m_ResultsManager.List = Nothing

        m_Results.BatchStart()
        m_Results.Clear()
        m_SepResults.BatchStart()
        m_SepResults.Clear()

        If m_Foods.Count > 0 Then
            fd = m_Foods.Item(1).Item
            AddCombinationList(fd.Combinations)
            'Add food's parents combinations
            If tsShelf_IFPC.Checked Then
                Do
                    fdl = fd.Parent : If fdl Is Nothing Then Exit Do
                    fd = fdl.Parent : If fd Is Nothing Then Exit Do
                    AddCombinationList(fd.Combinations)
                Loop
            End If
        End If

        'Remove non-common foods' combinations
        For i = 2 To m_Foods.Count
            fd = m_Foods.Item(i).Item
            If tsShelf_IFPC.Checked Then
                cmbrls.Clear()
                cmbrls.Add(fd.Combinations)
                Do
                    fdl = fd.Parent : If fdl Is Nothing Then Exit Do
                    fd = fdl.Parent : If fd Is Nothing Then Exit Do
                    cmbrls.Add(fd.Combinations)
                Loop
                IntersectCombinationLists(cmbrls)
            Else
                IntersectCombinationList(fd.Combinations)
            End If
        Next

        'Remove combinations in criteria
        For i = 1 To m_Combinations.Count
            RemoveCombination(m_Combinations.Item(i))
        Next

        'Remove non-My Shelf
        If tsShelf_SOMS.Checked Then
            For i = m_Results.Count To 1 Step -1
                cmbr = m_Results.Item(i)
                If Not ItemInList(MyShelf, cmbr.Item) Then
                    RemoveCombination(cmbr)
                End If
            Next
        End If

        'Remove non-Combinations
        For i = 1 To m_Combinations.Count
            If tsShelf_ICPC.Checked Then
                cmbrls.Clear()
                cmbrls.Add(m_Combinations.Item(i).Item.Combinations)
                cmbrls.Add(m_Combinations.Item(i).Item.Spice.DerivedCombinations.Item(1).Combinations)
                IntersectCombinationLists(cmbrls)
            Else
                IntersectCombinationList(m_Combinations.Item(i).Item.Combinations)
            End If
        Next

        'Set to correct form combinations
        For i = 1 To m_Results.Count
            cmbr = m_Results.Item(i)
            cmbrl = cmbr.Tag.Extra
            If (cmbrl IsNot Nothing) AndAlso (cmbrl.Count = 1) AndAlso (Not ReferenceEquals(cmbr.Item, cmbrl.Item(1))) Then
                cmbr.Item = cmbrl.Item(1).Item
            End If
        Next

        m_SepResults.BatchEnd()
        m_Results.BatchEnd()

        UpdateResults()
    End Sub
    Private Function AddCombination(ByVal Item As CombinationReference) As CombinationReference
        Dim i As Integer
        Dim cmbr As CombinationReference
        Dim cmbrl As CombinationReferenceList

        With Item
            cmbr = New CombinationReference(.Item)
            'cmbr = m_Results.Add(cmbr, False)
            cmbr = m_Results.Add(New CombinationReference(Item.Item.Spice.DerivedCombinations(1)), False)
            With .Comments
                For i = 1 To .Count
                    With .Item(i)
                        cmbr.Comments.Add(.Text)
                    End With
                Next
            End With
        End With
        If cmbr.Tag.Extra Is Nothing Then
            cmbrl = New CombinationReferenceList
            cmbr.Tag.Extra = cmbrl
        Else
            cmbrl = cmbr.Tag.Extra
        End If
        cmbrl.Add(Item)

        Return cmbr
    End Function
    Private Sub AddCombinationList(ByVal Items As CombinationReferenceList)
        Dim i As Integer

        For i = 1 To Items.Count
            AddCombination(Items.Item(i))
        Next
    End Sub
    Private Sub IntersectCombinationList(Items As CombinationReferenceList)
        Dim i As Integer, bRemove As Boolean
        Dim cmbr As CombinationReference

        i = 1
        While i <= m_Results.Count
            cmbr = m_Results.Item(i)
            bRemove = True
            If ItemInList(Items, cmbr.Item) Then
                i += 1
            Else
                m_SepResults.Add(cmbr)
                cmbr.Remove()
            End If
        End While
    End Sub
    Private Sub IntersectCombinationLists(Lists As Collection)
        Dim i As Integer, j As Long, bRemove As Boolean
        Dim cmbr As CombinationReference, cmbrl As CombinationReferenceList

        i = 1
        While i <= m_Results.Count
            cmbr = m_Results.Item(i)
            bRemove = True
            For j = 1 To Lists.Count
                cmbrl = Lists(j)
                If ItemInList(cmbrl, cmbr.Item) Then
                    bRemove = False
                    Exit For
                End If
            Next
            If bRemove Then
                m_SepResults.Add(cmbr)
                cmbr.Remove()
            Else
                i += 1
            End If
        End While
    End Sub
    Private Function ItemInResults(ByVal Item As CombinationReference) As CombinationReference
        Return m_Results.Item(New CombinationReference(Item.Item.Spice.DerivedCombinations(1)))
    End Function
    Private Function RemoveCombination(ByVal Item As CombinationReference) As Boolean
        Dim cmbr As CombinationReference = ItemInResults(Item)
        If cmbr IsNot Nothing Then
            cmbr.Remove()
            Return True
        Else
            Return False
        End If
    End Function
    Private Function ItemInList(ByVal List As CombinationReferenceList, ByVal Item As Combination) As Boolean
        Dim i As Integer
        Dim cmbr As CombinationReference

        If List(Item) Is Nothing Then
            For i = 1 To List.Count
                cmbr = List(i)
                If ReferenceEquals(cmbr.Item.Spice, Item.Spice) Then Return True
            Next
        Else
            Return True
        End If

        Return False
    End Function

    Private Sub UpdateResults()
        Dim i As Integer, j As Integer, s As String
        Dim lvi As ListViewItem
        Dim cmbr As CombinationReference, fdr As FoodReference
        Dim cmbrl As CombinationReferenceList
        Dim lvg As ListViewGroup

        ShowMe()

        If lvResults.Groups.Count = 0 Then Return

        lvg = lvResults.Groups(0)

        m_ResultsManager.List = m_Results : m_ResultsManager.Clipboard = MyDocument.ClipBoard
        lvg.Items.AddRange(lvResults.Items)
        lvg.Header = "Perfect matches (" & lvg.Items.Count & ")"
        With lvResults.Items
            For i = 1 To .Count
                lvi = .Item(i - 1)
                cmbr = lvi.Tag
                If cmbr IsNot Nothing Then
                    cmbrl = cmbr.Tag.Extra
                    If cmbrl IsNot Nothing Then
                        If (cmbrl.Count - IIf(cmbrl(cmbr.Item) Is Nothing, 0, 1)) > 1 Then
                            lvi.ForeColor = Color.Red
                        ElseIf (cmbrl(cmbr.Item) IsNot Nothing) AndAlso (cmbrl.Count > 1) Then
                            lvi.ForeColor = Color.Orange
                        Else
                            cmbrl = Nothing
                        End If
                        If cmbrl IsNot Nothing Then
                            s = ""
                            For j = 1 To cmbrl.Count
                                cmbr = cmbrl.Item(j)
                                If cmbr.Item.Definitions.Count > 0 Then
                                    If s = "" Then
                                        s = "[" & cmbr.Item.Definitions.DisplayText & "]"
                                    Else
                                        s = s & ", [" & cmbr.Item.Definitions.DisplayText & "]"
                                    End If
                                End If
                            Next
                            If s <> "" Then
                                lvi.SubItems.Add(s)
                            End If
                        End If
                    End If
                End If
            Next
        End With

        lvSepResults.Items.Clear()
        lvSepResults.Groups.Clear()
        With m_Foods
            For i = 1 To .Count
                lvg = Nothing
                fdr = .Item(i)
                With fdr.Item.Combinations
                    For j = 1 To .Count
                        cmbr = m_SepResults(.Item(j).Item)
                        If cmbr IsNot Nothing Then
                            If lvg Is Nothing Then
                                lvg = lvSepResults.Groups.Add(CStr(i) & "_", fdr.DisplayText)
                            End If
                            lvi = lvg.Items.Add(cmbr.DisplayText)
                            lvi.SubItems.Add(cmbr.Comments.FullName)
                            lvSepResults.Items.Add(lvi)
                            lvi.Tag = cmbr
                        End If
                    Next
                End With
                If lvg IsNot Nothing Then
                    lvg.Header = fdr.DisplayText & " (" & lvg.Items.Count & ")"
                End If
            Next
        End With

        Localize(MyMUISources.Current)
    End Sub
#End Region

#Region "Criteria"
    Private Sub Criteria_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_Combinations.ItemAdded, m_Combinations.ItemChanged, m_Combinations.ItemRemoved, m_Combinations.BatchEnded, m_Foods.ItemAdded, m_Foods.ItemChanged, m_Foods.ItemRemoved, m_Foods.BatchEnded
        Search()
    End Sub

    Private Sub MyShelf_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles MyShelf.ItemAdded, MyShelf.ItemChanged, MyShelf.ItemRemoved, MyShelf.BatchEnded
        If Visible Then
            Search()
        End If
    End Sub
#End Region

#Region "Add items"
    Friend Sub AddItem(ByVal TreeNode As TreeNode)
        If (TreeNode Is Nothing) OrElse (TreeNode.Tag Is Nothing) Then
            Return
        End If

        If TypeOf TreeNode.Tag Is Combination Then
            Dim cmb As Combination = TreeNode.Tag
            m_Combinations.Add(New CombinationReference(cmb))
        ElseIf TypeOf TreeNode.Tag Is Food Then
            Dim fd As Food = TreeNode.Tag
            m_Foods.Add(New FoodReference(fd))
        End If
    End Sub
    Friend Sub AddItems(ByVal ListItems As ListView.SelectedListViewItemCollection)
        Dim lvis As ListView.SelectedListViewItemCollection
        Dim lvi As ListViewItem
        Dim catr As FoodReference
        Dim cmb As Combination, cmbr As CombinationReference

        lvis = ListItems

        If lvis.Count > 0 Then
            m_Combinations.BatchStart()
            m_Foods.BatchStart()

            For Each lvi In lvis
                If (lvi.Tag IsNot Nothing) Then
                    If (TypeOf lvi.Tag Is FoodReference) Then
                        catr = lvi.Tag
                        m_Foods.Add(catr)
                    Else
                        cmb = Nothing
                        If (TypeOf lvi.Tag Is Combination) Then
                            cmb = lvi.Tag
                        ElseIf (TypeOf lvi.Tag Is CombinationReference) Then
                            cmbr = lvi.Tag
                            cmb = cmbr.Item
                        End If
                        If cmb IsNot Nothing Then
                            cmbr = m_Combinations.Add(New CombinationReference(cmb))
                        End If
                    End If
                End If
            Next

            m_Foods.BatchEnd()
            m_Combinations.BatchEnd()
        End If
    End Sub
#End Region

#Region "StartUp Settings"
    Sub LoadSUS()
        Dim Splits() As Integer = {50, 50, 50}, Headers() As Integer = {80, 80, 80, 80, 80, 80, 80, 80}, ListsViews() As Integer = {View.Details, View.Details, View.Details, View.Details}

        With MyWP
            .XMLDocument = MyWP_XML : .FormName = "IWC" : .Form = Me

            .Load(FormWindowState.Normal, Location.X, Location.Y, Size.Width, Size.Height, Splits, Headers, ListsViews)

            'Apparently the splitter does not update the contained child unless manually resized
            '.GetSplitter(scMain, 0)
            .GetSplitter(scManage, 1)
            .GetList(lvFoods, 2, 0, 1)
            .GetList(lvSpices, 1, 2, 0)
            .GetList(lvResults, 3, 3, 2)
            .GetList(lvSepResults, 2, 6, 3)
        End With

        tsShelf_SOMS.CheckState = IIf(My.Settings.IWC_SOMS, CheckState.Checked, CheckState.Unchecked)
        tsShelf_IFPC.CheckState = IIf(My.Settings.IWC_IFPC, CheckState.Checked, CheckState.Unchecked)
        tsShelf_ICPC.CheckState = IIf(My.Settings.IWC_ICPC, CheckState.Checked, CheckState.Unchecked)
    End Sub
    Sub SaveSUS()
        With MyWP
            .SetSplitter(scMain, 0)
            .SetSplitter(scManage, 1)
            .SetList(lvFoods, 2, 0, 1)
            .SetList(lvSpices, 1, 2, 0)
            .SetList(lvResults, 3, 3, 2)
            .SetList(lvSepResults, 2, 6, 3)

            .Save()
        End With

        My.Settings.IWC_SOMS = tsShelf_SOMS.Checked
        My.Settings.IWC_IFPC = tsShelf_IFPC.Checked
        My.Settings.IWC_ICPC = tsShelf_ICPC.Checked
    End Sub
#End Region

#Region "MUI"
    Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
        Text = Source("Forms", "IWannaCook", "FormCaption")
        tsShelf_IFPC.Text = Source("Forms", "IWannaCook", "General", "IncludeFoodsParentsCriteria")
        tsShelf_ICPC.Text = Source("Forms", "IWannaCook", "General", "IncludeCombinationsParentCriteria")
        tsShelf_SOMS.Text = Source("Forms", "IWannaCook", "General", "ShowOnlyMyShelf")
        tsShelf_MS.Text = Source("Forms", "IWannaCook", "General", "OpenMyShelf")
        gbCriteria.Text = Source("Forms", "IWannaCook", "General", "SearchCriteriaLabel")
        m_ResultsText = Source("Forms", "IWannaCook", "General", "ResultsLabel")
        m_ItemsText = Source("General", "ListCaptions", "Items")

        gbResults.Text = m_ResultsText & " (" & lvResults.Items.Count & " " & m_ItemsText & " (+ " & lvSepResults.Items.Count & ")):"
    End Sub
#End Region
End Class