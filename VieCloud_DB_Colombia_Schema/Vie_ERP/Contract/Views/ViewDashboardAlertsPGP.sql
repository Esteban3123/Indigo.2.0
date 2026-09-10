CREATE VIEW [Contract].[ViewDashboardAlertsPGP]
AS
WITH temp_CareGroup AS (
	SELECT cg.Id CareGroupId, cg.TechnicalNoteId
	FROM Contract.CareGroup cg WITH (NOLOCK)
	WHERE cg.LiquidationType = 5
),
temp_Groupers AS (
	SELECT	cg.CareGroupId, gc.CUPSEntityId, gc.CUPSEntityContractDescriptionId,
			CONCAT(g.Code, ' - ', g.Description) GrouperCodeName, g.UserMin, g.UserMax, g.ProjectCME, g.TotalContract
	FROM temp_CareGroup cg WITH (NOLOCK) 
	JOIN Contract.TechnicalNote tn WITH (NOLOCK) ON cg.TechnicalNoteId = tn.Id
	JOIN Contract.TechnicalNoteDetail tnd WITH (NOLOCK) ON tn.Id = tnd.TechnicalNoteId
	JOIN Contract.Groupers g ON tnd.GrouperId = g.Id
	JOIN Contract.GroupersCups gc ON g.Id = gc.GrouperId
),
temp_CUPSEntity AS (
	SELECT	ce.Id, ce.Code, ce.Description, 
			cecd.Id CUPSEntityContractDescriptionId,
			cd.Id ContractDescriptionId, cd.Code ContractDescriptionCode, cd.Name ContractDescriptionName
	FROM Contract.CUPSEntity ce WITH (NOLOCK) 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON ce.Id = cecd.CUPSEntityId AND cecd.IsDelete = 0
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE ce.Status = 1
),
temp_ADINGRESO AS (
	SELECT a.UFUCODIGO FunctionalUnitCode, a.NUMINGRES AdmissionNumber, cg.CareGroupId
	FROM temp_CareGroup cg WITH (NOLOCK)
	JOIN dbo.ADINGRESO a WITH (NOLOCK) ON cg.CareGroupId = a.GENCAREGROUP
	WHERE IESTADOIN <> 'A'
),
temp_HCHISPACA AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.FECHISPAC RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.INDICAMED Observations, h.GENSERVICEORDER ServiceOrderId
	FROM temp_ADINGRESO t WITH (NOLOCK)
	JOIN dbo.HCHISPACA h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES
	WHERE ESTAFOLIO <> 0
),
temp_HCORDIMAG AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDIMAG h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1 AND h.GENSERVICEORDER IS NULL
),
temp_HCORDLABO AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDLABO h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1 AND h.GENSERVICEORDER IS NULL
),
temp_HCORDPATO AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDPATO h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1 AND h.GENSERVICEORDER IS NULL
),
temp_HCORDINTE AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDINTE h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1 AND h.GENSERVICEORDER IS NULL
),
temp_HCORDPRON AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDPRON h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1 AND h.GENSERVICEORDER IS NULL
),
temp_HCORDPROQ AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDPROQ h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1 AND h.GENSERVICEORDER IS NULL
),
temp_HCORHEMCO AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, t.FunctionalUnitCode, t.CareGroupId,
			hd.ID EntityId, h.FECORDMED RequestDate, '' ProfessionalCode, h.CODDIAGNO DiagnosticCode, '' Observations,
			hd.CODSERIPS CUPSEntityCode, 1 Quantity, hd.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORHEMCO h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	JOIN dbo.HCORHEMSER hd WITH (NOLOCK) on h.ID = hd.HCORHEMCOID
	WHERE h.MANEXTPRO = 1 AND hd.ORDSERVICIOID IS NULL
),
temp_Invoice AS (
	SELECT i.CareGroupId, i.InvoiceNumber, i.CapitationInitialDate, i.CapitationEndDate
	FROM temp_CareGroup cg WITH (NOLOCK)
	JOIN Billing.Invoice i WITH (NOLOCK) ON cg.CareGroupId = i.CareGroupId
	WHERE i.Status = 1 AND i.DocumentType = 4
)

