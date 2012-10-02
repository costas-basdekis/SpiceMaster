Namespace Core
    'Base class of elements
    MustInherit Class Element

        Public Overrides Function ToString() As String
            Return IIf(Document Is Nothing, "!", "") & Name & "[" & Me.GetType.Name & "," & GetHashCode() & "]"
        End Function

        'Addition data to embed to items (such as an element in the GUI)
#Region "Tag"
        Class RefTag

#Region "Events"
#Region "Item"
            Friend Sub Item_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles m_Item.Changed
                If (TypeOf m_Item Is CombinationReference) OrElse (TypeOf m_Item Is FoodReference) Then Return

                Dim s As String = m_Item.DisplayText

                If TypeOf m_Item Is Food Then
                    Dim fd As Food = CType(m_Item, Food)
                    s = fd.Name
                End If

                If (ListViewItem IsNot Nothing) AndAlso (ListViewItem.ListView IsNot Nothing) AndAlso (ListViewItem.Index >= 0) Then
                    Dim lvi As ListViewItem = ListViewItem, lvs As ListView.ListViewItemCollection = lvi.ListView.Items
                    'Rename
                    If TypeOf m_Item Is Definition Then
                        Dim def As Definition = CType(m_Item, Definition)
                        lvi.Text = def.FullText
                        lvi.SubItems(1).Text = def.DisplayText
                    Else
                        lvi.Text = s
                    End If
                    'ReOrder
                    If (e IsNot Nothing) AndAlso (e.Action = EventArgs.ActionEnum.Index) Then
                        If TypeOf m_Item Is Combination Then
                        Else
                            lvi.Remove()
                            lvs.Insert(Item.Index - 1, lvi)
                        End If
                    End If
                End If
                If (TreeNode IsNot Nothing) AndAlso (TreeNode.TreeView IsNot Nothing) AndAlso (TreeNode.Index >= 0) Then
                    Dim tn As TreeNode = TreeNode, tns As TreeNodeCollection = tn.TreeView.Nodes
                    If e.Action = EventArgs.ActionEnum.Parent Then
                        If e.Source = EventArgs.SourceEnum.Caller Then
                            Dim fd As Food = e.SourceObject
                            If fd.Parent.Parent Is Nothing Then
                                tn = tn.TreeView.Nodes(0)
                            Else
                                tn = fd.Parent.Parent.Tag.TreeNode
                            End If
                            tns = tn.Nodes
                            fd.Tag.TreeNode.Remove()
                            tns.Insert(fd.Index - 1, fd.Tag.TreeNode)
                            tn = fd.Tag.TreeNode
                            tn.Tag = fd
                            fd.Tag.TreeNode = tn
                        End If
                    Else
                        'Rename
                        If tn.Text <> s Then
                            tn.Text = s
                        End If
                        'ReOrder
                        If tn.Parent IsNot Nothing Then
                            tns = tn.Parent.Nodes
                        End If
                        If tns.Count > 1 Then
                            Dim new_index As Integer = m_Item.Index - 1
                            If TypeOf m_Item Is Combination Then
                                If m_Item.Index > 1 Then
                                    new_index = new_index - 1
                                Else
                                    new_index = -1
                                End If
                            End If
                            If (new_index >= 0) AndAlso (new_index <> tn.Index) Then
                                tn.Remove()
                                tns.Insert(new_index, tn)
                            End If
                        End If
                    End If
                End If
            End Sub
            Friend Sub Item_Removed(ByVal sender As Object, ByVal e As EventArgs) Handles m_Item.Removed

                If (TypeOf m_Item Is CombinationReference) OrElse (TypeOf m_Item Is FoodReference) Then Return

                If ListViewItem IsNot Nothing Then
                    ListViewItem.Remove()
                End If
                If TreeNode IsNot Nothing Then
                    TreeNode.Remove()
                End If
            End Sub
#End Region
#End Region

