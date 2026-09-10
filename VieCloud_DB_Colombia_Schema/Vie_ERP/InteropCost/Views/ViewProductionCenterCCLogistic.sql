CREATE view [InteropCost].[ViewProductionCenterCCLogistic]
	as
select distinct(pc.Id), pc.Code, pc.Name, pc.Status, pc.CenterType from [InteropCost].[LogisticsProductionCenterRecordDetail] as lpc inner join
[InteropCost].[ProductionCenter] as pc on lpc.ProductionCenterId = pc.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centros de producción (centros de costo) que tienen al menos un registro de detalle logístico asociado. Combina la información de los centros de producción con sus registros de producción logística para filtrar únicamente aquellos centros que participan activamente en procesos logísticos. Expone el código, nombre, estado y tipo de centro, eliminando duplicados, y sirve como fuente de consulta para reportería de distribución y control de costos logísticos por centro de costo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewProductionCenterCCLogistic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewProductionCenterCCLogistic';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo distinto de centros de producción que tienen al menos un detalle de registro logístico asociado, para su uso en el módulo de costos/logística.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir filas en LogisticsProductionCenterRecordDetail referenciando ProductionCenterId válidos en ProductionCenter.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se excluyen centros de producción sin participación en registros logísticos (efecto del INNER JOIN).; No se duplican centros aunque tengan múltiples detalles logísticos (uso de DISTINCT).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Registro logístico', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] InteropCost.ProductionCenter: Devuelve únicamente centros de producción (Id, Code, Name, Status, CenterType) que aparecen al menos una vez como ProductionCenterId en LogisticsProductionCenterRecordDetail (INNER JOIN con DISTINCT).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.LogisticsProductionCenterRecordDetail; InteropCost.ProductionCenter', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterCCLogistic';
GO
