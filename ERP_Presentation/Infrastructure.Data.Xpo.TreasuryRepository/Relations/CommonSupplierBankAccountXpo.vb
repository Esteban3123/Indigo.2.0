Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Base

<Persistent("Common.SupplierBankAccount")>
Partial Public Class SupplierBankAccountXpo
    Inherits XPLiteObject

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

    Dim fSupplierId As Integer
    <PersistentAlias("SupplierReportId.Id")>
    Public Property SupplierId() As Integer
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierId", fSupplierId, value)
        End Set
    End Property

    Dim fSupplierReportId As CommonSuplierReportXpo
    <Persistent("SupplierId")>
    <Association("CommonSuplierReportXpo_References_SupplierBankAccountXpo")>
    Public Property SupplierReportId() As CommonSuplierReportXpo
        Get
            Return fSupplierReportId
        End Get
        Set(ByVal value As CommonSuplierReportXpo)
            SetPropertyValue("SupplierReportId", fSupplierReportId, value)
        End Set
    End Property

    Dim fBank As PayrollBankXpo
    <Persistent("BankId")>
    <Association("TreasuryPaymentMethodsXpo_References_SupplierBankAccountXpo")>
    Public Property Bank() As PayrollBankXpo
        Get
            Return fBank
        End Get
        Set(ByVal value As PayrollBankXpo)
            SetPropertyValue(Of PayrollBankXpo)("Bank", fBank, value)
        End Set
    End Property

    <PersistentAlias("Bank.Id")>
    Public ReadOnly Property BankId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("BankId"))
        End Get
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

    Dim fPaymentDefault As Boolean
    Public Property PaymentDefault() As Boolean
        Get
            Return fPaymentDefault
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PaymentDefault", fPaymentDefault, value)
        End Set
    End Property
    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PaymentDefault", fState, value)
        End Set
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("Currency_Reference_SupplierBankAccountXpo")>
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

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return If(Convert.ToInt32(EvaluateAlias("CurrencyId")) = 0,
                        SessionValues.Instance.OfficialCurrencyId, Convert.ToInt32(EvaluateAlias("CurrencyId")))
        End Get
    End Property

#End Region

#Region "Navigation"
    <Association("TreasuryVoucheDetailsReferencesSupplierBankAccount", GetType(TreasuryVoucherTransactionDetailsXpo))>
    Public ReadOnly Property TreasuryVoucherTransactionDetailsXpo() As XPCollection(Of TreasuryVoucherTransactionDetailsXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionDetailsXpo)("TreasuryVoucherTransactionDetailsXpo")
        End Get
    End Property

    <Association("TreasuryVoucherTransactionXpoReferencesCommonSupplierBankAccountXpo", GetType(TreasuryVoucherTransactionXpo))>
    Public ReadOnly Property TreasuryVoucherTransactionXpo() As XPCollection(Of TreasuryVoucherTransactionXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionXpo)("TreasuryVoucherTransactionXpo")
        End Get
    End Property
#End Region

#Region "PersistentAlias"
    <PersistentAlias("IIF(Type=1,'Ahorro','Corriente')")>
    Public ReadOnly Property TypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

    <PersistentAlias("CONCAT(Bank.Code,' - ', Bank.Name)")>
    Public ReadOnly Property BankCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("BankCodeName"))
        End Get
    End Property

    <PersistentAlias("Bank.Name")>
    Public ReadOnly Property BankName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("BankName"))
        End Get
    End Property

    <PersistentAlias("CONCAT(Number,' - ', Bank.Name)")>
    Public ReadOnly Property BankAccountName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("BankAccountName"))
        End Get
    End Property
#End Region
End Class