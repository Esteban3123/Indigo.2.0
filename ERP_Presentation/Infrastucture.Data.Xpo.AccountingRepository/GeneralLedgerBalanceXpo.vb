Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("GeneralLedger.GeneralLedgerBalance")> _
Public Class GeneralLedgerBalanceXpo
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
    Dim fMonth As Integer
    Public Property Month() As Integer
        Get
            Return fMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Month", fMonth, value)
        End Set
    End Property
    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property
    Dim fIdMainAccount As MainAccountsXpo
    <Association("GeneralLedger_GeneralLedgerBalanceReferencesGeneralLedger_MainAccounts")> _
    Public Property IdMainAccount() As MainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("GeneralLedger_GeneralLedgerBalanceReferencesCommon_ThirdParty")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdCostCenter As CostCenterXpo
    <Association("GeneralLedger_GeneralLedgerBalanceReferencesPayroll_CostCenter")> _
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
    'Propiedad Añadida saldo anterior de la cuenta
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
    'Propiedad Añadida saldo anterior del tercero
    Dim fSaldoTercero As Long
    <NonPersistent()> _
    Public Property SaldoTercero() As Long
        Get
            Return fSaldoTercero
        End Get
        Set(ByVal value As Long)
            Me.fSaldoTercero = value
        End Set
    End Property
    'Propiedad Añadida saldo actual de la cuenta
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
    'Propiedad Añadida nuevo saldo seria la suma del saldo anterior + el saldo actual
    Dim fNuevoSaldo As Long
    <NonPersistent()> _
    Public Property NuevoSaldo() As Long
        Get
            Return fNuevoSaldo
        End Get
        Set(ByVal value As Long)
            Me.fNuevoSaldo = value
        End Set
    End Property
    'Propiedad Añadida nuevo saldo seria la suma del saldo anterior + el saldo actual
    Dim fNuevoSaldoCentroCosto As Long
    <NonPersistent()> _
    Public Property NuevoSaldoCentroCosto() As Long
        Get
            Return fNuevoSaldoCentroCosto
        End Get
        Set(ByVal value As Long)
            Me.fNuevoSaldoCentroCosto = value
        End Set
    End Property
    'Propiedad Añadida nuevo saldo seria la suma del saldo anterior + el saldo actual
    Dim fNuevoSaldoTercero As Long
    <NonPersistent()> _
    Public Property NuevoSaldoTercero() As Long
        Get
            Return fNuevoSaldoTercero
        End Get
        Set(ByVal value As Long)
            Me.fNuevoSaldoTercero = value
        End Set
    End Property
    'Propiedad Añadida saldo anterior debito
    Dim fSaldoAnteriorDebito As Long
    <NonPersistent()> _
    Public Property SaldoAnteriorDebito() As Long
        Get
            Return fSaldoAnteriorDebito
        End Get
        Set(ByVal value As Long)
            Me.fSaldoAnteriorDebito = value
        End Set
    End Property
    'Propiedad Añadida saldo anterior credito 
    Dim fSaldoAnteriorCredito As Long
    <NonPersistent()> _
    Public Property SaldoAnteriorCredito() As Long
        Get
            Return fSaldoAnteriorCredito
        End Get
        Set(ByVal value As Long)
            Me.fSaldoAnteriorCredito = value
        End Set
    End Property
    'Propiedad Añadida nuevo saldo debito
    Dim fNuevoSaldoDebito As Long
    <NonPersistent()> _
    Public Property NuevoSaldoDebito() As Long
        Get
            Return fNuevoSaldoDebito
        End Get
        Set(ByVal value As Long)
            Me.fNuevoSaldoDebito = value
        End Set
    End Property
    'Propiedad Añadida nuevo saldo credito 
    Dim fNuevoSaldoCredito As Long
    <NonPersistent()> _
    Public Property NuevoSaldoCredito() As Long
        Get
            Return fNuevoSaldoCredito
        End Get
        Set(ByVal value As Long)
            Me.fNuevoSaldoCredito = value
        End Set
    End Property
    'Propiedad Añadida para guardar si la diferencia entre el saldo anterior y actual es positivo o negativo
    Dim fTypeValue As Integer
    <NonPersistent()> _
    Public Property TypeValue() As Integer
        Get
            Return fTypeValue
        End Get
        Set(ByVal value As Integer)
            Me.fTypeValue = value
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
