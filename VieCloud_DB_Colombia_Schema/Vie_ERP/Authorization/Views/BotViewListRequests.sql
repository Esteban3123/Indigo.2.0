

CREATE VIEW [Authorization].[BotViewListRequests]
AS

with temp_HCHISPACA as (
	select h.NUMEFOLIO, h.IPCODPACI, h.TIPHISPAC
	from .HCHISPACA h with(nolock)
	where ESTAFOLIO <> 0
),
temp_ADINGRESO as (
	select a.NUMINGRES, a.GENCAREGROUP, a.GENCONENTITY
	from .ADINGRESO a with(nolock)
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
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc with(nolock)
	JOIN [Authorization].AuthorizationPortfolio ap with(nolock) ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce with(nolock) ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa with(nolock) ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
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
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc with(nolock)
	JOIN [Authorization].AuthorizationPortfolio ap with(nolock) ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip with(nolock) ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa with(nolock) ON apip.Id = csa.AuthorizationPortfolioInventoryProductId
	WHERE ap.Status = 1
),
temp_ProductRateDetail as (
	SELECT	prd.ProductRateId, prd.ProductId, 
			MAX(prd.Id) Id, MAX(IIF(prd.Contracted = 1, 1, 0)) Contracted, MAX(IIF(prd.Quoted = 1, 1, 0)) Quoted,
			MIN(prd.InitialDate) InitialDate, MAX(prd.EndDate) EndDate
	FROM Inventory.ProductRateDetail prd with(nolock)
	GROUP BY prd.ProductRateId, prd.ProductId
),
temp_TraceabilityPaperworkAlert as (
	SELECT tpa.TraceabilityPaperworkId, 1 Alert
	FROM [Authorization].TraceabilityPaperworkAlert tpa with(nolock)
	WHERE tpa.Status = 1
	GROUP BY tpa.TraceabilityPaperworkId
),
temp_TraceabilityPaperworkPostponementReasons as (
	select MAX(Id) Id, TraceabilityPaperworkId
	from [Authorization].[TraceabilityPaperworkPostponementReasons] with(nolock)
	group by TraceabilityPaperworkId
)

