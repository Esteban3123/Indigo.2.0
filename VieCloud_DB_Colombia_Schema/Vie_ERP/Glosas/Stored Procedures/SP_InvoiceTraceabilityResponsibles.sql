-- =============================================
-- Author:		Rafael Patiño
-- Create date: 21/09/2014
-- Description:	Procedimientos para listar los datos de la trazabilidad de la factura
-- =============================================
CREATE PROCEDURE [Glosas].[SP_InvoiceTraceabilityResponsibles]
	@InvoiceNumber varchar(50)
AS
BEGIN
	select
	distinct
		m.InvoiceNumber, 
		Rg.Code +' - ' + rg.Name  ResponsibleGlosa,
		PortfolioGlosaEvaluation.Code + ' - ' + PortfolioGlosaEvaluation.Name as ResponsibleEvalutionInvoiceGlosa,
		PortfolioGlosaConfirmOffice.Code + ' - ' + PortfolioGlosaConfirmOffice.Name as ResponsibleConfirmOfficeGlosa,
		rr.Code + '-' + rr.Name as ResponsibleReiteration,
		PortfolioReiterationEvaluation.Code + ' - ' + PortfolioReiterationEvaluation.Name as ResponsibleEvalutionInvoiceReiteration,
		PortfolioReiterationConfirmOffice.Code + ' - ' + PortfolioReiterationConfirmOffice.Name as ResponsibleConfirmOfficeReiteration
	from 
	Glosas.GlosaMovementGlosa m inner join
	Glosas.GlosaPortfolioGlosada p on p.InvoiceNumber = m.InvoiceNumber inner join
	Glosas.Responsible rg on rg.id = m.ResponsibleId 	left join
	Glosas.Responsible rr on rr.id = m.ResponsibleReiterationId left join
	Glosas.Responsible PortfolioGlosaEvaluation on PortfolioGlosaEvaluation.id = p.ResponsibleEvaluationGlosa   left join
	Glosas.Responsible PortfolioGlosaConfirmOffice on PortfolioGlosaConfirmOffice.id = p.ResponsibleCoordinationGlosa left join 
	Glosas.Responsible PortfolioReiterationEvaluation on PortfolioReiterationEvaluation.id = p.ResponsibleEvaluationReiteration    left join
	Glosas.Responsible PortfolioReiterationConfirmOffice on PortfolioReiterationConfirmOffice.id = p.ResponsibleCoordinationReiteration 
	WHERE m.InvoiceNumber = @InvoiceNumber
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dado el número de una factura, consulta y consolida todos los responsables asignados en cada etapa del ciclo de glosas de esa factura: el responsable de la glosa inicial, el responsable de evaluación y el de coordinación en primera instancia (glosa), y los equivalentes en la etapa de reiteración. Cruza los movimientos de glosa (GlosaMovementGlosa) con la cartera de facturas glosadas (GlosaPortfolioGlosada) y la tabla de responsables (Responsible) para obtener el código y nombre de cada persona o rol involucrado. Se usa para la trazabilidad y auditoría del proceso de glosas, permitiendo identificar quién gestionó cada fase de la disputa de una factura con la EPS/EAPB.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceabilityResponsibles';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceabilityResponsibles';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los responsables involucrados en la trazabilidad de una factura glosada (responsable de glosa, evaluación, confirmación de oficina, y sus equivalentes en reiteración).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityResponsibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un movimiento de glosa (GlosaMovementGlosa) asociado al número de factura recibido.; Debe existir un registro en GlosaPortfolioGlosada para la misma factura (INNER JOIN).; El responsable principal de la glosa (ResponsibleId) debe existir en la tabla Responsible (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityResponsibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan filas donde la factura tenga simultáneamente registro en GlosaMovementGlosa y GlosaPortfolioGlosada.; El responsable principal de la glosa siempre estará presente (INNER JOIN), mientras que los responsables de reiteración, evaluación y confirmación pueden ser nulos (LEFT JOIN).; Los responsables se presentan siempre con el formato ''Código - Nombre''.; El resultado es libre de duplicados gracias al DISTINCT.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityResponsibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Glosa; Trazabilidad de factura; Responsable de glosa; Evaluación de glosa; Confirmación de oficina de glosa; Reiteración de glosa; Cartera glosada', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityResponsibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando existe un movimiento de glosa y cartera glosada para la factura indicada, retorna el conjunto distinto de responsables (glosa, evaluación de glosa, confirmación de oficina de glosa, reiteración, evaluación de reiteración y confirmación de oficina de reiteración) concatenando código y nombre.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityResponsibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaMovementGlosa; Glosas.GlosaPortfolioGlosada; Glosas.Responsible', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityResponsibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityResponsibles';
-- GO
