CREATE VIEW [MixingStation].[ViewQualityControl] 
AS

WITH CTE_ExpirationDays AS (
    SELECT TOP 1 ExpirationDays AS ExpirationDays
    FROM MixingStation.MixingStationSetting
), CTE_CriticalClasification AS (
	SELECT t.RequestPackageDetailStatusId,
		CAST(MAX(t.[Critical]) AS BIT) AS [Critical],
		t.ValidateWeigthNPT
	FROM (
		SELECT rpdsdc.RequestPackageDetailStatusId,
			CAST(rpdsdcd.Quality AS INT) AS [Critical],
			rpdsdc.ValidateWeigthNPT
		FROM MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc WITH(NOLOCK)
		JOIN MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd WITH(NOLOCK)
			ON rpdsdc.Id = rpdsdcd.RequestPackageDetailStatusDefectClassificationId
		JOIN MixingStation.DefectClassificationItem dci WITH(NOLOCK)
			ON rpdsdcd.DefectClassificationItemId = dci.Id
		WHERE rpdsdcd.Quality IS NOT NULL AND dci.[Critical] = 1
	) AS t
	GROUP BY t.RequestPackageDetailStatusId, t.ValidateWeigthNPT
),
CTE_LessClasification AS (
	SELECT rpdsdc.RequestPackageDetailStatusId,
		CAST(1 AS BIT) AS [Less]
	FROM MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc WITH(NOLOCK)
	JOIN MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd WITH(NOLOCK)
		ON rpdsdc.Id = rpdsdcd.RequestPackageDetailStatusDefectClassificationId
	JOIN MixingStation.DefectClassificationItem dci WITH(NOLOCK)
		ON rpdsdcd.DefectClassificationItemId = dci.Id
	WHERE rpdsdcd.Quality = 1 AND dci.[Less] = 1
	GROUP BY rpdsdc.RequestPackageDetailStatusId
),
DetailsWithDefect AS (
	SELECT	rpdsdcd.RequestPackageDetailStatusDefectClassificationId,
			MAX(IIF(dci.Critical = 1 AND rpdsdcd.Quality = 1, 1, 0)) CriticalDefects
	FROM MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd
	INNER JOIN MixingStation.DefectClassificationItem dci ON rpdsdcd.DefectClassificationItemId = dci.Id
	GROUP BY rpdsdcd.RequestPackageDetailStatusDefectClassificationId
),
CTE_Defects AS (
		SELECT 
		rmsd.Id AS RequestMixingStationDetailId,
		SUM(CASE WHEN dwd.CriticalDefects = 1 THEN 1 ELSE 0 END) AS CriticalDefects,
		SUM(CASE WHEN dwd.CriticalDefects = 0 THEN 1 ELSE 0 END) AS WhitoutDefects
	FROM MixingStation.RequestMixingStationDetail rmsd
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId
	JOIN MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc ON rpds.Id = rpdsdc.RequestPackageDetailStatusId
	JOIN DetailsWithDefect dwd ON rpdsdc.Id = dwd.RequestPackageDetailStatusDefectClassificationId
	GROUP BY rmsd.Id
)

