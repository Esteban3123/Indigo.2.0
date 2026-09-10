Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.CostDistribution")>
Public Class PayrollCostDistributionsReportXpo
    Inherits XPLiteObject

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

    Dim fGroupId As PayrollGroup
    <Association("Payroll_CostDistributionsReferencesPayroll_Group")>
    Public Property GroupId() As PayrollGroup
        Get
            Return fGroupId
        End Get
        Set(ByVal value As PayrollGroup)
            SetPropertyValue(Of PayrollGroup)("GroupId", fGroupId, value)
        End Set
    End Property

    Dim fPayrollEndDate As Date
    Public Property PayrollEndDate() As Date
        Get
            Return fPayrollEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("PayrollEndDate", fPayrollEndDate, value)
        End Set
    End Property

    Dim fLiquidationDetailId As PayrollLiquidationDetail
    <Association("Payroll_CostDistributionsReferencesPayroll_LiquidationDetail")>
    Public Property LiquidationDetailId() As PayrollLiquidationDetail
        Get
            Return fLiquidationDetailId
        End Get
        Set(ByVal value As PayrollLiquidationDetail)
            SetPropertyValue(Of PayrollLiquidationDetail)("LiquidationDetailId", fLiquidationDetailId, value)
        End Set
    End Property

    Dim fJournalVoucherTypeId As GeneralLedgerJournalVoucherTypesReportXpo
    <Association("Payroll_CostDistributionsReferencesGeneralLedger_JournalVoucherTypes")>
    Public Property JournalVoucherTypeId() As GeneralLedgerJournalVoucherTypesReportXpo
        Get
            Return fJournalVoucherTypeId
        End Get
        Set(ByVal value As GeneralLedgerJournalVoucherTypesReportXpo)
            SetPropertyValue(Of GeneralLedgerJournalVoucherTypesReportXpo)("JournalVoucherTypeId", fJournalVoucherTypeId, value)
        End Set
    End Property

    Dim fMainAccountId As MainAccountsXpo
    <Association("Payroll_CostDistributionsReferencesGeneralLedger_MainAccounts")>
    Public Property MainAccountId() As MainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Payroll_CostDistributionsReferencesCommon_ThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fCostCenterId As Payroll_CostCenterReportXpo
    <Association("Payroll_CostDistributionsReferencesPayroll_CostCenter")>
    Public Property CostCenterId() As Payroll_CostCenterReportXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Payroll_CostCenterReportXpo)
            SetPropertyValue(Of Payroll_CostCenterReportXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property

    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
        End Set
    End Property

    Dim fEmployeeId As PayrollEmployee
    <Association("Payroll_CostDistributionsReferencesPayroll_Employee")>
    Public Property EmployeeId() As PayrollEmployee
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As PayrollEmployee)
            SetPropertyValue(Of PayrollEmployee)("EmployeeId", fEmployeeId, value)
        End Set
    End Property

    Dim fNumberHours As Integer
    Public Property NumberHours() As Integer
        Get
            Return fNumberHours
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NumberHours", fNumberHours, value)
        End Set
    End Property

    Dim fDetail As String
    Public Property Detail As String
        Get
            Return fDetail
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class