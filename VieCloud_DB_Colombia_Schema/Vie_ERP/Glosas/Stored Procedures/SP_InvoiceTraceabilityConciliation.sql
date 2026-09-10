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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que devuelve la trazabilidad completa de conciliaciones de glosas para una factura específica. Dado un número de factura, consolida los encabezados de conciliación (ConciliationC) con sus líneas de detalle (ConciliationD) y la cartera glosada (GlosaPortfolioGlosada), mostrando el consecutivo y la fecha de cada conciliación, el estado del acta de conciliación y el estado del ítem de la factura dentro de ese proceso (Sin Confirmar, Confirmado o Anulado). Además, suma los valores aceptados en conciliación por parte de la IPS y de la EAPB/aseguradora, tomando como fuente los movimientos de glosa y sus ajustes de conciliación (GlosaMovementGlosa y GlosaMovementGlosaConciliation), y anula esos valores si la conciliación está en estado Anulado. Se usa en cartera y auditoría de glosas para responder preguntas como: ¿en cuántas conciliaciones ha participado esta factura?, ¿qué valores fueron aceptados por la IPS o la aseguradora en cada proceso de conciliación de glosa?', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceabilityConciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceabilityConciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista la trazabilidad de las conciliaciones aplicadas a una factura, mostrando estados (oficina y factura) y los valores aceptados por IPS y EAPB, neutralizando importes cuando la conciliación está anulada.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un número de factura existente en los detalles de conciliación y/o movimientos de glosa; Las conciliaciones cabecera deben estar relacionadas con sus detalles mediante ConciliationCId; Los movimientos de glosa deben referenciar la misma factura para ser sumarizados', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen conciliaciones cuyo detalle (ConciliationD) corresponde a la factura consultada; Las conciliaciones anuladas siempre muestran valor aceptado IPS y EAPB en cero, sin importar lo registrado en los movimientos; Cuando un movimiento de glosa tiene conciliación específica (GlosaMovementGlosaConciliation), ésta prevalece sobre los datos del movimiento original; Los valores nulos de aceptación se interpretan como 0; Solo se agregan movimientos asociados efectivamente a una conciliación (ConciliationCId no nulo)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación de glosas; Factura glosada; Glosa; IPS; EAPB; Estado de conciliación (Sin Confirmar/Confirmado/Anulado); Valor aceptado en conciliación; Cartera glosada', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada detalle de conciliación asociado a la factura, con consecutivo, fecha, estados traducidos y valores aceptados (IPS/EAPB), aplicando 0 cuando la conciliación cabecera está en estado ''3'' (Anulado)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Estado de la conciliación cabecera = ''1''/''2''/''3'' → Se traduce a ''Sin Confirmar'', ''Confirmado'' o ''Anulado'' respectivamente para el estado a nivel de oficina; si Estado del detalle de conciliación = ''1''/''2''/''3'' → Se traduce a ''Sin Confirmar'', ''Confirmado'' o ''Anulado'' respectivamente para el estado a nivel de factura; si Conciliación cabecera con estado = ''3'' (Anulado) → Los valores aceptados IPS y EAPB se reportan como 0 else Se reportan los valores aceptados acumulados (o 0 si son nulos); si Existe registro en GlosaMovementGlosaConciliation para el movimiento → Se usa ConciliationCId y valores aceptados de GlosaMovementGlosaConciliation else Se usa ConciliationCId y valores aceptados de GlosaMovementGlosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.ConciliationC; Glosas.ConciliationD; Glosas.GlosaPortfolioGlosada; Glosas.GlosaMovementGlosa; Glosas.GlosaMovementGlosaConciliation', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityConciliation';
-- GO
