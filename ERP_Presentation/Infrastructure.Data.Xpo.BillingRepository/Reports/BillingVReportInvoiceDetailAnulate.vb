Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

#Region "Structure"

Public Structure InvoiceDetailAnulateKey

    <Persistent("serviceOrderDetailId")> _
    Public Property serviceOrderDetailId As Integer

    <Persistent("invoiceDetailId")> _
    Public Property invoiceDetailId As Integer

    <Persistent("surgicalId")> _
    Public Property surgicalId As Nullable(Of Integer)

End Structure

#End Region

<Persistent("Billing.VReportInvoiceDetailAnulate")> _
Public Class BillingVReportInvoiceDetailAnulate
    Inherits XPLiteObject

    <Key(), Persistent()> _
    Public Property Key As InvoiceDetailAnulateKey

    Dim fId As Integer
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Size(15)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fserviceOrderDetailId As Integer
    Public Property serviceOrderDetailId() As Integer
        Get
            Return fserviceOrderDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("serviceOrderDetailId", fserviceOrderDetailId, value)
        End Set
    End Property
    Dim finvoiceDetailId As Integer
    Public Property invoiceDetailId() As Integer
        Get
            Return finvoiceDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("invoiceDetailId", finvoiceDetailId, value)
        End Set
    End Property
    Dim fsurgicalId As Integer
    Public Property surgicalId() As Integer
        Get
            Return fsurgicalId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("surgicalId", fsurgicalId, value)
        End Set
    End Property
    Dim fBillingGroup As String
    <Size(123)> _
    Public Property BillingGroup() As String
        Get
            Return fBillingGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillingGroup", fBillingGroup, value)
        End Set
    End Property
    Dim fCUPSCode As String
    <Size(20)> _
    Public Property CUPSCode() As String
        Get
            Return fCUPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSCode", fCUPSCode, value)
        End Set
    End Property
    Dim fCUPSName As String
    <Size(94)> _
    Public Property CUPSName() As String
        Get
            Return fCUPSName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSName", fCUPSName, value)
        End Set
    End Property
    Dim fCode As String
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
    <Size(94)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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
    Dim fSubTotalPatientSalesPrice As Decimal
    Public Property SubTotalPatientSalesPrice() As Decimal
        Get
            Return fSubTotalPatientSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalPatientSalesPrice", fSubTotalPatientSalesPrice, value)
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
    Dim fCodeSurgical As String
    <Size(20)> _
    Public Property CodeSurgical() As String
        Get
            Return fCodeSurgical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeSurgical", fCodeSurgical, value)
        End Set
    End Property
    Dim fNameSurgical As String
    <Size(200)> _
    Public Property NameSurgical() As String
        Get
            Return fNameSurgical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameSurgical", fNameSurgical, value)
        End Set
    End Property
    Dim fQuantitySurgical As Integer
    Public Property QuantitySurgical() As Integer
        Get
            Return fQuantitySurgical
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("QuantitySurgical", fQuantitySurgical, value)
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
    Dim fThirdPartyDiscount As Decimal
    Public Property ThirdPartyDiscount() As Decimal
        Get
            Return fThirdPartyDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscount", fThirdPartyDiscount, value)
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
    Dim fCodeAlternative As String
    <Size(30)> _
    Public Property CodeAlternative() As String
        Get
            Return fCodeAlternative
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternative", fCodeAlternative, value)
        End Set
    End Property
    Dim fCodeAlternativeTwo As String
    <Size(20)> _
    Public Property CodeAlternativeTwo() As String
        Get
            Return fCodeAlternativeTwo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternativeTwo", fCodeAlternativeTwo, value)
        End Set
    End Property
    Dim fCodeCUM As String
    <Size(20)> _
    Public Property CodeCUM() As String
        Get
            Return fCodeCUM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCUM", fCodeCUM, value)
        End Set
    End Property
    Dim fRecordType As Byte
    Public Property RecordType() As Byte
        Get
            Return fRecordType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RecordType", fRecordType, value)
        End Set
    End Property
    Dim fAuthorizationNumber As String
    <Size(20)>
    Public Property AuthorizationNumber() As String
        Get
            Return fAuthorizationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationNumber", fAuthorizationNumber, value)
        End Set
    End Property
    Dim fRIPSCode As String
    <Size(20)>
    Public Property RIPSCode() As String
        Get
            Return fRIPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIPSCode", fRIPSCode, value)
        End Set
    End Property
    Dim fRIPSName As String
    <Size(300)>
    Public Property RIPSName() As String
        Get
            Return fRIPSName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIPSName", fRIPSName, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
