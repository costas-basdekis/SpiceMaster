Imports System.Windows.Forms

Imports SpiceMaster.Core

Public Class frmMyShelf

#Region "Variables"
    Dim m_Loaded As Boolean = False

    Friend MyWP As New My.SpiceMaster.Settings.FormOptions.WindowPos, MyWP_XML As Xml.XmlDocument = mdiMain.MyWP_XML

    Dim WithEvents MyDocument As Document = mdiMain.MyDocument
    Dim WithEvents MyShelf As CombinationReferenceList = MyDocument.MyShelf
    Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

    Dim m_SpicesManager As New CombinationReferenceListManager
#End Region

#Region "Document"
    Private Sub MyDocument_Loaded() Handles MyDocument.Loaded
        ReCreateUI()
    End Sub
#End Region

#Region "UI"
    Private Sub CreateUI()
        If Not m_Loaded Then ReCreateUI()
    End Sub
    Private Sub ReCreateUI()
        m_SpicesManager.SetControls(MyShelf, lvCombinations, tsCombinations, gbCombinations)

        Localize(MyMUISources.Current)
        LoadSUS()

        m_Loaded = True
    End Sub
#End Region

#Region "Form"
    Friend Sub ShowMe()
        If Not Visible Then Me.Show(mdiMain)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Normal
        End If
        Me.Activate()
        Me.BringToFront()
        CreateUI()
    End Sub
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        CreateUI()
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSUS()
    End Sub

    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
#End Region

#Region "StartUp Settings"
    Sub LoadSUS()
        Dim Headers() As Integer = {80, 80}, ListsViews() As Integer = {View.Details}

        With MyWP
            .XMLDocument = MyWP_XML : .FormName = "MyShelf" : .Form = Me

            .Load(FormWindowState.Normal, Me.Left, Me.Top, Me.Width, Me.Height, , Headers, ListsViews)

            .GetList(lvCombinations, 2, 0, 0)
        End With
    End Sub
    Sub SaveSUS()
        With MyWP
            .SetList(lvCombinations, 2, 0, 0)

            .Save()
        End With
    End Sub
#End Region

#Region "MUI"
    Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
        Text = Source("Forms", "MyShelf", "FormCaption")
    End Sub
#End Region
End Class