Namespace Core
    Class NavigationHistory(Of ElementType As {Element, New}, ReferenceType As {TypedReference(Of ElementType), New}, ReferenceListType As {ReferenceList(Of ElementType, ReferenceType), New})

#Region "Events"
        Public Event ActivateItem(ByVal Item As ElementType)
        Public Event FindElement(ByVal Name As String, ByRef Item As ElementType)
        Public Event FindAllElements(ByVal Name As String, ByRef List As ReferenceListType)
        Public Event SearchElements(ByVal Name As String, ByRef List As ReferenceListType)

#Region "URLBox"
        Private Sub m_URLBox_GotFocus(sender As Object, e As System.EventArgs) Handles m_URLBox.GotFocus, m_URLBox.Enter
            m_URLBox.SelectAll()
        End Sub
        Private Sub URLBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_URLBox.LostFocus
            m_ToolTip.Hide(m_URLTextBox)
        End Sub
        Private Sub URLBox_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_URLBox.TextChanged
            Dim s As String, sText As String = "", sTitle As String = ""

            If m_Navigating Then Exit Sub

            m_MatchesOffset = 0
            m_MatchesList = Nothing

            s = m_URLTextBox.Text
            If s <> "" Then
                If Mid(s, 1, 1) = "=" Then
                    If Len(s) > 1 Then
                        RaiseEvent FindAllElements(Mid(s, 2), m_MatchesList)
                    End If
                Else
                    RaiseEvent SearchElements(s, m_MatchesList)
                End If
            End If

            CreateToolTip()
        End Sub
        Private Sub URLTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles m_URLTextBox.KeyDown
            Dim i As Integer

            Select Case e.KeyCode
                Case Keys.Enter
                    GoToButton_Click(Nothing, Nothing)
                    m_ToolTip.Hide(m_URLTextBox)
                    If m_MatchesList.Count > 0 Then
                        RaiseEvent ActivateItem(m_MatchesList.Item(m_MatchesOffset).Item)
                    End If
                    e.SuppressKeyPress = True
                Case Keys.Down
                    If e.Control Then
                        m_MatchesOffset = m_MatchesOffset + 1000000
                    Else
                        m_MatchesOffset = m_MatchesOffset + 1
                    End If
                    CreateToolTip()
                    e.SuppressKeyPress = True
                Case Keys.Up
                    If e.Control Then
                        m_MatchesOffset = 0
                    Else
                        m_MatchesOffset = m_MatchesOffset - 1
                    End If
                    CreateToolTip()
                    e.SuppressKeyPress = True
                Case Keys.PageDown
                    If e.Control Then
                        m_MatchesOffset = m_MatchesOffset + 25
                    Else
                        m_MatchesOffset = m_MatchesOffset + 10
                    End If
                    CreateToolTip()
                    e.SuppressKeyPress = True
                Case Keys.PageUp
                    If e.Control Then
                        m_MatchesOffset = m_MatchesOffset - 25
                    Else
                        m_MatchesOffset = m_MatchesOffset - 10
                    End If
                    CreateToolTip()
                    e.SuppressKeyPress = True
                Case Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0
                    If Not (e.Shift Or e.Alt Or (Not e.Control)) Then
                        i = e.KeyCode
                        If i = Keys.D0 Then
                            i = Keys.D9 + 1
                        End If
                        e.SuppressKeyPress = SelectMatch(i - Keys.D1 + 1)
                    End If
            End Select
        End Sub
