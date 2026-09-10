

CREATE view [Report].[UploadCubeVieSCMWareHouseRotation]
AS

SELECT 
	G.Code AS [CODIGO ALMACEN],-- [CodigoAlmacen],
	G.Name AS [ALMACEN],--[Almacen],
	G.Codigo AS [CODIGO PRODUCTO],--[CodigoProducto],
	G.NombreProducto AS [DESCRIPCION PRODUCTO],--[DescripcionProducto],
	G.CUM [CUM],
	G.NOMBTIPO AS [TIPO PRODUCTO],--[TipoProducto],
	G.CODMedica AS [CODIGO MEDICAMENTO INSUMO],--[CodigoMedicamentoInsumo],
	G.NombMedica AS [DESCRIPCION MEDICAMENTO INSUMO],--[DescripcionMedicamentoInsumo],
	G.COSTPROM AS [VALOR PROMEDIO],--[ValorPromedio],
	G.UCOMP AS [VALOR FINAL],--[ValorFinal],
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
	SUM(CASE WHEN CONCAT(YEAR(g.documentDate),'-',MONTH(g.documentDate)) = '2024-12'  THEN (g.cantidad ) ELSE 0 END) '2024-12',
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
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
		WHERE  K.EntityName in ('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution') AND K.AffectInventory =1 AND YEAR(K.DocumentDate)>='2023' --AND PD.Code ='0201147' 
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
		WHERE K.EntityName in ('TransferOrder') AND TOR.OrderType = 2 AND K.MovementType =2 AND K.AffectInventory =1 AND YEAR(K.DocumentDate)>='2023'--AND PD.Code ='0201147' --and AL.Code in ('044','047')
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
		LEFT JOIN Inventory .TransferOrderDevolution TOD ON TOD.TransferOrderId =TOR.Id 
		WHERE K.EntityName in ('TransferOrderDevolution') AND TOR.OrderType = 2 AND K.MovementType =1 AND K.AffectInventory =1 AND YEAR(K.DocumentDate)>='2023' --AND PD.Code ='0201147' --and AL.Code in ('044','047')
		group by AL.Code ,AL.Name  ,pd.Code , PD.Name , pd.CodeCUM, 
		(case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END),
		(case when PD.ProductTypeId = '1'THEN atc.Code when PD.ProductTypeId = '2'THEN ins.Code ELSE ''END), (case when PD.ProductTypeId = '1'THEN atc.Name when PD.ProductTypeId = '2'THEN ins.SupplieName ELSE ''END),
		PD.ProductCost, PD.FinalProductCost,K.DocumentDate
	) G
	GROUP BY G.Code,G.Name ,G.Codigo ,G.NombreProducto ,G.CUM ,G.NOMBTIPO ,G.CODMedica ,G.NombMedica ,G.COSTPROM ,G.UCOMP
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para cubo de datos que consolida la **rotación de inventario por almacén y producto** (medicamentos, insumos y nutrición) desde 2023, pivotando cantidades mes a mes para los años 2023 y 2024. Combina tres fuentes de movimiento del kardex: dispensaciones farmacéuticas (y sus devoluciones) y órdenes de traslado tipo 2 con sus devoluciones, ajustando el signo según el tipo de movimiento. Expone costo promedio, costo final y clasificación ATC o insumo por cada producto-almacén, orientada a alimentar un cubo analítico de consumo/rotación de inventario hospitalario.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la rotación mensual (2023-2024) de medicamentos, insumos y nutriciones por almacén, sumando salidas por dispensación, devoluciones y traslados a partir del kardex de inventario, para alimentar un cubo de reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Inventory.Kardex debe contener registros con DocumentDate de año >= 2023 y AffectInventory = 1 para ser considerados.; Los productos deben existir en Inventory.InventoryProduct (INNER JOIN por ProductId).; Los almacenes deben existir en Inventory.Warehouse (INNER JOIN por WarehouseId).; Para movimientos de tipo TransferOrder/TransferOrderDevolution, debe existir el registro padre en Inventory.TransferOrder con OrderType = 2.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran movimientos con AffectInventory = 1 (los que afectan el inventario real).; Solo se consideran documentos con año >= 2023.; Las salidas (MovementType=1) en dispensaciones y devoluciones de traslado se contabilizan con signo negativo; las entradas/otras (MovementType<>1) con signo positivo.; Los traslados sólo se incluyen cuando TransferOrder.OrderType = 2.; Los productos no clasificados como medicamento (1) o insumo (2) no exponen código ni nombre de medicamento/insumo (cadena vacía).; ULT_ACTUAL siempre se calcula con la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Almacén / Bodega; Producto de inventario; Medicamento; Insumo; Nutriciones; Clasificación ATC; CUM (Código Único de Medicamento); Kardex de inventario; Dispensación farmacéutica; Devolución de dispensación; Orden de traslado; Devolución de traslado; Costo promedio del producto; Costo final del producto; Rotación de inventario', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMWareHouseRotation: Devuelve por almacén y producto las cantidades movidas mensualmente entre 2023-1 y 2024-12, junto con costo promedio (ProductCost), costo final (FinalProductCost) y timestamp ULT_ACTUAL en zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si K.EntityName IN (''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution'') AND K.AffectInventory=1 → Se incluye el movimiento como dispensación farmacéutica o su devolución, signando la cantidad: si MovementType=1 se multiplica por -1, en caso contrario se mantiene positiva.; si K.EntityName=''TransferOrder'' AND TOR.OrderType=2 AND K.MovementType=2 AND K.AffectInventory=1 → Se incluye como salida por orden de traslado (OrderType=2), tomando la cantidad positiva.; si K.EntityName=''TransferOrderDevolution'' AND TOR.OrderType=2 AND K.MovementType=1 AND K.AffectInventory=1 → Se incluye como devolución de traslado, invirtiendo el signo de la cantidad (Quantity * -1).; si PD.ProductTypeId = ''1'' → Se clasifica como ''MEDICAMENTO'' y se toma código/nombre desde Inventory.ATC. else Si ProductTypeId=''2'' se clasifica como ''INSUMOS'' tomando datos de Inventory.InventorySupplie; si =''6'' se clasifica como ''NUTRICIONES''; en cualquier otro caso se etiqueta como ''OTR0'' con código y nombre vacíos.; si CONCAT(YEAR(documentDate),''-'',MONTH(documentDate)) coincide con cada par año-mes entre 2023-1 y 2024-12 → Se acumula la cantidad en la columna correspondiente al mes; en caso contrario aporta 0 a esa columna.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.InventoryProduct; Inventory.Warehouse; Inventory.ATC; Inventory.InventorySupplie; Inventory.TransferOrder; Inventory.TransferOrderDevolution', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMWareHouseRotation';
GO
