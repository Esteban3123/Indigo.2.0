
CREATE VIEW [Portfolio].[VReportListRadicatedInvoice]
AS
	SELECT 
		ri.Id AS RadicatedId,
		ri.RadicatedConsecutive AS RadicatedConsecutive,
		ri.RadicatedDate AS RadicatedDate,
		ri.DocumentDate AS DocumentDate,
		ri.ConfirmDate AS ConfirmDate,
		ri.State AS Status,

		c.Id AS IdCustomer,
		c.Nit AS NitCustomer,
		c.Name AS NameCustomer,
		
		rid.InvoiceNumber AS InvoiceNumber,
		rid.InvoiceDate AS InvoiceDate,
		rid.PatientCode AS PatientCode,
		rid.PatientName AS PatientName,
		rid.InvoiceValueEntity AS InvoiceValueEntity,
		rid.CreditNoteValue AS CreditNoteValue,
		rid.BalanceInvoice AS BalanceInvoice,
		rid.ContractCode AS ContractCode,
		rid.Devolution,
		rid.ConceptDevolution,
		
		cg.Code AS CodeCareGroup,
		cg.Name AS NameCareGroup,		
		cg.EntityType AS Regimen,
		pr.RegimenName
		
	FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
	JOIN Common.Customer c WITH (NOLOCK) ON ri.CustomerId = c.Id
	LEFT JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId	
	LEFT JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType NOT IN (4, 6)
	LEFT JOIN GeneralLedger.MainAccounts mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
	LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON ar.InvoiceId = i.Id
	LEFT JOIN [Contract].[CareGroup] cg WITH (NOLOCK) ON cg.Id = i.CareGroupId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el listado de facturas radicadas ante entidades pagadoras (EPS, aseguradoras, empresas) para reportería de cartera. Integra el encabezado del radicado (consecutivo, fechas de radicación, documentación y confirmación, estado del trámite) con los datos del cliente o pagador (NIT, nombre), el detalle de cada factura presentada al cobro (número de factura, fecha, código y nombre del paciente, valor cobrado a la entidad, notas crédito, saldo pendiente, contrato, devoluciones y conceptos de devolución) y el grupo de atención del contrato con su tipo de régimen (contributivo, subsidiado, etc.). Permite a los equipos de cartera y facturación consultar, auditar y hacer seguimiento al proceso de cobro radicado por cliente pagador, identificando el estado de cada factura, sus valores y el régimen al que pertenece.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportListRadicatedInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportListRadicatedInvoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista la información de radicación de facturas de cartera con datos del cliente pagador, detalle de facturas radicadas, grupo de atención y régimen contable asociado, para reportes de cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen radicados en Portfolio.RadicateInvoiceC con su cliente correspondiente en Common.Customer.; La función Portfolio.GetRegimes() debe estar disponible para mapear cuentas contables a nombres de régimen.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La relación radicado-cliente es obligatoria (INNER JOIN con Common.Customer); todo registro reportado tiene cliente identificado.; El régimen (RegimenName) se deriva del número de la cuenta contable principal vinculada a la cuenta por cobrar ''sin radicar'' (AccountWithoutRadicateId), no de la cuenta del cliente.; El grupo de atención (CareGroup) y régimen (EntityType) provienen de la factura original en Billing.Invoice referenciada por la cuenta por cobrar, no del radicado en sí.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Cartera / cuentas por cobrar; Cliente pagador (EPS/aseguradora/empresa); Nota crédito; Devolución de factura; Saldo de factura; Régimen; Grupo de atención (CareGroup); Cuenta contable (PUC); Contrato', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.VReportListRadicatedInvoice: Devuelve un registro por cada detalle de factura radicada (RadicateInvoiceD) asociado a un radicado (RadicateInvoiceC); si el radicado no tiene detalle, igualmente aparece por el LEFT JOIN con columnas de detalle nulas.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.AccountReceivableType NOT IN (4, 6) → Solo se cruzan cuentas por cobrar cuyo tipo no sea 4 ni 6, excluyendo esos tipos del enriquecimiento contable y de régimen. else Si la AR es de tipo 4 o 6, no se enlaza y los campos de cuenta contable, régimen y grupo de atención quedan en NULL.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetRegimes', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Common.Customer; Portfolio.RadicateInvoiceD; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; Portfolio.GetRegimes; Billing.Invoice; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportListRadicatedInvoice';
GO