-- Consulta optimizada con variable de ExpirationDays ya cargada
SELECT DISTINCT
	rpds.Id,
	NULL AS RequestPackageDetailStatusIds,
	c.CampaignNumber,
	c.ProcessingDate AS DateCampaign,
	rpds.BatchCode AS Lot,
	CASE 
		WHEN inp.code IS NULL THEN 
			CASE 
				WHEN ud.MSClass IN (5,7) THEN a.Code 
				ELSE pk.Code
			END
		ELSE inp.Code
	END AS FinishedProductCode,
	CASE 
		WHEN inp.Name IS NULL THEN 
			CASE 
				WHEN ud.MSClass IN (5,7) THEN a.AbbreviationName 
				ELSE pk.Name
			END
		ELSE inp.Name
	END AS FinishedProductName,
	DATEADD(DAY, (SELECT ExpirationDays FROM CTE_ExpirationDays), GETDATE()) AS ExpirationDate,
	RTRIM(p.Fullname) AS QualitySupervisor,
	CASE rpds.[Status] WHEN 2 THEN 'ProductoTerminado' END AS [State],
	rmsd.ProductionLineId,
	CASE rpds.QualityStatus
		WHEN 0 THEN 'Pendiente'
		WHEN 1 THEN 'Liberado'
		WHEN 2 THEN 'Rechazado'
		WHEN 3 THEN 'Reprocesado'
	END AS QualityStatus,
	rmsd.CampaignDetailId,
	rpds.RequestMixingStationDetailId,
	CONCAT(pl.Code,' - ',pl.Name) AS ProductionLineCodeName,
	CONCAT(ud.Code,' - ',ud.Description) AS UnitDoseTypeCodeName,
	ud.MSClass AS UnitDoseClass,
	pk.Code AS PackageDescription,
	CONCAT(LTRIM(RTRIM(pac.IPCODPACI)),' - ',pac.IPNOMCOMP) AS PatientCodeName,
	crt.[Critical] AS FlagDefectClasificationCritical,
	rmsd.LabelType,
	CASE rmsd.LabelType
		WHEN 1 THEN 'Bolsa'
		WHEN 2 THEN 'Nutriciones parenterales'
		WHEN 3 THEN 'Jeringa'
		ELSE ''
	END AS LabelTypeName,
	c.[Status],
	c.CampaignStatus,
	rpds.[Status] AS RequestPackageDetailStatusState,
	CASE
		WHEN crt.Critical = 1 OR crt.ValidateWeigthNPT = 0 THEN 3
		WHEN lss.Less = 1 THEN 2
		WHEN crt.Critical = 0 THEN 1
		ELSE 0
	END AS FlagQualification,
	NULL AS CriticalDefects,
	NULL AS WhitoutDefects
FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK)
JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.CampaignDetail c WITH(NOLOCK) ON c.Id = rmsd.CampaignDetailId AND c.CampaignStatus IN (5,6)
JOIN MixingStation.CampaignDetailUsers cdu WITH(NOLOCK) ON c.Id = cdu.CampaignDetailId AND cdu.UserRole = 1
JOIN MixingStation.ProductionLine pl ON c.ProductionLineId = pl.Id
JOIN MixingStation.UnitDoseType ud ON rmsd.UnitDoseTypeId = ud.Id
JOIN Security.[User] us ON cdu.UserId = us.Id
JOIN Security.Person p ON us.IdPerson = p.Id
LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON a.Id = rmsd.ATCId
LEFT JOIN MixingStation.Package pk WITH(NOLOCK) ON rpds.PackageId = pk.Id
LEFT JOIN Inventory.InventoryProduct inp WITH(NOLOCK) ON inp.Id = pk.ProductId
LEFT JOIN MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK) 
	ON rmsdp.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId AND rmsdp.CampaignDetailId = c.Id
LEFT JOIN INPACIENT pac WITH(NOLOCK) ON rmsdp.PatientCode = pac.IPCODPACI
LEFT JOIN CTE_CriticalClasification crt ON rpds.Id = crt.RequestPackageDetailStatusId
LEFT JOIN CTE_LessClasification lss ON rpds.Id = lss.RequestPackageDetailStatusId
WHERE rpds.Status = 2 AND rpds.BatchCode IS NOT NULL AND ud.MSClass NOT IN (5,7)

UNION ALL

