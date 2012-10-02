Namespace Core
    MustInherit Class NamedElement
        Inherits Element

#Region "Variables"
        Protected m_Name As String = ""
#End Region

#Region "Properties"
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim nel As NamedElement = obj
            If StrComp(DisplayText, nel.DisplayText) <> 0 Then Return False

            Return MyBase.Equals(obj)
        End Function
#End Region

#Region "Management Methods"
        Protected Friend Overrides Property _Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property
#End Region
    End Class
    Class Definition
        Inherits NamedElement

#Region "Variables"
        Protected m_FullText As String = ""
#End Region

#Region "Properties"
        Public Property FullText() As String
            Get
                Return m_FullText
            End Get
            Set(ByVal value As String)
                If value <> m_FullText Then
                    _FullText = value
                    RE_Changed(EventArgs.ActionEnum.Unspecified, EventArgs.SourceEnum.Caller, Me)
                End If
            End Set
        End Property

        Protected Overrides Function _Equals(ByVal obj As Element) As Boolean
            Dim def As Definition = obj

            If obj Is Nothing Then Return False

            Return (StrComp(m_FullText, def.FullText, CompareMethod.Text) = 0)
        End Function
#End Region

#Region "Management Methods"
        Protected Friend WriteOnly Property _FullText() As String
            Set(ByVal value As String)
                m_FullText = value
            End Set
        End Property

        Friend Overrides WriteOnly Property _Parent() As ElementList
            Set(ByVal value As ElementList)
                Dim ex As Exception = TrySetParent(Of DefinitionList)(value, m_Parent, Me) : If ex IsNot Nothing Then Throw ex : Return
            End Set
        End Property
#End Region
    End Class
    Class Spice
        Inherits NamedElement

#Region "Events"
#Region "Changed"
        Protected Friend Overrides Function RE_Changed(ByVal Action As EventArgs.ActionEnum, ByVal Source As EventArgs.SourceEnum, ByVal SourceObject As Object) As EventArgs
            Dim e As New EventArgs(Action, Source, SourceObject)

            _RE_Changed(e)

            If m_Parent IsNot Nothing Then
                m_Parent.RE_ItemChanged(Me, e)
            End If

            Return e
        End Function
#End Region
        'We monitor all combinations of this spice
