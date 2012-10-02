Namespace Core
    Class ListManager(Of ElementType As {NamedElement, New}, ListType As NamedList(Of ElementType), ReferenceElementType As {Element, New}, ReferenceType As {TypedReference(Of ReferenceElementType), New}, ReferenceListType As {ReferenceList(Of ReferenceElementType, ReferenceType), New})

#Region "Events"
#Region "List"
        Protected Overridable Sub List_ItemAdded(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs) Handles m_List.ItemAdded
            '
        End Sub
        Protected Overridable Sub List_ItemChanged(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs) Handles m_List.ItemChanged
            '
        End Sub
        Protected Overridable Sub List_ItemRemoved(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs) Handles m_List.ItemRemoved
            Dim el As ElementType

            If TypeOf e.SourceObject Is ElementType Then
                el = e.SourceObject
                With el.Tag.TreeNode
                    .Remove()
                    .Tag = Nothing
                End With
                el.Tag.TreeNode = Nothing
            End If
        End Sub
#End Region

#Region "TreeView"
        Private Sub TV_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles m_TV.AfterSelect
            SelectUpdate()
        End Sub
        Private Sub TV_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles m_TV.ItemDrag
            Dim tn As TreeNode = e.Item
            m_TV.DoDragDrop(tn, DragDropEffects.Copy Or DragDropEffects.Move)
        End Sub

        Protected Overridable Sub TV_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles m_TV.BeforeLabelEdit
            '
        End Sub
        Protected Overridable Sub TV_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles m_TV.AfterLabelEdit
            e.CancelEdit = True
            If e.Node.Parent IsNot Nothing Then
                Return
            End If

            If (e.Label IsNot Nothing) AndAlso (e.Label <> "") Then
                Dim el As ElementType = e.Node.Tag
                Dim doc As Document = el.Document
                If doc IsNot Nothing Then
                    doc.BatchStart()
                End If
                el.Name = e.Label
                If doc IsNot Nothing Then
                    doc.BatchEnd()
                End If
            End If
        End Sub
        'Allow F2 to edit
        Private Sub TV_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles m_TV.KeyDown
            If m_TV.SelectedNode IsNot Nothing Then
                Select Case e.KeyCode
                    Case Keys.F2
                        m_TV.SelectedNode.BeginEdit()
                    Case Keys.Delete
                        Remove_Click(Nothing, Nothing)
                End Select
            End If
        End Sub
#End Region

#Region "Buttons"
        Private Sub IWC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tIWC.Click, m_mIWC.Click
            If m_List Is Nothing Then
                Return
            End If

            frmIWannaCook.AddItem(m_TV.SelectedNode)
        End Sub
        Protected Overridable Sub Remove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tRemove.Click, m_mRemove.Click
            '
        End Sub
#End Region
#End Region

#Region "Variables"
        Protected WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources
        Protected m_Text As String, m_ItemsText As String

        Protected WithEvents m_List As ListType
        'The form items
        Protected WithEvents m_TV As TreeView, m_TB As ToolStrip, m_Menu As ContextMenuStrip, m_GB As GroupBox
        'Toolstrip buttons
        Protected WithEvents m_tIWC As ToolStripButton, m_tSep1 As ToolStripSeparator, m_tAdd As ToolStripButton, m_tEdit As ToolStripButton
        Protected WithEvents m_tSep2 As ToolStripSeparator, m_tCopy As ToolStripButton, m_tCut As ToolStripButton, m_tPaste As ToolStripButton, m_tRemove As ToolStripButton
        Protected WithEvents m_mIWC As ToolStripMenuItem, m_mSep1 As ToolStripSeparator, m_mAdd As ToolStripMenuItem, m_mEdit As ToolStripMenuItem
        Protected WithEvents m_mSep2 As ToolStripSeparator, m_mCopy As ToolStripMenuItem, m_mCut As ToolStripMenuItem, m_mPaste As ToolStripMenuItem, m_mRemove As ToolStripMenuItem

        Protected WithEvents m_History As NavigationHistory(Of ReferenceElementType, ReferenceType, ReferenceListType)
        Protected WithEvents m_CombinationsRLM As CombinationReferenceListManager

        Protected WithEvents m_ClipBoard As Clipboard
#End Region

#Region "Init/Terminate"
        Sub New()
            Localize(MyMUISources.Current)
        End Sub
#End Region

