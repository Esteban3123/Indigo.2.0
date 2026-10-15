CREATE VIEW  [MixingStation].[ViewDetailsParenteralNutritionLabel]
AS

--Medicamentos de la plantilla de nutrición parenteral, se muestran los macro y micro nutrientes, con su orden en la etiqueta, el volumen a administrar y su tipo de componente
SELECT 
		ppd.Id Id,
		npd.IDHCNUTPAREC ParenteralNutritionId,
		atc.AbbreviationName AbbreviationAtc,
		npd.CORRPURGACAL Volume,
		ppd.NPTItemOrder,
		ISNULL(atc.ComponentType, 0) ComponentType --1- Macro --2 Micro
FROM HCNUTPAREND npd
JOIN HCNUTPAREC npc ON npc.ID = npd.IDHCNUTPAREC
JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.IdOrigin = npc.ID
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON psms.Id = rmsdp.EntityId AND rmsdp.PatientCode = npc.IPCODPACI
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rmsdp.RequestMixingStationDetailId
JOIN Inventory.ATC atc ON npd.CODPRODUC = atc.Code
JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rmsd.PackagePersonalizedId AND atc.Id = ppd.AtcId AND npd.CORRPURGACAL = ppd.Quantity
WHERE ppd.ComponentType NOT IN (4,5)

UNION ALL

--Medicamentos adicionados en el paquete de referencia, identificados como 'Medicamento adicional NPT'
SELECT 
		ppd.Id Id,
		psms.IdOrigin ParenteralNutritionId,
		atc.AbbreviationName AbbreviationAtc,
		ppd.Quantity Volume,
		ppd.NPTItemOrder,
		ISNULL(atc.ComponentType, 0) ComponentType --1- Macro --2 Micro
FROM MixingStation.PackagePersonalizedDetail ppd
JOIN Inventory.ATC atc ON ppd.AtcId = atc.Id
JOIN MixingStation.RequestMixingStationDetail rmsd on rmsd.PackagePersonalizedId = ppd.PackagePersonalizedId
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp on rmsdp.RequestMixingStationDetailId = rmsd.Id
JOIN MedicalHistory.ProductSusceptibleMixingStation psms on psms.Id = rmsdp.EntityId AND psms.Origin = 'HCNUTPAREC'
WHERE ppd.ComponentType IN (4,5)

