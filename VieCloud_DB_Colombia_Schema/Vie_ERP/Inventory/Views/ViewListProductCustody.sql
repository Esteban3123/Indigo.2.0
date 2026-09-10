
CREATE VIEW [Inventory].[ViewListProductCustody]
AS

SELECT 
	f.Id AS ID, P.Code AS ProductCode, P.Name AS ProductName, IIF(P.MeasurementUnitId IS NOT NULL,IMU.Name, IIF( P.ATCId IS NOT NULL,rout.name,'')) AS  PharmaceuticalForm, BS.BatchCode , 
	bS.ExpirationDate,f.Quantity, f.AdmissionNumber, RTRIM(LTRIM(pa.IPCODPACI)) + ' - ' + RTRIM(LTRIM(pa.IPNOMCOMP)) AS Paciente,a.Code AS WarehouseCode, a.Name WareHouseName    
FROM Inventory.PhysicalInventoryCustody f INNER JOIN
Inventory.InventoryProduct p ON p.Id = f.ProductId INNER JOIN
Inventory.Warehouse a ON a.Id = f.WarehouseId   INNER JOIN
--Billing.RevenueControl RC on RC.Id = f.RevenueControlId inner join  
dbo.ADINGRESO RC  ON f.AdmissionNumber=RC.NUMINGRES INNER JOIN 
 Inventory.BatchSerial BS ON BS.Id = f.BatchSerialId INNER JOIN 
dbo.INPACIENT Pa ON pa.IPCODPACI = RC.IPCODPACI LEFT JOIN
Inventory.InventoryMeasurementUnit IMU ON IMU.Id = P.MeasurementUnitId LEFT JOIN
Inventory.ATC AS atc ON atc.id = p.ATCId LEFT JOIN
Inventory.AdministrationRoute AS rout ON rout.Id = atc.AdministrationRouteId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de productos en custodia del inventario físico, asociados a un paciente y su número de ingreso o admisión. Combina información del producto (código, nombre, forma farmacéutica o vía de administración), el lote o serial (código de lote y fecha de vencimiento), la bodega de almacenamiento, la cantidad en custodia, el número de admisión y los datos del paciente (cédula y nombre completo). Sirve para identificar qué medicamentos o insumos están bajo custodia para un paciente hospitalizado o en atención, permitiendo trazabilidad de entregas y control de inventario por ingreso clínico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListProductCustody';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListProductCustody';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos en custodia del inventario físico, asociándolos al paciente, su admisión, bodega, lote y forma farmacéutica o vía de administración.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListProductCustody';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de custodia debe tener un producto, bodega, lote/serie y número de admisión válidos; La admisión debe existir en dbo.ADINGRESO y referir a un paciente existente en dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListProductCustody';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen custodias cuyo producto, bodega, lote/serie, admisión y paciente existan; El identificador y nombre del paciente se muestran concatenados, sin espacios sobrantes; La forma farmacéutica nunca queda nula: se sustituye por unidad de medida, vía de administración o cadena vacía', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListProductCustody';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Custodia de inventario; Paciente; Admisión/Ingreso; Producto/Medicamento; Lote y fecha de vencimiento; Bodega; Forma farmacéutica; Vía de administración; Unidad de medida; Clasificación ATC', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListProductCustody';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un registro por cada custodia física que tenga producto, bodega, admisión, lote/serie y paciente relacionados (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListProductCustody';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El producto tiene MeasurementUnitId no nulo → Se muestra el nombre de la unidad de medida como forma farmacéutica else Si tiene ATCId no nulo se muestra el nombre de la vía de administración; en caso contrario, cadena vacía', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListProductCustody';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventoryCustody; Inventory.InventoryProduct; Inventory.Warehouse; dbo.ADINGRESO; Inventory.BatchSerial; dbo.INPACIENT; Inventory.InventoryMeasurementUnit; Inventory.ATC; Inventory.AdministrationRoute', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListProductCustody';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListProductCustody';
GO
