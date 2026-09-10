Imports DevExpress.Xpo

<Persistent("Billing.VReportInvoiceDetail")>
Public Class BillingVReportInvoiceDetail
    Inherits XPLiteObject

#Region "Properties"

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

    Dim fInvoiceId As BillingVReportInvoice
    <Association("VReportInvoice_References_VReportInvoiceDetail")>
    Public Property InvoiceId() As BillingVReportInvoice
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As BillingVReportInvoice)
            SetPropertyValue(Of BillingVReportInvoice)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fServiceOrderDetailId As Integer
    Public Property ServiceOrderDetailId() As Integer
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderDetailId", fServiceOrderDetailId, value)
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

    Dim fContractDescriptionCode As String
    Public Property ContractDescriptionCode() As String
        Get
            Return fContractDescriptionCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionCode", fContractDescriptionCode, value)
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

    Dim fPresentation As Integer?
    Public Property Presentation() As Integer?
        Get
            Return fPresentation
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("Presentation", fPresentation, value)
        End Set
    End Property

    Dim fDistributionType As Byte
    Public Property DistributionType() As Byte
        Get
            Return fDistributionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DistributionType", fDistributionType, value)
        End Set
    End Property

    Dim fMeasuryUnit As String
    Public Property MeasuryUnit() As String
        Get
            Return fMeasuryUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasuryUnit", fMeasuryUnit, value)
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

    Dim fThirdPartyDiscount As Decimal
    Public Property ThirdPartyDiscount() As Decimal
        Get
            Return fThirdPartyDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscount", fThirdPartyDiscount, value)
        End Set
    End Property

    Dim fSurgicalId As Nullable(Of Integer)
    Public Property SurgicalId() As Nullable(Of Integer)
        Get
            Return fSurgicalId
        End Get
        Set(ByVal value As Nullable(Of Integer))
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

    Dim fTotalSalesPriceSurgical As Decimal?
    Public Property TotalSalesPriceSurgical() As Decimal?
        Get
            Return fTotalSalesPriceSurgical
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TotalSalesPriceSurgical", fTotalSalesPriceSurgical, value)
        End Set
    End Property

    Dim fCodeMipres As String
    Public Property CodeMipres() As String
        Get
            Return fCodeMipres
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeMipres", fCodeMipres, value)
        End Set
    End Property

    Dim fIdMipres As String
    Public Property IdMipres() As String
        Get
            Return fIdMipres
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdMipres", fIdMipres, value)
        End Set
    End Property

    Dim fIvaPercentage As String
    Public Property IvaPercentage() As String
        Get
            Return fIvaPercentage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IvaPercentage", fIvaPercentage, value)
        End Set
    End Property

    Dim fIvaTotalValue As Decimal
    Public Property IvaTotalValue() As Decimal
        Get
            Return fIvaTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaTotalValue", fIvaTotalValue, value)
        End Set
    End Property

    Dim fGrossValue As Decimal
    Public Property GrossValue() As Decimal
        Get
            Return fGrossValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal?)("GrossValue", fGrossValue, value)
        End Set
    End Property

    Dim fNetWorth As Decimal
    Public Property NetWorth() As Decimal
        Get
            Return fNetWorth
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NetWorth", fNetWorth, value)
        End Set
    End Property

    Dim fNetUnitValue As Decimal
    Public Property NetUnitValue() As Decimal
        Get
            Return fNetUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NetUnitValue", fNetUnitValue, value)
        End Set
    End Property

    Dim fGrandTotalSalesPrice As Decimal
    Public Property GrandTotalSalesPrice() As Decimal
        Get
            Return fGrandTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrandTotalSalesPrice", fGrandTotalSalesPrice, value)
        End Set
    End Property

    Dim fGrandTotalDiscount As Decimal
    Public Property GrandTotalDiscount() As Decimal
        Get
            Return fGrandTotalDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrandTotalDiscount", fGrandTotalDiscount, value)
        End Set
    End Property

    Dim fGrossSubValue As Decimal
    Public Property GrossSubValue() As Decimal
        Get
            Return fGrossSubValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrossSubValue", fGrossSubValue, value)
        End Set
    End Property

    Dim fGrossUnitValue As Decimal
    Public Property GrossUnitValue() As Decimal
        Get
            Return fGrossUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrossUnitValue", fGrossUnitValue, value)
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
