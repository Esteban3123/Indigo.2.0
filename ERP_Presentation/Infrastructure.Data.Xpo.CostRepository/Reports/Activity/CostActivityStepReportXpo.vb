#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Cost.CostActivityStep")>
Public Class CostActivityStepReportXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
	
	Dim fCostActivityId As CostActivityReportXpo
    <Association("Cost_CostActivityStep_References_Cost_CostActivity")>
    Public Property CostActivityId() As CostActivityReportXpo
        Get
            Return fCostActivityId
        End Get
        Set(ByVal value As CostActivityReportXpo)
            SetPropertyValue(Of CostActivityReportXpo)("CostActivityId", fCostActivityId, value)
        End Set
    End Property

    Dim fOrder As Integer
    Public Property Order() As Integer
        Get
            Return fOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Order", fOrder, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("concat(Order,' - ',Description)")>
    Public ReadOnly Property OrderDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("OrderDescription"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("Cost_CostActivityStepFixedAsset_References_Cost_CostActivityStep", GetType(CostActivityStepFixedAssetReportXpo))>
    Public ReadOnly Property FixedAssetItems() As XPCollection(Of CostActivityStepFixedAssetReportXpo)
        Get
            Return GetCollection(Of CostActivityStepFixedAssetReportXpo)("FixedAssetItems")
        End Get
    End Property

    <Association("Cost_CostActivityStepPayroll_References_Cost_CostActivityStep", GetType(CostActivityStepPayrollReportXpo))>
    Public ReadOnly Property PayrollPositions() As XPCollection(Of CostActivityStepPayrollReportXpo)
        Get
            Return GetCollection(Of CostActivityStepPayrollReportXpo)("PayrollPositions")
        End Get
    End Property
	
	<Association("Cost_CostActivityStepInventory_References_Cost_CostActivityStep", GetType(CostActivityStepInventoryReportXpo))>
    Public ReadOnly Property InventoryGroups() As XPCollection(Of CostActivityStepInventoryReportXpo)
        Get
            Return GetCollection(Of CostActivityStepInventoryReportXpo)("InventoryGroups")
        End Get
    End Property
	
	<Association("Cost_CostActivityStepAddictionalCost_References_Cost_CostActivityStep", GetType(CostActivityStepAddictionalCostReportXpo))>
    Public ReadOnly Property AddictionalCosts() As XPCollection(Of CostActivityStepAddictionalCostReportXpo)
        Get
            Return GetCollection(Of CostActivityStepAddictionalCostReportXpo)("AddictionalCosts")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

#End Region

End Class