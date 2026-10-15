







CREATE VIEW [Authorization].[ViewRequests]
AS
SELECT	CONCAT(h.EntityName, '-', h.EntityId, '-', h.ItemId) Id, h.EntityName, h.EntityId,
		CONCAT(RTRIM(h.PatientCode), ' - ', p.IPNOMCOMP) Patient,
		h.AdmissionNumber, h.Folio,
		CONCAT(RTRIM(h.CareCenterCode), ' - ', cc.NOMCENATE) CareCenter,
		CONCAT(RTRIM(h.FunctionalUnitCode), ' - ', fu.UFUDESCRI) FunctionalUnit,		
		CONCAT(cg.Code, ' - ', cg.Name) CareGroup,
		CONCAT(ha.Code, ' - ', ha.Name) HealthAdministrator,
		h.RequestDate,
		h.ProfessionalCode Professional,		
		h.Type,
		h.ItemCodeOriginal,
		CONCAT(h.ItemCode, ' - ', h.ItemName) ItemCodeName, 
		h.DescriptionCodeName,
		h.Quantity,
		h.FinancedResourceUPC,
		IIF(ISNULL(ptc.Id, 0) > 0 OR ISNULL(prd.Id, 0) > 0, 1, 0) Covered,
		IIF(ISNULL(ptc.Contracted, 0) = 1 OR ISNULL(prd.Contracted, 0) = 1, 1, 0) Contracted,
		IIF(ISNULL(ptc.Quoted, 0) = 1 OR ISNULL(prd.Quoted, 0) = 1, 1, 0) Quoted,
		IIF(csa.Id IS NULL AND S.CODCONCEC IS NULL, 0, iif(s.SUSCEPTIB = 1 and (select count(*) from ADCONFSERD Ex WHERE EX.IDADCONFSER = s.CODCONCEC and Ex.IDCaregroup = cg.ID) > 0  ,0 ,1)) Authorized,
		ISNULL
		(
			h.OrderStatus,
			IIF
			(
				S.CODCONCEC IS NOT NULL, 
				CASE WHEN S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR (s.SUSCEPTIB = 1 and (select count(*) from ADCONFSERD Ex WHERE EX.IDADCONFSER = s.CODCONCEC and Ex.IDCaregroup = cg.ID) > 0  OR SS.PROESTADO IS NULL)  then 'No requiere autorización'
					WHEN SS.PROESTADO = 1 then 'Pendiente Solicitud'
					WHEN SS.PROESTADO = 2 then 'Solicitado'
					WHEN SS.PROESTADO = 3 then 'Autorizado'
					WHEN SS.PROESTADO = 4 then 'Anulado'
					WHEN SS.PROESTADO = 5 then 'No autorizado'
					ELSE 'Pendiente por Autorizar'
				END,
				CONCAT
				(
					CASE mmo.Status
						WHEN 1 THEN 'Cancelado, requiere desistimiento. '
						WHEN 2 THEN 'Cancelado. '
						WHEN 3 THEN 'Confirmado. '
						ELSE ''
					END,
					CASE ISNULL(h.Status, 1)
						WHEN 1 THEN 'Pendiente por Autorizar.'
						WHEN 2 THEN 'Autorizado. '
						WHEN 3 THEN 'No autorizado. '
						ELSE 'No requiere autorización.'
					END
				)
			)
		) StatusName
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
			ce.FinancedResourceUPC, 
			CAST
			(
				IIF
				(
					h.SERREAINT = 1 OR h.ESTSERIPS IN ('2','3','4'),
					'Realizado',
					IIF(h.ESTSERIPS = '6', 'Anulado', NULL)
				) AS VARCHAR(MAX)
			) OrderStatus,
			tpe.Status
	FROM .HCORDIMAG h
	JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on h.TraceabilityPaperworkEventsId = tpe.Id
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
			ce.FinancedResourceUPC, 
			CAST
			(
				IIF
				(
					h.ESTSERIPS IN ('3','4'),
					'Realizado',
					IIF(h.ESTSERIPS = '6', 'Anulado', NULL)
				) AS VARCHAR(MAX)
			) OrderStatus,
			tpe.Status
	FROM .HCORDLABO h
	JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on h.TraceabilityPaperworkEventsId = tpe.Id
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
			ce.FinancedResourceUPC, 
			CAST
			(
				IIF
				(
					h.ESTSERIPS IN ('2','3','4'),
					'Realizado',
					IIF(h.ESTSERIPS = '6', 'Anulado', NULL)
				) AS VARCHAR(MAX)
			) OrderStatus,
			tpe.Status
	FROM .HCORDPATO h
	JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on h.TraceabilityPaperworkEventsId = tpe.Id
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
			ce.FinancedResourceUPC, 
			CAST(IIF(h.NUMFOLINT IS NULL, NULL, 'Con Respuesta') AS VARCHAR(MAX)) OrderStatus,
			tpe.Status
	FROM .HCORDINTE h
	JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on h.TraceabilityPaperworkEventsId = tpe.Id
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
			ce.FinancedResourceUPC, 
			CAST(IIF(h.ESTSERIPS = '1', NULL, 'Realizado') AS VARCHAR(MAX)) OrderStatus,
			tpe.Status
	FROM .HCORDPRON h
	JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on h.TraceabilityPaperworkEventsId = tpe.Id
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
			ce.FinancedResourceUPC, 
			CAST
			(
				CASE h.ESTSERIPS
					WHEN '2' THEN 'Programado'
					WHEN '3' THEN 'Cancelado'
					WHEN '4' THEN 'Resultado Revisado'
				END AS VARCHAR(MAX)
			) OrderStatus,
			tpe.Status
	FROM .HCORDPROQ h
	JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on h.TraceabilityPaperworkEventsId = tpe.Id
