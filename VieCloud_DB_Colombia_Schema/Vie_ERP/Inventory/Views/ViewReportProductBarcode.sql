

CREATE VIEW [Inventory].[ViewReportProductBarcode]
AS
SELECT	CONCAT(phy.Id, '-') UUID,
			w.Id WarehouseId,
			CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName,
			ip.Id ProductId,
			ip.Code ProductCode,
			ip.Name ProducName,
			bs.Id BatchSerialId,
			bs.BatchCode,
			bs.ExpirationDate,
			pbc.Barcode,
			IIF(pbc.Barcode IS NULL,CONCAT(ip.Code,'*IND*', bs.BatchCode), pbc.Barcode) CalculatedBarCode,
			phy.Quantity
	FROM Inventory.PhysicalInventory phy
	JOIN Inventory.Warehouse w ON phy.WarehouseId = w.Id
	JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
	LEFT JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
	OUTER APPLY (
		Select top 1  pbcc.ProductId , pbcc.Barcode
		from Inventory.ProductBarcode pbcc
		where ip.Id = pbcc.ProductId
		ORDER by pbcc.CreationDate desc
	) as pbc
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte para etiquetas y códigos de barras de productos en inventario físico. Combina el inventario físico contado por bodega con el catálogo de productos, los lotes o seriales y sus códigos de barras registrados, permitiendo generar etiquetas o listados para escaneo en bodega. Cuando un producto no tiene código de barras registrado, calcula uno alternativo concatenando el código del producto con el código de lote (marcado como ''IND''), garantizando que siempre exista un identificador escaneable. Sirve para reportería operativa de control de stock, trazabilidad de lotes, fechas de vencimiento y gestión logística en almacenes o bodegas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportProductBarcode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportProductBarcode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida información de inventario físico con bodega, producto, lote y código de barras para reporte/etiquetado, generando un código de barras calculado cuando no existe uno registrado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportProductBarcode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se selecciona como máximo un código de barras por producto, el de mayor CreationDate (más reciente).; Cada fila siempre tendrá un CalculatedBarCode no nulo: si no hay Barcode, se sintetiza con código de producto + ''*IND*'' + código de lote.; La bodega y el producto son obligatorios (INNER JOIN); el lote/serial es opcional (LEFT JOIN).; El UUID de la fila se forma concatenando el Id del inventario físico con un guion.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportProductBarcode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Bodega/almacén; Producto; Lote y serial; Fecha de vencimiento; Código de barras', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportProductBarcode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PhysicalInventory: Devuelve una fila por cada registro de inventario físico, enriquecida con datos de bodega, producto, lote/serial y un único código de barras (el más reciente por CreationDate).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportProductBarcode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pbc.Barcode IS NULL (el producto no tiene código de barras registrado) → CalculatedBarCode se construye como CONCAT(ip.Code,''*IND*'', bs.BatchCode) else CalculatedBarCode toma el valor de pbc.Barcode existente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportProductBarcode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.BatchSerial; Inventory.ProductBarcode', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportProductBarcode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportProductBarcode';
GO
