

CREATE view [Report].[UploadCubeVieSCMConsolidatedInventoryRotation]
AS

SELECT 
		G.Codigo AS [CODIGO PRODUCTO],--[CodigoProducto],
		G.NombreProducto AS [DESCRIPCION PRODUCTO],--[DescripcionProducto],
		G.cum AS [CUM],
		G.NOMBTIPO AS [TIPO PRODUCTO],--[TipoProducto],
		G.CODMedica AS [CODIGO MEDICAMENTO PRODUCTO],--[CodigoMedicamentoProducto],
		G.NombMedica AS [DESCRIPCION MEDICAMENTO PRODUCTO],--[DescripcionMedicamentoProducto],
		CAST(G.COSTPROM AS NUMERIC) AS [VALOR PROMEDIO],--[ValorPromedio],
		CAST(G.UCOMP AS NUMERIC) AS [VALOR FINAL],--[ValorFinal],
		[2023-1],[2023-2],[2023-3],[2023-4],[2023-5],[2023-6],[2023-7],[2023-8],[2023-9],[2023-10],[2023-11],[2023-12],
		[2024-1],[2024-2],[2024-3],[2024-4],[2024-5],[2024-6],[2024-7],[2024-8],[2024-9],[2024-10],[2024-11],[2024-12],
		B.AL001 ,B.AL002 ,B.AL005 ,B.AL009,B.AL011, B.AL015 ,B.AL016 ,B.AL017 ,B.AL018 ,B.AL019 ,B.AL029 ,B.AL035 ,B.AL042 ,B.AL044 ,B.AL045 ,B.AL046 ,B.AL055 ,B.AL073 ,B.AL075 ,B.AL080 ,B.AL085, B.AL097, B.AL153, B.AL160, B.AL168,
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM
	(
		SELECT 
			Código Codigo, NombreProducto NombreProducto, CUM cum, NOMBTIPO NOMBTIPO, CODMedica CODMedica, NombMedica NombMedica, COSTPROM COSTPROM, UCOMP UCOMP, 
			SUM([2023-1]) [2023-1],SUM([2023-2]) [2023-2],SUM([2023-3]) [2023-3],SUM([2023-4]) [2023-4],SUM([2023-5]) [2023-5], SUM([2023-6]) [2023-6],SUM([2023-7]) [2023-7], SUM([2023-8]) [2023-8],SUM([2023-9]) [2023-9], SUM([2023-10]) [2023-10], SUM([2023-11]) [2023-11], SUM([2023-12]) [2023-12],
			SUM([2024-1]) [2024-1],SUM([2024-2]) [2024-2],SUM([2024-3]) [2024-3],SUM([2024-4]) [2024-4],SUM([2024-5]) [2024-5], SUM([2024-6]) [2024-6],SUM([2024-7]) [2024-7], SUM([2024-8]) [2024-8],SUM([2024-9]) [2024-9], SUM([2024-10]) [2024-10], SUM([2024-11]) [2024-11], SUM([2024-12]) [2024-12]	
		FROM REPORT.VIEW_ROTACION_POR_ALMACEN as rot
		JOIN Inventory.InventoryProduct as pro  on rot.Código=pro.Code 
		GROUP BY Código, NombreProducto, CUM, NOMBTIPO, CODMedica, NombMedica, COSTPROM, UCOMP
	) AS G
	LEFT JOIN
	(
		SELECT  
			pr.Code CodigoProducto,      
			SUM(case when pii.WarehouseId = '1' then pii.Quantity else 0 end) AL001,
			SUM(case when pii.WarehouseId = '2' then pii.Quantity else 0 end) AL002, SUM(case when pii.WarehouseId = '5' then pii.Quantity else 0 end) AL005,
			SUM(case when pii.WarehouseId = '9' then pii.Quantity else 0 end) AL009, SUM(case when pii.WarehouseId = '11' then pii.Quantity else 0 end) AL011,
			SUM(case when pii.WarehouseId = '15' then pii.Quantity else 0 end) AL015, SUM(case when pii.WarehouseId = '16' then pii.Quantity else 0 end) AL016,
			SUM(case when pii.WarehouseId = '17' then pii.Quantity else 0 end) AL017, SUM(case when pii.WarehouseId = '18' then pii.Quantity else 0 end) AL018,
			SUM(case when pii.WarehouseId = '19' then pii.Quantity else 0 end) AL019, SUM(case when pii.WarehouseId = '29' then pii.Quantity else 0 end) AL029,
			SUM(case when pii.WarehouseId = '35' then pii.Quantity else 0 end) AL035, SUM(case when pii.WarehouseId = '42' then pii.Quantity else 0 end) AL042,
			SUM(case when pii.WarehouseId = '44' then pii.Quantity else 0 end) AL044, SUM(case when pii.WarehouseId = '45' then pii.Quantity else 0 end) AL045,
			SUM(case when pii.WarehouseId = '46' then pii.Quantity else 0 end) AL046, SUM(case when pii.WarehouseId = '55' then pii.Quantity else 0 end) AL055,
			SUM(case when pii.WarehouseId = '73' then pii.Quantity else 0 end) AL073, SUM(case when pii.WarehouseId = '80' then pii.Quantity else 0 end) AL080,
			SUM(case when pii.WarehouseId = '85' then pii.Quantity else 0 end) AL085, SUM(case when pii.WarehouseId = '99' then pii.Quantity else 0 end) AL075,
			SUM(case when pii.WarehouseId = '106' then pii.Quantity else 0 end) AL097,
			SUM(CASE WHEN pii.WarehouseId = '167' THEN pii.Quantity ELSE 0 END) AL153,
			SUM(CASE WHEN pii.WarehouseId = '174' THEN pii.Quantity ELSE 0 END) AL160,
			SUM(CASE WHEN pii.WarehouseId = '182' THEN pii.Quantity ELSE 0 END) AL168
		FROM Inventory .PhysicalInventory as pii
		join Inventory .InventoryProduct as pr on pii.ProductId =pr.Id 
		join Inventory .Warehouse as wh on pii.WarehouseId =wh.Id
		WHERE pii.WarehouseId in ('1', '2', '5', '9', '11','15','16','17','18','19','29','35','42','44','45','46','55','73','80','85','99','106', '167', '174', '182')
		GROUP BY pr.Code 
	) AS B ON B.CodigoProducto = G.Codigo
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista destinada a alimentar un cubo de análisis (OLAP/reporting) con la rotación consolidada de inventario por producto. Combina la rotación mensual de 2023 y 2024 —obtenida desde la vista de rotación por almacén— con el inventario físico actual de cada producto desglosado por hasta 25 almacenes específicos. Incluye atributos maestros del producto como CUM, tipo, código de medicamento, costo promedio y unidades, junto con la marca de última actualización.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la rotación mensual de productos (2023-2024) con su existencia actual pivotada por almacén para alimentar un cubo de inventario.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'REPORT.VIEW_ROTACION_POR_ALMACEN debe exponer columnas Código, NombreProducto, CUM, NOMBTIPO, CODMedica, NombMedica, COSTPROM, UCOMP y columnas mensuales [2023-1]..[2024-12]; Inventory.InventoryProduct.Code debe corresponder al Código de la vista de rotación para enlazar el maestro; Inventory.PhysicalInventory debe tener WarehouseId entre los IDs fijos listados para que aparezcan cantidades; El servidor debe soportar AT TIME ZONE con la zona ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La rotación mensual se reporta para los meses de 2023-1 a 2024-12 agregada por SUM por producto; Solo se consideran existencias de los almacenes en la lista fija (1,2,5,9,11,15,16,17,18,19,29,35,42,44,45,46,55,73,80,85,99,106,167,174,182); cualquier otro almacén se excluye; El mapeo de almacén a columna es fijo: WarehouseId 99 se reporta como AL075 y WarehouseId 106 como AL097 (no es relación 1:1 por número); Los productos de la rotación se cruzan con InventoryProduct por Code (INNER JOIN), por lo que productos sin maestro no aparecen; Las existencias por almacén se enlazan vía LEFT JOIN; productos sin inventario físico en los almacenes listados muestran NULL en columnas AL; ULT_ACTUAL se calcula como GETDATE() convertido a zona horaria ''Pakistan Standard Time''; VALOR PROMEDIO y VALOR FINAL se entregan como NUMERIC (truncamiento de decimales por CAST sin precisión/escala)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Medicamento; CUM; Inventario físico; Almacén/Bodega; Rotación de inventario; Costo promedio; Última compra', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMConsolidatedInventoryRotation: Devuelve un registro por producto con datos maestros, valores monetarios, rotación mensual sumada (2023-1..2024-12), existencias por almacén pivotadas (AL001..AL168) y timestamp ULT_ACTUAL en zona Pakistan Standard Time', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pii.WarehouseId = ''<id>'' (1,2,5,9,11,15,16,17,18,19,29,35,42,44,45,46,55,73,80,85,99,106,167,174,182) → Suma pii.Quantity en la columna AL correspondiente (pivote por almacén); en otro caso suma 0', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'REPORT.VIEW_ROTACION_POR_ALMACEN; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMConsolidatedInventoryRotation';
GO
