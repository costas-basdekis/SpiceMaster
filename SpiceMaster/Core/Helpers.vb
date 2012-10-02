Namespace Core

    'XML save/load helper
#Region "XML"
    Class XMLSaver

#Region "Element functions"
        Protected Friend Function CreateElement(ByVal Name As String, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument) As Xml.XmlElement
            If ParentNode Is Nothing Then
                Return XMLDocument.AppendChild(XMLDocument.CreateElement(Name))
            Else
                Return ParentNode.AppendChild(XMLDocument.CreateElement(Name))
            End If
        End Function
        Protected Friend Function CreateAttributedElement(ByVal Name As String, ByVal AttributeName As String, ByVal AttributeValue As String, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument) As Xml.XmlElement
            CreateAttributedElement = CreateElement(Name, ParentNode, XMLDocument)
            AddAttribute(AttributeName, AttributeValue, CreateAttributedElement, XMLDocument)
        End Function
        Protected Friend Sub AddAttribute(ByVal AttributeName As String, ByVal AttributeValue As String, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument)
            Dim att As Xml.XmlAttribute

            att = XMLDocument.CreateAttribute(AttributeName)
            att.Value = AttributeValue
            ParentNode.Attributes.SetNamedItem(att)
        End Sub
#End Region
    End Class
    Class XMLLoader

#Region "Element Functions"
        Protected Friend Function ElHasName(ByVal Element As Xml.XmlElement, ByVal Name As String) As Boolean
            If (Element Is Nothing) Then
                Return False
            End If
            If StrComp(Element.Name, Name, CompareMethod.Text) <> 0 Then
                Return False
            End If
            Return True
        End Function
        Protected Friend Function ElHasAttr(ByVal Element As Xml.XmlElement, ByVal AttributeName As String, ByRef AttributeValue As String, Optional ByVal CheckValue As Boolean = True) As Boolean
            Dim att As Xml.XmlAttribute, s As String
            If (Element Is Nothing) Then
                Return False
            End If
            att = Element.Attributes(AttributeName)
            If (att Is Nothing) Then
                Return False
            End If
            If Not (TypeOf (att.Value) Is String) Then
                Return False
            End If
            s = att.Value
            If s = "" Then
                Return False
            End If
            If CheckValue Then
                If StrComp(s, AttributeValue, CompareMethod.Text) <> 0 Then
                    Return False
                End If
            Else
                AttributeValue = s
            End If

            Return True
        End Function
        Protected Friend Function ElHasAttr(ByVal Element As Xml.XmlElement, ByVal AttributeName As String, ByRef AttributeValue As Integer, Optional ByVal CheckValue As Boolean = True) As Boolean
            Dim att As Xml.XmlAttribute, s As String, c As Integer
            If (Element Is Nothing) Then
                Return False
            End If
            att = Element.Attributes(AttributeName)
            If (att Is Nothing) Then
                Return False
            End If
            If Not (TypeOf (att.Value) Is String) Then
                Return False
            End If
            s = att.Value
            If s = "" Then
                Return False
            End If
            If Not (IsNumeric(s) AndAlso (CDbl(CInt(s)) = CDbl(s))) Then Return False
            c = CInt(s)
            If CheckValue Then
                Return (AttributeValue = c)
            Else
                AttributeValue = c
            End If

            Return True
        End Function
#End Region
    End Class
#End Region

    'Localization helper
#Region "MUI"
    Friend Class MUISaver

#Region "Variables"
        Protected m_CurrentDocument As Xml.XmlDocument = Nothing
#End Region

#Region "Properties"
        Public Property Document() As Xml.XmlDocument
            Get
                Return m_CurrentDocument
            End Get
            Set(ByVal value As Xml.XmlDocument)
                m_CurrentDocument = value
            End Set
        End Property
#End Region

