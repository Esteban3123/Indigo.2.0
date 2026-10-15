

CREATE view [MixingStation].[ViewNonConformingProduct] 
as 
	Select RPD.Id AS Id
		, rmsd.[Source]
		, case rmsd.[Source]
			when 1 then 'Orden Médica'
			when 2 then 'Solicitud Externa Paciente'
			when 3 then 'Solicitud Externa Maquila'
			when 4 then 'Solicitud Inventario'
			else ''
		end as SourceName
		, rms.Code as RequestCode
		, rms.RequestDate
		, ps.Code as ProductionScheduleCode
		, SP2.Fullname as UserName
		,CASE RPD.[Status]
			WHEN '3' THEN 'Producto Liberado'
			WHEN '4' THEN 'Producto Reproceso'
			WHEN '5' THEN 'Producto Rechazado'
			WHEN '6' THEN 'Producto Anulado'
			End  As RequestPackageDetailstatus
		, CD.CampaignNumber AS CampaignNumber
		, CD.ProcessingDate AS DateCampaign
		, RPD.BatchCode AS Lot
		, INP.Code AS FinishedProductCode
		, INP.Name AS FinishedProductName
		, DATEADD(DAY,(SELECT top 1 ms.ExpirationDays from MixingStation.MixingStationSetting ms),GETDATE()) AS ExpirationDate
		, RTRIM(SP.Fullname) AS QualitySupervisor
		, CASE RPD.Status WHEN 2 THEN 'ProductoTerminado' END AS State
		, rmsd.ProductionLineId
		, CASE RPD.QualityStatus WHEN 0 THEN 'Pendiente' WHEN 1 THEN 'Liberado' WHEN 2 THEN 'Rechazado' WHEN 3 THEN 'Reprocesado' END AS QualityStatus
		, rmsd.CampaignDetailId, RPD.RequestMixingStationDetailId
		, concat(pl.Code, ' - ', pl.Name) as ProductionLineCodeName
		, concat(ud.Code, ' - ', ud.Description) as UnitDoseTypeCodeName
		, ud.MSClass as UnitDoseClass
		, ISNULL(pp.Code + ' - ' + pp.Description, p.Code + ' - ' + p.Description) PackageDescription
		, CONCAT(LTRIM(RTRIM(pac.IPCODPACI)), ' - ', pac.IPNOMCOMP) As PatientCodeName
		, fu.UFUDESCRI
		, ad.NOMCENATE
	from [MixingStation].[RequestPackageDetailStatus] RPD WITH(NOLOCK)
	Join MixingStation.RequestMixingStationDetail rmsd (nolock) on RPD.RequestMixingStationDetailId = rmsd.Id
	Join MixingStation.RequestMixingStation rms WITH (NOLOCK) on rmsd.RequestMixingStationId = rms.Id
	INNER JOIN MixingStation.Package P WITH(NOLOCK) ON RPD.PackageId= P.Id
	LEFT JOIN MixingStation.PackagePersonalized PP (NOLOCK) ON RPD.PackagePersonalizedId = PP.Id
	LEFT JOIN [Inventory].[InventoryProduct] INP WITH(NOLOCK) ON INP.Id = P.ProductId
	INNER JOIN [MixingStation].[CampaignDetail] CD WITH(NOLOCK) ON rmsd.CampaignDetailId = CD.Id AND CD.CampaignStatus IN (5,6) --campañas que se encuentren en estado "Procesadas" - "Terminadas"
	INNER JOIN [MixingStation].[CampaignDetailUsers] CDU WITH(NOLOCK) ON CD.Id = CDU.CampaignDetailId AND CDU.UserRole = 1 --Supervisor
	left Join MixingStation.CampaignDetailUsers cdu2 WITH (NOLOCK) On rmsd.CampaignDetailId = cdu2.CampaignDetailId And cdu2.UserRole = 2
	inner join MixingStation.ProductionLine pl with(nolock) on rmsd.ProductionLineId = pl.Id
	inner join MixingStation.UnitDoseType ud with(nolock) on rmsd.UnitDoseTypeId = ud.Id
	INNER JOIN Security.Person AS SP ON CDU.UserId = SP.Id
	INNER JOIN Security.Person AS SP2 ON cdu2.UserId = SP2.Id
	OUTER APPLY (
		SELECT top 1 pc.PatientCode, pc.FunctionalUnitCode 
		FROM MixingStation.RequestMixingStationDetailPatients pc 
		WHERE pc.RequestMixingStationDetailId = rpd.RequestMixingStationDetailId
	) as rdp
	left join INPACIENT pac (nolock) on rdp.PatientCode = pac.IPCODPACI
	left join INUNIFUNC fu (nolock) on rdp.FunctionalUnitCode = fu.UFUCODIGO
	left join ADCENATEN ad (nolock) on ad.CODCENATE = rmsd.CareCenterCode
	left join MixingStation.ProductionScheduleDetail psd WITH (NOLOCK) ON cd.Id = psd.CampaignDetailId
	left join MixingStation.ProductionSchedule ps WITH (NOLOCK) ON psd.ProductionScheduleId = ps.Id
	Where RPD.Status = 5 And RPD.QualityStatus = 2 and RPD.BatchCode IS NOT NULL --Producto Terminado PT