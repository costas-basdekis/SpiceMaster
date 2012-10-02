Namespace Core
    Enum ManageFlags
        Normal = 0
        DoNotAddIWC = 1
        IsReadOnly = 2
        CheckWhenAdding = 4
        ShowComments = 8
    End Enum

    MustInherit Class ReferenceListManager(Of ElementType As {Element, New}, ReferenceType As {TypedReference(Of ElementType), New}, ListType As ReferenceList(Of ElementType, ReferenceType))

#Region "Events"
        Public Event ListEvent(ByVal sender As Object, ByVal ee As ElementList.EventArgs)

#Region "List"
        Private Sub List_ItemAdded(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_List.ItemAdded
            'List_BatchEnded(Nothing, Nothing)
            If ee.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                CreateItem(ee.SourceObject)
            End If

            If sender IsNot Nothing Then
                RaiseEvent ListEvent(sender, ee)
            End If
        End Sub

        Private Sub List_ItemRemoved(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_List.ItemRemoved
            'List_BatchEnded(Nothing, Nothing)
            If ee.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                RemoveItem(ee.SourceObject)
            End If

            If sender IsNot Nothing Then
                RaiseEvent ListEvent(sender, ee)
            End If
        End Sub

        Private Sub List_BatchEnded(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_List.BatchEnded
            RefreshButtons()
            SelectItem()

            If sender IsNot Nothing Then
                RaiseEvent ListEvent(sender, ee)
            End If
        End Sub

        Private Sub List_ItemChanged(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_List.ItemChanged
            If ee.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                UpdateItem(ee.SourceObject)
            End If

            If sender IsNot Nothing Then
                RaiseEvent ListEvent(sender, ee)
            End If
        End Sub
#End Region

#Region "Drag"
        Private Sub DragStart(ByVal sender As Object, ByVal e As ItemDragEventArgs) Handles m_LV.ItemDrag, m_TV.ItemDrag
            If (m_LV IsNot Nothing) AndAlso (m_LV.Visible) Then
                m_LV.DoDragDrop(m_LV.SelectedItems, DragDropEffects.Copy Or DragDropEffects.Move)
            ElseIf (m_TV IsNot Nothing) AndAlso (m_TV.Visible) Then
                m_TV.DoDragDrop(e.Item, DragDropEffects.Copy Or DragDropEffects.Move)
            End If
        End Sub
        Private Sub DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles m_LV.DragEnter, m_LV.DragOver, m_TV.DragEnter, m_TV.DragOver
            If CanDrop(e) Then
                If e.KeyState And 4 Then
                    e.Effect = DragDropEffects.Copy
                Else
                    e.Effect = DragDropEffects.Move
                End If
            Else
                e.Effect = DragDropEffects.None
            End If
        End Sub
        Private Sub DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles m_LV.DragDrop, m_TV.DragDrop
            DragDrop(e)
        End Sub
#End Region

#Region "List view"
        Private Sub ListView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles m_LV.KeyDown
            If Not m_LV.Visible Then Return

            Select Case e.KeyCode
                Case Keys.Delete
                    If (m_tRemove IsNot Nothing) AndAlso m_tRemove.Enabled Then
                        Remove_Click(Nothing, Nothing)
                    End If
                Case Keys.Enter
                    Go_Click(Nothing, Nothing)
            End Select
        End Sub

        Protected Overridable Sub ListView_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_LV.ItemActivate
            If Not m_LV.Visible Then Return

            If m_LV.SelectedItems.Count = 0 Then Return

            If m_tGo.Enabled Then
                Go_Click(Nothing, Nothing)
            End If
        End Sub
        Private Sub ListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_LV.SelectedIndexChanged
            If Not m_LV.Visible Then Return

            SelectItem()
        End Sub
#End Region

#Region "TreeView"
        Private Sub TreeView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles m_TV.KeyDown
            If Not m_TV.Visible Then Return

            Select Case e.KeyCode
                Case Keys.Delete
                    If (m_tRemove IsNot Nothing) AndAlso m_tRemove.Enabled Then
                        Remove_Click(Nothing, Nothing)
                    End If
                Case Keys.Enter
                    Go_Click(Nothing, Nothing)
            End Select
        End Sub

        Private Sub TreeView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_TV.DoubleClick
            If Not m_TV.Visible Then Return

            If m_tGo.Enabled Then
                Go_Click(Nothing, Nothing)
            End If
        End Sub
        Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles m_TV.AfterSelect
            If Not m_TV.Visible Then Return

            SelectItem()
        End Sub
#End Region

#Region "Buttons"
        Private Sub IWC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tIWC.Click, m_mIWC.Click
            frmIWannaCook.AddItems(m_LV.SelectedItems)
        End Sub

        Protected Overridable Sub Go_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tGo.Click, m_mGo.Click
            '
        End Sub
        Protected Overridable Sub Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tAdd.Click, m_mAdd.Click
            '
        End Sub
        Private Sub Remove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tRemove.Click, m_mRemove.Click
            Dim i As Integer
            Dim ob As Object
            Dim spcrs() As ReferenceType, sc As Integer = 0

            If m_LV.Visible Then
                Dim lvis As ListView.SelectedListViewItemCollection = m_LV.SelectedItems

                If lvis.Count = 0 Then
                    Return
                End If

                ReDim spcrs(lvis.Count)
                For i = 1 To lvis.Count
                    ob = lvis.Item(i - 1).Tag
                    If (ob IsNot Nothing) AndAlso (TypeOf ob Is ReferenceType) Then
                        spcrs(i) = ob
                    Else
                        spcrs(i) = Nothing
                        Debug.Fail("A no-item in collection")
                    End If
                Next
            Else
                Dim tn As TreeNode = m_TV.SelectedNode

                If tn Is Nothing Then
                    Return
                End If

                ReDim spcrs(1)
                ob = tn.Tag
                If (ob IsNot Nothing) AndAlso (TypeOf ob Is ReferenceType) Then
                    spcrs(1) = ob
                Else
                    spcrs(i) = Nothing
                    Debug.Fail("A no-item in collection")
                End If
            End If

            sc = UBound(spcrs)

            If MsgBox("Are you sure to remove " & IIf(sc = 1, "this item?", sc & " items?"), MsgBoxStyle.OkCancel, "Remove") <> MsgBoxResult.Ok Then Return

            m_List.BatchStart()
            For i = 1 To UBound(spcrs)
                m_List.Remove(spcrs(i))
            Next
            m_List.BatchEnd()
        End Sub
        Private Sub Clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tClear.Click, m_mClear.Click
            If m_List.Count = 0 Then
                Return
            End If

            If MsgBox("Are you sure to clear ALL (" & m_List.Count & ") items?", MsgBoxStyle.OkCancel, "Clear items") = MsgBoxResult.Ok Then
                m_List.Clear()
            End If
        End Sub

        Protected Sub ViewDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tDetails.Click, m_mDetails.Click
            'm_tViews.Text = "Details" : m_mViews.Text = m_tViews.Text
            m_tDetails.CheckState = CheckState.Checked : m_mDetails.CheckState = m_tDetails.CheckState
            m_tTiles.CheckState = CheckState.Unchecked : m_mTiles.CheckState = m_tTiles.CheckState
            m_LV.View = View.Details
            ListView_SelectedIndexChanged(Nothing, Nothing)
        End Sub
        Protected Sub ViewTiles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tTiles.Click, m_mTiles.Click
            'm_tViews.Text = "Tiles" : m_mViews.Text = m_tViews.Text
            m_tDetails.CheckState = CheckState.Unchecked : m_mDetails.CheckState = m_tDetails.CheckState
            m_tTiles.CheckState = CheckState.Checked : m_mTiles.CheckState = m_tTiles.CheckState
            m_LV.View = View.Tile
        End Sub
#End Region

#Region "Clipboard"
        Protected Overridable Sub m_Clipboard_ItemChanged(ByVal Item As Clipboard.ItemType, ByVal InnerEvent As Object) Handles m_Clipboard.ItemChanged
            '
        End Sub
#End Region
#End Region

#Region "Variables"
        Protected WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

        Shared m_NameText As String, m_CommentsText As String
        Protected WithEvents m_List As ListType
        Protected WithEvents m_Clipboard As Clipboard

        Protected WithEvents m_LV As ListView, m_TV As TreeView, m_ToolBar As ToolStrip, m_Menu As ContextMenuStrip, m_GroupBox As GroupBox
        Protected m_Text As String, m_ItemsText As String

        Protected WithEvents m_tIWC As ToolStripButton, m_tGo As ToolStripButton, m_tSep1 As ToolStripSeparator, m_tAdd As ToolStripButton, m_tRemove As ToolStripButton, m_tClear As ToolStripButton, m_tSep2 As ToolStripSeparator, m_tViews As ToolStripSplitButton, m_tDetails As ToolStripMenuItem, m_tTiles As ToolStripMenuItem
        Protected WithEvents m_tCopy As ToolStripButton, m_tCut As ToolStripButton, m_tPaste As ToolStripButton
        Protected WithEvents m_mIWC As ToolStripMenuItem, m_mGo As ToolStripMenuItem, m_mSep1 As ToolStripSeparator, m_mAdd As ToolStripMenuItem, m_mRemove As ToolStripMenuItem, m_mClear As ToolStripMenuItem, m_mSep2 As ToolStripSeparator, m_mViews As ToolStripMenuItem, m_mDetails As ToolStripMenuItem, m_mTiles As ToolStripMenuItem
        Protected WithEvents m_mCopy As ToolStripMenuItem, m_mCut As ToolStripMenuItem, m_mPaste As ToolStripMenuItem

        Protected m_ManageFlags As ManageFlags
        Protected m_MFDontAddIWC As Boolean
        Protected m_MFIsReadOnly As Boolean
        Protected m_MFCheckWhenAdding As Boolean
        Protected m_MFShowComments As Boolean

        Protected m_AddingCombination As Boolean = False
#End Region

#Region "Properties"
        Public Property List() As ListType
            Get
                Return m_List
            End Get
            Set(ByVal NewValue As ListType)
                If m_List IsNot Nothing Then m_List.Tag = Nothing

                m_List = NewValue
                If (m_List IsNot Nothing) AndAlso (m_List.Document IsNot Nothing) Then m_Clipboard = m_List.Document.ClipBoard

                Refresh()
            End Set
        End Property
        Public Property Clipboard() As Clipboard
            Get
                Return m_Clipboard
            End Get
            Set(ByVal value As Clipboard)
                m_Clipboard = value
                RefreshButtons()
            End Set
        End Property

        Public ReadOnly Property AddingCombination() As Boolean
            Get
                Return m_AddingCombination
            End Get
        End Property
#End Region

#Region "Init/Terminate"
        Sub New()
            Localize(MyMUISources.Current)
        End Sub

        Protected Overrides Sub Finalize()
            If m_List IsNot Nothing Then m_List.Tag = Nothing
            ClearItems()
            MyBase.Finalize()
        End Sub
#End Region

#Region "Methods"
#Region "Controls"
        Public Overridable Function SetControls(ByVal List As ListType, ByVal ListView As ListView, ByVal ToolBar As ToolStrip, ByVal GroupBox As GroupBox, Optional ByVal Flags As ManageFlags = ManageFlags.Normal) As Boolean
            ClearControls()

            If (ListView Is Nothing) OrElse (ToolBar Is Nothing) OrElse (GroupBox Is Nothing) Then
                Return False
            End If

            m_List = List : If (m_List IsNot Nothing) AndAlso (m_List.Document IsNot Nothing) Then m_Clipboard = m_List.Document.ClipBoard
            m_LV = ListView
            m_ToolBar = ToolBar
            m_GroupBox = GroupBox
            m_Menu = New ContextMenuStrip
            m_ManageFlags = Flags
            m_MFDontAddIWC = OMHasMode(m_ManageFlags, ManageFlags.DoNotAddIWC)
            m_MFIsReadOnly = OMHasMode(m_ManageFlags, ManageFlags.IsReadOnly)
            m_MFCheckWhenAdding = OMHasMode(m_ManageFlags, ManageFlags.CheckWhenAdding)
            m_MFShowComments = OMHasMode(m_ManageFlags, ManageFlags.ShowComments)

            With m_LV
                .Items.Clear()
                With .Columns
                    .Clear()
                    .Add("nm", "")
                    If m_MFShowComments Then
                        m_LV.Columns.Add("cm", "")
                    End If
                End With
                .TileSize = New Size(100, 30)
            End With

            m_ToolBar.Items.Clear()
            m_Menu.Items.Clear()

            If Not m_MFDontAddIWC Then
                AddToolItem(m_tIWC, m_mIWC, "", My.Resources.IMAGE_APPLICATION)
                m_tIWC.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            End If
            AddToolItem(m_tGo, m_mGo, "", My.Resources.IMAGE_BROWSE_GO)
            AddToolSep(m_mSep1, m_tSep1)
            If Not m_MFIsReadOnly Then
                AddToolItem(m_tAdd, m_mAdd, "", My.Resources.IMAGE_LIST_ADD)
                AddToolItem(m_tRemove, m_mRemove, "", My.Resources.IMAGE_LIST_REMOVE)
                AddToolItem(m_tClear, m_mClear, "", My.Resources.IMAGE_LIST_CLEAR)
                AddToolSep(m_tSep2, m_mSep2)
            End If

            AddToolItem(m_tCopy, m_mCopy, "Copy", My.Resources.IMAGE_LIST_COPY)
            If Not m_MFIsReadOnly Then
                AddToolItem(m_tCut, m_mCut, "Cut", My.Resources.IMAGE_LIST_CUT)
                AddToolItem(m_tPaste, m_mPaste, "Paste", My.Resources.IMAGE_LIST_PASTE)
            End If

            With m_ToolBar.Items
                'Views
                m_tViews = New ToolStripSplitButton("")
                m_tViews.DisplayStyle = ToolStripItemDisplayStyle.Text
                'Details
                m_tDetails = New ToolStripMenuItem("")
                m_tDetails.DisplayStyle = ToolStripItemDisplayStyle.Text
                m_tViews.DropDownItems.Add(m_tDetails)
                'Tiles
                m_tTiles = New ToolStripMenuItem("")
                m_tTiles.DisplayStyle = ToolStripItemDisplayStyle.Text
                m_tViews.DropDownItems.Add(m_tTiles)
                .Add(m_tViews)
            End With
            With m_Menu.Items
                m_mViews = New ToolStripMenuItem("")
                With m_mViews.DropDown.Items
                    m_mDetails = New ToolStripMenuItem("") : .Add(m_mDetails)
                    m_mTiles = New ToolStripMenuItem("") : .Add(m_mTiles)
                End With
                .Add(m_mViews)
            End With
            m_LV.ContextMenuStrip = m_Menu

            Localize(MyMUISources.Current)

            Refresh()

            Return True
        End Function
        Public Shared Function OMHasMode(ByVal OM As ManageFlags, ByVal Mode As ManageFlags) As Boolean
            Return ((OM And Mode) = Mode)
        End Function

        Private Sub ClearControls()
            m_List = Nothing : m_Clipboard = Nothing
            m_LV = Nothing
            m_ToolBar = Nothing
            m_GroupBox = Nothing
            m_Menu = Nothing

            m_tIWC = Nothing : m_mIWC = Nothing
            m_tGo = Nothing : m_mGo = Nothing
            m_tSep1 = Nothing : m_mSep1 = Nothing
            m_tAdd = Nothing : m_mAdd = Nothing
            m_tRemove = Nothing : m_mRemove = Nothing
            m_tClear = Nothing : m_mClear = Nothing
            m_tSep2 = Nothing : m_mSep2 = Nothing
            m_tCopy = Nothing : m_mCopy = Nothing
            m_tCut = Nothing : m_mCut = Nothing
            m_tPaste = Nothing : m_mPaste = Nothing
            m_tViews = Nothing : m_mViews = Nothing
            m_tDetails = Nothing : m_mDetails = Nothing
            m_tTiles = Nothing : m_mTiles = Nothing
        End Sub

#Region "ToolItems"
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
            m_ToolBar.Items.Add(ToolItem)
            AddMenuItem(MenuItem, ToolItem)
        End Sub
        Protected Sub InsertToolItem(ByRef ToolItem As ToolStripItem, ByRef MenuItem As ToolStripMenuItem, ByVal Position As Integer, ByVal Text As String, Optional ByVal Image As System.Drawing.Image = Nothing)
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
            m_ToolBar.Items.Insert(Position, ToolItem)
            InsertMenuItem(MenuItem, ToolItem, Position)
        End Sub
        Private Sub AddToolSep(ByRef ToolItem As ToolStripSeparator, ByRef MenuItem As ToolStripSeparator)
            ToolItem = New ToolStripSeparator
            m_ToolBar.Items.Add(ToolItem)
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
        Private Sub InsertMenuItem(ByRef MenuItem As ToolStripMenuItem, ByVal ToolItem As ToolStripItem, ByVal Position As Integer)
            MenuItem = New ToolStripMenuItem(ToolItem.Text)
            With MenuItem
                .Image = ToolItem.Image
                .ImageTransparentColor = ToolItem.ImageTransparentColor
            End With
            m_Menu.Items.Insert(Position, MenuItem)
        End Sub
        Private Sub AddMenuSep(ByRef MenuItem As ToolStripSeparator)
            MenuItem = New ToolStripSeparator
            m_Menu.Items.Add(MenuItem)
        End Sub
        Protected Sub LocalizeToolItem(ByVal ToolItem As ToolStripItem, ByVal MenuItem As ToolStripMenuItem, ByVal Text As String)
            Try
                If ToolItem IsNot Nothing Then ToolItem.Text = Text : MenuItem.Text = Text
            Catch ex As Exception
            End Try
        End Sub
#End Region
#End Region

#Region "Refresh"
        Public Overridable Sub Refresh()
            Dim i As Integer
            Dim rel As ReferenceType

            If m_LV Is Nothing Then
                Return
            End If

            RefreshButtons()

            If m_List Is Nothing Then
                Return
            End If

            ClearItems()

            With m_List
                For i = 1 To .Count
                    rel = .Item(i)
                    CreateItem(rel)
                Next
            End With
        End Sub
        Public Sub RefreshButtons()
            Dim b As Boolean = False

            If m_LV Is Nothing Then
                Return
            End If

            b = m_MFIsReadOnly OrElse (m_List IsNot Nothing)

            m_GroupBox.Enabled = b
            m_ToolBar.Enabled = b
            m_LV.Enabled = b
            If m_Clipboard Is Nothing Then
                If m_tCut IsNot Nothing Then m_tCut.Enabled = False : m_mCut.Enabled = m_tCut.Enabled
                If m_tCopy IsNot Nothing Then m_tCopy.Enabled = False : m_mCopy.Enabled = m_tCopy.Enabled
                If m_tPaste IsNot Nothing Then m_tPaste.Enabled = False : m_mPaste.Enabled = m_tPaste.Enabled
            Else
                If m_tCut IsNot Nothing Then m_tCut.Enabled = b And m_LV.SelectedItems.Count > 0 : m_mCut.Enabled = m_tCut.Enabled
                If m_tCopy IsNot Nothing Then m_tCopy.Enabled = b And m_LV.SelectedItems.Count > 0 : m_mCopy.Enabled = m_tCopy.Enabled
                'If m_tPaste IsNot Nothing Then m_tPaste.Enabled = (m_Clipboard.Combs.Count > 0) : m_mPaste.Enabled = m_tPaste.Enabled
                m_Clipboard_ItemChanged(0, Nothing)
            End If
            If Not m_MFIsReadOnly Then
                m_tClear.Enabled = ((m_List IsNot Nothing) AndAlso (m_List.Count > 0)) : m_mClear.Enabled = m_tClear.Enabled
            End If
            SelectItem(False)

            Select Case m_LV.View
                Case View.Details
                    ViewDetails_Click(Nothing, Nothing)
                Case View.Tile
                    ViewTiles_Click(Nothing, Nothing)
            End Select

            If Not m_MFIsReadOnly Then
                If m_List Is Nothing Then
                    ClearItems()
                    m_GroupBox.Text = m_Text
                Else
                    m_GroupBox.Text = m_Text & " (" & m_List.Count & " " & m_ItemsText & "):"
                End If
            Else
                m_GroupBox.Text = m_Text
            End If
        End Sub
#End Region

#Region "Items"
        Protected Friend Overridable Sub CreateItem(ByVal Element As ReferenceType)
            Dim lvi As ListViewItem

            lvi = m_LV.Items.Insert(Element.Index - 1, Element.DisplayText)
            lvi.SubItems.Add(Element.Comments.FullName)
            lvi.Tag = Element
            Element.Tag.ListViewItem = lvi
        End Sub
        Protected Friend Overridable Sub UpdateItem(ByVal Element As ReferenceType)
            If (Element Is Nothing) OrElse (Element.Tag.ListViewItem Is Nothing) Then Exit Sub
            Dim lvi As ListViewItem = Element.Tag.ListViewItem, lvisi As ListViewItem.ListViewSubItem = lvi.SubItems(1)
            Dim elInd = Element.Index - 1, elText As String = Element.DisplayText, elCom As String = Element.Comments.FullName

            If lvi.Index <> elInd Then
                m_LV.Items.Remove(lvi)
                m_LV.Items.Insert(elInd, lvi)
            End If
            If lvi.Text <> elText Then
                lvi.Text = elText
            End If
            If lvisi.Text <> elCom Then
                lvisi.Text = elCom
            End If
        End Sub
        Protected Friend Overridable Sub RemoveItem(ByVal Element As ReferenceType)
            With Element.Tag
                If .ListViewItem IsNot Nothing Then
                    .ListViewItem.Remove()
                    .ListViewItem.Tag = Nothing
                End If
                .ListViewItem = Nothing
            End With
        End Sub
        Protected Overridable Sub ClearItems()
            Dim i As Integer
            Dim lvi As ListViewItem
            Dim rel As ReferenceType

            If m_LV Is Nothing Then Return

            With m_LV.Items
                For i = 1 To .Count
                    lvi = .Item(i - 1)
                    rel = lvi.Tag
                    If rel IsNot Nothing Then rel.Tag.ListViewItem = Nothing
                    lvi.Tag = Nothing
                Next
                .Clear()
            End With
        End Sub

        Protected Overridable Sub SelectItem(Optional ByVal AutoSelect As Boolean = True, Optional ByVal NonAuto_Count As Integer = 0)
            Dim b1 As Boolean = False, b2 As Boolean = False
            Dim SIC As Integer = 0

            If (m_LV Is Nothing) AndAlso (m_TV Is Nothing) Then Return

            If (m_LV IsNot Nothing) AndAlso m_LV.Visible Then
                SIC = m_LV.SelectedItems.Count
            ElseIf m_TV IsNot Nothing Then
                SIC = IIf(m_TV.SelectedNode IsNot Nothing, 1, 0)
            End If

            If AutoSelect Then
                b1 = (SIC > 0)
                b2 = (SIC = 1)
            Else
                b1 = (NonAuto_Count > 0)
                b2 = (NonAuto_Count = 1)
            End If

            If (m_LV IsNot Nothing) AndAlso (m_TV IsNot Nothing) Then
                If b1 Then
                    If m_LV.Visible Then
                        m_TV.SelectedNode = CType(m_LV.SelectedItems(0).Tag, ReferenceType).Tag.TreeNode
                    Else
                        With m_LV.SelectedItems
                            .Clear()
                            CType(m_TV.SelectedNode.Tag, ReferenceType).Tag.ListViewItem.Selected = True
                        End With
                    End If
                Else
                    If m_LV.Visible Then
                        m_TV.SelectedNode = Nothing
                    Else
                        m_LV.SelectedItems.Clear()
                    End If
                End If
            End If

            If Not m_MFDontAddIWC Then
                m_tIWC.Enabled = b1 : m_mIWC.Enabled = m_tIWC.Enabled
            End If
            m_tGo.Enabled = b2 : m_mGo.Enabled = m_tGo.Enabled
            If Not m_MFIsReadOnly Then
                m_tRemove.Enabled = b1 : m_mRemove.Enabled = m_tRemove.Enabled
            End If

            If m_Clipboard Is Nothing Then
                If m_tCut IsNot Nothing Then m_tCut.Enabled = False : m_mCut.Enabled = m_tCut.Enabled
                If m_tCopy IsNot Nothing Then m_tCopy.Enabled = False : m_mCopy.Enabled = m_tCopy.Enabled
                If m_tPaste IsNot Nothing Then m_tPaste.Enabled = False : m_mPaste.Enabled = m_tPaste.Enabled
            Else
                If m_tCut IsNot Nothing Then m_tCut.Enabled = b1 : m_mCut.Enabled = m_tCut.Enabled
                If m_tCopy IsNot Nothing Then m_tCopy.Enabled = b1 : m_mCopy.Enabled = m_tCopy.Enabled
                'If m_tPaste IsNot Nothing Then m_tPaste.Enabled = (m_Clipboard.Combs.Count > 0) : m_mPaste.Enabled = m_tPaste.Enabled
            End If
        End Sub
#End Region

#Region "Drag'N'Drop"
        Public Function CanDrop(ByVal e As DragEventArgs) As Boolean
            Dim ell As ElementList = m_List
            If TypeOf ell Is CombinationReferenceList Then
                Return CombinationReferenceListManager.CanDrop(e, ell)
            ElseIf TypeOf ell Is FoodReferenceList Then
                Return FoodReferenceListManager.CanDrop(e, ell)
            End If

            Return False
        End Function
        Public Sub DragDrop(ByVal e As DragEventArgs)
            Dim ell As ElementList = m_List
            If TypeOf ell Is CombinationReferenceList Then
                CombinationReferenceListManager.DragDrop(e, ell)
            ElseIf TypeOf ell Is FoodReferenceList Then
                FoodReferenceListManager.DragDrop(e, ell)
            End If
        End Sub
        Protected MustOverride Sub AddChild(ByVal Item As Element, ByVal KeyState As Integer)
#End Region
#End Region

        Protected Overridable Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
            m_ItemsText = Source("General", "ListCaptions", "Items")
            m_NameText = Source("General", "ListHeaders", "Item")
            m_CommentsText = Source("General", "ListHeaders", "Comments")
            RefreshButtons()
            If m_LV IsNot Nothing Then
                With m_LV.Columns
                    If .Count > 0 Then .Item(0).Text = Source("General", "ListHeaders", "Name")
                    If .Count > 1 Then .Item(1).Text = Source("General", "ListHeaders", "Comments")
                End With
            End If
            LocalizeToolItem(m_tIWC, m_mIWC, Source("General", "ItemsCommands", "CookIt"))
            LocalizeToolItem(m_tGo, m_mGo, Source("General", "Navigation", "Go"))
            LocalizeToolItem(m_tAdd, m_mAdd, Source("General", "ItemsCommands", "Add"))
            LocalizeToolItem(m_tCut, m_mCut, Source("General", "ItemsCommands", "Cut"))
            LocalizeToolItem(m_tCopy, m_mCopy, Source("General", "ItemsCommands", "Copy"))
            LocalizeToolItem(m_tPaste, m_mPaste, Source("General", "ItemsCommands", "Paste"))
            LocalizeToolItem(m_tRemove, m_mRemove, Source("General", "ItemsCommands", "Remove"))
            LocalizeToolItem(m_tClear, m_mClear, Source("General", "ItemsCommands", "Clear"))
            LocalizeToolItem(m_tViews, m_mViews, Source("General", "ItemsCommands", "View"))
            LocalizeToolItem(m_tDetails, m_mDetails, Source("General", "ItemsCommands", "ViewDetails"))
            LocalizeToolItem(m_tTiles, m_mTiles, Source("General", "ItemsCommands", "ViewTiles"))
        End Sub
    End Class

    Class CombinationReferenceListManager
        Inherits ReferenceListManager(Of Combination, CombinationReference, CombinationReferenceList)

#Region "Variables"
        Protected WithEvents m_tViewStyleSep As ToolStripSeparator, m_tListView As ToolStripMenuItem, m_tTreeView As ToolStripMenuItem
        Protected WithEvents m_mViewStyleSep As ToolStripSeparator, m_mListView As ToolStripMenuItem, m_mTreeView As ToolStripMenuItem
        Protected WithEvents m_tEdit As ToolStripButton, m_tCW As ToolStripSplitButton, m_tCW_N As ToolStripMenuItem, m_tCW_A As ToolStripMenuItem, m_tCW_O As ToolStripMenuItem
        Protected WithEvents m_mEdit As ToolStripMenuItem, m_mCW As ToolStripMenuItem, m_mCW_N As ToolStripMenuItem, m_mCW_A As ToolStripMenuItem, m_mCW_O As ToolStripMenuItem
#End Region

#Region "Events"
        Protected Overrides Sub ListView_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles m_View.ItemActivate
            Dim rel As CombinationReference

            If m_LV.SelectedItems.Count = 0 Then
                Return
            End If

            rel = m_LV.SelectedItems(0).Tag

            frmSpices.SelectItem(rel.Item)
        End Sub

#Region "Buttons"
        Protected Overrides Sub Go_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles m_tGo.Click, m_mGo.Click
            Dim lvi As ListViewItem

            If m_List Is Nothing Then Return
            If (m_LV Is Nothing) OrElse (m_LV.SelectedItems.Count = 0) Then Return
            lvi = m_LV.SelectedItems(0)
            If (lvi.Tag Is Nothing) OrElse Not (TypeOf lvi.Tag Is CombinationReference) Then Return

            frmSpices.SelectItem(CType(lvi.Tag, CombinationReference).Item)
        End Sub
        Protected Overrides Sub Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles m_tAdd.Click ', m_mAdd.Click
            Dim cmbr As New CombinationReference()

            If frmDefineCombination.EditCombination(cmbr) Then
                m_List.Add(cmbr)
            End If
        End Sub
        Private Sub Edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tEdit.Click, m_mEdit.Click
            Dim lvi As ListViewItem, lvis As ListView.SelectedListViewItemCollection = m_LV.SelectedItems
            Dim cmbr As CombinationReference, new_cmbr As CombinationReference, bDifferentCombination As Boolean

            For Each lvi In lvis
                If (lvi.Tag IsNot Nothing) Then
                    cmbr = lvi.Tag
                    'Make a copy so it can be changed
                    new_cmbr = New CombinationReference(cmbr)
                    If frmDefineCombination.EditCombination(new_cmbr, bDifferentCombination) Then
                        'Change item
                        If bDifferentCombination Then
                            m_List.BatchStart()
                            cmbr.Remove()
                            cmbr = m_List.Add(new_cmbr)
                            m_List.BatchEnd()
                        Else
                            'Copy comments back
                            cmbr.Comments.CopyFrom(new_cmbr.Comments, True)
                        End If
                    End If
                End If
            Next
        End Sub
        Private Sub ConnectWith_Item_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCW_N.Click, m_tCW_A.Click, m_tCW_O.Click, m_mCW_N.Click, m_mCW_A.Click, m_mCW_O.Click
            Dim lvi As ListViewItem, el As CombinationReference

            If m_List Is Nothing Then Return
            If (m_LV Is Nothing) OrElse (m_LV.SelectedItems.Count = 0) Then Return
            lvi = m_LV.SelectedItems(0)
            el = lvi.Tag
            If el Is Nothing Then Return

            If (sender Is m_tCW_A) OrElse (sender Is m_mCW_A) Then
                m_tCW_N.Checked = False : m_tCW_A.Checked = True : m_tCW_O.Checked = False : m_tCW.Text = m_tCW_A.Text
                el.ConnectWith = CombinationReference.ConnectWithEnum.And
            ElseIf (sender Is m_tCW_O) OrElse (sender Is m_mCW_O) Then
                m_tCW_N.Checked = False : m_tCW_A.Checked = False : m_tCW_O.Checked = True : m_tCW.Text = m_tCW_O.Text
                el.ConnectWith = CombinationReference.ConnectWithEnum.Or
            Else
                m_tCW_N.Checked = True : m_tCW_A.Checked = False : m_tCW_O.Checked = False : m_tCW.Text = m_tCW_N.Text
                el.ConnectWith = CombinationReference.ConnectWithEnum.None
            End If
            m_mCW_N.Checked = m_tCW_N.Checked
            m_mCW_A.Checked = m_tCW_A.Checked
            m_mCW_O.Checked = m_tCW_O.Checked
        End Sub

        Protected Sub ListView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tListView.Click, m_mListView.Click
            m_tListView.CheckState = CheckState.Checked : m_mListView.CheckState = m_tListView.CheckState
            m_tTreeView.CheckState = CheckState.Unchecked : m_mTreeView.CheckState = m_tTreeView.CheckState
            With m_TV
                .Visible = False
                .Dock = DockStyle.None
            End With
            With m_LV
                .Visible = True
                .Dock = DockStyle.Fill
                .BringToFront()
            End With
        End Sub
        Protected Sub TreeView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tTreeView.Click, m_mTreeView.Click
            m_tListView.CheckState = CheckState.Unchecked : m_mListView.CheckState = m_tListView.CheckState
            m_tTreeView.CheckState = CheckState.Checked : m_mTreeView.CheckState = m_tTreeView.CheckState
            With m_LV
                .Visible = False
                .Dock = DockStyle.None
            End With
            With m_TV
                .Visible = True
                .Dock = DockStyle.Fill
                .BringToFront()
            End With
        End Sub

        Private Sub Copy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCopy.Click, m_mCopy.Click
            Dim i As Integer
            Dim lvis As ListView.SelectedListViewItemCollection
            Dim ob As Object
            Dim spcrs() As CombinationReference

            lvis = m_LV.SelectedItems

            If lvis.Count = 0 Then
                Return
            End If

            ReDim spcrs(lvis.Count)
            For i = 1 To lvis.Count
                ob = lvis.Item(i - 1).Tag
                If (ob IsNot Nothing) AndAlso (TypeOf ob Is CombinationReference) Then
                    spcrs(i) = ob
                Else
                    Debug.Fail("A no-item in collection")
                    spcrs(i) = Nothing
                    'MsgBox("Nothing!")
                End If
            Next

            m_Clipboard.CombsOp = Clipboard.Operation.Copy
            m_Clipboard.CombsSource = m_List
            With m_Clipboard.Combs
                .BatchStart()
                .Clear()
                For i = 1 To UBound(spcrs)
                    .Add(spcrs(i))
                Next
                .BatchEnd()
            End With
        End Sub
        Private Sub Cut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCut.Click, m_mCut.Click
            Dim i As Integer
            Dim lvis As ListView.SelectedListViewItemCollection
            Dim ob As Object
            Dim spcrs() As CombinationReference

            lvis = m_LV.SelectedItems

            If lvis.Count = 0 Then
                Return
            End If

            ReDim spcrs(lvis.Count)
            For i = 1 To lvis.Count
                ob = lvis.Item(i - 1).Tag
                If (ob IsNot Nothing) AndAlso (TypeOf ob Is CombinationReference) Then
                    spcrs(i) = ob
                Else
                    Debug.Fail("A no-item in collection")
                    spcrs(i) = Nothing
                    'MsgBox("Nothing!")
                End If
            Next

            m_Clipboard.CombsOp = Clipboard.Operation.Cut
            m_Clipboard.CombsSource = m_List
            With m_Clipboard.Combs
                .BatchStart()
                .Clear()
                For i = 1 To UBound(spcrs)
                    .Add(spcrs(i))
                Next
                .BatchEnd()
            End With
        End Sub
        Private Sub Paste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tPaste.Click, m_mPaste.Click
            Dim i As Integer
            Dim pcmb As Combination
            Dim cmbr As CombinationReference
            Dim cmbrl As CombinationReferenceList

            With m_Clipboard.Combs
                If .Count = 0 Then Return

                'Check so that we don't add a combination to itself
                If (m_List.Parent IsNot Nothing) AndAlso (TypeOf (m_List.Parent) Is Combination) Then
                    pcmb = m_List.Parent
                Else
                    pcmb = Nothing
                End If
                'Add them
                m_List.BatchStart()
                For i = 1 To .Count
                    cmbr = .Item(i)
                    'Check so that we don't add a combination to itself
                    If Not ReferenceEquals(pcmb, cmbr.Item) Then
                        m_List.Add(cmbr)
                    End If
                Next
                m_List.BatchEnd()
                'Remove cut items from clipboard and original source
                If m_Clipboard.CombsOp = Clipboard.Operation.Cut Then
                    cmbrl = m_Clipboard.CombsSource

                    cmbrl.BatchStart()
                    For i = 1 To .Count
                        cmbr = .Item(i)
                        'Don't remove those we did not add
                        If Not ReferenceEquals(pcmb, cmbr.Item) Then
                            cmbr = cmbrl.Item(cmbr)
                            If cmbr IsNot Nothing Then
                                cmbr.Remove()
                            End If
                        End If
                    Next
                    cmbrl.BatchEnd()

                    m_Clipboard.CombsOp = Core.Clipboard.Operation.Copy
                    If m_Clipboard.CombOp = Core.Clipboard.Operation.Cut Then m_Clipboard.CombOp = Core.Clipboard.Operation.Copy
                End If
            End With
        End Sub
#End Region

        Protected Overrides Sub m_Clipboard_ItemChanged(ByVal Item As Clipboard.ItemType, ByVal InnerEvent As Object)
            If (m_tPaste IsNot Nothing) AndAlso (m_Clipboard IsNot Nothing) Then
                m_tPaste.Enabled = (m_Clipboard.Combs.Count > 0) : m_mPaste.Enabled = m_tPaste.Enabled
            End If
        End Sub
#End Region

#Region "Init/Terminate"
        Sub New()
            Localize(MyMUISources.Current)
        End Sub
#End Region

#Region "Controls"
        Public Overrides Function SetControls(ByVal List As CombinationReferenceList, ByVal ListView As ListView, ByVal ToolBar As ToolStrip, ByVal GroupBox As GroupBox, Optional ByVal Flags As ManageFlags = ManageFlags.Normal) As Boolean
            ClearControls()

            m_TV = New TreeView

            If Not MyBase.SetControls(List, ListView, ToolBar, GroupBox, Flags) Then ClearControls() : Return False

            m_TV.AllowDrop = True
            m_TV.Parent = m_LV.Parent
            m_TV.ContextMenuStrip = m_LV.ContextMenuStrip

            'ViewStyleSep
            m_tViewStyleSep = New ToolStripSeparator
            m_tViews.DropDownItems.Add(m_tViewStyleSep)
            'ListView
            m_tListView = New ToolStripMenuItem("List View")
            m_tListView.DisplayStyle = ToolStripItemDisplayStyle.Text
            m_tViews.DropDownItems.Add(m_tListView)
            'TreeView
            m_tTreeView = New ToolStripMenuItem("Tree View")
            m_tTreeView.DisplayStyle = ToolStripItemDisplayStyle.Text
            m_tViews.DropDownItems.Add(m_tTreeView)

            'ViewStyleSep
            m_mViewStyleSep = New ToolStripSeparator
            m_mViews.DropDownItems.Add(m_mViewStyleSep)
            'ListView
            m_mListView = New ToolStripMenuItem("List View")
            m_mListView.DisplayStyle = ToolStripItemDisplayStyle.Text
            m_mViews.DropDownItems.Add(m_mListView)
            'TreeView
            m_mTreeView = New ToolStripMenuItem("Tree View")
            m_mTreeView.DisplayStyle = ToolStripItemDisplayStyle.Text
            m_mViews.DropDownItems.Add(m_mTreeView)

            If m_tRemove IsNot Nothing Then
                InsertToolItem(m_tEdit, m_mEdit, IIf(m_tIWC Is Nothing, 3, 4), "Edit", My.Resources.IMAGE_LIST_EDIT)
                With m_ToolBar.Items
                    'Connect With
                    m_tCW = New ToolStripSplitButton(",")
                    m_tCW.DisplayStyle = ToolStripItemDisplayStyle.Text
                    'None
                    m_tCW_N = New ToolStripMenuItem(",")
                    m_tCW_N.DisplayStyle = ToolStripItemDisplayStyle.Text
                    m_tCW.DropDownItems.Add(m_tCW_N)
                    m_tCW_N.Checked = True
                    'And
                    m_tCW_A = New ToolStripMenuItem("+")
                    m_tCW_A.DisplayStyle = ToolStripItemDisplayStyle.Text
                    m_tCW.DropDownItems.Add(m_tCW_A)
                    'Or
                    m_tCW_O = New ToolStripMenuItem(" ή ")
                    m_tCW_O.DisplayStyle = ToolStripItemDisplayStyle.Text
                    m_tCW.DropDownItems.Add(m_tCW_O)
                    .Insert(IIf(m_tIWC Is Nothing, 4, 5), m_tCW)
                End With
                With m_Menu.Items
                    m_mCW = New ToolStripMenuItem("")
                    With m_mCW.DropDown.Items
                        m_mCW_N = New ToolStripMenuItem("") : m_mCW_N.Checked = True : .Add(m_mCW_N)
                        m_mCW_A = New ToolStripMenuItem("") : .Add(m_mCW_A)
                        m_mCW_O = New ToolStripMenuItem("") : .Add(m_mCW_O)
                    End With
                    .Insert(IIf(m_mIWC Is Nothing, 4, 5), m_mCW)
                    '.Add(m_mCW)
                End With
            End If

            Localize(MyMUISources.Current)

            SelectItem(True)

            m_tListView.PerformClick()
        End Function
        Private Sub ClearControls()
            m_TV = Nothing
            m_tEdit = Nothing : m_mEdit = Nothing
            m_tCW = Nothing : m_mCW = Nothing
            m_tCW_N = Nothing : m_mCW_N = Nothing
            m_tCW_A = Nothing : m_mCW_A = Nothing
            m_tCW_O = Nothing : m_mCW_O = Nothing
            m_tViewStyleSep = Nothing : m_mViewStyleSep = Nothing
            m_tListView = Nothing : m_mListView = Nothing
            m_tTreeView = Nothing : m_mTreeView = Nothing
        End Sub
#End Region

#Region "Items"
        Protected Friend Overrides Sub CreateItem(ByVal Element As CombinationReference)
            MyBase.CreateItem(Element)

            This_CreateItem(Element)
        End Sub
        Private Sub This_CreateItem(ByVal Element As CombinationReference)
            Dim i As Integer, s As String
            Dim tn As TreeNode
            Dim tns As TreeNodeCollection = m_TV.Nodes, tns2 = tns
            Dim cmbr2 As CombinationReference

            tn = Element.Tag.TreeNode
            If tn Is Nothing Then
                cmbr2 = m_List.Item(Element.Item.Spice.DerivedCombinations(1))
                'cmbr2 = m_List.Item(New CombinationReference(New Combination(Element.Item.Spice)))
                If cmbr2 IsNot Nothing Then
                    tn = cmbr2.Tag.TreeNode
                    If tn Is Nothing Then
                        s = cmbr2.Comments.FullName : If s <> "" Then s = " (" & s & ")"
                        tn = tns.Add(cmbr2.DisplayText & s) : tn.Tag = cmbr2 : If s <> "" Then tn.ForeColor = Color.Green
                        cmbr2.Tag.TreeNode = tn
                    End If
                End If
                If Element.Tag.TreeNode Is Nothing Then
                    If tn IsNot Nothing Then tns2 = tn.Nodes
                    s = Element.Comments.FullName : If s <> "" Then s = " (" & s & ")"
                    tn = tns2.Add(Element.DisplayText & s) : tn.Tag = Element : If s <> "" Then tn.ForeColor = Color.Green
                    Element.Tag.TreeNode = tn
                End If
            End If
            If Element.Item.Definitions.Count = 0 Then
                For i = 1 To m_List.Count
                    cmbr2 = m_List.Item(i)
                    If (Not ReferenceEquals(Element, cmbr2)) AndAlso ReferenceEquals(Element.Item.Spice, cmbr2.Item.Spice) Then
                        tn = cmbr2.Tag.TreeNode
                        If tn Is Nothing Then
                            This_CreateItem(cmbr2)
                        Else
                            If tn.Parent IsNot Element.Tag.TreeNode Then
                                tn.Remove()
                                Element.Tag.TreeNode.Nodes.Add(tn)
                            End If
                        End If
                    End If
                Next
            End If
        End Sub
        Protected Friend Overrides Sub UpdateItem(ByVal Element As CombinationReference)
            If (Element Is Nothing) OrElse (Element.Tag.TreeNode Is Nothing) Then Exit Sub
            Dim tn As TreeNode = Element.Tag.TreeNode
            Dim elText As String = Element.DisplayText, elCom As String = Element.Comments.FullName

            MyBase.UpdateItem(Element)

            If elCom <> "" Then
                elText = elText & " (" & elCom & ")"
            End If
            If tn.Text <> elText Then
                tn.Text = elText
            End If
        End Sub
        Protected Friend Overrides Sub RemoveItem(ByVal Element As CombinationReference)
            Dim i As Integer, j As Integer
            Dim tn As TreeNode, tns As TreeNodeCollection, tnsP As TreeNodeCollection
            MyBase.RemoveItem(Element)

            tn = Element.Tag.TreeNode
            If tn IsNot Nothing Then
                tns = tn.Nodes
                If tns.Count > 0 Then
                    tnsP = m_TV.Nodes
                    j = tn.Index
                    For i = tns.Count To 1 Step -1
                        tn = tns(i - 1)
                        tn.Remove()
                        tnsP.Insert(j, tn)
                    Next
                    tn = Element.Tag.TreeNode
                End If
                tn.Remove()
                tn.Tag = Nothing
                Element.Tag.TreeNode = Nothing
            End If
        End Sub
        Protected Overrides Sub ClearItems()
            MyBase.ClearItems()

            If m_TV Is Nothing Then Return

            m_TV.SelectedNode = Nothing
            ClearTreeNodes(m_TV.Nodes)
        End Sub
        Private Sub ClearTreeNodes(ByVal Nodes As TreeNodeCollection)
            Dim i As Integer
            Dim tni As TreeNode
            Dim el As Element

            Try
                For i = 1 To Nodes.Count
                    tni = Nodes.Item(i - 1)
                    el = tni.Tag
                    If el IsNot Nothing Then el.Tag.TreeNode = Nothing
                    ClearTreeNodes(tni.Nodes)
                Next
                Nodes.Clear()
            Catch ex As Exception
                'The window must not exist anymore - no need to do anything
            End Try
        End Sub

        Protected Overrides Sub SelectItem(Optional ByVal AutoSelect As Boolean = True, Optional ByVal NonAuto_Count As Integer = 0)
            Dim b As Boolean, s As String
            MyBase.SelectItem(AutoSelect, NonAuto_Count)

            If (m_tEdit Is Nothing) Then
                Return
            End If

            b = (Not m_MFIsReadOnly) And m_tRemove.Enabled
            If Not m_MFIsReadOnly Then
                m_tEdit.Enabled = b : m_mEdit.Enabled = b
                m_tCW.Enabled = b : m_mCW.Enabled = b
                s = ","
                If (m_LV IsNot Nothing) AndAlso (m_LV.SelectedItems.Count > 0) Then
                    Dim combr As CombinationReference = m_LV.SelectedItems(0).Tag
                    Select Case combr.ConnectWith
                        Case CombinationReference.ConnectWithEnum.And
                            s = "+"
                            m_tCW_N.Checked = False : m_tCW_A.Checked = True : m_tCW_O.Checked = False
                        Case CombinationReference.ConnectWithEnum.Or
                            s = " ή "
                            m_tCW_N.Checked = False : m_tCW_A.Checked = False : m_tCW_O.Checked = True
                        Case Else
                            m_tCW_N.Checked = True : m_tCW_A.Checked = False : m_tCW_O.Checked = False
                    End Select
                Else
                    m_tCW_N.Checked = True : m_tCW_A.Checked = False : m_tCW_O.Checked = False
                End If
                m_mCW_N.Checked = m_tCW_N.Checked : m_mCW_A.Checked = m_tCW_A.Checked : m_mCW_O.Checked = m_tCW_O.Checked
                m_tCW.Text = s
            End If
        End Sub
#End Region

#Region "Drag'N'Drop"
        Protected Overrides Sub AddChild(ByVal Item As Element, ByVal KeyState As Integer)
            AddChild(Item, KeyState, m_List)
        End Sub

        Public Overloads Shared Function CanDrop(ByVal e As DragEventArgs, ByVal List As CombinationReferenceList) As Boolean
            Dim p As Element = List.Parent
            Dim el As Element

            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode") Then
                Dim tn As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

                el = tn.Tag

                If el Is Nothing Then Return False

                If p Is Nothing Then
                    Return List.CanHaveAsChild(el)
                Else
                    Return p.CanHaveAsChild(el)
                End If
            ElseIf e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection") Then
                Dim lvis As ListView.SelectedListViewItemCollection
                Dim lvi As ListViewItem

                lvis = CType(e.Data.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection"), ListView.SelectedListViewItemCollection)

                For Each lvi In lvis
                    el = lvi.Tag

                    If el IsNot Nothing Then
                        If p Is Nothing Then
                            If List.CanHaveAsChild(el) Then Return True
                        Else
                            If p.CanHaveAsChild(el) Then Return True
                        End If
                    End If
                Next
            End If

            Return False
        End Function
        Public Overloads Shared Sub DragDrop(ByVal e As DragEventArgs, ByVal List As CombinationReferenceList)
            Dim el As Element

            If Not CanDrop(e, List) Then Return

            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode") Then
                Dim tn As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

                el = tn.Tag

                AddChild(el, e.KeyState, List)
            ElseIf e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection") Then
                Dim lvis As ListView.SelectedListViewItemCollection
                Dim lvi As ListViewItem

                lvis = CType(e.Data.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection"), ListView.SelectedListViewItemCollection)

                For Each lvi In lvis
                    el = lvi.Tag

                    If el IsNot Nothing Then
                        AddChild(el, e.KeyState, List)
                    End If
                Next
            End If
        End Sub
        Protected Overloads Shared Sub AddChild(ByVal Item As Element, ByVal KeyState As Integer, ByVal List As CombinationReferenceList)
            Dim cmbr As CombinationReference = Nothing

            If TypeOf Item Is Combination Then
                cmbr = New CombinationReference(CType(Item, Combination))
            ElseIf TypeOf Item Is CombinationReference Then
                cmbr = New CombinationReference(CType(Item, CombinationReference).Item)
            End If

            If (cmbr IsNot Nothing) AndAlso ((KeyState = 4) OrElse frmDefineCombination.EditCombination(cmbr)) Then
                cmbr.Item = cmbr.Item.Spice.DerivedCombinations.Add(cmbr.Item, False)
                List.Add(cmbr)
            End If
        End Sub
#End Region

        Protected Overrides Sub Localize(ByVal Source As MUISource)
            m_Text = Source("General", "ListCaptions", "Combinations")
            MyBase.Localize(Source)
            LocalizeToolItem(m_tEdit, m_mEdit, Source("General", "ItemsCommands", "Edit"))
            LocalizeToolItem(m_tCW, m_mCW, Source("General", "ItemsCommands", "ConnectWith"))
            LocalizeToolItem(m_tCW_N, m_mCW_N, Source("General", "ItemsCommands", "ConnectWithComma"))
            LocalizeToolItem(m_tCW_A, m_mCW_A, Source("General", "ItemsCommands", "ConnectWithAnd"))
            LocalizeToolItem(m_tCW_O, m_mCW_O, Source("General", "ItemsCommands", "ConnectWithOr"))
        End Sub
    End Class
    Class FoodReferenceListManager
        Inherits ReferenceListManager(Of Food, FoodReference, FoodReferenceList)

#Region "Events"
        Protected Overrides Sub ListView_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles m_View.ItemActivate
            Dim fdr As FoodReference

            If m_LV.SelectedItems.Count = 0 Then
                Return
            End If

            fdr = m_LV.SelectedItems(0).Tag

            frmFoods.SelectItem(fdr.Item)
        End Sub

#Region "Buttons"
        Protected Overrides Sub Go_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles m_tGo.Click, m_mGo.Click
            Dim lvi As ListViewItem, rel As FoodReference

            lvi = m_LV.SelectedItems(0)

            rel = lvi.Tag
            frmFoods.SelectItem(rel.Item)
        End Sub
        Protected Overrides Sub Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles m_tAdd.Click, m_mAdd.Click
            'frmFoods.ShowMe()
            MsgBox("Drag-and-drop or cut/copy-paste any item from either the Foods tree or from any list")
        End Sub

        Private Sub Copy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCopy.Click, m_mCopy.Click
            Dim i As Integer
            Dim lvis As ListView.SelectedListViewItemCollection
            Dim ob As Object
            Dim fdrs() As FoodReference

            lvis = m_LV.SelectedItems

            If lvis.Count = 0 Then
                Return
            End If

            ReDim fdrs(lvis.Count)
            For i = 1 To lvis.Count
                ob = lvis.Item(i - 1).Tag
                If (ob IsNot Nothing) AndAlso (TypeOf ob Is FoodReference) Then
                    fdrs(i) = ob
                Else
                    Debug.Fail("A no-item in collection")
                    fdrs(i) = Nothing
                    'MsgBox("Nothing!")
                End If
            Next

            m_Clipboard.FoodsOp = Clipboard.Operation.Copy
            m_Clipboard.FoodsSource = m_List
            With m_Clipboard.Foods
                .BatchStart()
                .Clear()
                For i = 1 To UBound(fdrs)
                    .Add(fdrs(i))
                Next
                .BatchEnd()
            End With
        End Sub
        Private Sub Cut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tCut.Click, m_mCut.Click
            Dim i As Integer
            Dim lvis As ListView.SelectedListViewItemCollection
            Dim ob As Object
            Dim fdrs() As FoodReference

            lvis = m_LV.SelectedItems

            If lvis.Count = 0 Then
                Return
            End If

            ReDim fdrs(lvis.Count)
            For i = 1 To lvis.Count
                ob = lvis.Item(i - 1).Tag
                If (ob IsNot Nothing) AndAlso (TypeOf ob Is CombinationReference) Then
                    fdrs(i) = ob
                Else
                    Debug.Fail("A no-item in collection")
                    fdrs(i) = Nothing
                    'MsgBox("Nothing!")
                End If
            Next

            m_Clipboard.FoodsOp = Clipboard.Operation.Cut
            m_Clipboard.FoodsSource = m_List
            With m_Clipboard.Foods
                .BatchStart()
                .Clear()
                For i = 1 To UBound(fdrs)
                    .Add(fdrs(i))
                Next
                .BatchEnd()
            End With
        End Sub
        Private Sub Paste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_tPaste.Click, m_mPaste.Click
            Dim i As Integer
            Dim fdr As FoodReference
            Dim fdrl As FoodReferenceList

            With m_Clipboard.Foods
                If .Count = 0 Then Return

                'Add them
                m_List.BatchStart()
                For i = 1 To .Count
                    m_List.Add(.Item(i))
                Next
                m_List.BatchEnd()
                'Remove cut items from clipboard and original source
                If m_Clipboard.FoodsOp = Clipboard.Operation.Cut Then
                    fdrl = m_Clipboard.FoodsSource

                    fdrl.BatchStart()
                    For i = 1 To .Count
                        fdr = .Item(i)
                        fdr = fdrl.Item(fdr)
                        If fdr IsNot Nothing Then
                            fdr.Remove()
                        End If
                    Next
                    fdrl.BatchEnd()

                    m_Clipboard.FoodsOp = Core.Clipboard.Operation.Copy
                    If m_Clipboard.FoodOp = Core.Clipboard.Operation.Cut Then m_Clipboard.FoodOp = Core.Clipboard.Operation.Copy
                End If
            End With
        End Sub
#End Region

        Protected Overrides Sub m_Clipboard_ItemChanged(ByVal Item As Clipboard.ItemType, ByVal InnerEvent As Object)
            If (m_tPaste IsNot Nothing) AndAlso (m_Clipboard IsNot Nothing) Then
                m_tPaste.Enabled = (m_Clipboard.Foods.Count > 0) : m_mPaste.Enabled = m_tPaste.Enabled
            End If
        End Sub
#End Region

#Region "Init/Terminate"
        Sub New()
            Localize(MyMUISources.Current)
        End Sub
#End Region

#Region "Methods"
#Region "Items"
        Protected Friend Overrides Sub CreateItem(ByVal Element As FoodReference)
            Dim el As Element = m_List.Parent
            Dim cmbr As CombinationReference
            Dim elCom As String = ""

            MyBase.CreateItem(Element)

            If (el Is Nothing) OrElse (Not (TypeOf el Is Combination)) Then Return

            cmbr = Element.Item.Combinations(CType(el, Combination))
            If cmbr IsNot Nothing Then
                elCom = cmbr.Comments.FullName
            End If

            Element.Tag.ListViewItem.SubItems(1).Text = elCom
        End Sub
        Protected Friend Overrides Sub UpdateItem(ByVal Element As FoodReference)
            Dim el As Element = m_List.Parent
            Dim cmbr As CombinationReference
            Dim elCom As String = ""

            MyBase.UpdateItem(Element)

            If (el IsNot Nothing) AndAlso (TypeOf el Is Combination) Then
                cmbr = Element.Item.Combinations(CType(el, Combination))
                If cmbr IsNot Nothing Then
                    elCom = cmbr.Comments.FullName
                End If
            End If

            If (Element IsNot Nothing) AndAlso (Element.Tag.ListViewItem IsNot Nothing) Then
                Element.Tag.ListViewItem.SubItems(1).Text = elCom
            End If
        End Sub
#End Region

#Region "Drag'N'Drop"
        Protected Overrides Sub AddChild(ByVal Item As Element, ByVal KeyState As Integer)
            AddChild(Item, KeyState, m_List)
        End Sub

#Region "Shared"
        Public Overloads Shared Function CanDrop(ByVal e As DragEventArgs, ByVal List As FoodReferenceList) As Boolean
            Dim p As Element = List.Parent
            Dim el As Element

            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode") Then
                Dim tn As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

                el = tn.Tag

                If el Is Nothing Then Return False

                If p Is Nothing Then
                    Return List.CanHaveAsChild(el)
                Else
                    Return p.CanHaveAsChild(el)
                End If
            ElseIf e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection") Then
                Dim lvis As ListView.SelectedListViewItemCollection
                Dim lvi As ListViewItem

                lvis = CType(e.Data.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection"), ListView.SelectedListViewItemCollection)

                For Each lvi In lvis
                    el = lvi.Tag

                    If el IsNot Nothing Then
                        If p Is Nothing Then
                            If List.CanHaveAsChild(el) Then Return True
                        Else
                            If p.CanHaveAsChild(el) Then Return True
                        End If
                    End If
                Next
            End If

            Return False
        End Function
        Public Overloads Shared Sub DragDrop(ByVal e As DragEventArgs, ByVal List As FoodReferenceList)
            Dim el As Element

            If Not CanDrop(e, List) Then Return

            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode") Then
                Dim tn As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

                el = tn.Tag

                AddChild(el, e.KeyState, List)
            ElseIf e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection") Then
                Dim lvis As ListView.SelectedListViewItemCollection
                Dim lvi As ListViewItem

                lvis = CType(e.Data.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection"), ListView.SelectedListViewItemCollection)

                For Each lvi In lvis
                    el = lvi.Tag

                    If el IsNot Nothing Then
                        AddChild(el, e.KeyState, List)
                    End If
                Next
            End If
        End Sub
        Protected Overloads Shared Sub AddChild(ByVal Item As Element, ByVal KeyState As Integer, ByVal List As FoodReferenceList)
            Dim fdr As FoodReference = Nothing

            If TypeOf Item Is Food Then
                fdr = New FoodReference(CType(Item, Food))
            ElseIf TypeOf Item Is FoodReference Then
                fdr = Item
            End If

            If fdr IsNot Nothing Then
                List.Add(fdr)
            End If
        End Sub
#End Region
#End Region
#End Region

        Protected Overrides Sub Localize(ByVal Source As MUISource)
            m_Text = Source("General", "ListCaptions", "Foods")
            MyBase.Localize(Source)
        End Sub
    End Class
End Namespace