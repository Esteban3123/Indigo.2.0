CREATE VIEW [MixingStation].[ViewMagistralLabel]
AS
WITH CTE_PharmaInfo AS (
    SELECT	pd.GroupingCodeDose,
			ar.DESVIAADM AS AdministrationName,
			h.ADMMEZLIQ	AS Indications
	FROM MedicalHistory.PharmaDose pd
    JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation AND psms.Origin = 'HCINFLIQA'
	JOIN HCINFLIQA h ON psms.IdOrigin = h.CONSECUTI
	LEFT JOIN dbo.HCINFLIQD  hc on hc.CODCONCEC = H.CODCONCEC
	LEFT JOIN HCVIAADMI ar on ar.CODVIAADM = hc.VIAADMDIL
	GROUP BY pd.GroupingCodeDose, ar.DESVIAADM, h.ADMMEZLIQ
)
SELECT	
		rpds.Id Id,
		rpds.Id RequestPackageDetailStatusId,
		cd.Id CampaignDetailId,
		ISNULL(pp.Id, p.Id) AS PackageId,
		p.[Name] PackageName,
		pac.IPNOMCOMP PatientName,
		rmsdp.PatientCode,
		rmsdp.Bed,
		pin.Indications,
		cd.ProcessingDate,
		rpds.BatchExpirationDate AS ExpirationDate,
		rpds.BatchCode AS InternalBatchCode,
		pin.AdministrationName AS AdministrationRoute,
		st.Description AS StorageConditions,
		cduPC.Nombre AS ProductionChemist,
		cduQC.Nombre AS Supervisor,
		'' AS Content,
		ISNULL(pp.SpecialConsiderations, p.SpecialConsiderations) AS SpecialConsiderations
FROM MixingStation.CampaignDetail cd
JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON rmsd.Id = rmsdp.RequestMixingStationDetailId
JOIN MixingStation.Package p ON p.Id = rmsd.PackageId
JOIN INPACIENT pac ON rmsdp.PatientCode = pac.IPCODPACI
JOIN MixingStation.UnitDoseType udt ON cd.UnitDoseTypeId = udt.Id
JOIN CTE_PharmaInfo pin ON rpds.GroupingCodeDose = pin.GroupingCodeDose 
LEFT JOIN MixingStation.PackagePersonalized pp ON rmsd.PackagePersonalizedId = pp.Id
LEFT JOIN Inventory.StorageTemperature st ON ISNULL(pp.Storage, p.Storage) = st.Id
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad 
WHERE udt.MSClass = 9 
		AND rmsd.Source = 1 -- 1. Orden Médica  2. Solicitud Externa Paciente  3. Solicitud Externa Maquila  4. Solicitud Inventario