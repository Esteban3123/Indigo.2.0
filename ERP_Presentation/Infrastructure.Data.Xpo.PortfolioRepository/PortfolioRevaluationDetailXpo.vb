
#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Portfolio.RevaluationDetail")>
Public Class PortfolioRevaluationDetailXpo
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

    Dim fRevaluationId As PortfolioRevaluationXpo
    <Association("Portfolio_RevaluationReferences_RevaluationDetail")>
    Public Property RevaluationId() As PortfolioRevaluationXpo
        Get
            Return fRevaluationId
        End Get
        Set(ByVal value As PortfolioRevaluationXpo)
            SetPropertyValue(Of PortfolioRevaluationXpo)("RevaluationId", fRevaluationId, value)
        End Set
    End Property

    Dim fCurrencyId As CommonCurrencyXpo
    <Association("CurrencyReferencePortfolioRevaluationDetailXpo")>
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyConverterId As CommonCurrencyXpo
    <Association("CurrencyReferencePortfolioRevaluationDetailConvertedXpo")>
    Public Property CurrencyConverterId() As CommonCurrencyXpo
        Get
            Return fCurrencyConverterId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyConverterId", fCurrencyConverterId, value)
        End Set
    End Property


    Dim fAccountReceivableId As PortfolioAccountReceivableXpo
    <Association("PortfolioRevaluationDetailReferencesAccountReceivable")>
    Public Property AccountReceivableId() As PortfolioAccountReceivableXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableXpo)
            SetPropertyValue(Of PortfolioAccountReceivableXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property

    Dim fPortfolioAdvanceId As PortfolioAdvanceXpo
    <Association("PortfolioRevaluationDetailReferencesPortfolioAdvance")>
    Public Property PortfolioAdvanceId() As PortfolioAdvanceXpo
        Get
            Return fPortfolioAdvanceId
        End Get
        Set(ByVal value As PortfolioAdvanceXpo)
            SetPropertyValue(Of PortfolioAdvanceXpo)("PortfolioAdvanceId", fPortfolioAdvanceId, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PortfolioRevaluationDetailReferencesThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fMainAccountId As MainAccountsXpo
    <Association("PortfolioRevaluationDetailReferencesMainAccounts")>
    Public Property MainAccountId() As MainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

#End Region

#Region "PersistentAlias"
    <PersistentAlias("iif(DocumentType=1, 'Factura', 'Anticipo')")>
    Public ReadOnly Property DocumentName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentName"))
        End Get
    End Property

    <Size(50)>
    <PersistentAlias("iif(DocumentType=1,AccountReceivableId.InvoiceNumber, PortfolioAdvanceId.Code)")>
    Public ReadOnly Property DocumentNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentNumber"))
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