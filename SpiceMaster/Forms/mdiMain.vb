Imports System.Windows.Forms
Imports System.IO

Imports SpiceMaster.Core

Public Class mdiMain

#Region "Constants"
    Dim m_SB_Ready As String
    Dim m_SB_SelectFile As String
    Dim m_SB_Working As String
    Dim m_SB_Failed As String
    Dim m_CouldNotSaveFile As String, m_CouldNotLoadFile As String, m_UntitledDocument As String, m_DocumentChanged As String, m_SaveInAnotherFile As String, m_SaveDocument As String
#End Region

#Region "Variables"
    Friend WithEvents MyRDL As New RecentDocumentsList
    Friend WithEvents MyDocument As New Document
    Friend WithEvents MyMUISources As New MUISourceCollection
    Friend MyWP As New My.SpiceMaster.Settings.FormOptions.WindowPos, MyWP_XML As New Xml.XmlDocument

    Dim m_RDLMenuItems(1) As ToolStripMenuItem
#End Region

#Region "Spice document"
    Private Sub MySpiceDocument_Loaded() Handles MyDocument.Loaded
        If My.Settings.StartUp_LoadFile Then
            If My.Settings.StartUp_LoadMode = My.SpiceMaster.Settings.AutoLoadFile.Mode.LastLoaded Then
                My.Settings.StartUp_FileName = MyDocument.FilePath
                My.Settings.Save()
            End If
        End If
        If MyDocument.FilePath <> "" Then
            MyRDL.Add(MyDocument.FilePath)
            Text = String.Format("SpiceMaster - [{0}]", MyDocument.FilePath)
        Else
            Text = String.Format("SpiceMaster - [{0}]", m_UntitledDocument)
        End If
    End Sub
    Private Sub MyDocument_HTMLRefreshed() Handles MyDocument.HTMLRefreshed
        Text = String.Format("SpiceMaster - [{0}{1}]", IIf(MyDocument.FilePath <> "", MyDocument.FilePath, m_UntitledDocument), IIf(MyDocument.ChangedSinceLastSaved, " *", ""))
    End Sub

    Private Function BeforeDocClose() As Boolean
        If MyDocument.ChangedSinceLastSaved Then
            Select Case MsgBox(m_DocumentChanged, MsgBoxStyle.YesNoCancel, m_SaveDocument)
                Case MsgBoxResult.Yes
                    If MyDocument.FilePath = "" Then
                        msMain_tsmiFile_tsmiSave_Click(msMain_tsmiFile_tsmiSaveAs, Nothing)
                    Else
                        Select Case MsgBox(m_SaveInAnotherFile, MsgBoxStyle.YesNoCancel, m_SaveDocument)
                            Case MsgBoxResult.Yes
                                msMain_tsmiFile_tsmiSave_Click(msMain_tsmiFile_tsmiSaveAs, Nothing)
                            Case MsgBoxResult.No
                                MyDocument.Save(MyDocument.FilePath, New Document.SMDSaver)
                            Case MsgBoxResult.Cancel
                                Return False
                        End Select
                    End If
                    If MyDocument.ChangedSinceLastSaved Then Return False
                Case MsgBoxResult.Cancel
                    Return False
            End Select
        End If

        Return True
    End Function
#End Region

#Region "Menus"
    Private Sub msMain_tsmiFile_tsmiNew_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles msMain_tsmiFile_tsmiNew.Click
        If Not BeforeDocClose() Then Return

        MyDocument.[Clear]()
    End Sub
    Private Sub msMain_tsmiFile_tsmiOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles msMain_tsmiFile_tsmiOpen.Click
        Dim s As String = frmLoadFileSelect.SelectFile()

        If s = "" Then Return

        If Not BeforeDocClose() Then Return

        ssMain_tsslMain.Text = m_SB_SelectFile
        ssMain_tsslMain.Text = m_SB_Working
        If frmLoadFileProccess.LoadFile(MyDocument, s) Then
            ssMain_tsslMain.Text = m_SB_Ready
        Else
            ssMain_tsslMain.Text = m_SB_Failed
        End If
    End Sub
    Private Sub msMain_tsmiFile_tsmiSave_Click(sender As System.Object, e As EventArgs) Handles msMain_tsmiFile_tsmiSave.Click, msMain_tsmiFile_tsmiSaveAs.Click
        If (MyDocument.FilePath = "") Or (sender Is msMain_tsmiFile_tsmiSaveAs) Then
