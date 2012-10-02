Namespace Core
    Class ReferenceList(Of ElementType As {Element, New}, ReferenceType As {TypedReference(Of ElementType), New})
        Inherits TypedElementList(Of ReferenceType)

#Region "Init/Terminate"
        Public Sub New()
            _Parent = Nothing
        End Sub
        Public Sub New(ByVal NewParent As Object)
            _Parent = NewParent
        End Sub
#End Region

#Region "Variables"
        Public InsertOrdered As Boolean = True
#End Region

#Region "Properties"
        Default Public Overrides ReadOnly Property Item(Index As Element, Optional MustRefEquals As Boolean = False) As Element
            Get
                If TypeOf (Index) Is ElementType Then
                    Dim el As ElementType = Index

                    Return Item(el)
                ElseIf TypeOf (Index) Is ReferenceType Then
                    Dim el As ReferenceType = Index

                    Return Item(el, MustRefEquals)
                Else
                    Dim ex As New Exception("Invalid type of " & Index.GetType.Name)
                    ex.Source = Me.GetType.Name

                    Throw ex
                    Return Nothing
                End If
            End Get
        End Property
        Default Public Overridable Overloads ReadOnly Property Item(ByVal Index As ElementType) As ReferenceType
            Get
                Dim i As Integer, rel As ReferenceType

                If Index Is Nothing Then
                    Return Nothing
                End If

                For i = 1 To m_Elements.Count
                    rel = m_Elements(i)
                    If rel.Item.Equals(Index) Then
                        Return rel
                    End If
                Next

                Return Nothing
            End Get
        End Property
        Default Public Overrides ReadOnly Property Item(ByVal Index As ReferenceType, Optional ByVal MustRefEquals As Boolean = False) As ReferenceType
            Get
                If Index Is Nothing Then Return Nothing

                If Not MustRefEquals Then
                    Return Item(Index.Item)
                Else
                    Return MyBase.Item(Index, True)
                End If
            End Get
        End Property
#End Region

#Region "Add"
        Public Overrides Function Add(ByVal NewItem As ReferenceType, Optional ByVal ReturnNothingIfExists As Boolean = True) As ReferenceType
            Dim el As New ReferenceType

            el._Item = NewItem.Item
            el.Comments.CopyFrom(NewItem.Comments)

            Return MyBase.Add(el, ReturnNothingIfExists)
        End Function
        Public Overloads Function Add(NewItem As ElementType, Optional ByVal ReturnNothingIfExists As Boolean = True) As ReferenceType
            Dim el As New ReferenceType

            el._Item = NewItem

            Return MyBase.Add(el, ReturnNothingIfExists)
        End Function
        Protected Overrides Function _Add(ByRef NewItem As ReferenceType) As ReferenceType
            If InsertOrdered Then
                'Add in an ordered fashion
                Return MyBase._Add(NewItem)
            Else
                Dim el As ReferenceType = Item(NewItem)

                If el IsNot Nothing Then Return el

                m_Elements.Add(NewItem)
                _Reindex(m_Elements.Count)
                NewItem._Document = m_Document

                Return NewItem
            End If
        End Function
        'Copy contents from another list
        Public Sub CopyFrom(ByVal OriginalList As ReferenceList(Of ElementType, ReferenceType), Optional ClearFirst As Boolean = True)
            Dim i As Integer
            Dim els As Collection

            If OriginalList Is Me Then Return

            BatchStart()

            If ClearFirst Then Clear()

            If OriginalList Is Nothing Then
                BatchEnd()
                Return
            End If

            els = OriginalList.m_Elements

            For i = 1 To els.Count
                Add(els(i))
            Next

            BatchEnd()
        End Sub
#End Region

        Public Overridable Function CanHaveAsChild(ByVal Item As Element) As Boolean
            Return False
        End Function
    End Class
    Class DefinitionReferenceList
        Inherits ReferenceList(Of Definition, DefinitionReference)

#Region "Init/Terminate"
        Public Sub New(Optional ByVal NewParent As Object = Nothing)
            _Parent = NewParent
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property DisplayText(Optional ByVal Delimiter As String = " ")
            Get
                Dim i As Integer, el As DefinitionReference
                Dim retval As String = ""

                With m_Elements
                    For i = 1 To .Count
                        el = .Item(i)
                        retval = retval & Delimiter & el.DisplayText
                    Next
                    If .Count > 0 Then
                        retval = Mid(retval, 2)
                    End If
                End With

                Return retval
            End Get
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim i As Integer, j As Integer, lst As DefinitionReferenceList
            Dim el1 As DefinitionReference, el2 As DefinitionReference
            Dim b As Boolean

            If (obj Is Nothing) OrElse (Not (TypeOf obj Is DefinitionReferenceList)) Then
                Return False
            End If

            If ReferenceEquals(Me, obj) Then
                Return True
            End If

            lst = obj

            With m_Elements
                If .Count = lst.m_Elements.Count Then
                    For i = 1 To .Count
                        el1 = .Item(i)
                        b = False
                        With lst.m_Elements
                            For j = 1 To .Count
                                el2 = .Item(j)
                                If ReferenceEquals(el1.Item, el2.Item) Then
                                    b = True
                                    Exit For
                                End If
                            Next
                        End With
                        If Not b Then
                            Return False
                        End If
                    Next
                Else
                    Return False
                End If
            End With

            Return True
        End Function
