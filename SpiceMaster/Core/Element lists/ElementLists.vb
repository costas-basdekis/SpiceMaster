Namespace Core
    MustInherit Class ElementList
        Public Overrides Function ToString() As String
            If m_Parent Is Nothing Then
                Return IIf(Document Is Nothing, "!", "") & "[" & Me.GetType.Name & "," & GetHashCode() & "]"
            Else
                Return m_Parent.ToString & ".[" & Me.GetType.Name & "," & GetHashCode() & "]"
            End If
        End Function

#Region "Events"
        'Events info while propagating an event
        Class EventArgs

#Region "Enums"
            Enum ActionEnum
                Unspecified = 0
                ItemAdded
                ItemChanged
                ItemRemoved
                BatchEnded
            End Enum

            Enum SourceEnum
                Unspecified = 0
                Caller
                ListItem
            End Enum
#End Region

#Region "Variables"
            Protected m_Action As ActionEnum = ActionEnum.Unspecified
            Protected m_Source As SourceEnum = SourceEnum.Caller, m_SourceObject As Object = Nothing
            Protected m_InnerEvent As Element.EventArgs = Nothing
#End Region

#Region "Init/Terminate"
            Public Sub New(Optional ByVal NewAction As ActionEnum = ActionEnum.Unspecified, Optional ByVal NewSource As SourceEnum = SourceEnum.Unspecified, Optional ByVal NewSourceObjec As Object = Nothing)
                m_Action = NewAction
                m_Source = NewSource
                m_SourceObject = NewSourceObjec
            End Sub
#End Region

#Region "Properties"
            Public ReadOnly Property Action() As ActionEnum
                Get
                    Return m_Action
                End Get
            End Property
            Public ReadOnly Property Source() As SourceEnum
                Get
                    Return m_Source
                End Get
            End Property
            Public ReadOnly Property SourceObject() As Object
                Get
                    Return m_SourceObject
                End Get
            End Property
            Public ReadOnly Property InnerEvent() As Element.EventArgs
                Get
                    Return m_InnerEvent
                End Get
            End Property
#End Region

#Region "Management Methods"
            Friend WriteOnly Property _Action() As ActionEnum
                Set(ByVal value As ActionEnum)
                    m_Action = value
                End Set
            End Property
            Friend WriteOnly Property _InnerEvent() As Element.EventArgs
                Set(ByVal value As Element.EventArgs)
                    m_InnerEvent = value
                End Set
            End Property
#End Region

            Public Overrides Function ToString() As String
                ToString = "(" & GetHashCode() & "): " & Action.ToString & " , " & Source.ToString & " , "
                If SourceObject Is Nothing Then
                    ToString = ToString & "<Nothing>"
                Else
                    If TypeOf SourceObject Is ElementList Then
                        ToString &= SourceObject.ToString
                    ElseIf TypeOf SourceObject Is Element Then
                        ToString &= SourceObject.ToString
                    ElseIf TypeOf SourceObject Is EventArgs Then
                        ToString &= SourceObject.ToString
                    ElseIf TypeOf SourceObject Is ElementList.EventArgs Then
                        ToString &= SourceObject.ToString
                    Else
                        ToString = ToString & SourceObject.GetType.FullName
                    End If
                End If
            End Function
        End Class
        'Internal routines to raise an event are prefixed RE_ - _RE_ prefixed routines actually raise the event
#Region "ItemAdded"
        Public Event ItemAdded(ByVal sender As Object, ByVal ee As ElementList.EventArgs)
        Protected Friend Overridable Sub RE_ItemAdded(ByVal Item As Element)
            Dim ee As New ElementList.EventArgs(ElementList.EventArgs.ActionEnum.ItemAdded, ElementList.EventArgs.SourceEnum.ListItem, Item)

            Dim doc As Document = Document
            If doc IsNot Nothing Then doc.BatchChange()

            m_BAdded = m_BAdded + 1

            ee._Action = ElementList.EventArgs.ActionEnum.ItemAdded
            _RE_ItemAdded(ee)
        End Sub
        Protected Sub _RE_ItemAdded(ByVal ee As ElementList.EventArgs)
            frmDocumentChanges.ElementChange(">" & ee.ToString, m_Parent)
            RaiseEvent ItemAdded(Me, ee)
        End Sub
