CREATE VIEW [MixingStation].[ViewParenteralNutritionLabel]
AS
	SELECT -- CABECERA: IDENTIFICACIÓN DEL PACIENTE
		npc.Id
		, cd.Id CampaignDetailId
		, rpds.Id RequestPackageDetailStatusId
		, pac.IPNOMCOMP AS PatientName
		, td.SIGLA AS PatientDocumentType
		, pac.IPCODPACI AS PatientCode
		, dbo.Edad(pac.IPFECNACI, Common.GETDATE()) AS PatientAge
		, CONCAT (npc.PESOPACIE, ' kg') AS PatientWeight
		, CONCAT(RTRIM(ca.NOMCENATE), ' - ', RTRIM(fu.Name)) AS PatientLocation
		, rmsdp.Bed PatientBed
		--Parámetros físico-químicos
		, rpds.BatchCode
		, CASE npc.VIADMIN 
			WHEN 1 THEN 'Línea central' 
			WHEN 2 THEN 'Línea periférica'
			ELSE '' 
		END AdministrationRouteDescription
		, npc.TEMPOADMIN InfusionTime
		, npc.VOLUTOTAL TotalVolume
		, npc.VELINFUSION InfusionVelocity
		--Datos de preparación
		, cd.ProcessingDate
		, rpds.BatchExpirationDate FinishDate
		, ISNULL(cduQC.Usuario, '') AS QualityChemical
		, ISNULL(cduPC.Usuario, '') AS ProductionChemical
		, '' Recommendations
		, pnc.[NAME] AS ParenteralNutritionName
		, case npc.TIPOINFUSION
			WHEN 1 THEN 'Continua'
			WHEN 2 THEN 'Ciclada'
			ELSE ''
		END InfusionType
		, CASE pp.Storage
			WHEN 1 THEN	'Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)'
			WHEN 2 THEN 'Ambiente controlada: 20 ° C - 25 ° C'  
			WHEN 3 THEN 'En frío: 8 ° C - 15 ° C'  
			WHEN 4 THEN 'Refrigerador: 2 ° C - 8 ° C'  
			WHEN 5 THEN 'Congelador: -25 ° C - 10 ° C'
		END StorageName
		, CASE pp.PhotoProtection
			WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
		END PhotoProtection
	FROM MixingStation.CampaignDetail cd
	JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON cd.Id = rmsdp.CampaignDetailId
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rmsdp.RequestMixingStationDetailId
	JOIN MixingStation.PackagePersonalized pp ON pp.Id = rmsd.PackagePersonalizedId
	JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON rmsdp.EntityId = psms.Id
	JOIN INPACIENT pac ON rmsdp.PatientCode = pac.IPCODPACI
	JOIN dbo.ADTIPOIDENTIFICA td ON pac.IPTIPODOC = td.CODIGO 
	JOIN HCNUTPAREC npc ON psms.IdOrigin = npc.ID
	JOIN HCPARNUTC pnc ON npc.IDHCPARNUTC = pnc.Id
	JOIN Payroll.FunctionalUnit fu ON psms.FunctionalUnitCode = fu.Code
	JOIN ADCENATEN ca ON psms.CenterAttentionCode = ca.CODCENATE
	-----Batchcode
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsdp.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId
	--- Datos de preparación
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC (NOLOCK) ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 