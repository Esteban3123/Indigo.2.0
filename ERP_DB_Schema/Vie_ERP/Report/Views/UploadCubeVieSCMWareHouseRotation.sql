

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