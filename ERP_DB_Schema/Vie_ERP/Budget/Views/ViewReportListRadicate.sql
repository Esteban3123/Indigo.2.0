
CREATE VIEW [Budget].[ViewReportListRadicate]
AS

select ROW_NUMBER() OVER(ORDER BY c.Regimen, c.Tipo) as consecutivo, c.Regimen, c.Tipo, c.valorRC as ValorRC, c.ValorLiquidacion as ValorLiquidacion,
c.valorRC - c.ValorLiquidacion as Diferencia, c.DocumentDate
from
(
select   Budget.fnEntityType(cg.EntityType) as Regimen, Budget.fnCollectionType(i.DocumentType) as Tipo, cr.DocumentDate, 
ipa.Value as ValorLiquidacion, cr.Value as ValorRC
from Treasury.CashReceipts as cr inner join  Portfolio.PortfolioAdvance as pa on cr.id = pa.CashReceiptId
inner join Billing.InvoicePortfolioAdvance as ipa on ipa.PortfolioAdvanceId = pa.Id inner join Billing.Invoice as i on i.Id = ipa.InvoiceId  and i.[Status] =1
inner join [Contract].CareGroup as cg on i.CareGroupId = cg.Id 
 where (cr.[Status] = 2) or
(cr.[Status] = 4 and month(cr.ReversedDate) <> month(cr.DocumentDate))
UNION all
select  'Particulares' as Regimen, 'Ventas de Contado Productos' as Tipo, cr.DocumentDate, 
crar.Value as ValorLiquidacion, cr.Value as ValorRC
from Treasury.CashReceipts as cr inner join Treasury.CashReceiptDetails as crd on cr.Id = crd.IdCashReceipt
inner join Treasury.CashReceiptAccountReceivable as crar on crd.id = crar.CashReceiptDetailId
inner join Billing.Invoice as i on i.InvoiceNumber = crar.InvoiceNumber 
 where (cr.[Status] = 2) or
(cr.[Status] = 4 and month(cr.ReversedDate) <> month(cr.DocumentDate))
) as c
--group by c.Regimen, c.Tipo
--order by Regimen