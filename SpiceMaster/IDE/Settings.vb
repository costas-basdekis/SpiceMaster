Imports SpiceMaster.Core

Namespace My
    Namespace SpiceMaster
        Namespace Settings
            Namespace AutoLoadFile
                Enum Mode
                    None
                    LastLoaded
                    Specific
                End Enum
            End Namespace
            Namespace FormOptions
                Class WindowPos

#Region "Variables"
                    Public XMLDocument As Xml.XmlDocument, FormName As String, Form As Form
                    Friend ParentNode As Xml.XmlElement
                    Public WindowState As Integer
                    Public X As Integer, Y As Integer
                    Public Width As Integer, Height As Integer
                    Public Splits() As Integer, Headers() As Integer, ListsViews() As Integer
#End Region

#Region "IO"
                    Public Sub Save()
                        Dim xs As New XMLSaver
                        Dim xd As Xml.XmlDocument, xel As Xml.XmlElement, xel2 As Xml.XmlElement

                        If FormName <> "" Then
                            xel = XMLDocument("Forms") : If xel Is Nothing Then XMLDocument.RemoveAll() : xel = XMLDocument.AppendChild(XMLDocument.CreateElement("Forms"))
                            xel2 = xel(FormName) : If xel2 Is Nothing Then xel2 = xel.AppendChild(XMLDocument.CreateElement(FormName))
                            ParentNode = xel2
                        End If
                        xd = ParentNode.OwnerDocument

                        If Form IsNot Nothing Then SetWindow(Form)
                        With xs
                            xel = ParentNode("WP") : If xel Is Nothing Then xel = .CreateElement("WP", ParentNode, xd)
                            SetAtt(xel, "WS", "V", WindowState)
                            SetAtt(xel, "X", "V", X) : SetAtt(xel, "Y", "V", Y)
                            SetAtt(xel, "W", "V", Width) : SetAtt(xel, "H", "V", Height)
                            For i As Integer = LBound(Splits) To UBound(Splits)
                                SetAtt(xel, "S" & CStr(i), "V", Splits(i))
                            Next
                            For i As Integer = LBound(Headers) To UBound(Headers)
                                SetAtt(xel, "H" & CStr(i), "V", Headers(i))
                            Next
                            For i As Integer = LBound(ListsViews) To UBound(ListsViews)
                                SetAtt(xel, "L" & CStr(i), "V", ListsViews(i))
                            Next
                        End With
                    End Sub
                    Sub SetAtt(ByVal ParentNode As Xml.XmlElement, ByVal Name As String, ByVal AttributeName As String, ByVal AttributeValue As Integer)
                        Dim xel As Xml.XmlElement, xatt As Xml.XmlAttribute

                        xel = ParentNode(Name) : If xel Is Nothing Then xel = ParentNode.AppendChild(ParentNode.OwnerDocument.CreateElement(Name))
                        xatt = xel.Attributes(AttributeName) : If xatt Is Nothing Then xatt = xel.Attributes.Append(xel.OwnerDocument.CreateAttribute(AttributeName))
                        xatt.Value = AttributeValue
                    End Sub

                    Public Sub Load(ByVal DefWS As Integer, ByVal DefaultX As Integer, ByVal DefaultY As Integer, ByVal DefaultWidth As Integer, ByVal DefaultHeight As Integer, Optional ByVal DefSplits() As Integer = Nothing, Optional ByVal DefHeaders() As Integer = Nothing, Optional ByVal DefListsViews() As Integer = Nothing)
                        Dim s As String = ""
                        Dim xl As New XMLLoader
                        Dim xel As Xml.XmlElement

                        If FormName <> "" Then
                            xel = XMLDocument("Forms")
                            If xel IsNot Nothing Then xel = xel(FormName)
                            ParentNode = xel
                        End If

                        Dim SpC As Integer = -1
                        If DefSplits IsNot Nothing Then SpC = UBound(DefSplits) - LBound(DefSplits)
                        ReDim Splits(0 To SpC)

                        Dim SpH As Integer = -1
                        If DefHeaders IsNot Nothing Then SpH = UBound(DefHeaders) - LBound(DefHeaders)
                        ReDim Headers(0 To SpH)

                        Dim SpL As Integer = -1
                        If DefListsViews IsNot Nothing Then SpL = UBound(DefListsViews) - LBound(DefListsViews)
                        ReDim ListsViews(0 To SpL)

                        With xl
                            If (ParentNode Is Nothing) OrElse (ParentNode("WP") Is Nothing) Then
                                WindowState = DefWS
                                X = DefaultX : Y = DefaultY 
                                Width = DefaultWidth : Height = DefaultHeight
                                For i As Integer = LBound(Splits) To UBound(Splits)
                                    Splits(i) = DefSplits(i)
                                Next
                                For i As Integer = LBound(Headers) To UBound(Headers)
                                    Headers(i) = DefHeaders(i)
                                Next
                                For i As Integer = LBound(ListsViews) To UBound(ListsViews)
                                    ListsViews(i) = DefListsViews(i)
                                Next
                            Else
                                xel = ParentNode("WP")
                                GetAtt(xl, xel, "WS", WindowState, DefWS)
                                GetAtt(xl, xel, "X", X, DefaultX) : GetAtt(xl, xel, "Y", Y, DefaultY)
                                GetAtt(xl, xel, "W", Width, DefaultWidth) : GetAtt(xl, xel, "H", Height, DefaultHeight)
                                If (X = 0) And (Y = 0) And (Width = 0) And (Height = 0) Then
                                    X = DefaultX : Y = DefaultY
                                    Width = DefaultWidth : Height = DefaultHeight
                                End If
                                For i As Integer = LBound(Splits) To UBound(Splits)
                                    GetAtt(xl, xel, "S" & CStr(i), Splits(i), DefSplits(i))
                                Next
                                For i As Integer = LBound(Headers) To UBound(Headers)
                                    GetAtt(xl, xel, "H" & CStr(i), Headers(i), DefHeaders(i))
                                Next
                                For i As Integer = LBound(ListsViews) To UBound(ListsViews)
                                    GetAtt(xl, xel, "L" & CStr(i), ListsViews(i), DefListsViews(i))
                                Next
                            End If
                        End With

                        If Form IsNot Nothing Then GetWindow(Form)
                    End Sub
                    Sub GetAtt(ByVal XL As XMLLoader, ByVal ParentNode As Xml.XmlElement, ByVal Name As String, ByRef Value As Integer, ByVal DefValue As Integer)
                        Dim xel As Xml.XmlElement
                        xel = ParentNode(Name) : If (xel Is Nothing) OrElse (Not XL.ElHasAttr(xel, "V", Value, False)) Then Value = DefValue
                    End Sub
