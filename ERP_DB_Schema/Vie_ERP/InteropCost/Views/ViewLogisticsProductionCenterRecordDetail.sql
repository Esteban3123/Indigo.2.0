CREATE VIEW [InteropCost].[ViewLogisticsProductionCenterRecordDetail]
AS
SELECT ROW_NUMBER() OVER (ORDER BY crd.Id) AS Id, cr.Id AS CenterRecordId, cr.Code, pc.Code + ' - ' + pc.Name AS ProductionCenter, mu.Code + ' - ' + mu.Name AS MeasurementUnit, crd.[Count] FROM [InteropCost].[LogisticsProductionCenterRecordDetail] AS crd
INNER JOIN [InteropCost].[LogisticsProductionCenterRecord] AS cr ON cr.Id = crd.LogisticsProductionCenterRecordId
INNER JOIN [Inventory].[InventoryMeasurementUnit] AS mu ON mu.Id = crd.InventoryMeasurementUnitId
INNER JOIN [InteropCost].[ProductionCenter] AS pc ON pc.Id = crd.ProductionCenterId