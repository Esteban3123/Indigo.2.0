

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
    FROM MedicalHistory.PharmaDose pd WITH (NOLOCK)
    JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH (NOLOCK) ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
    JOIN HCFARMEPC hc WITH (NOLOCK) ON pd.IDHCFARMEPC = hc.CODCONCEC
    LEFT JOIN INUNIFUNC fu WITH (NOLOCK) ON psms.FunctionalUnitCode = fu.UFUCODIGO
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
		, ROUND(COALESCE(pp.VolumeTotalPrepared, p.VolumeTotalPrepared, pp.VolumeTotalOrder, p.VolumeTotalOrder, 0), 0)	AS VolumeTotalOrder
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
		, IIF(COALESCE(pp.Concentration, p.Concentration, 0) = 0, ISNULL(pp.ConcentrationAntibiotic, p.ConcentrationAntibiotic), CONCAT(ISNULL(pp.Concentration, p.Concentration),' ', imuc.Abbreviation)) AS Concentration
		, cduQC.Nombre AS QualityChemist
		, cduPC.Nombre AS ProductionChemist
FROM MixingStation.CampaignDetail cd WITH(NOLOCK)
JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.CampaignDetailId = cd.Id
JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rpds.RequestMixingStationDetailId = rmsd.Id
JOIN CTE_PharmaInfo pd  ON pd.GroupingCodeDose = rpds.GroupingCodeDose 
JOIN MixingStation.Package p WITH(NOLOCK) ON rmsd.PackageId = p.Id
lEFT JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) ON rmsd.PackagePersonalizedId = pp.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON ISNULL(pp.VolumeTotalOrderMeasurementUnitId, p.VolumeTotalOrderMeasurementUnitId) = imu.Id
LEFT JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON p.ProductId = ipr.Id --PREGUNTAR
LEFT JOIN Inventory.InventoryMeasurementUnit imump WITH(NOLOCK) ON ISNULL(pp.MeasurementPreparedId, p.MeasurementPreparedId) = imump.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuc WITH(NOLOCK) ON ISNULL(pp.ConcentrationMeasurementUnitId, p.ConcentrationMeasurementUnitId) = imuc.id
LEFT JOIN INPACIENT pac WITH(NOLOCK) ON pd.IPCODPACI= pac.IPCODPACI
LEFT JOIN ADINGRESO ing WITH(NOLOCK) ON pd.NUMINGRES = ing.NUMINGRES
LEFT JOIN CHCAMASHO cma WITH(NOLOCK) ON (CASE WHEN ing.CODCAMACT IS NULL OR ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END) = cma.CODICAMAS
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
				ELSE 'Main' 
			END AS TypeItem
		FROM (
			SELECT pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, pd.ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
			FROM MixingStation.PackagePersonalizedDetail pd WITH(NOLOCK)
			WHERE pp.Id IS NOT NULL AND pd.PackagePersonalizedId = pp.Id

			UNION

			SELECT TOP 1 pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, 0 ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
			FROM MixingStation.PackagePersonalizedDetail pd WITH(NOLOCK)
			WHERE pp.Id IS NOT NULL AND pd.PackagePersonalizedId = pp.Id AND pd.MainMedicine = 1

			UNION

			SELECT pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, pd.ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
			FROM MixingStation.PackageDetail pd WITH(NOLOCK)
			WHERE pp.Id IS NULL AND pd.PackageId = p.Id
			
			UNION

			SELECT TOP 1 pd.MeasurementUnitId, pd.AtcId, pd.SupplieId, pd.ProductId, 0 ComponentType, pd.Vehicle, pd.Thinner, pd.Quantity
			FROM MixingStation.PackageDetail pd WITH(NOLOCK)
			WHERE pp.Id IS NULL AND pd.PackageId = p.Id AND pd.MainMedicine = 1
		) pd
		JOIN Inventory.InventoryMeasurementUnit mu WITH(NOLOCK) ON pd.MeasurementUnitId = mu.Id
		LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON pd.AtcId = a.Id
		LEFT JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON pd.SupplieId = s.Id
		LEFT JOIN Inventory.InventoryProduct pr WITH(NOLOCK) ON pd.ProductId = pr.Id
	) c
	PIVOT
	(
		MAX(Item)
		FOR c.TypeItem IN (Vehicle, Thinner, Main, Dose)
	) piv
) c
WHERE rmsd.Status <> 3 AND rpds.Status <> 6 AND rmsd.LabelType = 3