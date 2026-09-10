-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date:	2019-09-18
-- Description:	Obtiene los activos fijos a distribuir
-- =============================================
CREATE PROCEDURE [Cost].[SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId]
	@Year INT,
	@Month INT,
	@PhysicalAssetId INT
AS
BEGIN
	SET NOCOUNT ON;

	/******************************************************* RESULTADO *******************************************************/

    SELECT 
		fapa.Id, fapa.Plate,
		CONCAT(fai.Code, ' - ', fai.Description) FixedAssetItemCodeDescription,
		cpc.Id ProductionCenterId, CONCAT(cpc.Code, ' - ' , cpc.Name) ProductionCenterCodeName,
		SUM(CAST(faddc.DepreciatedDays * fal.UseTime AS INT)) HoursQuantity,
		SUM(faddc.DepreciationValue) DepreciationValue
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
		AND fapa.Id = ISNULL(@PhysicalAssetId, fapa.Id)
	GROUP BY fapa.Id, fapa.Plate, 
		fai.Code, fai.Description,
		cpc.Id, cpc.Code, cpc.Name
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene los datos de distribución de depreciación de activos fijos para un año, mes y activo físico específicos, con el fin de apoyar el proceso de distribución de costos en contabilidad de costos. Consolida información del ciclo de depreciación oficial (libro legal oficial) cruzando el activo físico con su ítem, ubicación, responsable y centro de costo, calculando las horas de uso (días depreciados por tiempo de uso en la ubicación) y el valor de depreciación acumulado. Relaciona cada activo con su centro de producción a través de la tabla puente entre centros de costo y centros de producción, permitiendo conocer qué centros de producción absorben el costo de depreciación de cada activo fijo en el período de cierre indicado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los activos fijos físicos depreciados en un período (año/mes) cerrado, agregando horas de uso y valor de depreciación por activo y centro de producción, para fines de distribución de costos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un proceso de depreciación cerrado (Status = 2) para el año y mes solicitados; Debe existir un libro contable marcado como oficial; Los detalles de costo de depreciación deben tener ubicación, responsable y tercero asociados', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran depreciaciones cuyo cierre tenga estado = 2 (cerrado/aprobado); Solo se toma información del libro contable oficial (OfficialBook = 1); Se excluyen detalles de costo con valor de depreciación menor o igual a cero; Las horas se calculan como días depreciados multiplicados por el tiempo de uso de la ubicación, truncado a entero; La agregación se hace por activo físico y centro de producción; La relación con centro de producción es opcional (LEFT JOIN): un activo sin centro de producción asociado igual aparece', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Depreciación; Cierre contable mensual; Libro oficial; Centro de costo; Centro de producción; Responsable de activo; Ubicación de activo; Placa de activo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.FixedAssetDepreciationDetailCost: Cuando ClosingYear = @Year, ClosingMonth = @Month, Status = 2, OfficialBook = 1, DepreciationValue > 0 y (activo coincide con @PhysicalAssetId o este es NULL) → retorna agregado de horas (DepreciatedDays * UseTime) y valor depreciado por activo físico y centro de producción', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Parámetro de activo físico es NULL → No filtra por activo: incluye todos los activos físicos del período else Restringe el resultado al activo físico indicado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetDepreciationDetailCost; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetResponsible; Common.ThirdParty; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId';
-- GO