#End Region
#Region "Buttons"
        Private Sub GoToButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_GoToButton.Click
            Dim i As Integer = -1, el As ElementType

            el = Nothing

            RaiseEvent FindElement(m_URLTextBox.Text, el)

            If el Is Nothing Then
                Return
            End If

            Navigate(el)
            RaiseEvent ActivateItem(el)
        End Sub
        Private Sub Navigate_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_Back.ButtonClick, m_Forward.ButtonClick
            Dim i As Integer, tssb As ToolStripSplitButton

            If Not (TypeOf sender Is ToolStripSplitButton) Then
                Return
            End If

            tssb = sender

            If tssb Is Nothing Then
                Return
            End If

            If Not IsNumeric(tssb.Tag) Then
                Return
            End If

            i = tssb.Tag

            NavigateDirection = i
            Navigate()
            RaiseEvent ActivateItem(m_History(m_Index).Item)
        End Sub
        Private Sub Navigate_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles m_Back.DropDownItemClicked, m_Forward.DropDownItemClicked
            Dim i As Integer

            If Not IsNumeric(e.ClickedItem.Tag) Then
                Return
            End If

            i = e.ClickedItem.Tag

            NavigateDirection = i
            Navigate()
            RaiseEvent ActivateItem(m_History(m_Index).Item)
        End Sub
#End Region
        Private Sub Tools_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_Tools.Resize
            Dim i As Integer
            Dim s As New Size(0, m_URLBox.Size.Height)
            With m_ToolStrip.Items
                For i = 1 To .Count
                    s = Size.Add(s, .Item(i - 1).Size)
                Next
            End With
            s = Size.Subtract(s, m_URLBox.Size)
            s = Size.Subtract(m_Tools.Size, s)
            s = Size.Subtract(s, New Size(15, 0))
            m_URLBox.Overflow = ToolStripItemOverflow.AsNeeded
            If s.Width < 100 Then
                s = m_Tools.Size
            End If
            m_URLBox.Size = s
        End Sub

        Private Sub m_History_ItemRemoved(sender As Object, ee As ElementList.EventArgs) Handles m_History.ItemRemoved
            Dim el As ReferenceType = ee.SourceObject
            Dim i As Integer

            If m_Navigating Then Exit Sub

            With m_Back.DropDownItems
                For i = 1 To .Count
                    If (.Item(i - 1).Tag + m_Index) = el.Index Then
                        .Remove(.Item(i - 1))
                        Exit For
                    End If
                Next
            End With
            With m_Forward.DropDownItems
                For i = 1 To .Count
                    If (.Item(i - 1).Tag + m_Index) = el.Index Then
                        .Remove(.Item(i - 1))
                        Exit For
                    End If
                Next
            End With
        End Sub
#End Region

#Region "Variables"
        Private WithEvents MyMUISources As MUISourceCollection = mdiMain.MyMUISources

        Private WithEvents m_History As New ReferenceListType
        Private m_Index As Integer = 0
        Private m_MaxCount As Integer = 20

        Private WithEvents m_Tools As ToolStripContainer
        Private WithEvents m_ToolStrip As ToolStrip

        Private WithEvents m_Back As ToolStripSplitButton, m_Forward As ToolStripSplitButton ', m_ToolTip As ToolTip
        Private WithEvents m_URLBox As ToolStripTextBox, m_URLTextBox As TextBox, m_GoToButton As ToolStripButton

        Private Shared m_ToolTip As ToolTip

        Private m_MatchesList As ReferenceListType, m_MatchesOffset As Integer = 1

        Private m_Navigating As Boolean = False
        Public NavigateDirection As Integer = 0
        Public NavigatingByHistory As Boolean = False
#End Region

#Region "Initialize"
        Private Sub Initialize()
            m_History.Clear()
            m_History.InsertOrdered = False
            m_Index = 0
            m_Navigating = False
            NavigateDirection = 0
            NavigatingByHistory = False

            m_Back.Enabled = False
            m_Back.DropDownItems.Clear()
            m_Forward.Enabled = False
            m_Forward.DropDownItems.Clear()
            m_Navigating = True
            m_URLBox.Text = ""
            m_Navigating = False
        End Sub
        Public Sub SetControls(ByVal ToolBar As ToolStrip, ByVal BackButton As ToolStripSplitButton, ByVal ForwardButton As ToolStripSplitButton, ByVal URLBox As ToolStripTextBox, ByVal GoToButton As ToolStripButton)
            m_ToolStrip = ToolBar
            m_Back = BackButton
            m_Forward = ForwardButton
            m_URLBox = URLBox
            m_URLTextBox = m_URLBox.TextBox
            m_GoToButton = GoToButton
            m_Tools = m_URLBox.Owner.Parent.Parent
            Localize(MyMUISources.Current)
            CreateSharedControls()
            Initialize()
        End Sub
        Private Sub CreateSharedControls()
            If m_ToolTip Is Nothing Then
                m_ToolTip = New ToolTip
            End If
        End Sub
