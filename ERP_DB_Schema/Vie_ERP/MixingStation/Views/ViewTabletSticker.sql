

CREATE VIEW [MixingStation].[ViewTabletSticker]
AS
	WITH CTE_Concentration AS(

	SELECT	a.Id,
			IIF(a.ConcentrationQuantity = 0, 
				IIF(a.Concentration = '0', CONCAT(a.ConcentrationQuantity, ' ', mu.Abbreviation), a.Concentration),
				CONCAT(a.ConcentrationQuantity, ' ', mu.Abbreviation)
			) Concentration,
			pf.Name PharmaceuticalForm
	FROM Inventory.ATC a
	JOIN Inventory.PharmaceuticalForm pf ON pf.Id = a.PharmaceuticalFormId
	JOIN Inventory.InventoryMeasurementUnit mu ON a.ConcentrationMeasureUnitId = mu.Id), 
		
	CTE_ProductsCampaignDetail AS 
	(
	SELECT cd.Id CampaignDetailId
		, ip.Id ProductId
		, ip.Code ProductCode
		, ip.Name ProductName
		, ip.HealthRegistration
		, bs.BatchCode ProductBatchCode
		, bs.ExpirationDate
		, a.AbbreviationName ProductAbbreviation
		, a.Id AtcId
		, cdv.ItemType
		, cdv.DeliveredQuantity
	FROM MixingStation.CampaignDetailValidation cdv
	JOIN MixingStation.CampaignDetail cd on cd.Id = cdv.CampaignDetailId
	JOIN Inventory.InventoryProduct ip ON cdv.ProductId = ip.Id
	JOIN Inventory.ATC a ON a.Id = ip.ATCId
	JOIN Inventory.BatchSerial bs ON bs.Id = cdv.BatchSerialId)

SELECT  CONCAT(rpds.Id, '-', cte.ProductId) Id
	, rpds.Id AS RequestPackageDetailStatusId
	, rmsd.CampaignDetailId
	, cte.ProductId
	, cte.ProductCode
	, cte.ProductName
	, cte.ProductAbbreviation
	, c.Concentration AS Dosis
	, 'VIA ORAL' AS AdministrationRoute
	, c.PharmaceuticalForm AS PharmaceuticalForm
	, rpds.BatchCode AS InternalBatchCode
	, cte.ProductBatchCode
	, cd.CreationDate AS CampaignDate
	, rpds.BatchExpirationDate   as ExpirationDate
	, cte.HealthRegistration
	, ud.MSClass as UnitDoseTypeClass
	, cduPA.Nombre AS CreatedBy
	, cduQC.Nombre AS VerifiedBy
FROM  MixingStation.RequestPackageDetailStatus rpds
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.CampaignDetail cd on	 cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.UnitDoseType ud ON ud.Id = cd.UnitDoseTypeId
JOIN CTE_ProductsCampaignDetail cte on cte.CampaignDetailId = cd.Id AND cte.AtcId = rmsd.ATCId AND rmsd.Quantity = cte.DeliveredQuantity
JOIN CTE_Concentration c ON cte.AtcId = c.Id
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPA ON cd.Id = cduPA.CampaignDetailId AND cduPA.UserRole = 3 -- Auxiliar de producción
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 -- QF Calidad 
WHERE rmsd.LabelType = 4 AND cte.ItemType = 1
GO