#End Region

#Region "Get info"
                    Public Sub GetWindow(ByVal Form As Form)
                        With Form
                            .Location = New Point(X, Y)
                            .Size = New Size(Width, Height)
                            .WindowState = WindowState
                        End With
                    End Sub
                    Public Sub GetSplitter(ByVal SC As SplitContainer, ByVal Index As Integer)
                        SC.SplitterDistance = IIf(SC.Orientation = Orientation.Vertical, SC.ClientSize.Width, SC.ClientSize.Height) * Splits(Index) / 100
                    End Sub
                    Public Sub GetList(ByRef List As ListView, ByVal HeadersCount As Integer, ByVal FirstHeader As Integer, Optional ByVal ViewIndex As Integer = -1)
                        Dim bEnabled As Boolean, iView As View

                        With List
                            bEnabled = .Enabled
                            iView = .View
                            If ViewIndex >= 0 Then iView = ListsViews(ViewIndex)

                            .Enabled = True
                            .View = View.Details
                            For i As Integer = 0 To HeadersCount - 1
                                If .Columns.Count > i Then .Columns(i).Width = Headers(FirstHeader + i)
                            Next
                            .View = iView
                            .Enabled = bEnabled
                        End With
                    End Sub
#End Region
#Region "Set Info"
                    Public Sub SetWindow(ByVal Form As Form)
                        With Form
                            If .WindowState = FormWindowState.Normal Then
                                With .Location
                                    X = .X : Y = .Y
                                End With
                                With .Size
                                    Width = .Width : Height = .Height
                                End With
                            End If
                            WindowState = .WindowState
                        End With
                    End Sub
                    Public Sub SetSplitter(ByVal SC As SplitContainer, ByVal Index As Integer)
                        Splits(Index) = SC.SplitterDistance * 100 / IIf(SC.Orientation = Orientation.Vertical, SC.ClientSize.Width, SC.Height)
                    End Sub
                    Public Sub SetList(ByVal List As ListView, ByVal HeadersCount As Integer, ByVal FirstHeader As Integer, Optional ByVal ViewIndex As Integer = -1)
                        Dim bEnabled As Boolean, iView As View

                        With List
                            bEnabled = .Enabled
                            iView = .View

                            .Enabled = True
                            .View = View.Details
                            For i As Integer = 0 To HeadersCount - 1
                                If .Columns.Count > i Then Headers(FirstHeader + i) = .Columns(i).Width
                            Next
                            .View = iView
                            .Enabled = bEnabled

                            If ViewIndex >= 0 Then ListsViews(ViewIndex) = iView
                        End With
                    End Sub
#End Region
                End Class
            End Namespace
        End Namespace
    End Namespace
    'This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.
    Partial Friend NotInheritable Class MySettings
    End Class
End Namespace