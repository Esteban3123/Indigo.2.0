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

    Dim fIdAccountLevel As GeneralLedgerMainAccountLevels
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountLevels")> _
    Public Property IdAccountLevel() As GeneralLedgerMainAccountLevels
        Get
            Return fIdAccountLevel
        End Get
        Set(ByVal value As GeneralLedgerMainAccountLevels)
            SetPropertyValue(Of GeneralLedgerMainAccountLevels)("IdAccountLevel", fIdAccountLevel, value)
        End Set
    End Property

    Dim fIdAccountClass As GeneralLedgerMainAccountClasses
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountClasses")> _
    Public Property IdAccountClass() As GeneralLedgerMainAccountClasses
        Get
            Return fIdAccountClass
        End Get
        Set(ByVal value As GeneralLedgerMainAccountClasses)
            SetPropertyValue(Of GeneralLedgerMainAccountClasses)("IdAccountClass", fIdAccountClass, value)
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

    Dim fNature As String
    Public Property Nature() As String
        Get
            Return fNature
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nature", fNature, value)
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

    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("GeneralLedgerMainAccountsXpoReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
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

    <PersistentAlias("concat(Number,' - ', Name)")>
    Public ReadOnly Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
    End Property

    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(GeneralLedgerMainAccountsXpo))> _
    Public ReadOnly Property GeneralLedgerMainAccountsCollection() As XPCollection(Of GeneralLedgerMainAccountsXpo)
        Get
            Return GetCollection(Of GeneralLedgerMainAccountsXpo)("GeneralLedgerMainAccountsCollection")
        End Get
    End Property

    <Association("PaymentsPaymentTransferDetailReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsPaymentTransferDetail))> _
    Public ReadOnly Property PaymentsPaymentTransferDetail() As XPCollection(Of PaymentsPaymentTransferDetail)
        Get
            Return GetCollection(Of PaymentsPaymentTransferDetail)("PaymentsPaymentTransferDetail")
        End Get
    End Property
    <Association("CommonDistributionLinesReferencesGeneralLedgerMainAccountsXpo", GetType(CommonDistributionLines))> _
    Public ReadOnly Property CommonDistributionLines() As XPCollection(Of CommonDistributionLines)
        Get
            Return GetCollection(Of CommonDistributionLines)("CommonDistributionLines")
        End Get
    End Property
    <Association("PaymentsPaymentTransferReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsPaymentTransfer))> _
    Public ReadOnly Property PaymentsPaymentTransfer() As XPCollection(Of PaymentsPaymentTransfer)
        Get
            Return GetCollection(Of PaymentsPaymentTransfer)("PaymentsPaymentTransfer")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailCxPReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryCrossingAccountDetailCxP))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailCxP() As XPCollection(Of TreasuryCrossingAccountDetailCxP)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailCxP)("TreasuryCrossingAccountDetailCxP")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionDetailsReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryVoucherTransactionDetails))> _
    Public ReadOnly Property TreasuryVoucherTransactionDetails() As XPCollection(Of TreasuryVoucherTransactionDetails)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetails)("TreasuryVoucherTransactionDetails")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionReferencesGeneralLedgerMainAccountsXpo", GetType(TreasuryVoucherTransaction))> _
    Public ReadOnly Property TreasuryVoucherTransaction() As XPCollection(Of TreasuryVoucherTransaction)
        Get
            Return GetCollection(Of TreasuryVoucherTransaction)("TreasuryVoucherTransaction")
        End Get
    End Property
    <Association("PaymentsAccountPayableDetailConceptXpoPReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAccountPayableDetailConceptXpoP))> _
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpoP() As XPCollection(Of PaymentsAccountPayableDetailConceptXpoP)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpoP)("PaymentsAccountPayableDetailConceptXpoP")
        End Get
    End Property
    <Association("PaymentsAccountPayableConceptsXpoPReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAccountPayableConceptsXpoP))> _
    Public ReadOnly Property PaymentsAccountPayableConceptsXpoP() As XPCollection(Of PaymentsAccountPayableConceptsXpoP)
        Get
            Return GetCollection(Of PaymentsAccountPayableConceptsXpoP)("PaymentsAccountPayableConceptsXpoP")
        End Get
    End Property
    <Association("PaymentsAccountPayableReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAccountPayable))> _
    Public ReadOnly Property PaymentsAccountPayable() As XPCollection(Of PaymentsAccountPayable)
        Get
            Return GetCollection(Of PaymentsAccountPayable)("PaymentsAccountPayable")
        End Get
    End Property
    <Association("PaymentsPaymentsNoteDetailsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsPaymentsNoteDetailsXpo))> _
    Public ReadOnly Property PaymentsPaymentsNoteDetailsXpo() As XPCollection(Of PaymentsPaymentsNoteDetailsXpo)
        Get
            Return GetCollection(Of PaymentsPaymentsNoteDetailsXpo)("PaymentsPaymentsNoteDetailsXpo")
        End Get
    End Property
    <Association("PaymentsInitialBalanceAdvanceXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsInitialBalanceAdvanceXpo))> _
    Public ReadOnly Property PaymentsInitialBalanceAdvanceXpo() As XPCollection(Of PaymentsInitialBalanceAdvanceXpo)
        Get
            Return GetCollection(Of PaymentsInitialBalanceAdvanceXpo)("PaymentsInitialBalanceAdvanceXpo")
        End Get
    End Property
    <Association("PaymentsInitialBalanceAccountPayableXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsInitialBalanceAccountPayableXpo))> _
    Public ReadOnly Property PaymentsInitialBalanceAccountPayableXpo() As XPCollection(Of PaymentsInitialBalanceAccountPayableXpo)
        Get
            Return GetCollection(Of PaymentsInitialBalanceAccountPayableXpo)("PaymentsInitialBalanceAccountPayableXpo")
        End Get
    End Property
    <Association("PaymentsDeferredCausationXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsDeferredCausationXpo))> _
    Public ReadOnly Property PaymentsDeferredCausationXpo() As XPCollection(Of PaymentsDeferredCausationXpo)
        Get
            Return GetCollection(Of PaymentsDeferredCausationXpo)("PaymentsDeferredCausationXpo")
        End Get
    End Property
    <Association("PaymentsDeferredCausationDetailsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsDeferredCausationDetailsXpo))> _
    Public ReadOnly Property PaymentsDeferredCausationDetailsXpo() As XPCollection(Of PaymentsDeferredCausationDetailsXpo)
        Get
            Return GetCollection(Of PaymentsDeferredCausationDetailsXpo)("PaymentsDeferredCausationDetailsXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableConceptNotesXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAccountPayableConceptNotesXpo))> _
    Public ReadOnly Property PaymentsAccountPayableConceptNotesXpo() As XPCollection(Of PaymentsAccountPayableConceptNotesXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableConceptNotesXpo)("PaymentsAccountPayableConceptNotesXpo")
        End Get
    End Property
    <Association("PaymentsAdvancePaymentsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PaymentsAdvancePaymentsXpo))> _
    Public ReadOnly Property PaymentsAdvancePaymentsXpo() As XPCollection(Of PaymentsAdvancePaymentsXpo)
        Get
            Return GetCollection(Of PaymentsAdvancePaymentsXpo)("PaymentsAdvancePaymentsXpo")
        End Get
    End Property
    <Association("Payments_PaymentTransferOtherConceptReferencesGeneralLedger_MainAccounts", GetType(PaymentsPaymentTransferOtherConcept))>
    Public ReadOnly Property PaymentsPaymentTransferOtherConcept() As XPCollection(Of PaymentsPaymentTransferOtherConcept)
        Get
            Return GetCollection(Of PaymentsPaymentTransferOtherConcept)("PaymentsPaymentTransferOtherConcept")
        End Get
    End Property

    <Association("GeneralLedgerIVAXpo_ReferencesMainAccounts", GetType(GeneralLedgerIVAXpo))>
    Public ReadOnly Property GeneralLedgerIVAXpoMainAccounts As XPCollection(Of GeneralLedgerIVAXpo)
        Get
            Return GetCollection(Of GeneralLedgerIVAXpo)("GeneralLedgerIVAXpoMainAccounts")
        End Get
    End Property
    <Association("GeneralLedgerIVAXpo_MainAccountDebit_ReferencesMainAccounts", GetType(GeneralLedgerIVAXpo))>
    Public ReadOnly Property GeneralLedgerIVAXpoMainAccountsDebit As XPCollection(Of GeneralLedgerIVAXpo)
        Get
            Return GetCollection(Of GeneralLedgerIVAXpo)("GeneralLedgerIVAXpoMainAccountsDebit")
        End Get
    End Property
    <Association("GeneralLedgerIVAXpo_MainAccountCredit_ReferencesMainAccounts", GetType(GeneralLedgerIVAXpo))>
    Public ReadOnly Property GeneralLedgerIVAXpoMainAccountsCredit As XPCollection(Of GeneralLedgerIVAXpo)
        Get
            Return GetCollection(Of GeneralLedgerIVAXpo)("GeneralLedgerIVAXpoMainAccountsCredit")
        End Get
    End Property

    <Association("PaymentsRevaluationDetailReferencesMainAccounts", GetType(PaymentsRevaluationDetailXpo))>
    Public ReadOnly Property PaymentsRevaluationDetailMainAccounts As XPCollection(Of PaymentsRevaluationDetailXpo)
        Get
            Return GetCollection(Of PaymentsRevaluationDetailXpo)("PaymentsRevaluationDetailMainAccounts")
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