#Region "Variables"
            'When deleting an item, we remove all references of that item for GC. This is a flag of whether we have done it once
            Private m_RRed As Boolean = False

            Protected WithEvents m_Item As Element
            'Any extra data
            Public Extra As Object = Nothing
            'GUI elemetns
            Public ListViewItem As ListViewItem, TreeNode As TreeNode
#End Region

#Region "Init/Terminate"
            Public Sub New(ByVal NewItem As Element)
                m_Item = NewItem
            End Sub

            Protected Overrides Sub Finalize()
                If Not m_RRed Then Me.RemRefs()
                MyBase.Finalize()
            End Sub
            'Remove all references of self
            Friend Sub RemRefs()
                m_RRed = True
                m_Item = Nothing
                Extra = Nothing
                TreeNode = Nothing
                ListViewItem = Nothing
            End Sub
#End Region

#Region "Properties"
            Public ReadOnly Property Item() As Element
                Get
                    Return m_Item
                End Get
            End Property
#End Region
        End Class
#End Region
        'Various events when the element is created/changed/deleted
#Region "Events"
        'Events info while propagating an event
        Class EventArgs
            Inherits System.EventArgs

#Region "Enums"
            Enum ActionEnum
                Unspecified = 0
                'Changed
                Index
                Name
                Parent
                Item
                DerivedCombinations
                DerivedFoods
                Combinations
                Foods
                Comments
                'Removed
                Removed
            End Enum

            Enum SourceEnum
                Unspecified = 0
                Caller
                EventArgs
                ElementListEventArgs
            End Enum
#End Region

#Region "Variables"
            Protected m_Action As ActionEnum = ActionEnum.Unspecified
            Protected m_Source As SourceEnum = SourceEnum.Caller, m_SourceObject As Object = Nothing
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
#Region "Changed"
        Public Event Changed(ByVal sender As Object, ByVal e As EventArgs)
        Protected Friend Overridable Function RE_Changed(ByVal Action As EventArgs.ActionEnum, ByVal Source As EventArgs.SourceEnum, ByVal SourceObject As Object) As EventArgs
            Dim e As New EventArgs(Action, Source, SourceObject)

            _RE_Changed(e)

            Return e
        End Function
        Protected Sub _RE_Changed(ByRef e As EventArgs)
            Dim doc As Document = Document

            If doc IsNot Nothing Then
                doc.BatchChange()
            End If

            frmDocumentChanges.ElementChange(e.ToString, Me)
            RaiseEvent Changed(Me, e)
        End Sub
#End Region
#Region "Removed"
        Public Event Removed(ByVal sender As Object, ByVal e As EventArgs)
        Protected Friend Overridable Sub RE_Removed()
            Dim e As New EventArgs(EventArgs.ActionEnum.Removed, EventArgs.SourceEnum.Caller, Me)

            _RE_Removed(e)
        End Sub
        Protected Friend Overridable Sub _RE_Removed(ByVal e As EventArgs)
            Dim doc As Document = Document

            If doc IsNot Nothing Then
                doc.BatchChange()
            End If

            frmDocumentChanges.ElementChange(Me.GetType.FullName & ".Removed", Me)
            RaiseEvent Removed(Me, e)
        End Sub
#End Region

        Protected Overridable Sub Comments_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_Comments.BatchEnded
            RE_Changed(EventArgs.ActionEnum.Comments, EventArgs.SourceEnum.ElementListEventArgs, ee)
        End Sub
#End Region

#Region "Variables"
        Public UUID As New Random
        Friend m_RRed As Boolean = False

        Protected m_Document As Document = Nothing

        Protected m_Index As Integer = 0
        Protected m_Parent As ElementList = Nothing
        Protected m_Renaming As Boolean = False

        Public Tag As New RefTag(Me)

        Protected WithEvents m_Comments As New CommentList(Me)

        Protected Friend FileUID As Integer
