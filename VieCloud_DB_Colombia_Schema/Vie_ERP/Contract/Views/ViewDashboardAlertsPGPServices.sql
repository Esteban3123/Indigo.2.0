CREATE VIEW [Contract].[ViewDashboardAlertsPGPServices]
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
temp_Services AS (
	SELECT	so.AdmissionNumber, so.PatientCode, sod.PerformsFunctionalUnitId FunctionalUnitId, sod.CareGroupId,
			sod.Id EntityId, so.Code ServiceOrderCode, so.OrderDate RequestDate, sod.PerformsHealthProfessionalCode ProfessionalCode, '' Observations,
			ce.Id CUPSEntityId, ce.CUPSEntityContractDescriptionId,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			sod.InvoicedQuantity, GrandTotalSalesPrice TotalServiceValue
	FROM Billing.ServiceOrder so WITH (NOLOCK)
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON so.Id = sod.ServiceOrderId
	JOIN temp_CUPSEntity ce WITH (NOLOCK) ON sod.CUPSEntityId = ce.Id AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(sod.CUPSEntityContractDescriptionId,0)
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
		v.Patient,
		v.AdmissionNumber,
		v.CareCenter,
		v.FunctionalUnit,
		v.Professional,
		v.DiagnosticCode Diagnostic,
		v.Observations,
		v.CUPSEntityCodeName,
		v.DescriptionCodeName,
		v.Quantity,
		v.CMEValue,
		v.TotalCME,
		v.TotalServiceValue,
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
	-- Ordenes de servico
	SELECT	sod.AdmissionNumber, CONCAT(ISNULL(tp.Nit, sod.PatientCode), ' - ', tp.Name) Patient, ca.NOMCENATE CareCenter, CONCAT(fu.Code, ' - ', fu.Name) FunctionalUnit, ISNULL(sc.CareGroupId, sod.CareGroupId) CareGroupId,
			'ServiceOrder' EntityName, sod.EntityId, ISNULL(sc.InvoiceDate, sod.RequestDate) RequestDate, CONCAT(sod.ProfessionalCode, ' - ', p.NOMMEDICO) Professional, diag.CODDIAGNO DiagnosticCode, sod.Observations,
			sod.CUPSEntityCodeName, sod.DescriptionCodeName, sod.InvoicedQuantity Quantity, ISNULL(g.ProjectCME, 0) CMEValue,
			sod.InvoicedQuantity * ISNULL(g.ProjectCME, 0) TotalCME, sod.TotalServiceValue,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			sod.ServiceOrderCode, sc.InvoiceNumber ServiceControlNumber, 
			STUFF
			(
				(
					SELECT	', ' + i.InvoiceNumber
					FROM temp_Invoice i WITH (NOLOCK)
					WHERE sc.CareGroupId = i.CareGroupId AND CAST(sc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
					FOR XML PATH ('')
				), 1, 1, ''
			) InvoiceNumber
	FROM temp_Services sod WITH (NOLOCK)
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON sod.FunctionalUnitId = fu.Id
	JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON sod.EntityId = sodd.ServiceOrderDetailId
	LEFT JOIN Billing.Invoice sc WITH (NOLOCK) ON sodd.RevenueControlDetailId = sc.RevenueControlDetailId AND sc.Status = 1 AND sc.DocumentType = 5
	JOIN temp_CareGroup cg WITH (NOLOCK) ON ISNULL(sc.CareGroupId, sod.CareGroupId) = cg.CareGroupId
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON ISNULL(sc.CareGroupId, sod.CareGroupId) = g.CareGroupId AND sod.CUPSEntityId = g.CUPSEntityId AND ISNULL(sod.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON sod.PatientCode = tp.Nit
	LEFT JOIN dbo.ADINGRESO a WITH (NOLOCK) ON sod.AdmissionNumber = a.NUMINGRES
	LEFT JOIN dbo.ADCENATEN ca WITH (NOLOCK) ON a.CODCENATE = CA.CODCENATE
	LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = a.NUMINGRES AND DIAG.CODDIAGNO = a.CODDIAEGR AND DIAG.CODDIAPRI = 1
	LEFT JOIN dbo.INPROFSAL p WITH (NOLOCK) ON sod.ProfessionalCode = p.CODPROSAL
) v
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de alertas para servicios bajo contratos de tipo PGP (Pago por Grupo Poblacional, tipo de liquidación 5): consolida en una sola vista los servicios solicitados, órdenes de servicio, controles de servicio y facturas asociadas a grupos de atención con notas técnicas y agrupadores CUPS pactados en el contrato. Integra información del paciente (cédula, nombre), número de ingreso/admisión, centro de atención, unidad funcional, profesional de salud, diagnóstico CIE-10, código CUPS del servicio, descripción de contrato, cantidades facturadas, valores CME proyectados y totales contratados, junto con los límites de usuarios (mínimo, máximo) y la frecuencia de cada agrupador. Calcula el estado de cada registro (Solicitado, Con Orden de Servicio, Con Control de Servicio o Con Factura) y concatena los números de factura de capitación vigentes según el período de la atención. Sirve para monitorear el consumo real de servicios frente a lo pactado en contratos PGP, apoyando la gestión de alertas de sobreuso, control presupuestal y auditoría de facturación por grupo poblacional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewDashboardAlertsPGPServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewDashboardAlertsPGPServices';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de tablero que consolida alertas de servicios bajo contratos PGP (capitación), mostrando órdenes de servicio con su agrupador contratado, paciente, profesional, diagnóstico, valores CME y estado según facturación.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de grupos de cuidado con LiquidationType = 5 (modalidad PGP/capitación); CUPSEntity debe estar activo (Status = 1) para ser considerado; Las facturas asociadas deben tener Status = 1; Para agruparse como factura final se requiere DocumentType = 4; como control de servicio DocumentType = 5; Los registros de CUPSEntityContractDescriptions deben tener IsDelete = 0', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen contratos con LiquidationType = 5 (PGP/capitación); Solo se incluyen CUPSEntity activos (Status=1); La unidad de medida de frecuencia se traduce a etiquetas Diario/Semanal/Mensual/Bimensual/Trimestral/Semestral/Anual (1..7); El TotalCME se calcula como InvoicedQuantity * ProjectCME (0 si no hay agrupador); La asociación CUPSEntity-ContractDescription se compara con ISNULL(...,0) para tratar nulos como equivalentes; La factura PGP asociada se vincula cuando la fecha del control de servicio cae entre CapitationInitialDate y CapitationEndDate del mismo CareGroup; El paciente se identifica preferentemente por NIT del tercero; si no existe, por PatientCode; Solo se considera el diagnóstico principal del ingreso (CODDIAPRI=1) coincidente con el diagnóstico de egreso', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato PGP (Pago Global Prospectivo / capitación); Grupo de cuidado (CareGroup); Nota técnica de contrato; Agrupador de contrato (Grouper); CUPS (Clasificación Única de Procedimientos en Salud); Orden de servicio; Control de servicio; Factura de capitación; CME (Costo Medio Esperado / Proyectado); Frecuencia de uso (Diario/Semanal/Mensual/Anual); Usuarios contratados (Min/Max/Número); Total contratado; Paciente / Tercero; Centro de atención; Unidad funcional; Profesional de la salud; Diagnóstico principal; Ingreso/Admisión', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada detalle de orden de servicio cuyo CareGroup esté en modalidad LiquidationType=5, enriquecida con agrupador, CUPS, paciente, centro, unidad funcional, profesional, diagnóstico principal y estado de facturación', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si GrouperCodeName IS NULL → GroupDescription se marca como ''SIN AGRUPADOR'' else Concatena descripción del agrupador con usuarios (Number/Min/Max), total contratado y frecuencia; si InvoiceNumber IS NOT NULL → StatusName = ''Con Factura'' else Evalúa siguiente condición; si ServiceControlNumber IS NOT NULL → StatusName = ''Con Control de Servicio'' else Evalúa siguiente condición; si ServiceOrderCode IS NOT NULL → StatusName = ''Con Orden de Servicio'' else StatusName = ''Solicitado''; si DIAG.CODDIAPRI = 1 → Solo se toma el diagnóstico marcado como principal del ingreso; si sc.CareGroupId IS NOT NULL (existe control de servicio asociado) → Se usa el CareGroupId y la fecha de factura del control de servicio en lugar del de la orden else Se usa el CareGroupId y la fecha de la orden de servicio', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.TechnicalNote; Contract.TechnicalNoteDetail; Contract.Groupers; Contract.GroupersCups; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailDistribution; Billing.Invoice; Payroll.FunctionalUnit; Common.ThirdParty; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INDIAGNOP; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewDashboardAlertsPGPServices';
GO
