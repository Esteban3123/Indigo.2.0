
CREATE VIEW [Report].[VIEW_ROTACION_POR_ALMACEN]
AS
select G.Code,G.Name ,G.Código ,G.NombreProducto ,G.CUM ,G.NOMBTIPO ,G.CODMedica ,G.NombMedica ,G.COSTPROM ,G.UCOMP,

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
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-1'  THEN (g.cantidad ) ELSE 0 END) '2024-1',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-2'  THEN (g.cantidad ) ELSE 0 END) '2024-2',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-3'  THEN (g.cantidad ) ELSE 0 END) '2024-3',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-4'  THEN (g.cantidad ) ELSE 0 END) '2024-4',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-5'  THEN (g.cantidad ) ELSE 0 END) '2024-5',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-6'  THEN (g.cantidad ) ELSE 0 END) '2024-6',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-7'  THEN (g.cantidad ) ELSE 0 END) '2024-7',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-8'  THEN (g.cantidad ) ELSE 0 END) '2024-8',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-9'  THEN (g.cantidad ) ELSE 0 END) '2024-9',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-10'  THEN (g.cantidad ) ELSE 0 END) '2024-10',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-11'  THEN (g.cantidad ) ELSE 0 END) '2024-11',
SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-12'  THEN (g.cantidad ) ELSE 0 END) '2024-12'
--INTO INDIGODWH.[INVENTORY].STG_ROTACION_POR_ALMACEN
FROM
(

       SELECT AL.Code ,AL.Name  ,pd.Code Código, PD.Name NombreProducto, pd.CodeCUM CUM, 
            (case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) NOMBTIPO,
            (case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END) CODMedica, (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END) NombMedica,
            PD.ProductCost AS COSTPROM, PD.FinalProductCost AS UCOMP,
            sum(IIF (K.MovementType =1, K.Quantity *-1, K.Quantity)) Cantidad,K.DocumentDate  
			FROM   inventory.Kardex AS K
            inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = k.ProductId
            JOIN Inventory.Warehouse AL WITH (nolock) on al.Id = k.WarehouseId
            LEFT join Inventory.ATC ATC WITH (nolock) on ATC.Id = PD.ATCId
            LEFT join Inventory.InventorySupplie Ins WITH (nolock) on ins.Id = PD.SupplieId
            WHERE  K.EntityName in ('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution') AND K.AffectInventory =1 AND YEAR(K.DocumentDate)>='2023'--AND PD.Code ='0201147' 
            group by AL.Code ,AL.Name  ,pd.Code , PD.Name , pd.CodeCUM, 
            (case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END),
            (case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END), (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END),
            PD.ProductCost, PD.FinalProductCost,K.DocumentDate
       UNION
           SELECT AL.Code ,AL.Name ,pd.Code Código, PD.Name NombreProducto, pd.CodeCUM CUM, 
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
           WHERE K.EntityName in ('TransferOrder') AND TOR.OrderType = 2 AND K.MovementType =2 AND K.AffectInventory =1 AND YEAR(K.DocumentDate)>='2023'--AND PD.Code ='0201147' --and AL.Code in ('044','047')
           group by AL.Code ,AL.Name  ,pd.Code , PD.Name , pd.CodeCUM, 
           (case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END),
           (case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END), (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END),
           PD.ProductCost, PD.FinalProductCost,K.DocumentDate
		UNION
           SELECT AL.Code ,AL.Name ,pd.Code Código, PD.Name NombreProducto, pd.CodeCUM CUM, 
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
		   LEFT JOIN Inventory .TransferOrderDevolution TOD ON TOD.TransferOrderId =TOR.Id 
           WHERE K.EntityName in ('TransferOrderDevolution') AND TOR.OrderType = 2 AND K.MovementType =1 AND K.AffectInventory =1 AND YEAR(K.DocumentDate)>='2023'--AND PD.Code ='0201147' --and AL.Code in ('044','047')
           group by AL.Code ,AL.Name  ,pd.Code , PD.Name , pd.CodeCUM, 
           (case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END),
           (case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END), (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END),
           PD.ProductCost, PD.FinalProductCost,K.DocumentDate
)  G
GROUP BY G.Code,G.Name ,G.Código ,G.NombreProducto ,G.CUM ,G.NOMBTIPO ,G.CODMedica ,G.NombMedica ,G.COSTPROM ,G.UCOMP
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que presenta la rotación mensual de productos (medicamentos, insumos y nutriciones) por almacén, pivotando las cantidades despachadas/devueltas mes a mes desde enero 2023 hasta diciembre 2024. Consolida tres tipos de movimientos del Kardex: dispensación farmacéutica, órdenes de transferencia salientes y sus devoluciones, incluyendo costo promedio, costo final, clasificación ATC o de insumo, y código CUM por producto.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un reporte pivotado de rotación mensual de productos (medicamentos, insumos y nutriciones) por almacén durante 2023 y 2024, consolidando salidas/entradas por dispensación farmacéutica, traslados y devoluciones de traslado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de inventario (Kardex, InventoryProduct, Warehouse, ATC, InventorySupplie, TransferOrder, TransferOrderDevolution) deben existir y estar pobladas con datos desde 2023 en adelante.; El campo K.AffectInventory debe estar correctamente marcado para distinguir movimientos contables de inventario.; K.EntityName debe contener los valores convenidos: ''PharmaceuticalDispensing'', ''PharmaceuticalDispensingDevolution'', ''TransferOrder'', ''TransferOrderDevolution''.; TransferOrder.OrderType = 2 identifica el tipo de traslado relevante para el reporte.; InventoryProduct.ProductTypeId usa los códigos 1=medicamento, 2=insumo, 6=nutrición.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera movimientos con K.AffectInventory = 1 (movimientos que efectivamente afectan inventario).; Solo considera movimientos con YEAR(K.DocumentDate) >= 2023.; En dispensaciones farmacéuticas, MovementType=1 se interpreta como salida (cantidad multiplicada por -1) y cualquier otro tipo como entrada (cantidad positiva).; Las columnas mensuales están fijas a los años 2023 y 2024; meses fuera de ese rango no se reportan aunque cumplan el filtro YEAR>=2023.; Las uniones a ATC e InventorySupplie son LEFT JOIN: un producto sin clasificación ATC/insumo aún se reporta.; El reporte agrupa por almacén (Warehouse) y producto, mostrando costo promedio (ProductCost) y último costo de compra (FinalProductCost).; Se usa UNION (no UNION ALL) entre los tres orígenes, eliminando duplicados exactos antes de pivotar.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Rotación de inventario; Almacén / bodega; Kardex; Dispensación farmacéutica; Devolución de dispensación; Orden de traslado entre almacenes; Devolución de traslado; Medicamento; Insumo médico; Nutrición; Clasificación ATC; Costo promedio del producto; Última unidad de compra (UCOMP); Código CUM', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.VIEW_ROTACION_POR_ALMACEN: Devuelve por almacén y producto las cantidades movidas pivotadas en 24 columnas mensuales (2023-1 a 2024-12), combinando dispensaciones, traslados tipo 2 y devoluciones de traslado tipo 2 con AffectInventory=1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.ProductTypeId = ''1'' → Clasifica el producto como ''MEDICAMENTO'' y toma código/nombre desde Inventory.ATC; si PD.ProductTypeId = ''2'' → Clasifica como ''INSUMOS'' y toma código/nombre desde Inventory.InventorySupplie (Code / SupplieName); si PD.ProductTypeId = ''6'' → Clasifica como ''NUTRICIONES'' (sin código/nombre de catálogo médico); si ProductTypeId distinto de 1, 2 o 6 → Etiqueta tipo como ''OTR0'' y deja CODMedica/NombMedica vacíos; si K.EntityName in (''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution'') → Suma cantidad invirtiendo el signo cuando MovementType=1 (salidas restan, entradas suman); si K.EntityName = ''TransferOrder'' AND TOR.OrderType = 2 AND K.MovementType = 2 → Incluye movimientos de traslado tipo 2 (salidas) como cantidad de rotación; si K.EntityName = ''TransferOrderDevolution'' AND TOR.OrderType = 2 AND K.MovementType = 1 → Incluye devoluciones de traslado tipo 2 (entradas) como cantidad de rotación; si CONCAT(YEAR(documentDate),''-'',MONTH(documentDate)) coincide con un mes específico de 2023 o 2024 → Pivotea la cantidad sumada en la columna correspondiente al mes-año (24 columnas: 2023-1..2024-12)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'inventory.Kardex; Inventory.InventoryProduct; Inventory.Warehouse; Inventory.ATC; Inventory.InventorySupplie; Inventory.TransferOrder; Inventory.TransferOrderDevolution', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ROTACION_POR_ALMACEN';
GO