#End Region

        'Remove references of self
        Friend Overridable Sub RemRefs(Optional Forced As Boolean = False)
            If (Not Forced) And m_RRed Then Return

            If Not m_RRed Then RE_Removed()

            m_RRed = True
            m_Comments.RemRefs() : m_Comments = Nothing
            m_Document = Nothing
            m_Index = -1
            m_Parent = Nothing
            Tag.RemRefs()

            MyBase.Finalize()
        End Sub

#Region "Properties"
        Public ReadOnly Property Index() As Integer
            Get
                Return m_Index
            End Get
            'Set(ByVal value As Integer)
            '	If m_Index <> value Then
            '		If (m_Index <> 0) AndAlso (value <> 0) Then
            '			m_Index = value
            '			RE_Changed(EventArgs.ActionEnum.Index, EventArgs.SourceEnum.Caller, Me)
            '		Else
            '			m_Index = value
            '		End If
            '	End If
            'End Set
        End Property

        Public Overridable Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                Rename(value)
            End Set
        End Property
        Public Overridable Sub Rename(ByVal NewName As String)
            Dim p As ElementList = m_Parent

            m_Renaming = True
            If p Is Nothing Then
                _Name = NewName
            Else
                p.Rename(Me, NewName)
            End If
            m_Renaming = False
        End Sub
        'Display text is the fully qualified text, eg for Food is the complete path, for Definition is the full name
        Public Overridable ReadOnly Property DisplayText() As String
            Get
                Return _Name
            End Get
        End Property

        Public ReadOnly Property Comments() As CommentList
            Get
                Return m_Comments
            End Get
        End Property

        Public ReadOnly Property Document() As Document
            Get
                Return m_Document
            End Get
        End Property

        Public Overridable Property Parent() As ElementList
            Get
                Return m_Parent
            End Get
            Set(ByVal value As ElementList)
                Throw New Exception("Cannot re-set parent")
            End Set
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean

            Return _Equals(obj)
        End Function
        Protected Overridable Function _Equals(ByVal obj As Element) As Boolean
            Return Me Is obj
        End Function
#End Region

#Region "Remove"
        Public Overridable Sub Remove()
            Dim p As ElementList = m_Parent
            Static bRemoving As Boolean = False

            If bRemoving Then
                Return
            End If
            bRemoving = True

            If p Is Nothing Then
                Dim ex As New Exception("Parent does not exist.")
                ex.Source = Me.GetType.Name

                Throw ex
                Return
            End If

            p.Remove(Me)

            bRemoving = False
        End Sub
#End Region
        'These methods are internal to the class (ie private), to change the structure
#Region "Management methods"
        Protected Friend WriteOnly Property _Index() As Integer
            Set(ByVal value As Integer)
                m_Index = value
            End Set
        End Property

        Protected Friend Overridable Property _Name() As String
            Get
                Debug.Fail("This property  should be overrided or never used by this class")
                Return ""
            End Get
            Set(ByVal value As String)
                Debug.Fail("This property  should be overrided or never used by this class. How did you forget to implement it you twit?")
            End Set
        End Property

        Friend Shared Function TrySetParent(Of CallerParentType)(ByVal value As Object, ByRef CallerParent As Object, ByVal Caller As Object) As Exception
            If value Is Nothing Then
                CallerParent = Nothing
            End If

            'If CallerParent IsNot Nothing Then
            '	Dim ex As New Exception("Cannot re-set")
            '	ex.Source = Caller.GetType.Name
            '	Return ex
            'End If

            If Not ((value Is Nothing) OrElse (TypeOf value Is CallerParentType)) Then
                Dim ob As CallerParentType = Nothing
                Dim ex As New Exception("Trying to set the parent with one of different type: '" & value.GetType.Name & "' instead of '" & ob.GetType.Name & "'")
                ex.Source = Caller.GetType.Name
                Return ex
            End If

            CallerParent = value

            Return Nothing
        End Function
        Friend Overridable WriteOnly Property _Parent() As ElementList
            Set(ByVal value As ElementList)
                m_Parent = value
            End Set
        End Property

        Public Overridable Function CanHaveAsChild(ByVal Item As Element) As Boolean
            Return False
        End Function

        Protected Friend Overridable WriteOnly Property _Document As Document
            Set(value As Document)
                m_Document = value
                m_Comments._Document = value
            End Set
        End Property
