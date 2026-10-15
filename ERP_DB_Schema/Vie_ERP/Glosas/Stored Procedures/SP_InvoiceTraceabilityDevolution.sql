-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 21/09/2014
-- Description:	Procedimientos para listar las devoluciones de la factura
-- =============================================
CREATE PROCEDURE [Glosas].[SP_InvoiceTraceabilityDevolution]
	@InvoiceNumber varchar(50)
AS
BEGIN
	
	select c.RadicatedConsecutive,c.RadicatedDate,
	case when C.State = 1 THEN 'Sin Confirmar' when C.State = 2 THEN 'Confirmado' when C.State = 4 then 'Anulado' END StateRadicate,
	case when D.State = 1 THEN 'Sin Confirmar' when D.State = 2 THEN 'Confirmado' when D.State = 4 then 'Anulado' END StateInvoice,
	case when m.TypeDevolution = 1 THEN 'Justificada' ELSE 'Injustificada' END TypeDevolution,
	con.Code + ' - ' +con.NameSpecific as NameSpecific,
	m.Comment,
	m.Answer   
	from Glosas.GlosaDevolutionsReceptionC c inner join
	Glosas.GlosaDevolutionsReceptionD d on c.id = d.GlosaDevolutionsReceptionCId left join
	Glosas.GlosaMovementDevolutions m on m.IdDevolutionsReceptionD = d.Id left join
	Common.ConceptGlosas con on m.IdConceptGlosa = con.id
	WHERE d.InvoiceNumber = @InvoiceNumber

END