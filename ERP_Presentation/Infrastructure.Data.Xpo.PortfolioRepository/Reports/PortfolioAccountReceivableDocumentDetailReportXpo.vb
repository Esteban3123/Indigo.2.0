Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.AccountReceivableDocumentDetail")> _
Public Class PortfolioAccountReceivableDocumentDetailReportXpo
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
    Dim fAccountReceivableDocumentId As PortfolioAccountReceivableDocumentReportXpo
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesPortfolio_AccountReceivableDocument")> _
    Public Property AccountReceivableDocumentId() As PortfolioAccountReceivableDocumentReportXpo
        Get
            Return fAccountReceivableDocumentId
        End Get
        Set(ByVal value As PortfolioAccountReceivableDocumentReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableDocumentReportXpo)("AccountReceivableDocumentId", fAccountReceivableDocumentId, value)
        End Set
    End Property
    Dim fAccountReceivableConceptId As PortfolioAccountReceivableConceptReportXpo
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesPortfolio_AccountReceivableConcept")> _
    Public Property AccountReceivableConceptId() As PortfolioAccountReceivableConceptReportXpo
        Get
            Return fAccountReceivableConceptId
        End Get
        Set(ByVal value As PortfolioAccountReceivableConceptReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableConceptReportXpo)("AccountReceivableConceptId", fAccountReceivableConceptId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesPortfolio_GeneralLedgerMainAccounts")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesPayroll_CostCenter")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
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
