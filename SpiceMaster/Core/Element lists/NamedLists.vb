Namespace Core
    Class NamedList(Of ElementType As {NamedElement, New})
        Inherits TypedElementList(Of ElementType)

        Public Overridable Overloads Function Add(ByVal NewItem As String, Optional ByVal ReturnNothingIfExists As Boolean = True) As ElementType
            Dim el As ElementType

            If NewItem = "" Then
                Dim ex As New Exception("Cannot add a zero-length item") : ex.Source = Me.GetType.Name : Throw ex : Return Nothing
            End If

            el = New ElementType
            el._Name = NewItem

            Return MyBase.Add(el, ReturnNothingIfExists)
        End Function

        Private Shadows ReadOnly Property Parent() As Element
            Get
                Return Nothing
            End Get
        End Property
    End Class
    Class SpiceList
        Inherits NamedList(Of Spice)

#Region "Init/Terminate"
        Public Sub New(Optional ByVal NewParent As Object = Nothing)
            _Parent = NewParent
        End Sub
#End Region

#Region "Find/Search"
        'Find all starting with Name
        Public Function FindAll(ByVal Name As String) As CombinationReferenceList
            Dim i As Integer, j As Integer, cmb As Combination, spc As Spice
            Dim retval As New CombinationReferenceList

            For i = 1 To m_Elements.Count
                spc = m_Elements(i)
                With spc.DerivedCombinations
                    For j = 1 To .Count
                        cmb = .Item(j)
                        Select Case StrComp(Name, Mid(cmb.DisplayText, 1, Len(Name)), CompareMethod.Text)
                            Case 0
                                retval.Add(New CombinationReference(cmb))
                            Case -1
                                Exit For
                        End Select
                    Next j
                End With
            Next i

            Return retval
        End Function
        'Find any item -or it's comments- that contains Name
        Public Overridable Function Search(ByVal Name As String) As CombinationReferenceList
            Dim i As Integer, j As Integer, k As Integer, cmb As Combination, cmbr As CombinationReference, spc As Spice
            Dim retval As New CombinationReferenceList

            For i = 1 To m_Elements.Count
                spc = m_Elements(i)
                With spc.DerivedCombinations
                    For j = 1 To .Count
                        cmb = .Item(j)
                        If InStr(cmb.DisplayText, Name) > 0 Then
                            cmbr = retval.Add(New CombinationReference(cmb))
                            If cmbr IsNot Nothing Then cmbr.Tag.Extra = Nothing
                        Else
                            With cmb.Combinations
                                For k = 1 To .Count
                                    If InStr(.Item(k).Comments.FullName, Name) > 0 Then
                                        cmbr = retval.Add(New CombinationReference(cmb))
                                        cmbr.Tag.Extra = "oc"
                                        Exit For
                                    End If
                                Next
                            End With
                        End If
                    Next j
                End With
            Next i

            Return retval
        End Function
#End Region
    End Class
    Class FoodList
        Inherits NamedList(Of Food)

#Region "Events"
        Protected Friend Overrides Sub RE_ItemAdded(ByVal Item As Element)
            MyBase.RE_ItemAdded(Item)
            If (m_Parent IsNot Nothing) AndAlso (TypeOf m_Parent Is Food) Then
                CType(m_Parent, Food).Root.RE_ItemAdded(Item)
            End If
        End Sub
        Protected Friend Overrides Sub RE_ItemChanged(ByVal Item As Element, ByVal InnerEvent As Element.EventArgs)
            MyBase.RE_ItemChanged(Item, InnerEvent)
            If (m_Parent IsNot Nothing) AndAlso (TypeOf m_Parent Is Food) Then
                CType(m_Parent, Food).Root.RE_ItemChanged(Item, InnerEvent)
            End If
        End Sub
        Protected Friend Overrides Sub RE_ItemRemoved(ByVal Item As Element)
            MyBase.RE_ItemRemoved(Item)
            If (m_Parent IsNot Nothing) AndAlso (TypeOf m_Parent Is Food) Then
                CType(m_Parent, Food).Root.RE_ItemRemoved(Item)
            End If
        End Sub
#End Region

#Region "Init/Terminate"
        Public Sub New(Optional ByVal NewParent As Object = Nothing)
            _Parent = NewParent
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property SubCount() As Integer
            Get
                Dim i As Integer, retval As Integer

                retval = Count
                For i = 1 To Count
                    retval += Item(i).Foods.SubCount
                Next

                Return retval
            End Get
        End Property
#End Region

#Region "Add"
        'Copy items from another food
        Sub CopyFrom(OriginalList As FoodList, Optional ClearFirst As Boolean = True)
            Dim el As Food, el2 As Food, els As Collection
            Dim i As Integer

            If OriginalList Is Me Then Return

            BatchStart()

            If ClearFirst Then Clear()

            If OriginalList Is Nothing Then
                BatchEnd()
                Return
            End If

            els = OriginalList.m_Elements
            For i = 1 To els.Count
                el = els(i)
                el2 = Add(el.Name, False)
                el2.Foods.CopyFrom(el.Foods)
                el2.Combinations.CopyFrom(el.Combinations)
                el2.Comments.CopyFrom(el.Comments)
            Next

            BatchEnd()
        End Sub
