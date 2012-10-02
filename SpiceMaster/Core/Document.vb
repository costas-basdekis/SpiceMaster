Imports System.Threading

Namespace Core
    Class Document
        Public Class LoadStatus
            Public TotalItemCount As Long, CurrentItemCount As Long

            Public Event LoadStart()
            Public Event LoadItem()

            Public Sub Start()
                CurrentItemCount = 0
                RaiseEvent LoadStart()
            End Sub
            Public Sub Item()
                CurrentItemCount += 1
                RaiseEvent LoadItem()
            End Sub
        End Class

#Region "Events"
        Public Event Loaded()
        Public Event HTMLRefreshed()
#End Region

#Region "Variables"
        Dim m_Spices As New SpiceList, m_Foods As New FoodList, m_Definitions As New DefinitionList
        Dim m_MyShelf As New CombinationReferenceList

        Dim m_BatchCount As Integer = 0, m_BatchChanges As Integer = 0
        Dim m_ChangedSinceLastSaved As Boolean = False

        Dim m_BatchingCount As Integer = 0

        Dim m_FilePath As String = ""

        'Cached static HTML
        Shared m_PreHTML As String = "", m_PostHTML As String = ""

        Public ClipBoard As New Clipboard
#End Region

        Sub New()
            m_Definitions._Document = Me
            m_Spices._Document = Me
            m_Foods._Document = Me
            m_MyShelf._Document = Me
            PrepareHTML()
        End Sub
        Public Sub Clear()
            Dim DefList As New DefinitionList, SpcList As New SpiceList, FdList As New FoodList

            m_Definitions.MoveFrom(New DefinitionList)
            m_Spices.MoveFrom(New SpiceList)
            m_Foods.MoveFrom(New FoodList)
            m_MyShelf = New CombinationReferenceList

            m_FilePath = ""
            m_ChangedSinceLastSaved = False
            ClipBoard.Clear()
            RaiseEvent Loaded()
        End Sub
        Public Sub MoveFrom(Other As Document, Optional KillMeAfter As Boolean = True)
            Dim d As Document = Nothing

            If KillMeAfter Then
                d = New Document
                d.MoveFrom(Me, False)
            End If

            'Copy items
            With Other
                m_Definitions.MoveFrom(.m_Definitions)
                m_Spices.MoveFrom(.m_Spices)
                m_Foods.MoveFrom(.m_Foods)
                m_MyShelf.MoveFrom(.m_MyShelf)
            End With
            ClipBoard.Clear()

            'Kill in the background
            If KillMeAfter Then
                Dim thrd As Thread

                thrd = New Thread(AddressOf d.KillMe)
                thrd.IsBackground = True
                thrd.Priority = ThreadPriority.BelowNormal
                thrd.Start()
            End If
        End Sub
        'Kill a document's items and references
        Private Sub KillMe()
            m_Foods.Clear()
            m_Spices.Clear()
            m_Definitions.Clear()
            m_MyShelf.Clear()
        End Sub

#Region "Properties"
#Region "Lists"
        Public ReadOnly Property Spices() As SpiceList
            Get
                Return m_Spices
            End Get
        End Property
        Public ReadOnly Property Foods() As FoodList
            Get
                Return m_Foods
            End Get
        End Property
        Public ReadOnly Property Definitions() As DefinitionList
            Get
                Return m_Definitions
            End Get
        End Property
        Public ReadOnly Property MyShelf() As CombinationReferenceList
            Get
                Return m_MyShelf
            End Get
        End Property
#End Region

        Public Property FilePath() As String
            Get
                Return m_FilePath
            End Get
            Set(value As String)
                m_FilePath = value
            End Set
        End Property

        Public ReadOnly Property ChangedSinceLastSaved() As Boolean
            Get
                Return m_ChangedSinceLastSaved
            End Get
        End Property
        Friend WriteOnly Property _ChangedSinceLastSaved() As Boolean
            Set(ByVal value As Boolean)
                m_ChangedSinceLastSaved = value
            End Set
        End Property
#End Region

