

    /*******************************************************************************************************************
Nombre: [Report].[ViewInventarioFisico]
Tipo:Vista
Observacion:Inventario fisico por almacen
Profesional: Nilsson Miguel Galindo Lopez
Fecha:03-08-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 01-02-2023
Observaciones: Se agrega el campo precio de venta por requerimiento de San Francisco
--------------------------------------
Version 3
Persona que modifico:Nilsson Miguel Galindo Lopez
Fecha: 21-11-2023
Observacion:Se adiciona los campos de IVA
--------------------------------------
Version 4
Persona que modifico:Amira Gil Meneses
Fecha: 19-03-2024
Observación:Se adiciona al reporte los campos de Grupo y Subgrupo del Producto- Solicitud INCS
--***********************************************************************************************************************************/

CREATE view [Report].[ViewInventarioFisico] as

WITH 
CTE_ENTRADA AS
(
select RED.ProductId,RE.DocumentDate,PRO.Name from 
Inventory.EntranceVoucher RE inner join
Inventory.EntranceVoucherDetail RED ON RE.Id=RED.EntranceVoucherId INNER JOIN 
Common.Supplier PRO ON RE.SupplierId=PRO.Id
)
SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
w.Name AS ALMACEN, 
INV.Code AS [CODIGO DE PRODUCTO], 
INV.CodeCUM AS [CODIGO CUM],
INV.Name AS PRODUCTO, 
ATC.CODE AS [CODIGO ATC],
DCI.CODE AS [CODIGO DCI],
AR.NAME AS [VIA DE ADMINISTRACION],
PG.NAME AS [GRUPO FARMACOLOGICO],
GR.name AS [GRUPO PRODUCTO],
SUB.name AS [SUBGRUPO PRODUCTO],
ATC.CONCENTRATION AS CONCENTRACION, 
RL.NAME AS [NIVEL DE RIESGO],
b.BatchCode AS LOTE, 
FAB.Name AS FABRICANTE,
CASE INV.ProductControl WHEN 0 THEN 'NO' 
						WHEN 1 THEN 'SI' END AS [DE CONTROL],
CASE INV.POSProduct WHEN 1 THEN 'SI'
					WHEN 0 THEN 'NO' END AS POS,
b.ExpirationDate AS [FECHA DE VENCIMIENTO], 
CASE WHEN DATEDIFF(DAY,GETDATE(),b.ExpirationDate)<=0 THEN 0 else DATEDIFF(DAY,GETDATE(),b.ExpirationDate) end AS [DIAS PARA VENCIMIENTO],
F.Quantity AS [CANTIDAD ACTUAL], 
INV.ProductCost AS [COSTO PROMEDIO], 
INV.FinalProductCost AS [COSTO ULTIMA COMPRA], 
CE.IvaValue AS [IVA ULTIMA COMPRA],
CE.IvaPercentage AS [% IVA ULTIMA COMPRA],
/*IN V2*/INV.SellingPrice AS [PRECIO DE VENTA],/*FN V2*/
CONVERT(date, INV.LastPurchase, 103) AS [FECHA ULTIMA COMPRA],
INV.MinimumStock AS [STOK MINIMO],
INV.MaximumStock AS [STOK MAXIMO],
(select top(1)[Name] from CTE_ENTRADA EN where INV.Id=EN.ProductId ORDER BY EN.DocumentDate ASC) AS PROVEEDOR,
INV.HealthRegistration AS [REGISTRO SANITARIO],
CAST(INV.ExpirationDate AS DATE) AS [FECHA DE VENCIMIENTO RS],
CAST(INV.LastPurchase AS date) AS [FECHA BUSQUEDA],
YEAR(INV.LastPurchase) AS [AÑO FECHA BUSQUEDA],
MONTH(INV.LastPurchase) AS [MES AÑO FECHA BUSQUEDA],
CASE MONTH(INV.LastPurchase) WHEN 1 THEN 'ENERO'
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
							 WHEN 12 THEN 'DICIEMBRE' END AS [MES NOMBRE FECHA BUSQUEDA],
FORMAT(DAY(INV.LastPurchase), '00') AS [DIA FECHA BUSQUEDA],
CONCAT(FORMAT(MONTH(b.ExpirationDate), '00') ,' - ', 
CASE MONTH(b.ExpirationDate) WHEN 1 THEN 'ENERO'
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
							 WHEN 12 THEN 'DICIEMBRE'END) MES_LABEL_VENCIMIENTO,