#End Region
    End Class
    Class CombinationReferenceList
        Inherits ReferenceList(Of Combination, CombinationReference)

#Region "Init/Terminate"
        Public Sub New()
            _Parent = Nothing
        End Sub
        Public Sub New(ByVal NewParent As Object)
            _Parent = NewParent
        End Sub
#End Region

#Region "Insert"
        Public Function Insert(ByVal Position As Integer, ByVal InsertAfter As Boolean, ByVal NewItem As CombinationReference, Optional ByVal ReturnNothingIfExists As Boolean = True) As CombinationReference
            Dim retval As CombinationReference, bAlreadyExisted As Boolean = False

            retval = FriendInsert(Position, InsertAfter, NewItem, ReturnNothingIfExists, bAlreadyExisted)

            If bAlreadyExisted Then
                If ReturnNothingIfExists Then
                    retval = Nothing
                End If
            Else
                If retval IsNot Nothing Then
                    RE_ItemAdded(retval)
                End If
            End If

            Return retval
        End Function
        Friend Function FriendInsert(ByVal Position As Integer, ByVal InsertAfter As Boolean, ByVal NewItem As CombinationReference, Optional ByVal ReturnNothingIfExists As Boolean = True, Optional ByRef Existed As Boolean = False) As CombinationReference
            Dim el As CombinationReference

            If NewItem Is Nothing Then
                Return Nothing
            End If

            If ReferenceEquals(m_Parent, NewItem) Then
                Return Nothing
            End If

            el = New CombinationReference
            el._Item = NewItem.Item
            el.Comments.CopyFrom(NewItem.Item.Comments)
            el.ConnectWith = NewItem.ConnectWith

            If PrivateInsert(Position, InsertAfter, el, Existed) Then
                If Existed Then
                    If ReturnNothingIfExists Then
                        el = Nothing
                    End If
                Else
                    el._Parent = Me
                End If
            ElseIf ReturnNothingIfExists Then
                el = Nothing
            End If

            Return el
        End Function
        Protected Function PrivateInsert(ByVal Position As Integer, ByVal InsertAfter As Boolean, ByRef NewItem As CombinationReference, Optional ByRef Existed As Boolean = False) As Boolean
            Dim i As Integer ', j As Integer
            Dim el As CombinationReference

            If NewItem Is Nothing Then
                Return False
            End If

            With NewItem
                If (Position < 1) And (Not InsertAfter) Then
                    Position = 1
                ElseIf (Position > Count) And InsertAfter Then
                    Position = Count
                End If
                Existed = False
                For i = 1 To m_Elements.Count
                    el = m_Elements(i)
                    If el.Equals(NewItem) Then
                        NewItem.Comments.CopyFrom(el.Comments, False)
                        If InsertAfter Then
                            If el.Index <= Position Then
                                Position -= 1
                            End If
                        Else
                            If el.Index < Position Then
                                Position -= 1
                            End If
                        End If
                        m_Elements.Remove(i)
                        Existed = True
                        Exit For
                    End If
                Next
                If InsertAfter Then
                    m_Elements.Add(NewItem, , , Position)
                Else
                    m_Elements.Add(NewItem, , Position)
                End If
                For i = 1 To Count
                    CType(m_Elements(i), CombinationReference)._Index = i
                Next
            End With

            Return True
        End Function
#End Region

        Public Overrides Function CanHaveAsChild(ByVal Item As Element) As Boolean
            If m_Parent Is Nothing Then
                If (TypeOf Item Is Combination) OrElse (TypeOf Item Is Food) Then
                    Return True
                ElseIf (TypeOf Item Is CombinationReference) OrElse (TypeOf Item Is FoodReference) Then
                    Return (Not ReferenceEquals(Me, Item.Parent))
                End If
            Else
                Return m_Parent.CanHaveAsChild(Item)
            End If

            Return False
        End Function
    End Class
    Class FoodReferenceList
        Inherits ReferenceList(Of Food, FoodReference)

#Region "Init/Terminate"
        Public Sub New()
            _Parent = Nothing
        End Sub
        Public Sub New(ByVal NewParent As Object)
            _Parent = NewParent
        End Sub
#End Region

        Public Overrides Function CanHaveAsChild(ByVal Item As Element) As Boolean
            If m_Parent Is Nothing Then
                If TypeOf Item Is Food Then
                    Return True
                ElseIf TypeOf Item Is FoodReference Then
                    Return (Not ReferenceEquals(Me, Item.Parent))
                End If
            Else
                Return m_Parent.CanHaveAsChild(Item)
            End If

            Return False
        End Function
    End Class
End Namespace