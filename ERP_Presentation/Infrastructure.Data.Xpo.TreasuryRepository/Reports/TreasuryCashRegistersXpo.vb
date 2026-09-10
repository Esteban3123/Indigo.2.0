Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CashRegisters")> _
Public Class TreasuryCashRegistersXpo
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
    '<Indexed("IX_CashRegister")> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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
    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
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
    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fRefundDate As DateTime
    Public Property RefundDate() As DateTime
        Get
            Return fRefundDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RefundDate", fRefundDate, value)
        End Set
    End Property
    Dim fAmountMax As Decimal
    Public Property AmountMax() As Decimal
        Get
            Return fAmountMax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountMax", fAmountMax, value)
        End Set
    End Property
    Dim fAmountMin As Decimal
    Public Property AmountMin() As Decimal
        Get
            Return fAmountMin
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountMin", fAmountMin, value)
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
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("TreasuryCashRegistersXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As PayrollCostCenterXpo
    <Association("TreasuryCashRegistersXpoReferencesPayrollCostCenterXpo")>
    Public Property IdCostCenter() As PayrollCostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("CashRegistersXpoReferencesThirdPartyXpo")>
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fIsMovement As Boolean
    Public Property IsMovement() As Boolean
        Get
            Return fIsMovement
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMovement", fIsMovement, value)
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
    <Association("CurrencyReferenceTreasuryCashRegistersXpo")>
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

    <PersistentAlias("CommonCurrency.ISO4217Xpo.CurrencyName")>
    Public ReadOnly Property CurrencyNameISO() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyNameISO"))
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
    <PersistentAlias("concat(Code,' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property
    <PersistentAlias("Iif(Type = 1, 'Caja Menor', Type = 2, 'Caja Mayor', '')")>
    Public ReadOnly Property TypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

#End Region

#Region "Navigation Properties"

    <Association("TreasuryCashReceiptsXpoReferencesTreasuryCashRegistersXpo", GetType(TreasuryCashReceiptsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptsXpo() As XPCollection(Of TreasuryCashReceiptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptsXpo)("TreasuryCashReceiptsXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionXpoReferencesTreasuryCashRegistersXpo", GetType(TreasuryVoucherTransactionXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionXpo() As XPCollection(Of TreasuryVoucherTransactionXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionXpo)("TreasuryVoucherTransactionXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionDetailsXpoReferencesTreasuryCashRegistersXpo", GetType(TreasuryVoucherTransactionDetailsXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionDetailsXpo() As XPCollection(Of TreasuryVoucherTransactionDetailsXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetailsXpo)("TreasuryVoucherTransactionDetailsXpo")
        End Get
    End Property
    <Association("TreasuryTreasuryBalanceReferencesTreasuryCashRegistersXpo", GetType(TreasuryTreasuryBalance))> _
    Public ReadOnly Property TreasuryTreasuryBalance() As XPCollection(Of TreasuryTreasuryBalance)
        Get
            Return GetCollection(Of TreasuryTreasuryBalance)("TreasuryTreasuryBalance")
        End Get
    End Property
    <Association("TreasuryExpenseConceptCashRegistersXpoReferencesTreasuryCashRegistersXpo", GetType(TreasuryExpenseConceptCashRegistersXpo))> _
    Public ReadOnly Property TreasuryExpenseConceptCashRegistersXpo() As XPCollection(Of TreasuryExpenseConceptCashRegistersXpo)
        Get
            Return GetCollection(Of TreasuryExpenseConceptCashRegistersXpo)("TreasuryExpenseConceptCashRegistersXpo")
        End Get
    End Property
    <Association("TreasuryNotesXpoReferencesTreasuryCashRegistersXpo", GetType(TreasuryNotesXpo))> _
    Public ReadOnly Property TreasuryNotesXpo() As XPCollection(Of TreasuryNotesXpo)
        Get
            Return GetCollection(Of TreasuryNotesXpo)("TreasuryNotesXpo")
        End Get
    End Property
    <Association("TreasuryConsignmentDetailXpoReferencesTreasuryCashRegistersXpo", GetType(TreasuryConsignmentDetailXpo))> _
    Public ReadOnly Property TreasuryConsignmentDetailXpo() As XPCollection(Of TreasuryConsignmentDetailXpo)
        Get
            Return GetCollection(Of TreasuryConsignmentDetailXpo)("TreasuryConsignmentDetailXpo")
        End Get
    End Property
    <Association("TreasuryRefundsXpoReferencesTreasuryCashRegistersXpo", GetType(TreasuryRefundsXpo))>
    Public ReadOnly Property TreasuryRefundsXpo() As XPCollection(Of TreasuryRefundsXpo)
        Get
            Return GetCollection(Of TreasuryRefundsXpo)("TreasuryRefundsXpo")
        End Get
    End Property
    <Association("Treasury_ConstitutionCashSmaller_References_Treasury_CashRegisters_By_CashRegisterSmallerId", GetType(TreasuryConstitutionCashSmallerXpo))>
    Public ReadOnly Property TreasuryConstitutionCashSmallerRegistersXpo() As XPCollection(Of TreasuryConstitutionCashSmallerXpo)
        Get
            Return GetCollection(Of TreasuryConstitutionCashSmallerXpo)("TreasuryConstitutionCashSmallerRegistersXpo")
        End Get
    End Property
    <Association("Treasury_ConstitutionCashSmaller_References_Treasury_CashRegisters_By_CashRegisterId", GetType(TreasuryConstitutionCashSmallerXpo))>
    Public ReadOnly Property TreasuryConstitutionCashRegistersXpo() As XPCollection(Of TreasuryConstitutionCashSmallerXpo)
        Get
            Return GetCollection(Of TreasuryConstitutionCashSmallerXpo)("TreasuryConstitutionCashRegistersXpo")
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