#End Region
#Region "ItemChanged"
        Public Event ItemChanged(ByVal sender As Object, ByVal ee As ElementList.EventArgs)
        Protected Friend Overridable Sub RE_ItemChanged(ByVal Item As Element, ByVal InnerEvent As Element.EventArgs)
            Dim ee As New ElementList.EventArgs(ElementList.EventArgs.ActionEnum.ItemChanged, ElementList.EventArgs.SourceEnum.ListItem, Item)
            ee._InnerEvent = InnerEvent

            'Dont propagate Item's Combinations change any further
            If (ee.InnerEvent IsNot Nothing) AndAlso (ee.InnerEvent.Action = Element.EventArgs.ActionEnum.Combinations) Then
                'RaiseEvent ListItemChanged(Me, ee)
                Return
            End If

            Dim doc As Document = Document
            If doc IsNot Nothing Then doc.BatchChange()

            m_BChanged = m_BChanged + 1

            ee._Action = ElementList.EventArgs.ActionEnum.ItemChanged
            _RE_ItemChanged(ee)
        End Sub
        Protected Sub _RE_ItemChanged(ByVal ee As ElementList.EventArgs)
            frmDocumentChanges.ElementChange(">" & ee.ToString, m_Parent)
            RaiseEvent ItemChanged(Me, ee)
        End Sub
#End Region
#Region "ItemRemoved"
        Public Event ItemRemoved(ByVal sender As Object, ByVal ee As ElementList.EventArgs)
        Protected Friend Overridable Sub RE_ItemRemoved(ByVal Item As Element)
            Dim ee As New ElementList.EventArgs(ElementList.EventArgs.ActionEnum.ItemRemoved, ElementList.EventArgs.SourceEnum.ListItem, Item)

            Dim doc As Document = Document
            If doc IsNot Nothing Then doc.BatchChange()

            m_BRemoved = m_BRemoved + 1

            ee._Action = ElementList.EventArgs.ActionEnum.ItemRemoved
            _RE_ItemRemoved(ee)
        End Sub
        Protected Sub _RE_ItemRemoved(ByVal ee As ElementList.EventArgs)
            frmDocumentChanges.ElementChange(">" & ee.ToString, m_Parent)
            RaiseEvent ItemRemoved(Me, ee)
        End Sub
#End Region
#Region "BatchEnded"
        Public Event BatchEnded(ByVal sender As Object, ByVal ee As ElementList.EventArgs)
        Protected Sub _RE_BatchEnded()
            Dim ee As ElementList.EventArgs = New ElementList.EventArgs(ElementList.EventArgs.ActionEnum.BatchEnded, ElementList.EventArgs.SourceEnum.Caller, Me)

            frmDocumentChanges.ElementChange(">" & ee.ToString, m_Parent)
            RaiseEvent BatchEnded(Me, ee)
        End Sub
#End Region
#End Region

#Region "Variables"
        Protected m_RRed As Boolean = False

        Protected m_Document As Document = Nothing

        Protected m_Elements As New Collection
        Protected m_Parent As Element = Nothing

        Public Tag As Object = Nothing
        'Batching - adding many elements without the need to propagate the events (eg loading a document)
        Protected m_BatchCount As Integer = 0
        Protected m_BRemoved As Integer = 0, m_BAdded As Integer = 0, m_BChanged As Integer = 0
#End Region

#Region "Init/Terminate"
        'Clear the list and items
        Friend Sub RemRefs(Optional ByVal ClearItem As Boolean = True, Optional Forced As Boolean = False)
            Dim i As Integer
            Dim el As Element

            If (Not Forced) And m_RRed Then Return
            If ClearItem Then m_RRed = True

            With m_Elements
                For i = 1 To .Count
                    el = .Item(i)
                    el.RemRefs()
                Next
                .Clear()
            End With
            el = Nothing

            If ClearItem Then
                m_Document = Nothing
                m_Parent = Nothing
                m_Elements = Nothing
            End If

            MyBase.Finalize()
        End Sub
#End Region
        'Stock properties accessors
