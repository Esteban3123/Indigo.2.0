





CREATE VIEW [Inventory].[ViewReportDispensingByPatientManualAndAutomatic]
AS


select mf.Number 'No. Fórmula',
case pac.IPTIPODOC 
	when 1 then 'Cédula de Ciudadanía' 
	when 2 then 'Cédula de Extranjería' 
	when 3 then 'Tarjeta de Identidad' 
	when 4 then 'Registro Civil' 
	when 5 then 'Pasporte' 
	when 6 then 'Adulto Sin Identificación' 
	when 7 then 'Menor Sin Identificación' 
	when 8 then 'Número único de identificación personal' 
end 'Tipo Identificación',
mf.PatientCode 'Identificación Paciente',
mf.PatientName 'Nombre Paciente',
mfd.ProductCode 'Código Producto',
mfd.ProductName 'Nombre Producto',
case when atc.Id is null then 'No especifica grupo farmacologico porque el producto no tiene asociado un ATC ya que es un insumo' else pg.Code + ' - ' + pg.Name end 'Grupo Farmacologico',
pro.Description 'Descripción',
mfpd.WarehouseDescription 'Almacén',
case when mfd.IsDeferred = 1 then mfdd.DeliveryQuantity else mfd.RequestQuantity end 'Cantidad Solicitada',
case when mfd.IsDeferred = 1 then mfdd.DeliveryQuantity - mfdd.PendingQuantity else mfd.DeliveryQuantity end 'Cantidad Entregada',
case when mfd.IsDeferred = 1 then mfdd.PendingQuantity else mfd.PendingQuantity end 'Cantidad Pendiente',
mf.CreationDate 'Fecha Solicitud',
mfpd.CreationDate 'Fecha Ultima Entrega'
from Inventory.MedicalFormula mf
inner join .INPACIENT pac on pac.IPCODPACI = mf.PatientCode
inner join Inventory.MedicalFormulaDetail mfd on mfd.MedicalFormulaId = mf.Id
inner join Inventory.InventoryProduct pro on pro.Code = mfd.ProductCode
left join Inventory.ATC atc on atc.Id = pro.ATCId
left join Inventory.PharmacologicalGroup pg on pg.Id = atc.PharmacologicalGroupId
left join Inventory.MedicalFormulaDetailDeferred mfdd on mfdd.MedicalFormulaDetailId = mfd.Id
left join (
	select MAX(pd.Id) PharmaceuticalDispensingId, pd.CreationDate, mfpd2.MedicalFormulaId, wh.Code + ' - ' + wh.Name WarehouseDescription, pro2.Code
	from Inventory.MedicalFormulaPharmaceuticalDispensing mfpd2
	inner join Inventory.PharmaceuticalDispensing pd on pd.Id = mfpd2.PharmaceuticalDispensingId
	inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.PharmaceuticalDispensingId = pd.Id
	inner join Inventory.Warehouse wh on wh.Id = pdd.WarehouseId
	inner join Inventory.InventoryProduct pro2 on pro2.Id = pdd.ProductId
	group by mfpd2.MedicalFormulaId, pd.CreationDate, wh.Code, wh.Name, pdd.PharmaceuticalDispensingId, pro2.Code
) mfpd on mfpd.MedicalFormulaId = mf.Id and mfpd.Code = mfd.ProductCode