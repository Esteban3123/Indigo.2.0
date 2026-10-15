




CREATE       VIEW [Inventory].[Onco_Ordendecompra_VS_Entrada] 

AS

select  OC.CODE NroOrdenCompra, OC.CreationDate AS FechaOC,OC.DeliveredDate FechaVencOC, CE.Code as NroCEntrada, CE.CreationDate Fecha, CE.InvoiceNumber NroFactura, CE.InvoiceDate FechaFactura,PROV.Code Nit, PROV.Name NombreProveedor, 
al.name NombAlmacen, (case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) TIPO, 
PD.CODE as CodProd, PD.Name Nomproducto, 
PD.HealthRegistration IVIMA, OCD.Quantity CantOrdenada, CEDL.Quantity CantRecibida, LT.BatchCode Lote, LT.ExpirationDate, OCD.Value VlUnitOrdenC , CED.UnitValue VlUnitFact,
Ocd.TotalValue VlTotalOrdenC, CED.TotalValue VlTotFact, ocd.IvaValue VlImpOC,CED.IvaValue Impfact,CE.TotalValue as VlTotalFact

from Inventory.PurchaseOrder OC WITH (nolock) 
Inner JOIN Inventory.PurchaseOrderDetail OCD WITH (nolock) on OCD.PurchaseOrderId = OC.ID
inner join Inventory.Warehouse al WITH (nolock) on oc.WarehouseId = al.Id
inner join Common.Supplier PROV WITH (nolock) on PROV.Id = oc.SupplierId
inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = ocd.ProductId
left join Inventory.EntranceVoucherDetail CED WITH (nolock) on ocd.Id = CED.PurchaseOrderDetailId
LEFT JOIN Inventory.EntranceVoucher as CE WITH (nolock) on ce.id = CED.EntranceVoucherId
left join Inventory.EntranceVoucherDetailBatchSerial CEDL WITH (nolock) on CED.Id = CEDL.EntranceVoucherDetailId
left join Inventory.BatchSerial LT WITH (nolock) on CEDL.BatchSerialId = LT.Id

---ORDER BY  OC.CODE 

---where ce.code ='ACBA0000000001'