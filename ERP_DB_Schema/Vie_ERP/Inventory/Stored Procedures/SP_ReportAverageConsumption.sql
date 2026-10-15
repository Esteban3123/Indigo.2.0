CREATE PROCEDURE [Inventory].[SP_ReportAverageConsumption]
	@DateStart DATE,
	@DateEnd DATE
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		CONCAT(tro.ProductCode, ' - ', tro.ProductName) [PRODUCTO],
		CONCAT(tro.MeasurementUnitCode, ' - ', tro.MeasurementUnitName) [UNIDAD],
		tro.Concentration [CONCENTRACION],
		IIF(tro.DispatchTo = 1, 'ALMACEN', 'UNIDAD FUNCIONAL') [TIPO DE DESTINO],
		CONCAT(tro.WarehouseSourceCode, ' - ', tro.WarehouseSourceName) [ORIGEN],
		CONCAT(tro.TargetCode, ' - ', tro.TargetName) [DESTINO],
		tro.Year [AÑO],
		SUM(IIF(tro.Month = 1, tro.QuantityTotal, 0)) [ENE],
		SUM(IIF(tro.Month = 2, tro.QuantityTotal, 0)) [FEB],
		SUM(IIF(tro.Month = 3, tro.QuantityTotal, 0)) [MAR],
		SUM(IIF(tro.Month = 4, tro.QuantityTotal, 0)) [ABR],
		SUM(IIF(tro.Month = 5, tro.QuantityTotal, 0)) [MAY],
		SUM(IIF(tro.Month = 6, tro.QuantityTotal, 0)) [JUN],
		SUM(IIF(tro.Month = 7, tro.QuantityTotal, 0)) [JUL],
		SUM(IIF(tro.Month = 8, tro.QuantityTotal, 0)) [AGO],
		SUM(IIF(tro.Month = 9, tro.QuantityTotal, 0)) [SEP],
		SUM(IIF(tro.Month =10, tro.QuantityTotal, 0)) [OCT],
		SUM(IIF(tro.Month =11, tro.QuantityTotal, 0)) [NOV],
		SUM(IIF(tro.Month =12, tro.QuantityTotal, 0)) [DIC]
	FROM
	(
		SELECT
			ip.Code ProductCode, 
			ip.Name ProductName,
			imu.Code MeasurementUnitCode, 
			imu.Name MeasurementUnitName,
			atc.Concentration,
			ws.Code WarehouseSourceCode, 
			ws.Name WarehouseSourceName,
			YEAR(tro.DocumentDate) Year,
			MONTH(tro.DocumentDate) Month,
			tro.DispatchTo,
			IIF(tro.DispatchTo = 1, wt.Code, fut.Code) TargetCode, 
			IIF(tro.DispatchTo = 1, wt.Name, fut.Name) TargetName,
			SUM(trod.Quantity) QuantityTotal
		FROM Inventory.TransferOrder tro WITH (NOLOCK)
		JOIN Inventory.Warehouse ws WITH (NOLOCK) ON tro.SourceWarehouseId = ws.Id

		JOIN Inventory.TransferOrderDetail trod WITH (NOLOCK) ON tro.Id = trod.TransferOrderId
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON trod.ProductId = ip.Id

		LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH (NOLOCK) ON ip.MeasurementUnitId = imu.Id
		LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id

		LEFT JOIN Inventory.Warehouse wt WITH (NOLOCK) ON tro.TargetWarehouseId = wt.Id
		LEFT JOIN Payroll.FunctionalUnit fut WITH (NOLOCK) ON tro.TargetFunctionalUnitId = fut.Id

		WHERE tro.Status = 2
			AND tro.OrderType = 2
			AND CAST(tro.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd

		GROUP BY	ip.Code, ip.Name,
					imu.Code, imu.Name,
					atc.Concentration,
					ws.Code, ws.Name,
					YEAR(tro.DocumentDate), MONTH(tro.DocumentDate),
					tro.DispatchTo,
					wt.Code, wt.Name,
					fut.Code, fut.Name
	) tro
	GROUP BY	tro.ProductCode, tro.ProductName,
				tro.MeasurementUnitCode, tro.MeasurementUnitName,
				tro.Concentration,
				tro.WarehouseSourceCode, tro.WarehouseSourceName,
				tro.Year,
				tro.DispatchTo,
				tro.TargetCode, tro.TargetName
END