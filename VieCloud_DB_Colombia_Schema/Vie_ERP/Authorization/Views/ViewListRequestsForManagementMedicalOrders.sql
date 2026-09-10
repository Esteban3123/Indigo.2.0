
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
		CONCAT(ageValues.Years, IIF(ageValues.Years = 1, ' año ', ' años '),
			   ageValues.Months, IIF(ageValues.Months = 1, ' mes ', ' meses '),
			   ageValues.Days, IIF(ageValues.Days = 1, ' día', ' días')) PatientAge,
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
	JOIN Contract.CUPSEntity ce  ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd  ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd  ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('6')) AND h.FECORDMED >= DATEFROMPARTS(2026, 01, 02)
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
	JOIN Contract.CUPSEntity ce  ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd  ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd  ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('6')) AND h.FECORDMED >= DATEFROMPARTS(2026, 01, 02)
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
	JOIN Contract.CUPSEntity ce  ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd  ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd  ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('6')) AND h.FECORDMED >= DATEFROMPARTS(2026, 01, 02)
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
	JOIN Contract.CUPSEntity ce  ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd  ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd  ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('5')) AND h.FECORDMED >= DATEFROMPARTS(2026, 01, 02)
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
	JOIN Contract.CUPSEntity ce  ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd  ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd  ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('5')) AND h.FECORDMED >= DATEFROMPARTS(2026, 01, 02)
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
	JOIN Contract.CUPSEntity ce  ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd  ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd  ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND NOT (h.ESTSERIPS IN ('3')) AND h.FECORDMED >= DATEFROMPARTS(2026, 01, 02)
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
	JOIN .HCORHEMCO h  ON ing.NUMINGRES = h.NUMINGRES
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
	JOIN Contract.CUPSEntity ce  ON hd.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd  ON ce.Id = cecd.CUPSEntityId AND hd.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd  ON cecd.ContractDescriptionId = cd.Id
	WHERE h.MANEXTPRO = 1 AND h.FECORDMED >= DATEFROMPARTS(2026, 01, 02)
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
	JOIN .HCDESCOEX h  ON hc.NUMINGRES = h.NUMINGRES AND hc.NUMEFOLIO = h.NUMEFOLIO
	JOIN Contract.CUPSEntity ce  ON h.CODSERIPS = ce.Code
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd  ON ce.Id = cecd.CUPSEntityId AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd  ON cecd.ContractDescriptionId = cd.Id
	WHERE hc.FECHISPAC >= DATEFROMPARTS(2026, 01, 02)
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
	JOIN Inventory.ATC atc  ON h.CODPRODUC = atc.Code
	JOIN Inventory.InventoryProduct ip  ON atc.Id = ip.ATCId
	WHERE h.MANEXTPRO = 1 AND h.FECINIDOS >= DATEFROMPARTS(2026, 01, 02)
) h
JOIN .ADCENATEN cc  ON h.CareCenterCode = cc.CODCENATE
JOIN .INUNIFUNC fu  ON h.FunctionalUnitCode = fu.UFUCODIGO
JOIN .ADINGRESO ing  ON h.AdmissionNumber = ing.NUMINGRES
JOIN Contract.CareGroup cg  ON ing.GENCAREGROUP = cg.Id
JOIN Contract.HealthAdministrator ha  ON ing.GENCONENTITY = ha.Id
JOIN .INPACIENT p  ON h.PatientCode = p.IPCODPACI
CROSS APPLY
(
	SELECT CAST(p.IPFECNACI AS date) BirthDate, CAST(GETDATE() AS date) CurrentDate
) ageDates
CROSS APPLY
(
	SELECT DATEDIFF(YEAR, ageDates.BirthDate, ageDates.CurrentDate)
		   - IIF(DATEADD(YEAR, DATEDIFF(YEAR, ageDates.BirthDate, ageDates.CurrentDate), ageDates.BirthDate) > ageDates.CurrentDate, 1, 0) Years
) ageYears
CROSS APPLY
(
	SELECT DATEADD(YEAR, ageYears.Years, ageDates.BirthDate) LastBirthday
) ageLastBirthday
CROSS APPLY
(
	SELECT DATEDIFF(MONTH, ageLastBirthday.LastBirthday, ageDates.CurrentDate)
		   - IIF(DATEADD(MONTH, DATEDIFF(MONTH, ageLastBirthday.LastBirthday, ageDates.CurrentDate), ageLastBirthday.LastBirthday) > ageDates.CurrentDate, 1, 0) Months
) ageMonths
CROSS APPLY
(
	SELECT DATEDIFF(DAY, DATEADD(MONTH, ageMonths.Months, ageLastBirthday.LastBirthday), ageDates.CurrentDate) Days
) ageDays
CROSS APPLY
(
	SELECT ageYears.Years, ageMonths.Months, IIF(ageDays.Days = 0 AND ageYears.Years = 0 AND ageMonths.Months = 0, 1, ageDays.Days) Days
) ageValues
LEFT JOIN Contract.ProcedureCups ptc  ON h.Type = 1 AND cg.ProcedureTemplateId = ptc.ProceduresTemplateId AND h.ItemId = ptc.CupsId AND ISNULL(h.ContractDescriptionId, 0) = ISNULL(ptc.CUPSEntityContractDescriptionId, 0)
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
	JOIN [Authorization].AuthorizationPortfolio ap  ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce  ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa  ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1 AND (ap.TypePortfolio = 1 OR ap.TypePortfolio IS NULL) 
