Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base

<Persistent("Treasury.VoucherTransaction")> _
Public Class TreasuryVoucherTransactionXpo
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
    Dim fIdThirdParty As CommonThirdPartyReportXpo
    <Association("TreasuryVoucherTransactionXpoReferencesCommonThirdPartyXpo")>
    Public Property IdThirdParty() As CommonThirdPartyReportXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("TreasuryVoucherTransactionXpoReferencesGeneralLedgerMainAccountsXpo")>
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpo
    <Association("TreasuryVoucherTransactionXpoReferencesPayrollCostCenterXpo")>
    Public Property IdCostCenter() As PayrollCostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("IdCostCenter", fIdCostCenter, value)
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
    <Size(500)>
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
    Dim fIdCashRegister As TreasuryCashRegistersXpo
    <Association("TreasuryVoucherTransactionXpoReferencesTreasuryCashRegistersXpo")>
    Public Property IdCashRegister() As TreasuryCashRegistersXpo
        Get
            Return fIdCashRegister
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("IdCashRegister", fIdCashRegister, value)
        End Set
    End Property

    Dim fIdEntityBankAccount As TreasuryEntityBankAccountsXpo
    <Association("TreasuryVoucherTransactionXpoReferencesTreasuryEntityBankAccountsXpo")>
    Public Property IdEntityBankAccount() As TreasuryEntityBankAccountsXpo
        Get
            Return fIdEntityBankAccount
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("IdEntityBankAccount", fIdEntityBankAccount, value)
        End Set
    End Property

    Dim fSupplierBankAccountId As SupplierBankAccountXpo
    <Association("TreasuryVoucherTransactionXpoReferencesCommonSupplierBankAccountXpo")>
    Public Property SupplierBankAccountId() As SupplierBankAccountXpo
        Get
            Return fSupplierBankAccountId
        End Get
        Set(ByVal value As SupplierBankAccountXpo)
            SetPropertyValue(Of SupplierBankAccountXpo)("SupplierBankAccountId", fSupplierBankAccountId, value)
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
    <Size(50)>
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
    Dim fSchedulePaymentId As TreasurySchedulePaymentXpo
    <Association("TreasuryVoucherTransactionXpoReferencesTreasurySchedulePaymentXpo")>
    Public Property SchedulePaymentId() As TreasurySchedulePaymentXpo
        Get
            Return fSchedulePaymentId
        End Get
        Set(ByVal value As TreasurySchedulePaymentXpo)
            SetPropertyValue(Of TreasurySchedulePaymentXpo)("SchedulePaymentId", fSchedulePaymentId, value)
        End Set
    End Property
    Dim fBeneficiaryIdentification As String
    <Size(20)>
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
    <Size(50)>
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

    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property

    Dim fReversedDate As DateTime
    Public Property ReversedDate() As DateTime
        Get
            Return fReversedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ReversedDate", fReversedDate, value)
        End Set
    End Property

    Dim fIdCheckCashingStatus As Byte
    Public Property IdCheckCashingStatus() As Byte
        Get
            Return fIdCheckCashingStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("IdCheckCashingStatus", fIdCheckCashingStatus, value)
        End Set
    End Property

    Dim fIdCheckStatusNew As Byte?
    <NonPersistent()>
    Public Property IdCheckStatusNew() As Byte?
        Get
            Return fIdCheckStatusNew
        End Get
        Set(ByVal value As Byte?)
            Me.fIdCheckStatusNew = value
        End Set
    End Property

    <PersistentAlias("iif(IdCheckStatusNew = 1, 'Girado', IdCheckStatusNew = 2, 'Entregado', IdCheckStatusNew = 3, 'Cobrado', IdCheckStatusNew = 4, 'Devuelto', IdCheckStatusNew = 5, 'Anulado', '')")>
    Public ReadOnly Property CheckStatusNew() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CheckStatusNew"))
        End Get
    End Property

    <PersistentAlias("iif(IdCheckCashingStatus = 1, 'Girado', IdCheckCashingStatus = 2, 'Entregado', IdCheckCashingStatus = 3, 'Cobrado', IdCheckCashingStatus = 4, 'Devuelto', IdCheckCashingStatus = 5, 'Anulado', '')")>
    Public ReadOnly Property CheckCashingStatus() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CheckCashingStatus"))
        End Get
    End Property

    Dim fIdUnitOperative As CommonOperatingUnit
    <Association("TreasuryVoucherTransactionXpoReferencesCommonOperatingUnit")>
    Public Property IdUnitOperative() As CommonOperatingUnit
        Get
            Return fIdUnitOperative
        End Get
        Set(ByVal value As CommonOperatingUnit)
            SetPropertyValue(Of CommonOperatingUnit)("IdUnitOperative", fIdUnitOperative, value)
        End Set
    End Property

    Dim fIdRefund As TreasuryRefundsXpo
    <Association("TreasuryVoucherTransactionXpoReferencesTreasuryRefundsXpo")>
    Public Property IdRefund() As TreasuryRefundsXpo
        Get
            Return fIdRefund
        End Get
        Set(ByVal value As TreasuryRefundsXpo)
            SetPropertyValue(Of TreasuryRefundsXpo)("IdRefund", fIdRefund, value)
        End Set
    End Property

    Dim fStatus As String
    <Persistent("Status")>
    Public Property Status() As String
        Get
            Select Case fStatus
                Case 1
                    fStatus = ResourceManager.GetString("StateUnconfirmed")
                Case 2
                    fStatus = ResourceManager.GetString("StateConfirmed")
                Case 3
                    fStatus = ResourceManager.GetString("StatusCanceled")
                Case 4
                    fStatus = ResourceManager.GetString("StatusReverse")
            End Select
            Return fStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
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

    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()>
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    'Propiedad Añadida
    Dim fGroupByAccountBankThirdPartyUser As String
    <NonPersistent()>
    Public Property GroupByAccountBankThirdPartyUser() As String
        Get
            Return fGroupByAccountBankThirdPartyUser
        End Get
        Set(ByVal value As String)
            Me.fGroupByAccountBankThirdPartyUser = value
        End Set
    End Property

    'Propiedad Añadida valor total en letras
    Dim fValueLetters As String
    <NonPersistent()>
    Public Property ValueLetters() As String
        Get
            Return fValueLetters
        End Get
        Set(ByVal value As String)
            Me.fValueLetters = value
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceTreasuryVoucherTransactionXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))),
                        SessionValues.Instance.CurrencyISO4217, Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property