#Region "Properties"
        Public ReadOnly Property Count() As Integer
            Get
                Return m_Elements.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As Element
            Get
                If (Index < 1) Or (Index > m_Elements.Count) Then
                    Return Nothing
                End If

                Return m_Elements(Index)
            End Get
        End Property
        Default Public Overridable ReadOnly Property Item(ByVal Index As Element, Optional ByVal MustRefEquals As Boolean = False) As Element
            Get
                Dim i As Integer, retval As Element = Nothing

                If Index Is Nothing Then
                    Return Nothing
                End If

                With m_Elements
                    For i = 1 To .Count
                        retval = .Item(i)
                        If (MustRefEquals AndAlso ReferenceEquals(retval, Index)) OrElse retval.Equals(Index) Then
                            Return retval
                        End If
                    Next
                End With

                Return Nothing
            End Get
        End Property
        Default Public ReadOnly Property Item(ByVal Index As String) As Element
            Get
                Dim pos As Long, bExists As Boolean = False

                pos = _ItemPosition(Index, bExists)

                If bExists Then
                    Return m_Elements(pos)
                Else
                    Return Nothing
                End If

                'Dim i As Integer, el As NamedElement

                'For i = 1 To m_Elements.Count
                '    el = m_Elements(i)
                '    If StrComp(Index, el.Name, CompareMethod.Text) = 0 Then
                '        Return el
                '    End If
                'Next

                'Return Nothing
            End Get
        End Property
        Friend ReadOnly Property ByFileUID(ByVal UID As Integer) As Element
            Get
                Dim i As Integer
                Dim el As Element = Nothing

                With m_Elements
                    'Check if FUID=Index for quicker access
                    If (UID > 0) And (UID <= .Count) Then
                        el = .Item(UID)
                        If el.FileUID = UID Then Return el
                    End If
                    'Find it
                    For i = 1 To .Count
                        el = .Item(i)
                        If el.FileUID = UID Then Return el
                    Next
                End With

                Return Nothing
            End Get
        End Property

        'Find position with bisection - returns where the item's position should be
        Protected Function _ItemPosition(Index As String, Optional ByRef Exists As Boolean = False) As Long
            Dim i_min As Long, i_max As Long, i As Long

            If Index = "" Then Return 0

            'Initialize on the whole range
            i_min = 1 : i_max = m_Elements.Count

            'Empty list
            If m_Elements.Count = 0 Then Return 1
            'Check first
            Select Case StrComp(Index, CType(m_Elements(i_min), Element).Name, CompareMethod.Text)
                Case -1
                    Exists = False
                    Return i_min
                Case 0
                    Exists = True
                    Return i_min
            End Select
            'Check last
            Select Case StrComp(Index, CType(m_Elements(i_max), Element).Name, CompareMethod.Text)
                Case 1
                    Exists = False
                    Return i_max + 1
                Case 0
                    Exists = True
                    Return i_max
            End Select

            'Bisection
            Do
                i = Math.Floor((i_min + i_max) / 2)
                If i = i_min Then
                    Exists = False
                    Return i_min + 1
                End If
                Select Case StrComp(Index, CType(m_Elements(i), Element).Name, CompareMethod.Text)
                    Case -1
                        i_max = i
                    Case 0
                        Exists = True
                        Return i
                    Case 1
                        i_min = i
                End Select
            Loop
        End Function

        Public ReadOnly Property Document() As Document
            Get
                Return m_Document
            End Get
        End Property

        Public Overridable ReadOnly Property Parent() As Element
            Get
                Return m_Parent
            End Get
        End Property
        Public ReadOnly Property HasForParent(ByVal Item As ElementList) As Boolean
            Get

            End Get
        End Property
        Public ReadOnly Property HasForParent(ByVal Item As Element) As Boolean
            Get
                Dim el As Element = Nothing, ell As ElementList = Me

                While ell IsNot Nothing
                    el = ell.Parent
                    If el Is Nothing Then Return False
                    If ReferenceEquals(el, Item) Then Return True
                    ell = el.Parent
                End While

                Return False
            End Get
        End Property
