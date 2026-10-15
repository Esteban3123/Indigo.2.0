



CREATE VIEW [MixingStation].[ViewParenteralNutritionLabelComponents]
AS
	SELECT DISTINCT	
		npd.ID Id
		, npd.IDHCNUTPAREC ParenteralNutritionId
		, CONCAT (RTRIM(npd.CODPRODUC), ' - ', RTRIM(atc.[Name])) AS ProductCode
		, npd.NAMENUT AS NutritionName
		, TRY_CAST(ISNULL(npd.APORTENUT, 0) AS DECIMAL(18,2)) Request
		, TRY_CAST(ISNULL(npd.VOLUMENCAL, 0) AS DECIMAL(18,2)) Volume
		, ppd.NPTItemOrder
	FROM HCNUTPAREND npd
	JOIN HCNUTPAREC npc ON npc.ID = npd.IDHCNUTPAREC
	JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.IdOrigin = npc.ID
	JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON psms.Id = rmsdp.EntityId AND rmsdp.PatientCode = npc.IPCODPACI
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rmsdp.RequestMixingStationDetailId
	JOIN Inventory.ATC atc ON npd.CODPRODUC = atc.Code
	JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rmsd.PackagePersonalizedId AND atc.Id = ppd.AtcId