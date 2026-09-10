Imports DevExpress.Xpo

<Persistent("Treasury.VReportCashBook")> _
Public Class TreasuryVReportCashBookXpo
    Inherits XPLiteObject

#Region "Properties"

    Dim fRow As String
    <Key(True)>
    Public Property Row() As String
        Get
            Return fRow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Row", fRow, value)
        End Set
    End Property

    Dim fNameVoucher As String
    Public Property NameVoucher() As String
        Get
            Return fNameVoucher
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameVoucher", fNameVoucher, value)
        End Set
    End Property

    Dim fCashRegisterId As Integer?
    Public Property CashRegisterId() As Integer?
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property

    Dim fCashRegisterCode As String
    Public Property CashRegisterCode() As String
        Get
            Return fCashRegisterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CashRegisterCode", fCashRegisterCode, value)
        End Set
    End Property

    Dim fCashRegisterName As String
    Public Property CashRegisterName() As String
        Get
            Return fCashRegisterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CashRegisterName", fCashRegisterName, value)
        End Set
    End Property

    Dim fCashRegisterStatus As Boolean
    Public Property CashRegisterStatus() As Boolean
        Get
            Return fCashRegisterStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CashRegisterStatus", fCashRegisterStatus, value)
        End Set
    End Property

    Dim fThirdPartyNit As String
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fDetail As String
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Dim fVoucherType As Integer
    Public Property VoucherType() As Integer
        Get
            Return fVoucherType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VoucherType", fVoucherType, value)
        End Set
    End Property

    Dim fPaymentMethod As Integer
    Public Property PaymentMethod() As Integer
        Get
            Return fPaymentMethod
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PaymentMethod", fPaymentMethod, value)
        End Set
    End Property

    Dim fValueDebit As Decimal
    Public Property ValueDebit() As Decimal
        Get
            Return fValueDebit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDebit", fValueDebit, value)
        End Set
    End Property

    Dim fValueCredit As Decimal
    Public Property ValueCredit() As Decimal
        Get
            Return fValueCredit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueCredit", fValueCredit, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Dim fCodeNameUser As String
    Public Property CodeNameUser() As String
        Get
            Return fCodeNameUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeNameUser", fCodeNameUser, value)
        End Set
    End Property

    Dim fIdBankAccount As Integer?
    Public Property IdBankAccount() As Integer?
        Get
            Return fIdBankAccount
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IdBankAccount", fIdBankAccount, value)
        End Set
    End Property

    Dim fBankAccountCode As String
    Public Property BankAccountCode() As String
        Get
            Return fBankAccountCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankAccountCode", fBankAccountCode, value)
        End Set
    End Property

    Dim fBankAccountName As String
    Public Property BankAccountName() As String
        Get
            Return fBankAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankAccountName", fBankAccountName, value)
        End Set
    End Property

    Dim fBankAccountStatus As Boolean
    Public Property BankAccountStatus() As Boolean
        Get
            Return fBankAccountStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("BankAccountStatus", fBankAccountStatus, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

#End Region

#Region "Custom Properties"

    'columna que devuelve el numero y nombre de la caja
    Dim fCodeNameCash As String
    <PersistentAlias("concat(concat(CashRegisterCode,' - '),CashRegisterName)")>
    Public ReadOnly Property CodeNameCash() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNameCash"))
        End Get
    End Property

    'columna que devuelve el nit y nombre del tercero
    Dim fNitNameThird As String
    <PersistentAlias("concat(concat(ThirdPartyNit,' - '),ThirdPartyName)")>
    Public ReadOnly Property NitNameThird() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitNameThird"))
        End Get
    End Property

    'Propiedad Añadida
    Dim fSaldoAnterior As Decimal
    <NonPersistent()>
    Public Property SaldoAnterior() As Decimal
        Get
            Return fSaldoAnterior
        End Get
        Set(ByVal value As Decimal)
            Me.fSaldoAnterior = value
        End Set
    End Property

    'Propiedad Añadida
    Dim fNuevoSaldo As Decimal
    <NonPersistent()>
    Public Property NuevoSaldo() As Decimal
        Get
            Return fNuevoSaldo
        End Get
        Set(ByVal value As Decimal)
            Me.fNuevoSaldo = value
        End Set
    End Property

    'columna que devuelve el numero y nombre de la caja
    Dim fCodeNameBank As String
    <PersistentAlias("concat(BankAccountCode,' - ',BankAccountName)")>
    Public ReadOnly Property CodeNameBank() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNameBank"))
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
