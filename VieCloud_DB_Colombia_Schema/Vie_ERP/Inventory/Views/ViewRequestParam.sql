

CREATE VIEW [Inventory].[ViewRequestParam] AS

WITH cte AS 
(
	SELECT DISTINCT
	rp.Frecuency,
	rp.MeasuryUnitTime,
	rp.Id IdRequestParam,
	IIF(rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1), 1 ,IIF(NOT EXISTS(SELECT TOP 1 Days FROM Inventory.RequestParamDetailPeriodicity rpdp WHERE Days = '1' AND rp.Id = rpdp.IdRequestParam), 0, 1)) Monday,
	IIF(rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1), 1 ,IIF(NOT EXISTS(SELECT TOP 1 Days FROM Inventory.RequestParamDetailPeriodicity rpdp WHERE Days = '2' AND rp.Id = rpdp.IdRequestParam), 0, 1)) Tuesday,
	IIF(rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1), 1 ,IIF(NOT EXISTS(SELECT TOP 1 Days FROM Inventory.RequestParamDetailPeriodicity rpdp WHERE Days = '3' AND rp.Id = rpdp.IdRequestParam), 0, 1)) Wednesday,
	IIF(rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1), 1 ,IIF(NOT EXISTS(SELECT TOP 1 Days FROM Inventory.RequestParamDetailPeriodicity rpdp WHERE Days = '4' AND rp.Id = rpdp.IdRequestParam), 0, 1)) Thursday,
	IIF(rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1), 1 ,IIF(NOT EXISTS(SELECT TOP 1 Days FROM Inventory.RequestParamDetailPeriodicity rpdp WHERE Days = '5' AND rp.Id = rpdp.IdRequestParam), 0, 1)) Friday,
	IIF(rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1), 1 ,IIF(NOT EXISTS(SELECT TOP 1 Days FROM Inventory.RequestParamDetailPeriodicity rpdp WHERE Days = '6' AND rp.Id = rpdp.IdRequestParam), 0, 1)) Saturday,
	IIF(rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1), 1 ,IIF(NOT EXISTS(SELECT TOP 1 Days FROM Inventory.RequestParamDetailPeriodicity rpdp WHERE Days = '7' AND rp.Id = rpdp.IdRequestParam), 0, 1)) Sunday,
	IIF(rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1), 1 ,IIF(NOT EXISTS(SELECT TOP 1 Days FROM Inventory.RequestParamDetailPeriodicity rpdp WHERE Days = '8' AND rp.Id = rpdp.IdRequestParam), 0, 1)) Holiday
	FROM Inventory.RequestParam rp 
	LEFT JOIN  Inventory.RequestParamDetailPeriodicity rpdp ON rp.Id = rpdp.IdRequestParam

)

	SELECT 
	CONCAT(rp.Id, '-', rpfu.FunctionalUnitId, '-', rpp.Type, '-', ISNULL(rpp.SupplieId, rpp.ProductId)) ViewKey,
	rpfu.FunctionalUnitId,
	NULL WarehouseId,
	cte.Monday,
	cte.Tuesday,
	cte.Wednesday,
	cte.Thursday,
	cte.Friday,
	cte.Saturday,
	cte.Sunday,
	cte.Holiday,
	ISNULL(rpp.Type, 0) Type,
	rpp.SupplieId,
	rpp.ProductId,
	ISNULL(rpp.Quantity, 0) Quantity,
	CASE cte.MeasuryUnitTime
			WHEN 1 --Dias
			THEN ISNULL((SELECT TOP 1 
					IIF(DATEDIFF( DAY,(DATEADD(DAY, cte.Frecuency, ir.ConfirmationDate)), Common.GETDATE()) >= 0, 1 , 0) 
					FROM Inventory.InventoryRequest ir WHERE rpfu.FunctionalUnitId = ir.TargetFunctionalUnitId
					ORDER BY ir.ConfirmationDate DESC),1)
			WHEN 2 --Semanas
			THEN ISNULL((SELECT TOP 1 
					IIF(DATEDIFF( WEEK,(DATEADD(WEEK, cte.Frecuency, ir.ConfirmationDate)), rp.initialDate ) >= 0, 1,0) 
					FROM Inventory.InventoryRequest ir WHERE rpfu.FunctionalUnitId = ir.TargetFunctionalUnitId
					ORDER BY ir.ConfirmationDate DESC),1)
			WHEN 3 --Meses
			THEN ISNULL((SELECT TOP 1 
					IIF(DATEDIFF( MONTH,(DATEADD(MONTH, cte.Frecuency, ir.ConfirmationDate)), rp.initialDate) >= 0, 1,0) 
					FROM Inventory.InventoryRequest ir WHERE rpfu.FunctionalUnitId = ir.TargetFunctionalUnitId
					ORDER BY ir.ConfirmationDate DESC),1)
			WHEN 4--Años
			THEN ISNULL((SELECT TOP 1 
					IIF(DATEDIFF( YEAR,(DATEADD(YEAR, cte.Frecuency, ir.ConfirmationDate)), rp.initialDate) >= 0, 1,0) 
					FROM Inventory.InventoryRequest ir WHERE rpfu.FunctionalUnitId = ir.TargetFunctionalUnitId
					ORDER BY ir.ConfirmationDate DESC),1)
			ELSE 1
	END Aproved,
	rp.Frecuency,
	rp.MeasuryUnitTime,
	CASE rp.MeasuryUnitTime
			WHEN 1 THEN (IIF(rp.Frecuency > 1, 'dias', 'dia'))
			WHEN 2 THEN (IIF(rp.Frecuency > 1, 'semanas', 'semana'))
			WHEN 3 THEN (IIF(rp.Frecuency > 1, 'meses', 'mes'))
			WHEN 4 THEN (IIF(rp.Frecuency > 1, 'años', 'año'))
	END UnitTime
