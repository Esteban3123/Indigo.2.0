



CREATE     VIEW [Inventory].[Onco_Entrada_VS_Ordendecompra] 

AS

select CE.Code as NroCEntrada, CE.CreationDate Fecha, CE.InvoiceNumber NroFactura, CE.InvoiceDate FechaFactura, OC.CODE NroOrdenCompra,PROV.Code Nit, PROV.Name NombreProveedor, 
al.name NombAlmacen, (case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) TIPO, PD.CODE as CodProd, PD.Name Nomproducto, OCD.Quantity CantOrdenada, CED.Quantity CantRecibida, OCD.Value VlUnitOrdenC , CED.UnitValue VlUnitFact,
Ocd.TotalValue VlTotalOrdenC, CED.TotalValue VlTotFact, ocd.IvaValue VlImpOC,CED.IvaValue Impfact,CE.TotalValue
from Inventory.EntranceVoucher as CE
inner join Inventory.EntranceVoucherDetail CED WITH (nolock) on CE.Id = CED.EntranceVoucherId
inner join Inventory.Warehouse al WITH (nolock) on CE.WarehouseId = al.Id
inner join Common.Supplier PROV WITH (nolock) on PROV.Id = CE.SupplierId
inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = CED.ProductId
LEFT JOIN Inventory.PurchaseOrderDetail OCD WITH (nolock) on CED.PurchaseOrderDetailId = OCD.ID
LEFT JOIN Inventory.PurchaseOrder OC WITH (nolock) on OCD.PurchaseOrderId = OC.ID

---where ce.code ='ACBA0000000001'