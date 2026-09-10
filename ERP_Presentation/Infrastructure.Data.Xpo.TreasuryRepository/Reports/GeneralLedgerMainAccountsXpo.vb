Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("GeneralLedger.MainAccounts")> _
Public Class GeneralLedgerMainAccountsXpo
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
    Dim fIdAccountLevel As GeneralLedgerMainAccountLevelsXpo
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountLevelsXpo")> _
    Public Property IdAccountLevel() As GeneralLedgerMainAccountLevelsXpo
        Get
            Return fIdAccountLevel
        End Get
        Set(ByVal value As GeneralLedgerMainAccountLevelsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountLevelsXpo)("IdAccountLevel", fIdAccountLevel, value)
        End Set
    End Property
    Dim fIdAccountClass As GeneralLedgerMainAccountClassesXpo
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountClassesXpo")> _
    Public Property IdAccountClass() As GeneralLedgerMainAccountClassesXpo
        Get
            Return fIdAccountClass
        End Get
        Set(ByVal value As GeneralLedgerMainAccountClassesXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountClassesXpo)("IdAccountClass", fIdAccountClass, value)
        End Set
    End Property
    Dim fNumber As String
    <Size(50)> _
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fIdParent As GeneralLedgerMainAccountsXpo
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdParent() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdParent
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdParent", fIdParent, value)
        End Set
    End Property
    Dim fHandlesThirdParty As Boolean
    Public Property HandlesThirdParty() As Boolean
        Get
            Return fHandlesThirdParty
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesThirdParty", fHandlesThirdParty, value)
        End Set
    End Property
    Dim fCloseThirdParty As Boolean
    Public Property CloseThirdParty() As Boolean
        Get
            Return fCloseThirdParty
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CloseThirdParty", fCloseThirdParty, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyReportXpo
    <Association("GeneralLedgerMainAccountsXpoReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyReportXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fReconcileAccount As Boolean
    Public Property ReconcileAccount() As Boolean
        Get
            Return fReconcileAccount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ReconcileAccount", fReconcileAccount, value)
        End Set
    End Property
    Dim fAvailability As Byte
    Public Property Availability() As Byte
        Get
            Return fAvailability
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Availability", fAvailability, value)
        End Set
    End Property
    Dim fHandlesCostCenter As Boolean
    Public Property HandlesCostCenter() As Boolean
        Get
            Return fHandlesCostCenter
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesCostCenter", fHandlesCostCenter, value)
        End Set
    End Property
    Dim fRetencionType As Byte
    Public Property RetencionType() As Byte
        Get
            Return fRetencionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RetencionType", fRetencionType, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fNature As Byte?
    Public Property Nature() As Byte?
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("Nature", fNature, value)
        End Set
    End Property

    <PersistentAlias("concat(Number,' - ', Name)")>
    Public ReadOnly Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
    End Property

    <PersistentAlias("IIF(Nature=1,'Débito','Crédito')")>
    Public ReadOnly Property NatureName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NatureName"))
        End Get
    End Property

    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(GeneralLedgerMainAccountsXpo))> _
    Public ReadOnly Property GeneralLedgerMainAccountsXpo() As XPCollection(Of GeneralLedgerMainAccountsXpo)
        Get
            Return GetCollection(Of GeneralLedgerMainAccountsXpo)("GeneralLedgerMainAccountsXpo")
        End Get
    End Property
    <Association("TreasuryCashReceiptDetailsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryCashReceiptDetailsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptDetailsXpo() As XPCollection(Of TreasuryCashReceiptDetailsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptDetailsXpo)("TreasuryCashReceiptDetailsXpo")
        End Get
    End Property
    <Association("TreasuryCashReceiptsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryCashReceiptsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptsXpo() As XPCollection(Of TreasuryCashReceiptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptsXpo)("TreasuryCashReceiptsXpo")
        End Get
    End Property
    <Association("TreasuryCashRegistersXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryCashRegistersXpo))> _
    Public ReadOnly Property TreasuryCashRegistersXpo() As XPCollection(Of TreasuryCashRegistersXpo)
        Get
            Return GetCollection(Of TreasuryCashRegistersXpo)("TreasuryCashRegistersXpo")
        End Get
    End Property
    <Association("TreasuryEntityBankAccountsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryEntityBankAccountsXpo))> _
    Public ReadOnly Property TreasuryEntityBankAccountsXpo() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountsXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryVoucherTransactionXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionXpo() As XPCollection(Of TreasuryVoucherTransactionXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionXpo)("TreasuryVoucherTransactionXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryVoucherTransactionDetailsXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionDetailsXpo() As XPCollection(Of TreasuryVoucherTransactionDetailsXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetailsXpo)("TreasuryVoucherTransactionDetailsXpo")
        End Get
    End Property
    <Association("TreasuryCashReceiptConceptsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryCashReceiptConceptsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptConceptsXpo() As XPCollection(Of TreasuryCashReceiptConceptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptConceptsXpo)("TreasuryCashReceiptConceptsXpo")
        End Get
    End Property
    <Association("TreasuryExpenseConceptsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryExpenseConceptsXpo))> _
    Public ReadOnly Property TreasuryExpenseConceptsXpo() As XPCollection(Of TreasuryExpenseConceptsXpo)
        Get
            Return GetCollection(Of TreasuryExpenseConceptsXpo)("TreasuryExpenseConceptsXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableConceptNotesXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAccountPayableConceptNotesXpo))> _
    Public ReadOnly Property PaymentsAccountPayableConceptNotesXpo() As XPCollection(Of PaymentsAccountPayableConceptNotesXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableConceptNotesXpo)("PaymentsAccountPayableConceptNotesXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableConceptsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAccountPayableConceptsXpo))> _
    Public ReadOnly Property PaymentsAccountPayableConceptsXpo() As XPCollection(Of PaymentsAccountPayableConceptsXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableConceptsXpo)("PaymentsAccountPayableConceptsXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableDetailConceptXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAccountPayableDetailConceptXpo))> _
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpo() As XPCollection(Of PaymentsAccountPayableDetailConceptXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpo)("PaymentsAccountPayableDetailConceptXpo")
        End Get
    End Property
    <Association("PortfolioAdvanceReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property
    <Association("PaymentsAdvancePaymentsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAdvancePaymentsXpo))> _
    Public ReadOnly Property PaymentsAdvancePaymentsXpo() As XPCollection(Of PaymentsAdvancePaymentsXpo)
        Get
            Return GetCollection(Of PaymentsAdvancePaymentsXpo)("PaymentsAdvancePaymentsXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableAccountingXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioAccountReceivableAccountingXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableAccountingXpo() As XPCollection(Of PortfolioAccountReceivableAccountingXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingXpo)("PortfolioAccountReceivableAccountingXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailCxCXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryCrossingAccountDetailCxCXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailCxCXpo() As XPCollection(Of TreasuryCrossingAccountDetailCxCXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailCxCXpo)("TreasuryCrossingAccountDetailCxCXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailCxPXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryCrossingAccountDetailCxPXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailCxPXpo() As XPCollection(Of TreasuryCrossingAccountDetailCxPXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailCxPXpo)("TreasuryCrossingAccountDetailCxPXpo")
        End Get
    End Property
    <Association("TreasuryConsignmentDetailXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryConsignmentDetailXpo))> _
    Public ReadOnly Property TreasuryConsignmentDetailXpo() As XPCollection(Of TreasuryConsignmentDetailXpo)
        Get
            Return GetCollection(Of TreasuryConsignmentDetailXpo)("TreasuryConsignmentDetailXpo")
        End Get
    End Property
    <Association("TreasuryConsignmentXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryConsignmentXpo))> _
    Public ReadOnly Property TreasuryConsignmentXpo() As XPCollection(Of TreasuryConsignmentXpo)
        Get
            Return GetCollection(Of TreasuryConsignmentXpo)("TreasuryConsignmentXpo")
        End Get
    End Property
    <Association("TreasurySchedulePaymentDetailXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasurySchedulePaymentDetailXpo))> _
    Public ReadOnly Property TreasurySchedulePaymentDetailXpo() As XPCollection(Of TreasurySchedulePaymentDetailXpo)
        Get
            Return GetCollection(Of TreasurySchedulePaymentDetailXpo)("TreasurySchedulePaymentDetailXpo")
        End Get
    End Property
    <Association("TreasuryNotesXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryNotesXpo))> _
    Public ReadOnly Property TreasuryNotesXpo() As XPCollection(Of TreasuryNotesXpo)
        Get
            Return GetCollection(Of TreasuryNotesXpo)("TreasuryNotesXpo")
        End Get
    End Property
    <Association("TreasuryNotesDetailXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryNotesDetailXpo))> _
    Public ReadOnly Property TreasuryNotesDetailXpo() As XPCollection(Of TreasuryNotesDetailXpo)
        Get
            Return GetCollection(Of TreasuryNotesDetailXpo)("TreasuryNotesDetailXpo")
        End Get
    End Property
    <Association("PortfolioTransferXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioTransferXpo))> _
    Public ReadOnly Property PortfolioTransferXpo() As XPCollection(Of PortfolioTransferXpo)
        Get
            Return GetCollection(Of PortfolioTransferXpo)("PortfolioTransferXpo")
        End Get
    End Property
    <Association("PortfolioTransferDetailXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioTransferDetailXpo))> _
    Public ReadOnly Property PortfolioTransferDetailXpo() As XPCollection(Of PortfolioTransferDetailXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailXpo)("PortfolioTransferDetailXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryCrossingAccountDetailOtherConceptXpo))>
    Public ReadOnly Property TreasuryCrossingAccountDetailOtherConceptXpo() As XPCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)("TreasuryCrossingAccountDetailOtherConceptXpo")
        End Get
    End Property

    <Association("MainAccount_Reference_AccountPurchaseService", GetType(GeneralLedgerIVAXpo))>
    Public ReadOnly Property IVAAccountPurchaseServiceXpo() As XPCollection(Of GeneralLedgerIVAXpo)
        Get
            Return GetCollection(Of GeneralLedgerIVAXpo)("IVAAccountPurchaseServiceXpo")
        End Get
    End Property

    <Association("MainAccount_Reference_AccountDebitControlFiscal", GetType(GeneralLedgerIVAXpo))>
    Public ReadOnly Property IVAAccountDebitControlFiscalXpo() As XPCollection(Of GeneralLedgerIVAXpo)
        Get
            Return GetCollection(Of GeneralLedgerIVAXpo)("IVAAccountDebitControlFiscalXpo")
        End Get
    End Property

    <Association("MainAccount_Reference_AccountCreditControlFiscal", GetType(GeneralLedgerIVAXpo))>
    Public ReadOnly Property IVAAccountCreditControlFiscalXpo() As XPCollection(Of GeneralLedgerIVAXpo)
        Get
            Return GetCollection(Of GeneralLedgerIVAXpo)("IVAAccountCreditControlFiscalXpo")
        End Get
    End Property

    <Association("Customer_References_MainAccounts", GetType(CustomerXpo))>
    Public ReadOnly Property CustomerXpo() As XPCollection(Of CustomerXpo)
        Get
            Return GetCollection(Of CustomerXpo)("CustomerXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
