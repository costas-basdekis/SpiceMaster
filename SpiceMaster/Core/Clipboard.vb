Namespace Core
    Class Clipboard

        Enum ItemType
            Food
            Foods
            Combination
            Combinations
        End Enum
        Enum Operation
            None
            Copy
            Cut
        End Enum

#Region "Events"
        Public Event ItemChanged(ByVal Item As ItemType, ByVal InnerEvent As Object)

#Region "Food"
        Private Sub Food_Changed(ByVal sender As Object, ByVal e As Element.EventArgs) Handles m_Food.Changed
            RaiseEvent ItemChanged(ItemType.Food, e)
        End Sub
        Private Sub Food_Removed(ByVal sender As Object, ByVal e As Element.EventArgs) Handles m_Food.Removed
            m_Food = Nothing
            Food_Changed(sender, e)
        End Sub
#End Region
#Region "Foods"
        Private Sub Foods_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_FoodRList.ItemAdded, m_FoodRList.ItemChanged, m_FoodRList.ItemRemoved, m_FoodRList.BatchEnded
            RaiseEvent ItemChanged(ItemType.Foods, ee)
        End Sub
#End Region
#Region "Comb"
        Private Sub Comb_Changed(ByVal sender As Object, ByVal e As Element.EventArgs) Handles m_Comb.Changed
            RaiseEvent ItemChanged(ItemType.Food, e)
        End Sub
        Private Sub Comb_Removed(ByVal sender As Object, ByVal e As Element.EventArgs) Handles m_Comb.Removed
            m_Comb = Nothing
            Food_Changed(sender, e)
        End Sub
#End Region
#Region "Combs"
        Private Sub Combs_Changed(ByVal sender As Object, ByVal ee As ElementList.EventArgs) Handles m_CombRList.ItemAdded, m_CombRList.ItemChanged, m_CombRList.ItemRemoved, m_CombRList.BatchEnded
            RaiseEvent ItemChanged(ItemType.Combinations, ee)
        End Sub
#End Region
#End Region

#Region "Variables"
        Public FoodOp As Operation = Operation.None, FoodsOp As Operation = Operation.None, CombOp As Operation = Operation.None, CombsOp As Operation = Operation.None

        Private WithEvents m_Food As Food = Nothing, m_FoodRList As New FoodReferenceList, m_Comb As Combination = Nothing, m_CombRList As New CombinationReferenceList
        Private m_FoodsSource As FoodReferenceList = Nothing, m_CombsSource As CombinationReferenceList = Nothing
#End Region

#Region "Properties"
        Public Property Food() As Food
            Get
                Return m_Food
            End Get
            Set(value As Food)
                m_Food = value
                RaiseEvent ItemChanged(ItemType.Food, Nothing)
            End Set
        End Property
        Public ReadOnly Property Foods() As FoodReferenceList
            Get
                Return m_FoodRList
            End Get
        End Property
        Public Property Comb() As Combination
            Get
                Return m_Comb
            End Get
            Set(value As Combination)
                m_Comb = value
                RaiseEvent ItemChanged(ItemType.Combination, Nothing)
            End Set
        End Property
        Public ReadOnly Property Combs() As CombinationReferenceList
            Get
                Return m_CombRList
            End Get
        End Property

        Public Property FoodsSource() As FoodReferenceList
            Get
                Return m_FoodsSource
            End Get
            Set(ByVal value As FoodReferenceList)
                m_FoodsSource = value
            End Set
        End Property
        Public Property CombsSource() As CombinationReferenceList
            Get
                Return m_CombsSource
            End Get
            Set(ByVal value As CombinationReferenceList)
                m_CombsSource = value
            End Set
        End Property
#End Region

        Public Sub Clear()
            m_Food = Nothing : FoodOp = Operation.None
            m_FoodRList.Clear() : FoodsOp = Operation.None
            m_Comb = Nothing : CombOp = Operation.None
            m_CombRList.Clear() : CombsOp = Operation.None
        End Sub
    End Class
End Namespace