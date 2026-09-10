

CREATE VIEW [Inventory].[ViewReportEntranceVoucher]
AS
SELECT
ROW_NUMBER() OVER (ORDER BY ievd.Id) AS Id,
(DATEADD(DAY,iev.DayPeriod,iev.InvoiceDate)) AS 'invoiceVtoDate',
iev.InvoiceNumber,
iev.InvoiceDate,
thi.Nit,
thi.Name AS 'NameThirdParty',
(SELECT TOP 1 Addresss FROM Common.[Address] a WHERE a.IdPerson = p.id) AS 'Addresss',
city.Name,
iip.Id As 'IdProduct',
iip.Code AS 'CodeProduct',
bs.ExpirationDate,
bs.BatchCode,
w.Code AS 'codeWarehouse',
ievdb.Quantity, 
(iimu.Code + ' - ' + iimu.Name) AS 'measurementUnit',
(((ievdb.Quantity * ievd.UnitValue) * (100 - ievd.DiscountPercentage) / 100) * (100 + ievd.IvaPercentage) / 100) TotalValue,
iev.CreationUser AS 'UserCodeNameAux', 
atc.Concentration,
iev.TotalValue AS 'totalValuePurchase',
iip.Name AS 'NameProduct',
w.Name AS 'NameWarehouse'
FROM 
Inventory.EntranceVoucher AS iev
LEFT JOIN Inventory.EntranceVoucherDetail AS ievd ON ievd.EntranceVoucherId = iev.Id
LEFT JOIN Inventory.EntranceVoucherDetailBatchSerial AS ievdb ON ievdb.EntranceVoucherDetailId = ievd.Id
INNER JOIN Inventory.Warehouse AS w ON iev.WarehouseId = w.Id
INNER JOIN Inventory.InventoryProduct AS iip ON ievd.ProductId = iip.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iimu ON iip.MeasurementUnitId = iimu.Id 
LEFT JOIN Inventory.BatchSerial AS bs ON ievdb.BatchSerialId = bs.Id
INNER JOIN Common.Supplier AS cs ON iev.SupplierId = cs.Id
INNER JOIN Common.City AS city ON cs.IdCity = city.Id
INNER JOIN Common.ThirdParty AS thi ON cs.IdThirdParty = thi.Id
INNER JOIN Common.Person p on p.Id = thi.PersonId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de comprobantes de entrada al inventario (recepciones de mercancía). Integra la cabecera del comprobante de entrada con el detalle por producto y el detalle de lotes o seriales, mostrando por cada ítem recibido: proveedor (NIT, nombre, dirección, ciudad), producto (código, nombre, concentración ATC, unidad de medida), lote (código de lote, fecha de vencimiento), bodega de destino, cantidad recibida, valor total calculado con descuentos e IVA, fecha de vencimiento de la factura y valor total de la compra. Sirve para reportería de compras y recepciones en almacén, trazabilidad de lotes por proveedor, auditoría de ingresos de medicamentos e insumos, y conciliación de facturas de compra con los productos efectivamente ingresados a bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportEntranceVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportEntranceVoucher';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los comprobantes de entrada de mercancía al inventario con su detalle de productos, lotes/seriales, proveedor, bodega y valores facturados para visualización gerencial.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada comprobante de entrada debe estar asociado a una bodega existente (Inventory.Warehouse).; Cada detalle debe referenciar un producto existente en Inventory.InventoryProduct.; El comprobante debe tener un proveedor válido en Common.Supplier con ciudad y tercero asociados.; El tercero debe estar vinculado a una persona en Common.Person.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El total por línea siempre aplica descuento antes de IVA.; La fecha de vencimiento siempre se deriva sumando el período de días a la fecha de la factura.; Solo se incluyen comprobantes con bodega, producto, proveedor, ciudad, tercero y persona válidos (INNER JOIN); detalle, lote/serial, ATC, unidad y BatchSerial son opcionales (LEFT JOIN).; Se entrega una única dirección por proveedor aun si la persona tiene varias registradas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada de inventario; Factura de proveedor; Fecha de vencimiento de factura; Lote y serial; Bodega; Producto/medicamento; Clasificación ATC; Unidad de medida; Proveedor; Tercero; Descuento e IVA en compras', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewReportEntranceVoucher: Calcula la fecha de vencimiento de la factura como InvoiceDate + DayPeriod días.; [RETURN_RESULT] Inventory.ViewReportEntranceVoucher: Calcula TotalValue por línea como ((Quantity * UnitValue) * (100 - DiscountPercentage)/100) * (100 + IvaPercentage)/100, aplicando primero el descuento y luego el IVA.; [RETURN_RESULT] Inventory.ViewReportEntranceVoucher: Selecciona solo la primera dirección (TOP 1) de la persona asociada al tercero proveedor como dirección reportada.; [RETURN_RESULT] Inventory.ViewReportEntranceVoucher: Concatena código y nombre de la unidad de medida en formato ''Code - Name''.; [RETURN_RESULT] Inventory.ViewReportEntranceVoucher: Genera un Id secuencial mediante ROW_NUMBER ordenado por el Id del detalle del comprobante.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucherDetailBatchSerial; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ATC; Inventory.InventoryMeasurementUnit; Inventory.BatchSerial; Common.Supplier; Common.City; Common.ThirdParty; Common.Person; Common.Address', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportEntranceVoucher';
GO
