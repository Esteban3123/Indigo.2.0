CREATE PROCEDURE [Inventory].[SP_ReportAverageIncome]
	@DateStart DATE,
	@DateEnd DATE
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		CONCAT(tro.ProductCode, ' - ', tro.ProductName) [PRODUCTO],
		CONCAT(tro.WarehouseSourceCode, ' - ', tro.WarehouseSourceName) [ALMACEN],
		CONCAT(tro.Nit , ' - ', tro.Name ) [PROVEEDOR], 
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
		select 
					ip.Code ProductCode, 
					ip.Name ProductName,
					ws.Code WarehouseSourceCode, 
					ws.Name WarehouseSourceName,
					s.Code Nit,
					s.Name Name,
					YEAR(ev.DocumentDate) Year,
					MONTH(ev.DocumentDate) Month,
					SUM(evd.Quantity) QuantityTotal 
		from Inventory .EntranceVoucher as ev WITH (NOLOCK)
		JOIN Inventory.Warehouse ws WITH (NOLOCK) ON ev.WarehouseId  = ws.Id 
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId 
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
		join Common .Supplier as s on s.Id =ev.SupplierId 
		LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
		WHERE ev.Status = 2
			AND CAST(ev.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
		GROUP BY	ip.Code, ip.Name,ws.Code, ws.Name,s.Code ,s.Name ,YEAR(ev.DocumentDate), MONTH(ev.DocumentDate)
	) tro
	GROUP BY	tro.ProductCode, tro.ProductName,
				tro.WarehouseSourceCode, tro.WarehouseSourceName,
				tro.Nit ,tro.Name ,	tro.Year
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de entradas promedio de inventario por producto, almacén y proveedor en un rango de fechas. Consolida las cantidades recibidas en bodega (comprobantes de entrada en estado aprobado/confirmado) y las pivota por mes, mostrando columnas de enero a diciembre para el año correspondiente. Permite analizar el comportamiento mensual de los ingresos de mercancía —medicamentos, insumos y dispositivos médicos— por cada combinación de producto, almacén y proveedor, útil para planeación de compras, evaluación de proveedores y control de abastecimiento. Recibe como parámetros la fecha de inicio (@DateStart) y fecha de fin (@DateEnd) del período a consultar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportAverageIncome';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportAverageIncome';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte pivoteado por mes con las cantidades ingresadas al inventario por producto, almacén y proveedor en un rango de fechas, mostrando totales mensuales (ENE–DIC) por año.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas @DateStart y @DateEnd deben definir un rango válido sobre EntranceVoucher.DocumentDate.; Solo se consideran comprobantes de entrada con Status = 2 (estado considerado válido/aprobado para reportar).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se totalizan comprobantes de entrada con Status = 2; cualquier otro estado se excluye del reporte.; El filtro de fecha se aplica sobre CAST(DocumentDate AS DATE), ignorando la parte horaria.; La agrupación se realiza por producto (Code+Name), almacén (Code+Name), proveedor (Nit+Name) y año, distribuyendo cantidades por mes en columnas separadas.; Los códigos mostrados concatenan ''Code - Name'' para producto, almacén y proveedor (Nit - Name).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada de inventario; Almacén/Bodega; Proveedor (NIT); Producto de inventario; Clasificación ATC; Ingreso mensual de mercancía', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto pivot con columnas PRODUCTO, ALMACEN, PROVEEDOR, AÑO y una columna por mes (ENE..DIC) con la SUMA de evd.Quantity, filtrado por ev.Status=2 y ev.DocumentDate BETWEEN @DateStart AND @DateEnd.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si tro.Month = N (para N de 1 a 12) → Acumula tro.QuantityTotal en la columna correspondiente al mes (ENE..DIC) mediante IIF + SUM.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.Warehouse; Inventory.EntranceVoucherDetail; Inventory.InventoryProduct; Common.Supplier; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAverageIncome';
-- GO
