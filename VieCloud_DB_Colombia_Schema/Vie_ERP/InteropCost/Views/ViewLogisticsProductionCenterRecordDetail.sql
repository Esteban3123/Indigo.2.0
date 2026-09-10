CREATE VIEW [InteropCost].[ViewLogisticsProductionCenterRecordDetail]
AS
SELECT ROW_NUMBER() OVER (ORDER BY crd.Id) AS Id, cr.Id AS CenterRecordId, cr.Code, pc.Code + ' - ' + pc.Name AS ProductionCenter, mu.Code + ' - ' + mu.Name AS MeasurementUnit, crd.[Count] FROM [InteropCost].[LogisticsProductionCenterRecordDetail] AS crd
INNER JOIN [InteropCost].[LogisticsProductionCenterRecord] AS cr ON cr.Id = crd.LogisticsProductionCenterRecordId
INNER JOIN [Inventory].[InventoryMeasurementUnit] AS mu ON mu.Id = crd.InventoryMeasurementUnitId
INNER JOIN [InteropCost].[ProductionCenter] AS pc ON pc.Id = crd.ProductionCenterId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de los registros de producción logística por centro de producción, combinando la información del acta o registro logístico, el centro de producción al que pertenece cada ítem y la unidad de medida de inventario utilizada. Para cada línea de detalle muestra el código y nombre del centro de producción, la unidad de medida (por ejemplo: unidad, caja, frasco) y la cantidad de unidades registradas. Sirve como fuente de consulta y reportería para auditar o revisar cuántas unidades de insumos o medicamentos fueron asignadas a cada centro de costo o unidad productiva dentro de un acta logística de despacho, preparación o alistamiento.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewLogisticsProductionCenterRecordDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewLogisticsProductionCenterRecordDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de los registros logísticos por centro de producción mostrando la cantidad asignada junto con el centro y la unidad de medida descritos por código y nombre.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewLogisticsProductionCenterRecordDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle debe tener referencias válidas a un registro logístico, una unidad de medida de inventario y un centro de producción (los INNER JOIN excluyen registros con FK nulas o huérfanas).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewLogisticsProductionCenterRecordDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles cuyo registro logístico, unidad de medida y centro de producción existan (filtro implícito por INNER JOIN).; El Id de salida no corresponde al Id físico del detalle, sino a un consecutivo calculado por ROW_NUMBER ordenado por crd.Id.; Las descripciones de centro de producción y unidad de medida siempre se presentan en formato ''Código - Nombre''.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewLogisticsProductionCenterRecordDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Registro logístico de producción; Unidad de medida de inventario; Cantidad asignada', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewLogisticsProductionCenterRecordDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] InteropCost.LogisticsProductionCenterRecordDetail: Devuelve un Id secuencial generado con ROW_NUMBER() OVER (ORDER BY crd.Id), junto con el Id y Code del registro logístico padre, la cantidad y descripciones concatenadas ''Code - Name'' del centro de producción y de la unidad de medida.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewLogisticsProductionCenterRecordDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.LogisticsProductionCenterRecordDetail; InteropCost.LogisticsProductionCenterRecord; Inventory.InventoryMeasurementUnit; InteropCost.ProductionCenter', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewLogisticsProductionCenterRecordDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewLogisticsProductionCenterRecordDetail';
GO