-----------------------------------------------------------------------------------------------------------------------

SELECT	CONCAT(v.EntityName, '-', v.EntityId, '-', v.GrouperCodeName, '-', v.CUPSEntityCodeName, '-', v.DescriptionCodeName) RowId,
		v.CareGroupId,
		CONCAT(v.GrouperCodeName, ' (Min. Usuarios: ', v.UserMin, ' - Max. Usuarios: ', v.UserMax, ' - Total Contratado: ', FORMAT(v.TotalContract, 'C0'), ')')  GroupDescription,
		v.UserMin, v.UserMax, v.TotalContract,
		v.RequestDate,
		v.PatientCode Patient,
		v.AdmissionNumber,
		v.Folio,
		v.CareCenterCode CareCenter,
		v.FunctionalUnitCode FunctionalUnit,
		v.ProfessionalCode Professional,
		v.DiagnosticCode Diagnostic,
		v.Observations,
		v.CUPSEntityCodeName,
		v.DescriptionCodeName,
		v.Quantity,
		v.CMEValue,
		v.ServiceValue,
		v.ServiceOrderCode,
		v.ServiceControlNumber,
		v.InvoiceNumber,
		CASE
			WHEN v.InvoiceNumber IS NOT NULL THEN 'Con Factura'
			WHEN v.ServiceControlNumber IS NOT NULL THEN 'Con Control de Servicio'
			WHEN v.ServiceOrderCode IS NOT NULL THEN 'Con Orden de Servicio'
			ELSE 'Solicitado'
		END StatusName