#End Region

#Region "Navigation"
        Public Sub Navigate(Optional ByVal NewItem As ElementType = Nothing)
            Dim HistoryCount As Integer = m_History.Count
            Dim NewIndex As Integer
            Dim el As ElementType, ref As ReferenceType

            If m_Navigating Then Exit Sub

            If NewItem Is Nothing Then
                If HistoryCount = 0 Then
                    Return
                End If
                If NavigateDirection = 0 Then
                    m_URLBox.Text = ""
                    Return
                End If
                NewIndex = NavigateDirection + m_Index
                If (NewIndex <= 0) Or (NewIndex > HistoryCount) Then
                    Return
                End If

                m_Index = NewIndex
            Else
                'Clear forward
                m_Navigating = True
                While m_Index < m_History.Count
                    m_History.Remove(m_History.Count)
                End While
                m_Navigating = False

                'Check for existing in back
                ref = m_History.Item(NewItem)
                If ref IsNot Nothing Then
                    m_Navigating = True
                    ref.Remove()
                    m_Navigating = False
                End If

                m_History.Add(NewItem)

                'Check for maximum exceeded
                While m_History.Count > m_MaxCount
                    m_History.Remove(1)
                End While

                m_Index = m_History.Count
            End If

            el = m_History(m_Index).Item

            PopulateButton(m_Back, m_Index - 1, 1, -1)
            PopulateButton(m_Forward, m_Index + 1, m_History.Count, 1)

            m_Navigating = True
            m_URLTextBox.Text = el.DisplayText
            m_URLTextBox.Enabled = True
            m_GoToButton.Enabled = True

            NavigateDirection = 0
            m_URLTextBox.SelectAll()
            RaiseEvent ActivateItem(el)
            m_Navigating = False
        End Sub

        Private Sub PopulateButton(ByVal Button As ToolStripSplitButton, ByVal IndexFrom As Integer, ByVal IndexTo As Integer, ByVal IndexStep As Integer)
            Dim i As Integer, el As ElementType, s As String

            With Button
                With .DropDownItems
                    .Clear()
                    For i = IndexFrom To IndexTo Step IndexStep
                        el = m_History(i).Item
                        .Add(i & ". " & el.DisplayText).Tag = CStr(i - m_Index)
                    Next
                    i = .Count
                    If i > 0 Then
                        s = .Item(.Count - 1).Text
                        s = " to '" & Mid(s, InStr(s, ".") + 2) & "'"
                    Else
                        s = ""
                    End If
                End With
                .Enabled = (i > 0)
                .ToolTipText = .Text & s
            End With
        End Sub
#End Region