UNION ALL
	-- Hemocomponentes
	SELECT 'HCORHEMSER' EntityName, 
			hd.ID EntityId,		
			h.CODCENATE CareCenterCode, 
			ing.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			COALESCE(HEMBOL.PROFSOLRES, HEMBOL.PROFSOLTRA, '') ProfessionalCode,
			1 Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			hd.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			cecd.Id ContractDescriptionId,
			cd.Id DescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			ce.FinancedResourceUPC,
			CAST
			(
				CASE hd.ESTADO
					WHEN '2' THEN 'Realizado'
					WHEN '3' THEN 'Anulado'
				END AS VARCHAR(MAX)
			) OrderStatus,
			tpe.Status
	FROM .HCORHEMBOL AS HEMBOL
	JOIN .HCORHEMCO h ON HEMBOL.HCORHEMCOID = h.ID
	JOIN .ADINGRESO ing ON ING.NUMINGRES = h.NUMINGRES
	JOIN .HCORHEMSER hd ON h.ID = hd.HCORHEMCOID
	JOIN Contract.CUPSEntity ce ON hd.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND hd.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on hd.TraceabilityPaperworkEventsId = tpe.Id
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
			ce.FinancedResourceUPC,
			'' OrderStatus,
			tpe.Status
	FROM dbo.HCHISPACA hc
	JOIN .HCDESCOEX h ON hc.NUMINGRES = h.NUMINGRES AND hc.NUMEFOLIO = h.NUMEFOLIO
	JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on h.TraceabilityPaperworkEventsId = tpe.Id
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
			IIF(pp.DiagnosticId IS NULL, 0, 1) FinancedResourceUPC, 
			CAST
			(
				CASE h.PREESTADO
					WHEN 2 THEN 'Ciclo Completado'
					WHEN 3 THEN 'Tratamiento Descontinuado'
					WHEN 4 THEN 'Tratamiento Suspendido'
					WHEN 7 THEN 'Tratamiento Terminado'
				END AS VARCHAR(MAX)
			) OrderStatus,
			tpe.Status
	FROM .HCPRESCRD h
	join .HCPRESCRA pres on pres.NUMINGRES = h.NUMINGRES and pres.NUMEFOLIO = h.NUMEFOLIO and pres.CODPRODUC = h.CODPRODUC
	JOIN Inventory.ATC atc ON h.CODPRODUC = atc.Code
	JOIN Inventory.InventoryProduct ip ON atc.Id = ip.ATCId
	LEFT JOIN 
	(
		SELECT diag.NUMINGRES, diag.NUMEFOLIO, MIN(id.Id) DiagnosticId
		FROM dbo.INDIAGNOP diag
		JOIN Inventory.Diagnostic id ON diag.CODDIAGNO = id.Code
		WHERE CODDIAPRI = 1
		GROUP BY diag.NUMINGRES, diag.NUMEFOLIO
	) id ON h.NUMINGRES = id.NUMINGRES AND h.NUMEFOLIO = id.NUMEFOLIO
	LEFT JOIN
	(
		SELECT ProductId, DiagnosticId
		FROM Inventory.POSPathologies
		GROUP BY ProductId, DiagnosticId
	) pp ON ip.Id = pp.ProductId AND id.DiagnosticId = pp.DiagnosticId
	LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe on h.TraceabilityPaperworkEventsId = tpe.Id
) h
JOIN .ADCENATEN cc ON h.CareCenterCode = cc.CODCENATE
JOIN .INUNIFUNC fu ON h.FunctionalUnitCode = fu.UFUCODIGO
JOIN .ADINGRESO ing ON h.AdmissionNumber = ing.NUMINGRES
JOIN Contract.CareGroup cg ON ing.GENCAREGROUP = cg.Id
JOIN Contract.HealthAdministrator ha ON ing.GENCONENTITY = ha.Id
JOIN .INPACIENT p ON h.PatientCode = p.IPCODPACI
LEFT JOIN Contract.ProcedureCups ptc ON h.Type = 1 AND cg.ProcedureTemplateId = ptc.ProceduresTemplateId AND h.ItemId = ptc.CupsId AND ISNULL(h.ContractDescriptionId, 0) = ISNULL(ptc.CUPSEntityContractDescriptionId, 0)
LEFT JOIN Inventory.ProductRateDetail prd ON h.Type = 2 AND cg.ProductRateId = prd.ProductRateId AND h.ItemId = prd.ProductId AND CAST(h.RequestDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
LEFT JOIN
(
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			1 Type, 
			apce.CUPSEntityId ItemId,
			apce.ContractDescriptionId DescriptionId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
UNION ALL
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			2 Type, 
			apip.InventoryProductId ItemId,
			NULL DescriptionId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apip.Id = csa.AuthorizationPortfolioInventoryProductId
	WHERE ap.Status = 1
) csa ON h.CareCenterCode = csa.CareCenterCode AND h.Type = csa.Type AND h.ItemId = csa.ItemId AND ISNULL(h.DescriptionId, 0) = ISNULL(csa.DescriptionId, 0)
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND cg.Id = csae.CareGroupId
LEFT JOIN [Authorization].ManagementMedicalOrder mmo ON h.EntityName = mmo.EntityName AND h.EntityId = mmo.EntityId
LEFT OUTER JOIN .ADCONFSER S with(nolock) ON S.CODSERIPS = h.ItemCode  
LEFT OUTER JOIN .ADAUTOSER ss with(nolock) on SS.IPCODPACI = h.PatientCode AND SS.NUMINGRES = h.AdmissionNumber AND SS.CODSERIPS = h.ItemCode and SS.NUMEFOLIO = h.Folio

UNION ALL

SELECT	CONCAT('ADRADICACIONQX', '-', h.ID) Id, 'ADRADICACIONQX' AS EntityName, h.ID AS EntityId,
		CONCAT(RTRIM(h.IPCODPACI), ' - ', p.IPNOMCOMP) Patient,
		'' AS AdmissionNumber, '' AS Folio,
		CONCAT(RTRIM(h.CODCENATE), ' - ', cc.NOMCENATE) CareCenter,
		'Ambulatorio' FunctionalUnit,		
		CONCAT(cg.Code, ' - ', cg.Name) CareGroup,
		CONCAT(ha.Code, ' - ', ha.Name) HealthAdministrator,
		h.FECHARADIC RequestDate, 
		h.CODPROSAL Professional,
		1 AS Type,
		h.QXPRINCIPAL ItemCodeOriginal,
		CONCAT(ce.Code, ' - ', ce.Description) ItemCodeName, 
		CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
		1 AS Quantity,
		ce.FinancedResourceUPC,
		IIF(ISNULL(ptc.Id, 0) > 0, 1, 0) Covered,
		ISNULL(ptc.Contracted, 0) Contracted,
		ISNULL(ptc.Quoted, 0) Quoted,
		CASE WHEN (select top 1 CAMPOFECHA  from .ADRADICACIONQXD D with(nolock) where  D.IDRADICACIONQX = h.ID AND D.TIPOCAMPO = 1) IS NULL THEN 0 ELSE 1 END Authorized,
		concat(CASE h.ESTADO when 1 then 'Radicado. ' when 2 then 'Confirmado. ' when 3 then 'Anulado. ' else '' end,
		CASE WHEN (select top 1 CAMPOFECHA  from .ADRADICACIONQXD D with(nolock) where  D.IDRADICACIONQX = h.ID AND D.TIPOCAMPO = 1) IS NULL THEN 'No requiere autorización.'
		ELSE 'Autorizado.'
		END) AS StatusName
	FROM .ADRADICACIONQX h WITH(NOLOCK)
	INNER JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON h.QXPRINCIPAL = ce.Code
	INNER JOIN .ADCENATEN cc WITH(NOLOCK) ON h.CODCENATE = cc.CODCENATE
	INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON h.GENCAREGROUP = cg.Id
	INNER JOIN Contract.HealthAdministrator ha WITH(NOLOCK) ON h.GENCONENTITY = ha.Id
	INNER JOIN .INPACIENT p WITH(NOLOCK) ON h.IPCODPACI = p.IPCODPACI
	LEFT OUTER JOIN Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT OUTER JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON cecd.ContractDescriptionId = cd.Id	
	LEFT OUTER JOIN Contract.ProcedureCups ptc WITH(NOLOCK) ON cg.ProcedureTemplateId = ptc.ProceduresTemplateId AND ce.Id = ptc.CupsId AND ISNULL(cecd.Id, 0) = ISNULL(ptc.CUPSEntityContractDescriptionId, 0)

UNION ALL

SELECT	
		CONCAT('HCORDIALI', '-', A.AUTO) Id, 'HCORDIALI' AS EntityName, h.ID AS EntityId,
		CONCAT(RTRIM(A.IPCODPACI), ' - ', H.IPNOMCOMP) Patient,
		A.NUMINGRES AS AdmissionNumber, A.NUMEFOLIO AS Folio,
		CONCAT(RTRIM(A.CODCENATE), ' - ', B.NOMCENATE) CareCenter,
		CONCAT(RTRIM(A.UFUCODIGO), ' - ', C.UFUDESCRI) FunctionalUnit,	
		CONCAT(F.Code, ' - ', F.Name) CareGroup,
		CONCAT(G.Code, ' - ', G.Name) HealthAdministrator,
		A.FECRECEXA RequestDate, 
		A.CODPROSAL Professional,
		1 AS Type,
		A.CODSERIPS ItemCodeOriginal,
		CONCAT(E.Code, ' - ', E.Description) ItemCodeName, 
		CONCAT(J.Code, ' - ', J.Name) DescriptionCodeName,
		A.CANSERIPS AS Quantity,
		E.FinancedResourceUPC,
		IIF(ISNULL(K.Id, 0) > 0, 1, 0) Covered,
		ISNULL(K.Contracted, 0) Contracted,
		ISNULL(K.Quoted, 0) Quoted,
		IIF(L.CODCONCEC IS NULL, 0, COALESCE(L.SUSCEPTIB, 1)) Authorized,
		concat(CASE A.ESTSERIPS when 1 THEN 'Solicitado. ' WHEN 2 THEN 'Programado. ' WHEN 3 THEN 'Anulado. ' WHEN 4 THEN 'Realizado. '  else '' end,
		CASE WHEN L.SUSCEPTIB IS NULL OR L.SUSCEPTIB = 0 then 'No requiere autorización'
				    WHEN M.PROESTADO = 1 then 'Pendiente Solicitud'
					WHEN M.PROESTADO = 2 then 'Solicitado'
					WHEN M.PROESTADO = 3 then 'Autorizado'
					WHEN M.PROESTADO = 4 then 'Anulado'
					WHEN M.PROESTADO = 5 then 'No autorizado'
					ELSE 'Pendiente por Autorizar'
				END) AS StatusName
		
	FROM .HCORDIALI A WITH(NOLOCK)
	JOIN .ADCENATEN B ON A.CODCENATE = B.CODCENATE
	JOIN .INUNIFUNC C ON A.UFUCODIGO = C.UFUCODIGO
	JOIN .ADINGRESO D ON A.NUMINGRES = D.NUMINGRES
	JOIN Contract.CUPSEntity E ON A.CODSERIPS = E.Code
	JOIN Contract.CareGroup F ON D.GENCAREGROUP = F.Id
	JOIN Contract.HealthAdministrator G ON D.GENCONENTITY = G.Id
	JOIN .INPACIENT H ON A.IPCODPACI = H.IPCODPACI
	LEFT OUTER JOIN Contract.CUPSEntityContractDescriptions I WITH(NOLOCK) ON E.Id = I.CUPSEntityId AND A.IDDESCRIPCIONRELACIONADA = I.Id
	LEFT OUTER JOIN Contract.ContractDescriptions J WITH(NOLOCK) ON I.ContractDescriptionId = J.Id	
	LEFT OUTER JOIN Contract.ProcedureCups K ON F.ProcedureTemplateId = K.ProceduresTemplateId AND E.Id = K.CupsId AND ISNULL(I.ContractDescriptionId, 0) = ISNULL(K.CUPSEntityContractDescriptionId, 0)
    LEFT OUTER JOIN .ADCONFSER L with(nolock) ON L.CODSERIPS = E.Code  
    LEFT OUTER JOIN .ADAUTOSER M with(nolock) on M.IPCODPACI = A.IPCODPACI AND M.NUMINGRES = A.NUMINGRES AND M.CODSERIPS = E.Code and M.NUMEFOLIO = A.NUMEFOLIO