Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.EntityBankAccounts")> _
Public Class TreasuryEntityBankAccountsXpo
    Inherits XPLiteObject

#Region "Properties"

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
    '<Indexed("IX_EntityBankAccount")> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fIdBank As PayrollBankXpo
    <Association("TreasuryEntityBankAccountsXpoReferencesPayrollBankXpo")> _
    Public Property IdBank() As PayrollBankXpo
        Get
            Return fIdBank
        End Get
        Set(ByVal value As PayrollBankXpo)
            SetPropertyValue(Of PayrollBankXpo)("IdBank", fIdBank, value)
        End Set
    End Property
    Dim fIdCity As CommonCityXpo
    <Association("TreasuryEntityBankAccountsXpoReferencesCommonCityXpo")> _
    Public Property IdCity() As CommonCityXpo
        Get
            Return fIdCity
        End Get
        Set(ByVal value As CommonCityXpo)
            SetPropertyValue(Of CommonCityXpo)("IdCity", fIdCity, value)
        End Set
    End Property
    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
        End Set
    End Property
    Dim fNumber As String
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property
    Dim fInitialBalance As Decimal
    Public Property InitialBalance() As Decimal
        Get
            Return fInitialBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialBalance", fInitialBalance, value)
        End Set
    End Property
    Dim fCurrentBalance As Decimal
    Public Property CurrentBalance() As Decimal
        Get
            Return fCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CurrentBalance", fCurrentBalance, value)
        End Set
    End Property
    Dim fRate As Decimal
    Public Property Rate() As Decimal
        Get
            Return fRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Rate", fRate, value)
        End Set
    End Property
    Dim fQuota As Decimal
    Public Property Quota() As Decimal
        Get
            Return fQuota
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Quota", fQuota, value)
        End Set
    End Property
    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("TreasuryEntityBankAccountsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpo
    <Association("TreasuryEntityBankAccountsXpoReferencesPayrollCostCenterXpo")> _
    Public Property IdCostCenter() As PayrollCostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fFinancialSourceId As BudgetFinancialSourceReportXpo
    <Association("Treasury_EntityBankAccountsReferencesBudget_FinancialSource")>
    Public Property FinancialSourceId() As BudgetFinancialSourceReportXpo
        Get
            Return fFinancialSourceId
        End Get
        Set(ByVal value As BudgetFinancialSourceReportXpo)
            SetPropertyValue(Of BudgetFinancialSourceReportXpo)("FinancialSourceId", fFinancialSourceId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("TreasuryEntityBankAccountsXpoReferencesThirdPartyXpo")>
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
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

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceTreasuryEntityBankAccountsXpo")>
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
            Return Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property
#End Region

#Region "Custom Properties"

    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()> _
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property
    <PersistentAlias("concat(Code,' - ', Number)")>
    Public ReadOnly Property CodeNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNumber"))
        End Get
    End Property

    <PersistentAlias("Iif(Type = 1, 'Ahorros', Type = 2, 'Corriente','')")>
    Public ReadOnly Property TypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.ISO4217Xpo.CurrencyName")>
    Public ReadOnly Property CurrencyNameISO() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyNameISO"))
        End Get
    End Property

#End Region

#Region "Navigation Properties"

    <Association("TreasuryCashReceiptsXpoReferencesTreasuryEntityBankAccountsXpo", GetType(TreasuryCashReceiptsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptsXpo() As XPCollection(Of TreasuryCashReceiptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptsXpo)("TreasuryCashReceiptsXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionXpoReferencesTreasuryEntityBankAccountsXpo", GetType(TreasuryVoucherTransactionXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionXpo() As XPCollection(Of TreasuryVoucherTransactionXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionXpo)("TreasuryVoucherTransactionXpo")
        End Get
    End Property
    <Association("TreasuryTreasuryBalanceReferencesTreasuryEntityBankAccountsXpo", GetType(TreasuryTreasuryBalance))> _
    Public ReadOnly Property TreasuryTreasuryBalance() As XPCollection(Of TreasuryTreasuryBalance)
        Get
            Return GetCollection(Of TreasuryTreasuryBalance)("TreasuryTreasuryBalance")
        End Get
    End Property
    <Association("TreasuryPaymentMethodsXpoReferencesTreasuryEntityBankAccountsXpo", GetType(TreasuryPaymentMethodsXpo))> _
    Public ReadOnly Property TreasuryPaymentMethodsXpo() As XPCollection(Of TreasuryPaymentMethodsXpo)
        Get
            Return GetCollection(Of TreasuryPaymentMethodsXpo)("TreasuryPaymentMethodsXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesTreasuryEntityBankAccountsXpo", GetType(TreasuryVoucherTransactionDetailsXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionDetailsXpo() As XPCollection(Of TreasuryVoucherTransactionDetailsXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetailsXpo)("TreasuryVoucherTransactionDetailsXpo")
        End Get
    End Property
    <Association("TreasuryNotesXpoReferencesTreasuryEntityBankAccountsXpo", GetType(TreasuryNotesXpo))> _
    Public ReadOnly Property TreasuryNotesXpo() As XPCollection(Of TreasuryNotesXpo)
        Get
            Return GetCollection(Of TreasuryNotesXpo)("TreasuryNotesXpo")
        End Get
    End Property
    <Association("TreasuryConsignmentXpoReferencesTreasuryEntityBankAccountsXpo", GetType(TreasuryConsignmentXpo))> _
    Public ReadOnly Property TreasuryConsignmentXpo() As XPCollection(Of TreasuryConsignmentXpo)
        Get
            Return GetCollection(Of TreasuryConsignmentXpo)("TreasuryConsignmentXpo")
        End Get
    End Property
    <Association("TreasurySchedulePaymentXpoReferencesTreasuryEntityBankAccountsXpo", GetType(TreasurySchedulePaymentXpo))> _
    Public ReadOnly Property TreasurySchedulePaymentXpo() As XPCollection(Of TreasurySchedulePaymentXpo)
        Get
            Return GetCollection(Of TreasurySchedulePaymentXpo)("TreasurySchedulePaymentXpo")
        End Get
    End Property
    <Association("TreasuryCancellationChecksXpoReferencesTreasuryEntityBankAccountsXpo", GetType(TreasuryCancellationChecksXpo))>
    Public ReadOnly Property TreasuryCancellationChecksXpo() As XPCollection(Of TreasuryCancellationChecksXpo)
        Get
            Return GetCollection(Of TreasuryCancellationChecksXpo)("TreasuryCancellationChecksXpo")
        End Get
    End Property
    <Association("TreasuryBankReconciliation_References_TreasuryEntityBankAccount", GetType(BankReconciliationReportXpo))>
    Public ReadOnly Property BankReconciliationReportXpo() As XPCollection(Of BankReconciliationReportXpo)
        Get
            Return GetCollection(Of BankReconciliationReportXpo)("BankReconciliationReportXpo")
        End Get
    End Property
    <Association("Treasury_ConstitutionCashSmaller_References_Treasury_EntityBankAccounts", GetType(TreasuryConstitutionCashSmallerXpo))>
    Public ReadOnly Property TreasuryConstitutionCashSmallers() As XPCollection(Of TreasuryConstitutionCashSmallerXpo)
        Get
            Return GetCollection(Of TreasuryConstitutionCashSmallerXpo)("TreasuryConstitutionCashSmallers")
        End Get
    End Property

    <Association("TreasuryBankReconciliationAutomatic_References_TreasuryEntityBankAccount", GetType(BankReconciliationAutomaticReportXpo))>
    Public ReadOnly Property BankReconciliationAutomaticReportXpo() As XPCollection(Of BankReconciliationAutomaticReportXpo)
        Get
            Return GetCollection(Of BankReconciliationAutomaticReportXpo)("BankReconciliationAutomaticReportXpo")
        End Get
    End Property

#End Region

#Region "Builders"

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