FROM
(
	-- Ordenes de imagenes ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDIMAG' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			h.Quantity * ISNULL(g.ProjectCME, 0) CMEValue, 0 ServiceValue,
			NULL ServiceOrderCode, NULL ServiceControlNumber, NULL InvoiceNumber
	FROM temp_HCORDIMAG h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de laboratorios ambulatorios
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDLABO' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			h.Quantity * ISNULL(g.ProjectCME, 0) CMEValue, 0 ServiceValue,
			NULL ServiceOrderCode, NULL ServiceControlNumber, NULL InvoiceNumber
	FROM temp_HCORDLABO h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de patologias ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDPATO' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			h.Quantity * ISNULL(g.ProjectCME, 0) CMEValue, 0 ServiceValue,
			NULL ServiceOrderCode, NULL ServiceControlNumber, NULL InvoiceNumber
	FROM temp_HCORDPATO h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de interconsultas ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDINTE' EntityName, h.EntityId,	CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			h.Quantity * ISNULL(g.ProjectCME, 0) CMEValue, 0 ServiceValue,
			NULL ServiceOrderCode, NULL ServiceControlNumber, NULL InvoiceNumber
	FROM temp_HCORDINTE h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de procedimientos no Qx ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDPRON' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			h.Quantity * ISNULL(g.ProjectCME, 0) CMEValue, 0 ServiceValue,
			NULL ServiceOrderCode, NULL ServiceControlNumber, NULL InvoiceNumber
	FROM temp_HCORDPRON h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de procedimientos Qx ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDPROQ' EntityName, h.EntityId,	CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			h.Quantity * ISNULL(g.ProjectCME, 0) CMEValue, 0 ServiceValue,
			NULL ServiceOrderCode, NULL ServiceControlNumber, NULL InvoiceNumber
	FROM temp_HCORDPROQ h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Hemocomponentes
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORHEMCO' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, '' ProfessionalCode, h.DiagnosticCode, '' Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			h.Quantity * ISNULL(g.ProjectCME, 0) CMEValue, 0 ServiceValue,
			NULL ServiceOrderCode, NULL ServiceControlNumber, NULL InvoiceNumber
	FROM temp_HCORHEMCO h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ce.CUPSEntityContractDescriptionId = h.CUPSEntityContractDescriptionId
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de control por la especialidad que atendió al paciente
	SELECT	hc.AdmissionNumber, hc.Folio, hc.PatientCode, hc.CareCenterCode, hc.FunctionalUnitCode, hc.CareGroupId CareGroupId,
			'HCDESCOEX' EntityName, h.AUTO EntityId, CAST(hc.RequestDate AS DATE) RequestDate, hc.ProfessionalCode, hc.DiagnosticCode, hc.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, 1 Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			ISNULL(g.ProjectCME, 0) CMEValue, 0 ServiceValue,
			NULL ServiceOrderCode, NULL ServiceControlNumber, NULL InvoiceNumber
	FROM temp_HCHISPACA hc WITH (NOLOCK)
	JOIN dbo.HCDESCOEX h WITH (NOLOCK) ON hc.AdmissionNumber = h.NUMINGRES AND hc.Folio = h.NUMEFOLIO
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON hc.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
	WHERE hc.ServiceOrderId IS NULL
UNION ALL
	-- Ordenes de servico
	SELECT	so.AdmissionNumber, 0 Folio, so.PatientCode, '' CareCenterCode, '' FunctionalUnitCode, ISNULL(sc.CareGroupId, sod.CareGroupId) CareGroupId,
			'ServiceOrder' EntityName, sod.ID EntityId, ISNULL(sc.InvoiceDate, so.OrderDate) RequestDate, sod.PerformsHealthProfessionalCode ProfessionalCode, '' DiagnosticCode, '' Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, sod.InvoicedQuantity Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, ISNULL(g.TotalContract, 0) TotalContract,
			sod.InvoicedQuantity * ISNULL(g.ProjectCME, 0) CMEValue, sod.GrandTotalSalesPrice ServiceValue,
			so.Code ServiceOrderCode, sc.InvoiceNumber ServiceControlNumber, 
			STUFF
			(
				(
					SELECT	', ' + i.InvoiceNumber
					FROM temp_Invoice i WITH (NOLOCK)
					WHERE sc.CareGroupId = i.CareGroupId AND CAST(sc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
					FOR XML PATH ('')
				), 1, 1, ''
			) InvoiceNumber
	FROM Billing.ServiceOrder so WITH (NOLOCK)
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON so.Id = sod.ServiceOrderId
	JOIN temp_CUPSEntity ce WITH (NOLOCK) ON sod.CUPSEntityId = ce.Id AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(sod.CUPSEntityContractDescriptionId,0)
	JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
	LEFT JOIN Billing.Invoice sc WITH (NOLOCK) ON sodd.RevenueControlDetailId = sc.RevenueControlDetailId AND sc.Status = 1 AND sc.DocumentType = 5
	JOIN temp_CareGroup cg WITH (NOLOCK) ON ISNULL(sc.CareGroupId, sod.CareGroupId) = cg.CareGroupId
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON ISNULL(sc.CareGroupId, sod.CareGroupId) = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
) v
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de alertas del dashboard para contratos con liquidación tipo PGP (Pago Global Prospectivo). Consolida las órdenes médicas pendientes de enviar a orden de servicio (imágenes diagnósticas, laboratorios, patología, interconsultas, procedimientos no quirúrgicos, procedimientos quirúrgicos y hemoderivados) generadas en ingresos activos de pacientes asociados a grupos de atención PGP, cruzando cada orden con los agrupadores tarifarios y códigos CUPS pactados en la nota técnica del contrato. Permite identificar en tiempo real qué servicios ordenados por el profesional aún no han sido tramitados como orden de servicio, mostrando datos del paciente (cédula, número de ingreso), centro de atención, unidad funcional, diagnóstico, agrupador del contrato, valores proyectados y límites de usuarios pactados, con el fin de alertar al equipo de gestión de contratos y facturación sobre posibles desviaciones o servicios externos no autorizados dentro del esquema PGP.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewDashboardAlertsPGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewDashboardAlertsPGP';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un tablero las solicitudes clínicas ambulatorias y órdenes de servicio asociadas a grupos de atención bajo modalidad de capitación/PGP, mostrando su estado de avance hacia facturación.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen CareGroup con LiquidationType = 5 (modalidad PGP/capitación); Cada CareGroup tiene una TechnicalNote con detalles de Groupers y CUPS asociados; Los ingresos (ADINGRESO) están vinculados al CareGroup vía GENCAREGROUP y no anulados (IESTADOIN <> ''A''); Las historias clínicas (HCHISPACA) tienen folio activo (ESTAFOLIO <> 0); Los CUPSEntity utilizados están activos (Status = 1) y sus descripciones de contrato no están eliminadas (IsDelete = 0)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan CareGroups con liquidación tipo 5 (PGP/capitación); CMEValue se calcula como Quantity * ProjectCME del agrupador (1 unidad para hemocomponentes y controles de especialidad); Las solicitudes clínicas que ya generaron orden de servicio (GENSERVICEORDER/ORDSERVICIOID/ServiceOrderId no nulos) no se listan como pendientes; UserMin, UserMax, TotalContract y ProjectCME se reemplazan por 0 cuando no hay agrupador coincidente (ISNULL); El emparejamiento CUPS-Agrupador exige coincidencia de código CUPS y de CUPSEntityContractDescriptionId (tratando NULL como 0); Las facturas de capitación deben tener Status=1 y DocumentType=4; los controles de servicio Status=1 y DocumentType=5; ServiceValue solo se reporta para órdenes de servicio (GrandTotalSalesPrice); las solicitudes clínicas se muestran con ServiceValue = 0; RowId se construye como concatenación EntityName-EntityId-Grouper-CUPS-Descripción, garantizando unicidad por origen y servicio', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas con StatusName = ''Con Factura'' cuando InvoiceNumber no es nulo; ''Con Control de Servicio'' cuando ServiceControlNumber no es nulo; ''Con Orden de Servicio'' cuando ServiceOrderCode no es nulo; en otro caso ''Solicitado''.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CareGroup.LiquidationType = 5 → Se incluye el grupo de atención como elegible para el dashboard PGP else Se excluye del análisis; si ADINGRESO.IESTADOIN <> ''A'' → Se considera el ingreso como activo/no anulado else Se descarta el ingreso; si HCHISPACA.ESTAFOLIO <> 0 → Se incluye el folio clínico else Se descarta; si En órdenes clínicas (HCORDIMAG/LABO/PATO/INTE/PRON/PROQ): MANEXTPRO = 1 AND GENSERVICEORDER IS NULL → Se reporta como solicitud pendiente sin orden de servicio generada else No se incluye; si HCORHEMCO.MANEXTPRO = 1 AND HCORHEMSER.ORDSERVICIOID IS NULL → Se incluye el hemocomponente como pendiente de orden de servicio else No se incluye; si HCHISPACA.GENSERVICEORDER IS NULL (en bloque HCDESCOEX) → Se reporta la orden de control de especialidad como solicitada else Se omite; si Invoice.Status = 1 AND DocumentType = 4 → La factura se considera factura de capitación válida para el CareGroup else No se asocia; si Invoice.Status = 1 AND DocumentType = 5 → Se trata como Control de Servicio (ServiceControlNumber) ligado al detalle de orden else No se asocia como control; si En ServiceOrder: COALESCE(sc.CareGroupId, sod.CareGroupId) → Se prefiere el CareGroup del control de servicio facturado y, si no existe, el del detalle de la orden; si Para ServiceOrder, fecha de la solicitud = ISNULL(sc.InvoiceDate, so.OrderDate) → Se usa la fecha del control si existe; en caso contrario la fecha de la orden; si sc.CareGroupId = i.CareGroupId AND sc.InvoiceDate BETWEEN i.CapitationInitialDate AND i.CapitationEndDate → Se concatenan los InvoiceNumber de capitación que cubren el periodo del control de servicio else InvoiceNumber queda nulo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.TechnicalNote; Contract.TechnicalNoteDetail; Contract.Groupers; Contract.GroupersCups; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.ADINGRESO; dbo.HCHISPACA; dbo.HCORDIMAG; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDINTE; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORHEMCO; dbo.HCORHEMSER; dbo.HCDESCOEX; Billing.Invoice; Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailDistribution', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGP';
GO
