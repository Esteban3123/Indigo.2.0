-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 21/09/2014
-- Description:	Procedimientos para listar las conciliaciones de una factura
-- =============================================
CREATE PROCEDURE [Glosas].[SP_InvoiceTraceabilityConciliation]
	@InvoiceNumber varchar(50)
AS
BEGIN
	SELECT	c.ConciliationConsecutive,
			c.ConciliationDate,  
			CASE c.State 
				WHEN '1' THEN 'Sin Confirmar'
				WHEN '2' THEN 'Confirmado'
				WHEN '3' THEN 'Anulado'
			END StateOficeConciliation,
			case cd.State 
				WHEN '1' THEN 'Sin Confirmar'
				WHEN '2' THEN 'Confirmado'
				WHEN '3' THEN 'Anulado' 
			END StateInvoiceConciliation,
			IIF(c.State = '3', 0, ISNULL(gmg.ValueAcceptedIPSconciliation, 0)) ValueAcceptedIPSconciliation,
			IIF(c.State = '3', 0, ISNULL(gmg.ValueAcceptedEAPBconciliation, 0)) ValueAcceptedEAPBconciliation
	FROM Glosas.ConciliationC c WITH (NOLOCK)
	JOIN Glosas.ConciliationD cd WITH (NOLOCK) ON c.Id = cd.ConciliationCId 
	JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON cd.GlosaPortfolioId = gpg.Id
	LEFT JOIN
	(
		SELECT	ISNULL(gmgc.ConciliationCId, gmg.ConciliationCId) ConciliationCId,
				SUM(COALESCE(gmgc.ValueAcceptedIPSconciliation, gmg.ValueAcceptedIPSconciliation, 0)) ValueAcceptedIPSconciliation,
				SUM(COALESCE(gmgc.ValueAcceptedEAPBconciliation, gmg.ValueAcceptedEAPBconciliation, 0)) ValueAcceptedEAPBconciliation
		FROM Glosas.GlosaMovementGlosa gmg WITH (NOLOCK)
		LEFT JOIN Glosas.GlosaMovementGlosaConciliation gmgc WITH (NOLOCK) ON gmg.Id = gmgc.GlosaMovementGlosaId
		WHERE gmg.InvoiceNumber = @InvoiceNumber AND ISNULL(gmgc.ConciliationCId, gmg.ConciliationCId) IS NOT NULL
		GROUP BY ISNULL(gmgc.ConciliationCId, gmg.ConciliationCId)
	) gmg ON c.Id = gmg.ConciliationCId
	WHERE cd.InvoiceNumber = @InvoiceNumber
END