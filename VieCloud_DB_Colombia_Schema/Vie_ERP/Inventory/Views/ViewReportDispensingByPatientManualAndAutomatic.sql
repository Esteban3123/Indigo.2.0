

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de dispensación de medicamentos e insumos por paciente, que consolida tanto entregas manuales como automáticas (inmediatas y diferidas/fraccionadas). Integra las fórmulas médicas con el detalle de cada producto recetado, el tipo y número de identificación del paciente, el grupo farmacológico o categoría ATC del medicamento, y la información de la bodega desde donde se despachó. Para cada ítem muestra las cantidades solicitadas, entregadas y pendientes —distinguiendo si la entrega es diferida o inmediata—, la fecha de solicitud de la fórmula y la fecha de la última dispensación farmacéutica. Sirve para reportería operativa y de auditoría de farmacia, permitiendo rastrear qué medicamentos o insumos fueron recetados y cuántos fueron efectivamente entregados a cada paciente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportDispensingByPatientManualAndAutomatic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportDispensingByPatientManualAndAutomatic';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un reporte unificado las dispensaciones de medicamentos e insumos por paciente, combinando entregas manuales y automáticas (incluyendo entregas diferidas) sobre cada fórmula médica.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDispensingByPatientManualAndAutomatic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente de la fórmula debe existir en INPACIENT (INNER JOIN); El producto formulado debe existir en InventoryProduct (INNER JOIN); La fórmula debe tener al menos un detalle en MedicalFormulaDetail', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDispensingByPatientManualAndAutomatic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una fórmula con producto sin ATC se considera insumo y no reporta grupo farmacológico; Para entregas diferidas, las cantidades provienen del cronograma diferido y no del detalle principal de la fórmula; La ''Fecha Última Entrega'' y el almacén corresponden a la dispensación farmacéutica con MAX(Id) por fórmula y producto; El cruce entre fórmula y dispensación se hace por MedicalFormulaId y por código de producto', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDispensingByPatientManualAndAutomatic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Fórmula médica; Paciente; Tipo de documento de identidad; Producto / medicamento / insumo; Clasificación ATC; Grupo farmacológico; Entrega diferida (fraccionada); Dispensación farmacéutica; Almacén / bodega; Cantidades solicitada, entregada y pendiente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDispensingByPatientManualAndAutomatic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna una fila por cada detalle de fórmula médica con datos del paciente, producto, grupo farmacológico, almacén de última entrega y cantidades (solicitada, entregada, pendiente)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDispensingByPatientManualAndAutomatic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.IPTIPODOC entre 1 y 8 → Traduce el código numérico al nombre del tipo de documento (Cédula de Ciudadanía, Cédula de Extranjería, Tarjeta de Identidad, Registro Civil, Pasaporte, Adulto/Menor Sin Identificación, NUIP); si atc.Id IS NULL (producto sin clasificación ATC) → Reporta ''No especifica grupo farmacologico porque el producto no tiene asociado un ATC ya que es un insumo'' else Muestra el código y nombre del grupo farmacológico (pg.Code + '' - '' + pg.Name); si mfd.IsDeferred = 1 (entrega diferida) → Toma cantidades desde MedicalFormulaDetailDeferred: Solicitada=DeliveryQuantity, Entregada=DeliveryQuantity-PendingQuantity, Pendiente=PendingQuantity else Toma cantidades directamente del detalle de la fórmula: RequestQuantity, DeliveryQuantity y PendingQuantity', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDispensingByPatientManualAndAutomatic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.MedicalFormula; INPACIENT; Inventory.MedicalFormulaDetail; Inventory.InventoryProduct; Inventory.ATC; Inventory.PharmacologicalGroup; Inventory.MedicalFormulaDetailDeferred; Inventory.MedicalFormulaPharmaceuticalDispensing; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDispensingByPatientManualAndAutomatic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDispensingByPatientManualAndAutomatic';
GO
