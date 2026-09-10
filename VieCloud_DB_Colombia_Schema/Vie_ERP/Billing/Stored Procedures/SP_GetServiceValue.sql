-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-26
-- Description:	Obtener el valor del Servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetServiceValue]
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
	FROM [Contract].GetServiceValue
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación que calcula y devuelve el valor tarifado de un servicio de salud para un ingreso específico, considerando el contrato vigente, el manual de tarifas aplicable, la unidad funcional, la especialidad, el grupo de atención y las características del paciente (género y fecha de nacimiento). Internamente invoca la función de tabla [Contract].[GetServiceValue] para resolver la tarifa según las reglas contractuales, y enriquece el resultado con información del catálogo de servicios IPS (incluyendo si el servicio es gravado con IVA) y del manual tarifario (tipo de liquidación, redondeo). Devuelve precios subtotal, tarifa manual, total y gran total, junto con metadatos de liquidación necesarios para generar la factura o cuenta de cobro del servicio prestado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetServiceValue';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetServiceValue';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el valor liquidado de un servicio de salud para una admisión, enriqueciéndolo con información tributaria (IVA) y de configuración de precios de venta.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El servicio debe poder ser valorizado por la función Contract.GetServiceValue con los parámetros de admisión, contrato, fecha y profesional; Debe existir configuración en GeneralLedger.CompanySettings (si no existe, se asume SalePriceIncludeTax=1 por defecto)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TaxedService siempre se devuelve como BIT, con 0 cuando IPSService.TaxedProduct es NULL; Los valores monetarios (SubTotalSalesPrice, RateManualSalePrice, TotalSalesPrice, GrandTotalSalesPrice) nunca se devuelven NULL: se sustituyen por 0; Si no hay registro en CompanySettings, SalePriceIncludeTax se asume en 1 (precio incluye impuesto); Solo se toma la primera fila (TOP 1) de CompanySettings como configuración global de la empresa', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'valor de servicio; admisión; manual tarifario; IVA / impuesto al valor agregado; servicio gravado; liquidación; SOAT; centro de costo; unidad funcional; especialidad; RIAS; grupo de atención; profesional de salud; CUPS; servicio IPS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila con el valor del servicio, datos del manual tarifario, indicador de servicio gravado (TaxedService), porcentaje de IVA aplicable y bandera SalePriceIncludeTax.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ips.TaxedProduct = 0 OR iva.Id IS NULL → TaxPercent = 0 (servicio no gravado o sin IVA configurado) else TaxPercent = ISNULL(iva.Percentage, 0) — aplica el porcentaje de IVA del catálogo', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.GetServiceValue; Contract.IPSService; Contract.RateManual; GeneralLedger.GeneralLedgerIVA; GeneralLedger.CompanySettings', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue';
-- GO