UNION ALL
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			2 Type, 
			apip.InventoryProductId ItemId,
			NULL DescriptionId,
			apip.AuthorizationGroupId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap  ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip  ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa  ON apip.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1 AND (ap.TypePortfolio = 1 OR ap.TypePortfolio IS NULL) 
) csa ON h.CareCenterCode = csa.CareCenterCode AND h.Type = csa.Type AND h.ItemId = csa.ItemId AND ISNULL(h.DescriptionId, 0) = ISNULL(csa.DescriptionId, 0)
left join [Authorization].AuthorizationGroup ag  on ag.Id = csa.AuthorizationGroupId
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae  ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND cg.Id = csae.CareGroupId
LEFT JOIN [Authorization].ManagementMedicalOrder mmo  ON h.EntityName = mmo.EntityName AND h.EntityId = mmo.EntityId AND h.ItemCodeOriginal = mmo.ItemCode
LEFT JOIN .HCHISPACA hc  ON h.Folio = hc.NUMEFOLIO AND h.PatientCode = hc.IPCODPACI
left join .INPROFSAL prof  on prof.CODPROSAL = h.ProfessionalCode
WHERE mmo.Id IS NULL OR (mmo.Status = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de órdenes médicas ambulatorias pendientes de gestión y autorización, agrupando en una sola consulta todos los tipos de orden clínica: imágenes diagnósticas (radiología, ecografías, tomografías, resonancias), laboratorios, patologías, interconsultas y procedimientos no quirúrgicos. Para cada orden expone los datos del paciente (cédula, nombre, dirección, teléfono, edad), el centro de atención, la unidad funcional, el número de ingreso, el folio, el profesional solicitante, la fecha de la orden médica, el servicio CUPS con su código y descripción, el concepto de contrato asociado, y los indicadores de cobertura, contratación, cotización y autorización. Sirve como fuente principal del módulo de gestión y autorización de órdenes médicas externas, permitiendo identificar qué servicios requieren trámite de autorización ante la aseguradora o entidad pagadora, a partir de órdenes generadas desde 2022 que no hayan sido canceladas o rechazadas.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequestsForManagementMedicalOrders';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequestsForManagementMedicalOrders';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las órdenes médicas ambulatorias (imágenes, laboratorios, patologías, interconsultas, procedimientos Qx/no Qx, hemocomponentes, controles y medicamentos extramurales) pendientes o en gestión de autorización, enriqueciéndolas con datos del paciente, contrato, cobertura, tarifa y grupo de autorización.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsForManagementMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe estar marcada como manejo extramural (MANEXTPRO = 1) salvo para órdenes de control por especialidad (HCDESCOEX).; La fecha de orden (FECORDMED / FECINIDOS / FECHISPAC) debe ser posterior al 01/01/2022.; Debe existir admisión (ADINGRESO) con grupo de cuidado (GENCAREGROUP) y administradora de salud (GENCONENTITY) válidos.; Debe existir paciente (INPACIENT), centro de atención (ADCENATEN) y unidad funcional (INUNIFUNC) asociados a la orden.; Para medicamentos extramurales el código debe estar mapeado en Inventory.ATC y existir en Inventory.InventoryProduct.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsForManagementMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran portafolios de autorización con ap.Status = 1.; El identificador de fila se construye como EntityName-EntityId-ItemId, garantizando unicidad por origen, registro e ítem.; Las órdenes de tipo 1 representan servicios/procedimientos (CUPS) y las de tipo 2 representan medicamentos del inventario.; La cobertura por tarifa de productos requiere que la fecha de solicitud caiga dentro del rango InitialDate–EndDate de Inventory.ProductRateDetail.; Las excepciones de autorización (csae) se aplican únicamente si coinciden la configuración del servicio ambulatorio y el grupo de cuidado de la admisión.; Hemocomponentes y órdenes de control no manejan ProfessionalCode/Observations propios (se devuelven como vacío o derivados de la historia clínica).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsForManagementMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewListRequestsForManagementMedicalOrders: Solo retorna registros donde no exista gestión previa (mmo.Id IS NULL) o cuya gestión esté en estado activo (mmo.Status = 1).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsForManagementMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si h.ESTSERIPS IN (''6'') para órdenes de imágenes, laboratorios y patologías → Se excluye la orden del resultado else Se incluye si cumple las demás condiciones; si h.ESTSERIPS IN (''5'') para interconsultas y procedimientos no Qx → Se excluye la orden else Se incluye; si h.ESTSERIPS IN (''3'') para procedimientos Qx → Se excluye la orden else Se incluye; si hd.ESTADO IN (3) en HCORHEMSER (detalle de hemocomponentes) → Se excluye el detalle del conteo de cantidad else Se agrupa y cuenta como Quantity; si ptc.Id > 0 OR prd.Id > 0 (existe en ProcedureCups o ProductRateDetail) → Covered = 1 else Covered = 0; si ptc.Contracted = 1 OR prd.Contracted = 1 → Contracted = 1 else Contracted = 0; si csa.Id IS NOT NULL (existe configuración de servicio ambulatorio para el centro/ítem) → Authorized = ISNULL(csae.SusceptibleAuthorization, 1) else Authorized = 0; si h.Type = 1 (servicio CUPS) → Cruza cobertura contra Contract.ProcedureCups por plantilla de procedimientos del CareGroup else Si Type = 2 (medicamento) cruza contra Inventory.ProductRateDetail por ProductRateId del CareGroup y vigencia entre InitialDate y EndDate; si hc.TIPHISPAC → Mapea tipo de historia clínica a TypeClinicalHistory: I/O/PT/F=1, E/V=2, N=3, T=4, P=5, S=6, NF=7, B=8, otro=0', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsForManagementMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDINTE; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORHEMCO; dbo.HCORHEMSER; dbo.HCDESCOEX; dbo.HCPRESCRA; dbo.HCHISPACA; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; dbo.INPROFSAL; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Contract.CareGroup; Contract.HealthAdministrator; Contract.ProcedureCups; Inventory.ATC; Inventory.InventoryProduct; Inventory.ProductRateDetail; Authorization.AuthorizationPortfolio; Authorization.AuthorizationPortfolioCareCenter; Authorization.AuthorizationPortfolioCUPSEntity; Authorization.AuthorizationPortfolioInventoryProduct; Authorization.ConfigurationServicesAmbulatory (+3 adicionales)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsForManagementMedicalOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequestsForManagementMedicalOrders';
GO
