

CREATE VIEW [Report].[ViewInventarioConceptos] as

WITH CTE_CONCEPTOS AS
(
select 
'03' AS CONCEPTO,
'Comprobante de Entrada' AS DESCRIPCION,
WarehouseId,ProductId,DocumentDate,Quantity,AverageCost
from Inventory.Kardex where EntityName = 'EntranceVoucher'
UNION ALL
select 
'04' AS CONCEPTO,
'Devolucion de Compra' AS DESCRIPCION,
WarehouseId,ProductId,DocumentDate,Quantity,AverageCost
from Inventory.Kardex where EntityName = 'EntranceVoucherDevolution'

UNION ALL
select 
'09' AS CONCEPTO,
'Dispensación Farmaceutica' AS DESCRIPCION,
WarehouseId,ProductId,DocumentDate,Quantity,AverageCost
from Inventory.Kardex where EntityName = 'PharmaceuticalDispensing'
UNION ALL
select 
'10' AS CONCEPTO,
'Devolucion Dispensacion Farmaceutica' AS DESCRIPCION,
WarehouseId,ProductId,DocumentDate,Quantity,AverageCost
from Inventory.Kardex where EntityName = 'PharmaceuticalDispensingDevolution'
UNION ALL
select 
'13' AS CONCEPTO,
'Orden de Traslado' AS DESCRIPCION,
K.WarehouseId,K.ProductId,K.DocumentDate,K.Quantity,K.AverageCost
from Inventory.Kardex K INNER JOIN
Inventory.TransferOrder OT ON K.EntityId=OT.ID AND OT.AdjustmentConceptId=5109
where K.EntityName = 'TransferOrder'
UNION ALL
select 
'14' AS CONCEPTO,
'Devolucion Orden de Traslado' AS DESCRIPCION,
WarehouseId,ProductId,DocumentDate,Quantity,AverageCost
from Inventory.Kardex where EntityName = 'TransferOrderDevolution'
),