#Region "Methods"
        Public Function SetControls(ByVal List As ListType, ByVal TreeView As TreeView, ByVal ToolBar As ToolStrip, ByVal GroupBox As GroupBox, ByVal History As NavigationHistory(Of ReferenceElementType, ReferenceType, ReferenceListType), ByVal CombinationsRLM As CombinationReferenceListManager) As Boolean
            m_List = Nothing : m_ClipBoard = Nothing
            m_TV = Nothing
            m_TB = Nothing
            m_GB = Nothing
            m_Text = ""
            m_Menu = Nothing

            m_tIWC = Nothing : m_mIWC = Nothing
            m_tSep1 = Nothing : m_mSep1 = Nothing
            m_tAdd = Nothing : m_mAdd = Nothing
            m_tEdit = Nothing : m_mEdit = Nothing
            m_tCopy = Nothing : m_mCopy = Nothing
            m_tCut = Nothing : m_mCut = Nothing
            m_tPaste = Nothing : m_mPaste = Nothing
            m_tRemove = Nothing : m_mRemove = Nothing

            m_History = Nothing
            m_CombinationsRLM = Nothing

            If (List Is Nothing) OrElse (TreeView Is Nothing) OrElse (ToolBar Is Nothing) OrElse (GroupBox Is Nothing) OrElse (History Is Nothing) OrElse (CombinationsRLM Is Nothing) Then
                Return False
            End If

            m_List = List : m_ClipBoard = m_List.Document.ClipBoard
            m_TV = TreeView
            m_TB = ToolBar
            m_GB = GroupBox
            m_Menu = New ContextMenuStrip

            m_History = History
            m_CombinationsRLM = CombinationsRLM

            m_TV.Nodes.Clear()
            m_TB.Items.Clear()
            m_Menu.Items.Clear()

            AddToolItem(m_tIWC, m_mIWC, "", My.Resources.IMAGE_APPLICATION)
            m_tIWC.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText

            AddToolSep(m_tSep1, m_mSep1)

            AddToolItem(m_tAdd, m_mAdd, "", My.Resources.IMAGE_LIST_ADD)
            m_tAdd.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            AddToolItem(m_tEdit, m_mEdit, "", My.Resources.IMAGE_LIST_EDIT)

            AddToolSep(m_tSep2, m_mSep2)

            AddToolItem(m_tCopy, m_mCopy, "", My.Resources.IMAGE_LIST_COPY)
            AddToolItem(m_tCut, m_mCut, "", My.Resources.IMAGE_LIST_CUT)
            AddToolItem(m_tPaste, m_mPaste, "", My.Resources.IMAGE_LIST_PASTE)
            AddToolItem(m_tRemove, m_mRemove, "", My.Resources.IMAGE_LIST_REMOVE)

            m_TV.ContextMenuStrip = m_Menu

            Localize(MyMUISources.Current)

            Populate()

            Return True
        End Function
        Protected Sub AddToolItem(ByRef ToolItem As ToolStripItem, ByRef MenuItem As ToolStripMenuItem, ByVal Text As String, Optional ByVal Image As System.Drawing.Image = Nothing)
            ToolItem = New ToolStripButton(Text)
            With ToolItem
                .Image = Image
                If .Image Is Nothing Then
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                Else
                    .ImageTransparentColor = Color.Magenta
                    .DisplayStyle = ToolStripItemDisplayStyle.Image
                End If
            End With
            m_TB.Items.Add(ToolItem)
            AddMenuItem(MenuItem, ToolItem)
        End Sub
        Protected Sub InsertToolItem(ByRef ToolItem As ToolStripItem, ToolItemIndex As Integer, ByRef MenuItem As ToolStripMenuItem, MenuItemIndex As Integer, ByVal Text As String, Optional ByVal Image As System.Drawing.Image = Nothing)
            ToolItem = New ToolStripButton(Text)
            With ToolItem
                .Image = Image
                If .Image Is Nothing Then
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                Else
                    .ImageTransparentColor = Color.Magenta
                    .DisplayStyle = ToolStripItemDisplayStyle.Image
                End If
            End With
            m_TB.Items.Insert(ToolItemIndex, ToolItem)
            InsertMenuItem(MenuItem, MenuItemIndex, ToolItem)
        End Sub
        Private Sub AddToolSep(ByRef ToolItem As ToolStripSeparator, ByRef MenuItem As ToolStripSeparator)
            ToolItem = New ToolStripSeparator
            m_TB.Items.Add(ToolItem)
            AddMenuSep(MenuItem)
        End Sub
        Private Sub AddMenuItem(ByRef MenuItem As ToolStripMenuItem, ByVal ToolItem As ToolStripItem)
            MenuItem = New ToolStripMenuItem(ToolItem.Text)
            With MenuItem
                .Image = ToolItem.Image
                .ImageTransparentColor = ToolItem.ImageTransparentColor
            End With
            m_Menu.Items.Add(MenuItem)
        End Sub
        Private Sub InsertMenuItem(ByRef MenuItem As ToolStripMenuItem, MenuItemIndex As Integer, ByVal ToolItem As ToolStripItem)
            MenuItem = New ToolStripMenuItem(ToolItem.Text)
            With MenuItem
                .Image = ToolItem.Image
                .ImageTransparentColor = ToolItem.ImageTransparentColor
            End With
            m_Menu.Items.Insert(MenuItemIndex, MenuItem)
        End Sub
        Private Sub AddMenuSep(ByRef MenuItem As ToolStripSeparator)
            MenuItem = New ToolStripSeparator
            m_Menu.Items.Add(MenuItem)
        End Sub
        Protected Sub LocalizeToolItem(ByVal ToolItem As ToolStripItem, ByVal MenuItem As ToolStripMenuItem, ByVal Text As String)
            If ToolItem IsNot Nothing Then ToolItem.Text = Text : MenuItem.Text = Text
        End Sub
        'Fill the tree or list view
        Public Overridable Sub Populate()
            If m_TV Is Nothing Then
                Return
            End If

            m_TV.Nodes.Clear()

            RefreshButtons()

            If m_List Is Nothing Then
                Return
            End If
        End Sub
        Public Sub RefreshButtons()
            Dim b As Boolean = False

            If m_TV Is Nothing Then
                Return
            End If

            m_GB.Enabled = True
            m_TB.Enabled = True
            m_TV.Enabled = True
            SelectUpdate()

            m_GB.Text = m_Text & " (" & m_List.Count & " " & m_ItemsText & "):"
        End Sub

        Public Overridable Sub SelectItem(ByVal Item As ReferenceElementType)
            '
        End Sub
        'Update enabled property of certain toolstrip and context menu items based on selection status
        Protected Overridable Sub SelectUpdate(Optional ByVal AutoSelect As Boolean = True)
            Dim b As Boolean = False
            Dim el As ReferenceElementType = Nothing

            If (m_TV Is Nothing) Then
                Return
            End If

            If AutoSelect Then
                b = (m_TV.SelectedNode IsNot Nothing) AndAlso (m_TV.SelectedNode.Tag IsNot Nothing)
            End If

            m_tIWC.Enabled = b : m_mIWC.Enabled = m_tIWC.Enabled
            m_tAdd.Enabled = (m_List IsNot Nothing) : m_mAdd.Enabled = m_tAdd.Enabled
            m_tEdit.Enabled = b : m_mEdit.Enabled = m_tEdit.Enabled

            m_tCut.Enabled = b : m_mCut.Enabled = m_tCut.Enabled
            m_tCopy.Enabled = b : m_mCopy.Enabled = m_tCopy.Enabled
            m_tRemove.Enabled = b : m_mRemove.Enabled = m_tRemove.Enabled

            If b Then
                el = m_TV.SelectedNode.Tag
            End If

            m_History.Navigate(el)
        End Sub
#End Region

        Protected Overridable Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
            m_ItemsText = Source("General", "ListCaptions", "Items")
            RefreshButtons()
            LocalizeToolItem(m_tIWC, m_mIWC, Source("General", "ItemsCommands", "CookIt"))
            LocalizeToolItem(m_tAdd, m_mAdd, Source("General", "ItemsCommands", "New"))
            LocalizeToolItem(m_tEdit, m_mEdit, Source("General", "ItemsCommands", "Edit"))
            LocalizeToolItem(m_tCut, m_mCut, Source("General", "ItemsCommands", "Cut"))
            LocalizeToolItem(m_tCopy, m_mCopy, Source("General", "ItemsCommands", "Copy"))
            LocalizeToolItem(m_tPaste, m_mPaste, Source("General", "ItemsCommands", "Paste"))
            LocalizeToolItem(m_tRemove, m_mRemove, Source("General", "ItemsCommands", "Remove"))
        End Sub
    End Class

    Class SpiceListManager
        Inherits ListManager(Of Spice, SpiceList, Combination, CombinationReference, CombinationReferenceList)

