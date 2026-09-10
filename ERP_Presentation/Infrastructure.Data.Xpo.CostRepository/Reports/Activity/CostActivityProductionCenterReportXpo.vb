#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Cost.CostActivityProductionCenter")>
Public Class CostActivityProductionCenterReportXpo
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
    <Association("Cost_CostActivityProductionCenter_References_Cost_CostActivity")>
    Public Property CostActivityId() As CostActivityReportXpo
        Get
            Return fCostActivityId
        End Get
        Set(ByVal value As CostActivityReportXpo)
            SetPropertyValue(Of CostActivityReportXpo)("CostActivityId", fCostActivityId, value)
        End Set
    End Property
	
	Dim fCostProductionCenterId As CostProductionCenterReportXpo
    <Association("Cost_CostActivityProductionCenter_References_Cost_CostProductionCenter")>
    Public Property CostProductionCenterId() As CostProductionCenterReportXpo
        Get
            Return fCostProductionCenterId
        End Get
        Set(ByVal value As CostProductionCenterReportXpo)
            SetPropertyValue(Of CostProductionCenterReportXpo)("CostProductionCenterId", fCostProductionCenterId, value)
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