SelectFile:
            ssMain_tsslMain.Text = m_SB_SelectFile
            If sfdMain.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                MyDocument.FilePath = ssMain_tsslMain.Text
            Else
                ssMain_tsslMain.Text = m_SB_Ready
                Return
            End If
        End If

        ssMain_tsslMain.Text = m_SB_Working
        If Not MyDocument.Save(MyDocument.FilePath, New Document.SMDSaver) Then
            ssMain_tsslMain.Text = m_SB_Failed
            If MsgBox(m_CouldNotSaveFile, MsgBoxStyle.YesNo, m_SB_SelectFile) = MsgBoxResult.Yes Then
                GoTo SelectFile
            End If
        Else
            ssMain_tsslMain.Text = m_SB_Ready
            MyRDL.Add(MyDocument.FilePath)
            MyDocument_HTMLRefreshed()
        End If
    End Sub

    Private Sub msMain_tsmiFile_tsmiExportHTML_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles msMain_tsmiFile_tsmiExportHTML.Click
        Dim f As HTMLFile

SelectFile:
        ssMain_tsslMain.Text = m_SB_SelectFile
        If sfdExport.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            ssMain_tsslMain.Text = m_SB_Working
            Try
                f = New HTMLFile
                f.Path = sfdExport.FileName
                f.Open()
                f.Put(MyDocument.ExportHTML)
                f.Close(False)
                f.Path = ""
                ssMain_tsslMain.Text = m_SB_Ready
            Catch ex As Exception
                ssMain_tsslMain.Text = m_SB_Failed
                If MsgBox(m_CouldNotSaveFile, MsgBoxStyle.YesNo, m_SB_SelectFile) = MsgBoxResult.Yes Then
                    GoTo SelectFile
                End If
            End Try
        Else
            ssMain_tsslMain.Text = m_SB_Ready
        End If
    End Sub

    Private Sub msMain_tsmiFile_tsmiExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles msMain_tsmiFile_tsmiExit.Click
        Close()
    End Sub
#End Region

#Region "Form"
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        My.Settings.Reload()

        LoadMUIs()
        LoadSUS()
        MyRDL.Items = My.Settings.RecentDocuments

        Refresh()

        'frmDocumentChanges.Show()
        frmSpices.ShowMe()
        frmFoods.ShowMe()

        If My.Settings.StartUp_LoadFile AndAlso (My.Settings.StartUp_FileName <> "") Then
            frmLoadFileProccess.LoadFile(MyDocument)
        Else
            msMain_tsmiFile_tsmiOpen_Click(Nothing, Nothing)
        End If

        sfdMain.InitialDirectory = Directory.GetCurrentDirectory()
        sfdExport.InitialDirectory = Directory.GetCurrentDirectory()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not BeforeDocClose() Then e.Cancel = True : Return
        SaveSUS()
    End Sub

    Private Sub msMain_tsmiWindows_tsmiCascade_Click(ByVal sender As Object, ByVal e As EventArgs) Handles msMain_tsmiWindows_tsmiCascade.Click, msMain_tsmiWindows_tsmiTileVer.Click, msMain_tsmiWindows_tsmiTileHor.Click, msMain_tsmiWindows_tsmiArrIcons.Click
        If sender Is msMain_tsmiWindows_tsmiCascade Then
            LayoutMdi(MdiLayout.Cascade)
        ElseIf sender Is msMain_tsmiWindows_tsmiTileVer Then
            LayoutMdi(MdiLayout.TileVertical)
        ElseIf sender Is msMain_tsmiWindows_tsmiTileHor Then
            LayoutMdi(MdiLayout.TileHorizontal)
        ElseIf sender Is msMain_tsmiWindows_tsmiArrIcons Then
            LayoutMdi(MdiLayout.ArrangeIcons)
        End If
    End Sub
