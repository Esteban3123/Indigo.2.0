CREATE VIEW [MixingStation].[ViewStickerLabel]
AS

WITH CTE_PharmaInfo AS (
    SELECT pd.GroupingCodeDose,
           psms.FunctionalUnitCode,
           fu.UFUDESCRI,
           hc.IPCODPACI,
           hc.NUMINGRES, 
           fu.UFUCODIGO,
           si.AdministrationName 
    FROM MedicalHistory.PharmaDose pd
    JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
    JOIN HCFARMEPC hc ON pd.IDHCFARMEPC = hc.CODCONCEC
    LEFT JOIN INUNIFUNC fu ON psms.FunctionalUnitCode = fu.UFUCODIGO
    OUTER APPLY (SELECT * FROM MixingStation.GetSyringeInformation(psms.Origin, psms.IdOrigin)) si
    GROUP BY pd.GroupingCodeDose, psms.FunctionalUnitCode, fu.UFUDESCRI, hc.IPCODPACI, hc.NUMINGRES, fu.UFUCODIGO, si.AdministrationName
)

SELECT DISTINCT	rmsd.Id AS Id
		, cd.Id AS CampaignDetailId
		, cd.CampaignNumber
		, ISNULL(pp.Id, p.Id) AS PackageId
		, ISNULL(pp.[Name], p.[Name]) AS PackageName
		, ISNULL(pp.[Description], p.[Description]) AS PackageDescription
		, c.Main
		, c.Vehicle
		, c.Thinner
		, ISNULL(p.ProductId, 0) AS ProductId
		, ISNULL(ipr.SerialNumber, '') AS SerialNumber
		, rpds.BatchCode
		, rpds.Id AS RequestPackageDetailStatusId
		, COALESCE(pp.VolumeTotalPrepared, p.VolumeTotalPrepared, pp.VolumeTotalOrder, p.VolumeTotalOrder, 0)	AS VolumeTotalOrder
		, ISNULL(pp.VolumeTotalOrderMeasurementUnitId, p.VolumeTotalOrderMeasurementUnitId) AS VolumeTotalOrderMeasurementUnitId
		, ISNULL(imu.Abbreviation, imump.Abbreviation)  AS AbbreviationUnitTotalOrder
		, TRIM(pac.IPNOMCOMP) AS PatientName
		, pac.IPCODPACI AS PatientCode
		, pd.UFUDESCRI AS FunctionalUnitName
		, ISNULL(cma.NUMCAMHOS, '') AS Bed
		, pd.AdministrationName
		, rpds.BatchExpirationDate ExpirationDate
		, c.Dose Dosis
		, CAST(NULL AS VARCHAR(20)) DosisAbbreviation
		, IIF(ISNULL(p.Concentration, 0) = 0, p.ConcentrationAntibiotic, CONCAT(p.Concentration,' ', imuc.Abbreviation)) AS Concentration
		, cduQC.Nombre AS QualityChemist
		, cduPC.Nombre AS ProductionChemist
