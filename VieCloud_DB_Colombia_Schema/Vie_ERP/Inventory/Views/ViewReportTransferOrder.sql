

CREATE VIEW [Inventory].[ViewReportTransferOrder]
AS
SELECT
ROW_NUMBER() OVER (ORDER BY tord.Id) AS Id,
tor.Code,
tor.CreationDate,
u.UserCode AS 'codeAuxPharmacy',
per.Fullname,
thi.Nit,
iip.Id As 'IdProduct',
iip.Code AS 'CodeProduct',
iip.Name AS 'NameProduct',
bs.ExpirationDate,
tord.Quantity,
iip.ProductCost,
w.Code AS 'codeWarehouse',
bs.BatchCode,
atc.Concentration,
thi.Name AS 'thirdPartyName',
(iim.Code + ' - ' + iim.Name) AS 'measurementUnit',
fun.Code AS 'codeUnitFunctional',
fun.Name,
w.Name AS 'NameWarehouse',
u.UserCode AS 'UserCodeNameAux'
FROM Inventory.TransferOrder AS tor
LEFT JOIN Inventory.TransferOrderDetail AS tord ON Tord.TransferOrderId = tor.Id
LEFT JOIN Inventory.TransferOrderDetailBatchSerial AS tordb ON tordb.TransferOrderDetailId = tord.Id
LEFT JOIN payroll.FunctionalUnit AS fun ON tor.TargetFunctionalUnitId = fun.Id
LEFT JOIN Inventory.Warehouse AS w ON w.Id = tor.SourceWarehouseId
INNER JOIN Common.ThirdParty AS thi ON tor.ThirdPartyId = thi.Id
INNER JOIN Inventory.InventoryProduct AS iip ON tord.ProductId = iip.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim ON iip.MeasurementUnitId = iim.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
INNER JOIN Inventory.PhysicalInventory AS phi ON tordb.PhysicalInventoryId = phi.Id
LEFT JOIN Inventory.BatchSerial AS bs ON phi.BatchSerialId = bs.Id
LEFT JOIN Security.[User] AS u ON tor.CreationUser= u.UserCode
INNER JOIN Security.Person AS per ON u.IdPerson = per.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de órdenes de traslado de inventario que consolida, por cada línea de producto despachado, la información del encabezado de la orden (código, fecha de creación, tercero destinatario y bodega de origen), el detalle del producto solicitado (código, nombre, concentración ATC, unidad de medida, costo y cantidad), el lote o serial asociado con su fecha de vencimiento, la unidad funcional de destino y el usuario farmacéutico responsable de la gestión. Integra las tablas de órdenes de traslado, sus detalles, lotes/seriales, inventario físico, catálogo de productos, terceros, bodegas y unidades funcionales para ofrecer una vista completa de cada movimiento de mercancía entre almacenes, centros de atención o terceros. Está orientada a la generación de informes de trazabilidad de traslados, control de stock por lote y auditoría de movimientos de medicamentos e insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportTransferOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportTransferOrder';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la información de órdenes de traslado de inventario con su detalle de productos, lotes/seriales, bodega origen, unidad funcional destino, tercero y usuario creador para generar reportes operativos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportTransferOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada orden de traslado debe tener un tercero asociado (ThirdPartyId) existente en Common.ThirdParty.; Cada detalle de orden debe referenciar un producto vigente en Inventory.InventoryProduct.; El usuario creador (CreationUser) debe existir en Security.User y tener una persona asociada en Security.Person.; Todo detalle reportado con lote/serial debe tener un PhysicalInventory asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportTransferOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El emparejamiento usuario-persona se hace por UserCode (tor.CreationUser = u.UserCode), no por Id de usuario.; La bodega reportada corresponde siempre a la bodega origen (SourceWarehouseId) y la unidad funcional al destino (TargetFunctionalUnitId).; Sólo se reportan registros que tengan tercero, producto y usuario-persona válidos (joins INNER).; El costo, lote, fecha de expiración y concentración ATC se toman del producto y lote físicamente inventariado, garantizando trazabilidad por PhysicalInventory.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportTransferOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de traslado de inventario; Detalle de orden de traslado; Lote/serial; Inventario físico; Bodega origen; Unidad funcional destino; Tercero; Producto de inventario; Unidad de medida; Clasificación ATC; Costo de producto; Fecha de expiración', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportTransferOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewReportTransferOrder: Devuelve una fila por cada combinación orden-detalle-lote/serial; si no existe lote/serial asociado en TransferOrderDetailBatchSerial, la fila se excluye por el INNER JOIN con PhysicalInventory.; [RETURN_RESULT] Inventory.ViewReportTransferOrder: Excluye órdenes cuyo usuario creador no exista en Security.User o no tenga persona asociada (INNER JOIN con Security.Person).; [RETURN_RESULT] Inventory.ViewReportTransferOrder: Asigna un Id consecutivo (ROW_NUMBER) ordenado por el Id del detalle de la orden de traslado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportTransferOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.TransferOrder; Inventory.TransferOrderDetail; Inventory.TransferOrderDetailBatchSerial; payroll.FunctionalUnit; Inventory.Warehouse; Common.ThirdParty; Inventory.InventoryProduct; Inventory.InventoryMeasurementUnit; Inventory.ATC; Inventory.PhysicalInventory; Inventory.BatchSerial; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportTransferOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportTransferOrder';
GO
