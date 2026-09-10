-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 08/11/2016
-- Description:	SP para exportar los datos de la distribucion de activos fijos
-- =============================================
CREATE PROCEDURE [Cost].[SP_ExportExcelCostDistributionFixedAsset]
	@Year int,
	@Month int
AS
BEGIN
	SET NOCOUNT ON;

	/******************************************************* RESULTADO *******************************************************/

	SELECT 
		fapa.Plate, 
		fai.Code FixedAssetItemCode, fai.Description FixedAssetItemDescription, 		
		fal.Code FixedAssetLocationCode, fal.Name FixedAssetLocationName,
		tp.Nit ThirdPartyNit, tp.Name ThirdPartyName,
		cpc.Code ProductionCenterCode, cpc.Name ProductionCenterName,
		CAST(faddc.DepreciatedDays * fal.UseTime AS INT) HoursQuantity,
		faddc.DepreciationValue
	FROM FixedAsset.FixedAssetDepreciation fad
	JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
	JOIN GeneralLedger.LegalBook lb ON lb.Id = fadd.LegalBookId AND lb.OfficialBook = 1
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fadd.FixedAssetPhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetDepreciationDetailCost faddc ON faddc.FixedAssetDepreciationDetailId = fadd.Id
	JOIN FixedAsset.FixedAssetLocation fal ON faddc.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetResponsible far ON faddc.ResponsibleId = far.Id
	JOIN Common.ThirdParty tp ON far.ThirdPartyId = tp.Id
	LEFT JOIN Cost.CostProductionCenterCostCenter cpccc ON faddc.CostCenterId = cpccc.CostCenterId
	LEFT JOIN Cost.CostProductionCenter cpc ON cpccc.ProductionCenterId = cpc.Id
	WHERE fad.ClosingYear = @Year AND fad.ClosingMonth = @Month 
		AND fad.Status = 2 AND faddc.DepreciationValue > 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exporta a Excel la distribución de costos de depreciación de activos fijos para un año y mes específicos, mostrando únicamente registros con valor de depreciación mayor a cero y con estado de cierre confirmado (Status=2) en el libro oficial contable. Para cada activo fijo, consolida información de la placa física del bien, código y descripción del tipo de activo, ubicación o sede donde se encuentra, responsable del activo (con su NIT y nombre como tercero), centro de producción al que se asigna el costo, cantidad de horas de uso calculadas como días depreciados multiplicados por el tiempo de uso de la ubicación, y el valor de depreciación distribuido. Sirve como insumo para el proceso de costos hospitalarios, permitiendo asignar el gasto por depreciación de activos fijos a los centros de producción correspondientes y analizar la carga de costos indirectos por sede y responsable en un período contable de cierre mensual.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte exportable a Excel con la distribución de costos de depreciación de activos fijos para un período contable, incluyendo placa, ubicación, responsable y centro de producción.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un cierre de depreciación con ClosingYear y ClosingMonth coincidentes y Status = 2 (cerrado/oficial).; Debe existir un LegalBook marcado como OfficialBook = 1 asociado al detalle de depreciación.; Los detalles de costo de depreciación deben tener LocationId y ResponsibleId válidos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las horas reportadas se calculan como DepreciatedDays * UseTime de la ubicación, truncadas a entero.; Solo se reporta depreciación del libro oficial (un único libro por detalle).; Nunca se incluyen depreciaciones con valor cero o negativo.; Solo se exportan depreciaciones cerradas (Status=2) del período solicitado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Depreciación; Placa de activo; Libro contable oficial; Ubicación del activo; Responsable del activo; Tercero; Centro de costo; Centro de producción; Cierre contable mensual', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado de consulta: Cuando fad.ClosingYear = @Year AND fad.ClosingMonth = @Month AND fad.Status = 2 AND faddc.DepreciationValue > 0, devuelve filas con placa, ítem, ubicación, tercero responsable, centro de producción, horas (DepreciatedDays * UseTime) y valor de depreciación.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si lb.OfficialBook = 1 → Solo se consideran detalles de depreciación asociados al libro contable oficial.; si fad.Status = 2 → Solo se incluyen procesos de depreciación con estado 2 (cerrado/oficial).; si faddc.DepreciationValue > 0 → Se excluyen registros sin valor depreciado positivo.; si Existencia de relación CostCenter↔ProductionCenter (LEFT JOIN) → Si hay relación se muestra el centro de producción; de lo contrario se devuelve NULL en esos campos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetDepreciationDetailCost; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetResponsible; Common.ThirdParty; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionFixedAsset';
-- GO
