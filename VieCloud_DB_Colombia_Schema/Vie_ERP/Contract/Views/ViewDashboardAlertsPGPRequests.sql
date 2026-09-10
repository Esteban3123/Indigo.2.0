CREATE VIEW [Contract].[ViewDashboardAlertsPGPRequests]
AS
WITH temp_CareGroup AS (
	SELECT cg.Id CareGroupId, cg.TechnicalNoteId
	FROM Contract.CareGroup cg WITH (NOLOCK)
	WHERE cg.LiquidationType = 5
),
temp_Groupers AS (
	SELECT	cg.CareGroupId, gc.CUPSEntityId, gc.CUPSEntityContractDescriptionId,
			CONCAT(g.Code, ' - ', g.Description) GrouperCodeName, 
			g.UserMin, g.UserMax, g.UserNumber,
			g.ProjectCME, g.TotalContract,
			CONCAT(g.Frequence, ' ', CASE g.MeasurementUnit
									WHEN 1 THEN 'Diario'
									WHEN 2 THEN 'Semanal'
									WHEN 3 THEN 'Mensual'
									WHEN 4 THEN 'Bimensual'
									WHEN 5 THEN 'Trimestral'
									WHEN 6 THEN 'Semestral'
									WHEN 7 THEN 'Anual'
								END) FrequenceDescription
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
			h.FECHISPAC RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.INDICAMED Observations
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
	WHERE h.MANEXTPRO = 1
),
temp_HCORDLABO AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDLABO h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1
),
temp_HCORDPATO AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDPATO h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1
),
temp_HCORDINTE AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDINTE h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1
),
temp_HCORDPRON AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDPRON h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1
),
temp_HCORDPROQ AS (
	SELECT	h.NUMINGRES AdmissionNumber, h.NUMEFOLIO Folio, h.IPCODPACI PatientCode, h.CODCENATE CareCenterCode, h.UFUCODIGO FunctionalUnitCode, t.CareGroupId,
			h.AUTO EntityId, h.FECORDMED RequestDate, h.CODPROSAL ProfessionalCode, h.CODDIAGNO DiagnosticCode, h.OBSSERIPS Observations,
			h.CODSERIPS CUPSEntityCode, h.CANSERIPS Quantity, h.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId
	FROM temp_HCHISPACA t WITH (NOLOCK)
	JOIN dbo.HCORDPROQ h WITH (NOLOCK) on t.AdmissionNumber = h.NUMINGRES and t.Folio = h.NUMEFOLIO and t.PatientCode = h.IPCODPACI
	WHERE h.MANEXTPRO = 1
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
		CASE
			WHEN v.GrouperCodeName IS NULL THEN 'SIN AGRUPADOR'
			ELSE CONCAT(v.GrouperCodeName, ' (Usuarios: ', v.UserNumber, ' - Min: ', v.UserMin, ' - Max: ', v.UserMax, ' - Total Contratado: ', FORMAT(v.TotalContract, 'C0'), ' - Frecuencia: ', v.FrequenceDescription, ')')
		END GroupDescription,
		v.UserMin, v.UserMax, v.TotalContract,
		v.RequestDate,
		CONCAT(ISNULL(tp.Nit, v.PatientCode), ' - ', tp.Name) Patient,
		v.AdmissionNumber,
		v.Folio,
		ca.NOMCENATE CareCenter,
		CONCAT(ISNULL(fu.Code, v.FunctionalUnitCode), ' - ', fu.Name) FunctionalUnit,
		CONCAT(v.ProfessionalCode, ' - ', p.NOMMEDICO) Professional,
		v.DiagnosticCode Diagnostic,
		v.Observations,
		v.CUPSEntityCodeName,
		v.DescriptionCodeName,
		v.Quantity,
		v.CMEValue,
		v.TotalCME
FROM
(
	-- Ordenes de imagenes ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDIMAG' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			ISNULL(g.ProjectCME, 0) CMEValue, h.Quantity * ISNULL(g.ProjectCME, 0) TotalCME
	FROM temp_HCORDIMAG h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de laboratorios ambulatorios
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDLABO' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			ISNULL(g.ProjectCME, 0) CMEValue, h.Quantity * ISNULL(g.ProjectCME, 0) TotalCME
	FROM temp_HCORDLABO h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de patologias ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDPATO' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			ISNULL(g.ProjectCME, 0) CMEValue, h.Quantity * ISNULL(g.ProjectCME, 0) TotalCME
	FROM temp_HCORDPATO h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de interconsultas ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDINTE' EntityName, h.EntityId,	CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			ISNULL(g.ProjectCME, 0) CMEValue, h.Quantity * ISNULL(g.ProjectCME, 0) TotalCME
	FROM temp_HCORDINTE h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de procedimientos no Qx ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDPRON' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			ISNULL(g.ProjectCME, 0) CMEValue, h.Quantity * ISNULL(g.ProjectCME, 0) TotalCME
	FROM temp_HCORDPRON h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de procedimientos Qx ambulatorias
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORDPROQ' EntityName, h.EntityId,	CAST(h.RequestDate AS DATE) RequestDate, h.ProfessionalCode, h.DiagnosticCode, h.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			ISNULL(g.ProjectCME, 0) CMEValue, h.Quantity * ISNULL(g.ProjectCME, 0) TotalCME
	FROM temp_HCORDPROQ h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.CUPSEntityContractDescriptionId,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Hemocomponentes
	SELECT	h.AdmissionNumber, h.Folio, h.PatientCode, h.CareCenterCode, h.FunctionalUnitCode, h.CareGroupId CareGroupId,
			'HCORHEMCO' EntityName, h.EntityId, CAST(h.RequestDate AS DATE) RequestDate, '' ProfessionalCode, h.DiagnosticCode, '' Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, h.Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			ISNULL(g.ProjectCME, 0) CMEValue, h.Quantity * ISNULL(g.ProjectCME, 0) TotalCME
	FROM temp_HCORHEMCO h WITH (NOLOCK)
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CUPSEntityCode and ce.CUPSEntityContractDescriptionId = h.CUPSEntityContractDescriptionId
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON h.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
UNION ALL
	-- Ordenes de control por la especialidad que atendió al paciente
	SELECT	hc.AdmissionNumber, hc.Folio, hc.PatientCode, hc.CareCenterCode, hc.FunctionalUnitCode, hc.CareGroupId CareGroupId,
			'HCDESCOEX' EntityName, h.AUTO EntityId, CAST(hc.RequestDate AS DATE) RequestDate, hc.ProfessionalCode, hc.DiagnosticCode, hc.Observations,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName, 1 Quantity,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			ISNULL(g.ProjectCME, 0) CMEValue, ISNULL(g.ProjectCME, 0) TotalCME
	FROM temp_HCHISPACA hc WITH (NOLOCK)
	JOIN dbo.HCDESCOEX h WITH (NOLOCK) ON hc.AdmissionNumber = h.NUMINGRES AND hc.Folio = h.NUMEFOLIO
	JOIN temp_CUPSEntity ce WITH (NOLOCK) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON hc.CareGroupId = g.CareGroupId AND ce.Id = g.CUPSEntityId AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
) v
LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON v.PatientCode = tp.Nit
LEFT JOIN dbo.ADCENATEN ca WITH (NOLOCK) ON v.CareCenterCode = CA.CODCENATE
LEFT JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON v.FunctionalUnitCode = fu.Code
LEFT JOIN dbo.INPROFSAL p WITH (NOLOCK) ON v.ProfessionalCode = p.CODPROSAL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de alertas para solicitudes de servicios médicos ordenados bajo contratos con modalidad de pago por grupo (PGP, tipo de liquidación 5). Cruza los grupos de atención, notas técnicas y agrupadores CUPS del contrato con los ingresos activos de pacientes (hospitalizaciones no cerradas) y las órdenes médicas generadas en historia clínica: imágenes diagnósticas, laboratorios, patología, interconsultas, procedimientos de enfermería, cirugías y hemoderivados, filtrando únicamente las órdenes de manejo externo o por terceros. Para cada solicitud consolida el código y nombre del agrupador tarifario, los límites de usuarios y cantidades proyectadas en el contrato, la frecuencia de liquidación (diaria, semanal, mensual, etc.), el servicio CUPS solicitado, la cantidad, el diagnóstico CIE-10, el profesional que ordenó y la descripción de contrato asociada, permitiendo al área de contratos y auditoría identificar alertas cuando las solicitudes de servicios superan o se acercan a los límites pactados con la aseguradora en contratos PGP.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewDashboardAlertsPGPRequests';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewDashboardAlertsPGPRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un tablero las solicitudes/órdenes médicas ambulatorias asociadas a grupos de atención bajo modalidad PGP (capitación), enriqueciéndolas con datos del agrupador contractual, CUPS, paciente, centro y CME proyectado.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen CareGroup con LiquidationType = 5 (modalidad PGP/capitación); Las notas técnicas y agrupadores deben estar relacionados al CareGroup vía TechnicalNoteDetail y GroupersCups; Los CUPSEntity deben tener Status = 1 para ser considerados; Las admisiones (ADINGRESO) deben tener IESTADOIN distinto de ''A'' (no anuladas); Los folios de historia clínica (HCHISPACA) deben tener ESTAFOLIO distinto de 0; Las órdenes deben estar marcadas como manejo externo profesional (MANEXTPRO = 1); En hemocomponentes, los detalles HCORHEMSER deben tener ORDSERVICIOID NULL (no asociados a una orden de servicio); Las CUPSEntityContractDescriptions consideradas deben tener IsDelete = 0', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan grupos de atención bajo modalidad de liquidación tipo 5 (PGP/capitación); Las órdenes ambulatorias se filtran exclusivamente por manejo externo profesional (MANEXTPRO=1); El TotalCME siempre se calcula como Quantity * ProjectCME, usando 0 cuando el agrupador no aporta valor; Las órdenes de control por especialidad (HCDESCOEX) siempre se contabilizan con Quantity = 1; Los hemocomponentes solo se listan cuando aún no tienen orden de servicio asociada; El emparejamiento orden-CUPS respeta la descripción de contrato (CUPSEntityContractDescriptionId), tratando NULL como 0; Los ingresos anulados y folios anulados nunca aparecen en el tablero', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'PGP (Pago Global Prospectivo / capitación); Grupo de atención (CareGroup); Nota técnica de contrato; Agrupador contractual; CUPS (Clasificación Única de Procedimientos en Salud); Descripción de contrato; CME proyectado (Costo Medio Esperado); Frecuencia de medición (diaria, semanal, mensual, bimensual, trimestral, semestral, anual); Admisión / Ingreso del paciente; Folio de historia clínica; Orden de imágenes diagnósticas; Orden de laboratorio; Orden de patología; Orden de interconsulta; Orden de procedimiento no quirúrgico; Orden de procedimiento quirúrgico; Hemocomponentes; Orden de control por especialidad; Centro de atención; Unidad funcional; Profesional tratante; Diagnóstico; Factura de capitación; Paciente; Total contratado; Usuarios mínimos/máximos', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.ViewDashboardAlertsPGPRequests: Devuelve una fila por cada orden ambulatoria (imágenes, laboratorios, patología, interconsulta, procedimientos no Qx, Qx, hemocomponentes y control por especialidad) cruzada con el agrupador del contrato PGP, calculando TotalCME = Quantity * ProjectCME', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CareGroup.LiquidationType = 5 → Se incluye el grupo de atención como candidato (modalidad PGP/capitación) else Se excluye del tablero; si ADINGRESO.IESTADOIN <> ''A'' → Se incluye la admisión else Se excluye (ingreso anulado); si HCHISPACA.ESTAFOLIO <> 0 → Se incluye el folio clínico else Se excluye; si Orden con MANEXTPRO = 1 → Se considera como solicitud externa de profesional para el tablero else No se incluye; si HCORHEMSER.ORDSERVICIOID IS NULL → Se incluye el hemocomponente como pendiente de orden de servicio else Se excluye; si GrouperCodeName IS NULL (no hay agrupador asociado) → Se etiqueta GroupDescription como ''SIN AGRUPADOR'' else Se construye descripción con usuarios mínimos/máximos, total contratado y frecuencia; si MeasurementUnit del agrupador (1..7) → Se traduce a etiqueta de frecuencia: Diario/Semanal/Mensual/Bimensual/Trimestral/Semestral/Anual; si Invoice.Status = 1 AND DocumentType = 4 → Se considera la factura de capitación vigente para el periodo (CTE temp_Invoice)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.TechnicalNote; Contract.TechnicalNoteDetail; Contract.Groupers; Contract.GroupersCups; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.ADINGRESO; dbo.HCHISPACA; dbo.HCORDIMAG; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDINTE; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORHEMCO; dbo.HCORHEMSER; dbo.HCDESCOEX; Billing.Invoice; Common.ThirdParty; dbo.ADCENATEN; Payroll.FunctionalUnit; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPRequests';
GO
