Namespace Core
    MustInherit Class Reference
        Inherits Element

        'Clumsy and awkward way to get the Item property
        Public MustOverride ReadOnly Property MyItem As Element

        Public Overrides Function ToString() As String
            If MyItem Is Nothing Then
                Return IIf(Document Is Nothing, "!", "") & "?" & "[" & Me.GetType.Name & "," & GetHashCode() & "]"
            Else
                Return IIf(Document Is Nothing, "!", "") & Name & "[" & Me.GetType.Name & "," & GetHashCode() & "]"
            End If
        End Function
    End Class
    MustInherit Class TypedReference(Of ElementType As {Element, New})
        Inherits Reference

#Region "Events"
        Protected Friend Overrides Function RE_Changed(ByVal Action As EventArgs.ActionEnum, ByVal Source As EventArgs.SourceEnum, ByVal SourceObject As Object) As EventArgs
            Dim e As EventArgs = New EventArgs(Action, Source, SourceObject)

            _RE_Changed(e)

            If m_Parent IsNot Nothing Then
                m_Parent.RE_ItemChanged(Me, e)
            End If

            Return e
        End Function
        'Inform the parent list
#Region "Item"
        Protected Overridable Sub Item_Changed(ByVal sender As Object, ByVal e As Element.EventArgs) Handles m_Item.Changed
            If (e.Action = EventArgs.ActionEnum.Name) Or (e.Action = EventArgs.ActionEnum.Parent) Then
                RE_Changed(EventArgs.ActionEnum.Item, EventArgs.SourceEnum.ElementListEventArgs, e)
            ElseIf e.Action = EventArgs.ActionEnum.Combinations Then
                If m_Parent IsNot Nothing Then
                    m_Parent.RE_ItemChanged(Me, New EventArgs(EventArgs.ActionEnum.Combinations, EventArgs.SourceEnum.Unspecified, Nothing))
                End If
                'It is the culprit of a great load of delay - the guard structure is definitely not designed corectly
                'RE_Changed(EventArgs.ActionEnum.Combinations, EventArgs.SourceEnum.Unspecified, Nothing)
            End If
        End Sub
        Protected Overridable Sub Item_Removed(ByVal sender As Object, ByVal e As Element.EventArgs) Handles m_Item.Removed
            If m_Parent IsNot Nothing Then
                Remove()
            End If
        End Sub
#End Region
#End Region

#Region "Variables"
        Protected WithEvents m_Item As ElementType
#End Region

#Region "Init/Terminate"
        Public Sub New()
            '
        End Sub
        Public Sub New(ByVal NewItem As ElementType)
            _Item = NewItem
        End Sub
        Public Sub New(ByVal Template As TypedReference(Of ElementType))
            m_Item = Template.m_Item
            m_Comments.CopyFrom(Template.Comments)
        End Sub

        Friend Overrides Sub RemRefs(Optional Forced As Boolean = False)
            If (Not Forced) And m_RRed Then Exit Sub

            m_Item = Nothing

            MyBase.RemRefs(True)

            m_RRed = True
        End Sub
#End Region

#Region "Properties"
        Public Property Item() As ElementType
            Get
                Return m_Item
            End Get
            Set(ByVal value As ElementType)
                If Not ReferenceEquals(m_Item, value) Then
                    _Item = value
                    RE_Changed(EventArgs.ActionEnum.Item, EventArgs.SourceEnum.Caller, Me)
                End If
            End Set
        End Property
        Public Overrides ReadOnly Property MyItem As Element
            Get
                Return m_Item
            End Get
        End Property

        Public Overrides ReadOnly Property DisplayText() As String
            Get
                Dim el As ElementType = m_Item

                Return el.DisplayText
            End Get
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim tpdr As TypedReference(Of ElementType) = obj
            If Not m_Item.Equals(tpdr.m_Item) Then Return False

            Return MyBase.Equals(obj)
        End Function
#End Region

#Region "Management Methods"
        Protected Friend Sub CopyFrom(ByVal OriginalElement As Object)
            m_Item = OriginalElement.Item
        End Sub
        Protected Friend Overridable WriteOnly Property _Item() As ElementType
            Set(ByVal value As ElementType)
                m_Item = value
            End Set
        End Property

        Protected Friend Overrides Property _Name() As String
            Get
                Return DisplayText
            End Get
            Set(ByVal value As String)
                Debug.Fail("This property  should be overrided or never used by this class")
            End Set
        End Property
#End Region
    End Class
    Class DefinitionReference
        Inherits TypedReference(Of Definition)

#Region "Init/Terminate"
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal NewItem As Definition)
            MyBase.New(NewItem)
        End Sub
#End Region
    End Class
    Class CombinationReference
        Inherits TypedReference(Of Combination)

        'eg "Epazote and basil", "Anice or agostura", it matters only when displaying as text
        Enum ConnectWithEnum
            None
            [And]
            [Or]
        End Enum

#Region "Variables"
        Protected m_ConnectWith As ConnectWithEnum = ConnectWithEnum.None
#End Region

#Region "Init/Terminate"
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal NewItem As Combination)
            MyBase.New(NewItem)
        End Sub
        Public Sub New(ByVal Template As CombinationReference)
            Me.New(Template.Item)
            With Template
                m_Comments.CopyFrom(.Comments)
                m_ConnectWith = .ConnectWith
            End With
        End Sub
#End Region

#Region "Properties"
        Public Property ConnectWith() As ConnectWithEnum
            Get
                Return m_ConnectWith
            End Get
            Set(ByVal value As ConnectWithEnum)
                If m_ConnectWith <> value Then
                    m_ConnectWith = value
                    RE_Changed(EventArgs.ActionEnum.Name, EventArgs.SourceEnum.Caller, Me)
                End If
            End Set
        End Property
#End Region

#Region "Management Methods"
        Protected Friend Overrides WriteOnly Property _Item() As Combination
            Set(ByVal value As Combination)
                If Not ReferenceEquals(m_Item, value) Then
                    Dim pEl As Combination, el As Combination

                    pEl = m_Item
                    m_Item = value
                    If m_Parent IsNot Nothing Then
                        Dim p As CombinationReferenceList = m_Parent
                        If TypeOf p.Parent Is Combination Then
                            Dim cmb As Combination = p.Parent
                            el = value
                            el.Combinations.Add(New CombinationReference(cmb))
                            pEl.Combinations.Remove(pEl.Combinations(cmb))
                        ElseIf TypeOf p.Parent Is Food Then
                            Dim fd As Food = p.Parent
                            el = value
                            el.Foods.Add(New FoodReference(fd))
                            pEl.Foods.Remove(pEl.Foods(fd))
                        End If
                    End If
                End If
            End Set
        End Property
#End Region
    End Class
    Class FoodReference
        Inherits TypedReference(Of Food)

#Region "Init/Terminate"
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal NewItem As Food)
            MyBase.New(NewItem)
        End Sub
#End Region
    End Class
End Namespace