#Region "Element functions"
        Protected Friend Function CreateElement(ByVal Name As String, Optional ByVal ParentNode As Xml.XmlElement = Nothing) As Xml.XmlElement
            If ParentNode Is Nothing Then
                Return m_CurrentDocument.AppendChild(m_CurrentDocument.CreateElement(Name))
            Else
                Return ParentNode.AppendChild(m_CurrentDocument.CreateElement(Name))
            End If
        End Function
        Protected Friend Function CreateAttributedElement(ByVal Name As String, ByVal AttributeName As String, ByVal AttributeValue As String, Optional ByVal ParentNode As Xml.XmlElement = Nothing) As Xml.XmlElement
            CreateAttributedElement = CreateElement(Name, ParentNode)
            AddAttribute(AttributeName, AttributeValue, CreateAttributedElement)
        End Function
        Protected Friend Sub AddAttribute(ByVal AttributeName As String, ByVal AttributeValue As String, ByVal ParentNode As Xml.XmlElement)
            Dim att As Xml.XmlAttribute

            att = m_CurrentDocument.CreateAttribute(AttributeName)
            att.Value = AttributeValue
            ParentNode.Attributes.SetNamedItem(att)
        End Sub
#End Region
    End Class
    Friend Class MUICreator
        Inherits MUISaver

        Friend Function Create(Optional Resource As String = "") As Xml.XmlDocument
            m_CurrentDocument = New Xml.XmlDocument
            m_CurrentDocument.InnerXml = Resource

            Return m_CurrentDocument
        End Function
    End Class
    Friend Class MUILoader
        Inherits XMLLoader

#Region "Variables"
        Dim m_CurrentStructure As MUISaver = Nothing
#End Region

        Friend Function Load(ByVal OriginalStructure As Xml.XmlDocument, ByVal Document As Xml.XmlDocument, Optional ByRef Language As String = "") As Xml.XmlDocument
            Dim iVer As Integer = 1, sLanName As String = ""
            Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

            m_CurrentStructure = New MUISaver
            m_CurrentStructure.Document = New Xml.XmlDocument

            xel = Document("MUIXML") : If xel Is Nothing Then Return Nothing
            If Not ElHasAttr(xel, "Language", Language, False) Then Return Nothing
            If Not ElHasAttr(xel, "Version", iVer, False) Then Return Nothing
            If Not ElHasAttr(xel, "LanguageName", sLanName, False) Then Return Nothing
            xel2 = m_CurrentStructure.CreateAttributedElement("MUIXML", "Language", Language) : m_CurrentStructure.AddAttribute("Version", iVer, xel2) : m_CurrentStructure.AddAttribute("LanguageName", sLanName, xel2)

            LoadNode(OriginalStructure("MUIXML"), xel, xel2)

            Return m_CurrentStructure.Document
        End Function
        Sub LoadNode(ByVal OriginalNode As Xml.XmlElement, ByVal ParentNode As Xml.XmlElement, ByVal StructNode As Xml.XmlElement)
            Dim i As Integer, j As Integer
            Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement, xelS As Xml.XmlElement
            Dim xatt As Xml.XmlAttribute, xatt2 As Xml.XmlAttribute

            With OriginalNode.ChildNodes
                For i = 1 To .Count
                    xel = .Item(i - 1)
                    xel2 = ParentNode(xel.Name) : If xel2 Is Nothing Then CopyNode(xel, StructNode) : Continue For 'xel2 = xel
                    xelS = m_CurrentStructure.CreateElement(xel2.Name, StructNode)
                    With xel.Attributes
                        For j = 1 To .Count
                            xatt = .Item(j - 1)
                            xatt2 = xel2.Attributes(xatt.Name)
                            If xatt2 Is Nothing Then xatt2 = xatt
                            m_CurrentStructure.AddAttribute(xatt2.Name, xatt2.Value, xelS)
                        Next
                        LoadNode(xel, xel2, xelS)
                    End With
                Next
            End With
        End Sub
        Sub CopyNode(ByVal NodeFrom As Xml.XmlElement, ByVal ParentNodeTo As Xml.XmlElement)
            Dim i As Integer
            Dim xel As Xml.XmlElement, xelS As Xml.XmlElement, xatt As Xml.XmlAttribute

            xelS = m_CurrentStructure.CreateElement(NodeFrom.Name, ParentNodeTo)
            With NodeFrom.Attributes
                For i = 1 To .Count
                    xatt = .Item(i - 1)
                    m_CurrentStructure.AddAttribute(xatt.Name, xatt.Value, xelS)
                Next
            End With
            With NodeFrom.ChildNodes
                For i = 1 To .Count
                    xel = .Item(i - 1)
                    CopyNode(xel, xelS)
                Next
            End With
        End Sub
    End Class

    Friend Class MUISource

