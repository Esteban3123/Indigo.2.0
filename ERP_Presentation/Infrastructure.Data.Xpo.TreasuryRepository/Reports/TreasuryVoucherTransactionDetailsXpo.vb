Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VoucherTransactionDetails")> _
Public Class TreasuryVoucherTransactionDetailsXpo
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
    Dim fIdVoucherTransaction As TreasuryVoucherTransactionXpo
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesTreasuryVoucherTransactionXpo")> _
    Public Property IdVoucherTransaction() As TreasuryVoucherTransactionXpo
        Get
            Return fIdVoucherTransaction
        End Get
        Set(ByVal value As TreasuryVoucherTransactionXpo)
            SetPropertyValue(Of TreasuryVoucherTransactionXpo)("IdVoucherTransaction", fIdVoucherTransaction, value)
        End Set
    End Property
    Dim fIdEntityBankAccount As TreasuryEntityBankAccountsXpo
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesTreasuryEntityBankAccountsXpo")> _
    Public Property IdEntityBankAccount() As TreasuryEntityBankAccountsXpo
        Get
            Return fIdEntityBankAccount
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("IdEntityBankAccount", fIdEntityBankAccount, value)
        End Set
    End Property
    Dim fCashRegisterId As TreasuryCashRegistersXpo
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesTreasuryCashRegistersXpo")> _
    Public Property CashRegisterId() As TreasuryCashRegistersXpo
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyReportXpo
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyReportXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdExpenseConcept As TreasuryExpenseConceptsXpo
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesTreasuryExpenseConceptsXpo")> _
    Public Property IdExpenseConcept() As TreasuryExpenseConceptsXpo
        Get
            Return fIdExpenseConcept
        End Get
        Set(ByVal value As TreasuryExpenseConceptsXpo)
            SetPropertyValue(Of TreasuryExpenseConceptsXpo)("IdExpenseConcept", fIdExpenseConcept, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
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
    Dim fIdCostCenter As PayrollCostCenterXpo
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesPayrollCostCenterXpo")> _
    Public Property IdCostCenter() As PayrollCostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("IdCostCenter", fIdCostCenter, value)
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
    Dim fIdRetentionConcept As Integer
    Public Property IdRetentionConcept() As Integer
        Get
            Return fIdRetentionConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetentionConcept", fIdRetentionConcept, value)
        End Set
    End Property
    Dim fPercentRetention As Decimal
    Public Property PercentRetention() As Decimal
        Get
            Return fPercentRetention
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentRetention", fPercentRetention, value)
        End Set
    End Property
    Dim fDetail As String
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Dim fdiscountableIVA As Boolean?
    Public Property discountableIVA() As Boolean?
        Get
            Return fdiscountableIVA
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("discountableIVA", fdiscountableIVA, value)
        End Set
    End Property

    Dim fValueIVA As Decimal?
    Public Property ValueIVA() As Decimal?
        Get
            Return fValueIVA
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("ValueIVA", fValueIVA, value)
        End Set
    End Property

    Dim fTotalConcept As Decimal?
    Public Property TotalConcept() As Decimal?
        Get
            Return fTotalConcept
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TotalConcept", fTotalConcept, value)
        End Set
    End Property

    Dim fTaxRegistration As Byte?
    Public Property TaxRegistration() As Byte?
        Get
            Return fTaxRegistration
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("TaxRegistration", fTaxRegistration, value)
        End Set
    End Property

    Dim fGeneralLedgerIVAXpo As GeneralLedgerIVAXpo
    <Persistent("IdGeneralLedgerIVA")>
    <Association("GeneralLedger_References_VoucherTransactionDetails")>
    Public Property GeneralLedgerIVAXpo() As GeneralLedgerIVAXpo
        Get
            Return fGeneralLedgerIVAXpo
        End Get
        Set(ByVal value As GeneralLedgerIVAXpo)
            SetPropertyValue(Of GeneralLedgerIVAXpo)("GeneralLedgerIVAXpo", fGeneralLedgerIVAXpo, value)
        End Set
    End Property

    <PersistentAlias("GeneralLedgerIVAXpo.Id")>
    Public ReadOnly Property IdGeneralLedgerIVA() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("IdGeneralLedgerIVA"))
        End Get
    End Property

    <Association("TreasuryDischargeBillXpoReferencesTreasuryVoucherTransactionDetailsXpo", GetType(TreasuryDischargeBillXpo))> _
    Public ReadOnly Property TreasuryDischargeBillXpo() As XPCollection(Of TreasuryDischargeBillXpo)
        Get
            Return GetCollection(Of TreasuryDischargeBillXpo)("TreasuryDischargeBillXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionAdvanceXpoReferencesTreasuryVoucherTransactionDetailsXpo", GetType(TreasuryVoucherTransactionAdvanceXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionAdvanceXpo() As XPCollection(Of TreasuryVoucherTransactionAdvanceXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionAdvanceXpo)("TreasuryVoucherTransactionAdvanceXpo")
        End Get
    End Property
    <Association("TreasuryTreasuryAdvancesXpoReferencesTreasuryVoucherTransactionDetailsXpo", GetType(TreasuryTreasuryAdvancesXpo))>
    Public ReadOnly Property TreasuryTreasuryAdvancesXpo() As XPCollection(Of TreasuryTreasuryAdvancesXpo)
        Get
            Return GetCollection(Of TreasuryTreasuryAdvancesXpo)("TreasuryTreasuryAdvancesXpo")
        End Get
    End Property

    Dim fSupplierBankAccountId As SupplierBankAccountXpo
    <Association("TreasuryVoucheDetailsReferencesSupplierBankAccount")>
    Public Property SupplierBankAccountId() As SupplierBankAccountXpo
        Get
            Return fSupplierBankAccountId
        End Get
        Set(value As SupplierBankAccountXpo)
            SetPropertyValue(Of SupplierBankAccountXpo)("SupplierBankAccountId", fSupplierBankAccountId, value)
        End Set
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