#End Region

        Public Function Rename(ByVal Item As Element, ByVal NewName As String) As Boolean
            Dim el As Element

            If Not ReferenceEquals(Me.Item(Item, True), Item) Then
                Dim ex As New Exception("Not a member of this list")
                ex.Source = Me.GetType.Name

                Throw ex
                Return False
            End If

            If NewName = "" Then
                Return False
            End If

            If StrComp(Item.Name, NewName, CompareMethod.Text) = 0 Then
                Return False
            End If

            el = Me.Item(NewName)

            If Not (el Is Nothing) Then
                Return False
            End If

            With Item
                ._Name = NewName
                m_Elements.Remove(.Index)
            End With

            _Add(Item)

            RE_ItemChanged(Item, Nothing)
            Item.RE_Changed(Element.EventArgs.ActionEnum.Name, Element.EventArgs.SourceEnum.Caller, Item)

            Return True
        End Function
        'Add an item in the right alphabetical order - and don't raise any events
        Protected Function _Add(NewItem As Element) As Element
            Dim bExists As Boolean = False, item_id As Long

            If NewItem Is Nothing Then Return Nothing

            'Find new position
            item_id = _ItemPosition(NewItem.Name, bExists)
            If item_id = 0 Then Return Nothing
            If bExists Then Return m_Elements(item_id)

            'Add it
            If item_id > m_Elements.Count Then
                m_Elements.Add(NewItem)
            Else
                m_Elements.Add(NewItem, , item_id)
            End If

            _Reindex(item_id)
            NewItem._Document = m_Document

            Return NewItem
        End Function

#Region "Remove"
        Public Sub Remove(ByVal Index As Element)
            Dim tEl As Element

            If Index Is Nothing Then
                Return
            End If

            tEl = Item(Index)

            If tEl Is Nothing Then Exit Sub

            m_Elements.Remove(tEl.Index)
            _Reindex()

            RE_ItemRemoved(tEl)

            'Clear element
            tEl.RemRefs()
        End Sub
        Public Sub Remove(ByVal Index As Integer)
            Dim i As Integer, el As Element
            Dim tEl As Element

            If (Index < 1) Or (Index > m_Elements.Count) Then
                Dim ex As New Exception("Invalid index: " & Index)
                ex.Source = Me.GetType.Name

                Throw ex
                Return
            End If

            tEl = m_Elements(Index)

            m_Elements.Remove(Index)
            With m_Elements
                For i = 1 To .Count
                    el = .Item(i)
                    el._Index = i
                Next
            End With

            RE_ItemRemoved(tEl)

            'Clear element
            tEl.RemRefs()
        End Sub
        Public Sub Clear()
            Dim el As Element

            BatchStart()
            With m_Elements
                While .Count > 0
                    el = .Item(1)
                    If el IsNot Nothing Then el.Remove()
                End While
            End With
            BatchEnd()
        End Sub
#End Region
        'Batching - changing  the structure of the document without the need of event propagation (eg loading a document or clearing it
#Region "Batch"
        Public Sub BatchStart()
            m_BatchCount = m_BatchCount + 1
            If m_Document IsNot Nothing Then m_Document.BatchStart()
        End Sub
        Public Sub BatchEnd()
            m_BatchCount = m_BatchCount - 1
            If m_BatchCount = 0 Then
                If (m_BAdded + m_BRemoved + m_BChanged) > 0 Then
                    _RE_BatchEnded()
                End If
                m_BAdded = 0
                m_BRemoved = 0
                m_BChanged = 0
            End If
            If m_Document IsNot Nothing Then m_Document.BatchEnd()

            If m_BatchCount < 0 Then
                Dim ex As New Exception("Batch operation has not started")
                ex.Source = Me.GetType.Name

                Throw ex
                Return
            End If
        End Sub

        Public ReadOnly Property Batching As Boolean
            Get
                Return (m_BatchCount > 0)
            End Get
        End Property
        Public ReadOnly Property BatchAddedCount() As Integer
            Get
                Return m_BAdded
            End Get
        End Property
        Public ReadOnly Property BatchChangedCount() As Integer
            Get
                Return m_BChanged
            End Get
        End Property
        Public ReadOnly Property BatchRemovedCount() As Integer
            Get
                Return m_BRemoved
            End Get
        End Property
#End Region

#Region "Management Functions"
        Friend Sub _Reindex(Optional StartingFrom As Long = 1)
            Dim i As Long

            For i = StartingFrom To m_Elements.Count
                With CType(m_Elements(i), Element)
                    ._Index = i
                    ._Parent = Me
                End With
            Next
        End Sub
        Friend Sub _Clear()
            m_Elements.Clear()
        End Sub

        Friend Sub MoveFrom(ByVal OriginalStructure As ElementList)
            With OriginalStructure
                m_Elements = .m_Elements
                .m_Elements = New Collection
            End With
            _Reindex()
            _Document = m_Document
        End Sub

        Friend Overridable WriteOnly Property _Parent() As Element
            Set(ByVal value As Element)
                m_Parent = value
            End Set
        End Property

        Protected Friend Overridable WriteOnly Property _Document As Document
            Set(value As Document)
                m_Document = value

                For i As Integer = 1 To m_Elements.Count
                    CType(m_Elements(i), Element)._Document = value
                Next
            End Set
        End Property
