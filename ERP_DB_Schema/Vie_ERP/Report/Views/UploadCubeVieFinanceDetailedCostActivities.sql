

--DECLARE @CENTRO_PRODUCCION VARCHAR(50) = 'QUIMIOTERAPIA'

CREATE view [Report].[UploadCubeVieFinanceDetailedCostActivities] as

SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	'ODO' AS EMPRESA,
	C.[Code] AS CODIGO_ACTIVIDAD,
	C.[Name] AS DESCRIPCION_ACTIVIDAD,
	CUPS.Code AS CUPS,
	C.[Description] AS DESCRIPCION_CUPS,
	S.[Order] AS TAREA,
	S.Description AS DESCRIPCION_TAREA,
	T.Hours AS [TIEMPO (HORAS)],
	CPD.Code AS CODIGO_CENTRO_PRODUCCION,
	CPD.Name AS CENTRO_PRODUCCION,
	N.Name AS RESPONSABLE,
	INVD.Code AS CODIGO_GRUPO_PRODUCTO,
	INVD.Name AS GRUPO_PRODUCTO,
	INVD.Description AS DESCRIPCION_GRUPO_PRODUCTO,
	UINV.NAME AS UNIDAD_MEDIDA,
	UINV.Abbreviation AS ABREVIACION_UNIDAD,
	INV.Quantity AS CANTIDAD_INVENTARIO,
	CGP.COSTO_GRUPO_PRODUCTO,
	(INV.Quantity * CGP.COSTO_GRUPO_PRODUCTO) AS COSTO_TOTAL_GRUPO,
	FAD.Description AS ACTIVO_FIJO,
	FA.Hours AS TIEMPO_ACTIVO_FIJO,
	CAST(GETDATE() AS DATE) AS [FECHA BUSQUEDA],
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM [Cost].[CostActivity] AS C
	LEFT JOIN [Cost].[CostActivityStep] AS S ON S.CostActivityId = C.Id
	LEFT JOIN [Cost].[CostActivityStepPayroll] AS T ON T.CostActivityStepId = S.Id
	LEFT JOIN [Cost].[CostActivityProductionCenter] AS CP ON CP.CostActivityId = C.Id
	LEFT JOIN [Cost].[CostProductionCenter] AS CPD ON CPD.Id = CP.CostProductionCenterId
	LEFT JOIN [Payroll].[Position] AS N ON N.Id = T.PayrollPositionId
	LEFT JOIN [Cost].[CostActivityStepInventory] AS INV ON INV.CostActivityStepId = S.Id
	LEFT JOIN [Cost].[CostInventoryGroup] AS INVD ON INVD.Id = INV.CostInventoryGroupId
	LEFT JOIN [Inventory].[InventoryMeasurementUnit] AS UINV ON UINV.ID = INVD.InventoryMeasurementUnitId
	LEFT JOIN [Cost].[CostActivityStepFixedAsset] AS FA ON FA.CostActivityStepId = S.Id
	LEFT JOIN [FixedAsset].[FixedAssetItem] AS FAD ON FAD.Id = FA.FixedAssetItemId
	LEFT JOIN Contract.CUPSEntity AS CUPS ON CUPS.Id = C.[CUPSEntityId]
	LEFT JOIN (SELECT CIGD.CostInventoryGroupId, SUM((CIGD.Quantity * IP.FinalProductCost)) as COSTO_GRUPO_PRODUCTO 
				FROM [Cost].[CostInventoryGroupDetail] AS CIGD
					LEFT JOIN Inventory.InventoryProduct AS IP ON IP.Id = CIGD.InventoryProductId
				GROUP BY CIGD.CostInventoryGroupId) AS CGP ON CGP.CostInventoryGroupId = INVD.Id
WHERE C.Status = 1


--WHERE CENTRO_PRODUCCION LIKE '%'+@CENTRO_PRODUCCION+'%'

--SELECT Name FROM [Cost].[CostProductionCenter]