#Region "HTML"
        Private Shared ReadOnly Property StyleHTML() As String
            Get
                Return SpiceMaster.My.Resources.html_style
            End Get
        End Property
        Private Shared ReadOnly Property ScriptHTML() As String
            Get
                Return My.Resources.html_script
            End Get
        End Property
        Private Shared ReadOnly Property PrologueHTML() As String
            Get
                Return My.Resources.html_prologue
                'Static s As String = ""
                'If s = "" Then
                '    s = _
                '        "<h2>English</h2>" &
                '        vbCrLf &
                '        "<p>This guide is a <b>per food category</b> classification of spices and herbs. " &
                '        "It will help you find what you can combine with the food you want to cook.</p>" &
                '        vbCrLf &
                '        "<p><b>Comments</b> on a spice are in regard to the specific food category or " &
                '        "explain further the type of food. The '+' symbol in some comments means that you " &
                '        "can use that spice at the current food category, as well as in the subcategory that it is mentionoed.</p>" &
                '        vbCrLf &
                '        "<p>Many foods can fall in more than one categories, so check the other relevant food " &
                '        "categories when cooking. Also, at least until you are familiar with the specific spices, " &
                '        "when a plate has ingredients from different categories use those spices that are referenced " &
                '        "in both categories. Replacements are denoted with 'or' and '+'.</p>" &
                '        vbCrLf &
                '        "<p>This guide <b>does not</b> contain information and way of use of the spices, nor how to make the mixes " &
                '        "- consult a specialized book about it or keep to the spices you know about. The more familiar you are with " &
                '        "the spices and cooking in general, the more usefull this information will be.</p>" &
                '        vbCrLf &
                '        "<p>The assosiaction of spices with food categories was done as it was mentioned in the litereature, and it was not always defined precisely.</p>" &
                '        vbCrLf &
                '        "<p>Spices that can be combined with a food category, can usually be combined with it's subecategories. " &
                '        "Also, when spices are mentioned in a combination (using '+') you can use each one on itself most of the times.</p>" & vbCrLf &
                '        "<br>" & vbCrLf &
                '        "<p>Trust this guide without hesitations, and don't hesitate to add a spice or herb in your food, as if it where " &
                '        "extravagant or ""too much"" (only quantity can be too much!). Keep in mind that the food is coocked in such a way " &
                '        "in some place of the world.</p>" &
                '        vbCrLf &
                '        "<p>Promote this guide to your friends that like to cook and contact me for any comment, " &
                '        "ideas and suggestions, as well to get the <b>updated version</b> of the guide.</p>" & vbCrLf &
                '        "<br>" & vbCrLf &
                '        "<p align=""right"">George Kanellopoulos <a href=""mailto:g_kanellopoulos@hotmail.com"">g_kanellopoulos@hotmail.com</a></p>" & vbCrLf &
                '        "<p align=""right""><b>Electronic media and development:</b>Costas Basdekis <a href=""mailto:costasgr43@yahoo.com"">costasgr43@hotmail.com</a></p>" &
                '        vbCrLf &
                '        "<h2>Greek/Ελληνικά</h2>" &
                '        vbCrLf &
                '        "<p>Ο οδηγός αυτός είναι μία <b>κατά φαγητό</b> ταξινόμηση των μπαχαρικών και γενικά των μυρωδικών. " &
                '        "Θα σας βοηθήσει να βρείτε τι ταιριάζει με το φαγητό που πρόκειται να μαγειρέψετε.</p>" &
                '        vbCrLf &
                '        "<p>Τα <b>σχόλια</b> που ξεκινούν από κάποιο μυρωδικό αφορούν τη χρήση του στο συγκεκριμένο φαγητό ή, " &
                '        "συνηθέστερα, επεξηγούν περεταίρω το είδος του πιάτου. Το σύμβολο + σε μερικά σχόλια σημαίνει πως το " &
                '        "εν λόγω μπαχαρικό χρησιμοποιείται και γενικά στο πιάτο της κατηγορίας, αλλά και στο υποείδος που ορίζει το σχόλιο.</p>" &
                '        vbCrLf &
                '        "<p>Πολλά φαγητά εμπίπτουν σε πάνω από μία κατηγορία, οπότε ψάξτε και στις άλλες σχετικές κατηγορίες με " &
                '        "αυτό που θα μαγειρέψετε. Επίσης, τουλ. μέχρι να εξοικειωθείτε με τα μπαχαρικά, όταν το φαγητό περιέχει υλικά " &
                '        "από διάφορες κατηγορίες, να επιλέγετε τα μυρωδικά που αναφέρονται από κοινού στις κατηγορίες αυτές. " &
                '        "Αντικαταστάσεις δηλώνονται με «ή» και συνδυασμοί με «+».</p>" &
                '        vbCrLf &
                '        "<p>Ο οδηγός αυτός <b>δεν</b> περιέχει πληροφορίες για την ταυτότητα και τον τρόπο χρήσης του κάθε μυρωδικού, " &
                '        "ούτε για τον τρόπο παρασκευής των μιγμάτων. Σ’ αυτό θα σας βοηθήσει κάποιος τίτλος από την <b>κυρίως</b> βιβλιογραφία. " &
                '        "Αλλιώς αρκεστείτε στα μπαχαρικά που ήδη γνωρίζετε. Όσο πιο καλά γνωρίζετε τα μπαχαρικά (και τη μαγειρική γενικά), " &
                '        "τόσο πιο πολύ θα μπορείτε να αξιοποιείτε την πληροφορία που περιέχεται εδώ.</p>" &
                '        vbCrLf &
                '        "<p>Η τοποθέτηση των μυρωδικών στις κατηγορίες των φαγητών έγινε όπως ακριβώς αναφερόταν στη βιβλιογραφία και " &
                '        "μερικές φορές δεν διευκρινιζόταν πολύ καλά. Όσες φορές δεν υπήρχε η αντίστοιχη λέξη στα ελληνικά ή όταν υπήρχε " &
                '        "αλλά δεν έδινε την ακριβή σημασία, η μετάφραση δεν έγινε.</p>" &
                '        vbCrLf &
                '        "<p>Μυρωδικά που εμπίπτουν σε μια κατηγορία, συνήθως εμπίπτουν και σε υποκατηγορίες αυτής. " &
                '        "Επίσης, όταν σε κάποιο φαγητό τα μπαχαρικά δίνονται σε συνδυασμό, συνήθως ταιριάζει και το καθένα μόνο του.</p>" &
                '        vbCrLf &
                '        "<p>Χρησιμοποίησα το κατά τη γνώμη μου πιο δημοφιλές ή/και σωστότερο όνομα του κάθε μπαχαρικού, " &
                '        "αλλά πολύ πιθανόν κάποια να τα γνωρίζετε με άλλο όνομα, συμβουλευτείτε γι’ αυτό τη βιβλιογραφία ή τον μπαχαροπώλη.</p>" & vbCrLf &
                '        "<br>" & vbCrLf &
                '        "<p>Εμπιστευτείτε άφοβα τον οδηγό και μη διστάσετε να προσθέσετε κάποιο μυρωδικό στο φαγητό σας, " &
                '        "κρίνοντάς το εξεζητημένο ή υπερβολικό (υπερβολική μπορεί να είναι μόνο μια λάθος ποσότητα!). " &
                '        "Να έχετε υπ’ όψιν ότι σε κάποιο μέρος της Ελλάδας ή του κόσμου, το φαγητό αυτό τρώγεται έτσι.</p>" &
                '        vbCrLf &
                '        "<p>Προωθήστε τον οδηγό σε γνωστούς σας που ψάχνονται σε ό,τι αφορά τη μαγειρική κ΄ επικοινωνείτε " &
                '        "μαζί μου για σχόλια, προτάσεις και για να λαμβάνετε την <b>ενημερωμένη έκδοση</b> του οδηγού.</p>" & vbCrLf &
                '        "<br>" & vbCrLf &
                '        "<p align=""right"">Γιώργος Κανελλόπουλος <a href=""mailto:g_kanellopoulos@hotmail.com"">g_kanellopoulos@hotmail.com</a></p>" & vbCrLf &
                '        "<p align=""right""><b>Ηλεκτρονική μορφή & Προγραμματισμός:</b>Κώστας Μπασδέκης <a href=""mailto:costasgr43@yahoo.com"">costasgr43@hotmail.com</a></p>"
                'End If

                'Return s
            End Get
        End Property
        Private Shared ReadOnly Property SourcesHTML() As String
            Get
                Return My.Resources.html_sources
            End Get
        End Property
        Private Sub PrepareHTML()
            Dim s As String = ""
            Dim LinePrefix As String = ""

            If m_PreHTML = "" Then
                s = _
                "<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EL"">" & vbCrLf &
                "<html>" & vbCrLf &
                "<head>" & vbCrLf &
                "   <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">" & vbCrLf &
                "   <META HTTP-EQUIV=""Content-Type"" CONTENT=""text/html; charset=ISO-8859-7"">" & vbCrLf &
                "   <title>SpiceMaster Foods</title>" & vbCrLf &
                "   <style>" & vbCrLf &
                "   <!--" & vbCrLf &
                StyleHTML & vbCrLf &
                "   -->" & vbCrLf &
                "   </style>" & vbCrLf &
                "</head>" & vbCrLf &
                "<script language=""javascript"" type=""text/javascript"">" & vbCrLf &
                "<!--" & vbCrLf &
                ScriptHTML & vbCrLf &
                "-->" & vbCrLf &
                "</script>" & vbCrLf &
                "<body bgcolor=""white"">" & vbCrLf &
                "	<a name=""F""><h3><center>George Kanellopoulos<br>(Electronic media and development: Costas Basdekis)</center></h3></a>" & vbCrLf &
                "	<h1><center>Spices per food category</center></h1>" & vbCrLf &
                "<dl>" & vbCrLf &
                "<dt class=""l1"">" & vbCrLf &
                        "   <a href=""javascript:ToggleVisibility2('PROLOGUE')"">" & LinePrefix &
                        "   <span class=""icon-button"">" & LinePrefix &
                        "      <span class=""icon"" id=""PROLOGUEM"" style=""display: none;"">" & LinePrefix &
                        "       <div class=""icon-minus-circle""></div>" & LinePrefix &
                        "              <div class=""icon-minus-line""></div>" & LinePrefix &
                        "       </span>" & LinePrefix &
                        "       <span class=""icon"" id=""PROLOGUEP"">" & LinePrefix &
                        "           <div class=""icon-plus-circle""></div>" & LinePrefix &
                        "           <div class=""icon-plus-line-1""></div>" & LinePrefix &
                        "           <div class=""icon-plus-line-2""></div>" & LinePrefix &
                        "       </span>" & LinePrefix &
                        "   </span>" & LinePrefix &
                        "   </a>" & LinePrefix &
                        "   <span class=""name"">&nbsp;</span>" & LinePrefix &
                "<a name=""MYPROLOG"" >Prologue/Usage guide</a></dt>" & vbCrLf &
                "<dd id=""PROLOGUE_"" style=""display: none"">" & vbCrLf &
                PrologueHTML & vbCrLf &
                "</dd>" & vbCrLf &
                "<dt class=""l1"">" & vbCrLf &
                        "   <a href=""javascript:ToggleVisibility2('FOODS')"">" & LinePrefix &
                        "   <span class=""icon-button"">" & LinePrefix &
                        "      <span class=""icon"" id=""FOODSM"">" & LinePrefix &
                        "       <div class=""icon-minus-circle""></div>" & LinePrefix &
                        "              <div class=""icon-minus-line""></div>" & LinePrefix &
                        "       </span>" & LinePrefix &
                        "       <span class=""icon"" id=""FOODSP"" style=""display: none;"">" & LinePrefix &
                        "           <div class=""icon-plus-circle""></div>" & LinePrefix &
                        "           <div class=""icon-plus-line-1""></div>" & LinePrefix &
                        "           <div class=""icon-plus-line-2""></div>" & LinePrefix &
                        "       </span>" & LinePrefix &
                        "   </span>" & LinePrefix &
                        "   </a>" & LinePrefix &
                        "   <span class=""name"">&nbsp;</span>"

                m_PreHTML = s
            End If
            If m_PostHTML = "" Then
                s = _
                "	</dl>" & vbCrLf & _
                "	</dd>" & vbCrLf & _
                "<dt class=""l1"">" & vbCrLf & _
                        "   <a href=""javascript:ToggleVisibility2('SOURCES')"">" & LinePrefix &
                        "   <span class=""icon-button"">" & LinePrefix &
                        "      <span class=""icon"" id=""SOURCESM"" style=""display: none;"">" & LinePrefix &
                        "       <div class=""icon-minus-circle""></div>" & LinePrefix &
                        "              <div class=""icon-minus-line""></div>" & LinePrefix &
                        "       </span>" & LinePrefix &
                        "       <span class=""icon"" id=""SOURCESP"">" & LinePrefix &
                        "           <div class=""icon-plus-circle""></div>" & LinePrefix &
                        "           <div class=""icon-plus-line-1""></div>" & LinePrefix &
                        "           <div class=""icon-plus-line-2""></div>" & LinePrefix &
                        "       </span>" & LinePrefix &
                        "   </span>" & LinePrefix &
                        "   </a>" & LinePrefix &
                        "   <span class=""name"">&nbsp;</span>" & LinePrefix &
                "<a name=""MYSOURCES"">Sources/Bibliography</a></dt>" & vbCrLf & _
                "<dd id=""SOURCES_"" style=""display: none"">" & vbCrLf & _
                SourcesHTML & vbCrLf & _
                "</dd>" & vbCrLf & _
                "	</dl>" & vbCrLf & _
                "</body>" & vbCrLf & _
                "</html>"

                m_PostHTML = s
            End If
        End Sub
        Public ReadOnly Property InnerHTML()
            Get
                Dim s As String = ""

                s = _
                m_PreHTML & vbCrLf & _
                "<a name=""MYFOODS"">" & Foods.Count & " food categories (" & Foods.SubCount & " total categories & subcategories)</a></dt>" & vbCrLf & _
                "<dd id=""FOODS_"">" & vbCrLf & _
                "	<dl>" & vbCrLf & _
                Foods.InnerHTML & vbCrLf & _
                m_PostHTML

                Return s
            End Get
        End Property
        Public Sub RefreshHTML()
            RefreshFdList(m_Foods)
            RaiseEvent HTMLRefreshed()
        End Sub
        Private Sub RefreshFdList(ByVal FdList As FoodList)
            Dim i As Integer

            With FdList
                For i = 1 To .Count
                    With .Item(i)
                        .HTML.Refresh()
                        RefreshFdList(.Foods)
                    End With
                Next
            End With
        End Sub

        Public ReadOnly Property ExportHTML() As String
            Get
                Dim s As String = ""

                s = _
                m_PreHTML & vbCrLf & _
                "<a name=""MYFOODS"">" & Foods.Count & " food categories (" & Foods.SubCount & " total categories & subcategories)</a></dt>" & vbCrLf & _
                "<dd id=""FOODS_"">" & vbCrLf & _
                "	<dl>" & vbCrLf & _
                Foods.ExportHTML & vbCrLf & _
                m_PostHTML

                Return s
            End Get
        End Property