YEAR(b.ExpirationDate) AS [AÑO FECHA VENCIMIENTO],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM   
Inventory.PhysicalInventory AS F INNER JOIN
Inventory.Warehouse AS w ON w.Id = F.WarehouseId AND F.Quantity <> 0 INNER JOIN
Inventory.InventoryProduct AS INV ON INV.Id = F.ProductId LEFT JOIN
Inventory.ProductGroup AS GR ON INV.ProductGroupId=GR.Code LEFT JOIN
Inventory.ProductSubGroup As SUB ON INV.ProductSubGroupId=SUB.Id LEFT JOIN
Inventory.BatchSerial AS b ON b.Id = F.BatchSerialId LEFT JOIN
Inventory.ATC AS ATC ON INV.ATCId=ATC.Id LEFT JOIN
Inventory.DCI AS DCI ON DCI.Id =ATC.DCIId LEFT JOIN
Inventory.AdministrationRoute AS AR ON ATC.AdministrationRouteId =AR.Id LEFT JOIN
Inventory.PharmacologicalGroup AS PG ON ATC.PharmacologicalGroupiD =PG.ID 
LEFT JOIN Inventory.InventoryRiskLevel AS RL ON ATC.InventoryRiskLevelId =RL.ID
LEFT JOIN Inventory.Manufacturer FAB ON INV.ManufacturerId=FAB.Id
LEFT JOIN Inventory.EntranceVoucherDetail CE ON F.ProductId=CE.ProductId AND CE.Id=(SELECT MAX(C.ID) FROM Inventory.EntranceVoucherDetail C WHERE F.ProductId=C.ProductId)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el inventario físico por almacén con cantidad distinta de cero, exponiendo para cada producto su clasificación farmacológica (ATC, DCI, vía de administración, grupo farmacológico, grupo y subgrupo), datos de lote (código, fecha de vencimiento y días restantes), costos (promedio, última compra, IVA), precio de venta, stocks mínimo/máximo, registro sanitario y proveedor histórico más antiguo. Está diseñada para consumo en reportes de control de inventario y vencimientos por bodega.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el inventario físico vigente por bodega y lote, enriqueciéndolo con clasificación del producto (ATC, DCI, grupo, subgrupo, vía, riesgo), datos de vencimiento, costos, IVA y proveedor de la última entrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Inventory.PhysicalInventory con cantidad distinta de cero asociados a una bodega y producto válidos.; El producto debe estar en Inventory.InventoryProduct; las clasificaciones (ATC, DCI, grupo, subgrupo, vía, riesgo, fabricante, lote) son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen existencias con cantidad distinta de cero (F.Quantity <> 0).; El proveedor reportado corresponde al de la PRIMERA entrada histórica del producto (TOP 1 de CTE_ENTRADA ordenado por DocumentDate ASC).; Los datos de IVA y % IVA de ''última compra'' se toman del detalle de entrada con el MAX(Id) por producto en Inventory.EntranceVoucherDetail (proxy de la entrada más reciente).; La fecha de actualización (ULT_ACTUAL) se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; ID_COMPANY se identifica con el nombre de la base de datos actual truncado a 9 caracteres (DB_NAME).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Bodega/Almacén; Lote y fecha de vencimiento; Clasificación ATC; DCI (principio activo); Vía de administración; Grupo farmacológico; Grupo y subgrupo de producto; Nivel de riesgo del inventario; Producto de control; Producto POS; Costo promedio y costo de última compra; IVA de última compra; Precio de venta; Stock mínimo y máximo; Registro sanitario; Proveedor; Fabricante', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewInventarioFisico: Devuelve una fila por combinación bodega/producto/lote con cantidad ≠ 0 (filtro F.Quantity <> 0 en el JOIN con Warehouse).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INV.ProductControl = 0 / = 1 → Etiqueta ''DE CONTROL'' como ''NO'' / ''SI'' respectivamente.; si INV.POSProduct = 1 / = 0 → Etiqueta ''POS'' como ''SI'' / ''NO'' respectivamente.; si DATEDIFF(DAY, GETDATE(), b.ExpirationDate) <= 0 → Días para vencimiento se reporta como 0 (producto vencido). else Se reporta la diferencia real en días hasta la fecha de vencimiento.; si MONTH(INV.LastPurchase) y MONTH(b.ExpirationDate) → Se traduce el número de mes (1-12) a su nombre en español (ENERO…DICIEMBRE) para etiquetas de mes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Common.Supplier; Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup; Inventory.BatchSerial; Inventory.ATC; Inventory.DCI; Inventory.AdministrationRoute; Inventory.PharmacologicalGroup; Inventory.InventoryRiskLevel; Inventory.Manufacturer', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioFisico';
GO
