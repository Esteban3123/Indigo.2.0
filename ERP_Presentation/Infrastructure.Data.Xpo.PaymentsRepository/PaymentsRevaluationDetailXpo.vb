Imports DevExpress.Xpo

<Persistent("Payments.PaymentsRevaluationDetail")>
Public Class PaymentsRevaluationDetailXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Size(20)>
    <Persistent("Value")>
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fDocumentType As Byte
    <Persistent("DocumentType")>
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Dim fValueCurrency As Decimal
    Public Property ValueCurrency() As Decimal
        Get
            Return fValueCurrency
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueCurrency", fValueCurrency, value)
        End Set
    End Property

    Dim fValueCurrencyReverse As Decimal
    Public Property ValueCurrencyReverse() As Decimal
        Get
            Return fValueCurrencyReverse
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueCurrencyReverse", fValueCurrencyReverse, value)
        End Set
    End Property

    Dim fActualValueCurrency As Decimal
    Public Property ActualValueCurrency() As Decimal
        Get
            Return fActualValueCurrency
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ActualValueCurrency", fActualValueCurrency, value)
        End Set
    End Property

    Dim fActualValueCurrencyReverse As Decimal
    Public Property ActualValueCurrencyReverse() As Decimal
        Get
            Return fActualValueCurrencyReverse
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ActualValueCurrencyReverse", fActualValueCurrencyReverse, value)
        End Set
    End Property

    Dim fBalanceConverted As Decimal
    Public Property BalanceConverted() As Decimal
        Get
            Return fBalanceConverted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceConverted", fBalanceConverted, value)
        End Set
    End Property

    Dim fActualBalanceConverter As Decimal
    Public Property ActualBalanceConverter() As Decimal
        Get
            Return fActualBalanceConverter
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ActualBalanceConverter", fActualBalanceConverter, value)
        End Set
    End Property

    Dim fProfitLostValue As Decimal
    Public Property ProfitLostValue() As Decimal
        Get
            Return fProfitLostValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProfitLostValue", fProfitLostValue, value)
        End Set
    End Property

    Dim fPaymentsRevaluationId As PaymentsRevaluationXpo
    <Association("Payments_PaymentsRevaluationReferences_PaymentsRevaluationDetail")>
    Public Property PaymentsRevaluationId() As PaymentsRevaluationXpo
        Get
            Return fPaymentsRevaluationId
        End Get
        Set(ByVal value As PaymentsRevaluationXpo)
            SetPropertyValue(Of PaymentsRevaluationXpo)("RevaluationId", fPaymentsRevaluationId, value)
        End Set
    End Property

    Dim fCurrencyId As CommonCurrencyXpo
    <Association("CurrencyReferencePaymentsRevaluationDetailXpo")>
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyConverterId As CommonCurrencyXpo
    <Association("CurrencyReferencePaymentsRevaluationDetailConvertedXpo")>
    Public Property CurrencyConverterId() As CommonCurrencyXpo
        Get
            Return fCurrencyConverterId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyConverterId", fCurrencyConverterId, value)
        End Set
    End Property

    Dim fAccountPayableId As AccountPayableXpo
    <Association("PaymentsRevaluationDetailReferencesAccountPayable")>
    Public Property AccountPayableId() As AccountPayableXpo
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As AccountPayableXpo)
            SetPropertyValue(Of AccountPayableXpo)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PaymentsRevaluationDetailReferencesThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PaymentsRevaluationDetailReferencesMainAccounts")>
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fAdvancePayment As AdvancePaymentsXpo
    <Persistent("AdvancePaymentId")>
    <Association("PaymentsRevaluationDetailReferencesAdvancePayments")>
    Public Property AdvancePayment() As AdvancePaymentsXpo
        Get
            Return fAdvancePayment
        End Get
        Set(ByVal value As AdvancePaymentsXpo)
            SetPropertyValue("AdvancePayment", fAdvancePayment, value)
        End Set
    End Property

    Dim fDeferredCausationId As Integer?
    Public Property DeferredCausationId() As Integer?
        Get
            Return fDeferredCausationId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("DeferredCausationId", fDeferredCausationId, value)
        End Set
    End Property

#End Region

#Region "PersistentAlias"

    <PersistentAlias("iif(DocumentType=1, 'Factura', DocumentType=2, 'Anticipo','Diferido')")>
    Public ReadOnly Property DocumentName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentName"))
        End Get
    End Property

    <Size(50)>
    <PersistentAlias("iif(DocumentType=2,AdvancePayment.Code,AccountPayableId.Code)")>
    Public ReadOnly Property DocumentNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentNumber"))
        End Get
    End Property

    <PersistentAlias("AdvancePayment.Id")>
    Public ReadOnly Property AdvancePaymentId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("AdvancePaymentId"))
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