#End Region
    End Class

    Class Comment
        Inherits Element

#Region "Variables"
        Protected m_Text As String = "", m_Scope As String = ""
#End Region

#Region "Properties"
        Public Overrides ReadOnly Property DisplayText() As String
            Get
                Return m_Text
            End Get
        End Property
        Public Property Text() As String
            Get
                Return m_Text
            End Get
            Set(ByVal value As String)
                m_Text = value
                RE_Changed(EventArgs.ActionEnum.Name, EventArgs.SourceEnum.Caller, Me)
            End Set
        End Property
        Public Property Scope() As String
            Get
                Return m_Scope
            End Get
            Set(ByVal value As String)
                m_Scope = value
                RE_Changed(EventArgs.ActionEnum.Name, EventArgs.SourceEnum.Caller, Me)
            End Set
        End Property
        Public Shadows Sub Rename(ByVal NewText As String, ByVal NewScope As String)
            Dim p As CommentList = m_Parent

            If p Is Nothing Then
                _Name = NewText
            Else
                p.Rename(Me, NewText, NewScope)
            End If
        End Sub

        Protected Overrides Function _Equals(ByVal obj As Element) As Boolean
            Dim com As Comment = obj

            Return (StrComp(m_Text, com.m_Text, CompareMethod.Text) = 0) AndAlso (StrComp(m_Scope, com.m_Scope, CompareMethod.Text) = 0)
        End Function
#End Region

#Region "Management methods"
        Friend WriteOnly Property _Text() As String
            Set(ByVal value As String)
                m_Text = value
            End Set
        End Property
        Friend WriteOnly Property _Scope() As String
            Set(ByVal value As String)
                m_Scope = value
            End Set
        End Property

        Protected Friend Overrides Property _Name() As String
            Get
                Return m_Text
            End Get
            Set(ByVal value As String)
                Debug.Fail("This property  should be overrided or never used by this class")
            End Set
        End Property

        Friend Overrides WriteOnly Property _Parent() As ElementList
            Set(ByVal value As ElementList)
                Dim ex As Exception = TrySetParent(Of CommentList)(value, m_Parent, Me) : If ex IsNot Nothing Then Throw ex : Return
            End Set
        End Property
#End Region
    End Class
    Class Combination
        Inherits Element

#Region "Events"
        Protected Friend Overrides Function RE_Changed(ByVal Action As EventArgs.ActionEnum, ByVal Source As EventArgs.SourceEnum, ByVal SourceObject As Object) As EventArgs
            Dim e As New EventArgs(Action, Source, SourceObject)

            _RE_Changed(e)

            If m_Parent IsNot Nothing Then
                m_Parent.RE_ItemChanged(Me, e)
            End If

            Return e
        End Function
        Protected Friend Overrides Sub RE_Removed()
            If m_Combinations IsNot Nothing Then m_Combinations.Clear()
            If m_Foods IsNot Nothing Then m_Foods.Clear()

            MyBase.RE_Removed()
        End Sub
        'Binded items events - we monitor the Food, Spice and Definitions for changes to update thhe GUI
