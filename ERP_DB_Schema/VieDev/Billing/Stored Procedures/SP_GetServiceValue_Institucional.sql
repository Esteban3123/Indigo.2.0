-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-26
-- Description:	Obtener el valor del Servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetServiceValue_Institucional]
	@AdmissionNumber CHAR(10),
	@CenterAttentionCode VARCHAR(20),
	@CupsEntityId Int,
	@IPSServiceId Int,
	@CareGroupId Int,
	@FunctionalUnitId Int,
	@SpecialtyId Char(3),
	@ServiceDate DateTime,
	@PatientGenus Int,
	@PatientDateBirth DateTime,
	@InvoicedQuantity Int,
	@ProfessionalHealthCode Varchar(20),
	@ProfessionalHealthThirdPartyId Int Null,
	@RiasId int = 0,
	@ContractDescriptionId int = 0
AS
BEGIN
	SET NOCOUNT ON

	SELECT	gs.StatusResult,
			gs.MessageResult,
			gs.Id,
			gs.CostCenterId,
			gs.ServiceType,
			gs.CodeNameIpsService,
			gs.CodeNameCups,
			gs.CodeNameFunctionalUnit,
			gs.CodeNameCostCenter,
			gs.AllowValueChange,
			gs.DefinitionRateDetailId,
			gs.DefinitionRateDetailConditionId,
			gs.LiquidationType,
			gs.Presentation,
			gs.IsSOAT,
			gs.RateManualType,
			gs.RateManualId,
			isnull(gs.SubTotalSalesPrice,0) SubTotalSalesPrice,
			isnull(gs.RateManualSalePrice,0) RateManualSalePrice,
			isnull(gs.TotalSalesPrice,0) TotalSalesPrice,
			gs.PerformsHealthProfessionalThirdPartyId,
			isnull(gs.GrandTotalSalesPrice,0) GrandTotalSalesPrice,
			gs.CostValue,
			gs.RecordType,
			gs.CareGroupId,
			gs.CUPSEntityId,
			gs.IPSServiceId,
			gs.InvoicedQuantity,
			gs.ServiceDate,
			gs.PerformsFunctionalUnitId,
			gs.PerformsHealthProfessionalCode,
			gs.PerformsProfessionalSpecialty,
			gs.BillingConceptId,
			gs.SettlementType,
			gs.RateManualDetailId,
			gs.ServiceOrderDetailSurgicalXml,
			gs.IncomeMainAccountId,
			gs.SurgicalInterventionType,
			gs.SurchargeApply,
			gs.RoundService,
			rm.LiquidateAllMIVIE,
			cast(isnull(ips.TaxedProduct, 0) as BIT) TaxedService,
			iif(ips.TaxedProduct = 0 or iva.Id is NULL, 0, ISNULL(iva.Percentage, 0)) TaxPercent,
			ISNULL((SELECT top 1 SalePriceIncludeTax from GeneralLedger.CompanySettings), 1) SalePriceIncludeTax
	FROM [Contract].[GetServiceValue_Institucional]
	(
		@AdmissionNumber, 
		@CenterAttentionCode,
		@CupsEntityId, 
		@IPSServiceId, 
		@CareGroupId, 
		@FunctionalUnitId, 
		@SpecialtyId, 
		@ServiceDate, 
		@PatientGenus, 
		@PatientDateBirth, 
		@InvoicedQuantity, 
		@ProfessionalHealthCode, 
		@ProfessionalHealthThirdPartyId, 
		@RiasId, 
		@ContractDescriptionId
	) gs
	LEFT JOIN Contract.IPSService ips WITH(NOLOCK) on ips.Id=gs.IPSServiceId
	LEFT JOIN Contract.RateManual rm WITH(NOLOCK) on gs.RateManualId =rm.Id
	LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on ips.IVAId =iva.Id
END