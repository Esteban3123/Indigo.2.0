

CREATE VIEW [Authorization].[ViewListRequestsTraceability]
AS

with temp_HCHISPACA as (
	select h.NUMEFOLIO, h.IPCODPACI, h.TIPHISPAC
	from .HCHISPACA h 
	where ESTAFOLIO <> 0
),
temp_ADINGRESO as (
	select a.NUMINGRES, a.GENCAREGROUP, a.GENCONENTITY
	from .ADINGRESO a 
	where IESTADOIN <> 'A'
),
temp_INPACIENT as (
	select IPCODPACI, IPNOMCOMP, IPDIRECCI, IPTELEFON, IPFECNACI
	from .INPACIENT p
	where ESTADOPAC = 1
),
temp_CSA as (
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			1 Type, 
			apce.CUPSEntityId ItemId,
			apce.ContractDescriptionId,
			csa.Request, csa.RequestUnit, 
			csa.Radicated, csa.RadicatedUnit,
			csa.DeliveryService, csa.DeliveryServiceUnit,
			apce.AuthorizationGroupId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc 
	JOIN [Authorization].AuthorizationPortfolio ap  ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce  ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa  ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
	UNION ALL
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			2 Type, 
			apip.InventoryProductId ItemId,
			NULL ContractDescriptionId,
			csa.Request, csa.RequestUnit, 
			csa.Radicated, csa.RadicatedUnit,
			csa.DeliveryService, csa.DeliveryServiceUnit,
			apip.AuthorizationGroupId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc 
	JOIN [Authorization].AuthorizationPortfolio ap  ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip  ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa  ON apip.Id = csa.AuthorizationPortfolioInventoryProductId
	WHERE ap.Status = 1
),
temp_ProductRateDetail as (
	SELECT	prd.ProductRateId, prd.ProductId, 
			MAX(prd.Id) Id, MAX(IIF(prd.Contracted = 1, 1, 0)) Contracted, MAX(IIF(prd.Quoted = 1, 1, 0)) Quoted,
			MIN(prd.InitialDate) InitialDate, MAX(prd.EndDate) EndDate
	FROM Inventory.ProductRateDetail prd 
	GROUP BY prd.ProductRateId, prd.ProductId
),
temp_TraceabilityPaperworkAlert as (
	SELECT tpa.TraceabilityPaperworkId, 1 Alert
	FROM [Authorization].TraceabilityPaperworkAlert tpa 
	WHERE tpa.Status = 1
	GROUP BY tpa.TraceabilityPaperworkId
),
temp_TraceabilityPaperworkPostponementReasons as (
	select MAX(Id) Id, TraceabilityPaperworkId
	from [Authorization].[TraceabilityPaperworkPostponementReasons] 
	group by TraceabilityPaperworkId
)

