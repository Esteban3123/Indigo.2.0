CREATE VIEW [Portfolio].[ViewInvoiceCustomerRetention]
AS
SELECT	icr.Id, 
		i.Id InvoiceId,
		i.InvoiceNumber, 
		--------------------  RETENCION --------------------
		rc.Name RetentionName,
		icr.RetentionRate,
		IIF(i.Status = 1, icr.BaseValue, 0) + ISNULL(pnarr.BaseValue, 0) BaseValue,
		IIF(i.Status = 1, icr.Value, 0) + ISNULL(pnarr.Value, 0) Value
FROM Billing.Invoice i WITH (NOLOCK) 
JOIN Billing.InvoiceCustomerRetention icr WITH (NOLOCK) ON i.Id = icr.InvoiceId
JOIN Common.CustomerRetention cc WITH (NOLOCK) ON icr.CustomerRetentionId = cc.Id
JOIN GeneralLedger.RetentionConcepts rc WITH (NOLOCK) ON cc.RetentionConceptId = rc.Id
LEFT JOIN
(
	SELECT	ar.InvoiceId, 
			pnarr.InvoiceCustomerRetentionId, 
			SUM(pnarr.BaseValue * IIF(pnarr.Nature = 1, 1, -1)) BaseValue,
			SUM(pnarr.Value * IIF(pnarr.Nature = 1, 1, -1)) Value
	FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
	JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON ar.Id = pnara.AccountReceivableId
	JOIN Portfolio.PortfolioNoteAccountReceivableRetention pnarr WITH (NOLOCK) ON pnara.Id = pnarr.PortfolioNoteAccountReceivableId
	GROUP BY ar.InvoiceId, pnarr.InvoiceCustomerRetentionId
) pnarr ON icr.InvoiceId = pnarr.InvoiceId AND icr.Id = pnarr.InvoiceCustomerRetentionId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de retenciones tributarias consolidadas por factura de cliente, combinando las retenciones registradas en la factura original (retención en la fuente, ICA, IVA u otras) con los ajustes de retención aplicados a través de notas de cartera (anticipos y movimientos posteriores). Para cada retención muestra el nombre del concepto, la tarifa, la base gravable acumulada y el valor retenido total, considerando únicamente facturas activas (no anuladas) en la parte de la factura original. Es la fuente principal para reportes de retenciones en cartera, conciliación tributaria y verificación de descuentos de ley aplicados a los clientes pagadores (EPS, aseguradoras, empresas).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewInvoiceCustomerRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewInvoiceCustomerRetention';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por factura las retenciones tributarias aplicadas al cliente, sumando los ajustes provenientes de notas de cartera (anticipos) y neutralizando las retenciones originales cuando la factura no está activa.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada InvoiceCustomerRetention debe tener un CustomerRetention válido y este a su vez un RetentionConcept válido (joins INNER obligatorios); Los registros de PortfolioNoteAccountReceivableRetention deben referenciar un InvoiceCustomerRetentionId existente para cruzar con la factura', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Si la factura no está en estado 1, la retención original no aporta a los totales y sólo se reflejan ajustes por notas de cartera; Los ajustes por notas de cartera se firman según pnarr.Nature: 1 = positivo, distinto de 1 = negativo; El cruce con notas de cartera exige coincidencia tanto de InvoiceId como de InvoiceCustomerRetentionId, garantizando que cada ajuste se asocie a la retención exacta de la factura; La retención sólo existe en la vista si tiene un concepto de retención configurado en CustomerRetention y RetentionConcepts (joins INNER)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención tributaria; Factura; Concepto de retención; Cuenta por cobrar; Nota de cartera; Anticipo de cuenta por cobrar; Tarifa de retención; Base gravable', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.Status = 1 (factura activa/vigente) → Toma BaseValue y Value de la retención original (icr) else Considera 0 para la base y valor de la retención original; sólo suma los ajustes provenientes de notas de cartera; si pnarr.Nature = 1 → Suma BaseValue y Value como positivos (incrementan la retención) else Resta BaseValue y Value (los multiplica por -1, disminuyen la retención)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceCustomerRetention; Common.CustomerRetention; GeneralLedger.RetentionConcepts; Portfolio.AccountReceivable; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNoteAccountReceivableRetention', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewInvoiceCustomerRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewInvoiceCustomerRetention';
GO
