Imports System.Windows.Forms
Imports SpiceMaster.Core

Public NotInheritable Class frmAbout

#Region "Variables"
	Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

	Dim TitleText As String, VersionText As String
#End Region

#Region "Form"
	Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		' Set the title of the form.
		Dim ApplicationTitle As String

		Localize(MyMUISources.Current)

		If My.Application.Info.Title <> "" Then
			ApplicationTitle = My.Application.Info.Title
		Else
			ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
		End If
		Me.Text = String.Format(TitleText & " {0}", ApplicationTitle)
		' Initialize all of the text displayed on the About Box.
		' TODO: Customize the application's assembly information in the "Application" pane of the project 
		'    properties dialog (under the "Project" menu).
		Me.LabelProductName.Text = My.Application.Info.ProductName
		Me.LabelVersion.Text = String.Format(VersionText & " {0}", My.Application.Info.Version.ToString)
		Me.LabelCopyright.Text = My.Application.Info.Copyright
		Me.LabelCompanyName.Text = My.Application.Info.CompanyName
		Me.TextBoxDescription.Text = My.Application.Info.Description
	End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
#End Region

#Region "MUI"
	Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
		TitleText = Source("Forms", "About", "FormCaption")
		VersionText = Source("Forms", "About", "General", "VersionCaption")
	End Sub
#End Region
End Class