#End Region

#Region "Batch"
        Public Sub BatchChange()
            m_ChangedSinceLastSaved = True
            m_BatchChanges += 1
            If m_BatchCount <= 0 Then
                m_BatchCount = 1
                BatchEnd()
            End If
        End Sub
        Public Sub BatchStart()
            m_BatchCount += 1
            frmDocumentChanges.ElementChange("BatchStart: " & m_BatchCount)
        End Sub
        Public Sub BatchEnd()
            m_BatchCount -= 1
            frmDocumentChanges.ElementChange("BatchEnd: " & m_BatchCount)
            If m_BatchCount = 0 Then
                If m_BatchChanges > 0 Then
                    RaiseEvent HTMLRefreshed()
                End If
                m_BatchChanges = 0
                frmDocumentChanges.DocumentChange()
            ElseIf m_BatchCount < 0 Then
                Debug.Fail("Should not call more BatchEnd than BatchStart")
            End If
        End Sub
#End Region

#Region "Changes"
        Public Property Batching() As Boolean
            Get
                Return (m_BatchingCount > 0)
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    m_BatchingCount += 1
                Else
                    If m_BatchingCount <= 0 Then
                        Debug.Fail("Cannot de-batch any more")
                    End If
                    m_BatchingCount -= 1
                End If
            End Set
        End Property

        Public Sub ItemChanged()
            '
        End Sub
        Public Sub RepresentationChanged()

        End Sub