FROM MixingStation.CampaignDetail cd
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.CampaignDetailId = cd.Id
JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.RequestMixingStationDetailId = rmsd.Id
JOIN CTE_PharmaInfo pd  ON pd.GroupingCodeDose = rpds.GroupingCodeDose 
JOIN MixingStation.Package p ON rmsd.PackageId = p.Id
lEFT JOIN MixingStation.PackagePersonalized pp ON rmsd.PackagePersonalizedId = pp.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ISNULL(pp.VolumeTotalOrderMeasurementUnitId, p.VolumeTotalOrderMeasurementUnitId) = imu.Id
LEFT JOIN Inventory.InventoryProduct ipr ON p.ProductId = ipr.Id --PREGUNTAR
LEFT JOIN Inventory.InventoryMeasurementUnit imump ON ISNULL(pp.MeasurementPreparedId, p.MeasurementPreparedId) = imump.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuc ON ISNULL(pp.ConcentrationMeasurementUnitId, p.ConcentrationMeasurementUnitId) = imuc.id
LEFT JOIN INPACIENT pac ON pd.IPCODPACI= pac.IPCODPACI
LEFT JOIN ADINGRESO ing ON pd.NUMINGRES = ing.NUMINGRES
LEFT JOIN CHCAMASHO cma ON (CASE WHEN ing.CODCAMACT IS NULL OR ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END) = cma.CODICAMAS
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC (NOLOCK) ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
OUTER APPLY (
	SELECT *
	FROM (
		SELECT 
			CASE pd.ComponentType
				WHEN 0 THEN concat(pd.Quantity, ' ', mu.Abbreviation)
				WHEN 1 THEN left(a.AbbreviationName, 27)
				WHEN 2 THEN concat(left(s.SupplieName, 27), ' - ', pd.Quantity, '/', mu.Abbreviation)
				WHEN 3 THEN concat(left(pr.Abbreviation, 27), ' - ', pd.Quantity, '/', mu.Abbreviation)
			END AS Item
			, case 				
				WHEN pd.Vehicle = 1 THEN 'Vehicle'
				WHEN pd.Thinner = 1 THEN 'Thinner'
				WHEN pd.ComponentType = 0 THEN 'Dose'
			END AS TypeItem
		FROM (
			SELECT pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, pd.ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
			FROM MixingStation.PackagePersonalizedDetail pd
			WHERE pp.Id IS NOT NULL AND pd.PackagePersonalizedId = pp.Id AND ISNULL(pd.MainMedicine, 0) = 0

			UNION

			SELECT pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, pd.ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
			FROM MixingStation.PackageDetail pd
			WHERE pp.Id IS NULL AND pd.PackageId = p.Id AND ISNULL(pd.MainMedicine, 0) = 0
		) pd
		JOIN Inventory.InventoryMeasurementUnit mu ON pd.MeasurementUnitId = mu.Id
		LEFT JOIN Inventory.ATC a ON pd.AtcId = a.Id
		LEFT JOIN Inventory.InventorySupplie s ON pd.SupplieId = s.Id
		LEFT JOIN Inventory.InventoryProduct pr ON pd.ProductId = pr.Id

		UNION ALL

		SELECT 
			ISNULL(aggMain.Abbreviations, '') AS Item
			, 'Main' AS TypeItem
		FROM (
			SELECT 
				STRING_AGG(
					CASE 
						WHEN a.AbbreviationName IS NOT NULL AND LTRIM(RTRIM(a.AbbreviationName)) <> '' 
						THEN CONCAT(
						left(a.AbbreviationName, 27),
						' ',
						CAST(ISNULL(pdMain.Quantity, 0) AS VARCHAR(20)),
						' ',
						isnull(mu.Abbreviation, '')
						)
						ELSE NULL
					END, 
					' + '
				) WITHIN GROUP (ORDER BY pdMain.Id) AS Abbreviations
			FROM (
				SELECT pd.Id, pd.AtcId, pd.Quantity, pd.MeasurementUnitId
				FROM MixingStation.PackagePersonalizedDetail pd
				WHERE pp.Id IS NOT NULL AND pd.PackagePersonalizedId = pp.Id AND pd.MainMedicine = 1

				UNION ALL

				SELECT pd.Id, pd.AtcId, pd.Quantity, pd.MeasurementUnitId
				FROM MixingStation.PackageDetail pd
				WHERE pp.Id IS NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
			) pdMain
			LEFT JOIN Inventory.ATC a ON pdMain.AtcId = a.Id
			LEFT JOIN Inventory.InventoryMeasurementUnit mu ON pdMain.MeasurementUnitId = mu.Id
		) aggMain

		UNION ALL

		SELECT 
			CONCAT(aggDose.TotalQuantity, ' ', aggDose.FirstUnitAbbreviation) AS Item
			, 'Dose' AS TypeItem
		FROM (
			SELECT 
				SUM(ISNULL(pdDose.Quantity, 0)) AS TotalQuantity,
				MIN(muDose.Abbreviation) AS FirstUnitAbbreviation
			FROM (
				SELECT pd.MeasurementUnitId, pd.Quantity
				FROM MixingStation.PackagePersonalizedDetail pd
				WHERE pp.Id IS NOT NULL AND pd.PackagePersonalizedId = pp.Id AND pd.MainMedicine = 1

				UNION ALL

				SELECT pd.MeasurementUnitId, pd.Quantity
				FROM MixingStation.PackageDetail pd
				WHERE pp.Id IS NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
			) pdDose
			JOIN Inventory.InventoryMeasurementUnit muDose ON pdDose.MeasurementUnitId = muDose.Id
			HAVING SUM(ISNULL(pdDose.Quantity, 0)) > 0
		) aggDose
	) c
	PIVOT
	(
		MAX(Item)
		FOR c.TypeItem IN (Vehicle, Thinner, Main, Dose)
	) piv
) c
WHERE rmsd.Status <> 3 AND rpds.Status <> 6 AND rmsd.LabelType = 3 AND rmsd.Source = 1

