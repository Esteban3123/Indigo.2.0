#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Cost.CostActivityStepAddictionalCost")>
Public Class CostActivityStepAddictionalCostReportXpo
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
    <Association("Cost_CostActivityStepAddictionalCost_References_Cost_CostActivityStep")>
    Public Property CostActivityStepId() As CostActivityStepReportXpo
        Get
            Return fCostActivityStepId
        End Get
        Set(ByVal value As CostActivityStepReportXpo)
            SetPropertyValue(Of CostActivityStepReportXpo)("CostActivityStepId", fCostActivityStepId, value)
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

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
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