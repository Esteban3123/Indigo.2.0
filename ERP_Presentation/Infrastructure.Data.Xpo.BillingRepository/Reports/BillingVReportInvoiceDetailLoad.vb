Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

#Region "Structure"

Public Structure InvoiceDetailKey2

    <Persistent("serviceOrderDetailId")> _
    Public Property serviceOrderDetailId As Integer

    <Persistent("invoiceDetailId")> _
    Public Property invoiceDetailId As Integer

    <Persistent("surgicalId")> _
    Public Property surgicalId As Nullable(Of Integer)

End Structure

#End Region

<Persistent("Billing.VReportInvoiceDetailLoad")> _
Public Class BillingVReportInvoiceDetailLoad
    Inherits XPLiteObject

    <Key(), Persistent()> _
    Public Property Key As InvoiceDetailKey2

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
    Dim fsurgicalId As Nullable(Of Integer)
    Public Property surgicalId() As Nullable(Of Integer)
        Get
            Return fsurgicalId
        End Get
        Set(ByVal value As Nullable(Of Integer))
            SetPropertyValue(Of Nullable(Of Integer))("surgicalId", fsurgicalId, value)
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
    <Size(300)> _
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
    <Size(300)> _
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
    <Size(300)> _
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
    Dim fPresentation As Integer
    Public Property Presentation() As Integer
        Get
            Return fPresentation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Presentation", fPresentation, value)
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
    Dim fRecordType As Integer
    Public Property RecordType() As Integer
        Get
            Return fRecordType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RecordType", fRecordType, value)
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

    Dim fProcessingDate As DateTime
    Public Property ProcessingDate() As DateTime
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property

    Dim fProcessLine As String
    <Size(80)>
    Public Property ProcessLine() As String
        Get
            Return fProcessLine
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProcessLine", fProcessLine, value)
        End Set
    End Property


    Dim fPacientEntity As String
    <Size(80)>
    Public Property PacientEntity() As String
        Get
            Return fPacientEntity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PacientEntity", fPacientEntity, value)
        End Set
    End Property

    Dim fPacientRegimen As String
    <Size(80)>
    Public Property PacientRegimen() As String
        Get
            Return fPacientRegimen
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PacientRegimen", fPacientRegimen, value)
        End Set
    End Property

    Dim fAffiliateType As String
    <Size(80)>
    Public Property AffiliateType() As String
        Get
            Return fAffiliateType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AffiliateType", fAffiliateType, value)
        End Set
    End Property

    Dim fAffiliateStatus As String
    <Size(80)>
    Public Property AffiliateStatus() As String
        Get
            Return fAffiliateStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AffiliateStatus", fAffiliateStatus, value)
        End Set
    End Property

    Dim fERPConfirm As String
    <Size(80)>
    Public Property ERPConfirm() As String
        Get
            Return fERPConfirm
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ERPConfirm", fERPConfirm, value)
        End Set
    End Property

    Dim fIPSReport As String
    <Size(80)>
    Public Property IPSReport() As String
        Get
            Return fIPSReport
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSReport", fIPSReport, value)
        End Set
    End Property

    Dim fAutorizacionIngreso As String
    <Size(15)>
    Public Property AutorizacionIngreso() As String
        Get
            Return fAutorizacionIngreso
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AutorizacionIngreso", fAutorizacionIngreso, value)
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
