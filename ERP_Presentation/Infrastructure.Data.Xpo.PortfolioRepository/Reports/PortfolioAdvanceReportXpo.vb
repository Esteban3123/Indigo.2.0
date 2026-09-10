Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioAdvance")> _
Public Class PortfolioAdvanceReportXpo
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
    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fCashReceiptId As Integer
    Public Property CashReceiptId() As Integer
        Get
            Return fCashReceiptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CashReceiptId", fCashReceiptId, value)
        End Set
    End Property
    Dim fCashReceiptDetailId As Integer
    Public Property CashReceiptDetailId() As Integer
        Get
            Return fCashReceiptDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CashReceiptDetailId", fCashReceiptDetailId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PortfolioAdvanceReportXpoReferencesCommonThirdPartyXpo")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PortfolioAdvanceReportXpoReferencesGeneralLedgerMainAccountsXpo")>
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("PortfolioAdvanceReportXpoReferencesPayrollCostCenterXpo")>
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fCustomerId As CommonCustomerReportXpo
    <Association("PortfolioAdvanceReportXpoReferencesCommonCustomerReportXpo")>
    Public Property CustomerId() As CommonCustomerReportXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CommonCustomerReportXpo)
            SetPropertyValue(Of CommonCustomerReportXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property
    Dim fSellerId As Integer
    Public Property SellerId() As Integer
        Get
            Return fSellerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SellerId", fSellerId, value)
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
    Dim fTransferValue As Decimal
    Public Property TransferValue() As Decimal
        Get
            Return fTransferValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TransferValue", fTransferValue, value)
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
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(300)>
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fConfirmationUser As String
    <Size(20)>
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)>
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Dim fTRMValue As Decimal?
    Public Property TRMValue() As Decimal?
        Get
            Return fTRMValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TRMValue", fTRMValue, value)
        End Set
    End Property

#End Region

#Region "PersistentAlias"

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property Abbreviation As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(Me.EvaluateAlias("Abbreviation"))) _
                , CrossCutting.Base.SessionValues.Instance.CurrencyISO4217,
                Convert.ToString(Me.EvaluateAlias("Abbreviation")))
        End Get
    End Property

#End Region

#Region "Association"

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferencePortfolioAdvanceReportXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <Association("PortfolioTransferReportXpoReferencesPortfolioAdvanceReportXpo", GetType(PortfolioTransferReportXpo))>
    Public ReadOnly Property PortfolioTransferReportXpo() As XPCollection(Of PortfolioTransferReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferReportXpo)("PortfolioTransferReportXpo")
        End Get
    End Property

    <Association("PortfolioNoteAccountReceivableAdvanceReportXpoReferencesPortfolioAdvanceReportXpo", GetType(PortfolioNoteAccountReceivableAdvanceReportXpo))>
    Public ReadOnly Property PortfolioNoteAccountReceivableAdvanceReportXpo() As XPCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)("PortfolioNoteAccountReceivableAdvanceReportXpo")
        End Get
    End Property

    <Association("PortfolioNoteDistributionReferencesPortfolioAdvanceReportXpo", GetType(PortfolioNoteDistributionXpo))>
    Public ReadOnly Property PortfolioNoteDistributionReportXpo() As XPCollection(Of PortfolioNoteDistributionXpo)
        Get
            Return GetCollection(Of PortfolioNoteDistributionXpo)("PortfolioNoteDistributionReportXpo")
        End Get
    End Property

    <Association("PortfolioNoteDistributionOriginalReferencesPortfolioAdvanceReportXpo", GetType(PortfolioNoteDistributionOriginalXpo))>
    Public ReadOnly Property PortfolioNoteDistributionOriginalXpo() As XPCollection(Of PortfolioNoteDistributionOriginalXpo)
        Get
            Return GetCollection(Of PortfolioNoteDistributionOriginalXpo)("PortfolioNoteDistributionOriginalXpo")
        End Get
    End Property

    <Association("PortfolioNoteReferencesPortfolioAdvanceReportXpo", GetType(PortfolioNoteReportXpo))>
    Public ReadOnly Property PortfolioNoteAdvanceReportXpo() As XPCollection(Of PortfolioNoteReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteReportXpo)("PortfolioNoteAdvanceReportXpo")
        End Get
    End Property
#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region
End Class