#End Region

#Region "Dialogs"
    Private Sub msMain_tsmiTools_tsmiOptions_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles msMain_tsmiTools_tsmiOptions.Click
        frmOptions.ShowDialog()
    End Sub
    Private Sub TranslationManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager.Click
        If Not frmLanguagesManager.Visible Then frmLanguagesManager.Show(Me)
    End Sub

    Private Sub msMain_tsmiHelp_tsmiAbout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles msMain_tsmiHelp_tsmiAbout.Click
        frmAbout.ShowDialog()
    End Sub
#End Region

#Region "Main forms"
    Private Sub Spices_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsMain_tsbSpices.Click, msMain_tsmiView_tsmiSpices.Click
        frmSpices.ShowMe()
    End Sub
    Private Sub Foods_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsMain_tsbFoods.Click, msMain_tsmiView_tsmiFoods.Click
        frmFoods.ShowMe()
    End Sub
    Private Sub Definitions_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsMain_tsbDefinitions.Click, msMain_tsmiView_tsmiDefinitions.Click
        frmDefineCombination.ShowMe()
    End Sub

    Private Sub AutoConvert_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsMain_tsbAutoConvert.Click, msMain_tsmiView_tsmiAutoConvertSpices.Click
        frmAutoConvertSpices.ShowDialog()
    End Sub
    Private Sub IWannaCook_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsMain_tsbIWannaCook.Click, msMain_tsmiView_tsmiIWannaCook.Click
        frmIWannaCook.ShowMe()
    End Sub
    Private Sub MyShelf_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsMain_tsbMyShelf.Click, msMain_tsmiView_tsmiMyShelf.Click
        frmMyShelf.ShowMe()
    End Sub
#End Region

#Region "StartUp Settings"
    Sub LoadSUS()
        Try
            MyWP_XML.LoadXml(My.Settings.StartUp_WindowPos)
        Catch ex As Exception
        End Try

        With MyWP
            .XMLDocument = MyWP_XML : .FormName = "Main" : .Form = Me

            .Load(FormWindowState.Maximized, 0, 0, 1024, 768)
        End With
    End Sub
    Sub SaveSUS()
        With MyWP
            .Save()
        End With

        My.Settings.StartUp_WindowPos = MyWP_XML.OuterXml
    End Sub
#End Region

