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