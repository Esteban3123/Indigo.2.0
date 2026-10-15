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