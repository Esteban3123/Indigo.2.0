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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta la trazabilidad completa de devoluciones de glosas asociadas a una factura específica. Dado un número de factura, retorna todas las recepciones de devolución (cabecera y detalle) junto con los movimientos de glosa vinculados, mostrando el consecutivo y fecha de radicación, el estado de la radicación y de la factura (sin confirmar, confirmado o anulado), el tipo de devolución (justificada o injustificada), el concepto de glosa aplicado con su código y nombre, los comentarios del auditor y las respuestas registradas. Se utiliza para hacer seguimiento y auditoría del ciclo de vida de glosas devueltas por entidades pagadoras (aseguradoras/EPS) sobre una factura de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceabilityDevolution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceabilityDevolution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la trazabilidad de devoluciones asociadas a una factura, mostrando estados de radicado y factura, tipo de devolución y concepto de glosa aplicado.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un detalle de devolución (GlosaDevolutionsReceptionD) cuya factura coincida con el número solicitado para retornar filas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los estados válidos reconocidos para radicado y factura son 1 (Sin Confirmar), 2 (Confirmado) y 4 (Anulado); cualquier otro valor queda sin etiqueta.; El tipo de devolución es binario: 1 = Justificada; cualquier otro valor se considera Injustificada.; Se conservan los registros aunque no exista movimiento de devolución ni concepto de glosa asociado (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de factura; Glosa; Radicado; Concepto de glosa; Devolución justificada/injustificada; Estado de confirmación/anulación', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando d.InvoiceNumber coincide con el parámetro, retorna consecutivo y fecha de radicado, estados traducidos, tipo de devolución, concepto de glosa (código + nombre), comentario y respuesta.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.State = 1 / 2 / 4 → Traduce el estado del radicado a ''Sin Confirmar'', ''Confirmado'' o ''Anulado'' respectivamente else NULL (no asigna etiqueta); si D.State = 1 / 2 / 4 → Traduce el estado de la factura devuelta a ''Sin Confirmar'', ''Confirmado'' o ''Anulado'' else NULL; si m.TypeDevolution = 1 → Clasifica la devolución como ''Justificada'' else ''Injustificada''', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaDevolutionsReceptionC; Glosas.GlosaDevolutionsReceptionD; Glosas.GlosaMovementDevolutions; Common.ConceptGlosas', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceabilityDevolution';
-- GO
