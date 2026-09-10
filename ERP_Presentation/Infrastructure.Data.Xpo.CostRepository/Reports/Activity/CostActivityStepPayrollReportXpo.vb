#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Cost.CostActivityStepPayroll")>
Public Class CostActivityStepPayrollReportXpo
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
    <Association("Cost_CostActivityStepPayroll_References_Cost_CostActivityStep")>
    Public Property CostActivityStepId() As CostActivityStepReportXpo
        Get
            Return fCostActivityStepId
        End Get
        Set(ByVal value As CostActivityStepReportXpo)
            SetPropertyValue(Of CostActivityStepReportXpo)("CostActivityStepId", fCostActivityStepId, value)
        End Set
    End Property

    Dim fPayrollPositionId As PayrollPositionReportXpo
    <Association("Cost_CostActivityStepPayroll_References_Payroll_Position")>
    Public Property PayrollPositionId() As PayrollPositionReportXpo
        Get
            Return fPayrollPositionId
        End Get
        Set(ByVal value As PayrollPositionReportXpo)
            SetPropertyValue(Of PayrollPositionReportXpo)("PayrollPositionId", fPayrollPositionId, value)
        End Set
    End Property

    Dim fHours As Decimal
    Public Property Hours() As Decimal
        Get
            Return fHours
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Hours", fHours, value)
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