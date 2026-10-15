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