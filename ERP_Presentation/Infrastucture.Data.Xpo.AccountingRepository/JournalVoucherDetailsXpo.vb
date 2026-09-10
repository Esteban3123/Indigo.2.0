Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("GeneralLedger.JournalVoucherDetails")> _
Public Class JournalVoucherDetailsXpo
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
    Dim fIdAccounting As JournalVouchersXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_JournalVouchers")> _
    Public Property IdAccounting() As JournalVouchersXpo
        Get
            Return fIdAccounting
        End Get
        Set(ByVal value As JournalVouchersXpo)
            SetPropertyValue(Of JournalVouchersXpo)("IdAccounting", fIdAccounting, value)
        End Set
    End Property
    Dim fIdMainAccount As MainAccountsXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_MainAccounts")> _
    Public Property IdMainAccount() As MainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesCommon_ThirdParty")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdCostCenter As CostCenterXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesPayroll_CostCenter")> _
    Public Property IdCostCenter() As CostCenterXpo
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As CostCenterXpo)
            SetPropertyValue(Of CostCenterXpo)("IdCostCenter", fIdCostCenter, value)
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
    Dim fIdRetention As RetentionConceptXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_RetentionConcepts")> _
    Public Property IdRetention() As RetentionConceptXpo
        Get
            Return fIdRetention
        End Get
        Set(ByVal value As RetentionConceptXpo)
            SetPropertyValue(Of RetentionConceptXpo)("IdRetention", fIdRetention, value)
        End Set
    End Property
    Dim fRetentionRate As Decimal
    Public Property RetentionRate() As Decimal
        Get
            Return fRetentionRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionRate", fRetentionRate, value)
        End Set
    End Property

    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
        End Set
    End Property
    Dim fBillingValue As Decimal
    Public Property BillingValue() As Decimal
        Get
            Return fBillingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillingValue", fBillingValue, value)
        End Set
    End Property

    'Propiedad Añadida
    Dim fSaldoAnterior As Long
    <NonPersistent()> _
    Public Property SaldoAnterior() As Long
        Get
            Return fSaldoAnterior
        End Get
        Set(ByVal value As Long)
            Me.fSaldoAnterior = value
        End Set
    End Property

    'Propiedad Añadida
    Dim fSaldoCuenta As Long
    <NonPersistent()> _
    Public Property SaldoCuenta() As Long
        Get
            Return fSaldoCuenta
        End Get
        Set(ByVal value As Long)
            Me.fSaldoCuenta = value
        End Set
    End Property

    'Propiedad Añadida
    Dim fSaldoAnteriorPrimerFiltro As Long
    <NonPersistent()> _
    Public Property SaldoAnteriorPrimerFiltro() As Long
        Get
            Return fSaldoAnteriorPrimerFiltro
        End Get
        Set(ByVal value As Long)
            Me.fSaldoAnteriorPrimerFiltro = value
        End Set
    End Property

    'Propiedad Añadida
    Dim fNuevoSaldoPrimerFiltro As Long
    <NonPersistent()> _
    Public Property NuevoSaldoPrimerFiltro() As Long
        Get
            Return fNuevoSaldoPrimerFiltro
        End Get
        Set(ByVal value As Long)
            Me.fNuevoSaldoPrimerFiltro = value
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
