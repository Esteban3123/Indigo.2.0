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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación que calcula y retorna el valor tarifado de un servicio de salud institucional para un ingreso específico. Dado el número de admisión, el servicio de la IPS, la fecha de prestación, los datos del paciente (sexo y fecha de nacimiento), la cantidad facturada, el profesional de salud y el contrato, consulta la función de valorización contractual [Contract].[GetServiceValue_Institucional] y enriquece el resultado con información del catálogo de servicios propios ([Contract].[IPSService]) y el manual tarifario aplicable ([Contract].[RateManual]), incluyendo el tipo de liquidación, precios de venta (subtotal, total y gran total), costo, tipo de registro y centro de costo. Adicionalmente calcula si el servicio aplica IVA y el porcentaje correspondiente según la configuración contable ([GeneralLedger].[GeneralLedgerIVA]), y si el precio de venta incluye impuesto según los parámetros de la empresa. Se utiliza en el proceso de valorización y prefacturación de servicios ambulatorios, hospitalarios y de urgencias prestados por la institución.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetServiceValue_Institucional';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetServiceValue_Institucional';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el valor liquidado de un servicio institucional para una admisión, enriqueciéndolo con datos de IVA, manual tarifario y configuración de precios con/sin impuesto.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La admisión, servicio IPS, entidad CUPS y unidad funcional referenciados deben existir para que la función Contract.GetServiceValue_Institucional devuelva resultado válido; Debe existir al menos un registro en GeneralLedger.CompanySettings para resolver SalePriceIncludeTax (de lo contrario se asume 1)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'SubTotalSalesPrice, RateManualSalePrice, TotalSalesPrice y GrandTotalSalesPrice nunca se devuelven NULL: se sustituyen por 0; TaxedService siempre se entrega como BIT (0/1) basado en ips.TaxedProduct; Solo aplica IVA cuando el servicio IPS está marcado como gravado y tiene IVAId asociado válido', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Valorización de servicios de salud; Admisión; Servicio IPS; CUPS; Manual tarifario; IVA; Precio con/sin impuesto incluido; Unidad funcional; Grupo de atención; Profesional de la salud; RIAS; Liquidación institucional', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve una fila con la valorización del servicio (precios, tipo de liquidación, tarifa, profesional, IVA aplicable e indicador de precio con impuesto incluido)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ips.TaxedProduct = 0 OR iva.Id IS NULL → TaxPercent = 0 (servicio no gravado o sin IVA asociado) else TaxPercent = ISNULL(iva.Percentage, 0) (toma el porcentaje del IVA configurado al servicio); si No existe registro en GeneralLedger.CompanySettings → SalePriceIncludeTax se asume 1 (precio incluye impuesto por defecto) else Se toma el valor configurado en CompanySettings', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.GetServiceValue_Institucional', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.GetServiceValue_Institucional; Contract.IPSService; Contract.RateManual; GeneralLedger.GeneralLedgerIVA; GeneralLedger.CompanySettings', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetServiceValue_Institucional';
-- GO
