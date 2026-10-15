






CREATE VIEW [MixingStation].[ViewListDashboardRequestMixingStation] 
as 

	SELECT DISTINCT
		cast(rmsdp.Id as varchar) as Id,
		rms.Id RequestMixingStationId, 
		rd.Id RequestMixingStationDetailId,
		rms.Code RequestCode, 
		rms.RequestDate, 
		ISNULL(npt.USERCREATE, rms.RequestUser) AS RequestUser,
		ISNULL(npt.USERCREATE, rms.RequestUser) + ' - ' + IIF(npt.USERCREATE IS NULL, per.Fullname, npt.Fullname) as RequestUserCodeName,
		rms.CMConfigurationId,
		rd.ProductionLineId, 
		IIF(rd.ProductionLineId is not null, pl.Code + ' - ' + pl.[Name], 'Sin Asignar') ProductionLineCodeName,
		rd.CareCenterCode, 
		IIF(cce.Id is not null, cce.Code + ' - ' + cce.[Description], rtrim(ltrim(cci.CODCENATE)) + ' - ' + rtrim(ltrim(cci.NOMCENATE))) CareCenterCodeName,
		rd.Quantity,
		rd.UnitDoseTypeId,
		ut.MSClass,
		udt.Code + ' - ' + udt.[Description] as UnitDoseTypeCodeName,
		rd.[Source] as RequestType,
		case rd.[Source] 
			when 1 then 'Orden Médica'
			when 2 then 'Solicitud Externa Paciente'
		end RequestTypeName,
		IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, 
		IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName, 
		ISNULL(pp.Id, ISNULL(p.Id, a.Id)) ItemId,  
		IIF (udt.MSClass = 2, 
				ISNULL( pp.Code + ' - ' + pp.Name, ISNULL(p.Code + ' - ' + p.Name, a.Code + ' - ' + a.Name)),
				ISNULL(pp.Code + ' - ' + pp.Description, ISNULL(p.Code + ' - ' + p.Description, a.Code + ' - ' + a.Name))
			) ItemCodeName,
		rmsdp.PatientCode + ' - ' + ISNULL(pecc.Name, RTRIM(ltrim(pat.IPNOMCOMP))) PatientCodeName,
		ISNULL(hcp.PREESTADO, ISNULL(hnpt.STATUS, 0)) As StatusHCPRESCRA,
		case ISNULL(hcp.PREESTADO, ISNULL(hnpt.STATUS, 0))
			when 1 then 'Iniciado'
			when 2 then IIF(ISNULL(hnpt.STATUS, 0) = 2, 'Tratamiento anulado', Iif(egr.FECALTPAC is not null, 'Alta médica', ''))
			when 3 then 'Tratamiento descontinuado'
			when 4 then 'Tratamiento suspendido'
			when 5 then 'Plan de manejo externo'
			when 6 then 'Medicamentos solicitados sin existencia actual en el kardex'
			when 7 then 'Tratamiento terminado por salida del paciente'
			else Iif(egr.FECALTPAC is not null, 'Alta médica', '')
		end As StatusNameHCPRESCRA,
		egr.FECALTPAC,
		rd.EntityName,
		iif(rd.EntityName = 'RequestPackageDetailStatus', 1, 2) as OrderField
		,hfc.ConfirmationStatus
	from MixingStation.RequestMixingStation rms (nolock) 
	INNER JOIN MixingStation.RequestMixingStationDetail rd (nolock) on rms.Id = rd.RequestMixingStationId
	INNER JOIN MixingStation.UnitDoseType udt (nolock) on udt.Id = rd.UnitDoseTypeId
	LEFT JOIN MixingStation.RequestMixingStationDetailPatients rmsdp (nolock) on rmsdp.RequestMixingStationDetailId = rd.Id
	LEFT JOIN MixingStation.ProductionLine pl (nolock) on pl.Id = rd.ProductionLineId
	LEFT JOIN [Security].[User] u on u.UserCode = rms.RequestUser
	LEFT JOIN [Security].Person per on per.Id = u.IdPerson
	LEFT JOIN .ADCENATEN cci (nolock) on cci.CODCENATE = rd.CareCenterCode and rd.Source in (1, 2)
	LEFT JOIN MixingStation.ExternalCareCenter cce (nolock) on cce.Code = rd.CareCenterCode and rd.Source in (1, 2)
	LEFT JOIN MixingStation.Package p (nolock) on p.Id = rd.PackageId
	LEFT JOIN Inventory.ATC a (nolock) on a.Id = rd.ATCId
	LEFT JOIN MixingStation.PackagePersonalized pp (nolock) on pp.Id = rd.PackagePersonalizedId
	LEFT JOIN dbo.INPACIENT pat (nolock) on pat.IPCODPACI = rmsdp.PatientCode and rmsdp.EntityName = 'ProductSusceptibleMixingStation'
	LEFT JOIN MixingStation.PatientExternalCareCenter pecc (nolock) on pecc.IdentificationNumber = rmsdp.PatientCode and rmsdp.EntityName = 'RequestUnitDoseExternalCareCenterPatientDetails'
	LEFT JOIN MedicalHistory.ProductSusceptibleMixingStation sus (nolock) on rmsdp .EntityId = sus.Id and rmsdp.EntityName = 'ProductSusceptibleMixingStation'
	LEFT JOIN HCPRESCRA hcp (nolock) on sus.IdOrigin = hcp.ID and sus.Origin = 'HCPRESCRA'
	LEFT JOIN MixingStation.ConfirmationUnitDose cu ON cu.Id = rd.EntityId AND rd.EntityName = 'ConfirmationUnitDose'
	LEFT JOIN MedicalHistory.PharmaDose pd on pd.GroupingCodeDose = cu.GroupingCodeDose AND pd.CodeSusceptibleMixingStation = sus.CodeSusceptibleMixingStation AND pd.ProductCode = sus.MainDrugCode
	LEFT JOIN HCFARMEPC hfc on hfc.CODCONCEC = pd.IDHCFARMEPC
	---------------------------------------------------------------------------------------------
	LEFT JOIN HCNUTPAREC hnpt(nolock) on sus.IdOrigin = hnpt.ID and sus.Origin = 'HCNUTPAREC'
	---------------------------------------------------------------------------------------------
	LEFT JOIN
	(
		SELECT 
			farm.CodeSusceptibleMixingStation, 
			RTRIM(C.CODUSUARI) USERCREATE, 
			RTRIM(C.NOMMEDICO) as Fullname
		FROM  HCFARMEPD farm
		JOIN HCNUTPAREC npt ON farm.IdSourceTable = npt.ID
		JOIN dbo.INPROFSAL C On farm.CODPROSAL = C.CODPROSAL 
		GROUP BY farm.CodeSusceptibleMixingStation, C.CODUSUARI, C.NOMMEDICO
	) npt ON udt.MSClass = 2 AND sus.CodeSusceptibleMixingStation = npt.CodeSusceptibleMixingStation
	LEFT JOIN MixingStation.UnitDoseType ut (NOLOCK) ON ut.Id = rd.UnitDoseTypeId
	OUTER APPLY (
		select Top 1 * from HCREGEGRE eg (nolock) where eg.NUMINGRES = hcp.NUMINGRES And eg.IPCODPACI = hcp.IPCODPACI
	) egr
	where rms.Status = 2 and rd.Source in (1, 2) and rd.SendTo = 0 and rd.CampaignDetailId Is Null And rmsdp.Status = 1 and rd.Status<>3
	
	UNION ALL

	SELECT 
		Concat(rd.Id,rms.Id,rd.Id),
		rms.Id RequestMixingStationId, 
		rd.Id RequestMixingStationDetailId,
		rms.Code RequestCode, rms.RequestDate, 
		rms.RequestUser, 
		rms.RequestUser + ' - ' + per.Fullname RequestUserCodeName,
		rms.CMConfigurationId,
		rd.ProductionLineId, IIF(rd.ProductionLineId is not null, pl.Code + ' - ' + pl.Name, 'Sin Asignar') ProductionLineCodeName,
		rd.CareCenterCode, IIF(cce.Id is not null, cce.Code + ' - ' + cce.Description, rtrim(ltrim(cci.CODCENATE)) + ' - ' + rtrim(ltrim(cci.NOMCENATE))) CareCenterCodeName,
		rd.Quantity,
		rd.UnitDoseTypeId,
		ut.MSClass,
		udt.Code + ' - ' + udt.Description UnitDoseTypeCodeName,
		rd.Source RequestType,
		case rd.Source 
			when 3 then 'Solicitud Externa Maquila'
			when 4 then 'Solicitud Inventario'
		end RequestTypeName,
		IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, 
		IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName, 
		ISNULL(pp.Id, ISNULL(p.Id, a.Id)) ItemId,  
		ISNULL(pp.Code + ' - ' + pp.Description, ISNULL(p.Code + ' - ' + p.Name, a.Code + ' - ' + a.Name)) ItemCodeName,
		NULL PatientCodeName,
		0 As StatusHCPRESCRA,
		'' As StatusNameHCPRESCRA,
		NULL as FECALTPAC,
		rd.EntityName,
		iif(rd.EntityName = 'RequestPackageDetailStatus', 1, 2) as OrderField
		,hfc.ConfirmationStatus
	FROM MixingStation.RequestMixingStation rms (nolock) 
	INNER JOIN MixingStation.RequestMixingStationDetail rd (nolock) on rms.Id = rd.RequestMixingStationId
	INNER JOIN MixingStation.UnitDoseType udt (nolock) on udt.Id = rd.UnitDoseTypeId
	LEFT JOIN MixingStation.ProductionLine pl (nolock) on pl.Id = rd.ProductionLineId
	LEFT JOIN .ADCENATEN cci (nolock) on cci.CODCENATE = rd.CareCenterCode and rd.Source in (3,4)
	LEFT JOIN MixingStation.ExternalCareCenter cce (nolock) on cce.Code = rd.CareCenterCode and rd.Source in (3,4)
	LEFT JOIN MixingStation.Package p (nolock) on p.Id = rd.PackageId
	LEFT JOIN Inventory.ATC a (nolock) on a.Id = rd.ATCId
	LEFT JOIN MixingStation.PackagePersonalized pp (nolock) on pp.Id = rd.PackagePersonalizedId
	LEFT JOIN Security.[User] u on u.UserCode = rms.RequestUser
	LEFT JOIN Security.Person per on per.Id = u.IdPerson
	LEFT JOIN MixingStation.RequestMixingStationDetailPatients rmsdp (nolock) on rmsdp.RequestMixingStationDetailId = rd.Id
	LEFT JOIN MedicalHistory.ProductSusceptibleMixingStation sus (nolock) on rmsdp .EntityId = sus.Id and rmsdp.EntityName = 'ProductSusceptibleMixingStation'
	LEFT JOIN HCPRESCRA hcp (nolock) on sus.IdOrigin = hcp.ID and sus.Origin = 'HCPRESCRA'
	LEFT JOIN MixingStation.UnitDoseType ut (NOLOCK) ON ut.Id = rd.UnitDoseTypeId
		outer apply (
		select Top 1 * from HCREGEGRE eg (nolock) where eg.NUMINGRES = hcp.NUMINGRES And eg.IPCODPACI = hcp.IPCODPACI
	) egr
	LEFT JOIN MixingStation.ConfirmationUnitDose cu ON cu.Id = rd.EntityId AND rd.EntityName = 'ConfirmationUnitDose'
	LEFT JOIN MedicalHistory.PharmaDose pd on pd.GroupingCodeDose = cu.GroupingCodeDose AND pd.CodeSusceptibleMixingStation = sus.CodeSusceptibleMixingStation AND pd.ProductCode = sus.MainDrugCode
	LEFT JOIN HCFARMEPC hfc on hfc.CODCONCEC = pd.IDHCFARMEPC
	where rms.Status = 2 and rd.Source in (3,4) and rd.SendTo = 0 and rd.CampaignDetailId IS NULL and rd.Status = 1