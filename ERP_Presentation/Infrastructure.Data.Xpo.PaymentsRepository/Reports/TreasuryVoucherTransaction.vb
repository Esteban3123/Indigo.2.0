Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VoucherTransaction")> _
Public Class TreasuryVoucherTransaction
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("TreasuryVoucherTransactionReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("TreasuryVoucherTransactionReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As Integer
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fVoucherClass As Integer
    Public Property VoucherClass() As Integer
        Get
            Return fVoucherClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VoucherClass", fVoucherClass, value)
        End Set
    End Property
    Dim fExpenseType As Byte
    Public Property ExpenseType() As Byte
        Get
            Return fExpenseType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ExpenseType", fExpenseType, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(500)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
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
    Dim fIdCashRegister As Integer
    Public Property IdCashRegister() As Integer
        Get
            Return fIdCashRegister
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCashRegister", fIdCashRegister, value)
        End Set
    End Property
    Dim fIdEntityBankAccount As Integer
    Public Property IdEntityBankAccount() As Integer
        Get
            Return fIdEntityBankAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEntityBankAccount", fIdEntityBankAccount, value)
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
    Dim fPaymentMethod As Byte
    Public Property PaymentMethod() As Byte
        Get
            Return fPaymentMethod
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PaymentMethod", fPaymentMethod, value)
        End Set
    End Property
    Dim fNoteNumber As String
    <Size(50)> _
    Public Property NoteNumber() As String
        Get
            Return fNoteNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NoteNumber", fNoteNumber, value)
        End Set
    End Property
    Dim fIdChecks As Integer
    Public Property IdChecks() As Integer
        Get
            Return fIdChecks
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdChecks", fIdChecks, value)
        End Set
    End Property
    Dim fCheckNumber As Long
    Public Property CheckNumber() As Long
        Get
            Return fCheckNumber
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("CheckNumber", fCheckNumber, value)
        End Set
    End Property
    Dim fTransactionDate As DateTime
    Public Property TransactionDate() As DateTime
        Get
            Return fTransactionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransactionDate", fTransactionDate, value)
        End Set
    End Property
    Dim fTaxByMil As Boolean
    Public Property TaxByMil() As Boolean
        Get
            Return fTaxByMil
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("TaxByMil", fTaxByMil, value)
        End Set
    End Property
    Dim fTaxByMilValue As Decimal
    Public Property TaxByMilValue() As Decimal
        Get
            Return fTaxByMilValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxByMilValue", fTaxByMilValue, value)
        End Set
    End Property
    Dim fCashRegisterExpense As Boolean
    Public Property CashRegisterExpense() As Boolean
        Get
            Return fCashRegisterExpense
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CashRegisterExpense", fCashRegisterExpense, value)
        End Set
    End Property
    Dim fRefundCashRegisterExpense As Boolean
    Public Property RefundCashRegisterExpense() As Boolean
        Get
            Return fRefundCashRegisterExpense
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RefundCashRegisterExpense", fRefundCashRegisterExpense, value)
        End Set
    End Property
    Dim fSchedulePaymentId As Integer
    Public Property SchedulePaymentId() As Integer
        Get
            Return fSchedulePaymentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SchedulePaymentId", fSchedulePaymentId, value)
        End Set
    End Property
    Dim fBeneficiaryIdentification As String
    <Size(20)> _
    Public Property BeneficiaryIdentification() As String
        Get
            Return fBeneficiaryIdentification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BeneficiaryIdentification", fBeneficiaryIdentification, value)
        End Set
    End Property
    Dim fBeneficiary As String
    Public Property Beneficiary() As String
        Get
            Return fBeneficiary
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Beneficiary", fBeneficiary, value)
        End Set
    End Property
    Dim fTransactionRelationship As Boolean
    Public Property TransactionRelationship() As Boolean
        Get
            Return fTransactionRelationship
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("TransactionRelationship", fTransactionRelationship, value)
        End Set
    End Property
    Dim fCheckReconciled As Boolean
    Public Property CheckReconciled() As Boolean
        Get
            Return fCheckReconciled
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CheckReconciled", fCheckReconciled, value)
        End Set
    End Property
    Dim fIdPaymentOrder As Integer
    Public Property IdPaymentOrder() As Integer
        Get
            Return fIdPaymentOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdPaymentOrder", fIdPaymentOrder, value)
        End Set
    End Property
    Dim fPrinted As Boolean
    Public Property Printed() As Boolean
        Get
            Return fPrinted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Printed", fPrinted, value)
        End Set
    End Property
    Dim fRTEValue As Decimal
    Public Property RTEValue() As Decimal
        Get
            Return fRTEValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RTEValue", fRTEValue, value)
        End Set
    End Property
    Dim fIVAValue As Decimal
    Public Property IVAValue() As Decimal
        Get
            Return fIVAValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IVAValue", fIVAValue, value)
        End Set
    End Property
    Dim fICAValue As Decimal
    Public Property ICAValue() As Decimal
        Get
            Return fICAValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ICAValue", fICAValue, value)
        End Set
    End Property
    Dim fOtherValue As Decimal
    Public Property OtherValue() As Decimal
        Get
            Return fOtherValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("OtherValue", fOtherValue, value)
        End Set
    End Property
    Dim fBankAccountNumber As String
    <Size(50)> _
    Public Property BankAccountNumber() As String
        Get
            Return fBankAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankAccountNumber", fBankAccountNumber, value)
        End Set
    End Property
    Dim fBankName As String
    Public Property BankName() As String
        Get
            Return fBankName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankName", fBankName, value)
        End Set
    End Property
    Dim fDetailsInterfaceBudget As String
    Public Property DetailsInterfaceBudget() As String
        Get
            Return fDetailsInterfaceBudget
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DetailsInterfaceBudget", fDetailsInterfaceBudget, value)
        End Set
    End Property
    Dim fIdUnitOperative As Integer
    Public Property IdUnitOperative() As Integer
        Get
            Return fIdUnitOperative
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdUnitOperative", fIdUnitOperative, value)
        End Set
    End Property
    Dim fIdRefund As Integer
    Public Property IdRefund() As Integer
        Get
            Return fIdRefund
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRefund", fIdRefund, value)
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
    <Size(20)> _
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
    <Size(20)> _
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
    <Size(20)> _
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
    <Size(20)> _
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
    <Association("PaymentsPaymentNotesReferencesTreasuryVoucherTransaction", GetType(PaymentsPaymentNotes))> _
    Public ReadOnly Property Payments_PaymentNotesCollection() As XPCollection(Of PaymentsPaymentNotes)
        Get
            Return GetCollection(Of PaymentsPaymentNotes)("Payments_PaymentNotesCollection")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionDetailsReferencesTreasuryVoucherTransaction", GetType(TreasuryVoucherTransactionDetails))> _
    Public ReadOnly Property TreasuryVoucherTransactionDetails() As XPCollection(Of TreasuryVoucherTransactionDetails)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetails)("TreasuryVoucherTransactionDetails")
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