#End Region

#Region "Load"
        Public Function Load(ByVal FileName As String, Optional ByVal PreferedLoader As BaseDocLoader = Nothing, Optional LoadStatusNotifier As LoadStatus = Nothing, Optional OldDocument As Document = Nothing) As Boolean
            Dim xml As New Xml.XmlDocument
            Dim loader As BaseDocLoader = PreferedLoader

            'Load the XML file
            Try
                xml.Load(FileName)
            Catch ex As Exception
                Return False
            End Try

            If PreferedLoader Is Nothing Then
                'Find version:
                ''SpiceDocument, v1
                'loader = New SDLoader
                'loader.LoadStatusNotifier = LoadStatusNotifier
                'OldDocument = loader.TryLoad(Me, xml)
                'If OldDocument IsNot Nothing Then
                '    GoTo Success
                'End If
                'SpiceMasterDocument, v1
                loader = New SMDLoader
                loader.LoadStatusNotifier = LoadStatusNotifier
                OldDocument = loader.TryLoad(Me, xml)
                If OldDocument IsNot Nothing Then
                    GoTo Success
                End If
            Else
                'Supplied loader
                OldDocument = loader.TryLoad(Me, xml)
                If OldDocument IsNot Nothing Then
                    GoTo Success
                End If
            End If

            Return False
Success:
            m_MyShelf._Document = Me
            m_FilePath = FileName
            m_ChangedSinceLastSaved = False

            RefreshHTML()
            RaiseEvent Loaded()

            OldDocument.MoveFrom(New Document)

            Return True
        End Function