#Region "Tooltip"
        Private Sub CreateToolTip()
            Dim i As Integer, iMin As Integer, iMax As Integer, el As ElementType, rel As ReferenceType
            Dim sText As String = "", sTitle As String = ""

            If m_MatchesList Is Nothing Then
                sTitle = "Directions:"
                sText = "There are 2 ways to find items:" & vbCrLf & vbCrLf & "1. Enter the text to find in the name, ie 'el' gives basel, cicely etc" & vbCrLf & "2. Enter '=' and the first letters to list the items starting with, ie 'ep' gives epazote" & vbCrLf & vbCrLf & "Note that it will only search for non-empty text"
                m_ToolTip.ToolTipTitle = sTitle
                If m_Navigating Then
                    m_URLBox.ToolTipText = sText
                    m_ToolTip.Hide(m_URLTextBox)
                ElseIf m_URLBox.Focused OrElse m_URLTextBox.Focused Then
                    m_ToolTip.Show(sText, m_URLTextBox, 0, m_URLTextBox.Height)
                End If
            Else
                If (m_MatchesList Is Nothing) OrElse (m_MatchesList.Count = 0) Then
                    sText = "No matches found"
                    sTitle = sText
                Else
                    sTitle = IIf(m_MatchesList.Count = 1, "Closest match:", "Closest matches (" & CStr(m_MatchesList.Count) & "):")

                    If m_MatchesOffset < 1 Then
                        m_MatchesOffset = 1
                    Else
                        If m_MatchesList.Count < 10 Then
                            m_MatchesOffset = 1
                        Else
                            If (m_MatchesOffset + 10 - 1) > m_MatchesList.Count Then
                                m_MatchesOffset = m_MatchesList.Count - 10 + 1
                            End If
                        End If
                    End If
                    iMin = m_MatchesOffset
                    iMax = iMin + 10 - 1
                    If iMax > m_MatchesList.Count Then
                        iMax = m_MatchesList.Count
                    End If

                    If iMax = 1 Then
                        sText = "(Press Ctrl+1 to select it)" & vbCrLf
                    Else
                        sText = "(Press Ctrl+1 to " & CStr(iMax - iMin + 1) & " to select one)" & vbCrLf
                    End If
                    sText = sText & "(Use Up/Down and PgUp/PgDn to navigate in results. For quick use Ctrl+)" & vbCrLf & vbCrLf

                    If iMin > 1 Then
                        sText = sText & "... (" & CStr(iMin - 1) & " more)" & vbCrLf
                    End If
                    For i = iMin To iMax
                        rel = m_MatchesList(i)
                        el = rel.Item()
                        sText = sText & CStr(i - iMin + 1) & ") " & el.DisplayText
                        If (rel.Tag.Extra IsNot Nothing) AndAlso (TypeOf rel.Tag.Extra Is String) Then
                            sText = sText & " (on comments)"
                        End If
                        If i < iMax Then
                            sText = sText & vbCrLf
                        End If
                    Next
                    If iMax < m_MatchesList.Count Then
                        sText = sText & vbCrLf & "... (" & CStr(m_MatchesList.Count - iMax) & " more)"
                    End If
                End If

                m_ToolTip.ToolTipTitle = sTitle
                If m_Navigating Then
                    m_URLBox.ToolTipText = sText
                    m_ToolTip.Hide(m_URLTextBox)
                Else
                    m_ToolTip.Show(sText, m_URLTextBox, 0, m_URLBox.Height)
                End If
            End If
        End Sub
        Private Function SelectMatch(ByVal Offset As Integer) As Boolean
            If (m_MatchesList IsNot Nothing) AndAlso (m_MatchesList.Count >= Offset) Then
                With m_URLTextBox
                    Dim rel As ReferenceType = m_MatchesList(m_MatchesOffset + Offset - 1), el As ElementType = rel.Item
                    .Text = el.DisplayText
                    .SelectionStart = Len(.Text)
                End With
                Return True
            End If
            Return False
        End Function
#End Region

#Region "Localize"
        Private Sub Localize(ByVal Source As MUISource) Handles MyMUISources.Localize
            LocalizeToolItem(m_Back, Source("General", "Navigation", "Back"))
            LocalizeToolItem(m_Forward, Source("General", "Navigation", "Forward"))
            LocalizeToolItem(m_GoToButton, Source("General", "Navigation", "Go"))
        End Sub
        Protected Sub LocalizeToolItem(ByVal ToolItem As ToolStripItem, ByVal Text As String)
            If ToolItem IsNot Nothing Then ToolItem.Text = Text
        End Sub
#End Region
    End Class
End Namespace