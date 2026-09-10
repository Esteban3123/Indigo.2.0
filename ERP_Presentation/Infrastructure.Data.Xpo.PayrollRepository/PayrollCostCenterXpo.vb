Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.CostCenter")> _
Public Class PayrollCostCenterXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Indexed(Name:="IX_CostCenter", Unique:=True)> _
    <Size(2)> _
    <Persistent("Code")> _
    Public Property Codigo() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    <Size(200)> _
    <Persistent("Name")> _
    Public Property Descripcion() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    <Association("PayrollFunctionalUnitReferencesPayrollCostCenter", GetType(PayrollFunctionalUnit))> _
    Public ReadOnly Property PayrollFunctionalUnit() As XPCollection(Of PayrollFunctionalUnit)
        Get
            Return GetCollection(Of PayrollFunctionalUnit)("PayrollFunctionalUnit")
        End Get
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    <Size(50)> _
    <PersistentAlias("concat(concat(Codigo,' - '),Descripcion)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Navigators"

    <Association("Payroll_Liquidation_References_Payroll_CostCenter", GetType(PayrollLiquidationXpo))> _
    Public ReadOnly Property PayrollLiquidations() As XPCollection(Of PayrollLiquidationXpo)
        Get
            Return GetCollection(Of PayrollLiquidationXpo)("PayrollLiquidations")
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
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
