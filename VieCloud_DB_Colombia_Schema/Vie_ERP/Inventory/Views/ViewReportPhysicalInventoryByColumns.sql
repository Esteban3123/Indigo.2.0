CREATE VIEW [Inventory].[ViewReportPhysicalInventoryByColumns]
AS
SELECT 
ROW_NUMBER() OVER (ORDER BY pin.ProductId) AS Id,
iip.Code + ' - ' + iip.Name AS 'Product',
bs.BatchCode,
pf.Name AS 'PharmaceuticalFormIdName',
w.Name, 
w.Code AS 'WarehouseIdCode',
SUM(pin.Quantity) AS 'Quantity',
iip.Code AS 'ProductIdCode',
iip.Name AS 'ProductIdName',
mu.Name AS 'MeasurementUnitIdName',
pin.ProductId
FROM Inventory.[PhysicalInventory] AS pin
inner join Inventory.InventoryProduct iip ON iip.Id =pin.ProductId
INNER JOIN Inventory.Warehouse AS w ON w.Id = pin.WarehouseId
LEFT JOIN Inventory.BatchSerial AS bs ON bs.Id = pin.BatchSerialId
LEFT JOIN Inventory.InventoryMeasurementUnit AS mu ON mu.Id = iip.MeasurementUnitId
LEFT JOIN Inventory.ATC AS atc ON atc.Id = iip.ATCId
LEFT JOIN Inventory.AdministrationRoute AS ar ON ar.Id = atc.AdministrationRouteId
LEFT JOIN Inventory.PharmaceuticalForm AS pf ON pf.Id = ar.PharmaceuticalFormId
GROUP BY w.Code, iip.Code, bs.BatchCode, iip.Name, mu.Name, pf.Name, pin.ProductId, w.Name
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte para el inventario físico por columnas, que consolida las cantidades contadas en bodega para cada producto/medicamento agrupadas por bodega y lote. Integra el catálogo de productos (código y nombre), el lote o serial asociado, la forma farmacéutica, la unidad de medida y los datos de la bodega (código y nombre) en un único resultado tabular. Sirve para conciliar el stock físico real contra el sistema, permitiendo generar reportes de inventario físico detallados por producto, lote y punto de almacenamiento. Es el origen de datos típico para informes de toma de inventario, auditorías de stock y control de medicamentos e insumos en farmacia y almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPhysicalInventoryByColumns';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPhysicalInventoryByColumns';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el inventario físico por producto, bodega, lote y forma farmacéutica, sumando las cantidades para reporte.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPhysicalInventoryByColumns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa la cantidad total física por combinación de producto, lote, bodega, unidad de medida y forma farmacéutica.; Solo se incluyen registros con producto y bodega existentes (INNER JOIN sobre InventoryProduct y Warehouse).; Lote, unidad de medida y forma farmacéutica son opcionales (LEFT JOIN), por lo que pueden aparecer nulos.; La forma farmacéutica se deriva del producto a través de su clasificación ATC y vía de administración.; El identificador de fila (Id) es secuencial generado por ROW_NUMBER ordenado por ProductId, sin valor de negocio persistente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPhysicalInventoryByColumns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Producto; Bodega/Almacén; Lote/Serial; Unidad de medida; Clasificación ATC; Vía de administración; Forma farmacéutica', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPhysicalInventoryByColumns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas agregadas con SUM(Quantity) agrupadas por bodega (Code/Name), producto (Code/Name/Id), lote (BatchCode), unidad de medida y forma farmacéutica.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPhysicalInventoryByColumns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.Warehouse; Inventory.BatchSerial; Inventory.InventoryMeasurementUnit; Inventory.ATC; Inventory.AdministrationRoute; Inventory.PharmaceuticalForm', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPhysicalInventoryByColumns';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPhysicalInventoryByColumns';
GO
