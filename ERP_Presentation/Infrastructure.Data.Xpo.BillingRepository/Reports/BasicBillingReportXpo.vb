Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.BasicBilling")>
Public Class BasicBillingReportXpo
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

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fBillingAuthorizationId As BillingAuthorizationReportXpo
    <Association("BasicBilling_References_BillingAuthorization")>
    Public Property BillingAuthorizationId() As BillingAuthorizationReportXpo
        Get
            Return fBillingAuthorizationId
        End Get
        Set(ByVal value As BillingAuthorizationReportXpo)
            SetPropertyValue(Of BillingAuthorizationReportXpo)("BillingAuthorizationId", fBillingAuthorizationId, value)
        End Set
    End Property

    Dim fCustomerId As CustomerXpo
    <Association("BasicBillingReport_References_Customer")>
    Public Property CustomerId() As CustomerXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CustomerXpo)
            SetPropertyValue(Of CustomerXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fAddressId As CommonAddressXpo
    <Association("BasicBilling_References_Address")>
    Public Property AddressId() As CommonAddressXpo
        Get
            Return fAddressId
        End Get
        Set(ByVal value As CommonAddressXpo)
            SetPropertyValue(Of CommonAddressXpo)("AddressId", fAddressId, value)
        End Set
    End Property

    Dim fInvoiceId As BillingVReportInvoice
    <Association("VReportInvoice_References_BasicBilling")>
    Public Property InvoiceId() As BillingVReportInvoice
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As BillingVReportInvoice)
            SetPropertyValue(Of BillingVReportInvoice)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fSaleModality As Byte
    Public Property SaleModality() As Byte
        Get
            Return fSaleModality
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SaleModality", fSaleModality, value)
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

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
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

    Dim fValueDiscount As Decimal
    Public Property ValueDiscount() As Decimal
        Get
            Return fValueDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDiscount", fValueDiscount, value)
        End Set
    End Property

    Dim fValueIVA As Decimal
    Public Property ValueIVA() As Decimal
        Get
            Return fValueIVA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueIVA", fValueIVA, value)
        End Set
    End Property

    Dim fWithholdingTax As Decimal
    Public Property WithholdingTax() As Decimal
        Get
            Return fWithholdingTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingTax", fWithholdingTax, value)
        End Set
    End Property

    Dim fWithholdingIVA As Decimal
    Public Property WithholdingIVA() As Decimal
        Get
            Return fWithholdingIVA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingIVA", fWithholdingIVA, value)
        End Set
    End Property

    Dim fWithholdingICA As Decimal
    Public Property WithholdingICA() As Decimal
        Get
            Return fWithholdingICA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingICA", fWithholdingICA, value)
        End Set
    End Property

    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
        End Set
    End Property

    Dim fThirdPartyEntityCopayId As ThirdPartyXpo
    <Association("BasicBillingReportXpo_References_ThirdPartyEntityCopayId")>
    Public Property ThirdPartyEntityCopayId() As ThirdPartyXpo
        Get
            Return fThirdPartyEntityCopayId
        End Get
        Set(ByVal value As ThirdPartyXpo)
            SetPropertyValue(Of ThirdPartyXpo)("ThirdPartyEntityCopayId", fThirdPartyEntityCopayId, value)
        End Set
    End Property

    Dim fConditionSalesId As ConditionSalesXpo
    <Association("BasicBillingReportXpo_References_ConditionSalesId")>
    Public Property ConditionSalesId() As ConditionSalesXpo
        Get
            Return fConditionSalesId
        End Get
        Set(ByVal value As ConditionSalesXpo)
            SetPropertyValue(Of ConditionSalesXpo)("ConditionSalesId", fConditionSalesId, value)
        End Set
    End Property


    Dim fCreationUser As String
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("BasicBillingReportXpo_References_Common_Currency")>
    Public Property Currency() As CommonCurrencyXpo
        Get
            Return fCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("Currency", fCurrency, value)
        End Set
    End Property

    <PersistentAlias("Currency.Id")>
    Public ReadOnly Property CurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    <PersistentAlias("Currency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    <PersistentAlias("Currency.ISO4217Xpo.CurrencyName")>
    Public ReadOnly Property CurrencyName() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyName"))
        End Get
    End Property

#End Region

#Region "Navigation"

    <Association("BasicBillingDetail_References_BasicBilling", GetType(BasicBillingDetailReportXpo))>
    Public ReadOnly Property BasicBillingDetails() As XPCollection(Of BasicBillingDetailReportXpo)
        Get
            Return GetCollection(Of BasicBillingDetailReportXpo)("BasicBillingDetails")
        End Get
    End Property

    <Association("InvoiceCopay_References_BasicBilling", GetType(BillingInvoiceCopayXpo))>
    Public ReadOnly Property InvoiceCopays() As XPCollection(Of BillingInvoiceCopayXpo)
        Get
            Return GetCollection(Of BillingInvoiceCopayXpo)("InvoiceCopays")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class