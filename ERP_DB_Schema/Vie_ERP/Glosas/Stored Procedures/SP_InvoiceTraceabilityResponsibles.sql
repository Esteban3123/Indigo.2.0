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