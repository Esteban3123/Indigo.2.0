
CREATE VIEW [Billing].[VReportInvoiceCustomerRetention]
AS
SELECT	icr.Id, 
		icr.InvoiceId, 
		icr.CalculateTaxAdvance,
		--------------------- CONCEPTO ---------------------
		pnc.Name PortfolioNoteConcept,
		--------------------  RETENCION --------------------
		rc.Name RetentionConcept,
		--------------------  DETALLE --------------------
		CASE icr.RetentionType
			WHEN 0 THEN 'Ninguna'
			WHEN 1 THEN 'Retefuente'
			WHEN 2 THEN 'ReteIVA'
			WHEN 3 THEN 'ReteICA'
			ELSE 'Otra'
		END RetentionTypeName,
		icr.RetentionRate,
		icr.BaseValue,
		icr.Value
FROM Billing.InvoiceCustomerRetention icr WITH (NOLOCK) 
JOIN Common.CustomerRetention cc WITH (NOLOCK) ON icr.CustomerRetentionId = cc.Id
JOIN Portfolio.PortfolioNoteConcept pnc WITH (NOLOCK) ON cc.PortfolioNoteConceptId = pnc.Id
JOIN GeneralLedger.RetentionConcepts rc WITH (NOLOCK) ON cc.RetentionConceptId = rc.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el detalle de las retenciones tributarias aplicadas a clientes en las facturas de facturación. Combina las retenciones por factura con sus conceptos de nota de cartera y conceptos contables de retención, mostrando para cada registro el tipo de retención (Retefuente, ReteIVA, ReteICA u otra), la tarifa aplicada, la base gravable y el valor calculado. Está diseñada para reportería tributaria y de cartera, permitiendo identificar qué retenciones se descontaron en cada factura emitida a un cliente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoiceCustomerRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoiceCustomerRetention';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para reportes, el detalle de las retenciones tributarias aplicadas en facturas, enriqueciendo cada retención con su concepto de cartera, concepto contable y la descripción legible del tipo de retención.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada InvoiceCustomerRetention debe tener un CustomerRetentionId válido en Common.CustomerRetention.; Cada CustomerRetention debe referenciar un PortfolioNoteConcept y un RetentionConcept existentes (los JOIN son INNER).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los tipos de retención reconocidos por el negocio son exclusivamente: Ninguna(0), Retefuente(1), ReteIVA(2), ReteICA(3); cualquier otro valor se clasifica como ''Otra''.; El concepto de retención mostrado proviene de la configuración del cliente (Common.CustomerRetention), no directamente de la factura.; Todas las consultas se hacen con NOLOCK, por lo que pueden incluir lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención tributaria; Retención en la fuente; ReteIVA; ReteICA; Factura; Concepto de nota de cartera; Concepto de retención contable; Base de retención; Tarifa de retención; Anticipo de impuesto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VReportInvoiceCustomerRetention: Devuelve únicamente retenciones cuya configuración de cliente esté completamente vinculada a un concepto de nota de cartera y a un concepto de retención (INNER JOIN sobre las tres tablas relacionadas).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si icr.RetentionType = 0 → Etiqueta el tipo como ''Ninguna''.; si icr.RetentionType = 1 → Etiqueta el tipo como ''Retefuente'' (retención en la fuente).; si icr.RetentionType = 2 → Etiqueta el tipo como ''ReteIVA''.; si icr.RetentionType = 3 → Etiqueta el tipo como ''ReteICA''.; si icr.RetentionType no coincide con 0,1,2,3 → Etiqueta el tipo como ''Otra''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceCustomerRetention; Common.CustomerRetention; Portfolio.PortfolioNoteConcept; GeneralLedger.RetentionConcepts', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceCustomerRetention';
GO
