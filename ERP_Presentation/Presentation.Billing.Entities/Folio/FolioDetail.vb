Imports Presentation.Billing.Entities

Public Class FolioDetail
    Implements IFolioDetail

    Public Sub New()
        MyBase.New()
    End Sub
    Public Property Id As Integer Implements IFolioDetail.Id

    Public Property ServiceOrderId As Integer Implements IFolioDetail.ServiceOrderId

    Public Property ServiceOrderCode As String Implements IFolioDetail.ServiceOrderCode

    Public Property ServiceOrderDetailId As Integer Implements IFolioDetail.ServiceOrderDetailId

    Public Property SurgeryNumber As Byte Implements IFolioDetail.SurgeryNumber

    Public Property PerformsFunctionalUnitCodeName As String Implements IFolioDetail.PerformsFunctionalUnitCodeName

    Public Property CostCenterCodeName As String Implements IFolioDetail.CostCenterCodeName

    Public Property AuthorizationNumber As String Implements IFolioDetail.AuthorizationNumber

    Public Property PerformsHealthProfessionalCode As String Implements IFolioDetail.PerformsHealthProfessionalCode

    Public Property PerformsProfessionalSpecialty As String Implements IFolioDetail.PerformsProfessionalSpecialty

    Public Property ServiceDate As DateTime Implements IFolioDetail.ServiceDate

    Public Property RecordType As Byte Implements IFolioDetail.RecordType

    Public Property Presentation As Byte? Implements IFolioDetail.Presentation

    Public Property SettlementType As Byte Implements IFolioDetail.SettlementType

    Public Property DistributionType As Byte Implements IFolioDetail.DistributionType

    Public Property InvoicedQuantity As Integer Implements IFolioDetail.InvoicedQuantity

    Public Property SupplyQuantity As Integer Implements IFolioDetail.SupplyQuantity

    Public Property DevolutionQuantity As Integer Implements IFolioDetail.DevolutionQuantity

    Public Property CostValue As Decimal Implements IFolioDetail.CostValue

    Public Property RateManualSalePrice As Decimal Implements IFolioDetail.RateManualSalePrice

    Public Property GrandTotalSalesPrice As Decimal Implements IFolioDetail.GrandTotalSalesPrice

    Public Property SubTotalSalesPrice As Decimal Implements IFolioDetail.SubTotalSalesPrice

    Public Property ThirdPartyDiscount As Decimal Implements IFolioDetail.ThirdPartyDiscount

    Public Property GrandTotalDiscount As Decimal Implements IFolioDetail.GrandTotalDiscount

    Public Property ThirdPartyDiscountPercentage As Decimal Implements IFolioDetail.ThirdPartyDiscountPercentage

    Public Property TotalSalesPrice As Decimal Implements IFolioDetail.TotalSalesPrice

    Public Property ThirdPartySalesPrice As Decimal Implements IFolioDetail.ThirdPartySalesPrice

    Public Property ThirdPartyPercentage As Decimal Implements IFolioDetail.ThirdPartyPercentage

    Public Property SurchargeApply As String Implements IFolioDetail.SurchargeApply

    Public Property RecoveryFeeType As Byte Implements IFolioDetail.RecoveryFeeType

    Public Property ApplyRecoveryFee As Byte Implements IFolioDetail.ApplyRecoveryFee

    Public Property SubTotalPatientSalesPrice As Decimal Implements IFolioDetail.SubTotalPatientSalesPrice

    Public Property PatientPercentage As Decimal Implements IFolioDetail.PatientPercentage

    Public Property IsPackage As Boolean Implements IFolioDetail.IsPackage

    Public Property CodeAssociateService As String Implements IFolioDetail.CodeAssociateService

    Public Property GuidHomologation As String Implements IFolioDetail.GuidHomologation

    Public Property ApplyRIAS As Boolean Implements IFolioDetail.ApplyRIAS

    Public Property RIASCupsId As Integer Implements IFolioDetail.RIASCupsId

    Public Property AllowValueChange As Boolean Implements IFolioDetail.AllowValueChange

    Public Property ServiceCode As String Implements IFolioDetail.ServiceCode

    Public Property ServiceName As String Implements IFolioDetail.ServiceName

    Public Property IPSServiceCode As String Implements IFolioDetail.IPSServiceCode

    Public Property IPSServiceName As String Implements IFolioDetail.IPSServiceName

    Public Property CUPS As String Implements IFolioDetail.CUPS

    Public Property ContractDescriptionCodeName As String Implements IFolioDetail.ContractDescriptionCodeName

    Public Property ServiceBillingGroupCodeName As String Implements IFolioDetail.ServiceBillingGroupCodeName

    Public Property ProductId As Integer Implements IFolioDetail.ProductId

    Public Property ProductCode As String Implements IFolioDetail.ProductCode

    Public Property ProductName As String Implements IFolioDetail.ProductName

    Public Property IsPOSProduct As Boolean? Implements IFolioDetail.IsPOSProduct

    Public Property ProductBillingGroupCodeName As String Implements IFolioDetail.ProductBillingGroupCodeName

    Public Property ProductATCCode As String Implements IFolioDetail.ProductATCCode

    Public Property HasPathologies As Boolean Implements IFolioDetail.HasPathologies

    Public Property IPSServiceGroupCodeName As String Implements IFolioDetail.IPSServiceGroupCodeName

    Property MiPres As String Implements IFolioDetail.MiPres

    Property IdMiPres As String Implements IFolioDetail.IdMiPres

    Property IsItemProduction As Boolean Implements IFolioDetail.IsItemProduction

    Property IconType As Byte Implements IFolioDetail.IconType

    Property FlagProductServiceDetail As Byte Implements IFolioDetail.FlagProductServiceDetail

    Property ProductLiquidationType As Byte Implements IFolioDetail.ProductLiquidationType

    Public Property DistributionTypeName As String Implements IFolioDetail.DistributionTypeName
        Get
            Select Case DistributionType
                Case 1
                    Return "Ninguno"
                Case 2
                    Return "Distribución Normal"
                Case 3
                    Return "Distribución por Corte de Cuentas"
                Case 4
                    Return "Distribución por Unidad"
                Case 5
                    Return "Ninguno NoPOS"
            End Select

            Return ""
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property CurrencyAbbreviation As String Implements IFolioDetail.CurrencyAbbreviation

    Public Property VoucherValue As Decimal Implements IFolioDetail.VoucherValue

    Public Property ThirdPartyResponsibleQuotaNitName As String Implements IFolioDetail.ThirdPartyResponsibleQuotaNitName

    Public Property PatientQuotaResponsibleThirdPartyId As Integer? Implements IFolioDetail.PatientQuotaResponsibleThirdPartyId

    Public Property GrandTotalTaxes As Decimal Implements IFolioDetail.GrandTotalTaxes

    Public Property GrossValue As Decimal Implements IFolioDetail.GrossValue

    Public Property IvaPercentage As String Implements IFolioDetail.IvaPercentage

    Public Property FeeNotCollectedId As Integer? Implements IFolioDetail.FeeNotCollectedId

    Public Property BasicbillingCopayId As Integer? Implements IFolioDetail.BasicbillingCopayId

End Class