#Region "MUI"
    Private Sub LoadMUIs()
        Dim i As Integer
        Dim ms As MUISource
        Dim tsmi As ToolStripMenuItem

        With MyMUISources
            For i = 1 To .Count
                ms = .Item(i)
                tsmi = msMain_tsmiTools_tsmiLanguage.DropDownItems.Add(ms.LanguageName)
                tsmi.Tag = ms
                AddHandler tsmi.Click, AddressOf MUISelect
            Next
        End With

        If My.Settings.StartUp_Language.StartsWith("_") Then
            ms = MyMUISources(My.Settings.StartUp_Language.Substring(1))
        Else
            ms = MyMUISources.ByFile(My.Settings.StartUp_Language)
        End If
        If ms Is Nothing Then ms = MyMUISources.ByFile(My.Settings.StartUp_Language)
        MyMUISources.Current = ms
    End Sub

    Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
        'Menus
        'Main menu
        'File
        msMain_tsmiFile.Text = Source("Forms", "Main", "Menus", "MainMenu", "File")
        msMain_tsmiFile_tsmiNew.Text = Source("Forms", "Main", "Menus", "MainMenu", "File", "New")
        msMain_tsmiFile_tsmiOpen.Text = Source("Forms", "Main", "Menus", "MainMenu", "File", "Open")
        msMain_tsmiFile_tsmiSave.Text = Source("Forms", "Main", "Menus", "MainMenu", "File", "Save")
        msMain_tsmiFile_tsmiSaveAs.Text = Source("Forms", "Main", "Menus", "MainMenu", "File", "SaveAs")
        msMain_tsmiFile_tsmiExportHTML.Text = Source("Forms", "Main", "Menus", "MainMenu", "File", "ExportHTML")
        msMain_tsmiFile_tsmiExit.Text = Source("Forms", "Main", "Menus", "MainMenu", "File", "Exit")
        'View
        msMain_tsmiView.Text = Source("Forms", "Main", "Menus", "MainMenu", "View")
        msMain_tsmiView_tsmiFoods.Text = Source("Forms", "Foods", "FormCaption") : tsMain_tsbFoods.Text = msMain_tsmiView_tsmiFoods.Text
        msMain_tsmiView_tsmiSpices.Text = Source("Forms", "Spices", "FormCaption") : tsMain_tsbSpices.Text = msMain_tsmiView_tsmiSpices.Text
        msMain_tsmiView_tsmiDefinitions.Text = Source("Forms", "DefineCombination", "FormCaption") : tsMain_tsbDefinitions.Text = msMain_tsmiView_tsmiDefinitions.Text
        msMain_tsmiView_tsmiAutoConvertSpices.Text = Source("Forms", "AutoConvertSpices", "FormCaption") : tsMain_tsbAutoConvert.Text = msMain_tsmiView_tsmiAutoConvertSpices.Text
        msMain_tsmiView_tsmiIWannaCook.Text = Source("Forms", "IWannaCook", "FormCaption") : tsMain_tsbIWannaCook.Text = msMain_tsmiView_tsmiIWannaCook.Text
        msMain_tsmiView_tsmiMyShelf.Text = Source("Forms", "MyShelf", "FormCaption") : tsMain_tsbMyShelf.Text = msMain_tsmiView_tsmiMyShelf.Text
        'Tools
        msMain_tsmiTools.Text = Source("Forms", "Main", "Menus", "MainMenu", "Tools")
        msMain_tsmiTools_tsmiOptions.Text = Source("Forms", "Options", "FormCaption")
        msMain_tsmiTools_tsmiLanguage.Text = Source("General", "ListItemsCaptions", "Language")
        msMain_tsmiTools_tsmiLanguage_tsmiLanguagesManager.Text = String.Format("{0}...", Source("Forms", "LanguagesManager", "FormCaption"))
        'Windows
        msMain_tsmiWindows.Text = Source("Forms", "Main", "Menus", "MainMenu", "Windows")
        msMain_tsmiWindows_tsmiCascade.Text = Source("Forms", "Main", "Menus", "MainMenu", "Windows", "Cascade")
        msMain_tsmiWindows_tsmiTileVer.Text = Source("Forms", "Main", "Menus", "MainMenu", "Windows", "TileVertical")
        msMain_tsmiWindows_tsmiTileHor.Text = Source("Forms", "Main", "Menus", "MainMenu", "Windows", "TileHorizontal")
        msMain_tsmiWindows_tsmiArrIcons.Text = Source("Forms", "Main", "Menus", "MainMenu", "Windows", "ArrangeIcons")
        'Help
        msMain_tsmiHelp.Text = Source("Forms", "Main", "Menus", "MainMenu", "Help")
        msMain_tsmiHelp_tsmiContents.Text = Source("Forms", "Main", "Menus", "MainMenu", "Help", "Contents")
        msMain_tsmiHelp_tsmiIndex.Text = Source("Forms", "Main", "Menus", "MainMenu", "Help", "Index")
        msMain_tsmiHelp_tsmiSearch.Text = Source("Forms", "Main", "Menus", "MainMenu", "Help", "Search")
        msMain_tsmiHelp_tsmiAbout.Text = Source("Forms", "Main", "Menus", "MainMenu", "Help", "About") & My.Application.Info.Title

        'Status messages
        m_SB_Ready = Source("Forms", "Main", "Messages", "Ready") : ssMain_tsslMain.Text = m_SB_Ready
        m_SB_SelectFile = Source("Forms", "Main", "Messages", "SelectFile")
        m_SB_Working = Source("Forms", "Main", "Messages", "Working")
        m_SB_Failed = Source("Forms", "Main", "Messages", "Failed")
        m_SaveDocument = Source("Forms", "Main", "Messages", "SaveDocument")
        m_CouldNotSaveFile = Source("Forms", "Main", "Messages", "CouldNotSaveFile")
        m_CouldNotLoadFile = Source("Forms", "Main", "Messages", "CouldNotLoadFile")
        m_UntitledDocument = Source("Forms", "Main", "Messages", "UntitledDocument")
        m_DocumentChanged = Source("Forms", "Main", "Messages", "DocumentChanged")
        m_SaveInAnotherFile = Source("Forms", "Main", "Messages", "SaveInAnotherFile")
        sfdMain.Title = m_SB_SelectFile
        sfdExport.Title = m_SB_SelectFile

        Dim i As Integer, tsmi As ToolStripMenuItem

        With msMain_tsmiTools_tsmiLanguage.DropDownItems
            For i = 1 To .Count
                If TypeOf (.Item(i - 1)) Is ToolStripMenuItem Then
                    tsmi = .Item(i - 1)
                    If (tsmi.Tag IsNot Nothing) AndAlso (ReferenceEquals(tsmi.Tag, Source)) Then
                        tsmi.Checked = True
                    Else
                        tsmi.Checked = False
                    End If
                End If
            Next
        End With

        If MyMUISources.Current.ReadOnly Then
            My.Settings.StartUp_Language = "_" & MyMUISources.Current.Language
        Else
            My.Settings.StartUp_Language = MyMUISources.Current.Path
        End If
    End Sub
    Friend Sub SourceAdded(ByVal Source As MUISource) Handles MyMUISources.SourceAdded
        Dim tsmi As ToolStripMenuItem
        tsmi = msMain_tsmiTools_tsmiLanguage.DropDownItems.Add(Source.Language)
        tsmi.Tag = Source
        AddHandler tsmi.Click, AddressOf MUISelect
    End Sub

    Private Sub MUISelect(ByVal sender As Object, ByVal e As EventArgs)
        Dim tsmi As ToolStripMenuItem

        If (sender Is Nothing) OrElse (Not (TypeOf sender Is ToolStripMenuItem)) Then Return

        tsmi = sender

        If (tsmi.Tag Is Nothing) OrElse (Not (TypeOf tsmi.Tag Is MUISource)) Then Return

        MyMUISources.Current = tsmi.Tag
    End Sub
