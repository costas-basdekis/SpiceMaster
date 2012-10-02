Imports System.Windows.Forms

Imports SpiceMaster.Core

Public Class frmSpices

#Region "Variables"
	Dim m_Loaded As Boolean = False

	Friend MyWP As New My.SpiceMaster.Settings.FormOptions.WindowPos, MyWP_XML As Xml.XmlDocument = mdiMain.MyWP_XML

	Dim WithEvents MyDocument As Document = mdiMain.MyDocument
	Dim WithEvents MySpices As SpiceList = MyDocument.Spices
	Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

	Dim m_SelectingItem As Boolean = False

	Dim WithEvents History As New NavigationHistory(Of Combination, CombinationReference, CombinationReferenceList)
	Dim m_SpicesManager As New SpiceListManager, m_SpicesCommentsManager As New CommentListManager
	Dim m_CombinationsManger As New CombinationReferenceListManager
	Dim m_FoodsManger As New FoodReferenceListManager
#End Region

#Region "Document"
	Private Sub MyDocument_Loaded() Handles MyDocument.Loaded
		ReCreateUI()
	End Sub
#End Region

#Region "History"
	Private Sub History_ActivateItem(ByVal Item As Core.Combination) Handles History.ActivateItem
		SelectItem(Item)
	End Sub
#End Region

#Region "UI"
	Private Sub CreateUI()
        If Not m_Loaded Then ReCreateUI()
	End Sub
	Private Sub ReCreateUI()
		History.SetControls(tsBrowser, tsBrowser_tssbBack, tsBrowser_tssbForward, tsBrowser_tstbName, tsBrowser_tsbGoTo)
		m_CombinationsManger.SetControls(Nothing, lvCombinations, tsCombinations, gbCombinations, ManageFlags.CheckWhenAdding Or ManageFlags.ShowComments)
		m_FoodsManger.SetControls(Nothing, lvFoods, tsFoods, gbFoods, ManageFlags.ShowComments)
		m_SpicesCommentsManager.SetControls(Nothing, lvSpicesComments, tsSpicesComments, gbSpicesComments)
        m_SpicesManager.SetControls(MySpices, tvSpices, tsSpices, gbSpices, History, m_CombinationsManger, m_FoodsManger)

        Localize(MyMUISources.Current)
        LoadSUS()

		m_Loaded = True
	End Sub
#End Region

#Region "Form"
	Friend Sub ShowMe()
		If m_Loaded Then
			Visible = True
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
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
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
	End Sub

	Private Sub tvSpices_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvSpices.AfterSelect
		If (e.Node Is Nothing) OrElse (e.Node.Tag Is Nothing) OrElse (Not (TypeOf e.Node.Tag Is Combination)) Then
            m_SpicesCommentsManager.List = Nothing
		Else
            m_SpicesCommentsManager.List = CType(e.Node.Tag, Combination).Comments
		End If
	End Sub
#End Region

#Region "Navigate consumers"
	Friend Sub SelectItem(ByVal Item As Combination)
		m_SpicesManager.SelectItem(Item)
	End Sub
#End Region

#Region "StartUp Settings"
	Sub LoadSUS()
		Dim Splits() As Integer = {40, 80, 80}, Headers() As Integer = {80, 80, 80, 80, 80, 80}, ListsViews() As Integer = {View.Details, View.Details}

		With MyWP
			.XMLDocument = MyWP_XML : .FormName = "Spices" : .Form = Me

			.Load(FormWindowState.Normal, mdiMain.ClientRectangle.Width / 2 - 2, 0, mdiMain.ClientRectangle.Width / 2 - 2, mdiMain.ClientRectangle.Height / 1.2, Splits, Headers, ListsViews)

			.GetSplitter(scMain, 0)
			.GetSplitter(scManage, 1)
			.GetSplitter(scView, 2)
			.GetList(lvCombinations, 2, 0, 0)
			.GetList(lvFoods, 2, 2, 1)
			.GetList(lvSpicesComments, 2, 4)
		End With
	End Sub
	Sub SaveSUS()
		With MyWP
			.SetSplitter(scMain, 0)
			.SetSplitter(scManage, 1)
			.SetSplitter(scView, 2)
			.SetList(lvCombinations, 2, 0, 0)
			.SetList(lvFoods, 2, 2, 1)
			.SetList(lvSpicesComments, 2, 4)

			.Save()
		End With
	End Sub
#End Region

#Region "MUI"
	Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
		Text = Source("Forms", "Spices", "FormCaption")
        tsBrowser_tssbRefresh.Text = Source("General", "Navigation", "Refresh")
	End Sub
#End Region
End Class