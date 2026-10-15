
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