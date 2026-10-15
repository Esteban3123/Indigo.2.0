CREATE VIEW  [MixingStation].[ViewDetailsParenteralNutritionLabel]
AS
SELECT 
		CONCAT(npd.ID, '-', ppd.Id) Id,
		npd.IDHCNUTPAREC ParenteralNutritionId,
		a.AbbreviationName AbbreviationAtc,
		npd.CORRPURGACAL Volume,
		ppd.NPTItemOrder,
		ISNULL(a.ComponentType, 0) ComponentType --1- Macro --2 Micro
FROM HCNUTPAREC npc  
JOIN HCNUTPAREND npd  ON npc.ID = npd.IDHCNUTPAREC
JOIN Inventory.ATC a ON npd.CODPRODUC = a.Code
JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.Origin = 'HCNUTPAREC' AND psms.IdOrigin = npc.ID
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON rmsdp.EntityName = 'ProductSusceptibleMixingStation' 
			AND	psms.Id	= rmsdp.EntityId AND rmsdp.PatientCode = npc.IPCODPACI
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsdp.RequestMixingStationDetailId = rmsd.Id
LEFT JOIN MixingStation.PackagePersonalizedDetail ppd ON rmsd.PackagePersonalizedId = ppd.PackagePersonalizedId AND a.Id = ppd.AtcId