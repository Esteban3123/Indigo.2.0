

CREATE PROCEDURE [Report].[SP_ROTACION_POR_ALMACEN]
AS

SELECT 
	G.Code AS [CodigoAlmacen],
	G.Name AS [Almacen],
	G.Codigo AS [CodigoProducto],
	G.NombreProducto AS [DescripcionProducto],
	G.CUM,
	G.NOMBTIPO AS [TipoProducto],
	G.CODMedica AS [CodigoMedicamentoInsumo],
	G.NombMedica AS [DescripcionMedicamentoInsumo],
	G.COSTPROM AS [ValorPromedio],
	G.UCOMP AS [ValorFinal],
	-- 2023
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-1'  THEN (g.cantidad ) ELSE 0 END) '2023-1',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-2'  THEN (g.cantidad ) ELSE 0 END) '2023-2',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-3'  THEN (g.cantidad ) ELSE 0 END) '2023-3',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-4'  THEN (g.cantidad ) ELSE 0 END) '2023-4',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-5'  THEN (g.cantidad ) ELSE 0 END) '2023-5',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-6'  THEN (g.cantidad ) ELSE 0 END) '2023-6',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-7'  THEN (g.cantidad ) ELSE 0 END) '2023-7',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-8'  THEN (g.cantidad ) ELSE 0 END) '2023-8',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-9'  THEN (g.cantidad ) ELSE 0 END) '2023-9',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-10'  THEN (g.cantidad ) ELSE 0 END) '2023-10',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-11'  THEN (g.cantidad ) ELSE 0 END) '2023-11',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2023-12'  THEN (g.cantidad ) ELSE 0 END) '2023-12',
	-- 2024
	SUM(case when concat(year(G.DocumentDate),'-',month(G.DocumentDate)) = '2024-1'  THEN (G.cANTIDAD ) else 0 END) '2024-1',
	SUM(case when concat(year(G.DocumentDate),'-',month(G.DocumentDate)) = '2024-2'  THEN (G.cANTIDAD ) else 0 END) '2024-2',
	SUM(case when concat(year(G.DocumentDate),'-',month(G.DocumentDate)) = '2024-3'  THEN (G.cANTIDAD ) else 0 END) '2024-3',
	SUM(case when concat(year(G.DocumentDate),'-',month(G.DocumentDate)) = '2024-4'  THEN (G.cANTIDAD ) else 0 END) '2024-4',
	SUM(case when concat(year(G.DocumentDate),'-',month(G.DocumentDate)) = '2024-5'  THEN (G.cANTIDAD ) else 0 END) '2024-5',
	SUM(case when concat(year(G.DocumentDate),'-',month(G.DocumentDate)) = '2024-6'  THEN (G.cANTIDAD ) else 0 END) '2024-6',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-7'  THEN (g.cantidad ) ELSE 0 END) '2024-7',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-8'  THEN (g.cantidad ) ELSE 0 END) '2024-8',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-9'  THEN (g.cantidad ) ELSE 0 END) '2024-9',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-10'  THEN (g.cantidad ) ELSE 0 END) '2024-10',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-11'  THEN (g.cantidad ) ELSE 0 END) '2024-11',
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-12'  THEN (g.cantidad ) ELSE 0 END) '2024-12'
	FROM
	(

		SELECT 
			AL.Code ,AL.Name  ,pd.Code Codigo, PD.Name NombreProducto, pd.CodeCUM CUM, 
			(case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) NOMBTIPO,
			(case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END) CODMedica, (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END) NombMedica,
			PD.ProductCost AS COSTPROM, PD.FinalProductCost AS UCOMP,
			sum(IIF (K.MovementType =1, K.Quantity *-1, K.Quantity)) Cantidad,K.DocumentDate  
		FROM   inventory.Kardex AS K
		inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = k.ProductId
		JOIN Inventory.Warehouse AL WITH (nolock) on al.Id = k.WarehouseId
		LEFT join Inventory.ATC ATC WITH (nolock) on ATC.Id = PD.ATCId
		LEFT join Inventory.InventorySupplie Ins WITH (nolock) on ins.Id = PD.SupplieId
		WHERE  K.EntityName in ('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution') AND K.AffectInventory =1 --AND PD.Code ='0201147' 
		group by AL.Code ,AL.Name  ,pd.Code , PD.Name , pd.CodeCUM, 
		(case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END),
		(case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END), (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END),
		PD.ProductCost, PD.FinalProductCost,K.DocumentDate

		UNION

		SELECT 
			AL.Code ,AL.Name ,pd.Code Código, PD.Name NombreProducto, pd.CodeCUM CUM, 
			(case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) NOMBTIPO,
			(case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END) CODMedica, (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END) NombMedica,
			PD.ProductCost AS COSTPROM, PD.FinalProductCost AS UCOMP,
			sum(IIF (K.MovementType =1, K.Quantity *-1, K.Quantity)) Cantidad,K.DocumentDate
		FROM   inventory.Kardex AS K
		inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = k.ProductId
		JOIN Inventory.Warehouse AL WITH (nolock) on al.Id = k.WarehouseId
		LEFT join Inventory.ATC ATC WITH (nolock) on ATC.Id = PD.ATCId
		LEFT join Inventory.InventorySupplie Ins WITH (nolock) on ins.Id = PD.SupplieId
		LEFT JOIN Inventory .TransferOrder AS TOR ON K.EntityId =TOR.Id 
		WHERE K.EntityName in ('TransferOrder') AND TOR.OrderType = 2 AND K.MovementType =2 AND K.AffectInventory =1 --AND PD.Code ='0201147' --and AL.Code in ('044','047')
		group by AL.Code ,AL.Name  ,pd.Code , PD.Name , pd.CodeCUM, 
		(case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END),
		(case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END), (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END),
		PD.ProductCost, PD.FinalProductCost,K.DocumentDate

		UNION

		SELECT 
			AL.Code ,AL.Name ,pd.Code Código, PD.Name NombreProducto, pd.CodeCUM CUM, 
			(case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) NOMBTIPO,
			(case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END) CODMedica, (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END) NombMedica,
			PD.ProductCost AS COSTPROM, PD.FinalProductCost AS UCOMP,
			sum(IIF (K.MovementType =1, K.Quantity *-1, K.Quantity)) Cantidad,K.DocumentDate
		FROM   inventory.Kardex AS K
		inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = k.ProductId
		JOIN Inventory.Warehouse AL WITH (nolock) on al.Id = k.WarehouseId
		LEFT join Inventory.ATC ATC WITH (nolock) on ATC.Id = PD.ATCId
		LEFT join Inventory.InventorySupplie Ins WITH (nolock) on ins.Id = PD.SupplieId
		LEFT JOIN .Inventory .TransferOrder AS TOR ON K.EntityId =TOR.Id 
		LEFT JOIN Inventory .TransferOrderDevolution TOD ON TOD.TransferOrderId =TOR.Id 
		WHERE K.EntityName in ('TransferOrderDevolution') AND TOR.OrderType = 2 AND K.MovementType =1 AND K.AffectInventory =1 --AND PD.Code ='0201147' --and AL.Code in ('044','047')
		group by AL.Code ,AL.Name  ,pd.Code , PD.Name , pd.CodeCUM, 
		(case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END),
		(case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END), (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END),
		PD.ProductCost, PD.FinalProductCost,K.DocumentDate
	) G
	GROUP BY G.Code,G.Name ,G.Codigo ,G.NombreProducto ,G.CUM ,G.NOMBTIPO ,G.CODMedica ,G.NombMedica ,G.COSTPROM ,G.UCOMP
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que genera la rotación de inventario por almacén, consolidando cantidades movidas mes a mes en forma de tabla pivote para los años 2023 y 2024. Combina tres fuentes del Kardex: dispensaciones farmacéuticas (y sus devoluciones) y órdenes de traslado tipo 2 (con sus devoluciones), afectando solo movimientos que impactan inventario. Cada fila representa un producto por almacén, incluyendo su tipo (medicamento, insumo, nutrición), clasificación ATC o de insumo, costo promedio y costo final.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de rotación mensual (años 2023 y 2024) de productos por almacén, consolidando salidas por dispensación farmacéutica, traslados y devoluciones de traslado a partir del Kardex de inventario.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas Inventory.Kardex, Inventory.InventoryProduct, Inventory.Warehouse, Inventory.ATC e Inventory.InventorySupplie deben existir y estar pobladas.; Los movimientos del Kardex deben tener AffectInventory = 1 para ser considerados.; Para traslados, debe existir el registro en Inventory.TransferOrder con OrderType = 2.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran movimientos de Kardex con AffectInventory = 1.; Los movimientos con MovementType = 1 (salida) se contabilizan con signo negativo respecto a Quantity.; Para los bloques de traslado, solo se incluye OrderType = 2.; El reporte solo expone columnas mensuales de los años 2023 y 2024 (rango fijo de 24 meses).; El tipo de producto y la fuente del código/nombre del medicamento o insumo se determinan exclusivamente por PD.ProductTypeId (1=ATC, 2=InventorySupplie, 6=Nutriciones, otros=''OTR0'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Almacén/Bodega; Producto de inventario; Medicamento; Insumo; Nutrición; Clasificación ATC; CUM (Código Único de Medicamento); Kardex; Dispensación farmacéutica; Devolución de dispensación; Orden de traslado; Devolución de traslado; Costo promedio; Costo final del producto; Rotación de inventario', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un conjunto con código y nombre de almacén, datos del producto (código, nombre, CUM, tipo, código/nombre de medicamento o insumo, costo promedio y final) y 24 columnas de cantidades agregadas por mes (''YYYY-M'') para los años 2023 y 2024.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si K.EntityName IN (''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution'') AND K.AffectInventory = 1 → Suma cantidades del Kardex como movimiento de dispensación; si MovementType = 1 la cantidad se invierte (Quantity * -1), de lo contrario se toma tal cual.; si K.EntityName = ''TransferOrder'' AND TOR.OrderType = 2 AND K.MovementType = 2 AND K.AffectInventory = 1 → Suma cantidades de salidas por orden de traslado tipo 2 (entrada/salida según MovementType=2).; si K.EntityName = ''TransferOrderDevolution'' AND TOR.OrderType = 2 AND K.MovementType = 1 AND K.AffectInventory = 1 → Suma cantidades de devoluciones de traslado, invirtiendo el signo (MovementType=1 → Quantity * -1).; si PD.ProductTypeId = ''1'' → Clasifica como ''MEDICAMENTO'' y toma código/nombre desde Inventory.ATC.; si PD.ProductTypeId = ''2'' → Clasifica como ''INSUMOS'' y toma código/nombre desde Inventory.InventorySupplie. else Si ProductTypeId = ''6'' clasifica como ''NUTRICIONES''; cualquier otro valor se etiqueta como ''OTR0'' y deja códigos vacíos.; si CONCAT(YEAR(DocumentDate),''-'',MONTH(DocumentDate)) = ''YYYY-M'' → Acumula la cantidad en la columna correspondiente al mes; en otro caso aporta 0.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.InventoryProduct; Inventory.Warehouse; Inventory.ATC; Inventory.InventorySupplie; Inventory.TransferOrder; Inventory.TransferOrderDevolution', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_ROTACION_POR_ALMACEN';
-- GO