#Region "Events"
#Region "List"
        Protected Overrides Sub List_ItemAdded(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs)
            Dim spc As Spice, cmb As Combination, nspct As Spice.RefTag
            Dim tn As TreeNode

            If e.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                spc = CType(e.SourceObject, Spice)
                cmb = spc.DerivedCombinations(1)

                If spc.Index = m_List.Count Then
                    tn = m_TV.Nodes.Add(spc.DisplayText)
                Else
                    nspct = m_List(spc.Index + 1).Tag
                    tn = m_TV.Nodes.Insert(nspct.TreeNode.Index, spc.DisplayText)
                End If

                tn.Tag = cmb
                cmb.Tag.TreeNode = tn
                spc.Tag.TreeNode = tn
            End If
        End Sub
        Protected Overrides Sub List_ItemChanged(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs)
            Dim ie As ElementList.EventArgs, cmb As Combination, spc As Spice
            Dim tn As TreeNode

            If e.InnerEvent IsNot Nothing Then
                With e.InnerEvent
                    Select Case .Action
                        Case Element.EventArgs.ActionEnum.DerivedCombinations
                            If .Source = Element.EventArgs.SourceEnum.EventArgs Then
                                ie = .SourceObject
                                Select Case ie.Action
                                    Case ElementList.EventArgs.ActionEnum.ItemAdded
                                        If ie.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                                            cmb = ie.SourceObject
                                            spc = cmb.Spice

                                            tn = spc.Tag.TreeNode.Nodes.Add(cmb.DisplayText)

                                            tn.Tag = cmb
                                            cmb.Tag.TreeNode = tn

                                            RefreshButtons()
                                        End If
                                    Case ElementList.EventArgs.ActionEnum.ItemRemoved
                                        If ie.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                                            cmb = ie.SourceObject
                                            cmb.Tag.TreeNode.Remove()
                                        End If
                                End Select
                            End If
                    End Select
                End With
            End If
        End Sub
        Protected Overrides Sub List_ItemRemoved(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs) ' Handles m_List.ListItemRemoved, m_List.BatchListItemRemoved
            Dim spc As Spice

            If e.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                spc = e.SourceObject
                With spc.Tag
                    .TreeNode.Remove()
                End With
                RefreshButtons()
            End If
        End Sub
#End Region

#Region "TreeView"
        Protected Overrides Sub TV_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs)
            If e.Node.Parent IsNot Nothing Then
                e.CancelEdit = True
            End If
        End Sub
        Protected Overrides Sub TV_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs)
            e.CancelEdit = True
            If e.Node.Parent IsNot Nothing Then
                Return
            End If

            If (e.Label IsNot Nothing) AndAlso (e.Label <> "") Then
                Dim cmb As Combination = e.Node.Tag

                cmb.Document.BatchStart()
                cmb.Spice.Name = e.Label
                cmb.Document.BatchEnd()
            End If
        End Sub

        Private Sub TV_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles m_TV.DragEnter, m_TV.DragOver
            Dim tnTo As TreeNode
            Dim cmbTo As Combination

            If m_List Is Nothing Then
                e.Effect = DragDropEffects.None
                Return
            End If

            tnTo = m_TV.HitTest(m_TV.PointToClient(New Point(e.X, e.Y))).Node
            If tnTo Is Nothing Then
                Return
            Else
                cmbTo = tnTo.Tag
            End If

            If CombinationReferenceListManager.CanDrop(e, cmbTo.Combinations) OrElse FoodReferenceListManager.CanDrop(e, cmbTo.Foods) Then
                e.Effect = DragDropEffects.Copy
                Return
            End If

            e.Effect = DragDropEffects.None
        End Sub
        Private Sub TV_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles m_TV.DragDrop
            Dim tnTo As TreeNode
            Dim cmb As Combination

            If m_List Is Nothing Then
                Return
            End If

            tnTo = m_TV.HitTest(m_TV.PointToClient(New Point(e.X, e.Y))).Node
            If tnTo Is Nothing Then Return

            cmb = tnTo.Tag

            TV_DragOver(sender, e)

            Select Case e.Effect
                Case DragDropEffects.Copy
                    If cmb IsNot Nothing Then
                        CombinationReferenceListManager.DragDrop(e, cmb.Combinations)
                        FoodReferenceListManager.DragDrop(e, cmb.Foods)
                    End If
            End Select
        End Sub
#End Region