#Region "Variables"
        Dim m_Document As Xml.XmlDocument, m_Path As String
        ReadOnly m_FallBack As MUISource, m_ReadOnly As Boolean
#End Region

        Sub New(ByVal Document As Xml.XmlDocument, ByVal FallBack As MUISource, Optional IsReadOnly As Boolean = False)
            m_Document = Document
            m_FallBack = FallBack
            m_ReadOnly = IsReadOnly
        End Sub

#Region "Properties"
        Public Property Document()
            Get
                Return m_Document
            End Get
            Set(ByVal value)
                m_Document = value
            End Set
        End Property

        Default Public ReadOnly Property Item(ByVal Category As String, ByVal ParamArray ItemPath() As String) As String
            Get
                Dim i As Integer
                Dim xel As Xml.XmlElement, xatt As Xml.XmlAttribute

                If m_Document Is Nothing Then Return FallBackString(Category, ItemPath)

                xel = m_Document("MUIXML")
                If xel Is Nothing Then Return FallBackString(Category, ItemPath)
                xel = xel(Category)
                If xel Is Nothing Then Return FallBackString(Category, ItemPath)
                For i = LBound(ItemPath) To UBound(ItemPath)
                    xel = xel(ItemPath(i))
                    If xel Is Nothing Then Return FallBackString(Category, ItemPath)
                Next
                xatt = xel.Attributes("Text")
                If xatt Is Nothing Then Return FallBackString(Category, ItemPath)

                Return xatt.Value
            End Get
        End Property

        Public ReadOnly Property Language() As String
            Get
                Return m_Document("MUIXML").Attributes("Language").Value
            End Get
        End Property
        Public ReadOnly Property LanguageName() As String
            Get
                Return m_Document("MUIXML").Attributes("LanguageName").Value
            End Get
        End Property

        Public ReadOnly Property [ReadOnly]() As Boolean
            Get
                Return m_ReadOnly
            End Get
        End Property

        Public Property Path() As String
            Get
                Return m_Path
            End Get
            Set(ByVal value As String)
                m_Path = value
            End Set
        End Property

        Public ReadOnly Property FallBackString(ByVal Category As String, ByVal ParamArray ItemPath() As String) As String
            Get
                If m_FallBack Is Nothing Then
                    Return ""
                Else
                    Return m_FallBack(Category, ItemPath)
                End If
            End Get
        End Property
#End Region

#Region "Subs"
        Public Sub Save()
            Try
                m_Document.Save(m_Path)
            Catch
            End Try
        End Sub

        Public Function CreateCopy(ByVal NewLanguage As String, ByVal NewPath As String, ByVal NewLangaugeName As String) As MUISource
            Dim ms As New MUISource(New Xml.XmlDocument, m_FallBack)
            Dim msa As New MUISaver, ml As New MUILoader
            Dim xel As Xml.XmlElement

            msa.Document = ms.Document
            xel = msa.CreateAttributedElement("MUIXML", "Language", NewLanguage) : msa.AddAttribute("Version", "1", xel) : msa.AddAttribute("LanguageName", NewLangaugeName, xel)

            ms.Document = ml.Load(m_Document, ms.Document)

            ms.Path = NewPath

            Return ms
        End Function
#End Region
    End Class
    Friend Class MUISourceCollection
#Region "Events"
        Public Event Localize(ByVal Source As MUISource)
        Public Event SourceAdded(ByVal Source As MUISource)
#End Region

#Region "Variables"
        ReadOnly m_Default As MUISource
        ReadOnly m_Elements As New Collection
        Dim m_Current As MUISource = Nothing
#End Region

