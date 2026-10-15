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