Imports SpiceMaster.Core

Public Class frmOptions

#Region "Variables"
    Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources
#End Region

#Region "Form"
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        KeyPreview = True
        With My.Settings
            If .StartUp_LoadFile Then
                cbAutoLoadDB.Checked = True
                If .StartUp_LoadMode = My.SpiceMaster.Settings.AutoLoadFile.Mode.LastLoaded Then
                    rbLoadLast.Checked = True
                Else
                    rbLoadSpecific.Checked = True
                End If
                tbSpecificFile.Text = .StartUp_FileName
            Else
                cbAutoLoadDB.Checked = False
            End If
            rbLoadSpecific_CheckedChanged(Nothing, Nothing)
        End With

        Localize(MyMUISources.Current)
    End Sub

    Private Sub cbAutoLoadDB_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbAutoLoadDB.CheckedChanged
        With cbAutoLoadDB
            rbLoadLast.Enabled = .Checked
            rbLoadSpecific.Enabled = .Checked
            rbLoadSpecific_CheckedChanged(Nothing, Nothing)
        End With
    End Sub

    Private Sub rbLoadSpecific_CheckedChanged(ByVal sender As System.Object)
        rbLoadSpecific_CheckedChanged(sender, Nothing)
    End Sub

    Private Sub rbLoadSpecific_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbLoadSpecific.CheckedChanged
        With rbLoadSpecific
            butSelectFile.Enabled = .Checked
            tbSpecificFile.Enabled = .Checked
        End With
    End Sub

    Private Sub butSelectFile_Click(ByVal sender As System.Object)
        butSelectFile_Click(sender, Nothing)
    End Sub

    Private Sub butSelectFile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles butSelectFile.Click
        If ofdMain.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbSpecificFile.Text = ofdMain.FileName
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles OK_Button.Click
        With My.Settings
            If cbAutoLoadDB.Checked Then
                .StartUp_LoadFile = True
                If rbLoadLast.Checked Then
                    .StartUp_LoadMode = My.SpiceMaster.Settings.AutoLoadFile.Mode.LastLoaded
                    .StartUp_FileName = mdiMain.MyDocument.FilePath
                Else
                    .StartUp_LoadMode = My.SpiceMaster.Settings.AutoLoadFile.Mode.Specific
                    .StartUp_FileName = tbSpecificFile.Text
                End If
                .StartUp_LoadFile = True
            Else
                .StartUp_LoadFile = False
            End If
            .Save()
        End With
        DialogResult = Windows.Forms.DialogResult.OK
        Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Close()
    End Sub
#End Region

#Region "MUI"
    Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
        Text = Source("Forms", "Options", "FormCaption")
        tcMain_tpOptions.Text = Source("Forms", "Options", "Tabs", "Startup", "TabCaption")
        cbAutoLoadDB.Text = Source("Forms", "Options", "Tabs", "Startup", "AutoLoadDB", "AutoLoadDBCaption")
        rbLoadLast.Text = Source("Forms", "Options", "Tabs", "Startup", "AutoLoadDB", "LastLoadedFileCaption")
        rbLoadSpecific.Text = Source("Forms", "Options", "Tabs", "Startup", "AutoLoadDB", "SpecificFileCaption")
        butSelectFile.Text = Source("Forms", "Options", "Tabs", "Startup", "AutoLoadDB", "SelectFile")
        OK_Button.Text = Source("General", "Buttons", "OK")
        Cancel_Button.Text = Source("General", "Buttons", "Cancel")
    End Sub
#End Region
End Class