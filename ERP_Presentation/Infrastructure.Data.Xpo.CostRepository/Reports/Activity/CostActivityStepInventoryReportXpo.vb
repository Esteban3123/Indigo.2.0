#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Cost.CostActivityStepInventory")>
Public Class CostActivityStepInventoryReportXpo
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
	
	Dim fCostActivityStepId As CostActivityStepReportXpo
    <Association("Cost_CostActivityStepInventory_References_Cost_CostActivityStep")>
    Public Property CostActivityStepId() As CostActivityStepReportXpo
        Get
            Return fCostActivityStepId
        End Get
        Set(ByVal value As CostActivityStepReportXpo)
            SetPropertyValue(Of CostActivityStepReportXpo)("CostActivityStepId", fCostActivityStepId, value)
        End Set
    End Property

    Dim fCostInventoryGroupId As CostInventoryGroupReportXpo
    <Association("Cost_CostActivityStepInventory_References_Inventory_CostInventoryGroup")>
    Public Property CostInventoryGroupId() As CostInventoryGroupReportXpo
        Get
            Return fCostInventoryGroupId
        End Get
        Set(ByVal value As CostInventoryGroupReportXpo)
            SetPropertyValue(Of CostInventoryGroupReportXpo)("CostInventoryGroupId", fCostInventoryGroupId, value)
        End Set
    End Property

    Dim fQuantity As Decimal
    Public Property Quantity() As Decimal
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Quantity", fQuantity, value)
        End Set
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