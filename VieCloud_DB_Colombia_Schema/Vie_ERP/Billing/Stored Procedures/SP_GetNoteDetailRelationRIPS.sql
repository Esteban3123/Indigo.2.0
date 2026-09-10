-- =============================================
-- Author: Giovanny Plazas Lozano
-- Create date: 2025-04-07
-- Description:	Obtiene la informacion del detalle de la Nota para la relacion del RIPS Electronico
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetNoteDetailRelationRIPS]
    @CodeNote VARCHAR(20)
WITH RECOMPILE 
AS
BEGIN
    SET NOCOUNT ON
	-- Parameter Sniffing
	Declare @NoteCode VARCHAR(20)
	SET @NoteCode = @CodeNote;
	------------------------------------------------------------------------------------------------------------------
	--Informacion general NOTA DETALLE

	WITH Cte_NoteDetail as (select	pnarad.Id,  
									bn.Code BillingNoteCode,  
									bnd.InvoiceNumber,  
									pnarad.[Value] as TotalValue,  
									pnarad.BaseValue,  
									pnarad.TaxValue,  
									pnarad.TaxPercentage,
									pnarad.EntityName,
									pnarad.EntityId,
									bnd.InvoiceId
							FROM Billing.BillingNote bn WITH(NOLOCK)  
							JOIN Billing.BillingNoteDetail bnd WITH(NOLOCK) on bn.Id =bnd.BillingNoteId  
							JOIN Portfolio.PortfolioNote pn WITH(NOLOCK) on bn.EntityId = pn.Id and bn.EntityName ='PortfolioNote' and pn.NoteType = 6  
							JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on bnd.InvoiceId =ar.InvoiceId  
							JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pnara.PortfolioNoteId = pn.Id and ar.Id =pnara.AccountReceivableId  
							JOIN portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) on pnara.Id =pnarad.PortfolioNoteAccountReceivableId
							WHERE bn.Code =@NoteCode)
---------------------------------------------------
-- Consulta para InvoiceDetail

	select	cte.Id,  
			cte.BillingNoteCode,  
			cte.InvoiceNumber,  
			id.Id InvoiceDetailId,  
			cte.TotalValue,  
			cte.BaseValue,  
			cte.TaxValue,  
			cte.TaxPercentage,  
			idjm.ConsecutiveJson,  
			idjm.ServiceNameJson
	FROM Cte_NoteDetail cte WITH(NOLOCK)
	JOIN Billing.InvoiceDetail id WITH(NOLOCK) on cte.EntityName ='InvoiceDetail' and cte.EntityId = id.Id and id.InvoiceId = cte.InvoiceId  
	JOIN Billing.InvoiceDetailJsonMap idjm WITH(NOLOCK) on idjm.InvoiceDetailId = id.Id  

	UNION ALL 
	
---------------------------------------------------
-- Consulta para InvoiceDetailSurgical
	select	0 Id,  
			cte.BillingNoteCode,  
			cte.InvoiceNumber,  
			id.Id InvoiceDetailId,  
			SUM(cte.TotalValue) AS TotalValue,  
			SUM(cte.BaseValue) AS BaseValue,  
			SUM(cte.TaxValue) AS TaxValue,  
			cte.TaxPercentage,  
			idjm.ConsecutiveJson,  
			idjm.ServiceNameJson
	FROM Cte_NoteDetail cte WITH(NOLOCK)
	JOIN Billing.InvoiceDetailSurgical ids WITH(NOLOCK) on  cte.EntityName ='InvoiceDetailSurgical' and cte.EntityId =ids.Id
	JOIN Billing.InvoiceDetail id WITH(NOLOCK) on id.InvoiceId = cte.InvoiceId  and ids.InvoiceDetailId =id.Id
	JOIN Billing.InvoiceDetailJsonMap idjm WITH(NOLOCK) on idjm.InvoiceDetailId = id.Id  
	GROUP by cte.BillingNoteCode,cte.InvoiceNumber,
			id.Id,cte.TaxPercentage,idjm.ConsecutiveJson,
			idjm.ServiceNameJson

	UNION ALL

---------------------------------------------------
-- Consulta para ServiceOrderDetailSurgical
	select	0 Id,  
			cte.BillingNoteCode,  
			cte.InvoiceNumber,  
			id.Id InvoiceDetailId,  
			SUM(cte.TotalValue) AS TotalValue,  
			SUM(cte.BaseValue) AS BaseValue,  
			SUM(cte.TaxValue) AS TaxValue,  
			cte.TaxPercentage,  
			idjm.ConsecutiveJson,  
			idjm.ServiceNameJson
	FROM Cte_NoteDetail cte WITH(NOLOCK)
	JOIN Billing.ServiceOrderDetailSurgical sods WITH(NOLOCK) on  cte.EntityName ='ServiceOrderDetailSurgical' and cte.EntityId =sods.Id
	JOIN Billing.InvoiceDetail id WITH(NOLOCK) on id.InvoiceId = cte.InvoiceId  and id.ServiceOrderDetailId =sods.ServiceOrderDetailId
	JOIN Billing.InvoiceDetailJsonMap idjm WITH(NOLOCK) on idjm.InvoiceDetailId = id.Id
	GROUP by cte.BillingNoteCode,cte.InvoiceNumber,
			id.Id,cte.TaxPercentage,idjm.ConsecutiveJson,
			idjm.ServiceNameJson

	UNION ALL