SELECT	CONCAT(h.EntityName, '-', h.EntityId, '-', h.ItemId) Id, h.EntityName,cast( h.EntityId as integer) EntityId,
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
FROM [Authorization].[ViewListRequest_CTE] h with(nolock)																		 
JOIN .ADCENATEN cc with(nolock) ON h.CareCenterCode = cc.CODCENATE
JOIN .INUNIFUNC fu with(nolock) ON h.FunctionalUnitCode = fu.UFUCODIGO
JOIN temp_INPACIENT p with(nolock) ON h.PatientCode = p.IPCODPACI
JOIN temp_CSA csa with(nolock) ON h.CareCenterCode = csa.CareCenterCode AND h.Type = csa.Type AND h.ItemId = csa.ItemId AND ISNULL(h.ContractDescriptionId, 0) = ISNULL(csa.ContractDescriptionId, 0)
join [Authorization].AuthorizationGroup ag with(nolock) on ag.Id = csa.AuthorizationGroupId
LEFT JOIN [Authorization].TraceabilityPaperwork tp with(nolock) ON h.TraceabilityPaperworkId =tp.Id
left join temp_ADINGRESO ing with(nolock) ON h.AdmissionNumber = ing.NUMINGRES
left join Contract.CareGroup cg with(nolock) ON cg.Id = IIF(tp.CareGroupId is null, ing.GENCAREGROUP, tp.CareGroupId)
left join Contract.HealthAdministrator ha with(nolock) ON ha.Id = IIF(tp.HealthAdministratorId is null, ing.GENCONENTITY, tp.HealthAdministratorId) 
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae with(nolock) ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND cg.Id = csae.CareGroupId
LEFT JOIN Contract.ProcedureCups ptc with(nolock) ON h.Type = 1 AND cg.ProcedureTemplateId = ptc.ProceduresTemplateId AND h.ItemId = ptc.CupsId AND ISNULL(h.CUPSEntityContractDescriptionId, 0) = ISNULL(ptc.CUPSEntityContractDescriptionId, 0)
LEFT JOIN temp_ProductRateDetail prd with(nolock) ON h.Type = 2 AND cg.ProductRateId = prd.ProductRateId AND h.ItemId = prd.ProductId AND CAST(h.RequestDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe with(nolock) ON h.TraceabilityPaperworkEventsId = tpe.Id
left join Payroll.FunctionalUnit fuT with(nolock) on fuT.Id = tp.FunctionalUnitTargetId
left join .ADCENATEN ccT with(nolock) on ccT.CODCENATE = tp.CareCenterTargetCode
LEFT JOIN Security.[User] u on u.UserCode = tp.AssignUserCode
LEFT JOIN Security.Person per on per.Id = u.IdPerson
LEFT JOIN Common.ThirdParty third with(nolock) on third.Nit = RTRIM(LTRIM(h.PatientCode))
LEFT JOIN temp_TraceabilityPaperworkAlert tpa with(nolock) ON tp.Id = tpa.TraceabilityPaperworkId
LEFT JOIN temp_HCHISPACA hc with(nolock) ON h.Folio = hc.NUMEFOLIO AND h.PatientCode = hc.IPCODPACI
left join .INPROFSAL prof with(nolock) on prof.CODPROSAL = h.ProfessionalCode
left join .INDIAGNOS diag with(nolock) on diag.CODDIAGNO = h.DiagnosticCode
left join temp_TraceabilityPaperworkPostponementReasons tpPrTemp on tpPrTemp.TraceabilityPaperworkId = tp.Id
left join [Authorization].[TraceabilityPaperworkPostponementReasons] tpPr with(nolock) on tpPr.Id = tpPrTemp.Id
left join [Authorization].PostponementReasons pr with(nolock) on pr.Id = tpPr.PostponementReasonsId
WHERE ISNULL(csae.SusceptibleAuthorization, 1) = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista utilizada por el bot de autorizaciones para listar todas las solicitudes de autorización pendientes o en curso, combinando información del paciente (nombre, documento/cédula, dirección, teléfono, edad), su ingreso o admisión, el folio de historia clínica asociado, el servicio o producto solicitado (procedimiento CUPS o medicamento/insumo de inventario), el centro de atención y la unidad funcional. Integra los portafolios de autorización con sus configuraciones de tiempos (tiempo de solicitud, radicación y entrega) para calcular en tiempo real cuánto ha transcurrido desde la solicitud y si está dentro del plazo permitido, asignando un color de alerta (semáforo) según el nivel de cumplimiento. También expone si el servicio está cubierto por contrato, el usuario asignado para gestionar la solicitud, y alertas de seguimiento o aplazamientos, siendo la fuente principal de datos para que el bot gestione, consulte y priorice autorizaciones de servicios ambulatorios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'BotViewListRequests';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'BotViewListRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la lista de solicitudes de autorización ambulatoria con sus tiempos de respuesta, estados, alertas, aplazamientos, paciente, profesional y cobertura contractual, para el tablero/bot de gestión de autorizaciones.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'BotViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe ViewListRequest_CTE con las solicitudes base (folio, paciente, ítem, tipo, centro, unidad funcional, fechas); El ítem solicitado debe estar parametrizado en ConfigurationServicesAmbulatory para el centro de atención y tipo (servicio CUPS=1 o producto inventario=2); El portafolio de autorización (AuthorizationPortfolio) debe estar en estado activo (Status=1); El paciente debe existir en INPACIENT con ESTADOPAC=1; Los folios considerados de HCHISPACA deben tener ESTAFOLIO<>0; Los ingresos considerados de ADINGRESO deben tener IESTADOIN<>''A'' (no anulados)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'BotViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ítems pertenecientes a portafolios de autorización activos (AuthorizationPortfolio.Status=1); Se excluyen ítems marcados como no susceptibles de autorización en la excepción del grupo de atención; Solo se consideran alertas de trámite con Status=1; Pacientes inactivos (ESTADOPAC<>1) y folios anulados (ESTAFOLIO=0) e ingresos anulados (IESTADOIN=''A'') quedan fuera; El motivo de aplazamiento mostrado siempre es el de mayor Id por trámite (último registrado); Para tarifas de productos, la vigencia se valida con InitialDate≤RequestDate≤EndDate; El tiempo de respuesta y su unidad se eligen según el estado del trámite (Request si pendiente, Radicated si radicado, DeliveryService si entregado); El Id de la fila se forma como EntityName-EntityId-ItemId garantizando unicidad por entidad y servicio', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'BotViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.BotViewListRequests: Devuelve solo solicitudes cuya configuración/excepción tenga SusceptibleAuthorization=1 (ISNULL(csae.SusceptibleAuthorization,1)=1)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'BotViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si hc.TIPHISPAC IN (''I'',''O'',''PT'',''F'') → TypeClinicalHistory=1 (hospitalización/observación) else Otros tipos mapean: ''E'',''V''→2; ''N''→3; ''T''→4; ''P''→5; ''S''→6; ''NF''→7; ''B''→8; resto→0; si h.Type=1 (CUPS) y existe match en Contract.ProcedureCups por plantilla y CUPS → Se evalúa cobertura/contratado/cotizado vía ProcedureCups else Si h.Type=2 (producto) se evalúa contra ProductRateDetail con vigencia entre InitialDate y EndDate respecto a RequestDate; si ISNULL(ptc.Id,0)>0 OR ISNULL(prd.Id,0)>0 → IsCovered=1 (servicio/producto cubierto por contrato o tarifa) else IsCovered=0; si tp.CareGroupId IS NULL → CareGroup se toma del ingreso (ing.GENCAREGROUP) else Se toma del trámite de trazabilidad (tp.CareGroupId); si tp.HealthAdministratorId IS NULL → HealthAdministrator se toma del ingreso (ing.GENCONENTITY) else Se toma de tp.HealthAdministratorId; si Existe excepción en ConfigurationServicesAmbulatoryExceptions para el CareGroup → Tiempos (Request/Radicated/DeliveryService) y unidades se toman de la excepción (csae) else Se toman de la configuración base (csa); si tpe.Id IS NULL → Fecha base para tiempo transcurrido = h.RequestDate else Si tpe.Status=1 usa tpe.CreationDate; si tpe.Status=2 usa tpe.AuthorizationDate; en otro caso usa h.RequestDate; si existe tpPr.PostponementDate, ésta prevalece; si ISNULL(tp.Status,0)<>16 → ColorRequest se calcula con fnGetColor según tiempo permitido vs transcurrido else ColorRequest se basa en comparación de tpPr.PostponementDate vs hoy: futuro=1, hoy=2, pasado=3', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'BotViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.GetRequestTime; Authorization.GetRequestUnitTime; Authorization.GetRequestElapsedTime; Authorization.fnGetColor; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'BotViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.ViewListRequest_CTE; Authorization.AuthorizationPortfolioCareCenter; Authorization.AuthorizationPortfolio; Authorization.AuthorizationPortfolioCUPSEntity; Authorization.AuthorizationPortfolioInventoryProduct; Authorization.ConfigurationServicesAmbulatory; Authorization.ConfigurationServicesAmbulatoryExceptions; Authorization.AuthorizationGroup; Authorization.TraceabilityPaperwork; Authorization.TraceabilityPaperworkEvents; Authorization.TraceabilityPaperworkAlert; Authorization.TraceabilityPaperworkPostponementReasons; Authorization.PostponementReasons; Inventory.ProductRateDetail; Contract.CareGroup; Contract.HealthAdministrator; Contract.ProcedureCups; Payroll.FunctionalUnit; Security.User; Security.Person; Common.ThirdParty; dbo.HCHISPACA; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'BotViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'BotViewListRequests';
GO