#Region "Loaders"
        MustInherit Class BaseDocLoader
            Inherits XMLLoader
            Public MustOverride Function TryLoad(ByVal Document As Document, ByVal XMLDocument As Xml.XmlDocument) As Document
            Public LoadStatusNotifier As LoadStatus
        End Class

        Class SDLoader
            'Inherits BaseDocLoader

            'Public Overrides Function TryLoad(ByVal Document As Document, ByVal XMLDocument As Xml.XmlDocument) As Document
            '    Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
            '    Dim DefList As New DefinitionList, SpcList As New SpiceList, FdList As New FoodList
            '    Dim s As String = "", c As Integer

            '    'Find element
            '    xel = XMLDocument("SpiceDocument") : If xel Is Nothing Then Return False

            '    'Check to have attrs
            '    If Not ElHasAttr(xel, "Version", s, False) Then Return False
            '    If Not (IsNumeric(s) AndAlso CInt(s) = CInt(CDbl(s))) Then Return False
            '    c = CInt(s) : If Not (c = 1) Then Return False

            '    'Load items
            '    If Not LoadDefList(DefList, xel) Then Return False
            '    If Not LoadSpcList(SpcList, xel) Then Return False
            '    xel2 = xel("Foods")
            '    If xel2 IsNot Nothing Then
            '        If Not LoadFdList(FdList, xel2, SpcList) Then Return False
            '    End If

            '    With Document
            '        .m_Definitions.MoveFrom(DefList)
            '        .m_Spices.MoveFrom(SpcList)
            '        .m_Foods.MoveFrom(FdList)
            '    End With

            '    Return True
            'End Function

            'Protected Function LoadDefList(ByVal List As DefinitionList, ByVal ParentNode As Xml.XmlElement) As Boolean
            '    Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
            '    Dim i As Integer, s As String = "", s2 As String = ""

            '    'Find the node
            '    xel = ParentNode("Definitions") : If xel Is Nothing Then Return False

            '    'Load the items
            '    List.BatchStart()
            '    With xel.ChildNodes
            '        For i = 1 To .Count
            '            xel2 = .Item(i - 1)
            '            If ElHasName(xel2, "Definition") Then
            '                If Not ElHasAttr(xel2, "Name", s, False) Then Return False
            '                If Not ElHasAttr(xel2, "FullText", s2, False) Then Return False
            '                If List.Add(s, s2, False) Is Nothing Then Return False
            '            End If
            '        Next
            '    End With
            '    List.BatchEnd()

            '    Return True
            'End Function
            'Protected Function LoadComList(ByVal List As CommentList, ByVal ParentNode As Xml.XmlElement) As Boolean
            '    Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
            '    Dim i As Integer, s As String = "", s2 As String = ""

            '    'Find the node
            '    xel = ParentNode("Comments") : If xel Is Nothing Then Return True

            '    'Load the items
            '    List.BatchStart()
            '    With xel.ChildNodes
            '        For i = 1 To .Count
            '            xel2 = .Item(i - 1)
            '            If ElHasName(xel2, "Comment") Then
            '                If Not ElHasAttr(xel2, "Text", s, False) Then Return False
            '                If Not ElHasAttr(xel2, "Scope", s2, False) Then s2 = ""
            '                If List.Add(s, s2, False) Is Nothing Then Return False
            '            End If
            '        Next
            '    End With
            '    List.BatchEnd()

            '    Return True
            'End Function
            'Protected Function LoadSpcList(ByVal List As SpiceList, ByVal ParentNode As Xml.XmlElement) As Boolean
            '    Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
            '    Dim i As Integer, j As Integer, s As String = "", c As Integer
            '    Dim cmb As Combination

            '    'Find the node
            '    xel = ParentNode("Ingredients") : If xel Is Nothing Then Return True

            '    'Check to have attrs
            '    If Not ElHasAttr(xel, "Count", s, False) Then Return False
            '    If Not (IsNumeric(s) AndAlso CInt(s) = CInt(CDbl(s))) Then Return False
            '    c = CInt(s) : If Not (c >= 0) Then Return False

            '    For i = 1 To c
            '        s = CStr(i)
            '        j = Len(CStr(c)) - Len(s)
            '        If j > 0 Then s = StrDup(j, "0") & s
            '        If List.Add(Chr(255) & s) Is Nothing Then Return False
            '    Next

            '    'Load the items
            '    List.BatchStart()
            '    With xel.ChildNodes
            '        For i = 1 To .Count
            '            xel2 = .Item(i - 1)
            '            If Not ElHasName(xel2, "Ingredient") Then Return False
            '            If Not ElHasAttr(xel2, "Name", s, False) Then Return False
            '            With List.Item(i)
            '                ._Name = s
            '                cmb = .DerivedCombinations(1)
            '                If Not LoadComList(cmb.Comments, xel2) Then Return False
            '                If Not LoadCmbRList(cmb.Combinations, xel2, List) Then Return False
            '            End With
            '        Next
            '    End With
            '    List.BatchEnd()

            '    Return True
            'End Function
            'Protected Function LoadCmbRList(ByVal List As CombinationReferenceList, ByVal ParentNode As Xml.XmlElement, ByVal SpiceList As SpiceList) As Boolean
            '    Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
            '    Dim i As Integer, s As String = "", c As Integer
            '    Dim spc As Spice, ncmb As Combination, cmbr As CombinationReference

            '    'Find the node
            '    xel = ParentNode("Combinations") : If xel Is Nothing Then Return True

            '    'Load the items
            '    List.BatchStart()
            '    With xel.ChildNodes
            '        For i = 1 To .Count
            '            xel2 = .Item(i - 1)
            '            If ElHasName(xel2, "Combination") Then
            '                If Not ElHasAttr(xel2, "ID", s, False) Then Return False
            '                'Check ID to be valid
            '                If Not (IsNumeric(s) AndAlso CInt(s) = CInt(CDbl(s))) Then Return False
            '                c = CInt(s) : If Not ((c > 0) AndAlso (c <= SpiceList.Count)) Then Return False
            '                spc = SpiceList.Item(c)

            '                ncmb = New Combination(spc)
            '                ncmb = spc.DerivedCombinations(ncmb)

            '                cmbr = List.Add(New CombinationReference(ncmb), False)
            '                If cmbr Is Nothing Then Return False
            '                If Not LoadComList(cmbr.Comments, xel2) Then Return False
            '            End If
            '        Next
            '    End With
            '    List.BatchEnd()


            '    Return True
            'End Function
            'Protected Function LoadFdList(ByVal List As FoodList, ByVal ParentNode As Xml.XmlElement, ByVal SpiceList As SpiceList) As Boolean
            '    Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
            '    Dim i As Integer, s As String = ""
            '    Dim fd As Food

            '    'Find the node
            '    xel = ParentNode("Categories") : If xel Is Nothing Then Return True

            '    'Load the items
            '    List.BatchStart()
            '    With xel.ChildNodes
            '        For i = 1 To .Count
            '            xel2 = .Item(i - 1)
            '            If ElHasName(xel2, "Category") Then
            '                If Not ElHasAttr(xel2, "Name", s, False) Then Return False
            '                fd = List.Add(s)
            '                If fd Is Nothing Then Return False
            '                If Not LoadComList(fd.Comments, xel2) Then Return False
            '                If Not LoadCmbRList(fd.Combinations, xel2, SpiceList) Then Return False
            '                If Not LoadFdList(fd.Foods, xel2, SpiceList) Then Return False
            '            End If
            '        Next
            '    End With
            '    List.BatchEnd()

            '    Return True
            'End Function
        End Class
        Class SMDLoader
            Inherits BaseDocLoader

            Public Overrides Function TryLoad(ByVal Document As Document, ByVal XMLDocument As Xml.XmlDocument) As Document
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim newdoc As New Document
                Dim sVersion As String = "", dVersion As Integer

                'Find element
                xel = XMLDocument("SpiceMasterDocument") : If xel Is Nothing Then Return Nothing

                'Check to have attrs
                If Not ElHasAttr(xel, "Version", sVersion, False) Then Return Nothing
                If Not (IsNumeric(sVersion) AndAlso CInt(sVersion) = CInt(CDbl(sVersion))) Then Return Nothing
                dVersion = CInt(sVersion) : If Not (dVersion = 1) Then Return Nothing

                If LoadStatusNotifier Is Nothing Then LoadStatusNotifier = New LoadStatus
                LoadStatusNotifier.TotalItemCount = GetTotalCount(xel)
                LoadStatusNotifier.Start()

                'Load items
                If Not LoadDefList(newdoc.m_Definitions, xel) Then Return Nothing
                If Not LoadSpcList(newdoc.m_Spices, xel, newdoc.m_Definitions) Then Return Nothing
                If Not LoadFoodList(newdoc.m_Foods, xel, newdoc.m_Spices) Then Return Nothing
                CombsBatchEnd(newdoc.m_Spices)
                xel2 = xel("MyShelf")
                If xel2 IsNot Nothing Then
                    If Not LoadCombRList(newdoc.m_MyShelf, xel2, newdoc.m_Spices) Then Return Nothing
                End If

                TryLoad = New Document
                TryLoad.MoveFrom(Document, False)
                Document.MoveFrom(newdoc, False)
            End Function

            Protected Function GetTotalCount(ParentNode As Xml.XmlElement) As Long
                Dim xel As Xml.XmlElement

                GetTotalCount = 0

                xel = ParentNode("Definitions")
                If xel IsNot Nothing Then GetTotalCount += xel.ChildNodes.Count

                xel = ParentNode("Spices")
                If xel IsNot Nothing Then GetTotalCount += xel.ChildNodes.Count

                xel = ParentNode("Foods")
                If xel IsNot Nothing Then GetTotalCount += xel.ChildNodes.Count
            End Function
            Protected Function LoadDefList(ByVal List As DefinitionList, ByVal ParentNode As Xml.XmlElement) As Boolean
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim i As Integer, sAbbr As String = "", sFull As String = "", eUID As Integer
                Dim el As Definition

                'Find the node
                xel = ParentNode("Definitions") : If xel Is Nothing Then Return True

                'Load the items
                List.BatchStart()
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        If Not LoadItem(List, xel2, "Definition", "Name", eUID, sAbbr) Then Return False
                        If Not ElHasAttr(xel2, "FullText", sFull, False) Then Return False
                        el = List.Add(sAbbr, sFull) : If el Is Nothing Then Return False
                        el.FileUID = eUID
                        LoadStatusNotifier.Item()
                    Next
                End With
                List.BatchEnd()

                Return True
            End Function
            Protected Function LoadSpcList(ByVal List As SpiceList, ByVal ParentNode As Xml.XmlElement, ByVal DefinitionList As DefinitionList) As Boolean
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim i As Integer, sName As String = "", eUID As Integer
                Dim el As Spice

                'Find the node
                xel = ParentNode("Spices") : If xel Is Nothing Then Return True

                'Load the spices and pre-load the combinations
                List.BatchStart()
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        If Not LoadItem(List, xel2, "Spice", "Name", eUID, sName) Then Return False
                        el = List.Add(sName) : If el Is Nothing Then Return False
                        el.FileUID = eUID
                        'Pre-load the combinations
                        If Not LoadCombListPre(el.DerivedCombinations, xel2, DefinitionList) Then Return False
                    Next
                End With

                CombsBatchStart(List)

                'Load the combinations' combinations
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        'Get UID
                        ElHasAttr(xel2, "UID", eUID, False) : el = List.ByFileUID(eUID)
                        'Load the combinations refs
                        If Not LoadCombListPost(el.DerivedCombinations, xel2, List) Then Return False
                        LoadStatusNotifier.Item()
                    Next
                End With
                List.BatchEnd()

                Return True
            End Function
            Protected Sub CombsBatchStart(List As SpiceList)
                Dim i As Integer, j As Integer

                'Mass batch start combinations' Foods and Combinations

                With List
                    For i = 1 To .Count
                        With .Item(i).DerivedCombinations
                            For j = 1 To .Count
                                With .Item(j)
                                    .Combinations.BatchStart()
                                    .Foods.BatchStart()
                                End With
                            Next
                        End With
                    Next
                End With
            End Sub
            Protected Sub CombsBatchEnd(List As SpiceList)
                Dim i As Integer, j As Integer

                'Mass batch end combinations' Foods and Combinations

                With List
                    For i = .Count To 1 Step -1
                        With .Item(i).DerivedCombinations
                            For j = .Count To 1 Step -1
                                With .Item(j)
                                    .Foods.BatchEnd()
                                    .Combinations.BatchEnd()
                                End With
                            Next
                        End With
                    Next
                End With
            End Sub
            Protected Function LoadCombListPre(ByVal List As CombinationList, ByVal ParentNode As Xml.XmlElement, ByVal DefinitionList As DefinitionList) As Boolean
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim i As Integer, eUID As Integer
                Dim el As Combination, spc As Spice = CType(List.Parent, Spice)

                'Find the node
                xel = ParentNode("Combinations") : If xel Is Nothing Then Return False

                'Load the items w/o the combinations' refcombinations
                List.BatchStart()
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        If Not LoadItem(List, xel2, "Combination", Nothing, eUID, Nothing) Then Return False
                        'Load definitions
                        el = New Combination(spc)
                        If Not LoadDefRList(el.Definitions, xel2, DefinitionList) Then Return False
                        If el.Definitions.Count = 0 Then
                            el = List.Item(1)
                        Else
                            el = List.Add(el) : If el Is Nothing Then Return False
                        End If
                        If Not LoadComList(el.Comments, xel2) Then Return False
                        el.FileUID = eUID
                    Next
                End With
                List.BatchEnd()

                Return True
            End Function
            Protected Function LoadDefRList(ByVal List As DefinitionReferenceList, ByVal ParentNode As Xml.XmlElement, ByVal DefinitionList As DefinitionList) As Boolean
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim i As Integer, s As String = "", c As Integer, eUID As Integer
                Dim def As Definition, el As DefinitionReference

                'Find the node
                xel = ParentNode("RefDefinitions") : If xel Is Nothing Then Return True

                'Load the items
                List.BatchStart()
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        If Not LoadItem(List, xel2, "RefDefinition", "RefUID", c, eUID) Then Return False
                        def = DefinitionList.ByFileUID(eUID) : If def Is Nothing Then Return False
                        'Add
                        el = List.Add(def) : If el Is Nothing Then Return False
                    Next
                End With
                List.BatchEnd()

                Return True
            End Function
            Protected Function LoadComList(ByVal List As CommentList, ByVal ParentNode As Xml.XmlElement) As Boolean
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim i As Integer, sText As String = "", sScope As String = ""
                Dim el As Comment

                'Find the node
                xel = ParentNode("Comments") : If xel Is Nothing Then Return True

                'Load the items
                List.BatchStart()
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        If Not LoadItem(List, xel2, "Comment", "Text", 0, sText) Then Return False
                        If Not ElHasAttr(xel2, "Scope", sScope, False) Then sScope = ""
                        el = List.Add(sText, sScope) : If el Is Nothing Then Return False
                    Next
                End With
                List.BatchEnd()

                Return True
            End Function
            Protected Function LoadCombListPost(ByVal List As CombinationList, ByVal ParentNode As Xml.XmlElement, ByVal SpiceList As SpiceList) As Boolean
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim i As Integer, sUID As String = "", eUID As Integer
                Dim el As Combination

                'Find the node
                xel = ParentNode("Combinations")

                'Load the item combinations' refcombinations
                List.BatchStart()
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        'Check attributes
                        ElHasAttr(xel2, "UID", sUID, False) : eUID = CInt(sUID) : el = List.ByFileUID(eUID)
                        If Not LoadCombRList(el.Combinations, xel2, SpiceList) Then Return False
                    Next
                End With
                List.BatchEnd()

                Return True
            End Function
            Protected Function LoadCombRList(ByVal List As CombinationReferenceList, ByVal ParentNode As Xml.XmlElement, ByVal SpiceList As SpiceList, Optional MinimumSpiceLoadUID As Integer = 0) As Boolean
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim i As Integer, c As Integer, sUID As Integer, cUID As Integer, eConWith As Integer, tConWith As CombinationReference.ConnectWithEnum
                Dim spc As Spice, cmb As Combination, el As CombinationReference

                'Find the node
                xel = ParentNode("RefCombinations") : If xel Is Nothing Then Return True

                'Load the items
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        If Not LoadItem(List, xel2, "RefCombination", "RefSpiceUID", c, sUID) Then Return False
                        If sUID > MinimumSpiceLoadUID Then
                            If Not ElHasAttr(xel2, "RefCombUID", cUID, False) Then Return False
                            If ElHasAttr(xel2, "ConnectWith", eConWith, False) Then
                                tConWith = eConWith
                                Select Case tConWith
                                    Case CombinationReference.ConnectWithEnum.And, CombinationReference.ConnectWithEnum.Or
                                    Case Else
                                        Return False
                                End Select
                            Else
                                tConWith = CombinationReference.ConnectWithEnum.None
                            End If
                            spc = SpiceList.ByFileUID(sUID) : If spc Is Nothing Then Return False
                            cmb = spc.DerivedCombinations.ByFileUID(cUID) : If cmb Is Nothing Then Return False
                            el = List.Add(cmb, False) : If el Is Nothing Then Return False
                            el.ConnectWith = tConWith
                            If Not LoadComList(el.Comments, xel2) Then Return False
                        End If
                    Next
                End With

                Return True
            End Function
            Protected Function LoadFoodList(ByVal List As FoodList, ByVal ParentNode As Xml.XmlElement, ByVal SpiceList As SpiceList) As Boolean
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement
                Dim i As Integer, eName As String = "", eUID As Integer
                Dim el As Food

                'Find the node
                xel = ParentNode("Foods") : If xel Is Nothing Then Return True

                'Add the items
                List.BatchStart()
                With xel.ChildNodes
                    For i = 1 To .Count
                        xel2 = .Item(i - 1)
                        If Not LoadItem(List, xel2, "Food", "Name", eUID, eName) Then Return False
                        el = List.Add(eName) : If el Is Nothing Then Return False
                        If Not LoadComList(el.Comments, xel2) Then Return False
                        el.Combinations.BatchStart()
                        If Not LoadCombRList(el.Combinations, xel2, SpiceList) Then Return False
                        el.Combinations.BatchEnd()
                        If Not LoadFoodList(el.Foods, xel2, SpiceList) Then Return False
                        If List.Parent Is Nothing Then LoadStatusNotifier.Item()
                    Next
                End With
                List.BatchEnd()

                Return True
            End Function