#End Region
    End Class
    Class TypedElementList(Of ElementType As {Element, New})
        Inherits ElementList

#Region "Properties"
        Default Public Overridable Overloads ReadOnly Property Item(ByVal Index As Integer) As ElementType
            Get
                Return MyBase.Item(Index)
            End Get
        End Property
        Default Public Overrides ReadOnly Property Item(ByVal Index As Element, Optional ByVal MustRefEquals As Boolean = False) As Element
            Get
                Dim el As ElementType = Index

                Return Item(el, MustRefEquals)
            End Get
        End Property
        Default Public Overridable Overloads ReadOnly Property Item(ByVal Index As ElementType, Optional ByVal MustRefEqueals As Boolean = False) As ElementType
            Get
                Dim el As Element = Index

                Return MyBase.Item(el, MustRefEqueals)
            End Get
        End Property
        Default Public Overridable Overloads ReadOnly Property Item(ByVal Index As String) As ElementType
            Get
                Dim i As Integer
                Dim el As ElementType = Nothing

                For i = 1 To m_Elements.Count
                    el = m_Elements(i)
                    If StrComp(el.Name, Index, CompareMethod.Text) = 0 Then
                        Return el
                    End If
                Next

                Return Nothing
            End Get
        End Property
#End Region

#Region "Add"
        'Add an item
        Public Overridable Function Add(NewItem As ElementType, Optional ByVal ReturnNothingIfExists As Boolean = True) As ElementType
            Dim el As ElementType

            'Chceck to see if it is already in the list
            If (NewItem.Index > 0) AndAlso (ReferenceEquals(m_Elements(NewItem.Index), NewItem)) Then
                el = NewItem
                If ReturnNothingIfExists Then
                    el = Nothing
                End If
            Else
                'Add to the list
                el = _Add(NewItem)
                If ReferenceEquals(el, NewItem) Then
                    BatchStart()
                    RE_ItemAdded(el)
                    BatchEnd()
                Else
                    If ReturnNothingIfExists Then
                        el = Nothing
                    End If
                End If
            End If

            Return el
        End Function
        'Add an item in the right alphabetical order - and don't raise any events
        Protected Overridable Overloads Function _Add(ByRef NewItem As ElementType) As ElementType
            Return MyBase._Add(CType(NewItem, Element))
        End Function
#End Region
    End Class

    Class CommentList
        Inherits TypedElementList(Of Comment)

        Public Sub New(Optional ByVal NewParent As Object = Nothing)
            _Parent = NewParent
        End Sub

        Public Shadows Function Add(ByVal Text As String, Optional ByVal Scope As String = "", Optional ByVal ReturnNothingIfExists As Boolean = True) As Comment
            Dim el As Comment

            If Text = "" Then
                Dim ex As New Exception("Parameter 'Text' in CommentList.FriendAdd cannot be zero-lenght") : ex.Source = Me.GetType.Name : Throw ex : Return Nothing
            End If

            el = New Comment
            el._Text = Text
            el._Scope = Scope

            Return MyBase.Add(el, ReturnNothingIfExists)
        End Function

#Region "Properties"
        Public ReadOnly Property FullName() As String
            Get
                Dim i As Integer, retval As String = ""
                Dim com As Comment

                For i = 1 To m_Elements.Count
                    com = m_Elements(i)
                    retval = retval & com.Text & IIf(com.Scope <> "", " (" & com.Scope & ")", "")
                    If i < m_Elements.Count Then
                        retval = retval & vbCrLf '","
                    End If
                Next

                Return retval
            End Get
        End Property

        Public Shadows Function Rename(ByVal Item As Comment, ByVal NewText As String, ByVal NewScope As String) As Boolean
            If Item Is Nothing Then
                Dim ex As New Exception("Null reference in 'Rename'")
                ex.Source = Me.GetType.Name

                Throw ex
                Return False
            End If

            If Not ReferenceEquals(Me.Item(Item, True), Item) Then
                Dim ex As New Exception("Not a member of this list")
                ex.Source = Me.GetType.Name

                Throw ex
                Return False
            End If

            If NewText = "" Then
                Return False
            End If

            With Item
                ._Text = NewText
                ._Scope = NewScope
            End With

            BatchStart()
            RE_ItemChanged(Item, Nothing)
            Item.RE_Changed(Element.EventArgs.ActionEnum.Name, Element.EventArgs.SourceEnum.Caller, Item)
            BatchEnd()

            Return True
        End Function
