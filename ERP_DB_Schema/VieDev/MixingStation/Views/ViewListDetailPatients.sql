




CREATE view [MixingStation].[ViewListDetailPatients] 
AS

	select	
			rpds.Id Id
			, rdp.Id as RequestMixingStationDetailPatientsId
			, rd.Id as RequestMixingStationDetailId
			, rdp.PatientCode
			, concat(rdp.PatientCode, ' - ', iif(pe.Id is not null, concat(pe.[Name], ' ', pe.LastName), rtrim(ltrim(p.IPNOMCOMP)))) as PatientCodeName
			, rdp.FunctionalUnitCode
			, rdp.FunctionalUnitCode + iif(fu.Id is not null, ' - ' + fu.[Name], '') as FunctionalUnitCodeName
			, rdp.Bed
			,CASE WHEN ut.MSClass = 2 AND hnpt.VIADMIN is not null THEN -- se evalua si es de tipo NPT obtiene la via de administracion de la tabla del EHR
			CASE WHEN hnpt.VIADMIN = 1 THEN 'Línea central'
				 ELSE
				  CASE WHEN  hnpt.VIADMIN = 2 THEN 'Línea periférica'
				  ELSE 'No encontrado'
				  END
			 END
		    ELSE art.[Name]
		    END AS AdministrationRouteCodeName
			, rdp.[Status] StatusDetailPatiens
			, rpds.[Status]
			, case rpds.[Status]
				when 1 then 'Producción' 
				when 2 then 'Terminado' 
				when 3 then 'Liberado' 
				when 4 then 'Reproceso' 
				when 5 then 'Rechazado' 
				when 6 then 'Anulado' 
			end as StatusName
			, case rdp.[Status]
				when 1 then 'Registrado'
				when 2 then 'Confirmado'
				when 3 then 'Anulado'
			end as RequestMixingStationDetailPatientsStatusName
			, isnull(data.PREESTADO, 0) as StatusHCPRESCRA
			, case isnull(data.PREESTADO, 0)
				when 1 then 'Iniciado'
				when 2 then 'Ciclo completado'
				when 3 then 'Tratamiento Descontinuado'
				when 4 then 'Tratamiento Suspendido'
				when 5 then 'Plan de manejo externo'
				when 6 then 'Medicamentos solicitados sin existencia actual en el kardex'
				when 7 then 'Tratamiento terminado por salida del paciente'
				else iif(egr.FECALTPAC is not null, 'Alta médica', '')
			end StatusNameHCPRESCRA
			, pa.Code as CodePackage
			, pa.[Name] as NamePackage
			, atc.Code as ATCCode
			, atc.[Name] as ATCName
			, concat(ut.Code,' - ', ut.[Description]) as DoseTypeCodeName
			, case rd.[Source]
				WHEN 1 THEN 'Orden Médica'
				WHEN 2 THEN 'Solicitud Externa Paciente'
				WHEN 3 THEN 'Solicitud Externa Maquila'
				WHEN 4 THEN 'Solicitud Inventario'
			ELSE 'N/A'
			END RequestType,
			case rd.LabelType
				WHEN 1 THEN 'Bolsa'
				WHEN 2 THEN 'Mediana'
				WHEN 3 THEN 'Jeringa'
				ELSE 'N/A'
			END LabelTypeName,
			rd.LabelType,
			rpds.Observations,
			concat(ut.Code, '', ut.Description) as  UnitDoseTypeCodeName,
			ut.MSClass as UnitDoseClass,
			concat(pl.Code, ' - ', pl.Name) as ProductionLineCodeName,
			(SELECT count(1) FROM [MixingStation].CampaignRawMaterial WHERE RequestPackageDetailStatusId = rpds.Id) As CampaignRawMaterialExist,
			rpds.BatchCode,
			egr.FECALTPAC,
			ISNULL(pp.Code + ' - ' + pp.Description, pa.Code + ' - ' + pa.Description) PackageDescription,
			IIF(x.RequestPackageDetailStatusId IS NULL, 0, 1) FlagQualityDefect,
			cast(iif((select count(di.ProductionChemical) from MixingStation.DefectClassificationItem di (nolock)
			JOIN MixingStation.DefectsUnitDoseType dut ON dut.Id_DefectsClassificationItem = di.Id
			JOIN MixingStation.UnitDoseType ud ON ud.Id = dut.Id_UnitDoseType
			where di.ProductionChemical = 1 and di.State = 1 and ud.MSClass = ut.MSClass) > 0, 1, 0) as bit) as HasProductionDeffect
	from MixingStation.RequestMixingStationDetailPatients rdp (NOLOCK)
	inner join MixingStation.RequestMixingStationDetail rd (NOLOCK) on rd.Id = rdp.RequestMixingStationDetailId
	inner join MixingStation.UnitDoseType ut (NOLOCK) ON rd.UnitDoseTypeId = ut.Id
	inner join MixingStation.RequestPackageDetailStatus rpds (NOLOCK) on rd.Id = rpds.RequestMixingStationDetailId-- or rd.RequestPackageDetailStatusId = rpds.Id
	inner join MixingStation.ProductionLine pl (nolock) on rd.ProductionLineId = pl.Id
	left join MixingStation.Package pa on rd.PackageId = pa.Id
	left join MixingStation.PackagePersonalized pp WITH(NOLOCK) on rpds.PackagePersonalizedId = pp.Id
	left join Inventory.ATC atc on rd.ATCId = atc.Id
	left join .INPACIENT p WITH(NOLOCK) on p.IPCODPACI = rdp.PatientCode and rd.[Source] = 1
	left join Payroll.FunctionalUnit fu WITH(NOLOCK) on fu.Code = rdp.FunctionalUnitCode and rd.[Source] = 1
	left join MixingStation.PatientExternalCareCenter pe WITH(NOLOCK) on pe.IdentificationNumber = rdp.PatientCode and rd.[Source] = 2
	left join Inventory.AdministrationRoute ar WITH(NOLOCK) on ar.Id = rdp.AdministrationRouteId
	--left join(	
	--	SELECT hc.ID IdHCPRESCRA, hc.CODVIAADM
	--		, hc.PREESTADO, h.ID IdHCFARMEPD           
	--		, hc.IPCODPACI, hc.NUMINGRES
	--	from HCFARMEPD h (NOLOCK)
	--	inner join HCPRESCRA hc (NOLOCK) on h.SourceTable = 'HCPRESCRA' and hc.ID = h.IdSourceTable
	--) data on data.IdHCFARMEPD = rdp.EntityId and rdp.EntityName = 'HCFARMEPD'
	left join(	
	SELECT hc.ID IdHCPRESCRA, hc.CODVIAADM, psms.Id ,
		hc.PREESTADO,        
		hc.IPCODPACI, hc.NUMINGRES
	from  MedicalHistory.ProductSusceptibleMixingStation as psms  
	left join  HCPRESCRA as hc on hc.ID = psms.IdOrigin 
	) data on data.Id = rdp.EntityId and rdp.EntityName = 'ProductSusceptibleMixingStation'
	outer apply (
		select Top 1 * from HCREGEGRE eg (nolock) where eg.NUMINGRES = data.NUMINGRES And eg.IPCODPACI = data.IPCODPACI
	) egr
	--LEFT JOIN  HCVIAADMI hcv WITH(NOLOCK) ON data.CODVIAADM = hcv.CODVIAADM
	left join Inventory.AdministrationRoute art (nolock) on rdp.AdministrationRouteId = art.Id

	left join MedicalHistory.ProductSusceptibleMixingStation psms on psms.Id = rdp.EntityId
	left join HCNUTPAREC hnpt on psms.IdOrigin = hnpt.ID

	LEFT JOIN(
		SELECT rpdsc.RequestPackageDetailStatusId
		from  MixingStation.RequestPackageDetailStatusDefectClassification rpdsc WITH(NOLOCK)
		join MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdscd WITH(NOLOCK) on rpdsc.Id = rpdscd.RequestPackageDetailStatusDefectClassificationId and rpdscd.Quality is not null
		GROUP by rpdsc.RequestPackageDetailStatusId
	) x on x.RequestPackageDetailStatusId= rpds.Id

	UNION ALL

	SELECT 	rpds.Id Id,
			null RequestMixingStationDetailPatientsId,
			rd.Id RequestMixingStationDetailId,
			null PatientCode,
			null  PatientCodeName,
			null FunctionalUnitCode,
			null FunctionalUnitCodeName,
			null Bed,
			null AdministrationRouteCodeName,
			null StatusDetailPatiens,
			rpds.Status,
			case rpds.status when 1 then 'Producción' when 2 then 'Terminado' when 3 then 'Liberado' when 4 then 'Reproceso' when 5 then 'Rechazado' when 6 then 'Anulado' end as StatusName,
			NULL RequestMixingStationDetailPatientsStatusName,
			NULL StatusHCPRESCRA,
			NULL StatusNameHCPRESCRA,
			p.Code CodePackage,
			p.Name NamePackage,
			atc.Code ATCCode,
			atc.Name ATCName,
			CONCAT(ut.Code,' - ',ut.Description) DoseTypeCodeName,
			case rd.[Source]
			WHEN 1 THEN 'Orden Médica'
			WHEN 2 THEN 'Solicitud Externa Paciente'
			WHEN 3 THEN 'Solicitud Externa Maquila'
			WHEN 4 THEN 'Solicitud Inventario'
			ELSE 'N/A'
			END RequestType,
			case rd.LabelType
				WHEN 1 THEN 'Bolsa'
				WHEN 2 THEN 'Mediana'
				WHEN 3 THEN 'Jeringa'
				ELSE 'N/A'
			END LabelTypeName,
			rd.LabelType,
			rpds.Observations,
			concat(ut.Code, '', ut.Description) as  UnitDoseTypeCodeName,
			ut.MSClass as UnitDoseClass,
			concat(pl.Code, ' - ', pl.Name) as ProductionLineCodeName,
			(SELECT count(1) FROM [MixingStation].CampaignRawMaterial WHERE RequestPackageDetailStatusId = rpds.Id) As CampaignRawMaterialExist,
			rpds.BatchCode,
			null FECALTPAC,
			ISNULL(pp.Code + ' - ' + pp.Description, p.Code + ' - ' + p.Description) PackageDescription,
			IIF(x.RequestPackageDetailStatusId IS NULL,0,1) FlagQualityDefect,
			cast(iif((select count(di.ProductionChemical) from MixingStation.DefectClassificationItem di (nolock)
			JOIN MixingStation.DefectsUnitDoseType dut ON dut.Id_DefectsClassificationItem = di.Id
			JOIN MixingStation.UnitDoseType ud ON ud.Id = dut.Id_UnitDoseType
			where di.ProductionChemical = 1 and di.State = 1 and ud.MSClass = ut.MSClass) > 0, 1, 0) as bit) as HasProductionDeffect
	from MixingStation.RequestMixingStationDetail rd with(NOLOCK)
	inner join MixingStation.RequestPackageDetailStatus rpds with(NOLOCK) on rd.Id = rpds.RequestMixingStationDetailId-- or rd.RequestPackageDetailStatusId = rpds.Id
	inner join MixingStation.UnitDoseType ut WITH(NOLOCK) ON rd.UnitDoseTypeId= ut.Id
	inner join MixingStation.ProductionLine pl with(nolock) on rd.ProductionLineId = pl.Id
	LEFT JOIN MixingStation.Package p with(NOLOCK) on rd.PackageId = p.Id
	LEFT JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) on rpds.PackagePersonalizedId =pp.Id
	left join Inventory.ATC atc with(NOLOCK) on rd.ATCId=atc.Id
	left join MixingStation.RequestMixingStationDetailPatients rmsdp with(NOLOCK) on rmsdp.RequestMixingStationDetailId =rd.Id
	LEFT JOIN(SELECT rpdsc.RequestPackageDetailStatusId
				from  MixingStation.RequestPackageDetailStatusDefectClassification rpdsc WITH(NOLOCK)
				join MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdscd WITH(NOLOCK) on rpdsc.Id = rpdscd.RequestPackageDetailStatusDefectClassificationId and rpdscd.Quality =1
				GROUP by rpdsc.RequestPackageDetailStatusId
				) x on x.RequestPackageDetailStatusId= rpds.Id
	where rmsdp.id is null
	GROUP by rpds.Id, rd.Id, p.code,p.Name,	atc.Code,atc.Name,rpds.Status
		,rd.LabelType,ut.Code,ut.Description, ut.MSClass, pl.Code, pl.Name, rd.[Source],rpds.Observations
		, rpds.BatchCode,pp.Code,pp.Description,p.Description,x.RequestPackageDetailStatusId