#Region "Element functions"
            Private Function LoadItem(ByVal List As ElementList, ByVal Node As Xml.XmlElement, ByVal TagName As String, ByVal PropertyName As String, ByRef FUID As Integer, ByRef PropertyValue As String) As Boolean
                Dim eValue As String = "", eUID As Integer

                'Check attributes
                If Not ElHasName(Node, TagName) Then Return False
                If Not ElHasAttr(Node, "UID", eUID, False) Then Return False
                ''UID must exist and be unique
                'If List.ByFileUID(c) IsNot Nothing Then Return False
                'Property
                If Not ((PropertyName Is Nothing) OrElse (PropertyName = "") OrElse (PropertyValue Is Nothing)) Then
                    If (Not ElHasAttr(Node, PropertyName, eValue, False)) Then Return False
                    PropertyValue = eValue
                End If

                FUID = eUID

                Return True
            End Function
#End Region
        End Class
#End Region
#End Region
#Region "Save"
        Public Function Save(ByVal FileName As String, ByVal Saver As BaseDocSaver) As Boolean
            Dim doc As New Xml.XmlDocument

            Saver.TrySave(Me, doc)

            Try
                doc.Save(FileName)
            Catch ex As Exception
                Return False
            End Try

            m_FilePath = FileName

            m_ChangedSinceLastSaved = False

            Return True
        End Function

