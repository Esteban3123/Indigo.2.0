Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioInitialBalanceAccountReceivableAccounting")> _
Public Class PortfolioInitialBalanceAccountReceivableAccountingReportXpo
    Inherits XPLiteObject
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
    Dim fPortfolioInitialBalanceAccountReceivableId As PortfolioInitialBalanceAccountReceivableReportXpo
    <Association("PortfolioInitialBalanceAccountReceivableAccountingReportXpoReferencesPortfolioInitialBalanceAccountReceivableReportXpo")> _
    Public Property PortfolioInitialBalanceAccountReceivableId() As PortfolioInitialBalanceAccountReceivableReportXpo
        Get
            Return fPortfolioInitialBalanceAccountReceivableId
        End Get
        Set(ByVal value As PortfolioInitialBalanceAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioInitialBalanceAccountReceivableReportXpo)("PortfolioInitialBalanceAccountReceivableId", fPortfolioInitialBalanceAccountReceivableId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PortfolioInitialBalanceAccountReceivableAccountingReportXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PortfolioInitialBalanceAccountReceivableAccountingReportXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("PortfolioInitialBalanceAccountReceivableAccountingReportXpoReferencesPayrollCostCenterXpo")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