#End Region

#Region "Associations"
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesTreasuryVoucherTransactionXpo", GetType(TreasuryVoucherTransactionDetailsXpo))>
    Public ReadOnly Property TreasuryVoucherTransactionDetailsXpo() As XPCollection(Of TreasuryVoucherTransactionDetailsXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetailsXpo)("TreasuryVoucherTransactionDetailsXpo")
        End Get
    End Property
    <Association("TreasurySchedulePaymentDetailXpoReferencesTreasuryVoucherTransactionXpo", GetType(TreasurySchedulePaymentDetailXpo))>
    Public ReadOnly Property TreasurySchedulePaymentDetailXpo() As XPCollection(Of TreasurySchedulePaymentDetailXpo)
        Get
            Return GetCollection(Of TreasurySchedulePaymentDetailXpo)("TreasurySchedulePaymentDetailXpo")
        End Get
    End Property
    <Association("TreasuryCheckCashingXpoReferencesTreasuryVoucherTransactionXpo", GetType(TreasuryCheckCashingXpo))>
    Public ReadOnly Property TreasuryCheckCashingXpo() As XPCollection(Of TreasuryCheckCashingXpo)
        Get
            Return GetCollection(Of TreasuryCheckCashingXpo)("TreasuryCheckCashingXpo")
        End Get
    End Property
    <Association("TreasuryNotesXpoReferencesTreasuryVoucherTransactionXpo", GetType(TreasuryNotesXpo))>
    Public ReadOnly Property TreasuryNotesXpo() As XPCollection(Of TreasuryNotesXpo)
        Get
            Return GetCollection(Of TreasuryNotesXpo)("TreasuryNotesXpo")
        End Get
    End Property
#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region
End Class