#Region "Items"
        Protected Sub Spice_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles m_Spice.Changed
            If e.Action = EventArgs.ActionEnum.Name Then
                RE_Changed(EventArgs.ActionEnum.Name, EventArgs.SourceEnum.EventArgs, e)
            End If
        End Sub
        Protected Sub Definitions_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_Definitions.BatchEnded
            RE_Changed(EventArgs.ActionEnum.Name, EventArgs.SourceEnum.EventArgs, ee)
        End Sub

        Protected Sub Combinations_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_Combinations.ItemAdded, m_Combinations.ItemRemoved, m_Combinations.BatchEnded
            'RE_Changed(EventArgs.ActionEnum.Combinations, EventArgs.SourceEnum.EventArgs, ee)
            Select Case ee.Action
                Case ElementList.EventArgs.ActionEnum.ItemAdded
                    Dim cmbr As CombinationReference = ee.SourceObject

                    'Add Me at the other item
                    cmbr.Item.Combinations.Add(Me)
                Case ElementList.EventArgs.ActionEnum.ItemRemoved
                    Dim cmbr As CombinationReference = ee.SourceObject

                    'Remove Me from the other item
                    Dim cmbrl As CombinationReferenceList = cmbr.Item.Combinations

                    If cmbrl IsNot Nothing Then
                        cmbr = cmbrl.Item(Me)
                        If cmbr IsNot Nothing Then
                            cmbr.Remove()
                        End If
                    End If

                    'Spice forms (ie Combination) exist only if they are referenced somewhere
                    If (m_Definitions.Count > 0) AndAlso (m_Combinations.Count = 0) AndAlso (m_Foods.Count = 0) Then
                        Remove()
                    End If
                Case ElementList.EventArgs.ActionEnum.BatchEnded
                    'Announce they changed
                    RE_Changed(EventArgs.ActionEnum.Combinations, EventArgs.SourceEnum.EventArgs, ee)
            End Select
        End Sub
        Protected Sub Foods_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_Foods.ItemAdded, m_Foods.ItemRemoved, m_Foods.BatchEnded
            If ee.Source = ElementList.EventArgs.SourceEnum.ListItem Then
                Dim fdr As FoodReference = ee.SourceObject

                'RE_Changed(EventArgs.ActionEnum.DerivedFoods, EventArgs.SourceEnum.ElementListEventArgs, ee)
                Select Case ee.Action
                    Case ElementList.EventArgs.ActionEnum.ItemAdded
                        'Add Me at the other item
                        fdr.Item.Combinations.Add(Me)
                    Case ElementList.EventArgs.ActionEnum.ItemRemoved
                        'Remove Me from the other item
                        Dim cmbrl As CombinationReferenceList = fdr.Item.Combinations, cmbr As CombinationReference

                        If cmbrl IsNot Nothing Then
                            cmbr = cmbrl.Item(Me)
                            If cmbr IsNot Nothing Then
                                cmbr.Remove()
                            End If
                        End If

                        'Spice forms (ie Combination) exist only if they are referenced somewhere
                        If (m_Definitions.Count > 0) AndAlso (m_Combinations.Count = 0) AndAlso (m_Foods.Count = 0) Then
                            Remove()
                        End If
                    Case ElementList.EventArgs.ActionEnum.BatchEnded
                        'Announce they changed
                        RE_Changed(EventArgs.ActionEnum.DerivedFoods, EventArgs.SourceEnum.ElementListEventArgs, ee)
                End Select
            End If
        End Sub
#End Region
#End Region

#Region "Variables"
        Protected WithEvents m_Spice As Spice, m_Definitions As New DefinitionReferenceList(Me), m_Combinations As New CombinationReferenceList(Me), m_Foods As New FoodReferenceList(Me)
        'HTML identifier
        Friend UID As UInt32
#End Region

