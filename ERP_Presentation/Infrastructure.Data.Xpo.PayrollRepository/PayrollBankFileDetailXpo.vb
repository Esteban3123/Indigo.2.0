#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

#End Region

<Persistent("Payroll.BankFileDetail")>
Public Class PayrollBankFileDetailXpo
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

    Dim fPayrollBankFileId As PayrollBankFileXpo
    <Association("Payroll_BankFileDetail_References_BankFile")>
    Public Property BankFileId As PayrollBankFileXpo
        Get
            Return fPayrollBankFileId
        End Get
        Set(value As PayrollBankFileXpo)
            SetPropertyValue(Of PayrollBankFileXpo)("BankFileId", fPayrollBankFileId, value)
        End Set

    End Property

    Dim fLiquidationId As PayrollLiquidationXpo
    <Association("Payroll_BankFileDetail_References_Payroll_Liquidation")>
    Public Property LiquidationId() As PayrollLiquidationXpo
        Get
            Return fLiquidationId
        End Get
        Set(ByVal value As PayrollLiquidationXpo)
            SetPropertyValue(Of PayrollLiquidationXpo)("LiquidationId", fLiquidationId, value)
        End Set
    End Property


    Dim fGroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
        End Set
    End Property

    Dim fPositionId As Integer
    Public Property PositionId() As Integer
        Get
            Return fPositionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PositionId", fPositionId, value)
        End Set
    End Property

    Dim fFunctionalUnitId As Integer
    Public Property FunctionalUnitId() As Integer
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FunctionalUnitId", fFunctionalUnitId, value)
        End Set
    End Property

    Dim fContractId As Integer
    Public Property ContractId() As Integer
        Get
            Return fContractId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractId", fContractId, value)
        End Set
    End Property

    Dim fEmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
        End Set
    End Property

    Dim fEmployeeBankId As Integer
    Public Property EmployeeBankId() As Integer
        Get
            Return fEmployeeBankId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeBankId", fEmployeeBankId, value)
        End Set
    End Property


    Dim fEmployeeBankTypeAccount As Integer
    Public Property EmployeeBankTypeAccount() As Integer
        Get
            Return fEmployeeBankTypeAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeBankTypeAccount", fEmployeeBankTypeAccount, value)
        End Set
    End Property

    Dim fEmployeeBankAccountNumber As String
    Public Property EmployeeBankAccountNumber() As String
        Get
            Return fEmployeeBankAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeeBankAccountNumber", fEmployeeBankAccountNumber, value)
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