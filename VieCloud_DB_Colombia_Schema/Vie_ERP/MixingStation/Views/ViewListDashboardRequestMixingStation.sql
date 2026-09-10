

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) de solicitudes activas en la estación de mezclas farmacéuticas. Consolida en una sola consulta las solicitudes de preparación de mezclas y dosis unitarias, junto con el detalle de cada ítem solicitado (medicamento, paquete o paquete personalizado), la línea de producción asignada, el centro de atención (interno o externo) y el paciente beneficiario. Muestra el estado clínico de la prescripción médica o la pauta nutricional asociada (iniciado, suspendido, alta médica, etc.), el usuario que generó la solicitud, y si aplica, la confirmación de dosis unitaria, permitiendo al personal de farmacia visualizar y gestionar en tiempo real las solicitudes pendientes de preparación, diferenciando entre órdenes médicas y solicitudes externas de paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardRequestMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardRequestMixingStation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida para el dashboard las solicitudes activas de la estación de mezclas, unificando órdenes médicas y solicitudes externas de paciente con solicitudes de maquila e inventario, junto con paciente, ítem, centro de atención y estado de prescripción.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La solicitud (RequestMixingStation) debe estar en Status = 2 (aprobada/activa).; El detalle (RequestMixingStationDetail) debe tener SendTo = 0 (aún no enviado) y CampaignDetailId NULL (no asociado a campaña).; Para el primer bloque (Source 1 y 2): el detalle debe tener Status<>3 y el paciente asociado (RequestMixingStationDetailPatients) Status = 1.; Para el segundo bloque (Source 3 y 4): el detalle debe tener Status = 1.; La relación con HCPRESCRA exige que ProductSusceptibleMixingStation.Origin = ''HCPRESCRA''; con HCNUTPAREC exige Origin = ''HCNUTPAREC''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen solicitudes con Status=2 y detalles aún no enviados (SendTo=0) y no asociados a campaña (CampaignDetailId IS NULL).; Las solicitudes con Source 1/2 siempre llevan información de paciente; las de Source 3/4 nunca llevan paciente (PatientCodeName=NULL, StatusHCPRESCRA=0).; ItemId siempre proviene jerárquicamente de PackagePersonalized → Package → ATC (primer no nulo).; El estado mostrado de prescripción proviene de HCPRESCRA.PREESTADO o, en su defecto, de HCNUTPAREC.STATUS; si no hay ninguno se considera 0.; Si la línea de producción no está asignada (ProductionLineId NULL), ProductionLineCodeName = ''Sin Asignar''.; La búsqueda de egreso (HCREGEGRE) se limita al primer registro coincidente por NUMINGRES e IPCODPACI (TOP 1).; En el bloque de Source 3/4 el filtro de estado de detalle es estrictamente Status=1; en Source 1/2 se aceptan todos los estados excepto 3.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListDashboardRequestMixingStation: Devuelve UNION ALL de dos conjuntos: (1) solicitudes con Source IN (1,2) — Orden Médica / Solicitud Externa Paciente — incluyendo paciente y estado HCPRESCRA/HCNUTPAREC; (2) solicitudes con Source IN (3,4) — Maquila / Inventario — sin paciente y con StatusHCPRESCRA=0.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rd.Source = 1 → RequestTypeName = ''Orden Médica''; si rd.Source = 2 → RequestTypeName = ''Solicitud Externa Paciente''; si rd.Source = 3 → RequestTypeName = ''Solicitud Externa Maquila''; si rd.Source = 4 → RequestTypeName = ''Solicitud Inventario''; si pp.Id IS NOT NULL → ItemType = 3 (''Paquete Personalizado'') else si p.Id IS NOT NULL → ItemType = 1 (''Paquete''); si no → ItemType = 2 (''Medicamento''); si udt.MSClass = 2 → ItemCodeName usa el campo Name del paquete/personalizado else ItemCodeName usa el campo Description; si cce.Id IS NOT NULL → CareCenterCodeName se construye con datos del centro externo (ExternalCareCenter) else se construye con datos de ADCENATEN (centro interno); si ISNULL(hcp.PREESTADO, hnpt.STATUS) = 1..7 → Mapea estado de prescripción a etiqueta: 1=Iniciado, 2=Tratamiento anulado/Alta médica, 3=Descontinuado, 4=Suspendido, 5=Plan manejo externo, 6=Sin existencia en kardex, 7=Terminado por salida del paciente else Si existe egr.FECALTPAC → ''Alta médica'', en otro caso vacío; si udt.MSClass = 2 AND existe registro en HCFARMEPD/HCNUTPAREC/INPROFSAL → RequestUser y RequestUserCodeName toman el usuario y nombre del profesional (CODUSUARI/NOMMEDICO) en lugar de rms.RequestUser else Se usa rms.RequestUser y per.Fullname (Security.Person); si rd.EntityName = ''RequestPackageDetailStatus'' → OrderField = 1 else OrderField = 2; si rmsdp.EntityName = ''ProductSusceptibleMixingStation'' → Se vincula al paciente vía dbo.INPACIENT (paciente interno); si rmsdp.EntityName = ''RequestUnitDoseExternalCareCenterPatientDetails'' → Se vincula al paciente vía MixingStation.PatientExternalCareCenter (paciente externo)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStation; MixingStation.RequestMixingStationDetail; MixingStation.UnitDoseType; MixingStation.RequestMixingStationDetailPatients; MixingStation.ProductionLine; Security.User; Security.Person; dbo.ADCENATEN; MixingStation.ExternalCareCenter; MixingStation.Package; Inventory.ATC; MixingStation.PackagePersonalized; dbo.INPACIENT; MixingStation.PatientExternalCareCenter; MedicalHistory.ProductSusceptibleMixingStation; dbo.HCPRESCRA; MixingStation.ConfirmationUnitDose; MedicalHistory.PharmaDose; dbo.HCFARMEPC; dbo.HCNUTPAREC; dbo.HCFARMEPD; dbo.INPROFSAL; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStation';
GO