#Region "DerivedCombinations"
        Private Sub DerivedCombinations_ItemAdded(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_DerivedCombinations.ItemAdded
            RE_Changed(EventArgs.ActionEnum.DerivedCombinations, EventArgs.SourceEnum.EventArgs, ee)
        End Sub
        Private Sub DerivedCombinations_ItemChanged(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_DerivedCombinations.ItemChanged
            If ee.InnerEvent IsNot Nothing Then
                Select Case ee.InnerEvent.Action
                    Case EventArgs.ActionEnum.Index, EventArgs.ActionEnum.Name
                        RE_Changed(EventArgs.ActionEnum.DerivedCombinations, EventArgs.SourceEnum.ElementListEventArgs, ee)
                End Select
            End If
        End Sub
        Private Sub DerivedCombinations_ItemRemoved(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_DerivedCombinations.ItemRemoved
            RE_Changed(EventArgs.ActionEnum.DerivedCombinations, EventArgs.SourceEnum.EventArgs, ee)
        End Sub
#End Region
#End Region

#Region "Variables"
        Protected WithEvents m_DerivedCombinations As New CombinationList(Me)
#End Region

#Region "Init/Terminate"
        Friend Overrides Sub RemRefs(Optional Forced As Boolean = False)
            If (Not Forced) And m_RRed Then Return
            m_RRed = True

            m_DerivedCombinations.RemRefs() : m_DerivedCombinations = Nothing

            MyBase.RemRefs(True)
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property DerivedCombinations() As CombinationList
            Get
                Return m_DerivedCombinations
            End Get
        End Property
#End Region

#Region "Management Methods"
        Friend Overrides WriteOnly Property _Parent() As ElementList
            Set(ByVal value As ElementList)
                Dim ex As Exception = TrySetParent(Of SpiceList)(value, m_Parent, Me) : If ex IsNot Nothing Then Throw ex : Return
            End Set
        End Property

        Protected Friend Overrides WriteOnly Property _Document As Document
            Set(value As Document)
                MyBase._Document = value

                m_DerivedCombinations._Document = value
            End Set
        End Property
#End Region
    End Class
    Class Food
        Inherits NamedElement

        'A food has also a position in the HTML file, so we store some relevant info
        Class HTMLTag
            'We use events when changing to update the HTML
            Private Sub Item_Changed(ByVal sender As Object, ByVal e As Element.EventArgs) Handles m_Item.Changed
                Select Case e.Action
                    Case EventArgs.ActionEnum.Name, EventArgs.ActionEnum.Item, EventArgs.ActionEnum.Combinations, EventArgs.ActionEnum.Comments, EventArgs.ActionEnum.DerivedFoods
                        Refresh()
                End Select
            End Sub

#Region "Variables"
            Private WithEvents m_Item As Food

            Friend Name As String
            Friend InnerHTMLPre As String, InnerHTMLPost As String
            Friend LinePrefix As String
#End Region

#Region "Initialize/Terminate"
            Public Sub New(ByVal Item As Food)
                m_Item = Item
                Refresh()
            End Sub
#End Region
            'UIDs are used to identify in the HTML file, here is a static class to manage them
#Region "UIDs"
            Class UIDCollection
                Private m_UIDS As New Collection, m_Objects As New Collection

                Public Function GetNewUID(ByVal AscosiateWith As Object) As UInteger
                    Dim retval As UInteger
                    Dim i As Integer

                    With m_UIDS
                        retval = 0
                        If (.Count > 0) AndAlso (.Item(.Count) <> .Count) Then
                            For i = 1 To .Count
                                If .Item(i) > i Then
                                    retval = i
                                    .Add(retval, , i)
                                    m_Objects.Add(AscosiateWith, , i)
                                    Exit For
                                End If
                            Next
                            If retval = 0 Then
                                retval = .Count + 1
                                .Add(retval)
                                m_Objects.Add(AscosiateWith)
                            End If
                        Else
                            retval = .Count + 1
                            .Add(retval)
                            m_Objects.Add(AscosiateWith)
                        End If

                        If retval = 0 Then
                            Debug.Fail("Should have found a new UID")
                        End If
                    End With

                    Return retval
                End Function
                Public Function Item(ByVal UID As UInteger) As Object
                    Dim i As Integer

                    i = FindUID(UID)

                    If i >= 1 Then
                        Return m_Objects(i)
                    Else
                        Return Nothing
                    End If
                End Function
                Public Sub RemoveUID(ByVal UID As UInteger)
                    Dim i As Integer

                    i = FindUID(UID)
                    If i >= 1 Then
                        m_UIDS.Remove(i)
                        m_Objects.Remove(i)
                    End If
                End Sub
                Private Function FindUID(ByVal UID As UInteger) As Integer
                    Dim lb As Integer, ub As Integer, nb As Integer

                    With m_UIDS
                        lb = 1
                        ub = .Count
                        Do
                            If (lb > ub) OrElse (UID < .Item(lb)) OrElse (UID > .Item(ub)) Then
                                Return -1
                            ElseIf UID = .Item(lb) Then
                                Return lb
                            ElseIf UID = .Item(ub) Then
                                Return ub
                            End If
                            If (ub - lb) <= 2 Then
                                If .Item(lb + 1) = UID Then
                                    Return lb + 1
                                Else
                                    Return -1
                                End If
                            End If
                            nb = (ub + lb) / 2
                            If UID = .Item(nb) Then
                                Return nb
                            ElseIf UID < .Item(nb) Then
                                ub = nb
                            Else
                                lb = nb
                            End If
                        Loop
                    End With
                End Function
            End Class

            Shared ReadOnly Property UIDs() As UIDCollection
                Get
                    Static m_UIDs As New UIDCollection

                    Return m_UIDs
                End Get
            End Property
#End Region

#Region "HTML"
            Shared Function GetCommentedName(ByVal CommentList As CommentList, ByVal Name As String) As String
                Dim retval As String = ""
                Dim iC As Integer
                Dim i As Integer, s As String
                Dim com As Comment

                If CommentList.Count > 0 Then
                    com = CommentList(1)
                    If com.Scope = "" Then
                        s = Name
                    Else
                        s = com.Scope
                    End If
                    i = InStr(Name, s, CompareMethod.Text)
                    If i > 1 Then
                        retval = Mid(Name, 1, i - 1)
                    Else
                        s = Name
                    End If
                    retval = retval & "<span class=""SCO"">" & s & "</span>"
                    retval = retval & "<span class=""com"">(" & com.Text & ")</span>"
                    If (i + Len(s)) <= Len(Name) Then
                        retval = retval & Mid(Name, i + Len(s))
                    End If
                    For iC = 2 To CommentList.Count
                        com = CommentList(iC)
                        retval = retval & "<span class=""com"">(" & com.Text & ")</span>"
                    Next
                Else
                    retval = retval & Name
                End If

                Return retval
            End Function

            Private Sub GetHTML(ByRef HTMLPre As String, ByRef HTMLPost As String, AddCombLinks As Boolean)
                Dim j As Integer
                Dim s As String
                Dim parent_name As String, fd_level
                Dim fd As Food

                If m_Item Is Nothing Then
                    HTMLPre = ""
                    HTMLPost = ""
                    Return
                End If

                With m_Item
                    'Get a UID for the name
                    If .UID = 0 Then
                        .UID = UIDs.GetNewUID(m_Item)
                    End If
                    Name = "F" & CStr(.UID)

                    fd_level = .Level
                    If fd_level = 1 Then
                        parent_name = "F"
                    Else
                        fd = .Parent.Parent
                        parent_name = fd.HTML.Name
                    End If
                    LinePrefix = vbCrLf & " "
                    '"   <a href=""javascript:ToggleVisibility2('" & Name & "')"">" & LinePrefix &
                    '"   <span class=""icon-button"">" & LinePrefix &
                    '"      <span class=""icon"" id=""" & Name & "M"">" & LinePrefix &
                    '"       <div class=""icon-minus-circle""></div>" & LinePrefix &
                    '"              <div class=""icon-minus-line""></div>" & LinePrefix &
                    '"       </span>" & LinePrefix &
                    '"       <span class=""icon"" id=""" & Name & "P"" style=""display: none;"">" & LinePrefix &
                    '"           <div class=""icon-plus-circle""></div>" & LinePrefix &
                    '"           <div class=""icon-plus-line-1""></div>" & LinePrefix &
                    '"           <div class=""icon-plus-line-2""></div>" & LinePrefix &
                    '"       </span>" & LinePrefix &
                    '"   </span>" & LinePrefix &
                    '"   </a>" & LinePrefix &
                    HTMLPre = LinePrefix &
                        "<dt id=""_" & Name & """ class=""l" & CStr(fd_level + 1) & """>" & LinePrefix &
                        "   <a href=""javascript:ToggleVisibility2('" & Name & "')"">" &
                        "<img id=""" & Name & "P"" style=""display: none"" src=""data:image/png; base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjEwMPRyoQAAAMdJREFUOE9j+P//PwMSFgCyG/79+/ccSKMAoNh3IJ4PFJRA1oOs2QGo4D26RnT+379/fwDVRcAMgRngQEgjFhcVgAwBGSBBjM3YXAIUM2AAau7HZXtAwP//Hh643QbUux5kAE5/g9wHdiMegFeaLANAzoZpRKexeQfDBSBFFBmA7F2yvECyAaCowBXIIO8QiMbnoDAwICMV/oPqSYAl5QZiDQG6GKwZ6nJwMgFjoEA7CYaAvM0BywvoOfI8LoNA2RyIM2CWgmgAOg6jlL8AcNEAAAAASUVORK5CYII="" />" &
                        "<img id=""" & Name & "M"" src=""data:image/png; base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAABp0RVh0U29mdHdhcmUAUGFpbnQuTkVUIHYzLjUuMTAw9HKhAAAAu0lEQVQ4T2P4//8/AxIWALIb/v379xxIowCg2Hcgng8UlEDWg6zZAajgPbpGdP7fv39/ANVFwAyBGeBASCMWFxWADAEZIEGMzdhcAhQzYABq7ifVdph6oN71IAMI+hufBSAvUAQwDAgIgIQMNuzhgWkXhgEgRRQZQKp/KA8DUFSQaitSND4HucCAVAOAlv6D6kmAJeUGYg2BaYa6HJyUwRgo0E6CISBvc8DyAnqOPI/LIFA2B+IMmKUgGgAgorks+m3UgAAAAABJRU5ErkJggg=="" />" &
                        "</a>" & LinePrefix &
                        LinePrefix &
                        "   <a href=""#" & Name & """ name=""" & Name & """ class=""food"">" & GetCommentedName(.Comments, .Name) & "</a>" & LinePrefix &
                        "   <span style=""margin-left:20; vertical-align: bottom;"">&nbsp;</span>" & LinePrefix &
                        LinePrefix &
                        "   <a class=""toc"" href=""#" & parent_name & """>" & LinePrefix &
                        "       <span class=""icon-button"">" & LinePrefix &
                        "           <span class=""icon"">" & LinePrefix &
                        "               <div class=""icon-up-circle""></div>" & LinePrefix &
                        "               <div class=""icon-up-triangle""></div>" & LinePrefix &
                        "               <div class=""icon-up-line""></div>" & LinePrefix &
                        "           </span>" & LinePrefix &
                        "       </span>" & LinePrefix &
                        "       <span class=""name"">Up</span>" & LinePrefix &
                        "   </a>" & LinePrefix &
                        LinePrefix &
                        "</dt>"

                    HTMLPre = HTMLPre & LinePrefix &
                        "<dd id=""" & (Name & "_") & """ class=""l" & (fd_level + 1) & "d"">" & LinePrefix &
                        "   <div class=""buttons"">"

                    LinePrefix = LinePrefix & "       "
                    'Combinations
                    With .Combinations
                        If .Count > 0 Then
                            For j = 1 To .Count
                                s = IIf(j > 1, ", ", "")
                                With .Item(j)
                                    If s <> "" Then
                                        Select Case .ConnectWith
                                            Case CombinationReference.ConnectWithEnum.And
                                                s = mdiMain.MyMUISources.Current("General", "ItemsCommands", "ConnectWithAnd")
                                            Case CombinationReference.ConnectWithEnum.Or
                                                s = mdiMain.MyMUISources.Current("General", "ItemsCommands", "ConnectWithOr")
                                            Case Else
                                                s = "" 'mdiMain.MyMUISources.Current("General", "ItemsCommands", "ConnectWithComma")
                                        End Select
                                    End If
                                    'Set a UID for that item
                                    If .Item.UID = 0 Then
                                        .Item.UID = UIDs.GetNewUID(.Item)
                                    End If
                                    HTMLPre = HTMLPre & LinePrefix &
                                        s & "<a " & IIf(AddCombLinks, "href=""#I" & .Item.UID & """", "") & " class=""button"">" & GetCommentedName(.Comments, .DisplayText) & "</a>"
                                End With
                            Next
                        End If
                    End With
                    LinePrefix = Mid(LinePrefix, 1, Len(LinePrefix) - 2)

                    'Foods
                    If .Foods.Count > 0 Then
                        HTMLPre = HTMLPre & LinePrefix & "    <dl>"
                        HTMLPost = LinePrefix & LinePrefix & "  </dl>"
                    Else
                        HTMLPost = LinePrefix
                    End If

                    HTMLPost = HTMLPost &
                        "   </div> " & LinePrefix &
                        "</dd>"
                End With
            End Sub

            Friend Sub Refresh()
                GetHTML(InnerHTMLPre, InnerHTMLPost, True)
            End Sub

            Public ReadOnly Property ExportHTML() As String
                Get
                    Dim pre As String = "", post As String = ""

                    GetHTML(pre, post, False)

                    Return pre & m_Item.Foods.ExportHTML & post
                    'Dim i As Integer, j As Integer
                    'Dim s As String
                    'Dim fdl As FoodList, fd As Food
                    'Dim retval_in As String = ""
                    'Dim retval_pre As String, retval_post As String

                    'If m_Item Is Nothing Then
                    '    retval_pre = ""
                    '    retval_post = ""
                    '    Return ""
                    'End If

                    'With m_Item
                    '    'Get a UID for the name
                    '    If .UID = 0 Then
                    '        .UID = UIDs.GetNewUID(m_Item)
                    '    End If
                    '    Name = "F" & CStr(.UID)

                    '    '"Go Up" and "Sync" buttons
                    '    i = .Level
                    '    If i = 1 Then
                    '        LinePrefix = vbCrLf & "		"
                    '        retval_pre = LinePrefix & "<dt class=""L2"" id=""_" & Name & """>"
                    '        s = "<a style=""margin-left:70""> </a><a class=""TOC"" href=""#F"">Go Up</a>"
                    '    Else
                    '        fdl = .Parent
                    '        fd = fdl.Parent
                    '        LinePrefix = fd.HTML.LinePrefix & "	"
                    '        retval_pre = LinePrefix & "<dt class=""L" & CStr(i + 1) & """ id=""_" & Name & """>"
                    '        s = "<a style=""margin-left:70""> </a><a class=""TOC"" href=""#" & fd.HTML.Name & """>Go Up</a>"
                    '    End If

                    '    'Tree button
                    '    If (m_Item.Foods.Count > 0) Or (m_Item.Combinations.Count > 0) Then
                    '        retval_pre = retval_pre & _
                    '        "<button onclick=""checkExpand()"" id=""" & Name & """>-</button>"
                    '    End If

                    '    'Food name
                    '    retval_pre = retval_pre & "<a name=""#" & Name & """>" & GetCommentedName(.Comments, .Name) & s & "</a></dt>" & _
                    '      LinePrefix & "<dd id=""" & (Name & "_") & """>"

                    '    'Combinations
                    '    With .Combinations
                    '        If .Count > 0 Then
                    '            For j = 1 To .Count
                    '                s = IIf(j > 1, ", ", "")
                    '                With .Item(j)
                    '                    If s <> "" Then
                    '                        Select Case .ConnectWith
                    '                            Case CombinationReference.ConnectWithEnum.And
                    '                                s = mdiMain.MyMUISources.Current("General", "ItemsCommands", "ConnectWithAnd")
                    '                            Case CombinationReference.ConnectWithEnum.Or
                    '                                s = mdiMain.MyMUISources.Current("General", "ItemsCommands", "ConnectWithOr")
                    '                            Case Else
                    '                                s = mdiMain.MyMUISources.Current("General", "ItemsCommands", "ConnectWithComma")
                    '                        End Select
                    '                    End If
                    '                    'Set a UID for that item
                    '                    If .Item.UID = 0 Then
                    '                        .Item.UID = UIDs.GetNewUID(.Item)
                    '                    End If
                    '                    retval_pre = retval_pre & s & GetCommentedName(.Comments, .DisplayText)
                    '                End With
                    '            Next
                    '            retval_pre = retval_pre & LinePrefix & "<br><br>"
                    '        End If
                    '    End With
                    '    'Foods
                    '    If .Foods.Count > 0 Then
                    '        retval_pre = retval_pre & "<dl>"
                    '        retval_in = .Foods.ExportHTML
                    '        retval_post = LinePrefix & "</dl>" & LinePrefix & "</dd> "
                    '    Else
                    '        retval_in = ""
                    '        retval_post = LinePrefix & "</dd> "
                    '    End If

                    '    Return retval_pre & retval_in & retval_post
                    'End With
                End Get
            End Property
#End Region
        End Class

#Region "Events"
        'Clear the structure
        Protected Friend Overrides Sub RE_Removed()
            If m_RRed Then Return

            m_Combinations.Clear()
            m_Foods.Clear()

            MyBase.RE_Removed()
        End Sub
        Protected Friend Overrides Function RE_Changed(Action As Element.EventArgs.ActionEnum, Source As Element.EventArgs.SourceEnum, SourceObject As Object) As Element.EventArgs
            If Action = EventArgs.ActionEnum.Name Then
                Dim i As Integer

                With m_Foods
                    For i = 1 To .Count
                        .Item(i).RE_Changed(EventArgs.ActionEnum.Name, EventArgs.SourceEnum.Unspecified, Nothing)
                    Next
                End With
            End If
            Return MyBase.RE_Changed(Action, Source, SourceObject)
        End Function
        Protected Sub Combinations_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_Combinations.ItemAdded, m_Combinations.ItemChanged, m_Combinations.ItemChanged, m_Combinations.ItemRemoved, m_Combinations.BatchEnded
            Select Case ee.Action
                Case ElementList.EventArgs.ActionEnum.ItemAdded
                    Dim cmbr As CombinationReference = ee.SourceObject

                    'Add Me at the other item
                    cmbr.Item.Foods.Add(Me)
                Case ElementList.EventArgs.ActionEnum.ItemRemoved
                    Dim cmbr As CombinationReference = ee.SourceObject

                    'Remove Me from the other item
                    Dim fdrl As FoodReferenceList = cmbr.Item.Foods, fdr As FoodReference

                    If fdrl IsNot Nothing Then
                        fdr = fdrl.Item(Me)
                        If fdr IsNot Nothing Then
                            fdr.Remove()
                        End If
                    End If
                Case ElementList.EventArgs.ActionEnum.ItemChanged
                    If Not m_Combinations.Batching Then
                        Dim e As EventArgs

                        'Announce they changed
                        e = RE_Changed(EventArgs.ActionEnum.Combinations, EventArgs.SourceEnum.ElementListEventArgs, ee)
                        Root.RE_ItemChanged(Me, e)
                    End If
                Case ElementList.EventArgs.ActionEnum.BatchEnded
                    Dim e As EventArgs

                    'Announce they changed
                    e = RE_Changed(EventArgs.ActionEnum.Combinations, EventArgs.SourceEnum.ElementListEventArgs, ee)
                    Root.RE_ItemChanged(Me, e)
            End Select
        End Sub
#End Region

#Region "Variables"
        Protected WithEvents m_Foods As New FoodList(Me)

        Protected WithEvents m_Combinations As New CombinationReferenceList(Me)

        Friend UID As UInt32, HTML As New HTMLTag(Me)
#End Region

#Region "Init/Terminate"
        Protected Overrides Sub Finalize()
            HTMLTag.UIDs.RemoveUID(UID)
            MyBase.Finalize()
        End Sub
        Friend Overrides Sub RemRefs(Optional Forced As Boolean = False)
            If (Not Forced) And m_RRed Then Exit Sub

            MyBase.RemRefs(True)

            m_Foods.RemRefs() : m_Foods = Nothing
            m_Combinations.RemRefs() : m_Combinations = Nothing

            m_RRed = True
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property Foods() As FoodList
            Get
                Return m_Foods
            End Get
        End Property
        Public ReadOnly Property Combinations() As CombinationReferenceList
            Get
                Return m_Combinations
            End Get
        End Property

        Public Overrides ReadOnly Property DisplayText() As String
            Get
                Return Path
            End Get
        End Property

        Public ReadOnly Property Path() As String
            Get
                Dim s As String = m_Name
                Dim fd As Food, fdl As FoodList

                fd = Me
                Do
                    fdl = fd.m_Parent : If fdl Is Nothing Then Exit Do
                    If Not (TypeOf fdl.Parent Is Food) Then Exit Do
                    fd = fdl.Parent : If fd Is Nothing Then Exit Do
                    s = fd.m_Name & "\" & s
                Loop

                Return s
            End Get
        End Property
        Public ReadOnly Property Root() As FoodList
            Get
                Dim fd As Food, fdl As FoodList = m_Parent

                fd = Me
                Do
                    If fd.m_Parent Is Nothing Then Exit Do
                    fdl = fd.m_Parent
                    If Not (TypeOf fdl.Parent Is Food) Then Exit Do
                    fd = fdl.Parent : If fd Is Nothing Then Exit Do
                Loop

                Return fdl
            End Get
        End Property
        Public ReadOnly Property Level() As Integer
            Get
                Dim i As Integer = 1
                Dim fd As Food, fdl As FoodList

                fd = Me
                Do
                    fdl = fd.m_Parent : If fdl Is Nothing Then Exit Do
                    If Not (TypeOf fdl.Parent Is Food) Then Exit Do
                    fd = fdl.Parent : If fd Is Nothing Then Exit Do
                    i += 1
                Loop

                Return i
            End Get
        End Property

        Public Overrides Property Parent() As ElementList
            Get
                Return m_Parent
            End Get
            Set(ByVal value As ElementList)
                'Dim s As String = "From '"

                'If (m_Parent Is Nothing) OrElse (Parent.Parent Is Nothing) Then
                '	s = s & "\"
                'Else
                '	s = s & Parent.Parent.DisplayText
                'End If

                's = s & "' to '"

                'If (value Is Nothing) OrElse (value.Parent Is Nothing) Then
                '	s = s & "\"
                'Else
                '	s = s & value.Parent.DisplayText
                'End If

                'MsgBox(s & "'." & vbCrLf & "Is it correct?" & vbCrLf & "Yet to be implemented! Wait!")

                Dim fdl As FoodList = value
                If (Not ReferenceEquals(fdl, m_Parent)) AndAlso (Not fdl.HasForParent(Me)) Then
                    fdl.MoveItemFrom(Me)
                End If
            End Set
        End Property
        Public ReadOnly Property HasForParent(ByVal Item As Food) As Boolean
            Get
                Dim el As Food = Me, ell As FoodList

                While el IsNot Nothing
                    If ReferenceEquals(el, Item) Then Return True
                    ell = el.m_Parent
                    If ell Is Nothing Then Return False
                    el = ell.Parent
                End While

                Return False
            End Get
        End Property
#End Region
        'Get the HTML code
#Region "HTML"
        Public ReadOnly Property InnerHTML() As String
            Get
                Dim s As String = ""

                s = HTML.InnerHTMLPre & m_Foods.InnerHTML & HTML.InnerHTMLPost

                Return s
            End Get
        End Property
#End Region

#Region "Management Methods"
        Friend Overrides WriteOnly Property _Parent() As ElementList
            Set(ByVal value As ElementList)
                Dim ex As Exception = TrySetParent(Of FoodList)(value, m_Parent, Me) : If ex IsNot Nothing Then Throw ex : Return
            End Set
        End Property

        Public Overrides Function CanHaveAsChild(ByVal Item As Element) As Boolean
            If TypeOf Item Is Combination Then
                Return True
            ElseIf TypeOf Item Is CombinationReference Then
                Return True
            ElseIf TypeOf Item Is Food Then
                'Can have as child only if 
                '   we are its parent
                '   it is not an ancestor
                Return (Item.Parent IsNot Me) AndAlso (Not HasForParent(Item))
            End If

            Return False
        End Function

        Protected Friend Overrides WriteOnly Property _Document As Document
            Set(value As Document)
                MyBase._Document = value

                m_Combinations._Document = value
                m_Foods._Document = value
            End Set
        End Property
#End Region

    End Class
End Namespace