-- Reempaque
SELECT
	rmsd.Id,
	STRING_AGG(TRY_CAST(rpds.Id AS VARCHAR(MAX)), ',') AS RequestPackageDetailStatusIds,
	c.CampaignNumber,
	c.ProcessingDate AS DateCampaign,
	rpds.BatchCode AS Lot,
	CASE 
		WHEN inp.code IS NULL THEN 
			CASE 
				WHEN ud.MSClass IN (5,7) THEN a.Code 
				ELSE pk.Code
			END
		ELSE inp.Code
	END AS FinishedProductCode,
	CASE 
		WHEN inp.Name IS NULL THEN 
			CASE 
				WHEN ud.MSClass IN (5,7) THEN a.AbbreviationName 
				ELSE pk.Name
			END
		ELSE inp.Name
	END AS FinishedProductName,
	DATEADD(DAY, (SELECT ExpirationDays FROM CTE_ExpirationDays), GETDATE()) AS ExpirationDate,
	RTRIM(p.Fullname) AS QualitySupervisor,
	CASE rpds.[Status] WHEN 2 THEN 'ProductoTerminado' END AS [State],
	rmsd.ProductionLineId,
	CASE rpds.QualityStatus
		WHEN 0 THEN 'Pendiente'
		WHEN 1 THEN 'Liberado'
		WHEN 2 THEN 'Rechazado'
		WHEN 3 THEN 'Reprocesado'
	END AS QualityStatus,
	rmsd.CampaignDetailId,
	rpds.RequestMixingStationDetailId,
	CONCAT(pl.Code,' - ',pl.Name) AS ProductionLineCodeName,
	CONCAT(ud.Code,' - ',ud.Description) AS UnitDoseTypeCodeName,
	ud.MSClass AS UnitDoseClass,
	NULL AS PackageDescription,
	NULL AS PatientCodeName,
	NULL AS FlagDefectClasificationCritical,
	rmsd.LabelType,
	CASE rmsd.LabelType
		WHEN 1 THEN 'Bolsa'
		WHEN 2 THEN 'Nutriciones parenterales'
		WHEN 3 THEN 'Jeringa'
		ELSE ''
	END AS LabelTypeName,
	c.[Status],
	c.CampaignStatus,
	rpds.[Status] AS RequestPackageDetailStatusState,
	NULL AS FlagQualification,
	cte.CriticalDefects AS CriticalDefects,
	cte.WhitoutDefects AS WhitoutDefects
FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK)
JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN Inventory.ATC a WITH(NOLOCK) ON a.Id = rmsd.ATCId
JOIN MixingStation.CampaignDetail c WITH(NOLOCK) ON c.Id = rmsd.CampaignDetailId AND c.CampaignStatus IN (5,6)
JOIN MixingStation.CampaignDetailUsers cdu WITH(NOLOCK) ON c.Id = cdu.CampaignDetailId AND cdu.UserRole = 1
JOIN MixingStation.ProductionLine pl ON c.ProductionLineId = pl.Id
JOIN MixingStation.UnitDoseType ud ON rmsd.UnitDoseTypeId = ud.Id
JOIN Security.[User] us ON cdu.UserId = us.Id
JOIN Security.Person p ON us.IdPerson = p.Id
LEFT JOIN CTE_Defects cte ON cte.RequestMixingStationDetailId = rmsd.Id
LEFT JOIN MixingStation.Package pk WITH(NOLOCK) ON rpds.PackageId = pk.Id
LEFT JOIN Inventory.InventoryProduct inp WITH(NOLOCK) ON inp.Id = pk.ProductId
LEFT JOIN MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK) 
	ON rmsdp.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId AND rmsdp.CampaignDetailId = c.Id
WHERE rpds.Status = 2 AND rpds.BatchCode IS NOT NULL AND ud.MSClass IN (5,7)
GROUP BY rmsd.Id, c.CampaignNumber, c.ProcessingDate, rpds.BatchCode, inp.Code, ud.MSClass, a.Code, 
	pk.Code, inp.Name, a.AbbreviationName, pk.Name, p.Fullname, rpds.Status, rmsd.ProductionLineId,
	rpds.QualityStatus, rmsd.CampaignDetailId, rpds.RequestMixingStationDetailId, pl.Code, pl.Name,
	ud.Code, ud.Description, rmsd.LabelType, c.Status, c.CampaignStatus, cte.CriticalDefects, cte.WhitoutDefects