#End Region

        Public Sub CopyFrom(ByVal OriginalList As CommentList, Optional ClearFirst As Boolean = True)
            Dim i As Integer

            If OriginalList Is Me Then Return

            BatchStart()

            If ClearFirst Then Clear()
            If (OriginalList IsNot Nothing) AndAlso (OriginalList.Count > 0) Then
                With OriginalList
                    For i = 1 To .Count
                        With .Item(i)
                            Add(.Text, .Scope)._Document = m_Document
                        End With
                    Next
                End With
            End If

            BatchEnd()
        End Sub
    End Class
    Class CombinationList
        Inherits TypedElementList(Of Combination)

#Region "Events"
        'Rearrange the items alphabetically
        Protected Friend Overrides Sub RE_ItemChanged(ByVal Element As Element, ByVal InnerEvent As Element.EventArgs)
            Dim doc As Document = Document

            If doc IsNot Nothing Then doc.BatchChange()

            'If the name has changed
            If (InnerEvent IsNot Nothing) AndAlso (InnerEvent.Action = Core.Element.EventArgs.ActionEnum.Name) Then
                Dim el_index As Integer = Element.Index
                Dim el As Combination
                Dim bOutOfOrder As Boolean = False

                'Check to see if it is out of order
                If el_index > 1 Then
                    el = m_Elements(el_index - 1)
                    bOutOfOrder = (StrComp(el.DisplayText, Element.DisplayText, CompareMethod.Text) = 1)
                End If
                If el_index < m_Elements.Count Then
                    el = m_Elements(el_index + 1)
                    bOutOfOrder = (StrComp(Element.DisplayText, el.DisplayText, CompareMethod.Text) = 1)
                End If
                'Re-add it
                If bOutOfOrder Then
                    m_Elements.Remove(el_index)
                    _Add(Element)

                    RE_ItemChanged(Element, Nothing)
                    Element.RE_Changed(Combination.EventArgs.ActionEnum.Index, Combination.EventArgs.SourceEnum.Caller, Element)
                End If
            End If
        End Sub
#End Region

        Public Sub New(ByVal NewParent As Spice)
            Dim cmb As Combination = New Combination(NewParent)

            _Parent = NewParent
            m_Elements.Add(cmb)
            _Reindex()
            cmb._Document = m_Document
        End Sub

        Public Overrides Function Add(ByVal NewItem As Combination, Optional ByVal ReturnNothingIfExists As Boolean = True) As Combination
            If Not ReferenceEquals(m_Parent, NewItem.Spice) Then
                Dim ex As New Exception("Wrong item to add to this combination list") : ex.Source = Me.GetType.Name : Throw ex : Return Nothing
            End If

            Return MyBase.Add(NewItem, ReturnNothingIfExists)
        End Function

#Region "Management Methods"
        Friend Overrides WriteOnly Property _Parent() As Element
            Set(ByVal value As Element)
                Dim ex As Exception = Element.TrySetParent(Of Spice)(value, m_Parent, Me) : If ex IsNot Nothing Then Throw ex : Return
            End Set
        End Property
#End Region
    End Class
    Class DefinitionList
        Inherits TypedElementList(Of Definition)

        Public Sub New(Optional ByVal NewParent As Object = Nothing)
            _Parent = NewParent
        End Sub

        Public Shadows Function Add(ByVal NewAbbrevation As String, ByVal NewFullText As String, Optional ByVal ReturnNothingIfExists As Boolean = True) As Definition
            Dim el As Definition

            If NewAbbrevation = "" Then
                Dim ex As New Exception("Cannot add a zero-length item")
                ex.Source = Me.GetType.Name

                Throw ex
                Return Nothing
            End If

            el = New Definition
            el._Name = NewAbbrevation
            el._FullText = NewFullText

            Return MyBase.Add(el, ReturnNothingIfExists)
        End Function

        Private Shadows ReadOnly Property Parent() As Element
            Get
                Return Nothing
            End Get
        End Property
    End Class
End Namespace