#Region "Savers"
        MustInherit Class BaseDocSaver
            Inherits XMLSaver
            Public MustOverride Sub TrySave(ByVal Document As Document, ByVal XMLDocument As Xml.XmlDocument)
        End Class

        Class SMDSaver
            Inherits BaseDocSaver

            Public Overrides Sub TrySave(ByVal Document As Document, ByVal XMLDocument As System.Xml.XmlDocument)
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

                xel = CreateAttributedElement("SpiceMasterDocument", "Version", "1", Nothing, XMLDocument)
                With Document
                    SaveDefList(.m_Definitions, xel, XMLDocument)
                    SaveSpcList(.m_Spices, xel, XMLDocument)
                    SaveFdList(.m_Foods, xel, XMLDocument)
                    If .m_MyShelf.Count > 0 Then
                        xel2 = CreateElement("MyShelf", xel, XMLDocument)
                        SaveCombRList(.m_MyShelf, xel2, XMLDocument)
                    End If
                End With
            End Sub

            Protected Sub SaveDefList(ByVal List As DefinitionList, ByVal ParentNode As Xml.XmlNode, ByVal XMLDocument As System.Xml.XmlDocument)
                Dim i As Integer
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

                With List
                    If .Count = 0 Then
                        Return
                    End If

                    xel = CreateElement("Definitions", ParentNode, XMLDocument)

                    For i = 1 To .Count
                        With .Item(i)
                            .FileUID = i
                            xel2 = CreateAttributedElement("Definition", "UID", .FileUID, xel, XMLDocument)
                            AddAttribute("Name", .Name, xel2, XMLDocument)
                            AddAttribute("FullText", .FullText, xel2, XMLDocument)
                        End With
                    Next
                End With
            End Sub
            Protected Sub SaveSpcList(ByVal List As SpiceList, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument)
                Dim i As Integer, j As Integer
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

                With List
                    If .Count = 0 Then
                        Return
                    End If

                    For i = 1 To .Count
                        With .Item(i)
                            .FileUID = i
                            With .DerivedCombinations
                                For j = 1 To .Count
                                    With .Item(j)
                                        .FileUID = j
                                    End With
                                Next
                            End With
                        End With
                    Next

                    xel = CreateElement("Spices", ParentNode, XMLDocument)

                    For i = 1 To .Count
                        With .Item(i)
                            .FileUID = i
                            xel2 = CreateAttributedElement("Spice", "UID", .FileUID, xel, XMLDocument)
                            AddAttribute("Name", .Name, xel2, XMLDocument)
                            'SaveComList(.Comments, xel2, XMLDocument)
                            SaveCombList(.DerivedCombinations, xel2, XMLDocument)
                        End With
                    Next
                End With
            End Sub
            Protected Sub SaveComList(ByVal List As CommentList, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument)
                Dim i As Integer
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

                With List
                    If .Count = 0 Then
                        Return
                    End If

                    xel = CreateElement("Comments", ParentNode, XMLDocument)

                    For i = 1 To .Count
                        With .Item(i)
                            .FileUID = i
                            xel2 = CreateAttributedElement("Comment", "UID", .FileUID, xel, XMLDocument)
                            AddAttribute("Text", .Text, xel2, XMLDocument)
                            If (.Scope <> "") AndAlso (StrComp(.Scope, .Text, CompareMethod.Text) <> 0) Then
                                AddAttribute("Scope", .Scope, xel2, XMLDocument)
                            End If
                        End With
                    Next
                End With
            End Sub
            Protected Sub SaveCombList(ByVal List As CombinationList, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument)
                Dim i As Integer
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

                xel = CreateElement("Combinations", ParentNode, XMLDocument)

                With List
                    For i = 1 To .Count
                        With .Item(i)
                            xel2 = CreateAttributedElement("Combination", "UID", .FileUID, xel, XMLDocument)
                            SaveDefRList(.Definitions, xel2, XMLDocument)
                            SaveComList(.Comments, xel2, XMLDocument)
                            SaveCombRList(.Combinations, xel2, XMLDocument)
                        End With
                    Next
                End With
            End Sub
            Protected Sub SaveDefRList(ByVal List As DefinitionReferenceList, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument)
                Dim i As Integer
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

                With List
                    If .Count = 0 Then
                        Return
                    End If

                    xel = CreateElement("RefDefinitions", ParentNode, XMLDocument)

                    For i = 1 To .Count
                        With .Item(i)
                            .FileUID = i
                            xel2 = CreateAttributedElement("RefDefinition", "UID", .FileUID, xel, XMLDocument)
                            AddAttribute("RefUID", .Item.FileUID, xel2, XMLDocument)
                            SaveComList(.Comments, xel2, XMLDocument)
                        End With
                    Next
                End With
            End Sub
            Protected Sub SaveCombRList(ByVal List As CombinationReferenceList, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument)
                Dim i As Integer
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

                With List
                    If .Count = 0 Then
                        Return
                    End If

                    xel = CreateElement("RefCombinations", ParentNode, XMLDocument)

                    For i = 1 To .Count
                        With .Item(i)
                            .FileUID = i
                            xel2 = CreateAttributedElement("RefCombination", "UID", .FileUID, xel, XMLDocument)
                            With .Item
                                AddAttribute("RefSpiceUID", .Spice.FileUID, xel2, XMLDocument)
                                AddAttribute("RefCombUID", .FileUID, xel2, XMLDocument)
                            End With
                            If (.ConnectWith <> CombinationReference.ConnectWithEnum.None) AndAlso (i > 1) Then
                                AddAttribute("ConnectWith", CStr(.ConnectWith), xel2, XMLDocument)
                            End If
                            SaveComList(.Comments, xel2, XMLDocument)
                        End With
                    Next
                End With
            End Sub
            Protected Sub SaveFdList(ByVal List As FoodList, ByVal ParentNode As Xml.XmlElement, ByVal XMLDocument As Xml.XmlDocument)
                Dim i As Integer
                Dim xel As Xml.XmlElement, xel2 As Xml.XmlElement

                With List
                    If .Count = 0 Then
                        Return
                    End If

                    xel = CreateElement("Foods", ParentNode, XMLDocument)

                    For i = 1 To .Count
                        With .Item(i)
                            .FileUID = i
                            xel2 = CreateAttributedElement("Food", "UID", .FileUID, xel, XMLDocument)
                            AddAttribute("Name", .Name, xel2, XMLDocument)
                            SaveComList(.Comments, xel2, XMLDocument)
                            SaveCombRList(.Combinations, xel2, XMLDocument)
                            SaveFdList(.Foods, xel2, XMLDocument)
                        End With
                    Next
                End With
            End Sub
        End Class
#End Region
#End Region
    End Class
End Namespace