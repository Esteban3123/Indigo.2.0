






CREATE view [Billing].[vFURIPS2]
as
(
select Radicado,NumeroFactura,Ingreso,Tipo, CodigoCUM, Descripcion, sum(Cantidad) as Cantidad, ValorUnitario as ValorUnitario, sum(SubTotal) as SubTotal, sum(Total) as Total
from
(
--	Obtengo los no Qx
select 
rc.Id as Radicado,
i.InvoiceNumber as NumeroFactura,
i.AdmissionNumber as Ingreso,
case ce.RIPSConcept when '14' then 5 when '09' then 5 else 2 end as Tipo,
case ce.RIPSConcept when '14' then '' when '09' then '' else ips.Code end as CodigoCUM,
LEFT(ips.Name, 40) as Descripcion,
sod.InvoicedQuantity as Cantidad,
sod.SubTotalSalesPrice as ValorUnitario,
sod.InvoicedQuantity * sod.SubTotalSalesPrice as SubTotal,
sod.InvoicedQuantity * sod.SubTotalSalesPrice as Total
from Portfolio.RadicateInvoiceC rc with (nolock)
inner join Portfolio.RadicateInvoiceD rd with (nolock)  on rc.Id = rd.RadicateInvoiceCId
inner join Portfolio.AccountReceivable ar with (nolock)  on ar.InvoiceNumber = rd.InvoiceNumber
inner join Billing.Invoice i with (nolock)  on i.Id = ar.InvoiceId
inner join Billing.InvoiceDetail id with (nolock)  on id.InvoiceId = i.Id
inner join Billing.ServiceOrderDetail sod with (nolock)  on sod.Id = id.ServiceOrderDetailId
inner join Contract.CUPSEntity ce with (nolock) on ce.Id = sod.CUPSEntityId
inner join Contract.IPSService ips with (nolock)  on ips.Id = sod.IPSServiceId
where sod.Presentation in (1,3) and sod.SubTotalSalesPrice > 0
union all

select 
rc.Id as Radicado,
i.InvoiceNumber as NumeroFactura,
i.AdmissionNumber as Ingreso,
2 as Tipo,
ips.Code as CodigoCUM,
LEFT(ips.Name,40) as Descripcion,
sod.InvoicedQuantity as Cantidad,
sods.TotalSalesPrice as ValorUnitario,
sod.InvoicedQuantity * sods.TotalSalesPrice as SubTotal,
sod.InvoicedQuantity * sods.TotalSalesPrice as Total
from Portfolio.RadicateInvoiceC rc with (nolock) 
inner join Portfolio.RadicateInvoiceD rd  with (nolock)  on rc.Id = rd.RadicateInvoiceCId
inner join Portfolio.AccountReceivable ar  with (nolock) on ar.InvoiceNumber = rd.InvoiceNumber
inner join Billing.Invoice i  with (nolock) on i.Id = ar.InvoiceId
inner join Billing.InvoiceDetail id  with (nolock) on id.InvoiceId = i.Id
inner join Billing.ServiceOrderDetail sod with (nolock)  on sod.Id = id.ServiceOrderDetailId
inner join Billing.ServiceOrderDetailSurgical sods   with (nolock) on sods.ServiceOrderDetailId = sod.Id
inner join Contract.IPSService ips  with (nolock)  on ips.Id = sods.IPSServiceId
where sod.Presentation = 2 and sods.TotalSalesPrice > 0


union all

select 
rc.Id as Radicado,
i.InvoiceNumber as NumeroFactura,
i.AdmissionNumber as Ingreso,
case pt.Class when 2 then 1 else 5 end as Tipo,
case pt.Class when 2 then ip.CodeCUM else '' end as CodigoCUM,
LEFT(ip.Name, 40) as Descripcion,
sod.InvoicedQuantity as Cantidad,
sod.SubTotalSalesPrice as ValorUnitario,
sod.InvoicedQuantity * sod.SubTotalSalesPrice as SubTotal,
sod.InvoicedQuantity * sod.SubTotalSalesPrice as Total
from Portfolio.RadicateInvoiceC rc with (nolock) 
inner join Portfolio.RadicateInvoiceD rd  with (nolock) on rc.Id = rd.RadicateInvoiceCId
inner join Portfolio.AccountReceivable ar  with (nolock) on ar.InvoiceNumber = rd.InvoiceNumber
inner join Billing.Invoice i  with (nolock) on i.Id = ar.InvoiceId
inner join Billing.InvoiceDetail id  with (nolock) on id.InvoiceId = i.Id
inner join Billing.ServiceOrderDetail sod  with (nolock) on sod.Id = id.ServiceOrderDetailId
inner join Inventory.InventoryProduct ip  with (nolock) on ip.Id = sod.ProductId
inner join Inventory.ProductType pt  with (nolock) on pt.Id = ip.ProductTypeId
where sod.SubTotalSalesPrice > 0
) as data group by Radicado,NumeroFactura,Ingreso,Tipo, CodigoCUM, Descripcion, ValorUnitario

)