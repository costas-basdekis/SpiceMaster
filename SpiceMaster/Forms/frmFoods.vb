Imports System.Windows.Forms

Imports SpiceMaster.Core

Public Class frmFoods

#Region "Variables"
    Dim m_Loaded As Boolean = False

    Friend MyWP As New My.SpiceMaster.Settings.FormOptions.WindowPos, MyWP_XML As Xml.XmlDocument = mdiMain.MyWP_XML

    Dim WithEvents MyDocument As Document = mdiMain.MyDocument
    Dim WithEvents MyFoods As FoodList = MyDocument.Foods
    Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

    Dim hFoods As New HTMLFile

    Dim WithEvents History As New NavigationHistory(Of Food, FoodReference, FoodReferenceList)
    Dim m_FoodsManager As New FoodListManager, m_FoodsCommentsManager As New CommentListManager
    Dim m_CombinationsManager As New CombinationReferenceListManager


    Dim ManageItem As Food = Nothing
    Dim m_SelectingItem As Boolean = False
    Dim m_FoodsRCNode As TreeNode = Nothing, m_FoodsRCItem As Food = Nothing
#End Region

#Region "Document"
    Private Sub MyDocument_HTMLRefreshed() Handles MyDocument.HTMLRefreshed
        RefreshHTML()
    End Sub
    Private Sub MyDocument_Loaded() Handles MyDocument.Loaded
        ReCreateUI()
    End Sub
#End Region

#Region "History"
    Private Sub History_ActivateItem(ByVal Item As Core.Food) Handles History.ActivateItem
        SelectItem(Item)
    End Sub
#End Region

#Region "UI"
    Private Sub CreateUI()
        If Not m_Loaded Then ReCreateUI()
        RefreshHTML()
    End Sub
    Private Sub ReCreateUI()
        History.SetControls(tsBrowser, tsBrowser_tssbBack, tsBrowser_tssbForward, tsBrowser_tstbName, tsBrowser_tsbGoTo)
        m_CombinationsManager.SetControls(Nothing, lvCombinations, tsCombinations, gbCombinations, ManageFlags.ShowComments)
        m_FoodsManager.SetControls(MyFoods, tvFoods, tsFoods, gbFoods, History, m_CombinationsManager)
        m_FoodsCommentsManager.SetControls(Nothing, lvFoodsComments, tsFoodsComments, gbFoodsComments)

        LoadSUS()
        Localize(MyMUISources.Current)

        m_Loaded = True
    End Sub

    Friend Sub RefreshHTML()
        hFoods.Open()
        hFoods.Put(MyDocument.InnerHTML)
        wbFoods.Navigate(hFoods.Path)
        'Apparently when it returns HRESULT E_FAIL it throws an exception
        Try
            wbFoods.Refresh()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Form"
    Friend Sub ShowMe()
        If m_Loaded And (Not Visible) Then
            Visible = True
            tvFoods.Nodes(0).Expand()
        Else
            Me.MdiParent = mdiMain
            Me.Show()
            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
            End If
            Me.Activate()
            Me.BringToFront()
            CreateUI()
        End If
    End Sub
    Private Sub frmFoods_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        CreateUI()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Visible = False
        End If
        SaveSUS()
    End Sub

    Private Sub tsBrowser_tssbRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsBrowser_tssbRefresh.Click
        ReCreateUI()
        MyDocument.RefreshHTML()
        RefreshHTML()
    End Sub

    Private Sub tsBrowser_tsbUp_Click(sender As Object, e As System.EventArgs) Handles tsBrowser_tsbUp.Click
        tvFoods.SelectedNode = tvFoods.SelectedNode.Parent
    End Sub
#End Region

#Region "Foods"
    'Private Sub tsBrowser_tsbUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsBrowse_tsbUp.Click
    '	Dim tn As TreeNode = tvFoods.SelectedNode
    '	Dim cat As Food = Nothing, catlist As FoodList

    '	If (tn Is Nothing) OrElse (tn.Parent Is Nothing) OrElse (tn.Tag Is Nothing) Then
    '		tsBrowse_tsbUp.Enabled = False
    '		Return
    '	End If

    '	cat = tn.Tag
    '	catlist = cat.Parent
    '	cat = catlist.Parent
    '	SelectItem(catlist.Parent)
    'End Sub
#End Region

