Imports DevExpress.Xpo

<Persistent("Billing.ViewListBasicbillingForImport")>
Public Class ViewListBasicBillingForImportXpo
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

    Dim fDocumentCode As String
    <Size(20)>
    Public Property DocumentCode() As String
        Get
            Return fDocumentCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentCode", fDocumentCode, value)
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

    Dim fCustomerId As Integer
    Public Property CustomerId() As Integer
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fCustomerCodeName As String
    Public Property CustomerCodeName() As String
        Get
            Return fCustomerCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerCodeName", fCustomerCodeName, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
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

    Dim fSaleModalityName As String
    Public Property SaleModalityName() As String
        Get
            Return fSaleModalityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SaleModalityName", fSaleModalityName, value)
        End Set
    End Property

    Dim fCurrencyId As Integer?
    Public Property CurrencyId() As Integer?
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    <Size(5)>
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

    Dim fHeaderId As Integer?
    Public Property HeaderId() As Integer?
        Get
            Return fHeaderId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("HeaderId", fHeaderId, value)
        End Set
    End Property

    Dim fSelected As Boolean
    Public Property Selected() As Boolean
        Get
            Return fSelected
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("Selected", fSelected, value)
        End Set
    End Property

    Dim fDetailId As Integer?
    Public Property DetailId() As Integer?
        Get
            Return fDetailId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("DetailId", fDetailId, value)
        End Set
    End Property

    Dim fDetailType As Byte
    Public Property DetailType() As Byte
        Get
            Return fDetailType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DetailType", fDetailType, value)
        End Set
    End Property

    Dim fDetailTypeName As String
    Public Property DetailTypeName() As String
        Get
            Return fDetailTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DetailTypeName", fDetailTypeName, value)
        End Set
    End Property

    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
        End Set
    End Property

    Dim fItemCodeName As String
    Public Property ItemCodeName() As String
        Get
            Return fItemCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeName", fItemCodeName, value)
        End Set
    End Property

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fUnitValue As Decimal
    Public Property UnitValue() As Decimal
        Get
            Return fUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitValue", fUnitValue, value)
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

    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
        End Set
    End Property

    Dim fBasicBillingId As Integer
    Public Property BasicBillingId() As Integer
        Get
            Return fBasicBillingId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BasicBillingId", fBasicBillingId, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class