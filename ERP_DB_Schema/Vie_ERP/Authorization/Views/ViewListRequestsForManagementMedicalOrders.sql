
CREATE VIEW [Authorization].[ViewListRequestsForManagementMedicalOrders]
AS
SELECT	CONCAT(h.EntityName, '-', h.EntityId, '-', h.ItemId) Id, h.EntityName, h.EntityId,
		h.CareCenterCode, cc.NOMCENATE CareCenterName,
		h.FunctionalUnitCode, fu.UFUDESCRI FunctionalUnitName,
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
		cg.Id CareGroupId, cg.Code CareGroupCode, cg.Name CareGroupName,
		ha.Id HealthAdministratorId, ha.Code HealthAdministratorCode, ha.Name HealthAdministratorName,
		h.PatientCode, RTRIM(LTRIM(p.IPNOMCOMP)) PatientName, p.IPDIRECCI PatientAddress, p.IPTELEFON PatientPhone,
		dbo.Edad(p.IPFECNACI, [Common].[GETDATE]()) PatientAge,	
		h.RequestDate,
		h.ProfessionalCode,
		h.Quantity,
		h.Type,
		h.ItemId, h.ItemCode, h.ItemCodeOriginal, h.ItemName, h.DescriptionCodeName,
		IIF(ISNULL(ptc.Id, 0) > 0 OR ISNULL(prd.Id, 0) > 0, 1, 0) Covered,
		IIF(ISNULL(ptc.Contracted, 0) = 1 OR ISNULL(prd.Contracted, 0) = 1, 1, 0) Contracted,
		IIF(ISNULL(ptc.Quoted, 0) = 1 OR ISNULL(prd.Quoted, 0) = 1, 1, 0) Quoted,
		IIF(csa.Id IS NULL, 0, ISNULL(csae.SusceptibleAuthorization, 1)) Authorized,
		mmo.Id ManagementMedicalOrderId,
		ISNULL(mmo.Status, 0) Status,
		h.Observations,
		ag.Id AuthorizationGroupId, ag.Code + ' - ' + ag.Name AuthorizationGroupCodeName,
		RTRIM(LTRIM(prof.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalCodeName
FROM
(
	-- Ordenes de imagenes ambulatorias
	SELECT	'HCORDIMAG' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			h.OBSSERIPS Observations
	FROM .HCORDIMAG h
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('6')) AND CAST(H.FECORDMED AS DATE) > DATEFROMPARTS(2022, 01, 01)
UNION ALL
	-- Ordenes de laboratorios ambulatorios
	SELECT	'HCORDLABO' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			h.OBSSERIPS Observations
	FROM .HCORDLABO h
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('6')) AND CAST(H.FECORDMED AS DATE) > DATEFROMPARTS(2022, 01, 01)
UNION ALL
	-- Ordenes de patologias ambulatorias
	SELECT	'HCORDPATO' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			h.OBSSERIPS Observations
	FROM .HCORDPATO h
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('6')) AND CAST(H.FECORDMED AS DATE) > DATEFROMPARTS(2022, 01, 01)
UNION ALL
	-- Ordenes de interconsultas ambulatorias
	SELECT	'HCORDINTE' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			h.OBSSERIPS Observations
	FROM .HCORDINTE h
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('5')) AND CAST(H.FECORDMED AS DATE) > DATEFROMPARTS(2022, 01, 01)
UNION ALL
	-- Ordenes de procedimientos no Qx ambulatorias
	SELECT	'HCORDPRON' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			h.OBSSERIPS Observations
	FROM .HCORDPRON h
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('5')) AND CAST(H.FECORDMED AS DATE) > DATEFROMPARTS(2022, 01, 01)
UNION ALL
	-- Ordenes de procedimientos Qx ambulatorias
	SELECT	'HCORDPROQ' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			h.OBSSERIPS Observations
	FROM .HCORDPROQ h
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('3')) AND CAST(H.FECORDMED AS DATE) > DATEFROMPARTS(2022, 01, 01)
UNION ALL
	-- Hemocomponentes
	SELECT 'HCORHEMCO' EntityName, 
			h.ID EntityId,		
			h.CODCENATE CareCenterCode, 
			ing.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			'' ProfessionalCode,
			hd.Quantity Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			hd.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			'' Observations
	FROM .ADINGRESO ing
	JOIN .HCORHEMCO h WITH(NOLOCK) ON ing.NUMINGRES = h.NUMINGRES
	JOIN 
	(
		SELECT	hd.HCORHEMCOID, 
				hd.CODSERIPS, 
				hd.TraceabilityPaperworkId, 
				hd.TraceabilityPaperworkEventsId, 
				hd.IDDESCRIPCIONRELACIONADA,
				COUNT(1) Quantity
		FROM .HCORHEMSER hd
		WHERE hd.ESTADO NOT IN (3)
		GROUP BY hd.HCORHEMCOID, hd.CODSERIPS, hd.TraceabilityPaperworkId, hd.TraceabilityPaperworkEventsId, hd.IDDESCRIPCIONRELACIONADA
	) hd ON h.ID = hd.HCORHEMCOID
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON hd.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND hd.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND CAST(H.FECORDMED AS DATE) > DATEFROMPARTS(2022, 01, 01)
UNION ALL
	-- Ordenes de control por la especialidad que atendió al paciente
	SELECT	'HCDESCOEX' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			hc.FECHISPAC RequestDate, 
			hc.CODPROSAL ProfessionalCode,
			1 Quantity,
			1 Type,			
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			hc.INDICAMED Observations
	FROM dbo.HCHISPACA hc
	JOIN .HCDESCOEX h  WITH(NOLOCK)ON hc.NUMINGRES = h.NUMINGRES AND hc.NUMEFOLIO = h.NUMEFOLIO
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE CAST(HC.FECHISPAC AS DATE) > DATEFROMPARTS(2022, 01, 01)
UNION ALL
	-- Medicamentos extramurales
	SELECT	'HCPRESCRA' EntityName, 
			h.ID EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECINIDOS RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANPEDPRO Quantity,
			2 Type,
			ip.Id ItemId,
			ip.Code ItemCode,
			h.CODPRODUC ItemCodeOriginal,
			ip.Name ItemName,
			NULL ContractDescriptionId,
			NULL DescriptionId,
			NULL DescriptionCodeName,
			'' Observations
	FROM .HCPRESCRA h
	JOIN Inventory.ATC atc  WITH(NOLOCK)ON h.CODPRODUC = atc.Code
	JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON atc.Id = ip.ATCId
	WHERE h.MANEXTPRO = 1 AND CAST(h.FECINIDOS AS DATE) > DATEFROMPARTS(2022, 01, 01)
) h
JOIN .ADCENATEN cc WITH(NOLOCK) ON h.CareCenterCode = cc.CODCENATE
JOIN .INUNIFUNC fu WITH(NOLOCK) ON h.FunctionalUnitCode = fu.UFUCODIGO
JOIN .ADINGRESO ing WITH(NOLOCK) ON h.AdmissionNumber = ing.NUMINGRES
JOIN Contract.CareGroup cg WITH(NOLOCK) ON ing.GENCAREGROUP = cg.Id
JOIN Contract.HealthAdministrator ha WITH(NOLOCK) ON ing.GENCONENTITY = ha.Id
JOIN .INPACIENT p WITH(NOLOCK) ON h.PatientCode = p.IPCODPACI
LEFT JOIN Contract.ProcedureCups ptc WITH(NOLOCK) ON h.Type = 1 AND cg.ProcedureTemplateId = ptc.ProceduresTemplateId AND h.ItemId = ptc.CupsId AND ISNULL(h.ContractDescriptionId, 0) = ISNULL(ptc.CUPSEntityContractDescriptionId, 0)
LEFT JOIN 
(
	SELECT	prd.ProductRateId, prd.ProductId, 
			MAX(prd.Id) Id, MAX(IIF(prd.Contracted = 1, 1, 0)) Contracted, MAX(IIF(prd.Quoted = 1, 1, 0)) Quoted,
			MIN(prd.InitialDate) InitialDate, MAX(prd.EndDate) EndDate
	FROM Inventory.ProductRateDetail prd
	GROUP BY prd.ProductRateId, prd.ProductId
) prd ON h.Type = 2 AND cg.ProductRateId = prd.ProductRateId AND h.ItemId = prd.ProductId AND CAST(h.RequestDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
LEFT JOIN
(
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			1 Type, 
			apce.CUPSEntityId ItemId,
			apce.ContractDescriptionId DescriptionId,
			apce.AuthorizationGroupId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap WITH(NOLOCK) ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce WITH(NOLOCK) ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa WITH(NOLOCK) ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
UNION ALL
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			2 Type, 
			apip.InventoryProductId ItemId,
			NULL DescriptionId,
			apip.AuthorizationGroupId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap WITH(NOLOCK) ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip WITH(NOLOCK) ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa WITH(NOLOCK) ON apip.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
) csa ON h.CareCenterCode = csa.CareCenterCode AND h.Type = csa.Type AND h.ItemId = csa.ItemId AND ISNULL(h.DescriptionId, 0) = ISNULL(csa.DescriptionId, 0)
left join [Authorization].AuthorizationGroup ag WITH(NOLOCK) on ag.Id = csa.AuthorizationGroupId
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae WITH(NOLOCK) ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND cg.Id = csae.CareGroupId
LEFT JOIN [Authorization].ManagementMedicalOrder mmo WITH(NOLOCK) ON h.EntityName = mmo.EntityName AND h.EntityId = mmo.EntityId AND h.ItemCodeOriginal = mmo.ItemCode
LEFT JOIN .HCHISPACA hc WITH(NOLOCK) ON h.Folio = hc.NUMEFOLIO AND h.PatientCode = hc.IPCODPACI
left join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = h.ProfessionalCode
WHERE mmo.Id IS NULL OR (mmo.Status = 1)