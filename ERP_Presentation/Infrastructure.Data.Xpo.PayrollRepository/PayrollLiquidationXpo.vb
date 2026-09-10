#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

#End Region

<Persistent("Payroll.Liquidation")>
Public Class PayrollLiquidationXpo
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

    Dim fGroupId As PayrollGroupXpo
    <Association("Payroll_Liquidation_References_Payroll_Group")>
    Public Property GroupId() As PayrollGroupXpo
        Get
            Return fGroupId
        End Get
        Set(ByVal value As PayrollGroupXpo)
            SetPropertyValue(Of PayrollGroupXpo)("GroupId", fGroupId, value)
        End Set
    End Property

    Dim fEmployeeId As PayrollEmployeeXpo
    <Association("Payroll_Liquidation_References_Payroll_Employee")>
    Public Property EmployeeId() As PayrollEmployeeXpo
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As PayrollEmployeeXpo)
            SetPropertyValue(Of PayrollEmployeeXpo)("EmployeeId", fEmployeeId, value)
        End Set
    End Property

    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("Payroll_Liquidation_References_Payroll_CostCenter")>
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fContractId As PayrollContractXpo
    <Association("Payroll_Liquidation_References_Payroll_Contract")>
    Public Property ContractId() As PayrollContractXpo
        Get
            Return fContractId
        End Get
        Set(ByVal value As PayrollContractXpo)
            SetPropertyValue(Of PayrollContractXpo)("ContractId", fContractId, value)
        End Set
    End Property

    Dim fRegisterStatus As Char
    Public Property RegisterStatus() As Char
        Get
            Return fRegisterStatus
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("RegisterStatus", fRegisterStatus, value)
        End Set
    End Property

    <Association("Payroll_BankFileDetail_References_Payroll_Liquidation", GetType(PayrollBankFileDetailXpo))>
    Public ReadOnly Property PayrollBankFiles() As XPCollection(Of PayrollBankFileDetailXpo)
        Get
            Return GetCollection(Of PayrollBankFileDetailXpo)("PayrollBankFiles")
        End Get
    End Property

    Dim fPayrollDateLiquidated As DateTime
    Public Property PayrollDateLiquidated() As DateTime
        Get
            Return fPayrollDateLiquidated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PayrollDateLiquidated", fPayrollDateLiquidated, value)
        End Set
    End Property

    Dim fBasicSalary As Decimal
    Public Property BasicSalary() As Decimal
        Get
            Return fBasicSalary
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BasicSalary", fBasicSalary, value)
        End Set
    End Property

    Dim fTotalAccrued As Decimal
    Public Property TotalAccrued() As Decimal
        Get
            Return fTotalAccrued
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAccrued", fTotalAccrued, value)
        End Set
    End Property

    Dim fTotalDeducted As Decimal
    Public Property TotalDeducted() As Decimal
        Get
            Return fTotalDeducted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDeducted", fTotalDeducted, value)
        End Set
    End Property

    Dim fTotalPaid As Decimal
    Public Property TotalPaid() As Decimal
        Get
            Return fTotalPaid
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPaid", fTotalPaid, value)
        End Set
    End Property

    Dim fDaysWorked As Decimal
    Public Property DaysWorked() As Integer
        Get
            Return fDaysWorked
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DaysWorked", fDaysWorked, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <NonPersistent>
    Public Property SelectOption As Boolean

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