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

END