Public Interface IFolioDetail

    Property Id As Integer

    Property ServiceOrderId As Integer

    Property ServiceOrderCode As String

    Property ServiceOrderDetailId As Integer

    Property SurgeryNumber As Byte

    Property PerformsFunctionalUnitCodeName As String

    Property CostCenterCodeName As String

    Property AuthorizationNumber As String

    Property PerformsHealthProfessionalCode As String

    Property PerformsProfessionalSpecialty As String

    Property ServiceDate As DateTime

    Property RecordType As Byte

    Property Presentation As Byte?

    Property SettlementType As Byte

    Property DistributionType As Byte

    Property InvoicedQuantity As Integer

    Property SupplyQuantity As Integer

    Property DevolutionQuantity As Integer

    Property CostValue As Decimal

    Property RateManualSalePrice As Decimal

    Property GrandTotalSalesPrice As Decimal

    Property SubTotalSalesPrice As Decimal

    Property ThirdPartyDiscount As Decimal

    Property GrandTotalDiscount As Decimal

    Property ThirdPartyDiscountPercentage As Decimal

    Property TotalSalesPrice As Decimal

    Property ThirdPartySalesPrice As Decimal

    Property ThirdPartyPercentage As Decimal

    Property SurchargeApply As String

    Property RecoveryFeeType As Byte

    Property ApplyRecoveryFee As Byte

    Property SubTotalPatientSalesPrice As Decimal

    Property PatientPercentage As Decimal

    Property IsPackage As Boolean

    Property CodeAssociateService As String

    Property GuidHomologation As String

    Property ApplyRIAS As Boolean

    Property RIASCupsId As Integer

    Property AllowValueChange As Boolean

    Property ServiceCode As String

    Property ServiceName As String

    Property IPSServiceCode As String

    Property IPSServiceName As String

    Property CUPS As String

    Property ContractDescriptionCodeName As String

    Property ServiceBillingGroupCodeName As String

    Property ProductId As Integer

    Property ProductCode As String

    Property ProductName As String

    Property IsPOSProduct As Boolean?

    Property ProductBillingGroupCodeName As String

    Property ProductATCCode As String

    Property HasPathologies As Boolean

    Property IPSServiceGroupCodeName As String

    Property MiPres As String

    Property IdMiPres As String

    Property IsItemProduction As Boolean

    Property IconType As Byte

    Property FlagProductServiceDetail As Byte

    Property ProductLiquidationType As Byte

    Property DistributionTypeName As String

    Property VoucherValue As Decimal

    Property ThirdPartyResponsibleQuotaNitName As String

    Property PatientQuotaResponsibleThirdPartyId As Integer?

    Property CurrencyAbbreviation As String

    Property GrandTotalTaxes As Decimal

    Property GrossValue As Decimal
    Property IvaPercentage As String

    ''' <summary>
    ''' Id Sin recaudo de cuota
    ''' </summary>
    ''' <returns></returns>
    Property FeeNotCollectedId As Integer?

    ''' <summary>
    ''' Id de la factura basica 
    ''' </summary>
    ''' <returns></returns>
    Property BasicbillingCopayId As Integer?
End Interface
