Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ViewReportQuotationDetails")>
Public Class BillingViewReportQuotationDetails
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fQuotationId As BillingViewReportQuotation
    <Association("BillingViewReportQuotation_References_BillingViewReportQuotationDetails")>
    Public Property QuotationId() As BillingViewReportQuotation
        Get
            Return fQuotationId
        End Get
        Set(ByVal value As BillingViewReportQuotation)
            SetPropertyValue(Of BillingViewReportQuotation)("QuotationId", fQuotationId, value)
        End Set
    End Property

    Dim fDetailId As Integer
    Public Property DetailId() As Integer
        Get
            Return fDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DetailId", fDetailId, value)
        End Set
    End Property

    Dim fBillingGroup As String
    Public Property BillingGroup() As String
        Get
            Return fBillingGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillingGroup", fBillingGroup, value)
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

    Dim fCUPSCode As String
    Public Property CUPSCode() As String
        Get
            Return fCUPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSCode", fCUPSCode, value)
        End Set
    End Property

    Dim fRIPSCode As String
    Public Property RIPSCode() As String
        Get
            Return fRIPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIPSCode", fRIPSCode, value)
        End Set
    End Property

    Dim fCodeAlternative As String
    Public Property CodeAlternative() As String
        Get
            Return fCodeAlternative
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternative", fCodeAlternative, value)
        End Set
    End Property

    Dim fCodeAlternativeTwo As String
    Public Property CodeAlternativeTwo() As String
        Get
            Return fCodeAlternativeTwo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternativeTwo", fCodeAlternativeTwo, value)
        End Set
    End Property

    Dim fCodeCUM As String
    Public Property CodeCUM() As String
        Get
            Return fCodeCUM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCUM", fCodeCUM, value)
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

    Dim fCUPSName As String
    Public Property CUPSName() As String
        Get
            Return fCUPSName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSName", fCUPSName, value)
        End Set
    End Property

    Dim fRIPSName As String
    Public Property RIPSName() As String
        Get
            Return fRIPSName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIPSName", fRIPSName, value)
        End Set
    End Property

    Dim fContractDescriptionName As String
    Public Property ContractDescriptionName() As String
        Get
            Return fContractDescriptionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionName", fContractDescriptionName, value)
        End Set
    End Property

    Dim fServiceDate As DateTime
    Public Property ServiceDate() As DateTime
        Get
            Return fServiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServiceDate", fServiceDate, value)
        End Set
    End Property

    Dim fAuthorizationNumber As String
    Public Property AuthorizationNumber() As String
        Get
            Return fAuthorizationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationNumber", fAuthorizationNumber, value)
        End Set
    End Property

    Dim fRecordType As Integer
    Public Property RecordType() As Integer
        Get
            Return fRecordType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RecordType", fRecordType, value)
        End Set
    End Property

    Dim fPresentation As Byte
    Public Property Presentation() As Byte
        Get
            Return fPresentation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Presentation", fPresentation, value)
        End Set
    End Property

    Dim fInvoicedQuantity As Integer
    Public Property InvoicedQuantity() As Integer
        Get
            Return fInvoicedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoicedQuantity", fInvoicedQuantity, value)
        End Set
    End Property

    Dim fTotalSalesPrice As Decimal
    Public Property TotalSalesPrice() As Decimal
        Get
            Return fTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPrice", fTotalSalesPrice, value)
        End Set
    End Property

    Dim fThirdPartyDiscount As Decimal
    Public Property ThirdPartyDiscount() As Decimal
        Get
            Return fThirdPartyDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscount", fThirdPartyDiscount, value)
        End Set
    End Property

    Dim fThirdPartySalesPrice As Decimal
    Public Property ThirdPartySalesPrice() As Decimal
        Get
            Return fThirdPartySalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartySalesPrice", fThirdPartySalesPrice, value)
        End Set
    End Property

    Dim fSurgicalId As Nullable(Of Integer)
    Public Property SurgicalId() As Nullable(Of Integer)
        Get
            Return fSurgicalId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Nullable(Of Integer))("SurgicalId", fSurgicalId, value)
        End Set
    End Property

    Dim fCodeSurgical As String
    Public Property CodeSurgical() As String
        Get
            Return fCodeSurgical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeSurgical", fCodeSurgical, value)
        End Set
    End Property

    Dim fNameSurgical As String
    Public Property NameSurgical() As String
        Get
            Return fNameSurgical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameSurgical", fNameSurgical, value)
        End Set
    End Property

    Dim fQuantitySurgical As Integer?
    Public Property QuantitySurgical() As Integer?
        Get
            Return fQuantitySurgical
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("QuantitySurgical", fQuantitySurgical, value)
        End Set
    End Property

    Dim fTotalSalesPriceSurgical As Decimal
    Public Property TotalSalesPriceSurgical() As Decimal
        Get
            Return fTotalSalesPriceSurgical
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPriceSurgical", fTotalSalesPriceSurgical, value)
        End Set
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
