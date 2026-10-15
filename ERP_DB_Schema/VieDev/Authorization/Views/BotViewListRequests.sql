





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
			[Authorization].[GetRequestUnitTime](ISNULL(tp.Status, 1), ISNULL(csae.RequestUnit, csa.RequestUnit), ISNULL(csae.RadicatedUnit, csa.RadicatedUnit), ISNULL(csae.DeliveryServiceUnit, csa.DeliveryServiceUnit))
		) RequestElapsedTime,
		IIF(ISNULL(tp.Status, 0) <> 16, 
		[Authorization].fnGetColor
		(
			[Authorization].[GetRequestTime](ISNULL(tp.Status, 1), ISNULL(csae.Request, csa.Request), ISNULL(csae.Radicated, csa.Radicated), ISNULL(csae.DeliveryService, csa.DeliveryService)),
			[Authorization].[GetRequestElapsedTime]
			(
				ISNULL(tpPr.PostponementDate, IIF(tpe.Id is null, h.RequestDate, IIF(tpe.Status = 1, tpe.CreationDate, IIF(tpe.Status = 2, tpe.AuthorizationDate, h.RequestDate)))), 
				[Authorization].[GetRequestUnitTime](ISNULL(tp.Status, 1), ISNULL(csae.RequestUnit, csa.RequestUnit), ISNULL(csae.RadicatedUnit, csa.RadicatedUnit), ISNULL(csae.DeliveryServiceUnit, csa.DeliveryServiceUnit))
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