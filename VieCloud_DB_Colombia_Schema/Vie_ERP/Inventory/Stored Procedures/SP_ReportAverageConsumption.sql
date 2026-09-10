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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de consumo promedio mensual de medicamentos e insumos despachados desde una bodega origen hacia otras bodegas o unidades funcionales, dentro de un rango de fechas. Consolida las órdenes de transferencia completadas (tipo despacho, estado aprobado) y pivota las cantidades consumidas mes a mes en columnas de enero a diciembre, agrupadas por producto, unidad de medida, concentración ATC, bodega de origen, destino y año. Útil para análisis de rotación de inventario, planificación de compras y control de consumo por servicio o almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportAverageConsumption';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportAverageConsumption';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte pivoteado por mes (ENE–DIC) del consumo promedio de productos de inventario a partir de órdenes de transferencia confirmadas, agrupado por producto, unidad de medida, origen y destino.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un rango de fechas (@DateStart y @DateEnd) para filtrar por DocumentDate.; Deben existir órdenes de transferencia con Status = 2 y OrderType = 2 en el rango para obtener resultados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes de transferencia con Status = 2 (confirmadas/aprobadas) y OrderType = 2.; El filtrado de fechas se aplica sobre CAST(DocumentDate AS DATE) inclusivo entre @DateStart y @DateEnd.; Las cantidades se totalizan por (producto, unidad, concentración, bodega origen, destino, año, mes) antes de pivotar a columnas mensuales.; El destino del despacho es excluyente: bodega o unidad funcional, según DispatchTo.; Todas las consultas usan WITH (NOLOCK), por lo que se aceptan lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de transferencia; Consumo promedio; Bodega/Almacén origen y destino; Unidad funcional; Producto de inventario; Unidad de medida; Clasificación ATC; Concentración del medicamento', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto tabular con columnas pivote por mes (ENE..DIC) sumando QuantityTotal según MONTH(DocumentDate), agrupado por producto, unidad, concentración, origen, destino y año.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si tro.DispatchTo = 1 → El destino se etiqueta como ''ALMACEN'' y se toma el código/nombre desde Inventory.Warehouse (TargetWarehouseId). else El destino se etiqueta como ''UNIDAD FUNCIONAL'' y se toma el código/nombre desde Payroll.FunctionalUnit (TargetFunctionalUnitId).; si tro.Month = N (N de 1 a 12) → La cantidad QuantityTotal se acumula en la columna del mes correspondiente (ENE..DIC); de lo contrario aporta 0. else 0', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.TransferOrder; Inventory.TransferOrderDetail; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.InventoryMeasurementUnit; Inventory.ATC; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageConsumption';
-- GO