#Region "Navigate producers"
    Private Sub tvFoods_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvFoods.AfterSelect
        tvFoods_NodeMouseClick(sender, Nothing)
    End Sub
    Private Sub tvFoods_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvFoods.NodeMouseClick
        If tvFoods.SelectedNode Is Nothing Then
            SelectItem(Nothing)
        Else
            SelectItem(tvFoods.SelectedNode.Tag)
        End If
    End Sub
#End Region

#Region "Navigate consumers"
    Friend Sub SelectItem(ByVal Item As Food)
        Dim fdl As FoodList, hte As HtmlElement, args(1) As Object

        m_FoodsManager.SelectItem(Item)

        If Item IsNot Nothing Then
            'Show comments
            m_FoodsCommentsManager.List = Item.Comments
            'Show item in HTML
            Do
                fdl = Item.Parent : If (fdl Is Nothing) OrElse (Not (TypeOf fdl.Parent Is Food)) Then Exit Do
                Item = fdl.Parent : If Item Is Nothing Then Exit Do
                If Item.HTML.Name <> "" Then
                    If wbFoods.Document IsNot Nothing Then
                        hte = wbFoods.Document.All(Item.HTML.Name)
                        If hte Is Nothing Then Exit Do
                        args(0) = hte.Id
                        args(1) = 1
                        wbFoods.Document.InvokeScript("SetVisibility", args)
                    End If
                End If
            Loop
            Item = tvFoods.SelectedNode.Tag
            If Item.HTML.Name <> "" Then
                If wbFoods.Document IsNot Nothing Then
                    hte = wbFoods.Document.All("_" & Item.HTML.Name)
                    If hte IsNot Nothing Then
                        hte.ScrollIntoView(True)
                    End If
                End If
            End If
        Else
            m_FoodsCommentsManager.List = Nothing
        End If

        tsBrowser_tsbUp.Enabled = (tvFoods.SelectedNode IsNot Nothing) AndAlso (tvFoods.SelectedNode.Parent IsNot Nothing) AndAlso (tvFoods.SelectedNode.Parent.Parent IsNot Nothing)
    End Sub

    Private Sub wbFoods_Navigating(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles wbFoods.Navigating
        Dim s As String, i As Integer
        Dim comb As Combination, cat As Food

        If e.Url.Fragment <> "" Then
            Select Case e.Url.Fragment
                Case "#MYPROLOGUE"
                Case "#MYFOODS"
                Case "#MYSOURCES"
                Case ""
                Case Else
                    e.Cancel = True
            End Select
        End If

        s = e.Url.Fragment

        If (Mid(s, 1, 2) = "#I") AndAlso (Len(s) > 2) Then
            i = Mid(s, 3)

            comb = Food.HTMLTag.UIDs.Item(i)

            frmSpices.SelectItem(comb)
        ElseIf Mid(s, 1, 2) = "#F" Then
            If Len(s) = 2 Then
                SelectItem(Nothing)
            Else
                i = Mid(s, 3)

                cat = Food.HTMLTag.UIDs.Item(i)

                SelectItem(cat)
            End If
        End If
    End Sub
#End Region

#Region "StartUp Settings"
    Sub LoadSUS()
        Dim Splits() As Integer = {40, 80, 80}, Headers() As Integer = {80, 80, 80, 80}, ListsViews() As Integer = {View.Tile}

        With MyWP
            .XMLDocument = MyWP_XML : .FormName = "Foods" : .Form = Me

            .Load(FormWindowState.Normal, 0, 0, mdiMain.DisplayRectangle.Width / 2 - 2, mdiMain.ClientRectangle.Height / 1.2, Splits, Headers, ListsViews)

            .GetSplitter(scMain, 0)
            .GetSplitter(scManage, 1)
            .GetSplitter(scView, 2)
            .GetList(lvCombinations, 2, 0, 0)
            .GetList(lvFoodsComments, 2, 2)
        End With
    End Sub
    Sub SaveSUS()
        With MyWP
            .SetSplitter(scMain, 0)
            .SetSplitter(scManage, 1)
            .SetSplitter(scView, 2)
            .SetList(lvCombinations, 2, 0, 0)
            .SetList(lvFoodsComments, 2, 2)

            .Save()
        End With
    End Sub
#End Region

#Region "MUI"
    Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
        Text = Source("Forms", "Foods", "FormCaption")
        'tsBrowser_tssbRefresh_Click(Nothing, Nothing)
        tsBrowser_tssbRefresh.Text = Source("General", "Navigation", "Refresh")
        tsBrowser_tsbUp.Text = Source("General", "Navigation", "GoUp")
    End Sub
#End Region
End Class