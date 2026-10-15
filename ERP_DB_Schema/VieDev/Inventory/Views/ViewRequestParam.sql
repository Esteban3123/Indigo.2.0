





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