CREATE VIEW [Cost].[ViewCostReportDistributionDirectCostDetail]
AS
	SELECT	cddcd.Id,
			cddcd.DistributionDirectCostId,
			CONCAT(cpc.Code, ' - ', cpc.Name) ProductionCenterCodeName,
			cpc.CenterType as CenterType,
			CASE cpc.CenterType
				WHEN 1 THEN 'Operativo'
				WHEN 2 THEN 'Administrativo'
				WHEN 3 THEN 'Logísitico'
			END CenterTypeName,
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			CONCAT(imu.Code, ' - ', imu.Name) MeasurementUnitCodeName,
			cddcd.Value
	FROM Cost.CostDistributionDirectCostDetail cddcd
	JOIN Cost.CostProductionCenter cpc on cddcd.ProductionCenterId = cpc.Id
	JOIN GeneralLedger.MainAccounts ma ON cddcd.MainAccountId = ma.Id
	LEFT JOIN Payroll.CostCenter cc ON cddcd.CostCenterId = cc.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu ON cddcd.MeasurementUnitId = imu.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de la distribución de costos directos para reportería del módulo de costos. Combina cada línea de reparto de costos directos con su centro de producción (código, nombre y tipo: operativo, administrativo o logístico), la cuenta contable del plan general, el centro de costo de nómina y la unidad de medida del inventario asociada. Se usa para analizar cómo se distribuyen los costos directos por área, cuenta y unidad de medida, presentando los campos descriptivos en formato legible (código - nombre) junto con el valor monetario de cada línea de distribución.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostReportDistributionDirectCostDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostReportDistributionDirectCostDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de la distribución de costos directos enriquecido con descripciones legibles de centro de producción, cuenta contable, centro de costo y unidad de medida, incluyendo la clasificación del tipo de centro.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCostDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle debe tener un centro de producción válido en Cost.CostProductionCenter; Cada detalle debe tener una cuenta contable válida en GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCostDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los tipos de centro de producción se restringen a tres categorías: Operativo (1), Administrativo (2) y Logístico (3); La relación con centro de producción y cuenta contable es obligatoria (INNER JOIN); La relación con centro de costo de nómina y unidad de medida es opcional (LEFT JOIN); Los campos descriptivos se presentan siempre como ''Código - Nombre''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCostDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos directos; Centro de producción; Centro de costo; Cuenta contable (Main Account); Unidad de medida; Tipo de centro (Operativo/Administrativo/Logístico)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCostDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.CostDistributionDirectCostDetail: Devuelve un registro por cada detalle de distribución de costo directo, concatenando código y nombre de los catálogos relacionados', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCostDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CenterType = 1 → Se etiqueta como ''Operativo''; si CenterType = 2 → Se etiqueta como ''Administrativo''; si CenterType = 3 → Se etiqueta como ''Logísitico'' (sic)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCostDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionDirectCostDetail; Cost.CostProductionCenter; GeneralLedger.MainAccounts; Payroll.CostCenter; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCostDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCostDetail';
GO
