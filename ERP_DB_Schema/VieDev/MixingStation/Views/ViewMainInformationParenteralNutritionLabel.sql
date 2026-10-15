

CREATE VIEW [MixingStation].[ViewMainInformationParenteralNutritionLabel]
AS
WITH Cte_ParenteralNutritionParameters AS (
	SELECT IDHCNUTPAREC,
		[2]  AS TotalCalories, -- [Calorías totales kg/día],
		[11] AS Carbohydrates, --[Carbohidratos (%)],
		[12] AS ProteinConcentration, --[Concentración de proteína (%)],
		[13] AS LipidConcentration, --[Concentración de lípidos (%)],
		[14] AS Osmolarity --[Osmolaridad (mOsm/L)]
	FROM (
		SELECT 
			d.IDHCNUTPAREC,
			f.CODNAME,
			d.RESULT
		FROM HCNUTPAREFD d
		JOIN  HCPARNUTFORD f ON d.IDHCPARNUTFORD = f.ID
		WHERE TRY_CAST(f.CODNAME AS INT) IN (2, 11, 12, 13, 14)
	) AS SourceTable 
	PIVOT (
		MAX(RESULT)
		FOR CODNAME IN ([2], [11], [12], [13], [14])
	) AS tp_ParenteralNutritionParameters
)
SELECT 
	npc.Id  AS Id,
	--  npc.Id,
	  cd.Id CampaignDetailId,
	  RTRIM(ie.INDNOMEMP) AS Institution,
	  RTRIM(ca.NOMCENATE) AS CareCenter,
	  RTRIM(fu.Name) AS FunctionalUnit,
	  cd.ProcessingDate,
	  rmsdp.Bed AS PatientBed ,
	  pac.IPNOMCOMP AS PatientName,
	  pac.IPCODPACI AS PatientIdentification,
	  pac.IPCODPACI AS HistoryNumber, --Numero de historia
	  npc.PESOPACIE AS PatientWeight,  --En kg
	  dbo.Edad(pac.IPFECNACI, Common.GETDATE()) AS PatientAge,
	  -----
	  ISNULL(cte_pnp.TotalCalories, 0) AS TotalCalories,
	  ISNULL(cte_pnp.Carbohydrates, 0) AS Carbohydrates,
	  ISNULL(cte_pnp.LipidConcentration, 0) AS LipidConcentration,
	  ISNULL(cte_pnp.ProteinConcentration, 0) AS ProteinConcentration,
	  ISNULL(cte_pnp.Osmolarity, 0) AS Osmolarity,
	  -----
	  npc.VOLUTOTAL AS TotalVolume, --Volumen total
	  npc.AGUAESTERIL AS SterileWater,--Agua esteril
	  CASE npc.VIADMIN 
			WHEN 1 THEN 'Línea central' 
			WHEN 2 THEN 'Línea periférica' 
			ELSE '' 
		END AdministrationRouteDescription,
	  npc.VELINFUSION AS InfusionRate, --velocidad de infusión
	  npc.TEMPOADMIN AS InfusionTime,
	  cduPC.Nombre AS ProductionChemist, --Elaborado por
	  td.FullName AS Supervisor, --verificado por
	  rpds.BatchExpirationDate AS Stability, --Estabilidad
	  st.Description AS StorageConditions,--Almacenamiento
	  ps.NOMMEDICO AS NutritionistName, --Nutricionista
	  rpds.BatchCode AS InternalBatchCode--Lote
FROM MixingStation.CampaignDetail cd
JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON rmsd.Id = rmsdp.RequestMixingStationDetailId
JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON rmsdp.EntityId = psms.Id AND psms.Origin = 'HCNUTPAREC' 
JOIN HCNUTPAREC npc ON psms.IdOrigin = npc.ID
JOIN MixingStation.PackagePersonalized pp ON rmsd.PackagePersonalizedId = pp.Id
JOIN INPACIENT pac ON rmsdp.PatientCode = pac.IPCODPACI
JOIN INPROFSAL ps ON npc.CODPROSAL = ps.CODPROSAL
JOIN ADCENATEN ca ON psms.CenterAttentionCode = ca.CODCENATE
JOIN Payroll.FunctionalUnit fu ON psms.FunctionalUnitCode = fu.Code
CROSS JOIN (SELECT TOP 1 INDNOMEMP FROM INEMPRESU) ie
JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsdp.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId
LEFT JOIN Inventory.StorageTemperature st ON pp.Storage = st.Id
LEFT JOIN Cte_ParenteralNutritionParameters cte_pnp ON npc.Id = cte_pnp.IDHCNUTPAREC
--Químicos 
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
LEFT JOIN MixingStation.ViewTechnicalDirectorByCampaign td ON cd.Id = td.CampaignDetailId --Director técnico