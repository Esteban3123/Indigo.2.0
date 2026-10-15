-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 21/09/2014
-- Description:	Procedimientos para listar los datos de la trazabilidad de la factura
-- =============================================
CREATE PROCEDURE [Glosas].[SP_InvoiceTraceabilityRadication]
	@InvoiceNumber varchar(50)
AS
BEGIN
	--radicacion
	select C.RadicatedConsecutive,C.RadicatedDate,C.ConfirmUser,
	case when C.State = 1 THEN 'Sin Confirmar' when C.State = 2 THEN 'Confirmado' when C.State = 4 then 'Anulado' END StateRadicate,
	case when D.State = 1 THEN 'Sin Confirmar' when D.State = 2 THEN 'Confirmado' when D.State = 4 then 'Anulado' END StateInvoice,
	case when (select count(*) from [Glosas].[GlosaMovementDevolutions] md inner join [Common].Conceptglosas C on C.id = md.IdConceptGlosa where md.invoicenumber =D.Invoicenumber ) = 0 THEN '' 
		when (select count(*) from [Glosas].[GlosaMovementDevolutions] md inner join [Common].Conceptglosas C on C.id = md.IdConceptGlosa where md.invoicenumber =D.Invoicenumber and D.state = 4 and  c.code = 100) > 0 THEN 'Por Error en Cartera' 
    	when (select count(*) from [Glosas].[GlosaMovementDevolutions] md inner join [Common].Conceptglosas C on C.id = md.IdConceptGlosa where md.invoicenumber =D.Invoicenumber and D.state = 4 and  c.code <> 100) > 0 THEN 'Por Devolucion'  
	END StateInvalidate
	FROM Portfolio.RadicateInvoiceC  C INNER join
	Portfolio.RadicateInvoiceD D on c.id = D.radicateInvoiceCid
	WHERE d.InvoiceNumber = @InvoiceNumber
END