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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la trazabilidad de radicación de una factura específica ante la entidad pagadora (EPS, aseguradora o empresa). Recibe el número de factura como parámetro y retorna el consecutivo de radicado, la fecha de radicación, el usuario que confirmó, el estado del radicado (sin confirmar, confirmado o anulado) y el estado de la factura dentro de ese radicado. Además, determina si la factura fue invalidada o devuelta consultando los movimientos de glosa y devolución asociados, distinguiendo si la anulación fue por error en cartera (concepto código 100) o por devolución por otro concepto de glosa. Sirve para auditar el ciclo de cobro y glosa de una factura: desde su radicación ante el pagador hasta su posible devolución o anulación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceabilityRadication';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceabilityRadication';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta la trazabilidad de radicación de una factura, mostrando datos del radicado, estado del radicado, estado de la factura y motivo de invalidación si aplica.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityRadication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en el detalle de radicación (Portfolio.RadicateInvoiceD) vinculada a un encabezado en Portfolio.RadicateInvoiceC.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityRadication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El concepto de glosa con code = 100 representa ''Error en Cartera''; cualquier otro concepto se clasifica como ''Devolución''.; El motivo de invalidación solo aplica cuando la factura está en estado 4 (Anulado).; Los estados manejados para radicado y factura son únicamente 1 (Sin Confirmar), 2 (Confirmado) y 4 (Anulado).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityRadication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de factura; Trazabilidad de factura; Glosa; Devolución de glosa; Anulación de factura; Error en cartera; Concepto de glosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityRadication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.RadicateInvoiceD: Devuelve consecutivo y fecha de radicación, usuario que confirma, y estados textualizados del radicado y de la factura para la factura cuyo InvoiceNumber coincide con el parámetro.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityRadication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.State = 1 → Estado del radicado se reporta como ''Sin Confirmar''; si C.State = 2 → Estado del radicado se reporta como ''Confirmado''; si C.State = 4 → Estado del radicado se reporta como ''Anulado''; si D.State = 1 → Estado de la factura se reporta como ''Sin Confirmar''; si D.State = 2 → Estado de la factura se reporta como ''Confirmado''; si D.State = 4 → Estado de la factura se reporta como ''Anulado''; si No existen movimientos de devolución de glosas asociados a la factura → El motivo de invalidación se reporta vacío; si Existen movimientos de devolución con concepto de glosa code = 100 y la factura está anulada (D.State = 4) → El motivo de invalidación se reporta como ''Por Error en Cartera''; si Existen movimientos de devolución con concepto de glosa code distinto de 100 y la factura está anulada (D.State = 4) → El motivo de invalidación se reporta como ''Por Devolucion''', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityRadication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Glosas.GlosaMovementDevolutions; Common.Conceptglosas', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityRadication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityRadication';
-- GO
