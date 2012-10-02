Imports System.Windows.Forms
Imports SpiceMaster.Core

Public Class frmProgress

#Region "Variables & Declarations"

#Region "Proccess Hook"
	Class ProccessHook

#Region "Variable"
		Friend m_PHB As ProccessHookBegin
#End Region

#Region "Events"
		Public Event UpdateCategory(ByVal NewText As String)
		Public Event UpdateItem(ByVal NewText As String)
		Public Event UpdateProgress(ByVal NewTotal As Integer, ByVal NewCurrent As Integer)
#End Region

#Region "Init/Terminate"
		Sub New(ByVal PHB As ProccessHookBegin)
			m_PHB = PHB
		End Sub
#End Region

#Region "Operations"
		Friend Sub Begin()
			'RaiseEvent BeginOperation()
			m_PHB.Begin(Me)
		End Sub

		Public WriteOnly Property Category() As String
			Set(ByVal value As String)
				RaiseEvent UpdateCategory(value)
			End Set
		End Property
		Public WriteOnly Property Item() As String
			Set(ByVal value As String)
				RaiseEvent UpdateItem(value)
			End Set
		End Property
		Public WriteOnly Property ProgressCurrent(ByVal NewTotal As Integer) As Integer
			Set(ByVal value As Integer)
				RaiseEvent UpdateProgress(NewTotal, value)
			End Set
		End Property
#End Region
	End Class
	Class ProccessHookBegin

#Region "Variables"
		Public CancelInvoked As Boolean = False
#End Region

#Region "Events"
		Public Event BeginOperation(ByVal ProccessHook As ProccessHook)
#End Region

#Region "Operations"
        Friend Sub Begin(ByVal ProccessHook As ProccessHook)
            CancelInvoked = False
            RaiseEvent BeginOperation(ProccessHook)
        End Sub
#End Region
	End Class
#End Region

	Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

	Dim m_Loaded As Boolean = False

	Dim WithEvents m_ProccesHook As ProccessHook
#End Region

#Region "Proccess Hook"
	Private Sub m_ProccesHook_UpdateCategory(ByVal NewText As String) Handles m_ProccesHook.UpdateCategory
		lblCategory.Text = NewText
	End Sub
	Private Sub m_ProccesHook_UpdateItem(ByVal NewText As String) Handles m_ProccesHook.UpdateItem
		lblItem.Text = NewText
	End Sub
	Private Sub m_ProccesHook_UpdateProgress(ByVal NewTotal As Integer, ByVal NewCurrent As Integer) Handles m_ProccesHook.UpdateProgress
		With pgMain
			.Minimum = 0
			.Maximum = NewTotal
			If NewCurrent > .Maximum Then NewCurrent = .Maximum
			.Value = NewCurrent
		End With
		My.Application.DoEvents()
	End Sub
#End Region

#Region "Form"
	Friend Function ShowMe(ByVal ParentForm As Form, ByVal Title As String, ByRef ProccesHookBegin As ProccessHookBegin, ByVal AllowCancel As Boolean) As Boolean
		Localize(MyMUISources.Current)
		If m_Loaded Then
			Visible = True
			Return False
		Else
			Me.Text = Title
			m_Loaded = True
			m_ProccesHook = New ProccessHook(ProccesHookBegin)
			Me.ShowDialog(ParentForm)
			Return True
		End If
	End Function
	Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Visible = True
		m_ProccesHook.Begin()
		Me.Close()
	End Sub
	Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		m_Loaded = False
	End Sub

	Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
		m_ProccesHook.m_PHB.CancelInvoked = True
	End Sub
#End Region

#Region "MUI"
	Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
		Text = Source("Forms", "Progress", "FormCaption")
		lblCategorylbl.Text = Source("Forms", "Progress", "General", "CategoryLabel")
		lblItemlbl.Text = Source("Forms", "Progress", "General", "ItemLabel")
		butCancel.Text = Source("General", "Buttons", "Cancel")
	End Sub
#End Region
End Class