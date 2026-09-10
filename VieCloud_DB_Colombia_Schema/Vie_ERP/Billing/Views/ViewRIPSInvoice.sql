
CREATE VIEW [Billing].[ViewRIPSInvoice]
AS
SELECT  distinct iv.Id AS InvoiceId, 
		iv.InvoiceNumber,
		iv.AdmissionNumber, 
		iv.DocumentType,
		iv.HealthAdministratorId,
		concat(ha.Code, ' - ', ha.[Name]) AS HealthAdministratorCodeName,
		concat(tp.Nit, ' - ', tp.[Name]) AS ThirdPartyNitName,
		iv.CareGroupId,
		concat(cg.Code, ' - ', cg.[Name]) AS CareGroupCodeName,
		iv.TotalInvoice, CAST(iv.InvoiceDate AS DATE) InvoiceDate,
		iv.ThirdPartySalesValue,
		iv.TotalPatientSalesPrice,
		iv.[Status] AS [State], 
		iif(iv.[Status] = 1, 'FACTURADO', 'ANULADO') AS StateName, 
		rc.RadicatedConsecutive,
		rc.Id AS InvoiceRadicateId, 
		iv.CapitationInitialDate,
		iv.CapitationEndDate,
		iv.ThirdPartyId,
		iv.InvoiceCategoryId,
		ing.CODCENATE, 
		ISNULL(iv.ContractId, cg.ContractId) AS ContractId ,
		ing.ITIPORIES,
		ing.ICAUSAING,
		COALESCE(Diag.CODDIAGNO,ing.CODDIAEGR,iv.OutputDiagnosis,'Z000') AS MainDiagnosticCode 
FROM	[Billing].[Invoice] iv WITH (nolock) LEFT JOIN
        [Contract].[HealthAdministrator] ha WITH (nolock) ON iv.HealthAdministratorId = ha.Id LEFT JOIN
        [Common].[ThirdParty] tp WITH (nolock) ON iv.ThirdPartyId = tp.Id INNER JOIN
        [Contract].[CareGroup] cg WITH (nolock) ON iv.CareGroupId = cg.Id INNER JOIN
        .[ADINGRESO] ing WITH (nolock) ON iv.AdmissionNumber = ing.NUMINGRES LEFT OUTER JOIN
		dbo.INPACIENT AS INP WITH (NOLOCK) ON iv.PatientCode = INP.IPCODPACI LEFT OUTER JOIN
        dbo.ADCENATEN AS CAI WITH (NOLOCK) ON ing.CODCENATE = CAI.CODCENATE LEFT JOIN
        [Portfolio].[RadicateInvoiceD] rd ON iv.InvoiceNumber = rd.InvoiceNumber LEFT JOIN
        [Portfolio].[RadicateInvoiceC] rc ON rd.RadicateInvoiceCId = rc.Id LEFT OUTER JOIN
        Contract.Contract AS C WITH (NOLOCK) ON ISNULL(iv.ContractId, cg.ContractId) = C.Id LEFT OUTER JOIN
        Billing.InvoiceCategories AS IC WITH (NOLOCK) ON iv.InvoiceCategoryId = IC.Id LEFT OUTER JOIN
		dbo.INDIAGNOP diag WITH (NOLOCK) ON ing.NUMINGRES =diag.NUMINGRES AND ing.CODDIAEGR = diag.CODDIAGNO AND diag.CODDIAPRI = 1
		WHERE  iv.[Status] = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de facturas activas (no anuladas) para la generación y reporte de RIPS (Registro Individual de Prestación de Servicios). Integra datos del encabezado de la factura con la administradora de salud (EPS/aseguradora), el grupo de atención del contrato, el ingreso o admisión del paciente, el centro de atención, el estado de radicación en cartera y el diagnóstico principal (CIE-10) del episodio. Sirve como fuente central para reportes de facturación electrónica, RIPS, conciliación de glosas y seguimiento de cobros a pagadores, combinando información clínica, contractual y financiera en un único resultado por factura vigente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRIPSInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRIPSInvoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la información de facturas activas con datos de admisión, contrato, pagador, radicación y diagnóstico principal para la generación del archivo RIPS (reporte oficial de prestaciones de salud).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice con Status = 1 (FACTURADO); La admisión referenciada en la factura debe existir en ADINGRESO (INNER JOIN por NUMINGRES); El grupo de cuidado (CareGroup) referenciado debe existir (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone facturas activas (Status=1); nunca incluye facturas anuladas; Siempre garantiza un código de diagnóstico principal no nulo (mínimo ''Z000'' como valor por defecto CIE-10 de examen general); Siempre asocia un contrato cuando exista al menos uno entre la factura o el grupo de cuidado; Toda fila tiene una admisión válida en ADINGRESO (INNER JOIN obligatorio); Toda fila tiene un grupo de atención (CareGroup) válido (INNER JOIN obligatorio); Se aplica DISTINCT para evitar duplicados generados por los múltiples LEFT JOIN (radicación, diagnósticos); InvoiceDate se expone truncada a fecha (sin componente de hora)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS (Registro Individual de Prestación de Servicios de Salud); Factura de venta; Admisión / ingreso del paciente; Administradora de salud (EPS/pagador); Tercero pagador; Grupo de cuidado (CareGroup); Contrato de salud; Radicación de factura; Centro de atención (CODCENATE); Diagnóstico principal (CIE-10); Diagnóstico de egreso; Tipo de ingreso (ITIPORIES); Causa de ingreso (ICAUSAING); Categoría de facturación; Capitación (fechas inicial y final); Copago / valor a paciente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewRIPSInvoice: Devuelve solo facturas con Status = 1; las anuladas (Status distinto) quedan excluidas por el WHERE iv.[Status] = 1; [RETURN_RESULT] Billing.ViewRIPSInvoice: Calcula MainDiagnosticCode con prioridad: INDIAGNOP.CODDIAGNO (diagnóstico principal del ingreso, CODDIAPRI=1) → ADINGRESO.CODDIAEGR (diagnóstico de egreso) → Invoice.OutputDiagnosis → ''Z000'' por defecto; [RETURN_RESULT] Billing.ViewRIPSInvoice: Resuelve ContractId con ISNULL(Invoice.ContractId, CareGroup.ContractId): si la factura no tiene contrato, usa el del grupo de cuidado; [RETURN_RESULT] Billing.ViewRIPSInvoice: Traduce Status numérico: 1 → ''FACTURADO'', cualquier otro → ''ANULADO'' (aunque por el WHERE solo aparecerán FACTURADO)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iv.[Status] = 1 → Etiqueta StateName=''FACTURADO'' else Etiqueta StateName=''ANULADO'' (no alcanzable por el filtro WHERE); si Invoice.ContractId IS NULL → Usa CareGroup.ContractId como contrato vigente else Conserva Invoice.ContractId; si Existe registro en INDIAGNOP con NUMINGRES coincidente, CODDIAGNO=ADINGRESO.CODDIAEGR y CODDIAPRI=1 → Usa INDIAGNOP.CODDIAGNO como diagnóstico principal else Cae en COALESCE: ADINGRESO.CODDIAEGR → Invoice.OutputDiagnosis → ''Z000''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Contract.HealthAdministrator; Common.ThirdParty; Contract.CareGroup; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADCENATEN; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Contract.Contract; Billing.InvoiceCategories; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoice';
GO