CTE_PIVOT AS
(
SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
P.Code AS [Codigo Producto],
P.Name AS [Producto],
AL.Code AS [Codigo Almacen],
AL.Name AS Almacen,
CON.DESCRIPCION AS [Descripcion Concepto],
SUM(CON.Quantity) AS Cantidad,
CON.AverageCost AS [Valor Unitario],
YEAR(CON.DocumentDate) AS 'AÑO FECHA BUSQUEDA',
YEAR(CON.DocumentDate) AS 'AÑO BUSQUEDA',
MONTH(CON.DocumentDate) AS 'MES BUSQUEDA',
CONCAT(FORMAT(MONTH(CON.DocumentDate), '00') ,' - ', 
	   CASE MONTH(CON.DocumentDate) 
	    WHEN 1 THEN 'ENERO'
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
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM
CTE_CONCEPTOS CON INNER JOIN
Inventory.InventoryProduct P ON CON.ProductId=P.ID INNER JOIN 
Inventory.Warehouse AL ON CON.WarehouseId=AL.Id
GROUP BY 
P.Code,P.Name,AL.Code,AL.Name,CON.DESCRIPCION,CON.AverageCost,YEAR(CON.DocumentDate),MONTH(CON.DocumentDate)
)
--select * from cte_pivot--194.786
SELECT * FROM 
(

	SELECT 
	ID_COMPANY,
	[Codigo Producto],
	Producto,
	[Codigo Almacen],
	Almacen,
	Pre.[Comprobante de Entrada],
	Pre.[Devolucion de Compra],
	Pre.[Dispensación Farmaceutica],
	Pre.[Devolucion Dispensacion Farmaceutica],
	Pre.[Orden de Traslado],
	Pre.[Devolucion Orden de Traslado],
	[Valor Unitario],
	[AÑO FECHA BUSQUEDA],
	[MES BUSQUEDA],
	[MES NOMBRE BUSQUEDA],
	ULT_ACTUAL
	FROM
	CTE_PIVOT
	SOURCE PIVOT (SUM(CANTIDAD) FOR [Descripcion Concepto] IN (
	[Comprobante de Entrada],
	[Devolucion de Compra],
	[Dispensación Farmaceutica],
	[Devolucion Dispensacion Farmaceutica],
	[Orden de Traslado],
	[Devolucion Orden de Traslado]))AS Pre
) AS CONCEPTOS
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que aplana y pivota los movimientos del kardex de inventario agrupados por seis conceptos (comprobantes de entrada, devoluciones de compra, dispensaciones farmacéuticas, devoluciones de dispensación, órdenes de traslado y sus devoluciones). Para cada combinación de producto, almacén, costo promedio y período (año/mes), presenta las cantidades de cada concepto como columnas independientes, facilitando el análisis mensual del inventario por empresa (DB_NAME()) con marca de última actualización.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que pivota los movimientos del kardex de inventario por producto, almacén y mes, mostrando las cantidades por concepto (entradas, devoluciones, dispensaciones y traslados) junto al costo promedio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Inventory.Kardex debe contener registros con EntityName en {''EntranceVoucher'',''EntranceVoucherDevolution'',''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution'',''TransferOrder'',''TransferOrderDevolution''}.; Para conceptos de ''Orden de Traslado'' debe existir el TransferOrder relacionado con AdjustmentConceptId = 5109.; ProductId y WarehouseId del kardex deben existir en Inventory.InventoryProduct e Inventory.Warehouse respectivamente (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran traslados cuyo AdjustmentConceptId = 5109; otros conceptos de ajuste de TransferOrder quedan excluidos.; ID_COMPANY se obtiene de DB_NAME() truncado a 9 caracteres.; ULT_ACTUAL se calcula con GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; La cantidad por celda pivotada es la suma (SUM) de Quantity agrupada por producto, código de producto, almacén, código de almacén, concepto, AverageCost, año y mes del DocumentDate.; Los registros de Kardex sin producto o sin almacén válido en los catálogos quedan excluidos por el INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex de inventario; Comprobante de entrada; Devolución de compra; Dispensación farmacéutica; Devolución de dispensación farmacéutica; Orden de traslado; Devolución de orden de traslado; Producto; Almacén/Bodega; Costo promedio; Concepto de ajuste de inventario', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultado de la vista): Devuelve un set pivotado por producto/almacén/mes con columnas para cada concepto: Comprobante de Entrada (03), Devolucion de Compra (04), Dispensación Farmaceutica (09), Devolucion Dispensacion Farmaceutica (10), Orden de Traslado (13), Devolucion Orden de Traslado (14).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Inventory.Kardex.EntityName = ''EntranceVoucher'' → Movimiento clasificado como concepto ''03'' Comprobante de Entrada; si Inventory.Kardex.EntityName = ''EntranceVoucherDevolution'' → Movimiento clasificado como concepto ''04'' Devolucion de Compra; si Inventory.Kardex.EntityName = ''PharmaceuticalDispensing'' → Movimiento clasificado como concepto ''09'' Dispensación Farmaceutica; si Inventory.Kardex.EntityName = ''PharmaceuticalDispensingDevolution'' → Movimiento clasificado como concepto ''10'' Devolucion Dispensacion Farmaceutica; si Inventory.Kardex.EntityName = ''TransferOrder'' AND TransferOrder.AdjustmentConceptId = 5109 → Movimiento clasificado como concepto ''13'' Orden de Traslado else Si AdjustmentConceptId ≠ 5109 el movimiento no se incluye en el reporte; si Inventory.Kardex.EntityName = ''TransferOrderDevolution'' → Movimiento clasificado como concepto ''14'' Devolucion Orden de Traslado; si MONTH(DocumentDate) en 1..12 → Se traduce al nombre del mes en español (ENERO..DICIEMBRE) y se concatena con el número de mes formateado a 2 dígitos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.TransferOrder; Inventory.InventoryProduct; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConceptos';
GO