SELECT	CONCAT(h.EntityName, '-', h.EntityId, '-', h.ItemId) Id, h.EntityName, h.EntityId,
		h.CareCenterCode, CONCAT(h.CareCenterCode, ' - ', cc.NOMCENATE) CareCenterCodeName,
		h.FunctionalUnitCode, fu.UFUDESCRI FunctionalUnitName, CONCAT(h.FunctionalUnitCode, ' - ', fu.UFUDESCRI) FunctionalUnitCodeName,
		h.AdmissionNumber, h.Folio,
		CASE hc.TIPHISPAC
			WHEN 'I' THEN 1
			WHEN 'E' THEN 2
			WHEN 'O' THEN 1
			WHEN 'N' THEN 3
			WHEN 'PT' THEN 1
			WHEN 'NF' THEN 7
			WHEN 'V' THEN 2
			WHEN 'F' THEN 1
			WHEN 'T' THEN 4
			WHEN 'S' THEN 6
			WHEN 'P' THEN 5
			WHEN 'B' THEN 8
			ELSE 0
		END TypeClinicalHistory,
		cg.Id CareGroupId, cg.Code CareGroupCode, cg.Name CareGroupName, CONCAT(cg.Code, ' - ', cg.Name) CareGroupCodeName,
		ha.Id HealthAdministratorId, ha.Code HealthAdministratorCode, ha.Name HealthAdministratorName, CONCAT(ha.Code, ' - ', ha.Name) HealthAdministratorCodeName,
		h.PatientCode, RTRIM(LTRIM(p.IPNOMCOMP)) PatientName, p.IPDIRECCI PatientAddress, p.IPTELEFON PatientPhone,
		dbo.Edad(p.IPFECNACI, GETDATE()) PatientAge,	
		h.RequestDate,
		h.ProfessionalCode,
		h.Quantity,
		h.Type,
		h.ItemId ServiceId, h.ItemCode ServiceCode, h.ItemCodeOriginal, CONCAT(h.ItemCode, ' - ',h.ItemName) ServiceDescription, 
		h.ContractDescriptionId, h.DescriptionCodeName ContractDescriptionCodeName,
		IIF(ISNULL(ptc.Id, 0) > 0 OR ISNULL(prd.Id, 0) > 0, 1, 0) IsCovered,
		IIF(ISNULL(ptc.Contracted, 0) = 1 OR ISNULL(prd.Contracted, 0) = 1, 1, 0) Contracted,
		IIF(ISNULL(ptc.Quoted, 0) = 1 OR ISNULL(prd.Quoted, 0) = 1, 1, 0) Quoted,
		ISNULL(tp.AssignUserCode, '') AssignUserCode, IIF(u.UserCode IS NULL, '', CONCAT(u.UserCode, ' - ', per.Fullname)) AssignUser,
		[Authorization].[GetRequestTime](ISNULL(tp.Status, 1), ISNULL(csae.Request, csa.Request), ISNULL(csae.Radicated, csa.Radicated), ISNULL(csae.DeliveryService, csa.DeliveryService)) RequestTime,
		[Authorization].[GetRequestUnitTime](ISNULL(tp.Status, 1), ISNULL(csae.RequestUnit, csa.RequestUnit), ISNULL(csae.RadicatedUnit, csa.RadicatedUnit), ISNULL(csae.DeliveryServiceUnit, csa.DeliveryServiceUnit)) RequestUnitTime,
		[Authorization].[GetRequestElapsedTime]
		(
			ISNULL(tpPr.PostponementDate, IIF(tpe.Id is null, h.RequestDate, IIF(tpe.Status = 1, tpe.CreationDate, IIF(tpe.Status = 2, tpe.AuthorizationDate, h.RequestDate)))),
			[Authorization].[GetRequestUnitTime](ISNULL(tp.Status, 1), ISNULL(csae.RequestUnit, csa.RequestUnit), ISNULL(csae.RadicatedUnit, csa.RadicatedUnit), ISNULL(csae.DeliveryServiceUnit, csa.DeliveryServiceUnit)),
			GETDATE()
		) RequestElapsedTime,
		IIF(ISNULL(tp.Status, 0) <> 16,
		[Authorization].fnGetColor
		(
			[Authorization].[GetRequestTime](ISNULL(tp.Status, 1), ISNULL(csae.Request, csa.Request), ISNULL(csae.Radicated, csa.Radicated), ISNULL(csae.DeliveryService, csa.DeliveryService)),
			[Authorization].[GetRequestElapsedTime]
			(
				ISNULL(tpPr.PostponementDate, IIF(tpe.Id is null, h.RequestDate, IIF(tpe.Status = 1, tpe.CreationDate, IIF(tpe.Status = 2, tpe.AuthorizationDate, h.RequestDate)))),
				[Authorization].[GetRequestUnitTime](ISNULL(tp.Status, 1), ISNULL(csae.RequestUnit, csa.RequestUnit), ISNULL(csae.RadicatedUnit, csa.RadicatedUnit), ISNULL(csae.DeliveryServiceUnit, csa.DeliveryServiceUnit)),
				GETDATE()
			)
		), IIF(cast(tpPr.PostponementDate as date) > cast(GETDATE() as date), 1, IIF(cast(tpPr.PostponementDate as date) = cast(GETDATE() as date), 2, 3))) ColorRequest,
		h.TraceabilityPaperworkId, ISNULL(tp.Status, 0) TraceabilityPaperworkStatus,
		h.TraceabilityPaperworkEventsId, tpe.Status TraceabilityPaperworkEventsStatus,	
		tp.AuthorizationSourceId, ISNULL(tp.IsManual, 0) IsManual,
		h.Observations,
		ISNULL(tpa.Alert, 0) Alert,
		third.Id PatientThirdPartyId,
		ag.Id AuthorizationGroupId, ag.Code + ' - ' + ag.Name AuthorizationGroupCodeName,
		RTRIM(LTRIM(prof.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalCodeName,
		RTRIM(LTRIM(ccT.CODCENATE)) + ' - ' + RTRIM(LTRIM(ccT.NOMCENATE)) CareCenterTargetCodeName,
		fuT.Code + ' - ' + fuT.Name FunctionalUnitTargetCodeName,
		h.DiagnosticCode,
		RTRIM(LTRIM(h.DiagnosticCode)) + ' - ' + RTRIM(LTRIM(diag.NOMDIAGNO)) DiagnosticDescription,
		tpPrTemp.Id TraceabilityPaperworkPostponementReasonsId,
		tpPr.PostponementReasonsId, tpPr.PostponementDate, tpPr.CreationDate PostponementCreationDate, tpPr.CreationUser PostponementCreationUser,
		pr.Code + ' - ' + pr.Name PostponementCodeName,
		tp.PreviousStatus
FROM [Authorization].[ViewListRequest_CTE] h 
JOIN .ADCENATEN cc  ON h.CareCenterCode = cc.CODCENATE
JOIN .INUNIFUNC fu  ON h.FunctionalUnitCode = fu.UFUCODIGO
JOIN temp_INPACIENT p  ON h.PatientCode = p.IPCODPACI
JOIN temp_CSA csa  ON h.CareCenterCode = csa.CareCenterCode AND h.Type = csa.Type AND h.ItemId = csa.ItemId AND ISNULL(h.ContractDescriptionId, 0) = ISNULL(csa.ContractDescriptionId, 0)
join [Authorization].AuthorizationGroup ag  on ag.Id = csa.AuthorizationGroupId
LEFT JOIN [Authorization].TraceabilityPaperwork tp  ON h.TraceabilityPaperworkId =tp.Id
left join temp_ADINGRESO ing  ON h.AdmissionNumber = ing.NUMINGRES
left join Contract.CareGroup cg  ON cg.Id = IIF(tp.CareGroupId is null, ing.GENCAREGROUP, tp.CareGroupId)
left join Contract.HealthAdministrator ha  ON ha.Id = IIF(tp.HealthAdministratorId is null, ing.GENCONENTITY, tp.HealthAdministratorId) 
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae  ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND cg.Id = csae.CareGroupId
LEFT JOIN Contract.ProcedureCups ptc  ON h.Type = 1 AND cg.ProcedureTemplateId = ptc.ProceduresTemplateId AND h.ItemId = ptc.CupsId AND ISNULL(h.CUPSEntityContractDescriptionId, 0) = ISNULL(ptc.CUPSEntityContractDescriptionId, 0)
LEFT JOIN temp_ProductRateDetail prd  ON h.Type = 2 AND cg.ProductRateId = prd.ProductRateId AND h.ItemId = prd.ProductId AND CAST(h.RequestDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe  ON h.TraceabilityPaperworkEventsId = tpe.Id
left join Payroll.FunctionalUnit fuT  on fuT.Id = tp.FunctionalUnitTargetId
left join .ADCENATEN ccT  on ccT.CODCENATE = tp.CareCenterTargetCode
LEFT JOIN Security.[User] u on u.UserCode = tp.AssignUserCode
LEFT JOIN Security.Person per on per.Id = u.IdPerson
LEFT JOIN Common.ThirdParty third  on third.Nit = RTRIM(LTRIM(h.PatientCode))
LEFT JOIN temp_TraceabilityPaperworkAlert tpa  ON tp.Id = tpa.TraceabilityPaperworkId
LEFT JOIN temp_HCHISPACA hc  ON h.Folio = hc.NUMEFOLIO AND h.PatientCode = hc.IPCODPACI
left join .INPROFSAL prof  on prof.CODPROSAL = h.ProfessionalCode
left join .INDIAGNOS diag  on diag.CODDIAGNO = h.DiagnosticCode
left join temp_TraceabilityPaperworkPostponementReasons tpPrTemp on tpPrTemp.TraceabilityPaperworkId = tp.Id
left join [Authorization].[TraceabilityPaperworkPostponementReasons] tpPr  on tpPr.Id = tpPrTemp.Id
left join [Authorization].PostponementReasons pr  on pr.Id = tpPr.PostponementReasonsId
WHERE ISNULL(csae.SusceptibleAuthorization, 1) = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de trazabilidad de solicitudes de autorización de servicios y medicamentos. Integra datos del paciente (identificación, nombre, dirección, teléfono, edad), su ingreso o admisión, el folio de historia clínica, el centro de atención y la unidad funcional, junto con la configuración de tiempos permitidos para cada servicio (CUPS) o producto de inventario según el portafolio de autorización vigente. Permite hacer seguimiento del estado de cada solicitud de autorización, calculando el tiempo transcurrido, el color de alerta por vencimiento de plazo, si el servicio está contratado o cotizado, si tiene alertas activas o aplazamientos, y a qué usuario autorizador está asignada. Es la fuente principal para tableros y reportes de gestión de autorizaciones ambulatorias, control de tiempos de respuesta y auditoría del proceso de autorización por entidad aseguradora, grupo de atención y centro de atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequestsTraceability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequestsTraceability';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de trazabilidad de solicitudes de autorización ambulatoria que combina datos del paciente, profesional, centro/unidad, cobertura contractual, tiempos configurados, eventos y aplazamientos para monitorear el estado y SLA de cada trámite.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La historia clínica (HCHISPACA) debe tener ESTAFOLIO distinto de 0 para ser considerada; Los ingresos administrativos (ADINGRESO) deben tener IESTADOIN distinto de ''A'' (no anulados); El paciente (INPACIENT) debe tener ESTADOPAC = 1 (activo); El portafolio de autorización (AuthorizationPortfolio) debe tener Status = 1 (activo); Las alertas de trazabilidad consideradas deben tener Status = 1; Solo se incluyen filas donde la excepción de configuración (csae.SusceptibleAuthorization) sea 1 o esté nula (susceptible de autorización)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone solicitudes susceptibles de autorización (SusceptibleAuthorization=1 o NULL por defecto); Excluye historias clínicas con ESTAFOLIO=0, ingresos anulados (IESTADOIN=''A'') y pacientes inactivos (ESTADOPAC<>1); Solo considera portafolios de autorización activos (Status=1); El identificador único de fila se construye como EntityName-EntityId-ItemId; La cobertura (IsCovered/Contracted/Quoted) se evalúa contra CUPS si Type=1 o ProductRateDetail si Type=2, nunca ambos; Cuando el estado del trámite es 16, el color se determina exclusivamente por la fecha de aplazamiento respecto a hoy; Para productos, la tarifa aplicable se determina porque RequestDate cae dentro de [InitialDate, EndDate] del detalle de tarifa; Si la trazabilidad no tiene CareGroup/HealthAdministrator propios, se heredan del ingreso administrativo', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewListRequestsTraceability: Devuelve un listado con un Id sintético CONCAT(EntityName,''-'',EntityId,''-'',ItemId) por cada solicitud susceptible de autorización.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si hc.TIPHISPAC = ''I'',''O'',''PT'',''F'' → TypeClinicalHistory = 1 (hospitalización/observación) else Otros tipos mapean: ''E''/''V''→2, ''N''→3, ''T''→4, ''P''→5, ''S''→6, ''NF''→7, ''B''→8, ELSE 0; si ISNULL(ptc.Id,0) > 0 OR ISNULL(prd.Id,0) > 0 → IsCovered = 1 (servicio/producto cubierto por contrato o tarifa) else IsCovered = 0; si ptc.Contracted = 1 OR prd.Contracted = 1 → Contracted = 1 else Contracted = 0; si ptc.Quoted = 1 OR prd.Quoted = 1 → Quoted = 1 else Quoted = 0; si tp.CareGroupId IS NULL → CareGroup se toma del ingreso (ing.GENCAREGROUP) else CareGroup se toma de la trazabilidad (tp.CareGroupId); si tp.HealthAdministratorId IS NULL → HealthAdministrator se toma del ingreso (ing.GENCONENTITY) else Se toma de la trazabilidad (tp.HealthAdministratorId); si tpe.Id IS NULL → Fecha base de SLA = h.RequestDate else Si tpe.Status=1 usa CreationDate; si Status=2 usa AuthorizationDate; en otro caso h.RequestDate; si tpPr.PostponementDate NOT NULL → La fecha base del cálculo de tiempo transcurrido se reemplaza por PostponementDate; si ISNULL(tp.Status,0) <> 16 → ColorRequest se calcula con fnGetColor sobre tiempo configurado vs. transcurrido else ColorRequest se basa en comparación de PostponementDate vs hoy: futuro=1, hoy=2, pasado=3; si csae existe para (csa, CareGroup) → Se usan los tiempos/unidades de excepción (csae) else Se usan los tiempos/unidades base (csa); si Type = 1 → Cobertura se valida contra Contract.ProcedureCups (CUPS) else Si Type = 2, cobertura se valida contra Inventory.ProductRateDetail (productos)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.GetRequestTime; Authorization.GetRequestUnitTime; Authorization.GetRequestElapsedTime; Authorization.fnGetColor; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.ViewListRequest_CTE; HCHISPACA; ADINGRESO; INPACIENT; Authorization.AuthorizationPortfolioCareCenter; Authorization.AuthorizationPortfolio; Authorization.AuthorizationPortfolioCUPSEntity; Authorization.AuthorizationPortfolioInventoryProduct; Authorization.ConfigurationServicesAmbulatory; Authorization.ConfigurationServicesAmbulatoryExceptions; Authorization.AuthorizationGroup; Authorization.TraceabilityPaperwork; Authorization.TraceabilityPaperworkEvents; Authorization.TraceabilityPaperworkAlert; Authorization.TraceabilityPaperworkPostponementReasons; Authorization.PostponementReasons; Inventory.ProductRateDetail; Contract.CareGroup; Contract.HealthAdministrator; Contract.ProcedureCups; Payroll.FunctionalUnit; Security.User; Security.Person; Common.ThirdParty; ADCENATEN; INUNIFUNC; INPROFSAL; INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsTraceability';
GO
