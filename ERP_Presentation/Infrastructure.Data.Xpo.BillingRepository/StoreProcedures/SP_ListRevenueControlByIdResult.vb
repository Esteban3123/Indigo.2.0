Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<NonPersistent()> _
Public Class SP_ListRevenueControlByIdResult
    Public Property Row As Long
    Public Property Id As Integer
    Public Property RevenueControlDetailId As Integer
    Public Property FolioOrder As Byte
    Public Property FolioType As Byte
    Public Property ContractEntityCodeName As String
    Public Property HealthAdministratorCodeName As String
    Public Property ThirdPartyNitName As String
    Public Property CareGroupCodeName As String
    Public Property TotalFolio As Decimal
    Public Property TotalSalesPrice1 As Decimal
    Public Property DistributionType As Byte
    Public Property ThirdPartySalesPrice As Decimal
    Public Property ThirdPartyPercentage As Decimal
    Public Property ResponsibleRecoveryFee As Byte
    Public Property SubTotalPatientSalesPrice As Decimal
    Public Property PatientPercentage As Decimal
    Public Property PatientDiscountPercentage As Decimal
    Public Property PatientDiscount As Decimal
    Public Property TotalPatientSalesPrice As Decimal
    Public Property IPSServiceCodeName As String
    Public Property ProductCodeName As String
    Public Property ServiceBillingGroupCodeName As String
    Public Property ProductBillingGroupCodeName As String
    Public Property InvoicedQuantity As Integer
    Public Property SupplyQuantity As Integer
    Public Property DevolutionQuantity As Integer
    Public Property RateManualSalePrice As Decimal
    Public Property RecoveryFeeType As Byte
    Public Property CostValue As Decimal
    Public Property ServiceDate As DateTime
    Public Property AuthorizationNumber As String
    Public Property PerformsFunctionalUnitCodeName As String
    Public Property PerformsHealthProfessionalCode As String
    Public Property PerformsProfessionalSpecialty As String
    Public Property IPSServiceGroupCodeName As String
    Public Property CostCenterCodeName As String
    Public Property SubTotalSalesPrice As Decimal
    Public Property ThirdPartyDiscount As Decimal
    Public Property ThirdPartyDiscountPercentage As Decimal
    Public Property TotalSalesPrice2 As Decimal
End Class