#Region "Init/Terminate"
        Public Sub New()
            Dim ex As New Exception("Method 'New()' should not be called by this class")
            ex.Source = Me.GetType.Name

            Throw ex
            Return
        End Sub
        Public Sub New(ByVal NewSpice As Spice)
            m_Spice = NewSpice
        End Sub
        Public Sub New(ByVal Template As Combination)
            With Template
                m_Spice = .m_Spice
                m_Definitions.CopyFrom(.m_Definitions)
            End With
        End Sub

        Protected Overrides Sub Finalize()
            Food.HTMLTag.UIDs.RemoveUID(UID)
            MyBase.Finalize()
        End Sub
        Friend Overrides Sub RemRefs(Optional Forced As Boolean = False)
            If (Not Forced) And m_RRed Then Return
            m_RRed = True

            m_Combinations.RemRefs() : m_Combinations = Nothing
            m_Foods.RemRefs() : m_Foods = Nothing

            MyBase.RemRefs(True)

            m_Definitions.RemRefs() : m_Definitions = Nothing
            m_Spice = Nothing
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property Spice() As Spice
            Get
                Return m_Spice
            End Get
        End Property
        Public ReadOnly Property Definitions() As DefinitionReferenceList
            Get
                Return m_Definitions
            End Get
        End Property
        Public ReadOnly Property Combinations() As CombinationReferenceList
            Get
                Return m_Combinations
            End Get
        End Property
        Public ReadOnly Property Foods() As FoodReferenceList
            Get
                Return m_Foods
            End Get
        End Property

        Public Overrides ReadOnly Property DisplayText() As String
            Get
                Dim s As String = " " & m_Definitions.DisplayText
                If s = " " Then
                    s = ""
                End If
                Return m_Spice.DisplayText & s
            End Get
        End Property

        Protected Overrides Function _Equals(ByVal obj As Element) As Boolean
            Dim comb As Combination = obj

            If obj Is Nothing Then Return False

            Return (ReferenceEquals(m_Spice, comb.m_Spice) AndAlso m_Definitions.Equals(comb.m_Definitions))
        End Function
#End Region

        Public Function Convert(ByVal Target As Combination) As Combination
            Dim i As Integer
            Dim combr As CombinationReference, combr2 As CombinationReference
            Dim ncmb As Combination = New Combination(Target.Spice)

            If Target Is Nothing Then Return Me
            If Target.Equals(Me) Then Return Me

            With Target.Definitions
                For i = 1 To .Count
                    ncmb.Definitions.Add(.Item(i))
                Next
            End With
            'Add it
            ncmb = Target.Spice.DerivedCombinations.Add(ncmb, False)
            'Convert old's combinations
            With Combinations
                For i = .Count To 1 Step -1
                    With .Item(i).Item.Combinations
                        combr = .Item(Me)
                        If combr IsNot Nothing Then
                            combr2 = .Item(ncmb)
                            If combr2 IsNot Nothing Then
                                combr2.Remove()
                                combr.Comments.CopyFrom(combr2.Comments, False)
                            End If
                            combr.Item = ncmb
                            combr.Comments.CopyFrom(Target.Comments, False)
                        End If
                    End With
                Next
            End With
            'Convert old's foods
            If Foods IsNot Nothing Then
                With Foods
                    For i = .Count To 1 Step -1
                        With .Item(1).Item.Combinations
                            combr = .Item(Me)
                            If combr IsNot Nothing Then
                                combr.Item = ncmb
                                combr.Comments.CopyFrom(Target.Comments, False)
                            End If
                        End With
                    Next
                End With
            End If

            Return ncmb
        End Function

#Region "Management functions"
        Protected Friend Overrides Property _Name() As String
            Get
                Return DisplayText
            End Get
            Set(ByVal value As String)
                MyBase._Name = value
            End Set
        End Property

        Protected Friend WriteOnly Property _Spice() As Spice
            Set(ByVal value As Spice)
                m_Spice = value
            End Set
        End Property
        'For drag'n'drop operations to check if the item can be a child or converted to an appropriate type
        Public Overrides Function CanHaveAsChild(ByVal Item As Element) As Boolean
            If TypeOf Item Is Combination Then
                If Not ReferenceEquals(m_Spice, CType(Item, Combination).Spice) Then Return True
            ElseIf TypeOf Item Is CombinationReference Then
                If Not ReferenceEquals(m_Spice, CType(Item, CombinationReference).Item.Spice) Then Return True
            ElseIf (TypeOf Item Is Food) OrElse (TypeOf Item Is FoodReference) Then
                Return True
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