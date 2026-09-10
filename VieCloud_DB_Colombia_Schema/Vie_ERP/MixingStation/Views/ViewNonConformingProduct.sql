

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los productos no conformes (rechazados por control de calidad) generados en la estación de mezclas farmacéuticas. Integra el historial de estados de paquetes, el detalle de solicitudes de preparación, las campañas de producción procesadas o terminadas, y el catálogo de productos terminados del inventario, mostrando únicamente aquellos paquetes con estado ''Producto Rechazado'' y estado de calidad ''Rechazado'' que ya tienen lote asignado. Incluye información del paciente (cédula, nombre), unidad funcional, centro de atención, línea de producción, tipo de dosis unitaria, envase o bolsa utilizada, supervisor de calidad, operario responsable, número de campaña, fecha de procesamiento, lote, código y nombre del producto terminado, y fecha de vencimiento calculada según la configuración de días de expiración. Sirve para reportería y trazabilidad de productos rechazados en el proceso de preparación farmacéutica (mezclas), permitiendo identificar qué producto, de qué campaña, para qué paciente fue rechazado y quién era el supervisor responsable.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewNonConformingProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewNonConformingProduct';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que lista los productos terminados rechazados por control de calidad en la estación de mezclas, consolidando datos de la solicitud, campaña, línea de producción, paciente y supervisores para su gestión como producto no conforme.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewNonConformingProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de paquete debe tener Status=5 (Producto Rechazado) y QualityStatus=2 (Rechazado); El BatchCode del paquete no puede ser NULL (debe corresponder a Producto Terminado - PT); La campaña asociada (CampaignDetail) debe estar en estado 5 (Procesada) o 6 (Terminada); Debe existir al menos un usuario con UserRole=1 (Supervisor de calidad) asignado al detalle de campaña; Debe existir configuración en MixingStation.MixingStationSetting para calcular la fecha de vencimiento (ExpirationDays)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewNonConformingProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de vencimiento se calcula sumando los días configurados en MixingStationSetting.ExpirationDays a la fecha actual (GETDATE()); Solo expone productos terminados rechazados por calidad (Status=5 y QualityStatus=2); Solo se asocian campañas en estado Procesada (5) o Terminada (6); El supervisor de calidad siempre proviene de CampaignDetailUsers con UserRole=1; El usuario operario proviene de CampaignDetailUsers con UserRole=2; El paciente y unidad funcional se obtienen del primer registro (TOP 1) de RequestMixingStationDetailPatients para el detalle', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewNonConformingProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto No Conforme; Producto Terminado (PT); Control de calidad farmacéutico (Liberado/Rechazado/Reprocesado/Pendiente); Estación de mezclas; Campaña de producción; Línea de producción; Lote (Batch); Supervisor de calidad; Dosis unitaria; Paciente; Unidad funcional; Centro de atención; Fecha de vencimiento; Orden Médica / Solicitud Externa / Maquila / Inventario; Programación de producción', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewNonConformingProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewNonConformingProduct: Devuelve un registro por cada RequestPackageDetailStatus con Status=5, QualityStatus=2 y BatchCode no nulo, enriquecido con datos del producto, campaña, paciente, unidad funcional y centro de atención', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewNonConformingProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rmsd.Source = 1/2/3/4 → Etiqueta SourceName como ''Orden Médica'' / ''Solicitud Externa Paciente'' / ''Solicitud Externa Maquila'' / ''Solicitud Inventario'' else Cadena vacía; si RPD.Status = 3/4/5/6 → Mapea RequestPackageDetailstatus a ''Producto Liberado'' / ''Producto Reproceso'' / ''Producto Rechazado'' / ''Producto Anulado''; si RPD.Status = 2 → State = ''ProductoTerminado'' else NULL; si RPD.QualityStatus = 0/1/2/3 → QualityStatus = ''Pendiente'' / ''Liberado'' / ''Rechazado'' / ''Reprocesado''; si RPD.PackagePersonalizedId existe (PP no nulo) → PackageDescription usa código y descripción de PackagePersonalized else Usa código y descripción del Package estándar; si CD.CampaignStatus IN (5,6) → Solo se incluyen campañas Procesadas o Terminadas else Excluye el registro; si CDU.UserRole = 1 → Trae al Supervisor de Calidad (QualitySupervisor); si cdu2.UserRole = 2 → Trae al usuario operario/responsable secundario (UserName)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewNonConformingProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStation; MixingStation.Package; MixingStation.PackagePersonalized; Inventory.InventoryProduct; MixingStation.CampaignDetail; MixingStation.CampaignDetailUsers; MixingStation.ProductionLine; MixingStation.UnitDoseType; Security.Person; MixingStation.RequestMixingStationDetailPatients; MixingStation.MixingStationSetting; INPACIENT; INUNIFUNC; ADCENATEN; MixingStation.ProductionScheduleDetail; MixingStation.ProductionSchedule', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewNonConformingProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewNonConformingProduct';
GO