#End Region

#Region "Change parent"
        Public Function MoveItemFrom(ByVal Item As Food) As Boolean
            If (Item Is Nothing) OrElse ReferenceEquals(Item.Parent, Me) OrElse HasForParent(Item) Then
                Return False
            End If

            If Item.Parent IsNot Nothing Then
                Dim PrevInd As Integer = Item.Index
                Dim fdl As FoodList = Item.Parent
                Dim e As Element.EventArgs

                If Not ReferenceEquals(_Add(Item), Item) Then Return False

                fdl.m_Elements.Remove(PrevInd)

                e = Item.RE_Changed(Element.EventArgs.ActionEnum.Parent, Element.EventArgs.SourceEnum.Caller, Item)

                If Item.Parent IsNot Nothing Then
                    With fdl.m_Elements
                        For i As Integer = 1 To .Count
                            Item = .Item(i)
                            Item._Index = i
                        Next
                    End With
                End If

                RE_ItemChanged(Item, e)
                ChildREChanged(Item.Foods)
            End If
        End Function
        Private Sub ChildREChanged(ByVal List As FoodList)
            Dim i As Integer, fd As Food

            With List
                For i = 1 To .Count
                    fd = .Item(i)
                    fd.RE_Changed(Element.EventArgs.ActionEnum.Parent, Element.EventArgs.SourceEnum.Caller, fd)
                    ChildREChanged(fd.Foods)
                Next
            End With
        End Sub
#End Region

#Region "Find/Search"
        Public Function FindAll(ByVal Name As String, Optional ByVal Results As FoodReferenceList = Nothing) As FoodReferenceList
            Dim i As Integer, s As String, ls As Integer, s1 As String, fd As Food
            Dim retval As FoodReferenceList = Results

            If retval Is Nothing Then
                retval = New FoodReferenceList
            End If

            If InStr(Name, "\\") >= 1 Then
                Return retval
            End If
            'Remove starting \
            If Mid(Name, 1, 1) = "\" Then
                Name = Mid(Name, 2)
            End If

            'Find next \
            ls = InStr(Name, "\", CompareMethod.Text)
            If ls < 1 Then
                If Len(Name) = 0 Then
                    AddAllToList(retval)
                    Return retval
                Else
                    s = Name
                    s1 = ""
                End If
            Else
                s = Mid(Name, 1, ls - 1)
                s1 = Mid(Name, ls + 1)
            End If

            If TypeOf m_Parent Is Food Then
                fd = m_Parent
            End If

            ls = Len(s)
            With m_Elements
                For i = 1 To .Count
                    fd = .Item(i)
                    If StrComp(Mid(fd.Name, 1, Len(Name)), Name, CompareMethod.Text) = 0 Then
                        retval.Add(New FoodReference(fd))
                    End If
                    Select Case StrComp(s, Mid(fd.Name, 1, ls), CompareMethod.Text)
                        Case 0
                            fd.Foods.FindAll(s1, retval)
                        Case -1
                            Exit For
                    End Select
                Next
            End With

            Return retval
        End Function
        Public Overridable Function Search(ByVal Name As String, Optional ByVal Results As FoodReferenceList = Nothing) As FoodReferenceList
            Dim i As Integer, j As Integer
            Dim fd As Food
            Dim fdr As FoodReference
            Dim retval As FoodReferenceList = Results

            If retval Is Nothing Then
                retval = New FoodReferenceList
            End If

            For i = 1 To m_Elements.Count
                fd = m_Elements(i)
                If InStr(fd.DisplayText, Name) > 0 Then
                    fdr = retval.Add(New FoodReference(fd))
                    fdr.Tag.Extra = Nothing
                    fd.Foods.AddAllToList(retval)
                Else
                    With fd.Combinations
                        For j = 1 To .Count
                            If InStr(.Item(j).Comments.FullName, Name) > 0 Then
                                fdr = retval.Add(New FoodReference(fd))
                                fdr.Tag.Extra = "oc"
                                Exit For
                            End If
                        Next
                    End With
                    fd.Foods.Search(Name, retval)
                End If
            Next

            Return retval
        End Function
        Private Sub AddAllToList(ByVal Results As FoodReferenceList)
            Dim i As Integer, fd As Food, fdr As FoodReference

            With m_Elements
                For i = 1 To .Count
                    fd = .Item(i)
                    fdr = Results.Add(New FoodReference(fd))
                    fdr.Tag.Extra = Nothing
                    fd.Foods.AddAllToList(Results)
                Next
            End With
        End Sub
#End Region

#Region "HTML"
        Public ReadOnly Property InnerHTML() As String
            Get
                Dim i As Integer, s As String = ""
                Dim fd As Food

                With m_Elements
                    For i = 1 To .Count
                        fd = .Item(i)
                        s = s & fd.InnerHTML
                    Next
                End With

                Return s
            End Get
        End Property

        Public ReadOnly Property ExportHTML() As String
            Get
                Dim i As Integer, s As String = ""
                Dim fd As Food

                With m_Elements
                    For i = 1 To .Count
                        fd = .Item(i)
                        s = s & fd.HTML.ExportHTML
                    Next
                End With

                Return s
            End Get
        End Property
#End Region
    End Class
End Namespace