---------------------------------------------------
-- Consulta para InitialBalanceInvoiceDetail (saldo inicial)
	SELECT	ibid.Id,
			cte.BillingNoteCode,
			cte.InvoiceNumber,
			ibid.Id AS InvoiceDetailId,
			cte.TotalValue,
			cte.BaseValue,
			cte.TaxValue,
			cte.TaxPercentage,
			ibid.Consecutive AS ConsecutiveJson,
			CASE ibid.ServiceType
				WHEN 1 THEN 'consultas'
				WHEN 2 THEN 'procedimientos'
				WHEN 5 THEN 'medicamentos'
				WHEN 6 THEN 'otrosServicios'
			END AS ServiceNameJson
	FROM Cte_NoteDetail cte WITH(NOLOCK)
	JOIN Portfolio.InitialBalanceInvoiceDetail ibid WITH(NOLOCK)
		ON cte.EntityName = 'InitialBalanceInvoiceDetail' AND cte.EntityId = ibid.Id
	WHERE ibid.ServiceType IN (1, 2, 5, 6)

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle de una nota de facturación (crédito o débito) identificada por su código, consolidando la información necesaria para la relación con el RIPS Electrónico. Cruza la nota de facturación con sus líneas de detalle, la nota de cartera asociada (tipo glosa o ajuste), las cuentas por cobrar y los anticipos aplicados, para obtener los valores base, impuestos y porcentajes de cada movimiento. El resultado cubre tres escenarios de servicios facturados: ítems de factura estándar (InvoiceDetail), procedimientos quirúrgicos facturados (InvoiceDetailSurgical) y procedimientos quirúrgicos de orden de servicio (ServiceOrderDetailSurgical), incluyendo el consecutivo y nombre del servicio en formato JSON para el reporte RIPS electrónico ante la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetNoteDetailRelationRIPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetNoteDetailRelationRIPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de una nota de facturación tipo 6 con su relación a las líneas de factura (directas, quirúrgicas o de orden de servicio quirúrgica) para construir el RIPS electrónico.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetNoteDetailRelationRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de nota recibido debe existir en Billing.BillingNote; La nota debe estar asociada a una Portfolio.PortfolioNote con NoteType = 6; Billing.BillingNote.EntityName debe ser ''PortfolioNote'' para enlazar con Portfolio.PortfolioNote; Debe existir el encadenamiento PortfolioNote → PortfolioNoteAccountReceivableAdvance → PortfolioNoteAccountReceivableDetail y la AccountReceivable debe coincidir con el InvoiceId del BillingNoteDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetNoteDetailRelationRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan notas cuya PortfolioNote tiene NoteType = 6; Solo se enlazan BillingNote cuyo EntityName = ''PortfolioNote''; Para los casos quirúrgicos (InvoiceDetailSurgical y ServiceOrderDetailSurgical) el Id de la fila resultado siempre es 0 y los valores monetarios se entregan sumarizados; Cada fila resultado siempre incluye ConsecutiveJson y ServiceNameJson provenientes de InvoiceDetailJsonMap, garantizando trazabilidad al JSON ministerial; Se utiliza WITH RECOMPILE y reasignación local del parámetro para mitigar parameter sniffing', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetNoteDetailRelationRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota de facturación; Nota de cartera (PortfolioNote tipo 6); Cuenta por cobrar; Anticipo de cartera; Detalle de factura; Detalle quirúrgico de factura; Orden de servicio quirúrgica; RIPS electrónico; Mapeo JSON ministerial (ConsecutiveJson/ServiceNameJson); Base gravable, IVA y porcentaje de impuesto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetNoteDetailRelationRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando PortfolioNoteAccountReceivableDetail.EntityName = ''InvoiceDetail'', retorna fila por línea uniendo con Billing.InvoiceDetail (id.InvoiceId = cte.InvoiceId y EntityId = id.Id) sin agregación, conservando el Id del detalle de la nota.; [RETURN_RESULT] resultset: Cuando EntityName = ''InvoiceDetailSurgical'', retorna fila con Id=0 agrupando por factura/InvoiceDetail/TaxPercentage/JSON y sumando TotalValue, BaseValue y TaxValue (vincula InvoiceDetailSurgical.Id = EntityId y su InvoiceDetailId con InvoiceDetail).; [RETURN_RESULT] resultset: Cuando EntityName = ''ServiceOrderDetailSurgical'', retorna fila con Id=0 agrupando y sumando TotalValue/BaseValue/TaxValue, enlazando ServiceOrderDetailSurgical.Id = EntityId y InvoiceDetail.ServiceOrderDetailId = sods.ServiceOrderDetailId.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetNoteDetailRelationRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName del detalle de la nota = ''InvoiceDetail'' → Se resuelve la relación directamente contra Billing.InvoiceDetail sin agregación de valores; si EntityName del detalle de la nota = ''InvoiceDetailSurgical'' → Se resuelve la relación contra Billing.InvoiceDetailSurgical y se agregan (SUM) los valores monetarios por InvoiceDetail; si EntityName del detalle de la nota = ''ServiceOrderDetailSurgical'' → Se resuelve la relación contra Billing.ServiceOrderDetailSurgical vinculando por ServiceOrderDetailId y se agregan (SUM) los valores monetarios', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetNoteDetailRelationRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingNote; Billing.BillingNoteDetail; Portfolio.PortfolioNote; Portfolio.AccountReceivable; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNoteAccountReceivableDetail; Billing.InvoiceDetail; Billing.InvoiceDetailJsonMap; Billing.InvoiceDetailSurgical; Billing.ServiceOrderDetailSurgical', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetNoteDetailRelationRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetNoteDetailRelationRIPS';
-- GO