#Region "Buttons"
        Private Sub Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tAdd.Click, m_mAdd.Click
            Dim nmcb As New CombinationReference
            Dim spc As Spice, cmb As Combination

            If frmDefineCombination.EditCombination(nmcb) Then
                If nmcb.Item.Tag.TreeNode IsNot Nothing Then
                    nmcb.Item.Tag.TreeNode.TreeView.SelectedNode = nmcb.Item.Tag.TreeNode
                Else
                    spc = m_List.Add(nmcb.Item.Spice.Name, False)
                    cmb = spc.DerivedCombinations.Add(nmcb.Item, False)
                    m_TV.SelectedNode = cmb.Tag.TreeNode
                End If
            End If
        End Sub
        Private Sub Edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tEdit.Click, m_mEdit.Click
            If m_List Is Nothing Then
                Return
            End If

            m_TV.SelectedNode.BeginEdit()
        End Sub
        Private Sub Convert_Click(sender As Object, e As System.EventArgs) Handles m_tConvert.Click, m_mConvert.Click
            Dim cmb As Combination, ccmb As Combination, cmbl As CombinationList
            Dim ctcmb As Combination
            Dim cmbr As New CombinationReference
            Dim doc As Document

            If m_List Is Nothing Then Return
            If m_TV Is Nothing Then Return
            If m_TV.SelectedNode Is Nothing Then Return

            cmb = m_TV.SelectedNode.Tag

            If frmDefineCombination.EditCombination(cmbr) Then
                doc = cmb.Document
                doc.BatchStart()
                cmb.Convert(cmbr.Item)
                If (cmb.Definitions IsNot Nothing) AndAlso (cmb.Definitions.Count = 0) Then
                    If cmb.Spice.DerivedCombinations.Count > 1 Then
                        If MsgBox("Do you want to also change all its forms?", vbYesNo) = vbYes Then
                            cmbl = cmb.Spice.DerivedCombinations
                            While cmbl.Count >= 2
                                ccmb = cmbl(2)
                                ctcmb = New Combination(cmbr.Item.Spice)
                                ctcmb.Definitions.CopyFrom(ccmb.Definitions)
                                ctcmb.Definitions.CopyFrom(cmbr.Item.Definitions, False)
                                ccmb.Convert(ctcmb)
                            End While
                            cmb.Remove()
                        End If
                    Else
                        cmb.Remove()
                    End If
                End If
                doc.BatchEnd()
            End If
        End Sub
        Protected Overrides Sub Remove_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles m_tRemove.Click, m_mRemove.Click
            Dim cmb As Combination, spc As Spice

            If m_List Is Nothing Then
                Return
            End If

            cmb = m_TV.SelectedNode.Tag
            If MsgBox("Are you sure to remove this item?", MsgBoxStyle.OkCancel, "Remove item") = MsgBoxResult.Ok Then
                Dim doc As Document = cmb.Document

                If doc IsNot Nothing Then doc.BatchStart()
                If cmb.Definitions.Count = 0 Then
                    spc = cmb.Spice
                    spc.Remove()
                Else
                    cmb.Combinations.Clear()
                    cmb.Foods.Clear()
                End If
                If doc IsNot Nothing Then doc.BatchEnd()
            End If
        End Sub

        Private Sub Copy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCopy.Click, m_mCopy.Click
            Dim comb As Combination

            If m_List Is Nothing Then
                Return
            End If

            comb = m_TV.SelectedNode.Tag

            m_ClipBoard.CombOp = Clipboard.Operation.Copy
            m_ClipBoard.Comb = comb

            m_ClipBoard.CombsOp = Clipboard.Operation.Copy
            With m_ClipBoard.Combs
                .BatchStart()
                .Clear()
                .Add(New CombinationReference(comb))
                .BatchEnd()
            End With
        End Sub
        Private Sub Cut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCut.Click, m_mCut.Click
            Dim comb As Combination

            If m_List Is Nothing Then
                Return
            End If

            comb = m_TV.SelectedNode.Tag

            m_ClipBoard.CombOp = Clipboard.Operation.Cut
            m_ClipBoard.Comb = comb

            m_ClipBoard.CombsOp = Clipboard.Operation.Copy
            With m_ClipBoard.Combs
                .BatchStart()
                .Clear()
                .Add(New CombinationReference(comb))
                .BatchEnd()
            End With
        End Sub
        Private Sub Paste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tPaste.Click, m_mPaste.Click
            Dim comb As Combination = Nothing
            Dim cmbr As CombinationReference = Nothing

            If m_List Is Nothing Then
                Return
            End If

            comb = m_TV.SelectedNode.Tag

            'Prompt user to select definitions

            cmbr = New CombinationReference(comb)
            If Not frmDefineCombination.EditCombination(cmbr) Then Return
            'Add to the spice
            comb = cmbr.Item
            comb = comb.Spice.DerivedCombinations.Add(comb, False)

            'NoOp
            If comb Is m_ClipBoard.Comb Then
                m_ClipBoard.CombOp = Clipboard.Operation.Copy
                Return
            End If

            'Copy info
            comb.Combinations.CopyFrom(m_ClipBoard.Comb.Combinations, False)
            comb.Foods.CopyFrom(m_ClipBoard.Comb.Foods, False)

            'Clear original if Cut
            If m_ClipBoard.CombOp = Clipboard.Operation.Cut Then
                m_ClipBoard.Comb.Remove()

                'Others can paste too
                m_ClipBoard.CombOp = Clipboard.Operation.Copy
                m_ClipBoard.Comb = comb

                m_ClipBoard.CombsOp = Clipboard.Operation.Copy
                m_ClipBoard.Combs.Add(comb)
            End If

            m_TV.SelectedNode = comb.Tag.TreeNode
        End Sub
#End Region

#Region "Clipboard"
        Private Sub m_ClipBoard_ItemChanged(ByVal Item As Clipboard.ItemType, ByVal InnerEvent As Object) Handles m_ClipBoard.ItemChanged
            If m_tPaste IsNot Nothing Then
                m_tPaste.Enabled = (m_ClipBoard.Comb IsNot Nothing) : m_mPaste.Enabled = m_tPaste.Enabled
            End If
        End Sub
#End Region
        'Respond to History requests and events
#Region "History"
        Private Sub History_ActivateItem(ByVal Item As Combination) Handles m_History.ActivateItem
            m_History.NavigatingByHistory = True
            m_TV.SelectedNode = Item.Tag.TreeNode
            m_History.NavigatingByHistory = False
        End Sub

        Private Sub History_FindAllElements(ByVal Name As String, ByRef List As CombinationReferenceList) Handles m_History.FindAllElements
            List = m_List.FindAll(Name)
        End Sub
        Private Sub History_SearchElements(ByVal Name As String, ByRef List As CombinationReferenceList) Handles m_History.SearchElements
            List = m_List.Search(Name)
        End Sub
#End Region
#End Region

#Region "Variables"
        Protected WithEvents m_FoodRLM As FoodReferenceListManager
        Protected WithEvents m_tConvert As ToolStripButton
        Protected WithEvents m_mConvert As ToolStripMenuItem
#End Region

#Region "Init/Terminate"
        Sub New()
            Localize(MyMUISources.Current)
        End Sub
#End Region