FROM Inventory.RequestParam rp
LEFT JOIN cte ON cte.IdRequestParam = rp.Id
LEFT JOIN Inventory.RequestParamFuncionalUnit rpfu ON rp.Id = rpfu.RequestParamId
LEFT JOIN Inventory.RequestParamProduct rpp ON rp.Id = rpp.RequestParamId
WHERE rp.State = 1

UNION ALL

	SELECT
	CONCAT(rp.Id, '-', rpw.WarehouseId, '-', rpp.Type, '-', ISNULL(rpp.SupplieId, rpp.ProductId)) ViewKey,
	NULL FunctionalUnitId,
	rpw.WarehouseId,
	cte.Monday,
	cte.Tuesday,
	cte.Wednesday,
	cte.Thursday,
	cte.Friday,
	cte.Saturday,
	cte.Sunday,
	cte.Holiday,
	ISNULL(rpp.Type, 0) Type,
	rpp.SupplieId,
	rpp.ProductId,
	ISNULL(rpp.Quantity, 0) Quantity,
	CASE cte.MeasuryUnitTime
			WHEN 1 --Dias
			THEN ISNULL((SELECT TOP 1 
					IIF(DATEDIFF( DAY,(DATEADD(DAY, cte.Frecuency, ir.ConfirmationDate)), Common.GETDATE()) >= 0, 1,0)
					FROM Inventory.InventoryRequest ir WHERE rpw.WarehouseId = ir.TargetWarehouseId
					ORDER BY ir.ConfirmationDate DESC),1)
			WHEN 2 --Semanas
			THEN ISNULL((SELECT TOP 1 
					IIF(DATEDIFF( WEEK,(DATEADD(WEEK, cte.Frecuency, ir.ConfirmationDate)), rp.initialDate ) >= 0, 1,0)
					FROM Inventory.InventoryRequest ir WHERE rpw.WarehouseId = ir.TargetWarehouseId
					ORDER BY ir.ConfirmationDate DESC),1)
			WHEN 3 --Meses
			THEN ISNULL((SELECT TOP 1 
					IIF(DATEDIFF( MONTH,(DATEADD(MONTH, cte.Frecuency, ir.ConfirmationDate)), rp.initialDate) >= 0, 1,0)
					FROM Inventory.InventoryRequest ir WHERE rpw.WarehouseId = ir.TargetWarehouseId
					ORDER BY ir.ConfirmationDate DESC),1)
			WHEN 4 --Años 
			THEN ISNULL((SELECT TOP 1 
					IIF(DATEDIFF( YEAR,(DATEADD(YEAR, cte.Frecuency, ir.ConfirmationDate)), rp.initialDate) >= 0, 1,0)
					FROM Inventory.InventoryRequest ir WHERE rpw.WarehouseId = ir.TargetWarehouseId
					ORDER BY ir.ConfirmationDate DESC),1)
			ELSE 1
	END Aproved,
	rp.Frecuency,
	rp.MeasuryUnitTime,
	CASE rp.MeasuryUnitTime
			WHEN 1 THEN (IIF(rp.Frecuency > 1, 'dias', 'dia'))
			WHEN 2 THEN (IIF(rp.Frecuency > 1, 'semanas', 'semana'))
			WHEN 3 THEN (IIF(rp.Frecuency > 1, 'meses', 'mes'))
			WHEN 4 THEN (IIF(rp.Frecuency > 1, 'años', 'año'))
	END UnitTime
