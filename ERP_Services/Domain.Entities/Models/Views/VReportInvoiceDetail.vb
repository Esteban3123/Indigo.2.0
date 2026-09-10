Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class VReportInvoiceDetail
    Public Property Id As String
    Public Property InvoiceId As Integer
    Public Property serviceOrderDetailId As Integer?
    Public Property BillingGroup As String
    Public Property Code As String
    Public Property CUPSCode As String
    Public Property RIPSCode As String
    Public Property CodeAlternative As String
    Public Property CodeAlternativeTwo As String
    Public Property CodeCUM As String
    Public Property Name As String
    Public Property CUPSName As String
    Public Property RIPSName As String
    Public Property ContractDescriptionName As String
    Public Property ServiceDate As Date
    Public Property AuthorizationNumber As String
    Public Property RecordType As Integer
    Public Property Presentation As Byte?
    Public Property DistributionType As Byte
    Public Property InvoicedQuantity As Integer
    Public Property TotalSalesPrice As Decimal?
    Public Property ThirdPartyDiscount As Decimal?
    Public Property SubTotalPatientSalesPrice As Decimal?
    Public Property ThirdPartySalesPrice As Decimal?
    Public Property surgicalId As Integer?
    Public Property CodeSurgical As String
    Public Property NameSurgical As String
    Public Property QuantitySurgical As Integer?
    Public Property TotalSalesPriceSurgical As Decimal?
End Class