UNION ALL
	
	SELECT DISTINCT	rpds.Id AS Id
		, cd.Id AS CampaignDetailId
		, cd.CampaignNumber
		, ISNULL(p.Id, p.Id) AS PackageId
		, ISNULL(p.[Name], p.[Name]) AS PackageName
		, ISNULL(p.[Description], p.[Description]) AS PackageDescription
		, ISNULL(c.Main, a.Name) Main
		, c.Vehicle
		, c.Thinner
		, ISNULL(p.ProductId, 0) AS ProductId
		, ISNULL(ipr.SerialNumber, '') AS SerialNumber
		, rpds.BatchCode
		, rpds.Id AS RequestPackageDetailStatusId
		, COALESCE(p.VolumeTotalPrepared, p.VolumeTotalOrder, 0) AS VolumeTotalOrder
		, p.VolumeTotalOrderMeasurementUnitId
		, ISNULL(imu.Abbreviation, imump.Abbreviation)  AS AbbreviationUnitTotalOrder
		, NULL PatientName
		, NULL PatientCode
		, NULL FunctionalUnitName
		, NULL Bed
		, ad.Name AdministrationName
		, rpds.BatchExpirationDate ExpirationDate
		, c.Dose Dosis
		, CAST(NULL AS VARCHAR(20)) DosisAbbreviation
		, CONCAT(CAST((ISNULL(c.Dose, 0) * 1.0 / NULLIF(COALESCE(p.VolumeTotalPrepared, p.VolumeTotalOrder, 1), 0)) AS DECIMAL(18,4)), CASE WHEN imuc.Abbreviation IS NOT NULL THEN CONCAT(' ', imuc.Abbreviation) ELSE '' END) AS Concentration
		, cduQC.Nombre AS QualityChemist
		, cduPC.Nombre AS ProductionChemist
	FROM MixingStation.RequestUnitDoseInventoryDetail rudid
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.EntityName = 'RequestUnitDoseInventoryDetail' AND rmsd.EntityId = rudid.Id
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.RequestMixingStationDetailId = rmsd.Id
	JOIN MixingStation.CampaignDetail cd ON cd.Id = rmsd.CampaignDetailId
	LEFT JOIN Inventory.ATC a ON a.Id = rudid.ATCId
	LEFT JOIN Inventory.AdministrationRoute ad ON ad.Id = a.AdministrationRouteId
	LEFT JOIN MixingStation.Package p ON p.Id = rudid.PackageId
	LEFT JOIN Inventory.InventoryProduct ipr ON p.ProductId = ipr.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu ON p.VolumeTotalOrderMeasurementUnitId = imu.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imump ON p.MeasurementPreparedId = imump.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imuc ON p.ConcentrationMeasurementUnitId = imuc.id
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC (NOLOCK) ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
	OUTER APPLY (
		SELECT *
		FROM (
			-- Componentes que NO son medicamentos principales (Vehicle, Thinner, Dose, Insumos, otros)
			SELECT 
				CASE pd.ComponentType
					WHEN 0 THEN concat(pd.Quantity, ' ', mu.Abbreviation)
					WHEN 1 THEN left(a.AbbreviationName, 27)
					WHEN 2 THEN concat(left(s.SupplieName, 27), ' - ', pd.Quantity, '/', mu.Abbreviation)
					WHEN 3 THEN concat(left(pr.Abbreviation, 27), ' - ', pd.Quantity, '/', mu.Abbreviation)
				END AS Item
				, case 				
					WHEN pd.Vehicle = 1 THEN 'Vehicle'
					WHEN pd.Thinner = 1 THEN 'Thinner'
					WHEN pd.ComponentType = 0 THEN 'Dose'
				END AS TypeItem
			FROM (
				SELECT pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, pd.ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
				FROM MixingStation.PackageDetail pd
				WHERE p.Id IS NOT NULL AND pd.PackageId = p.Id AND ISNULL(pd.MainMedicine, 0) = 0
			) pd
			JOIN Inventory.InventoryMeasurementUnit mu ON pd.MeasurementUnitId = mu.Id
			LEFT JOIN Inventory.ATC a ON pd.AtcId = a.Id
			LEFT JOIN Inventory.InventorySupplie s ON pd.SupplieId = s.Id
			LEFT JOIN Inventory.InventoryProduct pr ON pd.ProductId = pr.Id

			UNION ALL

			SELECT 
				ISNULL(aggMain.Abbreviations, '') AS Item
				, 'Main' AS TypeItem
			FROM (
				SELECT 
					STRING_AGG(
						CASE 
							WHEN a.AbbreviationName IS NOT NULL AND LTRIM(RTRIM(a.AbbreviationName)) <> '' 
							THEN CONCAT(
							left(a.AbbreviationName, 27),
							' ',
							CAST(ISNULL(pdMain.Quantity, 0) AS VARCHAR(20)),
							' ',
							ISNULL(mu.Abbreviation, '')
							)
							ELSE NULL
						END, 
						' + '
					) WITHIN GROUP (ORDER BY pdMain.Id) AS Abbreviations
				FROM (
					SELECT pd.Id, pd.AtcId, pd.Quantity, pd.MeasurementUnitId
					FROM MixingStation.PackageDetail pd
					WHERE p.Id IS NOT NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
				) pdMain
				LEFT JOIN Inventory.ATC a ON pdMain.AtcId = a.Id
				LEFT JOIN Inventory.InventoryMeasurementUnit mu ON pdMain.MeasurementUnitId = mu.Id
			) aggMain

			UNION ALL

			SELECT 
				CONCAT(aggDose.TotalQuantity, ' ', aggDose.FirstUnitAbbreviation) AS Item
				, 'Dose' AS TypeItem
			FROM (
				SELECT 
					SUM(ISNULL(pdDose.Quantity, 0)) AS TotalQuantity,
					MIN(muDose.Abbreviation) AS FirstUnitAbbreviation
				FROM (
					SELECT pd.MeasurementUnitId, pd.Quantity
					FROM MixingStation.PackageDetail pd
					WHERE p.Id IS NOT NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
				) pdDose
				JOIN Inventory.InventoryMeasurementUnit muDose ON pdDose.MeasurementUnitId = muDose.Id
				HAVING SUM(ISNULL(pdDose.Quantity, 0)) > 0
			) aggDose
		) c
		PIVOT
		(
			MAX(Item)
			FOR c.TypeItem IN (Vehicle, Thinner, Main, Dose)
		) piv
	) c
	WHERE rmsd.Status <> 3 AND rpds.Status <> 6 AND rmsd.LabelType = 3 AND rmsd.Source = 4
																												   
GO