#End Region

#Region "RDL"
    Private Sub RDL_Changed() Handles MyRDL.Changed
        For Each tsmi As ToolStripMenuItem In m_RDLMenuItems
            If Not (tsmi Is Nothing) Then msMain_tsmiFile.DropDownItems.Remove(tsmi)
        Next

        ReDim m_RDLMenuItems(MyRDL.Count)

        For i As Integer = 1 To m_RDLMenuItems.Length - 1
            Dim tsmi As ToolStripMenuItem

            Dim s As String = MyRDL.Items(i)
            tsmi = New ToolStripMenuItem(String.Format("&{0}. {1}", CStr(i), Path.GetFileName(s)))
            m_RDLMenuItems(i) = tsmi
            msMain_tsmiFile.DropDownItems.Insert(msMain_tsmiFile.DropDownItems.IndexOf(msMain_tsmiFile_tssSep3), tsmi)
            tsmi.Tag = s
            tsmi.ToolTipText = s
            AddHandler tsmi.Click, AddressOf RD_Click
        Next

        msMain_tsmiFile_tsmiNoRD.Visible = (m_RDLMenuItems.Length = 0)

        My.Settings.RecentDocuments = MyRDL.Items
    End Sub
    Private Sub RD_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim s As String

        If Not BeforeDocClose() Then Return

        s = CType(sender, ToolStripMenuItem).Tag
        If Not frmLoadFileProccess.LoadFile(MyDocument, s) Then
            If MsgBox(m_CouldNotLoadFile, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                MyRDL.Remove(s)
            End If
        End If
    End Sub
#End Region
End Class