

CREATE VIEW [Billing].[ViewRIPSInvoicePopulationGroup]
AS
SELECT
	iv.Id AS InvoiceId
   ,iv.InvoiceNumber
   ,iv.AdmissionNumber
   ,iv.DocumentType
   ,iv.HealthAdministratorId
   ,CONCAT(ha.Code, ' - ', ha.[Name]) AS HealthAdministratorCodeName
   ,CONCAT(tp.Nit, ' - ', tp.[Name]) AS ThirdPartyNitName
   ,iv.CareGroupId
   ,CONCAT(cg.Code, ' - ', cg.[Name]) AS CareGroupCodeName
   ,iv.TotalInvoice
   ,iv.InvoiceDate
   ,iv.ThirdPartySalesValue
   ,iv.TotalPatientSalesPrice
   ,iv.[Status] AS [State]
   ,IIF(iv.[Status] = 1, 'FACTURADO', 'ANULADO')
	AS StateName
   ,rc.RadicatedConsecutive
   ,rc.Id AS InvoiceRadicateId
   ,iv.CapitationInitialDate
   ,iv.CapitationEndDate
   ,iv.ThirdPartyId
   ,iv.InvoiceCategoryId
   ,ing.CODCENATE
   ,ISNULL(iv.ContractId, cg.ContractId) AS ContractId
   ,ing.ITIPORIES
   ,ing.ICAUSAING
   ,PSP.IDADPOBESPE
FROM [Billing].[Invoice] iv WITH (NOLOCK)
LEFT JOIN [Contract].[HealthAdministrator] ha WITH (NOLOCK)
	ON iv.HealthAdministratorId = ha.Id
LEFT JOIN [Common].[ThirdParty] tp WITH (NOLOCK)
	ON iv.ThirdPartyId = tp.Id
INNER JOIN [Contract].[CareGroup] cg WITH (NOLOCK)
	ON iv.CareGroupId = cg.Id
INNER JOIN .[ADINGRESO] ing WITH (NOLOCK)
	ON iv.AdmissionNumber = ing.NUMINGRES
LEFT OUTER JOIN dbo.INPACIENT AS INP WITH (NOLOCK)
	ON iv.PatientCode = INP.IPCODPACI
LEFT OUTER JOIN dbo.ADCENATEN AS CAI WITH (NOLOCK)
	ON ing.CODCENATE = CAI.CODCENATE
LEFT JOIN [Portfolio].[RadicateInvoiceD] rd
	ON iv.InvoiceNumber = rd.InvoiceNumber
LEFT JOIN [Portfolio].[RadicateInvoiceC] rc
	ON rd.RadicateInvoiceCId = rc.Id
LEFT OUTER JOIN Contract.Contract AS C WITH (NOLOCK)
	ON ISNULL(iv.ContractId, cg.ContractId) = C.Id
LEFT OUTER JOIN Billing.InvoiceCategories AS IC WITH (NOLOCK)
	ON iv.InvoiceCategoryId = IC.Id
LEFT OUTER JOIN dbo.ADPOBESPEPAC AS PSP WITH (NOLOCK)
	ON INP.IPCODPACI = PSP.IPCODPACI AND PSP.ESTADO = 1
WHERE iv.[Status] = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de facturas activas (no anuladas) para la generación y reporte de RIPS, agrupada por grupo de atención y grupo poblacional del paciente. Integra datos del encabezado de factura (número, fecha, valores totales, estado), la administradora de salud o pagador (EPS/aseguradora), el grupo de atención del contrato, el ingreso o admisión del paciente (tipo de origen, causa de ingreso, centro de atención) y el consecutivo de radicación en cartera. Incluye además el identificador del grupo poblacional especial del paciente (IDADPOBESPE), el contrato aplicable y la categoría de facturación, permitiendo así clasificar y reportar la producción facturada por régimen, pagador, grupo contractual y población para los archivos RIPS y la conciliación de cartera con aseguradoras y EPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRIPSInvoicePopulationGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRIPSInvoicePopulationGroup';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de facturas activas con sus datos de pagador, contrato, grupo de cuidado, admisión, paciente y radicación, orientada a la generación de archivos RIPS por grupo poblacional.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoicePopulationGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice con Status = 1 (facturada); Debe existir admisión correspondiente en ADINGRESO (NUMINGRES = AdmissionNumber) por ser INNER JOIN; Debe existir grupo de atención (CareGroup) asociado a la factura por ser INNER JOIN', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoicePopulationGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las filas devueltas corresponden a facturas en estado facturado (Status=1); las anuladas nunca aparecen; El ContractId nunca es nulo si al menos uno de iv.ContractId o cg.ContractId tiene valor (regla ISNULL); Solo se incluyen poblaciones especiales del paciente cuyo registro en ADPOBESPEPAC esté activo (PSP.ESTADO = 1); Cada fila representa una combinación factura-admisión válida (INNER JOIN con ADINGRESO y CareGroup); Los códigos de administradora, tercero y grupo de cuidado se exponen concatenados en formato ''Código - Nombre''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoicePopulationGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS; Grupo poblacional especial; Factura; Radicación de factura; Administradora de salud (EPS); Contrato; Grupo de atención (CareGroup); Admisión; Centro de atención; Paciente; Tercero pagador; Capitación; Causa de ingreso; Tipo de ingreso', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoicePopulationGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewRIPSInvoicePopulationGroup: Solo retorna facturas con iv.[Status] = 1; en cualquier otro caso quedan excluidas del resultado', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoicePopulationGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iv.[Status] = 1 → StateName se etiqueta como ''FACTURADO'' else StateName se etiqueta como ''ANULADO'' (aunque por el WHERE solo se devuelven Status=1); si iv.ContractId IS NULL → Se utiliza cg.ContractId (contrato del grupo de cuidado) como ContractId resultante else Se utiliza el ContractId propio de la factura', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoicePopulationGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Contract.HealthAdministrator; Common.ThirdParty; Contract.CareGroup; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADCENATEN; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Contract.Contract; Billing.InvoiceCategories; dbo.ADPOBESPEPAC', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoicePopulationGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIPSInvoicePopulationGroup';
GO
