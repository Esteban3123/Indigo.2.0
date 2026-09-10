

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de servicios y productos facturados para la generación del archivo RIPS (Registro Individual de Prestación de Servicios) tipo FU (Facturas), asociados a radicados de cartera. Integra los radicados de cobro ante entidades pagadoras (EPS, aseguradoras) con las facturas, sus líneas de detalle y los ítems de la orden de servicio, clasificando cada ítem según su tipo RIPS: medicamentos (tipo 1, con código CUM), procedimientos y servicios (tipo 2, con código CUPS), honorarios quirúrgicos (también tipo 2, tomando el precio del detalle quirúrgico) y estancias u otros conceptos sin código (tipo 5). Agrupa los registros por radicado, número de factura, número de ingreso, tipo RIPS, código y descripción del servicio o producto, entregando cantidad total, valor unitario y subtotales necesarios para la reportería obligatoria de RIPS ante los entes de control y las entidades pagadoras.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'vFURIPS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'vFURIPS2';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los ítems facturados (procedimientos, cirugías, medicamentos e insumos) asociados a facturas radicadas ante pagadores, clasificándolos por tipo RIPS para reportes FURIPS.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben estar radicadas (existir en Portfolio.RadicateInvoiceC/D) y vinculadas a una cuenta por cobrar (Portfolio.AccountReceivable).; Los ítems deben tener valor de venta positivo (SubTotalSalesPrice > 0 o TotalSalesPrice > 0).; Para ítems quirúrgicos debe existir el detalle en Billing.ServiceOrderDetailSurgical.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ítems con valor de venta estrictamente positivo.; La descripción se trunca a 40 caracteres (LEFT(Name,40)).; El SubTotal y Total siempre se calculan como InvoicedQuantity * precio unitario.; Los ítems no quirúrgicos se identifican por Presentation IN (1,3); los quirúrgicos por Presentation=2.; El tipo RIPS resultante toma valores 1 (medicamento), 2 (procedimiento/IPS) o 5 (otros/insumos o conceptos RIPS 09/14).; Se eliminan duplicados por Radicado/Factura/Tipo/CodigoCUM/Descripcion/ValorUnitario al consolidar las tres fuentes vía UNION ALL + GROUP BY.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicado de factura; Factura de venta; Ingreso del paciente; RIPS (Registro Individual de Prestación de Servicios); CUPS; CUM (medicamentos); Procedimiento quirúrgico; Medicamentos e insumos; Servicios IPS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.vFURIPS2: Devuelve filas agrupadas por Radicado, NumeroFactura, Ingreso, Tipo, CodigoCUM, Descripcion y ValorUnitario, sumando Cantidad, SubTotal y Total.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ServiceOrderDetail.Presentation IN (1,3) y SubTotalSalesPrice > 0 (ítems no quirúrgicos) → Toma código y descripción de Contract.IPSService; Tipo=5 si CUPSEntity.RIPSConcept es ''14'' o ''09'', en otro caso Tipo=2; CodigoCUM se vacía cuando RIPSConcept es ''14'' o ''09''.; si ServiceOrderDetail.Presentation = 2 y ServiceOrderDetailSurgical.TotalSalesPrice > 0 (ítems quirúrgicos) → Asigna Tipo=2, toma código/descripción del IPSService asociado al detalle quirúrgico y usa TotalSalesPrice como ValorUnitario.; si ServiceOrderDetail con producto de inventario y SubTotalSalesPrice > 0 → Si ProductType.Class = 2 (medicamento) Tipo=1 y CodigoCUM=InventoryProduct.CodeCUM; en caso contrario Tipo=5 y CodigoCUM se vacía.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.AccountReceivable; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailSurgical; Contract.CUPSEntity; Contract.IPSService; Inventory.InventoryProduct; Inventory.ProductType', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2';
GO