#Region "Properties"
        Public ReadOnly Property [Default]() As MUISource
            Get
                Return m_Default
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return m_Elements.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As MUISource
            Get
                If (Index < 1) Or (Index > Count) Then Return Nothing

                Return m_Elements(Index)
            End Get
        End Property
        Default Public ReadOnly Property Item(ByVal Index As String) As MUISource
            Get
                Dim i As Integer, ms As MUISource

                With m_Elements
                    For i = 1 To .Count
                        ms = .Item(i)
                        If StrComp(ms.Language, Index, CompareMethod.Text) = 0 Then
                            Return ms
                        End If
                    Next
                End With

                Return Nothing
            End Get
        End Property
        Public ReadOnly Property BuiltIn(Index As String) As MUISource
            Get
                If Index = "" Then Return [Default]

                With m_Elements
                    For i As Integer = 1 To .Count
                        Dim mSource As MUISource = .Item(i)
                        If StrComp(mSource.Language, Index, CompareMethod.Text) = 0 Then
                            Return mSource
                        End If
                    Next
                End With

                Return Nothing
            End Get
        End Property
        Public ReadOnly Property ByFile(ByVal Index As String) As MUISource
            Get
                If Index = "" Then Return [Default]

                With m_Elements
                    For i As Integer = 1 To .Count
                        Dim mSource As MUISource = .Item(i)
                        If StrComp(mSource.Path, Index, CompareMethod.Text) = 0 Then
                            Return mSource
                        End If
                    Next
                End With

                Return Nothing
            End Get
        End Property

        Public Property Current() As MUISource
            Get
                Return m_Current
            End Get
            Set(ByVal value As MUISource)
                If value Is Nothing Then
                    value = m_Default
                End If

                If ReferenceEquals(m_Current, value) Then Return

                Dim bInCollection As Boolean = False
                With m_Elements
                    For i As Integer = 1 To .Count
                        If ReferenceEquals(.Item(i), value) Then
                            bInCollection = True
                            Exit For
                        End If
                    Next
                End With
                If Not bInCollection Then Throw New Exception("Not a member of the loaded MUISources")

                m_Current = value

                RaiseEvent Localize(m_Current)
            End Set
        End Property
#End Region

        Sub New()
            Dim i As Integer, s As String
            Dim mCreator As MUICreator
            Dim mLoader As New MUILoader, xml As Xml.XmlDocument, xmlSource As Xml.XmlDocument
            Dim mSource As MUISource

            'English
            mCreator = New MUICreator()
            m_Default = New MUISource(mCreator.Create(My.Resources.MUI_en), Nothing, True)
            m_Elements.Add(m_Default)
            'Greek
            mCreator = New MUICreator()
            m_Elements.Add(New MUISource(mCreator.Create(My.Resources.MUI_gr), m_Default, True))

            With My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory, FileIO.SearchOption.SearchTopLevelOnly, "MUI-*.xml")
                For i = 1 To .Count
                    s = .Item(i - 1)
                    xml = New Xml.XmlDocument
                    Try
                        xml.Load(s)
                        xmlSource = mLoader.Load(m_Default.Document, xml)
                        If xmlSource IsNot Nothing Then
                            mSource = New MUISource(xmlSource, m_Default)
                            mSource.Path = s
                            mSource.Save()
                            m_Elements.Add(mSource)
                        End If
                    Catch
                    End Try
                Next
            End With
        End Sub
        Public Sub Add(ByVal Source As MUISource)
            m_Elements.Add(Source)
            RaiseEvent SourceAdded(Source)
        End Sub
        Public Sub Refresh()
            RaiseEvent Localize(m_Current)
        End Sub
    End Class
#End Region

    'Recent documents list helper