#Region "Methods"
        Public Shadows Function SetControls(ByVal List As SpiceList, ByVal TreeView As TreeView, ByVal ToolBar As ToolStrip, ByVal GroupBox As GroupBox, ByVal History As NavigationHistory(Of Combination, CombinationReference, CombinationReferenceList), ByVal CombinationsRLM As CombinationReferenceListManager, ByVal FoodRLM As FoodReferenceListManager) As Boolean
            m_FoodRLM = Nothing

            If FoodRLM Is Nothing Then
                MyBase.SetControls(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Return False
            End If

            m_FoodRLM = FoodRLM

            If Not MyBase.SetControls(List, TreeView, ToolBar, GroupBox, History, CombinationsRLM) Then
                m_FoodRLM = Nothing
                Return False
            End If

            m_tConvert = Nothing : m_mConvert = Nothing
            InsertToolItem(m_tConvert, m_TB.Items.IndexOf(m_tEdit) + 1, m_mConvert, m_Menu.Items.IndexOf(m_mEdit) + 1, "")

            Localize(MyMUISources.Current)

            Return True
        End Function
        'Fill the tree view
        Public Overrides Sub Populate()
            MyBase.Populate()

            Dim spc As Spice, cmb As Combination
            Dim tns As TreeNodeCollection, tns2 As TreeNodeCollection, tn As TreeNode

            If m_TV Is Nothing Then
                Return
            End If

            tns = m_TV.Nodes
            tns.Clear()

            With m_List
                For i As Integer = 1 To .Count
                    spc = .Item(i)
                    cmb = spc.DerivedCombinations(1)
                    tn = tns.Add(cmb.DisplayText)
                    tn.Tag = cmb
                    cmb.Tag.TreeNode = tn
                    spc.Tag.TreeNode = tn
                    tns2 = tn.Nodes
                    With spc.DerivedCombinations
                        For j As Integer = 2 To .Count
                            cmb = .Item(j)
                            tn = tns2.Add(cmb.DisplayText)
                            tn.Tag = cmb
                            cmb.Tag.TreeNode = tn
                        Next
                    End With
                Next i
            End With
        End Sub

        Public Overrides Sub SelectItem(ByVal Item As Combination)
            If m_List Is Nothing Then
                Return
            End If

            m_TV.SelectedNode = Item.Tag.TreeNode
        End Sub
        Protected Overrides Sub SelectUpdate(Optional ByVal AutoSelect As Boolean = True)
            Dim b As Boolean = False

            MyBase.SelectUpdate(AutoSelect)
            m_tPaste.Enabled = (m_ClipBoard.Comb IsNot Nothing) : m_mPaste.Enabled = m_tPaste.Enabled

            If (m_TV Is Nothing) Then
                Return
            End If

            If AutoSelect Then
                b = (m_TV.SelectedNode IsNot Nothing) AndAlso (m_TV.SelectedNode.Tag IsNot Nothing)
            End If

            If b Then
                Dim cmb As Combination = m_TV.SelectedNode.Tag

                m_CombinationsRLM.List = cmb.Combinations
                m_FoodRLM.List = cmb.Foods
            Else
                m_CombinationsRLM.List = Nothing
                m_FoodRLM.List = Nothing
            End If
        End Sub
#End Region

        Protected Overrides Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
            MyBase.Localize(Source)
            m_Text = Source("General", "ListCaptions", "Spices")
            LocalizeToolItem(m_tConvert, m_mConvert, Source("General", "ItemsCommands", "Convert"))
            RefreshButtons()
        End Sub
    End Class
    Class FoodListManager
        Inherits ListManager(Of Food, FoodList, Food, FoodReference, FoodReferenceList)

#Region "Events"
#Region "List"
        Protected Overrides Sub List_ItemAdded(ByVal sender As Object, ByVal e As Core.ElementList.EventArgs)
            Dim tn As TreeNode
            Dim p As FoodList, fd As Food

            If e.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                fd = e.SourceObject
                p = fd.Parent
                If (p.Parent Is Nothing) OrElse (Not (TypeOf p.Parent Is Food)) Then
                    tn = m_TV.Nodes("root")
                Else
                    fd = p.Parent
                    tn = fd.Tag.TreeNode
                End If
                fd = e.SourceObject
                tn = tn.Nodes.Insert(fd.Index - 1, fd.Name)
                fd.Tag.TreeNode = tn
                tn.Tag = fd
            End If
        End Sub
#End Region

#Region "TreeView"
        Protected Overrides Sub TV_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs)
            If e.Node.Parent Is Nothing Then
                e.CancelEdit = True
            End If
        End Sub
        Protected Overrides Sub TV_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs)
            e.CancelEdit = True
            If e.Node.Parent Is Nothing Then
                Return
            End If

            If (e.Label IsNot Nothing) AndAlso (e.Label <> "") Then
                Dim fd As Food = e.Node.Tag

                fd.Document.BatchStart()
                fd.Name = e.Label
                fd.Document.BatchEnd()
            End If
        End Sub

        Private Sub TV_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles m_TV.DragEnter, m_TV.DragOver
            Dim tnTo As TreeNode, fdTo As Food = Nothing, fdlTo As FoodList

            If m_List Is Nothing Then
                e.Effect = DragDropEffects.None
                Return
            End If

            tnTo = m_TV.HitTest(m_TV.PointToClient(New Point(e.X, e.Y))).Node
            If (tnTo Is Nothing) OrElse (tnTo.Parent Is Nothing) Then
                fdlTo = m_List.Document.Foods
            Else
                fdTo = tnTo.Tag
                fdlTo = fdTo.Foods
            End If

            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode") Then
                Dim tn As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
                If (tn.Tag IsNot Nothing) Then
                    If (TypeOf tn.Tag Is Food) Then
                        Dim fdCur As Food = tn.Tag
                        If tn.Parent Is Nothing Then
                            e.Effect = DragDropEffects.None
                        Else
                            If (Not ReferenceEquals(fdCur.Parent, fdlTo)) AndAlso (Not fdlTo.HasForParent(fdCur)) Then
                                If e.KeyState And 8 Then
                                    e.Effect = DragDropEffects.Copy
                                Else
                                    e.Effect = DragDropEffects.Move
                                End If
                            Else
                                e.Effect = DragDropEffects.None
                            End If
                        End If
                    ElseIf (TypeOf tn.Tag Is Combination) Then
                        If fdTo Is Nothing Then
                            e.Effect = DragDropEffects.None
                        Else
                            e.Effect = DragDropEffects.Copy
                        End If
                    End If
                End If
                Return
            ElseIf (fdTo IsNot Nothing) AndAlso (e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection")) Then
                Dim lvis As ListView.SelectedListViewItemCollection
                Dim lvi As ListViewItem
                Dim rel As CombinationReference, el As Combination

                lvis = CType(e.Data.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection"), ListView.SelectedListViewItemCollection)

                If lvis.Count = 0 Then
                    e.Effect = DragDropEffects.None
                    Return
                End If

                el = Nothing
                With m_List
                    For Each lvi In lvis
                        If (lvi.Tag IsNot Nothing) Then
                            If (TypeOf lvi.Tag Is Combination) Then
                                el = lvi.Tag
                            ElseIf (TypeOf lvi.Tag Is CombinationReference) Then
                                rel = lvi.Tag
                                el = rel.Item
                            End If
                            If el IsNot Nothing Then
                                If TypeOf m_List.Parent Is Combination Then
                                    Dim el2 As Combination = m_List.Parent
                                    If (el2 Is Nothing) OrElse (Not ReferenceEquals(el.Spice, el2.Spice)) Then
                                        e.Effect = DragDropEffects.Copy
                                        Return
                                    End If
                                Else
                                    e.Effect = DragDropEffects.Copy
                                    Return
                                End If
                            End If
                        End If
                    Next
                End With
            End If

            e.Effect = DragDropEffects.None
        End Sub
        Private Sub TV_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles m_TV.DragDrop
            Dim tnTo As TreeNode, fd As Food = Nothing, fdl As FoodList = Nothing

            If m_List Is Nothing Then
                Return
            End If

            tnTo = m_TV.HitTest(m_TV.PointToClient(New Point(e.X, e.Y))).Node
            If (tnTo Is Nothing) OrElse (tnTo.Parent Is Nothing) Then
                fdl = m_List.Document.Foods
            Else
                fd = tnTo.Tag
                fdl = fd.Foods
            End If

            TV_DragOver(sender, e)

            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
                Dim tnCur As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode), fdcur As Food = tnCur.Tag
                Select Case e.Effect
                    Case DragDropEffects.Move
                        If (e.KeyState = 4) OrElse (MsgBox("Are you sure to move this item?", MsgBoxStyle.OkCancel, "Move item") = MsgBoxResult.Ok) Then
                            With fdl.Document
                                .BatchStart()
                                fdcur.Parent = fdl
                                .BatchEnd()
                            End With
                        End If
                    Case DragDropEffects.Copy
                        With fdl.Document
                            .BatchStart()
                            fd = fdl.Add(fdcur.Name, False)
                            fd.Combinations.CopyFrom(fdcur.Combinations, False)
                            fd.Foods.CopyFrom(fdcur.Foods, False)
                            .BatchEnd()
                        End With
                End Select
            Else
                Select Case e.Effect
                    Case DragDropEffects.Copy
                        If fd IsNot Nothing Then
                            CombinationReferenceListManager.DragDrop(e, fd.Combinations)
                        End If
                End Select
            End If
        End Sub
#End Region

#Region "Buttons"
        Private Sub Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tAdd.Click, m_mAdd.Click
            Dim s As String = ""
            Dim el As Food
            Dim n As TreeNode

            If m_List Is Nothing Then
                Return
            End If

            s = InputBox("Enter the new item name:", "New item")
            If s = "" Then
                Return
            End If

            If m_TV.SelectedNode IsNot Nothing AndAlso m_TV.SelectedNode.Parent IsNot Nothing Then
                Dim fd As Food = m_TV.SelectedNode.Tag

                el = fd.Foods.Add(s)
            Else
                el = m_List.Add(s)
            End If

            If el IsNot Nothing Then
                n = el.Tag.TreeNode

                If n IsNot Nothing Then
                    m_TV.SelectedNode = n
                End If
            End If
        End Sub
        Private Sub Edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tEdit.Click, m_mEdit.Click
            Dim s As String = ""
            Dim fd As Food

            If m_List Is Nothing Then
                Return
            End If

            fd = m_TV.SelectedNode.Tag

            s = fd.Name
            s = InputBox("Enter the new item name:", "Rename", s)
            If s <> "" Then
                fd.Name = s
            End If
        End Sub
        Protected Overrides Sub Remove_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles m_tRemove.Click, m_mRemove.Click
            Dim fd As Food

            If m_List Is Nothing Then
                Return
            End If

            fd = m_TV.SelectedNode.Tag
            If MsgBox("Are you sure to remove this item?", MsgBoxStyle.OkCancel, "Remove item") = MsgBoxResult.Ok Then
                Dim doc As Document = fd.Document

                If doc IsNot Nothing Then doc.BatchStart()
                fd.Remove()
                If doc IsNot Nothing Then doc.BatchEnd()
            End If
        End Sub

        Private Sub Cop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCopy.Click, m_mCopy.Click
            Dim fd As Food

            If m_List Is Nothing Then
                Return
            End If

            fd = m_TV.SelectedNode.Tag

            m_ClipBoard.FoodOp = Clipboard.Operation.Copy
            m_ClipBoard.Food = fd

            m_ClipBoard.FoodsOp = Clipboard.Operation.Copy
            With m_ClipBoard.Foods
                .BatchStart()
                .Clear()
                .Add(New FoodReference(fd))
                .BatchEnd()
            End With
        End Sub
        Private Sub Cut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCut.Click, m_mCut.Click
            Dim fd As Food

            If m_List Is Nothing Then
                Return
            End If

            fd = m_TV.SelectedNode.Tag

            m_ClipBoard.FoodOp = Clipboard.Operation.Cut
            m_ClipBoard.Food = fd

            m_ClipBoard.FoodsOp = Clipboard.Operation.Copy
            With m_ClipBoard.Foods
                .BatchStart()
                .Clear()
                .Add(New FoodReference(fd))
                .BatchEnd()
            End With
        End Sub
        Private Sub Paste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tPaste.Click, m_mPaste.Click
            Dim fd As Food = Nothing, fdl As FoodList
            Dim doc As Document

            If m_List Is Nothing Then
                Return
            End If

            fd = m_TV.SelectedNode.Tag
            With m_ClipBoard.Food
                If fd Is Nothing Then
                    If .Parent Is m_List Then Return
                    fdl = m_List
                Else
                    If Not fd.CanHaveAsChild(m_ClipBoard.Food) Then
                        If m_ClipBoard.Food Is fd Then Return
                        MsgBox("Cannot move parent to child.")
                        Return
                    End If
                    fdl = fd.Foods
                End If

                doc = .Document
                If m_ClipBoard.FoodOp = Clipboard.Operation.Cut Then
                    doc.BatchStart()
                    m_ClipBoard.Food.Parent = fdl
                    doc.BatchEnd()

                    m_ClipBoard.FoodOp = Clipboard.Operation.Copy
                ElseIf m_ClipBoard.FoodOp = Clipboard.Operation.Copy Then
                    doc.BatchStart()
                    fd = fdl.Add(.Name, False)
                    fd.Combinations.CopyFrom(.Combinations, False)
                    fd.Foods.CopyFrom(.Foods, False)
                    doc.BatchEnd()
                End If
            End With

            m_TV.SelectedNode = fd.Tag.TreeNode
        End Sub
#End Region

#Region "Clipboard"
        Private Sub m_ClipBoard_ItemChanged(ByVal Item As Clipboard.ItemType, ByVal InnerEvent As Object) Handles m_ClipBoard.ItemChanged
            If m_tPaste IsNot Nothing Then
                m_tPaste.Enabled = (m_ClipBoard.Food IsNot Nothing) : m_mPaste.Enabled = m_tPaste.Enabled
            End If
        End Sub
#End Region

#Region "History"
        Private Sub History_ActivateItem(ByVal Item As Food) Handles m_History.ActivateItem
            m_History.NavigatingByHistory = True
            m_TV.SelectedNode = Item.Tag.TreeNode
            m_History.NavigatingByHistory = False
        End Sub

        Private Sub History_FindAllElements(ByVal Name As String, ByRef List As FoodReferenceList) Handles m_History.FindAllElements
            List = m_List.FindAll(Name)
        End Sub
        Private Sub History_SearchElements(ByVal Name As String, ByRef List As FoodReferenceList) Handles m_History.SearchElements
            List = m_List.Search(Name)
        End Sub
#End Region
#End Region

#Region "Init/Terminate"
        Sub New()
            Localize(MyMUISources.Current)
        End Sub
#End Region

#Region "Methods"
        Public Overrides Sub Populate()
            MyBase.Populate()

            Dim tns As TreeNodeCollection, tn As TreeNode

            If m_TV Is Nothing Then
                Return
            End If

            tns = m_TV.Nodes
            tns.Clear()
            tn = tns.Add("root", m_Text)
            CreateFoodList(m_List, tn)
            tns.Item(0).Expand()
        End Sub
        Private Sub CreateFoodList(ByVal FdList As FoodList, ByVal ParentNode As TreeNode)
            Dim i As Integer, fd As Food
            Dim tns As TreeNodeCollection = ParentNode.Nodes, tn As TreeNode

            With FdList
                For i = 1 To .Count
                    fd = .Item(i)
                    tn = tns.Add(fd.Name)
                    tn.Tag = fd
                    fd.Tag.TreeNode = tn
                    CreateFoodList(fd.Foods, tn)
                Next
            End With
        End Sub

        Public Overrides Sub SelectItem(ByVal Item As Food)
            If m_List Is Nothing Then
                Return
            End If

            If Item Is Nothing Then
                m_TV.SelectedNode = m_TV.Nodes(0)
            Else
                m_TV.SelectedNode = Item.Tag.TreeNode
            End If
        End Sub
        Protected Overrides Sub SelectUpdate(Optional ByVal AutoSelect As Boolean = True)
            Dim b As Boolean = False

            MyBase.SelectUpdate(AutoSelect)
            m_tPaste.Enabled = (m_ClipBoard.Food IsNot Nothing) : m_mPaste.Enabled = m_tPaste.Enabled

            If (m_TV Is Nothing) Then
                Return
            End If

            If AutoSelect Then
                b = (m_TV.SelectedNode IsNot Nothing) AndAlso (m_TV.SelectedNode.Tag IsNot Nothing)
            End If

            If b Then
                Dim fd As Food = m_TV.SelectedNode.Tag

                m_CombinationsRLM.List = fd.Combinations
            Else
                m_CombinationsRLM.List = Nothing
            End If
        End Sub
#End Region

        Protected Overrides Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
            MyBase.Localize(Source)
            m_Text = Source("General", "ListCaptions", "Foods")
            RefreshButtons()
            If (m_TV IsNot Nothing) AndAlso (m_TV.Nodes("root") IsNot Nothing) Then
                m_TV.Nodes("root").Text = m_Text
            End If
        End Sub
    End Class

    Class CommentListManager

#Region "Events"
#Region "List"
        Private Sub List_ItemAdded(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_List.ItemAdded
            'List_BatchEnded(Nothing, Nothing)
            Dim com As Comment
            Dim lvi As ListViewItem

            If ee.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                com = ee.SourceObject
                lvi = m_LV.Items.Add(com.Text)
                lvi.SubItems.Add(com.Scope)
                lvi.Tag = com
                com.Tag.ListViewItem = lvi
            End If
        End Sub

        Private Sub List_ItemRemoved(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_List.ItemRemoved
            'List_BatchEnded(Nothing, Nothing)
            Dim com As Comment, lvi As ListViewItem

            If ee.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                com = ee.SourceObject
                lvi = com.Tag.ListViewItem
                m_LV.Items.Remove(lvi)
            End If
        End Sub

        Private Sub List_BatchEnded(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_List.BatchEnded
            RefreshButtons()
            SelectUpdate()
        End Sub
#End Region

#Region "List view"
        Private Sub ListView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles m_LV.KeyDown
            If e.KeyCode = Keys.Delete Then
                If (m_tRemove IsNot Nothing) AndAlso m_tRemove.Enabled Then
                    Remove_Click(Nothing, Nothing)
                End If
            End If
        End Sub

        Protected Overridable Sub ListView_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_LV.ItemActivate
            If (m_tEdit IsNot Nothing) AndAlso m_tEdit.Enabled Then
                Edit_Click(Nothing, Nothing)
            End If
        End Sub
        Private Sub ListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_LV.SelectedIndexChanged
            SelectUpdate()
        End Sub
#End Region

#Region "Buttons"
        Protected Sub Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tAdd.Click, m_mAdd.Click
            Dim s As String = "", s1 As String

            If m_List Is Nothing Then
                Return
            End If

            s = InputBox("Enter the new item comment:", "New item")
            If s = "" Then
                Return
            End If

            If (m_List.Parent IsNot Nothing) AndAlso ((Not TypeOf m_List.Parent Is Reference) OrElse (CType(m_List.Parent, Reference).MyItem IsNot Nothing)) Then
                s1 = InputBox("Enter the new item scope:", "New item", CType(m_List.Parent, Element).Name)
            Else
                s1 = ""
            End If

            m_List.Add(s, s1)
        End Sub
        Protected Sub Edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tEdit.Click, m_mEdit.Click
            Dim s As String = "", s1 As String

            If m_List Is Nothing Then
                Return
            End If

            If m_LV.SelectedItems.Count < 1 Then
                Return
            End If

            With (CType(m_LV.SelectedItems(0).Tag, Comment))
                s = .Text
                s1 = .Scope
                If s1 = "" Then
                    s1 = CType(m_List.Parent, Element).Name
                End If

                s = InputBox("Enter the new item comment:", "New item", s)
                If s = "" Then
                    Return
                End If

                s1 = InputBox("Enter the new item scope:", "New item", s1)

                .Rename(s, s1)
            End With
        End Sub
        Private Sub Remove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tRemove.Click, m_mRemove.Click
            Dim i As Integer
            Dim lvis As ListView.SelectedListViewItemCollection
            Dim ob As Object
            Dim coms() As Comment

            lvis = m_LV.SelectedItems

            If lvis.Count = 0 Then
                Return
            End If

            If MsgBox("Are you sure to remove " & IIf(lvis.Count = 1, "this item?", lvis.Count & " items?"), MsgBoxStyle.OkCancel, "Remove") = MsgBoxResult.Ok Then
                ReDim coms(lvis.Count)
                For i = 1 To lvis.Count
                    ob = lvis.Item(i - 1).Tag
                    If (ob IsNot Nothing) AndAlso (TypeOf ob Is Comment) Then
                        coms(i) = ob
                    Else
                        coms(i) = Nothing
                        Debug.Fail("Cannot have nothing in ListViewItem.Tag!")
                    End If
                Next

                m_List.BatchStart()
                For i = 1 To UBound(coms)
                    m_List.Remove(coms(i))
                Next
                m_List.BatchEnd()
            End If
        End Sub
        Private Sub Clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tClear.Click, m_mClear.Click
            If m_List.Count = 0 Then
                Return
            End If

            If MsgBox("Are you sure to clear ALL (" & m_List.Count & ") items?", MsgBoxStyle.OkCancel, "Clear items") = MsgBoxResult.Ok Then
                m_List.Clear()
            End If
        End Sub
#End Region
#End Region

#Region "Variables"
        Protected WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources
        Protected m_TextText As String, m_CommentsText As String

        Protected WithEvents m_List As CommentList

        Protected WithEvents m_LV As ListView, m_TB As ToolStrip, m_Menu As ContextMenuStrip, m_GB As GroupBox
        Protected m_Text As String, m_ItemsText As String

        Protected WithEvents m_tAdd As ToolStripButton, m_tEdit As ToolStripButton
        Protected WithEvents m_tRemove As ToolStripButton, m_tClear As ToolStripButton
        Protected WithEvents m_mAdd As ToolStripMenuItem, m_mEdit As ToolStripMenuItem
        Protected WithEvents m_mRemove As ToolStripMenuItem, m_mClear As ToolStripMenuItem
#End Region

#Region "Init/Terminate"
        Sub New()
            Localize(MyMUISources.Current)
        End Sub
#End Region

#Region "Properties"
        Public Property List() As CommentList
            Get
                Return m_List
            End Get
            Set(ByVal NewValue As CommentList)
                m_List = NewValue

                Populate()
            End Set
        End Property
#End Region

#Region "Methods"
        Public Function SetControls(ByVal List As CommentList, ByVal ListView As ListView, ByVal ToolBar As ToolStrip, ByVal GroupBox As GroupBox) As Boolean
            m_List = Nothing
            m_LV = Nothing
            m_TB = Nothing
            m_GB = Nothing
            m_Text = ""
            m_Menu = Nothing

            m_tAdd = Nothing : m_mAdd = Nothing
            m_tEdit = Nothing : m_mEdit = Nothing
            m_tRemove = Nothing : m_mRemove = Nothing
            m_tClear = Nothing : m_mClear = Nothing

            If (ListView Is Nothing) OrElse (ToolBar Is Nothing) OrElse (GroupBox Is Nothing) Then
                Return False
            End If

            m_List = List
            m_LV = ListView
            m_TB = ToolBar
            m_GB = GroupBox
            m_Text = ""
            m_Menu = New ContextMenuStrip

            With m_LV
                .Items.Clear()
                With .Columns
                    .Clear()
                    .Add("")
                    .Add("")
                End With
            End With

            m_TB.Items.Clear()
            m_Menu.Items.Clear()

            AddToolItem(m_tAdd, m_mAdd, "", My.Resources.IMAGE_LIST_ADD)
            AddToolItem(m_tEdit, m_mEdit, "", My.Resources.IMAGE_LIST_EDIT)
            AddToolItem(m_tRemove, m_mRemove, "", My.Resources.IMAGE_LIST_REMOVE)
            AddToolItem(m_tClear, m_mClear, "", My.Resources.IMAGE_LIST_CLEAR)

            m_LV.ContextMenuStrip = m_Menu

            Populate()

            Localize(MyMUISources.Current)

            Return True
        End Function
        Private Sub AddToolItem(ByRef ToolItem As ToolStripItem, ByRef MenuItem As ToolStripMenuItem, ByVal Text As String, Optional ByVal Image As System.Drawing.Image = Nothing)
            ToolItem = New ToolStripButton(Text)
            With ToolItem
                .Image = Image
                If .Image Is Nothing Then
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                Else
                    .ImageTransparentColor = Color.Magenta
                    .DisplayStyle = ToolStripItemDisplayStyle.Image
                End If
            End With
            m_TB.Items.Add(ToolItem)
            AddMenuItem(MenuItem, ToolItem)
        End Sub
        Private Sub AddToolSep(ByRef ToolItem As ToolStripSeparator, ByRef MenuItem As ToolStripSeparator)
            ToolItem = New ToolStripSeparator
            m_TB.Items.Add(ToolItem)
            AddMenuSep(MenuItem)
        End Sub
        Private Sub AddMenuItem(ByRef MenuItem As ToolStripMenuItem, ByVal ToolItem As ToolStripItem)
            MenuItem = New ToolStripMenuItem(ToolItem.Text)
            With MenuItem
                .Image = ToolItem.Image
                .ImageTransparentColor = ToolItem.ImageTransparentColor
            End With
            m_Menu.Items.Add(MenuItem)
        End Sub
        Private Sub AddMenuSep(ByRef MenuItem As ToolStripSeparator)
            MenuItem = New ToolStripSeparator
            m_Menu.Items.Add(MenuItem)
        End Sub
        Protected Sub LocalizeToolItem(ByVal ToolItem As ToolStripItem, ByVal MenuItem As ToolStripMenuItem, ByVal Text As String)
            If ToolItem IsNot Nothing Then ToolItem.Text = Text : MenuItem.Text = Text
        End Sub

        Public Overridable Sub Populate()
            Dim i As Integer
            Dim lvi As ListViewItem, lvs As ListView.ListViewItemCollection
            Dim com As Comment

            If m_LV Is Nothing Then
                Return
            End If

            m_LV.Items.Clear()

            RefreshButtons()

            If m_List Is Nothing Then
                Return
            End If

            lvs = m_LV.Items
            lvs.Clear()

            With m_List
                For i = 1 To .Count
                    com = .Item(i)
                    lvi = lvs.Add(com.Text)
                    lvi.SubItems.Add(com.Scope)
                    lvi.Tag = com
                    com.Tag.ListViewItem = lvi
                Next
            End With
        End Sub
        Public Sub RefreshButtons()
            Dim b As Boolean = (m_List IsNot Nothing)

            If m_LV Is Nothing Then
                Return
            End If

            m_GB.Enabled = b
            m_TB.Enabled = b
            m_LV.Enabled = b
            SelectUpdate()

            If m_List Is Nothing Then
                m_GB.Text = m_Text
            Else
                m_GB.Text = m_Text & " (" & m_List.Count & " " & m_ItemsText & "):"
            End If
        End Sub

        Protected Overridable Sub SelectUpdate(Optional ByVal AutoSelect As Boolean = True)
            Dim b As Boolean = False

            If (m_LV Is Nothing) Then
                Return
            End If

            b = (m_List IsNot Nothing)
            m_tAdd.Enabled = b : m_mAdd.Enabled = m_tAdd.Enabled
            m_tClear.Enabled = b : m_mClear.Enabled = m_tClear.Enabled

            b = b And (m_LV.SelectedItems.Count > 0)
            m_tEdit.Enabled = b : m_mEdit.Enabled = m_tEdit.Enabled
            m_tRemove.Enabled = b : m_mRemove.Enabled = m_tRemove.Enabled
        End Sub
#End Region

        Protected Overridable Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
            m_Text = Source("General", "ListCaptions", "Comments")
            m_ItemsText = Source("General", "ListCaptions", "Items")
            m_TextText = Source("General", "ListHeaders", "CommentText")
            m_CommentsText = Source("General", "ListHeaders", "CommentScope")
            RefreshButtons()
            If m_LV IsNot Nothing Then
                With m_LV.Columns
                    If .Count > 0 Then .Item(0).Text = m_TextText
                    If .Count > 1 Then .Item(1).Text = m_CommentsText
                End With
            End If
            LocalizeToolItem(m_tAdd, m_mAdd, Source("General", "ItemsCommands", "Add"))
            LocalizeToolItem(m_tEdit, m_mEdit, Source("General", "ItemsCommands", "Edit"))
            LocalizeToolItem(m_tRemove, m_mRemove, Source("General", "ItemsCommands", "Remove"))
            LocalizeToolItem(m_tClear, m_mClear, Source("General", "ItemsCommands", "Clear"))
        End Sub
    End Class
End Namespace