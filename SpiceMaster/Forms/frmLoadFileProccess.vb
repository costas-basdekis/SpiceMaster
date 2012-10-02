Imports SpiceMaster.Core

Public NotInheritable Class frmLoadFileProccess
    Friend MyDocument As Document, FilePath As String, RetVal As Boolean

    Private WithEvents m_LoadStatus As New Document.LoadStatus
    Private m_StillLoading As Boolean = True

    Dim WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources
    Dim m_LoadingDocument As String, m_FailedToLoad As String

    Friend Function LoadFile(Target As Document, Optional SpecificDocument As String = "") As Boolean
        MyDocument = Target
        FilePath = SpecificDocument
        RetVal = False

        If Not Visible Then ShowDialog(mdiMain)

        Return RetVal
    End Function

    Private Sub frmLoadFileProcessScreen_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If Not m_StillLoading Then Close()
        End If
    End Sub
    Private Sub frmLoadFileProcessScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        m_StillLoading = True
    End Sub
    Private Sub LoadFileProcessScreen_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Localize(MyMUISources.Current)
        butClose.Visible = False
        lblLoading.Text = m_LoadingDocument
        lblLoading.ForeColor = SystemColors.Highlight
        lblLoading.Left = (Width - lblLoading.Width) / 2
        lblDocument.Text = ""
        Refresh()
        Application.DoEvents()

#If DEBUG Then
        If FilePath <> "" Then
            'Load specific file
            Close()
            RetVal = LoadDoc(FilePath)
        Else
            'Load a simple document
            Debug_Startup()
            Close()
            RetVal = True
        End If
#Else
        If FilePath <> "" Then
            'Load specific file
            RetVal = LoadDoc(FilePath)
        ElseIf My.Application.CommandLineArgs.Count > 0 Then
            'Load the document passed in the arguments
            RetVal = LoadDoc(My.Application.CommandLineArgs.Item(0))
        ElseIf My.Settings.StartUp_LoadFile Then
            'Load last loaded one
            RetVal = LoadDoc(My.Settings.StartUp_FileName)
        End If
#End If
    End Sub

    Private Sub Debug_Startup()
        Dim i As Integer, j As Integer
        Dim s As Spice, d As Definition, c As Combination

        lblDocument.Text = "Simple created document for debbugging purposes"

        With MyDocument
            'Foods
            With .Foods
                With .Add("Stews")
                    .Foods.Add("BBQ")
                End With
                For i = 1 To 5
                    With .Add("Some category #" & CStr(i)).Foods
                        For j = 1 To 2
                            .Add("Some subcategory #" & CStr(i) & "." & CStr(j))
                        Next
                    End With
                Next
            End With
            'Definitions
            With .Definitions
                d = .Add("b.", "beta")
                .Add("f.", "fi")
                .Add("h.", "heta")
            End With
            'Spices
            With .Spices
                .Add("Boldo")
                .Add("Epazote")
                .Add("Cicely")
                .Add("Boldo b.")
                s = .Add("Agioban f.")
                c = New Combination(s)
                c.Definitions.Add(New DefinitionReference(d))
                s.DerivedCombinations.Add(c)
                .Add("Epazote h. f.")
                .Add("Boldo h.")
            End With
        End With
    End Sub
    Private Function LoadDoc(FileName As String) As Boolean
        lblDocument.Text = FileName : Refresh()
        If Not MyDocument.Load(FileName, , m_LoadStatus) Then
            lblLoading.Text = m_FailedToLoad
            lblLoading.ForeColor = Color.Red
            lblLoading.Left = (Width - lblLoading.Width) / 2
            m_StillLoading = False
            butClose.Visible = True
            Return False
        Else
            Close()
            Return True
        End If
    End Function

    Private Sub m_LoadStatus_LoadStart() Handles m_LoadStatus.LoadStart
        If m_LoadStatus.TotalItemCount > 0 Then
            pgMain.Maximum = m_LoadStatus.TotalItemCount
        End If
    End Sub
    Private Sub m_LoadStatus_LoadItem() Handles m_LoadStatus.LoadItem
        Dim c As Long, i As Long, j As Long

        c = Math.Floor(pgMain.Maximum / 20)
        i = Math.Floor(m_LoadStatus.CurrentItemCount / c)
        j = Math.Floor(pgMain.Value / c)
        If i > j Then
            pgMain.Value = m_LoadStatus.CurrentItemCount
        End If
    End Sub

    Private Sub butClose_Click(sender As System.Object, e As EventArgs) Handles butClose.Click
        If Not m_StillLoading Then Close()
    End Sub

#Region "MUI"
    Friend Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
        m_LoadingDocument = Source("Forms", "LoadFileProcess", "Messages", "LoadingDocument")
        m_FailedToLoad = Source("Forms", "LoadFileProcess", "Messages", "FailedToLoad")
    End Sub
#End Region
End Class