#Region "RDL"
    Public Class RecentDocumentsList
        Public Event Changed()

        Public Const MaxItems = 5

        Private ReadOnly m_Items(MaxItems) As String
        Private m_Count As Long

        Public ReadOnly Property Count() As Long
            Get
                Return m_Count
            End Get
        End Property

        Public Property Items() As String
            Get
                If (m_Count = 0) Then Return ""

                Dim s As String = ""

                For i As Integer = 1 To m_Count
                    s &= ";" & m_Items(i)
                Next

                Return s.Substring(1)
            End Get
            Set(value As String)
                Dim i As Integer, j As Integer, k As Integer
                Dim s As String, sItems() As String
                Dim lb As Integer, ub As Integer, c As Integer

                'Extract files
                sItems = Split(value, ";")
                lb = LBound(sItems) : ub = UBound(sItems)

                'Remove same entries
                For i = lb To ub - 1
                    s = sItems(i)
                    For j = i + 1 To ub
                        If StrComp(sItems(j), s, CompareMethod.Text) = 0 Then
                            sItems(j) = ""
                        End If
                    Next
                Next
                k = 0
                For i = lb To ub
                    If sItems(i) = "" Then
                        k += 1
                    ElseIf k > 0 Then
                        sItems(i - k) = sItems(i)
                        sItems(i) = ""
                    End If
                Next
                ub -= k
                c = ub - lb + 1

                '0 < Count < Max
                m_Count = c
                If m_Count < 0 Then
                    m_Count = 0
                ElseIf c > MaxItems Then
                    m_Count = MaxItems
                End If

                'Create new list
                For i = 1 To m_Count
                    m_Items(i) = sItems(i - 1)
                Next

                RaiseEvent Changed()
            End Set
        End Property
        Public ReadOnly Property Items(Index As Long) As String
            Get
                If ((Index < 1) OrElse (Index > m_Count)) Then Throw New IndexOutOfRangeException()

                Return m_Items(Index)
            End Get
        End Property

        Public Sub Add(Path As String)
            Dim i As Integer, j As Integer

            For i = 1 To m_Count
                If StringComparer.OrdinalIgnoreCase.Compare(Path, m_Items(i)) = 0 Then
                    'Put on top
                    Dim s As String = m_Items(i)
                    For j = i To 2 Step -1
                        m_Items(j) = m_Items(j - 1)
                    Next
                    m_Items(1) = s

                    RaiseEvent Changed()

                    Return
                End If
            Next

            'Add to list
            m_Count += 1 : If m_Count > MaxItems Then m_Count = MaxItems

            For i = m_Count To 2 Step -1
                m_Items(i) = m_Items(i - 1)
            Next
            m_Items(1) = Path

            RaiseEvent Changed()
        End Sub
        Public Sub Remove(Path As String)
            Dim i As Integer, j As Integer

            For i = 1 To m_Count
                If StringComparer.OrdinalIgnoreCase.Compare(Path, m_Items(i)) = 0 Then
                    For j = i To m_Count - 1
                        m_Items(j) = m_Items(j + 1)
                    Next

                    RaiseEvent Changed()

                    Exit For
                End If
            Next
        End Sub
    End Class
#End Region

    'HTML file in-out helper
#Region "HTML File"
    Class HTMLFile
        Public HTML As String = ""
        Public FileNumber As Integer = 0
        Public Path As String = ""

        Protected Overrides Sub Finalize()
            Close()
        End Sub
        Public Function Open() As Boolean
            Dim i As Integer = 0

            Close()
GetTempFile:
            If Path = "" Then
                Path = String.Format("{0}\foods{1}.html", My.Computer.FileSystem.SpecialDirectories.Temp, CStr(i))
            End If
            Try
                FileNumber = FreeFile()
                FileOpen(FileNumber, Path, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockWrite)
            Catch ex As Exception
                If i < 2 ^ 16 Then
                    Path = ""
                    i += 1
                    GoTo GetTempFile
                Else
                    Throw
                End If
            End Try

            Return True
        End Function
        Public Sub Close(Optional ByVal DeleteFile As Boolean = True)
            If FileNumber > 0 Then
                FileClose(FileNumber)
                FileNumber = 0
            End If
            If DeleteFile Then
                Try
                    If My.Computer.FileSystem.FileExists(Path) Then
                        My.Computer.FileSystem.DeleteFile(Path)
                    End If
                Catch
                End Try
            End If
        End Sub
        Public Sub Put(Optional ByVal Text As String = "")
            Open()
            If Text = "" Then
                FilePut(FileNumber, HTML)
            Else
                FilePut(FileNumber, Text)
            End If
        End Sub
    End Class
#End Region
End Namespace