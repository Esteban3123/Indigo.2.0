

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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a reporting y carga de cubos analíticos que aplana el detalle completo de costos por actividad clínica (asociada a un código CUPS). Consolida, para cada actividad activa, sus pasos, tiempos de mano de obra por cargo, insumos de inventario con su costo unitario de grupo y costo total, activos fijos con horas de uso, y el centro de producción responsable. Incluye identificador de compañía derivado del nombre de la base de datos y marca de última actualización en zona horaria estándar de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una vista plana el detalle de costeo por actividad clínica/administrativa, integrando pasos, nómina, centros de producción, insumos, activos fijos y CUPS para alimentar un cubo financiero analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las actividades de costo deben tener Status = 1 (activas) para ser incluidas.; Para calcular COSTO_GRUPO_PRODUCTO se requiere que existan registros en CostInventoryGroupDetail asociados al grupo de inventario.; Se asume que la base de datos actual (DB_NAME()) corresponde a la empresa cuyo identificador se reporta.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'EMPRESA siempre se reporta con el literal ''ODO''.; ID_COMPANY se obtiene dinámicamente del nombre de la base de datos en ejecución, truncado a 9 caracteres.; FECHA BUSQUEDA siempre corresponde a la fecha actual del servidor (GETDATE()).; ULT_ACTUAL siempre se entrega convertido a la zona horaria ''Pakistan Standard Time''.; COSTO_TOTAL_GRUPO se calcula siempre como Quantity del inventario del paso multiplicada por el costo agregado del grupo de productos.; COSTO_GRUPO_PRODUCTO se calcula como la suma de (Quantity * FinalProductCost) de los productos detallados del grupo.; Solo las actividades de costo activas alimentan el cubo; pasos, nómina, insumos y activos fijos pueden estar ausentes (LEFT JOIN) sin excluir la actividad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Actividad de costo; CUPS (Clasificación Única de Procedimientos en Salud); Centro de producción / centro de costo; Paso de actividad de costeo; Nómina y cargo (Position); Grupo de inventario / insumos; Unidad de medida de inventario; Activo fijo; Costo del producto final (FinalProductCost); Costo total por grupo de productos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieFinanceDetailedCostActivities: Devuelve una fila por combinación de actividad activa (Status=1), paso, asignación de nómina, centro de producción, insumo y activo fijo, con costos calculados y marcas temporales.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.Status = 1 → Se incluye la actividad de costo en el resultado else La actividad queda excluida del cubo financiero', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostActivity; Cost.CostActivityStep; Cost.CostActivityStepPayroll; Cost.CostActivityProductionCenter; Cost.CostProductionCenter; Payroll.Position; Cost.CostActivityStepInventory; Cost.CostInventoryGroup; Inventory.InventoryMeasurementUnit; Cost.CostActivityStepFixedAsset; FixedAsset.FixedAssetItem; Contract.CUPSEntity; Cost.CostInventoryGroupDetail; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceDetailedCostActivities';
GO