FROM Inventory.RequestParam rp
LEFT JOIN cte ON cte.IdRequestParam = rp.Id
LEFT JOIN Inventory.RequestParamWarehouse rpw on rpw.RequestParamId = rp.Id
LEFT JOIN Inventory.RequestParamProduct rpp ON rp.Id = rpp.RequestParamId
WHERE rp.State = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los parámetros de solicitud de inventario activos, mostrando para cada combinación de producto o insumo con unidad funcional o bodega destino: los días de la semana habilitados para solicitar (lunes a domingo y festivos), la periodicidad configurada (días, semanas, meses o años), la cantidad requerida y si ya es momento de generar una nueva solicitud según el último pedido confirmado. Integra las tablas de configuración de parámetros (RequestParam), periodicidad (RequestParamDetailPeriodicity), unidades funcionales autorizadas (RequestParamFuncionalUnit), productos o insumos del pedido (RequestParamProduct) y el historial de solicitudes de inventario (InventoryRequest) para determinar si la solicitud está aprobada o vigente (campo Aproved). Se usa en la gestión de abastecimiento para determinar automáticamente cuándo y qué artículos deben pedirse desde cada servicio o bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRequestParam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRequestParam';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la configuración vigente de parámetros de solicitud de inventario por unidad funcional y por bodega, exponiendo días habilitados, productos/insumos asociados y si corresponde aprobar una nueva solicitud según la frecuencia configurada vs la última solicitud confirmada.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Solo se consideran parámetros de solicitud cuyo State = 1 (activos).; La evaluación de ''Aproved'' requiere existencia de solicitudes previas en InventoryRequest contra la misma unidad funcional o bodega; si no hay, asume 1 (aprobado).; Para que la periodicidad por días se considere específica, debe existir frecuencia distinta de 0 y unidad de medida distinta de 1 día semanal completo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen parámetros activos (State = 1).; Cada fila corresponde exclusivamente a una unidad funcional (con WarehouseId NULL) o a una bodega (con FunctionalUnitId NULL), nunca a ambos.; Cuando la frecuencia es 0 o es 1 día, todos los días de la semana y festivo quedan habilitados automáticamente.; El identificador de fila ViewKey se compone como Id-UnidadOBodega-Tipo-(SupplieId o ProductId).; Si rpp.SupplieId es NULL se usa ProductId como discriminador en ViewKey.; Cantidad y Tipo nulos se normalizan a 0 mediante ISNULL.; La evaluación de aprobación toma siempre la última solicitud por ConfirmationDate descendente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Parámetros de solicitud de inventario; Periodicidad semanal y festivos; Frecuencia y unidad de medida de tiempo (días/semanas/meses/años); Unidad funcional; Bodega/almacén; Productos e insumos; Solicitud de inventario y fecha de confirmación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN_RESULT: Devuelve una fila por combinación de parámetro-unidad funcional-producto y otra por parámetro-bodega-producto (UNION ALL), filtrando rp.State = 1.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rp.Frecuency = 0 OR (rp.Frecuency = 1 AND rp.MeasuryUnitTime = 1) → Marca todos los días (Lunes a Domingo y Festivo) como habilitados (=1) sin consultar la tabla de periodicidad.; si Existe registro en RequestParamDetailPeriodicity con Days = N (1..8) para el parámetro → El día correspondiente (Monday..Sunday/Holiday) se marca 1, en caso contrario 0.; si cte.MeasuryUnitTime = 1 (Días) → Aproved = 1 si han transcurrido al menos ''Frecuency'' días desde la última ConfirmationDate hasta Common.GETDATE(); si no hay solicitud previa, 1.; si cte.MeasuryUnitTime = 2 (Semanas) → Aproved = 1 si DATEDIFF en semanas entre (ConfirmationDate + Frecuency semanas) y rp.initialDate ≥ 0; si no hay solicitud previa, 1.; si cte.MeasuryUnitTime = 3 (Meses) → Aproved = 1 si DATEDIFF en meses entre (ConfirmationDate + Frecuency meses) y rp.initialDate ≥ 0; si no hay solicitud previa, 1.; si cte.MeasuryUnitTime = 4 (Años) → Aproved = 1 si DATEDIFF en años entre (ConfirmationDate + Frecuency años) y rp.initialDate ≥ 0; si no hay solicitud previa, 1.; si cte.MeasuryUnitTime no coincide con 1..4 → Aproved = 1 por defecto.; si rp.Frecuency > 1 → UnitTime se expresa en plural (dias/semanas/meses/años); en caso contrario en singular.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.RequestParam; Inventory.RequestParamDetailPeriodicity; Inventory.RequestParamFuncionalUnit; Inventory.RequestParamProduct; Inventory.RequestParamWarehouse; Inventory.InventoryRequest', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParam';
GO
