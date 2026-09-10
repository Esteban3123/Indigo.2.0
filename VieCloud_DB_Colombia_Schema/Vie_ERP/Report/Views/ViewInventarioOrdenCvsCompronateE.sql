

/*******************************************************************************************************************
Nombre: Report.ViewInventarioOrdenCvsCompronateE
Tipo:Vista
Observacion:Comprobante de entrada VS orden de entrada
Profesional: Nilsson Miguel Galindo Lopez
Fecha:25-07-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: Nilsson Galindo
Fecha: 26-10-2023
Ovservaciones: Se quita condición, para que muestre todas las ordenes de compra
--------------------------------------
Vercion 2
Persona que modifico:Nilsson Miguel Galindo Lopez
Fecha:1-11-2023
Ovservaciones:Se modifica la logica para que tambien el informe muestre las ordenes de entrada a si no tenga 
			  asociada una orden de compra.
--------------------------------------
Vercion 3
Persona que modifico:Nilsson Miguel Galindo Lopez
Fecha:12-03-2024
Ovservaciones:sE GREGAN LOS CAMPOS DE LOTE, EMPAQUE, REGISTRO INVIME, CADENA DE FRIO Y CUENTAS POR PAGAR.
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewInventarioOrdenCvsCompronateE]
as

select 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
TER.Nit AS [NIT PROVEEDOR],
PRO.Name AS [PROVEEDOR],
ISNULL(OC.Code,'') AS [ORDEN DE COMPRA],
OC.ConfirmationDate AS [FECHA DE DOCUMENTO],
ALM.Name AS ALMACEN,
INV.Code+' - '+INV.Name AS PRODUCTO,
LOT.BatchCode AS [LOTE/SERIAL],
EMP.NAME AS [EMPAQUE],
INV.HealthRegistration AS [REGISTRO INVIMA],
CASE INV.Storage WHEN 1 THEN 'Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)'
				 WHEN 2 THEN 'Ambiente controlada: 20 ° C - 25 ° C'
				 WHEN 3 THEN 'En frío: 8 ° C - 15 ° C'
				 WHEN 4 THEN 'Refrigerador: 2 ° C - 8 ° C'
				 WHEN 5 THEN 'Congelador: -25 ° C - 10 ° C' END AS [CADENA DE FRIO],
CONVERT(VARCHAR,CAST(OCD.Value AS MONEY),1) AS [VALOR UNITARIO O.C.],
CONVERT(VARCHAR,CAST(OCD.IvaValue AS MONEY),1) AS [VALOR DE IVA O.C.],
CONVERT(VARCHAR,CAST(OCD.SubTotalValue AS MONEY),1) AS [SUBTOTAL O.C.],
CONVERT(VARCHAR,CAST(OCD.TotalValue AS MONEY),1) AS [TOTAL VALOR O.C.],
ISNULL(OCD.Quantity,0) AS [CANTIDAD ORDEN DE COMPRA],
OED.Quantity AS [CANTIDAD COMPROBANTE DE ENTRADA],
ISNULL(OCD.OutstandingQuantity,-(OED.Quantity)) AS [CANTIDAD PENDIENTE],
OE.Code as [COMPROBANTE DE ENTRADA],
OE.ConfirmationDate AS [FECHA COMPROBANTE DE ENTRADA],
OE.InvoiceNumber AS FACTURA,
OE.InvoiceDate AS [FECHA FACTURA],
CONVERT(VARCHAR,CAST(OED.UnitValue AS MONEY),1) AS [VALOR UNITARIO C.E.],
CONVERT(VARCHAR,CAST(OED.IvaValue AS MONEY),1) AS [VALOR IVA C.E.],
CONVERT(VARCHAR,CAST(OED.SubTotalValue AS MONEY),1) AS [SUBTOTAL C.E.],
CONVERT(VARCHAR,CAST(OED.TotalValue AS MONEY),1) AS [TOTAL VALOR C.E.],
CP.CODE AS [CUENTA POR PAGAR],
 CAST(ISNULL(OE.ConfirmationDate,OC.ConfirmationDate) AS date) AS 'FECHA BUSQUEDA',
 YEAR(ISNULL(OE.ConfirmationDate,OC.ConfirmationDate)) AS 'AÑO FECHA BUSQUEDA',
 MONTH(ISNULL(OE.ConfirmationDate,OC.ConfirmationDate)) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH(ISNULL(OE.ConfirmationDate,OC.ConfirmationDate)) WHEN 1 THEN 'ENERO'
							   WHEN 2 THEN 'FEBRERO'
							   WHEN 3 THEN 'MARZO'
							   WHEN 4 THEN 'ABRIL'
							   WHEN 5 THEN 'MAYO'
							   WHEN 6 THEN 'JUNIO'
							   WHEN 7 THEN 'JULIO'
							   WHEN 8 THEN 'AGOSTO'
							   WHEN 9 THEN 'SEPTIEMBRE'
							   WHEN 10 THEN 'OCTUBRE'
							   WHEN 11 THEN 'NOVIEMBRE'
							   WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA',
FORMAT(DAY(ISNULL(OE.ConfirmationDate,OC.ConfirmationDate)), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(ISNULL(OE.ConfirmationDate,OC.ConfirmationDate)), '00') ,' - ', 
CASE MONTH(ISNULL(OE.ConfirmationDate,OC.ConfirmationDate)) WHEN 1 THEN 'ENERO'
							  WHEN 2 THEN 'FEBRERO'
							  WHEN 3 THEN 'MARZO'
							  WHEN 4 THEN 'ABRIL'
							  WHEN 5 THEN 'MAYO'
							  WHEN 6 THEN 'JUNIO'
							  WHEN 7 THEN 'JULIO'
							  WHEN 8 THEN 'AGOSTO'
							  WHEN 9 THEN 'SEPTIEMBRE'
							  WHEN 10 THEN 'OCTUBRE'
							  WHEN 11 THEN 'NOVIEMBRE'
							  WHEN 12 THEN 'DICIEMBRE' END) MES_LABEL_INGRESO,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from 
Inventory.PurchaseOrder OC 
INNER JOIN Inventory.PurchaseOrderDetail OCD ON OC.Id=OCD.PurchaseOrderId and OC.Status=2
FULL OUTER JOIN Inventory.EntranceVoucherDetail OED ON OCD.id=OED.PurchaseOrderDetailId
INNER JOIN Inventory.InventoryProduct INV ON ISNULL(OCD.ProductId,OED.ProductId)=INV.Id 
INNER JOIN Inventory.PackagingUnit EMP ON INV.PackagingUnitId=EMP.Id
LEFT JOIN Inventory.EntranceVoucher OE ON OED.EntranceVoucherId=OE.Id and OE.status=2
LEFT JOIN Common.Supplier PRO ON ISNULL(OC.SupplierId,OE.SupplierId)=PRO.Id
LEFT JOIN Common.ThirdParty TER ON PRO.IdThirdParty=TER.Id
LEFT JOIN Inventory.Warehouse ALM ON ISNULL(OC.WarehouseId,OE.WarehouseId)=ALM.Id
LEFT JOIN Payments.AccountPayable CP ON OE.Id=CP.EntityId AND CP.EntityName='EntranceVoucher'
LEFT JOIN Inventory.Kardex KD ON OE.ID=KD.EntityId AND KD.EntityName = 'EntranceVoucher'
LEFT JOIN Inventory.BatchSerial LOT ON KD.BatchSerialId=LOT.Id
--WHERE OE.status=2

--15.237

--SELECT TOP 100 * FROM Inventory.EntranceVoucherDetail WHERE EntranceVoucherId='5661'
--SELECT TOP 100 * FROM Inventory.EntranceVoucher WHERE InvoiceNumber='FEV92921'

----select top 100* from Inventory.InventoryProduct
--select top 100* from Inventory.Kardex where EntityName = 'EntranceVoucher' and EntityId=5661
--SELECT * FROM Inventory.BatchSerial
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que cruza órdenes de compra confirmadas con sus comprobantes de entrada al inventario, permitiendo comparar cantidades pedidas vs. recibidas y cantidades pendientes por producto. Incluye datos del proveedor, almacén, lote/serial, registro INVIMA, cadena de frío, valores monetarios (unitario, IVA, subtotal, total) tanto de la orden como del comprobante, factura asociada y cuenta por pagar vinculada. Soporta filtrado temporal por año, mes y día usando la fecha del comprobante o de la orden como referencia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que confronta órdenes de compra contra comprobantes de entrada al inventario, mostrando cantidades, valores, lote, empaque, registro INVIMA, cadena de frío y cuenta por pagar asociada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de compra consideradas deben tener Status=2 (confirmadas) para emparejarse con su detalle.; Los comprobantes de entrada se incluyen únicamente cuando OE.Status=2 (confirmados); de lo contrario el lado de la entrada queda nulo.; El kardex se asocia sólo cuando EntityName=''EntranceVoucher''.; La cuenta por pagar se vincula sólo cuando AccountPayable.EntityName=''EntranceVoucher''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se reportan órdenes de compra confirmadas (OC.Status=2) y comprobantes de entrada confirmados (OE.Status=2).; El proveedor y el almacén se resuelven priorizando los de la orden de compra y, si no existen, los del comprobante de entrada (ISNULL(OC.SupplierId,OE.SupplierId), ISNULL(OC.WarehouseId,OE.WarehouseId)).; El producto se obtiene de la OC si existe, en caso contrario del comprobante de entrada (ISNULL(OCD.ProductId,OED.ProductId)).; ID_COMPANY se fija al nombre de la base de datos actual truncado a 9 caracteres.; La marca de tiempo ULT_ACTUAL se entrega convertida a la zona horaria ''Pakistan Standard Time''.; Los valores monetarios se entregan formateados como MONEY con separador de miles (CONVERT(VARCHAR, CAST(... AS MONEY),1)).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de compra; Comprobante de entrada; Proveedor; NIT; Almacén/Bodega; Producto de inventario; Lote/Serial; Empaque; Registro INVIMA; Cadena de frío; IVA; Factura; Cuenta por pagar; Kardex', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewInventarioOrdenCvsCompronateE: Devuelve una fila por combinación de detalle de orden de compra y/o detalle de comprobante de entrada (FULL OUTER JOIN entre PurchaseOrderDetail y EntranceVoucherDetail), permitiendo mostrar entradas sin OC y OC sin entrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INV.Storage IN (1..5) → Traduce el código de almacenamiento a etiqueta de cadena de frío (Ambiente, Ambiente controlada, En frío, Refrigerador, Congelador con sus rangos de temperatura).; si OCD.OutstandingQuantity IS NULL → La cantidad pendiente se calcula como -(OED.Quantity), reflejando entrada sin orden de compra asociada. else Usa OCD.OutstandingQuantity como cantidad pendiente.; si OE.ConfirmationDate IS NULL → La ''FECHA BUSQUEDA'' (y sus derivados año/mes/día/etiquetas) se toma de OC.ConfirmationDate. else Se usa OE.ConfirmationDate como fecha de búsqueda.; si MONTH(fecha búsqueda) entre 1 y 12 → Se traduce el número de mes a su nombre en español (ENERO..DICIEMBRE) y se construye etiqueta ''MM - NOMBRE''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PurchaseOrder; Inventory.PurchaseOrderDetail; Inventory.EntranceVoucherDetail; Inventory.InventoryProduct; Inventory.PackagingUnit; Inventory.EntranceVoucher; Common.Supplier; Common.ThirdParty; Inventory.Warehouse; Payments.AccountPayable; Inventory.Kardex; Inventory.BatchSerial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioOrdenCvsCompronateE';
GO
